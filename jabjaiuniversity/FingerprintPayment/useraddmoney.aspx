<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="useraddmoney.aspx.cs" Inherits="FingerprintPayment.useraddmoney" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="Javascript" type="text/javascript">
        //jQuery(window).load(function () {
        //    //$.blockUI({ message: '<h1 ><img src="Scripts/blockUI/searching_progress.gif" /><div style="font-size: 45px !important; font-family: thaifont;">กำลังโหลดข้อมูล</div></h1>' });
        //    blockPage();
        //})
        //jQuery(window).unload(function () {
        //    $.unblockUI();
        //})

        //function to block the whole page  


        $(document).ready(function () {
            $("#divmain").css("display", "");
            $("#load").css("display", "none");
            //fnOpenDevice();
            if ($("input[id*=txtnMoney]").is(":focus") == false) {
                $("input[id*=txtnMoney]").focus();
            }
            GetListAddMoney();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript">
        //setInterval(function () { fnCapture(0, 'AddMoney'); }, 500);
        setInterval(function () { GetListAddMoney(); }, 10000);
        shortcut.add("Ctrl+C", function () {
            Clear();
        });
        function Clear() {
            $('input[id*=txtsID]').prop("disabled", false);
            $("input[id*=txtsID]").val('');
            $("input[id*=txtCheckFinger]").val('');
            $("input[id*=txtsName]").val('');
            $("input[id*=txtsLastName]").val('');
            $("input[id*=txtnBalance]").val('');
            $("input[id*=txtnMoney]").val('');
            $("input[id*=txtUserFinger]").val('');
        }

        function GetListAddMoney() {
            $.ajax({
                type: "POST",
                url: "ShowUpdateMoney.ashx",
                cache: false,
                dataType: "html",
                success: function (response) {
                    var _str = response;
                    $("div[id*=divhtml]").html(_str);
                }
            });
        }
        function jScript() {
            if ($("input[id*=txtsName]").val() != "") $("input[id*=txtnMoney]").focus();
            $("#btnSave").click(function () {
                InsertData();
                return false;
            });
            $("input[id*=txtnMoney]").keypress(function (e) {
                if (e.keyCode == 13) {
                    InsertData();
                    return false;
                }
            });
            function InsertData() {
                if ($("input[id*=txtnMoney]").val().trim() == "") {
                    j_infosell("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131170") %>");
                }
                else if ($("input[id*=txtUserFinger]").val().trim() == "") {
                    j_infosell("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131171") %>");
                }
                else {
                    var request = $.ajax({
                        type: "POST",
                        url: "/App_Logic/dataGeneric.ashx?Mode=insertmoney&id=" + $("input[id*=txtUserFinger]").val() + "&money=" + $("input[id*=txtnMoney]").val(),
                        cache: false,
                        dataType: "html",
                        success: function (response) {

                        }
                    });
                    request.done(function (msg) {
                        if (msg > 0) {
                            j_infosell('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131172") %> ' + $("input[id*=txtsName]").val() + ' ' + $("input[id*=txtsLastName]").val() + ' ' + $("input[id*=txtnMoney]").val() + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %> <br /> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131174") %> ' + msg + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %> ');
                        }
                        else {
                            j_infosell("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131170") %>");
                        }
                        Clear();
                        GetListAddMoney();
                        __doPostBack('ctl00$MainContent$btnClear', '');
                    });
                }
            }

        }
        function j_infosell(msg) {
            showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg);
        }
    </script>
    <object id="objSecuBSP" classid="CLSID:6283f7ea-608c-11dc-8314-0800200c9a66" height="0"
        name="objSecuBSP" style="left: 0px; top: 0px" viewastext="" width="0">
    </object>
    <div class="full-card box-content">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Timer ID="Timer1" runat="server" Enabled="true"  Interval="1000" />
        <%--   <div class="row">
            <img src="Scripts/blockUI/searching_progress.gif" id="load" />
        </div>--%>

        <div class="row user-add-money-container" id="divmain">
            <div class="col-lg-5 col-md-12 col-xs-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <script type="text/javascript" language="javascript">
                            Sys.Application.add_load(jScript);
                        </script>
                        <div class="row" style="display: none;">
                            <div class="col-lg-4 col-md-3 col-xs-4">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %></label>
                            </div>
                            <div class="col-lg-8 col-md-7 col-xs-7">
                                <asp:Button ID="btnRegister" runat="server" OnClientClick="fnCapture();return false;"
                                    class="btn btn-info" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132149") %>" UseSubmitBehavior="False" />
                                <span id="div" style="clear: both; width: 100%;"></span>&nbsp;
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
                            <div class="col-lg-4 col-md-3 col-xs-4">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></label>
                            </div>
                            <div class="col-lg-8 col-md-7 col-xs-7">
                                <asp:TextBox ID="txtsID" runat="server" MaxLength="4" ReadOnly="True" CssClass='form-control' />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-md-3 col-xs-4">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                            </div>
                            <div class="col-lg-8 col-md-7 col-xs-7">
                                <asp:TextBox ID="txtsName" runat="server" MaxLength="512" ReadOnly="True" CssClass='form-control' />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-md-3 col-xs-4">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                            </div>
                            <div class="col-lg-8 col-md-7 col-xs-7">
                                <asp:TextBox ID="txtsLastName" runat="server" ReadOnly="True" CssClass='form-control' />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-md-3 col-xs-4">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106151") %></label>
                            </div>
                            <div class="col-lg-8 col-md-7 col-xs-7">
                                <asp:TextBox ID="txtnBalance" runat="server" MaxLength="13" ReadOnly="True" CssClass='form-control'/>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-4 col-md-3 col-xs-4">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501008") %></label>
                            </div>
                            <div class="col-lg-8 col-md-7 col-xs-7">
                                <asp:TextBox ID="txtnMoney" runat="server" AutoCompleteType="Disabled" CssClass='form-control'/>
                            </div>
                        </div>
                        <div class="row mini--space__top">
                            <div class="button-segment">
                                <div class="col-lg-4"></div>
                                <div class="col-lg-8 col-xs-12 user-add-money-button-segment">
                                    <input type="button" id="btnSave" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" class="btn btn-success global-btn" />
                                    <asp:Button ID="btnClear" runat="server" AccessKey="N" OnKeyPress="return fales;"
                                        TabIndex="100" Text="Clear(Ctrl+C)" UseSubmitBehavior="False"
                                        class="btn btn-danger global-btn" />
                                    &nbsp;<asp:Button ID="btnCancel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" class="btn btn-danger global-btn"
                                        UseSubmitBehavior="False" Style="display: none;" />
                                </div>

                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="txtnMoney" />
                        <asp:AsyncPostBackTrigger EventName="Tick" ControlID="Timer1" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="col-lg-7 col-md-12 col-xs-12 text-center table-add-money-container">
                    <span class="table-caption" style="font-weight: bolder;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131175") %></span>
                    <br />
                    <p></p>
                    <asp:Literal ID="ltrHtml" runat="server" />
                    <div id="divhtml">
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
