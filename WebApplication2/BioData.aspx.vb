Imports SPGeneral
Imports System.IO

Public Class BioData
    Inherits System.Web.UI.Page

    Dim _Clsusers As New WebService.ClsUser
    Dim _ClsEncryption As New WebService.ClsEncryption
    Dim _ClsBioData As New WebService.ClsBioData
    'Dim _ClsProduk As New WebService.ClsProduct
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
                If UserLogin.IsActive Then
                    Try
                        Session("DashBoard") = "BioData List <i class='fa fa-group fa-fw'></i>"
                        Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                        _sama.isiddlMSSalutation(ddlSalutation)

                        _sama.isiddlBranch(ddlBranch)
                        ddlBranch.Items.RemoveAt(1)
                        doreset()
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - BioData - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=BioData.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=BioData.aspx", False)
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click, LinkSubmit.Click
        Try
            System.Threading.Thread.Sleep(500)
            If Session("isNew") = "1" Then
                If _Clsusers.LoadUseremail(txtBioDataEmail.Text) = True Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Email " & txtBioDataEmail.Text & " is already created. Please use other email');</script>")
                    Exit Sub
                Else
                End If
            Else
                If _Clsusers.LoadUseremail(txtBioDataEmail.Text) = True Then
                    If txtBioDataCode.Text <> _Clsusers.UserIdEditEmail Then
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Email " & txtBioDataEmail.Text & " is already created. Please use other email');</script>")
                        Exit Sub
                    End If

                End If
            End If
            If validasi() = True Then
                _ClsBioData.BioDataCode = txtBioDataCode.Text
                _ClsBioData.BioDataSalutation = ddlSalutation.SelectedValue
                _ClsBioData.BioDataName = txtBioDataname.Text
                _ClsBioData.BioDataNickName = txtBioDataNickname.Text
                _ClsBioData.BioDataAdd = txtBioDataAdd.Text
                _ClsBioData.BioDataCity = txtBioDataCity.Text
                _ClsBioData.BioDataZIP = txtBioDataZIP.Text
                _ClsBioData.BioDataPhone = txtBioDataPhone.Text
                _ClsBioData.BioDataFax = txtBioDataFax.Text
                _ClsBioData.BioDataContact = txtBioDataContact.Text
                _ClsBioData.IsActive = chkAktiv.Checked
                _ClsBioData.CRE_BY = UserLogin.UserId
                _ClsBioData.BioDataGender = RbGender.Text
                _ClsBioData.BioDataBirthdate = txtBioDataBirthDate.Text
                _ClsBioData.BioDataEmail = txtBioDataEmail.Text
                _ClsBioData.BioDataPict = Session("Pict")
                _ClsBioData.BioDataType = "U" 'RbType.SelectedValue
                _ClsBioData.BranchCode = ddlBranch.SelectedValue

                Dim dt As DataTable = _ClsBioData.InsertBioData()
                If dt.Rows(0).Item(0).ToString <> "" Then
                    txtBioDataCode.Text = dt.Rows(0).Item(0).ToString
                    If _Clsusers.LvlAdmin <> 1 Then
                        Session("UserName") = txtBioDataname.Text
                        Session("Email") = txtBioDataEmail.Text
                    End If
                    LinkSubmit.Enabled = False
                    bindData(TxtKeyWord.Text)
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data saved!');</script>")
                End If
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
            Dim msg As String = String.Format("{0} - BioData - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Public Function validasi() As Boolean
        If txtBioDataname.Text = "" Or txtBioDataAdd.Text = "" Or _
            txtBioDataCity.Text = "" Or txtBioDataContact.Text = "" Or txtBioDataZIP.Text = "" _
            Or txtBioDataPhone.Text = "" Or txtBioDataFax.Text = "" Or txtBioDataEmail.Text = "" _
            Or txtBioDataEmail.Text = "" Or txtBioDataBirthDate.Text = "" Or txtBioDataContact.Text = "" Or ddlBranch.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('All Fields cannot empty!');</script>")
            Return False
        Else
            Return True
        End If
        Return True
    End Function

    Sub doreset()
        txtBioDataCode.Text = ""
        txtBioDataNickname.Text = ""
        txtBioDataname.Text = ""
        txtBioDataAdd.Text = ""
        txtBioDataCity.Text = ""
        txtBioDataZIP.Text = ""
        txtBioDataPhone.Text = ""
        txtBioDataFax.Text = ""
        txtBioDataContact.Text = ""
        ddlSalutation.SelectedIndex = 0
        ddlBranch.SelectedIndex = 0
        'RbType.ClearSelection()
        RbGender.ClearSelection()
        txtBioDataEmail.Text = ""
        txtBioDataBirthDate.Text = ""
        'RbType.Items(2).Enabled = False
        chkAktiv.Checked = True
        Image1.ImageUrl = config.uploadFileBioData & "unknown1.png"
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            gridMenu.PageIndex = 0
            bindData(TxtKeyWord.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - BioData - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Protected Sub bindData(kd_BioData As String)
        Try
            gridMenu.DataSource = _ClsBioData.bindData(kd_BioData, ddlSearch.SelectedValue)
            gridMenu.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub bindisiData(kd_BioData As String)
        Try
            Dim dt As New DataTable
            dt = _ClsBioData.bindisiData(kd_BioData)
            If dt.Rows.Count > 0 Then
                txtBioDataCode.Text = dt.Rows(0)(0).ToString
                txtBioDataNickname.Text = dt.Rows(0)(1).ToString
                ddlSalutation.SelectedValue = IIf(dt.Rows(0)(2).ToString = "", "Mr", dt.Rows(0)(2).ToString)
                txtBioDataname.Text = dt.Rows(0)(3).ToString
                txtBioDataAdd.Text = dt.Rows(0)(4).ToString
                txtBioDataCity.Text = dt.Rows(0)(5).ToString
                txtBioDataZIP.Text = dt.Rows(0)(6).ToString
                txtBioDataPhone.Text = dt.Rows(0)(7).ToString
                txtBioDataFax.Text = dt.Rows(0)(8).ToString
                txtBioDataContact.Text = dt.Rows(0)(9).ToString
                txtBioDataBirthDate.Text = Format(CDate(dt.Rows(0)(10).ToString), "dd/MM/yyyy")
                RbGender.SelectedValue = dt.Rows(0)(11).ToString
                txtBioDataEmail.Text = dt.Rows(0)(12).ToString
                'If dt.Rows(0)(14).ToString = "" Then
                '    RbType.ClearSelection()
                'Else
                '    RbType.SelectedValue = dt.Rows(0)(14).ToString
                '    If dt.Rows(0)(14).ToString = "U" Then
                '        RbType.Enabled = False
                '    Else
                '        RbType.Enabled = True
                '    End If
                'End If
                ddlBranch.SelectedValue = dt.Rows(0)(15).ToString
                chkAktiv.Checked = dt.Rows(0)(16).ToString
                If Session("Pict") <> "" Then
                    If File.Exists(Server.MapPath(IIf(dt.Rows(0)(13).ToString <> "", config.uploadFileBioData & dt.Rows(0)(0).ToString & dt.Rows(0)(13).ToString, config.uploadFileBioData & "unknown1.png"))) Then
                        Image1.ImageUrl = IIf(dt.Rows(0)(13).ToString <> "", config.uploadFileBioData & dt.Rows(0)(0).ToString & dt.Rows(0)(13).ToString, config.uploadFileBioData & "unknown1.png")
                    Else
                        Image1.ImageUrl = config.uploadFile & "unknown1.png"
                    End If
                    'If System.IO.File.Exists(Server.MapPath(config.uploadFile & UserLogin.UserId & Session("Pict"))) Then

                Else
                    Image1.ImageUrl = config.uploadFile & "unknown1.png"
                End If
                'Image1.ImageUrl = IIf(dt.Rows(0)(13).ToString <> "", config.uploadFileBioData & dt.Rows(0)(0).ToString & dt.Rows(0)(13).ToString, config.uploadFileBioData & "unknown1.png")
            Else
                txtBioDataCode.Text = ""
                txtBioDataNickname.Text = ""
                txtBioDataname.Text = ""
                txtBioDataAdd.Text = ""
                txtBioDataCity.Text = ""
                txtBioDataZIP.Text = ""
                txtBioDataPhone.Text = ""
                txtBioDataFax.Text = ""
                txtBioDataContact.Text = ""

                txtBioDataBirthDate.Text = ""
                RbGender.SelectedValue = ""
                txtBioDataEmail.Text = ""
                chkAktiv.Checked = False
                ddlBranch.SelectedIndex = 0
                Image1.ImageUrl = config.uploadFileBioData & "unknown1.png"
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        System.Threading.Thread.Sleep(500)
        doreset()
        PnlMain.Visible = False
        pnlPopup.Visible = True
        Session("isNew") = "1"
        LinkSubmit.Enabled = True
        txtBioDataCode.ReadOnly = True
        'RbType.Enabled = True

    End Sub

    Protected Sub LinkClose4_Click(sender As Object, e As EventArgs) Handles LinkClose.Click
        System.Threading.Thread.Sleep(500)
        PnlMain.Visible = True
        pnlPopup.Visible = False
    End Sub

    Private Sub gridMenu_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridMenu.PageIndexChanging
        Try
            System.Threading.Thread.Sleep(500)
            gridMenu.PageIndex = e.NewPageIndex
            bindData(TxtKeyWord.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - BioData - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub gridMenu_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridMenu.RowCommand
        Try
            System.Threading.Thread.Sleep(500)
            If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then
                Session("isNew") = "0"
                Dim KEY As String = e.CommandArgument
                bindisiData(KEY)
                PnlMain.Visible = False
                pnlPopup.Visible = True
                LinkSubmit.Enabled = True
                txtBioDataCode.ReadOnly = True
            End If
            If e.CommandName = "UpdateLink" Then
                System.Threading.Thread.Sleep(500)
                Dim KEY As String = e.CommandArgument

                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim index As Integer = gvRow.RowIndex
                Dim sts As String = DirectCast(gridMenu.Rows(index).FindControl("hfstatus"), HiddenField).Value

                'Session("Avtive") = gridMenu.Rows(index).Cells(2).ToString
                'If Session("Avtive") <> chkAktiv.Checked Then
                'Else
                '    _ClsInventarisItem.ONUSE = txtBioDataCode.Text
                '    Dim dt As DataTable = _ClsInventarisItem.bindDataInventarisDetailonUSE()
                '    If dt.Rows.Count > 0 Then
                '        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('this biodata still use inventaris Item from depatement " & Session("UnitDepartemen") & " remove all item first, check more detail in menu inventaris room');</script>")
                '        Session("Avtive") = gridMenu.Rows(index).Cells(2).ToString
                '        Exit Sub
                '    End If
                'End If
                _sama.UpdateActive("MSBiodata", "ISACTIVE", IIf(sts = "True", "False", "True"), "BiodataCode", KEY)
                bindData(TxtKeyWord.Text)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - BioData - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub LinkUpload_Click(sender As Object, e As EventArgs) Handles LinkUpload.Click
        System.Threading.Thread.Sleep(500)
        If txtBioDataCode.Text = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Please save data first then upload the picture');</script>")
            Exit Sub
        End If
        If FileUpload1.PostedFile.ContentLength > 100000 Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('file are to big');</script>")
            Exit Sub
        End If

        ext = UCase(System.IO.Path.GetExtension(FileUpload1.FileName))
        If System.IO.File.Exists(Server.MapPath(config.uploadFileBioData & txtBioDataCode.Text & ext)) Then
            Kill(Server.MapPath(config.uploadFileBioData & txtBioDataCode.Text & ext))
        End If

        If ext = ".JPG" Or ext = ".BMP" Or ext = ".GIF" Or ext = ".PNG" Or ext = ".JPG" Or ext = ".JPEG" Then
            FileUpload1.SaveAs(Server.MapPath(config.uploadFileBioData & txtBioDataCode.Text & ext))
            Session("Pict") = ext
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('file not pict format');</script>")

        End If
        Image1.ImageUrl = config.uploadFileBioData & txtBioDataCode.Text & ext
    End Sub

End Class