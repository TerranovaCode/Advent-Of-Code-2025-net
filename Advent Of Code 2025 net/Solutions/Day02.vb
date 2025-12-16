Imports System.Linq.Expressions
Imports System.Numerics
Class Day02
	Inherits DayTemplate
	Sub New(_Input)
		MyBase.New(_Input)
	End Sub
	Public Overrides Function Part1() As String

		Dim ID_Ranges = input.Split(",")
		Dim WrongIDSum As BigInteger = 0

		For Each pRange In ID_Ranges
			Dim i As BigInteger
			Dim FirstInt As BigInteger = BigInteger.Parse(pRange.Split("-").First)
			Dim LastInt As BigInteger = BigInteger.Parse(pRange.Split("-").Last)
			For i = FirstInt To LastInt
				If IDisSilly(i) Then
					WrongIDSum += i
				End If
			Next
		Next

		Return WrongIDSum.ToString
	End Function
	Private Function IDisSilly(ID As BigInteger) As Boolean
		Dim pID As String = If(ID.ToString(), String.Empty)
		Dim len As Integer = pID.Length
		If len = 0 OrElse (len Mod 2) <> 0 Then
			Return False  ' must be even length
		End If

		Dim mid As Integer = len \ 2  ' integer division
		Dim firstHalf As String = pID.Substring(0, mid)
		Dim secondHalf As String = pID.Substring(mid)

		Return firstHalf = secondHalf
	End Function
	Public Overrides Function Part2() As String
		Dim ID_Ranges = input.Split(",")
		Dim WrongIDSum As BigInteger = 0

		For Each pRange In ID_Ranges
			Dim i As BigInteger
			Dim FirstInt As BigInteger = BigInteger.Parse(pRange.Split("-").First)
			Dim LastInt As BigInteger = BigInteger.Parse(pRange.Split("-").Last)
			For i = FirstInt To LastInt
				If IDisSillyP2(i) Then
					WrongIDSum += i
				End If
			Next
		Next
		Return WrongIDSum.ToString
	End Function
	Private Function IDisSillyP2(ID As BigInteger) As Boolean
		Dim pID As String = If(ID.ToString(), String.Empty)
		Dim len As Integer = pID.Length
		For i = 1 To len / 2
			If StringBuilderPattern(pID.Substring(0, i), len / i) = pID Then Return True
		Next
		Return False
	End Function
	Private Function StringBuilderPattern(Sstring As String, RepCount As Integer) As String
		Dim pRet As String
		For i = 1 To RepCount
			pRet = pRet & Sstring
		Next
		Return pRet
	End Function
End Class
