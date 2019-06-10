Imports System.Device.Location

Public Class WebForm1
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Dim Watcher As New GeoCoordinateWatcher
            test()
            'If Watcher.Position.Location.IsUnknown Then

            '    txtLat.Text = "Cannot find location data"

            'Else
            'Dim location As GeoCoordinate = Watcher.Position.Location
            'txtLat.Text = location.Latitude.ToString()
            'txtLong.Text = location.Longitude.ToString()

            'End If
        End If
    End Sub

    Sub test()
        Dim cookieCols As New HttpCookieCollection

        cookieCols = Request.Cookies

        Dim str As String
        'Dim b(1) As String
        'Dim i As Integer
        If cookieCols.Count = 0 Then
            Response.Write("No cookies")

        End If
        For Each str In cookieCols
            'If str = "lat" Then
            ''Session("lat") = Request.Cookies("lat").Value
            ' ''End If
            ' ''If str = "longt" Then
            ''Session("longt") = Request.Cookies("longt").Value
            ' ''End If

            ' ''If str = "UserId" Then
            ''Session("UserId") = Request.Cookies("UserId").Value
            ' ''End If

            ' ''If str = "Password" Then
            ''Session("Password") = Request.Cookies("Password").Value
            ' ''End If

            'b(i) = "Cookie: " & str & " : " & Request.Cookies(str).Value
            Response.Write("Value:" & str & " : " & Request.Cookies(str).Value)
        Next
    End Sub
    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    'TextBox1.Text = Session("Lat")
    '    'TextBox2.Text = Session("Longt")
    'End Sub
End Class