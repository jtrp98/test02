<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductData.aspx.cs" Inherits="FingerprintPayment.AssetManagement.ProductData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <div class="productList">
                <div class="row">
                    <div class="col-md-12 mb-12 text-left" style="padding-left: 1%;">
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132017") %></span> 
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
                            <th style="width: 17%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132002") %></th>
                            <th style="width: 17%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132003") %></th>
                            <th style="width: 17%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132018") %></th>
                            <th style="width: 17%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502020") %></th>
                            <th style="width: 17%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601045") %></th>
                            <th style="width: 10%" class="text-center">
                                <button type="button" class="btn btn-success" data-toggle="modal" data-target="#modalShowForm" form-name="ProductData.aspx" form-action="form" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132019") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></button></th>
                            <th></th>
                        </tr>
                    </thead>

                    <tfoot>
                        <tr>
                            <th colspan="8">
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

                var productList = {
                    PageIndex: 0,
                    PageSize: 20,
                    PageCount: 0,
                    LoadListData: function () {
                        var dt = $(".productList #tableData").DataTable({
                            "processing": true,
                            "serverSide": true,
                            "info": false,
                            "searching": false,
                            "paging": false,
                            "stateSave": true,
                            "ajax": {
                                "url": "Ashx/LoadProductList.ashx",
                                "type": "POST",
                                "contentType": "application/json; charset=utf-8",
                                "data": function (d) { 
                                    d.searchCat = '';
                                    d.searchProd = '';
                                    d.page = productList.PageIndex;
                                    d.length = productList.PageSize;

                                    return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                                }
                            },
                            "columns": [
                                { "data": "no", "orderable": false },
                                { "data": "Code", "orderable": true },
                                { "data": "Category", "orderable": true },
                                { "data": "Type", "orderable": true },
                                { "data": "Product", "orderable": true },
                                { "data": "Unit", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[5, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6] },
                                { "targets": [7], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<div class="btn-group">' +
                                                    //'<a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="ProductData.aspx" form-action="view" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132019") %>"><i class="fa fa-user"></i></a>' +
                                                    '<a href="#" data-toggle="modal" data-target="#modalShowForm" form-name="ProductData.aspx" form-action="form" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132019") %>"><i class="fa fa-edit"></i></a>' +
                                                    '<a href="#" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="Are you sure you want to remove this item?"><i class="fa fa-remove"></i></a>' +
                                               '</div>';
                                    },
                                    "targets": 6
                                }
                            ],
                            "drawCallback": function (settings) { 
                                var json = settings.json;

                                productList.PageCount = json.pageCount;

                                var options = '';
                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    options += '<option ' + (productList.PageIndex == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                                }
                                $('.productList #tableData #sltPageIndex').html(options);

                                $('.productList #tableData #spnPageInfo').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (productList.PageIndex + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + productList.PageSize + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');
                            }
                        });
                        // order.dt search.dt
                        dt.on('draw.dt', function () {
                            dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (productList.PageIndex * productList.PageSize) + (i + 1) + '.';
                            });
                            dt.column(6, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = dt.cells({ row: i, column: 7 }).data()[0];
                                //$(cell).find(".fa-user").parent().attr("id", id);
                                $(cell).find(".fa-edit").parent().attr("id", id);
                                $(cell).find(".fa-remove").parent().attr("id", id);
                            });
                        });
                    },
                    RemoveItem: function (id) {
                        $.ajax({
                            type: "POST",
                            url: "ProductData.aspx/RemoveItem",
                            data: '{id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: productList.OnSuccessRemove,
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

                                productList.ReloadListData();
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
                        var dt = $('.productList #tableData').DataTable();
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
                        productList.RemoveItem($(this).attr('id'));

                    });

                    // Searching, Pagination event 
                    $('.productList #btnSearch').click(function () {

                        productList.PageIndex = 0;

                        productList.ReloadListData();

                        return false;
                    });

                    $('.productList #tableData #sltPageSize').change(function () {

                        productList.PageSize = parseInt($(".productList #tableData #sltPageSize").children("option:selected").val());
                        productList.PageIndex = 0;

                        productList.ReloadListData();

                        return false;
                    });

                    $('.productList #tableData #sltPageIndex').change(function () {

                        productList.PageIndex = $(".productList #tableData #sltPageIndex").children("option:selected").val();

                        productList.ReloadListData();

                        return false;
                    });

                    $('.productList #tableData #aPrevious').click(function () {

                        if (productList.PageIndex > 0) {
                            productList.PageIndex--;
                            productList.ReloadListData();
                        }

                        return false;
                    });
                    $('.productList #tableData #aNext').click(function () {

                        if (productList.PageIndex < (productList.PageCount - 1)) {
                            productList.PageIndex++;
                            productList.ReloadListData();
                        }

                        return false;
                    });

                    // Datatable Section
                    productList.LoadListData();

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="productForm" style="padding: 15px;">
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
                            <label for="iptType"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132016") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <input type="text" class="form-control" id="iptType"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132016") %>" required>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="iptProduct"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502020") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <input type="text" class="form-control" id="iptProduct"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502020") %>" required>
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
                    <div class="row text-center">
                        <button id="save" type="submit" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="cancel" type="button"
                            class="btn btn-danger" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var productForm = {
                    GetItem: function (id) {
                        $.ajax({
                            type: "POST",
                            url: "ProductData.aspx/GetItem",
                            data: '{id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: productForm.OnSuccessGet,
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
                                $("#iptType").val($(this).find("F3").text());
                                $("#iptProduct").val($(this).find("F4").text());
                                $("#sltUnit").val($(this).find("F5").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "ProductData.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: productForm.OnSuccessSave,
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
                                    productForm.ClearSession(function () {
                                        modalForm.hideForm();

                                        productList.ReloadListData();
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
                            url: "ProductData.aspx/ClearSessionID",
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

                    $(".productForm #save").bind({
                        click: function () {

                            $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                            $('#modalNotifyConfirmSave').modal('show');

                            return false;
                        }
                    });

                    $(".productForm #cancel").bind({
                        click: function () {

                            // Close modal
                            productForm.ClearSession(function () {
                                modalForm.hideForm();
                            });

                            return false;
                        }
                    });

                    $("#sltCategory").change(function () {

                        $("#iptCode").val($("#sltCategory").children("option:selected").attr('code'));

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
                        data[3] = $("#iptType").val();
                        data[4] = $("#iptProduct").val();
                        data[5] = $("#sltUnit").children("option:selected").val();

                        productForm.SaveItem(data);

                    });

                    // Initial data
                    initFunction.setDropdownWithCode('#sltCategory', 'Category', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132009") %></option>');
                    initFunction.setDropdown('#sltUnit', 'Unit', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601049") %></option>');

                    // Load info command
                    productForm.GetItem(<%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            <div class="productView" style="padding: 15px;">
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

                var productView = {
                    ViewItem: function (id) {
                        $.ajax({
                            type: "POST",
                            url: "ProductData.aspx/ViewItem",
                            data: '{id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: productView.OnSuccessGet,
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
                    productView.ViewItem(<%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
    </asp:MultiView>
</body>
</html>
