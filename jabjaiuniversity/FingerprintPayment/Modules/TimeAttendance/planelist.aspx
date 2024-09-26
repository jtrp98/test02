<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="planelist.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.planelist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var td_rows;
        var mode = "Add";
        $(function () {
            $("#modalpopup-data-submit").click(function () {
                var data = {
                    "plane_id": $("#plane_id").val(), "plane_name": $("#plane_name").val(),
                    "mode": mode
                };
                PageMethods.updatedata(data, function (result) {
                    if ($("#modalpopup-data h1").html() === "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132621") %>") {
                        td_rows.children("td.plane_id").html(data.plane_id);
                        td_rows.children("td.plane_name").html(data.plane_name);
                        $("#modalpopup-data").modal("hide");
                        clearinput();
                    } else {
                        window.location.href = "/Modules/TimeAttendance/planelist.aspx";
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

        function getdata(title_id, control) {
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132621") %>");
            PageMethods.getdata(title_id,
                function (result) {
                    console.log(result);
                    $("#modalpopup-data").modal("show");
                    $("#plane_id").prop("readonly", true);
                    $("#plane_id").val(result.plane_id);
                    $("#plane_name").val(result.plane_name);
                    mode = result.mode;
                    td_rows = ($(control).parent().parents("tr"));
                },
                function (result) {
                    console.log(result);
                })
        }

        function popupadd() {
            clearinput();
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132620") %>");
            $("#modalpopup-data").modal("show");
        }

        function clearinput() {
            $("#plane_id").val("");
            $("#plane_name").val("");
            mode = "Add";
            $("#plane_id").prop("readonly", false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="row">
            <div class="col-xs-12 col-md-1">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
            </div>
            <div class="col-xs-12 col-md-4">
                <asp:TextBox CssClass="form-control col-md-4 col-xs-12" ID="txtSearch" runat="server"></asp:TextBox>
            </div>
            <div class="col-xs-12 col-md-6">
                <asp:Button CssClass="btn btn-primary global-btn" runat="server" ID="btnSearch" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
            </div>
        </div>
        <div class="row--space">
        </div>
        <div class="row">
            <div class="col-xs-12 col-md-12">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                </asp:ScriptManager>
                <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
                    GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table cool-table-4">
                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                    <Columns>
                        <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="rowindex" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                            <HeaderStyle></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="sPlaneID" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %>" ItemStyle-CssClass="plane_id">
                            <HeaderStyle></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="sPlaneName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %>" ItemStyle-CssClass="plane_name">
                            <HeaderStyle></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderStyle-CssClass="header-tb-color">
                            <ItemTemplate>
                                <%-- <p style="margin-left:0px;"> <asp:LinkButton  CssClass="btn btn-primary width200-px glyphicon glyphicon-list" ID="LinkButton2" runat="server" CommandName="Edit">&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131228") %></asp:LinkButton>
                   </p>--%>
                                <p style="margin-left: 0px;">
                                    <%--   <asp:LinkButton CssClass="btn btn-info width100-px glyphicon glyphicon-pencil"
                                    ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("sPlaneID") %>'>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></asp:LinkButton>--%>
                                    <a style="cursor: pointer;" class="btn btn-info minor-button btnpermission" onclick="getdata('<%# Eval("sPlaneID") %>',this)">
                                        <span class="glyphicon glyphicon-pencil global-btn"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>
                                    </a>
                                    <asp:LinkButton CssClass="btn btn-danger minor-button btnpermission" ID="btnDel"
                                        runat="server" CommandName="Del" CommandArgument='<%# Eval("sPlaneID") %>'><span class="glyphicon glyphicon-trash"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></asp:LinkButton>
                                </p>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <a class="btn btn-success btnpermission" style="cursor: pointer;" onclick="popupadd()"><span class="glyphicon glyphicon-plus"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></a>
                            </HeaderTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalpopup" runat="server">
    <div class="row planadd-row">
        <div class="col-xs-12">
            <div class="col-xs-3">
                <label>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %></label>
            </div>
            <div class="col-xs-8">
                <input class="form-control" type="text" id="plane_id" />
            </div>

        </div>
    </div>
    <div class="row planadd-row">
        <div class="col-xs-12">
            <div class="col-xs-3">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %></label>
            </div>
            <div class="col-xs-8">
                <input class="form-control" type="text" id="plane_name" />
            </div>

        </div>
    </div>
</asp:Content>
