<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="importExcel.aspx.cs" Inherits="FingerprintPayment.import.importExcel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
        label {
            font-weight: normal;
            font-size: 26px;
        }

        .disable {
            pointer-events: none;
            background-color: #f0f0f0;
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .centertext {
            text-align: center;
        }

        .centerText {
            text-align: center;
        }

        .setmin-width0 {
            min-width: 0px !important;
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

        .width10 {
            margin: 0 auto;
            width: 10%;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }

        .extime {
            font-size: 1% !important;
        }

        .gvbutton {
            font-size: 25px;
        }

        .labelbox {
            pointer-events: none;
            border: 0px;
        }

        .whitetxt {
            color: white;
        }

        .nounder a:hover {
            text-decoration: none;
        }

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

        .lh5 {
            line-height: 19px;
        }

        .pad0 {
            padding: 0px;
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

        .lds-facebook {
            display: inline-block;
            position: relative;
            width: 64px;
            height: 64px;
        }

        .lds-facebook div {
            display: inline-block;
            position: absolute;
            left: 6px;
            width: 13px;
            background: #cef;
            animation: lds-facebook 1.2s cubic-bezier(0, 0.5, 0.5, 1) infinite;
        }

        .lds-facebook div:nth-child(1) {
            left: 6px;
            animation-delay: -0.24s;
        }

        .lds-facebook div:nth-child(2) {
            left: 26px;
            animation-delay: -0.12s;
        }

        .lds-facebook div:nth-child(3) {
            left: 45px;
            animation-delay: 0;
        }

        .buttonhidden1 {
            display: none !important;
        }

        .buttonhidden2 {
            display: none !important;
        }

        @keyframes lds-facebook {
            0% {
                top: 6px;
                height: 51px;
            }

            50%, 100% {
                top: 19px;
                height: 26px;
            }
        }
    </style>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.8.0/jszip.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.8.0/xlsx.js"></script>
    <script>
        var ExcelToJSON = function () {
            var grade = document.getElementsByClassName("grade");
            var stdscore1 = document.getElementsByClassName("stdscore1");
            var stdscore2 = document.getElementsByClassName("stdscore2");
            var stdscore3 = document.getElementsByClassName("stdscore3");
            var stdscore4 = document.getElementsByClassName("stdscore4");
            var stdscore5 = document.getElementsByClassName("stdscore5");
            var stdscore6 = document.getElementsByClassName("stdscore6");
            var stdscore7 = document.getElementsByClassName("stdscore7");
            var stdscore8 = document.getElementsByClassName("stdscore8");
            var stdscore9 = document.getElementsByClassName("stdscore9");
            var stdscore10 = document.getElementsByClassName("stdscore10");
            var stdscore11 = document.getElementsByClassName("stdscore11");
            var stdscore12 = document.getElementsByClassName("stdscore12");
            var stdscore13 = document.getElementsByClassName("stdscore13");
            var stdscore14 = document.getElementsByClassName("stdscore14");
            var stdscore15 = document.getElementsByClassName("stdscore15");
            var stdscore16 = document.getElementsByClassName("stdscore16");
            var stdscore17 = document.getElementsByClassName("stdscore17");
            var stdscore18 = document.getElementsByClassName("stdscore18");
            var stdscore19 = document.getElementsByClassName("stdscore19");
            var stdscore20 = document.getElementsByClassName("stdscore20");
            var stdchewat1 = document.getElementsByClassName("stdchewat1");
            var stdchewat2 = document.getElementsByClassName("stdchewat2");
            var stdchewat3 = document.getElementsByClassName("stdchewat3");
            var stdchewat4 = document.getElementsByClassName("stdchewat4");
            var stdchewat5 = document.getElementsByClassName("stdchewat5");
            var stdchewat6 = document.getElementsByClassName("stdchewat6");
            var stdchewat7 = document.getElementsByClassName("stdchewat7");
            var stdchewat8 = document.getElementsByClassName("stdchewat8");
            var stdchewat9 = document.getElementsByClassName("stdchewat9");
            var stdchewat10 = document.getElementsByClassName("stdchewat10");
            var stdchewat11 = document.getElementsByClassName("stdchewat11");
            var stdchewat12 = document.getElementsByClassName("stdchewat12");
            var stdchewat13 = document.getElementsByClassName("stdchewat13");
            var stdchewat14 = document.getElementsByClassName("stdchewat14");
            var stdchewat15 = document.getElementsByClassName("stdchewat15");
            var stdchewat16 = document.getElementsByClassName("stdchewat16");
            var stdchewat17 = document.getElementsByClassName("stdchewat17");
            var stdchewat18 = document.getElementsByClassName("stdchewat18");
            var stdchewat19 = document.getElementsByClassName("stdchewat19");
            var stdchewat20 = document.getElementsByClassName("stdchewat20");
            var stdbehave1 = document.getElementsByClassName("stdbehave1");
            var stdbehave2 = document.getElementsByClassName("stdbehave2");
            var stdbehave3 = document.getElementsByClassName("stdbehave3");
            var stdbehave4 = document.getElementsByClassName("stdbehave4");
            var stdbehave5 = document.getElementsByClassName("stdbehave5");
            var stdbehave6 = document.getElementsByClassName("stdbehave6");
            var stdbehave7 = document.getElementsByClassName("stdbehave7");
            var stdbehave8 = document.getElementsByClassName("stdbehave8");
            var stdbehave9 = document.getElementsByClassName("stdbehave9");
            var stdbehave10 = document.getElementsByClassName("stdbehave10");
            var stdmid1 = document.getElementsByClassName("stdmid1");
            var stdmid2 = document.getElementsByClassName("stdmid2");
            var stdmid3 = document.getElementsByClassName("stdmid3");
            var stdmid4 = document.getElementsByClassName("stdmid4");
            var stdmid5 = document.getElementsByClassName("stdmid5");
            var stdmid6 = document.getElementsByClassName("stdmid6");
            var stdmid7 = document.getElementsByClassName("stdmid7");
            var stdmid8 = document.getElementsByClassName("stdmid8");
            var stdmid9 = document.getElementsByClassName("stdmid9");
            var stdmid10 = document.getElementsByClassName("stdmid10");
            var stdfinal1 = document.getElementsByClassName("stdfinal1");
            var stdfinal2 = document.getElementsByClassName("stdfinal2");
            var stdfinal3 = document.getElementsByClassName("stdfinal3");
            var stdfinal4 = document.getElementsByClassName("stdfinal4");
            var stdfinal5 = document.getElementsByClassName("stdfinal5");
            var stdfinal6 = document.getElementsByClassName("stdfinal6");
            var stdfinal7 = document.getElementsByClassName("stdfinal7");
            var stdfinal8 = document.getElementsByClassName("stdfinal8");
            var stdfinal9 = document.getElementsByClassName("stdfinal9");
            var stdfinal10 = document.getElementsByClassName("stdfinal10");
            var stdmidtotal = document.getElementsByClassName("stdmidtotal");
            var stdfinaltotal = document.getElementsByClassName("stdfinaltotal");
            var stdsamatanatotal = document.getElementsByClassName("stdsamatanatotal");
            var stdreadwritetotal = document.getElementsByClassName("stdreadwritetotal");
            var stdbehavetotal = document.getElementsByClassName("stdbehavetotal");
            var uploadstatus = document.getElementsByClassName("uploadstatus");

            var planheader = document.getElementsByClassName("planheader");
            var stdheader = document.getElementsByClassName("stdheader");
            var tranheader = document.getElementsByClassName("tranheader");
            var desirableAttributeCount = document.getElementsByClassName("DesirableAttributeCount");
            var readWriteCount = document.getElementsByClassName("ReadWriteCount");
            var samattanaCount = document.getElementsByClassName("SamattanaCount");

            var plancode = document.getElementsByClassName("plancode");
            var planname = document.getElementsByClassName("planname");
            var planstat = document.getElementsByClassName("planstat");

            var stdCode = document.getElementsByClassName("stdCode");
            var stdname = document.getElementsByClassName("stdname");
            var stdStat = document.getElementsByClassName("stdStat");
            var stdsid = document.getElementsByClassName("stdsid");

            var stdhidden1 = document.getElementsByClassName("stdhidden1");
            var stdhidden2 = document.getElementsByClassName("stdhidden2");
            var stdhidden3 = document.getElementsByClassName("stdhidden3");
            var stdhidden4 = document.getElementsByClassName("stdhidden4");

            var plnhidden1 = document.getElementsByClassName("plnhidden1");
            var plnhidden2 = document.getElementsByClassName("plnhidden2");
            var plnhidden3 = document.getElementsByClassName("plnhidden3");
            var plnhidden4 = document.getElementsByClassName("plnhidden4");

            var datahidden1 = document.getElementsByClassName("datahidden1");
            var datahidden2 = document.getElementsByClassName("datahidden2");
            var datahidden3 = document.getElementsByClassName("datahidden3");
            var datahidden4 = document.getElementsByClassName("datahidden4");

            var datastd = document.getElementsByClassName("datastd");
            var datacode = document.getElementsByClassName("datacode");
            var datagrade = document.getElementsByClassName("datagrade");
            var pagedgd = document.getElementsByClassName("pagedgd");

            var desirableDatahidden1 = document.getElementsByClassName("DesirableDatahidden1");
            var desirableDatahidden2 = document.getElementsByClassName("DesirableDatahidden2");
            var desirableDatahidden3 = document.getElementsByClassName("DesirableDatahidden3");

            var desirableDatastd = document.getElementsByClassName("DesirableDatastd");
            var desirableDatagrade = document.getElementsByClassName("DesirableDatagrade");

            var readWriteDatahidden1 = document.getElementsByClassName("ReadWriteDatahidden1");
            var readWriteDatahidden2 = document.getElementsByClassName("ReadWriteDatahidden2");
            var readWriteDatahidden3 = document.getElementsByClassName("ReadWriteDatahidden3");

            var readWriteDatastd = document.getElementsByClassName("ReadWriteDatastd");
            var readWriteDatagrade = document.getElementsByClassName("ReadWriteDatagrade");

            var samattanaDatahidden1 = document.getElementsByClassName("SamattanaDatahidden1");
            var samattanaDatahidden2 = document.getElementsByClassName("SamattanaDatahidden2");
            var samattanaDatahidden3 = document.getElementsByClassName("SamattanaDatahidden3");

            var samattanaDatastd = document.getElementsByClassName("SamattanaDatastd");
            var samattanaDatagrade = document.getElementsByClassName("SamattanaDatagrade");


            var loaddata = document.getElementsByClassName("loaddata");
            var loadlogo = document.getElementsByClassName("loadlogo");
            var savebutton1 = document.getElementsByClassName("savebutton1");
            var savebutton2 = document.getElementsByClassName("savebutton2");
            var disabletarget = document.getElementsByClassName("disabletarget");

            if (savebutton1[0] != undefined)
                savebutton1[0].classList.remove('hidden');

            if (savebutton2[0] != undefined)
                savebutton2[0].classList.add('hidden');

            if (loaddata[0] != undefined)
                loaddata[0].classList.add('hidden');
            if (loadlogo[0] != undefined)
                loadlogo[0].classList.remove('hidden');
            //console.log("1");
            var studentCode = [];
            this.parseExcel = function (file) {
                var reader = new FileReader();
                var isNewSheetExist = false;
                reader.onload = function (e) {
                    var data = e.target.result;
                    var workbook = XLSX.read(data, {
                        type: 'binary'
                    });
                    var waiting = workbook.SheetNames.length;
                    workbook.SheetNames.forEach(function (sheetName) {
                        // Here is your object

                        var XL_row_object = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                        var json_object = JSON.stringify(XL_row_object);
                        //console.log(JSON.parse(json_object));
                        var data = JSON.parse(json_object);
                        //var yyy = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %> " + data.length;
                        //console.log(data);
                        stdheader[1].textContent = 0;
                        stdheader[2].textContent = 0;

                        if (sheetName == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132303") %>") {

                            var columnsIn = [];

                            var worksheet = workbook.Sheets[sheetName];
                            var cells = Object.keys(worksheet);
                            //console.log(cells);
                            for (var i = 0; i < Object.keys(cells).length; i++) {
                               /* console.log(cells[i]);*/
                                if (cells[i].indexOf('1') > -1 && worksheet[cells[i]].v != "" && worksheet[cells[i]].v != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && worksheet[cells[i]].v != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>") {
                                    if (worksheet[cells[i]].v == "" || worksheet[cells[i]].v == "**") break;

                                    columnsIn.push(worksheet[cells[i]].v); //Contails all column names
                                }
                            }

                            //console.log("colValues" + columnsIn);

                            var countStd = 0;
                            for (var x = 0; x < 700; x++) {
                                if (data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != null && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != undefined && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != "")
                                    countStd++;
                            }

                            if (uploadstatus[0] != undefined)
                                uploadstatus[0].classList.remove('hidden');
                            if (disabletarget[0] != undefined)
                                disabletarget[0].classList.add('disable');
                            if (disabletarget[1] != undefined)
                                disabletarget[1].classList.add('disable');
                            var plancodelist = "";
                            //var columnsIn = data[0];
                            var plancount = 0;

                            var stdCodeList = [];
                            for (var x = 0; x < countStd; x++) {
                                if (data[x].<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> != null && data[x].<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> != undefined && data[x].<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> != "")
                                    stdCodeList[x] = data[x].<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>;
                            }

                            var countloop2 = 0;
                            for (var key = 0; key < columnsIn.length; key++) {
                                console.log(columnsIn[key]);
                                if (columnsIn[key] != undefined) {
                                    console.log("key " + columnsIn[key]);
                                    for (var x = 0; x < countStd; x++) {
                                        if (columnsIn[key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && columnsIn[key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>" && columnsIn[key] != "") {
                                            /*if (data[x][columnsIn[key]] != null && data[x][columnsIn[key]] != undefined) {*/
                                                countloop2 = countloop2 + 1;
                                           /* }*/
                                        }
                                    }
                                }
                            }
                            console.log(countloop2);
                            var plancount2 = 0;
                            for (var key = 0; key < columnsIn.length; key++) {
                                if (columnsIn[key] != undefined) {
                                    if (columnsIn[key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && columnsIn[key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>" && columnsIn[key] != "") {
                                        plancount2 = plancount2 + 1;
                                    }
                                }
                            }

                            //console.log("countStd" + countStd);
                            //console.log("countStd" + countloop2);

                            $('#TextBox12').val(countStd);
                            $('#TextBox13').val(countloop2);
                            $('#txtDesirableAttribute').val(countStd);
                            $('#txtReadWrite').val(countStd);
                            $('#txtSamattana').val(countStd);
                            $('#TextBox11').val(plancount2);
                            $('#TextBox11').trigger('change');


                            setTimeout(function () {
                                var countloop = 0;
                                console.log("columnsIn" + columnsIn);
                                for (var key = 0; key < columnsIn.length; key++) {
                                    console.log("countStd" + countStd);
                                    for (var x = 0; x < countStd; x++) {
                                        //if (stdCodeList[x] == "8719") {
                                        //    console.log("columnsIn[key]" + columnsIn[key]);
                                            //if (columnsIn[key] == "ง32202") {
                                            //    console.log(data[x][columnsIn[key]]);
                                            //}
                                            var gradevalue = (data[x][columnsIn[key]] != undefined) ? data[x][columnsIn[key]] : null;
                                            if (columnsIn[key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && columnsIn[key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>" && columnsIn[key] != undefined && columnsIn[key] != "undefined") {
                                                /* console.log("data[x][key]" + data[x][columnsIn[key]]);*/
                                                //if (data[x][columnsIn[key]] != null && data[x][columnsIn[key]] != undefined) {
                                                if (datahidden1[countloop] != undefined)
                                                    datahidden1[countloop].classList.remove('hidden');
                                                if (datahidden2[countloop] != undefined)
                                                    datahidden2[countloop].classList.remove('hidden');
                                                if (datahidden3[countloop] != undefined)
                                                    datahidden3[countloop].classList.remove('hidden');
                                                if (datahidden4[countloop] != undefined)
                                                    datahidden4[countloop].classList.remove('hidden');
                                                if (datastd[countloop] != undefined)
                                                    datastd[countloop].value = stdCodeList[x];
                                                if (datacode[countloop] != undefined)
                                                    datacode[countloop].value = columnsIn[key];
                                                if (datagrade[countloop] != undefined)
                                                    datagrade[countloop].value = gradevalue;

                                                if (datastd[countloop] != undefined && datacode[countloop] != undefined)
                                                    countloop = countloop + 1;
                                                //alert(stdCodeList[x] + " " + key + " " + data[x][key]);
                                                /*}*/
                                            }
                                        /*}*/
                                    }
                                }
                                //console.log("countloop" + countloop);
                                tranheader[0].textContent = countloop + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %>";
                                //if (countloop > 0) {
                                for (var key = 0; key < columnsIn.length; key++) {
                                    if (columnsIn[key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && columnsIn[key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>" && columnsIn[key] != undefined && columnsIn[key] != "") {
                                            plancount = plancount + 1;
                                        plancodelist = plancodelist + columnsIn[key] + "~"; //alert(key); // here is your column name you are looking for
                                        }
                                    }
                                    plancodelist = plancodelist.slice(0, -1);

                                    planheader[0].textContent = plancount + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>";
                                    stdheader[0].textContent = countStd + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>";

                                    planheader[1].textContent = 0;
                                    planheader[2].textContent = 0;

                                    var ddlYear = document.getElementById('<%=DropDownList1.ClientID%>').value;
                                    var ddlvalue = document.getElementById('<%=DropDownList2.ClientID%>').value;
                                    var ddlSubLevel = document.getElementById('<%=ddlSubLevel.ClientID%>').value;
                                    var planId = document.getElementById('<%=ddlPlan.ClientID%>').value;
                                    console.log("checkplan.ashx");
                                $.get("/import/checkplan.ashx?term=" + ddlvalue + "&id=" + plancodelist + "&idlv=" + ddlSubLevel + "&planId=" + planId + "&numberYear=" + ddlYear, function (Result) {
                                        var pln0 = 0;
                                        var pln1 = 0;
                                        $.each(Result, function (index) {
                                            if (plnhidden1[index] != undefined)
                                                plnhidden1[index].classList.remove('hidden');
                                            if (plnhidden2[index] != undefined)
                                                plnhidden2[index].classList.remove('hidden');
                                            if (plnhidden3[index] != undefined)
                                                plnhidden3[index].classList.remove('hidden');
                                            if (plnhidden4[index] != undefined)
                                                plnhidden4[index].classList.remove('hidden');
                                            if (plancode[index] != undefined)
                                                plancode[index].value = Result[index].planCode;
                                            if (planname[index] != undefined)
                                                planname[index].value = Result[index].planName;
                                            if (planstat[index] != undefined)
                                                planstat[index].value = Result[index].status;

                                            if (Result[index].status == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132299") %>")
                                                pln1 = pln1 + 1;
                                            else pln0 = pln0 + 1;

                                            planheader[1].textContent = pln1;
                                            planheader[2].textContent = pln0;

                                            if (pln0 != 0 && countloop > 0) {
                                                //console.log("Button 1");
                                                if (savebutton1[0] != undefined)
                                                    savebutton1[0].classList.add('hidden');
                                                if (savebutton2[0] != undefined)
                                                    savebutton2[0].classList.remove('hidden');
                                            }
                                        });
                                    });

                                    var std0 = 0;
                                    var std1 = 0;
                                    var idlist = "";
                                    for (var y = 0; y < countStd; y++) {
                                        idlist = idlist + data[y].<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> + "~";
                                    }

                                    idlist = idlist.slice(0, -1);
                                    console.log("checkStudent.ashx");
                                    $.get("/import/checkStudent.ashx?id=" + idlist, function (Result) {
                                        var std0 = 0;
                                        var std1 = 0;
                                       
                                        $.each(Result, function (index) {
                                            //console.log(Result[index]);
                                            if (stdhidden1[index] != undefined)
                                                stdhidden1[index].classList.remove('hidden');
                                            if (stdhidden2[index] != undefined)
                                                stdhidden2[index].classList.remove('hidden');
                                            if (stdhidden3[index] != undefined)
                                                stdhidden3[index].classList.remove('hidden');
                                            if (stdhidden4[index] != undefined)
                                                stdhidden4[index].classList.remove('hidden');

                                            if (stdCode[index] != undefined)
                                                stdCode[index].value = Result[index].stdCode;
                                            if (stdname[index] != undefined)
                                                stdname[index].value = Result[index].stdName;

                                            if (stdStat[index] != undefined)
                                                stdStat[index].value = Result[index].status;

                                            if (stdsid[index] != undefined)
                                                stdsid[index].value = Result[index].stdSid;

                                            if (Result[index].status == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132299") %>" && stdCode[index] != undefined) {
                                                std1 = std1 + 1;
                                                studentCode.push(Result[index].stdCode);
                                            }
                                            else std0 = std0 + 1;

                                            //console.log("stdheader[1].textContent" + stdheader[1].textContent);
                                            //console.log("stdheader[2].textContent" + stdheader[2].textContent);
                                            //console.log("std1" + std1);
                                            //console.log("std0" + std0);

                                            stdheader[1].textContent = std1;
                                            stdheader[2].textContent = std0;

                                            if (std0 != 0 && countloop  > 0) {
                                                console.log("Button 2");
                                                if (savebutton1[0] != undefined)
                                                    savebutton1[0].classList.add('hidden');

                                                if (savebutton2[0] != undefined)
                                                    savebutton2[0].classList.remove('hidden');
                                            }
                                        });
                                        //console.log("Student" + studentCode);
                                        BindOtherActivitiesData(workbook, studentCode);
                                    });

                                    //console.log(studentCode);
                                //}
                                //if (loaddata[0] != undefined)
                                //    loaddata[0].classList.remove('hidden');
                                //if (loadlogo[0] != undefined)
                                //    loadlogo[0].classList.add('hidden');
                            }, 9000);


                            //alert(plancodelist);

                        }


                      
                    })


                };

                //if (isNewSheetExist == false) {
                //    if (loaddata[0] != undefined)
                //        loaddata[0].classList.remove('hidden');
                //    if (loadlogo[0] != undefined)
                //        loadlogo[0].classList.add('hidden');
                //}


                reader.onerror = function (ex) {
                    console.log(ex);
                };

                reader.readAsBinaryString(file);
            };
        };

        //function finish() {
        //    waiting--;
        //    if (waiting == 0) {
        //        //do your Job intended to be done after forEach is completed
        //    }
        //}

        function BindOtherActivitiesData(workbook, studentCode) {

            var uploadstatus = document.getElementsByClassName("uploadstatus");

            //var planheader = document.getElementsByClassName("planheader");
            //var stdheader = document.getElementsByClassName("stdheader");
            //var tranheader = document.getElementsByClassName("tranheader");
            var desirableAttributeCount = document.getElementsByClassName("DesirableAttributeCount");
            var readWriteCount = document.getElementsByClassName("ReadWriteCount");
            var samattanaCount = document.getElementsByClassName("SamattanaCount");

            //var plancode = document.getElementsByClassName("plancode");
            //var planname = document.getElementsByClassName("planname");
            //var planstat = document.getElementsByClassName("planstat");

            //var stdCode = document.getElementsByClassName("stdCode");
            //var stdname = document.getElementsByClassName("stdname");
            //var stdStat = document.getElementsByClassName("stdStat");
            //var stdsid = document.getElementsByClassName("stdsid");

            //var stdhidden1 = document.getElementsByClassName("stdhidden1");
            //var stdhidden2 = document.getElementsByClassName("stdhidden2");
            //var stdhidden3 = document.getElementsByClassName("stdhidden3");
            //var stdhidden4 = document.getElementsByClassName("stdhidden4");

            //var plnhidden1 = document.getElementsByClassName("plnhidden1");
            //var plnhidden2 = document.getElementsByClassName("plnhidden2");
            //var plnhidden3 = document.getElementsByClassName("plnhidden3");
            //var plnhidden4 = document.getElementsByClassName("plnhidden4");

            //var datahidden1 = document.getElementsByClassName("datahidden1");
            //var datahidden2 = document.getElementsByClassName("datahidden2");
            //var datahidden3 = document.getElementsByClassName("datahidden3");
            //var datahidden4 = document.getElementsByClassName("datahidden4");

            //var datastd = document.getElementsByClassName("datastd");
            //var datacode = document.getElementsByClassName("datacode");
            //var datagrade = document.getElementsByClassName("datagrade");
            //var pagedgd = document.getElementsByClassName("pagedgd");

            var desirableDatahidden1 = document.getElementsByClassName("DesirableDatahidden1");
            var desirableDatahidden2 = document.getElementsByClassName("DesirableDatahidden2");
            var desirableDatahidden3 = document.getElementsByClassName("DesirableDatahidden3");

            var desirableDatastd = document.getElementsByClassName("DesirableDatastd");
            var desirableDatagrade = document.getElementsByClassName("DesirableDatagrade");

            var readWriteDatahidden1 = document.getElementsByClassName("ReadWriteDatahidden1");
            var readWriteDatahidden2 = document.getElementsByClassName("ReadWriteDatahidden2");
            var readWriteDatahidden3 = document.getElementsByClassName("ReadWriteDatahidden3");

            var readWriteDatastd = document.getElementsByClassName("ReadWriteDatastd");
            var readWriteDatagrade = document.getElementsByClassName("ReadWriteDatagrade");

            var samattanaDatahidden1 = document.getElementsByClassName("SamattanaDatahidden1");
            var samattanaDatahidden2 = document.getElementsByClassName("SamattanaDatahidden2");
            var samattanaDatahidden3 = document.getElementsByClassName("SamattanaDatahidden3");

            var samattanaDatastd = document.getElementsByClassName("SamattanaDatastd");
            var samattanaDatagrade = document.getElementsByClassName("SamattanaDatagrade");


            var loaddata = document.getElementsByClassName("loaddata");
            var loadlogo = document.getElementsByClassName("loadlogo");
            var savebutton1 = document.getElementsByClassName("savebutton1");
            var savebutton2 = document.getElementsByClassName("savebutton2");
            var disabletarget = document.getElementsByClassName("disabletarget");
            console.log("doAsynchronousFunction");
            //asynchronousjob with entry
            //callback();

            workbook.SheetNames.forEach(function (sheetName) {
                // Here is your object

                var XL_row_object = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                var json_object = JSON.stringify(XL_row_object);
                //console.log(JSON.parse(json_object));
                var data = JSON.parse(json_object);
                //var yyy = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %> " + data.length;

                //stdheader[1].textContent = 0;
                //stdheader[2].textContent = 0;

               


                //Desirable Attribute
                if (sheetName == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206141") %>") {

                    console.log("Desirable Attribute");
                    var countStd = 0;
                    for (var x = 0; x < 700; x++) {
                        if (data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != null && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != undefined && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != "")
                            countStd++;
                    }

                    if (uploadstatus[0] != undefined)
                        uploadstatus[0].classList.remove('hidden');
                    if (disabletarget[0] != undefined)
                        disabletarget[0].classList.add('disable');
                    if (disabletarget[1] != undefined)
                        disabletarget[1].classList.add('disable');

                    var columnsIn = data[0];
                    var plancount = 0;

                    var stdCodeList = [];
                    for (var x = 0; x < countStd; x++) {
                        if (data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != null && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != undefined && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != "")
                            stdCodeList[x] = data[x].<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>;
                    }

                    var countloop2 = 0;
                    for (var key in columnsIn) {
                        if (key != undefined) {
                            for (var x = 0; x < countStd; x++) {
                                if (key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>" && key != "") {
                                    if (data[x][key] != null) {
                                        countloop2 = countloop2 + 1;
                                    }
                                }
                            }
                        }
                    }

                    var plancount2 = 0;
                    for (var key in columnsIn) {
                        if (key != undefined) {
                            if (key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>" && key != "") {
                                plancount2 = plancount2 + 1;
                            }
                        }
                    }

                    //console.log(countloop2);

                    //if ($('#TextBox13').val() == "0" && studentCode.length > 0) {

                       // $('#txtDesirableAttribute').val(studentCode.length);
                       // $('#txtReadWrite').val(studentCode.length);
                       // $('#txtSamattana').val(studentCode.length);
                       // $('#TextBox11').val(plancount2);
                       // $('#TextBox11').trigger('change');
                    //}

                    //$('#txtDesirableAttribute').val(countloop2);
                    //$('#txtDesirableAttribute').trigger('change');
                    //stdCode = document.getElementsByClassName("stdCode");
                    // console.log(stdCode[0]);
                    // console.log($(stdCode[0]).val())
                    // console.log(stdCode[0].value);
                    //console.log(studentCode);
                    //console.log(studentCode[0]);
                    setTimeout(function () {
                        var countloop = 0;
                        //console.log("columnsIn" + columnsIn.length);
                        for (var key in columnsIn) {
                            //console.log("countStd" + countStd);
                            //console.log("key" + key);
                            if (key != undefined) {
                                for (var x = 0; x < countStd; x++) {
                                    if (key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>" && key != "" && key != undefined) {
                                        //console.log("data[x][key]" + data[x][key]);
                                        //console.log("studentCode[countloop]" + studentCode[countloop]);
                                        //console.log("stdCodeList[x]" + stdCodeList[x]);
                                        if (data[x][key] != null && data[x][key] != undefined && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503054") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132300") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103025") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103024") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206541") %>") {


                                            if (studentCode[countloop] != undefined && studentCode[countloop] == stdCodeList[x]) {

                                                if (desirableDatahidden1[countloop] != undefined)
                                                    desirableDatahidden1[countloop].classList.remove('hidden');
                                                if (desirableDatahidden2[countloop] != undefined)
                                                    desirableDatahidden2[countloop].classList.remove('hidden');
                                                if (desirableDatahidden3[countloop] != undefined) {
                                                    desirableDatahidden3[countloop].classList.remove('hidden');
                                                    //console.log("desirableDatahidden3[countloop]");
                                                }

console.log("desirableDatastd[countloop]" + desirableDatastd[countloop]);

                                                if (desirableDatastd[countloop] != undefined)
                                                    desirableDatastd[countloop].value = stdCodeList[x];

                                                if (desirableDatagrade[countloop] != undefined)
                                                    desirableDatagrade[countloop].value = data[x][key];

                                                countloop = countloop + 1;
                                                //alert(stdCodeList[x] + " " + key + " " + data[x][key]);
                                            }

                                            //console.log("stdCodeList[x]" + stdCodeList[x]);
                                            //if (desirableDatastd[countloop] != undefined)
                                            //    console.log("desirableDatastd[countloop].value" + desirableDatastd[countloop].value);
                                        }
                                    }
                                }
                            }
                        }
                        //console.log(countloop);
                        //console.log(desirableDatastd);
                        //console.log(desirableDatagrade);
                        desirableAttributeCount[0].textContent = countloop + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %>";

                        if (countloop != 0) {
                            if (savebutton2[0] != undefined)
                                savebutton2[0].classList.add('hidden');

                            if (savebutton1[0] != undefined)
                                savebutton1[0].classList.remove('hidden');
                        }

                        //if (loaddata[0] != undefined)
                        //    loaddata[0].classList.remove('hidden');
                        //if (loadlogo[0] != undefined)
                        //    loadlogo[0].classList.add('hidden');
                    }, 1000);


                    //alert(plancodelist);

                }

                //ReadWrite Attribute
                if (sheetName == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206086") %>") {
                    console.log("Desirable Attribute");
                    var countStd = 0;
                    for (var x = 0; x < 700; x++) {
                        if (data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != null && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != undefined && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != "")
                            countStd++;
                    }

                    if (uploadstatus[0] != undefined)
                        uploadstatus[0].classList.remove('hidden');
                    if (disabletarget[0] != undefined)
                        disabletarget[0].classList.add('disable');
                    if (disabletarget[1] != undefined)
                        disabletarget[1].classList.add('disable');

                    var columnsIn = data[0];
                    var plancount = 0;

                    var stdCodeList = [];
                    for (var x = 0; x < countStd; x++) {
                        if (data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != null && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != undefined && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != "")
                            stdCodeList[x] = data[x].<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>;
                    }

                    var countloop2 = 0;
                    for (var key in columnsIn) {
                        if (key != undefined) {
                            for (var x = 0; x < countStd; x++) {
                                if (key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>" && key != "") {
                                    if (data[x][key] != null) {
                                        countloop2 = countloop2 + 1;
                                    }
                                }
                            }
                        }
                    }

                    var plancount2 = 0;
                    for (var key in columnsIn) {
                        if (key != undefined) {
                            if (key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>" && key != "") {
                                plancount2 = plancount2 + 1;
                            }
                        }
                    }

                    console.log(countloop2);


                    //$('#txtDesirableAttribute').val(countloop2);
                    //$('#txtDesirableAttribute').trigger('change');


                    setTimeout(function () {
                        var countloop = 0;
                        console.log("columnsIn" + columnsIn.length);
                        for (var key in columnsIn) {
                            //console.log("countStd" + countStd);
                            //console.log("key" + key);
                            if (key != undefined) {
                                for (var x = 0; x < countStd; x++) {
                                    if (key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>" && key != "" && key != undefined) {
                                        //console.log("data[x][key]" + data[x][key]);
                                        if (data[x][key] != null && data[x][key] != undefined && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503054") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132300") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103025") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103024") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206541") %>") {

                                            if (studentCode[countloop] != undefined && studentCode[countloop] == stdCodeList[x]) {
                                                if (readWriteDatahidden1[countloop] != undefined)
                                                    readWriteDatahidden1[countloop].classList.remove('hidden');
                                                if (readWriteDatahidden2[countloop] != undefined)
                                                    readWriteDatahidden2[countloop].classList.remove('hidden');
                                                if (readWriteDatahidden3[countloop] != undefined) {
                                                    readWriteDatahidden3[countloop].classList.remove('hidden');
                                                    //console.log("desirableDatahidden3[countloop]");
                                                }

                                                if (readWriteDatastd[countloop] != undefined)
                                                    readWriteDatastd[countloop].value = stdCodeList[x];

                                                if (readWriteDatagrade[countloop] != undefined)
                                                    readWriteDatagrade[countloop].value = data[x][key];

                                                countloop = countloop + 1;
                                            }
                                            //alert(stdCodeList[x] + " " + key + " " + data[x][key]);

                                            //console.log("stdCodeList[x]" + stdCodeList[x]);
                                            //if (readWriteDatastd[countloop] != undefined)
                                            //    console.log("desirableDatastd[countloop].value" + readWriteDatastd[countloop].value);
                                        }
                                    }
                                }
                            }
                        }
                        //console.log(countloop);
                        //console.log(desirableDatastd);
                        //console.log(desirableDatagrade);
                        readWriteCount[0].textContent = countloop + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %>";

                        if (countloop != 0) {
                            if (savebutton2[0] != undefined)
                                savebutton2[0].classList.add('hidden');

                            if (savebutton1[0] != undefined)
                                savebutton1[0].classList.remove('hidden');
                        }
                        //if (loaddata[0] != undefined)
                        //    loaddata[0].classList.remove('hidden');
                        //if (loadlogo[0] != undefined)
                        //    loadlogo[0].classList.add('hidden');
                    }, 1000);


                    //alert(plancodelist);

                }

                //Samattana Attribute
                if (sheetName == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206070") %>") {
                    isNewSheetExist = true;
                    console.log("Desirable Attribute");
                    var countStd = 0;
                    for (var x = 0; x < 700; x++) {
                        if (data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != null && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != undefined && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != "")
                            countStd++;
                    }

                    if (uploadstatus[0] != undefined)
                        uploadstatus[0].classList.remove('hidden');
                    if (disabletarget[0] != undefined)
                        disabletarget[0].classList.add('disable');
                    if (disabletarget[1] != undefined)
                        disabletarget[1].classList.add('disable');

                    var columnsIn = data[0];
                    var plancount = 0;

                    var stdCodeList = [];
                    for (var x = 0; x < countStd; x++) {
                        if (data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != null && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != undefined && data[x]['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>'] != "")
                            stdCodeList[x] = data[x].<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>;
                    }

                    var countloop2 = 0;
                    for (var key in columnsIn) {
                        if (key != undefined) {
                            for (var x = 0; x < countStd; x++) {
                                if (key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>" && key != "") {
                                    if (data[x][key] != null) {
                                        countloop2 = countloop2 + 1;
                                    }
                                }
                            }
                        }
                    }

                    var plancount2 = 0;
                    for (var key in columnsIn) {
                        if (key != undefined) {
                            if (key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>" && key != "") {
                                plancount2 = plancount2 + 1;
                            }
                        }
                    }

                    console.log(countloop2);

                    setTimeout(function () {
                        var countloop = 0;
                        //console.log("columnsIn" + columnsIn.length);
                        for (var key in columnsIn) {
                            //console.log("countStd" + countStd);
                            //console.log("key" + key);
                            if (key != undefined) {
                                for (var x = 0; x < countStd; x++) {
                                    if (key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" && key != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>" && key != "" && key != undefined) {
                                        //console.log("data[x][key]" + data[x][key]);
                                        if (data[x][key] != null && data[x][key] != undefined && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503054") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132300") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103025") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103024") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %>" && data[x][key] != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206541") %>") {

                                            if (studentCode[countloop] != undefined && studentCode[countloop] == stdCodeList[x]) {
                                                if (samattanaDatahidden1[countloop] != undefined)
                                                    samattanaDatahidden1[countloop].classList.remove('hidden');
                                                if (samattanaDatahidden2[countloop] != undefined)
                                                    samattanaDatahidden2[countloop].classList.remove('hidden');
                                                if (samattanaDatahidden3[countloop] != undefined) {
                                                    samattanaDatahidden3[countloop].classList.remove('hidden');
                                                    //console.log("desirableDatahidden3[countloop]");
                                                }

                                                if (samattanaDatastd[countloop] != undefined)
                                                    samattanaDatastd[countloop].value = stdCodeList[x];

                                                if (samattanaDatagrade[countloop] != undefined)
                                                    samattanaDatagrade[countloop].value = data[x][key];

                                                countloop = countloop + 1;
                                            }
                                            //alert(stdCodeList[x] + " " + key + " " + data[x][key]);

                                            //console.log("stdCodeList[x]" + stdCodeList[x]);
                                            //if (samattanaDatastd[countloop] != undefined)
                                            //    console.log("desirableDatastd[countloop].value" + samattanaDatastd[countloop].value);
                                        }
                                    }
                                }
                            }
                        }

                        samattanaCount[0].textContent = countloop + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %>";

                        if (countloop != 0) {
                            if (savebutton2[0] != undefined)
                                savebutton2[0].classList.add('hidden');

                            if (savebutton1[0] != undefined)
                                savebutton1[0].classList.remove('hidden');
                        }
                        if (loaddata[0] != undefined)
                            loaddata[0].classList.remove('hidden');
                        if (loadlogo[0] != undefined)
                            loadlogo[0].classList.add('hidden');
                    }, 1000);
                }
            })
        }

        function handleFileSelect(evt) {

            var files = evt.target.files; // FileList object
            var xl2json = new ExcelToJSON();
            xl2json.parseExcel(files[0]);
        }



    </script>
    <script type="text/javascript" language="javascript">

        function ShowScoreAlreadyEnteredCourseCode() {
            bootbox.alert({
                message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02341") %></h3><br />' + getUrlParameter("coursecode").split(",").join("<br />") ,
                callback: function (result) {
                    showload();
                    window.location.href = "importExcel.aspx";
                },
                backdrop: true,
            });
        }

        function getUrlParameter(sParam) {
            var sPageURL = decodeURIComponent(window.location.search.substring(1)),
                sURLVariables = sPageURL.split('&'),
                sParameterName,
                i;

            for (i = 0; i < sURLVariables.length; i++) {
                sParameterName = sURLVariables[i].split('=');

                if (sParameterName[0] === sParam) {
                    return sParameterName[1] === undefined ? true : sParameterName[1];
                }
            }
        }

        function start() {
            var mode = document.getElementsByClassName("mode");
            var orange = document.getElementsByClassName("orangebutton");
            var orange2 = document.getElementsByClassName("orangebutton2");
            var green = document.getElementsByClassName("greenbutton");
            var greybutton = document.getElementsByClassName("greybutton");
            var havedata = document.getElementsByClassName("havedata");
            var time = document.getElementsByClassName("time");
            var timebutton = document.getElementsByClassName("timebutton");

            if (getUrlParameter("coursecode") != "" && getUrlParameter("coursecode") != undefined && getUrlParameter("coursecode") != "undefined")
            {
                ShowScoreAlreadyEnteredCourseCode();
            }

        }

        function gradeON() {
            var mode = document.getElementsByClassName("gradetab");
            mode[0].classList.remove('hidden');
            var btn = document.getElementsByClassName("buttonplan");
            btn[0].classList.add('hidden');
            btn[1].classList.remove('hidden');
        }

        function gradeOFF() {
            var mode = document.getElementsByClassName("gradetab");

            mode[0].classList.add('hidden');
            var btn = document.getElementsByClassName("buttonplan");
            btn[0].classList.remove('hidden');
            btn[1].classList.add('hidden');
        }

        function ddlyear() {
            var ddl2 = document.getElementsByClassName("ddl2");
            var ddl1 = document.getElementsByClassName("ddl1");

            var ddlvalue = document.getElementById('<%=DropDownList1.ClientID%>').value;
            var ddlvalue2 = document.getElementById('<%=DropDownList2.ClientID%>').value;

            var btn = document.getElementsByClassName("uploadbutton");
            if (ddlvalue != 0 && ddlvalue2 != 0)
                btn[0].classList.remove('hidden');


        }



        function ddlChange() {


            var ddlvalue = document.getElementById('<%=DropDownList1.ClientID%>').value;
            var ddlvalue2 = document.getElementById('<%=DropDownList2.ClientID%>').value;
            var ddlSubLevel = document.getElementById('<%=ddlSubLevel.ClientID%>').value;
            var ddlPlan = document.getElementById('<%=ddlPlan.ClientID%>').value;

            var btn = document.getElementsByClassName("uploadbutton");
            if (ddlvalue != 0 && ddlvalue2 != 0 && ddlSubLevel != 0 && ddlPlan != 0)
                btn[0].classList.remove('hidden');
        }
        function ddlSubLevelChange() {

            $.get("/import/GetPlan.ashx?term=" + ddlvalue + "&id=" + plancodelist + "&idlv=" + ddlSubLevel, function (Result) {
                var pln0 = 0;
                var pln1 = 0;
                $.each(Result, function (index) {
                    if (plnhidden1[index] != undefined)
                        plnhidden1[index].classList.remove('hidden');
                    if (plnhidden2[index] != undefined)
                        plnhidden2[index].classList.remove('hidden');
                    if (plnhidden3[index] != undefined)
                        plnhidden3[index].classList.remove('hidden');
                    if (plnhidden4[index] != undefined)
                        plnhidden4[index].classList.remove('hidden');
                    if (plancode[index] != undefined)
                        plancode[index].value = Result[index].planCode;
                    if (planname[index] != undefined)
                        planname[index].value = Result[index].planName;
                    if (planstat[index] != undefined)
                        planstat[index].value = Result[index].status;

                    if (Result[index].status == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132299") %>")
                        pln1 = pln1 + 1;
                    else pln0 = pln0 + 1;

                    planheader[1].textContent = pln1;
                    planheader[2].textContent = pln0;

                    if (pln0 != 0) {
                        console.log("Button 3");
                        if (savebutton1[0] != undefined)
                            savebutton1[0].classList.add('hidden');
                        if (savebutton2[0] != undefined)
                            savebutton2[0].classList.remove('hidden');
                    }
                });
            });

        }
        function stdON(index) {
            var mode = document.getElementsByClassName("stdtab");
            mode[index].classList.remove('hidden');
            if (index == 0) {
                var btn = document.getElementsByClassName("buttonstudent");
                btn[0].classList.add('hidden');
                btn[1].classList.remove('hidden');
            }
            if (index == 1) {
                var btn = document.getElementsByClassName("buttonlist");
                btn[0].classList.add('hidden');
                btn[1].classList.remove('hidden');
            }
            if (index == 2) {
                var btn = document.getElementsByClassName("btnDesirableAttriutes");
                btn[0].classList.add('hidden');
                btn[1].classList.remove('hidden');
            }

            if (index == 3) {
                var btn = document.getElementsByClassName("btnReadWrite");
                btn[0].classList.add('hidden');
                btn[1].classList.remove('hidden');
            }
            if (index == 3) {
                var btn = document.getElementsByClassName("btnSamattana");
                btn[0].classList.add('hidden');
                btn[1].classList.remove('hidden');
            }


        }

        function stdOFF(index) {
            var mode = document.getElementsByClassName("stdtab");
            mode[index].classList.add('hidden');
            if (index == 0) {
                var btn = document.getElementsByClassName("buttonstudent");
                if (btn[0] != undefined)
                    btn[0].classList.remove('hidden');
                if (btn[1] != undefined)
                    btn[1].classList.add('hidden');
            }
            if (index == 1) {
                var btn = document.getElementsByClassName("buttonlist");
                if (btn[0] != undefined)
                    btn[0].classList.remove('hidden');
                if (btn[1] != undefined)
                    btn[1].classList.add('hidden');
            }
            if (index == 2) {
                var btn = document.getElementsByClassName("btnDesirableAttriutes");
                if (btn[0] != undefined)
                    btn[0].classList.remove('hidden');
                if (btn[1] != undefined)
                    btn[1].classList.add('hidden');
            }
            if (index == 3) {
                var btn = document.getElementsByClassName("btnReadWrite");
                if (btn[0] != undefined)
                    btn[0].classList.remove('hidden');
                if (btn[1] != undefined)
                    btn[1].classList.add('hidden');
            }
            if (index == 3) {
                var btn = document.getElementsByClassName("btnSamattana");
                if (btn[0] != undefined)
                    btn[0].classList.remove('hidden');
                if (btn[1] != undefined)
                    btn[1].classList.add('hidden');
            }
        }

        function dgdSetup() {
            var pagedgd = document.getElementsByClassName("pagedgd");

            alert("sd");
        }

        function bootbox2() {
            bootbox.alert({
                message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206133") %></h3>',
                backdrop: true,
            });
        }

        
        

        function showload() {
            var loadstatus = document.getElementsByClassName("loadstatus");
            if (loadstatus[0] != undefined)
                loadstatus[0].classList.remove('hidden');
        }
        window.onload = start;
    </script>


    <div class="full-card box-content userlist-container col-xs-12" style="background-color: white;">

        <asp:HiddenField ID="hdfsid" runat="server" />
        <div id="loading" class="loadstatus hidden"></div>
        <div class="col-xs-12 lefttext">
            <h2 class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206102") %>
            </h2>
        </div>
        <div class="col-xs-12">
            <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206103") %> : <a target="_blank" href="/import/downloadList.aspx" class="" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206104") %></a></h3>
        </div>



        <div class="col-xs-12">
            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                    <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206105") %></h3>
                </div>
                <div class="col-xs-3">
                    <asp:DropDownList ID="DropDownList1" runat="server" onchange="ddlChange()" CssClass="ddl1 form-control disabletarget" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div class="col-xs-12">

                    <div class="col-xs-12 pad0">
                        <div class="col-xs-3 pad0">
                            <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206106") %></h3>
                        </div>
                        <div class="col-xs-3">
                            <asp:DropDownList ID="DropDownList2" runat="server" onchange="ddlChange()" CssClass="ddl2 form-control disabletarget">
                                <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %>" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-xs-12 hidden">
                    <asp:TextBox ID="Textbox4" runat="server" CssClass="ddlselect" Width="80%"> </asp:TextBox>
                    <asp:TextBox ID="Textbox8" runat="server" CssClass="" Width="80%"> </asp:TextBox>
                </div>


            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>


        <div class="col-xs-12">
            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                    <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206107") %></h3>
                </div>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlSubLevel" runat="server" CssClass="ddl4 form-control disabletarget" AutoPostBack="true" OnSelectedIndexChanged="ddlSubLevel_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="col-xs-12">
            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                    <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206108") %></h3>
                </div>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlPlan" runat="server" CssClass="ddl4 form-control disabletarget" onchange="ddlChange()">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="col-xs-12 uploadbutton hidden">
            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                    <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101283") %></h3>
                </div>
                <div class="col-xs-3">
                    <form enctype="multipart/form-data">
                        <input id="upload" type="file" name="files[]">
                    </form>
                </div>
            </div>
        </div>



        <script>
            document.getElementById('upload').addEventListener('change', handleFileSelect, false);

        </script>





        <div class="hidden uploadstatus">
            <div class="col-xs-12">
                <div class="col-xs-12 pad0">
                    <div class="col-xs-12 pad0">
                        <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206111") %></h3>
                    </div>

                </div>
                <div class="col-xs-12 pad0">
                    <h3 class="lh5" style="margin-left: 30px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132301") %> <a target="_blank" href="/bp1/courseregister.aspx" class="" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201047") %></a></h3>
                </div>
                <div class="col-xs-12 pad0">
                    <h3 class="lh5" style="margin-left: 60px;">- <u><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206113") %></u> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206114") %></h3>
                </div>
                <div class="col-xs-12 pad0">
                    <h3 class="col-xs-8 pad0 lh5" style="margin-left: 60px;">- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206115") %> <u><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206116") %></u> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206117") %></h3>
                    <div class="col-xs-1">
                        <asp:Button ID="Button2" class="btn btn-danger global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206118") %>" />
                    </div>
                    <div class="col-xs-2 savebutton1" onclick="showload()">
                        <asp:Button ID="Button1" class="btn btn-success global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" ValidationGroup="add" />
                    </div>
                    <div class="col-xs-2 savebutton2 hidden">
                        <div class="btn btn-success" onclick="bootbox2()">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xs-12">
                <hr>
            </div>

            <div class="col-xs-12 loadlogo loadlogo2">
                <div class="col-xs-5">
                </div>
                <div class="col-xs-5">
                    <div class="lds-facebook">
                        <div></div>
                        <div></div>
                        <div></div>
                    </div>
                </div>
            </div>

            <div class="col-xs-12 hidden loaddata">
                <div class="col-xs-12" style="margin: 10px;">
                    <div class="col-xs-3 pad0">
                        <input type="button" id="gradetab1" class='btn btn-info search-btn buttonplan' value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206119") %>" style="width: 190px;" onclick="gradeON()" />
                        <input type="button" id="gradetab2" class='btn btn-info search-btn hidden buttonplan' value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206120") %>" style="width: 190px;" onclick="gradeOFF()" />
                    </div>
                    <div class="col-xs-4 pad0">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206128") %>
                        <asp:Label ID="planidtxt" runat="server" CssClass="planheader"> </asp:Label>
                    </div>
                    <div class="col-xs-3 pad0">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206129") %>
                        <asp:Label ID="yeartxt" runat="server" CssClass="planheader"> </asp:Label>
                    </div>
                    <div class="col-xs-2 pad0">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206130") %>
                        <asp:Label ID="termtxt" runat="server" CssClass="planheader"> </asp:Label>
                    </div>
                </div>



                <div class="col-xs-12 pad0">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>

                            <div class="hidden">
                                <asp:TextBox ID="TextBox11" runat="server" Width="80%" Height="42.5px" CssClass="pagedgd" OnTextChanged="dgdSetup" AutoPostBack="true" ClientIDMode="Static"></asp:TextBox>
                                <asp:TextBox ID="TextBox12" runat="server" Width="80%" Height="42.5px" CssClass="pagedgd" AutoPostBack="true" ClientIDMode="Static"></asp:TextBox>
                                <asp:TextBox ID="TextBox13" runat="server" Width="80%" Height="42.5px" CssClass="pagedgd" AutoPostBack="true" ClientIDMode="Static"></asp:TextBox>
                                <asp:TextBox ID="txtDesirableAttribute" runat="server" Width="80%" Height="42.5px" CssClass="pagedgd" AutoPostBack="true" ClientIDMode="Static"></asp:TextBox>
                                <asp:TextBox ID="txtReadWrite" runat="server" Width="80%" Height="42.5px" CssClass="pagedgd" AutoPostBack="true" ClientIDMode="Static"></asp:TextBox>
                                <asp:TextBox ID="txtSamattana" runat="server" Width="80%" Height="42.5px" CssClass="pagedgd" AutoPostBack="true" ClientIDMode="Static"></asp:TextBox>
                            </div>

                            <div class="col-xs-12 gradetab hidden">


                                <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                                    GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen">

                                    <Columns>


                                        <asp:TemplateField ItemStyle-CssClass="centertext plnhidden1 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="txtnum" runat="server" Width="40px" CssClass="centertext" Text='<%# Eval("number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext plnhidden2 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %>">
                                            <HeaderStyle Width="25%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="planCodetxt" TabIndex="2" runat="server" Width="80%" Height="42.5px" CssClass="plancode labelbox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext plnhidden3 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %>">
                                            <HeaderStyle Width="25%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="planNametxt" TabIndex="3" runat="server" Width="80%" Height="42.5px" CssClass="planname labelbox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext plnhidden4 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>">
                                            <HeaderStyle Width="10%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="planStatustxt" TabIndex="4" runat="server" Width="80%" Height="42.5px" CssClass="planstat labelbox centertext"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />

                                </asp:GridView>
                            </div>

                            <div class="col-xs-12" style="margin: 10px;">
                                <div class="col-xs-3 pad0">
                                    <input type="button" class='btn btn-info search-btn buttonstudent' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206121") %>" style="width: 190px;" onclick="stdON(0)" />
                                    <input type="button" class='btn btn-info search-btn buttonstudent hidden' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206122") %>" style="width: 190px;" onclick="stdOFF(0)" />
                                </div>
                                <div class="col-xs-4 pad0">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132302") %>
                                    <asp:Label ID="Textbox1" runat="server" CssClass="stdheader"> </asp:Label>
                                </div>
                                <div class="col-xs-3 pad0">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206131") %>
                                    <asp:Label ID="Textbox2" runat="server" CssClass="stdheader"> </asp:Label>
                                </div>
                                <div class="col-xs-2 pad0">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206130") %>
                                    <asp:Label ID="Textbox3" runat="server" CssClass="stdheader"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 stdtab hidden">


                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                                    GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen">

                                    <Columns>


                                        <asp:TemplateField ItemStyle-CssClass="centertext stdhidden1 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Width="40px" CssClass="centertext" Text='<%# Eval("number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext stdhidden2 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>">
                                            <HeaderStyle Width="25%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="stdCodetxt" TabIndex="2" runat="server" Width="80%" Height="42.5px" CssClass="stdCode labelbox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext stdhidden3 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301008") %>">
                                            <HeaderStyle Width="25%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox5" TabIndex="3" runat="server" Width="80%" Height="42.5px" CssClass="stdname labelbox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext stdhidden4 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>">
                                            <HeaderStyle Width="10%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox6" TabIndex="4" runat="server" Width="80%" Height="42.5px" CssClass="stdStat labelbox centertext"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext hidden" HeaderStyle-CssClass="hidden" HeaderText="sid">
                                            <HeaderStyle Width="1%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="sidtxt" TabIndex="3" runat="server" Width="80%" Height="42.5px" CssClass="stdsid labelbox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />

                                </asp:GridView>
                            </div>

                            <div class="col-xs-12" style="margin: 10px;">
                                <div class="col-xs-3 pad0">
                                    <input type="button" class='btn btn-info search-btn buttonlist' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206123") %>" style="width: 190px;" onclick="stdON(1)" />
                                    <input type="button" class='btn btn-info search-btn buttonlist hidden' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206124") %>" style="width: 190px;" onclick="stdOFF(1)" />
                                </div>
                                <div class="col-xs-4 pad0">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206132") %>
                                    <asp:Label ID="Textbox7" runat="server" CssClass="tranheader"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 stdtab hidden">


                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                                    GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen">

                                    <Columns>


                                        <asp:TemplateField ItemStyle-CssClass="centertext datahidden1 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Width="40px" CssClass="centertext" Text='<%# Eval("number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext datahidden2 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %>">
                                            <HeaderStyle Width="25%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="codetxt" TabIndex="2" runat="server" Width="80%" Height="42.5px" CssClass="datacode labelbox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext datahidden3 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>">
                                            <HeaderStyle Width="25%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="stdnumtxt" TabIndex="3" runat="server" Width="80%" Height="42.5px" CssClass="datastd labelbox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext datahidden4 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206067") %>">
                                            <HeaderStyle Width="10%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="gradetxt" TabIndex="4" runat="server" Width="80%" Height="42.5px" CssClass="datagrade labelbox centertext"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />

                                </asp:GridView>


                            </div>

                            <!--Desirable attributes-->
                            <div class="col-xs-12" style="margin: 10px;">
                                <div class="col-xs-3 pad0">
                                    <input type="button" class='btn btn-info search-btn btnDesirableAttriutes' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206125") %>" style="width: 190px;" onclick="stdON(2)" />
                                    <input type="button" class='btn btn-info search-btn btnDesirableAttriutes hidden' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206125") %>" style="width: 190px;" onclick="stdOFF(2)" />
                                </div>
                                <div class="col-xs-4 pad0">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206132") %>
                                    <asp:Label ID="DesirableAttributeCount" runat="server" CssClass="DesirableAttributeCount"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 stdtab hidden">
                                <asp:GridView ID="GvDesirableAttribute" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                                    GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen">

                                    <Columns>


                                        <asp:TemplateField ItemStyle-CssClass="centertext DesirableDatahidden1 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Width="40px" CssClass="centertext" Text='<%# Eval("number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext DesirableDatahidden2 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>">
                                            <HeaderStyle Width="25%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="DesirableStdnumtxt" TabIndex="2" runat="server" Width="80%" Height="42.5px" CssClass="DesirableDatastd labelbox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext DesirableDatahidden3 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206067") %>">
                                            <HeaderStyle Width="10%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="DesirableGradetxt" TabIndex="3" runat="server" Width="80%" Height="42.5px" CssClass="DesirableDatagrade labelbox centertext"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />

                                </asp:GridView>
                            </div>

                            <!--ReadWrite-->
                            <div class="col-xs-12" style="margin: 10px;">
                                <div class="col-xs-3 pad0">
                                    <input type="button" class='btn btn-info search-btn btnReadWrite' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206126") %>" style="width: 190px;" onclick="stdON(3)" />
                                    <input type="button" class='btn btn-info search-btn btnReadWrite hidden' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206126") %>" style="width: 190px;" onclick="stdOFF(3)" />
                                </div>
                                <div class="col-xs-4 pad0">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206132") %>
                                    <asp:Label ID="ReadWriteCount" runat="server" CssClass="ReadWriteCount"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 stdtab hidden">
                                <asp:GridView ID="GvReadWrite" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                                    GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen">

                                    <Columns>


                                        <asp:TemplateField ItemStyle-CssClass="centertext ReadWriteDatahidden1 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Width="40px" CssClass="centertext" Text='<%# Eval("number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext ReadWriteDatahidden2 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>">
                                            <HeaderStyle Width="25%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="ReadWriteStdnumtxt" TabIndex="2" runat="server" Width="80%" Height="42.5px" CssClass="ReadWriteDatastd labelbox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext ReadWriteDatahidden3 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206067") %>">
                                            <HeaderStyle Width="10%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="ReadWriteGradetxt" TabIndex="3" runat="server" Width="80%" Height="42.5px" CssClass="ReadWriteDatagrade labelbox centertext"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />

                                </asp:GridView>
                            </div>

                            <!--Samatta-->
                            <div class="col-xs-12" style="margin: 10px;">
                                <div class="col-xs-3 pad0">
                                    <input type="button" class='btn btn-info search-btn btnSamattana' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206127") %>" style="width: 190px;" onclick="stdON(4)" />
                                    <input type="button" class='btn btn-info search-btn btnSamattana hidden' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206127") %>" style="width: 190px;" onclick="stdOFF(4)" />
                                </div>
                                <div class="col-xs-4 pad0">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206132") %>
                                    <asp:Label ID="SamattanaCount" runat="server" CssClass="SamattanaCount"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 stdtab hidden">
                                <asp:GridView ID="GvSamattana" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                                    GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen">

                                    <Columns>


                                        <asp:TemplateField ItemStyle-CssClass="centertext SamattanaDatahidden1 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Width="40px" CssClass="centertext" Text='<%# Eval("number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext SamattanaDatahidden2 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>">
                                            <HeaderStyle Width="25%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="SamattanaStdnumtxt" TabIndex="2" runat="server" Width="80%" Height="42.5px" CssClass="SamattanaDatastd labelbox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="centertext SamattanaDatahidden3 hidden" HeaderStyle-CssClass="centertext whitetxt" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206067") %>">
                                            <HeaderStyle Width="10%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="SamattanaGradetxt" TabIndex="3" runat="server" Width="80%" Height="42.5px" CssClass="SamattanaDatagrade labelbox centertext"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />

                                </asp:GridView>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="TextBox11" EventName="TextChanged" />
                            <asp:PostBackTrigger ControlID="Button1" />
                        </Triggers>
                    </asp:UpdatePanel>



                </div>

            </div>
        </div>
    </div>



    <%--  </div>
        </div>
    </div>--%>
</asp:Content>
