<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterOnline04.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnline04" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStyle" runat="server">
    <style>
        .card .card-body .col-form-label, .card .card-body .label-on-right {
            padding: 12px 5px 0 0;
        }

        .card .card-body .form-group {
            margin: 0px 0 0;
        }

        .col-form-label span {
            vertical-align: -webkit-baseline-middle;
            font-size: 1em;
            color: #000;
            font-weight: bold;
        }

        .card-wizard {
            min-height: inherit;
            box-shadow: none;
            opacity: 1;
        }

            /* Profile Picture */
            .card-wizard .picture {
                width: 163px;
                height: 163px;
            }

            .card-wizard .picture-src {
                width: 100%;
                height: 100%;
            }

        /* Container Bag */
        .div-bag {
            width: fit-content;
            /*float: right;*/
            text-align: left;
        }

            .div-bag.div-bag-select {
                margin-bottom: -25px;
                color: #707070;
            }

            .div-bag.div-bag-select2 {
                margin-bottom: -13px;
                color: #707070;
            }

            .div-bag.div-bag-input {
                margin-bottom: -10px;
                color: #707070;
            }

        .label-bag {
            color: #000;
            font-weight: bold;
        }

        /* Bootstrap-select Style */
        .bootstrap-select > .dropdown-toggle.bs-placeholder.btn-primary {
            color: #fff;
            font-size: 1em;
        }

        .form-control:invalid {
            background-image: linear-gradient(to top, #f44336 2px, rgba(244, 67, 54, 0) 2px), linear-gradient(to top, #d2d2d2 0px, rgba(210, 210, 210, 0) 0px);
        }

        .dropdown-menu .dropdown-item {
            font-size: 16px;
        }

        .filter-option-inner-inner {
            font-size: 16px;
        }

        .bootstrap-select .dropdown-toggle .filter-option-inner-inner {
            overflow: initial;
        }

        .bootstrap-select > .dropdown-toggle {
            padding-right: 7px;
        }

        /* Radio Style */
        .form-check .form-check-label .circle {
            border: 1px solid rgba(0, 0, 0, .54);
            height: 17px;
            width: 17px;
            border-radius: 100%;
            top: 1px;
        }

            .form-check .form-check-label .circle .check {
                height: 17px;
                width: 17px;
                border-radius: 100%;
                background-color: #9c27b0;
                -webkit-transform: scale3d(0, 0, 0);
                -moz-transform: scale3d(0, 0, 0);
                -o-transform: scale3d(0, 0, 0);
                -ms-transform: scale3d(0, 0, 0);
                transform: scale3d(0, 0, 0);
            }

        /* Input Style */
        .form-control {
            font-size: 16px;
        }

        @media (min-width: 320px) and (max-width: 767px) {

            .col-form-label {
                text-align: left !important;
                margin-left: 10px;
            }

            .label-on-right {
                text-align: right !important;
            }

            .div-bag {
                width: fit-content;
                /*float: left;*/
                text-align: left;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <form id="form">
                <div class="card">
                    <div class="card-header card-header-rose card-header-icon">
                        <div class="card-icon text-center" style="border-radius: 12px; margin-left: 30px; margin-top: -30px;">
                            <h4 style="font-family: THSarabunNew; margin-bottom: 0px; padding: 3px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></h4>
                            <p class="h6 text-center">(Student information)</p>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <br>
                        </div>
                        <div class="row col-required-field">
                            <div class="col-md-5 ml-auto mr-auto card-wizard" data-color="rose">
                                <div class="picture-container">
                                    <div class="picture">
                                        <img src="assets/img/default-avatar.png" class="picture-src" id="profilePicturePreview" title="" />
                                        <input type="file" id="profilePicture" name="profilePicture">
                                    </div>
                                    <h6 class="description" style="margin-top: 15px; color: #707070;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132841") %><br>
                                        (Upload photo)</h6>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <br>
                            <br>
                        </div>
                        <div class="row col-required-field">
                            <div class="col-md-2 ml-auto">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Student Type)</p>
                                </div>
                            </div>
                            <div class="col-md-4 mr-auto">
                                <div class="form-check form-check-inline">
                                    <label class="form-check-label label-bag" style="padding-right: 40px; color: #707070;">
                                        <input class="form-check-input" type="radio" name="iptStudentCategory" value="1" checked>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101122") %><p class="h6">(Day - School)</p>
                                        <span class="circle">
                                            <span class="check"></span>
                                        </span>
                                    </label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <label class="form-check-label label-bag" style="color: #707070;">
                                        <input class="form-check-input" type="radio" name="iptStudentCategory" value="2">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101123") %><p class="h6">(Boarding)</p>
                                        <span class="circle">
                                            <span class="check"></span>
                                        </span>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-select">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Prefixes)</p>
                                </div>
                                <select id="sltStudentTitle" name="sltStudentTitle" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <asp:Literal ID="ltrStudentTitle" runat="server" />
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132842") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Name)</p>
                                </div>
                                <input id="iptStudentName" name="iptStudentName" type="text" class="form-control" maxlength="256">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %>) <span class="red-star"></span>: </span></label>
                                    <p class="h6">(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Last Name)</p>
                                </div>
                                <input id="iptStudentLastname" name="iptStudentLastname" type="text" class="form-control" maxlength="256">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132843") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(English Name)</p>
                                </div>
                                <input id="iptStudentNameEn" name="iptStudentNameEn" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101070") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(English Last Name)</p>
                                </div>
                                <input id="iptStudentLastnameEn" name="iptStudentLastnameEn" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Nickname)</p>
                                </div>
                                <input id="iptStudentNickName" name="iptStudentNickName" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %>(Eng) <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Eng Nickname)</p>
                                </div>
                                <input id="iptStudentNickNameEn" name="iptStudentNickNameEn" type="text" class="form-control" maxlength="100">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Gender)</p>
                                </div>
                                <select id="sltStudentSex" name="sltStudentSex" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></option>
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Date of Birth)</p>
                                </div>
                                <input id="iptStudentBirthday" name="iptStudentBirthday" type="text" class="form-control datetimepicker" maxlength="10">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Citizen ID No.)</p>
                                </div>
                                <input id="iptStudentIDCard" name="iptStudentIDCard" type="text" class="form-control" maxlength="13" disabled>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Race)</p>
                                </div>
                                <%--<input id="iptStudentRace" name="iptStudentRace" type="text" class="form-control" maxlength="50">--%>
                                <select id="sltStudentRace" name="sltStudentRace" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %></option>
                                    <asp:Literal ID="ltrStudentRace" runat="server" />
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Nationality)</p>
                                </div>
                                <%--<input id="iptStudentNation" name="iptStudentNation" type="text" class="form-control" maxlength="50">--%>
                                <select id="sltStudentNation" name="sltStudentNation" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %></option>
                                    <asp:Literal ID="ltrStudentNation" runat="server" />
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Religion)</p>
                                </div>
                                <%--<input id="iptStudentReligion" name="iptStudentReligion" type="text" class="form-control" maxlength="50">--%>
                                <select id="sltStudentReligion" name="sltStudentReligion" class="selectpicker" data-live-search="false" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %></option>
                                    <asp:Literal ID="ltrStudentReligion" runat="server" />
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132844") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Siblings)</p>
                                </div>
                                <input id="iptStudentSonTotal" name="iptStudentSonTotal" type="text" class="form-control" maxlength="2">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101113") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Your Order Among Sibling)</p>
                                </div>
                                <input id="iptStudentSonNumber" name="iptStudentSonNumber" type="text" class="form-control" maxlength="2">
                            </div>
                        </div>
                        <div class="row">
                            <br>
                            <br>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <button id="btnBack" class="btn btn-warning btn-round col-md-2" style="font-size: 1.2em; height: 47px; padding-top: 7px;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %><p class="h6" style="margin-bottom: 0px;">(Back)</p>
                                </button>
                                <button id="btnSaveDraft" class="btn btn-info btn-round col-md-2" style="font-size: 1.2em; height: 47px; padding-top: 7px;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601083") %><p class="h6" style="margin-bottom: 0px;">(Save draft)</p>
                                </button>
                                <button id="btnNext" class="btn btn-success btn-round col-md-2" style="font-size: 1.2em; height: 47px; padding-top: 7px;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %><p class="h6" style="margin-bottom: 0px;">(Next)</p>
                                </button>
                            </div>
                        </div>
                        <div class="row">
                            <br>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">

    <!-- Plugin for Select, full documentation here: http://silviomoreto.github.io/bootstrap-select -->
    <script src="assets/js/plugins/bootstrap-selectpicker.js"></script>

    <!-- Plugin for the momentJs  -->
    <script src="assets/js/plugins/moment-with-locales.js"></script>
    <!-- Plugin for the DateTimePicker, full documentation here: https://eonasdan.github.io/bootstrap-datetimepicker/ -->
    <script src="assets/js/plugins/bootstrap-datetimepicker.th.min.js"></script>

    <script>

        // Upload function
        function readURL(input) {
            if (input.files && input.files[0]) {
                var file = input.files[0];

                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#profilePicturePreview').attr('src', e.target.result);

                    // Keep ProfilePicture to localStorage
                    preRegister.ProfilePicture = file.name; // e.target.result
                    preRegister.ProfilePictureName = file.name;
                    preRegister.ProfilePictureContentType = file.type;

                    var byteData = e.target.result;

                    UploadFileBase64ToSession({ id: 1, fileName: file.name, contentType: file.type, docId: 0, typeId: 1, vfiId: 0, byteData: byteData });
                }

                reader.readAsDataURL(input.files[0]);
            }
        }

        function LoadDataFromLocalStorage() {
            // Get data from localStorage
            if (ls.isBrowserSupport()) {
                // Code for localStorage
                preRegister = ls.getItem('preRegister');

                if (preRegister.Page04Saved) {
                    if (!$.isEmpty(preRegister.ProfilePicture)) {

                        RetreivedFileBase64FromSession(0, 1);
                    }
                    $('input:radio[name="iptStudentCategory"]').filter('[value="' + preRegister.StudentCategory + '"]').attr('checked', true);
                    $('#sltStudentTitle').selectpicker('val', preRegister.StudentTitle);
                    $('#iptStudentName').val(preRegister.StudentName);
                    $('#iptStudentLastname').val(preRegister.StudentLastname);
                    $('#iptStudentNameEn').val(preRegister.StudentNameEn);
                    $('#iptStudentLastnameEn').val(preRegister.StudentLastnameEn);
                    $('#iptStudentNickName').val(preRegister.StudentNickName);
                    $('#iptStudentNickNameEn').val(preRegister.StudentNickNameEn);
                    $('#sltStudentSex').selectpicker('val', preRegister.StudentSex);
                    $('#iptStudentBirthday').val(preRegister.StudentBirthday);
                    $('#iptStudentIDCard').val(preRegister.StudentIDCard);
                    //$('#iptStudentRace').val(preRegister.StudentRace);
                    $('#sltStudentRace').selectpicker('val', preRegister.StudentRace);
                    //$('#iptStudentNation').val(preRegister.StudentNation);
                    $('#sltStudentNation').selectpicker('val', preRegister.StudentNation);
                    //$('#iptStudentReligion').val(preRegister.StudentReligion);
                    $('#sltStudentReligion').selectpicker('val', preRegister.StudentReligion);
                    $('#iptStudentSonTotal').val(preRegister.StudentSonTotal);
                    $('#iptStudentSonNumber').val(preRegister.StudentSonNumber);
                }
                else {
                    // Init
                    $('#iptStudentIDCard').val(preRegister.IDCard);

                    // Clear picture
                    preRegister.ProfilePicture = '';
                }
            } else {
                // No web storage Support.
            }
        }

        function SaveDataToLocalStorage(callbackFunction) {
            if ($("#form").valid()) {

                preRegister.StudentCategory = $('input[name=iptStudentCategory]:checked').val();
                preRegister.StudentTitle = $('#sltStudentTitle').val();
                preRegister.StudentTitleText = $('#sltStudentTitle').find(':selected').text();
                preRegister.StudentName = $('#iptStudentName').val();
                preRegister.StudentLastname = $('#iptStudentLastname').val();
                preRegister.StudentNameEn = $('#iptStudentNameEn').val();
                preRegister.StudentLastnameEn = $('#iptStudentLastnameEn').val();
                preRegister.StudentNickName = $('#iptStudentNickName').val();
                preRegister.StudentNickNameEn = $('#iptStudentNickNameEn').val();
                preRegister.StudentSex = $('#sltStudentSex').val();
                preRegister.StudentSexText = $('#sltStudentSex').find(':selected').text();
                preRegister.StudentBirthday = $('#iptStudentBirthday').val();
                preRegister.StudentIDCard = $('#iptStudentIDCard').val();
                //preRegister.IDCard = $('#iptStudentIDCard').val();
                //preRegister.StudentRace = $('#iptStudentRace').val();
                preRegister.StudentRace = $('#sltStudentRace').val();
                preRegister.StudentRaceText = $('#sltStudentRace').find(':selected').text();
                //preRegister.StudentNation = $('#iptStudentNation').val();
                preRegister.StudentNation = $('#sltStudentNation').val();
                preRegister.StudentNationText = $('#sltStudentNation').find(':selected').text();
                //preRegister.StudentReligion = $('#iptStudentReligion').val();
                preRegister.StudentReligion = $('#sltStudentReligion').val();
                preRegister.StudentReligionText = $('#sltStudentReligion').find(':selected').text();
                preRegister.StudentSonTotal = $('#iptStudentSonTotal').val();
                preRegister.StudentSonNumber = $('#iptStudentSonNumber').val();

                preRegister.Page04Saved = true;

                // Save data to localStorage
                if (ls.isBrowserSupport()) {
                    // Code for localStorage
                    ls.setItem('preRegister', preRegister);
                } else {
                    // No web storage Support.
                }

                ez.activePageComplete(4);

                callbackFunction();

            }
        }

        function AddRequiredRulesvalidation(obj) {
            $(obj).closest('div.col-required-field').find('.red-star').html('<sup>*</sup>');
            $(obj).rules('add', { required: true });
            //$(obj).rules('remove', 'required');
        }

        function OnError(xhr, errorType, exception) {
            var responseText;
            try {
                responseText = jQuery.parseJSON(xhr.responseText);
                var errorMessage = "[" + errorType + ", " + exception + "] Exception:" + responseText.ExceptionType + ", StackTrace:" + responseText.StackTrace + ", Message:" + responseText.Message;

                Swal.fire({
                    title: 'Error!',
                    text: 'Error Message - ' + errorMessage,
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
            } catch (e) {
                responseText = xhr.responseText;
                Swal.fire({
                    title: 'Error!',
                    text: 'Error Message - c' + responseText,
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
            }
        }

        function UploadFileBase64ToSession(documentFile) {
            $.ajax({
                async: false,
                type: "POST",
                url: 'RegisterOnline04.aspx/UploadFileBase64ToSession',
                data: JSON.stringify({ documentFile: documentFile }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    var r = JSON.parse(result.d);
                    if (r.success) {

                        // Upload file base64 to session
                        // Find index of specific object using findIndex method.    
                        var objIndex = preRegister.Files.findIndex((obj => obj.docId == r.documentFile.docId));
                        if (objIndex == -1) {
                            preRegister.Files.push({ id: r.documentFile.id, fileName: r.documentFile.fileName, contentType: r.documentFile.contentType, docId: r.documentFile.docId, typeId: r.documentFile.typeId, vfiId: r.documentFile.vfiId, byteData: '' });
                        }
                        else {
                            preRegister.Files[objIndex].fileName = r.documentFile.fileName;
                            preRegister.Files[objIndex].contentType = r.documentFile.contentType;
                            preRegister.Files[objIndex].byteData = '';
                        }
                    }
                    else {
                        Swal.fire({
                            title: 'Warning!',
                            text: 'Warning Message (File Upload) - ' + r.message,
                            type: 'warning',
                            confirmButtonClass: "btn btn-warning",
                            buttonsStyling: false
                        });
                    }
                },
                error: OnError
            });
        }

        function RetreivedFileBase64FromSession(docId, typeId) {
            $.ajax({
                async: false,
                type: "POST",
                url: 'RegisterOnline04.aspx/RetreivedFileBase64FromSession',
                data: JSON.stringify({ docId: docId, typeId: typeId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    var r = JSON.parse(result.d);
                    if (r.success) {

                        // Retreived file base64 from session
                        if (r.documentFile != null) {
                            var base64 = r.documentFile.byteData;
                            $('#profilePicturePreview').attr('src', base64);

                            //Find index of specific object using findIndex method.    
                            var objIndex = preRegister.Files.findIndex((obj => obj.docId == r.documentFile.docId));
                            if (objIndex == -1) {
                                preRegister.Files.push({ id: r.documentFile.id, fileName: r.documentFile.fileName, contentType: r.documentFile.contentType, docId: r.documentFile.docId, typeId: r.documentFile.typeId, vfiId: r.documentFile.vfiId, byteData: '' });
                            }
                            else {
                                preRegister.Files[objIndex].byteData = '';
                            }
                        }
                    }
                    else {
                        Swal.fire({
                            title: 'Warning!',
                            text: 'Warning Message (File Upload) - ' + r.message,
                            type: 'warning',
                            confirmButtonClass: "btn btn-warning",
                            buttonsStyling: false
                        });
                    }
                },
                error: OnError
            });
        }

        var preRegister = null;
        $(document).ready(function () {

            ez.activateBootstrapSelect();

            ez.initFormExtendedDatetimepickersDisableFutureDate('#iptStudentBirthday');

            $.validator.addMethod("thaiDate",
                function (value, element) {
                    return value == '' || value.match(/^(0?[1-9]|[12][0-9]|3[0-1])[/., -](0?[1-9]|1[<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305069") %>])[/., -](24|25)?\d{2}$/);
                },
                "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132811") %>"
            );

            $.validator.addMethod("disableFutureDate",
                function (value, element) {
                    var now = moment().add(-1, 'days');
                    var dmy = value.split('/'); // '28/02/2564'
                    var mDate = new Date(parseInt(dmy[2]) - 543, parseInt(dmy[1]) - 1, parseInt(dmy[0]));
                    return this.optional(element) || mDate < now;
                },
                "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132845") %>"
            );

            $.validator.addMethod('fileSize', function (value, element, arg) {
                if (element.files.length > 0) {
                    if (element.files[0].size <= arg) {
                        return true;
                    } else {
                        return false;
                    }
                } else {
                    return true;
                }

            });

            $("#form").validate({
                rules: {
                    profilePicture: {
                        required: false,
                        extension: "jpg|jpeg|png",
                        fileSize: 2000000
                    },
                    iptStudentCategory: { required: false },
                    sltStudentTitle: { required: false },
                    iptStudentName: { required: false },
                    iptStudentLastname: { required: false },
                    iptStudentNameEn: { required: false },
                    iptStudentLastnameEn: { required: false },
                    iptStudentNickName: { required: false },
                    iptStudentNickNameEn: { required: false },
                    sltStudentSex: { required: false },
                    iptStudentBirthday: {
                        required: false,
                        thaiDate: true,
                        disableFutureDate: true
                    },
                    iptStudentIDCard: {
                        required: false,
                        minlength: 7,
                        maxlength: 13
                    },
                    sltStudentRace: { required: false },
                    sltStudentNation: { required: false },
                    sltStudentReligion: { required: false },
                    iptStudentSonTotal: {
                        required: false,
                        number: true
                    },
                    iptStudentSonNumber: {
                        required: false,
                        number: true
                    }
                },
                messages: {
                    profilePicture: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        extension: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132846") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132772") %>"
                    },
                    iptStudentCategory: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltStudentTitle: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentLastname: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentNameEn: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentLastnameEn: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentNickName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentNickNameEn: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltStudentSex: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentBirthday: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                    },
                    iptStudentIDCard: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        minlength: jQuery.validator.format("Please, at least {0} characters are necessary."),
                        maxlength: jQuery.validator.format("Please, at most {0} characters are necessary.")
                    },
                    sltStudentRace: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltStudentNation: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltStudentReligion: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentSonTotal: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    iptStudentSonNumber: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    }
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "profilePicture":
                        case "sltStudentTitle":
                        case "sltStudentSex":
                        case "sltStudentNation":
                        case "sltStudentReligion":
                        case "sltStudentRace": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
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


            $('#profilePicture').change(function () {

                readURL(this);

            });

            $("#btnBack").bind({
                click: function () {

                    window.location.href = "RegisterOnline03.aspx";

                    return false;
                }
            });

            $("#btnSaveDraft").bind({
                click: function () {

                    SaveDataToLocalStorage(function () { });

                    return false;
                }
            });

            $("#btnNext").bind({
                click: function () {

                    SaveDataToLocalStorage(function () {
                        window.location.href = "RegisterOnline05.aspx";
                    });

                    return false;
                }
            });

            LoadDataFromLocalStorage();

            if ($.isEmpty(preRegister.StudentBirthday)) {
                $('#iptStudentBirthday').val('');
            }
        });
    </script>
    <asp:Literal ID="ltrScriptRequiredField" runat="server"></asp:Literal>
</asp:Content>
