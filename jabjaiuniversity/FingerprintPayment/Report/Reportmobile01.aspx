<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="Reportmobile01.aspx.cs" Inherits="FingerprintPayment.Report.Reportmobile01" %>

<%@ Register Src="~/UserControls/YTFilter.ascx" TagPrefix="uc1" TagName="YTFilter" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link href="/bootstrap SB2/bower_components/morrisjs/morris.css" rel="stylesheet" />
    <link href="../Styles/SettingDialog.css" rel="stylesheet" />
    <%-- <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>--%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">

    <%-- <script src="/app/Reports/Come2school/ReportCome2SchoolStudentJS.js" type="text/javascript"></script>--%>

    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>
    <script src="/bootstrap SB2/bower_components/raphael/raphael-min.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/morrisjs/morris.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/excanvas.min.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.time.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js" type="text/javascript"></script>

    <script src="ScriptReport.js" type="text/javascript"></script>
    <script src="Script/Reportmobile01.js?<%= DateTime.Now.ToString("ddMMyyyyHHmm") %>" type="text/javascript"></script>
    <script src="../Scripts/jquery-dateformat.js" type="text/javascript"></script>

    <script type="text/javascript">
        var Reports_01 = new reports_01();
        var schoolData = {
            Year: '<%= SchoolData.Year??"0" %>', Term: '<%= SchoolData.Term??"0" %>',
            SchoolHeadName: '<%= SchoolData.SchoolHeadName  %>'
        };
        function Chart_Reports() {
            <% var q1 = report02UsersView01(new Search { dStart = DateTime.Today, sort_type = 0 }).FirstOrDefault();%>

            var _f_0 = <%=q1 ==null?0: q1.female_status_0 + q1.female_status_1 +q1.female_status_2 %>;
            var _f_1 =  <%= q1 ==null?0:q1.female_status_3+q1.female_status_4+q1.female_status_5 %>;
            var _f_2 =  <%= q1 ==null?0:q1.female_status_6 %>;
            var _m_0 =  <%= q1 ==null?0: q1.male_status_0 + q1.male_status_1+q1.male_status_2 %>;
            var _m_1 =  <%= q1 ==null?0:q1.male_status_3+q1.male_status_4+q1.male_status_5 %>;
            var _m_2 =  <%= q1 ==null?0:q1.male_status_6 %>;

            var m_0 =  <%=q1 ==null?0: q1.male_status_0 %>;
            var m_1 =  <%=q1 ==null?0: q1.male_status_1 %>;
            var m_2 =  <%=q1 ==null?0: q1.male_status_2 %>;

            var f_0 =  <%=q1 ==null?0: q1.female_status_0 %>;
            var f_1 =  <%=q1 ==null?0: q1.female_status_1  %>;
            var f_2 =  <%=q1 ==null?0: q1.female_status_2 %>;

            var _m =  <%= q1 ==null?0:q1.male_status_0 + q1.male_status_1 + q1.male_status_2+q1.male_status_3+q1.male_status_4+q1.male_status_5 +q1.male_status_6%> ;
            var _f =  <%= q1 ==null?0: q1.female_status_0 + q1.female_status_1 + q1.female_status_2+q1.female_status_3+q1.female_status_4+q1.female_status_5 +q1.female_status_6%>;

            //BAR CHART
            var bar = new Morris.Bar({
                element: 'bar-chart',
                resize: true,
                data: [
                    { y: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303004") %>', a: _m, b: _f, c: (_m + _f) },
                    { y: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303005") %>', a: _m_0, b: _f_0, c: _f_0 + _m_0 },
                    { y: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303006") %>', a: _m_1, b: _f_1, c: _f_1 + _m_1 },
                    { y: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301030") %>', a: _m_2, b: _f_2, c: _f_2 + _m_2 },
                ],
                barColors: ['#449d44', '#286090', '#ef6e6a'],
                xkey: 'y',
                ykeys: ['a', 'b', 'c'],
                labels: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %>'],
                hideHover: 'auto',
                axes: true
            });
            var _Html = "<br/><table class='table-hover dataTable table-show-result' cellspacing='0' width='100%'>";

            _Html += "<thead><tr id='headder' style='font-weight: bold; font-size:20px;text-align: center; '>"
                + "<th id='headder' class='text-center' colspan='3'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303004") %>"
                + "<th id='headder' class='text-center' colspan='3'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303005") %>  <i class='fa fa-question-circle' style='font-size: 18px;' data-original-title='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133163") %>' data-placement='top' data-toggle='tooltip'>"
                + "<th id='headder' class='text-center' colspan='3'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303006") %> <i class='fa fa-question-circle' style='font-size: 18px;' data-original-title='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133164") %>' data-placement='top' data-toggle='tooltip'>"
                + "<th id='headder' class='text-center' colspan='3'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301030") %>"
                + "<th id='headder' class='text-center' rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01327") %>";

            _Html += "<tr id='headder' style='font-weight: bold; font-size:18px;text-align: center;'>"
                + "<th class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %><th class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %><th class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %>"
                + "<th class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %><th class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %><th class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %>"
                + "<th class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %><th class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %><th class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %>"
                + "<th class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %><th class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %><th class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %>";
            _Html += "</thead>";
            _Html += "<tbody>";
            _Html += "<tr>"
                + "<td class='text-center'>" + _m + "<td class='text-center'>" + _f + "<td class='text-center'>" + (_m + _f)
                + "<td class='text-center'>" + _m_0 + "<td class='text-center'>" + _f_0 + "<td class='text-center'>" + (_m_0 + _f_0)
                + "<td class='text-center'>" + _m_1 + "<td class='text-center'>" + _f_1 + "<td class='text-center'>" + (_m_1 + _f_1)
                + "<td class='text-center'>" + _m_2 + "<td class='text-center'>" + _f_2 + "<td class='text-center'>" + (_m_2 + _f_2)
                + "<td class='text-center'>" + percent((_m + _f), (_m_0 + _f_0));
            _Html += "</tbody>";
            $("#table_0").html(_Html);
        }
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


            $("#btncontent_1").click(function () {
                $("#content_0").hide();
                $("#content_1").show();
                Reports02('<%= DateTime.Today.ToString("dd/MM/yyyy")%>');
            });

            $('select[id*=sort_type]').change(function () {
                switch_sort($(this).val());
            });
            switch_sort($('select[id*=sort_type]').val());

            Chart_Reports();

            $("#select_Report").change(function () {
                switch ($(this).val()) {
                    case "0": Reports_01.RenderHtml02_01("example", false);
                        $("#exportPDF_file").hide();
                        break;
                    case "1": Reports_01.RenderHtml02_02("example", false);
                        $("#exportPDF_file").hide();
                        break;
                    case "2": Reports_01.RenderHtml02_03("example", false); $("#exportPDF_file").show();
                        break;
                    case "3": Reports_01.RenderHtml02_04("example", false);
                        $("#exportPDF_file").hide();
                        break;
                    case "4": Reports_01.RenderHtml02_05("example", false);
                        $("#exportPDF_file").show();
                        break;
                }
            });

            $("#exportfile").click(function () {
                Reports_01.export_excel();
            });
        });

        function switch_sort(sort_type) {

            switch (sort_type) {
                case "0": $("div[class*=sort_type]").hide(); $(".sort_type" + sort_type).show(); break;
                case "1": $("div[class*=sort_type]").hide(); $(".sort_type" + sort_type).show(); break;
                case "2": $("div[class*=sort_type]").hide(); $(".sort_type" + sort_type).show(); break;
                case "3": $("div[class*=sort_type]").hide(); $(".sort_type" + sort_type).show(); break;
            }
        }

        function SearchData() {
            new Date().getFullYear();
            var dt = new Date();
            Search = {
                "sort_type": $("#sort_type").val(),
                "term_id": YTF.GetTermID(),
                "level_id": $('select[id*=ddlsublevel]').val(),
                "dStart": '',//getDate($("#txtstart").val()),
                "dEnd": '',//getDate($("#txtend").val())
            };

            if (Search.sort_type == "1") {
                Search.dStart = ($("#select_month").val()  + "/1/" + new Date().getFullYear());
                Search.dEnd = Search.dStart;
            }
            else if (Search.sort_type == "3") {
                if ($("#txtstart3").val() != '') {
                    Search.dStart = $("#txtstart3").data("DateTimePicker").date().format('MM/DD/YYYY');
                }
                else {
                    Search.dStart = moment().format('MM/DD/YYYY');
                }
                Search.dEnd = $("#txtend3").data("DateTimePicker").date().format('MM/DD/YYYY');
            }
            else if (Search.sort_type == "0") {
                if ($("#txtstart0").val() != '') {
                    Search.dStart = $("#txtstart0").data("DateTimePicker").date().format('MM/DD/YYYY');
                }
                else {
                    Search.dStart = moment().format('MM/DD/YYYY');
                }
                Search.dEnd = Search.dStart;
            }
            else {
                Search.dStart = moment().format('MM/DD/YYYY');
                Search.dEnd = Search.dStart;
            }
            //.add(-543, 'years')
            //Search.dStart = Search.dStart;
            //Search.dEnd = Search.dEnd;

            if (Search.sort_type !== "0") {
                var Header = $("#myHeader");
                var HtmlTable = $("#myTable");
                Header.html("");
                HtmlTable.html("");
              
                $("body").mLoading();               
                PageMethods.report02UsersView01(Search, function (response) {
                    Reports_01.reports_data = response;
                    Reports_01.RenderHtml01("example", false);
                    $(".w3-button.w3-teal").hide();
                    $("body").mLoading("hide");
                });
            } else {
                var dayreports = $("#txtstart0").val() === "" ? moment().format('DD/MM/YYYY') : $("#txtstart0").data("DateTimePicker").date().format('DD/MM/YYYY');
                Reports02(dayreports);
            }
        }

        function getDate(strDate) {
            if (strDate.split('/').length !== 3) return "";
            return strDate.split('/')[1] + "/" + strDate.split('/')[0] + "/" + strDate.split('/')[2];
        }

        function percent(maxuser, countstatus) {
            if (maxuser === 0) return 0
            else return ((100 * countstatus) / maxuser).toFixed(2);
        }

        function w3_open() {
            document.getElementById("mySidebar").style.display = "block";
            document.getElementById("myOverlay").style.display = "block";
        }
        function w3_close() {
            document.getElementById("mySidebar").style.display = "none";
            document.getElementById("myOverlay").style.display = "none";
        }


        function Reports02(day) {
            $(".w3-button").show();
            ReportDate = moment(day, 'DD/MM/YYYY').format('DD/MM/') + "" + moment(day, 'DD/MM/YYYY').add(543, 'years').format('YYYY');

            Search = {
                "sort_type": "0",
                "term_id": YTF.GetTermID(),
                "level_id": $('select[id*=ddlsublevel]').val(),
                "dStart": moment(day, 'DD/MM/YYYY').format('MM/DD/YYYY'),
                "dEnd": "",
                //moment(day, 'DD/MM/YYYY').add(-543, 'years').format('MM/DD/YYYY'),
            };

            $("body").mLoading();             
            PageMethods.report02UsersView02(Search, function (response) {
                if (response.status === "Session Time Out") location.reload();
                Reports_01.reports_data = response;
                if ($("#select_Report").val() === "0") {
                    Reports_01.RenderHtml02_01("example", false);
                } else if ($("#select_Report").val() === "1") {
                    Reports_01.RenderHtml02_02("example", false);
                } else if ($("#select_Report").val() === "2") {
                    Reports_01.RenderHtml02_03("example", false);
                } else if ($("#select_Report").val() === "3") {
                    Reports_01.RenderHtml02_04("example", false);
                } else if ($("#select_Report").val() === "4") {
                    Reports_01.RenderHtml02_05("example", false);
                }
                $("body").mLoading("hide");
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303002") %> 
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
                        <div class="full-card box-content" id="content_0" style="padding: 10px 20px;">
                            <div class="row">
                                <div class="col-md-12">
                                    <h3 class="text-center "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303003") %> <%= DateTime.Today.ToString("dd/MM/yyyy",new System.Globalization.CultureInfo("th-th")) %></h3>
                                </div>
                            </div>
                            <div class="box-body chart-responsive">
                                <div class="chart" id="bar-chart" style="height: 300px;"></div>
                            </div>
                            <div class="row--space">
                            </div>
                            <div class="row">
                                <div class="col-md-12" id="table_0">
                                </div>
                            </div>
                            <div class="row--space">
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <br />
                                    <div class="btn btn-success col-md-12" id="btncontent_1">
                                       <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601086") %>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="report-container" id="content_1" style="display: none;">

                            <div class="row">
                                <div class="col-md-1"></div>
                                <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                                <div class="col-md-3 ">
                                    <asp:DropDownList ID="ddlsublevel" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-1 col-form-label text-left  d-none"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                                <div class="col-md-3  d-none">
                                    <asp:DropDownList ID="ddlSubLV2" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2"></div>
                            </div>

                            <div class="row">
                                <div class="col-md-1"></div>
                                <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %></label>
                                <div class="col-md-3 ">
                                    <select id="sort_type" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105010") %></option>
                                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105011") %></option>
                                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206190") %></option>
                                        <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105013") %></option>
                                    </select>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-1 col-form-label text-left  d-none"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301008") %> </label>
                                <div class="col-md-3  d-none">
                                    <input type="text" class='form-control' id="txtid" style="display: none;" />
                                    <input type="text" class='form-control' id="txtname" />
                                </div>
                                <div class="col-md-2"></div>
                            </div>

                            <div class="row sort_type0" style="display: none">
                                <div class="col-md-1"></div>
                                <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                                <div class="col-md-3 ">
                                    <div class="form-group has-successx">
                                        <input type="text" id="txtstart0" name="txtstart0" class="form-control datepicker" required />
                                        <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                            <i class="material-icons">event</i>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-1 col-form-label text-left"></label>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-2"></div>
                            </div>

                            <div class="row sort_type1 " style="display: none">
                                <div class="col-md-1"></div>
                                <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></label>
                                <div class="col-md-3 ">
                                    <select id="select_month" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
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
                                <div class="col-md-1"></div>
                                <label class="col-md-1 col-form-label text-left"></label>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-2"></div>
                            </div>

                            <div class=" sort_type2" style="display: none">
                                <uc1:YTFilter runat="server" ID="YTFilter" />
                            </div>

                            <div class=" row sort_type3" style="display: none">
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



                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <br />
                                    <button type="button" onclick="SearchData();" class="btn btn-fill btn-info">
                                        <span class="material-icons">search</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                    </button>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12 text-right">
                                    <button type="button" class="btn btn-fill btn-success " id="exportfile">
                                        <span class="material-icons">receipt_long</span>&nbsp;Export File
                                    </button>
                                    <button type="button" class="btn btn-fill btn-warning " id="exportPDF_file" onclick="ExportPDF_03()" style="display:none;">
                                        <span class="material-icons">receipt_long</span>&nbsp;Export PDF File
                                    </button>
                                </div>

                            </div>


                        </div>
                        <asp:Literal ID="ltrHeaderReport" runat="server" />
                        <div class="row">
                            <br />
                            <div class="col-sm-12">
                                <fieldset>
                                    <asp:ListView ID="lvReport" runat="server">
                                    </asp:ListView>
                                    <table id="example" class="table-hover dataTable  table-show-result"
                                        cellspacing="0" width="100%">
                                    </table>
                                </fieldset>
                            </div>
                        </div>

                        <fieldset class="d-none" id="export_excel">
                            <table id="table_exports" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                            </table>
                        </fieldset>
                        <div>
                            <div class="w3-button w3-teal w3-right" style="position: fixed; top: 110px; right: 10px; z-index: 9999; border: 1px solid black; display: none;" onclick="w3_open()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303007") %></div>
                        </div>
                        <div class="w3-sidebar w3-bar-block w3-card w3-animate-right fxll-card" style="display: none; right: 0; z-index: 5; width: 300px; top: 120px; height: 150px;" id="mySidebar">

                            <div class="row" style="padding: 15px 10px; margin: 0 auto">
                                <div class="col-md-12 text-center">
                                    <div class="">
                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133165") %> </label>
                                    </div>
                                </div>
                                <div class="col-md-12 text-center">
                                    <select id="select_Report" class="form-control selectpickerx" style="appearance: listbox;">
                                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105043") %></option>
                                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105044") %></option>
                                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132513") %></option>
                                        <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132882") %></option>
                                        <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132883") %></option>
                                    </select>
                                </div>
                            </div>

                        </div>
                        <div class="w3-overlay w3-animate-opacity" onclick="w3_close()" style="cursor: pointer" id="myOverlay"></div>
                    </div>
                </div>
            </div>
        </div>
    </form>


</asp:Content>
