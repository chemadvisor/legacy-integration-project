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

Imports System.Xml
Imports ChemAdvisorIntegration.Loli4.ValidatableField.Number

Namespace Loli4.ListDataXml

    Public Class LoliListDataFactory

        Public Class XmlAdminField

            Public Name As String = Nothing
            Public DisplayName As String = Nothing
            Public Type As String = Nothing
            Public Delimiter As String = Nothing
            Public Values As New List(Of String)

        End Class

        Private ReadOnly _adminFields As Dictionary(Of String, XmlAdminField)
        Private Const NumberListType As String = "number"

#Region "Constructors"

        Sub New(xmlAdmin As String)

            _adminFields = ParseAdminString(XmlDocFromString(xmlAdmin))

        End Sub

        Sub New(doc As XmlDocument)

            _adminFields = ParseAdminString(doc)

        End Sub

#End Region

#Region "TryParse"

        'Instance Flavor

        Public Function TryParse(listDataXml As String, ByRef dt As DataTable) As Boolean

            Try

                Return TryParse(XmlDocFromString(listDataXml), dt)

            Catch ex As Exception

                Return False

            End Try

        End Function

        Public Function TryParse(listDataXml As XmlDocument, ByRef dt As DataTable) As Boolean

            Try

                ParseWithNumberExpansionImpl(_adminFields, dt, listDataXml, False)

            Catch ex As Exception

                Return False

            End Try

            Return True

        End Function


        'Shared Flavor

        Public Shared Function TryParse(ByVal xmlAdmin As String, listDataXml As String, ByRef dt As DataTable) As Boolean

            Try

                Return TryParse(XmlDocFromString(xmlAdmin), XmlDocFromString(listDataXml), dt)

            Catch ex As Exception

                Return False

            End Try

        End Function

        Public Shared Function TryParse(ByVal xmlAdmin As XmlDocument, listDataXml As XmlDocument, ByRef dt As DataTable) As Boolean

            Try

                ParseWithNumberExpansionImpl(ParseXmlAdmin(xmlAdmin), dt, listDataXml, False)

            Catch ex As Exception

                Return False

            End Try

            Return True

        End Function

#End Region

#Region "TryParseWithNumberExpansion"

        'Instance Flavor

        Public Function TryParseWithNumberExpansion(listDataXml As String, ByRef dt As DataTable) As Boolean

            Try

                Return TryParseWithNumberExpansion(XmlDocFromString(listDataXml), dt)

            Catch ex As Exception

                Return False

            End Try

        End Function

        Public Function TryParseWithNumberExpansion(listDataXml As XmlDocument, ByRef dt As DataTable) As Boolean

            Try

                ParseWithNumberExpansionImpl(_adminFields, dt, listDataXml, True)

            Catch ex As Exception

                Return False

            End Try

            Return True

        End Function


        'Shared Flavor

        Public Shared Function TryParseWithNumberExpansion(ByVal xmlAdmin As String, listDataXml As String, ByRef dt As DataTable) As Boolean

            Try

                Return TryParseWithNumberExpansion(XmlDocFromString(xmlAdmin), XmlDocFromString(listDataXml), dt)

            Catch ex As Exception

                Return False

            End Try

        End Function

        Public Shared Function TryParseWithNumberExpansion(ByVal xmlAdmin As XmlDocument, listDataXml As XmlDocument, ByRef dt As DataTable) As Boolean

            Try

                ParseWithNumberExpansionImpl(ParseXmlAdmin(xmlAdmin), dt, listDataXml, True)

            Catch ex As Exception

                Return False

            End Try

            Return True

        End Function

#End Region

#Region "Private"

        Private Shared Function ParseAdminString(ByVal xml As XmlDocument) As Dictionary(Of String, XmlAdminField)

            Return ParseXmlAdmin(xml)

        End Function

        Private Shared Function XmlDocFromString(ByVal xmlString As String) As XmlDocument

            Dim doc As XmlDocument = New XmlDocument()
            doc.LoadXml(xmlString)
            Return doc

        End Function

        Private Shared Sub ParseWithNumberExpansionImpl(ByVal parsedFormat As Dictionary(Of String, XmlAdminField), ByRef dt As DataTable, ByVal listDataXml As XmlDocument, ByRef expandNumbers As Boolean)

            dt = New DataTable
            InitializeColumns(parsedFormat, dt, expandNumbers)
            PopulateDataTableFromXml(listDataXml, parsedFormat, dt, expandNumbers)

        End Sub

        Private Shared Sub InitializeColumns(ByVal parsedFormat As Dictionary(Of String, XmlAdminField), ByVal dataTable As DataTable, ByVal expandNumbers As Boolean)

            For Each key As String In parsedFormat.Keys

                If expandNumbers AndAlso parsedFormat(key).Type = NumberListType Then
                    dataTable.Columns.Add(key)
                    dataTable.Columns.Add(key & "_OP1")
                    dataTable.Columns.Add(key & "_NUM1", Type.GetType("System.Double"))
                    dataTable.Columns.Add(key & "_OP2")
                    dataTable.Columns.Add(key & "_NUM2", Type.GetType("System.Double"))
                Else
                    dataTable.Columns.Add(key)
                End If

            Next

        End Sub

        Private Shared Sub PopulateDataTableFromXml(ByVal doc As XmlDocument, parsedFormat As Dictionary(Of String, XmlAdminField), ByRef dt As DataTable, ByRef expandNumbers As Boolean)

            For Each row As XmlNode In doc.DocumentElement.SelectNodes("row")

                Dim dr As DataRow = dt.NewRow
                AddRowFromNode(row, dr, parsedFormat, expandNumbers)
                dt.Rows.Add(dr)

            Next

        End Sub

        Private Shared Sub AddRowFromNode(rowNode As XmlNode, dr As DataRow, parsedFormat As Dictionary(Of String, XmlAdminField), ByVal expandNumbers As Boolean)

            For Each node As XmlNode In rowNode.ChildNodes

                Dim key As String = node.Name
                If parsedFormat.ContainsKey(key) Then

                    If Not IsNothing(rowNode.SelectSingleNode(key)) Then

                        If expandNumbers AndAlso parsedFormat(key).Type = NumberListType Then
                            LoadRange(key, rowNode.SelectSingleNode(key).InnerText, dr)

                        Else
                            dr.Item(key) = rowNode.SelectSingleNode(key).InnerText
                        End If

                    End If

                Else
                    Throw New Exception("xml does not match admin")
                End If
            Next

        End Sub

        Private Shared Sub LoadRange(ByVal key As String, ByVal text As String, ByVal dr As DataRow)

            Dim expression As LoliNumber = Nothing
            LoliNumberFactory.TryParse(text, expression)
            dr.Item(key) = expression.ToString

            If expression.IsValid Then
                Dim s As String = key & "_OP1"
                dr.Item(s) = expression.Operator1

                s = key & "_NUM1"
                dr.Item(s) = expression.Value1
                ProcessRange(expression, dr, key)

            Else
                Throw New Exception("Can not parse number field: numberText")
            End If

        End Sub

        Private Shared Sub ProcessRange(ByVal expression As LoliNumber, ByVal dr As DataRow, ByVal key As String)

            Dim s As String

            If expression.IsRange Then
                s = key & "_OP2"
                dr.Item(s) = expression.Operator2

                s = key & "_NUM2"
                dr.Item(s) = expression.Value2
            Else
                s = key & "_OP2"
                dr.Item(s) = DBNull.Value

                s = key & "_NUM2"
                dr.Item(s) = DBNull.Value
            End If

        End Sub

        Private Shared Function ParseXmlAdmin(ByVal doc As XmlDocument) As Dictionary(Of String, XmlAdminField)

            Dim xmlAdminFields As New Dictionary(Of String, XmlAdminField)

            For Each field As XmlNode In doc.DocumentElement.SelectSingleNode("/root/fieldlist").SelectNodes("field")

                Dim xmlField = New XmlAdminField
                xmlField.Name = field.SelectSingleNode("name").InnerText
                xmlField.Type = field.SelectSingleNode("type").InnerText
                ParseAdminDelimiterTag(xmlField, field)
                ParseAdminValuesTag(xmlField, field)
                xmlAdminFields.Add(xmlField.Name, xmlField)

            Next

            Return xmlAdminFields

        End Function

        Private Shared Sub ParseAdminDelimiterTag(ByVal xmlField As XmlAdminField, ByVal field As XmlNode)

            If Not IsNothing(field.InnerXml) Then

                Dim xmlNode As XmlNode = field.SelectSingleNode("delimiter")
                If Not IsNothing(xmlNode) Then
                    xmlField.Delimiter = xmlNode.InnerText
                End If

            End If

        End Sub

        Private Shared Sub ParseAdminValuesTag(ByVal xmlField As XmlAdminField, ByVal field As XmlNode)

            For Each innerfield As XmlNode In field.SelectNodes("values")

                For Each valueTag As XmlNode In innerfield.SelectNodes("value")

                    Dim node As XmlNode = valueTag.SelectSingleNode("show_as")
                    If Not IsNothing(node) Then
                        xmlField.Values.Add(node.InnerText)
                    Else
                        xmlField.Values.Add(valueTag.InnerText)
                    End If

                Next

            Next

        End Sub

#End Region

    End Class

End Namespace