<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs"
    Inherits="FingerprintPayment.shop.index" %>

<%@ Register Src="~/UserControls/TeacherAutocomplete.ascx" TagPrefix="uc1" TagName="TeacherAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <link href="../Content/bootstrap-select.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-select.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>--%>
    <%--<script src="../../Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>--%>
      <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <%-- <link href="/Styles/jquery-ui.css" rel="stylesheet" />--%>
    <link href="/Content/bootstrap-toggle.css" rel="stylesheet" />
    <%--    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="createTable.js" type="text/javascript"></script>--%>
    <%--    <script src="/Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />
    <script src="/Scripts/jquery-confirm.js"></script>--%>

    <style type="text/css">
        table.tableSection {
            display: table;
            width: 100%;
        }

            table.tableSection thead, table.tableSection tbody {
                float: left;
                width: 100%;
            }

                table.tableSection thead th {
                    vertical-align: top;
                }

            table.tableSection tbody {
                overflow-y: scroll;
                /* Giving height to make the tbody scroll */
                /* Giving height dynamically is recommended */
                height: 200px;
            }

            table.tableSection tr {
                width: 100%;
                display: table;
                /* Keeping the texts of both thead and tbody in same alignment */
                text-align: left;
            }

            table.tableSection th, table.tableSection td {
                width: 33%;
            }

            table.tableSection tr > td:last-child {
                /* removing fraction of width i.e 2% to align the tbody columns with thead columns. */
                /* It is must as we need to consider the tbody scroll width too */
                /* if the width is in pixels, then (width - 18px) would be enough */
                width: 31%;
            }
            /** for older browsers (IE8), if you know number of columns in your table **/
            table.tableSection tr > td:first-child + td + td {
                width: 31%;
            }

            table.tableSection thead {
                padding-right: 18px; /* 18px is approx. value of width of scroll bar */
                /*width: calc(100% - 20px);*/
            }

        .ui-autocomplete {
            z-index: 9999;
        }

        .toggle.btn {
            height: 40px !important;
            width: 155px !important;
        }

        .toggle-handle {
            width: 0 !important;
            height: 0 !important;
        }
    </style>




    <%--PopUp--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="../Scripts/bootstrap-toggle.js"></script>

    <script type="text/javascript">
        var availableValueEmployees = [];
        var employeeTable;
        $(function () {
            $("#aspnetForm").validate({
                rules: {
                    "shop_name": "required",
                },
                messages: {
                    "shop_name": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601013") %>",
                },
                tooltip_options: {
                    "shop_name": { placement: 'top', trigger: 'focus' },
                },
                submitHandler: function (e) {
                }
            });
        });
    
        $("#btnadd_emp").click(function () {
            var id = TAC.GetUserID();
            var name = TAC.GetUserName();

            if (id) {
                table_emp.push({ "employees_id": id, "employees_name": name });

                randertable();
                TAC.Clear();
            }

            return false;

        });

        $("#modalpopup-data-submit").click(function () {
            if ($("#aspnetForm").valid()) {
                if ($("#table-employees tbody tr").length == 0) {
                    $(".message").show().fadeOut(3000)
                }
                else {
                    var mobile = $("#mobile").prop("checked");
                    var data = {
                        "shop_id": $("#shop_id").val(),
                        "shop_name": $("#shop_name").val(),
                        "mobile": mobile,
                        "employees": table_emp,
                        "deduct": $("#deduct").val(),
                    };
                    $("body").mLoading('show');
                    /*   $.blockUI({ message: '<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101325") %></h1>' });*/
                    $("#modalpopup-data").modal("hide");
                    PageMethods.update_data(data,
                        function (response) {
                            //loaddata();
                            SearchData('data');
                            //$.unblockUI();
                            $("body").mLoading('hide');
                            //popupError("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601024") %>");
                            Swal.fire({
                                type: 'success',
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601024") %>',
                            });
                        },
                        function () {
                            //$.unblockUI();
                            $("body").mLoading('hide');
                            //popupError("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133265") %>");
                            Swal.fire({
                                type: 'error',
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133265") %>',
                            });
                        })
                }
            }
        });
        

        function randertable() {
            //employeeTable.destroy();
            //employeeTable.clear().draw();
            $("#table-employees tbody tr").remove();
            $.each(table_emp, function (index, rows) {
                $("#table-employees tbody").append('<tr>' +
                    '<td style = "width: 20%; text-align: center;" name="index" >' + (index + 1) + '</td > ' +
                    '<td style = "width: 60%; text-align: center;" > ' + rows.employees_name + '</td > ' +
                    '<td class="text-center"><i class="material-icons" style="color: red; cursor: pointer;" onclick="removeRows(' + rows.employees_id + ');">delete</i></td>' +
                    '</tr>');
            });

        }

        function popupadd() {
            clearinput();
            $("#modalpopup-data h3").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601012") %>");
            $("#modalpopup-data").modal("show");
        }

        function popupedit(shop_id) {
            clearinput();
            /*   $.blockUI({ message: '<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101325") %></h1>' });*/
            $("body").mLoading('show');
            PageMethods.get_shop(shop_id,
                function (respones) {
                    /*       console.log(respones);*/
                    respones = $.parseJSON(respones);
                    $("#shop_name").val(respones.shop_name);
                    $("#deduct").val(respones.deduct);
                    $("#deduct").selectpicker('refresh');
                    $("#shop_id").val(respones.shop_id);
                    table_emp = respones.employees;
                    randertable();
                    $("#mobile").bootstrapToggle(respones.mobile === true ? "on" : "off");
                    /*$.unblockUI();*/
                    $("body").mLoading('hide');
                    $("#modalpopup-data h3").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601026") %>");
                    $("#modalpopup-data").modal("show");
                },
                function (respones) {
                    /*console.log(respones);*/
                    /* $.unblockUI();*/
                    /* popupError("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133265") %>");*/
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133265") %>',
                    });
                }
            )

        }

        function clearinput() {
            $("#searchemp").val("");
            $("#deduct").val("");
            $("#shop_name").val("");
            $("#shop_id").val("0");
            $("#table-employees tbody tr").remove();
            table_emp = [];
        }
           
    </script>


    <script>

        function SearchData(t) {

            if (t == 'data') {

                var dt = $('#shop_table').DataTable({
                    "processing": true,
                    "destroy": true,
                    "info": false,
                    paging: true,
                    "pageLength": 20,
                    searching: false,
                    "lengthChange": false,
                    ajax: {
                        url: "/shop/index.aspx/returnlist",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        "dataSrc": function (r) {
                            return r.d.data;
                        },
                        'data': function (d) {

                            d.search = {
                                'wording': $("#txtSearch").val(),
                            };

                            return JSON.stringify(d);
                        },
                    },


                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', "data": "index", "width": "10%", "class": "text-center" },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601005") %>', "data": "shop_name", "width": "30%", "class": "text-center" },
                        {
                            "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %>', "data": "employess", "width": "30%", "class": "text-center",
                            render: function (data, type, row) {
                                var employess = '';
                                row.employess.forEach(function (item) {
                                    var employee = `<p class="my-2">${item.employees_name}</p>`;
                                    employess += employee;
                                })
                                return employess;
                            }
                        },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601009") %>', "data": "deduct", "width": "20%", "class": "text-center" },
                        {
                            "title": '<btn class="btn btn-success" id="btnadd" onclick="popupadd();" data-toggle="modal" data-target="#modalpopproduct"><span><i class="material-icons">add</i>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601011") %></span></btn>', "data": "shop_id", "width": "10%", "class": "text-center",
                            render: function (data, type, row) {
                                return `<div class="dropdown">
                                     <button class="btn btn-success dropdown-toggle" type="button" data-toggle="dropdown">
                                         <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103029") %><span class="caret"></span>
                                     </button>
                                     <ul class="dropdown-menu dropdown-success">
                                         <li><a href='#' tabindex="-1" onclick="popupedit('${row.shop_id}')"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133264") %></a></li>
                                         <li><a tabindex="-1" href="/shop/product-type.aspx?shop_id=${row.shop_id}" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601027") %></a></li>
                                         <li><a tabindex="-1" href="/shop/Product.aspx?shop_id=${row.shop_id}" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601039") %></a></li>
                                         <li><a tabindex="-1" href="/shop/StockList.aspx?shop_id=${row.shop_id}" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601068") %></a></li>
                                         <li style='display:none'><a tabindex="-1" href="${row.image}" download="QRCodeShop_${row.shop_name}.jpg"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00017") %></a></li>
                                     </ul>
                                 </div>`
                            },
                        }

                    ],
                    "order": [[0, 'asc']]
                });

            }
            else if (t == 'report') {
                //var json = JSON.stringify({
                //    //'yearNo': YTLCF.GetYearNo(),
                //    //'term': YTLCF.GetTermID(),
                //    //'termNo': YTLCF.GetTermNo(),
                //    //'level1': YTLCF.GetLevelID(),
                //    //'level2': YTLCF.GetClassID(),
                //    //'name': SAC.GetStudentName(),
                //    //'type': $('#SDQType').val(),
                //});
                //var dt = new Date();
                //var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306021") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                //xhr = new XMLHttpRequest();

                //xhr.open("POST", "/SDQ/Report/ByStudent.aspx/ExportExcel", true);
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

            $("#txtSearch").keypress(function (e) {
                if (e.keyCode === 13) {
                    e.preventDefault();
                    SearchData('data');
                }
            });

            SearchData('data');
        });
    </script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601003") %>
            </p>
        </div>
    </div>

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>

        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6 form-group">
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <label class="col-md-2  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601005") %></label>
                                    <div class="col-md-7">
                                        <input type="text" id="txtSearch" class='form-control' placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601005") %>" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 text-center">
                                <br />
                                <button type="button" id="btnSearch" class="btn btn-info search-btn" onclick="SearchData('data')">
                                    <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header card-header-success card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">shopping_cart</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601006") %></h4>
                    </div>
                    <div class="card-body">
                        <table id="shop_table" class="table" cellspacing="0" cellpadding="2" border="0">
                            <%--<thead>
                                <tr class="headerCell" style="font-weight: bold; font-style: normal; text-decoration: none;">
                                    <th class="center" scope="col" style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>
                                    </th>
                                    <th class="center" scope="col" style="width: 30%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601005") %></th>
                                    <th class="center" scope="col" style="width: 30%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %></th>
                                    <th class="center" scope="col" style="width: 20%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601009") %></th>
                                    <th class="centertext" scope="col" style="width: 10%;">
                                        <a class="btn btn-success text-white" style="cursor: pointer;" onclick="popupadd()">+ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601011") %></a>
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="target"></tbody>--%>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalpopup-data" class="modal fade alertBoxInfo" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header ">
                        <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601012") %></h4>
                    </div>

                    <div class="modal-body product-add-container" id="modalpopup-data-content">
                        <div class="row form-group">
                            <div class="col-1"></div>
                            <div class="col-3 px-0">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601005") %></label>
                            </div>
                            <div class="col-6">
                                <input type="hidden" id="shop_id" value="0" />
                                <input type="text" id="shop_name" name="shop_name" class="form-control" />
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-1"></div>
                            <div class="col-3 px-0">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601009") %></label>
                            </div>
                            <div class="col-6">
                                <select id="deduct" class="selectpicker col-12 px-0" data-style="select-with-transition" width="100%">
                                    <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203071") %></option>
                                    <% for (int i = 1; i <= 100; i++)
                                        {%>
                                    <option value="<%= i %>"><%= i %>%</option>
                                    <%}%>
                                </select>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-1"></div>
                            <div class="col-3 px-0">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601014") %></label>
                            </div>
                            <div class="col-6">
                                <%--<button type="button" class="btn btn-xs btn-secondary btn-toggle active" data-toggle="button" aria-pressed="true" autocomplete="off">
                 <div class="handle"></div>
             </button>--%>
                                <input type="checkbox" class="col-sm-12" id="mobile" data-toggle="toggle" data-onstyle="danger" data-width="250" data-height="40"
                                    data-offstyle="info" data-on="<i class='fa fa-mobile'></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601016") %>" data-off="<i class='fa fa-barcode'></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106042") %>">
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-1"></div>
                            <div class="col-3 px-0">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %></label>
                            </div>
                            <div class="col-6">
                                <%--<input type="text" id="searchemp" class='form-control' placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />--%>
                                <uc1:TeacherAutocomplete runat="server" ID="TeacherAutocomplete" />
                            </div>
                            <div class="col-2 px-0">
                                <button class="btn btn-success py-2 px-3" id="btnadd_emp"><i class="material-icons">add</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-10 mx-auto px-0">
                                <table id="table-employees" class="table tableSection">
                                    <thead>
                                        <tr>
                                            <th style="width: 20%!important; text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th style="width: 60%!important; text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105008") %></th>
                                            <th style="width: 20%!important; text-align: center;"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row message text-center" style="display: none;">
                            <div class="col-12 col-md-12">
                                <label class="text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601021") %></label>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer" style="display: block; text-align: center;">
                        <button type="button" id="modalpopup-data-submit" class="btn btn-primary global-btn">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %></button>
                        <button type="button" id="modalpopup-data-cancel" class="btn btn-danger global-btn"
                            data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </div>
            </div>
        </div>
    </form>


</asp:Content>



