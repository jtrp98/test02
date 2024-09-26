<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterOnline10.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnline10" %>

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
                            <h4 style="font-family: THSarabunNew; margin-bottom: 0px; padding: 3px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132857") %></h4>
                            <p class="h6 text-center">(Educational information)</p>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <br>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="form-check form-check-inline">
                                    <label class="form-check-label label-bag" style="padding-right: 40px; color: #707070;">
                                        <input class="form-check-input" type="radio" name="iptNoInstitution" value="no">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132858") %><p class="h6">(No Previous Education)</p>
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
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101172") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Former School)</p>
                                </div>
                                <input id="iptOldSchoolName" name="iptOldSchoolName" type="text" class="form-control" maxlength="250">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-select">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Province)</p>
                                </div>
                                <select id="sltOldSchoolProvince" name="sltOldSchoolProvince" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <asp:Literal ID="ltrProvince" runat="server" />
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-select">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(District)</p>
                                </div>
                                <select id="sltOldSchoolDistrict" name="sltOldSchoolDistrict" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-select">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Sub District)</p>
                                </div>
                                <select id="sltOldSchoolSubDistrict" name="sltOldSchoolSubDistrict" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-select">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Degree of Education)</p>
                                </div>
                                <select id="sltOldSchoolEducational" name="sltOldSchoolEducational" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option value="11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131188") %></option>
                                    <option value="12">อนุบาลศึกษา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01072") %> 1</option>
                                    <option value="13">อนุบาลศึกษา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01072") %> 2</option>
                                    <option value="14">อนุบาลศึกษา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01072") %> 3</option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131192") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131193") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131194") %></option>
                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131195") %></option>
                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131196") %></option>
                                    <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206406") %></option>
                                    <option value="18"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132154") %></option>
                                    <option value="19"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132155") %></option>
                                    <option value="20"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206407") %></option>
                                    <option value="21"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132156") %></option>
                                    <option value="22"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132157") %></option>
                                    <option value="23"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206408") %></option>
                                    <%--<option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131198") %></option>
                                    <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131199") %></option>--%>
                                    <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131200") %></option>
                                    <option value="15"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131201") %></option>
                                    <option value="16"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131202") %></option>
                                    <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131203") %></option>
                                    <option value="17"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131204") %></option>
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101174") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Grade Point Average Earned(GPA))</p>
                                </div>
                                <input id="iptGPA" name="iptGPA" type="text" class="form-control" maxlength="4">
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

                if (preRegister.Page10Saved) {
                    if (preRegister.NoInstitution == "no") {
                        $('input:radio[name="iptNoInstitution"]').filter('[value="' + preRegister.NoInstitution + '"]').attr('checked', true);

                        radioState = $('input:radio[name="iptNoInstitution"][value="' + preRegister.NoInstitution + '"]').get(0);
                        radioFlag = true;

                        // Disable control
                        $("input.form-control").prop('disabled', true);

                        $('#sltOldSchoolProvince').prop('disabled', true);
                        $('#sltOldSchoolProvince').selectpicker('refresh');

                        $('#sltOldSchoolDistrict').prop('disabled', true);
                        $('#sltOldSchoolDistrict').selectpicker('refresh');

                        $('#sltOldSchoolSubDistrict').prop('disabled', true);
                        $('#sltOldSchoolSubDistrict').selectpicker('refresh');

                        $('#sltOldSchoolEducational').prop('disabled', true);
                        $('#sltOldSchoolEducational').selectpicker('refresh');

                        $('.selectpicker').selectpicker('refresh');
                    }

                    $('#iptOldSchoolName').val(preRegister.OldSchoolName);

                    $('#sltOldSchoolProvince').selectpicker('val', preRegister.OldSchoolProvince);
                    LoadDistrict($("#sltOldSchoolProvince").val());

                    $('#sltOldSchoolDistrict').selectpicker('val', preRegister.OldSchoolDistrict);
                    LoadSubDistrict($("#sltOldSchoolDistrict").val());

                    $('#sltOldSchoolSubDistrict').selectpicker('val', preRegister.OldSchoolSubDistrict);

                    $('#sltOldSchoolEducational').selectpicker('val', preRegister.OldSchoolEducational);
                    $('#iptGPA').val(preRegister.GPA);
                }

            } else {
                // No web storage Support.
            }
        }

        function SaveDataToLocalStorage(callbackFunction) {
            if ($("#form").valid()) {

                preRegister.NoInstitution = $('input[name=iptNoInstitution]:checked').val();
                preRegister.OldSchoolName = $('#iptOldSchoolName').val();
                preRegister.OldSchoolProvince = $('#sltOldSchoolProvince').val();
                preRegister.OldSchoolProvinceText = $('#sltOldSchoolProvince').find(':selected').text();
                preRegister.OldSchoolDistrict = $('#sltOldSchoolDistrict').val();
                preRegister.OldSchoolDistrictText = $('#sltOldSchoolDistrict').find(':selected').text();
                preRegister.OldSchoolSubDistrict = $('#sltOldSchoolSubDistrict').val();
                preRegister.OldSchoolSubDistrictText = $('#sltOldSchoolSubDistrict').find(':selected').text();
                preRegister.OldSchoolEducational = $('#sltOldSchoolEducational').val();
                preRegister.OldSchoolEducationalText = $('#sltOldSchoolEducational').find(':selected').text();
                preRegister.GPA = $('#iptGPA').val();

                preRegister.Page10Saved = true;

                // Save data to localStorage
                if (ls.isBrowserSupport()) {
                    // Code for localStorage
                    ls.setItem('preRegister', preRegister);
                } else {
                    // No web storage Support.
                }

                ez.activePageComplete(10);

                callbackFunction();

            }
        }

        function LoadDistrict(provinceID) {
            if (provinceID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline10.aspx/LoadDistrict",
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

            $('#sltOldSchoolDistrict').empty();

            var options = '';
            $(plans).each(function () {

                options += '<option value="' + this.AMPHUR_ID + '">' + this.AMPHUR_NAME + '</option>';

            });

            $('#sltOldSchoolDistrict').html(options);

            //$('#sltOldSchoolDistrict').prop('disabled', radioFlag);
            $('#sltOldSchoolDistrict').selectpicker('refresh');
        }

        function LoadSubDistrict(districtID) {
            if (districtID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline10.aspx/LoadSubDistrict",
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

            $('#sltOldSchoolSubDistrict').empty();

            var options = '';
            $(plans).each(function () {

                options += '<option value="' + this.DISTRICT_ID + '">' + this.DISTRICT_NAME + '</option>';

            });

            $('#sltOldSchoolSubDistrict').html(options);

            //$('#sltOldSchoolSubDistrict').prop('disabled', radioFlag);
            $('#sltOldSchoolSubDistrict').selectpicker('refresh');
        }

        function AddRequiredRulesvalidation(obj) {
            $(obj).closest('div.col-required-field').find('.red-star').html('<sup>*</sup>');
            $(obj).rules('add', {
                required: function (element) {
                    return $("input[name='iptNoInstitution']:checked").val() != 'no';
                }
            });
            //$(obj).rules('remove', 'required');
        }

        var radioFlag = false;
        var radioState;

        var preRegister = null;
        $(document).ready(function () {

            LoadDataFromLocalStorage();

            ez.activateBootstrapSelect();

            $("#form").validate({
                rules: {
                    iptOldSchoolName: {
                        required: false
                    },
                    sltOldSchoolProvince: {
                        required: false
                    },
                    sltOldSchoolDistrict: {
                        required: false
                    },
                    sltOldSchoolSubDistrict: {
                        required: false
                    },
                    sltOldSchoolEducational: {
                        required: false
                    },
                    iptGPA: {
                        required: false,
                        number: true
                    }
                },
                messages: {
                    iptOldSchoolName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltOldSchoolProvince: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltOldSchoolDistrict: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltOldSchoolSubDistrict: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltOldSchoolEducational: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptGPA: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    }
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "sltOldSchoolProvince":
                        case "sltOldSchoolDistrict":
                        case "sltOldSchoolSubDistrict":
                        case "sltOldSchoolEducational": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            $("input[name='iptNoInstitution']:radio").on('click', function (e) {
                if (radioState === this) {
                    // unchecked
                    this.checked = false;
                    radioState = null;

                    radioFlag = false;

                    // Enable control
                    $("input.form-control").prop('disabled', false);

                    $('#sltOldSchoolProvince').prop('disabled', false);
                    $('#sltOldSchoolProvince').selectpicker('refresh');

                    $('#sltOldSchoolDistrict').prop('disabled', false);
                    $('#sltOldSchoolDistrict').selectpicker('refresh');

                    $('#sltOldSchoolSubDistrict').prop('disabled', false);
                    $('#sltOldSchoolSubDistrict').selectpicker('refresh');

                    $('#sltOldSchoolEducational').prop('disabled', false);
                    $('#sltOldSchoolEducational').selectpicker('refresh');

                } else {
                    // checked
                    radioState = this;

                    radioFlag = true;

                    // Disable control
                    $("input.form-control").prop('disabled', true);

                    $('#sltOldSchoolProvince').prop('disabled', true);
                    $('#sltOldSchoolProvince').selectpicker('refresh');

                    $('#sltOldSchoolDistrict').prop('disabled', true);
                    $('#sltOldSchoolDistrict').selectpicker('refresh');

                    $('#sltOldSchoolSubDistrict').prop('disabled', true);
                    $('#sltOldSchoolSubDistrict').selectpicker('refresh');

                    $('#sltOldSchoolEducational').prop('disabled', true);
                    $('#sltOldSchoolEducational').selectpicker('refresh');

                }
                //console.log(radioState === this);
            });

            $("#btnBack").bind({
                click: function () {

                    window.location.href = "RegisterOnline09.aspx";

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

                        window.location.href = "RegisterOnline11.aspx";

                    });

                    return false;
                }
            });

            $("#sltOldSchoolProvince").change(function () {

                LoadDistrict($("#sltOldSchoolProvince").val());

                if (!radioFlag) {
                    $('#sltOldSchoolSubDistrict').empty();
                    //$('#sltOldSchoolSubDistrict').prop('disabled', true);
                    $('#sltOldSchoolSubDistrict').selectpicker('refresh');
                }
            });

            $("#sltOldSchoolDistrict").change(function () {

                LoadSubDistrict($("#sltOldSchoolDistrict").val());

            });

            // Init disable control
            //if ($.isEmpty($('#sltOldSchoolDistrict').val())) {
            //    $('#sltOldSchoolDistrict').prop('disabled', true);
            //    $('#sltOldSchoolDistrict').selectpicker('refresh');
            //}

            //if ($.isEmpty($('#sltOldSchoolSubDistrict').val())) {
            //    $('#sltOldSchoolSubDistrict').prop('disabled', true);
            //    $('#sltOldSchoolSubDistrict').selectpicker('refresh');
            //}

        });
    </script>
    <asp:Literal ID="ltrScriptRequiredField" runat="server"></asp:Literal>
</asp:Content>
