<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionData.aspx.cs" Inherits="FingerprintPayment.AssetManagement.TransactionData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <div class="learningCenterList">
                <div class="row">
                    <div class="col-md-4 mb-4 text-right" style="padding-left: 4%;">
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131080") %></span> 
                    </div>
                    <div class="col-md-4 mb-4 text-center">
                        <select id="sltSearchLearningType" name="sltSearchLearningType[]"
                            class="form-control">
                            <option value="">-- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %> --</option>
                            <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131081") %></option>
                            <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131082") %></option> 
                        </select>
                    </div>
                    <div class="col-md-4 mb-4 text-left" style="padding-right: 2%;">
                        <button id="btnSearch" class="btn btn-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
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
                            <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></th>
                            <th style="width: 45%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131083") %></th>
                            <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131084") %></th>
                            <th style="width: 10%" class="text-center">
                                <button type="button" class="btn btn-success" data-toggle="modal" data-target="#modalShowForm" form-name="LearningCenterData.aspx" form-action="form" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131085") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131086") %></button></th>
                            <th></th>
                        </tr>
                    </thead>

                    <tfoot>
                        <tr>
                            <th colspan="6">
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

                var learningCenterList = {
                    PageIndex: 0,
                    PageSize: 20,
                    PageCount: 0,
                    LoadListData: function () {
                        var dt = $(".learningCenterList #tableData").DataTable({
                            "processing": true,
                            "serverSide": true,
                            "info": false,
                            "searching": false,
                            "paging": false,
                            "stateSave": true,
                            "ajax": {
                                "url": "Handles/LearningCenter/LoadLearningCenterList.ashx",
                                "type": "POST",
                                "contentType": "application/json; charset=utf-8",
                                "data": function (d) { 
                                    d.search = '';
                                    d.learningType = $(".learningCenterList #sltSearchLearningType").children("option:selected").val();
                                    d.page = learningCenterList.PageIndex;
                                    d.length = learningCenterList.PageSize;

                                    return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                                }
                            },
                            "columns": [
                                { "data": "no", "orderable": false },
                                { "data": "Type", "orderable": true },
                                { "data": "Name", "orderable": true },
                                { "data": "Admin", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[5, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 3, 4] },
                                { "targets": [5], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<div class="btn-group">' +
                                                    //'<a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="LearningCenterData.aspx" form-action="view" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102036") %>"><i class="fa fa-user"></i></a>' +
                                                    '<a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="LearningCenterData.aspx" form-action="form" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102036") %>"><i class="fa fa-edit"></i></a>' +
                                                    '<a href="#" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="Are you sure you want to remove this item?"><i class="fa fa-remove"></i></a>' +
                                               '</div>';
                                    },
                                    "targets": 4
                                }
                            ],
                            "drawCallback": function (settings) { 
                                var json = settings.json;

                                learningCenterList.PageCount = json.pageCount;

                                var options = '';
                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    options += '<option ' + (learningCenterList.PageIndex == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                                }
                                $('.learningCenterList #tableData #sltPageIndex').html(options);

                                $('.learningCenterList #tableData #spnPageInfo').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (learningCenterList.PageIndex + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + learningCenterList.PageSize + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');
                            }
                        });
                        // order.dt search.dt
                        dt.on('draw.dt', function () {
                            dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (learningCenterList.PageIndex * learningCenterList.PageSize) + (i + 1) + '.';
                            });
                            dt.column(4, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = dt.cells({ row: i, column: 5 }).data()[0];
                                //$(cell).find(".fa-user").parent().attr("id", id);
                                $(cell).find(".fa-edit").parent().attr("id", id);
                                $(cell).find(".fa-remove").parent().attr("id", id);
                            });
                        });
                    },
                    RemoveItem: function (id) {
                        $.ajax({
                            type: "POST",
                            url: "LearningCenterData.aspx/RemoveItem",
                            data: '{id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: learningCenterList.OnSuccessRemove,
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

                                learningCenterList.ReloadListData();
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
                        var dt = $('.learningCenterList #tableData').DataTable();
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
                        $id = $(e.relatedTarget).attr('id');
                        $(this).find('.modal-footer #modalConfirmRemove').attr('id', $id);
                    });
                    $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').on('click', function () {

                        $('#modalNotifyConfirmRemove').modal('hide');

                        $("#modalWaitDialog").modal('show');

                        // Remove command
                        learningCenterList.RemoveItem($(this).attr('id'));

                    });

                    // Searching, Pagination event 
                    $('.learningCenterList #btnSearch').click(function () {

                        learningCenterList.PageIndex = 0;

                        learningCenterList.ReloadListData();

                        return false;
                    });

                    $('.learningCenterList #tableData #sltPageSize').change(function () {

                        learningCenterList.PageSize = parseInt($(".learningCenterList #tableData #sltPageSize").children("option:selected").val());
                        learningCenterList.PageIndex = 0;

                        learningCenterList.ReloadListData();

                        return false;
                    });

                    $('.learningCenterList #tableData #sltPageIndex').change(function () {

                        learningCenterList.PageIndex = $(".learningCenterList #tableData #sltPageIndex").children("option:selected").val();

                        learningCenterList.ReloadListData();

                        return false;
                    });

                    $('.learningCenterList #tableData #aPrevious').click(function () {

                        if (learningCenterList.PageIndex > 0) {
                            learningCenterList.PageIndex--;
                            learningCenterList.ReloadListData();
                        }

                        return false;
                    });
                    $('.learningCenterList #tableData #aNext').click(function () {

                        if (learningCenterList.PageIndex < (learningCenterList.PageCount - 1)) {
                            learningCenterList.PageIndex++;
                            learningCenterList.ReloadListData();
                        }

                        return false;
                    });

                    // Datatable Section
                    learningCenterList.LoadListData();

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="learningCenterForm" style="padding: 15px;">
                <form class="form-padding">
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label
                                for="sltType">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <select id="sltType" name="sltType[]"
                                class="form-control" required>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131081") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131082") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="iptName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131083") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <input type="text" class="form-control" id="iptName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131083") %>" required>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="iptDetail"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <textarea class="form-control" rows="3" id="iptDetail" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %>" required></textarea>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="iptAdmin"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131084") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <input type="text" class="form-control" id="iptAdmin"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131084") %>" required>
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

                var learningCenterForm = {
                    GetItem: function (id) {
                        $.ajax({
                            type: "POST",
                            url: "LearningCenterData.aspx/GetItem",
                            data: '{id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: learningCenterForm.OnSuccessGet,
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

                                $("#sltType").val($(this).find("F1").text());
                                $("#iptName").val($(this).find("F2").text());
                                $("#iptDetail").val($(this).find("F3").text());
                                $("#iptAdmin").val($(this).find("F4").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "LearningCenterData.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: learningCenterForm.OnSuccessSave,
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
                                    learningCenterForm.ClearSession(function () {
                                        modalForm.hideForm();

                                        learningCenterList.ReloadListData();
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
                            url: "LearningCenterData.aspx/ClearSessionID",
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

                    $(".learningCenterForm #save").bind({
                        click: function () {

                            $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                            $('#modalNotifyConfirmSave').modal('show');

                            return false;
                        }
                    });

                    $(".learningCenterForm #cancel").bind({
                        click: function () {

                            // Close modal
                            learningCenterForm.ClearSession(function () {
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
                        data[1] = $("#sltType").children("option:selected").val();
                        data[2] = $("#iptName").val();
                        data[3] = $("#iptDetail").val();
                        data[4] = $("#iptAdmin").val();

                        learningCenterForm.SaveItem(data);

                    });

                    // Initial data


                    // Load info command
                    learningCenterForm.GetItem(<%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            <div class="learningCenterView" style="padding: 15px;">
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

                var learningCenterView = {
                    ViewItem: function (id) {
                        $.ajax({
                            type: "POST",
                            url: "LearningCenterData.aspx/ViewItem",
                            data: '{id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: learningCenterView.OnSuccessGet,
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
                    learningCenterView.ViewItem(<%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
    </asp:MultiView>
</body>
</html>
