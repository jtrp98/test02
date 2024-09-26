<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="JobScan.aspx.cs"
    Inherits="FingerprintPayment.UpdateStatus.JobScan" %>

<%@ Register Src="~/UserControls/YTLCFilter.ascx" TagPrefix="uc1" TagName="YTLCFilter" %>
<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
   
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
    </style>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="../Scripts/freeze-table.1.3.min.js"></script>
    <%--    <script type="text/javascript" src="../../Scripts/freeze-table.js"></script>--%>
    <script src="../Report/ScriptReport.js" type="text/javascript"></script>
    <script src="Script/JobScan.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmss") %>" type="text/javascript"></script>

    <script type="text/javascript">

        $("#content-overlay").html()
        var reports_data = [];
        var HeaderCSS = " text-align: center !important;vertical-align: middle !important;";
        var HeaderCSS_1 = " text-align: center !important;vertical-align: middle !important;color: black;padding:0px;min-width: 50px;";
        var HeaderCSS_2 = " text-align: center !important;vertical-align: middle !important;padding:0px;";
        var RowsCSS = "text-align: center !important; vertical-align: middle !important;padding:0px;";
        var RowsCSS_1 = "text-align: center !important; vertical-align: middle !important;";
        var Search = [];
        var CycleCSS = "font-size: 70%; border-radius: 100%;border: solid black 1px;padding-right: 0px;padding-left: 0px;padding-top: 0px;padding-bottom: 0px;height: 13px;text-align: center;color:red;";
        $(function () {
            //$('select[id*=ddlsublevel] option[value=""]').text("- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133179") %> -");
            //$('select[id*=ddlSubLV2] option[value=""]').text("- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111085") %> -");

            //$('select[id*=sort_type]').change(function () {
            //    switch_sort($(this).val());
            //});
            //switch_sort($('select[id*=sort_type]').val());

            //$('select[id*=ddlSubLV2]').change(function () {
            //    getliststudent();
            //});

            //$("select[id*=ddlsublevel]").change(function () {
            //    //getplane();
            //    $('select[id*=ddlSubLV] option[value=""]').text("- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111085") %> -");
            //});

            if (jQuery.validator) {

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",
                });

                //$("#aspnetForm").validate({  // initialize the plugin
                //    errorPlacement: function (error, element) {
                //        var _class = element.attr('class');

                //        if (_class.includes('--req-append-last')) {
                //            error.insertAfter(element.parent());
                //        }
                //        else
                //            error.insertAfter(element);
                //    }

                //});
            }

            Reports_04 = new reports_04();

            //$("#content-overlay").show();
        });

        function switch_sort(sort_type) {
            switch (sort_type) {
                case "0": $(".sort_type").hide(); $($(".sort_type")[2]).show(); break;
                case "1": $(".sort_type").hide(); $($(".sort_type")[0]).show(); break;
                case "2": $(".sort_type").hide(); $($(".sort_type")[1]).show(); break;
                case "3": $(".sort_type").hide(); $($(".sort_type")[2]).show(); $($(".sort_type")[3]).show(); break;
            }
        }
        var term_id = 0, level2_id = 0, plane_id = 0, level_id=0;
        function Getreports(table_name, export_file) {

            if ($('#aspnetForm').valid() == false) {
                return false;
            }

            //var dStart = "", dEnd = "";
            //if ($('select[id*=ddlSubLV2]').val() === "" && $("#txtid").val() === "") {
            //    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111085") %>");
            //    return;
            //}

            Search = {
                "term_id": YTLCF.GetTermID(),
                "level2_id": YTLCF.GetClassID(),
                "student_Id": SAC.GetStudentID(),
                "level_id": YTLCF.GetLevelID()
                //$("#txtid").val()
                //$('select[id*=ddlSubLV2]').val(),
                //"sort_type": $('select[id*=sort_type]').val(),
                //"dStart": dStart,
                //"dEnd": dEnd,
                //"plane_Id": $('select[id*=plane]').val(),
            };
            //File Name Reports

            term_id = Search.term_id;
            level2_id = Search.level2_id;
            plane_id = Search.plane_Id;

            //$('[data-toggle="popover"]').popover('hide');
            closepopover();
            $("body").mLoading();
            PageMethods.returnlist(Search, function (e) {
                Reports_04.reports_data = e;
                if (e.reportsDatas.length > 0) {
                    Reports_04.RenderHtml(table_name, export_file);
                    //$("body").mLoading('hide');
                    $("#btnsavedata").removeClass("d-none");
                }
                else {
                }
                $("body").mLoading('hide');
            }, function (e) {
                //$("body").mLoading('hide');
            });
        }

        var previousPopOverBody = null;
        function OpenPopover(control, selectID) {
            //$('[data-toggle="popover"]').popover('hide');
            closepopover();
            previousPopOverBody = control;
            $(control).popover('show');
            //$(".popover-content select").selectpicker();
            $("#s_" + selectID).change(function () {
                switch ($(this).val()) {
                    case "0": SettingStatus(control, $(this), "/", "#0dc742"); break;
                    case "1": SettingStatus(control, $(this), "ส", "#e4922f"); break;
                    case "3": SettingStatus(control, $(this), "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>", "#ff3942"); break;
                    case "10": SettingStatus(control, $(this), "ล", "#9850e0"); break;
                    case "11": SettingStatus(control, $(this), "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>", "#ff00fe"); break;
                    case "12": SettingStatus(control, $(this), "ก", "#0393c7"); break;
                    case "6": SettingStatus(control, $(this), "ห", "#eb9f2f"); break;
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
                    case "10": SettingStatus(data_scan, $(this), "ล", "#9850e0"); break;
                    case "11": SettingStatus(data_scan, $(this), "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>", "#ff00fe"); break;
                    case "12": SettingStatus(data_scan, $(this), "ก", "#0393c7"); break;
                    case "6": SettingStatus(data_scan, $(this), "ห", "#eb9f2f"); break;
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
            var select_control = "<div class='row'><div class='col-md-12 col-sm-12'><select class='form-control' style='font-weight:bolder' id='" + RowsIndex + "'>" +
                "<option value='-1' style='color:black;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303012") %></option>" +
                "<option value='0' style='color:#0dc742;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></option>" +
                "<option value='1' style='color:#e4922f;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></option>" +
                "<option value='3' style='color:#ff3942;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></option>" +
                "<option value='10' style='color:#9850e0;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></option>" +
                "<option value='11' style='color:#ff00fe;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></option>" +
                "<option value='12' style='color:#0393c7;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %></option>" +
                "<option value='6' style='color:#eb9f2f;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %></option>" +
                //"<option value=''><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103022") %></option>" +
                "</select></div><div class='row--space'><br /></div>" +
                "<div class='col-md-12 col-sm-12 text-center'><div class='btn btn-sm btn-default' style='font-size:14px;' onclick='closepopover();'>Close</div></div></div>";
            content += select_control;

            return "<div data-toggle=\"popover\" style=\"cursor: pointer;\" onclick=\"OpenPopoverAll(this,'" + RowsIndex + "');\" title=\"" + title
                + "\" data-scan=\"" + data_scan
                + "\" data-content=\"" + content
                + "\" data-trigger=\"focus\" ><div data-placement=\"top\" data-toggle=\"tooltip\" data-original-title=\"<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133396") %>\">" + status + "</div></div>";
        }

        function createPopover(title, content, data_scan, Schedule_Id, status, status_log, RowsIndex) {
            var select_control = "<div class='row'><div class='col-md-12 col-sm-12'><select class='form-control'  style='font-weight:bolder' id='s_" + RowsIndex + "'>" +
                "<option value='-1' style='color:black;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303012") %></option>" +
                "<option value='0' style='color:#0dc742;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></option>" +
                "<option value='1' style='color:#e4922f;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></option>" +
                "<option value='3' style='color:#ff3942;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></option>" +
                "<option value='10' style='color:#9850e0;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></option>" +
                "<option value='11' style='color:#ff00fe;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></option>" +
                "<option value='12' style='color:#0393c7;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %></option>" +
                "<option value='6' style='color:#eb9f2f;background-color:#fff;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %></option>" +
                //"<option value=''><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103022") %></option>" +
                "</select></div><div class='row--space'><br /></div>" +
                "<div class='col-md-12 col-sm-12 text-center'><div class='btn btn-sm btn-default' style='font-size:14px;' onclick='closepopover();'>Close</div></div></div>";
            content += select_control;

            var bgcolor = "";
            switch (status_log) {
                case "0": bgcolor = "#0dc742"; break;
                case "1": bgcolor = "#e4922f"; break;
                case "3": bgcolor = "#ff3942"; break;
                case "10": bgcolor = "#9850e0"; break;
                case "11": bgcolor = "#ff00fe"; break;
                case "12": bgcolor = "#0393c7"; break;
                case "6": bgcolor = "#eb9f2f"; break;
                default: break;
            }

            return "<div data-toggle=\"popover\" style=\"cursor: pointer;background-color:" + bgcolor + "\" onclick=\"OpenPopover(this,'" + RowsIndex + "');\" title=\"" + title + "\" data-content=\"" + content
                + "\" data_scan=\"" + data_scan + "\" data-status=\"" + status_log + "\" data-schedule-id=\"" + Schedule_Id + "\" data-trigger=\"focus\" >" + status + "</div>";
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
            $("body").mLoading({
                text: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133397") %>",
            });
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
                $("body").mLoading('hide');
                if (result === "Success") {
                   
                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00789") %>',
                        //text: 'Something went wrong!',                      
                    });
                } else {
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602024") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133398") %>',
                        //text: 'Something went wrong!',                      
                    })
                   
                }
            });
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133399") %>           
            </p>
        </div>
    </div>

    <form runat="server" id="form1">
        <asp:HiddenField ID="hdfschoolname" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>


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
                        <uc1:YTLCFilter runat="server" ID="YTLCFilter" IsRequired="true" />

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301008") %></label>
                            <div class="col-md-3 ">
                                <uc1:StudentAutocomplete runat="server" ID="StudentAutocomplete" />
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

            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-warning  card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102145") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12">
                                <%--<table id="lst-data" class=" table-hover dataTable" width="100%"></table>--%>
                                <div class="freeze-table">
                                    <table id="example" class="table-hover dataTable table-show-result" style="" cellspacing="0" width="100%">
                                    </table>
                                </div>

                                <div class="d-none" id="export_excel">
                                    <table id="table_exports" class="table-hover  table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <%--  <input type="button" id="btnsavedata" class="btn btn-success button-custom hidden" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" onclick="getDataScan()" />  --%>

                                <button type="button" id="btnsavedata" onclick="getDataScan()" class="btn btn-success d-none">
                                    <span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>


    <div class="full-card box-content d-none">
        <%--        <div class="row">
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
        </div>
        <div class="row student">
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
        </div>
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603047") %></label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" class='form-control' id="txtid" style="display: none;" />
                    <input type="text" class='form-control' id="txtname" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101340") %>" />
                </div>
            </div>
        </div>
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
        </div>--%>
        <%-- <asp:Literal ID="ltrHeaderReport" runat="server" />
        <div class="row border-bottom">
            <br />
            <div class="col-sm-12">
                <fieldset>
                     <asp:ListView ID="lvReport" runat="server">
                    </asp:ListView>
                </fieldset>
            </div>

        </div>
        <div class="row--space">
        </div>
        <div class="row">
        </div>--%>
    </div>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
    <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>--%>
