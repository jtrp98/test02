<%@ Page Title="" Language="C#" MasterPageFile="~/Material.Master" AutoEventWireup="true" CodeBehind="settingMainpage.aspx.cs" Inherits="FingerprintPayment.Setting.settingMainpage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--<link href="/Styles/jquery-ui.css" rel="stylesheet" />--%>

    <style>
        .hidden {
            display: none !important;
        }

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

        .modal-content {
            width: inherit;
            height: inherit;
            margin: 0 auto;
        }

       /* .modal-body {
            font-size: 26px;
        }*/

        .btn.btn-success, .btn.btn-cancel, .btn.btn-danger, .btn.btn-primary {
            width: 110px;
            height: 44px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">

        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">settings</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206151") %></h4>
                </div>
                <div class="card-body ">
                    <div class="col-md-12">
                        <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                            GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            OnDataBound="CustomersGridView_DataBound"
                            Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="table table-striped table-no-bordered table-hover">
                            <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                            <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                                BackColor="#337AB7" />

                            <PagerTemplate>

                                <table width="100%" class="tab">
                                    <tr>
                                        <td style="width: 25%">

                                            <asp:Label ID="Label11" BorderColor="#337AB7"
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
                                <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                    <HeaderStyle Width="10%"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="name" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302015") %>" ItemStyle-CssClass="lefttext" HeaderStyle-CssClass="centertext">
                                    <HeaderStyle Width="45%"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="status" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>" ItemStyle-CssClass="centertext status" HeaderStyle-CssClass="centertext">
                                    <HeaderStyle Width="15%"></HeaderStyle>
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-lg-2 adjust-col-padding col-space nounder" style="margin-top: 15px;">
                                                <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00273") %>">
                                                    <div class="fa fa-toggle-on statusON hidden" onclick="ToOffline(<%# Eval("click") %>)" style="font-size: 100%; cursor: pointer; color: green;">
                                                    </div>
                                                </div>
                                                <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00274") %>">
                                                    <div class="fa fa-toggle-off statusOFF hidden" onclick="ToOnline(<%# Eval("click") %>)" style="font-size: 100%; cursor: pointer;">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-10">
                                                <a target="_blank" href="settingTimePeriod.aspx" class="btn btn-warning button1 hidden"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206154") %> </a>
                                                <a target="_blank" href="settingGradeAdmin.aspx" class="btn btn-warning button3 hidden"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206166") %> </a>
                                                <a target="_blank" href="settingTeacherExtraTime.aspx" class="btn btn-warning button4 hidden"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206166") %> </a>
                                                <a target="_blank" href="/grade/SchoolReportTeacherDescribe.aspx" class="btn btn-warning button5 hidden"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206175") %> </a>
                                                <a target="_blank" href="/Pages/Settings/GradeViewSetting.aspx" class="btn btn-warning button6 hidden"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303007") %> </a>
                                                <a target="_blank" href="#" data-toggle="modal" data-target="#modalSetCostGradeRepair" onclick="return InitModalManageStudentTitle();" class="btn btn-warning button7 hidden"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303007") %> </a>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30%"></HeaderStyle>
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
    </div>


    <div id="modalSetCostGradeRepair" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 10%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="width: 500px;">

                <div class="modal-header" style="padding: 0px 15px; top: 25%;">
                    <h5 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206185") %></h5>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-7 col-form-label">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211057") %>
                            </div>
                            <div class="col-md-3">
                                <input id="iptCostGradeRepairMid" name="iptCostGradeRepairMid" type="text" class="form-control" style="padding-left: 7px;" maxlength="6" />
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row" style="margin-top: 7px;">
                            <div class="col-md-1"></div>
                            <div class="col-md-7">
                                <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206185") %></b>
                            </div>
                            <div class="col-md-3">
                                <input id="iptCostGradeRepairFinal" name="iptCostGradeRepairFinal" type="text" class="form-control" style="padding-left: 7px;" maxlength="6" />
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnSaveCostGradeRepair" class="btn btn-success global-btn">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    <button type="button" class="btn btn-cancel global-btn" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <%--<script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>

    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        function ToOnline(index) {
            var statusON = document.getElementsByClassName("statusON");
            var statusOFF = document.getElementsByClassName("statusOFF");
            var status = document.getElementsByClassName("status");

            if (index == 1) {
                statusON[0].classList.remove('hidden');
                statusOFF[0].classList.add('hidden');
                status[0].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206116") %>";
                $.ajax({
                    url: "/Setting/settingMainpage.ashx?mode=1",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                });
            }
            else if (index == 2) {
                statusON[1].classList.remove('hidden');
                statusOFF[1].classList.add('hidden');
                status[1].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206116") %>";
                $.ajax({
                    url: "/Setting/settingMainpage.ashx?mode=3",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                });
            }
            else if (index == 3) {
                statusON[3].classList.remove('hidden');
                statusOFF[3].classList.add('hidden');
                status[3].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206116") %>";
                $.ajax({
                    url: "/Setting/settingMainpage.ashx?mode=5",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                });
            }
            else if (index == 5) {
                statusON[2].classList.remove('hidden');
                statusOFF[2].classList.add('hidden');
                status[2].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206116") %>";
                $.ajax({
                    url: "/Setting/settingMainpage.ashx?mode=7",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                });
            }

        }

        function ToOffline(index) {

            var statusON = document.getElementsByClassName("statusON");
            var statusOFF = document.getElementsByClassName("statusOFF");
            var status = document.getElementsByClassName("status");

            if (index == 1) {
                statusON[0].classList.add('hidden');
                statusOFF[0].classList.remove('hidden');
                status[0].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206161") %>";

                $.ajax({
                    url: "/Setting/settingMainpage.ashx?mode=2",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                });
            }
            else if (index == 2) {
                statusON[1].classList.add('hidden');
                statusOFF[1].classList.remove('hidden');
                status[1].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206161") %>";

                $.ajax({
                    url: "/Setting/settingMainpage.ashx?mode=4",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                });
            }
            else if (index == 3) {
                statusON[3].classList.add('hidden');
                statusOFF[3].classList.remove('hidden');
                status[3].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206161") %>";

                $.ajax({
                    url: "/Setting/settingMainpage.ashx?mode=6",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                });
            }
            else if (index == 5) {
                statusON[2].classList.add('hidden');
                statusOFF[2].classList.remove('hidden');
                status[2].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206161") %>";

                $.ajax({
                    url: "/Setting/settingMainpage.ashx?mode=8",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                });
            }
        }

        var initCostGradeRepairMid = '<%= CostGradeRepairMid %>';
        var initCostGradeRepairFinal = '<%= CostGradeRepairFinal %>';

        function InitModalManageStudentTitle() {

            $("#iptCostGradeRepairMid").val(initCostGradeRepairMid);
            $("#iptCostGradeRepairFinal").val(initCostGradeRepairFinal);

            return false;
        }

        $(document).ready(function () {
            var button1 = document.getElementsByClassName("button1");
            var button2 = document.getElementsByClassName("button2");
            var button3 = document.getElementsByClassName("button3");
            var button4 = document.getElementsByClassName("button4");
            var button5 = document.getElementsByClassName("button5");
            var button6 = document.getElementsByClassName("button6");
            var button7 = document.getElementsByClassName("button7");
            var statusON = document.getElementsByClassName("statusON");
            var statusOFF = document.getElementsByClassName("statusOFF");
            var status = document.getElementsByClassName("status");
            var itemCell = document.getElementsByClassName("itemCell");

            itemCell[1].classList.add('hidden');
            button1[0].classList.remove('hidden');
            button3[3].classList.remove('hidden');
            statusON[4].classList.add('hidden');
            statusOFF[4].classList.add('hidden');
            button4[4].classList.remove('hidden');
            button5[5].classList.remove('hidden');
            button6[6].classList.remove('hidden');
            button7[7].classList.remove('hidden');

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
                window.location.href = "GradeRoomList.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&year=" + param3var + "&term=" + param4var + "&mode=" + mode[0].value;


            });

            $.get("/Setting/settingStartUp.ashx", function (Result) {
                $.each(Result, function (index) {

                    if (Result[index].settingGradeAdmin == null || Result[index].settingGradeAdmin == 0) {
                        statusOFF[3].classList.remove('hidden');
                        status[3].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206161") %>";
                    }
                    else {
                        statusON[3].classList.remove('hidden');
                        status[3].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206116") %>";
                    }

                    if (Result[index].settingPlanTeacher == null || Result[index].settingPlanTeacher == 1) {
                        statusON[1].classList.remove('hidden');
                        status[1].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206116") %>";
                    }
                    else {
                        statusOFF[1].classList.remove('hidden');
                        status[1].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206161") %>";
                    }

                    if (Result[index].settingReportView == null || Result[index].settingReportView == 0) {
                        statusOFF[2].classList.remove('hidden');
                        status[2].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206161") %>";
                    }
                    else {
                        statusON[2].classList.remove('hidden');
                        status[2].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206116") %>";
                    }

                    if (Result[index].settingTimePeriod == null || Result[index].settingTimePeriod == 0) {
                        statusOFF[0].classList.remove('hidden');
                        status[0].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206161") %>";
                    }
                    else {
                        statusON[0].classList.remove('hidden');
                        status[0].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206116") %>";
                    }

                });
            });

            $('#btnSaveCostGradeRepair').click(function () {
                $("#modalSetCostGradeRepair").modal('hide');

                var costData = {
                    "costGradeRepairMid": $("#iptCostGradeRepairMid").val(),
                    "costGradeRepairFinal": $("#iptCostGradeRepairFinal").val()
                }

                /* $('#modalWaitDialog').css('z-index', parseInt($('#modalSetCostGradeRepair').css('z-index')) + 1);*/
                /*$("#modalWaitDialog").modal('show');*/
                /*  $("#modalWaitDialog").modal('hide');*/

                $.ajax({
                    async: true,
                    type: "POST",
                    url: "settingMainpage.aspx/SaveCostGradeRepair",
                    data: "{costData:" + JSON.stringify(costData) + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var title = "";
                        var body = "";
                        console.log(response.d);
                        switch (response.d) {
                            case "success":
                                /*$("#modalWaitDialog").modal('hide');*/
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206186") %>';

                                $('#modalShowPassword').modal('hide');

                                initCostGradeRepairMid = $("#iptCostGradeRepairMid").val();
                                initCostGradeRepairFinal = $("#iptCostGradeRepairFinal").val();

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133251") %>';

                                break;
                        }

                        /* $("#modalWaitDialog").modal('hide');*/

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                        $("#modalNotifyOnlyClose").modal('show');

                        //$("#modalWaitDialog").modal('hide');
                    },
                    failure: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                        $("#modalWaitDialog").modal('hide');
                    },
                    error: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                        $("#modalWaitDialog").modal('hide');
                    }
                });

                return false;
            });

            $('#iptCostGradeRepairMid, #iptCostGradeRepairFinal').number(true, 2);

        });

        function bootbox2() {
            bootbox.alert({
                message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206412") %></h3>',
                backdrop: true
            });
        }

        //window.onload = start;

    </script>

</asp:Content>
