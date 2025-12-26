Imports System.Dynamic

Class Day07
	Inherits DayTemplate
	Property LaserTimelines As Integer
	Property Timelines As New List(Of Char(,))
	Dim BaseGrid As Char(,)
	Sub New(_Input)
		MyBase.New(_Input)
	End Sub
	Public Overrides Function Part1() As String
		input = ".......S.......
...............
.......^.......
...............
......^.^......
...............
.....^.^.^.....
...............
....^.^...^....
...............
...^.^...^.^...
...............
..^...^.....^..
...............
.^.^.^.^.^...^.
..............."
		Dim Grid = StringToRectangularCharArray(input)
		BaseGrid = Grid
		Dim splitCount As Integer
		For Row = 0 To Grid.GetUpperBound(0)
			For Col = 0 To Grid.GetUpperBound(1)
				Dim CurrentSquare = Grid(Row, Col)
				Dim CellType As LaserType = CharToLaserType(CurrentSquare)
				Grid(Row, Col) = "x"

				'For i = 0 To Grid.GetUpperBound(0)
				'	For j = 0 To Grid.GetUpperBound(1)
				'		Debug.WriteLine((Grid(i, j).ToString()))
				'	Next
				'Next
				'Debug.Print(Char2DArrayToString(Grid))
				Select Case CellType
					Case LaserType.Laser
						If Row + 1 < Grid.GetUpperBound(1) Then
							If Grid(Row + 1, Col) = "." Or Grid(Row, Col + 1) = "x" Then Grid(Row + 1, Col) = "|"
						End If
					Case LaserType.Blank
						If Row - 1 > Grid.GetLowerBound(1) Then
							If Grid(Row - 1, Col) = "|" Then Grid(Row, Col) = "|"
						End If
					Case LaserType.Start

						Grid(Row + 1, Col) = "|"
					Case LaserType.Splitter
						If Grid(Row - 1, Col) = "|" Then
							splitCount += 1
							If Col - 1 > Grid.GetLowerBound(1) Then
								If Grid(Row, Col - 1) <> "|" Then
									Grid(Row, Col - 1) = "|"
									'splitCount += 1
								End If
							End If
							If Col + 1 < Grid.GetUpperBound(0) Then
								If Grid(Row, Col + 1) <> "|" And Grid(Row - 1, Col + 1) <> "|" Then
									Grid(Row, Col + 1) = "|"
									'splitCount += 1
								End If
							End If
							Grid(Row, Col) = "o"

						End If
					Case Else
				End Select
				If Grid(Row, Col) = "x" Then Grid(Row, Col) = CurrentSquare
			Next
		Next
		Debug.Print(Char2DArrayToString(Grid))
		Return splitCount
	End Function
	Function Char2DArrayToString(chars As Char(,)) As String
		If chars Is Nothing Then
			Throw New ArgumentNullException(NameOf(chars), "Input array cannot be null.")
		End If

		Dim rows As Integer = chars.GetLength(0)
		Dim cols As Integer = chars.GetLength(1)

		Dim result As New Text.StringBuilder(rows * cols)

		' Loop through each element in row-major order
		For r As Integer = 0 To rows - 1
			result.Append("
")

			For c As Integer = 0 To cols - 1
				Dim ch As Char = chars(r, c)
				' Skip null characters (Chr(0)) if present
				If ch <> ChrW(0) Then
					result.Append(ch)
				End If
			Next
			' Optional: Add newline between rows
			' result.AppendLine()
		Next

		Return result.ToString()
	End Function
	Public Overrides Function Part2() As String
		input = ".......S.......
...............
.......^.......
...............
......^.^......
...............
.....^.^.^.....
...............
....^.^...^....
...............
...^.^...^.^...
...............
..^...^.....^..
...............
.^.^.^.^.^...^.
..............."
		BaseGrid = StringToRectangularCharArray(input)
		Dim splitCount As Integer
		Dim Grid = BaseGrid
		For Row = 0 To Grid.GetUpperBound(0)
			For Col = 0 To Grid.GetUpperBound(1)
				Dim CurrentSquare = Grid(Row, Col)
				Dim CellType As LaserType = CharToLaserType(CurrentSquare)
				Grid(Row, Col) = "x"

				'For i = 0 To Grid.GetUpperBound(0)
				'	For j = 0 To Grid.GetUpperBound(1)
				'		Debug.WriteLine((Grid(i, j).ToString()))
				'	Next
				'Next
				'Debug.Print(Char2DArrayToString(Grid))
				Select Case CellType
					Case LaserType.Laser
						If Row + 1 < Grid.GetUpperBound(1) Then
							If Grid(Row + 1, Col) = "." Or Grid(Row, Col + 1) = "x" Then Grid(Row + 1, Col) = "|"
						End If
					Case LaserType.Blank
						If Row - 1 > Grid.GetLowerBound(1) Then
							If Grid(Row - 1, Col) = "|" Then Grid(Row, Col) = "|"
						End If
					Case LaserType.Start
						Grid(Row + 1, Col) = "|"
					Case LaserType.Splitter
						If Grid(Row - 1, Col) = "|" Then
							Grid(Row, Col) = "o"
							If Col - 1 > Grid.GetLowerBound(1) Then
								If Grid(Row, Col - 1) <> "|" And Grid(Row - 1, Col + -1) <> "|" Then
									Grid(Row, Col - 1) = "|"
									LaserTimeline(Row, Col - 1, Grid, True)
								End If
							End If
							If Col + 1 < Grid.GetUpperBound(0) Then
								If Grid(Row, Col + 1) <> "|" And Grid(Row - 1, Col + 1) <> "|" Then
									If Grid(Row, Col - 1) = "|" Then Grid(Row, Col - 1) = "."
									Grid(Row, Col + 1) = "|"

								End If
							End If
							Grid(Row, Col) = "o"
						End If
					Case Else
				End Select
				If Grid(Row, Col) = "x" Then Grid(Row, Col) = CurrentSquare
			Next
		Next
		Return Timelines.Count
	End Function
	Friend Sub LaserTimeline(StartRow As Integer, StartCol As Integer, TimelineGrid As Char(,), GoLeft As Boolean)
		If StartRow = BaseGrid.GetUpperBound(0) - 1 Then
			Debug.Print(Char2DArrayToString(TimelineGrid))
			Timelines.Add(TimelineGrid)
			Exit Sub
		End If
		For Row = StartRow To TimelineGrid.GetUpperBound(0)
			For Col = StartCol To TimelineGrid.GetUpperBound(1)
				Dim CurrentSquare = TimelineGrid(Row, Col)
				Dim CellType As LaserType = CharToLaserType(CurrentSquare)
				TimelineGrid(Row, Col) = "x"

				'For i = 0 To TimelineGrid.GetUpperBound(0)
				'	For j = 0 To TimelineGrid.GetUpperBound(1)
				'		Debug.WriteLine((TimelineGrid(i, j).ToString()))
				'	Next
				'Next
				'Debug.Print(Char2DArrayToString(TimelineGrid))
				Select Case CellType
					Case LaserType.Laser
						If Row + 1 < TimelineGrid.GetUpperBound(1) Then
							If TimelineGrid(Row + 1, Col) = "." Or TimelineGrid(Row, Col + 1) = "x" Then TimelineGrid(Row + 1, Col) = "|"
						End If
					Case LaserType.Blank
						If Row - 1 > TimelineGrid.GetLowerBound(1) Then
							If TimelineGrid(Row - 1, Col) = "|" Then TimelineGrid(Row, Col) = "|"
						End If
					Case LaserType.Start
						TimelineGrid(Row + 1, Col) = "|"
					Case LaserType.Splitter
						If TimelineGrid(Row - 1, Col) = "|" Then
							TimelineGrid(Row, Col) = "o"
							If Col - 1 > TimelineGrid.GetLowerBound(1) Then
								If TimelineGrid(Row, Col - 1) <> "|" And TimelineGrid(Row - 1, Col + -1) <> "|" Then
									TimelineGrid(Row, Col - 1) = "|"
									LaserTimeline(Row, Col - 1, TimelineGrid, True)
								End If
							End If
							If Col + 1 < TimelineGrid.GetUpperBound(0) Then
								If TimelineGrid(Row, Col + 1) <> "|" And TimelineGrid(Row - 1, Col + 1) <> "|" Then
									If TimelineGrid(Row, Col - 1) = "|" Then TimelineGrid(Row, Col - 1) = "."
									TimelineGrid(Row, Col + 1) = "|"
									LaserTimeline(Row, Col - 1, TimelineGrid, True)
								End If
							End If
							TimelineGrid(Row, Col) = "o"
						End If
					Case Else
				End Select
				If TimelineGrid(Row, Col) = "x" Then TimelineGrid(Row, Col) = CurrentSquare
			Next
		Next
	End Sub
	Friend Function CharToLaserType(Character As Char)
		Select Case Character
			Case "."
				Return LaserType.Blank
			Case "S"
				Return LaserType.Start
			Case "|"
				Return LaserType.Laser
			Case "^"
				Return LaserType.Splitter
			Case "o"
				Return LaserType.UsedSplitter
			Case Else
				Return Nothing
		End Select
	End Function
End Class
Enum LaserType
	Blank
	Start
	Laser
	Splitter
	UsedSplitter
End Enum

