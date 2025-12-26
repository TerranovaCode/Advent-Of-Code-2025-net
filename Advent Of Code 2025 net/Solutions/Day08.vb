Class Day08
	Inherits DayTemplate
	Sub New(_Input)
		MyBase.New(_Input)
	End Sub
	Public Overrides Function Part1() As String
		Dim Points As String()
		Points = SplitLines(input)
		Dim Boxes As New List(Of JunctionBox)
		For Each pPoint In Points
			Dim Dimensions = pPoint.Split(",")
			Boxes.Add(New JunctionBox(Dimensions(0), Dimensions(1), Dimensions(2)))
		Next
		For Each pBox1 In Boxes
			Dim d As Long
			Dim ClosestBox As JunctionBox
			For Each pBox2 In Boxes
				Dim currentD = Math.Sqrt((pBox2.X - pBox1.X) ^ 2 + (pBox2.Y - pBox1.Y) ^ 2 + (pBox2.Z - pBox1.Z) ^ 2)
				If currentD > 0 AndAlso currentD < d Then
					d = currentD
					ClosestBox = pBox2
				End If
				pBox1.ClosestBox = ClosestBox
			Next
		Next
		Dim circuitList As New List(Of Circuit)
		circuitList.Add(New Circuit(Boxes(0), Boxes(0).ClosestBox))
		For i = 1 To Boxes.Count - 1
			Dim pBox = Boxes(i)
			For Each pCircuit In circuitList
				If pCircuit.Boxes.Contains(pBox) Then Continue For
				If pCircuit.Boxes.Contains(pBox.ClosestBox) Then
					pCircuit.Boxes.Add(pBox)
					Continue For
				End If
			Next
		Next
	End Function
	Public Overrides Function Part2() As String
		Throw New NotImplementedException()
	End Function
End Class
Class JunctionBox
	Property X As Long
	Property Y As Long
	Property Z As Long
	Property ClosestBox As JunctionBox
	Sub New(_X As Long, _Y As Long, _Z As Long)
		X = _X
		Y = _Y
		Z = _Z
	End Sub
End Class
Structure Circuit
	Property Boxes As List(Of JunctionBox)
	Property size As Integer
		Get
			Return Boxes.Count
		End Get
		Set(value As Integer)

		End Set
	End Property
	Sub New(_J1 As JunctionBox, _J2 As JunctionBox)
		Dim pBoxes As New List(Of JunctionBox)
		pBoxes.Add(_J1)
		pBoxes.Add(_J2)
		Boxes = pBoxes
	End Sub
End Structure