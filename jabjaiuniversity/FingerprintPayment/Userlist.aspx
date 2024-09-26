<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="Userlist.aspx.cs" Inherits="FingerprintPayment.User_list" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
        label {
            font-weight: normal;
            font-size: 26px;
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .centerText {
            text-align: center;
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }
    </style>
    <script type="text/javascript" language="javascript">

        var availableValueplane = [];
        $(document).ready(function () {
            $('input[id*=btnDel]').click(function () {
                showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('name')); return false;
            });
            $('#ctl00_MainContent_ddlsublevel').val(getUrlParameter("idlv"));

            funtionListSubLV2("ctl00_MainContent_ddlsublevel", "ddlSubLV2", getUrlParameter("idlv2"));
            availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");

            $('input[id*=txtSearch]').val(getUrlParameter("sname"));

            $('#ctl00_MainContent_ddlsublevel').change(function () {
                funtionListSubLV2("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
                availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
            });

            $('select[id*=ddlSubLV2]').change(function () {
                availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
            });


            $('input[id*=btnSearch]').click(function () {
                var param1var = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                var param2var = $('select[id*=ddlSubLV2] option:selected').val();
                var param3var = $('input[id*=txtSearch]').val();
                window.location.href = "Userlist.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&sname=" + param3var;
            });

            $('input[id*=txtSearch]').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                maxLength: 10,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                //source: function (request, response) {
                //    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
                //    response(results.slice(0, 10));
                //},
                source: lightwell,
                select: function (event, ui) {
                    event.preventDefault();
                    $("input[id*=txtSearch]").val(ui.item.label);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });
        });

        function lightwell(request, response) {
            function hasMatch(s) {
                return s.toLowerCase().indexOf(request.term.toLowerCase()) !== -1;
            }
            var i, l, obj, matches = [];

            if (request.term === "") {
                response([]);
                return;
            }

            for (i = 0, l = availableValueplane.length; i < l; i++) {
                obj = availableValueplane[i];
                if (hasMatch(obj.label) || hasMatch(obj.code)) {
                    matches.push(obj);
                }
            }
            response(matches.slice(0, 10));
        }

        function changeFinger() {
            $.ajax("/App_Logic/deleteDataJSON.ashx?mode=delfinger&userid=" + $("#ctl00_MainContent_hdfsid").val() + "&type=0", function (Result) {
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

        function ShowPopUPRegister(userid, name) {
            $.get("/App_Logic/dataJSon.ashx?mode=getpassword&userid=" + userid + "&type=0", "", function (result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121045") %> " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121046") %> <br/>" +
                    " <h1> " + result[0].password);
                $("#modalpopupdatamac .modal-footer").addClass("hidden")
            })
        }
    </script>
    <div class="full-card box-content userlist-container">
        <asp:HiddenField ID="hdfsid" runat="server" />
        <div class="form-group row student">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlsublevel" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <select id="ddlSubLV2" class="form-control">
                    </select>
                </div>
            </div>
        </div>
        <div class="row" id="row-name">
            <div class="col-md-6 col-sm-12 col-name">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-button">
                <asp:Literal ID="ltrMenu" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 button-section">
                <input type="button" id="btnSearch" class='btn btn-info search-btn' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
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
                            <asp:BoundColumn DataField="rowindex" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="sName" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="sLastName" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" DataField="sIdentification">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="dBirth" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %>"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <a href="Useradd.aspx" class="btn btn-success" style="min-width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></a>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" Style="min-width: 100px;" CssClass="btn btn-info"
                                        Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>" CommandName="Edit" />
                                    <button onclick="ShowPopUP(<%# Eval("sID") %>,'<%# Eval("sName").ToString() +" "+ Eval("sLastName").ToString() %>'); return false;"
                                        class="btn btn-permission <%# (bool)Eval("finger") ?"":"hidden" %>" data-toggle="modal" data-target="#modalpopupdatamac">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111044") %></button>

                                    <button onclick="ShowPopUPRegister(<%# Eval("sID") %>,'<%# Eval("sName").ToString() +" "+ Eval("sLastName").ToString() %>'); return false;"
                                        class="btn btn-permission <%# (bool)Eval("finger") ?"hidden":"" %>" data-toggle="modal" data-target="#modalpopupdatamac">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121047") %></button>

                                </ItemTemplate>
                                <HeaderStyle Width="100px"></HeaderStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Button ID="btnDel" runat="server" CssClass="btn btn-danger" Style="min-width: 100px;"
                                        OnClientClick="return false;" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" CommandName="Del" />
                                </ItemTemplate>
                                <HeaderStyle Width="100px"></HeaderStyle>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="sID" HeaderText="sID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="finger" HeaderText="finger" Visible="false"></asp:BoundColumn>
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
