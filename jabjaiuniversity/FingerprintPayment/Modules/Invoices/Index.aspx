<%@ Page Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FingerprintPayment.Modules.Invoices.Index" %>

<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>



<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link href="../../Content/select2/select2.min.css" rel="stylesheet" />
    <%--    <link href="../../Assets/plugins/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet" />--%>
    <link href="//cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../../bootstrap%20SB2/bower_components/datatables/media/css/jquery.dataTables.css" rel="stylesheet" />
    <link href="../../Content/custom/custom-datatable.css" rel="stylesheet" />
    <link href="../../Content/jquery-confirm.css" rel="stylesheet" />

    <style type="text/css">
        .modal-footer .btn.btn-default, .modal-footer .btn.btn-primary, .modal-footer .btn.btn-danger, .modal-footer .btn.btn-success {
            min-width: 95px !important;
            width: auto !important;
        }

        input {
            padding-left: 5px;
        }

        .content-required:after {
            color: red;
            content: " *";
        }

        /* .modal-font-size-default {
            font-size: 20px;
        }*/

        /*   .content-container {
            padding: 30px 0;
            font-size: 20px;
        }*/

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
        /*
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
        }*/

        .ball-green {
            color: #1dd1a1;
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .select2-container--default .select2-selection--single {
            border: 0px solid #aaa;
            border-radius: 0px;
            border-bottom: 1px solid #d2d2d2;
        }

        .fix-right .btn {
            padding: 12px 18px !important;
        }

        .dataTables_length {
            display: block !important;
        }

            .dataTables_length select {
                appearance: auto !important;
                text-align: center !important;
                width: 80px !important;
                border: 0px solid #aaa;
                border-radius: 0px;
                border-bottom: 1px solid #d2d2d2;
            }

        .monthselect {
            width: 50% !important;
        }

        .yearselect {
            width: 40% !important;
        }

        #modal-invoice-payment {
            z-index: 1040 !important;
        }

        #modal-add-paymentmethod {
            z-index: 1050 !important;
        }

        .lb-control {
            margin-top: 6px;
        }

        .card table td {
            font-size: .75rem;
        }

        #modal-invoice-payment *,
        #modal-add-paymentmethod * {
            font-size: .79rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <%-- <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/select2/select2.full.min.js"></script>
    <script src="../../Scripts/moment.js"></script>
    <%--    <script src="../../Assets/plugins/bootstrap-daterangepicker/daterangepicker.js"></script>--%>
    <script src="//cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.min.js"></script>
    <script src="/Scripts/jquery.blockUI.js"></script>
    <script type="text/javascript">
        var masterDataURL = "<%=ResolveUrl("~/Handles/Utilities/MasterDataHandle.ashx")%>";
        var getInvoiceUrl = "<%=ResolveUrl("~/Handles/Invoices/InvoiceDetailHandler.ashx")%>";
        var ajaxSearchHandlerUrl = "<%=ResolveUrl("~/Handles/Invoices/InvoiceListHandler.ashx")%>";
        var savePaymentUrl = "<%=ResolveUrl("~/Handles/Invoices/InvoicePaymentHandler.ashx")%>";
        var savePaymentMethodUrl = "<%=ResolveUrl("~/Handles/Invoices/InvoicePaymentMethodHandler.ashx")%>";
        var invoicePrintPreviewUrl = "<%=ResolveUrl("~/Handles/Invoices/InvoicePrintHandler.aspx")%>";
        var invoiceDraftStatus =  "<%=DraftStatus%>";
        var invoiceApproveStatus =  "<%=ApproveStatus%>";
        var invoicePaidStatus =  "<%=PaidStatus%>";
        var invoiceVoidStatus =  "<%=VoidStatus%>";
        var overDueDate = "<%=OverDueDate%>";
    </script>
    <script src="/bootstrap%20SB2/bower_components/datatables/media/js/jquery.dataTables.min.js"></script>
    <script src="/Scripts/jquery-confirm.js"></script>
    <script src="/Scripts/jquery.serializeObject.js"></script>
    <script src="/Scripts/jquery.serializejson.js"></script>
    <script src="/Scripts/jquery.validate.js"></script>
    <script src="/Scripts/jquery.validate.unobtrusive.js"></script>
    <script src="/Scripts/pages/invoice-list.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmssss") %>"></script>

    <link href="/Content/bootstrap-toggle.css" rel="stylesheet" />
    <script src="/Scripts/bootstrap-toggle.js" type="text/javascript"></script>

    <script type="text/javascript">
        var ajaxResult;
        $(function () {
            $("#btnappActive").click(function () {
                $.confirm({
                    title: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h3>',
                    content: `<h4>
<div class="row">
    <div class="col-md-4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02205") %> : </div>
    <div class="col-md-8"> <input id="appActive" type="checkbox" /></div>
</div>
<br/><br/>
<div class="row">
    <div class="col-md-4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> : </div>
    <div class="col-md-8"><select class="form-control select2" id="ddl-term-confirm"></div>
</div>
</h4>`,
                    theme: 'bootstrap',
                    onContentReady: function () {
                        $('#appActive').bootstrapToggle();
                        $.each(ajaxResult.Result, function (index, item) {
                            var newOption = new Option(item.Year.Number + "/" + item.sTerm, item.TermId, false, false);
                            $("#ddl-term-confirm").append(newOption).trigger('change');
                        });
                    }, buttons: {
                        "<span style=\"\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %></span>": function () {
                            $.ajax({
                                url: "/TuitionFee/Tuitionfeesetting.aspx/application_approved",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: '{TermId: "' + $("#ddl-term-confirm").val() + '","appActive" :' + $("#appActive").prop("checked") + ' }',
                                success: function (result) {

                                }
                            });

                        }, "<span style=\"\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": function () {
                        }
                    }
                });
            });
        })
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803031") %> > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01541") %> > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501019") %>
            </p>
        </div>
    </div>


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
                    <div class="content-container" style="background: #ffffff; border-radius: 5px 5px 0 0">

                        <br />
                        <form id="form-invocie-search">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label class="col-md-4 col-form-label text-left">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501020") %>
                                        </label>
                                        <div class="col-md-8">
                                            <input type="text" class="form-control" id="invoiceid" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01582") %>" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label class="col-md-4 col-form-label text-left">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101340") %>
                                        </label>
                                        <div class="col-md-8">
                                            <uc1:StudentAutocomplete runat="server" ID="StudentAutocomplete" />
                                            <%-- <input type="text" class="form-control" id="studentcode" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01582") %>" />--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="row">
                                        <label class="col-md-3  col-form-label text-left">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501021") %>
                                        </label>
                                        <div class="col-md-4">
                                            <select id="ddl-search-by" class="form-control " style="position: relative; top: -12px; appearance: auto;">
                                                <option value="term"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></option>
                                                <option value="date"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501032") %></option>
                                                <option value="duedate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502010") %></option>
                                            </select>
                                        </div>
                                        <div class="col-md-5">
                                            <input type="text" class="form-control d-none" id="dateRange" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01582") %>" style="position: relative; top: -6px;" />
                                            <select id="ddl-term" class="form-control " style="position: relative; top: -12px; appearance: auto;">
                                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01897") %></option>
                                            </select>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </form>
                        <div class="row" style="padding-top: 5px;">
                            <div class="col-md-4">
                                <div class="row">
                                    <label class="col-md-4 col-form-label text-left">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>
                                    </label>
                                    <div class="col-md-8">
                                        <select id="ddl-level" class="selectpickerx form-control select2 ddl-search-select2" data-width="100%" data-size="7" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01917") %>">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="row">
                                    <label class="col-md-4 col-form-label text-left">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>
                                    </label>
                                    <div class="col-md-8">
                                        <select id="ddl-sublevel" class="selectpickerx form-control select2 ddl-search-select2" data-width="100%" data-size="7" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01917") %>">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <button type="button" class="btn btn-primary" style="" id="btnAdvSearch"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
                                <div class="btn-group">
                                    <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103029") %><span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu pull-right" style="">
                                        <li><a href="/Modules/Invoices/Create.aspx"><i class="fa fa-folder"></i>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501014") %></a></li>
                                        <%--<li><a href="#" data-toggle="modal" data-target="#modalImportStudentData"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101027") %></a></li>--%>
                                        <li><a href="#" id="btnPrint"><i class="fa fa-print"></i>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01206") %></a></li>
                                        <li><a href="#" id="btnVoid"><i class="fa fa-remove"></i>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501025") %></a></li>
                                        <li><a href="#" id="btnappActive"><i class="fa fa-mobile-phone"></i>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501026") %></a></li>
                                    </ul>
                                </div>
                                <%--   <button type="button" class="btn btn-success btn-sm" style="font-size: 20px" onclick="window.location='/Modules/Invoices/Create.aspx'"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501014") %></button>
                    <button type="button" class="btn btn-warning btn-sm" style="font-size: 20px" id="btnPrint">
                        <i class="fa fa-print"></i>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01206") %></button>
                    <button type="button" class="btn btn-danger btn-sm" style="font-size: 20px" id="btnVoid">
                        <i class="fa fa-remove"></i>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501025") %></button>--%>
                            </div>
                        </div>

                    </div>

                    <div class=" content-container" style="background: #ffffff; border-radius: 0 0 5px 5px; padding: 0 5px;">

                        <ul class="nav nav-pills nav-pills-primary" role="tablist" id="ul-table">
                            <%-- <li class="nav-item">
                    <a class="nav-link active" data-toggle="tab" href="#link1" role="tablist" aria-expanded="true">Profile
                    </a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" data-toggle="tab" href="#link2" role="tablist" aria-expanded="false">Settings
                    </a>
                </li>--%>
                            <li class="nav-item ">
                                <a class="nav-link active" data-table="allSearch" data-timestamp="" data-toggle="tab" href="#all-list" aria-expanded="true"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-table="waitingSearch" data-timestamp="" data-toggle="tab" href="#waiting-list"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501028") %></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-table="overSearch" data-timestamp="" data-toggle="tab" href="#over-list"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501029") %></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-table="paidSearch" data-timestamp="" data-toggle="tab" href="#paid-list"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501030") %></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-table="cancelSearch" data-timestamp="" data-toggle="tab" href="#cancel-list"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></a>
                            </li>
                        </ul>
                        <%--  <ul class="nav nav-tabs" id="ul-table">
                
            </ul>--%>
                        <form id="invoice-form">
                            <div class="tab-content tab-space">
                                <div id="all-list" class="tab-pane  active" aria-expanded="true">
                                    <%--<span class="fa fa-circle ball-green "></span>--%>
                                    <table class="table table-striped table-bordered table-hover table-checkable order-column" id="allSearch" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <input type="checkbox" name="InvoiceId-all" /></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501020") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111098") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> -  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501032") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501033") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603022") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501034") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132493") %> </th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th class="fix-right" style="width: 150px;"></th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                                <div id="waiting-list" class="tab-pane ">
                                    <table class="table table-striped table-bordered table-hover table-checkable order-column  " id="waitingSearch" data-timestamp="" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <input type="checkbox" name="InvoiceId-all" /></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501020") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111098") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> -  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501032") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501033") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603022") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501034") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132493") %> </th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th class="fix-right" style="width: 150px;"></th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                                <div id="over-list" class="tab-pane ">
                                    <table class="table table-striped table-bordered table-hover table-checkable order-column  " id="overSearch" data-timestamp="" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th class="text-center">
                                                    <input type="checkbox" name="InvoiceId-all" /></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501020") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111098") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> -  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501032") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501033") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603022") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501034") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132493") %> </th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th class="fix-right" style="width: 150px;"></th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                                <div id="paid-list" class="tab-pane ">
                                    <table class="table table-striped table-bordered table-hover table-checkable order-column  " id="paidSearch" data-timestamp="" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <input type="checkbox" name="InvoiceId-all" /></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501020") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111098") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> -  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501032") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501033") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603022") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501034") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132493") %> </th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th class="fix-right" style="width: 150px;"></th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                                <div id="cancel-list" class="tab-pane ">
                                    <table class="table table-striped table-bordered table-hover table-checkable order-column  " id="cancelSearch" data-timestamp="" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <input type="checkbox" name="InvoiceId-all" /></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501020") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111098") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> -  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501032") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501033") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603022") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501034") %></th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> </th>
                                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132493") %> </th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th class="fix-right" style="width: 150px;"></th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                            </div>
                        </form>

                    </div>
                </div>

            </div>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade modal-font-size-default" role="dialog" id="modal-invoice-payment" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <form id="invoice-payment-form">
                <div class="modal-content" style="overflow-y: auto; height: 75vh;">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title text-left">
                            <b style="font-size: 16px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01597") %>
                       
                                <label id="modal-invoice-number">xxx</label>
                            </b>
                        </h4>
                    </div>
                    <div class="modal-body" style="padding-left: 10px; padding-right: 10px;">

                        <div class="row">
                            <div class="payment-gateway-summary col-md-9">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="">
                                            <label class="lb-control content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> / <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                                            <input type="text" id="termName" name="termName" value="" class="form-control" readonly="readonly" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="">
                                            <label class="lb-control"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111098") %></label>
                                            <input type="text" id="classtName" name="classtName" value="" class="form-control" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="">
                                            <label class="lb-control content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                                            <input type="text" id="studentName" name="studentName" value="" class="form-control" readonly="readonly" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="">
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="">
                                            <label class="lb-control content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01977") %></label>
                                            <input type="hidden" id="invoice-id" name="InvoiceId" value="" />
                                            <input type="hidden" id="invoice-code" name="InvoiceCode" value="" />
                                            <input type="text" id="datePayment" name="PaymentDate" value="" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="">
                                            <label class="lb-control"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503020") %></label>
                                            <input type="text" value="<%=UserName%>" class="form-control" disabled="disabled" style="background-color: #e9ecef;" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="">
                                            <div class="PaidAmount">
                                                <label class="lb-control content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501008") %></label>
                                                <input type="text" class="form-control" id="PaidAmount" name="PaidAmount" placeholder="0.00" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-4x">
                                        </div>
                                        <div class="">

                                            <div class="form-group">
                                                <label class="lb-control content-required"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503021") %></label>

                                                <div class="input-group ">
                                                    <select id="ddl-paymentmethod" name="PaymentMethodId" style="width: 85%" required class="form-control select2">
                                                    </select>
                                                    <div class="input-group-append">
                                                        <span class="input-group-text" id="btn-add-paymentmethod" style="cursor: pointer; width: 10px;">
                                                            <i class="material-icons" style="font-size: 20px;">add</i>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>



                                            <%-- <label class="lb-control">&nbsp;</label>
                                            <span class="input-group-btn">
                                                <button class="btn btn-success btn-sm"  type="button" tabindex="-1" style="min-height: 28px;">
                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                </button>
                                            </span>--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="row hidden d-none">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <label class="lb-control"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></label>
                                            <textarea class="form-control" name="Note" cols="10" rows="3"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="payment-gateway-right  col-md-3">
                                <ul class="invoice-result-box">
                                    <li class="total-deb" id="lbRemainWHTAmountBox" style="display: none"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132494") %> <strong id="lbRemainWHTAmount">0.00</strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>
                                    </li>
                                    <li class="total-deb"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503035") %> <strong id="lbremainpaymentamount_0">0.00</strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></li>
                                    <li class="total-deb"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02137") %> <strong id="lbremainpaymentamount_1">0.00</strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></li>
                                    <li class="total-deb"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501030") %> <strong id="lbremainpaymentamount_2">0.00</strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></li>
                                    <li class="total-deb"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00671") %> <strong id="lbremainpaymentamount">0.00</strong> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></li>
                                    <li class="total-lst"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01596") %>
                                    <div class="line-payment-summary"></div>
                                    </li>
                                    <li>
                                        <strong id="lbpaymentnetamount">0.00</strong> <span><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></strong></span>
                                        <div class="line-payment-summary"></div>
                                        <div class="clear "></div>
                                        <div class="line-payment-summary"></div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <br />
                        <label><b style="color: red;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></b> : <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132488") %></label>
                        <table class="table" id="tb-invoice-item">
                            <thead>
                                <tr>
                                    <th>
                                        <input id="chk-all" type="checkbox" /></th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02167") %></th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00871") %></th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106164") %></th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501034") %></th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></button>
                        &nbsp;
                        <button type="submit" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01595") %></button>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <!-- Modal -->
    <div id="modal-add-paymentmethod" class="modal fade modal-font-size-default" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title"><b style="font-size: 16px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01253") %></b></h3>
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
                                <form id="payment-method-1" name="payment-method-1">
                                    <input type="hidden" name="PaymentMethodTypeId" value="1" />
                                    <div class="row">
                                        <label class="col-md-3 content-required col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                        <div class="form-group col-md-9">
                                            <input type="text" name="PaymentMethodName" class="form-control" required="required" />
                                        </div>

                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></label>
                                        <div class="col-md-9 form-group">
                                            <textarea class="form-control" name="Note" cols="5" rows="3"></textarea>
                                        </div>
                                    </div>
                                </form>
                            </li>
                            <li style="padding-top: 5px" class="li-paymentmethod d-none" data-paymentmethod="2">
                                <form id="payment-method-2" name="payment-method-2">
                                    <div class="form-group">
                                        <input type="hidden" name="PaymentMethodTypeId" value="2" />
                                        <div class="row">
                                            <label class="col-md-3 content-required col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504015") %></label>
                                            <div class="col-md-9">
                                                <select id="ddl-bank" name="BankId" stylex="width: 60%" class="form-control " style="position: relative; top: -12px; appearance: auto;">
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
                                            <label class="col-md-3 content-required col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02793") %></label>
                                            <div class="col-md-9">
                                                <input type="text" name="PaymentMethodName" class="form-control" required="required" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-md-3 content-required col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02779") %></label>
                                            <div class="col-md-9">
                                                <input type="text" name="BankNumber" class="form-control" required="required" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></label>
                                        <div class="col-md-9">
                                            <textarea class="form-control" name="Note" cols="5" rows="3"></textarea>
                                        </div>
                                    </div>
                                </form>
                            </li>
                            <%--             <li style="padding-top: 5px" class="li-paymentmethod d-none" data-paymentmethod="3">
                                <form id="payment-method-3" name="payment-method-3">
                                    <div class="form-group">
                                        <input type="hidden" name="PaymentMethodTypeId" value="3" />
                                        <div class="row">
                                            <label class="col-md-3 content-required col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %></label>
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
                            </li>--%>
                        </ul>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01417") %></button>
                    &nbsp;
                    <button type="button" class="btn btn-primary" id="btnSavePaymentMethod"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02111") %></button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
