<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpTraining.aspx.cs" Inherits="FingerprintPayment.Employees.EmpTraining" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <div class="empTrainingList">
                <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102071") %></p>
                <div class="material-datatables data-table">
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
                                            <th style="width: 30%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102072") %></th>
                                            <th style="width: 35%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102073") %></th>
                                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102074") %></th>
                                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102075") %></th>
                                            <th style="width: 10%" class="text-center">
                                                <button type="button" id="btnEmpTraining" class="btn btn-success col-md-12" data-toggle="modal" data-target="#modalShowForm" form-name="EmpTraining.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102076") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></button></th>
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
            <script type="text/javascript">

                var empTrainingList = {
                    PageIndex: 0,
                    PageSize: 20,
                    PageCount: 0,
                    TotalRows: 0,
                    dt: null,
                    LoadListData: function (eid) {
                        empTrainingList.dt = $(".empTrainingList #tableData").DataTable({
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
                                "url": "Ashx/LoadEmpTrainingList.ashx",
                                "type": "POST",
                                "contentType": "application/json; charset=utf-8",
                                "data": function (d) {
                                    d.eid = eid;
                                    d.search = '';
                                    d.page = empTrainingList.PageIndex;
                                    d.length = empTrainingList.PageSize;

                                    return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                                },
                                "dataSrc": function (json) {
                                    //console.log(json.data);
                                    return json.data;
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
                                { "data": "ProjectName", "orderable": true },
                                { "data": "TrainingName", "orderable": true },
                                { "data": "StartDate", "orderable": true },
                                { "data": "EndDate", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[6, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 3, 4, 5] },
                                { "targets": [6], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="EmpTraining.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102076") %>" style="padding-right: 5px; font-size: 22px;"><i class="fa fa-edit"></i></a>' +
                                            '<a href="#" class="remove-row" data-title="Confirm Dialog" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101240") %>"><i class="fa fa-remove text-danger" style="font-size: 22px;"></i></a>';
                                    },
                                    "targets": 5
                                }
                            ],
                            "drawCallback": function (settings) {
                                var json = settings.json;
                                //console.log(json);

                                empTrainingList.PageCount = json.pageCount;
                                empTrainingList.TotalRows = json.recordsTotal;

                                var pageLRSize = 3;
                                var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                                var previousDot = '';
                                var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                                var nextDot = '';
                                var elements = '';

                                if (empTrainingList.PageIndex - pageLRSize > 1) {
                                    previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                                }

                                if (empTrainingList.PageIndex + pageLRSize < json.pageCount - 1) {
                                    nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                                }

                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    if (pi == 0) {
                                        elements += '<li class="paginate_button page-item ' + (empTrainingList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                        elements += previousDot;
                                    }
                                    else if (pi == json.pageCount - 1) {
                                        elements += nextDot;
                                        elements += '<li class="paginate_button page-item ' + (empTrainingList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                    else if (empTrainingList.PageIndex - pageLRSize <= pi && empTrainingList.PageIndex + pageLRSize >= pi) {
                                        elements += '<li class="paginate_button page-item ' + (empTrainingList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                }

                                $('.empTrainingList .pagination').html(previous + elements + next);

                                $('.empTrainingList .dataTables_info').html('Showing ' + ((empTrainingList.PageIndex * empTrainingList.PageSize) + 1) + ' to ' + ((empTrainingList.PageIndex * empTrainingList.PageSize) + empTrainingList.PageSize) + ' of ' + empTrainingList.TotalRows + ' rows');
                            }
                        });
                        // order.dt search.dt
                        empTrainingList.dt.on('draw.dt', function () {
                            empTrainingList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (empTrainingList.PageIndex * empTrainingList.PageSize) + (i + 1) + '.';
                            });
                            empTrainingList.dt.column(5, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = empTrainingList.dt.cells({ row: i, column: 6 }).data()[0];
                                $(cell).find(".fa-edit").parent().attr("pid", id);
                                $(cell).find(".fa-remove").parent().attr("pid", id);
                            });
                        });
                    },
                    RemoveItem: function (eid, pid) {
                        $.ajax({
                            type: "POST",
                            url: "EmpTraining.aspx/RemoveItem",
                            data: '{eid: ' + eid + ', id: ' + pid + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empTrainingList.OnSuccessRemove,
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

                                empTrainingList.ReloadListData();
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
                    ReloadListData: function () {
                        empTrainingList.dt.draw();
                    }
                }

                $(document).ready(function () {

                    // Searching, Pagination event 
                    $('.empTrainingList #datatables_length').change(function () {

                        empTrainingList.PageSize = parseInt($(".empTrainingList #datatables_length").children("option:selected").val());
                        empTrainingList.PageIndex = 0;

                        empTrainingList.ReloadListData();

                        return false;
                    });

                    $('.empTrainingList ul.pagination').on('click', 'li.paginate_button a', function () {

                        var pi = parseInt($(this).attr("data-dt-idx"));

                        if (pi == 100) {
                            if (empTrainingList.PageIndex > 0) {
                                empTrainingList.PageIndex--;
                                empTrainingList.ReloadListData();

                                $('.empTrainingList .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empTrainingList .pagination .paginate_button.page-item a[data-dt-idx=' + empTrainingList.PageIndex + ']').addClass('active');
                            }
                        }
                        else if (pi == 101) {
                            if (empTrainingList.PageIndex < (empTrainingList.PageCount - 1)) {
                                empTrainingList.PageIndex++;
                                empTrainingList.ReloadListData();

                                $('.empTrainingList .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empTrainingList .pagination .paginate_button.page-item a[data-dt-idx=' + empTrainingList.PageIndex + ']').addClass('active');
                            }
                        }
                        else {
                            empTrainingList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                            empTrainingList.ReloadListData();

                            $('.empTrainingList .pagination .paginate_button.page-item.active').removeClass('active');
                            $(this).addClass('active');
                        }

                        return false;
                    });

                    $('.empTrainingList #tableData #datatables_previous').click(function () {

                        if (empTrainingList.PageIndex > 0) {
                            empTrainingList.PageIndex--;
                            empTrainingList.ReloadListData();
                        }

                        return false;
                    });

                    $('.empTrainingList #tableData #datatables_next').click(function () {

                        if (empTrainingList.PageIndex < (empTrainingList.PageCount - 1)) {
                            empTrainingList.PageIndex++;
                            empTrainingList.ReloadListData();
                        }

                        return false;
                    });


                    $('.empTrainingList #tableData').on('click', '.remove-row', function () {

                        var $title = $(this).attr('data-title');
                        var $message = $(this).attr('data-message');
                        var $pid = $(this).attr('pid');

                        // Modal Section
                        $('#modalNotifyConfirmRemove').off().on('show.bs.modal', function (e) {
                            $(this).find('.modal-title').text($title);
                            $(this).find('.modal-body p').text($message);
                            $(this).find('.modal-footer #modalConfirmRemove').attr('pid', $pid);
                        });
                        $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').off().on('click', function () {

                            $('#modalNotifyConfirmRemove').modal('hide');

                            $("#modalWaitDialog").modal('show');

                            // Remove command
                            empTrainingList.RemoveItem(<%=Request.QueryString["eid"]%>, $(this).attr('pid'));

                        });

                        $("#modalNotifyConfirmRemove").modal('show');

                    });

                    // Datatable Section
                    empTrainingList.LoadListData(<%=Request.QueryString["eid"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="empTrainingForm popup-form" style="padding: 15px;">
                <form id="empTrainingForm" class="form-padding">
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="sltTraininType"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %> :</label></div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltTraininType" name="sltTraininType" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106160") %>">
                                <option selected="selected" value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102077") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102078") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102079") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102080") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptProjectName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102072") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptProjectName" name="iptProjectName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102072") %>" maxlength="100" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptTrainingName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102073") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptTrainingName" name="iptTrainingName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102073") %>" maxlength="100" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptStartDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102081") %> :</label></div>
                        <div class="col-md-8 mb-8">
                            <div class="form-group div-datepicker">
                                <input id="iptStartDate" name="iptStartDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptEndDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102082") %> :</label></div>
                        <div class="col-md-8 mb-8">
                            <div class="form-group div-datepicker">
                                <input id="iptEndDate" name="iptEndDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptTrainingHours"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102083") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptTrainingHours" name="iptTrainingHours"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102083") %>" maxlength="8" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptPlace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102084") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptPlace" name="iptPlace"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102084") %>" maxlength="100" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptCountry"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132124") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptCountry" name="iptCountry"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102085") %>" maxlength="100" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptProvince" name="iptProvince"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>" maxlength="100" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptExpenses"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102086") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptExpenses" name="iptExpenses"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102086") %>" maxlength="4" />
                        </div>
                    </div>

                    <div class="row text-center" style="display: block; padding-top: 15px;">
                        <button id="save" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="cancel" type="button"
                            class="btn btn-danger" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var empTrainingForm = {
                    GetItem: function (empID, id) {
                        $.ajax({
                            type: "POST",
                            url: "EmpTraining.aspx/GetItem",
                            data: '{empID: ' + empID + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empTrainingForm.OnSuccessGet,
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

                                $(".empTrainingForm #iptProjectName").val($(this).find("F1").text());
                                $(".empTrainingForm #iptTrainingName").val($(this).find("F2").text());
                                $(".empTrainingForm #iptStartDate").val($(this).find("F3").text());
                                $(".empTrainingForm #iptEndDate").val($(this).find("F4").text());
                                $(".empTrainingForm #iptPlace").val($(this).find("F5").text());
                                $(".empTrainingForm #iptProvince").val($(this).find("F6").text());
                                $(".empTrainingForm #iptCountry").val($(this).find("F7").text());
                                $(".empTrainingForm #iptExpenses").val($(this).find("F8").text());
                                $(".empTrainingForm #sltTraininType").selectpicker('val', $(this).find("F9").text());
                                $(".empTrainingForm #iptTrainingHours").val($(this).find("F10").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "EmpTraining.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empTrainingForm.OnSuccessSave,
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

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

                                    // Close modal
                                    empTrainingForm.ClearSession(function () {
                                        modalForm.hideForm();

                                        empTrainingList.ReloadListData();
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
                            url: "EmpTraining.aspx/ClearSessionID",
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

                    $("#empTrainingForm").validate({
                        rules: {
                            iptProjectName: "required",
                            iptTrainingName: "required",
                            iptStartDate: {
                                required: true,
                                thaiDate: true
                            },
                            iptEndDate: {
                                required: true,
                                thaiDate: true
                            },
                            iptTrainingHours: {
                                required: true,
                                number: true
                            },
                            iptPlace: "required",
                            iptCountry: "required",
                            iptProvince: "required",
                            iptExpenses: {
                                required: true,
                                number: true
                            }
                        },
                        messages: {
                            iptProjectName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptTrainingName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptStartDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptEndDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptTrainingHours: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            },
                            iptPlace: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptCountry: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptProvince: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptExpenses: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            }
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptProjectName":
                                case "iptTrainingName":
                                case "iptStartDate":
                                case "iptEndDate":
                                case "iptTrainingHours":
                                case "iptPlace":
                                case "iptCountry":
                                case "iptProvince":
                                case "iptExpenses": error.insertAfter(element); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    $(".empTrainingForm #save").bind({
                        click: function () {

                            if ($('#empTrainingForm').valid()) {

                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                //$('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off('click');
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = new Array();
                                    data[0] = xEmpKey.eid + "-" + xEmpKey.pid;
                                    data[1] = $(".empTrainingForm #iptProjectName").val();
                                    data[2] = $(".empTrainingForm #iptTrainingName").val();
                                    data[3] = $(".empTrainingForm #iptStartDate").val();
                                    data[4] = $(".empTrainingForm #iptEndDate").val();
                                    data[5] = $(".empTrainingForm #iptPlace").val();
                                    data[6] = $(".empTrainingForm #iptProvince").val();
                                    data[7] = $(".empTrainingForm #iptCountry").val();
                                    data[8] = $(".empTrainingForm #iptExpenses").val();
                                    data[9] = $(".empTrainingForm #sltTraininType").val();
                                    data[10] = $(".empTrainingForm #iptTrainingHours").val();

                                    empTrainingForm.SaveItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".empTrainingForm #cancel").bind({
                        click: function () {

                            // Close modal
                            empTrainingForm.ClearSession(function () {
                                modalForm.hideForm();
                            });

                            return false;
                        }
                    });

                    // Modal Section
                    $('#modalNotifyConfirmSave').off().on('show.bs.modal', function (e) {
                        $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                        $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
                    });

                    // Initial data

                    //$('.empTrainingForm #divStartDate, .empTrainingForm #divEndDate').datetimepicker({
                    //    format: 'DD/MM/YYYY-BE',
                    //    locale: 'th'
                    //});
                    $('.empTrainingForm .datepicker').datetimepicker({
                        keepOpen: false,
                        debug: false,
                        format: 'DD/MM/YYYY-BE',
                        locale: 'th',
                        icons: {
                            time: "fa fa-clock-o",
                            date: "fa fa-calendar",
                            up: "fa fa-chevron-up",
                            down: "fa fa-chevron-down",
                            previous: 'fa fa-chevron-left',
                            next: 'fa fa-chevron-right',
                            today: 'fa fa-screenshot',
                            clear: 'fa fa-trash',
                            close: 'fa fa-remove'
                        }
                    });

                    $(".empTrainingForm .datepicker").attr('maxlength', '10');

                    activateBootstrapSelect('.empTrainingForm .selectpicker');


                    // Load info command
                    xEmpKey.pid = "<%=Request.QueryString["id"]%>";
                    empTrainingForm.GetItem(<%=Request.QueryString["eid"]%>, <%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            View Content
        </asp:View>
    </asp:MultiView>
</body>
</html>
