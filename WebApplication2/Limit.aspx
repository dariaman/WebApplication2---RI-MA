 <%@  Page Title="File | General | Branch List" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Limit.aspx.vb" Inherits="WebApplication.Limit" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div class="row">
               <%-- <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Search Type </span>
                            <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" placeholder="Search By" name="Search By" type="Search By">
                                <asp:ListItem Value="Kode">Policy No</asp:ListItem>
                                <asp:ListItem Value="Nama">Limit</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>--%>
                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                            <asp:TextBox ID="TxtKeyWord" runat="server" class="form-control" placeholder="Search Policy No or Limit..." name="Search Policy No or Limit..." type="Search Policy No or Limit..." data-toggle="tooltip" data-placement="top" title="Input Key" ></asp:TextBox>
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
                            <h3 class="box-title">Limit <small>List</small></h3>
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
                                            DataKeyNames="PolicyNo" EmptyDataRowStyle-CssClass="empty_data" 
                                            EmptyDataText="No data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                       <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PolicyNo")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "PolicyNo")%></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CLIENTNAME" HeaderText="CLIENT NAME"  />
                                                <asp:BoundField DataField="Limit" HeaderText="Limit" DataFormatString="{0:N0}" />
                                                <asp:TemplateField HeaderText="Status" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfstatus" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IsActive")%> ' />
                                                    <asp:LinkButton ID="ImgViewActive" runat="server" CausesValidation="False"
                                                        data-toggle="tooltip" data-placement="top" title='<%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "Inactive", "Actived")%>'
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PolicyNo")%>' CommandName="UpdateLink"><i class='fa fa-signout fa-fw'></i>
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
        
    <asp:Panel ID="pnlPopup" runat="server" Visible="false"   DefaultButton="btnSave">
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
                            Policy No <i class="fa fa-code fa-fw"></i>
                        </div>
                          <div class="panel-body">
                            <asp:TextBox ID="txtPolicyNo" runat="server" MaxLength="20" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Input Policy No" CssClass="form-control" placeholder="PolicyNo.." name="PolicyNo.." type="PolicyNo.."></asp:TextBox>
                              <span class="input-group-btn">
                                <asp:LinkButton ID="Search1" CssClass="btn btn-primary btn-block btn-flat" runat="server"  data-toggle="tooltip" data-placement="top" title="Search Data" ><i class="fa fa-search"></i></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            CLIENT NAME <i class="fa fa-smile-o fa-fw"></i>
                        </div>
                          <div class="panel-body">
                              <asp:Label ID="LblCLIENTNAME" runat="server" Text=""></asp:Label>
                          </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Limit <i class="fa fa-bar-chart-o fa-fw"></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtLimit" runat="server" data-toggle="tooltip" data-placement="top" title="Input limit"  onkeypress="return isKeyNumber(event)"  onfocus="removeCommas(this)" onblur="addCommas2(this)"   MaxLength="10" CssClass="form-control" placeholder="Limit.." name="Limit.." type="Limit.." ></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

          
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        
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
