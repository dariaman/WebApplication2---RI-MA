Imports SPGeneral
Public Class Company
    Inherits System.Web.UI.Page
    Dim _ClsCompany As New WebService.ClsCompany
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
                        Session("DashBoard") = "Company List<i class='fa fa-dropbox fa-fw'></i>"
                        Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!," & ex.Message.ToString & "');</script>")
                        Dim msg As String = String.Format("{0} - Company - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=Company.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=Company.aspx", False)
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click, LinkSubmit.Click
        Try
            ext = UCase(System.IO.Path.GetExtension(FileUpload1.FileName))
            Session("ext") = ext
            If validasi() = True Then
                _ClsCompany.CompanyId = txtCompanyId.Text
                _ClsCompany.CompanyName = txtCompanyName.Text
                _ClsCompany.CompanyAddress = txtCompanyAddress.Text
                _ClsCompany.CompanyPict = txtCompanyId.Text & Session("ext")
                _ClsCompany.IsActive = chkAktiv.Checked
                _ClsCompany.CRE_BY = UserLogin.UserId
                Dim iresult As String = _ClsCompany.InsertCompany()
                If iresult <> "" Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data saved! id : " & iresult & "');</script>")
                    LinkSubmit.Enabled = False
                    bindData(txtKeyWord1.Text)
                    txtCompanyId.Text = iresult

                    System.Threading.Thread.Sleep(500)
                    If txtCompanyId.Text = "" Then
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Please save data first then upload the picture');</script>")
                        Exit Sub
                    End If
                    If FileUpload1.PostedFile.ContentLength > 1100000 Then
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('file are to big');</script>")
                        Exit Sub
                    End If

                    ext = UCase(System.IO.Path.GetExtension(FileUpload1.FileName))
                    Session("ext") = ext
                    If System.IO.File.Exists(config.uploadFilePictCompany & txtCompanyId.Text & ext) Then
                        Kill(config.uploadFilePictCompany & txtCompanyId.Text & ext)
                    End If

                    If ext = ".JPG" Or ext = ".BMP" Or ext = ".GIF" Or ext = ".PNG" Or ext = ".JPG" Or ext = ".JPEG" Then
                        FileUpload1.SaveAs(config.uploadFilePictCompany & txtCompanyId.Text & ext)
                    Else
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('file not pict format');</script>")

                    End If

                    Image1.ImageUrl = config.PictCompanyPath & txtCompanyId.Text & Session("ext")


                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Please Cek data!');</script>")

                End If
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!," & ex.Message.ToString & "');</script>")
            Dim msg As String = String.Format("{0} - Company - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Public Function validasi() As Boolean
        If txtCompanyName.Text = "" Or txtCompanyAddress.Text = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('All Fields cannot empty!');</script>")
            Return False
        Else
            Return True
        End If
        Return True
    End Function

    Sub doreset()
        ddlSearch.SelectedIndex = 0
        txtCompanyId.Text = ""
        txtCompanyName.Text = ""
        txtCompanyAddress.Text = ""
        chkAktiv.Checked = True
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            bindData(txtKeyWord1.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!," & ex.Message.ToString & "');</script>")
            Dim msg As String = String.Format("{0} - Company - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Protected Sub bindData(Company As String)
        Try
            gridMenu.DataSource = _ClsCompany.bindData(Company, ddlSearch.SelectedValue)
            gridMenu.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub bindisiData(ID As String)
        Try
            Dim dt As DataTable = _ClsCompany.bindisiData(ID)
            If dt.Rows.Count > 0 Then
                PnlMain.Visible = False
                pnlPopup.Visible = True
                txtCompanyId.Text = dt.Rows(0)(0).ToString
                txtCompanyName.Text = dt.Rows(0)(1).ToString
                txtCompanyAddress.Text = dt.Rows(0)(2).ToString
                ext = UCase(System.IO.Path.GetExtension(config.uploadFilePictCompany & dt.Rows(0)(3).ToString))
                Session("ext") = ext
                chkAktiv.Checked = dt.Rows(0)(4).ToString
            Else
                doreset()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gridMenu_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles gridMenu.PageIndexChanged
        Try
            gridMenu.PageIndex = e.NewPageIndex
            bindData(txtKeyWord1.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!," & ex.Message.ToString & "');</script>")
            Dim msg As String = String.Format("{0} - Company - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        doreset()
        PnlMain.Visible = False
        pnlPopup.Visible = True
        'txtCompanyId.ReadOnly = False
    End Sub

    Protected Sub LinkClose4_Click(sender As Object, e As EventArgs) Handles LinkClose.Click
        PnlMain.Visible = True
        pnlPopup.Visible = False
        LinkSubmit.Enabled = True
        Image1.ImageUrl = Nothing
    End Sub

    Private Sub DGuser_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridMenu.RowCommand
        Try
            If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then
                Dim KEY As String = e.CommandArgument
                bindisiData(KEY)
                txtCompanyId.ReadOnly = True
                Image1.ImageUrl = config.PictCompanyPath & txtCompanyId.Text & ext
            End If

            If e.CommandName = "UpdateLink" Then
                System.Threading.Thread.Sleep(500)
                Dim KEY As String = e.CommandArgument

                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim index As Integer = gvRow.RowIndex
                Dim sts As String = DirectCast(gridMenu.Rows(index).FindControl("hfstatus"), HiddenField).Value
                '_ClsInventarisItem.NO = KEY
                '_ClsInventarisItem.STATUS = IIf(sts = 1, 0, 1)
                '_ClsInventarisItem.updateStatus()
                _sama.UpdateActive("MSCompany", "ISACTIVE", IIf(sts = "True", "False", "True"), "CompanyId", KEY)
                bindData(txtKeyWord1.Text)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!," & ex.Message.ToString & "');</script>")
            Dim msg As String = String.Format("{0} - Company - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub
    Dim ext As String

    Protected Sub LinkUpload_Click(sender As Object, e As EventArgs) Handles LinkUpload.Click
        System.Threading.Thread.Sleep(500)
        If txtCompanyId.Text = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Please save data first then upload the picture');</script>")
            Exit Sub
        End If
        If FileUpload1.PostedFile.ContentLength > 1100000 Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('file are to big');</script>")
            Exit Sub
        End If

        ext = UCase(System.IO.Path.GetExtension(FileUpload1.FileName))
        Session("ext") = ext
        If System.IO.File.Exists(config.uploadFilePictCompany & txtCompanyId.Text & ext) Then
            Kill(config.uploadFilePictCompany & txtCompanyId.Text & ext)
        End If

        If ext = ".JPG" Or ext = ".BMP" Or ext = ".GIF" Or ext = ".PNG" Or ext = ".JPG" Or ext = ".JPEG" Then
            FileUpload1.SaveAs(config.uploadFilePictCompany & txtCompanyId.Text & ext)
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('file not pict format');</script>")

        End If

        Image1.ImageUrl = config.PictCompanyPath & txtCompanyId.Text & Session("ext")
    End Sub

End Class