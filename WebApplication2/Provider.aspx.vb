
Imports SPGeneral
Imports System.IO

Public Class Provider
    Inherits System.Web.UI.Page

    Dim _Clsusers As New WebService.ClsUser
    Dim _ClsEncryption As New WebService.ClsEncryption
    Dim _ClsProvider As New WebService.ClsProvider
    Dim _sama As New WebService.sama
    Public Property UserLogin() As WebService.ClsUser
        Get
            Return CType(Session("Users"), WebService.ClsUser)
        End Get
        Set(ByVal value As WebService.ClsUser)
            Session("Users") = value
        End Set
    End Property

    Sub test()
        Dim cookieCols As New HttpCookieCollection

        cookieCols = Request.Cookies

        Dim str As String
        'Dim b(1) As String
        'Dim i As Integer

        'For Each str In cookieCols
        'If str = "lat" Then
        'Session("lat") = Request.Cookies("lat").Value
        'End If
        'If str = "longt" Then
        'Session("longt") = Request.Cookies("longt").Value
        'End If

        'If str = "UserId" Then
        Session("UserId") = Request.Cookies("UserId").Value
        'End If

        'If str = "Password" Then
        Session("Password") = Request.Cookies("Password").Value
        'End If

        'b(i) = "Cookie: " & str & " : " & Request.Cookies(str).Value
        'ListBox1.Items.Add("Value:" & Request.Cookies(str).Value)
        'Next
    End Sub

    Private Sub ValidateLogin()
        Try
            _Clsusers.LoadData(Session("UserId"), Session("Password"))
            Session.Add("Users", _Clsusers)
            Session.Add("Username", _Clsusers.UserName)

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
                _Clsusers.LoadData(Session("UserId"), Session("Password"))

                Session.Add("OnlineDate", _Clsusers.OnlineDate)
                Session.Add("OnlineIp", _Clsusers.OnlineIp)
                Session.Add("OnlineDateCompare", _Clsusers.OnlineDateCompare)
                Session.Add("OnlineIpCompare", _Clsusers.OnlineIpCompare)

            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Provider - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            test()
            ValidateLogin()
            If Not UserLogin Is Nothing Then
                If UserLogin.IsActive Then
                    Try
                        'Dim lat As Decimal = CDec(Replace(Request.QueryString("Lat"), ".", ","))
                        'Dim longt As Decimal = CDec(Replace(Request.QueryString("longt"), ".", ","))

                        Session("lat") = Request.QueryString("Lat")
                        Session("longt") = Request.QueryString("longt")

                        Session("DashBoard") = "Provider List <i class='fa fa-building fa-fw'></i>"
                        'Dim filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        'If _sama.MenuAccess(filename, UserLogin.UserId) = False Then
                        '    Response.Redirect("Home.aspx", False)
                        'End If
                        RBProviderType.DataSource = _sama.isiProviderType()
                        RBProviderType.DataTextField = "PROVIDERTYPENAME"
                        RBProviderType.DataValueField = "PROVIDERTYPEID"
                        RBProviderType.DataBind()

                        RBProviderType1.DataSource = _sama.isiProviderType()
                        RBProviderType1.DataTextField = "PROVIDERTYPENAME"
                        RBProviderType1.DataValueField = "PROVIDERTYPEID"
                        RBProviderType1.DataBind()
                        RBProviderType1.Items.Add("All")
                        RBProviderType1.SelectedIndex = 3


                        If UserLogin.POLICYNO <> "" Or UserLogin.POLICYNO <> Nothing Then
                            If _ClsProvider.GETTPAID(UserLogin.POLICYNO, "1") = True Then
                            Else
                                RBProviderType1.Items.Remove(RBProviderType1.Items.FindByValue(1))
                            End If
                        Else
                            RBProviderType1.Items.Remove(RBProviderType1.Items.FindByValue(1))
                        End If

                        doreset()
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - Provider - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=Provider.aspx", False)
                End If
            Else

                Response.Redirect("login.aspx?p=Provider.aspx", False)
            End If
        End If
    End Sub

    
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click, LinkSubmit.Click
        Try
            System.Threading.Thread.Sleep(500)
            If validasi() = True Then

                _ClsProvider.PROVIDERID = txtProviderCode.Text
                _ClsProvider.PROVIDERTYPE = RBProviderType.SelectedValue
                _ClsProvider.PROVIDERNAME = txtProvidername.Text

                If _ClsProvider.InsertProvider() = True Then
                    bindData(TxtKeyWord.Text, Session("lat"), Session("longt"), RBProviderType1.SelectedValue, UserLogin.POLICYNO)
                    LinkSubmit.Enabled = False
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data saved!');</script>")
                End If
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Provider - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Public Function validasi() As Boolean
        If txtProviderCode.Text = "" Or txtProvidername.Text = "" Or RBProviderType.SelectedValue = Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('All Fields cannot empty!');</script>")
            Return False
        Else
            Return True
        End If
        Return True
    End Function

    Sub doreset()
        txtProviderCode.Text = ""
        txtProvidername.Text = ""
        RBProviderType.SelectedValue = Nothing
        txtRemark.Text = ""
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            gridMenu.PageIndex = 0
            bindData(TxtKeyWord.Text, Session("lat"), Session("longt"), RBProviderType1.SelectedValue, UserLogin.POLICYNO)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Provider - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub bindData(RangeRS As String, lat As String, longt As String, group As String, policyno As String)
        Try
            gridMenu.DataSource = _ClsProvider.bindData(Replace(RangeRS, "_", ""), lat, longt, group, policyno)
            gridMenu.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub bindisiData(kd_Provider As String)
        Try
            Dim dt As New DataTable
            dt = _ClsProvider.bindisiData(kd_Provider)
            If dt.Rows.Count > 0 Then
                txtProviderCode.Text = dt.Rows(0)(0).ToString
                txtProvidername.Text = dt.Rows(0)(2).ToString
                RBProviderType.SelectedValue = IIf(dt.Rows(0)(3).ToString = "", Nothing, dt.Rows(0)(3).ToString)
                txtRemark.Text = dt.Rows(0)(31).ToString
            Else
                txtProviderCode.Text = ""
                txtProvidername.Text = ""
                RBProviderType.SelectedValue = Nothing
                txtRemark.Text = ""
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        doreset()
        PnlMain.Visible = False
        pnlPopup.Visible = True
        txtProviderCode.ReadOnly = False
        txtProvidername.ReadOnly = False
        txtRemark.ReadOnly = False
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
            bindData(TxtKeyWord.Text, Session("lat"), Session("longt"), RBProviderType1.SelectedValue, UserLogin.POLICYNO)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Provider - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub gridMenu_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridMenu.RowCommand
        Try
            System.Threading.Thread.Sleep(500)
            'If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then
            '    Dim KEY As String = e.CommandArgument
            '    Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            '    Dim index As Integer = gvRow.RowIndex

            '    bindisiData(KEY)
            '    PnlMain.Visible = False
            '    pnlPopup.Visible = True
            '    txtProviderCode.ReadOnly = True
            '    txtProviderCode.ReadOnly = True
            '    txtProvidername.ReadOnly = True
            '    txtRemark.ReadOnly = True

            'End If
            'If e.CommandName = "UpdateLink" Then
            '    System.Threading.Thread.Sleep(500)
            '    Dim KEY As String = e.CommandArgument

            '    Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            '    Dim index As Integer = gvRow.RowIndex
            '    Dim sts As String = DirectCast(gridMenu.Rows(index).FindControl("hfstatus"), HiddenField).Value
            '    '_ClsInventarisItem.NO = KEY
            '    '_ClsInventarisItem.STATUS = IIf(sts = 1, 0, 1)
            '    '_ClsInventarisItem.updateStatus()
            '    _sama.UpdateActive("MSProvider", "ISACTIVE", IIf(sts = "True", "False", "True"), "ProviderCode", KEY)
            '    bindData(TxtKeyWord.Text, Session("lat"), Session("longt"), RBProviderType1.SelectedValue)
            'End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - Provider - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

End Class