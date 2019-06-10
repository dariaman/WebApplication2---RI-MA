Imports System.Data.SqlClient
Imports SPGeneral
Public Class resetPassword
    Inherits System.Web.UI.Page

    Dim _Clsusers As New WebService.ClsUser
    Dim _sama As New WebService.sama
    Dim _clsrole As New WebService.ClsRole
    Dim _ClsEncryption As New WebService.ClsEncryption
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
                    Session("DashBoard") = "Reset Password <i class='fa fa-unlock fa-fw'></i>"
                    Dim filename As String = System.IO.Path.GetFilename(Request.PhysicalPath)
                    If _sama.MenuAccess(filename, UserLogin.UserId) = False Then
                        Response.Redirect("Home.aspx", False)
                    End If
                Else
                    Response.Redirect("login.aspx?p=resetPassword.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=resetPassword.aspx", False)
        End If
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click, LinkReset.Click
        Try
            If lblUserId.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Select user');</script>")
                Exit Sub
            End If
            Dim passreset As String = _ClsEncryption.Encrypt(Format(CDate(Now), "fffff"))
            _Clsusers.ubahpass(passreset, lblUserId.Text, UserLogin.UserId)
            SelectDataMSUserlike(ddlSearch.SelectedValue, lblUserId.Text)
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Password reset menjadi = " & _ClsEncryption.Decrypt(passreset) & "');</script>")
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - resetPassword - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Private Sub SelectDataMSUserlike(type As String, userid As String)
        Try
            gridMenu.DataSource = _Clsusers.SelectDataMSUserlikeresetpass(type, userid, UserLogin.UserId.ToString)
            gridMenu.DataBind()
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try

    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            gridMenu.PageIndex = 0
            SelectDataMSUserlike(ddlSearch.SelectedValue, txtSearch.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - resetPassword - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Private Sub gridMenu_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridMenu.PageIndexChanging
        Try
            gridMenu.PageIndex = e.NewPageIndex
            SelectDataMSUserlike(ddlSearch.SelectedValue, txtSearch.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - resetPassword - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub gridMenu_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridMenu.RowCommand
        Try
            Dim index As Integer
            If e.Commandname.Equals("Select") Then
                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                index = gvRow.RowIndex
            ElseIf e.Commandname.Equals("SelectLink") Then
                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                index = gvRow.RowIndex
            End If
            gridMenu.PageIndex = 0
            If e.Commandname.Equals("Select") Or e.Commandname.Equals("SelectLink") Then
               
                lblUserId.Text = e.CommandArgument
                lblUsername.Text = gridMenu.Rows(index).Cells(1).Text
                lblPassword.Text = _ClsEncryption.Decrypt(gridMenu.Rows(index).Cells(3).Text)
                LinkMpeModalPopupExtender.Show()
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - resetPassword - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub gridMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridMenu.SelectedIndexChanged

    End Sub
End Class