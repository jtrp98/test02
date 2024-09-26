<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="sublevelsettings.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.sublevelsettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        .nav-tabs > li.active > a, .nav-tabs > li.active > a:hover, .nav-tabs > li.active > a:focus {
            background-color: #fff585;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })
        function GetData(Index) {
            $('input[id*=rblTimeType_' + Index + ']').attr("checked", "checked");
            __doPostBack();
        }
        function ListSublv(sublvid) {
            if ($('#' + sublvid).attr("class") == "glyphicon glyphicon-triangle-right") {
                $('tr[id*=' + sublvid + ']').removeClass("hidden");
                $('#' + sublvid).removeClass("glyphicon-triangle-right");
                $('#' + sublvid).addClass("glyphicon-triangle-bottom");
            } else {
                $('tr[id*=' + sublvid + ']').addClass("hidden");
                $('#' + sublvid).removeClass("glyphicon-triangle-bottom");
                $('#' + sublvid).addClass("glyphicon-triangle-right");
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="full-card box-content sublevelsetting-container">
        <asp:RadioButtonList ID="rblTimeType" runat="server" class="tab-pane active" RepeatDirection="Horizontal"
            Style="display: none;" />
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="col-xs-10">
                </div>
                <div class="col-xs-2">
                </div>
            </div>
            <div class="col-xs-12">
                <div class="col-xs-12">
                    <div class="wrapper-table">
                        <asp:Literal ID="ltrTabHeader" runat="server" />
                        <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
                            GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table cool-table-3">
                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                            <Columns>
                                <%--  <asp:BoundColumn DataField="SubLevel" HeaderStyle-Width="200px">
                                    <HeaderStyle Width="200px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>--%>
                                <asp:TemplateColumn HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %>">
                                    <ItemTemplate>
                                        <div class="glyphicon glyphicon-triangle-right" id="sublv<%# Eval("nTSubLevel") %>" style="cursor: pointer;" onclick="ListSublv('sublv<%# Eval("nTSubLevel") %>')">
                                            <span class="text-sublevel"><%# Eval("SubLevel") %></span>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="sTimeType" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803010") %>"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="btnDel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132670") %>" CommandName="Add" CssClass="btn btn-success btnpermission" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="btnTable" CommandName="AddTable" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132671") %>"
                                            CssClass="btn btn-danger custom-button btnpermission" />
                                        &nbsp;<asp:LinkButton runat="server" ID="btnEdit" CommandName="EditTable" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132672") %>"
                                            CssClass="btn btn-primary custom-button btnpermission" data-toggle="tooltip" data-placement="right" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102027") %>" />
                                        <asp:Label ID="lblListSub" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <%--  <asp:TemplateColumn>
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>
                                <asp:BoundColumn DataField="nTSubLevel" HeaderText="nTSubLevel" Visible="false" />
                                <asp:BoundColumn DataField="SubLevel" HeaderText="SubLevel" Visible="false" />
                            </Columns>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" CssClass="headerCell" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" CssClass="itemCell" />
                            <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="pagerCell" />
                            <SelectedItemStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
