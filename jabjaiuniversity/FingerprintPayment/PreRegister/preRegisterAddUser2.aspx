<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="preRegisterAddUser2.aspx.cs" Inherits="FingerprintPayment.PreRegister.preRegisterAddUser2" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />

    <%--<script src="//code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.8.0/jszip.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.8.0/xlsx.js"></script>
    <object id="objSecuBSP" style="left: 0px; top: 0px" height="0" width="0" classid="CLSID:6283f7ea-608c-11dc-8314-0800200c9a66"
        name="objSecuBSP" viewastext>
    </object>
    <style type="text/css">
        /* Tooltip */
        .tooltip {
            position: relative;
            display: inline-block;
            /*border-bottom: 1px dotted black;*/
            opacity: 1;
            right: -10px;
            width: 0px;
        }

            .tooltip i {
                /*color: #f74747;*/
            }

            .tooltip .tooltiptext {
                visibility: hidden;
                width: 220px;
                background-color: #555;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 5px 0;
                position: absolute;
                z-index: 1;
                bottom: 125%;
                left: 50%;
                margin-left: -60px;
                opacity: 0;
                transition: opacity 0.3s;
                font-size: 2rem;
            }

                .tooltip .tooltiptext::after {
                    content: "";
                    position: absolute;
                    top: 100%;
                    left: 25%;
                    margin-left: 0px;
                    margin-top: -1px;
                    border-width: 5px;
                    border-style: solid;
                    border-color: #555 transparent transparent transparent;
                }

            .tooltip:hover .tooltiptext {
                visibility: visible;
                opacity: 1;
            }
    </style>

    <script type="text/javascript">

        function ddlcheck2() {
            var ddlclassroom = document.getElementsByClassName("ddlclassroom");
            var ddltime = document.getElementsByClassName("ddltime");
            var ddltype1 = document.getElementsByClassName("ddltype1");
            var ddltype2 = document.getElementsByClassName("ddltype2");
            var idlvtxt = document.getElementsByClassName("idlvtxt");
            var idlvtxt2 = document.getElementsByClassName("idlvtxt2");
            var hiddentime = document.getElementsByClassName("hiddentime");
            var hidden1 = document.getElementsByClassName("hidden1");
            var hidden2 = document.getElementsByClassName("hidden2");

            var idlv = ddlclassroom[0].value.split('/');
            idlvtxt[0].value = idlv[1];
            idlvtxt2[0].value = idlv[0];

            //if (idlv[1] == 1) {
            //    hiddentime[0].classList.remove('hidden');
            //    hidden1[0].classList.remove('hidden');
            //    hidden2[0].classList.add('hidden');
            //}
            //else if (idlv[1] == 2) {
            //    hiddentime[0].classList.remove('hidden');
            //    hidden1[0].classList.add('hidden');
            //    hidden2[0].classList.remove('hidden');
            //}
            //else {
            //    hiddentime[0].classList.add('hidden');
            //    hidden1[0].classList.add('hidden');
            //    hidden2[0].classList.add('hidden');
            //}

            var levelName = ddlclassroom[0].options[ddlclassroom[0].selectedIndex].text;
            if (levelName.includes("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>")) {
                hiddentime[0].classList.remove('hidden');
                hidden1[0].classList.remove('hidden');
                hidden2[0].classList.add('hidden');
            } else if (levelName.includes("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>")) {
                hiddentime[0].classList.remove('hidden');
                hidden1[0].classList.add('hidden');
                hidden2[0].classList.remove('hidden');
            } else {
                hiddentime[0].classList.add('hidden');
                hidden1[0].classList.add('hidden');
                hidden2[0].classList.add('hidden');
            }
        }

        function ddlCheck() {
            var select = document.getElementById("ctl00_MainContent_ddlprovince");
            var select2 = document.getElementById("ctl00_MainContent_txtAumper");
            var select3 = document.getElementById("ctl00_MainContent_txtTumbon");
            var option = document.createElement('option');
            var option2 = document.createElement('option');
            var option3 = document.createElement('option');
            option.text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>";
            option.value = "x";
            option2.text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>";
            option2.value = "x";
            option3.text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>";
            option3.value = "x";

            select.add(option, 0);
            select.selectedIndex = 0;
            select2.add(option2, 0);
            select2.selectedIndex = 0;
            select3.add(option3, 0);
            select3.selectedIndex = 0;
        }
        $(document).ready(function () {
            ddlcheck2();
            $("#ctl00_MainContent_fulLogo").change(function () {
                displayPreview(this);
            });
        });
        var _URL = window.URL || window.webkitURL;
        var _src = "";
        function displayPreview(files) {
            var file = files.files[0]
            var img = new Image();
            var sizeKB = file.size / 1024;
            var chk = true;
            img.onload = function () {
                $('#preview').append(img);
                if (img.width > 3333 || img.height > 3333) {
                    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131219") %>");
                    $('img[id*=imgLogo]').attr('src', '');
                    return false;
                }
                else {
                    readURL(files);
                    _src = $("#ctl00_MainContent_fulLogo").val();
                    return true;
                }
            }
            img.src = _URL.createObjectURL(file);
        }

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('img[id*=imgLogo]').attr('src', e.target.result);
                }
            }
            reader.readAsDataURL(input.files[0]);
        }

        function noSchool() {
            var oName = document.getElementsByClassName("oName");
            var oProvince = document.getElementsByClassName("oProvince");
            var oAumpher = document.getElementsByClassName("oAumpher");
            var oTumbon = document.getElementsByClassName("oTumbon");
            var oGPA = document.getElementsByClassName("oGPA");
            var oGradu = document.getElementsByClassName("oGradu");
            var check12 = document.getElementsByClassName("check12");
            var oMoveout = document.getElementsByClassName("oMoveout");

            if (check12[0].checked == true) {
                oName[0].classList.add('disable');
                oProvince[0].classList.add('disable');
                oAumpher[0].classList.add('disable');
                oTumbon[0].classList.add('disable');
                oGPA[0].classList.add('disable');
                oMoveout[0].classList.add('disable');
                oGradu[0].classList.add('disable');
            }
            if (check12[0].checked == false) {
                oName[0].classList.remove('disable');
                oProvince[0].classList.remove('disable');
                oAumpher[0].classList.remove('disable');
                oTumbon[0].classList.remove('disable');
                oGPA[0].classList.remove('disable');
                oMoveout[0].classList.remove('disable');
                oGradu[0].classList.remove('disable');
            }
        }
        function copyAddress(index) {

            var famHome = document.getElementsByClassName("famHome");
            var famSoy = document.getElementsByClassName("famSoy");
            var famMuu = document.getElementsByClassName("famMuu");
            var famRoad = document.getElementsByClassName("famRoad");
            var famProvince = document.getElementsByClassName("famProvince");
            var famAumpher = document.getElementsByClassName("famAumpher");
            var famTumbon = document.getElementsByClassName("famTumbon");
            var famPost = document.getElementsByClassName("famPost");
            var fatherHome = document.getElementsByClassName("fatherHome");
            var fatherSoy = document.getElementsByClassName("fatherSoy");
            var fatherMuu = document.getElementsByClassName("fatherMuu");
            var fatherRoad = document.getElementsByClassName("fatherRoad");
            var fatherProvince = document.getElementsByClassName("fatherProvince");
            var fatherAumpher = document.getElementsByClassName("fatherAumpher");
            var fatherTumbon = document.getElementsByClassName("fatherTumbon");
            var fatherProvince2 = document.getElementsByClassName("fatherProvince2");
            var fatherAumpher2 = document.getElementsByClassName("fatherAumpher2");
            var fatherTumbon2 = document.getElementsByClassName("fatherTumbon2");
            var fatherProvinceStatus = document.getElementsByClassName("fatherProvinceStatus");
            var fatherAumpherStatus = document.getElementsByClassName("fatherAumpherStatus");
            var fatherTumbonStatus = document.getElementsByClassName("fatherTumbonStatus");
            var fatherProvinceStatus2 = document.getElementsByClassName("fatherProvinceStatus2");
            var fatherAumpherStatus2 = document.getElementsByClassName("fatherAumpherStatus2");
            var fatherTumbonStatus2 = document.getElementsByClassName("fatherTumbonStatus2");
            var fatherPost = document.getElementsByClassName("fatherPost");
            var motherHome = document.getElementsByClassName("motherHome");
            var motherSoy = document.getElementsByClassName("motherSoy");
            var motherMuu = document.getElementsByClassName("motherMuu");
            var motherRoad = document.getElementsByClassName("motherRoad");
            var motherProvince = document.getElementsByClassName("motherProvince");
            var motherAumpher = document.getElementsByClassName("motherAumpher");
            var motherTumbon = document.getElementsByClassName("motherTumbon");
            var motherProvince2 = document.getElementsByClassName("motherProvince2");
            var motherAumpher2 = document.getElementsByClassName("motherAumpher2");
            var motherTumbon2 = document.getElementsByClassName("motherTumbon2");
            var motherPost = document.getElementsByClassName("motherPost");
            var motherProvinceStatus = document.getElementsByClassName("motherProvinceStatus");
            var motherAumpherStatus = document.getElementsByClassName("motherAumpherStatus");
            var motherTumbonStatus = document.getElementsByClassName("motherTumbonStatus");
            var motherProvinceStatus2 = document.getElementsByClassName("motherProvinceStatus2");
            var motherAumpherStatus2 = document.getElementsByClassName("motherAumpherStatus2");
            var motherTumbonStatus2 = document.getElementsByClassName("motherTumbonStatus2");
            var motherClick = document.getElementsByClassName("motherClick");
            var fatherClick = document.getElementsByClassName("fatherClick");


            if (fatherClick[0].checked == true) {
                fatherHome[0].value = famHome[0].value;
                fatherMuu[0].value = famMuu[0].value;
                fatherPost[0].value = famPost[0].value;
                fatherProvince2[0].value = famProvince[0].options[famProvince[0].selectedIndex].text;
                fatherAumpher2[0].value = famAumpher[0].options[famAumpher[0].selectedIndex].text;
                fatherTumbon2[0].value = famTumbon[0].options[famTumbon[0].selectedIndex].text;
                fatherRoad[0].value = famRoad[0].value;
                fatherSoy[0].value = famSoy[0].value;
                fatherHome[0].classList.add('disable');
                fatherMuu[0].classList.add('disable');
                fatherPost[0].classList.add('disable');
                fatherProvince[0].classList.add('disable');
                fatherAumpher[0].classList.add('disable');
                fatherTumbon[0].classList.add('disable');
                fatherRoad[0].classList.add('disable');
                fatherSoy[0].classList.add('disable');

                fatherProvinceStatus2[0].classList.remove('hidden');
                fatherAumpherStatus2[0].classList.remove('hidden');
                fatherTumbonStatus2[0].classList.remove('hidden');
                fatherProvinceStatus[0].classList.add('hidden');
                fatherAumpherStatus[0].classList.add('hidden');
                fatherTumbonStatus[0].classList.add('hidden');
            }
            else if (fatherClick[0].checked == false) {

                fatherHome[0].classList.remove('disable');
                fatherMuu[0].classList.remove('disable');
                fatherPost[0].classList.remove('disable');
                fatherProvince[0].classList.remove('disable');
                fatherAumpher[0].classList.remove('disable');
                fatherTumbon[0].classList.remove('disable');
                fatherRoad[0].classList.remove('disable');
                fatherSoy[0].classList.remove('disable');

                fatherProvinceStatus2[0].classList.add('hidden');
                fatherAumpherStatus2[0].classList.add('hidden');
                fatherTumbonStatus2[0].classList.add('hidden');
                fatherProvinceStatus[0].classList.remove('hidden');
                fatherAumpherStatus[0].classList.remove('hidden');
                fatherTumbonStatus[0].classList.remove('hidden');
            }

            if (motherClick[0].checked == true) {
                motherHome[0].value = famHome[0].value;
                motherMuu[0].value = famMuu[0].value;
                motherPost[0].value = famPost[0].value;
                motherProvince2[0].value = famProvince[0].options[famProvince[0].selectedIndex].text;
                motherAumpher2[0].value = famAumpher[0].options[famAumpher[0].selectedIndex].text;
                motherTumbon2[0].value = famTumbon[0].options[famTumbon[0].selectedIndex].text;
                motherRoad[0].value = famRoad[0].value;
                motherSoy[0].value = famSoy[0].value;
                motherHome[0].classList.add('disable');
                motherMuu[0].classList.add('disable');
                motherPost[0].classList.add('disable');
                motherProvince[0].classList.add('disable');
                motherAumpher[0].classList.add('disable');
                motherTumbon[0].classList.add('disable');
                motherRoad[0].classList.add('disable');
                motherSoy[0].classList.add('disable');

                motherProvinceStatus2[0].classList.remove('hidden');
                motherAumpherStatus2[0].classList.remove('hidden');
                motherTumbonStatus2[0].classList.remove('hidden');
                motherProvinceStatus[0].classList.add('hidden');
                motherAumpherStatus[0].classList.add('hidden');
                motherTumbonStatus[0].classList.add('hidden');


            }
            else if (motherClick[0].checked == false) {

                motherHome[0].classList.remove('disable');
                motherMuu[0].classList.remove('disable');
                motherPost[0].classList.remove('disable');
                motherProvince[0].classList.remove('disable');
                motherAumpher[0].classList.remove('disable');
                motherTumbon[0].classList.remove('disable');
                motherRoad[0].classList.remove('disable');
                motherSoy[0].classList.remove('disable');

                motherProvinceStatus2[0].classList.add('hidden');
                motherAumpherStatus2[0].classList.add('hidden');
                motherTumbonStatus2[0].classList.add('hidden');
                motherProvinceStatus[0].classList.remove('hidden');
                motherAumpherStatus[0].classList.remove('hidden');
                motherTumbonStatus[0].classList.remove('hidden');
            }

            if (index == "1")
                setTimeout(function () {
                    copyAddress();
                }, 1000);
        }
    </script>

    <style type="text/css">
        input[type="file"] {
            /*display: none;*/
            visibility: collapse;
            position: absolute;
            width: 0%;
        }

        .no-file {
            display: none;
        }

        span.ready {
            display: inline-block;
            color: #23A818;
            font-size: 21px;
            /*margin-top: -19px;*/
        }

        span.open-file {
            color: orange;
            font-size: 22px;
            /*margin-top: -19px;*/
            cursor: pointer;
            margin-right: 3px;
        }

        span.upload-text {
            vertical-align: middle;
            font-size: 23px;
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
                font-size: 23px;
            }

            .table.table-striped tbody tr td .col-form-label {
                text-align: right;
                padding: 5px 5px 0 0;
            }

        .no-assumption-sriracha {
            display: none;
        }

        h6, .h6 {
            font-size: 20px;
            margin: -5px 0px 0px 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        .preview {
            display: block;
            max-width: 230px;
            max-height: 95px;
            width: auto;
            height: auto;
            background: black;
        }

        .disable {
            pointer-events: none;
            background-color: #eeeeee;
        }

        .disable2 {
            pointer-events: none;
        }

        .hid {
            visibility: hidden;
        }

        .width100 {
            margin: 0 auto;
            width: 100%;
        }

        .contentBox {
            margin: 0 auto;
            width: 120%;
        }

            .contentBox .column70 {
                float: left;
                margin: 0;
                width: 55%;
            }

            .contentBox .column50 {
                float: left;
                margin: 0;
                width: 45%;
            }

        .pad0 {
            padding-right: 0px;
            padding-left: 0px;
        }

        .contentBox .column30 {
            float: left;
            margin: 0;
            width: 45%;
        }

        .radioButtonList {
            list-style: none;
            margin: 0;
            padding: 0;
        }

            .radioButtonList.horizontal li {
                display: inline;
            }

            .radioButtonList label {
                display: inline;
            }

        select:invalid {
            color: gray;
        }

        #loading {
            display: block;
            position: fixed;
            top: 0;
            left: 0;
            z-index: 100;
            width: 100vw;
            height: 100vh;
            background-color: rgba(192, 192, 192, 0.5);
            background-image: url("https://i.imgur.com/CgViPo0.gif");
            background-repeat: no-repeat;
            background-position: center;
        }

        .grey {
            color: gray;
        }

        .center {
            position: relative;
            align-content: center;
        }

        .righttext {
            position: relative;
            text-align: right;
        }

        .lefttext {
            position: relative;
            text-align: left;
        }
    </style>
    <style>
        .wizard {
            margin: 20px auto;
            background: #fff;
        }

            .wizard .nav-tabs {
                position: relative;
                margin: 40px auto;
                margin-bottom: 0;
                border-bottom-color: #e0e0e0;
            }

            .wizard > div.wizard-inner {
                position: relative;
            }

        .connecting-line {
            height: 2px;
            background: #e0e0e0;
            position: absolute;
            width: 80%;
            margin: 0 auto;
            left: 0;
            right: 0;
            top: 50%;
            z-index: 1;
        }

        .wizard .nav-tabs > li.active > a, .wizard .nav-tabs > li.active > a:hover, .wizard .nav-tabs > li.active > a:focus {
            color: #555555;
            cursor: default;
            border: 0;
            border-bottom-color: transparent;
        }

        span.round-tab {
            width: 70px;
            height: 70px;
            line-height: 70px;
            display: inline-block;
            border-radius: 100px;
            background: #fff;
            border: 2px solid #e0e0e0;
            z-index: 2;
            position: absolute;
            left: 0;
            text-align: center;
            font-size: 25px;
        }

            span.round-tab i {
                color: #555555;
            }

        .wizard li.active span.round-tab {
            background: #fff;
            border: 2px solid #5bc0de;
        }

            .wizard li.active span.round-tab i {
                color: #5bc0de;
            }

        span.round-tab:hover {
            color: #333;
            border: 2px solid #333;
        }

        .wizard .nav-tabs > li {
            width: 20%;
        }

        .wizard li:after {
            content: " ";
            position: absolute;
            left: 46%;
            opacity: 0;
            margin: 0 auto;
            bottom: 0px;
            border: 5px solid transparent;
            border-bottom-color: #5bc0de;
            transition: 0.1s ease-in-out;
        }

        .wizard li.active:after {
            content: " ";
            position: absolute;
            left: 46%;
            opacity: 1;
            margin: 0 auto;
            bottom: 0px;
            border: 10px solid transparent;
            border-bottom-color: #5bc0de;
        }

        .wizard .nav-tabs > li a {
            width: 70px;
            height: 70px;
            margin: 20px auto;
            border-radius: 100%;
            padding: 0;
        }

            .wizard .nav-tabs > li a:hover {
                background: transparent;
            }

        .wizard .tab-pane {
            position: relative;
            padding-top: 50px;
        }

        .wizard h3 {
            margin-top: 0;
        }

        .step1 .row {
            margin-bottom: 10px;
        }

        .step_21 {
            border: 1px solid #eee;
            border-radius: 5px;
            padding: 10px;
        }

        .step33 {
            border: 1px solid #ccc;
            border-radius: 5px;
            padding-left: 10px;
            margin-bottom: 10px;
        }

        .dropselectsec {
            width: 68%;
            padding: 6px 5px;
            border: 1px solid #ccc;
            border-radius: 3px;
            color: #333;
            margin-left: 10px;
            outline: none;
            font-weight: normal;
        }

        .dropselectsec1 {
            width: 74%;
            padding: 6px 5px;
            border: 1px solid #ccc;
            border-radius: 3px;
            color: #333;
            margin-left: 10px;
            outline: none;
            font-weight: normal;
        }

        .mar_ned {
            margin-bottom: 10px;
        }

        .wdth {
            width: 25%;
        }

        .birthdrop {
            padding: 6px 5px;
            border: 1px solid #ccc;
            border-radius: 3px;
            color: #333;
            margin-left: 10px;
            width: 16%;
            outline: 0;
            font-weight: normal;
        }

        /* according menu */
        #accordion-container {
            font-size: 13px;
        }

        .accordion-header {
            font-size: 13px;
            background: #ebebeb;
            margin: 5px 0 0;
            padding: 7px 20px;
            cursor: pointer;
            color: #fff;
            font-weight: 400;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
        }

        .unselect_img {
            width: 18px;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

        .active-header {
            -moz-border-radius: 5px 5px 0 0;
            -webkit-border-radius: 5px 5px 0 0;
            border-radius: 5px 5px 0 0;
            background: #F53B27;
        }

            .active-header:after {
                content: "\f068";
                font-family: 'FontAwesome';
                float: right;
                margin: 5px;
                font-weight: 400;
            }

        .inactive-header {
            background: #333;
        }

            .inactive-header:after {
                content: "\f067";
                font-family: 'FontAwesome';
                float: right;
                margin: 4px 5px;
                font-weight: 400;
            }

        .accordion-content {
            display: none;
            padding: 20px;
            background: #fff;
            border: 1px solid #ccc;
            border-top: 0;
            -moz-border-radius: 0 0 5px 5px;
            -webkit-border-radius: 0 0 5px 5px;
            border-radius: 0 0 5px 5px;
        }

            .accordion-content a {
                text-decoration: none;
                color: #333;
            }

            .accordion-content td {
                border-bottom: 1px solid #dcdcdc;
            }

        @media( max-width : 585px ) {

            .wizard {
                width: 90%;
                height: auto !important;
            }

            span.round-tab {
                font-size: 16px;
                width: 50px;
                height: 50px;
                line-height: 50px;
            }

            .wizard .nav-tabs > li a {
                width: 50px;
                height: 50px;
                line-height: 50px;
            }

            .wizard li.active:after {
                content: " ";
                position: absolute;
                left: 35%;
            }
        }
    </style>
    <style type="text/css">
        div.TProduct:hover {
            color: blue;
        }

        .width-20 {
            width: 20px;
        }
    </style>
    <script language="Javascript" type="text/javascript">

        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }

    </script>

    <script type="text/javascript">

        function setInputFilter(textbox, inputFilter) {
            ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
                textbox.addEventListener(event, function () {
                    if (inputFilter(this.value)) {
                        this.oldValue = this.value;
                        this.oldSelectionStart = this.selectionStart;
                        this.oldSelectionEnd = this.selectionEnd;
                    } else if (this.hasOwnProperty("oldValue")) {
                        this.value = this.oldValue;
                        this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                    }
                });
            });
        }

        function Radio_Click() {
            var radio1 = document.getElementById("<%=RadioButton4.ClientID %>");
            var textBox = document.getElementById("<%=TextBox54.ClientID %>");
            document.getElementById('<%= TextBox54.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }
        function Radio_Click2() {
            var radio1 = document.getElementById("<%=RadioButton6.ClientID %>");
            var textBox = document.getElementById("<%=TextBox17.ClientID %>");
            document.getElementById('<%= TextBox17.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }
        function Radio_Click3() {
            var radio1 = document.getElementById("<%=RadioButton8.ClientID %>");
            var textBox = document.getElementById("<%=TextBox36.ClientID %>");
            document.getElementById('<%= TextBox36.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }
        function Radio_Click4() {
            var radio1 = document.getElementById("<%=RadioButton10.ClientID %>");
            var textBox = document.getElementById("<%=TextBox41.ClientID %>");
            document.getElementById('<%= TextBox41.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }
        function Radio_Click5() {
            var radio1 = document.getElementById("<%=RadioButton12.ClientID %>");
            var textBox = document.getElementById("<%=TextBox44.ClientID %>");
            document.getElementById('<%= TextBox44.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }
        <%--function Radio_Click6() {
            var radio1 = document.getElementById("<%=RadioButton2.ClientID %>");
            var textBox = document.getElementById("<%=TextBox3.ClientID %>");
            document.getElementById('<%= TextBox3.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }--%>
        <%--function Radio_Click7() {
            var radio1 = document.getElementById("<%=RadioButton14.ClientID %>");
            var textBox = document.getElementById("<%=TextBox8.ClientID %>");
            document.getElementById('<%= TextBox8.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }--%>
        <%--function Radio_Click8() {
            var radio1 = document.getElementById("<%=RadioButton16.ClientID %>");
            var textBox = document.getElementById("<%=TextBox10.ClientID %>");
            document.getElementById('<%= TextBox10.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }--%>
        <%--function Radio_Click9() {
            var radio1 = document.getElementById("<%=fatherSunChadddl2.ClientID %>");
            var textBox = document.getElementById("<%=fatherSunChad.ClientID %>");
            document.getElementById('<%= fatherSunChad.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }--%>
        <%--function Radio_Click10() {
            var radio1 = document.getElementById("<%=fatherChurChadddl2.ClientID %>");
            var textBox = document.getElementById("<%=fatherChurChad.ClientID %>");
            document.getElementById('<%= fatherChurChad.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }--%>
        <%--function Radio_Click11() {
            var radio1 = document.getElementById("<%=fatherReligion4.ClientID %>");
            var textBox = document.getElementById("<%=fatherReligiontxt.ClientID %>");
            document.getElementById('<%= fatherReligiontxt.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }--%>
        <%--function Radio_Click12() {
            var radio1 = document.getElementById("<%=motherChurChad2.ClientID %>");
            var textBox = document.getElementById("<%=motherChurChadtxt.ClientID %>");
            document.getElementById('<%= motherChurChadtxt.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }--%>
        <%--function Radio_Click13() {
            var radio1 = document.getElementById("<%=motherSunChad2.ClientID %>");
            var textBox = document.getElementById("<%=motherSunChadtxt.ClientID %>");
            document.getElementById('<%= motherSunChadtxt.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }--%>
        <%--function Radio_Click14() {
            var radio1 = document.getElementById("<%=motherReligion4.ClientID %>");
            var textBox = document.getElementById("<%=motherReligiontxt.ClientID %>");
            document.getElementById('<%= motherReligiontxt.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }--%>
        <%--function Radio_Click15() {
            var radio1 = document.getElementById("<%=GuardianChurChad2.ClientID %>");
            var textBox = document.getElementById("<%=GuardianChurChadtxt.ClientID %>");
            document.getElementById('<%= GuardianChurChadtxt.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }--%>
        <%--function Radio_Click16() {
            var radio1 = document.getElementById("<%=GuardianSunChad2.ClientID %>");
            var textBox = document.getElementById("<%=GuardianSunChadtxt.ClientID %>");
            document.getElementById('<%= GuardianSunChadtxt.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }--%>
        <%--function Radio_Click17() {
            var radio1 = document.getElementById("<%=GuardianReligion4.ClientID %>");
            var textBox = document.getElementById("<%=GuardianReligiontxt.ClientID %>");
            document.getElementById('<%= GuardianReligiontxt.ClientID %>').value = "";
            textBox.disabled = !radio1.checked;
            textBox.focus();
        }--%>

        function loading() {
            var loading = document.getElementsByClassName("loading");
            loading[0].classList.remove('hidden');
        }

        function check1() {

            var pagetitle = document.getElementsByClassName("pagetitle");
            var pageson = document.getElementsByClassName("pageson");
            var pageson2 = document.getElementsByClassName("pageson2");
            var pageson3 = document.getElementsByClassName("pageson3");
            var pageprovince = document.getElementsByClassName("pageprovince");
            var pageaumpher = document.getElementsByClassName("pageaumpher");
            var pagetumbon = document.getElementsByClassName("pagetumbon");
            var titlecheck = document.getElementsByClassName("titlecheck");
            var birthcheck = document.getElementsByClassName("birthcheck");

            if (pagetitle[0].value == "0") {
                bootbox.alert({
                    message: '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103093") %></p></h>',
                    backdrop: true
                });
            }
            else if (birthcheck[0].value == "" || birthcheck[1].value == "") {
                bootbox.alert({
                    message: '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103150") %></p></h>',
                    backdrop: true
                });
            }
            else if (titlecheck[0].value == "0") {
                bootbox.alert({
                    message: '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103151") %></p></h>',
                    backdrop: true
                });
            }
            else if (pageson[0].value == "0") {
                bootbox.alert({
                    message: '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103152") %></p></h>',
                    backdrop: true
                });
            }
            else if (pageprovince[0].value == "0") {
                bootbox.alert({
                    message: '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103153") %></p></h>',
                    backdrop: true
                });
            }
            else if (pageaumpher[0].value == "0") {
                bootbox.alert({
                    message: '<h2><p class="centertext">กรุณาเลือกเขต/อำเภอ</p></h>',
                    backdrop: true
                });
            }
            else if (pagetumbon[0].value == "0") {
                bootbox.alert({
                    message: '<h2><p class="centertext">กรุณาเลือกแขวง/ตำบล</p></h>',
                    backdrop: true
                });
            }
            else if (pageson2[0].value == "0") {
                bootbox.alert({
                    message: '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103154") %></p></h>',
                    backdrop: true
                });
            }
            else if (pageson3[0].value == "-1") {
                bootbox.alert({
                    message: '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103155") %></p></h>',
                    backdrop: true
                });
            }
            else {
                var $active = $('.wizard .nav-tabs li.active');
                $active.next().removeClass('disabled');
                nextTab($active);
            }
        }

        function check2() {
            var pagegradu = document.getElementsByClassName("pagegradu");
            var pageprovince = document.getElementsByClassName("pageprovince");
            var pageaumpher = document.getElementsByClassName("pageaumpher");
            var pagetumbon = document.getElementsByClassName("pagetumbon");
            var check12 = document.getElementsByClassName("check12");

            if (check12[0].checked == false) {
                if (pagegradu[0].value == "0") {
                    bootbox.alert({
                        message: '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103156") %></p></h>',
                        backdrop: true
                    });
                }
                else if (pageprovince[3].value == "0") {
                    bootbox.alert({
                        message: '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103153") %></p></h>',
                        backdrop: true
                    });
                }
                else if (pageaumpher[3].value == "0") {
                    bootbox.alert({
                        message: '<h2><p class="centertext">กรุณาเลือกเขต/อำเภอ</p></h>',
                        backdrop: true
                    });
                }
                else if (pagetumbon[3].value == "0") {
                    bootbox.alert({
                        message: '<h2><p class="centertext">กรุณาเลือกแขวง/ตำบล</p></h>',
                        backdrop: true
                    });
                }
                else {
                    var $active = $('.wizard .nav-tabs li.active');
                    $active.next().removeClass('disabled');
                    nextTab($active);
                }
            }

            else {
                var $active = $('.wizard .nav-tabs li.active');
                $active.next().removeClass('disabled');
                nextTab($active);
            }
        }

        function check3() {
            var pagetitle = document.getElementsByClassName("pagetitle");
            var pageson = document.getElementsByClassName("pageson");
            var pageprovince = document.getElementsByClassName("pageprovince");
            var pageaumpher = document.getElementsByClassName("pageaumpher");
            var pagetumbon = document.getElementsByClassName("pagetumbon");

            if (pagetitle[1].value == "0") {
                bootbox.alert({
                    message: '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103157") %></p></h>',
                    backdrop: true
                });
            }
            else if (pagetitle[2].value == "0") {
                bootbox.alert({
                    message: '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103158") %></p></h>',
                    backdrop: true
                });
            }
            else if (pagetitle[3].value == "0") {
                bootbox.alert({
                    message: '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103159") %></p></h>',
                    backdrop: true
                });
            }
            else {
                var $active = $('.wizard .nav-tabs li.active');
                $active.next().removeClass('disabled');
                nextTab($active);
            }
        }

        function check4() {
            var $active = $('.wizard .nav-tabs li.active');
            $active.next().removeClass('disabled');
            nextTab($active);
        }
    </script>

    <script type="text/javascript" language="javascript">

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        $(document).ready(function () {

            setInputFilter(document.getElementById("ctl00_MainContent_sStudentHomeRegisterCode"), function (value) {
                return /^\d*$/.test(value);
            });
            setInputFilter(document.getElementById("ctl00_MainContent_citizenID"), function (value) {
                return /^\d*$/.test(value);
            });

            setInputFilter(document.getElementById("ctl00_MainContent_sPhone"), function (value) {
                return /^\d*$/.test(value);
            });
            setInputFilter(document.getElementById("ctl00_MainContent_sStudentHousePhone"), function (value) {
                return /^\d*$/.test(value);
            });
            setInputFilter(document.getElementById("ctl00_MainContent_stayWithEmergencyCall"), function (value) {
                return /^\d*$/.test(value);
            });
            setInputFilter(document.getElementById("ctl00_MainContent_friendPhone"), function (value) {
                return /^\d*$/.test(value);
            });
            setInputFilter(document.getElementById("ctl00_MainContent_houseRegistrationPhone"), function (value) {
                return /^\d*$/.test(value);
            });


            //Initialize tooltips
            $('.nav-tabs > li a[title]').tooltip();

            //Wizard
            $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {

                var $target = $(e.target);

                if ($target.parent().hasClass('disabled')) {
                    return false;
                }
            });

            $(".next-step").click(function (e) {



            });
            $(".prev-step").click(function (e) {

                var $active = $('.wizard .nav-tabs li.active');
                prevTab($active);

            });
        });

        function nextTab(elem) {
            $(elem).next().find('a[data-toggle="tab"]').click();
        }
        function prevTab(elem) {
            $(elem).prev().find('a[data-toggle="tab"]').click();
        }

        //according menu
        var maxFileSize = 2097152; // 2 MB

        $(document).ready(function () {
            //Add Inactive Class To All Accordion Headers
            $('.accordion-header').toggleClass('inactive-header');

            //Set The Accordion Content Width
            var contentwidth = $('.accordion-header').width();
            $('.accordion-content').css({});

            //Open The First Accordion Section When Page Loads
            $('.accordion-header').first().toggleClass('active-header').toggleClass('inactive-header');
            $('.accordion-content').first().slideDown().toggleClass('open-content');

            // The Accordion Effect
            $('.accordion-header').click(function () {
                if ($(this).is('.inactive-header')) {
                    $('.active-header').toggleClass('active-header').toggleClass('inactive-header').next().slideToggle().toggleClass('open-content');
                    $(this).toggleClass('active-header').toggleClass('inactive-header');
                    $(this).next().slideToggle().toggleClass('open-content');
                }

                else {
                    $(this).toggleClass('active-header').toggleClass('inactive-header');
                    $(this).next().slideToggle().toggleClass('open-content');
                }
            });


            $(".open-file").bind({
                click: function () {

                    $('input[type=file][data-did=' + $(this).data('did') + '][data-tid=' + $(this).data('tid') + ']').trigger('click');

                    return false;
                }
            });

            // Upload File
            $('input[type=file]').change(function () {
                ImagesPreview($(this)[0].files, $(this).data('did'), $(this).data('tid'), $(this).data('vfiid'));
            });

            // Multiple images preview in browser
            var ImagesPreview = function (files, docId, typeId, vfiId) {
                if (files) {
                    var filesAmount = files.length;
                    for (i = 0; i < filesAmount; i++) {
                        var file = files[i];

                        // Check file size
                        if (file.size > maxFileSize) { alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132772") %>'); continue; }

                        if (!$('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').hasClass('ready')) {
                            $('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').addClass('ready');
                            $('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').removeClass('no-file');
                        }
                    }
                }
            };

            return false;
        });
    </script>
    <script language="javascript" type="text/javascript">
        function j_OnConfirm(header, msg, controlName) {
            BoxOverlayCSS();
            Sexy.confirm("<br/><h1>&nbsp;&nbsp;" + header + "</h1><br/><p>&nbsp;&nbsp;&nbsp;" + msg + "</p>",
                {
                    onComplete:
                        function (returnvalue) {
                            if (returnvalue) {
                                fnRegister('1');
                                // __doPostBack(controlName, ''); //**
                            }
                        }
                });
        }
        function fnRegister(id) {
            var err, payload
            $('input[id*=txtCheckFinger' + id + ']').val("1");
            try // Exception handling
            {
                // Open device. [AUTO_DETECT]
                // You must open device before enrollment.
                DEVICE_FDP02 = 1;
                DEVICE_FDU02 = 2;
                DEVICE_FDU03 = 3;
                DEVICE_FDU04 = 4;
                DEVICE_FDU05 = 5; // HU20
                DEVICE_AUTO_DETECT = 255;

                document.objSecuBSP.OpenDevice(DEVICE_AUTO_DETECT);
                err = document.objSecuBSP.ErrorCode; // Get error code

                if (err != 0)       // Device open failed
                {
                    alert('Device open failed !');
                    return;
                }

                // Enroll user's fingerprint.
                document.objSecuBSP.Enroll(payload);
                err = document.objSecuBSP.ErrorCode; // Get error code

                if (err != 0) // Enroll failed
                {
                    // alert('Registration failed ! Error Number : [' + err + ']');
                    return;
                }
                else // Enroll success
                {
                    $('input[id*=txtUserFinger' + id + ']').val(document.objSecuBSP.FIRTextData);
                    var sID = $('input[id*=txtUserFinger' + id + ']').val();
                    $.ajax({
                        type: "POST",
                        url: "getUser.ashx",
                        cache: false,
                        dataType: "html",
                        data: { ID: encodeURIComponent(sID) },
                        success: function (response) {
                            var _str = response;
                            if (_str == "") {
                                $('span[id*=div' + id + ']').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121061") %>');
                                $('span[id*=div' + id + ']').css('color', 'green');
                                $('input[id*=txtCheckFinger' + id + ']').val("1");
                                //                            alert('Verification failed !');
                            }
                            else if (_str.split('|')[0] == $('span[id*=lblUserID]').html()) {
                                $('input[id*=txtCheckFinger' + id + ']').val() == "1";
                                $('span[id*=div' + id + ']').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121061") %>');
                                $('span[id*=div' + id + ']').css('color', 'green');
                            }
                            else {
                                //                            alert('Verification success !');
                                $('span[id*=div' + id + ']').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121060") %>');
                                $('span[id*=div' + id + ']').css('color', 'red');
                                $('input[id*=txtCheckFinger' + id + ']').val("");
                            }
                        }
                    });
                }
                // Close device. [AUTO_DETECT]
                document.objSecuBSP.CloseDevice(DEVICE_AUTO_DETECT);
            }
            catch (e) {
                // alert(e.message);
            }
            return;
        }
        $(document).ready(function () {
            $('.datepicker').datepicker({});
            $("#stdCheck").addClass("width-20 form-control");
            $("select[id*=ddlTypeProduct]").change(function () {
                var _type = $("select[id*=ddlTypeProduct]").val();
                $('div[id*=Type_]').hide();
                $("div[id*=Type_" + _type + "]").show();
            });
            $("#stdCheck").click(function () {
                var $this = $(this);
                if ($this.is(':checked')) {
                    $("#ctl00_MainContent_rowTel").removeClass("hide");
                }
                else {
                    $("#ctl00_MainContent_rowTel").addClass("hide");
                }
            });
            $("select[id*=ctl00_MainContent_ddlcType]").change(function () {
                if ($(this).val() == "0") {
                    $("#ctl00_MainContent_rowSMS").removeClass("hide");
                    $("#ctl00_MainContent_rowSalary").addClass("hide");
                    $("#ctl00_MainContent_rowLV").addClass("show");
                } else {
                    $("#ctl00_MainContent_rowSMS").addClass("hide");
                    $("#ctl00_MainContent_rowSalary").removeClass("hide");
                    $("#ctl00_MainContent_rowLV").addClass("hide");
                }
            });
            $("div[id*=TProduct_]").click(function (e, s) {
                var _text = $("input[id*=txtval]").val();
                var _html = $("div[id*=tdBlackList]").html();
                if (_text.indexOf(this.id + ",") == -1 || _text == "") {
                    _html += "<span id=" + this.id + ">" + $(this).html() + "</span><img src='images/action_delete.png' style='vertical-align: middle;cursor:pointer;'width='20' height='20' name='" + this.id + "' onclick='DelSelectProduct(" + '"' + this.id + '"' + ")' /><br>";
                    _text += this.id + ",";
                    $("input[id*=txtval]").val(_text);
                    $("div[id*=tdBlackList]").html(_html);
                }
            });

            var availableValueplane = [];
            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=getlistproduct&id=",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].sProduct,
                            show: objjson[index].sProduct + " " + objjson[index].sBarCode,
                            value: objjson[index].nProductID
                        };
                        availableValueplane[index] = newObject;
                    });
                }
            });

            $("input[id*=txtSearch]").autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                source: function (request, response) {
                    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
                    $("div[id*=TProduct_]").hide();
                    $.each(results, function (i) {
                        $("#TProduct_" + results[i].value).show();
                    });
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("input[id*=txtSearc").val(ui.item.label);
                    $("div[id*=TProduct_]").hide();
                    $("#TProduct_" + ui.item.value).show();
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });

            //var _str = $("input[id*=txtval]").val();
            //var _html = "";
            //var _text = "";
            //jQuery.each(_str.split(','), function (index, item) {
            //    if (item != "") {
            //        var _id = item;
            //        var _val = $("div[id*=" + _id + "]").html();
            //        _html += "<span id=" + _id + ">" + _val + "<img src='images/action_delete.png' style='vertical-align: middle;cursor:pointer; 'width='20' height='20' name='" + _id + "' onclick='DelSelectProduct(" + '"' + _id + '"' + ")' /></span><br>";
            //        $("div[id*=tdBlackList]").html(_html);
            //    }
            //});
        });
        function DelSelectProduct(ID) {
            var _text = $("input[id*=txtval]").val();
            var _html = "";

            _text = _text.replace(ID + ",", "");
            $("input[id*=txtval]").val(_text);
            $("div[id*=tdBlackList]").html(_html);
            var _str = $("input[id*=txtval]").val();
            jQuery.each(_str.split(','), function (index, item) {
                if (item != "") {
                    var _id = item;
                    var _val = $("div[id*=" + _id + "]").html();
                    _html += "<span id=" + _id + ">" + _val + "<img src='images/action_delete.png' style='vertical-align: middle;cursor:pointer;'width='20' height='20' name='" + _id + "' onclick='DelSelectProduct(" + '"' + _id + '"' + ")' /></span><br>";
                    $("div[id*=tdBlackList]").html(_html);
                }
            });
        }

        function checkword() {
            var citizentxt = document.getElementsByClassName("citizentxt");
            var homecodetxt = document.getElementsByClassName("homecodetxt");
            var famcitizen = document.getElementsByClassName("famcitizen");
            var fathercitizen = document.getElementsByClassName("fathercitizen");
            var mothercitizen = document.getElementsByClassName("mothercitizen");

            while (citizentxt[0].value.length > 13) {

                citizentxt[0].value = citizentxt[0].value.slice(0, -1);
            }

            while (famcitizen[0].value.length > 13) {

                famcitizen[0].value = famcitizen[0].value.slice(0, -1);
            }

            while (fathercitizen[0].value.length > 13) {

                fathercitizen[0].value = fathercitizen[0].value.slice(0, -1);
            }

            while (mothercitizen[0].value.length > 13) {

                mothercitizen[0].value = mothercitizen[0].value.slice(0, -1);
            }

            while (homecodetxt[0].value.length > 20) {

                homecodetxt[0].value = homecodetxt[0].value.slice(0, -1);
            }
        }
    </script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp() {
            popUpObj = window.showModalDialog('frmRegister.aspx?page=users&Firstname=' + document.getElementById("aspnetForm").ctl00$MainContent$txtsName.value
                + '&Lastname=' + document.getElementById("aspnetForm").ctl00$MainContent$txtsLastName.value
                + '&IDCARD=' + document.getElementById("aspnetForm").ctl00$MainContent$txtsIdentification.value, this, 'status:1; resizable:1; dialogWidth:420px; dialogHeight:520px; dialogTop=180px; dialogLeft:510px; scroll:no;status=no');
        }
    </script>
    <div id="loading" class="loading hidden"></div>
    <div class="full-card planelist-container">
        <div class="full-card box-content row--space">
            <div class="wizard" id="top">
                <div class="wizard-inner">
                    <div class="connecting-line"></div>
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active">
                            <a href="#step1" data-toggle="tab" aria-controls="step1" role="tab" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %>">
                                <span class="round-tab">
                                    <i class="hid">1</i>
                                    <i class="glyphicon glyphicon-education"></i>
                                    <i class="hid">1</i>
                                </span>
                            </a>
                        </li>
                        <li role="presentation" class="disabled">
                            <a href="#step2" data-toggle="tab" aria-controls="step2" role="tab" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103094") %>">
                                <span class="round-tab">
                                    <i class="hid">1</i>
                                    <i class="glyphicon glyphicon-book"></i>
                                    <i class="hid">1</i>
                                </span>
                            </a>
                        </li>
                        <li role="presentation" class="disabled">
                            <a href="#step3" data-toggle="tab" aria-controls="step3" role="tab" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %>">
                                <span class="round-tab">
                                    <i class="hid">1</i>
                                    <i class="glyphicon glyphicon-user"></i>
                                    <i class="hid">1</i>
                                </span>
                            </a>
                        </li>
                        <li role="presentation" class="disabled">
                            <a href="#step4" data-toggle="tab" aria-controls="step4" role="tab" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %>">
                                <span class="round-tab">
                                    <i class="hid">1</i>
                                    <i class="glyphicon glyphicon-heart"></i>
                                    <i class="hid">1</i>
                                </span>
                            </a>
                        </li>
                        <li role="presentation" class="disabled">
                            <a href="#step5" data-toggle="tab" aria-controls="complete" role="tab" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103095") %>">
                                <span class="round-tab">
                                    <i class="hid">1</i>
                                    <i class="glyphicon glyphicon-folder-open"></i>
                                    <i class="hid">1</i>
                                </span>
                            </a>
                        </li>
                    </ul>
                </div>
                <div class="tab-content">
                    <div class="tab-pane active" role="tabpanel" id="step1" style="border-top: 0; padding-top: 0; margin-top: 0;">
                        <div class="step1">
                            <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103046") %></h1>

                            <div class="contentBox">
                                <div class="column70">
                                    <%--### StudentID - <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>--%>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input" style="display: flex; justify-content: center; align-items: center;">
                                                <asp:TextBox ID="sStudentidtxt" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                <div class="tooltip">
                                                    <i class="glyphicon glyphicon-question-sign"></i>
                                                    <span class="tooltiptext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103055") %></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:DropDownList ID="optionYear" runat="server" CssClass="width100 form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <%--### StudentCategory - <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103201") %>--%>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103201") %></label>
                                            <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                                                <asp:RadioButtonList ID="rdlStudentCategory" CssClass="radioButtonList" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103049") %>&nbsp;" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103050") %>&nbsp;</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201034") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:DropDownList ID="optionCourse" runat="server" CssClass="width100 form-control">
                                                    <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103009") %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                                        <ContentTemplate>
                                            <%--### StudentType - <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103201") %>--%>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103201") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="ddlStudentType" runat="server" CssClass="width100 form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlStudentType_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="optionLevel" runat="server" CssClass="width100 form-control ddlclassroom" onchange="ddlcheck2(); __doPostBack('ctl00$MainContent$optionLevel','');" OnSelectedIndexChanged="optionLevel_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--### Plan - <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103202") %>--%>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103202") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="ddlPlan" runat="server" CssClass="width100 form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlStudentType" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="optionLevel" EventName="SelectedIndexChanged" />
                                            <asp:PostBackTrigger ControlID="Button1" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <%---hidden section---%>
                                    <div class="form-group row student hidden">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                nTSubLevel</label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="nTSubLevel" runat="server" CssClass="width100 form-control idlvtxt2">
                                                           
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student hidden">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                nTLevel</label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="nTLevel" runat="server" CssClass="width100 form-control idlvtxt">
                                                           
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student hiddentime hidden">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132766") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:DropDownList ID="optionTime" runat="server" CssClass="width100 form-control ddltime">
                                                    <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132767") %>" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132768") %>"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132769") %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student hidden1 hidden">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02951") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:DropDownList ID="optionBranch1" runat="server" CssClass="width100 form-control ddltype1">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student hidden2 hidden">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02951") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:DropDownList ID="optionBranch2" runat="server" CssClass="width100 form-control ddltype2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="width100 form-control pagetitle">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %></label>
                                            <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                                                <asp:RadioButtonList ID="RadioButtonList8" CssClass="radioButtonList" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>&nbsp;" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %>"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %>&nbsp;</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                <br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sNameTH" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                <br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sLastTH" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                <br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sNameEN" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                <br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sLastEN" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                <br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132770") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sStudentNameOther" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                <br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132770") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sStudentLastOther" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %>
                                                <br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sNick" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %>
                                                <br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sNickNameEN" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="citizenID" onchange="checkword(13);" runat="server" CssClass='form-control citizentxt' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sPhone" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="Emailtxt" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="width: 13%;">
                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="width100 form-control birthcheck">
                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="1" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="2" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="3" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="4" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="5" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="6" Value="06"></asp:ListItem>
                                                    <asp:ListItem Text="7" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="8" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="9" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                    <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                    <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                    <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="margin-left: -15px; padding-left: -15px; width: 25%;">
                                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="width100 form-control birthcheck">
                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>" Value="06"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %>" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>" Value="12"></asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input">
                                                <asp:DropDownList ID="ddlAge" runat="server" CssClass="width100 form-control" Style="margin-left: -15px; padding-left: -15px;">
                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                            <%--<div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                                <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106130") %> &nbsp;&nbsp;" GroupName="Radio6" onclick="Radio_Click6()" />
                                                <asp:RadioButton ID="RadioButton2" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %> " GroupName="Radio6" onclick="Radio_Click6()" />
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                                <asp:TextBox ID="TextBox3" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                            </div>--%>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:DropDownList ID="ddlRace" runat="server" CssClass="width100 form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                            <%--<div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                                <asp:RadioButton ID="RadioButton13" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106130") %> &nbsp;&nbsp;" GroupName="Radio7" onclick="Radio_Click7()" />
                                                <asp:RadioButton ID="RadioButton14" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %> " GroupName="Radio7" onclick="Radio_Click7()" />
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                                <asp:TextBox ID="TextBox8" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                            </div>--%>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:DropDownList ID="ddlNation" runat="server" CssClass="width100 form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <%--<asp:RadioButton ID="RadioButton15" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01228") %> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" GroupName="Radio8" onclick="Radio_Click8()" />
                                                <asp:RadioButton ID="RadioButton17" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00263") %> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" GroupName="Radio8" onclick="Radio_Click8()" />
                                                <asp:RadioButton ID="RadioButton18" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02325") %>" GroupName="Radio8" onclick="Radio_Click8()" />--%>
                                                <asp:DropDownList ID="ddlReligion" runat="server" CssClass="width100 form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                            </label>
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                <asp:RadioButton ID="RadioButton16" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %> " GroupName="Radio8" onclick="Radio_Click8()" />
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input" style="padding-left: 0px;">
                                                <asp:TextBox ID="TextBox10" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103059") %></label>
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="width: 50%;">
                                                <asp:DropDownList ID="nSonTotal" runat="server" CssClass="width100 form-control pageson2">
                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103060") %>" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101113") %></label>
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="width: 50%;">
                                                <asp:DropDownList ID="DropDownList5" runat="server" CssClass="width100 form-control pageson">
                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103061") %>" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext" style="text-align: center">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103062") %><br />
                                                <sub style="font-size: 100%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103063") %></sub></label>
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="width: 50%;">
                                                <asp:DropDownList ID="nRelativeStudyHere" runat="server" CssClass="width100 form-control pageson3">
                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103064") %>" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %>" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="3 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="5 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="6 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="7 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="8 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="9 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="10 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="10"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>
                                    <h3 style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103066") %></h3>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103067") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sStudentHomeRegisterCode" onchange="checkword()" runat="server" CssClass='form-control homecodetxt' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="stdHomenum" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="stdSoy" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="stdMuu" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="stdRoad" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                        <ContentTemplate>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="ddlprovince" runat="server" CssClass="width100 form-control pageprovince" AutoPostBack="True" OnSelectedIndexChanged="ddlprovince_SelectedIndexChanged">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>" Value="-1"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="txtAumper" runat="server" CssClass="form-control pageaumpher" AutoPostBack="True" OnSelectedIndexChanged="txtAumper_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="txtTumbon" runat="server" CssClass="form-control pagetumbon"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:TextBox ID="txtPost" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlprovince" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtAumper" EventName="SelectedIndexChanged" />
                                            <asp:PostBackTrigger ControlID="Button1" />
                                        </Triggers>
                                    </asp:UpdatePanel>



                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sStudentHousePhone" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103069") %><br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103070") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:DropDownList ID="stayWithTitle" runat="server" CssClass="width100 form-control titlecheck">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103069") %><br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103071") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="stayWithName" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103069") %><br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103072") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="stayWithLast" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103069") %><br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103073") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="stayWithEmail" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                <sub style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103074") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="stayWithEmergencyCall" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext" style="">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101158") %></label>
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="width: 50%;">
                                                <asp:DropDownList ID="HomeType" runat="server" CssClass="width100 form-control">
                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103075") %>" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101159") %>" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101160") %>" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101161") %>" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101162") %>" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101163") %>" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104034") %><br>
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103071") %></sub></label></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:TextBox ID="friendName" runat="server" class="form-control inputname" Width="100%"></asp:TextBox>

                                                    </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104034") %><br>
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103072") %></sub></label></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:TextBox ID="friendLast" runat="server" class="form-control inputname" Width="100%"></asp:TextBox>

                                                    </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104034") %><br>
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103076") %></sub></label></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="friendSublevel" runat="server" CssClass="width100 form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104034") %><br>
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103077") %></sub></label></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:TextBox ID="friendPhone" runat="server" class="form-control inputname" Width="100%"></asp:TextBox>

                                                    </div>
                                        </div>
                                    </div>

                                    <h3 style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103079") %></h3>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="houseRegistrationNumber" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="houseRegistrationMuu" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="houseRegistrationSoy" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="houseRegistrationRoad" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                        <ContentTemplate>

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="houseRegistrationProvince" runat="server" CssClass="width100 form-control pageprovince" AutoPostBack="True" OnSelectedIndexChanged="houseRegistrationProvince_SelectedIndexChanged">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>" Value="-1"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="houseRegistrationAumpher" runat="server" CssClass="form-control pageaumpher" AutoPostBack="True" OnSelectedIndexChanged="houseRegistrationAumpher_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="houseRegistrationTumbon" runat="server" CssClass="form-control pagetumbon"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:TextBox ID="txtPost2" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="houseRegistrationProvince" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="houseRegistrationAumpher" EventName="SelectedIndexChanged" />
                                            <asp:PostBackTrigger ControlID="Button1" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="houseRegistrationPhone" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103080") %><br>
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103081") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="bornFrom" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                        <ContentTemplate>

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103080") %><br>
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></sub></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="bornFromProvince" runat="server" CssClass="width100 form-control pageprovince" AutoPostBack="True" OnSelectedIndexChanged="bornFromProvince_SelectedIndexChanged">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>" Value="-1"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103080") %><br>
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></sub></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="bornFromAumpher" runat="server" CssClass="form-control pageaumpher" AutoPostBack="True" OnSelectedIndexChanged="bornFromAumpher_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103080") %><br>
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></sub></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="bornFromTumbon" runat="server" CssClass="form-control pagetumbon"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="houseRegistrationProvince" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="houseRegistrationAumpher" EventName="SelectedIndexChanged" />
                                            <asp:PostBackTrigger ControlID="Button1" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                    <!--70-->
                                </div>


                                <div class="column30">
                                    <div class="form-group row student ">
                                        <div class="col-md-6 col-sm-12">
                                            <label class="col-lg-12 col-md-12 col-sm-12 col-xs-12 control-label">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103047") %></label>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 hidden">
                                        <asp:Image ID="imgLogo" runat="server" />
                                        <asp:FileUpload ID="fulLogo" runat="server" accept="image/*" />
                                    </div>
                                    <div class="form-group row student">
                                    </div>
                                    <div class="form-group row student hid">
                                    </div>
                                    <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                        <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 hid">
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <img id="blah" src="http://i.imgur.com/GDWqtzI.png"
                                                    width="200" height="190">
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="hid">
                                        <p>hidden</p>
                                    </div>
                                    <div class="col-md-8 col-sm-8 col-xs-8">
                                        <div class="lefttext">
                                            <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103056") %></h3>
                                        </div>
                                        <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA32MXWA3eBA7KeowjyGrgFFpHVyqXV0hw&libraries=places&callback=initMap"
                                            type="text/javascript"></script>
                                        <script>

                                            function markerDrag(event) {
                                                document.getElementById('lat').value = event.latLng.lat();
                                                document.getElementById('lng').value = event.latLng.lng();
                                            }
                                            function initMap() {
                                                var latlng = new google.maps.LatLng(13.76, 100.49);
                                                map = new google.maps.Map(document.getElementById('map'), {
                                                    center: latlng,
                                                    zoom: 16,
                                                    mapTypeControl: false,
                                                    streetViewControl: false
                                                });
                                                var input = document.getElementById('searchInput');
                                                map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

                                                var autocomplete = new google.maps.places.Autocomplete(input);
                                                autocomplete.bindTo('bounds', map);

                                                var infowindow = new google.maps.InfoWindow();
                                                var image = 'https://www.google.com/mapfiles/marker.png';

                                                var marker = new google.maps.Marker({
                                                    map: map,
                                                    draggable: true,
                                                    position: latlng,
                                                    icon: image
                                                });
                                                google.maps.event.addListener(marker, 'dragend', function (event) {
                                                    document.getElementById('lat').value = this.position.lat();
                                                    document.getElementById('lon').value = this.position.lng();
                                                    document.getElementById("<%=SendA.ClientID%>").value = this.position.lat();
                                                    document.getElementById("<%=SendB.ClientID%>").value = this.position.lng();
                                                    document.getElementById("<%=lat2.ClientID%>").value = this.position.lat();
                                                    document.getElementById("<%=lon2.ClientID%>").value = this.position.lng();

                                                });

                                                autocomplete.addListener('place_changed', function () {
                                                    infowindow.close();
                                                    marker.setVisible(false);
                                                    var place = autocomplete.getPlace();
                                                    if (!place.geometry) {
                                                        window.alert("Autocomplete's returned place contains no geometry");
                                                        return;
                                                    }

                                                    // If the place has a geometry, then present it on a map.
                                                    if (place.geometry.viewport) {
                                                        map.fitBounds(place.geometry.viewport);
                                                    } else {
                                                        map.setCenter(place.geometry.location);
                                                        map.setZoom(17);
                                                    }

                                                    marker.setPosition(place.geometry.location);
                                                    marker.setVisible(true);

                                                    var address = '';
                                                    if (place.address_components) {
                                                        address = [
                                                            (place.address_components[0] && place.address_components[0].short_name || ''),
                                                            (place.address_components[1] && place.address_components[1].short_name || ''),
                                                            (place.address_components[2] && place.address_components[2].short_name || '')
                                                        ].join(' ');
                                                    }

                                                    //infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);
                                                    //infowindow.open(map, marker);

                                                    //Location details

                                                    document.getElementById('lat').innerHTML = place.geometry.location.lat();
                                                    document.getElementById('lon').innerHTML = place.geometry.location.lng();
                                                    document.getElementById("<%=SendA.ClientID%>").value = place.geometry.location.lat();
                                                    document.getElementById("<%=SendB.ClientID%>").value = place.geometry.location.lng();
                                                    document.getElementById("<%=lat2.ClientID%>").value = place.geometry.location.lat();
                                                    document.getElementById("<%=lon2.ClientID%>").value = place.geometry.location.lng();

                                                });
                                            }
                                        </script>
                                        <input id="searchInput" class="controls" style="font-size: 140%;" type="text" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801049") %>">

                                        <div id="map" class="col-xs-12" style="width: 100%; height: 300px; margin-left: -18%; padding-left: -18%;"></div>



                                        <ul id="geoData">
                                            <div id="block_container">

                                                <div id="lat" class="hidden"></div>
                                                <div id="lon" class="hidden"></div>

                                            </div>



                                        </ul>
                                    </div>
                                    <div class="col-md-8 col-sm-8 col-xs-8" style="margin-left: -10%; padding-left: -10%; padding-top: 10px;">
                                        <div class="col-md-6 col-sm-6 col-xs-6">
                                            <asp:TextBox runat="server" ID="lat2" Value="" CssClass="width50right form-control" Enabled="False" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103057") %>" />
                                        </div>
                                        <div class="col-md-6 col-sm-6 col-xs-6">
                                            <asp:TextBox runat="server" ID="lon2" Value="" CssClass="width50 form-control" Enabled="False" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103058") %>" />
                                        </div>





                                    </div>
                                    <asp:HiddenField runat="server" ID="SendA" Value="" />

                                    <asp:HiddenField runat="server" ID="SendB" Value="" />
                                    <div class="form-group row student hid" style="margin-left: -15%; padding-left: -15%;">
                                        <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                hidden</label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox53" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student hid" style="margin-left: -15%; padding-left: -15%;">
                                        <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                hidden</label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox55" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student hid" style="margin-left: -15%; padding-left: -15%;">
                                        <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                hidden</label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox57" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row student">
                            <div class="col-md-12 col-sm-12">
                                <label class="col-xs-3 control-label righttext">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103082") %></label>
                                <div class="col-xs-1 control-input" style="padding: 0px; margin-top: 6px;">
                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="pull-right" />
                                </div>
                                <div class="col-xs-2 control-input">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103083") %></label>
                                </div>
                                <div class="col-xs-1 control-input" style="padding: 0px; margin-top: 6px;">
                                    <asp:CheckBox ID="CheckBox2" runat="server" CssClass="pull-right" />
                                </div>
                                <div class="col-xs-2 control-input">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:CheckBox ID="CheckBox9" runat="server" CssClass="" /><label style="text-indent: 10px;"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103084") %></label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row student">
                            <div class="col-md-12 col-sm-12">
                                <label class="col-xs-3 control-label righttext">
                                </label>
                                <div class="col-xs-1 control-input" style="padding: 0px; margin-top: 6px;">
                                    <asp:CheckBox ID="CheckBox3" runat="server" CssClass="pull-right" />
                                </div>
                                <div class="col-xs-2 control-input" style="padding-right: 0px;">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103085") %></label>
                                </div>
                                <div class="col-xs-1 control-input" style="padding: 0px; margin-top: 6px;">
                                    <asp:CheckBox ID="CheckBox4" runat="server" CssClass="pull-right" />
                                </div>
                                <div class="col-xs-2 control-input">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103086") %></label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:CheckBox ID="CheckBox10" runat="server" CssClass="" /><label style="text-indent: 10px;"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103087") %></label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row student">
                            <div class="col-md-12 col-sm-12">
                                <label class="col-xs-3 control-label righttext">
                                </label>
                                <div class="col-xs-1 control-input" style="padding: 0px; margin-top: 6px;">
                                    <asp:CheckBox ID="CheckBox5" runat="server" CssClass="pull-right" />
                                </div>
                                <div class="col-xs-2 control-input">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103088") %></label>
                                </div>
                                <div class="col-xs-1 control-input" style="padding: 0px; margin-top: 6px;">
                                    <asp:CheckBox ID="CheckBox6" runat="server" CssClass="pull-right" />
                                </div>
                                <div class="col-xs-2 control-input">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103089") %></label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:CheckBox ID="CheckBox11" runat="server" CssClass="" /><label style="text-indent: 10px;"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103090") %></label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row student">
                            <div class="col-md-12 col-sm-12">
                                <label class="col-xs-3 control-label righttext">
                                </label>
                                <div class="col-xs-1 control-input" style="padding: 0px; margin-top: 6px;">
                                    <asp:CheckBox ID="CheckBox7" runat="server" CssClass="pull-right" />
                                </div>
                                <div class="col-xs-2 control-input">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103091") %></label>
                                </div>
                                <div class="col-xs-1 control-input" style="padding: 0px; margin-top: 6px;">
                                    <asp:CheckBox ID="CheckBox8" runat="server" CssClass="pull-right" />
                                </div>
                                <div class="col-xs-2 control-input">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103092") %></label>
                                </div>
                            </div>
                        </div>

                        <div class="row mar_ned">
                            <div class="col-md-12 col-xs-12">

                                <div class="form-group row student">
                                    <div class="col-md-12 col-sm-12">
                                        <ul class="list-inline pull-right">
                                            <li>
                                                <button type="button" class="btn btn-default prev-step hid">Previous</button></li>
                                            <li><a href="#top">
                                                <button type="button" class="btn btn-primary next-step" onclick="check1()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></button></a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" role="tabpanel" id="step2" style="border-top: 0; padding-top: 0; margin-top: 0;">
                        <div class="step2">
                            <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103094") %></h1>
                        </div>

                        <div class="">
                            <div class="">
                                <div class="col-xs-12">
                                    <input type="checkbox" class="check12" id="Checkbox12" onclick="noSchool()" runat="server">
                                    <label class="custom-control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132764") %></label>
                                </div>
                                <div class="form-group row student">
                                    <div class="col-md-12 col-sm-12">
                                        <label class="col-xs-4 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103096") %></label>
                                        <div class="col-xs-5 control-input">
                                            <asp:TextBox ID="oldSchoolName" runat="server" CssClass="form-control oName" class="input--mid"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                                <div class="form-group row student">
                                    <div class="col-md-12 col-sm-12">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 control-input">
                                                            <asp:DropDownList ID="oldSchoolProvince" AutoPostBack="true" OnSelectedIndexChanged="oldschoolProvince_SelectedIndexChanged" runat="server" CssClass="width100 form-control oProvince pageprovince">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 control-input">
                                                            <asp:DropDownList ID="oldSchoolAumpher" runat="server" CssClass="form-control oAumpher pageaumpher" OnSelectedIndexChanged="oldschoolaumpher_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 control-input">
                                                            <asp:DropDownList ID="oldSchoolTumbon" runat="server" CssClass="form-control oTumbon pagetumbon"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>


                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="oldSchoolProvince" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="oldSchoolAumpher" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group row student">
                                    <div class="col-md-12 col-sm-12">
                                        <label class="col-xs-4 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132771") %></label>
                                        <div class="col-xs-5 control-input">
                                            <asp:TextBox ID="oldSchoolGPA" runat="server" CssClass="form-control oGPA" class="input--mid"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="form-group row student">
                                    <div class="col-md-12 col-sm-12">
                                        <label class="col-xs-4 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %></label>
                                        <div class="col-xs-5 control-input">
                                            <asp:DropDownList ID="oldSchoolGraduated" runat="server" CssClass="form-control oGradu pagegradu">
                                                <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131188") %>" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="อนุบาลศึกษา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01072") %> 1" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="อนุบาลศึกษา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01072") %> 2" Value="13"></asp:ListItem>
                                                <asp:ListItem Text="อนุบาลศึกษา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01072") %> 3" Value="14"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131192") %>" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131193") %>" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131194") %>" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131195") %>" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131196") %>" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206406") %>" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132154") %>" Value="18"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132155") %>" Value="19"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206407") %>" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132156") %>" Value="21"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132157") %>" Value="22"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206408") %>" Value="23"></asp:ListItem>
                                                <%--<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131198") %>" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131199") %>" Value="8"></asp:ListItem>--%>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131200") %>" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131201") %>" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131202") %>" Value="16"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131203") %>" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131204") %>" Value="17"></asp:ListItem>

                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row student">
                                    <div class="col-md-12 col-sm-12">
                                        <label class="col-xs-4 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101178") %></label>
                                        <div class="col-xs-5 control-input">
                                            <asp:TextBox ID="moveOutReason" runat="server" CssClass="form-control oMoveout" class="input--mid"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                                <!--70-->
                            </div>


                        </div>
                        <div class="col-xs-12 hid">
                            <p>hidden</p>
                        </div>
                        <ul class="list-inline pull-right">
                            <li><a href="#top">
                                <button type="button" class="btn btn-default prev-step"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></button></a></li>
                            <li><a href="#top">
                                <button type="button" class="btn btn-primary next-step" onclick="check2()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></button></a></li>
                        </ul>
                    </div>
                    <div class="tab-pane" role="tabpanel" id="step3" style="border-top: 0; padding-top: 0; margin-top: 0;">
                        <div class="step3">
                            <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102033") %></h1>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="col-xs-12 center">
                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %></label>
                                    </div>

                                    <div class="col-xs-12">
                                        <div class="col-xs-6">

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="famTitle" runat="server" CssClass="width100 form-control pagetitle">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                        <br />
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="famName" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                        <br />
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="famLast" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                        <br />
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sFamilyNameEN" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                        <br />
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sFamilyLastEN" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 15px;">
                                                        <asp:DropDownList ID="dFamilyBirthDayDD" runat="server" CssClass="width100 form-control">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="1" Value="01"></asp:ListItem>
                                                            <asp:ListItem Text="2" Value="02"></asp:ListItem>
                                                            <asp:ListItem Text="3" Value="03"></asp:ListItem>
                                                            <asp:ListItem Text="4" Value="04"></asp:ListItem>
                                                            <asp:ListItem Text="5" Value="05"></asp:ListItem>
                                                            <asp:ListItem Text="6" Value="06"></asp:ListItem>
                                                            <asp:ListItem Text="7" Value="07"></asp:ListItem>
                                                            <asp:ListItem Text="8" Value="08"></asp:ListItem>
                                                            <asp:ListItem Text="9" Value="09"></asp:ListItem>
                                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                            <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                            <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                            <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                            <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                            <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                            <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                            <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                            <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input pad0" style="margin-left: 3px;">
                                                        <asp:DropDownList ID="dFamilyBirthDayMM" runat="server" CssClass="width100 form-control">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>" Value="01"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>" Value="02"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>" Value="03"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>" Value="04"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>" Value="05"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>" Value="06"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %>" Value="07"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>" Value="08"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>" Value="09"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>" Value="11"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>" Value="12"></asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 3px;">
                                                        <asp:DropDownList ID="dFamilyBirthDayYY" runat="server" CssClass="width100 form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="famIdCard" runat="server" onchange="checkword()" onkeypress="return isNumber(event)" CssClass='form-control famcitizen' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                                    <%--<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input" style="padding-right: 0px">
                                                        <asp:RadioButton ID="GuardianChurChad1" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106130") %> &nbsp;&nbsp;" GroupName="Radio15" onclick="Radio_Click15()" />
                                                        <asp:RadioButton ID="GuardianChurChad2" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %> " GroupName="Radio15" onclick="Radio_Click15()" />
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input">
                                                        <asp:TextBox ID="GuardianChurChadtxt" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                                    </div>--%>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="ddlFamRace" runat="server" CssClass="width100 form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                                    <%--<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input" style="padding-right: 0px">
                                                        <asp:RadioButton ID="GuardianSunChad1" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106130") %> &nbsp;&nbsp;" GroupName="Radio16" onclick="Radio_Click16()" />
                                                        <asp:RadioButton ID="GuardianSunChad2" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %> " GroupName="Radio16" onclick="Radio_Click16()" />
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input">
                                                        <asp:TextBox ID="GuardianSunChadtxt" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                                    </div>--%>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="ddlFamNation" runat="server" CssClass="width100 form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <%--<asp:RadioButton ID="GuardianReligion1" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01228") %> &nbsp;&nbsp;" GroupName="Radio17" onclick="Radio_Click17()" />
                                                        <asp:RadioButton ID="GuardianReligion2" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00263") %> &nbsp;&nbsp;" GroupName="Radio17" onclick="Radio_Click17()" />
                                                        <asp:RadioButton ID="GuardianReligion3" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02325") %>" GroupName="Radio17" onclick="Radio_Click17()" />--%>
                                                        <asp:DropDownList ID="ddlFamReligion" runat="server" CssClass="width100 form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    </label>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                        <%--<asp:RadioButton ID="GuardianReligion4" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %> " GroupName="Radio17" onclick="Radio_Click17()" />--%>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input" style="padding-left: 0px;">
                                                        <%--<asp:TextBox ID="GuardianReligiontxt" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>--%>
                                                    </div>

                                                </div>
                                            </div>




                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101192") %></label>
                                                    <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                                                        <asp:RadioButtonList ID="RadioButtonList1" CssClass="radioButtonList" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %>"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %>&nbsp;</asp:ListItem>
                                                            <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %>"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %>&nbsp;</asp:ListItem>
                                                            <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %>"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %>&nbsp;</asp:ListItem>
                                                            <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>&nbsp;</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 pad0 control-label righttext" style="text-align: center">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103099") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input" style="">
                                                        <asp:DropDownList ID="nFamilyRequestStudyMoney" runat="server" CssClass="width100 form-control">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103100") %>" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101199") %>" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101200") %>" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-xs-4 control-label pad0 righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %></label>
                                                    <div class="col-xs-8 control-input">
                                                        <asp:DropDownList ID="sFamilyGraduated" runat="server" CssClass="form-control oGradu pagegradu">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00745") %>" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %>" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01315") %>" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %>" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103101") %>" Value="9"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103102") %>" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103103") %>" Value="8"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103104") %>" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %>" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102061") %>" Value="6"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-xs-4 control-label pad0 righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101201") %></label>
                                                    <div class="col-xs-8 control-input">
                                                        <asp:DropDownList ID="familyStatus" runat="server" CssClass="form-control oGradu pagegradu">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101202") %>" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101203") %>" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101204") %>" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101205") %>" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101206") %>" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103105") %>" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101207") %>" Value="6"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101208") %>" Value="7"></asp:ListItem>

                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>



                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="famHomenum" runat="server" CssClass='form-control famHome' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="famSoy" runat="server" CssClass='form-control famSoy' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="famMuu" runat="server" CssClass='form-control famMuu' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="famRoad" runat="server" CssClass='form-control famRoad' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="famProvince" AutoPostBack="true" OnSelectedIndexChanged="famProvince_SelectedIndexChanged" runat="server" CssClass="width100 form-control famProvince pageprovince">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="famaumpher" runat="server" CssClass='form-control famAumpher' OnSelectedIndexChanged="famaumpher_SelectedIndexChanged" class="input--mid" AutoPostBack="True"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="famTumbon" runat="server" CssClass='form-control famTumbon' class="input--mid"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="famPost" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sFamilyJob" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sFamilyWorkPlace" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                       <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104044") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:TextBox ID="nFamilyIncome" runat="server" onkeypress="return isNumber(event)" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                                    </div>
                                                    <label class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></label>
                                                </div>
                                            </div>
                                            <%--### ParentYearIncome - <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132773") %>--%>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104045") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="ddlParentYearIncome" runat="server" CssClass="form-control">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103107") %>" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103106") %>" Value="0 - 150,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103108") %>" Value="150,001 - 300,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103109") %>" Value="300,001 - 500,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103110") %>" Value="500,001 - 750,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103111") %>" Value="750,001 - 1,000,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103112") %>" Value="1,000,001 - 2,000,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103113") %>" Value="2,000,001 - 5,000,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103114") %>" Value="5,000,001+"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                        <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %>)</sub></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="famPhone1" runat="server" onkeypress="return isNumber(event)" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                        <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701042") %>)</sub></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="famPhone2" runat="server" CssClass='form-control' onkeypress="return isNumber(event)" class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                        <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131217") %>)</sub></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="famPhone3" runat="server" CssClass='form-control' onkeypress="return isNumber(event)" class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--### ParentEmail - <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132782") %>--%>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="txtParentEmail" runat="server" CssClass='form-control' class="input--mid" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-xs-12">
                                        <hr />
                                    </div>
                                    <div class="col-xs-12 center">
                                        <div class="col-xs-4">
                                        </div>
                                        <div class="col-xs-3">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101180") %></label>
                                        </div>
                                        <div class="col-xs-5">
                                            <div class="custom-control custom-checkbox">
                                                <input type="checkbox" class=" fatherClick" id="customCheck1" onclick="copyAddress()" runat="server">
                                                <label class="custom-control-label" for="customCheck1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00604") %></label>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-xs-12">
                                        <div class="col-xs-6">
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="fatherTitle" runat="server" CssClass="width100 form-control pagetitle">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                        <br />
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub>
                                                    </label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="fatherName" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                        <br />
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub>
                                                    </label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="fatherLast" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                        <br />
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub>
                                                    </label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sFatherNameEN" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                        <br />
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub>
                                                    </label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sFatherLastEN" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 15px;">
                                                        <asp:DropDownList ID="dFatherBirthDayDD" runat="server" CssClass="width100 form-control">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="1" Value="01"></asp:ListItem>
                                                            <asp:ListItem Text="2" Value="02"></asp:ListItem>
                                                            <asp:ListItem Text="3" Value="03"></asp:ListItem>
                                                            <asp:ListItem Text="4" Value="04"></asp:ListItem>
                                                            <asp:ListItem Text="5" Value="05"></asp:ListItem>
                                                            <asp:ListItem Text="6" Value="06"></asp:ListItem>
                                                            <asp:ListItem Text="7" Value="07"></asp:ListItem>
                                                            <asp:ListItem Text="8" Value="08"></asp:ListItem>
                                                            <asp:ListItem Text="9" Value="09"></asp:ListItem>
                                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                            <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                            <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                            <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                            <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                            <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                            <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                            <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                            <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input pad0" style="margin-left: 3px;">
                                                        <asp:DropDownList ID="dFatherBirthDayMM" runat="server" CssClass="width100 form-control">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>" Value="01"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>" Value="02"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>" Value="03"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>" Value="04"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>" Value="05"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>" Value="06"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %>" Value="07"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>" Value="08"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>" Value="09"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>" Value="11"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>" Value="12"></asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 3px;">
                                                        <asp:DropDownList ID="dFatherBirthDayYY" runat="server" CssClass="width100 form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="fatherIdCard" onkeypress="return isNumber(event)" runat="server" onchange="checkword()" CssClass='fathercitizen form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                                    <%--<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input" style="padding-right: 0px">
                                                        <asp:RadioButton ID="fatherChurChadddl1" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106130") %> &nbsp;&nbsp;" GroupName="Radio10" onclick="Radio_Click10()" />
                                                        <asp:RadioButton ID="fatherChurChadddl2" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %> " GroupName="Radio10" onclick="Radio_Click10()" />
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input">
                                                        <asp:TextBox ID="fatherChurChad" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                                    </div>--%>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="ddlFatherRace" runat="server" CssClass="width100 form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                                    <%--<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input" style="padding-right: 0px">
                                                        <asp:RadioButton ID="fatherSunChadddl1" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106130") %> &nbsp;&nbsp;" GroupName="Radio9" onclick="Radio_Click9()" />
                                                        <asp:RadioButton ID="fatherSunChadddl2" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %> " GroupName="Radio9" onclick="Radio_Click9()" />
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input">
                                                        <asp:TextBox ID="fatherSunChad" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                                    </div>--%>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="ddlFatherNation" runat="server" CssClass="width100 form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <%--<asp:RadioButton ID="fatherReligion1" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01228") %> &nbsp;&nbsp;" GroupName="Radio11" onclick="Radio_Click11()" />
                                                        <asp:RadioButton ID="fatherReligion2" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00263") %> &nbsp;&nbsp;" GroupName="Radio11" onclick="Radio_Click11()" />
                                                        <asp:RadioButton ID="fatherReligion3" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02325") %>" GroupName="Radio11" onclick="Radio_Click11()" />--%>
                                                        <asp:DropDownList ID="ddlFatherReligion" runat="server" CssClass="width100 form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    </label>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                        <%--<asp:RadioButton ID="fatherReligion4" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %> " GroupName="Radio11" onclick="Radio_Click11()" />--%>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input" style="padding-left: 0px;">
                                                        <%--<asp:TextBox ID="fatherReligiontxt" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>--%>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %></label>
                                                    <div class="col-xs-8 control-input">
                                                        <asp:DropDownList ID="sFatherGraduated" runat="server" CssClass="form-control oGradu pagegradu">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00745") %>" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %>" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01315") %>" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %>" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103101") %>" Value="9"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103102") %>" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103103") %>" Value="8"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103104") %>" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %>" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102061") %>" Value="6"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="fatherHome" runat="server" CssClass='form-control fatherHome' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="fatherSoy" runat="server" CssClass='form-control fatherSoy' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="fatherMuu" runat="server" CssClass='form-control fatherMuu' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="fatherRoad" runat="server" CssClass='form-control fatherRoad' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student fatherProvinceStatus2 hidden">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass='form-control fatherProvince2 disable' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student fatherAumpherStatus2 hidden">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="TextBox4" runat="server" CssClass='form-control fatherAumpher2 disable' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student fatherTumbonStatus2 hidden">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="TextBox5" runat="server" CssClass='form-control fatherTumbon2 disable' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student fatherProvinceStatus">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="fatherProvince" AutoPostBack="true" onchange="copyAddress(1)" OnSelectedIndexChanged="fatherProvince_SelectedIndexChanged" runat="server" CssClass="width100 form-control fatherProvince pageprovince">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student fatherAumpherStatus">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="fatherAumpher" runat="server" onchange="copyAddress(1)" CssClass='form-control fatherAumpher' OnSelectedIndexChanged="fatheraumpher_SelectedIndexChanged" class="input--mid" AutoPostBack="True"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="form-group row student fatherTumbonStatus">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="fatherTumbon" runat="server" onchange="copyAddress(1)" CssClass='form-control fatherTumbon' class="input--mid"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="fatherPost" runat="server" CssClass='form-control fatherPost' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sFatherJob" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sFatherWorkPlace" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                       <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104044") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:TextBox ID="nFatherIncome" onkeypress="return isNumber(event)" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                                    </div>
                                                    <label class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></label>
                                                </div>
                                            </div>
                                            <%--### FatherYearIncome - <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132774") %>--%>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104045") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="ddlFatherYearIncome" runat="server" CssClass="form-control">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103107") %>" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103106") %>" Value="0 - 150,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103108") %>" Value="150,001 - 300,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103109") %>" Value="300,001 - 500,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103110") %>" Value="500,001 - 750,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103111") %>" Value="750,001 - 1,000,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103112") %>" Value="1,000,001 - 2,000,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103113") %>" Value="2,000,001 - 5,000,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103114") %>" Value="5,000,001+"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                        <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %>)</sub></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sFatherPhone" onkeypress="return isNumber(event)" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                        <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701042") %>)</sub></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sFatherPhone2" onkeypress="return isNumber(event)" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                        <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131217") %>)</sub></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sFatherPhone3" onkeypress="return isNumber(event)" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--### FatherEmail - อีเมล์ของบิดา--%>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="txtFatherEmail" runat="server" CssClass='form-control' class="input--mid" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-xs-12">
                                        <hr />
                                    </div>
                                    <div class="col-xs-12 center">
                                        <div class="col-xs-4">
                                        </div>
                                        <div class="col-xs-3">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00194") %></label>
                                        </div>
                                        <div class="col-xs-5">
                                            <div class="custom-control custom-checkbox ">
                                                <input type="checkbox" class="custom-control-input motherClick" id="customCheck2" onclick="copyAddress()" runat="server">
                                                <label class="custom-control-label" for="customCheck2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00604") %></label>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-xs-12">
                                        <div class="col-xs-6">
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="motherTitle" runat="server" CssClass="width100 form-control pagetitle">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                        <br />
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub>
                                                    </label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="motherName" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                        <br />
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub>
                                                    </label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="motherLast" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                        <br />
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub>
                                                    </label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sMotherNameEN" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                        <br />
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub>
                                                    </label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sMotherLastEN" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 15px;">
                                                        <asp:DropDownList ID="dMotherBirthDayDD" runat="server" CssClass="width100 form-control">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="1" Value="01"></asp:ListItem>
                                                            <asp:ListItem Text="2" Value="02"></asp:ListItem>
                                                            <asp:ListItem Text="3" Value="03"></asp:ListItem>
                                                            <asp:ListItem Text="4" Value="04"></asp:ListItem>
                                                            <asp:ListItem Text="5" Value="05"></asp:ListItem>
                                                            <asp:ListItem Text="6" Value="06"></asp:ListItem>
                                                            <asp:ListItem Text="7" Value="07"></asp:ListItem>
                                                            <asp:ListItem Text="8" Value="08"></asp:ListItem>
                                                            <asp:ListItem Text="9" Value="09"></asp:ListItem>
                                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                            <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                            <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                            <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                            <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                            <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                            <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                            <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                            <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input pad0" style="margin-left: 3px;">
                                                        <asp:DropDownList ID="dMotherBirthDayMM" runat="server" CssClass="width100 form-control">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>" Value="01"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>" Value="02"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>" Value="03"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>" Value="04"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>" Value="05"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>" Value="06"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %>" Value="07"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>" Value="08"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>" Value="09"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>" Value="11"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>" Value="12"></asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 3px;">
                                                        <asp:DropDownList ID="dMotherBirthDayYY" runat="server" CssClass="width100 form-control" Style="">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="motherIdCard" onkeypress="return isNumber(event)" runat="server" onchange="checkword()" CssClass='mothercitizen form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                                    <%--<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input" style="padding-right: 0px">
                                                        <asp:RadioButton ID="motherChurChad1" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106130") %> &nbsp;&nbsp;" GroupName="Radio12" onclick="Radio_Click12()" />
                                                        <asp:RadioButton ID="motherChurChad2" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %> " GroupName="Radio12" onclick="Radio_Click12()" />
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input">
                                                        <asp:TextBox ID="motherChurChadtxt" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                                    </div>--%>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="ddlMotherRace" runat="server" CssClass="width100 form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                                    <%--<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input" style="padding-right: 0px;">
                                                        <asp:RadioButton ID="motherSunChad1" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106130") %> &nbsp;&nbsp;" GroupName="Radio13" onclick="Radio_Click13()" />
                                                        <asp:RadioButton ID="motherSunChad2" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %> " GroupName="Radio13" onclick="Radio_Click13()" />
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input">
                                                        <asp:TextBox ID="motherSunChadtxt" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                                    </div>--%>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="ddlMotherNation" runat="server" CssClass="width100 form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <%--<asp:RadioButton ID="motherReligion1" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01228") %> &nbsp;&nbsp" GroupName="Radio14" onclick="Radio_Click14()" />
                                                        <asp:RadioButton ID="motherReligion2" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00263") %> &nbsp;&nbsp;" GroupName="Radio14" onclick="Radio_Click14()" />
                                                        <asp:RadioButton ID="motherReligion3" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02325") %>" GroupName="Radio14" onclick="Radio_Click14()" />--%>
                                                        <asp:DropDownList ID="ddlMotherReligion" runat="server" CssClass="width100 form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    </label>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                        <%--<asp:RadioButton ID="motherReligion4" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %> " GroupName="Radio14" onclick="Radio_Click14()" />--%>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input" style="padding-left: 0px;">
                                                        <%--<asp:TextBox ID="motherReligiontxt" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>--%>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %></label>
                                                    <div class="col-xs-8 control-input">
                                                        <asp:DropDownList ID="sMotherGraduated" runat="server" CssClass="form-control oGradu pagegradu">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00745") %>" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %>" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01315") %>" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %>" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103101") %>" Value="9"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103102") %>" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103103") %>" Value="8"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103104") %>" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %>" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102061") %>" Value="6"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="motherHome" runat="server" CssClass='form-control motherHome' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="motherSoy" runat="server" CssClass='form-control motherSoy' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="motherMuu" runat="server" CssClass='form-control motherMuu' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="motherRoad" runat="server" CssClass='form-control motherRoad' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row student motherProvinceStatus2 hidden">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="TextBox11" runat="server" CssClass='form-control motherProvince2 disable' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student motherAumpherStatus2 hidden">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="TextBox14" runat="server" CssClass='form-control motherAumpher2 disable' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student motherTumbonStatus2 hidden">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="TextBox18" runat="server" CssClass='form-control motherTumbon2 disable' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row student motherProvinceStatus">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="motherProvince" AutoPostBack="true" onchange="copyAddress(1)" OnSelectedIndexChanged="motherProvince_SelectedIndexChanged" runat="server" CssClass="width100 form-control motherProvince pageprovince">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student motherAumpherStatus">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="motherAumpher" runat="server" onchange="copyAddress(1)" CssClass='form-control motherAumpher' OnSelectedIndexChanged="motheraumpher_SelectedIndexChanged" class="input--mid" AutoPostBack="True"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="form-group row student motherTumbonStatus">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="motherTumbon" runat="server" onchange="copyAddress(1)" CssClass='form-control motherTumbon' class="input--mid"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="motherPost" runat="server" CssClass='form-control motherPost' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sMotherJob" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sMotherWorkPlace" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                       <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104044") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:TextBox ID="nMotherIncome" onkeypress="return isNumber(event)" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                                    </div>
                                                    <label class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></label>
                                                </div>
                                            </div>
                                            <%--### MotherYearIncome - <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132775") %>--%>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104045") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="ddlMotherYearIncome" runat="server" CssClass="form-control">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103107") %>" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103106") %>" Value="0 - 150,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103108") %>" Value="150,001 - 300,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103109") %>" Value="300,001 - 500,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103110") %>" Value="500,001 - 750,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103111") %>" Value="750,001 - 1,000,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103112") %>" Value="1,000,001 - 2,000,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103113") %>" Value="2,000,001 - 5,000,000"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103114") %>" Value="5,000,001+"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                        <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %>)</sub></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sMotherPhone" onkeypress="return isNumber(event)" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                        <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701042") %>)</sub></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sMotherPhone2" onkeypress="return isNumber(event)" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                        <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131217") %>)</sub></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="sMotherPhone3" onkeypress="return isNumber(event)" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--### MotherEmail - <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132783") %>--%>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:TextBox ID="txtMotherEmail" runat="server" CssClass='form-control' class="input--mid" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="famProvince" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="famaumpher" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="motherProvince" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="motherAumpher" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="fatherProvince" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="fatherAumpher" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row mar_ned">
                            <div class="col-md-12 col-xs-12">

                                <div class="form-group row student">
                                    <div class="col-md-12 col-sm-12">
                                        <ul class="list-inline pull-right">
                                            <li><a href="#top">
                                                <button type="button" class="btn btn-default prev-step"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></button></a></li>
                                            <li><a href="#top">
                                                <button type="button" class="btn btn-primary next-step" onclick="check3()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></button></a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" role="tabpanel" id="step4" style="border-top: 0; padding-top: 0; margin-top: 0;">
                        <div class="step4">
                            <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %></h1>

                            <div class="contentBox">
                                <div class="column70">
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104050") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox42" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label lefttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103115") %>
                                            </label>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104051") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox43" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label lefttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131180") %>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101217") %></label>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input">
                                                <asp:DropDownList ID="ddlBlood" runat="server" CssClass="width100 form-control">
                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101218") %>" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103117") %>" Value="O"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103118") %>" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103119") %>" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103120") %>" Value="AB"></asp:ListItem>

                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101219") %></label>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101220") %></label>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                                <asp:RadioButton ID="RadioButton3" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132158") %>" GroupName="Radio" onclick="Radio_Click()" />
                                                <asp:RadioButton ID="RadioButton4" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132159") %>" GroupName="Radio" onclick="Radio_Click()" />
                                            </div>
                                            <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01102") %>
                                            </label>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input">
                                                <asp:TextBox ID="TextBox54" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131225") %></label>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                                <asp:RadioButton ID="RadioButton5" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132158") %>" GroupName="Radio2" onclick="Radio_Click2()" />
                                                <asp:RadioButton ID="RadioButton6" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132159") %>" GroupName="Radio2" onclick="Radio_Click2()" />
                                            </div>
                                            <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01102") %>
                                            </label>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input">
                                                <asp:TextBox ID="TextBox17" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101222") %></label>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                                <asp:RadioButton ID="RadioButton7" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132158") %>" GroupName="Radio3" onclick="Radio_Click3()" />
                                                <asp:RadioButton ID="RadioButton8" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132159") %>" GroupName="Radio3" onclick="Radio_Click3()" />
                                            </div>
                                            <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01102") %>
                                            </label>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input">
                                                <asp:TextBox ID="TextBox36" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101223") %></label>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                                <asp:RadioButton ID="RadioButton9" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %>" GroupName="Radio4" onclick="Radio_Click4()" />
                                                <asp:RadioButton ID="RadioButton10" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %>" GroupName="Radio4" onclick="Radio_Click4()" />
                                            </div>
                                            <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01102") %>
                                            </label>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input">
                                                <asp:TextBox ID="TextBox41" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101224") %></label>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                                <asp:RadioButton ID="RadioButton11" runat="server" Checked="True" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %>" GroupName="Radio5" onclick="Radio_Click5()" />
                                                <asp:RadioButton ID="RadioButton12" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %>" GroupName="Radio5" onclick="Radio_Click5()" />
                                            </div>
                                            <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01102") %>
                                            </label>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input">
                                                <asp:TextBox ID="TextBox44" runat="server" CssClass='form-control' class="input--mid" Enabled="false"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <!--70-->
                                </div>
                            </div>
                        </div>
                        <div class="form-group row student hid">
                            <div class="col-md-12 col-sm-12">
                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                    hide</label>
                            </div>
                        </div>
                        <div class="row mar_ned">
                            <div class="col-md-12 col-xs-12">

                                <div class="form-group row student">
                                    <div class="col-md-12 col-sm-12">
                                        <ul class="list-inline pull-right">
                                            <li><a href="#top">
                                                <button type="button" class="btn btn-default prev-step"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></button></a></li>
                                            <li><a href="#top">
                                                <button type="button" class="btn btn-primary next-step" onclick="check4()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></button></a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" role="tabpanel" id="step5" style="border-top: 0; padding-top: 0; margin-top: 0;">
                        <div class="step5">
                            <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103095") %></h1>
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
                                    <tr>
                                        <th scope="row">1</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103124") %><span class="red-star"></span>:
                                                <p class="h6" data-did="1" data-tid="1" data-vfiid="1">(One copy of the birth certificate or ID card, in case of foreign birth, a <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> translation must be attached.)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument01" data-did="1" data-tid="1" data-vfiid="1" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="1" data-tid="1" data-vfiid="1"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="1" data-tid="1" data-vfiid="1"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">2</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103125") %> <span class="red-star"></span>:
                                                <p class="h6">(A copy of the student’s house registration, father and mother)</p>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">2.1</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103126") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="2" data-tid="1" data-vfiid="3">(A copy of the student’s house registration 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument021" data-did="2" data-tid="1" data-vfiid="3" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="2" data-tid="1" data-vfiid="3"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="2" data-tid="1" data-vfiid="3"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">2.2</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103127") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="2" data-tid="2" data-vfiid="4">(A copy of the father’s house registration 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument022" data-did="2" data-tid="2" data-vfiid="4" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="2" data-tid="2" data-vfiid="4"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="2" data-tid="2" data-vfiid="4"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">2.3</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103128") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="2" data-tid="3" data-vfiid="5">(A copy of the mother’s house registration 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument023" data-did="2" data-tid="3" data-vfiid="5" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="2" data-tid="3" data-vfiid="5"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="2" data-tid="3" data-vfiid="5"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">3</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103130") %> <span class="red-star"></span>:
                                                <p class="h6">(Copy of ID card of father and mother)</p>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">3.1</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103131") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="3" data-tid="1" data-vfiid="7">(A copy of father ID card 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument031" data-did="3" data-tid="1" data-vfiid="7" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="3" data-tid="1" data-vfiid="7"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="3" data-tid="1" data-vfiid="7"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">3.2</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103132") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="3" data-tid="2" data-vfiid="8">(A copy of mother ID card 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument032" data-did="3" data-tid="2" data-vfiid="8" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="3" data-tid="2" data-vfiid="8"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="3" data-tid="2" data-vfiid="8"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">4</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132776") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="4" data-tid="1" data-vfiid="9">(A copy of the Transcript, 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument04" data-did="4" data-tid="1" data-vfiid="9" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="4" data-tid="1" data-vfiid="9"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="4" data-tid="1" data-vfiid="9"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">5</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103136") %> <span class="red-star"></span>:
                                                <p class="h6">(Copy of proof of name-surname change student, father and mother (if any))</p>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">5.1</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103137") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="5" data-tid="1" data-vfiid="11">(Copy of proof of name-surname change of student(if any))</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument051" data-did="5" data-tid="1" data-vfiid="11" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="5" data-tid="1" data-vfiid="11"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="5" data-tid="1" data-vfiid="11"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">5.2</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103138") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="5" data-tid="2" data-vfiid="12">(Copy of proof of name-surname change of father (if any))</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument052" data-did="5" data-tid="2" data-vfiid="12" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="5" data-tid="2" data-vfiid="12"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="5" data-tid="2" data-vfiid="12"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">5.3</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103139") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="5" data-tid="3" data-vfiid="13">(Copy of proof of name-surname change of mother (if any))</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument053" data-did="5" data-tid="3" data-vfiid="13" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="5" data-tid="3" data-vfiid="13"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="5" data-tid="3" data-vfiid="13"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="<%=NoSuankularbNonthaburi%>">
                                        <th scope="row">6</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132777") %> <span class="red-star"></span>:
                                            <p class="h6" data-did="12" data-tid="1" data-vfiid="166">(A copy of household registration of the homeowner or landlord)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument12" data-did="12" data-tid="1" data-vfiid="166" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="12" data-tid="1" data-vfiid="166"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="12" data-tid="1" data-vfiid="166"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="<%=NoSuankularbNonthaburi%>">
                                        <th scope="row">7</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132778") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="13" data-tid="1" data-vfiid="167">(A document of home ownership certificate)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument13" data-did="13" data-tid="1" data-vfiid="167" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="13" data-tid="1" data-vfiid="167"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="13" data-tid="1" data-vfiid="167"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="<%=NoSuankularbNonthaburi%>">
                                        <th scope="row">8</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132779") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="14" data-tid="1" data-vfiid="168">(A document of student special condition)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument14" data-did="14" data-tid="1" data-vfiid="168" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="14" data-tid="1" data-vfiid="168"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="14" data-tid="1" data-vfiid="168"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row" class="re-order">6</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103140") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="6" data-tid="1" data-vfiid="14">(Portfolio, only for students in grades 1 and 4)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument06" data-did="6" data-tid="1" data-vfiid="14" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="6" data-tid="1" data-vfiid="14"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="6" data-tid="1" data-vfiid="14"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row" class="re-order">7</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103141") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="7" data-tid="1" data-vfiid="15">(Copy of adoption registration certificate (In the case of adoption) 1 copy)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument07" data-did="7" data-tid="1" data-vfiid="15" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="7" data-tid="1" data-vfiid="15"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="7" data-tid="1" data-vfiid="15"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row" class="re-order">8</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103142") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="8" data-tid="1" data-vfiid="16">(Medical certificate (Hospital or Clinic))</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument08" data-did="8" data-tid="1" data-vfiid="16" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="8" data-tid="1" data-vfiid="16"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="8" data-tid="1" data-vfiid="16"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <th scope="row" class="re-order">9</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103143") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="9" data-tid="1" data-vfiid="17">(Proof of transfer of the application fee (transfer slip))</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument09" data-did="9" data-tid="1" data-vfiid="17" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="9" data-tid="1" data-vfiid="17"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="9" data-tid="1" data-vfiid="17"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="<%=NoAssumptionSriracha%>">
                                        <th scope="row">10</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132780") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="10" data-tid="1" data-vfiid="18">(Copy of documents showing the father/mother’s being Assumption Sriracha alumni or alumni Current students, brothers/sisters (if any))</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument10" data-did="10" data-tid="1" data-vfiid="18" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="10" data-tid="1" data-vfiid="18"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="10" data-tid="1" data-vfiid="18"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr class="<%=NoAssumptionSriracha%>">
                                        <th scope="row">11</th>
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132781") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="11" data-tid="1" data-vfiid="19">(Copy of the baptismal receipt (For Catholic students) 1 issue)</p>
                                        </td>
                                        <td>
                                            <div class="col-form-label">
                                                <asp:FileUpload ID="FileDocument11" data-did="11" data-tid="1" data-vfiid="19" runat="server" accept="application/pdf, image/*" />
                                                <span class="glyphicon glyphicon-ok-sign no-file" data-did="11" data-tid="1" data-vfiid="19"></span>
                                                <span class="glyphicon glyphicon-folder-open open-file" data-did="11" data-tid="1" data-vfiid="19"></span>
                                                <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                            </div>
                                        </td>
                                        <td></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="form-group row student">
                            <div class="col-md-12 col-sm-12">
                                <div class="col-xs-7 control-label righttext">
                                </div>
                                <div class="col-xs-5 control-input righttext">
                                    <ul class="list-inline ">
                                        <li>
                                            <a href="#top">
                                                <button type="button" class="btn btn-default prev-step "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></button></a>
                                        </li>
                                        <li>
                                            <asp:Button ID="Button2" class="btn btn-danger global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
                                        </li>
                                        <li onclick="loading()">
                                            <asp:Button ID="Button1" class="btn btn-success global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" ValidationGroup="add" />
                                        </li>

                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="row mar_ned">
                            <div class="col-md-12 col-xs-12">
                                <div class="col-md-4 col-sm-4">
                                </div>
                                <div class="form-group row student">
                                    <div class="col-md-8 col-sm-8">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="full-card planelist-container">
        <%--        <a href="plans-term.aspx" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131220") %></a> <a href="periodslist.aspx"
			class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131226") %></a>--%>
    </div>
    <asp:Literal ID="ltrScript" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
