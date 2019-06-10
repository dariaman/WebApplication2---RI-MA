<%@ Page Title="Upload & Download | Upload | Endorsment" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UploadEndorsment.aspx.vb" Inherits="WebApplication.UploadEndorsment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
 
        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;" DefaultButton="btnSearch1">
            <div class="row">
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
                
                <div class="col-sm-2">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon"><i class='fa fa-plus-square fa-fw'></i></span>
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Add New"
                                data-toggle="tooltip" data-placement="top" title="Click for add new " />
                        </div>
                    </div>
                </div>
            </div>

            
            <div id="DivBody">
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
                                    <asp:GridView ID="gridMenu" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                        DataKeyNames="NoReg" EmptyDataRowStyle-CssClass="empty_data" 
                                        EmptyDataText="No data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No Reg" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NoReg")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "NoReg")%></asp:LinkButton>
                                                    <asp:HiddenField ID="GFMEMBID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "FileName")%> ' />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DateReg" HeaderText="Date Reg"  DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="AddItem" HeaderText="Add Item" />
                                            <asp:BoundField DataField="ChangePlan" HeaderText="Change Plan"/>
                                            <asp:BoundField DataField="TerminatePlan" HeaderText="Terminate Plan" />
                                            <asp:BoundField DataField="AlterationPlan" HeaderText="Alteration Plan" />
                                            <%--<asp:BoundField DataField="FileName" HeaderText="File Ext" />--%>
                                            <asp:TemplateField HeaderText="FIle" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hffilename" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "FILENAME")%> ' />
                                                        <asp:LinkButton ID="Imgfilename" runat="server" CausesValidation="False"
                                                        data-toggle="tooltip" data-placement="top" title='<%# IIf(DataBinder.Eval(Container.DataItem, "FILENAME") <> "", "View File", "No File")%>'
                                                        Enabled='<%# IIf(DataBinder.Eval(Container.DataItem, "FILENAME") <> "", True, False)%>'
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NOReg")%>' CommandName="DownloadLink">
                                           <%# IIf(DataBinder.Eval(Container.DataItem, "FILENAME") <> "", "<i class='fa fa-cloud-download fa-fw'></i>", "<i class='fa fa-exclamation fa-fw'></i>")%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>

                                                    <asp:HiddenField ID="hfstatus" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ISPROCCES")%> '  />
                                                    <asp:LinkButton ID="ImgViewActive" runat="server" CausesValidation="False"
                                                        data-toggle="tooltip" data-placement="top" title='<%# IIf(DataBinder.Eval(Container.DataItem, "ISPROCCES") = "0", "Send", "Process")%>'
                                                        Enabled='<%# IIf(DataBinder.Eval(Container.DataItem, "ISPROCCES") = 0, "True", "False")%>'
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NOReg")%>' CommandName="UpdateLink">
                                            <%# IIf(DataBinder.Eval(Container.DataItem, "ISPROCCES") = "0", "<i class='fa fa-envelope fa-fw'></i>", "<i class='fa fa-inbox fa-fw'></i>")%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
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
        <div class="row">            
            <div class="col-sm-12">
                <div class="panel-body">
                    <div class="form-group input-group">
                    <center>
                        <table style="width:100%" >
                            <tr><td style="height: 24px"><asp:LinkButton ID="LinkClose" runat="server" Text="" data-toggle="tooltip" data-placement="top" title="Submit Data"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-check" ></i> Close</p></asp:LinkButton>
                            </td></tr>
                            </table>
                            </center>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon">Date Registration </span>
                        <asp:TextBox ID="txtDateRegistration" runat="server" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask
                            data-toggle="tooltip" data-placement="top" title="fill Registration date" class="form-control" placeholder="Date Registration.." name="Date Registration.." type="Date Registration.."></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon">No Register</span>
                        <asp:TextBox ID="txtNoRegistration" runat="server" onkeypress="return isKey(event)" 
                             data-toggle="tooltip" data-placement="top" title="No Registration"  MaxLength="15" class="form-control" placeholder="No Register.." name="No Register.." type="No Register.."></asp:TextBox>

                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon">Add Item QTY</span>
                        <asp:TextBox ID="txtAddItemQty" runat="server" onkeypress="return isKey(event)" 
                            data-toggle="tooltip" data-placement="top" title="Add Item Quantity" MaxLength="15" class="form-control" placeholder="Add Item Quantity.." name="Add Item Quantity.." type="Add Item Quantity.."></asp:TextBox>


                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon">Change Plan QTY</span>
                        <asp:TextBox ID="txtChangePlan" runat="server" onkeypress="return isKey(event)" 
                            data-toggle="tooltip" data-placement="top" title="Change Plan Quantity" MaxLength="15" class="form-control" placeholder="Change Plan Quantity.." name="Change Plan Quantity.." type="Change Plan Quantity.."></asp:TextBox>

                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon">Termination QTY</span>
                        <asp:TextBox ID="txtTermination" runat="server" onkeypress="return isKey(event)" 
                            data-toggle="tooltip" data-placement="top" title="Termination Quantity" MaxLength="15" class="form-control" placeholder="Termination Quantity.." name="Termination Quantity.." type="Termination Quantity.."></asp:TextBox>

                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <span class="input-group-addon">Alteration QTY</span>
                        <asp:TextBox ID="txtAlteration" runat="server" onkeypress="return isKey(event)" 
                            data-toggle="tooltip" data-placement="top" title="Alteration Quantity" MaxLength="15" class="form-control" placeholder="Alteration Quantity.." name="Alteration Quantity.." type="Alteration Quantity.."></asp:TextBox>

                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <center>
                            <div class="row-fluid">
                                <div class="fileupload fileupload-new" data-provides="fileupload" data-toggle="tooltip" data-placement="top" title="Select file to upload  max 1 Mb">
                                    <span class="btn btn-primary btn-file">
                                        <span class="fileupload-new"><i class='fa fa-image fa-fw'></i> Select File </span>
                                        <span class="fileupload-exists"><i class='fa  fa-file-image-o  fa-fw'></i> Change File </span><asp:FileUpload ID="FileUpload1"  runat="server"  />
                                        <span class="fileupload-preview"></span>
                                        <a href="#" class="close fileupload-exists" data-dismiss="fileupload" style="float: none;color:white;"><i class='fa fa-trash fa-fw'></i></></a>
                                        
                                    </span>
                                    
                                </div>
                                <asp:LinkButton ID="LinkUpload" runat="server" class="tn btn-primary btn-file" data-toggle="tooltip" data-placement="top" title="Upload Data">
                                    <span class="btn btn-primary btn-file">
                                        <i class='fa fa-upload fa-fw'></i>Submit & Up Load File<i class='fa fa-upload fa-fw'></i>
                                        </span></asp:LinkButton>
                                </div>
                        </center>
                    </div>
                </div>
            </div>
        </div>
        
    </asp:Panel>
        
</asp:Content>
