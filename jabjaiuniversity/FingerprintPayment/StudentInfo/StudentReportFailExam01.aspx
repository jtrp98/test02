<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="StudentReportFailExam01.aspx.cs" Inherits="FingerprintPayment.StudentInfo.StudentReportFailExam01" %>

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

        .btn.btn-success, .btn.btn-cancel, .btn.btn-danger, .btn.btn-primary {
            width: 110px;
            height: 44px;
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

        .checkbox-inline input[type=checkbox] {
            /* Double-sized Checkboxes */
            -ms-transform: scale(1.3); /* IE */
            -moz-transform: scale(1.3); /* FF */
            -webkit-transform: scale(1.3); /* Safari and Chrome */
            -o-transform: scale(1.3); /* Opera */
            transform: scale(1.3);
            padding: 10px;
            cursor: pointer;
        }

        .radio-inline + .radio-inline, .checkbox-inline + .checkbox-inline {
            margin-top: 0;
            margin-left: -5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="studentList">
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-6">
                    <label class="col-md-5">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                    <div class="col-md-7">
                        <select id="sltYear" name="sltYear[]"
                            class="form-control">
                            <asp:Literal ID="ltrYear" runat="server" />
                        </select>
                    </div>
                </div>
                <div class="col-md-6">
                    <label class="col-md-5">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                    <div class="col-md-7">
                        <select id="sltTerm" name="sltTerm[]"
                            class="form-control">
                            <asp:Literal ID="ltrTerm" runat="server" />
                        </select>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-6">
                    <label class="col-md-5">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206067") %></label>
                    <div class="col-md-7">
                        <label class="checkbox-inline">
                            <input type="checkbox" class="grade-search" value="0" style="position: inherit; margin: 0 5px 0 -17px;" />0</label>
                        <label class="checkbox-inline">
                            <input type="checkbox" class="grade-search" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>" style="position: inherit; margin: 0 5px 0 0;" /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %></label>
                        <label class="checkbox-inline">
                            <input type="checkbox" class="grade-search" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206416") %>" style="position: inherit; margin: 0 5px 0 0;" /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111096") %></label>
                        <label class="checkbox-inline">
                            <input type="checkbox" class="grade-search" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206419") %>" style="position: inherit; margin: 0 5px 0 0;" /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111097") %></label>
                    </div>
                </div>
                <div class="col-md-6">
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
            <div class="row">
                <div class="col-md-12 text-right">
                    <button id="btnExportPDF" class="btn btn-info btn-round col-md-2" style="font-size: 24px; float: none;">
                        Export PDF
                    </button>
                </div>
            </div>
            <table id="tableData" class="table table-bordered table-hover" style="width: 100%">
                <thead>
                    <tr>
                        <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                        <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></th>
                        <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></th>
                        <th style="width: 25%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                        <th style="width: 50%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111093") %></th>
                        <th></th>
                    </tr>
                </thead>

                <tfoot>
                    <tr>
                        <th colspan="6">
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

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">

    <script type='text/javascript' src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>

    <!-- DataTables -->
    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>

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
                        "url": "StudentReportFailExam01.aspx/LoadStudent",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.year = $(".studentList #sltYear").children("option:selected").val();
                            d.term = $(".studentList #sltTerm").children("option:selected").val();
                            d.page = studentList.PageIndex;
                            d.length = studentList.PageSize;

                            // get grade
                            var gradeFilter = '';
                            $.each($('input.grade-search:checked'), function (i, obj) {
                                gradeFilter += ', ' + $(obj).val();
                            });
                            d.grade = gradeFilter;

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
                        { "data": "No", "orderable": false },
                        { "data": "Level", "orderable": false },
                        { "data": "ClassRoom", "orderable": false },
                        { "data": "Name", "orderable": false },
                        { "data": "SubjectResultGrade", "orderable": false },
                        { "data": "sid", "orderable": false },
                        { "data": "rn", "orderable": true }
                    ],
                    "order": [[6, "asc"]],
                    "columnDefs": [
                        //{ className: "vertical-align-middle text-center", "targets": [0, 7] },
                        { className: "text-center", "targets": [0, 1, 2] },
                        { "targets": [5, 6], "visible": false }
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
                    url: "RetestStudentListByClassRoom.aspx/LoadTerm",
                    data: '{yearID: ' + yearID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var yearData = response.d;

                        $(objResult).empty();

                        if (yearData.length > 0) {

                            var options = '';
                            $(yearData).each(function () {

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

            $('.studentList #tableData #sltPageSize').change(function () {

                studentList.PageSize = parseInt($(".studentList #tableData #sltPageSize").children("option:selected").val());
                studentList.PageIndex = 0;

                studentList.ReloadListData();

                return false;
            });

            $('.studentList #tableData #sltPageIndex').change(function () {

                studentList.PageIndex = parseInt($(".studentList #tableData #sltPageIndex").children("option:selected").val());

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
            $("#sltYear").change(function () {

                LoadTerm($(this).val(), '#sltTerm');

            });

            $('.studentList #btnExportPDF').click(function () {

                var year = $("#sltYear").children("option:selected").val();
                var term = $("#sltTerm").children("option:selected").val();

                // get grade
                var grade = '';
                $.each($('input.grade-search:checked'), function (i, obj) {
                    grade += ',' + $(obj).val();
                });

                window.open("Ashx/ExportStudentReportFailExam01PDF.ashx?year=" + year + "&term=" + term + "&grade=" + grade);

                return false;
            });

            // Modal Section

            // Initial control

            // Datatable Section
            studentList.LoadListData();
        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
