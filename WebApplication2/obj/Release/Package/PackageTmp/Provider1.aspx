 <%@  Page Title="File | General | Provider List" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Provider1.aspx.vb" Inherits="WebApplication.Provider1" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div class="row">
                <div class="col-sm-4">
                    <div class="panel-body">
                        <asp:RadioButtonList ID="RBProviderType1" runat="server" CssClass="flat-red" data-placement="top" data-toggle="tooltip" RepeatDirection="Vertical" title="Click for Group Provider" />
                    </div>

                </div>
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Search Type </span>
                            <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" placeholder="Search By" name="Search By" type="Search By">
                                <asp:ListItem Value="0">Provider Name</asp:ListItem>
                                <asp:ListItem Value="1">Provider Provice</asp:ListItem>
                                <asp:ListItem Value="2">Provider City</asp:ListItem>
                                <asp:ListItem Value="3">Provider State</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                            <asp:TextBox ID="TxtKeyWord" runat="server" class="form-control" placeholder="Search Id or Name Provider..." name="Search Id or Name Provider..." type="Search Id or Name Provider..." data-toggle="tooltip" data-placement="top" title="Input Key" ></asp:TextBox>
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
                                                <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                                                <%--<asp:BoundField DataField="LATTITUDE" HeaderText="LATTITUDE" />
                                                <asp:BoundField DataField="LONGITUDE" HeaderText="LONGITUDE" />
                                                <asp:BoundField DataField="REMARK" HeaderText="REMARK" />--%>
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
</asp:Content>
