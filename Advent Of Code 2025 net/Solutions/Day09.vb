Imports System.Numerics

Class Day09
	Inherits DayTemplate
	Property LargestPoint As Point
	Property polygonEdges As New List(Of Edge)
	Sub New(_Input)
		MyBase.New(_Input)
	End Sub
	Public Overrides Function Part1() As String
		'input = "7,1
		'11,1
		'11,7
		'9,7
		'9,5
		'2,5
		'2,3
		'7,3"
		Dim PntList As New List(Of Point)
		For Each pString In SplitLines(input)
			If pString.Contains(",") Then
				Dim pPnt As New Point(pString.Split(",").First, pString.Split(",").Last)
				PntList.Add(pPnt)
			End If
		Next
		PntList.Sort(Function(P1, P2)
						 Dim c = P1.X.CompareTo(P2.X)
						 If c <> 0 Then Return c
						 Return P1.Y.CompareTo(P2.Y)
					 End Function)
		Dim pCurrentBox As New Box
		pCurrentBox.P1 = PntList(0)
		pCurrentBox.P2 = PntList(PntList.Count - 1)
		LargestPoint = pCurrentBox.P2
		For i = 0 To PntList.Count - 1
			Dim pPnt = PntList(i)
			'If PointTheSame(pPnt, pCurrentBox.P1) Or PointTheSame(pPnt, pCurrentBox.P2) Or IsPointInsideBox(pPnt, pCurrentBox) Then Continue For
			For Each pPnt2 In PntList
				'If PointTheSame(pPnt, pPnt2) Then Continue For
				If getArea(pPnt, pPnt2) > getArea(pCurrentBox.P1, pCurrentBox.P2) Then
					pCurrentBox.P1 = pPnt
					pCurrentBox.P2 = pPnt2
				End If
			Next
		Next
		Return pCurrentBox.Area.ToString
	End Function
	Friend Function PointTheSame(p1 As Point, p2 As Point)
		If p1.X = p2.X AndAlso p1.Y = p2.Y Then Return True
		Return False
	End Function
	Public Overrides Function Part2() As String
		'input = "7,1
		'11,1
		'11,7
		'9,7
		'9,5
		'2,5
		'2,3
		'7,3"
		Part1()

		Dim PntList As New List(Of Point)
		For Each pString In SplitLines(input)
			If pString.Contains(",") Then
				Dim pPnt As New Point(pString.Split(",").First, pString.Split(",").Last)
				PntList.Add(pPnt)
			End If
		Next
		polygonEdges.Clear()

		For i = 0 To PntList.Count - 2
			Dim pEdge As New Edge(PntList(i), PntList(i + 1))
			polygonEdges.Add(pEdge)
		Next
		Dim pCurrentBox As New Box
		pCurrentBox.P1 = PntList(0)
		pCurrentBox.P2 = PntList(1)
		polygonEdges.Add(New Edge(PntList(PntList.Count - 1), PntList(0)))
		For Each pPnt1 In PntList
			For Each pPnt2 In PntList
				Dim candidateArea = getArea(pPnt1, pPnt2)
				If candidateArea <= getArea(pCurrentBox.P1, pCurrentBox.P2) Then Continue For
				If IsValidRectangle(pPnt1, pPnt2) Then
					If getArea(pPnt1, pPnt2) > getArea(pCurrentBox.P1, pCurrentBox.P2) Then
						pCurrentBox.P1 = pPnt1
						pCurrentBox.P2 = pPnt2
					End If
				End If
			Next
		Next
		Return pCurrentBox.Area.ToString
	End Function
	Function IsValidRectangle(P1 As Point, P2 As Point)
		If P1.X = P2.X OrElse P1.Y = P2.Y Then Return False
		Dim CheckBox As New Box
		CheckBox.P1 = P1
		CheckBox.P2 = P2
		For Each pPnt In CheckBox.PointList
			If Not PointInsidePolygon(pPnt) Then
				Return False
			End If
		Next
		For Each E In polygonEdges
			If EdgeIntersectsRectangleInterior(E, CheckBox) Then Return False
		Next
		Return True
	End Function
	Function VerticalEdgeIntersectsInterior(E As Edge, rect As Box)
		Dim x = E.X1
		Dim y1 = Math.Min(E.Y1, E.Y2)
		Dim y2 = Math.Max(E.Y1, E.Y2)


		If x <= rect.xMin Or x >= rect.xMax Then Return False



		Dim overlapMin = Math.Max(y1, rect.yMin)
		Dim overlapMax = Math.Min(y2, rect.yMax)

		Return overlapMin < overlapMax
	End Function
	Function HorizontalEdgeIntersectsInterior(E As Edge, rect As Box)
		Dim y = E.Y1
		Dim x1 = Math.Min(E.X1, E.X2)
		Dim x2 = Math.Max(E.X1, E.X2)


		If y <= rect.yMin Or y >= rect.yMax Then Return False



		Dim overlapMin = Math.Max(x1, rect.xMin)
		Dim overlapMax = Math.Min(x2, rect.xMax)

		Return overlapMin < overlapMax
	End Function
	Function EdgeIntersectsRectangleInterior(E As Edge, rect As Box)
		If E.IsVertical Then
			Return VerticalEdgeIntersectsInterior(E, rect)
		Else
			Return HorizontalEdgeIntersectsInterior(E, rect)
		End If
	End Function
	Function PointInsidePolygon(P As Point)
		Dim crossings = 0

		For Each E In polygonEdges

			If E.IsVertical Then
				Dim x = E.X1
				Dim y1 = Math.Min(E.Y1, E.Y2)
				Dim y2 = Math.Max(E.Y1, E.Y2)
				If E.IsVertical AndAlso P.X = x AndAlso P.Y >= y1 AndAlso P.Y <= y2 Then
					Return True
				End If

				If x <= P.X Then Continue For



				If P.Y >= y1 And P.Y < y2 Then crossings += 1
			End If
		Next
		If crossings Mod 2 = 1 Then Return True
		Return False
	End Function
	'Friend Function ContainsOnlyValidTiles(Grid As Char(,), P1 As Point, P2 As Point)
	'	For X = math.Min(P1.X, P2.X) To math.Max(P1.X, P2.X)
	'		For Y = math.Min(P1.Y, P2.Y) To math.Max(P1.Y, P2.Y)
	'			If Grid(X, Y) = "." Then Return False
	'		Next
	'	Next
	'	Return True
	'End Function
	Friend Function IsPointInsideBox(CheckPoint As Point, Box As Box) As Boolean
		'Vertial check
		If CheckPoint.Y > Math.Min(Box.P1.Y, Box.P2.Y) AndAlso CheckPoint.Y < Math.Max(Box.P1.Y, Box.P2.Y) Then
			'HorizontalCheck 
			If CheckPoint.X > Math.Min(Box.P1.X, Box.P2.X) AndAlso CheckPoint.X < Math.Max(Box.P1.X, Box.P2.X) Then
				Return True
			End If
		End If
		Return False
	End Function
	Friend Function getArea(P1 As Point, P2 As Point)
		Dim Vertical As Long = Math.Max(P1.Y - P2.Y, P2.Y - P1.Y) + 1
		Dim Horizontal As Long = Math.Max(P1.X - P2.X, P2.X - P1.X) + 1
		Return Vertical * Horizontal
	End Function
End Class
Class Edge
	Public X1, Y1, X2, Y2 As Long
	Property IsVertical As Boolean
		Get
			If X1 = X2 Then Return True
			Return False
		End Get
		Set(value As Boolean)

		End Set
	End Property
	Sub New(P1 As Point, P2 As Point)
		X1 = P1.X
		Y1 = P1.Y
		X2 = P2.X
		Y2 = P2.Y
	End Sub
End Class
Class Box
	Property P1 As Point
	Property P2 As Point
	Property P3 As Point
		Get
			Return New Point(P2.X, P1.Y)
		End Get
		Set(value As Point)

		End Set
	End Property
	Property P4 As Point
		Get
			Return New Point(P1.X, P2.Y)
		End Get
		Set(value As Point)

		End Set
	End Property
	Property PointList As List(Of Point)
		Get
			Dim List = New List(Of Point)
			List.Add(P1)
			List.Add(P2)
			List.Add(P3)
			List.Add(P4)
			Return List
		End Get
		Set(value As List(Of Point))

		End Set
	End Property
	Property Area As Long
		Get
			Dim Vertical As Long = Math.Max(P1.Y - P2.Y, P2.Y - P1.Y) + 1
			Dim Horizontal As Long = Math.Max(P1.X - P2.X, P2.X - P1.X) + 1
			Return Vertical * Horizontal
		End Get
		Set(value As Long)

		End Set
	End Property
	Property xMin As Long
		Get
			Return Math.Min(P1.X, P2.X)
		End Get
		Set(value As Long)

		End Set
	End Property
	Property xMax As Long
		Get
			Return Math.Max(P1.X, P2.X)
		End Get
		Set(value As Long)

		End Set
	End Property
	Property yMin As Long
		Get
			Return Math.Min(P1.Y, P2.Y)
		End Get
		Set(value As Long)

		End Set
	End Property
	Property yMax As Long
		Get
			Return Math.Max(P1.Y, P2.Y)
		End Get
		Set(value As Long)

		End Set
	End Property

End Class
Class Point
	Property X As Long
	Property Y As Long
	Sub New(_x, _y)
		X = _x
		Y = _y
	End Sub
End Class