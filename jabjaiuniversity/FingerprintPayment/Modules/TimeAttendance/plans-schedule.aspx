<%@ Page Title="" Language="C#" MasterPageFile="~/mp3.Master" AutoEventWireup="true"
    CodeBehind="plans-schedule.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.plans_schedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="/bootstrap/bootstrap-chosen/chosen.jquery.js" type="text/javascript"></script>
    <link href="/bootstrap/bootstrap-chosen/bootstrap-chosen.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="/assets/plugins/datatables/jquery.dataTables.min.css" />
    <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>
    <script src="../../javascript/PlansScheduleJS.js?<%= DateTime.Now.ToString("ddMMyyyyHHmmss") %>" type="text/javascript"></script>
    <%-- <script src="../../Scripts/validator.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <%--<script src="../../Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>--%>

    <%--<script src="../../Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/bootstrap-toggle.js"></script>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <link href="/Content/bootstrap-toggle.css" rel="stylesheet" />
    <link rel="stylesheet" href="../../Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="../../Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <%--   <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.8.0/jszip.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.8.0/xlsx.js"></script>--%>
    <script type="text/javascript" charset="utf8" src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" language="javascript">

        function fncsave() {
            var copyyear = document.getElementsByClassName("copyyear");
            var copyterm = document.getElementsByClassName("copyterm");
            var copyroom = document.getElementsByClassName("copyroom");

            var full = window.location.href;
            var half = full.split('?');
            var split = half[1].split('&');
            var id = split[0];
            var idterm = split[1];
            //alert("/App_Logic/copyPlan.ashx?" + id + "&" + idterm + "&yeartxt=" + copyyear[0].value + "&termtxt=" + copyterm[0].value + "&roomtxt=" + copyroom[0].value);
            console.log(copyterm[0].value);
            $.ajax({
                url: "/App_Logic/copyPlan.ashx?" + id + "&" + idterm + "&yeartxt=" + copyyear[0].value + "&termtxt=" + $("#ctl00_MainContent_DropDownList2").val() + "&roomtxt=" + copyroom[0].value,
                //contentType: "application/json; charset=utf-8",
                //dataType: "json",
                success: function (data) {
                    //alert(data);
                    var errorMessage = document.getElementsByClassName("ErrorMessage");
                    if (data == "") {
                        for (i = 0; i < errorMessage.length; i++) {
                            errorMessage[0].classList.add('hidden');
                            errorMessage[0].innerHTML = "";
                        }
                        window.location.reload();
                    }
                    else {
                        for (i = 0; i < errorMessage.length; i++) {
                            errorMessage[0].classList.remove('hidden');
                            errorMessage[0].innerHTML = data;
                        }
                    }
                },
                error: function (data) {
                    window.location.reload();
                },
            });

            return false;
        }

        function ddl() {
            ddlyear();


        }
        function ddlyear() {
            var ddl1 = document.getElementsByClassName("ddl1");
            var ddl2 = document.getElementsByClassName("ddl2");
            var select = document.getElementById('DD1');
            var copyyear = document.getElementsByClassName("copyyear");
            var copyterm = document.getElementsByClassName("copyterm");


            for (i = -1; i <= 5; i++) {
                ddl1[1].remove(0);
            }

            $.get("/App_Logic/ddlterm.ashx?year=" + ddl1[0].options[ddl1[0].selectedIndex].value, function (Result) {
                $.each(Result, function (index) {

                    // Create an Option object       
                    var opt = document.createElement("option");

                    // Assign text and value to Option object
                    opt.text = Result[index].value;
                    opt.value = Result[index].value;

                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=DropDownList2.ClientID%>').options.add(opt);


                });

            });
            console.log("Test");
            setTimeout(function () {
                copyyear[0].value = ddl1[0].value;
                copyterm[0].value = ddl1[1].value;
                ddlclass();
            }, 1000);
        }

        function ddlterm() {
            var ddl1 = document.getElementsByClassName("ddl1");
            var ddl2 = document.getElementsByClassName("ddl2");
            var select = document.getElementById('DD1');
            var copyyear = document.getElementsByClassName("copyyear");
            var copyterm = document.getElementsByClassName("copyterm");

            copyyear[0].value = ddl1[0].value;
            copyterm[0].value = ddl1[1].value;
        }

        function ddlclass() {
            var ddl1 = document.getElementsByClassName("ddl1");
            var ddl2 = document.getElementsByClassName("ddl2");
            var copyroom = document.getElementsByClassName("copyroom");

            for (i = -1; i <= 20; i++) {
                ddl2[1].remove(0);
            }

            $("#<%=ddlsublevel2.ClientID%> option").remove();

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
            });


            setTimeout(function () {
                var noclick = document.getElementsByClassName("noclick");
                copyroom[0].value = ddl2[1].value;
                for (i = 0; i < noclick.length; i++) {
                    noclick[0].classList.remove('noclick');
                }
            }, 1000);
        }

        function ddlroom() {
            var ddl1 = document.getElementsByClassName("ddl1");
            var ddl2 = document.getElementsByClassName("ddl2");
            var copyroom = document.getElementsByClassName("copyroom");

            copyroom[0].value = ddl2[1].value;
        }

    </script>
    <style type="text/css">
        @media (min-width: 768px) {
            .modal-dialog {
                width:900px !important;
            }
        }
        .ui-autocomplete {
            max-height: 200px;
            overflow-y: auto; /* prevent horizontal scrollbar */
            overflow-x: hidden;
            z-index: 1060 !important;
        }
        /* IE 6 doesn't support max-height
   * we use height instead, but this forces the menu to always be this tall
   */
        * html .ui-autocomplete {
            height: 100px;
        }

        .ui-autocomplete-loading {
            background: white url("images/ui-anim_basic_16x16.gif") right center no-repeat;
        }

        #city {
            width: 25em;
        }

        .noclick {
            pointer-events: none;
        }
        /* Styles for custom tooltip template */
        .tooltip-head {
            color: #fff;
            background: #000;
            padding: 10px 10px 5px;
            border-radius: 4px 4px 0 0;
            text-align: center;
            margin-bottom: -2px; /* Hide default tooltip rounded corner from top */
        }

            .tooltip-head .glyphicon {
                font-size: 22px;
                vertical-align: bottom;
            }

            .tooltip-head h3 {
                margin: 0;
                font-size: 18px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="report-card box-content plans-schedule-container hidden" id="div1">
        <div class="row">
            <div class="col-lg-offset-1 col-lg-2 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>
            </div>
            <div class="col-lg-8 col-md-8 col-xs-8" style="vertical-align: text-bottom;">
            </div>
        </div>
        <div class="row">
            <div class="col-lg-offset-1 col-lg-2 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132624") %>
            </div>
            <div class="col-lg-8 col-md-8 col-xs-8" style="vertical-align: text-bottom;">
            </div>
        </div>
        <div class="row">
            <div class="col-lg-offset-1 col-lg-2 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>
            </div>
            <div class="col-lg-8 col-md-8 col-xs-8" style="vertical-align: text-bottom;">
            </div>
        </div>
        <div class="row">
            <div class="col-lg-offset-1 col-lg-2 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210005") %>
            </div>
            <div class="col-lg-8 col-md-8 col-xs-8" style="vertical-align: text-bottom;">
            </div>
        </div>
        <div class="row--space"></div>
        <div class="row button-section">
            <div class="col-lg-offset-1 col-lg-2 col-md-offset-1 col-md-3 col-xs-offset-1 col-xs-3">
            </div>
            <div class="col-lg-8 col-md-8 col-xs-8" style="vertical-align: text-bottom;">
                <input type="button" class="btn btn-success btn-large-size global-btn" id="saveteacher" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %>" />
            </div>
        </div>
    </div>
    <div class="report-card box-content" id="div2" style="font-size: 24px;">
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-4">
                <table border="1" style="width: 100%">
                    <tr style="border-bottom: none;">
                        <td style="width: 20%;"></td>
                        <td style="width: 20%;"></td>
                        <td style="width: 20%;"></td>
                        <td style="width: 20%;"></td>
                        <td style="width: 20%;"></td>
                    </tr>
                    <tr>
                        <td colspan="5" style="padding: 5px; background-color: #347ab7; text-align: center; color: #ffffff;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132625") %>                
                            <asp:Literal ID="ltrYear" runat="server" />
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210456") %>
                            <asp:Literal ID="ltrTerm" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-left: 5px; background-color: #347ab7; color: #ffffff;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></td>
                        <td colspan="3" style="padding-left: 5px; text-align: center;">
                            <asp:Literal ID="ltrLevel" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-left: 5px; background-color: #347ab7; color: #ffffff;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></td>
                        <td colspan="3" style="padding-left: 5px; text-align: center;">
                            <asp:Literal ID="ltrSubLv" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-left: 5px; background-color: #347ab7; color: #ffffff;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202006") %></td>
                        <td colspan="3" style="vertical-align: middle;">
                            <div class="row" style="margin-bottom: 2px;">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="input-group" data-placement="left" data-align="top" data-autoclose="true">
                                        <input type="text" id="txtaddteacher" runat="server" class="form-control" style="width: 400px; display: none;" />
                                        <input type="text" id="txtaddteacherid" runat="server" class="form-control" style="width: 400px; display: none;" />
                                        <select data-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202020") %>" class="chosen-select" id="selectteacherclass" tabindex="2" style="margin-bottom: 0px;">
                                            <option value=""></option>
                                        </select>
                                        <span class="input-group-addon" id="btndel" style="cursor: pointer;">
                                            <span class="glyphicon glyphicon-remove"></span>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2">
                <div class="panel panel-green panel-summary hidden">
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6">
                <table border="1" style="width: 100%; font-size: 16px;" id="tableplans">
                    <thead style="border-bottom: none;">
                        <tr class="hidden">
                            <th style="width: 30%;"></th>
                            <th style="width: 35%;"></th>
                            <th style="width: 35%;"></th>
                            <th style="width: 30%;"></th>
                            <th style="width: 35%;"></th>
                            <th style="width: 35%;"></th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12" style="vertical-align: text-bottom;">
                <input type="button" class="btn btn-success btn-large-size" id="save" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202014") %>" onclick="Clear();" data-toggle="modal" data-target="#myModal" />
                <input type="button" class="btn btn-success btn-large-size" id="copy" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202015") %>" onclick="ddl();" data-toggle="modal" data-target="#myModal2" />
                <input type="button" class="btn btn-danger btn-large-size" id="import" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202016") %>" onclick="ddl();" data-toggle="modal" data-target="#ImportModal" />
            </div>
        </div>
        <div class="row hidden">
            <div class="col-lg-12 col-md-12 col-sm-12" style="vertical-align: text-bottom;">
                <br />
                <div style="margin-top: -20px;" id="timetable">
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12" style="vertical-align: text-bottom;">
                <br />
                <div style="margin-top: -20px;">
                    <table border="1" width="1050px" style="border-collapse: inherit !important;">
                        <tr style="border-bottom: none; display: none;">
                            <td style="width: 10%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                            <td style="width: 1%;"></td>
                        </tr>
                        <tr>
                            <td class="centerText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %><div>
                                <div id="span1" class="t"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %></div>
                            </div>
                            </td>
                            <td class="centerText" colspan="6">06:00 -
                                07:00&nbsp;&nbsp; </td>
                            <td class="centerText" colspan="6">07:00 -
                                08:00&nbsp;&nbsp;</td>
                            <td class="centerText" colspan="6">08:00 -
                                09:00&nbsp;&nbsp;</td>
                            <td class="centerText" colspan="6">09:00 -
                                10:00&nbsp;&nbsp;</td>
                            <td class="centerText" colspan="6">10:00 -
                                11:00&nbsp;&nbsp;</td>
                            <td class="centerText" colspan="6">11:00 -
                                12:00&nbsp;&nbsp;</td>
                            <td class="centerText" colspan="6">12:00 -
                                13:00&nbsp;&nbsp;</td>
                            <td class="centerText" colspan="6">13:00 -
                                14:00&nbsp;&nbsp;</td>
                            <td class="centerText" colspan="6">14:00 -
                                15:00&nbsp;&nbsp;</td>
                            <td class="centerText" colspan="6">15:00 -
                                16:00&nbsp;&nbsp;</td>
                            <td class="centerText" colspan="6">16:00 -
                                17:00&nbsp;&nbsp;</td>
                            <td class="centerText" colspan="6">17:00 -
                                18:00&nbsp;&nbsp;</td>
                            <td class="centerText" colspan="6">18:00 -
                                19:00&nbsp;&nbsp;</td>
                            <td class="centerText" colspan="6">19:00 -
                                20:00&nbsp;&nbsp;</td>
                            <td class="centerText" colspan="6">20:00 -
                                21:00&nbsp;&nbsp;</td>
                        </tr>
                        <tr id="rowmo">
                            <td style="background-color: #fdb423; text-align: center; color: #000000;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202007") %></td>
                        </tr>
                        <tr id="rowtu">
                            <td style="background-color: #f38ab9; text-align: center; color: #000000;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202008") %></td>
                        </tr>
                        <tr id="rowwe">
                            <td style="background-color: #6bbd48; text-align: center; color: #000000;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202009") %></td>
                        </tr>
                        <tr id="rowth">
                            <td style="background-color: #f27924; text-align: center; color: #000000;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202010") %></td>
                        </tr>
                        <tr id="rowfr">
                            <td style="background-color: #4ab5e6; text-align: center; color: #000000;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202011") %></td>
                        </tr>
                        <tr id="rowsa">
                            <td style="background-color: #8653a0; text-align: center; color: #000000;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202012") %></td>
                        </tr>
                        <tr id="rowsu">
                            <td style="background-color: #ee3230; text-align: center; color: #000000;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202013") %></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document" style="width: 700px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title" id="myModalLabel">Modal title</h3>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-8" style="vertical-align: text-bottom;">
                                <%--   <input id="planeid" type="text" class="form-control" style="width: 400px; display: none;" />
                                <%--<input id="plane" type="text" class="form-control" style="width: 400px;" />--%>
                                <select data-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202018") %>" class="chosen-select" id="selectplane" tabindex="2">
                                    <option value=""></option>
                                </select>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202019") %>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-8" style="vertical-align: text-bottom;">
                                <select data-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202020") %>" class="chosen-select disabled" id="selectteacher" tabindex="2">
                                    <option value=""></option>
                                </select>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-8" style="vertical-align: text-bottom;">
                                <%--  <input id="roomid" type="text" class="form-control" style="width: 400px; display: none;" />
                                <input id="room" type="text" class="form-control" style="width: 400px;" />--%>
                                <select data-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101281") %>" class="chosen-select" id="selectroom" tabindex="2">
                                    <option value=""></option>
                                </select>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202022") %>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-8" style="vertical-align: text-bottom;">
                                <%--  <input id="roomid" type="text" class="form-control" style="width: 400px; display: none;" />
                                <input id="room" type="text" class="form-control" style="width: 400px;" />--%>
                                <label id="lblday"></label>
                                <select class="form-control addvalues" id="selectday" tabindex="2">
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202023") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202024") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202025") %></option>
                                    <option value="4">วันพฤหัสบดี</option>
                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202027") %></option>
                                    <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M406040") %></option>
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202028") %></option>
                                </select>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202029") %>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4" style="vertical-align: text-bottom;">
                                <label id="lbltimein"></label>
                                <div class="input-group clockpicker addvalues" data-placement="left" data-align="top" data-autoclose="true">
                                    <input type="text" class="form-control mon clock-box" id="timein" name="timein" placeholder="00:00" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4" style="vertical-align: text-bottom;">
                                <label id="lbltimeout"></label>
                                <div class="input-group clockpicker addvalues" data-placement="left" data-align="top" data-autoclose="true">
                                    <input type="text" class="form-control mon clock-box" id="timeout" name="timeout" placeholder="00:00" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202030") %>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-8">
                                <label class="checkbox-inline bottom">
                                    <input type="checkbox" class="col-sm-12" id="calculate" data-toggle="toggle" data-onstyle="info" data-width="170" data-height="45"
                                        data-offstyle="danger" data-on="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202031") %>" data-off="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202032") %>">
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                        <%--<div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132631") %>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-8">
                                <label class="checkbox-inline bottom">
                                    <input class="bottom" type="checkbox" checked style="position: static;" name="active" id="active" value="" /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206116") %></label>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4" style="vertical-align: text-bottom;">
                                <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                    <input type="text" class="form-control mon clock-box" id="timeinstart" name="timeinstart" placeholder="00:00" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4" style="vertical-align: text-bottom;">
                                <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                    <input type="text" class="form-control mon clock-box" id="timeinend" name="timeinend" placeholder="00:00" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4" style="vertical-align: text-bottom;">
                                <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                    <input type="text" class="form-control mon clock-box" id="timeoutstart" name="timeoutstart" placeholder="00:00" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4" style="vertical-align: text-bottom;">
                                <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                    <input type="text" class="form-control mon clock-box" id="timeoutend" name="timeoutend" placeholder="00:00" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132626") %>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4" style="vertical-align: text-bottom;">
                                <div class="input-group" data-placement="left" data-align="top" data-autoclose="true">
                                    <input type="text" class="form-control mon clock-box" id="timelate" name="timelate" type="number" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-plus-sign"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4" style="vertical-align: text-bottom;">
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132632") %>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4" style="vertical-align: text-bottom;">
                                <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                    <input type="text" class="form-control mon clock-box" id="timehalf" name="timehalf" placeholder="00:00" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4" style="vertical-align: text-bottom;">
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>--%>
                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-primary" style="width: 100px;" id="add"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %></button>
                        <input type="button" class="btn btn-danger hidden" style="width: 100px;" id="btndelete" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" />
                        <input type="button" class="btn btn-default" style="width: 100px;" id="btncancel" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" onclick="Clear();"
                            data-dismiss="modal" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="myModal2" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202015") %></h2>
                </div>
                <div class="modal-body">
                    <div class="col-xs-12" style="padding: 3px;">
                        <label id="ErrorMessage" class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label hidden label-warning ErrorMessage" style="width: 100%; font-size: 170%">
                        </label>
                    </div>
                    <div class="col-xs-12" style="padding: 3px;">
                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label" style="font-size: 170%">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                            <asp:DropDownList ID="DropDownList1" onchange="ddlyear()" runat="server" Style="font-size: 24px" CssClass="ddl1 form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12" style="padding: 3px;">
                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label" style="font-size: 170%">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                            <asp:DropDownList ID="DropDownList2" onchange="ddlterm()" runat="server" Style="font-size: 24px" CssClass="ddl1 form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12" style="padding: 3px;">
                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label" style="font-size: 170%">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                            <asp:DropDownList ID="ddlsublevel" onchange="ddlclass()" runat="server" Style="font-size: 24px" CssClass="ddl2 form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12" style="padding: 3px;">
                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label" style="font-size: 170%">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                            <asp:DropDownList ID="ddlsublevel2" runat="server" onchange="ddlroom()" Style="font-size: 24px" CssClass="ddl2 form-control">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group row student hidden">
                        <div class="col-md-6 col-sm-12">
                            <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                                year</label>
                            <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                                <asp:TextBox ID="yeartxt" runat="server" CssClass="copyyear" Width="50%"> </asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="form-group row student hidden">
                        <div class="col-md-6 col-sm-12">
                            <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                                term</label>
                            <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                                <asp:TextBox ID="termtxt" runat="server" CssClass="copyterm" Width="50%"> </asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="form-group row student hidden">
                        <div class="col-md-6 col-sm-12">
                            <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                                room</label>
                            <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                                <asp:TextBox ID="roomtxt" runat="server" CssClass="copyroom" Width="50%"> </asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="hid" style="font-size: 30%; visibility: hidden">hidden</div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary noclick" onclick="fncsave();return false" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ImportModal" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202016") %></h2>
                </div>
                <div class="modal-body">
                    <div class="col-xs-12" style="padding: 3px;">
                        <label id="ErrorMessage" class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label hidden label-warning ErrorMessage" style="width: 100%; font-size: 170%">
                        </label>
                    </div>
                    <div class="col-xs-12" style="padding: 3px;">
                        <label class="col-lg-2 col-md-4 col-sm-4 col-xs-4 control-label" style="font-size: 170%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202042") %></label>
                        <div class="col-lg-10 col-md-8 col-sm-8 col-xs-8 control-input">
                            <div class="form-group">
                                <form enctype="multipart/form-data">
                                    <div class="input-group input-file" name="Fichier1">
                                        <span class="input-group-btn">
                                            <button class="btn btn-default btn-choose" type="button">Choose</button>
                                        </span>
                                        <input type="text" class="form-control col-lg-12" style="height: 47.6px" placeholder='Choose a file...' />
                                        <span class="input-group-btn">
                                            <button class="btn btn-warning btn-reset" type="button">Reset</button>
                                        </span>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12" style="padding: 3px;" id="InValidData">
                    </div>
                    <div class="col-xs-12" style="padding: 3px; font-size: 20px">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202043") %>
                        <br />
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202044") %>
                        <br />
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202045") %> (<a href="#" data-toggle="modal" data-target="#SampleDoc"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202046") %></a>)<br />
                    </div>



                    <div style="font-size: 30%; visibility: hidden">hidden</div>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-primary noclick" onclick="fncsave();return false" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>--%>
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="SampleDoc" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" style="width:820px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>

                </div>
                <div class="modal-body" style="width:820px">
                    <img src="../../images/TimeTableSample.png" />
                </div>
                <div class="modal-footer">

                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>

        </div>
    </div>

    <div class="modal fade" id="ShowExtractedDataFromFile" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 1100px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h2 class="modal-title">โปรดตรวจสอบข้อมูล</h2>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12" style="width: 99%; font-size: 18px !important; height: 380px; overflow-y: scroll">
                            <div class="col-xs-12" style="width: 99%; padding: 3px;" id="InValidExtractData">
                            </div>

                            <table id="ExtractedData" class="display" cellspacing="0" width="100%;height:350px">
                                <thead>
                                    <tr>
                                        <th style="text-align: left; min-width: 10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>
                                        </th>
                                        <th style="text-align: left; min-width: 30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %></th>
                                        <th style="text-align: left; min-width: 30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132628") %></th>
                                        <th style="text-align: left; min-width: 30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132629") %></th>
                                        <th style="text-align: left; min-width: 20%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132630") %></th>
                                        <th style="text-align: left; min-width: 10%; white-space: nowrap"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101296") %></th>
                                        <th style="text-align: center; min-width: 5%"></th>
                                        <th style="text-align: center; min-width: 5%"></th>
                                        <th style="text-align: center; min-width: 5%"></th>
                                    </tr>
                                </thead>

                            </table>

                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12" style="width: 93%;">
                            <button type="button" class="btn btn-primary" id="SaveExtractedData" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>
                            <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
