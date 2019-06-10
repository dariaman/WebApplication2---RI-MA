Imports System.Data.SqlClient
Public Class ClsTiket
    Public NoTiket As String
    Public Desc_Tiket As String
    Public pict As String
    Public ext As String
    Public OriFile As String
    Public Note As String
    Public isActive As Boolean
    Public isSend As Boolean
    Public Cre_By As String
    Public Size As Integer

    Public Function bindData(cre_by As String, NoTiket As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try

                    cmd.CommandText = "[dbo].[SP_S_TRXTIKET] '" & cre_by & "','" & NoTiket & "'"
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

    Public Function FindActive(cre_by As String, NoTiket As String) As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "[dbo].[SP_S_TRXTIKET_Active] '" & cre_by & "','" & NoTiket & "'"
            Try
                con.Open()

                Return cmd.ExecuteScalar()
            Catch ex As Exception
                Return Nothing
                'Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function bindDataPict(NoTiket As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try

                    cmd.CommandText = "[dbo].[sp_S_TRXDETAILTIKET] '" & NoTiket & "'"
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

    Public Function autoinc(cre_by As String, TglBerobat As String, NoPolis As String, MemberId As String, TotalClaim As Decimal) As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "[dbo].[sp_s_autoinctiket] '" & cre_by & "','" & Format(CDate(TglBerobat), "MM/dd/yyyy") & "','" & NoPolis & "','" & MemberId & "','" & TotalClaim & "'"
            Try
                con.Open()

                Return cmd.ExecuteScalar()
            Catch ex As Exception
                Return Nothing
                'Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function delete(NoTiket As String, Pict As String, mod_by As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "[dbo].[sp_D_TRXDETAILTIKET] '" & NoTiket & "','" & Pict & "','" & Pict & "'"
            Try
                con.Open()

                Dim rowSuccess As Integer = cmd.ExecuteNonQuery
                If rowSuccess = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Return False

            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function deleteAll(NoTiket As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "[dbo].[sp_D_TRXDETAILTIKET_all] '" & NoTiket & "'"
            Try
                con.Open()

                Dim rowSuccess As Integer = cmd.ExecuteNonQuery
                If rowSuccess = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Return False

            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function updatePict(isupdate As String, value As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "[dbo].[sp_u_TRXTIKET] '" & Me.Cre_By & "','" & Me.NoTiket & "','" & isupdate & "','" & value & "'"
            Try
                con.Open()

                Dim rowSuccess As Integer = cmd.ExecuteNonQuery
                If rowSuccess = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Return False

            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function insertPict() As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "[dbo].[sp_i_TRXDETAILTIKET] '" & Me.NoTiket & "','" & Me.ext & "','" & Me.OriFile & "','" & Me.Note & "','" & Me.Cre_By & "','" & Me.Size & "'"
            Try
                con.Open()

                'Dim rowSuccess As Integer = cmd.ExecuteNonQuery
                'If rowSuccess = 0 Then
                '    Return False
                'Else
                '    Return True
                'End If
                Return cmd.ExecuteScalar()

            Catch ex As Exception
                Return Nothing

            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function selectSize(ByVal NoTiket As String) As Integer
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "[dbo].[SP_S_TRXDETAILTIKETSIZE] '" & NoTiket & "'"
            Try
                con.Open()

                Return cmd.ExecuteScalar()

            Catch ex As Exception
                Return 0

            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function EffExpDate(NoPolis As String, MemberId As String) As DataTable
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select EFFDT,EXPDTPOL from dbo.Vw_Member_Info where policyno='" & NoPolis & "' and membid='" & MemberId & "'"
            Try
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                Return dt
            Catch ex As Exception
                Return Nothing
                'Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

End Class
