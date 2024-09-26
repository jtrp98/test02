<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterExamResultCheck.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterExamResultCheck" %>

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
                                <span style="margin-top: 28px; font-size: 2.1em; color: #000; font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132817") %></span>
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
                                        <div class="col-md-12 text-center"><span style="color: #fff; font-size: 1.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132822") %></span></div>
                                    </div>
                                    <div class="row">
                                        <br>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-center"><span id="spnResult" style="color: #61f561; font-size: 3.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103024") %></span></div>
                                    </div>
                                    <div class="row">
                                        <br>
                                        <br>
                                        <br>
                                        <br>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-center"><span id="spnLabel1" style="color: #fff; font-size: .8rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132823") %></span></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <a id="btnDownload" href="https://jabjaistorage.blob.core.windows.net/preregister/recruitment/208/3/1.jpg" class="btn btn-success" style="font-size: .55rem;">
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
                                        <div class="col-md-4"><span style="font-size: 1.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132819") %>:</span></div>
                                        <div class="col-md-8"><span id="spnMeetingDate" style="font-size: 1.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132821") %></span></div>
                                    </div>
                                    <div class="row" style="margin-bottom: 13px;">
                                        <div class="col-md-4"><span style="font-size: 1.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %>:</span></div>
                                        <div class="col-md-8"><span id="spnMeetingTime" style="font-size: 1.4rem;">9.30 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %></span></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4"><span style="font-size: 1.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102084") %>:</span></div>
                                        <div class="col-md-8"><span id="spnMeetingPlace" style="font-size: 1.4rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132820") %></span></div>
                                    </div>
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

        function DownloadFile(filePath) {
            var link = document.createElement('a');
            link.href = filePath;
            link.target = '_blank';
            link.download = filePath.substr(filePath.lastIndexOf('/') + 1);
            link.click();
        }

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
                        url: 'RegisterExamResultCheck.aspx/ResultCheck',
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
                                        $('#spnMeetingDate').text(r.data.meetingDate);
                                        $('#spnMeetingTime').text(r.data.meetingTime);
                                        $('#spnMeetingPlace').text(r.data.meetingPlace);
                                        $('#spnResult').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103024") %>');
                                        $('#spnResult').css('color', '#61f561');

                                        if (r.data.attach) {
                                            $('#btnDownload').attr("href", r.data.attach);

                                            $('#spnLabel1').show();
                                            $('#btnDownload').show();
                                        }
                                        else {
                                            $('#btnDownload').attr("href", "#");

                                            $('#spnLabel1').hide();
                                            $('#btnDownload').hide();
                                        }

                                        $('#divResultCheck').hide();
                                        $('#divResultPlate').fadeIn(1000);
                                        break;
                                    case "202":
                                        $('#spnName').text(r.data.name);
                                        $('#spnYear').text(r.data.year);
                                        $('#spnPlan').text(r.data.plan);
                                        $('#spnMeetingDate').text('-');
                                        $('#spnMeetingTime').text('-');
                                        $('#spnMeetingPlace').text('-');
                                        $('#spnResult').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103025") %>');
                                        $('#spnResult').css('color', '#fff');

                                        if (r.data.attach) {
                                            $('#btnDownload').attr("href", r.data.attach);

                                            $('#spnLabel1').show();
                                            $('#btnDownload').show();
                                        }
                                        else {
                                            $('#btnDownload').attr("href", "#");

                                            $('#spnLabel1').hide();
                                            $('#btnDownload').hide();
                                        }

                                        $('#divResultCheck').hide();
                                        $('#divResultPlate').fadeIn(1000);
                                        break;
                                    case "204":
                                        $('#spnName').text(r.data.name);
                                        $('#spnYear').text(r.data.year);
                                        $('#spnPlan').text(r.data.plan);
                                        $('#spnMeetingDate').text(r.data.meetingDate);
                                        $('#spnMeetingTime').text(r.data.meetingTime);
                                        $('#spnMeetingPlace').text(r.data.meetingPlace);
                                        $('#spnResult').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103023") %>');
                                        $('#spnResult').css('color', '#69b5ff');

                                        if (r.data.attach) {
                                            $('#btnDownload').attr("href", r.data.attach);

                                            $('#spnLabel1').show();
                                            $('#btnDownload').show();
                                        }
                                        else {
                                            $('#btnDownload').attr("href", "#");

                                            $('#spnLabel1').hide();
                                            $('#btnDownload').hide();
                                        }

                                        $('#divResultCheck').hide();
                                        $('#divResultPlate').fadeIn(1000);
                                        break;
                                    case "203":
                                        Swal.fire({
                                            title: 'Warning!',
                                            text: 'Warning Message : ' + r.message,
                                            type: 'warning',
                                            confirmButtonClass: "btn btn-warning",
                                            buttonsStyling: false
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
