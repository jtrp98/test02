<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="SendDataPSIS.aspx.cs" Inherits="FingerprintPayment.StudentInfo.SendDataPSIS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <style>
        .tooltip {
            position: relative;
            display: inline-block;
            /*border-bottom: 1px dotted black;*/
            opacity: 1;
            z-index: 0;
        }

            .tooltip.check {
                color: green;
            }

            .tooltip.times {
                color: red;
            }

            .tooltip.question {
                color: orange;
            }

            .tooltip .tooltiptext {
                visibility: hidden;
                width: 160px;
                background-color: #555;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 5px 5px;
                position: absolute;
                z-index: 1;
                bottom: 125%;
                left: 50%;
                margin-left: -83px;
                opacity: 0;
                transition: opacity 0.3s;
                text-align: left;
            }

                .tooltip .tooltiptext::after {
                    content: "";
                    position: absolute;
                    top: 100%;
                    left: 50%;
                    margin-left: -5px;
                    border-width: 5px;
                    border-style: solid;
                    border-color: #555 transparent transparent transparent;
                }

            .tooltip:hover .tooltiptext {
                visibility: visible;
                opacity: 1;
            }

        .psis-menu:has(.disabled) {
            cursor: no-drop;
        }

        .modal {
            padding-right: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101300") %>
            </p>
        </div>
    </div>

    <div class="studentList row div-search">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">search</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltLevel" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %>">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                <asp:Literal ID="ltrLevel" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltClass" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                        <div class="col-sm-3 div-text-input">
                            <div class="form-group bmd-form-group">
                                <input id="iptStudentName" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101340") %>">
                            </div>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label"></label>
                        <div class="col-sm-3">
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-11 mr-auto text-center">
                            <button id="btnSearch" class="btn btn-info">
                                <span class="btn-label">
                                    <i class="material-icons">search</i>
                                </span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                            </button>
                        </div>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->


    <div class="studentList row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">account_circle</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></h4>
                </div>
                <div class="card-body">
                    <div class="toolbar">
                        <!-- Here you can write extra buttons/actions for the toolbar -->
                    </div>
                    <div class="material-datatables">
                        <div id="datatables_wrapper" class="dataTables_wrapper dt-bootstrap4">
                            <div class="row">
                                <div class="col-sm-12 col-md-6">
                                    <div class="dataTables_length">
                                        <label>
                                            Show
                                            <select id="datatables_length" aria-controls="datatables" class="custom-select custom-select-sm form-control form-control-sm" style="text-align-last: center;">
                                                <option value="10">10</option>
                                                <option value="20" selected>20</option>
                                                <option value="50">50</option>
                                                <option value="100">100</option>
                                            </select>
                                            rows</label>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <table id="tableData" class="table table-no-bordered table-hover" cellspacing="0" width="100%" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th style="width: 3%" class="text-center">
                                                    <div class="form-check">
                                                        <label class="form-check-label">
                                                            <input class="form-check-input check-all" type="checkbox">
                                                            <span class="form-check-sign">
                                                                <span class="check"></span>
                                                            </span>
                                                        </label>
                                                    </div>
                                                </th>
                                                <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></th>
                                                <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                                                <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></th>
                                                <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101301") %></th>
                                                <th style="width: 10%" class="text-center">
                                                    <div class="btn-group">
                                                        <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103029") %><span class="caret"></span>
                                                        </button>
                                                        <ul class="dropdown-menu pull-right psis-menu">
                                                            <li class="must-checked-student disabled"><a href="#" onclick="return SendDataToCentral();"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101300") %></a></li>
                                                        </ul>
                                                    </div>
                                                </th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-5">
                                    <div class="dataTables_info" role="status" aria-live="polite">Showing 1 to 10 of 40 rows</div>
                                </div>
                                <div class="col-sm-12 col-md-7">
                                    <div class="dataTables_paginate paging_full_numbers">
                                        <ul class="pagination">
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->


    <div id="modalSendDataToCentral" class="modal fade" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 10%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="">

                <div class="modal-header" style="padding: 0px 15px; top: 25%;">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101302") %></h3>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row">
                            <div class="col-md-12">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101303") %>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnSendDataToCentral" class="btn btn-success global-btn">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101304") %></button>
                    <button type="button" class="btn btn-danger global-btn" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <div id="modalLogin" class="modal fade" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 25%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="">
                <div class="modal-header" style="padding: 0px 15px; top: 25%;">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101305") %></h3>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row">
                            <label class="col-md-4 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101227") %> :</label>
                            <div class="col-sm-7 div-text-input">
                                <div class="form-group bmd-form-group">
                                    <input id="iptUsername" type="text" class="form-control" placeholder="username" />
                                </div>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-md-4 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01511") %> :</label>
                            <div class="col-sm-7 div-text-input">
                                <div class="form-group bmd-form-group">
                                    <input id="iptPassword" class="form-control" type="password" placeholder="password" style="padding-left: 7px;" />
                                </div>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                    </div>
                    <div class="row message_response">
                        <div class="col-xs-12 center">
                            <label class="text-danger"></label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnLogin" class="btn btn-primary global-btn" style="width: 123px;">
                        Login</button>
                    <button type="button" class="btn btn-danger global-btn"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type="text/javascript">

        var studentList = {
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            TotalRows: 0,
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
                        "url": "SendDataPSIS.aspx/LoadStudent",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
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
                            //$("#modalWaitDialog").modal('show');
                        },
                        "complete": function () {
                            // Handle the complete event
                            //$("#modalWaitDialog").modal('hide');
                        }
                    },
                    "columns": [
                        { "data": "check", "orderable": false },
                        { "data": "no", "orderable": false },
                        { "data": "Code", "orderable": true },
                        { "data": "Title", "orderable": true },
                        { "data": "Name", "orderable": true },
                        { "data": "Lastname", "orderable": true },
                        { "data": "ClassName", "orderable": true },
                        { "data": "Status", "orderable": true },
                        { "data": "action", "orderable": false },
                        { "data": "sid", "orderable": false },
                        { "data": "yid", "orderable": false },
                        { "data": "tid", "orderable": false },
                        { "data": "SendDate", "orderable": false },
                        { "data": "StatusCode", "orderable": false },
                        { "data": "ResponseContent", "orderable": false },
                        { "data": "SendDate2", "orderable": false },
                        { "data": "StatusCode2", "orderable": false },
                        { "data": "ResponseContent2", "orderable": false }

                    ],
                    "order": [[2, "desc"]],
                    "columnDefs": [
                        { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8] },
                        { "targets": [9, 10, 11, 12, 13, 14, 15, 16, 17], "visible": false },
                        {
                            "render": function (data, type, row) {
                                // row.rid
                                if (!row.SendDate || row.StatusCode != '201') {
                                    return `<div class="form-check">
                                            <label class="form-check-label">
                                                <input class="form-check-input check-one" type="checkbox">
                                                <span class="form-check-sign">
                                                    <span class="check"></span>
                                                </span>
                                            </label>
                                        </div>`;
                                }
                                else {
                                    return '';
                                }
                            },
                            "targets": 0
                        },
                        {
                            "render": function (data, type, row) {
                                if (!row.SendDate) {
                                    return '';
                                }
                                else {
                                    var message = '';
                                    var json = '';
                                    switch (row.StatusCode) {
                                        case '201':
                                            return '<div class="tooltip check"><i class="fa fa-check" style="padding-right: 5px;"></i><span class="tooltiptext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101304") %> ' + row.SendDate + '</span></div>';
                                            break;
                                        case '400':
                                            json = $.parseJSON(row.ResponseContent);
                                            if (Array.isArray(json.errors)) {
                                                // It is array
                                                message = 'Errors<br>';
                                                $.each(json.errors, function (i, o) {
                                                    message += (i + 1) + '. ' + o.message + '<br>';
                                                });
                                            }
                                            else if (json.errors !== null && typeof (json.errors) === 'object') {
                                                // It is object
                                                message = 'Error<br>';
                                                message += '1. ' + json.errors.message + '<br>';
                                            }
                                            return '<div class="tooltip times"><i class="fa fa-times" style="padding-right: 5px;"></i><span class="tooltiptext">' + message + '</span></div>';
                                            break;
                                        case '401':
                                            json = $.parseJSON(row.ResponseContent);
                                            return '<div class="tooltip question"><i class="fa fa-question" style="padding-right: 5px;"></i><span class="tooltiptext">' + json.message + '</span></div>';
                                            break;
                                    }
                                }
                            },
                            "targets": 8
                        }
                    ],
                    "drawCallback": function (settings) {
                        //var json = settings.json;
                        var json = $.parseJSON(settings.json.d);
                        //console.log(json);

                        studentList.PageCount = json.pageCount;
                        studentList.TotalRows = json.recordsTotal;

                        var pageLRSize = 3;
                        var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                        var previousDot = '';
                        var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                        var nextDot = '';
                        var elements = '';

                        if (studentList.PageIndex - pageLRSize > 1) {
                            previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                        }

                        if (studentList.PageIndex + pageLRSize < json.pageCount - 1) {
                            nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                        }

                        for (var pi = 0; pi < json.pageCount; pi++) {
                            if (pi == 0) {
                                elements += '<li class="paginate_button page-item ' + (studentList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                elements += previousDot;
                            }
                            else if (pi == json.pageCount - 1) {
                                elements += nextDot;
                                elements += '<li class="paginate_button page-item ' + (studentList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                            else if (studentList.PageIndex - pageLRSize <= pi && studentList.PageIndex + pageLRSize >= pi) {
                                elements += '<li class="paginate_button page-item ' + (studentList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                        }

                        $('.pagination').html(previous + elements + next);

                        $('.dataTables_info').html('Showing ' + ((studentList.PageIndex * studentList.PageSize) + 1) + ' to ' + ((studentList.PageIndex * studentList.PageSize) + studentList.PageSize) + ' of ' + studentList.TotalRows + ' rows');

                    }
                });

                // order.dt search.dt
                studentList.dt.on('draw.dt', function () {
                    studentList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        var sendDate = studentList.dt.cells({ row: i, column: 12 }).data()[0];
                        var statusCode = studentList.dt.cells({ row: i, column: 13 }).data()[0];

                        if (!sendDate || statusCode != '201') {
                            $(cell).find(".check-one").removeAttr("disabled");

                            var sid = studentList.dt.cells({ row: i, column: 9 }).data()[0];
                            var yid = studentList.dt.cells({ row: i, column: 10 }).data()[0];
                            $(cell).find(".check-one").attr("sid", sid).attr("yid", yid);
                        }
                    });
                    studentList.dt.column(1, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (studentList.PageIndex * studentList.PageSize) + (i + 1) + '.';
                    });
                    studentList.dt.column(8, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        var sendDate = studentList.dt.cells({ row: i, column: 12 }).data()[0];

                    });
                });
            },
            ReloadListData: function () {
                studentList.dt.draw();
            }
        }

        function LoadTerm(yearID, objResult) {
            if (yearID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "SendDataPSIS.aspx/LoadTerm",
                    data: '{yearID: ' + yearID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var yearData = response.d;

                        $(objResult).empty();

                        if (yearData.length > 0) {

                            var options = '';
                            var firstSelected = 'selected';
                            $(yearData).each(function () {

                                options += '<option value="' + this.id + '" ' + firstSelected + '>' + this.name + '</option>';

                                firstSelected = '';
                            });

                            $(objResult).html(options);
                            $(objResult).selectpicker('refresh');
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

        function LoadTermSubLevel2(subLevelID, objResult) {
            if (subLevelID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "SendDataPSIS.aspx/LoadTermSubLevel2",
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
                            $(objResult).selectpicker('refresh');
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

        function SendDataToCentral() {

            var data = [];
            $('.studentList #tableData input[type=checkbox].check-one:checked').each(function (index) {
                data.push({ sid: $(this).attr('sid'), yid: $(this).attr('yid') });
            });

            if (data.length > 0) {

                $("#modalSendDataToCentral").modal('show');

            } else {
                swal({
                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>',
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-success",
                    html: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111024") %></h3>'
                }).catch(swal.noop)
            }

            return false;
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

                return false;
            });

            $('.studentList #datatables_length').change(function () {

                studentList.PageSize = parseInt($("#datatables_length").children("option:selected").val());
                studentList.PageIndex = 0;

                studentList.ReloadListData();

                return false;
            });

            $('.studentList ul.pagination').on('click', 'li.paginate_button a', function () {

                var pi = parseInt($(this).attr("data-dt-idx"));

                if (pi == 100) {
                    if (studentList.PageIndex > 0) {
                        studentList.PageIndex--;
                        studentList.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + studentList.PageIndex + ']').addClass('active');
                    }
                }
                else if (pi == 101) {
                    if (studentList.PageIndex < (studentList.PageCount - 1)) {
                        studentList.PageIndex++;
                        studentList.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + studentList.PageIndex + ']').addClass('active');
                    }
                }
                else {
                    studentList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                    studentList.ReloadListData();

                    $('.pagination .paginate_button.page-item.active').removeClass('active');
                    $(this).addClass('active');
                }

                return false;
            });

            $('.studentList #tableData #datatables_previous').click(function () {

                if (studentList.PageIndex > 0) {
                    studentList.PageIndex--;
                    studentList.ReloadListData();
                }

                return false;
            });

            $('.studentList #tableData #datatables_next').click(function () {

                if (studentList.PageIndex < (studentList.PageCount - 1)) {
                    studentList.PageIndex++;
                    studentList.ReloadListData();
                }

                return false;
            });


            $('.check-all').click(function () {
                $(".check-one").prop('checked', $(this).prop('checked'));

                // Enable or Disable when checked student
                if ($(".check-one:checked").length) $('.must-checked-student').removeClass("disabled"); else $('.must-checked-student').addClass("disabled");
            });

            $('#tableData').on('change', 'tbody input.check-one', function () {
                $(".check-all").prop('checked', ($('.check-one').length == $('.check-one').filter(":checked").length));

                // Enable or Disable when checked student
                if ($(".check-one:checked").length) $('.must-checked-student').removeClass("disabled"); else $('.must-checked-student').addClass("disabled");
            });

            // Search
            $("#sltLevel").change(function () {

                LoadTermSubLevel2($(this).val(), '#sltClass');

            });

            // Modal Section
            $('#modalNotifyOnlyClose').on('hidden.bs.modal', function () {
                if (($("#modalWaitDialog").data('bs.modal') || {})._isShown) {
                    setTimeout(function () {
                        $("#modalWaitDialog").modal('hide');
                    }, 1000);
                }
            });
            $('#modalLogin').on('show.bs.modal', function () {
                if (($("#modalWaitDialog").data('bs.modal') || {})._isShown) {
                    setTimeout(function () {
                        $("#modalWaitDialog").modal('hide');
                    }, 1000);
                }
            });


            $("#sltSortLevel").change(function () {

                LoadTermSubLevel2($(this).val(), '#sltSortClass');

            });

            $('#btnSendDataToCentral').click(function () {

                var data = [];
                $('.studentList #tableData input[type=checkbox].check-one:checked').each(function (index) {
                    data.push({ sid: $(this).attr('sid'), yid: $(this).attr('yid') });
                });

                if (data.length > 0) {

                    $("#modalWaitDialog").modal('show');

                    $.ajax({
                        async: true,
                        type: "POST",
                        url: "SendDataPSIS.aspx/SendDataToCentral",
                        data: "{studentDatas:" + JSON.stringify(data) + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {

                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101304") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %>) REGIS <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101266") %> [<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101266") %>: ' + r.log.success + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01611") %>, <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01407") %>: ' + r.log.unsuccess + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01611") %>]';
                                //body += '<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101304") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %>) REGIS <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101266") %> [<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101266") %>: ' + r.log2.success + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01611") %>, <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01407") %>: ' + r.log2.unsuccess + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01611") %>]<br/>';

                                $('#modalSendDataToCentral').modal('hide');

                                $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                                $("#modalNotifyOnlyClose").find('.modal-body p').html(body);
                                $("#modalNotifyOnlyClose").modal('show');

                                studentList.ReloadListData();
                            }
                            else {
                                if (r.statusCode == 500) {
                                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101300") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01407") %> [' + r.message + ']';

                                    $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                                    $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                                    $("#modalNotifyOnlyClose").modal('show');
                                }
                                else if (r.statusCode == 401) {

                                    // Show Login Modal
                                    $("#modalLogin").modal('show');
                                }
                            }

                            $("#modalWaitDialog").modal('hide');
                        },
                        failure: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                            $("#modalWaitDialog").modal('hide');
                        },
                        error: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                            $("#modalWaitDialog").modal('hide');
                        }
                    });
                } else {
                    swal({
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>',
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        html: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111024") %></h3>'
                    }).catch(swal.noop)
                }

                return false;
            });

            $('#btnLogin').click(function () {

                var data = {
                    "username": $("#iptUsername").val(),
                    "password": $("#iptPassword").val()
                }

                $("#modalWaitDialog").modal('show');

                $.ajax({
                    async: false,
                    type: "POST",
                    url: "SendDataPSIS.aspx/LoginPSIS",
                    data: "{loginData:" + JSON.stringify(data) + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var title = "";
                        var body = "";

                        var res = $.parseJSON(response.d);
                        switch (res.result) {
                            case "success":
                                //title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                //body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01085") %>';

                                $('#modalLogin').modal('hide');

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = 'Login <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01407") %>[' + res.message + ']';

                                $(".message_response .text-danger").text(body);

                                break;
                        }

                        setTimeout(function () {
                            $("#modalWaitDialog").modal('hide');
                        }, 1000);
                    },
                    failure: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                        $("#modalWaitDialog").modal('hide');
                    },
                    error: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                        $("#modalWaitDialog").modal('hide');
                    }
                });

                return false;
            });

            $('.studentList #iptStudentName').autoComplete({
                resolverSettings: {
                    fail: function () {
                        console.log('fail');
                    }
                },
                resolver: 'custom',
                events: {
                    search: function (qry, callback) {
                        var param = { keyword: qry, termID: $('.studentList #sltTerm').val() };
                        $.ajax({
                            url: "SendDataPSIS.aspx/GetStudentName",
                            data: JSON.stringify(param),
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) { return data; },
                            success: function (data) {
                                callback(data.d);
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                console.log(textStatus);
                            }
                        });
                    }
                },
                minLength: 1
            });

            // Initial control

            // Datatable Section
            studentList.LoadListData();
        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
