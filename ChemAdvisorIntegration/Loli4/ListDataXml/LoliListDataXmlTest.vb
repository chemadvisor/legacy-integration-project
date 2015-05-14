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

Namespace Loli4.ListDataXml

    <TestFixture()>
    Public Class LoliListDataTest

        <Test()>
        Public Sub ExampleUse()

            Const xmladmin As String = "<root><fieldlist><field><name>limit</name><type>number</type></field><field><name>range</name><type>number</type></field><field><name>symbol</name><type>codelist</type><delimiter>,</delimiter></field><field><name>category</name><type>list</type><delimiter>,</delimiter></field><field><name>risk</name><type>codelist</type><delimiter>,</delimiter></field><field><name>safety</name><type>codelist</type><delimiter>,</delimiter></field><field><name>additional_information</name><type>text</type></field><field><name>remark</name><type>text</type></field></fieldlist></root>"
            Const listDataXml As String = "<root><row><limit>&gt;=-3.2 - &lt;=5</limit><symbol>T</symbol><risk>R25</risk></row><row><limit>0.25</limit><range>-86 - -74</range><symbol>Xn</symbol><risk>R22</risk></row><row><limit>-10</limit><symbol>Xn</symbol><risk>R21</risk></row><row><limit>1</limit><symbol>T</symbol><risk>R48/23/25</risk></row><row><limit>0.25</limit><range>1</range><symbol>Xn</symbol><risk>R48/20/22</risk></row><row><limit>1</limit><symbol>Xi</symbol><risk>R36/38</risk></row><row><limit>2.5</limit><symbol>N</symbol><risk>R50</risk><risk>R53</risk></row><row><limit>0.25</limit><range>2.5</range><symbol>N</symbol><risk>R51</risk><risk>R53</risk></row><row><limit>0.025</limit><range>0.25</range><risk>R52</risk><risk>R53</risk></row></root>"

            Dim dt As DataTable = Nothing
            NUnit.Framework.Assert.IsTrue(LoliListDataFactory.TryParseWithNumberExpansion(xmladmin, listDataXml, dt))
            NUnit.Framework.Assert.IsTrue(LoliListDataFactory.TryParse(xmladmin, listDataXml, dt))
            Console.WriteLine(dt.ToString)

        End Sub

        <Test()>
        Public Sub DataXmlTest1()
            Dim fileNames As List(Of String) = New List(Of String)({"11_xml.txt", "3091_xml.txt", "816_xml.txt", "2085_xml.txt", "4044_xml.txt", "283_xml.txt"})

            Dim xmlAdmins As List(Of String) = New List(Of String)({"<root><fieldlist><field><name>value</name><type>number</type></field><field><name>unit</name><type>text</type></field><field><name>value2</name><type>number</type><prefix>at_</prefix></field><field><name>unit2</name><type>text</type></field><field><name>method</name><type>text</type><prefix>[</prefix><suffix>]</suffix></field><field><name>remark</name><type>text</type></field><field><name>source</name><type>text</type><exclude-from-aggregate>true</exclude-from-aggregate></field></fieldlist></root>",
                                                                   "<root><fieldlist><field><name>value</name><type>number</type><translate>false</translate></field><field><name>unit</name><type>text</type><translate>true</translate></field><field><name>type</name><type>text</type><translate>true</translate></field><field><name>remark</name><type>text</type><translate>true</translate></field><field><name>listedunder</name><type>text</type><values /><prefix>listed_under_</prefix></field></fieldlist></root>",
                                                                    "<root><parsetable>ListData_Enfielded</parsetable><fieldlist><field><name>value</name><type>number</type></field><field><name>unit</name><type>text</type><translate>true</translate></field><field><name>type</name><type>text</type><translate>true</translate></field><field><name>type_src</name><type>text</type></field><field><name>remark</name><type>list</type><translate>true</translate><values /><delimiter>;</delimiter><prefix>(</prefix></field><field><name>expressedas</name><type>text</type><translate>true</translate><values /><prefix>as_</prefix></field><field><name>formofexposure</name><type>text</type><translate>true</translate><values /></field><field><name>shorttimeexposure</name><type>text</type><translate>true</translate><values /></field><field><name>workplace</name><type>text</type><translate>true</translate><values /></field><field><name>listedunder</name><type>text</type><values /><prefix>listed_under_</prefix></field></fieldlist></root>",
                                                                    "<root><parsetable>ListData_Enfielded</parsetable><fieldlist><field><name>value</name><type>number</type></field><field><name>unit</name><type>text</type><translate>true</translate></field><field><name>type</name><type>text</type><translate>true</translate></field><field><name>remark</name><type>list</type><translate>true</translate><delimiter>;</delimiter><prefix>(</prefix></field><field><name>expressedas</name><type>text</type><translate>true</translate><prefix>as_</prefix></field><field><name>formofexposure</name><type>text</type><translate>true</translate></field><field><name>listedunder</name><type>text</type><prefix>listed_under_</prefix></field></fieldlist></root>",
                                                                    "<root><fieldlist><field><name>value</name><type>number</type></field><field><name>unit</name><type>text</type><translate>true</translate></field><field><name>type</name><type>text</type><translate>true</translate></field><field><name>remark</name><type>text</type><translate>true</translate><prefix>(</prefix></field><field><name>packingmat</name><type>text</type><translate>true</translate><prefix>Packaging_Materials:_</prefix></field></fieldlist></root>",
                                                                    "<root><fieldlist><field><name>value</name><type>text</type></field><field><name>remark</name><type>list</type><translate>true</translate><delimiter>;</delimiter></field><field><name>listedunder</name><type>text</type><values /><prefix>listed_under_</prefix></field></fieldlist></root>"})

            Dim count As Integer = 0
            Dim textLine As String
            Dim adminIndex As Integer = 0

            For Each fileName As String In fileNames

                Dim objReader As New StreamReader("..\..\TestData\" & fileName)

                Do While objReader.Peek() <> -1

                    textLine = objReader.ReadLine()
                    If ProcessComments(textLine) Then
                        Continue Do
                    End If

                    count += 1

                    Dim dt As DataTable = Nothing
                    NUnit.Framework.Assert.IsTrue(LoliListDataFactory.TryParseWithNumberExpansion(xmlAdmins(adminIndex), textLine, dt))
                    TableDumper(dt)


                Loop
                adminIndex += 1

                Console.WriteLine("Checked (" & count.ToString() & ") strings from " & fileName)
            Next

        End Sub

        <Test()>
        Public Sub DataXmlTest2()

            ' Test using instance to factory instead of Shared Method

            Dim fileNames As List(Of String) = New List(Of String)({"11_xml.txt", "3091_xml.txt", "816_xml.txt", "2085_xml.txt", "4044_xml.txt", "283_xml.txt"})

            Dim xmlAdmins As List(Of String) = New List(Of String)({"<root><fieldlist><field><name>value</name><type>number</type></field><field><name>unit</name><type>text</type></field><field><name>value2</name><type>number</type><prefix>at_</prefix></field><field><name>unit2</name><type>text</type></field><field><name>method</name><type>text</type><prefix>[</prefix><suffix>]</suffix></field><field><name>remark</name><type>text</type></field><field><name>source</name><type>text</type><exclude-from-aggregate>true</exclude-from-aggregate></field></fieldlist></root>",
                                                                   "<root><fieldlist><field><name>value</name><type>number</type><translate>false</translate></field><field><name>unit</name><type>text</type><translate>true</translate></field><field><name>type</name><type>text</type><translate>true</translate></field><field><name>remark</name><type>text</type><translate>true</translate></field><field><name>listedunder</name><type>text</type><values /><prefix>listed_under_</prefix></field></fieldlist></root>",
                                                                    "<root><parsetable>ListData_Enfielded</parsetable><fieldlist><field><name>value</name><type>number</type></field><field><name>unit</name><type>text</type><translate>true</translate></field><field><name>type</name><type>text</type><translate>true</translate></field><field><name>type_src</name><type>text</type></field><field><name>remark</name><type>list</type><translate>true</translate><values /><delimiter>;</delimiter><prefix>(</prefix></field><field><name>expressedas</name><type>text</type><translate>true</translate><values /><prefix>as_</prefix></field><field><name>formofexposure</name><type>text</type><translate>true</translate><values /></field><field><name>shorttimeexposure</name><type>text</type><translate>true</translate><values /></field><field><name>workplace</name><type>text</type><translate>true</translate><values /></field><field><name>listedunder</name><type>text</type><values /><prefix>listed_under_</prefix></field></fieldlist></root>",
                                                                    "<root><parsetable>ListData_Enfielded</parsetable><fieldlist><field><name>value</name><type>number</type></field><field><name>unit</name><type>text</type><translate>true</translate></field><field><name>type</name><type>text</type><translate>true</translate></field><field><name>remark</name><type>list</type><translate>true</translate><delimiter>;</delimiter><prefix>(</prefix></field><field><name>expressedas</name><type>text</type><translate>true</translate><prefix>as_</prefix></field><field><name>formofexposure</name><type>text</type><translate>true</translate></field><field><name>listedunder</name><type>text</type><prefix>listed_under_</prefix></field></fieldlist></root>",
                                                                    "<root><fieldlist><field><name>value</name><type>number</type></field><field><name>unit</name><type>text</type><translate>true</translate></field><field><name>type</name><type>text</type><translate>true</translate></field><field><name>remark</name><type>text</type><translate>true</translate><prefix>(</prefix></field><field><name>packingmat</name><type>text</type><translate>true</translate><prefix>Packaging_Materials:_</prefix></field></fieldlist></root>",
                                                                    "<root><fieldlist><field><name>value</name><type>text</type></field><field><name>remark</name><type>list</type><translate>true</translate><delimiter>;</delimiter></field><field><name>listedunder</name><type>text</type><values /><prefix>listed_under_</prefix></field></fieldlist></root>"})

            Dim count As Integer = 0
            Dim textLine As String
            Dim adminIndex As Integer = 0

            For Each fileName As String In fileNames

                Dim adminStr As String = xmlAdmins(adminIndex)
                Dim ldf As LoliListDataFactory = New LoliListDataFactory(adminStr)
                Dim objReader As New StreamReader("..\..\TestData\" & fileName)

                Do While objReader.Peek() <> -1

                    textLine = objReader.ReadLine()

                    If ProcessComments(textLine) Then
                        Continue Do
                    End If

                    count += 1

                    Dim dt As DataTable = Nothing
                    NUnit.Framework.Assert.IsTrue(ldf.TryParseWithNumberExpansion(textLine, dt))
                    TableDumper(dt)

                Loop
                adminIndex += 1

                Console.WriteLine("Checked (" & count.ToString() & ") strings from " & fileName)

            Next

        End Sub

        Private Shared Function ProcessComments(ByVal textLine As String) As Boolean

            If textLine.Length = 0 Then
                Return False
            End If

            If textLine.StartsWith("#") Then
                Console.WriteLine("Input File Comment: " & textLine)
                Return False
            End If

            Return True

        End Function

        Private Shared Sub TableDumper(ByVal dt As DataTable)

            For Each col As Object In dt.Columns
                Console.Write(col.ToString & " | ")
            Next

            Console.WriteLine()

            For Each row As DataRow In dt.Rows

                For Each col As Object In dt.Columns
                    Console.Write(row.Item(col).ToString & " | ")
                Next
                Console.WriteLine()

            Next

            Console.WriteLine()

        End Sub

    End Class

End Namespace


