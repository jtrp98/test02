<%@ Page Title="" Language="C#" MasterPageFile="~/mppopup.Master" AutoEventWireup="true"
    CodeBehind="roomadd.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.roomadd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
    <div class="full-card  box-content col3 roomadd-container">
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-3 text-right">
                <label>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801045") %></label>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8">
                <asp:TextBox ID="txtsClass" CssClass="form-control" runat="server" />
                <asp:HiddenField ID="hfdsClassID" runat="server" />
                <asp:RequiredFieldValidator ID="revtxtsClass" runat="server" Display="None" SetFocusOnError="true"
                    ErrorMessage="<span style='font-size:16'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801045") %><br>"
                    ControlToValidate="txtsClass" ValidationGroup="add"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender runat="server" ID="vcerevtxtsClass" TargetControlID="revtxtsClass"
                    HighlightCssClass="validatorcallouthighlight" Width="200px" />
            </div>
            <div class="col-lg-1 col-md-1 col-sm-1"></div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-3 text-right">
                <label>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132654") %></label>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8">
                <asp:TextBox ID="txtsClassIP" CssClass="form-control" ReadOnly runat="server" />
                <%--                <asp:RequiredFieldValidator ID="reqtxtsClassIP" runat="server" Display="None" SetFocusOnError="true"
                    ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br />IP<br>" ControlToValidate="txtsClassIP"
                    ValidationGroup="add"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender runat="server" ID="vcereqtxtsClassIP" TargetControlID="reqtxtsClassIP"
                    HighlightCssClass="validatorcallouthighlight" Width="200px" />--%>
            </div>
            <div class="col-lg-1 col-md-1 col-sm-1"></div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-3 text-right">
                <label>
                    MacAddress</label>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8">
                <asp:TextBox ID="TextBox1" CssClass="form-control" ReadOnly runat="server" />
            </div>
            <div class="col-lg-1 col-md-1 col-sm-1"></div>
        </div>
        <div class="row--space">
            &nbsp;<br>
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 text-center vertical-mid button-section">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success global-btn"
                    Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                <asp:Button ID="btnCancel" CssClass="btn btn-danger global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
