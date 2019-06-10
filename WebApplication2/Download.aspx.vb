Imports SPGeneral
Imports System.IO
Imports System
Imports System.Net.Mail
Imports System.Web.Mail
Public Class Download
    Inherits System.Web.UI.Page

    Dim _sama As New WebService.sama
    Dim _general As New WebService.general
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
                        Session("DashBoard") = "Download <i class='fa fa-download fa-fw'></i>"
                        hyperlink1.NavigateUrl = config.FileDownloadAPK '"https://play.google.com/store/apps/details?id=com.asuransireliance.relihit"
                        hyperlink1.Text = "Download Apk"
                        If UserLogin.POLICYNO = "" Then
                            LinkPanduanPolis.Visible = False
                        Else
                            If System.IO.File.Exists(config.pathFileDataPanduanPolis & UserLogin.POLICYNO & ".pdf") Then
                                LinkPanduanPolis.Visible = True
                            Else
                                LinkPanduanPolis.Visible = False
                            End If

                            If System.IO.File.Exists(config.pathFileDataPanduanPolis & UserLogin.POLICYNO & ".zip") Then
                                LinkPanduanPolisZIP.Visible = True
                            Else
                                LinkPanduanPolisZIP.Visible = False
                            End If
                        End If

                        'Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        'If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                        '    Response.Redirect("home.aspx", False)
                        'End If
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - Home - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=home.aspx", False)
                End If
            End If


        Else
            Response.Redirect("login.aspx?p=home.aspx", False)
        End If
    End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    '    Try


    '        Dim msg As New System.Net.Mail.MailMessage
    '        With msg
    '            .To.Add("christian.pandu@reliance-insurance.com".Replace(";", ","))
    '            'If CcAddr.Trim <> "" Then
    '            '    .CC.Add(CcAddr.Replace(";", ","))
    '            'End If
    '            'If BccAddr.Trim <> "" Then
    '            '    .Bcc.Add(BccAddr.Replace(";", ","))
    '            'End If
    '            .From = New System.Net.Mail.MailAddress("admin.cc@reliance-insurance.com")
    '            'If FileAttachment.Trim <> "" Then
    '            '    .Attachments.Add(New System.Net.Mail.Attachment(FileAttachment))
    '            'End If
    '            .Subject = "HANYA PERCOBAAN:"
    '            .IsBodyHtml = False
    '            .Body = "FYI"
    '            .Priority = Net.Mail.MailPriority.High
    '        End With

    '        Dim SmtpClient As New System.Net.Mail.SmtpClient("202.46.146.211", 587)
    '        With SmtpClient
    '            .UseDefaultCredentials = False
    '            .Credentials = New System.Net.NetworkCredential("bulkmail@reliance-insurance.com", "[2741n5ur4nc3$")
    '            .DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
    '            .EnableSsl = True
    '            .Port = 587
    '            .Host = "mail.reliance-insurance.com"
    '            .Send(msg)
    '        End With
    '    Catch ex As Exception
    '        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
    '        Dim msg As String = String.Format("{0} - Home - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
    '        WriteFile.Write(config.SetFullFilePath, msg)

    '    End Try
    'End Sub

    'Protected Sub LinkAPK_Click(sender As Object, e As EventArgs) Handles LinkAPK.Click
    '    'System.Threading.Thread.Sleep(500)
    '    ''Response.ContentType = "Application/vnd.openxmlformats-officedocument.wordprocessingml.document"
    '    'Response.Clear()
    '    'Response.BufferOutput = True
    '    'Response.ContentType = "application/octet-stream"
    '    'Dim fi As FileInfo = New FileInfo(config.FileDownloadAPK & config.FileDownloadAPKName)
    '    'Dim fileLength As Long = fi.Length
    '    'Response.AddHeader("Content-Length", fileLength)
    '    'Response.AddHeader("content-disposition", "attachment; filename=" + config.FileDownloadAPKName)
    '    'Response.TransmitFile(config.FileDownloadAPK & config.FileDownloadAPKName)
    '    'Response.Flush()
    '    Try

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub LinkPanduanPolis_Click(sender As Object, e As EventArgs) Handles LinkPanduanPolis.Click
        Try
            
            '        Kill(Server.MapPath(config.uploadFileBioData & txtBioDataCode.Text & ext))
            '    End If
            System.Threading.Thread.Sleep(500)
            'Response.ContentType = "Application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            Response.Clear()
            Response.BufferOutput = True
            Response.ContentType = "application/octet-stream"
            Dim fi As FileInfo = New FileInfo(config.pathFileDataPanduanPolis & UserLogin.POLICYNO & ".pdf")
            Dim fileLength As Long = fi.Length
            Response.AddHeader("Content-Length", fileLength)
            Response.AddHeader("content-disposition", "attachment; filename=" + UserLogin.POLICYNO & ".Pdf")
            Response.TransmitFile(config.pathFileDataPanduanPolis & UserLogin.POLICYNO & ".Pdf")
            Response.Flush()

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Don't have Policy);</script>")
            Dim msg As String = String.Format("{0} - Download - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub LinkPanduanPolisZIP_Click(sender As Object, e As EventArgs) Handles LinkPanduanPolisZIP.Click
        Try

            '        Kill(Server.MapPath(config.uploadFileBioData & txtBioDataCode.Text & ext))
            '    End If
            System.Threading.Thread.Sleep(500)
            'Response.ContentType = "Application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            Response.Clear()
            Response.BufferOutput = True
            Response.ContentType = "application/octet-stream"
            Dim fi As FileInfo = New FileInfo(config.pathFileDataPanduanPolis & UserLogin.POLICYNO & ".zip")
            Dim fileLength As Long = fi.Length
            Response.AddHeader("Content-Length", fileLength)
            Response.AddHeader("content-disposition", "attachment; filename=" + UserLogin.POLICYNO & ".zip")
            Response.TransmitFile(config.pathFileDataPanduanPolis & UserLogin.POLICYNO & ".zip")
            Response.Flush()

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Don't have Policy);</script>")
            Dim msg As String = String.Format("{0} - Download - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

End Class