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
Imports DemoLOLI.Classes

Namespace Forms

    Public Class Main

        Private _loliDb As LoliDatafeed
        Private _viewAs As Integer
        Private _treeOrg As Integer
        Private _tagName As String = ""
        Private _currentListId As String = 0
        Private Const ShowXml As Integer = 0
        Private Const ShowXmLexp As Integer = 1
        Private Const ShowData As Integer = 2
        Private Const TreeRegion As Integer = 0
        Private Const TreeListcat1 As Integer = 1
        Private Const TreeListcat2 As Integer = 2
        Private Const TreeDataType As Integer = 3
        Private Const TreeListTag As Integer = 4

#Region "Form Related Events"
        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            txtDbName.Text = My.Settings.DbName
            txtSvrName.Text = My.Settings.SvrName
            txtUser.Text = My.Settings.UserName
            txtPassword.Text = My.Settings.Password
        End Sub

        Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
            If btnConnect.Text = "Connect" Then
                If txtDbName.Text <> "" AndAlso txtSvrName.Text <> "" AndAlso txtUser.Text <> "" AndAlso txtPassword.Text <> "" Then
                    _loliDb = New LoliDatafeed(txtSvrName.Text, txtDbName.Text, txtUser.Text, txtPassword.Text)
                    If _loliDb.DatabaseOpenOk Then
                        btnConnect.Text = "Disconnect"
                        txtDbName.Enabled = False
                        txtSvrName.Enabled = False
                        txtUser.Enabled = False
                        txtPassword.Enabled = False
                        bgTreeOptions.Enabled = True
                        gbViewOptions.Enabled = True
                        FillTreeView()

                        My.Settings.DbName = txtDbName.Text
                        My.Settings.SvrName = txtSvrName.Text
                        My.Settings.UserName = txtUser.Text
                        My.Settings.Password = txtPassword.Text
                        Text = "LOLI Datafeed Demo - Connected - Content Release: " & _loliDb.GetCurrentRelease()
                    Else
                        MsgBox(_loliDb.ErrorMessage, MsgBoxStyle.Critical)
                        _loliDb.Close()
                        _loliDb = Nothing
                        Text = "LOLI Datafeed Demo - Disconnected"
                    End If
                Else
                    MsgBox("All Fields are Required", MsgBoxStyle.Exclamation)
                End If
            Else
                _loliDb.Close()
                _loliDb = Nothing
                btnConnect.Text = "Connect"
                txtDbName.Enabled = True
                txtSvrName.Enabled = True
                txtUser.Enabled = True
                txtPassword.Enabled = True
                bgTreeOptions.Enabled = False
                gbViewOptions.Enabled = False
                tvLoliLists.Nodes.Clear()
                dgvListData.DataSource = Nothing
                Text = "LOLI Datafeed Demo - Disconnected"
            End If
        End Sub

        Private Sub btnBuildTreeByTagName_Click(sender As Object, e As EventArgs) Handles btnBuildTreeByTagName.Click
            _tagName = txtListTagName.Text
            FillTreeView()
        End Sub

        Private Sub btnClearListTags_Click(sender As Object, e As EventArgs) Handles btnClearListTags.Click
            txtListTagName.Clear()
        End Sub

        Private Sub tvLoliLists_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvLoliLists.AfterSelect
            If tvLoliLists.SelectedNode.Tag <> "" Then
                _currentListId = tvLoliLists.SelectedNode.Tag
                FillGrid()
            End If
        End Sub

        Private Sub rbListXML_Click(sender As Object, e As EventArgs) Handles rbListXML.Click
            If rbListXML.Checked Then
                _viewAs = ShowXml
                FillGrid()
            End If
        End Sub

        Private Sub rbListXMLexpand_Click(sender As Object, e As EventArgs) Handles rbListXMLexpand.Click
            If rbListXMLexpand.Checked Then
                _viewAs = ShowXmLexp
                FillGrid()
            End If
        End Sub

        Private Sub rbListData_Click(sender As Object, e As EventArgs) Handles rbListData.Click
            If rbListData.Checked Then
                _viewAs = ShowData
                FillGrid()
            End If
        End Sub

        Private Sub rbTreeListCat1_CheckedChanged(sender As Object, e As EventArgs) Handles rbTreeListCat1.CheckedChanged
            If rbTreeListCat1.Checked Then
                _treeOrg = TreeListcat1
                FillTreeView()
            End If
        End Sub

        Private Sub rbTreeListCat2_CheckedChanged(sender As Object, e As EventArgs) Handles rbTreeListCat2.CheckedChanged
            If rbTreeListCat2.Checked Then
                _treeOrg = TreeListcat2
                FillTreeView()
            End If
        End Sub

        Private Sub rbTreeRegion_CheckedChanged(sender As Object, e As EventArgs) Handles rbTreeRegion.CheckedChanged
            If rbTreeRegion.Checked Then
                _treeOrg = TreeRegion
                FillTreeView()
            End If
        End Sub

        Private Sub rbTreeDataType_CheckedChanged(sender As Object, e As EventArgs) Handles rbTreeDataType.CheckedChanged
            If rbTreeDataType.Checked Then
                _treeOrg = TreeDataType
                FillTreeView()
            End If
        End Sub

        Private Sub rbTreeListTag_CheckedChanged(sender As Object, e As EventArgs) Handles rbTreeListTag.CheckedChanged
            If rbTreeListTag.Checked Then
                tvLoliLists.Nodes.Clear()
                _treeOrg = TreeListTag
                ProcessAutoCompletion()
            Else
                txtListTagName.Clear()
            End If

            txtListTagName.Enabled = rbTreeListTag.Checked
            btnBuildTreeByTagName.Enabled = rbTreeListTag.Checked
            btnClearListTags.Enabled = rbTreeListTag.Checked
        End Sub

        Private Sub tsmiValidateContent_Click(sender As Object, e As EventArgs) Handles tsmiValidateContent.Click
            If _currentListId = 0 Then
                MsgBox("Please select a LOLI List first.", MsgBoxStyle.Exclamation)

            ElseIf IsDBNull(dgvListData.CurrentCell.Value) Then
                MsgBox("No Cell Selected.", MsgBoxStyle.Exclamation)

            Else
                Dim dgvCellStyle As New DataGridViewCellStyle
                If _loliDb.ValidateFieldValue(dgvListData.CurrentCell.Value) Then
                    dgvCellStyle.BackColor = Color.GreenYellow
                Else
                    MsgBox(_loliDb.ErrorMessage, MsgBoxStyle.Critical)
                    dgvCellStyle.BackColor = Color.Red
                End If
                dgvListData.CurrentCell.Style = dgvCellStyle
                dgvListData.ClearSelection()
            End If

        End Sub
#End Region

#Region "Private Methods"
        Private Sub FillTreeView()
            If IsNothing(_loliDb) Then
                tvLoliLists.Nodes.Clear()
                gbLists.Text = "LOLI Lists"

            Else
                tvLoliLists.Nodes.Clear()
                dgvListData.DataSource = Nothing

                Dim strParentField As String = ""
                Dim dtListNames As DataTable
                Select Case _treeOrg
                    Case TreeListTag
                        dtListNames = _loliDb.GetListNames(_tagName)
                    Case Else
                        dtListNames = _loliDb.GetListNames(_treeOrg)
                End Select

                If _treeOrg = TreeListcat2 Or _treeOrg = TreeListTag Then
                    Dim lastLc1 As String = ""
                    Dim lastLc2 As String = ""
                    Dim curLc1 As String
                    Dim curLc2 As String
                    Dim curListName As String
                    Dim curListId As String

                    For Each dr As DataRow In dtListNames.Rows
                        If _treeOrg = TreeListTag Then
                            curLc1 = dr("TagName")
                            curLc2 = dr("ListCategory1")
                        Else
                            curLc1 = dr("ListCategory1")
                            curLc2 = dr("ListCategory2")
                        End If
                        curListName = dr("ListName")
                        curListId = dr("ListID").ToString()

                        If curLc1 <> lastLc1 Then
                            tvLoliLists.Nodes.Add(curLc1, curLc1)
                        End If
                        lastLc1 = curLc1

                        If curLc2 <> lastLc2 Then
                            tvLoliLists.Nodes(curLc1).Nodes.Add(curLc2, curLc2)
                        End If
                        lastLc2 = curLc2

                        tvLoliLists.Nodes(curLc1).Nodes(curLc2).Nodes.Add(curListId, curListName)
                        tvLoliLists.Nodes(curLc1).Nodes(curLc2).Nodes(curListId).Tag = curListId
                    Next
                Else
                    Select Case _treeOrg
                        Case TreeListcat1
                            strParentField = "ListCategory1"
                        Case TreeRegion
                            strParentField = "Region"
                        Case TreeDataType
                            strParentField = "ListDataType"
                    End Select

                    For Each dr As DataRow In dtListNames.Rows
                        Dim parentNode As String = dr(strParentField).ToString
                        Dim nodeText As String = dr("ListName").ToString
                        Dim tagText As String = dr("ListID").ToString

                        Dim node As New List(Of TreeNode)
                        node.AddRange(tvLoliLists.Nodes.Find(parentNode, True))
                        If Not node.Any Then
                            node.Add(tvLoliLists.Nodes.Add(parentNode, parentNode))
                        End If
                        node(0).Nodes.Add(nodeText, nodeText)
                        node(0).Nodes(nodeText).Tag = tagText
                    Next
                End If

                gbLists.Text = "LOLI Lists (Count: " & Format(dtListNames.Rows.Count, "#,###") & ")"
            End If

        End Sub

        Private Sub FillGrid()
            dgvListData.DataSource = Nothing
            If _currentListId > 0 Then
                Dim dtListData As DataTable = _loliDb.GetListDataByListId(_currentListId, _viewAs)
                dgvListData.DataSource = dtListData
                gbListData.Text = tvLoliLists.SelectedNode.Text & " (ListID: " & tvLoliLists.SelectedNode.Tag & "; Count: " & Format(dtListData.Rows.Count, "#,###") & ")"
            End If
        End Sub

        Private Sub ProcessAutoCompletion()
            txtListTagName.AutoCompleteCustomSource.Clear()

            Dim allTagNames As AutoCompleteStringCollection = _loliDb.GetTagNames()
            txtListTagName.AutoCompleteCustomSource = allTagNames

        End Sub

#End Region

    End Class
End Namespace