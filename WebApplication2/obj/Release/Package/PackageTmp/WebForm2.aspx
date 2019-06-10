<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="WebForm2.aspx.vb" Inherits="WebApplication.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
<asp:Label ID="lblFolder" runat="server" Text=""></asp:Label>
<br />
<asp:Label ID="lblNext" runat="server" Text=""></asp:Label>
<br />
<asp:LinkButton ID="lnkUp1" runat="server">Up<i class='fa fa-level-up fa-fw'></i></asp:LinkButton>
<asp:GridView ID="DGuser" runat="server"  AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                    DataKeyNames="ItemName" EmptyDataRowStyle-CssClass="empty_data"
                                    EmptyDataText="No data Found">
        <Columns>
            <asp:TemplateField HeaderText="Item Name" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblIsFolder" runat="server" ><%# IIf(DataBinder.Eval(Container.DataItem, "IsFolder") = True, "<i class='fa fa-folder fa-fw'></i>", "<i class='fa fa-file fa-fw'></i>")%></asp:Label>
                    <asp:LinkButton ID="ImgViewItemName" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ItemName")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "ItemName")%></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="IsFolder" HeaderText="IsFolder" />
            <asp:TemplateField HeaderText="Download Item" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:LinkButton ID="downloadItem" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ItemName")%>' CommandName="Download"
                        visible='<%# IIf(DataBinder.Eval(Container.DataItem, "IsFolder") = True, "False", "True")%>'><i class='fa fa-download fa-fw'></i> </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
