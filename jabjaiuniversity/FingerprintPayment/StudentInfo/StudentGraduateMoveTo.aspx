<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="StudentGraduateMoveTo.aspx.cs" Inherits="FingerprintPayment.StudentInfo.StudentGraduateMoveTo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101342") %>
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
                        <div class="col-sm-3 div-select-input">
                            <select id="sltYear" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %>">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                <asp:Literal ID="ltrYear" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltTerm" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %>">
                                <asp:Literal ID="ltrTerm" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
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
                                <div class="col-sm-12 col-md-6 text-right">
                                    <button id="btnMoveTo" type="button" class="btn btn-success must-checked-student disabled" style="margin-right: 15px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106005") %></button>
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
                                                <th style="width: 6%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>
                                                <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></th>
                                                <th style="width: 17%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></th>
                                                <th style="width: 8%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></th>
                                                <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></th>
                                                <th style="width: 12%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                                <th style="width: 8%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %><br />
                                                    <select id="sltLevelxAll" class="selectpicker col-sm-12" data-style="select-with-transition"></select></th>
                                                <th style="width: 13%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %><br />
                                                    <select id="sltClassroomxAll" class="selectpicker col-sm-12" data-style="select-with-transition"></select></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></th>
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


    <div id="modalStudentGraduateMoveTo" class="modal fade" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 10%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="width: 400px;">

                <div class="modal-header" style="padding: 0px 15px; top: 25%;">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101345") %></h3>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row">
                            <div class="col-md-12">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101346") %>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnStudentGraduateMoveTo" class="btn btn-success global-btn">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106005") %></button>
                    <button type="button" class="btn btn-danger global-btn" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <div id="modalCustomConfig" class="modal fade" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 10%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="width: 400px;">

                <div class="modal-header" style="padding: 0px 15px; top: 25%;">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101344") %></h3>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row">
                            <label class="col-md-5 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</label>
                            <div class="col-sm-6 div-select-input">
                                <div class="form-group bmd-form-group">
                                    <select id="sltStudentGraduateMoveToYear" name="sltStudentGraduateMoveToYear[]" class="selectpicker col-sm-12" data-style="select-with-transition">
                                        <asp:Literal ID="ltrStudentGraduateMoveToYear" runat="server"></asp:Literal>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnConfigSave" class="btn btn-success global-btn">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %></button>
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

    <script type='text/javascript' src="/Scripts/init-function.js?v=<%=DateTime.Now.Ticks%>"></script>

    <script type="text/javascript">

        var studentList = {
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            TotalRows: 0,
            dt: null,
            LevelData: [<%=LevelData%>],
            ClassRoomData: [<%=ClassRoomData%>],
            GraduateMoveToData: [],
            LevelIndex: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211034") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111056") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111057") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111058") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111059") %>', 'P.1', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111060") %>', 'P.2', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111061") %>', 'P.3', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111062") %>', 'P.4', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111063") %>', 'P.5', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111064") %>', 'P.6', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111065") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111066") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111067") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111068") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111069") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111070") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>1', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>2', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>3', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>1', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>2'],
            LoadListData: function () {
                studentList.dt = $(".studentList #tableData").DataTable({
                    "processing": true,
                    "serverSide": true,
                    "info": false,
                    "searching": false,
                    "paging": false,
                    "stateSave": true,
                    "ajax": {
                        "url": "StudentGraduateMoveTo.aspx/LoadStudent",
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
                        { "data": "check", "orderable": false },
                        { "data": "no", "orderable": false },
                        { "data": "No", "orderable": true },
                        { "data": "Code", "orderable": true },
                        { "data": "Title", "orderable": false },
                        { "data": "Name", "orderable": true },
                        { "data": "Year", "orderable": false },
                        { "data": "ClassName", "orderable": true },
                        { "data": "Status", "orderable": true },
                        { "data": "level", "orderable": false },//9
                        { "data": "room", "orderable": false },//10
                        { "data": "sid", "orderable": false },
                        { "data": "fingerStatus", "orderable": false },
                        { "data": "tid", "orderable": false },
                        { "data": "Note", "orderable": false },
                        { "data": "LevelName", "orderable": false }//15
                    ],
                    "order": [[2, "asc"]],
                    "columnDefs": [
                        { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 14] },
                        { "targets": [4, 11, 12, 13, 15], "visible": false },
                        {
                            "render": function (data, type, row) {
                                // row.sid
                                return `<div class="form-check">
                                            <label class="form-check-label">
                                                <input class="form-check-input check-one" type="checkbox">
                                                <span class="form-check-sign">
                                                    <span class="check"></span>
                                                </span>
                                            </label>
                                        </div>`;
                            },
                            "targets": 0
                        },
                        {
                            "render": function (data, type, row) {
                                return '<select id="sltLevelx' + row.sid + '" name="sltLevelx' + row.sid + '[]" class="selectpicker col-sm-12" data-student-id="' + row.Code + '" data-style="select-with-transition"></select>';
                            },
                            "targets": 9
                        },
                        {
                            "render": function (data, type, row) {
                                return '<select id="sltClassroomx' + row.sid + '" name="sltClassroomx' + row.sid + '[]" class="selectpicker col-sm-12" data-student-id="' + row.Code + '" data-style="select-with-transition"></select>';
                            },
                            "targets": 10
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
                        var sid = studentList.dt.cells({ row: i, column: 11 }).data()[0];
                        var studentId = studentList.dt.cells({ row: i, column: 3 }).data()[0];
                        $(cell).find(".check-one").attr("sid", sid);
                        $(cell).find(".check-one").attr("student-id", studentId);
                    });
                    studentList.dt.column(1, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (studentList.PageIndex * studentList.PageSize) + (i + 1) + '.';
                    });
                    studentList.dt.column(9, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        var sid = studentList.dt.cells({ row: i, column: 11 }).data()[0];
                        var lid = studentList.dt.cells({ row: i, column: 15 }).data()[0];

                        var glevelIndex = studentList.LevelIndex.indexOf(lid.replace(' ', ''));

                        $('#sltLevelxAll').empty();
                        $.each(studentList.LevelData, function (i, data) {

                            $('#sltLevelx' + sid).append($('<option/>', { text: data.levelName, value: data.levelID }));

                            // All
                            $('#sltLevelxAll').append($('<option/>', { text: data.levelName, value: data.levelID }));
                        });
                        // Insert first option to select
                        $("#sltLevelxAll").prepend("<option value='' selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111020") %></option>");
                        $("#sltLevelxAll").selectpicker('refresh');

                        // Disable low index class level
                        var firstEnabledLevel = false;
                        $('#sltLevelx' + sid + ' > option').each(function () {
                            var levelIndex = studentList.LevelIndex.indexOf(this.text.replace(' ', ''));

                            $(this).attr("disabled", glevelIndex > levelIndex);

                            if (!(glevelIndex > levelIndex) && !firstEnabledLevel) {
                                $(this).prop('selected', true);
                                firstEnabledLevel = true;
                            }
                        });

                        $('#sltLevelxAll > option:gt(0)').each(function () {
                            var levelIndex = studentList.LevelIndex.indexOf(this.text.replace(' ', ''));

                            $(this).attr("disabled", glevelIndex > levelIndex);

                        });

                        // Insert first option to select
                        if ($('#sltClassroomxAll > option').length == 0) {
                            $("#sltClassroomxAll").prepend("<option value='' selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111072") %></option>");
                            $("#sltClassroomxAll").selectpicker('refresh');
                        }

                        if (!!$('#sltLevelx' + sid).val()) {
                            var classRooms = $(studentList.ClassRoomData).filter(function (i, n) { return n.lid == $('#sltLevelx' + sid).val(); });
                            if (classRooms && classRooms.length > 0) {
                                $.each(classRooms, function (i, data) {
                                    $('#sltClassroomx' + sid).append($('<option/>', { text: data.name, value: data.id }));
                                });
                            }
                        }

                        // Enable/Disable checkbox when classroom is empty
                        $('.check-one[sid=' + sid + ']').attr("disabled", !$('#sltLevelx' + sid).val() || !(classRooms && classRooms.length > 0));

                        // Set last select
                        var item = studentList.GraduateMoveToData.find(function (e) {
                            if (e.sid == sid) {
                                return e;
                            }
                        });
                        var index = studentList.GraduateMoveToData.indexOf(item);
                        if (index != -1) {
                            $('#sltLevelx' + sid).val(studentList.GraduateMoveToData[index].lid);
                            var classRooms = $(studentList.ClassRoomData).filter(function (i, n) { return n.lid == $('#sltLevelx' + sid).val(); });
                            if (classRooms && classRooms.length > 0) {
                                $.each(classRooms, function (i, data) {
                                    $('#sltClassroomx' + sid).append($('<option/>', { text: data.name, value: data.id }));
                                });
                            }

                            $('#sltClassroomx' + sid).val(studentList.GraduateMoveToData[index].crid);
                        }

                        activateBootstrapSelect('#sltLevelx' + sid);
                        $('#sltLevelx' + sid).selectpicker('refresh');

                        activateBootstrapSelect('#sltClassroomx' + sid);
                        $('#sltClassroomx' + sid).selectpicker('refresh');
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
                    url: "StudentGraduateMoveTo.aspx/LoadTerm",
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
                    url: "StudentGraduateMoveTo.aspx/LoadTermSubLevel2",
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

        function CustomConfig() {

            $("#modalCustomConfig").modal('show');

            return false;
        }

        function StudentGraduateMoveTo() {

            if (studentList.GraduateMoveToData.length > 0) {

                $("#modalCustomConfig").modal('hide');

                $("#modalStudentGraduateMoveTo").modal('show');

            } else {
                bootbox.alert({
                    message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111071") %></h3>',
                    backdrop: true
                });
            }

            return false;
        }

        function activateBootstrapSelect(selector) {
            if ($(selector).length != 0) {
                $(selector).selectpicker();
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

                // Clear choose data
                studentList.GraduateMoveToData = [];
                $('.must-checked-student').addClass("disabled");

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
                $(".check-one:not(:disabled)").prop('checked', $(this).prop('checked'));

                // Enable or Disable when checked student
                if ($(".check-one:checked").length) $('.must-checked-student').removeClass("disabled"); else $('.must-checked-student').addClass("disabled");

                // Update GraduateMoveToData
                $(".check-one").each(function (i) {
                    var sid = $(this).attr('sid');
                    var lid = 0;
                    var crid = 0;
                    var studentId = $(this).attr('student-id');

                    if (!!$('#sltLevelx' + sid).val()) {
                        lid = $('#sltLevelx' + sid).val();
                    }
                    if (!!$('#sltClassroomx' + sid).val()) {
                        crid = $('#sltClassroomx' + sid).val();
                    }

                    var item = studentList.GraduateMoveToData.find(function (e) {
                        if (e.sid == sid) {
                            return e;
                        }
                    });
                    var index = studentList.GraduateMoveToData.indexOf(item);
                    if (index == -1) {
                        studentList.GraduateMoveToData.push(
                            { sid: sid, lid: lid, crid: crid, chk: $(this).prop('checked'), studentId: studentId }
                        );
                    }
                    else {
                        studentList.GraduateMoveToData[index].sid = sid;
                        studentList.GraduateMoveToData[index].lid = lid;
                        studentList.GraduateMoveToData[index].crid = crid;
                        studentList.GraduateMoveToData[index].chk = $(this).prop('checked');
                        studentList.GraduateMoveToData[index].studentId = studentId;
                    }
                });
            });

            $('#tableData').on('change', 'tbody input.check-one', function () {
                $(".check-all").prop('checked', ($('.check-one:not(:disabled)').length == $('.check-one:not(:disabled)').filter(":checked").length));

                // Enable or Disable when checked student
                if ($(".check-one:checked").length) $('.must-checked-student').removeClass("disabled"); else $('.must-checked-student').addClass("disabled");

                // Update GraduateMoveToData
                var sid = $(this).attr('sid');
                var lid = 0;
                var crid = 0;
                var studentId = $(this).attr('student-id');

                if (!!$('#sltLevelx' + sid).val()) {
                    lid = $('#sltLevelx' + sid).val();
                }
                if (!!$('#sltClassroomx' + sid).val()) {
                    crid = $('#sltClassroomx' + sid).val();
                }

                var item = studentList.GraduateMoveToData.find(function (e) {
                    if (e.sid == sid) {
                        return e;
                    }
                });
                var index = studentList.GraduateMoveToData.indexOf(item);
                if (index == -1) {
                    studentList.GraduateMoveToData.push(
                        { sid: sid, lid: lid, crid: crid, chk: $(this).prop('checked'), studentId: studentId }
                    );
                }
                else {
                    studentList.GraduateMoveToData[index].sid = sid;
                    studentList.GraduateMoveToData[index].lid = lid;
                    studentList.GraduateMoveToData[index].crid = crid;
                    studentList.GraduateMoveToData[index].chk = $(this).prop('checked');
                    studentList.GraduateMoveToData[index].studentId = studentId;
                }
            });

            $('#sltLevelxAll').change(function () {
                if (!!$(this).val()) {
                    var classRooms = $(studentList.ClassRoomData).filter(function (i, n) { return n.lid == $('#sltLevelxAll').val(); });
                    if (classRooms && classRooms.length > 0) {
                        $('#sltClassroomxAll').empty();
                        $.each(classRooms, function (i, data) {
                            $('#sltClassroomxAll').append($('<option/>', { text: data.name, value: data.id }));
                        });
                    }
                    $('#sltClassroomxAll').selectpicker('refresh');

                    $('select[id^="sltLevelx"]:not([id=sltLevelxAll])').selectpicker('val', $(this).val());
                    $('select[id^="sltLevelx"]:not([id=sltLevelxAll])').trigger("change");
                }
                else {
                    $('#sltClassroomxAll').empty();
                    $('#sltClassroomxAll').prepend("<option value='' selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111072") %></option>");
                    $('#sltClassroomxAll').selectpicker('refresh');
                }
            });

            $('#sltClassroomxAll').change(function () {
                if (!!$(this).val()) {
                    $('select[id^="sltClassroomx"]:not([id=sltClassroomxAll])').selectpicker('val', $(this).val());
                    $('select[id^="sltClassroomx"]:not([id=sltClassroomxAll])').trigger("change");
                }
            });

            $('#tableData').on('change', 'select[id^="sltLevelx"]', function () {
                var sid = $(this).attr('id').replace('sltLevelx', '').replace('All', '');
                var studentId = $(this).data('student-id');
                if (!!$(this).val() && sid) {
                    var lid = $(this).val();
                    var crid = 0;
                    var classRooms = $.grep(studentList.ClassRoomData, function (classroom, i) { return classroom.lid == lid; });
                    if (classRooms && classRooms.length > 0) {
                        $('#sltClassroomx' + sid).empty();
                        $.each(classRooms, function (i, data) {
                            $('#sltClassroomx' + sid).append($('<option/>', { text: data.name, value: data.id }));
                        });
                        $('#sltClassroomx' + sid).selectpicker('refresh');

                        if (!!$('#sltClassroomx' + sid).val()) {
                            crid = $('#sltClassroomx' + sid).val();
                        }
                    }

                    // Enable/Disable checkbox when classroom is empty
                    $('.check-one[sid=' + sid + ']').attr("disabled", !(classRooms && classRooms.length > 0));

                    // Update GraduateMoveToData
                    var item = studentList.GraduateMoveToData.find(function (e) {
                        if (e.sid == sid) {
                            return e;
                        }
                    });
                    var index = studentList.GraduateMoveToData.indexOf(item);
                    if (index == -1) {
                        studentList.GraduateMoveToData.push(
                            { sid: sid, lid: lid, crid: crid, studentId: studentId }
                        );
                    }
                    else {
                        studentList.GraduateMoveToData[index].sid = sid;
                        studentList.GraduateMoveToData[index].lid = lid;
                        studentList.GraduateMoveToData[index].crid = crid;
                        studentList.GraduateMoveToData[index].studentId = studentId;
                    }
                }
            });

            $('#tableData').on('change', 'select[id^="sltClassroomx"]', function () {
                var sid = $(this).attr('id').replace('sltClassroomx', '').replace('All', '');
                var studentId = $(this).data('student-id');
                if (!!$(this).val() && sid) {
                    var lid = 0;
                    var crid = $(this).val();

                    if (!!$('#sltLevelx' + sid).val()) {
                        lid = $('#sltLevelx' + sid).val();
                    }

                    // Update GraduateMoveToData
                    var item = studentList.GraduateMoveToData.find(function (e) {
                        if (e.sid == sid) {
                            return e;
                        }
                    });
                    var index = studentList.GraduateMoveToData.indexOf(item);
                    if (index == -1) {
                        studentList.GraduateMoveToData.push(
                            { sid: sid, lid: lid, crid: crid, studentId: studentId }
                        );
                    }
                    else {
                        studentList.GraduateMoveToData[index].sid = sid;
                        studentList.GraduateMoveToData[index].lid = lid;
                        studentList.GraduateMoveToData[index].crid = crid;
                        studentList.GraduateMoveToData[index].studentId = studentId;
                    }
                }
            });

            // Search
            $("#sltYear").change(function () {

                LoadTerm($(this).val(), '#sltTerm');

            });

            $("#sltLevel").change(function () {

                LoadTermSubLevel2($(this).val(), '#sltClass');

            });

            // Modal Section
            //

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
                        var param = { keyword: qry, termID: $('.studentList #sltTerm').val() };
                        $.ajax({
                            url: "StudentGraduateMoveTo.aspx/GetStudentName",
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
            $('#btnMoveTo').click(function () {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/App_Logic/StudentLimitInContact.ashx",
                    data: '{}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        // console.log(response);
                        if (response.success) {
                            if (response.data.limitInContact > 0 && response.data.remainingNumber > 0) {
                                CustomConfig();
                            }
                            else {
                                systemMessage.LimitInContact(response);
                            }
                        }
                        else {
                            console.log(response);
                        }
                    },
                    failure: function (response) {
                        console.log(response);
                    },
                    error: function (response) {
                        console.log(response);
                    }
                });

                return false;
            });
            $('#btnConfigSave').click(function () {

                StudentGraduateMoveTo();

                return false;
            });
            $('#btnStudentGraduateMoveTo').click(function () {

                $("#modalWaitDialog").modal('show');

                $.ajax({
                    async: true,
                    type: "POST",
                    url: "StudentGraduateMoveTo.aspx/StudentGraduateMoveToClassRoom",
                    data: JSON.stringify({ studentDatas: studentList.GraduateMoveToData, yearID: $('#sltStudentGraduateMoveToYear').val(), year: $('#sltStudentGraduateMoveToYear option:selected').text() }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var title = "";
                        var body = "";

                        var r = JSON.parse(response.d);
                        if (r.success) {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101347") %> [<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101266") %>: ' + r.log.success + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01611") %>, <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01407") %>: ' + r.log.unsuccess + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01611") %>]';
                            if (r.log.unsuccess > 0) {
                                body += "<br/><br/>Error Message: <br/>" + r.log.error;
                            }

                            $('#modalStudentGraduateMoveTo').modal('hide');
                            $('#modalCustomConfig').modal('hide');

                            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                            $("#modalNotifyOnlyClose").find('.modal-body p').html(body);
                            $("#modalNotifyOnlyClose").modal('show');

                            studentList.ReloadListData();

                            // Clear choose data
                            studentList.GraduateMoveToData = [];
                            $('.must-checked-student').addClass("disabled");
                        }
                        else {

                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111054") %> [' + r.message + ']';

                            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                            $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                            $("#modalNotifyOnlyClose").modal('show');
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

                return false;
            });

            // Datatable Section
            studentList.LoadListData();
        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
