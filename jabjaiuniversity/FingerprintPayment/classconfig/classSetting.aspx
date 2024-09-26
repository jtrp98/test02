<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="classSetting.aspx.cs" Inherits="FingerprintPayment.classconfig.classSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
        label {
            font-weight: normal;
            font-size: 26px;
        }

        .statusOnline {
            color: black;
        }

        .statusOffline {
            color: black;
        }

        .statusEmpty {
            background-color: white;
            color: white;
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .centertext {
            text-align: center;
        }

        .pad0 {
            padding: 0px;
        }

        .centerText {
            text-align: center;
        }

        .statusbox3 {
            width: 100px;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
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

        .smol {
            font-size: 80%;
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

        .attendancebox {
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
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


        .nowrap {
            max-width: 100%;
            white-space: nowrap;
        }

        .namemangin {
            margin-left: 5px;
            padding-left: 35px;
            border-left: 10px
        }


        .select2-selection__rendered {
            line-height: 39px !important;
        }

        .select2-container .select2-selection--single {
            height: 39px !important;
        }

        .select2-selection__arrow {
            height: 39px !important;
        }

            .select2-selection__arrow b {
                border-color: black transparent transparent transparent !important;
            }

        .select2-results__options {
            font-size: 20px !important;
        }

        [class^='select2'] {
            border-radius: 4px !important;
            border-top-color: #abadb3 !important;
            border-left-color: #dbdfe6 !important;
            border-right-color: #dbdfe6 !important;
            border-bottom-color: #dbdfe6 !important;
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
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            $('.js-example-basic-multiple3').select2();
        });

        $(document).ready(function () {
            $('.js-example-basic-multiple2').select2();
        });

        $(document).ready(function () {

            function start() {

                var statusbox1 = document.getElementsByClassName("statusbox1");
                for (var x = 1; x <= statusbox1.length; x++) {
                    startUp1(x);
                }



                var statusOn = document.getElementsByClassName("statusON");
                var statusOff = document.getElementsByClassName("statusOFF");
                var statusbox1 = document.getElementsByClassName("statusbox1");
                var statusbox2 = document.getElementsByClassName("statusbox2");
                var statusbox3 = document.getElementsByClassName("statusbox3");
                var classID = document.getElementsByClassName("classID");
                var itemtype = document.getElementsByClassName("itemtype");
                $.get("/App_Logic/classconfigStartUp.ashx", function (Result) {
                    $.each(Result, function (index) {

                        for (var x = 1; x <= statusbox1.length; x++) {
                            if (classID[x - 1].value == Result[index].id) {
                                if (itemtype[x - 1].value == "termsublevel" &&
                                    Result[index].type == "TTerm") {
                                    statusbox1[x - 1].classList.add('statusOnline');
                                    statusbox1[x - 1].classList.remove('statusOffline');
                                    statusbox2[x - 1].classList.add('statusOnline');
                                    statusbox2[x - 1].classList.remove('statusOffline');
                                    statusOn[x - 1].classList.remove('hidden');
                                    statusOff[x - 1].classList.add('hidden');
                                    statusbox3[x - 1].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>";
                                }
                                else if (itemtype[x - 1].value == "sublevel" &&
                                    Result[index].type == "Sublv") {
                                    statusbox1[x - 1].classList.add('statusOnline');
                                    statusbox1[x - 1].classList.remove('statusOffline');
                                    statusbox2[x - 1].classList.add('statusOnline');
                                    statusbox2[x - 1].classList.remove('statusOffline');
                                    statusOn[x - 1].classList.remove('hidden');
                                    statusOff[x - 1].classList.add('hidden');
                                    statusbox3[x - 1].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>";
                                }
                            }


                        }

                    });
                });
            }

            window.onload = start;



        });


    </script>

    <script type="text/javascript" language="javascript">

        function bootbox2() {
            bootbox.alert({
                message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00060") %></h3>',
                backdrop: true
            });
        }

        function toOnline(number, id) {
            var itemtype = document.getElementsByClassName("itemtype");
            var statusOn = document.getElementsByClassName("statusON");
            var statusOff = document.getElementsByClassName("statusOFF");
            var statusbox1 = document.getElementsByClassName("statusbox1");
            var statusbox2 = document.getElementsByClassName("statusbox2");
            var statusbox3 = document.getElementsByClassName("statusbox3");
            var classID = document.getElementsByClassName("classID");
            statusbox1[number - 1].classList.add('statusOnline');
            statusbox1[number - 1].classList.remove('statusOffline');
            statusbox2[number - 1].classList.add('statusOnline');
            statusbox2[number - 1].classList.remove('statusOffline');
            statusOn[number - 1].classList.remove('hidden');
            statusOff[number - 1].classList.add('hidden');
            statusbox3[number - 1].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>";
            if (itemtype[number - 1].value == "termsublevel") {
                $.ajax({
                    url: "/App_Logic/classconfigChangeStatus.ashx?status=1&type=tterm&id=" + id,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                });
            }
            else {
                $.ajax({
                    url: "/App_Logic/classconfigChangeStatus.ashx?status=1&type=sublv&id=" + id,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                });
                reset();
            }




        }
        function toOffline(number, id) {
            var itemtype = document.getElementsByClassName("itemtype");
            var statusOn = document.getElementsByClassName("statusON");
            var statusOff = document.getElementsByClassName("statusOFF");
            var statusbox1 = document.getElementsByClassName("statusbox1");
            var statusbox2 = document.getElementsByClassName("statusbox2");
            var statusbox3 = document.getElementsByClassName("statusbox3");
            var classID = document.getElementsByClassName("classID");
            statusbox1[number - 1].classList.add('statusOffline');
            statusbox1[number - 1].classList.remove('statusOnline');
            statusbox2[number - 1].classList.add('statusOffline');
            statusbox2[number - 1].classList.remove('statusOnline');
            statusOn[number - 1].classList.add('hidden');
            statusOff[number - 1].classList.remove('hidden');
            statusbox3[number - 1].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102151") %>";

            if (itemtype[number - 1].value == "termsublevel") {
                $.ajax({
                    url: "/App_Logic/classconfigChangeStatus.ashx?status=0&type=tterm&id=" + id,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",

                });
            }
            else {
                $.ajax({
                    url: "/App_Logic/classconfigChangeStatus.ashx?status=0&type=sublv&id=" + id,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",

                });
                reset();
            }
        }

        function startUp1(number) {
            var itemtype = document.getElementsByClassName("itemtype");
            var statusOn = document.getElementsByClassName("statusON");
            var statusOff = document.getElementsByClassName("statusOFF");
            var statusbox1 = document.getElementsByClassName("statusbox1");
            var statusbox2 = document.getElementsByClassName("statusbox2");
            var statusbox3 = document.getElementsByClassName("statusbox3");
            var classID = document.getElementsByClassName("classID");
            var modaladd = document.getElementsByClassName("modaladd");
            var editbox1 = document.getElementsByClassName("editbox1");
            var editbox2 = document.getElementsByClassName("editbox2");
            statusbox1[number - 1].classList.add('statusOffline');
            statusbox1[number - 1].classList.remove('statusOnline');
            statusbox2[number - 1].classList.add('statusOffline');
            statusbox2[number - 1].classList.remove('statusOnline');
            statusOn[number - 1].classList.add('hidden');
            statusOff[number - 1].classList.remove('hidden');
            statusbox3[number - 1].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102151") %>";

            if (itemtype[number - 1].value == "sublevel") {
                editbox1[number - 1].classList.add('hidden');
            }
            else {
                modaladd[number - 1].classList.add('hidden');
                editbox2[number - 1].classList.add('hidden');
            }
        }

        function startUp2(number) {
            var statusOn = document.getElementsByClassName("statusON");
            var statusOff = document.getElementsByClassName("statusOFF");
            var statusbox1 = document.getElementsByClassName("statusbox1");
            var statusbox2 = document.getElementsByClassName("statusbox2");
            var statusbox3 = document.getElementsByClassName("statusbox3");
            var classID = document.getElementsByClassName("classID");
            statusbox1[number - 1].classList.add('statusOnline');
            statusbox1[number - 1].classList.remove('statusOffline');
            statusbox2[number - 1].classList.add('statusOnline');
            statusbox2[number - 1].classList.remove('statusOffline');
            statusOn[number - 1].classList.remove('hidden');
            statusOff[number - 1].classList.add('hidden');
            statusbox3[number - 1].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>";
        }



        function editModal(id) {
            var editfullname = document.getElementsByClassName("editfullname");
            var editnickname = document.getElementsByClassName("editnickname");
            var editfullnameEN = document.getElementsByClassName("editfullnameEN");
            var editnicknameEN = document.getElementsByClassName("editnicknameEN");
            var editntsublevel = document.getElementsByClassName("editntsublevel");

            $.get("/App_Logic/classconfigEdit.ashx?mode=sublv&id=" + id, function (Result) {
                $.each(Result, function (index) {

                    editfullname[0].value = Result[index].fullName;
                    editnickname[0].value = Result[index].SubLevel;
                    editfullnameEN[0].value = Result[index].fullNameEN;
                    editnicknameEN[0].value = Result[index].SubLevelEN;
                    editntsublevel[0].value = Result[index].nTSubLevel;

                });
            });
        }

        function editModal2(id) {
            var modal3name = document.getElementsByClassName("modal3name");
            var modal3id = document.getElementsByClassName("modal3id");
            var editType = document.getElementsByClassName("editType");
            var dllSpec = document.getElementsByClassName("dllSpec");
            //alert(id);
            $.get("/App_Logic/classconfigEdit.ashx?mode=tterm&id=" + id, function (Result) {
                $.each(Result, function (index) {


                    var ddlPatient = document.getElementById("<%=ddlBranch.ClientID %>");
                    ddlPatient.value = Number(Result[index].branchSpecId);
                    $(ddlPatient).change(); // Trigger the change event.

                    modal3name[0].value = Result[index].nTSubLevel2;
                    modal3id[0].value = Result[index].nTermSubLevel2;
                    editType[0].value = Result[index].time;

                    //if (Result[index].nTLevel == "1" || Result[index].nTLevel == "2") {
                    //    dllSpec[0].classList.remove('hidden');
                    //    dllSpec[1].classList.remove('hidden');
                    //}
                    //else {
                    //    dllSpec[0].classList.add('hidden');
                    //    dllSpec[1].classList.add('hidden');
                    //}

                    if (Result[index].TLevelName == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>" || Result[index].TLevelName == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>") {
                        dllSpec[0].classList.remove('hidden');
                        dllSpec[1].classList.remove('hidden');
                    } else {
                        dllSpec[0].classList.add('hidden');
                        dllSpec[1].classList.add('hidden');
                    }


                });
            });
        }

        function deletemodal(number) {
            var itemtype = document.getElementsByClassName("itemtype");
            var classID = document.getElementsByClassName("classID");
            var deletefullname = document.getElementsByClassName("deletefullname");
            var deletenickname = document.getElementsByClassName("deletenickname");
            var deletefullnameEN = document.getElementsByClassName("deletefullnameEN");
            var deletenicknameEN = document.getElementsByClassName("deletenicknameEN");
            var control1 = document.getElementsByClassName("control1");
            var control2 = document.getElementsByClassName("control2");
            var control3 = document.getElementsByClassName("control3");
            var control4 = document.getElementsByClassName("control4");
            var control5 = document.getElementsByClassName("control5");
            var control6 = document.getElementsByClassName("control6");
            var deleteclass = document.getElementsByClassName("deleteclass");
            var deleteroom = document.getElementsByClassName("deleteroom");
            var deleteid = document.getElementsByClassName("deleteid");
            var edittype = document.getElementsByClassName("edittype");

            var id = classID[number - 1].value;
            if (itemtype[number - 1].value == "termsublevel") {
                $.get("/App_Logic/classconfigEdit.ashx?mode=tterm&id=" + id, function (Result) {
                    $.each(Result, function (index) {

                        deletefullname[0].value = "";
                        deletenickname[0].value = "";
                        deletefullnameEN[0].value = "";
                        deletenicknameEN[0].value = "";
                        control1[0].classList.add('hidden');
                        control2[0].classList.add('hidden');
                        control3[0].classList.add('hidden');
                        control4[0].classList.add('hidden');
                        control5[0].classList.remove('hidden');
                        control6[0].classList.remove('hidden');
                        deleteclass[0].value = Result[index].fullName;
                        deleteroom[0].value = Result[index].nTSubLevel2;
                        deleteid[0].value = classID[number - 1].value;
                        edittype[0].value = itemtype[number - 1].value;
                    });
                });
            }
            else {
                $.get("/App_Logic/classconfigEdit.ashx?mode=sublv&id=" + id, function (Result) {
                    $.each(Result, function (index) {

                        control1[0].classList.remove('hidden');
                        control2[0].classList.remove('hidden');
                        control3[0].classList.remove('hidden');
                        control4[0].classList.remove('hidden');
                        control5[0].classList.add('hidden');
                        control6[0].classList.add('hidden');
                        deletefullname[0].value = Result[index].fullName;
                        deletenickname[0].value = Result[index].SubLevel;
                        deletefullnameEN[0].value = Result[index].fullNameEN;
                        deletenicknameEN[0].value = Result[index].SubLevelEN;
                        deleteclass[0].value = "";
                        deleteroom[0].value = "";
                        deleteid[0].value = classID[number - 1].value;
                        edittype[0].value = itemtype[number - 1].value;

                    });
                });
            }
        }

        function addModal(number) {
            var addID = document.getElementsByClassName("addID");
            var addIDroom = document.getElementsByClassName("addIDroom");
            var addIDroomEN = document.getElementsByClassName("addIDroomEN");
            var dllSpec = document.getElementsByClassName("dllSpec");
            var editHourYear = document.getElementsByClassName("editHourYear");
            var ddlnew = document.getElementsByClassName("ddlnew");
            var btnerror = document.getElementsByClassName("btnerror");
            var btnok = document.getElementsByClassName("btnok");

            editHourYear[0].value = "";
            ddlnew[0].selectedIndex = 0;
            btnerror[0].classList.remove('hidden');
            btnok[0].classList.add('hidden');

            addID[0].value = number;
            $.get("/App_Logic/classconfigEdit.ashx?mode=sublv&id=" + number, function (Result) {
                $.each(Result, function (index) {

                    addIDroom[0].value = Result[index].fullName;
                    addIDroomEN[0].value = Result[index].fullNameEN;

                    //if (Result[index].nTLevel == "1" || Result[index].nTLevel == "2") {
                    //    dllSpec[0].classList.remove('hidden');
                    //    dllSpec[1].classList.remove('hidden');
                    //}
                    //else {
                    //    dllSpec[0].classList.add('hidden');
                    //    dllSpec[1].classList.add('hidden');
                    //}

                    if (Result[index].TLevelName == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>" || Result[index].TLevelName == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>") {
                        dllSpec[0].classList.remove('hidden');
                        dllSpec[1].classList.remove('hidden');
                    } else {
                        dllSpec[0].classList.add('hidden');
                        dllSpec[1].classList.add('hidden');
                    }

                });
            });

        }

        function checkError() {
            var addID = document.getElementsByClassName("addID");
            var addIDroom = document.getElementsByClassName("addIDroom");
            var addIDroomEN = document.getElementsByClassName("addIDroomEN");
            var dllSpec = document.getElementsByClassName("dllSpec");
            var editHourYear = document.getElementsByClassName("editHourYear");
            var ddlnew = document.getElementsByClassName("ddlnew");
            var btnerror = document.getElementsByClassName("btnerror");
            var btnok = document.getElementsByClassName("btnok");



            if (ddlnew[0].selectedIndex != 0 && editHourYear[0].value != "") {
                btnerror[0].classList.add('hidden');
                btnok[0].classList.remove('hidden');
            }
            else {
                btnerror[0].classList.remove('hidden');
                btnok[0].classList.add('hidden');
            }

        }

        function reset() {

            var statusbox1 = document.getElementsByClassName("statusbox1");
            for (var x = 1; x <= statusbox1.length; x++) {
                startUp1(x);
            }



            var statusOn = document.getElementsByClassName("statusON");
            var statusOff = document.getElementsByClassName("statusOFF");
            var statusbox1 = document.getElementsByClassName("statusbox1");
            var statusbox2 = document.getElementsByClassName("statusbox2");
            var statusbox3 = document.getElementsByClassName("statusbox3");
            var classID = document.getElementsByClassName("classID");
            var itemtype = document.getElementsByClassName("itemtype");
            $.get("/App_Logic/classconfigStartUp.ashx", function (Result) {
                $.each(Result, function (index) {

                    for (var x = 1; x <= statusbox1.length; x++) {
                        if (classID[x - 1].value == Result[index].id) {
                            if (itemtype[x - 1].value == "termsublevel" &&
                                Result[index].type == "TTerm") {
                                statusbox1[x - 1].classList.add('statusOnline');
                                statusbox1[x - 1].classList.remove('statusOffline');
                                statusbox2[x - 1].classList.add('statusOnline');
                                statusbox2[x - 1].classList.remove('statusOffline');
                                statusOn[x - 1].classList.remove('hidden');
                                statusOff[x - 1].classList.add('hidden');
                                statusbox3[x - 1].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>";
                            }
                            else if (itemtype[x - 1].value == "sublevel" &&
                                Result[index].type == "Sublv") {
                                statusbox1[x - 1].classList.add('statusOnline');
                                statusbox1[x - 1].classList.remove('statusOffline');
                                statusbox2[x - 1].classList.add('statusOnline');
                                statusbox2[x - 1].classList.remove('statusOffline');
                                statusOn[x - 1].classList.remove('hidden');
                                statusOff[x - 1].classList.add('hidden');
                                statusbox3[x - 1].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>";
                            }
                        }


                    }

                });
            });
        }


    </script>

    <div class="full-card box-content userlist-container">
        <asp:HiddenField ID="hdfsid" runat="server" />






        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802016") %></h2>
                    </div>
                    <div class="modal-body">


                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802017") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="modalType" runat="server" Width="80%" AutoPostBack="false">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01907") %>" Value="0" class="grey hidden "></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802018") %> </label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modalName" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802019") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modalNickName" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802020") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modalNameEN" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802021") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modalNickNameEN" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>


                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="btnSave" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal4" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802023") %></h2>
                    </div>
                    <div class="modal-body">
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802018") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modal4name" runat="server" Width="80%" CssClass="editfullname"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802019") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modal4nick" runat="server" Width="80%" CssClass="editnickname"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802020") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modal4nameEN" runat="server" Width="80%" CssClass="editfullnameEN"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802021") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modal4nickEN" runat="server" Width="80%" CssClass="editnicknameEN"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12 hidden" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label>ntsublevel</label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modal4id" runat="server" Width="80%" CssClass="editntsublevel"> </asp:TextBox>
                            </div>
                        </div>


                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="editclassroom" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="deleteModal" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %></h2>
                    </div>
                    <div class="modal-body">
                        <div class="col-xs-12 control1" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802018") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox1" Enabled="false" runat="server" Width="80%" CssClass="deletefullname"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12 control2" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802019") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox2" Enabled="false" runat="server" Width="80%" CssClass="deletenickname"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12 control3" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802020") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox3" Enabled="false" runat="server" Width="80%" CssClass="deletefullnameEN"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12 control4" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802021") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox4" Enabled="false" runat="server" Width="80%" CssClass="deletenicknameEN"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12 control5" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132056") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox7" Enabled="false" runat="server" Width="80%" CssClass="deleteclass"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12 control6" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801045") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox8" Enabled="false" runat="server" Width="80%" CssClass="deleteroom"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12 control7 hidden" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label>id</label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="delid" runat="server" Width="80%" CssClass="deleteid"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12 control8 hidden" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label>type</label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="deltype" runat="server" Width="80%" CssClass="edittype"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-warning global-btn" ID="delete1" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal3" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132057") %></h2>
                    </div>
                    <div class="modal-body">

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801045") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modal3name" runat="server" Width="80%" CssClass="modal3name"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105033") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="timeEdit" runat="server" class="editType" Width="80%">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-xs-12 hidden" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label>ntsublevel</label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modal3id" runat="server" Width="80%" CssClass="modal3id"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12 dllSpec" style="padding: 5px;">
                            <div class="col-xs-5 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132058") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="ddlBranch" runat="server" Width="80%" Style="height: 39px;" CssClass="js-example-basic-multiple3" name="classchoice3[]">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="modal3btn" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal2" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01278") %></h2>
                    </div>
                    <div class="modal-body">

                        <div class="col-xs-12 hidden" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label>nTSubLevel</label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modal2nTSubLevel" runat="server" CssClass="addID"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modal2room" runat="server" Enabled="false" Width="80%" CssClass="addIDroom"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modal2roomEN" runat="server" Enabled="false" Width="80%" CssClass="addIDroomEN"> </asp:TextBox>
                            </div>
                        </div>



                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105033") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="ddlTimeTable" onchange="checkError()" runat="server" class="ddlnew" Width="80%">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-xs-12 dllSpec" style="padding: 5px;">
                            <div class="col-xs-5 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132058") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="ddlBranch2" runat="server" class="form-control" Width="80%" Style="height: 39px;" CssClass="js-example-basic-multiple2" name="classchoice2[]">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801045") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="modal2Name" onkeyup="checkError()" runat="server" Width="80%" CssClass="editHourYear"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-12">
                                <h3>***<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802022") %></h3>
                            </div>
                        </div>

                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <div class="btn btn-warning btnerror" style="width: 100px; border: 1px; background-color: grey;" onclick="bootbox2()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></div>
                        <asp:Button CssClass="btn btn-primary global-btn btnok" ID="Modal2Button" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        OnDataBound="CustomersGridView_DataBound" ShowHeaderWhenEmpty="true"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />

                        <PagerTemplate>

                            <table width="100%" class="tab">
                                <tr>
                                    <td style="width: 25%">

                                        <asp:Label ID="Label1" BorderColor="#337AB7"
                                            ForeColor="white"
                                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>:"
                                            runat="server" />
                                        <asp:DropDownList ID="PageDropDownList2"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged2"
                                            runat="server" />

                                    </td>
                                    <td style="width: 45%">
                                        <asp:LinkButton ID="backbutton" runat="server"
                                            CssClass="imjusttext" OnClick="backbutton_Click">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>
                                        </asp:LinkButton>
                                        <asp:DropDownList ID="PageDropDownList"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged"
                                            runat="server" />
                                        <asp:LinkButton ID="nextbutton" runat="server"
                                            CssClass="imjusttext" OnClick="nextbutton_Click">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
                                        </asp:LinkButton>

                                    </td>

                                    <td style="width: 70%; text-align: right">

                                        <asp:Label ID="CurrentPageLabel"
                                            ForeColor="white"
                                            runat="server" />

                                    </td>

                                </tr>
                            </table>

                        </PagerTemplate>

                        <Columns>
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext pad0" HeaderStyle-CssClass="centertext pad0">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-CssClass="centertext pad0" HeaderStyle-CssClass="centertext pad0" HeaderText="">
                                <HeaderStyle Width="0.1%"></HeaderStyle>
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 " style="padding: 0px;">
                                        <input type="text" class="classID hidden" style="width: 1%; font-size: 1%; border: 0px; background-color: white;" disabled="disabled" value="<%# Eval("id") %>">
                                    </div>
                                    </div>       
                                </div>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802013") %>">
                                <HeaderStyle Width="25%"></HeaderStyle>
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 " style="padding: 0px;">
                                        <input type="text" class="planName" style="padding: 0px; padding-left: 15px; width: 100%; font-size: 90%; border: 0px; background-color: white;" disabled="disabled" value="<%# Eval("LevelName") %>">
                                    </div>
                                    </div>       
         </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802014") %>">
                                <HeaderStyle Width="25%"></HeaderStyle>
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 " style="padding: 0px;">
                                        <input type="text" class="planName" style="padding: 0px; padding-left: 15px; width: 100%; font-size: 90%; border: 0px; background-color: white;" disabled="disabled" value="<%# Eval("fullname") %>">
                                    </div>
                                    </div>       
                                </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802015") %>">
                                <HeaderStyle Width="25%"></HeaderStyle>
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 " style="padding: 0px;">
                                        <input type="text" class="planName" style="padding: 0px; padding-left: 15px; width: 100%; font-size: 90%; border: 0px; background-color: white;" disabled="disabled" value="<%# Eval("NickName") %>">
                                    </div>
                                    </div>       
                                </div>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField ItemStyle-CssClass="centertext statusOffline statusbox1" HeaderStyle-CssClass="centertext" HeaderText="">
                                <HeaderStyle Width="1%"></HeaderStyle>
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 statusOffline statusbox2 hidden" style="padding: 0px; font-size: 0.1%">
                                        <input type="text" class="statusOffline statusbox3 hidden" style="padding: 0px; font-size: 0.1%" />
                                    </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext" HeaderText="">
                                <HeaderStyle Width="1%"></HeaderStyle>
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 " style="padding: 0px; font-size: 0.1%;">
                                        <input type="text" class="planName itemtype hidden" style="width: 1%; font-size: 0.1%; border: 0px; background-color: white;" disabled="disabled" value="<%# Eval("type") %>">
                                    </div>
                                    </div>       
                                </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext pad0" HeaderStyle-CssClass="centertext" HeaderText="">
                                <HeaderStyle Width="1%"></HeaderStyle>
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 " style="padding: 0px;">
                                        <div class="btn btn-success modaladd" style="margin: 0px; font-size: 80%;" onclick="addModal(<%# Eval("id") %>)" data-toggle="modal" data-target="#myModal2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01278") %></div>
                                    </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12">
                                        <div class="col-lg-4 col-md-4 col-sm-4 adjust-col-padding col-space nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103164") %>">
                                                <div class="fa fa-toggle-on statusON" onclick="toOffline(<%# Eval("number") %>,<%# Eval("id") %>)" style="font-size: 70%; cursor: pointer; color: green;">
                                                </div>
                                            </div>
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103164") %>">
                                                <div class="fa fa-toggle-off statusOFF" onclick="toOnline(<%# Eval("number") %>,<%# Eval("id") %>)" style="font-size: 70%; cursor: pointer;">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 adjust-col-padding col-space nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>">
                                                <div class="glyphicon glyphicon-edit editbox1" onclick="editModal2(<%# Eval("id") %>)" style="font-size: 70%; cursor: pointer;" data-toggle="modal" data-target="#myModal3">
                                                </div>
                                            </div>
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>">
                                                <div class="glyphicon glyphicon-edit editbox2" onclick="editModal(<%# Eval("id") %>)" style="font-size: 70%; cursor: pointer;" data-toggle="modal" data-target="#myModal4">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>">
                                                <div class="glyphicon glyphicon-remove" onclick="deletemodal(<%# Eval("number") %>)" style="font-size: 70%; cursor: pointer; color: red;" data-toggle="modal" data-target="#deleteModal">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="btn btn-success " style="margin-left: 25px;" data-toggle="modal" data-target="#myModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802016") %></div>
                                </HeaderTemplate>
                                <HeaderStyle Width="15%"></HeaderStyle>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>

