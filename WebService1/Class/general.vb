Imports System.Net.Mail
Public Class general

    Public Shared ReadOnly Property SetFullFilePath() As String
        Get
            Return String.Format("{0}{1}_{2}.txt", general.Path, general.LogPath, Now.ToString("ddMMyyyy"))
        End Get
    End Property

    Public Shared ReadOnly Property Path() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("Path")
        End Get
    End Property

    Public Shared ReadOnly Property LogPath() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("LogPath")
        End Get
    End Property

    Public Shared ReadOnly Property SMTPServer() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("SMTPServer")
        End Get
    End Property


End Class
