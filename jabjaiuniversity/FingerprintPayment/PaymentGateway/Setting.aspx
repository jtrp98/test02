<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master"
    AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="FingerprintPayment.PaymentGateway.Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <!-- bootstrap-toggle -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" />

    <style>
        .toggle.toggle-switch {
            width: 100px !important;
            height: 40px !important;
            border-radius: 5px !important;
            border: 1px solid transparent;
        }

            .toggle.toggle-switch .toggle-handle {
                height: 89%;
                left: 0px;
                top: 5%;
                padding: 0 10px;
            }

        .toggle-handle.btn {
            color: #333;
            background-color: #fff;
            border-color: #ccc;
            padding: 6px 12px;
        }

        label.left {
            display: flex;
            justify-content: left;
            align-items: center;
            /*height: 100%;*/
            margin-top: 6px;
        }

        .bmd-form-group {
            display: flex;
            justify-content: left;
            /*height: 100%;*/
            flex-direction: column;
        }

        label.error {
            margin-top: 5px;
        }

        .row.h50 {
            height: 50px;
        }

        .row.mb12 {
            margin-bottom: 12px;
        }

        .form-control {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504016") %>
            </p>
        </div>
    </div>
    <h3 style="margin: 20px 0px 0px 14px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504017") %></h3>
    <div class="wallet row">
        <div class="col-md-12">
            <form class="form-padding">
                <div class="card">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">wallet</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504018") %></h4>
                    </div>
                    <div class="card-body">
                        <div class="row mb12">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">
                                <label class="left">Active:</label>
                            </div>
                            <div class="col-sm-8">
                                <input name="Active1" class="card-title right" type="checkbox" data-toggle="toggle" data-on="On" data-off="Off" data-onstyle="success" data-height="40" data-width="100" data-style="toggle-switch" <%=((paymentGateway.Fd_Active ?? false) ? "checked" : "") %> />
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row h50">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">
                                <label class="left">Public Key:</label>
                            </div>
                            <div class="col-sm-8">
                                <input type="text" class="form-control" value="<%=paymentGateway.Fd_PublicKey %>" name="PublicKey1" maxlength="50" />
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row h50">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">
                                <label class="left">Secret Key:</label>
                            </div>
                            <div class="col-sm-8">
                                <input type="text" class="form-control" value="<%=paymentGateway.Fd_SecretKey %>" name="SecretKey1" maxlength="50" />
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row h50">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">
                                <label class="left">Fee:</label>
                            </div>
                            <div class="col-sm-8">
                                <input type="text" class="form-control" value="<%=paymentGateway.Fd_FeePayment %>" name="Fee1" maxlength="5" />
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <h2 class="col-sm-11 text-left" style="color: red; font-size: 14px; padding: 12px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %>:
                            <br />
                                1. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504019") %>
                            <br />
                                2. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504020") %>
                            <br />
                                3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504021") %> 
                            </h2>
                        </div>
                        <div class="row h50">
                            <div class="col-sm-10 text-center">
                                <div class="btn btn-success" onclick="updatePaymentGetway('wallet');"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></div>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
    <div class="invoice row">
        <div class="col-md-12">
            <form class="form-padding">
                <div class="card">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">school</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504022") %></h4>
                    </div>
                    <div class="card-body">
                        <div class="row mb12">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">
                                <label class="left">Active:</label>
                            </div>
                            <div class="col-sm-8">
                                <input name="Active2" class="card-title right" type="checkbox" data-toggle="toggle" data-on="On" data-off="Off" data-onstyle="success" data-height="40" data-width="100" data-style="toggle-switch" <%=((paymentGateway.Fd_ActiveInvoice ?? false) ? "checked" : "") %> />
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row h50">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">
                                <label class="left">Public Key:</label>
                            </div>
                            <div class="col-sm-8">
                                <input type="text" class="form-control" value="<%=paymentGateway.Fd_PublicKeyInvoice %>" name="PublicKey2" maxlength="50" />
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row h50">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">
                                <label class="left">Secret Key:</label>
                            </div>
                            <div class="col-sm-8">
                                <input type="text" class="form-control" value="<%=paymentGateway.Fd_SecretKeyInvoice %>" name="SecretKey2" maxlength="50" />
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row h50">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">
                                <label class="left">Fee:</label>
                            </div>
                            <div class="col-sm-8">
                                <input type="text" class="form-control" value="<%=paymentGateway.Fd_FeeInvoice %>" name="Fee2" maxlength="5" />
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <h2 class="col-sm-11 text-left" style="color: red; font-size: 14px; padding: 12px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %>:
                            <br />
                                1. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504019") %>
                            <br />
                                2. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504020") %>
                            <br />
                                3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504021") %> 
                            </h2>
                        </div>
                        <div class="row">
                            <div class="col-sm-10 text-center">
                                <div class="btn btn-success" onclick="updatePaymentGetway('invoice');"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></div>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
    <div class="qr_code row">
        <div class="col-md-12">
            <form class="form-padding">
                <div class="card">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">qr_code</i>
                        </div>
                        <h4 class="card-title">QR API</h4>
                    </div>
                    <div class="card-body">
                        <div class="row mb12">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">
                                <label class="left">Active:</label>
                            </div>
                            <div class="col-sm-8">
                                <input name="Active3" class="card-title right" type="checkbox" data-toggle="toggle" data-on="On" data-off="Off" data-onstyle="success" data-height="40" data-width="100" data-style="toggle-switch" <%=(paymentGateway.Fd_PromptPayActive == 1 ? "checked" : "") %> />
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row h50">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">
                                <label class="left">Merchant ID:</label>
                            </div>
                            <div class="col-sm-8">
                                <input type="text" class="form-control" value="<%=paymentGateway.Fd_MerchantMID %>" name="MerchantMID" maxlength="20" />
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row">
                            <div class="col-sm-10 text-center">
                                <div class="btn btn-success" onclick="updatePaymentGetway('qr_code');"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></div>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <h3 style="margin: 20px 0px 0px 14px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504023") %></h3>
    <div class="ktb_qr_code row">
        <div class="col-md-12">
            <form class="form-padding">
                <div class="card">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">qr_code</i>
                        </div>
                        <h4 class="card-title">QR API</h4>
                    </div>
                    <div class="card-body">
                        <div class="row mb12">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">
                                <label class="left">Active:</label>
                            </div>
                            <div class="col-sm-8">
                                <input name="Active4" class="card-title right" type="checkbox" data-toggle="toggle" data-on="On" data-off="Off" data-onstyle="success" data-height="40" data-width="100" data-style="toggle-switch" <%=((paymentGateway.Fd_KTBPayment ?? false) ? "checked" : "") %> />
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row h50">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">
                                <label class="left">Tax ID:</label>
                            </div>
                            <div class="col-sm-8">
                                <input type="text" class="form-control" value="<%=schoolInfo.TaxId %>" name="TaxId" maxlength="13" />
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row">
                            <div class="col-sm-10 text-center">
                                <div class="btn btn-success" onclick="updatePaymentGetway('ktb_qr_code');"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></div>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script src="/javascript/jquery-number/jquery.number.js" type="text/javascript"></script>

    <!-- bootstrap-toggle -->
    <script type='text/javascript' src="/assets/plugins/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>

    <script type="text/javascript">

        function updatePaymentGetway(paymentType) {

            var isValid = $("." + paymentType + " form").valid();
            if (isValid) {

                let SecretKey = $("." + paymentType + " input[name^=SecretKey]").val();
                let PublicKey = $("." + paymentType + " input[name^=PublicKey]").val();
                let Active = $("." + paymentType + " input[name^=Active]").prop('checked');
                let MerchantMID = $("." + paymentType + " input[name=MerchantMID]").val();
                let Fee = $("." + paymentType + " input[name^=Fee]").val();
                let TaxId = $("." + paymentType + " input[name=TaxId]").val();

                let data = {
                    "SecretKey": SecretKey, "PublicKey": PublicKey, "Active": Active, "MerchantMID": MerchantMID,
                    "paymentType": paymentType, "Fee": Fee, "TaxID": TaxId
                };

                console.log(data);

                $.ajax({
                    type: "POST",
                    url: "/PaymentGateway/Setting.aspx/updatePaymentGetway",
                    // The key needs to match your method's input parameter (case-sensitive).
                    data: JSON.stringify({ "paymentSetting": data }),
                    contentType: "application/json; charset=utf-8",
                    //dataType: "json",
                    success: function (r) {
                        //alert(r);
                        $('#AddNewUser').modal('hide');

                        if (r.d["Status"] == "success") {
                            Swal.fire({
                                icon: 'success',
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101266") %>',
                                //text: 'Something went wrong!',                      
                            })
                        }
                        else {
                            Swal.fire({
                                icon: 'error',
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132755") %>',
                                //text: 'Something went wrong!',                      
                            })
                        }
                    },
                    error: function (errMsg) {
                        //alert(errMsg);
                    }
                });
            }

            return false;
        }

        $(document).ready(function () {

            $(".wallet form").validate({
                rules: {
                    PublicKey1: {
                        required: function (element) {
                            return $("input[name=Active1]").prop('checked') && !$(element).val();
                        }
                    },
                    SecretKey1: {
                        required: function (element) {
                            return $("input[name=Active1]").prop('checked') && !$(element).val();
                        }
                    },
                    Fee1: {
                        required: false,
                        number: true
                    }
                },
                messages: {
                    PublicKey1: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                    },
                    SecretKey1: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                    },
                    Fee1: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    }
                },
                focusInvalid: false,
                invalidHandler: function () {
                    $(this).find(":input.error:first").focus();
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "PublicKey1":
                        case "SecretKey1":
                        case "Fee1": error.insertAfter(element); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            $(".invoice form").validate({
                rules: {
                    PublicKey2: {
                        required: function (element) {
                            return $("input[name=Active2]").prop('checked') && !$(element).val();
                        }
                    },
                    SecretKey2: {
                        required: function (element) {
                            return $("input[name=Active2]").prop('checked') && !$(element).val();
                        }
                    },
                    Fee2: {
                        required: false,
                        number: true
                    }
                },
                messages: {
                    PublicKey2: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                    },
                    SecretKey2: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                    },
                    Fee2: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    }
                },
                focusInvalid: false,
                invalidHandler: function () {
                    $(this).find(":input.error:first").focus();
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "PublicKey2":
                        case "SecretKey2":
                        case "Fee2": error.insertAfter(element); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            $(".qr_code form").validate({
                rules: {
                    MerchantMID: {
                        required: function (element) {
                            return $("input[name=Active3]").prop('checked') && !$(element).val();
                        }
                    }
                },
                messages: {
                    MerchantMID: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                    }
                },
                focusInvalid: false,
                invalidHandler: function () {
                    $(this).find(":input.error:first").focus();
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "MerchantMID": error.insertAfter(element); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            $(".ktb_qr_code form").validate({
                rules: {
                    TaxId: {
                        required: function (element) {
                            return $("input[name=Active4]").prop('checked') && !$(element).val();
                        }
                    }
                },
                messages: {
                    TaxId: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                    }
                },
                focusInvalid: false,
                invalidHandler: function () {
                    $(this).find(":input.error:first").focus();
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "TaxId": error.insertAfter(element); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            $('input[name^=Fee]').number(true, 2);
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
