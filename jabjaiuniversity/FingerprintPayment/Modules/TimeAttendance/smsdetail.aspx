<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="smsdetail.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.smsdetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../javascript/smsdetail.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="detail-card box-content smsadd-container periodadd-container">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePartialRendering="false">
        </asp:ScriptManager>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401047") %></label>
            </div>
            <div class="col-xs-4">
                <span id="type"></span>
            </div>
        </div>
        <div class="row" id="subtype">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132661") %></label>
            </div>
            <div class="col-xs-8">
                <span id="texttype"></span>
            </div>
        </div>
        <div class="row" id="datesection">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132657") %></label>
            </div>
            <div class="col-xs-8">
                <span id="daysend"></span>
            </div>
        </div>
        <div class="row" id="timesection">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %></label>
            </div>
            <div class="col-xs-8">
                <span id="timesend"></span>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132662") %></label>
            </div>
            <div class="col-xs-8">
                <span id="useradd"></span>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202042") %></label>
            </div>
            <div class="col-xs-8">
                <table class="table">
                    <tbody id="filerows">
                    </tbody>
                </table>
                <asp:Literal ID="ltrfile" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %> </label>
            </div>
            <div class="col-xs-8">
                <span id="text"></span>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <table id="tablelistuser" class="table table-striped col-lg-12 ">
                    <thead class="table-tab">
                        <tr>
                            <td style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %></td>
                            <td style="width: 60%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></td>
                            <td style="width: 15%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></td>
                            <td style="width: 15%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td>
                        </tr>
                    </thead>
                    <tbody style="height: 400px; overflow-y: scroll;">
                    </tbody>
                </table>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row mini--space__top">
                    <div class="col-xs-12 center">
                        &nbsp;<asp:Button ID="btnDelete" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" class="btn btn-danger global-btn" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>" class="btn btn-default global-btn" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
