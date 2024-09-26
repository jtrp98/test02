<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="StockList.aspx.cs" Inherits="FingerprintPayment.StockList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <%-- <script src="/Scripts/jquery-1.12.4.js" type="text/javascript"></script>--%>

    <%--    <script type="text/javascript" src="/Scripts/jquery.ui.1.8.10.js"></script>--%>
    <%--    <script src="/Scripts/mustache.js" type="text/javascript"></script>--%>

    <style>
        .show-history {
            cursor: pointer;
        }

        #table-stock_wrapper .col-sm-12 {
            padding: 0 !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script src="/Scripts/jscommon.js" type="text/javascript"></script>

    <script type="text/javascript">
       
        $(document).ready(function () {

            SearchData('data');
           

            $(".show-history").click(function () {
                if ($(".history-content").hasClass("d-none")) {
                    $(".history-content").removeClass("d-none")
                } else {
                    $(".history-content").addClass("d-none")
                }
            })
        })

        function onRemove(id) {

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
                    PageMethods.RemoveByID(id,
                        function (result) {
                            $("body").mLoading('hide');
                            if (result.text == "success") {

                                Swal.fire({
                                    type: 'success',
                                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                                });

                                window.location.reload();
                            }
                        },
                        function (result) {
                            console.log(result);
                        })

                    return true;
                }
            });

            return false;
        }

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
                        url: "/shop/StockList.aspx/returnlist",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        "dataSrc": function (r) {
                            return r.d.data;
                        },
                        'data': function (d) {

                            d.search = {
                                //'wording': $("#txtSearch").val(),
                                //'product_type': $('#ddlcType').val(),
                                'shop_id': getUrlParameter("shop_id"),
                            };

                            return JSON.stringify(d);
                        },
                    },

                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', data: 'index', "class": "text-center", "width": "5%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601070") %>", data: 'docNo', "class": "text-center", "width": "10%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601071") %>", data: 'addDate', "class": "text-center", "width": "10%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303015") %>", data: 'by', "width": "10%", "class": "text-center" },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>", data: '', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {

                                if (row.IsDel == true) {
                                    return '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>';
                                }
                                else  if (row.isDone == true) {
                                    return '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102233") %>';
                                }
                                else {
                                    return '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601083") %>';
                                }
                            }
                        },
                        {
                            "title": `<a class="btn btn-success" id="btnadd" target='_blank' href='/shop/StockAdd.aspx?shop_id=${getUrlParameter("shop_id")}'><span><i class="material-icons">add</i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></span></a>`,
                            data: '', "width": "15%", "class": "text-center", "orderable": false ,
                            "mRender": function (data, type, row) {
                                var s = `<a class="btn btn-info btn-sm" target="_blank" href="/shop/StockDetail.aspx?id=${row.id}" ><i class="material-icons">search</i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601086") %></a>`;

                                if (row.IsDel != true && row.isDone != true) {
                                    s += `<a class="btn btn-success  btn-sm"  href="/shop/StockEdit.aspx?id=${row.id}" ><i class="material-icons">edit</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></a>`;
                                }

                                if (row.IsDel != true) {
                                    s += `<a href='#' class="btn btn-danger  btn-sm"  onclick="onRemove('${row.id}')" ><i class="material-icons">delete</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></a>`;
                                }

                                return s;                       
                            }
                        },

                    ],
                    //"order": [[0, 'asc']]
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
        //.... Add Stock
        var table_stock = [];
        let stock_id = 0;

        $(document).ready(function () {
        

        });

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="aspnetForm" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release" />

        <div class="row">
            <div class="col-md-12">
                <p class="text-muted" style="font-size: small;">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601069") %>
                </p>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header card-header-success card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">shopping_cart</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601005") %> <%= shop.shop_name %></h4>
                    </div>
                    <div class="card-body">
                        <table id="template1" class=" table-hover dataTable" width="100%" style="margin: 0 5px;">
                        </table>                     
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card card1">
                    <div class="card-body mt-3">
                        <h4 class="show-history"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102185") %></h4>
                        <div class="mt-3 d-none history-content">
                           <%-- <div>11/11/2566 12:00:00 <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133266") %></strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %> <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133267") %></strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> <strong>1111</strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> <strong>1 / 2222</strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133268") %> <strong>1111</strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133269") %></div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
                
    </form>
</asp:Content>
