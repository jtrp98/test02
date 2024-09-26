<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="BySummary.aspx.cs" Inherits="FingerprintPayment.Screening.Report.BySummary" %>

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

        .hilight td {
            background-color: #e7e7e7;
        }
    </style>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script>

        function SearchData(t) {
            if (!$("#aspnetForm").valid()) {
                return;
            }

            if (t == 'data') {

                var search = {
                    'term': YTLCF.GetTermID(),
                    'level1': YTLCF.GetLevelID(),
                    'level2': YTLCF.GetClassID(),
                };
                $("body").mLoading();
                PageMethods.LoadData(search, function (res) {

                    $("#template1 tbody").empty();
                    $("#template2 tbody").empty();

                    var d1 = res.data.filter(function (obj) {
                        return (obj.type == 1);
                    });

                    d1.forEach(function (i) {
                        $("#template1 tbody").append(`<tr>
                            <td class='text-left'>${i.group}</td>
                            <td class='text-center'>${i.c1}</td>
                            <td class='text-center'>${i.p1.toFixed(2)}%</td>
                            <td class='text-center'>${i.c2}</td>
                            <td class='text-center'>${i.p2.toFixed(2)}%</td>
                        </tr>`);
                    })

                    var d2 = res.data.filter(function (obj) {
                        return (obj.type == 2);
                    });

                    d2.forEach(function (i) {
                        $("#template2 tbody").append(`<tr>
                            <td class='text-left'>${i.group}</td>
                            <td class='text-center'>${i.c1}</td>
                            <td class='text-center'>${i.p1.toFixed(2)}%</td>
                            <td class='text-center'>${i.c2}</td>
                            <td class='text-center'>${i.p2.toFixed(2)}%</td>
                            <td class='text-center'>${i.c3}</td>
                            <td class='text-center'>${i.p3.toFixed(2)}%</td>
                        </tr>`);
                    })

                    var d3 = res.data.filter(function (obj) {
                        return (obj.type == 3);
                    });

                    $("#template2 tbody").append(`<tr class='hilight'>
                            <td class='text-left'></td>
                            <td class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></td>
                            <td class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206205") %></td>
                            <td class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></td>
                            <td class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206205") %></td>
                            <td class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></td>
                            <td class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206205") %></td>
                        </tr>`);
                    d3.forEach(function (i) {
                        $("#template2 tbody").append(`<tr>
                            <td class='text-left'></td>
                            <td class='text-center'>${i.c1}</td>
                            <td class='text-center'>${i.p1.toFixed(2)}%</td>
                            <td class='text-center'>${i.c2}</td>
                            <td class='text-center'>${i.p2.toFixed(2)}%</td>
                            <td class='text-center'>${i.c3}</td>
                            <td class='text-center'>${i.p3.toFixed(2)}%</td>
                        </tr>`);
                    })

                    $('.result-wrapper').show();
                    $("body").mLoading("hide");
                });


            }
            else if (t == 'excel' || t == 'excelAll') {

                if (t == 'excel') {

                    var json = ({
                        'yearNo': YTLCF.GetYearNo(),
                        'term': YTLCF.GetTermID(),
                        'termNo': YTLCF.GetTermNo(),
                        'level1': YTLCF.GetLevelID(),
                        'level2': YTLCF.GetClassID(),
                        //'name': "",
                        //'type': $('#SDQType').val(),
                    });
                    var dt = new Date();
                    var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307006") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                    xhr = new XMLHttpRequest();

                    xhr.open("POST", "/Screening/Report/BySummary.aspx/ExportExcel", true);
                    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                    xhr.responseType = 'blob';
                    xhr.onload = function () {
                        //aa = xhr.getResponseHeader("filename");
                        saveAs(xhr.response, file_name);
                        //$("body").mLoading('hide');
                    };
                   
                    xhr.send(JSON.stringify({ 'search': json }));
                   
                }

            }
            else if (t == 'pdf' || t == 'pdfAll') {

                if (t == 'pdfAll') {

                    var json = ({
                        'yearNo': YTLCF.GetYearNo(),
                        'term': YTLCF.GetTermID(),
                        'termNo': YTLCF.GetTermNo(),
                        'level1': YTLCF.GetLevelID(),
                        'level2': YTLCF.GetClassID(),
                        //'name': "",
                        //'type': $('#SDQType').val(),
                    });
                    var dt = new Date();
                    var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307006") %>_' + dt.format("ddMMyyyyHHmmssss") + '.pdf';

                    xhr = new XMLHttpRequest();

                    xhr.open("POST", "/Screening/Report/BySummary.aspx/ExportPDF", true);
                    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                    xhr.responseType = 'blob';
                    xhr.onload = function () {
                        //aa = xhr.getResponseHeader("filename");
                        saveAs(xhr.response, file_name);
                        //$("body").mLoading('hide');
                    };

                    xhr.send(JSON.stringify({ 'search': json }));
                }

            }


            //else if (t == 'report') {
            //    var json = JSON.stringify({
            //        'yearNo': YTLCF.GetYearNo(),
            //        'term': YTLCF.GetTermID(),
            //        'termNo': YTLCF.GetTermNo(),
            //        'level1': YTLCF.GetLevelID(),
            //        'level2': YTLCF.GetClassID(),
            //        //'name': "",
            //        //'type': $('#SDQType').val(),
            //    });
            //    var dt = new Date();
            //    var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307006") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

            //    xhr = new XMLHttpRequest();

            //    xhr.open("POST", "/Screening/Report/BySummary.aspx/ExportExcel", true);
            //    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            //    xhr.responseType = 'blob';
            //    xhr.onload = function () {
            //        //aa = xhr.getResponseHeader("filename");
            //        saveAs(xhr.response, file_name);
            //        //$("body").mLoading('hide');
            //    };
            //    xhr.send(json);
            //}
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
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307007") %>
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
                            <span class="material-icons">list_alt</span>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307006") %></h4>
                    </div>
                    <div class="card-body ">



                        <div class="row">
                            <div class="col-md-12 result-wrapper" style="display: none">

                                <table id="template2" class=" table-hover dataTable" width="100%" style="margin: 0 5px;">
                                    <thead>
                                        <tr>
                                            <th style="width: 40%; text-align: center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306028") %></th>
                                            <th style="width: 20%; text-align: center" colspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></th>
                                            <th style="width: 20%; text-align: center" colspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></th>
                                            <th style="width: 20%; text-align: center" colspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></th>

                                        </tr>
                                        <tr>
                                            <th style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></th>
                                            <th style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206205") %></th>
                                            <th style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></th>
                                            <th style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206205") %></th>
                                            <th style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></th>
                                            <th style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206205") %></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                                <br />
                                <table id="template1" class=" table-hover dataTable" width="100%" style="margin: 0 5px;">
                                    <thead>
                                        <tr>
                                            <th style="width: 60%; text-align: center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306028") %></th>
                                            <th style="width: 20%; text-align: center" colspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %></th>
                                            <th style="width: 20%; text-align: center" colspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01361") %></th>
                                        </tr>
                                        <tr>
                                            <th style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></th>
                                            <th style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206205") %></th>
                                            <th style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></th>
                                            <th style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206205") %></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>

                            </div>
                        </div>

                        <%-- <div class="row">
                            <div class="col-md-12" id="result-wrapper">
                            </div>
                        </div>--%>
                    </div>
                </div>

            </div>
        </div>
    </form>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
