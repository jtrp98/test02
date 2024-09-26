<%@ Page Title="" Language="C#" MasterPageFile="~/Material.Master" AutoEventWireup="true" CodeBehind="PaymentGetway.aspx.cs" Inherits="FingerprintPayment.TuitionFee.PaymentGetway" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style type="text/css">
        .bd-example-modal-lg .modal-dialog {
            display: table;
            position: relative;
            margin: 0 auto;
            top: calc(50% - 24px);
        }

        .btn.btn-round.btn-info {
            background-color: #00bcd4;
            border-top: 1px solid #00bcd4;
            border-bottom: 1px solid #00bcd4;
            background-color: white;
            color: black;
        }

            .btn.btn-round.btn-info:last-child {
                border-radius: 0px 10px 10px 0px;
                border-right: 1px solid #00bcd4;
            }

            .btn.btn-round.btn-info:first-child {
                border-radius: 10px 0px 0px 10px;
                border-left: 1px solid #00bcd4;
            }

            .btn.btn-round.btn-info.active {
                background-color: #00bcd4;
                border-top: 1px solid #00bcd4;
                border-bottom: 1px solid #00bcd4;
                background-color: #00bcd4;
                color: white;
            }

        .filter-option-inner-inner {
            padding-top: 4px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02720") %> &rsaquo; Payment Getway   
            </p>
        </div>
    </div>

    <div class="employeeList row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">search</i>
                    </div>
                    <h4 class="card-title">Payment Getway Wallet</h4>
                </div>

                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left">Secret Key</label>
                        <div class="col-sm-3 div-select-input">
                            <input class="form-control" />
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"></label>
                        <div class="col-sm-3 div-select-input">
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left">Public Key</label>
                        <div class="col-sm-3 div-select-input">
                            <input class="form-control" />
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"></label>
                        <div class="col-sm-3 div-select-input">
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133364") %></label>
                        <div class="col-sm-1 div-select-input">
                            <input class="form-control" />
                        </div>
                        <label class="col-sm-1 col-form-label text-left">%</label>
                        <div class="col-sm-1"></div>
                        <div class="col-sm-3 div-select-input">
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
                            <div id="btnSearch" class="btn btn-info" onclick="LoadStudent()">
                                <span class="btn-label">
                                    <i class="material-icons">search</i>
                                </span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>
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

    <div class="employeeList row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">search</i>
                    </div>
                    <h4 class="card-title">Payment Getway Invoice</h4>
                </div>

                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left">Secret Key</label>
                        <div class="col-sm-3 div-select-input">
                            <input class="form-control" />
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"></label>
                        <div class="col-sm-3 div-select-input">
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left">Public Key</label>
                        <div class="col-sm-3 div-select-input">
                            <input class="form-control" />
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"></label>
                        <div class="col-sm-3 div-select-input">
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133364") %></label>
                        <div class="col-sm-1 div-select-input">
                            <input class="form-control" />
                        </div>
                        <label class="col-sm-1 col-form-label text-left">%</label>
                        <div class="col-sm-1"></div>
                        <div class="col-sm-3 div-select-input">
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
                            <div id="btnSearch" class="btn btn-info" onclick="LoadStudent()">
                                <span class="btn-label">
                                    <i class="material-icons">search</i>
                                </span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>
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
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
