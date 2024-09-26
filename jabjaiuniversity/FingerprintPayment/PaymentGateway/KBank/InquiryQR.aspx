<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InquiryQR.aspx.cs" Inherits="FingerprintPayment.PaymentGateway.KBank.InquiryQR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtCharge" runat="server" Rows="10" TextMode="MultiLine" Width="412px"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtsKey" runat="server" Height="16px" Width="403px"></asp:TextBox>
            <br />
            <asp:Button ID="btnCheck" runat="server" OnClick="btnCheck_Click" Text="Check" />
            <br />
            <asp:Label ID="lblResults" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblOrderIDList" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
