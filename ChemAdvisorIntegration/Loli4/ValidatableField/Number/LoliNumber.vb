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

' Abstract base class for representation of a number Expression

' For the purpose of LOLI4 number field validation, an expression is defined as follows:
' An expression is either a single valid term, or a range (two valid terms separated by a dash ('-'))
' <exp> ==> <term>|<term_left>-<term_right>

' All classes that implement number field validate for LOLI should be derive from this class

' A <term> is defined as [prefix]<num>[<space>postfix]
' [prefix], optional, is any one token from the set {">", "<", "<=", "<="}
' <num>, required, is any string for which Microsoft .Net's IsNumeric(<num>) returns TRUE
' A valid suffix is any one token from the set {" %"}, note the suffix token is '<space>%' not '%'

' In LOLI4, a number field accepts either a single valid expression, or range.
' A range is of the form <term>-<term>, sometimes referred to as <term_left>-<term_right>

Namespace Loli4.ValidatableField.Number

    Public MustInherit Class LoliNumber

        Implements ILoliNumberType, ILoliValidatableField

        Enum PrefixType
            None
            Left
            Right
        End Enum

        ' In the arrays below, the longer prefixes need to appear before the shorter ones
        ' or String.Split will not split as desired

        Public Shared ValidPrefixes As String() = New String() {">=", "<=", ">", "<"}
        Public Shared ValidRightIncludingPrefixes As String() = New String() {"<", "<="}
        Public Shared ValidLeftIncludingPrefixes As String() = New String() {">", ">="}
        Public Shared ValidSuffixes As String() = New String() {"%"}
        Public Shared TokenSeperator As String() = New String() {"-"}

        Public OriginalValue As String

        Public MustOverride Function GetErrorMessage() As String Implements ILoliValidatableField.GetErrorMessage
        Public MustOverride Function GetOriginalString() As String Implements ILoliValidatableField.GetOriginalString
        Public MustOverride Function IsValid() As Boolean Implements ILoliValidatableField.IsValid

        Public MustOverride Function IsRange() As Boolean Implements ILoliNumberType.IsRange
        Public MustOverride Function Operator1() As String Implements ILoliNumberType.Operator1
        Public MustOverride Function Value1() As Double Implements ILoliNumberType.Value1
        Public MustOverride Function Operator2() As String Implements ILoliNumberType.Operator2
        Public MustOverride Function Value2() As Double Implements ILoliNumberType.Value2

    End Class

End Namespace