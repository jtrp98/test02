<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpLeave.aspx.cs" Inherits="FingerprintPayment.Employees.EmpLeave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <div class="empLeaveList">
                <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121017") %></p>
                <table id="tableData" class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                            <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %></th>
                            <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131136") %></th>
                            <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></th>
                            <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></th>
                            <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></th>
                            <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01108") %></th>
                            <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01850") %></th>
                            <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121020") %></th>
                            <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121021") %></th>
                            <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121022") %></th>
                            <th style="width: 5%" class="text-center"><button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalShowForm" form-name="EmpLeave.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132125") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></button></th>
                            <th></th>
                        </tr>
                    </thead>

                    <tfoot>
                        <tr>
                            <th colspan="13">
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
                <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132121") %></p>
                <table id="tableData2" class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                            <th style="width: 12%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121023") %></th>
                            <th style="width: 14%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121024") %></th>
                            <th style="width: 12%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121018") %></th>
                            <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121019") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121025") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102084") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121012") %></th>
                            <th style="width: 10%" class="text-center"><button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalShowForm" form-name="EmpNameChange.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132122") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></button></th>
                            <th></th>
                        </tr>
                    </thead>

                    <tfoot>
                        <tr>
                            <th colspan="10">
                                <div class="row"> 
                                    <div class="col-md-4 mb-4 text-left" style="padding-left: 4%;">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>: </span>
                                        <select id="sltPageSize2">
                                            <option selected="selected" value="20">20</option>
                                            <option value="50">50</option>
                                            <option value="100">100</option>
                                        </select>
                                    </div>
                                    <div class="col-md-4 mb-4 text-center">
                                        <a id="aPrevious2" href="#" style="text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></a>
                                        <select id="sltPageIndex2">
                                        </select>
                                        <a id="aNext2" href="#" style="text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></a>
                                    </div>
                                    <div class="col-md-4 mb-4 text-right" style="padding-right: 2%;">
                                        <span id="spnPageInfo2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132005") %></span>
                                    </div>
                                </div>
                            </th>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <script type="text/javascript">

                var empLeaveList = {
                    PageIndex: 0,
                    PageSize: 20,
                    PageCount: 0,
                    LoadListData: function (eid) {
                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102049") %>
                        var dt = $(".empLeaveList #tableData").DataTable({
                            "processing": true,
                            "serverSide": true,
                            "info": false,
                            "searching": false,
                            "paging": false,
                            "stateSave": true,
                            "ajax": {
                                "url": "Ashx/LoadEmpLeaveList.ashx", 
                                "type": "POST", 
                                "contentType": "application/json; charset=utf-8", 
                                "data": function (d) { 
                                    d.eid = eid;
                                    d.search = '';
                                    d.page = empLeaveList.PageIndex;
                                    d.length = empLeaveList.PageSize;

                                    return  JSON.stringify(d, function (key, value) {return (value === undefined) ? "" : value});
                                }
                            },
                            "columns": [
                                { "data": "no", "orderable": false },
                                { "data": "Year", "orderable": true },
                                { "data": "Late", "orderable": true },
                                { "data": "Sick", "orderable": true },
                                { "data": "Errand", "orderable": true },
                                { "data": "Lacking", "orderable": true },
                                { "data": "ToGovernor", "orderable": true },
                                { "data": "Holiday", "orderable": true },
                                { "data": "Maternity", "orderable": true },
                                { "data": "Ordain", "orderable": true },
                                { "data": "ContinueStudy", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[1, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11] },
                                { "targets": [12], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<div class="btn-group">' +
                                                    '<button type="button" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121013") %></button>' +
                                                '<button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">' +
                                                    '<span class="caret"></span>' +
                                                    '<span class="sr-only">Toggle Dropdown</span>' +
                                                '</button>' +
                                                '<ul class="dropdown-menu" role="menu">' +
                                                    '<li><a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="EmpLeave.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132125") %>"><i class="fa fa-edit"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></a></li>' +
                                                    '<li><a href="#" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="Are you sure you want to remove this item?"><i class="fa fa-remove"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></a></li>' +
                                                '</ul>' +
                                            '</div>';
                                    },
                                    "targets": 11
                                }
                            ],
                            "drawCallback": function (settings) { 
                                var json = settings.json;

                                empLeaveList.PageCount = json.pageCount;

                                var options = '';
                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    options += '<option ' + (empLeaveList.PageIndex == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                                }
                                $('.empLeaveList #tableData #sltPageIndex').html(options);

                                $('.empLeaveList #tableData #spnPageInfo').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (empLeaveList.PageIndex + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + empLeaveList.PageSize + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');
                            }
                        });
                        // order.dt search.dt
                        dt.on('draw.dt', function () {
                            var info = dt.page.info();
                            dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (info.page * info.length) + (i + 1) + '.';
                            });
                            dt.column(11, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = dt.cells({ row: i, column: 12 }).data()[0];
                                $(cell).find(".fa-edit").parent().attr("id", id);
                                $(cell).find(".fa-remove").parent().attr("id", id);
                                $(cell).find(".fa-remove").parent().attr("tableNo", 1);
                            });
                        });
                    },
                    PageIndex2: 0,
                    PageSize2: 20,
                    PageCount2: 0,
                    LoadListData2: function (eid) {
                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102065") %>
                        var dt = $(".empLeaveList #tableData2").DataTable({
                            "processing": true,
                            "serverSide": true,
                            "info": false,
                            "searching": false,
                            "paging": false,
                            "stateSave": true,
                            "ajax": {
                                "url": "Ashx/LoadEmpNameChangeList.ashx", 
                                "type": "POST", 
                                "contentType": "application/json; charset=utf-8", 
                                "data": function (d) { 
                                    d.eid = eid;
                                    d.search = '';
                                    d.page = empLeaveList.PageIndex2;
                                    d.length = empLeaveList.PageSize2;

                                    return  JSON.stringify(d, function (key, value) {return (value === undefined) ? "" : value});
                                }
                            },
                            "columns": [
                                { "data": "no", "orderable": false },
                                { "data": "OldFirstName", "orderable": true },
                                { "data": "OldLastName", "orderable": true },
                                { "data": "NewFirstName", "orderable": true },
                                { "data": "NewLastName", "orderable": true },
                                { "data": "ChangeDate", "orderable": true },
                                { "data": "ChangePlace", "orderable": true },
                                { "data": "UpdateDate", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[5, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 5, 7, 8] },
                                { "targets": [9], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<div class="btn-group">' +
                                                    '<button type="button" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121013") %></button>' +
                                                '<button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">' +
                                                    '<span class="caret"></span>' +
                                                    '<span class="sr-only">Toggle Dropdown</span>' +
                                                '</button>' +
                                                '<ul class="dropdown-menu" role="menu">' +
                                                    '<li><a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="EmpNameChange.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132122") %>"><i class="fa fa-edit"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></a></li>' +
                                                    '<li><a href="#" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="Are you sure you want to remove this item?"><i class="fa fa-remove"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></a></li>' +
                                                '</ul>' +
                                            '</div>';
                                    },
                                    "targets": 8
                                }
                            ],
                            "drawCallback": function (settings) { 
                                var json = settings.json;

                                empLeaveList.PageCount2 = json.pageCount;

                                var options = '';
                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    options += '<option ' + (empLeaveList.PageIndex2 == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                                }
                                $('.empLeaveList #tableData2 #sltPageIndex2').html(options);

                                $('.empLeaveList #tableData2 #spnPageInfo2').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (empLeaveList.PageIndex2 + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + empLeaveList.PageSize2 + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');
                            }
                        });
                        // order.dt search.dt
                        dt.on('draw.dt', function () {
                            var info = dt.page.info();
                            dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (info.page * info.length) + (i + 1) + '.';
                            });
                            dt.column(8, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = dt.cells({ row: i, column: 9 }).data()[0];
                                $(cell).find(".fa-edit").parent().attr("id", id);
                                $(cell).find(".fa-remove").parent().attr("id", id);
                                $(cell).find(".fa-remove").parent().attr("tableNo", 2);
                            });
                        });
                    },
                    RemoveItem: function (eid, id) {
                        $.ajax({
                            type: "POST",
                            url: "EmpLeave.aspx/RemoveItem",
                            data: '{eid: ' + eid + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empLeaveList.OnSuccessRemove,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    RemoveItem2: function (eid, id) {
                        $.ajax({
                            type: "POST",
                            url: "EmpLeave.aspx/RemoveItem2",
                            data: '{eid: ' + eid + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empLeaveList.OnSuccessRemove2,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessRemove: function (response) {
                        var title = "";
                        var body = "";

                        switch (response.d) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101245") %>';

                                empLeaveList.ReloadListData();
                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121006") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121007") %>';

                                break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    OnSuccessRemove2: function (response) {
                        var title = "";
                        var body = "";

                        switch (response.d) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101245") %>';

                                empLeaveList.ReloadListData2();
                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121006") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121007") %>';

                                break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ReloadListData: function(){
                        var dt = $('.empLeaveList #tableData').DataTable();
                        dt.draw();
                    },
                    ReloadListData2: function(){
                        var dt = $('.empLeaveList #tableData2').DataTable();
                        dt.draw();
                    }
                }

                $(document).ready(function () {

                    // Searching, Pagination event 
                    // 1
                    $('.empLeaveList #tableData #sltPageSize').change(function () {

                        empLeaveList.PageSize = parseInt($(".empLeaveList #tableData #sltPageSize").children("option:selected").val());
                        empLeaveList.PageIndex = 0;

                        empLeaveList.ReloadListData();

                        return false;
                    });

                    $('.empLeaveList #tableData #sltPageIndex').change(function () {

                        empLeaveList.PageIndex = $(".empLeaveList #tableData #sltPageIndex").children("option:selected").val();

                        empLeaveList.ReloadListData();

                        return false;
                    });

                    $('.empLeaveList #tableData #aPrevious').click(function () {

                        if (empLeaveList.PageIndex > 0) {
                            empLeaveList.PageIndex--;
                            empLeaveList.ReloadListData();
                        }

                        return false;
                    });
                    $('.empLeaveList #tableData #aNext').click(function () {

                        if (empLeaveList.PageIndex < (empLeaveList.PageCount - 1)) {
                            empLeaveList.PageIndex++;
                            empLeaveList.ReloadListData();
                        }

                        return false;
                    });

                    // 2
                    $('.empLeaveList #tableData2 #sltPageSize2').change(function () {

                        empLeaveList.PageSize2 = parseInt($(".empLeaveList #tableData2 #sltPageSize2").children("option:selected").val());
                        empLeaveList.PageIndex2 = 0;

                        empLeaveList.ReloadListData2();

                        return false;
                    });

                    $('.empLeaveList #tableData2 #sltPageIndex2').change(function () {

                        empLeaveList.PageIndex2 = $(".empLeaveList #tableData2 #sltPageIndex2").children("option:selected").val();

                        empLeaveList.ReloadListData2();

                        return false;
                    });

                    $('.empLeaveList #tableData2 #aPrevious2').click(function () {

                        if (empLeaveList.PageIndex2 > 0) {
                            empLeaveList.PageIndex2--;
                            empLeaveList.ReloadListData2();
                        }

                        return false;
                    });
                    $('.empLeaveList #tableData2 #aNext2').click(function () {

                        if (empLeaveList.PageIndex2 < (empLeaveList.PageCount2 - 1)) {
                            empLeaveList.PageIndex2++;
                            empLeaveList.ReloadListData2();
                        }

                        return false;
                    });

                    // Datatable Section
                    empLeaveList.LoadListData(<%=Request.QueryString["eid"]%>);
                    empLeaveList.LoadListData2(<%=Request.QueryString["eid"]%>);

                    // Modal Section
                    $('#modalNotifyConfirmRemove').on('show.bs.modal', function (e) {
                        $title = $(e.relatedTarget).attr('data-title');
                        $(this).find('.modal-title').text($title);
                        $message = $(e.relatedTarget).attr('data-message');
                        $(this).find('.modal-body p').text($message);
                        $id = $(e.relatedTarget).attr('id');
                        $(this).find('.modal-footer #modalConfirmRemove').attr('id', $id);
                        $tableNo = $(e.relatedTarget).attr('tableNo');
                        $(this).find('.modal-footer #modalConfirmRemove').attr('tableNo', $tableNo);
                    });
                    $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').on('click', function () {

                        $('#modalNotifyConfirmRemove').modal('hide');

                        $("#modalWaitDialog").modal('show');

                        // Remove command
                        if($(this).attr('tableNo') == 1){
                            empLeaveList.RemoveItem(<%=Request.QueryString["eid"]%>, $(this).attr('id'));
                        }
                        else{
                            empLeaveList.RemoveItem2(<%=Request.QueryString["eid"]%>, $(this).attr('id'));
                        }

                    });

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="empLeaveForm" style="padding: 15px;">
                <form class="form-padding">
                    <div class="row div-row-padding">
                        <div class="col-md-1 mb-1">
                        </div>
                        <div class="col-md-2 mb-2">
                            <label for="sltYear"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %></label>
                            <select id="sltYear" name="sltYear[]" class="form-control" required>
                            </select>
                        </div>
                        <div class="col-md-2 mb-2">
                            <label for="iptLate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131136") %></label>
                            <input type="text" class="form-control" id="iptLate"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131136") %>" required>
                        </div>
                        <div class="col-md-2 mb-2">
                            <label for="iptSick"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></label>
                            <input type="text" class="form-control" id="iptSick"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %>" required>
                        </div>
                        <div class="col-md-2 mb-2">
                            <label for="iptErrand"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></label>
                            <input type="text" class="form-control" id="iptErrand"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %>" required>
                        </div>
                        <div class="col-md-2 mb-2">
                            <label for="iptLacking"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></label>
                            <input type="text" class="form-control" id="iptLacking"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>" required>
                        </div>
                        <div class="col-md-1 mb-1">
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-1 mb-1">
                        </div>
                        <div class="col-md-2 mb-2">
                            <label for="iptToGovernor"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01108") %></label>
                            <input type="text" class="form-control" id="iptToGovernor"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01108") %>" required>
                        </div>
                        <div class="col-md-2 mb-2">
                            <label for="iptHoliday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01850") %></label>
                            <input type="text" class="form-control" id="iptHoliday"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01850") %>" required>
                        </div>
                        <div class="col-md-2 mb-2">
                            <label for="iptMaternity"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121020") %></label>
                            <input type="text" class="form-control" id="iptMaternity"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121020") %>" required>
                        </div>
                        <div class="col-md-2 mb-2">
                            <label for="iptOrdain"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121021") %></label>
                            <input type="text" class="form-control" id="iptOrdain"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121021") %>" required>
                        </div>
                        <div class="col-md-2 mb-2">
                            <label for="iptContinueStudy"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121022") %></label>
                            <input type="text" class="form-control" id="iptContinueStudy"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121022") %>" required>
                        </div>
                        <div class="col-md-1 mb-1">
                        </div>
                    </div>
                    <div class="row text-right">
                        <button id="save" type="submit" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="cancel" type="button"
                            class="btn btn-default" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var empLeaveForm = {
                    GetItem: function (empID, id) {
                        $.ajax({
                            type: "POST",
                            url: "EmpLeave.aspx/GetItem",
                            data: '{empID: ' + empID + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empLeaveForm.OnSuccessGet,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessGet: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {

                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                $("#sltYear").val($(this).find("F1").text());
                                $("#iptLate").val($(this).find("F2").text());
                                $("#iptSick").val($(this).find("F3").text());
                                $("#iptErrand").val($(this).find("F4").text());
                                $("#iptLacking").val($(this).find("F5").text());
                                $("#iptToGovernor").val($(this).find("F6").text());
                                $("#iptHoliday").val($(this).find("F7").text());
                                $("#iptMaternity").val($(this).find("F8").text());
                                $("#iptOrdain").val($(this).find("F9").text());
                                $("#iptContinueStudy").val($(this).find("F10").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "EmpLeave.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empLeaveForm.OnSuccessSave,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSave: function (response) {
                        var title = "";
                        var body = "";

                        switch (response.d) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').on('click', function () {

                                    // Close modal
                                    empLeaveForm.ClearSession(function () {
                                        modalForm.hideForm();

                                        empLeaveList.ReloadListData();
                                    });

                                });

                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111026") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %>';

                                break;
                            default: break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ClearSession: function (callbackRedirect) {
                        $.ajax({
                            type: "POST",
                            url: "EmpLeave.aspx/ClearSessionID",
                            data: '{}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                callbackRedirect();
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
                    }
                }

                $(document).ready(function () {

                    $(".empLeaveForm #save").bind({
                        click: function () {

                            $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                            $('#modalNotifyConfirmSave').modal('show');

                            return false;
                        }
                    });

                    $(".empLeaveForm #cancel").bind({
                        click: function () {

                            // Close modal
                            empLeaveForm.ClearSession(function () {
                                modalForm.hideForm();
                            });

                            return false;
                        }
                    });

                    // Modal Section
                    $('#modalNotifyConfirmSave').on('show.bs.modal', function (e) {
                        $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                        $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
                    });
                    $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').on('click', function () {
                        $('#modalNotifyConfirmSave').modal('hide');

                        $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalWaitDialog").modal('show');

                        // Save command
                        var data = new Array();
                        data[0] = "0";
                        data[1] = $("#sltYear").children("option:selected").val();
                        data[2] = $("#iptLate").val();
                        data[3] = $("#iptSick").val();
                        data[4] = $("#iptErrand").val();
                        data[5] = $("#iptLacking").val();
                        data[6] = $("#iptToGovernor").val();
                        data[7] = $("#iptHoliday").val();
                        data[8] = $("#iptMaternity").val();
                        data[9] = $("#iptOrdain").val();
                        data[10] = $("#iptContinueStudy").val();

                        empLeaveForm.SaveItem(data);

                    });

                    // Initial data
                    initFunction.setDropdown('#sltYear', 'Year', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206144") %></option>');


                    // Load info command
                    empLeaveForm.GetItem(<%=Request.QueryString["eid"]%>, <%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            View Content
        </asp:View>
    </asp:MultiView>
</body>
</html>
