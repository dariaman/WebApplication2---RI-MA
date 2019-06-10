Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.IO

Public Class UploadFile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim vTitle As String = ""
        Dim vDesc As String = ""
        Dim FilePath As String = ""

        If Not String.IsNullOrEmpty(Request.Form("title")) Then
            vTitle = Request.Form("title")

        End If
        If Not String.IsNullOrEmpty(Request.Form("description")) Then
            vDesc = Request.Form("description")
        End If

        If vTitle = config.optendors Then
            FilePath = config.uploadFileDataMobile
        ElseIf vTitle = config.optclaim Then
            FilePath = config.uploadFileDataClaimMobile
        Else
            FilePath = ""
        End If


        'Dim FilePath As String = Server.MapPath("/RI/pict/")
        Dim MyFileCollection As HttpFileCollection = Request.Files
        'Dim MyFileCollection As FileUpload = Request.Files

        If MyFileCollection.Count > 0 Then
            MyFileCollection(0).SaveAs(FilePath + vDesc)
        Else
            Response.Write("0")
        End If

        Response.Clear()
        Response.ContentType = "application/json; charset=utf-8"
        Response.Write((FilePath))

    End Sub

End Class