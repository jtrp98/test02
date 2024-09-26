<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="preRegisterEdit.aspx.cs" Inherits="FingerprintPayment.PreRegister.preRegisterEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />

    <%--<script src="//code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

    <object id="objSecuBSP" style="left: 0px; top: 0px" height="0" width="0" classid="CLSID:6283f7ea-608c-11dc-8314-0800200c9a66"
        name="objSecuBSP" viewastext>
    </object>
    <script>



</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
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

        .pad0 {
            padding-left: 0px;
            padding-right: 0px;
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

        function loading() {
            var loading = document.getElementsByClassName("loading");
            loading[0].classList.remove('hidden');
        }

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
            //alert(idlv[1]);
            if (idlv[1] == 1) {
                hiddentime[0].classList.remove('hidden');
                hidden1[0].classList.remove('hidden');
                hidden2[0].classList.add('hidden');
            }
            else if (idlv[1] == 2) {
                hiddentime[0].classList.remove('hidden');
                hidden1[0].classList.add('hidden');
                hidden2[0].classList.remove('hidden');
            }
            else {
                hiddentime[0].classList.add('hidden');
                hidden1[0].classList.add('hidden');
                hidden2[0].classList.add('hidden');
            }
        }

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

        window.onload = startup;
        function startup() {
            ddlcheck2();
            var titletxt = document.getElementsByClassName("titletxt");

            if (titletxt[0].value != "" && titletxt[0].value != "0")
                document.getElementById('ctl00_MainContent_stdtitle').value = titletxt[0].value;
            if (titletxt[1].value != "" && titletxt[1].value != "0")
                document.getElementById('ctl00_MainContent_staywithTitle').value = titletxt[1].value;
            if (titletxt[2].value != "" && titletxt[2].value != "0")
                document.getElementById('ctl00_MainContent_famTitle2').value = titletxt[2].value;
            if (titletxt[3].value != "" && titletxt[3].value != "0")
                document.getElementById('ctl00_MainContent_fatherTitle2').value = titletxt[3].value;
            if (titletxt[4].value != "" && titletxt[4].value != "0")
                document.getElementById('ctl00_MainContent_motherTitle2').value = titletxt[4].value;




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
                            <a href="#step4" data-toggle="tab" aria-controls="complete" role="tab" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %>">
                                <span class="round-tab">
                                    <i class="hid">1</i>
                                    <i class="glyphicon glyphicon-heart"></i>
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

                            <asp:TextBox ID="titlestd" runat="server" CssClass="form-control titletxt hidden"></asp:TextBox>
                            <asp:TextBox ID="titlestay" runat="server" CssClass="form-control titletxt hidden"></asp:TextBox>
                            <asp:TextBox ID="titlefam" runat="server" CssClass="form-control titletxt hidden"></asp:TextBox>
                            <asp:TextBox ID="titlefa" runat="server" CssClass="form-control titletxt hidden"></asp:TextBox>
                            <asp:TextBox ID="titlema" runat="server" CssClass="form-control titletxt hidden"></asp:TextBox>
                            <div class="contentBox">

                                <div class="column70">
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sStudentidtxt" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
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
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:DropDownList ID="optionLevel" runat="server" CssClass="width100 form-control ddlclassroom" onchange="ddlcheck2();">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
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
                                    <%-------%>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:DropDownList ID="stdtitle" runat="server" CssClass="width100 form-control">
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
                                                <asp:TextBox ID="sNickEN" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %>
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
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %>
                                                <br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132770") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sStudentLastOther" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="citizenID" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
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
                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="width100 form-control">
                                                    <asp:ListItem Enabled="true" Text="1" Value="01"></asp:ListItem>
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
                                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="width100 form-control">
                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>" Value="01"></asp:ListItem>
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
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="stdRace" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="stdNation" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="stdReligion" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103059") %></label>
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="width: 50%;">
                                                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="width100 form-control pageson2">
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
                                                <asp:DropDownList ID="DropDownList6" runat="server" CssClass="width100 form-control pageson3">
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
                                                <asp:TextBox ID="stdHomenum2" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
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
                                                <asp:TextBox ID="TextBox50" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103069") %><br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103070") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:DropDownList ID="staywithTitle" runat="server" CssClass="width100 form-control ">
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
                                                <asp:TextBox ID="TextBox1" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103069") %><br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103072") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox20" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103069") %><br />
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103073") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox51" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131214") %><br />
                                                <sub style="font-size: 100%">/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105084") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox6" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext" style="">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101158") %></label>
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="width: 50%;">
                                                <asp:DropDownList ID="DropDownList7" runat="server" CssClass="width100 form-control">
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
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %>" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="อนุบาล 1" Value="39"></asp:ListItem>
                                                            <asp:ListItem Text="อนุบาล 2" Value="40"></asp:ListItem>
                                                            <asp:ListItem Text="อนุบาล 3" Value="41"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ะถม 1" Value="42"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ะถม 2" Value="43"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ะถม 3" Value="44"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ะถม 4" Value="45"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ะถม 5" Value="46"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ะถม 6" Value="47"></asp:ListItem>
                                                            <asp:ListItem Text="มัธยม 1" Value="48"></asp:ListItem>
                                                            <asp:ListItem Text="มัธยม 2" Value="49"></asp:ListItem>
                                                            <asp:ListItem Text="มัธยม 3" Value="50"></asp:ListItem>
                                                            <asp:ListItem Text="มัธยม 4" Value="51"></asp:ListItem>
                                                            <asp:ListItem Text="มัธยม 5" Value="52"></asp:ListItem>
                                                            <asp:ListItem Text="มัธยม 6" Value="53"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>" Value="54"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>" Value="55"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>" Value="-1"></asp:ListItem>

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
                                                <asp:TextBox ID="TextBox13" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox15" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox16" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox19" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
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
                                                        <asp:DropDownList ID="ddlprovince2" runat="server" CssClass="width100 form-control pageprovince" AutoPostBack="True" OnSelectedIndexChanged="ddlprovince2_SelectedIndexChanged">
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
                                                        <asp:DropDownList ID="txtAumper2" runat="server" CssClass="form-control pageaumpher" AutoPostBack="True" OnSelectedIndexChanged="txtAumper2_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="txtTumbon2" runat="server" CssClass="form-control pagetumbon"></asp:DropDownList>
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
                                            <asp:AsyncPostBackTrigger ControlID="ddlprovince2" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtAumper2" EventName="SelectedIndexChanged" />
                                            <asp:PostBackTrigger ControlID="Button1" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox21" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103080") %><br>
                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131221") %></sub></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox22" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
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
                                                        <asp:DropDownList ID="ddlprovince3" runat="server" CssClass="width100 form-control pageprovince" AutoPostBack="True" OnSelectedIndexChanged="ddlprovince3_SelectedIndexChanged">
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
                                                        <asp:DropDownList ID="txtAumper3" runat="server" CssClass="form-control pageaumpher" AutoPostBack="True" OnSelectedIndexChanged="txtAumper3_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103080") %><br>
                                                        <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></sub></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:DropDownList ID="txtTumbon3" runat="server" CssClass="form-control pagetumbon"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlprovince2" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtAumper2" EventName="SelectedIndexChanged" />
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
                                    <div class="form-group row student ">

                                        <div class="col-sm-12 col-md-12">
                                            <div class="user-image">
                                                <asp:Image ID="profileimage" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-sm-5 col-md-5" style="text-align: center;">
                                            <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131208") %></h3>
                                        </div>
                                        <div class="col-sm-9 col-md-9">
                                        </div>
                                        <div class="col-sm-12 col-md-12">
                                            <asp:FileUpload ID="FileUpload1" runat="server" accept="image/*" />
                                        </div>

                                    </div>
                                    <div class="hid">
                                        <p>hidden</p>
                                    </div>
                                    <div class="hidden">
                                        <asp:TextBox runat="server" ID="stdlat" CssClass="fromlat" />
                                        <asp:TextBox runat="server" ID="stdlon" CssClass="fromlon" />
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
                                                var fromlat = document.getElementsByClassName("fromlat");
                                                var fromlon = document.getElementsByClassName("fromlon");
                                                fromlat[0].value = event.latLng.lat();
                                                fromlon[0].value = event.latLng.lng();
                                            }
                                            function initMap() {
                                                var fromlat = document.getElementsByClassName("fromlat");
                                                var fromlon = document.getElementsByClassName("fromlon");


                                                var latlng = new google.maps.LatLng(fromlat[0].value, fromlon[0].value);
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
                                                    fromlat[0].value = this.position.lat();
                                                    fromlon[0].value = this.position.lng();
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


                        <div class="row mar_ned">
                            <div class="col-md-12 col-xs-12">

                                <div class="form-group row student">
                                    <div class="col-md-12 col-sm-12">
                                        <ul class="list-inline pull-right">
                                            <li>
                                                <button type="button" class="btn btn-default prev-step hid">Previous</button></li>
                                            <li><a href="#top">
                                                <button type="button" class="btn btn-primary next-step"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></button></a></li>
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
                                <div class="col-xs-12 hidden">
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
                                                            <asp:DropDownList ID="oldSchoolProvince" AutoPostBack="true" OnSelectedIndexChanged="oldschoolProvince_SelectedIndexChanged" runat="server" CssClass="width100 form-control oProvince">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 control-input">
                                                            <asp:DropDownList ID="oldSchoolAumpher" runat="server" CssClass="form-control oAumpher" OnSelectedIndexChanged="oldschoolaumpher_SelectedIndexChanged" class="input--mid" AutoPostBack="True"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 control-input">
                                                            <asp:DropDownList ID="oldSchoolTumbon" runat="server" CssClass="form-control oTumbon" class="input--mid"></asp:DropDownList>
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
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103097") %></label>
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
                                            <asp:DropDownList ID="oldSchoolGraduated" runat="server" CssClass="width100 form-control oGradu">
                                                <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>" Value=""></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131188") %>" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131189") %>" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131190") %>" Value="13"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131191") %>" Value="14"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131192") %>" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131193") %>" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131194") %>" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131195") %>" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131196") %>" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206406") %>" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131198") %>" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131199") %>" Value="8"></asp:ListItem>
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
                                            <asp:TextBox ID="TextBox23" runat="server" CssClass="form-control oMoveout" class="input--mid"></asp:TextBox>
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
                                <button type="button" class="btn btn-primary next-step"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></button></a></li>
                        </ul>
                    </div>
                    <div class="tab-pane" role="tabpanel" id="step3" style="border-top: 0; padding-top: 0; margin-top: 0;">
                        <div class="step3">
                            <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %></h1>

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
                                                <asp:DropDownList ID="famTitle2" runat="server" CssClass="width100 form-control">
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
                                                <asp:TextBox ID="TextBox24" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
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
                                                <asp:TextBox ID="TextBox25" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 15px;">
                                                <asp:DropDownList ID="DropDownList15" runat="server" CssClass="width100 form-control">
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
                                                <asp:DropDownList ID="DropDownList16" runat="server" CssClass="width100 form-control">
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
                                                <asp:DropDownList ID="DropDownList17" runat="server" CssClass="width100 form-control" Style="">
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
                                                <asp:TextBox ID="famIdCard" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="famRace" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="famNation" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="famReligion" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101192") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="famRelation" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 pad0 control-label righttext" style="text-align: center">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103099") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input" style="">
                                                <asp:DropDownList ID="DropDownList14" runat="server" CssClass="width100 form-control">
                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103100") %>" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101199") %>" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101200") %>" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %></label>
                                            <div class="col-xs-8 control-input">
                                                <asp:DropDownList ID="DropDownList18" runat="server" CssClass="form-control oGradu pagegradu">
                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102054") %>" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %>" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %>" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102059") %> " Value="4 "></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %>" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02176") %>" Value="6"></asp:ListItem>
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
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
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

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="famProvince" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="famaumpher" EventName="SelectedIndexChanged" />

                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="TextBox27" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="TextBox26" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                               <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104044") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox28" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                            </div>
                                            <label class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></label>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %>)</sub></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="famPhone1" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701042") %>)</sub></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="famPhone2" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131217") %>)</sub></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="famPhone3" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
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


                            </div>
                            <div class="col-xs-12">
                                <div class="col-xs-6">
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:DropDownList ID="fatherTitle2" runat="server" CssClass="width100 form-control">
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
                                                <asp:TextBox ID="TextBox29" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
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
                                                <asp:TextBox ID="TextBox30" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 15px;">
                                                <asp:DropDownList ID="DropDownList19" runat="server" CssClass="width100 form-control">
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
                                                <asp:DropDownList ID="DropDownList20" runat="server" CssClass="width100 form-control">
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
                                                <asp:DropDownList ID="DropDownList21" runat="server" CssClass="width100 form-control" Style="">
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
                                                <asp:TextBox ID="fatherIdCard" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="fatherRace" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="fatherNation" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="fatherReligion" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %></label>
                                            <div class="col-xs-8 control-input">
                                                <asp:DropDownList ID="DropDownList22" runat="server" CssClass="form-control oGradu pagegradu">
                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102054") %>" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %>" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %>" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102059") %> " Value="4 "></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %>" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02176") %>" Value="6"></asp:ListItem>
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
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group row student fatherProvinceStatus">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="fatherProvince" AutoPostBack="true" OnSelectedIndexChanged="fatherProvince_SelectedIndexChanged" runat="server" CssClass="width100 form-control fatherProvince pageprovince">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student fatherAumpherStatus">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="fatherAumpher" runat="server" CssClass='form-control fatherAumpher' OnSelectedIndexChanged="fatheraumpher_SelectedIndexChanged" class="input--mid" AutoPostBack="True"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="form-group row student fatherTumbonStatus">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="fatherTumbon" runat="server" CssClass='form-control fatherTumbon' class="input--mid"></asp:DropDownList>
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
                                        </ContentTemplate>
                                        <Triggers>

                                            <asp:AsyncPostBackTrigger ControlID="fatherProvince" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="fatherAumpher" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="TextBox31" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="TextBox32" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                               <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104044") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox33" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                            </div>
                                            <label class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></label>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %>)</sub></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="TextBox34" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701042") %>)</sub></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="TextBox35" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131217") %>)</sub></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="TextBox37" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
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

                            </div>
                            <div class="col-xs-12">
                                <div class="col-xs-6">
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:DropDownList ID="motherTitle2" runat="server" CssClass="width100 form-control">
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
                                                <asp:TextBox ID="TextBox48" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
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
                                                <asp:TextBox ID="TextBox49" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 pad0 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 15px;">
                                                <asp:DropDownList ID="DropDownList24" runat="server" CssClass="width100 form-control">
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
                                                <asp:DropDownList ID="DropDownList25" runat="server" CssClass="width100 form-control">
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
                                                <asp:DropDownList ID="DropDownList26" runat="server" CssClass="width100 form-control" Style="">
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
                                                <asp:TextBox ID="motherIdCard" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="motherRace" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="motherNation" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="motherReligion" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %></label>
                                            <div class="col-xs-8 control-input">
                                                <asp:DropDownList ID="DropDownList23" runat="server" CssClass="form-control oGradu pagegradu">
                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102054") %>" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %>" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %>" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102059") %> " Value="4 "></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %>" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02176") %>" Value="6"></asp:ListItem>
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
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group row student motherProvinceStatus">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="motherProvince" AutoPostBack="true" OnSelectedIndexChanged="motherProvince_SelectedIndexChanged" runat="server" CssClass="width100 form-control motherProvince pageprovince">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student motherAumpherStatus">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="motherAumpher" runat="server" CssClass='form-control motherAumpher' OnSelectedIndexChanged="motheraumpher_SelectedIndexChanged" class="input--mid" AutoPostBack="True"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="form-group row student motherTumbonStatus">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="motherTumbon" runat="server" CssClass='form-control motherTumbon' class="input--mid"></asp:DropDownList>
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
                                        </ContentTemplate>
                                        <Triggers>

                                            <asp:AsyncPostBackTrigger ControlID="motherProvince" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="motherAumpher" EventName="SelectedIndexChanged" />

                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="TextBox38" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="TextBox39" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                               <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104044") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="TextBox40" runat="server" CssClass='form-control famPost' class="input--mid"></asp:TextBox>
                                            </div>
                                            <label class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></label>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %>)</sub></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="TextBox45" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701042") %>)</sub></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="TextBox46" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131217") %>)</sub></label>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                <asp:TextBox ID="TextBox47" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
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
                                                <button type="button" class="btn btn-primary next-step"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></button></a></li>
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
                                                <asp:TextBox ID="stdWeight" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
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
                                                <asp:TextBox ID="stdHeight" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label lefttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103116") %>
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
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sickFood" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131225") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sickDrug" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132159") %>อื่น ๆ</label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sickOther" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101223") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sickNormal" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group row student">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101224") %></label>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                <asp:TextBox ID="sickDanger" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
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

                        <div class="form-group row student">
                            <div class="col-md-12 col-sm-12">
                                <div class="col-xs-7 control-label righttext">
                                </div>
                                <div class="col-xs-5 control-input">
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
