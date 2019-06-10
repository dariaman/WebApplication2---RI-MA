Imports SPGeneral
Public Class UserRole
    Inherits System.Web.UI.Page
    Dim _ClsRole As New WebService.ClsRole
    Dim _sama As New WebService.sama
    Public Property UserLogin() As WebService.ClsUser
        Get
            Return CType(Session("Users"), WebService.ClsUser)
        End Get
        Set(ByVal value As WebService.ClsUser)
            Session("Users") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not UserLogin Is Nothing Then
            If Not Page.IsPostBack Then
                If UserLogin.IsActive Then
                    Try
                        Session("DashBoard") = "Role List<i class='fa fa-gears fa-fw'></i>"
                        Dim Filename As String = System.IO.Path.GetFilename(Request.PhysicalPath)
                        If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                        Dim usridNew As String = Session("userSelect")
                        Dim isNew As String = Session("isNew")
                        _sama.isiddlMenuParent(ddlMenuParent)
                        _sama.isiddlRoleDesc(DdlLvlAdmin)
                        bindDataUnion(txtRole.Text, IIf(ddlMenuParent.SelectedValue.ToString = "99", "", ddlMenuParent.SelectedValue.ToString))
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=UserRole.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=UserRole.aspx", False)
        End If
    End Sub

    Protected Sub bindDataRole(type As String, RoleCode As String)
        Dim dt As DataTable
        dt = _ClsRole.bindDataRole(type, RoleCode)
        If dt.Rows.Count > 0 Then
            gridRole.DataSource = dt
            gridRole.DataBind()
        Else
            gridRole.DataSource = Nothing
            gridRole.DataBind()
        End If
    End Sub

    Protected Sub bindDataUnion(RoleCode As String, MenuParentCode As String)
        Try
            gridMenu.DataSource = _ClsRole.bindDataUnion(RoleCode, MenuParentCode)
            gridMenu.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function validasi() As Boolean
        If txtRoleDesc.Text = "" Or txtRoleCode.Text = "" Or DdlLvlAdmin.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('All Fields cannot empty!');</script>")
            Return False
        End If
        Return True
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click, LinkSubmit.Click
        Try
            System.Threading.Thread.Sleep(500)
            If validasi() = True Then
                _ClsRole.RoleCode = txtRoleCode.Text
                Dim Rc As Integer = gridMenu.Items.Count
                Dim Ri As Integer = 0
                For Each dgcheck As DataGridItem In gridMenu.Items
                    Dim chk As Boolean = CType(dgcheck.Cells(1).Controls(1), HtmlInputCheckBox).Checked
                    If chk = False Then
                        Ri = Ri + 1
                    End If
                Next
                If _ClsRole.DeleteUserSource(ddlMenuParent.SelectedValue.ToString) = True Then
                    For Each dgcheck As DataGridItem In gridMenu.Items
                        Dim chk As Boolean = CType(dgcheck.Cells(1).Controls(1), HtmlInputCheckBox).Checked
                        If chk Then
                            _ClsRole.MenuCode = dgcheck.Cells(0).Text
                            _ClsRole.InsertRoleAccess(_ClsRole.MenuCode)
                        End If
                    Next
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data not saved!');</script>")
                    Exit Sub
                End If
                _ClsRole.RoleCode = txtRoleCode.Text
                _ClsRole.RoleDesc = txtRoleDesc.Text
                _ClsRole.IsActive = chkAktiv.Checked
                _ClsRole.Admin = chkAdmin.Checked
                _ClsRole.LvlAdmin = DdlLvlAdmin.SelectedValue
                _ClsRole.CRE_BY = UserLogin.UserId

                If _ClsRole.InsertRole() = True Then
                    LinkSubmit.Enabled = False
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data saved!');</script>")
                End If
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            gridMenu.CurrentPageIndex = 0
            bindDataRole(txtRole.Text, ddlRole.SelectedValue)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub


    Protected Sub ddlMenuParent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMenuParent.SelectedIndexChanged
        Try
            System.Threading.Thread.Sleep(500)
            bindDataUnion(txtRoleCode.Text, IIf(ddlMenuParent.SelectedValue.ToString = "99", "", ddlMenuParent.SelectedValue.ToString))
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub gridMenu_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles gridMenu.PageIndexChanged
        Try
            System.Threading.Thread.Sleep(500)
            gridMenu.CurrentPageIndex = e.NewPageIndex
            bindDataUnion(txtRoleCode.Text, IIf(ddlMenuParent.SelectedValue.ToString = "99", "", ddlMenuParent.SelectedValue.ToString))
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub gridRole_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles gridRole.PageIndexChanged
        Try
            System.Threading.Thread.Sleep(500)
            gridRole.PageIndex = e.NewPageIndex
            bindDataRole(txtRole.Text, ddlRole.SelectedValue)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        System.Threading.Thread.Sleep(500)
        pnlPopup.Visible = True
        PnlMain.Visible = False
        ddlMenuParent.SelectedIndex = 0
        txtRoleCode.Text = ""
        txtRoleCode.ReadOnly = False
        txtRoleDesc.Text = ""
        DdlLvlAdmin.SelectedIndex = 0
        chkAdmin.Checked = False
        chkAktiv.Checked = True
        bindDataUnion(txtRole.Text, IIf(ddlMenuParent.SelectedValue.ToString = "99", "", ddlMenuParent.SelectedValue.ToString))
    End Sub

    Protected Sub LinkClose4_Click(sender As Object, e As EventArgs) Handles LinkClose.Click
        Try
            System.Threading.Thread.Sleep(500)
            pnlPopup.Visible = False
            PnlMain.Visible = True
            LinkSubmit.Enabled = True
            bindDataRole(txtRole.Text, ddlRole.SelectedValue)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserRole - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub DdlLvlAdmin_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlLvlAdmin.SelectedIndexChanged
        System.Threading.Thread.Sleep(500)
        If DdlLvlAdmin.SelectedValue = "1" Then
            chkAdmin.Checked = True
        Else
            chkAdmin.Checked = False
        End If
    End Sub

    Private Sub DGuser_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridRole.RowCommand
        System.Threading.Thread.Sleep(500)
        Dim index As Integer
        If e.CommandName.Equals("Select") Then
            Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            index = gvRow.RowIndex
        ElseIf e.CommandName.Equals("SelectLink") Then
            Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            index = gvRow.RowIndex
        End If
        If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then
            Session("isNew") = "0"
            bindDataUnion(e.CommandArgument, IIf(ddlMenuParent.SelectedValue.ToString = "99", "", ddlMenuParent.SelectedValue.ToString))

            pnlPopup.Visible = True
            PnlMain.Visible = False
            ddlMenuParent.SelectedIndex = 0
            txtRoleCode.Text = e.CommandArgument
            txtRoleCode.ReadOnly = True
            txtRoleDesc.Text = gridRole.Rows(index).Cells(1).Text
            chkAdmin.Checked = gridRole.Rows(index).Cells(2).Text
            DdlLvlAdmin.SelectedValue = gridRole.Rows(index).Cells(3).Text
        End If

        If e.CommandName = "UpdateLink" Then
            System.Threading.Thread.Sleep(500)
            Dim KEY As String = e.CommandArgument

            Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            index = gvRow.RowIndex
            Dim sts As String = DirectCast(gridRole.Rows(index).FindControl("hfstatus"), HiddenField).Value
            '_ClsInventarisItem.NO = KEY
            '_ClsInventarisItem.STATUS = IIf(sts = 1, 0, 1)
            '_ClsInventarisItem.updateStatus()
            _sama.UpdateActive("MSRole", "ISACTIVE", IIf(sts = "True", "False", "True"), "RoleCode", KEY)
            bindDataRole(txtRole.Text, ddlRole.SelectedValue)
        End If
    End Sub

End Class