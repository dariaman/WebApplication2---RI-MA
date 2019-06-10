Imports SPGeneral
Imports System.IO
Imports System
Imports System.Net.Mail
Imports System.Web.Mail
Public Class Home
    Inherits System.Web.UI.Page

    Dim _sama As New WebService.sama
    Dim _general As New WebService.general
    Dim _ClsGenerateTiket As New WebService.ClsTiket
    Dim _ClsPolicyMember As New WebService.ClsPolicyMember
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
                        Session("DashBoard") = "Home <i class='fa fa-home fa-fw'></i>"
                        'LblUsername.Text = Session("Username")
                        LblNamaPeserta.Text = Session("Username")
                        LblNamaPeserta1.Text = Session("Username")
                        
                        LblNoPol.Text = UserLogin.POLICYNO
                        LblIdPeserta.Text = UserLogin.memberid
                        LblNoPol1.Text = UserLogin.POLICYNO
                        LblIdPeserta1.Text = UserLogin.memberid
                        LblJnsKelamin.Text = IIf(UserLogin.GenderLogin = "M", "Laki-laki", "Perempuan")
                        LblTglLahir.Text = Format(UserLogin.BirthDateLogin, "dd-MMM-yyyy")
                        LblTglLahir1.Text = Format(UserLogin.BirthDateLogin, "dd-MMM-yyyy")
                        Dim dt As DataTable = _ClsGenerateTiket.EffExpDate(UserLogin.POLICYNO, UserLogin.memberid)
                        If dt.Rows.Count < 1 Then

                        Else
                            LblBelakusampai.Text = Format(CDate(dt.Rows(0)(1).ToString), "dd-MMM-yyyy")
                            'lblEffdt.Text = Format(CDate(dt.Rows(0)(0).ToString), "dd/MM/yyyy")
                        End If
                        Dim dt2 As DataTable = _ClsPolicyMember.bindDatapolicyno(1, UserLogin.memberid, UserLogin.POLICYNO)
                        If dt2.Rows.Count < 1 Then

                        Else
                            LblNamaKaryawan.Text = dt2.Rows(0)("EMPNAME").ToString
                            LblPerusahaan.Text = dt2.Rows(0)("CLIENTNAME").ToString
                            LblPerusahaan1.Text = dt2.Rows(0)("CLIENTNAME").ToString
                            LblFaskes.Text = dt2.Rows(0)("KLINIKBPJS").ToString
                            LblKelasRawat.Text = "1 (Satu)"
                            LblNoPesertaBPJS.Text = dt2.Rows(0)("NOBPJS").ToString
                            If dt2.Rows(0)("PRODUCTID").ToString = "P" Then
                                PnlNonIP.Visible = False
                                PnlIP.Visible = True
                            Else
                                PnlNonIP.Visible = True
                                PnlIP.Visible = False
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

End Class