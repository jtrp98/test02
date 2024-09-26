<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterRecruitmentData.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterRecruitmentData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <p class="text-muted" style="font-size: small;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00686") %>
                    </p>
                </div>
            </div>

            <div class="registerList row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header card-header-warning card-header-icon">
                            <div class="card-icon">
                                <i class="material-icons">account_circle</i>
                            </div>
                            <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00686") %></h4>
                        </div>
                        <div class="card-body">
                            <div class="toolbar">
                                <!-- Here you can write extra buttons/actions for the toolbar -->
                            </div>
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
                                                        <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></th>
                                                        <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103201") %></th>
                                                        <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></th>
                                                        <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103202") %></th>
                                                        <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103199") %></th>
                                                        <th style="width: 15%" class="text-center">
                                                            <asp:Literal ID="ltrButtonAdd" runat="server"></asp:Literal>
                                                        </th>
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
                        <!-- end content-->
                    </div>
                    <!--  end card  -->
                </div>
                <!-- end col-md-12 -->
            </div>
            <!-- end row -->

            <script type="text/javascript">

                var registerList = {
                    PageIndex: 0,
                    PageSize: 100,
                    PageCount: 0,
                    TotalRows: 0,
                    dt: null,
                    LoadListData: function () {
                        registerList.dt = $(".registerList #tableData").DataTable({
                            "processing": true,
                            "serverSide": true,
                            "info": false,
                            "searching": false,
                            "paging": false,
                            "stateSave": true,
                            "ajax": {
                                "url": "RegisterRecruitmentData.aspx/LoadRegisterRecruitment",
                                "type": "POST",
                                "contentType": "application/json; charset=utf-8",
                                "data": function (d) {
                                    d.page = registerList.PageIndex;
                                    d.length = registerList.PageSize;

                                    return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                                },
                                "dataSrc": function (json) {
                                    var jsond = $.parseJSON(json.d);
                                    return jsond.data;
                                }
                            },
                            "columns": [
                                { "data": "no", "orderable": false },
                                { "data": "Year", "orderable": true },
                                { "data": "StudentType", "orderable": true },
                                { "data": "LevelName", "orderable": true },
                                { "data": "PlanName", "orderable": true },
                                { "data": "EndDate", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[7, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6] },
                                { "targets": [7], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<a href="#" class="fa fa-edit" data-toggle="modal" data-target="#modalShowForm" form-name="RegisterRecruitmentData.aspx" form-action="form" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00686") %>" style="text-decoration: none; vertical-align: middle; margin-right: 5px; margin-top: 5px; font-size: 22px;"></a>' +
                                            '<a href="#" class="fa fa-remove text-danger" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102158") %>" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103188") %>" style="text-decoration: none; vertical-align: middle; font-size: 22px;"></a>';
                                    },
                                    "targets": 6
                                }
                            ],
                            "drawCallback": function (settings) {
                                //var json = settings.json;
                                var json = $.parseJSON(settings.json.d);
                                //console.log(json);

                                registerList.PageCount = json.pageCount;
                                registerList.TotalRows = json.recordsTotal;

                                var pageLRSize = 3;
                                var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                                var previousDot = '';
                                var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                                var nextDot = '';
                                var elements = '';

                                if (registerList.PageIndex - pageLRSize > 1) {
                                    previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                                }

                                if (registerList.PageIndex + pageLRSize < json.pageCount - 1) {
                                    nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                                }

                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    if (pi == 0) {
                                        elements += '<li class="paginate_button page-item ' + (registerList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                        elements += previousDot;
                                    }
                                    else if (pi == json.pageCount - 1) {
                                        elements += nextDot;
                                        elements += '<li class="paginate_button page-item ' + (registerList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                    else if (registerList.PageIndex - pageLRSize <= pi && registerList.PageIndex + pageLRSize >= pi) {
                                        elements += '<li class="paginate_button page-item ' + (registerList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                }

                                $('.pagination').html(previous + elements + next);

                                $('.dataTables_info').html('Showing ' + ((registerList.PageIndex * registerList.PageSize) + 1) + ' to ' + ((registerList.PageIndex * registerList.PageSize) + registerList.PageSize) + ' of ' + registerList.TotalRows + ' rows');

                            }
                        });
                        // order.dt search.dt
                        registerList.dt.on('draw.dt', function () {
                            registerList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (registerList.PageIndex * registerList.PageSize) + (i + 1) + '.';
                            });
                            registerList.dt.column(6, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var rid = registerList.dt.cells({ row: i, column: 7 }).data()[0];
                                $(cell).find(".fa-edit").attr("rid", rid);
                                $(cell).find(".fa-remove").attr("rid", rid);
                            });
                        });
                    },
                    RemoveItem: function (id) {
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: "RegisterRecruitmentData.aspx/RemoveItem",
                            data: '{id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: registerList.OnSuccessRemove,
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

                                registerList.ReloadListData();
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
                        registerList.dt.draw();
                    }
                }

                $(document).ready(function () {

                    // Modal Section
                    $('#modalNotifyConfirmRemove').on('show.bs.modal', function (e) {
                        $title = $(e.relatedTarget).attr('data-title');
                        $(this).find('.modal-title').text($title);
                        $message = $(e.relatedTarget).attr('data-message');
                        $(this).find('.modal-body p').text($message);
                        $rid = $(e.relatedTarget).attr('rid');
                        $(this).find('.modal-footer #modalConfirmRemove').attr('rid', $rid);
                    });
                    $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').on('click', function () {

                        $('#modalNotifyConfirmRemove').modal('hide');

                        $("#modalWaitDialog").modal('show');

                        // Remove command
                        registerList.RemoveItem($(this).attr('rid'));

                    });

                    $('#modalWarning').on('show.bs.modal', function (e) {
                        $title = $(e.relatedTarget).attr('data-title');
                        $(this).find('.modal-title').text($title);
                        $message = $(e.relatedTarget).attr('data-message');
                        $(this).find('.modal-body p').text($message);
                    });


                    // Searching, Pagination event 
                    $('.registerList #btnSearch').click(function () {

                        registerList.PageIndex = 0;

                        registerList.ReloadListData();

                        return false;
                    });

                    $('.registerList #datatables_length').change(function () {

                        registerList.PageSize = parseInt($("#datatables_length").val());
                        registerList.PageIndex = 0;

                        registerList.ReloadListData();

                        return false;
                    });

                    $('.registerList ul.pagination').on('click', 'li.paginate_button a', function () {

                        var pi = parseInt($(this).attr("data-dt-idx"));

                        if (pi == 100) {
                            if (registerList.PageIndex > 0) {
                                registerList.PageIndex--;
                                registerList.ReloadListData();

                                $('.pagination .paginate_button.page-item.active').removeClass('active');
                                $('.pagination .paginate_button.page-item a[data-dt-idx=' + registerList.PageIndex + ']').addClass('active');
                            }
                        }
                        else if (pi == 101) {
                            if (registerList.PageIndex < (registerList.PageCount - 1)) {
                                registerList.PageIndex++;
                                registerList.ReloadListData();

                                $('.pagination .paginate_button.page-item.active').removeClass('active');
                                $('.pagination .paginate_button.page-item a[data-dt-idx=' + registerList.PageIndex + ']').addClass('active');
                            }
                        }
                        else {
                            registerList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                            registerList.ReloadListData();

                            $('.pagination .paginate_button.page-item.active').removeClass('active');
                            $(this).addClass('active');
                        }

                        return false;
                    });

                    $('.registerList #tableData #datatables_previous').click(function () {

                        if (registerList.PageIndex > 0) {
                            registerList.PageIndex--;
                            registerList.ReloadListData();
                        }

                        return false;
                    });

                    $('.registerList #tableData #datatables_next').click(function () {

                        if (registerList.PageIndex < (registerList.PageCount - 1)) {
                            registerList.PageIndex++;
                            registerList.ReloadListData();
                        }

                        return false;
                    });

                    // Initial data

                    // Datatable Section
                    registerList.LoadListData();

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="registerForm popup-form">
                <form id="registerForm" class="form-padding">
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="sltYear"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltYear" name="sltYear" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %></option>
                                <asp:Literal ID="ltrYear" runat="server" />
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="sltStudentType"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103201") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltStudentType" name="sltStudentType" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103218") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103218") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103051") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103052") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="sltLevel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltLevel" name="sltLevel" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %></option>
                                <asp:Literal ID="ltrLevel" runat="server" />
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="sltPlan"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103202") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltPlan" name="sltPlan" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103219") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103219") %></option>
                                <asp:Literal ID="ltrPlan" runat="server" />
                            </select>
                        </div>
                    </div>
                    <%--/*SB-7758--%>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="iptActiveBackupPlan"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103203") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8">
                            <input id="iptActiveBackupPlan" type="checkbox" disabled data-on="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101117") %>" data-off="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>" data-toggle="toggle" data-onstyle="success" data-height="40" data-width="100" data-style="toggle-switch" />
                        </div>
                    </div>
                    <div class="row div-row-padding backup-plans hide">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="iptOrderPlans"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103204") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <div class="input-group">
                                <input type="text" class="form-control"
                                    id="iptOrderPlans" name="iptOrderPlans" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103204") %>" maxlength="2" />
                                <div class="input-group-addon">
                                    <span class="input-group-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row div-row-padding backup-plans hide">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="sltBackupPlans"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103206") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltBackupPlans" name="sltBackupPlans" class="selectpicker col-sm-12" data-style="select-with-transition" multiple data-size="10" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103206") %>">
                            </select>
                        </div>
                    </div>
                    <%--SB-7758*/--%>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="sltPaymentGroup"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103207") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltPaymentGroup" name="sltPaymentGroup" data-live-search="true" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103220") %>" >
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103022") %></option>
                                <asp:Literal ID="ltrPaymentGroup" runat="server" />
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="iptStudentMax"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103221") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <div class="input-group">
                                <input type="text" class="form-control"
                                    id="iptStudentMax" name="iptStudentMax" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103221") %>" maxlength="6" />
                                <div class="input-group-addon">
                                    <span class="input-group-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="iptDocumentDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103209") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8">
                            <div class="form-group div-datepicker">
                                <input id="iptDocumentDate" name="iptDocumentDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="iptEndDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103210") %> :</label>
                        </div>
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
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="iptFee"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103222") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptFee" name="iptFee"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103222") %>" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="iptExamAnnounce"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103212") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8">
                            <input id="iptExamAnnounce" type="checkbox" data-on="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101117") %>" data-off="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>" data-toggle="toggle" data-onstyle="success" data-height="40" data-width="100" data-style="toggle-switch" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="iptMeetingDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103213") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8">
                            <div class="form-group div-datepicker">
                                <input id="iptMeetingDate" name="iptMeetingDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="iptMeetingTime"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103214") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8">
                            <div class="form-group div-datepicker">
                                <input id="iptMeetingTime" name="iptMeetingTime" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="iptMeetingPlace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103223") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptMeetingPlace" name="iptMeetingPlace"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103223") %>" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="iptAttachmentsPassExam"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103216") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8 div-text-input" style="display: flex;">
                            <input type="file" id="iptAttachmentsPassExam" class="filestyle" />
                            <a id="aAttachmentsPassExam" href="#" style="display: none;">
                                <p id="pAttachmentsPassExam" class="fa fa-file" aria-hidden="true" style="color: #555; margin: 15px 0px 0px 10px;"></p>
                            </a>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right">
                            <label for="iptAttachmentsFailExam"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103217") %> :</label>
                        </div>
                        <div class="col-md-8 mb-8 div-text-input" style="display: flex;">
                            <input type="file" id="iptAttachmentsFailExam" class="filestyle" />
                            <a id="aAttachmentsFailExam" href="#" style="display: none;">
                                <p id="pAttachmentsFailExam" class="fa fa-file" aria-hidden="true" style="color: #555; margin: 15px 0px 0px 10px;"></p>
                            </a>
                        </div>
                    </div>
                    <div class="row text-center" style="display: block; padding-top: 15px;">
                        <button id="save" type="submit" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="cancel" type="button"
                            class="btn btn-danger" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                function LoadPlan(classLevel) {
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "RegisterRecruitmentData.aspx/LoadPlan",
                        data: '{classLevel: ' + classLevel + ' }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: OnSuccessLoadPlan,
                        failure: function (response) {
                            console.log(response.d);
                        },
                        error: function (response) {
                            console.log(response.d);
                        }
                    });
                }

                function OnSuccessLoadPlan(response) {
                    var plans = response.d;

                    $('#sltPlan').empty();

                    var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103219") %></option><option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>';
                    var options2 = '';
                    $(plans).each(function () {

                        options += '<option value="' + this.ID + '">' + this.Planname + '</option>';
                        options2 += '<option value="' + this.ID + '">' + this.Planname + '</option>';
                    });

                    $('#sltPlan').html(options);
                    $('#sltPlan').selectpicker('refresh');

                    $('#sltBackupPlans').html(options2);
                    $('#sltBackupPlans').selectpicker('refresh');
                }

                var registerForm = {
                    GetItem: function (rid) {
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: "RegisterRecruitmentData.aspx/GetItem",
                            data: '{id: ' + rid + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: registerForm.OnSuccessGet,
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

                            // Init
                            $("#iptMeetingDate").prop("disabled", true);
                            $("#iptMeetingTime").prop("disabled", true);
                            $("#iptMeetingPlace").prop("disabled", true);
                            $("#iptAttachmentsPassExam").prop("disabled", true);
                            $("#iptAttachmentsFailExam").prop("disabled", true);

                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                $("#sltYear").selectpicker('val', $(this).find("F1").text());
                                $("#sltStudentType").selectpicker('val', $(this).find("F3").text());
                                $("#sltLevel").selectpicker('val', $(this).find("F4").text());

                                LoadPlan($("#sltLevel").val());

                                $("#sltPlan").selectpicker('val', $(this).find("F5").text());
                                $("#iptStudentMax").val($(this).find("F6").text());
                                $("#iptDocumentDate").val($(this).find("F7").text());
                                $("#iptEndDate").val($(this).find("F8").text());
                                $("#iptFee").val($(this).find("F9").text());

                                $("#iptExamAnnounce").bootstrapToggle($(this).find("F10").text() == '1' ? 'on' : 'off');
                                $("#iptMeetingDate").val($(this).find("F11").text());
                                $("#iptMeetingTime").val($(this).find("F12").text());
                                $("#iptMeetingPlace").val($(this).find("F13").text());
                                //14, 15
                                if ($(this).find("F14").text()) {
                                    $("#aAttachmentsPassExam").attr('href', $(this).find("F14").text()).attr('target', '_blank').show();
                                    $("#pAttachmentsPassExam").text($(this).find("F15").text());
                                }
                                //16, 17
                                if ($(this).find("F16").text()) {
                                    $("#aAttachmentsFailExam").attr('href', $(this).find("F16").text()).attr('target', '_blank').show();
                                    $("#pAttachmentsFailExam").text($(this).find("F17").text());
                                }
                                $("#sltPaymentGroup").selectpicker('val', $(this).find("F18").text());

                                $("#iptActiveBackupPlan").bootstrapToggle($(this).find("F19").text() ? 'on' : 'off');
                                $("#iptOrderPlans").val($(this).find("F20").text());

                                if (!!$(this).find("F21").text()) {
                                    var backupPlansObj = JSON.parse($(this).find("F21").text());
                                    var backupPlanIds = backupPlansObj.map(function (o) { return o.planId; });
                                    $("#sltBackupPlans").val(backupPlanIds);
                                    $('#sltBackupPlans').selectpicker('refresh');
                                }
                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: "Ashx/UploadFileRecruitmentHandler.ashx",
                            data: data,
                            dataType: "json",
                            contentType: false,
                            processData: false,
                            success: registerForm.OnSuccessSave,
                            failure: function (xhr, status, error) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>' + error);

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (xhr, status, error) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>' + error);

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSave: function (response) {
                        var title = "";
                        var body = "";

                        if (response.success) {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';
                        } else {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [' + response.message + ']';
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ClearSession: function (callbackRedirect) {
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: "RegisterRecruitmentData.aspx/ClearSessionID",
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

                    $.validator.addMethod("thaiDate",
                        function (value, element) {
                            if (!value) return true; // when set required: false
                            return value.match(/^(0?[1-9]|[12][0-9]|3[0-1])[/., -](0?[1-9]|1[<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305069") %>])[/., -](24|25)?\d{2}$/);
                        },
                        "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111051") %>"
                    );

                    $.validator.addMethod("time", function (value, element) {
                        return this.optional(element) || /^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$/i.test(value);
                    }, "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132952") %>");

                    $("#registerForm").validate({
                        rules: {
                            sltYear: "required",
                            sltStudentType: "required",
                            sltLevel: "required",
                            sltPlan: "required",
                            sltPaymentGroup: {
                                required: false,
                            },
                            iptStudentMax: {
                                required: true,
                                number: true,
                                digits: true
                            },
                            iptFee: {
                                required: true,
                                number: true,
                                digits: true
                            },
                            iptDocumentDate: {
                                required: false,
                                thaiDate: true
                            },
                            iptEndDate: {
                                required: false,
                                thaiDate: true
                            },
                            iptMeetingDate: {
                                required: false,
                                thaiDate: true
                            },
                            iptMeetingTime: {
                                required: false,
                                time: true
                            },
                            iptOrderPlans: {
                                required: function (element) {
                                    return $('#iptActiveBackupPlan').prop('checked') && !$(element).val();
                                },
                                number: true,
                                digits: true
                            },
                            sltBackupPlans: {
                                required: function (element) {
                                    return $('#iptActiveBackupPlan').prop('checked') && $(element).val().length == 0;
                                }
                            }
                        },
                        messages: {
                            sltYear: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltStudentType: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltLevel: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltPlan: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltPaymentGroup: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptStudentMax: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>",
                                digits: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121005") %>"
                            },
                            iptFee: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>",
                                digits: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121005") %>"
                            },
                            iptDocumentDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptEndDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptMeetingDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptMeetingTime: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptOrderPlans: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>",
                                digits: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121005") %>"
                            },
                            sltBackupPlans: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptStudentMax": 
                                case "iptOrderPlans": error.insertAfter(element.parent()); break;
                                case "iptFee":
                                case "iptDocumentDate":
                                case "iptEndDate":
                                case "iptMeetingDate":
                                case "iptMeetingTime": error.insertAfter(element); break;
                                case "sltYear":
                                case "sltStudentType":
                                case "sltLevel":
                                case "sltPlan":
                                case "sltPaymentGroup": 
                                case "sltBackupPlans": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    $(".registerForm #save").bind({
                        click: function () {

                            // Check order plans eq set backup plans
                            if ($('#iptActiveBackupPlan').prop('checked') && $('#iptOrderPlans').val() != $("#sltBackupPlans").val().length) {
                                Swal.fire({
                                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %>!',
                                    html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132953") %> <b style="font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103204") %></b> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132955") %> <b style="font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103206") %></b> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132954") %>',
                                    type: 'warning',
                                    confirmButtonClass: "btn btn-warning",
                                    buttonsStyling: false
                                });

                                return false;
                            }

                            if ($('#registerForm').valid()) {
                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var formData = new FormData();// it creats form object
                                    formData.append('yearID', $("#sltYear").val());
                                    formData.append('year', $('#sltYear option:selected').text());
                                    formData.append('studentType', $("#sltStudentType").val());
                                    formData.append('levelID', $("#sltLevel").val());
                                    formData.append('planID', $("#sltPlan").val());
                                    formData.append('paymentGroupID', $("#sltPaymentGroup").children("option:selected").val());
                                    formData.append('studentMax', $("#iptStudentMax").val());
                                    formData.append('documentDate', $("#iptDocumentDate").val());
                                    formData.append('endDate', $("#iptEndDate").val());
                                    formData.append('fee', $("#iptFee").val());

                                    formData.append('examAnnounce', $('#iptExamAnnounce').prop('checked') ? '1' : '0');
                                    formData.append('meetingDate', $("#iptMeetingDate").val());
                                    formData.append('meetingTime', $("#iptMeetingTime").val());
                                    formData.append('meetingPlace', $("#iptMeetingPlace").val());
                                    formData.append('filePassExam', $('#iptAttachmentsPassExam')[0].files[0]); //it reads file from from HTML form
                                    formData.append('fileFailExam', $('#iptAttachmentsFailExam')[0].files[0]); //it reads file from from HTML form

                                    formData.append('activeBackupPlan', $('#iptActiveBackupPlan').prop('checked'));
                                    if ($('#iptActiveBackupPlan').prop('checked')) {

                                        formData.append('orderPlans', $('#iptOrderPlans').val());

                                        var backupPlans = [];
                                        $("#sltBackupPlans option:selected").each(function () {
                                            backupPlans.push({ planId: $(this).val(), planName: $(this).text() });
                                        });
                                        formData.append('backupPlans', JSON.stringify(backupPlans));
                                    }

                                    registerForm.SaveItem(formData);

                                });
                            }

                            return false;
                        }
                    });

                    $(".registerForm #cancel").off().on("click", function (e) {

                        // Close modal
                        registerForm.ClearSession(function () {
                            modalForm.hideForm();
                        });

                        return false;

                    });

                    // Modal Section
                    $('#modalNotifyConfirmSave').off().on('show.bs.modal', function (e) {
                        $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                        $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
                    });

                    $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

                        // Close modal
                        registerForm.ClearSession(function () {
                            modalForm.hideForm();

                            registerList.ReloadListData();
                        });

                    });

                    $("#sltLevel").change(function () {

                        if ($("#sltLevel").val() != '') {
                            LoadPlan($("#sltLevel").val());
                        }

                    });

                    $("#sltPlan").change(function () {

                        if ($(this).val() == '0') {
                            $("#iptActiveBackupPlan").bootstrapToggle('enable');
                        }
                        else {
                            $("#iptActiveBackupPlan").bootstrapToggle('off');
                            $("#iptActiveBackupPlan").bootstrapToggle('disable');
                        }

                    });

                    // Initial data
                    //$('#divDocumentDate, #divEndDate, #divMeetingDate').datetimepicker({
                    //    format: 'DD/MM/YYYY-BE',
                    //    locale: 'th'
                    //});

                    //$('#divMeetingTime').datetimepicker({
                    //    format: 'HH:mm'
                    //});
                    $('#iptDocumentDate, #iptEndDate, #iptMeetingDate').datetimepicker({
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
                    $("#iptDocumentDate, #iptEndDate, #iptMeetingDate").attr('maxlength', '10');

                    $('#iptMeetingTime').datetimepicker({
                        keepOpen: false,
                        debug: false,
                        format: 'HH:mm',
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
                    $("#iptMeetingTime").attr('maxlength', '5');

                    $('#iptExamAnnounce, #iptActiveBackupPlan').bootstrapToggle();

                    $('#iptExamAnnounce').change(function () {
                        $("#iptMeetingDate").prop("disabled", !$(this).prop('checked'));
                        $("#iptMeetingTime").prop("disabled", !$(this).prop('checked'));
                        $("#iptMeetingPlace").prop("disabled", !$(this).prop('checked'));
                        $("#iptAttachmentsPassExam").prop("disabled", !$(this).prop('checked'));
                        $("#iptAttachmentsFailExam").prop("disabled", !$(this).prop('checked'));
                    });

                    $('#iptActiveBackupPlan').change(function () {
                        if ($(this).prop('checked')) {
                            $('.backup-plans').removeClass('hide');
                        }
                        else {
                            $('.backup-plans').addClass('hide');
                        }
                    });

                    $('#iptAttachmentsPassExam').filestyle({
                        input: false,
                        buttonName: 'btn-warning',
                        buttonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103224") %>'
                    });

                    $('#iptAttachmentsFailExam').filestyle({
                        input: false,
                        buttonName: 'btn-warning',
                        buttonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103225") %>'
                    });

                    activateBootstrapSelect('.registerForm .selectpicker');

                    // Load info command
                    registerForm.GetItem(<%=Request.QueryString["rid"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
        </asp:View>
    </asp:MultiView>
</body>
</html>
