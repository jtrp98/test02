<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="BackupCardHistory.aspx.cs" Inherits="FingerprintPayment.Card.BackupCardHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- DataTables -->
  <%--  <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />--%>
     <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style type="text/css">
        .modal-title {
            font-size: 36px;
        }

        .modal-body {
            font-size: 26px;
        }

        .dropdown-menu {
            font-size: 22px;
        }

        .btn.btn-success, .btn.btn-cancel, .btn.btn-danger, .btn.btn-primary, .btn.btn-default {
            width: 110px;
            height: 44px;
        }

        .modal {
        }

        .gray-text {
            padding: 0px 7px;
            color: #999;
        }


        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }

        table.dataTable tfoot tr:last-child th {
            border-top: 1px solid #000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106145") %>
            </p>
        </div>
    </div>

    <div class="row ">
        <div class="col-md-12">
            <div class="card ">

                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">search</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                </div>
                <div class="card-body ">
                    <div class="backupCardHistoryList">
                        <div class="row" style="">
                            <div class="col-md-2">
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106154") %></span>
                                <span class="gray-text">
                                    <asp:Literal ID="ltrCardName" runat="server"></asp:Literal></span>
                            </div>
                            <div class="col-md-2">
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106155") %></span>
                                <span class="gray-text">
                                    <asp:Literal ID="ltrBarCode" runat="server"></asp:Literal></span>
                            </div>
                            <div class="col-md-4">
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106150") %></span>
                                <span class="gray-text">
                                    <asp:Literal ID="ltrNFC" runat="server"></asp:Literal></span>
                            </div>
                            <div class="col-md-4">
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <table id="tableData" class="dataTable  table-hover" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106152") %></th>
                                            <th style="width: 25%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106169") %></th>
                                            <th style="width: 25%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></th>
                                            <th style="width: 25%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                        </tr>
                                    </thead>

                                   <%-- <tfoot>
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
                                    </tfoot>--%>
                                </table>
                            </div>
                        </div>

                        <div class="row">
                            <br />
                        </div>
                        <div class="row" style="padding-bottom: 10px;">
                            <div class="col-md-12 text-center">
                                <button id="btnBack" class="btn btn-info ">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <!-- DataTables -->
<%--    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>--%>

    <script type="text/javascript">

        var backupCardHistoryList = {
            PageIndex: 0,
            PageSize: 100,
            PageCount: 0,
            dt: null,
            LoadListData: function (cid) {
                backupCardHistoryList.dt = $(".backupCardHistoryList #tableData").DataTable({
                    "processing": true,
                    //"serverSide": true,
                    "info": false,
                    "searching": false,
                     paging: true,
                    "pageLength": 20,
                    "bLengthChange": false,
                    //"stateSave": true,
                    "ajax": {
                        "url": "BackupCardHistory.aspx/LoadBackupCardHistory",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.cid = cid;
                            d.page = backupCardHistoryList.PageIndex;
                            d.length = backupCardHistoryList.PageSize;

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
                        { "data": "BorrowingDate", "orderable": true },
                        { "data": "ReturnDate", "orderable": true },
                        { "data": "UserType", "orderable": true },
                        { "data": "UserName", "orderable": true }
                    ],
                    //"order": [[1, "desc"]],
                    "columnDefs": [
                        { className: "text-center", "targets": [0, 1, 2, 3, 4] }
                    ],
                    //"drawCallback": function (settings) {
                    //    //var json = settings.json;
                    //    var json = $.parseJSON(settings.json.d);

                    //    backupCardHistoryList.PageCount = json.pageCount;

                    //    var options = '';
                    //    for (var pi = 0; pi < json.pageCount; pi++) {
                    //        options += '<option ' + (backupCardHistoryList.PageIndex == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                    //    }
                    //    $('.backupCardHistoryList #tableData #sltPageIndex').html(options);

                    //    $('.backupCardHistoryList #tableData #spnPageInfo').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (backupCardHistoryList.PageIndex + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + backupCardHistoryList.PageCount + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');
                    //}
                });

                // order.dt search.dt
                backupCardHistoryList.dt.on('draw.dt', function () {
                    backupCardHistoryList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = ('00' + ((backupCardHistoryList.PageIndex * backupCardHistoryList.PageSize) + (i + 1))).slice(-2);
                    });
                });
            },
            ReloadListData: function () {
                backupCardHistoryList.dt.draw();
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
            $('.backupCardHistoryList #btnSearch').click(function () {

                backupCardHistoryList.PageIndex = 0;

                backupCardHistoryList.ReloadListData();

                return false;
            });

            $('.backupCardHistoryList #tableData #sltPageSize').change(function () {

                backupCardHistoryList.PageSize = parseInt($(".backupCardHistoryList #tableData #sltPageSize").children("option:selected").val());
                backupCardHistoryList.PageIndex = 0;

                backupCardHistoryList.ReloadListData();

                return false;
            });

            $('.backupCardHistoryList #tableData #sltPageIndex').change(function () {

                backupCardHistoryList.PageIndex = parseInt($(".backupCardHistoryList #tableData #sltPageIndex").children("option:selected").val());

                backupCardHistoryList.ReloadListData();

                return false;
            });

            $('.backupCardHistoryList #tableData #aPrevious').click(function () {

                if (backupCardHistoryList.PageIndex > 0) {
                    backupCardHistoryList.PageIndex--;
                    backupCardHistoryList.ReloadListData();
                }

                return false;
            });

            $('.backupCardHistoryList #tableData #aNext').click(function () {

                if (backupCardHistoryList.PageIndex < (backupCardHistoryList.PageCount - 1)) {
                    backupCardHistoryList.PageIndex++;
                    backupCardHistoryList.ReloadListData();
                }

                return false;
            });

            $('#btnBack').click(function () {

                window.location.href = "BackupCardList.aspx";

                return false;
            });

            // Search

            // Initial control

            // Datatable Section
            backupCardHistoryList.LoadListData('<%=Request.QueryString["cid"]%>');
        });

    </script>
</asp:Content>

