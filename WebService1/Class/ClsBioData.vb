Imports System.Data.Sql
Imports System.Data.SqlClient

'create  by pandu
Public Class ClsBioData
#Region "property"

    Private _BioDataCode As String
    Private _BioDataNickName As String
    Private _BioDataSalutation As String
    Private _BioDataName As String
    Private _BioDataAdd As String
    Private _BioDataCity As String
    Private _BioDataZIP As String
    Private _BioDataPhone As String
    Private _BioDataFax As String
    Private _BioDataGender As String
    Private _BioDataEmail As String
    Private _BioDataPict As String
    Private _BioDataBirthdate As Date

    Private _BioDataContact As String
    Private _BioDataType As String
    Private _BranchCode As String
    Private _IsActive As Boolean
    Private _CRE_BY As String

    Public Property BioDataCode() As String
        Get
            Return _BioDataCode
        End Get
        Set(ByVal value As String)
            _BioDataCode = value
        End Set
    End Property

    Public Property BioDataNickName() As String
        Get
            Return _BioDataNickName
        End Get
        Set(ByVal value As String)
            _BioDataNickName = value
        End Set
    End Property

    Public Property BioDataName() As String
        Get
            Return _BioDataName
        End Get
        Set(ByVal value As String)
            _BioDataName = value
        End Set
    End Property

    Public Property BioDataSalutation() As String
        Get
            Return _BioDataSalutation
        End Get
        Set(ByVal value As String)
            _BioDataSalutation = value
        End Set
    End Property

    Public Property BioDataAdd() As String
        Get
            Return _BioDataAdd
        End Get
        Set(ByVal value As String)
            _BioDataAdd = value
        End Set
    End Property

    Public Property BioDataCity() As String
        Get
            Return _BioDataCity
        End Get
        Set(ByVal value As String)
            _BioDataCity = value
        End Set
    End Property

    Public Property BioDataZIP() As String
        Get
            Return _BioDataZIP
        End Get
        Set(ByVal value As String)
            _BioDataZIP = value
        End Set
    End Property

    Public Property BioDataPhone() As String
        Get
            Return _BioDataPhone
        End Get
        Set(ByVal value As String)
            _BioDataPhone = value
        End Set
    End Property

    Public Property BioDataFax() As String
        Get
            Return _BioDataFax
        End Get
        Set(ByVal value As String)
            _BioDataFax = value
        End Set
    End Property

    Public Property BioDataGender() As String
        Get
            Return _BioDataGender
        End Get
        Set(ByVal value As String)
            _BioDataGender = value
        End Set
    End Property

    Public Property BioDataEmail() As String
        Get
            Return _BioDataEmail
        End Get
        Set(ByVal value As String)
            _BioDataEmail = value
        End Set
    End Property

    Public Property BioDataPict() As String
        Get
            Return _BioDataPict
        End Get
        Set(ByVal value As String)
            _BioDataPict = value
        End Set
    End Property

    Public Property BioDataBirthdate() As Date
        Get
            Return _BioDataBirthdate
        End Get
        Set(ByVal value As Date)
            _BioDataBirthdate = value
        End Set
    End Property

    Public Property BioDataContact() As String
        Get
            Return _BioDataContact
        End Get
        Set(ByVal value As String)
            _BioDataContact = value
        End Set
    End Property

    Public Property BioDataType() As String
        Get
            Return _BioDataType
        End Get
        Set(ByVal value As String)
            _BioDataType = value
        End Set
    End Property

    Public Property BranchCode() As String
        Get
            Return _BranchCode
        End Get
        Set(ByVal value As String)
            _BranchCode = value
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

    Public Function bindData(kd_BioData As String, type As String) As DataTable
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
                    txtsql = " SELECT * FROM MSBioData with (nolock) where BioDataCode like '" & kd_BioData & "%' and BioDataCode not in ('pandu','admin')  ORDER BY BioDataCode"
                Else
                    txtsql = " SELECT * FROM MSBioData with (nolock) where BioDataName like '" & kd_BioData & "%' and BioDataCode not in ('pandu','admin')  ORDER BY BioDataName"
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
                
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function bindisiData(kd_BioData As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = " SELECT * FROM MSBioData with (nolock) where BioDataCode = '" & kd_BioData & "'  ORDER BY BioDataCode"
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

    Public Function InsertBioData() As DataTable
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                con.Open()
                Dim sql As String = ""
                sql = "dbo.SP_I_U_MSBioData"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure

                com.Parameters.Add("@BioDataCode", SqlDbType.VarChar).Value = BioDataCode
                com.Parameters.Add("@BioDataSalutation", SqlDbType.VarChar).Value = BioDataSalutation
                com.Parameters.Add("@BioDataName", SqlDbType.VarChar).Value = BioDataName
                com.Parameters.Add("@BioDataNickName", SqlDbType.VarChar).Value = BioDataNickName
                com.Parameters.Add("@BioDataAdd", SqlDbType.VarChar).Value = BioDataAdd
                com.Parameters.Add("@BioDataCity", SqlDbType.VarChar).Value = BioDataCity
                com.Parameters.Add("@BioDataZIP", SqlDbType.VarChar).Value = BioDataZIP
                com.Parameters.Add("@BioDataPhone", SqlDbType.VarChar).Value = BioDataPhone
                com.Parameters.Add("@BioDataFax", SqlDbType.VarChar).Value = BioDataFax
                com.Parameters.Add("@BioDataContact", SqlDbType.VarChar).Value = BioDataContact
                com.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IIf(IsActive.ToString = "True", 1, 0)
                com.Parameters.Add("@CRE_BY", SqlDbType.VarChar).Value = CRE_BY

                com.Parameters.Add("@BioDataGender", SqlDbType.VarChar).Value = BioDataGender
                com.Parameters.Add("@BioDataBirthdate", SqlDbType.Date).Value = BioDataBirthdate
                com.Parameters.Add("@BioDataEmail", SqlDbType.VarChar).Value = BioDataEmail
                com.Parameters.Add("@BioDataPict", SqlDbType.VarChar).Value = IIf(BioDataPict = Nothing, "", BioDataPict)
                com.Parameters.Add("@BioDataType", SqlDbType.VarChar).Value = BioDataType
                com.Parameters.Add("@BranchCode", SqlDbType.VarChar).Value = BranchCode

                Dim da As New SqlDataAdapter(com)
                Dim dt As New DataTable
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Return dt
                Else
                    Return dt
                End If

                'Dim rowSuccess As Integer = com.ExecuteNonQuery
                'If rowSuccess = 0 Then
                '    Return False
                '    Throw New Exception("Error -" & sql)
                'Else
                '    Return True
                'End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

End Class

