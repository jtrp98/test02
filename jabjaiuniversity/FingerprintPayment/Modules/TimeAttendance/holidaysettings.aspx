<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="holidaysettings.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.holidaysettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>
    <%--   <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style type="text/css">
        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }

        .cover {
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .wordWrap {
            word-wrap: break-word; /* IE 5.5-7 */
            white-space: -moz-pre-wrap; /* Firefox 1.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305069") %>.0 */
            white-space: pre-wrap; /* current browsers */
        }

        .pink {
            background-color: #FFAD9E;
        }

        .green {
            background-color: #77EEB5;
        }

        .blue {
            background-color: #CC9FF9;
        }

        .grey2 {
            background-color: #DEDEDE;
        }

        .yellow {
            background-color: #FFD997;
        }

        .bigfont {
            font-size: 200%;
        }

        .smol {
            font-size: 85%;
        }

        .listItem {
            color: blue;
            background-color: White;
        }

        .hid {
            visibility: hidden;
        }

        .hid2 {
            visibility: hidden;
            display: none;
        }

        .width10 {
            margin: 0 auto;
            width: 10%;
        }

        .centertext {
            text-align: center;
        }

        .righttext {
            text-align: right;
        }

        .lefttext {
            text-align: left;
        }

        .bord {
            border-left: 1px solid #ffffff;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }

        label {
            font-weight: normal;
            font-size: 26px;
        }

        .gvbutton {
            font-size: 25px;
        }

        .nounder a:hover {
            text-decoration: none;
        }

        .button-custom {
            font-size: 26px;
            padding-left: 30px;
            padding-right: 30px;
            width: 100%;
        }

        .shadowblack {
            text-decoration: none;
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .boxhead a {
            color: #FFFFFF;
            text-decoration: none;
        }

        a.imjusttext {
            color: #ffffff;
            text-decoration: none;
        }

            a.imjusttext:hover {
                color: aquamarine;
            }

        .centerText {
            text-align: center;
        }

        .btn-red {
            background: red; /* use your color here */
        }


        .nowrap {
            max-width: 100%;
            white-space: nowrap;
        }

        .width100 {
            margin: 0 auto;
            width: 100%;
        }

        .namemangin {
            margin-left: 5px;
            padding-left: 35px;
            border-left: 10px
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }

        .tab {
            border-collapse: collapse;
            margin-left: 6px;
            margin-right: 6px;
            border-bottom: 3px solid #337AB7;
            border-left: 3px solid #337AB7;
            border-right: 3px solid #337AB7;
            border-top: 3px solid #337AB7;
            box-shadow: inset 0 1px 0 #337AB7;
        }

        table.dataTable tbody tr:last-child td,
        table.dataTable tbody tr:first-child th,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }

        .swal2-popup.swal2-modal{
            width:43em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>  
 <%--   <script src="//cdn.datatables.net/plug-ins/1.10.11/sorting/date-eu.js"></script>--%>
    <script type="text/javascript">

        $.fn.dataTable.moment = function (format, locale) {
            var types = $.fn.dataTable.ext.type;

            // Add type detection
            types.detect.unshift(function (d) {
                return moment(d, format, locale, true).isValid() ? 'moment-' + format : null;
            });

            // Add sorting method - ascending
            types.order['moment-' + format + '-pre'] = function (d) {
                return moment(d, format, locale, true).unix();
            };
        };

        const getDatesBetween = (startDate, endDate) => {
            let dates = [];
            let currentDate = new Date(startDate);
            while (currentDate <= endDate) {
                dates.push(new Date(currentDate));
                currentDate.setDate(currentDate.getDate() + 1);
            }
            return dates;
        };

        $(document).ready(function () {
            //$('.js-example-basic-multiple1').select2();
            //$('.js-example-basic-multiple3').select2();
            //$('.js-example-basic-multiple4').select2();
            //$('.js-example-basic-multiple2').select2();
            $.fn.dataTable.moment('DD/MM/YYYY');

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

            window.onload = ddlchange();
            //var appcolor = document.getElementsByClassName("appcolor");
            //for (var x = 1; x <= appcolor.length; x++) {
            //    paintColor(x);
            //}

            $('#modalStart').on('change', function (e) {
                var $end = $('#modalEnd');
                //if ($end.val() == "")
                $end.val($(this).val());
            });

            $('#editStart').on('change', function (e) {
                var $end = $('#editEnd');
                //if ($end.val() == "")
                $end.val($(this).val());
            });

            $("input[id$='Button1']").click(function () {
                var from = $("#modalStart").val();
                var to = $("#modalEnd").val();
                var reg = /^\d{2}\/\d{2}\/\d{4}$/;
                if ((reg.test(from)) && (reg.test(to))) {
                    var date1 = $("#modalStart").data("DateTimePicker").date(); //moment(from, "DD/MM/YYYY");
                    var date2 = $("#modalEnd").data("DateTimePicker").date();

                    if (date1 <= date2) {

                        Swal.fire({
                            icon: 'warning',
                            title: `<strong ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %></strong>`,
                            html: `
<span stylex="font-size:1.55em"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132573") %> <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00363") %> <%= userData.Name%></strong>
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132574") %>${($('#modalType').val() == "0" ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %>" : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %>")} <br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> ${from}-${to}
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132575") %>
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132576") %>
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132577") %>
<br/><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101259") %>
<br/><br/><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132578") %></strong></span>`,
                            showDenyButton: true,
                            showCancelButton: false,
                            confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %>',
                            denyButtonText: `<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>`,
                        }).then((result) => {
                            /* Read more about isConfirmed, isDenied below */
                            if (result.isConfirmed) {
                                __doPostBack("<%= Button1.UniqueID%>");
                            } else if (result.isDenied) {
                                return false;
                            }
                        });

                        return false;
                    }
                    else {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132579") %>');
                        return false;
                    }
                }
                else {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00043") %>');
                    return false;
                }
            });

            $("input[id$='editSave']").click(function () {
                var from = $("#editStart").val();
                var to = $("#editEnd").val();
                var reg = /^\d{2}\/\d{2}\/\d{4}$/;
                if ((reg.test(from)) && (reg.test(to))) {
                    $("#editStart").trigger('change');
                    $("#editEnd").trigger('change');
                    var _date1 = $("#editStart").data("DateTimePicker").date();
                    var _date2 = $("#editEnd").data("DateTimePicker").date();
                    var _arr1 = getDatesBetween(_date1, _date2);
                            
                    var _old1 = $("#dStartHidden").data("DateTimePicker").date();
                    var _old2 = $("#dEndHidden").data("DateTimePicker").date();
                    var _arr2 = getDatesBetween(_old1, _old2);

                    var arr = _arr2.filter(function (obj) {
                        return !_arr1.map(x => JSON.stringify(x)).includes(JSON.stringify(obj));
                    });
                    var dates = '';
                    if (arr != null) {
                        dates = arr.map(function (x) { return moment(x).add(543, 'years').format('DD/MM/YYYY'); }).join(',');
                    }
                 
                    if (_date1 <= _date2) {
                        Swal.fire({
                            icon: 'warning',
                            title: `<strong ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %></strong>`,
                            html: `
<span stylex="font-size:1.55em"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132573") %> <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00363") %> <%= userData.Name%></strong>
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132580") %>${($('#editType').val() == "0" ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %>" : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %>")} 
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> ${from}-${to}
${ (dates != "" ? `<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132581") %> ${dates} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132583") %>` : `` )}
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132575") %>
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132576") %>
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132577") %>
<br/><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101259") %>
<br/><br/><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132578") %></strong></span>`,
                            showDenyButton: true,
                            showCancelButton: false,
                            confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %>',
                            denyButtonText: `<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>`,
                        }).then((result) => {
                            /* Read more about isConfirmed, isDenied below */
                            if (result.isConfirmed) {
                                __doPostBack("<%= editSave.UniqueID%>");
                                //return false;
                            } else if (result.isDenied) {
                                return false;
                            }
                        });
                        return false;
                    }
                    else {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132579") %>');
                        return false;
                    }

                    return false;
                }
                else {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00043") %>');
                    return false;
                }
            });

            $("input[id$='deleteBtn']").click(function () {
                var from = $("#ctl00_MainContent_Textbox2").val();
                var to = $("#ctl00_MainContent_Textbox3").val();
                var reg = /^\d{2}\/\d{2}\/\d{4}$/;

                Swal.fire({
                    icon: 'warning',
                    title: `<strong ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %></strong>`,
                    html: `
<span stylex="font-size:1.55em"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132573") %> <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00363") %> <%= userData.Name%></strong>
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132584") %>
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> ${from}-${to} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132583") %> 
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132575") %>
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132576") %>
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132577") %>
<br/><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101259") %>
<br/><br/><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132578") %></strong></span>`,
                    showDenyButton: true,
                    showCancelButton: false,
                    confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %>',
                    denyButtonText: `<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>`,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        __doPostBack("<%= deleteBtn.UniqueID%>");
                            } else if (result.isDenied) {
                                return false;
                            }
                        });
                return false;
            });

            onSearch();
        });

        function onSearch() {

            if (!$("#aspnetForm").valid()) {
                return;
            }

            //var dStart, dEnd;

            //if ($("#date1").val() != '')
            //    dStart = moment($("#date1").val(), 'DD/MM/YYYY').format("YYYYMMDD");//getDate($("#txtstart").val());

            //if ($("#date2").val() != '')
            //    dEnd = moment($("#date2").val(), 'DD/MM/YYYY').format("YYYYMMDD");//getDate($("#txtend").val());
            //else
            //    dEnd = dStart
           /* $.fn.dataTable.moment('DD/MM/YY');*/
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
                    url: "holidaysettings.aspx/LoadData",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    "dataSrc": function (r) {
                        return r.d.data;
                    },
                    'data': function (d) {

                        d.search = {
                            'start': $('#startDay').val(),
                            'end': $('#endDay').val(),
                            'type': $('#ddlType').val(),
                            'yearStr': $('#ddlYear').val(),
                        };

                        return JSON.stringify(d);
                    },
                },

                "columns": [
                    { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', data: 'number', "class": "text-center", "width": "5%" },
                    {
                        "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %>", data: 'type', "width": "80px", "class": "text-center",
                        "mRender": function (data, type, row) {
                            return ` <input type="text" class="checktype" style="padding: 0px; padding-left: 15px; width: 100%; font-size: 90%; border: 0px;" value="${row.type}">`;
                        }
                    },
                    { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105085") %>", data: 'start', "class": "text-center", "width": "8%" },
                    { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132562") %>", data: 'end', "class": "text-center", "width": "8%" },
                    { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801016") %>", data: 'name', "class": "text-center" },
                    { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801017") %>", data: 'whoSee', "class": "text-center", "width": "8%" },
                    { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01980") %>", data: 'created', "class": "text-center", "width": "12%" },
                    { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801019") %>", data: 'modify', "class": "text-center", "width": "12%" },

                    //{
                    //    "title": "", data: 'color', "width": "0px", "class": "text-center",
                    //    "mRender": function (data, type, row) {
                    //        return `<input type="text" class="appcolor" style="padding: 0px; width: 1%; border: 0px; " value="${row.color}" />`;
                    //    }
                    //},
                    {
                        "title": "", data: 'color', "width": "1%", "class": "text-center setcolor1",
                        "mRender": function (data, type, row) {
                            return `<input type="text" class="appcolor" style="display:none" value="${row.color}" />
                            <div class="col-md-12 col-sm-12 " style="padding: 0px;">
                                <input type="text" class="setcolor2" style="padding: 0px; padding-left: 15px; width: 100%;border: 0px;">
                            </div>`;
                        }
                    },
                    {
                        "title": `<div class="btn btn-sm btn-success" style="margin-right: 10px;" data-toggle="modal" data-target="#myModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></div>
   <div class="btn btn-sm  btn-info" style="margin-right: 10px;" data-toggle="modal" data-target="#myCopyModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801035") %></div>`,
                        data: 'color', "width": "200px", "class": "text-center",
                        "mRender": function (data, type, row) {
                            return `<div class="row text-center">
                                      <div class="col-md-12 adjust-col-padding col-space nounder">
                                          <div class="fa fa-edit editbox1" onclick="editmodal('${row.nHoliday}')" style="cursor: pointer; font-size: 20px" data-toggle="modal" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>" data-target="#editmodal">
                                          </div>
                                          <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>" class="fa fa-remove" onclick="editmodal('${row.nHoliday}')" style="cursor: pointer; color: red; font-size: 20px" data-toggle="modal" data-target="#deletemodal">
                                              </a>
                                          </div>
                                      </div>
                                  </div>`;
                        }
                    },
                    //{ "title": "", data: 'user', "class": "text-center" },
                    //{ "title": "", data: 'user', "class": "text-center" },
                    /* { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>", data: 'count', "class": "text-center", "width": "10%" },*/
                ],
                //"columnDefs": [{ "targets": 2, "type": "date-eu" }, { "targets": 3, "type": "date-eu" }],
                "aoColumnDefs": [
                    {
                        'bSortable': false, 'aTargets': [8, 9]
                    }
                ],
                //"fnInitComplete": function (oSettings, json) {
                //    var appcolor = document.getElementsByClassName("appcolor");
                //    for (var x = 1; x <= appcolor.length; x++) {
                //        paintColor(x);
                //    }
                //},
                "fnDrawCallback": function (oSettings) {
                    var appcolor = document.getElementsByClassName("appcolor");
                    for (var x = 1; x <= appcolor.length; x++) {
                        paintColor(x);
                    }
                },
                "order": [[0, 'asc']]
            });

        }

        function relink() {
            //var from = document.getElementsByClassName("linkfrom");
            //var to = document.getElementsByClassName("linkto");
            //var date1 = moment($('#startDay').val(), 'DD/MM/YYYY').add(-543, 'years').format('MM/DD/YYYY');
            //var date2 = moment($('#endDay').val(), 'DD/MM/YYYY').add(-543, 'years').format('MM/DD/YYYY');
            //var reg = /^\d{2}\/\d{2}\/\d{4}$/;
            //var date1 = $('#startDay').val();
            //var date2 = $('#endDay').val();
            ////if ((reg.test(from[0].value) || !from[0].value) && (reg.test(to[0].value) || !to[0].value)) {
            //if (date1 != '' && date2 != '') {
            //    //var type = document.getElementsByClassName("linkyear");
            //    //window.location.href = "holidaysettings.aspx?&type=" + type[0].value + "&end=" + to[0].value + "&start=" + from[0].value;
            //    window.location.href = `holidaysettings.aspx?year=${$('#ddlYear').val()}&type=${$('#ddlType').val()}&start=${date1}&end=${date2}`;
            //}
            //else {
            //    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00043") %>');
            //}
            var date1 = $('#startDay').val();
            var date2 = $('#endDay').val();
            window.location.href = `holidaysettings.aspx?year=${$('#ddlYear').val()}&type=${$('#ddlType').val()}&start=${date1}&end=${date2}`;
        }

        function addclasstext(id) {
            //alert(id);
            //$('multi').remove();

            var textBox = document.createClass("label2");
            var textBox2 = document.createElement("br");
            $('#parent').append(textBox);
            $('#parent').append(textBox2);
            var multi = document.getElementsByClassName("multiadd");
            alert(multi.length);
        }

        function addword(_input, _output) {
            if (_input != null) {
                $("[id*=" + _output + "]").val("");
                let _word = "";
                $.each(_input, function (e, s) {
                    _word += (_word == "" ? "" : "/") + s;
                });
                $("[id*=" + _output + "]").val(_word);
            }
        }

        function ddlchange() {

            //$("#ctl00_MainContent_modalClass").html($("#ctl00_MainContent_modalClass option").sort(function (a, b) {
            //    return parseInt($(a).val()) == parseInt($(b).val()) ? 0 : parseInt($(a).val()) < parseInt($(b).val()) ? -1 : 1;
            //}));
            //$("#ctl00_MainContent_modalPlanType").html($("#ctl00_MainContent_modalPlanType option").sort(function (a, b) {
            //    return parseInt($(a).val()) == parseInt($(b).val()) ? 0 : parseInt($(a).val()) < parseInt($(b).val()) ? -1 : 1;
            //}));
            //$("#ctl00_MainContent_modalPlanType2").html($("#ctl00_MainContent_modalPlanType2 option").sort(function (a, b) {
            //    return parseInt($(a).val()) == parseInt($(b).val()) ? 0 : parseInt($(a).val()) < parseInt($(b).val()) ? -1 : 1;
            //}));
            //$("#ctl00_MainContent_modalClass2").html($("#ctl00_MainContent_modalClass2 option").sort(function (a, b) {
            //    return parseInt($(a).val()) == parseInt($(b).val()) ? 0 : parseInt($(a).val()) < parseInt($(b).val()) ? -1 : 1;
            //}));

            var ddltype = document.getElementsByClassName("ddltype");
            var ddlcolor = document.getElementsByClassName("ddlcolor");
            var ddlcolor2 = document.getElementsByClassName("ddlcolor2");
            var ddlevent = document.getElementsByClassName("ddlevent");
            var colorchoose = document.getElementsByClassName("colorchoose");
            var classchoose = document.getElementsByClassName("classchoose");
            var classchoose2 = document.getElementsByClassName("classchoose2");
            var mType = document.querySelectorAll("select.modalType");
            var mType2 = document.querySelectorAll("select.modalType2");

            var mColor = document.getElementsByClassName("modalColor");
            var mColor2 = document.getElementsByClassName("modalColor2");
            var mWho = document.getElementsByClassName("modalWho");
            //alert(mWho[0].value);

            if (mType[0].value == 0) {
                ddlcolor[1].classList.add('d-none');
                ddlcolor[0].classList.remove('d-none');
                mColor[0].selectedIndex = "0";
                $("#myModal [target=select_active]").hide();
            }

            if (mType[0].value == 1) {
                ddlcolor[1].classList.remove('d-none');
                ddlcolor[0].classList.add('d-none');
                mColor[0].selectedIndex = "1";
                $("#myModal [target=select_active]").show();
            }

            if (mType2[0].value == 0) {
                ddlcolor2[1].classList.add('d-none');
                ddlcolor2[0].classList.remove('d-none');
                mColor2[0].selectedIndex = "0";
                $("#editmodal [target=select_active]").hide();
            }

            if (mType2[0].value == 1) {
                ddlcolor2[1].classList.remove('d-none');
                ddlcolor2[0].classList.add('d-none');
                mColor2[0].selectedIndex = "1";
                $("#editmodal [target=select_active]").show();
            }

        }

        function ddlchange2() {

            var ddltype = document.getElementsByClassName("ddltype");
            var ddlcolor = document.getElementsByClassName("ddlcolor");
            var ddlcolor2 = document.getElementsByClassName("ddlcolor2");
            var ddlevent = document.getElementsByClassName("ddlevent");
            var colorchoose = document.getElementsByClassName("colorchoose");
            var classchoose2 = document.getElementsByClassName("classchoose2");
            var classchoose = document.getElementsByClassName("classchoose");
            var mType = document.querySelectorAll("select.modalType");
            var mType2 = document.querySelectorAll("select.modalType2");
            var mColor = document.getElementsByClassName("modalColor");
            var mColor2 = document.getElementsByClassName("modalColor2");
            var mWho = document.getElementsByClassName("modalWho");
            var mWho2 = document.getElementsByClassName("modalWho2");
            //alert(mWho[0].value);

            $(".EmpType, .EmpType_edit").addClass("d-none");
            var v1 = mWho[1].value;
            var v2 = mWho2[1].value;
            if (v1 != 4) {
                classchoose[0].classList.add('d-none');
            }

            if (v1 == 4) {
                classchoose[0].classList.remove('d-none');
            }

            if (v2 != 4) {
                classchoose2[0].classList.add('d-none');
            }

            if (v2 == 4) {
                classchoose2[0].classList.remove('d-none');
            }


            if (v1 != 3) {
                classchoose[1].classList.add('d-none');
            }

            if (v1 == 3) {
                classchoose[1].classList.remove('d-none');
            }

            if (v2 != 3) {
                classchoose2[1].classList.add('d-none');
            }

            if (v2 == 3) {
                classchoose2[1].classList.remove('d-none');
            }

            if (v2 == 5) {
                $(".EmpType_edit").removeClass("d-none");
            }

            if (v1 == 5) {
                $(".EmpType").removeClass("d-none");
            }

        }

       
        function editmodal(id) {
            //alert(id);

            var modalType2 = document.getElementsByClassName("modalType2");
            var modalName2 = document.getElementsByClassName("modalName2");
            var modalStart2 = document.getElementsByClassName("modalStart2");
            var modalEnd2 = document.getElementsByClassName("modalEnd2");
            var modalID = document.getElementsByClassName("modalID");
            modalID[0].value = id;
            var modalColor2 = document.getElementsByClassName("modalColor2");
            var modalWho2 = document.getElementsByClassName("modalWho2");
            var classchoose2 = document.getElementsByClassName("classchoose2");
            var deleteID = document.getElementsByClassName("deleteID");
            deleteID[0].value = id;
            var deleteType = document.getElementsByClassName("deleteType");
            var deleteName = document.getElementsByClassName("deleteName");
            var deleteStart = document.getElementsByClassName("deleteStart");
            var deleteFrom = document.getElementsByClassName("deleteFrom");
            var editmulti = document.getElementsByClassName("editmulti");
            //var data = $('.js-example-basic-multiple2').select2('data');

            classchoose2[0].classList.add('d-none');
            classchoose2[1].classList.add('d-none');

            $.get("/App_Logic/holidayEdit.ashx?id=" + id, function (Result) {
                $.each(Result, function (index, _data) {

                    if (Result[index].whoSee == 3) {
                        classchoose2[1].classList.remove('d-none');
                        //modalWho2[0].value = 4;
                    }
                    if (Result[index].whoSee == 4) {
                        classchoose2[0].classList.remove('d-none');
                        //modalWho2[0].value = 4;
                    }
                    //modalWho2[0].value = Result[index].whoSee;
                    $('#editWho').val(Result[index].whoSee);
                    $('#editWho').selectpicker('refresh');
                  
                    $('#ddlTimeType2').val((Result[index].TimeType || '1'));
                    $('#ddlTimeType2').selectpicker('refresh');

                    $('#editType').val((Result[index].Type));
                    $('#editType').selectpicker('refresh');
                    //modalType2[0].value = Result[index].Type;
                    modalName2[0].value = Result[index].name;
                    //oldDate1 = Result[index].Start;
                    //modalStart2[0].value = Result[index].Start;         
                    $('#editStart').data("DateTimePicker").date(Result[index].Start);                 
                    $('#dStartHidden').data("DateTimePicker").date(Result[index].Start);
                    //oldDate2 = Result[index].End;
                    //modalEnd2[0].value = Result[index].End;
                    $('#editEnd').data("DateTimePicker").date(Result[index].End);                   
                    $('#dEndHidden').data("DateTimePicker").date(Result[index].End);
                    //modalColor2[0].value = Result[index].color;
                    $('#editColor').val((Result[index].color));
                    $('#editColor').selectpicker('refresh');
                    //modalWho2[0].value = Result[index].whoSee;

                    $('#txtTimeType').val((Result[index].TimeType == 2 ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132557") %>" : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132585") %>"));
                    deleteType[0].value = Result[index].Type;
                    deleteName[0].value = Result[index].name;
                    deleteStart[0].value = Result[index].Start;
                    deleteFrom[0].value = Result[index].End;

                    let _word = "";
                    if (Result[index].whoSee == 3 || Result[index].whoSee == 4 || Result[index].whoSee == 5) {

                        let modalClass2 = $("[id*=modalClass2]");
                        let modalPlanType2 = $("[id*=modalPlanType2]");
                        modalClass2.val(null);
                        modalPlanType2.val(null);
                        $.each(_data.LevelId, function (e1, s1) {
                            _word += (_word == "" ? "" : "/") + s1;
                        });
                    }

                    $(".EmpType_edit").addClass("d-none");
                    $('[id*=ddlEmpType_edit]').val(null).trigger('change');
                    if (Result[index].whoSee == 3) {
                        $('[id*=modalClass2]').val(_data.LevelId).trigger("change");
                        $('[id*=editmulticlass_0]').val(_word);
                    }
                    else if (Result[index].whoSee == 4) {
                        $('[id*=modalPlanType2]').val(_data.LevelId).trigger("change");
                        $('[id*=editmulticlass_1]').val(_word);
                    }
                    else if (Result[index].whoSee == 5) {
                        $('[id*=ddlEmpType_edit]').val(_data.LevelId).trigger("change");
                        $('[id*=editmulticlass_2]').val(_word);
                        $(".EmpType_edit").removeClass("d-none");
                    }

                    ddlchange();
                    ddlchange2();

                    if (Result[index].Type === "1") {
                        $("#editmodal [target=select_active]").show();
                        $("[id*=cStatusActive1]").val(Result[index].actvice);
                        $("[id*=cStatusActive1]").selectpicker('refresh');

                    } else {
                        $("#editmodal [target=select_active]").hide();
                    }

                    //$('#editmodal .datepicker').datetimepicker({
                    //    format: 'DD/MM/YYYY-BE',
                    //    locale: 'th',
                    //    debug: false,                     
                    //     icons: {
                    //         time: "fa fa-clock-o",
                    //         date: "fa fa-calendar",
                    //         up: "fa fa-chevron-up",
                    //         down: "fa fa-chevron-down",
                    //         previous: 'fa fa-chevron-left',
                    //         next: 'fa fa-chevron-right',
                    //         today: 'fa fa-screenshot',
                    //         clear: 'fa fa-trash',
                    //         close: 'fa fa-remove'
                    //     }
                    // });
                });
            });
        }

        function paintColor(x) {
            var appcolor = document.querySelectorAll("input.appcolor");
            var setcolor1 = document.querySelectorAll("td.setcolor1");
            var setcolor2 = document.querySelectorAll("input.setcolor2");
            var checktype = document.querySelectorAll("input.checktype");
            if (appcolor[x - 1].value == "") {
                if (checktype[x - 1].value == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %>") {
                    setcolor1[x - 1].classList.add('green');
                    setcolor2[x - 1].classList.add('green');
                }
                else {
                    setcolor1[x - 1].classList.add('pink');
                    setcolor2[x - 1].classList.add('pink');
                }
            }
            if (appcolor[x - 1].value == 0) {
                setcolor1[x - 1].classList.add('pink');
                setcolor2[x - 1].classList.add('pink');
            }
            else if (appcolor[x - 1].value == 1) {
                setcolor1[x - 1].classList.add('green');
                setcolor2[x - 1].classList.add('green');
            }
            else if (appcolor[x - 1].value == 2) {
                setcolor1[x - 1].classList.add('blue');
                setcolor2[x - 1].classList.add('blue');
            }
            else if (appcolor[x - 1].value == 3) {
                setcolor1[x - 1].classList.add('grey2');
                setcolor2[x - 1].classList.add('grey2');
            }
            else if (appcolor[x - 1].value == 4) {
                setcolor1[x - 1].classList.add('yellow');
                setcolor2[x - 1].classList.add('yellow');
            }
            //appcolor[x - 1].value = "";
        }

       <%-- function ConfirmAlert( date1 , date2) {
            Swal.fire({
                icon: 'warning',
                title: `<strong ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %></strong>`,
                html: `
<span stylex="font-size:1.55em"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132573") %> <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00363") %> <%= userData.Name%></strong>
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132587") %> ${date1}-${date2}
<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132586") %>
<br/><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101259") %>
<br/><br/><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132578") %></strong></span>`,
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %>',
                denyButtonText: `<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>`,
            }).then((result) => {
                /* Read more about isConfirmed, isDenied below */
                if (result.isConfirmed) {
                    $('#form1').submit();
                } else if (result.isDenied) {
                    return false;
                }
            })
        }--%>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801011") %>          
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
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="ddlYear" ClientIDMode="Static" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <%-- <% for (int i = DateTime.Today.Year + 1; i >= 2015; i--)
                                        {%>
                                    <option selected value="<%= i %>"><%= i + 543 %></option>
                                    <%}%>--%>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"></label>
                            <div class="col-md-3 ">
                            </div>
                            <div class="col-md-2"></div>

                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105085") %></label>
                            <div class="col-md-3 ">
                                <div class="form-group has-successx">
                                    <%--   <input type="text" id="date1" name="date1" class="form-control datepicker" value="" required>--%>
                                    <asp:TextBox ID="startDay" runat="server" ClientIDMode="static" CssClass="form-control linkfrom datepicker" Width="100%" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>

                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106102") %></label>
                            <div class="col-md-3 ">
                                <div class="form-group has-successx">
                                    <%-- <input type="text" id="date2" name="date2" class="form-control datepicker" value="" required>--%>
                                    <asp:TextBox ID="endDay" runat="server" ClientIDMode="static" CssClass="form-control linkto datepicker" Width="100%" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>

                            </div>
                            <div class="col-md-2"></div>

                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801014") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="ddlType" ClientIDMode="Static" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <asp:ListItem Selected="True" Value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></asp:ListItem>
                                    <asp:ListItem Value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %></asp:ListItem>
                                    <asp:ListItem Value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %></asp:ListItem>
                                </asp:DropDownList>
                                <%--  <asp:DropDownList ID="DropDownList1" runat="server" class="form-control linkyear">
                        </asp:DropDownList>--%>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"></label>
                            <div class="col-md-3 ">
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
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %></h4>
                    </div>

                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12">
                                <table id="lst-data" class=" table-hover dataTable" width="100%"></table>
                            </div>
                            <div class="col-md-12" style="overflow-y: auto; height: 250px;">
                                <div class="row">
                                    <% foreach (var l in LOGS)
                                        {  %>
                                    <label class="col-12 col-form-label text-left">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> <%=l.Date.Value.ToString("dd/MM/yyyy <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> HH:mm <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %>",new System.Globalization.CultureInfo("th")) %>
                                        <%=l.Detail %>
                                    </label>
                                    <label class="col-12 col-form-label text-left">
                                        <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132591") %> <%= string.IsNullOrWhiteSpace(l.ByName)? "-" : l.ByName%></strong>
                                    </label>
                                    <% } %>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="modal fade" id="modalDetail" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132054") %></h2>
                    </div>
                    <div class="modal-body">
                        <div class="col-xs-12" style="padding: 5px;">
                            <div id="parent" class="col-xs-12">
                                <asp:TextBox ID="Textbox4" class="form-control wordWrap multiadd" runat="server" Width="80%"></asp:TextBox>
                            </div>
                        </div>

                        <div class="hid" style="font-size: 30%">d-none</div>
                    </div>
                    <div class="modal-footer">

                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801020") %></h4>
                    </div>
                    <div class="modal-body">
                        <%-- <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503004") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="ddlTimeType" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132585") %>" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132557") %>" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>--%>

                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801021") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="modalType" ClientIDMode="Static" runat="server" class="modalType selectpicker" data-style="select-with-transition" data-width="100%" data-size="7" onchange="ddlchange()">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %>" Value="0" class="grey ddlevent" onchange="ddlchange()"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %>" Value="1" class="grey ddlevent" onchange="ddlchange()"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row" style="padding: 5px; display: none;" target="select_active">
                            <label class="col-md-5  col-form-label text-left">ปรับสถานะกิจกรรมอัติโนมัติ</label>
                            <div class="col-md-7 ">
                                <asp:DropDownList class="selectpicker" runat="server" ID="cStatusActive0" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <asp:ListItem Value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132588") %></asp:ListItem>
                                    <asp:ListItem Value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132589") %></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801016") %></label>
                            <div class="col-md-7 ">
                                <asp:TextBox ID="modalPlanName" class="form-control modalName" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105085") %></label>
                            <div class="col-md-7 ">
                                <div class="form-group has-successx">
                                    <asp:TextBox ID="modalStart" runat="server" ClientIDMode="static" CssClass="form-control linkfrom modalStart datepicker" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106102") %></label>
                            <div class="col-md-7 ">
                                <div class="form-group has-successx">
                                    <asp:TextBox ID="modalEnd" runat="server" ClientIDMode="static" CssClass="form-control linkfrom modalEnd datepicker" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801022") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="modalColor" runat="server" class="modalColor selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801059") %>" Value="0" class="grey ddlcolor pink"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801060") %>" Value="1" class="grey ddlcolor green"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801025") %>" Value="2" class="grey ddlcolor blue"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801061") %>" Value="4" class="grey ddlcolor yellow"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801027") %>" Value="3" class="grey ddlcolor grey2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801017") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="modalWho" runat="server" class=" modalWho selectpicker" data-style="select-with-transition" data-width="100%" data-size="7" onchange="ddlchange2()">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801028") %>" Value="0" class="grey ddlwho" onchange="ddlchange2()"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801062") %>" Value="1" class="grey ddlwho" onchange="ddlchange2()"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801030") %>" Value="5" class="grey ddlwho2" onchange="ddlchange2()"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801031") %>" Value="2" class="grey ddlwho" onchange="ddlchange2()"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801032") %>" Value="4" class="grey ddlwho" onchange="ddlchange2()"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801033") %>" Value="3" class="grey ddlwho" onchange="ddlchange2()"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row EmpType d-none">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="ddlEmpType" runat="server" onchange="addword($(this).val(),'multiclassTxt3')" classx="form-control modalWho " data-style="select-with-transition" data-width="100%" data-size="7" CssClass="js-example-basic-multiple3 selectpicker" multiple="multiple" name="ddlEmpType[]">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="row classchoose d-none">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132568") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="modalPlanType" onchange="addword($(this).val(),'multiclassTxt2')" runat="server" Width="80%" CssClass="js-example-basic-multiple1 selectpicker" data-style="select-with-transition" data-width="100%" data-size="7" name="classchoice[]" multiple="multiple">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row classchoose d-none">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132592") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="modalClass" onchange="addword($(this).val(),'multiclassTxt1')" runat="server" Width="80%" CssClass="js-example-basic-multiple3 selectpicker" data-style="select-with-transition" data-width="100%" data-size="7" name="classchoice3[]" multiple="multiple">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-12 d-none" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label>xxxxxx</label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="multiclassTxt1" class="form-control modalMultiClass" runat="server" Width="80%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12  d-none" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label>xxxxxx2</label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="multiclassTxt2" class="form-control modalMultiClass2" runat="server" Width="80%"></asp:TextBox>
                                <asp:TextBox ID="multiclassTxt3" class="form-control modalMultiClass2" runat="server" Width="80%"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="Button1" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" ClientIDMode="Static" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myCopyModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801036") %></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201040") %></label>
                            <div class="col-md-7 ">
                                <label class="col-md-5 col-form-label text-left"><%=CURRENT_YEAR %></label>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801038") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="ddlYearCopy" ClientIDMode="Static" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="btnCopy" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801035") %>" ClientIDMode="Static" OnClick="btnCopy_Click" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="deletemodal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802056") %></h4>
                    </div>
                    <div class="modal-body">

                        <div class="col-xs-12 d-none" style="padding: 4px;">
                            <div class="col-xs-4 righttext">
                                <label>
                                    id
                                </label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="deleteid" runat="server" ClientIDMode="static" CssClass="form-control linkfrom deleteID" Width="80%" />
                            </div>
                        </div>

                        <%--   <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503004") %></label>
                            <div class="col-md-7 ">
                                <asp:TextBox ID="txtTimeType" ClientIDMode="static" CssClass="form-control " Enabled="false" runat="server" Width="80%"></asp:TextBox>
                            </div>
                        </div>--%>

                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801021") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control deleteType" Enabled="false" Width="80%" onchange="ddlchange()">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %>" Value="0" class="grey ddlevent2"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %>" Value="1" class="grey ddlevent2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801016") %></label>
                            <div class="col-md-7 ">
                                <asp:TextBox ID="Textbox1" CssClass="form-control deleteName" Enabled="false" runat="server" Width="80%"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105085") %></label>
                            <div class="col-md-7 ">
                                <asp:TextBox ID="Textbox2" CssClass="form-control deleteStart" Enabled="false" runat="server" Width="80%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106102") %></label>
                            <div class="col-md-7 ">
                                <asp:TextBox ID="Textbox3" CssClass="form-control deleteFrom" Enabled="false" runat="server" Width="80%"></asp:TextBox>
                            </div>
                        </div>

                        <div class="hid" style="font-size: 30%">d-none</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="deleteBtn" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="editmodal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801039") %></h4>
                    </div>
                    <div class="modal-body">

                        <%--   <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503004") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="ddlTimeType2" ClientIDMode="Static" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132585") %>" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132557") %>" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>--%>

                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801021") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="editType" ClientIDMode="Static" runat="server" class=" modalType2 selectpicker" data-style="select-with-transition" data-width="100%" data-size="7" onchange="ddlchange()">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %>" Value="0" class="grey ddlevent2" onchange="ddlchange()"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %>" Value="1" class="grey ddlevent2" onchange="ddlchange();"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row" style="display: none;" target="select_active">
                            <label class="col-md-5  col-form-label text-left">ปรับสถานะกิจกรรมอัติโนมัติ</label>
                            <div class="col-md-7 ">
                                <asp:DropDownList class=" selectpicker" runat="server" ID="cStatusActive1" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <asp:ListItem Value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132588") %></asp:ListItem>
                                    <asp:ListItem Value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132589") %></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801016") %></label>
                            <div class="col-md-7 ">
                                <asp:TextBox ID="editName" class="form-control modalName2" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                             <input type="text" id="dStartHidden" class="datepicker d-none"  />       
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105085") %></label>
                            <div class="col-md-7 ">
                                     
                                <div class="form-group has-successx">                                                           
                                    <asp:TextBox ID="editStart" runat="server" ClientIDMode="static" CssClass="form-control linkfrom modalStart2 datepicker" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>

                            </div>
                        </div>
                        <div class="row">
                             <input type="text" id="dEndHidden" class="datepicker d-none"  />
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106102") %></label>
                            <div class="col-md-7 ">                                
                                <div class="form-group has-successx">                                   
                                    <asp:TextBox ID="editEnd" runat="server" ClientIDMode="static" CssClass="form-control linkfrom modalEnd2 datepicker" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row d-none">
                            <label class="col-md-5  col-form-label text-left">id</label>
                            <div class="col-md-7 ">
                                <asp:TextBox ID="editid" runat="server" ClientIDMode="static" CssClass="form-control linkfrom modalID" Width="50%" />
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801022") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="editColor" runat="server" ClientIDMode="Static" class=" modalColor2 selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801059") %>" Value="0" class="grey ddlcolor2 pink"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801060") %>" Value="1" class="grey ddlcolor2 green"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801025") %>" Value="2" class="grey ddlcolor2 blue"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801061") %>" Value="4" class="grey ddlcolor2 yellow"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801027") %>" Value="3" class="grey ddlcolor2 grey2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801017") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="editWho" ClientIDMode="Static" runat="server" class=" modalWho2 selectpicker" data-style="select-with-transition" data-width="100%" data-size="7" onchange="ddlchange2()">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801028") %>" Value="0" class="grey ddlwho2" onchange="ddlchange2()"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132590") %>" Value="1" class="grey ddlwho2" onchange="ddlchange2()"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801030") %>" Value="5" class="grey ddlwho2" onchange="ddlchange2()"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801031") %>" Value="2" class="grey ddlwho2" onchange="ddlchange2()"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801032") %>" Value="4" class="grey ddlwho2" onchange="ddlchange2()"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801033") %>" Value="3" class="grey ddlwho2" onchange="ddlchange2()"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row EmpType_edit d-none">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="ddlEmpType_edit" onchange="addword($(this).val(),'editmulticlass_2')" runat="server" class=" modalWho2 " data-style="select-with-transition" data-width="100%" data-size="7" CssClass="js-example-basic-multiple3 selectpicker" multiple="multiple" name="ddlEmpType_edit[]">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row classchoose2 d-none">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132568") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="modalPlanType2" onchange="addword($(this).val(),'editmulticlass_1')" runat="server" Width="80%" CssClass="js-example-basic-multiple2 selectpicker" data-style="select-with-transition" data-width="100%" data-size="7" name="classchoice2[]" multiple="multiple">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row classchoose2 d-none">
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132592") %></label>
                            <div class="col-md-7 ">
                                <asp:DropDownList ID="modalClass2" onchange="addword($(this).val(),'editmulticlass_0')" runat="server" Width="80%" CssClass="js-example-basic-multiple4 selectpicker" data-style="select-with-transition" data-width="100%" data-size="7" name="classchoice4[]" multiple="multiple">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row  d-none">
                            <label class="col-md-5  col-form-label text-left"></label>
                            <div class="col-md-7 ">
                                <asp:TextBox ID="editmulticlass_0" class="form-control editmulti" runat="server" Width="80%"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row  d-none">
                            <label class="col-md-5  col-form-label text-left"></label>
                            <div class="col-md-7 ">
                                <asp:TextBox ID="editmulticlass_1" class="form-control editmulti2" runat="server" Width="80%"></asp:TextBox>
                                <asp:TextBox ID="editmulticlass_2" class="form-control editmulti2" runat="server" Width="80%"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row  d-none">
                            <label class="col-md-5  col-form-label text-left"></label>
                            <div class="col-md-7 ">
                            </div>
                        </div>


                        <div class="hid" style="font-size: 30%">d-none</div>
                    </div>
                    <div class="modal-footer">

                        <asp:Button CssClass="btn btn-primary global-btn" ID="editSave" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />

                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>


