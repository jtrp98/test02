<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="PaymentTransaction.aspx.cs" Inherits="FingerprintPayment.PaymentGateway.PaymentTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
        label.col-form-label {
            font-size: 14px;
            color: black;
        }

        .badge {
            padding-top: 7px;
            font-weight: bold;
        }

        .table > thead > tr > th {
            font-weight: bold;
        }

        .card .tab-content.tab-space {
            padding: 12px 0 9px 0;
        }

        .nav-pills {
            padding: 0 5px;
        }

        .bg-danger {
            background-color: #dc3545 !important;
        }

        .bg-warning {
            background-color: #ffc107 !important;
        }

        .bg-primary {
            background-color: #007bff !important;
        }

        .bg-success {
            background-color: #28a745 !important;
        }

        .bg-cyan {
            background-color: #17a2b8 !important;
        }

        .bg-gray {
            background-color: #6c757d !important;
        }

        .bg-orange {
            background-color: #fd7e14 !important;
        }

        .bg-citrine {
            background-color: #e3c910 !important;
        }

        .bg-royal-red {
            background-color: #c90076 !important;
        }

        .bg-pastel-blue {
            background-color: #a2c4c9 !important;
        }

        .bg-cyan-blue-azure {
            background-color: #3d85c6 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                Payment Transaction
            </p>
        </div>
    </div>

    <div class="studentList row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">search</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106101") %></label>
                        <div class="col-sm-3">
                            <div class="form-group div-datepicker">
                                <input id="iptStartDate" name="iptStartDate" type="text" class="form-control datepicker" value="<%=DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"))%>" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106102") %></label>
                        <div class="col-sm-3">
                            <div class="form-group div-datepicker">
                                <input id="iptEndDate" name="iptEndDate" type="text" class="form-control datepicker" value="<%=DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"))%>" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></label>
                        <div class="col-sm-8">
                            <input id="iptSearch" class="form-control" type="text" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603070") %>">
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-11 mr-auto text-center">
                            <button id="btnSearch" class="btn btn-info">
                                <span class="btn-label">
                                    <i class="material-icons">search</i>
                                </span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                            </button>
                        </div>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->

    <div class="studentList row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">account_circle</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603071") %></h4>
                </div>
                <div class="card-body">
                    <div class="toolbar">
                        <!-- Here you can write extra buttons/actions for the toolbar -->
                    </div>
                    <div class="row" style="padding-top: 0.9375rem;">
                        <div class="col-sm-12">
                            <ul class="nav nav-pills" id="tx-tabs" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active" id="payment-gateway-tab" data-toggle="tab" href="#payment-gateway" role="tablist">Payment Gateway</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" id="promptpay-qr-tab" data-toggle="tab" href="#promptpay-qr" role="tablist">Promptpay QR</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" id="payment-gateway-ktb-tab" data-toggle="tab" href="#payment-gateway-ktb" role="tablist">Payment Gateway (KTB)</a>
                                </li>
                            </ul>
                            <div class="tab-content tab-space">
                                <div class="tab-pane active" id="payment-gateway">
                                    <div class="table-responsive">
                                        <table id="tblPaymentGateway" class="table table-hover text-nowrap">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" style="width: 8%;">ID</th>
                                                    <th class="text-center" style="width: 10%;">Date</th>
                                                    <th class="text-center" style="width: 15%;">ChargeID</th>
                                                    <th class="text-center" style="width: 10%;">ReferenceNo</th>
                                                    <th class="text-center" style="width: 5%;">Code</th>
                                                    <th class="text-center" style="width: 12%;">Name</th>
                                                    <th class="text-center" style="width: 10%;">Amount</th>
                                                    <th class="text-center" style="width: 10%;">Status</th>
                                                    <th class="text-center" style="width: 25%;">Note</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="text-center">7903032</td>
                                                    <td class="text-center">2024-01-03 01:12:55.727</td>
                                                    <td class="text-center">chrg_prod_567d85a67a7eb2543ee84a4e356e2c1bfef</td>
                                                    <td class="text-center">081967010300024S32544</td>
                                                    <td class="text-center">32544</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">77.00</td>
                                                    <td><span class="badge badge-pill bg-success">SUCCESS</span></td>
                                                    <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132741") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">7903044</td>
                                                    <td class="text-center">2024-01-03 11:54:37.183</td>
                                                    <td class="text-center">chrg_prod_5674d21bf5a80fe4f299b603709d3730f9e</td>
                                                    <td class="text-center">081967010300036S30436</td>
                                                    <td class="text-center">32544</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">100.00</td>
                                                    <td><span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan">BOT15MIN</span></td>
                                                    <td>Updated: 2024-01-03 12:05:34.950<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132742") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">7903111</td>
                                                    <td class="text-center">2024-01-03 02:27:08.913</td>
                                                    <td class="text-center">chrg_prod_567f42c69ff632a417a89652f4b493d5d0c</td>
                                                    <td class="text-center">081967010300103S29221</td>
                                                    <td class="text-center">32544</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">0.70</td>
                                                    <td><span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan">BOT3AM</span></td>
                                                    <td>Updated: 2024-01-04 02:27:08.913<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132743") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">7903111</td>
                                                    <td class="text-center">2024-01-03 02:27:08.913</td>
                                                    <td class="text-center">chrg_prod_567f42c69ff632a417a89652f4b493d5d0c</td>
                                                    <td class="text-center">081967010300103S29221</td>
                                                    <td class="text-center">32544</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">0.70</td>
                                                    <td><span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan">BOTMANUAL</span></td>
                                                    <td>Updated: 2024-01-03 02:28:25.120<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132744") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">7903104</td>
                                                    <td class="text-center">2024-01-03 02:19:04.910</td>
                                                    <td class="text-center">chrg_prod_56759180f9abb024659990d4f06a1eb4dc3</td>
                                                    <td class="text-center">081967010300096S31615</td>
                                                    <td class="text-center">32544</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">100.00</td>
                                                    <td><span class="badge badge-pill bg-warning">CANCELLED</span></td>
                                                    <td>QR is cancelled and cannot be used.<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132745") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">7903106</td>
                                                    <td class="text-center">2024-01-03 02:20:13.207</td>
                                                    <td class="text-center">chrg_prod_567f42c69ff632a417a89652f4b493d5d0c</td>
                                                    <td class="text-center">081967010300098S31615</td>
                                                    <td class="text-center">32544</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">50.00</td>
                                                    <td><span class="badge badge-pill bg-danger">EXPIRED</span></td>
                                                    <td>QR is expired and cannot be used.<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132745") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">7903106</td>
                                                    <td class="text-center">2024-01-03 02:20:13.207</td>
                                                    <td class="text-center">chrg_prod_567f42c69ff632a417a89652f4b493d5d0c</td>
                                                    <td class="text-center">081967010300098S31615</td>
                                                    <td class="text-center">32544</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">50.00</td>
                                                    <td><span class="badge badge-pill bg-orange">NO FOUND</span></td>
                                                    <td>Transaction does not exist in the bank system.<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132745") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">7903106</td>
                                                    <td class="text-center">2024-01-03 02:20:13.207</td>
                                                    <td class="text-center">chrg_prod_567f42c69ff632a417a89652f4b493d5d0c</td>
                                                    <td class="text-center">081967010300098S31615</td>
                                                    <td class="text-center">32544</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">50.00</td>
                                                    <td><span class="badge badge-pill bg-citrine">NOT PAID</span></td>
                                                    <td>QR is requested but not yet paid or cancelled.<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132745") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">8572149</td>
                                                    <td class="text-center">2024-01-31 12:11:45.760</td>
                                                    <td class="text-center">chrg_prod_8207f5ae6573e58f423f898349d847f114ed</td>
                                                    <td class="text-center">C025967013124084S6313IV</td>
                                                    <td class="text-center">6313</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">9470.00</td>
                                                    <td><span class="badge badge-pill bg-cyan-blue-azure">PRE-AUTHORIZED</span></td>
                                                    <td>FailureCode: 50, FailureMessage: Failed Authentication<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132745") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">8577518</td>
                                                    <td class="text-center">2024-01-31 17:46:37.493</td>
                                                    <td class="text-center">chrg_prod_722b5513919e20f47b8aa10709bc0994091</td>
                                                    <td class="text-center">C087867013129459S9142IV</td>
                                                    <td class="text-center">9142</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">30000.00</td>
                                                    <td><span class="badge badge-pill bg-royal-red">DECLINED</span></td>
                                                    <td>Status: fail, FailureCode: 58, FailureMessage: Transaction not Permitted to Terminal<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132745") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">7903106</td>
                                                    <td class="text-center">2024-01-03 02:20:13.207</td>
                                                    <td class="text-center">chrg_prod_567f42c69ff632a417a89652f4b493d5d0c</td>
                                                    <td class="text-center">081967010300098S31615</td>
                                                    <td class="text-center">32544</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">50.00</td>
                                                    <td><span class="badge badge-pill bg-gray">???</span></td>
                                                    <td>Pending...<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132746") %></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="tab-pane" id="promptpay-qr">
                                    <div class="table-responsive">
                                        <table id="tblPromptpayQR" class="table table-hover text-nowrap">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" style="width: 8%;">ID</th>
                                                    <th class="text-center" style="width: 10%;">Date</th>
                                                    <th class="text-center" style="width: 20%;">Shop</th>
                                                    <th class="text-center" style="width: 10%;">IMEI</th>
                                                    <th class="text-center" style="width: 12%;">TxnNo</th>
                                                    <th class="text-center" style="width: 10%;">Amount</th>
                                                    <th class="text-center" style="width: 10%;">Status</th>
                                                    <th class="text-center" style="width: 20%;">Note</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="text-center">904458</td>
                                                    <td class="text-center">2024-01-25 08:32:21.797</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132754") %></td>
                                                    <td class="text-center">2440790443045c1202d1</td>
                                                    <td class="text-center">API17060585041091292</td>
                                                    <td class="text-center">145.00</td>
                                                    <td><span class="badge badge-pill bg-success">SUCCESS</span></td>
                                                    <td>StatusCode: 00, ErrorCode: 00, GUID NOT NULL<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132741") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">904483</td>
                                                    <td class="text-center">2024-01-25 08:32:21.797</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132754") %></td>
                                                    <td class="text-center">244079044304581a0951</td>
                                                    <td class="text-center">202401240361007</td>
                                                    <td class="text-center">40.00</td>
                                                    <td><span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan">BOT15MIN</span></td>
                                                    <td>StatusCode: 00, GUID NOT NULL, TxnStatus: PAID(1)<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132742") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">904483</td>
                                                    <td class="text-center">2024-01-25 08:32:21.797</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132754") %></td>
                                                    <td class="text-center">244079044304581a0951</td>
                                                    <td class="text-center">202401240361007</td>
                                                    <td class="text-center">40.00</td>
                                                    <td><span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan">BOT8AM</span></td>
                                                    <td>StatusCode: 00, GUID NOT NULL, TxnStatus: PAID(1)<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132747") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">904639</td>
                                                    <td class="text-center">2024-01-25 08:32:21.797</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132754") %></td>
                                                    <td class="text-center">24407904430458330511</td>
                                                    <td class="text-center">API17060595332010075</td>
                                                    <td class="text-center">20.00</td>
                                                    <td><span class="badge badge-pill bg-warning">EXPIRED</span></td>
                                                    <td>TxnStatus: EXPIRED(1)<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132748") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">904468</td>
                                                    <td class="text-center">2024-01-25 08:32:21.797</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132754") %></td>
                                                    <td class="text-center">24c079044304583307d1</td>
                                                    <td class="text-center">API17060586101338761</td>
                                                    <td class="text-center">17.00</td>
                                                    <td><span class="badge badge-pill bg-danger">EXPIRED</span></td>
                                                    <td>TxnStatus: EXPIRED(2)<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132749") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">904468</td>
                                                    <td class="text-center">2024-01-25 08:32:21.797</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132754") %></td>
                                                    <td class="text-center">24c079044304583307d1</td>
                                                    <td class="text-center">API17060586101338761</td>
                                                    <td class="text-center">17.00</td>
                                                    <td><span class="badge badge-pill bg-gray">???</span></td>
                                                    <td>PartnerTxnUID: 022901451244067012400065Q, TxnNo:<br />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132750") %></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="tab-pane" id="payment-gateway-ktb">
                                    <div class="table-responsive">
                                        <table id="tblPaymentGatewayKTB" class="table table-hover text-nowrap">
                                            <thead>
                                                <tr>
                                                    <th class="text-center" style="width: 8%;">ID</th>
                                                    <th class="text-center" style="width: 10%;">Date</th>
                                                    <th class="text-center" style="width: 8%;">Type</th>
                                                    <th class="text-center" style="width: 10%;">Ref2</th>
                                                    <th class="text-center" style="width: 12%;">Code</th>
                                                    <th class="text-center" style="width: 12%;">Name</th>
                                                    <th class="text-center" style="width: 10%;">Amount</th>
                                                    <th class="text-center" style="width: 10%;">Status</th>
                                                    <th class="text-center" style="width: 20%;">Note</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="text-center">60558</td>
                                                    <td class="text-center">2024-06-27 13:23:19.813</td>
                                                    <td class="text-center">TOPUP</td>
                                                    <td class="text-center">010113517400060558</td>
                                                    <td class="text-center">38560</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">50.00</td>
                                                    <td><span class="badge badge-pill bg-success">SUCCESS</span></td>
                                                    <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132741") %><br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">60520</td>
                                                    <td class="text-center">2024-06-27 12:45:05.547</td>
                                                    <td class="text-center">SHOP</td>
                                                    <td class="text-center">030000244000060520</td>
                                                    <td class="text-center">9909</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">20.00</td>
                                                    <td><span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan">BOT8AM</span></td>
                                                    <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132747") %><br />
                                                        Updated: 2024-06-28 08:01:16.527<br />
                                                        Shop: ร้านน้ำ 2</td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">6467</td>
                                                    <td class="text-center">2024-05-29 05:33:55.213</td>
                                                    <td class="text-center">INVOICE</td>
                                                    <td class="text-center">040122940600006467</td>
                                                    <td class="text-center">40859</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">1050.00</td>
                                                    <td><span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan">BOTMANUAL</span></td>
                                                    <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132744") %><br />
                                                        Updated: 2024-06-27 11:10:37.460<br />
                                                        Invoice: INV-20240501238</td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">58058</td>
                                                    <td class="text-center">2024-06-27 10:47:08.730</td>
                                                    <td class="text-center">TOPUP</td>
                                                    <td class="text-center">010108041800058058</td>
                                                    <td class="text-center">40257</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">50.00</td>
                                                    <td><span class="badge badge-pill bg-warning">CANCELLED</span></td>
                                                    <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132751") %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">167830</td>
                                                    <td class="text-center">2024-07-30 11:38:34.747</td>
                                                    <td class="text-center">TOPUP</td>
                                                    <td class="text-center">010108862400167830</td>
                                                    <td class="text-center">11075</td>
                                                    <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132753") %></td>
                                                    <td class="text-center">50.00</td>
                                                    <td><span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan-blue-azure">BANKDELAY</span></td>
                                                    <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132752") %><br />
                                                        Updated: 2024-07-30 12:04:13.297</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">

        function PaymentGatewayStatusDisplay(obj) {
            var status = '';
            if (obj.success == 1) {
                if (obj.bot15MIN == 1) {
                    status = '<span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan">BOT15MIN</span>';
                }
                else if (obj.bot3AM == 1) {
                    status = '<span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan">BOT3AM</span>';
                }
                else if (obj.botMANUAL == 1) {
                    status = '<span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan">BOTMANUAL</span>';
                }
                else {
                    status = '<span class="badge badge-pill bg-success">SUCCESS</span>';
                }
            }
            else if (obj.errorCode == 'no_transaction_found') {
                status = '<span class="badge badge-pill bg-orange">NO FOUND</span>';
            }
            else if (obj.errorCode == 'transaction_cancelled') {
                status = '<span class="badge badge-pill bg-warning">CANCELLED</span>';
            }
            else if (obj.errorCode == 'transaction_expired') {
                status = '<span class="badge badge-pill bg-danger">EXPIRED</span>';
            }
            else if (obj.errorCode == 'transaction_not_yet_paid') {
                status = '<span class="badge badge-pill bg-citrine">NOT PAID</span>';
            }
            else if (obj.errorCode == 'Declined') {
                status = '<span class="badge badge-pill bg-royal-red">DECLINED</span>';
            }
            else if (obj.errorCode == 'Pre-Authorized') {
                status = '<span class="badge badge-pill bg-cyan-blue-azure">PRE-AUTHORIZED</span>';
            }
            else {
                status = '<span class="badge badge-pill bg-gray">???</span>';
            }
            return status;
        }

        function PaymentGatewayNoteDisplay(obj) {
            var note = '';
            if (obj.success == 1) {
                if (obj.bot15MIN == 1 || obj.bot3AM == 1 || obj.botMANUAL == 1) {
                    note = 'Updated: ' + obj.updatedDate;
                }
                else {
                    note = '';
                }
            }
            else if (obj.errorCode == 'no_transaction_found') {
                note = 'Transaction does not exist in the bank system.';
            }
            else if (obj.errorCode == 'transaction_cancelled') {
                note = 'QR is cancelled and cannot be used.';
            }
            else if (obj.errorCode == 'transaction_expired') {
                note = 'QR is expired and cannot be used.';
            }
            else if (obj.errorCode == 'transaction_not_yet_paid') {
                note = 'QR is requested but not yet paid or cancelled.';
            }
            else if (obj.errorCode == 'Declined') {
                note = obj.errorMessage;
            }
            else if (obj.errorCode == 'Pre-Authorized') {
                note = obj.errorMessage;
            }
            else {
                note = 'Pending...';
            }
            return note;
        }

        function PromptpayQRStatusDisplay(obj) {
            var status = '';
            if (obj.statusCode == '00' && obj.errorCode == '00' && !!obj.sellGuID) {
                status = '<span class="badge badge-pill bg-success">SUCCESS</span>';
            }
            else if (obj.statusCode == '00' && !!obj.sellGuID && obj.paid == 1) {
                status = '<span class="badge badge-pill bg-primary">PAID</span>';

                if (obj.bot8AM == 1)
                    status += ' <span class="badge badge-pill bg-cyan">BOT8AM</span>';
                else
                    status += ' <span class="badge badge-pill bg-cyan">BOT15MIN</span>';
            }
            else if (obj.expired == 1) {
                status = '<span class="badge badge-pill bg-warning">EXPIRED</span>';
            }
            else if (obj.expired > 1) {
                status = '<span class="badge badge-pill bg-danger">EXPIRED</span>';
            }
            else {
                status = '<span class="badge badge-pill bg-gray">???</span>';
            }
            return status;
        }

        function PromptpayQRNoteDisplay(obj) {
            var note = '';
            if (obj.statusCode == '00' && obj.errorCode == '00' && !!obj.sellGuID) {
                note = 'StatusCode: 00, ErrorCode: 00, GUID NOT NULL';
            }
            else if (obj.statusCode == '00' && !!obj.sellGuID && obj.paid == 1) {
                note = 'StatusCode: 00, GUID NOT NULL, TxnStatus: PAID(1)';

                if (obj.bot8AM == 1)
                    note += ', Bot: 8AM';
                else
                    note += ', Bot: 15MIN';
            }
            else if (obj.expired == 1) {
                note = 'TxnStatus: EXPIRED(1)';
            }
            else if (obj.expired > 1) {
                note = 'TxnStatus: EXPIRED(2)';
            }
            else {
                note = 'PartnerTxnUID: ' + obj.partnerTxnUID + ', TxnNo: ' + BlankForNull(obj.txnNo);
            }
            return note;
        }

        function PaymentGatewayKTBStatusDisplay(obj) {
            var status = '';
            if (obj.success == 0) {
                if (obj.bot3AM == 1) {
                    status = '<span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan">BOT3AM</span>';
                }
                else if (obj.bankDELAY == 1) {
                    status = '<span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan-blue-azure">BANKDELAY</span>';
                }
                else if (obj.botMANUAL == 1) {
                    status = '<span class="badge badge-pill bg-primary">PAID</span> <span class="badge badge-pill bg-cyan">BOTMANUAL</span>';
                }
                else {
                    status = '<span class="badge badge-pill bg-success">SUCCESS</span>';
                }
            }
            else {
                status = '<span class="badge badge-pill bg-citrine">NOT PAID</span>';
            }
            return status;
        }

        function PaymentGatewayKTBNoteDisplay(obj) {
            var note = '';
            if (obj.success == 0) {
                if (obj.bot3AM == 1 || obj.bankDELAY == 1 || obj.botMANUAL == 1) {
                    note = 'Updated: ' + obj.updatedDate; //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132743") %><br /> //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132744") %><br />
                }
                else {
                    note = ''; //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132741") %>
                }

                if (obj.transType == 'SHOP') {
                    note += 'Shop: ' + obj.shop;
                }
                else if (obj.transType == 'INVOICE') {
                    note += 'Invoice: ' + obj.invoiceCode;
                }
            }
            else {
                if (obj.approvalRespCode == 0 && !obj.paymentTransID) {
                    note = 'รายการที่ Approval แล้วแต่ไม่ได้รับ Callback Payment กลับมา';
                }
                else {
                    note = ''; //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132751") %>
                }
            }
            return note;
        }

        function BlankForNull(s) {
            return !s ? "" : s;
        }

        $(function () {

            // Initial control
            $('.datepicker').datetimepicker({
                keepOpen: false,
                debug: false,
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
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

            $(".datepicker").attr('maxlength', '10');

            $('.nav-pills li').click(function () {
                var clickTab = $(this).find("a").attr("id");
                switch (clickTab) {
                    case 'payment-gateway-tab':
                        $('#iptSearch').attr("placeholder", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603070") %>");
                        //$('#btnSearch').attr('disabled', true);
                        break;
                    case 'promptpay-qr-tab':
                        $('#iptSearch').attr("placeholder", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603072") %>");
                        //$('#btnSearch').attr('disabled', false);
                        break;
                    case 'payment-gateway-ktb-tab':
                        $('#iptSearch').attr("placeholder", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603073") %>");
                        //$('#btnSearch').attr('disabled', true);
                        break;
                }
            });

            $("#btnSearch").click(function () {
                if ($(this).attr('disabled') || $(this).hasClass('disabled')) return;

                $(this).button("loading");

                var activeTab = $("#tx-tabs").find(".active").attr("id");
                switch (activeTab) {
                    case 'payment-gateway-tab':
                        $.ajax({
                            url: "PaymentTransaction.aspx/GetPaymentGatewayTx",
                            data: JSON.stringify({ search: $('#iptSearch').val(), startDate: $('#iptStartDate').val(), endDate: $('#iptEndDate').val() }),
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                var r = JSON.parse(response.d);
                                if (r.success) {
                                    // clear table rows
                                    $("#tblPaymentGateway > tbody").empty();

                                    // add rows
                                    $.each(r.data, function (i, obj) {
                                        $('#tblPaymentGateway > tbody').append(`<tr>
                                            <td class="text-center">`+ obj.transactionId + `</td>
                                            <td class="text-center">`+ obj.createDate + `</td>
                                            <td class="text-center">`+ BlankForNull(obj.chargeId) + `</td>
                                            <td class="text-center">`+ obj.referenceNo + `</td>
                                            <td class="text-center">`+ obj.code + `</td>
                                            <td class="text-center">`+ BlankForNull(obj.name) + ` ` + BlankForNull(obj.lastname) + `</td>
                                            <td class="text-center">`+ obj.amount + `</td>
                                            <td>`+ PaymentGatewayStatusDisplay(obj) + `</td>
                                            <td>`+ PaymentGatewayNoteDisplay(obj) + `</td>
                                        </tr>`);
                                    });
                                    //

                                    swal.fire({
                                        type: "success",
                                        title: "Your process has been completed."
                                    });
                                }
                                else {
                                    swal.fire({
                                        type: 'error',
                                        title: "Oops...",
                                        text: "Something went wrong! [Error: " + r.message + "]"
                                    });
                                }
                                //

                                $("#btnSearch").button("reset");
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                swal.fire({
                                    type: 'error',
                                    title: "Oops...",
                                    text: "Something went wrong! [XMLHttpRequest: " + XMLHttpRequest + ", textStatus: " + textStatus + ", errorThrown: " + errorThrown +"]"
                                });
                            }
                        });
                        //

                        break;
                    case 'promptpay-qr-tab':
                        $.ajax({
                            url: "PaymentTransaction.aspx/GetPromptpayQRTx",
                            data: JSON.stringify({ search: $('#iptSearch').val(), startDate: $('#iptStartDate').val(), endDate: $('#iptEndDate').val() }),
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                var r = JSON.parse(response.d);
                                if (r.success) {
                                    // clear table rows
                                    $("#tblPromptpayQR > tbody").empty();

                                    // add rows
                                    $.each(r.data, function (i, obj) {
                                        $('#tblPromptpayQR > tbody').append(`<tr>
                                            <td class="text-center">`+ obj.transactionId + `</td>
                                            <td class="text-center">`+ obj.createDate + `</td>
                                            <td class="text-center">`+ obj.shop + `</td>
                                            <td class="text-center">`+ obj.imei + `</td>
                                            <td class="text-center">`+ BlankForNull(obj.txnNo) + `</td>
                                            <td class="text-center">`+ obj.txnAmount + `</td>
                                            <td>`+ PromptpayQRStatusDisplay(obj) + `</td>
                                            <td>`+ PromptpayQRNoteDisplay(obj) + `</td>
                                        </tr>`);
                                    });
                                    //

                                    swal.fire({
                                        type: "success",
                                        title: "Your process has been completed."
                                    });
                                }
                                else {
                                    swal.fire({
                                        type: 'error',
                                        title: "Oops...",
                                        text: "Something went wrong! [Error: " + r.message + "]"
                                    });
                                }
                                //

                                $("#btnSearch").button("reset");
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                swal.fire({
                                    type: 'error',
                                    title: "Oops...",
                                    text: "Something went wrong! [XMLHttpRequest: " + XMLHttpRequest + ", textStatus: " + textStatus + ", errorThrown: " + errorThrown + "]"
                                });
                            }
                        });
                        //

                        break;
                    case 'payment-gateway-ktb-tab':
                        $.ajax({
                            url: "PaymentTransaction.aspx/GetPaymentGatewayKTBTx",
                            data: JSON.stringify({ search: $('#iptSearch').val(), startDate: $('#iptStartDate').val(), endDate: $('#iptEndDate').val() }),
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                var r = JSON.parse(response.d);
                                if (r.success) {
                                    // clear table rows
                                    $("#tblPaymentGatewayKTB > tbody").empty();

                                    // add rows
                                    $.each(r.data, function (i, obj) {
                                        $('#tblPaymentGatewayKTB > tbody').append(`<tr>
                                            <td class="text-center">`+ obj.transactionId + `</td>
                                            <td class="text-center">`+ obj.createDate + `</td>
                                            <td class="text-center">`+ obj.transType + `</td>
                                            <td class="text-center">`+ obj.ref2 + `</td>
                                            <td class="text-center">`+ obj.code + `</td>
                                            <td class="text-center">`+ BlankForNull(obj.name) + ` ` + BlankForNull(obj.lastname) + `</td>
                                            <td class="text-center">`+ obj.amount + `</td>
                                            <td>`+ PaymentGatewayKTBStatusDisplay(obj) + `</td>
                                            <td>`+ PaymentGatewayKTBNoteDisplay(obj) + `</td>
                                        </tr>`);
                                    });
                                    //

                                    swal.fire({
                                        type: "success",
                                        title: "Your process has been completed."
                                    });
                                }
                                else {
                                    swal.fire({
                                        type: 'error',
                                        title: "Oops...",
                                        text: "Something went wrong! [Error: " + r.message + "]"
                                    });
                                }
                                //

                                $("#btnSearch").button("reset");
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                swal.fire({
                                    type: 'error',
                                    title: "Oops...",
                                    text: "Something went wrong! [XMLHttpRequest: " + XMLHttpRequest + ", textStatus: " + textStatus + ", errorThrown: " + errorThrown + "]"
                                });
                            }
                        });
                        //

                        break;
                }
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
