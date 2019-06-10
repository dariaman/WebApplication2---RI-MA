<%@ Page Title="Report | Report | TOP 10 DIANGNOSIS" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportTopDiagnos.aspx.vb" Inherits="WebApplication.ReportTopDiagnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    <div id="DivBody">
        <asp:Panel ID="PnlPolicy" runat="server" Style="background: transparent;">
            
            <div class="row">
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Sort By </span>
                            <asp:RadioButtonList ID="RBOption" runat="server">
                                <asp:ListItem Value="0" Selected="True">Claim Amount</asp:ListItem>
                                <asp:ListItem Value="1">Total Case</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Policy </span>
                            <asp:DropDownList ID="DDLPolicy" runat="server" class="form-control" placeholder="Policy No...." name="Policy No...." type="Policy No....">
                            </asp:DropDownList>
                            <asp:TextBox ID="TxtKeyWord" runat="server" class="form-control" placeholder=" Policy no..." name=" Policy no..." type=" Policy no..." data-toggle="tooltip" data-placement="top" title="Input Policy no" ></asp:TextBox>
                        </div>
                    </div>
                </div>


                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon">Date range:</span>
                                <asp:TextBox ID="reservation" runat="server" class="form-control pull-right" placeholder="Date.." name="Date.." type="Date.." data-toggle="tooltip" data-placement="top" title="Input Date"></asp:TextBox>
                                
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2">
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="input-group">
                                    <asp:Button ID="btnPrint" runat="server" Text="Print Report" CssClass="btn btn-primary btn-block btn-flat" />
                                    <%--<asp:LinkButton ID="btnPrint" CssClass="btn-flat btn-primary btn-block " runat="server" data-toggle="tooltip" data-placement="top" title="Print Data"><i class="fa fa-print"> </i>Print Report</asp:LinkButton>--%>
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
        </asp:Panel>
        
    </div>


</asp:Content>
