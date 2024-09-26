<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpTOEIC.aspx.cs" Inherits="FingerprintPayment.Employees.EmpTOEIC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <div class="empTOEICList">
                <p class="bg-primary">TOEIC</p>
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
                                            <th style="width: 25%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102087") %></th>
                                            <th style="width: 45%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102088") %></th>
                                            <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102089") %></th>
                                            <th style="width: 10%" class="text-center">
                                                <button type="button" id="btnEmpTOEIC" class="btn btn-success col-md-12" data-toggle="modal" data-target="#modalShowForm" form-name="EmpTOEIC.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102090") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></button></th>
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

                var empTOEICList = {
                    PageIndex: 0,
                    PageSize: 20,
                    PageCount: 0,
                    TotalRows: 0,
                    dt: null,
                    LoadListData: function (eid) {
                        empTOEICList.dt = $(".empTOEICList #tableData").DataTable({
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
                                "url": "Ashx/LoadEmpTOEICList.ashx",
                                "type": "POST",
                                "contentType": "application/json; charset=utf-8",
                                "data": function (d) {
                                    d.eid = eid;
                                    d.search = '';
                                    d.page = empTOEICList.PageIndex;
                                    d.length = empTOEICList.PageSize;

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
                                { "data": "TOEICScore", "orderable": true },
                                { "data": "InstitutionAnnouncement", "orderable": true },
                                { "data": "ExpirationDate", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[5, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 3, 4] },
                                { "targets": [5], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="EmpTOEIC.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102090") %>" style="padding-right: 5px; font-size: 22px;"><i class="fa fa-edit"></i></a>' +
                                            '<a href="#" class="remove-row" data-title="Confirm Dialog" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101240") %>"><i class="fa fa-remove text-danger" style="font-size: 22px;"></i></a>';
                                    },
                                    "targets": 4
                                }
                            ],
                            "drawCallback": function (settings) {
                                var json = settings.json;
                                //console.log(json);

                                empTOEICList.PageCount = json.pageCount;
                                empTOEICList.TotalRows = json.recordsTotal;

                                var pageLRSize = 3;
                                var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                                var previousDot = '';
                                var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                                var nextDot = '';
                                var elements = '';

                                if (empTOEICList.PageIndex - pageLRSize > 1) {
                                    previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                                }

                                if (empTOEICList.PageIndex + pageLRSize < json.pageCount - 1) {
                                    nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                                }

                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    if (pi == 0) {
                                        elements += '<li class="paginate_button page-item ' + (empTOEICList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                        elements += previousDot;
                                    }
                                    else if (pi == json.pageCount - 1) {
                                        elements += nextDot;
                                        elements += '<li class="paginate_button page-item ' + (empTOEICList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                    else if (empTOEICList.PageIndex - pageLRSize <= pi && empTOEICList.PageIndex + pageLRSize >= pi) {
                                        elements += '<li class="paginate_button page-item ' + (empTOEICList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                }

                                $('.empTOEICList .pagination').html(previous + elements + next);

                                $('.empTOEICList .dataTables_info').html('Showing ' + ((empTOEICList.PageIndex * empTOEICList.PageSize) + 1) + ' to ' + ((empTOEICList.PageIndex * empTOEICList.PageSize) + empTOEICList.PageSize) + ' of ' + empTOEICList.TotalRows + ' rows');
                            }
                        });
                        // order.dt search.dt
                        empTOEICList.dt.on('draw.dt', function () {
                            empTOEICList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (empTOEICList.PageIndex * empTOEICList.PageSize) + (i + 1) + '.';
                            });
                            empTOEICList.dt.column(4, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = empTOEICList.dt.cells({ row: i, column: 5 }).data()[0];
                                $(cell).find(".fa-edit").parent().attr("pid", id);
                                $(cell).find(".fa-remove").parent().attr("pid", id);
                            });
                        });
                    },
                    RemoveItem: function (eid, pid) {
                        $.ajax({
                            type: "POST",
                            url: "EmpTOEIC.aspx/RemoveItem",
                            data: '{eid: ' + eid + ', id: ' + pid + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empTOEICList.OnSuccessRemove,
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

                                empTOEICList.ReloadListData();
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
                        empTOEICList.dt.draw();
                    }
                }

                $(document).ready(function () {

                    // Searching, Pagination event 
                    $('.empTOEICList #datatables_length').change(function () {

                        empTOEICList.PageSize = parseInt($(".empTOEICList #datatables_length").children("option:selected").val());
                        empTOEICList.PageIndex = 0;

                        empTOEICList.ReloadListData();

                        return false;
                    });

                    $('.empTOEICList ul.pagination').on('click', 'li.paginate_button a', function () {

                        var pi = parseInt($(this).attr("data-dt-idx"));

                        if (pi == 100) {
                            if (empTOEICList.PageIndex > 0) {
                                empTOEICList.PageIndex--;
                                empTOEICList.ReloadListData();

                                $('.empTOEICList .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empTOEICList .pagination .paginate_button.page-item a[data-dt-idx=' + empTOEICList.PageIndex + ']').addClass('active');
                            }
                        }
                        else if (pi == 101) {
                            if (empTOEICList.PageIndex < (empTOEICList.PageCount - 1)) {
                                empTOEICList.PageIndex++;
                                empTOEICList.ReloadListData();

                                $('.empTOEICList .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empTOEICList .pagination .paginate_button.page-item a[data-dt-idx=' + empTOEICList.PageIndex + ']').addClass('active');
                            }
                        }
                        else {
                            empTOEICList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                            empTOEICList.ReloadListData();

                            $('.empTOEICList .pagination .paginate_button.page-item.active').removeClass('active');
                            $(this).addClass('active');
                        }

                        return false;
                    });

                    $('.empTOEICList #tableData #datatables_previous').click(function () {

                        if (empTOEICList.PageIndex > 0) {
                            empTOEICList.PageIndex--;
                            empTOEICList.ReloadListData();
                        }

                        return false;
                    });

                    $('.empTOEICList #tableData #datatables_next').click(function () {

                        if (empTOEICList.PageIndex < (empTOEICList.PageCount - 1)) {
                            empTOEICList.PageIndex++;
                            empTOEICList.ReloadListData();
                        }

                        return false;
                    });

                    $('.empTOEICList #btnEmpTOEIC').click(function () {

                        $('#modalShowForm').find('.modal-dialog').addClass('modal-lg');

                        //return false;
                    });

                    $('.empTOEICList #tableData').on('click', '.remove-row', function () {

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
                            empTOEICList.RemoveItem(<%=Request.QueryString["eid"]%>, $(this).attr('pid'));

                        });

                        $("#modalNotifyConfirmRemove").modal('show');

                    });

                    // Datatable Section
                    empTOEICList.LoadListData(<%=Request.QueryString["eid"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="empTOEICForm popup-form" style="padding: 15px;">
                <form id="empTOEICForm" class="form-padding">
                    <div class="row div-row-padding justify-content-md-center">
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptTOEICScore"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102087") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptTOEICScore" name="iptTOEICScore"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102087") %>" maxlength="4" />
                        </div>
                    </div>
                    <div class="row div-row-padding justify-content-md-center">
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptInstitutionAnnouncement"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102088") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptInstitutionAnnouncement" name="iptInstitutionAnnouncement"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102088") %>" maxlength="300" />
                        </div>
                    </div>
                    <div class="row div-row-padding justify-content-md-center">
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptExpirationDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102089") %> :</label></div>
                        <div class="col-md-8 mb-8">
                            <div class="form-group div-datepicker">
                                <input id="iptExpirationDate" name="iptExpirationDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
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

                var empTOEICForm = {
                    GetItem: function (empID, id) {
                        $.ajax({
                            type: "POST",
                            url: "EmpTOEIC.aspx/GetItem",
                            data: '{empID: ' + empID + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empTOEICForm.OnSuccessGet,
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

                                $(".empTOEICForm #iptTOEICScore").val($(this).find("F1").text());
                                $(".empTOEICForm #iptInstitutionAnnouncement").val($(this).find("F2").text());
                                $(".empTOEICForm #iptExpirationDate").val($(this).find("F3").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "EmpTOEIC.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empTOEICForm.OnSuccessSave,
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
                                    empTOEICForm.ClearSession(function () {
                                        modalForm.hideForm();

                                        empTOEICList.ReloadListData();
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
                            url: "EmpTOEIC.aspx/ClearSessionID",
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

                    $("#empTOEICForm").validate({
                        rules: {
                            iptTOEICScore: {
                                required: true,
                                number: true
                            },
                            iptInstitutionAnnouncement: "required",
                            iptExpirationDate: {
                                required: true,
                                thaiDate: true
                            }
                        },
                        messages: {
                            iptTOEICScore: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            },
                            iptInstitutionAnnouncement: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptExpirationDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            }
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptTOEICScore":
                                case "iptInstitutionAnnouncement":
                                case "iptExpirationDate": error.insertAfter(element); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    $(".empTOEICForm #save").bind({
                        click: function () {

                            if ($('#empTOEICForm').valid()) {

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
                                    data[1] = $(".empTOEICForm #iptTOEICScore").val();
                                    data[2] = $(".empTOEICForm #iptInstitutionAnnouncement").val();
                                    data[3] = $(".empTOEICForm #iptExpirationDate").val();

                                    empTOEICForm.SaveItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".empTOEICForm #cancel").bind({
                        click: function () {

                            // Close modal
                            empTOEICForm.ClearSession(function () {
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
                    $('.empTOEICForm .datepicker').datetimepicker({
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

                    $(".empTOEICForm .datepicker").attr('maxlength', '10');

                    // Load info command
                    xEmpKey.pid = "<%=Request.QueryString["id"]%>";
                    empTOEICForm.GetItem(<%=Request.QueryString["eid"]%>, <%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            View Content
        </asp:View>
    </asp:MultiView>
</body>
</html>
