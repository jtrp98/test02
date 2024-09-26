<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="GroupList.aspx.cs" Inherits="FingerprintPayment.Message.GroupList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link href="/Permission/asset/toggle.css" rel="stylesheet">
    <style>
        .el-switch .el-switch-style {
            height: 1.6em;
            left: 0;
            background: #ff514b;
            -webkit-border-radius: 0.8em;
            border-radius: 0.8em;
            display: inline-block;
            position: relative;
            top: 0;
            -webkit-transition: all 0.3s ease-in-out;
            transition: all 0.3s ease-in-out;
            width: 3em;
            cursor: pointer;
        }

        #tableData tr td ul.dropdown-menu li {
            white-space: nowrap;
        }

        #tableData tr td .btn-group, #tableData tr td .btn-group-vertical {
            position: relative;
            margin: 3px 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01561") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401066") %>
            </p>
        </div>
    </div>
    <div class="row">
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
                        <label class="col-sm-2 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401068") %></label>
                        <div class="col-sm-10">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group bmd-form-group">
                                        <input id="iptGroupName" type="text" class="form-control" placeholder="">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
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
                        <i class="material-icons">account_circle</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401069") %></h4>
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
                                                <th style="width: 16%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401070") %></th>
                                                <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401071") %></th>
                                                <th style="width: 30%" class="disabled-sorting text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102148") %></th>
                                                <th style="width: 15%" class="disabled-sorting text-center"></th>
                                                <th style="width: 15%" class="disabled-sorting text-center">
                                                    <button id="btnGroupNew" class="btn btn-success">
                                                        <span class="btn-label">
                                                            <i class="material-icons">add</i>
                                                        </span>
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401074") %>
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

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script>
        var groupList = {
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            TotalRows: 0,
            dt: null,
            LoadListData: function () {
                groupList.dt = $('#tableData').DataTable({
                    "processing": true,
                    "serverSide": true,
                    "info": false,
                    "searching": false,
                    "paging": false,
                    "stateSave": true,
                    "ajax": {
                        "url": "GroupList.aspx/LoadGroup",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.groupName = $("#iptGroupName").val();
                            d.page = groupList.PageIndex;
                            d.length = groupList.PageSize;

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
                        { "data": "GroupName", "orderable": true },
                        { "data": "GroupNameEn", "orderable": true },
                        { "data": "status", "orderable": true },
                        { "data": "@switch", "orderable": false },
                        { "data": "action", "orderable": false },
                        { "data": "gid", "orderable": false },
                        { "data": "istatus", "orderable": false }
                    ],
                    "order": [[1, "asc"]],
                    "columnDefs": [
                        { className: "text-center", "targets": [0, 3, 4, 5] },
                        { "targets": [6, 7], "visible": false },
                        {
                            "render": function (data, type, row) {
                                return `<div data-id='` + row.gid + `' style="margin-left: auto;margin-right: auto;width: 120px;">
                                            ` + (row.istatus == '1' ?
                                        `<p style='background-color: #4caf50; width: 120px; height: 41px; color: white; margin: -9px 0px -9px 0px; padding-top: 9px;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206116") %></p>` :
                                        `<p style='background-color: #ff514b; width: 120px; height: 41px; color: white; margin: -9px 0px -9px 0px; padding-top: 9px;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206161") %></p>`) + `
                                        </div>`;
                            },
                            "targets": 3
                        },
                        {
                            "render": function (data, type, row) {
                                return `<label class="el-switch el-switch">
                                            <input type="checkbox" class="switch-button" data-id="` + row.gid + `" ` + (row.istatus == '1' ? 'checked' : '') + `>
                                            <span class="el-switch-style"></span>
                                        </label>`;
                            },
                            "targets": 4
                        },
                        {
                            "render": function (data, type, row) {
                                return `<div class="btn-group">
                                            <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103029") %><span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu pull-right">
                                                <li><a href="GroupForm.aspx?gid=`+ row.gid + `"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132451") %></a></li>
                                                <li><a href="#" class='group-remove' data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101240") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132452") %></a></li>
                                                <li><a href="#"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132453") %></a></li>
                                            </ul>
                                        </div>`;
                            },
                            "targets": 5
                        }
                    ],
                    "drawCallback": function (settings) {
                        //var json = settings.json;
                        var json = $.parseJSON(settings.json.d);
                        console.log(json);

                        groupList.PageCount = json.pageCount;
                        groupList.TotalRows = json.recordsTotal;

                        var pageLRSize = 3;
                        var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                        var previousDot = '';
                        var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                        var nextDot = '';
                        var elements = '';

                        if (groupList.PageIndex - pageLRSize > 1) {
                            previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                        }

                        if (groupList.PageIndex + pageLRSize < json.pageCount - 1) {
                            nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                        }

                        for (var pi = 0; pi < json.pageCount; pi++) {
                            if (pi == 0) {
                                elements += '<li class="paginate_button page-item ' + (groupList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                elements += previousDot;
                            }
                            else if (pi == json.pageCount - 1) {
                                elements += nextDot;
                                elements += '<li class="paginate_button page-item ' + (groupList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                            else if (groupList.PageIndex - pageLRSize <= pi && groupList.PageIndex + pageLRSize >= pi) {
                                elements += '<li class="paginate_button page-item ' + (groupList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                        }

                        $('.pagination').html(previous + elements + next);

                        $('.dataTables_info').html('Showing ' + ((groupList.PageIndex * groupList.PageSize) + 1) + ' to ' + ((groupList.PageIndex * groupList.PageSize) + groupList.PageSize) + ' of ' + groupList.TotalRows + ' rows');
                    }
                });

                // order.dt search.dt
                groupList.dt.on('draw.dt', function () {
                    groupList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (groupList.PageIndex * groupList.PageSize) + (i + 1) + '.';
                    });
                    groupList.dt.column(5, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        var gid = groupList.dt.cells({ row: i, column: 6 }).data()[0];
                        $(cell).find(".group-remove").attr("gid", gid);
                    });
                });
            },
            RemoveItem: function (gid) {
                $.ajax({
                    type: "POST",
                    url: "GroupList.aspx/RemoveItem",
                    data: '{gid: ' + gid + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: groupList.OnSuccessRemove,
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

                    groupList.ReloadListData();
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
            SwitchStatus: function (gid) {
                $.ajax({
                    type: "POST",
                    url: "GroupList.aspx/SwitchStatus",
                    data: '{gid: ' + gid + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var r = JSON.parse(response.d);
                        console.log('SwitchStatus: ' + r.message);
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
            },
            ReloadListData: function () {
                groupList.dt.draw();
            }
        }

        $(document).on('click', '.switch-button', function () {
            $('div[data-id=' + $(this).attr('data-id') + '] p').css('background-color', this.checked ? '#4caf50' : '#ff514b').html(this.checked ? '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206116") %>' : '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206161") %>');

            // Save status to db
            groupList.SwitchStatus($(this).attr('data-id'));
        });

        $(document).ready(function () {

            // Init

            // Searching, Pagination event 
            $('#btnSearch').click(function () {

                groupList.PageIndex = 0;

                groupList.ReloadListData();

                return false;
            });

            $('#datatables_length').change(function () {

                groupList.PageSize = parseInt($("#datatables_length").children("option:selected").val());
                groupList.PageIndex = 0;

                groupList.ReloadListData();

                return false;
            });

            $('ul.pagination').on('click', 'li.paginate_button a', function () {

                var pi = parseInt($(this).attr("data-dt-idx"));

                if (pi == 100) {
                    if (groupList.PageIndex > 0) {
                        groupList.PageIndex--;
                        groupList.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + groupList.PageIndex + ']').addClass('active');
                    }
                }
                else if (pi == 101) {
                    if (groupList.PageIndex < (groupList.PageCount - 1)) {
                        groupList.PageIndex++;
                        groupList.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + groupList.PageIndex + ']').addClass('active');
                    }
                }
                else {
                    groupList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                    groupList.ReloadListData();

                    $('.pagination .paginate_button.page-item.active').removeClass('active');
                    $(this).addClass('active');
                }

                return false;
            });

            $('#tableData #datatables_previous').click(function () {

                if (groupList.PageIndex > 0) {
                    groupList.PageIndex--;
                    groupList.ReloadListData();
                }

                return false;
            });

            $('#tableData #datatables_next').click(function () {

                if (groupList.PageIndex < (groupList.PageCount - 1)) {
                    groupList.PageIndex++;
                    groupList.ReloadListData();
                }

                return false;
            });

            $('#btnGroupNew').click(function () {

                window.location.href = 'GroupForm.aspx?gid=0';

                return false;
            });

            // Modal Section
            $('#modalNotifyConfirmRemove').on('show.bs.modal', function (e) {
                $title = $(e.relatedTarget).attr('data-title');
                $(this).find('.modal-title').text($title);
                $message = $(e.relatedTarget).attr('data-message');
                $(this).find('.modal-body p').text($message);
                $gid = $(e.relatedTarget).attr('gid');
                $(this).find('.modal-footer #modalConfirmRemove').attr('gid', $gid);
            });
            $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').on('click', function () {

                $('#modalNotifyConfirmRemove').modal('hide');

                $("#modalWaitDialog").modal('show');

                // Remove command
                groupList.RemoveItem($(this).attr('gid'));
            });

            $('#modalNotifyOnlyClose').on('hidden.bs.modal', function (e) {
                if ($("#modalWaitDialog").data('bs.modal')?._isShown) {
                    $("#modalWaitDialog").modal('hide');
                }
            });

            // Datatable Section
            groupList.LoadListData();

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
