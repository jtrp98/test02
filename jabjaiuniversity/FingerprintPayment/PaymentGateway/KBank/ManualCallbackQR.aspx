<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManualCallbackQR.aspx.cs" Inherits="FingerprintPayment.PaymentGateway.KBank.ManualCallbackQR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtOrderID" runat="server" Rows="10" TextMode="MultiLine" Width="369px"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtsKey" runat="server" Height="22px" Width="369px"></asp:TextBox>
            <br />
            <asp:Button ID="btnTopup" runat="server" OnClick="btnTopup_Click" Text="Topup" />
            <br />
            <asp:Label ID="lblResult" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
