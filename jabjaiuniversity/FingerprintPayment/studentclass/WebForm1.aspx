<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="FingerprintPayment.studentclass.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList runat="server" ID="ddlyear"></asp:DropDownList>
    <br>
    <asp:Button runat="server" text="Update" ID="Button1" OnClick="btnSubmit_Click" />
    <%--<asp:Button runat="server" text="Delete" ID="btnDelete" OnClick="btnDelete_Click" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
