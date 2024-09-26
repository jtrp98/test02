<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ReportBalanceNew.aspx.cs" EnableEventValidation="false" Async="true" Inherits="FingerprintPayment.Report.ReportBalanceNew" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- DatetimePicker -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.css" />
    <%--<link href="Css/css.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        .bootstrap-datetimepicker-widget table td, .bootstrap-datetimepicker-widget table td span, .bootstrap-datetimepicker-widget table th.picker-switch {
            font-size: 20px;
        }

            .bootstrap-datetimepicker-widget table td span {
                font-size: 18px;
            }

        .bootstrap-datetimepicker-widget table td {
            width: 30px;
            height: 30px;
        }

            .bootstrap-datetimepicker-widget table td.active, .bootstrap-datetimepicker-widget table td.active:hover {
                background-color: transparent !important;
            }

            .bootstrap-datetimepicker-widget table td.day:hover, .bootstrap-datetimepicker-widget table td.hour:hover, .bootstrap-datetimepicker-widget table td.minute:hover, .bootstrap-datetimepicker-widget table td.second:hover {
                background: transparent;
            }

        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603095") %>      
            </p>
        </div>
    </div>

    <form id="aspnetForm" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release">
        </asp:ScriptManager>
        <asp:HiddenField ID="hdfsid" runat="server" />
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
                        <div class="row ml-md-5">

                            <%-- <div class="col-md-1"></div>--%>
                            <label class="col-xl-1 col-lg-2 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                            <div class="col-xl-2 col-lg-4">
                                <asp:DropDownList ID="ddlYear" ClientIDMode="Static" runat="server" CssClass="selectpicker col-md-12" data-style="select-with-transition">
                                </asp:DropDownList>
                            </div>

                            <div class="col-xl-1 d-lg-none d-xl-block"></div>

                            <label class="col-xl-1 col-lg-2 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %></label>
                            <div class="col-xl-2 col-lg-4">
                                <asp:DropDownList ID="ddlType" ClientIDMode="Static" AutoPostBack="false" runat="server" CssClass="selectpicker col-md-12" data-style="select-with-transition" Width="100%">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="1" />
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206367") %>" Value="2" />
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603086") %>" Value="3" />
                                </asp:DropDownList>
                            </div>

                            <div class="col-xl-1 d-lg-none d-xl-block"></div>

                            <label class="col-xl-1 col-lg-2 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206248") %></label>
                            <div class="col-xl-2 col-lg-4 ">
                                <asp:TextBox runat="server" ID="txtstart" CssClass="form-control  datepicker --date-validate" ClientIDMode="Static" MaxLength="10" Style="" required autocomplete="off" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>

                            <div class="col-xl-1 d-lg-none d-xl-block"></div>
                        </div>

                        <%--                        <div class="row">
                            <div class="col-md-12 button-section text-center">
                                <button type="button" id="btnSearch" class="btn btn-info search-btn" onclick="onSearch()">
                                    <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                            </div>
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
                            <div class="col-md-12 text-right">
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603097") %>
                                </span>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12">
                <div class="card ">

                    <div class="card-header card-header-warning card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102145") %></h4>
                    </div>
                    <div class="card-body ">
                        <div id="t1wrapper">
                            <table id="datatable1" class="table-hover dataTable" width="100%" style="width: 100%"></table>
                        </div>

                        <div id="t2wrapper">
                            <table id="datatable2" class="table-hover dataTable" width="100%" style="width: 100%">
                            </table>
                        </div>

                        <div id="t3wrapper">
                            <table id="datatable3" class="table-hover dataTable" width="100%" style="width: 100%">
                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <%--  <script type="text/javascript" language="javascript" src="//cdn.datatables.net/1.10.22/js/jquery.dataTables.min.js"></script>--%>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/1.6.4/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/1.6.4/js/buttons.flash.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <%--	<script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/1.6.4/js/buttons.html5.min.js"></script>--%>
    <%--<script src="Js/buttons.html5.js"></script>--%>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/1.6.4/js/buttons.print.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/bootstrap-datetimepicker/moment-with-locales.js"></script>
    <script type='text/javascript' src="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.th.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" type="text/javascript"></script>

    <script>
        var $table;

        function SearchData(t) {

            if (!$("#aspnetForm").valid()) {
                return;
            }

            //if ($.fn.dataTable.isDataTable('#datatable1')) {
            //    $table.destroy();            
            //}
            //else {            
            //}

            var dStart , dEnd ;
            if ($("#txtstart").val() != '')
                dStart = $("#txtstart").data("DateTimePicker").date();//moment($("#txtstart").val(), 'DD/MM/YYYY').format("YYYYMMDD");//getDate($("#txtstart").val());

            if (dStart < new Date("2021-05-01")) {

                Swal.fire({
                    type: 'warning',
                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133162") %>',
                    //text: 'Something went wrong!',                     
                });

                return;
            }

            if (t == 'data') {
                if ($("#ddlType").val() == "1") {
                    $("#t1wrapper").show();
                    $("#t2wrapper").hide();
                    $("#t3wrapper").hide();
                    $table = $('#datatable1').DataTable({
                        "processing": true,
                        "destroy": true,
                        "info": false,
                        paging: true,
                        "pageLength": 50,
                        "bLengthChange": false,
                        searching: false,

                        ajax: {
                            url: "ReportBalanceNew.aspx/GetData1",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            "dataSrc": function (r) {
                                return r.d.data;
                            },
                            'data': function (d) {

                                d.search = {
                                    'year': $('#ddlYear').val(),
                                    'date1': dStart.format("MM/DD/YYYY"),
                                };

                                return JSON.stringify(d);
                            },
                        },

                        columns: [
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', "data": "index", "width": "5%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>', "data": "date", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602015") %>', "data": "topup", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603098") %>', "data": "topupCancle", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603099") %>', "data": "use", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603100") %>', "data": "useCancle", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603062") %>', "data": "withraw", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106151") %>', "data": "net", "width": "8%", "class": "text-center" },
                        ]
                    });
                }
                else if ($("#ddlType").val() == "2") {
                    $("#t1wrapper").hide();
                    $("#t2wrapper").show();
                    $("#t3wrapper").hide();
                    $table = $('#datatable2').DataTable({
                        "processing": true,
                        "destroy": true,
                        "info": false,
                        paging: true,
                        "pageLength": 50,
                        "bLengthChange": false,
                        searching: false,

                        ajax: {
                            url: "ReportBalanceNew.aspx/GetData2",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            "dataSrc": function (r) {
                                return r.d.data;
                            },
                            'data': function (d) {

                                d.search = {
                                    'year': $('#ddlYear').val(),
                                    'date1': dStart.format("MM/DD/YYYY"),
                                };

                                return JSON.stringify(d);
                            },
                        },

                        columns: [
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', "data": "index", "width": "5%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>', "data": "date", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>', "data": "code", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>', "data": "fullName", "width": "14%", "class": "text-left" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %> / <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210024") %>', "data": "type", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602015") %>', "data": "topup", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603098") %>', "data": "topupCancle", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603099") %>', "data": "use", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603100") %>', "data": "useCancle", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603062") %>', "data": "withraw", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106151") %>', "data": "net", "width": "8%", "class": "text-center" },
                        ]
                    });
                }
                else {
                    $("#t1wrapper").hide();
                    $("#t2wrapper").hide();
                    $("#t3wrapper").show();
                    $table = $('#datatable3').DataTable({
                        "processing": true,
                        "destroy": true,
                        "info": false,
                        paging: true,
                        "pageLength": 50,
                        "bLengthChange": false,
                        searching: false,

                        ajax: {
                            url: "ReportBalanceNew.aspx/GetData3",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            "dataSrc": function (r) {
                                return r.d.data;
                            },
                            'data': function (d) {

                                d.search = {
                                    'year': $('#ddlYear').val(),
                                    'date1': dStart.format("MM/DD/YYYY"),
                                };

                                return JSON.stringify(d);
                            },
                        },

                        columns: [
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', "data": "index", "width": "5%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>', "data": "date", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106154") %>', "data": "card", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>', "data": "fullName", "width": "14%", "class": "text-left" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ะภท', "data": "type", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602015") %>', "data": "topup", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603098") %>', "data": "topupCancle", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603099") %>', "data": "use", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603100") %>', "data": "useCancle", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603062") %>', "data": "withraw", "width": "8%", "class": "text-center" },
                            { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106151") %>', "data": "net", "width": "8%", "class": "text-center" },
                        ]
                    });
                }
            }
            else if (t == 'report') {

                var json = JSON.stringify({
                    search: {
                        'date1': dStart,
                        'year': $('#ddlYear').val(),
                    }
                });
                var dt = new Date();
                var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01693") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                xhr = new XMLHttpRequest();

                if ($("#ddlType").val() == "1") {
                    xhr.open("POST", "/Report/ReportBalanceNew.aspx/ExportExcel1", true);
                }
                else if ($("#ddlType").val() == "2") {
                    xhr.open("POST", "/Report/ReportBalanceNew.aspx/ExportExcel2", true);
                } else {
                    xhr.open("POST", "/Report/ReportBalanceNew.aspx/ExportExcel3", true);
                }
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
            var _startDate = moment("01/01/2022", "DD/MM/YYYY");

            $('.datepicker').datetimepicker({
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
                debug: false,
                minDate: _startDate,
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

            if (jQuery.validator) {//.messages

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",
                });

                $("#aspnetForm").validate({  // initialize the plugin

                    errorPlacement: function (error, element) {
                        let _class = element.attr('class');

                        if (_class.includes('--date-validate')) {
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
