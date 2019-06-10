
Imports SPGeneral
Public Class register
    Inherits System.Web.UI.Page

    Dim _Clsusers As New WebService.ClsUser
    Dim _ClsEncryption As New WebService.ClsEncryption

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("act") = "1" Then
                Session.RemoveAll()
            End If
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnSignUp.Click
        System.Threading.Thread.Sleep(500)
        Try
            ValidateLogin()

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - login - " & Session("UserId") & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub ValidateLogin()
        Try

            If txtUserEmail.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Please fill Email');</script>")
                Exit Sub
            ElseIf txtUsername.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Please fill Name');</script>")
                Exit Sub
            ElseIf CbTerm.Checked = False Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Please read the term');</script>")
                Exit Sub
            Else
                If _Clsusers.LoadUseremail(txtUserEmail.Text) = True Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Email " & txtUserEmail.Text & " is already created. Please use other email');</script>")
                Else
                    _Clsusers.UserNameInp = txtUsername.Text
                    _Clsusers.EmailInp = txtUserEmail.Text
                    If _Clsusers.InsertUserSignUp() = True Then
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('User " & _Clsusers.UserId & " created. password : getsoft ');</script>")
                    End If
                End If
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - login - " & Session("UserId") & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath(), msg)
        End Try
    End Sub

End Class