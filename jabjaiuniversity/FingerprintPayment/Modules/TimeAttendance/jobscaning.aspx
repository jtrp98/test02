<%@ Page Title="" Language="C#" MasterPageFile="~/mpjopscan.Master" AutoEventWireup="true"
    CodeBehind="jobscaning.aspx.cs" ViewStateEncryptionMode="Never" Inherits="FingerprintPayment.Modules.TimeAttendance.jobscaning" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript">

        shortcut.add("Ctrl+N", function () {
            $('input[id*=btnSavepage1]').click();
        });

        shortcut.add("Ctrl+C", function () {
            Clear();
        });

        function j_infosell(msg) {

            j_alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg);
            //showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg, function () {
            //    $("input[id*=txtsBarCode]").focus();
            //    $('input[id*=txtsBarCode]').select();
            //    Clear();
            //});

            //$('#modalAlert').on('hidden.bs.modal', function () {
            //    $("input[id*=txtsBarCode]").focus();
            //    $('input[id*=txtsBarCode]').select();
            //    Clear();
            //});
        }

        var _id;
        $(document).ready(function () {
            //if (readCookie("test") == null) {
            //    createCookie("test", "1111", 10000);
            //}
            //else {
            //    alert(readCookie("test") + " " + readCookie("sEntities"));
            //}
            fnOpenDevice();
            ListScan();
            setInterval(function () { fnCapture(0, 'JobScan'); }, 600);
            //_id = setInterval(function () { ListScan(); }, 10000);
            $("input[id*=txtsID]").focus();
            //$('input[id*=txtsID]').keyup(function () {
            //    if ($('input[id*=txtsID]').val().length == 4) {
            //        $('input[id*=txtsID]').prop("disabled", true);
            //    }
            //});
            $("#spanText").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132594") %>");
        });

        function ListScan() {
            $.ajax({
                type: "POST",
                url: "/App_Logic/dataReportGeneric.ashx?mode=ListScan",
                cache: false,
                dataType: "json",
                success: function (response) {
                    $.each(response, function (index) {
                        var sType = response[index].LogEmpType;
                        var sStatus = response[index].LogEmpScanStatus;
                        if (sType.trim() == "0") {
                            $("#sNameIN").html(response[index].sName);
                            $("#TimeIN").html(Dateformat(response[index].LogEmpTime));
                            switch (sStatus.trim()) {
                                case "0":
                                    $("#StatusIN").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>");
                                    $("#StatusIN").css({ "color": "#00796B" });
                                    $("#TimeIN").css({ "color": "#00796B" });
                                    break;
                                case "1":
                                    $("#StatusIN").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>");
                                    $("#StatusIN").css({ "color": "#EF5350" });
                                    $("#TimeIN").css({ "color": "#EF5350" });
                                    break;
                                case "3":
                                    $("#StatusIN").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132595") %>");
                                    $("#StatusIN").css({ "color": "#EF5350" });
                                    $("#TimeIN").css({ "color": "#EF5350" });
                                    break;
                            }
                        }
                        else {
                            $("#sNameOUT").html(response[index].sName);
                            $("#TimeOUT").html(Dateformat(response[index].LogEmpTime));
                            switch (sStatus.trim()) {
                                case "0":
                                    $("#StatusOUT").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>");
                                    $("#StatusOUT").css({ "color": "#00796B" });
                                    $("#TimeOUT").css({ "color": "#00796B" });
                                    break;
                                case "2":
                                    $("#StatusOUT").html("ออกก่อน");
                                    $("#StatusOUT").css({ "color": "#EF5350" });
                                    $("#TimeOUT").css({ "color": "#EF5350" });
                                    break;
                                case "3":
                                    $("#StatusOUT").html("ออกช้า");
                                    $("#StatusOUT").css({ "color": "#EF5350" });
                                    $("#TimeOUT").css({ "color": "#EF5350" });
                                    break;
                            }

                        }
                    });
                }
            });
        }
        function hideResult() {
            $("#scandiv").show();
            $("#resultScan").hide();
            Clear();
        }

        function postbackReady() {
            $("input[id*=txtsBarCode]").focus();
        }

        function hideScan(name, statusScan, time) {
            if ($("#resultScan").css('display') == 'none') {
                //$("#scandiv").hide();
                //$("#resultScan").show();
                $("input[id*=txtsID]").prop("disabled", false);
                $("input[id*=txtsID]").val("");
                $("#resultName").val(name);
                if (statusScan == "0") {
                    $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132547") %>");
                    $("#statusScan").css({ "color": "#00796B" });
                }
                else if (statusScan == "-1") {
                    $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132548") %>");
                    $("#statusScan").css({ "color": "#FB8C00" });
                }
                else if (statusScan == "2") {
                    $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105017") %>");
                    $("#statusScan").css({ "color": "#EF5350" });
                }
                else if (statusScan == "3") {
                    $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132549") %>");
                    $("#statusScan").css({ "color": "#000000" });
                }
                else if (statusScan == "4") {
                    $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132550") %>");
                    $("#statusScan").css({ "color": "#00796B" });
                }
                else if (statusScan == "5") {
                    $("#sNameIN").html(name);
                    $("#StatusIN").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132593") %>").css({ "color": "#00796B" });
                    $("#TimeIN").html(time).css({ "color": "#00796B" });

                    $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132552") %> " + time + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %>");
                    $("#statusScan").css({ "color": "#00796B" });
                }
                else if (statusScan == "6") {
                    $("#sNameOUT").html(name);
                    $("#StatusOUT").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132593") %>").css({ "color": "#00796B" });
                    $("#TimeOUT").html(time).css({ "color": "#00796B" });

                    $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132553") %> " + time + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %>");
                    $("#statusScan").css({ "color": "#00796B" });
                }
                else if (statusScan == "7") {
                    $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132551") %>");
                    $("#statusScan").css({ "color": "#00796B" });
                }
                else if (statusScan == "8") {
                    $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132554") %>");
                    $("#statusScan").css({ "color": "#EF5350" });
                }
                else if (statusScan == "9") {
                    $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132596") %>");
                    $("#statusScan").css({ "color": "#EF5350" });
                }
                else {
                    $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131136") %>");
                    $("#statusScan").css({ "color": "#EF5350" });
                }
                //clearInterval(ListScan());
                //setTimeout(function () { hideResult(); }, 700);
                if (statusScan != "5" && statusScan != "6") {
                    ListScan();
                }
                else {
                    hideResult();
                }
                //else {
                //    //clearInterval(_id);
                //    //_id = setInterval(function () { ListScan(); }, 10000);
                //}
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <object id="objSecuBSP" classid="CLSID:6283f7ea-608c-11dc-8314-0800200c9a66" height="0"
        name="objSecuBSP" style="left: 0px; top: 0px" viewastext="" width="0">
    </object>
    <div class="row">
        <div class="col-lg-2">
        </div>
        <div class="col-lg-8">
            <div class="full-card box-content" style="background-color: white;">
                <div class="row">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="col-lg-12">
                                <asp:Literal ID="ltrjavascript" runat="server" />
                                <div id="scandiv">
                                    <div class="row" style="display: none;">
                                        <div class="col-xs-4">
                                            <label class="pull-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %></label>
                                        </div>
                                        <div class="col-xs-8">
                                            <asp:Button ID="btnRegister" runat="server" OnClientClick="fnCapture(); return false;"
                                                class="btn btn-info" Text="ตรวจสอบรายนิ้วมือ(Ctrl+P)" />
                                            <span id="div" style="clear: both; width: 100px;"></span>&nbsp;
                            <asp:TextBox ID="txtCheckFinger" runat="server" Style="display: none;" Text="1" Width="200px" />
                                            <asp:TextBox ID="txtUserFinger" runat="server" Style="display: none;" Text="1" />
                                            <asp:RequiredFieldValidator ID="revtxtCheckFinger" runat="server" Display="None"
                                                SetFocusOnError="true" ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %><br>"
                                                ControlToValidate="txtCheckFinger" ValidationGroup="add" />
                                            <cc1:ValidatorCalloutExtender runat="server" ID="vcetxtCheckFinger" TargetControlID="revtxtCheckFinger"
                                                HighlightCssClass="validatorcallouthighlight" Width="200px" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <label class="pull-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></label>
                                        </div>
                                        <div class="col-xs-4">
                                            <asp:TextBox ID="txtsIDShow" runat="server" MaxLength="4" class="form-control" Width="300px" />
                                            <asp:TextBox ID="txtsID" runat="server" MaxLength="4" class="form-control hidden" Width="300px" />
                                        </div>
                                        <div class="col-xs-6 text-center">
                                            <asp:Button ID="btnClear" runat="server" AccessKey="N" OnKeyPress="return false;"
                                                OnClientClick="Clear(); " Text="Clear(Ctrl+C)" UseSubmitBehavior="False"
                                                class="btn btn-primary" />
                                        </div>
                                    </div>
                                    <div class="row hidden">
                                        <div class="col-xs-4">
                                            <label class="pull-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                        </div>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="txtsName" runat="server" MaxLength="512" ReadOnly="True" class="form-control" Width="300px" />
                                        </div>
                                    </div>
                                    <div class="row hidden">
                                        <div class="col-xs-4">
                                            <label class="pull-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                                        </div>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="txtsLastName" runat="server" ReadOnly="True" class="form-control" Width="300px" />
                                        </div>
                                    </div>
                                    <div class="row mini--space__top hidden">
                                        <div class="col-xs-12 center">
                                            <%--  <asp:Button ID="btnClear" runat="server" AccessKey="N" OnKeyPress="return false;"
                                                OnClientClick="Clear(); " TabIndex="100" Text="Clear(Ctrl+C)" UseSubmitBehavior="False"
                                                class="btn btn-primary" />--%>
                                            <div class="hidden">
                                                <asp:Button ID="btnSavepage1" runat="server" AccessKey="N" OnKeyPress="return false;"
                                                    TabIndex="100" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>(Ctrl+N)" UseSubmitBehavior="False" class="btn btn-primary" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="resultScan" class="hidden" style="display: none;">
                                    <div class="row">
                                        <div class="col-xs-4">
                                            <label class="pull-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                        </div>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="resultName" runat="server" MaxLength="512" ClientIDMode="Static"
                                                ReadOnly="True" class="input--mid" disabled />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-xs-12" style="text-align: center">
                                            <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132597") %></h1>
                                            <h2>
                                                <label id="statusScan">
                                                    -</label></h2>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="col-lg-2">
        </div>
    </div>
    <div class="row--space">
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="row" style="font-size: 30px;">
                <div class="col-lg-4 text-center">
                </div>
                <div class="col-lg-4 text-center">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132598") %></label>
                </div>
                <div class="col-lg-4 text-center">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133257") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204050") %></label>
                </div>
            </div>
            <div class="row" style="font-size: 50px;">
                <div class="col-lg-4 text-center">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %>
                </div>
                <div class="col-lg-4 text-center">
                    <label id="sNameIN">-</label>
                </div>
                <div class="col-lg-4 text-center">
                    <label id="sNameOUT">-</label>
                </div>
            </div>
            <div class="row" style="font-size: 50px;">
                <div class="col-lg-4 text-center">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %>
                </div>
                <div class="col-lg-4 text-center">
                    <label id="TimeIN">-</label>
                </div>
                <div class="col-lg-4 text-center">
                    <label id="TimeOUT">-</label>
                </div>
            </div>
            <div class="row" style="font-size: 50px;">
                <div class="col-lg-4 text-center">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>
                </div>
                <div class="col-lg-4 text-center">
                    <label id="StatusIN">-</label>
                </div>
                <div class="col-lg-4 text-center">
                    <label id="StatusOUT">-</label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
