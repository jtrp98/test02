<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterOnline09.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnline09" %>

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

        .dropdown.bootstrap-select.disabled {
            cursor: not-allowed;
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
                            <h4 style="font-family: THSarabunNew; margin-bottom: 0px; padding: 3px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %></h4>
                            <p class="h6 text-center">(Parent information)</p>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <br>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101192") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Relationship)</p>
                                </div>
                                <select id="sltParentRelation" name="sltParentRelation" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101195") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101195") %></option>
                                    <option value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101196") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101196") %></option>
                                    <option value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></option>
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132855") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Relationship)</p>
                                </div>
                                <input id="iptParentRelation" name="iptParentRelation" type="text" class="form-control" maxlength="20" disabled />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag div-bag-select">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Prefixes)</p>
                                </div>
                                <select id="sltParentTitle" name="sltParentTitle" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <asp:Literal ID="ltrParentTitle" runat="server" />
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104038") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %>) <span class="red-star"></span>: </span></label>
                                    <p class="h6">(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Name)</p>
                                </div>
                                <input id="iptParentName" name="iptParentName" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %>) <span class="red-star"></span>: </span></label>
                                    <p class="h6">(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Last Name)</p>
                                </div>
                                <input id="iptParentLastname" name="iptParentLastname" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <!---->
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104038") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131001") %>) <span class="red-star"></span>: </span></label>
                                    <p class="h6">(English Name)</p>
                                </div>
                                <input id="iptParentNameEn" name="iptParentNameEn" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101070") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(English Last Name)</p>
                                </div>
                                <input id="iptParentLastnameEn" name="iptParentLastnameEn" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <!---->
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Date of Birth)</p>
                                </div>
                                <input id="iptParentBirthday" name="iptParentBirthday" type="text" class="form-control datetimepicker" maxlength="10">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Citizen ID No.)</p>
                                </div>
                                <input id="iptParentIDCard" name="iptParentIDCard" type="text" class="form-control" maxlength="13">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Race)</p>
                                </div>
                                <%--<input id="iptParentRace" name="iptParentRace" type="text" class="form-control" maxlength="50">--%>
                                <select id="sltParentRace" name="sltParentRace" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %></option>
                                    <asp:Literal ID="ltrParentRace" runat="server" />
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Nationality)</p>
                                </div>
                                <%--<input id="iptParentNation" name="iptParentNation" type="text" class="form-control" maxlength="50">--%>
                                <select id="sltParentNation" name="sltParentNation" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %></option>
                                    <asp:Literal ID="ltrParentNation" runat="server" />
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Religion)</p>
                                </div>
                                <%--<input id="iptParentReligion" name="iptParentReligion" type="text" class="form-control" maxlength="50">--%>
                                <select id="sltParentReligion" name="sltParentReligion" class="selectpicker" data-live-search="false" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %></option>
                                    <asp:Literal ID="ltrParentReligion" runat="server" />
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Degree of Education)</p>
                                </div>
                                <select id="sltParentEducational" name="sltParentEducational" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
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
                            <div class="col-md-6 ml-auto mr-auto">
                                <div class="div-bag div-bag-select">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101201") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Family Status)</p>
                                </div>
                                <select id="sltParentStatus" name="sltParentStatus" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101203") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101204") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101205") %></option>
                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101206") %></option>
                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103105") %></option>
                                    <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101207") %></option>
                                    <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101208") %></option>
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Address No.)</p>
                                </div>
                                <input id="iptParentHomeNumber" name="iptParentHomeNumber" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Soi)</p>
                                </div>
                                <input id="iptParentAlley" name="iptParentAlley" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Moo)</p>
                                </div>
                                <input id="iptParentVillageNo" name="iptParentVillageNo" type="text" class="form-control" maxlength="50">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Street)</p>
                                </div>
                                <input id="iptParentRoad" name="iptParentRoad" type="text" class="form-control" maxlength="50">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Province)</p>
                                </div>
                                <select id="sltParentProvince" name="sltParentProvince" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                    <asp:Literal ID="ltrProvince" runat="server" />
                                </select>
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(District)</p>
                                </div>
                                <select id="sltParentDistrict" name="sltParentDistrict" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Sub District)</p>
                                </div>
                                <select id="sltParentSubDistrict" name="sltParentSubDistrict" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title=" ">
                                </select>

                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Post Code)</p>
                                </div>
                                <input id="iptParentPostalCode" name="iptParentPostalCode" type="text" class="form-control" maxlength="5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Occupation)</p>
                                </div>
                                <input id="iptParentCareer" name="iptParentCareer" type="text" class="form-control" maxlength="200">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Company's Name)</p>
                                </div>
                                <input id="iptParentWorkplace" name="iptParentWorkplace" type="text" class="form-control" maxlength="200">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Monthly Income)</p>
                                </div>
                                <input id="iptParentMonthIncome" name="iptParentMonthIncome" type="text" class="form-control" maxlength="15">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-select2">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104045") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Yearly Income)</p>
                                </div>
                                <select id="sltParentYearIncome" name="sltParentYearIncome" class="selectpicker" data-style="select-with-transition" data-width="100%" title=" ">
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
                                <input id="iptParentHousePhone" name="iptParentHousePhone" type="text" class="form-control" maxlength="30">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Mobile Phone)</p>
                                </div>
                                <input id="iptParentMobilePhone" name="iptParentMobilePhone" type="text" class="form-control" maxlength="30">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ml-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Work Phone Number)</p>
                                </div>
                                <input id="iptParentWorkPhone" name="iptParentWorkPhone" type="text" class="form-control" maxlength="30">
                            </div>
                            <div class="col-md-3 mr-auto col-required-field">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(E-Mail)</p>
                                </div>
                                <input id="iptParentEmail" name="iptParentEmail" type="text" class="form-control" maxlength="100">
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

                if (preRegister.Page09Saved) {
                    var isDisabled = true;
                    if (preRegister.ParentRelation == '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101195") %>' || preRegister.ParentRelation == '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101196") %>') {
                        isDisabled = true;

                        $("#sltParentRelation").selectpicker('val', preRegister.ParentRelation);
                        $('#iptParentRelation').val('').prop('disabled', isDisabled);
                    }
                    else {
                        isDisabled = false;

                        $("#sltParentRelation").selectpicker('val', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %>');
                        $('#iptParentRelation').val(preRegister.ParentRelation).attr('old-data', preRegister.ParentRelation).prop('disabled', isDisabled);
                    }
                    $('#sltParentTitle').selectpicker('val', preRegister.ParentTitle).prop('disabled', isDisabled).selectpicker('refresh');
                    $('#iptParentName').val(preRegister.ParentName).prop('disabled', isDisabled);
                    $('#iptParentLastname').val(preRegister.ParentLastname).prop('disabled', isDisabled);
                    $('#iptParentNameEn').val(preRegister.ParentNameEn).prop('disabled', isDisabled);
                    $('#iptParentLastnameEn').val(preRegister.ParentLastnameEn).prop('disabled', isDisabled);
                    $('#iptParentBirthday').val(preRegister.ParentBirthday).prop('disabled', isDisabled);
                    $('#iptParentIDCard').val(preRegister.ParentIDCard).prop('disabled', isDisabled);
                    //$('#iptParentRace').val(preRegister.ParentRace).prop('disabled', isDisabled);
                    $('#sltParentRace').selectpicker('val', preRegister.ParentRace).prop('disabled', isDisabled).selectpicker('refresh');
                    //$('#iptParentNation').val(preRegister.ParentNation).prop('disabled', isDisabled);
                    $('#sltParentNation').selectpicker('val', preRegister.ParentNation).prop('disabled', isDisabled).selectpicker('refresh');
                    //$('#iptParentReligion').val(preRegister.ParentReligion).prop('disabled', isDisabled);
                    $('#sltParentReligion').selectpicker('val', preRegister.ParentReligion).prop('disabled', isDisabled).selectpicker('refresh');
                    $('#sltParentEducational').selectpicker('val', preRegister.ParentEducational).prop('disabled', isDisabled).selectpicker('refresh');
                    $('#sltParentStatus').selectpicker('val', preRegister.ParentStatus); //.prop('disabled', isDisabled).selectpicker('refresh');
                    $('#iptParentHomeNumber').val(preRegister.ParentHomeNumber).prop('disabled', isDisabled);
                    $('#iptParentAlley').val(preRegister.ParentAlley).prop('disabled', isDisabled);
                    $('#iptParentVillageNo').val(preRegister.ParentVillageNo).prop('disabled', isDisabled);
                    $('#iptParentRoad').val(preRegister.ParentRoad).prop('disabled', isDisabled);
                    $('#sltParentProvince').selectpicker('val', preRegister.ParentProvince).prop('disabled', isDisabled).selectpicker('refresh');
                    LoadDistrict($("#sltParentProvince").val());

                    $('#sltParentDistrict').selectpicker('val', preRegister.ParentDistrict).prop('disabled', isDisabled).selectpicker('refresh');
                    LoadSubDistrict($("#sltParentDistrict").val());

                    $('#sltParentSubDistrict').selectpicker('val', preRegister.ParentSubDistrict).prop('disabled', isDisabled).selectpicker('refresh');
                    $('#iptParentPostalCode').val(preRegister.ParentPostalCode).prop('disabled', isDisabled);
                    $('#iptParentCareer').val(preRegister.ParentCareer).prop('disabled', isDisabled);
                    $('#iptParentWorkplace').val(preRegister.ParentWorkplace).prop('disabled', isDisabled);
                    $('#iptParentMonthIncome').val(preRegister.ParentMonthIncome).prop('disabled', isDisabled);
                    $('#sltParentYearIncome').selectpicker('val', preRegister.ParentYearIncome).prop('disabled', isDisabled).selectpicker('refresh');
                    $('#iptParentHousePhone').val(preRegister.ParentHousePhone).prop('disabled', isDisabled);
                    $('#iptParentMobilePhone').val(preRegister.ParentMobilePhone).prop('disabled', isDisabled);
                    $('#iptParentWorkPhone').val(preRegister.ParentWorkPhone).prop('disabled', isDisabled);
                    $('#iptParentEmail').val(preRegister.ParentEmail).prop('disabled', isDisabled);

                    if (isDisabled) {
                        $('.dropdown.bootstrap-select.disabled').parent().css('cursor', 'not-allowed');
                    }
                    else {
                        $('.dropdown.bootstrap-select').parent().css('cursor', 'default');
                    }
                }
            } else {
                // No web storage Support.
            }
        }

        function SaveDataToLocalStorage(callbackFunction) {
            if ($("#form").valid()) {

                preRegister.ParentRelation = ($("#sltParentRelation").val() == '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %>') ? $('#iptParentRelation').val() : $("#sltParentRelation").val();
                preRegister.ParentTitle = $('#sltParentTitle').val();
                preRegister.ParentTitleText = $('#sltParentTitle').find(':selected').text();
                preRegister.ParentName = $('#iptParentName').val();
                preRegister.ParentLastname = $('#iptParentLastname').val();
                preRegister.ParentNameEn = $('#iptParentNameEn').val();
                preRegister.ParentLastnameEn = $('#iptParentLastnameEn').val();
                preRegister.ParentBirthday = $('#iptParentBirthday').val();
                preRegister.ParentIDCard = $('#iptParentIDCard').val();
                //preRegister.ParentRace = $('#iptParentRace').val();
                preRegister.ParentRace = $('#sltParentRace').val();
                preRegister.ParentRaceText = $('#sltParentRace').find(':selected').text();
                //preRegister.ParentNation = $('#iptParentNation').val();
                preRegister.ParentNation = $('#sltParentNation').val();
                preRegister.ParentNationText = $('#sltParentNation').find(':selected').text();
                //preRegister.ParentReligion = $('#iptParentReligion').val();
                preRegister.ParentReligion = $('#sltParentReligion').val();
                preRegister.ParentReligionText = $('#sltParentReligion').find(':selected').text();
                preRegister.ParentEducational = $('#sltParentEducational').val();
                preRegister.ParentEducationalText = $('#sltParentEducational').find(':selected').text();
                preRegister.ParentStatus = $('#sltParentStatus').val();
                preRegister.ParentStatusText = $('#sltParentStatus').find(':selected').text();
                preRegister.ParentHomeNumber = $('#iptParentHomeNumber').val();
                preRegister.ParentAlley = $('#iptParentAlley').val();
                preRegister.ParentVillageNo = $('#iptParentVillageNo').val();
                preRegister.ParentRoad = $('#iptParentRoad').val();
                preRegister.ParentProvince = $('#sltParentProvince').val();
                preRegister.ParentProvinceText = $('#sltParentProvince').find(':selected').text();
                preRegister.ParentDistrict = $('#sltParentDistrict').val();
                preRegister.ParentDistrictText = $('#sltParentDistrict').find(':selected').text();
                preRegister.ParentSubDistrict = $('#sltParentSubDistrict').val();
                preRegister.ParentSubDistrictText = $('#sltParentSubDistrict').find(':selected').text();
                preRegister.ParentPostalCode = $('#iptParentPostalCode').val();
                preRegister.ParentCareer = $('#iptParentCareer').val();
                preRegister.ParentWorkplace = $('#iptParentWorkplace').val();
                preRegister.ParentMonthIncome = $('#iptParentMonthIncome').val();
                preRegister.ParentYearIncome = $('#sltParentYearIncome').val();
                preRegister.ParentYearIncomeText = $('#sltParentYearIncome').find(':selected').text();
                preRegister.ParentHousePhone = $('#iptParentHousePhone').val();
                preRegister.ParentMobilePhone = $('#iptParentMobilePhone').val();
                preRegister.ParentWorkPhone = $('#iptParentWorkPhone').val();
                preRegister.ParentEmail = $('#iptParentEmail').val();

                preRegister.Page09Saved = true;

                // Save data to localStorage
                if (ls.isBrowserSupport()) {
                    // Code for localStorage
                    ls.setItem('preRegister', preRegister);
                } else {
                    // No web storage Support.
                }

                ez.activePageComplete(9);

                callbackFunction();

            }
        }

        function LoadDistrict(provinceID) {
            if (provinceID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline09.aspx/LoadDistrict",
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

            $('#sltParentDistrict').empty();

            var options = '';
            $(plans).each(function () {

                options += '<option value="' + this.AMPHUR_ID + '">' + this.AMPHUR_NAME + '</option>';

            });

            $('#sltParentDistrict').html(options);

            //$('#sltParentDistrict').prop('disabled', false);
            $('#sltParentDistrict').selectpicker('refresh');
        }

        function LoadSubDistrict(districtID) {
            if (districtID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline09.aspx/LoadSubDistrict",
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

            $('#sltParentSubDistrict').empty();

            var options = '';
            $(plans).each(function () {

                options += '<option value="' + this.DISTRICT_ID + '">' + this.DISTRICT_NAME + '</option>';

            });

            $('#sltParentSubDistrict').html(options);

            //$('#sltParentSubDistrict').prop('disabled', false);
            $('#sltParentSubDistrict').selectpicker('refresh');
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

            ez.initFormExtendedDatetimepickersDisableFutureDate('#iptParentBirthday');

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
                    sltParentRelation: { required: false },
                    iptParentRelation: {
                        required: false
                        //equalTo: {
                        //    param: '#sltParentRelation',
                        //    depends: function (element) {
                        //        return $("#sltParentRelation").val() == '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %>' && (!$(element).val() || $(element).val() == '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101195") %>' || $(element).val() == '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101196") %>');
                        //    }
                        //}
                    },
                    sltParentTitle: { required: false },
                    iptParentName: { required: false },
                    iptParentLastname: { required: false },
                    iptParentNameEn: { required: false },
                    iptParentLastnameEn: { required: false },
                    iptParentBirthday: {
                        required: false,
                        thaiDate: true,
                        disableFutureDate: true
                    },
                    iptParentIDCard: {
                        required: false,
                        minlength: 7,
                        maxlength: 13
                    },
                    sltParentRace: { required: false },
                    sltParentNation: { required: false },
                    sltParentReligion: { required: false },
                    sltParentEducational: { required: false },
                    sltParentStatus: { required: false },
                    iptParentHomeNumber: { required: false },
                    iptParentAlley: { required: false },
                    iptParentVillageNo: { required: false },
                    iptParentRoad: { required: false },
                    sltParentProvince: { required: false },
                    sltParentDistrict: { required: false },
                    sltParentSubDistrict: { required: false },
                    iptParentPostalCode: {
                        required: false,
                        number: true
                    },
                    iptParentCareer: { required: false },
                    iptParentWorkplace: { required: false },
                    iptParentMonthIncome: {
                        required: false,
                        number: true
                    },
                    sltParentYearIncome: { required: false },
                    iptParentHousePhone: {
                        required: false,
                        number: true
                    },
                    iptParentMobilePhone: {
                        required: false,
                        number: true,
                        minlength: 10,
                        maxlength: 10
                    },
                    iptParentWorkPhone: {
                        required: false,
                        number: true
                    },
                    iptParentEmail: {
                        required: false,
                        email: true
                    }
                },
                messages: {
                    sltParentRelation: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentRelation: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                        /*equalTo: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132856") %>"*/
                    },
                    sltParentTitle: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentLastname: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentNameEn: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentLastnameEn: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentBirthday: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                    },
                    iptParentIDCard: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        minlength: jQuery.validator.format("Please, at least {0} characters are necessary."),
                        maxlength: jQuery.validator.format("Please, at most {0} characters are necessary.")
                    },
                    sltParentRace: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltParentNation: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltParentReligion: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltParentEducational: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltParentStatus: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentHomeNumber: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentAlley: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentVillageNo: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentRoad: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltParentProvince: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltParentDistrict: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltParentSubDistrict: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentPostalCode: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    iptParentCareer: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentWorkplace: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentMonthIncome: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    sltParentYearIncome: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptParentHousePhone: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    iptParentMobilePhone: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>",
                        minlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132848") %>",
                        maxlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132849") %>"
                    },
                    iptParentWorkPhone: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    iptParentEmail: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        email: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132850") %>"
                    }
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "sltParentRelation":
                        case "sltParentTitle":
                        case "sltParentEducational":
                        case "sltParentStatus":
                        case "sltParentProvince":
                        case "sltParentDistrict":
                        case "sltParentSubDistrict":
                        case "sltParentYearIncome":
                        case "sltParentNation":
                        case "sltParentReligion":
                        case "sltParentRace": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            $("#btnBack").bind({
                click: function () {

                    window.location.href = "RegisterOnline08.aspx";

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

                        window.location.href = "RegisterOnline10.aspx";

                    });

                    return false;
                }
            });

            $("#sltParentProvince").change(function () {

                LoadDistrict($("#sltParentProvince").val());

                $('#sltParentSubDistrict').empty();
                //$('#sltParentSubDistrict').prop('disabled', true);
                $('#sltParentSubDistrict').selectpicker('refresh');
            });

            $("#sltParentDistrict").change(function () {

                LoadSubDistrict($("#sltParentDistrict").val());

            });

            $("#sltParentRelation").change(function () {
                if ($("#sltParentRelation").val() == '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %>') {
                    $('#iptParentRelation').prop('disabled', false).val($('#iptParentRelation').attr('old-data'));

                    $('#sltParentTitle').prop('disabled', false).val('default').selectpicker("refresh");
                    $('#iptParentName').prop('disabled', false).val('');
                    $('#iptParentLastname').prop('disabled', false).val('');
                    $('#iptParentNameEn').prop('disabled', false).val('');
                    $('#iptParentLastnameEn').prop('disabled', false).val('');
                    $('#iptParentBirthday').prop('disabled', false).val('');
                    $('#iptParentIDCard').prop('disabled', false).val('');
                    //$('#iptParentRace').prop('disabled', false).val('');
                    $('#sltParentRace').prop('disabled', false).val('default').selectpicker("refresh");
                    //$('#iptParentNation').prop('disabled', false).val('');
                    $('#sltParentNation').prop('disabled', false).val('default').selectpicker("refresh");
                    //$('#iptParentReligion').prop('disabled', false).val('');
                    $('#sltParentReligion').prop('disabled', false).val('default').selectpicker("refresh");
                    $('#sltParentEducational').prop('disabled', false).val('default').selectpicker("refresh");
                    //$('#sltParentStatus').prop('disabled', false).val('default').selectpicker("refresh");
                    $('#iptParentHomeNumber').prop('disabled', false).val('');
                    $('#iptParentAlley').prop('disabled', false).val('');
                    $('#iptParentVillageNo').prop('disabled', false).val('');
                    $('#iptParentRoad').prop('disabled', false).val('');
                    $('#sltParentProvince').prop('disabled', false).val('default').selectpicker("refresh");
                    $('#sltParentDistrict').prop('disabled', false).empty().val('default').selectpicker("refresh");
                    $('#sltParentSubDistrict').prop('disabled', false).empty().val('default').selectpicker("refresh");
                    $('#iptParentPostalCode').prop('disabled', false).val('');
                    $('#iptParentCareer').prop('disabled', false).val('');
                    $('#iptParentWorkplace').prop('disabled', false).val('');
                    $('#iptParentMonthIncome').prop('disabled', false).val('');
                    $('#sltParentYearIncome').prop('disabled', false).val('default').selectpicker("refresh");
                    $('#iptParentHousePhone').prop('disabled', false).val('');
                    $('#iptParentMobilePhone').prop('disabled', false).val('');
                    $('#iptParentWorkPhone').prop('disabled', false).val('');
                    $('#iptParentEmail').prop('disabled', false).val('');

                    $('.dropdown.bootstrap-select').parent().css('cursor', 'default');
                }
                else {
                    $('#iptParentRelation').val('').prop('disabled', true);

                    switch ($("#sltParentRelation").val()) {
                        case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101195") %>':

                            $('#sltParentTitle').selectpicker('val', preRegister.FatherTitle).prop('disabled', true).selectpicker('refresh');
                            $('#iptParentName').val(preRegister.FatherName).prop('disabled', true);
                            $('#iptParentLastname').val(preRegister.FatherLastname).prop('disabled', true);
                            $('#iptParentNameEn').val(preRegister.FatherNameEn).prop('disabled', true);
                            $('#iptParentLastnameEn').val(preRegister.FatherLastnameEn).prop('disabled', true);
                            $('#iptParentBirthday').val(preRegister.FatherBirthday).prop('disabled', true);
                            $('#iptParentIDCard').val(preRegister.FatherIDCard).prop('disabled', true);
                            //$('#iptParentRace').val(preRegister.FatherRace).prop('disabled', true);
                            $('#sltParentRace').selectpicker('val', preRegister.FatherRace).prop('disabled', true).selectpicker('refresh');
                            //$('#iptParentNation').val(preRegister.FatherNation).prop('disabled', true);
                            $('#sltParentNation').selectpicker('val', preRegister.FatherNation).prop('disabled', true).selectpicker('refresh');
                            //$('#iptParentReligion').val(preRegister.FatherReligion).prop('disabled', true);
                            $('#sltParentReligion').selectpicker('val', preRegister.FatherReligion).prop('disabled', true).selectpicker('refresh');
                            $('#sltParentEducational').selectpicker('val', preRegister.FatherEducational).prop('disabled', true).selectpicker('refresh');
                            $('#sltParentStatus').selectpicker('val', preRegister.ParentStatus); //.prop('disabled', true).selectpicker('refresh');
                            $('#iptParentHomeNumber').val(preRegister.FatherHomeNumber).prop('disabled', true);
                            $('#iptParentAlley').val(preRegister.FatherAlley).prop('disabled', true);
                            $('#iptParentVillageNo').val(preRegister.FatherVillageNo).prop('disabled', true);
                            $('#iptParentRoad').val(preRegister.FatherRoad).prop('disabled', true);
                            $('#sltParentProvince').selectpicker('val', preRegister.FatherProvince).prop('disabled', true).selectpicker('refresh');
                            LoadDistrict($("#sltParentProvince").val());

                            $('#sltParentDistrict').selectpicker('val', preRegister.FatherDistrict).prop('disabled', true).selectpicker('refresh');
                            LoadSubDistrict($("#sltParentDistrict").val());

                            $('#sltParentSubDistrict').selectpicker('val', preRegister.FatherSubDistrict).prop('disabled', true).selectpicker('refresh');
                            $('#iptParentPostalCode').val(preRegister.FatherPostalCode).prop('disabled', true);
                            $('#iptParentCareer').val(preRegister.FatherCareer).prop('disabled', true);
                            $('#iptParentWorkplace').val(preRegister.FatherWorkplace).prop('disabled', true);
                            //$('#iptParentMonthIncome').val(preRegister.FatherMonthIncome).prop('disabled', true);
                            if (!$.isEmpty(preRegister.FatherMonthIncome)) {
                                $('#iptParentMonthIncome').val(preRegister.FatherMonthIncome);
                            }
                            $('#iptParentMonthIncome').prop('disabled', true);
                            $('#sltParentYearIncome').selectpicker('val', preRegister.FatherYearIncome).prop('disabled', true).selectpicker('refresh');
                            $('#iptParentHousePhone').val(preRegister.FatherHousePhone).prop('disabled', true);
                            $('#iptParentMobilePhone').val(preRegister.FatherMobilePhone).prop('disabled', true);
                            $('#iptParentWorkPhone').val(preRegister.FatherWorkPhone).prop('disabled', true);
                            $('#iptParentEmail').val(preRegister.FatherEmail).prop('disabled', true);

                            break;
                        case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101196") %>':

                            $('#sltParentTitle').selectpicker('val', preRegister.MotherTitle).prop('disabled', true).selectpicker('refresh');
                            $('#iptParentName').val(preRegister.MotherName).prop('disabled', true);
                            $('#iptParentLastname').val(preRegister.MotherLastname).prop('disabled', true);
                            $('#iptParentNameEn').val(preRegister.MotherNameEn).prop('disabled', true);
                            $('#iptParentLastnameEn').val(preRegister.MotherLastnameEn).prop('disabled', true);
                            $('#iptParentBirthday').val(preRegister.MotherBirthday).prop('disabled', true);
                            $('#iptParentIDCard').val(preRegister.MotherIDCard).prop('disabled', true);
                            //$('#iptParentRace').val(preRegister.MotherRace).prop('disabled', true);
                            $('#sltParentRace').selectpicker('val', preRegister.MotherRace).prop('disabled', true).selectpicker('refresh');
                            //$('#iptParentNation').val(preRegister.MotherNation).prop('disabled', true);
                            $('#sltParentNation').selectpicker('val', preRegister.MotherNation).prop('disabled', true).selectpicker('refresh');
                            //$('#iptParentReligion').val(preRegister.MotherReligion).prop('disabled', true);
                            $('#sltParentReligion').selectpicker('val', preRegister.MotherReligion).prop('disabled', true).selectpicker('refresh');
                            $('#sltParentEducational').selectpicker('val', preRegister.MotherEducational).prop('disabled', true).selectpicker('refresh');
                            $('#sltParentStatus').selectpicker('val', preRegister.ParentStatus); //.prop('disabled', true).selectpicker('refresh');
                            $('#iptParentHomeNumber').val(preRegister.MotherHomeNumber).prop('disabled', true);
                            $('#iptParentAlley').val(preRegister.MotherAlley).prop('disabled', true);
                            $('#iptParentVillageNo').val(preRegister.MotherVillageNo).prop('disabled', true);
                            $('#iptParentRoad').val(preRegister.MotherRoad).prop('disabled', true);
                            $('#sltParentProvince').selectpicker('val', preRegister.MotherProvince).prop('disabled', true).selectpicker('refresh');
                            LoadDistrict($("#sltParentProvince").val());

                            $('#sltParentDistrict').selectpicker('val', preRegister.MotherDistrict).prop('disabled', true).selectpicker('refresh');
                            LoadSubDistrict($("#sltParentDistrict").val());

                            $('#sltParentSubDistrict').selectpicker('val', preRegister.MotherSubDistrict).prop('disabled', true).selectpicker('refresh');
                            $('#iptParentPostalCode').val(preRegister.MotherPostalCode).prop('disabled', true);
                            $('#iptParentCareer').val(preRegister.MotherCareer).prop('disabled', true);
                            $('#iptParentWorkplace').val(preRegister.MotherWorkplace).prop('disabled', true);
                            //$('#iptParentMonthIncome').val(preRegister.MotherMonthIncome).prop('disabled', true);
                            if (!$.isEmpty(preRegister.MotherMonthIncome)) {
                                $('#iptParentMonthIncome').val(preRegister.MotherMonthIncome);
                            }
                            $('#iptParentMonthIncome').prop('disabled', true);
                            $('#sltParentYearIncome').selectpicker('val', preRegister.MotherYearIncome).prop('disabled', true).selectpicker('refresh');
                            $('#iptParentHousePhone').val(preRegister.MotherHousePhone).prop('disabled', true);
                            $('#iptParentMobilePhone').val(preRegister.MotherMobilePhone).prop('disabled', true);
                            $('#iptParentWorkPhone').val(preRegister.MotherWorkPhone).prop('disabled', true);
                            $('#iptParentEmail').val(preRegister.MotherEmail).prop('disabled', true);

                            break;
                    }

                    $('.dropdown.bootstrap-select.disabled').parent().css('cursor', 'not-allowed');

                    $("#form").valid();
                }
            });

            // Init disable control
            //if ($.isEmpty($('#sltParentDistrict').val())) {
            //    $('#sltParentDistrict').prop('disabled', true);
            //    $('#sltParentDistrict').selectpicker('refresh');
            //}

            //if ($.isEmpty($('#sltParentSubDistrict').val())) {
            //    $('#sltParentSubDistrict').prop('disabled', true);
            //    $('#sltParentSubDistrict').selectpicker('refresh');
            //}

            LoadDataFromLocalStorage();

            $('#iptParentMonthIncome').number(true, 2);

            if ($.isEmpty(preRegister.ParentBirthday)) {
                $('#iptParentBirthday').val('');
            }
        });
    </script>
    <asp:Literal ID="ltrScriptRequiredField" runat="server"></asp:Literal>
</asp:Content>
