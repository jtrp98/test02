<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="eventsettings.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.eventsettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $("#txtHolidayStart").datepicker();
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $("#txtHolidayEnd").datepicker();
            });
        });
    </script>
    <style>
        .hidden {
            display: none;
        }

        .unhidden {
            display: block;
        }
    </style>

    <script type="text/javascript">
        function unhide(clickedButton, divID, divID2) {
            var item = document.getElementById(divID);
            var item2 = document.getElementById(divID2);
            if (item) {
                if (item.className == 'hidden') {
                    item.className = 'unhidden';
                    window.scrollTo(0, 0);
                } else {
                    item.className = 'hidden';
                    window.scrollTo(0, 0);
                }
            }
            if (item2) {
                if (item2.className == 'hidden') {
                    item2.className = 'unhidden';
                    window.scrollTo(0, 0);
                } else {
                    item2.className = 'hidden';
                    window.scrollTo(0, 0);
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="about" class="hidden">
        <div class="detail-card box-content holiday-container" id="table" runat="server">
            <div class="row">
                <div class="col-lg-offset-2 col-lg-3 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3 adjust-col-padding">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132558") %>
                    </label>
                </div>
                <div class="col-lg-7 col-md-8 col-xs-8">
                    <label>
                        <input type="radio" name="optradio" id="rd1" value="0" runat="server" checked>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132557") %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                    <label>
                        <input type="radio" id="rd2" value="1" runat="server" name="optradio">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132559") %>
                    </label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-3">
                </div>
                <div class="col-xs-9">
                    <div id="divmain">
                    </div>
                    <div id="myTabContent" class="tab-content">
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-offset-2 col-lg-3 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132560") %>
                    </label>
                </div>
                <div class="col-lg-7 col-md-8 col-xs-8">
                    <input class="form-control" type="text" id="txtHoliday"
                        runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-offset-2 col-lg-3 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132561") %>
                    </label>
                </div>
                <div class="col-lg-7 col-md-8 col-xs-8 date-container">
                    <input class="form-control datepicker" data-date-format="dd/mm/yyyy" aria-describedby="basic-addon2"
                        id="txtHolidayStart" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-offset-2 col-lg-3 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132562") %>
                    </label>
                </div>
                <div class="col-lg-7 col-md-8 col-xs-8 date-container">
                    <input class="form-control datepicker" data-date-format="dd/mm/yyyy" aria-describedby="basic-addon2"
                        id="txtHolidayEnd" runat="server" />
                </div>
            </div>
            <div class="row button-section">
                <div class="col-xs-12" style="text-align: center;">
                    <asp:Button CssClass="btn btn-success font20 global-btn" ID="btnSave" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                    <input type="button" class="btn btn-warning" onclick="unhide(this, 'about', 'about2') " value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>">
                    <asp:TextBox runat="server" ID="txtListtime" Style="display: none;" />
                </div>
            </div>
        </div>
    </div>

    <div id="about2" class="unhidden">
        <div id="Div1" class="full-card box-content row--space holiday-table-container">
            <ul id="myTab" class="nav nav-tabs nav-tabs-title">
                <li class="active"><a href="#home" style="color: black;" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132566") %> </a></li>
                <li><a href="#ios" style="color: black;" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132567") %></a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane fade" id="ios" style="background: white;">
                    <asp:DataGrid ID="dgd2" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <Columns>
                            <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="name" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132560") %>">
                                <HeaderStyle Width="25%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="start"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132561") %>">
                                <HeaderStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="end" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132562") %>">
                                <HeaderStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderStyle-CssClass="header-tb-color" HeaderStyle-Width="35%">
                                <ItemTemplate>
                                    <div class=" <%# (bool)Eval("delete") ?"":"hidden" %>">
                                        <input type="button" a href="/Modules/TimeAttendance/holidaysettings-detail.aspx?id=<%# Eval("nHoliday") %>" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132568") %>" class="btn btn-success" data-toggle="modal" data-target="#modalpopupdata"> 
                                        </input>
                                        <asp:LinkButton CssClass="btn btn-warning" ID="setBtn2"
                                            runat="server" CommandName="Manage" CommandArgument='<%# Eval("nHoliday") %>'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></asp:LinkButton>

                                        <asp:Button CssClass="btn btn-danger" ID="btnDel"
                                            runat="server" CommandName="Del" CommandArgument='<%# Eval("nHoliday") %>'
                                            OnClientClick="return false;" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>" />
                                    </div>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <input type="button" class="btn btn-info" onclick="unhide(this, 'about', 'about2') " value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132563") %>">
                                </HeaderTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn Visible="false" DataField="nHoliday" HeaderText="nHoliday" />
                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="itemCell" />
                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="pagerCell" />
                        <SelectedItemStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                    </asp:DataGrid>
                </div>
                <div class="tab-pane fade in active" id="home" style="background: white;">
                    <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <Columns>
                            <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="name" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132560") %>">
                                <HeaderStyle Width="25%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="start"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132561") %>">
                                <HeaderStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="end" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132562") %>">
                                <HeaderStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderStyle-CssClass="header-tb-color" HeaderStyle-Width="35%">
                                <ItemTemplate>
                                    <%-- <asp:LinkButton CssClass="btn btn-warning width40-per glyphicon glyphicon-cog" ID="setBtn1"
                                runat="server" CommandName="Manage" CommandArgument='<%# Eval("nHoliday") %>'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303007") %></asp:LinkButton>--%>

                                    <div class=" <%# (bool)Eval("delete") ?"":"hidden" %>">
                                        <asp:Button CssClass="btn btn-danger " ID="btnDel"
                                            runat="server" CommandName="Del" CommandArgument='<%# Eval("nHoliday") %>' OnClientClick="return false;" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>" />
                                    </div>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <input type="button" class="btn btn-info" onclick="unhide(this, 'about', 'about2') " value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132563") %>">
                                </HeaderTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
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
    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h3 class="modal-title" id="myModalLabel">ตั่งค่าตารางเวลา</h3>
                </div>
                <div class="modal-body" style="height: 470px; overflow: scroll;">
                    <div id="modalSet">
                    </div>
                    <div id="modalsub" class="tab-content">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default font20 btnModalFooter" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    <button type="button" id="btnall" class="btn btn-success font20 btnModalFooter">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01897") %></button>
                    <button type="button" id="btnSaveH" class="btn btn-primary font20 btnModalFooter">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var dataUpdate;
        var _nhol = "";
        function ShowSubLevel(level) {
            if ($('table[id*=tdsublevel' + level + ']').css("display") == "none") {
                $('table[id*=tdsublevel' + level + ']').css("display", "");
                $('i[id=tdlevel' + level + ']').removeClass("glyphicon glyphicon-chevron-right");
                $('i[id=tdlevel' + level + ']').addClass("glyphicon glyphicon-chevron-down");
            } else {
                $('table[id*=tdsublevel' + level + ']').css("display", "none");
                $('i[id=tdlevel' + level + ']').removeClass("glyphicon glyphicon-chevron-down");
                $('i[id=tdlevel' + level + ']').addClass("glyphicon glyphicon-chevron-right");
            }
        }
        $(document).ready(function () {
            $('input[id*=btnDel]').click(function () {
                showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('name')); return false;
            });
            $('.datepicker').datepicker({ format: 'dd/mm/yyyy' });
            //            $(".datepicker").datepicker({ dateFormat: 'dd/mm/yy', isBuddhist: true, defaultDate: toDay }); 

            $('#btnall').click(function () {
                $('input[termsub=yes]').prop({ "checked": "checked" });
            });
            $('input[id*=rd]').click(function () {
                if ($('input[id*=rd]:checked').val() == "0") {
                    $('#divmain').html("");
                    $('#myTabContent').html("");
                    $('#modalSet').html("");
                    $('#modalsub').html("");
                }
                else {
                    $('input[id*=txtListtime]').val("");
                    tabtimetype('000', 'divmain', 'myTabContent');
                }
            });

            $('td[id*=tdlevel]').bind("DOMSubtreeModified", function (e, s) {
                alert($(this).attr('id'));
            });

            $("#btnSaveH").click(function () {

                ajaxdoPostback();
                $.ajax({
                    url: "/App_Logic/insertJSON.aspx?mode=updateHoliday",
                    type: "post",
                    data: dataUpdate,
                    success: function (resp) {
                        if (resp == "success") {
                            $("#myModal").hide();
                            alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132565") %>");
                            __doPostBack();
                            //                            window.location.reload(true);
                        } else {
                            alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132564") %>");
                        }
                        //  window.location.reload(true);
                    }
                });

            });
        });

        function modalHoliday(nHold, divname) {

            $("#" + divname).html('<div style="text-align:center"><img src="/images/logo tv.png" alt="logo" style="width:50%" /></div>');
            $.ajaxSetup({ cache: false });

            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=holidayset&nHol=" + nHold,
                cache: false,
                success: function (msg) {
                    console.log(msg);
                    var objjson = $.parseJSON(msg);
                    var strHtml = tabtimetype() + '<ul id="myTab" class="nav nav-tabs">';
                    var tabHTML = '';
                    var i = 1;
                    var checked = "";
                    $.each(objjson, function (index) {
                        checked = "";
                        if (index % 3 == 0) {
                            strHtml += ' <li ';

                            if (index == 0) {
                                strHtml += ' class="active"';
                                tabHTML += '<div id="myTabContent" class="tab-content">';
                                tabHTML += '<div class="tab-pane fade in active" id="tab' + i + '" style="background:white;">';
                            }
                            else {
                                tabHTML += '<div class="tab-pane fade" id="tab' + i + '" style="background:white;">';
                            }
                            strHtml += '><a href="#tab' + i + '" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132194") %> ' + i + '</a></li>';

                            tabHTML += '<table class="table table-striped" style="font-family:thaifont;"><tr><td style="width:70%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></td><td style="width:30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td></tr>';
                            tabHTML += '<tr><td colspan="2" style="background:#FF8A65;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132194") %> ' + i + '</td></tr>';
                            i += 1;
                        }
                        if (objjson[index].nAll == "1") {
                            checked = " checked";
                        }
                        tabHTML += '<tr><td>' + objjson[index].SubLevel + '</td><td><input nHol="' + nHold + '" type="checkbox" value="' + objjson[index].nTSubLevel + '" termsub="yes" id="txt' + index + '" style="" ' + checked + ' /></td></tr>';
                        if (index % 3 == 2) {
                            tabHTML += "</table></div>";
                        }
                    });
                    strHtml += '</ul>';
                    tabHTML += "</div>";
                    tabHTML += "<script type='text/javascript'>$('input[id*=txt]').click(function () {  });"
                    tabHTML += '<\/script>'
                    $("#" + divname).html(strHtml + tabHTML);
                }
            });
        }

        function tabtimetype(nHol, divmain, divsub) {
            _nhol = nHol;
            $('input[id*=txtListtime]').val("");
            $('#divmain').html("");
            $('#myTabContent').html("");
            $('#modalSet').html("");
            $('#modalsub').html("");
            var strHtml = "";
            $.ajaxSetup({ cache: false });
            $.ajax({
                type: "POST",
                url: "/App_Logic/dataGeneric.ashx?ID=&mode=tabtimetype",
                cache: false,
                contentType: 'application/json;',
                success: function (objjson, status) {
                    strHtml = '<ul id="myTab" class="nav nav-tabs nav-select-class">';
                    var tabHTML = "";
                    $.each(objjson, function (index) {
                        strHtml += ' <li ';
                        if (index == 0) {
                            strHtml += ' class="active"';
                        }
                        else {
                        }
                        strHtml += '><a class="select-class-link" href="#tab' + objjson[index].nTimeType + '" data-toggle="tab">' + objjson[index].sTimeType + '</a></li>';
                        getlevel(objjson[index].nTimeType, index, divsub)
                    })
                    strHtml += '</ul>';
                    $('#' + divmain).html(strHtml);
                }
            });
        }

        function getlevel(sid, _index, divsub) {
            var strHtml = "";
            $.ajax({
                type: "POST",
                url: "/App_Logic/dataGeneric.ashx?ID=" + sid + "&mode=listlevel",
                cache: false,
                contentType: 'application/json;',
                success: function (objlevel, status) {
                    var tabHTML = "";
                    var checked = "checked";
                    if (_index == 0) {
                        tabHTML += '<div class="tab-pane fade in active" id="tab' + sid + '" style="background:white;">';
                    }
                    else {
                        tabHTML += '<div class="tab-pane fade" id="tab' + sid + '" style="background:white;">';
                    }
                    tabHTML += '<table class="table table-striped table-sub-level table-select-class"><tr style="background:#FF8A65;"><td style="width:70%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132570") %></td><td style="width:15%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td></tr>';
                    $.each(objlevel, function (index) {
                        var strsublevel = getsublevel(objlevel[index].nTLevel);
                        if (strsublevel == undefined) {
                            strsublevel = "";
                        }
                        tabHTML += '<tr><td id="tdlevel' + objlevel[index].nTLevel + '" >'
                        tabHTML += '<i id="tdlevel' + objlevel[index].nTLevel + '" class="glyphicon glyphicon-chevron-right" onclick="ShowSubLevel(' + objlevel[index].nTLevel + ')" ></i> ' + objlevel[index].LevelName
                        tabHTML += '</td><td><input type="checkbox" '
                        tabHTML += 'value="' + objlevel[index].nTLevel + '" termsub="yes" class="" id="txtlv' + objlevel[index].nTLevel + '" '
                        tabHTML += ' ' + checked + ' onclick=Allsublevel(' + objlevel[index].nTLevel + ',$(this).is(":checked"));  />' + strsublevel
                        tabHTML += '</td>'
                        tabHTML += '</td></tr>';
                    })
                    tabHTML += "</table></div>";
                    $('#' + divsub).html($('#' + divsub).html() + tabHTML);
                }
            });
        }
        function getsublevel(sid) {
            var strHtml = "";
            $.ajax({
                type: "POST",
                url: "/App_Logic/dataGeneric.ashx?ID=" + sid + "&mode=listsublevel&nhol=" + _nhol,
                cache: false,
                contentType: 'application/json;',
                success: function (objlevel, status) {
                    var tabHTML = "";
                    var checked = false;
                    tabHTML += '<table class="table table-striped table-select-class" style="display:none;" id="tdsublevel' + sid + '">';
                    $.each(objlevel, function (index) {
                        if (objlevel[index].nAll == "1") {
                            checked = " checked";
                        }
                        else {
                            checked = "";
                            $('input[id*=txtlv' + sid + ']').attr("checked", false);
                        }
                        tabHTML += '<tr><td style="width:70%;">' + objlevel[index].SubLevel + '</td>'
                        tabHTML += '<td style="vertical-align: middle;"><input nHol="' + objlevel[index].nTSubLevel + '" type="checkbox" '
                        tabHTML += 'value="' + objlevel[index].nTSubLevel + '" termsub="yes" class="" id="txtsublv' + sid + '_' + objlevel[index].nTSubLevel + '" '
                        tabHTML += ' ' + checked + ' onclick=GetListtime($(this).val(),$(this).is(":checked"),' + sid + '); /></td>'
                        tabHTML += '</td></tr>';
                    })
                    tabHTML += "</table>";
                    $('#tdlevel' + sid).html($('#tdlevel' + sid).html() + tabHTML);
                }
            });
        }

        function Allsublevel(val, addtime) {
            var _replace = $('input[id*=txtListtime]').val();
            var _str = "";
            var _ch = true;
            $.each(_replace.split(','), function (e, s) {
                $('input[id*=txtsublv' + val + ']').each(function (e, subs) {
                    if (s != "" && $(subs).val() == s) {
                        _ch = false;
                    }
                });
                if (_ch && s != "") {
                    _str += s + ",";
                }
                _ch = true;
            });
            if (_str == NaN) _str = "";
            if (addtime) {
                $('input[id*=txtsublv' + val + ']').each(function (e, subs) {
                    _str += $(subs).val() + ",";
                });
            }
            $('input[id*=txtListtime]').val(_str);
            $('input[id*=txtsublv' + val + ']').prop("checked", addtime);
        }
        function GetListtime(val, addtime, lv) {
            if (addtime) {
                $('input[id*=txtListtime]').val($('input[id*=txtListtime]').val() + val + ",");
            }
            else {
                var _replace = $('input[id*=txtListtime]').val();
                var _str = "";
                $.each(_replace.split(','), function (e, s) {
                    if (s != "" && val != s) {
                        _str = _str + s + ",";
                    }
                    else {
                    }
                });
                $('input[id*=txtListtime]').val(_str);
            }
            var _ch = true;
            $('input[id*=txtsublv' + lv + ']').each(function (e, subs) {
                if ($(subs).is(":checked") == false) {
                    _ch = false;
                }
            });
            $('input[id*=txtlv' + lv + ']').prop("checked", _ch)
        }

        function ajaxdoPostback() {
            dataUpdate = "";
            var j = 0;
            var check;
            var nHol;
            $('input[termsub=yes]').each(function () {
                j++;
                check = "0";
                nHol = $(this).attr("nHol");
                if (j > 1) dataUpdate += "&";
                if ($(this).is(':checked')) check = "1";
                dataUpdate += "txt" + j + "=" + check + "&sublv" + j + "=" + $(this).val();
            });
            dataUpdate += "&nHol=" + _nhol;
            console.log(dataUpdate);
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
