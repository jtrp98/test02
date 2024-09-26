<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="billing.aspx.cs" Inherits="FingerprintPayment.PaymentGateway.BBL.TuitionFees.billing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        page {
            background: white;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
            box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
            margin-top: 10%;
        }

            page[size="A4"] {
                width: 21cm;
                height: 29.7cm;
                margin-top: 50px;
            }

                page[size="A4"][layout="landscape"] {
                    width: 29.7cm;
                    height: 21cm;
                }

            page[size="A3"] {
                width: 29.7cm;
                height: 42cm;
            }

                page[size="A3"][layout="landscape"] {
                    width: 42cm;
                    height: 29.7cm;
                }

            page[size="A5"] {
                width: 14.8cm;
                height: 21cm;
            }

                page[size="A5"][layout="landscape"] {
                    width: 21cm;
                    height: 14.8cm;
                }

        .blue-font {
            color: #3498db
        }

        table tbody tr {
            line-height: 12px;
        }

        @media print {
            body, page {
                margin: 0;
                box-shadow: 0;
            }
        }

        .jabjai-sticky-toolbar {
            width: 120px !important;
        }
    </style>

    <script src="../../../Scripts/jquery.js"></script>
    <link href="/Content/select2/select2.min.css" rel="stylesheet" />
    <link href="/Content/custom/custom-sticky.css" rel="stylesheet" />
    <script src="/Scripts/select2/select2.full.min.js"></script>
    <script src="/Scripts/moment.js"></script>
    <link href="/Content/custom/custom-datatable.css" rel="stylesheet" />
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />
    <script src="/Scripts/thaibath.js"></script>
    <link href="/bootstrap SB2/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <script src="../../../Scripts/JsBarcode.all.min.js" type="text/javascript"></script>
    <script src="/Scripts/FileSaver.js" type="text/javascript"></script>

    <script>
        $(function () {
            JsBarcode(".barcode").init();
        })

        function export_PDF() {
            var d = new Date();

            let datestring = ("0" + d.getDate()).slice(-2) + ("0" + (d.getMonth() + 1)).slice(-2) +
                d.getFullYear() + ("0" + d.getHours()).slice(-2) + ("0" + d.getMinutes()).slice(-2) + ("0" + d.getMilliseconds()).slice(-2);

            if ($("#txtstart").val() === "") Search.dStart = (new Date().getMonth() + 1) + "/" + new Date().getDate() + "/" + new Date().getUTCFullYear()
            var file_name = 'invoices_' + datestring + '.pdf';

            //$("body").mLoading('show');
            var json = InvoiceId = '<%= Model.InvoiceId %>';
            var req = new XMLHttpRequest();
            xhr = new XMLHttpRequest();
            xhr.open("POST", "/PaymentGateway/BBL/TuitionFees/billing.aspx/ExportPDF?InvoiceId=<%= Model.InvoiceId %>", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = "blob";
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                //$("body").mLoading('hide');
            };
            xhr.send();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">

        <page size="A4" id="page1">

            <div class="form-group" style="padding-top: 10px; margin: 0">
                <div class="col-md-12 text-center">
                    <img src="<%=  schoolData.sImage %>" style="width: 150px; height: 150px" />
                </div>
            </div>
            <div class="form-group" style="padding-top: 10px; margin: 0">
                <div class="col-md-12 text-center">
                    <b style="font-size: 36px;">
                        <%=  schoolData.sCompany %><br />
                        <%=  schoolData.sNameEN %><br />
                    </b>
                    <p style="font-size: 32px;">
                        &nbsp;
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> <%=string.IsNullOrEmpty(schoolData.sHomeNumber) ? "-" : schoolData.sHomeNumber %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <%=string.IsNullOrEmpty(schoolData.sMuu) ? "-" : schoolData.sMuu %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <%=string.IsNullOrEmpty(schoolData.sRoad) ? "-" : schoolData.sRoad %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <%= string.IsNullOrEmpty(schoolData.sSoy) ? "-" :  schoolData.sSoy %>
                        <br />
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <%=  schoolData.sTumbon %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <%=  schoolData.sAumpher %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <%=  schoolData.sProvince %>
                        <br />
                        &nbsp;  <%= schoolData.sPost %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132537") %> <%= schoolData.sPhoneOne %>
                        <br />
                        &nbsp; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132514") %> :  <%=  schoolData.TaxId %>
                    </p>
                </div>
            </div>

            <hr style="width: 90%; border-top: 2px solid black;" />

            <div class="form-group" style="padding: 0px 0 0 15px; font-size: 32px;">
                <div class="col-md-12">
                    <% string day = Model.DueDate.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-th")); %>
                    <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> <%= day %></b>
                </div>
            </div>

            <div class="form-group" style="padding: 0px 0 0 15px; font-size: 32px;">
                <div class="col-sm-5 col-md-4">
                    <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>&nbsp;&nbsp;</b>
                </div>
                <div class="col-sm-7 col-md-8">
                    : <span><%=Model.StudentName %></span>
                </div>
            </div>

            <div class="form-group" style="padding: 0px 0 0 15px; font-size: 32px;">
                <div class="col-sm-5 col-md-4">
                    <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %>&nbsp;&nbsp;</b>
                </div>
                <div class="col-sm-7 col-md-8">
                    : <span><%=Model.StudentCode %></span>
                </div>
            </div>

            <div class="form-group" style="padding: 0px 0 0 15px; font-size: 32px;">
                <div class="col-sm-5 col-md-4">
                    <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>/Class&nbsp;&nbsp;</b>
                </div>
                <div class="col-sm-7 col-md-8">
                    : <span><%=Model.SubLevel2 %></span>
                </div>
            </div>

            <div class="form-group" style="padding: 0px 0 0 15px; font-size: 32px;">
                <div class="col-md-12 col-sm-12">
                    <%
                        int Term = 0, StudentCode = 0;
                        int.TryParse(Model.Term, out Term);
                        int.TryParse(Model.StudentCode, out StudentCode);

                        string _REF1 = string.Format("{0}{1:00}{2:00000000}", Model.TermYear, Term, StudentCode);
                    %>
                    REF.1 <%= _REF1 %>
                </div>
            </div>

            <div class="form-group" style="padding: 0px 0 0 15px; font-size: 32px;">
                <div class="col-md-12 col-sm-12">
                    REF.2 <%= string.Format("{0:00000}",Model.SchoolId) + Model.Code.Replace("IV-","") %>
                </div>
            </div>

            <div class="form-group" style="padding: 0px 0 0 15px; font-size: 32px;">
                <div class="col-sm-4 col-md-4">
                    <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106164") %></b>
                </div>
                <div class="col-sm-8 col-md-8 text-right" style="color: green;">
                    <%=( Model.OutstandingAmount??0).ToString("#,#0.00") %>
                </div>
            </div>

            <div class="form-group" style="padding: 0px 0 0 15px; font-size: 32px; color: green;">
                <div class="col-sm-4 col-md-4">
                </div>
                <div class="col-sm-8 col-md-8 text-right">
                    ( <%=<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %>BahtOutstandingAmount %> )
                </div>
            </div>

            <hr style="width: 90%; border-top: 2px solid black; padding: 20px 0px;" />

            <div class="form-group" style="padding: 0px 0 0 15px; font-size: 32px; color: green;">
                <div class="col-sm-2 col-md-2">
                    <img src="<%= imageQRCODEUri %>" alt="Barcode" style="width: 100%; height: 100%;" />
                </div>
                <div class="col-sm-10 col-md-10 text-right">
                    <img src="<%= imageDataUri %>" alt="Barcode" style="width: 100%; height: 100%;" />
                </div>
            </div>

        </page>

        <div class="row">
            <div class="col-lg-2 col-sm-2 col-md-2"></div>
            <div class="btn btn-success col-lg-8 col-sm-8 col-md-8 text-center" id="downloads-pdf" onclick="export_PDF()">Download</div>
            <div class="col-lg-2 col-sm-2 col-md-2"></div>
        </div>

    </form>
</body>
</html>
