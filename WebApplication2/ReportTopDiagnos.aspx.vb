Imports SPGeneral
Public Class ReportTopDiagnos
    Inherits System.Web.UI.Page

    Dim _ClsPolicyMember As New WebService.ClsPolicyMember
    'Dim _ClsProduk As New WebService.ClsProduct
    Dim _sama As New WebService.sama
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
                        Session("DashBoard") = "TOP 10 DIAGNOSIS <i class='fa fa-star fa-fw'></i>"
                        Dim filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                        _clsrole.RoleCode = Session("RoleCode")
                        stsfrm = _clsrole.PolicyMemberAdminOpen(_clsrole.RoleCode)
                        reservation.Text = Format(Today, "dd/MM/yyyy - dd/MM/yyyy")

                        If stsfrm = True And (Session("rolecode") = "00005") Then
                            PnlPolicy.Visible = True
                            TxtKeyWord.Visible = False
                            DDLPolicy.Visible = True
                            Dim dt As DataTable = _ClsPolicyMember.bindDataLOGINDETAIL_Policyno(Session("UserId"))
                            DDLPolicy.DataValueField = "POLICYNO"
                            DDLPolicy.DataTextField = "POLICYNO"
                            DDLPolicy.DataSource = dt
                            DDLPolicy.DataBind()
                            Exit Sub
                        End If

                        If stsfrm = False And (Session("rolecode") = "00001" Or Session("rolecode") = "00002") Then
                            PnlPolicy.Visible = True
                            TxtKeyWord.Visible = True
                            DDLPolicy.Visible = False

                            Exit Sub
                        End If
                        If stsfrm = True Then
                            PnlPolicy.Visible = True
                            TxtKeyWord.Visible = False

                            Dim dt As DataTable = _ClsPolicyMember.bindDataLOGINDETAIL_Policyno(Session("UserId"))
                            DDLPolicy.DataValueField = "POLICYNO"
                            DDLPolicy.DataTextField = "POLICYNO"
                            DDLPolicy.DataSource = dt
                            DDLPolicy.DataBind()
                        Else
                            PnlPolicy.Visible = False
                            TxtKeyWord.Visible = False
                        End If
                        'doreset()
                    Catch ex As Exception

                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - ReportTopDiagnos - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=ReportTopDiagnos.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=ReportTopDiagnos.aspx", False)
        End If
    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim dawal As Date = Left(reservation.Text, 10)
        Dim dakhir As Date = Right(reservation.Text, 10)
        Dim nopol As String = IIf(DDLPolicy.Visible = True, DDLPolicy.SelectedValue, TxtKeyWord.Text)
        Dim opt As String = RBOption.SelectedValue
        viewrpt("WebViewer.aspx", Format(dawal, "yyyy-MM-dd"), Format(dakhir, "yyyy-MM-dd"), nopol, opt)
    End Sub

    Sub viewrpt(strform As String, key1 As String, key2 As String, key3 As String, key4 As String)
        Try
            'If Session("Syariah") = False Then
            '    Session("No") = "2"
            'Else
            Session("No") = "2"
            'End If
            Session("key1") = key1
            Session("key2") = key2
            Session("key3") = key3
            Session("key4") = key4
            Session("Param1") = Session("Username")
            Session("Param2") = "Date : " & Format(CDate(Left(reservation.Text, 10)), "dd-MMM-yyyy") & " S/d " & Format(CDate(Right(reservation.Text, 10)), "dd-MMM-yyyy")
            Session("JudulXls") = "TOP_10_Diagnosis"
            Response.Redirect(strform, False)
            'ClientScript.RegisterStartupScript(Me.GetType, "onClick", "window.open('" & strform & "','_newtab');", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class