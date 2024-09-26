<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterOnline11.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnline11" %>

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
            color: #707070;
            font-weight: bold;
        }

        .card-wizard {
            min-height: inherit;
            box-shadow: none;
            opacity: 1;
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

            .form-control:invalid {
                background-image: linear-gradient(to top, #f44336 2px, rgba(244, 67, 54, 0) 2px), linear-gradient(to top, #d2d2d2 0px, rgba(210, 210, 210, 0) 0px);
            }

        label.error {
            color: red;
        }

        /* Modal */
        .modal {
            height: 100%;
            width: 100%;
            position: fixed;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            margin: auto;
            background: rgba(142,68,173,.2);
            z-index: 9999;
            color: #fff;
        }

        .spinner3 {
            float: left;
            position: absolute;
            margin: -5px 0 0 -25px;
            height: 28px;
            width: 28px;
            animation: rotate 0.8s infinite linear;
            border: 4px solid #26c6da;
            border-right-color: transparent;
            border-radius: 50%;
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
                            <h4 style="font-family: THSarabunNew; margin-bottom: 0px; padding: 3px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %></h4>
                            <p class="h6 text-center">(Health information)</p>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <br>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131178") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Weight)</p>
                                </div>
                                <input id="iptWeight" name="iptWeight" type="text" class="form-control" maxlength="3">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131179") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Height)</p>
                                </div>
                                <input id="iptHeight" name="iptHeight" type="text" class="form-control" maxlength="3">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-select">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101217") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Blood Type)</p>
                                </div>
                                <select id="sltBlood" name="sltBlood" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option value="A">A</option>
                                    <option value="B">B</option>
                                    <option value="AB">AB</option>
                                    <option value="O">O</option>
                                </select>
                            </div>
                        </div>
                        <div class="row col-required-field">
                            <div class="col-md-2 ml-auto">
                                <div class="div-bag">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101219") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Allergy Symptoms)</p>
                                </div>
                            </div>
                            <div class="col-md-4 mr-auto">
                                <div class="form-check form-check-inline">
                                    <label class="form-check-label label-bag" style="padding-right: 40px; color: #707070;">
                                        <input class="form-check-input" type="radio" name="iptAllergySymptoms" value="yes">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %><p class="h6">(Yes)</p>
                                        <span class="circle">
                                            <span class="check"></span>
                                        </span>
                                    </label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <label class="form-check-label label-bag" style="color: #707070;">
                                        <input class="form-check-input" type="radio" name="iptAllergySymptoms" value="no" checked>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %><p class="h6">(No)</p>
                                        <span class="circle">
                                            <span class="check"></span>
                                        </span>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101220") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Food Allergy)</p>
                                </div>
                                <input id="iptFoodAllergy" name="iptFoodAllergy" type="text" class="form-control allergy-symptoms" maxlength="250" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131225") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Drug(s) Allergy)</p>
                                </div>
                                <input id="iptBeAllergic" name="iptBeAllergic" type="text" class="form-control allergy-symptoms" maxlength="250" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101222") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Other Allergy)</p>
                                </div>
                                <input id="iptOtherAllergic" name="iptOtherAllergic" type="text" class="form-control allergy-symptoms" maxlength="250" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101223") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Chronic Disease)</p>
                                </div>
                                <input id="iptCongenitalDisease" name="iptCongenitalDisease" type="text" class="form-control" maxlength="250" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101224") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Serious Illness or Medical Condition)</p>
                                </div>
                                <input id="iptSeriousDisease" name="iptSeriousDisease" type="text" class="form-control" maxlength="250" />
                            </div>
                        </div>
                        <div class="row">
                            <br>
                            <br>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <button id="btnBack" class="btn btn-warning btn-round col-md-2" style="font-size: 1.2em;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %><p class="h6" style="margin-bottom: 0px;">(Back)</p>
                                </button>
                                <button id="btnSaveDraft" class="btn btn-info btn-round col-md-2" style="font-size: 1.2em;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601083") %><p class="h6" style="margin-bottom: 0px;">(Save draft)</p>
                                </button>
                                <button id="btnNext" class="btn btn-success btn-round col-md-2" style="font-size: 1.2em;">
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

        function LoadDataFromLocalStorage() {
            // Get data from localStorage
            if (ls.isBrowserSupport()) {
                // Code for localStorage
                preRegister = ls.getItem('preRegister');

                if (preRegister.Page11Saved) {
                    $('#iptWeight').val(preRegister.Weight);
                    $('#iptHeight').val(preRegister.Height);
                    $('#sltBlood').selectpicker('val', preRegister.Blood);
                    $('input:radio[name="iptAllergySymptoms"]').filter('[value="' + preRegister.AllergySymptoms + '"]').attr('checked', true);
                    //if (preRegister.AllergySymptoms == 'no') {
                    //    $("input.form-control").prop('disabled', true);
                    //}
                    //else {
                    //    $("input.form-control").prop('disabled', false);
                    //}
                    $('#iptFoodAllergy').val(preRegister.FoodAllergy);
                    $('#iptBeAllergic').val(preRegister.BeAllergic);
                    $('#iptOtherAllergic').val(preRegister.OtherAllergic);
                    $('#iptCongenitalDisease').val(preRegister.CongenitalDisease);
                    $('#iptSeriousDisease').val(preRegister.SeriousDisease);
                }

            } else {
                // No web storage Support.
            }
        }

        function SaveDataToLocalStorage(callbackFunction) {
            if ($("#form").valid()) {

                preRegister.Weight = $('#iptWeight').val();
                preRegister.Height = $('#iptHeight').val();
                preRegister.Blood = $('#sltBlood').val();
                preRegister.BloodText = $('#sltBlood').find(':selected').text();
                preRegister.AllergySymptoms = $('input[name=iptAllergySymptoms]:checked').val();
                preRegister.FoodAllergy = $('#iptFoodAllergy').val();
                preRegister.BeAllergic = $('#iptBeAllergic').val();
                preRegister.OtherAllergic = $('#iptOtherAllergic').val();
                preRegister.CongenitalDisease = $('#iptCongenitalDisease').val();
                preRegister.SeriousDisease = $('#iptSeriousDisease').val();

                preRegister.Page11Saved = true;

                // Save data to localStorage
                if (ls.isBrowserSupport()) {
                    // Code for localStorage
                    ls.setItem('preRegister', preRegister);
                } else {
                    // No web storage Support.
                }

                ez.activePageComplete(11);

                callbackFunction();

            }
        }

        //function addRules(rulesObj) {
        //    for (var item in rulesObj) {
        //        $('#' + item).rules('add', rulesObj[item]);
        //    }
        //}

        //function removeRules(rulesObj) {
        //    for (var item in rulesObj) {
        //        $('#' + item).rules('remove');
        //    }
        //}

        function AddRequiredRulesvalidation(obj) {
            $(obj).closest('div.col-required-field').find('.red-star').html('<sup>*</sup>');
            $(obj).rules('add', { required: true });
            //$(obj).rules('remove', 'required');
        }

        var preRegister = null;
        $(document).ready(function () {

            LoadDataFromLocalStorage();

            ez.activateBootstrapSelect();

            $("#form").validate({
                rules: {
                    iptWeight: {
                        required: false,
                        number: true
                    },
                    iptHeight: {
                        required: false,
                        number: true
                    },
                    sltBlood: { required: false },
                    iptAllergySymptoms: { required: false },
                    iptFoodAllergy: {
                        required: false
                        /*require_from_group: [1, ".allergy-symptoms"]*/
                    },
                    iptBeAllergic: {
                        required: false
                        /*require_from_group: [1, ".allergy-symptoms"]*/
                    },
                    iptOtherAllergic: {
                        required: false
                        /*require_from_group: [1, ".allergy-symptoms"]*/
                    },
                    iptCongenitalDisease: { required: false },
                    iptSeriousDisease: { required: false }
                },
                messages: {
                    iptWeight: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    iptHeight: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    sltBlood: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptAllergySymptoms: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFoodAllergy: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptBeAllergic: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptOtherAllergic: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptCongenitalDisease: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptSeriousDisease: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "sltBlood": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            //var allergy_symptoms_required = {
            //    iptFoodAllergy: {
            //        require_from_group: [1, ".allergy-symptoms"]
            //    },
            //    iptBeAllergic: {
            //        require_from_group: [1, ".allergy-symptoms"]
            //    },
            //    iptOtherAllergic: {
            //        require_from_group: [1, ".allergy-symptoms"]
            //    }
            //}

            //$('input[type=radio][name=iptAllergySymptoms]').change(function () {
            //    if (this.value == 'yes') {
            //        //addRules(allergy_symptoms_required);

            //        // Enable control
            //        $("#iptFoodAllergy").prop('disabled', false);
            //        $("#iptBeAllergic").prop('disabled', false);
            //        $("#iptOtherAllergic").prop('disabled', false);
            //        $("#iptCongenitalDisease").prop('disabled', false);
            //        $("#iptSeriousDisease").prop('disabled', false);
            //    }
            //    else {
            //        //removeRules(allergy_symptoms_required);

            //        // Disable control
            //        $("#iptFoodAllergy").prop('disabled', true);
            //        $("#iptBeAllergic").prop('disabled', true);
            //        $("#iptOtherAllergic").prop('disabled', true);
            //        $("#iptCongenitalDisease").prop('disabled', true);
            //        $("#iptSeriousDisease").prop('disabled', true);

            //        //$("#form").valid();
            //    }
            //});

            $("#btnBack").bind({
                click: function () {

                    window.location.href = "RegisterOnline10.aspx";

                    return false;
                }
            });

            $("#btnSaveDraft").bind({
                click: function () {

                    SaveDataToLocalStorage(function () {

                        ez.showNotification('top', 'right', 'success', 'done', '[Notification Message]<br/><br/>Save draft is complete.', 3000);

                    });

                    return false;
                }
            });

            $("#btnNext").bind({
                click: function () {

                    SaveDataToLocalStorage(function () {

                        window.location.href = "RegisterOnline12.aspx";

                    });

                    return false;
                }
            });

        });
    </script>
    <asp:Literal ID="ltrScriptRequiredField" runat="server"></asp:Literal>
</asp:Content>
