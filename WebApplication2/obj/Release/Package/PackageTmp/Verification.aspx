<%@  Page Language="vb" AutoEventWireup="false" CodeBehind="Verification.aspx.vb" Inherits="WebApplication.Verification" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>RELI-HIT | Verification</title>
    <link href="images/icon.png" rel="SHORTCUT ICON" />
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- Bootstrap 3.3.2 -->
    <link href="Bootstrap/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome Icons -->
    <link href="Bootstrap/bootstrap/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="Bootstrap/dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />

    <!-- jQuery 2.1.3 -->
    <script src="bootstrap/plugins/jQuery/jQuery-2.1.3.min.js"></script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="bootstrap/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    <%--custome Loading--%>
    <script type="text/javascript" src="jscript/jquery1.2.3.js"></script>
    <script type="text/javascript" src="Loading/Loading.js"></script>
    <link rel="stylesheet" type="text/css" href="Loading/Loading.css" />

    <%--custome msgbox--%>
    <script type="text/javascript" src="jscript/jQuery1.8.3.js"></script>
    <script type="text/javascript" src="Cusmsgbox/Cusmsgbox.js"></script>
    <link rel="stylesheet" type="text/css" href="Cusmsgbox/Cusmsgbox.css" />

</head>
<body class="lockscreen">

    <form id="frmLogin" runat="server" method="post">
        <div class="lockscreen-wrapper">
            <div class="lockscreen-logo">
                <img alt="" src="images/logoname.png" />
            </div>

            <div class="lockscreen-item">
                <div class="lockscreen-image">
                    <asp:Image ID="Image1" runat="server" CssClass="user image img-rounded" />
                </div>
                <i class="fa fa-user"></i>
                <div class="lockscreen-credentials">
                    <asp:Label runat="server" ID="txtOnlineQuestion"></asp:Label>
                    <div class="input-group">
                        <asp:TextBox ID="txtOnlineAnswer" runat="server" class="form-control" placeholder="Answer.." name="Answer.." type="Answer.."></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btnLogin" CssClass="btn" runat="server"><i class="fa fa-arrow-right text-muted"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="help-block text-center">
                Answer the question to retrieve your session     
            </div>
            <div class='text-center'>
                <a href="login.aspx">Or sign in as a different user</a>
            </div>
            <div class='lockscreen-footer text-center'>
                <strong>© 2018-2019 Powered by Asuransi Reliance Indonesia.</strong> www.AsuransiReliance.com
                  
            </div>
        </div>
        <div id="loader" class="loading" style="text-align: 'center'">
            <table>
                <tr>
                    <td>
                        <i class='fa fa-refresh fa-spin'></i></td>
                    <td>&nbsp;&nbsp;Loading. Please wait...</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
