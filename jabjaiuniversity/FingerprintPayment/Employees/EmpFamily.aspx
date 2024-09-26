<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpFamily.aspx.cs" Inherits="FingerprintPayment.Employees.EmpFamily" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <div class="empFamilyList">
                <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102033") %></p>
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
                                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101192") %></th>
                                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></th>
                                            <th style="width: 25%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                                            <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></th>
                                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102034") %></th>
                                            <th style="width: 10%" class="text-center">
                                                <button type="button" class="btn btn-success col-md-12" data-toggle="modal" data-target="#modalShowForm" form-name="EmpFamily.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102036") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></button></th>
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

                var empFamilyList = {
                    PageIndex: 0,
                    PageSize: 20,
                    PageCount: 0,
                    TotalRows: 0,
                    dt: null,
                    LoadListData: function (eid) {
                        empFamilyList.dt = $(".empFamilyList #tableData").DataTable({
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
                                "url": "Ashx/LoadEmpFamilyList.ashx",
                                "type": "POST",
                                "contentType": "application/json; charset=utf-8",
                                "data": function (d) {
                                    d.eid = eid;
                                    d.search = '';
                                    d.page = empFamilyList.PageIndex;
                                    d.length = empFamilyList.PageSize;

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
                                { "data": "FamilyRelation", "orderable": true },
                                { "data": "Title", "orderable": true },
                                { "data": "FirstName", "orderable": true },
                                { "data": "LastName", "orderable": true },
                                { "data": "Birthday", "orderable": true },
                                { "data": "PersonalStatus", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[8, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 5, 6, 7] },
                                { "targets": [8], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="EmpFamily.aspx" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102036") %>" style="padding-right: 5px; font-size: 22px;"><i class="fa fa-edit"></i></a>' +
                                            '<a href="#" class="remove-row" data-title="Confirm Dialog" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101240") %>"><i class="fa fa-remove text-danger" style="font-size: 22px;"></i></a>';
                                    },
                                    "targets": 7
                                }
                            ],
                            "drawCallback": function (settings) {
                                var json = settings.json;
                                //console.log(json);

                                empFamilyList.PageCount = json.pageCount;
                                empFamilyList.TotalRows = json.recordsTotal;

                                var pageLRSize = 3;
                                var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                                var previousDot = '';
                                var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                                var nextDot = '';
                                var elements = '';

                                if (empFamilyList.PageIndex - pageLRSize > 1) {
                                    previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                                }

                                if (empFamilyList.PageIndex + pageLRSize < json.pageCount - 1) {
                                    nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                                }

                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    if (pi == 0) {
                                        elements += '<li class="paginate_button page-item ' + (empFamilyList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                        elements += previousDot;
                                    }
                                    else if (pi == json.pageCount - 1) {
                                        elements += nextDot;
                                        elements += '<li class="paginate_button page-item ' + (empFamilyList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                    else if (empFamilyList.PageIndex - pageLRSize <= pi && empFamilyList.PageIndex + pageLRSize >= pi) {
                                        elements += '<li class="paginate_button page-item ' + (empFamilyList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                }

                                $('.empFamilyList .pagination').html(previous + elements + next);

                                $('.empFamilyList .dataTables_info').html('Showing ' + ((empFamilyList.PageIndex * empFamilyList.PageSize) + 1) + ' to ' + ((empFamilyList.PageIndex * empFamilyList.PageSize) + empFamilyList.PageSize) + ' of ' + empFamilyList.TotalRows + ' rows');
                            }
                        });
                        // order.dt search.dt
                        empFamilyList.dt.on('draw.dt', function () {
                            empFamilyList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (empFamilyList.PageIndex * empFamilyList.PageSize) + (i + 1) + '.';
                            });
                            empFamilyList.dt.column(7, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = empFamilyList.dt.cells({ row: i, column: 8 }).data()[0];
                                $(cell).find(".fa-edit").parent().attr("pid", id);
                                $(cell).find(".fa-remove").parent().attr("pid", id);
                            });
                        });
                    },
                    RemoveItem: function (eid, pid) {
                        $.ajax({
                            type: "POST",
                            url: "EmpFamily.aspx/RemoveItem",
                            data: '{eid: ' + eid + ', id: ' + pid + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empFamilyList.OnSuccessRemove,
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

                                empFamilyList.ReloadListData();
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
                        empFamilyList.dt.draw();
                    }
                }

                $(document).ready(function () {

                    // Searching, Pagination event 
                    $('.empFamilyList #datatables_length').change(function () {

                        empFamilyList.PageSize = parseInt($("#datatables_length").children("option:selected").val());
                        empFamilyList.PageIndex = 0;

                        empFamilyList.ReloadListData();

                        return false;
                    });

                    $('.empFamilyList ul.pagination').on('click', 'li.paginate_button a', function () {

                        var pi = parseInt($(this).attr("data-dt-idx"));

                        if (pi == 100) {
                            if (empFamilyList.PageIndex > 0) {
                                empFamilyList.PageIndex--;
                                empFamilyList.ReloadListData();

                                $('.empFamilyList .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empFamilyList .pagination .paginate_button.page-item a[data-dt-idx=' + empFamilyList.PageIndex + ']').addClass('active');
                            }
                        }
                        else if (pi == 101) {
                            if (empFamilyList.PageIndex < (empFamilyList.PageCount - 1)) {
                                empFamilyList.PageIndex++;
                                empFamilyList.ReloadListData();

                                $('.empFamilyList .pagination .paginate_button.page-item.active').removeClass('active');
                                $('.empFamilyList .pagination .paginate_button.page-item a[data-dt-idx=' + empFamilyList.PageIndex + ']').addClass('active');
                            }
                        }
                        else {
                            empFamilyList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                            empFamilyList.ReloadListData();

                            $('.empFamilyList .pagination .paginate_button.page-item.active').removeClass('active');
                            $(this).addClass('active');
                        }

                        return false;
                    });

                    $('.empFamilyList #tableData #datatables_previous').click(function () {

                        if (empFamilyList.PageIndex > 0) {
                            empFamilyList.PageIndex--;
                            empFamilyList.ReloadListData();
                        }

                        return false;
                    });

                    $('.empFamilyList #tableData #datatables_next').click(function () {

                        if (empFamilyList.PageIndex < (empFamilyList.PageCount - 1)) {
                            empFamilyList.PageIndex++;
                            empFamilyList.ReloadListData();
                        }

                        return false;
                    });


                    $('.empFamilyList #tableData').on('click', '.remove-row', function () {

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
                            empFamilyList.RemoveItem(<%=Request.QueryString["eid"]%>, $(this).attr('pid'));

                        });

                        $("#modalNotifyConfirmRemove").modal('show');

                    });

                    // Datatable Section
                    empFamilyList.LoadListData(<%=Request.QueryString["eid"]%>);

                    if ('<%=Request.QueryString["eid"]%>' == '0') {
                        $('.empFamilyList #tableData thead tr th button').addClass("disabled");
                    }

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="empFamilyForm popup-form">
                <form id="empFamilyForm" class="form-padding">
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptFamilyRelationship"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102037") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptFamilyRelationship" name="iptFamilyRelationship"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102037") %>" maxlength="20" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="sltTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> :</label></div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltTitle" name="sltTitle" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %></option>
                                <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptFirstName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptFirstName" name="iptFirstName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>" maxlength="30" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptLastName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptLastName" name="iptLastName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>" maxlength="50" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label></div>
                        <div class="col-md-8 mb-8">
                            <div class="form-group div-datepicker">
                                <input id="iptBirthday" name="iptBirthday" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="sltPersonalStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102022") %> :</label></div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltPersonalStatus" name="sltPersonalStatus" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102021") %>">
                                <option selected="selected" value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102038") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121008") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102039") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102040") %></option>
                                <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102041") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptLiveStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102042") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptLiveStatus" name="iptLiveStatus"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102042") %>" maxlength="30" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptDeathStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102043") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptDeathStatus" name="iptDeathStatus"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102043") %>" maxlength="30" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="iptFamilyCareer"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102044") %> :</label></div>
                        <div class="col-md-8 mb-8 div-text-input">
                            <input type="text" class="form-control" id="iptFamilyCareer" name="iptFamilyCareer"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102044") %>" maxlength="50" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4 col-form-label text-right"><label for="sltEducationBackground"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102045") %> :</label></div>
                        <div class="col-md-8 mb-8 div-select-input">
                            <select id="sltEducationBackground" name="sltEducationBackground" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102046") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102046") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102054") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %></option>
                                <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102057") %></option>
                                <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102058") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102059") %></option>
                                <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %></option>
                                <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102061") %></option>
                                <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102062") %></option>
                                <asp:Literal ID="ltrLevel" runat="server"></asp:Literal>
                            </select>
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

                var empFamilyForm = {
                    GetItem: function (empID, id) {
                        $.ajax({
                            type: "POST",
                            url: "EmpFamily.aspx/GetItem",
                            data: '{empID: ' + empID + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empFamilyForm.OnSuccessGet,
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

                                $(".empFamilyForm #iptFamilyRelationship").val($(this).find("F1").text());
                                $(".empFamilyForm #sltTitle").selectpicker('val', $(this).find("F2").text());
                                $(".empFamilyForm #iptFirstName").val($(this).find("F3").text());
                                $(".empFamilyForm #iptLastName").val($(this).find("F4").text());
                                $(".empFamilyForm #iptBirthday").val($(this).find("F5").text());
                                $(".empFamilyForm #sltPersonalStatus").selectpicker('val', $(this).find("F6").text());
                                $(".empFamilyForm #iptLiveStatus").val($(this).find("F7").text());
                                $(".empFamilyForm #iptDeathStatus").val($(this).find("F8").text());
                                $(".empFamilyForm #iptFamilyCareer").val($(this).find("F9").text());
                                $(".empFamilyForm #sltEducationBackground").selectpicker('val', $(this).find("F10").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "EmpFamily.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empFamilyForm.OnSuccessSave,
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
                                    empFamilyForm.ClearSession(function () {
                                        modalForm.hideForm();

                                        empFamilyList.ReloadListData();
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
                            url: "EmpFamily.aspx/ClearSessionID",
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

                    $("#empFamilyForm").validate({
                        rules: {
                            iptFamilyRelationship: "required",
                            sltTitle: "required",
                            iptFirstName: "required",
                            iptLastName: "required",
                            iptBirthday: {
                                thaiDate: true
                            }
                        },
                        messages: {
                            iptFamilyRelationship: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltTitle: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptFirstName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptLastName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptBirthday: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            }
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptFamilyRelationship":
                                case "iptFirstName":
                                case "iptLastName":
                                case "iptBirthday": error.insertAfter(element); break;
                                case "sltTitle": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    $(".empFamilyForm #save").bind({
                        click: function () {

                            if ($('#empFamilyForm').valid()) {

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
                                    data[1] = $(".empFamilyForm #iptFamilyRelationship").val();
                                    data[2] = $(".empFamilyForm #sltTitle").val();
                                    data[3] = $(".empFamilyForm #iptFirstName").val();
                                    data[4] = $(".empFamilyForm #iptLastName").val();
                                    data[5] = $(".empFamilyForm #iptBirthday").val();
                                    data[6] = $(".empFamilyForm #sltPersonalStatus").val();
                                    data[7] = $(".empFamilyForm #iptLiveStatus").val();
                                    data[8] = $(".empFamilyForm #iptDeathStatus").val();
                                    data[9] = $(".empFamilyForm #iptFamilyCareer").val();
                                    data[10] = $(".empFamilyForm #sltEducationBackground").val();

                                    empFamilyForm.SaveItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".empFamilyForm #cancel").bind({
                        click: function () {

                            // Close modal
                            empFamilyForm.ClearSession(function () {
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
                    //initFunction.setDropdown('#sltTitle', 'Title', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %></option>');
                    //initFunction.setDropdown('#sltLevel', 'Level', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102046") %></option>');


                    //$('.empFamilyForm #divBirthday').datetimepicker({
                    //    format: 'DD/MM/YYYY-BE',
                    //    locale: 'th'
                    //});
                    $('.empFamilyForm .datepicker').datetimepicker({
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

                    $(".empFamilyForm .datepicker").attr('maxlength', '10');

                    activateBootstrapSelect('.empFamilyForm .selectpicker');

                    // Load info command
                    xEmpKey.pid = "<%=Request.QueryString["id"]%>";
                    empFamilyForm.GetItem(<%=Request.QueryString["eid"]%>, <%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            View Content
        </asp:View>
    </asp:MultiView>
</body>
</html>
