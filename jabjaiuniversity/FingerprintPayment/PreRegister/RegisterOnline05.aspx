<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterOnline05.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnline05" %>

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

        /* Input Style */
        .form-control {
            font-size: 16px;
        }

            .form-control:invalid {
                background-image: linear-gradient(to top, #f44336 2px, rgba(244, 67, 54, 0) 2px), linear-gradient(to top, #d2d2d2 0px, rgba(210, 210, 210, 0) 0px);
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
                            <h4 style="font-family: THSarabunNew; margin-bottom: 0px; padding: 3px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %></h4>
                            <p class="h6 text-center">(Permanent Address)</p>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <br>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101130") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(House Code)</p>
                                </div>
                                <input id="iptHouseCode" name="iptHouseCode" type="text" class="form-control" maxlength="11">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Address No.)</p>
                                </div>
                                <input id="iptHouseHomeNumber" name="iptHouseHomeNumber" type="text" class="form-control" maxlength="20">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Soi)</p>
                                </div>
                                <input id="iptHouseAlley" name="iptHouseAlley" type="text" class="form-control" maxlength="100">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Moo)</p>
                                </div>
                                <input id="iptHouseVillageNo" name="iptHouseVillageNo" type="text" class="form-control" maxlength="100">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Street)</p>
                                </div>
                                <input id="iptHouseRoad" name="iptHouseRoad" type="text" class="form-control" maxlength="100">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Province)</p>
                                </div>
                                <select id="sltHouseProvince" name="sltHouseProvince" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <asp:Literal ID="ltrProvince" runat="server" />
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(District)</p>
                                </div>
                                <select id="sltHouseDistrict" name="sltHouseDistrict" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Sub District)</p>
                                </div>
                                <select id="sltHouseSubDistrict" name="sltHouseSubDistrict" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Post Code)</p>
                                </div>
                                <input id="iptHousePostalCode" name="iptHousePostalCode" type="text" class="form-control" maxlength="20">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Mobile Phone)</p>
                                </div>
                                <input id="iptHousePhone" name="iptHousePhone" type="text" class="form-control" maxlength="20">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(E-Mail)</p>
                                </div>
                                <input id="iptHouseEmail" name="iptHouseEmail" type="text" class="form-control" maxlength="256">
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

                if (preRegister.Page05Saved) {
                    $('#iptHouseCode').val(preRegister.HouseCode);
                    $('#iptHouseHomeNumber').val(preRegister.HouseHomeNumber);
                    $('#iptHouseAlley').val(preRegister.HouseAlley);
                    $('#iptHouseVillageNo').val(preRegister.HouseVillageNo);
                    $('#iptHouseRoad').val(preRegister.HouseRoad);

                    $('#sltHouseProvince').selectpicker('val', preRegister.HouseProvince);
                    LoadDistrict($("#sltHouseProvince").val());

                    $('#sltHouseDistrict').selectpicker('val', preRegister.HouseDistrict);
                    LoadSubDistrict($("#sltHouseDistrict").val());

                    $('#sltHouseSubDistrict').selectpicker('val', preRegister.HouseSubDistrict);


                    $('#iptHousePostalCode').val(preRegister.HousePostalCode);
                    $('#iptHousePhone').val(preRegister.HousePhone);
                    $('#iptHouseEmail').val(preRegister.HouseEmail);
                }

            } else {
                // No web storage Support.
            }
        }

        function SaveDataToLocalStorage(callbackFunction) {
            if ($("#form").valid()) {

                preRegister.HouseCode = $('#iptHouseCode').val();
                preRegister.HouseHomeNumber = $('#iptHouseHomeNumber').val();
                preRegister.HouseAlley = $('#iptHouseAlley').val();
                preRegister.HouseVillageNo = $('#iptHouseVillageNo').val();
                preRegister.HouseRoad = $('#iptHouseRoad').val();
                preRegister.HouseProvince = $('#sltHouseProvince').val();
                preRegister.HouseProvinceText = $('#sltHouseProvince').find(':selected').text();
                preRegister.HouseDistrict = $('#sltHouseDistrict').val();
                preRegister.HouseDistrictText = $('#sltHouseDistrict').find(':selected').text();
                preRegister.HouseSubDistrict = $('#sltHouseSubDistrict').val();
                preRegister.HouseSubDistrictText = $('#sltHouseSubDistrict').find(':selected').text();
                preRegister.HousePostalCode = $('#iptHousePostalCode').val();
                preRegister.HousePhone = $('#iptHousePhone').val();
                preRegister.HouseEmail = $('#iptHouseEmail').val();

                preRegister.Page05Saved = true;

                // Save data to localStorage
                if (ls.isBrowserSupport()) {
                    // Code for localStorage
                    ls.setItem('preRegister', preRegister);
                } else {
                    // No web storage Support.
                }

                ez.activePageComplete(5);

                callbackFunction();

            }
        }

        function LoadDistrict(provinceID) {
            if (provinceID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline05.aspx/LoadDistrict",
                    data: '{provinceID: ' + provinceID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessLoadDistrict,
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }

        function OnSuccessLoadDistrict(response) {
            var plans = response.d;

            $('#sltHouseDistrict').empty();

            var options = '';
            $(plans).each(function () {

                options += '<option value="' + this.AMPHUR_ID + '">' + this.AMPHUR_NAME + '</option>';

            });

            $('#sltHouseDistrict').html(options);

            //$('#sltHouseDistrict').prop('disabled', false);
            $('#sltHouseDistrict').selectpicker('refresh');
        }

        function LoadSubDistrict(districtID) {
            if (districtID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline05.aspx/LoadSubDistrict",
                    data: '{districtID: ' + districtID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessLoadSubDistrict,
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }

        function OnSuccessLoadSubDistrict(response) {
            var plans = response.d;

            $('#sltHouseSubDistrict').empty();

            var options = '';
            $(plans).each(function () {

                options += '<option value="' + this.DISTRICT_ID + '">' + this.DISTRICT_NAME + '</option>';

            });

            $('#sltHouseSubDistrict').html(options);

            //$('#sltHouseSubDistrict').prop('disabled', false);
            $('#sltHouseSubDistrict').selectpicker('refresh');
        }

        function AddRequiredRulesvalidation(obj) {
            $(obj).closest('div.col-required-field').find('.red-star').html('<sup>*</sup>');
            $(obj).rules('add', { required: true });
            //$(obj).rules('remove', 'required');
        }

        var preRegister = null;
        $(document).ready(function () {

            LoadDataFromLocalStorage();

            ez.activateBootstrapSelect();

            $.validator.addMethod('houseCode', function (value, element, arg) {
                return (value.length == 0 || value.length == 11);
            });

            $("#form").validate({
                rules: {
                    iptHouseCode: {
                        required: false,
                        number: true,
                        houseCode: true
                    },
                    iptHouseHomeNumber: { required: false },
                    iptHouseAlley: { required: false },
                    iptHouseVillageNo: { required: false },
                    iptHouseRoad: { required: false },
                    sltHouseProvince: { required: false },
                    sltHouseDistrict: { required: false },
                    sltHouseSubDistrict: { required: false },
                    iptHousePostalCode: {
                        required: false,
                        number: true
                    },
                    iptHousePhone: {
                        required: false,
                        number: true,
                        minlength: 10,
                        maxlength: 10
                    },
                    iptHouseEmail: {
                        required: false,
                        email: true
                    }
                },
                messages: {
                    iptHouseCode: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>",
                        houseCode: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132847") %>",
                    },
                    iptHouseHomeNumber: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptHouseAlley: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptHouseVillageNo: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptHouseRoad: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltHouseProvince: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltHouseDistrict: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltHouseSubDistrict: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptHousePostalCode: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    iptHousePhone: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>",
                        minlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132848") %>",
                        maxlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132849") %>"
                    },
                    iptHouseEmail: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        email: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132850") %>"
                    }
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "sltHouseProvince":
                        case "sltHouseDistrict":
                        case "sltHouseSubDistrict": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            $("#btnBack").bind({
                click: function () {

                    window.location.href = "RegisterOnline04.aspx";

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

                        window.location.href = "RegisterOnline06.aspx";

                    });

                    return false;
                }
            });

            $("#sltHouseProvince").change(function () {

                LoadDistrict($("#sltHouseProvince").val());

                $('#sltHouseSubDistrict').empty();
                //$('#sltHouseSubDistrict').prop('disabled', true);
                $('#sltHouseSubDistrict').selectpicker('refresh');
            });

            $("#sltHouseDistrict").change(function () {

                LoadSubDistrict($("#sltHouseDistrict").val());

            });

            // Init disable control
            //if ($.isEmpty($('#sltHouseDistrict').val())) {
            //    $('#sltHouseDistrict').prop('disabled', true);
            //    $('#sltHouseDistrict').selectpicker('refresh');
            //}

            //if ($.isEmpty($('#sltHouseSubDistrict').val())) {
            //    $('#sltHouseSubDistrict').prop('disabled', true);
            //    $('#sltHouseSubDistrict').selectpicker('refresh');
            //}
        });
    </script>
    <asp:Literal ID="ltrScriptRequiredField" runat="server"></asp:Literal>
</asp:Content>
