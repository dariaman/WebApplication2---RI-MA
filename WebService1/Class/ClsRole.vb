Imports System.Data.SqlClient
Public Class ClsRole
    Public RoleCode As String
    Public RoleDesc As String
    Public IsActive As String
    Public MenuCode As String
    Public Admin As String
    Public LvlAdmin As String
    Public CRE_BY As String

    Public Function InsertRole() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SP_I_U_MSRole '" & Me.RoleCode & "','" & Me.RoleDesc & "','" & Me.IsActive & "','" & Me.Admin & "','" & Me.LvlAdmin & "','" & Me.CRE_BY & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function PolicyMemberAdminOpen(loginrole As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "sp_s_PolicyMemberAdmin '" & Me.RoleCode & "'"
            Try

                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                If dr.HasRows = True Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function
    Public Function DeleteUserSource(MenuParent As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "sp_d_MSRoleaccess '" & IIf(MenuParent = "99", "", MenuParent) & "','" & Me.RoleCode & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function InsertRoleAccess(usrMenu As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "sp_I_MSRoleAccess '" & Me.RoleCode & "','" & usrMenu & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function bindDataRole(type As String, RoleCode As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_S_MSRole '" & RoleCode & "' ,'" & type & "'"
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

    Public Function bindData(rolecode As String, txtKeyword As String, BranchCode As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try

                    cmd.CommandText = "[dbo].[SP_S_MSUserList] '" & txtKeyword & "','True','" & rolecode & "','" & BranchCode & "'"
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

    Public Function bindDataPIC(rolecode As String, txtKeyword As String, BranchCode As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try

                    cmd.CommandText = "[dbo].[SP_S_MSUserListPIC] '" & txtKeyword & "','True','" & rolecode & "','" & BranchCode & "'"
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

    Public Function bindDataGVPIC(userid As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try

                    cmd.CommandText = "[dbo].[sp_s_MSLOGINDETAIL] '" & userid & "'"
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


    Public Function bindDataUnion(RoleCode As String, MenuParentCode As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "[dbo].[SP_S_MSMenuUnion] '" & RoleCode & "','" & MenuParentCode & "','True'"
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

    Public Function LoadDataUser(ByVal userId As String) As DataTable
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = "dbo.SP_S_MSUserListRole '" & userId & "','true'"
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                Return dt
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using

    End Function

    Public Function cekusr(email As String, isNew As Integer) As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                Dim cmd As New SqlCommand
                cmd.CommandText = "select * from msuser with (nolock) where email='" & email & "'"
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                con.Open()
                If isNew = 0 Then
                    Return "1"
                Else
                    Dim dr As SqlDataReader = cmd.ExecuteReader()
                    If dr.HasRows = True Then
                        Return "2"
                    Else
                        Return "3"
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function bindDataGroup(UserId As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "[dbo].[SP_S_MSGroupUNION] '" & UserId & "'"
                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable
                    da.Fill(dt)
                    Return dt
                Catch ex As Exception
                    Return Nothing
                End Try
                
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function bindDatabranch(UserId As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try

                    cmd.CommandText = "[dbo].[SP_S_MSBranchUNION] '" & UserId & "'"
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

    Public Function bindDataProduct(UserId As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try

                    cmd.CommandText = "[dbo].[SP_S_MSProdukUNION] '" & UserId & "'"
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

    Public Function validRole(RoleCode As String) As ArrayList
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("select admin,lvladmin from MsRole with (nolock) where RoleCode= '" & RoleCode & "' ", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Dim ar As New ArrayList
                While dr.Read()
                    ar.Add(dr(0).ToString)
                    ar.Add(dr(1).ToString)
                End While
                Return ar
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function
End Class
