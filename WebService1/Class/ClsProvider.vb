Imports System.Data.Sql
Imports System.Data.SqlClient

'create  by pandu
Public Class ClsProvider
#Region "property"

    Private _PROVIDERID As Long
    Private _PROVIDERNAME As String
    Private _PROVIDERTYPE As String
    Private _TYPE As String
    Private _BUILDING As String
    Private _STREET1 As String
    Private _STREET2 As String
    Private _STREET3 As String
    Private _COUNTRYID As String
    Private _PROVINCEID As String
    Private _CITYID As String
    Private _STATEID As String
    Private _DISTRICTID As String
    Private _ZIPCODE As String
    Private _PHONE1 As String
    Private _PHONE2 As String
    Private _FAX1 As String
    Private _FAX2 As String
    Private _EMAIL As String
    Private _PROVIDER As Boolean
    Private _CLAIMDUE As String
    Private _EFFDT As datetime
    Private _EXPDT As datetime
    Private _PAYTYPE As String
    Private _IP As Boolean
    Private _OP As Boolean
    Private _DT As Boolean
    Private _MT As Boolean
    Private _GL As Boolean
    Private _TPAADMEDIKA As Boolean
    Private _TPAGLOBAL As Boolean
    Private _STATUS As String
    Private _LATTITUDE As Decimal
    Private _LONGITUDE As Decimal
    Private _GRADE As String
    Private _REMARK As String

    Public Property PROVIDERID() As Long
        Get
            Return _PROVIDERID
        End Get
        Set(ByVal value As Long)
            _PROVIDERID = value
        End Set
    End Property

    Public Property PROVIDERTYPE() As String
        Get
            Return _PROVIDERTYPE
        End Get
        Set(ByVal value As String)
            _PROVIDERTYPE = value
        End Set
    End Property

    Public Property PROVIDERNAME() As String
        Get
            Return _PROVIDERNAME
        End Get
        Set(ByVal value As String)
            _PROVIDERNAME = value
        End Set
    End Property

    Public Property TYPE() As String
        Get
            Return _TYPE
        End Get
        Set(ByVal value As String)
            _TYPE = value
        End Set
    End Property

    Public Property BUILDING() As String
        Get
            Return _BUILDING
        End Get
        Set(ByVal value As String)
            _BUILDING = value
        End Set
    End Property

    Public Property STREET1() As String
        Get
            Return _STREET1
        End Get
        Set(ByVal value As String)
            _STREET1 = value
        End Set
    End Property

    Public Property STREET2() As String
        Get
            Return _STREET2
        End Get
        Set(ByVal value As String)
            _STREET2 = value
        End Set
    End Property

    Public Property STREET3() As String
        Get
            Return _STREET3
        End Get
        Set(ByVal value As String)
            _STREET3 = value
        End Set
    End Property

    Public Property COUNTRYID() As String
        Get
            Return _COUNTRYID
        End Get
        Set(ByVal value As String)
            _COUNTRYID = value
        End Set
    End Property

    Public Property PROVINCEID() As String
        Get
            Return _PROVINCEID
        End Get
        Set(ByVal value As String)
            _PROVINCEID = value
        End Set
    End Property

    Public Property CITYID() As String
        Get
            Return _CITYID
        End Get
        Set(ByVal value As String)
            _CITYID = value
        End Set
    End Property

    Public Property STATEID() As String
        Get
            Return _STATEID
        End Get
        Set(ByVal value As String)
            _STATEID = value
        End Set
    End Property

    Public Property DISTRICTID() As String
        Get
            Return _DISTRICTID
        End Get
        Set(ByVal value As String)
            _DISTRICTID = value
        End Set
    End Property

    Public Property ZIPCODE() As String
        Get
            Return _ZIPCODE
        End Get
        Set(ByVal value As String)
            _ZIPCODE = value
        End Set
    End Property

    Public Property PHONE1() As String
        Get
            Return _PHONE1
        End Get
        Set(ByVal value As String)
            _PHONE1 = value
        End Set
    End Property

    Public Property PHONE2() As String
        Get
            Return _PHONE2
        End Get
        Set(ByVal value As String)
            _PHONE2 = value
        End Set
    End Property

    Public Property FAX1() As String
        Get
            Return _FAX1
        End Get
        Set(ByVal value As String)
            _FAX1 = value
        End Set
    End Property

    Public Property FAX2() As String
        Get
            Return _FAX2
        End Get
        Set(ByVal value As String)
            _FAX2 = value
        End Set
    End Property

    Public Property EMAIL() As String
        Get
            Return _EMAIL
        End Get
        Set(ByVal value As String)
            _EMAIL = value
        End Set
    End Property

    Public Property PROVIDER() As Boolean
        Get
            Return _PROVIDER
        End Get
        Set(ByVal value As Boolean)
            _PROVIDER = value
        End Set
    End Property

    Public Property CLAIMDUE() As String
        Get
            Return _CLAIMDUE
        End Get
        Set(ByVal value As String)
            _CLAIMDUE = value
        End Set
    End Property

    Public Property EFFDT() As DateTime
        Get
            Return _EFFDT
        End Get
        Set(ByVal value As DateTime)
            _EFFDT = value
        End Set
    End Property

    Public Property EXPDT() As DateTime
        Get
            Return _EXPDT
        End Get
        Set(ByVal value As DateTime)
            _EXPDT = value
        End Set
    End Property

    Public Property PAYTYPE() As String
        Get
            Return _PAYTYPE
        End Get
        Set(ByVal value As String)
            _PAYTYPE = value
        End Set
    End Property

    Public Property IP() As Boolean
        Get
            Return _IP
        End Get
        Set(ByVal value As Boolean)
            _IP = value
        End Set
    End Property

    Public Property OP() As Boolean
        Get
            Return _OP
        End Get
        Set(ByVal value As Boolean)
            _OP = value
        End Set
    End Property

    Public Property DT() As Boolean
        Get
            Return _DT
        End Get
        Set(ByVal value As Boolean)
            _DT = value
        End Set
    End Property

    Public Property MT() As Boolean
        Get
            Return _MT
        End Get
        Set(ByVal value As Boolean)
            _MT = value
        End Set
    End Property

    Public Property GL() As Boolean
        Get
            Return _GL
        End Get
        Set(ByVal value As Boolean)
            _GL = value
        End Set
    End Property

    Public Property TPAADMEDIKA() As Boolean
        Get
            Return _TPAADMEDIKA
        End Get
        Set(ByVal value As Boolean)
            _TPAADMEDIKA = value
        End Set
    End Property


    Public Property TPAGLOBAL() As Boolean
        Get
            Return _TPAGLOBAL
        End Get
        Set(ByVal value As Boolean)
            _TPAGLOBAL = value
        End Set
    End Property

    Public Property STATUS() As String
        Get
            Return _STATUS
        End Get
        Set(ByVal value As String)
            _STATUS = value
        End Set
    End Property

    Public Property LATTITUDE() As Decimal
        Get
            Return _LATTITUDE
        End Get
        Set(ByVal value As Decimal)
            _LATTITUDE = value
        End Set
    End Property

    Public Property LONGITUDE() As Decimal
        Get
            Return _LONGITUDE
        End Get
        Set(ByVal value As Decimal)
            _LONGITUDE = value
        End Set
    End Property

    Public Property GRADE() As String
        Get
            Return _GRADE
        End Get
        Set(ByVal value As String)
            _GRADE = value
        End Set
    End Property

    Public Property REMARK() As String
        Get
            Return _REMARK
        End Get
        Set(ByVal value As String)
            _REMARK = value
        End Set
    End Property

#End Region



    Public Function bindData1(PROVIDERID As String, PROVIDERTYPE As String, SEARCH As String, POLICYNO As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim txtsql As String = " Sp_S_PRV_PROVIDER_MASTER '" & PROVIDERID & "' ,'" & PROVIDERTYPE & "' ,'" & SEARCH & "','" & POLICYNO & "'"
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

    Public Function bindData(RangeRS As String, lat1 As String, longt1 As String, group As String, POLICYNO As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim txtsql As String = " Sp_S_PRV_PROVIDER_MASTER_REL " & RangeRS & " ," & lat1 & " ," & longt1 & ",'" & group & "','" & POLICYNO & "'"
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

    Public Function bindisiData(PROVIDERID As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = " Sp_S_PRV_PROVIDER_MASTER_ '" & PROVIDERID & "' "

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

    Public Function InsertProvider() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                con.Open()
                Dim sql As String = ""
                sql = "dbo.SP_I_U_MSPROVIDERGROUP"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure

                com.Parameters.Add("@PROVIDERID", SqlDbType.VarChar).Value = PROVIDERID
                com.Parameters.Add("@PROVIDERTYPE", SqlDbType.VarChar).Value = PROVIDERTYPE
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

    Public Function GETTPAID(POLICYNO As String, TPAID As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                con.Open()
                Dim sql As String = ""
                sql = "dbo.SP_S_TPAID"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure

                com.Parameters.Add("@POLICYNO", SqlDbType.VarChar).Value = POLICYNO
                com.Parameters.Add("@TPAID", SqlDbType.VarChar).Value = TPAID
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

    Public Function DeleteProvider() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                con.Open()
                Dim sql As String = ""
                sql = "dbo.SP_D_MSPROVIDER"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure

                com.Parameters.Add("@PROVIDERID", SqlDbType.VarChar).Value = PROVIDERID
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

