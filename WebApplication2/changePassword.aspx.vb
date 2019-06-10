Imports SPGeneral
Public Class changePassword
    Inherits System.Web.UI.Page

    Dim _Clsusers As New WebService.ClsUser
    Dim _ClsEncryption As New WebService.ClsEncryption
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
                Session("DashBoard") = "Change Password <i class='fa fa-lock fa-fw'></i>"
                Dim Filename As String = System.IO.Path.GetFilename(Request.PhysicalPath)
                If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                    Response.Redirect("Home.aspx", False)
                End If
                If UserLogin.IsActive Then
                    lblUsername.Text = UserLogin.Username
                Else
                    Response.Redirect("login.aspx?p=changePassword.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=changePassword.aspx", False)
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try
            System.Threading.Thread.Sleep(500)
            If String.IsNullOrEmpty(txtOldPassword.Text) Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Enter your Old Password.');</script>")
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtNewPassword.Text) Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Enter your New Password.');</script>")
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtConfirm.Text) Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('re-Enter your Password.');</script>")
                Exit Sub
            End If

            If Not _ClsEncryption.Encrypt(txtOldPassword.Text) = UserLogin.Pass Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Your old password is not correct, please try again');</script>")
                Exit Sub
            End If

            If String.Compare(txtNewPassword.Text, txtConfirm.Text) <> 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Confirmation password is not match.');</script>")
                Exit Sub
            End If

            UserLogin.ubahpass(_ClsEncryption.Encrypt(txtNewPassword.Text), UserLogin.UserId, UserLogin.UserId)
            _Clsusers.LoadData(UserLogin.UserId, _ClsEncryption.Encrypt(txtNewPassword.Text))
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Password change!');</script>")

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - changePassword - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub LinkUserprofile_Click(sender As Object, e As EventArgs) Handles LinkUserprofile.Click
        Response.Redirect("userProfile.aspx")
    End Sub
End Class