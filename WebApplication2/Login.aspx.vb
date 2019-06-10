Imports SPGeneral
Imports System.IO

Public Class Login
    Inherits System.Web.UI.Page

    Dim _Clsusers As New WebService.ClsUser
    Dim _ClsEncryption As New WebService.ClsEncryption

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("act") = "1" Then
                Session.RemoveAll()
                If IsNothing(Request.Cookies("UserId")) = False Then
                    Response.Cookies("UserId").Expires = DateTime.Now.AddDays(-1)
                End If
            End If
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        System.Threading.Thread.Sleep(500)
        Try
            txtPassword.TextMode = TextBoxMode.Password
            ValidateLogin()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - login - " & Session("UserId") & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub ValidateLogin()
        Try

            _Clsusers.LoadData(txtUserid.Text.Trim, _ClsEncryption.Encrypt(txtPassword.Text.Trim))
            If _ClsEncryption.Encrypt(txtPassword.Text) <> _Clsusers.Pass Or txtPassword.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Sorry incorrect user name or password');</script>")
                txtPassword.Text = ""
            Else
                If _Clsusers.ExpirateDate < Today Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('User Expirate');</script>")
                    Exit Sub
                End If
                Response.Cookies("UserId").Value = txtUserid.Text.Trim
                Response.Cookies("Password").Value = _ClsEncryption.Encrypt(txtPassword.Text.Trim)
                Session.Add("Users", _Clsusers)

                Session.Add("OnlineKeyQuestion", _Clsusers.OnlineKeyQuestion)
                Session.Add("Questiondesc", _Clsusers.Questiondesc)
                Session.Add("OnlineKeyAnswer", _Clsusers.OnlineKeyAnswer)
                Session.Add("firstOnline", _Clsusers.firstOnline)
                Session.Add("UserId", _Clsusers.UserId)
                Session.Add("Username", _Clsusers.UserName)
                Session.Add("Password", _Clsusers.Pass)
                Session.Add("RoleCode", _Clsusers.RoleCode)
                Session.Add("RoleDesc", _Clsusers.RoleDesc)
                Session.Add("Judul", _Clsusers.Judul)
                Session.Add("Mkt_Code", _Clsusers.Mkt_Code)
                Session.Add("Email", _Clsusers.Email)
                Session.Add("BranchCodeStay", _Clsusers.BranchCodeStay)
                Session.Add("LvlAdmin", _Clsusers.LvlAdmin)
                Session.Add("Pict", _Clsusers.Pict)
                Session.Add("ExpirateDate", _Clsusers.ExpirateDate)
                Session.Add("Cre_Date", _Clsusers.Cre_Date)
                If Session("Pict") <> "" Then
                    If File.Exists(Server.MapPath(config.uploadFile & _Clsusers.UserId & Session("Pict"))) Then
                        Session("Pictadd") = config.uploadFile & _Clsusers.UserId & Session("Pict")
                    Else
                        Session("Pictadd") = config.uploadFile & "unknown1.png"
                    End If
                Else
                    Session("Pictadd") = config.uploadFile & "unknown1.png"
                End If

                _Clsusers.UserId = Session("Userid")
                _Clsusers.Online = "true"
                If _Clsusers.UpdateUseronlineUpdate() = True Then
                    _Clsusers.LoadData(txtUserid.Text.Trim, _ClsEncryption.Encrypt(txtPassword.Text.Trim))

                    Session.Add("OnlineDate", _Clsusers.OnlineDate)
                    Session.Add("OnlineIp", _Clsusers.OnlineIp)
                    Session.Add("OnlineDateCompare", _Clsusers.OnlineDateCompare)
                    Session.Add("OnlineIpCompare", _Clsusers.OnlineIpCompare)

                    If _Clsusers.firstOnline = True Then
                        Response.Redirect("UserProfile.aspx", False)
                        Exit Sub
                    End If
                End If

                Dim redirectPage As String = Request.QueryString("p")
                If String.IsNullOrEmpty(redirectPage) Then
                    Response.Redirect("home.aspx", False)
                Else
                    Response.Redirect(redirectPage, False)
                End If

            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - login - " & Session("UserId") & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath(), msg)
        End Try
    End Sub

    Protected Sub LinkForgot_Click(sender As Object, e As EventArgs) Handles LinkForgot.Click
        LinkMpeModalPopupExtender.Show()
        LblCap.Text = Format(CDate(Now), "ffff")
        txtCaptcha.Text = ""
    End Sub

    Protected Sub LinkSend_Click(sender As Object, e As EventArgs) Handles LinkSend.Click
        Try
            If LblCap.Text = Trim(txtCaptcha.Text) Then

                If txtPolicyNo.Text = "" Or txtMebid.Text = "" Or txtEmail.Text = "" Or txtBirthDate.Text = "" Or IsDate(CDate(txtBirthDate.Text)) = False Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Info, Please fill policy no, member id, Email and birth date');</script>")
                    Exit Sub
                End If
                If txtPolicyNo.Text <> "" Then
                    _Clsusers.POLICYNOINP = txtPolicyNo.Text
                    _Clsusers.memberid = txtMebid.Text
                    _Clsusers.BirthDate = CDate(txtBirthDate.Text)
                    If _Clsusers.findPolicyEmail(1) = "" Then
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Info, policy Not found');</script>")
                        Exit Sub
                    End If
                    If txtMebid.Text <> "" Then
                        Dim dt As DataTable = _Clsusers.findPolicyEmaildt(2)
                        If dt.Rows.Count <= 0 Then
                            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Info, member id not found');</script>")
                            Exit Sub
                        Else
                            Dim _sama As New WebService.sama
                            Dim bodymsg As String
                            bodymsg = "Yth Bapak/Ibu " & dt.Rows(0)(0).ToString & vbCrLf & vbCrLf
                            bodymsg = bodymsg & "Berikut kami kirim kan user id dan password peserta" & vbCrLf & vbCrLf
                            bodymsg = bodymsg & "User id : " & dt.Rows(0)(1).ToString & vbCrLf & vbCrLf
                            bodymsg = bodymsg & "Password : " & _ClsEncryption.Decrypt(dt.Rows(0)(2).ToString) & vbCrLf & vbCrLf
                            bodymsg = bodymsg & "Demikian kami sampaikan, atas perhatian dan kerjasamanya kami ucapkan terimakasih" & vbCrLf & vbCrLf & vbCrLf
                            bodymsg = bodymsg & "Salam," & vbCrLf & vbCrLf & vbCrLf
                            bodymsg = bodymsg & "Asuransi Reliance Indonesia,"


                            _sama.SendMail("admin.cc@reliance-insurance.com", txtEmail.Text, "[Dont reply] Password User", bodymsg, "admin.cc@reliance-insurance.com", "", "")
                            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Info, Please Check your email " & txtEmail.Text & "' );</script>")
                            Exit Sub
                        End If
                    End If

                End If
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Info, captcha not match' );</script>")
                Exit Sub
            End If
        Catch ex As Exception
            If ex.Message = "Conversion from string " + Chr(34) + Chr(34) + " to type 'Date' is not valid." Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Information, Please input date with format dd/MM/yyyy ');</script>")
                Exit Sub
            End If
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - login - " & Session("UserId") & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath(), msg)
        End Try
    End Sub
End Class