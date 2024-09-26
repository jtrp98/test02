<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DiplomaReport.aspx.cs" Inherits="FingerprintPayment.Diploma.DiplomaReport" %>

<!DOCTYPE html>

<html>
<head>
    <link rel="icon" href="/images/School Bright logo only.png" sizes="16x16 32x32" type="image/png" />

    <title>Diploma Report - School Bright</title>

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

    <link rel="stylesheet" href="/Transcript/assets/css/bootstrap-print.css?v=<%=DateTime.Now.Ticks%>" />

    <style>
        @import url("../PreRegister/webfonts/TH_Sarabun/thsarabunnew.css?v=<%=DateTime.Now.Ticks%>");

        @page {
            size: A4;
            margin: 0px;
        }

        body {
            font-family: THSarabunNew;
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            background-color: #FAFAFA;
        }

        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            /*-webkit-text-size-adjust: none;
            text-size-adjust: none;*/
        }

        .page {
            width: 210mm;
            min-height: 297mm;
            padding: 10mm 7mm 10mm 14mm;
            margin: 10mm auto;
            border: 1px;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }

            .page.report-1 {
                background-image: url('/images/bg-front-diploma-report-1.png');
                background-position: center;
                background-repeat: no-repeat;
                background-size: cover;
                color: red;
            }

            .page.report-2 {
                background-image: url('/images/bg-front-diploma-report-2.png');
                background-position: center;
                background-repeat: no-repeat;
                background-size: cover;
                color: red;
            }

        .subpage {
            padding: 0cm 0cm 0cm 0cm;
            border: 0px;
            height: 275mm;
            /*outline: 2cm;*/
        }

        .page-cut {
            page-break-after: always;
            page-break-inside: avoid;
        }

        @media print {
            html, body {
                width: 210mm;
                height: 297mm;
            }

            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                /*page-break-after: always;*/
            }

            .no-print-me {
                display: none !important;
            }

            .table-fixed {
                table-layout: fixed;
            }

                .table-fixed td {
                    white-space: nowrap;
                }
        }

        .table-fixed {
            table-layout: fixed;
        }

            .table-fixed td {
                white-space: nowrap;
            }

        .print-diploma {
            padding: 8px 16px;
            text-align: center;
            cursor: pointer;
            white-space: nowrap;
            color: #fff !important;
            background-color: #2196F3 !important;
            position: fixed;
            top: 20%;
            right: 10px;
            border: 1px solid black;
        }

            .print-diploma:hover {
                color: #000 !important;
                background-color: #ccc !important
            }

        .page-number {
            float: right;
            margin-right: 1.3cm;
        }

        .report-title {
            display: block;
            line-height: 1.75;
            font-size: 17px;
            font-weight: bold;
            letter-spacing: -0.9px;
        }

        .space-10cm {
            height: 1.0cm;
        }

        .space-13cm {
            height: 1.3cm;
        }

        .space-025cm {
            height: 0.25cm;
        }

        .space-030cm {
            height: 0.30cm;
        }

        .space-045cm {
            height: 0.45cm;
        }

        .student-table {
            border-collapse: collapse;
            border-spacing: 0;
        }

            .student-table th {
                border: 1px solid black;
                font-size: 14px;
                vertical-align: middle;
                padding-top: 5px;
            }

            .student-table td {
                border: 1px solid black;
                font-size: 14px;
                vertical-align: top;
                padding-top: 3px;
            }

                .student-table td span {
                    display: block;
                    text-align: center;
                    height: 0.73cm;
                }

        .diploma div.page:nth-child(n+3) .student-table td {
            padding-top: 5px;
            /*color: yellow;*/
        }

            .diploma div.page:nth-child(n+3) .student-table td span {
                height: 0.75cm;
            }

        .td-no {
            width: 1.6cm;
        }

        .td-code {
            width: 3cm;
        }

        .td-fullname {
            width: 4.8cm;
        }

        .td-birthday {
            width: 3.6cm;
        }

        .td-graduatedate {
            width: 3.8cm;
        }

        .td-note {
            width: 2cm;
        }

        .td-fullname span {
            padding-left: 5px;
            padding-right: 1px;
            text-align: left !important;
        }

        .td-birthday span {
            padding-left: 5px;
            padding-right: 1px;
            /*text-align: left !important;*/
        }

        td[id^='tdSummary'] span {
            display: block;
            text-align: left;
            line-height: 2;
            margin: 10px 0 0 3px;
        }

        td[id^='tdSummarySheet'] {
            vertical-align: top;
        }

            td[id^='tdSummarySheet'] span {
                display: block;
                margin: 15px 0 0 54px;
                letter-spacing: 1.2px;
            }

        .summary-table {
            float: right;
            margin: 16px 8px 0 0px;
        }

            .summary-table td {
                border: 1px solid black;
                font-size: 14px;
                width: 1.24cm;
            }

                .summary-table td span {
                    display: block;
                    text-align: center;
                }

            .summary-table tr {
                height: 0.75cm;
            }

        .signature-1 {
            /*display: block;*/
            padding: 0px 0 0 64px !important;
        }

            .signature-1 .sign {
                display: inline-block;
                width: 62%;
                height: 20px;
                vertical-align: top;
            }

            .signature-1 .sign-label {
                display: inline-block;
                margin-top: 7px;
            }

        .signature-2 {
            /*display: block;*/
            text-align: center;
            padding: 53px 0px 0px 0px;
        }

            .signature-2 .sign {
                display: inline-block;
                width: 41%;
                height: 17pt;
                vertical-align: top;
            }

            .signature-2 .sign-2 {
                display: inline-block;
                width: fit-content;
                min-width: 43%;
                vertical-align: top;
            }

        .signature-3 {
            /*display: block;*/
            text-align: center;
            padding: 31px 0px 0px 0px;
        }

            .signature-3 .sign {
                display: inline-block;
                width: 41%;
                height: 17pt;
                vertical-align: top;
            }

            .signature-3 .sign-2 {
                display: inline-block;
                width: fit-content;
                min-width: 43%;
                vertical-align: top;
            }

            .signature-3 .sign-3 {
                display: inline-block;
            }

            .signature-3 .sign-date {
                margin-top: 10px;
            }

        .input-data {
            white-space: nowrap;
            border-bottom: dotted 1px black;
            text-align: center;
            display: inline-block;
            width: 100%;
            height: 100%;
        }

        .input-data-no-dot {
            white-space: nowrap;
            border-bottom: none;
            text-align: center;
            display: inline-block;
            width: 100%;
            height: 100%;
        }

        .page-amount {
            display: contents !important;
        }

        .draft {
            display: none;
        }
    </style>
</head>
<body>
    <div class="diploma">
        <div>
            <div class="print-diploma no-print-me glyphicon glyphicon-print " onclick="window.print();">
                <p>
                    <br />
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>
                </p>
            </div>
        </div>
        <div class="page head draft">
            <div class="subpage">
                <div class="row space-13cm">
                    <div class="col-md-12">
                        <%--space 1.3 cm--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <span class="page-number" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %> ๑</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <img id="imgKrut" src="/Transcript/assets/img/krut-1.5-cm.png" alt="" class="" style="width: 1.5cm; height: 1.75cm; position: absolute; margin-top: 9px; margin-left: -5px;">
                    </div>
                </div>
                <div class="row space-025cm">
                    <div class="col-md-12">
                        <%--space 0.25 cm--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <span class="report-title" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132084") %><br />
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132097") %><br />
                            <span class="edu-level"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132098") %></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> <span class="edu-year"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132099") %></span><br />
                            <span class="school-name"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132100") %></span><br />
                            <span class="school-address"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132101") %></span><br />
                            <span class="school-owner"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132102") %></span></span>
                    </div>
                </div>
                <div class="row space-025cm">
                    <div class="col-md-12">
                        <%--space 0.25 cm--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="padding: 0px 0px 0px 18px;">
                        <table class="student-table table-fixed">
                            <tr style="height: 1.5cm">
                                <th class="text-center">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %></span>
                                </th>
                                <th class="text-center">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132103") %></span>
                                </th>
                                <th class="text-center">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132104") %></span>
                                </th>
                                <th class="text-center">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132105") %></span>
                                </th>
                                <th class="text-center">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132106") %>
                                        <br />
                                        การจบหลักสูตร</span>
                                </th>
                                <th class="text-center">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></span>
                                </th>
                            </tr>
                            <tr style="height: 9.6cm;">
                                <td class="td-no">
                                    <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>-->
                                </td>
                                <td class="td-code">
                                    <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132103") %>-->
                                </td>
                                <td class="td-fullname">
                                    <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132104") %>-->
                                </td>
                                <td class="td-birthday">
                                    <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132907") %>-->
                                </td>
                                <td class="td-graduatedate">
                                    <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132106") %> การจบหลักสูตร-->
                                </td>
                                <td class="td-note">
                                    <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %>-->
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="row space-045cm">
                    <div class="col-md-12">
                        <%--space 0.45 cm--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="padding: 0px 0px 0px 18px;">
                        <table class="table-fixed">
                            <tr style="height: 2cm;">
                                <td class="text-right" style="width: 9.4cm;">
                                    <table class="table-fixed">
                                        <tr style="height: 1cm;">
                                            <td style="width: 1.6cm;"></td>
                                            <td id="tdSummary" style="width: 3cm;">
                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132114") %><br />
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132115") %></span>
                                            </td>
                                            <td style="width: 4.8cm;">
                                                <table class="summary-table table-fixed">
                                                    <tr>
                                                        <td><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></span></td>
                                                        <td><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></span></td>
                                                        <td><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span class="male-amount"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132108") %>๗</span></td>
                                                        <td><span class="female-amount">๕๗</span></td>
                                                        <td><span class="total-amount">๑๒๔</span></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                                <td id="tdSummarySheet" style="width: 9.4cm;">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132107") %> <span class="page-amount"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132108") %></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132109") %></span>
                                </td>
                            </tr>
                            <tr style="height: 3.5cm;">
                                <td class="signature-1">
                                    <p style="margin: 0px;"></p>
                                    <p class="sign">
                                        <span class="input-data"><%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132110") %>--%></span>
                                    </p>
                                    <span class="sign-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132111") %></span><br />
                                    <p class="sign">
                                        <span class="input-data"><%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132112") %>--%></span>
                                    </p>
                                    <span class="sign-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132090") %></span><br />
                                    <p class="sign">
                                        <span class="input-data"><%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132113") %>--%></span>
                                    </p>
                                    <span class="sign-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132091") %></span><br />
                                </td>
                                <td class="signature-2">
                                    <p class="sign">
                                        <span class="input-data"></span>
                                    </p>
                                    <br />
                                    <span>(</span>
                                    <p class="sign-2">
                                        <span class="registrar input-data-no-dot"></span>
                                    </p>
                                    <span>)</span><br />
                                    <span class="" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206511") %></span>
                                </td>
                            </tr>
                            <tr style="height: 3.5cm;">
                                <td></td>
                                <td class="signature-3">
                                    <p class="sign">
                                        <span class="input-data"></span>
                                    </p>
                                    <br />
                                    <span>(</span>
                                    <p class="sign-2">
                                        <span class="director input-data-no-dot"></span>
                                    </p>
                                    <span>)</span><br />
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206510") %></span><br />
                                    <div class="sign-date">
                                        <span class=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></span>
                                        <p class="sign-3">
                                            <span class="graduate-day input-data-no-dot">๑๐</span>
                                        </p>
                                        <%--<span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></span>--%>
                                        <p class="sign-3">
                                            <span class="graduate-month input-data-no-dot"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %></span>
                                        </p>
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211009") %></span>
                                        <p class="sign-3">
                                            <span class="graduate-year input-data-no-dot">๒๕<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132108") %>๕</span>
                                        </p>
                                    </div>

                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="page other draft">
            <div class="subpage">
                <div class="row space-10cm">
                    <div class="col-md-12">
                        <%--space 1.0 cm--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <span class="page-number" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %> ๒</span>
                    </div>
                </div>
                <div class="row space-025cm">
                    <div class="col-md-12">
                        <%--space 0.25 cm--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <span class="report-title" style="">การออกประกาศนียบัตร</span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> <span class="edu-year"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132099") %></span></span>
                    </div>
                </div>
                <div class="row space-045cm">
                    <div class="col-md-12">
                        <%--space 0.45 cm--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="padding: 0px 0px 0px 18px;">
                        <table class="student-table table-fixed">
                            <tr style="height: 1.5cm">
                                <th class="text-center">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %></span>
                                </th>
                                <th class="text-center">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132103") %></span>
                                </th>
                                <th class="text-center">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132104") %></span>
                                </th>
                                <th class="text-center">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132907") %></span>
                                </th>
                                <th class="text-center">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132106") %>
                                        <br />
                                        การจบหลักสูตร</span>
                                </th>
                                <th class="text-center">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></span>
                                </th>
                            </tr>
                            <tr style="height: 18.2cm;">
                                <td class="td-no">
                                    <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>-->
                                </td>
                                <td class="td-code">
                                    <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132103") %>-->
                                </td>
                                <td class="td-fullname">
                                    <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132104") %>-->
                                </td>
                                <td class="td-birthday">
                                    <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132907") %>-->
                                </td>
                                <td class="td-graduatedate">
                                    <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132106") %> การจบหลักสูตร-->
                                </td>
                                <td class="td-note">
                                    <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %>-->
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="row space-030cm">
                    <div class="col-md-12">
                        <%--space 0.30 cm--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="padding: 0px 0px 0px 8px;">
                        <table class="table-fixed">
                            <tr style="height: 3.5cm;">
                                <td class="signature-1" style="width: 9.4cm;">
                                    <p style="margin: 0px;"></p>
                                    <p class="sign">
                                        <span class="input-data"></span>
                                    </p>
                                    <span class="sign-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132111") %></span><br>
                                    <p class="sign">
                                        <span class="input-data"></span>
                                    </p>
                                    <span class="sign-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132090") %></span><br>
                                    <p class="sign">
                                        <span class="input-data"></span>
                                    </p>
                                    <span class="sign-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132091") %></span><br>
                                </td>
                                <td class="signature-2" style="width: 9.4cm; padding: 52px 0px 0px 10px;">
                                    <p class="sign">
                                        <span class="input-data"></span>
                                    </p>
                                    <br>
                                    <span>(</span>
                                    <p class="sign-2">
                                        <span class="registrar input-data-no-dot"></span>
                                    </p>
                                    <span>)</span><br>
                                    <span class="" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206511") %></span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <!--  Plugin for Sweet Alert -->
    <script src="/PreRegister/assets/js/plugins/sweetalert2.js"></script>

    <script type="text/javascript"> 

        const thaiNumberSymbol = ["๐", "๑", "๒", "๓", "๔", "๕", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132108") %>", "๗", "๘", "๙"];

        function ConvertNumberTo<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %>Number(number) {
            if (!number) return "";

            var thaiNumber = "";

            for (var i = 0; i < number.length; i++) {
                if (isNaN(parseInt(number.charAt(i)))) {
                    thaiNumber += number.charAt(i);
                }
                else {
                    thaiNumber += thaiNumberSymbol[parseInt(number.charAt(i))];
                }
            }

            return thaiNumber;
        }

        function LoadDiplomaReportData(yearId, levelId) {
            $.ajax({
                async: false,
                type: "POST",
                url: 'DiplomaReport.aspx/LoadDiplomaReportData',
                data: JSON.stringify({ yearId: yearId, levelId: levelId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var r = JSON.parse(result.d);
                    if (r.success) {

                        var pageNo = 1;
                        var firstPageMaxStudentCount = 13;
                        var otherPageMaxStudentCount = 24;

                        $.each(r.diplomaReportData.sets, function (index, set) {

                            // Reset page no when new head report
                            pageNo = 1;

                            // First Page
                            var draftHeadClone = $('.page.head.draft').clone().removeClass('draft');
                            $(draftHeadClone).find('.page-number').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %> ' + pageNo++);
                            for (var i = 0; i < set.studentList.length && i < firstPageMaxStudentCount; i++) {
                                $(draftHeadClone).find('.student-table .td-no').append('<span>' + set.studentList[i].no<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> + '</span>');
                                $(draftHeadClone).find('.student-table .td-code').append('<span>' + set.studentList[i].diplomaCode<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> + '</span>');
                                $(draftHeadClone).find('.student-table .td-fullname').append('<span>' + set.studentList[i].fullname + '</span>');
                                $(draftHeadClone).find('.student-table .td-birthday').append('<span>' + set.studentList[i].birthday<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> + '</span>');
                                $(draftHeadClone).find('.student-table .td-graduatedate').append('<span>' + set.studentList[i].graduateDate<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> + '</span>');
                                $(draftHeadClone).find('.student-table .td-note').append('<span>' + set.studentList[i].note + '</span>');
                            }
                            $(draftHeadClone).find('.male-amount').text(set.maleAmount);
                            $(draftHeadClone).find('.female-amount').text(set.femaleAmount);
                            $(draftHeadClone).find('.total-amount').text(set.totalAmount);
                            //$(draftHeadClone).find('.page-amount').text(set.pageAmount);

                            $(draftHeadClone).find('.graduate-day').text(set.graduateDay);
                            $(draftHeadClone).find('.graduate-month').text(set.graduateMonth);
                            $(draftHeadClone).find('.graduate-year').text(set.graduateYear);

                            $('.diploma').append(draftHeadClone);

                            // Other Page
                            if (set.pageAmount > 1) {
                                for (var i = 2, j = firstPageMaxStudentCount; i <= set.pageAmount; i++) {

                                    var draftClone = $(".page.other.draft").clone().removeClass('draft');
                                    $(draftClone).find('.page-number').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %> ' + pageNo++);

                                    for (var k = 0; k < otherPageMaxStudentCount && j < set.studentList.length; j++, k++) {

                                        $(draftClone).find('.student-table .td-no').append('<span>' + set.studentList[j].no<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> + '</span>');
                                        $(draftClone).find('.student-table .td-code').append('<span>' + set.studentList[j].diplomaCode<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> + '</span>');
                                        $(draftClone).find('.student-table .td-fullname').append('<span>' + set.studentList[j].fullname + '</span>');
                                        $(draftClone).find('.student-table .td-birthday').append('<span>' + set.studentList[j].birthday<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> + '</span>');
                                        $(draftClone).find('.student-table .td-graduatedate').append('<span>' + set.studentList[j].graduateDate<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> + '</span>');
                                        $(draftClone).find('.student-table .td-note').append('<span>' + set.studentList[j].note + '</span>');
                                    }

                                    $('.diploma').append(draftClone);
                                }
                            }

                        });


                        // Set all page data
                        $('.edu-level').text(r.diplomaReportData.level<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %>);
                        $('.edu-year').text(r.diplomaReportData.year<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %>);
                        $('.school-name').text(r.diplomaReportData.school);
                        $('.school-address').text(r.diplomaReportData.address);
                        $('.school-owner').text(r.diplomaReportData.owner);
                        $('.registrar').text(r.diplomaReportData.registrar);
                        $('.director').text(r.diplomaReportData.director);
                        $('.page-amount').text(r.diplomaReportData.pageAmount);
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
                error: onError
            });
        }

        function onError(xhr, errorType, exception) {
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

        $(document).ready(function () {

            LoadDiplomaReportData('<%=Request.QueryString["year"]%>', '<%=Request.QueryString["level"]%>');

        });

    </script>
</body>
</html>
