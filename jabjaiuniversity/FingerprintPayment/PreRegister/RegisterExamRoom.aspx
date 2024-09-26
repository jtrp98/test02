<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="RegisterExamRoom.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterExamRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        #tableData tr td ul.dropdown-menu li {
            white-space: nowrap;
        }

        #tableData tr td .btn-group, #tableData tr td .btn-group-vertical {
            position: relative;
            margin: 3px 1px;
        }

        .material-list .dropdown-toggle .filter-option-inner-inner {
            font-size: 14px;
        }

        label.col-form-label {
            color: #AAAAAA;
        }

        table.dataTable thead .sorting:before, table.dataTable thead .sorting_asc:before, table.dataTable thead .sorting_desc:before, table.dataTable thead .sorting_asc_disabled:before, table.dataTable thead .sorting_desc_disabled:before, table.dataTable thead .sorting:after, table.dataTable thead .sorting_asc:after, table.dataTable thead .sorting_desc:after, table.dataTable thead .sorting_asc_disabled:after, table.dataTable thead .sorting_desc_disabled:after {
            top: 7%;
        }

        #frmExamRoom .form-check, label.error {
            font-size: 0.8rem;
            color: #f44336;
        }

        #frmExamRoom .bmd-form-group .form-control, #frmExamRoom .bmd-form-group label, #frmExamRoom .bmd-form-group input::placeholder {
            padding-left: 3px;
            padding-bottom: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00405") %>
            </p>
        </div>
    </div>
    <div class="row material-list">
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
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103190") %></label>
                        <div class="col-sm-3">
                            <select id="sltPlanSearch" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01915") %>">
                                <asp:Literal ID="ltrPlanSearch" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103196") %></label>
                        <div class="col-sm-3">
                            <div class="form-group bmd-form-group">
                                <input id="iptExamRoomNameSearch" type="text" class="form-control" placeholder="">
                            </div>
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
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">newspaper</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00405") %></h4>
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
                                    <table id="tableData" class="table table-no-bordered" cellspacing="0" width="100%" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                <th style="width: 26%" class="text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103190") %></th>
                                                <th style="width: 26%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103196") %></th>
                                                <th style="width: 26%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103197") %></th>
                                                <th style="width: 13%" class="disabled-sorting text-center">
                                                    <button id="btnExamRoomNew" class="btn btn-success btn-sm" role="button" aria-pressed="true" style="margin: -21px -9px -9px -9px;">
                                                        <span class="btn-label">
                                                            <i class="material-icons">add</i>
                                                        </span>
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103193") %>
                                                    </button>
                                                </th>
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

    <div id="modalExamRoomForm" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 10%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="width: 550px;">

                <div class="modal-header text-center" style="padding: 15px 15px 0px 15px; top: 15%; display: block;">
                    <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103193") %></h4>
                </div>
                <div class="modal-body">
                    <form id="frmExamRoom">
                        <div>
                            <div class="row" style="padding-left: 17px;">
                                <label class="col-md-4 col-form-label text-left" style="font-weight: normal; font-size: 14px; white-space: nowrap; padding-top: 14px;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>:</label>
                                <div class="col-md-8">
                                    <select id="sltLevel" name="sltLevel"
                                        class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %>" data-size="7">
                                        <asp:Literal ID="ltrLevel" runat="server" />
                                    </select>
                                </div>
                            </div>
                            <div class="row" style="padding-left: 17px;">
                                <label class="col-md-4 col-form-label text-left" style="font-weight: normal; font-size: 14px; padding-top: 14px;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103195") %>:</label>
                                <div class="col-md-8">
                                    <select id="sltPlan" name="sltPlan"
                                        class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132825") %>">
                                    </select>
                                </div>
                            </div>
                            <div class="row" style="padding-left: 17px;">
                                <label class="col-md-4 col-form-label text-left" style="font-weight: normal; font-size: 14px; padding-top: 14px;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103196") %>:</label>
                                <div class="col-md-8">
                                    <input id="iptExamRoomName" name="iptExamRoomName" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103196") %>" maxlength="150" />
                                </div>
                            </div>
                            <div class="row" style="padding-left: 17px;">
                                <label class="col-md-4 col-form-label text-left" style="font-weight: normal; font-size: 14px; padding-top: 14px;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103197") %>:</label>
                                <div class="col-md-8">
                                    <input id="iptSeats" name="iptSeats" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103197") %>" maxlength="4" />
                                </div>
                            </div>
                        </div>
                    </form>

                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnSaveExamRoom" class="btn btn-success global-btn">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    <button type="button" class="btn btn-danger global-btn" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>

    <script>
        var examRoom = {
            ExamRoomID: 0,
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            TotalRows: 0,
            dt: null,
            LoadListData: function () {
                examRoom.dt = $('#tableData').DataTable({
                    "processing": true,
                    "serverSide": true,
                    "info": false,
                    "searching": false,
                    "paging": false,
                    "stateSave": true,
                    "ajax": {
                        "url": "RegisterExamRoom.aspx/LoadRegisterExamRoom",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.plan = $("#sltPlanSearch").children("option:selected").val();
                            d.examRoomName = $("#iptExamRoomNameSearch").val();
                            d.page = examRoom.PageIndex;
                            d.length = examRoom.PageSize;

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
                        { "data": "no", "orderable": false },
                        { "data": "PlanName", "orderable": true },
                        { "data": "ExamRoomName", "orderable": true },
                        { "data": "Seats", "orderable": true },
                        { "data": "action", "orderable": false },
                        { "data": "erid", "orderable": false }
                    ],
                    "order": [[5, "desc"]],
                    "columnDefs": [
                        { className: "text-center", "targets": [0, 1, 2, 3, 4] },
                        { "targets": [5], "visible": false },
                        {
                            "render": function (data, type, row) {
                                return `<a href="#"><i class="fa fa-edit show-form-data" style="padding-right: 5px; font-size: 22px;"></i></a>
                                        <a href="#" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101240") %>"><i class="fa fa-remove text-danger" style="font-size: 22px;"></i></a>`;
                            },
                            "targets": 4
                        }
                    ],
                    "drawCallback": function (settings) {
                        //var json = settings.json;
                        var json = $.parseJSON(settings.json.d);
                        //console.log(json);

                        examRoom.PageCount = json.pageCount;
                        examRoom.TotalRows = json.recordsTotal;

                        var pageLRSize = 3;
                        var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                        var previousDot = '';
                        var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                        var nextDot = '';
                        var elements = '';

                        if (examRoom.PageIndex - pageLRSize > 1) {
                            previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                        }

                        if (examRoom.PageIndex + pageLRSize < json.pageCount - 1) {
                            nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                        }

                        for (var pi = 0; pi < json.pageCount; pi++) {
                            if (pi == 0) {
                                elements += '<li class="paginate_button page-item ' + (examRoom.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                elements += previousDot;
                            }
                            else if (pi == json.pageCount - 1) {
                                elements += nextDot;
                                elements += '<li class="paginate_button page-item ' + (examRoom.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                            else if (examRoom.PageIndex - pageLRSize <= pi && examRoom.PageIndex + pageLRSize >= pi) {
                                elements += '<li class="paginate_button page-item ' + (examRoom.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                        }

                        $('.pagination').html(previous + elements + next);

                        $('.dataTables_info').html('Showing ' + ((examRoom.PageIndex * examRoom.PageSize) + 1) + ' to ' + ((examRoom.PageIndex * examRoom.PageSize) + examRoom.PageSize) + ' of ' + examRoom.TotalRows + ' rows');
                    }
                });

                // order.dt search.dt
                examRoom.dt.on('draw.dt', function () {
                    examRoom.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (examRoom.PageIndex * examRoom.PageSize) + (i + 1) + '.';
                    });
                    examRoom.dt.column(4, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        var erid = examRoom.dt.cells({ row: i, column: 5 }).data()[0];
                        $(cell).find(".fa-edit").parent().attr("onClick", 'ShowRegisterExamRoomForm(' + erid + ')');
                        $(cell).find(".fa-remove").parent().attr("erid", erid);
                    });

                });
            },
            SaveItem: function (data) {
                $.ajax({
                    type: "POST",
                    url: "RegisterExamRoom.aspx/SaveData",
                    data: JSON.stringify({ registerExamRoomData: data }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: examRoom.OnSuccessSave,
                    failure: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                        $("#modalWaitDialog").modal('hide');
                    },
                    error: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                        $("#modalWaitDialog").modal('hide');
                    }
                });
            },
            OnSuccessSave: function (response) {
                var title = "";
                var body = "";

                var r = JSON.parse(response.d);
                if (r.success) {
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                    $("#modalExamRoomForm").modal('hide');

                    examRoom.ReloadListData();
                }
                else {
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206582") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [' + r.message + ']';
                }

                $("#modalWaitDialog").modal('hide');

                $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                $("#modalNotifyOnlyClose").modal('show');
            },
            RemoveItem: function (erid) {
                $.ajax({
                    type: "POST",
                    url: "RegisterExamRoom.aspx/RemoveItem",
                    data: '{erid: ' + erid + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: examRoom.OnSuccessRemove,
                    failure: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                        $("#modalWaitDialog").modal('hide');
                    },
                    error: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                        $("#modalWaitDialog").modal('hide');
                    }
                });
            },
            OnSuccessRemove: function (response) {
                var title = "";
                var body = "";

                var r = JSON.parse(response.d);
                if (r.success) {
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101245") %>';

                    examRoom.ReloadListData();
                }
                else {
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121007") %>:' + r.message;
                }

                $("#modalWaitDialog").modal('hide');

                $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                $("#modalNotifyOnlyClose").find('.modal-body p').html(body);
                $("#modalNotifyOnlyClose").modal('show');
            },
            ReloadListData: function () {
                examRoom.dt.draw();
            }
        }

        function ShowRegisterExamRoomForm(erid) {

            $("#modalExamRoomForm").modal('show');

            examRoom.ExamRoomID = erid;

            if (examRoom.ExamRoomID == 0) {
                $('#sltLevel').selectpicker('val', 'default').prop('disabled', false);
                $('#sltPlan').selectpicker('val', 'default').prop('disabled', false);
                $('#iptExamRoomName').val('').prop('disabled', false);
                $('#iptSeats').val('').prop('disabled', false);
            }
            else {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterExamRoom.aspx/GetRegisterExamRoomData",
                    data: '{erid: ' + erid + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var r = $.parseJSON(response.d);
                        //console.log(r);

                        if (r.success) {
                            $('#sltLevel').selectpicker('val', r.data.nTSubLevel).prop('disabled', true);
                            $('#sltPlan').selectpicker('val', r.data.RegisterPlanSetupID).prop('disabled', true);
                            $('#iptExamRoomName').val(r.data.ExamRoomName);
                            $('#iptSeats').val(r.data.Seats);
                        }
                        else {
                            Swal.fire({
                                title: 'Warning!',
                                text: 'Warning Message - ' + r.message,
                                type: 'warning',
                                confirmButtonClass: "btn btn-warning",
                                buttonsStyling: false
                            });
                        }
                    },
                    failure: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');
                    },
                    error: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');
                    }
                });

            }
        }

        function LoadPlan(subLevelID, objResult) {
            if (subLevelID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterExamRoom.aspx/LoadPlan",
                    data: '{subLevelID: ' + subLevelID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var subLevel2 = response.d;

                        $(objResult).empty();

                        if (subLevel2.length > 0) {

                            var options = '';
                            $(subLevel2).each(function () {

                                options += '<option value="' + this.id + '">' + this.name + '</option>';

                            });

                            $(objResult).html(options);
                            //$(objResult).selectpicker('refresh');
                        }

                        $(objResult).selectpicker('refresh');
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

        $(document).ready(function () {

            // Validate rule for examRoom
            $("#frmExamRoom").validate({
                rules: {
                    sltLevel: "required",
                    sltPlan: "required",
                    iptExamRoomName: "required",
                    iptSeats: {
                        required: true,
                        number: true
                    }
                },
                messages: {
                    sltLevel: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltPlan: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptExamRoomName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptSeats: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    }
                },
                focusInvalid: false,
                invalidHandler: function () {
                    $(this).find(":input.error:first").focus();
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "iptExamRoomName":
                        case "iptSeats": error.insertAfter(element); break;
                        case "sltLevel":
                        case "sltPlan": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });


            // Search
            $("#sltPlanSearch").change(function () {

            });

            $("#sltLevel").change(function () {

                LoadPlan($(this).val(), '#sltPlan');

            });

            // Searching, Pagination event 
            $('#btnSearch').click(function () {

                examRoom.PageIndex = 0;

                examRoom.ReloadListData();

                return false;
            });

            $('#datatables_length').change(function () {

                examRoom.PageSize = parseInt($("#datatables_length").children("option:selected").val());
                examRoom.PageIndex = 0;

                examRoom.ReloadListData();

                return false;
            });

            $('ul.pagination').on('click', 'li.paginate_button a', function () {

                var pi = parseInt($(this).attr("data-dt-idx"));

                if (pi == 100) {
                    if (examRoom.PageIndex > 0) {
                        examRoom.PageIndex--;
                        examRoom.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + examRoom.PageIndex + ']').addClass('active');
                    }
                }
                else if (pi == 101) {
                    if (examRoom.PageIndex < (examRoom.PageCount - 1)) {
                        examRoom.PageIndex++;
                        examRoom.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + examRoom.PageIndex + ']').addClass('active');
                    }
                }
                else {
                    examRoom.PageIndex = parseInt($(this).attr("data-dt-idx"));
                    examRoom.ReloadListData();

                    $('.pagination .paginate_button.page-item.active').removeClass('active');
                    $(this).addClass('active');
                }

                return false;
            });

            $('#tableData #datatables_previous').click(function () {

                if (examRoom.PageIndex > 0) {
                    examRoom.PageIndex--;
                    examRoom.ReloadListData();
                }

                return false;
            });

            $('#tableData #datatables_next').click(function () {

                if (examRoom.PageIndex < (examRoom.PageCount - 1)) {
                    examRoom.PageIndex++;
                    examRoom.ReloadListData();
                }

                return false;
            });

            $('#btnExamRoomNew').click(function () {

                // Open modal form (add)
                ShowRegisterExamRoomForm(0);

                return false;
            });

            $('#btnSaveExamRoom').click(function () {

                if ($('#frmExamRoom').valid()) {

                    $("#modalWaitDialog").modal('show');

                    // Save command
                    var data = {
                        examRoomId: examRoom.ExamRoomID,
                        levelId: $("#sltLevel").children("option:selected").val(),
                        planId: $("#sltPlan").children("option:selected").val(),
                        examRoomName: $("#iptExamRoomName").val(),
                        seats: $("#iptSeats").val()
                    };
                    examRoom.SaveItem(data);
                }

                return false;
            });

            // Modal Section
            $('#modalNotifyConfirmRemove').on('show.bs.modal', function (e) {
                $title = $(e.relatedTarget).attr('data-title');
                $(this).find('.modal-title').text($title);
                $message = $(e.relatedTarget).attr('data-message');
                $(this).find('.modal-body p').text($message);
                $erid = $(e.relatedTarget).attr('erid');
                $(this).find('.modal-footer #modalConfirmRemove').attr('erid', $erid);
            });
            $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').on('click', function () {

                $('#modalNotifyConfirmRemove').modal('hide');

                $("#modalWaitDialog").modal('show');

                // Remove command
                examRoom.RemoveItem($(this).attr('erid'));

            });

            $('#modalNotifyOnlyClose').on('hidden.bs.modal', function () {
                if (($("#modalWaitDialog").data('bs.modal') || {})._isShown) {
                    setTimeout(function () {
                        $("#modalWaitDialog").modal('hide');
                    }, 1000);
                }
            });

            // Init
            $('#iptSeats').number(true, 0);

            // Datatable Section
            examRoom.LoadListData();

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
