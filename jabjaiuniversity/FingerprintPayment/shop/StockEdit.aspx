<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master"
    CodeBehind="StockEdit.aspx.cs" Inherits="FingerprintPayment.StockEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/ProductAutocomplete.ascx" TagPrefix="uc1" TagName="ProductAutocomplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        .swal2-popup .swal2-title{

        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
        <script src="//cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>
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

            $(".product-amount").mask('0000000');

            $('#template1').on('keyup change', 'input.product-amount,input.product-total', function (e) {
                var $tr = $(this).parents('tr');
                var num = +$tr.find('.product-amount').val();
                var total = +$tr.find('.product-total').val();

                if (typeof num === 'number' && typeof total === 'number') {
                    var v = total / num;
                    $tr.find('.product-price').text(v.toFixed(2));

                    SumFooter($('#sum-price'), $('.product-price'), 'span', 'float');
                    SumFooter($('#sum-amount'), $('.product-amount'), 'input', 'int');
                    SumFooter($('#sum-total'), $('.product-total'), 'input', 'float');
                }
            });

            $('.product-code,.product-name').autoComplete({
                resolver: 'custom',
                minLength: 1,
                events: {
                    search: function (qry, callback) {
                        var data = PAC.GetProductList()
                        if (data != null) {
                            var res = data.filter((v) => {
                                if (v.text.toLowerCase().indexOf(qry.toLowerCase()) > -1 ||
                                    v.code.indexOf(qry) > -1) {
                                    return v;
                                }
                            })
                            acRes = res;
                            callback(res);
                        }
                    }
                }
            });

            $('.product-code,.product-name').on('autocomplete.select', function (evt, item) {
                var $tr = $(this).parents('tr');
                $tr.find('.product-id').val(item.id);
                $tr.find('.product-code').val(item.code);
                $tr.find('.product-name').val(item.text);
                $tr.find('.product-unit').text(item.unit);
                console.log(item);
            }).on('keydown', function (e) {
                if (e.keyCode == 13) {                   
                    if (acRes != null && acRes.length > 0) {
                        var $tr = $(this).parents('tr');
                        $tr.find('.product-id').val(acRes[0].id);
                        $tr.find('.product-code').val(acRes[0].code);
                        $tr.find('.product-name').val(acRes[0].text);
                        $tr.find('.product-unit').text(acRes[0].unit);
                    }
                }
            });

            $('#selectContact').val('<%= ModelData.Stock.ContactID %>');
            $('#selectContact').selectpicker('refresh');
        });

        function SaveForm(isDone) {

            if ($('#aspnetForm').valid() == false) {

                //e.preventDefault();
                //e.stopPropagation();
                return false;
            }

            var countEmptyProduct= $('#template1 tr.editrow .product-id').filter(function () {
                return $(this).val().trim() === '';
            }).length;

            if (countEmptyProduct > 0) {

                Swal.fire({
                    type: 'warning',
                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601085") %>',
                });

                return false;
            }
        
            $('#formsubmit1').prop('disabled', true);
            setTimeout(function () { $('#formsubmit1').prop('disabled', false); }, 4000);

            var invDate = $("#txtInvDate").data("DateTimePicker").date();
            var poDate = $("#txtPODate").data("DateTimePicker").date();
            var stock = {};

            stock.ContactID = $('#selectContact').val();
            stock.INVNo = $('#txtInvNo').val();
            stock.INVDate = invDate != null ? invDate.format("MM/DD/YYYY") : null;
            stock.PONo = $('#txtPONo').val();
            stock.PODate = poDate != null ? poDate.format("MM/DD/YYYY") : null;
            stock.IsDone = isDone;

            var stockList = [];
            $('#template1 tbody tr.editrow').each(function (i) {
                var $tr = $(this);
                var item = {};
                item.nOrder = +$tr.find('.product-no').text();
                item.nNumber = +$tr.find('.product-amount').val();
                item.nProductID = +$tr.find('.product-id').val();
                item.Cost = +$tr.find('.product-price').text();
                item.SumPrice = +$tr.find('.product-total').val();

                stockList.push(item);
            });

            var data = {
                'ID':  '<%=HttpContext.Current.Request.QueryString["id"] %>',
                'ShopID': PAC.GetShopID(),
                'Stock': stock,
                'Detail': stockList,
            };

            // console.log(data);
            $("body").mLoading('show');
            PageMethods.SaveData(data, function (result) {

                if (result.status == 'success') {
                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                        showConfirmButton: false,
                        timer: 1500,
                    }).then((result) => {
                        window.location.reload();
                    });
                } else {
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                    });
                }
                $("body").mLoading('hide');
                return false;
            });

        }

        function ClearForm() {
            window.location.reload();
        }

        function SumFooter($id, $doms, type, type2) {
            var sum = 0;

            $doms.each(function () {
                if (type == 'input') {
                    let value = parseFloat(+$(this).val()) || 0;
                    sum += value;
                }
                else {
                    let value = parseFloat(+$(this).text()) || 0;
                    sum += value;
                }
            });

            if (type2 == 'float') {
                const formattedNumber = new Intl.NumberFormat(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }).format(sum);

                $id.text(formattedNumber);
            } else {
                const formattedNumber = new Intl.NumberFormat(undefined, {
                    minimumFractionDigits: 0,
                    maximumFractionDigits: 0
                }).format(sum);

                $id.text(formattedNumber);
            }
        }

        function onRemoveRow(t) {
            $(t).parents('tr').remove();
        }

        function AddNewRow() {
            var $o = $('#tempRow').clone();
            $o.removeClass('d-none');
            var index = $('#template1 tbody tr').length;
            $o.attr('id', 'row' + index);
            $o.attr('class', 'editrow');
            $o.find('.product-no').text(index);
            var $txtCode = $o.find('.product-code');
            $txtCode.attr('id', 'productcode' + index);
            $txtCode.attr('name', 'productcode' + index);
            var $txtName = $o.find('.product-name');
            $txtName.attr('id', 'productname' + index);
            $txtName.attr('name', 'productname' + index);
            var $txtAmount = $o.find('.product-amount');
            $txtAmount.mask('0000000');
            $txtAmount.attr('id', 'productamount' + index);
            $txtAmount.attr('name', 'productamount' + index);
            var $txtTotal = $o.find('.product-total');
            $txtTotal.attr('id', 'producttotal' + index);
            $txtTotal.attr('name', 'producttotal' + index);

            $txtCode.add($txtName).autoComplete({
                resolver: 'custom',
                minLength: 1,
                events: {
                    search: function (qry, callback) {
                        var data = PAC.GetProductList();
                        if (data != null) {
                            var res = data.filter((v) => {
                                if (v.text.toLowerCase().indexOf(qry.toLowerCase()) > -1 ||
                                    v.code.indexOf(qry) > -1) {
                                    return v;
                                }
                            })
                            acRes = res;
                            callback(res);
                        }
                    }
                }
            });

            $txtCode.add($txtName).on('autocomplete.select', function (evt, item) {
                var $tr = $(this).parents('tr');
                $tr.find('.product-id').val(item.id);
                $tr.find('.product-code').val(item.code);
                $tr.find('.product-name').val(item.text);
                $tr.find('.product-unit').text(item.unit);
                console.log(item);
            }).on('keydown', function (e) {
                if (e.keyCode == 13) {                   
                    if (acRes != null && acRes.length > 0) {           
                        var $tr = $(this).parents('tr');
                        $tr.find('.product-id').val(acRes[0].id);
                        $tr.find('.product-code').val(acRes[0].code);
                        $tr.find('.product-name').val(acRes[0].text);
                        $tr.find('.product-unit').text(acRes[0].unit);
                    }
                }
            });

            $('#template1 tbody').append($o);
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form id="aspnetForm" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Release" />

        <div class="row">
            <div class="col-md-12">
                <p class="text-muted" style="font-size: small;">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601069") %>
                </p>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header card-header-success card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">shopping_cart</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601005") %> <%= ModelData.Shop.shop_name %></h4>

                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601072") %></label>
                            <div class="col-sm-3">
                                <%--<label class="col-form-label text-left"><%= ModelData.Contact %></label>--%>
                                <select id="selectContact" class="selectpicker --req-append-last" data-width="100%" data-size="7" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601073") %>" data-live-search="true" required>
                                    <% foreach (var i in ContactList)
                                        {%>
                                    <option value="<%=i.Value %>"><%=i.Text %></option>
                                    <% } %>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3 ">
                                <div class="d-none">
                                    <uc1:ProductAutocomplete runat="server" ID="ProductAutocomplete" />
                                </div>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601074") %></label>
                            <div class="col-sm-3">
                                <input type="text" name="txtInvNo" id="txtInvNo" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601075") %>" value="<%= ModelData.Stock.INVNo %>" />

                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                            <div class="col-sm-3">
                                <div class="form-group ">
                                    <input type="text" name="txtInvDate" id="txtInvDate" class="datepicker form-control" value="<%= ModelData.Stock.INVDate?.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %>" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601076") %></label>
                            <div class="col-sm-3">
                                <input type="text" name="txtPONo" id="txtPONo" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601077") %>" value="<%= ModelData.Stock.PONo %>" />
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                            <div class="col-sm-3">
                                <div class="form-group ">
                                    <input type="text" name="txtPODate" id="txtPODate" class="datepicker form-control" value="<%= ModelData.Stock.PODate?.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %>" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <table id="template1" class=" table-hover dataTable" width="100%" style="margin: 0 5px;">
                                    <thead>
                                        <tr>
                                            <th width="8%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th width="18%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601079") %></th>
                                            <th width="23%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %></th>
                                            <th width="10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601045") %></th>
                                            <th width="10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601080") %></th>
                                            <th width="13%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></th>
                                            <th width="13%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601082") %></th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="tempRow" class="d-none">
                                            <td class="text-center">
                                                <span class="product-no"></span>
                                            </td>
                                            <td class="">
                                                <input type="text" class="product-code form-control text-center" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601079") %>" autocomplete="off" required />
                                                <input type="hidden" class="product-id" />
                                            </td>
                                            <td>
                                                <input type="text" class="product-name form-control text-center" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %>" autocomplete="off" required />
                                            </td>
                                            <td class="text-center">
                                                <span class="product-unit"></span>
                                            </td>
                                            <td class="text-center">
                                                <span class="product-price"></span>
                                            </td>
                                            <td>
                                                <input type="number" class="product-amount form-control text-center" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %>" required />
                                            </td>
                                            <td>
                                                <input type="number" class="product-total form-control  text-center" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601082") %>" required />
                                            </td>
                                            <td class="text-center">
                                                <a href="#" class="text-danger" onclick="onRemoveRow(this)">
                                                    <span class="btn-label">
                                                        <i class="material-icons">close</i>
                                                    </span>
                                                </a>
                                            </td>
                                        </tr>
                                        <%foreach (var r in ModelData.DetailList)
                                            {  %>
                                        <tr class="editrow" id="row<%=r.Index %>">
                                            <td class="text-center">
                                                <span class="product-no"><%=r.Index %></span>
                                            </td>
                                            <td class="">
                                                <input type="text" class="product-code form-control text-center" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601079") %>" autocomplete="off" required value="<%=r.Barcode %>" />
                                                <input type="hidden" class="product-id" value="<%=r.ProductID %>" />
                                            </td>
                                            <td>
                                                <input type="text" class="product-name form-control text-center" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %>" autocomplete="off" required value="<%=r.Product %>" />
                                            </td>
                                            <td class="text-center">
                                                <span class="product-unit"><%=r.Unit %></span>
                                            </td>
                                            <td class="text-center">
                                                <span class="product-price"><%=r.Cost?.ToString("#,0.##") %></span>
                                            </td>
                                            <td>
                                                <input type="number" class="product-amount form-control text-center" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %>" required value="<%=r.Amount?.ToString("#,0") %>" />
                                            </td>
                                            <td>
                                                <input type="number" class="product-total form-control  text-center" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601082") %>" required value="<%=r.Total?.ToString("#,0.##") %>" />
                                            </td>
                                            <td class="text-center">
                                                <a href="#" class="text-danger" onclick="onRemoveRow(this)">
                                                    <span class="btn-label">
                                                        <i class="material-icons">close</i>
                                                    </span>
                                                </a>
                                            </td>
                                        </tr>
                                        <% }%>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th></th>
                                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></th>
                                            <th></th>
                                            <th></th>
                                            <th class="text-center">
                                                <span id="sum-price">
                                                    <%=ModelData.DetailList.Sum( o => o.Cost)?.ToString("#,0.##") %>
                                                </span>
                                            </th>
                                            <th class="text-center">
                                                <span id="sum-amount">
                                                    <%=ModelData.DetailList.Sum( o => o.Amount)?.ToString("#,0") %>
                                                </span>
                                            </th>
                                            <th class="text-center">
                                                <span id="sum-total">
                                                    <%=ModelData.DetailList.Sum( o => o.Total)?.ToString("#,0.##") %>
                                                </span>
                                            </th>
                                            <th></th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-left">
                                <button type="button" class="btn btn-success" onclick="AddNewRow();">
                                    <span class="btn-label">
                                        <i class="material-icons">add</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106153") %>
                                </button>
                            </div>
                        </div>

                        <div class="row mt-5">
                            <div class="col-md-12 text-center">
                                <button type="button" id="formsubmit2" class="btn btn-info mr-3" onclick="SaveForm(false)">
                                    <span class="btn-label">
                                        <i class="material-icons">save</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601083") %>
                                </button>

                                <button type="button" id="formsubmit1" class="btn btn-success mr-3" onclick="SaveForm(true)">
                                    <span class="btn-label">
                                        <i class="material-icons">save</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601084") %>
                                </button>

                                <button type="button" class="btn btn-danger" onclick="ClearForm()">
                                    <span class="btn-label">
                                        <i class="material-icons">close</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%-- <div class="row">
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
