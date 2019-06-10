
Imports SPGeneral
Imports System.IO
Public Class GenerateUser
    Inherits System.Web.UI.Page

    Dim _Clsuser As New WebService.ClsUser
    'Dim _ClsProduk As New WebService.ClsProduct
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
                If UserLogin.IsActive Then
                    Try
                        Session("DashBoard") = "Generate User policy <i class='fa fa-users fa-fw'></i>"
                        Dim filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                        _sama.isiddlRolegenuser(ddlRoleAP)
                        'doreset()
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - GenerateUser - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=GenerateUser.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=GenerateUser.aspx", False)
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click, LinkSubmit.Click
        Try
            System.Threading.Thread.Sleep(500)
            If validasi() = True Then

                '_ClsProvider.PROVIDERID = txtProviderCode.Text
                ''_ClsProvider.PROVIDERTYPE = RBProviderType.SelectedValue
                '_ClsProvider.PROVIDERNAME = txtProvidername.Text

                'If _ClsProvider.InsertProvider() = True Then
                '    'bindData(TxtKeyWord.Text, RBProviderType1.SelectedValue)
                '    LinkSubmit.Enabled = False
                '    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data saved!');</script>")
                'End If
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - GenerateUser - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Public Function validasi() As Boolean
        If txtProviderCode.Text = "" Or txtProvidername.Text = "" Then
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
        txtRemark.Text = ""
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            gridMenu.PageIndex = 0
            bindData(TxtKeyWord.Text, IIf(RBType1.SelectedValue = "New", "", RBType1.SelectedValue), IIf(RBType1.SelectedValue = "New", "", RBType1.SelectedValue))
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - GenerateUser - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Protected Sub bindData(policy_no_rel As String, policy_no As String, membid As String)
        Try
            Dim dt As DataTable = _Clsuser.SelectAllUser(policy_no_rel, policy_no, membid)
            gridMenu.DataSource = dt
            gridMenu.DataBind()
            
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            System.Threading.Thread.Sleep(2500)

            If TxtKeyWord.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Keyword masih kosong');</script>")
                TxtKeyWord.Focus()
                Exit Sub
            End If
            gridMenu.DataSource = Nothing
            gridMenu.DataBind()
            'RBType1.SelectedIndex = 0

            Session("flnm") = TxtKeyWord.Text & "_" & Now.ToString("yyyyMMddHHmmss") & ".txt"
            Dim flnm = Session("flnm")
            Dim strFile As String = config.createFiletxt & flnm
            'Dim fileExists As Boolean = File.Exists(strFile)
            Dim fs As FileStream = File.Create(strFile)
            fs.Close()

            '_Clsuser.SelectAllUser(TxtKeyWord.Text, "", "")

            Dim dt As DataTable = _Clsuser.SelectAllUser(TxtKeyWord.Text, IIf(RBType1.SelectedValue = "New", "", RBType1.SelectedValue), IIf(RBType1.SelectedValue = "New", "", RBType1.SelectedValue))
            Dim i As Integer = 0
            Dim objWriter As New System.IO.StreamWriter(strFile, True)
            objWriter.WriteLine("User Id | User Name | Policy | MemberId | Password | Sub Group | Emp Id ", "")
            Do While i <= dt.Rows.Count - 1
                If dt.Rows(i)(0).ToString = "" Then

                    _Clsuser.UserIdInp = ""
                    _Clsuser.UserNameInp = Replace(dt.Rows(i)(4).ToString, "'", "`")
                    _Clsuser.EmailInp = ""

                    _Clsuser.RoleCodeInp = ddlRoleAP.SelectedValue

                    _Clsuser.IsActiveInp = "True"
                    _Clsuser.UserId = UserLogin.UserId
                    _Clsuser.Mkt_Code = ""
                    _Clsuser.Branch = "00"
                    _Clsuser.PassGenUsr = Format(CDate(Now), "fffff")
                    _Clsuser.BirthDate = FormatDateTime(dt.Rows(i)(6).ToString, DateFormat.ShortDate)
                    _Clsuser.Gender = dt.Rows(i)(5).ToString
                    _Clsuser.ExpirateDate = FormatDateTime("31/12/2030", DateFormat.ShortDate)
                    _Clsuser.Pict = ""
                    _Clsuser.POLICYNOINP = dt.Rows(i)(2).ToString
                    _Clsuser.memberid = dt.Rows(i)(3).ToString
                    _Clsuser.UserIdInp = _Clsuser.InsertUserNewGENUSR()
                    If _Clsuser.UserIdInp <> "" Then
                        _Clsuser.InsertUserBranch("00")
                        _Clsuser.InsertMSLOGINDETAIL(Trim(_Clsuser.UserIdInp), dt.Rows(i)("POLICYNO").ToString, dt.Rows(i)("MEMBID").ToString, "", _Clsuser.RoleCodeInp)
                        If System.IO.File.Exists(strFile) = True Then

                            objWriter.WriteLine(Trim(_Clsuser.UserIdInp) & " | " & _Clsuser.UserNameInp & " | " & dt.Rows(i)("POLICYNO").ToString & " | " & dt.Rows(i)("MEMBID").ToString & " | " & _Clsuser.PassGenUsr & " | " & dt.Rows(i)("USERFIELD3").ToString & " | " & dt.Rows(i)("EMPLOYEEID").ToString, "")

                        End If
                        _Clsuser.UserIdInp = ""
                        'sw.WriteLine( _
                        '    IIf(fileExists, _Clsuser.UserIdInp & " | " & _Clsuser.UserNameInp & " | " & dt.Rows(i)(2).ToString & " | " & dt.Rows(i)(3).ToString, ""))

                    End If
                Else
                    If System.IO.File.Exists(strFile) = True Then

                        objWriter.WriteLine(Trim(dt.Rows(i)(7).ToString) & " | " & dt.Rows(i)(4).ToString & " | " & dt.Rows(i)("POLICYNO").ToString & " | " & dt.Rows(i)("MEMBID").ToString & " | " & _ClsEncryption.Decrypt(dt.Rows(i)(8).ToString) & " | " & dt.Rows(i)("USERFIELD3").ToString & " | " & dt.Rows(i)("EMPLOYEEID").ToString, "")
                        'objWriter.Close()
                        'objWriter.Dispose()
                    End If
                End If
                i = i + 1
            Loop
            objWriter.Close()
            objWriter.Dispose()
            TxtKeyWord.Text = ""

            btnDownload.Visible = True

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - GenerateUser - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
        'bindData(TxtKeyWord.Text, IIf(RBType1.SelectedValue = "New", "", RBType1.SelectedValue), "")

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

            bindData(TxtKeyWord.Text, IIf(RBType1.SelectedValue = "New", "", RBType1.SelectedValue), IIf(RBType1.SelectedValue = "New", "", RBType1.SelectedValue))
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - GenerateUser - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    'Private Sub gridMenu_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridMenu.RowCommand
    '    Try
    '        System.Threading.Thread.Sleep(500)
    '        If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then
    '            Dim KEY As String = e.CommandArgument
    '            Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
    '            Dim index As Integer = gvRow.RowIndex

    '            bindisiData(KEY)
    '            PnlMain.Visible = False
    '            pnlPopup.Visible = True
    '            txtProviderCode.ReadOnly = True
    '            txtProviderCode.ReadOnly = True
    '            txtProvidername.ReadOnly = True
    '            txtRemark.ReadOnly = True

    '        End If
    '        If e.CommandName = "UpdateLink" Then
    '            System.Threading.Thread.Sleep(500)
    '            Dim KEY As String = e.CommandArgument

    '            Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
    '            Dim index As Integer = gvRow.RowIndex
    '            Dim sts As String = DirectCast(gridMenu.Rows(index).FindControl("hfstatus"), HiddenField).Value
    '            '_ClsInventarisItem.NO = KEY
    '            '_ClsInventarisItem.STATUS = IIf(sts = 1, 0, 1)
    '            '_ClsInventarisItem.updateStatus()
    '            _sama.UpdateActive("MSProvider", "ISACTIVE", IIf(sts = "True", "False", "True"), "ProviderCode", KEY)
    '            'bindData(TxtKeyWord.Text, RBProviderType1.SelectedValue)
    '        End If
    '    Catch ex As Exception
    '        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
    '        Dim msg As String = String.Format("{0} - Provider - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
    '        WriteFile.Write(config.SetFullFilePath, msg)
    '    End Try
    'End Sub

    Protected Sub RBType1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RBType1.SelectedIndexChanged
        'If RBType1.SelectedValue = "New" Then
        '    btnAdd.Visible = True
        'Else
        '    btnAdd.Visible = False
        'End If
        gridMenu.DataSource = Nothing
        gridMenu.DataBind()
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Try
            Dim flnm = Session("flnm")
            Dim strFile As String = config.createFiletxt & flnm
            btnDownload.Visible = False
            If System.IO.File.Exists(strFile) = False Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information(' cant download anymore');</script>")
                Exit Sub
            End If

            Response.Clear()
            Response.BufferOutput = True
            Response.ContentType = "application/octet-stream"
            Dim fi As FileInfo = New FileInfo(config.createFiletxt & flnm)
            Dim fileLength As Long = fi.Length
            Response.AddHeader("Content-Length", fileLength)
            Response.AddHeader("content-disposition", "attachment; filename=" + flnm)
            Response.TransmitFile(config.createFiletxt & flnm)
            Response.Flush()
            'System.Threading.Thread.Sleep(10000)

            'Kill(Server.MapPath(config.createFiletxt & flnm))
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - GenerateUser - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub
End Class