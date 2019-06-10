<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Home.aspx.vb" Inherits="WebApplication.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
   
    <%--<asp:Button ID="Button1" runat="server" Text="Button" />--%>
    <%--<div id="DivBody">
        <div id="card">

        </div>
    </div>--%>
<asp:Panel ID="PnlIP" runat="server">
    <div class="row">


    <div class="col-card-4">
      <div class="card-counternonipdepan" >
        <%--<i class="fa fa-users"></i>
        <span class="count-numbers"><asp:Label ID="LblUsername" runat="server" Text=""></asp:Label></span>
        <span class="count-name"><asp:Label ID="LblPolicyNo" runat="server" Text=""></asp:Label></span>
        <span class="count-address"><asp:Label ID="LblMemberid" runat="server" Text=""></asp:Label></span>--%>

        <table style="font-size: 11px;margin-top: 40px;margin-left: 5px;"><tr>
           <td style="padding-bottom: 2.5px;font-weight:bold;"> Nama Peserta</td><td style="padding-bottom: 2.5px;font-weight:bold;">&nbsp;:&nbsp;</td> 
           <td style="padding-bottom: 2.5px;"> <asp:Label ID="LblNamaPeserta" runat="server" Text=""></asp:Label></td>
        </tr><tr>
            <td style="padding-bottom: 2.5px;font-weight:bold;"> No. ID Peserta</td><td style="padding-bottom: 2.5px;font-weight:bold;">&nbsp;:&nbsp;</td> 
            <td style="padding-bottom: 2.5px;"> <asp:Label ID="LblIdPeserta" runat="server" Text=""></asp:Label></td> 
        </tr><tr>
           <td style="padding-bottom: 2.5px;font-weight:bold;"> Nama Karyawan </td><td style="padding-bottom: 2.5px;font-weight:bold;">&nbsp;:&nbsp;</td> 
           <td style="padding-bottom: 2.5px;"> <asp:Label ID="LblNamaKaryawan" runat="server" Text=""></asp:Label></td>
        </tr><tr>
        <td style="padding-bottom: 2.5px;font-weight:bold;"> Tgl Lahir </td><td style="padding-bottom: 2.5px;font-weight:bold;">&nbsp;:&nbsp;</td> 
        <td style="padding-bottom: 2.5px;"> <asp:Label ID="LblTglLahir" runat="server" Text=""></asp:Label></td>
        </tr><tr>
        <td style="padding-bottom: 2.5px;font-weight:bold;"> Jenis Kelamin </td><td style="padding-bottom: 2.5px;font-weight:bold;">&nbsp;:&nbsp;</td> 
        <td style="padding-bottom: 2.5px;"> <asp:Label ID="LblJnsKelamin" runat="server" Text=""></asp:Label></td>
        </tr><tr>
        <td style="padding-bottom: 2.5px;font-weight:bold;"> Perusahaan </td><td style="padding-bottom: 2.5px;font-weight:bold;">&nbsp;:&nbsp;</td> 
        <td style="padding-bottom: 2.5px;"> <asp:Label ID="LblPerusahaan" runat="server" Text=""></asp:Label></td>
        </tr><tr>
        <td style="padding-bottom: 2.5px;font-weight:bold;"> Nomor Polis </td><td style="padding-bottom: 2.5px;font-weight:bold;">&nbsp;:&nbsp;</td> 
        <td style="padding-bottom: 2.5px;"> <asp:Label ID="LblNoPol" runat="server" Text=""></asp:Label></td>
        </tr><tr>
        <td style="padding-bottom: 2.5px;font-weight:bold;"> Berlaku sampai  </td><td style="padding-bottom: 2.5px;font-weight:bold;">&nbsp;:&nbsp;</td> 
        <td style="padding-bottom: 2.5px;"> <asp:Label ID="LblBelakusampai" runat="server" Text=""></asp:Label></td>
        </tr>
        </table>
      </div>
    </div>

    <div class="col-card-4">
      <div class="card-counternonipbelakang" >

      </div>
    </div>
  </div>
</asp:Panel>


<asp:Panel ID="PnlNonIP" runat="server">
<div class="row">

<div class="col-card-4">
<div class="card-counteripdepan">
<table style="font-size: 11px; margin-top: 40px; margin-left: 5px;">
<tr>
<td style="padding-bottom: 2.5px; width: 18%"></td>
<td style="padding-bottom: 2.5px; width: 1%"></td>
<td></td>
<td style="padding-bottom: 2.5px; font-weight: bold; width: 40%; text-align: right;">Nomor Peserta BPJS</td>
</tr>
<tr>
<td style="padding-bottom: 2.5px; width: 18%"></td>
<td style="padding-bottom: 2.5px; width: 1%"></td>
<td></td>
<td style="padding-bottom: 2.5px; text-align: right;"><asp:Label ID="LblNoPesertaBPJS" runat="server"></asp:Label></td>
</tr>

<tr>
<td style="padding-bottom: 2.5px; font-weight: bold;">Nama Peserta</td>
<td style="padding-bottom: 2.5px; font-weight: bold;">&nbsp;:&nbsp;</td>
<td style="padding-bottom: 2.5px; "><asp:Label ID="LblNamaPeserta1" runat="server"></asp:Label></td>
<td style="padding-bottom: 2.5px; font-weight: bold; text-align: right;">Faskes Tinggkat I</td>

</tr>
<tr>
<td style="padding-bottom: 2.5px; font-weight: bold;">No. ID Peserta</td>
<td style="padding-bottom: 2.5px; font-weight: bold;">&nbsp;:&nbsp;</td>
<td style="padding-bottom: 2.5px;">
<asp:Label ID="LblIdPeserta1" runat="server"></asp:Label></td>
<td style="padding-bottom: 2.5px; text-align: right;"><asp:Label ID="LblFaskes" runat="server"></asp:Label></td>

</tr>
<tr>
<td style="padding-bottom: 2.5px; font-weight: bold;">Taggal Lahir&nbsp; </td>
<td style="padding-bottom: 2.5px; font-weight: bold;">&nbsp;:&nbsp;</td>
<td style="padding-bottom: 2.5px;">
<asp:Label ID="LblTglLahir1" runat="server"></asp:Label></td>
<td style="padding-bottom: 2.5px; font-weight: bold; text-align: right;">Kelas Rawat</td>
</tr>
<tr>
<td style="padding-bottom: 2.5px; font-weight: bold;">Perusahaan </td>
<td style="padding-bottom: 2.5px; font-weight: bold;">&nbsp;:&nbsp;</td>
<td style="padding-bottom: 2.5px;">
<asp:Label ID="LblPerusahaan1" runat="server"></asp:Label></td>
<td style="padding-bottom: 2.5px; height: 17px; text-align: right;">
<asp:Label ID="LblKelasRawat" runat="server"></asp:Label></td>
</tr>
<tr>
<td style="padding-bottom: 2.5px; font-weight: bold;">Nomor Polis</td>
<td style="padding-bottom: 2.5px; font-weight: bold;">&nbsp;:&nbsp;</td>
<td style="padding-bottom: 2.5px;">
<asp:Label ID="LblNoPol1" runat="server"></asp:Label></td>


</tr>
<tr>
<td style="padding-bottom: 2.5px; font-weight: bold; height: 17px;"></td>
<td style="padding-bottom: 2.5px; font-weight: bold; height: 17px;">&nbsp;&nbsp;</td>
<td style="padding-bottom: 2.5px; height: 17px;"></td>

</tr>
<tr>
<td style="padding-bottom: 2.5px; font-weight: bold;">&nbsp;</td>
<td style="padding-bottom: 2.5px; font-weight: bold;">&nbsp;&nbsp;</td>
<td style="padding-bottom: 2.5px;">&nbsp;</td>
</tr>
<tr>
<td style="padding-bottom: 2.5px; font-weight: bold;">&nbsp;</td>
<td style="padding-bottom: 2.5px; font-weight: bold;">&nbsp;&nbsp;</td>
<td style="padding-bottom: 2.5px;">&nbsp;</td>
</tr>
</table>
</div>
</div>

<div class="col-card-4">
<div class="card-counteripbelakang">
</div>
</div>
</div>
</asp:Panel>
</asp:Content>
