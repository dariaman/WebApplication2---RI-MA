 <%@  Page Language="vb" AutoEventWireup="false" CodeBehind="register.aspx.vb" Inherits="WebApplication.register" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>RELI-HIT | Registration</title>
    <link href="images/icon.png" rel="SHORTCUT ICON" />

    <%--custome Loading--%>
    <script type="text/javascript" src="jscript/jquery1.2.3.js"></script>
    <script type="text/javascript" src="Loading/Loading.js"></script>
    <link rel="stylesheet" type="text/css" href="Loading/Loading.css" />

    <%--custome msgbox--%>
    <script type="text/javascript" src="jscript/jQuery1.8.3.js"></script>
    <script type="text/javascript" src="Cusmsgbox/Cusmsgbox.js"></script>
    <link rel="stylesheet" type="text/css" href="Cusmsgbox/Cusmsgbox.css" />

    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- Bootstrap 3.3.2 -->
    <link href="Bootstrap/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome Icons -->
    <link href="Bootstrap/bootstrap/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="Bootstrap/dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <!-- iCheck -->
    <link href="Bootstrap/plugins/iCheck/square/blue.css" rel="stylesheet" type="text/css" />
    <!-- iCheck for checkboxes and radio inputs -->
    <link href="Bootstrap/plugins/iCheck/all.css" rel="stylesheet" type="text/css" />
    <link href="Bootstrap/dist/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <%--<link href="Bootstrap/plugins/iCheck/all.css" rel="stylesheet" type="text/css" />--%>
    <!-- bootstrap wysihtml5 - text editor -->
    <link href="Bootstrap/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" rel="stylesheet" type="text/css" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->

    <script type="text/javascript">
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '10%' // optional
            });
        });
    </script>
    <!-- jQuery 2.1.3 -->
    <script src="Bootstrap/plugins/jQuery/jQuery-2.1.3.min.js"></script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="Bootstrap/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <!-- iCheck -->
    <script src="Bootstrap/plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <script src="Bootstrap/dist/js/app.min.js" type="text/javascript"></script>

    <!-- CK Editor -->
    <script src="Bootstrap/cdn.ckeditor.com/4.4.3/standard/ckeditor.js"></script>
    <!-- Bootstrap WYSIHTML5 -->
    <script src="Bootstrap/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            // Replace the <textarea id="editor1"> with a CKEditor
            // instance, using default configuration.
            CKEDITOR.replace('editor1');
            //bootstrap WYSIHTML5 - text editor
            $(".textarea").wysihtml5();
        });
    </script>

</head>
<body class="register-page">
    <form id="frmRegister" runat="server" method="post">
        <div class="register-box">
            <div class="register-logo">
                <img alt="" src="images/logoName.png" />
            </div>
            <div class="register-box-body">
                <p class="login-box-msg">Register a new membership</p>
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="User Name.." name="Username" data-toggle="tooltip" data-placement="top" title="Input User Name"></asp:TextBox>
                    <span class="glyphicon glyphicon-user form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtUserEmail" runat="server" CssClass="form-control" placeholder="User Email.." name="Email" data-toggle="tooltip" data-placement="top" title="input yuor email"></asp:TextBox>
                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">Term <small>check the box on bottom if u Agrre</small></h3>
                        <div class="pull-right box-tools">
                            <button class="btn btn-info btn-sm" data-toggle="tooltip" data-widget="collapse" title="Collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button class="btn btn-info btn-sm" data-toggle="tooltip" data-widget="remove" title="Remove">
                                <i class="fa fa-times"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body pad">
                        <textarea id="editor1" style="width: 100%" name="editor1" rows="5">
Do you Agree
                    </textarea>
                    </div>
                </div>
                <div class="form-group has-feedback">
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="box-body">
                                <label>
                                    <asp:CheckBox ID="CbTerm" runat="server" CssClass="flat-red" />
                                    I agree to the terms
                                </label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <asp:Button ID="btnSignUp" CssClass="btn btn-primary btn-block btn-flat" runat="server" Text="Register"   data-toggle="tooltip" data-placement="top" title="Click for sign up"/>
                        </div>
                    </div>
                    <a href="login.aspx" class="text-center"   data-toggle="tooltip" data-placement="top" title="Back to login">I already have a membership</a>
                </div>
            </div>
        </div>
    </form>
    <div id="loader" class="loading" style="text-align: 'center'">
        <table>
            <tr>
                <td>
                    <i class='fa fa-refresh fa-spin'></i></td>
                <td>&nbsp;&nbsp;Loading. Please wait...</td>
            </tr>
        </table>
    </div>

</body>
</html>
