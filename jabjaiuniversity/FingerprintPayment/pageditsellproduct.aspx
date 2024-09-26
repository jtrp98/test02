<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="pageditsellproduct.aspx.cs" Inherits="FingerprintPayment.pageditsellproduct" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <object id="objSecuBSP" classid="CLSID:6283f7ea-608c-11dc-8314-0800200c9a66" height="0"
        name="objSecuBSP" style="left: 0px; top: 0px" viewastext="" width="0">
    </object>
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
    shortcut.add("Ctrl+S", function () {
        $('input[id*=btnSave]').click();
    });
    shortcut.add("Ctrl+P", function () {
        fnCapture();
    });
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
                return false;
            }
        });
    });

    </script>
    <script language="Javascript" type="text/javascript">
        //$(document).ready(function () {
        //    fnOpenDevice();
        //});

        //setInterval(function () { fnCapture(0, 'SellEditProduct'); }, 1000);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:MultiView ID="mtvsell" runat="server" ActiveViewIndex="1" OnActiveViewChanged="mtvsell_ActiveViewChanged">
        <asp:View ID="pagesell" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="full-card box-content">
                <div class="row">
                    <div class="col-xs-12 center">
                        <asp:Label runat="server" ID="lblsName" class="pull-right" />
                    </div>
                    <div class="col-xs-12">
                        <div class="wrapper-table">
                            <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
                                GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                                <Columns>
                                    <asp:BoundColumn DataField="dSell" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>"
                                        DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="25%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="nTotal" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501013") %>">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="25%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <%-- <asp:TemplateColumn HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %>">
                                            <ItemTemplate>
                                                <asp:Label ID="txtdgdnNumber" runat="server" Width="50px" AutoPostBack="true" CommandName="Edit" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                                    <asp:BoundColumn DataField="sSellID" HeaderText="nProductID" Visible="False" />
                                    <asp:TemplateColumn HeaderText="">
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></asp:LinkButton>--%>
                                            <asp:LinkButton ID="lbkEdit" runat="server" CommandName="Edit"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15%" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <FooterStyle BackColor="Tan" />
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
                <div class="row center mini--space__top">
                    <asp:Button ID="btnCancel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>(Esc)" class="btn btn-danger"
                        TabIndex="100" Visible="False" UseSubmitBehavior="False" />
                    <asp:Button ID="btnSave" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>(Ctrl+S)" class="btn btn-success"
                        UseSubmitBehavior="False" Visible="false" OnClientClick="$('.se-pre-con').show();" />
                    &nbsp;<asp:Button ID="btnCancel2" runat="server" Text="ยอนกลับ(Ctrl+B)" class="btn btn-primary"
                        UseSubmitBehavior="False" />
                </div>
            </div>
        </asp:View>
        <asp:View ID="submitbuy" runat="server">
            <div class="detail-card detail-card-2 box-content pageeditsellproduct-container">
                <asp:Literal ID="ltrjavascript" runat="server" />
                <p class="tbheader">
                    <div class="row" style="display: none;">
                        <div class="col-xs-4">
                            <label class="pull-right">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %></label>
                        </div>
                        <div class="col-xs-8">
                            <%----%>
                            <asp:Button ID="btnRegister" runat="server" class="btn btn-info" Text="ตรวจสอบรายนิ้วมือ(Ctrl+P)"
                                OnClientClick="fnCapture(); return false;" />
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
                        <div class="col-xs-3">
                            <label class="pull-right">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></label>
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtsID" runat="server" MaxLength="4" CssClass='form-control'/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3">
                            <label class="pull-right">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtsName" runat="server" MaxLength="512" ReadOnly="True" CssClass='form-control' />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3">
                            <label class="pull-right">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtsLastName" runat="server" ReadOnly="True" CssClass='form-control' />
                        </div>
                    </div>
                    <div class="row mini--space__top button-section">
                        <div class="col-xs-12">
                            <asp:Button ID="btnClear" runat="server" AccessKey="N" OnKeyPress="return fales;"
                                OnClientClick="Clear(); return fales;" TabIndex="100" Text="Clear(Ctrl+C)" UseSubmitBehavior="False"
                                class="btn btn-danger global-btn" />
                            <asp:Button ID="btnSavepage1" runat="server" AccessKey="N" OnKeyPress="return fales;"
                                TabIndex="100" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>(Ctrl+N)" UseSubmitBehavior="False" class="btn btn-primary global-btn"
                                Style="display: none;" />
                        </div>
                    </div>
            </div>
        </asp:View>
        <asp:View ID="pageeditsell" runat="server">
            <div class="full-card box-content">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="wrapper-table">
                            <asp:DataGrid ID="dgdEdit" runat="server" AutoGenerateColumns="False" Width="100%"
                                CellPadding="2" GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" PageSize="20"
                                CssClass="cool-table">
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
                                    <asp:BoundColumn DataField="nNumber" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %>"
                                        Visible="False">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="nProductID" HeaderText="nProductID" Visible="False" />
                                    <asp:TemplateColumn HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %>">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumber" runat="server" Text='<%# Eval("nNumber") %>'>' ></asp:Label>
                                            <asp:TextBox ID="txtNumber" runat="server" Width="50px" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="nPrice" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404034") %>">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="nTotal" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131102") %>">
                                        <HeaderStyle Width="15%" />
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></asp:LinkButton>
                                            <%--<asp:LinkButton ID="lbkEdit" runat="server" CommandName="Edit"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></asp:LinkButton>--%>
                                            <asp:LinkButton ID="lbkConform" runat="server" CommandName="Conform" Visible="false"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15%" />
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
                <div class="row center mini--space__top">
                    <asp:Button ID="btnCancelEidt" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>(Esc)" class="btn btn-danger"
                        TabIndex="100" Visible="False" UseSubmitBehavior="False" />
                    <asp:Button ID="btnSaveEdit" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>(Ctrl+S)" class="btn btn-success"
                        UseSubmitBehavior="False" Visible="true" />
                    &nbsp;<asp:Button ID="btnBackEdit" runat="server" Text="ยอนกลับ(Ctrl+B)" class="btn btn-primary"
                        UseSubmitBehavior="False" />
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
