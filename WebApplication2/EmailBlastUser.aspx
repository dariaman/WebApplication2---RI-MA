<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EmailBlastUser.aspx.vb" Inherits="WebApplication.EmailBlastUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
   
    <div id="DivBody">

    <div class="row">
<div class="col-sm-2">
    <div class="row-fluid">
            <div class="fileupload fileupload-new" data-provides="fileupload" data-toggle="tooltip" data-placement="top" title="Select file to upload  max 1 Mb">
                &nbsp;<span class="btn btn-primary btn-file"><span class="fileupload-new"><i class='fa fa-image fa-fw'></i>Select File </span>
                    <span class="fileupload-exists"><i class='fa  fa-file-image-o  fa-fw'></i> Change File </span><asp:FileUpload ID="FileUpload1"  runat="server"  />
                    <span class="fileupload-preview"></span>
                    <a href="#" class="close fileupload-exists" data-dismiss="fileupload" style="float: none;color:white;"><i class='fa fa-trash fa-fw'></i></></a>
                                        
                </span>
                                    
            </div>
    </div>
    <div class="row">
    <div class="col-sm-6">
            <asp:LinkButton ID="LinkUpload" runat="server" class="tn btn-primary btn-file" data-toggle="tooltip" data-placement="top" title="Upload Data">
                <span class="btn btn-primary btn-file">
                    <i class='fa fa-upload fa-fw'></i>Submit & Up Load File<i class='fa fa-upload fa-fw'></i>
                    </span></asp:LinkButton>
    </div>
    </div>
    </div>
    </div>
        <div class="panel-body">
            <div class="form-group input-group">
                <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                <asp:TextBox ID="TxtKeyWord" runat="server" class="form-control" placeholder="Search..." name="Search..." type="Search..." data-toggle="tooltip" data-placement="top" title="Input Key"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:LinkButton ID="btnSearch1" CssClass="btn btn-primary btn-block btn-flat" runat="server" data-toggle="tooltip" data-placement="top" title="Search Data"><i class="fa fa-search"></i></asp:LinkButton></span>
            </div>
        </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="box box-info">
                <div class="box-header">
                    <h3 class="box-title">Upload Endorsment <small>List</small></h3>

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
                            <asp:GridView ID="gridMenu" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                DataKeyNames="Membid" EmptyDataRowStyle-CssClass="empty_data" 
                                EmptyDataText="No data Found">
                                <Columns>
                                    <asp:BoundField DataField="No" HeaderText="No"   />
                                    <asp:BoundField DataField="policyno" HeaderText="policyno"   />
                                   <%-- <asp:TemplateField HeaderText="Membid" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Membid")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "Membid")%></asp:LinkButton>
                                            <asp:HiddenField ID="GFMembid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Membid")%> ' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="Membid" HeaderText="Membid"   />
                                    <asp:BoundField DataField="FULLNAME" HeaderText="FULLNAME"   />
                                    <asp:BoundField DataField="EMAIL" HeaderText="EMAIL"   />
                                    
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-12">
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </div>

    </div>
    <div class="row-fluid">
            <div class="fileupload fileupload-new" data-provides="fileupload" data-toggle="tooltip" data-placement="top" title="Select file to upload  max 1 Mb">
                <span class="btn btn-primary btn-file">
                    <span class="fileupload-new"><i class='fa fa-image fa-fw'></i> Select File </span>
                    <span class="fileupload-exists"><i class='fa  fa-file-image-o  fa-fw'></i> Change File </span><asp:FileUpload ID="FileUpload2"  runat="server"  />
                    <span class="fileupload-preview"></span>
                    <a href="#" class="close fileupload-exists" data-dismiss="fileupload" style="float: none;color:white;"><i class='fa fa-trash fa-fw'></i></></a>
                                        
                </span>
                                    
            </div>
            <asp:LinkButton ID="LinkButton1" runat="server" class="tn btn-primary btn-file" data-toggle="tooltip" data-placement="top" title="Upload Data">
                <span class="btn btn-primary btn-file">
                    <i class='fa fa-upload fa-fw'></i>Submit & Up Load File<i class='fa fa-upload fa-fw'></i>
                    </span></asp:LinkButton>
    </div>
    <div class="col-sm-2">
        <div class="panel-body">
            <div class="form-group input-group">
                <span class="input-group-addon"><i class='fa fa-plus-square fa-fw'></i></span>
                <asp:Button ID="btnSend" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Send"
                    data-toggle="tooltip" data-placement="top" title="Click for add new " />
            </div>
        </div>
    </div>
</div>
</asp:Content>
