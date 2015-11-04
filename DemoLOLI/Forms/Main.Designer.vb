Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Main
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.gbDbCon = New System.Windows.Forms.GroupBox()
            Me.btnConnect = New System.Windows.Forms.Button()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.txtPassword = New System.Windows.Forms.TextBox()
            Me.txtUser = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.txtSvrName = New System.Windows.Forms.TextBox()
            Me.txtDbName = New System.Windows.Forms.TextBox()
            Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
            Me.gbListData = New System.Windows.Forms.GroupBox()
            Me.dgvListData = New System.Windows.Forms.DataGridView()
            Me.cmsGrid = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.tsmiValidateContent = New System.Windows.Forms.ToolStripMenuItem()
            Me.gbLists = New System.Windows.Forms.GroupBox()
            Me.tvLoliLists = New System.Windows.Forms.TreeView()
            Me.gbViewOptions = New System.Windows.Forms.GroupBox()
            Me.rbListXMLexpand = New System.Windows.Forms.RadioButton()
            Me.rbListXML = New System.Windows.Forms.RadioButton()
            Me.rbListData = New System.Windows.Forms.RadioButton()
            Me.bgTreeOptions = New System.Windows.Forms.GroupBox()
            Me.rbTreeDataType = New System.Windows.Forms.RadioButton()
            Me.rbTreeListCat2 = New System.Windows.Forms.RadioButton()
            Me.rbTreeRegion = New System.Windows.Forms.RadioButton()
            Me.rbTreeListCat1 = New System.Windows.Forms.RadioButton()
            Me.gbDbCon.SuspendLayout()
            Me.TableLayoutPanel1.SuspendLayout()
            Me.gbListData.SuspendLayout()
            CType(Me.dgvListData, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.cmsGrid.SuspendLayout()
            Me.gbLists.SuspendLayout()
            Me.gbViewOptions.SuspendLayout()
            Me.bgTreeOptions.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbDbCon
            '
            Me.gbDbCon.Controls.Add(Me.btnConnect)
            Me.gbDbCon.Controls.Add(Me.Label4)
            Me.gbDbCon.Controls.Add(Me.Label3)
            Me.gbDbCon.Controls.Add(Me.txtPassword)
            Me.gbDbCon.Controls.Add(Me.txtUser)
            Me.gbDbCon.Controls.Add(Me.Label2)
            Me.gbDbCon.Controls.Add(Me.Label1)
            Me.gbDbCon.Controls.Add(Me.txtSvrName)
            Me.gbDbCon.Controls.Add(Me.txtDbName)
            Me.gbDbCon.Location = New System.Drawing.Point(12, 12)
            Me.gbDbCon.Name = "gbDbCon"
            Me.gbDbCon.Size = New System.Drawing.Size(1139, 48)
            Me.gbDbCon.TabIndex = 2
            Me.gbDbCon.TabStop = False
            Me.gbDbCon.Text = "Database Server Connection"
            '
            'btnConnect
            '
            Me.btnConnect.Location = New System.Drawing.Point(1016, 18)
            Me.btnConnect.Name = "btnConnect"
            Me.btnConnect.Size = New System.Drawing.Size(117, 23)
            Me.btnConnect.TabIndex = 4
            Me.btnConnect.Text = "Connect"
            Me.btnConnect.UseVisualStyleBackColor = True
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(830, 22)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(53, 13)
            Me.Label4.TabIndex = 7
            Me.Label4.Text = "Password"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(630, 23)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(61, 13)
            Me.Label3.TabIndex = 6
            Me.Label3.Text = "User Login:"
            '
            'txtPassword
            '
            Me.txtPassword.Location = New System.Drawing.Point(889, 19)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.Size = New System.Drawing.Size(111, 20)
            Me.txtPassword.TabIndex = 3
            Me.txtPassword.UseSystemPasswordChar = True
            '
            'txtUser
            '
            Me.txtUser.Location = New System.Drawing.Point(697, 19)
            Me.txtUser.Name = "txtUser"
            Me.txtUser.Size = New System.Drawing.Size(116, 20)
            Me.txtUser.TabIndex = 2
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(312, 23)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(87, 13)
            Me.Label2.TabIndex = 3
            Me.Label2.Text = "Database Name:"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(29, 23)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(72, 13)
            Me.Label1.TabIndex = 2
            Me.Label1.Text = "Server Name:"
            '
            'txtSvrName
            '
            Me.txtSvrName.Location = New System.Drawing.Point(107, 20)
            Me.txtSvrName.Name = "txtSvrName"
            Me.txtSvrName.Size = New System.Drawing.Size(199, 20)
            Me.txtSvrName.TabIndex = 0
            '
            'txtDbName
            '
            Me.txtDbName.Location = New System.Drawing.Point(405, 20)
            Me.txtDbName.Name = "txtDbName"
            Me.txtDbName.Size = New System.Drawing.Size(203, 20)
            Me.txtDbName.TabIndex = 1
            '
            'TableLayoutPanel1
            '
            Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TableLayoutPanel1.AutoSize = True
            Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.TableLayoutPanel1.ColumnCount = 2
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.67954!))
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.32046!))
            Me.TableLayoutPanel1.Controls.Add(Me.gbListData, 0, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.gbLists, 0, 0)
            Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 104)
            Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
            Me.TableLayoutPanel1.RowCount = 1
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel1.Size = New System.Drawing.Size(1138, 531)
            Me.TableLayoutPanel1.TabIndex = 3
            '
            'gbListData
            '
            Me.gbListData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.gbListData.AutoSize = True
            Me.gbListData.Controls.Add(Me.dgvListData)
            Me.gbListData.Location = New System.Drawing.Point(397, 3)
            Me.gbListData.Name = "gbListData"
            Me.gbListData.Size = New System.Drawing.Size(738, 525)
            Me.gbListData.TabIndex = 2
            Me.gbListData.TabStop = False
            Me.gbListData.Text = "LOLI List Data"
            '
            'dgvListData
            '
            Me.dgvListData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.dgvListData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvListData.ContextMenuStrip = Me.cmsGrid
            Me.dgvListData.Location = New System.Drawing.Point(6, 19)
            Me.dgvListData.Name = "dgvListData"
            Me.dgvListData.Size = New System.Drawing.Size(729, 491)
            Me.dgvListData.TabIndex = 12
            '
            'cmsGrid
            '
            Me.cmsGrid.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiValidateContent})
            Me.cmsGrid.Name = "cmsGrid"
            Me.cmsGrid.Size = New System.Drawing.Size(163, 26)
            '
            'tsmiValidateContent
            '
            Me.tsmiValidateContent.Name = "tsmiValidateContent"
            Me.tsmiValidateContent.Size = New System.Drawing.Size(162, 22)
            Me.tsmiValidateContent.Text = "Validate Content"
            '
            'gbLists
            '
            Me.gbLists.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.gbLists.Controls.Add(Me.tvLoliLists)
            Me.gbLists.Location = New System.Drawing.Point(3, 3)
            Me.gbLists.Name = "gbLists"
            Me.gbLists.Size = New System.Drawing.Size(388, 525)
            Me.gbLists.TabIndex = 1
            Me.gbLists.TabStop = False
            Me.gbLists.Text = "LOLI Lists"
            '
            'tvLoliLists
            '
            Me.tvLoliLists.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tvLoliLists.Location = New System.Drawing.Point(6, 19)
            Me.tvLoliLists.Name = "tvLoliLists"
            Me.tvLoliLists.Size = New System.Drawing.Size(372, 491)
            Me.tvLoliLists.TabIndex = 11
            '
            'gbViewOptions
            '
            Me.gbViewOptions.Controls.Add(Me.rbListXMLexpand)
            Me.gbViewOptions.Controls.Add(Me.rbListXML)
            Me.gbViewOptions.Controls.Add(Me.rbListData)
            Me.gbViewOptions.Location = New System.Drawing.Point(409, 59)
            Me.gbViewOptions.Name = "gbViewOptions"
            Me.gbViewOptions.Size = New System.Drawing.Size(742, 39)
            Me.gbViewOptions.TabIndex = 4
            Me.gbViewOptions.TabStop = False
            Me.gbViewOptions.Text = "View Options"
            '
            'rbListXMLexpand
            '
            Me.rbListXMLexpand.AutoSize = True
            Me.rbListXMLexpand.Location = New System.Drawing.Point(179, 16)
            Me.rbListXMLexpand.Name = "rbListXMLexpand"
            Me.rbListXMLexpand.Size = New System.Drawing.Size(271, 17)
            Me.rbListXMLexpand.TabIndex = 9
            Me.rbListXMLexpand.TabStop = True
            Me.rbListXMLexpand.Text = "Show Parsed ListData.XML with Numeric Expansion"
            Me.rbListXMLexpand.UseVisualStyleBackColor = True
            '
            'rbListXML
            '
            Me.rbListXML.AutoSize = True
            Me.rbListXML.Checked = True
            Me.rbListXML.Location = New System.Drawing.Point(18, 16)
            Me.rbListXML.Name = "rbListXML"
            Me.rbListXML.Size = New System.Drawing.Size(155, 17)
            Me.rbListXML.TabIndex = 8
            Me.rbListXML.TabStop = True
            Me.rbListXML.Text = "Show Parsed ListData.XML"
            Me.rbListXML.UseVisualStyleBackColor = True
            '
            'rbListData
            '
            Me.rbListData.AutoSize = True
            Me.rbListData.Location = New System.Drawing.Point(461, 15)
            Me.rbListData.Name = "rbListData"
            Me.rbListData.Size = New System.Drawing.Size(220, 17)
            Me.rbListData.TabIndex = 10
            Me.rbListData.Text = "Show ListData.Data (with unparsed XML)"
            Me.rbListData.UseVisualStyleBackColor = True
            '
            'bgTreeOptions
            '
            Me.bgTreeOptions.Controls.Add(Me.rbTreeDataType)
            Me.bgTreeOptions.Controls.Add(Me.rbTreeListCat2)
            Me.bgTreeOptions.Controls.Add(Me.rbTreeRegion)
            Me.bgTreeOptions.Controls.Add(Me.rbTreeListCat1)
            Me.bgTreeOptions.Location = New System.Drawing.Point(12, 59)
            Me.bgTreeOptions.Name = "bgTreeOptions"
            Me.bgTreeOptions.Size = New System.Drawing.Size(391, 39)
            Me.bgTreeOptions.TabIndex = 5
            Me.bgTreeOptions.TabStop = False
            Me.bgTreeOptions.Text = "Tree Options"
            '
            'rbTreeDataType
            '
            Me.rbTreeDataType.AutoSize = True
            Me.rbTreeDataType.Location = New System.Drawing.Point(315, 16)
            Me.rbTreeDataType.Name = "rbTreeDataType"
            Me.rbTreeDataType.Size = New System.Drawing.Size(75, 17)
            Me.rbTreeDataType.TabIndex = 8
            Me.rbTreeDataType.TabStop = True
            Me.rbTreeDataType.Text = "Data Type"
            Me.rbTreeDataType.UseVisualStyleBackColor = True
            '
            'rbTreeListCat2
            '
            Me.rbTreeListCat2.AutoSize = True
            Me.rbTreeListCat2.Location = New System.Drawing.Point(126, 16)
            Me.rbTreeListCat2.Name = "rbTreeListCat2"
            Me.rbTreeListCat2.Size = New System.Drawing.Size(110, 17)
            Me.rbTreeListCat2.TabIndex = 6
            Me.rbTreeListCat2.TabStop = True
            Me.rbTreeListCat2.Text = "By List Category 2"
            Me.rbTreeListCat2.UseVisualStyleBackColor = True
            '
            'rbTreeRegion
            '
            Me.rbTreeRegion.AutoSize = True
            Me.rbTreeRegion.Location = New System.Drawing.Point(241, 16)
            Me.rbTreeRegion.Name = "rbTreeRegion"
            Me.rbTreeRegion.Size = New System.Drawing.Size(74, 17)
            Me.rbTreeRegion.TabIndex = 7
            Me.rbTreeRegion.TabStop = True
            Me.rbTreeRegion.Text = "By Region"
            Me.rbTreeRegion.UseVisualStyleBackColor = True
            '
            'rbTreeListCat1
            '
            Me.rbTreeListCat1.AutoSize = True
            Me.rbTreeListCat1.Checked = True
            Me.rbTreeListCat1.Location = New System.Drawing.Point(9, 16)
            Me.rbTreeListCat1.Name = "rbTreeListCat1"
            Me.rbTreeListCat1.Size = New System.Drawing.Size(110, 17)
            Me.rbTreeListCat1.TabIndex = 5
            Me.rbTreeListCat1.TabStop = True
            Me.rbTreeListCat1.Text = "By List Category 1"
            Me.rbTreeListCat1.UseVisualStyleBackColor = True
            '
            'Main
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1159, 643)
            Me.Controls.Add(Me.bgTreeOptions)
            Me.Controls.Add(Me.gbViewOptions)
            Me.Controls.Add(Me.TableLayoutPanel1)
            Me.Controls.Add(Me.gbDbCon)
            Me.Name = "Main"
            Me.Text = "LOLI Datafeed Demo - Disconnected"
            Me.gbDbCon.ResumeLayout(False)
            Me.gbDbCon.PerformLayout()
            Me.TableLayoutPanel1.ResumeLayout(False)
            Me.TableLayoutPanel1.PerformLayout()
            Me.gbListData.ResumeLayout(False)
            CType(Me.dgvListData, System.ComponentModel.ISupportInitialize).EndInit()
            Me.cmsGrid.ResumeLayout(False)
            Me.gbLists.ResumeLayout(False)
            Me.gbViewOptions.ResumeLayout(False)
            Me.gbViewOptions.PerformLayout()
            Me.bgTreeOptions.ResumeLayout(False)
            Me.bgTreeOptions.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents gbDbCon As System.Windows.Forms.GroupBox
        Friend WithEvents btnConnect As System.Windows.Forms.Button
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents txtPassword As System.Windows.Forms.TextBox
        Friend WithEvents txtUser As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents txtSvrName As System.Windows.Forms.TextBox
        Friend WithEvents txtDbName As System.Windows.Forms.TextBox
        Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents gbListData As System.Windows.Forms.GroupBox
        Friend WithEvents dgvListData As System.Windows.Forms.DataGridView
        Friend WithEvents gbLists As System.Windows.Forms.GroupBox
        Friend WithEvents tvLoliLists As System.Windows.Forms.TreeView
        Friend WithEvents gbViewOptions As System.Windows.Forms.GroupBox
        Friend WithEvents rbListXMLexpand As System.Windows.Forms.RadioButton
        Friend WithEvents rbListXML As System.Windows.Forms.RadioButton
        Friend WithEvents rbListData As System.Windows.Forms.RadioButton
        Friend WithEvents bgTreeOptions As System.Windows.Forms.GroupBox
        Friend WithEvents rbTreeRegion As System.Windows.Forms.RadioButton
        Friend WithEvents rbTreeListCat1 As System.Windows.Forms.RadioButton
        Friend WithEvents rbTreeListCat2 As System.Windows.Forms.RadioButton
        Friend WithEvents cmsGrid As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents tsmiValidateContent As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents rbTreeDataType As System.Windows.Forms.RadioButton

    End Class
End Namespace