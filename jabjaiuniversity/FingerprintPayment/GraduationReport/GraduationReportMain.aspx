<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="GraduationReportMain.aspx.cs" Inherits="FingerprintPayment.GraduationReport.GraduationReportMain" %>

<%--<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="FingerprintPayment.Employees.List" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />

    <style type="text/css">
        #tableData .btn-group {
            display: inline-flex;
        }

        #tableData .dropdown-menu {
            font-size: inherit;
        }

        .table > tbody > tr > td.vertical-align-middle {
            vertical-align: middle;
        }

        .table > thead > tr > th.vertical-align-middle {
            vertical-align: middle;
            padding-right: 10px;
        }

        .error {
            color: red;
        }

        .modal-title {
            font-size: 36px;
        }

        .modal-body {
            font-size: 26px;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">

        <div class="row text-center  text-handle">
            <div class="col-md-2 col-xs-12">
                <label class="text-handle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
            </div>
            <div class="col-md-3 col-xs-12">

                <select class="form-control" id="ddlYear">
                     <option value =""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                </select>
            </div>
            <div class="col-md-1 col-xs-12">
            </div>
            <div class="col-md-2 col-xs-12">
                <label class="text-handle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
            </div>
            <div class="col-md-3 col-xs-12">
                <select class="form-control" id="ddlTerm">
                    <option value =""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                </select>
            </div>
            <div class="col-md-1 col-xs-12">
            </div>
        </div>

        <div class="row text-center  text-handle">
            <div class="col-md-2 col-xs-12">
                <label class="text-handle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></label>
            </div>
            <div class="col-md-3 col-xs-12">
                <select class="form-control" id="ddlSubLV">
                    <option value =""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                </select>
            </div>
            <div class="col-md-1 col-xs-12">
            </div>
            <div class="col-md-2 col-xs-12">
            </div>
            <div class="col-md-3 col-xs-12">
            </div>
            <div class="col-md-1 col-xs-12">
            </div>
        </div>

                              
        <div class="List">
            <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                <div class="col-12 text-center">
                    <button id="btnSearch" type="button" class="btn btn-primary col-md-2" style="float: none;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
                </div>
                <div class="col-md-offset-8 col-md-4 text-center">
                    <button id="printPage" type="button" class="btn btn-warning" onclick="ClickPrint()" style="float: none;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206403") %></button>
                </div>

            </div>
            <table id="tableData" class="table table-bordered table-hover">
                <thead>
                    <tr>
                        <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                        <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                        <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206402") %></th>
                        <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></th>
                        <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                        <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                        <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></th>

                    </tr>
                </thead>

                <tfoot>
                    <tr>
                        <th colspan="7">
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
    <!-- DataTables -->
    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript" src="/scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js"></script>

    <script language="javascript" type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>
    <script type="text/javascript" src="JSGraduationReportMain.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script type='text/javascript'>

        var List = {
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            dt: null,
            LoadListData: function () {
                List.dt = $(".List #tableData").DataTable({
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
                        "url": "/GraduationReport/AshxFile/LoadStdGraduationList.ashx",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.ddlYear = $("#ddlYear").val();
                            d.ddlTerm = $("#ddlTerm").val();
                            d.ddlSubLV = $("#ddlSubLV").val();
                            d.page = List.PageIndex;
                            d.length = List.PageSize;

                            return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                        }
                    },
                    "columns": [
                        { "data": "no", "orderable": false },
                        { "data": "StudentId", "orderable": false },
                        { "data": "Identification", "orderable": false },
                        { "data": "Title", "orderable": false },
                        { "data": "Name", "orderable": false },
                        { "data": "Lastname", "orderable": false },
                        { "data": "ClassDisplay", "orderable": false },
                    ],
                    "order": [[2, "asc"]],
                    "columnDefs": [
                        { className: "vertical-align-middle text-center", "targets": [0, 6] },
                        { className: "text-center", "targets": [1, 2, 3, 4, 5, 6] },
                    ],
                    "drawCallback": function (settings) {
                        var json = settings.json;

                        List.PageCount = json.pageCount;

                        var options = '';
                        for (var pi = 0; pi < json.pageCount; pi++) {
                            options += '<option ' + (List.PageIndex == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                        }
                        $('.List #tableData #sltPageIndex').html(options);

                        $('.List #tableData #spnPageInfo').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (List.PageIndex + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + List.PageCount + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');
                    }
                });
                // order.dt search.dt
                List.dt.on('draw.dt', function () {
                    List.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (List.PageIndex * List.PageSize) + (i + 1) + '.';
                    });
                });
            },

            ReloadListData: function () {
                //List.dt = $('.List #tableData').DataTable();
                List.dt.draw();
            }
        }

        $(function () {



            // Searching, Pagination event 
            $('.List #btnSearch').click(function () {

                List.PageIndex = 0;

                List.ReloadListData();

                return false;
            });

            $('.List #tableData #sltPageSize').change(function () {

                List.PageSize = parseInt($(".List #tableData #sltPageSize").children("option:selected").val());
                List.PageIndex = 0;

                List.ReloadListData();

                return false;
            });

            $('.List #tableData #sltPageIndex').change(function () {

                List.PageIndex = parseInt($(".List #tableData #sltPageIndex").children("option:selected").val());

                List.ReloadListData();

                return false;
            });

            $('.List #tableData #aPrevious').click(function () {

                if (List.PageIndex > 0) {
                    List.PageIndex--;
                    List.ReloadListData();
                }

                return false;
            });
            $('.List #tableData #aNext').click(function () {

                if (List.PageIndex < (List.PageCount - 1)) {
                    List.PageIndex++;
                    List.ReloadListData();
                }

                return false;
            });



            // Datatable Section
            List.LoadListData();

        });

    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
