Public Class ReportSumDetailClaim
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim dawal As Date = Left(reservation.Text, 10)
        Dim dakhir As Date = Right(reservation.Text, 10)
        Dim nopol As String = TxtKeyWord.Text
        viewrpt("WebViewer.aspx", Format(dawal, "yyyy-MM-dd"), Format(dakhir, "yyyy-MM-dd"), nopol)
    End Sub

    Sub viewrpt(strform As String, key1 As String, key2 As String, key3 As String)
        Try
            'If Session("Syariah") = False Then
            '    Session("No") = "2"
            'Else
            Session("No") = "3"
            'End If
            Session("key1") = key1
            Session("key2") = key2
            Session("key3") = key3
            Session("Param1") = Session("Username")
            Session("Param2") = "Date : " & Format(CDate(Left(reservation.Text, 10)), "dd-MMM-yyyy") & " S/d " & Format(CDate(Right(reservation.Text, 10)), "dd-MMM-yyyy")
            Session("JudulXls") = "Summary Detail Claim"
            Response.Redirect(strform, False)
            'ClientScript.RegisterStartupScript(Me.GetType, "onClick", "window.open('" & strform & "','_newtab');", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class