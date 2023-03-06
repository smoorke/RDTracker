<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblMem = New System.Windows.Forms.Label()
        Me.txtKey = New System.Windows.Forms.TextBox()
        Me.chkTopmost = New System.Windows.Forms.CheckBox()
        Me.chkMulti = New System.Windows.Forms.CheckBox()
        Me.chkCata = New System.Windows.Forms.CheckBox()
        Me.ttpInfo = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkUgaris = New System.Windows.Forms.CheckBox()
        Me.chkV2 = New System.Windows.Forms.CheckBox()
        Me.btnV2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(5, 100)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(60, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "Ok"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(73, 100)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(55, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblMem
        '
        Me.lblMem.AutoSize = True
        Me.lblMem.Location = New System.Drawing.Point(2, 8)
        Me.lblMem.Name = "lblMem"
        Me.lblMem.Size = New System.Drawing.Size(25, 13)
        Me.lblMem.TabIndex = 3
        Me.lblMem.Text = "Key"
        '
        'txtKey
        '
        Me.txtKey.Location = New System.Drawing.Point(30, 5)
        Me.txtKey.MaxLength = 16
        Me.txtKey.Name = "txtKey"
        Me.txtKey.Size = New System.Drawing.Size(98, 20)
        Me.txtKey.TabIndex = 0
        Me.txtKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkTopmost
        '
        Me.chkTopmost.AutoSize = True
        Me.chkTopmost.Location = New System.Drawing.Point(9, 77)
        Me.chkTopmost.Name = "chkTopmost"
        Me.chkTopmost.Size = New System.Drawing.Size(96, 17)
        Me.chkTopmost.TabIndex = 4
        Me.chkTopmost.Text = "Always on Top"
        Me.ttpInfo.SetToolTip(Me.chkTopmost, "Have Tracker Be Always On Top")
        Me.chkTopmost.UseVisualStyleBackColor = True
        '
        'chkMulti
        '
        Me.chkMulti.AutoSize = True
        Me.chkMulti.Location = New System.Drawing.Point(9, 54)
        Me.chkMulti.Name = "chkMulti"
        Me.chkMulti.Size = New System.Drawing.Size(48, 17)
        Me.chkMulti.TabIndex = 7
        Me.chkMulti.Text = "Multi"
        Me.ttpInfo.SetToolTip(Me.chkMulti, "Will Track Multiple Alts" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Note: Experimental")
        Me.chkMulti.UseVisualStyleBackColor = True
        '
        'chkCata
        '
        Me.chkCata.AutoSize = True
        Me.chkCata.Location = New System.Drawing.Point(66, 31)
        Me.chkCata.Name = "chkCata"
        Me.chkCata.Size = New System.Drawing.Size(48, 17)
        Me.chkCata.TabIndex = 8
        Me.chkCata.Text = "Cata"
        Me.ttpInfo.SetToolTip(Me.chkCata, "Enable CATA Mode")
        Me.chkCata.UseVisualStyleBackColor = True
        '
        'chkUgaris
        '
        Me.chkUgaris.AutoSize = True
        Me.chkUgaris.Location = New System.Drawing.Point(9, 31)
        Me.chkUgaris.Name = "chkUgaris"
        Me.chkUgaris.Size = New System.Drawing.Size(56, 17)
        Me.chkUgaris.TabIndex = 9
        Me.chkUgaris.Text = "Ugaris"
        Me.ttpInfo.SetToolTip(Me.chkUgaris, "Compatability mode for Ugaris Server")
        Me.chkUgaris.UseVisualStyleBackColor = True
        '
        'chkV2
        '
        Me.chkV2.AutoSize = True
        Me.chkV2.Location = New System.Drawing.Point(66, 54)
        Me.chkV2.Name = "chkV2"
        Me.chkV2.Size = New System.Drawing.Size(39, 17)
        Me.chkV2.TabIndex = 10
        Me.chkV2.Text = "V2"
        Me.chkV2.UseVisualStyleBackColor = True
        '
        'btnV2
        '
        Me.btnV2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnV2.Image = Global.RDTracker.My.Resources.Resources.gear_wheel
        Me.btnV2.Location = New System.Drawing.Point(97, 49)
        Me.btnV2.Name = "btnV2"
        Me.btnV2.Size = New System.Drawing.Size(23, 23)
        Me.btnV2.TabIndex = 11
        Me.btnV2.UseVisualStyleBackColor = True
        '
        'frmSettings
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(186, 168)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnV2)
        Me.Controls.Add(Me.chkV2)
        Me.Controls.Add(Me.chkUgaris)
        Me.Controls.Add(Me.chkCata)
        Me.Controls.Add(Me.txtKey)
        Me.Controls.Add(Me.chkMulti)
        Me.Controls.Add(Me.chkTopmost)
        Me.Controls.Add(Me.lblMem)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmSettings"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RDT Settings"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblMem As Label
    Friend WithEvents txtKey As TextBox
    Friend WithEvents chkTopmost As CheckBox
    Friend WithEvents chkMulti As CheckBox
    Friend WithEvents chkCata As CheckBox
    Friend WithEvents ttpInfo As ToolTip
    Friend WithEvents chkUgaris As CheckBox
    Friend WithEvents chkV2 As CheckBox
    Friend WithEvents btnV2 As Button
End Class
