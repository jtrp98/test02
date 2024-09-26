<%@ Page Title="" Language="C#" MasterPageFile="~/mp_notFrom.Master" AutoEventWireup="true" CodeBehind="paymentMethod.aspx.cs" Inherits="FingerprintPayment.Modules.Invoices.paymentMethod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        input {
            padding-left: 5px;
        }

        .content-required:after {
            color: red;
            content: " *";
        }

        .modal-font-size-default {
            font-size: 20px;
        }

        .content-container {
            padding: 30px 0;
            font-size: 20px;
        }

        #GridViewInvoices thead tr th {
            text-align: center !important;
        }

        .payment-gateway-summary {
            /*text-align: left;*/
            /*height: 35vh;*/
            border-right: 1px solid #DDDDDD;
        }

        .payment-gateway-right {
            bottom: 0;
            right: 0;
        }

        ul.no-bullet {
            list-style: none;
        }

        ul.invoice-result-box {
            padding: 0;
            list-style: none;
        }

            ul.invoice-result-box li {
                text-align: right;
            }

        .line-payment-summary {
            width: 100%;
            height: 1px;
            background: #000;
            float: right;
            margin-top: 3px;
        }

        .select2-results {
            font-size: 20px;
        }

        .daterangepicker {
            font-size: 18px !important;
        }

            .daterangepicker select.monthselect, .daterangepicker select.yearselect {
                font-size: 18px !important;
            }

        .applyBtn, .cancelBtn {
            font-size: 20px;
        }

        .ball-green {
            color: #1dd1a1;
        }
    </style>
    <link href="../../Content/select2/select2.min.css" rel="stylesheet" />
    <script src="../../Scripts/select2/select2.full.min.js"></script>
    <script src="../../Scripts/moment.js"></script>
    <link href="../../Assets/plugins/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../../Assets/plugins/bootstrap-daterangepicker/daterangepicker.js"></script>
    <link href="../../bootstrap%20SB2/bower_components/datatables/media/css/jquery.dataTables.css" rel="stylesheet" />
    <link href="../../Content/custom/custom-datatable.css" rel="stylesheet" />
    <link href="../../Content/jquery-confirm.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <form id="invoice-form">
            <div class="tab-content">
                <div id="all-list" class="tab-pane fade in active">
                    <%--<span class="fa fa-circle ball-green "></span>--%>
                    <table class="table table-striped table-bordered table-hover table-checkable order-column" id="allSearch" style="width: 100%">
                        <thead>
                            <tr>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603083") %></th>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132508") %></th>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02793") %></th>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132509") %></th>
                                <%--     <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> </th>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501032") %> </th>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501033") %> </th>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603022") %></th>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501034") %></th>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> </th>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132493") %> </th>
                                <th></th>
                                <th></th>
                                <th></th>--%>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></th>
                                <th class="fix-right"></th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </form>
    </div>

    <!-- Modal -->
    <div id="modal-add-paymentmethod" class="modal fade modal-font-size-default" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01253") %></b></h3>
                </div>
                <div class="modal-body" style="min-height: 40vh">
                    <div class="modal-form">
                        <ul class="no-bullet" id="ul-paymentmethod">
                            <li>
                                <div class="row">
                                    <label class="col-md-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></label>
                                    <div class="col-md-9">
                                        <div class="radio" style="padding: 0 5px; margin: 0; float: left;">
                                            <label>
                                                <input type="radio" name="rdPaymentType" value="1" checked="checked" />
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504014") %>
                                            </label>
                                        </div>
                                        <div class="radio" style="padding: 0 5px; margin: 0; float: left;">
                                            <label>
                                                <input type="radio" name="rdPaymentType" value="2" />
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504015") %>
                                           
                                            </label>
                                        </div>
                                        <%--<div class="radio" style="padding: 0 5px; margin: 0; float: left;">
                                            <label>
                                                <input type="radio" name="rdPaymentType" value="3" />
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132489") %>
                                           
                                            </label>
                                        </div>--%>
                                    </div>
                                </div>
                            </li>
                            <li style="padding-top: 5px" class="li-paymentmethod" data-paymentmethod="1">
                                <form id="payment-method-1">
                                    <input type="hidden" name="PaymentMethodTypeId" value="1" />
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-md-3 content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                            <div class="col-md-9">
                                                <input type="text" name="PaymentMethodName" class="form-control" required="required" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></label>
                                        <div class="col-md-9">
                                            <textarea class="form-control" name="Note" cols="5" rows="3"></textarea>
                                        </div>
                                    </div>
                                </form>
                            </li>
                            <li style="padding-top: 5px" class="li-paymentmethod hide" data-paymentmethod="2">
                                <form id="payment-method-2">
                                    <div class="form-group">
                                        <input type="hidden" name="PaymentMethodTypeId" value="2" />
                                        <div class="row">
                                            <label class="col-md-3 content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504015") %></label>
                                            <div class="col-md-9">
                                                <select class="form-control select2" id="ddl-bank" name="BankId" style="width: 60%" required="required">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <%--     <div class="row">
                                    <label class="col-md-3 content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603083") %></label>
                                    <div class="col-md-9">
                                        <select class="form-control" id="ddl-bank-type" name="BankAccountType" style="width: 60%">
                                            <option><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132490") %></option>
                                            <option><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132492") %></option>
                                            <option><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132491") %></option>
                                        </select>
                                    </div>
                                </div>--%>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-md-3 content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02793") %></label>
                                            <div class="col-md-9">
                                                <input type="text" name="PaymentMethodName" class="form-control" required="required" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-md-3 content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02779") %></label>
                                            <div class="col-md-9">
                                                <input type="text" name="BankNumber" class="form-control" required="required" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></label>
                                        <div class="col-md-9">
                                            <textarea class="form-control" name="Note" cols="5" rows="3"></textarea>
                                        </div>
                                    </div>
                                </form>
                            </li>
                            <li style="padding-top: 5px" class="li-paymentmethod hide" data-paymentmethod="3">
                                <form id="payment-method-3">
                                    <div class="form-group">
                                        <input type="hidden" name="PaymentMethodTypeId" value="3" />
                                        <div class="row">
                                            <label class="col-md-3 content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %></label>
                                            <div class="col-md-9">
                                                <input type="text" class="form-control" name="PaymentMethodName" required="required" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-md-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></label>
                                            <div class="col-md-9">
                                                <textarea class="form-control" name="Note" cols="5" rows="3"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </form>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01417") %></button>
                    <button type="button" class="btn btn-primary" id="btnSavePaymentMethod"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02111") %></button>
                </div>
            </div>
        </div>
    </div>

    <script src="/Scripts/jquery.blockUI.js"></script>
    <script type="text/javascript">
        var masterDataURL = "<%=ResolveUrl("~/Handles/Utilities/MasterDataHandle.ashx")%>";
        var ajaxSearchHandlerUrl = "<%=ResolveUrl("~/Handles/Invoices/PaymentMethodListHandler.ashx")%>";
        var savePaymentMethodUrl = "<%=ResolveUrl("~/Handles/Invoices/InvoicePaymentMethodHandler.ashx")%>";
    </script>
    <script src="/bootstrap%20SB2/bower_components/datatables/media/js/jquery.dataTables.js"></script>
    <script src="/Scripts/jquery-confirm.js"></script>
    <script src="/Scripts/jquery.serializeObject.js"></script>
    <script src="/Scripts/jquery.serializejson.js"></script>
    <script src="/Scripts/jquery.validate.js"></script>
    <script src="/Scripts/jquery.validate.unobtrusive.js"></script>
    <script src="/Scripts/pages/payment-method-list.js"></script>
    <script type="text/javascript">
        $(function () {
            masterData(
                { target: "#ddl-bank", entity: "bank", label: "BankName", value: "BankId" }
            );
        });

        function getPaymentMethod() {
            $('#modal-add-paymentmethod').modal();
        }

        function updatePaymentMethod() {

        };

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
