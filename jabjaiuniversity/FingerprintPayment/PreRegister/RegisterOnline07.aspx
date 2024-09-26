<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterOnline07.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnline07" %>

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
                            <h4 style="font-family: THSarabunNew; margin-bottom: 0px; padding: 3px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101180") %></h4>
                            <p class="h6 text-center">(Father information)</p>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto">
                                <span style="color: red;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132852") %></span>
                            </div>
                        </div>
                        <div class="row">
                            <br>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-select">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Prefixes)</p>
                                </div>
                                <select id="sltFatherTitle" name="sltFatherTitle" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <asp:Literal ID="ltrFatherTitle" runat="server" />
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211011") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Father's name)</p>
                                </div>
                                <input id="iptFatherName" name="iptFatherName" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Last Name)</p>
                                </div>
                                <input id="iptFatherLastname" name="iptFatherLastname" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132853") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Eng Father's name)</p>
                                </div>
                                <input id="iptFatherNameEn" name="iptFatherNameEn" type="text" class="form-control" maxlength="100">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>(Eng) <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Eng Last Name)</p>
                                </div>
                                <input id="iptFatherLastnameEn" name="iptFatherLastnameEn" type="text" class="form-control" maxlength="100">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Date of Birth)</p>
                                </div>
                                <input id="iptFatherBirthday" name="iptFatherBirthday" type="text" class="form-control datetimepicker" maxlength="10">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Citizen ID No.)</p>
                                </div>
                                <input id="iptFatherIDCard" name="iptFatherIDCard" type="text" class="form-control" maxlength="13">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Race)</p>
                                </div>
                                <%--<input id="iptFatherRace" name="iptFatherRace" type="text" class="form-control" maxlength="50">--%>
                                <select id="sltFatherRace" name="sltFatherRace" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %></option>
                                    <asp:Literal ID="ltrFatherRace" runat="server" />
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Nationality)</p>
                                </div>
                                <%--<input id="iptFatherNation" name="iptFatherNation" type="text" class="form-control" maxlength="50">--%>
                                <select id="sltFatherNation" name="sltFatherNation" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %></option>
                                    <asp:Literal ID="ltrFatherNation" runat="server" />
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Religion)</p>
                                </div>
                                <%--<input id="iptFatherReligion" name="iptFatherReligion" type="text" class="form-control" maxlength="50">--%>
                                <select id="sltFatherReligion" name="sltFatherReligion" class="selectpicker" data-live-search="false" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %></option>
                                    <asp:Literal ID="ltrFatherReligion" runat="server" />
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Degree of Education)</p>
                                </div>
                                <select id="sltFatherEducational" name="sltFatherEducational" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00745") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %></option>
                                    <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01315") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %></option>
                                    <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103101") %></option>
                                    <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103102") %></option>
                                    <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103103") %></option>
                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103104") %></option>
                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %></option>
                                    <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102061") %></option>
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Address No.)</p>
                                </div>
                                <input id="iptFatherHomeNumber" name="iptFatherHomeNumber" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Soi)</p>
                                </div>
                                <input id="iptFatherAlley" name="iptFatherAlley" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Moo)</p>
                                </div>
                                <input id="iptFatherVillageNo" name="iptFatherVillageNo" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Street)</p>
                                </div>
                                <input id="iptFatherRoad" name="iptFatherRoad" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Province)</p>
                                </div>
                                <select id="sltFatherProvince" name="sltFatherProvince" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <asp:Literal ID="ltrProvince" runat="server" />
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(District)</p>
                                </div>
                                <select id="sltFatherDistrict" name="sltFatherDistrict" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Sub District)</p>
                                </div>
                                <select id="sltFatherSubDistrict" name="sltFatherSubDistrict" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Post Code)</p>
                                </div>
                                <input id="iptFatherPostalCode" name="iptFatherPostalCode" type="text" class="form-control" maxlength="5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Occupation)</p>
                                </div>
                                <input id="iptFatherCareer" name="iptFatherCareer" type="text" class="form-control" maxlength="100">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Company's Name)</p>
                                </div>
                                <input id="iptFatherWorkplace" name="iptFatherWorkplace" type="text" class="form-control" maxlength="200">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Monthly Income)</p>
                                </div>
                                <input id="iptFatherMonthIncome" name="iptFatherMonthIncome" type="text" class="form-control" maxlength="15">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104045") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Yearly Income)</p>
                                </div>
                                <select id="sltFatherYearIncome" name="sltFatherYearIncome" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option value="0 - 150,000"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103106") %></option>
                                    <option value="150,001 - 300,000"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103108") %></option>
                                    <option value="300,001 - 500,000"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103109") %></option>
                                    <option value="500,001 - 750,000"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103110") %></option>
                                    <option value="750,001 - 1,000,000"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103111") %></option>
                                    <option value="1,000,001 - 2,000,000"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103112") %></option>
                                    <option value="2,000,001 - 5,000,000"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103113") %></option>
                                    <option value="5,000,001+"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103114") %></option>
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Telephone Number)</p>
                                </div>
                                <input id="iptFatherHousePhone" name="iptFatherHousePhone" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Mobile Phone)</p>
                                </div>
                                <input id="iptFatherMobilePhone" name="iptFatherMobilePhone" type="text" class="form-control" maxlength="20">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Work Phone Number)</p>
                                </div>
                                <input id="iptFatherWorkPhone" name="iptFatherWorkPhone" type="text" class="form-control" maxlength="20">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(E-Mail)</p>
                                </div>
                                <input id="iptFatherEmail" name="iptFatherEmail" type="text" class="form-control" maxlength="100">
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

    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>

    <script>

        function LoadDataFromLocalStorage() {
            // Get data from localStorage
            if (ls.isBrowserSupport()) {
                // Code for localStorage
                preRegister = ls.getItem('preRegister');

                if (preRegister.Page07Saved) {
                    $('#sltFatherTitle').selectpicker('val', preRegister.FatherTitle);
                    $('#iptFatherName').val(preRegister.FatherName);
                    $('#iptFatherLastname').val(preRegister.FatherLastname);
                    $('#iptFatherNameEn').val(preRegister.FatherNameEn);
                    $('#iptFatherLastnameEn').val(preRegister.FatherLastnameEn);
                    $('#iptFatherBirthday').val(preRegister.FatherBirthday);
                    $('#iptFatherIDCard').val(preRegister.FatherIDCard);
                    //$('#iptFatherRace').val(preRegister.FatherRace);
                    $('#sltFatherRace').selectpicker('val', preRegister.FatherRace);
                    //$('#iptFatherNation').val(preRegister.FatherNation);
                    $('#sltFatherNation').selectpicker('val', preRegister.FatherNation);
                    //$('#iptFatherReligion').val(preRegister.FatherReligion);
                    $('#sltFatherReligion').selectpicker('val', preRegister.FatherReligion);
                    $('#sltFatherEducational').selectpicker('val', preRegister.FatherEducational);
                    $('#iptFatherHomeNumber').val(preRegister.FatherHomeNumber);
                    $('#iptFatherAlley').val(preRegister.FatherAlley);
                    $('#iptFatherVillageNo').val(preRegister.FatherVillageNo);
                    $('#iptFatherRoad').val(preRegister.FatherRoad);
                    $('#sltFatherProvince').selectpicker('val', preRegister.FatherProvince);
                    LoadDistrict($("#sltFatherProvince").val());

                    $('#sltFatherDistrict').selectpicker('val', preRegister.FatherDistrict);
                    LoadSubDistrict($("#sltFatherDistrict").val());

                    $('#sltFatherSubDistrict').selectpicker('val', preRegister.FatherSubDistrict);
                    $('#iptFatherPostalCode').val(preRegister.FatherPostalCode);
                    $('#iptFatherCareer').val(preRegister.FatherCareer);
                    $('#iptFatherWorkplace').val(preRegister.FatherWorkplace);
                    $('#iptFatherMonthIncome').val(preRegister.FatherMonthIncome);
                    $('#sltFatherYearIncome').selectpicker('val', preRegister.FatherYearIncome);
                    $('#iptFatherHousePhone').val(preRegister.FatherHousePhone);
                    $('#iptFatherMobilePhone').val(preRegister.FatherMobilePhone);
                    $('#iptFatherWorkPhone').val(preRegister.FatherWorkPhone);
                    $('#iptFatherEmail').val(preRegister.FatherEmail);
                }
            } else {
                // No web storage Support.
            }
        }

        function SaveDataToLocalStorage(callbackFunction) {
            if ($("#form").valid()) {

                preRegister.FatherTitle = $('#sltFatherTitle').val();
                preRegister.FatherTitleText = $('#sltFatherTitle').find(':selected').text();
                preRegister.FatherName = $('#iptFatherName').val();
                preRegister.FatherLastname = $('#iptFatherLastname').val();
                preRegister.FatherNameEn = $('#iptFatherNameEn').val();
                preRegister.FatherLastnameEn = $('#iptFatherLastnameEn').val();
                preRegister.FatherBirthday = $('#iptFatherBirthday').val();
                preRegister.FatherIDCard = $('#iptFatherIDCard').val();
                //preRegister.FatherRace = $('#iptFatherRace').val();
                preRegister.FatherRace = $('#sltFatherRace').val();
                preRegister.FatherRaceText = $('#sltFatherRace').find(':selected').text();
                //preRegister.FatherNation = $('#iptFatherNation').val();
                preRegister.FatherNation = $('#sltFatherNation').val();
                preRegister.FatherNationText = $('#sltFatherNation').find(':selected').text();
                //preRegister.FatherReligion = $('#iptFatherReligion').val();
                preRegister.FatherReligion = $('#sltFatherReligion').val();
                preRegister.FatherReligionText = $('#sltFatherReligion').find(':selected').text();
                preRegister.FatherEducational = $('#sltFatherEducational').val();
                preRegister.FatherEducationalText = $('#sltFatherEducational').find(':selected').text();
                preRegister.FatherHomeNumber = $('#iptFatherHomeNumber').val();
                preRegister.FatherAlley = $('#iptFatherAlley').val();
                preRegister.FatherVillageNo = $('#iptFatherVillageNo').val();
                preRegister.FatherRoad = $('#iptFatherRoad').val();
                preRegister.FatherProvince = $('#sltFatherProvince').val();
                preRegister.FatherProvinceText = $('#sltFatherProvince').find(':selected').text();
                preRegister.FatherDistrict = $('#sltFatherDistrict').val();
                preRegister.FatherDistrictText = $('#sltFatherDistrict').find(':selected').text();
                preRegister.FatherSubDistrict = $('#sltFatherSubDistrict').val();
                preRegister.FatherSubDistrictText = $('#sltFatherSubDistrict').find(':selected').text();
                preRegister.FatherPostalCode = $('#iptFatherPostalCode').val();
                preRegister.FatherCareer = $('#iptFatherCareer').val();
                preRegister.FatherWorkplace = $('#iptFatherWorkplace').val();
                preRegister.FatherMonthIncome = $('#iptFatherMonthIncome').val();
                preRegister.FatherYearIncome = $('#sltFatherYearIncome').val();
                preRegister.FatherYearIncomeText = $('#sltFatherYearIncome').find(':selected').text();
                preRegister.FatherHousePhone = $('#iptFatherHousePhone').val();
                preRegister.FatherMobilePhone = $('#iptFatherMobilePhone').val();
                preRegister.FatherWorkPhone = $('#iptFatherWorkPhone').val();
                preRegister.FatherEmail = $('#iptFatherEmail').val();

                preRegister.Page07Saved = true;

                // Save data to localStorage
                if (ls.isBrowserSupport()) {
                    // Code for localStorage
                    ls.setItem('preRegister', preRegister);
                } else {
                    // No web storage Support.
                }

                ez.activePageComplete(7);

                callbackFunction();

            }
        }

        function LoadDistrict(provinceID) {
            if (provinceID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline07.aspx/LoadDistrict",
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

            $('#sltFatherDistrict').empty();

            var options = '';
            $(plans).each(function () {

                options += '<option value="' + this.AMPHUR_ID + '">' + this.AMPHUR_NAME + '</option>';

            });

            $('#sltFatherDistrict').html(options);

            //$('#sltFatherDistrict').prop('disabled', false);
            $('#sltFatherDistrict').selectpicker('refresh');
        }

        function LoadSubDistrict(districtID) {
            if (districtID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline07.aspx/LoadSubDistrict",
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

            $('#sltFatherSubDistrict').empty();

            var options = '';
            $(plans).each(function () {

                options += '<option value="' + this.DISTRICT_ID + '">' + this.DISTRICT_NAME + '</option>';

            });

            $('#sltFatherSubDistrict').html(options);

            //$('#sltFatherSubDistrict').prop('disabled', false);
            $('#sltFatherSubDistrict').selectpicker('refresh');
        }

        function AddRequiredRulesvalidation(obj) {
            $(obj).closest('div.col-required-field').find('.red-star').html('<sup>*</sup>');
            $(obj).rules('add', { required: true });
            //$(obj).rules('remove', 'required');
        }

        var preRegister = null;
        $(document).ready(function () {

            //LoadDataFromLocalStorage();

            ez.activateBootstrapSelect();

            ez.initFormExtendedDatetimepickersDisableFutureDate('#iptFatherBirthday');

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

            $("#form").validate({
                rules: {
                    sltFatherTitle: { required: false },
                    iptFatherName: { required: false },
                    iptFatherLastname: { required: false },
                    iptFatherNameEn: { required: false },
                    iptFatherLastnameEn: { required: false },
                    iptFatherBirthday: {
                        required: false,
                        thaiDate: true,
                        disableFutureDate: true
                    },
                    iptFatherIDCard: {
                        required: false,
                        minlength: 7,
                        maxlength: 13
                    },
                    sltFatherRace: { required: false },
                    sltFatherNation: { required: false },
                    sltFatherReligion: { required: false },
                    sltFatherEducational: { required: false },
                    iptFatherHomeNumber: { required: false },
                    iptFatherAlley: { required: false },
                    iptFatherVillageNo: { required: false },
                    iptFatherRoad: { required: false },
                    sltFatherProvince: { required: false },
                    sltFatherDistrict: { required: false },
                    sltFatherSubDistrict: { required: false },
                    iptFatherPostalCode: {
                        required: false,
                        number: true
                    },
                    iptFatherCareer: { required: false },
                    iptFatherWorkplace: { required: false },
                    iptFatherMonthIncome: {
                        required: false,
                        number: true
                    },
                    sltFatherYearIncome: { required: false },
                    iptFatherHousePhone: {
                        required: false,
                        number: true
                    },
                    iptFatherMobilePhone: {
                        required: false,
                        number: true,
                        minlength: 10,
                        maxlength: 10
                    },
                    iptFatherWorkPhone: {
                        required: false,
                        number: true
                    },
                    iptFatherEmail: {
                        required: false,
                        email: true
                    }
                },
                messages: {
                    sltFatherTitle: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFatherName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFatherLastname: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFatherNameEn: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFatherLastnameEn: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFatherBirthday: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                    },
                    iptFatherIDCard: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        minlength: jQuery.validator.format("Please, at least {0} characters are necessary."),
                        maxlength: jQuery.validator.format("Please, at most {0} characters are necessary.")
                    },
                    sltFatherRace: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltFatherNation: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltFatherReligion: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltFatherEducational: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFatherHomeNumber: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFatherAlley: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFatherVillageNo: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFatherRoad: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltFatherProvince: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltFatherDistrict: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltFatherSubDistrict: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFatherPostalCode: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    iptFatherCareer: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFatherWorkplace: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFatherMonthIncome: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    sltFatherYearIncome: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptFatherHousePhone: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    iptFatherMobilePhone: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>",
                        minlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132848") %>",
                        maxlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132849") %>"
                    },
                    iptFatherWorkPhone: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    iptFatherEmail: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        email: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132850") %>"
                    }
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "sltFatherTitle":
                        case "sltFatherEducational":
                        case "sltFatherProvince":
                        case "sltFatherDistrict":
                        case "sltFatherSubDistrict":
                        case "sltFatherYearIncome":
                        case "sltFatherNation":
                        case "sltFatherReligion":
                        case "sltFatherRace": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            $("#btnBack").bind({
                click: function () {

                    window.location.href = "RegisterOnline06.aspx";

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

                        window.location.href = "RegisterOnline08.aspx";

                    });

                    return false;
                }
            });

            $("#sltFatherProvince").change(function () {

                LoadDistrict($("#sltFatherProvince").val());

                $('#sltFatherSubDistrict').empty();
                //$('#sltFatherSubDistrict').prop('disabled', true);
                $('#sltFatherSubDistrict').selectpicker('refresh');
            });

            $("#sltFatherDistrict").change(function () {

                LoadSubDistrict($("#sltFatherDistrict").val());

            });

            // Init disable control
            //if ($.isEmpty($('#sltFatherDistrict').val())) {
            //    $('#sltFatherDistrict').prop('disabled', true);
            //    $('#sltFatherDistrict').selectpicker('refresh');
            //}

            //if ($.isEmpty($('#sltFatherSubDistrict').val())) {
            //    $('#sltFatherSubDistrict').prop('disabled', true);
            //    $('#sltFatherSubDistrict').selectpicker('refresh');
            //}

            LoadDataFromLocalStorage();

            $('#iptFatherMonthIncome').number(true, 2);

            if ($.isEmpty(preRegister.FatherBirthday)) {
                $('#iptFatherBirthday').val('');
            }
        });
    </script>
    <asp:Literal ID="ltrScriptRequiredField" runat="server"></asp:Literal>
</asp:Content>
