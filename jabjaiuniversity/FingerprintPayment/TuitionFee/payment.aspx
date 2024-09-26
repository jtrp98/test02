<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="payment.aspx.cs" Inherits="FingerprintPayment.TuitionFee.payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Scripts/jsgrid/jsgrid-theme.css" rel="stylesheet" />
    <script src="../Scripts/jsgrid/jsgrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap-dialog.js" type="text/javascript"></script>
    <link href="../Content/bootstrap-dialog.css" rel="stylesheet" />

    <style type="text/css">
        .jsgrid-header-cell {
            font-size: 26px;
            background-color: #337AB7 !important;
            color: white;
            text-align: center;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var td_rows;
        $(function () {
            $('#price').number(true, 2);

            $("#modalpopup-data-submit").click(function () {
                var data = {
                    "payment_name": $("#payment_name").val(), "payment_id": $("#payment_id").val(),
                    "price": $("#price").val(), "productCode": $("#productCode").val(),
                    "productType": $("#productType").val()
                };

                PageMethods.updatedata(data, function (result) {
                    if ($("#modalpopup-data h1").html() === "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00141") %>") {
                        td_rows.children("td.payment_name").html(data.payment_name);
                        td_rows.children("td.price").html($.number(data.price, 2));
                        $("#modalpopup-data").modal("hide");
                    } else {
                        window.location.href = "/TuitionFee/payment.aspx";
                    }
                },
                    function (result) {
                        alert(result._meassage);
                    })
            });

            $("#modalpopup-data-cancel").click(function () {
                $("#modalpopup-data").modal("hide");
                clearinput();
            });
        })

        function getdata(payment_id, control) {
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00141") %>");
            PageMethods.getdata(payment_id,
                function (result) {
                    console.log(result);
                    $("#modalpopup-data").modal("show");
                    $("#payment_id").val(result.payment_id);
                    $("#payment_name").val(result.payment_name);
                    $("#price").val(result.price);
                    $("#productCode").val(result.productCode);
                    $("#productType").val(result.productType);
                    td_rows = ($(control).parent().parents("tr"));
                },
                function (result) {
                    console.log(result);
                })
        }

        function DelteData(payment_id) {
            var dialog = BootstrapDialog.show({
                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01468") %>',
                closable: false,
                message: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01822") %>',
                buttons: [{
                    label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>',
                    cssClass: 'btn-primary',
                    action: function (dialog) {
                        PageMethods.delete(payment_id, function (e) {
                            console.log(e);
                            if (e == "Success") {
                                dialog.close();
                                window.location.href = window.location;
                            } else {
                                popupSystemMessage();
                            }
                        });
                    }
                },
                {
                    label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                    action: function (dialog) {
                        dialog.close();
                    }
                }
                ]
            });

            dialog.getModalDialog().css("margin-top", "20%");
            dialog.open();
        }

        function popupSystemMessage() {
            BootstrapDialog.show({
                title: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h3>',
                closable: false,
                message: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133363") %>',
                buttons: [{
                    label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>',
                    cssClass: 'btn-primary',
                    action: function (dialog) {
                        dialog.close();
                    }
                }]
            });
        }

        function popupadd() {
            clearinput();
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01243") %>");
            $("#modalpopup-data").modal("show");
        }

        function clearinput() {
            $("#payment_id").val("0");
            $("#payment_name").val("");
            $("#price").val("0");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="full-card box-content employeeslist-container">
        <div class="row form-group">
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601030") %></label>
                </div>
                <div class="col-lg-8 col-md-7 col-sm-8">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass='form-control' Style="width: 100%;"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row form-group">
            <div class="col-sm-12 text-center">
                <asp:Button ID="Button1" class="btn btn-primary global-btn"
                    runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        ShowHeaderWhenEmpty="true"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />
                        <PagerTemplate>
                            <table width="100%" class="tab">
                                <tr>
                                    <td style="width: 25%">

                                        <asp:Label ID="Label1" BorderColor="#337AB7"
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
                            <asp:BoundField DataField="index" HeaderStyle-Width="100px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="center">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="payment_name" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %>" ItemStyle-CssClass="payment_name">
                                <HeaderStyle Width="40%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="price" DataFormatString="{0:#,##0.00}" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501008") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext price">
                                <HeaderStyle Width="30%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12">
                                        <div class="col-lg-4 col-md-4 col-sm-4 adjust-col-padding col-space nounder">
                                            <a class="glyphicon glyphicon-edit" style="font-size: 70%; cursor: pointer;" onclick="getdata(<%# Eval("payment_id") %>,this)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>"></a>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>">
                                                <a onclick="DelteData('<%# Eval("payment_id") %>')" class="glyphicon glyphicon-remove" style="font-size: 70%; color: red; cursor: pointer;"></a>
                                            </div>
                                        </div>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <a class="btn btn-success" style="cursor: pointer;" onclick="popupadd()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106153") %></a>
                                </HeaderTemplate>
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataFormatString="sFinger" HeaderText="sFinger" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="sEmp" HeaderText="sEmp" Visible="False" />
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalpopup" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <div class="col-xs-4">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %></label>
            </div>
            <div class="col-xs-8">
                <input type="hidden" id="payment_id" value="0" />
                <input type="text" id="payment_name" value="0" class="form-control" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <div class="col-xs-4">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501008") %></label>
            </div>
            <div class="col-xs-8">
                <input type="text" id="price" value="0" class="form-control" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <div class="col-xs-4">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501007") %></label>
            </div>
            <div class="col-xs-8">
                <input type="text" id="productCode" value="0" class="form-control" maxlength="20" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <div class="col-xs-4">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01013") %></label>
            </div>
            <div class="col-xs-8">
                <select id="productType" class="form-control">
                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503018") %></option>
                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503019") %></option>
                </select>
            </div>
        </div>
    </div>
</asp:Content>
