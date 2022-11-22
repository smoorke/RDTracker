<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.btnReset = New System.Windows.Forms.Button()
        Me.cboAlt = New System.Windows.Forms.ComboBox()
        Me.lblEnter = New System.Windows.Forms.Label()
        Me.tmrTick = New System.Windows.Forms.Timer(Me.components)
        Me.btnSettings = New System.Windows.Forms.Button()
        Me.tmrActive = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'btnReset
        '
        Me.btnReset.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnReset.Location = New System.Drawing.Point(0, 243)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(43, 23)
        Me.btnReset.TabIndex = 0
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'cboAlt
        '
        Me.cboAlt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAlt.FormattingEnabled = True
        Me.cboAlt.Items.AddRange(New Object() {"Someone"})
        Me.cboAlt.Location = New System.Drawing.Point(44, 244)
        Me.cboAlt.Name = "cboAlt"
        Me.cboAlt.Size = New System.Drawing.Size(121, 21)
        Me.cboAlt.TabIndex = 1
        '
        'lblEnter
        '
        Me.lblEnter.AutoSize = True
        Me.lblEnter.ForeColor = System.Drawing.Color.White
        Me.lblEnter.Location = New System.Drawing.Point(171, 248)
        Me.lblEnter.Name = "lblEnter"
        Me.lblEnter.Size = New System.Drawing.Size(41, 13)
        Me.lblEnter.TabIndex = 0
        Me.lblEnter.Text = "Enter 0"
        '
        'tmrTick
        '
        Me.tmrTick.Interval = 50
        '
        'btnSettings
        '
        Me.btnSettings.BackColor = System.Drawing.Color.Transparent
        Me.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSettings.Image = Global.RDTracker.My.Resources.Resources.gear_wheel
        Me.btnSettings.Location = New System.Drawing.Point(220, 242)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(23, 23)
        Me.btnSettings.TabIndex = 3
        Me.btnSettings.UseVisualStyleBackColor = False
        '
        'tmrActive
        '
        Me.tmrActive.Enabled = True
        '
        'frmMain
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.CancelButton = Me.btnReset
        Me.ClientSize = New System.Drawing.Size(244, 266)
        Me.Controls.Add(Me.btnSettings)
        Me.Controls.Add(Me.lblEnter)
        Me.Controls.Add(Me.cboAlt)
        Me.Controls.Add(Me.btnReset)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.ShowIcon = False
        Me.Text = "RD Tracker"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnReset As Button
    Friend WithEvents cboAlt As ComboBox
    Friend WithEvents lblEnter As Label
    Friend WithEvents tmrTick As Timer
    Friend WithEvents btnSettings As Button
    Friend WithEvents tmrActive As Timer
End Class
