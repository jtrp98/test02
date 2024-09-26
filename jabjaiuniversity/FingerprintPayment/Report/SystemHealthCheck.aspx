<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="SystemHealthCheck.aspx.cs" Inherits="FingerprintPayment.Report.SystemHealthCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.css">
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

        .ui-timepicker-standard {
            z-index: 99 !important;
        }
    </style>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.js"></script>
    <script>
        var $table;
        function SearchData(t) {
            if (!$("#aspnetForm").valid()) {
                return;
            }

            if (t == 'data') {
                var date1 = $("#datestart1").val();
                $table = $('#template1').DataTable({
                    "processing": true,
                    //"serverSide": true,
                    "destroy": true,
                    "info": false,
                    paging: true,
                    "pageLength": +$("#pageLen").val(),
                    searching: false,
                    "lengthChange": false,
                    ajax: {
                        url: "/Report/SystemHealthCheck.aspx/LoadData",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        "dataSrc": function (r) {
                            return r.d.data;
                        },
                        'data': function (d) {

                            d.search = {
                                'method': $('#methodName').val(),
                                'date1': moment(date1, 'DD/MM/YYYY').add(-543, 'years').format('MM/DD/YYYY'),
                                'time1': $('#time1').val(),
                                'time2': $('#time2').val(),
                                'page': $('#pageLen').val(),
                            };

                            return JSON.stringify(d);
                        },
                    },

                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><br/>Order', data: 'Index', "class": "text-center", "width": "7%" },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701075") %><br/>Transaction Time', data: 'Transaction', "class": "text-center", "width": "15%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701076") %><br/>Transmission Time", data: 'Transmission', "class": "text-center", "width": "15%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701077") %><br/>Processing Time(ms)", data: 'Processing', "class": "text-center", "width": "15%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701078") %><br/>User ID", data: 'Code', "width": "15%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701079") %><br/>Processing Status", data: 'Status1', "width": "15%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701080") %><br/>Internet Status", data: 'Status2', "width": "15%", "class": "text-center" },

                    ],
                    "order": [[0, 'asc']],
                    "fnInitComplete": function (oSettings, json) {
                        if ($('#methodName').val() == 'CheckBalance') {
                            $("#note1").show();
                        }
                        else {
                            $("#note1").hide();
                        }
                    }
                });

            }
            else if (t == 'report') {
                //var json = JSON.stringify({
                //    'yearNo': YTLCF.GetYearNo(),
                //    'term': YTLCF.GetTermID(),
                //    'termNo': YTLCF.GetTermNo(),
                //    'level1': YTLCF.GetLevelID(),
                //    'level2': YTLCF.GetClassID(),
                //    'name': SAC.GetStudentName(),
                //    //'type': $('#SDQType').val(),
                //});
                //var dt = new Date();
                //var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306001") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                //xhr = new XMLHttpRequest();

                //xhr.open("POST", "/EQ/Report/ByStudent.aspx/ExportExcel", true);
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

            $(".datepicker").keydown(function (e) {
                e.preventDefault();
            });

            //$('.timepicker').timepicker({
            //    timeFormat: 'HH:mm',
            //    interval: 30,
            //    //minTime: '00:00',
            //    //maxTime: '23:59',
            //    /* defaultTime: '11',*/
            //    /*startTime: '00:00',*/
            //    dynamic: false,
            //    dropdown: true,
            //    scrollbar: true
            //});

            //$(".timepicker").keydown(function (e) {
            //    e.preventDefault();
            //});

            //$('#pageLen').on('change', function () {
            //    if ($table) {
            //        $table.page.len(+$('#pageLen').val()).draw();
            //    }
            //});

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
                System Health Check
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

                        <%-- <uc1:YTLCFilter runat="server" ID="YTLCFilter" IsLevelRequired="true" />--%>

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701067") %></label>
                            <div class="col-sm-3">

                                <select id="methodName" class="selectpicker " data-style="select-with-transition" data-width="100%">
                                    <option value="BuyProduct_Barcode">School Bright Smart Canteen</option>
                                    <option value="PosSales">School Bright Shop</option>
                                  <%--  <option value="CheckBalance">Check Balance</option>--%>
                                    <option value="TopUpUserWebOrMobile"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701068") %></option>
                                    <%-- <option value="LoginPosDevice">Attendance</option>--%>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                            <div class="col-sm-3">
                                <div class="form-group has-successx">
                                    <input type="text" id="datestart1" class="form-control datepicker" required />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>                              
                            </div>
                   
                            <label class="col-sm-7 col-form-label text-left" style="color:red">* สามารถดูข้อมูลย้อนหลังได้  1 เดือน</label>
                        </div>

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701070") %></label>
                            <div class="col-md-3 ">
                                <input type="time" id="time1" class="form-control timepicker1" required value="00:00" />
                              
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701073") %></label>
                            <div class="col-md-3 "> 
                                <input type="time" id="time2" class="form-control timepicker1" required value="23:59" />
                               
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701071") %></label>
                            <div class="col-sm-3">
                                <select id="pageLen" class="selectpicker " data-style="select-with-transition" data-width="100%">
                                    <option value="50">50</option>
                                    <option value="100">100</option>
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
                                <%--<div class="btn btn-info pull-rightx" id="exportfile" onclick="SearchData('report')">
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
                        <h4 class="card-title">System Health Check</h4>
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
                            <div class="col-md-12 text-center" id="note1" style="display:none">
                                <strong style="color:red"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133153") %>
                                </strong>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" id="result-wrapper">
                                <strong style="float: right;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701074") %>
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
