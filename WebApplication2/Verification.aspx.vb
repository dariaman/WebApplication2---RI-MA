Imports SPGeneral
Imports System.IO

Public Class Verification
    Inherits System.Web.UI.Page

    Dim _Clsusers As New WebService.ClsUser
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
                        txtOnlineQuestion.Text = Session("Questiondesc")
                        Image1.ImageUrl = Session("Pictadd")
                        If txtOnlineQuestion.Text = "" Then
                            txtOnlineQuestion.Text = "Skip it, if you don't have any answer"
                        End If
                        'If Session("Pict") <> "" Then
                        '    If File.Exists(Server.MapPath(config.uploadFile & UserLogin.UserId & Session("Pict"))) Then
                        '        Image1.ImageUrl = Session("Pictadd")
                        '    Else
                        '        imgprofilesm.ImageUrl = config.uploadFile & "unknown1.png"
                        '        imgfront.ImageUrl = config.uploadFile & "unknown1.png"
                        '        imgprofilelg.ImageUrl = config.uploadFile & "unknown1.png"
                        '        Session("Pictadd") = config.uploadFile & "unknown1.png"
                        '    End If
                        '    'If System.IO.File.Exists(Server.MapPath(config.uploadFile & UserLogin.UserId & Session("Pict"))) Then

                        'Else
                        '    imgprofilesm.ImageUrl = config.uploadFile & "unknown1.png"
                        '    imgfront.ImageUrl = config.uploadFile & "unknown1.png"
                        '    imgprofilelg.ImageUrl = config.uploadFile & "unknown1.png"
                        '    Session("Pictadd") = config.uploadFile & "unknown1.png"
                        'End If

                        txtOnlineAnswer.Text = ""
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - Verification - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=login.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=UserList.aspx", False)
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        System.Threading.Thread.Sleep(500)
        Try
            ValidateVerifikasi()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Verification - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub ValidateVerifikasi()
        Try
            txtOnlineQuestion.Text = Session("Question")
            If txtOnlineAnswer.Text = Session("OnlineKeyAnswer") Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Same answer, relogin again');</script>")

                _Clsusers.UserId = UserLogin.UserId
                _Clsusers.Online = "false"
                If _Clsusers.UpdateUseronlineUpdate() = True Then

                    _Clsusers.UserId = UserLogin.UserId
                    _Clsusers.Online = "true"
                    If _Clsusers.UpdateUseronlineUpdate() = True Then
                        _Clsusers.LoadData(UserLogin.UserId, Session("Password"))

                        Session("OnlineDate") = _Clsusers.OnlineDate
                        Session("OnlineIp") = _Clsusers.OnlineIp
                        Session("OnlineDateCompare") = _Clsusers.OnlineDateCompare
                        Session("OnlineIpCompare") = _Clsusers.OnlineIpCompare
                        If Session("firstOnline") = "True" Then
                            Response.Redirect("UserProfile.aspx", False)
                            Exit Sub
                        End If
                    End If
                    Response.Redirect("home.aspx", False)
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Please contact your Admin!');</script>")
                    txtOnlineQuestion.Text = Session("Questiondesc")
                    txtOnlineAnswer.Text = ""
                End If
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Try again!');</script>")
                txtOnlineQuestion.Text = Session("Questiondesc")
                txtOnlineAnswer.Text = ""
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Verification - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath(), msg)
        End Try
    End Sub


End Class