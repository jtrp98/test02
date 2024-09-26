<%@ Page Language="C#" MasterPageFile="~/mp_notFrom.Master" AutoEventWireup="true"
    CodeBehind="perRegisterCreate.aspx.cs" Inherits="FingerprintPayment.Modules.Invoices.perRegisterCreate" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
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
            text-align: left;
            height: 35vh;
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

        .drag-icon {
            background: #f0f0f0 !important;
            cursor: move;
        }

            .drag-icon img {
                max-height: 16px;
                vertical-align: middle;
                padding: 0;
            }


        .table-custom-sortable {
            width: 100%;
            /* color: #646464; */
            /* margin-top: 25px; */
            /* line-height: 40px; */
            /* float: left; */
            /* text-align: center; */
            border: 1px solid #DDDDDD;
            /* border-bottom: none; */
            border-radius: 5px 5px 0 0;
            border-collapse: separate;
            box-sizing: border-box;
        }

        .price-box-input {
            text-align: right;
            padding-right: 5px;
        }

        .table-custom-sortable thead tr th {
            text-align: center;
            color: black;
            background-color: #FAB1A0;
        }

            .table-custom-sortable thead tr th input, .table-custom-sortable tbody tr td input, .table-custom-sortable tbody tr td select, .table-custom-sortable tbody tr td textarea, .table-custom-sortable tbody tr td span.select2-selection {
                border-radius: 0 !important;
                height: 40px;
                color: black;
            }

        .select2-selection {
            text-align: left;
        }

        .table-custom-sortable tbody tr td span.select2-selection
        span.select2-selection__rendered {
            padding: 5px 5px;
            color: black;
        }

        input.select2-search__field {
            font-size: 18px;
        }

        .select2-selection__clear {
            padding-right: 15px;
        }

        .has-error .select2-selection {
            border-color: rgb(185, 74, 72) !important;
        }

        .white-card {
            background: #ffffff;
            border-radius: 5px 5px 0 0
        }

        .circle-payment-orange {
            background: #FF5733;
        }

        .circle-payment {
            width: 24px;
            height: 24px;
            border-radius: 50%;
            font-size: 20px;
            color: #fff;
            line-height: 25px;
            text-align: center;
        }

        .line-payment-orange {
            background: #FF5733;
        }

        .line-circle {
            width: 50px;
            height: 3px;
            float: left;
            position: relative;
            top: -13px;
            left: 20px;
        }
    </style>

    <form id="invoice-form" class="row">
        <input type="hidden" id="InvoiceStatus" name="InvoiceStatus" value="<%=Model.InvoiceStatus %>" />
        <input type="hidden" id="InvoiceId" name="InvoiceId" value="<%=Model.InvoiceId %>" />
        <input type="hidden" id="InvoiceCode" name="InvoiceCode" value="<%=Model.InvoiceCode %>" />
        <input type="hidden" name="IsActive" value="true" />
        <input type="hidden" name="IsDelete" value="false" />

        <div class="col-md-12 content-container white-card">
            <h1 class="text-center"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803041") %></b></h1>
            <hr />
            <div class="row" style="padding: 0 10px;">
                <div class="form-group col-md-3">
                    <label class="lb-control content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02755") %></label>
                    <select class="form-control select2" id="ddl-user" name="StudentId" required="required" data-show-error="true">
                    </select>
                </div>
                <div class="col-md-2">
                    <label class="lb-control content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501032") %></label>
                    <input type="text" class="form-control date-single" name="Date" disabled="disabled" value="<%=Model.Date.ToString("dd/M/yyyy") %>" />
                </div>
                <div class="col-md-2">
                    <label class="lb-control content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502010") %></label>
                    <input type="text" class="form-control date-single" name="DueDate" value="<%=Model.DueDate.ToString("dd/M/yyyy") %>" />
                </div>
                <div class="col-md-2">
                    <label class="lb-control content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601070") %></label>
                    <input type="text" class="form-control" name="Number" value="<%=Model.Code %>" disabled="disabled" />
                </div>
                <div class="col-md-2">
                    <label class="lb-control content-required">ปีการศึกษา/เทอม</label>
                    <select class="form-control select2" id="ddl-term" name="TermId" required="required" data-show-error="true">
                    </select>
                </div>
                <div class="col-md-1">
                </div>
            </div>
            <hr />
        </div>

        <div class="col-md-12 content-container" style="background: #ffffff; border-radius: 0 0 5px 5px; padding: 0 10px 100px 10px;">
            <table class="table-custom-sortable" id="tb-products">
                <thead>
                    <tr>
                        <th style="width: 2%"></th>
                        <th class="col-md-2 content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02167") %></th>
                        <th class="col-md-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00871") %></th>
                        <th class="col-md-1 content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></th>
                        <th class="col-md-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404034") %></th>
                        <th class="col-md-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503017") %></th>
                        <th style="width: 2%"></th>
                    </tr>
                </thead>
                <tbody class="ui-sortable">
                </tbody>
            </table>

            <button class="btn btn-success btn-sm" type="button" style="font-size: 20px; border-radius: 0 0 5px 5px;" id="btn-add-item">
                <span class="glyphicon glyphicon-plus"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106153") %>
            </button>
            <hr />

            <div class="row">
                <div class="col-md-8">
                    <label class="lb-control"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></label>
                    <textarea class="form-control" id="Description" name="Description" cols="5" rows="4" style="width: 50%;"> </textarea>
                </div>
                <div class="col-md-4">
                    <div class="payment-gateway-right  col-md-12">
                        <ul class="invoice-result-box">
                            <li class="total-deb">
                                <div>
                                    <div class="col-md-5">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503017") %>
                                    </div>
                                    <div class="col-md-5">
                                        <input type="text" placeholder="0.00" class="form-control price-box-input" name="ManualDiscount" id="ManualDiscount" />
                                    </div>
                                    <div class="col-md-1">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>
                                    </div>
                                </div>
                            </li>
                            <li class="total-deb">
                                <div>
                                    <div class="col-md-5">
                                        <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01487") %></b>
                                    </div>
                                    <div class="col-md-5">
                                        <span id="AllAmount">0.00</span>
                                    </div>
                                    <div class="col-md-1">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div>
                                    <div class="col-md-5">
                                        <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02138") %></b>
                                    </div>
                                    <div class="col-md-5">
                                        <span id="TotalDiscount">0.00</span>
                                    </div>
                                    <div class="col-md-1">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>
                                    </div>
                                </div>
                                <div class="line-payment-summary"></div>
                                <div class="clear "></div>
                                <div class="line-payment-summary"></div>
                            </li>
                            <li>
                                <div>
                                    <div class="col-md-5">
                                        <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603022") %></b>
                                    </div>
                                    <div class="col-md-5">
                                        <span id="TotalAmount">0.00</span>
                                    </div>
                                    <div class="col-md-1">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>
                                    </div>
                                </div>
                                <div class="line-payment-summary"></div>
                                <div class="clear "></div>
                                <div class="line-payment-summary"></div>
                            </li>
                        </ul>

                    </div>
                </div>
            </div>

            <div class="row text-right" style="padding-right: 30px;">
                <div id="btnBack" runat="server" class="col-md-12 text-right">
                    <button type="button" id="btn-back" class="btn btn-default"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00070") %></button>
                </div>
                <div id="btnSpan" runat="server" class="col-md-12 text-right">
                    <button id="btnDraft" runat="server" type="button" class="btn btn-success" data-status="<%=DraftStatus%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00906") %></button>
                    <button type="button" id="btn-save-approve" class="btn btn-primary" data-status="<%=ApproveStatus%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00913") %></button>
                    <span id="spanBtnVoid" runat="server">
                        <button type="button" id="btn-cancel-invoice" class="btn btn-warning" data-status="<%=VoidStatus%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132473") %></button></span>
                    <span id="spanBtnDelete" runat="server">
                        <button type="button" id="btn-delete-invoice" class="btn btn-danger" data-status="Delete"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01825") %></button></span>
                    <button id="spanBtnCancel" runat="server" type="button" class="btn btn-default"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01417") %></button>
                </div>
            </div>
            <hr />
            <div id="paid-payment-history" style="margin-top: 15px;">
                <h3><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01035") %></b></h3>
            </div>
        </div>
    </form>
    <script src="/Scripts/jquery.blockUI.js"></script>
    <script type="text/javascript">
        var termDataUrl =  "<%=ResolveUrl("~/Handles/Utilities/MasterDataHandle.ashx")%>";
        var searchUserUrl =  "<%=ResolveUrl("~/Handles/Users/UserSearchHandle.ashx")%>";
        var searchProductUrl =  "<%=ResolveUrl("~/Handles/Products/ProductSearchHandle.ashx")%>";
        var saveUrl = "<%=ResolveUrl("~/Handles/Invoices/InvoiceCreationHandler.ashx")%>";
        var paidHistory = "<%=ResolveUrl("~/Handles/Invoices/InvoicePaidHistoryHandler.ashx")%>";
        var paidUpdateToVoidUrl = "<%=ResolveUrl("~/Handles/Invoices/InvoicePaymentVoidHandler.ashx")%>";
        var invoiceStatusDraft =  "<%=DraftStatus%>";
        var invoiceStatusApprove = "<%=ApproveStatus%>";
        var _invoice = <%=ModelJson%>;
        var _isLocked = <%=IsLocked.ToString().ToLower()%>;


    </script>
    <link href="/Content/select2/select2.min.css" rel="stylesheet" />
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />
    <script src="/Scripts/jquery-confirm.js"></script>
    <script src="/Scripts/jquery.serializeObject.js"></script>
    <script src="/Scripts/jquery.serializejson.js"></script>
    <script src="/Scripts/jquery.validate.js"></script>
    <script src="/Scripts/jquery.validate.unobtrusive.js"></script>
    <script src="/Scripts/pages/preRegister-invoice-detail.js?d=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script src="/Scripts/select2/select2.full.min.js"></script>
    <script src="/Scripts/moment.js"></script>
    <link href="/Assets/plugins/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="/Assets/plugins/bootstrap-daterangepicker/daterangepicker.js"></script>
</asp:Content>
