<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="subleveladdtable.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.subleveladdtable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <style>
        .nav-tabs > li.active > a, .nav-tabs > li.active > a:hover, .nav-tabs > li.active > a:focus {
            background-color: #fff585;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center;" class="timeiosetting-container subleveladdtable-container">
        <asp:RadioButtonList ID="rblTimeType" runat="server" class="tab-pane active" RepeatDirection="Horizontal"
            Style="display: none;" />
        <asp:Literal ID="ltrTabHeader" runat="server" />
        <div class="row hidden">
            <div class="col-lg-2 text-left" style="font-size: 30px; margin-left: 58px;">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101281") %></label>
            </div>
            <div class="col-lg-4 text-left" style="font-size: 30px;">
                <asp:DropDownList ID="ddlnTermSubLevel2" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-lg-6 text-left" style="font-size: 30px;">
            </div>
        </div>
        <div class="timeiosetting-container">
            <table align="center" class="table table-striped time-table timeiosettings-table" border="1" style="width: 100%; font-weight: bolder">
                <thead>
                    <tr style="background: #337AB7; color: #fff;">
                        <%--   <td style="width: 12%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132589") %>
                    </td>--%>
                        <td style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>
                        </td>
                        <td style="width: 30%; text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %>
                        </td>
                        <td style="width: 15%; text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803014") %>
                        </td>
                        <td style="width: 10%; text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803015") %>
                        </td>
                        <td style="width: 30%; text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <!-- start Monday -->
                    <tr>
                        <td>
                            <div class="checkbox">
                                <label>
                                    <input class="daycheckbox" day="mon" id="chmon" runat="server" type="checkbox" style="position: static;" />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202007") %>
                                </label>
                            </div>
                            <%--  </td>
                    <td>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202007") %>--%>
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control mon clock-box" id="monstart1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control mon clock-box" id="monstart2" runat="server" />
                            </div>

                        </td>
                        <td>
                            <div class="row">
                                <div class="col-md-6 adjust-col-padding-right-side">
                                    <input type="text" class="form-control mon minute-box" id="montimelate" runat="server" />
                                </div>
                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                            </div>
                        </td>
                        <td>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control mon clock-box" id="montimehalf" runat="server" />
                            </div>
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control mon clock-box" id="monstop1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control mon clock-box" id="monstop2" runat="server" />
                            </div>

                        </td>
                    </tr>
                    <!-- end Monday-->
                    <!-- start Tuesday -->
                    <tr class="active">
                        <td>
                            <div class="checkbox">
                                <label>
                                    <input class="daycheckbox" day="tue" id="chtue" runat="server" type="checkbox" style="position: static;" />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202008") %>
                                </label>
                            </div>
                            <%-- </td>
                    <td>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202008") %>--%>
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control tue clock-box" id="tuestart1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control tue clock-box" id="tuestart2" runat="server" />
                            </div>
                        </td>
                        <td>
                            <div class="row">
                                <div class="col-md-6 adjust-col-padding-right-side">
                                    <input type="text" class="form-control tue minute-box" id="tuetimelate" runat="server" />
                                </div>
                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                            </div>
                        </td>
                        <td>
                            <div class="input-group clockpicker" data-placement="left" data-align="top"
                                data-autoclose="true">
                                <input type="text" class="form-control tue clock-box" id="tuetimehalf" runat="server" />
                            </div>
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control tue clock-box" id="tuestop1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control tue clock-box" id="tuestop2" runat="server" />
                            </div>

                        </td>
                    </tr>
                    <!-- end Tuesday -->
                    <!-- start Wensday -->
                    <tr>
                        <td>
                            <div class="checkbox">
                                <label>
                                    <input class="daycheckbox" day="wes" id="chwes" runat="server" type="checkbox" style="position: static;" />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202009") %>
                                </label>
                            </div>
                            <%--   </td>
                    <td>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202009") %>--%>
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control wes clock-box" id="wesstart1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control wes clock-box" id="wesstart2" runat="server" />
                            </div>

                        </td>
                        <td>
                            <div class="row">
                                <div class="col-md-6 adjust-col-padding-right-side">
                                    <input type="text" class="form-control wes minute-box" id="westimelate" runat="server" />
                                </div>
                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                            </div>
                        </td>
                        <td>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control wes clock-box" id="westimehalf" runat="server" />
                            </div>
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control wes clock-box" id="wesstop1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left"
                                data-align="top" data-autoclose="true">
                                <input type="text" class="form-control wes clock-box" id="wesstop2" runat="server" />
                            </div>

                        </td>
                    </tr>
                    <!-- end Wensday -->
                    <!--  start Thursday-->
                    <tr class="active">
                        <td>
                            <div class="checkbox">
                                <label>
                                    <input class="daycheckbox" day="thu" id="chthu" runat="server" type="checkbox" style="position: static;" />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202010") %>
                                </label>
                            </div>
                            <%--   </td>
                    <td>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202010") %>--%>
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left"
                                data-align="top" data-autoclose="true">
                                <input type="text" class="form-control thu clock-box" id="thustart1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control thu clock-box" id="thustart2" runat="server" />
                            </div>

                        </td>
                        <td>
                            <div class="row">
                                <div class="col-md-6 adjust-col-padding-right-side">
                                    <input type="text" class="form-control thu minute-box" id="thutimelate" runat="server" />
                                </div>
                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                            </div>
                        </td>
                        <td>
                            <div class="input-group clockpicker" data-placement="left" data-align="top"
                                data-autoclose="true">
                                <input type="text" class="form-control thu clock-box" id="thutimehalf" runat="server" />
                            </div>
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left"
                                data-align="top" data-autoclose="true">
                                <input type="text" class="form-control thu clock-box" id="thustop1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left"
                                data-align="top" data-autoclose="true">
                                <input type="text" class="form-control thu clock-box" id="thustop2" runat="server" />
                            </div>

                        </td>
                    </tr>
                    <!-- end Thursday -->
                    <!-- start Friday-->
                    <tr>
                        <td>
                            <div class="checkbox">
                                <label>
                                    <input class="daycheckbox" day="fri" id="chfri" runat="server" type="checkbox" style="position: static;" />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202011") %>
                                </label>
                            </div>
                            <%--   </td>
                    <td>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202011") %>--%>
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left"
                                data-align="top" data-autoclose="true">
                                <input type="text" class="form-control fri clock-box" id="fristart1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left"
                                data-align="top" data-autoclose="true">
                                <input type="text" class="form-control fri clock-box" id="fristart2" runat="server" />
                            </div>

                        </td>
                        <td>
                            <div class="row">
                                <div class="col-md-6 adjust-col-padding-right-side">
                                    <input type="text" class="form-control fri minute-box" id="fritimelate" runat="server" />
                                </div>
                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                            </div>
                        </td>
                        <td>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control fri clock-box" id="fritimehalf" runat="server" />
                            </div>
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control fri clock-box" id="fristop1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control fri clock-box" id="fristop2" runat="server" />
                            </div>

                        </td>
                    </tr>
                    <!-- end Friday -->
                    <!-- start Saturday -->
                    <tr class="active">
                        <td>
                            <div class="checkbox">
                                <label>
                                    <input class="daycheckbox" day="sat" id="chsat" runat="server" type="checkbox" style="position: static;" />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202012") %>
                                </label>
                            </div>
                            <%--   </td>
                    <td>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202012") %>--%>
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control sat clock-box" id="satstart1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control sat clock-box" id="satstart2" runat="server" />
                            </div>

                        </td>
                        <td>
                            <div class="row">
                                <div class="col-md-6 adjust-col-padding-right-side">
                                    <input type="text" class="form-control sat minute-box" id="sattimelate" runat="server" />
                                </div>
                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                            </div>
                        </td>
                        <td>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control sat clock-box" id="sattimehalf" runat="server" />
                            </div>
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control sat clock-box" id="satstop1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control sat clock-box" id="satstop2" runat="server" />
                            </div>

                        </td>
                    </tr>
                    <!-- end Saturday -->
                    <!-- start Sunday -->
                    <tr>
                        <td>
                            <div class="checkbox">
                                <label style="vertical-align: middle;">
                                    <input class="daycheckbox" day="sun" id="chsun" runat="server" type="checkbox" style="position: static;" />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202013") %>
                                </label>
                            </div>
                            <%--    </td>
                    <td>--%>
                       
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control sun clock-box" id="sunstart1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control sun clock-box" id="sunstart2" runat="server" />
                            </div>

                        </td>
                        <td>
                            <div class="row">
                                <div class="col-md-6 adjust-col-padding-right-side">
                                    <input type="text" class="form-control sun minute-box" id="suntimelate" runat="server" />
                                </div>
                                <div class="col-md-6 time-unit adjust-col-padding-left-side"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></div>
                            </div>
                        </td>
                        <td>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control sun clock-box" id="suntimehalf" runat="server" />
                            </div>
                        </td>
                        <td class="table-td-attend">
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control sun clock-box" id="sunstop1" runat="server" />
                            </div>
                            <div style="text-align: center;">
                                -
                            </div>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <input type="text" class="form-control sun clock-box" id="sunstop2" runat="server" />
                            </div>

                        </td>
                    </tr>
                    <!-- end Sunday -->
                </tbody>
            </table>
        </div>
    </div>
    <div style="text-align: center; padding: 30px 30px 40px 0px" class="subleveladdtable-button-section">
        <asp:Button ID="btnSave" ValidationGroup="add" runat="server" CssClass="btn btn-success"
            Font-Size="24px" Font-Bold="true" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
        <asp:Button ID="btnCancle" type="button" class="btn btn-danger" Style="margin-right: 5px; font-size: 24px; font-weight: bold;"
            runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
        <button id="btnReset" type="button" class="btn btn-default" style="margin-right: 5px; font-size: 24px; font-weight: bold;">
            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803017") %></button>
    </div>
    <script type="text/javascript">
        //$('.clockpicker').clockpicker();
        $(function () {
            $('input[id*=start1]').focus(function () {
                $(this).clockpicker({
                    placement: 'left',
                    align: 'top',
                    autoclose: true,
                    'default': 'now',
                    afterShow: function () {
                        //alert("after show");
                        //$('.popover').css('margin-left', '200px');
                    }
                });
                //clockpicker1();
            });
            $('input[id*=start2]').focus(function () {
                $(this).clockpicker({
                    placement: 'left',
                    align: 'top',
                    autoclose: true,
                    'default': 'now',
                    afterShow: function () {
                        //alert("after show");
                        //$('.popover').css('margin-left', '365px');
                    }
                });
            });
            $('input[id*=timehalf]').focus(function () {
                $(this).clockpicker({
                    placement: 'left',
                    align: 'top',
                    autoclose: true,
                    'default': 'now',
                    afterShow: function () {
                        //alert("after show");
                        //$('.popover').css('margin-left', '655px');
                    }
                });
            });
            $('input[id*=stop1]').focus(function () {
                $(this).clockpicker({
                    placement: 'left',
                    align: 'top',
                    autoclose: true,
                    'default': 'now',
                    afterShow: function () {
                        //alert("after show");
                        //$('.popover').css('margin-left', '780px');
                    }
                });
            });
            $('input[id*=stop2]').focus(function () {
                $(this).clockpicker({
                    placement: 'left',
                    align: 'top',
                    autoclose: true,
                    'default': 'now',
                    afterShow: function () {
                        //alert("after show");
                        //$('.popover').css('margin-left', '945px');
                    }
                });
            });

            $(".daycheckbox").change(function () {
                if ($(this).is(':checked')) {
                    $("." + $(this).attr("day")).removeAttr("disabled");
                    $(".overlay-" + $(this).attr("day")).hide();
                }
                else {
                    $("." + $(this).attr("day")).attr('disabled', 'disabled');
                    $(".overlay-" + $(this).attr("day")).show();
                }
            });
            $(document).ready(function () {
                var StrDays = ["mon", "tue", "wes", "thu", "fri", "sat", "sun"];
                $.each(StrDays, function (index, values) {
                    resetClock(values);
                });
            });
            $("#btnReset").click(function () {
                var StrDays = ["mon", "tue", "wes", "thu", "fri", "sat", "sun"];
                $.each(StrDays, function (index, values) {
                    resetClock(values);
                });
            });

            $("#btnCancle").click(function () {
                modalConfirm(); /*
            if (window.confirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132666") %> ")) {
                window.location = "/AdminMain.aspx";
            }*/
            });
        });

        function resetClock(days) {
            if (days != "sat" && days != "sun") {
                $("." + days).removeAttr("disabled");
                $(".overlay-" + days).hide();
                $('#ctl00_MainContent_ch' + days).attr('checked', 'checked');
            }
            else {
                $("." + days).attr('disabled', 'disabled');
                $(".overlay-" + days).show();
                $('#ctl00_MainContent_ch' + days).removeAttr("checked");
            }
            $("#ctl00_MainContent_" + days + "start1").val("06:00");
            $("#ctl00_MainContent_" + days + "start2").val("07:50");
            $("#ctl00_MainContent_" + days + "stop1").val("17:00");
            $("#ctl00_MainContent_" + days + "stop2").val("18:50");
            $("#ctl00_MainContent_" + days + "timehalf").val("12:00");
            $("#ctl00_MainContent_" + days + "timelate").val("120");
        }

        function modalConfirm() {
            showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", '<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132667") %></p>' +
       '<button class="btn btn-danger" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>&nbsp;' +
       '<button class="btn btn-success" onclick="window.location=\'/AdminMain.aspx\';"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>', function () {

       });
        }
    </script>
    <br />
    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Modal title</h4>
                </div>
                <div class="modal-body">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132668") %>
                    <asp:TextBox runat="server" ID="txtsTimetype" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    <asp:Button class="btn btn-primary" ID="btnInsertData" runat="server" Text="Save changes" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#ctl00_MainContent_txtdHolidayStart').datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
