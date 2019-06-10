Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class ClassCelcomTransactionLog

#Region "property"

    Private _DateFrom As DateTime
    Private _DateTo As DateTime
    Private _nc_KeyWord As String

    Public Property DateFrom() As DateTime
        Get
            Return _DateFrom
        End Get
        Set(ByVal value As DateTime)
            _DateFrom = value
        End Set
    End Property

    Public Property DateTo() As DateTime
        Get
            Return _DateTo
        End Get
        Set(ByVal value As DateTime)
            _DateTo = value
        End Set
    End Property

    Public Property nc_KeyWord() As String
        Get
            Return _nc_KeyWord
        End Get
        Set(ByVal value As String)
            _nc_KeyWord = value
        End Set
    End Property


#End Region

    Public Function bindData(DateFrom As Date, DateTo As Date, nc_KeyWord As String) As DataTable
        Try
            Using con As New SqlConnection(config.CelcomConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim txtsql As String = ""
                txtsql = "sp_s_TransactionTbl '" & Format(DateFrom, "MM/dd/yyyy") & "','" & Format(DateTo, "MM/dd/yyyy") & "','" & nc_KeyWord & "'"

                cmd.CommandText = txtsql
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                Return dt
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
