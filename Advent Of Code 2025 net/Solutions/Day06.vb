Imports System.Numerics

Class Day06
	Inherits DayTemplate
	Sub New(_Input)
		MyBase.New(_Input)
	End Sub
	Public Overrides Function Part1() As String
		Dim Csv = input
		Dim total As BigInteger = 0
		Do Until Not Csv.Contains("  ")
			Csv = Csv.Replace("  ", " ")
		Loop
		Csv = Csv.Replace(" ", ",")
		Dim data = CSV_StringTo2DArray(Csv, ",")
		Dim sums As New List(Of Sum)
		Dim lastrow As Integer = data.GetUpperBound(0)
		For col = 0 To data.GetUpperBound(1)
			Dim pSum As New Sum(data(4, col))
			For row = 0 To 3
				If data(row, col) <> "" Then pSum.Nums.Add(data(row, col))
			Next
			sums.Add(pSum)
			'Dim SumTotal As Integer = CInt(Array(0, col))
			'For row = 1 To lastrow - 3
			'	If Array(Array.GetUpperBound(0), col) = "*" Then
			'		If Array(row, col) <> "" Then SumTotal *= CInt(Array(row, col))
			'	Else
			'		If Array(row, col) <> "" Then SumTotal += CInt(Array(row, col))
			'	End If
			'Next
			'total += SumTotal
		Next
		For Each pSum In sums
			total += pSum.result
		Next
		Return total.ToString
	End Function
	Public Overrides Function Part2() As String
		Dim grid = StringToRectangularCharArray(input)
		Dim Sums = New List(Of Sum)
		For Col = 0 To grid.GetUpperBound(1)
			If grid(4, Col) = " " Then Continue For
			Sums.Add(New Sum(grid(4, Col)))
		Next
		Dim currentSum As Integer
		For Col = 0 To grid.GetUpperBound(1)

			Dim NumString As String = grid(0, Col)
			For row = 1 To 3
				NumString = NumString & grid(row, Col)
			Next
			If NumString = "    " Then
				currentSum += 1
			Else Sums(currentSum).Nums.Add(CInt(NumString))
			End If
		Next
		Dim Total As BigInteger
		For Each pSum In sums
			total += pSum.result
		Next
		Return total.ToString
	End Function
End Class
Class Sum
	Property Nums As New List(Of Integer)
	Property Op As String
	Property result As BigInteger
		Get
			Return GetResult()
		End Get
		Set(value As BigInteger)

		End Set
	End Property
	Sub New(_op As String)
		Op = _op
	End Sub
	Function ConvertNums() As List(Of Integer)
		Dim NumString(3, 3) As String
		For i = 0 To 3
			Dim S = Nums(i).ToString
			For j = 0 To S.Length

			Next

		Next

	End Function
	Function GetResult()
		Dim pResult As BigInteger
		If Op = "*" Then
			pResult = 1
			For Each pNum In Nums
				pResult *= pNum
			Next
		Else
			For Each pNum In Nums
				pResult += pNum
			Next
		End If
		Return pResult
	End Function
End Class