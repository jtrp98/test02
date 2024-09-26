<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master"
    CodeBehind="StockCard.aspx.cs" Inherits="FingerprintPayment.StockCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/ProductAutocomplete.ascx" TagPrefix="uc1" TagName="ProductAutocomplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">

        //var shop_id = getUrlParameter("shop_id");
        var acRes;
        $(document).ready(function () {

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

            $('#selectShop').on('change', function () {
                $("body").mLoading('show');
                PAC.LoadProduct($(this).val());
                $("body").mLoading('hide');
            });

        });

        function onSearch(t) {
            if ($('#aspnetForm').valid() == false) {
                return false;
            }
            $('#template1').show();
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
                        url: "/shop/Report/StockCard.aspx/OnSearchReport",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        "dataSrc": function (r) {
                            return r.d.data;
                        },
                        'data': function (d) {

                            d.model = {
                                'ShopID': $('#selectShop').val(),
                                'ProductID': PAC.GetProductID(),
                                'Date1': $("#txtDate1").data("DateTimePicker").date().format("MM/DD/YYYY"),
                                'Date2': $("#txtDate2").data("DateTimePicker").date().format("MM/DD/YYYY"),
                            };

                            return JSON.stringify(d);
                        },
                    },


                    "columns": [
                        { data: 'index', "class": "text-center", "width": "5%" },
                        { data: 'date', "class": "text-center", "width": "12%", "orderable": false },
                        { data: 'doc', "class": "text-center", "width": "15%", "orderable": false },
                        { data: 'type', "class": "text-center", "orderable": false },
                        { data: 'cost', "class": "text-center", "orderable": false },
                        { data: 'amountIn', "class": "text-center", "orderable": false },
                        { data: 'totalIn', "class": "text-center", "orderable": false },

                        { data: 'amountOut', "class": "text-center", "orderable": false },
                        { data: 'totalOut', "class": "text-center", "orderable": false },

                        { data: 'costAvg', "class": "text-center", "orderable": false },
                        { data: 'amountNet', "class": "text-center", "orderable": false },
                        { data: 'totalNet', "class": "text-center", "orderable": false },
                    ],
                    "order": [[0, 'asc']],
                    "fnInitComplete": function (oSettings, json) {
                        var p = json.d.summary;
                        $('#amountIn').text(p.amountIn);
                        $('#totalIn').text(p.totalIn);

                        $('#amountOut').text(p.amountOut);
                        $('#totalOut').text(p.totalOut);

                        $('#costNet').text(p.costNet);
                        $('#amountNet').text(p.amountNet);
                        $('#totalNet').text(p.totalNet);
                    }
                });
            }
            else if (t == 'pdf') {

                var json = ({
                    'ShopID': $('#selectShop').val(),
                    'ProductID': PAC.GetProductID(),
                    'Date1': $("#txtDate1").data("DateTimePicker").date().format("MM/DD/YYYY"),
                    'Date2': $("#txtDate2").data("DateTimePicker").date().format("MM/DD/YYYY"),
                });

                var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133254") %>_' + moment().format('DDMMYYYYhmmss') + '.pdf';

                xhr = new XMLHttpRequest();

                xhr.open("POST", "/Shop/Report/StockCard.aspx/ExportPdf", true);
                xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                xhr.responseType = 'blob';
                xhr.onload = function () {
                    //aa = xhr.getResponseHeader("filename");
                    saveAs(xhr.response, file_name);
                    //$("body").mLoading('hide');
                };
                xhr.send(JSON.stringify({ 'model': json }));

            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form id="aspnetForm" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Release" />

        <div class="row">
            <div class="col-md-12">
                <p class="text-muted" style="font-size: small;">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133252") %>
                </p>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header card-header-success card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601003") %> :</label>
                            <div class="col-sm-3">
                                <select name="selectShop" id="selectShop" class="selectpicker --req-append-last" data-width="100%" data-size="7" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133253") %>" data-live-search="true" required>
                                    <% foreach (var i in ShopList)
                                        {%>
                                    <option value="<%=i.Value %>"><%=i.Text %></option>
                                    <% } %>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3">
                                <div class="d-none">
                                </div>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %></label>
                            <div class="col-sm-3">
                                <uc1:ProductAutocomplete runat="server" ID="ProductAutocomplete" IsRequired="true" />
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :</label>
                            <div class="col-sm-3">
                                <div class="form-group ">
                                    <input type="text" name="txtDate1" id="txtDate1" class="datepicker form-control" value="" required />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                            <div class="col-sm-3">
                                <div class="form-group ">
                                    <input type="text" name="txtDate2" id="txtDate2" class="datepicker form-control" value="" required />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row mt-2">
                            <div class="col-md-12 text-center">
                                <button type="button" id="formsubmit1" class="btn btn-info mr-3" onclick="onSearch('data')">
                                    <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                                <button type="button" class="btn btn-success mr-3" onclick="onSearch('pdf')">
                                    <span class="btn-label">
                                        <i class="material-icons">download</i>
                                    </span>
                                    Export
                                </button>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card">
                    <div class="card-header card-header-warning card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133254") %></h4>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <table id="template1" class=" table-hover dataTable" width="100%" style="margin: 0 5px;display:none">
                                    <thead>
                                        <tr>
                                            <th width="5%" class="text-center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th width="10%" class="text-center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></th>
                                            <th width="10%" class="text-center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133255") %></th>
                                            <th width="10%" class="text-center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133258") %></th>
                                            <th width="10%" class="text-center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603016") %></th>

                                            <th class="text-center" width="18%" colspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133256") %></th>
                                            <th class="text-center" width="18%" colspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133257") %></th>
                                            <th class="text-center" width="27%" colspan="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405006") %></th>
                                        </tr>
                                        <tr>
                                            <th class="text-center" width="8%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></th>
                                            <th class="text-center" width="8%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133259") %></th>

                                            <th class="text-center" width="8%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></th>
                                            <th class="text-center" width="8%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133259") %></th>

                                            <th class="text-center" width="8%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133260") %></th>
                                            <th class="text-center" width="8%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></th>
                                            <th class="text-center" width="8%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133259") %></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></th>
                                            <th></th>

                                            <th class="text-center">
                                                <span id="amountIn"></span>
                                            </th>
                                            <th class="text-center">
                                                <span id="totalIn"></span>
                                            </th>

                                            <th class="text-center">
                                                <span id="amountOut"></span>
                                            </th>
                                            <th class="text-center">
                                                <span id="totalOut"></span>
                                            </th>


                                            <th class="text-center">
                                                <%--         <span id="costNet"></span>--%>
                                            </th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>

                                            <th></th>
                                            <th></th>

                                            <th></th>
                                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133261") %></th>

                                            <th class="text-center">
                                                <span id="costNet"></span>
                                            </th>
                                            <th class="text-center">
                                                <span id="amountNet"></span>
                                            </th>
                                            <th class="text-center">
                                                <span id="totalNet"></span>
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--     <div class="row">
            <div class="col-md-12">
                <div class="card card1">
                    <div class="card-body mt-3">
                        <h4 class="show-history"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102185") %></h4>
                        <div class="mt-3 d-none history-content">
                          
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
    </form>

</asp:Content>
