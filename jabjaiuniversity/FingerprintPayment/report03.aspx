<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="report03.aspx.cs" Inherits="FingerprintPayment.report03" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }

        .listItem {
            color: blue;
            background-color: White;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }
    </style>
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="app/Reports/SettingControlJS.js" type="text/javascript"></script>
    <script src="../app/Reports/ReportBuyItem.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdfschoolname" runat="server" />
    <div class="report-card box-content">
        <div class="row">
            <div class="col-xs-12 col-md-2">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131112") %> :</label>
            </div>
            <div class="col-xs-12 col-md-4">
                <select id="sell_type" class="form-control">
                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206621") %></option>
                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206620") %></option>
                </select>
            </div>
            <div class="col-xs-12 col-md-2 type_sell" style="display: none;">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %> :</label>
            </div>
            <div class="col-xs-12 col-md-4 type_sell" style="display: none;">
                <asp:DropDownList ID="ddlSearch" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"></asp:ListItem>
                    <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131115") %>"></asp:ListItem>
                    <asp:ListItem Value="2" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01131") %>"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row hidden type_row_0">
            <div class="col-xs-12 col-md-2">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %> :<span style="color: red;"></span></label>
            </div>
            <div class="col-xs-12 col-md-4">
                <select name="selecttype" class="form-control">
                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></option>
                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %></option>
                </select>
            </div>
            <div class="col-xs-6 col-md-12">
            </div>
        </div>
        <div class="row hidden type_row_1">
            <div class="col-xs-12 col-md-2">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> :<span style="color: red;"></span></label>
            </div>
            <div class="col-xs-12 col-md-4">
                <select name="selectlevel" class="form-control dropdown">
                </select>
            </div>
            <div class="col-xs-12 col-md-2">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> :<span style="color: red;"></span></label>
            </div>
            <div class="col-xs-12 col-md-4">
                <select name="selectsublevel" class="form-control">
                </select>
            </div>
        </div>
        <div class="row hidden type_row_2">
            <div class="col-xs-12 col-md-2">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %> :<span style="color: red;"></span></label>
            </div>
            <div class="col-md-4 col-xs-12">
                <input type="text" name="sname" class="form-control" />
                <input type="hidden" name="iduser" class="form-control" />
            </div>
            <div class="col-xs-6 col-md-12">
            </div>
        </div>
        <div class="row row--space hidden" id="sname">
            <div class="col-md-2 col-xs-12">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %> :</label>
            </div>
            <div class="col-xs-8 col-md-12">
                <asp:TextBox ID="txtsunit" runat="server" CssClass='form-control' Style="width: 300px;" />
            </div>
        </div>
        <div class="row row--space">
            <div class="col-md-2 col-xs-12">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :</label>
            </div>
            <div class="col-md-4 col-xs-12">
                <input type="text" id="txtdStart" class="form-control datepicker" />
                <%--<div class="input-group">
                    <div class="input-group-addon">
                        <i class="glyphicon glyphicon-calendar"></i>
                    </div>
                </div>--%>
            </div>
            <div class="col-md-2 text-center col-xs-12">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %> :</label>
            </div>
            <div class="col-md-4 col-xs-12">
                <input type="text" id="txtdEnd" class="form-control datepicker" />
                <%--<div class="input-group">
                    <div class="input-group-addon">
                        <i class="glyphicon glyphicon-calendar"></i>
                    </div>
                </div>--%>
            </div>
        </div>
        <div class="row row--space">
            <div class="col-xs-12 col-md-2">
                <label class="pull-right label--filter">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603091") %> :</label>
            </div>
            <div class="col-xs-12 col-md-4">
                <asp:DropDownList class="form-control dropdown" ID="ddlcType" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-xs-12 col-md-2">
                <label class="pull-right label--filter">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %> :
                </label>
            </div>
            <div class="col-xs-12 col-md-4">
                <asp:TextBox ID="txtSearch" CssClass='form-control' runat="server" />
                <input type="hidden" name="productid" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12 center">
                <input type="button" id="btnSearch" class="btn btn-primary col-xs-12 col-md-12" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="gentablesum()" />
                <%--    <asp:Button ID="btnSearch0" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" class="btn btn-primary" />
                &nbsp;<asp:Button ID="btnSearch2" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132151") %>" class="btn btn-danger"
                    PostBackUrl="~/reportmain.aspx" />
                &nbsp;<input type="button" value="Print" class="btn btn-success" onclick="PrintElem('#divprint');" />--%>
            </div>
        </div>
        <div class="row row--space__top">
            <div class="col-lg-9">
            </div>
            <div class="col-md-3 col-xs-12">
                <div class="btn btn-success button-custom col-xs-12 col-md-12" id="exportfile">Export File</div>
            </div>
        </div>
        <div class="row mini--space__top">
            <asp:Literal ID="ltrHtml" runat="server" />
            <div class="col-xs-12">
                <div id="divprint">
                </div>
                <table style="width: 100%" class='table table-condensed table-bordered' id="dailySales">
                    <thead class="hidden"></thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
