<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="manual.aspx.cs" Inherits="FingerprintPayment.manual.manual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="row">
            <div class="col-lg-4">
                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132407") %></h1>
                <h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132408") %></h2>
                <ul class="list-group" style="margin-left: 10px;">
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/1.1.1%20วิธี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603091") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132409") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/1.1.2%20วิธี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601027") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132410") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/1.1.3%20วิธี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601027") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132411") %></a></li>
                </ul>
                <h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132412") %></h2>
                <ul class="list-group" style="margin-left: 10px;">
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/1.2.1%20วิธี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601039") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132413") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/1.2.2%20วิธี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601057") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132414") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/1.2.3%20วิธี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601067") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132415") %></a></li>
                </ul>
                <h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132416") %></h2>
                <ul class="list-group" style="margin-left: 10px;">
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/1.3.1%20วิธี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601068") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132417") %></a></li>
                </ul>
                <h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132418") %></h2>
                <ul class="list-group" style="margin-left: 10px;">
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/1.4.1%20วิธี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121075") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132419") %></a></li>
                </ul>
                <%--     <h2> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132416") %></h2>
                <ul class="list-group">
                    <li class="list-group-item justify-content-between"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132412") %></li>
                </ul>--%>
            </div>
            <div class="col-lg-4">
                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132420") %></h1>
                <ul class="list-group" style="margin-left: 10px;">
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/2.1%20<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802002") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132421") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/2.2%20วิธี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132422") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/2.3%20วิธี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %>%20-%20<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133257") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204050") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132423") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/2.4%20<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502011") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132424") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/2.5%20<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801002") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132425") %></a></li>
                </ul>
                <h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132426") %></h2>
                <ul class="list-group" style="margin-left: 10px;">
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/2.6.1%20วิธี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404030") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603001") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133256") %>%20-%20<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133257") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204050") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132427") %></a></li>
                </ul>
                <h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132428") %></h2>
                <ul class="list-group" style="margin-left: 10px;">
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/2.7.1<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132622") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132429") %></a></li>
                </ul>
            </div>
            <div class="col-lg-4">
                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132430") %></h1>
                <ul class="list-group">
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00375") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>เว็บ/3.1<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701026") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132431") %></a></li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
