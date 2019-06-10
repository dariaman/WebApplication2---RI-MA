Imports SPGeneral
Imports System.IO
Public Class History1
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

    Public Property dateFrom() As Date
        Get
            _dateFrom = Left(reservation.Text, 10)
            Return _dateFrom
        End Get
        Set(ByVal value As Date)
            _dateFrom = value
        End Set
    End Property

    Public Property dateTo() As Date
        Get
            _dateTo = Right(reservation.Text, 10)
            Return _dateTo
        End Get
        Set(ByVal value As Date)
            _dateTo = value
        End Set
    End Property

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
                        Session("DashBoard") = "History <i class='fa fa-clock-o fa-fw'></i>"
                        Dim Filename As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                        If _sama.MenuAccess(Filename, UserLogin.UserId) = False Then
                            Response.Redirect("Default.aspx", False)
                        End If
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
                        Dim msg As String = String.Format("{0} - history - " & UserLogin.UserId & " - {1}{2}", Now.ToString("dd/MM/yyyy HH:mm:ss"), ex, Environment.NewLine)
                        WriteFile.Write(config.SetFullFilePath, msg)
                    End Try
                Else
                    Response.Redirect("login.aspx?p=history.aspx", False)
                End If
            End If
        Else
            Response.Redirect("login.aspx?p=history.aspx", False)
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        Try
            System.Threading.Thread.Sleep(500)
            If reservation.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Input Date');</script>")

                gvwList.DataSource = Nothing
                gvwList.DataBind()
                Exit Sub
            End If
            If DateDiff(DateInterval.Day, dateFrom, dateTo) > 2 Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('Max total date are 3 days');</script>")
                gvwList.DataSource = Nothing
                gvwList.DataBind()
                Exit Sub
            End If

            If bindData().Rows.Count < 1 Then
                ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Information('No data');</script>")
                gvwList.DataSource = Nothing
                gvwList.DataBind()
                Exit Sub
            End If
            gvwList.DataSource = bindData()
            gvwList.DataBind()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - History - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub
    Public Function bindData() As DataTable
        Try
            Dim dt As New DataTable
            dt = _sama.bindData(txtKey.Text, dateFrom, dateTo)

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub gvwList_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvwList.Sorting
        Try
            System.Threading.Thread.Sleep(500)
            gvsorting = e.SortExpression
            sorting(False)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - History - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
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
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - History - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
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
            ClientScript.RegisterStartupScript(Me.GetType, "confirm", "<script language=javascript>jqxAlert.Alert('Error!, " & ex.Message.ToString & vbCrLf & Environment.NewLine & "');</script>")
            Dim msg As String = String.Format("{0} - History - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
    End Sub

End Class