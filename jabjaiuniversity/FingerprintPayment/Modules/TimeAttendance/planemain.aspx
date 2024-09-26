<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="planemain.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.planemain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12" style="margin-top: 20px;">
        <div class="col-md-3"></div>
        <div class="col-md-6">
            <div class="card box-content mini hover">
                <h3 name="reportjobscan"><a href="planelist.aspx"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132622") %></a></h3>
            </div>
            <div class="card box-content mini hover">
                <h3 name="reportjobscan"><a href="periodslist.aspx"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132623") %></a></h3>
            </div>
            <div class="card box-content mini hover">
                <h3 name="reportempscan"><a href="plans-term.aspx">สร้างตารางสอน</a></h3>
            </div>
        </div>
        <div class="col-md-3"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
