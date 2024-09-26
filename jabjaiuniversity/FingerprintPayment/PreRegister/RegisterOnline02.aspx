<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterOnline02.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnline02" %>

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
                                <span style="margin-top: 28px; font-size: 2.1em; color: #000; font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132827") %></span>
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
                                <button id="btnNext" class="btn btn-success btn-round btn-block" style="font-size: 1.5em;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
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

                if (preRegister.Page02Saved) {
                    $('#iptIDCard').val(preRegister.IDCard);
                }

            } else {
                // No web storage Support.
            }
        }

        function SaveDataToLocalStorage() {
            if ($("#form").valid()) {

                if ('<%=(string)Session["RegisterF"]%>' != 'sendmail') {

                    // Check new identity
                    if (preRegister.IDCard != $('#iptIDCard').val()) {

                        preRegister = {}; // Clear all local storage
                        preRegister.Page01Saved = true;

                        preRegister.Page03Saved = preRegister.Page04Saved = preRegister.Page05Saved = preRegister.Page06Saved = preRegister.Page07Saved = preRegister.Page08Saved = preRegister.Page09Saved = preRegister.Page10Saved = preRegister.Page11Saved = preRegister.Page12Saved = false;
                    }

                    preRegister.FullComplete = false;

                    preRegister.Page02Saved = true;

                    if (preRegister.Files == null) {
                        preRegister.Files = [];
                    }
                }

                preRegister.IDCard = $('#iptIDCard').val();
                preRegister.StudentIDCard = $('#iptIDCard').val();

                // Save data to localStorage
                if (ls.isBrowserSupport()) {
                    // Code for localStorage
                    ls.setItem('preRegister', preRegister);
                } else {
                    // No web storage Support.
                }

                ez.activePageComplete(2);

                window.location.href = "RegisterOnline03.aspx";

            }
        }

        var preRegister = null;
        $(document).ready(function () {

            LoadDataFromLocalStorage();

            $("#form").validate({
                rules: {
                    iptIDCard: {
                        required: true,
                        minlength: 13,
                        maxlength: 13
                    }
                },
                messages: {
                    iptIDCard: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
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

            $("#btnNext").bind({
                click: function () {

                    $.ajax({
                        async: false,
                        type: "POST",
                        url: 'RegisterOnline02.aspx/SearchData',
                        data: JSON.stringify({ id: $('#iptIDCard').val() }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {

                            var r = JSON.parse(result.d);
                            if (r.success) {

                                if ('<%=(string)Session["RegisterF"]%>' == 'sendmail') {
                                    preRegister = r.registerData;
                                }

                                SaveDataToLocalStorage();
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
