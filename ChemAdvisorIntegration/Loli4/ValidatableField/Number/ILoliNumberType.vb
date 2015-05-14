Namespace Loli4.ValidatableField.Number

    Public Interface ILoliNumberType

        ' Defines the interface required for Number fields

        Function IsRange() As Boolean
        Function Operator1() As String
        Function Value1() As Double
        Function Operator2() As String
        Function Value2() As Double

    End Interface

End Namespace