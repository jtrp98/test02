<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPlanData.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterPlanData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <style></style>
            <div class="row">
                <div class="col-md-12">
                    <p class="text-muted" style="font-size: small;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00700") %>
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
                            <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00700") %></h4>
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
                                                        <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></th>
                                                        <th style="width: 40%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103202") %></th>
                                                        <th style="width: 25%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103186") %></th>
                                                        <th style="width: 15%" class="text-center">
                                                            <button type="button" class="btn btn-success" data-toggle="modal" data-target="#modalShowForm" form-name="RegisterPlanData.aspx" form-action="form" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103184") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103184") %></button>
                                                        </th>
                                                        <th></th>
                                                        <th></th>
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
                                "url": "RegisterPlanData.aspx/LoadRegisterPlan",
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
                                { "data": "LevelName", "orderable": true },
                                { "data": "PlanName", "orderable": true },
                                { "data": "PlanCode", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "rid", "orderable": false },
                                { "data": "lid", "orderable": false },
                                { "data": "CanRemove", "orderable": false }
                            ],
                            "order": [[6, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 3, 4] },
                                { "targets": [5, 6, 7], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<a href="#" class="fa fa-edit" data-toggle="modal" data-target="#modalShowForm" form-name="RegisterPlanData.aspx" form-action="form" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103184") %>" style="vertical-align: middle; margin-top: 5px; padding-right: 5px; font-size: 22px;"></a>' +
                                            '<a href="#" class="fa fa-remove text-danger" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102158") %>" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103188") %>" style="text-decoration: none; vertical-align: middle; padding-right: 5px; font-size: 22px;"></a>';
                                    },
                                    "targets": 4
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
                            registerList.dt.column(4, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var rid = registerList.dt.cells({ row: i, column: 5 }).data()[0];
                                var lid = registerList.dt.cells({ row: i, column: 6 }).data()[0];
                                var canRemove = registerList.dt.cells({ row: i, column: 7 }).data()[0];
                                $(cell).find(".fa-edit").attr("rid", rid).attr("lid", lid);

                                if (canRemove == 'yes') {
                                    $(cell).find(".fa-remove").attr("rid", rid).attr("lid", lid);
                                }
                                else {
                                    $(cell).find(".fa-remove").attr("data-target", "#modalWarning").attr("data-title", "Warning Dialog").attr("data-message", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132930") %>");
                                }
                            });
                        });
                    },
                    RemoveItem: function (rid, lid) {
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: "RegisterPlanData.aspx/RemoveItem",
                            data: '{rid: ' + rid + ', lid: ' + lid + '}',
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

                        var r = JSON.parse(response.d);
                        if (r.success) {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101245") %>';

                            registerList.ReloadListData();
                        }
                        else {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121007") %> [' + r.message + ']';
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
                        $lid = $(e.relatedTarget).attr('lid');
                        $(this).find('.modal-footer #modalConfirmRemove').attr('rid', $rid).attr('lid', $lid);
                    });
                    $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').on('click', function () {

                        $('#modalNotifyConfirmRemove').modal('hide');

                        $("#modalWaitDialog").modal('show');

                        // Remove command
                        registerList.RemoveItem($(this).attr('rid'), $(this).attr('lid'));

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

                        registerList.PageSize = parseInt($("#datatables_length").children("option:selected").val());
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
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="sltLevel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %> :</label></div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltLevel" name="sltLevel" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111022") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %></option>
                                <asp:Literal ID="ltrLevel" runat="server"></asp:Literal>
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptPlanName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103185") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptPlanName" name="iptPlanName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103185") %>"/>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptPlanCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103186") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptPlanCode" name="iptPlanCode"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103186") %>" minlength="2" maxlength="2" />
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

                var registerForm = {
                    GetItem: function (rid, lid) {
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: "RegisterPlanData.aspx/GetItem",
                            data: '{rid: ' + rid + ', lid: ' + lid + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: registerForm.OnSuccessGet,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                //$("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                //$("#modalWaitDialog").modal('hide');
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

                                $("#sltLevel").selectpicker('val', $(this).find("F1").text());
                                $("#iptPlanName").val($(this).find("F2").text());
                                $("#iptPlanCode").val($(this).find("F3").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: "RegisterPlanData.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: registerForm.OnSuccessSave,
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

                        var r = JSON.parse(response.d);
                        if (r.success) {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';
                        }
                        else {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [' + r.message + ']';
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
                            url: "RegisterPlanData.aspx/ClearSessionID",
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

                    $("#registerForm").validate({
                        rules: {
                            sltLevel: "required",
                            iptPlanName: "required",
                            iptPlanCode: "required"
                        },
                        messages: {
                            sltLevel: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptPlanName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptPlanCode: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptPlanName": 
                                case "iptPlanCode": error.insertAfter(element); break;
                                case "sltLevel": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    $(".registerForm #save").bind({
                        click: function () {

                            if ($('#registerForm').valid()) {
                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = new Array();
                                    data[0] = "0";
                                    data[1] = $("#sltLevel").val();
                                    data[2] = $("#iptPlanName").val();
                                    data[3] = $("#iptPlanCode").val();

                                    registerForm.SaveItem(data);

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

                    // Initial data
                    if ('<%=Request.QueryString["rid"]%>' != '0' && '<%=Request.QueryString["lid"]%>' != '0') {
                        $("#sltLevel").prop('disabled', true);
                    }

                    activateBootstrapSelect('.registerForm .selectpicker');

                    // Load info command
                    registerForm.GetItem(<%=Request.QueryString["rid"]%>, <%=Request.QueryString["lid"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
        </asp:View>
    </asp:MultiView>
</body>
</html>
