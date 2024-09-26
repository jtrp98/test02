<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="roomlist2.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.roomlist2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            getPermission();
            $('input[id*=btnDel]').click(function () {
                showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('name')); return false;
            });
        })
        function getPermission() {
            PageMethods.GetPermission(OnMyMethodComplete);
        }
        function OnMyMethodComplete(result, userContext, methodName) {
            if (result == 1) {
                $('.col3').css("display", "none");
            }
        }

    </script>
    <div class="full-card  box-content col3 hidden">
        <div class="row">
            <div class="col-lg-3">
            </div>
            <div class="col-lg-1">
                <label>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801045") %></label>
            </div>
            <div class="col-lg-5">
                <asp:TextBox ID="txtsClass" CssClass="form-control" runat="server" />
                <asp:HiddenField ID="hfdsClassID" runat="server" />
                <asp:RequiredFieldValidator ID="revtxtsClass" runat="server" Display="None" SetFocusOnError="true"
                    ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801045") %><br>"
                    ControlToValidate="txtsClass" ValidationGroup="add"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender runat="server" ID="vcerevtxtsClass" TargetControlID="revtxtsClass"
                    HighlightCssClass="validatorcallouthighlight" Width="200px" />
            </div>
            <div class="col-lg-3">
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3">
            </div>
            <div class="col-lg-1">
                <label>IP</label>
            </div>
            <div class="col-lg-5">
                <asp:TextBox ID="txtsClassIP" CssClass="form-control" runat="server" />
                <asp:RequiredFieldValidator ID="reqtxtsClassIP" runat="server" Display="None" SetFocusOnError="true"
                    ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br />IP<br>" ControlToValidate="txtsClassIP"
                    ValidationGroup="add"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender runat="server" ID="vcereqtxtsClassIP" TargetControlID="reqtxtsClassIP"
                    HighlightCssClass="validatorcallouthighlight" Width="200px" />
            </div>
            <div class="col-lg-3">
            </div>
        </div>
        <div class="row--space">
            &nbsp;<br />
        </div>
        <div class="row">
            <div class="col-md-12 text-center vertical-mid">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success custom-btn" ValidationGroup="add"
                    Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                <asp:Button ID="btnCancel" CssClass="btn btn-danger custom-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
            </div>
        </div>
    </div>
    <div class="row--space">
    </div>
    <div class="full-card  box-content">
        <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" CssClass="cool-table"
            GridLines="None" Width="100%">
            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
            <Columns>
                <asp:BoundColumn DataField="sClass" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801045") %>" ItemStyle-Width="60%"></asp:BoundColumn>
                <asp:BoundColumn DataField="sClassIP" HeaderText="IP" ItemStyle-Width="60%" Visible="false" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <%--<a href="roomadd.aspx?id=<%# Eval("sClassID") %>" data-toggle="modal" data-target="#modalpopupdata" class="btn btn-primary button-fixed-width"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></a>--%>
                        <asp:LinkButton ID="lnkEdit" CssClass="btn btn-primary" runat="server" CommandArgument='<%# Eval("sClassID") %>'
                            CommandName="Edit" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>" Visible="false" />
                   <%--     <asp:Button ID="btnDel" runat="server" CommandArgument='<%# Eval("sClassID") %>'
                            CommandName="Del" CssClass="btn btn-danger button-fixed-width" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>" OnClientClick="return false;" />--%>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <a href="roomadd.aspx" class="btn btn-info button-fixed-width" data-toggle="modal" data-target="#modalpopupdata"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></a>
                    </HeaderTemplate>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>