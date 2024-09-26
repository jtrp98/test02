<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="menureport.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.menureport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="col-md-12" style="margin-top:20px;">
    <div class="col-md-3"></div>
    <div class="col-md-6"> 
    <div class="card box-content mini hover">
    <h3 name="reportjobscan"><a href="../../Reportmobile01.aspx"> <i class="glyphicon glyphicon-new-window"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132610") %></a></h3>
    </div>

   <%-- <div class="card box-content mini hover">
    <h3 name="reportjobscan"> <a href="reportlearnscanning.aspx"><i class="glyphicon glyphicon-new-window"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132611") %></a></h3>
    </div>--%>

     <div class="card box-content mini hover">
    <h3 name="reportempscan"> <a href="reportempscanning.aspx"><i class="glyphicon glyphicon-new-window"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132612") %></a></h3>
    </div>
    </div>
    <div class="col-md-3"></div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
