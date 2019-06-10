Imports SPGeneral
Public Class PrintTransactionLog

    Inherits System.Web.UI.Page
    Public Shared _gvsorting As String
    Public Shared _gvsortingsum As String
    Public Shared _gvdirect As String
    Public Shared _gvdirectsum As String
    Public Shared _dateFrom As Date
    Public Shared _dateTo As Date

    Public Property gvsorting() As String
        Get
            Return _gvsorting
        End Get
        Set(ByVal value As String)
            _gvsorting = value
        End Set
    End Property

    Public Property gvdirect() As String
        Get
            Return _gvdirect
        End Get
        Set(ByVal value As String)
            _gvdirect = value
        End Set
    End Property

    Public Property gvdirectsum() As String
        Get
            Return _gvdirectsum
        End Get
        Set(ByVal value As String)
            _gvdirectsum = value
        End Set
    End Property

    Public Property gvsortingsum() As String
        Get
            Return _gvsortingsum
        End Get
        Set(ByVal value As String)
            _gvsortingsum = value
        End Set
    End Property

    Dim _sama As New WebService.sama
    Dim _CelcomTransactionLog As New WebService.ClassCelcomTransactionLog

    Public Property UserLogin() As WebService.ClsUser
        Get
            Return CType(Session("Users"), WebService.ClsUser)
        End Get
        Set(ByVal value As WebService.ClsUser)
            Session("Users") = value
        End Set
    End Property


    Public Function bindData() As DataTable
        Try
            Dim dateFrom As Date = Session("dateFrom")
            Dim dateTo As Date = Session("dateTo")
            Dim txtKey As String = Session("txtKey")
            Dim dt As New DataTable
            dt = _CelcomTransactionLog.bindData(dateFrom, dateTo, txtKey)

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not UserLogin Is Nothing Then
            If Not Page.IsPostBack Then
                If UserLogin.IsActive Then
                    Try

                        LblDate.Text = Format(Today, "dd-MMM-yyyy")
                        LblUserId.Text = UserLogin.UserName
                        Dim dt As DataTable = bindData()
                        gvwList.DataSource = dt
                        gvwList.DataBind()

                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
                        Dim msg As String = String.Format("{0} - PrintStockItem - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("../login.aspx?p=WinPrint/PrintStockItem.aspx", False)
                End If
            End If
        Else
            Response.Redirect("../login.aspx?p=WinPrint/PrintStockItem.aspx", False)
        End If
    End Sub

    Private Sub gvwList_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvwList.Sorting
        Try
            System.Threading.Thread.Sleep(500)
            gvsorting = e.SortExpression
            sorting(False)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
            Dim msg As String = String.Format("{0} - RptTransactionLog - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Sub sorting(paging As Boolean)
        Try
            If paging = False Then
                If SortDirection() = SortDirection.Ascending Then
                    SortDirection = SortDirection.Descending
                    gvdirect = " DESC"
                Else
                    SortDirection = SortDirection.Ascending
                    gvdirect = " ASC"
                End If
            End If

            Dim table As DataTable = bindData()
            table.DefaultView.Sort = gvsorting + gvdirect
            gvwList.DataSource = table
            gvwList.DataBind()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
            Dim msg As String = String.Format("{0} - RptTransactionLog - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

    Private Property SortDirection() As SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = SortDirection.Ascending
            End If
            Return ViewState("SortDirection")
        End Get
        Set(ByVal value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub gvwList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvwList.PageIndexChanging
        Try
            System.Threading.Thread.Sleep(500)
            gvwList.PageIndex = e.NewPageIndex
            sorting(True)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, Please contact IT Support');</script>")
            Dim msg As String = String.Format("{0} - RptTransactionLog - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub


End Class