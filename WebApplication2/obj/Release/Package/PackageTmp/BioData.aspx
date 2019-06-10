<%@ Page Title="File | Setting | BioData List" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BioData.aspx.vb" Inherits="WebApplication.BioData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">

    

        <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;"  DefaultButton="btnSearch1">
            <div class="row">
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="form-group input-group">
                            <span class="input-group-addon">Search Type </span>
                            <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" placeholder="Search By" name="Search By" type="Search By" >
                                <asp:ListItem Value="Kode">BioData Code</asp:ListItem>
                                <asp:ListItem Value="Name">BioData Name</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
                <div class="col-sm-4">
                    <div class="panel-body">
                        <div class="input-group">
                            <span class="input-group-addon"><i class='fa fa-key fa-fw'></i></span>
                            <asp:TextBox ID="TxtKeyWord" runat="server" class="form-control" placeholder="Search..." name="Search..." type="Search..." data-toggle="tooltip" data-placement="top" title="Search Key"></asp:TextBox>

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
                                data-toggle="tooltip" data-placement="top" title="Click for add new BioData" />
                        </div>
                    </div>
                </div>

            </div>
            <div id="DivBody">
            <div class="row">
                <div class="col-sm-12">

                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">BioData <small>List</small></h3>

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
                                        DataKeyNames="BioDataCode" EmptyDataRowStyle-CssClass="empty_data" 
                                        EmptyDataText="No data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BioDataCode")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "BioDataCode")%></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BioDataname" HeaderText=" Name" />
                                            <asp:BoundField DataField="BioDataNickname" HeaderText=" NickName" />
                                            <asp:BoundField DataField="BioDataAdd" HeaderText=" Address" />
                                            <asp:BoundField DataField="BioDataCity" HeaderText=" City" />
                                            <asp:BoundField DataField="BioDataZIP" HeaderText="ZIP" />
                                            <asp:BoundField DataField="BioDataPhone" HeaderText="Phone" />
                                            <asp:BoundField DataField="BioDataFax" HeaderText="Fax" />
                                            <asp:BoundField DataField="BioDataContact" HeaderText="Contact" />
                                            <asp:TemplateField HeaderText="Status" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>

                                                    <asp:HiddenField ID="hfstatus" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IsActive")%> ' />
                                                    <asp:LinkButton ID="ImgViewActive" runat="server" CausesValidation="False"
                                                        data-toggle="tooltip" data-placement="top" title='<%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "Inactive", "Actived")%>'
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BioDataCode")%>' CommandName="UpdateLink"><i class='fa fa-signout fa-fw'></i>
                                            <%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "<i class='fa fa-check fa-fw'></i>", "<i class='fa fa-times fa-fw'></i>")%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField>
                                                <HeaderTemplate>
                                                    IsActive
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CBIsActive" runat="server" Checked='<%# Eval("IsActive")%>' Enabled="false" CssClass="flat-red" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="BioDataPict" HeaderText="pict" />
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
        
        
    <asp:Panel ID="pnlPopup" runat="server"  DefaultButton="btnSave" Visible="false">
        <div id="DivBody1">
            <table class="table table-striped table-bordered table-hover" id="mGridPict">
                <tr>
                    <td style="vertical-align: central; text-align: center;">
                        <asp:LinkButton ID="LinkClose" runat="server" Text="" data-toggle="tooltip" data-placement="top" title="Close Page"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-times" ></i> Exit</p></asp:LinkButton>
                    </td>
                    <td style="vertical-align: central; text-align: center;">
                        <asp:LinkButton ID="LinkSubmit" runat="server" Text="" data-toggle="tooltip" data-placement="top" title="Submit Data"> <p style="line-height: 0%;text-align: center;"><i class="fa fa-check" ></i> Submit</p></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <center><asp:Image ID="Image1" runat="server" Style="height: 150px; width: 150px" CssClass="img-thumbnail img-rounded" />
                                <br />
                            <br />
                                <div class="fileupload fileupload-new" data-provides="fileupload" data-toggle="tooltip" data-placement="top" title="Select a picture to upload" >
                                    <span class="btn btn-primary btn-file">
                                        <span class="fileupload-new"><i class='fa fa-image fa-fw'></i> Select </span>
                                        <span class="fileupload-exists"><i class='fa  fa-file-image-o  fa-fw'></i> Change </span><asp:FileUpload ID="FileUpload1" runat="server" />
                                        <span class="fileupload-preview"></span>
                                        <a href="#" class="close fileupload-exists" data-dismiss="fileupload" style="float: none;color:white;"><i class='fa fa-trash fa-fw'></i></a>
                                    </span>
                                </div>
                        </center>
                        </div>
                        <div class="panel-footer">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:LinkButton ID="LinkUpload" runat="server" class="btn btn-primary btn-block btn-group-vertical" data-toggle="tooltip" data-placement="top" title="Upload a Picture"><i class='fa fa-upload fa-fw'></i> Up Load Picture<i class='fa fa-upload fa-fw'></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            BioData Code <i class='fa fa-code fa-fw'></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtBioDataCode" runat="server" MaxLength="5" data-toggle="tooltip" data-placement="top" title="Input Biodata Code" CssClass="form-control" placeholder="BioData Code.." name="BioData Code.." type="BioData Code.." onkeypress="return isKey(event)" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            BioData Name <i class='fa fa-user fa-fw'></i>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlSalutation" runat="server" CssClass="form-control" placeholder="Salutation" name="Salutation" type="Salutation">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtBioDataname" runat="server" data-toggle="tooltip" data-placement="top" title="Input Biodata Name" MaxLength="100" CssClass="form-control" placeholder="BioData Name.." name="BioData Name.." type="BioData Name.." onkeypress="return isKey(event)"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Nick Name <i class='fa fa-user fa-fw'></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtBioDataNickname" runat="server" data-toggle="tooltip" data-placement="top" title="Input Nick Name" MaxLength="50" CssClass="form-control" placeholder="NickName.." name="NickName.." type="NickName.." onkeypress="return isKey(event)"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Address <i class='fa fa-road fa-fw'></i>
                            <asp:TextBox ID="txtBioDataAdd" runat="server" onKeyUp="javascript:Check(this, 400);"
                                onChange="javascript:Check(this, 400);" data-toggle="tooltip" data-placement="top" title="Input Address" MaxLength="500" Height="69px" TextMode="MultiLine" CssClass="form-control" placeholder="Address.." name="Address.." type="Address.." onkeypress="return isKey(event)"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            City <i class='fa fa-road fa-fw'></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtBioDataCity" runat="server" data-toggle="tooltip" data-placement="top" title="Input City" MaxLength="20" CssClass="form-control" placeholder="City.." name="City.." type="City.." onkeypress="return isKey(event)"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            ZIP <i class='fa fa-envelope fa-fw'></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtBioDataZIP" runat="server" data-toggle="tooltip" data-placement="top" title="Input zip code" MaxLength="10" CssClass="form-control" placeholder="Zip.." name="Zip.." type="Zip.." onkeypress="return isKey(event)"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Phone No <i class='fa fa-phone fa-fw'></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtBioDataPhone" runat="server" data-toggle="tooltip" data-placement="top" title="Input Phone Number" MaxLength="20" CssClass="form-control" placeholder="Phone.." name="Phone.." type="Phone.." data-inputmask='"mask": "(9999) 999-99-999"' data-mask></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            Fax No <i class='fa fa-fax fa-fw'></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtBioDataFax" runat="server" data-toggle="tooltip" data-placement="top" title="Input Fax Number" MaxLength="20" CssClass="form-control" placeholder="Fax.." name="Fax.." type="Fax.." data-inputmask='"mask": "(9999) 999-99-999"' data-mask></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon">Email @ <i class='fa fa-envelope-o fa-fw'></i></span>
                                <asp:TextBox ID="txtBioDataEmail" runat="server" data-toggle="tooltip" data-placement="top" title="Input Email BioData" MaxLength="50" class="form-control" placeholder="Email" name="Email" type="Email" onkeypress="return isKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <asp:RadioButtonList ID="RbGender" runat="server" RepeatDirection="Horizontal" CssClass="flat-red" data-toggle="tooltip" data-placement="top" title="Select Gender">
                                <asp:ListItem Selected="True" Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon">Birth Date <i class='fa fa-calendar fa-fw'></i></span>

                                <asp:TextBox ID="txtBioDataBirthDate" runat="server" class="form-control" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask 
                                    name="Date.." placeholder="Date.." data-toggle="tooltip" data-placement="top" title="input day birth" type="Date.."></asp:TextBox>

                            </div>
                        </div>
                        <div class="panel-footer">
                            <label>
                                <asp:CheckBox ID="chkAktiv" CssClass="flat-red" runat="server" Checked="True" Text="IsActive" data-toggle="tooltip" data-placement="top" title="Click for Active / Non Active BioData" />

                                &nbsp;</label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group input-group">
                                <span class="input-group-addon">User Branch <i class="fa fa-building fa-fw"></i></span>
                                <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" placeholder="Branch Stay.." name="Branch Stay.." type="Branch Stay..">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            BioData Contact  <i class='fa fa-mobile-phone fa-fw'></i>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtBioDataContact" runat="server" data-toggle="tooltip" data-placement="top" title="Input BioData Contact" MaxLength="20" CssClass="form-control" placeholder="BioData Contact.." name="BioData Contact.." type="BioData Contact.." data-inputmask='"mask": "(9999) 999-99-999"' data-mask></asp:TextBox>
                        </div>
                        <div class="panel-footer">
                            <label>

                                <%--<asp:RadioButtonList ID="RbType" runat="server" RepeatDirection="Horizontal" CssClass="flat-red" data-toggle="tooltip" data-placement="top" title="Select Type">
                                    <asp:ListItem Value="C" Selected="True">Customer</asp:ListItem>
                                    <asp:ListItem Value="S">Supplier</asp:ListItem>
                                    <asp:ListItem Value="U" Enabled="False">User System</asp:ListItem>
                                </asp:RadioButtonList>--%>

                            </label>
                        </div>
                    </div>
                </div>
            </div>

            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Submit" Visible="False" />
        </div>
    </asp:Panel>
</asp:Content>
