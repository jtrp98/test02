<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="TeacherCardPrint2.aspx.cs" Inherits="FingerprintPayment.TeacherCard.TeacherCardPrint2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></title>
    <link href="../Fonts/thsarabunnew.css?v=1" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <link href="/bootstrap SB2/bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <%-- <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">--%>
    <script src="../TeacherCard/Script/jquery.quickfit.js?v=3"></script>
    <style type="text/css">
        /*  @font-face {
            font-family: 'TH Sarabun';
            src: url('../Fonts/thsarabunnew.css');
        }

        @font-face {
            font-family: 'THSarabun';
            src: url('../Fonts/thsarabunnew.css');
        }*/

        html, body {
            /*     font-family: 'Angsana New' !important;*/
            /* font-size: 14px;
            line-height: 1.42857143;
            color: #333;
            background-color: #fff;*/
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
            white-space: nowrap
        }



        .w3-btn, .w3-button {
            -webkit-touch-callout: none;
            -webkit-user-select: none;
            -khtml-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none
        }

            .w3-button:hover {
                color: #000 !important;
                background-color: #ccc !important
            }


        @media print {

            html, body {
                width: 8.5cm;
                min-height: 5.4cm;
            }

            .no-print, .no-print * {
                display: none !important;
            }

            .page {
                width: 8.5cm;
                min-height: 5.4cm;
                size: landscape
            }

            .example-screen {
                display: none;
            }

            .example-print {
                display: block;
            }

            .FwhiteOnly12 span {
                color: #fff !important;
                -webkit-print-color-adjust: exact;
            }

            .FblackOnly12 span {
                color: #000 !important;
                -webkit-print-color-adjust: exact;
            }

            .FblueOnly12 span {
                color: #004DAA !important;
                -webkit-print-color-adjust: exact;
            }

            @media print and (-webkit-min-device-pixel-ratio:0) {
                .Fwhite {
                    color: #fff !important;
                    -webkit-print-color-adjust: exact;
                }

                .Fblack {
                    color: #000 !important;
                    -webkit-print-color-adjust: exact;
                }

                .Fblue {
                    color: #004DAA !important;
                    -webkit-print-color-adjust: exact;
                }


                .FwhiteOnly12 span {
                    color: #fff !important;
                    -webkit-print-color-adjust: exact;
                }

                .FblackOnly12 span {
                    color: #000 !important;
                    -webkit-print-color-adjust: exact;
                }

                .FblueOnly12 span {
                    color: #004DAA !important;
                    -webkit-print-color-adjust: exact;
                }
            }
        }

        #meassure {
            display: none;
        }

        p {
            margin: 0 !important;
            line-height: 1.358 !important;
        }
    </style>

    <style type="text/css" media="print">
        .pagecut {
            page-break-after: always;
            page-break-inside: avoid;
            border: 0px solid #FFF !important;
        }
    </style>

    <style>
        #card {
            background-image: url("<%= BackgroundCard %>") !important;
            height: <%= Height %>mm;
            width: <%= Width %>mm;
            background-repeat: no-repeat, no-repeat !important;
            background-position: top, left;
            background-size: cover !important;
            border-radius: 2px;
            border: 0px solid transparent;
            margin: 0 !important;
            padding: 0 !important;
            position: relative;
            top: 0;
            left: 0;
        }

        @media print {

            #card {
                -webkit-print-color-adjust: exact;
                border-radius: 0px !important;
                border: 0px !important;
            }
        }
    </style>
</head>
<body>

    <form id="form1" autocomplete="off" runat="server">

        <div id="printbutton" runat="server" class="example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall" style="position: fixed; top: 40%; right: 10px; z-index: 4; border: 1px solid black;"
            onclick="window.print()">
            <p>
                <br>
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>
            </p>
        </div>

        <!-- Page Content -->

        <asp:Literal runat="server" ID="ltrCard"></asp:Literal>


        <script>
            $(function () {
                $(".resizeableFont").quickfit({ max: 15, min: 13, truncate: false, width: 220 });
            });
        </script>
    </form>

</body>

</html>
