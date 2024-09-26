﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="manual3.aspx.cs" Inherits="FingerprintPayment.manual.manual3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="row">
            <div class="col-lg-4">
                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132435") %></h1>
                <ul class="list-group" style="margin-left: 10px;">
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132435") %>/1.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00734") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>อง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00200") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132436") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132435") %>/2.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502011") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131141") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132437") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132435") %>/3.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133256") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133257") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204050") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132438") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132435") %>/4.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603001") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701068") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132439") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132435") %>/5.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603001") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00613") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132440") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132435") %>/6.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132471") %>.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132441") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132435") %>/7.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00094") %>เ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>ลี่ยนภาษา.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132442") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132435") %>/8.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>เกี่ยว<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132955") %>เ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>า.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132443") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132435") %>/9.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203033") %>เสนอแนะ.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132444") %></a></li>
                    <li class="list-group-item justify-content-between"><a href="/Downloads/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132435") %>/10.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>สี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>อง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>ต่างๆ.pdf" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132445") %></a></li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
