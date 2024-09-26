<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="StudentListQRCode.aspx.cs" Inherits="FingerprintPayment.StudentInfo.StudentListQRCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />

    <!-- DatetimePicker -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.css" />

    <style type="text/css">
        .table > tbody > tr > td.vertical-align-middle {
            vertical-align: middle;
        }

        .table > thead > tr > th.vertical-align-middle {
            vertical-align: middle;
            padding-right: 10px;
        }

        .modal-body {
            font-size: 26px;
        }

        .dropdown-menu {
            font-size: 22px;
        }

        .ui-menu {
            list-style: none;
            padding: 2px;
            margin: 0;
            display: block;
            float: left;
        }

            .ui-menu .ui-menu {
                margin-top: -3px;
            }

            .ui-menu .ui-menu-item {
                margin: 0;
                padding: 0;
                zoom: 1;
                float: left;
                clear: left;
                width: 100%;
            }

                .ui-menu .ui-menu-item a {
                    text-decoration: none;
                    display: block;
                    padding: .2em .4em;
                    line-height: 1.5;
                    zoom: 1;
                    cursor: pointer;
                }

                    .ui-menu .ui-menu-item a strong {
                        color: orange;
                    }

                    .ui-menu .ui-menu-item a.ui-state-hover,
                    .ui-menu .ui-menu-item a.ui-state-active {
                        font-weight: normal;
                        margin: -1px;
                    }

        .btn.btn-success, .btn.btn-cancel, .btn.btn-danger, .btn.btn-primary, .btn.btn-info {
            width: 110px;
            height: 44px;
        }

        #btnExportAllQRCode {
            width: 130px;
        }

        #modalSortStudentNo input[type=checkbox], #modalSortStudentNo input[type=radio],
        #modalManageStudentTitle input[type=checkbox], #modalManageStudentTitle input[type=radio] {
            -ms-transform: scale(1.3);
            -moz-transform: scale(1.3);
            -webkit-transform: scale(1.3);
            -o-transform: scale(1.3);
            transform: scale(1.3);
            padding: 10px;
            cursor: pointer;
        }

        .modal {
        }

        .vertical-alignment-helper {
            display: table;
            height: 100%;
            width: 100%;
        }

        .vertical-align-center {
            /* To center vertically */
            display: table-cell;
            vertical-align: middle;
        }

        .modal-content {
            /* Bootstrap sets the size of the modal in the modal-dialog class, we need to inherit it */
            width: inherit;
            height: inherit;
            /* To center horizontally */
            margin: 0 auto;
        }

        .none-finger {
            background-color: #fd7a7a;
            color: white;
        }

        #tableData tbody tr.none-finger.even:hover, #tableData tbody tr.none-finger.odd:hover {
            background-color: #fd7a7a;
            color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="studentList">
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-6">
                    <label class="col-md-5">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405034") %></label>
                    <div class="col-md-7">
                        <select id="sltUserType" name="sltUserType[]"
                            class="form-control">
                            <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405014") %></option>
                            <option value="0" selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></option>
                        </select>
                    </div>
                </div>
                <div class="col-md-6">
                </div>
            </div>
            <div class="row row-student" style="padding-bottom: 10px;">
                <div class="col-md-6">
                    <label class="col-md-5">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                    <div class="col-md-7">
                        <select id="sltLevel" name="sltLevel[]"
                            class="form-control">
                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                            <asp:Literal ID="ltrLevel" runat="server" />
                        </select>
                    </div>
                </div>
                <div class="col-md-6">
                    <label class="col-md-5">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                    <div class="col-md-7">
                        <select id="sltClass" name="sltClass[]"
                            class="form-control">
                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-6">
                    <label class="col-md-5">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                    <div class="col-md-7">
                        <input id="iptStudentName" name="iptStudentName" type="text" class="form-control" style="width: 100%;" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101340") %>" />
                    </div>
                </div>
                <div class="col-md-6" style="display: none;">
                </div>
            </div>
            <div class="row">
                <br />
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <button id="btnSearch" class="btn btn-info btn-round col-md-2" style="font-size: 24px; float: none;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                    </button>
                </div>
            </div>
            <div class="row">
                <br />
            </div>
            <table id="tableData" class="table table-bordered table-hover" style="width: 100%">
                <thead>
                    <tr>
                        <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                        <th style="width: 12%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                        <th style="width: 13%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></th>
                        <th style="width: 30%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                        <th style="width: 30%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                        <th style="width: 10%" class="text-center">
                            <div class="btn-group">
                                <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103029") %><span class="caret"></span>
                                </button>
                                <ul class="dropdown-menu pull-right">
                                    <li><a href="#" onclick="GenerateAllQrCode(); return false;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106138") %></a></li>
                                    <li><a href="#" onclick="ExportAllQrCode(); return false;">Export to Image(.Png)</a></li>
                                </ul>
                            </div>
                        </th>
                        <th></th>
                    </tr>
                </thead>

                <tfoot>
                    <tr>
                        <th colspan="7">
                            <div class="row">
                                <div class="col-md-4 mb-4 text-left" style="padding-left: 4%;">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>: </span>
                                    <select id="sltPageSize">
                                        <option selected="selected" value="20">20</option>
                                        <option value="50">50</option>
                                        <option value="100">100</option>
                                    </select>
                                </div>
                                <div class="col-md-4 mb-4 text-center">
                                    <a id="aPrevious" href="#" style="text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></a>
                                    <select id="sltPageIndex">
                                    </select>
                                    <a id="aNext" href="#" style="text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></a>
                                </div>
                                <div class="col-md-4 mb-4 text-right" style="padding-right: 2%;">
                                    <span id="spnPageInfo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132005") %></span>
                                </div>
                            </div>
                        </th>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>

    <div id="modalGenerateQrCode" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered global-modal" role="document">
            <div class="modal-content" style="max-width: 900px;">

                <div class="modal-header center" style="padding: 0px; top: 25%;">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00018") %></h1>
                </div>
                <div class="modal-body">
                    <div class="iframe-print-loading" style="text-align: center; display: none;">
                        <img src="/assets/images/wait.gif" style="width: 75px; height: 75px;" />
                    </div>
                    <div>
                        <div class="row">
                            <div class="col-xs-12 text-center">
                                <iframe id="iframePrint" src="" frameborder="0" scrolling="no" style="width: inherit;" onload="this.style.height=this.contentDocument.body.scrollHeight + 40 + 'px';"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnPrintQRCode" class="btn btn-info global-btn">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></button>
                    <button type="button" class="btn btn-danger global-btn"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>

            </div>
        </div>
    </div>
    <!-- /.modal -->

    <div id="modalGenerateAllQrCode" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered global-modal" role="document">
            <div class="modal-content" style="max-width: 900px;">

                <div class="modal-header center" style="padding: 0px; top: 25%;">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00018") %></h1>
                </div>
                <div class="modal-body">
                    <div class="iframe-print-all-loading" style="text-align: center; display: none;">
                        <img src="/assets/images/wait.gif" style="width: 75px; height: 75px;" />
                    </div>
                    <div>
                        <div class="row">
                            <div class="col-xs-12 text-center">
                                <iframe id="iframePrintAll" src="" frameborder="0" scrolling="yes" style="width: inherit;" onload="this.style.height='607px';"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnPrintAllQRCode" class="btn btn-info global-btn">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></button>
                    <button type="button" class="btn btn-danger global-btn"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>

            </div>
        </div>
    </div>
    <!-- /.modal -->

    <div id="modalExportAllQrCode" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered global-modal" role="document">
            <div class="modal-content" style="max-width: 900px;">

                <div class="modal-header center" style="padding: 0px; top: 25%;">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00018") %></h1>
                </div>
                <div class="modal-body">
                    <div class="iframe-export-all-loading" style="text-align: center; display: none;">
                        <img src="/assets/images/wait.gif" style="width: 75px; height: 75px;" />
                    </div>
                    <div>
                        <div class="row">
                            <div class="col-xs-12 text-center">
                                <iframe id="iframeExportAll" src="" frameborder="0" scrolling="yes" style="width: inherit;" onload="this.style.height='607px';"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnExportAllQRCode" class="btn btn-info global-btn">
                        Download Zip</button>
                    <button type="button" class="btn btn-danger global-btn"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>

            </div>
        </div>
    </div>
    <!-- /.modal -->

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">

    <script type='text/javascript' src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>

    <!-- DataTables -->
    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript" src="/scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js"></script>

    <script language="javascript" type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>


    <script type="text/javascript">

        var studentList = {
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            dt: null,
            LoadListData: function () {
                studentList.dt = $(".studentList #tableData").DataTable({
                    "processing": true,
                    "serverSide": true,
                    "info": false,
                    "searching": false,
                    "paging": false,
                    "stateSave": true,
                    "ajax": {
                        "url": "StudentListQRCode.aspx/LoadStudent",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.userType = $(".studentList #sltUserType").children("option:selected").val();
                            d.level = $(".studentList #sltLevel").children("option:selected").val();
                            d.className = $(".studentList #sltClass").children("option:selected").val();
                            d.stdName = $(".studentList #iptStudentName").val();
                            d.page = studentList.PageIndex;
                            d.length = studentList.PageSize;

                            return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                        },
                        "dataSrc": function (json) {
                            var jsond = $.parseJSON(json.d);
                            //console.log(jsond);
                            return jsond.data;
                        },
                        "beforeSend": function () {
                            // Handle the beforeSend event
                            $("#modalWaitDialog").modal('show');
                        },
                        "complete": function () {
                            // Handle the complete event
                            $("#modalWaitDialog").modal('hide');
                        }
                    },
                    "columns": [
                        { "data": "no", "orderable": false },
                        { "data": "Code", "orderable": true },
                        { "data": "Title", "orderable": true },
                        { "data": "Name", "orderable": true },
                        { "data": "Lastname", "orderable": true },
                        { "data": "action", "orderable": false },
                        { "data": "sid", "orderable": false }
                    ],
                    "order": [[1, "desc"]],
                    "columnDefs": [
                        { className: "vertical-align-middle text-center", "targets": [0, 5] },
                        { className: "text-center", "targets": [0, 1, 2, 3, 4, 5] },
                        { "targets": [6], "visible": false },
                        {
                            "render": function (data, type, row) {
                                return '<a href="#"><i class="fa fa-print" style="padding-right: 5px;"></i></a>';
                            },
                            "targets": 5
                        }
                    ],
                    "drawCallback": function (settings) {
                        //var json = settings.json;
                        var json = $.parseJSON(settings.json.d);

                        studentList.PageCount = json.pageCount;

                        var options = '';
                        for (var pi = 0; pi < json.pageCount; pi++) {
                            options += '<option ' + (studentList.PageIndex == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                        }
                        $('.studentList #tableData #sltPageIndex').html(options);

                        $('.studentList #tableData #spnPageInfo').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (studentList.PageIndex + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + studentList.PageCount + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');
                    }
                });

                // order.dt search.dt
                studentList.dt.on('draw.dt', function () {
                    studentList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (studentList.PageIndex * studentList.PageSize) + (i + 1) + '.';
                    });
                    studentList.dt.column(5, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        var sid = studentList.dt.cells({ row: i, column: 6 }).data()[0];
                        $(cell).find(".fa-print").parent().attr("onClick", 'GenerateQrCode(' + sid + ')');
                    });
                });
            },
            ReloadListData: function () {
                studentList.dt.draw();
            }
        }

        function ExportAllQrCode() {
            var userType = $(".studentList #sltUserType").children("option:selected").val();
            var level = $(".studentList #sltLevel").children("option:selected").val();
            var className = $(".studentList #sltClass").children("option:selected").val();
            var stdName = $(".studentList #iptStudentName").val();

            if ((userType == '0' && (className || stdName)) || userType == '1') {
                $("#iframeExportAll").contents().find("body").html("");

                $('.iframe-export-all-loading').fadeIn();
                $('#btnExportAllQRCode').prop('disabled', true);

                $('#iframeExportAll').attr('src', '/StudentInfo/StdExportQRCode.aspx?id=0&userType=' + userType + '&level=' + level + '&className=' + className + '&stdName=' + stdName);

                $('#modalExportAllQrCode').modal('show');
            }
            else {
                $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>');
                $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111091") %>');
                $("#modalNotifyOnlyClose").modal('show');
            }
        }

        function GenerateAllQrCode() {
            var userType = $(".studentList #sltUserType").children("option:selected").val();
            var level = $(".studentList #sltLevel").children("option:selected").val();
            var className = $(".studentList #sltClass").children("option:selected").val();
            var stdName = $(".studentList #iptStudentName").val();

            if ((userType == '0' && (className || stdName)) || userType == '1') {
                $("#iframePrintAll").contents().find("body").html("");

                $('.iframe-print-all-loading').fadeIn();

                $('#iframePrintAll').attr('src', '/StudentInfo/StdPrintQRCode.aspx?id=0&userType=' + userType + '&level=' + level + '&className=' + className + '&stdName=' + stdName);

                $('#modalGenerateAllQrCode').modal('show');
            }
            else {
                $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>');
                $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111092") %>');
                $("#modalNotifyOnlyClose").modal('show');
            }
        }

        function GenerateQrCode(sid) {
            $('.iframe-print-loading').fadeIn();

            var userType = $(".studentList #sltUserType").children("option:selected").val();
            $('#iframePrint').attr('src', '/StudentInfo/StdPrintQRCode.aspx?id=' + sid + '&userType=' + userType);

            $('#modalGenerateQrCode').modal('show');
        }

        function LoadTermSubLevel2(subLevelID, objResult) {
            if (subLevelID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "StudentListQRCode.aspx/LoadTermSubLevel2",
                    data: '{subLevelID: ' + subLevelID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var subLevel2 = response.d;

                        $(objResult).empty();

                        if (subLevel2.length > 0) {

                            var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>';
                            $(subLevel2).each(function () {

                                options += '<option value="' + this.id + '">' + this.name + '</option>';

                            });

                            $(objResult).html(options);
                        }
                    },
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }

        function PadLeft(char, width) {
            char = char.toString();
            return char.length < width ? PadLeft("0" + char, width) : char;
        }

        $(document).ready(function () {

            $.ajaxSetup({
                statusCode: {
                    500: function () {
                        window.location.replace("/Default.aspx");
                    }
                }
            });

            if (jQuery().dataTable) {
                $.fn.dataTable.ext.errMode = 'none';
            }

            // Searching, Pagination event 
            $('.studentList #btnSearch').click(function () {

                studentList.PageIndex = 0;

                studentList.ReloadListData();

                switch ($("#sltUserType").val()) {
                    case "0":
                        $(studentList.dt.column(1).header()).text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>');
                        break;
                    case "1":
                        $(studentList.dt.column(1).header()).text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111089") %>');
                        break;
                }

                return false;
            });

            $('.studentList #tableData #sltPageSize').change(function () {

                studentList.PageSize = parseInt($(".studentList #tableData #sltPageSize").children("option:selected").val());
                studentList.PageIndex = 0;

                studentList.ReloadListData();

                return false;
            });

            $('.studentList #tableData #sltPageIndex').change(function () {

                studentList.PageIndex = $(".studentList #tableData #sltPageIndex").children("option:selected").val();

                studentList.ReloadListData();

                return false;
            });

            $('.studentList #tableData #aPrevious').click(function () {

                if (studentList.PageIndex > 0) {
                    studentList.PageIndex--;
                    studentList.ReloadListData();
                }

                return false;
            });

            $('.studentList #tableData #aNext').click(function () {

                if (studentList.PageIndex < (studentList.PageCount - 1)) {
                    studentList.PageIndex++;
                    studentList.ReloadListData();
                }

                return false;
            });

            // Search
            $("#sltUserType").change(function () {
                switch ($(this).val()) {
                    case "0":
                        $('.row-student').show();
                        $("#iptStudentName").attr("placeholder", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101340") %>");
                        //$(studentList.dt.column(1).header()).text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>');
                        break;
                    case "1":
                        $('.row-student').hide();
                        $("#iptStudentName").attr("placeholder", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111090") %>");
                        //$(studentList.dt.column(1).header()).text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111089") %>');
                        break;
                }
            });

            $("#sltLevel").change(function () {

                LoadTermSubLevel2($(this).val(), '#sltClass');

            });

            // Modal Section

            $.ui.autocomplete.prototype._renderItem = function (ul, item) {
                var t = String(item.value).replace(
                    new RegExp(this.term, "gi"),
                    "<strong>$&</strong>");
                return $("<li></li>")
                    .data("item.autocomplete", item)
                    .append("<a>" + t + "</a>")
                    .appendTo(ul);
            };

            $(".studentList #iptStudentName").autocomplete({
                source: function (request, response) {
                    var param = { keyword: $('.studentList #iptStudentName').val(), userType: $(".studentList #sltUserType").children("option:selected").val() };
                    $.ajax({
                        url: "StudentListQRCode.aspx/GetStudentName",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            console.log(textStatus);
                        }
                    });
                },
                select: function (event, ui) {
                    // ui.item
                    // ui.item.value
                },
                minLength: 1
            });

            $('#btnPrintQRCode').click(function () {

                // print just the modal div
                $('#iframePrint')[0].contentWindow.print();

                return false;
            });

            $('#btnPrintAllQRCode').click(function () {

                // print just the modal div
                $('#iframePrintAll')[0].contentWindow.print();

                return false;
            });

            $('#btnExportAllQRCode').click(function () {

                // Get level & class id, name
                var levelID = $(".studentList #sltLevel").children("option:selected").val();
                var classID = $(".studentList #sltClass").children("option:selected").val();
                var className = $(".studentList #sltClass").children("option:selected").text().replace(/ /g, '').replace('/', '_');
                var stdName = $(".studentList #iptStudentName").val();

                var zipFileName = "LINE@PrintQRCode";
                if (classID && stdName) {
                    zipFileName = zipFileName + "_" + levelID + "" + PadLeft(classID, 2) + "_" + className + "_" + stdName;
                } else if (classID) {
                    zipFileName = zipFileName + "_" + levelID + "" + PadLeft(classID, 2) + "_" + className;
                } else if (stdName) {
                    zipFileName = zipFileName + "_" + stdName;
                }

                // Calling Function inside an iFrame
                $('#btnExportAllQRCode').prop('disabled', true);
                $('#iframeExportAll')[0].contentWindow.ExportQRCodeToZip(zipFileName);

                return false;
            });

            $('#iframePrint').on('load', function () {
                // code will run after iframe has finished loading
                $('.iframe-print-loading').fadeOut(300);
            });

            $('#iframePrintAll').on('load', function () {
                // code will run after iframe has finished loading
                $('.iframe-print-all-loading').fadeOut(300);

            });

            $('#iframeExportAll').on('load', function () {
                // code will run after iframe has finished loading
                $('.iframe-export-all-loading').fadeOut(300);

                $('#btnExportAllQRCode').prop('disabled', false);
            });

            // Initial control

            // Datatable Section
            studentList.LoadListData();
        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
