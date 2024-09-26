<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="RestoreItem.aspx.cs" Inherits="FingerprintPayment.RestoreItem" %>

<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <%--<link rel="stylesheet" href="/assets/plugins/datatables/jquery.dataTables.min.css" />--%>
<%--    <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        .dropdown.bootstrap-select {
            width: 99% !important;
        }

        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
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

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type='text/javascript' src="/Scripts/init-function.js?v=<%=DateTime.Now.Ticks%>"></script>

    <script>    

        if (jQuery.validator) { //.messages

            jQuery.extend(jQuery.validator.messages, {
                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M1001003") %>",
            });
        }

        $(function () {

            //md.initFormExtendedDatetimepickers();

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

            $('#date2').on('dp.change', function (e) {
                var d1 = moment($("#date1").val(), 'DD/MM/YYYY');
                var d2 = moment($("#date2").val(), 'DD/MM/YYYY');

                if (d2 < d1) {
                    //alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131144") %>');
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131144") %>',
                        //text: 'Something went wrong!',                      
                    })
                    $("#date2").val('');
                }
            })

            $(".datepicker").keydown(function (e) {
                e.preventDefault();
            });

        });

        function RestoreUser(sid, type) {
            $("body").mLoading();
            $.ajax({
                async: false,
                type: "POST",
                url: "/App_Logic/StudentLimitInContact.ashx",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // console.log(response);
                    if (response.success) {
                        if (response.data.limitInContact > 0 && response.data.remainingNumber > 0) {
                            restoreData(sid, type);
                        }
                        else {
                            systemMessage.LimitInContact(response);
                        }
                    }
                    else {
                        console.log(response);
                    }
                    $("body").mLoading('hide');
                },
                failure: function (response) {
                    console.log(response);
                    $("body").mLoading('hide');
                },
                error: function (response) {
                    console.log(response);
                    $("body").mLoading('hide');
                }
            });

            return false;
        }

        function restoreData(sid, type) {

            Swal.fire({
                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M1001005") %>',
                type: 'question',

                showCloseButton: true,
                showCancelButton: true,
                focusConfirm: false,


                confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201067") %>',
                //confirmButtonAriaLabel: 'Thumbs up, great!',
                cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                //cancelButtonAriaLabel: 'Thumbs down'
            }).then((result) => {
                /* Read more about isConfirmed, isDenied below */
                if (result.value) {

                    //alert(1);
                    PageMethods.RestoreData(sid, type,
                        function (response) {

                            if (response.status == true) {
                                Swal.fire({
                                    type: 'success',
                                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106017") %>',
                                    //text: 'Something went wrong!',                      
                                })

                                onSearch();
                            }
                            else {
                                if (response.text == "duplicate") {
                                    Swal.fire({
                                        type: 'error',
                                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131145") %>',
                                        //text: 'Something went wrong!',                      
                                    })
                                }
                            }
                        },
                        function (response) {
                            Swal.fire({
                                type: 'error',
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132153") %>',
                                //text: 'Something went wrong!',                      
                            })
                        });

                } else if (result.isDenied) {

                }
            });

        }

        function onSearch() {

            if (!$("#aspnetForm").valid()) {
                return;
            }

            var dStart, dEnd;

            if ($("#date1").val() != '')
                dStart = moment($("#date1").val(), 'DD/MM/YYYY').format("YYYYMMDD");//getDate($("#txtstart").val());

            if ($("#date2").val() != '')
                dEnd = moment($("#date2").val(), 'DD/MM/YYYY').format("YYYYMMDD");//getDate($("#txtend").val());
            else
                dEnd = dStart

            var dt = $('#lst-data').DataTable({
                "processing": true,
                //"serverSide": true,
                "destroy": true,
                "info": false,
                paging: true,
                "pageLength": 20,
                searching: false,
                "lengthChange": false,
                //dom: 'Bfrtip',
                "ajax": "./App_Logic/RestoreItem.ashx?type=" + $("#ddlType").val() + "&date1=" + dStart + "&date2=" + dEnd + "&name=" + SAC.GetTypeText(),
                "columns": [
                    { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', data: 'index', "class": "text-center", "width": "7%" },
                    { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M403005") %>", data: 'title', "class": "text-center", "width": "15%" },
                    { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %>", data: 'detail', "width": "20%", "class": "text-center" },
                    { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %>", data: 'date', "width": "15%", "class": "text-center" },
                    { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103165") %>", data: 'editor', "width": "15%", "class": "text-center" },
                    {
                        "title": "", data: 'customCol', "class": "text-center", "width": "15%", "mRender": function (data, type, row) {
                            return "<a href='#' onclick='return RestoreUser(" + row.sid + ", " + row.type + ");' class='btn btn-fill btn-info' ><span class='material-icons'>restore</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131147") %></a> ";
                        }
                    },
                ],
                "order": [[0, 'asc']]
            });

        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803033") %>          
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
                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></label>
                            <div class="col-md-3 ">
                                <select id="ddlType" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></option>
                                </select>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></label>
                            <div class="col-md-3 ">
                                <uc1:StudentAutocomplete runat="server" ID="StudentAutocomplete" />
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101361") %></label>
                            <div class="col-md-3 ">
                                <div class="form-group has-successx">
                                    <input type="text" id="date1" name="date1" class="form-control datepicker" value="" required>
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101362") %></label>
                            <div class="col-md-3 ">
                                <div class="form-group has-successx">
                                    <input type="text" id="date2"  name="date2" class="form-control datepicker" value="" required>
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-center">
                                <br />
                                <button type="button" onclick="onSearch()" class="btn btn-fill btn-info">
                                    <span class="material-icons">search</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-warning  card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">delete</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M1001002") %></h4>
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
