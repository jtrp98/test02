<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="plans-room.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.plans_room" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
        var dataUpdate;
        var _nhol = "";
        function ShowSubLevel(level) {
            if ($('div[id*=tdsublevel' + level + ']').css("display") == "none") {
                $('div[id*=tdsublevel' + level + ']').css("display", "");
                $('i[id=tdlevel' + level + ']').removeClass("glyphicon glyphicon-chevron-right");
                $('i[id=tdlevel' + level + ']').addClass("glyphicon glyphicon-chevron-down");
            } else {
                $('div[id*=tdsublevel' + level + ']').css("display", "none");
                $('i[id=tdlevel' + level + ']').removeClass("glyphicon glyphicon-chevron-down");
                $('i[id=tdlevel' + level + ']').addClass("glyphicon glyphicon-chevron-right");
            }
        }

        function ShowSubLevel2(level) {
            if ($('div[id*=tdsublevel_2_' + level + ']').html() == "") {
                //                $('div[id*=tdsublevel_2_' + level + ']').css("display", "");
                $('i[id=isublevel' + level + ']').removeClass("glyphicon glyphicon-chevron-right");
                $('i[id=isublevel' + level + ']').addClass("glyphicon glyphicon-chevron-down");
                getsublevel2(level);
            } else {
                //                $('div[id*=tdsublevel_2_' + level + ']').css("display", "none");
                $('i[id=isublevel' + level + ']').removeClass("glyphicon glyphicon-chevron-down");
                $('i[id=isublevel' + level + ']').addClass("glyphicon glyphicon-chevron-right");
                $('#tdsublevel_2_' + level).html("");
            }
        }

        $(document).ready(function () {
            tabtimetype('000', 'divmain', 'myTabContent');
        });

        function tabtimetype(nHol, divmain, divsub) {
            var request = $.ajax({
                type: "POST",
                url: "/App_Logic/dataGeneric.ashx?ID=00&mode=listplansroom&idterm=" + getUrlParameter("idterm"),
                cache: false,
                contentType: 'application/json;',
                success: function (objlevel, status) {
                    objlevel2 = objlevel;
                }
            });
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
                    strHtml = '<ul id="myTab" class="nav nav-tabs tab-titles">';
                    var tabHTML = "";
                    $.each(objjson, function (index) {
                        strHtml += ' <li ';
                        if (index == 0) {
                            strHtml += ' class="active"';
                        }
                        else {
                        }
                        strHtml += '><a class="tabs-link" href="#tab' + objjson[index].nTimeType + '" data-toggle="tab">' + objjson[index].sTimeType + '</a></li>';
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
                    tabHTML += '<table class="table table-striped plans-room-table"><tr style="background:#286090; color: white;"><td class="tableHeader"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132570") %></td></tr>';
                    $.each(objlevel, function (index) {
                        var strsublevel = getsublevel(objlevel[index].nTLevel);
                        if (strsublevel == undefined) {
                            strsublevel = "";
                        }
                        tabHTML += '<tr><td id="tdlevel' + objlevel[index].nTLevel + '" >'
                        tabHTML += '<span id="tdlevel' + objlevel[index].nTLevel + '" onclick="ShowSubLevel(' + objlevel[index].nTLevel + ')" ><span class="glyphicon glyphicon-chevron-right"></span> ' + objlevel[index].LevelName + '</span><br/><br/>'
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
                    tabHTML += '<div style="display:none;" id="tdsublevel' + sid + '">';
                    $.each(objlevel, function (index) {
                        tabHTML += '<div class="col-lg-12 col-md-12 col-sm-12" id="divsublevel2_' + objlevel[index].nTSubLevel + '">';
                        tabHTML += '<div class="col-lg-12 col-md-12 col-sm-12" style="padding-bottom: 18px;"><span id="isublevel' + objlevel[index].nTSubLevel
                        tabHTML += '" onclick="ShowSubLevel2(' + objlevel[index].nTSubLevel + ')" ><span class="glyphicon glyphicon-chevron-right"></span> '
                        tabHTML += objlevel[index].SubLevel + '</span></div>'
                        tabHTML += '<div id="tdsublevel_2_' + objlevel[index].nTSubLevel + '">';
                        tabHTML += "</div>";
                        tabHTML += '</div>';
                    })
                    tabHTML += "</div>";
                    $('#tdlevel' + sid).html($('#tdlevel' + sid).html() + tabHTML);
                }
            });
        }
        function getsublevel2(sid) {
            var strHtml = "";
            var tabHTML = "";
            var checked = false;
            $.each(objlevel2, function (index) {
                if (objlevel2[index].nTSubLevel == sid) {
                    tabHTML += '<div class="row"><div class="col-lg-1 col-md-1 col-sm-1"></div>';
                    tabHTML += '<div class="col-lg-4 col-md-4 col-sm-4">' + objlevel2[index].SubLevel + ' / ' + objlevel2[index].nTSubLevel2 + '</div>'
                    tabHTML += '<div class="col-lg-7 col-md-7 col-sm-7"><a href="plans-schedule.aspx?id=' + objlevel2[index].nTermSubLevel2 + "&idterm=" + getUrlParameter("idterm") + '" class="btn btn-primary btn-link btn-plans-room"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202003") %></a>'
                    if (objlevel2[index].NumberSchedule != "0") {
                        tabHTML += '&nbsp;<a href="plans-scheduledetail.aspx?id=' + objlevel2[index].nTermSubLevel2 + "&idterm=" + getUrlParameter("idterm") + '" class="btn btn-info btn-link btn-plans-room"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202005") %></a> '
                    }
                    tabHTML += "</div> </div>";
                }
            })
            $('#tdsublevel_2_' + sid).html(tabHTML);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card plans-room-container">
        <div class="row">
            <div class="col-xs-12">
                <div id="divmain">
                </div>
                <div id="myTabContent" class="tab-content">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
