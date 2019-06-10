Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net.Mail
Imports System.Data.OleDb
Imports System.Data
Public Class sama

    Public Function UpdateActive(tbl As String, FieldActive As String, Active As String, FieldFilter As String, Filters As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                con.Open()
                Dim sql As String = ""
                sql = "dbo.sp_updatetable"
                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure
                com.Parameters.Add("@tbl", SqlDbType.VarChar).Value = tbl
                com.Parameters.Add("@FieldActive", SqlDbType.VarChar).Value = FieldActive
                com.Parameters.Add("@Active", SqlDbType.VarChar).Value = Active
                com.Parameters.Add("@FieldFilter", SqlDbType.VarChar).Value = FieldFilter
                com.Parameters.Add("@Filter", SqlDbType.VarChar).Value = Filters
                'cmd.CommandText = txtsql
                Dim rowSuccess As Integer = com.ExecuteNonQuery
                If rowSuccess = 0 Then
                    Return False
                    Throw New Exception("Error - " & sql)
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

    Public Function isiProviderType() As DataTable

        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                cmd.CommandText = " select * from dbo.MSPROVIDERTYPE()  "
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

    Public Sub isiddlMSUnit(ByVal Nameddl As DropDownList)


        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("select * from dbo.MSunit with (nolock)where isactive='true'", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                Nameddl.Items.Insert(0, New ListItem("Select..", "99"))
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlMSSalutation(ByVal Nameddl As DropDownList)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("select * from dbo.MSSALUTATION()", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                'Nameddl.Items.Insert(0, New ListItem("Select..", "99"))
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlMSProductTypeProduct(ByVal Nameddl As DropDownList)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("select * from dbo.MSProdukTypeProduct('')", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                Nameddl.Items.Insert(0, New ListItem("Select..", "99"))
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub
    'pake
    Public Function MenuAccess(ByVal FormName As String, ByVal userid As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                Dim cmd As New SqlCommand
                cmd.CommandText = "sp_s_MSroleAccessCekMenu '" & FormName & "','" & userid & "'"
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Dim strMenu As String = ""
                If dr.HasRows Then
                    If dr.Read Then
                        strMenu = dr("MenuCode").ToString
                    End If
                End If
                If strMenu = "" Then
                    Return False
                Else
                    Return True
                End If
                Return strMenu
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using

    End Function

    'blom kepake
    Public Function ConvertDateCalendar(DateConv As DateTime, Calendar As String, DateLangCulture As String) As String
        Try
            Dim DTFormat As DateTimeFormatInfo
            DateLangCulture = DateLangCulture.ToLower()
            If Calendar = "Hijri" AndAlso DateLangCulture.StartsWith("en-") Then
                DateLangCulture = "ar-sa"
            End If
            DTFormat = New System.Globalization.CultureInfo(DateLangCulture, False).DateTimeFormat

            Select Case Calendar
                Case "Hijri"
                    DTFormat.Calendar = New System.Globalization.HijriCalendar()
                    Exit Select
                Case "Gregorian"
                    DTFormat.Calendar = New System.Globalization.GregorianCalendar()
                    Exit Select
                Case Else
                    Return ""
            End Select
            DTFormat.ShortDatePattern = "dd/MM/yyyy"
            Return (DateConv.Date.ToString("f", DTFormat))

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Selectdate() As DateTime
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                Dim cmd As New SqlCommand
                cmd.CommandText = "select GETDATE() as startdt"
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Dim startdt As DateTime
                If dr.HasRows Then
                    If dr.Read Then
                        startdt = dr("startdt").ToString
                    End If
                End If
                Return startdt
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Sub GetBranchAccess(ByVal Nameddl As DropDownList, ByVal UserId As String)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("sp_s_MSBranchAccess '" & UserId & "'", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
                If Nameddl.Items.Count < 3 Then
                    Nameddl.Items.RemoveAt(0)
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub GetProductAccess(ByVal Nameddl As DropDownList, ByVal UserId As String)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("sp_s_MSProdukAccess '" & UserId & "'", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
                If Nameddl.Items.Count < 3 Then
                    Nameddl.Items.RemoveAt(0)
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlMenuParent(ByVal Nameddl As DropDownList)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("SELECT MenuParentCode,MenuParentCode +' - '+MenuType MenuType,[IsActive] FROM MSMenuParent with (nolock) where IsActive='True'  order by MenuParentCode", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                Nameddl.Items.Insert(0, New ListItem("Select..", "99"))
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlQuestion(ByVal Nameddl As DropDownList)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("select * from dbo.MSQuestion()  order by Question", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                Nameddl.Items.Insert(0, New ListItem("Select..", "99"))
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlRoleDesc(ByVal Nameddl As DropDownList)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("select * from dbo.MSRoleDesc()", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                Nameddl.Items.Insert(0, New ListItem("Select..", "99"))
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlMSCurrency(ByVal Nameddl As DropDownList)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("select * from dbo.MSCurrency()", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                Nameddl.Items.Insert(0, New ListItem("Select..", "99"))
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(0).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlMsGroup(ByVal Nameddl As DropDownList, usrid As String)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("sp_s_MsGroup_userid '" & usrid & "'", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                Nameddl.Items.Insert(0, New ListItem("Select..", "99"))
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
                If Nameddl.Items.Count = 2 Then
                    Nameddl.Items.RemoveAt(0)
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlmsProductGroup(ByVal Nameddl As DropDownList, TypeProduct As String, IsActive As String)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("sp_s_msProdukGroup '" & TypeProduct & "','" & IsActive & "'", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                Nameddl.Items.Insert(0, New ListItem("Select..", "99"))
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlMSProduct(ByVal Nameddl As DropDownList, lob As String, userid As String)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("sp_s_msProduk '" & lob & "','" & userid & "'", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                Nameddl.Items.Insert(0, New ListItem("ALL", "ALL"))
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
                If Nameddl.Items.Count = 2 Then
                    Nameddl.Items.RemoveAt(0)
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlMSUser(ByVal Nameddl As DropDownList, UserId As String, BranchCode As String, lob As String, lvlAdm As String)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("sp_s_MsUresRole '" & UserId & "','" & BranchCode & "','" & lob & "','" & lvlAdm & "'", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                'Nameddl.Items.Insert(0, New ListItem("Select..", "99"))
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
                If Nameddl.Items.Count = 2 Then
                    Nameddl.Items.RemoveAt(0)
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlRole(ByVal Nameddl As DropDownList, role As String)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim strsql As String
            If role = "00001" Then
                strsql = "select RoleCode,RoleDesc from msrole with (nolock) "
            Else
                strsql = "select RoleCode,RoleDesc from msrole with (nolock) where RoleCode<>'00001'"
            End If

            Dim cmd As New SqlCommand(strsql, con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlRolegenuser(ByVal Nameddl As DropDownList)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim strsql As String

            strsql = "sp_s_msrolegenuser"

            Dim cmd As New SqlCommand(strsql, con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlRoleSelectedRole(ByVal Nameddl As DropDownList, role As String)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim strsql As String
            
            strsql = "sp_s_MSROLE_SelectedRole"

            Dim cmd As New SqlCommand(strsql, con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub


    Public Sub isiddlGroup(ByVal Nameddl As DropDownList)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim strsql As String
            strsql = "SP_S_MSGroup"

            Dim cmd As New SqlCommand(strsql, con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddlBranch(ByVal Nameddl As DropDownList)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("SP_S_MsBranch ", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(1).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Sub isiddllob(ByVal Nameddl As DropDownList)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("SELECT [BranchCode] ,[BranchAbbreviation],BranchCode +' - '+ [BranchName],[BranchAdd],[BranchPhone],[BranchFax],[NPWP],[IsActive]  FROM [dbo].[msBranch] with (nolock) ORDER BY BranchCode", con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Nameddl.Items.Clear()
                Nameddl.Items.Insert(0, New ListItem("Select..", "99"))
                Nameddl.Items.Insert(1, New ListItem("ALL", "ALL"))
                While dr.Read()
                    Dim n As New ListItem
                    n.Text = dr(2).ToString()
                    n.Value = dr(0).ToString()
                    Nameddl.Items.Add(n)
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Public Function ClientIP_add() As String
        Dim ip_client As String
        If (HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")) <> Nothing Then
            ip_client = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR").ToString
        Else
            ip_client = HttpContext.Current.Request.UserHostAddress
        End If
        If ip_client = "::1" Then ip_client = "172.0.0.1"
        Return ip_client
    End Function

    Public Function convDate(ByVal txtDt As Date) As String
        Dim dt As String
        'dt = Date.ParseExact(txtDt, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToString("MM/dd/yyyy")
        dt = txtDt.ToString("MM/dd/yyyy")
        Return dt
    End Function

    Public Function SelectemailInputSPPA(lob As String, BranchCode As String) As DataTable
        Dim rtrStr As String = ""
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                con.Open()
                Dim sql As String = ""
                sql = "dbo.sp_email_inputsppa"

                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.CommandTimeout = config.SQLtimeout
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure
                com.Parameters.Add("@lob", SqlDbType.VarChar).Value = lob
                com.Parameters.Add("@BranchCode", SqlDbType.VarChar).Value = BranchCode

                Dim da As New SqlDataAdapter(com)
                Dim dt As New DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    'rtrStr = dt.Rows(0)(0)
                    Return dt
                End If
                Return dt

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
                'Return dt
            End Try
        End Using
    End Function


    Public Function bindData(Key As String, dateAwal As String, dateAkhir As String) As DataTable
        Try
            Using con As New SqlConnection(config.MSSQLConnection)
                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = config.SQLtimeout
                cmd.Connection = con
                Try

                    cmd.CommandText = "[dbo].[SP_S_MSHISTORY] '" & Key & "','" & Format(CDate(dateAwal), "MM/dd/yyyy") & "','" & Format(CDate(dateAkhir), "MM/dd/yyyy") & "',''" ' ,'" & IIf(ddlKD_STORE.SelectedValue = "ALL", "", ddlKD_STORE.SelectedValue) & "','" & Session("Rolecode") & "'"
                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable
                    da.Fill(dt)
                    If dt.Rows.Count = 0 Then
                        dt = Nothing
                    End If
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

    Public Function Selectemail(UserIdTo As String) As DataTable
        Dim rtrStr As String = ""
        Using con As New SqlConnection(config.MSSQLConnection)
            Try
                con.Open()
                Dim sql As String = ""
                sql = "dbo.sp_email"

                Dim com As SqlCommand = New SqlCommand(sql, con)
                com.Parameters.Clear()
                com.CommandTimeout = config.SQLtimeout
                com.Connection = con
                com.CommandType = CommandType.StoredProcedure
                com.Parameters.Add("@UseridTo", SqlDbType.VarChar).Value = UserIdTo
                Try

                    Dim da As New SqlDataAdapter(com)
                    Dim dt As New DataTable
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        'rtrStr = dt.Rows(0)(0)
                        Return dt
                    End If
                    Return dt

                Catch ex As Exception
                    Return Nothing
                Finally
                    con.Close()
                End Try
            Catch ex As Exception
                Throw New Exception(ex.Message)
                'Return dt
            End Try
        End Using
    End Function

    Public Sub SendMail(ByVal FromAddr As String, ByVal ToAddr As String, ByVal sbjct As String, ByVal bodymsg As String, Optional ByVal CcAddr As String = Nothing, Optional ByVal BccAddr As String = Nothing, Optional ByVal FileAttachment As String = Nothing)
        Try

            Dim msg As New System.Net.Mail.MailMessage
            With msg
                .To.Add(ToAddr.Replace(";", ","))
                If CcAddr.Trim <> "" Or CcAddr <> Nothing Then
                    .CC.Add(CcAddr.Replace(";", ","))
                End If
                If BccAddr.Trim <> "" Or BccAddr <> Nothing Then
                    .Bcc.Add(BccAddr.Replace(";", ","))
                End If
                .From = New System.Net.Mail.MailAddress(FromAddr) '"admin.cc@reliance-insurance.com")
                If FileAttachment.Trim <> "" Or FileAttachment.Trim <> Nothing Then
                    .Attachments.Add(New System.Net.Mail.Attachment(FileAttachment))
                End If
                .Subject = sbjct
                .IsBodyHtml = False
                .Body = bodymsg
                .Priority = Net.Mail.MailPriority.High
            End With

            Dim SmtpClient As New System.Net.Mail.SmtpClient("202.46.146.211", 587)
            With SmtpClient
                .UseDefaultCredentials = False
                .Credentials = New System.Net.NetworkCredential("bulkmail@reliance-insurance.com", "[2741n5ur4nc3$")
                .DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
                .EnableSsl = True
                .Port = 587
                .Host = "mail.reliance-insurance.com"
                .Send(msg)
            End With

        Catch exp As Exception
            Throw New Exception(exp.Message)
        End Try

    End Sub

   
    Public Function datasetXlsemail(path As String, Sheetname As String) As DataSet
        Dim MyConnection As New OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & path & "';Extended Properties=Excel 8.0;")
        'Dim MyConnection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & path & "';Extended Properties=Excel 12.0;")
        Dim DtSet As New DataSet
        Dim MyCommand As New OleDbDataAdapter("select * from [" & Sheetname & "$]", MyConnection)
        Try
            'MyCommand.TableMappings.Add("Table", Sheetname)
            'DtSet = New DataSet
            MyCommand.Fill(DtSet)
            MyConnection.Close()
            Return DtSet
        Catch ex As Exception
            Return Nothing
        Finally
            MyConnection.Close()
        End Try
    End Function

    'Public Function datasetemail1(path As String, Sheetname As String) As DataSet
    '    Dim MyConnection As New OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & path & "';Extended Properties=Excel 8.0;")
    '    'Dim MyConnection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & path & "';Extended Properties=Excel 12.0;")
    '    Dim DtSet As New DataSet
    '    Dim MyCommand As New OleDbDataAdapter("select * from [" & Sheetname & "$] where employe=1", MyConnection)
    '    Try
    '        'MyCommand.TableMappings.Add("Table", Sheetname)
    '        'DtSet = New DataSet
    '        MyCommand.Fill(DtSet)
    '        Return DtSet
    '    Catch ex As Exception
    '        Return Nothing
    '    Finally
    '        MyConnection.Close()
    '    End Try
    'End Function

    Public Function datasetemail(path As String, Sheetname As String) As DataSet
        Dim MyConnection As New OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & path & "';Extended Properties=Excel 8.0;")
        'Dim MyConnection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & path & "';Extended Properties=Excel 12.0;")
        Dim DtSet As New DataSet
        Dim MyCommand As New OleDbDataAdapter("select * from [" & Sheetname & "$] where employe=1", MyConnection)
        Try
            'MyCommand.TableMappings.Add("Table", Sheetname)
            'DtSet = New DataSet
            MyCommand.Fill(DtSet)
            MyConnection.Close()
            Return DtSet
        Catch ex As Exception
            Return Nothing
        Finally
            MyConnection.Close()
        End Try
    End Function

    Public Sub SendMailMultiAttach(ByVal FromAddr As String, ByVal ToAddr As String, ByVal sbjct As String, ByVal bodymsg As String, ByVal FileAttachment() As String, Optional ByVal CcAddr As String = Nothing, Optional ByVal BccAddr As String = Nothing)
        Try

            Dim msg As New System.Net.Mail.MailMessage
            With msg
                .To.Add(ToAddr.Replace(";", ","))
                If CcAddr.Trim <> "" Or CcAddr <> Nothing Then
                    .CC.Add(CcAddr.Replace(";", ","))
                End If
                If BccAddr.Trim <> "" Or BccAddr <> Nothing Then
                    .Bcc.Add(BccAddr.Replace(";", ","))
                End If
                .From = New System.Net.Mail.MailAddress(FromAddr) '"admin.cc@reliance-insurance.com")
                If FileAttachment.Count > 0 Then
                    For i = 0 To FileAttachment.Count - 1
                        .Attachments.Add(New System.Net.Mail.Attachment(FileAttachment(i)))

                    Next
                End If
                .Subject = sbjct
                .IsBodyHtml = False
                .Body = bodymsg
                .Priority = Net.Mail.MailPriority.High
            End With

            Dim SmtpClient As New System.Net.Mail.SmtpClient("202.46.146.211", 587)
            With SmtpClient
                .UseDefaultCredentials = False
                .Credentials = New System.Net.NetworkCredential("bulkmail@reliance-insurance.com", "[2741n5ur4nc3$")
                .DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
                .EnableSsl = True
                .Port = 587
                .Host = "mail.reliance-insurance.com"
                .Send(msg)
            End With

        Catch exp As Exception
            Throw New Exception(exp.Message)
        End Try

    End Sub

    Public Function InsertTempEmail(NO As String, POLICYNO As String, MEMBID As String, EMPLOYE As String, FULLNAME As String, EMAIL As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "sp_i_TempEmail '" & NO & "','" & POLICYNO & "','" & MEMBID & "','" & EMPLOYE & "','" & FULLNAME & "','" & EMAIL & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            Finally
                con.Close()
            End Try
        End Using
    End Function

    Public Function updateTempEmail(POLICYNO As String, MEMBID As String) As Boolean
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = config.SQLtimeout
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "sp_u_TempEmail '" & POLICYNO & "','" & MEMBID & "'"
            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            Finally
                con.Close()
            End Try
        End Using
    End Function
End Class
