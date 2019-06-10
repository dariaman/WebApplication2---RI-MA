Imports SPGeneral
Imports System.IO

Imports System

Public Class GenerateTiket
    Inherits System.Web.UI.Page

    Dim _sama As New WebService.sama
    Dim _General As WebService.general

    Dim _ClsGenerateTiket As New WebService.ClsTiket
    Dim _CLSlimit As New WebService.ClsLimit
    Dim _ClsCompanyPolicy As New WebService.ClsCompanyPolicy
    Dim ext As String
    Private Shared fupict As FileUpload
    Dim _ClsPolicyMember As New WebService.ClsPolicyMember
    Dim _clsrole As New WebService.ClsRole
    Public stsfrm As Boolean
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
                        Session("DashBoard") = "Generate Tiket <i class='fa fa-upload fa-fw'></i>"
                        If Session("Email") = "" Then
                            Response.Redirect("UserProfile.aspx", False)
                        End If
                        Dim dt As DataTable = _ClsGenerateTiket.EffExpDate(UserLogin.POLICYNO, UserLogin.memberid)
                        If dt.Rows.Count < 1 Then

                        Else
                            lblExpdt.Text = Format(CDate(dt.Rows(0)(1).ToString), "dd/MM/yyyy")
                            lblEffdt.Text = Format(CDate(dt.Rows(0)(0).ToString), "dd/MM/yyyy")
                        End If
                        '_clsrole.RoleCode = Session("RoleCode")
                        'stsfrm = _clsrole.PolicyMemberAdminOpen(_clsrole.RoleCode)
                        'If (stsfrm = True And Session("rolecode") = "00005") Or (stsfrm = True And Session("rolecode") = "00007") Then

                        Dim dt1 As DataTable = _ClsPolicyMember.bindDatapolicyno("7", UserLogin.UserId, UserLogin.POLICYNO)
                        DDLMemberId.DataValueField = "MEMBID"
                        DDLMemberId.DataTextField = "FULLNAME"
                        DDLMemberId.DataSource = dt1
                        DDLMemberId.DataBind()

                        'End If

                        _CLSlimit.POLICYNO = UserLogin.POLICYNO
                        lblTtlClaim.Text = " Maximal : " & FormatNumber(IIf(_CLSlimit.LimitPolicy() = Nothing, config.DefaultLimit, _CLSlimit.LimitPolicy()), 2)

                        LinkMpeModalPopupExtender.Show()
                        'Panel1.Visible = True
                        'Panel2.Visible = False
                        'Panel3.Visible = False

                        txtNoPolis.Text = UserLogin.POLICYNO
                        'DDLMemberId.Text = UserLogin.memberid
                        txtTglBerobat.Text = Format(Today, "dd/MM/yyyy")
                        bindData("%")
                        Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                            Response.Redirect("home.aspx", False)
                        ElseIf _ClsCompanyPolicy.DISABLEClaimFotoScalar(UserLogin.POLICYNO) = True Then
                            'LinkButton1.Enabled = False
                            LinkButton1.Visible = False
                            lblNotification.Visible = True
                            lblNotification.Text = "YOU DONT HAVE AUTHORIZATION FOR CLAIM PHOTO"
                            lblNotification.ForeColor = Drawing.Color.Red
                            'ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.iNFORMATION('you dont have authorization for claim photo ');</script>")
                        End If
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - GenerateTiket - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=GenerateTiket.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=GenerateTiket.aspx", False)
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'If Replace(txtTotalClaim.Text, ".", "") > 1000000 Then
        '    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Max Nominal claim 1.000.000');</script>")
        '    Exit Sub
        'Else
        
        Dim _CLSlimit As New WebService.ClsLimit
        _CLSlimit.POLICYNO = UserLogin.POLICYNO
        Dim limitpolicy As Decimal = IIf(_CLSlimit.LimitPolicy() = Nothing, config.DefaultLimit, _CLSlimit.LimitPolicy())
        If CDec(Replace(txtTotalClaim.Text, ".", "")) > CDec(limitpolicy) Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Total claim max " & FormatNumber(limitpolicy, 0) & "');</script>")
            Exit Sub
        End If

        If DDLMemberId.SelectedItem.Text = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Pilih Member yang ingin melakukan claim');</script>")
            Exit Sub
        End If

        If CDate(txtTglBerobat.Text) > CDate(lblExpdt.Text) Or CDate(txtTglBerobat.Text) < CDate(lblEffdt.Text) Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Masukan Tanggal sesuai periode tanggal effektif');</script>")
            Exit Sub
        ElseIf txtNoPolis.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "confirm", "<script language=javascript>jqxAlert.Information('No Polis');</script>", True)
            Exit Sub
        ElseIf DDLMemberId.SelectedItem.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('No member Id');</script>")
            Exit Sub
        ElseIf Replace(txtTotalClaim.Text, ".", "") = "" Or CInt(Replace(txtTotalClaim.Text, ".", "")) = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('No total claim');</script>")
            Exit Sub
        Else
            Dim s As String = _ClsGenerateTiket.FindActive(UserLogin.UserId, "%")
            If s <> "" Or s <> Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Pastikan semua status tiket sudah di upload atau cancel');</script>")
                'lblMsg.Text = "Pastikan semua status tiket sudah di upload atau cancel"
            Else
                txtNoTiket.Text = _ClsGenerateTiket.autoinc(UserLogin.UserId, txtTglBerobat.Text, txtNoPolis.Text, DDLMemberId.SelectedValue.ToString, Replace(txtTotalClaim.Text, ".", ""))
            End If
            bindData("%")
            txtTglBerobat.Text = Format(Today, "dd/MM/yyyy")
            txtTotalClaim.Text = 0
            gridMenu.Focus()
        End If
    End Sub

    'Protected Sub LinkClose_Click(sender As Object, e As EventArgs) Handles LinkClose.Click
    '    PnlMain.Visible = True
    '    pnlPopup.Visible = False
    '    'bindData(TxtKeyWord.Text)
    'End Sub

    'Protected Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
    '    bindData(TxtKeyWord.Text)
    'End Sub

    Protected Sub bindData(notiket As String)
        Try

            gridMenu.DataSource = _ClsGenerateTiket.bindData(UserLogin.UserId, notiket)
            gridMenu.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gridMenu_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridMenu.PageIndexChanging
        Try
            System.Threading.Thread.Sleep(500)
            gridMenu.PageIndex = e.NewPageIndex
            'bindData(TxtKeyWord.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - GenerateTiket - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
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
                Session("isNew") = "0"
                txtNotiketPict.Text = e.CommandArgument
                'bindisiData(KEY)
                bindisiDataPict(KEY)
                PnlMain.Visible = False
                pnlPopup.Visible = True
                'LinkSubmit.Enabled = True
                'txtBioDataCode.ReadOnly = True
                txtNotiketPict.ReadOnly = True
                'txtDateRegistration.ReadOnly = True
            End If
            If e.CommandName = "DownloadLink" Then

                Dim KEY As String = e.CommandArgument
                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim index As Integer = gvRow.RowIndex
                Dim flnm As String = DirectCast(gridMenu.Rows(index).FindControl("hffilename"), HiddenField).Value
                System.Threading.Thread.Sleep(500)
                Response.Clear()
                Response.BufferOutput = True
                Response.ContentType = "application/octet-stream"
                Dim fi As FileInfo = New FileInfo(config.uploadFileData & KEY & flnm)
                Dim fileLength As Long = fi.Length
                Response.AddHeader("Content-Length", fileLength)
                Response.AddHeader("content-disposition", "attachment; filename=" + KEY & flnm)
                Response.TransmitFile(config.pathFileData & KEY & flnm)
                Response.Flush()


            End If
            If e.CommandName = "UpdateLink1" Then


                Dim KEY As String = e.CommandArgument
                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim index As Integer = gvRow.RowIndex
                'Dim flnm As String = DirectCast(gridMenu.Rows(index).FindControl("hfstatus1"), HiddenField).Value
                System.Threading.Thread.Sleep(500)
                Dim dt As DataTable
                dt = _ClsGenerateTiket.bindDataPict(KEY)
                Dim bodymsg As String
                Dim gender As String
                If UserLogin.Gender = "F" Then
                    gender = "Ibu"
                Else
                    gender = "Bapak"
                End If
                bodymsg = "Yth Departemen Klaim," & vbCrLf & vbCrLf
                bodymsg = bodymsg & "Bersama ini disampaikan, bahwa kami telah menerima pembatalan claim dengan No tiket : " & KEY & " dan segera kami proses permintaan pembatalan ini dalam system kami." & vbCrLf
                bodymsg = bodymsg & "Dokumen yang di lampirkan sebagai berikut : " & vbCrLf & vbCrLf
                For i = 0 To dt.Rows.Count - 1
                    bodymsg = bodymsg & " - " & dt.Rows(i)(2).ToString & "." & vbCrLf
                    If dt.Rows(i)(4).ToString <> "&nbsp;" Then
                        bodymsg = bodymsg & " Note : " & dt.Rows(i)(4).ToString & vbCrLf & vbCrLf
                    Else
                        bodymsg = bodymsg & vbCrLf
                    End If
                Next

                bodymsg = bodymsg & "Demikian kami sampaikan, atas perhatian dan kerjasamanya kami ucapkan terimakasih" & vbCrLf & vbCrLf
                bodymsg = bodymsg & "Best Regards," & vbCrLf & vbCrLf & vbCrLf
                bodymsg = bodymsg & Session("Username")
                Dim addemail As String = _ClsCompanyPolicy.AddEmailScalar(UserLogin.POLICYNO)
                If addemail <> "" Or addemail <> Nothing Then
                    addemail = ";" & addemail
                Else
                    addemail = ""
                End If
                _sama.SendMail(Session("Email") & addemail, config.EmailClaim & ";" & "claimphoto@reliance-insurance.com", "Cancel Claim Tiket (" & KEY & ")", bodymsg, "", "", "")

                _ClsGenerateTiket.Cre_By = UserLogin.UserId
                _ClsGenerateTiket.NoTiket = e.CommandArgument
                _ClsGenerateTiket.updatePict("D", 0)
                'PnlMain.Visible = True
                'pnlPopup.Visible = False
                'bindisiDataPict(txtNotiketPict.Text)
                bindData("%")
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - GenerateTiket - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub bindisiDataPict(NoTiket As String)
        Try
            Dim dt As New DataTable
            dt = _ClsGenerateTiket.bindDataPict(NoTiket)
            GridView1.DataSource = dt
            GridView1.DataBind()
            LblTtl.Text = FormatNumber(_ClsGenerateTiket.selectSize(txtNotiketPict.Text), 0) & " b"
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        LinkMpeModalPopupExtender.Hide()
    End Sub

    Protected Sub BtnSubmit_Click(sender As Object, e As EventArgs) Handles BtnSubmit.Click
        '_ClsGenerateTiket.insert()
        'Session("Email") = "christian.pandu@reliance-insurance.com"
        Try

            If _ClsGenerateTiket.selectSize(txtNotiketPict.Text) > 8651000 Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Total all file are to big, max 8 Mb');</script>")
                Exit Sub
            End If

            Dim _CLSlimit As New WebService.ClsLimit
            _CLSlimit.POLICYNO = UserLogin.POLICYNO

            If CDec(Replace(txtTotalClaim.Text, ".", "")) > CDec(_CLSlimit.LimitPolicy) Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Total all file are to big, max 8 Mb');</script>")
                Exit Sub
            End If

            Dim bodymsg As String
            Dim gender As String
            'If UserLogin.GenderLogin = "F" Then
            '    gender = "Ibu"
            'Else
            '    gender = "Bapak"
            'End If
            gender = "Bapak/Ibu"
            bodymsg = "Yth " & gender & " " & Session("Username") & "," & vbCrLf & vbCrLf
            bodymsg = bodymsg & "Bersama ini disampaikan, bahwa kami telah menerima data  klaim foto dengan No tiket : " & txtNotiketPict.Text & " dan segera kami registrasikan ke dalam sistem." & vbCrLf & vbCrLf
            bodymsg = bodymsg & "Dokumen yang di lampirkan sebagai berikut :  " & vbCrLf & vbCrLf
            For i = 0 To GridView1.Rows.Count - 1
                bodymsg = bodymsg & " - " & GridView1.Rows(i).Cells(2).Text & "." & vbCrLf
                If GridView1.Rows(i).Cells(4).Text <> "&nbsp;" Then
                    bodymsg = bodymsg & " Note : " & GridView1.Rows(i).Cells(4).Text & vbCrLf & vbCrLf
                Else
                    bodymsg = bodymsg & vbCrLf
                End If
            Next
            bodymsg = bodymsg & "Demikian kami sampaikan, atas perhatian dan kerjasamanya kami ucapkan terimakasih" & vbCrLf & vbCrLf & vbCrLf
            bodymsg = bodymsg & "Salam," & vbCrLf & vbCrLf & vbCrLf
            bodymsg = bodymsg & "Asuransi Reliance Indonesia,"
            Dim addemail As String = _ClsCompanyPolicy.AddEmailScalar(UserLogin.POLICYNO)
            If addemail <> "" Or addemail <> Nothing Then
                addemail = addemail
            Else
                addemail = ""
            End If

            Dim gvcount As Integer = GridView1.Rows.Count
            If gvcount < 1 Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Info, No pict uploaded');</script>")
                Exit Sub
            End If

            'Dim pathclaim(GridView1.Rows.Count - 1) As String
            'For index = 0 To GridView1.Rows.Count - 1
            '    Dim sts As String = DirectCast(GridView1.Rows(index).FindControl("hfstatus1"), HiddenField).Value
            '    Dim extpict As String = DirectCast(GridView1.Rows(index).FindControl("hfext"), HiddenField).Value
            '    pathclaim(index) = config.uploadFileDataClaim & sts & extpict
            'Next

            '_sama.SendMailMultiAttach("admin.cc@reliance-insurance.com", config.EmailClaim & ";" & Session("Email"), config.HeaderEmail & " Foto Claim Tiket (" & txtNotiketPict.Text & ")", bodymsg, pathclaim, config.CCEmail, "")
            _sama.SendMail("claimphoto@reliance-insurance.com", config.EmailClaim & ";" & Session("Email"), "Foto Claim Tiket (" & txtNotiketPict.Text & ")", bodymsg, addemail, "", "")


            _ClsGenerateTiket.Cre_By = UserLogin.UserId
            _ClsGenerateTiket.NoTiket = txtNotiketPict.Text
            _ClsGenerateTiket.updatePict("A", 1)
            PnlMain.Visible = True
            pnlPopup.Visible = False
            bindisiDataPict(txtNotiketPict.Text)
            bindData("%")
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - GenerateTiket - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)

        End Try
    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

        Try
            System.Threading.Thread.Sleep(500)

            PnlMain.Visible = True
            pnlPopup.Visible = False
            bindisiDataPict(txtNotiketPict.Text)
            bindData("%")
        Catch ex As Exception

            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - GenerateTiket - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "DeletePict" Then
            System.Threading.Thread.Sleep(500)
            Dim KEY As String = e.CommandArgument

            Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            Dim index As Integer = gvRow.RowIndex
            Dim sts As String = DirectCast(GridView1.Rows(index).FindControl("hfstatus1"), HiddenField).Value
            Dim extpict As String = DirectCast(GridView1.Rows(index).FindControl("hfext"), HiddenField).Value

            If _ClsGenerateTiket.delete(txtNotiketPict.Text, sts, UserLogin.UserId) = True Then
                If System.IO.File.Exists(config.uploadFileDataClaim & sts & extpict) Then
                    Kill(config.uploadFileDataClaim & sts & extpict)
                End If

            End If
            bindisiDataPict(txtNotiketPict.Text)
        End If
        If e.CommandName = "SelectLink" Then
            System.Threading.Thread.Sleep(500)
            Dim KEY As String = e.CommandArgument

            Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            Dim index As Integer = gvRow.RowIndex
            Dim sts As String = DirectCast(GridView1.Rows(index).FindControl("hfstatus1"), HiddenField).Value
            Dim extpict As String = DirectCast(GridView1.Rows(index).FindControl("hfext"), HiddenField).Value
            GridView1.Visible = False
            ImgPict.ImageUrl = config.PictClaimPath & sts & extpict
            LblJudul.Text = DirectCast(GridView1.Rows(index).FindControl("hfstatus1"), HiddenField).Value
            LinkMpeModalPopupExtender1.Show()
            'Panel2.Visible = True
        End If
    End Sub

    Private Sub LinkUpload_Click(sender As Object, e As EventArgs) Handles LinkUpload.Click
        Try

            If (FileUploadClaim.HasFile) Then
                If FileUploadClaim.PostedFile.ContentLength > 8651000 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('file are to big, max 8 Mb');</script>")
                    Exit Sub
                End If
            End If
            'Dim dir As String = config.uploadFileDataClaim & txtNotiketPict.Text & ext

            If FileUploadClaim.PostedFile.ContentLength > 8651000 Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('file are to big, max 8 Mb');</script>")
                Exit Sub
            End If
            ext = UCase(System.IO.Path.GetExtension(FileUploadClaim.FileName))
            If ext.ToLower() <> "" And (ext.ToLower() = ".gif" Or ext.ToLower() = ".png" Or ext.ToLower() = ".jpeg" Or ext.ToLower() = ".jpg" Or ext.ToLower() = ".bmp") Then

                fupict = FileUploadClaim
                txtNote.Text = ""
                ModalPopupExtender2.Show()
                'Panel3.Visible = True
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('please choose a pict jpg, jpeg, gif, png,bmp');</script>")
                Exit Sub
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - GenerateTiket - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub BtnConfirmPict_Click(sender As Object, e As EventArgs) Handles BtnConfirmPict.Click
        Try
            ext = UCase(System.IO.Path.GetExtension(fupict.FileName))

            _ClsGenerateTiket.NoTiket = txtNotiketPict.Text
            _ClsGenerateTiket.ext = ext
            _ClsGenerateTiket.Cre_By = UserLogin.UserId
            _ClsGenerateTiket.OriFile = fupict.FileName ' Session("OriFile") 'FileUploadClaim.FileName
            _ClsGenerateTiket.Note = txtNote.Text
            _ClsGenerateTiket.Size = fupict.PostedFile.ContentLength
            Dim pict As String = _ClsGenerateTiket.insertPict()
            If pict <> "" Then

                fupict.SaveAs(config.uploadFileDataClaim & pict & ext)

            End If

            bindisiDataPict(txtNotiketPict.Text)

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - GenerateTiket - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Response.Redirect("home.aspx")
    End Sub

    Private Sub LinkButton3_Click(sender As Object, e As EventArgs) Handles LinkButton3.Click
        GridView1.Visible = True
    End Sub

    Protected Sub gridMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridMenu.SelectedIndexChanged

    End Sub
End Class