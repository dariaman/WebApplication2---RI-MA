<%@  Page Title="File | Setting | User Profile" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserProfile.aspx.vb" Inherits="WebApplication.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    <div id="DivBody">

        <div class="row">
            <div class="col-sm-12">

                <div class="panel panel-default">
                    <div class="panel-heading">
                        User Profile
                    </div>
                    <div class="panel-body">
                        <table class="table table-bordered table-striped">
                            
                            <tr>
                                <td class="table5" colspan="2">
                                    <div class="row">
                                        <div class="col-sm-12">
                                          <%--  <div class="panel panel-default">
                                                <div class="panel-body">
                                                   <center> <asp:Image ID="Image1" runat="server" Style="height: 150px; width: 150px" CssClass="img-thumbnail img-rounded" /></center>
                                                </div>
                                            </div>--%>
 
                                        </div>
                                    </div>
                                    <div class="row" style="visibility:hidden;">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <center><asp:FileUpload ID="FileUpload1" runat="server"  CssClass="btn btn-primary btn-block btn-flat" Visible="False" />
                                                    <asp:LinkButton ID="LinkUpload" runat="server"  class="btn btn-primary btn-block btn-flat" Visible="False"><i class='fa fa-upload fa-fw'></i> Up Load</asp:LinkButton>
                                                    </center>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="table5">User Id&nbsp; 
                        <i class="fa fa-code fa-fw" ></i> 
                                </td>
                                <td class="auto-table31">
                                    <asp:Label ID="lblUserId" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table5">User Name 
                        <i class="fa fa-user fa-fw" ></i> 
                                </td>
                                <td class="auto-table31">
                                    <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table5">Email @ <i class='fa fa-envelope-o fa-fw'></i></td>
                                <td class="auto-table31">
                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table5">&nbsp;</td>
                                <td class="auto-table31">
                            <asp:LinkButton ID="LinkChangeDetail" runat="server" Text=""><p style="line-height: 0%;text-align: Left;"><i class="fa fa-user" ></i> Change User Detail</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="table5"></td>
                                <td class="auto-table31">
                            <asp:LinkButton ID="LinkChangePass" runat="server" Text=""> <p style="line-height: 0%;text-align: Left;"><i class="fa fa-lock" ></i> Change Password</p></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row" style="visibility:hidden;">
                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Question <i class='fa fa-question-circle fa-fw'></i></span>
                            <asp:DropDownList ID="ddlQuestion" runat="server" class="form-control" name="Question" placeholder="Question" >
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Answer <i class='fa  fa-comments fa-fw'></i></span>
                            <asp:TextBox ID="txtAnswer" runat="server" onkeypress="return isKey(event)" ToolTip="Answer" MaxLength="30" class="form-control" placeholder="Answer.." name="Answer.." type="Answer.."></asp:TextBox>
                            <span class="input-group-btn"><i class='fa fa-comments fa-fw'></i></span>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="visibility:hidden;">
            <div class="col-sm-12">
                <div class="panel-body">
            <div class="col-sm-2">
                <div class="form-group input-group">
                                    <span class="input-group-addon"><i class='fa fa-check-square fa-fw'></i></span>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Submit" ToolTip="Click for edit User" />

                </div>
                </div>
                </div>
            </div>
        </div>
        
    </div>
</asp:Content>
