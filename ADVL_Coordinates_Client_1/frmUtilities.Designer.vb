<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUtilities
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txtOutputSign = New System.Windows.Forms.TextBox()
        Me.txtInputSign = New System.Windows.Forms.TextBox()
        Me.txtOutputAngle = New System.Windows.Forms.TextBox()
        Me.txtInputAngle = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtOutputSeconds = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtOutputMinutes = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtOutputDegrees = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtInputSeconds = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtInputMinutes = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtInputDegrees = New System.Windows.Forms.TextBox()
        Me.btnConvert = New System.Windows.Forms.Button()
        Me.cmbOutput = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbInput = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btnConvertProjection = New System.Windows.Forms.Button()
        Me.lblOutUnits2 = New System.Windows.Forms.Label()
        Me.lblOutUnits1 = New System.Windows.Forms.Label()
        Me.lblInUnits2 = New System.Windows.Forms.Label()
        Me.lblInUnits1 = New System.Windows.Forms.Label()
        Me.lblOutput2 = New System.Windows.Forms.Label()
        Me.lblOutput1 = New System.Windows.Forms.Label()
        Me.lblInput2 = New System.Windows.Forms.Label()
        Me.lblInput1 = New System.Windows.Forms.Label()
        Me.txtOutputSign2 = New System.Windows.Forms.TextBox()
        Me.txtOutput2 = New System.Windows.Forms.TextBox()
        Me.lblOutSec2 = New System.Windows.Forms.Label()
        Me.txtOutputSeconds2 = New System.Windows.Forms.TextBox()
        Me.lblOutMin2 = New System.Windows.Forms.Label()
        Me.txtOutputMinutes2 = New System.Windows.Forms.TextBox()
        Me.lblOutDeg2 = New System.Windows.Forms.Label()
        Me.txtOutputDegrees2 = New System.Windows.Forms.TextBox()
        Me.txtOutputSign1 = New System.Windows.Forms.TextBox()
        Me.txtOutput1 = New System.Windows.Forms.TextBox()
        Me.lblOutSec1 = New System.Windows.Forms.Label()
        Me.txtOutputSeconds1 = New System.Windows.Forms.TextBox()
        Me.lblOutMin1 = New System.Windows.Forms.Label()
        Me.txtOutputMinutes1 = New System.Windows.Forms.TextBox()
        Me.lblOutDeg1 = New System.Windows.Forms.Label()
        Me.txtOutputDegrees1 = New System.Windows.Forms.TextBox()
        Me.txtInputSign2 = New System.Windows.Forms.TextBox()
        Me.txtInput2 = New System.Windows.Forms.TextBox()
        Me.lblInSec2 = New System.Windows.Forms.Label()
        Me.txtInputSeconds2 = New System.Windows.Forms.TextBox()
        Me.lblInMin2 = New System.Windows.Forms.Label()
        Me.txtInputMinutes2 = New System.Windows.Forms.TextBox()
        Me.lblInDeg2 = New System.Windows.Forms.Label()
        Me.txtInputDegrees2 = New System.Windows.Forms.TextBox()
        Me.txtInputSign1 = New System.Windows.Forms.TextBox()
        Me.txtInput1 = New System.Windows.Forms.TextBox()
        Me.lblInSec1 = New System.Windows.Forms.Label()
        Me.txtInputSeconds1 = New System.Windows.Forms.TextBox()
        Me.lblInMin1 = New System.Windows.Forms.Label()
        Me.txtInputMinutes1 = New System.Windows.Forms.TextBox()
        Me.lblInDeg1 = New System.Windows.Forms.Label()
        Me.txtInputDegrees1 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbProjnOutputUnits = New System.Windows.Forms.ComboBox()
        Me.cmbProjnInputUnits = New System.Windows.Forms.ComboBox()
        Me.cmbProjnOutput = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbProjnInput = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnUpdateList = New System.Windows.Forms.Button()
        Me.cmbProjectedCRS = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(944, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(64, 22)
        Me.btnExit.TabIndex = 20
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 40)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(996, 277)
        Me.TabControl1.TabIndex = 21
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtOutputSign)
        Me.TabPage1.Controls.Add(Me.txtInputSign)
        Me.TabPage1.Controls.Add(Me.txtOutputAngle)
        Me.TabPage1.Controls.Add(Me.txtInputAngle)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.txtOutputSeconds)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.txtOutputMinutes)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.txtOutputDegrees)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.txtInputSeconds)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.txtInputMinutes)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.txtInputDegrees)
        Me.TabPage1.Controls.Add(Me.btnConvert)
        Me.TabPage1.Controls.Add(Me.cmbOutput)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.cmbInput)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(988, 251)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Angles"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txtOutputSign
        '
        Me.txtOutputSign.Location = New System.Drawing.Point(265, 65)
        Me.txtOutputSign.Name = "txtOutputSign"
        Me.txtOutputSign.Size = New System.Drawing.Size(16, 20)
        Me.txtOutputSign.TabIndex = 113
        '
        'txtInputSign
        '
        Me.txtInputSign.Location = New System.Drawing.Point(8, 65)
        Me.txtInputSign.Name = "txtInputSign"
        Me.txtInputSign.Size = New System.Drawing.Size(16, 20)
        Me.txtInputSign.TabIndex = 112
        '
        'txtOutputAngle
        '
        Me.txtOutputAngle.Location = New System.Drawing.Point(265, 91)
        Me.txtOutputAngle.Name = "txtOutputAngle"
        Me.txtOutputAngle.Size = New System.Drawing.Size(120, 20)
        Me.txtOutputAngle.TabIndex = 111
        '
        'txtInputAngle
        '
        Me.txtInputAngle.Location = New System.Drawing.Point(8, 91)
        Me.txtInputAngle.Name = "txtInputAngle"
        Me.txtInputAngle.Size = New System.Drawing.Size(120, 20)
        Me.txtInputAngle.TabIndex = 110
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(378, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 13)
        Me.Label3.TabIndex = 109
        Me.Label3.Text = "Sec"
        '
        'txtOutputSeconds
        '
        Me.txtOutputSeconds.Location = New System.Drawing.Point(375, 65)
        Me.txtOutputSeconds.Name = "txtOutputSeconds"
        Me.txtOutputSeconds.Size = New System.Drawing.Size(112, 20)
        Me.txtOutputSeconds.TabIndex = 108
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(334, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 13)
        Me.Label4.TabIndex = 107
        Me.Label4.Text = "Min"
        '
        'txtOutputMinutes
        '
        Me.txtOutputMinutes.Location = New System.Drawing.Point(331, 65)
        Me.txtOutputMinutes.Name = "txtOutputMinutes"
        Me.txtOutputMinutes.Size = New System.Drawing.Size(38, 20)
        Me.txtOutputMinutes.TabIndex = 106
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(290, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(27, 13)
        Me.Label5.TabIndex = 105
        Me.Label5.Text = "Deg"
        '
        'txtOutputDegrees
        '
        Me.txtOutputDegrees.Location = New System.Drawing.Point(287, 65)
        Me.txtOutputDegrees.Name = "txtOutputDegrees"
        Me.txtOutputDegrees.Size = New System.Drawing.Size(39, 20)
        Me.txtOutputDegrees.TabIndex = 104
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(120, 48)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(26, 13)
        Me.Label9.TabIndex = 103
        Me.Label9.Text = "Sec"
        '
        'txtInputSeconds
        '
        Me.txtInputSeconds.Location = New System.Drawing.Point(117, 65)
        Me.txtInputSeconds.Name = "txtInputSeconds"
        Me.txtInputSeconds.Size = New System.Drawing.Size(120, 20)
        Me.txtInputSeconds.TabIndex = 102
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(76, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 13)
        Me.Label7.TabIndex = 101
        Me.Label7.Text = "Min"
        '
        'txtInputMinutes
        '
        Me.txtInputMinutes.Location = New System.Drawing.Point(73, 65)
        Me.txtInputMinutes.Name = "txtInputMinutes"
        Me.txtInputMinutes.Size = New System.Drawing.Size(38, 20)
        Me.txtInputMinutes.TabIndex = 100
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(32, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(27, 13)
        Me.Label6.TabIndex = 99
        Me.Label6.Text = "Deg"
        '
        'txtInputDegrees
        '
        Me.txtInputDegrees.Location = New System.Drawing.Point(29, 65)
        Me.txtInputDegrees.Name = "txtInputDegrees"
        Me.txtInputDegrees.Size = New System.Drawing.Size(39, 20)
        Me.txtInputDegrees.TabIndex = 98
        '
        'btnConvert
        '
        Me.btnConvert.Location = New System.Drawing.Point(176, 24)
        Me.btnConvert.Name = "btnConvert"
        Me.btnConvert.Size = New System.Drawing.Size(68, 22)
        Me.btnConvert.TabIndex = 97
        Me.btnConvert.Text = "Convert"
        Me.btnConvert.UseVisualStyleBackColor = True
        '
        'cmbOutput
        '
        Me.cmbOutput.FormattingEnabled = True
        Me.cmbOutput.Location = New System.Drawing.Point(265, 24)
        Me.cmbOutput.Name = "cmbOutput"
        Me.cmbOutput.Size = New System.Drawing.Size(159, 21)
        Me.cmbOutput.TabIndex = 96
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(262, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 95
        Me.Label2.Text = "Output"
        '
        'cmbInput
        '
        Me.cmbInput.FormattingEnabled = True
        Me.cmbInput.Location = New System.Drawing.Point(11, 24)
        Me.cmbInput.Name = "cmbInput"
        Me.cmbInput.Size = New System.Drawing.Size(159, 21)
        Me.cmbInput.TabIndex = 94
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 93
        Me.Label1.Text = "Input"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnConvertProjection)
        Me.TabPage2.Controls.Add(Me.lblOutUnits2)
        Me.TabPage2.Controls.Add(Me.lblOutUnits1)
        Me.TabPage2.Controls.Add(Me.lblInUnits2)
        Me.TabPage2.Controls.Add(Me.lblInUnits1)
        Me.TabPage2.Controls.Add(Me.lblOutput2)
        Me.TabPage2.Controls.Add(Me.lblOutput1)
        Me.TabPage2.Controls.Add(Me.lblInput2)
        Me.TabPage2.Controls.Add(Me.lblInput1)
        Me.TabPage2.Controls.Add(Me.txtOutputSign2)
        Me.TabPage2.Controls.Add(Me.txtOutput2)
        Me.TabPage2.Controls.Add(Me.lblOutSec2)
        Me.TabPage2.Controls.Add(Me.txtOutputSeconds2)
        Me.TabPage2.Controls.Add(Me.lblOutMin2)
        Me.TabPage2.Controls.Add(Me.txtOutputMinutes2)
        Me.TabPage2.Controls.Add(Me.lblOutDeg2)
        Me.TabPage2.Controls.Add(Me.txtOutputDegrees2)
        Me.TabPage2.Controls.Add(Me.txtOutputSign1)
        Me.TabPage2.Controls.Add(Me.txtOutput1)
        Me.TabPage2.Controls.Add(Me.lblOutSec1)
        Me.TabPage2.Controls.Add(Me.txtOutputSeconds1)
        Me.TabPage2.Controls.Add(Me.lblOutMin1)
        Me.TabPage2.Controls.Add(Me.txtOutputMinutes1)
        Me.TabPage2.Controls.Add(Me.lblOutDeg1)
        Me.TabPage2.Controls.Add(Me.txtOutputDegrees1)
        Me.TabPage2.Controls.Add(Me.txtInputSign2)
        Me.TabPage2.Controls.Add(Me.txtInput2)
        Me.TabPage2.Controls.Add(Me.lblInSec2)
        Me.TabPage2.Controls.Add(Me.txtInputSeconds2)
        Me.TabPage2.Controls.Add(Me.lblInMin2)
        Me.TabPage2.Controls.Add(Me.txtInputMinutes2)
        Me.TabPage2.Controls.Add(Me.lblInDeg2)
        Me.TabPage2.Controls.Add(Me.txtInputDegrees2)
        Me.TabPage2.Controls.Add(Me.txtInputSign1)
        Me.TabPage2.Controls.Add(Me.txtInput1)
        Me.TabPage2.Controls.Add(Me.lblInSec1)
        Me.TabPage2.Controls.Add(Me.txtInputSeconds1)
        Me.TabPage2.Controls.Add(Me.lblInMin1)
        Me.TabPage2.Controls.Add(Me.txtInputMinutes1)
        Me.TabPage2.Controls.Add(Me.lblInDeg1)
        Me.TabPage2.Controls.Add(Me.txtInputDegrees1)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.cmbProjnOutputUnits)
        Me.TabPage2.Controls.Add(Me.cmbProjnInputUnits)
        Me.TabPage2.Controls.Add(Me.cmbProjnOutput)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.cmbProjnInput)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.btnUpdateList)
        Me.TabPage2.Controls.Add(Me.cmbProjectedCRS)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(988, 251)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Projections"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnConvertProjection
        '
        Me.btnConvertProjection.Location = New System.Drawing.Point(788, 65)
        Me.btnConvertProjection.Name = "btnConvertProjection"
        Me.btnConvertProjection.Size = New System.Drawing.Size(60, 22)
        Me.btnConvertProjection.TabIndex = 153
        Me.btnConvertProjection.Text = "Convert"
        Me.btnConvertProjection.UseVisualStyleBackColor = True
        '
        'lblOutUnits2
        '
        Me.lblOutUnits2.AutoSize = True
        Me.lblOutUnits2.Location = New System.Drawing.Point(730, 172)
        Me.lblOutUnits2.Name = "lblOutUnits2"
        Me.lblOutUnits2.Size = New System.Drawing.Size(31, 13)
        Me.lblOutUnits2.TabIndex = 152
        Me.lblOutUnits2.Text = "Units"
        '
        'lblOutUnits1
        '
        Me.lblOutUnits1.AutoSize = True
        Me.lblOutUnits1.Location = New System.Drawing.Point(492, 172)
        Me.lblOutUnits1.Name = "lblOutUnits1"
        Me.lblOutUnits1.Size = New System.Drawing.Size(31, 13)
        Me.lblOutUnits1.TabIndex = 151
        Me.lblOutUnits1.Text = "Units"
        '
        'lblInUnits2
        '
        Me.lblInUnits2.AutoSize = True
        Me.lblInUnits2.Location = New System.Drawing.Point(250, 172)
        Me.lblInUnits2.Name = "lblInUnits2"
        Me.lblInUnits2.Size = New System.Drawing.Size(31, 13)
        Me.lblInUnits2.TabIndex = 150
        Me.lblInUnits2.Text = "Units"
        '
        'lblInUnits1
        '
        Me.lblInUnits1.AutoSize = True
        Me.lblInUnits1.Location = New System.Drawing.Point(6, 172)
        Me.lblInUnits1.Name = "lblInUnits1"
        Me.lblInUnits1.Size = New System.Drawing.Size(31, 13)
        Me.lblInUnits1.TabIndex = 149
        Me.lblInUnits1.Text = "Units"
        '
        'lblOutput2
        '
        Me.lblOutput2.AutoSize = True
        Me.lblOutput2.Location = New System.Drawing.Point(730, 89)
        Me.lblOutput2.Name = "lblOutput2"
        Me.lblOutput2.Size = New System.Drawing.Size(47, 13)
        Me.lblOutput2.TabIndex = 148
        Me.lblOutput2.Text = "Northing"
        '
        'lblOutput1
        '
        Me.lblOutput1.AutoSize = True
        Me.lblOutput1.Location = New System.Drawing.Point(492, 89)
        Me.lblOutput1.Name = "lblOutput1"
        Me.lblOutput1.Size = New System.Drawing.Size(42, 13)
        Me.lblOutput1.TabIndex = 147
        Me.lblOutput1.Text = "Easting"
        '
        'lblInput2
        '
        Me.lblInput2.AutoSize = True
        Me.lblInput2.Location = New System.Drawing.Point(250, 89)
        Me.lblInput2.Name = "lblInput2"
        Me.lblInput2.Size = New System.Drawing.Size(54, 13)
        Me.lblInput2.TabIndex = 146
        Me.lblInput2.Text = "Longitude"
        '
        'lblInput1
        '
        Me.lblInput1.AutoSize = True
        Me.lblInput1.Location = New System.Drawing.Point(6, 89)
        Me.lblInput1.Name = "lblInput1"
        Me.lblInput1.Size = New System.Drawing.Size(45, 13)
        Me.lblInput1.TabIndex = 145
        Me.lblInput1.Text = "Latitude"
        '
        'txtOutputSign2
        '
        Me.txtOutputSign2.Location = New System.Drawing.Point(733, 123)
        Me.txtOutputSign2.Name = "txtOutputSign2"
        Me.txtOutputSign2.Size = New System.Drawing.Size(16, 20)
        Me.txtOutputSign2.TabIndex = 144
        '
        'txtOutput2
        '
        Me.txtOutput2.Location = New System.Drawing.Point(733, 149)
        Me.txtOutput2.Name = "txtOutput2"
        Me.txtOutput2.Size = New System.Drawing.Size(158, 20)
        Me.txtOutput2.TabIndex = 143
        '
        'lblOutSec2
        '
        Me.lblOutSec2.AutoSize = True
        Me.lblOutSec2.Location = New System.Drawing.Point(845, 106)
        Me.lblOutSec2.Name = "lblOutSec2"
        Me.lblOutSec2.Size = New System.Drawing.Size(26, 13)
        Me.lblOutSec2.TabIndex = 142
        Me.lblOutSec2.Text = "Sec"
        '
        'txtOutputSeconds2
        '
        Me.txtOutputSeconds2.Location = New System.Drawing.Point(842, 123)
        Me.txtOutputSeconds2.Name = "txtOutputSeconds2"
        Me.txtOutputSeconds2.Size = New System.Drawing.Size(120, 20)
        Me.txtOutputSeconds2.TabIndex = 141
        '
        'lblOutMin2
        '
        Me.lblOutMin2.AutoSize = True
        Me.lblOutMin2.Location = New System.Drawing.Point(801, 106)
        Me.lblOutMin2.Name = "lblOutMin2"
        Me.lblOutMin2.Size = New System.Drawing.Size(24, 13)
        Me.lblOutMin2.TabIndex = 140
        Me.lblOutMin2.Text = "Min"
        '
        'txtOutputMinutes2
        '
        Me.txtOutputMinutes2.Location = New System.Drawing.Point(798, 123)
        Me.txtOutputMinutes2.Name = "txtOutputMinutes2"
        Me.txtOutputMinutes2.Size = New System.Drawing.Size(38, 20)
        Me.txtOutputMinutes2.TabIndex = 139
        '
        'lblOutDeg2
        '
        Me.lblOutDeg2.AutoSize = True
        Me.lblOutDeg2.Location = New System.Drawing.Point(757, 106)
        Me.lblOutDeg2.Name = "lblOutDeg2"
        Me.lblOutDeg2.Size = New System.Drawing.Size(27, 13)
        Me.lblOutDeg2.TabIndex = 138
        Me.lblOutDeg2.Text = "Deg"
        '
        'txtOutputDegrees2
        '
        Me.txtOutputDegrees2.Location = New System.Drawing.Point(754, 123)
        Me.txtOutputDegrees2.Name = "txtOutputDegrees2"
        Me.txtOutputDegrees2.Size = New System.Drawing.Size(39, 20)
        Me.txtOutputDegrees2.TabIndex = 137
        '
        'txtOutputSign1
        '
        Me.txtOutputSign1.Location = New System.Drawing.Point(495, 123)
        Me.txtOutputSign1.Name = "txtOutputSign1"
        Me.txtOutputSign1.Size = New System.Drawing.Size(16, 20)
        Me.txtOutputSign1.TabIndex = 136
        '
        'txtOutput1
        '
        Me.txtOutput1.Location = New System.Drawing.Point(495, 149)
        Me.txtOutput1.Name = "txtOutput1"
        Me.txtOutput1.Size = New System.Drawing.Size(158, 20)
        Me.txtOutput1.TabIndex = 135
        '
        'lblOutSec1
        '
        Me.lblOutSec1.AutoSize = True
        Me.lblOutSec1.Location = New System.Drawing.Point(607, 106)
        Me.lblOutSec1.Name = "lblOutSec1"
        Me.lblOutSec1.Size = New System.Drawing.Size(26, 13)
        Me.lblOutSec1.TabIndex = 134
        Me.lblOutSec1.Text = "Sec"
        '
        'txtOutputSeconds1
        '
        Me.txtOutputSeconds1.Location = New System.Drawing.Point(604, 123)
        Me.txtOutputSeconds1.Name = "txtOutputSeconds1"
        Me.txtOutputSeconds1.Size = New System.Drawing.Size(120, 20)
        Me.txtOutputSeconds1.TabIndex = 133
        '
        'lblOutMin1
        '
        Me.lblOutMin1.AutoSize = True
        Me.lblOutMin1.Location = New System.Drawing.Point(563, 106)
        Me.lblOutMin1.Name = "lblOutMin1"
        Me.lblOutMin1.Size = New System.Drawing.Size(24, 13)
        Me.lblOutMin1.TabIndex = 132
        Me.lblOutMin1.Text = "Min"
        '
        'txtOutputMinutes1
        '
        Me.txtOutputMinutes1.Location = New System.Drawing.Point(560, 123)
        Me.txtOutputMinutes1.Name = "txtOutputMinutes1"
        Me.txtOutputMinutes1.Size = New System.Drawing.Size(38, 20)
        Me.txtOutputMinutes1.TabIndex = 131
        '
        'lblOutDeg1
        '
        Me.lblOutDeg1.AutoSize = True
        Me.lblOutDeg1.Location = New System.Drawing.Point(519, 106)
        Me.lblOutDeg1.Name = "lblOutDeg1"
        Me.lblOutDeg1.Size = New System.Drawing.Size(27, 13)
        Me.lblOutDeg1.TabIndex = 130
        Me.lblOutDeg1.Text = "Deg"
        '
        'txtOutputDegrees1
        '
        Me.txtOutputDegrees1.Location = New System.Drawing.Point(516, 123)
        Me.txtOutputDegrees1.Name = "txtOutputDegrees1"
        Me.txtOutputDegrees1.Size = New System.Drawing.Size(39, 20)
        Me.txtOutputDegrees1.TabIndex = 129
        '
        'txtInputSign2
        '
        Me.txtInputSign2.Location = New System.Drawing.Point(253, 123)
        Me.txtInputSign2.Name = "txtInputSign2"
        Me.txtInputSign2.Size = New System.Drawing.Size(16, 20)
        Me.txtInputSign2.TabIndex = 128
        '
        'txtInput2
        '
        Me.txtInput2.Location = New System.Drawing.Point(253, 149)
        Me.txtInput2.Name = "txtInput2"
        Me.txtInput2.Size = New System.Drawing.Size(158, 20)
        Me.txtInput2.TabIndex = 127
        '
        'lblInSec2
        '
        Me.lblInSec2.AutoSize = True
        Me.lblInSec2.Location = New System.Drawing.Point(365, 106)
        Me.lblInSec2.Name = "lblInSec2"
        Me.lblInSec2.Size = New System.Drawing.Size(26, 13)
        Me.lblInSec2.TabIndex = 126
        Me.lblInSec2.Text = "Sec"
        '
        'txtInputSeconds2
        '
        Me.txtInputSeconds2.Location = New System.Drawing.Point(362, 123)
        Me.txtInputSeconds2.Name = "txtInputSeconds2"
        Me.txtInputSeconds2.Size = New System.Drawing.Size(120, 20)
        Me.txtInputSeconds2.TabIndex = 125
        '
        'lblInMin2
        '
        Me.lblInMin2.AutoSize = True
        Me.lblInMin2.Location = New System.Drawing.Point(321, 106)
        Me.lblInMin2.Name = "lblInMin2"
        Me.lblInMin2.Size = New System.Drawing.Size(24, 13)
        Me.lblInMin2.TabIndex = 124
        Me.lblInMin2.Text = "Min"
        '
        'txtInputMinutes2
        '
        Me.txtInputMinutes2.Location = New System.Drawing.Point(318, 123)
        Me.txtInputMinutes2.Name = "txtInputMinutes2"
        Me.txtInputMinutes2.Size = New System.Drawing.Size(38, 20)
        Me.txtInputMinutes2.TabIndex = 123
        '
        'lblInDeg2
        '
        Me.lblInDeg2.AutoSize = True
        Me.lblInDeg2.Location = New System.Drawing.Point(277, 106)
        Me.lblInDeg2.Name = "lblInDeg2"
        Me.lblInDeg2.Size = New System.Drawing.Size(27, 13)
        Me.lblInDeg2.TabIndex = 122
        Me.lblInDeg2.Text = "Deg"
        '
        'txtInputDegrees2
        '
        Me.txtInputDegrees2.Location = New System.Drawing.Point(274, 123)
        Me.txtInputDegrees2.Name = "txtInputDegrees2"
        Me.txtInputDegrees2.Size = New System.Drawing.Size(39, 20)
        Me.txtInputDegrees2.TabIndex = 121
        '
        'txtInputSign1
        '
        Me.txtInputSign1.Location = New System.Drawing.Point(9, 123)
        Me.txtInputSign1.Name = "txtInputSign1"
        Me.txtInputSign1.Size = New System.Drawing.Size(16, 20)
        Me.txtInputSign1.TabIndex = 120
        '
        'txtInput1
        '
        Me.txtInput1.Location = New System.Drawing.Point(9, 149)
        Me.txtInput1.Name = "txtInput1"
        Me.txtInput1.Size = New System.Drawing.Size(158, 20)
        Me.txtInput1.TabIndex = 119
        '
        'lblInSec1
        '
        Me.lblInSec1.AutoSize = True
        Me.lblInSec1.Location = New System.Drawing.Point(121, 106)
        Me.lblInSec1.Name = "lblInSec1"
        Me.lblInSec1.Size = New System.Drawing.Size(26, 13)
        Me.lblInSec1.TabIndex = 118
        Me.lblInSec1.Text = "Sec"
        '
        'txtInputSeconds1
        '
        Me.txtInputSeconds1.Location = New System.Drawing.Point(118, 123)
        Me.txtInputSeconds1.Name = "txtInputSeconds1"
        Me.txtInputSeconds1.Size = New System.Drawing.Size(120, 20)
        Me.txtInputSeconds1.TabIndex = 117
        '
        'lblInMin1
        '
        Me.lblInMin1.AutoSize = True
        Me.lblInMin1.Location = New System.Drawing.Point(77, 106)
        Me.lblInMin1.Name = "lblInMin1"
        Me.lblInMin1.Size = New System.Drawing.Size(24, 13)
        Me.lblInMin1.TabIndex = 116
        Me.lblInMin1.Text = "Min"
        '
        'txtInputMinutes1
        '
        Me.txtInputMinutes1.Location = New System.Drawing.Point(74, 123)
        Me.txtInputMinutes1.Name = "txtInputMinutes1"
        Me.txtInputMinutes1.Size = New System.Drawing.Size(38, 20)
        Me.txtInputMinutes1.TabIndex = 115
        '
        'lblInDeg1
        '
        Me.lblInDeg1.AutoSize = True
        Me.lblInDeg1.Location = New System.Drawing.Point(33, 106)
        Me.lblInDeg1.Name = "lblInDeg1"
        Me.lblInDeg1.Size = New System.Drawing.Size(27, 13)
        Me.lblInDeg1.TabIndex = 114
        Me.lblInDeg1.Text = "Deg"
        '
        'txtInputDegrees1
        '
        Me.txtInputDegrees1.Location = New System.Drawing.Point(30, 123)
        Me.txtInputDegrees1.Name = "txtInputDegrees1"
        Me.txtInputDegrees1.Size = New System.Drawing.Size(39, 20)
        Me.txtInputDegrees1.TabIndex = 113
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(626, 49)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(31, 13)
        Me.Label13.TabIndex = 104
        Me.Label13.Text = "Units"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(136, 49)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(31, 13)
        Me.Label12.TabIndex = 103
        Me.Label12.Text = "Units"
        '
        'cmbProjnOutputUnits
        '
        Me.cmbProjnOutputUnits.FormattingEnabled = True
        Me.cmbProjnOutputUnits.Location = New System.Drawing.Point(626, 65)
        Me.cmbProjnOutputUnits.Name = "cmbProjnOutputUnits"
        Me.cmbProjnOutputUnits.Size = New System.Drawing.Size(156, 21)
        Me.cmbProjnOutputUnits.TabIndex = 102
        '
        'cmbProjnInputUnits
        '
        Me.cmbProjnInputUnits.FormattingEnabled = True
        Me.cmbProjnInputUnits.Location = New System.Drawing.Point(139, 65)
        Me.cmbProjnInputUnits.Name = "cmbProjnInputUnits"
        Me.cmbProjnInputUnits.Size = New System.Drawing.Size(156, 21)
        Me.cmbProjnInputUnits.TabIndex = 101
        '
        'cmbProjnOutput
        '
        Me.cmbProjnOutput.FormattingEnabled = True
        Me.cmbProjnOutput.Location = New System.Drawing.Point(496, 65)
        Me.cmbProjnOutput.Name = "cmbProjnOutput"
        Me.cmbProjnOutput.Size = New System.Drawing.Size(124, 21)
        Me.cmbProjnOutput.TabIndex = 100
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(493, 49)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(39, 13)
        Me.Label10.TabIndex = 99
        Me.Label10.Text = "Output"
        '
        'cmbProjnInput
        '
        Me.cmbProjnInput.FormattingEnabled = True
        Me.cmbProjnInput.Location = New System.Drawing.Point(9, 65)
        Me.cmbProjnInput.Name = "cmbProjnInput"
        Me.cmbProjnInput.Size = New System.Drawing.Size(124, 21)
        Me.cmbProjnInput.TabIndex = 98
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 49)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(31, 13)
        Me.Label11.TabIndex = 97
        Me.Label11.Text = "Input"
        '
        'btnUpdateList
        '
        Me.btnUpdateList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateList.Location = New System.Drawing.Point(907, 5)
        Me.btnUpdateList.Name = "btnUpdateList"
        Me.btnUpdateList.Size = New System.Drawing.Size(75, 22)
        Me.btnUpdateList.TabIndex = 11
        Me.btnUpdateList.Text = "Update List"
        Me.btnUpdateList.UseVisualStyleBackColor = True
        '
        'cmbProjectedCRS
        '
        Me.cmbProjectedCRS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbProjectedCRS.FormattingEnabled = True
        Me.cmbProjectedCRS.Location = New System.Drawing.Point(211, 6)
        Me.cmbProjectedCRS.Name = "cmbProjectedCRS"
        Me.cmbProjectedCRS.Size = New System.Drawing.Size(690, 21)
        Me.cmbProjectedCRS.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(199, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Projected Coordinate Reference System:"
        '
        'frmUtilities
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1020, 329)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmUtilities"
        Me.Text = "Utilities"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents txtOutputSign As System.Windows.Forms.TextBox
    Friend WithEvents txtInputSign As System.Windows.Forms.TextBox
    Friend WithEvents txtOutputAngle As System.Windows.Forms.TextBox
    Friend WithEvents txtInputAngle As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtOutputSeconds As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtOutputMinutes As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtOutputDegrees As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtInputSeconds As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtInputMinutes As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtInputDegrees As System.Windows.Forms.TextBox
    Friend WithEvents btnConvert As System.Windows.Forms.Button
    Friend WithEvents cmbOutput As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbInput As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnUpdateList As System.Windows.Forms.Button
    Friend WithEvents cmbProjectedCRS As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbProjnOutput As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbProjnInput As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbProjnOutputUnits As System.Windows.Forms.ComboBox
    Friend WithEvents cmbProjnInputUnits As System.Windows.Forms.ComboBox
    Friend WithEvents lblOutput2 As System.Windows.Forms.Label
    Friend WithEvents lblOutput1 As System.Windows.Forms.Label
    Friend WithEvents lblInput2 As System.Windows.Forms.Label
    Friend WithEvents lblInput1 As System.Windows.Forms.Label
    Friend WithEvents txtOutputSign2 As System.Windows.Forms.TextBox
    Friend WithEvents txtOutput2 As System.Windows.Forms.TextBox
    Friend WithEvents lblOutSec2 As System.Windows.Forms.Label
    Friend WithEvents txtOutputSeconds2 As System.Windows.Forms.TextBox
    Friend WithEvents lblOutMin2 As System.Windows.Forms.Label
    Friend WithEvents txtOutputMinutes2 As System.Windows.Forms.TextBox
    Friend WithEvents lblOutDeg2 As System.Windows.Forms.Label
    Friend WithEvents txtOutputDegrees2 As System.Windows.Forms.TextBox
    Friend WithEvents txtOutputSign1 As System.Windows.Forms.TextBox
    Friend WithEvents txtOutput1 As System.Windows.Forms.TextBox
    Friend WithEvents lblOutSec1 As System.Windows.Forms.Label
    Friend WithEvents txtOutputSeconds1 As System.Windows.Forms.TextBox
    Friend WithEvents lblOutMin1 As System.Windows.Forms.Label
    Friend WithEvents txtOutputMinutes1 As System.Windows.Forms.TextBox
    Friend WithEvents lblOutDeg1 As System.Windows.Forms.Label
    Friend WithEvents txtOutputDegrees1 As System.Windows.Forms.TextBox
    Friend WithEvents txtInputSign2 As System.Windows.Forms.TextBox
    Friend WithEvents txtInput2 As System.Windows.Forms.TextBox
    Friend WithEvents lblInSec2 As System.Windows.Forms.Label
    Friend WithEvents txtInputSeconds2 As System.Windows.Forms.TextBox
    Friend WithEvents lblInMin2 As System.Windows.Forms.Label
    Friend WithEvents txtInputMinutes2 As System.Windows.Forms.TextBox
    Friend WithEvents lblInDeg2 As System.Windows.Forms.Label
    Friend WithEvents txtInputDegrees2 As System.Windows.Forms.TextBox
    Friend WithEvents txtInputSign1 As System.Windows.Forms.TextBox
    Friend WithEvents txtInput1 As System.Windows.Forms.TextBox
    Friend WithEvents lblInSec1 As System.Windows.Forms.Label
    Friend WithEvents txtInputSeconds1 As System.Windows.Forms.TextBox
    Friend WithEvents lblInMin1 As System.Windows.Forms.Label
    Friend WithEvents txtInputMinutes1 As System.Windows.Forms.TextBox
    Friend WithEvents lblInDeg1 As System.Windows.Forms.Label
    Friend WithEvents txtInputDegrees1 As System.Windows.Forms.TextBox
    Friend WithEvents lblOutUnits2 As System.Windows.Forms.Label
    Friend WithEvents lblOutUnits1 As System.Windows.Forms.Label
    Friend WithEvents lblInUnits2 As System.Windows.Forms.Label
    Friend WithEvents lblInUnits1 As System.Windows.Forms.Label
    Friend WithEvents btnConvertProjection As System.Windows.Forms.Button
End Class
