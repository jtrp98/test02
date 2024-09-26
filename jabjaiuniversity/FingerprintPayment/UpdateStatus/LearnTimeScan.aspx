<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="LearnTimeScan.aspx.cs"
    Inherits="FingerprintPayment.UpdateStatus.LearnTimeScan" %>

<%@ Register Src="~/UserControls/LCFilter.ascx" TagPrefix="uc1" TagName="LCFilter" %>
<%@ Register Src="~/UserControls/YTFilter.ascx" TagPrefix="uc1" TagName="YTFilter" %>




<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <%--    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>
    <%--    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>--%>
    <%--<script src="/app/Reports/Come2school/ReportCome2SchoolStudentJS.js" type="text/javascript"></script>--%>

    <%--   <script src="/bootstrap/bootstrap-chosen/chosen.jquery.js" type="text/javascript"></script>
    <link href="/bootstrap/bootstrap-chosen/bootstrap-chosen.css" rel="stylesheet" />--%>


    <%--   <style type="text/css">
        @media (max-width: 999px) {
            .report-container {
                font-size: 18px;
            }
            label {
                font-weight: normal;
                font-size: 18px;
            }
            legend {
                padding-left: 30px;
                font-size: 18px;    
                font-weight: bold;
            }
            .button-custom {
                font-size: 18px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }
            .table-show-result {
                font-size: 20px;
            }
        }

        @media (min-width: 1000px) and (max-width: 1199px) {
            .report-container {
                font-size: 22px;
            }
            label {
                font-weight: normal;
                font-size: 22px;
            }
            legend {
                padding-left: 30px;
                font-size: 22px;
                font-weight: bold;
            }
            .button-custom {
                font-size: 22px;
                width: 100%;
                padding-left: 30px;
                padding-right: 30px;
            }
            .table-show-result {
                font-size: 22px;
            }
        }
        @media (min-width: 1200px) {
            .report-container {
                font-size: 26px;
            }
            label {
                font-weight: normal;
                font-size: 26px;
            }
            legend {
                padding-left: 30px;
                font-size: 26px;
                font-weight: bold;
            }
            .button-custom {
                font-size: 26px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }
            .table-show-result {
                font-size: 26px;
            }
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
        @media (min-width: 1300px) {
            #page-wrapper {
                position: inherit;
                margin: 0 0 0 250px;
                padding: 0 30px;
                border-left: 1px solid #e7e7e7;
                background-color: #eee;
                padding-top: 30px;
                padding-bottom: 30px;
                min-height: 900px;
            }
        }
        .header_01 {
            min-width: 100px;
        }
        .header_02 {
            min-width: 50px;
        }
        /*#example {
            background-color: white;
        }*/
        .sort_type {
            display: none;
        }
    </style>--%>

    <style>
        select {
            -webkit-appearance: none !important;
            /*webkit browsers */
            -moz-appearance: none !important;
            /*Firefox */
            appearance: auto !important;
            /* modern browsers */
            border-radius: 0;
        }

        .dataTable > tbody > tr > td {
            padding: 0px !important;
        }

        table.dataTable {
            margin-top: 0px !important;
            margin-bottom: 0px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="/Scripts/jscommon.js" type="text/javascript"></script>
    <%--    <script src="/app/Reports/Come2school/ReportCome2SchoolStudentJS.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>
    <script type="text/javascript" src="../../Scripts/freeze-table.js"></script>
    <script src="../Report/ScriptReport.js" type="text/javascript"></script>
    <script src="Script/LearnTimeScan.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmm") %>" type="text/javascript"></script>

    <script type="text/javascript">
        var reports_data = [];
        var HeaderCSS = " text-align: center !important;vertical-align: middle !important;";
        var HeaderCSS_1 = " text-align: center !important;vertical-align: middle !important;padding:0px;min-width: 50px;";
        var HeaderCSS_2 = " text-align: center !important;vertical-align: middle !important;padding:0px;";
        var RowsCSS = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px;";
        var RowsCSS_1 = "text-align: center !important; vertical-align: middle !important;";
        var Search = [];
        var CycleCSS = "font-size: 70%; border-radius: 100%;border: solid black 1px;padding-right: 0px;padding-left: 0px;padding-top: 0px;padding-bottom: 0px;height: 13px;text-align: center;color:red;";
        $(function () {
            //$("#content-overlay").show();
            //$('select[id*=ddlsublevel] option[value=""]').text("- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133179") %> -");
            //$('select[id*=ddlSubLV2] option[value=""]').text("- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111085") %> -");
            //$('select[id*=sort_type]').change(function () {
            //    switch_sort($(this).val());
            //});

            //switch_sort($('select[id*=sort_type]').val());

            //$("select[id*=ddlyear]").change(function () {
            //    getListTrem();
            //});


            $('select#sltClass').change(function () {
                getplane();
            });

            //$('#ctl00_MainContent_ddlsublevel').change(function () {
            //    getListSubLV2();
            //});

            //getListSubLV2();
            $('select#sltTerm').change(function () {
                getplane();
            });

            //$("select[id*=ddlsublevel]").change(function () {
            //    //getplane();
            //    $('select[id*=ddlSubLV] option[value=""]').text("- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111085") %> -");
            //});

            //getListTrem();

            //$('#plane').chosen();
            //$('.chosen-select-deselect').chosen({ allow_single_deselect: true });
            Reports_04 = new reports_04();
        });
        //function switch_sort(sort_type) {
        //    switch (sort_type) {
        //        case "0": $(".sort_type").hide(); $($(".sort_type")[2]).show(); break;
        //        case "1": $(".sort_type").hide(); $($(".sort_type")[0]).show(); break;
        //        case "2": $(".sort_type").hide(); $($(".sort_type")[1]).show(); break;
        //        case "3": $(".sort_type").hide(); $($(".sort_type")[2]).show(); $($(".sort_type")[3]).show(); break;
        //    }
        //}

        var term_id = 0, level2_id = 0, plane_id = 0;
        function Getreports(table_name, export_file) {

            if ($('#aspnetForm').valid() == false) {
                //e.preventDefault();
                //e.stopPropagation();
                return false;
            }

            var dStart = "", dEnd = "";
            //if ($('select[id*=ddlSubLV2]').val() === "") {
            //    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111085") %>");
            //    return;
            //}
            Search = {
                "term_id": YTF.GetTermID(),
                "level2_id": LCF.GetClassID(),
                "sort_type": $('select[id*=sort_type]').val(),
                "dStart": dStart,
                "dEnd": dEnd,
                "plane_Id": $('#plane').val()
            };

            //File Name Reports
            term_id = Search.term_id;
            level2_id = Search.level2_id;
            plane_id = Search.plane_Id;
            //$('[data-toggle="popover"]').popover('hide');
            closepopover();
            //$("body").mLoading();
            PageMethods.returnlist(Search, function (e) {
                Reports_04.reports_data = e;
                Reports_04.RenderHtml(table_name, export_file);
                //$("body").mLoading('hide');
                $("#btnsavedata").removeClass("d-none");
            }, function (e) {
                //$("body").mLoading('hide');
            });
        }
        function getplane() {

            //if ($("select[id*=semister]").val() === null || $("select[id*=ddlsublevel]").val() === null)
            //    return;
            if (YTF.GetTermID() == '' || LCF.GetLevelID() == '')
                return;
            $('#plane option').remove();
            $("select[id*=plane]").attr('disabled', 'disabled');

            PageMethods.SearchPlane(YTF.GetTermID(), LCF.GetClassID(), function (result) {
                result = $.parseJSON(result);
                //$('select[id*=plane]')
                //     .append($("<option></option>")
                //         .attr("value", "")
                //         .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"));
                $.each(result, function (index) {
                    $('#plane').append($('<option>', {
                        value: result[index].value,
                        text: result[index].text
                    }));
                });
                $("#plane").removeAttr('disabled');
                $("#plane").selectpicker('refresh');
                /* $('.chosen-select-deselect').chosen({ allow_single_deselect: true });*/
            }, function (mgserror) {
                console.log(mgserror);
            });
        }
        var previousPopOverBody = null;
        function OpenPopover(control, selectID) {
            //$('[data-toggle="popover"]').popover('hide');
            closepopover();
            previousPopOverBody = control;
            $(control).popover('show');
            $("#s_" + selectID).change(function () {
                switch ($(this).val()) {
                    case "0": SettingStatus(control, $(this), "/", "#0dc742"); break;
                    case "1": SettingStatus(control, $(this), "ส", "#e4922f"); break;
                    case "3": SettingStatus(control, $(this), "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>", "#ff3942"); break;
                    case "4": SettingStatus(control, $(this), "ล", "#9850e0"); break;
                    case "5": SettingStatus(control, $(this), "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>", "#ff00fe"); break;
                    case "6": SettingStatus(control, $(this), "ก", "#0393c7"); break;
                    case "9": SettingStatus(control, $(this), "ห", "#eb9f2f"); break;
                    default: $(this).css({ "background-color": "#fff", "coolr": "#000" }); break;
                }
            });
        }

        var previousPopOverHeader = null;
        function OpenPopoverAll(control, selectID) {
            //$('[data-toggle="popover"]').popover('hide');
            closepopover();
            previousPopOverHeader = control;
            $(control).popover('show');
            var data_scan = '[data_scan*="' + $(control).attr("data-scan") + '"]';
            $("#" + selectID).change(function () {
                switch ($(this).val()) {
                    case "0": SettingStatus(data_scan, $(this), "/", "#0dc742"); break;
                    case "1": SettingStatus(data_scan, $(this), "ส", "#e4922f"); break;
                    case "3": SettingStatus(data_scan, $(this), "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>", "#ff3942"); break;
                    case "4": SettingStatus(data_scan, $(this), "ล", "#9850e0"); break;
                    case "5": SettingStatus(data_scan, $(this), "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>", "#ff00fe"); break;
                    case "6": SettingStatus(data_scan, $(this), "ก", "#0393c7"); break;
                    case "9": SettingStatus(data_scan, $(this), "ห", "#eb9f2f"); break;
                    default: $(this).css({ "background-color": "#fff", "coolr": "#000" }); break;
                }
            });
        }
        function SettingStatus(data_scan, selectValues, data_display, color) {
            $(data_scan).html(data_display);
            $(data_scan).attr("status-update", true);
            $(data_scan).attr("data-status", $(selectValues).val());
            $(data_scan).css("background-color", color);
            $(selectValues).css({ "background-color": color, "color": "#fff" });
        }
        function createPopoverHeder(title, content, data_scan, status, RowsIndex) {
            var select_control = "<div class='row'><div class='col-md-12 col-sm-12'><select class='form-control' style='font-weight:bolder'  id='" + RowsIndex + "'>" +
                "<option value='-1' style='color:black;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303012") %></option>" +
                "<option value='0' style='color:#0dc742;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></option>" +
                "<option value='1' style='color:#e4922f;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></option>" +
                "<option value='3' style='color:#ff3942;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></option>" +
                "<option value='4' style='color:#9850e0;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></option>" +
                "<option value='5' style='color:#ff00fe;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></option>" +
                "<option value='6' style='color:#0393c7;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %></option>" +
                "<option value='9' style='color:#eb9f2f;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %></option>" +
                //"<option value=''><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103022") %></option>" +
                "</select></div><div class='row--space'><br /></div>" +
                "<div class='col-md-12 col-sm-12 text-center'><div class='btn btn-sm btn-default' style='font-size:14px;' onclick='closepopover();'>Close</div></div></div>";
            content += select_control;
            return "<div data-toggle=\"popover\" style=\"cursor: pointer;\" onclick=\"OpenPopoverAll(this,'" + RowsIndex + "');\" title=\"" + title
                + "\" data-scan=\"" + data_scan
                + "\" data-content=\"" + content
                + "\" data-trigger=\"focus\" ><div data-placement=\"top\" data-toggle=\"tooltip\" data-original-title=\"<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133396") %>\">" + status + "</div></div>";
        }
        function createPopover(title, content, data_scan, Schedule_Id, status, status_log, RowsIndex , StudentStatus) {
            var select_control = "<div class='row'><div class='col-md-12 col-sm-12'><select class='form-control' style='font-weight:bolder' id='s_" + RowsIndex + "'>" +
                "<option value='-1' style='color:black;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303012") %></option>" +
                "<option value='0' style='color:#0dc742;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></option>" +
                "<option value='1' style='color:#e4922f;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></option>" +
                "<option value='3' style='color:#ff3942;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></option>" +
                "<option value='4' style='color:#9850e0;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></option>" +
                "<option value='5' style='color:#ff00fe;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></option>" +
                "<option value='6' style='color:#0393c7;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %></option>" +
                "<option value='9' style='color:#eb9f2f;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %></option>" +
                //"<option value=''><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103022") %></option>" +
                "</select></div><div class='row--space'><br /></div>" +
                "<div class='col-md-12 col-sm-12 text-center'><div class='btn btn-sm btn-default' style='font-size:14px;' onclick='closepopover();'>Close</div></div></div>";
            content += select_control;
            var bgcolor = "";
            switch (status_log) {
                case "0": bgcolor = "#0dc742"; break;
                case "1": bgcolor = "#e4922f"; break;
                case "3": bgcolor = "#ff3942"; break;
                case "4": bgcolor = "#9850e0"; break;
                case "5": bgcolor = "#ff00fe"; break;
                case "6": bgcolor = "#0393c7"; break;
                case "9": bgcolor = "#eb9f2f"; break;
                default: break;
            }

            if (StudentStatus == 0) {
                return "<div data-toggle=\"popover\" style=\"cursor: pointer;background-color:" + bgcolor + "\" onclick=\"OpenPopover(this,'" + RowsIndex + "');\" title=\"" + title + "\" data-content=\"" + content
                    + "\" data_scan=\"" + data_scan + "\" data-status=\"" + status_log + "\" data-schedule-id=\"" + Schedule_Id + "\" data-trigger=\"focus\" >" + status + "</div>";
            }
            else {
                return "<div data-toggle=\"popover\" style=\"cursor: pointer;background-color:" + bgcolor + "\" >" + status + "</div>";
            }
        }
        function closepopover() {
            //$("[data-toggle=popover]").popover('hide');
            if (previousPopOverHeader != null) {
                $(previousPopOverHeader).popover('hide');
            }
            if (previousPopOverBody != null) {
                $(previousPopOverBody).popover('hide');
            }
        }
        function getDataScan() {

            Swal.fire({
                type: 'info',
                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133397") %>',
            });

            //$("body").mLoading({
            //    text: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133397") %>",
            //});
            //$('[data-toggle="popover"]').popover('hide');
            closepopover();
            var table_tr = $("#example tbody tr");
            var user_log = [];
            $.each(table_tr, function (tr_index, tr_values) {
                var table_td = $(tr_values).find("td");
                var td_lable = $(tr_values).find("td lable");
                var td_div = $(tr_values).find("td div[data-toggle='popover'][status-update=true]");
                var i;
                var scan_data = [];
                $.each(td_div, function (td_div_index, td_div_values) {
                    if ($(td_div_values).html().trim() !== "") {
                        scan_data.push({
                            "date_log": $(td_div_values).attr("data_scan"),
                            "schedule_id": $(td_div_values).attr("data-schedule-id"),
                            "status_log": $(td_div_values).attr("data-status")
                        });
                    }
                });
                user_log.push({ student_id: $(td_lable).attr("data-userid"), updateScanLogs: scan_data });
            });

            console.log(user_log);

            PageMethods.updateData({
                term_id: term_id,
                level2_id: level2_id,
                plane_id: plane_id,
                updateUsers: user_log
            }, function (result) {
                $("#example tbody tr").find("td div[data-toggle='popover'][status-update=true]").attr("status-update", false);
                //$("body").mLoading('hide');
                if (result === "Success") {

                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00789") %>',
                    });

                    //$.confirm({
                    //    title: false,
                    //    content: '<img src="/images/checked.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h1 class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00789") %></h1>',
                    //    theme: 'material',
                    //    type: 'blue',
                    //    buttons: {
                    //        "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                    //            btnClass: 'btn-primary',
                    //            keys: ['enter', 'shift'],
                    //            action: function () {
                    //            }
                    //        }
                    //    }
                    //});
                } else {
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602024") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133398") %>',
                        //text: 'Something went wrong!',                      
                    })

                    //$.confirm({
                    //    title: false,
                    //    content: '<img src="/images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><h1 class="text-danger text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602024") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133398") %></h1>',
                    //    theme: 'material',
                    //    columnClass: 'col-md-5 col-md-offset-4',
                    //    type: 'red',
                    //    buttons: {
                    //        "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                    //            keys: ['enter', 'shift'],
                    //            btnClass: 'btn-primary',
                    //            action: function () {
                    //                console.log(result);
                    //            }
                    //        }
                    //    }
                    //});
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206192") %>
            </p>
        </div>
    </div>

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Release" />
        <asp:HiddenField ID="hdfschoolname" runat="server" />

        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                    </div>
                    <div class="card-body ">
                        <uc1:LCFilter runat="server" ID="LCFilter" IsRequired="true" />

                        <uc1:YTFilter runat="server" ID="YTFilter" IsRequired="true" />

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %></label>
                            <div class="col-md-3 ">
                                <select id="plane" name="plane" disabled required class="selectpicker --req-append-last" data-style="select-with-transition" data-width="100%" data-size="7" data-live-search="true">
                                </select>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"></label>
                            <div class="col-md-3 ">
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-center">
                                <br />
                                <button type="button" onclick="Getreports('example', false);" class="btn btn-fill btn-info">
                                    <span class="material-icons">search</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-warning card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102145") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset>
                                    <div class="freeze-table">
                                        <table id="example" class="table-hover dataTable table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                                        </table>
                                    </div>
                                </fieldset>

                                <fieldset class="d-none" id="export_excel">
                                    <table id="table_exports" class="table-hover dataTable table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                                    </table>
                                </fieldset>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-center">
                                <%--  <input type="button" id="btnsavedata" class="btn btn-success d-none" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" onclick="getDataScan()" />--%>
                                <button type="button" id="btnsavedata" onclick="getDataScan()" class="btn btn-success d-none">
                                    <span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="full-card box-content d-none">
            <%--       <div class="row student">
                <div class="form-group col-sm-6">
                    <label class="col-md-5 col-sm-6 control-label">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> :</label>
                    <div class="col-md-7 col-sm-6">
                        <asp:DropDownList ID="ddlsublevel" runat="server" class="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group col-sm-6">
                    <label class="col-md-5 col-sm-6 control-label">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> :</label>
                    <div class="col-md-7 col-sm-6">
                        <asp:DropDownList ID="ddlSubLV2" runat="server" class="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>--%>
            <%-- <div class="row">
                <div class="form-group col-sm-6">
                    <label class="col-md-5 col-sm-6 control-label">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</label>
                    <div class="col-md-7 col-sm-6">
                        <asp:DropDownList ID="ddlyear" runat="server" class="form-control">
                            <asp:ListItem Text="2558" Value="2557" Selected="True" />
                            <asp:ListItem Text="2559" Value="2557" Selected="False" />
                            <asp:ListItem Text="2560" Value="2557" Selected="False" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group col-sm-6">
                    <label class="col-md-5 col-sm-6 control-label">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :</label>
                    <div class="col-md-7 col-sm-6">
                        <asp:DropDownList ID="semister" runat="server" class="form-control">
                            <asp:ListItem Text="1" Value="1" Selected="True" />
                            <asp:ListItem Text="2" Value="2" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>--%>
            <%--   <div class="row">
                <div class="form-group col-sm-6">
                    <label class="col-md-5 col-sm-6 control-label">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %> :</label>
                    <div class="col-md-7 col-sm-6">
                        <select id="plane" name="plane" class="form-control chosen-select" disabled></select>
                    </div>
                </div>
                <div class="form-group col-sm-6">
                   
                </div>
            </div>--%>
            <%-- <div class="row hidden">
                <div class="form-group col-sm-6">
                    <label class="col-md-5 col-sm-6 control-label">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603047") %></label>
                    <div class="col-md-7 col-sm-6">
                        <input type="text" class='form-control' id="txtid" style="display: none;" />
                        <input type="text" class='form-control' id="txtname" />
                    </div>
                </div>
            </div>--%>
            <div class="row">
                <div class="col-sm-12">
                    <input type="button" class="btn btn-primary button-custom" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="Getreports('example', false);" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                </div>
            </div>
            <div class="row--space">
            </div>
            <asp:Literal ID="ltrHeaderReport" runat="server" />
            <%--<div class="row border-bottom">
                <br />
                <div class="col-sm-12">
                    <fieldset>
                        <asp:ListView ID="lvReport" runat="server">
                        </asp:ListView>
                        <div class="freeze-table">
                            <table id="example" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                            </table>
                        </div>
                    </fieldset>
                </div>
                <fieldset class="hidden" id="export_excel">
                    <table id="table_exports" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                    </table>
                </fieldset>
            </div>--%>
            <div class="row--space">
            </div>
            <%-- <div class="row">
                <div class="col-sm-12">
                    <input type="button" id="btnsavedata" class="btn btn-success button-custom hidden" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" onclick="getDataScan()" />
                </div>
            </div>--%>
        </div>
    </form>
</asp:Content>

