<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="reportmain.aspx.cs" Inherits="FingerprintPayment.App_Code.reportsmain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('div[id=sub]').hide();

            $('div[id=main]').click(function () {
                if ($('a[id=link]').html() == "+ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603005") %>") {
                    $('div[id=sub]').show();
                    $('a[id=link]').html("- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603005") %>");
                }
                else {
                    $('div[id=sub]').hide();
                    $('a[id=link]').html("+ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603005") %>");
                }
            });
        });

    </script>
    <style type="text/css">
        .long-btn {
            font-size: 34px !important;
            width: 400px;
            text-align:left;
        }
        .btn:hover, .btn:focus, .focus.btn {
        color:#fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div class="detail-card box-content">
        <div id="main" class="row center">
            <a href='#' id="link" class="btn btn-info long-btn">+ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603005") %></a>
        </div>
        <div class="row center" id="sub">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='report03.aspx' class="btn btn-info long-btn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131125") %></a>
        </div>
        <div class="row center" id="sub">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='report08.aspx' class="btn btn-info long-btn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131126") %></a>
        </div>
        <div class="row center" id="sub">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='report05.aspx' class="btn btn-info long-btn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131122") %></a>
        </div>
        <div class="row center" id="sub">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='report09.aspx' class="btn btn-info long-btn">รายงานการการเต็มเงิน</a>
        </div>
        <div class="row center">
            <a href='report04.aspx' class="btn btn-info long-btn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603089") %></a>
        </div>
    </div>--%>
    <asp:Literal ID="ltrReportMenu" runat="server" />
</asp:Content>
