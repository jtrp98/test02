<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpTeaching.aspx.cs" Inherits="FingerprintPayment.Employees.EmpTeaching" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <div class="empTeachingList">
                <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102092") %></p>
                <table id="tableData" class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503007") %></th>
                            <th style="width: 25%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121095") %></th>
                            <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121096") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></th>
                            <th style="width: 10%" class="text-center">ชั่วโมง/สัปดาร์</th>
                            <th style="width: 10%" class="text-center"></th>
                            <th></th>
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
            <script type="text/javascript">

                var empTeachingList = {
                    PageIndex: 0,
                    PageSize: 20,
                    PageCount: 0,
                    LoadListData: function (eid) {
                        var dt = $(".empTeachingList #tableData").DataTable({
                            "processing": true,
                            "serverSide": true,
                            "info": false,
                            "searching": false,
                            "paging": false,
                            "stateSave": true,
                            "ajax": {
                                "url": "Ashx/LoadEmpTeachingList.ashx", 
                                "type": "POST", 
                                "contentType": "application/json; charset=utf-8", 
                                "data": function (d) { 
                                    d.eid = eid;
                                    d.search = '';
                                    d.page = empTeachingList.PageIndex;
                                    d.length = empTeachingList.PageSize;

                                    return  JSON.stringify(d, function (key, value) {return (value === undefined) ? "" : value});
                                }
                            },
                            "columns": [
                                { "data": "no", "orderable": false },
                                { "data": "Year", "orderable": true },
                                { "data": "Term", "orderable": true },
                                { "data": "GroupLearning", "orderable": true },
                                { "data": "SubjectCode", "orderable": true },
                                { "data": "Class", "orderable": true },
                                { "data": "HourWeek", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[8, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7] },
                                { "targets": [8], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<div class="btn-group">' +
                                                    '<button type="button" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121013") %></button>' +
                                                '<button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">' +
                                                    '<span class="caret"></span>' +
                                                    '<span class="sr-only">Toggle Dropdown</span>' +
                                                '</button>' +
                                                '<ul class="dropdown-menu" role="menu">' +
                                                    '<li><a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="EmpTeaching.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121093") %>"><i class="fa fa-edit"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></a></li>' +
                                                    '<li><a href="#" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="Are you sure you want to remove this item?"><i class="fa fa-remove"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></a></li>' +
                                                '</ul>' +
                                            '</div>';
                                    },
                                    "targets": 7
                                }
                            ],
                            "drawCallback": function (settings) { 
                                var json = settings.json;

                                empTeachingList.PageCount = json.pageCount;

                                var options = '';
                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    options += '<option ' + (empTeachingList.PageIndex == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                                }
                                $('.empTeachingList #tableData #sltPageIndex').html(options);

                                $('.empTeachingList #tableData #spnPageInfo').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (empTeachingList.PageIndex + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + empTeachingList.PageSize + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');
                            }
                        });
                        // order.dt search.dt
                        dt.on('draw.dt', function () {
                            var info = dt.page.info();
                            dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (info.page * info.length) + (i + 1) + '.';
                            });
                            dt.column(7, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = dt.cells({ row: i, column: 8 }).data()[0];
                                $(cell).find(".fa-edit").parent().attr("id", id);
                                $(cell).find(".fa-remove").parent().attr("id", id);
                            });
                        });
                    },
                    ReloadListData: function(){
                        var dt = $('.empTeachingList #tableData').DataTable();
                        dt.draw();
                    }
                }

                $(document).ready(function () {

                    // Searching, Pagination event 
                    $('.empTeachingList #tableData #sltPageSize').change(function () {

                        empTeachingList.PageSize = parseInt($(".empTeachingList #tableData #sltPageSize").children("option:selected").val());
                        empTeachingList.PageIndex = 0;

                        empTeachingList.ReloadListData();

                        return false;
                    });

                    $('.empTeachingList #tableData #sltPageIndex').change(function () {

                        empTeachingList.PageIndex = $(".empTeachingList #tableData #sltPageIndex").children("option:selected").val();

                        empTeachingList.ReloadListData();

                        return false;
                    });

                    $('.empTeachingList #tableData #aPrevious').click(function () {

                        if (empTeachingList.PageIndex > 0) {
                            empTeachingList.PageIndex--;
                            empTeachingList.ReloadListData();
                        }

                        return false;
                    });
                    $('.empTeachingList #tableData #aNext').click(function () {

                        if (empTeachingList.PageIndex < (empTeachingList.PageCount - 1)) {
                            empTeachingList.PageIndex++;
                            empTeachingList.ReloadListData();
                        }

                        return false;
                    });

                    // Datatable Section
                    empTeachingList.LoadListData(<%=Request.QueryString["eid"]%>);

                    // Modal Section

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            View Content
        </asp:View>
    </asp:MultiView>
</body>
</html>
