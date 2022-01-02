<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewSettings
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
        Me.components = New System.ComponentModel.Container()
        Dim Label3 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Me.tcSettings = New System.Windows.Forms.TabControl()
        Me.tpBasic = New System.Windows.Forms.TabPage()
        Me.txtClass = New System.Windows.Forms.TextBox()
        Me.txtExe = New System.Windows.Forms.TextBox()
        Me.chkSwapXY = New System.Windows.Forms.CheckBox()
        Me.chkCata = New System.Windows.Forms.CheckBox()
        Me.txtKey = New System.Windows.Forms.TextBox()
        Me.chkMulti = New System.Windows.Forms.CheckBox()
        Me.chkTopmost = New System.Windows.Forms.CheckBox()
        Me.lblMem = New System.Windows.Forms.Label()
        Me.tpAdvanced = New System.Windows.Forms.TabPage()
        Me.chkBeepGT = New System.Windows.Forms.CheckBox()
        Me.txtFsprite2Offset = New System.Windows.Forms.TextBox()
        Me.txtFlagsOffset = New System.Windows.Forms.TextBox()
        Me.txtOffsetY = New System.Windows.Forms.TextBox()
        Me.txtOffsetX = New System.Windows.Forms.TextBox()
        Me.txtiSprite = New System.Windows.Forms.TextBox()
        Me.tpColors = New System.Windows.Forms.TabPage()
        Me.lblWarning = New System.Windows.Forms.Label()
        Me.lblText = New System.Windows.Forms.Label()
        Me.cmbColorType = New System.Windows.Forms.ComboBox()
        Me.pbPreview = New System.Windows.Forms.PictureBox()
        Me.btnColorPicker = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.clDialog = New System.Windows.Forms.ColorDialog()
        Me.ttpInfo = New System.Windows.Forms.ToolTip(Me.components)
        Label3 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Me.tcSettings.SuspendLayout()
        Me.tpBasic.SuspendLayout()
        Me.tpAdvanced.SuspendLayout()
        Me.tpColors.SuspendLayout()
        CType(Me.pbPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(128, 35)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(27, 13)
        Label3.TabIndex = 25
        Label3.Text = ".exe"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(15, 57)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(71, 13)
        Label1.TabIndex = 25
        Label1.Text = "fSprite2Offset"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(16, 33)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(57, 13)
        Label2.TabIndex = 23
        Label2.Text = "flagsOffset"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(15, 81)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(49, 13)
        Label4.TabIndex = 22
        Label4.Text = "OffsetXY"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(16, 9)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(36, 13)
        Label5.TabIndex = 21
        Label5.Text = "iSprite"
        '
        'tcSettings
        '
        Me.tcSettings.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tcSettings.Controls.Add(Me.tpBasic)
        Me.tcSettings.Controls.Add(Me.tpAdvanced)
        Me.tcSettings.Controls.Add(Me.tpColors)
        Me.tcSettings.Location = New System.Drawing.Point(0, 0)
        Me.tcSettings.Name = "tcSettings"
        Me.tcSettings.SelectedIndex = 0
        Me.tcSettings.Size = New System.Drawing.Size(176, 155)
        Me.tcSettings.TabIndex = 0
        '
        'tpBasic
        '
        Me.tpBasic.Controls.Add(Label3)
        Me.tpBasic.Controls.Add(Me.txtClass)
        Me.tpBasic.Controls.Add(Me.txtExe)
        Me.tpBasic.Controls.Add(Me.chkSwapXY)
        Me.tpBasic.Controls.Add(Me.chkCata)
        Me.tpBasic.Controls.Add(Me.txtKey)
        Me.tpBasic.Controls.Add(Me.chkMulti)
        Me.tpBasic.Controls.Add(Me.chkTopmost)
        Me.tpBasic.Controls.Add(Me.lblMem)
        Me.tpBasic.Location = New System.Drawing.Point(4, 25)
        Me.tpBasic.Name = "tpBasic"
        Me.tpBasic.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBasic.Size = New System.Drawing.Size(168, 126)
        Me.tpBasic.TabIndex = 0
        Me.tpBasic.Text = "Basic"
        Me.tpBasic.UseVisualStyleBackColor = True
        '
        'txtClass
        '
        Me.txtClass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClass.Location = New System.Drawing.Point(8, 58)
        Me.txtClass.Name = "txtClass"
        Me.txtClass.Size = New System.Drawing.Size(149, 20)
        Me.txtClass.TabIndex = 24
        Me.txtClass.Text = "MAINWNDMOAC | 䅍义乗䵄䅏C | MAINWNDASTONIA"
        Me.ttpInfo.SetToolTip(Me.txtClass, "pipe separted list of windowclass")
        '
        'txtExe
        '
        Me.txtExe.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExe.Location = New System.Drawing.Point(8, 32)
        Me.txtExe.Name = "txtExe"
        Me.txtExe.Size = New System.Drawing.Size(119, 20)
        Me.txtExe.TabIndex = 23
        Me.txtExe.Text = "moac | new | Knights of Astonia"
        Me.ttpInfo.SetToolTip(Me.txtExe, "pipe seperated list of executable names")
        '
        'chkSwapXY
        '
        Me.chkSwapXY.AutoSize = True
        Me.chkSwapXY.Location = New System.Drawing.Point(8, 85)
        Me.chkSwapXY.Name = "chkSwapXY"
        Me.chkSwapXY.Size = New System.Drawing.Size(67, 17)
        Me.chkSwapXY.TabIndex = 20
        Me.chkSwapXY.Text = "SwapXY"
        Me.ttpInfo.SetToolTip(Me.chkSwapXY, "Compatability mode for Ugaris Server")
        Me.chkSwapXY.UseVisualStyleBackColor = True
        '
        'chkCata
        '
        Me.chkCata.AutoSize = True
        Me.chkCata.Location = New System.Drawing.Point(79, 84)
        Me.chkCata.Name = "chkCata"
        Me.chkCata.Size = New System.Drawing.Size(48, 17)
        Me.chkCata.TabIndex = 19
        Me.chkCata.Text = "Cata"
        Me.ttpInfo.SetToolTip(Me.chkCata, "Enable CATA Mode")
        Me.chkCata.UseVisualStyleBackColor = True
        '
        'txtKey
        '
        Me.txtKey.Location = New System.Drawing.Point(36, 6)
        Me.txtKey.MaxLength = 16
        Me.txtKey.Name = "txtKey"
        Me.txtKey.Size = New System.Drawing.Size(121, 20)
        Me.txtKey.TabIndex = 15
        Me.txtKey.Text = "414352"
        Me.txtKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ttpInfo.SetToolTip(Me.txtKey, "Required for basic functionality")
        '
        'chkMulti
        '
        Me.chkMulti.AutoSize = True
        Me.chkMulti.Location = New System.Drawing.Point(8, 108)
        Me.chkMulti.Name = "chkMulti"
        Me.chkMulti.Size = New System.Drawing.Size(48, 17)
        Me.chkMulti.TabIndex = 18
        Me.chkMulti.Text = "Multi"
        Me.ttpInfo.SetToolTip(Me.chkMulti, "Track Multiple Alts")
        Me.chkMulti.UseVisualStyleBackColor = True
        '
        'chkTopmost
        '
        Me.chkTopmost.AutoSize = True
        Me.chkTopmost.Location = New System.Drawing.Point(61, 108)
        Me.chkTopmost.Name = "chkTopmost"
        Me.chkTopmost.Size = New System.Drawing.Size(96, 17)
        Me.chkTopmost.TabIndex = 17
        Me.chkTopmost.Text = "Always on Top"
        Me.ttpInfo.SetToolTip(Me.chkTopmost, "Have Tracker Be Always On Top")
        Me.chkTopmost.UseVisualStyleBackColor = True
        '
        'lblMem
        '
        Me.lblMem.AutoSize = True
        Me.lblMem.Location = New System.Drawing.Point(9, 9)
        Me.lblMem.Name = "lblMem"
        Me.lblMem.Size = New System.Drawing.Size(25, 13)
        Me.lblMem.TabIndex = 16
        Me.lblMem.Text = "Key"
        '
        'tpAdvanced
        '
        Me.tpAdvanced.Controls.Add(Me.chkBeepGT)
        Me.tpAdvanced.Controls.Add(Me.txtFsprite2Offset)
        Me.tpAdvanced.Controls.Add(Label1)
        Me.tpAdvanced.Controls.Add(Me.txtFlagsOffset)
        Me.tpAdvanced.Controls.Add(Label2)
        Me.tpAdvanced.Controls.Add(Me.txtOffsetY)
        Me.tpAdvanced.Controls.Add(Me.txtOffsetX)
        Me.tpAdvanced.Controls.Add(Label4)
        Me.tpAdvanced.Controls.Add(Me.txtiSprite)
        Me.tpAdvanced.Controls.Add(Label5)
        Me.tpAdvanced.Location = New System.Drawing.Point(4, 25)
        Me.tpAdvanced.Name = "tpAdvanced"
        Me.tpAdvanced.Padding = New System.Windows.Forms.Padding(3)
        Me.tpAdvanced.Size = New System.Drawing.Size(168, 126)
        Me.tpAdvanced.TabIndex = 1
        Me.tpAdvanced.Text = "Advanced"
        Me.tpAdvanced.UseVisualStyleBackColor = True
        '
        'chkBeepGT
        '
        Me.chkBeepGT.AutoSize = True
        Me.chkBeepGT.Location = New System.Drawing.Point(50, 104)
        Me.chkBeepGT.Name = "chkBeepGT"
        Me.chkBeepGT.Size = New System.Drawing.Size(101, 17)
        Me.chkBeepGT.TabIndex = 27
        Me.chkBeepGT.Text = "SysBeep on GT"
        Me.chkBeepGT.UseVisualStyleBackColor = True
        '
        'txtFsprite2Offset
        '
        Me.txtFsprite2Offset.Location = New System.Drawing.Point(97, 54)
        Me.txtFsprite2Offset.MaxLength = 16
        Me.txtFsprite2Offset.Name = "txtFsprite2Offset"
        Me.txtFsprite2Offset.Size = New System.Drawing.Size(60, 20)
        Me.txtFsprite2Offset.TabIndex = 26
        Me.txtFsprite2Offset.Text = "-4"
        Me.txtFsprite2Offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFlagsOffset
        '
        Me.txtFlagsOffset.Location = New System.Drawing.Point(97, 30)
        Me.txtFlagsOffset.MaxLength = 16
        Me.txtFlagsOffset.Name = "txtFlagsOffset"
        Me.txtFlagsOffset.Size = New System.Drawing.Size(60, 20)
        Me.txtFlagsOffset.TabIndex = 24
        Me.txtFlagsOffset.Text = "12"
        Me.txtFlagsOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOffsetY
        '
        Me.txtOffsetY.Location = New System.Drawing.Point(121, 78)
        Me.txtOffsetY.MaxLength = 4
        Me.txtOffsetY.Name = "txtOffsetY"
        Me.txtOffsetY.Size = New System.Drawing.Size(36, 20)
        Me.txtOffsetY.TabIndex = 20
        Me.txtOffsetY.Text = "51"
        Me.txtOffsetY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOffsetX
        '
        Me.txtOffsetX.Location = New System.Drawing.Point(80, 78)
        Me.txtOffsetX.MaxLength = 4
        Me.txtOffsetX.Name = "txtOffsetX"
        Me.txtOffsetX.Size = New System.Drawing.Size(36, 20)
        Me.txtOffsetX.TabIndex = 19
        Me.txtOffsetX.Text = "196"
        Me.txtOffsetX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtiSprite
        '
        Me.txtiSprite.Location = New System.Drawing.Point(62, 6)
        Me.txtiSprite.MaxLength = 16
        Me.txtiSprite.Name = "txtiSprite"
        Me.txtiSprite.Size = New System.Drawing.Size(95, 20)
        Me.txtiSprite.TabIndex = 18
        Me.txtiSprite.Text = "669168"
        Me.txtiSprite.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpColors
        '
        Me.tpColors.Controls.Add(Me.lblWarning)
        Me.tpColors.Controls.Add(Me.lblText)
        Me.tpColors.Controls.Add(Me.cmbColorType)
        Me.tpColors.Controls.Add(Me.pbPreview)
        Me.tpColors.Controls.Add(Me.btnColorPicker)
        Me.tpColors.Location = New System.Drawing.Point(4, 25)
        Me.tpColors.Name = "tpColors"
        Me.tpColors.Size = New System.Drawing.Size(168, 126)
        Me.tpColors.TabIndex = 2
        Me.tpColors.Text = "Colors"
        Me.tpColors.UseVisualStyleBackColor = True
        '
        'lblWarning
        '
        Me.lblWarning.AutoSize = True
        Me.lblWarning.Location = New System.Drawing.Point(81, 104)
        Me.lblWarning.Name = "lblWarning"
        Me.lblWarning.Size = New System.Drawing.Size(47, 13)
        Me.lblWarning.TabIndex = 8
        Me.lblWarning.Text = "Warning"
        '
        'lblText
        '
        Me.lblText.AutoSize = True
        Me.lblText.Location = New System.Drawing.Point(44, 104)
        Me.lblText.Name = "lblText"
        Me.lblText.Size = New System.Drawing.Size(25, 13)
        Me.lblText.TabIndex = 7
        Me.lblText.Text = "Info"
        '
        'cmbColorType
        '
        Me.cmbColorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColorType.FormattingEnabled = True
        Me.cmbColorType.Location = New System.Drawing.Point(22, 6)
        Me.cmbColorType.Name = "cmbColorType"
        Me.cmbColorType.Size = New System.Drawing.Size(96, 21)
        Me.cmbColorType.TabIndex = 0
        '
        'pbPreview
        '
        Me.pbPreview.Location = New System.Drawing.Point(44, 33)
        Me.pbPreview.Name = "pbPreview"
        Me.pbPreview.Size = New System.Drawing.Size(84, 84)
        Me.pbPreview.TabIndex = 2
        Me.pbPreview.TabStop = False
        '
        'btnColorPicker
        '
        Me.btnColorPicker.Location = New System.Drawing.Point(124, 5)
        Me.btnColorPicker.Name = "btnColorPicker"
        Me.btnColorPicker.Size = New System.Drawing.Size(23, 23)
        Me.btnColorPicker.TabIndex = 1
        Me.btnColorPicker.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(4, 152)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(43, 23)
        Me.btnReset.TabIndex = 6
        Me.btnReset.Text = "Reset?"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(117, 152)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(55, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(58, 152)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(55, 23)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "Ok"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmNewSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(176, 178)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.tcSettings)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "frmNewSettings"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RDT Settings"
        Me.TopMost = True
        Me.tcSettings.ResumeLayout(False)
        Me.tpBasic.ResumeLayout(False)
        Me.tpBasic.PerformLayout()
        Me.tpAdvanced.ResumeLayout(False)
        Me.tpAdvanced.PerformLayout()
        Me.tpColors.ResumeLayout(False)
        Me.tpColors.PerformLayout()
        CType(Me.pbPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tcSettings As TabControl
    Friend WithEvents tpBasic As TabPage
    Friend WithEvents tpAdvanced As TabPage
    Friend WithEvents tpColors As TabPage
    Friend WithEvents txtClass As TextBox
    Friend WithEvents txtExe As TextBox
    Friend WithEvents chkSwapXY As CheckBox
    Friend WithEvents chkCata As CheckBox
    Friend WithEvents txtKey As TextBox
    Friend WithEvents chkMulti As CheckBox
    Friend WithEvents chkTopmost As CheckBox
    Friend WithEvents lblMem As Label
    Friend WithEvents chkBeepGT As CheckBox
    Friend WithEvents txtFsprite2Offset As TextBox
    Friend WithEvents txtFlagsOffset As TextBox
    Friend WithEvents txtOffsetY As TextBox
    Friend WithEvents txtOffsetX As TextBox
    Friend WithEvents txtiSprite As TextBox
    Friend WithEvents cmbColorType As ComboBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOK As Button
    Friend WithEvents pbPreview As PictureBox
    Friend WithEvents btnColorPicker As Button
    Friend WithEvents clDialog As ColorDialog
    Friend WithEvents btnReset As Button
    Friend WithEvents ttpInfo As ToolTip
    Friend WithEvents lblWarning As Label
    Friend WithEvents lblText As Label
End Class
