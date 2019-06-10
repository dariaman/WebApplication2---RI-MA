Imports SPGeneral
Imports System.IO

Public Class UserDetail1
    Inherits System.Web.UI.Page

    Dim _Clsusers As New WebService.ClsUser
    Dim _ClsBioData As New WebService.ClsBioData
    Dim _Clsuser As New WebService.ClsUser
    Dim _ClsEncryption As New WebService.ClsEncryption
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
                            If Filename <> "UserDetail1.aspx" Then
                                Response.Redirect("Home.aspx", False)
                            End If
                        End If
                        doreset()
                        _sama.isiddlMSSalutation(ddlSalutation)
                        bindisiData(UserLogin.UserId)
                        txtBioDataCode.Text = UserLogin.UserId
                        txtBioDataname.Text = Session("UserName")
                        txtBioDataEmail.Text = Session("Email")
                        chkAktiv.Checked = UserLogin.IsActive

                        '_sama.isiddlBranch(ddlBranch)
                        'ddlBranch.Items.RemoveAt(1)
                        'ddlBranch.SelectedIndex = ddlBranch.Items.Count - 1
                        'ddlBranch.SelectedValue = ddlBranch.Items(1).Value
                        'Session("isNew") = "0"

                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - USerDetail1 - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=USerDetail1.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=USerDetail1.aspx", False)
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click, LinkSubmit.Click
        Try
            
            If validasi() = True Then
                'If Session("isNew") = "1" Then
                '    If _Clsusers.LoadUseremail(txtBioDataEmail.Text) = True Then
                '        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Email " & txtBioDataEmail.Text & " is already created. Please use other email');</script>")
                '        Exit Sub
                '    Else
                '    End If
                'Else
                '    If _Clsusers.LoadUseremail(txtBioDataEmail.Text) = True Then
                '        If txtBioDataCode.Text <> _Clsusers.UserIdEditEmail Then
                '            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Email " & txtBioDataEmail.Text & " is already created. Please use other email');</script>")
                '            Exit Sub
                '        End If

                '    End If
                'End If

                _ClsBioData.BioDataCode = txtBioDataCode.Text
                '_ClsBioData.BioDataSalutation = ddlSalutation.SelectedValue
                '_ClsBioData.BioDataName = txtBioDataname.Text
                '_ClsBioData.BioDataNickName = txtBioDataNickname.Text
                '_ClsBioData.BioDataAdd = txtBioDataAdd.Text
                '_ClsBioData.BioDataCity = txtBioDataCity.Text
                '_ClsBioData.BioDataZIP = txtBioDataZIP.Text
                '_ClsBioData.BioDataPhone = txtBioDataPhone.Text
                '_ClsBioData.BioDataFax = txtBioDataFax.Text
                '_ClsBioData.IsActive = chkAktiv.Checked
                '_ClsBioData.BioDataGender = RbGender.Text
                '_ClsBioData.BioDataEmail = txtBioDataEmail.Text
                '_ClsBioData.BioDataBirthdate = txtBioDataBirthDate.Text
                '_ClsBioData.BioDataContact = txtBioDataContact.Text
                '_ClsBioData.CRE_BY = UserLogin.UserId
                '_ClsBioData.BioDataPict = Session("Pict")
                '_ClsBioData.BioDataType = "U"
                '_ClsBioData.BranchCode = ddlBranch.SelectedValue


                _ClsBioData.BioDataSalutation = ddlSalutation.SelectedValue
                _ClsBioData.BioDataName = txtBioDataname.Text
                _ClsBioData.BioDataNickName = "" 'txtBioDataNickname.Text
                _ClsBioData.BioDataAdd = "" 'txtBioDataAdd.Text
                _ClsBioData.BioDataCity = "" 'txtBioDataCity.Text
                _ClsBioData.BioDataZIP = "" 'txtBioDataZIP.Text
                _ClsBioData.BioDataPhone = "" 'txtBioDataPhone.Text
                _ClsBioData.BioDataFax = "" 'txtBioDataFax.Text
                _ClsBioData.IsActive = chkAktiv.Checked
                _ClsBioData.BioDataGender = RbGender.Text
                _ClsBioData.BioDataEmail = txtBioDataEmail.Text
                _ClsBioData.BioDataBirthdate = txtBioDataBirthDate.Text
                _ClsBioData.BioDataContact = "" 'txtBioDataContact.Text
                _ClsBioData.CRE_BY = UserLogin.UserId
                _ClsBioData.BioDataPict = Session("Pict")
                _ClsBioData.BioDataType = "U"
                _ClsBioData.BranchCode = "00" 'ddlBranch.SelectedValue

                Dim dt As DataTable = _ClsBioData.InsertBioData()
                If dt.Rows(0).Item(0).ToString <> "" Then

                    txtBioDataCode.Text = dt.Rows(0).Item(0).ToString
                    If _Clsusers.LvlAdmin <> 1 Then
                        Session("UserName") = txtBioDataname.Text
                        Session("Email") = txtBioDataEmail.Text
                    End If
                    LinkSubmit.Enabled = False
                    'ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data saved!');</script>")
                    Response.Redirect("userProfile.aspx")
                End If
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - USerDetail1 - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Public Function validasi() As Boolean
        If txtBioDataCode.Text = "" Or txtBioDataname.Text = "" Or txtBioDataEmail.Text = "" Or txtBioDataEmail.Text = "" Or txtBioDataBirthDate.Text = "" Then
            'txtBioDataCity.Text = "" Or txtBioDataContact.Text = "" Or txtBioDataZIP.Text = "" _'txtBioDataAdd.Text = ""
            'Or txtBioDataPhone.Text = "" Or txtBioDataFax.Text = "" Or txtBioDataContact.Text = "" Or ddlBranch.SelectedIndex = 0 _

            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('All Fields cannot empty!');</script>")
            Return False
        Else
            Return True
        End If
        Return True
    End Function

    Sub doreset()
        txtBioDataCode.Text = ""
        'txtBioDataNickname.Text = ""
        txtBioDataname.Text = ""
        'txtBioDataAdd.Text = ""
        'txtBioDataCity.Text = ""
        'txtBioDataZIP.Text = ""
        'txtBioDataPhone.Text = ""
        'txtBioDataFax.Text = ""
        'txtBioDataContact.Text = ""
        'ddlSalutation.SelectedIndex = 0
        'ddlBranch.SelectedIndex = 0
        chkAktiv.Checked = False
        'Image1.ImageUrl = config.uploadFileBioData & "unknown1.png"
    End Sub

    Protected Sub bindisiData(kd_BioData As String)
        Try
            Dim dt As New DataTable
            dt = _ClsBioData.bindisiData(kd_BioData)
            If dt.Rows.Count > 0 Then
                txtBioDataCode.Text = dt.Rows(0)(0).ToString
                'txtBioDataNickname.Text = dt.Rows(0)(1).ToString
                ddlSalutation.SelectedValue = IIf(dt.Rows(0)(11).ToString = "M", "Mr", "Mrs")
                'txtBioDataname.Text = dt.Rows(0)(3).ToString
                'txtBioDataAdd.Text = dt.Rows(0)(4).ToString
                'txtBioDataCity.Text = dt.Rows(0)(5).ToString
                'txtBioDataZIP.Text = dt.Rows(0)(6).ToString
                'txtBioDataPhone.Text = dt.Rows(0)(7).ToString
                'txtBioDataFax.Text = dt.Rows(0)(8).ToString
                'txtBioDataContact.Text = dt.Rows(0)(9).ToString
                txtBioDataBirthDate.Text = Format(CDate(dt.Rows(0)(10).ToString), "dd/MM/yyyy")
                RbGender.SelectedValue = dt.Rows(0)(11).ToString
                'txtBioDataEmail.Text = dt.Rows(0)(12).ToString

                'ddlBranch.SelectedValue = dt.Rows(0)(15).ToString
                'chkAktiv.Checked = dt.Rows(0)(16).ToString
                'Image1.ImageUrl = Session("Pictadd")

                
            Else
                txtBioDataCode.Text = ""
                'txtBioDataNickname.Text = ""
                'txtBioDataname.Text = ""
                'txtBioDataAdd.Text = ""
                'txtBioDataCity.Text = ""
                'txtBioDataZIP.Text = ""
                'txtBioDataPhone.Text = ""
                'txtBioDataFax.Text = ""
                'txtBioDataContact.Text = ""
                'ddlBranch.SelectedIndex = 0
                ddlSalutation.SelectedValue = 0
                'txtBioDataBirthDate.Text = ""
                RbGender.SelectedValue = ""
                'txtBioDataEmail.Text = ""
                chkAktiv.Checked = False
                'ddlBranch.SelectedIndex = 0
                'Image1.ImageUrl = Session("Pictadd")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    'Protected Sub LinkUpload_Click(sender As Object, e As EventArgs) Handles LinkUpload.Click
    '    If txtBioDataCode.Text = "" Then
    '        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Please save data first then upload the picture');</script>")
    '        Exit Sub
    '    End If
    '    If FileUpload1.PostedFile.ContentLength > 100000 Then
    '        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('file are to big');</script>")
    '        Exit Sub
    '    End If

    '    ext = UCase(System.IO.Path.GetExtension(FileUpload1.FileName))
    '    If System.IO.File.Exists(Server.MapPath(config.uploadFileBioData & txtBioDataCode.Text & ext)) Then
    '        Kill(Server.MapPath(config.uploadFileBioData & txtBioDataCode.Text & ext))
    '    End If

    '    If ext = ".JPG" Or ext = ".BMP" Or ext = ".GIF" Or ext = ".PNG" Or ext = ".JPG" Or ext = ".JPEG" Then
    '        FileUpload1.SaveAs(Server.MapPath(config.uploadFileBioData & txtBioDataCode.Text & ext))
    '        Session("Pict") = ext
    '    Else
    '        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('file not pict format');</script>")

    '    End If

    '    Image1.ImageUrl = config.uploadFileBioData & txtBioDataCode.Text & ext
    'End Sub

    Protected Sub LinkClose_Click(sender As Object, e As EventArgs) Handles LinkClose.Click
        Response.Redirect("userProfile.aspx")
    End Sub
End Class