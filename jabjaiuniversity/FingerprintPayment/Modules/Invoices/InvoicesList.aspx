<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="InvoicesList.aspx.cs" Inherits="FingerprintPayment.Modules.Invoices.InvoicesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        #tableData tr td ul.dropdown-menu li {
            white-space: nowrap;
        }

        #tableData tr td .btn-group, #tableData tr td .btn-group-vertical {
            position: relative;
            margin: 3px 1px;
        }

        .material-list .dropdown-toggle .filter-option-inner-inner {
            font-size: 14px;
        }

        label.col-form-label {
            color: #AAAAAA;
        }

        table.dataTable thead .sorting:before, table.dataTable thead .sorting_asc:before, table.dataTable thead .sorting_desc:before, table.dataTable thead .sorting_asc_disabled:before, table.dataTable thead .sorting_desc_disabled:before, table.dataTable thead .sorting:after, table.dataTable thead .sorting_asc:after, table.dataTable thead .sorting_desc:after, table.dataTable thead .sorting_asc_disabled:after, table.dataTable thead .sorting_desc_disabled:after {
            top: 7%;
        }
        .modal-content.modal-800 {
    width:1050px;
    margin: 30px auto;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803041") %> &rsaquo; 
            </p>
        </div>
    </div>    
    <!-- end row -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">newspaper</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803041") %></h4>
                </div>
                <div class="card-body">
                    <div class="toolbar">
                        <!-- Here you can write extra buttons/actions for the toolbar -->
                    </div>
                    <div class="material-datatables">
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
                                <div class="col-md-11 mr-auto text-right">
                                    <button id="btnAdd" class="btn btn-info" onclick="OpenSetting()">
                                        <span class="btn-label">
                                            <i class="material-icons"></i>
                                        </span>
                                       <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132495") %>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <table id="tableData" class="table table-no-bordered" cellspacing="0" width="100%" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></th>
                                              <%--  <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404034") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501013") %></th>--%>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132496") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132497") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132498") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132499") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103165") %></th>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ModalPopup" runat="server">

    <!-- Modal -->
    <div class="modal fade modal-font-size-default" role="dialog" id="modal-invoice-setting" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog  modal-lg">
             <div class="modal-content modal-800">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132502") %></h4>
                </div>
                <div class="modal-body">   
                    <div class="row">
                        <div class="col-md-12 col-xs-12">  
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>        
                                <select id="ddlStaffs" name="ddlStaffs" class="form-control select2" style="width: 50%" required>
                                    <option value="">-- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132500") %> --</option>                                                        
                                </select>

                                 <button class="btn btn-info" onclick="AddUserNotification()">                               
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132501") %>
                                </button>
                            </div>    
                    </div>

                    <hr />

                    <div class="row">
                        <div class="col-md-12">
                            <div class="material-datatables">
                                <div class="dataTables_wrapper dt-bootstrap4">                                               
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="tableData2" class="table table-no-bordered" cellspacing="0" width="100%" style="width: 100%; font-size:14px">
                                                <thead>
                                                    <tr>
                                                        <th>No</th>
                                                        <th style="min-width:150px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></th>
                                                        <th style="min-width:150px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></th>                                      
                                                        <th style="min-width:150px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02941") %></th>
                                                        <th>LINE Notify</th>   
                                                        <th>Link</th>   
                                                        <th></th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </div>
                                    </div>                                            
                                </div>
                            </div>
                        </div>
                        <!-- end col-md-12 -->
                    </div>                      
                </div>
             </div>             
        </div>
    </div>    

    <div class="modal fade modal-font-size-default" role="dialog" id="modal-connect-line" data-backdrop="static" data-keyboard="false" >
        <div class="modal-dialog">
             <div class="modal-content modal-800">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132502") %></h4>
                </div>
                <div class="modal-body">  
                    
                    <iframe id="connect-line"></iframe> 
                </div>
             </div>  
           </div>
    </div>

    <div class="modal fade modal-font-size-default" role="dialog" id="modal-connect-line-qr" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog  modal-sm">
             <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body">                      
                    <img src="#" id="qrcode" style="width:100%" />        
                    <div class="text-center"> 
                        <p id="url"></p>
                        <a download="qr.png" href="#" id="download_qrcode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M900001") %>QR Code</a>  
                    </div>
                </div>               
             </div>  
           </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <link href="/Content/select2/select2.min.css" rel="stylesheet" />
    <script src="/Scripts/select2/select2.full.min.js"></script>
    <script>        

        var table2;

        var tableInvoiceList = {
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            TotalRows: 0,
            dt: null,
            LoadListData: function () {
                tableInvoiceList.dt = $('#tableData').DataTable({
                    "processing": true,
                    "serverSide": true,
                    "pageLength": 10,
                    "searching": true,
                    "paging": false,
                    "ajax": {
                        "url": "InvoicesList.aspx/LoadHistoryList",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {

                            d.page = tableInvoiceList.PageIndex;
                            d.length = tableInvoiceList.PageSize;

                            return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                        },
                        "dataSrc": function (json) {
                            var jsond = $.parseJSON(json.d);
                            //console.log(jsond);
                            return jsond.data;
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
                        {
                            data: null,
                            className: "tab-1",
                            render: function (data, type, full, meta) {
                                //I want to get row index here somehow
                                return meta.row + 1;
                            }
                        },
                        { "data": "current_year", "orderable": false },
                        { "data": "current_term", "orderable": false },
                        //{ "data": "student_invoice", "orderable": false },
                        //{ "data": "price", "orderable": false },
                        //{ "data": "total_price", "orderable": false },
                        { "data": "create_date", "orderable": false },                        
                        {
                            "render": function (data, type, row) {
                                if (row.invoice_link_url != null) {
                                    return `<a target='_blank' href="` + row.invoice_link_url + `">` + row.invoice_code + `</a>`;
                                } else {
                                    return '';
                                }                            
                            }
                        },
                        { "data": "due_date", "orderable": false },
                        {
                            "render": function (data, type, row) {
                                if (row.reciept_link_url != null) {
                                    return `<a target='_blank' href="` + row.reciept_link_url + `">` + row.reciept_code + `</a>`;
                                } else {
                                    return '';
                                }   
                            }
                        },
                        { "data": "approve_name", "orderable": false }
                    ],
                    "order": [[1, "desc"]]
                });
            },
            RemoveItem: function (sid) {

            },
            OnSuccessRemove: function (response) {

            },
            ReloadListData: function () {
                tableInvoiceList.dt.draw();
            }
        }
       

        $(document).ready(function () {
            $('.select2').select2();

            LoadInvoiceHistory();

            LoadStaffList();

            //$("#btnStaffNotification").bind({
            //    click: function () {

            //        if ($('#formStaffNotification').valid()) {

            //            var data = {
            //                NotificationSettingId: $("#NotificationSettingId").val(),
            //                Position: $("#Position").val(),
            //                FirstName: $("#FirstName").val(),
            //                LastName: $("#LastName").val(),
            //                Email: $("#Email").val(),
            //            };

            //            $.ajax({
            //                url: "InvoicesList.aspx/SaveStaffNotification",
            //                type: 'post',
            //                //dataType: 'json',
            //                contentType: 'application/json;',
            //                data: JSON.stringify({ data: data }),
            //                success: function (response) {
            //                    console.log(response)
            //                    var result = JSON.parse(response.d);  
            //                    if (result.success != true) {
            //                        alert(result.message);
            //                    }
            //                    else {
            //                        $("#modal-add-notify").modal("hide");
            //                        LoadNotificationSetting();
            //                    }
            //                }
            //            });
            //        }

            //        return false;
            //    }
            //});
        });

        function LoadInvoiceHistory() {
            tableInvoiceList.LoadListData();
        }

        function LoadNotificationSetting() {
            //newsList2.LoadListData();
            if (table2 !== undefined) {
                table2.destroy();
            }

            table2 = $('#tableData2').DataTable({
                        "processing": true,
                        "serverSide": true,
                        "pageLength": 10,
                        "searching": true,
                        "paging": false,
                        "ajax": {
                            "url": "InvoicesList.aspx/LoadNotificationSettingList",
                            "type": "POST",
                            "contentType": "application/json; charset=utf-8",
                            "data": function (d) {

                                d.page = tableInvoiceList.PageIndex;
                                d.length = tableInvoiceList.PageSize;

                                return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                            },
                            "dataSrc": function (json) {
                                var jsond = $.parseJSON(json.d);
                                //console.log(jsond);
                                return jsond.data;
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
                            {
                                data: null,
                                className: "tab-1",
                                render: function (data, type, full, meta) {
                                    //I want to get row index here somehow
                                    return meta.row + 1;
                                }
                            },
                            { "data": "Position", "orderable": false, "autoWidth": true },
                            { "data": "FullName", "orderable": false, "autoWidth": true },                         
                            { "data": "Email", "orderable": false, "autoWidth": true },
                            {
                                "render": function (data, type, row) {                                    
                                    if (row.LineNotificationAccessToken != '' && row.LineNotificationAccessToken != null) {
                                        return `<div class="btn-group">
                                                <button class="btn btn-success btn-sm view-data" onclick=DisConnectLine("` + row.NotificationSettingId + `") style="width: auto;">
                                                    หยุดเชื่อมต่อ LINE
                                                </button>                                             
                                            </div>`;
                                    }
                                    else {
                                        return `<div class="btn-group">
                                                <button class="btn btn-default btn-sm view-data" onclick=ConnectLine("` + row.NotificationSettingId + `") style="width: auto;">
                                                    เชื่อมต่อ LINE
                                                </button>
                                            </div>`;
                                    }
                                }
                            },
                            {
                                "render": function (data, type, row) {
                                    return `<div class="btn-group">
                                                <button class="btn btn-info btn-sm view-data" onclick=OpenQr("` + row.NotificationSettingId + `") style="width: auto;">                                                    
                                                    LINE QR
                                                </button>                                             
                                            </div>`;
                                }
                            },
                            {
                                "render": function (data, type, row) {
                                    return `<div class="btn-group">
                                                <button class="btn btn-info btn-sm view-data" onclick=CopyLink("` + row.LineUrlConnect + `") style="width: auto;">                                                    
                                                    Copy Link
                                                </button>                                             
                                            </div>`;
                                }
                            },
                            {
                                "render": function (data, type, row) {
                                    return `<div class="btn-group">                                               
                                                <button class="btn btn-danger btn-sm view-data" onclick=RemoveNotification("` + row.NotificationSettingId + `") style="width: auto;">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                                                </button>
                                            </div> `;
                                }
                            }
                        ],
                        "order": [[1, "desc"]],
                        "columnDefs": [
                            { "width": "200px", "targets": 2 },  
                            { "width": "200px", "targets": 3 }, 
                        ],
            });
        }

        function LoadStaffList() {
            $.ajax({
                type: "POST", url: "InvoicesList.aspx/GetStaffList", dataType: "json", contentType: "application/json", success: function (res) {
                    var items = JSON.parse(res.d)              
                    $.each(items.data, function (data, value) {
                        $("#ddlStaffs").append($("<option></option>").val(value.StaffId).html(value.FullName));
                    });
                }
            });  
        }

        function OpenSetting() {       
           
            $("#modal-invoice-setting").modal("show");
            LoadNotificationSetting();
        }

        function OpenAddNotification() {
            $("#modal-add-notify").modal("show");
        }

        function OpenEditNotification(id) {
           
            $("#modal-add-notify").modal("show");

            var data = {
                NotificationSettingId: id
            };

            $.ajax({
                url: "InvoicesList.aspx/GetStaffByID",
                type: 'post',
                contentType: 'application/json;',
                data: JSON.stringify({ data: data }),
                success: function (response) {  
                    var result = JSON.parse(response.d);                
                    $('#NotificationSettingId').val(result.data.NotificationSettingId).trigger('change');
                    $('#Position').val(result.data.Position).trigger('change');
                    $('#FirstName').val(result.data.FirstName).trigger('change');
                    $('#FirstName').val(result.data.FirstName).trigger('change');
                    $('#Email').val(result.data.Email).trigger('change');     
                }
            });          
        }

        function RemoveNotification(id) {
            if (!confirm('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132503") %>')) return false;

            var data = {
                NotificationSettingId: id
            };

            $.ajax({
                url: "InvoicesList.aspx/RemoveNotification",
                type: 'post',
                contentType: 'application/json;',
                data: JSON.stringify({ data: data }),
                success: function (response) {

                    LoadNotificationSetting();
                }
            });    
        }

        function ConnectLine(id) {
            if (!confirm('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132506") %>')) return false;

            var data = {
                NotificationSettingId: id
            };

            $.ajax({
                url: "InvoicesList.aspx/ConnectLine",
                type: 'post',
                contentType: 'application/json;',
                data: JSON.stringify({ data: data }),
                success: function (response) {
                    console.log(response.d)
                    var result = JSON.parse(response.d);
                    if (result.success) {
                        window.open(result.url, '_blank');
                    }
                    else {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132507") %>');
                    }                    
                }
            });           
        }

        function DisConnectLine(id) {
            if (!confirm('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132504") %>')) return false;

            var data = {
                NotificationSettingId: id
            };

            $.ajax({
                url: "InvoicesList.aspx/DisConnectLine",
                type: 'post',
                contentType: 'application/json;',
                data: JSON.stringify({ data: data }),
                success: function (response) {
                    
                    LoadNotificationSetting();
                }
            });     
        }

        function OpenQr(id) {

            $("#modal-connect-line-qr").modal("show");

            var data = {
                NotificationSettingId: id
            };
           
            $.ajax({
                url: "InvoicesList.aspx/OpenQr",
                type: 'post',
                contentType: 'application/json;',
                data: JSON.stringify({ data: data }),
                success: function (response) {     

                    var result = JSON.parse(response.d); 
                    $(".modal-title").text(result.staff.Position + ' ' + result.staff.FullName);
                    $("#qrcode").attr("src", 'data:image/png;base64,' + result.qr);
                    $("#download_qrcode").attr("href", 'data:image/png;base64,' + result.qr);
                    $("#download_qrcode").attr("download", result.staff.FullName + '.png');
                    //$("#url").text(result.url);
                }
            });    
        }

        function AddUserNotification() {
            var data = {
                StaffId: $('#ddlStaffs').val(),
                FullName: $('#ddlStaffs').text(),
            };

            $.ajax({
                url: "InvoicesList.aspx/AddStaffNotification",
                type: 'post',
                contentType: 'application/json;',
                data: JSON.stringify({ data: data }),
                success: function (response) {
                    LoadNotificationSetting();
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132505") %>');
                }
            });         
        }

        function CopyLink(text) {
            console.log(text)
            navigator.clipboard.writeText(text);
        }
    </script>


    <script src="/Scripts/jquery.validate.js"></script>
    <script src="/Scripts/jquery.validate.unobtrusive.js"></script>
</asp:Content>