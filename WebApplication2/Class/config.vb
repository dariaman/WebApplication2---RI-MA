Public Class config

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

    Public Shared ReadOnly Property PictCompanyPath() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("PictCompanyPath")
        End Get
    End Property


    Public Shared ReadOnly Property uploadFilePictCompany() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("uploadFilePictCompany")
        End Get
    End Property

    Public Shared ReadOnly Property uploadFileBioData() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("uploadFileBioData")
        End Get
    End Property

    Public Shared ReadOnly Property uploadFileData() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("uploadFileData")
        End Get
    End Property

    Public Shared ReadOnly Property uploadFileDataClaim() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("uploadFileDataClaim")
        End Get
    End Property

    Public Shared ReadOnly Property uploadFileDataMobile() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("uploadFileDataMobile")
        End Get
    End Property

    Public Shared ReadOnly Property uploadFileDataClaimMobile() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("uploadFileDataClaimMobile")
        End Get
    End Property

    Public Shared ReadOnly Property pathFileData() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("pathFileData")
        End Get
    End Property

    Public Shared ReadOnly Property pathFileDataPanduanPolis() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("pathFileDataPanduanPolis")
        End Get
    End Property

    Public Shared ReadOnly Property FileDownloadAPK() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("FileDownloadAPK")
        End Get
    End Property

    Public Shared ReadOnly Property FileDownloadAPKName() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("FileDownloadAPKName")
        End Get
    End Property
    Public Shared ReadOnly Property DefaultLimit() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("DefaultLimit")
        End Get
    End Property
    Public Shared ReadOnly Property HeaderEmail() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("HeaderEmail")
        End Get
    End Property
    Public Shared ReadOnly Property EmailMembership() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("EmailMembership")
        End Get
    End Property

    Public Shared ReadOnly Property EmailClaim() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("EmailClaim")
        End Get
    End Property

    Public Shared ReadOnly Property PictClaimPath() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("PictClaimPath")
        End Get
    End Property

    Public Shared ReadOnly Property CCEmail() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("CCEmail")
        End Get
    End Property

    Public Shared ReadOnly Property createFiletxt() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("createFiletxt")
        End Get
    End Property

    Public Shared ReadOnly Property uploadFileSupplier() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("uploadFileSupplier")
        End Get
    End Property

    Public Shared ReadOnly Property MSSQLConnection() As String
        Get
            Return System.Configuration.ConfigurationManager.ConnectionStrings("getSoftware").ConnectionString
        End Get
    End Property

    Public Shared ReadOnly Property AcaBlankoDB() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("AcaBlankoDB")
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

    Public Shared ReadOnly Property RptPath() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("RptPath")
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

    Public Shared ReadOnly Property uploadDataEmailXls() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("uploadDataEmailXls")
        End Get
    End Property

    Public Shared ReadOnly Property uploadDataEmailXlsAttachment() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("uploadDataEmailXlsAttachment")
        End Get
    End Property


    Public Shared ReadOnly Property optclaim() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("optclaim")
        End Get
    End Property

    Public Shared ReadOnly Property optendors() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("optendors")
        End Get
    End Property
End Class
