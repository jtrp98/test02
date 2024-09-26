<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="GradeRoomList.aspx.cs" Inherits="FingerprintPayment.grade.GradeRoomList" %>

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

        .dropdown-menu > li > span {
            display: block;
            padding: 3px 20px;
            clear: both;
            font-weight: normal;
            line-height: 1.42857143;
            color: #333;
            white-space: nowrap;
            cursor: pointer;
            /*color: blue;
            text-decoration: underline;*/
        }
        .dropdown-menu > li > span:hover, .dropdown-menu > li > span:focus {
            color: #262626;
            text-decoration: none;
            background-color: #f5f5f5;
        }
        #mySpan {
            cursor: pointer;
            color: blue;
            text-decoration: underline;
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

            // $.ajaxSetup({
            //    error: AjaxError
            //});

            var full = window.location.href;
            var half = full.split('?');


            var greenbutton = document.getElementsByClassName("greenbutton");
            var sorry = document.getElementsByClassName("sorry");
            if (greenbutton.length == 0 && half[1].length > 10)
                sorry[0].classList.remove('hidden');

            $('input[id*=btnSearch]').click(function () {
                var load = document.getElementsByClassName("load");
                load[0].classList.remove('hidden');
                var mode = document.getElementsByClassName("mode");

                var param1var = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                var param2var = $('select[id*=ddlsublevel2] option:selected').val();
                var param3var = $('select[id*=DropDownList1] option:selected').val();
                if (param2var == undefined)
                    param2var = "";
                var param4var = $('select[id*=DropDownList2] option:selected').val();
                if (param4var == undefined)
                    param4var = "";

                if (param1var != "" && param2var != "" && param4var != "") {
                    window.location.href = "GradeRoomList.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&year=" + param3var + "&term=" + param4var + "&mode=" + mode[0].value;
                }
                else if (param1var == "") {
                     var load = document.getElementsByClassName("load");
                     load[0].classList.add('hidden');
                     bootbox.alert({
                        message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206003") %></h3>',
                        backdrop: true
                     });
                }
                else if (param2var == "") {
                     var load = document.getElementsByClassName("load");
                     load[0].classList.add('hidden');
                    bootbox.alert({
                        message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206004") %></h3>',
                        backdrop: true
                     });
                }
                else if (param4var == "") {
                     var load = document.getElementsByClassName("load");
                     load[0].classList.add('hidden');
                    bootbox.alert({
                        message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></h3>',
                        backdrop: true
                     });
                }


            });


        });


        //function AjaxError(x, e) {
        //      console.log("Ajax Error" + x)
        //                console.log("jqXHR" + e);
                       
        //}

        function bootbox2() {
            bootbox.alert({
                message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206412") %></h3>',
                backdrop: true
            });
        }

        function bootbox3() {
            bootbox.alert({
                message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132217") %></h3>',
                backdrop: true
            });
        }

        function bootbox4(url) {
            var full = window.location.href;
            var half = full.split('?');
            var split = half[1].split('&');
            var idlv = split[0].split('=');
            var idlv2 = split[1].split('=');
            var year = split[2].split('=');
            var term = split[3].split('=');

            var todayterm = document.getElementsByClassName("todayterm");

            if (todayterm[0].value != term[1]) {
                bootbox.confirm({
                    title: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101328") %></h>',
                    message: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00780") %></h>',
                    buttons: {
                        cancel: {
                            label: '<i class="fa fa-times"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>'
                        },
                        confirm: {
                            label: '<i class="fa fa-check"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206010") %>'
                        }
                    },
                    callback: function (result) {
                        if (result == true) {
                            ShowAdditionalAlert(url)
                        }
                    }
                });
            }
            else ShowAdditionalAlert(url);


        }

        function ShowAdditionalAlert(url) {

              bootbox.confirm({
                    title: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206006") %></h>',
                    message: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206007") %></h><br><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206008") %><br><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206009") %><br>064-9364027',
                    buttons: {
                        cancel: {
                            label: '<i class="fa fa-times"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>'
                        },
                        confirm: {
                            label: '<i class="fa fa-check"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206010") %>'
                        }
                    },
                    callback: function (result) {
                        if (result == true) {
                           window.location.href = url;
                        }
                    }
                });
        }

        function changeFinger() {
            $.ajax("/Api/change/?userid=" + $("#ctl00_MainContent_hdfsid").val() + "&type=0", function (Result) {
            }).done(function (Result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111048") %> " + Result);
                $("#modalpopupdatamac .modal-footer").addClass("hidden");
            });
        }

        function ddlyear() {
            var ddl1 = document.getElementsByClassName("ddl1");
            var select = document.getElementById('DD1');

            for (i = -1; i <= 5; i++) {
                ddl1[1].remove(0);
            }

           <%-- $.get("/App_Logic/ddlterm.ashx?year=" + ddl1[0].options[ddl1[0].selectedIndex].value, function (Result) {
                $.each(Result, function (index) {

                    // Create an Option object       
                    var opt = document.createElement("option");

                    // Assign text and value to Option object
                    opt.text = Result[index].value;
                    opt.value = Result[index].value;

                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=DropDownList2.ClientID%>').options.add(opt);


                });
            });--%>

            $.get("../api/common/TermByYear/?numberYear=" + ddl1[0].options[ddl1[0].selectedIndex].value, function (Result) {
                $.each(Result, function (index) {

                    // Create an Option object       
                    var opt = document.createElement("option");

                    // Assign text and value to Option object
                    opt.text = Result[index].sTerm;
                    opt.value = Result[index].sTerm;

                     if (getUrlParameter("term") != "" && getUrlParameter("term") == Result[index].sTerm) {
                            opt.selected = "selected";
                     }  

                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=DropDownList2.ClientID%>').options.add(opt);


                });
            }).fail(function (jqXHR, textStatus, errorThrown) {

                if (errorThrown == "Request Timeout") {

                    bootbox.confirm({
                    title: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101328") %></h>',
                    message: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132218") %></h>',
                    buttons: {
                       
                        confirm: {
                            label: '<i class="fa fa-check"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206010") %>'
                        }
                    },
                    callback: function (result) {
                        
                            window.location.href = window.location.protocol + "//" + window.location.host + "/Default.aspx";
                       
                    }
                    });

                }
                
            });


        }

      

        function ddlclass() {
            var ddl2 = document.getElementsByClassName("ddl2");
            for (i = -1; i <= 90; i++) {
                    ddl2[1].remove(0);
            }
            if (ddl2[0].options[ddl2[0].selectedIndex].value != "") {
                $.get("/App_Logic/ddlclassroom.ashx?idlv=" + ddl2[0].options[ddl2[0].selectedIndex].value, function (Result) {

                    // Create an Empty Option object       
                    var opt = document.createElement("option");

                    // Assign text and value to Option object
                    opt.text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %>";
                    opt.value = "";

                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);

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
                }).fail(function (jqXHR, textStatus, errorThrown) {

                    if (errorThrown == "Request Timeout") {

                        bootbox.confirm({
                        title: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101328") %></h>',
                        message: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132218") %></h>',
                        buttons: {
                       
                            confirm: {
                                label: '<i class="fa fa-check"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206010") %>'
                            }
                        },
                        callback: function (result) {
                           
                                window.location.href = window.location.protocol + "//" + window.location.host + "/Default.aspx";
                            
                        }
                        });
                    }
                
                });
            }
            else {

                 // Create an Empty Option object       
                var opt = document.createElement("option");

                // Assign text and value to Option object
                opt.text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %>";
                opt.value = "";

                // Add an Option object to Drop Down List Box
                document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);
                
            }
        }

        function ShowPopUP(userid, name) {
            $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>" +
                "เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
            $("#ctl00_MainContent_hdfsid").val(userid);
            $("#modalpopupdatamac .modal-footer").removeClass("hidden")
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

            var d2 = document.getElementsByClassName("d2");
            ddlyear();
            ddlclass();

            var full = window.location.href;
            var half = full.split('?');
            var split = half[1].split('&');
            var idlv = split[0];
            var idlv2 = split[1].split('=');
            var year = split[2];
            var term = split[3].split('=');

            var d1 = document.getElementsByClassName("d1");
            console.log("location.hostname" + location.hostname);
            //if (location.hostname === "system.schoolbright.co") {
            //    $("#lnkallroomsrpt").css("display", "none");
            //}

            if (mode[0].value == "1") {
                //alert("aa");
                for (var i = 0; i < orange.length; i++) {
                    orange[i].classList.add('hidden');
                    orange2[i].classList.add('hidden');
                    green[i].classList.remove('hidden');
                }
            }

            if (mode[0].value == "3") {
                //alert("bb");

                for (var i = 0; i < orange.length; i++) {

                    if (havedata[i].value == "0") {
                        greybutton[i].classList.remove('hidden');
                    }
                    else {
                        orange[i].classList.add('hidden');
                        green[i].classList.add('hidden');
                        orange2[i].classList.remove('hidden');
                    }

                }
            }
            var settingtime = document.getElementsByClassName("settingtime");

            if (settingtime[0].value == "1") {
                if (time[0].value == "out") {
                    for (var i = 0; i < timebutton.length; i++) {
                        orange[i].classList.add('hidden');
                        orange2[i].classList.add('hidden');
                        green[i].classList.add('hidden');
                        timebutton[i].classList.remove('hidden');
                    }
                }
            }

            var extime = document.getElementsByClassName("extime");
            for (var x = 0; x < extime.length; x++) {
                if (extime[x].textContent == "1") {
                    green[x].classList.remove('hidden');
                    timebutton[x].classList.add('hidden');
                }
            }

            if (mode[0].value == "2") {
                //alert("bb");

                for (var i = 0; i < orange.length; i++) {

                    if (timebutton[i] != undefined)
                        timebutton[i].classList.add('hidden');

                    if (havedata[i] != undefined) {
                        if (havedata[i].value == "0") {
                            greybutton[i].classList.remove('hidden');
                        }
                        else {
                            orange[i].classList.remove('hidden');
                            green[i].classList.add('hidden');
                            orange2[i].classList.add('hidden');
                        }
                    }

                }
            }

            setTimeout(function () {
                document.getElementById('<%=DropDownList2.ClientID%>').value = d1[0].value;
                document.getElementById('<%=ddlsublevel2.ClientID%>').value = d2[0].value;
            }, 900);
        }

        function Test() {
            alert("X");
            return true;
        }

        function allroomslinkclick(PlanCourseId, planId) {
            $('#loading').show();
            // Prevent the link from immediately navigating
            //event.preventDefault();

            $.get("../api/common/GetStudyStudentsForSpecificSubject/?year=" + getUrlParameter("year") + "&term=" + getUrlParameter("term") + "&idlv=" + getUrlParameter("idlv") + "&planCourseId=" + PlanCourseId + "&sPlaneid=" + planId, function (Result) {
                if (Result.length > 0) {
                    var destinationUrl = "/grade/BP5detailallrooms.aspx?year=" + getUrlParameter("year") + "&idlv=" + getUrlParameter("idlv") + "&idlv2=" + getUrlParameter("idlv2") + "&term=" + getUrlParameter("term") + "&id=" + planId + "&PlanCourseId=" + PlanCourseId;
                   
                    window.open(destinationUrl, "_blank");
                   
                    console.log("redirect");
                   

                } else {

                    bootbox.alert({
                        message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206442") %></h3>',
                        backdrop: true
                    });
                    $('#loading').hide();
                    // Handle the case where the AJAX function returns false or some other value
                    /* alert("AJAX function did not return true.");*/
                }
            });

        }
            
        //function ShowModalConfig() {

            

        //    $('#modalConfig').modal('show');

        //    return false;
        //}
        window.onload = start;
    </script>
    <%--  <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTUser" ServicePath="AutoCompleteService.asmx" EnableCaching="true"
        FirstRowSelected="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>--%>
    <div class="full-card box-content userlist-container">
        <div id="loading" class="hidden load"></div>
        <asp:HiddenField ID="hdfsid" runat="server" />

        <div class="form-group row student">
            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">
                    <label>ddl1</label>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtddl1" runat="server" CssClass="d1" Width="50%"> </asp:TextBox>
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



        <div class="row">
            <div class="col-xs-12 button-section">
                <input type="button" id="btnSearch" class='btn btn-info search-btn' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
            </div>
        </div>

        <div class="col-md-6 col-sm-12 hidden">
            <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                time</label>
            <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                <asp:TextBox ID="Textbox1" runat="server" CssClass="time" Width="50%"> </asp:TextBox>
            </div>
            <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                todayterm</label>
            <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                <asp:TextBox ID="todayterm" runat="server" CssClass="todayterm" Width="50%"> </asp:TextBox>
            </div>
        </div>
        <div class="col-xs-12 hidden">
            <asp:TextBox ID="settingtime" runat="server" CssClass="settingtime" Width="50%"> </asp:TextBox>
            <asp:TextBox ID="plan" runat="server" CssClass="settingplan" Width="50%"> </asp:TextBox>
        </div>

        <div class="col-xs-12 hidden sorry">
            <hr />
            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206005") %></label>
        </div>

        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        OnDataBound="CustomersGridView_DataBound"
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
                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                <HeaderStyle Width="8%"></HeaderStyle>
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 " style="padding: 0px;">
                                        <input type="text" class="" style="padding: 0px; text-align: center; width: 100%; font-size: 90%; border: 0px; background-color: white;" value="<%# Eval("number") %>">
                                        <input type="text" class="havedata hidden" style="padding: 0px; text-align: center; width: 100%; font-size: 90%; border: 0px; background-color: white;" value="<%# Eval("havedata") %>">
                                    </div>
                                    </div>       
                                </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:BoundField DataField="planId" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132219") %>">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="code" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %>">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="planName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %>">
                                <HeaderStyle Width="25%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="extratime" HeaderText="" ItemStyle-CssClass="extime">
                                <HeaderStyle Width="1%"> </HeaderStyle>
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%--<a onclick="ShowModalConfig();" class="btn btn-success" role="button" aria-pressed="true" style="width: 159px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211051") %> <i class="fa fa-cog"></i></a>--%>
                                   <%--<a id="linktoopenallstudent" target="_blank"></a>--%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <% if (Session["sEntities"].ToString() == "JabJaiEntities00005")
                                        {
                                    %>
                                    &nbsp;<a target="_blank" href="/grade/BP5cover-chalerm.aspx?year=<%# Eval("year")%>&idlv=<%# Eval("idlv")%>&idlv2=<%# Eval("idlv2")%>&term=<%# Eval("term")%>&id=<%# Eval("planId")%>&mode=3&PlanCourseId=<%# Eval("PlanCourseId")%>"
                                        class="btn btn-warning right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132220") %> </a>
                                    <%
                                        } %>

                                    <div class="dropdown orangebutton hidden" >
                                            <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206410") %> <span class="caret" />
                                            </button>
                                            <ul class="dropdown-menu" style="font-size:24px">
                                                <li><a target="_blank" href="/grade/BP5detail.aspx?year=<%# Eval("year")%>&idlv=<%# Eval("idlv")%>&idlv2=<%# Eval("idlv2")%>&term=<%# Eval("term")%>&id=<%# Eval("planId")%>&PlanCourseId=<%# Eval("PlanCourseId")%>" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206410") %></a></li>
                                                <li id="lnkallroomsrpt"><span onclick="allroomslinkclick(<%# Eval("PlanCourseId")%>, <%# Eval("planId")%>)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206411") %></span>
                                                   <%-- <a id="<%# Eval("planId")%>" onclick="return Test()" target="_blank" href="/grade/BP5detail.aspx?year=<%# Eval("year")%>&idlv=<%# Eval("idlv")%>&idlv2=<%# Eval("idlv2")%>&term=<%# Eval("term")%>&id=<%# Eval("planId")%>&PlanCourseId=<%# Eval("PlanCourseId")%>" ></a>--%>

                                                </li>
                                            </ul>
                                    </div>
                                    
                                    <a target="_blank" href="/grade/BP5cover-chalerm.aspx?year=<%# Eval("year")%>&idlv=<%# Eval("idlv")%>&idlv2=<%# Eval("idlv2")%>&term=<%# Eval("term")%>&id=<%# Eval("planId")%>&PlanCourseId=<%# Eval("PlanCourseId")%>" class="btn btn-warning orangebutton2 hidden pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206410") %> </a>
                                    <%--<div class="btn btn-primary greybutton hidden pull-right" onclick="bootbox2()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206410") %></div>--%>
                                    <div class="dropdown greybutton hidden" >
                                            <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206410") %> <span class="caret" />
                                            </button>
                                            <ul class="dropdown-menu" style="font-size:24px">
                                                <li><a target="_blank" onclick="bootbox2();return false;" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206410") %></a></li>
                                                <li id="lnkallroomsrptNoGrade"><a target="_blank" onclick="bootbox2();return false;"  ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206411") %></a></li>
                                            </ul>
                                    </div>
                                    <div class="btn btn-success timebutton hidden" onclick="bootbox3()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206001") %></div>
                                    <div class="btn btn-success greenbutton hidden" onclick="bootbox4('../Pages/grade/AssessmentScore.aspx?year=<%# Eval("year")%>&idlv=<%# Eval("idlv")%>&idlv2=<%# Eval("idlv2")%>&term=<%# Eval("term")%>&id=<%# Eval("planId")%>&mode=<%# Eval("mode")%>&PlanCourseId=<%# Eval("PlanCourseId")%>')"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206001") %></div>
                                </ItemTemplate>
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:TemplateField>



                            <asp:BoundField DataFormatString="sFinger" HeaderText="sFinger" Visible="False"></asp:BoundField>

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
     <!-- /.modal -->

    <%--<div id="modalConfig" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 10%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="max-width: 900px;">

                <div class="modal-header center" style="padding: 0px; top: 25%;">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211051") %></h1>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row" style="padding-bottom: 10px;">
                            <div class="col-md-4">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>:</label>
                            </div>
                            <div class="col-md-6">
                                <div class='input-group date' id='divDocDate'>
                                    <input id="iptDocDate" name="iptDocDate" type='text' class="form-control"
                                        placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01985") %>" maxlength="10" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                        <div class="row" style="padding-bottom: 10px;">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-8">
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="checkbox" id="chkShowStampDate" value="1" />
                                    <label class="form-check-label" for="chkShowStampDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02208") %></label>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 10px;">
                            <div class="col-md-4">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00993") %>:</label>
                            </div>
                            <div class="col-md-8">
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="checkbox" id="chkOriginalDocument" value="1" />
                                    <label class="form-check-label" for="chkOriginalDocument"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206520") %></label>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 10px;">
                            <div class="col-md-4">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101294") %>:</label>
                            </div>
                            <div class="col-md-8">
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="checkbox" id="chkHideProfileImage" value="1" />
                                    <label class="form-check-label" for="chkHideProfileImage"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00608") %></label>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 10px;">
                            <div class="col-md-4">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206510") %>:</label>
                            </div>
                            <div class="col-md-8">
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="checkbox" id="chkShowDirector" value="1" />
                                    <label class="form-check-label" for="chkShowDirector"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206399") %></label>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 10px;">
                            <div class="col-md-4"></div>
                            <div class="col-md-8">
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="checkbox" id="chkShowDirectorPosition" value="1" />
                                    <label class="form-check-label" for="chkShowDirectorPosition"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02195") %></label>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 10px;">
                            <div class="col-md-4">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206511") %>:</label>
                            </div>
                            <div class="col-md-8">
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="checkbox" id="chkShowRegistrar" value="1" />
                                    <label class="form-check-label" for="chkShowRegistrar"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02193") %></label>
                                </div>
                            </div>
                        </div>
                        <div class="row credit-config" style="padding-bottom: 10px;">
                            <div class="col-md-12">
                                <span style="font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00688") %></span>
                            </div>
                        </div>
                        <div class="row credit-config">
                            <div class="col-md-12 condition">
                                <span>1. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01156") %></span> <span id="spnCreditSum1">81</span> <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206138") %><br />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00663") %></span>
                                <input id="iptCredit11" type="text" class="form-control input-underline credit-desc-1" value="52" maxlength="2" />
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206138") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01945") %></span>
                                <input id="iptCredit12" type="text" class="form-control input-underline credit-desc-1" value="29" maxlength="2" />
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206138") %></span>
                            </div>
                        </div>
                        <div class="row credit-config">
                            <div class="col-md-12 condition">
                                <span>2. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01155") %></span> <span id="spnCreditSum2">77</span> <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206138") %><br />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00663") %></span>
                                <input id="iptCredit21" type="text" class="form-control input-underline credit-desc-2" value="52" maxlength="2" />
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206138") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01946") %></span>
                                <input id="iptCredit22" type="text" class="form-control input-underline credit-desc-2" value="25" maxlength="2" />
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206138") %></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnSaveConfig" class="btn btn-success global-btn">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    <button type="button" class="btn btn-danger global-btn" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>

            </div>
        </div>
    </div>--%>
    <!-- /.modal -->
</asp:Content>
