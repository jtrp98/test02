<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="paymentgroup.aspx.cs" Inherits="FingerprintPayment.TuitionFee.paymentgroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/bootstrap/bootstrap-chosen/chosen.jquery.js" type="text/javascript"></script>
    <link href="/bootstrap/bootstrap-chosen/bootstrap-chosen.css" rel="stylesheet" />
    <link href="/Content/bootstrap-checkbox.css" rel="stylesheet" />
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <%--<script src="../../Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        var td_rows;
        $(function () {
            $('.chosen-select').chosen();
            $("#aspnetForm").validate().destroy();
            PageMethods.paymentlist(
                function (result) {
                    result = $.parseJSON(result);
                    var template = $('#payment_template').html();
                    var data = [];
                    Mustache.parse(template);   // optional, speeds up future uses
                    var rendered = Mustache.render(template, result);
                    $('.payment-list').html(rendered);
                    $("input[id*=payment_id]").click(function () {
                        if ($(this).prop("checked") === true) {
                            getpaymentdata($(this).val());
                        }
                        else {
                            $("#table_payment tbody tr.payment_id-" + $(this).val()).remove();
                            UpdateTotalPrice();
                        }
                    });
                },
                function (e) {
                    console.log(e);
                });

            $("#btnsubmit").click(function () {
                if ($("#aspnetForm").valid() === false) return;
                if ($("#table_payment tbody").children("tr").length === 0) {
                    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133365") %>");
                    return;
                }

                var paymentgroup_list = [];
                $.each($("#table_payment tbody").children("tr"), function (e, s) {
                    var td = $(s).children("td");
                    paymentgroup_list.push({
                        payment_id: $(td[1]).find("input").attr("payment-id"),
                        price: $(td[1]).find("input").val()
                    });
                    $("#table_payment tbody tr").children();
                });

                var data = {
                    paymentgroup_id: $("#paymentgroup_id").val(), paymentgroup_name: $("#paymentgroup_name").val(),
                    paymentgroup_list: paymentgroup_list
                };

                PageMethods.updatepaymentgroup(data,
                    function (response) {
                        if (response === "Success") {
                            if (data.paymentgroup_id === "0") {
                                window.location.href = "/TuitionFee/paymentgroup.aspx";
                            } else {
                                td_rows.find(".paymentgroup_name").html($("#paymentgroup_name").val());
                                td_rows.find(".totalprice").html($("#total_price").html());
                                $(".group-data").hide();
                                $(".group-list").show();
                                document.getElementById(td_rows.find("label").attr("id")).scrollIntoView(true);
                            }
                        }
                    },
                    function (e) {

                    });
            });

            $("#btncancel").click(function () {
                $(".group-data").hide();
                $(".group-list").show();
                if (td_rows.find("label").length !== 0) document.getElementById(td_rows.find("label").attr("id")).scrollIntoView(true);
                else window.scrollTo(0, 0);
            });

            $("#modalconfirm-delete-confirm").click(function () {
                PageMethods.deletedata($("#modalconfirm-delete #deleteid").val(),
                    function (e) {
                        if (e === "Success") {
                            window.location.href = "/TuitionFee/paymentgroup.aspx";
                        }
                    },
                    function (e) {
                        console.log(e);
                    });
            });
        });

        function getpaymentdata(payment_id) {
            PageMethods.getpaymentdata(payment_id,
                function (response) {
                    var table_payment = $("#table_payment tbody");
                    response = $.parseJSON(response);
                    var template = $('#payment-list_template').html();
                    var data = [];
                    Mustache.parse(template);   // optional, speeds up future uses
                    var rendered = Mustache.render(template, response);
                    table_payment.append(rendered);
                    $("[name=payment_values]").change(function () {
                        UpdateTotalPrice();
                    });
                    UpdateTotalPrice();
                    $('.price').number(true, 2);
                },
                function (e) {

                });
        }

        function UpdateTotalPrice() {
            var total_price = 0.0;
            $.each($("[name=payment_values]"), function (e, s) {
                total_price += parseFloat($(s).val());
            });
            $("#total_price").number(total_price, 2);
        }

        function adddata(control) {
            td_rows = $($(control).parent().parents("tr"));
            var table_payment = $("#table_payment tbody");
            table_payment.find("tr").remove();
            $(".payment-list").find("input").prop("checked", false);
            $("#paymentgroup_name").val("");
            $("#paymentgroup_id").val("0");
            $(".group-data").show();
            $(".group-list").hide();
        }

        function getdata(paymentgroup_id, control) {
            td_rows = $($(control).parent().parents("tr"));
            PageMethods.getdata(paymentgroup_id,
                function (response) {
                    response = $.parseJSON(response);
                    console.log(response);
                    $("#paymentgroup_name").val(response.group_name);
                    $("#paymentgroup_id").val(response.group_id);
                    var table_payment = $("#table_payment tbody");
                    table_payment.find("tr").remove();
                    $(".payment-list").find("input").prop("checked", false);
                    var template = $('#payment-list_template').html();
                    var data = [];
                    Mustache.parse(template);   // optional, speeds up future uses
                    $.each(response.group_list, function (s, e) {
                        var rendered = Mustache.render(template, e);
                        table_payment.append(rendered);
                        $(".payment-list").find("input[value='" + e.payment_id + "']").prop("checked", true);
                    });
                    $("[name=payment_values]").change(function (e) {
                        e.isDefaultPrevented();
                        UpdateTotalPrice();
                    });
                    $('.price').number(true, 2);
                    window.scrollTo(0, 0);
                    UpdateTotalPrice();
                    $(".group-data").show();
                    $(".group-list").hide();
                },
                function (e) {
                    console.log(e);
                });
        }

        function deletedata(paymentgroup_id) {
            $("#modalconfirm-delete").modal("show");
            $("#modalconfirm-delete .modal-header h3").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01468") %>");
            $("#modalconfirm-delete .modal-body").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133366") %>");
            $("#deleteid").val(paymentgroup_id);
        }

    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="full-card box-content employeeslist-container group-list">
        <div class="row form-group">
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00248") %></label>
                </div>
                <div class="col-lg-8 col-md-7 col-sm-8">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass='form-control' Style="width: 100%;"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row form-group">
            <div class="col-sm-12 text-center">
                <asp:Button ID="Button1" class="btn btn-primary global-btn"
                    runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" OnClick="Button1_Click" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" ShowHeaderWhenEmpty="true"
                        Font-Strikeout="False" Font-Underline="False" PageSize="5" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />
                        <PagerTemplate>
                            <table width="100%" class="tab" style="border-collapse: separate;">
                                <tr>
                                    <td style="width: 40%">
                                        <asp:Label ID="Label1" BorderColor="#337AB7"
                                            ForeColor="white"
                                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>:"
                                            runat="server" />
                                        <asp:DropDownList ID="PageDropDownList2"
                                            AutoPostBack="true" OnSelectedIndexChanged="PageDropDownList2_SelectedIndexChanged"
                                            runat="server" />

                                    </td>
                                    <td style="width: 20%">
                                        <asp:LinkButton ID="backbutton" runat="server"
                                            CssClass="imjusttext label" OnClick="backbutton_Click">
                               <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></label> 
                                        </asp:LinkButton>
                                        <asp:DropDownList ID="PageDropDownList"
                                            AutoPostBack="true"
                                            runat="server" OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged" />
                                        <asp:LinkButton ID="nextbutton" runat="server"
                                            CssClass="imjusttext label" OnClick="nextbutton_Click">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 40%; text-align: right">
                                        <asp:Label ID="CurrentPageLabel"
                                            ForeColor="white"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </PagerTemplate>
                        <Columns>
                            <%--   <asp:BoundField DataField="" HeaderStyle-Width="100px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>--%>
                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <label id='tabindex-<%# Container.DataItemIndex + 1 %>'>
                                        <%# Container.DataItemIndex + 1 %>
                                    </label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>
                                </HeaderTemplate>
                                <HeaderStyle Width="10%" CssClass="center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="paymentgroup_name" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501012") %>" ItemStyle-CssClass="paymentgroup_name center">
                                <HeaderStyle Width="40%" CssClass="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totalprice" DataFormatString="{0:#,##0.00}" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501013") %>" ItemStyle-CssClass="totalprice right">
                                <HeaderStyle Width="40%" CssClass="center" />
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12">
                                        <div class="col-lg-4 col-md-4 col-sm-4 adjust-col-padding col-space nounder">
                                            <a class="glyphicon glyphicon-edit" style="font-size: 70%; cursor: pointer;" onclick="getdata(<%# Eval("paymentgroup_id") %>,this)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>"></a>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>">
                                                <a class="glyphicon glyphicon-remove" style="font-size: 70%; color: red; cursor: pointer;" onclick="deletedata('<%# Eval("paymentgroup_id") %>')"></a>
                                            </div>
                                        </div>
                                </ItemTemplate>

                                <HeaderTemplate>
                                    <a class="btn btn-success" style="cursor: pointer;" onclick="adddata()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01232") %></a>
                                </HeaderTemplate>
                                <HeaderStyle Width="20%" />
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
    <br />
    <div class="full-card box-content group-data" style="display: none;">
        <div class="row">
            <div class="col-xs-12 col-md-12">
                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02112") %></h1>
            </div>
        </div>
        <div class="full-card" style="border: solid; padding: 10px 0 0 0;">
            <div class="row" style="margin: 0px;">
                <div class="col-xs-12 col-md-2">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107039") %>
                </div>
                <div class="col-xs-12 col-md-4">
                    <input type="text" id="paymentgroup_name" class="form-control" required />
                    <input type="hidden" id="paymentgroup_id" value="0" />
                </div>
                <div class="col-xs-12 col-md-4">
                    <%--<asp:DropDownList ID="ddlgroup" runat="server" class="form-control chosen-select" />--%>
                </div>
                <div class="col-xs-12 col-md-2"></div>
            </div>
            <div class="row--space"></div>
            <div class="row" style="border-top: solid; margin: 0 0;">
                <div class="col-xs-6 col-md-6" style="">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00575") %>
                    <div class="row">
                        <div class="col-xs-12 col-md-12 payment-list">
                            <script id="payment_template" type="x-tmpl-mustache">
                                {{#data}}
                                <div class="checkbox checkbox-primary">
                                    <input id="payment_id_{{payment_id}}" value="{{payment_id}}" class="styled" type="checkbox" />
                                    <label for="checkbox2">
                                    {{payment_name}}
                                    </label>
                                </div>
                                {{/data}}
                            </script>
                        </div>
                    </div>
                </div>
                <div class="col-xs-6 col-md-6" style="border-left: solid;">
                    <div class="row" style="min-height: 300px;">
                        <table class="table" id="table_payment">
                            <thead>
                                <tr>
                                    <td style="width: 30% !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00575") %></td>
                                    <td style="width: 70% !important;" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502004") %></td>
                                </tr>
                            </thead>
                            <tbody>
                                <script id="payment-list_template" type="x-tmpl-mustache">
                                    <tr class="payment_id-{{payment_id}}">
                                    <td>
                                        {{payment_name}}
                                    </td>
                                    <td>
                                        <div class="form-group">
                                            <div class="col-sm-10">
                                                <input type="text" name="payment_values" value="{{price}}" payment-id="{{payment_id}}" 
                                                class="form-control price text-right" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501008") %>" aria-label="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501008") %>" aria-describedby="basic-addon1" required >
                                            </div>
                                            <label class="col-sm-2 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></label>
                                        </div>
                                    </tr>
                                </script>
                            </tbody>
                            <tfoot>
                            </tfoot>
                        </table>
                    </div>
                    <div class="row" style="border-top: 1px solid #ddd;">
                        <div class="col-xs-12 col-md-6">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01348") %>
                        </div>
                        <div class="col-xs-12 col-md-6 text-right">
                            <label id="total_price">0</label>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row text-center" style="border-top: solid; padding: 10px 0 0 0; margin: 0px; padding: 10px;">
                <div class="col-xs-12 col-md-12">
                    <div class="btn btn-success" id="btnsubmit"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></div>
                    <div class="btn btn-danger btn-xl" id="btncancel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
