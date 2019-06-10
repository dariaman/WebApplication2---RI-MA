
Imports SPGeneral
Public Class Branch
    Inherits System.Web.UI.Page

    Dim _ClsBranch As New WebService.ClsBranch
    'Dim _ClsProduk As New WebService.ClsProduct
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
                        Session("DashBoard") = "Office Station List <i class='fa fa-building fa-fw'></i>"
                        Dim filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If

                        doreset()
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - Branch - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=Branch.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=Branch.aspx", False)
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click, LinkSubmit.Click
        Try
            System.Threading.Thread.Sleep(500)
            If validasi() = True Then
                _ClsBranch.BranchCode = txtBranchCode.Text
                _ClsBranch.BranchName = txtBranchname.Text
                _ClsBranch.BranchAbbreviation = txtBranchAbbreviation.Text
                _ClsBranch.BranchAdd = txtBranchAdd.Text
                _ClsBranch.BranchCity = txtBranchCity.Text
                _ClsBranch.BranchZIP = txtBranchZIP.Text
                _ClsBranch.BranchPhone = txtBranchPhone.Text
                _ClsBranch.BranchFax = txtBranchFax.Text
                _ClsBranch.IsActive = chkAktiv.Checked
                _ClsBranch.NPWP = txtNPWP.Text
                _ClsBranch.CRE_BY = UserLogin.UserId

                If _ClsBranch.InsertBranch() = True Then
                    bindData(TxtKeyWord.Text)
                    LinkSubmit.Enabled = False
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data saved!');</script>")
                End If
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Branch - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Public Function validasi() As Boolean
        If txtBranchCode.Text = "" Or txtBranchname.Text = "" Or txtBranchAdd.Text = "" Or txtBranchCity.Text = "" Or _
            txtBranchCity.Text = "" Or txtBranchZIP.Text = "" _
            Or txtBranchPhone.Text = "" Or txtBranchFax.Text = "" Or txtNPWP.Text = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('All Fields cannot empty!');</script>")
            Return False
        Else
            Return True
        End If
        Return True
    End Function

    Sub doreset()
        txtBranchCode.Text = ""
        txtBranchAbbreviation.Text = ""
        txtBranchname.Text = ""
        txtBranchAdd.Text = ""
        txtBranchCity.Text = ""
        txtBranchZIP.Text = ""
        txtBranchPhone.Text = ""
        txtBranchFax.Text = ""
        txtNPWP.Text = ""
        chkAktiv.Checked = True
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            gridMenu.PageIndex = 0
            bindData(TxtKeyWord.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Branch - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Protected Sub bindData(kd_branch As String)
        Try
            gridMenu.DataSource = _ClsBranch.bindData(kd_branch, ddlSearch.SelectedValue)
            gridMenu.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub bindisiData(kd_branch As String)
        Try
            Dim dt As New DataTable
            dt = _ClsBranch.bindisiData(kd_branch)
            If dt.Rows.Count > 0 Then
                txtBranchCode.Text = dt.Rows(0)(0).ToString
                txtBranchAbbreviation.Text = dt.Rows(0)(1).ToString
                txtBranchname.Text = dt.Rows(0)(2).ToString
                txtBranchAdd.Text = dt.Rows(0)(3).ToString
                txtBranchCity.Text = dt.Rows(0)(4).ToString
                txtBranchZIP.Text = dt.Rows(0)(5).ToString
                txtBranchPhone.Text = dt.Rows(0)(6).ToString
                txtBranchFax.Text = dt.Rows(0)(7).ToString
                txtNPWP.Text = dt.Rows(0)(8).ToString
                chkAktiv.Checked = dt.Rows(0)(9).ToString
            Else
                txtBranchCode.Text = ""
                txtBranchAbbreviation.Text = ""
                txtBranchname.Text = ""
                txtBranchAdd.Text = ""
                txtBranchCity.Text = ""
                txtBranchZIP.Text = ""
                txtBranchPhone.Text = ""
                txtBranchFax.Text = ""
                txtNPWP.Text = ""
                chkAktiv.Checked = False
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        doreset()
        PnlMain.Visible = False
        pnlPopup.Visible = True
        txtBranchCode.ReadOnly = False
    End Sub

    Protected Sub LinkClose4_Click(sender As Object, e As EventArgs) Handles LinkClose.Click
        System.Threading.Thread.Sleep(500)
        PnlMain.Visible = True
        pnlPopup.Visible = False
        LinkSubmit.Enabled = True
    End Sub

    Private Sub gridMenu_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridMenu.PageIndexChanging
        Try
            System.Threading.Thread.Sleep(500)
            gridMenu.PageIndex = e.NewPageIndex
            bindData(TxtKeyWord.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Branch - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub gridMenu_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridMenu.RowCommand
        Try
            System.Threading.Thread.Sleep(500)
            If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then
                Dim KEY As String = e.CommandArgument
                bindisiData(KEY)
                PnlMain.Visible = False
                pnlPopup.Visible = True
                txtBranchCode.ReadOnly = True
            End If
            If e.CommandName = "UpdateLink" Then
                System.Threading.Thread.Sleep(500)
                Dim KEY As String = e.CommandArgument

                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim index As Integer = gvRow.RowIndex
                Dim sts As String = DirectCast(gridMenu.Rows(index).FindControl("hfstatus"), HiddenField).Value
                '_ClsInventarisItem.NO = KEY
                '_ClsInventarisItem.STATUS = IIf(sts = 1, 0, 1)
                '_ClsInventarisItem.updateStatus()
                _sama.UpdateActive("MSBranch", "ISACTIVE", IIf(sts = "True", "False", "True"), "BranchCode", KEY)
                bindData(TxtKeyWord.Text)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Branch - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub gridMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridMenu.SelectedIndexChanged

    End Sub
End Class