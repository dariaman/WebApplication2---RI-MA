Imports SPGeneral
Public Class UserListPIC
    Inherits System.Web.UI.Page
    Dim _Clsusers As New WebService.ClsUser
    Dim _sama As New WebService.sama
    Dim _clsrole As New WebService.ClsRole

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
                        Session("DashBoard") = "Generate User PIC | Provider <i class='fa fa-user fa-fw'></i>"
                        Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                        _sama.isiddlRoleSelectedRole(ddlRoleAP, Session("RoleCode"))
                        '_sama.isiddlRole(ddlRoleAP1, Session("RoleCode"))
                        _sama.isiddlBranch(ddlBranchCode)

                        _sama.isiddlBranch(ddlBranch)
                        ddlBranch.Items.RemoveAt(1)
                        txtExpirateDate.Text = DateAdd(DateInterval.Year, 1, Today)
                        Session("isNew") = 0

                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - UserListPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=UserListPIC.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=UserListPIC.aspx", False)
        End If
    End Sub

    Protected Sub bindData(rolecode As String, Branchcode As String)
        Try
            Dim dt As DataTable
            dt = _clsrole.bindDataPIC(rolecode, txtKeyword.Text, Branchcode)
            If dt.Rows.Count > 0 Then
                DGuser.DataSource = dt
                DGuser.DataBind()
            Else
                DGuser.DataSource = Nothing
                DGuser.DataBind()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            System.Threading.Thread.Sleep(500)
            'LinkMpeModalPopupExtender.Show()
            txtUid.Text = ""
            txtName.Text = ""
            txtEmail.Text = ""
            Session("isNew") = "1"
            pnlPopup.Visible = True
            PnlMain.Visible = False
            txtBirthDate.Text = ""
            chkAktiv.Enabled = True
            txtExpirateDate.Text = ""
            RbGender.ClearSelection()
            ddlBranch.SelectedIndex = 0
            bindDatabranch("")
            'bindDataProduct("")
            bindDataGroup("")
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserListPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            DGuser.PageIndex = 0
            bindData(Session("RoleCode"), ddlBranchCode.SelectedValue)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserListPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub LoadDataUser(ByVal userId As String)
        Try
            Dim dr As DataTable = _clsrole.LoadDataUser(userId)
            txtUid.Text = userId
            txtName.Text = dr.Rows(0).Item("Username").ToString()
            txtEmail.Text = dr.Rows(0).Item("email").ToString()
            ddlRoleAP.SelectedValue = dr.Rows(0).Item("rolecode").ToString()
            
            Dim adm As String = validRole(ddlRoleAP.SelectedValue).Item(0)
            Dim lvladm As String = validRole(ddlRoleAP.SelectedValue).Item(1)
            chkAktiv.Checked = IIf(IsDBNull(dr.Rows(0).Item("IsActive").ToString), "False", dr.Rows(0).Item("IsActive").ToString())
            LblMarketing.Text = dr.Rows(0).Item("Mkt_code").ToString()
            ddlBranch.SelectedValue = IIf(IsDBNull(dr.Rows(0).Item("branch").ToString()), "99", dr.Rows(0).Item("branch").ToString())
            txtEmail.Text = dr.Rows(0).Item("Email").ToString()
            txtBirthDate.Text = Format(CDate(IIf(dr.Rows(0).Item("BirthDate").ToString() = "", Today, dr.Rows(0).Item("BirthDate").ToString())), "dd/MM/yyyy")
            txtExpirateDate.Text = Format(CDate(IIf(dr.Rows(0).Item("ExpirateDate").ToString() = "", Today, dr.Rows(0).Item("ExpirateDate").ToString())), "dd/MM/yyyy")
            RbGender.SelectedValue = IIf(dr.Rows(0).Item("gender").ToString() = "", "M", dr.Rows(0).Item("gender").ToString())
           
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Function cekusr(userid As String) As Boolean
        Try
            Dim x As String = _clsrole.cekusr(userid, Session("isNew"))
            If x = 1 Then
                Return True
            ElseIf x = 2 Then
                txtEmail.Text = ""
                txtEmail.Focus()
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function validasi() As Boolean
        Try
            'If cekusr(txtEmail.Text) = False Then
            '    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Email already exists change the email');</script>")
            '    Return False
            'End If
            If txtName.Text = "" Or ddlBranch.SelectedIndex = 0 Or txtEmail.Text = "" Or Len(txtBirthDate.Text) < 10 Or Len(txtExpirateDate.Text) < 10 _
             Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('All Fields cannot empty!');</script>")
                Return False
            ElseIf txtName.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Input Name');</script>")
                txtName.Focus()
                Return False
            Else
                Return True
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click, LinkSubmit.Click
        Try
            System.Threading.Thread.Sleep(500)
            If validasi() = True Then
                _Clsusers.UserIdInp = txtUid.Text
                _Clsusers.UserNameInp = txtName.Text
                _Clsusers.EmailInp = txtEmail.Text

                'If Session("isNew") = "1" Then
                '    _Clsusers.RoleCodeInp = ddlRoleAP.SelectedValue
                'Else
                _Clsusers.RoleCodeInp = ddlRoleAP.SelectedValue
                'End If

                _Clsusers.IsActiveInp = chkAktiv.Checked
                _Clsusers.UserId = UserLogin.UserId
                _Clsusers.Mkt_Code = LblMarketing.Text
                _Clsusers.Branch = ddlBranch.SelectedValue
                _Clsusers.BirthDate = CDate(txtBirthDate.Text)
                _Clsusers.Gender = RbGender.SelectedValue
                _Clsusers.ExpirateDate = CDate(txtExpirateDate.Text)
                _Clsusers.Pict = Session("Pict")

                Dim dt As DataTable = _Clsusers.InsertUser()
                _Clsusers.UserIdInp = dt.Rows(0).Item(0).ToString

                _Clsusers.InsertUserBranch("00")
                
               
                Dim Ri As Integer = 0
                For Each dgcheck As DataGridItem In gridMenu2.Items
                    Dim chk As Boolean = CType(dgcheck.Cells(1).Controls(1), HtmlInputCheckBox).Checked
                    If chk = False Then
                        Ri = Ri + 1
                    End If
                Next

                Dim Ri1 As Integer = 0
                For Each dgcheck As DataGridItem In gridMenu1.Items
                    Dim chk As Boolean = CType(dgcheck.Cells(1).Controls(1), HtmlInputCheckBox).Checked
                    If chk = False Then
                        Ri1 = Ri1 + 1
                    End If
                Next

                If _Clsusers.DeleteBranchAccess() = True Then
                    'For Each dgcheck As DataGridItem In gridMenu2.Items
                    '    Dim chk As Boolean = CType(dgcheck.Cells(1).Controls(1), HtmlInputCheckBox).Checked
                    '    If chk Then
                    '        _Clsusers.BranchCode = dgcheck.Cells(0).Text
                    '        _Clsusers.InsertUserBranch(_Clsusers.BranchCode)
                    '    End If
                    'Next
                    _Clsusers.BranchCode = "00"
                    _Clsusers.InsertUserBranch(_Clsusers.BranchCode)
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data not saved!');</script>")

                    Exit Sub
                End If


                'If _Clsusers.DeleteGroupAccess() = True Then
                '    For Each dgcheck As DataGridItem In gridMenu3.Items
                '        Dim chk As Boolean = CType(dgcheck.Cells(1).Controls(1), HtmlInputCheckBox).Checked
                '        If chk Then
                '            _Clsusers.GroupCode = dgcheck.Cells(0).Text
                '            _Clsusers.InsertUserGroup(_Clsusers.GroupCode)
                '        End If
                '    Next
                'Else
                '    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data not saved!');</script>")
                '    Exit Sub
                'End If

                If dt.Rows.Count > 0 Then
                    LinkSubmit.Enabled = False
                    If _Clsusers.LvlAdmin <> 1 Then
                        Session("UserName") = dt.Rows(0).Item(0).ToString
                        Session("Email") = txtEmail.Text
                    End If
                    If Session("isNew") = "1" Then
                        txtUid.Text = dt.Rows(0).Item(0).ToString
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('User " & dt.Rows(0).Item(0).ToString & " saved! Password : abcd_1234');</script>")

                    Else
                        txtUid.Text = dt.Rows(0).Item(0).ToString
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('User " & dt.Rows(0).Item(0).ToString & " updated! ');</script>")

                    End If
                End If
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserListPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
            pnlPopup.Visible = True
            PnlMain.Visible = False
        End Try
    End Sub

    Sub Keluar()
        Try
            bindData(Session("RoleCode"), ddlBranchCode.SelectedValue)
            pnlPopup.Visible = False
            PnlMain.Visible = True
            LinkSubmit.Enabled = True
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserListPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Function validRole(RoleCode As String) As ArrayList
        Try
            Dim ar As New ArrayList
            ar = _clsrole.validRole(RoleCode)
            Return ar
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Protected Sub LinkClose4_Click(sender As Object, e As EventArgs) Handles LinkClose.Click
        System.Threading.Thread.Sleep(500)
        Keluar()
    End Sub

    Protected Sub bindDatabranch(UserId As String)
        Try
            Dim dt As New DataTable
            dt = _clsrole.bindDatabranch(UserId)
            If dt.Rows.Count > 0 Then
                gridMenu2.DataSource = dt
                gridMenu2.DataBind()
            Else
                gridMenu2.DataSource = Nothing
                gridMenu2.DataBind()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub bindDataGroup(UserId As String)
        Try
            Dim dt As New DataTable
            dt = _clsrole.bindDataGroup(UserId)
            If dt.Rows.Count > 0 Then
                gridMenu3.DataSource = dt
                gridMenu3.DataBind()
            Else
                gridMenu3.DataSource = Nothing
                gridMenu3.DataBind()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub LinkMarketing_Click(sender As Object, e As EventArgs) Handles LinkMarketing.Click, btnMarketing.Click
        System.Threading.Thread.Sleep(500)
        LinkMpeModalPopupExtender2.Show()
        KeyMarketing.Text = ""
    End Sub

    Private Sub DGuser_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles DGuser.PageIndexChanging
        Try
            System.Threading.Thread.Sleep(500)
            DGuser.PageIndex = e.NewPageIndex
            bindData(Session("RoleCode"), ddlBranchCode.SelectedValue)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserListPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub DGUser_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles DGUser.RowCommand
        Try
            System.Threading.Thread.Sleep(500)
            Dim index As Integer
            If e.CommandName.Equals("Select") Then
                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                index = gvRow.RowIndex
            ElseIf e.CommandName.Equals("SelectLink") Then
                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                index = gvRow.RowIndex
            End If
            If e.CommandName = "UpdateLink" Then
                System.Threading.Thread.Sleep(500)
                Dim KEY As String = e.CommandArgument

                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                index = gvRow.RowIndex
                Dim sts As String = DirectCast(DGuser.Rows(index).FindControl("hfstatus"), HiddenField).Value
                '_ClsInventarisItem.NO = KEY
                '_ClsInventarisItem.STATUS = IIf(sts = 1, 0, 1)
                '_ClsInventarisItem.updateStatus()
                _sama.UpdateActive("MSUser", "ISACTIVE", IIf(sts = "True", "False", "True"), "UserId", KEY)
                bindData(Session("RoleCode"), ddlBranchCode.SelectedValue)
            End If
            If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then
                Session("isNew") = "0"
                LoadDataUser(e.CommandArgument)
                pnlPopup.Visible = True
                PnlMain.Visible = False
                bindDatabranch(e.CommandArgument)
                'bindDataProduct(e.CommandArgument)
                bindDataGroup(e.CommandArgument)
                'If Left(ddlRoleAP.SelectedValue, 1) <> "S" Then
                '    gridMenu3.Visible = False
                'ElseIf Left(ddlRoleAP.SelectedValue, 1) = "0" Then
                '    gridMenu3.Visible = False
                'End If
                'If Left(ddlRoleAP.SelectedValue, 1) = "S" Then
                '    gridMenu3.Visible = True
                'ElseIf Left(ddlRoleAP.SelectedValue, 1) = "0" Then
                '    gridMenu3.Visible = True
                'End If
            ElseIf e.CommandName.Equals("KillLink") Then
                _Clsusers.UserId = e.CommandArgument
                _Clsusers.Online = "False"
                If _Clsusers.UpdateUseronlineUpdate() = True Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('User already off line');</script>")
                    bindData(Session("RoleCode"), ddlBranchCode.SelectedValue)
                End If
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserListPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

End Class