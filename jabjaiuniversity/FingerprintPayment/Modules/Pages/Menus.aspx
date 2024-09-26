<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BeforeLoginMaster.Master" AutoEventWireup="true" CodeBehind="Menus.aspx.cs" Inherits="FingerprintPayment.Modules.Pages.Menus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row">
        <div class="col-xs-4">
            <div class="card box-content">
                <h3 class="highlight"><i class="glyphicon glyphicon-pushpin"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132539") %>
                </h3>
                <ul>
                    <li><a href="/Modules/TimeAttendance/jobscaning.aspx">- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132540") %></a></li>
                    <li><a href="/Modules/TimeAttendance/empscanning.aspx">- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132541") %> </a></li>
                </ul>
            </div>
        </div>

        <div class="col-xs-4">
            <div class="card box-content">
                <h3 class="highlight"><i class="glyphicon glyphicon-pushpin"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132542") %>
                </h3>
                <ul>
                    <li><a href="#">- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132543") %></a></li>
                </ul>
            </div>
        </div>
        <//div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
