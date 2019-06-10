<%@  Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="WebApplication.Login" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>

    <title>RELI-HIT | Sign In</title>
    <link href="images/icon.png" rel="SHORTCUT ICON" />
    <!-- Bootstrap 3.3.2 -->
    <link href="Bootstrap/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome Icons -->
    <link href="Bootstrap/bootstrap/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="Bootstrap/dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <!-- iCheck -->
    <link href="Bootstrap/plugins/iCheck/square/blue.css" rel="stylesheet" type="text/css" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->


    <!-- jQuery 2.1.3 -->
    <script src="Bootstrap/plugins/jQuery/jQuery-2.1.3.min.js"></script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="Bootstrap/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <!-- iCheck -->
    <script src="Bootstrap/plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <script>
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%' // optional
            });
        });
    </script>

    <%--custome Loading--%>
    <script type="text/javascript" src="jscript/jquery1.2.3.js"></script>
    <script type="text/javascript" src="Loading/Loading.js"></script>
    <link rel="stylesheet" type="text/css" href="Loading/Loading.css" />

    <%--custome msgbox--%>
    <script type="text/javascript" src="jscript/jQuery1.8.3.js"></script>
    <script type="text/javascript" src="Cusmsgbox/Cusmsgbox.js"></script>
    <link rel="stylesheet" type="text/css" href="Cusmsgbox/Cusmsgbox.css" />

    <link rel="stylesheet" type="text/css" href="css/PopUp.css" />
    <link rel="stylesheet" type="text/css" href="css/MyCss3.css" />
    <link rel="stylesheet" type="text/css" href="css/StyleAdd.css" />
    <!-- FastClick -->
    <script src='bootstrap/plugins/fastclick/fastclick.min.js'></script>
    <script src="bootstrap/plugins/input-mask/jquery.inputmask.js" type="text/javascript"></script>
    <script src="bootstrap/plugins/input-mask/jquery.inputmask.date.extensions.js" type="text/javascript"></script>
    <script src="bootstrap/plugins/input-mask/jquery.inputmask.extensions.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            //Datemask dd/mm/yyyy
            $("#datemask").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            //Datemask2 mm/dd/yyyy
            $("#datemask2").inputmask("mm/dd/yyyy", { "placeholder": "mm/dd/yyyy" });

            $("#datemask3").inputmask("yyyydd", { "placeholder": "yyyydd" });
            //Money Euro
            $("[data-mask]").inputmask();


            //Date range picker
            $('#reservation').daterangepicker();
            $('#FrameContent_reservation').daterangepicker({ timePicker: true, timePickerIncrement: 30, format: 'DD/MM/YYYY' });
            //Date range picker with time picker
            $('#reservationtime').daterangepicker({ timePicker: true, timePickerIncrement: 30, format: 'DD/MM/YYYY h:mm A' });
            //Date range as a button
            $('#daterange-btn').daterangepicker(
                    {
                        ranges: {
                            'Today': [moment(), moment()],
                            'Yesterday': [moment().subtract('days', 1), moment().subtract('days', 1)],
                            'Last 7 Days': [moment().subtract('days', 6), moment()],
                            'Last 30 Days': [moment().subtract('days', 29), moment()],
                            'This Month': [moment().startOf('month'), moment().endOf('month')],
                            'Last Month': [moment().subtract('month', 1).startOf('month'), moment().subtract('month', 1).endOf('month')]
                        },
                        startDate: moment().subtract('days', 29),
                        endDate: moment()
                    },
            function (start, end) {
                $('#reportrange span').html(start.format('MMMM DD, YYYY') + ' - ' + end.format('MMMM DD, YYYY'));
            }
            );

            //iCheck for checkbox and radio inputs
            $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue'
            });
            //Red color scheme for iCheck
            $('input[type="checkbox"].minimal-red, input[type="radio"].minimal-red').iCheck({
                checkboxClass: 'icheckbox_minimal-red',
                radioClass: 'iradio_minimal-red'
            });
            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red,span.flat-red,table.flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-blue',
                radioClass: 'iradio_flat-blue'
            });

            //Colorpicker
            $(".my-colorpicker1").colorpicker();
            //color picker with addon
            $(".my-colorpicker2").colorpicker();

            //Timepicker
            $(".timepicker").timepicker({
                showInputs: false
            });
        });
    </script>
</head>
<body class="login-page">
    <form id="frmLogin" runat="server" method="post">
<asp:scriptmanager ID="Scriptmanager1" runat="server"></asp:scriptmanager>
        <div class="login-box">
            <div class="login-logo">
                <img alt="" src="images/logoname.png" />
                <%--<br />--%>
                <H3>RELI-HIT</H3>
            </div>
            <!-- /.login-logo -->
            <div class="login-box-body">

                <p class="login-box-msg">Sign in to start your session</p>

                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtUserid" runat="server" class="form-control" placeholder="Input User Id.." name="UserId" data-toggle="tooltip" data-placement="top" title="Input User Id.." MaxLength="50"></asp:TextBox>
                    <span class="glyphicon glyphicon-user form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Password.." name="password.." type="password" data-toggle="tooltip" data-placement="top" title="Input your password" MaxLength="50"></asp:TextBox>
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <asp:Button ID="btnLogin" class="btn btn-primary btn-block btn-flat" runat="server" Text="Sign In"   data-toggle="tooltip" data-placement="top" title="Click for Sign In"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <asp:LinkButton ID="LinkForgot" runat="server">Forget Password</asp:LinkButton>
                    </div>
                </div>
                <%--<a href="register.aspx" class="text-center">Register a new membership</a>--%>
            </div>
        </div>

<asp:LinkButton ID="LinkMpe" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="LinkMpeModalPopupExtender" PopupControlID="pnlPopup" runat="server"
        TargetControlID="LinkMpe" PopupDragHandleControlID="mGridPict" BackgroundCssClass="Overlay">
    </asp:ModalPopupExtender>

    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" >
        <div class="modalPopup-dialog" role="document">
            <div class="modalPopup-content">
                <div class="modalPopup-header" id="mGridPict">
                    <table class="table table-striped table-bordered table-hover">
                        <tr>

                            <td style="vertical-align: central; text-align: start;">
                                <asp:LinkButton ID="LinkSend" runat="server" CssClass="btn btn-info btn-sm" Text="" data-toggle="tooltip" data-placement="top" title="Send password">Send password</asp:LinkButton>
                            </td>
                            <td style="vertical-align: central; text-align: end;">
                                <asp:LinkButton ID="LinkClose" runat="server" CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Close Page"><i class="fa fa-times" ></i></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modalPopup-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="panel panel-default">
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-striped">
                                            <tr>
                                                <td>
                                                    <div class="form-group has-feedback">
                                                        <asp:TextBox ID="txtPolicyNo" runat="server" class="form-control" placeholder="Policy No.." name="Policy No.."  data-toggle="tooltip" data-placement="top" title="Policy No" MaxLength="20"></asp:TextBox>
                                                        <span class="glyphicon glyphicon-list form-control-feedback"></span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="form-group has-feedback">
                                                        <asp:TextBox ID="txtMebid" runat="server" class="form-control" placeholder="Member Id.." name="Member Id.." data-toggle="tooltip" data-placement="top" title="Member Id" MaxLength="10"></asp:TextBox>
                                                        <span class="glyphicon glyphicon-user form-control-feedback"></span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="form-group has-feedback">
                                                        <asp:TextBox ID="txtBirthDate" runat="server" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask
                                                                                    data-toggle="tooltip" data-placement="top" title="fill Birth Date" class="form-control" placeholder="Birth Date.." name="Birth Date.." type="Birth Date.."></asp:TextBox>            
                                                        <span class="glyphicon glyphicon-calendar form-control-feedback"></span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <div class="form-group has-feedback">
                                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email.." name="Email.." data-toggle="tooltip" data-placement="top" title="Input your Email" MaxLength="50"></asp:TextBox>
                                                        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="1">
                                                    <div class="form-group has-feedback"><asp:Label ID="LblCap" runat="server" Text="1234567890" ></asp:Label>
                                                        
                                                    </div>
                                                </td>
                                                <td colspan="2">
                                                    <div class="form-group has-feedback">
                                                        <asp:TextBox ID="txtCaptcha" runat="server" class="form-control" placeholder="Captcha.." name="Captcha.." data-toggle="tooltip" data-placement="top" title="Input your captcha" MaxLength="4"></asp:TextBox>
                                                        
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </asp:Panel>

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
