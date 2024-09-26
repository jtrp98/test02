<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="NewsReportList.aspx.cs" Inherits="FingerprintPayment.Message.NewsReportList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
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

        .material-list .dropdown-toggle .filter-option-inner-inner {
            font-size: 14px;
        }

        label.col-form-label {
            color: #AAAAAA;
        }

        table.dataTable thead .sorting:before, table.dataTable thead .sorting_asc:before, table.dataTable thead .sorting_desc:before, table.dataTable thead .sorting_asc_disabled:before, table.dataTable thead .sorting_desc_disabled:before, table.dataTable thead .sorting:after, table.dataTable thead .sorting_asc:after, table.dataTable thead .sorting_desc:after, table.dataTable thead .sorting_asc_disabled:after, table.dataTable thead .sorting_desc_disabled:after {
            top: 7%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01561") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401005") %>
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
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                        <div class="col-sm-3">
                            <select id="sltYear" class="selectpicker col-sm-12" data-size="5" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %>">
                                <asp:Literal ID="ltrYear" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                        <div class="col-sm-3">
                            <select id="sltTerm" class="selectpicker col-sm-12" data-size="5" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %>">
                                <asp:Literal ID="ltrTerm" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106101") %></label>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <input id="iptStartDate" type="text" class="form-control datepicker" value="<%--<%=Convert.ToDateTime(DateTime.Now.Month + "/1/" + DateTime.Now.Year).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %>--%>">
                            </div>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106102") %></label>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <input id="iptEndDate" type="text" class="form-control datepicker" value="<%--<%=DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %>--%>">
                            </div>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401007") %></label>
                        <div class="col-sm-3">
                            <div class="form-group bmd-form-group">
                                <input id="iptSender" type="text" class="form-control" placeholder="">
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
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">newspaper</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401005") %></h4>
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
                                                <th style="width: 11%" class="text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401006") %></th>
                                                <th style="width: 11%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401007") %></th>
                                                <th style="width: 11%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401047") %></th>
                                                <th style="width: 11%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401045") %></th>
                                                <th style="width: 11%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106143") %></th>
                                                <th style="width: 11%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401010") %></th>
                                                <th style="width: 14%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></th>
                                                <th style="width: 11%" class="disabled-sorting text-center"></th>
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
        var newsList = {
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            TotalRows: 0,
            dt: null,
            LoadListData: function () {
                newsList.dt = $('#tableData').DataTable({
                    "processing": true,
                    "serverSide": true,
                    "info": false,
                    "searching": false,
                    "paging": false,
                    "stateSave": false,
                    "ajax": {
                        "url": "NewsList.aspx/LoadNews",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.startDate = $("#iptStartDate").val();
                            d.endDate = $("#iptEndDate").val();
                            d.sender = $("#iptSender").val();
                            d.page = newsList.PageIndex;
                            d.length = newsList.PageSize;

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
                        { "data": "SendDate", "orderable": true },
                        { "data": "Recorder", "orderable": true },
                        { "data": "Type", "orderable": true },
                        { "data": "Title", "orderable": true },
                        { "data": "Receiver", "orderable": true },
                        { "data": "Duration", "orderable": true },
                        { "data": "News", "orderable": true },
                        { "data": "action", "orderable": false },
                        { "data": "nid", "orderable": false }
                    ],
                    "order": [[1, "desc"]],
                    "columnDefs": [
                        { className: "text-center", "targets": [0, 1, 2, 3, 5, 6, 8] },
                        { "targets": [9], "visible": false },
                        {
                            "render": function (data, type, row) {
                                return `<div class="btn-group">
                                            <button class="btn btn-info btn-sm view-data" data-nid="` + row.nid + `" style="width: 111px;">
                                                <span class="btn-label">
                                                    <i class="material-icons">search</i>
                                                </span>
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302012") %>
                                            </button>
                                        </div>`;
                            },
                            "targets": 8
                        }
                    ],
                    "drawCallback": function (settings) {
                        //var json = settings.json;
                        var json = $.parseJSON(settings.json.d);
                        //console.log(json);

                        newsList.PageCount = json.pageCount;
                        newsList.TotalRows = json.recordsTotal;

                        var pageLRSize = 3;
                        var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                        var previousDot = '';
                        var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                        var nextDot = '';
                        var elements = '';

                        if (newsList.PageIndex - pageLRSize > 1) {
                            previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                        }

                        if (newsList.PageIndex + pageLRSize < json.pageCount - 1) {
                            nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                        }

                        for (var pi = 0; pi < json.pageCount; pi++) {
                            if (pi == 0) {
                                elements += '<li class="paginate_button page-item ' + (newsList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                elements += previousDot;
                            }
                            else if (pi == json.pageCount - 1) {
                                elements += nextDot;
                                elements += '<li class="paginate_button page-item ' + (newsList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                            else if (newsList.PageIndex - pageLRSize <= pi && newsList.PageIndex + pageLRSize >= pi) {
                                elements += '<li class="paginate_button page-item ' + (newsList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                        }

                        $('.pagination').html(previous + elements + next);

                        $('.dataTables_info').html('Showing ' + ((newsList.PageIndex * newsList.PageSize) + 1) + ' to ' + ((newsList.PageIndex * newsList.PageSize) + newsList.PageSize) + ' of ' + newsList.TotalRows + ' rows');
                    }
                });

                // order.dt search.dt
                newsList.dt.on('draw.dt', function () {
                    newsList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (newsList.PageIndex * newsList.PageSize) + (i + 1) + '.';
                    });
                });
            },
            RemoveItem: function (sid) {

            },
            OnSuccessRemove: function (response) {

            },
            ReloadListData: function () {
                newsList.dt.draw();
            }
        }

        function LoadTerm(yearID, objResult) {
            if (yearID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "NewsList.aspx/LoadTerm",
                    data: '{yearID: ' + yearID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var yearData = JSON.parse(response.d);
                        console.log(yearData);

                        $(objResult).empty();

                        if (yearData.length > 0) {

                            var options = '<option value="" data-start="" data-end="" selected="selected"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>';
                            $(yearData).each(function () {

                                options += '<option value="' + this.id + '" data-start="' + this.start + '" data-end="' + this.end + '">' + this.term + '</option>';

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

            // Init
            $('.datepicker').datetimepicker({
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
            });

            // Search
            $("#sltYear").change(function () {

                LoadTerm($(this).val(), '#sltTerm');

                $('#iptStartDate').val($(this).children("option:selected").data('start'));
                $('#iptEndDate').val($(this).children("option:selected").data('end'));

            });

            $("#sltTerm").change(function () {
                if ($('#sltTerm').val() == '') {
                    $('#iptStartDate').val($('#sltYear').children("option:selected").data('start'));
                    $('#iptEndDate').val($('#sltYear').children("option:selected").data('end'));
                }
                else {
                    $('#iptStartDate').val($(this).children("option:selected").data('start'));
                    $('#iptEndDate').val($(this).children("option:selected").data('end'));
                }
            });

            // Searching, Pagination event 
            $('#btnSearch').click(function () {

                newsList.PageIndex = 0;

                newsList.ReloadListData();

                return false;
            });

            $('#datatables_length').change(function () {

                newsList.PageSize = parseInt($("#datatables_length").children("option:selected").val());
                newsList.PageIndex = 0;

                newsList.ReloadListData();

                return false;
            });

            $('ul.pagination').on('click', 'li.paginate_button a', function () {

                var pi = parseInt($(this).attr("data-dt-idx"));

                if (pi == 100) {
                    if (newsList.PageIndex > 0) {
                        newsList.PageIndex--;
                        newsList.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + newsList.PageIndex + ']').addClass('active');
                    }
                }
                else if (pi == 101) {
                    if (newsList.PageIndex < (newsList.PageCount - 1)) {
                        newsList.PageIndex++;
                        newsList.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + newsList.PageIndex + ']').addClass('active');
                    }
                }
                else {
                    newsList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                    newsList.ReloadListData();

                    $('.pagination .paginate_button.page-item.active').removeClass('active');
                    $(this).addClass('active');
                }

                return false;
            });

            $('#tableData #datatables_previous').click(function () {

                if (newsList.PageIndex > 0) {
                    newsList.PageIndex--;
                    newsList.ReloadListData();
                }

                return false;
            });

            $('#tableData #datatables_next').click(function () {

                if (newsList.PageIndex < (newsList.PageCount - 1)) {
                    newsList.PageIndex++;
                    newsList.ReloadListData();
                }

                return false;
            });

            $('#tableData').on('click', '.view-data', function () {

                window.location.href = 'NewsReportView.aspx?nid=' + $(this).data('nid');

                return false;
            });

            // Datatable Section
            newsList.LoadListData();

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
