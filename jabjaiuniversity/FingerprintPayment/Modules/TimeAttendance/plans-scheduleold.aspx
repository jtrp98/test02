<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="plans-scheduleold.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.plans_scheduleold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="/bootstrap/bootstrap-chosen/chosen.jquery.js" type="text/javascript"></script>
    <link href="/bootstrap/bootstrap-chosen/bootstrap-chosen.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        var availableValueT = [""];
        var availableTagsT = [""];
        var addteacher = [""];
        var availableTagsplane = [""];
        var timetableid = "";
        var _listperiod = [];
        var para1var = getUrlParameter("id");

        function checkData() {
            var _ch = true;
            $.each(availableTagsplane, function (indexperiod) {
                var nperiod = _listperiod[_indexperiod].value;
                var nday = _indexday;

                if (availableTagsplane[indexperiod].nperiod == nperiod && availableTagsplane[indexperiod].nday == nday) {
                    _ch = false;
                }
            });
            return _ch;
        }

        var _listday = ["<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202007") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202008") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202009") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202010") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202011") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202012") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202013") %>"];
        function ListTimeTable() {
            var _html = "";
            _html += "<table class='table table-condensed table-bordered'>" +
                '<tr class="headerCell" style="font-style: normal; font-weight: bold; text-decoration: none; background-color: rgb(51, 122, 183); color: white;">' +
                "<td style='vertical-align: text-bottom; width:12%;'></td>"
            $.each(_listday, function (indexday0) {
                _html += "<td style='vertical-align: text-bottom; width:10%;'>" + _listday[indexday0] + "</td>"
            });
            _html += "</tr>";

            $.each(_listperiod, function (indexperiod) {
                _html += "<tr><td>" + _listperiod[indexperiod].label + "</td>"
                $.each(_listday, function (indexday) {
                    var tmp = "";
                    $.each(availableTagsplane, function (indexdata) {
                        if (_listperiod[indexperiod].value == availableTagsplane[indexdata].nperiod && availableTagsplane[indexdata].nday == indexday) {
                            tmp = "<td>" + availableTagsplane[indexdata].plane
                            tmp += '<br/><div class="schedule-operator"><span class="glyphicon glyphicon-edit" aria-hidden="true" data-toggle="modal" data-target="#myModal" onclick="editdata(' + indexday + ',' + indexperiod + ',' + indexdata + ')"></span>'
                            tmp += '<span class="glyphicon glyphicon-remove" aria-hidden="true" onclick="deldata(' + indexdata + ')"></span></div>'
                            tmp += "</td>";
                        }
                    });
                    if (tmp == "") {
                        tmp = '<td data-toggle="modal" data-target="#myModal" onclick="adddata(' + indexday + ',' + indexperiod + ')" ></td>';
                    }
                    _html += tmp;
                });
                _html += "</tr>";
            });

            _html += "</table>"
            $("#timetable").html(_html)
        }

        var _indexday;
        var _indexperiod;
        function adddata(indexday, indexperiod) {
            $("#myModalLabel").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %> " + _listday[indexday] + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206444") %> " + _listperiod[indexperiod].label);
            _indexday = indexday;
            _indexperiod = indexperiod;
        }
        var indexedit = 0;

        function editdata(indexday, indexperiod, indexdata) {
            $("#myModalLabel").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %> " + _listday[indexday] + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206444") %> " + availableTagsplane[indexdata].speriod);
            $("#selectplane_chosen .chosen-single span").html(availableTagsplane[indexdata].plane);
            $("#selectplane_chosen .chosen-single p").html(availableTagsplane[indexdata].nplane);
            $("#selectroom_chosen .chosen-single p").html(availableTagsplane[indexdata].roomsid);
            $("#selectroom_chosen .chosen-single span").html(availableTagsplane[indexdata].rooms);
            $("#selectteacher_chosen .chosen-single span").html(availableTagsplane[indexdata].teacher);
            $("#selectteacher_chosen .chosen-single p").html(availableTagsplane[indexdata].teacherid);
            //$("select[id*=number]").val(availableTagsplane[indexdata].nday);
            //$("select[id*=ddlsPeriods]").val(availableTagsplane[indexdata].nperiod);
            //$("#teacherid").val(availableTagsplane[indexdata].teacherid);
            //$("select[id*=number]").prop("disabled", true);
            //$("select[id*=ddlsPeriods]").prop("disabled", true);
            $("input[id=add]").val("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>");
            _indexperiod = indexperiod;
            _indexday = indexday;
            indexedit = indexdata;
            //GetValuesPeriods();
        }

        function deldata(indexdata) {
            availableTagsplane[indexdata].plane = "";
            availableTagsplane[indexdata].nplane = "";
            availableTagsplane[indexdata].teacherid = "";
            availableTagsplane[indexdata].teacher = "";
            availableTagsplane[indexdata].roomsid = "";
            availableTagsplane[indexdata].rooms = "";
            availableTagsplane[indexdata].nday = "";
            availableTagsplane[indexdata].sday = "";
            availableTagsplane[indexdata].speriod = "";
            availableTagsplane[indexdata].nperiod = "";
            ListTimeTable();
            Clear();
        }

        function Clear() {
            $("#selectplane_chosen .chosen-single p").html("");
            $("#selectplane_chosen .chosen-single span").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202018") %>");
            $("#selectteacher_chosen .chosen-single p").html("");
            $("#selectteacher_chosen .chosen-single span").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202020") %>");
            $("#selectroom_chosen .chosen-single p").html("");
            $("#selectroom_chosen .chosen-single span").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101281") %>");
            $("#teacher").val("");
            $("#teacherid").val("");
            $("input[id=add]").val("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>");
            $("select[id*=ddlsPeriods]").prop("disabled", false);
            $("select[id*=number]").prop("disabled", false);
        }

        $(document).ready(function () {

            var requestteacher = $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=teacher",
                success: function (msg) {
                    var i = 0;
                    var objjson = $.parseJSON(msg);
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].sName + ' ' + objjson[index].sLastname,
                            value: objjson[index].sEmp
                        };

                        addteacher[index] = newObject;

                        $('#selectteacher').append($('<option>', {
                            value: objjson[index].sEmp,
                            text: objjson[index].sName + ' ' + objjson[index].sLastname
                        }));

                        $('#selectteacherclass').append($('<option>', {
                            value: objjson[index].sEmp,
                            text: objjson[index].sName + ' ' + objjson[index].sLastname
                        }));

                    });
                }
            });

            requestteacher.done(function (msg) {
                $('#selectteacherclass').chosen();
                $('#selectteacher').chosen();
                $('.chosen-select-deselect').chosen({ allow_single_deselect: true });
                $("#selectteacherclass_chosen .chosen-single span").html($("#ctl00_MainContent_txtaddteacher").val());
                $("#selectteacherclass_chosen .chosen-single p").html($('#ctl00_MainContent_txtaddteacherid').val());
            });

            var requestplane = $.ajax({
                url: "/App_Logic/dataGeneric.ashx?id=" + para1var + "&mode=listplane",
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        $('#selectplane').append($('<option>', {
                            value: objjson[index].value,
                            text: objjson[index].value + ' ' + objjson[index].name
                        }));
                    });
                }
            });

            requesTPlanes.done(function (msg) {
                $('#selectplane').chosen();
                $('.chosen-select-deselect').chosen({ allow_single_deselect: true });
            });

            var requestplane = $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=itemclass",
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        $('#selectroom').append($('<option>', {
                            value: objjson[index].sClassID,
                            text: objjson[index].sClass
                        }));
                    });
                }
            });

            requesTPlanes.done(function (msg) {
                $('#selectroom').chosen();
                $('.chosen-select-deselect').chosen({ allow_single_deselect: true });
            });

            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listvaluesperiod&id=" + getUrlParameter("id"),
                success: function (obj) {
                    $.each(obj, function (index) {
                        var newObject = {
                            label: obj[index].label + "<br/>" + obj[index].dStart + " - " + obj[index].dEnd,
                            value: obj[index].value
                        };
                        _listperiod[_lisTPeriods.length] = newObject;
                    });
                }
            });

            $("input[id=save]").click(function () {
                var request = data = JSON.stringify(availableTagsplane);
                $.ajax({
                    contentType: "application/json; charset=utf-8",
                    url: "/App_Logic/dataGeneric.ashx?mode=insertschedule&json=" + data,
                    dataType: "json",
                    success: function (msg) {
                        window.location.href = "/Modules/TimeAttendance/plans-room.aspx?idterm=" + getUrlParameter("idterm");
                    }
                })

                window.location.href = "/Modules/TimeAttendance/plans-room.aspx?idterm=" + getUrlParameter("idterm");

                //                request.done(function (msg) {
                //                });

            });

            $("input[id=add]").click(function () {
                if ($("input[id=add]").val() == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>") {
                    availableTagsplane[indexedit].plane = $("#selectplane_chosen .chosen-single span").html();
                    availableTagsplane[indexedit].nplane = $("#selectplane_chosen .chosen-single p").html();
                    availableTagsplane[indexedit].teacherid = $("#selectteacher_chosen .chosen-single p").html();
                    availableTagsplane[indexedit].teacher = $("#selectteacher_chosen .chosen-single span").html();
                    availableTagsplane[indexedit].roomsid = $("#selectroom_chosen .chosen-single p").html();
                    availableTagsplane[indexedit].rooms = $("#selectroom_chosen .chosen-single span").html();
                    availableTagsplane[indexedit].nday = _indexday;
                    availableTagsplane[indexedit].sday = _listday[_indexday];
                    availableTagsplane[indexedit].speriod = _listperiod[_indexperiod].label;
                    availableTagsplane[indexedit].nperiod = _listperiod[_indexperiod].value;
                    ListTimeTable();
                    Clear();
                }
                else {
                    if (checkData()) {
                        var newObject = {
                            plane: $("#selectplane_chosen .chosen-single span").html(),
                            nplane: $("#selectplane_chosen .chosen-single p").html(),
                            teacherid: $("#selectteacher_chosen .chosen-single p").html(),
                            teacher: $("#selectteacher_chosen .chosen-single span").html(),
                            roomsid: $("#selectroom_chosen .chosen-single p").html(),
                            rooms: $("#selectroom_chosen .chosen-single span").html(),
                            nday: _indexday,
                            sday: _listday[_indexday],
                            speriod: _listperiod[_indexperiod].label,
                            nperiod: _listperiod[_indexperiod].value,
                            tableid: timetableid,
                            nschedule: 0
                        };

                        availableTagsplane[availableTagsplane.length] = newObject;
                        ListTimeTable();
                        Clear();
                    }
                    else {
                        alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132633") %>");
                    }
                }
            });

            $('#saveteacher').click(function () {
                var idterm = getUrlParameter("idterm");
                var sid = getUrlParameter("id");
                var teacherid = $("#selectteacherclass_chosen .chosen-single p").html();
                var request = $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/App_Logic/dataGeneric.ashx?mode=insertteacher&id=" + sid + "&teacherid=" + teacherid + "&idterm=" + idterm,
                    dataType: "json",
                    success: function (msg) {
                        timetableid = msg;
                        $("#div2").css("display", "");
                        $("#div1").css("display", "none");
                    }
                });
                request.done(function (msg) {
                    var requestschedule = $.ajax({
                        url: "/App_Logic/dataGeneric.ashx?mode=listschedule&id=" + sid + "&idterm=" + idterm,
                        success: function (objjson) {
                            $.each(objjson, function (index) {
                                availableTagsplane[availableTagsplane.length] = objjson[index];
                            });
                        }
                    });

                    requesTSchedules.done(function (msg) {
                        ListTimeTable();
                    });

                });

            });

            $("select[id*=ddlsPeriods]").change(function () {
                GetValuesPeriods();
            });
        });

    </script>
    <style type="text/css">
        .ui-autocomplete {
            max-height: 200px;
            overflow-y: auto; /* prevent horizontal scrollbar */
            overflow-x: hidden;
            z-index: 1060 !important;
        }
        /* IE 6 doesn't support max-height
   * we use height instead, but this forces the menu to always be this tall
   */
        * html .ui-autocomplete {
            height: 100px;
        }

        .ui-autocomplete-loading {
            background: white url("images/ui-anim_basic_16x16.gif") right center no-repeat;
        }

        #city {
            width: 25em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="report-card box-content plans-schedule-container" id="div1">
        <div class="row">
            <div class="col-lg-offset-1 col-lg-2 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>
            </div>
            <div class="col-lg-8 col-md-8 col-xs-8" style="vertical-align: text-bottom;">
                <asp:Literal ID="ltrYear" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-offset-1 col-lg-2 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132624") %>
            </div>
            <div class="col-lg-8 col-md-8 col-xs-8" style="vertical-align: text-bottom;">
                <asp:Literal ID="ltrTerm" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-offset-1 col-lg-2 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>
            </div>
            <div class="col-lg-8 col-md-8 col-xs-8" style="vertical-align: text-bottom;">
                <asp:Literal ID="ltrSubLv" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-offset-1 col-lg-2 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210005") %>
            </div>
            <div class="col-lg-8 col-md-8 col-xs-8" style="vertical-align: text-bottom;">
                <input type="text" id="txtaddteacher" runat="server" class="form-control" style="width: 400px; display: none;" />
                <input type="text" id="txtaddteacherid" runat="server" class="form-control" style="width: 400px; display: none;" />
                <select data-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202020") %>" class="chosen-select" id="selectteacherclass" tabindex="2" style="">
                    <option value=""></option>
                </select>
            </div>
        </div>
        <div class="row--space"></div>
        <div class="row button-section">
            <div class="col-lg-offset-1 col-lg-2 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3">
            </div>
            <div class="col-lg-8 col-md-8 col-xs-8" style="vertical-align: text-bottom;">
                <input type="button" class="btn btn-success btn-large-size global-btn" id="saveteacher" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %>" />
            </div>
        </div>
    </div>
    <div class="report-card box-content" id="div2" style="display: none;">
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-4">
                <table border="1" style="width: 100%">
                    <tr style="border-bottom: none;">
                        <td style="width: 20%;"></td>
                        <td style="width: 20%;"></td>
                        <td style="width: 20%;"></td>
                        <td style="width: 20%;"></td>
                        <td style="width: 20%;"></td>

                    </tr>
                    <tr>
                        <td colspan="5" style="padding: 5px; background-color: #347ab7; text-align: center; color: #ffffff;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132625") %> xxxx <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210456") %> x</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-left: 5px; background-color: #347ab7; color: #ffffff;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></td>
                        <td colspan="3" style="padding-left: 5px; text-align: center;">55577854</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-left: 5px; background-color: #347ab7; color: #ffffff;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></td>
                        <td colspan="3" style="padding-left: 5px; text-align: center;">55577854</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-left: 5px; background-color: #347ab7; color: #ffffff;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202006") %></td>
                        <td colspan="3" style="padding-left: 5px; text-align: center;">55577854</td>
                    </tr>
                </table>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4">
                <div class="panel panel-green panel-summary hidden">
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4">
                <table border="1" style="width: 100%">
                    <tr style="border-bottom: none;">
                        <td style="width: 30%;"></td>
                        <td style="width: 35%;"></td>
                        <td style="width: 35%;"></td>

                    </tr>
                    <tr>
                        <td colspan="1" style="padding-left: 10px; border-right: 0px;">120402</td>
                        <td colspan="2" style="border-left: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %></td>
                    </tr>
                    <tr>
                        <td colspan="1" style="padding-left: 10px; border-right: 0px;">120402</td>
                        <td colspan="2" style="border-left: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %></td>
                    </tr>
                    <tr>
                        <td colspan="1" style="padding-left: 10px; border-right: 0px;">120402</td>
                        <td colspan="2" style="border-left: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %></td>
                    </tr>
                    <tr>
                        <td colspan="1" style="padding-left: 10px; border-right: 0px;">120402</td>
                        <td colspan="2" style="border-left: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %></td>
                    </tr>
                    <tr>
                        <td colspan="1" style="padding-left: 10px; border-right: 0px;">120402</td>
                        <td colspan="2" style="border-left: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %></td>
                    </tr>
                    <tr>
                        <td colspan="1" style="padding-left: 10px; border-right: 0px;">120402</td>
                        <td colspan="2" style="border-left: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12" style="vertical-align: text-bottom;">
                <input type="button" class="btn btn-success btn-large-size" id="save" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12" style="vertical-align: text-bottom;">
                <br />
                <div style="margin-top: -20px;" id="timetable">
                </div>
            </div>
        </div>
        <div class="row"></div>
        <div class="col-lg-12 col-md-12 col-sm-12" style="vertical-align: text-bottom;">
            <br />
            <div style="margin-top: -20px;">
                <table border="1">
                    <tr style="border: none;">
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="6">06:00 - 07:00</td>
                        <td colspan="6">07:00 - 08:00</td>
                        <td colspan="6">08:00 - 09:00</td>
                        <td colspan="6">09:00 - 10:00</td>
                        <td colspan="6">10:00 - 11:00</td>
                        <td colspan="6">11:00 - 12:00</td>
                        <td colspan="6">12:00 - 13:00</td>
                        <td colspan="6">13:00 - 14:00</td>
                        <td colspan="6">14:00 - 15:00</td>
                        <td colspan="6">15:00 - 16:00</td>
                        <td colspan="6">16:00 - 17:00</td>
                        <td colspan="6">17:00 - 18:00</td>
                        <td colspan="6">18:00 - 19:00</td>
                        <td colspan="6">19:00 - 20:00</td>
                        <td colspan="6">20:00 - 21:00</td>
                    </tr>
                    <tr>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202007") %></td>
                        <td colspan="1"></td>
                        <td colspan="2" style="background-color: green;"></td>
                    </tr>
                    <tr>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202008") %></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202009") %></td>
                    </tr>
                    <tr>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202010") %></td>
                    </tr>
                    <tr>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202011") %></td>
                    </tr>
                    <tr>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202012") %></td>
                    </tr>
                    <tr>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202013") %></td>
                    </tr>
                </table>
            </div>
        </div>
        <!-- Modal -->
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title" id="myModalLabel">Modal title</h3>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-8" style="vertical-align: text-bottom;">
                                <%--   <input id="planeid" type="text" class="form-control" style="width: 400px; display: none;" />
                                <input id="plane" type="text" class="form-control" style="width: 400px;" />--%>
                                <select data-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202018") %>" class="chosen-select" id="selectplane" tabindex="2">
                                    <option value=""></option>
                                </select>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202019") %>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-8" style="vertical-align: text-bottom;">
                                <select data-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202020") %>" class="chosen-select" id="selectteacher" tabindex="2">
                                    <option value=""></option>
                                </select>
                                <%--                                <input id="teacherid" type="text" class="form-control" style="width: 400px; display: none;" />
                                <input id="teacher" type="text" class="form-control" style="width: 400px;" />--%>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-8" style="vertical-align: text-bottom;">
                                <%--  <input id="roomid" type="text" class="form-control" style="width: 400px; display: none;" />
                                <input id="room" type="text" class="form-control" style="width: 400px;" />--%>
                                <select data-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101281") %>" class="chosen-select" id="selectroom" tabindex="2">
                                    <option value=""></option>
                                </select>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <input type="button" class="btn btn-primary btn-large-size" id="add" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>" data-dismiss="modal" />
                        <input type="button" class="btn btn-danger btn-large-size" id="cancel" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" onclick="Clear();"
                            data-dismiss="modal" />
                    </div>
                </div>
            </div>
        </div>
    </div>
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
        function GetValuesPeriods() {
            var str = "";
            $("select[id*=ddlsPeriods] option:selected").each(function () {
                str = $(this).val() + " ";
            });

            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?id=" + str + "&mode=getperiod",
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        $("#Time").html(objjson[index].sTime);
                        $("#StartTime").html(objjson[index].sStartTime);
                        $("#EndTime").html(objjson[index].sEndTime);
                    });
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
