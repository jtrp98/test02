<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="StudentGraduateList.aspx.cs" Inherits="FingerprintPayment.StudentInfo.StudentGraduateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101338") %>
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
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                        <div class="col-sm-3">
                            <select id="sltYear" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %>">
                                <asp:Literal ID="ltrYear" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                        <div class="col-sm-3">
                            <select id="sltTerm" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %>">
                                <asp:Literal ID="ltrTerm" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
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
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101338") %></h4>
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
                                                <th style="width: 6%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>
                                                <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                                <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></th>
                                                <th style="width: 14%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                                                <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                                <th style="width: 12%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></th>
                                                <th style="width: 18%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                                <th style="width: 15%" class="text-center"></th>
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
                        "url": "StudentGraduateList.aspx/LoadStudent",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.year = $(".studentList #sltYear").children("option:selected").val();
                            d.term = $(".studentList #sltTerm").children("option:selected").val();
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
                        { "data": "no", "orderable": false },
                        { "data": "No", "orderable": true },
                        { "data": "Code", "orderable": true },
                        { "data": "Title", "orderable": true },
                        { "data": "Name", "orderable": true },
                        { "data": "Lastname", "orderable": true },
                        { "data": "ClassName", "orderable": true },
                        { "data": "Status", "orderable": true },
                        { "data": "action", "orderable": false },
                        { "data": "sid", "orderable": false },
                        { "data": "fingerStatus", "orderable": false },
                        { "data": "tid", "orderable": false }
                    ],
                    "order": [[1, "asc"]],
                    "columnDefs": [
                        { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8] },
                        { "targets": [9, 10, 11], "visible": false },
                        {
                            "render": function (data, type, row) {
                                return '<a href="#"><i class="fa fa-vcard-o" style="padding-right: 5px; font-size: 22px;"></i></a>';
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
                        var tid = studentList.dt.cells({ row: i, column: 11 }).data()[0];
                        $(cell).find(".fa-vcard-o").parent().attr("href", "StudentGraduateDetail.aspx?v=view&sid=" + sid + "&tid=" + tid).attr("target", "_blank");
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
                    url: "StudentGraduateList.aspx/LoadTerm",
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
                    url: "StudentGraduateList.aspx/LoadTermSubLevel2",
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


            // Search
            $("#sltYear").change(function () {

                LoadTerm($(this).val(), '#sltTerm');

            });

            $("#sltLevel").change(function () {

                LoadTermSubLevel2($(this).val(), '#sltClass');

            });

            // Modal Section
            //$('#modalNotifyOnlyClose').on('hidden.bs.modal', function () {
            //    if (($("#modalWaitDialog").data('bs.modal') || {})._isShown) {
            //        setTimeout(function () {
            //            $("#modalWaitDialog").modal('hide');
            //        }, 1000);
            //    }
            //});
            //$('#modalLogin').on('show.bs.modal', function () {
            //    if (($("#modalWaitDialog").data('bs.modal') || {})._isShown) {
            //        setTimeout(function () {
            //            $("#modalWaitDialog").modal('hide');
            //        }, 1000);
            //    }
            //});


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
                            url: "StudentGraduateList.aspx/GetStudentName",
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
