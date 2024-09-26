<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="FingerprintPayment.Employees.EmployeeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        .dropdown .dropdown-toggle:after {
            margin-right: -18px;
        }

        .form-control:disabled, .form-control[readonly] {
            background-color: transparent;
            opacity: 1;
        }

        .modal small {
            display: block;
            font-size: 65%;
            margin-top: -2px;
        }

        .modal .form-group {
            margin: 0px 0 0;
        }

        .row.h41 {
            height: 41px;
        }

        .auto-gen-pass, .create-pass {
            display: none;
            margin-top: -12px;
            padding-bottom: 10px;
        }

        label.error {
            display: flex;
            font-size: 11px !important;
            margin-top: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102002") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102003") %>
            </p>
        </div>
    </div>

    <div class="employeeList row">
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
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %></label>
                        <div class="col-sm-3">
                            <select id="sltSearchEmpType" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102005") %>">
                                <asp:Literal ID="ltrEmpType" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                        <div class="col-sm-3">
                            <input type="text" class="form-control" id="iptSearchName" name="iptSearchName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %>">
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

    <div class="employeeList row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">account_circle</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102003") %></h4>
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
                                                <th style="width: 14%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401083") %>
                                                    <p style="height: 0px; margin: -2px 0px 2px 0px; font-size: 0.9em;">(Username)</p>
                                                </th>
                                                <th style="width: 8%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                                                <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                                <th style="width: 15%" class="text-center">
                                                    <div class="btn-group">
                                                        <button type="button" data-cy="menu-employee" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103029") %><span class="caret"></span>
                                                        </button>
                                                        <ul class="dropdown-menu pull-right">
                                                            <li><a data-cy="new-employee" href="/Employees/EmployeeDetail.aspx?v=view&eid=0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102006") %></a></li>
                                                            <li><a data-cy="import-employee" href="/Employees/ImportEmployeeData.aspx"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102007") %></a></li>
                                                            <li><a target="_blank" data-cy="import-image-employee" href="/StudentInfo/ProfileImage.aspx"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102008") %></a></li>
                                                        </ul>
                                                    </div>
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

    <div id="modalShowPassword" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 10%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="width: 650px; max-width: 900px;">
                <div class="modal-header text-center" style="padding: 0px; display: block;">
                    <h3 id="modalTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101226") %><small>Password Information</small></h3>
                </div>
                <div class="modal-body product-add-container">
                    <div id="divShowPassword">
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-3 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101227") %><small>User</small></label>
                            <div class="col-sm-8">
                                <div class="form-group bmd-form-group" style="padding-right: 25px; margin-top: 7px;">
                                    <span id="spnFullname" style="padding-left: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111079") %></span>
                                    <input id="iptUsername" type="text" class="form-control" readonly="readonly" data-sid="" style="width: 70%; display: none;" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-3 col-form-label text-left text-nowrap"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101228") %><small>First Password</small></label>
                            <div class="col-sm-8">
                                <div class="input-group" id="showHidePassword" style="display: inline-flex; width: 60%;">
                                    <input id="iptPassword" class="form-control" type="password" value="0000" placeholder="password" readonly="readonly" style="padding-left: 0px;" />
                                    <div class="input-group-addon" style="margin-left: -30px;">
                                        <a href="" class="btn btn-primary global-btn" style="padding: 5px 6px;"><i class="fa fa-eye-slash" aria-hidden="true" style=""></i></a>
                                    </div>
                                </div>
                                <button type="button" id="btnChangePassword" class="btn btn-primary" style="padding: 5px 15px 2px 13px; font-size: 10px;"><i class="fa fa-key" aria-hidden="true" style="margin-top: -10px; margin-right: 7px; font-size: 15px;"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101229") %><small style="padding-left: 20px;">Change Password</small></button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-3 col-form-label text-left">PIN</label>
                            <div class="col-sm-8">
                                <button type="button" id="btnResetPIN" class="btn btn-outline-primary">Reset PIN</button>
                            </div>
                        </div>
                    </div>
                    <div id="divResetPassword" style="display: none;">
                        <form id="frmResetPassword">
                            <div class="row">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101227") %><small>User</small></label>
                                <div class="col-sm-8">
                                    <div class="form-group bmd-form-group" style="padding-right: 25px; margin-top: 7px;">
                                        <span id="spnFullname2" style="padding-left: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111079") %></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row h41">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-11">
                                    <input type="radio" name="rdoResetPassword" id="rdoAutoGeneratePassword" value="auto-gen-pass" />
                                    <label class="form-check-label" for="rdoAutoGeneratePassword" style="font-weight: normal; vertical-align: middle; margin-left: 5px; color: black;">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101231") %><small>Automatically Generate Password</small>
                                    </label>
                                </div>
                            </div>
                            <div class="row auto-gen-pass">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label text-left text-nowrap" style="padding-left: 38px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101232") %><small>New Password</small></label>
                                <div class="col-sm-8">
                                    <div class="input-group" id="showNewPassword" style="display: inline-flex; width: 60%;">
                                        <input id="iptNewPassword" name="iptNewPassword" class="form-control" type="text" style="padding-left: 0px; padding-right: 30px; text-align: center;" maxlength="20" />
                                        <div class="input-group-addon" style="margin-left: -30px;">
                                            <a href="" class="btn btn-primary global-btn eye" style="padding: 5px 6px;"><i class="fa fa-eye" aria-hidden="true" style=""></i></a>
                                            <a href="" class="btn btn-primary global-btn refresh" style="padding: 5px 6px;"><i class="fa fa-refresh" aria-hidden="true" style=""></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row h41">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-11">
                                    <input type="radio" name="rdoResetPassword" id="rdoCreatePassword" value="create-pass" />
                                    <label class="form-check-label" for="rdoCreatePassword" style="font-weight: normal; vertical-align: middle; margin-left: 5px; color: black;">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101233") %><small>Create Password</small>
                                    </label>
                                </div>
                            </div>
                            <div class="row create-pass">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label text-left text-nowrap" style="padding-left: 38px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101232") %><small>New Password</small></label>
                                <div class="col-sm-8">
                                    <div class="input-group" id="showNewPassword2" style="display: inline-flex; width: 60%;">
                                        <input id="iptNewPassword2" name="iptNewPassword2" class="form-control" type="text" style="padding-left: 0px; padding-right: 30px; text-align: center;" maxlength="20" />
                                        <div class="input-group-addon" style="margin-left: -30px;">
                                            <a href="" class="btn btn-primary global-btn eye" style="padding: 5px 6px;"><i class="fa fa-eye" aria-hidden="true" style=""></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row create-pass">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-11 col-form-label text-left text-nowrap" style="padding-left: 38px; font-size: 10px; margin-top: -7px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101234") %><small>Password must be at least 6 characters.</small></label>
                            </div>
                            <div class="row h41">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-11">
                                    <input type="checkbox" id="chkConfirmMailToParent" disabled />
                                    <label class="form-check-label" for="chkConfirmMailToParent" style="font-weight: normal; vertical-align: middle; margin-left: 5px; color: #ccc;">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101235") %><small>Confirm Password Reset</small>
                                    </label>
                                </div>
                            </div>
                            <div class="row h41">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-11">
                                    <label class="form-check-label" style="font-weight: normal; color: black; font-size: 12px;">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101236") %>
                                        <small style="margin-top: 1px; font-size: 75%;">Note : The application will automatically log the user out, and forced to set a new password after logging in.</small>
                                    </label>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div class="row message_response">
                        <div class="col-xs-12 center">
                            <label class="text-danger"></label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnClose" class="btn btn-danger global-btn"
                        data-dismiss="modal">
                        <i class="fa fa-close" aria-hidden="true" style="margin-right: 7px;"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    <button type="button" id="btnResetPassword" class="btn btn-success global-btn" style="display: none;">
                        <i class="fa fa-save" aria-hidden="true" style="margin-right: 7px;"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111080") %></button>
                    <button type="button" id="btnCancelResetPassword" class="btn btn-danger global-btn" style="display: none;">
                        <i class="fa fa-close" aria-hidden="true" style="margin-right: 7px;"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>

            </div>
        </div>
    </div>
    <!-- /.modal -->

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type='text/javascript'>

        var employeeList = {
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            TotalRows: 0,
            dt: null,
            LoadListData: function () {
                employeeList.dt = $(".employeeList #tableData").DataTable({
                    "processing": true,
                    "serverSide": true,
                    "info": false,
                    "searching": false,
                    "paging": false,
                    "stateSave": true,
                    "language": {
                        "emptyTable": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102035") %>"
                    },
                    "ajax": {
                        "url": "Ashx/LoadEmployeeList.ashx",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.empType = $(".employeeList #sltSearchEmpType").val();
                            d.studentName = $(".employeeList #iptSearchName").val();
                            d.page = employeeList.PageIndex;
                            d.length = employeeList.PageSize;

                            return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                        },
                        "dataSrc": function (json) {
                            //console.log(json.data);
                            return json.data;
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
                        { "data": "Type", "orderable": true },
                        { "data": "Code", "orderable": true },
                        { "data": "Title", "orderable": true },
                        { "data": "FirstName", "orderable": true },
                        { "data": "LastName", "orderable": true },
                        { "data": "PhoneNumber", "orderable": true },
                        { "data": "Birthday", "orderable": true },
                        { "data": "WorkStatus", "orderable": true },
                        { "data": "action", "orderable": false },
                        { "data": "id", "orderable": false }
                    ],
                    "order": [[2, "desc"]],
                    "columnDefs": [
                        { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9] },
                        { "targets": [10], "visible": false },
                        {
                            "render": function (data, type, row) {
                                return '<a href="#"><i class="fa fa-edit" style="padding-right: 5px; font-size: 22px;"></i></a>' +
                                    ((/true/).test('<%=HaveNewResetPasswordPermission%>'.toLowerCase()) ? '<a href="#"><i class="fa fa-key" style="padding-right: 5px; font-size: 22px;"></i></a>' : '') +
                                    '<a href="#" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101240") %>"><i class="fa fa-remove text-danger" style="font-size: 22px;"></i></a>';
                            },
                            "targets": 9
                        }
                    ],
                    "drawCallback": function (settings) {
                        var json = settings.json;
                        //console.log(json);

                        employeeList.PageCount = json.pageCount;
                        employeeList.TotalRows = json.recordsTotal;

                        var pageLRSize = 3;
                        var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                        var previousDot = '';
                        var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                        var nextDot = '';
                        var elements = '';

                        if (employeeList.PageIndex - pageLRSize > 1) {
                            previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                        }

                        if (employeeList.PageIndex + pageLRSize < json.pageCount - 1) {
                            nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                        }

                        for (var pi = 0; pi < json.pageCount; pi++) {
                            if (pi == 0) {
                                elements += '<li class="paginate_button page-item ' + (employeeList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                elements += previousDot;
                            }
                            else if (pi == json.pageCount - 1) {
                                elements += nextDot;
                                elements += '<li class="paginate_button page-item ' + (employeeList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                            else if (employeeList.PageIndex - pageLRSize <= pi && employeeList.PageIndex + pageLRSize >= pi) {
                                elements += '<li class="paginate_button page-item ' + (employeeList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                        }

                        $('.pagination').html(previous + elements + next);

                        $('.dataTables_info').html('Showing ' + ((employeeList.PageIndex * employeeList.PageSize) + 1) + ' to ' + ((employeeList.PageIndex * employeeList.PageSize) + employeeList.PageSize) + ' of ' + employeeList.TotalRows + ' rows');
                    }
                });
                // order.dt search.dt
                employeeList.dt.on('draw.dt', function () {
                    employeeList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (employeeList.PageIndex * employeeList.PageSize) + (i + 1) + '.';
                    });
                    employeeList.dt.column(9, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        var eid = employeeList.dt.cells({ row: i, column: 10 }).data()[0];
                        $(cell).find(".fa-edit").parent().attr("class", "EmployeeDetail").attr("href", "EmployeeDetail.aspx?v=form&eid=" + eid).attr("target", "_blank").attr("id", eid);
                        if ((/true/).test('<%=HaveNewResetPasswordPermission%>'.toLowerCase())) $(cell).find(".fa-key").parent().attr("onClick", 'ShowPassword(' + eid + ')');
                        $(cell).find(".fa-remove").parent().attr("eid", eid);
                    });
                });
            },
            RemoveItem: function (eid) {
                $.ajax({
                    type: "POST",
                    url: "EmployeeList.aspx/RemoveItem",
                    data: '{eid: ' + eid + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: employeeList.OnSuccessRemove,
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

                    employeeList.ReloadListData();
                }
                else {
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                    switch (r.status) {
                        case 201:
                        case 202:
                        case 203:
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121007") %>:' + r.message;
                            break;
                    }
                }

                $("#modalWaitDialog").modal('hide');

                $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                $("#modalNotifyOnlyClose").find('.modal-body p').html(body);
                $("#modalNotifyOnlyClose").modal('show');
            },
            ReloadListData: function () {
                employeeList.dt.draw();
            }
        }

        function ShowPassword(eid) {
            $.ajax({
                async: false,
                type: "POST",
                url: "EmployeeList.aspx/ShowPassword",
                data: '{eid: ' + eid + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d != "error") {
                        $("span[id^=spnFullname]").text(response.d.userName);
                        $("#iptUsername").val(response.d.userName).attr('data-eid', eid);
                        $("#iptPassword").val(response.d.password);

                        $('#modalTitle').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101226") %><small>Password Information</small>');
                        $('#divShowPassword').show();
                        $('#btnClose').show();

                        $('input[name=rdoResetPassword]').prop('checked', false);
                        $('.auto-gen-pass').hide();
                        $('.create-pass').hide();

                        $('#btnResetPassword').attr('disabled', 'disabled');

                        $('#divResetPassword').hide();
                        $('#btnResetPassword').hide();
                        $('#btnCancelResetPassword').hide();

                        $('#modalShowPassword').modal('show');
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

        $(function () {

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

            $("form").validate({
                rules: {
                    iptNewPassword: {
                        required: function (element) {
                            return ($('input[name="rdoResetPassword"]:radio:checked').val() == 'auto-gen-pass') && !$(element).val();
                        },
                        minlength: 6,
                    },
                    iptNewPassword2: {
                        required: function (element) {
                            return ($('input[name="rdoResetPassword"]:radio:checked').val() == 'create-pass') && !$(element).val();
                        },
                        minlength: 6,
                    }
                },
                messages: {
                    iptNewPassword: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101237") %>",
                        minlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111086") %>",
                    },
                    iptNewPassword2: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101237") %>",
                        minlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111086") %>",
                    }
                },
                focusInvalid: false,
                invalidHandler: function () {
                    $(this).find(":input.error:first").focus();
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "iptNewPassword":
                        case "iptNewPassword2": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            // Searching, Pagination event 
            $('.employeeList #btnSearch').click(function () {

                employeeList.PageIndex = 0;

                employeeList.ReloadListData();

                return false;
            });

            $('.employeeList #datatables_length').change(function () {

                employeeList.PageSize = parseInt($("#datatables_length").children("option:selected").val());
                employeeList.PageIndex = 0;

                employeeList.ReloadListData();

                return false;
            });

            $('.employeeList ul.pagination').on('click', 'li.paginate_button a', function () {

                var pi = parseInt($(this).attr("data-dt-idx"));

                if (pi == 100) {
                    if (employeeList.PageIndex > 0) {
                        employeeList.PageIndex--;
                        employeeList.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + employeeList.PageIndex + ']').addClass('active');
                    }
                }
                else if (pi == 101) {
                    if (employeeList.PageIndex < (employeeList.PageCount - 1)) {
                        employeeList.PageIndex++;
                        employeeList.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + employeeList.PageIndex + ']').addClass('active');
                    }
                }
                else {
                    employeeList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                    employeeList.ReloadListData();

                    $('.pagination .paginate_button.page-item.active').removeClass('active');
                    $(this).addClass('active');
                }

                return false;
            });

            $('.employeeList #tableData #datatables_previous').click(function () {

                if (employeeList.PageIndex > 0) {
                    employeeList.PageIndex--;
                    employeeList.ReloadListData();
                }

                return false;
            });

            $('.employeeList #tableData #datatables_next').click(function () {

                if (employeeList.PageIndex < (employeeList.PageCount - 1)) {
                    employeeList.PageIndex++;
                    employeeList.ReloadListData();
                }

                return false;
            });


            // Modal Section
            $('#modalNotifyConfirmRemove').on('show.bs.modal', function (e) {
                $title = $(e.relatedTarget).attr('data-title');
                $(this).find('.modal-title').text($title);
                $message = $(e.relatedTarget).attr('data-message');
                $(this).find('.modal-body p').text($message);
                $eid = $(e.relatedTarget).attr('eid');
                $(this).find('.modal-footer #modalConfirmRemove').attr('eid', $eid);
            });
            $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').on('click', function () {

                $('#modalNotifyConfirmRemove').modal('hide');

                $("#modalWaitDialog").modal('show');

                // Remove command
                employeeList.RemoveItem($(this).attr('eid'));

            });

            $('#modalNotifyOnlyClose').on('hidden.bs.modal', function () {
                if (($("#modalWaitDialog").data('bs.modal') || {})._isShown) {
                    setTimeout(function () {
                        $("#modalWaitDialog").modal('hide');
                    }, 1000);
                }
            });


            $("#showHidePassword a").on('click', function (event) {
                event.preventDefault();
                if ($('#showHidePassword input').attr("type") == "text") {
                    $('#showHidePassword input').attr('type', 'password');
                    $('#showHidePassword i').addClass("fa-eye-slash").removeClass("fa-eye");
                } else if ($('#showHidePassword input').attr("type") == "password") {
                    $('#showHidePassword input').attr('type', 'text');
                    $('#showHidePassword i').removeClass("fa-eye-slash").addClass("fa-eye");
                }
            });

            $("#showNewPassword a.eye").on('click', function (event) {
                event.preventDefault();
                if ($('#showNewPassword input').attr("type") == "text") {
                    $('#showNewPassword input').attr('type', 'password');
                    $('#showNewPassword i.fa-eye').addClass("fa-eye-slash").removeClass("fa-eye");
                } else if ($('#showNewPassword input').attr("type") == "password") {
                    $('#showNewPassword input').attr('type', 'text');
                    $('#showNewPassword i.fa-eye-slash').removeClass("fa-eye-slash").addClass("fa-eye");
                }
            });

            $("#showNewPassword2 a.eye").on('click', function (event) {
                event.preventDefault();
                if ($('#showNewPassword2 input').attr("type") == "text") {
                    $('#showNewPassword2 input').attr('type', 'password');
                    $('#showNewPassword2 i.fa-eye').addClass("fa-eye-slash").removeClass("fa-eye");
                } else if ($('#showNewPassword2 input').attr("type") == "password") {
                    $('#showNewPassword2 input').attr('type', 'text');
                    $('#showNewPassword2 i.fa-eye-slash').removeClass("fa-eye-slash").addClass("fa-eye");
                }
            });

            $("#showNewPassword a.refresh").on('click', function (event) {
                event.preventDefault();
                $('#showNewPassword input').val(Math.random().toString(16).slice(-10).toUpperCase());
            });

            $('input[name="rdoResetPassword"]:radio').on('change', function (e) {

                switch ($(this).val()) {
                    case "auto-gen-pass":
                        $('.auto-gen-pass').css('display', 'flex');
                        $('.create-pass').hide();
                        $('#showNewPassword input').val(Math.random().toString(16).slice(-10).toUpperCase());
                        break;
                    case "create-pass":
                        $('.auto-gen-pass').hide();
                        $('.create-pass').css('display', 'flex');
                        break;
                }

                $('#btnResetPassword').removeAttr('disabled');

                return false;
            });

            $('#btnChangePassword').click(function () {

                $('#divShowPassword').hide();
                $('#btnClose').hide();

                $('#modalTitle').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101230") %><small>Reset Password</small>');
                $('#divResetPassword').show();
                $('#btnResetPassword').show();
                $('#btnCancelResetPassword').show();

                return false;
            });

            $('#btnCancelResetPassword').click(function () {

                $('#modalTitle').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101226") %><small>Password Information</small>');
                $('#divShowPassword').show();
                $('#btnClose').show();

                $('#divResetPassword').hide();
                $('#btnResetPassword').hide();
                $('#btnCancelResetPassword').hide();

                return false;
            });

            $('#btnResetPIN').click(function () {

                $("#modalWaitDialog").modal('show');

                var eid = $("#iptUsername").attr('data-eid');

                $.ajax({
                    async: false,
                    type: "POST",
                    url: "EmployeeList.aspx/ResetPIN",
                    data: '{eid: ' + eid + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var title = "";
                        var body = "";

                        switch (response.d) {
                            case "success":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01751") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111087") %>';

                                break;
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

            $('#btnResetPassword').click(function () {

                if ($("#frmResetPassword").valid()) {

                    var newPassword = '';
                    switch ($('input[name="rdoResetPassword"]:radio:checked').val()) {
                        case 'auto-gen-pass': newPassword = $('#iptNewPassword').val(); break;
                        case 'create-pass': newPassword = $('#iptNewPassword2').val(); break;
                    }

                    var data = {
                        "eid": $("#iptUsername").attr('data-eid'),
                        "newPassword": newPassword
                    }

                    $("#modalWaitDialog").modal('show');

                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "EmployeeList.aspx/ResetPassword",
                        data: "{resetPasswordInfo:" + JSON.stringify(data) + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {

                            var title = "";
                            var body = "";

                            var res = $.parseJSON(response.d);
                            //console.log(res);
                            switch (res.result) {
                                case "success":
                                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101238") %>';

                                    $('#modalShowPassword').modal('hide');

                                    break;
                                case "error":
                                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111075") %>[' + res.message + ']';

                                    break;
                            }

                            $('#modalShowPassword').modal('hide');
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
                }

                return false;
            });

            $('.employeeList #iptSearchName').autoComplete({
                resolverSettings: {
                    fail: function () {
                        console.log('fail');
                    }
                },
                resolver: 'custom',
                events: {
                    search: function (qry, callback) {
                        var param = { keyword: qry, termID: $('.employeeList #sltTerm').val() };
                        $.ajax({
                            url: "EmployeeList.aspx/GetEmployeeName",
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


            // Datatable Section
            employeeList.LoadListData();

        });

    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
