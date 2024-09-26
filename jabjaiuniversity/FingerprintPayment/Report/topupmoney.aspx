<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="topupmoney.aspx.cs" Inherits="FingerprintPayment.Report.topupmoney" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>

    <script src="/bootstrap SB2/bower_components/flot/excanvas.min.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.time.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js" type="text/javascript"></script>
    <link href="../Styles/SettingDialog.css" rel="stylesheet" />
    <script src="ScriptReport.js" type="text/javascript"></script>
       <script src="//cdnjs.cloudflare.com/ajax/libs/dompurify/2.4.0/purify.min.js"></script>
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script src="Script/topupmoney.js?v=<%= DateTime.Now.ToString("ddMMyyyyHHmm")%>" type="text/javascript"></script>
        <script src="/Content/Material/assets/js/plugins/sweetalert2.js"></script>
   
    <style type="text/css">
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
        /*.sort_type {
            display: none;
        }*/

        .btn_red.active {
            background-color: #337AB7;
        }

        .btn_red {
            min-width: 70px;
        }

        .swal2-popup{
            width :50em !important;
        }
        .swal2-header{
            font-size: 1.7rem !important;
        }
    </style>
    <script src="../Scripts/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        var Report_topupmoney = new report_topupmoney();
        var availableValueEmployees = [];
        $(function () {
                   <% 
        string jsonData = "";
        var q1 = reports_data(new Search { sort_type = 0, dEnd = DateTime.Today, dStart = DateTime.Today }) as header_reports;

        foreach (var data in q1.data as List<views01>)
        {
            jsonData += (string.IsNullOrEmpty(jsonData) ? "" : ",") + "{ \"lable\": \"" + data.lable + "\", \"mobile\": " + data.mobile + ",\"website\":" + data.website + ",\"KIOSK\":" + data.KIOSK + " }";
        }
        %>
            var report_data = [<%= jsonData %>]
            Chart_Reports("<%= q1.header_text %>", report_data)

            $("#btncontent_1").click(function () {
                $("#content_1").show();
                $("#content_0").hide();
                qurey_detail(<%= "'" +  DateTime.Today.ToString("dd/MM/yyyy") + "'" %>);
            });

            $(".datepicker").datepicker({
                format: 'dd/mm/yyyy',
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

            showsort_type();
            $("#sort_type").change(function () {
                showsort_type();
            })

            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=teacher",
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].sName + ' ' + objjson[index].sLastname,
                            value: objjson[index].sEmp
                        };
                        availableValueEmployees[index] = newObject;
                    });
                }
            });

            $('#txtemployees').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                source: function (request, response) {
                    results = $.ui.autocomplete.filter(availableValueEmployees, request.term);
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("#txtemployees").val(ui.item.label);
                    $("#txtemployees_id").val(ui.item.value);
                    //getbalance(ui.item.value, "1");
                },
                focus: function (event, ui) {
                    event.preventDefault();
                },
                change: function (event, ui) {
                    if (ui.item == null) {
                        $("#txtemployees_id").val("");

                    }
                }
            });
        });

        function Chart_Reports(header_text, report_data) {

            $(".report_header").html(header_text)
            //BAR CHART
            $("#bar-chart").html("");
            var bar = new Morris.Line({
                element: 'bar-chart',
                resize: true,
                data: report_data,
                barColors: ['#337AB7', '#FFAA18'],
                xkey: 'lable',
                ykeys: ['mobile', 'website', 'KIOSK'],
                labels: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603053") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603054") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603055") %>'],
                hideHover: 'auto',
                parseTime: false,
                axes: true
            });
            $("body").mLoading('hide');
        }

        function SearchData() {
            var dStart = "", dEnd = "";
            var time1 = "", time2 = "";
            switch ($("#sort_type").val()) {
                case "0": dStart = <%= "'" +  DateTime.Today.ToString("MM/dd/yyyy")  + "'" %>; break;
                case "1": dStart = $("#select_month").val() + '/01/' + $("#select_year1").val(); break;
                case "2": dStart = '01/01/' + $("#select_year2").val(); break;
                case "3":
                    dStart = getDate($("#txtstart").val());
                    dEnd = ($("#txtend").val() == "" ? getDate($("#txtstart").val()) : getDate($("#txtend").val()));
                    if (dStart == dEnd) {
                        time1 = $("#txtTime1").val();
                        time2 = $("#txtTime2").val();
                    }
                    else {
                        var t1 = $("#txtTime1").val();
                        var t2 = $("#txtTime2").val();
                        if (t1 || t2) {
                            Swal.fire({
                                type: 'info',
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133218") %>',
                            });
                            $("#txtTime1").val('');
                            $("#txtTime2").val('');
                        }
                       
                    }
                    break;
            }

            var data = {
                "sort_type": $("#sort_type").val(),
                "dStart": dStart,
                "dEnd": dEnd,
                "emp_id": $("#txtemployees_id").val(),
                "type": $("#sort").val(),
                "time1": time1,
                "time2": time2,
            };

            Report_topupmoney.searchData = data;
            $("body").mLoading();
            if ($("#sort_type").val() != "3" || data.dEnd != data.dStart) {
                PageMethods.reports_data(data, function (e) {
                    console.log(e.data);
                    Report_topupmoney.reports_data = e;
                    Report_topupmoney.RenderHtml("example", false);
                });
            } else {
                PageMethods.reports_detail(data, function (e) {
                    console.log(e.data);
                    Report_topupmoney.reports_data = e;
                    Report_topupmoney.RenderHtml_Detail("example", false);
                });
            }
        }

        function showsort_type() {
            $("div[class*=sort_type]").hide();
            var type = $("#sort_type").val();
            $(".sort_type" + type).show();

            //switch ($("#sort_type").val()) {
            //    case "1": $(sort_type[0]).show(); $(sort_type[1]).show(); break;
            //    case "2": $(sort_type[1]).show(); break;
            //    case "3": $(sort_type[2]).show(); break;
            //}
        }

        function qurey_month(dStart) {
            var data = { "sort_type": 1, "dStart": getDate(dStart), "emp_id": $("#txtemployees_id").val(), "type": $("#sort").val() };

            $("body").mLoading();
            PageMethods.reports_data(data, function (e) {
                console.log(e.data);
                Report_topupmoney.reports_data = e;
                Report_topupmoney.RenderHtml("example", false);
            });
        }

        function qurey_data(sort_type, dStart, dEnd) {
            var data = { "sort_type": sort_type, "dStart": dStart, "dEnd": dEnd, "emp_id": $("#txtemployees_id").val(), "type": $("#sort").val() };
            $("body").mLoading();
            PageMethods.reports_data(data, function (e) {
                console.log(e.data);
                Chart_Reports(e.header_text, e.data);
            });
        }

        function qurey_detail(dStart) {
            var data = { "sort_type": 3, "dStart": getDate(dStart), "emp_id": $("#txtemployees_id").val(), "type": $("#sort").val() };
            $("body").mLoading();
            PageMethods.reports_detail(data, function (e) {
                console.log(e.data);
                Report_topupmoney.searchData.dStart = dStart;
                Report_topupmoney.searchData.dEnd = dStart;
                Report_topupmoney.reports_data = e;
                Report_topupmoney.RenderHtml_Detail("example", false);
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />
    <asp:HiddenField ID="hdfschoolname" runat="server" />
    <div class="full-card box-content" id="content_0">
        <div class="row">
            <h1 class="center report_header"></h1>
            <div class="pull-right">
                <div class="btn-group" data-toggle="buttons">
                    <label class="btn btn-default btn_red active" onclick="qurey_data('0', <%= "'" + DateTime.Today.ToString("MM/dd/yyyy") + "'" %>, <%= "'" + DateTime.Today.ToString("MM/dd/yyyy") + "'" %>)">
                        <input type="radio" name="options" id="option1" autocomplete="off" checked>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603007") %>
                    </label>
                    <label class="btn btn-default btn_red" onclick="qurey_data('1', <%=  "'" +  DateTime.Today.ToString("MM/dd/yyyy") + "'" %>, <%= "'" + DateTime.Today.ToString("MM/dd/yyyy") + "'" %>)">
                        <input type="radio" name="options" id="option2" autocomplete="off">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>
                    </label>
                    <label class="btn btn-default btn_red" onclick="qurey_data('2', <%= "'" +  DateTime.Today.ToString("MM/dd/yyyy") %>', <%= "'" + DateTime.Today.ToString("MM/dd/yyyy") + "'" %>)">
                        <input type="radio" name="options" id="option3" autocomplete="off">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>
                    </label>
                </div>
            </div>
        </div>
        <div class="box-body chart-responsive">
            <div class="chart" id="bar-chart" style="height: 300px;"></div>
        </div>
        <div class="row--space">
        </div>
        <div class="row">
            <div class="col-md-12" id="table_0">
                <table>
                    <tr>
                        <td>
                            <div class="col-md-12" style="background-color: #FFAA18; min-height: 30px; border-radius: 30px;"></div>
                        </td>
                        <td style="padding: 5px 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603053") %></td>
                        <td>
                            <div class="col-md-12" style="background-color: #337AB7; min-height: 30px; border-radius: 30px;"></div>
                        </td>
                        <td style="padding: 5px 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603054") %></td>
                        <td>
                            <div class="col-md-12" style="background-color: #7a92a3; min-height: 30px; border-radius: 30px;"></div>
                        </td>
                        <td style="padding: 5px 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603055") %></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row--space">
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="btn btn-success col-md-12" id="btncontent_1">
                   <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601086") %>
                </div>
            </div>
        </div>
    </div>
    <div class="full-card box-content report-container" id="content_1" style="display: none;">
        <div class="row student">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603056") %></label>
                <div class="col-md-7 col-sm-6">
                    <select id="sort" class="form-control">
                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                        <option value="WB"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603057") %></option>
                        <option value="MB"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603058") %></option>
                        <option value="KIOSK"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603055") %></option>
                    </select>
                </div>
            </div>
            <div class="form-group col-sm-6 hidden">
            </div>
        </div>
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <select id="sort_type" class="form-control">
                        <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105013") %></option>
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105011") %></option>
                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206508") %></option>
                    </select>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603046") %></label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" class='form-control' id="txtemployees_id" style="display: none;" />
                    <input type="text" class='form-control' id="txtemployees" />
                </div>
            </div>
        </div>
        <div class="row sort_type1">
            <div class="form-group col-sm-6 ">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <select id="select_month" class="form-control">
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
            </div>
            <div class="form-group col-sm-6 ">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <select id="select_year1" class="form-control">
                        <% for (int i = 2018; i <= DateTime.Now.Year; i++)
                            {%>
                        <option value="<%= i %>"><%= i+543 %></option>
                        <%}%>
                    </select>
                </div>
            </div>
        </div>

        <div class="row sort_type2">
            <div class="form-group col-sm-6 ">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <select id="select_year2" class="form-control">
                        <% for (int i = 2018; i <= DateTime.Now.Year; i++)
                            {%>
                        <option value="<%= i %>"><%= i+543 %></option>
                        <%}%>
                    </select>
                </div>
            </div>

            <div class="form-group col-sm-6 ">
            </div>
        </div>

        <div class="row sort_type3">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" id="txtstart" class="form-control datepicker col-md-6" readonly="readonly" />

                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" id="txtend" class="form-control datepicker" readonly="readonly" />
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603059") %></label>
                <div class="col-md-7 col-sm-6">
                    <input type="time" id="txtTime1" class="form-control "  />

                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603060") %></label>
                <div class="col-md-7 col-sm-6">
                    <input type="time" id="txtTime2" class="form-control " />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <input type="button" class="btn btn-primary button-custom" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="SearchData();" />
            </div>
        </div>
        <div class="row--space">
        </div>
        <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8">
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2">
                <div class="btn btn-success button-custom" id="exportfile" onclick="Report_topupmoney.export_excel()">Export File</div>
            </div>
        </div>
        <asp:Literal ID="ltrHeaderReport" runat="server" />
        <div class="chart" id="bar-chart-reports" style="height: 300px;"></div>
        <div class="row border-bottom">
            <br />
            <div class="col-sm-12">
                <fieldset>
                    <asp:ListView ID="lvReport" runat="server">
                    </asp:ListView>
                    <table id="example" class="table table-condensed table-bordered table-show-result"
                        cellspacing="0" width="100%">
                    </table>
                </fieldset>
            </div>
        </div>
    </div>
    <fieldset class="hidden" id="export_excel">
        <table id="table_exports" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
        </table>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
