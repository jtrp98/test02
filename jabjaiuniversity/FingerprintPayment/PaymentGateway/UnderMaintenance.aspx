<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnderMaintenance.aspx.cs" Inherits="FingerprintPayment.PaymentGateway.UnderMaintenance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        /* https://www.cleancss.com/css-minify/ */
        @font-face {font-family: THSarabun;src: url(../../fonts/THSarabun.ttf) }html, body {height: 100%;display: flex;flex-direction: column;font-family: THSarabun }.section-top {height: 90% }.section-bottom {flex-grow: 1;margin-top: 20px }.section-bottom.under-maintenance {text-align: center;margin-top: 5px;}.section-bottom.under-maintenance .confirm-btn {text-decoration: none;width: 90% !important;border-radius: 25px;background-image: linear-gradient(to right, #29c47b, #63d39f);}.container {height: 100%;display: flex;flex-direction: column }.confirm-btn {text-align: center;padding: 15px 0 0 }.btn-success {color: #fff;background-color: #5cb85c;border-color: #4cae4c }.btn {display: inline-block;margin-bottom: 0;font-weight: 400;text-align: center;white-space: nowrap;vertical-align: middle;-ms-touch-action: manipulation;touch-action: manipulation;cursor: pointer;background-image: none;border: 1px solid transparent;padding: 6px 0;font-size: 14px;line-height: 1.42857143;border-radius: 4px;-webkit-user-select: none;-moz-user-select: none;-ms-user-select: none;user-select: none;margin-top: 10px;font-size: 23px;width: 100% }.under-maintenance p.message {background-color: #ff5e5e;border-radius: 20px;padding: 22px 34px;color: white;font-size: 30px;}.under-maintenance img.contact {width: 24%;float: left;margin-right: 12px;}.under-maintenance p.contact {text-align: left;font-size: 24px;font-weight: bold;margin: 0px;}hr.under-maintenance {width: 92%;border-top: 2px solid orange;margin-top: 15px;}
    </style>
    <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
</head>
<body>
    <asp:Literal ID="ltrPay" runat="server"></asp:Literal>
</body>
</html>
