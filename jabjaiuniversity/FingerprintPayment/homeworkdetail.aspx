<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="homeworkdetail.aspx.cs" Inherits="FingerprintPayment.homeworkdetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="detail-card box-content smsadd-container periodadd-container">
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %></label>
            </div>
            <div class="col-xs-8">
                <asp:Literal ID="ltrPlane" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131074") %></label>
            </div>
            <div class="col-xs-4">
                <asp:Literal ID="ltrdayStart" runat="server" />
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %>
                <asp:Literal ID="ltrdayEnd" runat="server" />
            </div>
            <div class="col-xs-4">
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131075") %></label>
            </div>
            <div class="col-xs-8">
                <asp:Literal ID="ltrdayNotification" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131077") %></label>
            </div>
            <div class="col-xs-8">
                <asp:Literal ID="ltrteacher" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202042") %></label>
            </div>
            <div class="col-xs-8">
                <asp:Literal ID="ltrfile" runat="server" />           
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131076") %></label>
            </div>
            <div class="col-xs-8">
                <asp:Literal ID="ltrDetail" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-1">
            </div>
            <div class="col-xs-10">
                <asp:Literal ID="ltrTable" runat="server" />
            </div>
            <div class="col-xs-1">
            </div>
        </div>
        <div class="listuser type1"></div>
        <div class="row center">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
