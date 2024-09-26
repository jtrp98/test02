<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="UserDailyBalance.aspx.cs" Inherits="FingerprintPayment.Report.UserDailyBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />
    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>

    <%--  <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>--%>

    <%-- <script src="/bootstrap SB2/bower_components/flot/excanvas.min.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.time.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js" type="text/javascript"></script>--%>
    <link href="../Styles/SettingDialog.css" rel="stylesheet" />
    <script src="ScriptReport.js" type="text/javascript"></script>
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script src="Script/reportTransLog.js?d=<%= DateTime.Now.ToString("ddMMyyyyHH") %>" type="text/javascript"></script>
    <script src="../Scripts/FileSaver.js" type="text/javascript"></script>

    <link href="/Content/jquery-confirm.css" rel="stylesheet" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>

    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" type="text/javascript"></script>
    <style type="text/css">
        .fa.fa-print.btn-print {
            cursor: pointer;
        }

        @media (max-width: 999px) {
            .report-container {
                font-size: 18px;
            }

            .fa.fa-print.btn-print {
                cursor: pointer;
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
    </style>
    <script type="text/javascript">

        var availableValueUsers = [];
        var availableValueUsers2 = [];
        var Reports = new report_sales();
        $(function () {
            $("#btncontent_1").click(function () {
                //$("body").mLoading();
                $("#content_1").show();
                $("#content_0").hide();
                //qurey_detail(date)
            });

            $(".datepicker").datepicker();
            //showsort_type();
            //$("#sort_type").change(function () {
            //    showsort_type();
            //})

            $("#type").change(function () {
                var t = $(this).val();

                $('.typewrap').hide();

                switch (t) {
                    case "1": $('#typewrap1').show(); break;
                    case "2": $('#typewrap2').show(); break;
                  
                }
            });

            $('#txtname1').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                maxLength: 10,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                //source: function (request, response) {
                //    var results = $.ui.autocomplete.filter(availableValuestudent, request.term);
                //    response(results.slice(0, 10));
                //},
                source: lightwellBuyer,
                select: function (event, ui) {
                    event.preventDefault();
                    $("input[id*=txtname1]").val(ui.item.label);
                    $("#txtid1").val(ui.item.value);
                    console.log(ui.item);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });


            $('#txtname2').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                maxLength: 10,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                //source: function (request, response) {
                //    var results = $.ui.autocomplete.filter(availableValuestudent, request.term);
                //    response(results.slice(0, 10));
                //},
                source: lightwellBuyer2,
                select: function (event, ui) {
                    event.preventDefault();
                    $("input[id*=txtname2]").val(ui.item.label);
                    $("#txtid2").val(ui.item.value);
                    console.log(ui.item);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });

            $.ajax({
                url: "/App_Logic/dataGenericListData.ashx?mode=liststudent&nelevel=&nsublevel=",
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].sName,
                            value: objjson[index].sID,
                            code: objjson[index].studentid,
                        };

                        availableValueUsers.push(newObject);
                    });
                }
            });

            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=teacher",
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].sName + ' ' + objjson[index].sLastname,
                            value: objjson[index].sEmp,
                            code: objjson[index].Code,
                        };

                        availableValueUsers.push(newObject);
                    });
                }
            });

            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=backupcard",
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].name,
                            value: objjson[index].id,
                            money: objjson[index].money,
                            insurance: objjson[index].insurance,
                        };

                        availableValueUsers2.push(newObject);
                    });
                }
            });
        });

        function lightwellBuyer(request, response) {
            function hasMatch(s) {
                s += '';
                return s.toLowerCase().indexOf((request.term + '').toLowerCase()) !== -1;
            }
            var i, l, obj, matches = [];

            if (request.term === "") {
                response([]);
                return;
            }

            var availableValue = [];
            availableValue = availableValueUsers;
            for (i = 0, l = availableValue.length; i < l; i++) {
                obj = availableValue[i];
                //if (obj.code != null && obj.code != undefined && obj.code != "") {
                if (hasMatch(obj.label) || hasMatch(obj.code)) {
                    matches.push(obj);
                }
                //}
            }
            response(matches.slice(0, 10));
        }

        function lightwellBuyer2(request, response) {

            function hasMatch(s) {
                s += '';
                return s.toLowerCase().indexOf((request.term + '').toLowerCase()) !== -1;
            }
            var i, l, obj, matches = [];

            if (request.term === "") {
                response([]);
                return;
            }

            var availableValue = [];
            availableValue = availableValueUsers2;
            for (i = 0, l = availableValue.length; i < l; i++) {
                obj = availableValue[i];
                //if (obj.code != null && obj.code != undefined && obj.code != "") {
                if (hasMatch(obj.label)) {
                    matches.push(obj);
                }
                //}
            }
            response(matches.slice(0, 10));
        }

        function SearchData() {
            var t = $('#type').val();
            if (t == '1') {
                if ($("#txtid1").val() == '')
                    return;

                if ($("#txtstart").val() == '')
                    return;

                var dStart = "", dEnd = "";

                dStart = getDate($("#txtstart").val());
                dEnd = ($("#txtend").val() == "" ? getDate($("#txtstart").val()) : getDate($("#txtend").val()));

                //shop_id = $("#shop_id").val();
                var data = {
                    //"sort_type": $("#sort_type").val(),
                    "dStart": dStart,
                    "dEnd": dEnd,
                    //"shop_id": shop_id,
                    "user_id": $("#txtid1").val(),
                    //"emp_id": $("#txtemployees_id").val(),
                    //"user_id": $("#txtuser_id").val(),
                };

                searchData = data;

                var dt = $('#lst-data').DataTable({
                    "processing": true,
                    /*  "serverSide": true,*/
                    "destroy": true,
                    "info": false,
                    paging: true,
                    "pageLength": 50,
                    "lengthChange": false,
                    searching: false,

                    ajax: {
                        url: "UserDailyBalance.aspx/reports_detail",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        "dataSrc": function (r) {
                            return r.d.data;
                        },
                        'data': function (d) {

                            //d.search = {
                            //    'type': type,
                            //    'shop_id': shop_id,
                            //    'sdate1': dStart,
                            //    'sdate2': dEnd,
                            //    'level': level,
                            //    'room': room,
                            //};
                            d.search = data;

                            return JSON.stringify(d);
                        },
                    },

                    dom: 'Bfrtip',
                    buttons: [

                        {
                            extend: 'excel',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133151") %> ' + $("#date1").val() + ' - ' + $("#date2").val(),
                            exportOptions: {
                                columns: [0, 1, 2, 3, 4, 5],
                                //order: [[7, "asc"]]
                            },
                            customize: function (xlsx) {
                                var sheet = xlsx.xl.worksheets['sheet1.xml'];
                                //$('row c[r=A1]', sheet).attr('s', '2').attr('s', '51');
                                //$('c[r=A1] t', sheet).text('Custom text');
                            }
                        }
                    ],

                    "columns": [
                        //{ "title": 'No', data: 'index', "class": "text-center", "width": "5%" },                       
                        { "title": "BusinessDate", data: 'DateStr', "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504013") %>", data: 'OpeningBalance', "class": "text-center", "width": "12%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01428") %>", data: 'TotalTopUp', "class": "text-center", "width": "12%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133220") %>", data: 'TotalCancelTopUp', "class": "text-center", "width": "12%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603062") %>", data: 'TotalWithDraw', "class": "text-center", "width": "12%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133221") %>", data: 'TotalCancelWithDraw', "class": "text-center", "width": "12%" },
                        { "title": "ยอดซื้อ", data: 'TotalSales', "class": "text-center", "width": "12%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133222") %>", data: 'TotalCancelSales', "class": "text-center", "width": "12%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106151") %>", data: 'Balance', "class": "text-center", "width": "12%" },
                        /* { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>", data: 'count', "class": "text-center", "width": "10%" },*/
                    ],
                    "order": [[0, 'asc']]
                });

                //$("body").mLoading();
                //PageMethods.reports_detail(data, function (e) {
                //    //console.log(e.data);
                //    Reports.reports_data = e;
                //    Reports.RenderHtml("example", false);
                //});
            }
            else if (t == '2') {
                if ($("#txtid2").val() == '')
                    return;

                if ($("#txtstart").val() == '')
                    return;

                var dStart = "", dEnd = "";

                dStart = getDate($("#txtstart").val());
                dEnd = ($("#txtend").val() == "" ? getDate($("#txtstart").val()) : getDate($("#txtend").val()));

                //shop_id = $("#shop_id").val();
                var data = {
                    "dStart": dStart,
                    "dEnd": dEnd,
                    "card_id": $("#txtid2").val(),
                };

                searchData = data;
                $("body").mLoading();
                PageMethods.ReportBackupCard(data, function (e) {
                    //console.log(e.data);
                    Reports.reports_data = e;
                    Reports.RenderHtml2("example", false);
                    $("body").mLoading("hide");
                });
            }
         
        }

        var searchData = [];
        function showsort_type() {
            var sort_type = $(".sort_type");
            $(".sort_type").hide();
            switch ($("#sort_type").val()) {
                case "1": $(sort_type[0]).show(); $(sort_type[1]).show(); break;
                case "2": $(sort_type[1]).show(); break;
                case "3": $(sort_type[2]).show(); break;
            }
        }

        function qurey_month(dStart) {
            var data = { "sort_type": 1, "dStart": getDate(dStart), "shop_id": shop_id, "user_id": $("#txtid").val(), "emp_id": $("#txtemployees_id").val() };

            $("body").mLoading();
            PageMethods.reports_detail(data, function (e) {
                console.log(e.data);
                Reports.reports_data = e;
                Reports.RenderHtml("example", false);
            });
        }

        function qurey_data(sort_type, dStart, dEnd) {
            var data = { "sort_type": sort_type, "dStart": dStart, "dEnd": dEnd, "shop_id": shop_id, "user_id": $("#txtid").val(), "emp_id": $("#txtemployees_id").val() };
            $("body").mLoading();
            PageMethods.reports_detail(data, function (e) {
                console.log(e.data);
                Chart_Reports(e.header_text, e.data);
            });
        }

        function getDate(values) {
            if (values != null && values != "") {
                var array = values.split("/");
                return array[1] + "/" + array[0] + "/" + array[2];
            }
            else return '<%= DateTime.Today.ToString("MM/dd/yyyy") %>';
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />
    <asp:HiddenField ID="hdfschoolname" runat="server" />
    <div class="full-card box-content report-container" id="content_1">
        <%-- <div class="row">
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
            </div>
        </div>--%>
        <%-- <div class="row">
            <div class="form-group col-sm-6 sort_type">
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
            <div class="form-group col-sm-6 sort_type">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <select id="select_year" class="form-control">
                        <% for (int i = 2018; i <= DateTime.Now.Year; i++)
                            {%>
                        <option value="<%= i %>"><%= i+543 %></option>
                        <%}%>
                    </select>
                </div>
            </div>
        </div>--%>
        <div class="row" style="display:none">
            <div class="form-group col-sm-6 ">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603083") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <select id="type" class='form-control'>
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504002") %></option>
                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603084") %></option>
                    </select>
                </div>
            </div>
            <div class="form-group col-sm-6 ">
            </div>
        </div>
        <div class="row typewrap" id="typewrap1">
            <div class="form-group col-sm-6 ">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603011") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" class='form-control' id="txtid1" style="display: none;" />
                    <input type="text" class='form-control' id="txtname1" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>" />
                </div>
            </div>
            <div class="form-group col-sm-6 ">
            </div>
        </div>
        <div class="row typewrap" id="typewrap2" style="display:none">
            <div class="form-group col-sm-6 ">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603086") %> :</label>
                <div class="col-md-7 col-sm-6">
                     <input type="text" class='form-control' id="txtid2" style="display: none;" />
                    <input type="text" class='form-control' id="txtname2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>" />
                </div>
            </div>
            <div class="form-group col-sm-6 ">
            </div>
        </div>
        <div class="row ">
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
        </div>

        <div class="row">
            <div class="col-sm-12">
                <input type="button" class="btn btn-primary button-custom" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="SearchData();" />
            </div>
        </div>
        <div class="row--space">
        </div>
        <div class="row"  style="display:none">
            <div class="col-lg-2 col-md-2 col-sm-2">
                <%-- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %>--%>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8">
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2">
                <div class="btn btn-success button-custom" id="exportfile" onclick="Reports.export_excel()">Export File</div>
            </div>
        </div>
        <asp:Literal ID="ltrHeaderReport" runat="server" />
        <div class="row border-bottom">
            <br />
            <div class="col-sm-12">
                <fieldset>
                    <asp:ListView ID="lvReport" runat="server">
                    </asp:ListView>
                    <span id="lblSummary" style="float: right;"></span>
                   <%-- <table id="example" class="table table-bordered table-hover dataTable no-footer"
                        cellspacing="0" width="100%">
                    </table>--%>
                    <table id="lst-data" class="table table-bordered table-hover dataTable no-footer" width="100%"></table>
                </fieldset>
            </div>
        </div>
    </div>
    <fieldset class="hidden" id="export_excel">
        <table id="table_exports" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
        </table>
    </fieldset>
    <iframe id="txtArea1" style="display: none"></iframe>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
