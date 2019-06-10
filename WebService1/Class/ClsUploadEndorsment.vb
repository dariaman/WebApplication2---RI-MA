Imports System.Data.SqlClient
Public Class ClsUploadEndorsment
    Public NoReg As String
    Public DateReg As Date
    Public AddItem As Integer
    Public ChangePlan As Integer
    Public TerminatePlan As Integer
    Public AlterationPlan As Integer
    Public FileName As String
    Public Cre_By As String

    Public Function InsertREGISTER() As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SP_I_U_MSREGISTER '" & Me.NoReg & "','" & Format(Me.DateReg, "yyyy-MM-dd") & "','" & Me.AddItem & "','" & Me.ChangePlan & "','" & Me.TerminatePlan & "','" & Me.AlterationPlan & "','" & Me.FileName & "','" & Me.Cre_By & "'"
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

    Public Function bindData(NoReg As String, cre_by As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try

                    cmd.CommandText = "[dbo].[SP_S_MSREGISTER_NOREG] '" & NoReg & "','" & cre_by & "'"
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

    Public Function bindisiData(NoReg As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try

                    cmd.CommandText = "[dbo].[SP_S_MSREGISTER_NOREG] '" & NoReg & "','%'"
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
