Imports System.Data.Sql
Imports System.Data.SqlClient

'create  by pandu
Public Class ClsProductGroup

#Region "property"

    Private _typeProduk As String
    Private _DESCRIPTION As String
    Private _Product As String
    Private _ProductPLUS As Boolean
    Private _CRE_BY As String
    Private _CRE_IP As String
    Private _IsActive As String
    Private _price As Double
    Private _priceSale As Double
    Private _itemStock As Integer

    Public Property typeProduk() As String
        Get
            Return _typeProduk
        End Get
        Set(ByVal value As String)
            _typeProduk = value
        End Set
    End Property

    Public Property DESCRIPTION() As String
        Get
            Return _DESCRIPTION
        End Get
        Set(ByVal value As String)
            _DESCRIPTION = value
        End Set
    End Property

    Public Property Product() As String
        Get
            Return _Product
        End Get
        Set(ByVal value As String)
            _Product = value
        End Set
    End Property

    Public Property ProductPLUS() As Boolean
        Get
            Return _ProductPLUS
        End Get
        Set(ByVal value As Boolean)
            _ProductPLUS = value
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

    Public Property CRE_IP() As String
        Get
            Return _CRE_IP
        End Get
        Set(ByVal value As String)
            _CRE_IP = value
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

    Public Property price() As Double
        Get
            Return _price
        End Get
        Set(ByVal value As Double)
            _price = value
        End Set
    End Property

    Public Property priceSale() As Double
        Get
            Return _priceSale
        End Get
        Set(ByVal value As Double)
            _priceSale = value
        End Set
    End Property

    Public Property itemStock() As Integer
        Get
            Return _itemStock
        End Get
        Set(ByVal value As Integer)
            _itemStock = value
        End Set
    End Property
#End Region


    Public Function InsertProduct() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                con.Open()
                Dim sql As String = ""
                sql = "dbo.SP_I_U_MSProdukGroup"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure
                com.Parameters.Add("@typeProduk", SqlDbType.VarChar).Value = typeProduk
                com.Parameters.Add("@DESCRIPTION ", SqlDbType.VarChar).Value = DESCRIPTION
                com.Parameters.Add("@CRE_BY ", SqlDbType.VarChar).Value = CRE_BY
                com.Parameters.Add("@CRE_IP ", SqlDbType.VarChar).Value = CRE_IP
                com.Parameters.Add("@IsActive ", SqlDbType.VarChar).Value = IsActive
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
    Public Function bindData(Product As String, type As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim txtsql As String = ""
                If type = "Kode" Then
                    txtsql = " SELECT [typeProduk],[DESCRIPTION],IsActive FROM [dbo].[MSProdukGroup] with (nolock) where [typeProduk] like '" & Product & "%'  ORDER BY [typeProduk]"
                Else
                    txtsql = " SELECT [typeProduk],[DESCRIPTION],IsActive FROM [dbo].[MSProdukGroup] with (nolock) where [DESCRIPTION] like '" & Product & "%'  ORDER BY [DESCRIPTION]"
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
                cmd.CommandText = "SELECT [typeProduk],[DESCRIPTION],IsActive FROM [dbo].[MSProdukGroup] with (nolock) where [typeProduk]= '" & ID & "'  ORDER BY typeProduk"
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
