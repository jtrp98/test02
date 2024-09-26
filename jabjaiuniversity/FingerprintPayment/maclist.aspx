<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="maclist.aspx.cs" Inherits="FingerprintPayment.maclist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function getAverment() {
            $.ajax({
                type: "GET",
                url: "/api/Addmobile",
                cache: false,
                success: function (response) {
                }
            }).done(function (objjson) {
                $.each(objjson, function (index) {
                    if (objjson[index].sTime == "00:00") {
                        clearInterval(refreshIntervalId);
                        $("#modalpopupdata-content").html('<span class="detail-box"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131088") %> ' + objjson[index].sAverment + "<br><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131092") %></span>");
                    }
                    else if (1 = 1) {
                    }
                    else {
                        $("#modalpopupdata-content").html('<span class="detail-box"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131088") %> ' + objjson[index].sAverment + "<br><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131089") %> " + objjson[index].sTime + "</span>");
                    }
                });

                //var xmlDoc = $.parseXML(objjson);
                //var $xml = $(xmlDoc);
                //var $sAverment = $xml.find("sAverment");
                //var $sTime = $xml.find("sTime");
                //$("#modalpopupdata-content").html(sAverment + " " + sTime);
            });
        }
        var refreshIntervalId;
        $(document).ready(function () {
            refreshIntervalId = setInterval(function () { getAverment(); }, 500);
            //getAverment();
            $('input[id*=btnDel]').click(function () {
                showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('name')); return false;
            });
            $('input[id*=type]').click(function () {
                if ($('input[id*=type]:checked').val() == "0") {
                    $(".classname").addClass("hidden");
                    $(".comname").removeClass("hidden");
                } else {
                    $(".comname").addClass("hidden");
                    $(".classname").removeClass("hidden");
                    $('input[id*=txtsClass]').val($('select[id*=ddlClass] option:selected').html());
                }
            });
            if ($('input[id*=type]:checked').val() == "0") {
                $(".classname").addClass("hidden");
                $(".comname").removeClass("hidden");
            } else {
                $(".comname").addClass("hidden");
                $(".classname").removeClass("hidden");
            }
            $('select[id*=ddlClass]').change(function () {
                $('input[id*=txtsClass]').val($('select[id*=ddlClass] option:selected').html());
            })
            $("input[id*=txtsMac1]").keyup(function (e) {
                if (e.keyCode == 8) return;
                if ($("input[id*=txtsMac1]").val().length == 2) {
                    $("input[id*=txtsMac2]").focus();
                    $("input[id*=txtsMac2]").focus();
                }
            });
            $("input[id*=txtsMac2]").keyup(function (e) {
                if (e.keyCode == 8) return;
                if ($("input[id*=txtsMac2]").val().length == 2) {
                    $("input[id*=txtsMac3]").focus();
                    $("input[id*=txtsMac3]").focus();
                }
            });
            $("input[id*=txtsMac3]").keyup(function (e) {
                if (e.keyCode == 8) return;
                if ($("input[id*=txtsMac3]").val().length == 2) {
                    $("input[id*=txtsMac4]").focus();
                    $("input[id*=txtsMac4]").focus();
                }
            });
            $("input[id*=txtsMac4]").keyup(function (e) {
                if (e.keyCode == 8) return;
                if ($("input[id*=txtsMac4]").val().length == 2) {
                    $("input[id*=txtsMac5]").focus();
                    $("input[id*=txtsMac5]").focus();
                }
            });
            $("input[id*=txtsMac5]").keyup(function (e) {
                if (e.keyCode == 8) return;
                if ($("input[id*=txtsMac5]").val().length == 2) {
                    $("input[id*=txtsMac6]").focus();
                    $("input[id*=txtsMac6]").focus();
                }
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
    <div class="full-card  box-content maclist-container">
        <%--        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" CssClass="cool-table cool-table-8"
            GridLines="None" Width="100%">
            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
            <Columns>
                <asp:BoundColumn DataField="sComputerName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131093") %>" ItemStyle-Width="30%"></asp:BoundColumn>
                <asp:BoundColumn DataField="sMac" HeaderText="Mac Address" ItemStyle-Width="30%"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <%-- <asp:LinkButton ID="lnkEdit" CssClass="btn btn-primary" runat="server" CommandArgument='<%# Eval("nComputerID") %>'
                            CommandName="Edit" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>" />--%>
                        <a href="#" class="btn btn-primary btn-maclist btn-phone-data" data-toggle="modal"
                            data-target="#modalpopupdatamac"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131090") %></a>

                        <asp:Button ID="btnDel" runat="server" CommandArgument='<%# Eval("nComputerID") %>'
                            OnClientClick="return false;"
                            CommandName="Del" CssClass="btn btn-danger btn-del-maclist" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>" />
                    </ItemTemplate>
                    <HeaderTemplate>
                        <a href="#" class="btn btn-success btn-maclist" data-toggle="modal"
                            data-target="#modalpopupdatamac"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131091") %></a>
                    </HeaderTemplate>
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

        <%--       </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="timecode" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Timer ID="timecode" runat="server" Interval="100"></asp:Timer>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
    <div id="modalpopupdatamac" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true">
        <div class="modal-dialog maclist-modal" style="top: 50px;">
            <div class="modal-content">
                <div class="modal-header">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131094") %>                    <%--    <button type="button" id="modalpopupdata-close" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 style="font-size: 20px; text-align: center; font-weight: bold" class="modal-title"
                            id="modalpopupdata-header">Modal title</h4>--%>
                </div>
                <div class="modal-body" id="modalpopupdata-content">
                    <img src="/images/loader-128x/Preloader_2.gif" />
                </div>
                <%--  <div class="modal-footer" style="display: block; text-align: center;">
                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
