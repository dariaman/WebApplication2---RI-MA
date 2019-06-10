
Imports System.Data.SqlClient
Public Class ClsLimit
    Public POLICYNO As String
    Public Limit As String
    Public IsActive As String
    Public CRE_BY As String

    Public Function InsertLimit() As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SP_I_U_MSLIMIT '" & Me.POLICYNO & "','" & Me.Limit & "','" & Me.IsActive & "','" & Me.CRE_BY & "'"
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

    Public Function LimitPolicy() As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SP_S_MSLIMITPOLICY '" & Me.POLICYNO & "'"
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

    Public Function PolicyClient() As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from VW_POL_POLICY_DTL where policyno='" & Me.POLICYNO & "'"
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

    Public Function bindDataLimit() As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try
                    cmd.CommandText = "SP_S_MSLIMIT '" & Me.POLICYNO & "' "
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
