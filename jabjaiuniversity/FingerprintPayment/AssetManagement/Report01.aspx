<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report01.aspx.cs" Inherits="Staff.AssetManagement.Report01" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div class="reportList">
        <div class="row div-row-padding">
            <div class="col-md-12 mb-12 text-left" style="padding-left: 1%;">
                <span>หน้าหลัก > รายงานคงเหลือตามกลุ่ม</span>
            </div>
        </div>
        <div class="row div-row-padding">
            <div class="col-md-3 mb-3">
                <label
                    for="sltYear">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</label>
            </div>
            <div class="col-md-3 mb-3">
                <select id="sltYear" name="sltYear[]"
                    class="form-control">
                </select>
            </div>
            <div class="col-md-3 mb-3">
                <label
                    for="sltYear">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132003") %> :</label>
            </div>
            <div class="col-md-3 mb-3">
                <select id="sltCategory" name="sltCategory[]"
                    class="form-control">
                </select>
            </div>
        </div>
        <div class="row div-row-padding">
            <div class="col-md-12 mb-12 text-center">
                <button id="btnSearch" class="btn btn-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
                <button id="btnExportFile" class="btn btn-success" style="float: right;">Export File</button>
            </div>
        </div>
        <table id="tableData" class="table table-bordered table-hover">
            <thead>
                <tr>
                    <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                    <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132020") %></th>
                    <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132003") %></th>
                    <th style="width: 25%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502020") %></th>
                    <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></th>
                    <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601045") %></th>
                    <th style="width: 20%" class="text-center">แผนก/หน่วยงาน</th>
                    <th></th>
                </tr>
            </thead>

            <tfoot>
                <tr>
                    <th colspan="8">
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
    <script type="text/javascript">

        var reportList = {
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            LoadListData: function () {
                var dt = $(".reportList #tableData").DataTable({
                    "processing": true,
                    "serverSide": true,
                    "info": false,
                    "searching": false,
                    "paging": false,
                    "stateSave": true,
                    "ajax": {
                        "url": "Ashx/LoadReport01List.ashx",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.searchYear = $("#sltYear").children("option:selected").val();
                            d.searchCategory = $("#sltCategory").children("option:selected").val();
                            d.page = reportList.PageIndex;
                            d.length = reportList.PageSize;

                            return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                        }
                    },
                    "columns": [
                        { "data": "no", "orderable": false },
                        { "data": "Code", "orderable": true },
                        { "data": "Category", "orderable": true },
                        { "data": "Product", "orderable": true },
                        { "data": "Amount", "orderable": true },
                        { "data": "Unit", "orderable": true },
                        { "data": "Department", "orderable": true },
                        { "data": "id", "orderable": false }
                    ],
                    "order": [[7, "desc"]],
                    "columnDefs": [
                        { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7] },
                        { "targets": [7], "visible": false }
                    ],
                    "drawCallback": function (settings) {
                        var json = settings.json;

                        reportList.PageCount = json.pageCount;

                        var options = '';
                        for (var pi = 0; pi < json.pageCount; pi++) {
                            options += '<option ' + (reportList.PageIndex == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                        }
                        $('.reportList #tableData #sltPageIndex').html(options);

                        $('.reportList #tableData #spnPageInfo').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (reportList.PageIndex + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + reportList.PageSize + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');
                    }
                });
                // order.dt search.dt
                dt.on('draw.dt', function () {
                    dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (reportList.PageIndex * reportList.PageSize) + (i + 1) + '.';
                    });
                });
            },
            ReloadListData: function () {
                var dt = $('.reportList #tableData').DataTable();
                dt.draw();
            }
        }

        $(document).ready(function () {

            // Searching, Pagination event 
            $('.reportList #btnSearch').click(function () {

                reportList.PageIndex = 0;

                reportList.ReloadListData();

                return false;
            });

            $('.reportList #tableData #sltPageSize').change(function () {

                reportList.PageSize = parseInt($(".reportList #tableData #sltPageSize").children("option:selected").val());
                reportList.PageIndex = 0;

                reportList.ReloadListData();

                return false;
            });

            $('.reportList #tableData #sltPageIndex').change(function () {

                reportList.PageIndex = $(".reportList #tableData #sltPageIndex").children("option:selected").val();

                reportList.ReloadListData();

                return false;
            });

            $('.reportList #tableData #aPrevious').click(function () {

                if (reportList.PageIndex > 0) {
                    reportList.PageIndex--;
                    reportList.ReloadListData();
                }

                return false;
            });
            $('.reportList #tableData #aNext').click(function () {

                if (reportList.PageIndex < (reportList.PageCount - 1)) {
                    reportList.PageIndex++;
                    reportList.ReloadListData();
                }

                return false;
            });

            // Export File
            $('.reportList #btnExportFile').click(function () {

                var dt = $(".reportList #tableData").DataTable();
                var order = dt.order();

                window.open("Ashx/ExportReport01Excel.ashx?year=" + $("#sltYear").children("option:selected").val() + "&cate=" + $("#sltCategory").children("option:selected").val() + "&col=" + order[0][0] + "&dir=" + order[0][1]);

                return false;
            });

            // Initial data
            initFunction.setDropdown('#sltYear', 'Year', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %></option>');
            initFunction.setDropdown('#sltCategory', 'Category', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132009") %></option>');

            // Datatable Section
            reportList.LoadListData();

        });

    </script>
</body>
</html>
