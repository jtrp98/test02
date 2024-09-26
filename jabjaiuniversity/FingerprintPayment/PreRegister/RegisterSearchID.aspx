<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterSearchID.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterSearchID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStyle" runat="server">
    <style>
        .input-round {
            height: 45px;
            border: 1px solid #ccc;
            text-align: center;
            font-size: 1.5em;
        }

        .form-control:invalid {
            background-image: linear-gradient(to top, #f44336 2px, rgba(244, 67, 54, 0) 2px), linear-gradient(to top, #d2d2d2 0px, rgba(210, 210, 210, 0) 0px);
        }

        label.error {
            color: red;
        }

        #iptIDCard {
            letter-spacing: 20px;
        }

        @media (min-width: 320px) and (max-width: 767px) {

            #iptIDCard {
                letter-spacing: 14px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <form id="form">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <br>
                            <br>
                            <br>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center" style="padding-top: 25px;">
                                <span style="margin-top: 28px; font-size: 2.1em; color: #000; font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132966") %></span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center" style="padding-top: 25px;">
                                <span style="margin-top: 28px; font-size: 1.4em; color: #000; font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %> / passport</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-7 ml-auto mr-auto text-center" style="padding-top: 25px;">
                                <input id="iptIDCard" name="iptIDCard" type="text" class="form-control input-round" placeholder="" maxlength="13">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-5 ml-auto mr-auto mx-auto text-center" style="padding-top: 25px;">
                                <button id="btnPrint" class="btn btn-success btn-round btn-block" style="font-size: 1.5em;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>
                                </button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8 ml-auto mr-auto text-center" style="padding-top: 25px;">
                                <span style="margin-top: 28px; font-size: 1.1em; color: #000;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132818") %></span>
                            </div>
                        </div>
                        <div class="row">
                            <br>
                            <br>
                            <br>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">
    <script>

        function LoadDataFromLocalStorage() {
            // Get data from localStorage
            if (ls.isBrowserSupport()) {
                // Code for localStorage
                preRegister = ls.getItem('preRegister');

                if (preRegister.FullComplete) {
                    $('#iptIDCard').val(preRegister.IDCard);
                }

            } else {
                // No web storage Support.
            }
        }

        function ChooseStudyApplication(registerID) {

            $.ajax({
                async: false,
                type: "POST",
                url: 'RegisterSearchID.aspx/ChooseStudyApplication',
                data: JSON.stringify({ registerID: registerID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    var r = JSON.parse(result.d);
                    if (r.success) {

                        window.location.href = r.redirectUrl;

                    }
                    else {
                        Swal.fire({
                            title: 'Warning!',
                            text: 'Warning Message : ' + r.message,
                            type: 'warning',
                            confirmButtonClass: "btn btn-warning",
                            buttonsStyling: false
                        });
                    }
                },
                error: function (response) {
                    console.log(response.d);
                }
            });

            return false;
        }

        $(document).ready(function () {

            LoadDataFromLocalStorage();

            $("#form").validate({
                rules: {
                    iptIDCard: {
                        required: true,
                        minlength: 7,
                        maxlength: 13
                    }
                },
                messages: {
                    iptIDCard: {
                        required: "This field is required.",
                        minlength: jQuery.validator.format("Please, at least {0} characters are necessary."),
                        maxlength: jQuery.validator.format("Please, at most {0} characters are necessary.")
                    }
                },
                onfocusout: false,
                invalidHandler: function (form, validator) {
                    var errors = validator.numberOfInvalids();
                    if (errors) {
                        validator.errorList[0].element.focus();
                    }
                }
            });

            $("#btnPrint").bind({
                click: function () {

                    $.ajax({
                        async: false,
                        type: "POST",
                        url: 'RegisterSearchID.aspx/SearchData',
                        data: JSON.stringify({ id: $('#iptIDCard').val() }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {

                            var r = JSON.parse(result.d);
                            if (r.success) {
                                switch (r.registerDatas.length) {
                                    case 1:

                                        window.location.href = r.redirectUrl;

                                        break;
                                    default:

                                        var listStudyApplicationYear = '';
                                        $.each(r.registerDatas, function (index, row) {

                                            listStudyApplicationYear += '<a href="#" onClick="ChooseStudyApplication(' + row.RegisterID + '); return false;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132967") %> ' + row.RegisterYear + '</a> <br/>';

                                        });

                                        Swal.fire({
                                            title: '<strong>พบใบสมัคร ' + r.registerDatas.length + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132968") %></strong>',
                                            icon: 'info',
                                            html:
                                                'กรุณาเลือกใบสมัคร: <br/>' + listStudyApplicationYear,
                                            showCloseButton: true,
                                            showCancelButton: false,
                                        });

                                        break;
                                }
                            }
                            else {
                                Swal.fire({
                                    title: 'Warning!',
                                    text: 'Warning Message : ' + r.message,
                                    type: 'warning',
                                    confirmButtonClass: "btn btn-warning",
                                    buttonsStyling: false
                                });
                            }
                        },
                        error: function (response) {
                            console.log(response.d);
                        }
                    });

                    return false;
                }
            });

        });
    </script>
</asp:Content>
