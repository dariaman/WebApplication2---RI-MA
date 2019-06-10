Imports System.Net
Imports System.Net.FtpClient
Imports WinSCP
Public Class WebForm2
    Inherits System.Web.UI.Page

    'Function FTPTable() As DataTable
    '    Dim table As New DataTable
    '    table.Columns.Add("ItemName", GetType(String))
    '    Return table
    'End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'ftpFolder()
            'DGuser.DataSource = DTWinScp(1, "ftp.admedika.co.id", "ftpreliance", "rliftp2010")
            DGuser.DataSource = DTWinScp(1, "ftp.fhnid.com", 2244, "ftpreliance", "EHfK24")
            DGuser.DataBind()
            lnkUp1.Attributes.Add("onClick", "javascript:history.back(); return false;")
        End If
    End Sub

   
    Private Sub DGuser_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles DGuser.RowCommand
        Try
            System.Threading.Thread.Sleep(500)

            If e.CommandName.Equals("Download") Then
                Dim KEY As String = e.CommandArgument
                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim index As Integer = gvRow.RowIndex
                fileDownload(2, "ftp.admedika.co.id", 21, "ftpreliance", "rliftp2010", lblNext.Text & "/" & e.CommandArgument, "C:\log\")
            End If
            If e.CommandName.Equals("Select") Or e.CommandName.Equals("SelectLink") Then

                Dim KEY As String = e.CommandArgument
                Dim gvRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim index As Integer = gvRow.RowIndex

                'If KEY = ".." Then
                '    DGuser.DataSource = FolderDTWinScp(lblUp1.Text)
                '    Dim i As Integer = Len(lblNext.Text) - Len(lblUp1.Text)
                '    Dim ii As Integer = Len(lblUp1.Text)
                '    lblNext.Text = lblUp1.Text 'Replace(lblNext.Text & "/" & KEY, "/..", "")
                '    lblUp1.Text = Left(lblUp1.Text, ii - i)
                'Else
                DGuser.DataSource = FolderDTWinScp(2, "ftp.admedika.co.id", 21, "ftpreliance", "rliftp2010", lblNext.Text & "/" & KEY)
                lblNext.Text = lblNext.Text & "/" & KEY
                'lblUp1.Text = lblNext.Text
                'End If

                DGuser.DataBind()
                'Dim wrDownload As FtpWebRequest = Nothing
                'wrDownload.Method = WebRequestMethods.Ftp.DownloadFile
            End If
        Catch ex As Exception

        End Try

    End Sub
    Public Shared Function DTWinScp(protocolOpt As Integer, HostName As String, port As String, UserName As String, Password As String) As DataTable

        Try
            ' Setup session options
            Dim dt As DataTable = New DataTable("FTP")
            dt.Columns.Add("ItemName", GetType(String))
            dt.Columns.Add("IsFolder", GetType(String))

            Dim sessionOptions As New SessionOptions
            With sessionOptions
                .Protocol = protocolOpt 'Protocol.Ftp
                .HostName = HostName '"ftp.admedika.co.id"
                .UserName = UserName '"ftpreliance"
                .Password = Password '"rliftp2010"
                .PortNumber = port
                If protocolOpt = 1 Then
                    .SshHostKeyFingerprint = "ssh-rsa 4096 bc:77:c9:ed:fb:bf:a8:d0:38:2f:61:5f:ee:f5:f2:44"
                    .GiveUpSecurityAndAcceptAnySshHostKey = True
                End If

            End With

            Using session As New Session
                ' Connect
                session.Open(sessionOptions)

                Dim directory As RemoteDirectoryInfo = session.ListDirectory("/")

                Dim fileInfo As RemoteFileInfo
                For Each fileInfo In directory.Files
                    If fileInfo.Name <> ".." Then
                        dt.Rows.Add(fileInfo.Name, fileInfo.IsDirectory)
                    End If
                Next
                'session.Close()
            End Using

            Return dt
        Catch e As Exception

            Return Nothing
        End Try

    End Function

    Public Shared Function fileDownload(protocolOpt As Integer, HostName As String, port As String, UserName As String, Password As String, FtpFilePath As String, DownloadPath As String) As String

        Dim x As String = Nothing
        Try
            ' Setup session options
            Dim sessionOptions As New SessionOptions
            With sessionOptions
                .Protocol = protocolOpt 'Protocol.Ftp
                .HostName = HostName '"ftp.admedika.co.id"
                .UserName = UserName '"ftpreliance"
                .Password = Password '"rliftp2010"
                .PortNumber = port
                If protocolOpt = 1 Then
                    .SshHostKeyFingerprint = "ssh-rsa 4096 bc:77:c9:ed:fb:bf:a8:d0:38:2f:61:5f:ee:f5:f2:44"
                End If
            End With
            Using session As New Session
                ' Connect
                session.Open(sessionOptions)

                ' Download files
                Dim transferOptions As New TransferOptions
                transferOptions.TransferMode = TransferMode.Binary

                Dim transferResult As TransferOperationResult
                transferResult = session.GetFiles(FtpFilePath, DownloadPath, False, transferOptions)

                transferResult.Check()

                ' Print results
                For Each transfer In transferResult.Transfers
                    x = "Download of " & transfer.FileName & " succeeded"
                Next
            End Using

            Return x
        Catch e As Exception

            Return x
        End Try

    End Function

    Public Shared Function FolderDTWinScp(protocolOpt As Integer, HostName As String, port As String, UserName As String, Password As String, Folder As String) As DataTable

        Try
            ' Setup session options
            Dim dt As DataTable = New DataTable("FTP")
            dt.Columns.Add("ItemName", GetType(String))
            dt.Columns.Add("IsFolder", GetType(String))

            Dim sessionOptions As New SessionOptions
            With sessionOptions
                .Protocol = Protocol.Ftp
                .HostName = "ftp.admedika.co.id"
                .UserName = "ftpreliance"
                .Password = "rliftp2010"
                .PortNumber = port
                If protocolOpt = 1 Then
                    .SshHostKeyFingerprint = "ssh-rsa 4096 bc:77:c9:ed:fb:bf:a8:d0:38:2f:61:5f:ee:f5:f2:44"
                End If
            End With

            Using session As New Session
                ' Connect
                session.Open(sessionOptions)

                Dim directory As RemoteDirectoryInfo = session.ListDirectory(Folder & "/")

                Dim fileInfo As RemoteFileInfo
                For Each fileInfo In directory.Files
                    If fileInfo.Name <> ".." Then
                        dt.Rows.Add(fileInfo.Name, fileInfo.IsDirectory)
                    End If
                Next
            End Using

            Return dt
        Catch e As Exception
            Console.WriteLine("Error: {0}", e)
            Return Nothing
        End Try

    End Function

    Protected Sub lnkUp1_Click(sender As Object, e As EventArgs) Handles lnkUp1.Click

    End Sub
End Class