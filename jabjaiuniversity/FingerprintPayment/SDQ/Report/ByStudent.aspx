<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ByStudent.aspx.cs" Inherits="FingerprintPayment.SDQ.Report.ByStudent" %>

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
    <script src="sdq.js?v=1"></script>
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
                    ajax: {
                        url: "/SDQ/Report/ByStudent.aspx/LoadData",
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
                                'name': SAC.GetStudentName(),
                                'type': $('#SDQType').val(),
                            };

                            return JSON.stringify(d);
                        },
                    },


                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', data: 'Index', "class": "text-center", "width": "7%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>", data: 'Code', "class": "text-center", "width": "10%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>", data: 'Room', "class": "text-center", "width": "10%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %>", data: 'FullName', "width": "15%", "class": "text-center" },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02323") %>", data: 'Score1', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcTypeScore(1, row.Score1);
                            }
                        },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00282") %>", data: 'Score2', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcTypeScore(2, row.Score2);
                            }
                        },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02108") %>", data: 'Score3', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcTypeScore(3, row.Score3);
                            }
                        },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00295") %>", data: 'Score4', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcTypeScore(4, row.Score4);
                            }
                        },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02147") %>", data: 'Score5', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcTypeScore(5, row.Score5);
                            }
                        },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01121") %>", data: 'ScoreAll', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                var type = $('#SDQType').val();
                                var scoreAll = 0;
                                switch (type) {
                                    case "1":
                                        //scoreAll = row.Score1 + row.Score2 + row.Score3 + row.Score4;
                                        scoreAll= quickSum([row.Score1 , row.Score2 , row.Score3 , row.Score4])
                                        break;
                                    case "2":
                                    case "3":
                                        //scoreAll = row.Score1 + row.Score2 + row.Score3 + row.Score4 + row.Score5;
                                        scoreAll =quickSum([row.Score1, row.Score2, row.Score3, row.Score4, row.Score5])
                                        break;

                                    default:
                                        return "";
                                }
                                return CalcTypeScore(6, scoreAll);
                            }
                        },
                       
                    ],
                    "order": [[0, 'asc']]
                });

            }
            else if (t == 'report') {
                var json = JSON.stringify({
                    'yearNo': YTLCF.GetYearNo(),
                    'term': YTLCF.GetTermID(),
                    'termNo': YTLCF.GetTermNo(),
                    'level1': YTLCF.GetLevelID(),
                    'level2': YTLCF.GetClassID(),
                    'name': SAC.GetStudentName(),
                    'type': $('#SDQType').val(),
                });
                var dt = new Date();
                var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306021") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                xhr = new XMLHttpRequest();

                xhr.open("POST", "/SDQ/Report/ByStudent.aspx/ExportExcel", true);
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
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304044") %>
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

                        <div class="row">
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
                    <div class="card-header card-header-primary card-header-icon">
                        <div class="card-icon">
                            <span class="material-icons">list_alt</span>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306021") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12" style="">
                                <table id="template1" class=" table-hover dataTable" width="100%" style="margin: 0 5px;">
                                    <%-- <thead>
                                        <tr>
                                            <th style="width: 10%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th style="width: 15%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                            <th style="width: 30%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                            <th style="width: 10%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                            <th style="width: 20%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305003") %></th>
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
    </form>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
