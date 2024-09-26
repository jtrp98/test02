<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="report02.aspx.cs" Inherits="FingerprintPayment.report02" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .completionList
        {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }
        
        .listItem
        {
            color: blue;
            background-color: White;
        }
        
        .itemHighlighted
        {
            background-color: #ffc0c0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtsunit"
            CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
            ServiceMethod="getAutoListTEmployees" ServicePath="AutoCompleteService.asmx" EnableCaching="true"
            FirstRowSelected="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
            CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="report-card box-content">
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105008") %></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtsunit" runat="server" class="input--mid"/>
            </div>
        </div>
        <div class="row row--space">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></label>
            </div>
            <div class="col-xs-8">
                <asp:DropDownList ID="ddlListMonth" runat="server" />
            </div>
        </div>
        <div class="row row--space">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %></label>
            </div>
            <div class="col-xs-8">
                <asp:DropDownList ID="ddlListYear" runat="server" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12 center">
                    <asp:Button ID="btnSearch" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131111") %>" class="btn btn-primary"/>
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" GroupTreeImagesFolderUrl=""
                Height="50px" ToolbarImagesFolderUrl="" ToolPanelWidth="200px" Width="350px"
                HasZoomFactorList="False" ToolPanelView="None" EnableDatabaseLogonPrompt="False"
                EnableParameterPrompt="False" HasPageNavigationButtons="False" HasToggleGroupTreeButton="False"
                HasToggleParameterPanelButton="False" EnableToolTips="False" HasDrilldownTabs="False"
                HasRefreshButton="True" HasSearchButton="False" HasCrystalLogo="False" SeparatePages="False" />
            <%--<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="Report\report02.rpt">
                </Report>
                </CR:CrystalReportSource>--%>
            </div>
        </div>
    <div>
    
</asp:Content>
