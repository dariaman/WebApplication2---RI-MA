﻿<%@ Page Title="File | Master | Product Group List" Language="vb" AutoEventWireup="false" MasterPageFile="Site.Master"
    CodeBehind="ProdukGroup.aspx.vb" Inherits="WebApplication.ProdukGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    <div id="DivBody">
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div class="row">
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Search Type </span>
                            <asp:DropDownList ID="ddlSearch" runat="server" data-toggle="tooltip" data-placement="top" title="Select search type" class="form-control" placeholder="Type" name="Type" type="Type">
                                <asp:ListItem Value="Kode">Type Product</asp:ListItem>
                                <asp:ListItem Value="Name">Descirption</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                            <asp:TextBox ID="txtKeyWord1" runat="server" class="form-control" placeholder="Search..." name="Search..." type="Search..." data-toggle="tooltip" data-placement="top" title="Input key"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server" data-toggle="tooltip" data-placement="top" title="Search data"><i class="fa fa-search"></i></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa  fa-plus-square  fa-fw'></i></span>
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Add New"
                                data-toggle="tooltip" data-placement="top" title="Click for add new Product Group" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="col-sm-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Product Group <small>List</small></h3>
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
                                        DataKeyNames="TypeProduk" EmptyDataRowStyle-CssClass="empty_data" PagerStyle-CssClass="pagination"
                                        EmptyDataText="No data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Type Product" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ImgViewTypeProduk" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TypeProduk")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "TypeProduk")%></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" />
                                            <asp:TemplateField HeaderText="Status" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>

                                                    <asp:HiddenField ID="hfstatus" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IsActive")%> ' />
                                                    <asp:LinkButton ID="ImgViewActive" runat="server" CausesValidation="False"
                                                        data-toggle="tooltip" data-placement="top" title='<%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "Inactive", "Actived")%>'
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TypeProduk")%>' CommandName="UpdateLink"><i class='fa fa-signout fa-fw'></i>
                                            <%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "<i class='fa fa-check fa-fw'></i>", "<i class='fa fa-times fa-fw'></i>")%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <%--<asp:TemplateField>

                                              <%--  <HeaderTemplate>
                                                    IsActive
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CBIsActive" runat="server" Checked='<%# Eval("IsActive")%>' Enabled="false" CssClass="flat-red" />
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
        </asp:Panel>
    </div>
    <asp:Panel ID="pnlPopup" runat="server" Style="display: yes;" Visible="false"  DefaultButton="btnSave">
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
                            Type Produk <i class="fa fa-code fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtTypeProduk" runat="server" MaxLength="20" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Type Produk" class="form-control" placeholder="Type Produk.." name="Type Produk.." type="Type Produk.."></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Description 
                        <i class="fa fa-align-justify fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtDESCRIPTION" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Description" MaxLength="50" class="form-control" placeholder="Description.." name="Description.." type="Description.."></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-footer">
                            <asp:CheckBox ID="chkAktiv" runat="server" Checked="True" Text="IsActive" data-toggle="tooltip" data-placement="top" title="Click ForActive /Non Active Product" CssClass="flat-red" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Submit" Visible="False" />
    </asp:Panel>
</asp:Content>
