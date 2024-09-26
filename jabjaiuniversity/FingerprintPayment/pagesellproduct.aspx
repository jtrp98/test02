<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="pagesellproduct.aspx.cs" Inherits="FingerprintPayment.pagesellproduct" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript">
<!--
    shortcut.add("Ctrl+N", function () {
        $('input[id*=btnSavepage1]').click();
    });
    shortcut.add("ESC", function () {
        return false;
        //$('input[id*=btnCancel]').click();
    });
    shortcut.add("Ctrl+P", function () {
        fnCapture();
    });
    shortcut.add("Ctrl+C", function () {
        Clear();
    });
    shortcut.add("Ctrl+B", function () {
        $('input[id*=btnCancel2]').click();
    });
    shortcut.add("Ctrl+Shift", function () {
        $('.se-pre-con').show();
        loadingconfirm();
    });
    shortcut.add("esc", function () {
        return false;
    });

    function Clear() {
        $('input[id*=txtsID]').prop("disabled", false);
        $("input[id*=txtsID]").val('');
        $("input[id*=txtCheckFinger]").val('');
        $("input[id*=txtsName]").val('');
        $("input[id*=txtsLastName]").val('');
        $("input[id*=txtnBalance]").val('');
    }
    function j_infosell(msg) {
        showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg, function () {
            $("input[id*=txtsBarCode]").focus();
            $('input[id*=txtsBarCode]').select();
        });

        $('#modalAlert').on('hidden.bs.modal', function () {
            $("input[id*=txtsBarCode]").focus();
            $('input[id*=txtsBarCode]').select();
        });
    }
    function postbackReady() {
        $("input[id*=txtsBarCode]").focus();
    }

    function getComfirm() {
        $.ajax({
            type: "POST",
            url: "/App_Logic/APIJabJai.ashx?mode=getstatus",
            cache: false,
            dataType: "html",
            success: function (response) {

            }
        }).done(function (result) {
            if (result == "1") {
                $('#imgloading').attr("src", "/images/success.gif");
                $('.se-pre-con').show();
                clearInterval(refreshIntervalId);
                setTimeout(function () { __doPostBack('ctl00$MainContent$btnSave', ''); }, 500);
            }
            else if (result == "-1") {
                clearInterval(refreshIntervalId);
                $('#modalloading').modal('hide');
            }
        });
    }
    var refreshIntervalId;
    function loadingconfirm() {
        $.ajax({
            type: "POST",
            url: "/App_Logic/APIJabJai.ashx?mode=msgconfirm",
            cache: false,
            dataType: "html",
            success: function (response) {

            }
        }).done(function (result) {
        });
        $('#modalloading').modal('show');
        //$(".rowhidden").removeClass()
        clearInterval(refreshIntervalId);
        refreshIntervalId = setInterval(function () { getComfirm(); }, 500);
    }

    $(document).ready(function () {
        $("input[id*=txtsBarCode]").keypress(function (e) {
            if ($("input[id*=txtsBarCode]").val() == "") {
                $("input[id*=txtsBarCode]").focus();
            }
            var sID = $("input[id*=txtsBarCode]").val();
            if (e.keyCode == 13) {
                $.ajax({
                    type: "POST",
                    url: "PriceProduct.ashx",
                    cache: false,
                    dataType: "html",
                    data: { ID: encodeURIComponent(sID) },
                    success: function (response) {
                        var _str = response;
                        if (_str != "") {
                            $('span[id*=productname]').html(_str.split('|')[1]);
                            $('span[id*=productprice]').html(_str.split('|')[0]);
                        }
                        else {
                            $('span[id*=productname]').html("");
                            $('span[id*=productprice]').html("");
                        }
                    }
                });
                $("input[id*=txtnNumber]").val("1");
                $("input[id*=txtnNumber]").select();
                if ($("input[id*=txtsBarCode]").attr("readonly"))
                    return true;
                else
                    return false;
            }
        });
    });
    </script>
    <script language="Javascript" type="text/javascript">
        $(document).ready(function () {
            //fnOpenDevice();
        });
        function jScript() {
            //shortcut.add("Ctrl+B", function () {
            //    $('input[id*=btnCancel2]').click();
            //});
            //shortcut.add("Ctrl+Shift", function () {
            //    $('.se-pre-con').show();
            //    loadingconfirm();
            //});
            $("input[id*=txtsBarCode]").focus();
            $("input[id*=txtnNumber]").keypress(function (e) {
                if ($("input[id*=txtnNumber]").val() == "") {
                    $("input[id*=txtnNumber]").focus();
                }
                var sID = $("input[id*=txtnNumber]").val();
                if (e.keyCode == 13) {
                    __doPostBack('ctl00$MainContent$txtsBarCode', '');
                }
            });

            //$("input[id*=txtsBarCode]").keypress(function (e) {
            //    if ($("input[id*=txtsBarCode]").val() == "") {
            //        $("input[id*=txtsBarCode]").focus();
            //    }
            //    var sID = $("input[id*=txtsBarCode]").val();
            //    if (e.keyCode == 13) {
            //        $.ajax({
            //            type: "POST",
            //            url: "PriceProduct.ashx",
            //            cache: false,
            //            dataType: "html",
            //            data: { ID: encodeURIComponent(sID) },
            //            success: function (response) {
            //                var _str = response;
            //                if (_str != "") {
            //                    $('span[id*=productname]').html(_str.split('|')[1]);
            //                    $('span[id*=productprice]').html(_str.split('|')[0]);
            //                }
            //                else {
            //                    $('span[id*=productname]').html("");
            //                    $('span[id*=productprice]').html("");
            //                }
            //            }
            //        });
            //        $("input[id*=txtnNumber]").val("1");
            //        $("input[id*=txtnNumber]").select();
            //        if ($("input[id*=txtsBarCode]").attr("readonly"))
            //        {
            //            return true;
            //        }
            //        else
            //            return false;
            //    }
            //});
        }
        //setInterval(function () { fnCapture(0, 'SellProduct'); }, 1000);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="timerSell" runat="server" Interval="500"></asp:Timer>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">
                Sys.Application.add_load(jScript);
            </script>
            <asp:MultiView ID="mtvsell" runat="server" ActiveViewIndex="1" OnActiveViewChanged="mtvsell_ActiveViewChanged">
                <asp:View ID="pagesell" runat="server">
                    <div class="full-card box-content pagesellproduct-container">
                        <asp:Literal ID="ltrscript" runat="server" />
                        <div class="col-xs-12 center">
                            <asp:Label runat="server" ID="lblsName" class="pull-right" />
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-xs-12 .input-section">
                                <div class="row">
                                    <div class="col-lg-4 col-md-5 col-xs-12">
                                        <label class="pull-right">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106155") %></label>
                                    </div>
                                    <div class="col-lg-8 col-md-7 col-xs-12">
                                        <asp:TextBox ID="txtsBarCode" runat="server" MaxLength="50" AutoCompleteType="Disabled"
                                            AutoPostBack="True" OnTextChanged="txtnNumber_TextChanged" CssClass='form-control'/>
                                        <asp:RequiredFieldValidator ID="revtxtsBarCode" runat="server" Display="None" SetFocusOnError="true"
                                            ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106155") %><br>"
                                            ControlToValidate="txtsBarCode" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender runat="server" ID="vcetxtsBarCode" TargetControlID="revtxtsBarCode"
                                            HighlightCssClass="validatorcallouthighlight" Width="200px" />
                                    </div>
                                </div>
                                <div class="row--space">
                                    <div class="col-xs-4">
                                    </div>
                                    <div class="col-xs-8">
                                    </div>
                                </div>
                                <div class="row hidden rowhidden">
                                    <div class="col-xs-4">
                                        <label class="pull-right">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %></label>
                                    </div>
                                    <div class="col-xs-8">
                                        <span id="productname" runat="server"></span>
                                    </div>
                                </div>
                                <div class="row hidden rowhidden">
                                    <div class="col-xs-4">
                                        <label class="pull-right">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131100") %></label>
                                    </div>
                                    <div class="col-xs-8">
                                        <span id="productprice" runat="server"></span>
                                    </div>
                                </div>
                                <div class="row hidden rowhidden">
                                    <div class="col-xs-4">
                                        <label class="pull-right">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131105") %></label>
                                    </div>
                                    <div class="col-xs-8">
                                        <asp:TextBox ID="txtnNumber" runat="server"
                                            OnTextChanged="txtnNumber_TextChanged" ValidationGroup="add"
                                            Width="180px" Visible="False" CssClass='form-control' Style="width: 200px;">1</asp:TextBox>
                                        <asp:Label ID="lblAmount" runat="server">1</asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4 col-md-5 col-xs-12">
                                        <label class="pull-right">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501013") %></label>
                                    </div>
                                    <div class="col-lg-8 col-md-7 col-xs-12">
                                        <asp:TextBox ID="txtnMoney" runat="server" ReadOnly="True"
                                            Style="font-size: 60px; text-align: right; width: 100%;">0</asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mini--space__top pagesellproduct-button-segment">
                                    <div class="col-sm-12">
                                        <input type="button" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>(Ctrl+Shift)" class="btn btn-success global-btn" onclick="$('.se-pre-con').show(); loadingconfirm(); return false;" />
                                        <asp:Button ID="btnSave" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>(Ctrl+Shift)" class="btn btn-success global-btn hidden"
                                            UseSubmitBehavior="False" />
                                        <asp:Button ID="btnCancel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>(Esc)" class="btn btn-danger global-btn"
                                            TabIndex="100" Visible="False" UseSubmitBehavior="False" />
                                        <asp:Button ID="btnCancel2" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131104") %>" class="btn btn-primary global-btn"
                                            UseSubmitBehavior="False" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-7 col-xs-12 table-section">
                                <div class="wrapper-table">
                                    <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0"
                                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table mini-font">
                                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                                        <Columns>
                                            <asp:BoundColumn DataField="sBarCode" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106155") %>">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Width="25%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="sProduct" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %>">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Width="25%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="nNumber" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %>">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="nProductID" HeaderText="nProductID" Visible="False" />
                                            <asp:BoundColumn DataField="nPrice" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404034") %>">
                                                <HeaderStyle Width="10%" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="nTotal" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131102") %>">
                                                <HeaderStyle Width="15%" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></asp:LinkButton>
                                                    <asp:LinkButton ID="lbkEdit" runat="server" CommandName="Edit"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" CssClass="headerCell" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" CssClass="itemCell" />
                                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Font-Bold="False" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="pagerCell" />
                                        <SelectedItemStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" />
                                    </asp:DataGrid>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="submitbuy" runat="server">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    <div class="page-sell-product-animate-container" style="text-align: center;">
                        <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131106") %>
                        </h1>
                        <center>
                            <img src="images/fingerprint_animate.gif" />
                        </center>
                    </div>
                    <div class="detail-card box-content hidden">
                        <asp:Literal ID="ltrjavascript" runat="server" />
                        <div class="row">
                            <div class="col-xs-4">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></label>
                            </div>
                            <div class="col-xs-8">
                                <asp:TextBox ID="txtsID" runat="server" MaxLength="4" CssClass='form-control' Style="width: 300px;" />
                            </div>
                        </div>
                        <div class="row" style="display: none;">
                            <div class="col-xs-4">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %></label>
                            </div>
                            <div class="col-xs-8">
                                <asp:Button ID="btnRegister" runat="server" class="btn btn-info" OnClientClick="fnCapture(); return false;"
                                    Text="ตรวจสอบรายนิ้วมือ(Ctrl+P)" />
                                <span id="div" style="clear: both; width: 100px;"></span>&nbsp;
                        <asp:TextBox ID="txtCheckFinger" runat="server" Style="display: none;" Text="" />
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
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                            </div>
                            <div class="col-xs-8">
                                <asp:TextBox ID="txtsName" runat="server" MaxLength="512" ReadOnly="True" CssClass='form-control'
                                    Style="width: 300px;" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-4">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                            </div>
                            <div class="col-xs-8">
                                <asp:TextBox ID="txtsLastName" runat="server" ReadOnly="True" CssClass='form-control'
                                    Style="width: 300px;" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-4">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106151") %></label>
                            </div>
                            <div class="col-xs-8">
                                <asp:TextBox ID="txtnBalance" runat="server" MaxLength="13" ReadOnly="True" CssClass='form-control'
                                    Style="width: 300px;" />
                            </div>
                        </div>
                        <div class="row mini--space__top">
                            <div class="col-xs-12 center">
                                <asp:Button ID="btnClear" runat="server" AccessKey="N" OnKeyPress="return fales;"
                                    OnClientClick="Clear(); return fales;" TabIndex="100" Text="Clear(Ctrl+C)" UseSubmitBehavior="False"
                                    class="btn btn-danger custom-button" />
                                <asp:Button ID="btnSavepage1" runat="server" AccessKey="N" OnKeyPress="return fales;"
                                    TabIndex="100" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>(Ctrl+N)" UseSubmitBehavior="False" class="btn btn-primary custom-button"
                                    Style="display: none;" />
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="timerSell" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="modalloading" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true">
        <div class="modal-md" style="top: 50px; margin-top: 14%">
            <div class="modal-content" style="max-width: 700px; margin-left: 200px;">
                <div class="modal-header center">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131108") %></h1>
                </div>
                <div class="modal-body" id="modalpopupdata-content" style="text-align: center; font-size: 19px; height: 275px;">
                    <img id="imgloading" src="/images/loader-128x/Preloader_2.gif" />
                </div>
                <%--    <div class="modal-footer" style="display: block; text-align: center;">
                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
