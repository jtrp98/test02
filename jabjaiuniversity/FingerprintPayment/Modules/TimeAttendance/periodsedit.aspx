<%@ Page Title="" Language="C#" MasterPageFile="~/mppopup.Master" AutoEventWireup="true"
    CodeBehind="periodsedit.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.periodsedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        @media (max-width: 999px) {
            body .modal-content {
                width: 600px;
                margin-left: 90px;
            }
        }

        @media (min-width: 1000px) and (max-width: 1199px) {
            body .modal-content {
                width: 800px;
                margin-left: 50px;
            }
        }
    </style>

    <script>
        function modal_plane(msg) {
            showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg, function () {
                $("input[id*=txtPlaneID]").focus();
            });
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="full-card content-body periodadd-container">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <table class="periodsadd-table"  style="padding-left: 10px; width: 100%">
                            <tr>
                                <td style="width: 130px;"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132613") %></label>
                                </td>
                                <td>
                                    <input class="form-control" type="text" clientidmode="Static"
                                        id="txtPlaneID" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132614") %></label>
                                </td>
                                <td class="time-section">
                                    <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                        <input type="text" class="form-control mon" id="txtdStart" runat="server" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                                    </div>
                                    <div>
                                        -
                                    </div>
                                    <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                        <input type="text" class="form-control mon" id="txtdEnd" runat="server" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132615") %></label>
                                </td>
                                <td class="time-section">
                                    <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                        <input type="text" class="form-control mon" id="txtdTimeStart_IN" runat="server" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                                    </div>
                                    <div>
                                        -
                                    </div>
                                    <div class="input-group clockpicker" data-placement="left"
                                        data-align="top" data-autoclose="true">
                                        <input type="text" class="form-control mon" id="txtdTimeStart_OUT" runat="server" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132616") %></label>
                                </td>
                                <td class="time-section">
                                    <div class="input-group clockpicker" data-placement="left"
                                        data-align="top" data-autoclose="true">
                                        <input type="text" class="form-control mon" id="txtdTimeEnd_IN" runat="server" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                                    </div>
                                    <div>
                                        -
                                    </div>
                                    <div class="input-group clockpicker" data-placement="left"
                                        data-align="top" data-autoclose="true">
                                        <input type="text" class="form-control mon" id="txtdTimeEnd_OUT" runat="server" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <div id="divmain" style="margin-top: 10px;">
                                    </div>
                                    <div id="myTabContent" class="tab-content">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 text-center periodsadd-button-segment">
                            <asp:Button CssClass="btn btn-primary glyphicon glyphicon-plus global-btn" ID="btnSave" runat="server"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" />
                            <asp:Button CssClass="btn btn-danger glyphicon glyphicon-remove global-btn" ID="btnCancle"
                                runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
                            <asp:HiddenField ID="hdfid" runat="server" />
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:TextBox runat="server" ID="txtListtime" Style="display: none;" />
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
            $('.clockpicker').clockpicker();
            tabtimetype('00', 'divmain', 'myTabContent');
        });

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
                    tabHTML += '<table class="table table-striped" style="margin-bottom:0px;"><tr class="table-tab"><th style="width:70%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132570") %></th><th style="width:15%"></th></tr></table>';
                    tabHTML += '<div style="overflow-y: scroll; height: 300px;"><table class="table table-striped table-select-class">';
                    $.each(objlevel, function (index) {
                        var strsublevel = getsublevel(objlevel[index].nTLevel);
                        if (strsublevel == undefined) {
                            strsublevel = "";
                        }
                        tabHTML += '<tr class="level-name"><td id="tdlevel' + objlevel[index].nTLevel + '" >'
                        tabHTML += '<div id="tdlevel' + objlevel[index].nTLevel + '" onclick="ShowSubLevel(' + objlevel[index].nTLevel + ')" ><span class="glyphicon glyphicon-chevron-right"></span> ' + objlevel[index].LevelName + '</div><br/><br/>'
                        tabHTML += '</td><td><div class="checkbox-container"><input type="checkbox" '
                        tabHTML += 'value="' + objlevel[index].nTLevel + '" termsub="yes" class="form-control termsub" id="txtlv' + objlevel[index].nTLevel + '" '
                        tabHTML += ' ' + checked + ' onclick=Allsublevel(' + objlevel[index].nTLevel + ',$(this).is(":checked"));  />' + strsublevel
                        tabHTML += '</div></td>'
                        tabHTML += '</td></tr>';
                    })
                    tabHTML += "</table></div></div>";
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
                    tabHTML += '<table class="table table-striped" style="display:none;" id="tdsublevel' + sid + '">';
                    $.each(objlevel, function (index) {
                        if (objlevel[index].nHoliday != null || objlevel[index].nAll == "1") {
                            checked = " checked";
                        }
                        else {
                            checked = "";
                            $('input[id*=txtlv' + sid + ']').attr("checked", false);
                        }
                        tabHTML += '<tr><td style="width:70%">' + objlevel[index].SubLevel + '</td>'
                        tabHTML += '<td><input nHol="' + objlevel[index].nTSubLevel + '" type="checkbox" '
                        tabHTML += 'value="' + objlevel[index].nTSubLevel + '" termsub="yes" class="form-control termsub" id="txtsublv' + sid + '_' + objlevel[index].nTSubLevel + '" '
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
            $('input[id*=txtsublv' + val + ']').attr("checked", addtime);
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
            $('input[id*=txtlv' + lv + ']').attr("checked", _ch)
        }

        function getUrlParameter(sParam) {
            var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

            for (i = 0; i < sURLVariables.length; i++) {
                sParameterName = sURLVariables[i].split('=');

                if (sParameterName[0] === sParam) {
                    return sParameterName[1] === undefined ? true : sParameterName[1];
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
