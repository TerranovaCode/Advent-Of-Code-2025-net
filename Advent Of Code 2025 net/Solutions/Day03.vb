Imports System.Numerics

Class Day03
	Inherits DayTemplate
	Sub New(_Input)
		MyBase.New(_Input)
	End Sub
	Public Overrides Function Part1() As String
		Dim pInputBanks = SplitLines(input)
		Dim MaxJolts As Integer
		For Each pInputBank In pInputBanks
			Dim pBattery As New JoltageBattery(pInputBank)
			MaxJolts += pBattery.MaxJolt
		Next
		Return MaxJolts
	End Function
	Public Overrides Function Part2() As String
		Dim pInputBanks = SplitLines(input)
		Dim MaxMultJolts As BigInteger
		Dim Counter As Integer
		For Each pInputBank In pInputBanks
			Counter += 1
			Dim pBattery As New JoltageBattery(pInputBank)
			MaxMultJolts += BigInteger.Parse(pBattery.LargestSubsequenceOfLengthK(pBattery.Bank, 12))
		Next
		Return MaxMultJolts.ToString
	End Function

End Class
Class JoltageBattery
	Property Bank As String
	Property BankCells As New List(Of JoltBatteryCell)
	Property MaxJolt As Integer
		Get
			Return CalcMaxJolt()
		End Get
		Set(value As Integer)

		End Set
	End Property
	Sub New(_Bank As String)
		Bank = _Bank
	End Sub
	Private Function CalcMaxJolt() As Integer
		Dim MaxJolt = 0
		For i = 0 To Bank.Length - 1
			Dim FirstNum As Integer = CInt(Char.GetNumericValue(Bank.Chars(i)))
			For j = 0 To Bank.Length - 1
				If j <= i Then Continue For
				Dim SecondNum As Integer = CInt(Char.GetNumericValue(Bank.Chars(j)))
				If FirstNum & SecondNum > MaxJolt Then MaxJolt = FirstNum & SecondNum
			Next
		Next
		Return MaxJolt
	End Function
	Friend Function MaxMultiVolt()
		For i = 0 To Bank.Length - 1
			BankCells.Add(New JoltBatteryCell(CInt(Char.GetNumericValue(Bank.Chars(i)))))
		Next
		For Each pCell As JoltBatteryCell In BankCells
			If CellOnCount() = 12 Then Exit For
			For i = 0 To 8
				If CellOnCount() = 12 Then Exit For
				If pCell.Value = 9 - i Then pCell.IsOn = True
			Next
		Next
		Dim MaxiMult As String
		For Each pCell As JoltBatteryCell In BankCells
			If pCell.IsOn Then MaxiMult = MaxiMult & pCell.Value
		Next
		Return MaxiMult
	End Function
	Friend Function CellOnCount()
		Dim OnCount As Integer
		For Each pCell As JoltBatteryCell In BankCells
			If pCell.IsOn Then OnCount += 1
		Next
		Return OnCount
	End Function
	Friend Function LargestSubsequenceOfLengthK(s As String, k As Integer) As String
		Dim n As Integer = s.Length
		Dim stack As New List(Of Char)(k)

		For i As Integer = 0 To n - 1
			Dim d As Char = s(i)
			' Number of characters remaining including current position
			Dim remainingIncl As Integer = n - i

			While stack.Count > 0 AndAlso stack(stack.Count - 1) < d AndAlso (stack.Count - 1) + remainingIncl >= k
				stack.RemoveAt(stack.Count - 1)
			End While

			If stack.Count < k Then
				stack.Add(d)
			End If
		Next

		Return New String(stack.ToArray())
	End Function
End Class

Class JoltBatteryCell
	Property Value As Integer
	Property IsOn As Boolean = False
	Sub New(_Value As Integer)
		Value = _Value
	End Sub
End Class
