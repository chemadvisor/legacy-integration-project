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

' Represent a number range

' In LOLI4, a number field accepts either a single valid expression, or range.
' A range is of the form <term>-<term>, sometimes referred to as <term_left>-<term_right>

' Both term_left and term_right, must themselves be valid terms.

' Additional constraints on ranges:
'  either both <term> must have prefix, or neither may have a prefix
'     >2-<5 is valid, >2-5 is not.  >2-<=5 should be used
'  either both <term> must have a suffix or neither may have a suffix
'     2%-5% is valid, 2-5 is valid, 2%-5 is not valid
'  the term_left.num must be < term_right.num (2-5 is valid, 5-2 is not)

' Suffixes are either left including or right including, based on the left and right directions on a number line ... -2 -1 0 1 2 ...
'  ">" and ">=" are right including
'  "<" and "<=" are left including

'In a valid range, term_left.num must be < term_right.num
'  term_left can have a prefix, iff its prefix is right including
'  exp2 can have a prefix, iff its prefix is right including

Namespace Loli4.ValidatableField.Number

    Public Class LoliNumberRange
        Inherits LoliNumber
        Implements ILoliValidatableField, ILoliNumberType

        Public ExpLeft As LoliNumberTerm = Nothing
        Public ExpRight As LoliNumberTerm = Nothing
        Public ErrorText As String = Nothing

        Sub New()

            ExpLeft = New LoliNumberTerm()
            ExpRight = New LoliNumberTerm()
            ErrorText = "Uninitialized"
            OriginalValue = String.Empty

        End Sub

        Sub New(ByRef inRange As LoliNumberRange)

            ExpLeft = inRange.ExpLeft
            ExpRight = inRange.ExpRight
            ErrorText = inRange.ErrorText

            OriginalValue = inRange.OriginalValue

        End Sub

        Sub New(ByRef expLeft As LoliNumberTerm, ByRef expRight As LoliNumberTerm)

            Me.ExpLeft = expLeft
            Me.ExpRight = expRight
            ErrorText = "Uninitialized"

        End Sub

        Public Overrides Function IsValid() As Boolean

            If Not ExpLeft.Valid OrElse Not ExpRight.Valid Then

                ErrorText = "Unable parse range" & vbNewLine
                ErrorText &= "Both the left and right terms must be valid. " _
                              & If(ExpLeft.IsValid, String.Empty, "Left term '" & ExpLeft.OriginalValue & "' appears in invalid") _
                              & If(ExpRight.IsValid, String.Empty, "Right term '" & ExpRight.OriginalValue & "' appears in invalid")

                ' If the either expression is invalid, then there's no point trying to validate the range any further
                Return False

            End If

            Return RangeValidate()

        End Function

        Public Overrides Function ToString() As String

            Return ExpLeft.ToString() & " " & TokenSeperator(0) & " " & ExpRight.ToString()

        End Function

        Public Overrides Function GetErrorMessage() As String

            Return ErrorText & If(ExpLeft.ErrorText.Equals(String.Empty), String.Empty, vbNewLine & ExpLeft.ErrorText) _
                   & If(ExpRight.ErrorText.Equals(String.Empty), String.Empty, vbNewLine & ExpRight.ErrorText)

        End Function

        Public Overrides Function GetOriginalString() As String

            Return OriginalValue

        End Function

        Public Overrides Function IsRange() As Boolean

            Return True

        End Function

        Public Overrides Function Operator1() As String

            Return ExpLeft.Prefix

        End Function

        Public Overrides Function Operator2() As String

            Return ExpRight.Prefix

        End Function

        Public Overrides Function Value1() As Double

            Return ExpLeft.Number

        End Function

        Public Overrides Function Value2() As Double

            Return ExpRight.Number

        End Function

        Private Function RangeValidate() As Boolean

            Dim isValid = True
            ErrorText = String.Empty

            If ExpLeft.Number >= ExpRight.Number Then

                ErrorText &= If(ErrorText.Length > 0, vbNewLine, String.Empty)
                ErrorText &= "The left value of a range must be less than the right" & vbNewLine
                ErrorText &= "'" & ExpLeft.Number.ToString() & "' is not less than '" & ExpRight.Number.ToString() & "'"

                isValid = False

            End If

            If ExpLeft.TypeOfPrefix = PrefixType.Right Then

                ErrorText &= If(ErrorText.Length > 0, vbNewLine, String.Empty)
                ErrorText &= "The left term cannot have (" & ExpLeft.Prefix & ") as a prefix, it must be one of the following: " _
                              & Join(ValidLeftIncludingPrefixes, " ") & "."

                isValid = False

            End If

            If ExpRight.TypeOfPrefix = PrefixType.Left Then

                ErrorText &= If(ErrorText.Length > 0, vbNewLine, String.Empty)
                ErrorText &= "The right term cannot have (" & ExpRight.Prefix & ") as a prefix, it must be one of the following: " _
                              & Join(ValidRightIncludingPrefixes, " ") & vbNewLine

                isValid = False

            End If

            If Not isValid Then
                ErrorText = "Unsatisfiable Range." & vbNewLine & "A value cannot be between '" _
                             & ExpLeft.ToString() & "' and " & ExpRight.ToString() & "'" & vbNewLine _
                             & ErrorText
            End If

            If Not ExpLeft.Suffix.Equals(ExpRight.Suffix) Then

                ErrorText &= If(ErrorText.Length > 0, vbNewLine, String.Empty)
                ErrorText &= "Both values must have '" & ValidSuffixes(0) & "' or both must not."

                isValid = False

            End If

            Return isValid

        End Function

    End Class

End Namespace