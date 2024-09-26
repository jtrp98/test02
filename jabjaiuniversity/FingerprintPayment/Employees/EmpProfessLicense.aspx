<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpProfessLicense.aspx.cs" Inherits="FingerprintPayment.Employees.EmpProfessLicense" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <div class="empProfessLicenseList">
                <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102114") %></p>
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
                                            <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102115") %></th>
                                            <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102116") %></th>
                                            <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102117") %></th>
                                            <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102118") %></th>
                                            <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102119") %></th>
                                            <th style="width: 10%" class="text-center">
                                                <button type="button" class="btn btn-success col-md-12" data-toggle="modal" data-target="#modalShowForm" form-name="EmpProfessLicense.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121084") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></button></th>
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

                var empProfessLicenseList = {
                    PageIndex: 0,
                    PageSize: 20,
                    PageCount: 0,
                    TotalRows: 0,
                    dt: null,
                    LoadListData: function (eid) {
                        empProfessLicenseList.dt = $(".empProfessLicenseList #tableData").DataTable({
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
                                "url": "Ashx/LoadEmpProfessLicenseList.ashx",
                                "type": "POST",
                                "contentType": "application/json; charset=utf-8",
                                "data": function (d) {
                                    d.eid = eid;
                                    d.search = '';
                                    d.page = empProfessLicenseList.PageIndex;
                                    d.length = empProfessLicenseList.PageSize;

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
                                { "data": "LicenseType", "orderable": true },
                                { "data": "LicenseNo", "orderable": true },
                                { "data": "LicenseName", "orderable": true },
                                { "data": "IssuedDate", "orderable": true },
                                { "data": "ExpireDate", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[7, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6] },
                                { "targets": [7], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="EmpProfessLicense.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102120") %>" style="padding-right: 5px; font-size: 22px;"><i class="fa fa-edit"></i></a>' +
                                            '<a href="#" class="remove-row" data-title="Confirm Dialog" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101240") %>"><i class="fa fa-remove text-danger" style="font-size: 22px;"></i></a>';
                                    },
                                    "targets": 6
                                }
                            ],
                            "drawCallback": function (settings) {
                                var json = settings.json;
                                //console.log(json);

                                empProfessLicenseList.PageCount = json.pageCount;
                                empProfessLicenseList.TotalRows = json.recordsTotal;

                                var pageLRSize = 3;
                                var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                                var previousDot = '';
                                var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                                var nextDot = '';
                                var elements = '';

                                if (empProfessLicenseList.PageIndex - pageLRSize > 1) {
                                    previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                                }

                                if (empProfessLicenseList.PageIndex + pageLRSize < json.pageCount - 1) {
                                    nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                                }

                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    if (pi == 0) {
                                        elements += '<li class="paginate_button page-item ' + (empProfessLicenseList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                        elements += previousDot;
                                    }
                                    else if (pi == json.pageCount - 1) {
                                        elements += nextDot;
                                        elements += '<li class="paginate_button page-item ' + (empProfessLicenseList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                    else if (empProfessLicenseList.PageIndex - pageLRSize <= pi && empProfessLicenseList.PageIndex + pageLRSize >= pi) {
                                        elements += '<li class="paginate_button page-item ' + (empProfessLicenseList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                }

                                $('.empProfessLicenseList .pagination').html(previous + elements + next);

                                $('.empProfessLicenseList .dataTables_info').html('Showing ' + ((empProfessLicenseList.PageIndex * empProfessLicenseList.PageSize) + 1) + ' to ' + ((empProfessLicenseList.PageIndex * empProfessLicenseList.PageSize) + empProfessLicenseList.PageSize) + ' of ' + empProfessLicenseList.TotalRows + ' rows');
                            }
                        });
                        // order.dt search.dt
                        empProfessLicenseList.dt.on('draw.dt', function () {
                            empProfessLicenseList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (empProfessLicenseList.PageIndex * empProfessLicenseList.PageSize) + (i + 1) + '.';
                            });
                            empProfessLicenseList.dt.column(6, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = empProfessLicenseList.dt.cells({ row: i, column: 7 }).data()[0];
                                $(cell).find(".fa-edit").parent().attr("pid", id);
                                $(cell).find(".fa-remove").parent().attr("pid", id);
                            });
                        });
                    },
                    RemoveItem: function (eid, pid) {
                        $.ajax({
                            type: "POST",
                            url: "EmpProfessLicense.aspx/RemoveItem",
                            data: '{eid: ' + eid + ', id: ' + pid + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empProfessLicenseList.OnSuccessRemove,
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

                                empProfessLicenseList.ReloadListData();
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
                        empProfessLicenseList.dt.draw();
                    }
                }

                $(document).ready(function () {

                    // Searching, Pagination event 
                    $('.empProfessLicenseList #datatables_length').change(function () {

                        empProfessLicenseList.PageSize = parseInt($(".empProfessLicenseList #datatables_length").children("option:selected").val());
                        empProfessLicenseList.PageIndex = 0;

                        empProfessLicenseList.ReloadListData();

                        return false;
                    });

                    $('.empProfessLicenseList ul.pagination').on('click', 'li.paginate_button a', function () {

                        var pi = parseInt($(this).attr("data-dt-idx"));

                        if (pi == 100) {
                            if (empProfessLicenseList.PageIndex > 0) {
                                empProfessLicenseList.PageIndex--;
                                empProfessLicenseList.ReloadListData();

                                $('.empProfessLicenseList .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empProfessLicenseList .pagination .paginate_button.page-item a[data-dt-idx=' + empProfessLicenseList.PageIndex + ']').addClass('active');
                            }
                        }
                        else if (pi == 101) {
                            if (empProfessLicenseList.PageIndex < (empProfessLicenseList.PageCount - 1)) {
                                empProfessLicenseList.PageIndex++;
                                empProfessLicenseList.ReloadListData();

                                $('.empProfessLicenseList .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empProfessLicenseList .pagination .paginate_button.page-item a[data-dt-idx=' + empProfessLicenseList.PageIndex + ']').addClass('active');
                            }
                        }
                        else {
                            empProfessLicenseList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                            empProfessLicenseList.ReloadListData();

                            $('.empProfessLicenseList .pagination .paginate_button.page-item.active').removeClass('active');
                            $(this).addClass('active');
                        }

                        return false;
                    });

                    $('.empProfessLicenseList #tableData #datatables_previous').click(function () {

                        if (empProfessLicenseList.PageIndex > 0) {
                            empProfessLicenseList.PageIndex--;
                            empProfessLicenseList.ReloadListData();
                        }

                        return false;
                    });

                    $('.empProfessLicenseList #tableData #datatables_next').click(function () {

                        if (empProfessLicenseList.PageIndex < (empProfessLicenseList.PageCount - 1)) {
                            empProfessLicenseList.PageIndex++;
                            empProfessLicenseList.ReloadListData();
                        }

                        return false;
                    });


                    $('.empProfessLicenseList #tableData').on('click', '.remove-row', function () {

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
                            empProfessLicenseList.RemoveItem(<%=Request.QueryString["eid"]%>, $(this).attr('pid'));

                        });

                        $("#modalNotifyConfirmRemove").modal('show');

                    });

                    // Datatable Section
                    empProfessLicenseList.LoadListData(<%=Request.QueryString["eid"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="empProfessLicenseForm popup-form" style="padding: 15px;">
                <form id="empProfessLicenseForm" class="form-padding">
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right" for="sltLicenseType"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102121") %> :</label></div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltLicenseType" name="sltLicenseType" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121087") %>">
                                <option selected="selected" value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210024") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121089") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121090") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121091") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptLicenseNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102122") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptLicenseNo" name="iptLicenseNo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102122") %>" maxlength="20" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptLicenseName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102123") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptLicenseName" name="iptLicenseName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102123") %>" maxlength="100" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptIssuedDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102124") %> :</label></div>
                        <div class="col-md-8 mb-8">
                            <div class="form-group div-datepicker">
                                <input id="iptIssuedDate" name="iptIssuedDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptExpireDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102125") %> :</label></div>
                        <div class="col-md-8 mb-8">
                            <div class="form-group div-datepicker">
                                <input id="iptExpireDate" name="iptExpireDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptAgencyIssued"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102126") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptAgencyIssued" name="iptAgencyIssued"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102126") %>" maxlength="100" />
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

                var empProfessLicenseForm = {
                    GetItem: function (empID, id) {
                        $.ajax({
                            type: "POST",
                            url: "EmpProfessLicense.aspx/GetItem",
                            data: '{empID: ' + empID + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empProfessLicenseForm.OnSuccessGet,
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

                                $(".empProfessLicenseForm #sltLicenseType").selectpicker('val', $(this).find("F1").text());
                                $(".empProfessLicenseForm #iptLicenseNo").val($(this).find("F2").text());
                                $(".empProfessLicenseForm #iptLicenseName").val($(this).find("F3").text());
                                $(".empProfessLicenseForm #iptIssuedDate").val($(this).find("F4").text());
                                $(".empProfessLicenseForm #iptExpireDate").val($(this).find("F5").text());
                                $(".empProfessLicenseForm #iptAgencyIssued").val($(this).find("F6").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "EmpProfessLicense.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empProfessLicenseForm.OnSuccessSave,
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
                                    empProfessLicenseForm.ClearSession(function () {
                                        modalForm.hideForm();

                                        empProfessLicenseList.ReloadListData();
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
                            url: "EmpProfessLicense.aspx/ClearSessionID",
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

                    $("#empProfessLicenseForm").validate({
                        rules: {
                            iptLicenseNo: "required",
                            iptLicenseName: "required",
                            iptIssuedDate: {
                                required: true,
                                thaiDate: true
                            },
                            iptExpireDate: {
                                required: true,
                                thaiDate: true
                            },
                            iptAgencyIssued: "required"
                        },
                        messages: {
                            iptLicenseNo: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptLicenseName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptIssuedDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptExpireDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptAgencyIssued: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptLicenseNo":
                                case "iptLicenseName":
                                case "iptIssuedDate":
                                case "iptExpireDate": 
                                case "iptAgencyIssued": error.insertAfter(element); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    $(".empProfessLicenseForm #save").bind({
                        click: function () {

                            if ($('#empProfessLicenseForm').valid()) {

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
                                    data[1] = $(".empProfessLicenseForm #sltLicenseType").val();
                                    data[2] = $(".empProfessLicenseForm #iptLicenseNo").val();
                                    data[3] = $(".empProfessLicenseForm #iptLicenseName").val();
                                    data[4] = $(".empProfessLicenseForm #iptIssuedDate").val();
                                    data[5] = $(".empProfessLicenseForm #iptExpireDate").val();
                                    data[6] = $(".empProfessLicenseForm #iptAgencyIssued").val();

                                    empProfessLicenseForm.SaveItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".empProfessLicenseForm #cancel").bind({
                        click: function () {

                            // Close modal
                            empProfessLicenseForm.ClearSession(function () {
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

                    //$('.empProfessLicenseForm #divIssuedDate, .empProfessLicenseForm #divExpireDate').datetimepicker({
                    //    format: 'DD/MM/YYYY-BE',
                    //    locale: 'th'
                    //});
                    $('.empProfessLicenseForm .datepicker').datetimepicker({
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

                    $(".empProfessLicenseForm .datepicker").attr('maxlength', '10');

                    activateBootstrapSelect('.empProfessLicenseForm .selectpicker');

                    // Load info command
                    xEmpKey.pid = "<%=Request.QueryString["id"]%>";
                    empProfessLicenseForm.GetItem(<%=Request.QueryString["eid"]%>, <%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            View Content
        </asp:View>
    </asp:MultiView>
</body>
</html>
