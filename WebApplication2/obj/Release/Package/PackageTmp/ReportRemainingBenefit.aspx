<%@ Page Title="Report | Report | REMAINING BENEFIT" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportRemainingBenefit.aspx.vb" Inherits="WebApplication.ReportRemainingBenefit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    <div id="DivBody">
        <asp:Panel ID="PnlPolicy" runat="server" Style="background: transparent;">
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
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Benefit </span>
                            <asp:DropDownList ID="DDLRemaining" runat="server" class="form-control" placeholder="Policy No...." name="Policy No...." type="Policy No....">
                                <asp:ListItem Value="OP">Remaining Benefit OP</asp:ListItem>
                                <asp:ListItem Value="IP">Remaining Benefit IP</asp:ListItem>
                                <asp:ListItem Value="MT">Remaining Benefit MT</asp:ListItem>
                                <asp:ListItem Value="GL">Remaining Benefit GL</asp:ListItem>
                                <asp:ListItem Value="DT">Remaining Benefit DT</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            
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
