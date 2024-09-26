<%@ Page Title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00992") %>" MasterPageFile="~/Material2.Master" Language="C#" AutoEventWireup="true" CodeBehind="NoPermission.aspx.cs" Inherits="FingerprintPayment.NoPermission" %>


<%--<!DOCTYPE html>
<html lang="id" dir="ltr">

<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="icon" href="/images/School Bright logo only.png" sizes="16x16 32x32" type="image/png">
    <!-- Title -->
    <title>No Permission</title>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" />
    <link rel="stylesheet" href="//stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" integrity="sha384-WskhaSGFgHYWDcbwN70/dfYBj47jz9qbsMId/iRN3ewGhXQFZCSftd1LZCfmhktB" crossorigin="anonymous" />
</head>

<body class="bg-white text-black py-5">
    <div class="container py-5">
        <div class="row">
            <div class="col-md-4 text-right">
                <p><i class="fa fa-exclamation-triangle fa-6x" style="font-size: 8em; color: #ffc107"></i></p>
            </div>
            <div class="col-md-8 text-left">
                <h1 style="margin-bottom: 1rem;">ACCESS DENIED</h1>
                <p style="font-size: 18px;">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131098") %> 
                    <br />
                    โปรดติดต่อครูผู้ดูแลระบบโรงเรียนท่าน เพื่อทำการแก้ไขสิทธิขอบคุณค่ะ
                </p>

                <a class="btn btn-danger" href="AdminMain.aspx"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131164") %></a>
            </div>
        </div>
    </div>

    <div id="footer" class="text-center">
    </div>
</body>

</html>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card ">
        <div class="row">
            <div class="col-md-12 text-center " style="padding: 10px">
                <img src="images/access@2x.png" style="width:40%" />
            </div>
            <div class="col-md-12 text-center " style="padding: 10px">
                <h2 style="color:#ed8229"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00992") %></h2>
            </div>
            <div class="col-md-12 text-center " style="padding: 10px">
                <h4 style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131098") %></h4>
                <h4>โปรดติดต่อครูผู้ดูแลระบบโรงเรียนของท่าน เพื่อทำการแก้ไขสิทธิ์ ขอบคุณค่ะ</h4>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
