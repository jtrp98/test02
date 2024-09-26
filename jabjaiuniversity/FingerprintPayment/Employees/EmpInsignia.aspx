<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpInsignia.aspx.cs" Inherits="FingerprintPayment.Employees.EmpInsignia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <div class="empInsigniaList">
                <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102113") %></p>
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
                                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102130") %></th>
                                            <th style="width: 33%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102132") %></th>
                                            <th style="width: 32%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></th>
                                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102140") %></th>
                                            <th style="width: 10%" class="text-center">
                                                <button type="button" class="btn btn-success col-md-12" data-toggle="modal" data-target="#modalShowForm" form-name="EmpInsignia.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102129") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></button></th>
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

                var empInsigniaList = {
                    PageIndex: 0,
                    PageSize: 20,
                    PageCount: 0,
                    TotalRows: 0,
                    dt: null,
                    LoadListData: function (eid) {
                        empInsigniaList.dt = $(".empInsigniaList #tableData").DataTable({
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
                                "url": "Ashx/LoadEmpInsigniaList.ashx",
                                "type": "POST",
                                "contentType": "application/json; charset=utf-8",
                                "data": function (d) {
                                    d.eid = eid;
                                    d.search = '';
                                    d.page = empInsigniaList.PageIndex;
                                    d.length = empInsigniaList.PageSize;

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
                                { "data": "Year", "orderable": true },
                                { "data": "Grade", "orderable": true },
                                { "data": "Position", "orderable": true },
                                { "data": "Date", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[6, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6] },
                                { "targets": [6], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="EmpInsignia.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102129") %>" style="padding-right: 5px; font-size: 22px;"><i class="fa fa-edit"></i></a>' +
                                            '<a href="#" class="remove-row" data-title="Confirm Dialog" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101240") %>"><i class="fa fa-remove text-danger" style="font-size: 22px;"></i></a>';
                                    },
                                    "targets": 5
                                }
                            ],
                            "drawCallback": function (settings) {
                                var json = settings.json;
                                //console.log(json);

                                empInsigniaList.PageCount = json.pageCount;
                                empInsigniaList.TotalRows = json.recordsTotal;

                                var pageLRSize = 3;
                                var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                                var previousDot = '';
                                var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                                var nextDot = '';
                                var elements = '';

                                if (empInsigniaList.PageIndex - pageLRSize > 1) {
                                    previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                                }

                                if (empInsigniaList.PageIndex + pageLRSize < json.pageCount - 1) {
                                    nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                                }

                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    if (pi == 0) {
                                        elements += '<li class="paginate_button page-item ' + (empInsigniaList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                        elements += previousDot;
                                    }
                                    else if (pi == json.pageCount - 1) {
                                        elements += nextDot;
                                        elements += '<li class="paginate_button page-item ' + (empInsigniaList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                    else if (empInsigniaList.PageIndex - pageLRSize <= pi && empInsigniaList.PageIndex + pageLRSize >= pi) {
                                        elements += '<li class="paginate_button page-item ' + (empInsigniaList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                }

                                $('.empInsigniaList .pagination').html(previous + elements + next);

                                $('.empInsigniaList .dataTables_info').html('Showing ' + ((empInsigniaList.PageIndex * empInsigniaList.PageSize) + 1) + ' to ' + ((empInsigniaList.PageIndex * empInsigniaList.PageSize) + empInsigniaList.PageSize) + ' of ' + empInsigniaList.TotalRows + ' rows');
                            }
                        });
                        // order.dt search.dt
                        empInsigniaList.dt.on('draw.dt', function () {
                            empInsigniaList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (empInsigniaList.PageIndex * empInsigniaList.PageSize) + (i + 1) + '.';
                            });
                            empInsigniaList.dt.column(5, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = empInsigniaList.dt.cells({ row: i, column: 6 }).data()[0];
                                $(cell).find(".fa-edit").parent().attr("pid", id);
                                $(cell).find(".fa-remove").parent().attr("pid", id);
                            });
                        });
                    },
                    RemoveItem: function (eid, pid) {
                        $.ajax({
                            type: "POST",
                            url: "EmpInsignia.aspx/RemoveItem",
                            data: '{eid: ' + eid + ', id: ' + pid + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empInsigniaList.OnSuccessRemove,
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

                                empInsigniaList.ReloadListData();
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
                        //var dt = $('.empInsigniaList #tableData').DataTable();
                        empInsigniaList.dt.draw();
                    }
                }

                $(document).ready(function () {

                    // Searching, Pagination event 
                    $('.empInsigniaList #datatables_length').change(function () {

                        empInsigniaList.PageSize = parseInt($(".empInsigniaList #datatables_length").children("option:selected").val());
                        empInsigniaList.PageIndex = 0;

                        empInsigniaList.ReloadListData();

                        return false;
                    });

                    $('.empInsigniaList ul.pagination').on('click', 'li.paginate_button a', function () {

                        var pi = parseInt($(this).attr("data-dt-idx"));

                        if (pi == 100) {
                            if (empInsigniaList.PageIndex > 0) {
                                empInsigniaList.PageIndex--;
                                empInsigniaList.ReloadListData();

                                $('.empInsigniaList .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empInsigniaList .pagination .paginate_button.page-item a[data-dt-idx=' + empInsigniaList.PageIndex + ']').addClass('active');
                            }
                        }
                        else if (pi == 101) {
                            if (empInsigniaList.PageIndex < (empInsigniaList.PageCount - 1)) {
                                empInsigniaList.PageIndex++;
                                empInsigniaList.ReloadListData();

                                $('.empInsigniaList .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empInsigniaList .pagination .paginate_button.page-item a[data-dt-idx=' + empInsigniaList.PageIndex + ']').addClass('active');
                            }
                        }
                        else {
                            empInsigniaList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                            empInsigniaList.ReloadListData();

                            $('.empInsigniaList .pagination .paginate_button.page-item.active').removeClass('active');
                            $(this).addClass('active');
                        }

                        return false;
                    });

                    $('.empInsigniaList #tableData #datatables_previous').click(function () {

                        if (empInsigniaList.PageIndex > 0) {
                            empInsigniaList.PageIndex--;
                            empInsigniaList.ReloadListData();
                        }

                        return false;
                    });

                    $('.empInsigniaList #tableData #datatables_next').click(function () {

                        if (empInsigniaList.PageIndex < (empInsigniaList.PageCount - 1)) {
                            empInsigniaList.PageIndex++;
                            empInsigniaList.ReloadListData();
                        }

                        return false;
                    });


                    $('.empInsigniaList #tableData').on('click', '.remove-row', function () {

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
                            empInsigniaList.RemoveItem(<%=Request.QueryString["eid"]%>, $(this).attr('pid'));

                        });

                        $("#modalNotifyConfirmRemove").modal('show');

                    });

                    // Datatable Section
                    empInsigniaList.LoadListData(<%=Request.QueryString["eid"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="empInsigniaForm popup-form" style="padding: 15px;">
                <form id="empInsigniaForm" class="form-padding">
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="sltYear"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102130") %> :</label></div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltYear" name="sltYear" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102131") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102131") %></option>
                                <asp:Literal ID="ltrYear" runat="server"></asp:Literal>
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptGrade"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102132") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptGrade" name="iptGrade"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102132") %>" maxlength="50" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptPosition"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptPosition" name="iptPosition"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %>" maxlength="50" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptBookNumber"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102135") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptBookNumber" name="iptBookNumber"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102135") %>" maxlength="10" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptPart"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102137") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptPart" name="iptPart"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102137") %>" maxlength="10" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptNumber"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102139") %>:</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptNumber" name="iptNumber"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102138") %>" maxlength="10" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102140") %> :</label></div>
                        <div class="col-md-8 mb-8">
                            <div class="form-group div-datepicker">
                                <input id="iptDate" name="iptDate" type="text" class="form-control datepicker" />
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

                var empInsigniaForm = {
                    GetItem: function (empID, id) {
                        $.ajax({
                            type: "POST",
                            url: "EmpInsignia.aspx/GetItem",
                            data: '{empID: ' + empID + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empInsigniaForm.OnSuccessGet,
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

                                $(".empInsigniaForm #sltYear").selectpicker('val', $(this).find("F1").text());
                                $(".empInsigniaForm #iptGrade").val($(this).find("F2").text());
                                $(".empInsigniaForm #iptPosition").val($(this).find("F3").text());
                                $(".empInsigniaForm #iptBookNumber").val($(this).find("F4").text());
                                $(".empInsigniaForm #iptPart").val($(this).find("F5").text());
                                $(".empInsigniaForm #iptNumber").val($(this).find("F6").text());
                                $(".empInsigniaForm #iptDate").val($(this).find("F7").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "EmpInsignia.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empInsigniaForm.OnSuccessSave,
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
                                    empInsigniaForm.ClearSession(function () {
                                        modalForm.hideForm();

                                        empInsigniaList.ReloadListData();
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
                            url: "EmpInsignia.aspx/ClearSessionID",
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

                    $("#empInsigniaForm").validate({
                        rules: {
                            sltYear: "required",
                            iptGrade: "required",
                            iptPosition: "required",
                            iptBookNumber: "required",
                            iptPart: "required",
                            iptNumber: "required",
                            iptDate: {
                                required: true,
                                thaiDate: true
                            }
                        },
                        messages: {
                            sltYear: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptGrade: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptPosition: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptBookNumber: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptPart: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptNumber: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            }
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptGrade":
                                case "iptPosition":
                                case "iptBookNumber":
                                case "iptPart":
                                case "iptNumber":
                                case "iptDate": error.insertAfter(element); break;
                                case "sltYear": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    $(".empInsigniaForm #save").bind({
                        click: function () {

                            if ($('#empInsigniaForm').valid()) {

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
                                    data[1] = $(".empInsigniaForm #sltYear").val();
                                    data[2] = $(".empInsigniaForm #iptGrade").val();
                                    data[3] = $(".empInsigniaForm #iptPosition").val();
                                    data[4] = $(".empInsigniaForm #iptBookNumber").val();
                                    data[5] = $(".empInsigniaForm #iptPart").val();
                                    data[6] = $(".empInsigniaForm #iptNumber").val();
                                    data[7] = $(".empInsigniaForm #iptDate").val();

                                    empInsigniaForm.SaveItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".empInsigniaForm #cancel").bind({
                        click: function () {

                            // Close modal
                            empInsigniaForm.ClearSession(function () {
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

                    //$('.empInsigniaForm #divDate').datetimepicker({
                    //    format: 'DD/MM/YYYY-BE',
                    //    locale: 'th'
                    //});
                    $('.empInsigniaForm .datepicker').datetimepicker({
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

                    $(".empInsigniaForm .datepicker").attr('maxlength', '10');

                    activateBootstrapSelect('.empInsigniaForm .selectpicker');

                    // Load info command
                    xEmpKey.pid = "<%=Request.QueryString["id"]%>";
                    empInsigniaForm.GetItem(<%=Request.QueryString["eid"]%>, <%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            View Content
        </asp:View>
    </asp:MultiView>
</body>
</html>
