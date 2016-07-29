Public Class frmUtilities
    'The Utilities form is used to perform a range of useful coordinate functions.

    'Dim myCommCheck As New CommCheck 'Communications check class. Sends a test message every 60 seconds.

    'Dim myTimer As System.Timers.Timer

#Region " Process XML files - Read and write XML files."

    'Private Sub WriteFormSettingsXmlFile()
    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <!--Form settings for Utilities form.-->
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

        Dim SettingsFileName As String = "Formsettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)

        'If Trim(Main.ProjectPath) <> "" Then 'Write the Form Settings file in the Project Directory
        '    settingsData.Save(Main.ProjectPath & "\" & "FormSettings_" & Me.Text & ".xml")
        'Else 'Write the Form Settings file in the Application Directory
        '    settingsData.Save(Main.ApplicationDir & "\" & "FormSettings_" & Me.Text & ".xml")
        'End If

    End Sub

    'Private Sub ReadFormSettingsXmlFile()
    '    'Read the Form Settings XML file:

    '    Dim FilePath As String

    '    If Trim(Main.ProjectPath) <> "" Then 'Read the Form Settings file in the Project Directory
    '        If System.IO.File.Exists(Main.ProjectPath & "\" & "FormSettings_" & Me.Text & ".xml") Then
    '            FilePath = Main.ProjectPath & "\" & "FormSettings_" & Me.Text & ".xml"
    '            If System.IO.File.Exists(FilePath) Then
    '                ReadSettings(FilePath)
    '            Else
    '                'Initialise property values:
    '            End If

    '            'Dim settingsData As System.Xml.Linq.XDocument = XDocument.Load(Main.ProjectPath & "\" & "FormSettings_" & Me.Text & ".xml")

    '            ''Read form position and size:
    '            'Me.Left = settingsData.<FormSettings>.<Left>.Value
    '            'Me.Top = settingsData.<FormSettings>.<Top>.Value
    '            'Me.Height = settingsData.<FormSettings>.<Height>.Value
    '            'Me.Width = settingsData.<FormSettings>.<Width>.Value

    '        Else
    '            'Initialise property values:

    '        End If
    '    Else 'Read the Form Settings in the Application Directory
    '        FilePath = Main.ApplicationDir & "\" & "FormSettings_" & Me.Text & ".xml"

    '        If System.IO.File.Exists(FilePath) Then
    '            ReadSettings(FilePath)

    '            'Dim settingsData As System.Xml.Linq.XDocument = XDocument.Load(Main.ApplicationDir & "\" & "FormSettings_" & Me.Text & ".xml")

    '            ''Read form position and size:
    '            'Me.Left = settingsData.<FormSettings>.<Left>.Value
    '            'Me.Top = settingsData.<FormSettings>.<Top>.Value
    '            'Me.Height = settingsData.<FormSettings>.<Height>.Value
    '            'Me.Width = settingsData.<FormSettings>.<Width>.Value

    '        Else
    '            'Initialise property values:

    '        End If
    '    End If

    'End Sub

    'Private Sub ReadSettings(ByVal FilePath As String)
    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        Dim SettingsFileName As String = "Formsettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"

        'Dim settingsData As System.Xml.Linq.XDocument = XDocument.Load(FilePath)

        If Main.Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Main.Project.ReadXmlSettings(SettingsFileName, Settings)

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

        ''Read form position and size:
        'Me.Left = settingsData.<FormSettings>.<Left>.Value
        'Me.Top = settingsData.<FormSettings>.<Top>.Value
        'Me.Height = settingsData.<FormSettings>.<Height>.Value
        'Me.Width = settingsData.<FormSettings>.<Width>.Value

    End Sub

#End Region

#Region " Form Subroutines - Code used to display this form."

    Private Sub myForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        'ReadFormSettingsXmlFile()

        'Restore the form settings: ---------------------------------------------------------
        RestoreFormSettings()

        cmbInput.Items.Add("Degrees, Minutes, Seconds")
        cmbInput.Items.Add("Decimal Degrees")
        cmbInput.Items.Add("Sexagesimal Degrees")
        cmbInput.Items.Add("Radians")
        cmbInput.Items.Add("Gradians")
        cmbInput.Items.Add("Turns")
        cmbInput.SelectedIndex = 0

        cmbOutput.Items.Add("Degrees, Minutes, Seconds")
        cmbOutput.Items.Add("Decimal Degrees")
        cmbOutput.Items.Add("Sexagesimal Degrees")
        cmbOutput.Items.Add("Radians")
        cmbOutput.Items.Add("Gradians")
        cmbOutput.Items.Add("Turns")
        cmbOutput.SelectedIndex = 1

        cmbProjnInput.Items.Add("Latitude, Longitude")
        cmbProjnInput.Items.Add("Longitude, Latitude")
        cmbProjnInput.Items.Add("Easting, Northing")
        cmbProjnInput.Items.Add("Northing, Easting")
        cmbProjnInput.SelectedIndex = 0 'Select first item

        cmbProjnOutput.Items.Add("Latitude, Longitude")
        cmbProjnOutput.Items.Add("Longitude, Latitude")
        cmbProjnOutput.Items.Add("Easting, Northing")
        cmbProjnOutput.Items.Add("Northing, Easting")
        cmbProjnOutput.SelectedIndex = 2 'Select third item

        lblInUnits1.Location = New Point(9, 106)
        txtInput1.Location = New Point(9, 123)
        lblInUnits2.Location = New Point(253, 106)
        txtInput2.Location = New Point(253, 123)
        lblOutUnits1.Location = New Point(495, 106)
        txtOutput1.Location = New Point(495, 123)
        lblOutUnits2.Location = New Point(733, 106)
        txtOutput2.Location = New Point(733, 123)

        'CommCheck.Start()


        'myTimer = New System.Timers.Timer()
        'myTimer.Interval() = 10000 '10 seconds
        'AddHandler myTimer.Elapsed, AddressOf OnTimedEvent
        'myTimer.Enabled = True

    End Sub

    'Private Sub OnTimedEvent(source As Object, e As System.Timers.ElapsedEventArgs)

    '    Main.MessageAdd("Timer elapsed " & vbCrLf)

    '    If IsNothing(Main.client) Then
    '        Main.MessageAdd("No client connection available!  " & Format(Now, "d-MMM-yyyy H:mm:ss") & vbCrLf)
    '    Else
    '        If Main.client.State = ServiceModel.CommunicationState.Faulted Then
    '            Main.MessageAdd("client state is faulted. Messge not sent!  " & Format(Now, "d-MMM-yyyy H:mm:ss") & vbCrLf)
    '        Else

    '            Main.client.SendMessageAsync("CoordinateServer", "Comm Check  " & Format(Now, "d-MMM-yyyy H:mm:ss") & vbCrLf)
    '        End If

    '    End If

    'End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form

        'Save the form settings: ------------------------------------------------------------
        SaveFormSettings()

        'Close the form: --------------------------------------------------------------------
        Me.Close()
    End Sub

    'Private Sub myForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    '    SaveSettings()
    'End Sub

    'Private Sub SaveSettings()
    '    WriteFormSettingsXmlFile()
    'End Sub

#End Region 'Form Subroutines

#Region " Form Methods - The main actions performed by this form."

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

    Private Sub btnConvert_Click(sender As Object, e As EventArgs) Handles btnConvert.Click
        'Convert Input angle to Output angle

        'Create the XML instructions to convert the angles:
        Dim decl As New XDeclaration("1.0", "utf-8", "yes")
        Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.

        Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
        Dim clientName As New XElement("ClientName", "CoordinatesClient") 'This tells the coordinate server the name of the client making the request.
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

        'Process.Add(operation)
        xmessage.Add(operation)

        'doc.Add(Process)
        doc.Add(xmessage)

        'Send the message.
        'The message text to send is in the rich text box is rtbMessageToSend.
        'The name of the destination application is in the txtDestination text box.

        'To send a message to the Message Exchange application, use the destination "MessageExchange"

        If IsNothing(Main.client) Then
            'rtbMessagesReceived.AppendText("No client connection available!" & vbCrLf)
            Main.Message.Add("No client connection available!" & vbCrLf)
        Else
            If Main.client.State = ServiceModel.CommunicationState.Faulted Then
                'rtbMessagesReceived.AppendText("client state is faulted. Messge not sent!" & vbCrLf)
                Main.Message.Add("client state is faulted. Messge not sent!" & vbCrLf)
                'Dim StateFlag As System.Enum
                'Dim StateHasFlag As Boolean
                'StateHasFlag = client.State.HasFlag(StateFlag)
            Else
                'Main.client.SendMessageAsync(txtDestination.Text, rtbMessageToSend.Text)
                Main.client.SendMessageAsync("CoordinateServer", doc.ToString)
            End If

        End If

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

    Private Sub btnUpdateList_Click(sender As Object, e As EventArgs) Handles btnUpdateList.Click
        'Get an updated list of projected coordinate refernce systems.

        'Create the XMsg instructions to get the list.
        Dim decl As New XDeclaration("1.0", "utf-8", "yes")
        Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.

        Dim xmessage As New XElement("XMsg")
        Dim clientName As New XElement("ClientName", "CoordinatesClient") 'This tells the coordinate server the name of the client making the request.
        xmessage.Add(clientName)
        Dim operation As New XElement("Command", "GetProjectedCrsList") 'This tells the coordinate server to reply with the list of projected coordinate reference systems
        xmessage.Add(operation)

        doc.Add(xmessage)

        If IsNothing(Main.client) Then
            Main.Message.Add("No client connection available!" & vbCrLf)
        Else
            If Main.client.State = ServiceModel.CommunicationState.Faulted Then
                Main.Message.Add("client state is faulted. Messge not sent!" & vbCrLf)
            Else
                cmbProjectedCRS.Items.Clear()
                Main.client.SendMessageAsync("CoordinateServer", doc.ToString)
            End If
        End If

    End Sub

#End Region 'Form Methods


   

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
        Dim clientName As New XElement("ClientName", "CoordinatesClient") 'This tells the coordinate server the name of the client making the request.
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

        If IsNothing(Main.client) Then
            Main.Message.Add("No client connection available!" & vbCrLf)
        Else
            If Main.client.State = ServiceModel.CommunicationState.Faulted Then
                Main.Message.Add("client state is faulted. Messge not sent!" & vbCrLf)
            Else
                'cmbProjectedCRS.Items.Clear()
                Main.client.SendMessageAsync("CoordinateServer", doc.ToString)
            End If
        End If


    End Sub

    Private Sub txtInputAngle_TextChanged(sender As Object, e As EventArgs) Handles txtInputAngle.TextChanged

    End Sub
End Class




'Public Class CommCheck
'    Shared _timer As System.Timers.Timer

'    Shared Sub Start()
'        _timer = New System.Timers.Timer(6000) 'Timer created with a 6 second interval
'        AddHandler _timer.Elapsed, AddressOf OnTimedEvent
'        _timer.Enabled = True

'    End Sub

'    Private Shared Sub OnTimedEvent(source As Object, e As System.Timers.ElapsedEventArgs)

'        If IsNothing(Main.client) Then
'            Main.MessageAdd("No client connection available!  " & Format(Now, "d-MMM-yyyy H:mm:ss") & vbCrLf)
'        Else
'            If Main.client.State = ServiceModel.CommunicationState.Faulted Then
'                Main.MessageAdd("client state is faulted. Messge not sent!  " & Format(Now, "d-MMM-yyyy H:mm:ss") & vbCrLf)
'            Else

'                Main.client.SendMessageAsync("CoordinateServer", "Comm Check  " & Format(Now, "d-MMM-yyyy H:mm:ss") & vbCrLf)
'            End If

'        End If

'    End Sub
'End Class

