<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ByRoomSDQType.aspx.cs" Inherits="FingerprintPayment.SDQ.Report.ByRoomSDQType" %>

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
                        url: "/SDQ/Report/ByRoomSDQType.aspx/LoadData",
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
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', data: 'Index', "class": "text-center", "width": "5%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>", data: 'Room', "class": "text-center", "width": "12%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %>", data: 'CountAll', "class": "text-center", "width": "8%" },

                        { "title": status1, data: 'Score1Type1', "class": "text-center", "width": "8%" },
                        { "title": status2, data: 'Score2Type1', "class": "text-center", "width": "8%" },
                        { "title": status3, data: 'Score3Type1', "class": "text-center", "width": "8%" },

                        { "title": status1, data: 'Score1Type2', "class": "text-center", "width": "8%" },
                        { "title": status2, data: 'Score2Type2', "class": "text-center", "width": "8%" },
                        { "title": status3, data: 'Score3Type2', "class": "text-center", "width": "8%" },

                        { "title": status1, data: 'Score1Type3', "class": "text-center", "width": "8%" },
                        { "title": status2, data: 'Score2Type3', "class": "text-center", "width": "8%" },
                        { "title": status3, data: 'Score3Type3', "class": "text-center", "width": "8%" },
                    ],
                    "order": [[0, 'asc']],
                    "fnInitComplete": function (oSettings, json) {
                        var p = json.d.percent;

                        $('#type11').text(p.Score1Type1.toFixed(2) + '%');
                        $('#type21').text(p.Score2Type1.toFixed(2) + '%');
                        $('#type31').text(p.Score3Type1.toFixed(2) + '%');

                        $('#type12').text(p.Score1Type2.toFixed(2) + '%');
                        $('#type22').text(p.Score2Type2.toFixed(2) + '%');
                        $('#type32').text(p.Score3Type2.toFixed(2) + '%');

                        $('#type13').text(p.Score1Type3.toFixed(2) + '%');
                        $('#type23').text(p.Score2Type3.toFixed(2) + '%');
                        $('#type33').text(p.Score3Type3.toFixed(2) + '%');

                        var f = json.d.summary;
                        var $foot = $("#template1 tfoot");
                        $foot.find('th:eq(1)').text(f.CountAll);

                        $foot.find('th:eq(2)').text(f.Score1Type1);
                        $foot.find('th:eq(3)').text(f.Score2Type1);
                        $foot.find('th:eq(4)').text(f.Score3Type1);

                        $foot.find('th:eq(5)').text(f.Score1Type2);
                        $foot.find('th:eq(6)').text(f.Score2Type2);
                        $foot.find('th:eq(7)').text(f.Score3Type2);

                        $foot.find('th:eq(8)').text(f.Score1Type3);
                        $foot.find('th:eq(9)').text(f.Score2Type3);
                        $foot.find('th:eq(10)').text(f.Score3Type3);

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

                xhr.open("POST", "/SDQ/Report/ByRoomSDQType.aspx/ExportExcel", true);
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
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304029") %>
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
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304030") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12 result-wrapper" style="display: none">
                                <table id="template1" class=" table-hover dataTable" width="100%">

                                    <thead>
                                        <tr>
                                            <th style="text-align: center;" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th style="text-align: center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></th>
                                            <th sstyle="text-align:center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %></th>
                                            <th colspan="3" style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></th>
                                            <th colspan="3" style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210024") %></th>
                                            <th colspan="3" style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %></th>
                                        </tr>
                                        <tr>
                                            <th style="text-align: center"><span class='text-success'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></span></th>
                                            <th style="text-align: center"><span class='text-warning'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></span></th>
                                            <th style="text-align: center"><span class='text-danger'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></span></th>
                                            <th style="text-align: center"><span class='text-success'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></span></th>
                                            <th style="text-align: center"><span class='text-warning'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></span></th>
                                            <th style="text-align: center"><span class='text-danger'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></span></th>
                                            <th style="text-align: center"><span class='text-success'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></span></th>
                                            <th style="text-align: center"><span class='text-warning'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></span></th>
                                            <th style="text-align: center"><span class='text-danger'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></span></th>
                                        </tr>
                                    </thead>

                                    <tbody></tbody>

                                    <tfoot>
                                        <tr>
                                            <th colspan="2" style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304021") %></th>
                                            <th style="text-align: center"></th>

                                            <th style="text-align: center"></th>
                                            <th style="text-align: center"></th>
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
                            <div class="col-md-12 result-wrapper" style="display: none">
                                <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306019") %>  <span id="type11"></span>/ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304035") %>  <span id="type21"></span>/ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304036") %>  <span id="type31"></span>
                                </strong>
                                <br />
                                <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304037") %>  <span id="type12"></span>/ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304038") %>  <span id="type22"></span>/ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304039") %>  <span id="type32"></span>
                                </strong>
                                <br />
                                <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304040") %>  <span id="type13"></span>/ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304041") %>  <span id="type23"></span>/ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304042") %>  <span id="type33"></span>
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
