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

' Factory method for creating valid instances of Loli Numerics

Imports System.Linq

Namespace Loli4.ValidatableField.Number

    Public Class LoliNumberFactory

        Public Shared Function TryParse(ByVal s As String, ByRef expression As ILoliValidatableField) As Boolean

            If s Is Nothing Then
                Return False
            End If

            If s.Equals(String.Empty) Then
                Return True
            End If

            Dim inStr As String = s
            Dim tokens() As String = inStr.Split(LoliNumber.TokenSeperator, StringSplitOptions.None)

            If tokens.Count = 0 Then
                Return False
            End If

            Dim termLeft As LoliNumberTerm = Nothing
            Dim termRight As LoliNumberTerm = Nothing
            Dim range As LoliNumberRange = Nothing

            Select Case tokens.Count

                Case 1

                    termLeft = ValidatePositiveTerm(tokens(0))
                    termLeft.OriginalValue = s
                    expression = termLeft

                Case 2

                    termLeft = ValidatePositiveTerm(tokens(0))

                    If termLeft.Valid Then

                        termRight = ValidatePositiveTerm(tokens(1))
                        range = If(termLeft.Number > termRight.Number, New LoliNumberRange(termRight, termLeft), New LoliNumberRange(termLeft, termRight))
                        range.OriginalValue = s
                        expression = range

                    Else

                        termLeft = ValidatePositiveTerm(tokens(0) & "-" & tokens(1))
                        termLeft.OriginalValue = s
                        expression = termLeft

                    End If

                Case 3

                    termLeft = ValidatePositiveTerm(tokens(0))

                    If termLeft.IsValid() Then

                        termRight = ValidatePositiveTerm(tokens(1) & "-" & tokens(2))

                    Else

                        termLeft = ValidatePositiveTerm(tokens(0) & "-" & tokens(1))
                        termRight = ValidatePositiveTerm(tokens(2))

                    End If

                    range = If(termLeft.Number > termRight.Number, New LoliNumberRange(termRight, termLeft), New LoliNumberRange(termLeft, termRight))
                    range.OriginalValue = s
                    expression = range

                Case 4

                    termLeft = ValidatePositiveTerm(tokens(0) & "-" & tokens(1))

                    If termLeft.IsValid() Then
                        termRight = ValidatePositiveTerm(tokens(2) & "-" & tokens(3))
                    Else
                        termRight = New LoliNumberTerm()
                    End If

                    range = If(termLeft.Number > termRight.Number, New LoliNumberRange(termRight, termLeft), New LoliNumberRange(termLeft, termRight))
                    range.OriginalValue = s
                    expression = range

            End Select

            Return expression.IsValid()

        End Function

        Private Shared Function ValidatePositiveTerm(ByVal inStr As String) As LoliNumberTerm

            Dim term As LoliNumberTerm = New LoliNumberTerm()
            term.OriginalValue = inStr
            inStr = inStr.Trim()

            If inStr.Length = 0 Then
                Return term
            End If

            For Each s As String In LoliNumber.ValidSuffixes

                If inStr.EndsWith(s) Then
                    term.Suffix = s
                    inStr = Trim(inStr.Remove(inStr.LastIndexOf(s, StringComparison.Ordinal)))
                    Exit For
                End If

            Next

            For Each s As String In LoliNumber.ValidPrefixes

                If inStr.StartsWith(s) Then

                    term.Prefix = s

                    If LoliNumber.ValidLeftIncludingPrefixes.Contains(s) Then
                        term.TypeOfPrefix = LoliNumber.PrefixType.Left
                        inStr = inStr.Remove(0, s.Length)
                        Exit For
                    End If

                    If LoliNumber.ValidRightIncludingPrefixes.Contains(s) Then
                        term.TypeOfPrefix = LoliNumberTerm.PrefixType.Right
                        inStr = inStr.Remove(0, s.Length)
                        Exit For
                    End If

                End If

            Next
            inStr = inStr.Trim()

            If Double.TryParse(inStr, term.Number) Then

                term.Valid = True

                ' ReSharper disable once CompareOfFloatsByEqualityOperator
                If term.Number = 0 Then
                    term.Sign = 0
                ElseIf term.Number > 0 Then
                    term.Sign = 1
                Else
                    term.Sign = -1
                End If

                term.NumberString = inStr.Replace("-", String.Empty)

            Else

                Dim aChar() As Char = inStr
                Dim prefix As String = ""
                term.Valid = False
                term.ErrorText = "Unable to parse '" & inStr & "' as a number."

                For i As Integer = 0 To aChar.Length - 1
                    If Not IsNumeric(aChar(i)) Then
                        prefix &= aChar(i)
                    Else
                        Exit For
                    End If
                Next

                If prefix.Length > 0 Then
                    If prefix.Length < inStr.Length Then
                        term.ErrorText &= vbNewLine & "'" & prefix & "' is not a valid prefix."
                    End If
                End If

            End If

            Return term

        End Function

    End Class

End Namespace