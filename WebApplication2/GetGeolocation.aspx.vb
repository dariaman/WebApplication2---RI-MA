Imports System.Data.SqlClient
Imports SPGeneral
Imports Microsoft.Reporting.WebForms
Public Class GetGeolocation
    Inherits System.Web.UI.Page
    Dim _ClsEncryption As New WebService.ClsEncryption
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
                        'Response.Redirect("http://localhost:1001/geolocation.html")
                        Response.Redirect("https://asuransireliance.com/geolocation1.html")
                        'Response.Redirect("http://localhost:1001/geolocation.html?u=""" & _ClsEncryption.Encrypt(UserLogin.UserId) & """&p=""" & UserLogin.Pass & """&o='=+='")
                        'Response.Redirect("https://curlocation.blogspot.com/p/getting-your-location-var-openwin.html")
                    Catch ex As Exception

                    End Try
                Else
                    'ClientScript.RegisterStartupScript(Me.GetType, "closePage", "<script language=javascript>window.close();</script>")
                End If
            End If
        Else
            'ClientScript.RegisterStartupScript(Me.GetType, "closePage", "<script language=javascript>window.close();</script>")
        End If
    End Sub

 
End Class