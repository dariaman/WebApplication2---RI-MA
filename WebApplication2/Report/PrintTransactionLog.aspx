 <%@  Page Language="vb" AutoEventWireup="false" CodeBehind="PrintTransactionLog.aspx.vb" Inherits="WebApplication.PrintTransactionLog" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Report | Transaction Log</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- Bootstrap 3.3.2 -->
    <link href="../bootstrap/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome Icons -->
    <!-- Font Awesome Icons -->
    <link href="../Bootstrap/bootstrap/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="../Bootstrap/bootstrap/css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="../bootstrap/dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Jscript/jquery-latest.min.js"></script>
    <script type="text/javascript" src="../Jscript/Jscript.js"></script>
    <script type="text/javascript">
        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://tempuri.org/"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
                , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            }
        })()
    </script>
</head>
<script lang="javascript" type="text/javascript">
    function printDiv(divID) {
        //Get the HTML of div
        var divElements = document.getElementById(divID).innerHTML;
        //Get the HTML of whole page
        var oldPage = document.body.innerHTML;

        //Reset the page's HTML with div's HTML only
        document.body.innerHTML =
          "<html><head><title></title></head><body>" +
          divElements + "</body>";

        //Print Page
        window.print();

        //Restore orignal HTML
        document.body.innerHTML = oldPage;
    }
</script>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div id="DivBody">
                <section class="invoice">
                    <div class="row">
                        <div class="col-sm-12">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <img alt="" src="../images/logogs.png " style="width: 90%; height: 90%" />

                                    </td>
                                    <td style="width: 90%; height: 10%;">Suite 16-6, Wisma UOA II
                                        <br />
                                        No 21, Jalan Pinang 50450 Kuala Lumpur<br /> 
                                    </td>
                                </tr>
                            </table>

                          <%--  <small class="pull-right">Date:
                                <asp:Label runat="server" ID="LblNote_Date" CssClass="text-warning"></asp:Label></small>--%>

                        </div>

                    </div>
                    <div class="row">
                        <div class="col-sm-12 invoice-col">
                            <b style="font-size: large">Transaction Log</b>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 table-responsive">
                                    <asp:GridView ID="gvwList" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" DataKeyNames="nc_Orikeyword" EmptyDataRowStyle-CssClass="empty_data" PagerStyle-CssClass="pagination" EmptyDataText="Tidak ada data." PageSize="25" AllowSorting="True">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:BoundField DataField="nc_DateIN" DataFormatString="{0:dd/MM/yyyy}" HeaderText="nc_DateIN" SortExpression="nc_DateIN">
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nc_timeIN" HeaderText="nc_timeIN" SortExpression="nc_timeIN">
                                            <ItemStyle HorizontalAlign="left" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nc_ChargeMSISDN" HeaderText="nc_ChargeMSISDN" SortExpression="nc_ChargeMSISDN">
                                            <ItemStyle HorizontalAlign="left" Width="40%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nc_Orikeyword" HeaderText="nc_Orikeyword" SortExpression="nc_Orikeyword" >
                                            <ItemStyle HorizontalAlign="left" Width="40%" />
                                            </asp:BoundField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="empty_data" />
                                        <PagerStyle CssClass="pgr" />
                                    </asp:GridView>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-6">
                                    <p class="text-muted well well-sm no-shadow" style="margin-top: 10px;">
                                        <asp:Label runat="server" ID="LblUserId"></asp:Label>
                                    </p>
                                </div>
                                <div class="col-sm-6">
                                    <small class="pull-right">Print Date :
                                <asp:Label runat="server" ID="LblDate" CssClass="text-warning"></asp:Label></small>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                        <div class="row">
                            <div class="col-sm-12" style="vertical-align: middle; text-align: center; font-size: 8px;">

                                <img alt="" src="../images/logogs_brand.png" style="width: 2%; height: 2%" />
                                <img alt="" src="../images/logoname_brand.png" style="width: 5%; height: 5%" />
                            </div>
                        </div>
                    <div class="row">
                        <div class="col-sm-12" style="vertical-align:middle;text-align:center;font-size:8px;">
                            www.getSoftware.com
                    </div>
                    </div>
                </section>
            </div>
            <div class="col-sm-4">
                <input id="BtnPrint" type="button" value="Print" onclick="javascript: printDiv('DivBody')" class="btn btn-primary btn-block btn-flat" />
            </div>
            <div class="col-sm-4">
                 <input type="button" onclick="tableToExcel('DivBody', 'Transaction Log')" value="Export to Excel" class="btn btn-primary btn-block btn-flat">
            </div>
        </div>
    </form>
</body>
</html>
