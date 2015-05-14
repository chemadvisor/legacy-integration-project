'   The MIT License (MIT)

'   Copyright (c) 2015 ChemADVISOR, Inc.

'   Permission is hereby granted, free of charge, to any person obtaining a copy
'   of this software and associated documentation files (the "Software"), to deal
'   in the Software without restriction, including without limitation the rights
'   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
'   copies of the Software, and to permit persons to whom the Software is
'   furnished to do so, subject to the following conditions:

'   The above copyright notice and this permission notice shall be included in
'   all copies or substantial portions of the Software.

'   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
'   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
'   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
'   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
'   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
'   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
'   THE SOFTWARE.

' Represent a single value numeric expression

' A <term> is defined as [prefix]<num>[<space>postfix]
' [prefix], optional, is any one token from the set {">", "<", "<=", "<="}
' <num>, required, is any string for which Microsoft .Net's IsNumeric(<num>) returns TRUE
' A valid suffix is any one token from the set {" %"}, note the suffix token is '<space>%' not '%'

' examples: 5, 5.625, >8, <=-3.2

Namespace Loli4.ValidatableField.Number

    Public Class LoliNumberTerm
        Inherits LoliNumber
        Implements ILoliValidatableField, ILoliNumberType

        Public Prefix As String = Nothing
        Public TypeOfPrefix As PrefixType = Nothing
        Public Suffix As String = Nothing
        Public Sign As Integer = Nothing
        Public Number As Double = Nothing
        Public NumberString As String = Nothing
        Public ErrorText = Nothing

#Region "Constructors"

        Sub New()

            Prefix = String.Empty
            TypeOfPrefix = PrefixType.None
            Suffix = String.Empty
            Sign = 0
            Number = 0.0
            NumberString = String.Empty
            Valid = False
            ErrorText = String.Empty
            OriginalValue = String.Empty

        End Sub

        Sub New(exp As LoliNumberTerm)

            Prefix = exp.Prefix
            TypeOfPrefix = exp.Prefix
            Suffix = String.Empty
            Sign = exp.Sign
            Number = exp.Number
            NumberString = exp.NumberString
            Valid = exp.Valid
            ErrorText = exp.ErrorText
            OriginalValue = exp.OriginalValue

        End Sub

        Public Property Valid As Boolean = Nothing

#End Region

#Region "ILoliValidatableField Interface Implementation"

        Public Overrides Function IsValid() As Boolean

            Return Valid

        End Function

        Public Overrides Function ToString() As String

            If NumberString.EndsWith(".") Then
                NumberString = NumberString.Remove(NumberString.Length - 1)
            End If

            Return Prefix & IIf(Sign < 0, "-", String.Empty) & NumberString & IIf(Suffix.Equals(String.Empty), String.Empty, " " & Suffix)

        End Function

        Public Overrides Function GetErrorMessage() As String

            Return ErrorText

        End Function

        Public Overrides Function GetOriginalString() As String

            Return OriginalValue

        End Function

#End Region

#Region "Private methods"

        Public Shared Function TestMe() As Boolean

            Return True

        End Function

        Public Function IsRightIncluding() As Boolean

            Return TypeOfPrefix = PrefixType.Right

        End Function

        Public Function IsLeftIncluding() As Boolean

            Return TypeOfPrefix = PrefixType.Left

        End Function

#End Region

        Public Overrides Function Operator1() As String

            Return Prefix

        End Function

        Public Overrides Function Value1() As Double

            Return Number

        End Function

        Public Overrides Function Operator2() As String

            Return Nothing

        End Function


        Public Overrides Function Value2() As Double

            Return Nothing

        End Function

        Public Overrides Function IsRange() As Boolean

            Return False

        End Function

    End Class

End Namespace