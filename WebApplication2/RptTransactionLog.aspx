<%@  Page Title="Menu | Report | Report Transaction" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RptTransactionLog.aspx.vb" Inherits="WebApplication.RptTransactionLog" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">


        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel-body">

                        <div class="form-group">
                            <div class="input-group">

                                <span class="input-group-addon">Date in range:</span>

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
                            <asp:TextBox ID="txtKey" runat="server" data-toggle="tooltip" data-placement="top" title="Original Keyword" class="form-control" placeholder="Key.." name="Key.." type="Key.."></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server"  data-toggle="tooltip" data-placement="top" title="Search Data"><i class="fa fa-search"></i></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
 
</asp:Content>
