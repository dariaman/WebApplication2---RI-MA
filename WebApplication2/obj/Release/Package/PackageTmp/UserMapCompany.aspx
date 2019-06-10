<%@ Page Title="File | Setting | Company Map" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserMapCompany.aspx.vb" Inherits="WebApplication.UserMapCompany" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    
    <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;" DefaultButton="btnSearch1">
        <div class="row">
            <div class="col-sm-4">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon">Company <i class="fa fa-building fa-fw"></i></span>
                            <asp:DropDownList ID="ddlType" runat="server" class="form-control" name="Company" placeholder="Company" type="Company">
                                <asp:ListItem>Id</asp:ListItem>
                                <asp:ListItem>Name</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlType" runat="server" class="form-control" name="Company" placeholder="Company" type="Company">
                            <asp:ListItem>Id</asp:ListItem>
                            <asp:ListItem>Name</asp:ListItem>
                        </asp:DropDownList>--%>
                    </div>
                </div>
            </div>
            <div class="col-sm-4">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                        <asp:TextBox ID="txtKeyword" runat="server" onkeypress="return isKey(event)" class="form-control" placeholder="Company ID Or Name" name="Company ID Or Name" type="Company ID Or Name" data-toggle="tooltip" data-placement="top" title="Masukan Keyword"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server" data-toggle="tooltip" data-placement="top" title="Search Data"><i class="fa fa-search"></i></asp:LinkButton></span>
                    </div>
                </div>
            </div>
            <%--<div class="col-sm-2">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon"><i class='fa  fa-plus-square  fa-fw'></i></span>--%>
                        <asp:Button ID="btnAdd" CssClass="btn btn-primary btn-block btn-flat" runat="server" Text="Add New" data-toggle="tooltip" data-placement="top" title="Click for add new" Visible="False" />
                    <%--</div>
                </div>
            </div>--%>
            <br />
            <div class="row">
            </div>
            <div id="DivBody">
            <div class="col-sm-12">
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">Company <small>List</small></h3>

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
                                <asp:GridView ID="DGuser" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                    DataKeyNames="CompanyId" EmptyDataRowStyle-CssClass="empty_data"
                                    EmptyDataText="No data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Company Id" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="ImgViewCompanyId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CompanyId")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "CompanyId")%></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                        <asp:BoundField DataField="CompanyAddress" HeaderText="Company Address" />
                                        <%--<asp:TemplateField>
                                            <HeaderTemplate>
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
</div>
    </asp:Panel>
    <asp:Panel ID="pnlPopup" runat="server" Visible="false" DefaultButton="LinkSubmit">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td style="vertical-align: central; text-align: center;">
                    <asp:LinkButton ID="LinkClose" runat="server" Text="" data-toggle="tooltip" data-placement="top" title="Close Page"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-times" ></i> Exit</p></asp:LinkButton>
                </td>
                <td style="vertical-align: central; text-align: center;">
                    <asp:LinkButton ID="LinkSubmit" runat="server" Text="" data-toggle="tooltip" data-placement="top" title="Submit Data" Visible="False"></asp:LinkButton>
                </td>
            </tr>
        </table>

        <div class="row">
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Company Name <i class="fa fa-building-o fa-fw"></i></span>
                            <asp:TextBox ID="txtCompanyName" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Comapny Name" MaxLength="100" class="form-control" placeholder="User Name.." name="User Name.." type="User Name.." ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>  
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Company ID<i class="fa fa-code fa-fw"></i></span>
                            <asp:TextBox ID="txtComapnyId" runat="server" onkeypress="return isKey(event)" MaxLength="10" data-toggle="tooltip" data-placement="top" title="Autogenerate User id" class="form-control" placeholder="User ID.." name="User ID.." type="User ID.." ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Policy No <i class="fa fa-files-o fa-fw"></i></span>
                            <asp:TextBox ID="TxtPolicyNo" runat="server" data-toggle="tooltip" data-placement="top" title="Input Policy No.." MaxLength="20" CssClass="form-control" placeholder="Policy No.." name="Policy No.." type="Policy No.." onkeypress="return isKey(event)"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Additional Email Claim Foto <i class="fa fa-mail-o fa-fw"></i></span>
                            <asp:TextBox ID="txtPICEmailPolicy" runat="server" data-toggle="tooltip" data-placement="top" title="PIC Email Policy.." MaxLength="20" CssClass="form-control" placeholder="PIC Email Policy.." name="PIC Email Policy.." type="PIC Email Policy.." onkeypress="return isKey(event)"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-footer">
                            <asp:CheckBox ID="chkNotClaimPhoto" runat="server" Checked="false" Text="Disable Claim Photo" data-toggle="tooltip" data-placement="top" title="Not Claim Photo" />
                        </div>
                    </div>
            </div>
            <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-footer">
                            <asp:CheckBox ID="chkTPA" runat="server" Checked="false" Text="TPA" data-toggle="tooltip" data-placement="top" title="TPA" AutoPostBack="True" Visible="False" />
                        </div>
                    </div>
            </div>
        </div>
        <div class="row">
            <asp:Panel runat="server" ID="PnlTPA" Visible="false">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon">TPA NAME <i class="fa fa-files-o fa-fw"></i></span>
                                <asp:TextBox ID="txtTPAName" runat="server" data-toggle="tooltip" data-placement="top" title="Input TPA NAME.." MaxLength="20" CssClass="form-control" placeholder="TPA NAME.." name="TPA NAME.." type="TPA NAME.." onkeypress="return isKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <asp:CheckBox ID="chkisEmailTPA" runat="server" Checked="false" Text="Email TPA" data-toggle="tooltip" data-placement="top" title="Email TPA" AutoPostBack="True" Visible="True" />
                            <asp:CheckBox ID="chkisFTPTPA" runat="server" Checked="false" Text="FTP TPA" data-toggle="tooltip" data-placement="top" title="Email TPA" AutoPostBack="True" Visible="True" />
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <asp:TextBox ID="txtTPAEmailAddress" runat="server" data-toggle="tooltip" data-placement="top" title="Input TPA Email Address.." MaxLength="20" CssClass="form-control" placeholder="Email Address.." name="Email Address.." type="Email Address.." onkeypress="return isKey(event)" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <asp:TextBox ID="TxtTPAFTPAddress" runat="server" data-toggle="tooltip" data-placement="top" title="Input TPA Ftp Address.." MaxLength="20" CssClass="form-control" placeholder="Ftp Address.." name="Ftp Address.." type="Ftp Address.." onkeypress="return isKey(event)" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <div class="panel-body">
                    <div style="overflow-y: scroll; height: 100px; padding-left: 5px;">
                        <asp:DataGrid ID="DGDetailLog" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" CssClass="table table-striped table-bordered table-hover" ItemStyle-HorizontalAlign="Left" Width="98%">
                            <ItemStyle HorizontalAlign="Left" />
                            <PagerStyle CssClass="pagination" HorizontalAlign="Left" Mode="NumericPages" NextPageText="Next" PageButtonCount="5" Position="Bottom" PrevPageText="Prev" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "POLICYNO")%>' CommandName="SelectLink"><i class='fa  fa-times fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "POLICYNO")%></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="POLICYNO" HeaderText="POLICY NO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="isNotClaimFoto" HeaderText="Disable Claim Foto"></asp:BoundColumn>
                                <%--<asp:BoundColumn DataField="TPAName" HeaderText="TPA NAME"></asp:BoundColumn>
                                <asp:BoundColumn DataField="IsTPA" HeaderText="is TPA"></asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="PICEmailPolicy" HeaderText="Additional Email Claim Foto"></asp:BoundColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


    <div class="row" >
        <div class="col-sm-12">
            <div class="panel-body">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Submit"  />
            </div>
        </div>
    </div>

</asp:Content>
