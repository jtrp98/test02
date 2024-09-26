<%@ Page Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ProductBalanceReportNew.aspx.cs" Inherits="FingerprintPayment.Report.ProductBalanceReportNew" %>

<asp:Content ID="Content5" ContentPlaceHolderID="Head" runat="server">
    <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <style>
        /*  .dropdown.bootstrap-select {
         width: 99% !important;
     }*/

        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }

        .dataTables_wrapper .btn-group {
            display: none;
        }

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

<asp:Content ID="Content6" ContentPlaceHolderID="Script" runat="server">

    <script>

        $(function () {


            $("#ddlShop").change(function () {
                PageMethods.GetProductType($("#ddlShop").val(), function (response) {
                    //console.log(e.data);
                    $("#ddlProdType option").remove();
                    $("#ddlProdType").append($("<option></option>")
                        .attr("value", "")
                        .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"));
                    $.each(response, function (e, s) {
                        $("#ddlProdType").append($("<option></option>")
                            .attr("value", s.TypeId)
                            .text(s.TypeName));
                    })
                    $("#ddlProdType").selectpicker('refresh');
                });
            }); /*<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133156") %>*/

        });

        function SearchData(t) {
            if (!$("#aspnetForm").valid()) {
                return;
            }

            if (t == 'data') {
              //  $("body").mLoading();

                var $table = $('#template1').DataTable({
                    "processing": true,
                    "destroy": true,
                    "info": false,
                    paging: true,
                    "pageLength": 50,
                    "bLengthChange": false,
                    searching: false,

                    ajax: {
                        url: "ProductBalanceReportNew.aspx/GetData",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        "dataSrc": function (r) {
                            return r.d.data;
                        },
                        'data': function (d) {

                            d.search = {
                                'ShopId': $('#ddlShop').val(),
                                'TypeId': $('#ddlProdType').val(),
                                'Name': $('#txtProdName').val(),
                            };

                            return JSON.stringify(d);
                        },
                    },

                    columns: [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', "data": "No", "width": "5%", "class": "text-center" },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601005") %>', "data": "ShopName", "width": "8%", "class": "text-center" },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106155") %>', "data": "BarCode", "width": "8%", "class": "text-center" },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %>', "data": "ProductName", "width": "8%", "class": "text-left" },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603091") %>', "data": "Type", "width": "8%", "class": "text-center" },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133157") %>', "data": "Quantity", "width": "8%", "class": "text-center" },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603014") %><br><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133159") %>', "data": "Cost", "width": "8%", "class": "text-center" },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601051") %><br><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133159") %>', "data": "Price", "width": "8%", "class": "text-center" },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133158") %>', "data": "SumValue", "width": "8%", "class": "text-center" },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br>', "data": "Status", "width": "8%", "class": "text-center" }
                    ],
                    "order": [[0, 'asc']],

                    "fnInitComplete": function (oSettings, json) {                                            
                        var f = json.d.summary;
                        var $foot = $("#template1 tfoot");
                        $foot.find('th:eq(2)').text(f.Balance);
                        $foot.find('th:eq(3)').text(f.Cost);
                        $foot.find('th:eq(4)').text(f.Price);
                        $foot.find('th:eq(5)').text(f.SumValue);
                        $foot.show();
                    },
                });

            }
            else if (t == 'report') {
              
                var json = ({                   
                    'ShopId': $('#ddlShop').val(),
                    'TypeId': $('#ddlProdType').val(),
                    'Name': $('#txtProdName').val(),                    
                });

                var dt = new Date();
                var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603088") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                xhr = new XMLHttpRequest();

                xhr.open("POST", "/Report/ProductBalanceReportNew.aspx/GetExcel", true);
                xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                xhr.responseType = 'blob';
                xhr.onload = function () {
                    //aa = xhr.getResponseHeader("filename");
                    saveAs(xhr.response, file_name);
                    //$("body").mLoading('hide');
                };
                xhr.send(JSON.stringify({ 'search': json }));
                //$('.dataTables_wrapper .buttons-excel').click();
                //var url = "Handles/DataEmpTraining_Handler.ashx?c=report&emp=" + $("#txtid").val() + "&type=" + $("#type").val() + "&dstart=" + dStart + "&dend=" + dEnd;
                //window.open(url);
            }
        }
    </script>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133160") %> > Inventory report
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
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                    </div>
                    <div class="card-body ">

                        <div class="row ">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601003") %>/<br />
                                Report type:
                            </label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlShop" ClientIDMode="Static" runat="server" class='selectpicker' data-width="100%" data-style="select-with-transition">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                            </label>
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row ">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603091") %>/<br />
                                Product type:
                            </label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlProdType" ClientIDMode="Static" runat="server" class='selectpicker' data-width="100%" data-style="select-with-transition">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %>/<br />
                                Product name:
                            </label>
                            <div class="col-sm-3">
                                <input type="text" id="txtProdName" name="txtProdName" class="form-control" value="">
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
                                <div class="btn btn-info pull-rightx" id="exportfile" onclick="SearchData('report')">
                                    <span class="btn-label">
                                        <span class="material-icons">receipt_long
                                        </span>
                                    </span>
                                    Export
                                </div>
                            </div>
                        </div>

                    </div>
                </div>


                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <span class="material-icons">list_alt</span>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102145") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12" >
                                <table id="template1" class=" table-hover dataTable" width="100%" style="margin: 0 5px;">
                                    <thead>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                    <tfoot style="display:none">
                                        <tr>
                                            <th style="text-align: center"></th>
                                            <th colspan="4" style="text-align: left !important"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133161") %></th>
                                            <th style="text-align: center"></th>
                                            <th style="text-align: center"></th>
                                            <th style="text-align: center"></th>
                                            <th style="text-align: center"></th>
                                            <th style="text-align: center"></th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </form>
</asp:Content>
