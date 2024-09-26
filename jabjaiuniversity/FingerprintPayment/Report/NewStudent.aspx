<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="NewStudent.aspx.cs" Inherits="FingerprintPayment.Report.NewStudent" %>

<%@ Register Src="~/UserControls/YTFilter.ascx" TagPrefix="uc1" TagName="YTFilter" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

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

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">

    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/2.0.1/js/dataTables.buttons.min.js"></script>
    <%--    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/1.6.4/js/buttons.flash.min.js"></script>--%>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/2.0.1/js/buttons.html5.min.js"></script>
    <%-- <script src="Js/buttons.html5.js"></script>--%>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/2.0.1/js/buttons.print.min.js"></script>

    <script>
        $(function () {


            $('.datepicker').datetimepicker({
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
                debug: false,
                //defaultDate: "<%=DateTime.Now.ToString("dd/MM/yyyy") %>",
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

            //$('.datepicker').datetimepicker({
            //    format: 'DD/MM/YYYY-BE',
            //    locale: 'th',
            //    debug: false,
            //    //defaultDate: "DateTime.Now.ToString("dd/MM/yyyy")",
            //    //autoclose: true,
            //    //autoclose: true,
            //    //showOn: "button",
            //    icons: {
            //        time: "fa fa-clock-o",
            //        date: "fa fa-calendar",
            //        up: "fa fa-chevron-up",
            //        down: "fa fa-chevron-down",
            //        previous: 'fa fa-chevron-left',
            //        next: 'fa fa-chevron-right',
            //        today: 'fa fa-screenshot',
            //        clear: 'fa fa-trash',
            //        close: 'fa fa-remove'
            //    }
            //});

            //$('#date2').on('dp.change', function (e) {
            //    var d1 = moment($("#date1").val(), 'DD/MM/YYYY');
            //    var d2 = moment($("#date2").val(), 'DD/MM/YYYY');
            //    if (d2 < d1) {
            //        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131144") %>');
            //        $("#date2").val($("#date1").val());
            //    }
            //});

            //$(".datepicker").keydown(function (e) {
            //    e.preventDefault();
            //});

            //$('#ddlLevel1').change(function () {
            //    getListSubLV2();
            //});

        });

        //function getListSubLV2() {
        //    var SubLVID = $('#ddlLevel1 option:selected').val();
        //    $('#ddlLevel2 option').remove();
        //    $.ajax({
        //        url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
        //        success: function (msg) {
        //            if (msg.length > 0) {

        //                $('#ddlLevel2')
        //                    .append($("<option></option>")
        //                        .attr("value", "")
        //                        .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203069") %>"));

        //                $.each(msg, function (index) {
        //                    $('#ddlLevel2')
        //                        .append($("<option></option>")
        //                            .attr("value", msg[index].nTermSubLevel2)
        //                            .text(msg[index].nTSubLevel2)
        //                        );
        //                });

        //                $('#ddlLevel2').selectpicker('refresh');
        //            }
        //        }
        //    });
        //}

        function SearchData(t) {
            if (!$("#aspnetForm").valid()) {
                return;
            }

            if (t == 'data') {
                $("body").mLoading();           
                var date1 = $("#date1").data("DateTimePicker").date().format('MM/DD/YYYY');
                var date2 = $("#date2").data("DateTimePicker").date().format('MM/DD/YYYY');

                PageMethods.GetData1(YTF.GetTermID(), date1, date2, function (response) {
                    $("#result-wrapper").html('');
                    if (response.length > 0) {
                        response.forEach(r => {
                            var d = r.Data;

                            var $table = $("#template1").clone();
                            var $body = $table.find('tbody');

                            var no = 1;
                            d.forEach(i => {

                                var tr = '<tr id=' + i.sID + '>\
                                <td style="text-align: center">' + no +
                                '<td style="text-align: center">' + i.sStudentID +
                                '<td style="text-align: left">' + i.title + ' ' +i.FullName +
                                '<td style="text-align: center">' + r.Room +
                                '<td style="text-align: center">' + i.moveInDate +
                                '<td style="text-align: center">' + i.oldSchoolName
                                ;

                                $body.append(tr);
                                no++;
                            });

                            $table.find('tfoot #stdAmount').text(r.Count);

                            $('#result-wrapper').append($table);
                        });
                    }
                    else {
                        var $table = $("#template1").clone();
                        var $body = $table.find('tbody');

                        var tr = '<tr><td style="text-align: center" colspan=6><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102035") %>';

                        $body.append(tr);

                        $table.find('tfoot #stdAmount').text(0);

                        $('#result-wrapper').append($table);
                    }
                    $("body").mLoading("hide");
                });

            }
            else if (t == 'report') {
                var date1 = $("#date1").data("DateTimePicker").date().format('MM/DD/YYYY');
                var date2 = $("#date2").data("DateTimePicker").date().format('MM/DD/YYYY');

                var json = JSON.stringify({
                    'term': YTF.GetTermID(),
                    'termno': YTF.GetTermText(),
                    'year': YTF.GetYearNo(),
                    'date1': date1,
                    'date2': date2,
                });

                var dt = new Date();
                var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104068") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                xhr = new XMLHttpRequest();

                xhr.open("POST", "/Report/NewStudent.aspx/ExportExcel1", true);
                xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                xhr.responseType = 'blob';
                xhr.onload = function () {
                    //aa = xhr.getResponseHeader("filename");
                    saveAs(xhr.response, file_name);
                    //$("body").mLoading('hide');
                };
                xhr.send(json);
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
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104068") %>
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
                        <uc1:YTFilter runat="server" ID="YTFilter" IsRequired="true" />

                        
                        <div class="row reptype">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104069") %><br />
                                Date
                            </label>
                            <div class="col-sm-3">
                                <div class="form-group ">
                                    <input type="text" id="date1" name="date1" class="form-control datepicker" value="" required>
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104070") %><br />
                                To Date
                            </label>
                            <div class="col-sm-3">
                                <div class="form-group ">
                                    <input type="text" id="date2" name="date2" class="form-control datepicker" value="" required>
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
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
                            <div class="col-md-12" style="display: none">
                                <table id="template1" class=" table-hover dataTable" width="100%" style="margin:0 5px;">
                                    <thead>
                                        <tr>
                                            <th style="width: 10%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th style="width: 15%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104063") %></th>
                                            <th style="width: 30%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                            <th style="width: 10%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104064") %></th>
                                            <th style="width: 15%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101050") %></th>
                                            <th style="width: 20%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104071") %></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td style="text-align:right; border-bottom: 2px solid #000;" colspan="6">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104072") %><span id="stdAmount"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="text-align:right" colspan="6">
                                                <br />
                                            </td>
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



