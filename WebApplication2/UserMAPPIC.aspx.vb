Imports SPGeneral
Public Class UserMAPPIC
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
                        Session("DashBoard") = "Mapping User PIC | Provider<i class='fa fa-user fa-fw'></i>"
                        Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                        '_sama.isiddlRoleSelectedRole(ddlRoleAP, Session("RoleCode"))
                        _sama.isiddlBranch(ddlBranchCode)
                        Session("isNew") = 0

                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - UserMAPPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=UserMAPPIC.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=UserMAPPIC.aspx", False)
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
            Session("isNew") = "1"
            pnlPopup.Visible = True
            PnlMain.Visible = False
            bindDatabranch("")
            'bindDataProduct("")
            bindDataGroup("")
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserMAPPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
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
            Dim msg As String = String.Format("{0} - UserMAPPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub LoadDataUser(ByVal userId As String)
        Try
            Dim dr As DataTable = _clsrole.LoadDataUser(userId)
            txtUid.Text = userId
            txtName.Text = dr.Rows(0).Item("Username").ToString()
            'If Session("isNew") = "1" Then
            'Else
            '    ddlRoleAP1.SelectedValue = dr.Rows(0).Item("rolecode").ToString()
            'End If
            'LblMarketing.Text = dr.Rows(0).Item("Mkt_code").ToString()
            'If ddlRoleAP.SelectedValue = "00003" Then
            '    PnlPIC.Visible = True
            '    PnlProvider.Visible = False
            '    DGDetailLog.Columns(0).Visible = True
            '    DGDetailLog.Columns(1).Visible = False
            '    DGDetailLog.Columns(2).Visible = False
            '    Dim dt As New DataTable
            '    dt = _clsrole.bindDataGVPIC(userId)
            '    If dt.Rows.Count > 0 Then
            '        DGDetailLog.DataSource = dt
            '        DGDetailLog.DataBind()
            '    Else
            '        DGDetailLog.DataSource = Nothing
            '        DGDetailLog.DataBind()
            '    End If
            'Else
            '    PnlProvider.Visible = True
            '    PnlPIC.Visible = False
            '    DGDetailLog.Columns(0).Visible = False
            '    DGDetailLog.Columns(1).Visible = False
            '    DGDetailLog.Columns(2).Visible = True
            '    Dim dt As New DataTable
            '    dt = _clsrole.bindDataGVPIC(userId)
            '    If dt.Rows.Count > 0 Then
            '        DGDetailLog.DataSource = dt
            '        DGDetailLog.DataBind()
            '    Else
            '        DGDetailLog.DataSource = Nothing
            '        DGDetailLog.DataBind()
            '    End If
            'End If
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
            If txtName.Text = "" Then
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
                If Session("roleinp") = "00003" Then
                    _Clsusers.InsertMSLOGINDETAIL(Session("useridinp"), TxtPolicyNo.Text, "", "", Session("roleinp"))
                Else
                    _Clsusers.InsertMSLOGINDETAIL(Session("useridinp"), "", "", txtProvider.Text, Session("roleinp"))
                End If
                TxtPolicyNo.Text = ""
                txtProvider.Text = ""
                Dim dt1 As DataTable
                If Session("roleinp") = "00003" Then

                    PnlPIC.Visible = True
                    PnlProvider.Visible = False
                    DGDetailLog.Columns(1).Visible = True
                    DGDetailLog.Columns(2).Visible = False
                    DGDetailLog.Columns(3).Visible = False

                    dt1 = _clsrole.bindDataGVPIC(Session("useridinp"))
                    If dt1.Rows.Count > 0 Then
                        DGDetailLog.DataSource = dt1
                        DGDetailLog.DataBind()
                    Else
                        DGDetailLog.DataSource = Nothing
                        DGDetailLog.DataBind()
                    End If
                Else
                    PnlProvider.Visible = True
                    PnlPIC.Visible = False
                    DGDetailLog.Columns(1).Visible = False
                    DGDetailLog.Columns(2).Visible = False
                    DGDetailLog.Columns(3).Visible = True

                    dt1 = _clsrole.bindDataGVPIC(Session("useridinp"))
                    If dt1.Rows.Count > 0 Then
                        DGDetailLog.DataSource = dt1
                        DGDetailLog.DataBind()
                    Else
                        DGDetailLog.DataSource = Nothing
                        DGDetailLog.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserMAPPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
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
            Dim msg As String = String.Format("{0} - UserMAPPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
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
            Dim msg As String = String.Format("{0} - UserMAPPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub DGUser_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles DGUser.RowCommand
        Try
            System.Threading.Thread.Sleep(500)
            Dim index As Integer
            

            If e.CommandName.Equals("Select") Then
                
            ElseIf e.CommandName.Equals("SelectLink") Then
                
            End If
            If e.CommandName = "UpdateLink" Then
                System.Threading.Thread.Sleep(500)
                Dim KEY As String = e.CommandArgument

                Dim sts As String = DirectCast(DGuser.Rows(index).FindControl("hfstatus"), HiddenField).Value
                bindData(Session("RoleCode"), ddlBranchCode.SelectedValue)
            End If
            If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then
                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                index = gvRow.RowIndex
                Session("roleinp") = DGuser.Rows(index).Cells(2).Text
                Session("useridinp") = e.CommandArgument

                Session("isNew") = "0"
                LoadDataUser(e.CommandArgument)
                pnlPopup.Visible = True
                PnlMain.Visible = False
                bindDatabranch(e.CommandArgument)
                bindDataGroup(e.CommandArgument)
                Dim dt1 As DataTable
                If DGuser.Rows(index).Cells(2).Text = "00003" Then

                    PnlPIC.Visible = True
                    PnlProvider.Visible = False
                    DGDetailLog.Columns(1).Visible = True
                    DGDetailLog.Columns(2).Visible = False
                    DGDetailLog.Columns(3).Visible = False

                    dt1 = _clsrole.bindDataGVPIC(e.CommandArgument)
                    If dt1.Rows.Count > 0 Then
                        DGDetailLog.DataSource = dt1
                        DGDetailLog.DataBind()
                    Else
                        DGDetailLog.DataSource = Nothing
                        DGDetailLog.DataBind()
                    End If
                Else
                    PnlProvider.Visible = True
                    PnlPIC.Visible = False
                    DGDetailLog.Columns(1).Visible = False
                    DGDetailLog.Columns(2).Visible = False
                    DGDetailLog.Columns(3).Visible = True

                    dt1 = _clsrole.bindDataGVPIC(e.CommandArgument)
                    If dt1.Rows.Count > 0 Then
                        DGDetailLog.DataSource = dt1
                        DGDetailLog.DataBind()
                    Else
                        DGDetailLog.DataSource = Nothing
                        DGDetailLog.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserMAPPIC - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub DGDetailLog_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DGDetailLog.ItemCommand
        System.Threading.Thread.Sleep(500)
        Dim Policyno As String = DGDetailLog.Items(e.Item.ItemIndex).Cells(1).Text
        Dim Provider As String = DGDetailLog.Items(e.Item.ItemIndex).Cells(3).Text
        If e.CommandName.Equals("SelectLink") Then
            _Clsusers.DeleteMSLOGINDETAIL(e.CommandArgument, Policyno, Provider, Session("roleinp"))
            Dim dt1 As DataTable
            If Session("roleinp") = "00003" Then

                PnlPIC.Visible = True
                PnlProvider.Visible = False
                DGDetailLog.Columns(1).Visible = True
                DGDetailLog.Columns(2).Visible = False
                DGDetailLog.Columns(3).Visible = False

                dt1 = _clsrole.bindDataGVPIC(Session("useridinp"))
                If dt1.Rows.Count > 0 Then
                    DGDetailLog.DataSource = dt1
                    DGDetailLog.DataBind()
                Else
                    DGDetailLog.DataSource = Nothing
                    DGDetailLog.DataBind()
                End If
            Else
                PnlProvider.Visible = True
                PnlPIC.Visible = False
                DGDetailLog.Columns(1).Visible = False
                DGDetailLog.Columns(2).Visible = False
                DGDetailLog.Columns(3).Visible = True

                dt1 = _clsrole.bindDataGVPIC(Session("useridinp"))
                If dt1.Rows.Count > 0 Then
                    DGDetailLog.DataSource = dt1
                    DGDetailLog.DataBind()
                Else
                    DGDetailLog.DataSource = Nothing
                    DGDetailLog.DataBind()
                End If
            End If
        End If
    End Sub

    Protected Sub DGDetailLog_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DGDetailLog.SelectedIndexChanged

    End Sub
End Class