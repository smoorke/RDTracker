Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Configuration
Imports System.IO.MemoryMappedFiles

Public Class frmMain

    Private _memManager As New MemoryManager


    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Sub GetClassName(ByVal hWnd As System.IntPtr, ByVal lpClassName As System.Text.StringBuilder, ByVal nMaxCount As Integer)
    End Sub

    Public Function GetWindowClass(ByVal hwnd As Long) As String
        Dim sClassName As New System.Text.StringBuilder("", 256)
        Call GetClassName(hwnd, sClassName, 256)
        Return sClassName.ToString
    End Function

    Public Maze = New Maze(My.Settings.Cata)


    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If My.Settings.SingleInstance AndAlso IPC.AlreadyOpen Then
            IPC.RequestActivation = True
            End
        End If
        IPC.AlreadyOpen = True

        Me.Location = My.Settings.Location
        Me.TopMost = My.Settings.Topmost

        Me.BackgroundImage = Maze.display
        Me.lblEnter.BackColor = My.Settings.background
        Me.BackColor = My.Settings.background

        btnSettings.Region = New Region(New Rectangle(3, 3, btnSettings.Width - 6, btnSettings.Height - 6))

        Dim strAltName As String = ""
        Dim intFound As Integer = 0
        For Each p As Process In listProcesses()
            strAltName = Strings.Left(p.MainWindowTitle, p.MainWindowTitle.IndexOf(" - "))
            If strAltName <> "Someone" Then
                cboAlt.Items.Add(strAltName)
                intFound += 1
            End If
        Next

        Dim args() As String = Environment.GetCommandLineArgs().Skip(1).ToArray

        If args.Count > 0 Then
            For Each arg In args
                If cboAlt.Items.Contains(arg) Then
                    cboAlt.SelectedItem = arg
                    Exit For
                End If
            Next
        Else
            cboAlt.SelectedIndex = If(intFound > 0, 1, 0)
        End If

        Me.Text = If(My.Settings.Cata, "CATA", "RD") & " Tracker"

    End Sub
    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If Not Screen.AllScreens.Any(Function(s) s.WorkingArea.Contains(Me.Location)) Then
            Me.Location = Screen.PrimaryScreen.WorkingArea.Location
        End If
    End Sub
    Private Sub cboAlt_DropDown(sender As Object, e As EventArgs) Handles cboAlt.DropDown
        Dim lstAst As New List(Of String)
        Dim Name As String = ""
        lstAst.Add("Someone")
        For Each p As Process In listProcesses()
            If p.MainWindowTitle IsNot Nothing AndAlso
             Not p.MainWindowTitle.StartsWith("Someone") Then
                Name = Strings.Left(p.MainWindowTitle, p.MainWindowTitle.IndexOf(" - "))
                lstAst.Add(Name)
                If Not cboAlt.Items.Contains(Name) Then
                    cboAlt.Items.Add(Name)
                End If
            End If
        Next

        '    clean ComboBox of idled clients
        Dim i As Integer = 1
        Do While i < cboAlt.Items.Count
            If Not lstAst.Contains(cboAlt.Items(i)) Then
                If cboAlt.SelectedIndex = i Then
                    cboAlt.SelectedIndex = 0
                End If
                cboAlt.Items.RemoveAt(i)
                i -= 1
            End If
            i += 1
        Loop

    End Sub
    Private strSock As String
    Private _mmf As MemoryMappedFile
    Private _mmva As MemoryMappedViewAccessor
    Private isSDL As Boolean = False
    Private Sub cboAlt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAlt.SelectedIndexChanged

        tmrTick.Enabled = False
        _memManager.DetachFromProcess()
        If cboAlt.SelectedIndex <> 0 Then
            If _memManager.TryAttachToProcess(cboAlt.SelectedItem & " - ", My.Settings.exe.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)) Then
                strSock = Strings.Right(_memManager.targetProcess.MainWindowTitle, 8)
                wasArea = False
                _mmf = MemoryMappedFile.CreateOrOpen($"MOAC{_memManager.targetProcess.Id}", Marshal.SizeOf(GetType(MoacSharedMem)))
                _mmva = _mmf.CreateViewAccessor()
                _mmva.Read(0, shm)
                If shm.pID = _memManager.targetProcess.Id Then
                    isSDL = True
                Else
                    isSDL = False
                End If
                tmrTick.Enabled = True
            End If

        End If

    End Sub
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto, Pack:=0)>
    Structure MoacSharedMem
        Dim pID As UInt32
        Dim hp, shield, [end], mana As Byte
        Dim base As UInt64
        Dim key, isprite, offX, offY As Integer
        Dim flags, fsprite As Integer
        Dim swapped As Byte
    End Structure
    Private shm As MoacSharedMem
    'Dim brushWhite As New SolidBrush(Color.White)
    'Dim brushRed As New SolidBrush(Color.Red)
    'Dim brushCyan As New SolidBrush(Color.Cyan)
    Dim ptZero As New Point(0, 0)
    Dim prevP As New Point(0, 0)
    Dim gameX, gameY As Integer
    Dim wasArea As Boolean = False
    Private reader As New MemoryManager
    Private Function readMemPoint(pp As Process) As Point
        If pp.HasExited Then Return New Point(0, 0)
        Dim mmf As MemoryMappedFile = MemoryMappedFile.CreateOrOpen($"MOAC{pp.Id}", Marshal.SizeOf(GetType(MoacSharedMem)))
        Dim mmva As MemoryMappedViewAccessor = mmf.CreateViewAccessor()
        Dim gameX, gameY As Integer
        Try
            reader.TryAttachToProcess(pp)
            Dim mshm As MoacSharedMem

            mmva.Read(0, mshm)
            Dim SDL As Boolean = False
            If mshm.pID = pp.Id Then
                SDL = True
            End If
            If SDL Then
                If mshm.swapped = 0 Then
                    gameX = reader.ReadIntWoW64(mshm.base + mshm.key)
                    gameY = reader.ReadIntWoW64(mshm.base + mshm.key + 4)
                Else
                    gameY = reader.ReadIntWoW64(mshm.base + mshm.key) 'note: Ugaris has X and Y swapped in memory
                    gameX = reader.ReadIntWoW64(mshm.base + mshm.key + 4)
                End If
            Else
                If Not My.Settings.SwapXY Then
                    gameX = reader.ReadInt32(My.Settings.PlayerX, True)
                    gameY = reader.ReadInt32(My.Settings.PlayerX + 4, True)
                Else
                    gameY = reader.ReadInt32(My.Settings.PlayerX, True) 'note: Ugaris has X and Y swapped in memory
                    gameX = reader.ReadInt32(My.Settings.PlayerX + 4, True)
                End If

            End If
        Catch
            Return New Point(0, 0)
        Finally
            mmva.Dispose()
            mmf.Dispose()
            reader.DetachFromProcess()
        End Try

        Return New Point(gameX, gameY)

    End Function

    Dim mainRdNum As Integer = 0
    Dim loopCount As Integer = 0
    Dim lstAproc As List(Of Process) = listProcesses()

    Private Sub tmrTick_Tick(sender As Object, e As EventArgs) Handles tmrTick.Tick
        If cboAlt.SelectedIndex = 0 Then
            tmrTick.Enabled = False
            Exit Sub
        End If
        If isSDL Then
            If shm.swapped = 0 Then
                gameX = _memManager.ReadIntWoW64(shm.base + shm.key + 4)
                gameY = _memManager.ReadIntWoW64(shm.base + shm.key)
            Else
                gameY = _memManager.ReadIntWoW64(shm.base + shm.key)
                gameX = _memManager.ReadIntWoW64(shm.base + shm.key + 4)
            End If
        Else
            If Not My.Settings.SwapXY Then
                gameX = _memManager.ReadInt32(My.Settings.PlayerX, True)
                gameY = _memManager.ReadInt32(My.Settings.PlayerX + 4, True)
            Else
                gameY = _memManager.ReadInt32(My.Settings.PlayerX, True)
                gameX = _memManager.ReadInt32(My.Settings.PlayerX + 4, True)
            End If
        End If

        'Debug.Print($"xy:{gameX}/{gameY}")


        If wasArea AndAlso Not Maze.isLobby(gameX, gameY) Then
            ' hack to prevent messing up map when changing area
            Exit Sub
        End If
        If gameX <= 0 OrElse gameY <= 0 OrElse gameX >= 255 OrElse gameY >= 255 Then
            'error in reading gamecoords
            lblEnter.Text = "Error " & mainRdNum
            prevP = ptZero
            Exit Sub
        End If
        _memManager.targetProcess.Refresh()
        If Not _memManager.targetProcess.MainWindowTitle.Contains(strSock) Then
            lblEnter.Text = "Area " & mainRdNum
            wasArea = True
            Exit Sub
        End If
        If Maze.isLobby(gameX, gameY) Then
            lblEnter.Text = "Lobby " & mainRdNum
            prevP = ptZero
            wasArea = False
            Exit Sub
        End If
        If Maze.isYendor(gameX, gameY) Then
            lblEnter.Text = "Yendor " & mainRdNum
            prevP = ptZero
            Exit Sub
        End If

        mainRdNum = Maze.getNum(gameX, gameY)

        lblEnter.Text = "Enter " & mainRdNum
        Dim newP As New Point(gameX, gameY)
        If prevP <> ptZero AndAlso prevP <> newP Then
            Maze.plotMaze(prevP.X, prevP.Y, My.Settings.path)
        End If
        prevP = newP



        Dim radius As Integer = 8
        'If My.Settings.V2 Then
        Dim gtInRange As Boolean = False
        For dX = -radius To radius
            Dim cX As Integer = prevP.X + dX
            If Not isValidMapUnit(cX) Then
                Continue For
            End If
            For dY = -radius To radius
                Dim cY As Integer = prevP.Y + dY
                If Not isValidMapUnit(cY) Then
                    Continue For
                End If
                Dim offset As Integer
                If isSDL Then
                    offset = (shm.offX * dX) + (shm.offX * shm.offY * dY)
                Else
                    offset = (My.Settings.OffsetXY.X * dX) + (My.Settings.OffsetXY.X * My.Settings.OffsetXY.Y * dY)
                End If

                'Dim gsprite As Integer = _memManager.ReadInt32(My.Settings.iSprite - 16 + offset)
                'Dim gsprite2 As Integer = _memManager.ReadInt32(My.Settings.iSprite - 12 + offset)
                'Dim fsprite As Integer = _memManager.ReadInt32(My.Settings.iSprite - 8 + offset)
                Dim isprite As Integer
                Dim flags As Integer
                Dim fsprite2 As Integer
                If isSDL Then
                    isprite = _memManager.ReadIntWoW64(shm.base + shm.isprite + offset)
                    flags = _memManager.ReadIntWoW64(shm.base + shm.isprite + offset + shm.flags)
                    fsprite2 = _memManager.ReadIntWoW64(shm.base + shm.isprite + offset + shm.fsprite)
                    If dX = 0 AndAlso dY = 0 Then
                        Debug.Print($"isprite {isprite}")
                    End If
                Else
                    isprite = _memManager.ReadInt32(My.Settings.iSprite + offset, True)
                    flags = _memManager.ReadInt32(My.Settings.iSprite + offset + My.Settings.flagsOffset, True) ' 12
                    fsprite2 = _memManager.ReadInt32(My.Settings.iSprite + offset + My.Settings.fSprite2Offset, True) '-4
                End If

                If (flags And &H10) = 0 Then Continue For 'not visible


                If fsprite2 <> 0 AndAlso sameSpot(prevP) Then
                    Select Case fsprite2
                        Case 15291, 15300
                            If Not Maze.hasGT(cX, cY) Then
                                If My.Settings.sysBeepOnGT Then Beep()
                                Maze.drawGT(cX, cY, My.Settings.gastrap)
                            End If
                            gtInRange = True
                        Case Is < 6553600 'draw wall?
                            Maze.drawWall(cX, cY, My.Settings.walls)
                    End Select
                End If
                If isprite <> 0 AndAlso sameSpot(prevP) Then
                    Select Case isprite
                        Case 15272 'trapdoor netosw open
                            Maze.drawTD(cX, cY, True, True, My.Settings.trapdoor, My.Settings.background, My.Settings.path)
                        Case 15274 'trapdoor netosw closed
                            Maze.drawTD(cX, cY, True, False, My.Settings.trapdoor, My.Settings.background, My.Settings.path)
                        Case 15275 'trapdoor nwtose open
                            Maze.drawTD(cX, cY, False, True, My.Settings.trapdoor, My.Settings.background, My.Settings.path)
                        Case 15277 'trapdoor nwtose closed
                            Maze.drawTD(cX, cY, False, False, My.Settings.trapdoor, My.Settings.background, My.Settings.path)
                        Case 15281 To 15290 ' trash
                            Maze.drawTrash(cX, cY, My.Settings.junk)
                        Case 58216 ' Valor
                            Maze.drawShrine(cX, cY, Color.LightBlue, My.Settings.shrineoutline)
                        Case 59391 ' cont
                            Maze.drawShrine(cX, cY, Color.Red, My.Settings.shrineoutline)
                        Case 59392 ' indi
                            Maze.drawShrine(cX, cY, Color.LimeGreen, My.Settings.shrineoutline)
                        Case 59393 ' bribe
                            Maze.drawShrine(cX, cY, Color.BlueViolet, My.Settings.shrineoutline)
                        Case 59394 ' Weld
                            Maze.drawShrine(cX, cY, Color.Yellow, My.Settings.shrineoutline)
                        Case 59395 ' LOE
                            Maze.drawShrine(cX, cY, Color.LimeGreen, My.Settings.shrineoutline)
                        Case 59396 ' Kindness
                            Maze.drawShrine(cX, cY, Color.LimeGreen, My.Settings.shrineoutline)
                        Case 59397 ' Vit
                            Maze.drawShrine(cX, cY, Color.LimeGreen, My.Settings.shrineoutline)
                        Case 59398 ' brave
                            Maze.drawShrine(cX, cY, Color.LimeGreen, My.Settings.shrineoutline)
                        Case 59399 ' Sec
                            Maze.drawShrine(cX, cY, Color.BlueViolet, My.Settings.shrineoutline)
                        Case 59400, 59401 ' Jobless
                            Maze.drawShrine(cX, cY, Color.BlueViolet, My.Settings.shrineoutline)
                        Case 59403 ' Death
                            Maze.drawShrine(cX, cY, Color.Black, My.Settings.shrineoutline)

                    End Select
                End If
            Next
        Next
        If gtInRange Then
            lblEnter.ForeColor = My.Settings.textwarning
        Else
            lblEnter.ForeColor = My.Settings.text
        End If




        Maze.Update()

        If My.Settings.Multi Then
            If loopCount > (1500 \ tmrTick.Interval) Then
                ' only update list evey 1.5 seconds
                lstAproc = listProcesses()
                loopCount = 0
            Else
                loopCount += 1
            End If

            Dim altP As Point
            Dim altNum As Integer
            For Each pp As Process In lstAproc
                If Not pp.isRunning Then
                    Continue For
                End If
                If pp.MainWindowTitle IsNot Nothing AndAlso
                   pp.Id <> _memManager.targetProcess.Id AndAlso
                   Not pp.MainWindowTitle.StartsWith("Someone") AndAlso
                   pp.MainWindowTitle.Contains(strSock) Then
                    altP = readMemPoint(pp)
                    altNum = Maze.getNum(altP.X, altP.Y)
                    If altP <> ptZero AndAlso
                       altNum = mainRdNum Then
                        Maze.plotMaze(altP.X, altP.Y, My.Settings.path)
                        Maze.plotPlayer(altP.X, altP.Y, My.Settings.alts)
                    End If
                End If
            Next
        End If

        Maze.plotPlayer(gameX, gameY, My.Settings.main)
        Me.BackgroundImage = Maze.display
        Me.Refresh()

    End Sub

    Private Function sameSpot(prevP As Point)
        If isSDL Then
            If shm.swapped Then
                If prevP.Y = _memManager.ReadIntWoW64(shm.base + shm.key) AndAlso prevP.X = _memManager.ReadIntWoW64(shm.base + shm.key + 4) Then
                    Return True
                End If
            Else
                If prevP.X = _memManager.ReadIntWoW64(shm.base + shm.key) AndAlso prevP.Y = _memManager.ReadInt32(shm.base + shm.key + 4) Then
                    Return True
                End If
            End If
            Return False
        End If
        If My.Settings.SwapXY Then
            If prevP.Y = _memManager.ReadInt32(My.Settings.PlayerX, True) AndAlso prevP.X = _memManager.ReadInt32(My.Settings.PlayerX + 4, True) Then
                Return True
            End If
        Else
            If prevP.X = _memManager.ReadInt32(My.Settings.PlayerX, True) AndAlso prevP.Y = _memManager.ReadInt32(My.Settings.PlayerX + 4, True) Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Function isValidMapUnit(unit As Integer) As Boolean
        If unit <= 0 OrElse unit >= 255 Then
            Return False
        End If
        Return True
    End Function
    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        frmNewSettings.Show()
        frmNewSettings.Focus()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        tmrTick.Enabled = False
        _memManager.DetachFromProcess()
        If cboAlt.SelectedIndex <> 0 Then
            Dim exeNames() As String = My.Settings.exe.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
            If _memManager.TryAttachToProcess(cboAlt.SelectedItem & " -", exeNames) Then
                strSock = Strings.Right(_memManager.targetProcess.MainWindowTitle, 8)
            End If
        End If
        lblEnter.Text = "Enter 0"
        mainRdNum = 0
        prevP = ptZero
        wasArea = False
        Maze.Clear(My.Settings.background)
        Me.BackColor = My.Settings.background
        lblEnter.ForeColor = My.Settings.text
        lblEnter.BackColor = My.Settings.background
        tmrTick.Enabled = True
        lblEnter.Focus()

        If cboAlt.SelectedIndex <> 0 Then
            Try
                AppActivate(CType(_memManager.targetProcess?.Id, Integer))
            Catch
            End Try
        End If

        Me.Refresh()
    End Sub

    Private Sub frmMain_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Me.WindowState = FormWindowState.Normal Then
            My.Settings.Location = Me.Location
        End If
    End Sub

    Public Sub runAsAdmin()
        tmrTick.Enabled = False
        Dim procStartInfo As New ProcessStartInfo With {
            .UseShellExecute = True,
            .FileName = Environment.GetCommandLineArgs()(0),
            .Arguments = """" & Me.cboAlt.SelectedItem & """",
            .WindowStyle = ProcessWindowStyle.Normal,
            .Verb = "runas" 'add this to prompt for elevation
        }

        If Me.WindowState = FormWindowState.Normal Then
            My.Settings.Location = Me.Location
        End If
        My.Settings.Save()

        Try
            Process.Start(procStartInfo).WaitForInputIdle()
        Catch e As System.ComponentModel.Win32Exception
            'operation cancelled
        Catch e As InvalidOperationException
            'wait for inputidle is needed
        Catch e As Exception
            Throw e
        End Try

    End Sub

    Public Function listProcesses() As List(Of Process)
        Dim lst As List(Of Process) = New List(Of Process)()
        For Each pp As Process In ListProcessesByNameArray(My.Settings.exe.Split({"|"}, StringSplitOptions.RemoveEmptyEntries))
            If pp.MainWindowTitle IsNot Nothing AndAlso
             Not pp.MainWindowTitle.StartsWith("Someone") AndAlso
             isAstoniaClass(pp) Then
                lst.Add(pp)
            End If
        Next
        Return lst
    End Function
    Public Function ListProcessesByNameArray(exes() As String) As List(Of Process)
        Dim list As List(Of Process) = New List(Of Process)
        For Each exe As String In exes
            list.AddRange(Process.GetProcessesByName(Trim(exe)))
        Next
        Return list
    End Function

    Private Async Sub tmrActive_Tick(sender As Object, e As EventArgs) Handles tmrActive.Tick
        If IPC.RequestActivation Then
            IPC.RequestActivation = 0
            Debug.Print("IPC.requestActivation")

            If Me.WindowState = FormWindowState.Minimized Then
                Const WM_SYSCOMMAND = &H112
                Const SC_RESTORE = &HF120
                WndProc(Message.Create(Me.Handle, WM_SYSCOMMAND, SC_RESTORE, IntPtr.Zero))
            End If

            Me.TopMost = True
            Me.BringToFront()
            Await Task.Delay(100)
            Me.TopMost = My.Settings.Topmost

            'Me.Activate()

        End If
    End Sub

    'Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    If frmSettings.Visible = True Then frmSettings.btnOK.PerformClick()
    '    Me.TopMost = False

    '    Dim name As String = InputBox("Choose a name to save current settings as", "Save Settings", My.Settings.SelectedConfig)

    '    If name = "" Then
    '        Me.TopMost = My.Settings.Topmost
    '        Exit Sub
    '    End If
    '    For Each kar As Char In name.ToUpper

    '        If Not "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 _".Contains(kar) Then
    '            MessageBox.Show("Illegal charater """ & kar & """")
    '            Me.TopMost = My.Settings.Topmost
    '            Exit Sub
    '        End If
    '    Next



    '    If System.IO.File.Exists(configDir & "\" & name & ".RDT.config") AndAlso
    '       MessageBox.Show("""" & name & """ config already exists. Overwrite?", "Notice", MessageBoxButtons.OKCancel) = DialogResult.Cancel Then
    '        Me.TopMost = My.Settings.Topmost
    '        Exit Sub
    '    End If

    '    My.Settings.SelectedConfig = name
    '    My.Settings.Save()

    '    FileIO.FileSystem.CopyFile(configDir & "\user.config", configDir & "\" & name & ".RDT.config", True)

    '    Me.TopMost = My.Settings.Topmost

    'End Sub

    'Dim configDir = IO.Path.GetDirectoryName(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath)
    'Private Sub ImportToolStripMenuItem_Click(sender As Object, e As EventArgs)

    '    Debug.Print(configDir)
    '    Using dialog As New OpenFileDialog With {
    '        .Title = "Import Settings - RDT",
    '        .InitialDirectory = Environment.SpecialFolder.Desktop,
    '        .Multiselect = True,
    '        .DefaultExt = ".RDT.config",
    '        .CheckFileExists = True,
    '        .CheckPathExists = True,
    '        .Filter = "RDT Config Files (*.RDT.config)|*.RDT.config|All Files (*.*)|*.*",
    '        .FilterIndex = 0
    '        }
    '        If dialog.ShowDialog() <> DialogResult.OK Then Exit Sub

    '        importFiles(dialog.FileNames)
    '    End Using
    'End Sub
    'Private Sub importFiles(files() As String, Optional silent As Boolean = False)
    '    Dim validCount As Integer = 0
    '    Dim validSingleName As String = ""
    '    For Each file In files

    '        If System.IO.Path.GetExtension(file) <> ".config" Then Continue For
    '        If file = configDir & "user.config" Then Continue For

    '        If testConfig(file) Then
    '            FileIO.FileSystem.CopyFile(file, configDir & "\import\" & System.IO.Path.GetFileName(file), True)
    '            validCount += 1
    '            validSingleName = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(file))
    '        End If
    '    Next
    '    Try
    '        My.Computer.FileSystem.CopyDirectory(configDir & "\import", configDir, If(Not silent, FileIO.UIOption.AllDialogs, FileIO.UIOption.OnlyErrorDialogs))
    '    Catch
    '        validCount = 0
    '    Finally
    '        If FileIO.FileSystem.DirectoryExists(configDir & "\import") Then
    '            FileIO.FileSystem.DeleteDirectory(configDir & "\import", FileIO.DeleteDirectoryOption.DeleteAllContents)
    '        End If
    '    End Try
    '    If validCount = 1 Then
    '        LoadConfigToolStripMenuItem_Click(New ToolStripMenuItem With {.Tag = configDir & "\" & validSingleName & ".RDT.config"}, Nothing)
    '        My.Settings.SelectedConfig = validSingleName
    '    End If
    'End Sub
    'Private Sub frmMain_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
    '    Dim files As List(Of String) = New List(Of String)
    '    Dim drops() As String = e.Data.GetData(DataFormats.FileDrop)
    '    For Each file As String In drops
    '        If FileIO.FileSystem.DirectoryExists(file) Then
    '            For Each subfile In My.Computer.FileSystem.GetFiles(file, FileIO.SearchOption.SearchTopLevelOnly, {"*.RDT.config"})
    '                files.Add(subfile)
    '                If files.Count > 0 Then Exit For
    '            Next
    '        Else
    '            files.Add(file)
    '        End If
    '        If files.Count > 0 Then Exit For
    '    Next
    '    If files.Any(Function(f) f.EndsWith("RDT.config")) Then
    '        e.Effect = DragDropEffects.Copy
    '    End If
    'End Sub

    'Private Sub frmMain_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
    '    Dim files As List(Of String) = New List(Of String)
    '    Dim drops() As String = e.Data.GetData(DataFormats.FileDrop)
    '    For Each file As String In drops
    '        If FileIO.FileSystem.DirectoryExists(file) Then
    '            For Each subfile In My.Computer.FileSystem.GetFiles(file, FileIO.SearchOption.SearchTopLevelOnly, {"*.RDT.config"})
    '                files.Add(subfile)
    '            Next
    '        Else
    '            files.Add(file)
    '        End If

    '    Next
    '    importFiles(files.ToArray)
    'End Sub
    'Private Function testConfig(file As String) As Boolean
    '    Dim valid As Boolean = False

    '    My.Settings.Save()

    '    FileIO.FileSystem.CopyFile(configDir & "\user.config", configDir & "\backup.config", True)


    '    FileIO.FileSystem.CopyFile(file, configDir & "\user.config", True)
    '    Try
    '        My.Settings.Reload()
    '        Dim dummy As String = My.Settings.SelectedConfig
    '        valid = True
    '    Catch
    '        valid = False
    '    Finally
    '        FileIO.FileSystem.CopyFile(configDir & "\backup.config", configDir & "\user.config", True)
    '        FileIO.FileSystem.DeleteFile(configDir & "\backup.config")
    '        My.Settings.Reload()
    '    End Try

    '    Return valid
    'End Function


    Private Function isAstoniaClass(pp As Process) As Boolean
        Dim wndClass As String() = My.Settings.className.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
        For i As Integer = 0 To UBound(wndClass)
            wndClass(i) = Trim(wndClass(i))
        Next
        'Debug.Print("""" & wndClass(0) & """")
        Return wndClass.Contains(GetWindowClass(pp.MainWindowHandle))
    End Function



    'Private Sub LoadToolStripMenuItem_DropDownOpening(sender As ToolStripMenuItem, e As EventArgs) Handles LoadToolStripMenuItem.DropDownOpening
    '    sender.DropDownItems.Clear()
    '    If System.IO.Directory.Exists(configDir) Then

    '        For Each config As String In My.Computer.FileSystem.GetFiles(configDir, FileIO.SearchOption.SearchTopLevelOnly, {"*.RDT.config"})
    '            Debug.Print(config)
    '            Dim name As String = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(config))
    '            Debug.Print(name)
    '            If name = "user" Then Continue For
    '            Dim item As ToolStripMenuItem = sender.DropDownItems.Add(name, Nothing, AddressOf LoadConfigToolStripMenuItem_Click)
    '            item.Tag = config

    '            If name.ToUpper = My.Settings.SelectedConfig.ToUpper Then
    '                item.Checked = True
    '            End If
    '        Next
    '    End If
    '    'If sender.DropDownItems.Count = 0 Then sender.DropDownItems.Add("(None)").Enabled = False
    '    sender.DropDownItems.Add(New ToolStripSeparator)
    '    sender.DropDownItems.Add("Open Config Folder...", Nothing, AddressOf OpenConfigFolderToolStripMenuItem_Click)
    'End Sub

    'Private Sub OpenConfigFolderToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    My.Settings.Save()
    '    Dim pp As New Process With {.StartInfo = New ProcessStartInfo With {.FileName = configDir}}
    '    Try
    '        pp.Start()
    '    Catch

    '    End Try
    'End Sub

    'Private Sub LoadConfigToolStripMenuItem_Click(sender As ToolStripMenuItem, e As EventArgs) 'handles LoadConigToolStripMenuItem.Click
    '    Debug.Print(sender.Text)

    '    frmSettings.Close()
    '    frmV2.Close()
    '    'Make backup

    '    FileIO.FileSystem.CopyFile(configDir & "\user.config", configDir & "\backup.config", True)

    '    FileIO.FileSystem.CopyFile(sender.Tag, configDir & "\user.config", True)

    '    Try
    '        My.Settings.Reload()
    '        My.Settings.SelectedConfig = sender.Text
    '    Catch
    '        FileIO.FileSystem.CopyFile(configDir & "\backup.config", configDir & "\user.config", True)
    '        MessageBox.Show(System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(sender.Tag)) & " Config Invalid/Corrupted", "Error")
    '        My.Settings.Reload()
    '    Finally
    '        FileIO.FileSystem.DeleteFile(configDir & "\backup.config")
    '    End Try


    '    My.Settings.Save()
    'End Sub

End Class

Module extensions
    <Extension()>
    Public Function isRunning(AltPP As Process) As Boolean
        Try
            Return AltPP IsNot Nothing AndAlso Not AltPP.HasExited
        Catch e As Exception
            Debug.Print("elevating " & AltPP.Id)
            frmMain.runAsAdmin()
            End
        End Try
    End Function
End Module