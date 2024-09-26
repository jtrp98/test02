<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="ReplyMessageReport.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.ReplyMessageReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />

    <style type="text/css">
        .table > tbody > tr > td.vertical-align-middle {
            vertical-align: middle;
        }

        .table > thead > tr > th.vertical-align-middle {
            vertical-align: middle;
            padding-right: 10px;
        }

        .modal {
        }

        .modal-body {
            font-size: 26px;
        }

        .modal-content {
            /* Bootstrap sets the size of the modal in the modal-dialog class, we need to inherit it */
            width: inherit;
            height: inherit;
            /* To center horizontally */
            margin: 0 auto;
        }

        .btn.btn-success, .btn.btn-cancel, .btn.btn-danger, .btn.btn-primary {
            width: 110px;
            height: 44px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="replyMessageList">
            <div class="row">
                <div class="col-md-6">
                    <label style="margin-bottom: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401060") %></label>
                    <span id="spnRead"></span>&nbsp;/ <span class="all-message"></span>
                    &nbsp;&nbsp;&nbsp;<label style="margin-bottom: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132635") %></label>
                    <span id="spnReply"></span>&nbsp;/ <span class="all-message"></span>
                </div>
                <div class="col-md-6">
                </div>
            </div>
            <table id="tableData" class="table table-bordered table-hover" style="width: 100%">
                <thead>
                    <tr>
                        <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                        <th style="width: 50%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></th>
                        <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></th>
                        <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                        <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132636") %></th>
                        <th></th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th colspan="5">
                            <div class="row">
                                <div class="col-md-4 mb-4 text-left" style="padding-left: 4%;">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>: </span>
                                    <select id="sltPageSize">
                                        <option value="20">20</option>
                                        <option value="50">50</option>
                                        <option selected="selected" value="100">100</option>
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
    <!-- DataTables -->
    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript">

        var replyMessageList = {
            PageIndex: 0,
            PageSize: 100,
            PageCount: 0,
            dt: null,
            LoadListData: function () {
                replyMessageList.dt = $(".replyMessageList #tableData").DataTable({
                    "processing": true,
                    "serverSide": true,
                    "info": false,
                    "searching": false,
                    "paging": false,
                    "stateSave": true,
                    "ajax": {
                        "url": "ReplyMessageReport.aspx/LoadReplyMessage",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.smsID = '<%=Request.QueryString["smsID"]%>';
                            d.page = replyMessageList.PageIndex;
                            d.length = replyMessageList.PageSize;

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
                        { "data": "Name", "orderable": true },
                        { "data": "Type", "orderable": true },
                        { "data": "ReadStatus", "orderable": true },
                        { "data": "ReplyMessage", "orderable": true },
                        { "data": "uid", "orderable": false }
                    ],
                    "order": [[5, "asc"]],
                    "columnDefs": [
                        { className: "vertical-align-middle text-center", "targets": [0, 2, 3, 4] },
                        { "targets": [5], "visible": false }
                    ],
                    "drawCallback": function (settings) {
                        //var json = settings.json;
                        var json = $.parseJSON(settings.json.d);

                        replyMessageList.PageCount = json.pageCount;

                        var options = '';
                        for (var pi = 0; pi < json.pageCount; pi++) {
                            options += '<option ' + (replyMessageList.PageIndex == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                        }
                        $('.replyMessageList #tableData #sltPageIndex').html(options);

                        $('.replyMessageList #tableData #spnPageInfo').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (replyMessageList.PageIndex + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + replyMessageList.PageCount + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');

                        $('.all-message').text(json.recordsTotal);
                        $('#spnRead').text(json.readCount);
                        $('#spnReply').text(json.replyCount);
                    }
                });

                // order.dt search.dt
                replyMessageList.dt.on('draw.dt', function () {
                    replyMessageList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (replyMessageList.PageIndex * replyMessageList.PageSize) + (i + 1) + '.';
                        //var uid = replyMessageList.dt.cells({ row: i, column: 5 }).data()[0];
                        //console.log({ uid: uid });
                    });
                });
            },
            ReloadListData: function () {
                replyMessageList.dt.draw();
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
            $('.replyMessageList #btnSearch').click(function () {

                replyMessageList.PageIndex = 0;

                replyMessageList.ReloadListData();

                return false;
            });

            $('.replyMessageList #tableData #sltPageSize').change(function () {

                replyMessageList.PageSize = parseInt($(".replyMessageList #tableData #sltPageSize").children("option:selected").val());
                replyMessageList.PageIndex = 0;

                replyMessageList.ReloadListData();

                return false;
            });

            $('.replyMessageList #tableData #sltPageIndex').change(function () {

                replyMessageList.PageIndex = parseInt($(".replyMessageList #tableData #sltPageIndex").children("option:selected").val());

                replyMessageList.ReloadListData();

                return false;
            });

            $('.replyMessageList #tableData #aPrevious').click(function () {

                if (replyMessageList.PageIndex > 0) {
                    replyMessageList.PageIndex--;
                    replyMessageList.ReloadListData();
                }

                return false;
            });

            $('.replyMessageList #tableData #aNext').click(function () {

                if (replyMessageList.PageIndex < (replyMessageList.PageCount - 1)) {
                    replyMessageList.PageIndex++;
                    replyMessageList.ReloadListData();
                }

                return false;
            });

            // Search

            // Modal Section

            // Initial control

            // Datatable Section
            replyMessageList.LoadListData();
        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
