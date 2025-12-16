Option Strict On
Option Explicit On
Option Infer On

Imports System
Imports System.Diagnostics
Imports System.IO
Imports AdventOfCode.Core


Module Program
 Sub Main(args As String())
 Dim day As Integer = ParseDay(args)

 If day = -1 Then
 Console.Write("Enter Advent of Code day (1-25): ")
 Dim s = Console.ReadLine()
 If Not Integer.TryParse(s, day) Then
 Console.WriteLine("Invalid day.")
 Return
 End If
 End If

 If day < 1 OrElse day > 25 Then
 Console.WriteLine("Day must be between 1 and 25.")
 Return
 End If

 Dim paths = New Paths()
 Dim inputPath = paths.GetInputPath(day)

 If Not File.Exists(inputPath) Then
 Console.WriteLine("Input file not found: " & inputPath)
 Console.WriteLine("Place your file at Inputs\day" & day.ToString("00") & ".txt")
 Return
 End If

 Dim raw = File.ReadAllText(inputPath)
 Console.WriteLine("Running Day " & day & "...")

 Dim part1 As String = RunPart(day, 1, raw)
 Console.WriteLine("Part 1: " & part1)

 Dim part2 As String = RunPart(day, 2, raw)
 Console.WriteLine("Part 2: " & part2)

 ' Write outputs
 Dim out1 = paths.GetOutputPath(day, 1)
 Dim out2 = paths.GetOutputPath(day, 2)
 Directory.CreateDirectory(Path.GetDirectoryName(out1))
 Directory.CreateDirectory(Path.GetDirectoryName(out2))
 File.WriteAllText(out1, If(part1, ""))
 File.WriteAllText(out2, If(part2, ""))

 Console.WriteLine("Outputs written:")
 Console.WriteLine(" - " & out1)
 Console.WriteLine(" - " & out2)
 End Sub

 Private Function ParseDay(args As String()) As Integer
 If args Is Nothing OrElse args.Length = 0 Then Return -1

 For i = 0 To args.Length - 1
 Dim a = args(i)
 Dim v As Integer
 If Integer.TryParse(a, v) Then Return v

 If a.StartsWith("--day=", StringComparison.OrdinalIgnoreCase) Then
 Dim s = a.Substring(6)
 If Integer.TryParse(s, v) Then Return v
 End If

 If a.Equals("-d", StringComparison.OrdinalIgnoreCase) AndAlso i + 1 < args.Length Then
 If Integer.TryParse(args(i + 1), v) Then Return v
 End If
 Next

 Return -1
 End Function

 Private Function RunPart(day As Integer, part As Integer, input As String) As String
 Select Case day
 Case 1
 Dim Day01 As New Day01(input)
 If part = 1 Then
  Return Day01.Output1
 Else Return Day01.Output2
 End If
 Case 2
 Dim Day02 As New Day02(input)
 If part = 1 Then
  Return Day02.Output1
 Else Return Day02.Output2
 End If
 Case 3
 Dim Day03 As New Day03(input)
 If part = 1 Then
  Return Day03.Output1
 Else Return Day03.Output2
 End If
 Case 4
 Dim Day04 As New Day04(input)
 If part = 1 Then
  Return Day04.Output1
 Else Return Day04.Output2
 End If
 Case 5
 Dim Day05 As New Day05(input)
 If part = 1 Then
  Return Day05.Output1
 Else Return Day05.Output2
 End If
 Case 6
 Dim Day06 As New Day06(input)
 If part = 1 Then
  Return Day06.Output1
 Else Return Day06.Output2
 End If
 Case 7
 Dim Day07 As New Day07(input)
 If part = 1 Then
  Return Day07.Output1
 Else Return Day07.Output2
 End If
 ' Add more days by copying the pattern:
 'Case 2
 ' If part = 1 Then Return Day02_Part1(input) Else Return Day02_Part2(input)

 Case Else
 Return "Not implemented"
 End Select
 End Function



 Private Class Paths
 Private ReadOnly inputsDev As String
 Private ReadOnly outputsDev As String
 Private ReadOnly inputsBin As String
 Private ReadOnly outputsBin As String

 Public Sub New()
 Dim baseDir = AppContext.BaseDirectory
 inputsBin = Path.Combine(baseDir, "Inputs")
 outputsBin = Path.Combine(baseDir, "Outputs")
 inputsDev = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "Inputs"))
 outputsDev = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "Outputs"))
 End Sub

 Public Function GetInputPath(day As Integer) As String
 Dim file = "day" & day.ToString("00") & ".txt"
 Dim p1 = Path.Combine(inputsBin, file)
 Dim p2 = Path.Combine(inputsDev, file)
 If Directory.Exists(p1) Then Return p1
 Return p2
 End Function

 Public Function GetOutputPath(day As Integer, part As Integer) As String
 Dim dir As String
 If Directory.Exists(outputsBin) Then
 dir = outputsBin
 Else
 dir = outputsDev
 End If
 Dim file = "day" & day.ToString("00") & "_part" & part & ".txt"
 Return Path.Combine(dir, file)
 End Function
 End Class
End Module


