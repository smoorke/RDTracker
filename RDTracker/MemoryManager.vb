Imports System.IO.MemoryMappedFiles
Imports System.Runtime.InteropServices
Public Class MemoryManager
    Private Declare Function OpenProcess Lib "kernel32.dll" (ByVal dwDesiredAcess As UInt32, ByVal bInheritHandle As Boolean, ByVal dwProcessId As Int32) As IntPtr
    Private Declare Function CloseHandle Lib "kernel32.dll" (ByVal hObject As IntPtr) As Boolean
    Public targetProcess As Process = Nothing
    Public isSDL As Boolean = False
    Private targetProcessHandle As IntPtr = IntPtr.Zero 'Used for ReadProcessMemory
    Private Const PROCESS_ALL_ACCESS As UInt32 = &H1F0FFF
    Private Const PROCESS_VM_READ As UInt32 = &H10
    Private _mmf As MemoryMappedFile
    Private _mmva As MemoryMappedViewAccessor
    Public shm As MoacSharedMem
    Private base As UInt32 = 0

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto, Pack:=0)>
    Structure MoacSharedMem
        Dim pID As UInt32
        Dim hp, shield, [end], mana As Byte
        Dim base As UInt64
        Dim key, isprite, offX, offY As Integer
        Dim flags, fsprite As Integer
        Dim swapped As Byte
    End Structure
#Region "attach by class"
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Sub GetClassName(ByVal hWnd As System.IntPtr, ByVal lpClassName As System.Text.StringBuilder, ByVal nMaxCount As Integer)
    End Sub
    Private Function GetWindowClass(ByVal hwnd As Long) As String
        Dim sClassName As New System.Text.StringBuilder("", 256)
        Call GetClassName(hwnd, sClassName, 256)
        Return sClassName.ToString
    End Function
    Public Function TryAttachToProcess(ByVal windowCaption As String, ByVal windowClass As String, ByVal exeNames() As String) As Boolean
        For Each pp As Process In ListProcessesByNameArray(exeNames)
            If pp.MainWindowTitle.Contains(windowCaption) AndAlso GetWindowClass(pp.MainWindowHandle) = windowClass Then
                Return TryAttachToProcess(pp)
            End If
        Next
        Return False
    End Function
#End Region
    Public Shared Function ListProcessesByNameArray(strings() As String) As List(Of Process)
        Dim list As List(Of Process) = New List(Of Process)
        For Each exe As String In strings
            list.AddRange(Process.GetProcessesByName(Trim(exe)))
        Next
        Return list
    End Function

    Public Function TryAttachToProcess(ByVal windowCaption As String, ByVal exeNames() As String) As Boolean
        For Each pp As Process In ListProcessesByNameArray(exeNames)
            If pp.MainWindowTitle.Contains(windowCaption) Then
                Return TryAttachToProcess(pp)
            End If
        Next

        Return False
    End Function

    Public Function TryAttachToProcess(ByVal proc As Process) As Boolean
        If proc Is Nothing Then Return False
        If targetProcessHandle = IntPtr.Zero Then 'not already attached
            targetProcess = proc
            'targetProcessHandle = OpenProcess(PROCESS_ALL_ACCESS, False, targetProcess.Id)
            targetProcessHandle = OpenProcess(PROCESS_VM_READ, False, targetProcess.Id)
            If targetProcessHandle = 0 Then
                TryAttachToProcess = False
                frmMain.runAsAdmin()
                End
                'MessageBox.Show("OpenProcess() FAIL!")
            Else
                'if we get here, all connected and ready to use ReadProcessMemory()
                _mmf = MemoryMappedFile.CreateOrOpen($"MOAC{Me.targetProcess.Id}", Marshal.SizeOf(GetType(MoacSharedMem)))
                _mmva = _mmf.CreateViewAccessor()
                _mmva.Read(0, shm)
                If shm.pID = Me.targetProcess.Id Then
                    isSDL = True
                Else
                    isSDL = False
                    base = targetProcess.MainModule.BaseAddress
                End If
                TryAttachToProcess = True
                'MessageBox.Show("OpenProcess() OK")
            End If
        Else
            'MessageBox.Show("Already attached! (Please Detach first?)")
            TryAttachToProcess = False
        End If
    End Function

    Public Sub DetachFromProcess()
        If Not (targetProcessHandle = IntPtr.Zero) Then
            targetProcess = Nothing
            Try
                CloseHandle(targetProcessHandle)
                targetProcessHandle = IntPtr.Zero
                'MessageBox.Show("MemReader::Detach() OK")
            Catch ex As Exception
                MessageBox.Show("MemoryManager::DetachFromProcess::CloseHandle error " & Environment.NewLine & ex.Message)
            End Try
        End If
    End Sub

#Region "readmemory"
    Private Declare Function ReadProcessMemory Lib "kernel32" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer() As Byte, ByVal iSize As Integer, ByRef lpNumberOfBytesRead As Integer) As Boolean
    <DllImport("NTDll.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Public Shared Function NtWow64ReadVirtualMemory64(
                handle As IntPtr,
                BaseAddress As UInt64,
                Buffer As Byte(),
                Size As UInt64,
                ByRef NumberOfBytesRead As UInt64) As Integer
    End Function
    Public Function ReadInt32(ByVal addr As IntPtr, Optional ByVal isOffset As Boolean = False) As Int32
        If isOffset Then
            addr = addr + base
        End If
        Dim _dataBytes(3) As Byte
        ReadProcessMemory(targetProcessHandle, addr, _dataBytes, 4, vbNull)
        Return BitConverter.ToInt32(_dataBytes, 0)
    End Function

    Public Function ReadInt16(ByVal addr As IntPtr) As Int16
        Dim _rtnBytes(1) As Byte
        ReadProcessMemory(targetProcessHandle, addr, _rtnBytes, 2, vbNull)
        Return BitConverter.ToInt16(_rtnBytes, 0)
    End Function
    'Public Function ReadInt32(ByVal addr As IntPtr) As Int32
    '    Dim _rtnBytes(3) As Byte
    '    ReadProcessMemory(targetProcessHandle, addr, _rtnBytes, 4, vbNull)

    '    Return BitConverter.ToInt32(_rtnBytes, 0)
    'End Function
    Public Function ReadIntWoW64(ByVal addr As ULong) As Integer
        Dim _rtnBytes(3) As Byte
        NtWow64ReadVirtualMemory64(targetProcessHandle, addr, _rtnBytes, 4, vbNull)
        Return BitConverter.ToInt32(_rtnBytes, 0)
    End Function
    Public Function ReadInt64(ByVal addr As IntPtr) As Int64
        Dim _rtnBytes(7) As Byte
        ReadProcessMemory(targetProcessHandle, addr, _rtnBytes, 8, vbNull)
        Return BitConverter.ToInt64(_rtnBytes, 0)
    End Function
    Public Function ReadUInt16(ByVal addr As IntPtr) As UInt16
        Dim _rtnBytes(1) As Byte
        ReadProcessMemory(targetProcessHandle, addr, _rtnBytes, 2, vbNull)
        Return BitConverter.ToUInt16(_rtnBytes, 0)
    End Function
    Public Function ReadUInt32(ByVal addr As IntPtr) As UInt32
        Dim _rtnBytes(3) As Byte
        ReadProcessMemory(targetProcessHandle, addr, _rtnBytes, 4, vbNull)
        Return BitConverter.ToUInt32(_rtnBytes, 0)
    End Function
    Public Function ReadUInt64(ByVal addr As IntPtr) As UInt64
        Dim _rtnBytes(7) As Byte
        ReadProcessMemory(targetProcessHandle, addr, _rtnBytes, 8, vbNull)
        Return BitConverter.ToUInt64(_rtnBytes, 0)
    End Function
    Public Function ReadFloat(ByVal addr As IntPtr) As Single
        Dim _rtnBytes(3) As Byte
        ReadProcessMemory(targetProcessHandle, addr, _rtnBytes, 4, vbNull)
        Return BitConverter.ToSingle(_rtnBytes, 0)
    End Function
    Public Function ReadDouble(ByVal addr As IntPtr) As Double
        Dim _rtnBytes(7) As Byte
        ReadProcessMemory(targetProcessHandle, addr, _rtnBytes, 8, vbNull)
        Return BitConverter.ToDouble(_rtnBytes, 0)
    End Function
    Public Function ReadIntPtr(ByVal addr As IntPtr) As IntPtr
        Dim _rtnBytes(IntPtr.Size - 1) As Byte
        ReadProcessMemory(targetProcessHandle, addr, _rtnBytes, IntPtr.Size, Nothing)
        If IntPtr.Size = 4 Then
            Return New IntPtr(BitConverter.ToUInt32(_rtnBytes, 0))
        Else
            Return New IntPtr(BitConverter.ToInt64(_rtnBytes, 0))
        End If
    End Function
    Public Function ReadBytes(ByVal addr As IntPtr, ByVal size As Int32) As Byte()
        Dim _rtnBytes(size - 1) As Byte
        ReadProcessMemory(targetProcessHandle, addr, _rtnBytes, size, vbNull)
        Return _rtnBytes
    End Function
#End Region
#Region "writememory"
    Private Declare Function WriteProcessMemory Lib "kernel32" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer() As Byte, ByVal iSize As Integer, ByRef lpNumberOfBytesRead As Integer) As Boolean

    Public Function WriteByte(ByVal addr As IntPtr, ByVal aByte As Byte) As Boolean
        Return WriteProcessMemory(targetProcessHandle, addr, New Byte() {aByte}, 1, vbNull) 'awk array decl :/
    End Function
    Public Function WriteInt16(ByVal addr As IntPtr, ByVal data As Int16) As Boolean
        Return WriteProcessMemory(targetProcessHandle, addr, BitConverter.GetBytes(data), 2, vbNull)
    End Function
    Public Function WriteInt32(ByVal addr As IntPtr, ByVal data As Int32) As Boolean
        Return WriteProcessMemory(targetProcessHandle, addr, BitConverter.GetBytes(data), 4, vbNull)
    End Function
    Public Function WriteInt64(ByVal addr As IntPtr, ByVal data As Int64) As Boolean
        Return WriteProcessMemory(targetProcessHandle, addr, BitConverter.GetBytes(data), 8, vbNull)
    End Function
    Public Function WriteUInt16(ByVal addr As IntPtr, ByVal data As UInt16) As Boolean
        Return WriteProcessMemory(targetProcessHandle, addr, BitConverter.GetBytes(data), 2, vbNull)
    End Function
    Public Function WriteUInt32(ByVal addr As IntPtr, ByVal data As UInt32) As Boolean
        Return WriteProcessMemory(targetProcessHandle, addr, BitConverter.GetBytes(data), 4, vbNull)
    End Function
    Public Function WriteUInt64(ByVal addr As IntPtr, ByVal data As UInt64) As Boolean
        Return WriteProcessMemory(targetProcessHandle, addr, BitConverter.GetBytes(data), 8, vbNull)
    End Function
    Public Function WriteFloat(ByVal addr As IntPtr, ByVal data As Single) As Boolean
        Return WriteProcessMemory(targetProcessHandle, addr, BitConverter.GetBytes(data), 4, vbNull)
    End Function
    Public Function WriteDouble(ByVal addr As IntPtr, ByVal data As Double) As String
        Return WriteProcessMemory(targetProcessHandle, addr, BitConverter.GetBytes(data), 8, vbNull)
    End Function
    Public Function WriteIntPtr(ByVal addr As IntPtr, ByVal ptr As IntPtr) As Boolean
        Dim _bytes(IntPtr.Size - 1) As Byte

        If IntPtr.Size = 4 Then
            _bytes = BitConverter.GetBytes(Convert.ToUInt32(ptr))
            Return WriteProcessMemory(targetProcessHandle, addr, _bytes, 4, vbNull)
        Else
            _bytes = BitConverter.GetBytes(Convert.ToUInt64(ptr))
            Return WriteProcessMemory(targetProcessHandle, addr, _bytes, 8, vbNull)
        End If
    End Function
    Public Function WriteUnicodeString(ByVal addr As IntPtr, ByVal str As String) As Boolean
        Dim _bytes() As Byte = System.Text.Encoding.Unicode.GetBytes(str)
        Return WriteProcessMemory(targetProcessHandle, addr, _bytes, _bytes.Length, vbNull)
    End Function
    Public Function WriteAsciiString(ByVal addr As IntPtr, ByVal str As String) As Boolean
        Dim _bytes() As Byte = System.Text.Encoding.ASCII.GetBytes(str)
        Return WriteProcessMemory(targetProcessHandle, addr, _bytes, _bytes.Length, vbNull)
    End Function
    Public Function WriteBytes(ByVal addr As IntPtr, ByVal bytes() As Byte) As Boolean
        Dim _writeLength As Int32 = 0
        If WriteProcessMemory(targetProcessHandle, addr, bytes, bytes.Length, _writeLength) Then
            If _writeLength = bytes.Length Then
                Return True
            Else
                MessageBox.Show("MemoryManager::WriteBytes() writeLength < buff.size")
                Return False
            End If
        Else
            Return False 'wpm failed!
        End If
    End Function
#End Region
End Class
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto, Pack:=0)>
Structure MoacSharedMem
    Dim pID As UInt32
    Dim hp, shield, [end], mana As Byte
    Dim base As UInt64
    Dim key, isprite, offX, offY As Integer
    Dim flags, fsprite As Integer
    Dim swapped As Byte
End Structure