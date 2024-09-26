<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="UpdateLog.aspx.cs" Inherits="FingerprintPayment.UpdateLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content" style="padding: 15px 30px;">
        <div class="card-body">
            <h1 class="card-title"><b>Update log</b></h1>
            <hr style="margin: 10px 0px 10px 0px; border: 1px solid;"/>
            <div class="list-group notification-li">
                <asp:Literal ID="ltrMessageSystem" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
