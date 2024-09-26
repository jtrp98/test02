<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="manual4.aspx.cs" Inherits="FingerprintPayment.manual.manual4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="row">
            <div class="col-lg-4">
                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132446") %></h1>
                <ul class="list-group" style="margin-left: 10px;">
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132446") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132447") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132447") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132446") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132448") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132448") %></a></li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
