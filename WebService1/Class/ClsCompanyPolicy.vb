Imports System.Data.Sql
Imports System.Data.SqlClient

'create  by pandu
Public Class ClsCompanyPolicy

#Region "property"

    Private _CompanyId As String
    Private _POLICYNO As String
    Private _CRE_BY As String
    Private _IsTPA As Boolean
    Private _TPAName As String
    Private _isTPAEmail As Boolean
    Private _TPAEmailAddress As String
    Private _isTPAFTP As Boolean
    Private _TPAFTPAddress As String
    Private _PICEmailPolicy As String
    Private _isNotClaimFoto As Boolean

    Public Property CompanyId() As String
        Get
            Return _CompanyId
        End Get
        Set(ByVal value As String)
            _CompanyId = value
        End Set
    End Property

    Public Property POLICYNO() As String
        Get
            Return _POLICYNO
        End Get
        Set(ByVal value As String)
            _POLICYNO = value
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

    Public Property IsTPA() As Boolean
        Get
            Return _IsTPA
        End Get
        Set(ByVal value As Boolean)
            _IsTPA = value
        End Set
    End Property

    Public Property TPAName() As String
        Get
            Return _TPAName
        End Get
        Set(ByVal value As String)
            _TPAName = value
        End Set
    End Property

    Public Property isTPAEmail() As Boolean
        Get
            Return _isTPAEmail
        End Get
        Set(ByVal value As Boolean)
            _isTPAEmail = value
        End Set
    End Property

    Public Property TPAEmailAddress() As String
        Get
            Return _TPAEmailAddress
        End Get
        Set(ByVal value As String)
            _TPAEmailAddress = value
        End Set
    End Property

    Public Property isTPAFTP() As Boolean
        Get
            Return _isTPAFTP
        End Get
        Set(ByVal value As Boolean)
            _isTPAFTP = value
        End Set
    End Property

    Public Property TPAFTPAddress() As String
        Get
            Return _TPAFTPAddress
        End Get
        Set(ByVal value As String)
            _TPAFTPAddress = value
        End Set
    End Property

    Public Property PICEmailPolicy() As String
        Get
            Return _PICEmailPolicy
        End Get
        Set(ByVal value As String)
            _PICEmailPolicy = value
        End Set
    End Property

    Public Property isNotClaimFoto() As Boolean
        Get
            Return _isNotClaimFoto
        End Get
        Set(ByVal value As Boolean)
            _isNotClaimFoto = value
        End Set
    End Property
#End Region

    Public Function InsertCompanyPolicy() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                con.Open()
                Dim sql As String = ""
                sql = "dbo.SP_I_MSCOMPANYPOLICY"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure
                com.Parameters.Add("@CompanyId", SqlDbType.VarChar).Value = CompanyId
                com.Parameters.Add("@POLICYNO", SqlDbType.VarChar).Value = POLICYNO
                com.Parameters.Add("@IsTPA", SqlDbType.VarChar).Value = IsTPA
                com.Parameters.Add("@TPAName", SqlDbType.VarChar).Value = TPAName
                com.Parameters.Add("@isTPAEmail", SqlDbType.VarChar).Value = isTPAEmail
                com.Parameters.Add("@TPAEmailAddress", SqlDbType.VarChar).Value = TPAEmailAddress
                com.Parameters.Add("@isTPAFTP", SqlDbType.VarChar).Value = isTPAFTP
                com.Parameters.Add("@TPAFTPAddress", SqlDbType.VarChar).Value = TPAFTPAddress
                com.Parameters.Add("@PICEmailPolicy", SqlDbType.VarChar).Value = PICEmailPolicy
                com.Parameters.Add("@isNotClaimFoto", SqlDbType.VarChar).Value = isNotClaimFoto
                com.Parameters.Add("@CRE_BY", SqlDbType.VarChar).Value = CRE_BY
                Dim rowSuccess As Integer = com.ExecuteNonQuery
                If rowSuccess = 0 Then
                    Return False
                    Throw New Exception("Error -" & sql)
                Else
                    Return True
                End If
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function DeleteCompanyPolicy() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                con.Open()
                Dim sql As String = ""
                sql = "dbo.SP_D_MSCOMPANYPOLICY"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure
                com.Parameters.Add("@CompanyId", SqlDbType.VarChar).Value = CompanyId
                com.Parameters.Add("@POLICYNO", SqlDbType.VarChar).Value = POLICYNO

                Dim rowSuccess As Integer = com.ExecuteNonQuery
                If rowSuccess = 0 Then
                    Return False
                    Throw New Exception("Error -" & sql)
                Else
                    Return True
                End If
            Catch ex As Exception
                Return False
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
                If type = "Id" Then
                    txtsql = " SELECT A.[CompanyId],[CompanyName],[CompanyAddress] FROM [dbo].[MSCOMPANY] A with (nolock) " & _
                             " where A.[CompanyId] like '" & Company & "%'  ORDER BY A.[CompanyId]"
                Else
                    txtsql = " SELECT A.[CompanyId],[CompanyName],[CompanyAddress] FROM [dbo].[MSCOMPANY] A with (nolock) " & _
                            " where [CompanyName] like '" & Company & "%'  ORDER BY [CompanyName]"
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
                cmd.CommandText = "SELECT [CompanyId],[POLICYNO],isNotClaimFoto,TPAName,IsTPA,PICEmailPolicy FROM [dbo].[MSCOMPANYPOLICY] with (nolock) where [CompanyId] = '" & ID & "'  ORDER BY CompanyId"
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                Return dt
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function PictureScalar(_POLICYNO As String) As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SP_S_COMPANYPICT '" & _POLICYNO & "'"
            Try
                con.Open()
                Return cmd.ExecuteScalar()
            Catch ex As Exception
                Return ""
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function DISABLEClaimFotoScalar(_POLICYNO As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SP_S_DISABLEClaimFoto '" & _POLICYNO & "'"
            Try
                con.Open()
                Return cmd.ExecuteScalar()
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function AddEmailScalar(_POLICYNO As String) As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SP_S_AddEmail '" & _POLICYNO & "'"
            Try
                con.Open()
                Return cmd.ExecuteScalar()
            Catch ex As Exception
                Return ""
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function
End Class
