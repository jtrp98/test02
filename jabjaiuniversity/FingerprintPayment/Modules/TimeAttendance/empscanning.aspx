<%@ Page Title="" Language="C#" MasterPageFile="~/mp2.Master" AutoEventWireup="true"
    CodeBehind="empscanning.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.empscanning" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript">

        shortcut.add("Ctrl+N", function () {
            $('input[id*=btnSavepage1]').click();
        });

        shortcut.add("Ctrl+C", function () {
            Clear();
        });

        function Clear() {
            $('input[id*=txtsID]').prop("disabled", false);
            $("input[id*=txtsID]").val('');
            $("input[id*=txtsID]").focus();
        }

        function j_infosell(msg) {

            showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg, function () {
                $("input[id*=txtsBarCode]").focus();
                $('input[id*=txtsBarCode]').select();
                Clear();
            });
            $('#modalAlert').on('hidden.bs.modal', function () {
                $("input[id*=txtsBarCode]").focus();
                $('input[id*=txtsBarCode]').select();
                Clear();
            });
        }


        $(document).ready(function () {
            fnOpenDevice();
            setInterval(function () { fnCapture(0, 'EmpScan'); }, 1000);
            $("input[id*=txtsID]").focus();
            $('input[id*=txtsID]').keyup(function () {
                if ($('input[id*=txtsID]').val().length == 4) {
                    $('input[id*=txtsID]').prop("disabled", true);
                }
            });
            $("#spanText").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132555") %>");
        });


        function hideResult() {
            $("#scandiv").show();
            $("#resultScan").hide();
            Clear();
        }

        function hideScan(name, statusScan, time) {
            if ($("#resultScan").css('display') == 'none') {
                $("#scandiv").hide();
                $("#resultScan").show();
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
                    $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132552") %> " + time + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %>");
                    $("#statusScan").css({ "color": "#00796B" });
                }
                else if (statusScan == "6") {
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
                else {
                    $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131136") %>");
                    $("#statusScan").css({ "color": "#EF5350" });
                }

                setTimeout(function () { hideResult(); }, 6000);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="detail-card box-content">
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
                            <asp:TextBox ID="txtCheckFinger" runat="server" Style="display: none;" Text="1" />
                            <asp:TextBox ID="txtUserFinger" runat="server" Style="display: none;" Text="1" />
                            <asp:RequiredFieldValidator ID="revtxtCheckFinger" runat="server" Display="None"
                                SetFocusOnError="true" ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %><br>"
                                ControlToValidate="txtCheckFinger" ValidationGroup="add" />
                            <cc1:ValidatorCalloutExtender runat="server" ID="vcetxtCheckFinger" TargetControlID="revtxtCheckFinger"
                                HighlightCssClass="validatorcallouthighlight" Width="200px" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4">
                            <label class="pull-right">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></label>
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtsID" runat="server" MaxLength="4" class="input--mid" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4">
                            <label class="pull-right">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtsName" runat="server" MaxLength="512" ReadOnly="True" class="input--mid" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4">
                            <label class="pull-right">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtsLastName" runat="server" ReadOnly="True" class="input--mid" />
                        </div>
                    </div>
                    <div class="row mini--space__top">
                        <div class="col-xs-12 center">
                            <asp:Button ID="btnClear" runat="server" AccessKey="N" OnKeyPress="return fales;"
                                OnClientClick="Clear(); " TabIndex="100" Text="Clear(Ctrl+C)" UseSubmitBehavior="False"
                                class="btn btn-primary" />
                            <div style="display: none;">
                                <asp:Button ID="btnSavepage1" runat="server" AccessKey="N" OnKeyPress="return fales;"
                                    TabIndex="100" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>(Ctrl+N)" UseSubmitBehavior="False" class="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="resultScan" style="display: none;">
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
                            <h1>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105023") %></h1>
                            <h2>
                                <label id="statusScan">
                                    -</label></h2>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
