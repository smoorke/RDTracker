Public Class Maze
    Public display As New Bitmap(255, 255)
    Private buffer As New Bitmap(255, 255)
    Private v2Over As New Bitmap(255, 255)
    Private v2Unde As New Bitmap(255, 255)

    Private grDisp As Graphics = Graphics.FromImage(display)
    Private grBuff As Graphics = Graphics.FromImage(buffer)

    Public grOver As Graphics = Graphics.FromImage(v2Over)
    Public grUnde As Graphics = Graphics.FromImage(v2unde)

    Public cata As Boolean
    Private size, div, bound As Integer
    Private Boxes(,,) As Rectangle
    Private gasTraps(,,) As Rectangle
    Private Walls(,,) As Rectangle
    Private TrapDoors(,) As Point

    Public Sub New(Optional cata As Boolean = False)
        Clear(My.Settings.background)
        Me.cata = cata
        size = If(cata, 3, 4)
        div = 141 - 20 * size '81 or 61
        bound = 34 - 5 * size '19 or 14
        Boxes = New Rectangle(bound, bound, 2) {}
        For Y = 0 To bound
            For X = 0 To bound
                Boxes(X, Y, 0) = New Rectangle(size * (4 * X + 1), size * (4 * Y + 1), size * 3, size * 3)
                Boxes(X, Y, 1) = New Rectangle(size * (4 * X + 1), size * 4 * Y, size * 3, size)
                Boxes(X, Y, 2) = New Rectangle(size * 4 * X, size * (4 * Y + 1), size, size * 3)
            Next
        Next
        Walls = New Rectangle(bound + 1, bound + 1, 2) {}
        For Y = 0 To bound + 1
            For X = 0 To bound + 1
                Walls(X, Y, 0) = New Rectangle(size * (4 * X), size * (4 * Y), size, size)
                Walls(X, Y, 1) = New Rectangle(size * (4 * X + 1), size * 4 * Y, size * 3, size)
                Walls(X, Y, 2) = New Rectangle(size * 4 * X, size * (4 * Y + 1), size, size * 3)
            Next
        Next




        If Not cata Then
            TrapDoors = New Point(bound, bound) {}
            For Y = 0 To bound
                For X = 0 To bound
                    TrapDoors(X, Y) = New Point(16 * X + 8, 16 * Y + 8)
                Next
            Next
            gasTraps = New Rectangle(bound, bound, 1) {}
            For Y = 0 To bound
                For X = 0 To bound
                    gasTraps(X, Y, 0) = New Rectangle(4 * (4 * X + 1), 4 * 4 * Y + 8, 8, 4)
                    gasTraps(X, Y, 1) = New Rectangle(4 * 4 * Y + 8, 4 * (4 * X + 1), 4, 8)
                Next
            Next
        End If
    End Sub

    Private Function toMaz(GameX As Integer, GameY As Integer) As Point
        Return New Point(((GameX - 2) Mod div) * size, ((GameY - 2) Mod div) * size)
    End Function

    Public Function getNum(GameX As Integer, GameY As Integer) As Integer
        Return ((GameX - 2) \ div + 1) + (((GameY - 2) \ div) * size)
    End Function

    Public Sub plotMaze(GameX As Integer, GameY As Integer, col As Drawing.Color)
        Dim player As Point = toMaz(GameX, GameY)
        For Each box As Rectangle In Boxes
            If box.Contains(player) Then
                grBuff.FillRectangle(New SolidBrush(col), box)
                Exit Sub
            End If
        Next
    End Sub

    Public Sub plotPlayer(GameX As Integer, GameY As Integer, col As Color)
        Dim Player As Point = toMaz(GameX, GameY)
        grDisp.FillRectangle(New SolidBrush(col), Player.X, Player.Y, size, size)
    End Sub
    Private ptZero As New Point(0, 0)

    Public Sub Update()
        grDisp.DrawImage(v2Unde, ptZero)
        grDisp.DrawImage(buffer, ptZero)
        grDisp.DrawImage(v2Over, ptZero)
    End Sub
    Public Sub Clear(col As Color)
        grDisp.Clear(col)
        grBuff.Clear(Color.Transparent)
        grOver.Clear(Color.Transparent)
        grUnde.Clear(Color.Transparent)
    End Sub


    Public Sub drawTrash(gameX As Integer, gameY As Integer, col As Drawing.Color)
        Dim target As Point = toMaz(gameX, gameY)
        grBuff.FillRectangle(New SolidBrush(col), target.X, target.Y, size, size)
    End Sub

    'Public Sub drawfloor(gameX As Integer, gameY As Integer)
    '    Dim target As Point = toMaz(gameX, gameY)

    '    grUnde.FillRectangle(New SolidBrush(Color.LightGray), target.X, target.Y, size, size)

    'End Sub

    Public Sub drawShrine(gameX As Integer, gameY As Integer, col As Color, outline As Color)
        Dim target As Point = toMaz(gameX, gameY)

        grOver.FillRectangle(New SolidBrush(outline), New Rectangle(target.X - 2, target.Y - 2, 8, 8))
        grOver.FillRectangle(New SolidBrush(col), New Rectangle(target.X, target.Y, 4, 4))


    End Sub

    Public Sub drawWall(gameX As Integer, gameY As Integer, col As Color)
        Dim target As Point = toMaz(gameX, gameY)
        If target.X Mod size * 2 > 0 AndAlso target.Y Mod size * 2 > 0 Then
            Exit Sub
        End If
        For Each wall As Rectangle In Walls
            If wall.Contains(target) Then
                grUnde.FillRectangle(New SolidBrush(col), wall)
                Exit Sub
            End If
        Next
    End Sub
    Public Function isWall(gameX As Integer, gameY As Integer)
        Dim target As Point = toMaz(gameX, gameY)
        For Each wall As Rectangle In Walls
            If wall.Contains(target) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Sub drawTD(gameX As Integer, gameY As Integer, NEtoSW As Boolean, open As Boolean, col As Drawing.Color, opencol As Drawing.Color, closedcol As Drawing.Color)
        Dim xOff As Integer = 0
        Dim yOff As Integer = 0
        Dim door As Drawing.Size
        Dim target As Point = toMaz(gameX, gameY)
        If NEtoSW Then
            yOff = -size
            door = New Size(size, size * 3)
        Else
            xOff = -size
            door = New Size(size * 3, size)
        End If
        For Each trap As Point In TrapDoors
            If trap = target Then
                grOver.FillRectangle(New SolidBrush(col), New Rectangle(New Point(trap.X + xOff, trap.Y + yOff), door))
                grOver.FillRectangle(New SolidBrush(If(open, opencol, closedcol)), trap.X, trap.Y, size, size)
            End If
        Next
    End Sub

    Public Function hasGT(gameX As Integer, gameY As Integer) As Boolean
        Dim target As Point = toMaz(gameX, gameY)
        If display.GetPixel(target.X, target.Y).ToArgb = My.Settings.gastrap.ToArgb Then
            Return True
        End If
        Return False
    End Function
    Public Sub drawGT(gameX As Integer, gameY As Integer, col As Drawing.Color)
        Dim target As Point = toMaz(gameX, gameY)
        For Each gt As Rectangle In gasTraps
            If gt.Contains(target) Then
                grOver.FillRectangle(New SolidBrush(col), gt)
                Exit Sub
            End If
        Next
    End Sub
    Public Function isGT(gameX As Integer, gameY As Integer)
        Dim target As Point = toMaz(gameX, gameY)
        For Each gt As Rectangle In gasTraps
            If gt.Contains(target) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function isLobby(GameX As Integer, GameY As Integer) As Boolean
        If cata Then
            If (GameX >= 245 OrElse GameY >= 245) Then
                Return True
            End If
        Else
            If (GameX >= 226 And GameX <= 253 And GameY <= 253 And GameY >= 247) Or
               (GameX >= 247 And GameX <= 253 And GameY <= 246 And GameY >= 226) Then
                Return True
            End If
        End If
        Return False
    End Function
    Public Function isYendor(GameX As Integer, GameY As Integer) As Boolean
        If Not cata AndAlso (GameX >= 2 And GameX <= 43 And GameY >= 248 And GameY <= 252) Then
            Return True
        End If
        Return False
    End Function
End Class
