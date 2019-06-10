Imports SPGeneral
Public Class ReportRemainingBenefit
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
                        Session("DashBoard") = "REMAINING BENEFIT INFO <i class='fa fa-dollar fa-fw'></i>"
                        Dim filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                        _clsrole.RoleCode = Session("RoleCode")
                        stsfrm = _clsrole.PolicyMemberAdminOpen(_clsrole.RoleCode)

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
                        Dim msg As String = String.Format("{0} - ReportRemainingBenefit - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=ReportRemainingBenefit.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=ReportRemainingBenefit.aspx", False)
        End If
    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim nopol As String = IIf(DDLPolicy.Visible = True, DDLPolicy.SelectedValue, TxtKeyWord.Text)

        viewrpt("WebViewer.aspx", nopol, DDLRemaining.SelectedValue)
    End Sub

    Sub viewrpt(strform As String, key1 As String, key2 As String)
        Try
            Session("No") = "4"
            'End If
            Session("key1") = key1
            Session("key2") = key2
            Session("Param1") = Session("Username")
            Session("Param2") = DDLRemaining.SelectedItem.Text
            Session("JudulXls") = "REMAINING_BENEFIT"
            Response.Redirect(strform, False)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class