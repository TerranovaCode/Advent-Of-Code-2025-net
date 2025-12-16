Public Class Day01
 Inherits DayTemplate
 Sub New(_Input)
 MyBase.New(_Input)
 End Sub
 Public Overrides Function Part1() As String
 Dim pos As Integer = 50
 Dim zeros As Integer = 0

 If input Is Nothing Then Return 0

 ' SafeDialNormalize line endings and split
 Dim lines = input.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf) _
  .Split(New Char() {ChrW(10)}, StringSplitOptions.RemoveEmptyEntries)

 For Each line In lines
 Dim s = line.Trim()
 If s.Length = 0 Then Continue For

 Dim dir As Char = Char.ToUpperInvariant(s(0))
 Dim distStr As String = s.Substring(1).Trim()

 Dim dist As Integer
 If Not Integer.TryParse(distStr, dist) Then
 Throw New FormatException("Invalid rotation distance: " & s)
 End If

 Select Case dir
 Case "L"c
  pos = SafeDialNormalize((pos - dist) Mod 100)
 Case "R"c
  pos = SafeDialNormalize((pos + dist) Mod 100)
 Case Else
  Throw New FormatException("Invalid rotation direction (must be L or R): " & s)
 End Select

 If pos = 0 Then
 zeros += 1
 End If
 Next

 Return zeros
 End Function
 ' Ensures value is within [0, 99]
 Private Function SafeDialNormalize(value As Integer) As Integer
 value = value Mod 100
 If value < 0 Then value += 100
 Return value
 End Function
 Private Function SafeDialRequiresNormalize(value As Integer) As Boolean
 value = value Mod 100
 If value < 0 Then Return True
 Return False
 End Function
 Public Overrides Function Part2() As String
 Dim pos As Integer = 50
 Dim zeros As Integer = 0

 If String.IsNullOrEmpty(input) Then Return 0

 ' Normalize line endings and split
 Dim lines = input.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf) _
  .Split(New Char() {ChrW(10)}, StringSplitOptions.RemoveEmptyEntries)

 For Each line In lines
 Dim s = line.Trim()
 If s.Length = 0 Then Continue For

 Dim dir As Char = Char.ToUpperInvariant(s(0))
 Dim distStr As String = s.Substring(1).Trim()

 Dim dist As Integer
 If Not Integer.TryParse(distStr, dist) Then
 Throw New FormatException("Invalid rotation distance: " & s)
 End If

 Select Case dir
 Case "L"c
  ' Move left dist times, checking each click
  For i As Integer = 1 To dist
  If pos = 0 Then
  pos = 99
  Else
  pos -= 1
  End If
  If pos = 0 Then
  zeros += 1
  End If
  Next

 Case "R"c
  ' Move right dist times, checking each click
  For i As Integer = 1 To dist
  pos = (pos + 1) Mod 100
  If pos = 0 Then
  zeros += 1
  End If
  Next

 Case Else
  Throw New FormatException("Invalid rotation direction (must be L or R): " & s)
 End Select
 Next

 Return zeros
 End Function
End Class
Public MustInherit Class DayTemplate
	Property input As String
 Property Output1 As String
 Get
 Output1 = Part1()
 End Get
 Set(value As String)

 End Set
 End Property
 Property Output2 As String
 Get
 Output2 = Part2()
 End Get
 Set(value As String)

 End Set
 End Property
 Sub New(_input As String)
		input = _input
	End Sub
	Public MustOverride Function Part1() As String


	Public MustOverride Function Part2() As String


End Class
