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

Imports System.Data.SqlClient
Imports ChemAdvisorIntegration.Loli4.ListDataXml
Imports ChemAdvisorIntegration.Loli4.ValidatableField
Imports ChemAdvisorIntegration.Loli4.ValidatableField.Number

Namespace Classes

    Public Class LoliDatafeed

        Private ReadOnly _mySqlConnection As SqlConnection
        Private Const ShowXml As Integer = 0
        Private Const ShowXmLexp As Integer = 1
        Private Const ShowData As Integer = 2
        Private Const TreeRegion As Integer = 0
        Private Const TreeListcat1 As Integer = 1
        Private Const TreeListcat2 As Integer = 2
        Private Const TreeDataType As Integer = 3

#Region "Public Properties & Methods"
        Public Property DatabaseName As String
        Public Property ServerName As String
        Public Property DatabaseOpenOk As Boolean
        Public Property ErrorMessage As String

        Public Sub New(svrName As String, dbName As String, user As String, password As String)

            Dim dataSource As String = String.Format("Data Source={0};Initial Catalog={1};Integrated Security=False;User ID={2};Password={3}", svrName, dbName, user, password)

            _mySqlConnection = New SqlConnection(dataSource)
            Try
                _mySqlConnection.Open()
                DatabaseOpenOk = True
                DatabaseName = dbName
                ServerName = svrName
                ErrorMessage = ""

            Catch ex As Exception
                DatabaseOpenOk = False
                ErrorMessage = ex.Message
            End Try

        End Sub

        Public Sub Close()

            _mySqlConnection.Close()
            DatabaseOpenOk = False
            ErrorMessage = ""

        End Sub

        Public Function GetCurrentRelease() As String

            Const mySql As String = "SELECT TOP 1 ReleaseQuarter FROM ReleaseHistory ORDER BY UPD_ID DESC"
            Dim releaseHistory As DataTable = GetTable(mySql)

            If releaseHistory.Rows.Count > 0 Then
                Return releaseHistory.Rows.Item(0).Item("ReleaseQuarter")
            Else
                Return ""
            End If

        End Function

        Public Function GetListNames(ByVal processListNamesMethod As Integer) As DataTable

            Dim mySql As String = ""
            Select Case processListNamesMethod
                Case TreeListcat1
                    mySql = "SELECT ListCategory1,ListName,ListID FROM ListNames ORDER BY ListCategory1,ListName"
                Case TreeListcat2
                    mySql = "SELECT ListCategory1,ListCategory2,ListName,ListID FROM ListNames ORDER BY ListCategory1,ListCategory2,ListName"
                Case TreeRegion
                    mySql = "SELECT Region,ListName,ListID FROM ListNames ORDER BY Region,ListName"
                Case TreeDataType
                    mySql = "SELECT ListDataType,ListName,ListID FROM ListNames ORDER BY ListDataType,ListName"
            End Select

            Dim dtListNames As DataTable = GetTable(mySql)

            Return dtListNames

        End Function

        Public Function GetListDataByListId(ByVal listId As String, ByVal processListDataMethod As Integer) As DataTable

            Dim mySql As String
            Dim dtListData As New DataTable

            Select Case processListDataMethod
                Case ShowData
                    mySql = "SELECT a.ListID,a.Cas,b.Name,a.Data,a.XML FROM ListData a INNER JOIN CasSyns b ON a.Cas=b.Cas AND a.LangID=b.LangID AND a.SynID=b.SynID WHERE a.ListID=" & listId & " ORDER BY a.CasSort"
                    dtListData = GetTable(mySql)

                Case ShowXml, ShowXmLexp
                    mySql = "SELECT a.ListID,a.Cas,b.Name,a.XML FROM ListData a INNER JOIN CasSyns b ON a.Cas=b.Cas AND a.LangID=b.LangID AND a.SynID=b.SynID WHERE a.ListID=" & listId & " ORDER BY a.CasSort"
                    dtListData = GetTable(mySql, listId, processListDataMethod)

            End Select

            Return dtListData

        End Function

        Public Function ValidateFieldValue(ByVal valueIn As String) As Boolean

            ErrorMessage = ""

            Dim ex As ILoliValidatableField = Nothing

            LoliNumberFactory.TryParse(valueIn, ex)
            If ex.IsValid() Then

                Return True

            End If

            ErrorMessage = ex.GetErrorMessage()

            Return False

        End Function
#End Region

#Region "Private Methods"
        Private Function GetTable(tSql As String) As DataTable

            Dim dtResult As New DataTable
            Dim myCmd As SqlCommand

            myCmd = New SqlCommand(tSql, _mySqlConnection)

            Using sqlDa As New SqlDataAdapter(myCmd)
                sqlDa.Fill(dtResult)
            End Using

            Return dtResult

        End Function

        Private Function GetTable(tSql As String, listId As String, processListDataMethod As Integer) As DataTable

            Dim dtListData As DataTable = FillListData(tSql)
            Dim dtListNames As DataTable = FillListNames(listId)

            Dim xmlFactory As New LoliListDataFactory(dtListNames.Rows(0).Item("XML_Admin").ToString)

            Dim dtBuild As New DataTable()
            Dim dtResult As New DataTable

            For Each drLd As DataRow In dtListData.Rows

                Select Case processListDataMethod

                    Case ShowXml
                        xmlFactory.TryParse(drLd("XML"), dtBuild)

                    Case ShowXmLexp
                        xmlFactory.TryParseWithNumberExpansion(drLd("XML"), dtBuild)

                End Select

                CopyCasAndName(drLd, dtBuild)
                dtResult.Merge(dtBuild)

            Next

            Return dtResult

        End Function

        Private Function FillListNames(listId As String) As DataTable

            Dim myCmd As SqlCommand
            Dim dtListNames As New DataTable

            myCmd = New SqlCommand("SELECT XML_Admin FROM ListNames_Control WHERE ListID=" & listId, _mySqlConnection)
            Using sqlDa As New SqlDataAdapter(myCmd)
                sqlDa.Fill(dtListNames)
            End Using

            Return dtListNames

        End Function

        Private Function FillListData(tSql As String) As DataTable

            Dim myCmd As SqlCommand
            Dim dtListData As New DataTable

            myCmd = New SqlCommand(tSql, _mySqlConnection)
            Using sqlDa As New SqlDataAdapter(myCmd)
                sqlDa.Fill(dtListData)
            End Using

            Return dtListData

        End Function

        Private Shared Sub CopyCasAndName(drLd As DataRow, dtBuild As DataTable)

            Dim dcCas As DataColumn = New DataColumn("Cas")
            Dim dcName As DataColumn = New DataColumn("Name")

            dtBuild.Columns.Add(dcCas)
            dtBuild.Columns("Cas").SetOrdinal(0)
            dtBuild.Columns.Add(dcName)
            dtBuild.Columns("Name").SetOrdinal(1)

            Dim drCn As DataRow
            For Each drCn In dtBuild.Rows
                drCn("Cas") = drLd("Cas")
                drCn("Name") = drLd("Name")
            Next

        End Sub

#End Region

    End Class
End Namespace