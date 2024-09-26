<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterOnline06.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnline06" %>

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
                            <h4 style="font-family: THSarabunNew; margin-bottom: 0px; padding: 3px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101147") %></h4>
                            <p class="h6 text-center">(Current Address)</p>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <br>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto">
                                <div class="form-check form-check-inline">
                                    <label class="form-check-label label-bag" style="padding-right: 40px; color: #707070;">
                                        <input class="form-check-input" type="radio" name="iptSameHouseAddress" value="same">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %><p class="h6">(Same as House Registration Address)</p>
                                        <span class="circle">
                                            <span class="check"></span>
                                        </span>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Address No.)</p>
                                </div>
                                <input id="iptStudentHomeNumber" name="iptStudentHomeNumber" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Soi)</p>
                                </div>
                                <input id="iptStudentAlley" name="iptStudentAlley" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Moo)</p>
                                </div>
                                <input id="iptStudentVillageNo" name="iptStudentVillageNo" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Street)</p>
                                </div>
                                <input id="iptStudentRoad" name="iptStudentRoad" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Province)</p>
                                </div>
                                <select id="sltStudentProvince" name="sltStudentProvince" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <asp:Literal ID="ltrProvince" runat="server" />
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(District)</p>
                                </div>
                                <select id="sltStudentDistrict" name="sltStudentDistrict" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Sub District)</p>
                                </div>
                                <select id="sltStudentSubDistrict" name="sltStudentSubDistrict" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Post Code)</p>
                                </div>
                                <input id="iptStudentPostalCode" name="iptStudentPostalCode" type="text" class="form-control" maxlength="5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Mobile Phone)</p>
                                </div>
                                <input id="iptStudentHousePhone" name="iptStudentHousePhone" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(E-Mail)</p>
                                </div>
                                <input id="iptStudentHouseEmail" name="iptStudentHouseEmail" type="text" class="form-control" maxlength="100">
                            </div>
                        </div>
                        <!---->
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-select">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101149") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Stay with (Prefixes))</p>
                                </div>
                                <select id="sltStudentStayWithTitle" name="sltStudentStayWithTitle" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <asp:Literal ID="ltrStudentStayWithTitle" runat="server" />
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101151") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Stay with (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Name))</p>
                                </div>
                                <input id="iptStudentStayWithName" name="iptStudentStayWithName" type="text" class="form-control stay-with" maxlength="100">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101152") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Stay with (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Last Name))</p>
                                </div>
                                <input id="iptStudentStayWithLast" name="iptStudentStayWithLast" type="text" class="form-control stay-with" maxlength="100">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101153") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Emergency contacts / phone)</p>
                                </div>
                                <input id="iptStudentStayWithEmergencyCall" name="iptStudentStayWithEmergencyCall" type="text" class="form-control stay-with" maxlength="20">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101158") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(House style)</p>
                                </div>
                                <select id="sltStudentHomeType" name="sltStudentHomeType" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103075") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101159") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101160") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101161") %></option>
                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101162") %></option>
                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101163") %></option>
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101155") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Neighbor (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Name))</p>
                                </div>
                                <input id="iptStudentNeighborName" name="iptStudentNeighborName" type="text" class="form-control stay-with" maxlength="100">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101156") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Neighbor (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Last Name))</p>
                                </div>
                                <input id="iptStudentNeighborLastName" name="iptStudentNeighborLastName" type="text" class="form-control stay-with" maxlength="100">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132851") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Neighbor (Education level))</p>
                                </div>
                                <select id="sltStudentNeighborSubLevel" name="sltStudentNeighborSubLevel" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <asp:Literal ID="ltrStudentNeighborSubLevel" runat="server" />
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101157") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Neighbor (Mobile Phone))</p>
                                </div>
                                <input id="iptStudentNeighborPhone" name="iptStudentNeighborPhone" type="text" class="form-control stay-with" maxlength="30">
                            </div>
                        </div>
                        <!---->
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

                if (preRegister.Page06Saved) {
                    if (preRegister.SameHouseAddress == "same") {
                        $('input:radio[name="iptSameHouseAddress"]').filter('[value="' + preRegister.SameHouseAddress + '"]').attr('checked', true);

                        radioState = $('input:radio[name="iptSameHouseAddress"][value="' + preRegister.SameHouseAddress + '"]').get(0);
                        radioFlag = true;

                        // Disable control
                        $("input.form-control:not(.stay-with)").prop('disabled', true);

                        $('#sltStudentProvince').prop('disabled', true);
                        $('#sltStudentProvince').selectpicker('refresh');

                        $('#sltStudentDistrict').prop('disabled', true);
                        $('#sltStudentDistrict').selectpicker('refresh');

                        $('#sltStudentSubDistrict').prop('disabled', true);
                        $('#sltStudentSubDistrict').selectpicker('refresh');

                        $('.selectpicker').selectpicker('refresh');
                    }

                    $('#iptStudentHomeNumber').val(preRegister.StudentHomeNumber);
                    $('#iptStudentAlley').val(preRegister.StudentAlley);
                    $('#iptStudentVillageNo').val(preRegister.StudentVillageNo);
                    $('#iptStudentRoad').val(preRegister.StudentRoad);
                    $('#sltStudentProvince').selectpicker('val', preRegister.StudentProvince);
                    LoadDistrict($("#sltStudentProvince").val());
                    oldProvince = preRegister.StudentProvince;

                    $('#sltStudentDistrict').selectpicker('val', preRegister.StudentDistrict);
                    LoadSubDistrict($("#sltStudentDistrict").val());
                    oldDistrict = preRegister.StudentDistrict;

                    $('#sltStudentSubDistrict').selectpicker('val', preRegister.StudentSubDistrict);
                    oldSubDistrict = preRegister.StudentSubDistrict;

                    $('#iptStudentPostalCode').val(preRegister.StudentPostalCode);
                    $('#iptStudentHousePhone').val(preRegister.StudentHousePhone);
                    $('#iptStudentHouseEmail').val(preRegister.StudentHouseEmail);

                    $('#sltStudentStayWithTitle').selectpicker('val', preRegister.StudentStayWithTitle);
                    $('#iptStudentStayWithName').val(preRegister.StudentStayWithName);
                    $('#iptStudentStayWithLast').val(preRegister.StudentStayWithLast);
                    $('#iptStudentStayWithEmergencyCall').val(preRegister.StudentStayWithEmergencyCall);
                    $('#sltStudentHomeType').selectpicker('val', preRegister.StudentHomeType);
                    $('#iptStudentNeighborName').val(preRegister.StudentNeighborName);
                    $('#iptStudentNeighborLastName').val(preRegister.StudentNeighborLastName);
                    $('#sltStudentNeighborSubLevel').selectpicker('val', preRegister.StudentNeighborSubLevel);
                    $('#iptStudentNeighborPhone').val(preRegister.StudentNeighborPhone);

                }

            } else {
                // No web storage Support.
            }
        }

        function SaveDataToLocalStorage(callbackFunction) {
            if ($("#form").valid()) {

                preRegister.SameHouseAddress = $('input[name=iptSameHouseAddress]:checked').val();
                preRegister.StudentHomeNumber = $('#iptStudentHomeNumber').val();
                preRegister.StudentAlley = $('#iptStudentAlley').val();
                preRegister.StudentVillageNo = $('#iptStudentVillageNo').val();
                preRegister.StudentRoad = $('#iptStudentRoad').val();
                preRegister.StudentProvince = $('#sltStudentProvince').val();
                preRegister.StudentProvinceText = $('#sltStudentProvince').find(':selected').text();
                preRegister.StudentDistrict = $('#sltStudentDistrict').val();
                preRegister.StudentDistrictText = $('#sltStudentDistrict').find(':selected').text();
                preRegister.StudentSubDistrict = $('#sltStudentSubDistrict').val();
                preRegister.StudentSubDistrictText = $('#sltStudentSubDistrict').find(':selected').text();
                preRegister.StudentPostalCode = $('#iptStudentPostalCode').val();
                preRegister.StudentHousePhone = $('#iptStudentHousePhone').val();
                preRegister.StudentHouseEmail = $('#iptStudentHouseEmail').val();

                preRegister.StudentStayWithTitle = $('#sltStudentStayWithTitle').val();
                preRegister.StudentStayWithTitleText = $('#sltStudentStayWithTitle').find(':selected').text();
                preRegister.StudentStayWithName = $('#iptStudentStayWithName').val();
                preRegister.StudentStayWithLast = $('#iptStudentStayWithLast').val();
                preRegister.StudentStayWithEmergencyCall = $('#iptStudentStayWithEmergencyCall').val();
                preRegister.StudentHomeType = $('#sltStudentHomeType').val();
                preRegister.StudentHomeTypeText = $('#sltStudentHomeType').find(':selected').text();
                preRegister.StudentNeighborName = $('#iptStudentNeighborName').val();
                preRegister.StudentNeighborLastName = $('#iptStudentNeighborLastName').val();
                preRegister.StudentNeighborSubLevel = $('#sltStudentNeighborSubLevel').val();
                preRegister.StudentNeighborSubLevelText = $('#sltStudentNeighborSubLevel').find(':selected').text();
                preRegister.StudentNeighborPhone = $('#iptStudentNeighborPhone').val();

                preRegister.Page06Saved = true;

                // Save data to localStorage
                if (ls.isBrowserSupport()) {
                    // Code for localStorage
                    ls.setItem('preRegister', preRegister);
                } else {
                    // No web storage Support.
                }

                ez.activePageComplete(6);

                callbackFunction();

            }
        }

        function LoadDistrict(provinceID) {
            if (provinceID && (oldProvince != provinceID)) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline06.aspx/LoadDistrict",
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

            $('#sltStudentDistrict').empty();

            var options = '';
            $(plans).each(function () {

                options += '<option value="' + this.AMPHUR_ID + '">' + this.AMPHUR_NAME + '</option>';

            });

            $('#sltStudentDistrict').html(options);

            //$('#sltStudentDistrict').prop('disabled', radioFlag);
            $('#sltStudentDistrict').selectpicker('refresh');
        }

        function LoadSubDistrict(districtID) {
            if (districtID && (oldDistrict != districtID)) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline06.aspx/LoadSubDistrict",
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

            $('#sltStudentSubDistrict').empty();

            var options = '';
            $(plans).each(function () {

                options += '<option value="' + this.DISTRICT_ID + '">' + this.DISTRICT_NAME + '</option>';

            });

            $('#sltStudentSubDistrict').html(options);

            //$('#sltStudentSubDistrict').prop('disabled', radioFlag);
            $('#sltStudentSubDistrict').selectpicker('refresh');
        }

        function AddRequiredRulesvalidation(obj) {
            $(obj).closest('div.col-required-field').find('.red-star').html('<sup>*</sup>');
            $(obj).rules('add', { required: true });
            //$(obj).rules('remove', 'required');
        }

        var oldProvince = 0;
        var oldDistrict = 0;
        var oldSubDistrict = 0;
        var radioFlag = false;
        var radioState;

        var preRegister = null;
        $(document).ready(function () {

            LoadDataFromLocalStorage();

            ez.activateBootstrapSelect();

            $("#form").validate({
                rules: {
                    iptStudentHomeNumber: { required: false },
                    iptStudentAlley: { required: false },
                    iptStudentVillageNo: { required: false },
                    iptStudentRoad: { required: false },
                    sltStudentProvince: { required: false },
                    sltStudentDistrict: { required: false },
                    sltStudentSubDistrict: { required: false },
                    iptStudentPostalCode: {
                        required: false,
                        number: true
                    },
                    iptStudentHousePhone: {
                        required: false,
                        number: true,
                        minlength: 10,
                        maxlength: 10
                    },
                    iptStudentHouseEmail: {
                        required: false,
                        email: true
                    },
                    sltStudentStayWithTitle: { required: false },
                    iptStudentStayWithName: { required: false },
                    iptStudentStayWithLast: { required: false },
                    iptStudentStayWithEmergencyCall: {
                        required: false,
                        number: true
                    },
                    sltStudentHomeType: { required: false },
                    iptStudentNeighborName: { required: false },
                    iptStudentNeighborLastName: { required: false },
                    sltStudentNeighborSubLevel: { required: false },
                    iptStudentNeighborPhone: {
                        required: false,
                        number: true
                    }
                },
                messages: {
                    iptStudentHomeNumber: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentAlley: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentVillageNo: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentRoad: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltStudentProvince: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltStudentDistrict: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltStudentSubDistrict: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentPostalCode: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    iptStudentHousePhone: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>",
                        minlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132848") %>",
                        maxlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132849") %>"
                    },
                    iptStudentHouseEmail: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        email: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132850") %>"
                    },
                    sltStudentStayWithTitle: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentStayWithName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentStayWithLast: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentStayWithEmergencyCall: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    sltStudentHomeType: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentNeighborName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentNeighborLastName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltStudentNeighborSubLevel: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptStudentNeighborPhone: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    }
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "sltStudentProvince":
                        case "sltStudentDistrict":
                        case "sltStudentSubDistrict":
                        case "sltStudentStayWithTitle":
                        case "sltStudentHomeType":
                        case "sltStudentNeighborSubLevel": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            $("input[name='iptSameHouseAddress']:radio").on('click', function (e) {
                if (radioState === this) {
                    // unchecked
                    this.checked = false;
                    radioState = null;

                    radioFlag = false;

                    // Enable control
                    $("input.form-control:not(.stay-with)").prop('disabled', false);

                    $('#sltStudentProvince').prop('disabled', false);
                    $('#sltStudentProvince').selectpicker('refresh');

                    $('#sltStudentDistrict').prop('disabled', false);
                    $('#sltStudentDistrict').selectpicker('refresh');

                    $('#sltStudentSubDistrict').prop('disabled', false);
                    $('#sltStudentSubDistrict').selectpicker('refresh');

                } else {
                    // checked
                    radioState = this;

                    radioFlag = true;

                    // Disable control
                    $("input.form-control:not(.stay-with)").prop('disabled', true);

                    $('#sltStudentProvince').prop('disabled', true);
                    $('#sltStudentProvince').selectpicker('refresh');

                    $('#sltStudentDistrict').prop('disabled', true);
                    $('#sltStudentDistrict').selectpicker('refresh');

                    $('#sltStudentSubDistrict').prop('disabled', true);
                    $('#sltStudentSubDistrict').selectpicker('refresh');

                    // Set value
                    $('#sltStudentProvince').selectpicker('val', preRegister.HouseProvince);
                    //LoadDistrict($("#sltStudentProvince").val());
                    oldProvince = preRegister.HouseProvince;

                    $('#sltStudentDistrict').selectpicker('val', preRegister.HouseDistrict);
                    //LoadSubDistrict($("#sltStudentDistrict").val());
                    oldDistrict = preRegister.HouseDistrict;

                    $('#sltStudentSubDistrict').selectpicker('val', preRegister.HouseSubDistrict);
                    oldSubDistrict = preRegister.HouseSubDistrict;

                    $('#iptStudentHomeNumber').val(preRegister.HouseHomeNumber);
                    $('#iptStudentAlley').val(preRegister.HouseAlley);
                    $('#iptStudentVillageNo').val(preRegister.HouseVillageNo);
                    $('#iptStudentRoad').val(preRegister.HouseRoad);
                    $('#iptStudentPostalCode').val(preRegister.HousePostalCode);
                    $('#iptStudentHousePhone').val(preRegister.HousePhone);
                    $('#iptStudentHouseEmail').val(preRegister.HouseEmail);

                    if (preRegister.Page06Saved) {
                        $('#sltStudentStayWithTitle').selectpicker('val', preRegister.StudentStayWithTitle);
                        $('#iptStudentStayWithName').val(preRegister.StudentStayWithName);
                        $('#iptStudentStayWithLast').val(preRegister.StudentStayWithLast);
                        $('#iptStudentStayWithEmergencyCall').val(preRegister.StudentStayWithEmergencyCall);
                        $('#sltStudentHomeType').selectpicker('val', preRegister.StudentHomeType);
                        $('#iptStudentNeighborName').val(preRegister.StudentNeighborName);
                        $('#iptStudentNeighborLastName').val(preRegister.StudentNeighborLastName);
                        $('#sltStudentNeighborSubLevel').selectpicker('val', preRegister.StudentNeighborSubLevel);
                        $('#iptStudentNeighborPhone').val(preRegister.StudentNeighborPhone);
                    }

                    $('.selectpicker').selectpicker('refresh');
                }
                //console.log(radioState === this);
            });

            $("#btnBack").bind({
                click: function () {

                    window.location.href = "RegisterOnline05.aspx";

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

                        window.location.href = "RegisterOnline07.aspx";

                    });

                    return false;
                }
            });

            $("#sltStudentProvince").change(function () {

                LoadDistrict($("#sltStudentProvince").val());

                if (!radioFlag) {
                    $('#sltStudentSubDistrict').empty();
                    //$('#sltStudentSubDistrict').prop('disabled', true);
                    $('#sltStudentSubDistrict').selectpicker('refresh');
                }
            });

            $("#sltStudentDistrict").change(function () {

                LoadSubDistrict($("#sltStudentDistrict").val());

            });

            // Init disable control
            //if ($.isEmpty($('#sltStudentDistrict').val())) {
            //    $('#sltStudentDistrict').prop('disabled', true);
            //    $('#sltStudentDistrict').selectpicker('refresh');
            //}

            //if ($.isEmpty($('#sltStudentSubDistrict').val())) {
            //    $('#sltStudentSubDistrict').prop('disabled', true);
            //    $('#sltStudentSubDistrict').selectpicker('refresh');
            //}
        });
    </script>
    <asp:Literal ID="ltrScriptRequiredField" runat="server"></asp:Literal>
</asp:Content>
