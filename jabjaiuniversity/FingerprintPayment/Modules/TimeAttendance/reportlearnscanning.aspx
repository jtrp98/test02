﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="reportlearnscanning.aspx.cs"
    Inherits="FingerprintPayment.Modules.TimeAttendance.reportlearnscanning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../Styles/jquery-ui.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../../Scripts/Easy-Customizable-Loading/jquery.mloading.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="../../app/Reports/ReportLearnScanningJS/ReportLearnScanningJS.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>

    <style type="text/css">
        @media (max-width: 999px) {
            .report-container {
                font-size: 18px;
            }

            label {
                font-weight: normal;
                font-size: 18px;
            }

            legend {
                padding-left: 30px;
                font-size: 18px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 18px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }

            .table-show-result {
                font-size: 20px;
            }
        }

        @media (min-width: 1000px) and (max-width: 1199px) {
            .report-container {
                font-size: 22px;
            }

            label {
                font-weight: normal;
                font-size: 22px;
            }

            legend {
                padding-left: 30px;
                font-size: 22px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 22px;
                width: 100%;
                padding-left: 30px;
                padding-right: 30px;
            }

            .table-show-result {
                font-size: 22px;
            }
        }

        @media (min-width: 1200px) {
            .report-container {
                font-size: 26px;
            }

            label {
                font-weight: normal;
                font-size: 26px;
            }

            legend {
                padding-left: 30px;
                font-size: 26px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 26px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }

            .table-show-result {
                font-size: 26px;
            }
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .centerText {
            text-align: center;
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdfschoolname" runat="server" />
    <div class="report-container">
        <%--      <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %> :
                </label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="dllTypeReport" runat="server" class="form-control">
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %>" Value="1" Selected="True" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803012") %>" Value="2" Selected="False" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-sm-6">
            </div>
        </div>--%>
        <div class="row student">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlyear" runat="server" class="form-control">
                        <asp:ListItem Text="2558" Value="2557" Selected="True" />
                        <asp:ListItem Text="2559" Value="2557" Selected="False" />
                        <asp:ListItem Text="2560" Value="2557" Selected="False" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="semister" runat="server" class="form-control">
                        <asp:ListItem Text="1" Value="1" Selected="True" />
                        <asp:ListItem Text="2" Value="2" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row student">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlsublevel" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlSubLV2" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <%--        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlPlaneID" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>--%>
        <%--        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" class='form-control' id="txtid" style="display: none;" />
                    <input type="text" class='form-control' id="txtname" />
                </div>
            </div>
        </div>--%>
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" id="txtstart" readonly="true" class="form-control datepicker" />
                    <%--<div class="input-group">
                        <div class="input-group-addon">
                            <i class="glyphicon glyphicon-calendar"></i>
                        </div>
                    </div>--%>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" id="txtend" class="form-control datepicker" readonly="true" />
                    <%--<div class="input-group">
                        <div class="input-group-addon">
                            <i class="glyphicon glyphicon-calendar"></i>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
        <%--     <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="status" runat="server" class="form-control">
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" Selected="True" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>" Value="0" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>" Value="1" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>" Value="3" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>--%>
        <div class="row">
            <div class="col-sm-12">
                <input type="button" class="btn btn-primary button-custom" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="reportHeader();" />
            </div>
        </div>
        <div class='row hidden' style="font-weight: bolder; font-size: 40px;">
            <br />
            <fieldset>
                <legend><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M406001") %></legend>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-success'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %><br />
                        <span class='text-large' id="status01">0</span>
                    </p>
                </div>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-warning'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %><br />
                        <span class='text-large' id="status02">0</span>
                    </p>
                </div>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-danger'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %><br />
                        <span class='text-large' id="status03">0</span>
                    </p>
                </div>
            </fieldset>
        </div>
        <div class="row--space">
        </div>
        <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8">
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2">
                 <div class="btn btn-success button-custom" id="exportfile">Export File</div>
            </div>
        </div>
        <asp:Literal ID="ltrHeaderReport" runat="server" />
        <div class="row border-bottom">
            <br />
            <div class="col-sm-12">
                <fieldset>
                    <asp:ListView ID="lvReport" runat="server">
                    </asp:ListView>
                    <table id="example" class="table table-condensed table-bordered table-show-result"
                        cellspacing="0" width="100%">
                        <thead id="myHeader"hidden></thead>
                        <tbody id="myTable">
                        </tbody>
                    </table>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
