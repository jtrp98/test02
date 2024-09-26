<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DiplomaPrint2.aspx.cs" Inherits="FingerprintPayment.Diploma.DiplomaPrint2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" href="/images/School Bright logo only.png" sizes="16x16 32x32" type="image/png" />

    <title>Diploma Print - School Bright</title>

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

    <link rel="stylesheet" href="../Transcript/assets/css/bootstrap-print.css?v=<%=DateTime.Now.Ticks%>" />

    <style>
        @import url("../PreRegister/webfonts/TH_Sarabun/thsarabunnew.css?v=<%=DateTime.Now.Ticks%>");

        @font-face {
            font-family: JSYodthida;
            src: url('/Fonts/JS-Yodthida.woff');
        }

        @font-face {
            font-family: TFArluck;
            src: url('/Fonts/TF-Arluck.woff');
        }

        @font-face {
            font-family: THDanViVek;
            src: url('/Fonts/TH_Dan_Vi_Vek_ver_1.03.woff');
        }

        .font-jsyodthida {
            font-family: JSYodthida;
        }

        .font-tfarluck {
            font-family: TFArluck;
        }

        .font-thdanvivek {
            font-family: THDanViVek;
        }

        .font-size-50em {
            font-size: 5.0em;
        }

        .font-size-42em {
            font-size: 4.2em;
        }

        .font-size-38em {
            font-size: 3.8em;
        }

        .font-size-32em {
            font-size: 3.2em;
        }

        .font-size-30em {
            font-size: 3.0em;
        }

        .font-size-28em {
            font-size: 2.8em;
        }

        .font-size-27em {
            font-size: 2.7em;
        }

        .font-size-24em {
            font-size: 2.4em;
        }

        .font-size-20em {
            font-size: 2.0em;
        }

        .font-size-18em {
            font-size: 1.8em;
        }

        .font-size-15em-data {
            font-size: 1.5em;
            font-weight: bold;
        }

        .font-size-13em-data {
            font-size: 1.3em;
            font-weight: bold;
        }

        .font-size-11em-data {
            font-size: 1.1em;
            font-weight: bold;
        }

        .font-size-10em-data {
            font-size: 1.0em;
        }

        .font-size-08em-data {
            font-size: 0.8em;
        }

        .font-size-14em-data {
            font-size: 1.4em;
        }

        .font-size-10em-bold {
            font-size: 1.0em;
            font-weight: bold;
        }

        .font-size-10em {
            font-size: 1.0em;
        }

        .font-size-08em {
            font-size: 0.8em;
        }

        .font-size-14em {
            font-size: 1.4em;
        }

        .font-size-08em-bold {
            font-size: 0.8em;
            font-weight: bold;
        }

        .font-size-17em-data {
            font-size: 1.7em;
        }

        .font-size-18em-data {
            font-size: 1.8em;
        }

        .font-size-25em-data {
            font-size: 2.5em;
        }

        .font-size-16em-data {
            font-size: 1.6em;
        }

        .font-size-20em-data {
            font-size: 2.0em;
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
        }

        .page {
            /*width: 796px;
            min-height: 556px;*/
            width: 21cm;
            min-height: 14.5cm;
            padding: 0cm 1.6cm 0cm 1.6cm;
            margin: 0.8cm auto;
            border: 1px;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            background-image: url('/images/bg-diploma-2.png');
            background-size: contain;
            background-repeat: no-repeat;
        }

            .page.front {
                background-image: url('/images/bg-front-diploma-3.png');
                background-position: 1pt 2pt;
            }

            .page.back {
                background-image: url('/images/bg-back-diploma-4.png');
                background-position: 2pt 4pt;
            }

        .subpage {
        }

        .page-cut {
            page-break-after: always;
            page-break-inside: avoid;
        }

        @page {
            /*size: 8.3in 5.8in;*/
            size: 21cm 14.5cm;
            margin: 0%;
            padding: 0%;
        }

        @media print {

            html, body {
                margin: 0;
                padding: 0;
            }

            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
            }

            .input-data.print-data-only-input {
                border-bottom: dotted 0px #999 !important;
            }

            .spn-label-print.print-data-only-label {
                visibility: hidden !important;
            }

            .no-print-me {
                display: none !important;
            }

            .print-data-only {
                visibility: hidden !important;
            }
        }

        .print-diploma {
            padding: 8px 16px;
            text-align: center;
            cursor: pointer;
            white-space: nowrap;
            color: #fff !important;
            background-color: #2196F3 !important;
            position: fixed;
            top: 22%;
            right: 10px;
            border: 1px solid black;
        }

            .print-diploma:hover {
                color: #000 !important;
                background-color: #ccc !important
            }

        .input-data {
            white-space: nowrap;
            border-bottom: dotted 1px #999;
            text-align: center;
            display: inline-block;
            height: 20px;
            /*margin: 0 2px 0 2px;*/
        }

        .transcript input[type=checkbox] {
            /* Double-sized Checkboxes */
            -ms-transform: scale(1.3); /* IE */
            -moz-transform: scale(1.3); /* FF */
            -webkit-transform: scale(1.3); /* Safari and Chrome */
            -o-transform: scale(1.3); /* Opera */
            transform: scale(1.3);
            padding: 10px;
            cursor: pointer;
        }

        .print-data-only {
            color: #ddd;
        }

        .print-data-only-input {
            border-bottom: dotted 1px #ddd;
        }

        .print-data-only-label {
            display: none;
        }

        .spn-label {
            letter-spacing: -0.8pt;
        }

        .back .spn-label {
            letter-spacing: 0.6pt;
        }
    </style>
</head>
<body>
    <div class="transcript">
        <div>
            <div class="form-check no-print-me" style="position: fixed; top: 14%; right: 30px;">
                <input class="form-check-input" type="checkbox" value="" id="chkPrintDataOnly" />
                <label class="form-check-label" for="chkPrintDataOnly">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206398") %>
                </label>
            </div>
            <div class="form-check no-print-me" style="position: fixed; top: 17%; right: 10px;">
                <input class="form-check-input" type="checkbox" value="" id="chkShowDirectorName" checked />
                <label class="form-check-label" for="chkShowDirectorName">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206399") %>
                </label>
            </div>
            <div class="print-diploma no-print-me glyphicon glyphicon-print " onclick="window.print();">
                <p>
                    <br />
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>
                </p>
            </div>
        </div>
        <div class="page <%--front--%>">
            <div class="subpage">
                <div class="row" style="height: 15pt;"><%--20pt--%>
                    <%--height: 10px;--%>
                    &nbsp;
                </div>
                <div class="row">
                    <div class="col-md-12 text-right" style="height: 18pt;">
                        <span class="font-tfarluck font-size-18em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132092") %> : </span><span class="font-tfarluck font-size-18em spn-label"><%=diplomaQuery.SubLevel=="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111067") %>"?"บ":(diplomaQuery.SubLevel=="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111070") %>"?"<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>":"<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>")%></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-right" style="height: 23pt;">
                        <span class="font-tfarluck font-size-18em spn-label" style="padding-right: 6pt;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></span>
                        <p style="display: inline-block; margin: -2pt 0pt 2pt 0pt; width: 12%; height: 20pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-16em-data input-data" style="width: 100%; height: 100%; padding-right: 10pt;"><%=diplomaQuery.DiplomaCode%></span>
                        </p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center" style="height: 35pt;">
                        <span class="font-tfarluck font-size-38em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132084") %></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center" style="height: 38pt;">
                        <span class="font-tfarluck font-size-28em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132085") %></span>
                    </div>
                </div>
                <div class="row" style="margin-top: 0px; margin-bottom: 5pt;">
                    <%--margin-top: 8px; margin-bottom: 7px;--%>
                    <div class="col-md-12 text-center">
                        <p style="display: inline-block; margin: -3pt 6pt 2pt 0pt; width: 62%; height: 30pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-25em-data input-data" style="width: 100%; height: 100%;"><%=diplomaQuery.FullNameStudent%></span>
                        </p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center" style="height: 26pt; padding-right: 22pt;">
                        <%--height: 42px;--%>
                        <span class="font-tfarluck font-size-24em spn-label" style="padding-left: 3pt;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211008") %></span>
                        <p style="display: inline-block; margin: -2pt 0pt 2pt 0pt; width: 9%; height: 25pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-20em-data input-data" style="width: 100%; height: 100%;"><%=diplomaQuery.BirthDateDay%></span>
                        </p>
                        <span class="font-tfarluck font-size-24em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></span>
                        <p style="display: inline-block; margin: -2pt 0pt 2pt 0pt; width: 21%; height: 25pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-20em-data input-data" style="width: 100%; height: 100%; padding-right: 8pt;"><%=diplomaQuery.BirthDateMonth%></span>
                        </p>
                        <span class="font-tfarluck font-size-24em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211009") %></span>
                        <p style="display: inline-block; margin: -2pt 0pt 2pt 0pt; width: 12%; height: 25pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-20em-data input-data" style="width: 100%; height: 100%; padding-right: 8pt;"><%=diplomaQuery.BirthDateYear%></span>
                        </p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center" style="height: 25pt;">
                        <%--height: 33px;--%>
                        <span class="font-tfarluck font-size-24em spn-label"><%=diplomaQuery.SubLevel == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111067") %>" ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132095") %>" : (diplomaQuery.SubLevel == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111070") %>" ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132086") %>" : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132095") %>")%></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center" style="height: 26pt;">
                        <%--height: 38px;--%>
                        <span class="font-tfarluck font-size-24em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204038") %></span>
                        <p style="display: inline-block; margin: -2pt 8pt 2pt -1pt; width: 70%; height: 25pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-20em-data input-data" style="width: 100%; height: 100%; text-align: center;"><%=diplomaQuery.SchoolName%></span>
                        </p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center" style="height: 26pt;">
                        <%--height: 47px;--%>
                        <span class="font-tfarluck font-size-24em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></span>
                        <p style="display: inline-block; margin: 0pt 0pt 0pt 0pt; width: 23%; height: 23pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-18em-data input-data" style="width: 100%; height: 100%;"><%=diplomaQuery.Province%></span>
                        </p>
                        <span class="font-tfarluck font-size-24em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105072") %></span>
                        <p style="display: inline-block; margin: 0pt -60pt 0pt 0pt; width: 50%; height: 23pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-18em-data input-data" style="width: 100%; height: 100%; padding-left: 3pt;"><%=diplomaQuery.Owner%></span>
                        </p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center" style="height: 26pt; padding-right: 22pt;">
                        <span class="font-tfarluck font-size-24em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503060") %></span>
                        <p style="display: inline-block; margin: -2pt 0pt 2pt 0pt; width: 9%; height: 25pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-20em-data input-data" style="width: 100%; height: 100%;"><%=diplomaQuery.GraduationApprovalDateDay%></span>
                        </p>
                        <span class="font-tfarluck font-size-24em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></span>
                        <p style="display: inline-block; margin: -2pt 0pt 2pt 0pt; width: 21%; height: 25pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-20em-data input-data" style="width: 100%; height: 100%; padding-right: 8pt;"><%=diplomaQuery.GraduationApprovalDateMonth%></span>
                        </p>
                        <span class="font-tfarluck font-size-24em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211009") %></span>
                        <p style="display: inline-block; margin: -2pt 0pt 2pt 0pt; width: 12%; height: 25pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-20em-data input-data" style="width: 100%; height: 100%; padding-right: 8pt;"><%=diplomaQuery.GraduationApprovalDateYear%></span>
                        </p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <span class="font-tfarluck font-size-24em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132077") %></span>
                    </div>
                </div>
                <div class="row" style="height: 21pt;">
                    <%--height: 41px;--%>
                    &nbsp;
                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <span class="font-thdanvivek font-size-17em-data spn-label-print director-name">(<%=string.IsNullOrEmpty(diplomaQuery.FullNameSchoolDirector1)?diplomaQuery.FullNameSchoolDirector2:diplomaQuery.FullNameSchoolDirector1%>)</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center" style="margin-top: -3pt;">
                        <span class="font-thdanvivek font-size-17em-data spn-label-print director-name"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206510") %></span>
                    </div>
                </div>
            </div>
        </div>
        <div class="page<%-- back--%>">
            <div class="subpage">
                <div class="row" style="height: 167pt;"><%--155pt--%>
                    &nbsp;
                </div>
                <div class="row">
                    <div class="col-md-12 text-center" style="height: 41pt;">
                        <span class="font-size-10em-bold spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132096") %></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 text-left" style="height: 23pt;">
                        <p style="display: inline-block; margin: -6pt -2pt 2pt 44pt; width: 53%; height: 17pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-14em-data input-data" style="width: 100%; height: 100%; text-align: center;"></span>
                        </p>
                        <span class="font-size-08em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132089") %></span>
                    </div>
                    <div class="col-md-6 text-left">
                        <p style="display: inline-block; margin: -6pt -2pt 2pt 35pt; width: 53%; height: 17pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-14em-data input-data" style="width: 100%; height: 100%; text-align: center;"></span>
                        </p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 text-left" style="height: 23pt;">
                        <p style="display: inline-block; margin: -6pt -2pt 2pt 44pt; width: 53%; height: 17pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-14em-data input-data" style="width: 100%; height: 100%; text-align: center;"></span>
                        </p>
                        <span class="font-size-08em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132090") %></span>
                    </div>
                    <div class="col-md-6 text-left">
                        <span class="font-size-08em spn-label" style="margin-left: 35pt;">(</span>
                        <p style="display: inline-block; margin: -6pt 0pt 2pt 0pt; width: 47%; height: 17pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-14em-data input-data" style="width: 100%; height: 100%; text-align: center;"><%=diplomaQuery.FullNameRegistrar%></span>
                        </p>
                        <span class="font-size-08em spn-label">)</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 text-left" style="height: 46pt;">
                        <p style="display: inline-block; margin: -6pt -2pt 2pt 44pt; width: 53%; height: 17pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-14em-data input-data" style="width: 100%; height: 100%; text-align: center;"></span>
                        </p>
                        <span class="font-size-08em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132091") %></span>
                    </div>
                    <div class="col-md-6 text-left">
                        <span class="font-size-08em-bold spn-label" style="margin-left: 73pt;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206511") %></span>
                    </div>
                </div>
                <%------%>
                <div class="row">
                    <div class="col-md-6" style="height: 23pt;">
                    </div>
                    <div class="col-md-6 text-left">
                        <p style="display: inline-block; margin: -6pt 0pt 2pt 35pt; width: 52%; height: 17pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-14em-data input-data" style="width: 100%; height: 100%; text-align: center;"></span>
                        </p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6" style="height: 23pt;">
                    </div>
                    <div class="col-md-6 text-left">
                        <span class="font-size-08em spn-label" style="margin-left: 35pt;">(</span>
                        <p style="display: inline-block; margin: -6pt 0pt 2pt 0pt; width: 47%; height: 17pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-14em-data input-data" style="width: 100%; height: 100%; text-align: center;"><%=diplomaQuery.FullNameStudent%></span>
                        </p>
                        <span class="font-size-08em spn-label">)</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6" style="height: 23pt;">
                    </div>
                    <div class="col-md-6 text-left">
                        <span class="font-size-08em-bold spn-label" style="margin-left: 60pt;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132080") %></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6" style="height: 23pt;">
                    </div>
                    <div class="col-md-6 text-left">
                        <span class="font-size-08em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></span>
                        <p style="display: inline-block; margin: -7pt -3pt 2pt -4pt; width: 11%; height: 18pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-14em-data input-data" style="width: 100%; height: 100%; padding-left: 2pt;"><%=diplomaQuery.GraduationApprovalDateDay%></span>
                        </p>
                        <span class="font-size-08em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></span>
                        <p style="display: inline-block; margin: -7pt -2pt 2pt -4pt; width: 31%; height: 18pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-14em-data input-data" style="width: 100%; height: 100%; padding-left: 2pt;"><%=diplomaQuery.GraduationApprovalDateMonth%></span>
                        </p>
                        <span class="font-size-08em spn-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211009") %></span>
                        <p style="display: inline-block; margin: -7pt 0pt 2pt -4pt; width: 18%; height: 18pt; vertical-align: top;">
                            <span class="font-thdanvivek font-size-14em-data input-data" style="width: 100%; height: 100%; padding-left: 8pt;"><%=diplomaQuery.GraduationApprovalDateYear%></span>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $('#chkPrintDataOnly').click(function () {
                if ($(this).prop('checked')) {
                    $('.spn-label').addClass('print-data-only');
                    $('.input-data').addClass('print-data-only-input');
                }
                else {
                    $('.spn-label').removeClass('print-data-only');
                    $('.input-data').removeClass('print-data-only-input');
                }
            });

            $('#chkShowDirectorName').click(function () {
                if ($(this).prop('checked')) {
                    $('.director-name').removeClass('print-data-only-label');
                }
                else {
                    $('.director-name').addClass('print-data-only-label');
                }
            });

        });

    </script>
</body>
</html>
