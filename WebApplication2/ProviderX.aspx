 <%--<%@  Page Title="File | General | Provider List" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Provider.aspx.vb" Inherits="WebApplication.Provider" %>--%>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProviderX.aspx.vb" Inherits="WebApplication.ProviderX" %>
 <%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">--%>


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

<html>
<body class="skin-blue">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="wrapper">
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div class="row">
                <%--<div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Search Type </span>
                            <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" placeholder="Search By" name="Search By" type="Search By">
                                <asp:ListItem Value="Kode">Provider Id</asp:ListItem>
                                <asp:ListItem Value="Nama">Provider Name</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>--%>
                <div class="col-sm-4">
                    <div class="panel-body">
                        <asp:RadioButtonList ID="RBProviderType1" runat="server" CssClass="flat-red" data-placement="top" data-toggle="tooltip" RepeatDirection="Vertical" title="Click for Group Provider" />
                    </div>

                </div>
                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                            <asp:TextBox ID="TxtKeyWord" runat="server" class="form-control" placeholder="Input Range..." name="Input Range..." type="Input Range..." data-toggle="tooltip" data-placement="top" title="Input Key" ></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server"  data-toggle="tooltip" data-placement="top" title="Search Data" ><i class="fa fa-search"></i></asp:LinkButton></span>
                        </div> 
                    </div>

                </div>
                <div class="col-sm-2">
                    <div class="panel-body" >
                        <div class="form-group input-group">
                            <%--<span class="input-group-addon"><i class='fa fa-plus-square fa-fw'></i></span>--%>
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Add Group" Visible="false"
                            data-toggle="tooltip" data-placement="top" title="Click for add new Provider" />
                            </div>
                    </div>
                </div>
            </div>

            
    <div id="DivBody">
            <div class="row">
                <div class="col-sm-12">

                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Provider <small>List</small></h3>
                            
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
                                <div class="panel-body">
                                    <div class="dataTable_wrapper">
                                        <br />
                                        <asp:GridView ID="gridMenu" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                            DataKeyNames="PROVIDERID" EmptyDataRowStyle-CssClass="empty_data" 
                                            EmptyDataText="No data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                       <asp:LinkButton ID="ImgViewPROVIDERID" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PROVIDERID")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "PROVIDERID")%></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PROVIDERTYPENAME" HeaderText="PROVIDER TYPE" />
                                                <asp:BoundField DataField="PROVIDERNAME" HeaderText="PROVIDER NAME" />
                                                <asp:BoundField DataField="BUILDING" HeaderText="BUILDING" />
                                                <asp:BoundField DataField="STREET1" HeaderText="STREET 1" />
                                                <asp:BoundField DataField="COUNTRYNM" HeaderText="COUNTRY NAME" />
                                                <asp:BoundField DataField="PROVINCENM" HeaderText="PROVINCE NAME" />
                                                <asp:BoundField DataField="STATENM" HeaderText="STATE NAME" />
                                                <asp:BoundField DataField="RangeRS" HeaderText="JARAK KE RS TUJUAN (KM)" />
                                                <%--<asp:BoundField DataField="LATTITUDE" HeaderText="LATTITUDE" />
                                                <asp:BoundField DataField="LONGITUDE" HeaderText="LONGITUDE" />--%>
                                                <asp:BoundField DataField="REMARK" HeaderText="REMARK" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                    </div>
                </div>
            </div>
    </div>
        </asp:Panel>
    <asp:Panel ID="pnlPopup" runat="server" Visible="false" DefaultButton="btnSave">
        <div id="DivBody1">
                <table class="table table-striped table-bordered table-hover" id="mGridPict">
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkClose" runat="server" Text=""  data-toggle="tooltip" data-placement="top" title="Close Page" > <p style="line-height: 0%;text-align: center;"><i class="fa fa-times" ></i> Exit</p></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkSubmit" runat="server" Text="" data-toggle="tooltip" data-placement="top" title="Submit Data" > <p style="line-height: 0%;text-align: center;"><i class="fa fa-check" ></i> Submit</p></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Provider Type ID <i class="fa fa-code fa-fw"></i>
                        </div>
                          <div class="panel-body">
                              <%--<asp:BulletedList ID="BulletedList1" runat="server" CssClass="flat-red" data-toggle="tooltip" data-placement="top" title="Click for Group Provider" />--%>
                              <asp:RadioButtonList ID="RBProviderType" runat="server" CssClass="flat-red" data-toggle="tooltip" data-placement="top" title="Click for Group Provider" RepeatDirection="Vertical" /></asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Provider Code <i class="fa fa-code fa-fw"></i>
                        </div>
                          <div class="panel-body">
                            <asp:TextBox ID="txtProviderCode" runat="server" MaxLength="20" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Station Code" CssClass="form-control" placeholder="Provider Code.." name="Provider Code.." type="Provider Code.."></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Provider name <i class="fa fa-building fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtProvidername" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Station name" MaxLength="100" CssClass="form-control" placeholder="Provider name.." name="Provider name.." type="Provider name.."></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                            REMARK
                        <i class="fa fa-pencil-square-o fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtRemark" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Note" MaxLength="20" CssClass="form-control" placeholder="Note.." name="Note.." type="Note.."></asp:TextBox>
                        </div>
                        <div class="panel-footer">
                        </div>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Submit" Visible="False" />
        </div>
    </asp:Panel>
            </div>
        </form>
    </body>
    </html>
<%--</asp:Content>--%>
