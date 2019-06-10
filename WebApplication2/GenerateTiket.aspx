<%@ Page Title="Upload & Download | Upload | Upload Claim" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="GenerateTiket.aspx.vb" Inherits="WebApplication.GenerateTiket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">

    <asp:Panel ID="PnlMain" runat="server" Style="background: transparent;" DefaultButton="">


        <%--       <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-body">
                        <h3>
                            <%--<asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            <img alt="1234" src="file://192.168.30.2/relihit/uploadDataClaim/18101801000001-01.JPG" width='100px' height='100px'/>
                        </h3>
                    </div>
                </div>
            </div>

        </div>--%>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-heading">
                        Ticket No
                    </div>
                    <div class="panel-body">
                        <asp:TextBox ID="txtNoTiket" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="No Tiket" MaxLength="100" class="form-control" placeholder="No Tiket.." name="No Tiket.." type="No Tiket.." ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-heading">
                        Policy No 
                    </div>
                    <div class="panel-body">
                        <asp:TextBox ID="txtNoPolis" runat="server" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="No Polis" MaxLength="100" class="form-control" placeholder="No Polis.." name="No Polis.." type="No Polis.." ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-heading">
                        Member
                    </div>
                    <div class="panel-body">
                        <asp:DropDownList ID="DDLMemberId" runat="server" class="form-control" placeholder="Policy No...." name="Policy No...." type="Policy No...."></asp:DropDownList>
                        <%--<asp:TextBox ID="txtMembId" runat="server" onkeypress="return isKey(event)" MaxLength="10" data-toggle="tooltip" data-placement="top" title="Member Id" class="form-control" placeholder="Member ID.." name="Member ID.." type="Member ID.." ReadOnly="True"></asp:TextBox>--%>
                    </div>
                </div>
            </div>

        </div>

        <div class="row">
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-heading">
                        Admission Date (Periode
                        <asp:Label ID="lblEffdt" runat="server" Text=""></asp:Label>-<asp:Label ID="lblExpdt" runat="server"></asp:Label>
                        )
                    </div>
                    <div class="panel-body">
                        <asp:TextBox ID="txtTglBerobat" runat="server" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask
                            data-toggle="tooltip" data-placement="top" title="fill tanggal berobat" class="form-control" placeholder="Date.." name="Date.." type="Date.."></asp:TextBox>


                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-heading">
                        Total Claim <asp:Label ID="lblTtlClaim" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <asp:TextBox ID="txtTotalClaim" runat="server" data-toggle="tooltip" data-placement="top" title="Input Total Claim" onkeypress="return isKeyNumber(event)"  onfocus="removeCommas(this)" onblur="addCommas2(this)"   MaxLength="10"  CssClass="form-control" placeholder="Total Claim.." name="Total Claim.." type="Total Claim.." >0</asp:TextBox>
                    </div>
                </div>
            </div>

        </div>

        <div class="row">
            <div class="col-sm-4">
                <div class="panel-body">
                    <div class="form-group input-group">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Generate Ticket"
                            data-toggle="tooltip" data-placement="top" title="Click Generate Tiket " />
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <div class="panel-body">
                    <div class="form-group input-group">
                    </div>
                </div>
            </div>
        </div>

        <div id="DivBody">
            <div class="row">
                <div class="col-sm-12">

                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Ticket <small>List</small></h3>

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
                                        DataKeyNames="NoTiket" EmptyDataRowStyle-CssClass="empty_data"
                                        EmptyDataText="No data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No Ticket" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False"
                                                        Enabled='<%# IIf(DataBinder.Eval(Container.DataItem, "StatusData") = "Canceled", False, IIf(DataBinder.Eval(Container.DataItem, "StatusData") = "Closed", False, True))%>'
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NoTiket")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "NoTiket")%></asp:LinkButton>
                                                    <asp:HiddenField ID="GFMEMBID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "StatusData")%> ' />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TglBerobat" HeaderText="Addmission Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="NoPolis" HeaderText="Policy No" />
                                            <asp:BoundField DataField="TotalClaim" HeaderText="Total Claim" DataFormatString="{0:N0}" />
                                            <asp:BoundField DataField="StatusData" HeaderText="Status Claim" />
                                            <%--<asp:BoundField DataField="IsActive" HeaderText="Active" />--%>
                                            <asp:TemplateField HeaderText="Cancel" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfstatus1" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IsActive")%> ' />
                                                    <asp:LinkButton ID="ImgViewActive1" runat="server" CausesValidation="False"
                                                        Enabled='<%# IIf(DataBinder.Eval(Container.DataItem, "StatusData") = "Canceled", False, IIf(DataBinder.Eval(Container.DataItem, "StatusData") = "Closed", False, True))%>'
                                                        data-toggle="tooltip" data-placement="top" title='<%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "Cancel", IIf(DataBinder.Eval(Container.DataItem, "IsClosed") = "True", "Closed", "Canceled"))%>'
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NoTiket")%>' CommandName="UpdateLink1"><i class='fa fa-times fa-fw'></i>
                                           <%-- <%# IIf(DataBinder.Eval(Container.DataItem, "IsActive") = "True", "<i class='fa fa-check fa-fw'></i>", "<i class='fa fa-times fa-fw'></i>")
                                                %>--%>
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



    <asp:Panel ID="pnlPopup" runat="server" Visible="false" DefaultButton="BtnSubmit">
        <div class="row">

            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-heading">
                        No Ticket
                    </div>
                    <div class="panel-body">
                        <asp:TextBox ID="txtNotiketPict" runat="server" data-toggle="tooltip" data-placement="top" title="No Tiket" MaxLength="20" CssClass="form-control" placeholder="No Tiket.." name="No Tiket.." type="No Tiket.."></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-12">

                <%--<div class="row-fluid">
                                <div class="fileupload fileupload-new" data-provides="fileupload" data-toggle="tooltip" data-placement="top" title="Select file to upload  max 1 Mb">
                                   <span class="btn btn-primary btn-file">
                                        <span class="fileupload-new"><i class='fa fa-image fa-fw'></i> Select File </span>
                                        <span class="fileupload-exists"><i class='fa  fa-file-image-o  fa-fw'></i> Change File </span>--%>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel">
                            <div class="panel-heading">
                                <h4><p style="color: red;font-weight:bold;" > Maximal  8 Mb - (8.000 Kb) </p></h4>
                                 </div>
                            <div class="panel-body">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="FileUploadClaim" runat="server" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkUpload" runat="server" class="tn btn-primary btn-file" data-toggle="tooltip" data-placement="top" title="Upload Data">
                                    <span class="btn btn-primary btn-file">
                                        <i class='fa fa-upload fa-fw'></i>Confirm<i class='fa fa-upload fa-fw'></i>
                                        </span></asp:LinkButton>
                                        </td>
                                    </tr>

                                </table>

                            </div>
                        </div>
                    </div>
                </div>


                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">Claim Photo <small>List</small>
                            <asp:Label ID="LblTtl" runat="server" Text=""></asp:Label></h3>

                        <div class="pull-right box-tools">
                            <button class="btn btn-info btn-sm" data-toggle="tooltip" data-widget="collapse" title="Collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                        <div class="col-lg-12">
                            <div class="panel-body">
                                <div class="box-body pad">
                                    <div class="panel-body">
                                        <div class="dataTable_wrapper">
                                            <br />
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                                DataKeyNames="Pict" EmptyDataRowStyle-CssClass="empty_data"
                                                EmptyDataText="No data Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Pict" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="ImgViewUserId" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Pict")%>' CommandName="SelectLink"><i class='fa fa-edit fa-fw'></i> <%# DataBinder.Eval(Container.DataItem, "Pict")%></asp:LinkButton>
                                                            <asp:HiddenField ID="hfext" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ext")%> ' />
                                                        </ItemTemplate>
                                                        <ItemStyle ForeColor="Blue" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Size" HeaderText="Size (Byte)" DataFormatString="{0:N0}" />
                                                    <asp:BoundField DataField="OriFile" HeaderText="Original File Name" />
                                                    <asp:BoundField DataField="Ext" HeaderText="File Type" />
                                                    <asp:BoundField DataField="Note" HeaderText="Note" ItemStyle-Width="200" >
                                                    <ItemStyle Width="30%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-ForeColor="#0000FF" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hfstatus1" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Pict")%> ' />
                                                            <asp:LinkButton ID="ImgViewActive1" runat="server" CausesValidation="False"
                                                                data-toggle="tooltip" data-placement="top" title='Delete Pict'
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NoTiket")%>' CommandName="DeletePict"><i class='fa fa-times fa-fw'></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle ForeColor="Blue" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="empty_data" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div class="row">

            <div class="col-sm-6">
                <div class="panel">
                    <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Cancel"
                        data-toggle="tooltip" data-placement="top" title="Cancel" />

                </div>
            </div>
            <div class="col-sm-6">
                <div class="panel">
                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Submit"
                        data-toggle="tooltip" data-placement="top" title="Click Generate Ticket " />
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkMpe" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="LinkMpeModalPopupExtender" PopupControlID="Panel1" runat="server"
        TargetControlID="LinkMpe" PopupDragHandleControlID="mGridPict" BackgroundCssClass="Overlay">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="display:none;" >
        <div class="modalPopup-dialog" role="document">
            <div class="modalPopup-content">
                <div class="modalPopup-header" id="mGridPict">
                    <table class="table table-striped table-bordered table-hover">
                        <tr>
                            <td style="vertical-align: central; text-align: start;">
                                <center><h3>Term & Condition</h3></center>
                            </td>
                            <%--<td style="vertical-align: central; text-align: end;">
                                
                            </td>--%>
                        </tr>
                    </table>

                </div>
                <div class="modalPopup-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <%--<div class="panel-heading">
                                        
                                    </div>--%>
                                <div class="table-responsive" style="overflow-y: scroll; height: 400px; width: 100%;">
                                    <table class="table-bordered table-striped" style="height: 100%; width: 100%;">
                                        <tr>
                                            <td>Nasabah setuju bahwa aplikasi ini merupakan alat alternatif penyampaian klaim asuransi yang memerlukan proses verifikasi sesuai ketentuan Reliance. 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nasabah setuju bahwa data/informasi yang diberikan melalui aplikasi ini adalah benar dan bertanggung jawab atas isi data/informasi. 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Reliance berhak untuk melakukan persetujuan atau penolakan terhadap pengajuan klaim yang terjadi sesuai ketentuan polis.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Dokumen asli wajib disimpan selama 6 bulan, dan wajib di kirimkan ke Reliance bila diperlukan. 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Bila peserta tidak dapat menunjukan dokumen asli pada saat diperlukan maka peserta wajib mengembalikan dana yang telah dibayarkan Reliance. 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Dokumen <b>ASLI</b> yang perlu diupload antara lain : kwitansi dokter beserta diagnosa, No SIP, No Telpon dan alamat  pengobatan ; kwitansi obat-obatan dan pemeriksaan penunjang; copy resep, surat rujukan dan  hasil pemeriksaan penunjang (bila ada).
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="modalPopup-footer">
                                        <table class="table-bordered">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Label ID="lblNotification" runat="server" Text="" Visible="false"></asp:Label> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Agree"><i class="fa fa-check" ></i>Agree</asp:LinkButton>&nbsp;
                                                    
                                                </td>
                                                <td colspan="3">
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Close"><i class="fa fa-times" ></i>Cancel</asp:LinkButton>&nbsp;
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkMpe1" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="LinkMpeModalPopupExtender1" PopupControlID="Panel2" runat="server"
        TargetControlID="LinkMpe1" PopupDragHandleControlID="Panel2" BackgroundCssClass="Overlay">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" style="display:none;"  >
        <div class="modalPopup-dialog" >
            <div class="box box-info">
                <div class="box-header">
                    <h5 class="box-title">
                        <asp:Label ID="LblJudul" runat="server" Text=""></asp:Label></h5>
                    <div class="pull-right box-tools">
                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Close Page"><i class="fa fa-times"></i></asp:LinkButton>
                    </div>
                    <br />
                    <br />
                    <div class="modalPopup-body" style="overflow-y: scroll;height:500px;" >
                            <table >
                                <tr >
                                    <td>
                                        <asp:Image ID="ImgPict" runat="server" CssClass="img-thumbnail"  />
                                    </td>
                                </tr>
                                
                            </table>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


    <asp:LinkButton ID="LinkMpe2" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="ModalPopupExtender2" PopupControlID="Panel3" runat="server"
        TargetControlID="LinkMpe1" PopupDragHandleControlID="Panel3" BackgroundCssClass="Overlay">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" style="display:none;">
        <div class="modalPopup-dialog" role="document">
            <div class="box box-info">
                <div class="box-header">
                    <h5 class="box-title">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label></h5>
                    <div class="pull-right box-tools">
                        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Close Page"><i class="fa fa-times"></i></asp:LinkButton>
                    </div>
                    <br />
                    <br />
                    <div class="modalPopup-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="panel">
                                    <div class="panel-heading">
                                        Note Picture 
                                    </div>
                                    <div class="panel-body">
                                        <asp:TextBox ID="txtNote" runat="server"  onKeyUp="javascript:Check(this, 500);"
                                onChange="javascript:Check(this, 500);" onkeypress="return isKey(event)" data-toggle="tooltip" data-placement="top" title="Note" MaxLength="500" class="form-control" placeholder="Note.." name="Note.." type="Note.." TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <asp:Button ID="BtnConfirmPict" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Submit"
                        data-toggle="tooltip" data-placement="top" title="Submit" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
