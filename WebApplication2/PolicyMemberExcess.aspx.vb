﻿
Imports SPGeneral
Public Class PolicyMemberExcess
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
                        Session("DashBoard") = "Excess <i class='fa fa-file-text fa-fw'></i>"
                        Dim filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                        _clsrole.RoleCode = Session("RoleCode")
                        stsfrm = _clsrole.PolicyMemberAdminOpen(_clsrole.RoleCode)

                        If stsfrm = True And Session("rolecode") = "00005" Then
                            PnlPolicy.Visible = True
                            PnlSearch.Visible = False
                            Dim dt As DataTable = _ClsPolicyMember.bindDataLOGINDETAIL_Policyno(Session("UserId"))
                            DDLPolicy.DataValueField = "POLICYNO"
                            DDLPolicy.DataTextField = "POLICYNO"
                            DDLPolicy.DataSource = dt
                            DDLPolicy.DataBind()
                            bindDatapolicyno("5", Session("UserId"), DDLPolicy.SelectedValue)
                            Exit Sub
                        End If
                        If stsfrm = True Then
                            PnlPolicy.Visible = True
                            PnlSearch.Visible = True

                            Dim dt As DataTable = _ClsPolicyMember.bindDataLOGINDETAIL_Policyno(Session("UserId"))
                            DDLPolicy.DataValueField = "POLICYNO"
                            DDLPolicy.DataTextField = "POLICYNO"
                            DDLPolicy.DataSource = dt
                            DDLPolicy.DataBind()
                        Else
                            PnlPolicy.Visible = False
                            PnlSearch.Visible = True
                        End If
                        'doreset()
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - PolicyMemberExcess - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=PolicyMemberExcess.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=PolicyMemberExcess.aspx", False)
        End If
    End Sub

    Protected Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            GVVw_Claim_Info_Header.PageIndex = 0
            _clsrole.RoleCode = Session("RoleCode")
            stsfrm = _clsrole.PolicyMemberAdminOpen(_clsrole.RoleCode)
            If stsfrm = True Then
                bindDatapolicyno(ddlSearch.SelectedValue, TxtKeyWord.Text, DDLPolicy.SelectedValue)
            Else
                bindData(ddlSearch.SelectedValue, TxtKeyWord.Text)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - PolicyMemberExcess - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Protected Sub bindDataClaimInfoHeader(policy As String, membid As String)
        Try
            GVVw_Claim_Info_Header.DataSource = _ClsPolicyMember.bindDataClaimInfoHeader(policy, membid)
            GVVw_Claim_Info_Header.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub bindData(type As String, KeyWord As String)
        Try
            GVVw_Claim_Info_Header.DataSource = _ClsPolicyMember.bindDataClaim_Info_Header_Policyno_excess_value(type, KeyWord)
            GVVw_Claim_Info_Header.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub bindDatapolicyno(type As String, value As String, policyno As String)
        Try
            GVVw_Claim_Info_Header.DataSource = _ClsPolicyMember.bindDataClaim_Info_Header_Policyno_excess(type, value, policyno)
            GVVw_Claim_Info_Header.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub bindisiData(noPolicy As String, memberid As String)
        Try
            Dim dt As New DataTable, dt1 As New DataTable, dt2 As New DataTable
            dt = _ClsPolicyMember.bindisiData(noPolicy, memberid)


            'GVVw_Claim_Info_Header.DataSource = _ClsPolicyMember.bindDataClaimInfoHeader(noPolicy, memberid)
            'GVVw_Claim_Info_Header.DataBind()

            If dt.Rows.Count > 0 Then
                LblBirthDate.Text = FormatDateTime(dt.Rows(0)("BIRTHDATE").ToString, DateFormat.ShortDate)
                LblClientNm.Text = dt.Rows(0)("CLIENTNAME").ToString
                LblMemID.Text = dt.Rows(0)("MEMBID").ToString & " / " & dt.Rows(0)("FULLNAME").ToString
                LblNIK.Text = dt.Rows(0)("EMPLOYEEID").ToString
                LblPeserta.Text = dt.Rows(0)("EMPNAME").ToString
                LblPOLIS.Text = dt.Rows(0)("POLICYNO").ToString
                LblRelasi.Text = dt.Rows(0)("RELSHIPID").ToString & " - " & dt.Rows(0)("RELSHIPNM2").ToString
                LblSex.Text = IIf(dt.Rows(0)("SEX").ToString = "M", "LAKI-LAKI", "PEREMPUAN")
                If dt.Rows(0)("STATUS").ToString = "IF" Then
                    LblStatus.Text = "INFORCE"
                ElseIf dt.Rows(0)("STATUS").ToString = "TR" Then
                    LblStatus.Text = "TERMINATE"
                ElseIf dt.Rows(0)("STATUS").ToString = "CN" Then
                    LblStatus.Text = "CANCEL"
                ElseIf dt.Rows(0)("STATUS").ToString = "DT" Then
                    LblStatus.Text = "DEATH"
                End If
                LblAccNo.Text = dt.Rows(0)("ACCOUNTNO").ToString & " \ " & dt.Rows(0)("ACCOUNTNM").ToString
            Else
                LblBirthDate.Text = ""
                LblClientNm.Text = ""
                LblMemID.Text = ""
                LblNIK.Text = ""
                LblPeserta.Text = ""
                LblPOLIS.Text = ""
                LblRelasi.Text = ""
                LblSex.Text = ""
                LblStatus.Text = ""
                LblAccNo.Text = ""
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub LinkClose4_Click(sender As Object, e As EventArgs) Handles LinkClose.Click
        System.Threading.Thread.Sleep(500)
        PnlMain.Visible = True
        pnlPopup.Visible = False
    End Sub

    Protected Sub bindDataClaimInfoDetailExcess(claimno As String)
        Try
            Dim dt As New DataTable, dt1 As New DataTable, dt2 As New DataTable

            GVClaim_Info_Detail.DataSource = _ClsPolicyMember.bindDataClaimInfoDetailExcess(claimno)
            GVClaim_Info_Detail.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub GVVw_Claim_Info_Header_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GVVw_Claim_Info_Header.RowCommand
        System.Threading.Thread.Sleep(500)
        If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then
            Dim KEY As String = e.CommandArgument
            '
            Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            Dim index As Integer = gvRow.RowIndex
            Dim hfMEMBID As String = DirectCast(GVVw_Claim_Info_Header.Rows(index).FindControl("GFMEMBID"), HiddenField).Value
            Dim hfPolicyNo As String = DirectCast(GVVw_Claim_Info_Header.Rows(index).FindControl("GFPOLICYNO"), HiddenField).Value
            bindisiData(hfPolicyNo, hfMEMBID)

            pnlPopup.Visible = True
            PnlMain.Visible = False
            bindDataClaimInfoDetailExcess(KEY)
            'txtBranchCode.ReadOnly = True
        End If
    End Sub

    Private Sub GVVw_Claim_Info_Header_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GVVw_Claim_Info_Header.PageIndexChanging
        Try
            System.Threading.Thread.Sleep(500)
            GVVw_Claim_Info_Header.PageIndex = e.NewPageIndex

            _clsrole.RoleCode = Session("RoleCode")
            stsfrm = _clsrole.PolicyMemberAdminOpen(_clsrole.RoleCode)
            If stsfrm = True And Session("rolecode") = "00005" Then
                bindDatapolicyno("5", Session("UserId"), DDLPolicy.SelectedValue)
                Exit Sub
            End If
            If stsfrm = True Then
                bindDatapolicyno(ddlSearch.SelectedValue, TxtKeyWord.Text, DDLPolicy.SelectedValue)
            Else
                bindData(ddlSearch.SelectedValue, TxtKeyWord.Text)
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - PolicyMemberExcess - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

End Class