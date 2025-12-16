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