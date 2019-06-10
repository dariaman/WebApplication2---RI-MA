Imports System.Data.Sql
Imports System.Data.SqlClient

'create  by pandu
Public Class ClsCompany

#Region "property"

    Private _CompanyId As String
    Private _CompanyAddress As String
    Private _CompanyName As String
    Private _CompanyPict As String
    Private _IsActive As String
    Private _CRE_BY As String

    Public Property CompanyId() As String
        Get
            Return _CompanyId
        End Get
        Set(ByVal value As String)
            _CompanyId = value
        End Set
    End Property

    Public Property CompanyAddress() As String
        Get
            Return _CompanyAddress
        End Get
        Set(ByVal value As String)
            _CompanyAddress = value
        End Set
    End Property

    Public Property CompanyName() As String
        Get
            Return _CompanyName
        End Get
        Set(ByVal value As String)
            _CompanyName = value
        End Set
    End Property

    Public Property CompanyPict() As String
        Get
            Return _CompanyPict
        End Get
        Set(ByVal value As String)
            _CompanyPict = value
        End Set
    End Property
    Public Property CRE_BY() As String
        Get
            Return _CRE_BY
        End Get
        Set(ByVal value As String)
            _CRE_BY = value
        End Set
    End Property


    Public Property IsActive() As String
        Get
            Return _IsActive
        End Get
        Set(ByVal value As String)
            _IsActive = value
        End Set
    End Property

#End Region

    Public Function InsertCompany() As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                con.Open()
                Dim sql As String = ""
                sql = "dbo.SP_I_U_MSCompany"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure
                com.Parameters.Add("@CompanyId", SqlDbType.VarChar).Value = CompanyId
                com.Parameters.Add("@CompanyAddress ", SqlDbType.VarChar).Value = CompanyAddress
                com.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = CompanyName
                com.Parameters.Add("@CompanyPict", SqlDbType.VarChar).Value = CompanyPict
                com.Parameters.Add("@CRE_BY ", SqlDbType.VarChar).Value = CRE_BY
                com.Parameters.Add("@IsActive ", SqlDbType.VarChar).Value = IsActive
                Return com.ExecuteScalar

                'If rowSuccess = "" Then
                '    Return False
                '    Throw New Exception("Error -" & sql)
                'Else
                '    Return True
                'End If
            Catch ex As Exception
                Return ""
                Throw New Exception(ex.Message)

            Finally
                con.Close()
            End Try
        End Using
    End Function
    Public Function bindData(Company As String, type As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim txtsql As String = ""
                If type = "Kode" Then
                    txtsql = " SELECT [CompanyId],[CompanyName],[CompanyAddress],COMPANYPICT,IsActive FROM [dbo].[MSCompany] with (nolock) where [CompanyId] like '" & Company & "%'  ORDER BY [CompanyId]"
                Else
                    txtsql = " SELECT [CompanyId],[CompanyName],[CompanyAddress],COMPANYPICT,IsActive FROM [dbo].[MSCompany] with (nolock) where [CompanyName] like '" & Company & "%'  ORDER BY [CompanyName]"
                End If
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

    Public Function bindisiData(ID As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = "SELECT [CompanyId],[CompanyName],[CompanyAddress],COMPANYPICT,IsActive FROM [dbo].[MSCompany] with (nolock) where [CompanyId]= '" & ID & "'  ORDER BY CompanyId"
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
