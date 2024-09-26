<%@ Page Title="" Language="C#"
    CodeBehind="StockExport.aspx.cs" Inherits="FingerprintPayment.StockExport" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <link rel="icon" href="/images/School Bright logo only.png" sizes="16x16 32x32" type="image/png">
    <title><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133272") %></title>

    <link href="/Content/Material/assets/css/material-dashboard.css?v=2.1.0" rel="stylesheet" />
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link href="/Content/Material/layout.css?v=<%=DateTime.Now.Ticks%>" rel="stylesheet" />
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <%--     <link rel="stylesheet" href="/styles/style-visithouse-print.css?v=<%=DateTime.Now.Ticks%>" />--%>
    <style>
        body {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            background-color: #FAFAFA;
            /*    font: 12pt "Tahoma";*/
        }

        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }

        .page {
            width: 210mm !important;
            min-height: 297mm !important;
            padding: 2mm;
            margin: 5mm auto;
            border: 1px #D3D3D3 solid;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            /*background: #FFEAEA;*/
        }

        .sub-page {
            /*padding: 1cm;*/
            border: 0px #ccc solid;
            height: 297mm !important;
            /*outline: 2cm #FFEAEA solid;*/
            background: white;
            position: relative;
        }

        @page {
            size: A4 landscape;
            margin: 0;
        }

        @media print {
            html, body {
                width: 210mm;
                height: 297mm;
            }

            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: always;
                color: #000;
                background-color: #fff;
            }
        }

        .print-btn {
            position: fixed;
            top: 50%;
            right: 10px;
            z-index: 4;
            border: 1px solid black;
            padding: 10px 15px;
            color: #fff !important;
            background-color: #2196F3 !important;
            text-align: center;
            cursor: pointer;
        }



        .input-with-dot {
            /* text-decoration:underline;
            text-decoration-style:dotted;*/
            border: 0px;
            border-bottom: 1px dotted;
            text-align: center;
        }

        .page {
        }

        @media print {
            .print-btn {
                display: none !important;
            }
        }

        .sub-page {
            /* width: 297mm !important;*/
        }

        .dataTable tfoot th {
            border-bottom: 1px solid #000;
        }

        .dataTable thead th {
            border-top: 1px solid #000;
        }
    </style>
    <script src="/Content/Material/assets/js/plugins/moment-with-locales.js"></script>
    <script src="/Scripts/FileSaver.min.js" type="text/javascript"></script>
    <script>
        function onExport(type) {
            if (type == 'pdf') {
                var json = JSON.stringify({
                    id: '<%= HttpContext.Current.Request.QueryString["id"]%>',
                });
                var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601087") %>_' + moment().format('DDMMYYYYhmmss') + '.pdf';

                xhr = new XMLHttpRequest();

                xhr.open("POST", "/Shop/StockExport.aspx/ExportPdf", true);
                xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                xhr.responseType = 'blob';
                xhr.onload = function () {
                    //aa = xhr.getResponseHeader("filename");
                    saveAs(xhr.response, file_name);
                    //$("body").mLoading('hide');
                };
                xhr.send(json);
            } else {

            }
        }
    </script>
</head>
<body>
    <div class="print-btn " onclick="onExport('pdf')">
        <span class="glyphicon glyphicon-print"></span>
        <br>
        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>      
    </div>

    <%-- <div class="row">
     <div class="col-md-12">
         <p class="text-muted" style="font-size: small;">
             <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601069") %>
         </p>
     </div>
 </div>--%>

    <div class="book">
        <div class="page">
            <div class="sub-page">
                <div class="row">
                    <div class="col-md-12 p-0">
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <h3><%= ModelData.School %></h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601087") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603003") %> <%= ModelData.Shop.shop_name %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> <%= ModelData.Stock.dStock.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("th-TH")) %></h4>
                            </div>
                        </div>
                        <div class="row mt-5">
                            <label class="col-sm-2 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133255") %> :</label>
                            <label class="col-sm-2 col-form-label text-left"><%= ModelData.Stock.DocRef %></label>
                            <label class="col-sm-2 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601074") %></label>
                            <label class="col-sm-2 col-form-label text-left"><%= ModelData.Stock.INVNo %></label>
                            <label class="col-sm-2 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :</label>
                            <label class="col-sm-2 col-form-label text-left"><%= ModelData.Stock.INVDate?.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-2 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601072") %></label>
                            <label class="col-sm-2 col-form-label text-left"><%= ModelData.Contact %></label>
                            <label class="col-sm-2 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601076") %></label>
                            <label class="col-sm-2 col-form-label text-left"><%= ModelData.Stock.PONo %></label>
                            <label class="col-sm-2 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :</label>
                            <label class="col-sm-2 col-form-label text-left"><%= ModelData.Stock.PODate?.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %></label>
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
                                            <td width="10%" class="text-center"><%=r.Cost?.ToString("#,0.##") %></td>
                                            <td width="13%" class="text-center"><%=r.Amount?.ToString("#,0.##") %></td>
                                            <td width="13%" class="text-center"><%=r.Total?.ToString("#,0.##") %></td>

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
                                                <span id="sum-price" class="" style="text-decoration: underline">
                                                    <%=ModelData.DetailList.Sum( o => o.Cost)?.ToString("#,0.##") %>
                                                </span>
                                            </th>
                                            <th class="text-center">
                                                <span id="sum-amount" style="text-decoration: underline">
                                                    <%=ModelData.DetailList.Sum( o => o.Amount)?.ToString("#,0.##") %>
                                                </span>
                                            </th>
                                            <th class="text-center">
                                                <span id="sum-total" style="text-decoration: underline">
                                                    <%=ModelData.DetailList.Sum( o => o.Total)?.ToString("#,0.##") %>
                                                </span>
                                            </th>

                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-sm-12 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601088") %> <%=DateTime.Now.ToString("dd/MM/yyyy HH:mm <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %>" , new System.Globalization.CultureInfo("th-TH")) %></label>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
