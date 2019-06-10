Imports System.Data.SqlClient
Imports SPGeneral
Imports System.IO

Public Class WebViewerExport
    Inherits System.Web.UI.Page

    Public Property UserLogin() As WebService.ClsUser
        Get
            Return CType(Session("Users"), WebService.ClsUser)
        End Get
        Set(ByVal value As WebService.ClsUser)
            Session("Users") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                Response.ClearContent()
                Response.ClearHeaders()
                Response.ContentType = "application/pdf"
                Response.BinaryWrite(clsReport.Bytes)
                Dim fileNameExtension As String = Session("JudulXls") & ".pdf"
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" & Replace(Replace(Session("ReportPath"), "ReportViewer/", ""), "rdlc", "") & fileNameExtension)
                Dim fs As FileStream = New FileStream("output.pdf", FileMode.Create)
                fs.Write(clsReport.Bytes, 0, clsReport.Bytes.Length)
                Response.Flush()
                Response.Close()
                Session("JudulXls") = ""
            Catch ex As Exception
                'ltrlMsg.Text = sama.showMsg("Error", "Error terjadi. Mohon kontak IT support.", 4).Show(Me)
                'ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>alert('Error terjadi. Mohon kontak IT support.');</script>")
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                Dim msg As String = String.Format("{0} - WebViewerExport - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.UserId, ex, Environment.NewLine)
                WriteFile.Write(WebService.general.SetFullFilePath, msg)
            End Try
        End If
    End Sub

End Class