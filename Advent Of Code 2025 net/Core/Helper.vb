Imports System.Runtime.InteropServices

Module Helper
    Enum MathOperation
        Plus
        Multiplty
    End Enum
    Public Function SplitLines(input As String) As String()
        Return input.Split(New String() {vbCrLf, vbLf, vbCr}, StringSplitOptions.None)
    End Function
    Public Function SplitStringWithDLim(input As String, Delim As String)
        Return input.Split(Delim)
    End Function
    Public Function CountCharacter(ByVal value As String, ByVal ch As Char) As Integer
        Dim cnt As Integer = 0
        For Each c As Char In value
            If c = ch Then
                cnt += 1
            End If
        Next
        Return cnt
    End Function
    Public Function StringToRectangularCharArray(ByVal input As String, Optional ByVal pad As Char = " "c) As Char(,)
        ' Normalize line endings: handle CRLF and CR
        Dim normalized As String = input.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf)

        Dim lines() As String = normalized.Split(New String() {vbLf}, StringSplitOptions.None)

        Dim rows As Integer = lines.Length
        Dim cols As Integer = 0

        ' Find the maximum line length
        For Each line As String In lines
            If line.Length > cols Then
                cols = line.Length
            End If
        Next

        Dim grid(rows - 1, cols - 1) As Char

        ' Initialize with pad character
        For r As Integer = 0 To rows - 1
            For c As Integer = 0 To cols - 1
                grid(r, c) = pad
            Next
        Next

        ' Fill with actual characters
        For r As Integer = 0 To rows - 1
            Dim line As String = lines(r)
            For c As Integer = 0 To line.Length - 1
                grid(r, c) = line(c)
            Next
        Next

        Return grid
    End Function
    ''' <summary>
    ''' Gets how many adjecnt squares in a grid match a value 
    ''' </summary>
    ''' <param name="StartRow"></param>
    ''' <param name="StartCol"></param>
    ''' <param name="grid"></param>
    ''' <param name="CheckValue"></param>
    ''' <returns></returns>
    Public Function CheckAdjecntIn2dCharArray(StartRow As Integer, StartCol As Integer, grid As Char(,), CheckValue As Char) As Integer
        Dim checkCount As Integer
        For RowMove As Integer = -1 To 1
            For ColMove As Integer = -1 To 1
                If RowMove = 0 AndAlso ColMove = 0 Then Continue For
                Dim NeighbourCol As Integer = StartCol + ColMove
                Dim NeighbourRow As Integer = StartRow + RowMove
                If NeighbourCol > grid.GetUpperBound(1) Or NeighbourRow > grid.GetUpperBound(0) Then
                    Continue For
                ElseIf NeighbourRow < 0 Or NeighbourCol < 0 Then
                    Continue For
                Else
                    Dim cols = grid.GetLength(1)
                    If grid(NeighbourRow, NeighbourCol) = CheckValue Then checkCount += 1
                End If
            Next
        Next
        Return checkCount
    End Function
End Module
