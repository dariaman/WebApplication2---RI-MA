<%@ Page Title="Report | Report | Viewer" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="WebViewer.aspx.vb" Inherits="WebApplication.WebViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--<html xmlns="http://www.w3.org/1999/xhtml">--%>
<%--<head id="Head1" runat="server">
    <title>Cetak</title>

    <link href="style.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="images/icon.png" rel="SHORTCUT ICON" />
    <link href="CSS/MessageBox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Jscript/JScript.js"></script>
</head>--%>
<%--<body style="width: 823px; height: 642px">
    <link href="style.css" rel="stylesheet" type="text/css" media="screen" />
</body>
    <form id="form1" runat="server">
    </form>
        <asp:Literal ID="ltrlMsg" runat="server"></asp:Literal>
</html>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="pnlfooter">
                <asp:Button ID="BtnExport01" runat="server" Text="Export Excel" CssClass="btn btn-primary btn-block btn-flat" />
                <asp:Button ID="BtnExport1" runat="server" Text="Export PDF" CssClass="btn btn-primary btn-block btn-flat"/>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <%--<asp:Panel ID="pnlFormChils" runat="server">--%>
            <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
            <%--<div>
                    <h1>
                        <asp:Label ID="LblHeader" runat="server" CssClass="table5" Font-Bold="True" Font-Size="20pt" Font-Underline="False" Text="PREVIEW"></asp:Label>
                    </h1>
                </div>--%><div class="box-body pad">
                    <div class="panel-body">
                        <div class="dataTable_wrapper1">
                            
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="800" Height="500" CssClass="table table-striped table-bordered table-hover form-control pull-center"></rsweb:ReportViewer>
                            <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/underconstruction.gif" />--%>
                        </div>
                    </div>
                </div>
        </div>
        <%--</asp:Panel>--%>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="pnlfooter">
                <asp:Button ID="BtnExport0" runat="server" Text="Export Excel" CssClass="btn btn-primary btn-block btn-flat" Visible="False" />
                &nbsp;<asp:Button ID="BtnExport" runat="server" Text="Export PDF" CssClass="btn btn-primary btn-block btn-flat" />
            </div>
        </div>
    </div>
</asp:Content>

