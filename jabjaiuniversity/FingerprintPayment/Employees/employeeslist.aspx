<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="employesslist.aspx.cs" Inherits="FingerprintPayment.employesslist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('input[id*=btnDel]').click(function () {
                showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('name')); return false;
            });
            //$("input[id*=txtSearch]").keypress(function (e) {
            //    if (e.keyCode == 13) {
            //        $("input[id*=btnSearch]").click();
            //    }
            //});
            var availableValueEmployees = [];
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=teacher",
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].sName + ' ' + objjson[index].sLastname,
                            value: objjson[index].sEmp
                        };
                        availableValueEmployees[index] = newObject;
                    });
                }
            });

            $('#ctl00_MainContent_txtSearch').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                source: function (request, response) {
                    results = $.ui.autocomplete.filter(availableValueEmployees, request.term);
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("#ctl00_MainContent_txtSearch").val(ui.item.label);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });

            $('#ctl00_MainContent_txtSearch').val(getUrlParameter("name"));
            $("#type").val(getUrlParameter("type"));
            $("#btnsearch").click(function () {
                window.location.href = "employeeslist.aspx?name=" + $('#ctl00_MainContent_txtSearch').val() + "&type=" + $("#type").val();
            });
        });

        function changeFinger() {
            $.ajax("/Api/change/?userid=" + $("#ctl00_MainContent_hdfsid").val() + "&type=1", function (Result) {
            }).done(function (Result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111048") %> " + Result);
                $("#modalpopupdatamac .modal-footer").addClass("hidden");
            });
        }
        function ShowPopUP(userid, name) {
            $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>" +
                "เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
            $("#ctl00_MainContent_hdfsid").val(userid);
            $("#modalpopupdatamac .modal-footer").removeClass("hidden")
        }
    </script>
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
    <%--  <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTEmployees" ServicePath="AutoCompleteService.asmx"
        EnableCaching="true" FirstRowSelected="true" CompletionListCssClass="completionList"
        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>--%>
    <div class="full-card box-content employeeslist-container">
        <asp:HiddenField ID="hdfsid" runat="server" />
        <div class="row form-group">
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102162") %></label>
                </div>
                <div class="col-lg-8 col-md-7 col-sm-8">
                    <select class="form-control" style="width: 100%;" id="type">
                        <option value="" selected="selected"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %></option>
                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102166") %></option>
                    </select>
                </div>
            </div>
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                </div>
                <div class="col-lg-8 col-md-7 col-sm-8">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass='form-control' Style="width: 100%;"></asp:TextBox>
                </div>
            </div>

        </div>
        <div class="row form-group">
            <div class="col-sm-12 text-center">
                <input type="button" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" class="btn btn-primary global-btn" id="btnsearch" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table filter-table">
                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <Columns>
                            <asp:BoundColumn DataField="rowindex" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="sName" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="sLastName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderStyle-Width="15%" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %>" DataField="sIdentification"></asp:BoundColumn>
                            <asp:BoundColumn DataField="dBirth" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %>"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <%--<a href="employeesadd.aspx" class="btn btn-success custom-a"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></a>--%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>" class="btn btn-info custom-a" CommandName="Edit" />
                                    <button onclick="ShowPopUP(<%# Eval("sEmp") %>,'<%# Eval("sName").ToString() +" "+ Eval("sLastName").ToString() %>'); return false;"
                                        class="btn btn-permission <%# (bool)Eval("finger") ?"":"hidden" %>" data-toggle="modal" data-target="#modalpopupdatamac">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111044") %></button>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Button ID="btnDel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" class="btn btn-danger custom-a"
                                        OnClientClick="return false;" CommandName="Del" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataFormatString="sFinger" HeaderText="sFinger" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="sEmp" HeaderText="sEmp" Visible="False" />
                            <asp:BoundColumn DataField="finger" HeaderText="finger" Visible="False" />
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
    <div id="modalpopupdatamac" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true">
        <div class="modal-dialog maclist-modal" style="top: 50px;">
            <div class="modal-content">
                <div class="modal-header">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111044") %>                    <%--    <button type="button" id="modalpopupdata-close" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 style="font-size: 20px; text-align: center; font-weight: bold" class="modal-title"
                            id="modalpopupdata-header">Modal title</h4>--%>
                </div>
                <div class="modal-body" id="modalpopupdata-content">
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <label class="btn btn-primary" onclick="changeFinger();" style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111046") %></label>
                    <label class="btn btn-danger" ondblclick='$("#modalpopupdatamac").modal("hide");' onclick="" style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
