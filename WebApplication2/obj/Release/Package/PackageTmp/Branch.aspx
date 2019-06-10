 <%@  Page Title="File | General | Branch List" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Branch.aspx.vb" Inherits="WebApplication.Branch" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div class="row">
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Search Type </span>
                            <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" placeholder="Search By" name="Search By" type="Search By">
                                <asp:ListItem Value="Kode">Branch Code</asp:ListItem>
                                <asp:ListItem Value="Nama">Branch name</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                            <asp:TextBox ID="TxtKeyWord" runat="server" class="form-control" placeholder="Search..." name="Search..." type="Search..." data-toggle="tooltip" data-placement="top" title="Input Key" ></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server"  data-toggle="tooltip" data-placement="top" title="Search Data" ><i class="fa fa-search"></i></asp:LinkButton></span>
                        </div> 
                    </div>

                </div>
                <div class="col-sm-2">
                    <div class="panel-body">
                        <div class="form-group input-group"><span class="input-group-addon"><i class='fa fa-plus-square fa-fw'></i></span><asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Add New"
                            data-toggle="tooltip" data-placement="top" title="Click for add new branch" />
                            </div>
                    </div>
                </div>
            </div>
    <div id="DivBody">
            <div class="row">
                <div class="col-sm-12">

                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Branch <small>List</small></h3>
                            
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
                                            DataKeyNames="BranchCode" EmptyDataRowStyle-CssClass="empty_data" 
                                            EmptyDataText="No data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                       <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BranchCode")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "BranchCode")%></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Branchname" HeaderText="Name" />
                                                <asp:BoundField DataField="BranchAbbreviation" HeaderText="Abbreviation" />
                                                <asp:BoundField DataField="BranchAdd" HeaderText="Address" />
                                                <asp:BoundField DataField="BranchCity" HeaderText="City" />
                                                <asp:BoundField DataField="BranchZIP" HeaderText="ZIP" />
                                                <asp:BoundField DataField="BranchPhone" HeaderText="Phone" />
                                                <asp:BoundField DataField="BranchFax" HeaderText="Fax" />
                                                <asp:BoundField DataField="NPWP" HeaderText="NPWP" />
                                                <asp:TemplateField HeaderText="Status" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>

                                                    <asp:HiddenField ID="hfstatus" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IsActive")%> ' />
                                                    <asp:LinkButton ID="ImgViewActive" runat="server" CausesValidation="False"
                                                        data-toggle="tooltip" data-placement="top" title='<%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "Inactive", "Actived")%>'
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BranchCode")%>' CommandName="UpdateLink"><i class='fa fa-signout fa-fw'></i>
                                            <%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "<i class='fa fa-check fa-fw'></i>", "<i class='fa fa-times fa-fw'></i>")%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                    <%--<HeaderTemplate>
                                                        IsActive
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CBIsActive" runat="server" Checked='<%# Eval("IsActive")%>' Enabled="false" CssClass="flat-red" />
                                                    </ItemTemplate>--%>
                                                </asp:TemplateField>
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
        
    <asp:Panel ID="pnlPopup" runat="server" Visible="false" Style="display: none;"  DefaultButton="btnSave">
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
                            Branch Code <i class="fa fa-code fa-fw"></i>
                        </div>
                          <div class="panel-body">
                            <asp:TextBox ID="txtBranchCode" runat="server" MaxLength="5" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Station Code" CssClass="form-control" placeholder="Branch Code.." name="Branch Code.." type="Branch Code.."></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Branch name <i class="fa fa-building fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtBranchname" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Station name" MaxLength="30" CssClass="form-control" placeholder="Branch name.." name="Branch name.." type="Branch name.."></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Abbreviation <i class="fa fa-building fa-fw"></i>&nbsp;<asp:TextBox ID="txtBranchAbbreviation" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Abbreviation" MaxLength="10" CssClass="form-control" placeholder="Abbreviation.." name="Abbreviation.." type="Abbreviation.."></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Address <i class="fa fa-road fa-fw"></i>&nbsp;<asp:TextBox ID="txtBranchAdd" runat="server" onKeyUp="javascript:Check(this, 400);"
                                onChange="javascript:Check(this, 400);" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Address" MaxLength="500" Height="69px" TextMode="MultiLine" CssClass="form-control" placeholder="Address.." name="Address.." type="Address.."></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            City <i class="fa fa-road fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtBranchCity" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input City" MaxLength="20" CssClass="form-control" placeholder="City.." name="City.." type="City.."></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            ZIP
                        <i class="fa fa-envelope fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtBranchZIP" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Zip" MaxLength="10" CssClass="form-control" placeholder="Zip.." name="Zip.." type="Zip.."></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Phone No
                        <i class="fa fa-phone fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtBranchPhone" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Phone Number" MaxLength="20" CssClass="form-control" placeholder="Phone.." name="Phone.." type="Phone.." data-inputmask='"mask": "(9999) 999-99-999"' data-mask></asp:TextBox>

                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Fax No  
                        <i class="fa fa-fax fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtBranchFax" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input fax Number " MaxLength="20" CssClass="form-control" placeholder="Fax.." name="Fax.." type="Fax.." data-inputmask='"mask": "(9999) 999-99-999"' data-mask></asp:TextBox>

                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                            Note
                        <i class="fa fa-pencil-square-o fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtNPWP" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Note" MaxLength="20" CssClass="form-control" placeholder="Note.." name="Note.." type="Note.."></asp:TextBox>
                        </div>
                        <div class="panel-footer">
                            <asp:CheckBox ID="chkAktiv" runat="server" Checked="True" Text="IsActive" data-toggle="tooltip" data-placement="top" title="Click For Active /Non Active Station" CssClass="flat-red" />
                        </div>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Submit" Visible="False" />
        </div>
    </asp:Panel>
</asp:Content>
