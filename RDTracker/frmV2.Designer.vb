<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmV2
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
        Me.grpV2 = New System.Windows.Forms.GroupBox()
        Me.txtOffsetY = New System.Windows.Forms.TextBox()
        Me.txtOffsetX = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtiSprite = New System.Windows.Forms.TextBox()
        Me.lblMem = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.grpV2.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpV2
        '
        Me.grpV2.Controls.Add(Me.txtOffsetY)
        Me.grpV2.Controls.Add(Me.txtOffsetX)
        Me.grpV2.Controls.Add(Me.Label1)
        Me.grpV2.Controls.Add(Me.txtiSprite)
        Me.grpV2.Controls.Add(Me.lblMem)
        Me.grpV2.Location = New System.Drawing.Point(0, 0)
        Me.grpV2.Name = "grpV2"
        Me.grpV2.Size = New System.Drawing.Size(150, 70)
        Me.grpV2.TabIndex = 9
        Me.grpV2.TabStop = False
        Me.grpV2.Text = "V2 - Beta"
        '
        'txtOffsetY
        '
        Me.txtOffsetY.Location = New System.Drawing.Point(100, 45)
        Me.txtOffsetY.MaxLength = 16
        Me.txtOffsetY.Name = "txtOffsetY"
        Me.txtOffsetY.Size = New System.Drawing.Size(47, 20)
        Me.txtOffsetY.TabIndex = 3
        Me.txtOffsetY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOffsetX
        '
        Me.txtOffsetX.Location = New System.Drawing.Point(49, 45)
        Me.txtOffsetX.MaxLength = 16
        Me.txtOffsetX.Name = "txtOffsetX"
        Me.txtOffsetX.Size = New System.Drawing.Size(47, 20)
        Me.txtOffsetX.TabIndex = 2
        Me.txtOffsetX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Offset"
        '
        'txtiSprite
        '
        Me.txtiSprite.Location = New System.Drawing.Point(49, 19)
        Me.txtiSprite.MaxLength = 16
        Me.txtiSprite.Name = "txtiSprite"
        Me.txtiSprite.Size = New System.Drawing.Size(98, 20)
        Me.txtiSprite.TabIndex = 1
        Me.txtiSprite.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMem
        '
        Me.lblMem.AutoSize = True
        Me.lblMem.Location = New System.Drawing.Point(7, 22)
        Me.lblMem.Name = "lblMem"
        Me.lblMem.Size = New System.Drawing.Size(36, 13)
        Me.lblMem.TabIndex = 10
        Me.lblMem.Text = "iSprite"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancel.Location = New System.Drawing.Point(132, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(18, 18)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "X"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnOK.Location = New System.Drawing.Point(115, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(18, 18)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "V"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmV2
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(256, 70)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.grpV2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmV2"
        Me.Text = "RDT V2"
        Me.TopMost = True
        Me.grpV2.ResumeLayout(False)
        Me.grpV2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpV2 As GroupBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents txtOffsetY As TextBox
    Friend WithEvents txtOffsetX As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtiSprite As TextBox
    Friend WithEvents lblMem As Label
    Friend WithEvents btnOK As Button
End Class
