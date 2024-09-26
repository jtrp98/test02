<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="SummaryByStatus.aspx.cs" Inherits="FingerprintPayment.SDQ.Report.SummaryByStatus" %>

<%@ Register Src="~/UserControls/YTLCFilter.ascx" TagPrefix="uc1" TagName="YTLCFilter" %>
<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />
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

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/2.0.1/js/dataTables.buttons.min.js"></script>
    <%--    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/1.6.4/js/buttons.flash.min.js"></script>--%>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/2.0.1/js/buttons.html5.min.js"></script>
    <%-- <script src="Js/buttons.html5.js"></script>--%>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/2.0.1/js/buttons.print.min.js"></script>

    <script>

        function SearchData(t) {
            if (!$("#aspnetForm").valid()) {
                return;
            }

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
                        url: "/SDQ/Report/SummaryByStatus.aspx/LoadData",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        "dataSrc": function (r) {
                            return r.d.data;
                        },
                        'data': function (d) {

                            d.search = {
                                'term': YTLCF.GetTermID(),
                                'level1': YTLCF.GetLevelID(),
                                'level2': YTLCF.GetClassID(),
                                'type': $('#SDQType').val(),
                            };

                            return JSON.stringify(d);
                        },
                    },
                    "ordering": false,
                    //"columnDefs": [{
                    //    "targets": 0,
                    //    "orderable": false
                    //}],
                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304049") %>', data: 'Name', "class": "text-center", "width": "7%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304050") %>", data: 'Count1', "class": "text-center", "width": "10%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304051") %>", data: 'Count2', "class": "text-center", "width": "10%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304052") %>", data: 'Count3', "width": "15%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304053") %>", data: 'Count4', "width": "15%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304054") %>", data: 'Count5', "width": "15%", "class": "text-center" },
                    ],
                    //"order": [[0, 'asc']],
                    "fnInitComplete": function (oSettings, json) {

                        var f = json.d.summary;
                        var $foot = $("#template1 tfoot");
                        $foot.find('th:eq(1)').text(f.Count1);
                        $foot.find('th:eq(2)').text(f.Count2);
                        $foot.find('th:eq(3)').text(f.Count3);
                        $foot.find('th:eq(4)').text(f.Count4);
                        $foot.find('th:eq(5)').text(f.Count5);

                        $('.result-wrapper').show();
                    },
                });

            }
            else if (t == 'excel' || t == 'excelAll') {

                if (t == 'excel') {

                    var search = {
                        'yearNo': YTLCF.GetYearNo(),
                        'term': YTLCF.GetTermID(),
                        'termNo': YTLCF.GetTermNo(),
                        'level1': YTLCF.GetLevelID(),
                        'level2': YTLCF.GetClassID(),
                        'type': $('#SDQType').val(),
                    };

                    xhr = new XMLHttpRequest();

                    xhr.open("POST", "/SDQ/Report/SummaryByStatus.aspx/ExportExcel", true);
                    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                    xhr.responseType = 'blob';
                    xhr.onload = function () {
                        var dt = new Date();
                        var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306026") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';
                        //aa = xhr.getResponseHeader("filename");
                        saveAs(xhr.response, file_name);
                        //$("body").mLoading('hide');
                    };
                    xhr.send(JSON.stringify({ 'search': search }));
                }
               
            }
            else if (t == 'pdf'  || t == 'pdfAll') {
                
                if (t == 'pdfAll') {

                    var search = {
                        'yearNo': YTLCF.GetYearNo(),
                        'term': YTLCF.GetTermID(),
                        'termNo': YTLCF.GetTermNo(),
                        'type': $('#SDQType').val(),
                    };

                    xhr = new XMLHttpRequest();

                    xhr.open("POST", "/SDQ/Report/SummaryByStatus.aspx/ExportPDF", true);
                    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                    xhr.responseType = 'blob';
                    xhr.onload = function () {
                        var dt = new Date();
                        var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306026") %>_' + dt.format("ddMMyyyyHHmmssss") + '.pdf';
                        //aa = xhr.getResponseHeader("filename");
                        saveAs(xhr.response, file_name);
                        //$("body").mLoading('hide');
                    };
                    xhr.send(JSON.stringify({ 'search': search }));
                }
               
            }
        }

        $(function () {

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
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304048") %>
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

                        <uc1:YTLCFilter runat="server" ID="YTLCFilter"  />

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304045") %></label>
                            <div class="col-sm-3">
                                 <select id="SDQType" class="selectpicker" data-width="100%" data-size="7" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304046") %>">
                                    <option value="1" selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210024") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %></option>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3">
                               
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

                                <div class="dropdown d-inline">
                                    <button class="btn btn-success dropdown-toggle success " type="button" data-toggle="dropdown">
                                        <span class="material-icons">receipt_long
                                        </span>
                                        Export <span class="caret" />
                                    </button>
                                    <ul class="dropdown-menu" style="font-size: 24px">
                                        <li>
                                            <a href="#"  onclick="SearchData('excel')">Excel</a>
                                        </li>
                                        <li>
                                            <a href="#" onclick="SearchData('pdfAll')">PDF (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305014") %>)</a>
                                        </li>
                                    </ul>
                                </div>

                               <%--   <div class="btn btn-info pull-rightx" id="exportfile" onclick="SearchData('excel')">
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
                            <span class="material-icons">list_alt</span>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306026") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12 result-wrapper" style="display:none">
                                <table id="template1" class=" table-hover dataTable" width="100%" style="margin: 0 5px;">
                                     <thead>
                                        <tr>
                                            <th style="width: 10%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304049") %></th>
                                            <th style="width: 15%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304050") %></th>
                                            <th style="width: 15%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304051") %></th>
                                            <th style="width: 15%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304052") %></th>
                                            <th style="width: 15%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304053") %></th>
                                            <th style="width: 15%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304054") %></th>
                                        </tr>
                                    </thead>

                                    <tfoot>
                                        <tr>
                                            <th style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304021") %></th>

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

                        <div class="row">
                            <div class="col-md-12" id="result-wrapper">
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </form>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
