<%@  Page Title="File | Setting | Change Password" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="changePassword.aspx.vb" Inherits="WebApplication.changePassword" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    <div id="DivBody">
        <div class="row">
            <div class="col-sm-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Change Password
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table class="table table-bordered table-striped">
                                <tbody>
                                    <tr>
                                        <td class="table5">User name</td>
                                        <td class="auto-table31">
                                            <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table5">Old Password</td>
                                        <td class="auto-table31">
                                            <asp:TextBox ID="txtOldPassword" runat="server" onkeypress="return isKey(event)" MaxLength="10" class="form-control" placeholder="Old Password" name="password" type="password"  data-toggle="tooltip" data-placement="top" title="Old Password" ></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table5">New Password</td>
                                        <td class="auto-table31">
                                            <asp:TextBox ID="txtNewPassword" runat="server" onselectstart="return false" onpaste="return false;" onCopy="return false" onCut="return false" onDrag="return false" onDrop="return false" autocomplete=off onkeypress="return isKey(event)" MaxLength="10" class="form-control" placeholder="New Password" name="password" type="password" data-toggle="tooltip" data-placement="top" title="New Password" ></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table5">Confirm Password</td>
                                        <td class="auto-table31">
                                            <asp:TextBox ID="txtConfirm" runat="server" onselectstart="return false" onpaste="return false;" onCopy="return false" onCut="return false" onDrag="return false" onDrop="return false" autocomplete=off onkeypress="return isKey(event)" MaxLength="10" class="form-control" placeholder="Confirm Password" name="password" type="password"  data-toggle="tooltip" data-placement="top" title="Confirm Password" ></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table5">&nbsp;</td>
                                        <td class="auto-table31">
                                            <asp:LinkButton ID="LinkUserprofile" runat="server" Text=""> <p style="line-height: 0%;text-align: Left;"><i class="fa fa-user" ></i>User profile</p></asp:LinkButton>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-table31" colspan="2">

                                            <div class="col-sm-2">
                                                <div class="panel-body">
                                                    <div class="form-group input-group">
                                                        <span class="input-group-addon"><i class='fa fa-check-square fa-fw'></i></span>
                                                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary btn-block btn-flat" runat="server" Text="Submit" />
                                                    </div>
                                                </div>
                                            </div>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
