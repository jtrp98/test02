<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="manual2.aspx.cs" Inherits="FingerprintPayment.manual.manual2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="row">
            <div class="col-lg-6">
                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132432") %></h1>
                <ul class="list-group" style="margin-left: 10px;">
                    <li class="list-group-item justify-content-between">
                        <a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132432") %>/วิธีกา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>สมัคแอ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132049") %>.pdf" target="_blank">วิธีกา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>สมัคแอ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132049") %></a>
                    </li>
                    <li class="list-group-item justify-content-between">
                        <a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132432") %>/วิธีกา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>สมัคแอ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>สำห<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ับ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102166") %>.pdf" target="_blank">วิธีกา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>สมัคแอ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>สำห<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ับ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102166") %></a>
                    </li>
                </ul>
            </div>
            <div class="col-lg-6"></div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
