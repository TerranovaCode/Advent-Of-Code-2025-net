Class Day04
	Inherits DayTemplate
	Dim Grid As Char(,)
	Sub New(_Input)
		MyBase.New(_Input)

	End Sub
	Public Overrides Function Part1() As String
		Dim MovableCount As Integer
		Grid = StringToRectangularCharArray(input)
		For iOuter As Integer = Grid.GetLowerBound(0) To Grid.GetUpperBound(0)
			'iOuter represents the first dimension
			For iInner As Integer = Grid.GetLowerBound(1) To Grid.GetUpperBound(1)
				'iInner represents the second dimension
				If Grid(iOuter, iInner) = "@" AndAlso CheckAdjecntIn2dCharArray(iOuter, iInner, Grid, "@") < 4 Then MovableCount += 1
			Next
		Next
		Return MovableCount
	End Function
	Public Overrides Function Part2() As String
		Grid = StringToRectangularCharArray(input)
		Dim LastMovedCount = 1
		Dim TotalMoved As Integer
		Dim loopCount As Integer
		Do While LastMovedCount > 0

			loopCount += 1
			'If loopCount > 20 Then Exit Do
			Dim CurrentMovableCount As Integer
			CurrentMovableCount = 0
			For iOuter As Integer = Grid.GetLowerBound(0) To Grid.GetUpperBound(0)
				'iOuter represents the first dimension
				For iInner As Integer = Grid.GetLowerBound(1) To Grid.GetUpperBound(1)
					'iInner represents the second dimension
					If Grid(iOuter, iInner) = "@" AndAlso CheckAdjecntIn2dCharArray(iOuter, iInner, Grid, "@") < 4 Then
						CurrentMovableCount += 1
						Grid(iOuter, iInner) = "X"
					End If
				Next
			Next
			LastMovedCount = CurrentMovableCount
			TotalMoved += CurrentMovableCount
		Loop
		Return TotalMoved
	End Function
End Class