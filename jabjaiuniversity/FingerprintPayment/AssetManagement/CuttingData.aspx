<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CuttingData.aspx.cs" Inherits="FingerprintPayment.AssetManagement.CuttingData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <div class="cuttingList">
                <div class="row">
                    <div class="col-md-4 mb-4 text-left" style="padding-left: 1%;">
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132001") %></span>
                    </div>
                    <div class="col-md-4 mb-4 text-center">
                    </div>
                    <div class="col-md-4 mb-4 text-left" style="padding-right: 2%;">
                        <%--<button id="btnSearch" class="btn btn-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>--%>
                    </div>
                </div>
                <div class="row">
                </div>
                <div class="row">
                </div>
                <table id="tableData" class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132002") %></th>
                            <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132003") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502020") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601045") %></th>
                            <th style="width: 10%" class="text-center">แผนก/หน่วยงาน</th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132006") %></th>
                            <th style="width: 10%" class="text-center">
                                <button type="button" class="btn btn-success" data-toggle="modal" data-target="#modalShowForm" form-name="CuttingData.aspx" form-action="form" form-title="การตัดจ่ายครุภัณฑ์"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></button></th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>

                    <tfoot>
                        <tr>
                            <th colspan="12">
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

                var cuttingList = {
                    PageIndex: 0,
                    PageSize: 20,
                    PageCount: 0,
                    LoadListData: function () {
                        var dt = $(".cuttingList #tableData").DataTable({
                            "processing": true,
                            "serverSide": true,
                            "info": false,
                            "searching": false,
                            "paging": false,
                            "stateSave": true,
                            "ajax": {
                                "url": "Ashx/LoadCuttingList.ashx",
                                "type": "POST",
                                "contentType": "application/json; charset=utf-8",
                                "data": function (d) { 
                                    d.searchDate = '';
                                    d.page = cuttingList.PageIndex;
                                    d.length = cuttingList.PageSize;

                                    return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                                }
                            },
                            "columns": [
                                { "data": "no", "orderable": false },
                                { "data": "DateStamp", "orderable": true },
                                { "data": "Code", "orderable": true },
                                { "data": "Category", "orderable": true },
                                { "data": "Product", "orderable": true },
                                { "data": "Amount", "orderable": true },
                                { "data": "Unit", "orderable": true },
                                { "data": "Department", "orderable": true },
                                { "data": "Receiver", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "year", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[11, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9] },
                                { "targets": [10, 11], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<div class="btn-group">' +
                                                    //'<a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="CuttingData.aspx" form-action="view" form-title="การตัดจ่ายครุภัณฑ์"><i class="fa fa-user"></i></a>' +
                                                    '<a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="CuttingData.aspx" form-action="form" form-title="การตัดจ่ายครุภัณฑ์"><i class="fa fa-edit"></i></a>' +
                                                    '<a href="#" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="Are you sure you want to remove this item?"><i class="fa fa-remove"></i></a>' +
                                               '</div>';
                                    },
                                    "targets": 9
                                }
                            ],
                            "drawCallback": function (settings) { 
                                var json = settings.json;

                                cuttingList.PageCount = json.pageCount;

                                var options = '';
                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    options += '<option ' + (cuttingList.PageIndex == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                                }
                                $('.cuttingList #tableData #sltPageIndex').html(options);

                                $('.cuttingList #tableData #spnPageInfo').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (cuttingList.PageIndex + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + cuttingList.PageSize + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');
                            }
                        });
                        // order.dt search.dt
                        dt.on('draw.dt', function () {
                            dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (cuttingList.PageIndex * cuttingList.PageSize) + (i + 1) + '.';
                            });
                            dt.column(9, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var year = dt.cells({ row: i, column: 10 }).data()[0];
                                var id = dt.cells({ row: i, column: 11 }).data()[0];
                                //$(cell).find(".fa-user").parent().attr("year", year).attr("id", id);
                                $(cell).find(".fa-edit").parent().attr("year", year).attr("id", id);
                                $(cell).find(".fa-remove").parent().attr("year", year).attr("id", id);
                            });
                        });
                    },
                    RemoveItem: function (year, id) {
                        $.ajax({
                            type: "POST",
                            url: "CuttingData.aspx/RemoveItem",
                            data: '{year: ' + year + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: cuttingList.OnSuccessRemove,
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

                                cuttingList.ReloadListData();
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
                        var dt = $('.cuttingList #tableData').DataTable();
                        dt.draw();
                    }
                }

                $(document).ready(function () {

                    // Modal Section
                    $('#modalNotifyConfirmRemove').on('show.bs.modal', function (e) {
                        $title = $(e.relatedTarget).attr('data-title');
                        $(this).find('.modal-title').text($title);
                        $message = $(e.relatedTarget).attr('data-message');
                        $(this).find('.modal-body p').text($message);
                        $year = $(e.relatedTarget).attr('year');
                        $id = $(e.relatedTarget).attr('id');
                        $(this).find('.modal-footer #modalConfirmRemove').attr('year', $year).attr('id', $id);
                    });
                    $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').on('click', function () {

                        $('#modalNotifyConfirmRemove').modal('hide');

                        $("#modalWaitDialog").modal('show');

                        // Remove command
                        cuttingList.RemoveItem($(this).attr('year'), $(this).attr('id'));

                    });

                    // Searching, Pagination event 
                    $('.cuttingList #btnSearch').click(function () {

                        cuttingList.PageIndex = 0;

                        cuttingList.ReloadListData();

                        return false;
                    });

                    $('.cuttingList #tableData #sltPageSize').change(function () {

                        cuttingList.PageSize = parseInt($(".cuttingList #tableData #sltPageSize").children("option:selected").val());
                        cuttingList.PageIndex = 0;

                        cuttingList.ReloadListData();

                        return false;
                    });

                    $('.cuttingList #tableData #sltPageIndex').change(function () {

                        cuttingList.PageIndex = $(".cuttingList #tableData #sltPageIndex").children("option:selected").val();

                        cuttingList.ReloadListData();

                        return false;
                    });

                    $('.cuttingList #tableData #aPrevious').click(function () {

                        if (cuttingList.PageIndex > 0) {
                            cuttingList.PageIndex--;
                            cuttingList.ReloadListData();
                        }

                        return false;
                    });
                    $('.cuttingList #tableData #aNext').click(function () {

                        if (cuttingList.PageIndex < (cuttingList.PageCount - 1)) {
                            cuttingList.PageIndex++;
                            cuttingList.ReloadListData();
                        }

                        return false;
                    });

                    // Datatable Section
                    cuttingList.LoadListData();

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="cuttingForm" style="padding: 15px;">
                <form class="form-padding">
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label
                                for="iptCode">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132002") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <input type="text" class="form-control" id="iptCode" disabled
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132002") %>" required>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="sltCategory"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132003") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <select id="sltCategory" name="sltCategory[]"
                                class="form-control">
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="sltProduct"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502020") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <select id="sltProduct" name="sltProduct[]"
                                class="form-control">
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="iptAmount"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <input type="text" class="form-control" id="iptAmount"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %>" required>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="sltUnit"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601045") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <select id="sltUnit" name="sltUnit[]"
                                class="form-control">
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="sltReceiver"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132006") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <select id="sltReceiver" name="sltReceiver[]"
                                class="form-control">
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="sltDepartment">แผนก/หน่วยงาน</label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <select id="sltDepartment" name="sltDepartment[]"
                                class="form-control">
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label
                                for="sltStatus">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <select id="sltStatus" name="sltStatus[]"
                                class="form-control" required>
                                <option value="1">1</option>
                                <option value="2">2</option>
                            </select>
                        </div>
                    </div>
                    <div class="row text-center">
                        <button id="save" type="submit" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="cancel" type="button"
                            class="btn btn-danger" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var cuttingForm = {
                    GetItem: function (year, id) {
                        $.ajax({
                            type: "POST",
                            url: "CuttingData.aspx/GetItem",
                            data: '{year: ' + year + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: cuttingForm.OnSuccessGet,
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

                                $("#iptCode").val($(this).find("F1").text());
                                $("#sltCategory").val($(this).find("F2").text());
                                $("#sltProduct").val($(this).find("F3").text());
                                $("#iptAmount").val($(this).find("F4").text());
                                $("#sltUnit").val($(this).find("F5").text());
                                $("#sltReceiver").val($(this).find("F6").text());
                                $("#sltDepartment").val($(this).find("F7").text());
                                $("#sltStatus").val($(this).find("F8").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "CuttingData.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: cuttingForm.OnSuccessSave,
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
                                    cuttingForm.ClearSession(function () {
                                        modalForm.hideForm();

                                        cuttingList.ReloadListData();
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
                            url: "CuttingData.aspx/ClearSessionID",
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

                    $(".cuttingForm #save").bind({
                        click: function () {

                            $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                            $('#modalNotifyConfirmSave').modal('show');

                            return false;
                        }
                    });

                    $(".cuttingForm #cancel").bind({
                        click: function () {

                            // Close modal
                            cuttingForm.ClearSession(function () {
                                modalForm.hideForm();
                            });

                            return false;
                        }
                    });

                    $("#sltCategory").change(function () {

                        $("#iptCode").val($("#sltCategory").children("option:selected").attr('code'));

                        initFunction.setDropdownWithCatePara('#sltProduct', 'ProductForCate', $("#sltCategory").children("option:selected").val(), '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132007") %></option>');

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
                        data[1] = $("#iptCode").val();
                        data[2] = $("#sltCategory").children("option:selected").val();
                        data[3] = $("#sltProduct").children("option:selected").val();
                        data[4] = $("#iptAmount").val();
                        data[5] = $("#sltUnit").children("option:selected").val();
                        data[6] = $("#sltReceiver").children("option:selected").val();
                        data[7] = $("#sltDepartment").children("option:selected").val();
                        data[8] = $("#sltStatus").children("option:selected").val();

                        cuttingForm.SaveItem(data);

                    });

                    // Initial data
                    initFunction.setDropdownWithCode('#sltCategory', 'Category', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132009") %></option>');
                    initFunction.setDropdown('#sltProduct', 'Product', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132007") %></option>');
                    initFunction.setDropdown('#sltUnit', 'Unit', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601049") %></option>');
                    initFunction.setDropdown('#sltReceiver', 'Employee', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132010") %></option>');
                    initFunction.setDropdown('#sltDepartment', 'Department', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132011") %></option>');

                    // Load info command
                    cuttingForm.GetItem(<%=Request.QueryString["year"]%>, <%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            <div class="cuttingView" style="padding: 15px;">
                <form class="form-padding">
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label
                                for="pType">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <p id="pType"></p>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="pName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131083") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <p id="pName"></p>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="pDetail"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <p id="pDetail"></p>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="pAdmin"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131084") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <p id="pAdmin"></p>
                        </div>
                    </div>
                    <div class="row text-center">
                        <button id="close" type="button"
                            class="btn btn-danger" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var cuttingView = {
                    ViewItem: function (id) {
                        $.ajax({
                            type: "POST",
                            url: "CuttingData.aspx/ViewItem",
                            data: '{id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: cuttingView.OnSuccessGet,
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

                                $("#pType").html($(this).find("F1").text());
                                $("#pName").html($(this).find("F2").text());
                                $("#pDetail").html($(this).find("F3").text().replace(/\n/g, "<br />"));
                                $("#pAdmin").html($(this).find("F4").text());

                            });

                        }
                    }
                }

                $(document).ready(function () {

                    // Initial data


                    // Load info command
                    cuttingView.ViewItem(<%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
    </asp:MultiView>
</body>
</html>
