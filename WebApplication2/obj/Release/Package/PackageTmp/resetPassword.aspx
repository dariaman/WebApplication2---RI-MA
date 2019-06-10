<%@ Page Title="File | Setting | Reset Password" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="resetPassword.aspx.vb" Inherits="WebApplication.resetPassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Search Type </span>
                            <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" placeholder="Search By" name="Search By" type="Search By"   >
                                <asp:ListItem>User Id</asp:ListItem>
                                <asp:ListItem Value="User Name">Name</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                    </div>

                </div>
                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                            <asp:TextBox ID="txtSearch" onkeypress="return isKey(event)" runat="server" class="form-control" placeholder="Search.." name="Search.." type="Search.."   data-toggle="tooltip" data-placement="top" title="Search Data"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server"   data-toggle="tooltip" data-placement="top" title="Search data"><i class="fa fa-search"></i></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
            </div>
    <div id="DivBody">
            <div class="row">
                <div class="col-sm-12">

                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Reset <small>Password</small></h3>

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
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="dataTable_wrapper">
                                        <br />
                                        <asp:GridView ID="gridMenu" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" DataKeyNames="UserId" EmptyDataText="No data Found" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="User Id" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserId")%>' CommandName="SelectLink"> <i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "UserId")%></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Username" HeaderText="User Name" />
                                                <asp:BoundField DataField="POLICYNO" HeaderText="Polis" />
                                                <asp:BoundField DataField="Password" HeaderText="Password" />
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </div>
        </asp:Panel>

    <asp:LinkButton ID="LinkMpe" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="LinkMpeModalPopupExtender" PopupControlID="pnlPopup" runat="server"
        TargetControlID="LinkMpe" PopupDragHandleControlID="mGridPict" BackgroundCssClass="Overlay">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none;" >
        <div class="modalPopup-dialog" role="document">
            <div class="modalPopup-content">
                <div class="modalPopup-header" id="mGridPict">
                    <table class="table table-striped table-bordered table-hover">
                        <tr>

                            <td style="vertical-align: central; text-align: start;">
                                <asp:LinkButton ID="LinkReset" runat="server" CssClass="btn btn-info btn-sm" Text="" data-toggle="tooltip" data-placement="top" title="Reset password"><i class="fa fa-rotate-right">&nbsp;</i>Reset</asp:LinkButton>
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
                                    <div class="panel-heading">
                                        Recent Password
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-striped">
                                            <tr>
                                                <td>Name Id
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUserId" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Name User
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Password
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPassword" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>

                                        <div class="col-sm-2">
                                            <asp:Button ID="btnReset" CssClass="btn btn-primary btn-block btn-flat" runat="server" Text="Reset" Visible="False" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
