<%@ Page Title="" Language="C#" MasterPageFile="~/Material.Master" AutoEventWireup="true" CodeBehind="Reportmobile04.aspx.cs" Inherits="FingerprintPayment.Report.Reportmobile04" %>

<%@ Register Src="~/UserControls/LCFilter.ascx" TagPrefix="uc1" TagName="LCFilter" %>
<%@ Register Src="~/UserControls/YTFilter.ascx" TagPrefix="uc1" TagName="YTFilter" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <style>
        table.dataTable {
            margin-top: 0px !important;
            margin-bottom: 0px !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">

    <script src="/app/Reports/Come2school/ReportCome2SchoolStudentJS.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>
    <script src="ScriptReport.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmss") %>" type="text/javascript"></script>
    <script src="Script/Reportmobile04.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmss") %>" type="text/javascript"></script>
    <script src="../Scripts/freeze-table.js" type="text/javascript"></script>

    <script>
        $(function () {

            $('.datepicker').datetimepicker({
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
                debug: false,
                //defaultDate: "<%=DateTime.Now.ToString("dd/MM/yyyy") %>",
                //autoclose: true,
                //autoclose: true,
                //showOn: "button",
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-chevron-up",
                    down: "fa fa-chevron-down",
                    previous: 'fa fa-chevron-left',
                    next: 'fa fa-chevron-right',
                    today: 'fa fa-screenshot',
                    clear: 'fa fa-trash',
                    close: 'fa fa-remove'
                }
            });

            $(".datepicker").keydown(function (e) {
                e.preventDefault();
            });

            if (jQuery.validator) {

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",

                });

                $("#aspnetForm").validate({  // initialize the plugin
                    errorPlacement: function (error, element) {
                        var _class = element.attr('class');

                        if (_class.includes('--req-append-last')) {
                            error.insertAfter(element.parent());
                        }
                        else
                            error.insertAfter(element);
                    }

                });
            }
        });
    </script>

    <script type="text/javascript">
        var reports_data = [];
        var HeaderCSS = "text-align: center !important;vertical-align: middle !important;min-width: 150px;";
        var HeaderCSS_1 = "text-align: center !important;vertical-align: middle !important;padding:0px;min-width: 50px;";
        var HeaderCSS_2 = "text-align: center !important;vertical-align: middle !important;padding:0px;";
        var RowsCSS = "text-align: center !important; vertical-align: middle !important;padding:0px;";
        var RowsCSS_1 = "text-align: center !important; vertical-align: middle !important;";
        var Search = [];
        var CycleCSS = "font-size: 70%; border-radius: 100%;border: solid black 1px;padding-right: 0px;padding-left: 0px;padding-top: 0px;padding-bottom: 0px;height: 13px;text-align: center;color:red;";
        $(function () {
            $('select[id*=ddlsublevel] option[value=""]').text("- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133179") %> -");
            $('select[id*=ddlSubLV2] option[value=""]').text("- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111085") %> -");

            $('select[id*=sort_type]').change(function () {
                switch_sort($(this).val());
            });

            switch_sort($('select[id*=sort_type]').val());

            $('select#sltClass').change(function () {
                getplane();
                getliststudent();
            });

            $('select#sltTerm').change(function () {
                getplane();
            });

            $('select#select_month').change(function () {
                getplaneByMonth();
            });

            //$("select[id*=ddlsublevel]").change(function () {
            //    //getplane();
            //    $('select[id*=ddlSubLV2] option[value=""]').text("- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111085") %> -");
            //});

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

        function switch_sort(sort_type) {

            switch (sort_type) {
                case "1": $("div[class*=sort_type]").hide(); $(".sort_type" + sort_type).show(); break;
                case "2": $("div[class*=sort_type]").hide(); $(".sort_type" + sort_type).show(); break;
                case "3": $("div[class*=sort_type]").hide(); $(".sort_type" + sort_type).show(); break;
            }
        }

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
            Reports_04.report_txt = $('select[id*=plane] option:selected').text();

            if ($('select[id*=sort_type]').val() === "1") {
                //dStart = $("#select_month").val() + "/1/" + ($("select[id*=select_year] option:selected").text() - 543);
                dStart = "1/" + $("#select_month").val() + "/" + ($("#select_year option:selected").text());
                Reports_04.report_txt += " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133174") %> " + $("select[id*=select_month] option:selected").text() + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %> " + $("select[id*=select_year] option:selected").text();
            }
            else if ($('select[id*=sort_type]').val() === "2") {
                Reports_04.report_txt += " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> " + $("select[id*=ddlyear] option:selected").text() + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> " + $("select[id*=semister] option:selected").text();
            } else {
                dStart = $("#txtstart3").val();
                dEnd = $("#txtend3").val();
                //if ($("#txtstart3").val() !== "") {
                //    dStart = $("#txtstart3").val().split('/')[1] + "/" + $("#txtstart3").val().split('/')[0] + "/" + $("#txtstart3").val().split('/')[2];
                //}
                //if ($("#txtend").val() !== "") {
                //    dEnd = $("#txtend").val().split('/')[1] + "/" + $("#txtend").val().split('/')[0] + "/" + $("#txtend").val().split('/')[2];
                //}
            }

            Search = {
                "term_id": YTF.GetTermID(),
                "level2_id": LCF.GetClassID(),
                "sort_type": $('select[id*=sort_type]').val(),
                "dStart": dStart != "" ? moment(dStart, 'DD/MM/YYYY').add(-543, 'years').format('MM/DD/YYYY') : "",
                "dEnd": dEnd != "" ? moment(dEnd, 'DD/MM/YYYY').add(-543, 'years').format('MM/DD/YYYY') : "",
                "plane_Id": $('#plane').val()
            };

            //File Name Reports

            switch ($('select[id*=sort_type]').val()) {
                //case "0":
                //    Reports_04.report_txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133175") %> " + $('#plane option:selected').text() + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133167") %> " + $("#txtstart").val();
                //    break;
                case "1":
                    Reports_04.report_txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133175") %> " + $('#plane option:selected').text() + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133174") %> " + $("#select_month option:selected").text() + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %> " + $("select[id*=ddlyear] option:selected").text();
                    break;
                case "2":
                    Reports_04.report_txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133175") %> " + $('#plane option:selected').text() + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133176") %> " + YTF.GetTermText() + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> " + YTF.GetYearNo();
                    break;
                case "3":
                    Reports_04.report_txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133175") %> " + $('#plane option:selected').text() + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133177") %> " + $("#txtstart3").val() + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %> " + $("#txtend3").val();
                    break;
                default:
                    return "";
            }

            //$("body").mLoading();
            PageMethods.returnlist(Search, function (e) {
                Reports_04.reports_data = $.parseJSON(e);
                if (Search.sort_type === "0") {
                    Reports_04.RenderHTML_02(table_name, export_file);
                } else {
                    Reports_04.RenderHtml(table_name, export_file);
                }
                //$("body").mLoading('hide');
            }, function (e) {
                //$("body").mLoading('hide');
            });
        }

        function getplaneByMonth() {

            if (LCF.GetClassID() == '' || LCF.GetLevelID() == '')
                return;

            $('#plane option').remove();
            $("#plane").attr('disabled', 'disabled');
            //console.log("ddlsublevel" + $("select[id*=ddlsublevel]").val());
            //console.log("ddlsublevel2" + $("select[id*=ddlSubLV2]").val());

            PageMethods.SearchPlaneByMonth(LCF.GetClassID(), $("#select_month").val(), $("#select_year").val(), function (result) {
                console.log("result" + result);
                result = $.parseJSON(result);

                $.each(result, function (index) {
                    $('#plane').append($('<option>', {
                        value: result[index].value,
                        text: result[index].text
                    }));
                });
                $("#plane").removeAttr('disabled');
                $("#plane").selectpicker('refresh');
                //$('#plane').trigger("chosen:updated");
                //$('.chosen-select-deselect').chosen({ allow_single_deselect: true });
            }, function (mgserror) {
                console.log(mgserror);
            });
        }

        function getplane() {

            if (YTF.GetTermID() == '' || LCF.GetLevelID() == '')
                return;

            $('#plane option').remove();
            $("#plane").attr('disabled', 'disabled');
            //console.log("semister" + $("select[id*=semister]").val());
            //console.log("ddlsublevel" + $("select[id*=ddlsublevel]").val());

            PageMethods.SearchPlane(YTF.GetTermID(), LCF.GetClassID(), function (result) {
                console.log("result" + result);
                result = $.parseJSON(result);

                $.each(result, function (index) {
                    $('#plane').append($('<option>', {
                        value: result[index].value,
                        text: result[index].text
                    }));
                });
                $("#plane").removeAttr('disabled');
                $("#plane").selectpicker('refresh');
                //$('#plane').trigger("chosen:updated");
                //$('.chosen-select-deselect').chosen({ allow_single_deselect: true });
            }, function (mgserror) {
                console.log(mgserror);
            });
        }
        function popupAlart() {

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

                    <div class=" row ">
                        <div class="col-md-1"></div>
                        <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %></label>
                        <div class="col-md-3 ">
                            <select id="sort_type" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105011") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206190") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105013") %></option>
                            </select>
                        </div>
                        <div class="col-md-1"></div>
                        <label class="col-md-1 col-form-label text-left"></label>
                        <div class="col-md-3 ">
                        </div>
                        <div class="col-md-2"></div>
                    </div>

                    <div class=" row sort_type1" style="display: none">
                        <div class="col-md-1"></div>
                        <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %></label>
                        <div class="col-md-3 ">
                            <select id="select_year" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                <% for (int i = DateTime.Today.Year; i >= 2015; i--)
                                    {%>
                                <option value="<%= i %>"><%= i + 543 %></option>
                                <%}%>
                            </select>
                        </div>
                        <div class="col-md-1"></div>
                        <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></label>
                        <div class="col-md-3 ">
                            <select id="select_month" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7" required>
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206188") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %></option>
                                <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %></option>
                                <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %></option>
                                <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %></option>
                                <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %></option>
                                <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %></option>
                                <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %></option>
                                <option value="11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %></option>
                                <option value="12"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %></option>
                            </select>
                        </div>
                        <div class="col-md-2"></div>
                    </div>

                    <div class="sort_type2" style="display: none">
                        <uc1:YTFilter runat="server" ID="YTFilter" />
                    </div>

                    <div class=" row sort_type3 " style="display: none">
                        <div class="col-md-1"></div>
                        <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                        <div class="col-md-3 ">
                            <div class="form-group has-successx">
                                <input type="text" id="txtstart3" name="txtstart3" class="form-control datepicker" required />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-1"></div>
                        <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                        <div class="col-md-3 ">
                            <div class="form-group has-successx">
                                <input type="text" id="txtend3" name="txtend3" class="form-control datepicker" required />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                    </div>

                    <div class=" row ">
                        <div class="col-md-1"></div>
                        <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %></label>
                        <div class="col-md-3 ">
                            <select id="plane" name="plane" disabled required class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7" data-live-search="true">
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

                    <div class="row">
                        <div class="col-md-12 text-right">
                            <a class="btn btn-fill btn-warning " href="/UpdateStatus/LearnTimeScan.aspx" style="" target="_blank">
                                <span class="material-icons">edit</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>
                            </a>
                            <button type="button" class="btn btn-fill btn-success " onclick="Reports_04.export_excel()">
                                <span class="material-icons">receipt_long</span>&nbsp;Export File
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
                    <div class="row" style="overflow-y: auto;">
                        <br />
                        <div class="col-sm-12">
                            <fieldset>
                                <asp:ListView ID="ListView1" runat="server">
                                </asp:ListView>
                                <table id="example" class="table-hover dataTable table-show-result" style="" cellspacing="0" width="100%">
                                </table>
                            </fieldset>
                        </div>
                        <fieldset class="d-none hidden" id="export_excel">
                            <table id="table_exports" class="table-hover dataTable table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                            </table>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="report-container d-none">
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
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %> :</label>
                <div class="col-md-7 col-sm-6">
                </div>
            </div>
        </div>
        <div class="row sort_type">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    :</label>
                <div class="col-md-7 col-sm-6">
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %> :</label>
                <div class="col-md-7 col-sm-6">
                </div>
            </div>
        </div>
        <div class="row sort_type">
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
        <div class="row">
            <div class="form-group col-sm-6 sort_type">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" id="txtstart" class="form-control datepicker col-md-6" readonly="readonly" />
                    <%-- <div class="input-group">
                        <input type="text" id="txtstart" readonly="true" class="form-control datepicker" /><div
                            class="input-group-addon">
                            <i class="glyphicon glyphicon-calendar"></i>
                        </div>
                    </div>--%>
                </div>
            </div>
            <div class="form-group col-sm-6 sort_type">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <%--<div class="input-group">--%>
                    <input type="text" id="txtend" class="form-control datepicker" readonly="readonly" />
                    <%--<div class="input-group-addon">
                            <i class="glyphicon glyphicon-calendar"></i>
                        </div>
                </div>--%>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %> :</label>
                <div class="col-md-7 col-sm-6">
                </div>
            </div>
            <div class="form-group col-sm-6">
                <%--    <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603047") %></label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" class='form-control' id="txtid" style="display: none;" />
                    <input type="text" class='form-control' id="txtname" />
                </div>--%>
            </div>
        </div>
        <div class="row hidden d-none">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603047") %></label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" class='form-control' id="txtid" style="display: none;" />
                    <input type="text" class='form-control' id="txtname" />
                </div>
            </div>
        </div>
        <div class="row hidden d-none">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="status" runat="server" class="form-control col-sm-6">
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" Selected="True" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>" Value="0" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>" Value="1" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>" Value="3" />
                    </asp:DropDownList>
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
        <div class='row hidden d-none' style="font-weight: bolder; font-size: 40px;">
            <br />
            <fieldset>
                <legend><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M406001") %></legend>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-success'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>
                            <br />
                        <span class='text-large' id="status01">0</span>
                    </p>
                </div>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-warning'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>
                            <br />
                        <span class='text-large' id="status02">0</span>
                    </p>
                </div>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-danger'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>
                            <br />
                        <span class='text-large' id="status03">0</span>
                    </p>
                </div>
            </fieldset>
        </div>
        <div class="row--space">
        </div>
        <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6"></div>
            <div class="col-lg-2 col-md-2 col-sm-2">
                <% if (DateTime.Now >= DateTime.Today.AddHours(13))
                    {%>
                <a class="btn btn-info button-custom btnpermission" href="/UpdateStatus/LearnTimeScan.aspx" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %></a>
                <%}
                    else
                    {%>
                <a class="btn btn-info button-custom btnpermission" href="#" onclick="alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133178") %>');"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %></a>
                <%}
                %>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2">
                <%--<div class="btn btn-success button-custom" id="exportfile">Export File</div>--%>
                <div class="btn btn-success button-custom" onclick="Reports_04.export_excel()">Export File</div>
            </div>
        </div>
        <asp:Literal ID="ltrHeaderReport" runat="server" />
        <div class="row border-bottom" style="overflow-y: auto;">
            <br />
            <div class="col-sm-12">
                <fieldset>
                    <asp:ListView ID="lvReport" runat="server">
                    </asp:ListView>
                    <table id="example" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                    </table>
                </fieldset>
            </div>
            <fieldset class="hidden d-nonex" id="export_excel">
                <table id="table_exports" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                </table>
            </fieldset>
        </div>
    </div>
</asp:Content>

