<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="pagcashsales.aspx.cs" Inherits="FingerprintPayment.pagcashsales1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript">
<!--
    shortcut.add("Ctrl+N", function () {
        $('input[id*=btnSavepage1]').click();
    });
    shortcut.add("Ctrl+B", function () {
        $('input[id*=btnCancel2]').click();
    });
    shortcut.add("ESC", function () {
        $('input[id*=btnCancel]').click();
    });
    shortcut.add("Ctrl+Shift", function () {
        $('input[id*=btnSave]').click();
    });
    shortcut.add("Ctrl+P", function () {
        fnCapture();
    });
    function j_infosell(msg) {
        //            Sexy.info("<br/><h1>&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %></h1><br/><p>&nbsp;&nbsp;&nbsp;" + msg + "</p>",
        //            { onComplete:
        //			        function (returnvalue) {
        //			            if (returnvalue) {
        //			                $("input[id*=txtsBarCode]").focus();
        //			            }
        //			        }
        //            });
        showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg, function () {
            console.log("ON Close");
            $("input[id*=txtsBarCode]").focus();
        });

        $('#modalAlert').on('hidden.bs.modal', function () {
            $("input[id*=txtsBarCode]").focus();
            $('input[id*=txtsBarCode]').select();
        });
    }

    function postbackReady() {
        $("input[id*=txtsBarCode]").focus();
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
                            //                                fnCapture();
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
    function fnCapture() {
        var err
        $('input[id*=txtCheckFinger]').val("");
        try // Exception handling
        {
            // Open device. [AUTO_DETECT]
            // You must open device before capture.
            DEVICE_FDP02 = 1;
            DEVICE_FDU02 = 2;
            DEVICE_FDU03 = 3;
            DEVICE_FDU04 = 4;
            DEVICE_FDU05 = 5; 	// HU20

            DEVICE_AUTO_DETECT = 255;

            document.objSecuBSP.OpenDevice(DEVICE_AUTO_DETECT);
            err = document.objSecuBSP.ErrorCode; // Get error code

            if (err != 0)		// Device open failed
            {
                alert('Device open failed !');
                return;
            }

            // Enroll user's fingerprint.
            document.objSecuBSP.Capture();
            err = document.objSecuBSP.ErrorCode; // Get error code

            if (err != 0)		// Enroll failed
            {
                //                    alert('Capture failed ! Error Number : [' + err + ']');
                return;
            }
            else	// Capture success
            {
                // Get text encoded FIR data from SecuBSP module.
                $('input[id*=txtUserFinger]').val(document.objSecuBSP.FIRTextData);
                var sID = $('input[id*=txtUserFinger]').val();
                $.ajax({
                    type: "POST",
                    url: "getUserMoney.ashx",
                    cache: false,
                    dataType: "html",
                    data: { ID: encodeURIComponent(sID) },
                    success: function (response) {
                        var _str = response;
                        if (_str != "") {
                            $('span[id*=div]').html('');
                            $('input[id*=txtCheckFinger]').val(_str.split('|')[0]);
                            $('input[id*=txtsName]').val(_str.split('|')[1]);
                            $('input[id*=txtsLastName]').val(_str.split('|')[2]);
                            $('input[id*=txtnBalance]').val(_str.split('|')[3]);
                            $('input[id*=btnSavepage1]').focus();
                        }
                        else {
                            $('span[id*=div]').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131103") %>');
                            $('span[id*=div]').css('color', 'red');
                            $('input[id*=txtCheckFinger]').val("");
                            $('input[id*=txtsName]').val("");
                            $('input[id*=txtsLastName]').val("");
                            $('input[id*=txtnBalance]').val("");
                            $('input[id*=btnRegister]').focus();
                            setTimeout(fnCapture, 100);
                            //                                fnCapture();
                        }
                    }
                });
                //                    document.bspmain.template2.value = document.objSecuBSP.FIRTextData;
                //                    alert('Capture success !');
            }

            // Close device. [AUTO_DETECT]
            document.objSecuBSP.CloseDevice(DEVICE_AUTO_DETECT);

        }
        catch (e) {
            alert(e.message);
        }

        return;
    }
    </script>
    <div class="full-card box-content pagecashsales-container">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-5 col-xs-12 input-section">
                        <div class="row">
                            <div class="col-lg-4 col-md-5 col-xs-12">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106155") %></label>
                            </div>
                            <div class="col-lg-8 col-md-7 col-xs-12">
                                <asp:TextBox ID="txtsBarCode" runat="server" MaxLength="50" AutoCompleteType="Disabled"
                                    AutoPostBack="True" OnTextChanged="txtnNumber_TextChanged" CssClass='form-control' />
                                <asp:RequiredFieldValidator ID="revtxtsBarCode" runat="server" Display="None" SetFocusOnError="true"
                                    ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106155") %><br>"
                                    ControlToValidate="txtsBarCode" ValidationGroup="add"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender runat="server" ID="vcetxtsBarCode" TargetControlID="revtxtsBarCode"
                                    HighlightCssClass="validatorcallouthighlight" Width="200px" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-md-5 col-xs-12">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %></label>
                            </div>
                            <div class="col-lg-8 col-md-7 col-xs-12">
                                <span id="productname" runat="server"></span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-md-5 col-xs-12">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131100") %></label>
                            </div>
                            <div class="col-lg-8 col-md-7 col-xs-12">
                                <span id="productprice" runat="server"></span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-md-5 col-xs-12">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></label>
                            </div>
                            <div class="col-lg-8 col-md-7 col-xs-12">
                                <asp:TextBox ID="txtnNumber" runat="server" OnKeyPress="return chkNumberProduct(this);"
                                    OnTextChanged="txtnNumber_TextChanged" AutoPostBack="True" ValidationGroup="add"
                                    Width="100%" Visible="False" CssClass='form-control' Style="width: 100%;">0</asp:TextBox>
                                <asp:Label ID="lblAmount" runat="server">0</asp:Label>
                                <%--<asp:Button ID="btnAdd" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131101") %>" Width="100px" ValidationGroup="add" />--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-md-5 col-xs-12">
                                <label class="pull-right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501013") %></label>
                            </div>
                            <div class="col-lg-8 col-md-7 col-xs-12">
                                <asp:TextBox ID="txtnMoney" runat="server" ReadOnly="True" Style="font-size: 60px; width: 100%; text-align: right;"
                                    CssClass='form-control'>0</asp:TextBox>
                            </div>
                        </div>
                        <div class="row mini--space__top pagecashsales-button-segment">
                            <div class="col-sm-12">
                                <asp:Button ID="btnSave" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>(Ctrl+Shift)" class="btn btn-success global-btn"
                                    UseSubmitBehavior="False" OnClientClick="blockUI();" />
                                <asp:Button ID="btnCancel2" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131104") %>" class="btn btn-primary global-btn"
                                    Visible="false" UseSubmitBehavior="False" />
                                <asp:Button ID="btnCancel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>(Esc)" class="btn btn-danger global-btn"
                                    TabIndex="100" Visible="True" UseSubmitBehavior="False" />
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
                                    <%-- <asp:TemplateColumn HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtdgdnNumber" runat="server" Width="50px" AutoPostBack="true" CommandName="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
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
                                        <HeaderStyle Width="18%" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></asp:LinkButton>
                                            <asp:LinkButton ID="lbkEdit" runat="server" CommandName="Edit"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="12%" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
