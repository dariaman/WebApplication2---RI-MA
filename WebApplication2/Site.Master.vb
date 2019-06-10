Imports System.IO

Public Class Site
    Inherits System.Web.UI.MasterPage
    Dim _Clsusers As New WebService.ClsUser
    Dim _ClsMenu As New WebService.ClsMenu
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
        '_Clsusers.LoadUserid(Session("UserId"))
        'If _Clsusers.Online = "False" Or CStr(Session("OnlineDate")) <> CStr(_Clsusers.OnlineDateCompare) Then
        '    Session.Abandon()
        '    Response.Redirect("login.aspx")
        'ElseIf _Clsusers.Verification = "True" Then
        '    Response.Redirect("Verification.aspx")
        'End If
        If Not Page.IsPostBack Then
            lblDashboard.Text = Session("DashBoard")
            If UserLogin.UserId = Nothing Or UserLogin.UserId = "" Then
                Response.Redirect("login.aspx", False)
                Exit Sub
            End If
            Dim dt As DataTable = _ClsMenu.bindGroup(UserLogin.UserId)
            For i As Integer = 0 To dt.Rows.Count - 1
                ltrlMenuhome.Text = ltrlMenuhome.Text & _ClsMenu.BindMenu(Session("role"), UserLogin.UserId, dt.Rows(i)(0).ToString)
            Next
            LblUser.Text = UserLogin.UserName
            LblUser1.Text = UserLogin.UserName
            LblUser2.Text = "<B>" & UserLogin.UserName & "</B> <Br /> " & Session("RoleDesc")
            'LblCre_Date.Text = Format(Session("Cre_Date"), "MMM yyyy")
            'LblExpirateDate.Text = Format(Session("ExpirateDate"), "MMM yyyy")

            'If Session("Pict") <> "" Then
            '    If File.Exists(Server.MapPath(config.uploadFile & UserLogin.UserId & Session("Pict"))) Then
            '        imgprofilesm.ImageUrl = config.uploadFile & UserLogin.UserId & Session("Pict")
            '        imgfront.ImageUrl = config.uploadFile & UserLogin.UserId & Session("Pict")
            '        imgprofilelg.ImageUrl = config.uploadFile & UserLogin.UserId & Session("Pict")
            '    Else
            '        imgprofilesm.ImageUrl = config.uploadFile & "unknown1.png"
            '        imgfront.ImageUrl = config.uploadFile & "unknown1.png"
            '        imgprofilelg.ImageUrl = config.uploadFile & "unknown1.png"
            '    End If
            '    'If System.IO.File.Exists(Server.MapPath(config.uploadFile & UserLogin.UserId & Session("Pict"))) Then

            'Else
            '    imgprofilesm.ImageUrl = config.uploadFile & "unknown1.png"
            '    imgfront.ImageUrl = config.uploadFile & "unknown1.png"
            '    imgprofilelg.ImageUrl = config.uploadFile & "unknown1.png"
            'End If
            If _ClsCompanyPolicy.PictureScalar(UserLogin.POLICYNO) = "" Then
                imgprofilesm.ImageUrl = Session("Pictadd")
                imgfront.ImageUrl = Session("Pictadd")
                imgprofilelg.ImageUrl = Session("Pictadd")

            Else
                imgprofilesm.ImageUrl = config.PictCompanyPath & UserLogin.CompanyPicture
                imgfront.ImageUrl = config.PictCompanyPath & UserLogin.CompanyPicture
                imgprofilelg.ImageUrl = config.PictCompanyPath & UserLogin.CompanyPicture


            End If


        End If
    End Sub

    Protected Sub LinkLogout_Click(sender As Object, e As EventArgs)

        _Clsusers.UserId = UserLogin.UserId
        _Clsusers.Online = "false"
        If _Clsusers.UpdateUseronlineUpdate() = True Then
            Session.Abandon()
            Session.RemoveAll()
            If IsNothing(Request.Cookies("UserId")) = False Then
                Response.Cookies("UserId").Expires = DateTime.Now.AddDays(-1)
            End If
            If IsNothing(Request.Cookies("Password")) = False Then
                Response.Cookies("Password").Expires = DateTime.Now.AddDays(-1)
            End If
            Response.Redirect("login.aspx")
        End If
    End Sub

End Class