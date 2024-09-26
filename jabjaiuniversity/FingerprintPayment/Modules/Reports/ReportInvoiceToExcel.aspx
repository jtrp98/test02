<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mp_notFrom.Master" CodeBehind="ReportInvoiceToExcel.aspx.cs" Inherits="FingerprintPayment.Modules.Reports.ReportInvoiceToExcel" %>


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
    <link href="../../Assets/plugins/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../../Assets/plugins/bootstrap-daterangepicker/daterangepicker.js"></script>
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />

    <div class="col-md-12 content-container" style="background: #ffffff; border-radius: 5px 5px 0 0">
    </div>

    <script src="/Scripts/jquery.blockUI.js"></script>
    <script src="/Scripts/jquery-confirm.js"></script>
    <script src="/Scripts/jquery.serializeObject.js"></script>
    <script src="/Scripts/jquery.serializejson.js"></script>
    <script src="/Scripts/pages/report-invoice-excel.js"></script>
    <script src="/Scripts/Chart.js"></script>

    <script>
        var YearJson = <%=YearsJson%>;

    </script>
</asp:Content>
