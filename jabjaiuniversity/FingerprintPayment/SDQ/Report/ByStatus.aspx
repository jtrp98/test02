<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ByStatus.aspx.cs" Inherits="FingerprintPayment.SDQ.Report.ByStatus" %>

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
    <script src="sdq.js"></script>

    <script>

        var status1 = "<span class='text-success'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></span>";
        var status2 = "<span class='text-warning'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></span>";
        var status3 = "<span class='text-danger'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></span>";

    
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
                    "autoWidth": false,
                    header: true,
                    footer: true,
                    ajax: {
                        url: "/SDQ/Report/ByStatus.aspx/LoadData",
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
                               
                            };

                            return JSON.stringify(d);
                        },
                    },

                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', data: 'Index', "class": "text-center", "width": "8%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>", data: 'Room', "class": "text-center", "width": "15%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %>", data: 'CountAll', "class": "text-center", "width": "11%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304019") %>", data: 'Done1', "class": "text-center", "width": "11%" }, 
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304020") %>", data: 'Not1', "class": "text-center", "width": "11%" }, 
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304019") %>", data: 'Done2', "class": "text-center", "width": "11%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304020") %>", data: 'Not2', "class": "text-center", "width": "11%" }, 
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304019") %>", data: 'Done3', "class": "text-center", "width": "11%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304020") %>", data: 'Not3', "class": "text-center", "width": "11%" }, 
                    ],
                    "order": [[0, 'asc']],
                    "fnInitComplete": function (oSettings, json) {
                        var p = json.d.percent;

                        $('#type1done').text(p.Done1.toFixed(2) + '%');
                        $('#type1not').text(p.Not1.toFixed(2) + '%');

                        $('#type2done').text(p.Done2.toFixed(2) + '%');
                        $('#type2not').text(p.Not2.toFixed(2) + '%');

                        $('#type3done').text(p.Done3.toFixed(2) + '%');
                        $('#type3not').text(p.Not3.toFixed(2) + '%');

                        var f = json.d.summary;
                        var $foot = $("#template1 tfoot");
                        $foot.find('th:eq(1)').text(f.CountAll);
                        $foot.find('th:eq(2)').text(f.Done1);
                        $foot.find('th:eq(3)').text(f.Not1);

                        $foot.find('th:eq(4)').text(f.Done2);
                        $foot.find('th:eq(5)').text(f.Not2);

                        $foot.find('th:eq(6)').text(f.Done3);
                        $foot.find('th:eq(7)').text(f.Not3);

                        $('.result-wrapper').show();
                    },

                
                });
              
            }
            else if (t == 'report') {

                var json = JSON.stringify({
                    'yearNo': YTLCF.GetYearNo(),
                    'term': YTLCF.GetTermID(),
                    'termNo': YTLCF.GetTermNo(),
                    'level1': YTLCF.GetLevelID(),
                    'level2': YTLCF.GetClassID(),                    
                });
                var dt = new Date();
                var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306021") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                xhr = new XMLHttpRequest();

                xhr.open("POST", "/SDQ/Report/ByStatus.aspx/ExportExcel", true);
                xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                xhr.responseType = 'blob';
                xhr.onload = function () {
                    //aa = xhr.getResponseHeader("filename");
                    saveAs(xhr.response, file_name);
                    //$("body").mLoading('hide');
                };
                xhr.send(json);

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
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304014") %>
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

                        <uc1:YTLCFilter runat="server" ID="YTLCFilter" IsLevelRequired="true" />

                      <%--  <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                            <div class="col-sm-3">
                                <uc1:StudentAutocomplete runat="server" ID="StudentAutocomplete" />
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304045") %></label>
                            <div class="col-sm-3">
                                <select id="SDQType" class="selectpicker" data-width="100%" data-size="7" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304046") %>">
                                    <option value="1" selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210024") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %></option>
                                </select>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>--%>

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
                    <div class="card-header card-header-primary card-header-icon">
                        <div class="card-icon">
                            <span class="material-icons">list_alt</span>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306008") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12 result-wrapper" style="display:none">
                                <table id="template1" class=" table-hover dataTable" width="100%" >

                                     <thead>
                                         <tr>
                                            <th style="text-align:center ;" rowspan="2"  ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th style="text-align:center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></th>
                                            <th sstyle="text-align:center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %></th>
                                            <th colspan="2" style="text-align:center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></th>
                                            <th colspan="2" style="text-align:center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210024") %></th>
                                            <th colspan="2" style="text-align:center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %></th>
                                         </tr>
                                        <tr>
                                           
                                            <th style="text-align:center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304019") %></th>
                                            <th style="text-align:center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304020") %></th>
                                            <th style="text-align:center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304019") %></th>
                                            <th style="text-align:center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304020") %></th>
                                            <th style="text-align:center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304019") %></th>
                                            <th style="text-align:center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304020") %></th>
                                        </tr>
                                    </thead>

                                    <tbody></tbody>

                                    <tfoot>
                                        <tr>
                       <%--                     <th style="text-align: center"></th>--%>
                                            <th colspan="2" style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304021") %></th>
                                            <th style="text-align: center"></th>
                                            <th style="text-align: center"></th>
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
                            <div class="col-md-12 result-wrapper" style="display:none">
                                <strong>
                                     <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306011") %>  <span id="type1done"></span> / <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306012") %>  <span  id="type1not"></span>
                                </strong>
                                <br />
                                <strong>
                                     <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304024") %>  <span  id="type2done"></span> / <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304025") %>  <span  id="type2not"></span>
                                </strong>
                                 <br />
                                <strong>
                                      <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304026") %>  <span  id="type3done"></span> / <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304027") %>  <span  id="type3not"></span> 
                                </strong>
                              
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
