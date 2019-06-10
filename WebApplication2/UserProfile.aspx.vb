Imports SPGeneral
Imports System.IO

Public Class UserProfile
    Inherits System.Web.UI.Page
    Dim _Clsusers As New WebService.ClsUser
    Dim _ClsEncryption As New WebService.ClsEncryption
    Dim _sama As New WebService.sama
    Dim ext As String

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
                Session("DashBoard") = "User Profile <i class='fa fa-book fa-fw'></i>"

                Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)

                If UserLogin.Email = "" Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Info, Harap lengkapi email');</script>")
                End If

                If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                    Response.Redirect("Home.aspx", False)
                End If
                If UserLogin.IsActive Then
                    lblUserId.Text = UserLogin.UserId
                    
                    lblUsername.Text = Session("UserName")
                    lblEmail.Text = Session("Email")
                    _sama.isiddlQuestion(ddlQuestion)
                    ddlQuestion.SelectedValue = Session("OnlineKeyQuestion")
                    txtAnswer.Text = Session("OnlineKeyAnswer")
                Else
                    Response.Redirect("login.aspx?p=UserProfile.aspx", False)
                End If
                'Image1.ImageUrl = Session("Pictadd")
            End If
        Else
            Response.Redirect("login.aspx?p=UserProfile.aspx", False)
        End If
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            System.Threading.Thread.Sleep(500)
            _Clsusers.OnlineKeyQuestion = ddlQuestion.SelectedValue
            _Clsusers.OnlineKeyAnswer = txtAnswer.Text
            _Clsusers.UserId = UserLogin.UserId
            If _Clsusers.UpdateUserQuestion(Session("Pict")) = True Then
                _Clsusers.LoadData(UserLogin.UserId, UserLogin.Pass)
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data saved');</script>")
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UserProfile - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath(), msg)
        End Try
    End Sub

    Protected Sub LinkUpload_Click(sender As Object, e As EventArgs) Handles LinkUpload.Click

        System.Threading.Thread.Sleep(500)
        If FileUpload1.PostedFile.ContentLength > 100000 Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('File to big');</script>")
            Exit Sub
        End If

        ext = UCase(System.IO.Path.GetExtension(FileUpload1.FileName))
        If System.IO.File.Exists(Server.MapPath(config.uploadFile & lblUserId.Text & ext)) Then
            Kill(Server.MapPath(config.uploadFile & lblUserId.Text & ext))
        End If

        If ext = ".JPG" Or ext = ".BMP" Or ext = ".GIF" Or ext = ".PNG" Or ext = ".JPG" Or ext = ".JPEG" Then
            FileUpload1.SaveAs(Server.MapPath(config.uploadFile & lblUserId.Text & ext))
            Session("Pict") = ext
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('File not picture format');</script>")

        End If

        'Image1.ImageUrl = config.uploadFile & lblUserId.Text & ext
    End Sub

    Protected Sub LinkChangePass_Click(sender As Object, e As EventArgs) Handles LinkChangePass.Click
        System.Threading.Thread.Sleep(500)
        Response.Redirect("changePassword.aspx")
    End Sub

    Protected Sub LinkChangeDetail_Click(sender As Object, e As EventArgs) Handles LinkChangeDetail.Click
        System.Threading.Thread.Sleep(500)
        Response.Redirect("UserDetail1.aspx")
    End Sub
End Class