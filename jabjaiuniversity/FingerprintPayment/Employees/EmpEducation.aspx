<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpEducation.aspx.cs" Inherits="FingerprintPayment.Employees.EmpEducation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <div class="empEducationList">
                <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102049") %></p>
                <div class="material-datatables data-table">
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
                                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01072") %></th>
                                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01072") %></th>
                                            <th style="width: 30%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102045") %></th>
                                            <th style="width: 35%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102050") %></th>
                                            <th style="width: 10%" class="text-center">
                                                <button type="button" id="btnEmpEducation" class="btn btn-success col-md-12" data-toggle="modal" data-target="#modalShowForm" form-name="EmpEducation.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102051") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></button></th>
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

                <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102065") %></p>
                <div class="material-datatables data-table">
                    <div id="datatables_wrapper2" class="dataTables_wrapper dt-bootstrap4">
                        <div class="row">
                            <div class="col-sm-12 col-md-6">
                                <div class="dataTables_length">
                                    <label>
                                        Show
                                            <select id="datatables_length2" aria-controls="datatables" class="custom-select custom-select-sm form-control form-control-sm" style="text-align-last: center;">
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
                                <table id="tableData2" class="table table-no-bordered table-hover" cellspacing="0" width="100%" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th style="width: 35%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102066") %></th>
                                            <th style="width: 30%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102067") %></th>
                                            <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102130") %></th>
                                            <th style="width: 10%" class="text-center">
                                                <button type="button" id="btnEmpFame" class="btn btn-success col-md-12" data-toggle="modal" data-target="#modalShowForm" form-name="EmpFame.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102069") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></button></th>
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

            <script type="text/javascript">

                var empEducationList = {
                    PageIndex: 0,
                    PageSize: 20,
                    PageCount: 0,
                    TotalRows: 0,
                    dt: null,
                    LoadListData: function (eid) {
                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102049") %>
                        empEducationList.dt = $(".empEducationList #tableData").DataTable({
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
                                "url": "Ashx/LoadEmpEducationList.ashx",
                                "type": "POST",
                                "contentType": "application/json; charset=utf-8",
                                "data": function (d) {
                                    d.eid = eid;
                                    d.search = '';
                                    d.page = empEducationList.PageIndex;
                                    d.length = empEducationList.PageSize;

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
                                { "data": "StudyYear", "orderable": true },
                                { "data": "GraduationYear", "orderable": true },
                                { "data": "Level", "orderable": true },
                                { "data": "Institution", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[6, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 3, 4, 5] },
                                { "targets": [6], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="EmpEducation.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102051") %>" style="padding-right: 5px; font-size: 22px;"><i class="fa fa-edit"></i></a>' +
                                            '<a href="#" class="remove-row" data-title="Confirm Dialog" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101240") %>"><i class="fa fa-remove text-danger" style="font-size: 22px;"></i></a>';
                                    },
                                    "targets": 5
                                }
                            ],
                            "drawCallback": function (settings) {
                                var json = settings.json;
                                //console.log(json);

                                empEducationList.PageCount = json.pageCount;
                                empEducationList.TotalRows = json.recordsTotal;

                                var pageLRSize = 3;
                                var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                                var previousDot = '';
                                var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                                var nextDot = '';
                                var elements = '';

                                if (empEducationList.PageIndex - pageLRSize > 1) {
                                    previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                                }

                                if (empEducationList.PageIndex + pageLRSize < json.pageCount - 1) {
                                    nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                                }

                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    if (pi == 0) {
                                        elements += '<li class="paginate_button page-item ' + (empEducationList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                        elements += previousDot;
                                    }
                                    else if (pi == json.pageCount - 1) {
                                        elements += nextDot;
                                        elements += '<li class="paginate_button page-item ' + (empEducationList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                    else if (empEducationList.PageIndex - pageLRSize <= pi && empEducationList.PageIndex + pageLRSize >= pi) {
                                        elements += '<li class="paginate_button page-item ' + (empEducationList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                }

                                $('.empEducationList #datatables_wrapper .pagination').html(previous + elements + next);

                                $('.empEducationList #datatables_wrapper .dataTables_info').html('Showing ' + ((empEducationList.PageIndex * empEducationList.PageSize) + 1) + ' to ' + ((empEducationList.PageIndex * empEducationList.PageSize) + empEducationList.PageSize) + ' of ' + empEducationList.TotalRows + ' rows');
                            }
                        });
                        // order.dt search.dt
                        empEducationList.dt.on('draw.dt', function () {
                            empEducationList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (empEducationList.PageIndex * empEducationList.PageSize) + (i + 1) + '.';
                            });
                            empEducationList.dt.column(5, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = empEducationList.dt.cells({ row: i, column: 6 }).data()[0];
                                $(cell).find(".fa-edit").parent().attr("pid", id);
                                $(cell).find(".fa-remove").parent().attr("pid", id);
                            });
                        });
                    },
                    PageIndex2: 0,
                    PageSize2: 20,
                    PageCount2: 0,
                    TotalRows2: 0,
                    dt2: null,
                    LoadListData2: function (eid) {
                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102065") %>
                        empEducationList.dt2 = $(".empEducationList #tableData2").DataTable({
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
                                "url": "Ashx/LoadEmpFameList.ashx",
                                "type": "POST",
                                "contentType": "application/json; charset=utf-8",
                                "data": function (d) {
                                    d.eid = eid;
                                    d.search = '';
                                    d.page = empEducationList.PageIndex2;
                                    d.length = empEducationList.PageSize2;

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
                                { "data": "Department", "orderable": true },
                                { "data": "Year", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[5, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 3, 4] },
                                { "targets": [5], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="EmpFame.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102069") %>" style="padding-right: 5px; font-size: 22px;"><i class="fa fa-edit"></i></a>' +
                                            '<a href="#" class="remove-row" data-title="Confirm Dialog" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101240") %>"><i class="fa fa-remove text-danger" style="font-size: 22px;"></i></a>';
                                    },
                                    "targets": 4
                                }
                            ],
                            "drawCallback": function (settings) {
                                var json = settings.json;
                                //console.log(json);

                                empEducationList.PageCount2 = json.pageCount;
                                empEducationList.TotalRows2 = json.recordsTotal;

                                var pageLRSize = 3;
                                var previous = '<li class="paginate_button page-item previous" id="datatables_previous2"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                                var previousDot = '';
                                var next = '<li class="paginate_button page-item next" id="datatables_next2"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                                var nextDot = '';
                                var elements = '';

                                if (empEducationList.PageIndex2 - pageLRSize > 1) {
                                    previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis2"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                                }

                                if (empEducationList.PageIndex2 + pageLRSize < json.pageCount - 1) {
                                    nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis2"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                                }

                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    if (pi == 0) {
                                        elements += '<li class="paginate_button page-item ' + (empEducationList.PageIndex2 == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                        elements += previousDot;
                                    }
                                    else if (pi == json.pageCount - 1) {
                                        elements += nextDot;
                                        elements += '<li class="paginate_button page-item ' + (empEducationList.PageIndex2 == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                    else if (empEducationList.PageIndex2 - pageLRSize <= pi && empEducationList.PageIndex2 + pageLRSize >= pi) {
                                        elements += '<li class="paginate_button page-item ' + (empEducationList.PageIndex2 == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                }

                                $('.empEducationList #datatables_wrapper2 .pagination').html(previous + elements + next);

                                $('.empEducationList #datatables_wrapper2 .dataTables_info').html('Showing ' + ((empEducationList.PageIndex2 * empEducationList.PageSize2) + 1) + ' to ' + ((empEducationList.PageIndex2 * empEducationList.PageSize2) + empEducationList.PageSize2) + ' of ' + empEducationList.TotalRows2 + ' rows');
                            }
                        });
                        // order.dt search.dt
                        empEducationList.dt2.on('draw.dt', function () {
                            empEducationList.dt2.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (empEducationList.PageIndex2 * empEducationList.PageSize2) + (i + 1) + '.';
                            });
                            empEducationList.dt2.column(4, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = empEducationList.dt2.cells({ row: i, column: 5 }).data()[0];
                                $(cell).find(".fa-edit").parent().attr("pid", id);
                                $(cell).find(".fa-remove").parent().attr("pid", id);
                            });
                        });
                    },
                    RemoveItem: function (eid, pid) {
                        $.ajax({
                            type: "POST",
                            url: "EmpEducation.aspx/RemoveItem",
                            data: '{eid: ' + eid + ', id: ' + pid + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empEducationList.OnSuccessRemove,
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
                    RemoveItem2: function (eid, pid) {
                        $.ajax({
                            type: "POST",
                            url: "EmpEducation.aspx/RemoveItem2",
                            data: '{eid: ' + eid + ', id: ' + pid + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empEducationList.OnSuccessRemove2,
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

                        switch (response.d) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101245") %>';

                                empEducationList.ReloadListData();
                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121006") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121007") %>';

                                break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    OnSuccessRemove2: function (response) {
                        var title = "";
                        var body = "";

                        switch (response.d) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101245") %>';

                                empEducationList.ReloadListData2();
                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121006") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121007") %>';

                                break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ReloadListData: function () {
                        empEducationList.dt.draw();
                    },
                    ReloadListData2: function () {
                        empEducationList.dt2.draw();
                    }
                }

                $(document).ready(function () {

                    // Searching, Pagination event 
                    // 1
                    $('.empEducationList #datatables_length').change(function () {

                        empEducationList.PageSize = parseInt($(".empEducationList #datatables_length").children("option:selected").val());
                        empEducationList.PageIndex = 0;

                        empEducationList.ReloadListData();

                        return false;
                    });

                    $('.empEducationList #datatables_wrapper ul.pagination').on('click', 'li.paginate_button a', function () {

                        var pi = parseInt($(this).attr("data-dt-idx"));

                        if (pi == 100) {
                            if (empEducationList.PageIndex > 0) {
                                empEducationList.PageIndex--;
                                empEducationList.ReloadListData();

                                $('.empEducationList #datatables_wrapper .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empEducationList #datatables_wrapper .pagination .paginate_button.page-item a[data-dt-idx=' + empEducationList.PageIndex + ']').addClass('active');
                            }
                        }
                        else if (pi == 101) {
                            if (empEducationList.PageIndex < (empEducationList.PageCount - 1)) {
                                empEducationList.PageIndex++;
                                empEducationList.ReloadListData();

                                $('.empEducationList #datatables_wrapper .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empEducationList #datatables_wrapper .pagination .paginate_button.page-item a[data-dt-idx=' + empEducationList.PageIndex + ']').addClass('active');
                            }
                        }
                        else {
                            empEducationList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                            empEducationList.ReloadListData();

                            $('.empEducationList #datatables_wrapper .pagination .paginate_button.page-item.active').removeClass('active');
                            $(this).addClass('active');
                        }

                        return false;
                    });

                    $('.empEducationList #tableData #datatables_previous').click(function () {

                        if (empEducationList.PageIndex > 0) {
                            empEducationList.PageIndex--;
                            empEducationList.ReloadListData();
                        }

                        return false;
                    });

                    $('.empEducationList #tableData #datatables_next').click(function () {

                        if (empEducationList.PageIndex < (empEducationList.PageCount - 1)) {
                            empEducationList.PageIndex++;
                            empEducationList.ReloadListData();
                        }

                        return false;
                    });

                    // 2
                    $('.empEducationList #datatables_length2').change(function () {

                        empEducationList.PageSize2 = parseInt($(".empEducationList #datatables_length2").children("option:selected").val());
                        empEducationList.PageIndex2 = 0;

                        empEducationList.ReloadListData2();

                        return false;
                    });

                    $('.empEducationList #datatables_wrapper2 ul.pagination').on('click', 'li.paginate_button a', function () {

                        var pi = parseInt($(this).attr("data-dt-idx"));

                        if (pi == 100) {
                            if (empEducationList.PageIndex2 > 0) {
                                empEducationList.PageIndex2--;
                                empEducationList.ReloadListData2();

                                $('.empEducationList #datatables_wrapper2 .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empEducationList #datatables_wrapper2 .pagination .paginate_button.page-item a[data-dt-idx=' + empEducationList.PageIndex2 + ']').addClass('active');
                            }
                        }
                        else if (pi == 101) {
                            if (empEducationList.PageIndex2 < (empEducationList.PageCount2 - 1)) {
                                empEducationList.PageIndex2++;
                                empEducationList.ReloadListData2();

                                $('.empEducationList #datatables_wrapper2 .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empEducationList #datatables_wrapper2 .pagination .paginate_button.page-item a[data-dt-idx=' + empEducationList.PageIndex2 + ']').addClass('active');
                            }
                        }
                        else {
                            empEducationList.PageIndex2 = parseInt($(this).attr("data-dt-idx"));
                            empEducationList.ReloadListData2();

                            $('.empEducationList #datatables_wrapper2 .pagination .paginate_button.page-item.active').removeClass('active');
                            $(this).addClass('active');
                        }

                        return false;
                    });

                    $('.empEducationList #tableData2 #datatables_previous').click(function () {

                        if (empEducationList.PageIndex2 > 0) {
                            empEducationList.PageIndex2--;
                            empEducationList.ReloadListData2();
                        }

                        return false;
                    });

                    $('.empEducationList #tableData2 #datatables_next').click(function () {

                        if (empEducationList.PageIndex2 < (empEducationList.PageCount2 - 1)) {
                            empEducationList.PageIndex2++;
                            empEducationList.ReloadListData2();
                        }

                        return false;
                    });


                    $('.empEducationList #tableData').on('click', '.remove-row', function () {

                        var $title = $(this).attr('data-title');
                        var $message = $(this).attr('data-message');
                        var $pid = $(this).attr('pid');

                        // Modal Section
                        $('#modalNotifyConfirmRemove').off().on('show.bs.modal', function (e) {
                            $(this).find('.modal-title').text($title);
                            $(this).find('.modal-body p').text($message);
                            $(this).find('.modal-footer #modalConfirmRemove').attr('pid', $pid);
                        });
                        $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').off().on('click', function () {

                            $('#modalNotifyConfirmRemove').modal('hide');

                            $("#modalWaitDialog").modal('show');

                            // Remove command
                            empEducationList.RemoveItem(<%=Request.QueryString["eid"]%>, $(this).attr('pid'));

                        });

                        $("#modalNotifyConfirmRemove").modal('show');

                    });

                    $('.empEducationList #tableData2').on('click', '.remove-row', function () {

                        var $title = $(this).attr('data-title');
                        var $message = $(this).attr('data-message');
                        var $pid = $(this).attr('pid');

                        // Modal Section
                        $('#modalNotifyConfirmRemove').off().on('show.bs.modal', function (e) {
                            $(this).find('.modal-title').text($title);
                            $(this).find('.modal-body p').text($message);
                            $(this).find('.modal-footer #modalConfirmRemove').attr('pid', $pid);
                        });
                        $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').off().on('click', function () {

                            $('#modalNotifyConfirmRemove').modal('hide');

                            $("#modalWaitDialog").modal('show');

                            // Remove command
                            empEducationList.RemoveItem2(<%=Request.QueryString["eid"]%>, $(this).attr('pid'));

                        });

                        $("#modalNotifyConfirmRemove").modal('show');

                    });

                    // Datatable Section
                    empEducationList.LoadListData(<%=Request.QueryString["eid"]%>);
                    empEducationList.LoadListData2(<%=Request.QueryString["eid"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="empEducationForm popup-form" style="padding: 15px;">
                <form id="empEducationForm" class="form-padding">
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptInstitution"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102050") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptInstitution" name="iptInstitution"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102050") %>" maxlength="100" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptStudyYear"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102052") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptStudyYear" name="iptStudyYear"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102052") %>" maxlength="4" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptGraduationYear"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102053") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptGraduationYear" name="iptGraduationYear"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102053") %>" maxlength="4" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="sltLevel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102045") %> :</label></div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltLevel" name="sltLevel" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102046") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102046") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102054") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %></option>
                                <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102057") %></option>
                                <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102058") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102059") %></option>
                                <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %></option>
                                <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102061") %></option>
                                <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102062") %></option>
                                <asp:Literal ID="ltrLevel" runat="server"></asp:Literal>
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptMajor"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102063") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptMajor" name="iptMajor"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102063") %>" maxlength="100" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptMinorSubject"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102064") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptMinorSubject" name="iptMinorSubject"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102064") %>" maxlength="100" />
                        </div>
                    </div>

                    <div class="row text-center" style="display: block; padding-top: 15px;">
                        <button id="save" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="cancel" type="button"
                            class="btn btn-danger" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var empEducationForm = {
                    GetItem: function (empID, id) {
                        $.ajax({
                            type: "POST",
                            url: "EmpEducation.aspx/GetItem",
                            data: '{empID: ' + empID + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empEducationForm.OnSuccessGet,
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
                    OnSuccessGet: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {

                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                $(".empEducationForm #iptInstitution").val($(this).find("F1").text());
                                $(".empEducationForm #iptStudyYear").val($(this).find("F2").text());
                                $(".empEducationForm #iptGraduationYear").val($(this).find("F3").text());
                                $(".empEducationForm #sltLevel").selectpicker('val', $(this).find("F4").text());
                                $(".empEducationForm #iptMajor").val($(this).find("F5").text());
                                $(".empEducationForm #iptMinorSubject").val($(this).find("F6").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "EmpEducation.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empEducationForm.OnSuccessSave,
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

                        switch (response.d) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

                                    // Close modal
                                    empEducationForm.ClearSession(function () {
                                        modalForm.hideForm();

                                        empEducationList.ReloadListData();
                                    });

                                });

                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111026") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %>';

                                break;
                            default: break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ClearSession: function (callbackRedirect) {
                        $.ajax({
                            type: "POST",
                            url: "EmpEducation.aspx/ClearSessionID",
                            data: '{}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                callbackRedirect();
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
                }

                $(document).ready(function () {

                    $("#empEducationForm").validate({
                        rules: {
                            iptInstitution: "required",
                            iptStudyYear: {
                                required: true,
                                number: true,
                                digits: true
                            },
                            iptGraduationYear: {
                                required: true,
                                number: true,
                                digits: true
                            },
                            sltLevel: "required"
                        },
                        messages: {
                            iptInstitution: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptStudyYear: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>",
                                digits: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121005") %>"
                            },
                            iptGraduationYear: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>",
                                digits: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121005") %>"
                            },
                            sltLevel: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptInstitution":
                                case "iptStudyYear":
                                case "iptGraduationYear": error.insertAfter(element); break;
                                case "sltLevel": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    $(".empEducationForm #save").bind({
                        click: function () {

                            if ($('#empEducationForm').valid()) {

                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                //$('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off('click');
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = new Array();
                                    data[0] = xEmpKey.eid + "-" + xEmpKey.pid;
                                    data[1] = $(".empEducationForm #iptInstitution").val();
                                    data[2] = $(".empEducationForm #iptStudyYear").val();
                                    data[3] = $(".empEducationForm #iptGraduationYear").val();
                                    data[4] = $(".empEducationForm #sltLevel").val();
                                    data[5] = $(".empEducationForm #iptMajor").val();
                                    data[6] = $(".empEducationForm #iptMinorSubject").val();

                                    empEducationForm.SaveItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".empEducationForm #cancel").bind({
                        click: function () {

                            // Close modal
                            empEducationForm.ClearSession(function () {
                                modalForm.hideForm();
                            });

                            return false;
                        }
                    });

                    // Modal Section
                    $('#modalNotifyConfirmSave').off().on('show.bs.modal', function (e) {
                        $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                        $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
                    });

                    // Initial data

                    activateBootstrapSelect('.empEducationForm .selectpicker');

                    // Load info command
                    xEmpKey.pid = "<%=Request.QueryString["id"]%>";
                    empEducationForm.GetItem(<%=Request.QueryString["eid"]%>, <%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            View Content
        </asp:View>
    </asp:MultiView>
</body>
</html>
