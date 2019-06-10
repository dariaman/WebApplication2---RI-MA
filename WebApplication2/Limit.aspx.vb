
Imports SPGeneral
Public Class Limit
    Inherits System.Web.UI.Page

    Dim _ClsLimit As New WebService.ClsLimit
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
                        Session("DashBoard") = "Limit List <i class='fa fa-bar-chart-o fa-fw'></i>"
                        Dim filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If

                        doreset()
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - Limit - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.branchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=Limit.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=Limit.aspx", False)
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click, LinkSubmit.Click
        Try
            System.Threading.Thread.Sleep(500)
            If validasi() = True Then
                _ClsLimit.POLICYNO = txtPolicyNo.Text
                _ClsLimit.Limit = Replace(txtLimit.Text, ".", "")
                _ClsLimit.IsActive = chkAktiv.Checked
                _ClsLimit.CRE_BY = UserLogin.UserId

                If _ClsLimit.InsertLimit() = True Then
                    bindData(TxtKeyWord.Text)
                    LinkSubmit.Enabled = False
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data saved!');</script>")
                End If
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Limit - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Public Function validasi() As Boolean
        If txtPolicyNo.Text = "" Or txtLimit.Text = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('All Fields cannot empty!');</script>")
            Return False
        Else
            Return True
        End If
        Return True
    End Function

    Sub doreset()
        txtLimit.Text = ""
        txtPolicyNo.Text = ""
        chkAktiv.Checked = True
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            gridMenu.PageIndex = 0
            bindData(TxtKeyWord.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Limit - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.branchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Protected Sub bindData(PolicyNo As String)
        Try
            _ClsLimit.POLICYNO = PolicyNo
            gridMenu.DataSource = _ClsLimit.bindDataLimit()
            gridMenu.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub bindisiData(PolicyNo As String)
        Try
            Dim dt As New DataTable
            _ClsLimit.POLICYNO = PolicyNo
            dt = _ClsLimit.bindDataLimit()
            If dt.Rows.Count > 0 Then
                txtPolicyNo.Text = dt.Rows(0)(0).ToString
                txtLimit.Text = FormatNumber(dt.Rows(0)(1).ToString, 0)
                txtPolicyNo.Text = dt.Rows(0)(0).ToString
                chkAktiv.Checked = dt.Rows(0)(2).ToString
                LblCLIENTNAME.Text = dt.Rows(0)(7).ToString
            Else
                txtPolicyNo.Text = ""
                txtLimit.Text = "0"
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
        txtPolicyNo.ReadOnly = False
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
            Dim msg As String = String.Format("{0} - Limit - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.branchCode, ex, Environment.NewLine)
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
                txtPolicyNo.ReadOnly = True
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
                _sama.UpdateActive("MSLimit", "ISACTIVE", IIf(sts = "True", "False", "True"), "branchCode", KEY)
                bindData(TxtKeyWord.Text)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Limit - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.branchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub Search1_Click(sender As Object, e As EventArgs) Handles Search1.Click
        _ClsLimit.POLICYNO = txtPolicyNo.Text
        LblCLIENTNAME.Text = _ClsLimit.PolicyClient()
    End Sub
End Class