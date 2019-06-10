Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Newtonsoft.Json
Imports System.Configuration
'Imports System.Web.Script.Services
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class ServiceRelihit
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()> _
    Public Function loginusr(ByVal userid As String, ByVal password As String) As String
        Dim _ClsUser As New ClsUser
        Dim _ClsEncryp As New ClsEncryption
        Dim ds As DataSet = New DataSet()

        ds = _ClsUser.loginusr(userid, _ClsEncryp.Encrypt(password))

        Return JsonConvert.SerializeObject(ds)
    End Function

    <WebMethod()> _
    Public Function MenuDetail(ByVal ROLECODE As String, ByVal MENUPARENTCODE As String, ByVal ISACTIVE As String) As String
        Dim _ClsMenu As New ClsMenu
        Dim ds As DataSet = New DataSet()

        ds = _ClsMenu.MenuDetail(ROLECODE, MENUPARENTCODE, ISACTIVE)

        Return JsonConvert.SerializeObject(ds)
    End Function

    <WebMethod()> _
    Public Sub AutocompleteMSUser(UserName As String)
        Dim CS As String = config.MSSQLConnection 'ConfigurationManager.ConnectionStrings("DBCS").ConnectionString
        Dim countries As New List(Of ClsUser)()

        Using con As New SqlConnection(CS)
            Dim cmd As New SqlCommand("select top 20 UserId, UserName from msuser where UserName like '%" & UserName & "%' ", con)
            cmd.CommandType = CommandType.Text
            'cmd.Parameters.AddWithValue("@UserId", UserId)
            con.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            While dr.Read()
                Dim ClsUser As New ClsUser()
                ClsUser.UserId = dr("UserId").ToString()
                ClsUser.UserName = dr("UserName").ToString()
                countries.Add(ClsUser)
            End While
        End Using
        Dim JS As New JavaScriptSerializer()
        Context.Response.Write(JS.Serialize(countries))
        'Return JsonConvert.SerializeObject(countries)
    End Sub
    'Public Function AutocompleteMSUser(ByVal userid As String) As String
    '    Dim _ClsUser As New ClsUser
    '    Dim ds As DataSet = New DataSet()

    '    ds = _ClsUser.AutocompleteMSUser(userid)

    '    Return JsonConvert.SerializeObject(ds)
    'End Function
End Class