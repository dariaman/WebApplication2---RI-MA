<%@ Page Title="File | Master | Product List" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Produk.aspx.vb" Inherits="WebApplication.Produk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    <div id="DivBody">
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div class="row">
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Search Type </span>
                            <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" placeholder="Type" name="Type" type="Type" data-toggle="tooltip" data-placement="top" title="Select type">
                                <asp:ListItem Value="Kode">Type Product</asp:ListItem>
                                <asp:ListItem Value="Name">Descirption</asp:ListItem>
                                <asp:ListItem Value="Product">Product</asp:ListItem>
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
                                <asp:LinkButton ID="btnSearch1" runat="server" CssClass="btn btn-primary btn-block btn-flat" data-toggle="tooltip" data-placement="top" title="Search data"><i class="fa fa-search"></i></asp:LinkButton>
                            </span>

                        </div>
                    </div>
                </div>
                <div class="col-sm-2">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa  fa-plus-square  fa-fw'></i></span>
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Add New"
                                data-toggle="tooltip" data-placement="top" title="Click for add new" />
                        </div>
                    </div>
                </div>
                <br />
            </div>
            <div class="col-sm-12">
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">Product <small>List</small></h3>

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
                                    DataKeyNames="ID" EmptyDataRowStyle-CssClass="empty_data" PagerStyle-CssClass="pagination"
                                    EmptyDataText="No data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False"  data-toggle="tooltip" data-placement="top" title="View Record"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "ID")%> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TypeProduk" HeaderText="Type Product" />
                                        <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" />
                                        <asp:BoundField DataField="Produk" HeaderText="Product" />
                                        <asp:BoundField DataField="UnitType" HeaderText="Unit Type" />
                                       
                                            <asp:TemplateField HeaderText="Status" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>

                                                    <asp:HiddenField ID="hfstatus" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IsActive")%> ' />
                                                    <asp:LinkButton ID="ImgViewActive" runat="server" CausesValidation="False"
                                                        data-toggle="tooltip" data-placement="top" title='<%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "Inactive", "Actived")%>'
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID")%>' CommandName="UpdateLink"><i class='fa fa-signout fa-fw'></i>
                                            <%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "<i class='fa fa-check fa-fw'></i>", "<i class='fa fa-times fa-fw'></i>")%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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
                            <asp:Label ID="lblID" runat="server"></asp:Label>
                        </div>
                        <div class="panel-body">

                            <div class="form-group input-group">
                                <span class="input-group-addon">Type Product</span>
                                <asp:DropDownList ID="ddlTypeProduct" runat="server" class="form-control" name="Type" placeholder="Type" data-toggle="tooltip" data-placement="top" title="Select Search Type" type="Type">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Product <i class="fa fa-cube fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="TxtProduct" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Product" MaxLength="250" class="form-control" placeholder="Product.." name="Product.." type="Product.."></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-body">
                            <asp:TextBox ID="TxtDescription" runat="server" onKeyUp="javascript:Check(this, 250);"
                                onChange="javascript:Check(this, 250);" data-toggle="tooltip" data-placement="top" title="Input Description" MaxLength="250" Height="69px" TextMode="MultiLine" CssClass="form-control" placeholder="Description.." name="Description.." type="Description.." onkeypress="return isKey(event)"></asp:TextBox>
                        
                        </div>
                        <div class="panel-footer">
                            <asp:CheckBox ID="chkPlus" runat="server" Checked="True" Text="Plus" CssClass="flat-red" />
                            &nbsp;<asp:CheckBox ID="chkAktiv" runat="server" Checked="True" Text="IsActive" data-toggle="tooltip" data-placement="top" title="Click For Active /Non Active Product" CssClass="flat-red" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-4">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon">Price (Rp) <i class="fa fa-money fa-fw"></i></span>
                                <asp:TextBox ID="TxtPrice" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input real price" MaxLength="10" onfocus="removeCommas(this)" OnBlur="addCommas(this)" class="form-control" placeholder="Price.." name="Price.." type="Price.."></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="TxtPrice_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" TargetControlID="TxtPrice" ValidChars="1234567890.,">
                                </asp:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon">Price Sales (Rp) <i class="fa fa-money fa-fw"></i></span>
                                <asp:TextBox ID="Txtpricesale" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input price sale" MaxLength="10" onfocus="removeCommas(this)" OnBlur="addCommas(this)" class="form-control" placeholder="Price Sale.." name="Price Sale.." type="Price Sale.."></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="Txtpricesale_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" TargetControlID="Txtpricesale" ValidChars="1234567890.,">
                                </asp:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon">Stock <i class="fa  fa-cubes fa-fw"></i></span>
                                <asp:TextBox ID="Txtitemstock" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Stock" MaxLength="5" class="form-control" placeholder="Stock.." name="Stock.." type="Stock.."></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="Txtitemstock_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" TargetControlID="Txtitemstock" ValidChars="1234567890.,">
                                </asp:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon">Product Unit</span>
                                <asp:DropDownList ID="DDLProductUnit" runat="server" class="form-control" name="Product Unit" placeholder="Product Unit" data-toggle="tooltip" data-placement="top" title="Select Product Unit" type="Product Unit">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Submit" Visible="False" />
    </asp:Panel>
</asp:Content>
