Imports System.Numerics

Class Day05
	Inherits DayTemplate
	Sub New(_Input)
		MyBase.New(_Input)
	End Sub
	Public Overrides Function Part1() As String
		Dim InputLines = SplitLines(input)
		Dim pFreshRange = GetFreshRanges(InputLines)
		Dim pAvailable = GetAvailableIngredients(InputLines)
		Dim FreshAvliableIngredients As Integer
		For Each pIngredient In pAvailable
			Dim pIngridentHasBeenAdded As Boolean = False
			For Each pRange As String In pFreshRange
				Dim FirstInt As BigInteger = BigInteger.Parse(pRange.Split("-").First)
				Dim LastInt As BigInteger = BigInteger.Parse(pRange.Split("-").Last)
				If pIngredient > FirstInt AndAlso pIngredient < LastInt Then
					If Not pIngridentHasBeenAdded Then
						FreshAvliableIngredients += 1
						pIngridentHasBeenAdded = True
					End If
				End If
			Next
		Next
		Return FreshAvliableIngredients
	End Function
	Public Overrides Function Part2() As String

		Dim InputLines = SplitLines(input)
		Dim pFreshRange = GetFreshRanges(InputLines)
		Dim StructuredRanges As New List(Of FreshRange)
		For Each s In pFreshRange
			Dim FirstInt As BigInteger = BigInteger.Parse(s.Split("-").First)
			Dim LastInt As BigInteger = BigInteger.Parse(s.Split("-").Last)
			Dim pRange As New FreshRange(FirstInt, LastInt)
			StructuredRanges.Add(pRange)
		Next

		StructuredRanges.Sort(Function(x, y)
								  Dim c = x.StartInt.CompareTo(y.StartInt)
								  If c <> 0 Then Return c
								  Return x.EndInt.CompareTo(y.EndInt)
							  End Function)
		Dim Total As BigInteger = 0
		Dim currentStart = StructuredRanges(0).StartInt
		Dim currentEnd = StructuredRanges(0).EndInt
		For i = 1 To StructuredRanges.Count - 1
			Dim pRange = StructuredRanges(i)
			If pRange.StartInt <= currentEnd Then
				If pRange.StartInt < currentStart Then currentStart = pRange.StartInt
				If currentEnd < pRange.EndInt Then currentEnd = pRange.EndInt
			Else Total += currentEnd - currentStart + 1
				currentStart = pRange.StartInt
				currentEnd = pRange.EndInt
			End If
		Next
		Total += currentEnd - currentStart + 1
		Return Total.ToString
	End Function
	Private Function GetFreshRanges(Lines As String()) As List(Of String)
		Dim pRet As New List(Of String)
		For Each pRange As String In Lines
			If pRange.Contains("-") Then pRet.Add(pRange)
		Next
		Return pRet
	End Function
	Friend Function GetAvailableIngredients(Lines As String()) As List(Of BigInteger)
		Dim pRet As New List(Of BigInteger)
		For Each pIngredient As String In Lines
			If pIngredient.Contains("-") Then
				Continue For
			ElseIf pIngredient = Nothing Then
				Continue For
			Else
				pRet.Add(BigInteger.Parse(pIngredient))
			End If
		Next
		Return pRet
	End Function

End Class
Public Structure FreshRange
	Public StartInt As BigInteger
	Public EndInt As BigInteger
	Public Sub New(a As BigInteger, B As BigInteger)
		If a <= B Then
			StartInt = a
			EndInt = B
		Else
			StartInt = B
			EndInt = a
		End If
	End Sub
End Structure