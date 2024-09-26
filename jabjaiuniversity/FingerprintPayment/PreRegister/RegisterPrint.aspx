<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPrint.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterPrint" %>

<!DOCTYPE html>
<html>
<head>
    <link rel="apple-touch-icon" sizes="76x76" href="assets/img/apple-icon.png" />
    <link rel="icon" type="image/png" href="assets/img/favicon.png" />
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <title></title>
    <link rel="stylesheet" href="assets/css/style-preregister-print.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        @import url("webfonts/TH_Sarabun/thsarabunnew.css");

        body {
            font-family: THSarabunNew;
        }

        .btn.btn-info:active:hover {
            color: #fff;
            background-color: #00aec5;
            border-color: #004b55;
        }

        .btn-info {
            color: #ffffff;
            background-color: #00bcd4;
            border-color: #00bcd4;
            box-shadow: none;
        }

        .btn.btn-success:active:hover {
            color: #fff;
            background-color: #47a44b;
            border-color: #255627;
        }

        .btn.btn-success {
            color: #fff;
            background-color: #4caf50;
            border-color: #4caf50;
            box-shadow: 0 2px 2px 0 rgba(76, 175, 80, 0.14), 0 3px 1px -2px rgba(76, 175, 80, 0.2), 0 1px 5px 0 rgba(76, 175, 80, 0.12);
        }

        .btn.btn-info:active:hover, .btn.btn-info:active:focus, .btn.btn-info:active.focus, .btn.btn-info.active:hover, .btn.btn-info.active:focus, .btn.btn-info.active.focus, .open > .btn.btn-info.dropdown-toggle:hover, .open > .btn.btn-info.dropdown-toggle:focus, .open > .btn.btn-info.dropdown-toggle.focus, .show > .btn.btn-info.dropdown-toggle:hover, .show > .btn.btn-info.dropdown-toggle:focus, .show > .btn.btn-info.dropdown-toggle.focus {
            color: #fff;
            background-color: #00aec5;
            border-color: #004b55;
        }

        .btn:not(:disabled):not(.disabled):active, .btn:not(:disabled):not(.disabled).active {
            background-image: none;
            box-shadow: none;
        }

        .btn:not(:disabled):not(.disabled) {
            cursor: pointer;
        }

        .btn.btn-round {
            border-radius: 30px;
        }

        .btn:hover, .btn:focus {
            text-decoration: none;
        }

        .btn {
            position: relative;
            padding: 12px 30px;
            margin: 0.3125rem 1px;
            font-weight: 400;
            line-height: 1.428571;
            text-transform: uppercase;
            letter-spacing: 0;
            border: 0;
            will-change: box-shadow, transform;
        }

        .btn {
            display: inline-block;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            user-select: none;
        }

        button, html [type="button"], [type="reset"], [type="submit"] {
            -webkit-appearance: button;
        }

        button {
            overflow: visible;
        }

        button {
            font-family: inherit;
        }

        .table-fixed {
            table-layout: fixed;
        }

            .table-fixed td {
                white-space: nowrap;
            }

    </style>
</head>
<body>
    <div class="book">
        <div class="page">
            <div class="subpage">
                <div class="school-logo">
                    <img src="<%=schoolLogo %>" width="98" class="picture-src" title="">
                </div>
                <span class="text-label student"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %> : _________________</span>
                <div class="photo">
                    <p class="photo-text" style="<%=HaveProfileImage?"display: none;":""%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132935") %></p>
                    <img src="<%=ProfileImageUrl%>" style="height: 2.6cm; width: 2.2cm; <%=HaveProfileImage?"":"display: none;"%>">
                </div>
                <div class="text-center">
                    <span class="header-title"><%=schoolName %></span><br />
                    <span class="header-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132931") %> <%=registerYearBE %></span><br />
                    <span class="header-title"></span>
                </div>

                <div class="no-border tab-2" style="padding: 30px 0 0 0;">
                    <table class="table-fixed">
                        <tr>
                            <th style="width: 21%;"></th>
                            <th style="width: 9%;"></th>
                            <th style="width: 17%;"></th>
                            <th style="width: 17%;"></th>
                            <th style="width: 18%;"></th>
                            <th style="width: 18%;"></th>
                        </tr>
                        <tr>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132936") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.RegisterCode %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132937") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.FullDate %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132932") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.Class %></td>
                        </tr>
                        <tr>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentCategory %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132938") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.PlanName %></td>
                            <td class="text-label text-right"><!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132784") %> :--></td>
                            <td class="text-data"><!--<%=entityRegisterPrint.ExamCode %>--></td>
                        </tr>
                    </table>
                </div>

                <div class="div-title">
                    <span class="text-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></span>
                </div>
                <div class="no-border tab-2" style="padding: 5px 0 0 15px;">
                    <table class="table-fixed">
                        <tr>
                            <th style="width: 20%;"></th>
                            <th style="width: 16%;"></th>
                            <th style="width: 10%;"></th>
                            <th style="width: 16%;"></th>
                            <th style="width: 10%;"></th>
                            <th style="width: 9%;"></th>
                            <th style="width: 8%;"></th>
                            <th style="width: 11%;"></th>
                        </tr>
                        <tr>
                            <td class="text-label text-left" colspan="2" style="padding-left: 10px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %> : <%=entityRegisterPrint.StudentFullName %></td>
                            <%--<td class="text-data"><%=entityRegisterPrint.StudentFullName %></td>--%>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentBirthday %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111037") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentAge %></td>
                            <td class="text-data"></td>
                            <td class="text-data"></td>
                        </tr>
                        <tr>
                            <td class="text-label text-left" colspan="2" style="padding-left: 10px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106111") %> : <%=entityRegisterPrint.IDCard %></td>
                            <%--<td class="text-data"><%=entityRegisterPrint.IDCard %></td>--%>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentRace %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentNation %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentReligion %></td>
                        </tr>
                    </table>
                </div>

                <div class="div-title">
                    <span class="text-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101147") %></span>
                </div>
                <div class="no-border tab-2" style="padding: 5px 0 0 15px;">
                    <table class="table-fixed">
                        <tr>
                            <th style="width: 19%;"></th>
                            <th style="width: 20%;"></th>
                            <th style="width: 14%;"></th>
                            <th style="width: 16%;"></th>
                            <th style="width: 13%;"></th>
                            <th style="width: 18%;"></th>
                        </tr>
                        <tr>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentHomeNumber %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentAlley %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentRoad %></td>
                        </tr>
                        <tr>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132939") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentSubDistrict %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132087") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentDistrict %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentProvince %></td>
                        </tr>
                        <tr>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentPostalCode %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentHousePhone %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.StudentHouseEmail %></td>
                        </tr>
                    </table>
                </div>

                <div class="div-title">
                    <span class="text-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132940") %></span>
                </div>
                <div class="no-border tab-2" style="padding: 5px 0 0 15px;">
                    <table class="table-fixed">
                        <tr>
                            <th style="width: 20%;"></th>
                            <th style="width: 46%;"></th>
                            <th style="width: 16%;"></th>
                            <th style="width: 18%;"></th>
                        </tr>
                        <tr>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.FatherFullName %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132941") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.FatherPhone2 %></td>
                        </tr>
                        <tr>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.FatherJob %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.FatherIncome %></td>
                        </tr>
                        <tr>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.MotherFullName %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132941") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.MotherPhone2 %></td>
                        </tr>
                        <tr>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.MotherJob %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.MotherIncome %></td>
                        </tr>
                        <tr>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.ParentFullName %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132941") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.ParentPhone2 %></td>
                        </tr>
                        <tr>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.ParentJob %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.ParentIncome %></td>
                        </tr>
                    </table>
                </div>

                <div class="div-title">
                    <span class="text-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %></span>
                </div>
                <div class="no-border tab-2" style="padding: 5px 0 0 15px;">
                    <table class="table-fixed">
                        <tr>
                            <th style="width: 20%;"></th>
                            <th style="width: 30%;"></th>
                            <th style="width: 22%;"></th>
                            <th style="width: 28%;"></th>
                        </tr>
                        <tr>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101223") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.ChronicDisease %></td>
                            <td class="text-label text-right">ภูมิแพ้ :</td>
                            <td class="text-data"><%=entityRegisterPrint.OtherAllergy %></td>
                        </tr>
                        <tr>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132943") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.FoodAllergy %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132942") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.DrugAllergy %></td>
                        </tr>
                    </table>
                </div>

                <div class="div-title">
                    <span class="text-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101172") %></span>
                </div>
                <div class="no-border tab-2" style="padding: 5px 0 0 15px;">
                    <table class="table-fixed">
                        <tr>
                            <th style="width: 20%;"></th>
                            <th style="width: 30%;"></th>
                            <th style="width: 22%;"></th>
                            <th style="width: 28%;"></th>
                        </tr>
                        <tr>
                            <td class="text-label text-right">ชื่อสถานศึกษา :</td>
                            <td class="text-data"><%=entityRegisterPrint.OldSchoolName %></td>
                            <td class="text-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</td>
                            <td class="text-data"><%=entityRegisterPrint.OldSchoolProvince %></td>
                        </tr>
                    </table>
                </div>

                <div class="no-border tab-2" style="padding: 15px 0 0 0;">
                    <table style="font-weight: bold;">
                        <tr>
                            <th style="width: 50%;"></th>
                            <th style="width: 50%;"></th>
                        </tr>
                        <tr>
                            <td class="text-label text-center"><%=entityRegisterPrint.StudentFullName %></td>
                            <td class="text-data text-center"><%=entityRegisterPrint.ParentFullName %></td>
                        </tr>
                        <tr>
                            <td class="text-label text-center">(ผู้สมัคร)</td>
                            <td class="text-data text-center">(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %>)</td>
                        </tr>
                        <tr>
                            <td class="text-label text-center"><%=entityRegisterPrint.Date %></td>
                            <td class="text-data text-center"><%=entityRegisterPrint.Date %></td>
                        </tr>
                    </table>
                </div>

                <div class="footer" style="padding: 75px 0 0 0;">
                    <table class="small-card" style="height: 12rem;">
                        <tr>
                            <td class="text-label text-center small-card" style="width: 50%;">
                                <table class="no-border" style="border: 0px solid black;">
                                    <tr>
                                        <td class="text-label text-center" style="width: 100%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132863") %></td>
                                    </tr>
                                    <tr>
                                        <td class="text-label text-center" style="width: 100%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132873") %> <%=entityRegisterPrint.Fee %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></td>
                                    </tr>
                                    <tr>
                                        <td class="text-label text-center" style="width: 100%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="text-label text-center" style="width: 100%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132945") %></td>
                                    </tr>
                                    <tr>
                                        <td class="text-label text-center" style="width: 100%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132946") %></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="text-data text-center small-card" style="width: 50%;">
                                <div class="school-logo small-card">
                                    <img src="<%=schoolLogo %>" width="65" class="picture-src" title="">
                                </div>
                                <div class="photo small-card">
                                    <p class="photo-text small-card" style="<%=HaveProfileImage?"display: none;":""%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132935") %></p> 
                                    <img src="<%=ProfileImageUrl%>" style="height: 85px; width: 65px; <%=HaveProfileImage?"":"display: none;"%>">
                                </div>
                                <table class="no-border" style="border: 0px solid black; height: 90%; margin-top: 5px;">
                                    <tr>
                                        <td class="text-label text-center" style="width: 100%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132933") %></td>
                                    </tr>
                                    <tr>
                                        <td class="text-label text-center" style="width: 100%;"><%=schoolName %></td>
                                    </tr>
                                    <tr>
                                        <td class="text-label text-center" style="width: 100%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132934") %> <%=registerYearBE %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %><%=entityRegisterPrint.ClassLongName %></td>
                                    </tr>
                                    <tr>
                                        <td class="text-label text-center" style="width: 100%;"></td>
                                    </tr>
                                    <tr>
                                        <td class="text-label text-center" style="width: 100%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %> : <%=entityRegisterPrint.StudentFullName %></td>
                                    </tr>
                                    <tr>
                                        <td class="text-label text-center" style="width: 100%;">
                                            <table class="no-border" style="border: 0px solid black; height: 100%;">
                                                <tr>
                                                    <td class="text-label text-center" style="width: 60%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103190") %></td>
                                                    <td class="text-label text-center" style="width: 40%;"><!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132784") %>--></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-label text-center" style="width: 60%;"><%=entityRegisterPrint.PlanName %></td>
                                                    <td class="text-label text-center" style="width: 40%;"><!--<%=entityRegisterPrint.ExamCode %>--></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="no-print-me" style="text-align: center; margin-bottom: 40px;">
            <button id="btnSaveDraft" class="btn btn-info btn-round col-md-2" style="font-size: 1.2em;" onclick="window.print(); return false;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>
            </button>
            <button id="btnSave" class="btn btn-success btn-round col-md-2" style="font-size: 1.2em;" onclick="window.location.href = 'RegisterOnline01.aspx'; return false;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>
            </button>
        </div>
    </div>
</body>
</html>
<!--139621139-->
