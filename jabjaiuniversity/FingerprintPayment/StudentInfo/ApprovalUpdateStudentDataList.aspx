<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ApprovalUpdateStudentDataList.aspx.cs" Inherits="FingerprintPayment.StudentInfo.ApprovalUpdateStudentDataList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <!-- bootstrap-toggle -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" />

    <style>
        .studentList .dropdown .dropdown-toggle:after {
            margin-right: -18px;
        }

        .studentList .btn.btn-warning {
            width: 129px;
            color: #fff !important;
        }

        .studentList .section {
            font-size: 12px;
            margin: 0px 0px -8px 0px;
        }

        #modalOnOffRequestUpdateData .toggle.toggle-switch {
            width: 100px !important;
            height: 40px !important;
            border-radius: 5px !important;
            border: 1px solid transparent;
        }

            #modalOnOffRequestUpdateData .toggle.toggle-switch .toggle-handle {
                height: 89%;
                left: 0px;
                top: 5%;
                padding: 0 10px;
            }

        #modalOnOffRequestUpdateData .toggle-handle.btn {
            color: #333;
            background-color: #fff;
            border-color: #ccc;
            padding: 6px 12px;
        }

        #modalOnOffRequestUpdateData .form-check .form-check-label .circle {
            height: 25px;
            width: 25px;
        }

            #modalOnOffRequestUpdateData .form-check .form-check-label .circle .check {
                height: 25px;
                width: 25px;
            }

        #modalOnOffRequestUpdateData .form-check .form-check-label {
            padding-left: 40px;
            line-height: 2.2;
            color: black;
        }

        #modalOnOffRequestUpdateData label[for=iptStartDate], #modalOnOffRequestUpdateData label[for=iptEndDate] {
            color: black;
        }

        #modalOnOffRequestUpdateData .form-check-inline {
            margin-bottom: 0px;
        }

        #modalOnOffRequestUpdateData .div-datepicker {
            display: inline-block;
        }

        #modalOnOffRequestUpdateData .form-control-feedback {
            color: #000;
            opacity: 1;
            right: -2px;
        }

        .mt-07rem, .my-07rem {
            margin-top: 0.7rem !important;
        }

        .lh-13em {
            line-height: 1.3em;
        }

        .note {
            font-size: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101349") %>
            </p>
        </div>
    </div>

    <div class="studentList row">
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
                        <div class="col-sm-3">
                            <select id="sltLevel" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %>">
                                <asp:Literal ID="ltrLevel" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                        <div class="col-sm-3">
                            <select id="sltClass" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                        <div class="col-sm-3">
                            <div class="form-group bmd-form-group">
                                <input id="iptStudentName" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101013") %>">
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
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101349") %></h4>
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
                                                <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></th>
                                                <th style="width: 17%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                                                <th style="width: 18%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101350") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101351") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101352") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                                <th style="width: 10%" class="text-center">
                                                    <div class="btn-group">
                                                        <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103029") %><span class="caret"></span>
                                                        </button>
                                                        <ul class="dropdown-menu pull-right">
                                                            <li><a href="#" data-toggle="modal" data-target="#modalOnOffRequestUpdateData" onclick="return OnOffRequestUpdateData();"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101356") %></a></li>
                                                        </ul>
                                                    </div>
                                                </th>
                                                <th><%--sID--%></th>
                                                <th><%--RequestDate--%></th>
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

    <div id="modalOnOffRequestUpdateData" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto;">
        <div class="modal-dialog global-modal">
            <div class="modal-content">
                <div class="modal-header" style="">
                    <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101356") %></h4>
                </div>
                <div class="modal-body pt-2 pb-0">
                    <div class="row">
                        <div class="col-md-12 p-0 text-left">
                            <input id="iptOpenEditUserProfile" type="checkbox" data-on="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101117") %>" data-off="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>" data-toggle="toggle" data-onstyle="success" data-height="40" data-width="100" data-style="toggle-switch" />
                        </div>
                    </div>
                    <div class="row on-approve d-none">
                        <div class="col-md-12 p-0 pt-3 text-left">
                            <div class="form-check form-check-inline">
                                <label class="form-check-label" for="iptApproveOption1">
                                    <input class="form-check-input" type="radio" name="iptApproveOption" id="iptApproveOption1" value="1" checked />
                                    <span class="label-text" style="display: contents;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101359") %></span>
                                    <span class="circle">
                                        <span class="check"></span>
                                    </span>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="row justify-content-end on-approve d-none">
                        <div class="col-md-12 p-0 pt-1 text-left">
                            <div class="float-left mt-07rem">
                                <div class="form-check form-check-inline">
                                    <label class="form-check-label" for="iptApproveOption2">
                                        <input class="form-check-input" type="radio" name="iptApproveOption" id="iptApproveOption2" value="2" />
                                        <span class="label-text" style="display: contents;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101360") %></span>
                                        <span class="circle">
                                            <span class="check"></span>
                                        </span>
                                    </label>
                                </div>
                            </div>
                            <div class="float-right specify-time d-none">
                                <label for="iptStartDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101361") %> :</label>
                                <div class="form-group div-datepicker">
                                    <input id="iptStartDate" name="iptStartDate" type="text" class="form-control datepicker" value="<%=DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %>" />
                                    <span class="form-control-feedback">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row justify-content-end on-approve specify-time d-none">
                        <div class="col-md-12 p-0 text-right">
                            <label for="iptEndDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603060") %></label>
                            <div class="form-group div-datepicker">
                                <input id="iptEndDate" name="iptEndDate" type="text" class="form-control datepicker" value="<%=DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %>" />
                                <span class="form-control-feedback">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 p-0 text-left">
                            <p class="note text-danger mb-0 pt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101358") %></p>
                        </div>
                    </div>
                </div>
                <div class="modal-footer pt-3" style="display: block; text-align: center;">
                    <button type="button" id="btnSaveOnOffRequestUpdateData" class="btn btn-success global-btn">
                        <i class="material-icons">save</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    <button type="button" id="btnCancelOnOffRequestUpdateData" class="btn btn-danger global-btn" data-dismiss="modal">
                        <i class="material-icons">close</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script type='text/javascript' src="/Scripts/init-function.js?v=<%=DateTime.Now.Ticks%>"></script>

    <!-- bootstrap-toggle -->
    <script type='text/javascript' src="/assets/plugins/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>

    <script type="text/javascript">

        var studentList = {
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            TotalRows: 0,
            dt: null,
            LoadListData: function () {
                studentList.dt = $('.studentList #tableData').DataTable({
                    "processing": true,
                    "serverSide": true,
                    "info": false,
                    "searching": false,
                    "paging": false,
                    "stateSave": false,
                    "ajax": {
                        "url": "ApprovalUpdateStudentDataList.aspx/LoadRequestUpdateStudent",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.level = $(".studentList #sltLevel").children("option:selected").val();
                            d.classId = $(".studentList #sltClass").children("option:selected").val();
                            d.studentName = $(".studentList #iptStudentName").val();
                            d.page = studentList.PageIndex;
                            d.length = studentList.PageSize;

                            return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                        },
                        "dataSrc": function (json) {
                            var jsond = $.parseJSON(json.d);
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
                        { "data": "Code", "orderable": true },
                        { "data": "Classroom", "orderable": true },
                        { "data": "Name", "orderable": true },
                        { "data": "Lastname", "orderable": true },
                        { "data": "RequestDate", "orderable": true, "className": "text-center lh-13em" },
                        { "data": "ApproveDate", "orderable": true, "className": "text-center lh-13em" },
                        { "data": "Status", "orderable": true }, //7
                        { "data": "action", "orderable": false }, //8
                        { "data": "sid", "orderable": false },
                        { "data": "requestDate", "orderable": false }
                    ],
                    "order": [[5, "desc"]],
                    "columnDefs": [
                        { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8] },
                        { "targets": [9, 10], "visible": false },
                        {
                            "render": function (data, type, row) {
                                var status = '';
                                switch (data) {
                                    case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101353") %>': status = '<span class="text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101353") %></span>'; break;
                                    case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102233") %>': status = '<span class="text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102233") %></span>'; break;
                                    case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102232") %>': status = '<span class="text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102232") %></span>'; break;
                                    case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101354") %>': status = '<span class="text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101354") %></span>'; break;
                                }
                                //profile, permanent-address, contact-address, father-info, mother-info, parent-info
                                //switch (row.section) {
                                //    case 'profile': status += '<p class="text-secondary section">( <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210022") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %> )</p>'; break;
                                //    case 'permanent-address': status += '<p class="text-secondary section">( <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %> )</p>'; break;
                                //    case 'contact-address': status += '<p class="text-secondary section">( <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101147") %> )</p>'; break;
                                //    case 'father-info': status += '<p class="text-secondary section">( <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101180") %> )</p>'; break;
                                //    case 'mother-info': status += '<p class="text-secondary section">( <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00194") %> )</p>'; break;
                                //    case 'parent-info': status += '<p class="text-secondary section">( <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %> )</p>'; break;
                                //}
                                return status;
                            },
                            "targets": 7
                        },
                        {
                            "render": function (data, type, row) {
                                return '<a class="btn btn-warning check-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101355") %></a>';
                            },
                            "targets": 8
                        }
                    ],
                    "drawCallback": function (settings) {
                        //var json = settings.json;
                        var json = $.parseJSON(settings.json.d);
                        // console.log(json);

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
                    },
                    "createdRow": function (row, data, dataIndex) {
                    }
                });

                // order.dt search.dt
                studentList.dt.on('draw.dt', function () {
                    studentList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (studentList.PageIndex * studentList.PageSize) + (i + 1) + '.';
                    });
                    studentList.dt.column(8, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        var sid = studentList.dt.cells({ row: i, column: 9 }).data()[0];
                        var requestDate = studentList.dt.cells({ row: i, column: 10 }).data()[0];
                        $(cell).find(".check-info").attr("href", "ApprovalStudentDataForm.aspx?sid=" + sid + "&requestDate=" + requestDate);
                    });
                });
            },
            ReloadListData: function () {
                studentList.dt.draw();
            }
        };

        function OnOffRequestUpdateData() {
            // Load config
            $.ajax({
                type: "POST",
                url: "ApprovalUpdateStudentDataList.aspx/GetApprovalConfig",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var r = JSON.parse(response.d);
                    //console.log(r);

                    if (r.success) {
                        $('#iptOpenEditUserProfile').bootstrapToggle(r.data.isOpenApproveUserProfile ? 'on' : 'off');
                        if (r.data.isOpenApproveUserProfile) {
                            $('#iptApproveOption' + r.data.approveOption).click();
                            if (r.data.approveOption == 2) {
                                $("#iptStartDate").val(r.data.approveStartDate);
                                $("#iptEndDate").val(r.data.approveEndDate);
                            }
                        }
                    }
                },
                beforeSend: function () {
                    // Handle the beforeSend event
                    //$("body").mLoading();
                },
                complete: function () {
                    // Handle the complete event
                    //$("body").mLoading('hide');
                },
                failure: function (response) {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');
                },
                error: function (response) {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');
                }
            });

        }

        function LoadTermSubLevel2(subLevelID, objResult) {
            if (subLevelID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "ApprovalUpdateStudentDataList.aspx/LoadTermSubLevel2",
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

            $('#iptOpenEditUserProfile').change(function () {
                //console.log('toggle: ' + $(this).prop('checked'));
                if ($(this).prop('checked')) {
                    // on
                    $(".on-approve").removeClass('d-none');

                    switch ($('input[name=iptApproveOption]:checked').val()) {
                        case '2': $('.specify-time').removeClass('d-none'); break;
                        default: $('.specify-time').addClass('d-none'); break;
                    }

                    // message on
                    //$('.note.text-danger').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133342") %>');
                    $('.note.text-danger').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101357") %>');
                }
                else {
                    // off
                    $(".on-approve").addClass('d-none');

                    // message off
                    $('.note.text-danger').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101358") %>');
                }
            });

            $(document).on('change', 'input:radio[id^="iptApproveOption"]', function (event) {
                //console.log($(this).val());
                switch ($(this).val()) {
                    case '2': $('.specify-time').removeClass('d-none'); break;
                    default: $('.specify-time').addClass('d-none'); break;
                }
            });

            $('#btnSaveOnOffRequestUpdateData').click(function () {

                $("#modalWaitDialog").modal('show');

                // Prepare data
                var configData = {
                    isOpenApproveUserProfile: $('#iptOpenEditUserProfile').prop('checked'),
                    approveOption: typeof $('input[name=iptApproveOption]:checked').val() === "undefined" ? '' : $('input[name=iptApproveOption]:checked').val(),
                    approveStartDate: $('#iptStartDate').val(),
                    approveEndDate: $('#iptEndDate').val()
                };

                // Save command
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "ApprovalUpdateStudentDataList.aspx/SaveApprovalConfig",
                    data: JSON.stringify({ configData }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var title = "";
                        var body = "";

                        var r = JSON.parse(response.d);
                        if (r.success) {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101363") %>';

                            // After close message popup
                            $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {
                                // Close popup
                                $("#modalOnOffRequestUpdateData").modal('hide');
                            });
                        }
                        else {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111015") %>[' + r.message + ']';
                        }
                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                        $("#modalNotifyOnlyClose").modal('show');
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

            // Searching
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


            // Search
            $(".studentList #sltLevel").change(function () {

                LoadTermSubLevel2($(this).val(), '#sltClass');

            });

            // Modal Section
            $('#modalNotifyConfirmSave').off().on('show.bs.modal', function (e) {
                $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111017") %>');
                //$(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
            });

            $('#modalNotifyOnlyClose').on('hidden.bs.modal', function () {
                if (($("#modalWaitDialog").data('bs.modal') || {})._isShown) {
                    setTimeout(function () {
                        $("#modalWaitDialog").modal('hide');
                    }, 1000);
                }
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
                        var param = { keyword: qry, termID: '<%=CurrentTermID%>' };
                        $.ajax({
                            url: "ApprovalUpdateStudentDataList.aspx/GetStudentName",
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

            // Initial control (.datepicker)
            $('#iptStartDate').datetimepicker({
                keepOpen: false,
                debug: false,
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
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
            }).on('dp.change', function (e) {
                $('#iptEndDate').data("DateTimePicker").minDate(e.date);
            });

            $('#iptEndDate').datetimepicker({
                keepOpen: false,
                debug: false,
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
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
            }).on('dp.change', function (e) {
                $('#iptStartDate').data("DateTimePicker").maxDate(e.date);
            });

            $('#iptStartDate').data("DateTimePicker").maxDate('<%=DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %>');
            $('#iptEndDate').data("DateTimePicker").minDate('<%=DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %>');

            $(".datepicker").attr('maxlength', '10');

            $('#iptOpenEditUserProfile').bootstrapToggle();

            // Datatable Section
            studentList.LoadListData();

        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
