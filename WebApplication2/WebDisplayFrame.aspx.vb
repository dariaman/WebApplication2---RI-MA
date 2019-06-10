
Imports SPGeneral
Public Class WebDisplayFrame
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
                        Session("DashBoard") = Request.QueryString("act") & "<i class='fa fa-user fa-fw'></i>"
                        'Session("Search") = "<i class='fa fa-search'></i>"
                        'btnSearch. = Session("Search")
                        'Dim filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        'If _sama.MenuAccess(filename, Session("UserId")) = False Then
                        '    Response.Redirect("Home.aspx", False)
                        'End If
                        'sama.isiddlSource(ddlSourceAP, Session("Userid"))
                        'sama.isiddlSourceUsr(ddlStoreAP, Session("userid"), Session("rolecode"))

                        'LtrFrame.Text = " <iframe class='table table-striped table-bordered table-hover' src=" & Chr(34) & "http://www.w3schools.com" & Chr(34) & " width='100%' frameborder='0' scrolling='yes' height='1000px'></iframe>"

                        If Request.QueryString("act") <> "" Then
                            ContentIframe.Attributes("src") = Request.QueryString("act")
                        End If

                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error terjadi. Mohon kontak IT support.');</script>")
                        Dim msg As String = String.Format("{0} - UserList - " & Session("userid") & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=UserList.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=UserList.aspx", False)
        End If
    End Sub

End Class