Imports System.Data.Sql
Imports System.Data.SqlClient

'create  by pandu
Public Class ClsProduct

#Region "property"

    Private _ID As String
    Private _TypeProduk As String
    Private _DESCRIPTION As String
    Private _Product As String
    Private _ProdukPlus As Boolean
    Private _CRE_BY As String
    Private _CRE_IP As String
    Private _IsActive As Boolean
    Private _UnitCode As String
    Private _price As Double
    Private _priceSale As Double
    Private _itemStock As Integer

    Public Property TypeProduk() As String
        Get
            Return _TypeProduk
        End Get
        Set(ByVal value As String)
            _TypeProduk = value
        End Set
    End Property

    Public Property ID() As String
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
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

    Public Property ProdukPlus() As Boolean
        Get
            Return _ProdukPlus
        End Get
        Set(ByVal value As Boolean)
            _ProdukPlus = value
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

    Public Property IsActive() As Boolean
        Get
            Return _IsActive
        End Get
        Set(ByVal value As Boolean)
            _IsActive = value
        End Set
    End Property

    Public Property UnitCode() As String
        Get
            Return _UnitCode
        End Get
        Set(ByVal value As String)
            _UnitCode = value
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
                sql = "dbo.SP_I_U_MSProduk"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = ID
                com.Parameters.Add("@TypeProduk", SqlDbType.VarChar).Value = TypeProduk
                com.Parameters.Add("@DESCRIPTION ", SqlDbType.VarChar).Value = DESCRIPTION
                com.Parameters.Add("@Produk ", SqlDbType.VarChar).Value = Product
                com.Parameters.Add("@CRE_BY ", SqlDbType.VarChar).Value = CRE_BY
                com.Parameters.Add("@CRE_IP ", SqlDbType.VarChar).Value = CRE_IP
                com.Parameters.Add("@IsActive ", SqlDbType.VarChar).Value = IsActive
                com.Parameters.Add("@price ", SqlDbType.Money).Value = price
                com.Parameters.Add("@priceSale ", SqlDbType.Money).Value = priceSale
                com.Parameters.Add("@itemStock ", SqlDbType.Int).Value = itemStock
                com.Parameters.Add("@ProdukPlus ", SqlDbType.Bit).Value = ProdukPlus
                com.Parameters.Add("@UnitCode ", SqlDbType.VarChar).Value = UnitCode
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
                    txtsql = " SELECT [typeProduk],MP.[DESCRIPTION],Produk,UnitType,MP.[IsActive],[ID],isnull(cast(price as decimal(10,2)),0)price,isnull(cast(pricesale as decimal(10,2)),0)pricesale,isnull(itemstock ,0)itemstock FROM [dbo].[MSProduk] MP with (nolock) Left join MsUnit MU with (nolock) on MU.UnitCode=MP.UnitCode where [typeProduk] like '" & Product & "%'  ORDER BY [typeProduk]"
                ElseIf type = "Product" Then
                    txtsql = " SELECT [typeProduk],MP.[DESCRIPTION],Produk,UnitType,MP.[IsActive],[ID],isnull(cast(price as decimal(10,2)),0)price,isnull(cast(pricesale as decimal(10,2)),0)pricesale,isnull(itemstock ,0)itemstock FROM [dbo].[MSProduk] MP with (nolock) Left join MsUnit MU with (nolock) on MU.UnitCode=MP.UnitCode where [Produk] like '" & Product & "%'  ORDER BY [Produk]"
                Else
                    txtsql = " SELECT [TypeProduk],MP.[DESCRIPTION],Produk,UnitType,MP.[IsActive],[ID],isnull(cast(price as decimal(10,2)),0)price,isnull(cast(pricesale as decimal(10,2)),0)pricesale,isnull(itemstock ,0)itemstock FROM [dbo].[MSProduk] MP with (nolock) Left join MsUnit MU with (nolock) on MU.UnitCode=MP.UnitCode where mp.[DESCRIPTION] like '" & Product & "%'  ORDER BY mp.[DESCRIPTION]"
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
                cmd.CommandText = "SELECT [ID],[typeProduk],[DESCRIPTION],Produk,UnitCode,[IsActive],ProdukPlus,isnull(cast(price as decimal(10,2)),0)price,isnull(cast(pricesale as decimal(10,2)),0)pricesale,isnull(itemstock ,0)itemstock  FROM [dbo].[MSProduk] with (nolock)  where [ID]= '" & ID & "'  ORDER BY typeProduk"
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                Return dt
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function bindiRptData(Product As String, type As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = "SELECT [ID],[typeProduk],[DESCRIPTION],Produk,UnitCode,[IsActive],ProdukPlus,isnull(cast(price as decimal(10,2)),0)price,isnull(cast(pricesale as decimal(10,2)),0)pricesale,isnull(itemstock ,0)itemstock  FROM [dbo].[MSProduk] with (nolock) where typeProduk= '" & type & "'and Produk like '" & Product & "%'  ORDER BY typeProduk"
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                Return dt
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function bindDataAccess(Product As String, type As String, UserId As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim txtsql As String = ""
                If type = "Kode" Then
                    txtsql = " SELECT MP.[TypeProduk],MP.[DESCRIPTION],MP.Produk,MP.[IsActive],MU.UnitType,MP.[ID],isnull(cast(MP.price as decimal(10,2)),0)price,isnull(cast(MP.pricesale as decimal(10,2)),0)pricesale,isnull(MP.itemstock ,0)itemstock FROM [dbo].[MSProduk] MP with (nolock) left join MSPRODUKACCESS MAP on MAP.TYPEPRODUK=MP.TYPEPRODUK Left join MsUnit MU with (nolock) on MU.UnitCode=MP.UnitCode where MP.[typeProduk] like '" & Product & "%' and MAP.UserId='" & UserId & "'  ORDER BY MP.[typeProduk]"
                ElseIf type = "Product" Then
                    txtsql = " SELECT MP.[TypeProduk],MP.[DESCRIPTION],MP.Produk,MP.[IsActive],MU.UnitType,MP.[ID],isnull(cast(MP.price as decimal(10,2)),0)price,isnull(cast(MP.pricesale as decimal(10,2)),0)pricesale,isnull(MP.itemstock ,0)itemstock FROM [dbo].[MSProduk] MP with (nolock) left join MSPRODUKACCESS MAP on MAP.TYPEPRODUK=MP.TYPEPRODUK Left join MsUnit MU with (nolock) on MU.UnitCode=MP.UnitCode where MP.[Produk] like '" & Product & "%' and MAP.UserId='" & UserId & "'  ORDER BY MP.[Produk]"
                Else
                    txtsql = " SELECT MP.[TypeProduk],MP.[DESCRIPTION],MP.Produk,MP.[IsActive],MU.UnitType,MP.[ID],isnull(cast(MP.price as decimal(10,2)),0)price,isnull(cast(MP.pricesale as decimal(10,2)),0)pricesale,isnull(MP.itemstock ,0)itemstock FROM [dbo].[MSProduk] MP with (nolock) left join MSPRODUKACCESS MAP on MAP.TYPEPRODUK=MP.TYPEPRODUK Left join MsUnit MU with (nolock) on MU.UnitCode=MP.UnitCode where MP.[DESCRIPTION] like '" & Product & "%' and MAP.UserId='" & UserId & "'  ORDER BY MP.[DESCRIPTION]"
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
End Class
