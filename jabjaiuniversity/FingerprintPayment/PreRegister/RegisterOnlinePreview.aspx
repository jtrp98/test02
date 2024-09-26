<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterOnlinePreview.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnlinePreview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStyle" runat="server">
    <style>
        /* Preview CSS*/
        div p.user-data {
            margin: 0px 0px 5px 0px;
            font-size: 16px;
            border-bottom: 1px solid #d2d2d2;
            font-weight: bold;
            height: 25px;
        }

        .form-check .form-check-input[disabled] ~ .check, .form-check .form-check-input[disabled] ~ .circle {
            opacity: 1;
        }

        .form-check .form-check-input[disabled] ~ .circle {
            border-color: #0000003F;
        }

        /* --- */

        .card .card-body .col-form-label, .card .card-body .label-on-right {
            padding: 12px 5px 0 0;
        }

        .card .card-body .form-group {
            margin: 0px 0 0;
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
            text-align: left;
        }

        input[type="file"] {
            /*display: none;*/
            visibility: collapse;
            position: absolute;
            width: 0%;
        }

        .no-file {
            display: none;
        }

        .card .card-body .col-form-label {
            text-align: left;
        }

            .card .card-body .col-form-label.multi-file {
                padding: 5px 0px 0px 30px;
            }


        .col-form-label {
            padding: 0px;
            margin: 0px;
        }

            .col-form-label span {
                vertical-align: -webkit-baseline-middle;
                font-size: 1em;
                color: #707070;
                font-weight: bold;
            }

                .col-form-label span.ready {
                    display: inline-block;
                    color: #23A818;
                    font-size: 26px;
                    margin-top: -19px;
                }

                .col-form-label span.open-file {
                    color: orange;
                    font-size: 30px;
                    margin-top: -19px;
                    cursor: pointer;
                }

                .col-form-label span.upload-text {
                    vertical-align: bottom;
                }

        .table .thead-light th {
            font-weight: bold;
        }

        .table.table-striped tbody tr td, .table.table-striped tbody tr th {
            text-align: center;
        }

            .table.table-striped tbody tr td:nth-child(2) {
                text-align: left;
                font-weight: bold;
            }

            .table.table-striped tbody tr td .col-form-label {
                text-align: right;
                padding: 5px 5px 0 0;
            }

        .no-assumption-sriracha, .no-suankularb-nonthaburi, .row-fileinput {
            display: none;
        }
        
        .row-fileinput.enable {
            display: revert;
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

        .label-bag {
            color: #000;
            font-weight: bold;
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
    <%--RegisterOnline04--%>
    <div class="row">
        <div class="col-md-12">
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
                    <div class="row">
                        <div class="col-md-5 ml-auto mr-auto card-wizard" data-color="rose">
                            <div class="picture-container">
                                <div class="picture">
                                    <img src="assets/img/default-avatar.png" class="picture-src" id="profilePicturePreview" title="" />
                                </div>
                                <h6 class="description" style="margin-top: 15px; color: #707070;"></h6>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <br>
                        <br>
                    </div>
                    <div class="row col-required-field">
                        <div class="col-md-2 ml-auto">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Student Type)</p>
                            </div>
                        </div>
                        <div class="col-md-4 mr-auto">
                            <div class="form-check form-check-inline">
                                <label class="form-check-label label-bag" style="padding-right: 40px; color: #707070;">
                                    <input class="form-check-input" type="radio" name="iptStudentCategory" value="1" checked disabled>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101122") %><p class="h6">(Day - School)</p>
                                    <span class="circle">
                                        <span class="check"></span>
                                    </span>
                                </label>
                            </div>
                            <div class="form-check form-check-inline">
                                <label class="form-check-label label-bag" style="color: #707070;">
                                    <input class="form-check-input" type="radio" name="iptStudentCategory" value="2" disabled>
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
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Prefixes)</p>
                            </div>
                            <div>
                                <p id="pStudentTitle" data-input="sltStudentTitle" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132842") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Name)</p>
                            </div>
                            <div>
                                <p id="pStudentName" data-input="iptStudentName" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %>) <span class="red-star"></span>: </span></label>
                                <p class="h6">(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Last Name)</p>
                            </div>
                            <div>
                                <p id="pStudentLastname" data-input="iptStudentLastname" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132843") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(English Name)</p>
                            </div>
                            <div>
                                <p id="pStudentNameEn" data-input="iptStudentNameEn" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101070") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(English Last Name)</p>
                            </div>
                            <div>
                                <p id="pStudentLastnameEn" data-input="iptStudentLastnameEn" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Nickname)</p>
                            </div>
                            <div>
                                <p id="pStudentNickName" data-input="iptStudentNickName" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %>(Eng) <span class="red-star"></span>: </span></label>
                                <p class="h6">(Eng Nickname)</p>
                            </div>
                            <div>
                                <p id="pStudentNickNameEn" data-input="iptStudentNickNameEn" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Gender)</p>
                            </div>
                            <div>
                                <p id="pStudentSex" data-input="sltStudentSex" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Date of Birth)</p>
                            </div>
                            <div>
                                <p id="pStudentBirthday" data-input="iptStudentBirthday" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Citizen ID No.)</p>
                            </div>
                            <div>
                                <p id="pStudentIDCard" data-input="iptStudentIDCard" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Race)</p>
                            </div>
                            <div>
                                <p id="pStudentRace" data-input="sltStudentRace" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Nationality)</p>
                            </div>
                            <div>
                                <p id="pStudentNation" data-input="sltStudentNation" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Religion)</p>
                            </div>
                            <div>
                                <p id="pStudentReligion" data-input="sltStudentReligion" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132844") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Siblings)</p>
                            </div>
                            <div>
                                <p id="pStudentSonTotal" data-input="iptStudentSonTotal" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101113") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Your Order Among Sibling)</p>
                            </div>
                            <div>
                                <p id="pStudentSonNumber" data-input="iptStudentSonNumber" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <br>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--RegisterOnline05--%>
    <div class="row">
        <div class="col-md-12">
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
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101130") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(House Code)</p>
                            </div>
                            <div>
                                <p id="pHouseCode" data-input="iptHouseCode" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Address No.)</p>
                            </div>
                            <div>
                                <p id="pHouseHomeNumber" data-input="iptHouseHomeNumber" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Soi)</p>
                            </div>
                            <div>
                                <p id="pHouseAlley" data-input="iptHouseAlley" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Moo)</p>
                            </div>
                            <div>
                                <p id="pHouseVillageNo" data-input="iptHouseVillageNo" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Street)</p>
                            </div>
                            <div>
                                <p id="pHouseRoad" data-input="iptHouseRoad" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Province)</p>
                            </div>
                            <div>
                                <p id="pHouseProvince" data-input="sltHouseProvince" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(District)</p>
                            </div>
                            <div>
                                <p id="pHouseDistrict" data-input="sltHouseDistrict" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Sub District)</p>
                            </div>
                            <div>
                                <p id="pHouseSubDistrict" data-input="sltHouseSubDistrict" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Post Code)</p>
                            </div>
                            <div>
                                <p id="pHousePostalCode" data-input="iptHousePostalCode" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Mobile Phone)</p>
                            </div>
                            <div>
                                <p id="pHousePhone" data-input="iptHousePhone" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(E-Mail)</p>
                            </div>
                            <div>
                                <p id="pHouseEmail" data-input="iptHouseEmail" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <br>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--RegisterOnline06--%>
    <div class="row">
        <div class="col-md-12">
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
                                    <input class="form-check-input" type="radio" name="iptSameHouseAddress" value="same" disabled>
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
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Address No.)</p>
                            </div>
                            <div>
                                <p id="pStudentHomeNumber" data-input="iptStudentHomeNumber" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Soi)</p>
                            </div>
                            <div>
                                <p id="pStudentAlley" data-input="iptStudentAlley" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Moo)</p>
                            </div>
                            <div>
                                <p id="pStudentVillageNo" data-input="iptStudentVillageNo" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Street)</p>
                            </div>
                            <div>
                                <p id="pStudentRoad" data-input="iptStudentRoad" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag div-bag-select2">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Province)</p>
                            </div>
                            <div>
                                <p id="pStudentProvince" data-input="sltStudentProvince" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(District)</p>
                            </div>
                            <div>
                                <p id="pStudentDistrict" data-input="sltStudentDistrict" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Sub District)</p>
                            </div>
                            <div>
                                <p id="pStudentSubDistrict" data-input="sltStudentSubDistrict" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Post Code)</p>
                            </div>
                            <div>
                                <p id="pStudentPostalCode" data-input="iptStudentPostalCode" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Mobile Phone)</p>
                            </div>
                            <div>
                                <p id="pStudentHousePhone" data-input="iptStudentHousePhone" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(E-Mail)</p>
                            </div>
                            <div>
                                <p id="pStudentHouseEmail" data-input="iptStudentHouseEmail" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <!---->
                    <div class="row">
                        <div class="col-md-6 ml-auto mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101149") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Stay with (Prefixes))</p>
                            </div>
                            <div>
                                <p id="pStudentStayWithTitle" data-input="sltStudentStayWithTitle" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101151") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Stay with (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Name))</p>
                            </div>
                            <div>
                                <p id="pStudentStayWithName" data-input="iptStudentStayWithName" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101152") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Stay with (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Last Name))</p>
                            </div>
                            <div>
                                <p id="pStudentStayWithLast" data-input="iptStudentStayWithLast" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101153") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Emergency contacts / phone)</p>
                            </div>
                            <div>
                                <p id="pStudentStayWithEmergencyCall" data-input="iptStudentStayWithEmergencyCall" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101158") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(House style)</p>
                            </div>
                            <div>
                                <p id="pStudentHomeType" data-input="sltStudentHomeType" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101155") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Neighbor (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Name))</p>
                            </div>
                            <div>
                                <p id="pStudentNeighborName" data-input="iptStudentNeighborName" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101156") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Neighbor (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Last Name))</p>
                            </div>
                            <div>
                                <p id="pStudentNeighborLastName" data-input="iptStudentNeighborLastName" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132851") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Neighbor (Education level))</p>
                            </div>
                            <div>
                                <p id="pStudentNeighborSubLevel" data-input="sltStudentNeighborSubLevel" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101157") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Neighbor (Mobile Phone))</p>
                            </div>
                            <div>
                                <p id="pStudentNeighborPhone" data-input="iptStudentNeighborPhone" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <!---->
                    <div class="row">
                        <br>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--RegisterOnline07--%>
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-rose card-header-icon">
                    <div class="card-icon text-center" style="border-radius: 12px; margin-left: 30px; margin-top: -30px;">
                        <h4 style="font-family: THSarabunNew; margin-bottom: 0px; padding: 3px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101180") %></h4>
                        <p class="h6 text-center">(Father information)</p>
                    </div>
                </div>
                <div class="card-body">
                    <%--<div class="row">
                        <div class="col-md-6 ml-auto mr-auto">
                            <span style="color: red;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132852") %></span>
                        </div>
                    </div>--%>
                    <div class="row">
                        <br>
                    </div>
                    <div class="row">
                        <div class="col-md-6 ml-auto mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Prefixes)</p>
                            </div>
                            <div>
                                <p id="pFatherTitle" data-input="sltFatherTitle" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211011") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Father's name)</p>
                            </div>
                            <div>
                                <p id="pFatherName" data-input="iptFatherName" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Last Name)</p>
                            </div>
                            <div>
                                <p id="pFatherLastname" data-input="iptFatherLastname" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132853") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Eng Father's name)</p>
                            </div>
                            <div>
                                <p id="pFatherNameEn" data-input="iptFatherNameEn" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>(Eng) <span class="red-star"></span>: </span></label>
                                <p class="h6">(Eng Last Name)</p>
                            </div>
                            <div>
                                <p id="pFatherLastnameEn" data-input="iptFatherLastnameEn" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Date of Birth)</p>
                            </div>
                            <div>
                                <p id="pFatherBirthday" data-input="iptFatherBirthday" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Citizen ID No.)</p>
                            </div>
                            <div>
                                <p id="pFatherIDCard" data-input="iptFatherIDCard" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Race)</p>
                            </div>
                            <div>
                                <p id="pFatherRace" data-input="sltFatherRace" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Nationality)</p>
                            </div>
                            <div>
                                <p id="pFatherNation" data-input="sltFatherNation" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Religion)</p>
                            </div>
                            <div>
                                <p id="pFatherReligion" data-input="sltFatherReligion" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Degree of Education)</p>
                            </div>
                            <div>
                                <p id="pFatherEducational" data-input="sltFatherEducational" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Address No.)</p>
                            </div>
                            <div>
                                <p id="pFatherHomeNumber" data-input="iptFatherHomeNumber" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Soi)</p>
                            </div>
                            <div>
                                <p id="pFatherAlley" data-input="iptFatherAlley" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Moo)</p>
                            </div>
                            <div>
                                <p id="pFatherVillageNo" data-input="iptFatherVillageNo" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Street)</p>
                            </div>
                            <div>
                                <p id="pFatherRoad" data-input="iptFatherRoad" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Province)</p>
                            </div>
                            <div>
                                <p id="pFatherProvince" data-input="sltFatherProvince" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(District)</p>
                            </div>
                            <div>
                                <p id="pFatherDistrict" data-input="sltFatherDistrict" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Sub District)</p>
                            </div>
                            <div>
                                <p id="pFatherSubDistrict" data-input="sltFatherSubDistrict" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Post Code)</p>
                            </div>
                            <div>
                                <p id="pFatherPostalCode" data-input="iptFatherPostalCode" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Occupation)</p>
                            </div>
                            <div>
                                <p id="pFatherCareer" data-input="iptFatherCareer" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Company's Name)</p>
                            </div>
                            <div>
                                <p id="pFatherWorkplace" data-input="iptFatherWorkplace" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Monthly Income)</p>
                            </div>
                            <div>
                                <p id="pFatherMonthIncome" data-input="iptFatherMonthIncome" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104045") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Yearly Income)</p>
                            </div>
                            <div>
                                <p id="pFatherYearIncome" data-input="sltFatherYearIncome" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Telephone Number)</p>
                            </div>
                            <div>
                                <p id="pFatherHousePhone" data-input="iptFatherHousePhone" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Mobile Phone)</p>
                            </div>
                            <div>
                                <p id="pFatherMobilePhone" data-input="iptFatherMobilePhone" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Work Phone Number)</p>
                            </div>
                            <div>
                                <p id="pFatherWorkPhone" data-input="iptFatherWorkPhone" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(E-Mail)</p>
                            </div>
                            <div>
                                <p id="pFatherEmail" data-input="iptFatherEmail" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <br>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--RegisterOnline08--%>
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-rose card-header-icon">
                    <div class="card-icon text-center" style="border-radius: 12px; margin-left: 30px; margin-top: -30px;">
                        <h4 style="font-family: THSarabunNew; margin-bottom: 0px; padding: 3px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00194") %></h4>
                        <p class="h6 text-center">(Mother information)</p>
                    </div>
                </div>
                <div class="card-body">
                    <%--<div class="row">
                        <div class="col-md-6 ml-auto mr-auto">
                            <span style="color: red;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132854") %></span>
                        </div>
                    </div>--%>
                    <div class="row">
                        <br>
                    </div>
                    <div class="row">
                        <div class="col-md-6 ml-auto mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Prefixes)</p>
                            </div>
                            <div>
                                <p id="pMotherTitle" data-input="sltMotherTitle" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104047") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Mother's Name)</p>
                            </div>
                            <div>
                                <p id="pMotherName" data-input="iptMotherName" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Last Name)</p>
                            </div>
                            <div>
                                <p id="pMotherLastname" data-input="iptMotherLastname" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104047") %>(Eng) <span class="red-star"></span>: </span></label>
                                <p class="h6">(Eng Mother's Name)</p>
                            </div>
                            <div>
                                <p id="pMotherNameEn" data-input="iptMotherNameEn" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>(Eng) <span class="red-star"></span>: </span></label>
                                <p class="h6">(Eng Last Name)</p>
                            </div>
                            <div>
                                <p id="pMotherLastnameEn" data-input="iptMotherLastnameEn" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Date of Birth)</p>
                            </div>
                            <div>
                                <p id="pMotherBirthday" data-input="iptMotherBirthday" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Citizen ID No.)</p>
                            </div>
                            <div>
                                <p id="pMotherIDCard" data-input="iptMotherIDCard" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Race)</p>
                            </div>
                            <div>
                                <p id="pMotherRace" data-input="sltMotherRace" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Nationality)</p>
                            </div>
                            <div>
                                <p id="pMotherNation" data-input="sltMotherNation" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Religion)</p>
                            </div>
                            <div>
                                <p id="pMotherReligion" data-input="sltMotherReligion" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Degree of Education)</p>
                            </div>
                            <div>
                                <p id="pMotherEducational" data-input="sltMotherEducational" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Address No.)</p>
                            </div>
                            <div>
                                <p id="pMotherHomeNumber" data-input="iptMotherHomeNumber" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Soi)</p>
                            </div>
                            <div>
                                <p id="pMotherAlley" data-input="iptMotherAlley" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Moo)</p>
                            </div>
                            <div>
                                <p id="pMotherVillageNo" data-input="iptMotherVillageNo" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Street)</p>
                            </div>
                            <div>
                                <p id="pMotherRoad" data-input="iptMotherRoad" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Province)</p>
                            </div>
                            <div>
                                <p id="pMotherProvince" data-input="sltMotherProvince" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(District)</p>
                            </div>
                            <div>
                                <p id="pMotherDistrict" data-input="sltMotherDistrict" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Sub District)</p>
                            </div>
                            <div>
                                <p id="pMotherSubDistrict" data-input="sltMotherSubDistrict" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Post Code)</p>
                            </div>
                            <div>
                                <p id="pMotherPostalCode" data-input="iptMotherPostalCode" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Occupation)</p>
                            </div>
                            <div>
                                <p id="pMotherCareer" data-input="iptMotherCareer" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Company's Name)</p>
                            </div>
                            <div>
                                <p id="pMotherWorkplace" data-input="iptMotherWorkplace" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Monthly Income)</p>
                            </div>
                            <div>
                                <p id="pMotherMonthIncome" data-input="iptMotherMonthIncome" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104045") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Yearly Income)</p>
                            </div>
                            <div>
                                <p id="pMotherYearIncome" data-input="sltMotherYearIncome" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Telephone Number)</p>
                            </div>
                            <div>
                                <p id="pMotherHousePhone" data-input="iptMotherHousePhone" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Mobile Phone)</p>
                            </div>
                            <div>
                                <p id="pMotherMobilePhone" data-input="iptMotherMobilePhone" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Work Phone Number)</p>
                            </div>
                            <div>
                                <p id="pMotherWorkPhone" data-input="iptMotherWorkPhone" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(E-Mail)</p>
                            </div>
                            <div>
                                <p id="pMotherEmail" data-input="iptMotherEmail" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <br>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--RegisterOnline09--%>
    <div class="row">
        <div class="col-md-12">
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
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101192") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Relationship)</p>
                            </div>
                            <div>
                                <p id="pParentRelation" data-input="sltParentRelation" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132855") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Relationship)</p>
                            </div>
                            <div>
                                <p id="pParentRelationOther" data-input="iptParentRelation" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 ml-auto mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Prefixes)</p>
                            </div>
                            <div>
                                <p id="pParentTitle" data-input="sltParentTitle" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104038") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %>) <span class="red-star"></span>: </span></label>
                                <p class="h6">(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Name)</p>
                            </div>
                            <div>
                                <p id="pParentName" data-input="iptParentName" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %>) <span class="red-star"></span>: </span></label>
                                <p class="h6">(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> Last Name)</p>
                            </div>
                            <div>
                                <p id="pParentLastname" data-input="iptParentLastname" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <!---->
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104038") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131001") %>) <span class="red-star"></span>: </span></label>
                                <p class="h6">(English Name)</p>
                            </div>
                            <div>
                                <p id="pParentNameEn" data-input="iptParentNameEn" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101070") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(English Last Name)</p>
                            </div>
                            <div>
                                <p id="pParentLastnameEn" data-input="iptParentLastnameEn" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <!---->
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Date of Birth)</p>
                            </div>
                            <div>
                                <p id="pParentBirthday" data-input="iptParentBirthday" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Citizen ID No.)</p>
                            </div>
                            <div>
                                <p id="pParentIDCard" data-input="iptParentIDCard" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Race)</p>
                            </div>
                            <div>
                                <p id="pParentRace" data-input="sltParentRace" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Nationality)</p>
                            </div>
                            <div>
                                <p id="pParentNation" data-input="sltParentNation" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Religion)</p>
                            </div>
                            <div>
                                <p id="pParentReligion" data-input="sltParentReligion" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Degree of Education)</p>
                            </div>
                            <div>
                                <p id="pParentEducational" data-input="sltParentEducational" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 ml-auto mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101201") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Family Status)</p>
                            </div>
                            <div>
                                <p id="pParentStatus" data-input="sltParentStatus" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Address No.)</p>
                            </div>
                            <div>
                                <p id="pParentHomeNumber" data-input="iptParentHomeNumber" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Soi)</p>
                            </div>
                            <div>
                                <p id="pParentAlley" data-input="iptParentAlley" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Moo)</p>
                            </div>
                            <div>
                                <p id="pParentVillageNo" data-input="iptParentVillageNo" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Street)</p>
                            </div>
                            <div>
                                <p id="pParentRoad" data-input="iptParentRoad" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Province)</p>
                            </div>
                            <div>
                                <p id="pParentProvince" data-input="sltParentProvince" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(District)</p>
                            </div>
                            <div>
                                <p id="pParentDistrict" data-input="sltParentDistrict" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Sub District)</p>
                            </div>
                            <div>
                                <p id="pParentSubDistrict" data-input="sltParentSubDistrict" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Post Code)</p>
                            </div>
                            <div>
                                <p id="pParentPostalCode" data-input="iptParentPostalCode" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Occupation)</p>
                            </div>
                            <div>
                                <p id="pParentCareer" data-input="iptParentCareer" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Company's Name)</p>
                            </div>
                            <div>
                                <p id="pParentWorkplace" data-input="iptParentWorkplace" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Monthly Income)</p>
                            </div>
                            <div>
                                <p id="pParentMonthIncome" data-input="iptParentMonthIncome" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104045") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Yearly Income)</p>
                            </div>
                            <div>
                                <p id="pParentYearIncome" data-input="sltParentYearIncome" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Telephone Number)</p>
                            </div>
                            <div>
                                <p id="pParentHousePhone" data-input="iptParentHousePhone" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Mobile Phone)</p>
                            </div>
                            <div>
                                <p id="pParentMobilePhone" data-input="iptParentMobilePhone" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ml-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Work Phone Number)</p>
                            </div>
                            <div>
                                <p id="pParentWorkPhone" data-input="iptParentWorkPhone" class="user-data">xxxx</p>
                            </div>
                        </div>
                        <div class="col-md-3 mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(E-Mail)</p>
                            </div>
                            <div>
                                <p id="pParentEmail" data-input="iptParentEmail" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <br>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--RegisterOnline10--%>
    <div class="row">
        <div class="col-md-12">
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
                        <div class="col-md-6 ml-auto mr-auto">
                            <div class="form-check form-check-inline">
                                <label class="form-check-label label-bag" style="padding-right: 40px; color: #707070;">
                                    <input class="form-check-input" type="radio" name="iptNoInstitution" value="no" disabled>
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
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101172") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Former School)</p>
                            </div>
                            <div>
                                <p id="pOldSchoolName" data-input="iptOldSchoolName" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 ml-auto mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Province)</p>
                            </div>
                            <div>
                                <p id="pOldSchoolProvince" data-input="sltOldSchoolProvince" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 ml-auto mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(District)</p>
                            </div>
                            <div>
                                <p id="pOldSchoolDistrict" data-input="sltOldSchoolDistrict" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 ml-auto mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Sub District)</p>
                            </div>
                            <div>
                                <p id="pOldSchoolSubDistrict" data-input="sltOldSchoolSubDistrict" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 ml-auto mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Degree of Education)</p>
                            </div>
                            <div>
                                <p id="pOldSchoolEducational" data-input="sltOldSchoolEducational" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 ml-auto mr-auto col-required-field">
                            <div class="div-bag">
                                <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101174") %> <span class="red-star"></span>: </span></label>
                                <p class="h6">(Grade Point Average Earned(GPA))</p>
                            </div>
                            <div>
                                <p id="pGPA" data-input="iptGPA" class="user-data">xxxx</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <br>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--RegisterOnline11--%>
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
                                <div class="div-bag">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131178") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Weight)</p>
                                </div>
                                <div>
                                    <p id="pWeight" data-input="iptWeight" class="user-data">xxxx</p>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131179") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Height)</p>
                                </div>
                                <div>
                                    <p id="pHeight" data-input="iptHeight" class="user-data">xxxx</p>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101217") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Blood Type)</p>
                                </div>
                                <div>
                                    <p id="pBlood" data-input="sltBlood" class="user-data">xxxx</p>
                                </div>
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
                                        <input class="form-check-input" type="radio" name="iptAllergySymptoms" value="yes" disabled>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %><p class="h6">(Yes)</p>
                                        <span class="circle">
                                            <span class="check"></span>
                                        </span>
                                    </label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <label class="form-check-label label-bag" style="color: #707070;">
                                        <input class="form-check-input" type="radio" name="iptAllergySymptoms" value="no" checked disabled>
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
                                <div class="div-bag">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101220") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Food Allergy)</p>
                                </div>
                                <div>
                                    <p id="pFoodAllergy" data-input="iptFoodAllergy" class="user-data">xxxx</p>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131225") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Drug(s) Allergy)</p>
                                </div>
                                <div>
                                    <p id="pBeAllergic" data-input="iptBeAllergic" class="user-data">xxxx</p>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101222") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Other Allergy)</p>
                                </div>
                                <div>
                                    <p id="pOtherAllergic" data-input="iptOtherAllergic" class="user-data">xxxx</p>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101223") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Chronic Disease)</p>
                                </div>
                                <div>
                                    <p id="pCongenitalDisease" data-input="iptCongenitalDisease" class="user-data">xxxx</p>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ml-auto mr-auto col-required-field">
                                <div class="div-bag">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101224") %> <span class="red-star"></span>: </span></label>
                                    <p class="h6">(Serious Illness or Medical Condition)</p>
                                </div>
                                <div>
                                    <p id="pSeriousDisease" data-input="iptSeriousDisease" class="user-data">xxxx</p>
                                </div>
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

    <%--RegisterOnline12--%>
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-rose card-header-icon">
                    <div class="card-icon text-center" style="border-radius: 12px; margin-left: 30px; margin-top: -30px;">
                        <h4 style="font-family: THSarabunNew; margin-bottom: 0px; padding: 3px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103095") %></h4>
                        <p class="h6 text-center">(Document information)</p>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <br>
                    </div>
                    <div class="row">
                        <div class="col-md-12 ml-auto mr-auto">
                            <table class="table table-striped">
                                <thead class="thead-light">
                                    <tr>
                                        <th width="12%" class="text-center" scope="col"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><p class="h6">No.</p>
                                        </th>
                                        <th width="68%" class="text-center" scope="col"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103123") %><p class="h6">list of document information</p>
                                        </th>
                                        <th width="15%" scope="col"></th>
                                        <th width="5%" scope="col"></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr style="display: none;">
                                        <th scope="row">0</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132859") %> :
                                                <p class="h6" data-did="0" data-tid="1" data-vfiid="0">(Please also download the photos in the application form.)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument00" name="fileDocument00" type="file" data-did="0" data-tid="1" data-vfiid="0" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="0" data-tid="1" data-vfiid="0">check_circle</span>
                                                <span class="material-icons open-file" data-did="0" data-tid="1" data-vfiid="0">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row">1</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103124") %><span class="red-star"></span>:
                                                <p class="h6" data-did="1" data-tid="1" data-vfiid="1">(One copy of the birth certificate or ID card, in case of foreign birth, a <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> translation must be attached.)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument01" name="fileDocument01" type="file" data-did="1" data-tid="1" data-vfiid="1" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="1" data-tid="1" data-vfiid="1">check_circle</span>
                                                <span class="material-icons open-file" data-did="1" data-tid="1" data-vfiid="1">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput h-2">
                                        <th scope="row">2</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103125") %> <span class="red-star"></span>:
                                                <p class="h6">(A copy of the student’s house registration, father and mother)</p>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row">2.1</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103126") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="2" data-tid="1" data-vfiid="3">(A copy of the student’s house registration 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument021" name="fileDocument021" type="file" data-did="2" data-tid="1" data-vfiid="3" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="2" data-tid="1" data-vfiid="3">check_circle</span>
                                                <span class="material-icons open-file" data-did="2" data-tid="1" data-vfiid="3">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row">2.2</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103127") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="2" data-tid="2" data-vfiid="4">(A copy of the father’s house registration 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument022" name="fileDocument022" type="file" data-did="2" data-tid="2" data-vfiid="4" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="2" data-tid="2" data-vfiid="4">check_circle</span>
                                                <span class="material-icons open-file" data-did="2" data-tid="2" data-vfiid="4">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row">2.3</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103128") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="2" data-tid="3" data-vfiid="5">(A copy of the mother’s house registration 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument023" name="fileDocument023" type="file" data-did="2" data-tid="3" data-vfiid="5" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="2" data-tid="3" data-vfiid="5">check_circle</span>
                                                <span class="material-icons open-file" data-did="2" data-tid="3" data-vfiid="5">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row">2.4</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103129") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="2" data-tid="4" data-vfiid="171">(A copy of the host or homeowner)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument024" name="fileDocument024" type="file" data-did="2" data-tid="4" data-vfiid="171" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="2" data-tid="4" data-vfiid="171">check_circle</span>
                                                <span class="material-icons open-file" data-did="2" data-tid="4" data-vfiid="171">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput h-3">
                                        <th scope="row">3</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103130") %> <span class="red-star"></span>:
                                                <p class="h6">(Copy of ID card of father and mother)</p>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row">3.1</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103131") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="3" data-tid="1" data-vfiid="7">(A copy of father ID card 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument031" name="fileDocument031" type="file" data-did="3" data-tid="1" data-vfiid="7" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="3" data-tid="1" data-vfiid="7">check_circle</span>
                                                <span class="material-icons open-file" data-did="3" data-tid="1" data-vfiid="7">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row">3.2</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103132") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="3" data-tid="2" data-vfiid="8">(A copy of mother ID card 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument032" name="fileDocument032" type="file" data-did="3" data-tid="2" data-vfiid="8" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="3" data-tid="2" data-vfiid="8">check_circle</span>
                                                <span class="material-icons open-file" data-did="3" data-tid="2" data-vfiid="8">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput h-4">
                                        <th scope="row">4</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103133") %> <span class="red-star"></span>:
                                            <p class="h6">(A copy of the Transcript)</p>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row">4.1</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103134") %> <span class="red-star"></span>:
                                            <p class="h6" data-did="4" data-tid="1" data-vfiid="9">(A copy of the Transcript, front document 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument041" name="fileDocument041" type="file" data-did="4" data-tid="1" data-vfiid="169" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="4" data-tid="1" data-vfiid="169">check_circle</span>
                                                <span class="material-icons open-file" data-did="4" data-tid="1" data-vfiid="169">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row">4.2</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103135") %> <span class="red-star"></span>:
                                            <p class="h6" data-did="4" data-tid="1" data-vfiid="9">(A copy of the Transcript, back document 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument042" name="fileDocument042" type="file" data-did="4" data-tid="2" data-vfiid="170" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="4" data-tid="2" data-vfiid="170">check_circle</span>
                                                <span class="material-icons open-file" data-did="4" data-tid="2" data-vfiid="170">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput h-5">
                                        <th scope="row">5</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103136") %> <span class="red-star"></span>:
                                                <p class="h6">(Copy of proof of name-surname change student, father and mother (if any))</p>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row">5.1</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103137") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="5" data-tid="1" data-vfiid="11">(Copy of proof of name-surname change of student(if any))</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument051" name="fileDocument051" type="file" data-did="5" data-tid="1" data-vfiid="11" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="5" data-tid="1" data-vfiid="11">check_circle</span>
                                                <span class="material-icons open-file" data-did="5" data-tid="1" data-vfiid="11">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row">5.2</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103138") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="5" data-tid="2" data-vfiid="12">(Copy of proof of name-surname change of father (if any))</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument052" name="fileDocument052" type="file" data-did="5" data-tid="2" data-vfiid="12" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="5" data-tid="2" data-vfiid="12">check_circle</span>
                                                <span class="material-icons open-file" data-did="5" data-tid="2" data-vfiid="12">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row">5.3</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103139") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="5" data-tid="3" data-vfiid="13">(Copy of proof of name-surname change of mother (if any))</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument053" name="fileDocument053" type="file" data-did="5" data-tid="3" data-vfiid="13" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="5" data-tid="3" data-vfiid="13">check_circle</span>
                                                <span class="material-icons open-file" data-did="5" data-tid="3" data-vfiid="13">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput <%=NoSuankularbNonthaburi%>">
                                        <th scope="row">6</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132777") %> <span class="red-star"></span>:
                                            <p class="h6" data-did="12" data-tid="1" data-vfiid="166">(A copy of household registration of the homeowner or landlord)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument12" name="fileDocument12" type="file" data-did="12" data-tid="1" data-vfiid="166" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="12" data-tid="1" data-vfiid="166">check_circle</span>
                                                <span class="material-icons open-file" data-did="12" data-tid="1" data-vfiid="166">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput <%=NoSuankularbNonthaburi%>">
                                        <th scope="row">7</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132778") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="13" data-tid="1" data-vfiid="167">(A document of home ownership certificate)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument13" name="fileDocument13" type="file" data-did="13" data-tid="1" data-vfiid="167" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="13" data-tid="1" data-vfiid="167">check_circle</span>
                                                <span class="material-icons open-file" data-did="13" data-tid="1" data-vfiid="167">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput <%=NoSuankularbNonthaburi%>">
                                        <th scope="row">8</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132779") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="14" data-tid="1" data-vfiid="168">(A document of student special condition)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument14" name="fileDocument14" type="file" data-did="14" data-tid="1" data-vfiid="168" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="14" data-tid="1" data-vfiid="168">check_circle</span>
                                                <span class="material-icons open-file" data-did="14" data-tid="1" data-vfiid="168">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row" class="re-order">6</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103140") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="6" data-tid="1" data-vfiid="14">(Portfolio, only for students in grades 1 and 4)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument06" name="fileDocument06" type="file" data-did="6" data-tid="1" data-vfiid="14" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="6" data-tid="1" data-vfiid="14">check_circle</span>
                                                <span class="material-icons open-file" data-did="6" data-tid="1" data-vfiid="14">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row" class="re-order">7</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103141") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="7" data-tid="1" data-vfiid="15">(Copy of adoption registration certificate (In the case of adoption) 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument07" name="fileDocument07" type="file" data-did="7" data-tid="1" data-vfiid="15" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="7" data-tid="1" data-vfiid="15">check_circle</span>
                                                <span class="material-icons open-file" data-did="7" data-tid="1" data-vfiid="15">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row" class="re-order">8</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103142") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="8" data-tid="1" data-vfiid="16">(Medical certificate (Hospital or Clinic))</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument08" name="fileDocument08" type="file" data-did="8" data-tid="1" data-vfiid="16" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="8" data-tid="1" data-vfiid="16">check_circle</span>
                                                <span class="material-icons open-file" data-did="8" data-tid="1" data-vfiid="16">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput">
                                        <th scope="row" class="re-order">9</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103143") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="9" data-tid="1" data-vfiid="17">(Proof of transfer of the application fee (transfer slip))</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument09" name="fileDocument09" type="file" data-did="9" data-tid="1" data-vfiid="17" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="9" data-tid="1" data-vfiid="17">check_circle</span>
                                                <span class="material-icons open-file" data-did="9" data-tid="1" data-vfiid="17">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput <%=NoAssumptionSriracha%>">
                                        <th scope="row">10</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132780") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="10" data-tid="1" data-vfiid="18">(Copy of documents showing the father/mother’s being Assumption Sriracha alumni or alumni Current students, brothers/sisters (if any))</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument10" name="fileDocument10" type="file" data-did="10" data-tid="1" data-vfiid="18" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="10" data-tid="1" data-vfiid="18">check_circle</span>
                                                <span class="material-icons open-file" data-did="10" data-tid="1" data-vfiid="18">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="row-fileinput <%=NoAssumptionSriracha%>">
                                        <th scope="row">11</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132781") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="11" data-tid="1" data-vfiid="19">(Copy of the baptismal receipt (For Catholic students) 1 issue)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <input id="fileDocument11" name="fileDocument11" type="file" data-did="11" data-tid="1" data-vfiid="19" accept="application/pdf, image/*" />
                                                <span class="material-icons no-file" data-did="11" data-tid="1" data-vfiid="19">check_circle</span>
                                                <span class="material-icons open-file" data-did="11" data-tid="1" data-vfiid="19">folder</span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="row">
                        <br>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <button id="btnBack" class="btn btn-warning btn-round col-md-2" style="font-size: 1.2em;">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %><p class="h6" style="margin-bottom: 0px;">(Back)</p>
                            </button>
                            <button id="btnSave" class="btn btn-success btn-round col-md-2" style="font-size: 1.2em;">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %><p class="h6" style="margin-bottom: 0px;">(Save)</p>
                            </button>
                        </div>
                    </div>
                    <div class="row">
                        <br>
                    </div>
                    <div class="modal">
                        <div class="card" style="width: 300px; left: 50%; top: 50%; margin-left: -150px; margin-top: -100px;">
                            <div class="card-body text-center" style="padding-left: 15px;">
                                <%--<div class="spinner3" style="margin: -3px 0 0 0px;"></div>--%>
                                <span style="margin-left: 25px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132862") %></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">
    <script>

        var registerDocument = {
            FileData: [],
        }

        function RetreivedFileObject() {
            // TODO: RetreivedFileBase64FromSession

            var base64 = preRegister.ProfilePicture;

            $('#profilePicturePreview').attr('src', base64);
        }

        function LoadDataFromLocalStorage() {
            // Get data from localStorage
            if (ls.isBrowserSupport()) {
                // Code for localStorage
                preRegister = ls.getItem('preRegister');

                // RegisterOnline04
                if (preRegister.Page04Saved) {
                    //if (!$.isEmpty(preRegister.ProfilePicture)) {
                    //    RetreivedFileObject();
                    //}
                    $('input:radio[name="iptStudentCategory"]').filter('[value="' + preRegister.StudentCategory + '"]').attr('checked', true);
                    $('#pStudentTitle').text(preRegister.StudentTitleText);
                    $('#pStudentName').text(preRegister.StudentName);
                    $('#pStudentLastname').text(preRegister.StudentLastname);
                    $('#pStudentNameEn').text(preRegister.StudentNameEn);
                    $('#pStudentLastnameEn').text(preRegister.StudentLastnameEn);
                    $('#pStudentNickName').text(preRegister.StudentNickName);
                    $('#pStudentNickNameEn').text(preRegister.StudentNickNameEn);
                    $('#pStudentSex').text(preRegister.StudentSexText);
                    $('#pStudentBirthday').text(preRegister.StudentBirthday);
                    $('#pStudentIDCard').text(preRegister.StudentIDCard);
                    $('#pStudentRace').text(preRegister.StudentRaceText);
                    $('#pStudentNation').text(preRegister.StudentNationText);
                    $('#pStudentReligion').text(preRegister.StudentReligionText);
                    $('#pStudentSonTotal').text(preRegister.StudentSonTotal);
                    $('#pStudentSonNumber').text(preRegister.StudentSonNumber);
                }

                // RegisterOnline05
                if (preRegister.Page05Saved) {
                    $('#pHouseCode').text(preRegister.HouseCode);
                    $('#pHouseHomeNumber').text(preRegister.HouseHomeNumber);
                    $('#pHouseAlley').text(preRegister.HouseAlley);
                    $('#pHouseVillageNo').text(preRegister.HouseVillageNo);
                    $('#pHouseRoad').text(preRegister.HouseRoad);
                    $('#pHouseProvince').text(preRegister.HouseProvinceText);
                    $('#pHouseDistrict').text(preRegister.HouseDistrictText);
                    $('#pHouseSubDistrict').text(preRegister.HouseSubDistrictText);
                    $('#pHousePostalCode').text(preRegister.HousePostalCode);
                    $('#pHousePhone').text(preRegister.HousePhone);
                    $('#pHouseEmail').text(preRegister.HouseEmail);
                }

                // RegisterOnline06
                if (preRegister.Page06Saved) {
                    if (preRegister.SameHouseAddress == "same") {
                        $('input:radio[name="iptSameHouseAddress"]').filter('[value="' + preRegister.SameHouseAddress + '"]').attr('checked', true);
                    }
                    $('#pStudentHomeNumber').text(preRegister.StudentHomeNumber);
                    $('#pStudentAlley').text(preRegister.StudentAlley);
                    $('#pStudentVillageNo').text(preRegister.StudentVillageNo);
                    $('#pStudentRoad').text(preRegister.StudentRoad);
                    $('#pStudentProvince').text(preRegister.StudentProvinceText);
                    $('#pStudentDistrict').text(preRegister.StudentDistrictText);
                    $('#pStudentSubDistrict').text(preRegister.StudentSubDistrictText);
                    $('#pStudentPostalCode').text(preRegister.StudentPostalCode);
                    $('#pStudentHousePhone').text(preRegister.StudentHousePhone);
                    $('#pStudentHouseEmail').text(preRegister.StudentHouseEmail);
                    $('#pStudentStayWithTitle').text(preRegister.StudentStayWithTitleText);
                    $('#pStudentStayWithName').text(preRegister.StudentStayWithName);
                    $('#pStudentStayWithLast').text(preRegister.StudentStayWithLast);
                    $('#pStudentStayWithEmergencyCall').text(preRegister.StudentStayWithEmergencyCall);
                    $('#pStudentHomeType').text(preRegister.StudentHomeTypeText);
                    $('#pStudentNeighborName').text(preRegister.StudentNeighborName);
                    $('#pStudentNeighborLastName').text(preRegister.StudentNeighborLastName);
                    $('#pStudentNeighborSubLevel').text(preRegister.StudentNeighborSubLevelText);
                    $('#pStudentNeighborPhone').text(preRegister.StudentNeighborPhone);
                }

                // RegisterOnline07
                if (preRegister.Page07Saved) {
                    $('#pFatherTitle').text(preRegister.FatherTitleText);
                    $('#pFatherName').text(preRegister.FatherName);
                    $('#pFatherLastname').text(preRegister.FatherLastname);
                    $('#pFatherNameEn').text(preRegister.FatherNameEn);
                    $('#pFatherLastnameEn').text(preRegister.FatherLastnameEn);
                    $('#pFatherBirthday').text(preRegister.FatherBirthday);
                    $('#pFatherIDCard').text(preRegister.FatherIDCard);
                    $('#pFatherRace').text(preRegister.FatherRaceText);
                    $('#pFatherNation').text(preRegister.FatherNationText);
                    $('#pFatherReligion').text(preRegister.FatherReligionText);
                    $('#pFatherEducational').text(preRegister.FatherEducationalText);
                    $('#pFatherHomeNumber').text(preRegister.FatherHomeNumber);
                    $('#pFatherAlley').text(preRegister.FatherAlley);
                    $('#pFatherVillageNo').text(preRegister.FatherVillageNo);
                    $('#pFatherRoad').text(preRegister.FatherRoad);
                    $('#pFatherProvince').text(preRegister.FatherProvinceText);
                    $('#pFatherDistrict').text(preRegister.FatherDistrictText);
                    $('#pFatherSubDistrict').text(preRegister.FatherSubDistrictText);
                    $('#pFatherPostalCode').text(preRegister.FatherPostalCode);
                    $('#pFatherCareer').text(preRegister.FatherCareer);
                    $('#pFatherWorkplace').text(preRegister.FatherWorkplace);
                    $('#pFatherMonthIncome').text(preRegister.FatherMonthIncome);
                    $('#pFatherYearIncome').text(preRegister.FatherYearIncomeText);
                    $('#pFatherHousePhone').text(preRegister.FatherHousePhone);
                    $('#pFatherMobilePhone').text(preRegister.FatherMobilePhone);
                    $('#pFatherWorkPhone').text(preRegister.FatherWorkPhone);
                    $('#pFatherEmail').text(preRegister.FatherEmail);
                }

                // RegisterOnline08
                if (preRegister.Page08Saved) {
                    $('#pMotherTitle').text(preRegister.MotherTitleText);
                    $('#pMotherName').text(preRegister.MotherName);
                    $('#pMotherLastname').text(preRegister.MotherLastname);
                    $('#pMotherNameEn').text(preRegister.MotherNameEn);
                    $('#pMotherLastnameEn').text(preRegister.MotherLastnameEn);
                    $('#pMotherBirthday').text(preRegister.MotherBirthday);
                    $('#pMotherIDCard').text(preRegister.MotherIDCard);
                    $('#pMotherRace').text(preRegister.MotherRaceText);
                    $('#pMotherNation').text(preRegister.MotherNationText);
                    $('#pMotherReligion').text(preRegister.MotherReligionText);
                    $('#pMotherEducational').text(preRegister.MotherEducationalText);
                    $('#pMotherHomeNumber').text(preRegister.MotherHomeNumber);
                    $('#pMotherAlley').text(preRegister.MotherAlley);
                    $('#pMotherVillageNo').text(preRegister.MotherVillageNo);
                    $('#pMotherRoad').text(preRegister.MotherRoad);
                    $('#pMotherProvince').text(preRegister.MotherProvinceText);
                    $('#pMotherDistrict').text(preRegister.MotherDistrictText);
                    $('#pMotherSubDistrict').text(preRegister.MotherSubDistrictText);
                    $('#pMotherPostalCode').text(preRegister.MotherPostalCode);
                    $('#pMotherCareer').text(preRegister.MotherCareer);
                    $('#pMotherWorkplace').text(preRegister.MotherWorkplace);
                    $('#pMotherMonthIncome').text(preRegister.MotherMonthIncome);
                    $('#pMotherYearIncome').text(preRegister.MotherYearIncomeText);
                    $('#pMotherHousePhone').text(preRegister.MotherHousePhone);
                    $('#pMotherMobilePhone').text(preRegister.MotherMobilePhone);
                    $('#pMotherWorkPhone').text(preRegister.MotherWorkPhone);
                    $('#pMotherEmail').text(preRegister.MotherEmail);
                }

                // RegisterOnline09
                if (preRegister.Page09Saved) {
                    if (preRegister.ParentRelation == '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101195") %>' || preRegister.ParentRelation == '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101196") %>') {
                        $("#pParentRelation").text(preRegister.ParentRelation);
                        $('#pParentRelationOther').text('');
                    }
                    else {
                        $("#pParentRelation").text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %>');
                        $('#pParentRelationOther').text(preRegister.ParentRelation);
                    }
                    $('#pParentTitle').text(preRegister.ParentTitleText);
                    $('#pParentName').text(preRegister.ParentName);
                    $('#pParentLastname').text(preRegister.ParentLastname);
                    $('#pParentNameEn').text(preRegister.ParentNameEn);
                    $('#pParentLastnameEn').text(preRegister.ParentLastnameEn);
                    $('#pParentBirthday').text(preRegister.ParentBirthday);
                    $('#pParentIDCard').text(preRegister.ParentIDCard);
                    $('#pParentRace').text(preRegister.ParentRaceText);
                    $('#pParentNation').text(preRegister.ParentNationText);
                    $('#pParentReligion').text(preRegister.ParentReligionText);
                    $('#pParentEducational').text(preRegister.ParentEducationalText);
                    $('#pParentStatus').text(preRegister.ParentStatusText);
                    $('#pParentHomeNumber').text(preRegister.ParentHomeNumber);
                    $('#pParentAlley').text(preRegister.ParentAlley);
                    $('#pParentVillageNo').text(preRegister.ParentVillageNo);
                    $('#pParentRoad').text(preRegister.ParentRoad);
                    $('#pParentProvince').text(preRegister.ParentProvinceText);
                    $('#pParentDistrict').text(preRegister.ParentDistrictText);
                    $('#pParentSubDistrict').text(preRegister.ParentSubDistrictText);
                    $('#pParentPostalCode').text(preRegister.ParentPostalCode);
                    $('#pParentCareer').text(preRegister.ParentCareer);
                    $('#pParentWorkplace').text(preRegister.ParentWorkplace);
                    $('#pParentMonthIncome').text(preRegister.ParentMonthIncome);
                    $('#pParentYearIncome').text(preRegister.ParentYearIncomeText);
                    $('#pParentHousePhone').text(preRegister.ParentHousePhone);
                    $('#pParentMobilePhone').text(preRegister.ParentMobilePhone);
                    $('#pParentWorkPhone').text(preRegister.ParentWorkPhone);
                    $('#pParentEmail').text(preRegister.ParentEmail);
                }

                // RegisterOnline10
                if (preRegister.Page10Saved) {
                    if (preRegister.NoInstitution == "no") {
                        $('input:radio[name="iptNoInstitution"]').filter('[value="' + preRegister.NoInstitution + '"]').attr('checked', true);
                    }
                    $('#pOldSchoolName').text(preRegister.OldSchoolName);
                    $('#pOldSchoolProvince').text(preRegister.OldSchoolProvinceText);
                    $('#pOldSchoolDistrict').text(preRegister.OldSchoolDistrictText);
                    $('#pOldSchoolSubDistrict').text(preRegister.OldSchoolSubDistrictText);
                    $('#pOldSchoolEducational').text(preRegister.OldSchoolEducationalText);
                    $('#pGPA').text(preRegister.GPA);
                }

                // RegisterOnline11
                if (preRegister.Page11Saved) {
                    $('#pWeight').text(preRegister.Weight);
                    $('#pHeight').text(preRegister.Height);
                    $('#pBlood').text(preRegister.BloodText);
                    $('input:radio[name="iptAllergySymptoms"]').filter('[value="' + preRegister.AllergySymptoms + '"]').attr('checked', true);
                    $('#pFoodAllergy').text(preRegister.FoodAllergy);
                    $('#pBeAllergic').text(preRegister.BeAllergic);
                    $('#pOtherAllergic').text(preRegister.OtherAllergic);
                    $('#pCongenitalDisease').text(preRegister.CongenitalDisease);
                    $('#pSeriousDisease').text(preRegister.SeriousDisease);
                }

                // RegisterOnline12
                RetreivedFileBase64FromSession();

            } else {
                // No web storage Support.
            }
        }

        function SaveDataToDatabase(callbackFunction) {

            InsertSaveSpin();

            $.ajax({
                async: false,
                type: "POST",
                url: 'RegisterOnlinePreview.aspx/SaveData',
                data: JSON.stringify({ register: preRegister }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    ClearSaveSpin();

                    var r = JSON.parse(result.d);
                    if (r.success) {

                        // Set PageXXSaved = false & set FullComplete = true
                        preRegister.FullComplete = true;
                        preRegister.Page01Saved = preRegister.Page02Saved = preRegister.Page03Saved = preRegister.Page04Saved = preRegister.Page05Saved = preRegister.Page06Saved = preRegister.Page07Saved = preRegister.Page08Saved = preRegister.Page09Saved = preRegister.Page10Saved = preRegister.Page11Saved = preRegister.Page12Saved = false;

                        // Set student code
                        preRegister.studentID = r.studentID;
                        preRegister.examCode = r.examCode;
                        preRegister.registerID = r.registerID;

                        Swal.fire({
                            title: 'Done!',
                            text: 'Complete your process.',
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            showConfirmButton: true,
                            buttonsStyling: false
                        }).then(result => {
                            if (result.value) {
                                // handle Confirm button click
                                // result.value will contain `true` or the input value
                            } else {
                                // handle dismissals
                                // result.dismiss can be 'cancel', 'overlay', 'esc' or 'timer'
                            }
                            callbackFunction();
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Warning!',
                            text: 'Warning Message - ' + r.message,
                            type: 'warning',
                            confirmButtonClass: "btn btn-warning",
                            buttonsStyling: false
                        });
                    }
                },
                error: OnError
            });
        }

        function OnError(xhr, errorType, exception) {

            ClearSaveSpin();

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

        function InsertSaveSpin() {
            //$("#btnSave").prepend('<div class="spinner" style="margin: -3px 0 0 -20px;"></div>');

            $(".modal").fadeToggle();
        }

        function ClearSaveSpin() {
            //$("#btnSave").children().remove(".spinner");

            $(".modal").fadeOut();
        }

        function AddRequiredRulesvalidation(obj, cateId) {
            switch (cateId) {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    switch (obj) {
                        case 'iptStudentCategory':
                        case 'iptAllergySymptoms':
                            $('input[name=' + obj + ']').closest('div.col-required-field').find('.red-star').html('<sup>*</sup>');
                            break;
                        default:
                            $("p[data-input='" + obj + "']").closest('div.col-required-field').find('.red-star').html('<sup>*</sup>');
                            break;
                    }
                    break;
                case 9:
                    $(obj).closest('tr').find('.red-star').html('<sup>*</sup>');
                    break;
            }
        }

        function EnableFileUploadDocument(obj) {
            $(obj).closest('.row-fileinput').addClass('enable');

            var hobj = $('.row-fileinput.h-' + $(obj).data('did'));
            if (!$(hobj).hasClass("enable")) {
                $(hobj).addClass('enable');
            }
        }

        function ReNoFileUploadDocument() {
            var no = 1;
            $('.row-fileinput.enable th').each(function (index) {
                if ($(this).text().indexOf(".") != -1) {
                    no--;
                    $(this).text(no + $(this).text().substring(1));
                }
                else {
                    $(this).text(no);
                }
                no++;
            });
        }

        function ReapplyTableStriping() {
            $("tr:visible").each(function (index) {
                $(this).css("background-color", !!(index & 1) ? "#ffffff" : "#f9f9f9");
            });
        }

        function RetreivedFileBase64FromSession() {
            $.ajax({
                async: false,
                type: "POST",
                url: 'RegisterOnline04.aspx/ListFileBase64FromSession',
                data: JSON.stringify({}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    var r = JSON.parse(result.d);
                    if (r.success) {
                        // TODO : Retreived file base64 from session
                        if (r.documentFiles != null) {
                            // RegisterOnline04
                            if (preRegister.Page04Saved) {
                                const profilePicture = r.documentFiles.filter(function (obj) { return obj.docId == 0; });
                                $.each(profilePicture, function (i, obj) {
                                    var docId = obj.docId;
                                    var typeId = obj.typeId;
                                    $('input[type=file][data-did=' + docId + '][data-tid=' + typeId + ']').data('selected', true);
                                    if (!$('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').hasClass('ready')) {
                                        $('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').addClass('ready');
                                        $('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').removeClass('no-file');
                                    }

                                    var base64 = obj.byteData;
                                    $('#profilePicturePreview').attr('src', base64);

                                    registerDocument.FileData.push(obj);
                                });
                            }

                            // RegisterOnline12
                            if (preRegister.Page12Saved) {
                                //registerDocument.FileData = preRegister.Files;
                                const documentFiles = r.documentFiles.filter(function (obj) { return obj.docId != 0; });
                                $.each(documentFiles, function (i, obj) {
                                    var docId = obj.docId;
                                    var typeId = obj.typeId;
                                    $('input[type=file][data-did=' + docId + '][data-tid=' + typeId + ']').data('selected', true);
                                    if (!$('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').hasClass('ready')) {
                                        $('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').addClass('ready');
                                        $('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').removeClass('no-file');
                                    }

                                    registerDocument.FileData.push(obj);
                                });
                            }
                            preRegister.Files = registerDocument.FileData;
                        } else {
                            preRegister.Files = [];
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

            LoadDataFromLocalStorage();

            $("#btnBack").bind({
                click: function () {

                    window.location.href = "RegisterOnline12.aspx";

                    return false;
                }
            });

            $("#btnSave").bind({
                click: function () {

                    SaveDataToDatabase(function () {

                        window.location.href = "RegisterPrint.aspx";

                    });

                    return false;
                }
            });
        });
    </script>
    <asp:Literal ID="ltrScriptRequiredField" runat="server"></asp:Literal>
</asp:Content>
