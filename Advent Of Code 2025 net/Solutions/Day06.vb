Class Day06
	Inherits DayTemplate
	Sub New(_Input)
		MyBase.New(_Input)
	End Sub
	Public Overrides Function Part1() As String
		Throw New NotImplementedException()
	End Function
	Public Overrides Function Part2() As String
		Throw New NotImplementedException()
	End Function
End Class
Class Sum
	Property Elements As New List(Of Integer)
	Property Operation As MathOperation
	Property Result As Integer
		Get
			Dim pRet As Integer = Elements(1)
			For i = 2 To Elements.Count
				Select Case Operation
					Case MathOperation.Multiplty
						pRet *=
					Case MathOperation.Plus

				End Select
			Next

		End Get
		Set(value As Integer)

		End Set
	End Property
End Class