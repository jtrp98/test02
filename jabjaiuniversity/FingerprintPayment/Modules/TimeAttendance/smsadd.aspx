<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="smsadd.aspx.cs"
    Inherits="FingerprintPayment.Modules.TimeAttendance.smsadd" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="/javascript/smssetting.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $("")
        })
    </script>
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
    <div class="detail-card box-content smsadd-container periodadd-container">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePartialRendering="false">
        </asp:ScriptManager>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401047") %></label>
            </div>
            <div class="col-xs-4">
                <asp:DropDownList ID="ddlType" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401055") %></label>
            </div>
            <div class="col-xs-4">
                <asp:DropDownList ID="ddlAcceptType" ClientIDMode="Static" runat="server" CssClass="form-control">
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401022") %>" Value="0" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401023") %>" Value="1" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %>" Value="2" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401025") %>" Value="3" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401026") %>" Value="4" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401027") %>" Value="5" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401028") %>" Value="6" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401029") %>" Value="7" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="row hidden" id="subtype">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132661") %></label>
            </div>
            <div class="col-xs-8">
                <asp:DropDownList ID="ddlStype" ClientIDMode="Static" runat="server" CssClass="form-control" Style="width: 200px; height: 45px; font-size: 25px;"></asp:DropDownList>
            </div>
        </div>
        <div class="row" id="datesection">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132657") %></label>
            </div>
            <div class="col-xs-8">
                <div class="input-group" style="float: left;">
                    <input class="form-control datepicker" data-date-format="dd/mm/yyyy" aria-describedby="basic-addon2" id="dateSMS" style="width: 100px; height: 35px; float: left; font-size: 24px;" runat="server" />
                    <span class="input-group-addon" id="basic-addon2" style="float: left; min-height: 36px; min-width: 36px;"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                </div>
            </div>
        </div>
        <div class="row" id="timesection">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %></label>
            </div>
            <div class="col-xs-8">
                <div class="input-group clockpicker" style="float: left;" data-placement="left"
                    data-align="top" data-autoclose="true">
                    <input type="text" class="form-control mon clock-box" id="monstop1" runat="server" style="width: 100px; height: 35px; float: left; font-size: 24px;" />
                    <span class="input-group-addon" style="float: left; min-height: 36px; min-width: 36px;"><span class="glyphicon glyphicon-time"></span></span>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401046") %></label>
            </div>
            <div class="col-xs-4">
                <asp:DropDownList ID="ddlDuration" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="row hide">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M403005") %> SMS <span style="color: red;">*</span></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtTitle" runat="server" MaxLength="512" class="input--mid" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401048") %> <span style="color: red;">*</span></label>
            </div>
            <div class="col-xs-6">
                <label class="radio-inline">
                    <input type="radio" name="optradio" id="optradio_0" runat="server" style="position: unset; margin-right: 5px;" value="0" checked><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401031") %></label>
                <label class="radio-inline">
                    <input type="radio" name="optradio" id="optradio_1" runat="server" style="position: unset; margin-right: 5px;" value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206367") %></label>
            </div>
        </div>
        <div class="row send_type_0">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401049") %><span style="color: red;"></span></label>
            </div>
            <div class="col-xs-8">
                <asp:DropDownList ID="ddlSendType" ClientIDMode="Static" runat="server" CssClass="form-control">
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="0" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132658") %>" Value="2" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132663") %>" Value="1" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132660") %>" Value="3" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="row send_type_0">
            <!-- Nav tabs -->
            <div class="container hidden" style="width: 100%">
                <div id="lvSet"></div>
                <div id="divmain"></div>
                <div id="myTabContent" class="tab-content">
                </div>
            </div>
        </div>

        <div class="row hidden send_type_1">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %><span style="color: red;"></span></label>
            </div>
            <div class="col-xs-4">
                <select name="selecttype" class="form-control">
                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></option>
                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %></option>
                </select>
            </div>
            <div class="col-xs-4">
            </div>
        </div>
        <div class="row hidden send_type_1 user_type_1">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> :<span style="color: red;"></span></label>
            </div>
            <div class="col-xs-8">
                <select name="selectlevel" class="form-control dropdown">
                </select>
            </div>
        </div>
        <div class="row hidden send_type_1 user_type_1">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> :<span style="color: red;"></span></label>
            </div>
            <div class="col-xs-8">
                <select name="selectsublevel" class="form-control">
                </select>
            </div>
        </div>
        <div class="row hidden send_type_1">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %> <span style="color: red;"></span></label>
            </div>
            <div class="col-xs-6">
                <input type="text" name="sname" class="form-control" />
                <input type="hidden" name="iduser" class="form-control" />
            </div>
            <div class="col-xs-2">
                <input type="button" id="btnaddlistuser" class="btn btn-success" style="width: 100px;" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>" />
            </div>
        </div>
        <div class="row hidden send_type_1">
            <div class="col-xs-12">
                <table id="tablelistuser" class="table table-striped col-lg-12 ">
                    <thead class="table-tab">
                        <tr>
                            <td style="width: 60%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></td>
                            <td style="width: 20%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></td>
                            <td style="width: 20%; display: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %></td>
                        </tr>
                    </thead>
                    <tbody style="height: 400px; overflow-y: scroll;">
                    </tbody>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202042") %></label>
            </div>
            <div class="col-xs-6">
                <asp:FileUpload ID="fudFile" runat="server" AllowMultiple="true" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %> <span style="color: red;">*</span></label>
            </div>
            <div class="col-xs-6">
                <asp:TextBox ID="txtDesp" runat="server" MaxLength="4800" TextMode="MultiLine" CssClass="form-control" Width="480px" Height="350px" Style="resize: none !important;" />
                <span style="color: red;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132464") %></span>
            </div>
        </div>
        <asp:HiddenField ID="txtUserType1" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="txtUserType0" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="txtLv" runat="server" ClientIDMode="Static" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row mini--space__top">
                    <div class="col-xs-12 center">
                        <asp:Button ID="btnSave" runat="server" OnClientClick="return validate();" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" ValidationGroup="add"
                            class="btn btn-success global-btn" OnClick="btnSave_Click" />
                        <%--<input type="button" id="btnSaveData" class="btn btn-success global-btn" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>" />--%>
                        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" class="btn btn-danger global-btn" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
