<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="CanteenAttendance.aspx.cs" Inherits="FingerprintPayment.Report.CanteenAttendance" %>

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

            if (jQuery.validator) { //.messages

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>",
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

            $('.datepicker').datetimepicker({
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
                debug: false,
                //defaultDate: "DateTime.Now.ToString("dd/MM/yyyy")",
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

            $('#date2').on('dp.change', function (e) {
                var d1 = $("#date1").data("DateTimePicker").date();
                var d2 = $("#date2").data("DateTimePicker").date();
                if (d2 < d1) {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131144") %>');
                    $("#date2").val($("#date1").val());
                }
            });

            $(".datepicker").keydown(function (e) {
                e.preventDefault();
            });

            $('#ddlLevel1').change(function () {
                getListSubLV2();
            });

            $('#type').change(function () {
                var v = $(this).val();

                switch (v) {
                    case '1':
                    case '':
                        $('.row.-student').hide();
                        break;
                                     
                    case '0':
                        $('.row.-student').show();
                        break;                                       
                }
            });

        });

        function getListSubLV2() {
            var SubLVID = $('#ddlLevel1 option:selected').val();
            $('#ddlLevel2 option').remove();
            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
                success: function (msg) {
                    if (msg.length > 0) {

                        $('#ddlLevel2')
                            .append($("<option></option>")
                                .attr("value", "")
                                .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203069") %>"));

                        $.each(msg, function (index) {
                            $('#ddlLevel2')
                                .append($("<option></option>")
                                    .attr("value", msg[index].nTermSubLevel2)
                                    .text(msg[index].nTSubLevel2)
                                );
                        });

                        $('#ddlLevel2').selectpicker('refresh');
                    }
                }
            });
        }

        function SearchData(t) {
            if (!$("#aspnetForm").valid()) {
                return;
            }
            //if ($("#txtstart").val() == '')
            //    return;
            var dStart = "", dEnd = "";
            if ($("#date1").val() != '')
                dStart = $("#date1").data("DateTimePicker").date().format("MM/DD/YYYY");//getDate($("#txtstart").val());
            if ($("#date2").val() != '')
                dEnd = $("#date2").data("DateTimePicker").date().format("MM/DD/YYYY");//getDate($("#txtend").val());
            //else
            //    dEnd = dStart

            var type = $("#type").val();
            var shop_id = $("#shop_id").val();
            var level = $('#ddlLevel1').val();
            var room = $('#ddlLevel2').val();
            //var obj = {
            //    'shop_id': shop_id,
            //    //'date1': dStart,
            //    //'date2': dEnd
            //};
            //var search = {};
            //search.shop_id = shop_id;
            //search.date1 = dStart;
            //search.date2 = dEnd;

            if (t == 'data') {
                $.fn.dataTable.ext.errMode = 'throw';
                var dt = $('#lst-data').DataTable({
                    "processing": true,
                    /*  "serverSide": true,*/
                    "destroy": true,
                    "info": false,
                    paging: true,
                    "pageLength": 20,
                    "lengthChange": false,
                    searching: false,

                    ajax: {
                        url: "CanteenAttendance.aspx/GetData1",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        "dataSrc": function (r) {
                            return r.d.data;
                        },
                        'data': function (d) {

                            d.search = {
                                'type': type,
                                'shop_id': shop_id,
                                'date1': dStart,
                                'date2': dEnd,
                                'level': level,
                                'room': room,
                            };

                            return JSON.stringify(d);
                        },
                    },

                    dom: 'Bfrtip',
                    buttons: [

                        {
                            extend: 'excel',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133151") %> ' + $("#date1").val() + ' - ' + $("#date2").val(),
                            exportOptions: {
                                columns: [0, 1, 2, 3, 4, 5],
                                //order: [[7, "asc"]]
                            },
                            customize: function (xlsx) {
                                var sheet = xlsx.xl.worksheets['sheet1.xml'];
                                //$('row c[r=A1]', sheet).attr('s', '2').attr('s', '51');
                                //$('c[r=A1] t', sheet).text('Custom text');
                            }
                        }
                    ],

                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', data: 'index', "class": "text-center", "width": "5%" },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> <span class='material-icons' style='font-size:18px;display:none;' id='tooltipdate1' data-toggle='tooltip' title='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133152") %>'>help_outline</span>", data: 'date', "class": "text-center", "width": "20%"
                        },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603003") %>", data: 'shop', "class": "text-center", "width": "20%",
                        },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103165") %>", data: 'user', "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104064") %>", data: 'level', "class": "text-center", "width": "20%" },
                       /* { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>", data: 'count', "class": "text-center", "width": "10%" },*/
                    ],
                    "order": [[0, 'asc']]
                });

                $('#tooltipdate1').tooltip();
            }
            else if (t == 'report') {

                var json = JSON.stringify({
                    search: {
                        'type': type,
                        'shop_id': shop_id,
                        'date1': dStart,
                        'date2': dEnd,
                        'level': level,
                        'room': room,
                    }
                });
                var dt = new Date();
                var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603002") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                xhr = new XMLHttpRequest();

                xhr.open("POST", "/Report/CanteenAttendance.aspx/ExportExcel1", true);
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
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603002") %>
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

                        <div class="row">
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></label>
                            <div class=" col-md-3 ">
                                <select id="type" class="selectpicker" data-width="100%" data-style="select-with-transition">
                                    <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210024") %></option>
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></option>
                                </select>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601003") %></label>
                            <div class=" col-md-3">
                                <select id="shop_id" class="selectpicker" data-width="100%" data-style="select-with-transition">
                                    <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                    <% JabjaiEntity.DB.JabJaiEntities entities = new JabjaiEntity.DB.JabJaiEntities(MasterEntity.Connection.StringConnectionSchool(Session["sEntities"] + "",MasterEntity.ConnectionDB.Read));
                                        var q_shop = entities.TShops.Where(w => w.del == null && w.SchoolID == this.UserData.CompanyID).ToList();
                                        foreach (var data in q_shop)
                                        {
                                    %>
                                    <option value="<%= data.shop_id %>"><%= data.shop_name %></option>
                                    <%
                                        }
                                    %>
                                </select>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row -student" style="display: none">
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                            <div class=" col-md-3 ">
                                <asp:DropDownList ID="ddlLevel1" ClientIDMode="Static" data-width="100%" runat="server" class="selectpicker" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %>" data-style="select-with-transition" >
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                            <div class=" col-md-3">
                                <asp:DropDownList ID="ddlLevel2" ClientIDMode="Static" data-width="100%" runat="server" class="selectpicker" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>" data-style="select-with-transition">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2"></div>
                        </div>


                        <div class="row">
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101361") %></label>
                            <div class=" col-md-3 ">
                                <div class="form-group has-success-x">
                                    <input type="text" name="date1" id="date1" class="form-control datepicker" value="" required>
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>

                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101362") %></label>
                            <div class=" col-md-3">
                                <div class="form-group has-success-x">
                                    <input type="text" name="date2" id="date2" class="form-control datepicker" required>
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2"></div>
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
                            <div class="col-md-12">
                                <table id="lst-data" class=" table-hover dataTable" width="100%"></table>
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

