'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
'
'Copyright 2016 Signalworks Pty Ltd, ABN 26 066 681 598

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at
'
'http://www.apache.org/licenses/LICENSE-2.0
'
'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
'See the License for the specific language governing permissions and
'limitations under the License.
'
'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Public Class Main
    'The ADVL_Coordinates_Client_1 application is used to store locations on the earth's surface as a set of coordinates
    '    and convert the coordinates between different coordinate reference systems.
    '    The application communicates with the Andorville (TM) Coordinates application to perform the conversions.

#Region " CODING NOTES"
    'CODING NOTES:

    'ADD THE SYSTEM UTILITIES REFERENCE: ==========================================================================================
    'The following references are required by this software: 
    'Project \ Add Reference... \ ADVL_System_Utilties.dll

    'ADD THE SERVICE REFERENCE: ===================================================================================================
    'A service reference to the Message Service must be added to the source code before this service can be used.

    'Adding the service referenc to a project that includes the WcfMsgServiceLib project: -----------------------------------------
    'Project \ Add Service Reference
    'Press the Discover button.
    'Expand the items in the Services window and select IMsgService.
    'Press OK.
    '------------------------------------------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------------------------
    'Adding the service reference to other projects that dont include the WcfMsgServiceLib project: -------------------------------
    'Run the ADVL_Info_Exchange application to start the Info Exchange message service.
    'In Microsoft Visual Studio select: Project \ Add Service Reference
    'Press the down arrow and select the address of the service used by the Message Exchange:
    'http://localhost:8733/Design_Time_Addresses/WcfMsgServiceLib/Service1/mex
    'Press the Go button.
    'MsgService is found.
    'Press OK to add ServiceReference1 to the project.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'ADD THE MsgServiceCallback CODE: ---------------------------------------------------------------------------------------------
    'In Microsoft Visual Studio select: Project \ Add Class
    'MsgServiceCallback.vb
    'Add the following code to the class:
    'Imports System.ServiceModel
    'Public Class MsgServiceCallback
    '    Implements ServiceReference1.IMsgServiceCallback
    '    Public Sub OnSendMessage(message As String) Implements ServiceReference1.IMsgServiceCallback.OnSendMessage
    '        'A message has been received.
    '        'Set the InstrReceived property value to the XMessage. This will also apply the instructions in the XMessage.
    '        Main.InstrReceived = message
    '    End Sub
    'End Class
    '------------------------------------------------------------------------------------------------------------------------------
#End Region

#Region " Variable declarations - All the variables used in this form and this application." '-------------------------------------------------------------------------------------------------

    Public WithEvents ApplicationInfo As New ADVL_Utilities_Library_1.ApplicationInfo 'This object is used to store application information.
    Public WithEvents Project As New ADVL_Utilities_Library_1.Project 'This object is used to store Project information.
    Public WithEvents Message As New ADVL_Utilities_Library_1.Message 'This object is used to display messages in the Messages window.
    Public WithEvents ApplicationUsage As New ADVL_Utilities_Library_1.Usage 'This object stores application usage information.



    'Declare Forms used by the application:
    Public WithEvents Utilities As frmUtilities
    'Public WithEvents XmsgInstructions As frmXmsgInstructions


    'Declare objects used to connect to the Application Network:
    Public client As ServiceReference1.MsgServiceClient
    Public WithEvents XMsg As New ADVL_Utilities_Library_1.XMessage
    Dim XDoc As New System.Xml.XmlDocument
    Public Status As New System.Collections.Specialized.StringCollection
    Dim ClientName As String 'The name of the client requesting coordinate operations
    Dim MessageText As String 'The text of a message sent through the MessageExchange
    Dim MessageDest As String 'The destination of a message sent through the MessageExchange.

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Properties - All the properties used in this form and this application" '------------------------------------------------------------------------------------------------------------

    'The Andorville Exchange connection hashcode. This is used to identify a connection in the Application Network when reconnecting.
    Private _connectionHashcode As Integer
    Property ConnectionHashcode As Integer
        Get
            Return _connectionHashcode
        End Get
        Set(value As Integer)
            _connectionHashcode = value
        End Set
    End Property

    'True if the application is connected to the Application Network.
    Private _connectedToAppNet As Boolean = False
    Property ConnectedToAppnet As Boolean
        Get
            Return _connectedToAppNet
        End Get
        Set(value As Boolean)
            _connectedToAppNet = value
        End Set
    End Property

    Private _instrReceived As String = "" 'Contains Instructions received from the Application Network message service.
    Property InstrReceived As String
        Get
            Return _instrReceived
        End Get
        Set(value As String)
            If value = Nothing Then
                Message.Add("Empty message received!")
            Else
                _instrReceived = value

                'Add the message to the XMessages window:
                Message.Color = Color.Blue
                Message.FontStyle = FontStyle.Bold
                'Message.Add("Message received: " & vbCrLf)
                Message.XAdd("Message received: " & vbCrLf)
                Message.SetNormalStyle()
                Message.XAdd(_instrReceived & vbCrLf & vbCrLf)

                If _instrReceived.StartsWith("<XMsg>") Then 'This is an XMessage set of instructions.
                    Try
                        Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
                        XDoc.LoadXml(XmlHeader & vbCrLf & _instrReceived)
                        XMsg.Run(XDoc, Status)
                    Catch ex As Exception
                        Message.Add("Error running XMsg: " & ex.Message & vbCrLf)
                    End Try

                    'XMessage has been run.
                    'Reply to this message:
                    'Add the message reply to the XMessages window:
                    If ClientName = "" Then
                        'No client to send a message to!
                    Else
                        Message.Color = Color.Red
                        Message.FontStyle = FontStyle.Bold
                        'Message.Add("Message sent to " & ClientName & ":" & vbCrLf)
                        Message.XAdd("Message sent to " & ClientName & ":" & vbCrLf)
                        Message.SetNormalStyle()
                        Message.XAdd(MessageText & vbCrLf & vbCrLf)
                        MessageDest = ClientName
                        'SendMessage sends the contents of MessageText to MessageDest.
                        SendMessage() 'This subroutine triggers the timer to send the message after a short delay.
                    End If
                Else

                End If
            End If

        End Set
    End Property

#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Process XML files - Read and write XML files." '-------------------------------------------------------------------------------------------------------------------------------------

    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <!--Form settings for Main form.-->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <!---->
                               <ProjectedCrsList>
                                   <%= From Item In cmbProjectedCRS.Items
                                       Select _
                                       <ProjectedCrsName><%= Item %></ProjectedCrsName>
                                   %>
                               </ProjectedCrsList>
                               <SelectedCrs><%= cmbProjectedCRS.SelectedIndex %></SelectedCrs>
                               <SelectedTabIndex><%= TabControl1.SelectedIndex %></SelectedTabIndex>
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        Dim SettingsFileName As String = "Formsettings_" & ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub


    'Private Sub WriteFormSettingsXmlFile()
    '    'Write form settings

    '    Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
    '                       <!---->
    '                       <!--Form settings for Main form.-->
    '                       <FormSettings>
    '                           <Left><%= Me.Left %></Left>
    '                           <Top><%= Me.Top %></Top>
    '                           <Width><%= Me.Width %></Width>
    '                           <Height><%= Me.Height %></Height>
    '                           <!---->
    '                           <ProjectedCrsList>
    '                               <%= From Item In cmbProjectedCRS.Items
    '                                   Select _
    '                                   <ProjectedCrsName><%= Item %></ProjectedCrsName>
    '                               %>
    '                           </ProjectedCrsList>
    '                           <SelectedCrs><%= cmbProjectedCRS.SelectedIndex %></SelectedCrs>
    '                           <SelectedTabIndex><%= TabControl1.SelectedIndex %></SelectedTabIndex>
    '                       </FormSettings>

    '    If Trim(ProjectPath) <> "" Then 'Write the Form Settings file in the Project Directory
    '        settingsData.Save(ProjectPath & "\" & "FormSettings_" & Me.Text & ".xml")
    '    Else 'Write the Form Settings file in the Application Directory
    '        settingsData.Save(ApplicationDir & "\" & "FormSettings_" & Me.Text & ".xml")
    '    End If

    'End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        Dim SettingsFileName As String = "Formsettings_" & ApplicationInfo.Name & "_" & Me.Text & ".xml"

        If Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore form position and size:
            If Settings.<FormSettings>.<Left>.Value = Nothing Then
                'Form setting not saved.
            Else
                Me.Left = Settings.<FormSettings>.<Left>.Value
            End If

            If Settings.<FormSettings>.<Top>.Value = Nothing Then
                'Form setting not saved.
            Else
                Me.Top = Settings.<FormSettings>.<Top>.Value
            End If

            If Settings.<FormSettings>.<Height>.Value = Nothing Then
                'Form setting not saved.
            Else
                Me.Height = Settings.<FormSettings>.<Height>.Value
            End If

            If Settings.<FormSettings>.<Width>.Value = Nothing Then
                'Form setting not saved.
            Else
                Me.Width = Settings.<FormSettings>.<Width>.Value
            End If

            'Add code to read other saved setting here:

            'Read other settings:
            For Each Item In Settings.<FormSettings>.<ProjectedCrsList>.<ProjectedCrsName>
                'For Each Item In settingsData.<FormSettings>.<ProjectedCrsList>
                cmbProjectedCRS.Items.Add(Item.Value)
            Next

            If Settings.<FormSettings>.<SelectedCrs>.Value = Nothing Then
            Else
                cmbProjectedCRS.SelectedIndex = Settings.<FormSettings>.<SelectedCrs>.Value
            End If
            If Settings.<FormSettings>.<SelectedTabIndex>.Value = Nothing Then

            Else
                TabControl1.SelectedIndex = Settings.<FormSettings>.<SelectedTabIndex>.Value
            End If

        End If
    End Sub

    'Private Sub ReadFormSettingsXmlFile()
    '    'Read the Form Settings XML file:

    '    Dim FilePath As String

    '    If Trim(ProjectPath) <> "" Then 'Read the Form Settings file in the Project Directory
    '        FilePath = ProjectPath & "\" & "FormSettings_" & Me.Text & ".xml"
    '        If System.IO.File.Exists(FilePath) Then
    '            ReadSettings(FilePath)
    '        Else
    '            'Initialise property values:
    '        End If
    '    Else 'Read the Form Settings in the Application Directory
    '        FilePath = ApplicationDir & "\" & "FormSettings_" & Me.Text & ".xml"
    '        If System.IO.File.Exists(FilePath) Then
    '            ReadSettings(FilePath)

    '        Else
    '            'Initialise property values:
    '        End If
    '    End If

    'End Sub

    'Private Sub ReadSettings(ByVal FilePath As String)
    '    Dim settingsData As System.Xml.Linq.XDocument = XDocument.Load(FilePath)

    '    'Read form position and size:
    '    Me.Left = settingsData.<FormSettings>.<Left>.Value
    '    Me.Top = settingsData.<FormSettings>.<Top>.Value
    '    Me.Height = settingsData.<FormSettings>.<Height>.Value
    '    Me.Width = settingsData.<FormSettings>.<Width>.Value

    '    'Read other settings:
    '    For Each Item In settingsData.<FormSettings>.<ProjectedCrsList>.<ProjectedCrsName>
    '        'For Each Item In settingsData.<FormSettings>.<ProjectedCrsList>
    '        cmbProjectedCRS.Items.Add(Item.Value)
    '    Next
    '    'If IsNothing(settingsData.<FormSettings>.<SelectedCrs>.Value) Then
    '    If settingsData.<FormSettings>.<SelectedCrs>.Value = Nothing Then
    '    Else
    '        cmbProjectedCRS.SelectedIndex = settingsData.<FormSettings>.<SelectedCrs>.Value
    '    End If
    '    If settingsData.<FormSettings>.<SelectedTabIndex>.Value = Nothing Then

    '    Else
    '        TabControl1.SelectedIndex = settingsData.<FormSettings>.<SelectedTabIndex>.Value
    '    End If


    'End Sub

    'Private Sub WriteApplicationInfoXmlFile()
    '    'Write the Application Info file:

    '    Dim applicationInfo = <?xml version="1.0" encoding="utf-8"?>
    '                          <!---->
    '                          <!--Application Information file for Application: ADVL_CoordinatesClient-->
    '                          <!---->
    '                          <!--Application Information.-->
    '                          <ApplicationInfo>
    '                              <ApplicationLastUsed><%= Format(Now, "d-MMM-yyyy H:mm:ss") %></ApplicationLastUsed>
    '                              <!---->
    '                              <LastProjectUsed>
    '                                  <Name><%= ProjectName %></Name>
    '                                  <Description><%= ProjectDescription %></Description>
    '                                  <Path><%= ProjectPath %></Path>
    '                                  <CreationDate><%= Format(ProjectCreationDate, "d-MMM-yyyy H:mm:ss") %></CreationDate>
    '                                  <LastUsed><%= Format(Now, "d-MMM-yyyy H:mm:ss") %></LastUsed>
    '                              </LastProjectUsed>
    '                              <!---->
    '                          </ApplicationInfo>

    '    applicationInfo.Save(ApplicationDir & "\" & "Application_Info.xml")

    'End Sub

    'Private Sub WriteProjectInfoXmlFile()
    '    'Write the Project Info file:

    '    If ProjectPath <> "" Then
    '        Dim projectInfo = <?xml version="1.0" encoding="utf-8"?>
    '                          <!---->
    '                          <!--Project Information file for Application: ADVL_CoordinatesClient-->
    '                          <ProjectInfo>
    '                              <!---->
    '                              <!--Application Information.-->
    '                              <ApplicationInfo>
    '                                  <Name><%= "TDS_Import" %></Name>
    '                                  <Version><%= "1.00" %></Version>
    '                              </ApplicationInfo>
    '                              <!---->
    '                              <!--Project Information.-->
    '                              <Project>
    '                                  <Name><%= ProjectName %></Name>
    '                                  <Description><%= ProjectDescription %></Description>
    '                                  <Path><%= ProjectPath %></Path>
    '                                  <CreationDate><%= Format(ProjectCreationDate, "d-MMM-yyyy H:mm:ss") %></CreationDate>
    '                                  <LastUsed><%= Format(Now, "d-MMM-yyyy H:mm:ss") %></LastUsed>
    '                              </Project>
    '                              <!---->
    '                          </ProjectInfo>

    '        projectInfo.Save(ProjectPath & "\" & "Project_Info.xml")
    '    End If
    'End Sub

    Private Sub ReadApplicationInfo()
        'Read the Application Information.
        'Generate a new ApplicationInfo file if none exists.
        'ApplicationInfo.ApplicationDir = My.Application.Info.DirectoryPath.ToString 'Set the Application Directory property
        If ApplicationInfo.FileExists Then
            ApplicationInfo.ReadFile()
        Else
            'There is no Application_Info.xml file.
            'Set up default application properties: ---------------------------------------------------------------------------
            DefaultAppProperties()
        End If

    End Sub

    Private Sub DefaultAppProperties()

        ApplicationInfo.Name = "ADVL_Coordinates_Client_1"

        'ApplicationInfo.ApplicationDir is set when the application is started.
        ApplicationInfo.ExecutablePath = Application.ExecutablePath

        ApplicationInfo.Description = "Client of the ADVL_Coordinates_1 application for processing coordinate data including converting between coordinate reference systems."
        ApplicationInfo.CreationDate = "7-Jan-2016 12:00:00"

        'Author -----------------------------------------------------------------------------------------------------------
        ApplicationInfo.Author.Name = "Signalworks Pty Ltd"
        ApplicationInfo.Author.Description = "Signalworks Pty Ltd" & vbCrLf & _
            "Australian Proprietary Company" & vbCrLf & _
            "ABN 26 066 681 598" & vbCrLf & _
            "Registration Date 05/10/1994"

        ApplicationInfo.Author.Contact = "http://www.andorville.com.au/"


        'File Associations: -----------------------------------------------------------------------------------------------
        'Dim Assn1 As New ADVL_System_Utilities.FileAssociation
        'Assn1.Extension = "ADVLCoord"
        'Assn1.Description = "Andorville (TM) software coordinate system parameter file"
        'ApplicationInfo.FileAssociations.Add(Assn1)

        'Version ----------------------------------------------------------------------------------------------------------
        ApplicationInfo.Version.Major = My.Application.Info.Version.Major
        ApplicationInfo.Version.Minor = My.Application.Info.Version.Minor
        ApplicationInfo.Version.Build = My.Application.Info.Version.Build
        ApplicationInfo.Version.Revision = My.Application.Info.Version.Revision

        'Copyright --------------------------------------------------------------------------------------------------------
        ApplicationInfo.Copyright.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        ApplicationInfo.Copyright.PublicationYear = "2016"

        'Trademarks -------------------------------------------------------------------------------------------------------
        Dim Trademark1 As New ADVL_Utilities_Library_1.Trademark
        Trademark1.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark1.Text = "Andorville"
        Trademark1.Registered = False 'Note the trademark may be registered in some countries. Setting Registered to False displays the TM symbol instead of the registered trademark symbol. 
        'This is the suitable notice for any country, where the trademark is registered or unregistered.
        Trademark1.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark1)

        Dim Trademark2 As New ADVL_Utilities_Library_1.Trademark
        Trademark2.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark2.Text = "AL-H7"
        Trademark2.Registered = False 'Note the trademark may be registered in some countries. Setting Registered to False displays the TM symbol instead of the registered trademark symbol.
        'This is the suitable notice for any country, where the trademark is registered or unregistered.
        Trademark2.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark2)

        'License -------------------------------------------------------------------------------------------------------
        ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.Apache_License_2_0
        ApplicationInfo.License.CopyrightOwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        ApplicationInfo.License.PublicationYear = "2016"
        ApplicationInfo.License.Notice = ApplicationInfo.License.ApacheLicenseNotice
        ApplicationInfo.License.Text = ApplicationInfo.License.ApacheLicenseText

        'Source Code: --------------------------------------------------------------------------------------------------
        ApplicationInfo.SourceCode.Language = "Visual Basic 2013"
        ApplicationInfo.SourceCode.FileName = ""
        ApplicationInfo.SourceCode.FileSize = 0
        ApplicationInfo.SourceCode.FileHash = ""
        ApplicationInfo.SourceCode.WebLink = ""
        ApplicationInfo.SourceCode.Contact = ""
        ApplicationInfo.SourceCode.Comments = ""

        'ModificationSummary: -----------------------------------------------------------------------------------------
        ApplicationInfo.ModificationSummary.BaseCodeName = ""
        ApplicationInfo.ModificationSummary.BaseCodeDescription = ""
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Major = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Minor = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Build = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Revision = 0
        ApplicationInfo.ModificationSummary.Description = "This is the first released version of the application. No earlier base code used."

        'Library List: ------------------------------------------------------------------------------------------------
        'Add ADVL_System_Utilties library:
        Dim NewLib As New ADVL_Utilities_Library_1.LibrarySummary
        NewLib.Name = "ADVL_System_Utilities"
        NewLib.Description = "System Utility classes used in Andorville (TM) software development system applications"
        NewLib.CreationDate = "7-Jan-2016 12:00:00"
        NewLib.LicenseNotice = "Copyright 2016 Signalworks Pty Ltd, ABN 26 066 681 598" & vbCrLf &
                               vbCrLf & _
                               "Licensed under the Apache License, Version 2.0 (the ""License"");" & vbCrLf & _
                               "you may not use this file except in compliance with the License." & vbCrLf & _
                               "You may obtain a copy of the License at" & vbCrLf & _
                               vbCrLf & _
                               "http://www.apache.org/licenses/LICENSE-2.0" & vbCrLf & _
                               vbCrLf & _
                               "Unless required by applicable law or agreed to in writing, software" & vbCrLf & _
                               "distributed under the License is distributed on an ""AS IS"" BASIS," & vbCrLf & _
                               "WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied." & vbCrLf & _
                               "See the License for the specific language governing permissions and" & vbCrLf & _
                               "limitations under the License." & vbCrLf

        NewLib.CopyrightNotice = "Copyright 2016 Signalworks Pty Ltd, ABN 26 066 681 598"

        NewLib.Version.Major = 1
        NewLib.Version.Minor = 0
        NewLib.Version.Build = 1
        NewLib.Version.Revision = 0

        NewLib.Author.Name = "Signalworks Pty Ltd"
        NewLib.Author.Description = "Signalworks Pty Ltd" & vbCrLf & _
            "Australian Proprietary Company" & vbCrLf & _
            "ABN 26 066 681 598" & vbCrLf & _
            "Registration Date 05/10/1994"

        NewLib.Author.Contact = "http://www.andorville.com.au/"

        Dim NewClass1 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass1.Name = "ZipComp"
        NewClass1.Description = "The ZipComp class is used to compress files into and extract files from a zip file."
        NewLib.Classes.Add(NewClass1)
        Dim NewClass2 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass2.Name = "XSequence"
        NewClass2.Description = "The XSequence class is used to run an XML property sequence (XSequence) file. XSequence files are used to record and replay processing sequences in Andorville (TM) software applications."
        NewLib.Classes.Add(NewClass2)
        Dim NewClass3 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass3.Name = "XMessage"
        NewClass3.Description = "The XMessage class is used to read an XML Message (XMessage). An XMessage is a simplified XSequence used to exchange information between Andorville (TM) software applications."
        NewLib.Classes.Add(NewClass3)
        Dim NewClass4 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass4.Name = "Location"
        NewClass4.Description = "The Location class consists of properties and methods to store data in a location, which is either a directory or archive file."
        NewLib.Classes.Add(NewClass4)
        Dim NewClass5 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass5.Name = "Project"
        NewClass5.Description = "An Andorville (TM) software application can store data within one or more projects. Each project stores a set of related data files. The Project class contains properties and methods used to manage a project."
        NewLib.Classes.Add(NewClass5)
        Dim NewClass6 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass6.Name = "ProjectSummary"
        NewClass6.Description = "ProjectSummary stores a summary of a project."
        NewLib.Classes.Add(NewClass6)
        Dim NewClass7 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass7.Name = "DataFileInfo"
        NewClass7.Description = "The DataFileInfo class stores information about a data file."
        NewLib.Classes.Add(NewClass7)
        Dim NewClass8 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass8.Name = "Message"
        NewClass8.Description = "The Message class contains text properties and methods used to display messages in an Andorville (TM) software application."
        NewLib.Classes.Add(NewClass8)
        Dim NewClass9 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass9.Name = "ApplicationSummary"
        NewClass9.Description = "The ApplicationSummary class stores a summary of an Andorville (TM) software application."
        NewLib.Classes.Add(NewClass9)
        Dim NewClass10 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass10.Name = "LibrarySummary"
        NewClass10.Description = "The LibrarySummary class stores a summary of a software library used by an application."
        NewLib.Classes.Add(NewClass10)
        Dim NewClass11 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass11.Name = "ClassSummary"
        NewClass11.Description = "The ClassSummary class stores a summary of a class contained in a software library."
        NewLib.Classes.Add(NewClass11)
        Dim NewClass12 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass12.Name = "ModificationSummary"
        NewClass12.Description = "The ModificationSummary class stores a summary of any modifications made to an application or library."
        NewLib.Classes.Add(NewClass12)
        Dim NewClass13 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass13.Name = "ApplicationInfo"
        NewClass13.Description = "The ApplicationInfo class stores information about an Andorville (TM) software application."
        NewLib.Classes.Add(NewClass13)
        Dim NewClass14 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass14.Name = "Version"
        NewClass14.Description = "The Version class stores application, library or project version information."
        NewLib.Classes.Add(NewClass14)
        Dim NewClass15 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass15.Name = "Author"
        NewClass15.Description = "The Author class stores information about an Author."
        NewLib.Classes.Add(NewClass15)
        Dim NewClass16 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass16.Name = "FileAssociation"
        NewClass16.Description = "The FileAssociation class stores the file association extension and description. An application can open files on its file association list."
        NewLib.Classes.Add(NewClass16)
        Dim NewClass17 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass17.Name = "Copyright"
        NewClass17.Description = "The Copyright class stores copyright information."
        NewLib.Classes.Add(NewClass17)
        Dim NewClass18 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass18.Name = "License"
        NewClass18.Description = "The License class stores license information."
        NewLib.Classes.Add(NewClass18)
        Dim NewClass19 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass19.Name = "SourceCode"
        NewClass19.Description = "The SourceCode class stores information about the source code for the application."
        NewLib.Classes.Add(NewClass19)
        Dim NewClass20 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass20.Name = "Usage"
        NewClass20.Description = "The Usage class stores information about application or project usage."
        NewLib.Classes.Add(NewClass20)
        Dim NewClass21 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass21.Name = "Trademark"
        NewClass21.Description = "The Trademark class stored information about a trademark used by the author of an application or data."
        NewLib.Classes.Add(NewClass21)

        ApplicationInfo.Libraries.Add(NewLib)

        'Add other library information here: --------------------------------------------------------------------------


    End Sub

#End Region 'Process XML Files ----------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Form Display Methods - Code used to display this form." '----------------------------------------------------------------------------------------------------------------------------

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Set the Application Directory path: ------------------------------------------------
        Project.ApplicationDir = My.Application.Info.DirectoryPath.ToString

        'Read the Application Information file: ---------------------------------------------
        ApplicationInfo.ApplicationDir = My.Application.Info.DirectoryPath.ToString 'Set the Application Directory property
        'If ApplicationInfo.ApplicationLocked Then
        '    MessageBox.Show("The application is locked. If the application is not already in use, remove the 'Application_Info.lock file from the application directory: " & ApplicationInfo.ApplicationDir, "Notice", MessageBoxButtons.OK)
        '    Application.Exit()
        'End If
        If ApplicationInfo.ApplicationLocked Then
            MessageBox.Show("The application is locked. If the application is not already in use, remove the 'Application_Info.lock file from the application directory: " & ApplicationInfo.ApplicationDir, "Notice", MessageBoxButtons.OK)
            Dim dr As Windows.Forms.DialogResult
            dr = MessageBox.Show("Press 'Yes' to unlock the application", "Notice", MessageBoxButtons.YesNo)
            If dr = Windows.Forms.DialogResult.Yes Then
                ApplicationInfo.UnlockApplication()
            Else
                Application.Exit()
            End If
        End If
        ReadApplicationInfo()
        ApplicationInfo.LockApplication()

        'Read the Application Usage information: --------------------------------------------
        ApplicationUsage.StartTime = Now
        ApplicationUsage.SaveLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory
        ApplicationUsage.SaveLocn.Path = Project.ApplicationDir
        ApplicationUsage.RestoreUsageInfo()

        'Restore Project information: -------------------------------------------------
        Project.ApplicationName = ApplicationInfo.Name
        Project.ReadLastProjectInfo()
        Project.ReadProjectInfoFile()
        Project.Usage.StartTime = Now

        ApplicationInfo.SettingsLocn = Project.SettingsLocn

        'Set up the Message object:
        Message.ApplicationName = ApplicationInfo.Name
        Message.SettingsLocn = Project.SettingsLocn

        'Restore the form settings: ---------------------------------------------------------
        RestoreFormSettings()

        'Set up input angle unit options:
        cmbInput.Items.Add("Degrees, Minutes, Seconds")
        cmbInput.Items.Add("Decimal Degrees")
        cmbInput.Items.Add("Sexagesimal Degrees")
        cmbInput.Items.Add("Radians")
        cmbInput.Items.Add("Gradians")
        cmbInput.Items.Add("Turns")
        cmbInput.SelectedIndex = 0

        'Set up output angle unit options:
        cmbOutput.Items.Add("Degrees, Minutes, Seconds")
        cmbOutput.Items.Add("Decimal Degrees")
        cmbOutput.Items.Add("Sexagesimal Degrees")
        cmbOutput.Items.Add("Radians")
        cmbOutput.Items.Add("Gradians")
        cmbOutput.Items.Add("Turns")
        cmbOutput.SelectedIndex = 1

        'Set up projected input data type options:
        cmbProjnInput.Items.Add("Latitude, Longitude")
        cmbProjnInput.Items.Add("Longitude, Latitude")
        cmbProjnInput.Items.Add("Easting, Northing")
        cmbProjnInput.Items.Add("Northing, Easting")
        cmbProjnInput.SelectedIndex = 0 'Select first item

        'Set up projected output data type options:
        cmbProjnOutput.Items.Add("Latitude, Longitude")
        cmbProjnOutput.Items.Add("Longitude, Latitude")
        cmbProjnOutput.Items.Add("Easting, Northing")
        cmbProjnOutput.Items.Add("Northing, Easting")
        cmbProjnOutput.SelectedIndex = 2 'Select third item

        'lblInUnits1.Location = New Point(9, 106)
        lblInUnits1.Location = New Point(9, 138)
        'txtInput1.Location = New Point(9, 123)
        txtInput1.Location = New Point(9, 155)
        'lblInUnits2.Location = New Point(253, 106)
        lblInUnits2.Location = New Point(253, 138)
        'txtInput2.Location = New Point(253, 123)
        txtInput2.Location = New Point(253, 155)
        'lblOutUnits1.Location = New Point(495, 106)
        lblOutUnits1.Location = New Point(495, 138)
        'txtOutput1.Location = New Point(495, 123)
        txtOutput1.Location = New Point(495, 155)
        'lblOutUnits2.Location = New Point(733, 106)
        lblOutUnits2.Location = New Point(733, 138)
        'txtOutput2.Location = New Point(733, 123)
        txtOutput2.Location = New Point(733, 155)

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Application

        DisconnectFromAppNet()

        'Save the form settings: ------------------------------------------------------------
        SaveFormSettings()

        'Update the Application Information file: -------------------------------------------
        ApplicationInfo.WriteFile()
        ApplicationInfo.UnlockApplication()

        'Update the Project Information file: -----------------------------------------------
        Project.SaveProjectInfoFile()

        'Save Application Usage information: ------------------------------------------------
        ApplicationUsage.SaveUsageInfo()

        'Close all the application forms:
        Application.Exit()
    End Sub

#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Open and close forms - Code used to open and close other forms."

    Private Sub btnUtilities_Click(sender As Object, e As EventArgs) Handles btnUtilities.Click
        'Show the Utilities form:
        If IsNothing(Utilities) Then
            Utilities = New frmUtilities
            Utilities.Show()
        Else
            Utilities.Show()
        End If
    End Sub

    Private Sub Utilities_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Utilities.FormClosed
        Utilities = Nothing
    End Sub

    Private Sub btnMessages_Click(sender As Object, e As EventArgs) Handles btnMessages.Click
        Message.ApplicationName = ApplicationInfo.Name
        Message.SettingsLocn = Project.SettingsLocn
        Message.Show()
        Message.MessageForm.BringToFront()
    End Sub

    Private Sub btnAppInfo_Click(sender As Object, e As EventArgs) Handles btnAppInfo.Click
        ApplicationInfo.ShowInfo()
    End Sub

#End Region


#Region " Form Methods - The main actions performed by this form."


#Region " Online/Offline code."


    Private Sub btnOnline_Click(sender As Object, e As EventArgs) Handles btnOnline.Click
        'Connect to or disconnect from the Message Exchange.
        If ConnectedToAppnet = False Then
            'ConnectToExchange()
            ConnectToAppNet()
        Else
            DisconnectFromAppNet()
        End If
    End Sub

    Private Sub ConnectToAppNet()
        'Connect to the Andorville (TM) Application Network

        Dim Result As Boolean

        If IsNothing(client) Then
            client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
        End If


        If client.State = ServiceModel.CommunicationState.Faulted Then
            Message.SetWarningStyle()
            Message.Add("client state is faulted. Connection not made!" & vbCrLf)
        Else
            Try
                'Send timeout temporarily set to two seconds. If the Message Exchange Service is not running and the timeout is not reduces, the application will appear to freeze.
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 8) 'Temporarily set the send timeaout to 8 seconds.  (2 seconds is not enough time!!!)
                'Result = client.Connect("CoordinatesClient")
                'Result = client.Connect(ApplicationName, ServiceReference1.clsConnectionenumAppType.Application, False, False) 'Application Name is "CoordinatesClient"
                'Result = client.Connect(ApplicationInfo.Name, ServiceReference1.clsConnectionenumAppType.Application, False, False) 'Application Name is "CoordinatesClient"
                Result = client.Connect(ApplicationInfo.Name, ServiceReference1.clsConnectionAppTypes.Application, False, False) 'Application Name is "CoordinatesClient"
                'appName, appType, getAllWarnings, getAllMessages

                If Result = True Then
                    Message.SetNormalStyle()
                    'Message.Add("Connected to the Message Exchange as CoordinatesClient" & vbCrLf)
                    Message.Add("Connected to the Application Network as " & ApplicationInfo.Name & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeaout to 1 hour
                    btnOnline.Text = "Online"
                    btnOnline.ForeColor = Color.ForestGreen
                    'ConnectedToExchange = True
                    ConnectedToAppnet = True
                    SendApplicationInfo()
                Else
                    Message.SetWarningStyle()
                    'Message.Add("Connection to the Message Exchange failed!" & vbCrLf)
                    Message.Add("Connection to the Application Network failed!" & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeaout to 1 hour
                End If
            Catch ex As System.TimeoutException
                'MessageAdd("Timeout error. Check if the MessageExchange is running." & vbCrLf)
                Message.SetWarningStyle()
                Message.Add("Timeout error. Check if the Application Network is running." & vbCrLf)
            Catch ex As Exception
                Message.SetWarningStyle()
                Message.Add("Cannot connect to the Application Network. Error type: " & ex.GetType.ToString & vbCrLf)
                'MessageAdd("Error message: " & ex.InnerException.Message & vbCrLf)
                Message.Add("Error message: " & ex.Message & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeaout to 1 hour
            End Try
        End If

    End Sub

    Private Sub DisconnectFromAppNet()
        'Disconnect from the Andorville (TM) Application Network.

        Dim Result As Boolean

        If IsNothing(client) Then
            Message.Add("Already disconnected from the Application Network." & vbCrLf)
            btnOnline.Text = "Offline"
            btnOnline.ForeColor = Color.Black
            ConnectedToAppnet = False
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.SetWarningStyle()
                Message.Add("client state is faulted." & vbCrLf)
            Else
                Try
                    'client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
                    Message.Add("Running client.Disconnect(ApplicationName)   ApplicationName = " & ApplicationInfo.Name & vbCrLf)
                    client.Disconnect(ApplicationInfo.Name) 'Error after period of no usage: An unhandled exception of type 'System.ServiceModel.CommunicationObjectFaultedException' occurred in mscorlib.dll   Additional information: The communication object, System.ServiceModel.Channels.ServiceChannel, cannot be used for communication because it is in the Faulted state.
                    btnOnline.Text = "Offline"
                    btnOnline.ForeColor = Color.Black
                    ConnectedToAppnet = False
                    'client.Close()
                Catch ex As Exception
                    Message.SetWarningStyle()
                    Message.Add("Error disconnecting from Application Network: " & ex.Message & vbCrLf)
                End Try
             
            End If
        End If
    End Sub

    ''Private Sub CloseExchangeConnection()
    'Private Sub CloseAppNetConnection()
    '    'Disconnect from the Application Network and close the connection.
    '    'Disconnect from the Message Exchange and close the connection.

    '    Dim Result As Boolean

    '    If IsNothing(client) Then
    '        Message.Add("Already disconnected from the Message Exchange." & vbCrLf)
    '        btnOnline.Text = "Offline"
    '        btnOnline.ForeColor = Color.Black
    '        'ConnectedToExchange = False
    '        ConnectedToAppnet = False
    '    Else
    '        'client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
    '        Message.Add("Running client.Disconnect(ApplicationName)   ApplicationName = " & ApplicationInfo.Name & vbCrLf)

    '        'Errors result when this subroutine is used to process MessageExchangeClosing instruction
    '        'client.Disconnect(ApplicationName) 'Error running XMsg: This operation would deadlock because the reply cannot be received until the current Message completes processing. If you want to allow out-of-order message processing, specify ConcurrencyMode of Reentrant or Multiple on CallbackBehaviorAttribute.
    '        'client.Disconnect(ApplicationName) 'Additional information: This request operation sent to http://localhost:8733/Design_Time_Addresses/WcfMsgServiceLib/Service1/ did not receive a reply within the configured timeout (00:00:00).  The time allotted to this operation may have been a portion of a longer timeout.  This may be because the service is still processing the operation or because the service was unable to send a reply message.  Please consider increasing the operation timeout (by casting the channel/proxy to IContextChannel and setting the OperationTimeout property) and ensure that the service is able to connect to the client.

    '        client.Disconnect(ApplicationInfo.Name)
    '        btnOnline.Text = "Offline"
    '        btnOnline.ForeColor = Color.Black
    '        'ConnectedToExchange = False
    '        ConnectedToAppnet = False
    '        Try
    '            client.Close()
    '        Catch ex As Exception
    '            client.Abort()
    '        End Try

    '    End If
    'End Sub

    Private Sub SendApplicationInfo()
        'Send the application information to the Administrator connections.

        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                'client.SendMessageAsync("CoordinateServer", doc.ToString)

                'Create the XML instructions to send application information.
                Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                Dim applicationInfo As New XElement("ApplicationInfo")
                Dim name As New XElement("Name", Me.ApplicationInfo.Name)
                applicationInfo.Add(name)

                Dim exePath As New XElement("ExecutablePath", Me.ApplicationInfo.ExecutablePath)
                applicationInfo.Add(exePath)

                Dim directory As New XElement("Directory", Me.ApplicationInfo.ApplicationDir)
                applicationInfo.Add(directory)
                Dim description As New XElement("Description", Me.ApplicationInfo.Description)
                applicationInfo.Add(description)
                xmessage.Add(applicationInfo)
                doc.Add(xmessage)
                'client.SendMessageAsync("CoordinateServer", doc.ToString)
                'client.SendAdminMessageAsync(doc.ToString) 'Send the application information to all Admin connections.
                'client.SendMainNodeMessageAsync(doc.ToString) 'Send the application information to the Main Node.
                'client.SendMessage("MessageExchange", doc.ToString)

                Message.Color = Color.Red
                Message.FontStyle = FontStyle.Bold
                Message.XAdd("Application Info Message sent to Application Network" & vbCrLf)
                Message.SetNormalStyle()
                Message.XAdd(doc.ToString & vbCrLf & vbCrLf)

                client.SendMessage("ApplicationNetwork", doc.ToString)
            End If
        End If

    End Sub

    'Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click
    '    'Disconnect from the Message Exchange.


    '    Dim Result As Boolean

    '    If IsNothing(client) Then
    '        client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
    '    End If

    '    If client.State = ServiceModel.CommunicationState.Faulted Then
    '        'rtbMessagesReceived_Old.AppendText("client state is faulted!" & vbCrLf)
    '        Message.Add("client state is faulted!" & vbCrLf)
    '    Else
    '        If client.State = ServiceModel.CommunicationState.Closed Then
    '            'rtbMessagesReceived_Old.AppendText("client state is closed!" & vbCrLf)
    '            Message.Add("client state is closed!" & vbCrLf)
    '        Else
    '            Result = client.Disconnect("CoordinatesClient")

    '            If Result = True Then
    '                'rtbMessagesReceived_Old.AppendText("CoordinatesClient successfully disconnected from the Message Exchange." & vbCrLf)
    '                Message.Add("CoordinatesClient successfully disconnected from the Message Exchange." & vbCrLf)
    '            Else
    '                'rtbMessagesReceived_Old.AppendText("Disconnection of CoordinatesClient from the Message Exchange failed!" & vbCrLf)
    '                Message.Add("Disconnection of CoordinatesClient from the Message Exchange failed!" & vbCrLf)
    '            End If
    '        End If
    '    End If
    'End Sub

#End Region ' Online/Offline code.

    'Convert the angle in input units to the angle in output units.
    Private Sub btnConvert_Click(sender As Object, e As EventArgs) Handles btnConvert.Click
        'Convert Input angle to Output angle

        'Create the XML instructions to convert the angles:
        Dim decl As New XDeclaration("1.0", "utf-8", "yes")
        Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.

        Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
        'Dim clientName As New XElement("ClientName", "CoordinatesClient") 'This tells the coordinate server the name of the client making the request.
        Dim clientName As New XElement("ClientName", ApplicationInfo.Name) 'This tells the coordinate server the name of the client making the request.
        xmessage.Add(clientName)

        Dim operation As New XElement("ConvertAngle")

        If txtInputAngle.Text = "" Then
            If cmbInput.Text = "Sexagesimal Degrees" Then
                txtInputAngle.Text = "0.000000"
            Else
                txtInputAngle.Text = "0"
            End If
        End If
        If txtInputSign.Text = "" Then txtInputSign.Text = "+"
        If txtInputDegrees.Text = "" Then txtInputDegrees.Text = "0"
        If txtInputMinutes.Text = "" Then txtInputMinutes.Text = "0"
        If txtInputSeconds.Text = "" Then txtInputSeconds.Text = "0"

        Select Case cmbInput.Text
            Case "Degrees, Minutes, Seconds" 'Input angle is | Sign | Degrees | Minutes | Seconds |
                If txtInputSign.Text = "+" Then
                    Dim inputDmsSign As New XElement("InputDmsSign", "+")
                    operation.Add(inputDmsSign)
                ElseIf txtInputSign.Text = "-" Then
                    Dim inputDmsSign As New XElement("InputDmsSign", "-")
                    operation.Add(inputDmsSign)
                End If
                Dim inputDmsDegrees As New XElement("InputDmsDegrees", txtInputDegrees.Text)
                operation.Add(inputDmsDegrees)
                Dim inputDmsMinutes As New XElement("InputDmsMinutes", txtInputMinutes.Text)
                operation.Add(inputDmsMinutes)
                Dim inputDmsSeconds As New XElement("InputDmsSeconds", txtInputSeconds.Text)
                operation.Add(inputDmsSeconds)
                Select Case cmbOutput.Text
                    Case "Degrees, Minutes, Seconds" 'Output angle in Dms
                        'When the coordinates server returns the converted angle, the angle components will be named OutputSign, OutputDegrees, OutputMinutes and OutputSeconds.
                        Dim outputDmsSignName As New XElement("OutputDmsSignName", "OutputDmsSign")
                        operation.Add(outputDmsSignName)
                        Dim outputDmsDegreesName As New XElement("OutputDmsDegreesName", "OutputDmsDegrees")
                        operation.Add(outputDmsDegreesName)
                        Dim outputDmsMinutesName As New XElement("OutputDmsMinutesName", "OutputDmsMinutes")
                        operation.Add(outputDmsMinutesName)
                        Dim outputDmsSecondsName As New XElement("OutputDmsSecondsName", "OutputDmsSeconds")
                        operation.Add(outputDmsSecondsName)
                        Dim command As New XElement("Command", "ConvertDmsToDms")
                        operation.Add(command)
                    Case "Decimal Degrees" 'Output angle in decimal degrees
                        'When the coordinates server returns the converted angle, the angle will be named OutputDecimalDegrees.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputDecimalDegrees")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertDmsToDecimalDegrees")
                        operation.Add(command)
                    Case "Sexagesimal Degrees" 'Output angle in sexagesimal degrees
                        'When the coordinates server returns the converted angle, the angle will be named OutputSexagesimalDegrees.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputSexagesimalDegrees")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertDmsToSexagecimalDegrees")
                        operation.Add(command)
                    Case "Radians" 'Output angle in radians
                        Dim outputType As New XElement("Type", "Radians")
                        'When the coordinates server returns the converted angle, the angle will be named OutputRadians.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputRadians")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertDmsToRadians")
                        operation.Add(command)
                    Case "Gradians" 'Output angle in gradians
                        Dim outputType As New XElement("Type", "Gradians")
                        'When the coordinates server returns the converted angle, the angle will be named OutputGradians.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputGradians")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertDmsToGradians")
                        operation.Add(command)
                    Case "Turns" 'Output angle in turns
                        Dim outputType As New XElement("Type", "Turns")
                        'When the coordinates server returns the converted angle, the angle will be named OutputTurns.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputTurns")
                        'outputAngle.Add(outputAngleName)
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertDmsToTurns")
                        operation.Add(command)
                End Select
            Case "Decimal Degrees" 'Input angle is | Decimal Degrees |
                Dim inputAngle As New XElement("InputDecimalDegrees", txtInputAngle.Text)
                operation.Add(inputAngle)
                Select Case cmbOutput.Text
                    Case "Degrees, Minutes, Seconds" 'Output angle is | Sign | Degrees | Minutes | Seconds |
                        'When the coordinates server returns the converted angle, the angle components will be named OutputSign, OutputDegrees, OutputMinutes and OutputSeconds.
                        Dim outputDmsSignName As New XElement("OutputDmsSignName", "OutputDmsSign")
                        operation.Add(outputDmsSignName)
                        Dim outputDmsDegreesName As New XElement("OutputDmsDegreesName", "OutputDmsDegrees")
                        operation.Add(outputDmsDegreesName)
                        Dim outputDmsMinutesName As New XElement("OutputDmsMinutesName", "OutputDmsMinutes")
                        operation.Add(outputDmsMinutesName)
                        Dim outputDmsSecondsName As New XElement("OutputDmsSecondsName", "OutputDmsSeconds")
                        operation.Add(outputDmsSecondsName)
                        Dim command As New XElement("Command", "ConvertDecimalDegreesToDms")
                        operation.Add(command)
                    Case "Decimal Degrees" 'Output angle in decimal degrees
                        'When the coordinates server returns the converted angle, the angle will be named OutputDecimalDegrees.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputDecimalDegrees")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertDecimalDegreesToDecimalDegrees")
                        operation.Add(command)
                    Case "Sexagesimal Degrees" 'Output angle in sexagesimal degrees
                        'When the coordinates server returns the converted angle, the angle will be named OutputSexagesimalDegrees.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputSexagesimalDegrees")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertDecimalDegreesToSexagesimalDegrees")
                        operation.Add(command)
                    Case "Radians"  'Output angle in radians
                        'When the coordinates server returns the converted angle, the angle will be named OutputRadians.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputRadians")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertDecimalDegreesToRadians")
                        operation.Add(command)
                    Case "Gradians"  'Output angle in gradians
                        'When the coordinates server returns the converted angle, the angle will be named OutputGradians.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputGradians")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertDecimalDegreesToGradians")
                        operation.Add(command)
                    Case "Turns" 'Output angle in turns
                        'When the coordinates server returns the converted angle, the angle will be named OutputTurns.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputTurns")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertDecimalDegreesToTurns")
                        operation.Add(command)
                End Select
            Case "Sexagesimal Degrees" 'Input angle is | Sexagesimal Degrees |
                Dim inputAngle As New XElement("InputSexagesimalDegrees", txtInputAngle.Text)
                operation.Add(inputAngle)
                Select Case cmbOutput.Text
                    Case "Degrees, Minutes, Seconds" 'Output angle in DMS
                        Dim outputDmsSignName As New XElement("OutputDmsSignName", "OutputDmsSign")
                        operation.Add(outputDmsSignName)
                        Dim outputDmsDegreesName As New XElement("OutputDmsDegreesName", "OutputDmsDegrees")
                        operation.Add(outputDmsDegreesName)
                        Dim outputDmsMinutesName As New XElement("OutputDmsMinutesName", "OutputDmsMinutes")
                        operation.Add(outputDmsMinutesName)
                        Dim outputDmsSecondsName As New XElement("OutputDmsSecondsName", "OutputDmsSeconds")
                        operation.Add(outputDmsSecondsName)
                        Dim command As New XElement("Command", "ConvertSexagesimalDegreesToDms")
                        operation.Add(command)
                    Case "Decimal Degrees" 'Output angle in decimal degrees
                        'When the coordinates server returns the converted angle, the angle will be named OutputDecimalDegrees.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputDecimalDegrees")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertSexagesimalDegreesToDecimalDegrees")
                        operation.Add(command)
                    Case "Sexagesimal Degrees" 'Output angle in sexagesimal degrees
                        'When the coordinates server returns the converted angle, the angle will be named OutputSexagesimalDegrees.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputSexagesimalDegrees")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertSexagesimalDegreesToSexagesimalDegrees")
                        operation.Add(command)
                    Case "Radians" 'Output angle in radians
                        'When the coordinates server returns the converted angle, the angle will be named OutputRadians.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputRadians")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertSexagesimalDegreesToRadians")
                        operation.Add(command)
                    Case "Gradians" 'Output angle in radians
                        'When the coordinates server returns the converted angle, the angle will be named OutputGradians.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputGradians")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertSexagesimalDegreesToGradians")
                        operation.Add(command)
                    Case "Turns" 'Output angle in turns
                        'When the coordinates server returns the converted angle, the angle will be named OutputTurns.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputTurns")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertSexagesimalDegreesToTurns")
                        operation.Add(command)
                End Select
            Case "Radians" 'Input angle is | Radians |
                Dim inputAngle As New XElement("InputRadians", txtInputAngle.Text)
                operation.Add(inputAngle)
                Select Case cmbOutput.Text
                    Case "Degrees, Minutes, Seconds"  'Output angle in DMS
                        'When the coordinates server returns the converted angle, the angle components will be named OutputSign, OutputDegrees, OutputMinutes and OutputSeconds.
                        Dim outputDmsSignName As New XElement("OutputDmsSignName", "OutputDmsSign")
                        operation.Add(outputDmsSignName)
                        Dim outputDmsDegreesName As New XElement("OutputDmsDegreesName", "OutputDmsDegrees")
                        operation.Add(outputDmsDegreesName)
                        Dim outputDmsMinutesName As New XElement("OutputDmsMinutesName", "OutputDmsMinutes")
                        operation.Add(outputDmsMinutesName)
                        Dim outputDmsSecondsName As New XElement("OutputDmsSecondsName", "OutputDmsSeconds")
                        operation.Add(outputDmsSecondsName)
                        Dim command As New XElement("Command", "ConvertRadiansToDms")
                        operation.Add(command)
                    Case "Decimal Degrees" 'Output angle in decimal degrees
                        'When the coordinates server returns the converted angle, the angle will be named OutputDecimalDegrees.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputDecimalDegrees")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertRadiansToDecimalDegrees")
                        operation.Add(command)
                    Case "Sexagesimal Degrees" 'Output angle in sexagesimal degrees
                        'When the coordinates server returns the converted angle, the angle will be named OutputSexagesimalDegrees.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputSexagesimalDegrees")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertRadiansToSexagesimalDegrees")
                        operation.Add(command)
                    Case "Radians" 'Output angle in radians
                        'When the coordinates server returns the converted angle, the angle will be named OutputRadians.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputRadians")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertRadiansToRadians")
                        operation.Add(command)
                    Case "Gradians"  'Output angle in gradians
                        Dim outputType As New XElement("Type", "Gradians")
                        'When the coordinates server returns the converted angle, the angle will be named OutputGradians.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputGradians")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertRadiansToGradians")
                        operation.Add(command)
                    Case "Turns"  'Output angle in turns
                        'When the coordinates server returns the converted angle, the angle will be named OutputTurns.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputTurns")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertRadiansToTurns")
                        operation.Add(command)
                End Select
            Case "Gradians" 'Input angle is | Gradians |
                Dim inputAngle As New XElement("InputGradians", txtInputAngle.Text)
                operation.Add(inputAngle)
                Select Case cmbOutput.Text
                    Case "Degrees, Minutes, Seconds"  'Output angle in DMS
                        ''When the coordinates server returns the converted angle, the angle components will be named OutputSign, OutputDegrees, OutputMinutes and OutputSeconds.
                        Dim outputDmsSignName As New XElement("OutputDmsSignName", "OutputDmsSign")
                        operation.Add(outputDmsSignName)
                        Dim outputDmsDegreesName As New XElement("OutputDmsDegreesName", "OutputDmsDegrees")
                        operation.Add(outputDmsDegreesName)
                        Dim outputDmsMinutesName As New XElement("OutputDmsMinutesName", "OutputDmsMinutes")
                        operation.Add(outputDmsMinutesName)
                        Dim outputDmsSecondsName As New XElement("OutputDmsSecondsName", "OutputDmsSeconds")
                        operation.Add(outputDmsSecondsName)
                        Dim command As New XElement("Command", "ConvertGradiansToDms")
                        operation.Add(command)
                    Case "Decimal Degrees" 'Output angle in decimal degrees
                        'When the coordinates server returns the converted angle, the angle will be named OutputDecimalDegrees.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputDecimalDegrees")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertGradiansToDecimalDegrees")
                        operation.Add(command)
                    Case "Sexagesimal Degrees"  'Output angle in sexagesimal degrees
                        'When the coordinates server returns the converted angle, the angle will be named OutputSexagesimalDegrees.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputSexagesimalDegrees")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertGradiansToSexagesimalDegrees")
                        operation.Add(command)
                    Case "Radians"  'Output angle in radians
                        'When the coordinates server returns the converted angle, the angle will be named OutputRadians.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputRadians")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertGradiansToRadians")
                        operation.Add(command)
                    Case "Gradians"  'Output angle in gradians
                        'When the coordinates server returns the converted angle, the angle will be named OutputGradians.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputGradians")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertGradiansToGradians")
                        operation.Add(command)
                    Case "Turns"  'Output angle in turns
                        'When the coordinates server returns the converted angle, the angle will be named OutputTurns.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputTurns")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertGradiansToTurns")
                        operation.Add(command)
                End Select
                'operation.Add(outputAngle)
            Case "Turns" 'Input angle is | Turns |
                Dim inputAngle As New XElement("InputTurns", txtInputAngle.Text)
                operation.Add(inputAngle)
                Select Case cmbOutput.Text
                    Case "Degrees, Minutes, Seconds" 'Output angle in DMS
                        ''When the coordinates server returns the converted angle, the angle components will be named OutputSign, OutputDegrees, OutputMinutes and OutputSeconds.
                        Dim outputDmsSignName As New XElement("OutputDmsSignName", "OutputDmsSign")
                        operation.Add(outputDmsSignName)
                        Dim outputDmsDegreesName As New XElement("OutputDmsDegreesName", "OutputDmsDegrees")
                        operation.Add(outputDmsDegreesName)
                        Dim outputDmsMinutesName As New XElement("OutputDmsMinutesName", "OutputDmsMinutes")
                        operation.Add(outputDmsMinutesName)
                        Dim outputDmsSecondsName As New XElement("OutputDmsSecondsName", "OutputDmsSeconds")
                        operation.Add(outputDmsSecondsName)
                        Dim command As New XElement("Command", "ConvertTurnsToDms")
                        operation.Add(command)
                    Case "Decimal Degrees" 'Output angle in decimal degrees
                        'When the coordinates server returns the converted angle, the angle will be named OutputDecimalDegrees.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputDecimalDegrees")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertTurnsToDecimalDegrees")
                        operation.Add(command)
                    Case "Sexagesimal Degrees" 'Output angle in sexagesimal degrees
                        'When the coordinates server returns the converted angle, the angle will be named OutputSexagesimalDegrees.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputSexagesimalDegrees")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertTurnsToDecimalDegrees")
                        operation.Add(command)
                    Case "Radians" 'Output angle in radians
                        'When the coordinates server returns the converted angle, the angle will be named OutputRadians.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputRadians")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertTurnsToRadians")
                        operation.Add(command)
                    Case "Gradians" 'Output angle in gradians
                        'When the coordinates server returns the converted angle, the angle will be named OutputGradians.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputGradians")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertTurnsToGradians")
                        operation.Add(command)
                    Case "Turns"  'Output angle in turns
                        'When the coordinates server returns the converted angle, the angle will be named OutputTurns.
                        Dim outputAngleName As New XElement("OutputAngleName", "OutputTurns")
                        operation.Add(outputAngleName)
                        Dim command As New XElement("Command", "ConvertTurnsToTurns")
                        operation.Add(command)
                End Select
        End Select

        xmessage.Add(operation)

        doc.Add(xmessage)

        'Send the message.
        'The message text to send is in the rich text box is rtbMessageToSend.
        'The name of the destination application is in the txtDestination text box.

        'To send a message to the Message Exchange application, use the destination "MessageExchange"

        If IsNothing(client) Then
            'rtbMessagesReceived.AppendText("No client connection available!" & vbCrLf)
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                'rtbMessagesReceived.AppendText("client state is faulted. Messge not sent!" & vbCrLf)
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
                'Dim StateFlag As System.Enum
                'Dim StateHasFlag As Boolean
                'StateHasFlag = client.State.HasFlag(StateFlag)
            Else

                'client.SendMessageAsync("CoordinatesServer", doc.ToString)
                'client.SendMessageAsync("ADVL_Coordinates", doc.ToString)
                client.SendMessageAsync("ADVL_Coordinates_1", doc.ToString)

                ''Check is the Messages form is open:
                'If IsNothing(Messages) Then
                '    Messages = New frmMessages
                '    Messages.Show()
                '    Messages.rtbInstructionsSent.Text = doc.ToString & vbCrLf
                'Else
                '    Messages.Show()
                '    Messages.rtbInstructionsSent.Text = doc.ToString & vbCrLf
                'End If
                Message.Color = Color.Red
                Message.FontStyle = FontStyle.Bold
                'Message.XAdd("Message sent to " & "ADVL_Coordinates" & ":" & vbCrLf)
                Message.XAdd("Message sent to " & "ADVL_Coordinates_1" & ":" & vbCrLf)
                Message.SetNormalStyle()
                Message.XAdd(doc.ToString & vbCrLf & vbCrLf)

            End If
        End If
    End Sub

    'Update the projected coordinate reference system list.
    Private Sub btnUpdateList_Click(sender As Object, e As EventArgs) Handles btnUpdateList.Click
        'Get an updated list of projected coordinate refernce systems.

        'Create the XMsg instructions to get the list.
        Dim decl As New XDeclaration("1.0", "utf-8", "yes")
        Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.

        Dim xmessage As New XElement("XMsg")
        'Dim clientName As New XElement("ClientName", "CoordinatesClient") 'This tells the coordinate server the name of the client making the request.
        Dim clientName As New XElement("ClientName", ApplicationInfo.Name) 'This tells the coordinate server the name of the client making the request.
        xmessage.Add(clientName)
        Dim operation As New XElement("Command", "GetProjectedCrsList") 'This tells the coordinate server to reply with the list of projected coordinate reference systems
        xmessage.Add(operation)

        doc.Add(xmessage)

        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Messge not sent!" & vbCrLf)
            Else
                cmbProjectedCRS.Items.Clear()
                'client.SendMessageAsync("CoordinateServer", doc.ToString)
                'client.SendMessageAsync("ADVL_Coordinates", doc.ToString)
                client.SendMessageAsync("ADVL_Coordinates_1", doc.ToString)

                Message.Color = Color.Red
                Message.FontStyle = FontStyle.Bold
                'Message.XAdd("Message sent to " & "ADVL_Coordinates" & ":" & vbCrLf)
                Message.XAdd("Message sent to " & "ADVL_Coordinates_1" & ":" & vbCrLf)
                Message.SetNormalStyle()
                Message.XAdd(doc.ToString & vbCrLf & vbCrLf)

            End If
        End If
    End Sub

    Private Sub cmbInput_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbInput.SelectedIndexChanged
        Select Case cmbInput.Text
            Case "Degrees, Minutes, Seconds"
                txtInputAngle.Hide()
                txtInputSign.Show()
                txtInputDegrees.Show()
                txtInputMinutes.Show()
                txtInputSeconds.Show()
                txtInputSign.Location = New Point(6, 64)
                txtInputSign.Width = 16
                txtInputDegrees.Location = New Point(27, 64)
                txtInputDegrees.Width = 38
                txtInputMinutes.Location = New Point(71, 64)
                txtInputMinutes.Width = 38
                txtInputSeconds.Location = New Point(115, 64)
                txtInputSeconds.Width = 120

                Label6.Show() 'Deg label
                Label7.Show() 'Min label
                Label9.Show() 'Sec label

            Case "Decimal Degrees"
                txtInputAngle.Show()
                txtInputSign.Hide()
                txtInputDegrees.Hide()
                txtInputMinutes.Hide()
                txtInputSeconds.Hide()
                txtInputAngle.Location = New Point(7, 64)
                txtInputAngle.Width = 120

                Label6.Hide() 'Deg label
                Label7.Hide() 'Min label
                Label9.Hide() 'Sec label

            Case "Sexagesimal Degrees"
                txtInputAngle.Show()
                txtInputSign.Hide()
                txtInputDegrees.Hide()
                txtInputMinutes.Hide()
                txtInputSeconds.Hide()
                txtInputAngle.Location = New Point(7, 64)
                txtInputAngle.Width = 120

                Label6.Hide() 'Deg label
                Label7.Hide() 'Min label
                Label9.Hide() 'Sec label

            Case "Radians"
                txtInputAngle.Show()
                txtInputSign.Hide()
                txtInputDegrees.Hide()
                txtInputMinutes.Hide()
                txtInputSeconds.Hide()
                txtInputDegrees.Location = New Point(7, 64)
                txtInputDegrees.Width = 120

                Label6.Hide() 'Deg label
                Label7.Hide() 'Min label
                Label9.Hide() 'Sec label

            Case "Gradians"
                txtInputAngle.Show()
                txtInputSign.Hide()
                txtInputDegrees.Hide()
                txtInputMinutes.Hide()
                txtInputSeconds.Hide()
                txtInputAngle.Location = New Point(7, 64)
                txtInputAngle.Width = 120

                Label6.Hide() 'Deg label
                Label7.Hide() 'Min label
                Label9.Hide() 'Sec label

            Case "Turns"
                txtInputAngle.Show()
                txtInputSign.Hide()
                txtInputDegrees.Hide()
                txtInputMinutes.Hide()
                txtInputSeconds.Hide()
                txtInputAngle.Location = New Point(7, 64)
                txtInputAngle.Width = 120

                Label6.Hide() 'Deg label
                Label7.Hide() 'Min label
                Label9.Hide() 'Sec label

        End Select
    End Sub

    Private Sub cmbOutput_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOutput.SelectedIndexChanged
        Select Case cmbOutput.Text
            Case "Degrees, Minutes, Seconds"
                txtOutputAngle.Hide()
                txtOutputSign.Show()
                txtOutputSign.Text = ""
                txtOutputDegrees.Show()
                txtOutputDegrees.Text = ""
                txtOutputMinutes.Show()
                txtOutputMinutes.Text = ""
                txtOutputSeconds.Show()
                txtOutputSeconds.Text = ""
                txtOutputSign.Location = New Point(263, 64)
                txtOutputSign.Width = 16
                txtOutputDegrees.Location = New Point(285, 64)
                txtOutputDegrees.Width = 38
                txtOutputMinutes.Location = New Point(329, 64)
                txtOutputMinutes.Width = 38
                txtOutputSeconds.Location = New Point(373, 64)
                txtOutputSeconds.Width = 120

                Label5.Show() 'Deg label
                Label4.Show() 'Min label
                Label3.Show() 'Sec label

            Case "Decimal Degrees"
                txtOutputAngle.Show()
                txtOutputAngle.Text = ""
                txtOutputSign.Hide()
                txtOutputDegrees.Hide()
                txtOutputMinutes.Hide()
                txtOutputSeconds.Hide()
                txtOutputAngle.Location = New Point(263, 64)
                txtOutputAngle.Width = 120

                Label5.Hide() 'Deg label
                Label4.Hide() 'Min label
                Label3.Hide() 'Sec label

            Case "Sexagesimal Degrees"
                txtOutputAngle.Show()
                txtOutputAngle.Text = ""
                txtOutputSign.Hide()
                txtOutputDegrees.Hide()
                txtOutputMinutes.Hide()
                txtOutputSeconds.Hide()
                txtOutputAngle.Location = New Point(263, 64)
                txtOutputAngle.Width = 120

                Label5.Hide() 'Deg label
                Label4.Hide() 'Min label
                Label3.Hide() 'Sec label

            Case "Radians"
                txtOutputAngle.Show()
                txtOutputAngle.Text = ""
                txtOutputSign.Hide()
                txtOutputDegrees.Hide()
                txtOutputMinutes.Hide()
                txtOutputSeconds.Hide()
                txtOutputDegrees.Location = New Point(263, 64)
                txtOutputDegrees.Width = 120

                Label5.Hide() 'Deg label
                Label4.Hide() 'Min label
                Label3.Hide() 'Sec label

            Case "Gradians"
                txtOutputAngle.Show()
                txtOutputAngle.Text = ""
                txtOutputSign.Hide()
                txtOutputDegrees.Hide()
                txtOutputMinutes.Hide()
                txtOutputSeconds.Hide()
                txtOutputAngle.Location = New Point(263, 64)
                txtOutputAngle.Width = 120

                Label5.Hide() 'Deg label
                Label4.Hide() 'Min label
                Label3.Hide() 'Sec label

            Case "Turns"
                txtOutputAngle.Show()
                txtOutputAngle.Text = ""
                txtOutputSign.Hide()
                txtOutputDegrees.Hide()
                txtOutputMinutes.Hide()
                txtOutputSeconds.Hide()
                txtOutputAngle.Location = New Point(263, 64)
                txtOutputAngle.Width = 120

                Label5.Hide() 'Deg label
                Label4.Hide() 'Min label
                Label3.Hide() 'Sec label

        End Select
    End Sub

    Private Sub txtInputDegrees_LostFocus(sender As Object, e As EventArgs) Handles txtInputDegrees.LostFocus
        txtInputDegrees.Text = Str(Val(txtInputDegrees.Text))
        If Val(txtInputDegrees.Text) < 0 Then
            'Check that Minutes and Seconds are also negative
            If Val(txtInputMinutes.Text) > 0 Then
                txtInputMinutes.Text = Str(Val(txtInputMinutes.Text) * -1)
            End If
            If Val(txtInputSeconds.Text) > 0 Then
                txtInputSeconds.Text = Str(Val(txtInputSeconds.Text) * -1)
            End If
        ElseIf Val(txtInputDegrees.Text) > 0 Then
            'Check that Minutes and Seconds are also positive
            If Val(txtInputMinutes.Text) < 0 Then
                txtInputMinutes.Text = Str(Val(txtInputMinutes.Text) * -1)
            End If
            If Val(txtInputSeconds.Text) < 0 Then
                txtInputSeconds.Text = Str(Val(txtInputSeconds.Text) * -1)
            End If
        End If
    End Sub

    Private Sub txtInputMinutes_LostFocus(sender As Object, e As EventArgs) Handles txtInputMinutes.LostFocus
        txtInputMinutes.Text = Str(Val(txtInputMinutes.Text))
        If Val(txtInputMinutes.Text) < 0 Then
            'Check that Degrees and Seconds are also negative
            If Val(txtInputDegrees.Text) > 0 Then
                txtInputDegrees.Text = Str(Val(txtInputDegrees.Text) * -1)
            End If
            If Val(txtInputSeconds.Text) > 0 Then
                txtInputSeconds.Text = Str(Val(txtInputSeconds.Text) * -1)
            End If
        ElseIf Val(txtInputMinutes.Text) > 0 Then
            'Check that Degrees and Seconds are also positive
            If Val(txtInputDegrees.Text) < 0 Then
                txtInputDegrees.Text = Str(Val(txtInputDegrees.Text) * -1)
            End If
            If Val(txtInputSeconds.Text) < 0 Then
                txtInputSeconds.Text = Str(Val(txtInputSeconds.Text) * -1)
            End If
        End If
    End Sub

    Private Sub txtInputSeconds_LostFocus(sender As Object, e As EventArgs) Handles txtInputSeconds.LostFocus
        txtInputSeconds.Text = Str(Val(txtInputSeconds.Text))
        If Val(txtInputSeconds.Text) < 0 Then
            'Check that Degrees and Minutes are also negative
            If Val(txtInputDegrees.Text) > 0 Then
                txtInputDegrees.Text = Str(Val(txtInputDegrees.Text) * -1)
            End If
            If Val(txtInputMinutes.Text) > 0 Then
                txtInputMinutes.Text = Str(Val(txtInputMinutes.Text) * -1)
            End If
        ElseIf Val(txtInputSeconds.Text) > 0 Then
            'Check that Degrees and Minutes are also positive
            If Val(txtInputDegrees.Text) < 0 Then
                txtInputDegrees.Text = Str(Val(txtInputDegrees.Text) * -1)
            End If
            If Val(txtInputMinutes.Text) < 0 Then
                txtInputMinutes.Text = Str(Val(txtInputMinutes.Text) * -1)
            End If
        End If
    End Sub

    Private Sub txtInputAngle_LostFocus(sender As Object, e As EventArgs) Handles txtInputAngle.LostFocus
        If cmbInput.Text = "Sexagesimal Degrees" Then
            txtInputAngle.Text = Format(Val(txtInputAngle.Text), "##0.0000##############")
        Else
            txtInputAngle.Text = Str(Val(txtInputAngle.Text))
        End If
    End Sub

    Private Sub txtOutputDegrees_LostFocus(sender As Object, e As EventArgs) Handles txtOutputDegrees.LostFocus
        txtOutputDegrees.Text = Str(Val(txtOutputDegrees.Text))
    End Sub

    Private Sub txtOutputMinutes_LostFocus(sender As Object, e As EventArgs) Handles txtOutputMinutes.LostFocus
        txtOutputMinutes.Text = Str(Val(txtOutputMinutes.Text))
    End Sub

    Private Sub txtOutputSeconds_LostFocus(sender As Object, e As EventArgs) Handles txtOutputSeconds.LostFocus
        txtOutputSeconds.Text = Str(Val(txtOutputSeconds.Text))
    End Sub

    Private Sub txtOutputAngle_LostFocus(sender As Object, e As EventArgs) Handles txtOutputAngle.LostFocus
        txtOutputAngle.Text = Str(Val(txtOutputAngle.Text))
    End Sub

    Private Sub cmbProjnInput_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProjnInput.SelectedIndexChanged
        Select Case cmbProjnInput.Text
            Case "Latitude, Longitude"
                SetProjnInputAngleUnits()
                lblInput1.Text = "Latitude"
                lblInput2.Text = "Longitude"
            Case "Longitude, Latitude"
                SetProjnInputAngleUnits()
                lblInput1.Text = "Longitude"
                lblInput2.Text = "Latitude"
            Case "Easting, Northing"
                SetProjnInputDistanceUnits()
                lblInput1.Text = "Easting"
                lblInput2.Text = "Northing"
            Case "Northing, Easting"
                SetProjnInputDistanceUnits()
                lblInput1.Text = "Northing"
                lblInput2.Text = "Easting"
        End Select
    End Sub

    Private Sub SetProjnInputAngleUnits()
        'Set the Projection Input Units to angle units:
        cmbProjnInputUnits.Items.Clear()
        cmbProjnInputUnits.Items.Add("Degrees, Minutes, Seconds")
        cmbProjnInputUnits.Items.Add("Decimal Degrees")
        cmbProjnInputUnits.Items.Add("Sexagesimal Degrees")
        cmbProjnInputUnits.Items.Add("Radians")
        cmbProjnInputUnits.Items.Add("Gradians")
        cmbProjnInputUnits.Items.Add("Turns")
        cmbProjnInputUnits.SelectedIndex = 0
    End Sub

    Private Sub SetProjnInputDistanceUnits()
        'Set the Projection Input Units to distance units:
        cmbProjnInputUnits.Items.Clear()
        cmbProjnInputUnits.Items.Add("Metres")
        cmbProjnInputUnits.Items.Add("Feet")
        cmbProjnInputUnits.Items.Add("Default")
        cmbProjnInputUnits.SelectedIndex = 0
    End Sub

    Private Sub cmbProjnOutput_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProjnOutput.SelectedIndexChanged
        Select Case cmbProjnOutput.Text
            Case "Latitude, Longitude"
                SetProjnOutputAngleUnits()
                lblOutput1.Text = "Latitude"
                lblOutput2.Text = "Longitude"
            Case "Longitude, Latitude"
                SetProjnOutputAngleUnits()
                lblOutput1.Text = "Longitude"
                lblOutput2.Text = "Latitude"
            Case "Easting, Northing"
                SetProjnOutputDistanceUnits()
                lblOutput1.Text = "Easting"
                lblOutput2.Text = "Northing"
            Case "Northing, Easting"
                SetProjnOutputDistanceUnits()
                lblOutput1.Text = "Northing"
                lblOutput2.Text = "Easting"
        End Select
    End Sub

    Private Sub SetProjnOutputAngleUnits()
        'Set the Projection Input Units to angle units:
        cmbProjnOutputUnits.Items.Clear()
        cmbProjnOutputUnits.Items.Add("Degrees, Minutes, Seconds")
        cmbProjnOutputUnits.Items.Add("Decimal Degrees")
        cmbProjnOutputUnits.Items.Add("Sexagesimal Degrees")
        cmbProjnOutputUnits.Items.Add("Radians")
        cmbProjnOutputUnits.Items.Add("Gradians")
        cmbProjnOutputUnits.Items.Add("Turns")
        cmbProjnOutputUnits.SelectedIndex = 0
    End Sub

    Private Sub SetProjnOutputDistanceUnits()
        'Set the Projection Input Units to distance units:
        cmbProjnOutputUnits.Items.Clear()
        cmbProjnOutputUnits.Items.Add("Metres")
        cmbProjnOutputUnits.Items.Add("Feet")
        cmbProjnOutputUnits.Items.Add("Default")
        cmbProjnOutputUnits.SelectedIndex = 0
    End Sub

    Private Sub cmbProjnInputUnits_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProjnInputUnits.SelectedIndexChanged
        Select Case cmbProjnInputUnits.Text
            Case "Degrees, Minutes, Seconds"
                lblInDeg1.Visible = True
                lblInDeg2.Visible = True
                lblInMin1.Visible = True
                lblInMin2.Visible = True
                lblInSec1.Visible = True
                lblInSec2.Visible = True
                txtInputSign1.Visible = True
                txtInputSign2.Visible = True
                txtInputDegrees1.Visible = True
                txtInputDegrees2.Visible = True
                txtInputMinutes1.Visible = True
                txtInputMinutes2.Visible = True
                txtInputSeconds1.Visible = True
                txtInputSeconds2.Visible = True
                lblInUnits1.Visible = False
                lblInUnits2.Visible = False
                txtInput1.Visible = False
                txtInput2.Visible = False
            Case "Decimal Degrees"
                lblInDeg1.Visible = False
                lblInDeg2.Visible = False
                lblInMin1.Visible = False
                lblInMin2.Visible = False
                lblInSec1.Visible = False
                lblInSec2.Visible = False
                txtInputSign1.Visible = False
                txtInputSign2.Visible = False
                txtInputDegrees1.Visible = False
                txtInputDegrees2.Visible = False
                txtInputMinutes1.Visible = False
                txtInputMinutes2.Visible = False
                txtInputSeconds1.Visible = False
                txtInputSeconds2.Visible = False
                lblInUnits1.Visible = True
                lblInUnits1.Text = "Decimal Degrees"
                lblInUnits2.Visible = True
                lblInUnits2.Text = "Decimal Degrees"
                txtInput1.Visible = True
                txtInput2.Visible = True
            Case "Sexagesimal Degrees"
                lblInDeg1.Visible = False
                lblInDeg2.Visible = False
                lblInMin1.Visible = False
                lblInMin2.Visible = False
                lblInSec1.Visible = False
                lblInSec2.Visible = False
                txtInputSign1.Visible = False
                txtInputSign2.Visible = False
                txtInputDegrees1.Visible = False
                txtInputDegrees2.Visible = False
                txtInputMinutes1.Visible = False
                txtInputMinutes2.Visible = False
                txtInputSeconds1.Visible = False
                txtInputSeconds2.Visible = False
                lblInUnits1.Visible = True
                lblInUnits1.Text = "Sexagesimal Degrees"
                lblInUnits2.Visible = True
                lblInUnits2.Text = "Sexagesimal Degrees"
                txtInput1.Visible = True
                txtInput2.Visible = True
            Case "Radians"
                lblInDeg1.Visible = False
                lblInDeg2.Visible = False
                lblInMin1.Visible = False
                lblInMin2.Visible = False
                lblInSec1.Visible = False
                lblInSec2.Visible = False
                txtInputSign1.Visible = False
                txtInputSign2.Visible = False
                txtInputDegrees1.Visible = False
                txtInputDegrees2.Visible = False
                txtInputMinutes1.Visible = False
                txtInputMinutes2.Visible = False
                txtInputSeconds1.Visible = False
                txtInputSeconds2.Visible = False
                lblInUnits1.Visible = True
                lblInUnits1.Text = "Radians"
                lblInUnits2.Visible = True
                lblInUnits2.Text = "Radians"
                txtInput1.Visible = True
                txtInput2.Visible = True
            Case "Gradians"
                lblInDeg1.Visible = False
                lblInDeg2.Visible = False
                lblInMin1.Visible = False
                lblInMin2.Visible = False
                lblInSec1.Visible = False
                lblInSec2.Visible = False
                txtInputSign1.Visible = False
                txtInputSign2.Visible = False
                txtInputDegrees1.Visible = False
                txtInputDegrees2.Visible = False
                txtInputMinutes1.Visible = False
                txtInputMinutes2.Visible = False
                txtInputSeconds1.Visible = False
                txtInputSeconds2.Visible = False
                lblInUnits1.Visible = True
                lblInUnits1.Text = "Gradians"
                lblInUnits2.Visible = True
                lblInUnits2.Text = "Gradians"
                txtInput1.Visible = True
                txtInput2.Visible = True
            Case "Turns"
                lblInDeg1.Visible = False
                lblInDeg2.Visible = False
                lblInMin1.Visible = False
                lblInMin2.Visible = False
                lblInSec1.Visible = False
                lblInSec2.Visible = False
                txtInputSign1.Visible = False
                txtInputSign2.Visible = False
                txtInputDegrees1.Visible = False
                txtInputDegrees2.Visible = False
                txtInputMinutes1.Visible = False
                txtInputMinutes2.Visible = False
                txtInputSeconds1.Visible = False
                txtInputSeconds2.Visible = False
                lblInUnits1.Visible = True
                lblInUnits1.Text = "Turns"
                lblInUnits2.Visible = True
                lblInUnits2.Text = "Turns"
                txtInput1.Visible = True
                txtInput2.Visible = True
            Case "Metres"
                lblInDeg1.Visible = False
                lblInDeg2.Visible = False
                lblInMin1.Visible = False
                lblInMin2.Visible = False
                lblInSec1.Visible = False
                lblInSec2.Visible = False
                txtInputSign1.Visible = False
                txtInputSign2.Visible = False
                txtInputDegrees1.Visible = False
                txtInputDegrees2.Visible = False
                txtInputMinutes1.Visible = False
                txtInputMinutes2.Visible = False
                txtInputSeconds1.Visible = False
                txtInputSeconds2.Visible = False
                lblInUnits1.Visible = True
                lblInUnits1.Text = "Metres"
                lblInUnits2.Visible = True
                lblInUnits2.Text = "Metres"
                txtInput1.Visible = True
                txtInput2.Visible = True
            Case "Feet"
                lblInDeg1.Visible = False
                lblInDeg2.Visible = False
                lblInMin1.Visible = False
                lblInMin2.Visible = False
                lblInSec1.Visible = False
                lblInSec2.Visible = False
                txtInputSign1.Visible = False
                txtInputSign2.Visible = False
                txtInputDegrees1.Visible = False
                txtInputDegrees2.Visible = False
                txtInputMinutes1.Visible = False
                txtInputMinutes2.Visible = False
                txtInputSeconds1.Visible = False
                txtInputSeconds2.Visible = False
                lblInUnits1.Visible = True
                lblInUnits1.Text = "Feet"
                lblInUnits2.Visible = True
                lblInUnits2.Text = "Feet"
                txtInput1.Visible = True
                txtInput2.Visible = True
            Case "Default"
                lblInDeg1.Visible = False
                lblInDeg2.Visible = False
                lblInMin1.Visible = False
                lblInMin2.Visible = False
                lblInSec1.Visible = False
                lblInSec2.Visible = False
                txtInputSign1.Visible = False
                txtInputSign2.Visible = False
                txtInputDegrees1.Visible = False
                txtInputDegrees2.Visible = False
                txtInputMinutes1.Visible = False
                txtInputMinutes2.Visible = False
                txtInputSeconds1.Visible = False
                txtInputSeconds2.Visible = False
                lblInUnits1.Visible = True
                lblInUnits1.Text = "Default"
                lblInUnits2.Visible = True
                lblInUnits2.Text = "Default"
                txtInput1.Visible = True
                txtInput2.Visible = True
        End Select
    End Sub

    Private Sub cmbProjnOutputUnits_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProjnOutputUnits.SelectedIndexChanged
        Select Case cmbProjnOutputUnits.Text
            Case "Degrees, Minutes, Seconds"
                lblOutDeg1.Visible = True
                lblOutDeg2.Visible = True
                lblOutMin1.Visible = True
                lblOutMin2.Visible = True
                lblOutSec1.Visible = True
                lblOutSec2.Visible = True
                txtOutputSign1.Visible = True
                txtOutputSign2.Visible = True
                txtOutputDegrees1.Visible = True
                txtOutputDegrees2.Visible = True
                txtOutputMinutes1.Visible = True
                txtOutputMinutes2.Visible = True
                txtOutputSeconds1.Visible = True
                txtOutputSeconds2.Visible = True
                lblOutUnits1.Visible = False
                lblOutUnits2.Visible = False
                txtOutput1.Visible = False
                txtOutput2.Visible = False
            Case "Decimal Degrees"
                lblOutDeg1.Visible = False
                lblOutDeg2.Visible = False
                lblOutMin1.Visible = False
                lblOutMin2.Visible = False
                lblOutSec1.Visible = False
                lblOutSec2.Visible = False
                txtOutputSign1.Visible = False
                txtOutputSign2.Visible = False
                txtOutputDegrees1.Visible = False
                txtOutputDegrees2.Visible = False
                txtOutputMinutes1.Visible = False
                txtOutputMinutes2.Visible = False
                txtOutputSeconds1.Visible = False
                txtOutputSeconds2.Visible = False
                lblOutUnits1.Visible = True
                lblOutUnits1.Text = "Decimal Degrees"
                lblOutUnits2.Visible = True
                lblOutUnits2.Text = "Decimal Degrees"
                txtOutput1.Visible = True
                txtOutput2.Visible = True
            Case "Sexagesimal Degrees"
                lblOutDeg1.Visible = False
                lblOutDeg2.Visible = False
                lblOutMin1.Visible = False
                lblOutMin2.Visible = False
                lblOutSec1.Visible = False
                lblOutSec2.Visible = False
                txtOutputSign1.Visible = False
                txtOutputSign2.Visible = False
                txtOutputDegrees1.Visible = False
                txtOutputDegrees2.Visible = False
                txtOutputMinutes1.Visible = False
                txtOutputMinutes2.Visible = False
                txtOutputSeconds1.Visible = False
                txtOutputSeconds2.Visible = False
                lblOutUnits1.Visible = True
                lblOutUnits1.Text = "Sexagesimal Degrees"
                lblOutUnits2.Visible = True
                lblOutUnits2.Text = "Sexagesimal Degrees"
                txtOutput1.Visible = True
                txtOutput2.Visible = True
            Case "Radians"
                lblOutDeg1.Visible = False
                lblOutDeg2.Visible = False
                lblOutMin1.Visible = False
                lblOutMin2.Visible = False
                lblOutSec1.Visible = False
                lblOutSec2.Visible = False
                txtOutputSign1.Visible = False
                txtOutputSign2.Visible = False
                txtOutputDegrees1.Visible = False
                txtOutputDegrees2.Visible = False
                txtOutputMinutes1.Visible = False
                txtOutputMinutes2.Visible = False
                txtOutputSeconds1.Visible = False
                txtOutputSeconds2.Visible = False
                lblOutUnits1.Visible = True
                lblOutUnits1.Text = "Radians"
                lblOutUnits2.Visible = True
                lblOutUnits2.Text = "Radians"
                txtOutput1.Visible = True
                txtOutput2.Visible = True
            Case "Gradians"
                lblOutDeg1.Visible = False
                lblOutDeg2.Visible = False
                lblOutMin1.Visible = False
                lblOutMin2.Visible = False
                lblOutSec1.Visible = False
                lblOutSec2.Visible = False
                txtOutputSign1.Visible = False
                txtOutputSign2.Visible = False
                txtOutputDegrees1.Visible = False
                txtOutputDegrees2.Visible = False
                txtOutputMinutes1.Visible = False
                txtOutputMinutes2.Visible = False
                txtOutputSeconds1.Visible = False
                txtOutputSeconds2.Visible = False
                lblOutUnits1.Visible = True
                lblOutUnits1.Text = "Gradians"
                lblOutUnits2.Visible = True
                lblOutUnits2.Text = "Gradians"
                txtOutput1.Visible = True
                txtOutput2.Visible = True
            Case "Turns"
                lblOutDeg1.Visible = False
                lblOutDeg2.Visible = False
                lblOutMin1.Visible = False
                lblOutMin2.Visible = False
                lblOutSec1.Visible = False
                lblOutSec2.Visible = False
                txtOutputSign1.Visible = False
                txtOutputSign2.Visible = False
                txtOutputDegrees1.Visible = False
                txtOutputDegrees2.Visible = False
                txtOutputMinutes1.Visible = False
                txtOutputMinutes2.Visible = False
                txtOutputSeconds1.Visible = False
                txtOutputSeconds2.Visible = False
                lblOutUnits1.Visible = True
                lblOutUnits1.Text = "Turns"
                lblOutUnits2.Visible = True
                lblOutUnits2.Text = "Turns"
                txtOutput1.Visible = True
                txtOutput2.Visible = True
            Case "Metres"
                lblOutDeg1.Visible = False
                lblOutDeg2.Visible = False
                lblOutMin1.Visible = False
                lblOutMin2.Visible = False
                lblOutSec1.Visible = False
                lblOutSec2.Visible = False
                txtOutputSign1.Visible = False
                txtOutputSign2.Visible = False
                txtOutputDegrees1.Visible = False
                txtOutputDegrees2.Visible = False
                txtOutputMinutes1.Visible = False
                txtOutputMinutes2.Visible = False
                txtOutputSeconds1.Visible = False
                txtOutputSeconds2.Visible = False
                lblOutUnits1.Visible = True
                lblOutUnits1.Text = "Metres"
                lblOutUnits2.Visible = True
                lblOutUnits2.Text = "Metres"
                txtOutput1.Visible = True
                txtOutput2.Visible = True
            Case "Feet"
                lblOutDeg1.Visible = False
                lblOutDeg2.Visible = False
                lblOutMin1.Visible = False
                lblOutMin2.Visible = False
                lblOutSec1.Visible = False
                lblOutSec2.Visible = False
                txtOutputSign1.Visible = False
                txtOutputSign2.Visible = False
                txtOutputDegrees1.Visible = False
                txtOutputDegrees2.Visible = False
                txtOutputMinutes1.Visible = False
                txtOutputMinutes2.Visible = False
                txtOutputSeconds1.Visible = False
                txtOutputSeconds2.Visible = False
                lblOutUnits1.Visible = True
                lblOutUnits1.Text = "Feet"
                lblOutUnits2.Visible = True
                lblOutUnits2.Text = "Feet"
                txtOutput1.Visible = True
                txtOutput2.Visible = True
            Case "Default"
                lblOutDeg1.Visible = False
                lblOutDeg2.Visible = False
                lblOutMin1.Visible = False
                lblOutMin2.Visible = False
                lblOutSec1.Visible = False
                lblOutSec2.Visible = False
                txtOutputSign1.Visible = False
                txtOutputSign2.Visible = False
                txtOutputDegrees1.Visible = False
                txtOutputDegrees2.Visible = False
                txtOutputMinutes1.Visible = False
                txtOutputMinutes2.Visible = False
                txtOutputSeconds1.Visible = False
                txtOutputSeconds2.Visible = False
                lblOutUnits1.Visible = True
                lblOutUnits1.Text = "Default"
                lblOutUnits2.Visible = True
                lblOutUnits2.Text = "Default"
                txtOutput1.Visible = True
                txtOutput2.Visible = True
        End Select
    End Sub

    Private Sub btnConvertProjection_Click(sender As Object, e As EventArgs) Handles btnConvertProjection.Click
        'Covert between the specified geographic and projected coordinates.

        'Create the XML instructions to convert the angles:
        Dim decl As New XDeclaration("1.0", "utf-8", "yes")
        Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.

        Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
        'Dim clientName As New XElement("ClientName", "CoordinatesClient") 'This tells the coordinate server the name of the client making the request.
        Dim clientName As New XElement("ClientName", ApplicationInfo.Name) 'This tells the coordinate server the name of the client making the request.
        xmessage.Add(clientName)
        Dim operation As New XElement("ConvertProjectedCoordinates")

        Dim projectedCRS As New XElement("ProjectedCRS", cmbProjectedCRS.Text)
        operation.Add(projectedCRS)

        Dim inputCoordinates As New XElement("InputCoordinates")

        Select Case cmbProjnInput.Text
            Case "Latitude, Longitude"
                'Dim conversionType As New XElement("ConversionType", "GeographicToProjected")
                'operation.Add(conversionType)
                Dim Type As New XElement("Type", "Geographic")
                inputCoordinates.Add(Type)
                Select Case cmbProjnInputUnits.Text
                    Case "Degrees, Minutes, Seconds"
                        If txtInputSign1.Text = "+" Then
                            Dim LatDmsSign As New XElement("LatitudeDmsSign", "+")
                            inputCoordinates.Add(LatDmsSign)
                        ElseIf txtInputSign1.Text = "" Then
                            Dim LatDmsSign As New XElement("LatitudeDmsSign", "+")
                            inputCoordinates.Add(LatDmsSign)
                        ElseIf txtInputSign1.Text = "-" Then
                            Dim LatDmsSign As New XElement("LatitudeDmsSign", "-")
                            inputCoordinates.Add(LatDmsSign)
                        Else

                        End If
                        Dim LatDmsDegrees As New XElement("LatitudeDmsDegrees", txtInputDegrees1.Text)
                        inputCoordinates.Add(LatDmsDegrees)
                        Dim LatDmsMinutes As New XElement("LatitudeDmsMinutes", txtInputMinutes1.Text)
                        inputCoordinates.Add(LatDmsMinutes)
                        Dim LatDmsSeconds As New XElement("LatitudeDmsSeconds", txtInputSeconds1.Text)
                        inputCoordinates.Add(LatDmsSeconds)
                        If txtInputSign2.Text = "+" Then
                            Dim LongDmsSign As New XElement("LongitudeDmsSign", "+")
                            inputCoordinates.Add(LongDmsSign)
                        ElseIf txtInputSign2.Text = "" Then
                            Dim LongDmsSign As New XElement("LongitudeDmsSign", "+")
                            inputCoordinates.Add(LongDmsSign)
                        ElseIf txtInputSign2.Text = "-" Then
                            Dim LongDmsSign As New XElement("LongitudeDmsSign", "-")
                            inputCoordinates.Add(LongDmsSign)
                        Else

                        End If
                        Dim LongDmsDegrees As New XElement("LongitudeDmsDegrees", txtInputDegrees2.Text)
                        inputCoordinates.Add(LongDmsDegrees)
                        Dim LongDmsMinutes As New XElement("LongitudeDmsMinutes", txtInputMinutes2.Text)
                        inputCoordinates.Add(LongDmsMinutes)
                        Dim LongDmsSeconds As New XElement("LongitudeDmsSeconds", txtInputSeconds2.Text)
                        inputCoordinates.Add(LongDmsSeconds)

                    Case "Decimal Degrees"
                        Dim LatitudeDecimalDegrees As New XElement("LatitudeDecimalDegrees", txtInput1.Text)
                        inputCoordinates.Add(LatitudeDecimalDegrees)
                        Dim LongitudeDecimalDegrees As New XElement("LongitudeDecimalDegrees", txtInput2.Text)
                        inputCoordinates.Add(LongitudeDecimalDegrees)
                    Case "Sexagesimal Degrees"
                        Dim LatitudeSexagesimalDegrees As New XElement("LatitudeSexagesimalDegrees", txtInput1.Text)
                        inputCoordinates.Add(LatitudeSexagesimalDegrees)
                        Dim LongitudeSexagesimalDegrees As New XElement("LongitudeSexagesimalDegrees", txtInput2.Text)
                        inputCoordinates.Add(LongitudeSexagesimalDegrees)
                    Case "Radians"
                        Dim LatitudeRadians As New XElement("LatitudeRadians", txtInput1.Text)
                        inputCoordinates.Add(LatitudeRadians)
                        Dim LongitudeRadians As New XElement("LongitudeRadians", txtInput2.Text)
                        inputCoordinates.Add(LongitudeRadians)
                    Case "Gradians"
                        Dim LatitudeGradians As New XElement("LatitudeGradians", txtInput1.Text)
                        inputCoordinates.Add(LatitudeGradians)
                        Dim LongitudeGradians As New XElement("LongitudeGradians", txtInput2.Text)
                        inputCoordinates.Add(LongitudeGradians)
                    Case "Turns"
                        Dim LatitudeTurns As New XElement("LatitudeTurns", txtInput1.Text)
                        inputCoordinates.Add(LatitudeTurns)
                        Dim LongitudeTurns As New XElement("LongitudeTurns", txtInput2.Text)
                        inputCoordinates.Add(LongitudeTurns)
                End Select
            Case "Longitude, Latitude"
                Dim Type As New XElement("Type", "Geographic")
                inputCoordinates.Add(Type)
                Select Case cmbProjnInputUnits.Text
                    Case "Degrees, Minutes, Seconds"
                        If txtInputSign1.Text = "+" Then
                            Dim LongDmsSign As New XElement("LongitudeDmsSign", "+")
                            inputCoordinates.Add(LongDmsSign)
                        ElseIf txtInputSign1.Text = "" Then
                            Dim LongDmsSign As New XElement("LongitudeDmsSign", "+")
                            inputCoordinates.Add(LongDmsSign)
                        ElseIf txtInputSign1.Text = "-" Then
                            Dim LongDmsSign As New XElement("LongitudeDmsSign", "-")
                            inputCoordinates.Add(LongDmsSign)
                        Else

                        End If
                        Dim LongDmsDegrees As New XElement("LongitudeDmsDegrees", txtInputDegrees1.Text)
                        inputCoordinates.Add(LongDmsDegrees)
                        Dim LongDmsMinutes As New XElement("LongitudeDmsMinutes", txtInputMinutes1.Text)
                        inputCoordinates.Add(LongDmsMinutes)
                        Dim LongDmsSeconds As New XElement("LongitudeDmsSeconds", txtInputSeconds1.Text)
                        inputCoordinates.Add(LongDmsSeconds)
                        If txtInputSign2.Text = "+" Then
                            Dim LatDmsSign As New XElement("LatitudeDmsSign", "+")
                            inputCoordinates.Add(LatDmsSign)
                        ElseIf txtInputSign2.Text = "" Then
                            Dim LatDmsSign As New XElement("LatitudeDmsSign", "+")
                            inputCoordinates.Add(LatDmsSign)
                        ElseIf txtInputSign2.Text = "-" Then
                            Dim LatDmsSign As New XElement("LatitudeDmsSign", "-")
                            inputCoordinates.Add(LatDmsSign)
                        Else

                        End If
                        Dim LatDmsDegrees As New XElement("LatitudeDmsDegrees", txtInputDegrees2.Text)
                        inputCoordinates.Add(LatDmsDegrees)
                        Dim LatDmsMinutes As New XElement("LatitudeDmsMinutes", txtInputMinutes2.Text)
                        inputCoordinates.Add(LatDmsMinutes)
                        Dim LatDmsSeconds As New XElement("LatitudeDmsSeconds", txtInputSeconds2.Text)
                        inputCoordinates.Add(LatDmsSeconds)
                    Case "Decimal Degrees"
                        Dim LongitudeDecimalDegrees As New XElement("LongitudeDecimalDegrees", txtInput1.Text)
                        inputCoordinates.Add(LongitudeDecimalDegrees)
                        Dim LatitudeDecimalDegrees As New XElement("LatitudeDecimalDegrees", txtInput2.Text)
                        inputCoordinates.Add(LatitudeDecimalDegrees)
                    Case "Sexagesimal Degrees"
                        Dim LongitudeSexagesimalDegrees As New XElement("LongitudeSexagesimalDegrees", txtInput1.Text)
                        inputCoordinates.Add(LongitudeSexagesimalDegrees)
                        Dim LatitudeSexagesimalDegrees As New XElement("LatitudeSexagesimalDegrees", txtInput2.Text)
                        inputCoordinates.Add(LatitudeSexagesimalDegrees)
                    Case "Radians"
                        Dim LongitudeRadians As New XElement("LongitudeRadians", txtInput1.Text)
                        inputCoordinates.Add(LongitudeRadians)
                        Dim LatitudeRadians As New XElement("LatitudeRadians", txtInput2.Text)
                        inputCoordinates.Add(LatitudeRadians)
                    Case "Gradians"
                        Dim LongitudeGradians As New XElement("LongitudeGradians", txtInput1.Text)
                        inputCoordinates.Add(LongitudeGradians)
                        Dim LatitudeGradians As New XElement("LatitudeGradians", txtInput2.Text)
                        inputCoordinates.Add(LatitudeGradians)
                    Case "Turns"
                        Dim LongitudeTurns As New XElement("LongitudeTurns", txtInput1.Text)
                        inputCoordinates.Add(LongitudeTurns)
                        Dim LatitudeTurns As New XElement("LatitudeTurns", txtInput2.Text)
                        inputCoordinates.Add(LatitudeTurns)
                End Select
            Case "Easting, Northing"
                Dim Type As New XElement("Type", "Projected")
                inputCoordinates.Add(Type)
                Select Case cmbProjnInputUnits.Text
                    Case "Metres"
                        Dim EastingMetres As New XElement("EastingMetres", txtInput1.Text)
                        inputCoordinates.Add(EastingMetres)
                        Dim NorthingMetres As New XElement("NorthingMetres", txtInput2.Text)
                        inputCoordinates.Add(NorthingMetres)
                    Case "Feet"
                        Dim EastingFeet As New XElement("EastingFeet", txtInput1.Text)
                        inputCoordinates.Add(EastingFeet)
                        Dim NorthingFeet As New XElement("NorthingFeet", txtInput2.Text)
                        inputCoordinates.Add(NorthingFeet)
                    Case "Default"
                        Dim EastingDefaultUnits As New XElement("EastingDefaultUnits", txtInput1.Text)
                        inputCoordinates.Add(EastingDefaultUnits)
                        Dim NorthingDefaultUnits As New XElement("NorthingDefaultUnits", txtInput2.Text)
                        inputCoordinates.Add(NorthingDefaultUnits)
                End Select
            Case "Northing, Easting"
                Dim Type As New XElement("Type", "Projected")
                inputCoordinates.Add(Type)
                Select Case cmbProjnInputUnits.Text
                    Case "Metres"
                        Dim NorthingMetres As New XElement("NorthingMetres", txtInput1.Text)
                        inputCoordinates.Add(NorthingMetres)
                        Dim EastingMetres As New XElement("EastingMetres", txtInput2.Text)
                        inputCoordinates.Add(EastingMetres)
                    Case "Feet"
                        Dim NorthingFeet As New XElement("NorthingFeet", txtInput1.Text)
                        inputCoordinates.Add(NorthingFeet)
                        Dim EastingFeet As New XElement("EastingFeet", txtInput2.Text)
                        inputCoordinates.Add(EastingFeet)
                    Case "Default"
                        Dim NorthingDefaultUnits As New XElement("NorthingDefaultUnits", txtInput1.Text)
                        inputCoordinates.Add(NorthingDefaultUnits)
                        Dim EastingDefaultUnits As New XElement("EastingDefaultUnits", txtInput2.Text)
                        inputCoordinates.Add(EastingDefaultUnits)
                End Select
        End Select

        operation.Add(inputCoordinates)

        Dim outputCoordinates As New XElement("OutputCoordinates")

        Select Case cmbProjnOutput.Text
            Case "Latitude, Longitude"
                Dim Type As New XElement("OType", "Geographic")
                outputCoordinates.Add(Type)
                Select Case cmbProjnOutputUnits.Text
                    Case "Degrees, Minutes, Seconds"
                        Dim LatitudeUnits As New XElement("LatitudeUnits", "DegreesMinutesSeconds")
                        outputCoordinates.Add(LatitudeUnits)
                        Dim LongitudeUnits As New XElement("LongitudeUnits", "DegreesMinutesSeconds")
                        outputCoordinates.Add(LongitudeUnits)
                    Case "Decimal Degrees"
                        Dim LatitudeUnits As New XElement("LatitudeUnits", "DecimalDegrees")
                        outputCoordinates.Add(LatitudeUnits)
                        Dim LongitudeUnits As New XElement("LongitudeUnits", "DecimalDegrees")
                        outputCoordinates.Add(LongitudeUnits)
                    Case "Sexagesimal Degrees"
                        Dim LatitudeUnits As New XElement("LatitudeUnits", "SexagesimalDegrees")
                        outputCoordinates.Add(LatitudeUnits)
                        Dim LongitudeUnits As New XElement("LongitudeUnits", "SexagesimalDegrees")
                        outputCoordinates.Add(LongitudeUnits)
                    Case "Radians"
                        Dim LatitudeUnits As New XElement("LatitudeUnits", "Radians")
                        outputCoordinates.Add(LatitudeUnits)
                        Dim LongitudeUnits As New XElement("LongitudeUnits", "Radians")
                        outputCoordinates.Add(LongitudeUnits)
                    Case "Gradians"
                        Dim LatitudeUnits As New XElement("LatitudeUnits", "Gradians")
                        outputCoordinates.Add(LatitudeUnits)
                        Dim LongitudeUnits As New XElement("LongitudeUnits", "Gradians")
                        outputCoordinates.Add(LongitudeUnits)
                    Case "Turns"
                        Dim LatitudeUnits As New XElement("LatitudeUnits", "Turns")
                        outputCoordinates.Add(LatitudeUnits)
                        Dim LongitudeUnits As New XElement("LongitudeUnits", "Turns")
                        outputCoordinates.Add(LongitudeUnits)
                End Select
            Case "Longitude, Latitude"
                Dim Type As New XElement("Type", "Geographic")
                outputCoordinates.Add(Type)
                Select Case cmbProjnOutputUnits.Text
                    Case "Degrees, Minutes, Seconds"
                        Dim LongitudeUnits As New XElement("LongitudeUnits", "DegreesMinutesSeconds")
                        outputCoordinates.Add(LongitudeUnits)
                        Dim LatitudeUnits As New XElement("LatitudeUnits", "DegreesMinutesSeconds")
                        outputCoordinates.Add(LatitudeUnits)
                    Case "Decimal Degrees"
                        Dim LongitudeUnits As New XElement("LongitudeUnits", "DecimalDegrees")
                        outputCoordinates.Add(LongitudeUnits)
                        Dim LatitudeUnits As New XElement("LatitudeUnits", "DecimalDegrees")
                        outputCoordinates.Add(LatitudeUnits)
                    Case "Sexagesimal Degrees"
                        Dim LongitudeUnits As New XElement("LongitudeUnits", "SexagesimalDegrees")
                        outputCoordinates.Add(LongitudeUnits)
                        Dim LatitudeUnits As New XElement("LatitudeUnits", "SexagesimalDegrees")
                        outputCoordinates.Add(LatitudeUnits)
                    Case "Radians"
                        Dim LongitudeUnits As New XElement("LongitudeUnits", "Radians")
                        outputCoordinates.Add(LongitudeUnits)
                        Dim LatitudeUnits As New XElement("LatitudeUnits", "Radians")
                        outputCoordinates.Add(LatitudeUnits)
                    Case "Gradians"
                        Dim LongitudeUnits As New XElement("LongitudeUnits", "Gradians")
                        outputCoordinates.Add(LongitudeUnits)
                        Dim LatitudeUnits As New XElement("LatitudeUnits", "Gradians")
                        outputCoordinates.Add(LatitudeUnits)
                    Case "Turns"
                        Dim LongitudeUnits As New XElement("LongitudeUnits", "Turns")
                        outputCoordinates.Add(LongitudeUnits)
                        Dim LatitudeUnits As New XElement("LatitudeUnits", "Turns")
                        outputCoordinates.Add(LatitudeUnits)
                End Select
            Case "Easting, Northing"
                Dim Type As New XElement("Type", "Projected")
                outputCoordinates.Add(Type)
                Select Case cmbProjnOutputUnits.Text
                    Case "Metres"
                        Dim EastingUnits As New XElement("EastingUnits", "Metres")
                        outputCoordinates.Add(EastingUnits)
                        Dim NorthingUnits As New XElement("NorthingUnits", "Metres")
                        outputCoordinates.Add(NorthingUnits)
                    Case "Feet"
                        Dim EastingUnits As New XElement("EastingUnits", "Feet")
                        outputCoordinates.Add(EastingUnits)
                        Dim NorthingUnits As New XElement("NorthingUnits", "Feet")
                        outputCoordinates.Add(NorthingUnits)
                    Case "Default"
                        Dim EastingUnits As New XElement("EastingUnits", "Default")
                        outputCoordinates.Add(EastingUnits)
                        Dim NorthingUnits As New XElement("NorthingUnits", "Default")
                        outputCoordinates.Add(NorthingUnits)
                End Select
            Case "Northing, Easting"
                Dim Type As New XElement("Type", "Projected")
                outputCoordinates.Add(Type)
                Select Case cmbProjnOutputUnits.Text
                    Case "Metres"
                        Dim NorthingUnits As New XElement("NorthingUnits", "Metres")
                        outputCoordinates.Add(NorthingUnits)
                        Dim EastingUnits As New XElement("EastingUnits", "Metres")
                        outputCoordinates.Add(EastingUnits)
                    Case "Feet"
                        Dim NorthingUnits As New XElement("NorthingUnits", "Feet")
                        outputCoordinates.Add(NorthingUnits)
                        Dim EastingUnits As New XElement("EastingUnits", "Feet")
                        outputCoordinates.Add(EastingUnits)
                    Case "Default"
                        Dim NorthingUnits As New XElement("NorthingUnits", "Default")
                        outputCoordinates.Add(NorthingUnits)
                        Dim EastingUnits As New XElement("EastingUnits", "Default")
                        outputCoordinates.Add(EastingUnits)
                End Select
        End Select

        operation.Add(outputCoordinates)

        Dim command As New XElement("Command", "ConvertCoordinates")
        operation.Add(command)

        xmessage.Add(operation)
        doc.Add(xmessage)

        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Messge not sent!" & vbCrLf)
            Else
                'cmbProjectedCRS.Items.Clear()
                'client.SendMessageAsync("CoordinatesServer", doc.ToString)
                'client.SendMessageAsync("ADVL_Coordinates", doc.ToString)
                client.SendMessageAsync("ADVL_Coordinates_1", doc.ToString)

                Message.Color = Color.Red
                Message.FontStyle = FontStyle.Bold
                'Message.XAdd("Message sent to " & "ADVL_Coordinates" & ":" & vbCrLf)
                Message.XAdd("Message sent to " & "ADVL_Coordinates_1" & ":" & vbCrLf)
                Message.SetNormalStyle()
                Message.XAdd(doc.ToString & vbCrLf & vbCrLf)
            End If
        End If
    End Sub

    Private Sub btnConnectionTest_Click(sender As Object, e As EventArgs) Handles btnConnectionTest.Click
        'Test the connection

        If IsNothing(client) Then
            'client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
            txtConnectionTest.Text = "Connection has not been created."
        Else
            Message.Add("client.State.ToString: " & client.State.ToString & vbCrLf)
            Message.Add("client.InnerChannel.State: " & client.InnerChannel.State & vbCrLf)
            Message.Add("client.Endpoint.Binding.ToString: " & client.Endpoint.Binding.ToString & vbCrLf)
            Message.Add("client.Endpoint.Address.ToString: " & client.Endpoint.Address.ToString & vbCrLf)
            'ServiceReference1.MsgServiceClient.
            Message.Add("client.InnerDuplexChannel.State.ToString: " & client.InnerDuplexChannel.State.ToString & vbCrLf)
            Message.Add("client.State.ToString: " & client.State.ToString & vbCrLf)
            Message.Add("client.ClientCredentials.ToString: " & client.ClientCredentials.ToString & vbCrLf)

            If client.State = ServiceModel.CommunicationState.Faulted Then
                txtConnectionTest.Text = "Connection state is faulted."
            Else
                txtConnectionTest.Text = "Testing connection..."
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 2) 'Temporarily set the send timeaout to 2 seconds
                client.Endpoint.Binding.ReceiveTimeout = New System.TimeSpan(0, 0, 2) 'Temporarily set the send timeaout to 2 seconds
                client.Endpoint.Binding.OpenTimeout = New System.TimeSpan(0, 0, 2) 'Temporarily set the send timeaout to 2 seconds
                client.Endpoint.Binding.CloseTimeout = New System.TimeSpan(0, 0, 2) 'Temporarily set the send timeaout to 2 seconds
                'client.Endpoint.Binding.
                'client.

                Try
                    If client.IsAlive Then
                        txtConnectionTest.Text = "Connection is OK."
                    End If
                Catch ex As Exception
                    Message.Add("Error: " & ex.Message & vbCrLf)
                End Try

            End If
        End If
    End Sub

    Private Sub btnNameFind_Click(sender As Object, e As EventArgs) Handles btnNameFind.Click
        'Find the first record with specified text contained within the Name field.
        FindRecord(txtSearchText.Text)
    End Sub

    Private Sub FindRecord(ByVal SearchString As String)
        'Find a record using the SearchString to match the Name field
        Dim FoundIndex As Integer
        'FoundIndex = Main.GeocentricCRS.List.FindIndex(Function(x) x.Name.Contains(SearchString))
        FoundIndex = cmbProjectedCRS.FindString(SearchString)
        If FoundIndex = -1 Then
            Message.Add("String not found." & vbCrLf)
        Else
            'CurrentRecordNo = FoundIndex + 1
            cmbProjectedCRS.SelectedIndex = FoundIndex
        End If
    End Sub

    Private Sub btnNameFindNext_Click(sender As Object, e As EventArgs) Handles btnNameFindNext.Click
        'Find the next record with specified text contained within the Name field.
        Dim FoundIndex As Integer
        FoundIndex = cmbProjectedCRS.FindString(txtSearchText.Text, cmbProjectedCRS.SelectedIndex)
        If FoundIndex = -1 Then
            Message.Add("String not found." & vbCrLf)
        Else
            cmbProjectedCRS.SelectedIndex = FoundIndex
        End If

    End Sub

    Private Sub btnNameFindPrev_Click(sender As Object, e As EventArgs) Handles btnNameFindPrev.Click
        'Find the previous record with specified text contained within the Name field.
        Dim FoundIndex As Integer

    End Sub


#Region "Run XMessage Statements"

    Private Sub XMsg_ErrorMsg(ErrMsg As String) Handles XMsg.ErrorMsg
        'Process the error message:
        Message.Add("XMsg Error message: " & ErrMsg & vbCrLf)
    End Sub

    'Private Sub XMsg_Instruction(Path As String, Prop As String) Handles XMsg.Instruction
    'Private Sub XMsg_Instruction(Locn As String, Info As String) Handles XMsg.Instruction
    Private Sub XMsg_Instruction(Info As String, Locn As String) Handles XMsg.Instruction
        'Process each Property Path and Property Value instruction.

        'MessageAdd("Property Path = " & Path & "    Property Value = " & Prop & vbCrLf)

        'CODE UPDATES: Angle conversions are now done on the Main form instead of the Utilities form.
        Select Case Locn
            Case "ConvertedAngle:OutputDecimalDegrees"
                'Utilities.txtOutputAngle.Text = Prop
                txtOutputAngle.Text = Info
            Case "ConvertedAngle:OutputSexagesimalDegrees"
                'Utilities.txtOutputAngle.Text = Prop
                txtOutputAngle.Text = Info
            Case "ConvertedAngle:OutputDmsSign"
                'Utilities.txtOutputSign.Text = Prop
                txtOutputSign.Text = Info
            Case "ConvertedAngle:OutputDmsDegrees"
                'Utilities.txtOutputDegrees.Text = Prop
                txtOutputDegrees.Text = Info
            Case "ConvertedAngle:OutputDmsMinutes"
                'Utilities.txtOutputMinutes.Text = Prop
                txtOutputMinutes.Text = Info
            Case "ConvertedAngle:OutputDmsSeconds"
                'Utilities.txtOutputSeconds.Text = Prop
                txtOutputSeconds.Text = Info
            Case "ConvertedAngle:OutputRadians"
                'Utilities.txtOutputAngle.Text = Prop
                txtOutputAngle.Text = Info
            Case "ConvertedAngle:OutputGradians"
                'Utilities.txtOutputAngle.Text = Prop
                txtOutputAngle.Text = Info
            Case "ConvertedAngle:OutputTurns"
                'Utilities.txtOutputAngle.Text = Prop
                txtOutputAngle.Text = Info
            Case "ProjectedCrsList:ProjectedCrsName"
                'Utilities.cmbProjectedCRS.Items.Add(Prop)
                cmbProjectedCRS.Items.Add(Info)

            Case "TransformedCoordinates:OutputEasting"
                Select Case cmbProjnOutput.Text
                    Case "Easting, Northing"
                        txtOutput1.Text = Info
                    Case "Northing, Easting"
                        txtOutput2.Text = Info
                End Select

            Case "TransformedCoordinates:OutputNorthing"
                Select Case cmbProjnOutput.Text
                    Case "Easting, Northing"
                        txtOutput2.Text = Info
                    Case "Northing, Easting"
                        txtOutput1.Text = Info
                End Select

                'Case "MessageExchangeClosing"
            Case "ApplicationNetworkClosing"
                'CloseExchangeConnection() 'Error running XMsg: This operation would deadlock because the reply cannot be received until the current Message completes processing. If you want to allow out-of-order message processing, specify ConcurrencyMode of Reentrant or Multiple on CallbackBehaviorAttribute.

                btnOnline.Text = "Offline"
                btnOnline.ForeColor = Color.Black
                'ConnectedToExchange = False
                ConnectedToAppnet = False
                Try
                    client.Close()
                Catch ex As Exception
                    client.Abort()
                End Try
                client = Nothing



                'Timer2.Interval = 100 '100ms delay
                'Timer2.Enabled = True

            Case "EndOfSequence"

            Case Else
                Message.Add("Instruction not recognised:  " & Locn & "    Property:  " & Info & vbCrLf)

        End Select

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        'CloseExchangeConnection()
        DisconnectFromAppNet()

        'Stop timer:
        Timer2.Enabled = False
    End Sub


#End Region 'Run XMessage Statements


#Region " Send XMessage"

    Private Sub SendMessage()
        'Code used to send a message after a timer delay.
        'The message destination is stored in MessageDest
        'The message text is stored in MessageText
        Timer1.Interval = 100 '100ms delay
        Timer1.Enabled = True 'Start the timer.
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If IsNothing(client) Then
            Message.SetWarningStyle()
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.SetWarningStyle()
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                Try
                    Message.Add("Sending a message. Number of characters: " & MessageText.Length & vbCrLf)
                    client.SendMessage(MessageDest, MessageText)
                    Message.XAdd(MessageText & vbCrLf)
                    MessageText = "" 'Clear the message after it has been sent.
                Catch ex As Exception
                    Message.SetWarningStyle()
                    Message.Add("Error sending message: " & ex.Message & vbCrLf)
                End Try
            End If
        End If

        'Stop timer:
        Timer1.Enabled = False
    End Sub

#End Region 'Process XMessages

#End Region 'Form Methods - The main actions performed by this form.


















End Class
