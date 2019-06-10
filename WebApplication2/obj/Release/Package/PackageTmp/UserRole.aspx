<%@ Page Title="File | Setting | Role List" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserRole.aspx.vb" Inherits="WebApplication.UserRole" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">

    <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;" DefaultButton="btnSearch1">
        <div class="row">
            <div class="col-sm-4">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon">Search Type </span>
                        <asp:DropDownList ID="ddlRole" runat="server" class="form-control" placeholder="Type" name="Type" type="Type">
                            <asp:ListItem>Role Code</asp:ListItem>
                            <asp:ListItem>Role name</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-4">
                <div class="panel-body">
                    <div class="input-group">
                        <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>

                        <asp:TextBox ID="txtRole" runat="server" onkeypress="return isKey(event)"
                            MaxLength="50" class="form-control" placeholder="Search..." name="Search..." type="Search..." data-toggle="tooltip" data-placement="top" title="Search role"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server" data-toggle="tooltip" data-placement="top" title="Search data"><i class="fa fa-search"></i></asp:LinkButton></span>
                    </div>
                </div>
            </div>
            <div class="col-sm-2">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon"><i class='fa  fa-plus-square  fa-fw'></i></span>
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Add New" data-toggle="tooltip" data-placement="top" title="Click for add new role" />
                    </div>
                </div>
            </div>
        </div>
        <div id="DivBody">
            <div class="col-sm-12">
                <div class="panel panel-default">

                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Role <small>List</small></h3>

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
                                    <asp:GridView ID="gridRole" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                        DataKeyNames="RoleCode" EmptyDataRowStyle-CssClass="empty_data"
                                        EmptyDataText="No data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Role Code" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RoleCode")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "RoleCode")%></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RoleDesc" HeaderText="Nama Role" />
                                            <asp:BoundField DataField="Admin" HeaderText="Admin" />
                                            <asp:BoundField DataField="lvlAdmin" HeaderText="Level Admin" />
                                            <asp:TemplateField HeaderText="Status" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>

                                                    <asp:HiddenField ID="hfstatus" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IsActive")%> ' />
                                                    <asp:LinkButton ID="ImgViewActive" runat="server" CausesValidation="False"
                                                        data-toggle="tooltip" data-placement="top" title='<%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "Inactive", "Actived")%>'
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RoleCode")%>' CommandName="UpdateLink"><i class='fa fa-signout fa-fw'></i>
                                            <%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "<i class='fa fa-check fa-fw'></i>", "<i class='fa fa-times fa-fw'></i>")%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField>
                                                <HeaderTemplate>
                                                    ISACTIVE
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CBISACTIVE" runat="server" Checked='<%# Eval("ISACTIVE")%>' CssClass="flat-red" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
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


    <asp:Panel ID="pnlPopup" runat="server" DefaultButton="btnSave" Visible="False">
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Submit" Visible="False" />
        <div id="DivBody1">
            <table class="table table-striped table-bordered table-hover" id="mGridPict">
                <tr>
                    <td style="vertical-align: central; text-align: center;">
                        <asp:LinkButton ID="LinkClose" runat="server" Text="" data-toggle="tooltip" data-placement="top" title="Close Page"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-times" ></i> Exit</p></asp:LinkButton>
                    </td>
                    <td style="vertical-align: central; text-align: center;">
                        <asp:LinkButton ID="LinkSubmit" runat="server" Text="" data-toggle="tooltip" data-placement="top" title="Submit Data"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-check" ></i> Submit</p></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Role Code <i class="fa fa-code fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtRoleCode" runat="server" MaxLength="5" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Role Code" class="form-control" placeholder="Role Code.." name="Role Code.." type="Role Code.."></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Role Description <i class="fa fa-align-justify fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtRoleDesc" runat="server" MaxLength="50" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Role Description" class="form-control" placeholder="Role Description.." name="Role Description.." type="Role Description.."></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon">Level Admin <i class="fa fa-android fa-fw"></i></span>
                                <asp:DropDownList ID="DdlLvlAdmin" runat="server" AutoPostBack="True" class="form-control" placeholder="Level Admin" name="Level Admin" type="Level Admin">
                                </asp:DropDownList>
                            </div>
                            <div class="panel-footer">
                                <asp:CheckBox ID="chkAdmin" runat="server" Text="Admin" data-toggle="tooltip" data-placement="top" title="Click for Admin or Not" CssClass="flat-red" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon">Menu header <i class="fa fa-sitemap fa-fw"></i></span>
                                <asp:DropDownList ID="ddlMenuParent" runat="server" AutoPostBack="True" class="form-control" placeholder="Menu header" name="Menu header" type="Menu header">
                                </asp:DropDownList>
                            </div>
                            <div class="panel-footer">
                                <asp:CheckBox ID="chkAktiv" runat="server" Checked="True" Text="ISACTIVE?" data-toggle="tooltip" data-placement="top" title="Click for ACTIVE / Non ACTIVE Role" CssClass="flat-red" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-body">
                            <div style="overflow-y: scroll; height: 250px;">
                                <asp:DataGrid ID="gridMenu" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" CssClass="table table-striped table-bordered table-hover" ItemStyle-HorizontalAlign="Left">
                                    <PagerStyle CssClass="pagination" HorizontalAlign="Left" Mode="NumericPages" NextPageText="Next" PageButtonCount="5" Position="Bottom" PrevPageText="Prev" />

                                    <Columns>
                                        <asp:BoundColumn DataField="MenuCode" HeaderText="Menu Code">
                                            <HeaderStyle />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <input type="checkbox" id="chk" checked='<%# Eval("Chk")%>' runat="server" onclick="CheckChanged();" class="flat-red" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                Select
                                            </HeaderTemplate>
                                            <HeaderStyle Width="20px" />
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="MenuType" HeaderText="Menu Type">
                                            <HeaderStyle />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ChildsCaption" HeaderText="Menu name"></asp:BoundColumn>
                                    </Columns>
                                    <HeaderStyle CssClass="header" HorizontalAlign="Center" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
