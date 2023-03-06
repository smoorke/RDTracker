
Public Class frmSettings
    Public iSprite As Integer = My.Settings.iSprite
    Public Offset As Point = My.Settings.Offset
    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtKey.Text = My.Settings.Mem

        chkTopmost.Checked = My.Settings.Topmost
        chkMulti.Checked = My.Settings.Multi
        chkCata.Checked = My.Settings.Cata
        chkUgaris.Checked = My.Settings.Ugaris

        chkV2.Checked = My.Settings.V2

        btnV2.Region = New Region(New Rectangle(3, 3, btnV2.Width - 6, btnV2.Height - 6))

    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Try
            My.Settings.Mem = Integer.Parse(txtKey.Text)
        Catch ex As Exception
            MessageBox.Show("Error in key")
            txtKey.Text = My.Settings.Mem
            txtKey.Focus()
            Exit Sub
        End Try

        My.Settings.Multi = chkMulti.Checked
        My.Settings.Topmost = chkTopmost.Checked
        frmMain.TopMost = chkTopmost.Checked

        My.Settings.Ugaris = chkUgaris.Checked

        If Not chkCata.Checked = My.Settings.Cata Then
            frmMain.tmrTick.Enabled = False
            My.Settings.Cata = chkCata.Checked
            frmMain.Maze = New Maze(My.Settings.Cata)
            frmMain.Text = If(My.Settings.Cata, "CATA", "RD") & " Tracker"
            frmMain.tmrTick.Enabled = True
        End If

        My.Settings.V2 = chkV2.Checked
        My.Settings.iSprite = iSprite
        My.Settings.Offset = Offset

        My.Settings.Save()
        Me.Close()
    End Sub

    Private Sub txtKey_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKey.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnV2_Click(sender As Object, e As EventArgs) Handles btnV2.Click
        frmV2.Show()
        frmV2.Focus()

    End Sub
End Class