Public Class frmNewSettings

    Dim colors As Dictionary(Of String, Drawing.Color) = loadColors()
    Dim maz As New Maze()

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'basic
        txtKey.Text = My.Settings.PlayerX

        txtExe.Text = My.Settings.exe
        txtClass.Text = My.Settings.className

        chkTopmost.Checked = My.Settings.Topmost
        chkMulti.Checked = My.Settings.Multi
        chkCata.Checked = My.Settings.Cata
        chkSwapXY.Checked = My.Settings.SwapXY

        'advanced
        txtiSprite.Text = My.Settings.iSprite
        txtFlagsOffset.Text = My.Settings.flagsOffset
        txtFsprite2Offset.Text = My.Settings.fSprite2Offset
        txtOffsetX.Text = My.Settings.OffsetXY.X
        txtOffsetY.Text = My.Settings.OffsetXY.Y

        chkBeepGT.Checked = My.Settings.sysBeepOnGT

        'colors

        cmbColorType.Items.AddRange(colors.Keys.ToArray)
        cmbColorType.SelectedIndex = 0

        makeSampleMaze(My.Settings)

        pbPreview.Image = maz.display

        lblText.ForeColor = My.Settings.text
        lblWarning.ForeColor = My.Settings.textwarning
        lblText.BackColor = My.Settings.background
        lblWarning.BackColor = My.Settings.background

    End Sub

    Private Sub makeSampleMaze(settingsobject As My.MySettings)
        '( 2, 2)------------------(22, 2)
        '      |-##### | ####T### |
        '      | # | # | # .--- # |
        '      | = | ##### | C### |  , 8)
        '      | # °----------- # |
        '      | # . T | S   : p# |  ,12)
        '      | # |   .----------|  
        '      |   |
        '      |   .----

        For y = 6 To 10 Step 2
            maz.drawWall(6, y, settingsobject.walls)
        Next

        For y = 4 To 6 Step 2
            maz.drawWall(10, y, settingsobject.walls)
        Next

        For x = 2 To 22 Step 2 'top border
            maz.drawWall(x, 2, settingsobject.walls)
        Next

        For y = 4 To 14 Step 2 'right border
            maz.drawWall(22, y, settingsobject.walls)
        Next

        For y = 4 To 18 Step 2 'left border
            maz.drawWall(2, y, settingsobject.walls)
        Next

        For x = 14 To 18 Step 2
            maz.drawWall(x, 6, settingsobject.walls)
        Next
        For y = 8 To 10 Step 2
            maz.drawWall(14, y, settingsobject.walls)
        Next
        For x = 6 To 18 Step 2
            maz.drawWall(x, 10, settingsobject.walls)
        Next
        maz.drawWall(10, 12, settingsobject.walls)
        For x = 10 To 20 Step 2
            maz.drawWall(x, 14, settingsobject.walls)
        Next
        For y = 14 To 18 Step 2
            maz.drawWall(6, y, settingsobject.walls)
        Next
        For x = 8 To 12 Step 2
            maz.drawWall(x, 18, settingsobject.walls)
        Next

        For y = 14 To 6 Step -2
            maz.plotMaze(4, y, settingsobject.path)
        Next
        For x = 4 To 20 Step 2 'samplepath along top
            If x = 10 Then Continue For
            maz.plotMaze(x, 4, settingsobject.path)
        Next
        maz.plotMaze(8, 6, settingsobject.path)
        For x = 8 To 12 Step 2
            maz.plotMaze(x, 8, settingsobject.path)
        Next
        maz.plotMaze(12, 6, settingsobject.path)
        For y = 6 To 12 Step 2
            maz.plotMaze(20, y, settingsobject.path)
        Next
        maz.plotMaze(16, 8, settingsobject.path)
        maz.plotMaze(18, 8, settingsobject.path)


        maz.drawTD(16, 12, True, True, settingsobject.trapdoor, settingsobject.background, settingsobject.path)
        maz.drawTD(4, 8, False, False, settingsobject.trapdoor, settingsobject.background, settingsobject.path)

        maz.drawGT(8, 11, settingsobject.gastrap)
        maz.drawGT(16, 3, settingsobject.gastrap)
        maz.drawGT(3, 4, settingsobject.gastrap)

        maz.drawTrash(7, 3, settingsobject.junk)
        maz.drawTrash(13, 9, settingsobject.junk)
        maz.drawTrash(9, 16, settingsobject.junk)


        maz.drawShrine(16, 8, Color.Red, settingsobject.shrineoutline)
        maz.drawShrine(12, 12, Color.Blue, settingsobject.shrineoutline)

        maz.Update()

        maz.plotPlayer(20, 6, settingsobject.alts)
        maz.plotPlayer(18, 12, settingsobject.main)
    End Sub

    Function loadColors() As Dictionary(Of String, System.Drawing.Color)
        '"Background", "Path", "Main", "Alts", "Walls", "GasTrap", "TrapDoor", "Junk", "ShrineOutline", "Text", "TextWarning"
        Dim colors As New Dictionary(Of String, Drawing.Color)
        For Each name As String In {"Background", "Path", "Main", "Alts", "Walls", "GasTrap", "TrapDoor", "Junk", "ShrineOutline", "Text", "TextWarning"}
            colors.Add(name, My.Settings(name))
        Next
        Return colors
    End Function
    Private Sub saveColors(settingsobject As My.MySettings)
        For Each name As String In colors.Keys
            settingsobject(name) = colors(name)
        Next
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim playerX, iSprite, offsetX, offsetY, flags, fSprite As Integer

        Try
            playerX = Integer.Parse(txtKey.Text)
        Catch ex As Exception
            MessageBox.Show("Error in Key")
            txtKey.Text = My.Settings.PlayerX
            txtKey.Focus()
            Exit Sub
        End Try
        Try
            iSprite = Integer.Parse(txtiSprite.Text)
        Catch ex As Exception
            MessageBox.Show("Error in iSprite")
            txtiSprite.Text = My.Settings.iSprite
            txtiSprite.Focus()
            Exit Sub
        End Try
        Try
            flags = Integer.Parse(txtFlagsOffset.Text)
        Catch ex As Exception
            MessageBox.Show("Error in flags")
            txtFlagsOffset.Text = My.Settings.flagsOffset
            txtFlagsOffset.Focus()
            Exit Sub
        End Try
        Try
            fSprite = Integer.Parse(txtFsprite2Offset.Text)
        Catch ex As Exception
            MessageBox.Show("Error in fSprite")
            txtFsprite2Offset.Text = My.Settings.fSprite2Offset
            txtFsprite2Offset.Focus()
            Exit Sub
        End Try
        Try
            offsetX = Integer.Parse(txtOffsetX.Text)
        Catch ex As Exception
            MessageBox.Show("Error in offsetX")
            txtOffsetX.Text = My.Settings.OffsetXY.X
            txtOffsetX.Focus()
            Exit Sub
        End Try
        Try
            offsetY = Integer.Parse(txtOffsetY.Text)
        Catch ex As Exception
            MessageBox.Show("Error in offsetY")
            txtOffsetY.Text = My.Settings.OffsetXY.Y
            txtOffsetY.Focus()
            Exit Sub
        End Try

        saveColors(My.Settings)
        frmMain.BackColor = My.Settings.background
        frmMain.lblEnter.ForeColor = My.Settings.text
        frmMain.lblEnter.BackColor = My.Settings.background

        My.Settings.PlayerX = playerX
        My.Settings.iSprite = iSprite
        My.Settings.flagsOffset = flags
        My.Settings.fSprite2Offset = fSprite
        My.Settings.OffsetXY = New Point(offsetX, offsetY)

        My.Settings.sysBeepOnGT = chkBeepGT.Checked

        My.Settings.Multi = chkMulti.Checked
        My.Settings.Topmost = chkTopmost.Checked
        frmMain.TopMost = chkTopmost.Checked

        My.Settings.SwapXY = chkSwapXY.Checked

        If Not chkCata.Checked = My.Settings.Cata Then
            frmMain.tmrTick.Enabled = False
            My.Settings.Cata = chkCata.Checked
            frmMain.Maze = New Maze(My.Settings.Cata)
            frmMain.Text = If(My.Settings.Cata, "CATA", "RD") & " Tracker"
            frmMain.tmrTick.Enabled = True
        End If

        My.Settings.exe = txtExe.Text
        My.Settings.className = txtClass.Text

        My.Settings.Save()
        Me.Close()
    End Sub
    Private Sub txtPosValues_KeyPress(sender As Object, e As KeyPressEventArgs) Handles _
            txtKey.KeyPress, txtiSprite.KeyPress, txtOffsetX.KeyPress, txtOffsetY.KeyPress
        If Not (Char.IsDigit(e.KeyChar) OrElse Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtValues_KeyPress(sender As Object, e As KeyPressEventArgs) Handles _
            txtFlagsOffset.KeyPress, txtFsprite2Offset.KeyPress
        If Not (Char.IsDigit(e.KeyChar) OrElse Char.IsControl(e.KeyChar) OrElse e.KeyChar = "-") Then
            e.Handled = True
        End If
        If e.KeyChar = "-" Then
            Try
                Integer.Parse(sender.Text.Insert(sender.SelectionStart, "-"))
            Catch
                If sender.Text <> "" Then e.Handled = True
            End Try
        End If
    End Sub
    Private Sub updateColorButton(col As Drawing.Color)
        Dim bm As New Bitmap(16, 16)
        Using g As Graphics = Graphics.FromImage(bm)
            g.Clear(col)
        End Using
        btnColorPicker.Image = bm
        colors(cmbColorType.SelectedItem) = col
        Dim dummysettings As New My.MySettings
        saveColors(dummysettings)
        maz.Clear(dummysettings.background)
        makeSampleMaze(dummysettings)
        pbPreview.Image = maz.display
        lblText.BackColor = dummysettings.background
        lblWarning.BackColor = dummysettings.background
        lblText.ForeColor = dummysettings.text
        lblWarning.ForeColor = dummysettings.textwarning
        dummysettings = Nothing
    End Sub
    Private Sub cmbColorType_SelectedIndexChanged(sender As ComboBox, e As EventArgs) Handles cmbColorType.SelectedIndexChanged
        Debug.Print($"color:{sender.SelectedItem}:{Hex(colors(sender.SelectedItem).ToArgb And &HFFFFFF)}")
        updateColorButton(colors(sender.SelectedItem))
    End Sub
    Private Sub btnColorPicker_Click(sender As Object, e As EventArgs) Handles btnColorPicker.Click
        clDialog.Color = colors(cmbColorType.SelectedItem)
        If clDialog.ShowDialog() = DialogResult.OK Then
            updateColorButton(clDialog.Color)
        End If
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If MessageBox.Show("Reset All Settings To Default?", "Reset Settings", MessageBoxButtons.OKCancel) = DialogResult.OK Then
            My.Settings.Reset()
            colors = loadColors()
            btnOK.PerformClick()
        End If
    End Sub
End Class