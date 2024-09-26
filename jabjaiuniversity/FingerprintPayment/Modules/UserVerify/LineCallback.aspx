<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineCallback.aspx.cs" Inherits="FingerprintPayment.Modules.UserVerify.LineCallback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="icon" href="https://dev.schoolbright.co/images/School Bright logo only.png" sizes="16x16 32x32" type="image/png">
    <!-- Title -->
    <title>Line Callback</title>
    <!-- Fonts and icons -->
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" />
    <!-- CSS Files -->
    <link href="/Content/Material/assets/css/material-dashboard.css?v=2.1.0" rel="stylesheet" />
    <style>
        @import url("/Content/Material/assets/webfonts/TH_Sarabun/thsarabunnew.css");

        body {
            font-family: THSarabunNew;
        }
    </style>
</head>
<body class="bg-white text-black py-5">
    <div class="container py-3">
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Literal ID="ltrIcon" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center line-message py-4" style="font-size: 1.1rem; font-weight: bold; color: gray;">
                <asp:Literal ID="ltrMessage" runat="server"></asp:Literal>
                <asp:Literal ID="ltrErrorMessage" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <a id="btnClose" class="btn btn-success" href="sbapp://drawers/linesuccess/<%=SCHOOLID%>/<%=STUDENTID%>" style="border-radius: 25px; font-size: 1.3rem; width: 225px; height: 50px; display: inline-block; background: linear-gradient(90deg, rgba(40,196,122,1) 35%, rgba(95,211,157,1) 100%);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></a>
            </div>
        </div>
    </div>
    <asp:Literal ID="ltrScript" runat="server"></asp:Literal>
</body>
</html>
