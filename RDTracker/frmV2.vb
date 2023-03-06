Public Class frmV2
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Left = frmSettings.Left + frmSettings.btnV2.Left + 20
        Me.Top = frmSettings.Top + frmSettings.btnV2.Top
        Me.Width = 150

        txtiSprite.Text = frmSettings.iSprite
        txtOffsetX.Text = frmSettings.Offset.X
        txtOffsetY.Text = frmSettings.Offset.Y

        txtiSprite.Focus()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click



        Try
            frmSettings.iSprite = Integer.Parse(txtiSprite.Text)
        Catch ex As Exception
            MessageBox.Show("Error in iSprite")
            txtiSprite.Text = My.Settings.iSprite
            txtiSprite.Focus()
            Exit Sub
        End Try

        Try
            frmSettings.Offset = New Point(Integer.Parse(txtOffsetX.Text), Integer.Parse(txtOffsetY.Text))
        Catch ex As Exception
            MessageBox.Show("Error in Offset")
            txtOffsetX.Text = My.Settings.Offset.X
            txtOffsetY.Text = My.Settings.Offset.Y
            txtOffsetX.Focus()
            Exit Sub
        End Try

        Me.Close()
    End Sub
End Class