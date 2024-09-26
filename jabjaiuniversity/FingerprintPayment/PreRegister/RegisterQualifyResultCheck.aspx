<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterQualifyResultCheck.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterQualifyResultCheck" %>

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

        .school-logo2 {
            width: 142px;
            height: 142px;
            background: #fff;
            -moz-border-radius: 70px;
            -webkit-border-radius: 70px;
            border-radius: 70px;
            display: inline-block;
        }

            .school-logo2 img {
                margin: 19px 0px 0px 22px;
                display: block;
            }

        .document-info {
            color: white;
            font-size: 0.96rem;
            margin: 10px 22px 0px 0px;
            padding: 0px 2px 0px 20px;
            display: block;
            height: 153px;
            overflow-y: auto;
        }

        ::-webkit-scrollbar {
            width: 8px;
        }

        ::-webkit-scrollbar-track {
            background-color: #394e68;
            border-radius: 50px;
        }

        ::-webkit-scrollbar-thumb {
            background-color: #f1b767;
            border-radius: 50px;
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
                <div id="divResultCheck" class="card">
                    <div class="card-body">
                        <div class="row">
                            <br>
                            <br>
                            <br>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center" style="padding-top: 25px;">
                                <span style="margin-top: 28px; font-size: 2.1em; color: #000; font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132951") %></span>
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
                                <button id="btnResultCheck" class="btn btn-success btn-round btn-block" style="font-size: 1.5em;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132542") %>
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
                <div id="divResultPlate" class="card" style="display: none;">
                    <div class="card-body" style="padding: 0px;">
                        <div style="background-image: url('/images/RegisterExamResult background.jpg'); background-size: cover; height: 485px; border-radius: 7px; background-repeat: no-repeat; background-position: left center;">
                            <div class="row">
                                <a id="btnBack" class="btn btn-link" style="font-size: .55rem; position: absolute; padding: 12px 18px; margin-left: 11px;">
                                    <i class="material-icons" style="color: #fff; margin: -4px 0px 0px 0px;">reply</i>
                                </a>
                                <br>
                                <br>
                            </div>
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="row" style="margin-top: -3px;">
                                        <div class="col-md-12 text-center">
                                            <div class="school-logo2">
                                                <img src="<%=schoolLogo %>" width="98" class="picture-src" id="wizardPicturePreview" title="">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <br>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-center"><span style="color: #fff; font-size: 1.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132950") %></span></div>
                                    </div>
                                    <div class="row">
                                        <br>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-center"><span id="spnResult" style="color: #61f561; font-size: 3.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103028") %></span></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-center"><span id="spnDocumentInfo" class="document-info"></span></div>
                                    </div>
                                    <div class="row d-none">
                                        <br>
                                        <br>
                                        <br>
                                        <br>
                                    </div>
                                    <div class="row d-none">
                                        <div class="col-md-12 text-center"><span id="spnLabel1" style="color: #fff; font-size: .8rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132823") %></span></div>
                                    </div>
                                    <div class="row d-none">
                                        <div class="col-md-12 text-center">
                                            <a id="btnDownload" href="#" class="btn btn-success" style="font-size: .55rem;">
                                                <i class="material-icons" style="color: #fff; margin: -4px 0px 0px 0px;">save_alt</i> Download
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-6" style="padding-left: 22px;">
                                    <div class="row">
                                        <br>
                                        <br>
                                        <br>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12"><span id="spnName" style="font-size: 2.2rem;">ดช. นันทชาติ ทรายแก้ว</span></div>
                                    </div>
                                    <div class="row">
                                        <br>
                                    </div>
                                    <div class="row" style="margin-bottom: 13px;">
                                        <div class="col-md-4"><span style="font-size: 1.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>:</span></div>
                                        <div class="col-md-8"><span id="spnYear" style="font-size: 1.4rem;">2563</span></div>
                                    </div>
                                    <div class="row" style="margin-bottom: 13px;">
                                        <div class="col-md-4"><span style="font-size: 1.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103190") %>:</span></div>
                                        <div class="col-md-8"><span id="spnPlan" style="font-size: 1.4rem;">MLP</span></div>
                                    </div>
                                    <div class="row" style="margin-bottom: 13px;">
                                        <span id="spnCheckEmail" style="font-size: 1.8rem; display: block; margin: 67px 0px 0px -40px;">"<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132949") %>"</span>
                                    </div>
                                    <%--<div class="row" style="margin-bottom: 13px;">
                                        <div class="col-md-4"><span style="font-size: 1.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132819") %>:</span></div>
                                        <div class="col-md-8"><span id="spnMeetingDate" style="font-size: 1.4rem;">-</span></div>
                                    </div>
                                    <div class="row" style="margin-bottom: 13px;">
                                        <div class="col-md-4"><span style="font-size: 1.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %>:</span></div>
                                        <div class="col-md-8"><span id="spnMeetingTime" style="font-size: 1.4rem;">-</span></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4"><span style="font-size: 1.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102084") %>:</span></div>
                                        <div class="col-md-8"><span id="spnMeetingPlace" style="font-size: 1.4rem;">-</span></div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">
    <script>

        $(document).ready(function () {

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

            $("#btnResultCheck").bind({
                click: function () {

                    $.ajax({
                        async: false,
                        type: "POST",
                        url: 'RegisterQualifyResultCheck.aspx/ResultCheck',
                        data: JSON.stringify({ id: $('#iptIDCard').val() }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {

                            var r = JSON.parse(result.d);
                            if (r.success) {
                                switch (r.code) {
                                    case "201":
                                        $('#spnName').text(r.data.name);
                                        $('#spnYear').text(r.data.year);
                                        $('#spnPlan').text(r.data.plan);
                                        $('#spnDocumentInfo').hide();
                                        $('#spnCheckEmail').hide();

                                        $('#spnResult').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103028") %>');
                                        $('#spnResult').css('color', '#61f561');

                                        $('#divResultCheck').hide();
                                        $('#divResultPlate').fadeIn(1000);
                                        break;
                                    case "202":
                                        $('#spnName').text(r.data.name);
                                        $('#spnYear').text(r.data.year);
                                        $('#spnPlan').text(r.data.plan);
                                        $('#spnDocumentInfo').text(r.data.documentsInfo);
                                        $('#spnDocumentInfo').show();
                                        $('#spnCheckEmail').show();

                                        $('#spnResult').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103027") %>');
                                        $('#spnResult').css('color', '#fff');

                                        $('#divResultCheck').hide();
                                        $('#divResultPlate').fadeIn(1000);
                                        break;
                                    case "203":
                                        //Swal.fire({
                                        //    title: 'Warning!',
                                        //    text: 'Warning Message : ' + r.message,
                                        //    type: 'warning',
                                        //    confirmButtonClass: "btn btn-warning",
                                        //    buttonsStyling: false
                                        //});
                                        $('#spnName').text(r.data.name);
                                        $('#spnYear').text(r.data.year);
                                        $('#spnPlan').text(r.data.plan);
                                        $('#spnDocumentInfo').text(r.data.documentsInfo);
                                        $('#spnDocumentInfo').show();
                                        $('#spnCheckEmail').hide();

                                        $('#spnResult').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103026") %>');
                                        $('#spnResult').css('color', '#f50').css('font-size', '1.4rem');

                                        $('#divResultCheck').hide();
                                        $('#divResultPlate').fadeIn(1000);

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

            $("#btnBack").bind({
                click: function () {

                    $('#divResultPlate').hide();
                    $('#divResultCheck').fadeIn(1000);

                    return false;
                }
            });

        });
    </script>
</asp:Content>
