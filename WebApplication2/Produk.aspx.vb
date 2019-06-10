Imports SPGeneral
Public Class Produk
    Inherits System.Web.UI.Page
    Dim _ClsProduct As New WebService.ClsProduct
    Dim _sama As New WebService.sama

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
                        Session("DashBoard") = "Product <i class='fa fa-cubes fa-fw'></i>"
                        Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                            Response.Redirect("Home.aspx", False)
                        End If
                        _sama.isiddlmsProductGroup(ddlTypeProduct, "", "True")
                        _sama.isiddlMSUnit(DDLProductUnit)
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
                        Dim msg As String = String.Format("{0} - ProduK - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=ProduK.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=ProduK.aspx", False)
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click, LinkSubmit.Click
        Try
            System.Threading.Thread.Sleep(500)
            If validasi() = True Then
                _ClsProduct.ID = lblID.Text
                _ClsProduct.TypeProduk = ddlTypeProduct.SelectedValue
                _ClsProduct.DESCRIPTION = TxtDescription.Text
                _ClsProduct.IsActive = chkAktiv.Checked
                _ClsProduct.Product = TxtProduct.Text
                _ClsProduct.ProdukPlus = chkPlus.Checked
                _ClsProduct.CRE_BY = UserLogin.UserId
                _ClsProduct.CRE_IP = _sama.ClientIP_add
                _ClsProduct.price = TxtPrice.Text
                _ClsProduct.priceSale = Txtpricesale.Text
                _ClsProduct.itemStock = Txtitemstock.Text
                _ClsProduct.UnitCode = DDLProductUnit.SelectedValue
                If _ClsProduct.InsertProduct() = True Then
                    ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Data saved!');</script>")
                    LinkSubmit.Enabled = False
                    bindData(txtKeyWord1.Text)
                End If
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
            Dim msg As String = String.Format("{0} - Produk - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Public Function validasi() As Boolean
        If ddlTypeProduct.SelectedIndex = 0 Or TxtProduct.Text = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('All Fields cannot empty!');</script>")
            Return False
        ElseIf CDec(TxtPrice.Text) > CDec(Txtpricesale.Text) Then
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Price cannot greater then price sales');</script>")
            Return False
        Else
            Return True
        End If
        Return True
    End Function

    Sub doreset()
        ddlSearch.SelectedIndex = 0
        lblID.Text = ""
        ddlTypeProduct.SelectedIndex = 0
        DDLProductUnit.SelectedIndex = 0
        TxtProduct.Text = ""
        TxtPrice.Text = 0
        Txtpricesale.Text = 0
        Txtitemstock.Text = 0
        chkPlus.Checked = False
        chkAktiv.Checked = True
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            bindData(txtKeyWord1.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
            Dim msg As String = String.Format("{0} - Produk - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try

    End Sub

    Protected Sub bindData(Product As String)
        Try
            gridMenu.DataSource = _ClsProduct.bindData(Product, ddlSearch.SelectedValue)
            gridMenu.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub bindisiData(ID As String)
        Try
            Dim dt As DataTable = _ClsProduct.bindisiData(ID)
            If dt.Rows.Count > 0 Then
                PnlMain.Visible = False
                pnlPopup.Visible = True
                lblID.Text = dt.Rows(0)(0).ToString
                ddlTypeProduct.SelectedValue = dt.Rows(0)(1).ToString
                TxtDescription.Text = dt.Rows(0)(2).ToString
                TxtProduct.Text = dt.Rows(0)(3).ToString
                DDLProductUnit.SelectedValue = dt.Rows(0)(4).ToString
                chkAktiv.Checked = dt.Rows(0)(5).ToString
                chkPlus.Checked = dt.Rows(0)(6).ToString
                TxtPrice.Text = FormatNumber(CDec(dt.Rows(0)(7).ToString), 2)
                Txtpricesale.Text = FormatNumber(CDec(dt.Rows(0)(8).ToString), 2)
                Txtitemstock.Text = CDec(dt.Rows(0)(9).ToString)
            Else
                doreset()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        System.Threading.Thread.Sleep(500)
        doreset()
        PnlMain.Visible = False
        pnlPopup.Visible = True
        'TxtProduct.ReadOnly = False
    End Sub

    Protected Sub LinkClose4_Click(sender As Object, e As EventArgs) Handles LinkClose.Click
        System.Threading.Thread.Sleep(500)
        PnlMain.Visible = True
        pnlPopup.Visible = False
        LinkSubmit.Enabled = True
    End Sub

    Private Sub gridMenu_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridMenu.PageIndexChanging
        Try
            System.Threading.Thread.Sleep(500)
            gridMenu.PageIndex = e.NewPageIndex
            bindData(txtKeyWord1.Text)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
            Dim msg As String = String.Format("{0} - Produk - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Sub DGuser_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridMenu.RowCommand
        Try
            System.Threading.Thread.Sleep(500)
            If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then
                Dim KEY As String = e.CommandArgument
                bindisiData(KEY)
                'TxtProduct.ReadOnly = True
            End If
            
            If e.CommandName = "UpdateLink" Then
                System.Threading.Thread.Sleep(500)
                Dim KEY As String = e.CommandArgument

                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim index As Integer = gvRow.RowIndex
                Dim sts As String = DirectCast(gridMenu.Rows(index).FindControl("hfstatus"), HiddenField).Value
                '_ClsInventarisItem.NO = KEY
                '_ClsInventarisItem.STATUS = IIf(sts = 1, 0, 1)
                '_ClsInventarisItem.updateStatus()
                _sama.UpdateActive("MSProduk", "ISACTIVE", IIf(sts = "True", "False", "True"), "ID", KEY)
                bindData(txtKeyWord1.Text)
            End If
            'If e.CommandName = "UpdateLink" Then
            '    System.Threading.Thread.Sleep(500)
            '    Dim KEY As String = e.CommandArgument

            '    Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            '    Dim index As Integer = gvRow.RowIndex
            '    Dim sts As String = DirectCast(gridMenu.Rows(index).FindControl("hfstatus"), HiddenField).Value
            '    '_ClsInventarisItem.NO = KEY
            '    '_ClsInventarisItem.STATUS = IIf(sts = 1, 0, 1)
            '    '_ClsInventarisItem.updateStatus()
            '    '_sama.UpdateActive("MSProduk", "ISACTIVE", IIf(sts = "True", "False", "True"), "ID", KEY)
            '    bindData(txtKeyWord1.Text)
            'End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
            Dim msg As String = String.Format("{0} - Produk - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Protected Sub gridMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridMenu.SelectedIndexChanged

    End Sub
End Class