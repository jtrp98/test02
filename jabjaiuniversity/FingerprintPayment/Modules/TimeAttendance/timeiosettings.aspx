<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="timeiosettings.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.timeiosettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />
    <script src="/Scripts/jquery-confirm.js"></script>
    <script type="text/javascript">

        function FunctionPopUpInser(Mode, Title) {
            $('#myModal').modal(Mode)
            $('#myModalLabel').html(Title + "")
            $('input[id*=btnInsertData]').css("display", "");
            $('input[id*=btnEditData]').css("display", "none");
            $('input[id*=txtsTimetype]').val("");
            $('input[id*=rblcUserType]').prop("checked", false);
            $('input[id*=txtid]').val("");
        }

        function FunctionPopUp(Mode, Title, id, name, type) {
            $('#myModal').modal(Mode)
            $('#myModalLabel').html(Title + "")
            $('input[id*=txtsTimetype]').val(name);
            $('input[id*=rblcUserType_' + (type - 1) + ']').prop("checked", true);
            $('input[id*=txtid]').val(id);
            $('input[id*=btnInsertData]').css("display", "none");
            $('input[id*=btnEditData]').css("display", "");
        }

        function GaeTime(strTime) {
            var MyDate_String_Value = strTime;
            var value = new Date(parseInt(MyDate_String_Value.replace(/(^.*\()|([+-].*$)/g, '')));
            var Hours = value.getHours() + "";
            var Minutes = value.getMinutes() + "";
            if (Hours.length == 1) Hours = "0" + Hours;
            if (Minutes.length == 1) Minutes = "0" + Minutes;
            return dat = Hours + ':' + Minutes;
        }

        function GetData(Index, tablename, type) {

            //$('#ctl00_MainContent_rblTimeType_' + Index).attr("checked", "checked");
            $("#tablename").html($('#' + tablename + ' a').html() + ' <i class="fa fa-edit fa-fw">');
            $('input[id*=hdfnTimeType]').val(Index);
            $('input[id*=hdfsName]').val($('#' + tablename + ' a').html());
            $('input[id*=hdfnType]').val(type);

            var sID = Index;//$('#ctl00_MainContent_rblTimeType_' + Index + ':checked').val();

            $.blockUI({ message: '<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101325") %></h1>' });
            $.ajax({
                type: "POST",
                url: "/App_Logic/dataGeneric.ashx?ID=" + sID + "&mode=time",
                cache: false,
                contentType: 'application/json;',
                success: function (obj) {
                    $.each(obj, function (index) {
                        var dTimeStart_IN = GaeTime(obj[index].dTimeStart_IN);
                        var dTimeEnd_IN = GaeTime(obj[index].dTimeEnd_IN);
                        var dTimeStart_OUT = GaeTime(obj[index].dTimeStart_OUT);
                        var dTimeEnd_OUT = GaeTime(obj[index].dTimeEnd_OUT);
                        var dTimeHalf = GaeTime(obj[index].dTimeHalf);
                        var nTimeLate = obj[index].nTimeLate;
                        var sDay = "";
                        switch (obj[index].nDay) {
                            case 0: sDay = "mon"; break;
                            case 1: sDay = "tue"; break;
                            case 2: sDay = "wes"; break;
                            case 3: sDay = "thu"; break;
                            case 4: sDay = "fri"; break;
                            case 5: sDay = "sat"; break;
                            case 6: sDay = "sun"; break;
                        }

                        $("input[id*=" + sDay + "start1]").val(dTimeStart_IN);
                        $("input[id*=" + sDay + "start2]").val(dTimeEnd_IN);
                        $("input[id*=" + sDay + "stop1]").val(dTimeStart_OUT);
                        $("input[id*=" + sDay + "stop2]").val(dTimeEnd_OUT);
                        $("input[id*=" + sDay + "timehalf]").val(dTimeHalf);
                        $("input[id*=" + sDay + "timelate]").val(nTimeLate);

                        if (obj[index].cDel == "1") {
                            $("input[id*=ch" + sDay + "]").prop("checked", true);
                            $("." + sDay).removeAttr("disabled");
                            $(".overlay-" + sDay).hide();
                        }
                        else {
                            $("input[id*=ch" + sDay + "]").prop("checked", false);
                            $("." + sDay).attr('disabled', 'disabled');
                            $(".overlay-" + sDay).show();
                        }
                    });
                    $.unblockUI();
                }
            });
        }
        function GaeDateTime(strTime) {
            var MyDate_String_Value = strTime;
            var value = new Date(
                parseInt(MyDate_String_Value.replace(/(^.*\()|([+-].*$)/g, '')));
            return dat = value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getFullYear() + ' ' + value.getHours() + ':' + value.getMinutes();
        }

        $(document).ready(function () {
            GetData($('input[id*=hdfnTimeType]').val(), 'type1_0');
            $('li[id*=type1]').css('display', '');
            $('.dropdown-toggle').dropdown()
        });

        function activetabtype(act, dis) {
            $('li[id*=type]').removeClass('active').a;
            $('li[id*=type' + act + ']').css('display', '');
            $('li[id=type' + act + '_0]').addClass('active');
            $('li[id*=type' + dis + ']').css('display', 'none');
            GetData($('li[id=type' + act + '_0]').children('a').attr('id'))
        }
    </script>

    <%-- <%
        var permissionData = new RoleStatus();
        try
        {
            permissionData =  mp_master.Permission_Page;
            if (permissionData != null && !string.IsNullOrEmpty(permissionData.permission) && permissionData.permission == "0")
            { %>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#tablename").click(function () {
                FunctionPopUp("show", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>", $('input[id*=hdfnTimeType]').val(), $('input[id*=hdfsName]').val(), $('input[id*=hdfnType]').val());
            })
        });
    </script>
    <%
            }
        }
        catch (Exception ex)
        {
            Response.Write(permissionData);
        }%>--%>


    <script type="text/javascript">

        $(document).ready(function () {
            $("#tablename").click(function () {
                FunctionPopUp("show", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>", $('input[id*=hdfnTimeType]').val(), $('input[id*=hdfsName]').val(), $('input[id*=hdfnType]').val());
            })
        });
    </script>
    <style type="text/css">
        .input-group-addon, .input-group-btn, .input-group .form-control {
            text-align: center;
        }

        .dropdown-menu {
            max-height: 500px;
            overflow-y: auto;
            overflow-x: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
    <div class="row">
        <asp:HiddenField ID="hdfsName" runat="server" />
        <asp:HiddenField ID="hdfnType" runat="server" />
        <asp:HiddenField ID="hdfnTimeType" runat="server" />
        <div class="col-lg-12">
            <div class="panel panel-default timeiosetting-container">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="col-xs-offset-6 col-xs-6 col-md-offset-4 col-md-8 col-sm-offset-2 col-sm-10" style="padding-right: 0px;">
                                <ul class="nav navbar-nav navbar-right hidden">
                                    <li id="fat-menu" class="dropdown">
                                        <a id="dLabel2" href="#" class="dropdown-toggle btn btn-default timeiosetting-btn-select" data-toggle="dropdown"
                                            aria-haspopup="true" style="margin-right: 5px; color: #000;"
                                            aria-expanded="false"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132673") %> <span class="caret"></span>
                                        </a>
                                        <ul id="menu2" class="dropdown-menu text-right" aria-labelledby="dLabel2" style="margin-right: 5px; width: 100%;">
                                            <asp:Literal ID="ltrTabHeaderMenu2" runat="server" />
                                        </ul>
                                    </li>
                                </ul>
                                <ul class="nav navbar-nav navbar-right">
                                    <li id="fat-menu" class="dropdown">
                                        <a id="dLabel1" href="#" class="dropdown-toggle btn btn-default timeiosetting-btn-select" data-toggle="dropdown"
                                            aria-haspopup="true" style="margin-right: 5px; color: #000;"
                                            aria-expanded="false"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803009") %> <span class="caret"></span>
                                        </a>
                                        <ul id="menu1" class="dropdown-menu" aria-labelledby="dLabel1" style="margin-right: 5px; min-width: 100%;">
                                            <asp:Literal ID="ltrTabHeaderMenu1" runat="server" />
                                        </ul>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:RadioButtonList ID="rblTimeType" runat="server" class="tab-pane active" RepeatDirection="Horizontal" />
                            <table align="center" class="table table-striped timeiosettings-table" border="1" style="font-weight: bolder">
                                <thead>
                                    <tr style="border-top: 2px; border-left: 1px; border-bottom: 0px; border-right: 1px; border-style: solid; border-color: #fff;">
                                        <td colspan="5" style="padding: 0px;">
                                            <ul class="nav nav-tabs" role="tablist">
                                                <li role="presentation" class="active">
                                                    <a href="#home" id="tablename" aria-controls="home" role="tab" data-toggle="tab">Home </a>
                                                </li>
                                            </ul>
                                        </td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr style="background: #337AB7; color: #fff;">
                                        <%--   <td style="width: 12%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132589") %>
                    </td>--%>
                                        <td style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>
                                        </td>
                                        <td style="width: 30%; text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %>
                                        </td>
                                        <td style="width: 15%; text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803014") %>
                                        </td>
                                        <td style="width: 10%; text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803015") %>
                                        </td>
                                        <td style="width: 30%; text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %>
                                        </td>
                                    </tr>
                                    <!-- start Monday -->
                                    <tr>
                                        <td>
                                            <div class="checkbox">
                                                <label>
                                                    <input class="daycheckbox" day="mon" id="chmon" runat="server" type="checkbox" style="position: static;" />
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202007") %>
                                                </label>
                                            </div>
                                            <%--  </td>
                    <td>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202007") %>--%>
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control mon clock-box" id="monstart1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control mon clock-box" id="monstart2" runat="server" />
                                            </div>

                                        </td>
                                        <td>
                                            <div class="row">
                                                <div class="col-md-6 adjust-col-padding-right-side">
                                                    <input type="text" class="form-control mon minute-box" id="montimelate" runat="server" />
                                                </div>
                                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control mon clock-box" id="montimehalf" runat="server" />
                                            </div>
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control mon clock-box" id="monstop1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control mon clock-box" id="monstop2" runat="server" />
                                            </div>

                                        </td>
                                    </tr>
                                    <!-- end Monday-->
                                    <!-- start Tuesday -->
                                    <tr class="active">
                                        <td>
                                            <div class="checkbox">
                                                <label>
                                                    <input class="daycheckbox" day="tue" id="chtue" runat="server" type="checkbox" style="position: static;" />
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202008") %>
                                                </label>
                                            </div>
                                            <%-- </td>
                    <td>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202008") %>--%>
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control tue clock-box" id="tuestart1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control tue clock-box" id="tuestart2" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div class="row">
                                                <div class="col-md-6 adjust-col-padding-right-side">
                                                    <input type="text" class="form-control tue minute-box" id="tuetimelate" runat="server" />
                                                </div>
                                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top"
                                                data-autoclose="true">
                                                <input type="text" class="form-control tue clock-box" id="tuetimehalf" runat="server" />
                                            </div>
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control tue clock-box" id="tuestop1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control tue clock-box" id="tuestop2" runat="server" />
                                            </div>

                                        </td>
                                    </tr>
                                    <!-- end Tuesday -->
                                    <!-- start Wensday -->
                                    <tr>
                                        <td>
                                            <div class="checkbox">
                                                <label>
                                                    <input class="daycheckbox" day="wes" id="chwes" runat="server" type="checkbox" style="position: static;" />
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202009") %>
                                                </label>
                                            </div>
                                            <%--   </td>
                    <td>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202009") %>--%>
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control wes clock-box" id="wesstart1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control wes clock-box" id="wesstart2" runat="server" />
                                            </div>

                                        </td>
                                        <td>
                                            <div class="row">
                                                <div class="col-md-6 adjust-col-padding-right-side">
                                                    <input type="text" class="form-control wes minute-box" id="westimelate" runat="server" />
                                                </div>
                                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control wes clock-box" id="westimehalf" runat="server" />
                                            </div>
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control wes clock-box" id="wesstop1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left"
                                                data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control wes clock-box" id="wesstop2" runat="server" />
                                            </div>

                                        </td>
                                    </tr>
                                    <!-- end Wensday -->
                                    <!--  start Thursday-->
                                    <tr class="active">
                                        <td>
                                            <div class="checkbox">
                                                <label>
                                                    <input class="daycheckbox" day="thu" id="chthu" runat="server" type="checkbox" style="position: static;" />
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202010") %>
                                                </label>
                                            </div>
                                            <%--   </td>
                    <td>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202010") %>--%>
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left"
                                                data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control thu clock-box" id="thustart1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control thu clock-box" id="thustart2" runat="server" />
                                            </div>

                                        </td>
                                        <td>
                                            <div class="row">
                                                <div class="col-md-6 adjust-col-padding-right-side">
                                                    <input type="text" class="form-control thu minute-box" id="thutimelate" runat="server" />
                                                </div>
                                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top"
                                                data-autoclose="true">
                                                <input type="text" class="form-control thu clock-box" id="thutimehalf" runat="server" />
                                            </div>
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left"
                                                data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control thu clock-box" id="thustop1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left"
                                                data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control thu clock-box" id="thustop2" runat="server" />
                                            </div>

                                        </td>
                                    </tr>
                                    <!-- end Thursday -->
                                    <!-- start Friday-->
                                    <tr>
                                        <td>
                                            <div class="checkbox">
                                                <label>
                                                    <input class="daycheckbox" day="fri" id="chfri" runat="server" type="checkbox" style="position: static;" />
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202011") %>
                                                </label>
                                            </div>
                                            <%--   </td>
                    <td>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202011") %>--%>
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left"
                                                data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control fri clock-box" id="fristart1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left"
                                                data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control fri clock-box" id="fristart2" runat="server" />
                                            </div>

                                        </td>
                                        <td>
                                            <div class="row">
                                                <div class="col-md-6 adjust-col-padding-right-side">
                                                    <input type="text" class="form-control fri minute-box" id="fritimelate" runat="server" />
                                                </div>
                                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control fri clock-box" id="fritimehalf" runat="server" />
                                            </div>
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control fri clock-box" id="fristop1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control fri clock-box" id="fristop2" runat="server" />
                                            </div>

                                        </td>
                                    </tr>
                                    <!-- end Friday -->
                                    <!-- start Saturday -->
                                    <tr class="active">
                                        <td>
                                            <div class="checkbox">
                                                <label>
                                                    <input class="daycheckbox" day="sat" id="chsat" runat="server" type="checkbox" style="position: static;" />
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202012") %>
                                                </label>
                                            </div>
                                            <%--   </td>
                    <td>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202012") %>--%>
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control sat clock-box" id="satstart1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control sat clock-box" id="satstart2" runat="server" />
                                            </div>

                                        </td>
                                        <td>
                                            <div class="row">
                                                <div class="col-md-6 adjust-col-padding-right-side">
                                                    <input type="text" class="form-control sat minute-box" id="sattimelate" runat="server" />
                                                </div>
                                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control sat clock-box" id="sattimehalf" runat="server" />
                                            </div>
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control sat clock-box" id="satstop1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control sat clock-box" id="satstop2" runat="server" />
                                            </div>

                                        </td>
                                    </tr>
                                    <!-- end Saturday -->
                                    <!-- start Sunday -->
                                    <tr>
                                        <td>
                                            <div class="checkbox">
                                                <label style="vertical-align: middle;">
                                                    <input class="daycheckbox" day="sun" id="chsun" runat="server" type="checkbox" style="position: static;" />
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202013") %>
                                                </label>
                                            </div>
                                            <%--    </td>
                    <td>--%>
                       
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control sun clock-box" id="sunstart1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control sun clock-box" id="sunstart2" runat="server" />
                                            </div>

                                        </td>
                                        <td>
                                            <div class="row">
                                                <div class="col-md-6 adjust-col-padding-right-side">
                                                    <input type="text" class="form-control sun minute-box" id="suntimelate" runat="server" />
                                                </div>
                                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control sun clock-box" id="suntimehalf" runat="server" />
                                            </div>
                                        </td>
                                        <td class="table-td-attend">
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control sun clock-box" id="sunstop1" runat="server" />
                                            </div>
                                            <div style="text-align: center;">
                                                -
                                            </div>
                                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                                <input type="text" class="form-control sun clock-box" id="sunstop2" runat="server" />
                                            </div>

                                        </td>
                                    </tr>
                                    <!-- end Sunday -->
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div style="text-align: center; padding: 30px 30px 40px 0px" class="button-section">
                        <asp:Button ID="btnSave" ValidationGroup="add" runat="server" CssClass="btn btn-success global-btn col3 btnpermission"
                            Font-Bold="true" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                        <button id="btnCancle" type="button" class="btn btn-danger global-btn col3 btnpermission hidden" style="margin-right: 5px; font-weight: bold;">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                        <button id="btnReset" type="button" class="btn btn-default global-btn col3 btnpermission pull-right" style="margin-right: 5px; font-weight: bold;">
                            <%--<i class="glyphicon glyphicon-pencil"></i>--%><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803017") %></button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $('.clockpicker').clockpicker();
        $(function () {
            $(".daycheckbox").change(function () {
                if ($(this).is(':checked')) {
                    $("." + $(this).attr("day")).removeAttr("disabled");
                    $(".overlay-" + $(this).attr("day")).hide();
                }
                else {
                    $("." + $(this).attr("day")).attr('disabled', 'disabled');
                    $(".overlay-" + $(this).attr("day")).show();
                }
            });

            $("#btnReset").click(function () {
                var StrDays = ["mon", "tue", "wes", "thu", "fri", "sat", "sun"];
                $.each(StrDays, function (index, values) {
                    resetClock(values);
                });
            });

            $("#btnCancle").click(function () {
                modalConfirm(); /*
            if (window.confirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132666") %> ")) {
                window.location = "/AdminMain.aspx";
            }*/
            });
        });

        function resetClock(days) {
            if (days != "sat" && days != "sun") {
                $("." + days).removeAttr("disabled");
                $(".overlay-" + days).hide();
                $('#ctl00_MainContent_ch' + days).attr('checked', 'checked');
            }
            else {
                $("." + days).attr('disabled', 'disabled');
                $(".overlay-" + days).show();
                $('#ctl00_MainContent_ch' + days).removeAttr("checked");
            }
            $("#ctl00_MainContent_" + days + "start1").val("06:00");
            $("#ctl00_MainContent_" + days + "start2").val("07:50");
            $("#ctl00_MainContent_" + days + "stop1").val("17:00");
            $("#ctl00_MainContent_" + days + "stop2").val("18:50");
            $("#ctl00_MainContent_" + days + "timehalf").val("12:00");
            $("#ctl00_MainContent_" + days + "timelate").val("120");
        }

        function modalConfirm() {
            showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", '<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132667") %></p>' +
                '<button class="btn btn-danger" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>&nbsp;' +
                '<button class="btn btn-success" onclick="window.location=\'/AdminMain.aspx\';"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>', function () {

                });
        }
    </script>
    <br />
    <!-- Modal -->
    <div id="myModal" class="modal fade alertBoxInfo timeio-modal-container" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md" style="top: 150px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h3 class="modal-title" id="myModalLabel">Modal title</h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-4">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803010") %>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtid" Style="display: none;" />
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtsTimetype" />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-4">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803011") %>
                            </div>
                            <div class="col-sm-8">
                                <asp:RadioButtonList ID="rblcUserType" runat="server" RepeatDirection="Vertical">
                                    <asp:ListItem Text="&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %>" Selected="True" Value="1" />
                                    <asp:ListItem Text="&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803012") %>" Value="2" />
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default global-btn" data-dismiss="modal">
                        Close</button>
                    <asp:Button class="btn btn-primary global-btn" ID="btnEditData" runat="server" Text="แก้ไฃข้อมูล" />
                    <asp:Button class="btn btn-primary global-btn" ID="btnInsertData" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
