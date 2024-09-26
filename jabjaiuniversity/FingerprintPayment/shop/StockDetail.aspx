<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master"
    CodeBehind="StockDetail.aspx.cs" Inherits="FingerprintPayment.StockDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">

        //var shop_id = getUrlParameter("shop_id");

        $(document).ready(function () {

            //if (jQuery.validator) {//.messages

            //    jQuery.extend(jQuery.validator.messages, {
            //        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",
            //    });

            //    $("#aspnetForm").validate({  // initialize the plugin

            //        errorPlacement: function (error, element) {
            //            let _class = element.attr('class');

            //            if (_class.includes('--req-append-last')) {
            //                error.insertAfter(element.parent());
            //            }
            //            else {
            //                error.insertAfter(element);
            //            }

            //        }

            //    });
            //}

            //$('.datepicker').datetimepicker({
            //    format: 'DD/MM/YYYY-BE',
            //    locale: 'th',
            //    debug: false,
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

            //$('#template1').on('keyup change', 'input.product-amount,input.product-total', function (e) {
            //    var $tr = $(this).parents('tr');
            //    var num = +$tr.find('.product-amount').val();
            //    var total = +$tr.find('.product-total').val();

            //    if (typeof num === 'number' && typeof total === 'number') {
            //        var v = total / num;
            //        $tr.find('.product-price').text(v.toFixed(2));

            //        SumFooter($('#sum-price'), $('.product-price'), 'span');
            //        SumFooter($('#sum-amount'), $('.product-amount'), 'input');
            //        SumFooter($('#sum-total'), $('.product-total'), 'input');
            //    }
            //});

            //AddNewRow();
        });

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
                        <h4 class="card-title <%= ModelData.Stock.nStock %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601005") %> <%= ModelData.Shop.shop_name %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133271") %> <%= ModelData.Stock.dStock.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("th-TH")) %></h4>

                        <a target="_blank" href="/shop/StockExport.aspx?id=<%=HttpContext.Current.Request.QueryString["id"] %>" class="btn btn-success mr-3 float-right">
                            <span class="btn-label">
                                <i class="material-icons">download</i>
                            </span>
                            Export PDF
                        </a>

                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601072") %></label>
                            <div class="col-sm-3">
                                <label class="col-form-label text-left"><%= ModelData.Contact %></label>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601074") %></label>
                            <div class="col-sm-3">
                                <label class="col-form-label text-left"><%= ModelData.Stock.INVNo %></label>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                            <div class="col-sm-3">
                                <label class="col-form-label text-left"><%= ModelData.Stock.INVDate?.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %></label>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601076") %></label>
                            <div class="col-sm-3">
                                <label class="col-form-label text-left"><%= ModelData.Stock.PONo %></label>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                            <div class="col-sm-3">
                                <label class=" col-form-label text-left"><%= ModelData.Stock.PODate?.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %></label>
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
                                        <%foreach (var r in ModelData.DetailList)
                                            {  %>
                                        <tr>
                                            <td width="8%" class="text-center"><%=r.Index %>.</td>
                                            <td width="18%" class="text-center"><%=r.Barcode %></td>
                                            <td width="23%" class="text-center"><%=r.Product %></td>
                                            <td width="10%" class="text-center"><%=r.Unit %></td>
                                            <td width="10%" class="text-center"><%=r.Cost?.ToString("#,0.00") %></td>
                                            <td width="13%" class="text-center"><%=r.Amount?.ToString("#,0") %></td>
                                            <td width="13%" class="text-center"><%=r.Total?.ToString("#,0.00") %></td>
                                            <td></td>
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
                                                    <%=ModelData.DetailList.Sum( o => o.Cost)?.ToString("#,0.00") %>
                                                </span>
                                            </th>
                                            <th class="text-center">
                                                <span id="sum-amount">
                                                    <%=ModelData.DetailList.Sum( o => o.Amount)?.ToString("#,0") %>
                                                </span>
                                            </th>
                                            <th class="text-center">
                                                <span id="sum-total">
                                                    <%=ModelData.DetailList.Sum( o => o.Total)?.ToString("#,0.00") %>
                                                </span>
                                            </th>
                                            <th></th>
                                        </tr>
                                    </tfoot>
                                </table>
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
