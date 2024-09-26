<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="levelsettings.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.levelsettings" %>

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

        .modal {
            z-index: 9999;
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
        function GetData(Index) {
            $('input[id*=rblTimeType_' + Index + ']').attr("checked", "checked");
            __doPostBack();
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true">
    </asp:ScriptManager>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            getPermission();
        })
        function getPermission() {
            PageMethods.GetPermission(OnMyMethodComplete);
        }
        function OnMyMethodComplete(result, userContext, methodName) {
            if (result == 1) {
                $('.btnpermission').css("display", "none");
            }
        }

    </script>
    <div class="full-card box-content levelsetting-container">
        <asp:RadioButtonList ID="rblTimeType" runat="server" class="tab-pane active" RepeatDirection="Horizontal"
            Style="display: none;" AutoPostBack="True" />
        <div class="row">
            <div class="col-xs-1">
            </div>
            <div class="col-xs-11">
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="col-xs-12">
                    <div class="wrapper-table" style="text-align: center">
                        <asp:Literal ID="ltrTabHeader" runat="server" />
                        <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
                            GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                            <Columns>
                                <asp:BoundColumn DataField="nTLevel" HeaderStyle-Width="200px" Visible="false" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>">
                                    <HeaderStyle Width="200px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="LevelName" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %>">
                                    <HeaderStyle Width="200px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="btnAdd" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132609") %>" CommandName="Add" CssClass="col3 btn btn-success btnpermission" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302012") %>" CommandName="Data" />
                                        <asp:LinkButton ID="btnDel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" CommandName="Del" Visible='<%# Eval("nCout").ToString() == "0"?true:false %>' CssClass="btnpermission" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="120px"></HeaderStyle>
                                </asp:TemplateColumn>
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
