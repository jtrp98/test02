<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="behavior-log.aspx.cs" Inherits="FingerprintPayment.behavior_log" EnableViewState="true" %>

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

            $('input[id*=txtSearch]').val(getUrlParameter("txtSearch"));
            $('input[id*=txtdDate]').val(getUrlParameter("txtdDate"));

            $(document).ready(function () {
                $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy' });                
            });

            $('#ctl00_MainContent_ddlsublevel').change(function () {
                funtionListSubLV2("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
                availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
            });

            $('select[id*=ddlSubLV2]').change(function () {
                availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
            });


            $('input[id*=btnSearch]').click(function () {
                var param1var = $('input[id*=txtSearch]').val();
                var param2var = $('input[id*=txtdDate]').val();                
                window.location.href = "behavior-log.aspx?txtSearch=" + param1var + "&txtdDate=" + param2var;
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
                source: function (request, response) {
                    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("input[id*=txtSearch]").val(ui.item.label);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });
        });
        function changeFinger() {
            $.ajax("/Api/change/?userid=" + $("#ctl00_MainContent_hdfsid").val() + "&type=0", function (Result) {
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
    <%--  <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTUser" ServicePath="AutoCompleteService.asmx" EnableCaching="true"
        FirstRowSelected="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>--%>
    <div class="full-card box-content userlist-container">
        <asp:HiddenField ID="hdfsid" runat="server" />
                
        <div class="form-group row" id="row-name">
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
        <div class="form-group row" id="row-name">
            <div class="col-md-6 col-sm-12 col-name">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701010") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <div class="input-group">
                    <asp:TextBox ID="txtdDate" runat="server" class="form-control datepicker" />
                    <div class="input-group-addon">
                        <i class="glyphicon glyphicon-calendar"></i>
                    </div>
                </div>
                </div>
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
                            <asp:BoundColumn DataField="UserAddId" HeaderStyle-Width="23%" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132037") %>">
                                <HeaderStyle Width="23%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DayAdd" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderStyle-Width="22%"
                                HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %>">
                                <HeaderStyle Width="22%" />
                            </asp:BoundColumn>                            
                            <asp:BoundColumn DataField="BehaviorName" HeaderStyle-Width="22%" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701046") %>">
                                <HeaderStyle Width="22%"></HeaderStyle>
                            </asp:BoundColumn>
                           <asp:TemplateColumn HeaderStyle-CssClass="header-tb-color" HeaderStyle-Width="10%" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132038") %>">
                                <ItemTemplate>
                                     <%# Eval("type") + " " + Eval("score")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="studentName" HeaderStyle-Width="23%" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301008") %>">
                                <HeaderStyle Width="23%"></HeaderStyle>
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
