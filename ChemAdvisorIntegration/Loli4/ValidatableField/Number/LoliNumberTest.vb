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

Imports System.IO
Imports NUnit.Framework

Namespace Loli4.ValidatableField.Number

    <TestFixture()>
    Public Class LoliNumberTest

        <Test()>
        Public Sub ExampleUse()

            Dim expression As ILoliNumberType = Nothing
            ' create an input string
            Const inputString As String = "10 - 20a"
            If LoliNumberFactory.TryParse(inputString, expression) Then

                'success
                'expression contains an instance of ILoliNumber
                'that can be used in calculations and also formats nicely
                Console.WriteLine(expression.ToString)

                If expression.IsRange Then

                    'dealing with a range, there's information 
                    'in both value1 and value2

                    Console.WriteLine("lval: " & expression.Operator1 & expression.Value1 * 42)
                    Console.WriteLine("rval: " & expression.Operator2 & expression.Value2 * 42)

                Else

                    'dealing with a term, there's no information in value2
                    'only in value1

                    Console.WriteLine("lval: " & expression.Operator1 & expression.Value1 * 42)

                End If

            Else

                'fail
                'expression contains an instance of ILoliNumber where
                'the error message is populated.  This may be useful
                'information but the instance should not be used in calculation
                'to get the error message expression must be cast to ILoliValidatableField

                Console.WriteLine((TryCast(expression, ILoliValidatableField).GetErrorMessage))

                'But we could just use to string to see the error message and avoid casting

                Console.WriteLine(expression.ToString)

            End If

            'Make this look list a successful test
            Assert.IsTrue(True)

        End Sub


        <Test()>
        Public Sub NumberTest1()

            Const fileName As String = "..\..\TestData\valid_numbers.txt"

            Dim count As Integer = 0
            Dim textLine As String

            Dim objReader As New StreamReader(fileName)

            Do While objReader.Peek() <> -1

                textLine = objReader.ReadLine()

                If textLine.Length = 0 Then
                    Continue Do
                End If

                If textLine.StartsWith("#") Then
                    Console.WriteLine("Input File Comment: " & textLine)
                    Continue Do
                End If

                count += 1

                Dim expression As ILoliValidatableField = Nothing

                Assert.IsTrue(LoliNumberFactory.TryParse(textLine, expression), "Line " & count & ":" & textLine)
                Assert.IsTrue(expression.IsValid(), "Line " & count & ":" & textLine)
                Assert.AreEqual(textLine, expression.ToString())
                Assert.AreEqual(String.Empty, expression.GetErrorMessage())
                Assert.AreEqual(textLine, expression.GetOriginalString())

            Loop

            Console.WriteLine("Checked (" & count.ToString() & ") strings from " & fileName)

        End Sub

        <Test()>
        Public Sub NumberTest2()

            Const fileName As String = "..\..\TestData\numbers_to_reformat.txt"

            Dim count As Integer = 0
            Dim textLine As String
            Dim tokenSeperator As String() = New String() {"|"}

            Dim objReader As New StreamReader(fileName)

            Do While objReader.Peek() <> -1

                textLine = objReader.ReadLine()
                Dim testValues As String() = textLine.Split(tokenSeperator, StringSplitOptions.None)

                If textLine.StartsWith("#") Then
                    Console.WriteLine("Input File Comment: " & textLine)
                    Continue Do
                End If
                If textLine.Length = 0 Then
                    Continue Do
                End If

                count += 1

                Dim expression As ILoliValidatableField = Nothing

                Assert.IsTrue(LoliNumberFactory.TryParse(testValues(0), expression), "Line " & count & ":" & textLine)
                Assert.IsTrue(expression.IsValid(), "Line " & count & ":" & textLine)
                Assert.AreEqual(testValues(1), expression.ToString())
                Assert.AreEqual(String.Empty, expression.GetErrorMessage())
                Assert.AreEqual(testValues(0), expression.GetOriginalString())

            Loop

            Console.WriteLine("Checked (" & count.ToString() & ") strings from " & fileName)

        End Sub

        <Test()>
        Public Sub NumberTest3()

            Const fileName As String = "..\..\TestData\invalid_numbers.txt"

            Dim count As Integer = 0
            Dim textLine As String

            Dim objReader As New StreamReader(fileName)

            Do While objReader.Peek() <> -1

                textLine = objReader.ReadLine()

                If textLine.StartsWith("#") Then
                    Console.WriteLine("Input File Comment: " & textLine)
                    Continue Do
                End If
                If textLine.Length = 0 Then
                    Continue Do
                End If

                count += 1

                Dim expression As ILoliValidatableField = Nothing

                Assert.IsFalse(LoliNumberFactory.TryParse(textLine, expression), "Line " & count & ":" & textLine)
                LoliNumberFactory.TryParse(textLine, expression)
                If Not expression.IsValid() Then
                    Console.WriteLine(count & ": " & textLine)
                    Console.WriteLine(vbTab & expression.GetErrorMessage())
                End If
                Assert.IsFalse(expression.IsValid(), "Line " & count & ":" & textLine)

            Loop

            Console.WriteLine("Checked (" & count.ToString() & ") strings from " & fileName)
        End Sub

        <Test()>
        Public Sub NumberTest4()

            Const fileName As String = "..\..\TestData\valid_numbers_small_set.txt"

            Dim textLine As String

            Dim objReader As New StreamReader(fileName)

            Do While objReader.Peek() <> -1

                textLine = objReader.ReadLine()

                If textLine.StartsWith("#") Then
                    Continue Do
                End If
                If textLine.Length = 0 Then
                    Continue Do
                End If

                Dim expression As ILoliNumberType = Nothing

                LoliNumberFactory.TryParse(textLine, expression)

                If expression.IsRange Then
                    Assert.AreEqual(expression.ToString, AggregateNumberFieldsForRange(expression))
                Else
                    Assert.AreEqual(expression.ToString, AggregateNumberFieldsForTerm(expression))
                End If

            Loop

        End Sub

        Private Function AggregateNumberFieldsForTerm(ByVal expression As ILoliNumberType) As String

            Return expression.Operator1 & expression.Value1

        End Function

        Private Function AggregateNumberFieldsForRange(ByVal expression As ILoliNumberType) As String

            Return expression.Operator1 & expression.Value1 & " - " & expression.Operator2 & expression.Value2

        End Function

    End Class

End Namespace