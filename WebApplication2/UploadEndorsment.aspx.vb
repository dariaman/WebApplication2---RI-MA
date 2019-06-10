Imports SPGeneral
Imports System.IO

Imports System

Public Class UploadEndorsment
    Inherits System.Web.UI.Page

    Dim _sama As New WebService.sama
    Dim _General As WebService.general
    Dim _ClsUploadEndorsment As New WebService.ClsUploadEndorsment
    Dim ext As String
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
        If Not UserLogin Is Nothing Then
            If Not Page.IsPostBack Then
                If UserLogin.IsActive Then
                    Try
                        Session("DashBoard") = "Upload Endorsment <i class='fa fa-upload fa-fw'></i>"
                        If Session("Email") = "" Then
                            Response.Redirect("UserProfile.aspx", False)
                        End If

                        
                        'Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        'If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                        '    Response.Redirect("home.aspx", False)
                        'End If
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - UploadEndorsment - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=UploadEndorsment.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=UploadEndorsment.aspx", False)
        End If
    End Sub

    Protected Sub LinkUpload_Click(sender As Object, e As EventArgs) Handles LinkUpload.Click

        ext = UCase(System.IO.Path.GetExtension(FileUpload1.FileName))
        'Dim dir As String = config.uploadFileData & txtNoRegistration.Text & ext
        If FileUpload1.PostedFile.ContentLength > 8651000 Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('file are to big, max 8 Mb');</script>")
            Exit Sub
        End If

        _ClsUploadEndorsment.NoReg = txtNoRegistration.Text
        _ClsUploadEndorsment.DateReg = txtDateRegistration.Text
        _ClsUploadEndorsment.AddItem = txtAddItemQty.Text
        _ClsUploadEndorsment.ChangePlan = txtChangePlan.Text
        _ClsUploadEndorsment.TerminatePlan = txtTermination.Text
        _ClsUploadEndorsment.AlterationPlan = txtAlteration.Text
        _ClsUploadEndorsment.FileName = ext
        _ClsUploadEndorsment.Cre_By = UserLogin.UserId
        txtNoRegistration.Text = _ClsUploadEndorsment.InsertREGISTER()

        If txtNoRegistration.Text = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Please submit first');</script>")
            Exit Sub
        End If

        If ext = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Please upload the data then upload');</script>")
            Exit Sub
        End If

        If System.IO.File.Exists(config.uploadFileData & txtNoRegistration.Text & ext) Then
            Kill(config.uploadFileData & txtNoRegistration.Text & ext)
        End If
        FileUpload1.SaveAs(config.uploadFileData & txtNoRegistration.Text & ext)
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        PnlMain.Visible = False
        pnlPopup.Visible = True
        txtNoRegistration.Text = ""
        txtDateRegistration.Text = Format(Today, "dd/MM/yyyy")
        txtDateRegistration.ReadOnly = True
        txtNoRegistration.ReadOnly = True
        txtAddItemQty.Text = 0
        txtAlteration.Text = 0
        txtChangePlan.Text = 0
        txtTermination.Text = 0
        ext = ""
    End Sub

    Protected Sub LinkClose_Click(sender As Object, e As EventArgs) Handles LinkClose.Click
        PnlMain.Visible = True
        pnlPopup.Visible = False
        bindData(TxtKeyWord.Text)
    End Sub

    Protected Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        bindData(TxtKeyWord.Text)
    End Sub

    Protected Sub bindData(TxtKeyWord As String)
        Try
            gridMenu.DataSource = _ClsUploadEndorsment.bindData(TxtKeyWord, UserLogin.UserId)
            gridMenu.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gridMenu_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridMenu.PageIndexChanging
        Try
            System.Threading.Thread.Sleep(500)
            gridMenu.PageIndex = e.NewPageIndex
            bindData(TxtKeyWord.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UploadEndorsment - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub gridMenu_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridMenu.RowCommand
        Try
            System.Threading.Thread.Sleep(500)

            If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then

                Dim KEY As String = e.CommandArgument
                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim index As Integer = gvRow.RowIndex
                Dim flnm As String = DirectCast(gridMenu.Rows(index).FindControl("hffilename"), HiddenField).Value
                Session("isNew") = "0"
                'Dim KEY As String = e.CommandArgument
                bindisiData(KEY)
                PnlMain.Visible = False
                pnlPopup.Visible = True
                'LinkSubmit.Enabled = True
                'txtBioDataCode.ReadOnly = True
                txtNoRegistration.ReadOnly = True
                txtDateRegistration.ReadOnly = True
            End If
            If e.CommandName = "DownloadLink" Then

                Dim KEY As String = e.CommandArgument
                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim index As Integer = gvRow.RowIndex
                Dim flnm As String = DirectCast(gridMenu.Rows(index).FindControl("hffilename"), HiddenField).Value
                System.Threading.Thread.Sleep(500)
                'Response.ContentType = "Application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                Response.Clear()
                Response.BufferOutput = True
                Response.ContentType = "application/octet-stream"
                Dim fi As FileInfo = New FileInfo(config.uploadFileData & KEY & flnm)
                Dim fileLength As Long = fi.Length
                Response.AddHeader("Content-Length", fileLength)
                Response.AddHeader("content-disposition", "attachment; filename=" + KEY & flnm)
                Response.TransmitFile(config.pathFileData & KEY & flnm)
                Response.Flush()

                'Response.AppendHeader("content-deposition", "attachment/filename=\""+fileName+" \ "")
                'Response.TransmitFile(Server.MapPath(Server.MapPath(config.uploadFileData & txtNoRegistration.Text & flnm)))
                'Response.End()

            End If
            If e.CommandName = "UpdateLink" Then

                Dim KEY As String = e.CommandArgument
                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim index As Integer = gvRow.RowIndex
                Dim flnm As String = DirectCast(gridMenu.Rows(index).FindControl("hffilename"), HiddenField).Value
                System.Threading.Thread.Sleep(500)
                'Dim KEY As String = e.CommandArgument

                'Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                'Dim index As Integer = gvRow.RowIndex
                'Dim sts As String = DirectCast(gridMenu.Rows(index).FindControl("hfstatus"), HiddenField).Value

                ''Session("Avtive") = gridMenu.Rows(index).Cells(2).ToString
                ''If Session("Avtive") <> chkAktiv.Checked Then
                ''Else
                ''    _ClsInventarisItem.ONUSE = txtBioDataCode.Text
                ''    Dim dt As DataTable = _ClsInventarisItem.bindDataInventarisDetailonUSE()
                ''    If dt.Rows.Count > 0 Then
                ''        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('this biodata still use inventaris Item from depatement " & Session("UnitDepartemen") & " remove all item first, check more detail in menu inventaris room');</script>")
                ''        Session("Avtive") = gridMenu.Rows(index).Cells(2).ToString
                ''        Exit Sub
                ''    End If
                ''End If
                Dim bodymsg As String
                bodymsg = "Yth Reliance Membership, " & vbCrLf & vbCrLf
                bodymsg = bodymsg & "Berikut kami lampirkan list data yang akan di endors" & vbCrLf
                bodymsg = bodymsg & "Atas perhatiannya kami ucapkan terima kasih" & vbCrLf & vbCrLf
                bodymsg = bodymsg & "Regards," & vbCrLf & vbCrLf & vbCrLf
                bodymsg = bodymsg & Session("Username")

                Dim addemail As String = _ClsCompanyPolicy.AddEmailScalar(UserLogin.POLICYNO)
                If addemail <> "" Or addemail <> Nothing Then
                    addemail = ";" & addemail
                Else
                    addemail = ""
                End If
                _sama.SendMail("admin.cc@reliance-insurance.com", config.EmailMembership & ";" & Session("Email") & addemail, "List Endorsment (" & KEY & flnm & ")", bodymsg, config.CCEmail, "", config.pathFileData & KEY & flnm)
                '_sama.SendMail("admin.cc@reliance-insurance.com", "christian.pandu@reliance-insurance.com;" & Session("Email"), "List Endormsnet (" & KEY & flnm & ")", bodymsg, "", "", config.pathFileData & KEY & flnm)

                _sama.UpdateActive("MSREGISTER", "ISPROCCES", 1, "NoReg", KEY)

                bindData(TxtKeyWord.Text)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UploadEndorsment - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub bindisiData(Noreg As String)
        Try
            Dim dt As New DataTable
            dt = _ClsUploadEndorsment.bindisiData(Noreg)
            If dt.Rows.Count > 0 Then
                txtNoRegistration.Text = dt.Rows(0)(0).ToString
                txtDateRegistration.Text = dt.Rows(0)(1).ToString
                txtAddItemQty.Text = dt.Rows(0)(2).ToString
                txtChangePlan.Text = dt.Rows(0)(3).ToString
                txtTermination.Text = dt.Rows(0)(4).ToString
                txtAlteration.Text = dt.Rows(0)(5).ToString
                ext = dt.Rows(0)(6).ToString

                'Image1.ImageUrl = IIf(dt.Rows(0)(13).ToString <> "", config.uploadFileBioData & dt.Rows(0)(0).ToString & dt.Rows(0)(13).ToString, config.uploadFileBioData & "unknown1.png")
            Else
                txtNoRegistration.Text = ""
                txtDateRegistration.Text = ""
                txtAddItemQty.Text = ""
                txtChangePlan.Text = ""
                txtTermination.Text = ""
                txtAlteration.Text = ""
                ext = ""
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class