<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterOnline08.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnline08" %>

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
                            <h4 style="font-family: THSarabunNew; margin-bottom: 0px; padding: 3px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00194") %></h4>
                            <p class="h6 text-center">(Mother information)</p>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto">
                                <span style="color: red;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132854") %></span>
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
                                <select id="sltMotherTitle" name="sltMotherTitle" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <asp:Literal ID="ltrMotherTitle" runat="server" />
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104047") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Mother's Name)</p>
                                </div>
                                <input id="iptMotherName" name="iptMotherName" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Last Name)</p>
                                </div>
                                <input id="iptMotherLastname" name="iptMotherLastname" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104047") %>(Eng) <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Eng Mother's Name)</p>
                                </div>
                                <input id="iptMotherNameEn" name="iptMotherNameEn" type="text" class="form-control" maxlength="100">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>(Eng) <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Eng Last Name)</p>
                                </div>
                                <input id="iptMotherLastnameEn" name="iptMotherLastnameEn" type="text" class="form-control" maxlength="100">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Date of Birth)</p>
                                </div>
                                <input id="iptMotherBirthday" name="iptMotherBirthday" type="text" class="form-control datetimepicker" maxlength="10">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Citizen ID No.)</p>
                                </div>
                                <input id="iptMotherIDCard" name="iptMotherIDCard" type="text" class="form-control" maxlength="13">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Race)</p>
                                </div>
                                <%--<input id="iptMotherRace" name="iptMotherRace" type="text" class="form-control" maxlength="50">--%>
                                <select id="sltMotherRace" name="sltMotherRace" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %></option>
                                    <asp:Literal ID="ltrMotherRace" runat="server" />
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Nationality)</p>
                                </div>
                                <%--<input id="iptMotherNation" name="iptMotherNation" type="text" class="form-control" maxlength="50">--%>
                                <select id="sltMotherNation" name="sltMotherNation" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %></option>
                                    <asp:Literal ID="ltrMotherNation" runat="server" />
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Religion)</p>
                                </div>
                                <%--<input id="iptMotherReligion" name="iptMotherReligion" type="text" class="form-control" maxlength="50">--%>
                                <select id="sltMotherReligion" name="sltMotherReligion" class="selectpicker" data-live-search="false" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %></option>
                                    <asp:Literal ID="ltrMotherReligion" runat="server" />
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Degree of Education)</p>
                                </div>
                                <select id="sltMotherEducational" name="sltMotherEducational" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
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
                                <input id="iptMotherHomeNumber" name="iptMotherHomeNumber" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Soi)</p>
                                </div>
                                <input id="iptMotherAlley" name="iptMotherAlley" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Moo)</p>
                                </div>
                                <input id="iptMotherVillageNo" name="iptMotherVillageNo" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Street)</p>
                                </div>
                                <input id="iptMotherRoad" name="iptMotherRoad" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Province)</p>
                                </div>
                                <select id="sltMotherProvince" name="sltMotherProvince" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <asp:Literal ID="ltrProvince" runat="server" />
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(District)</p>
                                </div>
                                <select id="sltMotherDistrict" name="sltMotherDistrict" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Sub District)</p>
                                </div>
                                <select id="sltMotherSubDistrict" name="sltMotherSubDistrict" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Post Code)</p>
                                </div>
                                <input id="iptMotherPostalCode" name="iptMotherPostalCode" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Occupation)</p>
                                </div>
                                <input id="iptMotherCareer" name="iptMotherCareer" type="text" class="form-control" maxlength="100">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Company's Name)</p>
                                </div>
                                <input id="iptMotherWorkplace" name="iptMotherWorkplace" type="text" class="form-control" maxlength="200">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Monthly Income)</p>
                                </div>
                                <input id="iptMotherMonthIncome" name="iptMotherMonthIncome" type="text" class="form-control" maxlength="15">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104045") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Yearly Income)</p>
                                </div>
                                <select id="sltMotherYearIncome" name="sltMotherYearIncome" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
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
                                <input id="iptMotherHousePhone" name="iptMotherHousePhone" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Mobile Phone)</p>
                                </div>
                                <input id="iptMotherMobilePhone" name="iptMotherMobilePhone" type="text" class="form-control" maxlength="20">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Work Phone Number)</p>
                                </div>
                                <input id="iptMotherWorkPhone" name="iptMotherWorkPhone" type="text" class="form-control" maxlength="20">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(E-Mail)</p>
                                </div>
                                <input id="iptMotherEmail" name="iptMotherEmail" type="text" class="form-control" maxlength="100">
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

                if (preRegister.Page08Saved) {
                    $('#sltMotherTitle').selectpicker('val', preRegister.MotherTitle);
                    $('#iptMotherName').val(preRegister.MotherName);
                    $('#iptMotherLastname').val(preRegister.MotherLastname);
                    $('#iptMotherNameEn').val(preRegister.MotherNameEn);
                    $('#iptMotherLastnameEn').val(preRegister.MotherLastnameEn);
                    $('#iptMotherBirthday').val(preRegister.MotherBirthday);
                    $('#iptMotherIDCard').val(preRegister.MotherIDCard);
                    //$('#iptMotherRace').val(preRegister.MotherRace);
                    $('#sltMotherRace').selectpicker('val', preRegister.MotherRace);
                    //$('#iptMotherNation').val(preRegister.MotherNation);
                    $('#sltMotherNation').selectpicker('val', preRegister.MotherNation);
                    //$('#iptMotherReligion').val(preRegister.MotherReligion);
                    $('#sltMotherReligion').selectpicker('val', preRegister.MotherReligion);
                    $('#sltMotherEducational').selectpicker('val', preRegister.MotherEducational);
                    $('#iptMotherHomeNumber').val(preRegister.MotherHomeNumber);
                    $('#iptMotherAlley').val(preRegister.MotherAlley);
                    $('#iptMotherVillageNo').val(preRegister.MotherVillageNo);
                    $('#iptMotherRoad').val(preRegister.MotherRoad);
                    $('#sltMotherProvince').selectpicker('val', preRegister.MotherProvince);
                    LoadDistrict($("#sltMotherProvince").val());

                    $('#sltMotherDistrict').selectpicker('val', preRegister.MotherDistrict);
                    LoadSubDistrict($("#sltMotherDistrict").val());

                    $('#sltMotherSubDistrict').selectpicker('val', preRegister.MotherSubDistrict);
                    $('#iptMotherPostalCode').val(preRegister.MotherPostalCode);
                    $('#iptMotherCareer').val(preRegister.MotherCareer);
                    $('#iptMotherWorkplace').val(preRegister.MotherWorkplace);
                    $('#iptMotherMonthIncome').val(preRegister.MotherMonthIncome);
                    $('#sltMotherYearIncome').selectpicker('val', preRegister.MotherYearIncome);
                    $('#iptMotherHousePhone').val(preRegister.MotherHousePhone);
                    $('#iptMotherMobilePhone').val(preRegister.MotherMobilePhone);
                    $('#iptMotherWorkPhone').val(preRegister.MotherWorkPhone);
                    $('#iptMotherEmail').val(preRegister.MotherEmail);
                }
            } else {
                // No web storage Support.
            }
        }

        function SaveDataToLocalStorage(callbackFunction) {
            if ($("#form").valid()) {

                preRegister.MotherTitle = $('#sltMotherTitle').val();
                preRegister.MotherTitleText = $('#sltMotherTitle').find(':selected').text();
                preRegister.MotherName = $('#iptMotherName').val();
                preRegister.MotherLastname = $('#iptMotherLastname').val();
                preRegister.MotherNameEn = $('#iptMotherNameEn').val();
                preRegister.MotherLastnameEn = $('#iptMotherLastnameEn').val();
                preRegister.MotherBirthday = $('#iptMotherBirthday').val();
                preRegister.MotherIDCard = $('#iptMotherIDCard').val();
                //preRegister.MotherRace = $('#iptMotherRace').val();
                preRegister.MotherRace = $('#sltMotherRace').val();
                preRegister.MotherRaceText = $('#sltMotherRace').find(':selected').text();
                //preRegister.MotherNation = $('#iptMotherNation').val();
                preRegister.MotherNation = $('#sltMotherNation').val();
                preRegister.MotherNationText = $('#sltMotherNation').find(':selected').text();
                //preRegister.MotherReligion = $('#iptMotherReligion').val();
                preRegister.MotherReligion = $('#sltMotherReligion').val();
                preRegister.MotherReligionText = $('#sltMotherReligion').find(':selected').text();
                preRegister.MotherEducational = $('#sltMotherEducational').val();
                preRegister.MotherEducationalText = $('#sltMotherEducational').find(':selected').text();
                preRegister.MotherHomeNumber = $('#iptMotherHomeNumber').val();
                preRegister.MotherAlley = $('#iptMotherAlley').val();
                preRegister.MotherVillageNo = $('#iptMotherVillageNo').val();
                preRegister.MotherRoad = $('#iptMotherRoad').val();
                preRegister.MotherProvince = $('#sltMotherProvince').val();
                preRegister.MotherProvinceText = $('#sltMotherProvince').find(':selected').text();
                preRegister.MotherDistrict = $('#sltMotherDistrict').val();
                preRegister.MotherDistrictText = $('#sltMotherDistrict').find(':selected').text();
                preRegister.MotherSubDistrict = $('#sltMotherSubDistrict').val();
                preRegister.MotherSubDistrictText = $('#sltMotherSubDistrict').find(':selected').text();
                preRegister.MotherPostalCode = $('#iptMotherPostalCode').val();
                preRegister.MotherCareer = $('#iptMotherCareer').val();
                preRegister.MotherWorkplace = $('#iptMotherWorkplace').val();
                preRegister.MotherMonthIncome = $('#iptMotherMonthIncome').val();
                preRegister.MotherYearIncome = $('#sltMotherYearIncome').val();
                preRegister.MotherYearIncomeText = $('#sltMotherYearIncome').find(':selected').text();
                preRegister.MotherHousePhone = $('#iptMotherHousePhone').val();
                preRegister.MotherMobilePhone = $('#iptMotherMobilePhone').val();
                preRegister.MotherWorkPhone = $('#iptMotherWorkPhone').val();
                preRegister.MotherEmail = $('#iptMotherEmail').val();

                preRegister.Page08Saved = true;

                // Save data to localStorage
                if (ls.isBrowserSupport()) {
                    // Code for localStorage
                    ls.setItem('preRegister', preRegister);
                } else {
                    // No web storage Support.
                }

                ez.activePageComplete(8);

                callbackFunction();

            }
        }

        function LoadDistrict(provinceID) {
            if (provinceID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline08.aspx/LoadDistrict",
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

            $('#sltMotherDistrict').empty();

            var options = '';
            $(plans).each(function () {

                options += '<option value="' + this.AMPHUR_ID + '">' + this.AMPHUR_NAME + '</option>';

            });

            $('#sltMotherDistrict').html(options);

            //$('#sltMotherDistrict').prop('disabled', false);
            $('#sltMotherDistrict').selectpicker('refresh');
        }

        function LoadSubDistrict(districtID) {
            if (districtID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline08.aspx/LoadSubDistrict",
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

            $('#sltMotherSubDistrict').empty();

            var options = '';
            $(plans).each(function () {

                options += '<option value="' + this.DISTRICT_ID + '">' + this.DISTRICT_NAME + '</option>';

            });

            $('#sltMotherSubDistrict').html(options);

            //$('#sltMotherSubDistrict').prop('disabled', false);
            $('#sltMotherSubDistrict').selectpicker('refresh');
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

            ez.initFormExtendedDatetimepickersDisableFutureDate('#iptMotherBirthday');
            
            $.validator.addMethod("thaiDate",
                function (value, element) {
                    return value == '' || value.match(/^(0?[1-9]|[12][0-9]|3[0-1])[/., -](0?[1-9]|1[<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305069") %>])[/., -](24|25)?\d{2}$/);
                },
                "Please enter a date in the format dd/mm/yyyy."
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
                    sltMotherTitle: { required: false },
                    iptMotherName: { required: false },
                    iptMotherLastname: { required: false },
                    iptMotherNameEn: { required: false },
                    iptMotherLastnameEn: { required: false },
                    iptMotherBirthday: {
                        required: false,
                        thaiDate: true,
                        disableFutureDate: true
                    },
                    iptMotherIDCard: {
                        required: false,
                        minlength: 7,
                        maxlength: 13
                    },
                    sltMotherRace: { required: false },
                    sltMotherNation: { required: false },
                    sltMotherReligion: { required: false },
                    sltMotherEducational: { required: false },
                    iptMotherHomeNumber: { required: false },
                    iptMotherAlley: { required: false },
                    iptMotherVillageNo: { required: false },
                    iptMotherRoad: { required: false },
                    sltMotherProvince: { required: false },
                    sltMotherDistrict: { required: false },
                    sltMotherSubDistrict: { required: false },
                    iptMotherPostalCode: {
                        required: false,
                        number: true
                    },
                    iptMotherCareer: { required: false },
                    iptMotherWorkplace: { required: false },
                    iptMotherMonthIncome: {
                        required: false,
                        number: true
                    },
                    sltMotherYearIncome: { required: false },
                    iptMotherHousePhone: {
                        required: false,
                        number: true
                    },
                    iptMotherMobilePhone: {
                        required: false,
                        number: true,
                        minlength: 10,
                        maxlength: 10
                    },
                    iptMotherWorkPhone: {
                        required: false,
                        number: true
                    },
                    iptMotherEmail: {
                        required: false,
                        email: true
                    }
                },
                messages: {
                    sltMotherTitle: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMotherName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMotherLastname: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMotherNameEn: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMotherLastnameEn: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMotherBirthday: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                    },
                    iptMotherIDCard: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        minlength: jQuery.validator.format("Please, at least {0} characters are necessary."),
                        maxlength: jQuery.validator.format("Please, at most {0} characters are necessary.")
                    },
                    sltMotherRace: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltMotherNation: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltMotherReligion: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltMotherEducational: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMotherHomeNumber: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMotherAlley: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMotherVillageNo: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMotherRoad: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltMotherProvince: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltMotherDistrict: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltMotherSubDistrict: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMotherPostalCode: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    iptMotherCareer: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMotherWorkplace: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMotherMonthIncome: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    sltMotherYearIncome: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMotherHousePhone: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    iptMotherMobilePhone: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>",
                        minlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132848") %>",
                        maxlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132849") %>"
                    },
                    iptMotherWorkPhone: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    iptMotherEmail: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        email: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132850") %>"
                    }
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "sltMotherTitle":
                        case "sltMotherEducational":
                        case "sltMotherProvince":
                        case "sltMotherDistrict":
                        case "sltMotherSubDistrict":
                        case "sltMotherYearIncome":
                        case "sltMotherNation":
                        case "sltMotherReligion":
                        case "sltMotherRace": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            $("#btnBack").bind({
                click: function () {

                    window.location.href = "RegisterOnline07.aspx";

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

                        window.location.href = "RegisterOnline09.aspx";

                    });

                    return false;
                }
            });

            $("#sltMotherProvince").change(function () {

                LoadDistrict($("#sltMotherProvince").val());

                $('#sltMotherSubDistrict').empty();
                //$('#sltMotherSubDistrict').prop('disabled', true);
                $('#sltMotherSubDistrict').selectpicker('refresh');
            });

            $("#sltMotherDistrict").change(function () {

                LoadSubDistrict($("#sltMotherDistrict").val());

            });

            // Init disable control
            //if ($.isEmpty($('#sltMotherDistrict').val())) {
            //    $('#sltMotherDistrict').prop('disabled', true);
            //    $('#sltMotherDistrict').selectpicker('refresh');
            //}

            //if ($.isEmpty($('#sltMotherSubDistrict').val())) {
            //    $('#sltMotherSubDistrict').prop('disabled', true);
            //    $('#sltMotherSubDistrict').selectpicker('refresh');
            //}

            LoadDataFromLocalStorage();

            $('#iptMotherMonthIncome').number(true, 2);

            if ($.isEmpty(preRegister.MotherBirthday)) {
                $('#iptMotherBirthday').val('');
            }
        });
    </script>
    <asp:Literal ID="ltrScriptRequiredField" runat="server"></asp:Literal>
</asp:Content>
