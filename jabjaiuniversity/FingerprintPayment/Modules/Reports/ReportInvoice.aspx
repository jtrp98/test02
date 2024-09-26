<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mp_notFrom.Master" CodeBehind="ReportInvoice.aspx.cs" Inherits="FingerprintPayment.Modules.Reports.ReportInvoice" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
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
    <link href="/Content/select2/select2.min.css" rel="stylesheet" />
    <script src="/Scripts/select2/select2.full.min.js"></script>
    <script src="/Scripts/moment.js"></script>
    <link href="/Content/custom/custom-datatable.css" rel="stylesheet" />
    <link href="/Assets/plugins/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="/Assets/plugins/bootstrap-daterangepicker/daterangepicker.js"></script>
    <link href="/bootstrap%20SB2/bower_components/datatables/media/css/jquery.dataTables.css" rel="stylesheet" />
    <link href="/Content/custom/custom-datatable.css" rel="stylesheet" />
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />

    <div class="col-md-12 content-container" style="background: #ffffff; border-radius: 5px 5px 0 0">

        <div class="tab-content">
            <div id="chart" style="padding: 10px;">
                <form id="form-search">
                    <div class="row">
                        <div class="col-md-6 col-xs-7">
                            <div class="col-md-3 text-right">
                                <label class="lb-control"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501021") %></label>
                            </div>
                            <div class="col-md-3">
                                <select class="form-control" id="ddl-search-by" name="ReportType">
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %></option>
                                </select>
                            </div>
                            <div class="col-md-3">
                                <input type="text" class="form-control" id="dateRange" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01582") %>" />
                                <select class="form-control hide ddl-year" id="ddl-year" name="ReportYear">
                                </select>

                            </div>
                            <div class="col-md-3">
                                <select class="form-control select2 hide" id="ddl-month" name="ReportMonth">
                                    <option value="01"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %></option>
                                    <option value="02"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %></option>
                                    <option value="03"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %></option>
                                    <option value="04"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %></option>
                                    <option value="05"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %></option>
                                    <option value="06"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %></option>
                                    <option value="07">ก<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>กฏาคม</option>
                                    <option value="08"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %></option>
                                    <option value="09"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %></option>
                                    <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %></option>
                                    <option value="11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %></option>
                                    <option value="12"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %></option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-6 col-xs-5">
                            <button type="button" class="btn btn-primary btn-sm" style="font-size: 20px" id="btnAdvSearch"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
                            <button type="button" class="btn btn-success btn-sm" style="font-size: 20px" id="btnExport">Export Excel</button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-xs-7">
                            <div class="col-md-3 text-right">
                                <label class="lb-control"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503005") %></label>
                            </div>
                            <div class="col-md-6">
                                <select class="form-control select2" id="ddl-paymentmethod" name="PaymentMethodId">
                                    <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01897") %></option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-6 col-xs-5">
                            &nbsp;
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-xs-7">
                            <div class="col-md-3 text-right">
                                <label class="lb-control"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503007") %></label>
                            </div>
                            <div class="col-md-6">
                                <select class="form-control select2" id="ddl-term" name="TermId">
                                    <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01897") %></option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-6 col-xs-5">
                            &nbsp;
                        </div>
                    </div>
                </form>

                <canvas id="chart-accounting-movement" class="hide" style="width: 100%"></canvas>
                <div id="pre-submit" class="col-md-12" style="min-height: 250px; margin-top: 20px; padding-left: 10%; padding-right: 10%; padding-bottom: 20px; background-color: #c0c0c0">
                    <h1 class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00030") %></h1>
                </div>
            </div>
            <div id="export" class="col-md-12">
                <form id="form-search-excel">
                    <%--<div class="col-md-6">
                        <div class="col-md-3">
                            <label class="lb-control"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105013") %></label>
                        </div>
                        <div class="col-md-3">
                            <select class="form-control" id="ddl-search-by-excel" name="ReportType">
                                <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %></option>
                            </select>
                        </div>
                        <div class="col-md-3">
                            <input type="text" class="form-control" id="date" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01582") %>" />
                            <select class="form-control hide ddl-year" id="ddl-year-excel" name="ReportYear">
                            </select>
                        </div>
                        <div class="col-md-3">
                            <select class="form-control select2 hide" id="ddl-month-excel" name="ReportMonth">
                                <option value="01"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %></option>
                                <option value="02"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %></option>
                                <option value="03"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %></option>
                                <option value="04"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %></option>
                                <option value="05"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %></option>
                                <option value="06"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %></option>
                                <option value="07">ก<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>กฏาคม</option>
                                <option value="08"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %></option>
                                <option value="09"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %></option>
                                <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %></option>
                                <option value="11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %></option>
                                <option value="12"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary btn-sm" style="font-size: 20px" id="btnSearch"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
                        <button type="button" class="btn btn-success btn-sm" style="font-size: 20px" id="btnExport">Export Excel</button>
                    </div>--%>
                    <br />

                    <div class="form-group">
                        <table class="table table-striped table-bordered table-hover table-checkable order-column " id="allSearch" style="width: 100%">
                            <thead>
                                <tr>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> </th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503014") %> </th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> </th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %> </th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> </th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503007") %> </th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503035") %></th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01442") %></th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503020") %> </th>
                                    <th class="fix-right"></th>
                                    <th class="fix-right"></th>
                                    <th class="fix-right"></th>
                                    <th class="fix-right"></th>
                                    <th class="fix-right"></th>
                                    <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503037") %></th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <script src="/bootstrap%20SB2/bower_components/datatables/media/js/jquery.dataTables.min.js"></script>
    <script src="/Scripts/jquery.blockUI.js"></script>
    <script src="/Scripts/jquery-confirm.js"></script>
    <script src="/Scripts/jquery.serializeObject.js"></script>
    <script src="/Scripts/jquery.serializejson.js"></script>
    <script src="/Scripts/pages/report-invoice.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="/Scripts/pages/report-invoice-excel.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="/Scripts/Chart.js"></script>

    <script>
        var canvas = document.getElementById('chart-accounting-movement');
        var ctx = canvas.getContext('2d');
        var getReportInvoiceUrl = "<%=ResolveUrl("~/Handles/Reports/ReportInvoiceHandler.ashx")%>";
        var exportExcelUrl = "<%=ResolveUrl("~/Handles/Reports/ReportInvoiceExcelHandler.ashx")%>";
        var masterDataUrl = "<%=ResolveUrl("~/Handles/Utilities/MasterdataHandle.ashx")%>";
        var exportExcelUrl = "<%=ResolveUrl("~/Handles/Reports/ReportInvoiceExcelHandler.ashx")%>";
        var detailsUrl = "<%=ResolveUrl("~/Handles/Reports/ReportInvoiceDetailsHandler.ashx")%>";
        var YearJson = <%=YearsJson%>;
    </script>
</asp:Content>
