<%@ Page Title="Info | Info | Info Claim" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PolicyMemberKlaim.aspx.vb" Inherits="WebApplication.PolicyMemberKlaim" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    
        <asp:Panel ID="PnlPolicy" runat="server" Style="background: transparent;">
            <div class="row">
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Policy </span>
                            <asp:DropDownList ID="DDLPolicy" runat="server" class="form-control" placeholder="Policy No...." name="Policy No...." type="Policy No....">
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;" DefaultButton="btnSearch1">
            <div class="row">
                <asp:Panel ID="PnlSearch" runat="server" Style="background: transparent;">
                    <div class="col-sm-4">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon">Search Type </span>
                                <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" placeholder="Search By" name="Search By" type="Search By">
                                    <asp:ListItem Value="1">MEMBER ID</asp:ListItem>
                                    <asp:ListItem Value="2">EMPLOYEE ID</asp:ListItem>
                                    <asp:ListItem Value="3">MEMBER NAME</asp:ListItem>
                                    <asp:ListItem Value="4">EMPLOYEE NAME</asp:ListItem>
                                    <asp:ListItem Value="6">POLICY NO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="col-sm-6">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                                <asp:TextBox ID="TxtKeyWord" runat="server" class="form-control" placeholder="Search..." name="Search..." type="Search..." data-toggle="tooltip" data-placement="top" title="Input Key"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server" data-toggle="tooltip" data-placement="top" title="Search Data"><i class="fa fa-search"></i></asp:LinkButton></span>
                            </div>
                        </div>

                    </div>
                </asp:Panel>
                <%--<div class="col-sm-2">
                    <div class="panel-body">
                        <div class="form-group input-group"><span class="input-group-addon"><i class='fa fa-plus-square fa-fw'></i></span><asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Add New"
                            data-toggle="tooltip" data-placement="top" title="Click for add new branch" />
                            </div>
                    </div>
                </div>--%>
            </div>

            <div id="DivBody">
            <div class="row">
                <div class="col-sm-12">

                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Claim <small>List</small></h3>

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
                                    <asp:GridView ID="GVVw_Claim_Info_Header" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" DataKeyNames="CLAIMNO" EmptyDataRowStyle-CssClass="empty_data" EmptyDataText="No data Found" >
                                        <columns>
                                            <asp:TemplateField HeaderText="CLAIM NO" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ImgViewUserId0" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CLAIMNO")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "CLAIMNO")%></asp:LinkButton>
                                                    <asp:HiddenField ID="GFMEMBID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "MEMBID")%> ' />
                                                    <asp:HiddenField ID="GFPolicyNo" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "POLICYNO")%> ' />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FULLNAME" HeaderText="MEMBER NAME" />
                                            <asp:BoundField DataField="DOR" HeaderText="DATE OF RECEIPT" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="BILLEDAMT" HeaderText="CLAIM AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="ACCEPTAMT" HeaderText="ACCEPTED AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="ACCEPTAMT" HeaderText="PAID AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="UNPAIDAMT" HeaderText="UNPAID AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="PAIDDT" HeaderText="PAID AMOUNT" DataFormatString="{0:dd-MMM-yyyy}" />
                                            
                                        </columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </div>
        </asp:Panel>
        
    <asp:Panel ID="pnlPopup" runat="server" Visible="false" DefaultButton="LinkClose">
        <div id="DivBody1">
            <table class="table table-striped table-bordered table-hover" id="mGridPict">
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkClose" runat="server" Text="" data-toggle="tooltip" data-placement="top" title="Close Page"> <p style="line-height: 0%; text-align: center;"><i class="fa fa-times"></i>Exit</p></asp:LinkButton>
                    </td>
                    <%-- <td>
                            <asp:LinkButton ID="LinkSubmit" runat="server" Text="" data-toggle="tooltip" data-placement="top" title="Submit Data" > <p style="line-height: 0%;text-align: center;"><i class="fa fa-check" ></i> Submit</p></asp:LinkButton>
                        </td>--%>
                </tr>
            </table>
            <div class="row">
                <div class="col-sm-12">
                    
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">MEMBER <small>INFORMATION</small></h3>

                            <div class="pull-right box-tools">
                                <button class="btn btn-info btn-sm" data-toggle="tooltip" data-widget="collapse" title="Collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                            </div>

                        </div>
                        
                        <div class="box-body pad">
                            <div class="row">
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-user"></i>&nbsp; MEMBER &nbsp; 
                                             :</b>&nbsp; 
                                        <asp:Label ID="LblPeserta" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                    
                                </div>
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-link"></i>&nbsp; RELATION &nbsp;  
                                            :</b>&nbsp; 
                                        <asp:Label ID="LblRelasi" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>

                                </div>
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-file-o"></i>&nbsp; POLICY &nbsp;  
                                            :</b>&nbsp; 
                                        <asp:Label ID="LblPOLIS" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-tags"></i>&nbsp; STATUS &nbsp;                            
                                            :</b>&nbsp; 
                                        <asp:Label ID="LblStatus" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>

                                <div class="col-sm-4" style="color:#1c578a">
                                        <b>
                                            <i class='fa fa-male'></i><i class='fa fa-female'></i>
                                            &nbsp; JENIS KELAMIN &nbsp; 
                                            :</b>&nbsp; 
                                        <asp:Label ID="LblSex" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>

                                </div>
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-credit-card"></i>&nbsp; MEMBER ID &nbsp; 
                                            :</b>&nbsp; 
                                        <asp:Label ID="LblMemID" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-credit-card"></i>&nbsp; SUB GROUP &nbsp; 
                                            :</b>&nbsp; 
                                        <asp:Label ID="LblClientNm" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-calendar"></i>&nbsp; BIRTH DATE &nbsp; 
                                            :</b>&nbsp; 
                                        <asp:Label ID="LblBirthDate" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>

                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-list-alt"></i>&nbsp; EMPLOYEE ID &nbsp; 
                                            :</b>&nbsp; 
                                        <asp:Label ID="LblNIK" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4" style="color:#1c578a">
                                        <b><i class="fa fa-book"></i>&nbsp; ACCOUNT NO &nbsp; 
                                            :</b>&nbsp; 
                                        <asp:Label ID="LblAccNo" runat="server" Text="" BackColor="#ffffcc" ForeColor="#000066"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Claim Detail <small> INFORMATION</small></h3>

                            <div class="pull-right box-tools">
                                <button class="btn btn-info btn-sm" data-toggle="tooltip" data-widget="collapse" title="Collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                            </div>

                        </div>

                        <div class="box-body pad">
                            <div class="panel-body">
                                <div class="dataTable_wrapper">
                                    <br />
                                    <asp:GridView ID="GVClaim_Info_Detail" runat="server"  AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" DataKeyNames="CLAIMNO" EmptyDataRowStyle-CssClass="empty_data" EmptyDataText="No data Found" >
                                        <columns>
                                            <asp:BoundField DataField="SEQNO" HeaderText="NO" />
                                            <asp:BoundField DataField="BENEFITNM2" HeaderText="BENEFIT" />
                                            <asp:BoundField DataField="BILLEDAMT" HeaderText="CLAIM AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="ACCEPTAMT" HeaderText="ACCEPTED AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="ACCEPTAMT" HeaderText="PAID AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="UNPAIDAMT" HeaderText="UNPAID AMOUNT" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="DIAGDESC" HeaderText="DIAGNOSE" />
                                            <asp:BoundField DataField="REMARK" HeaderText="REMARK" />
                                            <asp:BoundField DataField="ADMISSIONDT" HeaderText="ADMISSION DATE" DataFormatString="{0:dd-MMM-yyyy}" />
                                        </columns>
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
