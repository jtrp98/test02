<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="RegisterPrint00258.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterPrint00258" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="/bootstrap SB2/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.5.0/css/all.css" integrity="sha384-B4dIYHKNBt8Bc12p+WXckhzcICo0wtJAoU8YZTY5qE0Id1GSseTk6S+L3BlXeVIU" crossorigin="anonymous" />

    <style>
        body {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            background-color: #FAFAFA;
            font: 12pt "Tahoma";
        }

        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }

        .page {
            width: 210mm;
            min-height: 292mm;
            padding: 10mm;
            margin: 2mm auto;
            border: 1px;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }

        .subpage {
            padding: 0.5cm;
            border: 5px;
            height: 275mm;
            outline: 2cm;
        }

        @page {
            size: A4;
            margin: 0mm;
        }

        @media print {
            .no-print, .no-print * {
                display: none !important;
            }

            .example-screen {
                display: none;
            }
        }

        .w3-right {
            float: right !important
        }

        .w3-xlarge {
            font-size: 24px !important
        }

        .w3-teal, .w3-hover-teal:hover {
            color: #fff !important;
            background-color: #009688 !important
        }

        .w3-btn, .w3-button {
            border: none;
            display: inline-block;
            padding: 8px 16px;
            vertical-align: middle;
            overflow: hidden;
            text-decoration: none;
            color: inherit;
            background-color: inherit;
            text-align: center;
            cursor: pointer;
            white-space: nowrap;
        }

            .w3-button:hover {
                color: #000 !important;
                background-color: #ccc !important
            }

        .w3-blue, .w3-hover-blue:hover {
            color: #fff !important;
            background-color: #2196F3 !important;
        }

        .bold {
            font-weight: bold;
        }

        .book {
            font-family: THSarabun;
            font-size: 21px;
        }

            .book .padding-0 {
                padding: 0px;
            }

            .book .padding-lr-10 {
                padding-left: 10px;
                padding-right: 10px;
            }

            .book .padding-l-20 {
                padding-left: 20px;
            }

            .book .padding-t-10 {
                padding-top: 10px;
            }

            .book .padding-t-130 {
                padding-top: 130px;
            }

            .book .padding-b-10 {
                padding-bottom: 10px;
            }

            .book .font16 {
                font-size: 16px;
            }

            .book .font18 {
                font-size: 18px;
            }

            .book .font10 {
                font-size: 10px;
            }

            .book .font20 {
                font-size: 20px;
            }

            .book .font22 {
                font-size: 22px;
            }

            .book .font25 {
                font-size: 25px;
            }

            .book .border {
                border: 1px solid #000;
            }

            .book i.fa-circle, .book i.fa-check-circle {
                font-size: 14px;
                margin-top: 5px;
                margin-right: 5px;
            }

            .book p {
                margin: 0 0 3px;
            }

                .book p.bold {
                    font-weight: bold;
                }

                .book p.flex {
                    display: flex;
                }

                .book span.underline, .book p.underline {
                    text-decoration: underline;
                    font-weight: bold;
                }

            .book div.underline {
                text-decoration: underline;
                font-weight: bold;
            }

            .book .input-row {
                display: flex;
                white-space: nowrap;
            }

                .book .input-row .input-value {
                    border-bottom: 1px dotted black;
                    height: 23px;
                    text-align: center;
                    margin: 0 5px 0 5px;
                }

            .book p.margin-b-n5 {
                margin: 0 0 -5px;
            }

            .book .padding-l-24 {
                padding-left: 24px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="no-print w3-button w3-blue w3-right glyphicon glyphicon-print" style="position: fixed; top: 40%; right: 10px; z-index: 4; border: 1px solid black; width: 84px;" onclick="window.print(); return false;">
                <p>
                    <br>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>
                </p>
            </div>
        </div>
        <!-- Page Content -->
        <div class="book">
            <div class="page printableArea pagecut" style="padding: 0px;">
                <div class="subpage">
                    <div class="col-xs-12 padding-0">
                        <div class="col-xs-5 padding-0">
                            <div class="col-xs-12 border">
                                <p class="bold text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132867") %></p>
                                <p><i class="far fa-circle"></i><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132868") %></span></p>
                                <p class="flex"><i class="far fa-circle"></i><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132869") %></span></p>
                                <p><i class="far fa-circle"></i><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132870") %></span></p>
                                <p><i class="far fa-circle"></i><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132871") %></span></p>
                                <p><i class="far fa-circle"></i><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132872") %></span></p>
                                <p><i class="far fa-circle"></i><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132926") %></span></p>
                            </div>
                            <div class="col-xs-12 border" style="border-top: 0px;">
                                <p class="bold text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132874") %></p>
                                <p><i class="far fa-circle"></i><span>สำเนาระเบียนแสดงผลการเรียน <span class="underline">(ปพ 1)</span></span></p>
                                <p><i class="far fa-circle"></i><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132876") %> <span class="underline">(ปศ 20)</span></span></p>
                                <p><i class="far fa-circle"></i><span>สำเนาใบรับรองการศึกษา <span class="underline"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132878") %></span></span></p>
                            </div>
                        </div>
                        <div class="col-xs-7 padding-0">
                            <div class="col-xs-12 padding-0">
                                <div class="col-xs-7 text-center">
                                    <img src="data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==" id="imgPage8Logo" runat="server" class="" style="width: 90px; margin-top: 12px;" />
                                </div>
                                <div class="col-xs-5 padding-0 text-right">
                                    <div class="input-row underline" style="width: 183px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132899") %>&nbsp;<span><%--2564--%><asp:Literal ID="ltrRegisterYear" runat="server"></asp:Literal></span></div>
                                    <p>&nbsp;</p>
                                    <div class="font22 input-row">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132927") %><div class="input-value" style="width: 80px;"><span><%--0000000--%><asp:Literal ID="ltrStudentID" runat="server"></asp:Literal></span></div>
                                    </div>
                                    <div class="font22 input-row">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132901") %><div class="input-value" style="width: 113px;"><span><%--0000000--%><asp:Literal ID="ltrRegisterLevel" runat="server"></asp:Literal></span></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 padding-0">
                                <div class="col-xs-7 text-center">
                                    <p class="font25 bold"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132903") %></p>
                                    <p class="font25 bold">
                                        <asp:Literal ID="ltrSchoolName" runat="server"></asp:Literal>
                                    </p>
                                    <p class="font10">&nbsp;</p>
                                    <p class="font22 bold"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132902") %></p>
                                </div>
                                <div class="col-xs-5 padding-0 text-right">
                                    <div class="col-xs-8 padding-0" style="height: 3.1cm; width: 2.8cm; border: 1px; border-color: #ccc; border-style: dashed; line-height: 2.5cm; text-align: center; font-size: 1.3em; color: #999; float: right;">
                                        <label class="font22" style="font-weight: normal; line-height: 23px; margin-top: 31px;">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132928") %><br />
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132929") %></label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 padding-0">
                                <div class="col-xs-7 padding-0 text-left" style="margin-left: 10px;">
                                    <p class="font10">&nbsp;</p>
                                    <p class="font10">&nbsp;</p>
                                    <div class="font22 input-row">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103190") %><div class="input-value" style="width: 170px;"><span><%--0000000--%><asp:Literal ID="ltrPlan" runat="server"></asp:Literal></span></div>
                                    </div>
                                    <div class="font22 input-row">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132904") %><div class="input-value" style="width: 156px;"><span><%--0000000--%><asp:Literal ID="ltrRegisterDate" runat="server"></asp:Literal></span></div>
                                    </div>
                                </div>
                                <div class="col-xs-5 padding-0 text-right" style="margin-left: -10px;">
                                    <div style="text-align: left; float: right; margin-top: 10px;">
                                        <p class="margin-b-n5"><i class="far fa<%= ltrReligion101.Text%>-square font16"></i><span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01228") %></span></p>
                                        <asp:Literal ID="ltrReligion101" runat="server" Visible="false"></asp:Literal>
                                        <p class="margin-b-n5"><i class="far fa<%= ltrReligionN001.Text%>-square font16"></i><span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132905") %></span></p>
                                        <asp:Literal ID="ltrReligionN001" runat="server" Visible="false"></asp:Literal>
                                        <p class="margin-b-n5"><i class="far fa<%= ltrReligion102.Text%>-square font16"></i><span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132906") %></span></p>
                                        <asp:Literal ID="ltrReligion102" runat="server" Visible="false"></asp:Literal>
                                        <p class="margin-b-n5"><i class="far fa<%= ltrReligion103.Text%>-square font16"></i><span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02325") %></span></p>
                                        <asp:Literal ID="ltrReligion103" runat="server" Visible="false"></asp:Literal>
                                        <p class="margin-b-n5"><i class="far fa<%= ltrReligion999.Text%>-square font16"></i><span>&nbsp;....................</span></p>
                                        <asp:Literal ID="ltrReligion999" runat="server" Visible="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 padding-lr-10 padding-t-10">
                            <div class="col-xs-12">
                                <div class="font22 input-row">
                                    <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></b><div class="input-value" style="width: 450px;"><span><%--0000000--%><asp:Literal ID="ltrStudentName" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132907") %><div class="input-value" style="width: 150px;"><span><%--0000000--%><asp:Literal ID="ltrStudentBirthday" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111037") %><div class="input-value" style="width: 50px;"><span><%--0000000--%><asp:Literal ID="ltrStudentAgeYear" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %><div class="input-value" style="width: 50px;"><span><%--0000000--%><asp:Literal ID="ltrStudentAgeMonth" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132908") %><div class="input-value" style="width: 160px;"><span><%--0000000--%><asp:Literal ID="ltrSymptoms" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132909") %><div class="input-value" style="width: 70px;"><span><%--0000000--%><asp:Literal ID="ltrStudentHomeNumber" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %><div class="input-value" style="width: 70px;"><span><%--0000000--%><asp:Literal ID="ltrStudentMuu" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %><div class="input-value" style="width: 170px;"><span><%--0000000--%><asp:Literal ID="ltrStudentSoy" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %><div class="input-value" style="width: 192px;"><span><%--0000000--%><asp:Literal ID="ltrStudentRoad" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %><div class="input-value" style="width: 170px;"><span><%--0000000--%><asp:Literal ID="ltrStudentTumbon" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %><div class="input-value" style="width: 170px;"><span><%--0000000--%><asp:Literal ID="ltrStudentAumpher" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %><div class="input-value" style="width: 175px;"><span><%--0000000--%><asp:Literal ID="ltrStudentProvince" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %><div class="input-value" style="width: 280px;"><span><%--0000000--%><asp:Literal ID="ltrStudentPost" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105084") %><div class="input-value" style="width: 291px;"><span><%--0000000--%><asp:Literal ID="ltrPhone" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %><div class="input-value" style="width: 40px;"><span><%--0000000--%><asp:Literal ID="ltrHouseRegistrationNumber" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %><div class="input-value" style="width: 40px;"><span><%--0000000--%><asp:Literal ID="ltrHouseRegistrationMuu" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %><div class="input-value" style="width: 90px;"><span><%--0000000--%><asp:Literal ID="ltrHouseRegistrationSoy" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %><div class="input-value" style="width: 90px;"><span><%--0000000--%><asp:Literal ID="ltrHouseRegistrationRoad" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %><div class="input-value" style="width: 108px;"><span><%--0000000--%><asp:Literal ID="ltrHouseRegistrationTumbon" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %><div class="input-value" style="width: 150px;"><span><%--0000000--%><asp:Literal ID="ltrHouseRegistrationAumpher" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %><div class="input-value" style="width: 150px;"><span><%--0000000--%><asp:Literal ID="ltrHouseRegistrationProvince" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106111") %><div class="input-value" style="width: 150px;"><span><%--0000000--%><asp:Literal ID="ltrIdentification" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></b><div class="input-value" style="width: 324px;"><span><%--0000000--%><asp:Literal ID="ltrFatherName" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106111") %><div class="input-value" style="width: 164px;"><span><%--0000000--%><asp:Literal ID="ltrFatherIdentification" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111037") %><div class="input-value" style="width: 60px;"><span><%--0000000--%><asp:Literal ID="ltrFatherAge" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %><div class="input-value" style="width: 150px;"><span><%--0000000--%><asp:Literal ID="ltrFatherRace" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %><div class="input-value" style="width: 150px;"><span><%--0000000--%><asp:Literal ID="ltrFatherNation" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %><div class="input-value" style="width: 150px;"><span><%--0000000--%><asp:Literal ID="ltrFatherReligion" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132910") %><div class="input-value" style="width: 600px;"><span><%--0000000--%><asp:Literal ID="ltrFatherGraduated" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %><div class="input-value" style="width: 437px;"><span><%--0000000--%><asp:Literal ID="ltrFatherJob" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02790") %><div class="input-value" style="width: 190px;"><span><%--0000000--%><asp:Literal ID="ltrFatherIncome" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132879") %><div class="input-value" style="width: 608px;"><span><%--0000000--%><asp:Literal ID="ltrFatherWorkPlace" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105084") %><div class="input-value" style="width: 270px;"><span><%--0000000--%><asp:Literal ID="ltrFatherPhone1" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132911") %><div class="input-value" style="width: 278px;"><span><%--0000000--%><asp:Literal ID="ltrFatherPhone2" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></b><div class="input-value" style="width: 309px;"><span><%--0000000--%><asp:Literal ID="ltrMotherName" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106111") %><div class="input-value" style="width: 164px;"><span><%--0000000--%><asp:Literal ID="ltrMotherIdentification" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111037") %><div class="input-value" style="width: 60px;"><span><%--0000000--%><asp:Literal ID="ltrMotherAge" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %><div class="input-value" style="width: 150px;"><span><%--0000000--%><asp:Literal ID="ltrMotherRace" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %><div class="input-value" style="width: 150px;"><span><%--0000000--%><asp:Literal ID="ltrMotherNation" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %><div class="input-value" style="width: 150px;"><span><%--0000000--%><asp:Literal ID="ltrMotherReligion" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132910") %><div class="input-value" style="width: 600px;"><span><%--0000000--%><asp:Literal ID="ltrMotherGraduated" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %><div class="input-value" style="width: 437px;"><span><%--0000000--%><asp:Literal ID="ltrMotherJob" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02790") %><div class="input-value" style="width: 190px;"><span><%--0000000--%><asp:Literal ID="ltrMotherIncome" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132879") %><div class="input-value" style="width: 608px;"><span><%--0000000--%><asp:Literal ID="ltrMotherWorkPlace" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105084") %><div class="input-value" style="width: 270px;"><span><%--0000000--%><asp:Literal ID="ltrMotherPhone1" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132911") %><div class="input-value" style="width: 278px;"><span><%--0000000--%><asp:Literal ID="ltrMotherPhone2" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 bold input-row"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132912") %></div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row"><i class="far fa<%= ltrFamilyStatus01.Text%>-circle"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121008") %></div>
                                <asp:Literal ID="ltrFamilyStatus01" runat="server" Visible="false"></asp:Literal>
                                <div class="font22 input-row"><i class="far fa<%= ltrFamilyStatus03.Text%>-circle padding-l-20"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102040") %></div>
                                <asp:Literal ID="ltrFamilyStatus03" runat="server" Visible="false"></asp:Literal>
                                <div class="font22 input-row"><i class="far fa-circle padding-l-20"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132913") %></div>
                                <div class="font22 input-row"><i class="far fa-circle padding-l-20"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132914") %></div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row"><i class="far fa<%= ltrFamilyStatus07.Text%>-circle"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132915") %></div>
                                <asp:Literal ID="ltrFamilyStatus07" runat="server" Visible="false"></asp:Literal>
                                <div class="font22 input-row"><i class="far fa<%= ltrFamilyStatus04.Text%>-circle padding-l-20"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101206") %></div>
                                <asp:Literal ID="ltrFamilyStatus04" runat="server" Visible="false"></asp:Literal>
                                <div class="font22 input-row"><i class="far fa<%= ltrFamilyStatus05.Text%>-circle padding-l-20"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103105") %></div>
                                <asp:Literal ID="ltrFamilyStatus05" runat="server" Visible="false"></asp:Literal>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row"><i class="far fa<%= ltrFamilyStatus06.Text%>-circle"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132916") %></div>
                                <asp:Literal ID="ltrFamilyStatus06" runat="server" Visible="false"></asp:Literal>
                                <div class="font22 input-row">
                                    <i class="far fa<%= ltrFamilyStatus99.Text%>-circle padding-l-20"></i>อื่น ๆ
                                    <div class="input-value" style="width: 150px;"><span><%--0000000--%></span></div>
                                    <asp:Literal ID="ltrFamilyStatus99" runat="server" Visible="false"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="book">
            <div class="page printableArea pagecut" style="padding: 0px;">
                <div class="subpage">
                    <div class="col-xs-12 padding-0">
                        <div class="col-xs-12 padding-lr-10 padding-t-10">
                            <div class="col-xs-12 text-center">
                                <div class="font22 bold"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132880") %></div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132917") %></b><div class="input-value" style="width: 290px;"><span><%--0000000--%><asp:Literal ID="ltrParentName" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106111") %><div class="input-value" style="width: 164px;"><span><%--0000000--%><asp:Literal ID="ltrParentIdentification" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111037") %><div class="input-value" style="width: 60px;"><span><%--0000000--%><asp:Literal ID="ltrParentAge" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %><div class="input-value" style="width: 150px;"><span><%--0000000--%><asp:Literal ID="ltrParentRace" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %><div class="input-value" style="width: 150px;"><span><%--0000000--%><asp:Literal ID="ltrParentNation" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %><div class="input-value" style="width: 150px;"><span><%--0000000--%><asp:Literal ID="ltrParentReligion" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132918") %><div class="input-value" style="width: 124px;"><span><%--0000000--%><asp:Literal ID="ltrParentRelate" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132919") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132920") %><div class="input-value" style="width: 240px;"><span><%--0000000--%><asp:Literal ID="ltrParentGraduated" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %><div class="input-value" style="width: 437px;"><span><%--0000000--%><asp:Literal ID="ltrParentJob" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02790") %><div class="input-value" style="width: 190px;"><span><%--0000000--%><asp:Literal ID="ltrParentIncome" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132879") %><div class="input-value" style="width: 608px;"><span><%--0000000--%><asp:Literal ID="ltrParentWorkPlace" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105084") %><div class="input-value" style="width: 270px;"><span><%--0000000--%><asp:Literal ID="ltrParentPhone1" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132911") %><div class="input-value" style="width: 277px;"><span><%--0000000--%><asp:Literal ID="ltrParentPhone2" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 text-center">
                                <div class="font22 bold padding-t-10 padding-b-10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102049") %></div>
                            </div>
                            <div class="col-xs-12 input-row" style="padding-right: 7px;">
                                <div class="font22 input-row" style="width: 100%;">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132921") %><%=ltrSchoolName.Text %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132922") %></span><div class="input-value" style="width: 100%;"><span><%--0000000--%><asp:Literal ID="ltrOldSchoolName" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %><div class="input-value" style="width: 170px;"><span><%--0000000--%><asp:Literal ID="ltrOldSchoolTumbon" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %><div class="input-value" style="width: 170px;"><span><%--0000000--%><asp:Literal ID="ltrOldSchoolAumpher" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %><div class="input-value" style="width: 170px;"><span><%--0000000--%><asp:Literal ID="ltrOldSchoolProvince" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row">
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132923") %><div class="input-value" style="width: 316px;"><span><%--0000000--%><asp:Literal ID="ltrOldSchoolGraduated" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="font22 input-row">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206595") %><div class="input-value" style="width: 205px;"><span><%--0000000--%><asp:Literal ID="ltrOldSchoolGPA" runat="server"></asp:Literal></span></div>
                                </div>
                            </div>
                            <div class="col-xs-12 input-row padding-t-130">
                                <div class="col-xs-5"></div>
                                <div class="col-xs-6 font22 input-row padding-l-24" style="justify-content: center;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132924") %><div class="input-value" style="width: 205px;"><span><%--0000000--%></span></div>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %>
                                </div>
                                <div class="col-xs-1"></div>
                            </div>
                            <div class="col-xs-12 input-row padding-t-10">
                                <div class="col-xs-5"></div>
                                <div class="col-xs-6 font22 input-row" style="justify-content: center;">
                                    (<div class="input-value" style="width: 200px;"><span><%--0000000--%></span></div>
                                    )
                                </div>
                                <div class="col-xs-1"></div>
                            </div>
                            <div class="col-xs-12 input-row padding-t-10">
                                <div class="col-xs-5"></div>
                                <div class="col-xs-6 font22 text-center">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132925") %>
                                </div>
                                <div class="col-xs-1"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
