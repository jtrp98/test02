<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="SchoolReportList.aspx.cs" Inherits="FingerprintPayment.grade.SchoolReportList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Styles/protip.min.css">
    <script src="/Scripts/protip.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
        label {
            font-weight: normal;
            font-size: 26px;
        }

        .nopad {
            padding: 0px;
        }

        .nopad100 {
            font-size: 85%;
            width: 124px;
            height: 40px;
            background: rgba(0,0,0,0);
            border: none;
            color: white;
            background-color: #337AB7;
            margin-left: -15px;
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .smol {
            font-size: 85%;
        }

        .centertext {
            text-align: center;
        }

        .lefttext {
            text-align: left;
        }

        .righttext {
            text-align: right;
        }

        .centerText {
            text-align: center;
        }

        th.rotate {
            /* Something you can count on */
            white-space: nowrap;
        }

        .rotate2 {
            transform: rotate(90deg);
        }

        .rotate > div {
            transform:
            /* Magic Numbers */
            translate(5px, 30px)
            /* 45 is really 360 - 45 */
            rotate(270deg);
            width: 30px;
        }

            .rotate > div > span {
            }

        .table_legenda th {
            word-wrap: break-word;
        }

        .gly-rotate-90 {
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=1);
            -webkit-transform: rotate(90deg);
            -moz-transform: rotate(90deg);
            -ms-transform: rotate(90deg);
            -o-transform: rotate(90deg);
            transform: rotate(90deg);
        }

        .gly-rotate-180 {
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=2);
            -webkit-transform: rotate(180deg);
            -moz-transform: rotate(180deg);
            -ms-transform: rotate(180deg);
            -o-transform: rotate(180deg);
            transform: rotate(180deg);
        }

        .gly-rotate-270 {
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);
            -webkit-transform: rotate(270deg);
            -moz-transform: rotate(270deg);
            -ms-transform: rotate(270deg);
            -o-transform: rotate(270deg);
            transform: rotate(270deg);
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .headerCell2 {
        }

        .h50 {
            height: 50px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }
    </style>
    <style>
        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }

        .cover {
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .listItem {
            color: blue;
            background-color: White;
        }

        .hid {
            visibility: hidden;
        }

        .hid2 {
            visibility: hidden;
            display: none;
        }

        .width10 {
            margin: 0 auto;
            width: 10%;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }

        .gvbutton {
            font-size: 25px;
        }

        .nounder a:hover {
            text-decoration: none;
        }

        .dropbtn {
            background-color: #4CAF50;
            color: white;
            padding: 16px;
            font-size: 16px;
            border: none;
        }

        .dropdown {
            position: relative;
            display: inline-block;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: #f1f1f1;
            min-width: 200px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            z-index: 1;
        }

            .dropdown-content a {
                color: black;
                padding: 12px 16px;
                text-decoration: none;
                display: block;
            }

                .dropdown-content a:hover {
                    background-color: #ddd;
                }

        .dropdown:hover .dropdown-content {
            display: block;
        }

        .dropdown:hover .dropbtn {
            background-color: #3e8e41;
        }

        d
        .shadowblack {
            text-decoration: none;
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .boxhead a {
            color: #FFFFFF;
            text-decoration: none;
        }

        a.imjusttext {
            color: #ffffff;
            text-decoration: none;
        }

            a.imjusttext:hover {
                color: aquamarine;
            }

        .btn-red {
            background: red; /* use your color here */
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

        .cool-table .headerCell {
            height: 130px;
        }

        .headerCell22 {
            background-color: #337AB7;
            color: White;
            font-weight: bold;
            border-style: solid;
            border-width: thin;
        }

        .headerCell33 {
            background-color: #337AB7;
            color: White;
            font-weight: bold;
            border-style: solid;
            border-width: thin;
        }

        .nowrap {
            max-width: 100%;
            white-space: nowrap;
        }

        .namemangin {
            margin-left: 5px;
            padding-left: 35px;
            border-left: 10px
        }

        .tab {
            border-collapse: collapse;
            margin-left: 6px;
            margin-right: 6px;
            border-bottom: 3px solid #337AB7;
            border-left: 3px solid #337AB7;
            border-right: 3px solid #337AB7;
            border-top: 3px solid #337AB7;
            box-shadow: inset 0 1px 0 #337AB7;
        }
    </style>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>
    <script type="text/javascript" language="javascript">

        var availableValueplane = [];



        $(document).ready(function () {

            $.protip();

            $('input[id*=btnSearch]').click(function () {
                var load = document.getElementsByClassName("load");
                load[0].classList.remove('hidden');

                var param1var = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                var param2var = $('select[id*=ddlsublevel2] option:selected').val();
                var param3var = $('select[id*=DropDownList1] option:selected').val();
                if (param2var == undefined)
                    param2var = "";
                var param4var = $('select[id*=DropDownList2] option:selected').val();
                if (param4var == undefined)
                    param4var = "";

                if (param4var != "")
                    window.location.href = "Schoolreportlist.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&year=" + param3var + "&term=" + param4var;


            });

            $('input[id*=txtSearch]').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                maxLength: 10,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                source: function (request, response) {
                    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("input[id*=txtSearch]").val(ui.item.label);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });
        });
        function changeFinger() {
            $.ajax("/Api/change/?userid=" + $("#ctl00_MainContent_hdfsid").val() + "&type=0", function (Result) {
            }).done(function (Result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111048") %> " + Result);
                $("#modalpopupdatamac .modal-footer").addClass("hidden");
            });
        }
        function ShowPopUP(userid, name) {
            $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>" +
                "เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
            $("#ctl00_MainContent_hdfsid").val(userid);
            $("#modalpopupdatamac .modal-footer").removeClass("hidden")
        }



        function editddl() {

            var special = document.getElementsByClassName("special");
            var useryear = document.getElementsByClassName("useryear");
            var userterm = document.getElementsByClassName("userterm");
            var userplan = document.getElementsByClassName("userplan");
            var siduser = document.getElementsByClassName("siduser");

            var full = window.location.href;
            var half = full.split('?');
            var split = half[1].split('&');
            var idlv = split[0].split('=');
            var idlv2 = split[1].split('=');
            var year = split[2].split('=');
            var term = split[3].split('=');

            $.get("/App_Logic/schoolReportListDDL.ashx?idlv=" + idlv[1] + "&idlv2=" + idlv2[1] + "&year=" + year[1] + "&term=" + term[1], function (Result) {
                $.each(Result, function (index) {

                    for (var x = 0; x < siduser.length; x++) {

                        if (siduser[x].value == Result[index].planId) {
                            special[x].selectedIndex = Number(Result[index].sortNumber) - 1;
                        }
                    }



                });
            });
        }

        function settingclick() {
            //ddlfill();
            editddl();
        }
        function start() {

            var headertxt = document.getElementsByClassName("headertxt");
            var headertxt2 = document.getElementsByClassName("headertxt2");
            var score = document.getElementsByClassName("txtscore");
            var setup = document.getElementsByClassName("setup");
            var d2 = document.getElementsByClassName("d2");
            var button1 = document.getElementById("ctl00_MainContent_totalstudy");
            var button2 = document.getElementsByClassName("button2");
            var headerCell22 = document.getElementsByClassName("headerCell22");
            var headerCell33 = document.getElementsByClassName("headerCell33");
            var centertext22 = document.getElementsByClassName("centertext22");
            var centertext33 = document.getElementsByClassName("centertext33");
            var centertext44 = document.getElementsByClassName("centertext44");
            var centertext55 = document.getElementsByClassName("centertext55");
            var totalP = document.getElementsByClassName("totalP");
            var hideExport = document.getElementsByClassName("hideExport");

            var full = window.location.href;
            var half = full.split('?');

            var split = "";
            var idlv = "";
            var idlv2 = "";
            var year = "";
            var term = "";


            if (half[1] != null) {
                split = half[1].split('&');
                idlv = split[0];
                idlv2 = split[1].split('=');
                year = split[2];
                term = split[3].split('=');
               
                setTimeout(function () {
                    document.getElementById('<%=DropDownList2.ClientID%>').value = getUrlParameter("term");
                    document.getElementById('<%=ddlsublevel2.ClientID%>').value = d2[0].value;
                }, 2000);
            }

            if (button1.textContent == "0 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %>" || button1.textContent == "" || button1.textContent == null) {
                button2[0].classList.add('hidden');
                button2[1].classList.add('hidden');
                if (button2[2] != undefined)
                    button2[2].classList.add('hidden');
            }



            if (half[1] == null) {
                headertxt[0].classList.add('hidden');
                headertxt[1].classList.add('hidden');
                headertxt[2].classList.add('hidden');
                headertxt[3].classList.add('hidden');
                headertxt2[0].classList.add('hidden');
            }
            ddlyear();
            ddlclass();

            score[0].value = setup[0].value;
            score[1].value = setup[1].value;
            score[2].value = setup[2].value;
            score[3].value = setup[3].value;
            score[4].value = setup[4].value;
            score[5].value = setup[5].value;
            score[6].value = setup[6].value;
            score[7].value = setup[7].value;
            score[8].value = setup[8].value;
            score[9].value = setup[9].value;
            score[10].value = setup[10].value;
            score[11].value = setup[11].value;
            score[12].value = setup[12].value;
            score[13].value = setup[13].value;
            score[14].value = setup[14].value;
            score[15].value = setup[15].value;
            score[16].value = setup[16].value;
            score[17].value = setup[17].value;
            score[18].value = setup[18].value;
            score[19].value = setup[19].value;
            score[20].value = setup[20].value;
            score[21].value = setup[21].value;
            score[22].value = setup[22].value;
            score[23].value = setup[23].value;
            score[24].value = setup[24].value;
            score[25].value = setup[25].value;
            score[26].value = setup[26].value;
            score[27].value = setup[27].value;
            score[28].value = setup[28].value;
            score[29].value = setup[29].value;
            score[30].value = setup[30].value;
            score[31].value = setup[31].value;
            score[32].value = setup[32].value;
            score[33].value = setup[33].value;
            score[34].value = setup[34].value;
            score[35].value = setup[35].value;
            score[36].value = setup[36].value;
            score[37].value = setup[37].value;
            score[38].value = setup[38].value;
            score[39].value = setup[39].value;
            score[40].value = setup[40].value;
            score[41].value = setup[41].value;
            score[42].value = setup[42].value;
            score[43].value = setup[43].value;
            score[44].value = setup[44].value;
            score[45].value = setup[45].value;
            score[46].value = setup[46].value;
            score[47].value = setup[47].value;
            score[48].value = setup[48].value;
            score[49].value = setup[49].value;
            score[50].value = setup[50].value;
            score[51].value = setup[51].value;

            var count = 0;
            if (score[0].value == "") {
                headerCell22[0].classList.remove('headerCell22');
                headerCell33[0].classList.remove('headerCell33');
                centertext22[0].classList.remove('centertext22');
                centertext33[0].classList.remove('centertext33');
                count++;
            }
            if (score[1 - count].value == "") {
                headerCell22[1 - count].classList.remove('headerCell22');
                headerCell33[1 - count].classList.remove('headerCell33');
                centertext22[1 - count].classList.remove('centertext22');
                centertext33[1 - count].classList.remove('centertext33');
                count++;
            }
            if (score[2 - count].value == "") {
                headerCell22[2 - count].classList.remove('headerCell22');
                headerCell33[2 - count].classList.remove('headerCell33');
                centertext22[2 - count].classList.remove('centertext22');
                centertext33[2 - count].classList.remove('centertext33');
                count++;
            }
            if (score[3 - count].value == "") {
                headerCell22[3 - count].classList.remove('headerCell22');
                headerCell33[3 - count].classList.remove('headerCell33');
                centertext22[3 - count].classList.remove('centertext22');
                centertext33[3 - count].classList.remove('centertext33');
                count++;
            }
            if (score[4 - count].value == "") {
                headerCell22[4 - count].classList.remove('headerCell22');
                headerCell33[4 - count].classList.remove('headerCell33');
                centertext22[4 - count].classList.remove('centertext22');
                centertext33[4 - count].classList.remove('centertext33');
                count++;
            }
            if (score[5 - count].value == "") {
                headerCell22[5 - count].classList.remove('headerCell22');
                headerCell33[5 - count].classList.remove('headerCell33');
                centertext22[5 - count].classList.remove('centertext22');
                centertext33[5 - count].classList.remove('centertext33');
                count++;
            }
            if (score[6 - count].value == "") {
                headerCell22[6 - count].classList.remove('headerCell22');
                headerCell33[6 - count].classList.remove('headerCell33');
                centertext22[6 - count].classList.remove('centertext22');
                centertext33[6 - count].classList.remove('centertext33');
                count++;
            }
            if (score[7 - count].value == "") {
                headerCell22[7 - count].classList.remove('headerCell22');
                headerCell33[7 - count].classList.remove('headerCell33');
                centertext22[7 - count].classList.remove('centertext22');
                centertext33[7 - count].classList.remove('centertext33');
                count++;
            }
            if (score[8 - count].value == "") {
                headerCell22[8 - count].classList.remove('headerCell22');
                headerCell33[8 - count].classList.remove('headerCell33');
                centertext22[8 - count].classList.remove('centertext22');
                centertext33[8 - count].classList.remove('centertext33');
                count++;
            }
            if (score[9 - count].value == "") {
                headerCell22[9 - count].classList.remove('headerCell22');
                headerCell33[9 - count].classList.remove('headerCell33');
                centertext22[9 - count].classList.remove('centertext22');
                centertext33[9 - count].classList.remove('centertext33');
                count++;
            }
            if (score[10 - count].value == "") {
                headerCell22[10 - count].classList.remove('headerCell22');
                headerCell33[10 - count].classList.remove('headerCell33');
                centertext22[10 - count].classList.remove('centertext22');
                centertext33[10 - count].classList.remove('centertext33');
                count++;
            }
            if (score[11 - count].value == "") {
                headerCell22[11 - count].classList.remove('headerCell22');
                headerCell33[11 - count].classList.remove('headerCell33');
                centertext22[11 - count].classList.remove('centertext22');
                centertext33[11 - count].classList.remove('centertext33');
                count++;
            }
            if (score[12 - count].value == "") {
                headerCell22[12 - count].classList.remove('headerCell22');
                headerCell33[12 - count].classList.remove('headerCell33');
                centertext22[12 - count].classList.remove('centertext22');
                centertext33[12 - count].classList.remove('centertext33');
                count++;
            }
            if (score[13 - count].value == "") {
                headerCell22[13 - count].classList.remove('headerCell22');
                headerCell33[13 - count].classList.remove('headerCell33');
                centertext22[13 - count].classList.remove('centertext22');
                centertext33[13 - count].classList.remove('centertext33');
                count++;
            }
            if (score[14 - count].value == "") {
                headerCell22[14 - count].classList.remove('headerCell22');
                headerCell33[14 - count].classList.remove('headerCell33');
                centertext22[14 - count].classList.remove('centertext22');
                centertext33[14 - count].classList.remove('centertext33');
                count++;
            }
            if (score[15 - count].value == "") {
                if (headerCell22[15 - count] != undefined)
                    headerCell22[15 - count].classList.remove('headerCell22');
                if (headerCell33[15 - count] != undefined)
                    headerCell33[15 - count].classList.remove('headerCell33');
                if (centertext22[15 - count] != undefined)
                    centertext22[15 - count].classList.remove('centertext22');
                if (centertext33[15 - count] != undefined)
                    centertext33[15 - count].classList.remove('centertext33');
                count++;
            }
            if (score[16 - count].value == "") {
                if (headerCell22[16 - count] != undefined)
                    headerCell22[16 - count].classList.remove('headerCell22');
                if (headerCell33[16 - count] != undefined)
                    headerCell33[16 - count].classList.remove('headerCell33');
                if (centertext22[16 - count] != undefined)
                    centertext22[16 - count].classList.remove('centertext22');
                if (centertext33[16 - count] != undefined)
                    centertext33[16 - count].classList.remove('centertext33');
                count++;
            }
            if (score[17 - count].value == "") {
                if (headerCell22[17 - count] != undefined)
                    headerCell22[17 - count].classList.remove('headerCell22');
                if (headerCell33[17 - count] != undefined)
                    headerCell33[17 - count].classList.remove('headerCell33');
                if (centertext22[17 - count] != undefined)
                    centertext22[17 - count].classList.remove('centertext22');
                if (centertext33[17 - count])
                    centertext33[17 - count].classList.remove('centertext33');
                count++;
            }
            if (score[18 - count].value == "") {
                if (headerCell22[18 - count] != undefined)
                    headerCell22[18 - count].classList.remove('headerCell22');
                if (headerCell33[18 - count] != undefined)
                    headerCell33[18 - count].classList.remove('headerCell33');
                if (centertext22[18 - count] != undefined)
                    centertext22[18 - count].classList.remove('centertext22');
                if (centertext33[18 - count] != undefined)
                    centertext33[18 - count].classList.remove('centertext33');
                count++;
            }
            if (score[19 - count].value == "") {
                if (headerCell22[19 - count] != undefined)
                    headerCell22[19 - count].classList.remove('headerCell22');
                if (headerCell33[19 - count] != undefined)
                    headerCell33[19 - count].classList.remove('headerCell33');
                if (centertext22[19 - count] != undefined)
                    centertext22[19 - count].classList.remove('centertext22');
                if (centertext33[19 - count] != undefined)
                    centertext33[19 - count].classList.remove('centertext33');
                count++;
            }
            if (score[20 - count].value == "") {
                if (headerCell22[20 - count] != undefined)
                    headerCell22[20 - count].classList.remove('headerCell22');
                if (headerCell33[20 - count] != undefined)
                    headerCell33[20 - count].classList.remove('headerCell33');
                if (centertext22[20 - count] != undefined)
                    centertext22[20 - count].classList.remove('centertext22');
                if (centertext33[20 - count] != undefined)
                    centertext33[20 - count].classList.remove('centertext33');
                count++;
            }
            if (score[21 - count].value == "") {
                if (headerCell22[21 - count] != undefined)
                    headerCell22[21 - count].classList.remove('headerCell22');
                if (headerCell33[21 - count] != undefined)
                    headerCell33[21 - count].classList.remove('headerCell33');
                if (centertext22[21 - count] != undefined)
                    centertext22[21 - count].classList.remove('centertext22');
                if (centertext33[21 - count] != undefined)
                centertext33[21 - count].classList.remove('centertext33');
                count++;
            }
            if (score[22 - count].value == "") {
                if (headerCell22[22 - count] != undefined)
                    headerCell22[22 - count].classList.remove('headerCell22');
                if (headerCell33[22 - count] != undefined)
                    headerCell33[22 - count].classList.remove('headerCell33');
                if (centertext22[22 - count] != undefined)
                    centertext22[22 - count].classList.remove('centertext22');
                if (centertext33[22 - count] != undefined)
                    centertext33[22 - count].classList.remove('centertext33');
                count++;
            }
            if (score[23 - count].value == "") {
                if (headerCell22[23 - count] != undefined)
                    headerCell22[23 - count].classList.remove('headerCell22');
                if (headerCell33[23 - count] != undefined)
                    headerCell33[23 - count].classList.remove('headerCell33');
                if (centertext22[23 - count] != undefined)
                    centertext22[23 - count].classList.remove('centertext22');
                if (centertext33[23 - count] != undefined)
                    centertext33[23 - count].classList.remove('centertext33');
                count++;
            }
            if (score[24 - count].value == "") {
                if (headerCell22[24 - count] != undefined)
                    headerCell22[24 - count].classList.remove('headerCell22');
                if (headerCell33[24 - count] != undefined)
                    headerCell33[24 - count].classList.remove('headerCell33');
                if (centertext22[24 - count] != undefined)
                    centertext22[24 - count].classList.remove('centertext22');
                if (centertext33[24 - count] != undefined)
                    centertext33[24 - count].classList.remove('centertext33');
                count++;
            }
            if (score[25 - count].value == "") {
                if (headerCell22[25 - count] != undefined)
                    headerCell22[25 - count].classList.remove('headerCell22');
                if (headerCell33[25 - count] != undefined)
                    headerCell33[25 - count].classList.remove('headerCell33');
                if (centertext22[25 - count] != undefined)
                    centertext22[25 - count].classList.remove('centertext22');
                if (centertext33[25 - count] != undefined)
                    centertext33[25 - count].classList.remove('centertext33');
                count++;
            }

        }

        function ddlfill() {
            var ddltotal = document.getElementsByClassName("ddltotal");
            var grey = document.getElementsByClassName("grey");



            var total = Number(ddltotal[0].value);
            for (var i = 26; i > total; i--) {
                grey[i].classList.add("hidden");

            }
        }

        function dgdclick(id) {
            var data1 = document.getElementsByClassName("dgddata1");
            var data2 = document.getElementsByClassName("dgddata2");
            var data3 = document.getElementsByClassName("dgddata3");
            var data4 = document.getElementsByClassName("dgddata4");

            if (id == 1) {
                data1[0].classList.add("in");
                data1[0].classList.add("active");
                data1[0].classList.remove("hidden");
                data2[0].classList.remove("in");
                data2[0].classList.remove("active");
                data2[0].classList.add("hidden");
                data3[0].classList.remove("in");
                data3[0].classList.remove("active");
                data3[0].classList.add("hidden");
                data4[0].classList.remove("in");
                data4[0].classList.remove("active");
                data4[0].classList.add("hidden");
            }
            if (id == 2) {
                data1[0].classList.remove("in");
                data1[0].classList.remove("active");
                data1[0].classList.add("hidden");
                data2[0].classList.add("active");
                data2[0].classList.add("in");
                data2[0].classList.remove("hidden");
                data3[0].classList.remove("in");
                data3[0].classList.remove("active");
                data3[0].classList.add("hidden");
                data4[0].classList.remove("in");
                data4[0].classList.remove("active");
                data4[0].classList.add("hidden");
            }
            if (id == 3) {
                data1[0].classList.remove("in");
                data1[0].classList.remove("active");
                data1[0].classList.add("hidden");
                data2[0].classList.remove("active");
                data2[0].classList.remove("in");
                data2[0].classList.add("hidden");
                data3[0].classList.add("in");
                data3[0].classList.add("active");
                data3[0].classList.remove("hidden");
                data4[0].classList.remove("in");
                data4[0].classList.remove("active");
                data4[0].classList.add("hidden");
            }
            if (id == 4) {
                data1[0].classList.remove("in");
                data1[0].classList.remove("active");
                data1[0].classList.add("hidden");
                data2[0].classList.remove("active");
                data2[0].classList.remove("in");
                data2[0].classList.add("hidden");
                data3[0].classList.remove("in");
                data3[0].classList.remove("active");
                data3[0].classList.add("hidden");
                data4[0].classList.add("in");
                data4[0].classList.add("active");
                data4[0].classList.remove("hidden");
            }
        }
        function ddlyear() {
            var d1 = document.getElementById('<%=DropDownList1.ClientID%>').value
            var ddl1 = document.getElementsByClassName("ddl1");
            var select = document.getElementById('DD1');

            for (i = -1; i <= 5; i++) {
                ddl1[1].remove(0);
            }

            if (ddl1[0].options[ddl1[0].selectedIndex] != undefined) {
                $.get("/App_Logic/ddlterm.ashx?year=" + ddl1[0].options[ddl1[0].selectedIndex].value, function (Result) {
                    $.each(Result, function (index) {

                        // Create an Option object       
                        var opt = document.createElement("option");

                        // Assign text and value to Option object
                        opt.text = Result[index].value;
                        opt.value = Result[index].value;

                        if (getUrlParameter("term") != "" && getUrlParameter("term") == Result[index].value) {
                            opt.selected = "selected";
                        }


                        // Add an Option object to Drop Down List Box
                        document.getElementById('<%=DropDownList2.ClientID%>').options.add(opt);
                    });
                });
            }
        }

        function ddlclass() {
            var ddl2 = document.getElementsByClassName("ddl2");
            var load = document.getElementsByClassName("load");
            load[0].classList.remove('hidden');

            for (i = -1; i <= 90; i++) {
                ddl2[1].remove(0);
            }
            if (ddl2[0].options[ddl2[0].selectedIndex] != undefined) {

                setTimeout(function () {


                    $.get("/App_Logic/ddlclassroom.ashx?idlv=" + ddl2[0].options[ddl2[0].selectedIndex].value, function (Result) {
                        $.each(Result, function (index) {

                            // Create an Option object       
                            var opt = document.createElement("option");

                            // Assign text and value to Option object
                            opt.text = Result[index].name;
                            opt.value = Result[index].value;

                            if (getUrlParameter("idlv2") != "" && getUrlParameter("idlv2") == Result[index].value) {
                                opt.selected = "selected";
                            }

                            // Add an Option object to Drop Down List Box
                            document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);

                        });
                        load[0].classList.add('hidden');
                    });

                }, 300);
            }
            else {
                 load[0].classList.add('hidden');
            }
        }
        function nextpage(id, name, code) {

            var full = window.location.href;
            var half = full.split('?');
            var split = half[1].split('&');
            var year = split[0];
            var idlv = split[1];
            var idlv2 = split[2];
            var term = split[3];

            if (id == "99999") {
                var signsetup = document.getElementsByClassName("signsetup");
                var win = window.open("Schoolreportiframeall.aspx?" + year + "&" + idlv + "&" + idlv2 + "&" + term + "&id=" + "&mode=all&sign1name=" + signsetup[0].value + "&sign1job=<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210005") %>&sign2name=" + signsetup[1].value + "&sign2job=" + signsetup[2].value + "&name=" + name + "&code=" + code + "&ddl1=0&ddl2=0&ddl3=0&ddl4=0" + "&sign3name=" + signsetup[3].value + "&sign3job=" + signsetup[4].value + "&sign4name=" + signsetup[5].value + "&sign4job=" + signsetup[6].value, '_blank');
                win.focus();
            }
            else if (id == "99998") {
                var win = window.open("SchoolreportPrint.aspx?" + year + "&" + idlv + "&" + idlv2 + "&" + term + "&id=99998", '_blank');
                win.focus();
            }
            else if (id == "99996") {
                var clickButton = document.getElementById("<%= btnExport.ClientID %>");
                clickButton.click();
            }
            else if (id == "99997") {
                var clickButton = document.getElementById("<%= btnExport2.ClientID %>");
                clickButton.click();
            }
            else if (id == "99995") {
                var clickButton = document.getElementById("<%= btnExport3.ClientID %>");
                clickButton.click();
            }
            else if (id === 99994) {
                var clickButton = document.getElementById("<%= btnExport4.ClientID %>");
                clickButton.click();
            }
            else {
                var signsetup = document.getElementsByClassName("signsetup");

                var win = window.open("Schoolreportiframe.aspx?" + year + "&" + idlv + "&" + idlv2 + "&" + term + "&id=" + id + "&mode=single&sign1name=" + signsetup[0].value + "&sign1job=<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210005") %>&sign2name=" + signsetup[1].value + "&sign2job=" + signsetup[2].value + "&name=" + name + "&code=" + code + "&ddl1=0&ddl2=0&ddl3=0&ddl4=0" + "&sign3name=" + signsetup[3].value + "&sign3job=" + signsetup[4].value + "&sign4name=" + signsetup[5].value + "&sign4job=" + signsetup[6].value, '_blank');

                win.focus();

            }

        }
        window.onload = start;
    </script>
    <%--  <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTUser" ServicePath="AutoCompleteService.asmx" EnableCaching="true"
        FirstRowSelected="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>--%>
    <div class="full-card box-content userlist-container">
        <asp:HiddenField ID="hdfsid" runat="server" />
        <div id="loading" class="hidden load"></div>
        <div class="form-group row student">
            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">
                    <label>ddl1</label>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtddl1" runat="server" CssClass="d1" Width="50%"> </asp:TextBox>
                    <asp:TextBox ID="ddltotalplan" runat="server" CssClass="ddltotal" Width="50%"> </asp:TextBox>
                </div>
                <div class="col-xs-2 righttext">
                    <label>ddl2</label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtddl2" runat="server" CssClass="d2" Width="50%"> </asp:TextBox>
                </div>
            </div>

            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="DropDownList1" onchange="ddlyear()" runat="server" CssClass="ddl1 form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddl1 form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group row student hidden">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    mode</label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:TextBox ID="modetxt" runat="server" CssClass="mode" Width="50%"> </asp:TextBox>
                </div>
            </div>

        </div>
        <div class="form-group row student">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlsublevel" onchange="ddlclass()" runat="server" CssClass="ddl2 form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlsublevel2" runat="server" CssClass="ddl2 form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <asp:TextBox ID="signname1" runat="server" CssClass="signsetup" Width="50%"> </asp:TextBox>
            </div>

            <div class="col-xs-2 righttext">
                <asp:TextBox ID="signname2" runat="server" CssClass="signsetup" Width="50%"> </asp:TextBox>
            </div>

            <div class="col-xs-2 righttext">
                <asp:TextBox ID="signjob" runat="server" CssClass="signsetup" Width="50%"> </asp:TextBox>
            </div>

            <div class="col-xs-2 righttext">
                <asp:TextBox ID="signname3" runat="server" CssClass="signsetup" Width="50%"> </asp:TextBox>
            </div>

            <div class="col-xs-2 righttext">
                <asp:TextBox ID="job3" runat="server" CssClass="signsetup" Width="50%"> </asp:TextBox>
            </div>

            <div class="col-xs-2 righttext">
                <asp:TextBox ID="signname4" runat="server" CssClass="signsetup" Width="50%"> </asp:TextBox>
            </div>

            <div class="col-xs-2 righttext">
                <asp:TextBox ID="job4" runat="server" CssClass="signsetup" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>1</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="id1" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
            <div class="col-xs-2 righttext">
                <label>2</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="id2" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>3</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="id3" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
            <div class="col-xs-2 righttext">
                <label>4</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="id4" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>5</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="id5" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
            <div class="col-xs-2 righttext">
                <label>6</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="id6" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>7</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="id7" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
            <div class="col-xs-2 righttext">
                <label>8</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="id8" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>9</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="id9" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
            <div class="col-xs-2 righttext">
                <label>10</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="id10" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>11</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="id11" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
            <div class="col-xs-2 righttext">
                <label>12</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="id12" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>13</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="id13" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>

        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>14</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="id14" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
            <div class="col-xs-2 righttext">
                <label>15</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="id15" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>16</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="id16" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
            <div class="col-xs-2 righttext">
                <label>17</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="id17" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>18</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="id18" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
            <div class="col-xs-2 righttext">
                <label>19</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="id19" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>20</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="id20" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
            <div class="col-xs-2 righttext">
                <label>21</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="id21" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>22</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="id22" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
            <div class="col-xs-2 righttext">
                <label>23</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="id23" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>24</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="id24" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
            <div class="col-xs-2 righttext">
                <label>25</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="id25" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>

            <div class="col-xs-2 righttext">
                <label>26</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="id26" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id27" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id28" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id29" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id30" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id31" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id32" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id33" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id34" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id35" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id36" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id37" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id38" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id39" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id40" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id41" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id42" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id43" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id44" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id45" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id46" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id47" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id48" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id49" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id50" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id51" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
                <asp:TextBox ID="id52" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
        </div>

        <div class="col-xs-12 hidden">
            <div class="col-xs-3 righttext">
                <label>BehaviorPart</label>
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="Textbox13" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
            <div class="col-xs-2 righttext">
                <label>disableReading</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="Textbox14" runat="server" CssClass="setup" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 button-section">
                <input type="button" id="btnSearch" class='btn btn-info search-btn' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
            </div>
        </div>
        <div class="col-xs-12 hid">
            <div class="col-xs-2 righttext">
                <label>hidden</label>
            </div>

        </div>
        <div class="col-xs-12 headertxt">

            <div class="col-xs-6 righttext">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206207") %></label>
            </div>
            <div class="col-xs-2">
                <asp:Label ID="totalstudy" CssClass="totalP"
                    runat="server">                                    
                </asp:Label>
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %>
            </div>

            <div class="col-xs-2 headertxt hid">
                <div class="btn btn-danger " style="width: 150px;" data-toggle="modal" data-target="#myModal" onclick="settingclick()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132245") %></div>
            </div>

            <div class="col-xs-2 headertxt hideExport">
                <div class="dropdown">
                    <button class="btn btn-success button2" style="width: 150px; pointer-events: none; margin-left: 4%; margin-bottom: 10px;">Export to Excel</button>
                    <div class="dropdown-content">
                        <a onclick="nextpage(99997,0,0)" style="cursor: pointer;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206266") %></a>
                        <a onclick="nextpage(99994,0,0)" style="cursor: pointer;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206267") %></a>
                        <a onclick="nextpage(99995,0,0)" style="cursor: pointer;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206268") %></a>
                        <a onclick="nextpage(99996,0,0)" style="cursor: pointer;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206269") %></a>
                    </div>
                </div>
            </div>


            <asp:Button ID="btnExport" CausesValidation="false" class="btn btn-success hidden" runat="server" Text="Export To Excel" />
            <asp:Button ID="btnExport2" CausesValidation="false" class="btn btn-success hidden" runat="server" Text="Export To Excel2" />
            <asp:Button ID="btnExport3" CausesValidation="false" class="btn btn-success hidden" runat="server" Text="Export To Excel3" />
            <asp:Button ID="btnExport4" CausesValidation="false" class="btn btn-success hidden" runat="server" Text="Export To Excel4" />

        </div>
        <div class="col-xs-12 headertxt">

            <div class="col-xs-6 righttext">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206208") %></label>
            </div>
            <div class="col-xs-2">
                <asp:Label ID="registerstudy"
                    runat="server">                                    
                </asp:Label>
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %>
            </div>


            <div class="col-xs-2 headertxt">
                <%-- <input type="button" id="btnPrint" onclick="nextpage(99998,0,0)" style="width: 150px;" class='btn btn-warning search-btn button2' value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132246") %>" />--%>
            </div>

            <div class="col-xs-2 headertxt">
                <input type="button" id="btnPrint2" onclick="nextpage(99999, 0, 0)" style="margin-left: 5%; width: 150px;" class='btn btn-warning search-btn button2' value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106138") %>" />
            </div>


        </div>


        <style>
            .cool-table td {
                padding: 1px;
            }
        </style>
        <div class="col-xs-12 headertxt2">
            <ul class="nav nav-tabs">
                <li class="active" onclick="dgdclick(1)"><a data-toggle="tab" href="#home"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206210") %></a></li>
                <li onclick="dgdclick(2)"><a data-toggle="tab" href="#menu1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206211") %></a></li>
                <li onclick="dgdclick(3)"><a data-toggle="tab" href="#menu1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206212") %></a></li>
                <li onclick="dgdclick(4)"><a data-toggle="tab" href="#menu1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206213") %></a></li>
            </ul>

            <div class="tab-content">
                <div id="home" class="tab-pane fade in active">
                    <h3></h3>
                </div>
                <div id="menu1" class="tab-pane fade">
                    <h3></h3>
                </div>

            </div>

        </div>


        <div class="row mini--space__top fade in active dgddata1">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd2" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />



                        <Columns>
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext counter">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <asp:Label ID="lblstudentcode" runat="server" Width="50px" CssClass="lefttext smol" Text='<%# Eval("studentcode") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Width="190px" CssClass="lefttext smol" Text='<%# Eval("name") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="txtnum" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score1") %>'></asp:Label>
                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan1" runat="server" CssClass="nopad100 txtscore plan1 " Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score2") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan2" runat="server" CssClass="nopad100 txtscore plan2" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score3") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan3" runat="server" CssClass="nopad100 txtscore plan3" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score4") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan4" runat="server" CssClass="nopad100 txtscore plan4" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score5") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan5" runat="server" CssClass="nopad100 txtscore plan5" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score6") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan6" runat="server" CssClass="nopad100 txtscore plan6" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score7") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan7" runat="server" CssClass="nopad100 txtscore plan7" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score8") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan8" runat="server" CssClass="nopad100 txtscore plan8" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score9") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan9" runat="server" CssClass="nopad100 txtscore plan9" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score10") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan10" runat="server" CssClass="nopad100 txtscore plan10" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score11") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan11" runat="server" CssClass="nopad100 txtscore plan11" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score12") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan12" runat="server" CssClass="nopad100 txtscore plan12" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score13") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan13" runat="server" CssClass="nopad100 txtscore plan13" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="nopad">
                                <ItemTemplate>
                                    <i class="fa fa-print" onclick="nextpage('<%# Eval("sID") %>','<%# Eval("name") %>','<%# Eval("studentcode") %>')" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>" style="font-size: 80%; cursor: pointer; color: cadetblue;"></i>&nbsp;
                                    
                                </ItemTemplate>
                                <HeaderStyle />
                                <ItemStyle Width="30" CssClass="nopad"></ItemStyle>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemStyle />
                            </asp:TemplateField>


                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="headerCell headerCell2" />
                        <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="itemCell" />

                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="row mini--space__top fade dgddata2 hidden">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd3" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />
                        <Columns>
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext counter">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <asp:Label ID="lblstudentcode" runat="server" Width="50px" CssClass="lefttext smol" Text='<%# Eval("studentcode") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <asp:Label ID="Label28" runat="server" Width="190px" CssClass="lefttext smol" Text='<%# Eval("name") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label29" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score14") %>'></asp:Label>
                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan14" runat="server" CssClass="nopad100 txtscore plan14" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label30" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score15") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan15" runat="server" CssClass="nopad100 txtscore plan15" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label31" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score16") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan16" runat="server" CssClass="nopad100 txtscore plan16" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label32" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score17") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan17" runat="server" CssClass="nopad100 txtscore plan17" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label33" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score18") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan18" runat="server" CssClass="nopad100 txtscore plan18" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label34" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score19") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan19" runat="server" CssClass="nopad100 txtscore plan19" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label35" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score20") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan20" runat="server" CssClass="nopad100 txtscore plan20" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label36" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score21") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan21" runat="server" CssClass="nopad100 txtscore plan21" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label37" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score22") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan22" runat="server" CssClass="nopad100 txtscore plan22" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label38" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score23") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan23" runat="server" CssClass="nopad100 txtscore plan23" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label39" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score24") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan24" runat="server" CssClass="nopad100 txtscore plan24" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label40" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score25") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan25" runat="server" CssClass="nopad100 txtscore plan25" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label41" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score26") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan26" runat="server" CssClass="nopad100 txtscore plan26" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="nopad">
                                <ItemTemplate>
                                    <i class="fa fa-print" onclick="nextpage('<%# Eval("sID") %>','<%# Eval("name") %>','<%# Eval("studentcode") %>')" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>" style="font-size: 80%; cursor: pointer; color: cadetblue;"></i>&nbsp;
                                    
                                </ItemTemplate>
                                <HeaderStyle />
                                <ItemStyle Width="30" CssClass="nopad"></ItemStyle>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemStyle />
                            </asp:TemplateField>


                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="headerCell" />
                        <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="itemCell" />

                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="row mini--space__top fade dgddata3 hidden">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd4" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />
                        <Columns>
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext counter">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <asp:Label ID="lblstudentcode" runat="server" Width="50px" CssClass="lefttext smol" Text='<%# Eval("studentcode") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <asp:Label ID="Label109" runat="server" Width="190px" CssClass="lefttext smol" Text='<%# Eval("name") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label110" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score27") %>'></asp:Label>
                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan27" runat="server" CssClass="nopad100 txtscore plan27" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label111" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score28") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan28" runat="server" CssClass="nopad100 txtscore plan28" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label112" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score29") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan29" runat="server" CssClass="nopad100 txtscore plan29" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label113" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score30") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan30" runat="server" CssClass="nopad100 txtscore plan30" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label114" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score31") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan31" runat="server" CssClass="nopad100 txtscore plan31" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label115" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score32") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan32" runat="server" CssClass="nopad100 txtscore plan32" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label116" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score33") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan33" runat="server" CssClass="nopad100 txtscore plan33" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label117" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score34") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan34" runat="server" CssClass="nopad100 txtscore plan34" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label118" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score35") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan35" runat="server" CssClass="nopad100 txtscore plan35" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label119" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score36") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan36" runat="server" CssClass="nopad100 txtscore plan36" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label120" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score37") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan37" runat="server" CssClass="nopad100 txtscore plan37" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label121" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score38") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan38" runat="server" CssClass="nopad100 txtscore plan38" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label122" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score39") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan39" runat="server" CssClass="nopad100 txtscore plan39" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="nopad">
                                <ItemTemplate>
                                    <i class="fa fa-print" onclick="nextpage('<%# Eval("sID") %>','<%# Eval("name") %>','<%# Eval("studentcode") %>')" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>" style="font-size: 80%; cursor: pointer; color: cadetblue;"></i>&nbsp;
                                    
                                </ItemTemplate>
                                <HeaderStyle />
                                <ItemStyle Width="30" CssClass="nopad"></ItemStyle>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemStyle />
                            </asp:TemplateField>


                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="headerCell" />
                        <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="itemCell" />

                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="row mini--space__top fade dgddata4 hidden">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd5" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />
                        <Columns>
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext counter">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <asp:Label ID="lblstudentcode" runat="server" Width="50px" CssClass="lefttext smol" Text='<%# Eval("studentcode") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <asp:Label ID="Label123" runat="server" Width="190px" CssClass="lefttext smol" Text='<%# Eval("name") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label124" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score40") %>'></asp:Label>
                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan40" runat="server" CssClass="nopad100 txtscore plan40" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label125" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score41") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan41" runat="server" CssClass="nopad100 txtscore plan41" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label126" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score42") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan42" runat="server" CssClass="nopad100 txtscore plan42" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label127" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score43") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan43" runat="server" CssClass="nopad100 txtscore plan43" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label128" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score44") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan44" runat="server" CssClass="nopad100 txtscore plan44" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label129" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score45") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan45" runat="server" CssClass="nopad100 txtscore plan45" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label130" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score46") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan46" runat="server" CssClass="nopad100 txtscore plan46" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label131" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score47") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan47" runat="server" CssClass="nopad100 txtscore plan47" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label132" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score48") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan48" runat="server" CssClass="nopad100 txtscore plan48" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label133" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score49") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan49" runat="server" CssClass="nopad100 txtscore plan49" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label134" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score50") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan50" runat="server" CssClass="nopad100 txtscore plan50" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label135" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score51") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan51" runat="server" CssClass="nopad100 txtscore plan51" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label136" runat="server" Width="40px" CssClass="centertext smol" Text='<%# Eval("score52") %>'></asp:Label>

                                </ItemTemplate>

                                <HeaderTemplate>
                                    <div class="rotate tdtr5">
                                        <div>
                                            <asp:TextBox ID="plan52" runat="server" CssClass="nopad100 txtscore plan52" Enabled="false"> </asp:TextBox>
                                        </div>
                                    </div>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="nopad">
                                <ItemTemplate>
                                    <i class="fa fa-print" onclick="nextpage('<%# Eval("sID") %>','<%# Eval("name") %>','<%# Eval("studentcode") %>')" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>" style="font-size: 80%; cursor: pointer; color: cadetblue;"></i>&nbsp;
                                    
                                </ItemTemplate>
                                <HeaderStyle />
                                <ItemStyle Width="30" CssClass="nopad"></ItemStyle>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemStyle />
                            </asp:TemplateField>


                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="headerCell" />
                        <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="itemCell" />

                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="row mini--space__top hidden">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" ShowHeader="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />
                        <Columns>
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="studentcode" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104063") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <ItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Width="190px" CssClass="centertext" Text='<%# Eval("name") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="scoreall" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132248") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="scoreget" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132249") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="grade" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206067") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ranking" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132247") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label1") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label16" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label2") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label17" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label3") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label18" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label4") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label19" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label5") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label20" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label6") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label21" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label7") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label22" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label8") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label23" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label9") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label24" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label10") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label25" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label11") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label26" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label12") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label27" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label13") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label42" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label14") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label43" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label15") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label44" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label16") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label45" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label17") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label46" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label18") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label47" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label19") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label48" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label20") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label49" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label21") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label50" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label22") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label51" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label23") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label52" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label24") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label53" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label25") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label54" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label26") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label137" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label27") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label138" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label28") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label139" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label29") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label140" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label30") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label141" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label31") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label142" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label32") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label143" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label33") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label144" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label34") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label145" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label35") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label146" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label36") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label147" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label37") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label148" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label38") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label149" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label39") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label150" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label40") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label151" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label41") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label152" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label42") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label153" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label43") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label154" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label44") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label155" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label45") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label156" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label46") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label157" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label47") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label158" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label48") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label159" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label49") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label160" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label50") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label161" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label51") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label162" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("label52") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                        </Columns>

                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="row mini--space__top hidden">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" ShowHeader="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />



                        <Columns>
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="studentcode" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104063") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <ItemTemplate>
                                    <asp:Label ID="Label55" runat="server" Width="190px" CssClass="centertext" Text='<%# Eval("name") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="scoreall" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132248") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="scoreget" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132249") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="grade" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206067") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ranking" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132247") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label56" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score1") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label57" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score2") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label58" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score3") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label59" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score4") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label60" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score5") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label61" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score6") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label62" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score7") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label63" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score8") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label64" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score9") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label65" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score10") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label66" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score11") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label67" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score12") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label68" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score13") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label69" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score14") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label70" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score15") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label71" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score16") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label72" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score17") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label73" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score18") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label74" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score19") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label75" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score20") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label76" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score21") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label77" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score22") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label78" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score23") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label79" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score24") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label80" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score25") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label81" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score26") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label163" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score27") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label164" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score28") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label165" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score29") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label166" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score30") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label167" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score31") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label168" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score32") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label169" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score33") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label170" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score34") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label171" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score35") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label172" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score36") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label173" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score37") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label174" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score38") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label175" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score39") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label176" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score40") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label177" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score41") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label178" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score42") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label179" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score43") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label180" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score44") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label181" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score45") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label182" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score46") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label183" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score47") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label184" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score48") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label185" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score49") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label186" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score50") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label187" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score51") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label188" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("score52") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                        </Columns>

                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="row mini--space__top hidden">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" ShowHeader="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />



                        <Columns>
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="studentcode" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104063") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <ItemTemplate>
                                    <asp:Label ID="Label82" runat="server" Width="190px" CssClass="centertext" Text='<%# Eval("name") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="scoreall" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132248") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="scoreget" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132249") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Percentage" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206067") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ranking" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132247") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="headerCell">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label83" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade1") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label84" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade2") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label85" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade3") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label86" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade4") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label87" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade5") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label88" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade6") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label89" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade7") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label90" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade8") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label91" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade9") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label92" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade10") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label93" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade11") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label94" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade12") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label95" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade13") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label96" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade14") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label97" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade15") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label98" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade16") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label99" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade17") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label100" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade18") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label101" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade19") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label102" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade20") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label103" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade21") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label104" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade22") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label105" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade23") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label106" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade24") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label107" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade25") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label108" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade26") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label189" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade27") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label190" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade28") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label191" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade29") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label192" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade30") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label193" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade31") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label194" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade32") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label195" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade33") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label196" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade34") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label197" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade35") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label198" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade36") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label199" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade37") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label200" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade38") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label201" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade39") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label202" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade40") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label203" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade41") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label204" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade42") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label205" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade43") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label206" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade44") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label207" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade45") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label208" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade46") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label209" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade47") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label210" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade48") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label211" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade49") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label212" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade50") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label213" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade51") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell33">
                                <ItemTemplate>
                                    <asp:Label ID="Label214" runat="server" Width="40px" CssClass="centertext33" Text='<%# Eval("grade52") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                        </Columns>

                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>


    </div>
</asp:Content>
