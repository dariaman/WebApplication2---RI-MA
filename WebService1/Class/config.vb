Public Class config

    'Public Shared ReadOnly Property getdate() As String
    '    Get
    '        Return Format(Now, "MMddyyhhssmmfff")
    '    End Get
    'End Property

    Public Shared ReadOnly Property downloadFile() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("downloadFile")
        End Get
    End Property

    Public Shared ReadOnly Property uploadFile() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("uploadFile")
        End Get
    End Property

    Public Shared ReadOnly Property MSSQLConnection() As String
        Get
            Return System.Configuration.ConfigurationManager.ConnectionStrings("getSoftware").ConnectionString
        End Get
    End Property

    Public Shared ReadOnly Property CelcomConnection() As String
        Get
            Return System.Configuration.ConfigurationManager.ConnectionStrings("Celcom").ConnectionString
        End Get
    End Property

    Public Shared ReadOnly Property mssqlOwner() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("mssqlOwner")
        End Get
    End Property

    Public Shared ReadOnly Property SMTPServer() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("SMTPServer")
        End Get
    End Property

    Public Shared ReadOnly Property NotificationSender() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("notifSender")
        End Get
    End Property

    Public Shared ReadOnly Property UserEmail() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("UserEmail")
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

    Public Shared ReadOnly Property SetFullFilePath() As String
        Get
            Return String.Format("{0}{1}_{2}.txt", Path, LogPath, Now.ToString("ddMMyyyy"))
        End Get
    End Property

    Public Shared ReadOnly Property DbSvr() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("DbSvr")
        End Get
    End Property

    Public Shared ReadOnly Property DbName() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("DbName")
        End Get
    End Property

    Public Shared ReadOnly Property DbUsr() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("DbUsr")
        End Get
    End Property

    Public Shared ReadOnly Property DbPwd() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("DbPwd")
        End Get
    End Property

    Public Shared ReadOnly Property SQLtimeout() As Integer
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("SQLtimeout")
        End Get
    End Property

    Public Shared ReadOnly Property Keterangan() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("Keterangan")
        End Get
    End Property

End Class
