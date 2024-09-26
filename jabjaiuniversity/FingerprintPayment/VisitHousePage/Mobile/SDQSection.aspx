<%@ Page Title="" Language="C#" MasterPageFile="~/Material3.Master" AutoEventWireup="true" CodeBehind="SDQSection.aspx.cs" Inherits="FingerprintPayment.VisitHousePage.Mobile.SDQSection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content contents">
        <div class="title-bar" style="display: none;">
            28 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %> 2565
        </div>
        <div class="user">
            <div class="d-flex align-items-center justify-content-between">
                <div class="w-100 d-flex align-items-center px-4">
                    <img src="<%=StudentPicture%>" alt="user" style="width: 50px; height: 50px; border-radius: 50%;">
                    <div>
                        <p><%=StudentCode%></p>
                        <p><%=StudentName%></p>
                        <p><%=StudentClass%></p>
                    </div>
                </div>
            </div>
        </div>
        <a class="choose-part" data-section="1" href="SDQForm.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&sid=<%=Request.QueryString["sid"]%>&type=<%=Request.QueryString["type"]%>&client=<%=Request.QueryString["client"]%>&section=1">
            <p class="m-0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133475") %></p>
            <span>&#x203A;</span>
        </a>
        <a class="choose-part" data-section="2" href="SDQForm.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&sid=<%=Request.QueryString["sid"]%>&type=<%=Request.QueryString["type"]%>&client=<%=Request.QueryString["client"]%>&section=2">
            <p class="m-0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133476") %></p>
            <span>&#x203A;</span>
        </a>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
