<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="Summary.aspx.cs" Inherits="FingerprintPayment.VisitHousePage.Web.Report.Summary" %>

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
                        url: "/VisitHousePage/Web/Report/Summary.aspx/LoadData",
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
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>", data: 'Room', "class": "text-center", "width": "20%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %>", data: 'CountAll', "class": "text-center", "width": "15%" },

                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305006") %>", data: 'Status2', "class": "text-center", "width": "15%" }, 
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305005") %>", data: 'Status3', "class": "text-center", "width": "15%" }, 
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133543") %>", data: 'Status1', "class": "text-center", "width": "15%" }, 
                      /*  { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133542") %>", data: 'Status1', "class": "text-center", "width": "10%" }, */

                    ],
                    "order": [[0, 'asc']],
                    "fnInitComplete": function (oSettings, json) {
                        var p = json.d.percent;

                        $('#status1').text(p.Status1.toFixed(2) + '%');
                        $('#status2').text(p.Status2.toFixed(2) + '%');
                        $('#status3').text(p.Status3.toFixed(2) + '%');
                        //$('#status4').text(p.Status4.toFixed(2) + '%');

                        var f = json.d.summary;
                        var $foot = $("#template1 tfoot");
                        $foot.find('th:eq(1)').text(f.CountAll);

                        $foot.find('th:eq(2)').text(f.Status2);
                        $foot.find('th:eq(3)').text(f.Status3);
                        $foot.find('th:eq(4)').text(f.Status1);

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
                var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603001") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305007") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                xhr = new XMLHttpRequest();

                xhr.open("POST", "/VisitHousePage/Web/Report/Summary.aspx/ExportExcel", true);
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
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305008") %>
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
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305007") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12 result-wrapper" style="display:none">
                                <table id="template1" class=" table-hover dataTable" width="100%" >

                                     <thead>
                                         <tr>
                                            <th style="text-align:center" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></th>
                                            <th style="text-align:center" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %></th>

                                            <th style="text-align:center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305006") %></th>
                                            <th style="text-align:center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305005") %></th>
                                            <th style="text-align:center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133543") %></th>
                                         </tr>
                                      
                                    </thead>

                                    <tbody></tbody>

                                    <tfoot>
                                        <tr>
                                            <th  style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304021") %></th>

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
                                     <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305009") %> <span id="status2"></span> 
                                </strong>
                                <br />
                                <strong>
                                     <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305010") %> <span id="status3"></span>
                                </strong>
                                <br />
                                <strong>
                                     <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305011") %> <span id="status1"></span> 
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
