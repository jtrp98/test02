<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="homeworklist.aspx.cs"
    Inherits="FingerprintPayment.homeworklist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--   <script type="text/javascript">
        $(function () {
            $.get("/App_Logic/dataJSONArray.ashx?mode=listplane4homework", "", function (msg) {
                var dll = $('select[id*=ddlcType]');
                $('select[id*=ddlcType] option').remove();
                dll.append($("<option></option>")
                    .attr("value", " ")
                    .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"));
                $.each(msg, function (index) {
                    dll.append($("<option></option>")
                        .attr("value", msg[index].planeid)
                        .text(msg[index].planename));
                });
            });
        })
    </script>--%>
    <script src="/bootstrap/bootstrap-chosen/chosen.jquery.js" type="text/javascript"></script>
    <link href="/bootstrap/bootstrap-chosen/bootstrap-chosen.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $('.chosen-select').chosen();
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content smssetting-container">
        <div class="row">
            <div class="col-lg-1 col-md-1 col-xs-2" style="margin-left: 5px;">
                <label class="label--filter"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %></label>
            </div>
            <div class="col-lg-6 col-md-6 col-xs-5">
                <asp:DropDownList class="input--filter form-control chosen-select" ID="ddlcType" runat="server" AutoPostBack="true">
                </asp:DropDownList>
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
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="nHomeWork" Visible="false" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="sPlaneName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %>">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132133") %>">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# ((DateTime)Eval("dOrder")).ToString("dd/MM/yyyy") %>
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
                                    <a href="homeworkadd.aspx" class="btn btn-success custom-button btnpermission"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401011") %></a>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="btnEdit" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>" CommandName="Edit" CssClass="btn btn-info custom-button button-top btnpermission" Visible='<%# DateTime.Parse(Eval("dEnd").ToString()) > DateTime.Now ? true :false  %>' />
                                    <asp:LinkButton ID="btnDel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" CommandName="Del" CssClass="btn btn-danger custom-button btnpermission" Visible='<%# DateTime.Parse(Eval("dEnd").ToString()) > DateTime.Now ? true :false  %>' />--%>
                                    <asp:LinkButton ID="btnEdit" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>" CommandName="Edit" CssClass="btn btn-info custom-button button-top btnpermission" Visible="false" />
                                    <asp:LinkButton ID="btnDel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" CommandName="Del" CssClass="btn btn-danger custom-button btnpermission" Visible="false" />
                                    <a href="homeworkdetail.aspx?id=<%# Eval("nHomeWork").ToString() %>" class="btn-link"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302012") %></a>
                                </ItemTemplate>
                                <HeaderStyle></HeaderStyle>
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
