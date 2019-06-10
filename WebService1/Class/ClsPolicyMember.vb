Imports System.Data.Sql
Imports System.Data.SqlClient

'create  by pandu
Public Class ClsPolicyMember
#Region "property"

    Private _CLIENTCODE As String
    Private _CLIENTNAME As String
    Private _POLICYNO As String
    Private _MEMBID As String
    Private _FULLNAME As String
    Private _SEX As String
    Private _BIRTHDATE As Date
    Private _RELSHIPID As String
    Private _RELSHIPNM2 As String
    Private _USERFIELD1 As String
    Private _USERFIELD2 As String
    Private _USERFIELD3 As String
    Private _EMPLOYEEID As String
    Private _EFFDT As Date
    Private _EXPDT As Date
    Private _STATUS As String
    Private _TERMDT As Date
    Private _EMPNAME As String
    Private _REMARK As String
    Private _CLIENTGROUP As String
    Private _ACCOUNTNM As String
    Private _ACCOUNTNO As String
    Private _BANKDESC As String
    Private _BRANCH As String
    Private _EDITDT As Date

    Public Property CLIENTCODE() As String
        Get
            Return _CLIENTCODE
        End Get
        Set(ByVal value As String)
            _CLIENTCODE = value
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

    Public Property MEMBID() As String
        Get
            Return _MEMBID
        End Get
        Set(ByVal value As String)
            _MEMBID = value
        End Set
    End Property
    Public Property FULLNAME() As String
        Get
            Return _FULLNAME
        End Get
        Set(ByVal value As String)
            _FULLNAME = value
        End Set
    End Property

    Public Property SEX() As String
        Get
            Return _SEX
        End Get
        Set(ByVal value As String)
            _SEX = value
        End Set
    End Property

    Public Property BIRTHDATE() As Date
        Get
            Return _BIRTHDATE
        End Get
        Set(ByVal value As Date)
            _BIRTHDATE = value
        End Set
    End Property

    Public Property RELSHIPID() As String
        Get
            Return _RELSHIPID
        End Get
        Set(ByVal value As String)
            _RELSHIPID = value
        End Set
    End Property

    Public Property RELSHIPNM2() As String
        Get
            Return _RELSHIPNM2
        End Get
        Set(ByVal value As String)
            _RELSHIPNM2 = value
        End Set
    End Property

    Public Property USERFIELD1() As String
        Get
            Return _USERFIELD1
        End Get
        Set(ByVal value As String)
            _USERFIELD1 = value
        End Set
    End Property

    Public Property USERFIELD2() As String
        Get
            Return _USERFIELD2
        End Get
        Set(ByVal value As String)
            _USERFIELD2 = value
        End Set
    End Property

    Public Property USERFIELD3() As String
        Get
            Return _USERFIELD3
        End Get
        Set(ByVal value As String)
            _USERFIELD3 = value
        End Set
    End Property

    Public Property EMPLOYEEID() As String
        Get
            Return _EMPLOYEEID
        End Get
        Set(ByVal value As String)
            _EMPLOYEEID = value
        End Set
    End Property
    Public Property EFFDT() As Date
        Get
            Return _EFFDT
        End Get
        Set(ByVal value As Date)
            _EFFDT = value
        End Set
    End Property

    Public Property EXPDT() As Date
        Get
            Return _EXPDT
        End Get
        Set(ByVal value As Date)
            _EXPDT = value
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

    Public Property TERMDT() As Date
        Get
            Return _TERMDT
        End Get
        Set(ByVal value As Date)
            _TERMDT = value
        End Set
    End Property

    Public Property EMPNAME() As String
        Get
            Return _EMPNAME
        End Get
        Set(ByVal value As String)
            _EMPNAME = value
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

    Public Property CLIENTGROUP() As String
        Get
            Return _CLIENTGROUP
        End Get
        Set(ByVal value As String)
            _CLIENTGROUP = value
        End Set
    End Property

    Public Property ACCOUNTNM() As String
        Get
            Return _ACCOUNTNM
        End Get
        Set(ByVal value As String)
            _ACCOUNTNM = value
        End Set
    End Property

    Public Property ACCOUNTNO() As String
        Get
            Return _ACCOUNTNO
        End Get
        Set(ByVal value As String)
            _ACCOUNTNO = value
        End Set
    End Property

    Public Property BANKDESC() As String
        Get
            Return _BANKDESC
        End Get
        Set(ByVal value As String)
            _BANKDESC = value
        End Set
    End Property

    Public Property EDITDT() As Date
        Get
            Return _EDITDT
        End Get
        Set(ByVal value As Date)
            _EDITDT = value
        End Set
    End Property

#End Region
    Public Function bindDataLOGINDETAIL_Policyno(USERID As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim kode As String = ""
                Dim lokasi As String = ""
                Dim txtsql As String = " sp_s_MSLOGINDETAIL_Policyno '" & USERID & "' "
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

    Public Function bindDataClaim_Info_Header_value(type As String, value As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim kode As String = ""
                Dim lokasi As String = ""
                Dim txtsql As String = " sp_s_Vw_Claim_Info_Header_value '" & type & "','" & value & "' "

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

    Public Function bindDataClaim_Info_Header_Policyno(type As String, value As String, policyno As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim kode As String = ""
                Dim lokasi As String = ""
                Dim txtsql As String = " sp_s_Vw_Claim_Info_Header_Policyno '" & type & "','" & value & "','" & policyno & "' "
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

    Public Function bindDataClaim_Info_Header_Policyno_excess(type As String, value As String, policyno As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim kode As String = ""
                Dim lokasi As String = ""
                Dim txtsql As String = " sp_s_Vw_Claim_Info_Header_Policyno_excess '" & type & "','" & value & "','" & policyno & "' "
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


    Public Function bindDataClaim_Info_Header_Policyno_excess_value(type As String, value As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim kode As String = ""
                Dim lokasi As String = ""
                Dim txtsql As String = " sp_s_Vw_Claim_Info_Header_Policyno_excess_value '" & type & "','" & value & "'"
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
    Public Function bindData(type As String, value As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim kode As String = ""
                Dim lokasi As String = ""
                Dim txtsql As String = " sp_s_vw_member_info_type '" & type & "', '" & value & "' "
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

    Public Function bindDatapolicyno(type As String, value As String, policyno As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim kode As String = ""
                Dim lokasi As String = ""
                Dim txtsql As String = " sp_s_vw_member_info_policyno '" & type & "', '" & value & "' , '" & policyno & "' "
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

    Public Function bindisiData(policyno As String, nemberid As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = " sp_s_vw_member_info_dtl '" & policyno & "', '" & nemberid & "' "
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

    Public Function bindDataClaimInfoHeader(policyno As String, nemberid As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = " sp_s_Vw_Claim_Info_Header '" & policyno & "', '" & nemberid & "' "
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

    Public Function bindDataClaimInfoDetail(claimno As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = " sp_s_Vw_Claim_Info_detail '" & claimno & "' "

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

    Public Function bindDataClaimInfoDetailExcess(claimno As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = " sp_s_Vw_Claim_Info_Detail_EXCESS '" & claimno & "' "

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

    Public Function bindDataBenefitInfo(policyno As String, nemberid As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = " select * from dbo.FC_Remain_Benefit( '" & policyno & "', '" & nemberid & "') "

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

End Class

