Imports System.Data.SqlClient
Imports SPGeneral

Public Class WebViewerExportExcel
    Inherits System.Web.UI.Page

    Public Property UserLogin() As WebService.ClsUser
        Get
            Return CType(Session("Users"), WebService.ClsUser)
        End Get
        Set(ByVal value As WebService.ClsUser)
            Session("Users") = value
        End Set
    End Property

    'Dim Bytes As Byte()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                Response.ClearContent()
                Response.ClearHeaders()
                Response.ContentType = "application/excel"
                Response.BinaryWrite(clsReport.Bytes)
                Dim fileNameExtension As String = Session("JudulXls") & ".xls"
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" & Replace(Replace(Session("ReportPath"), "ReportViewer/", ""), "rdlc", "") & fileNameExtension)
                Response.Flush()
                Response.Close()
            Catch ex As Exception
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error terjadi. Mohon kontak IT support.');</script>")
                'ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>alert('Error terjadi. Mohon kontak IT support.');</script>")
                Dim msg As String = String.Format("{0} - WebViewerExportExcel - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                WriteFile.Write(config.SetFullFilePath, msg)
            End Try
        End If

    End Sub

End Class