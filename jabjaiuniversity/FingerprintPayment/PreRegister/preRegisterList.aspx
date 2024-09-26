<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="preRegisterList.aspx.cs" Inherits="FingerprintPayment.PreRegister.preRegisterList" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
        label {
            font-weight: normal;
            font-size: 26px;
        }

        .btn-group, .btn-group-vertical {
            position: relative;
            display: inline-block;
            vertical-align: middle;
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .centertext {
            text-align: center;
        }

        .padcenter {
            text-align: center;
            padding-left: 0px !important;
            padding-right: 0px !important;
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }
    </style>
    <style>
        .select2-selection__rendered {
            line-height: 41px !important;
        }

        .select2-container .select2-selection--single {
            height: 41px !important;
        }

        .select2-selection__arrow {
            height: 41px !important;
            display: none !important;
        }

            .select2-selection__arrow b {
                display: none !important;
            }

        [class^='select2'] {
            border-radius: 1px !important;
            border-top-color: #abadb3 !important;
            border-left-color: #dbdfe6 !important;
            border-right-color: #dbdfe6 !important;
            border-bottom-color: #dbdfe6 !important;
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

        .ddlconfig {
            border-top-right-radius: 5px !important;
            border-bottom-right-radius: 5px !important;
            border-top-left-radius: 5px !important;
            border-bottom-left-radius: 5px !important;
            font-size: 90% !important;
        }

        #loading {
            display: block;
            position: fixed;
            top: 0;
            left: 0;
            z-index: 9999;
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

        .pad0 {
            padding: 0px !important;
        }

        .gvbutton {
            font-size: 25px;
        }

        .nounder a:hover {
            text-decoration: none;
        }

        .select2-results__option {
            font-size: 120%;
        }

        .shadowblack {
            text-decoration: none;
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .smalltxt {
            font-size: 1% !important;
            color: white;
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

        $(document).ready(function () {
            $('.js-example-basic-multiple2').select2({
                minimumResultsForSearch: 3,
                allowClear: true,
                placeholder: ''
            });
        });

        function comboclick() {
            var data = $('.js-example-basic-multiple2').select2('data');
            data.prop('selectedIndex', 1).change();
            $('#fieldId').select2("val", $('#fieldId option:eq(1)').val());
        }

        function clickexport() {

            var clickButton = document.getElementById("<%= btnExport.ClientID %>");
            clickButton.click();

        }

        function saveclick() {
            var loading = document.getElementsByClassName("loading");
            loading[0].classList.remove('hidden');

            var clickButton = document.getElementById("<%= importSave.ClientID %>");
            clickButton.click();


        }

        function checkbutton() {
            var moveid = document.getElementsByClassName("moveid");
            var moveinerror = document.getElementsByClassName("moveinerror");
            var moveinok = document.getElementsByClassName("moveinok");

            if (moveid[0].value == "") {
                moveinerror[0].classList.remove('hidden');
                moveinok[0].classList.add('hidden');
            }
            else {
                moveinerror[0].classList.add('hidden');
                moveinok[0].classList.remove('hidden');
            }
        }

        function showloading() {
            var loading = document.getElementsByClassName("loading");

            loading[0].classList.remove('hidden');
        }

        function ddlclass() {
            var ddl2 = document.getElementsByClassName("ddl2");
            var load = document.getElementsByClassName("load");
            load[0].classList.remove('hidden');
            for (i = -1; i <= 90; i++) {
                ddl2[1].remove(0);
            }
            $("#<%=editilv2.ClientID%> option").remove();

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
                    document.getElementById('<%=editilv2.ClientID%>').options.add(opt);


                });
                load[0].classList.add('hidden');
                ddlchange2();
            });

        }

        function ddlclass_2() {
            var ddl2 = document.getElementsByClassName("ddl2");

            for (i = -1; i <= 90; i++) {
                ddl2[3].remove(0);
            }

            $.get("/App_Logic/ddlclassroom.ashx?idlv=" + ddl2[2].options[ddl2[2].selectedIndex].value, function (Result) {
                $.each(Result, function (index) {

                    // Create an Option object       
                    var opt = document.createElement("option");

                    // Assign text and value to Option object
                    opt.text = Result[index].name;
                    opt.value = Result[index].value;

                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=DropDownList5.ClientID%>').options.add(opt);


                });
                ddlchange2_2();
            });

        }

        $(window).on('load', function () {
            start();

            ddlclass_2();

            var sel = document.getElementById("ctl00_MainContent_optionLevel");
            var val = document.getElementById("ctl00_MainContent_Textbox4").value;

            for (var i = 0, j = sel.options.length; i < j; ++i) {

                if (sel.options[i].value === val) {
                    sel.selectedIndex = i;
                    break;
                }
            }
            idlvchange();

        });

        function editModal(code, id) {
            var modalCode = document.getElementsByClassName("modalCode");
            var modalID = document.getElementsByClassName("modalID");
            modalID[0].value = id;
            modalCode[0].value = code;
            ddlclass();
        }

        function ddlchange2() {
            var modalidlv2 = document.getElementsByClassName("modalidlv2");
            var ddlidlv2 = document.getElementsByClassName("ddlidlv2");
            modalidlv2[0].value = ddlidlv2[0].options[ddlidlv2[0].selectedIndex].value;
        }

        function ddlchange2_2() {
            var modalidlv2 = document.getElementsByClassName("modalidlv2");
            var ddlidlv2 = document.getElementsByClassName("ddlidlv2");
            modalidlv2[1].value = ddlidlv2[1].options[ddlidlv2[1].selectedIndex].value;
        }

        function movein(index) {
            var moveid = document.getElementsByClassName("moveid");
            var movecheck = document.getElementsByClassName("movecheck");
            var movetxt = document.getElementsByClassName("movetxt");

            if (movecheck[index - 1].checked == true) {
                moveid[0].value = moveid[0].value + movetxt[index - 1].textContent + "~";
            }
            else {
                moveid[0].value = "";
                for (var x = 0; x < movecheck.length; x++) {
                    if (movecheck[x].checked == true) {
                        moveid[0].value = moveid[0].value + movetxt[x].textContent + "~";
                    }
                }

            }
            checkbutton();

        }

        function moveinall() {
            var moveid = document.getElementsByClassName("moveid");
            var movecheck = document.getElementsByClassName("movecheck");
            var movecheckall = document.getElementsByClassName("movecheckall");
            var movetxt = document.getElementsByClassName("movetxt");

            if (movecheckall[0].checked == true) {
                for (var x = 0; x < movecheck.length; x++) {
                    movecheck[x].checked = true;

                }

                moveid[0].value = "";
                for (var x = 0; x < movecheck.length; x++) {
                    if (movecheck[x].checked == true) {
                        moveid[0].value = moveid[0].value + movetxt[x].textContent + "~";
                    }
                }
            }
            else {
                for (var x = 0; x < movecheck.length; x++) {
                    movecheck[x].checked = false;
                }

                moveid[0].value = "";

            }
            checkbutton();
        }

        function ddlchangestatus() {
            var ddlstatus = document.getElementsByClassName("ddlstatus");
            var ddlhidden = document.getElementsByClassName("ddlhidden");
            var import1 = document.getElementsByClassName("import1");
            var import2 = document.getElementsByClassName("import2");
            var modalok = document.getElementsByClassName("modalok");

            if (ddlstatus[0].value == "3") {
                ddlhidden[0].classList.remove('hidden');
                ddlhidden[1].classList.remove('hidden');
                ddlhidden[2].classList.remove('hidden');
                ddlhidden[3].classList.remove('hidden');
                modalok[0].classList.remove('hidden');
                modalok[1].classList.add('hidden');
                modalok[2].classList.add('hidden');
                modalok[3].classList.add('hidden');
                import1[0].classList.add('hidden');
                import2[0].classList.add('hidden');
                ddlchangestatus2();
            }
            else {
                ddlhidden[0].classList.add('hidden');
                ddlhidden[1].classList.add('hidden');
                ddlhidden[2].classList.add('hidden');
                ddlhidden[3].classList.add('hidden');
                modalok[0].classList.add('hidden');
                modalok[1].classList.add('hidden');
                modalok[2].classList.add('hidden');
                modalok[3].classList.add('hidden');
                import1[0].classList.add('hidden');
                import2[0].classList.remove('hidden');
            }

        }

        function ddlchangestatus2() {
            var modalbirth = document.getElementsByClassName("modalbirth");
            var import1 = document.getElementsByClassName("import1");
            var import2 = document.getElementsByClassName("import2");
            var modalok = document.getElementsByClassName("modalok");


            if (modalbirth[0].value != "-1" && modalbirth[1].value != "-1") {
                modalok[0].classList.add('hidden');
                modalok[1].classList.add('hidden');
                modalok[2].classList.add('hidden');
                modalok[3].classList.add('hidden');
                import2[0].classList.add('hidden');
                import1[0].classList.remove('hidden');
            }


        }

        function ddlchangestatus3() {
            var modalbirth = document.getElementsByClassName("modalbirth");
            var import1 = document.getElementsByClassName("import1");
            var import2 = document.getElementsByClassName("import2");
            var modalok2 = document.getElementsByClassName("modalok2");


            if (modalbirth[2].value != "-1" && modalbirth[3].value != "-1") {
                modalok2[0].classList.add('hidden');
                import1[1].classList.remove('hidden');
            }


        }

        function delModal(code, name, id) {
            var deleteName = document.getElementsByClassName("deleteName");
            var deleteCode = document.getElementsByClassName("deleteCode");
            var deleteId = document.getElementsByClassName("deleteId");
            deleteName[0].value = name;
            deleteCode[0].value = code;
            deleteId[0].value = id;
        }

        function idlvchange() {

            var idlvchange = document.getElementsByClassName("idlvchange");
            var param5var = $('select[id*=optionLevel] option:selected').val();
            var idlv = param5var.split('/');
            if (idlv[1] == "1" || idlv[1] == "2")
                idlvchange[0].classList.remove('hidden');
            else idlvchange[0].classList.add('hidden');

            var planList = document.getElementsByClassName("planList");
            if (idlv[0] != "0") {
                LoadPlan(idlv[0]);

                planList[0].classList.remove('hidden');
            }
            else {
                planList[0].classList.add('hidden');
            }
        }

        function LoadPlan(planID) {
            $.ajax({
                async: false,
                type: "POST",
                url: "preRegisterList.aspx/LoadPlan",
                data: '{planID: ' + planID + ' }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessLoadPlan,
                failure: function (response) {
                    console.log(response.d);
                },
                error: function (response) {
                    console.log(response.d);
                }
            });
        }

        function OnSuccessLoadPlan(response) {
            var plans = response.d;

            $('select[id*=ddlPlan]').empty();

            var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>';
            $(plans).each(function () {

                options += '<option value="' + this.ID + '">' + this.Planname + '</option>';

            });

            $('select[id*=ddlPlan]').html(options);
        }

        function start() {

            var modalLink = document.getElementsByClassName("modalLink");

            var full = modalLink[0].textContent;
            var half = full.split('/');

            modalLink[0].textContent = half[0] + "//" + half[2] + "/" + half[3] + "/" + half[5];

            var availableTags = [];


            $.get("/PreRegister/preRegisterStudentList.ashx", function (Result) {
                $.each(Result, function (index) {
                    availableTags.push(Result[index].fullName);
                });
            });


            $("#ctl00_MainContent_tags").autocomplete({
                source: availableTags
            });

            var availableTags2 = [];


            $.get("/App_Logic/branchList.ashx", function (Result) {
                $.each(Result, function (index) {
                    availableTags2.push(Result[index].name);
                });
            });


            $("#ctl00_MainContent_tags2").autocomplete({
                source: availableTags2
            });
        }

        $(document).ready(function () {


            $('input[id*=btnSearch]').click(function () {
                var load = document.getElementsByClassName("load");
                load[0].classList.remove('hidden');
                var mode = document.getElementsByClassName("mode");
                var inputname = document.getElementsByClassName("inputname");
                var inputname2 = document.getElementsByClassName("inputname2");


                var param3var = $('select[id*=DropDownList1] option:selected').val();
                var param4var = $('select[id*=DropDownList2] option:selected').val();
                if (param4var == undefined)
                    param4var = "";
                var param5var = $('select[id*=optionLevel] option:selected').val();
                var idlv = param5var.split('/');
                var param6var = $('select[id*=ddlCourseType] option:selected').val();
                var param7var = $('select[id*=ddlCourseTime] option:selected').val();
                var param8var = $('select[id*=ddlPlan] option:selected').val();
                if (!param8var) param8var = '';

                window.location.href = "preRegisterList.aspx?year=" + param3var + "&mode=" + param4var + "&name=" + inputname[0].value + "&idlv=" + idlv[0] + "&type=" + param6var + "&time=" + param7var + "&branch=" + inputname2[0].value + "&planID=" + param8var;

            });


        });

        function bootbox2() {
            bootbox.alert({
                message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132785") %></h3>',
                backdrop: true
            });
        }

        function bootbox4() {
            bootbox.alert({
                message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132786") %></h3>',
                backdrop: true
            });
        }

        function bootbox3() {
            bootbox.alert({
                message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132787") %></h3>',
                backdrop: true
            });
        }

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


            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="DropDownList1" onchange="ddlyear()" runat="server" CssClass="ddl1 form-control" Width="100%">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddl1 form-control" Width="100%">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="0" CssClass=""></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103003") %>" Value="1" CssClass=""></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103004") %>" Value="2" CssClass=""></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103005") %>" Value="3" CssClass=""></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01460") %>" Value="4" CssClass=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

        </div>

        <div class="form-group row student">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="optionLevel" onchange="idlvchange()" runat="server" CssClass="ddl1 form-control" Width="100%">
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="0/0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201034") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlCourseType" runat="server" CssClass="ddl1 form-control" Width="100%">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="0" CssClass=""></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>" Value="1" CssClass=""></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103009") %>" Value="3" CssClass=""></asp:ListItem>

                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="form-group row student hidden idlvchange">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132766") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlCourseTime" runat="server" CssClass="ddl1 form-control" Width="100%">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="0" CssClass=""></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132767") %>" Value="1" CssClass=""></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132768") %>" Value="2" CssClass=""></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132769") %>" Value="3" CssClass=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02951") %></label>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                    <asp:TextBox ID="tags2" runat="server" class="form-control inputname2" Width="100%">                    
                    </asp:TextBox>
                </div>
            </div>
            <asp:TextBox ID="Textbox4" runat="server" class="hidden" Width="100%">                    
            </asp:TextBox>
        </div>

        <div class="form-group row student">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                    <asp:TextBox ID="tags" runat="server" class="form-control inputname" Width="100%">                    
                    </asp:TextBox>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 hidden planList">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103202") %></label>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlPlan" runat="server" CssClass="ddl1 form-control" Width="100%">
                    </asp:DropDownList>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-xs-6 button-section">
                <input type="button" id="btnSearch" class='btn btn-info search-btn pull-right' value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
            </div>
            <div class="col-xs-1">
            </div>
            <div class="col-xs-5 button-section">

                <div class="btn btn-warning" onclick="clickexport()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103040") %></div>
                <asp:Button ID="btnExport" CausesValidation="false" class="btn btn-success hidden" runat="server" Text="Export To Excel" />
                <div class="btn btn-danger  pull-right" data-toggle="modal"
                    data-target="#myModal3">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02121") %>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 button-section">
            </div>
            <div class="col-xs-1">
            </div>
            <div class="col-xs-5 button-section">

                <input type="button" class="btn btn-primary search-btn moveinerror pull-right" style="width: 169.5px; height: 48px; background-color: grey; border: 1px solid grey;" onclick="bootbox4()" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103012") %>" />
                <div class="btn btn-success  pull-right moveinok hidden" data-toggle="modal" style="width: 169.5px; font-size: 100%; height: 48px;"
                    data-target="#myModal4">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103012") %>
                </div>
            </div>
        </div>

        <div class="hidden">
            <asp:TextBox ID="TextBox3" runat="server" CssClass="moveid" Width="50%" />
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
                            <asp:TemplateField ItemStyle-CssClass="pad0" HeaderStyle-CssClass="centertext" HeaderText="">
                                <HeaderStyle Width="2%" Font-Size="70%"></HeaderStyle>
                                <HeaderTemplate>
                                    <input type="checkbox" class="movecheckall" onclick="moveinall()">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input type="checkbox" class="movecheck" onclick="movein('<%# Eval("number") %>')">
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="padcenter">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="lefttext padcenter">
                                <HeaderStyle Width="25%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="registerDate" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103015") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="padcenter">
                                <HeaderStyle Width="12%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Code" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="padcenter">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="idlvname" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="padcenter">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="status" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="padcenter">
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="preRegisterId" HeaderText="" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="movetxt pad0 smalltxt">
                                <HeaderStyle Width="1%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="" ItemStyle-CssClass="padcenter">
                                <ItemTemplate>
                                    <div class="btn-group pull-left">
                                        <button style="height: 44px; padding: 5px;" type="button" class="btn btn-primary btn-lg ddlconfig" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103029") %>&nbsp;
                 <span class="caret"></span>
                                            <span class="sr-only">Toggle Dropdown</span>
                                        </button>
                                        <ul class="dropdown-menu">
                                            <li><a href="/preRegister/preRegisterEdit.aspx?id=<%#Eval("preRegisterId")%>" style="font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103031") %></a></li>
                                            <li><a href="#" onclick="editModal('<%#Eval("Code")%>','<%#Eval("preRegisterId")%>')" data-toggle="modal" data-target="#myModal2" style="font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103164") %></a></li>
                                            <li><a href="/preRegister/registerPaper.aspx?id=<%#Eval("preRegisterId")%>&idSchool=<%#Eval("idSchool")%>&mode=1" onclick="toOffline(<%# Eval("preRegisterId") %>)" target="_blank" style="font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103034") %></a></li>
                                            <li><a target="_blank" href="/preRegister/registerPaper.aspx?id=<%#Eval("preRegisterId")%>&idSchool=<%#Eval("idSchool")%>&mode=2" style="font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103035") %></a></li>
                                            <li><a href="#" onclick="delModal('<%#Eval("Code")%>','<%#Eval("Name")%>','<%#Eval("preRegisterId")%>')" data-toggle="modal" data-target="#deletemodal" style="font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103036") %></a></li>
                                        </ul>

                                    </div>

                                </ItemTemplate>
                                <HeaderStyle Width="15%"></HeaderStyle>
                                <HeaderTemplate>
                                    <a target="_blank" href="/preRegister/preRegisterAddUser.aspx" class="btn-sm btn-success gvbutton"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103021") %></a>
                                </HeaderTemplate>
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


                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("number") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("registerYear") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("optionBranch") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("optionTime") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("optionCourse") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label63" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("planName") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("optionLevel") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sStudentID") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label64" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("examCode") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("cSex") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sName") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sNameEN") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sNickname") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="text" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Width="40px" CssClass="text" Text='<%# Eval("sStudentIdCardNumber") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("dBirth") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label28" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sStudentRace") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label29" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sStudentNation") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label30" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sStudentReligion") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label31" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("nSonNumber") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label32" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sStudentHomeNumber") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label33" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sStudentSoy") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label34" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sStudentMuu") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label35" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sStudentRoad") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label36" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sStudentProvince") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label37" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sStudentAumpher") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label38" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sStudentTumbon") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label39" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sStudentPost") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="text" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label40" runat="server" Width="40px" CssClass="text" Text='<%# Eval("sPhone") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label41" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sEmail") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label55" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("nWeight") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label56" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("nHeight") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label57" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sBlood") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label58" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sSickFood") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label59" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sSickDrug") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label60" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sSickOther") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label61" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sSickNormal") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label62" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sSickDanger") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label64" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("addDate") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label65" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("moveInDate") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label63" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("registerCode") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label66" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("nTermSubLevel2") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label67" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("paymentStatus") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label68" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("oldSchoolName") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label69" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("oldSchoolProvince") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label70" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("oldSchoolAumpher") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label71" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("oldSchoolTumbon") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="text" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label72" runat="server" Width="40px" CssClass="text" Text='<%# Eval("oldSchoolGPA") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label73" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("oldSchoolGraduated") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label74" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFamilyName") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="text" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label75" runat="server" Width="40px" CssClass="text" Text='<%# Eval("sFamilyIdCardNumber") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label76" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFamilyRace") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label77" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFamilyNation") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label78" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFamilyReligion") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label79" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFamilyHomeNumber") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFamilySoy") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label16" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFamilyMuu") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label17" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFamilyRoad") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label18" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFamilyProvince") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label19" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFamilyAumpher") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label20" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFamilyTumbon") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label21" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFamilyPost") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="text" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label22" runat="server" Width="40px" CssClass="text" Text='<%# Eval("sPhoneOne") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="text" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label23" runat="server" Width="40px" CssClass="text" Text='<%# Eval("sPhoneTwo") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="text" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label24" runat="server" Width="40px" CssClass="text" Text='<%# Eval("sPhoneThree") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label25" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFamilyRelate") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label26" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFatherName") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="text" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label27" runat="server" Width="40px" CssClass="text" Text='<%# Eval("sFatherIdCardNumber") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label42" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFatherRace") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label43" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFatherNation") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label44" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFatherReligion") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label45" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFatherHomeNumber") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label46" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFatherSoy") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label47" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFatherMuu") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label48" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFatherRoad") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label49" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFatherProvince") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label50" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFatherAumpher") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label51" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFatherTumbon") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label52" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFatherPost") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="text" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label53" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sFatherPhone") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label54" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("fatherIncome") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label137" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("Mothername") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="text" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label138" runat="server" Width="40px" CssClass="text" Text='<%# Eval("sMotherIdCardNumber") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label139" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sMotherRace") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label140" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sMotherNation") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label141" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sMotherReligion") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label142" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sMotherHomeNumber") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label143" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sMotherSoy") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label144" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sMotherMuu") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label145" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sMotherRoad") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label146" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sMotherProvince") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label147" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sMotherAumpher") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label148" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sMotherTumbon") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label149" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sMotherPost") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="text" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label150" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("sMotherPhone") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label151" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("motherIncome") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label152" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("knowFrom1") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label153" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("knowFrom2") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label154" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("knowFrom3") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label155" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("knowFrom4") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label156" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("knowFrom5") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label157" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("knowFrom6") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label158" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("knowFrom7") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label159" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("knowFrom8") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label160" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("knowFrom9") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label161" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("knowFrom10") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext33" HeaderStyle-CssClass="headerCell22">
                                <ItemTemplate>
                                    <asp:Label ID="Label162" runat="server" Width="40px" CssClass="centertext22" Text='<%# Eval("knowFrom11") %>'></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>

                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
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
                                <asp:TextBox ID="deleteid" runat="server" ClientIDMode="static" CssClass="form-control linkfrom deleteId" Width="50%" />
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox2" class="form-control deleteName" Enabled="false" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox1" class="form-control deleteCode" Enabled="false" runat="server" Width="80%"> </asp:TextBox>
                            </div>
                        </div>








                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="deleteBtn" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" />

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
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103164") %></h2>
                    </div>
                    <div class="modal-body">

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103164") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="ddlpayment" runat="server" CssClass="form-control ddlstatus" onchange="ddlchangestatus()">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103002") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103003") %>" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103004") %>" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103005") %>" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103012") %>" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-xs-12 ddlhidden hidden" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132788") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="editstudentid" class="form-control modalCode" runat="server"> </asp:TextBox>
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

                        <div class="col-xs-12 hidden" style="padding: 4px;">
                            <div class="col-xs-4 righttext">
                                <label>
                                    idlv2
                                </label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="editidlv2" runat="server" ClientIDMode="static" CssClass="form-control linkfrom modalidlv2" Width="50%" />
                            </div>
                        </div>

                        <div class="col-xs-12 ddlhidden hidden" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103160") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="editidlv" runat="server" onchange="ddlclass()" CssClass="ddl2 form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-xs-12 ddlhidden hidden" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="editilv2" runat="server" CssClass="ddl2 form-control ddlidlv2" onchange="ddlchange2()">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-xs-12 ddlhidden hidden" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103161") %></label>
                            </div>
                            <div class="col-xs-7" style="padding: 0px;">
                                <div class="col-xs-3" style="">
                                    <asp:DropDownList ID="editDay" runat="server" CssClass="width100 form-control modalbirth" onchange="ddlchangestatus2()">
                                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>" Value="-1" class="grey hidden"></asp:ListItem>
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
                                <div class="col-xs-5" style="padding: 0px">
                                    <asp:DropDownList ID="editMonth" runat="server" CssClass="width100 form-control modalbirth" onchange="ddlchangestatus2()">
                                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>" Value="-1" class="grey hidden"></asp:ListItem>
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
                                <div class="col-xs-4" style="">
                                    <asp:DropDownList ID="ddlyear" runat="server" CssClass="width100 form-control" Style="">
                                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>






                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">

                        <div class="col-xs-6"></div>
                        <div class="col-xs-3" style="padding: 0px;">
                            <input type="button" id="boot" class="btn btn-primary search-btn hidden modalok" style="width: 100px;" onclick="bootbox2()" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                            <input type="button" id="boot2" class="btn btn-primary search-btn hidden modalok" style="width: 100px;" onclick="saveclick()" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                            <input type="button" id="boot4" class="btn btn-primary search-btn hidden modalok" style="width: 100px;" onclick="saveclick2()" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                            <input type="button" id="boot3" class="btn btn-primary search-btn modalok" style="width: 100px;" onclick="bootbox3()" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                            <asp:Button CssClass="btn btn-primary global-btn import1 hidden modalok" ID="importSave" runat="server" Style="width: 100px;"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                            <asp:Button CssClass="btn btn-primary global-btn import2 hidden modalok" ID="importSave2" runat="server" Style="width: 100px;"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                        </div>
                        <div class="col-xs-3">
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
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103162") %></h2>
                    </div>
                    <div class="modal-body">

                        <div class="col-xs-12" style="padding: 5px;">

                            <div class="col-xs-12">
                                <asp:Label ID="genlink" class="form-control modalLink" runat="server"> </asp:Label>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 4px;">
                            <div class="col-xs-12 righttext">
                                <label>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103163") %>
                                </label>
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

        <div class="modal fade" id="myModal4" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103164") %></h2>
                    </div>
                    <div class="modal-body">



                        <div class="col-xs-12 hidden" style="padding: 4px;">
                            <div class="col-xs-4 righttext">
                                <label>
                                    idlv2
                                </label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="TextBox6" runat="server" ClientIDMode="static" CssClass="form-control linkfrom modalidlv2" Width="50%" />
                            </div>
                        </div>

                        <div class="col-xs-12 " style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103160") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="DropDownList4" runat="server" onchange="ddlclass_2()" CssClass="ddl2 form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="DropDownList5" runat="server" CssClass="ddl2 form-control ddlidlv2" onchange="ddlchange2_2()">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-xs-12 ddlhidden" style="padding: 5px;">
                            <div class="col-xs-4 righttext">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103161") %></label>
                            </div>
                            <div class="col-xs-7" style="padding: 0px;">
                                <div class="col-xs-3" style="">
                                    <asp:DropDownList ID="DropDownList6" runat="server" CssClass="width100 form-control modalbirth" onchange="ddlchangestatus3()">
                                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>" Value="-1" class="grey hidden"></asp:ListItem>
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
                                <div class="col-xs-5" style="padding: 0px">
                                    <asp:DropDownList ID="DropDownList7" runat="server" CssClass="width100 form-control modalbirth" onchange="ddlchangestatus3()">
                                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>" Value="-1" class="grey hidden"></asp:ListItem>
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
                                <div class="col-xs-4" style="">
                                    <asp:DropDownList ID="DropDownList8" runat="server" CssClass="width100 form-control" Style="">
                                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>






                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">

                        <div class="col-xs-6"></div>
                        <div class="col-xs-3" style="padding: 0px;">
                            <input type="button" id="booterror2" class="btn btn-primary search-btn  modalok2" style="width: 100px;" onclick="bootbox2()" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />

                            <asp:Button CssClass="btn btn-primary global-btn import1 hidden modalok2" ID="Button1" runat="server" Style="width: 100px;"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                        </div>
                        <div class="col-xs-3">
                            <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>


</asp:Content>
