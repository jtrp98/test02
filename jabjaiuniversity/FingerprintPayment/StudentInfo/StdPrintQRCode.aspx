<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StdPrintQRCode.aspx.cs" Inherits="FingerprintPayment.StudentInfo.StdPrintQRCode" %>

<!DOCTYPE html>
<html>
<head>
    <link rel="apple-touch-icon" sizes="76x76" href="/PreRegister/assets/img/apple-icon.png" />
    <link rel="icon" type="image/png" href="/PreRegister/assets/img/favicon.png" />
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <title></title>
    <link rel="stylesheet" href="/Styles/style-student-qrcode-print.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        @import url("/PreRegister/webfonts/TH_Sarabun/thsarabunnew.css");

        body {
            font-family: THSarabunNew;
        }

        .btn.btn-info:active:hover {
            color: #fff;
            background-color: #00aec5;
            border-color: #004b55;
        }

        .btn-info {
            color: #ffffff;
            background-color: #00bcd4;
            border-color: #00bcd4;
            box-shadow: none;
        }

        .btn.btn-success:active:hover {
            color: #fff;
            background-color: #47a44b;
            border-color: #255627;
        }

        .btn.btn-success {
            color: #fff;
            background-color: #4caf50;
            border-color: #4caf50;
            box-shadow: 0 2px 2px 0 rgba(76, 175, 80, 0.14), 0 3px 1px -2px rgba(76, 175, 80, 0.2), 0 1px 5px 0 rgba(76, 175, 80, 0.12);
        }

        .btn.btn-info:active:hover, .btn.btn-info:active:focus, .btn.btn-info:active.focus, .btn.btn-info.active:hover, .btn.btn-info.active:focus, .btn.btn-info.active.focus, .open > .btn.btn-info.dropdown-toggle:hover, .open > .btn.btn-info.dropdown-toggle:focus, .open > .btn.btn-info.dropdown-toggle.focus, .show > .btn.btn-info.dropdown-toggle:hover, .show > .btn.btn-info.dropdown-toggle:focus, .show > .btn.btn-info.dropdown-toggle.focus {
            color: #fff;
            background-color: #00aec5;
            border-color: #004b55;
        }

        .btn:not(:disabled):not(.disabled):active, .btn:not(:disabled):not(.disabled).active {
            background-image: none;
            box-shadow: none;
        }

        .btn:not(:disabled):not(.disabled) {
            cursor: pointer;
        }

        .btn.btn-round {
            border-radius: 30px;
        }

        .btn:hover, .btn:focus {
            text-decoration: none;
        }

        .btn {
            position: relative;
            padding: 12px 30px;
            margin: 0.3125rem 1px;
            font-weight: 400;
            line-height: 1.428571;
            text-transform: uppercase;
            letter-spacing: 0;
            border: 0;
            will-change: box-shadow, transform;
        }

        .btn {
            display: inline-block;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            user-select: none;
        }

        button, html [type="button"], [type="reset"], [type="submit"] {
            -webkit-appearance: button;
        }

        button {
            overflow: visible;
        }

        button {
            font-family: inherit;
        }

        .table-fixed {
            table-layout: fixed;
        }

            .table-fixed td {
                white-space: nowrap;
            }
    </style>
    <script>
    </script>
</head>
<body>
    <div class="book">
        <asp:Literal ID="ltrPages" runat="server"></asp:Literal>
    </div>
</body>
</html>
<!--139621140-->
