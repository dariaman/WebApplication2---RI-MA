<%@ Page Title="Display" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="WebDisplayFrame.aspx.vb" Inherits="WebApplication.WebDisplayFrame" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    <div id="DivBody">
        <div class="row">
            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="panel-heading">
                        Preview
                    </div>
                    <div class="panel-body">
                        <%--<asp:Literal ID="LtrFrame" runat="server"></asp:Literal>--%>
                        <iframe id="ContentIframe" runat="server" class="table table-striped table-bordered table-hover"  width="100%" frameborder="0" scrolling="yes" height="1000px"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
