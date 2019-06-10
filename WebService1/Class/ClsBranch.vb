Imports System.Data.Sql
Imports System.Data.SqlClient

'create  by pandu
Public Class ClsBranch
#Region "property"

    Private _BranchCode As String
    Private _BranchAbbreviation As String
    Private _BranchName As String
    Private _BranchAdd As String
    Private _BranchCity As String
    Private _BranchZIP As String
    Private _BranchPhone As String
    Private _BranchFax As String
    Private _NPWP As String
    Private _IsActive As Boolean
    Private _CRE_BY As String

    Public Property BranchCode() As String
        Get
            Return _BranchCode
        End Get
        Set(ByVal value As String)
            _BranchCode = value
        End Set
    End Property

    Public Property BranchAbbreviation() As String
        Get
            Return _BranchAbbreviation
        End Get
        Set(ByVal value As String)
            _BranchAbbreviation = value
        End Set
    End Property

    Public Property BranchName() As String
        Get
            Return _BranchName
        End Get
        Set(ByVal value As String)
            _BranchName = value
        End Set
    End Property
    Public Property BranchAdd() As String
        Get
            Return _BranchAdd
        End Get
        Set(ByVal value As String)
            _BranchAdd = value
        End Set
    End Property

    Public Property BranchCity() As String
        Get
            Return _BranchCity
        End Get
        Set(ByVal value As String)
            _BranchCity = value
        End Set
    End Property

    Public Property BranchZIP() As String
        Get
            Return _BranchZIP
        End Get
        Set(ByVal value As String)
            _BranchZIP = value
        End Set
    End Property

    Public Property BranchPhone() As String
        Get
            Return _BranchPhone
        End Get
        Set(ByVal value As String)
            _BranchPhone = value
        End Set
    End Property

    Public Property BranchFax() As String
        Get
            Return _BranchFax
        End Get
        Set(ByVal value As String)
            _BranchFax = value
        End Set
    End Property

    Public Property NPWP() As String
        Get
            Return _NPWP
        End Get
        Set(ByVal value As String)
            _NPWP = value
        End Set
    End Property
    Public Property IsActive() As Boolean
        Get
            Return _IsActive
        End Get
        Set(ByVal value As Boolean)
            _IsActive = value
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


#End Region

    Public Function bindData(kd_branch As String, type As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim kode As String = ""
                Dim lokasi As String = ""
                Dim txtsql As String
                If type = "Kode" Then
                    txtsql = " SELECT * FROM MSBranch with (nolock) where BranchCode like '" & kd_branch & "%'  ORDER BY BranchCode"
                Else
                    txtsql = " SELECT * FROM MSBranch with (nolock) where BranchName like '" & kd_branch & "%'  ORDER BY BranchName"
                End If
                Try
                    cmd.CommandText = txtsql
                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable
                    da.Fill(dt)
                    Return dt

                Catch ex As Exception
                    Return Nothing
                Finally
                    con.Close()
                End Try
                cmd.CommandText = txtsql

            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function bindisiData(kd_branch As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = " SELECT * FROM MSBranch with (nolock) where BranchCode = '" & kd_branch & "'  ORDER BY BranchCode"
                Try

                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable
                    da.Fill(dt)
                    Return dt
                Catch ex As Exception
                    Return Nothing
                Finally
                    con.Close()
                End Try
            End Using

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function InsertBranch() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                con.Open()
                Dim sql As String = ""
                sql = "dbo.SP_I_U_MSBranch"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure

                com.Parameters.Add("@BranchCode", SqlDbType.VarChar).Value = BranchCode
                com.Parameters.Add("@BranchAbbreviation", SqlDbType.VarChar).Value = BranchAbbreviation
                com.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = BranchName
                com.Parameters.Add("@BranchAdd", SqlDbType.VarChar).Value = BranchAdd
                com.Parameters.Add("@BranchCity", SqlDbType.VarChar).Value = BranchCity
                com.Parameters.Add("@BranchZIP", SqlDbType.VarChar).Value = BranchZIP
                com.Parameters.Add("@BranchPhone", SqlDbType.VarChar).Value = BranchPhone
                com.Parameters.Add("@BranchFax", SqlDbType.VarChar).Value = BranchFax
                com.Parameters.Add("@NPWP", SqlDbType.VarChar).Value = NPWP
                com.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IIf(IsActive.ToString = "True", 1, 0)
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

End Class

