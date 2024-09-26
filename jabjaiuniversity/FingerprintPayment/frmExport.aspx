<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmExport.aspx.cs" Inherits="FingerprintPayment.frmExport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <link href="/bootstrap/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <script src="/bootstrap/js/bootstrap.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 99%">
            <tr>
                <td>
                    <asp:Button ID="btnExportPdf" CssClass="btn btn-success hidden" Width="99%" runat="server" Text="Export PDF" />
                    <asp:Button ID="btnExport" CssClass="btn btn-success" Width="99%" runat="server" Text="Export Excel" />
                </td>
            </tr>
        </table>
        <br />
        <div>
            <table id="tbl" class='table table-condensed table-bordered' style="font-size: 20px;"
                cellspacing="0" width="100%" runat="server">
            </table>
        </div>
    </form>
</body>
</html>
