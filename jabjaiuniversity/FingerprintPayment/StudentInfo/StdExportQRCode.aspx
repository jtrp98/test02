<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StdExportQRCode.aspx.cs" Inherits="FingerprintPayment.StudentInfo.StdExportQRCode" %>

<!DOCTYPE html>

<html>
<head>
    <title></title>
    <link rel="stylesheet" href="/Styles/style-student-qrcode-print.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        @import url("/PreRegister/webfonts/TH_Sarabun/thsarabunnew.css");

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
        <asp:Literal ID="ltrPages" runat="server"></asp:Literal>
    </div>

    <script src="/StudentInfo/Scripts/jquery/1.12.2/jquery.min.js"></script>
    <script src="/StudentInfo/Scripts/html2canvas.js"></script>

    <script src="/StudentInfo/Scripts/jszip/dist/jszip.js" type="text/javascript"></script>
    <script src="/StudentInfo/Scripts/jszip-utils/dist/jszip-utils.js" type="text/javascript"></script>
    <script src="/StudentInfo/Scripts/jszip/vendor/FileSaver.js" type="text/javascript"></script>

    <script>

        var zip;
        var folder;
        var no = 0;
        var allRecords = 0;

        var zipFileName = "";

        function ExportQRCodeToZip(_zipFileName) {

            zip = new JSZip();

            // Init
            zipFileName = _zipFileName;
            allRecords = $('.book .page').length;
            folder = zip.folder(zipFileName);

            // Manage image to folder to zip
            recursiveAddImageToZip();
            //recursiveAddImageToZip2();
        }

        function recursiveAddImageToZip() {
            no++;
            if (no > allRecords) {
                zip.generateAsync({ type: "blob" }).then(function (content) {
                    saveAs(content, zipFileName + ".zip");
                });

                $(window.parent.document).find('#btnExportAllQRCode').prop('disabled', false);
                return;
            }

            html2canvas(document.querySelector("#divPage" + no)).then(canvas => {
                var imgData = canvas.toDataURL('image/png').split(',')[1]; // gets rid of the leading part of the URL

                setTimeout(function () {
                    folder.file($("#divPage" + no).attr('data-filename') + ".png", imgData, { base64: true });

                    recursiveAddImageToZip();
                }, 100);
                //folder.file($("#divPage" + no).attr('data-filename') + ".png", imgData, { base64: true });

                //recursiveAddImageToZip();
            });
        }

        function recursiveAddImageToZip2() {
            no++;
            html2canvas(document.querySelector("#divPage" + no)).then(canvas => {

                var a = document.createElement('a');
                a.href = canvas.toDataURL("image/png").replace("image/png", "image/octet-stream");
                a.download = $("#divPage" + no).attr('data-filename') + '.png';
                a.click();

                setTimeout(function () {
                    recursiveAddImageToZip2();
                }, 100);
            });
        }

        $(document).ready(function () {
            var no = 0;
            var allRecords = 0;

            var zipFileName = "";
        });

    </script>
</body>
</html>
