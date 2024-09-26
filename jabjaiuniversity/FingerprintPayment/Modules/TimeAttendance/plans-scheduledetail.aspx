<%@ Page Title="" Language="C#" MasterPageFile="~/mp3.Master" AutoEventWireup="true"
    CodeBehind="plans-scheduledetail.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.plans_scheduledetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../app/plans/PlansScheduleDetailS.js" type="text/javascript"></script>
    <style type="text/css">
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
    <div class="report-card box-content plan-schedule-detail-container">
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
                            <asp:Literal ID="ltrTerm" runat="server" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-left: 5px; background-color: #347ab7; color: #ffffff;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></td>
                        <td colspan="3" style="padding-left: 5px; text-align: center;">
                            <asp:Literal ID="ltrLevel" runat="server" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-left: 5px; background-color: #347ab7; color: #ffffff;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></td>
                        <td colspan="3" style="padding-left: 5px; text-align: center;">
                            <asp:Literal ID="ltrSubLv" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-left: 5px; background-color: #347ab7; color: #ffffff;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202006") %></td>
                        <td colspan="3" class="center" style="vertical-align: middle;">
                            <asp:Literal ID="ltrTeacher" runat="server" />
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
                    <table border="1" width="1050px">
                        <tr style="border-bottom: none;">
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
