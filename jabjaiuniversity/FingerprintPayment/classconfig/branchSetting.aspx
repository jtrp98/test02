<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="branchSetting.aspx.cs" Inherits="FingerprintPayment.classconfig.branchSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $("#startDay").datepicker();
            });
        });

        $(document).ready(function () {
            $('.js-example-basic-multiple3').select2();
        });

        $(document).ready(function () {
            $('.js-example-basic-multiple2').select2();
        });

        $(document).ready(function () {
            $(function () {
                $("#modalStart").datepicker();
            });
        });

        $(document).ready(function () {
            $(function () {
                $("#modalEnd").datepicker();
            });
        });

        $(document).ready(function () {
            $(function () {
                $("#editStart").datepicker();
            });
        });

        $(document).ready(function () {
            $(function () {
                $("#editEnd").datepicker();
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $("#endDay").datepicker();
            });

            window.onload = ddlchange();

            var appcolor = document.getElementsByClassName("appcolor");
            for (var x = 1; x <= appcolor.length; x++) {
                paintColor(x);
            }
        });

        function relink() {
            var from = document.getElementsByClassName("linkfrom");
            var to = document.getElementsByClassName("linkto");
            var type = document.getElementsByClassName("linkyear");
            window.location.href = "holidaysettings.aspx?&type=" + type[0].value + "&end=" + to[0].value + "&start=" + from[0].value;
        }
        $(document).ready(function () {
            $('.js-example-basic-multiple1').select2();
        });

        $(document).ready(function () {
            $('.js-example-basic-multiple3').select2();
        });

        $(document).ready(function () {
            $('.js-example-basic-multiple4').select2();
        });

        $(document).ready(function () {
            $('.js-example-basic-multiple2').select2();
        });





        function addclasstext(id) {
            //alert(id);
            //$('multi').remove();

            var textBox = document.createClass("label2");
            var textBox2 = document.createElement("br");
            $('#parent').append(textBox);
            $('#parent').append(textBox2);
            var multi = document.getElementsByClassName("multiadd");
            alert(multi.length);
        }

        function addword() {
            var from = document.getElementsByClassName("classchoose");
            var data = $('.js-example-basic-multiple1').select2('data')

            var mName = document.getElementsByClassName("modalName");
            var multiClass = document.getElementsByClassName("modalMultiClass");
            var mStart = document.getElementsByClassName("modalStart");
            var mEnd = document.getElementsByClassName("modalEnd");
            var mType = document.getElementsByClassName("modalType");
            var mColor = document.getElementsByClassName("modalColor");
            var mWho = document.getElementsByClassName("modalWho");
            var x;
            //alert(data.length);
            multiClass[0].value = "";
            for (x = 0; x < Number(data.length); x++) {
                multiClass[0].value = multiClass[0].value + "/" + data[x].id;
            }

        }

        function addword2() {
            var from = document.getElementsByClassName("classchoose");
            var data = $('.js-example-basic-multiple2').select2('data')

            var mName = document.getElementsByClassName("modalName");
            var multiClass = document.getElementsByClassName("editmulti2");
            var mStart = document.getElementsByClassName("modalStart");
            var mEnd = document.getElementsByClassName("modalEnd");
            var mType = document.getElementsByClassName("modalType");
            var mColor = document.getElementsByClassName("modalColor");
            var mWho = document.getElementsByClassName("modalWho");
            var x;
            //alert(data.length);
            multiClass[0].value = "";
            for (x = 0; x < Number(data.length); x++) {
                multiClass[0].value = multiClass[0].value + "/" + data[x].id;
            }

        }

        function addword3() {
            var from = document.getElementsByClassName("classchoose");
            var data = $('.js-example-basic-multiple3').select2('data')

            var mName = document.getElementsByClassName("modalName");
            var multiClass = document.getElementsByClassName("modalMultiClass2");
            var mStart = document.getElementsByClassName("modalStart");
            var mEnd = document.getElementsByClassName("modalEnd");
            var mType = document.getElementsByClassName("modalType");
            var mColor = document.getElementsByClassName("modalColor");
            var mWho = document.getElementsByClassName("modalWho");
            var x;
            //alert(data.length);
            multiClass[0].value = "";
            for (x = 0; x < Number(data.length); x++) {
                multiClass[0].value = multiClass[0].value + "/" + data[x].id;
            }

        }

        function dgdclick(id) {
            var data1 = document.getElementsByClassName("dgddata1");
            var data2 = document.getElementsByClassName("dgddata2");
            var data3 = document.getElementsByClassName("dgddata3");

            if (id == 1) {
                data1[0].classList.add("in");
                data2[0].classList.remove("in");
                data3[0].classList.remove("in");
                data1[0].classList.add("active");
                data2[0].classList.remove("active");
                data3[0].classList.remove("active");
                data2[0].classList.add("hidden");
                data3[0].classList.add("hidden");
                data1[0].classList.remove("hidden");

            }
            if (id == 2) {
                data1[0].classList.remove("in");
                data3[0].classList.remove("in");
                data2[0].classList.add("in");
                data1[0].classList.remove("active");
                data3[0].classList.remove("active");
                data2[0].classList.add("active");
                data1[0].classList.add("hidden");
                data3[0].classList.add("hidden");
                data2[0].classList.remove("hidden");
            }
            if (id == 3) {
                data1[0].classList.remove("in");
                data2[0].classList.remove("in");
                data3[0].classList.add("in");
                data1[0].classList.remove("active");
                data2[0].classList.remove("active");
                data3[0].classList.add("active");
                data2[0].classList.add("hidden");
                data3[0].classList.remove("hidden");
                data1[0].classList.add("hidden");

            }
        }

        function addword4() {
            var from = document.getElementsByClassName("classchoose");
            var data = $('.js-example-basic-multiple4').select2('data')

            var mName = document.getElementsByClassName("modalName");
            var multiClass = document.getElementsByClassName("editmulti4");
            var mStart = document.getElementsByClassName("modalStart");
            var mEnd = document.getElementsByClassName("modalEnd");
            var mType = document.getElementsByClassName("modalType");
            var mColor = document.getElementsByClassName("modalColor");
            var mWho = document.getElementsByClassName("modalWho");
            var x;
            //alert(data.length);
            multiClass[0].value = "";
            for (x = 0; x < Number(data.length); x++) {
                multiClass[0].value = multiClass[0].value + "/" + data[x].id;
            }

        }


        function ddlchange() {

            $("#ctl00_MainContent_modalClass").html($("#ctl00_MainContent_modalClass option").sort(function (a, b) {
                return parseInt($(a).val()) == parseInt($(b).val()) ? 0 : parseInt($(a).val()) < parseInt($(b).val()) ? -1 : 1;
            }));
            $("#ctl00_MainContent_modalPlanType").html($("#ctl00_MainContent_modalPlanType option").sort(function (a, b) {
                return parseInt($(a).val()) == parseInt($(b).val()) ? 0 : parseInt($(a).val()) < parseInt($(b).val()) ? -1 : 1;
            }));
            $("#ctl00_MainContent_modalPlanType2").html($("#ctl00_MainContent_modalPlanType2 option").sort(function (a, b) {
                return parseInt($(a).val()) == parseInt($(b).val()) ? 0 : parseInt($(a).val()) < parseInt($(b).val()) ? -1 : 1;
            }));
            $("#ctl00_MainContent_modalClass2").html($("#ctl00_MainContent_modalClass2 option").sort(function (a, b) {
                return parseInt($(a).val()) == parseInt($(b).val()) ? 0 : parseInt($(a).val()) < parseInt($(b).val()) ? -1 : 1;
            }));

            var ddltype = document.getElementsByClassName("ddltype");
            var ddlcolor = document.getElementsByClassName("ddlcolor");
            var ddlcolor2 = document.getElementsByClassName("ddlcolor2");
            var ddlevent = document.getElementsByClassName("ddlevent");
            var colorchoose = document.getElementsByClassName("colorchoose");
            var classchoose = document.getElementsByClassName("classchoose");
            var classchoose2 = document.getElementsByClassName("classchoose2");
            var mType = document.getElementsByClassName("modalType");
            var mType2 = document.getElementsByClassName("modalType2");
            var mColor = document.getElementsByClassName("modalColor");
            var mColor2 = document.getElementsByClassName("modalColor2");
            var mWho = document.getElementsByClassName("modalWho");
            //alert(mWho[0].value);



            if (mType[0].value == 0) {
                ddlcolor[1].classList.add('hidden');
                ddlcolor[0].classList.remove('hidden');
                mColor[0].selectedIndex = "0";
            }

            if (mType[0].value == 1) {
                ddlcolor[1].classList.remove('hidden');
                ddlcolor[0].classList.add('hidden');
                mColor[0].selectedIndex = "1";
            }

            if (mType2[0].value == 0) {
                ddlcolor2[1].classList.add('hidden');
                ddlcolor2[0].classList.remove('hidden');
                mColor2[0].selectedIndex = "0";
            }

            if (mType2[0].value == 1) {
                ddlcolor2[1].classList.remove('hidden');
                ddlcolor2[0].classList.add('hidden');
                mColor2[0].selectedIndex = "1";
            }

        }

        function ddlchange2() {
            var ddltype = document.getElementsByClassName("ddltype");
            var ddlcolor = document.getElementsByClassName("ddlcolor");
            var ddlcolor2 = document.getElementsByClassName("ddlcolor2");
            var ddlevent = document.getElementsByClassName("ddlevent");
            var colorchoose = document.getElementsByClassName("colorchoose");
            var classchoose2 = document.getElementsByClassName("classchoose2");
            var classchoose = document.getElementsByClassName("classchoose");
            var mType = document.getElementsByClassName("modalType");
            var mType2 = document.getElementsByClassName("modalType2");
            var mColor = document.getElementsByClassName("modalColor");
            var mColor2 = document.getElementsByClassName("modalColor2");
            var mWho = document.getElementsByClassName("modalWho");
            var mWho2 = document.getElementsByClassName("modalWho2");
            //alert(mWho[0].value);

            if (mWho[0].value != 3) {
                classchoose[0].classList.add('hidden');
            }

            if (mWho[0].value == 3) {
                classchoose[0].classList.remove('hidden');
            }

            if (mWho2[0].value != 3) {
                classchoose2[0].classList.add('hidden');
            }

            if (mWho2[0].value == 3) {
                classchoose2[0].classList.remove('hidden');
            }


            if (mWho[0].value != 4) {
                classchoose[1].classList.add('hidden');
            }

            if (mWho[0].value == 4) {
                classchoose[1].classList.remove('hidden');
            }

            if (mWho2[0].value != 4) {
                classchoose2[1].classList.add('hidden');
            }

            if (mWho2[0].value == 4) {
                classchoose2[1].classList.remove('hidden');
            }

        }

        function editmodal(id) {

            var modalType2 = document.getElementsByClassName("modalType2");
            var modalName2 = document.getElementsByClassName("modalName2");
            var modalStart2 = document.getElementsByClassName("modalStart2");
            var modalEnd2 = document.getElementsByClassName("modalEnd2");
            var modalID = document.getElementsByClassName("modalID");
            modalID[0].value = id;
            var modalColor2 = document.getElementsByClassName("modalColor2");
            var modalWho2 = document.getElementsByClassName("modalWho2");
            var classchoose2 = document.getElementsByClassName("classchoose2");
            var deleteID = document.getElementsByClassName("deleteID");
            deleteID[0].value = id;
            var delete1type = document.getElementsByClassName("delete1type");
            var delete1name = document.getElementsByClassName("delete1name");
            var deleteStart = document.getElementsByClassName("deleteStart");
            var deleteFrom = document.getElementsByClassName("deleteFrom");
            var editmulti = document.getElementsByClassName("editmulti");
            var data = $('.js-example-basic-multiple2').select2('data');



            $.get("/App_Logic/branchEdit.ashx?mode=1&id=" + id, function (Result) {
                $.each(Result, function (index) {

                    modalName2[0].value = Result[index].name;
                    document.getElementById("ctl00_MainContent_editType").selectedIndex = Number(Result[index].nTLevel) - 1;
                    delete1name[0].value = Result[index].name;
                    if (Result[index].nTLevel == "1")
                        delete1type[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>";
                    if (Result[index].nTLevel == "2")
                        delete1type[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>";
                });
            });


        }

        function editmodal2(id) {

            var edit2modalType = document.getElementsByClassName("edit2modaltype");
            var edit2modalName = document.getElementsByClassName("edit2modalName");
            var edit2modalName2 = document.getElementsByClassName("edit2modalName2");
            var modalStart2 = document.getElementsByClassName("modalStart2");
            var modalEnd2 = document.getElementsByClassName("modalEnd2");
            var modalID = document.getElementsByClassName("modalID");
            modalID[1].value = id;
            var modalColor2 = document.getElementsByClassName("modalColor2");
            var modalWho2 = document.getElementsByClassName("modalWho2");
            var classchoose2 = document.getElementsByClassName("classchoose2");
            var deleteID = document.getElementsByClassName("deleteID");
            deleteID[1].value = id;
            var delete2type = document.getElementsByClassName("delete2type");
            var delete2name = document.getElementsByClassName("delete2name");
            var delete2name2 = document.getElementsByClassName("delete2name2");
            var deleteStart = document.getElementsByClassName("deleteStart");
            var deleteFrom = document.getElementsByClassName("deleteFrom");
            var editmulti = document.getElementsByClassName("editmulti");
            var data = $('.js-example-basic-multiple2').select2('data');



            $.get("/App_Logic/branchEdit.ashx?mode=2&id=" + id, function (Result) {
                $.each(Result, function (index) {


                    if (Result[index].nTLevel == "1")
                        edit2modalType[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>";
                    if (Result[index].nTLevel == "2")
                        edit2modalType[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>";
                    edit2modalName[0].value = Result[index].branchName;
                    edit2modalName2[0].value = Result[index].name;

                    if (Result[index].nTLevel == "1")
                        delete2type[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>";
                    if (Result[index].nTLevel == "2")
                        delete2type[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>";
                    delete2name[0].value = Result[index].branchName;
                    delete2name2[0].value = Result[index].name;
                });
            });


        }

        function editmodal3(id) {

            var edit3modalType = document.getElementsByClassName("edit3modaltype");
            var edit3modalName = document.getElementsByClassName("edit3modalName");
            var edit3modalName2 = document.getElementsByClassName("edit3modalName2");
            var edit3modalName3 = document.getElementsByClassName("edit3modalName3");
            var modalStart2 = document.getElementsByClassName("modalStart2");
            var modalEnd2 = document.getElementsByClassName("modalEnd2");
            var modalID = document.getElementsByClassName("modalID");
            modalID[2].value = id;
            var modalColor2 = document.getElementsByClassName("modalColor2");
            var modalWho2 = document.getElementsByClassName("modalWho2");
            var classchoose2 = document.getElementsByClassName("classchoose2");
            var deleteID = document.getElementsByClassName("deleteID");
            deleteID[2].value = id;
            var delete3type = document.getElementsByClassName("delete3type");
            var delete3name = document.getElementsByClassName("delete3name");
            var delete3name2 = document.getElementsByClassName("delete3name2");
            var delete3name3 = document.getElementsByClassName("delete3name3");
            var deleteStart = document.getElementsByClassName("deleteStart");
            var deleteFrom = document.getElementsByClassName("deleteFrom");
            var editmulti = document.getElementsByClassName("editmulti");
            var data = $('.js-example-basic-multiple2').select2('data');



            $.get("/App_Logic/branchEdit.ashx?mode=3&id=" + id, function (Result) {
                $.each(Result, function (index) {


                    if (Result[index].nTLevel == "1")
                        edit3modalType[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>";
                    if (Result[index].nTLevel == "2")
                        edit3modalType[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>";
                    edit3modalName[0].value = Result[index].branchName;
                    edit3modalName2[0].value = Result[index].subjectName;
                    edit3modalName3[0].value = Result[index].name;

                    if (Result[index].nTLevel == "1")
                        delete3type[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>";
                    if (Result[index].nTLevel == "2")
                        delete3type[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>";
                    delete3name[0].value = Result[index].branchName;
                    delete3name2[0].value = Result[index].subjectName;
                    delete3name3[0].value = Result[index].name;
                });
            });


        }

        function paintColor(x) {
            var appcolor = document.getElementsByClassName("appcolor");
            var setcolor1 = document.getElementsByClassName("setcolor1");
            var setcolor2 = document.getElementsByClassName("setcolor2");
            var checktype = document.getElementsByClassName("checktype");
            if (appcolor[x - 1].value == "") {
                if (checktype[x - 1].value == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %>") {
                    setcolor1[x - 1].classList.add('green');
                    setcolor2[x - 1].classList.add('green');
                }
                else {
                    setcolor1[x - 1].classList.add('pink');
                    setcolor2[x - 1].classList.add('pink');
                }
            }
            if (appcolor[x - 1].value == 0) {
                setcolor1[x - 1].classList.add('pink');
                setcolor2[x - 1].classList.add('pink');
            }
            else if (appcolor[x - 1].value == 1) {
                setcolor1[x - 1].classList.add('green');
                setcolor2[x - 1].classList.add('green');
            }
            else if (appcolor[x - 1].value == 2) {
                setcolor1[x - 1].classList.add('blue');
                setcolor2[x - 1].classList.add('blue');
            }
            else if (appcolor[x - 1].value == 3) {
                setcolor1[x - 1].classList.add('grey2');
                setcolor2[x - 1].classList.add('grey2');
            }
            else if (appcolor[x - 1].value == 4) {
                setcolor1[x - 1].classList.add('yellow');
                setcolor2[x - 1].classList.add('yellow');
            }
            //appcolor[x - 1].value = "";
        }
    </script>


    <style>
        /* Tabs*/
        section {
            padding: 60px 0;
        }

            section .section-title {
                text-align: center;
                color: #007b5e;
                margin-bottom: 50px;
                text-transform: uppercase;
            }

        #tabs {
            background: #007b5e;
            color: #eee;
        }

        .select2-results__options {
            font-size: 20px !important;
        }

        #tabs h6.section-title {
            color: #eee;
        }

        .bold {
            font-weight: bold;
        }

        #tabs .nav-tabs .nav-item.show .nav-link, .nav-tabs .nav-link.active {
            color: #f3f3f3;
            background-color: transparent;
            border-color: transparent transparent #f3f3f3;
            border-bottom: 4px solid !important;
            font-size: 20px;
            font-weight: bold;
        }

        #tabs .nav-tabs .nav-link {
            border: 1px solid transparent;
            border-top-left-radius: .25rem;
            border-top-right-radius: .25rem;
            color: #eee;
            font-size: 20px;
        }

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

        .wordWrap {
            word-wrap: break-word; /* IE 5.5-7 */
            white-space: -moz-pre-wrap; /* Firefox 1.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305069") %>.0 */
            white-space: pre-wrap; /* current browsers */
        }

        .pink {
            background-color: #FFAD9E;
        }

        .green {
            background-color: #77EEB5;
        }

        .blue {
            background-color: #CC9FF9;
        }

        .grey2 {
            background-color: #DEDEDE;
        }

        .yellow {
            background-color: #FFD997;
        }


        .select2-selection__rendered {
            line-height: 41px !important;
        }

        .select2-container .select2-selection--single {
            height: 41px !important;
        }

        .select2-selection__arrow {
            height: 41px !important;
        }

            .select2-selection__arrow b {
                border-color: black transparent transparent transparent !important;
            }

        [class^='select2'] {
            border-radius: 4px !important;
            border-top-color: #abadb3 !important;
            border-left-color: #dbdfe6 !important;
            border-right-color: #dbdfe6 !important;
            border-bottom-color: #dbdfe6 !important;
        }


        .bigfont {
            font-size: 200%;
        }

        .smol {
            font-size: 85%;
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

        .centertext {
            text-align: center;
        }

        .righttext {
            text-align: right;
        }

        .lefttext {
            text-align: left;
        }

        .bord {
            border-left: 1px solid #ffffff;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }

        label {
            font-weight: normal;
            font-size: 26px;
        }

        .gvbutton {
            font-size: 25px;
        }

        .nounder a:hover {
            text-decoration: none;
        }

        .button-custom {
            font-size: 26px;
            padding-left: 30px;
            padding-right: 30px;
            width: 100%;
        }

        .shadowblack {
            text-decoration: none;
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
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

        .centerText {
            text-align: center;
        }

        .btn-red {
            background: red; /* use your color here */
        }


        .nowrap {
            max-width: 100%;
            white-space: nowrap;
        }

        .width100 {
            margin: 0 auto;
            width: 100%;
        }

        .namemangin {
            margin-left: 5px;
            padding-left: 35px;
            border-left: 10px;
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="full-card box-content employeeslist-container">

        <div class="col-xs-12" style="padding: 5px;">
            <div class="col-xs-4 righttext" style="padding: 0px;">
                <asp:Label ID="header11" CssClass="bold" runat="server"> </asp:Label>
            </div>

            <div class="col-xs-1">
                <asp:Label ID="header12" CssClass="userplan"
                    runat="server">                                    
                </asp:Label>
            </div>
            <div class="col-xs-4 righttext">
                <asp:Label ID="header13" CssClass="bold" runat="server"> </asp:Label>
            </div>
            <div class="col-xs-1">
                <asp:Label ID="header14" CssClass="useryear"
                    runat="server">                                    
                </asp:Label>
            </div>
            <div class="col-xs-2">
                <div class="btn btn-success pull-right" style="width: 100%;" data-toggle="modal" data-target="#myModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802037") %></div>
            </div>
        </div>

        <div class="col-xs-12" style="padding: 5px;">
            <div class="col-xs-4 righttext" style="padding: 0px;">
                <asp:Label ID="header21" CssClass="bold" runat="server"> </asp:Label>
            </div>

            <div class="col-xs-1">
                <asp:Label ID="header22" CssClass="userplan"
                    runat="server">                                    
                </asp:Label>
            </div>
            <div class="col-xs-4 righttext">
                <asp:Label ID="header23" CssClass="bold" runat="server"> </asp:Label>
            </div>
            <div class="col-xs-1">
                <asp:Label ID="header24" CssClass="useryear"
                    runat="server">                                    
                </asp:Label>
            </div>
            <div class="col-xs-2">
                <div class="btn btn-success pull-right" style="width: 100%;" data-toggle="modal" data-target="#myModal2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802038") %></div>
            </div>
        </div>

        <div class="col-xs-12" style="padding: 5px;">
            <div class="col-xs-4 righttext" style="padding: 0px;">
                <asp:Label ID="header31" CssClass="bold" runat="server"> </asp:Label>
            </div>

            <div class="col-xs-1">
                <asp:Label ID="header32" CssClass="userplan"
                    runat="server">                                    
                </asp:Label>
            </div>
            <div class="col-xs-4 righttext">
                <asp:Label ID="header33" CssClass="bold" runat="server"> </asp:Label>
            </div>
            <div class="col-xs-1">
                <asp:Label ID="header34" CssClass="useryear"
                    runat="server">                                    
                </asp:Label>
            </div>
            <div class="col-xs-2">
                <div class="btn btn-success pull-right" style="width: 100%;" data-toggle="modal" data-target="#myModal3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802042") %></div>
            </div>
        </div>



        <div class="col-xs-12 hidden">
            <p>hidden</p>
        </div>
        <style>
            .cool-table td {
                padding: 1px;
            }
        </style>
        <div class="col-xs-12 headertxt2">
            <ul class="nav nav-tabs">
                <li class="active" onclick="dgdclick(1)"><a data-toggle="tab" href="#home"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206322") %></a></li>
                <li onclick="dgdclick(2)"><a data-toggle="tab" href="#menu1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206323") %></a></li>
                <li onclick="dgdclick(3)"><a data-toggle="tab" href="#menu2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206324") %></a></li>
            </ul>

            <div class="tab-content">
                <div id="home" class="tab-pane fade in active">
                    <h3></h3>
                </div>
                <div id="menu1" class="tab-pane fade">
                    <h3></h3>
                </div>
                <div id="menu2" class="tab-pane fade">
                    <h3></h3>
                </div>

            </div>

        </div>


        <div class="row mini--space__top fade in active dgddata1">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        ShowHeaderWhenEmpty="true" Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline=" False" Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7" BackColor="#337AB7" />

                        <Columns>
                            <asp:BoundField DataField="number" HeaderStyle-Width="100px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201034") %>">
                                <HeaderStyle Width="10%"></HeaderStyle>
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 " style="padding: 0px;">
                                        <input type="text" class="centertext" style="padding: 0px; padding-left: 15px; width: 100%; font-size: 90%; border: 0px;" value="<%# Eval("nTLevel") %>">
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="name" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802036") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="30%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="branchSubject" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802052") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="branchSpec" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802053") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderStyle-Width="10px" HeaderText="" ItemStyle-CssClass="nounder">
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12">
                                        <div class="col-lg-4 col-md-4 col-sm-4 adjust-col-padding col-space nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>">
                                                <div class="glyphicon glyphicon-edit editbox1" onclick="editmodal('<%# Eval("id") %>')" style="font-size: 70%; cursor: pointer;" data-toggle="modal" data-target="#editmodal">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>">
                                                <div class="glyphicon glyphicon-remove" onclick="editmodal('<%# Eval("id") %>')" style="font-size: 70%; cursor: pointer; color: red;" data-toggle="modal" data-target="#deletemodal">
                                                    </a>    
                                                </div>
                                            </div>
                                        </div>
                                </ItemTemplate>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <HeaderStyle Width="1%"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>

                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="headerCell" />
                        <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="itemCell" />
                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />

                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="row mini--space__top fade dgddata2 hidden">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        ShowHeaderWhenEmpty="true" Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7" BackColor="#337AB7" />

                        <Columns>
                            <asp:BoundField DataField="number" HeaderStyle-Width="100px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201034") %>">
                                <HeaderStyle Width="10%"></HeaderStyle>
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 " style="padding: 0px;">
                                        <input type="text" class="centertext" style="padding: 0px; padding-left: 15px; width: 100%; font-size: 90%; border: 0px;" value="<%# Eval("nTLevel") %>">
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="subjectName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802041") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="30%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="branchName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802050") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="spectotal" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802053") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderStyle-Width="10px" HeaderText="" ItemStyle-CssClass="nounder">
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12">
                                        <div class="col-lg-4 col-md-4 col-sm-4 adjust-col-padding col-space nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>">
                                                <div class="glyphicon glyphicon-edit editbox1" onclick="editmodal2('<%# Eval("id") %>')" style="font-size: 70%; cursor: pointer;" data-toggle="modal" data-target="#editmodal2">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>">
                                                <div class="glyphicon glyphicon-remove" onclick="editmodal2('<%# Eval("id") %>')" style="font-size: 70%; cursor: pointer; color: red;" data-toggle="modal" data-target="#deletemodal2">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <HeaderStyle Width="1%"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>

                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="headerCell" />
                        <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="itemCell" />
                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="row mini--space__top fade dgddata3 hidden">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        ShowHeaderWhenEmpty="true" Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7" BackColor="#337AB7" />

                        <Columns>
                            <asp:BoundField DataField="number" HeaderStyle-Width="100px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201034") %>">
                                <HeaderStyle Width="10%"></HeaderStyle>
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 " style="padding: 0px;">
                                        <input type="text" class="centertext" style="padding: 0px; padding-left: 15px; width: 100%; font-size: 90%; border: 0px;" value="<%# Eval("nTLevel") %>">
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="BranchSpecName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802045") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="30%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="branchName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802050") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="subjectName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802051") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderStyle-Width="10px" HeaderText="" ItemStyle-CssClass="nounder">
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12">
                                        <div class="col-lg-4 col-md-4 col-sm-4 adjust-col-padding col-space nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>">
                                                <div class="glyphicon glyphicon-edit editbox1" onclick="editmodal3('<%# Eval("id") %>')" style="font-size: 70%; cursor: pointer;" data-toggle="modal" data-target="#editmodal3">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>">
                                                <div class="glyphicon glyphicon-remove" onclick="editmodal3('<%# Eval("id") %>')" style="font-size: 70%; cursor: pointer; color: red;" data-toggle="modal" data-target="#deletemodal3">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <HeaderStyle Width="1%"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>

                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="headerCell" />
                        <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="itemCell" />
                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalDetail" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132054") %></h2>
                    </div>
                    <div class="modal-body">
                        <div class="col-xs-12" style="padding: 5px;">
                            <div id="parent" class="col-xs-12">
                                <asp:TextBox ID="Textbox4" class="form-control wordWrap multiadd" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802032") %></h2>
                    </div>
                    <div class="">
                        <div class="col-xs-12" style="margin-top: 30px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802033") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="ddlRegisterType" runat="server" class="" Width="80%">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>" Value="1" class="grey"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>" Value="2" class="grey"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-12" style="margin-top: 10px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802036") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modalPlanName" class="form-control modalName" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="hid" style="font-size: 30%">hidden</div>

                        <div class="modal-footer">
                            <asp:Button CssClass="btn btn-primary global-btn" ID="Button1" runat="server" Style="width: 100px;" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal2" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802039") %></h2>
                    </div>
                    <div class="">
                        <div class="col-xs-12" style="margin-top: 30px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802040") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="ddlSubject" runat="server" class="form-control" Width="80%" CssClass="js-example-basic-multiple3" name="classchoice3[]">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-xs-12" style="margin-top: 10px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802041") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="subjectName" class="form-control modalName" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="hid" style="font-size: 30%">hidden</div>

                        <div class="modal-footer">
                            <asp:Button CssClass="btn btn-primary global-btn" ID="Button2" runat="server" Style="width: 100px;" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal3" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802043") %></h2>
                    </div>
                    <div class="">

                        <div class="col-xs-12" style="margin-top: 30px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802044") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="ddlSpec" runat="server" class="form-control" Width="80%" CssClass="js-example-basic-multiple2" name="classchoice2[]">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-12" style="margin-top: 10px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802041") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="specName" class="form-control modalName" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="hid" style="font-size: 30%">hidden</div>
                        <div class="modal-footer">
                            <asp:Button CssClass="btn btn-primary global-btn" ID="Button3" runat="server" Style="width: 100px;" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="deletemodal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802056") %></h2>
                    </div>
                    <div class="modal-body">
                        <div class="col-xs-12 hidden" style="padding: 4px;">
                            <div class="col-xs-4 righttext">
                                <label>
                                    id
                                </label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="deleteid" runat="server" ClientIDMode="static" CssClass="form-control linkfrom deleteID" Width="50%" />
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802033") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox2" class="form-control delete1type" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802036") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox1" class="form-control delete1name" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="deleteBtn" runat="server" Style="width: 100px;" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="deletemodal2" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802056") %></h2>
                    </div>
                    <div class="modal-body">

                        <div class="col-xs-12 hidden" style="padding: 4px;">
                            <div class="col-xs-4 righttext">
                                <label>
                                    id
                                </label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="delete2id" runat="server" ClientIDMode="static" CssClass="form-control linkfrom deleteID" Width="50%" />
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802033") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox13" class="form-control delete2type" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802036") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox14" class="form-control delete2name" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802041") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox15" class="form-control delete2name2" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="deleteBtn2" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" />

                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="deletemodal3" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802056") %></h2>
                    </div>
                    <div class="modal-body">

                        <div class="col-xs-12 hidden" style="padding: 4px;">
                            <div class="col-xs-4 righttext">
                                <label>
                                    id
                                </label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="delete3id" runat="server" ClientIDMode="static" CssClass="form-control linkfrom deleteID" Width="50%" />
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802033") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox17" class="form-control delete3type" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802036") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox18" class="form-control delete3name" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802041") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox19" class="form-control delete3name2" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802045") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox20" class="form-control delete3name3" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="deleteBtn3" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" />

                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="editmodal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00139") %></h2>
                    </div>
                    <div class="modal-body">

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802033") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="editType" runat="server" class="form-control" Width="80%">
                                    <%--<asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>" Value="1" class="grey"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>" Value="2" class="grey"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802036") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="editName" class="form-control modalName2" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>



                        <div class="col-xs-12 hidden" style="padding: 4px;">
                            <div class="col-xs-4 righttext">
                                <label>
                                    id
                                </label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="editid" runat="server" ClientIDMode="static" CssClass="form-control linkfrom modalID" Width="50%" />
                            </div>
                        </div>


                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">

                        <asp:Button CssClass="btn btn-primary global-btn" ID="editSave" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />

                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="editmodal2" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802054") %></h2>
                    </div>
                    <div class="modal-body">

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802033") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox9" class="form-control edit2modaltype" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802055") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox5" class="form-control edit2modalName" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802041") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="edit2Name" class="form-control edit2modalName2" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>


                        <div class="col-xs-12 hidden" style="padding: 4px;">
                            <div class="col-xs-4 righttext">
                                <label>
                                    id
                                </label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="edit2id" runat="server" ClientIDMode="static" CssClass="form-control linkfrom modalID" Width="50%" />
                            </div>
                        </div>


                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">

                        <asp:Button CssClass="btn btn-primary global-btn" ID="editSave2" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />

                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="editmodal3" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00143") %></h2>
                    </div>
                    <div class="modal-body">

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802033") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox7" class="form-control edit3modaltype" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802055") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox8" class="form-control edit3modalName" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132055") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox10" class="form-control edit3modalName2" disabled="disabled" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802045") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="edit3name" class="form-control edit3modalName3" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12 hidden" style="padding: 4px;">
                            <div class="col-xs-4 righttext">
                                <label>
                                    id
                                </label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="edit3id" runat="server" ClientIDMode="static" CssClass="form-control linkfrom modalID" Width="50%" />
                            </div>
                        </div>


                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">

                        <asp:Button CssClass="btn btn-primary global-btn" ID="editSave3" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />

                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


