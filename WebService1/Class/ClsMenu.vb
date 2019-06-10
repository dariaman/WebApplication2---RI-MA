Imports System.Data.Sql
Imports System.Data.SqlClient

'create  by pandu
Public Class ClsMenu

    Public Function MenuDetail(ByVal ROLECODE As String, ByVal MENUPARENTCODE As String, ByVal ISACTIVE As String) As DataSet
        Dim txtsql As String = "SP_S_MSMENUUNION '" & ROLECODE & "','" & MENUPARENTCODE & "','" & ISACTIVE & "'"
        Dim con As New SqlConnection(config.MSSQLConnection)
        Dim DtSet As New DataSet
        Dim MyCommand As New SqlDataAdapter(txtsql, con)
        Try
            MyCommand.Fill(DtSet)
            Return DtSet
        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
        End Try
    End Function

    Public Function BindMenu(ByVal roleId As String, ByVal UserId As String, ByVal Group As String) As String
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim com As New SqlCommand
            com.Connection = con
            REM load Menu Header
            Dim txtsql As String = "SP_S_MSRoleAccess '" & UserId & "','True','Web','" & Group & "'"
            Dim dadCategories As New SqlDataAdapter(txtsql, con)

            REM Load Menu BY USER Id
            txtsql = "SP_S_MSRoleAccessDetail '" & UserId & "','True','Web',1,'" & Group & "'"
            Dim dadSubCat As New SqlDataAdapter(txtsql, con)

            ' Add the DataTables to the DataSet
            Dim dsCat As New DataSet()
            Using con
                con.Open()
                dadCategories.Fill(dsCat, "MsMenuHeader")
                dadSubCat.Fill(dsCat, "MsMenuDetail")
            End Using

            dsCat.Relations.Add("Children", dsCat.Tables("MsMenuHeader").Columns("MenuParentCode"), dsCat.Tables("MsMenuDetail").Columns("MenuParentCode"))
            Dim strmenuheader As String = ""
            Dim strmenu As String, strmenu1 As String = ""
            Dim strmenucat As String = ""

            For Each categoryRow As DataRow In dsCat.Tables("MsMenuHeader").Rows

                ' Get matching Sub Category
                strmenucat = ""
                Dim subCatRows() As DataRow = categoryRow.GetChildRows("Children")
                For Each row As DataRow In subCatRows
                    strmenucat = strmenucat & vbCrLf & "<li><a href='" & CType(row("WebName"), String) & "'><i class='fa " & CType(row("PictURL"), String) & " fa-fw'></i>" & CType(row("ChildsCaption"), String) & "</a></li>"
                Next
                strmenu = ""
                strmenu = "<li><a href='#'><i class='fa " & CType(categoryRow("PictHeader"), String) & " fa-fw'></i> " & CType(categoryRow("MenuType"), String) & " <span class='fa fa-angle-left pull-right'></span></a><ul class='treeview-menu'>" & vbCrLf & _
                                    strmenucat & _
                            "</ul></li>"
                strmenu1 = strmenu1 & vbCrLf & strmenu
            Next
            strmenuheader = "<li><a href='#'><i class='fa  fa-folder-o fa-fw'></i> " & Group & "<span class='fa fa-angle-left pull-right'></span></a>" & _
                            "<ul class='treeview-menu'>" & vbCrLf & _
                        strmenu1 & vbCrLf & _
                        "</ul></li>"
            If con.State = ConnectionState.Open Then con.Close()
            Return strmenuheader
        End Using
    End Function

    'Public Function Bindbreadcrumb(ByVal roleId As String, ByVal UserId As String, ByVal Group As String) As String
    '    Using con As New SqlConnection(config.MSSQLConnection)
    '        Dim com As New SqlCommand
    '        com.Connection = con
    '        REM load Menu Header
    '        Dim txtsql As String = "SP_S_MSRoleAccess '" & UserId & "','True','Web','" & Group & "'"
    '        Dim dadCategories As New SqlDataAdapter(txtsql, con)

    '        REM Load Menu BY USER Id
    '        txtsql = "SP_S_MSRoleAccessDetail '" & UserId & "','True','Web',1,'" & Group & "'"
    '        Dim dadSubCat As New SqlDataAdapter(txtsql, con)

    '        ' Add the DataTables to the DataSet
    '        Dim dsCat As New DataSet()
    '        Using con
    '            con.Open()
    '            dadCategories.Fill(dsCat, "MsMenuHeader")
    '            dadSubCat.Fill(dsCat, "MsMenuDetail")
    '        End Using

    '        dsCat.Relations.Add("Children", dsCat.Tables("MsMenuHeader").Columns("MenuParentCode"), dsCat.Tables("MsMenuDetail").Columns("MenuParentCode"))
    '        Dim strmenuheader As String = ""
    '        Dim strmenu As String, strmenu1 As String = ""
    '        Dim strmenucat As String = ""

    '        For Each categoryRow As DataRow In dsCat.Tables("MsMenuHeader").Rows

    '            ' Get matching Sub Category
    '            strmenucat = ""
    '            Dim subCatRows() As DataRow = categoryRow.GetChildRows("Children")
    '            For Each row As DataRow In subCatRows
    '                strmenucat = strmenucat & vbCrLf & "<li><a href='" & CType(row("WebName"), String) & "'><i class='fa " & CType(row("PictURL"), String) & " fa-fw'></i>" & CType(row("ChildsCaption"), String) & "</a></li>"
    '            Next
    '            strmenu = ""
    '            strmenu = "<li><a href='#'><i class='fa " & CType(categoryRow("PictHeader"), String) & " fa-fw'></i> " & CType(categoryRow("MenuType"), String) & " <span class='fa fa-angle-left pull-right'></span></a>" & vbCrLf & _
    '                                strmenucat & _
    '                        "</li>"
    '            strmenu1 = strmenu1 & vbCrLf & strmenu
    '        Next
    '        strmenuheader = "<li><a href='#'><i class='fa  fa-folder-o fa-fw'></i> " & Group & "<span class='fa fa-angle-left pull-right'></span></a>" & _
    '                        "" & vbCrLf & _
    '                    strmenu1 & vbCrLf & _
    '                    "</li>"

    '        Return strmenuheader
    '    End Using
    'End Function

    Public Function bindGroup(Userid As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Dim txtsql As String
                txtsql = "[SP_S_MSMenu] '" & Userid & "','True'"
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
End Class
