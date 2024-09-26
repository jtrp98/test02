<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="ReportEQ.aspx.cs" Inherits="FingerprintPayment.Qusetion.ReportEQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />

    <style type="text/css">

        .modal-body {
            font-size: 26px;
        }

        .dropdown-menu {
            font-size: 22px;
        }

        .ui-menu {
            list-style: none;
            padding: 2px;
            margin: 0;
            display: block;
            float: left;
        }

            .ui-menu .ui-menu {
                margin-top: -3px;
            }

            .ui-menu .ui-menu-item {
                margin: 0;
                padding: 0;
                zoom: 1;
                float: left;
                clear: left;
                width: 100%;
            }

                .ui-menu .ui-menu-item a {
                    text-decoration: none;
                    display: block;
                    padding: .2em .4em;
                    line-height: 1.5;
                    zoom: 1;
                    cursor: pointer;
                }

                    .ui-menu .ui-menu-item a strong {
                        color: orange;
                    }

                    .ui-menu .ui-menu-item a.ui-state-hover,
                    .ui-menu .ui-menu-item a.ui-state-active {
                        font-weight: normal;
                        margin: -1px;
                    }

        .btn.btn-success, .btn.btn-cancel, .btn.btn-danger, .btn.btn-primary {
            width: 110px;
            height: 44px;
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
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                    <div class="col-md-7">
                        <select id="sltLevel" name="sltLevel[]"
                            class="form-control">
                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                            <asp:Literal ID="ltrLevel" runat="server" />
                        </select>
                    </div>
                </div>
                <div class="col-md-6">
                    <label class="col-md-5">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                    <div class="col-md-7">
                        <select id="sltClass" name="sltClass[]"
                            class="form-control">
                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-6">
                    <label class="col-md-5">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                    <div class="col-md-7">
                        <input id="iptStudentName" name="iptStudentName" type="text" class="form-control" style="width: 100%;" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101013") %>" />
                    </div>
                </div>
                <div class="col-md-6" style="display: none;">
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
            <table id="tableData" class="table table-bordered table-hover" style="width: 100%">
                <thead>
                    <tr>
                        <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                        <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                        <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                        <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                        <th style="width: 12%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></th>
                        <th style="width: 11%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306023") %></th>
                        <th style="width: 11%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306024") %></th>
                        <th style="width: 11%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306025") %></th>
                        <th style="width: 11%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01121") %></th>
                    </tr>
                </thead>

                <tfoot>
                    <tr>
                        <th colspan="9">
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

    <script type="text/javascript" src="/scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js"></script>

    <script language="javascript" type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>


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
                        "url": "ReportEQ.aspx/LoadReportEQ",
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
                            $("#modalWaitDialog").modal('show');
                        },
                        "complete": function () {
                            // Handle the complete event
                            $("#modalWaitDialog").modal('hide');
                        }
                    },
                    "columns": [
                        { "data": "no", "orderable": false },
                        { "data": "Code", "orderable": true },
                        { "data": "Name", "orderable": true },
                        { "data": "Lastname", "orderable": true },
                        { "data": "ClassName", "orderable": true },
                        { "data": "EQ11T", "orderable": true },
                        { "data": "EQ21T", "orderable": true },
                        { "data": "EQ31T", "orderable": true },
                        { "data": "EQSUMT", "orderable": true }
                    ],
                    "order": [[1, "asc"]],
                    "columnDefs": [
                        { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8] }
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
                    },
                    "createdRow": function (row, data, dataIndex) {

                    }
                });

                // order.dt search.dt
                studentList.dt.on('draw.dt', function () {
                    studentList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (studentList.PageIndex * studentList.PageSize) + (i + 1) + '.';
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
                    url: "ReportEQ.aspx/LoadTerm",
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

        function LoadTermSubLevel2(subLevelID, objResult) {
            if (subLevelID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "ReportEQ.aspx/LoadTermSubLevel2",
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

            $("#sltLevel").change(function () {

                LoadTermSubLevel2($(this).val(), '#sltClass');

            });

            // Modal Section
            $.ui.autocomplete.prototype._renderItem = function (ul, item) {
                var t = String(item.value).replace(
                    new RegExp(this.term, "gi"),
                    "<strong>$&</strong>");
                return $("<li></li>")
                    .data("item.autocomplete", item)
                    .append("<a>" + t + "</a>")
                    .appendTo(ul);
            };

            $(".studentList #iptStudentName").autocomplete({
                source: function (request, response) {
                    var param = { keyword: $('.studentList #iptStudentName').val(), termID: $('.studentList #sltTerm').val() };
                    $.ajax({
                        url: "ReportEQ.aspx/GetStudentName",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            console.log(textStatus);
                        }
                    });
                },
                select: function (event, ui) {
                    // ui.item
                    // ui.item.value
                },
                minLength: 1
            });

            // Initial control

            // Datatable Section
            studentList.LoadListData();
        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
