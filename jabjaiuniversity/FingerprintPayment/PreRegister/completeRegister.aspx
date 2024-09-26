<%@ Page Title="" Language="C#" MasterPageFile="~/grade/iframe.Master" AutoEventWireup="true" CodeBehind="completeRegister.aspx.cs" Inherits="FingerprintPayment.PreRegister.completeRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    
    <%--<script src="//code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

    <object id="objSecuBSP" style="left: 0px; top: 0px" height="0" width="0" classid="CLSID:6283f7ea-608c-11dc-8314-0800200c9a66"
        name="objSecuBSP" viewastext>
    </object>
    <script>
        $(window).on('load', function () {
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
        });
        $(document).ready(function () {
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
            
            if(index == "1")
                setTimeout(function () {
                    copyAddress();
                }, 1000);
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
        .margin40{
            margin-left:40px;
        }
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
            width: 25%;
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

   

    <script type="text/javascript" language="javascript">


        $(document).ready(function () {
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

                var $active = $('.wizard .nav-tabs li.active');
                $active.next().removeClass('disabled');
                nextTab($active);

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
                      //                  __doPostBack(controlName, ''); //**
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

                if (err != 0)       // Enroll failed
                {
                    //                    alert('Registration failed ! Error Number : [' + err + ']');
                    return;
                }
                else    // Enroll success
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
                //                alert(e.message);
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

            var _str = $("input[id*=txtval]").val();
            var _html = "";
            var _text = "";
            jQuery.each(_str.split(','), function (index, item) {
                if (item != "") {
                    var _id = item;
                    var _val = $("div[id*=" + _id + "]").html();
                    _html += "<span id=" + _id + ">" + _val + "<img src='images/action_delete.png' style='vertical-align: middle;cursor:pointer; 'width='20' height='20' name='" + _id + "' onclick='DelSelectProduct(" + '"' + _id + '"' + ")' /></span><br>";
                    $("div[id*=tdBlackList]").html(_html);
                }
            });
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
    </script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp() {
            popUpObj = window.showModalDialog('frmRegister.aspx?page=users&Firstname=' + document.getElementById("aspnetForm").ctl00$MainContent$txtsName.value
            + '&Lastname=' + document.getElementById("aspnetForm").ctl00$MainContent$txtsLastName.value
            + '&IDCARD=' + document.getElementById("aspnetForm").ctl00$MainContent$txtsIdentification.value, this, 'status:1; resizable:1; dialogWidth:420px; dialogHeight:520px; dialogTop=180px; dialogLeft:510px; scroll:no;status=no');
        }

    </script>

    <script type="text/javascript"
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false">
    </script>
    <script type="text/javascript">
        var geocoder;
        var map;
        function initialize() {
            geocoder = new google.maps.Geocoder();
            var myLatlng = new google.maps.LatLng(13.729550, 100.475519);
            var myOptions = {
                zoom: 7,
                center: myLatlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            map = new google.maps.Map(document.getElementById("gmap"), myOptions);
            // marker refers to a global variable
            marker = new google.maps.Marker({
                position: myLatlng,
                map: map
            });
            // if center changed then update lat and lon document objects
            google.maps.event.addListener(map, 'center_changed', function () {
                var location = map.getCenter();
                document.getElementById("lat").innerHTML = location.lat();

                document.getElementById("lon").innerHTML = location.lng();
                // call function to reposition marker location
                placeMarker(location);
            });
            // if zoom changed, then update document object with new info
            google.maps.event.addListener(map, 'zoom_changed', function () {
                zoomLevel = map.getZoom();
                document.getElementById("zoom_level").innerHTML = zoomLevel;
            });
            // double click on the marker changes zoom level
            google.maps.event.addListener(marker, 'dblclick', function () {
                zoomLevel = map.getZoom() + 1;
                if (zoomLevel == 20) {
                    zoomLevel = 10;
                }
                document.getElementById("zoom_level").innerHTML = zoomLevel;
                map.setZoom(zoomLevel);
            });
            function codeAddress() {
                var address = document.getElementById('address').value;
                geocoder.geocode({ 'address': address }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        map.setCenter(results[0].geometry.location);
                        var marker = new google.maps.Marker({
                            map: map,
                            position: results[0].geometry.location
                        });
                    } else {
                        alert('Geocode was not successful for the following reason: ' + status);
                    }
                });
            }
            function placeMarker(location) {
                var clickedLocation = new google.maps.LatLng(location);
                marker.setPosition(location);
            }
        }
        window.onload = function () { initialize() };
    </script>
    <style type="text/css">
        div#gmap {
            width: 100%;
            height: 500px;
            border: double;
        }
    </style>

    <div class="full-card planelist-container" style="overflow-x: hidden;background-color:white;">
        <div class="full-card box-content row--space">
             <div class="tab-content">                   
                    <div class=""  id="step4" style="border-top: 0; padding-top: 0; margin-top: 0;">
                        <div class="step4">
                            <div class="xs-col-12" style="text-align:center">
                            <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132762") %></h1>
                                </div>
                            <div class="contentBox">
                                <div class="column70">
                                   
                                   

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
                        
                        <div class="form-group row student">
                            <div class="col-md-12 col-sm-12">
                                <div class="col-xs-5 control-label righttext">
                                </div>
                                <div class="col-xs-2 control-input">
                                    
                                            <asp:Button ID="Button1" class="btn btn-success global-btn margin40" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132763") %>" ValidationGroup="add" />
                                       
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
