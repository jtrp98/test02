<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="report01.aspx.cs" Inherits="FingerprintPayment.repotr01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="/bootstrap/bootstrap-chosen/bootstrap-chosen.css" rel="stylesheet" />
    <style type="text/css">
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

        @media (max-width: 999px) {
            .custom-button {
                font-size: 16px;
                width: 200px;
            }
        }

        @media (min-width: 1000px) and (max-width: 1199px) {
            .custom-button {
                font-size: 18px;
                width: 300px;
            }
        }

        @media (min-width: 1200px) {
            .custom-button {
                font-size: 24px;
                width: 400px;
            }
        }

        .headerCell {
            text-align: center;
        }

        .itemCell td {
            text-align: center;
        }

        .alternateCell td {
            text-align: center;
        }
    </style>
    <script src="../Scripts/bootstrap-datepicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $(".datepicker").datepicker({
                format: 'dd/mm/yyyy',
                locale: 'th',
                debug: false,
                //defaultDate: "<%=DateTime.Now.ToString("dd/MM/yyyy") %>",
                //autoclose: true,
                //autoclose: true,
                //showOn: "button",
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-chevron-up",
                    down: "fa fa-chevron-down",
                    previous: 'fa fa-chevron-left',
                    next: 'fa fa-chevron-right',
                    today: 'fa fa-screenshot',
                    clear: 'fa fa-trash',
                    close: 'fa fa-remove'
                }
            });
            $('.chosen-select').chosen({ allow_single_deselect: true, allow_single_deselect: true });
            //$(".chosen-select").chosen().change(function (e, params) {
            //    var values = $(".chosen-select").chosen().val();
            //    alert(values);
            //    // params.selected and params.deselected will now contain the values of the
            //    // or deselected elements.
            //});
            //$('.select2').select2();
            //            var d = new Date();
            //            var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);
            //            $("input[id$='txtdDate']").datepicker({ showOn: 'button', buttonImageOnly: true, buttonImage: 'images/calendar_blue.png', changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy', isBuddhist: true, defaultDate: toDay, dayNames: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202007") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202008") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202009") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603009") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202011") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202012") %>'],
            //                dayNamesMin: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131005") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131006") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131007") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131008") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131009") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131010") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131011") %>'],
            //                monthNames: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>'],
            //                monthNamesShort: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210010") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210012") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210014") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210015") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210016") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210017") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210018") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210019") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210020") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210021") %>']
            //            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="full-card box-content report01-container">
        <div class="row">
            <div class="col-md-2 col-xs-12">
                <label class="pull-right label--filter">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %>
                </label>
            </div>
            <div class="col-md-4 col-xs-12">
                <asp:DropDownList ID="txtSearch" runat="server" CssClass='chosen-select col-md-12 col-xs-12' data-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105004") %>">
                </asp:DropDownList>
            </div>
            <div class="col-md-2 col-xs-12">
                <label class="pull-right label--filter">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701011") %>
                </label>
            </div>
            <div class="col-md-4 col-xs-12">
                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="chosen-select col-md-12 col-xs-12" data-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701012") %>" Width="100%">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201047") %></asp:ListItem>
                    <asp:ListItem Value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701014") %></asp:ListItem>
                    <asp:ListItem Value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %></asp:ListItem>
                    <asp:ListItem Value="3">สังการบ้าน</asp:ListItem>
                    <asp:ListItem Value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701017") %></asp:ListItem>
                    <asp:ListItem Value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701018") %></asp:ListItem>
                    <asp:ListItem Value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603091") %></asp:ListItem>
                    <asp:ListItem Value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601039") %></asp:ListItem>
                    <asp:ListItem Value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601068") %></asp:ListItem>
                    <asp:ListItem Value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301045") %></asp:ListItem>
                    <asp:ListItem Value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701023") %></asp:ListItem>
                    <asp:ListItem Value="11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102227") %></asp:ListItem>
                    <asp:ListItem Value="12"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></asp:ListItem>
                    <asp:ListItem Value="13"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701026") %></asp:ListItem>
                    <asp:ListItem Value="14"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></asp:ListItem>
                    <asp:ListItem Value="15"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301045") %></asp:ListItem>
                    <asp:ListItem Value="16"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701028") %></asp:ListItem>
                    <asp:ListItem Value="17"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701029") %></asp:ListItem>
                    <asp:ListItem Value="18"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></asp:ListItem>
                    <asp:ListItem Value="19"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102045") %></asp:ListItem>
                    <asp:ListItem Value="20"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %></asp:ListItem>
                    <asp:ListItem Value="21"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %></asp:ListItem>
                    <asp:ListItem Value="22"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701034") %></asp:ListItem>
                    <asp:ListItem Value="23"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210005") %></asp:ListItem>
                    <asp:ListItem Value="24">ตำแแหน่ง</asp:ListItem>
                    <asp:ListItem Value="25"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701037") %></asp:ListItem>
                    <asp:ListItem Value="26"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206342") %></asp:ListItem>
                    <asp:ListItem Value="999">Login</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row row--space">
            <div class="col-md-2 col-xs-12">
                <label class="pull-right label--filter">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701005") %>
                </label>
            </div>
            <div class="col-md-4 col-xs-12">
                <asp:DropDownList ID="ddlaction" runat="server" CssClass="chosen-select col-md-12 col-xs-12" data-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701006") %>">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></asp:ListItem>
                    <asp:ListItem Value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %></asp:ListItem>
                    <asp:ListItem Value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2 col-xs-12">
                <label class="pull-right label--filter">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701039") %>
                </label>
            </div>
            <div class="col-md-4 col-xs-12">
                <asp:DropDownList ID="ddlplatform" runat="server" CssClass="chosen-select col-md-12 col-xs-12" data-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701040") %>">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701041") %></asp:ListItem>
                    <asp:ListItem Value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701042") %></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row row--space">
            <div class="col-md-2 col-xs-12">
                <label class="pull-right label--filter">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701010") %>
                </label>
            </div>
            <div class="col-md-4 col-xs-12">
                <asp:TextBox ID="txtdDate" runat="server" class="form-control datepicker col-md-12 col-xs-12" Width="100%" />
                <%--<div class="input-group">
                    <div class="input-group-addon">
                        <i class="glyphicon glyphicon-calendar"></i>
                    </div>
                </div>--%>
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12 center">
                <asp:Button ID="btnSearch" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" class="btn btn-primary mid-btn global-btn" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <Columns>
                            <asp:BoundColumn DataField="sName" HeaderStyle-Width="25%" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %>">
                                <HeaderStyle Width="25%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="dLog" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderStyle-Width="15%"
                                HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %>">
                                <HeaderStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="sLog" HeaderStyle-Width="60%" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701046") %>">
                                <HeaderStyle Width="60%"></HeaderStyle>
                            </asp:BoundColumn>
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
</asp:Content>
<asp:Content ContentPlaceHolderID="CPH3" runat="server">
    <script src="/bootstrap/bootstrap-chosen/chosen.jquery.js" type="text/javascript"></script>
</asp:Content>
