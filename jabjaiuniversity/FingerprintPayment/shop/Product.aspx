<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="Product.aspx.cs" Inherits="FingerprintPayment.Product" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Head" runat="server">
    <%--  <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <style>
        label.error {
            color: red;
        }

        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }

        .dataTables_wrapper .btn-group {
            display: none;
        }

        /*       input[type="checkbox"] {
            appearance: none;
            background-color: #fff;
            margin: 0;
            font: inherit;
            color: currentColor;
            width: 1.15em;
            height: 1.15em;
            border: 0.15em solid currentColor;
            border-radius: 0.15em;
            transform: translateY(-0.075em);
        }*/

        /*  .dropdown.bootstrap-select {
            width: 99% !important;
        }*/

        /*   table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }

        .dataTables_wrapper .btn-group {
            display: none;
        }
*/
        /*  .dataTable tfoot td {
            border-bottom: 2px solid #000;
        }*/
        /*table.dataTable thead .sorting_asc,
        table.dataTable thead .sorting_desc,
        table.dataTable thead .sorting {
            background-image: url('') !important;
        }
        .dataTables_wrapper .dataTables_paginate .paginate_button:hover {
            background: #fff !important;
            border: 0px !important;
        }*/
    </style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Script" runat="server">

    <script src="/app/Product/ProductJS.js?v=1.1" type="text/javascript"></script>
    <script src="/Scripts/jscommon.js" type="text/javascript"></script>
    <script type="text/javascript" src="/StudentInfo/Scripts/circle-progress.js"></script>    
      <script src="//cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>
    <script type="text/javascript">
        var shop_id = getUrlParameter("shop_id");
        function SearchData(t) {
            //if (!$("#aspnetForm").valid()) {
            //    return;
            //}

            if (t == 'data') {

                var dt = $('#template1').DataTable({
                    "processing": true,
                    //"serverSide": true,
                    "destroy": true,
                    "info": false,
                    paging: true,
                    "pageLength": 20,
                    searching: false,
                    "lengthChange": false,
                    ajax: {
                        url: "/shop/Product.aspx/returnlist",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        "dataSrc": function (r) {
                            return r.d.data;
                        },
                        'data': function (d) {

                            d.search = {
                                'wording': $("#txtSearch").val(),
                                'product_type': $('#ddlcType').val(),
                                'shop_id': getUrlParameter("shop_id"),
                            };

                            return JSON.stringify(d);
                        },
                    },


                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', data: 'index', "class": "text-center", "width": "5%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106155") %>", data: 'barcode', "class": "text-center", "width": "12%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %>", data: 'product_name', "class": "text-center", "width": "15%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405006") %>", data: 'quantity', "width": "10%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601045") %>", data: 'unit', "width": "10%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601046") %>", data: 'cost', "width": "10%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601047") %>", data: 'price', "width": "10%", "class": "text-center" },
                        {
                            "title": ` <div  class="dropdown pull-right">
     <button class="btn btn-success btn-sm dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
         <i class="material-icons">settings</i>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404028") %>
     <span class="caret"></span>
     </button>
     <ul class="dropdown-menu" aria-labelledby="dropdownMenu1" >
         <li><a onclick="cleardata();" href="#" data-toggle="modal" data-target="#modalpopproduct"><span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></span></a></li>
         <li><a href="#"  data-toggle="modal" data-target="#modalUploadProductData" onclick="$('.row .table').hide() ;$('.row.upload').show();"><span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601055") %></span></a></li>
     </ul>
 </div>`,
                            //"title": `<btn class="btn btn-success" id="btnadd" onclick="cleardata();" data-toggle="modal" data-target="#modalpopproduct"><span><i class="material-icons">add</i>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></span></btn>
                            //<btn class="btn btn-success" data-toggle="modal" data-target="#modalUploadProductData" onclick="$('.row .table').hide() ;$('.row.upload').show();"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601055") %></a>`,
                            data: '', "width": "10%", "class": "text-center", "orderable": false ,
                            "mRender": function (data, type, row) {
                                return `
                            <div  class="dropdown pull-right">
                                <button class="btn btn-success btn-sm dropdown-toggle" type="button" id="dropdownMenu2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                    <i class="material-icons">settings</i>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101022") %>
                                <span class="caret"></span>
                                </button>
                                <ul class="dropdown-menu" aria-labelledby="dropdownMenu2" >
                                    <li><a onclick="getproductdata('${row.product_id}');" href="#" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601057") %></a></li>
                                    <li><a onclick="onRestock('${row.product_id}');" href="#" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601058") %></a></li>
                                    <li><a onclick="deletedata('${row.product_id}');" href="#" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601067") %></a></li>
                                </ul>
                            </div>`;
                            }
                        },

                    ],
                    "order": [[0, 'asc']]
                });

            }
            else if (t == 'report') {
                //var json = JSON.stringify({
                //    //'yearNo': YTLCF.GetYearNo(),
                //    //'term': YTLCF.GetTermID(),
                //    //'termNo': YTLCF.GetTermNo(),
                //    //'level1': YTLCF.GetLevelID(),
                //    //'level2': YTLCF.GetClassID(),
                //    //'name': SAC.GetStudentName(),
                //    //'type': $('#SDQType').val(),
                //});
                //var dt = new Date();
                //var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306021") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                //xhr = new XMLHttpRequest();

                //xhr.open("POST", "/SDQ/Report/ByStudent.aspx/ExportExcel", true);
                //xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                //xhr.responseType = 'blob';
                //xhr.onload = function () {
                //    //aa = xhr.getResponseHeader("filename");
                //    saveAs(xhr.response, file_name);
                //    //$("body").mLoading('hide');
                //};
                //xhr.send(json);
            }
        }

        $(function () {
            $('#txtCountable').mask('0000000');
            if (jQuery.validator) {//.messages

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",
                });

                $("#aspnetForm").validate({  // initialize the plugin

                    errorPlacement: function (error, element) {
                        let _class = element.attr('class');

                        if (_class.includes('--req-append-last')) {
                            error.insertAfter(element.parent());
                        }
                        else {
                            error.insertAfter(element);
                        }
                    }
                });
            }

            $('.datepicker').datetimepicker({
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
                debug: false,
                //autoclose: true,
                //autoclose: true,
                //showOn: "button",
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
            $("#txtSearch").keypress(function (e) {
                if (e.keyCode === 13) {
                    e.preventDefault();
                    SearchData('data');
                }
            });

            $("#chkStock").click(function () {
                $("#txtnWarn").attr("disabled", $(this).prop("checked"))
            });

            $('#txtCountable').on('change keyup', function () {
                var v = +$('#txtCountable').val();
                var vStock = +$('#txtInStock').val();

                if (v < vStock) {
                    var diff = v - vStock;

                    const formattedNumber = new Intl.NumberFormat(undefined, { minimumFractionDigits: 0, maximumFractionDigits: 0 }).format(diff);

                    $('#txtStockDiff').val(formattedNumber);
                } else {
                    $('#txtCountable').val('');
                    $('#txtStockDiff').val('');
                }

            });

            $('#ddlStockNote').on('change', function () {
                if ($(this).val() == 'AO') {
                    $('#txtStockNote').show();
                }
                else {
                    $('#txtStockNote').hide();
                }
            });

            SearchData('data');
        });

        var productId;

        function onRestock(pid) {
            productId = pid;
            $("body").mLoading('show');
            PageMethods.GetProductDetail(pid,
                function (result) {
                    $("body").mLoading('hide');
                    $('#txtDateReStock').val('');
                    $('#txtInStock').val(result.data.QUANTITY);
                    $('#txtCountable').val('');
                    $('#ddlStockNote').val('');
                    $('#ddlStockNote').selectpicker('refresh');
                    $('#modalRestock').modal('show');
                                      
                },
                function (result) {
                    console.log(result);
                });


        }

        function onSaveRestock() {

            if ($('#aspnetForm').valid() == false) {
                return false;
            }

            var data = {
                'ProductID': productId,
                'CheckDate': $("#txtDateReStock").data("DateTimePicker").date().format("MM/DD/YYYY"),
                'InStock': $("#txtInStock").val(),
                'Count': $("#txtCountable").val(),
                'Diff': $("#txtStockDiff").val(),
                'Remark': $("#ddlStockNote").val(),
                'RemarkText': ($("#ddlStockNote").val() == 'AO' ? $("#txtStockNote").val() : ''),
            };

            // console.log(data);
            $("body").mLoading('show');
            PageMethods.SaveRestock(data, function (result) {

                if (result.status == 'success') {
                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                        showConfirmButton: false,
                        timer: 1500,
                    }).then((result) => {
                        window.location.reload();
                    });
                } else {
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                    });
                }
                $("body").mLoading('hide');
                return false;
            });
        }

        function deletedata(product_id) {
            //product_id = id;
            //$('#modalconfirm-delete #modal-delete-header').text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>");
            //$('#modalconfirm-delete #modalconfirm-delete-content').html("<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>");
            //$('#modalconfirm-delete').modal('show');

            Swal.fire({
                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102158") %>',
                //text: "You won't be able to revert this!",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>',
                cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>'
            }).then((result) => {
                if (result.value) {
                    $("body").mLoading('show');
                    PageMethods.delete_data(product_id,
                        function (result) {
                            $("body").mLoading('hide');
                            if (result == "Success") {

                                SearchData('data');

                                Swal.fire({
                                    type: 'success',
                                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                                });


                            }
                        },
                        function (result) {
                            console.log(result);
                        });

                    return true;
                }
            });

            return false;

        }

        //function loaddata() {
        //    var data = { "wording": wording, "pageSize": pageSize, "pageNumber": pageNumber, "product_type": product_type, "shop_id": shop_id }
        //    $('#target').html('<tr><td colspan="7" class="text-center"><i class="fa fa-spin fa-refresh fa-3x"></i><h1>Loading</h1></td></tr>');
        //    $.blockUI({ message: '<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101325") %></h1>' });
        //    PageMethods.returnlist(data,
        //        function (respones) {
        //            respones = $.parseJSON(respones);
        //            temp.PageSetting({ 'pageSize': pageSize, 'pageNumber': pageNumber });
        //            temp.TemplateSetting({ template_id: "#tmpl-mustache", target_name: "#target" });
        //            temp.RenderRows(respones);
        //            $.unblockUI();
        //        },
        //        function (respones) {
        //            console.log(respones);
        //            $.unblockUI();
        //        }
        //    )
        //}
    </script>
      <script src="js/importData.js?d=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
      
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601040") %>
            </p>
        </div>
    </div>

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>

        <div class="row">
            <div class="col-md-12">

                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601005") %> <%= shop.shop_name %></h4>
                    </div>
                    <div class="card-body ">

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603091") %></label>
                            <div class="col-sm-3">
                                <asp:DropDownList class="selectpicker " data-style="select-with-transition" data-width="100%" ID="ddlcType" ClientIDMode="Static" runat="server" data-size="10" data-live-search="true" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" />
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601042") %></label>
                            <div class="col-sm-3">
                                <input type="text" id="txtSearch" class='form-control' placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601042") %>" />
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row mt-2">
                            <div class="col-md-12 text-center">
                                <button type="button" class="btn btn-success" onclick="SearchData('data');">
                                    <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                                <%-- <div class="btn btn-info pull-rightx" id="exportfile" onclick="SearchData('report')">
                                 <span class="btn-label">
                                     <span class="material-icons">receipt_long
                                     </span>
                                 </span>
                                 Export
                             </div>--%>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="card ">
                    <div class="card-header card-header-primary card-header-icon">
                        <div class="card-icon">
                            <span class="material-icons">shopping_cart</span>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601043") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12" style="">
                                <table id="template1" class=" table-hover dataTable" width="100%" style="margin: 0 5px;">
                                    <%-- <thead>
                                        <tr class="headerCell" style="font-weight: bold; font-style: normal; text-decoration: none;">
                                            <th class="center" scope="col" style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th class="center" scope="col" style="width: 20%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106155") %></th>
                                            <th class="center" scope="col" style="width: 20%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %></th>
                                            <th class="center" scope="col" style="width: 12%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405006") %></th>
                                            <th class="center" scope="col" style="width: 12%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603014") %> (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>)</th>
                                            <th class="center" scope="col" style="width: 12%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601047") %></th>
                                            <th class="centertext" scope="col" style="">

                                                <a class="btn btn-success text-white" id="btnadd" onclick="cleardata();" data-toggle="modal" data-target="#modalpopproduct"><i class="material-icons">add</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></a>
                                            </th>
                                        </tr>
                                    </thead>--%>
                                </table>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12" id="result-wrapper">
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <div class="row" style="display: none">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body mt-3">
                        <h4 class="show-history"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102185") %></h4>
                        <div class="mt-3 d-none history-content">
                            <div>11/11/2566 12:00:00 <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133266") %></strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %> <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133267") %></strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> <strong>1111</strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> <strong>1 / 2222</strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133268") %> <strong>1111</strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133269") %></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalRestock" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
            aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content ">
                    <div class="modal-header">
                        <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133270") %></h4>
                    </div>
                    <div class="modal-body product-add-container">
                        <div>

                            <div class="row">
                                <div class="col-1"></div>
                                <label class="col-5 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601059") %>
                                </label>
                                <div class="col-5">
                                    <div class="form-group ">
                                        <input type="text" name="txtDateReStock" id="txtDateReStock" class="datepicker form-control" value="" required />
                                        <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                            <i class="material-icons">event</i>
                                        </span>
                                    </div>
                                </div>
                            </div>

                            <div class="row ">
                                <div class="col-1"></div>
                                <label class="col-5 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601060") %>
                                </label>
                                <div class="col-5">
                                    <input id="txtInStock" name="txtInStock" type="text" class='form-control' placeholder="  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601060") %>" style="pointer-events: none" />
                                </div>
                            </div>

                            <div class="row ">
                                <div class="col-1"></div>
                                <label class="col-5 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601061") %>
                                </label>
                                <div class="col-5">
                                    <input id="txtCountable" name="txtCountable" type="text" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601061") %>" class='form-control' required />
                                </div>
                            </div>

                            <div class="row ">
                                <div class="col-1"></div>
                                <label class="col-5 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601062") %>
                                </label>
                                <div class="col-5">
                                    <input id="txtStockDiff" name="txtStockDiff" type="text" class='form-control' style="pointer-events: none" />
                                </div>
                            </div>

                            <div class="row ">
                                <div class="col-1"></div>
                                <label class="col-5 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601063") %>
                                </label>
                                <div class="col-5">
                                    <select id="ddlStockNote" class="selectpicker --req-append-last" data-style="select-with-transition" data-size="10" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601064") %>" data-width="100%" required >
                                        <option value="AL"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601065") %></option>
                                        <option value="AD"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404053") %></option>
                                        <option value="AU"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601066") %></option>
                                        <option value="AO"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></option>
                                    </select>
                                </div>
                            </div>

                            <div class="row ">
                                <div class="col-1"></div>
                                <label class="col-5 col-form-label text-left">
                                </label>
                                <div class="col-5">
                                    <input id="txtStockNote" name="txtStockNote" type="text" class='form-control' style="display: none" />
                                </div>
                            </div>

                            <%--  <div class="row stockNote">
                                <div class="col-1"></div>
                                <label class="col-4 col-form-label text-left">
                                </label>
                                <div class="col-6">
                                  
                                </div>
                            </div>--%>
                        </div>
                    </div>
                    <div class="modal-footer" style="display: block; text-align: center;">
                        <input type="button" id="btnSaveStock" class="btn btn-success global-btn" style="width: 100px;" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" onclick="onSaveRestock()" />
                        &nbsp;<input type="button" id="btnCancelStock" class="btn btn-danger global-btn" style="width: 100px;" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" data-dismiss="modal" />
                    </div>
                </div>
            </div>
        </div>

        <div id="modalpopproduct" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
            aria-hidden="true">
            <div class="modal-dialog global-modal">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 id="headerpopup"></h4>
                    </div>
                    <div class="modal-body product-add-container" id="modalpopupdata-content">
                        <div id="productpopup">

                            <div class="row">
                                <div class="col-1"></div>
                                <label class="col-4 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106155") %>
                                </label>
                                <div class="col-6">
                                    <input id="productid" name="productid" type="hidden" class='form-control' />
                                    <input id="txtsBarCode" name="txtsBarCode" type="text" class='form-control' />
                                </div>
                            </div>

                            <div class="row ">
                                <div class="col-1"></div>
                                <label class="col-4 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %>
                                </label>
                                <div class="col-6">
                                    <input id="txtsProduct" name="txtsProduct" type="text" class='form-control' />
                                </div>
                            </div>

                            <div class="row ">
                                <div class="col-1"></div>
                                <label class="col-4 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601045") %>
                                </label>
                                <div class="col-6">
                                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="selectpicker" data-style="select-with-transition" data-live-search="true" data-size="10" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601049") %>" data-width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row ">
                                <div class="col-1"></div>
                                <label class="col-4 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603091") %>
                                </label>
                                <div class="col-6">
                                    <asp:DropDownList ID="ddlnType" runat="server" CssClass="selectpicker" data-style="select-with-transition" data-live-search="true" data-size="10" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601050") %>" data-width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row" style="display: none">
                                <div class="col-xs-4">
                                    <label class="pull-right">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603014") %></label>
                                </div>
                                <div class="col-xs-8">
                                    <input id="txtnCost" name="txtnCost" type="text" class='form-control' style="width: 300px;" />
                                </div>
                            </div>

                            <div class="row ">
                                <div class="col-1"></div>
                                <label class="col-4 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601051") %>
                                </label>
                                <div class="col-6">
                                    <input id="txtPrice" name="txtPrice" type="text" class='form-control' />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-1"></div>
                                <label class="col-4 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601052") %>
                                </label>
                                <div class="col-6">
                                    <input id="txtnWarn" name="txtnWarn" type="text" class='form-control' />
                                </div>
                            </div>

                            <div class="row ">
                                <div class="col-1"></div>
                                <div class="col-4">
                                    <label>
                                    </label>
                                </div>
                                <div class="col-6">
                                    <div class="form-check mt-3">
                                        <label class="form-check-label">
                                            <input id="chkStock" class="form-check-input" type="checkbox" value="" />
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01371") %>
                                              <span class="form-check-sign">
                                                  <span class="check"></span>
                                              </span>
                                        </label>
                                    </div>
                                    <%-- <div class="form-check form-check-inline">
                                        <label class="form-check-label">
                                            <asp:CheckBox runat="server" ID="chkStock" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01371") %>" CssClass="mt-4 form-check-label" />
                                        </label>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer" style="display: block; text-align: center;">
                        <input type="submit" id="btnSave" class="btn btn-success global-btn" style="width: 100px;" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                        &nbsp;<input type="button" id="btnCancel" class="btn btn-danger global-btn" style="width: 100px;" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
                    </div>
                </div>
            </div>
        </div>

        <div id="modalUploadProductData" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
            aria-hidden="true" style="margin: 0 auto; top: 5%;">
            <div class="modal-dialog modal-lg">
                <div class="modal-content ">

                    <div class="modal-header" style="padding: 0px; top: 15%; display: block; text-align: center;">
                        <h3>Upload File Excel</h3>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div class="row upload">
                                <div id="prgUpload" class="upload-progress" style="display: none;"></div>
                                <div class="col-xs-12 text-center">
                                    <div style="display: none;">
                                        <input type="file" name="fuUpload" id="fuUpload" onchange="updateFiles()" />
                                    </div>
                                    <div class="drag-zone" align="center">
                                        <div class="file-drag-area">
                                            <div class="file-choose">
                                                <div class="row">
                                                    <div class="col-md-12 ">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601056") %>                                                      
                                                        <input id="btnImport" name="btnImport" type="button" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101287") %>" class="btn btn-success" onclick="$('#fuUpload').click();" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row table">
                                <table id="tblProductData" class="table table-no-bordered table-hover" cellspacing="0" width="100%" class="hide">
                                    <thead>
                                        <tr style="display: table; width: 100%; table-layout: fixed; /* even columns width , fix width of table too*/">
                                            <th style="width: 25%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603091") %></th>
                                            <th style="width: 25%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106155") %></th>
                                            <th style="width: 25%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %></th>
                                            <%--           <th style="width: 74px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603014") %></th>
                                       <th style="width: 74px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601051") %></th>
                                       <th style="width: 74px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></th>
                                       <th style="width: 74px;">นับสต๊อกทุกชิ้น</th>--%>
                                            <th style="width: 25%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></th>
                                        </tr>
                                    </thead>
                                    <tbody style="display: block; height: 250px; overflow: auto; table-layout: fixed; width: 100%;">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer" style="display: block; text-align: center;">
                        <button id="btnCloseModalUpload" type="button" class="btn btn-danger global-btn"
                            data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>

                </div>
            </div>
        </div>
    </form>

</asp:Content>
