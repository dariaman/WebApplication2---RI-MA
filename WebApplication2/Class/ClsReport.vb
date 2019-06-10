Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
'create  by pandu
Public Class clsReport
    Public Shared Bytes As Byte()


    Public Shared Sub Rpt2Key(p As clsReport, pColl As clsReport.MSReportCollection, ByVal Key As String, ByVal Key1 As String, ByVal Key2 As String, param1 As String, param2 As String)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("SELECT * from MSReport where [no] =" & Key, con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    p.ReportPath = dr("ReportPath")
                    p.RptSql1 = dr("RptSql1") & " '" & Key1 & "', '" & Key2 & "'"
                    p.DSstr1 = dr("DSstr1")
                    p.Param1 = param1
                    p.Param2 = param2
                    p.Param3 = dr("Param3")
                    p.Param4 = dr("Param4")
                    p.Param5 = dr("Param5")
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
            pColl.Add(p)
        End Using
    End Sub

    Public Shared Sub Rpt3Key(p As clsReport, pColl As clsReport.MSReportCollection, ByVal Key As String, ByVal Key1 As String, ByVal Key2 As String, ByVal Key3 As String, param1 As String, param2 As String)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("SELECT * from MSReport where [no] =" & Key, con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    p.ReportPath = dr("ReportPath")
                    p.RptSql1 = dr("RptSql1") & " '" & Key1 & "', '" & Key2 & "', '" & Key3 & "'"
                    p.DSstr1 = dr("DSstr1")
                    p.Param1 = param1
                    p.Param2 = param2
                    p.Param3 = dr("Param3")
                    p.Param4 = dr("Param4")
                    p.Param5 = dr("Param5")
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
            pColl.Add(p)
        End Using
    End Sub

    Public Shared Sub Rpt4Key(p As clsReport, pColl As clsReport.MSReportCollection, ByVal Key As String, ByVal Key1 As String, ByVal Key2 As String, ByVal Key3 As String, ByVal Key4 As String, param1 As String, param2 As String)
        Using con As New SqlConnection(config.MSSQLConnection)
            Dim cmd As New SqlCommand("SELECT * from MSReport where [no] =" & Key, con)
            Try
                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    p.ReportPath = dr("ReportPath")
                    p.RptSql1 = dr("RptSql1") & " '" & Key1 & "', '" & Key2 & "', '" & Key3 & "', '" & Key4 & "'"
                    p.DSstr1 = dr("DSstr1")
                    p.Param1 = param1
                    p.Param2 = param2
                    p.Param3 = dr("Param3")
                    p.Param4 = dr("Param4")
                    p.Param5 = dr("Param5")
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                con.Close()
            End Try
            pColl.Add(p)
        End Using
    End Sub

#Region "property"
    Public _ReportPath As String, _RptSql1 As String, _DSstr1 As String, _Param1 As String, _Param2 As String, _Param3 As String, _Param4 As String, _Param5 As String
    Public _RptPin As String

    Public Property ReportPath() As String
        Get
            Return _ReportPath
        End Get
        Set(ByVal value As String)
            _ReportPath = value
        End Set
    End Property

    Public Property RptSql1() As String
        Get
            Return _RptSql1
        End Get
        Set(ByVal value As String)
            _RptSql1 = value
        End Set
    End Property

    Public Property DSstr1() As String
        Get
            Return _DSstr1
        End Get
        Set(ByVal value As String)
            _DSstr1 = value
        End Set
    End Property

    Public Property Param1() As String
        Get
            Return _Param1
        End Get
        Set(ByVal value As String)
            _Param1 = value
        End Set
    End Property

    Public Property Param2() As String
        Get
            Return _Param2
        End Get
        Set(ByVal value As String)
            _Param2 = value
        End Set
    End Property

    Public Property Param3() As String
        Get
            Return _Param3
        End Get
        Set(ByVal value As String)
            _Param3 = value
        End Set
    End Property

    Public Property Param4() As String
        Get
            Return _Param4
        End Get
        Set(ByVal value As String)
            _Param4 = value
        End Set
    End Property

    Public Property Param5() As String
        Get
            Return _Param5
        End Get
        Set(ByVal value As String)
            _Param5 = value
        End Set
    End Property

    Public Property RptPin() As String
        Get
            Return _RptPin
        End Get
        Set(ByVal value As String)
            _RptPin = value
        End Set
    End Property
#End Region

    Public Class MSReportCollection
        Inherits List(Of clsReport)
        'Dim GEN As general

        Public Function exporttofile(RV As ReportViewer, ByVal exfile As String) As Byte()
            Dim Bytes As Byte() = RV.LocalReport.Render(exfile, "")
            Return Bytes
        End Function

        Public Function RptQuery(ByVal qry As String) As DataSet
            Dim ds As New DataSet
            Using con As New SqlConnection(config.MSSQLConnection)
                Try
                    Dim cmd As New SqlCommand
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = config.SQLtimeout
                    cmd.Connection = con
                    cmd.CommandText = qry
                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable
                    da.Fill(ds)
                    Return ds
                Catch ex As Exception
                    Return ds
                End Try
            End Using
        End Function

        Public Sub viewrpt(RV As ReportViewer, ReportPath As String, RptSql As String, DsName As String, prm1 As String, prm2 As String, prm3 As String, prm4 As String, prm5 As String)
            Dim DSUtama As DataSet
            With RV.LocalReport
                .ReportPath = ReportPath
                DSUtama = RptQuery(RptSql)
                Dim reportDS As ReportDataSource = New ReportDataSource(DsName, DSUtama.Tables(0))
                .ReportEmbeddedResource = ReportPath
                RV.ProcessingMode = ProcessingMode.Local
                .DataSources.Clear()
                .DataSources.Add(reportDS)
                .DisplayName = "Report : " & DsName
                If Len(prm1) = 0 Then prm1 = " "
                If Len(prm2) = 0 Then prm2 = " "
                If Len(prm3) = 0 Then prm3 = " "
                If Len(prm4) = 0 Then prm4 = " "
                If Len(prm5) = 0 Then prm5 = " "
                Dim Param1 As New ReportParameter("Param1", prm1)
                Dim Param2 As New ReportParameter("Param2", prm2)
                Dim Param3 As New ReportParameter("Param3", prm3)
                Dim Param4 As New ReportParameter("Param4", prm4)
                Dim Param5 As New ReportParameter("Param5", prm5)
                .SetParameters(Param1)
                .SetParameters(Param2)
                .SetParameters(Param3)
                .SetParameters(Param4)
                .SetParameters(Param5)
                '.Refresh()
            End With
            'Dim RV1 As ReportViewer
            'RV1 = RV
            'Return RV1
        End Sub
    End Class
End Class