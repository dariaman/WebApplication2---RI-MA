Imports SPGeneral
Imports System.IO
Imports System
Imports System.Net.Mail
Imports System.Web.Mail
Public Class EmailBlastUser
    Inherits System.Web.UI.Page

    Dim _sama As New WebService.sama
    Dim _general As New WebService.general
    Dim _ClsUser As New WebService.ClsUser
    Dim _clsEncrypt As New WebService.ClsEncryption

    Public Property UserLogin() As WebService.ClsUser
        Get
            Return CType(Session("Users"), WebService.ClsUser)
        End Get
        Set(ByVal value As WebService.ClsUser)
            Session("Users") = value
        End Set
    End Property

    Public Shared pathflnmEmailXls As String, pathflnmattchEmail As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not UserLogin Is Nothing Then
            If Not Page.IsPostBack Then
                If UserLogin.IsActive Then
                    Try
                        Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                            Response.Redirect("home.aspx", False)
                        End If
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - EmailBlastUser - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=EmailBlastUser.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=EmailBlastUser.aspx", False)
        End If
    End Sub

    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        Try
            If Label1.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Attachment tidak ada!!');</script>")
                Exit Sub
            End If
            For i = 0 To gridMenu.Rows.Count - 1
                Dim dt As DataTable = _ClsUser.dataemailPass(gridMenu.Rows(i).Cells(1).Text, gridMenu.Rows(i).Cells(2).Text)
                Dim x As String = ""
                Dim xx As String = ""
                x = bodyhdr()
                For ii = 0 To dt.Rows.Count - 1
                    xx = xx & bodymsg(dt.Rows(ii)(0), dt.Rows(ii)(3), dt.Rows(ii)(2))

                Next
                x = x & xx & bodyftr()
                '_sama.SendMail("admin.cc@reliance-insurance.com", "christian.pandu@reliance-insurance.com", "Reliance Relihit User Id dan Password", x, "christian.pandu@reliance-insurance.com", "", pathflnmattchEmail)
                _sama.SendMail("admin.cc@reliance-insurance.com", gridMenu.Rows(i).Cells(4).Text, "[ Not reply ] Reliance Relihit User Id dan Password ", x, "yuli.nurwanto@reliance-insurance.com", "", pathflnmattchEmail)
                If _sama.updateTempEmail(gridMenu.Rows(i).Cells(1).Text, gridMenu.Rows(i).Cells(2).Text) = True Then

                End If
            Next
            gridMenu.DataSource = Nothing
            gridMenu.DataBind()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UploadEndorsment - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub
    Function bodyhdr() As String
        Dim x As String
        x = "Kepada Yth." & vbCrLf & _
            "Bapak/Ibu Pemegang Polis Asuransi Reliance Indonesia" & vbCrLf & _
            "Di tempat" & vbCrLf & _
            "Kami mengucapkan terima kasih atas kepercayaan yang diberikan kepada PT Asuransi Reliance Indonesia sebagai penyelenggara program asuransi kesehatan untuk karyawan Bapak dan/atau Ibu." & vbCrLf & _
            "Bersama ini disampaikan bahwa Website Reliance untuk masing-masing peserta sudah aktif dan dapat di akses pada http://www.relihc-app.com/relihit/ di halaman browser. " & vbCrLf & _
            "Terlampir User ID & Password untuk Member dan berikut User ID & Password untuk PIC. beserta manual guidelines RELIHIT." & vbCrLf & vbCrLf
        Return x
    End Function
    Function bodyftr() As String
        Dim x As String
        x = vbCrLf & vbCrLf & "Pada website ini, setiap peserta dapat melakukan upload foto dokumen klaim dengan maks klaim 2.000.000-. Untuk pertanyaan lebih lanjut dapat menghubungi layanan 24 jam hotline center Asuransi Reliance Indonesia di no telp 021- 8082 3177/toll free 0 800 1000 327 atau customer.care@reliance-insurance.com" & vbCrLf & vbCrLf & vbCrLf & _
            "Demikian informasi yang dapat kami sampaikan. Terima kasih atas perhatian dan kerjasamanya." & vbCrLf & vbCrLf & vbCrLf & vbCrLf & _
            "Asuransi Reliance Indonesia"

        Return x
    End Function
    Function bodymsg(usrid As String, nama As String, passwrd As String) As String
        Dim x As String
        x = "Nama Peserta : " & nama & vbCrLf & _
            "User id Login : " & usrid & vbCrLf & _
            "Password Login : " & _clsEncrypt.Decrypt(passwrd) & vbCrLf & vbCrLf

        Return x
    End Function

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try

            pathflnmattchEmail = config.uploadDataEmailXlsAttachment & FileUpload2.PostedFile.FileName
            If FileUpload2.PostedFile.ContentLength > 8651000 Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('file are to big, max 8 Mb');</script>")
                Exit Sub
            End If

            If System.IO.File.Exists(pathflnmattchEmail) Then
                Kill(pathflnmattchEmail)
            End If

            Label1.Text = UCase(System.IO.Path.GetFileName(pathflnmattchEmail))
            FileUpload2.SaveAs(pathflnmattchEmail)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UploadEndorsment - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub LinkUpload_Click(sender As Object, e As EventArgs) Handles LinkUpload.Click
        Try

            pathflnmEmailXls = config.uploadDataEmailXls & FileUpload1.PostedFile.FileName
            If FileUpload1.PostedFile.ContentLength > 8651000 Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('file are to big, max 8 Mb');</script>")
                Exit Sub
            End If

            If System.IO.File.Exists(pathflnmEmailXls) Then
                Kill(pathflnmEmailXls)
            End If
            FileUpload1.SaveAs(pathflnmEmailXls)
            Dim ds As New DataSet
            ds = _sama.datasetemail(pathflnmEmailXls, "INDOSAT")

            Dim i As Integer = ds.Tables(0).Rows.Count - 1
            For i = 0 To ds.Tables(0).Rows.Count - 1
                _sama.InsertTempEmail(ds.Tables(0).Rows(i)(0).ToString, ds.Tables(0).Rows(i)(1).ToString, ds.Tables(0).Rows(i)(2).ToString, ds.Tables(0).Rows(i)(3).ToString, ds.Tables(0).Rows(i)(4).ToString, ds.Tables(0).Rows(i)(5).ToString)
            Next

            Dim dt As DataTable = _ClsUser.dataTempEmail(ds.Tables(0).Rows(0)(1).ToString)
            gridMenu.DataSource = dt
            gridMenu.DataBind()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UploadEndorsment - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try

            Dim dt As DataTable = _ClsUser.dataTempEmail(TxtKeyWord.Text)
            gridMenu.DataSource = dt
            gridMenu.DataBind()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - UploadEndorsment - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub
End Class