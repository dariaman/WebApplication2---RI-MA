<%@  Page Title="File | Setting | History" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="History1.aspx.vb" Inherits="WebApplication.History1" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
   
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel-body">

                        <div class="form-group">
                            <div class="input-group">

                                <span class="input-group-addon">Date range:</span>

                                <asp:TextBox ID="reservation" runat="server" class="form-control pull-right" placeholder="Date.." name="Date.." type="Date.."  data-toggle="tooltip" data-placement="top" title="Input Date"></asp:TextBox>
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                            <asp:TextBox ID="txtKey" runat="server" data-toggle="tooltip" data-placement="top" title="Input Key,No , User Id, code Product etc" class="form-control" placeholder="Key.." name="Key.." type="Key.."></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server"  data-toggle="tooltip" data-placement="top" title="Search Data"><i class="fa fa-search"></i></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
            </div>
             <div id="DivBody">
            <div class="row">
                <div class="col-sm-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">History <small>List</small></h3>                            
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
                                    <asp:GridView ID="gvwList" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" DataKeyNames="UserCode" EmptyDataRowStyle-CssClass="empty_data"  EmptyDataText="Tidak ada data." PageSize="25" AllowSorting="True">
                                        <Columns>
                                            <asp:BoundField DataField="CRE_DATE" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" HeaderText="Tanggal" SortExpression="CRE_DATE">
                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UserCode" HeaderText="By" SortExpression="UserCode">
                                                <ItemStyle HorizontalAlign="left" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="HistoryDesc" HeaderText="History Description" SortExpression="HistoryDesc">
                                                <ItemStyle HorizontalAlign="left" Width="90%" />
                                            </asp:BoundField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="empty_data" />
                                        <PagerStyle CssClass="pgr" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </div>
        </asp:Panel>

        
</asp:Content>
