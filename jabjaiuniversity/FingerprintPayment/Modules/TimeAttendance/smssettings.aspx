<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="smssettings.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.smssettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="/stylesheet" href="../../Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="../../Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }

        .listItem {
            color: blue;
            background-color: White;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTSMS" ServicePath="AutoCompleteService.asmx" EnableCaching="true"
        FirstRowSelected="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>
    <div class="full-card box-content smssetting-container">
        <div class="row">
            <div class="col-lg-1 col-md-1 col-xs-2" style="margin-left: 5px;">
                <label class="label--filter"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></label>
            </div>
            <div class="col-lg-4 col-md-4 col-xs-5">
                <asp:DropDownList class="input--filter form-control" ID="ddlcType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcType_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-xs-2" style="display: none;">
                <label class="pull-right label--filter"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132664") %></label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtSearch" Visible="false" class="input--filter" runat="server" AutoCompleteType="Disabled" />
            </div>
        </div>

        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%"
                        CellPadding="2" GridLines="None" AllowPaging="True" Font-Bold="False"
                        Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20"
                        CssClass="cool-table">
                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <Columns>
                            <asp:BoundColumn DataField="index" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="200"></HeaderStyle>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="nSMS" Visible="false" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="200"></HeaderStyle>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SMSTitle" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401046") %>">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="200"></HeaderStyle>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00166") %>">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="true" />
                                <ItemTemplate>
                                    <%# ((string)Eval("SMSDesp"))  %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132133") %>">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="200"></HeaderStyle>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# ((DateTime)Eval("dSend")).ToString("dd/MM/yyyy HH:mm") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <%--<asp:TemplateColumn HeaderStyle-Width="100px">
                                <HeaderTemplate>
                                    <img alt="" src="/images/edit_button.png" />
                                    <asp:LinkButton ID="btnDel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132134") %>" 
                                        CommandName="Add" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                </ItemTemplate>
                                <HeaderStyle Width="120px"></HeaderStyle>
                            </asp:TemplateColumn>--%>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:LinkButton ID="btnMain" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401011") %>" CssClass="btn btn-success custom-button btnpermission"
                                        CommandName="Add" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%--    <asp:LinkButton ID="btnEdit"  runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>" CommandName="Edit" CssClass="btn btn-info custom-button button-top btnpermission" Visible='<%# DateTime.Parse(Eval("dSend").ToString()) > DateTime.Now ? true :false  %>' />
                                    <asp:LinkButton ID="btnDel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" CommandName="Del" CssClass="btn btn-danger custom-button btnpermission" Visible='<%# DateTime.Parse(Eval("dSend").ToString()) > DateTime.Now ? true :false  %>' />--%>
                                    <asp:LinkButton ID="btnEdit" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>" CommandName="Edit" CssClass="btn btn-info custom-button button-top btnpermission" Visible="false" />
                                    <asp:LinkButton ID="btnDel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" CommandName="Del" CssClass="btn btn-danger custom-button btnpermission" Visible="false" />
                                    <a href="smsdetail.aspx?id=<%# Eval("nSMS").ToString() %>" class="btn-link"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302012") %></a>
                                </ItemTemplate>
                                <HeaderStyle Width="200"></HeaderStyle>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="headerCell" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="itemCell" />
                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Font-Bold="False"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="pagerCell" />
                        <SelectedItemStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
