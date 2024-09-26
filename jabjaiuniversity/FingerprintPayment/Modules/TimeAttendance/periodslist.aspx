<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="periodslist.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.periodslist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="periodlist-container" style="background: white;">
        <asp:ListView ID="lvListData" runat="server" GroupPlaceholderID="groupPlaceHolder1"
            ItemPlaceholderID="itemPlaceHolder1" OnPagePropertiesChanging="OnPagePropertiesChanging"
            OnSelectedIndexChanged="lvListData_SelectedIndexChanged"
            OnItemCommand="lvListData_ItemCommand">
            <LayoutTemplate>
                <table class="table table-hover cool-table periodlist-table">
                    <tr class="headerCell" style="background-color: rgb(51, 122, 183); color: white;">
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132617") %>
                        </td>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132618") %>
                        </td>
                        <td>
                            <a href="/Modules/TimeAttendance/periodsadd.aspx" class="btn btn-info btn-custom" data-toggle="modal" data-target="#modalpopupdatabig"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132619") %></a>
                            <%--  <asp:Button CssClass="btn btn-success glyphicon glyphicon-plus" ID="btnSave" runat="server" 
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132619") %>" PostBackUrl="~/Modules/TimeAttendance/periodsadd.aspx" />--%>
                        </td>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                    <tr>
                        <td colspan="3">
                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvListData" PageSize="20">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                        ShowNextPageButton="false" />
                                    <asp:NumericPagerField ButtonType="Link" />
                                    <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="false" ShowLastPageButton="false"
                                        ShowPreviousPageButton="false" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <EmptyDataTemplate>
                <table class="table table-hover cool-table periodlist-table">
                    <tr class="headerCell" style="background-color: rgb(51, 122, 183); color: white;">
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132617") %>
                        </td>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132618") %>
                        </td>
                        <td>
                            <a href="/Modules/TimeAttendance/periodsadd.aspx" class="btn btn-info btn-custom" data-toggle="modal" data-target="#modalpopupdatabig"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132619") %></a>
                            <%--  <asp:Button CssClass="btn btn-success glyphicon glyphicon-plus" ID="btnSave" runat="server" 
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132619") %>" PostBackUrl="~/Modules/TimeAttendance/periodsadd.aspx" />--%>
                        </td>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                    <tr>
                        <td colspan="3">
                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvListData" PageSize="20">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                        ShowNextPageButton="false" />
                                    <asp:NumericPagerField ButtonType="Link" />
                                    <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="false" ShowLastPageButton="false"
                                        ShowPreviousPageButton="false" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <GroupTemplate>
                <tr>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                </tr>
            </GroupTemplate>
            <ItemTemplate>
                <tr class='active'>
                    <td style='text-align: center;'>
                        <%# Eval("sPeriodName")%>
                    </td>
                    <td style='text-align: center;'>
                        <%# DateTime.Parse(Eval("dStart").ToString()).ToShortTimeString()%>
                        -
                        <%# DateTime.Parse(Eval("dEnd").ToString()).ToShortTimeString()%>
                    </td>
                    <td style='text-align: left;'>
                        <a href="periodsedit.aspx?id=<%# Eval("sPeriodID") %>" data-toggle="modal" data-target="#modalpopupdatabig" class="btn btn-primary global-btn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %></a>
                        <asp:Button runat="server" ID="btnDel" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" CommandName="Del" CssClass="btn btn-danger global-btn" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
