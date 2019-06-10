Imports SPGeneral
Public Class UserMapCompany
    Inherits System.Web.UI.Page
    Dim _ClsCompany As New WebService.ClsCompany
    Dim _sama As New WebService.sama
    Dim _ClsCompanyPolicy As New WebService.ClsCompanyPolicy

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
                        Session("DashBoard") = "Mapping Company <i class='fa fa-user fa-fw'></i>"
                        Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                        '_sama.isiddlRoleSelectedRole(ddlRoleAP, Session("RoleCode"))
                        '_sama.isiddlBranch(ddlType)
                        Session("isNew") = 0

                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - UserMapCompany - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=UserMapCompany.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=UserMapCompany.aspx", False)
        End If
    End Sub

    Protected Sub bindData()
        Try
            Dim dt As DataTable
            dt = _ClsCompanyPolicy.bindData(txtKeyword.Text, ddlType.SelectedValue)
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
            txtComapnyId.Text = ""
            txtCompanyName.Text = ""
            Session("isNew") = "1"
            pnlPopup.Visible = True
            PnlMain.Visible = False
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserMapCompany - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            DGuser.PageIndex = 0
            bindData()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserMapCompany - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub


    Public Function validasi() As Boolean
        Try
            If txtCompanyName.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('All Fields cannot empty!');</script>")
                Return False
            ElseIf txtCompanyName.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Input Name');</script>")
                txtCompanyName.Focus()
                Return False
            ElseIf TxtPolicyNo.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Input Policy No');</script>")
                TxtPolicyNo.Focus()
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

                _ClsCompanyPolicy.CompanyId = txtComapnyId.Text
                _ClsCompanyPolicy.POLICYNO = TxtPolicyNo.Text
                _ClsCompanyPolicy.IsTPA = chkTPA.Checked
                _ClsCompanyPolicy.TPAName = txtTPAName.Text
                _ClsCompanyPolicy.isTPAEmail = chkisEmailTPA.Checked
                _ClsCompanyPolicy.TPAEmailAddress = txtTPAEmailAddress.Text
                _ClsCompanyPolicy.isTPAFTP = chkisFTPTPA.Checked
                _ClsCompanyPolicy.TPAFTPAddress = TxtTPAFTPAddress.Text
                _ClsCompanyPolicy.PICEmailPolicy = txtPICEmailPolicy.Text
                _ClsCompanyPolicy.isNotClaimFoto = chkNotClaimPhoto.Checked
                _ClsCompanyPolicy.CRE_BY = UserLogin.UserId
                If _ClsCompanyPolicy.InsertCompanyPolicy() = True Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Error!, Data saved');</script>")
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Data not saved' );</script>")
                End If

                Dim dt1 As DataTable = _ClsCompanyPolicy.bindisiData(txtComapnyId.Text)
                If dt1.Rows.Count > 0 Then
                    DGDetailLog.DataSource = dt1
                    DGDetailLog.DataBind()
                Else
                    DGDetailLog.DataSource = Nothing
                    DGDetailLog.DataBind()
                End If

                TxtPolicyNo.Text = ""
                chkTPA.Checked = False
                txtTPAName.Text = ""
                chkisEmailTPA.Checked = False
                txtTPAEmailAddress.Text = ""
                chkisFTPTPA.Checked = False
                TxtTPAFTPAddress.Text = ""
                txtPICEmailPolicy.Text = ""
                chkNotClaimPhoto.Checked = False

            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserMapCompany - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
            pnlPopup.Visible = True
            PnlMain.Visible = False
        End Try
    End Sub

    Sub Keluar()
        Try
            bindData()
            pnlPopup.Visible = False
            PnlMain.Visible = True
            LinkSubmit.Enabled = True
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserMapCompany - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub LinkClose4_Click(sender As Object, e As EventArgs) Handles LinkClose.Click
        System.Threading.Thread.Sleep(500)
        Keluar()
    End Sub

    Private Sub DGuser_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles DGuser.PageIndexChanging
        Try
            System.Threading.Thread.Sleep(500)
            DGuser.PageIndex = e.NewPageIndex
            bindData()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserMapCompany - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub DGUser_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles DGuser.RowCommand
        Try
            System.Threading.Thread.Sleep(500)
            Dim index As Integer
            'If e.CommandName.Equals("Select") Then

            'ElseIf e.CommandName.Equals("SelectLink") Then

            'End If
            If e.CommandName = "UpdateLink" Then
                System.Threading.Thread.Sleep(500)
                Dim KEY As String = e.CommandArgument

                Dim sts As String = DirectCast(DGuser.Rows(index).FindControl("hfstatus"), HiddenField).Value
                bindData()
            End If
            If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then
                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                index = gvRow.RowIndex

                Session("isNew") = "0"
                Dim dt1 As DataTable = _ClsCompanyPolicy.bindisiData(e.CommandArgument)
                txtComapnyId.Text = e.CommandArgument ' DGuser.Rows(index).Cells(0).Text
                txtCompanyName.Text = DGuser.Rows(index).Cells(1).Text
                pnlPopup.Visible = True
                PnlMain.Visible = False
                If dt1.Rows.Count > 0 Then
                    DGDetailLog.DataSource = dt1
                    DGDetailLog.DataBind()
                Else
                    DGDetailLog.DataSource = Nothing
                    DGDetailLog.DataBind()
                End If
                
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserMapCompany - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub DGDetailLog_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DGDetailLog.ItemCommand
        System.Threading.Thread.Sleep(500)
        Dim Policyno As String = DGDetailLog.Items(e.Item.ItemIndex).Cells(1).Text
        If e.CommandName.Equals("SelectLink") Then
            _ClsCompanyPolicy.POLICYNO = e.CommandArgument
            _ClsCompanyPolicy.CompanyId = txtComapnyId.Text

            If _ClsCompanyPolicy.DeleteCompanyPolicy() = True Then

                Dim dt1 As DataTable = _ClsCompanyPolicy.bindisiData(txtComapnyId.Text)
                If dt1.Rows.Count > 0 Then
                    DGDetailLog.DataSource = dt1
                    DGDetailLog.DataBind()
                Else
                    DGDetailLog.DataSource = Nothing
                    DGDetailLog.DataBind()
                End If
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Policy no :" & e.CommandArgument & " deleted');</script>")
            End If
        End If
    End Sub

    Protected Sub chkTPA_CheckedChanged(sender As Object, e As EventArgs) Handles chkTPA.CheckedChanged
        If chkTPA.Checked = True Then
            PnlTPA.Visible = True
            chkisEmailTPA.Checked = False
            chkisFTPTPA.Checked = False
        ElseIf chkTPA.Checked = False Then
            PnlTPA.Visible = False
            chkisEmailTPA.Checked = False
            chkisFTPTPA.Checked = False
        End If
    End Sub

    Protected Sub chkisEmailTPA_CheckedChanged(sender As Object, e As EventArgs) Handles chkisEmailTPA.CheckedChanged
        If chkisEmailTPA.Checked = True Then
            txtTPAEmailAddress.Visible = True
        Else
            txtTPAEmailAddress.Visible = False
        End If
    End Sub

    Protected Sub chkisFTPTPA_CheckedChanged(sender As Object, e As EventArgs) Handles chkisFTPTPA.CheckedChanged
        If chkisFTPTPA.Checked = True Then
            TxtTPAFTPAddress.Visible = True
        Else
            TxtTPAFTPAddress.Visible = False
        End If
    End Sub
End Class