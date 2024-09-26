<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" enableEventValidation="false"
    CodeBehind="homeworkadd.aspx.cs" Inherits="FingerprintPayment.homeworkadd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="app/Homework/ControllUser.js" type="text/javascript"></script>

    <script src="app/Homework/ListUser.js" type="text/javascript"></script>
    <script src="app/Homework/TableLevel.js" type="text/javascript"></script>
    <script src="app/Homework/HomeworkJS.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="detail-card box-content smsadd-container periodadd-container">
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %></label>
            </div>
            <div class="col-xs-8">
                <asp:DropDownList ID="ddlPlane" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Loading . . ."> </asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131074") %></label>
            </div>
            <div class="col-xs-3">
                <div class="input-group" style="float: left;">
                    <input class="form-control datepicker" data-date-format="dd/mm/yyyy" aria-describedby="basic-addon2" id="dayStart" name="dayStart" style="width: 120px; height: 35px; float: left; font-size: 24px;" />
                    <span class="input-group-addon" id="basic-addon1" style="float: left; min-height: 36px; min-width: 36px;"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                </div>
            </div>
            <div class="col-xs-1">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
            </div>
            <div class="col-xs-3">
                <div class="input-group" style="float: left;">
                    <input class="form-control datepicker col-lg-12" data-date-format="dd/mm/yyyy" aria-describedby="basic-addon2" id="dayEnd" name="dayEnd" style="width: 120px; height: 35px; float: left; font-size: 24px;" />
                    <span class="input-group-addon" id="basic-addon2" style="float: left; min-height: 36px; min-width: 36px;"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                </div>
            </div>
            <div class="col-xs-1">
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131075") %></label>
            </div>
            <div class="col-xs-8">
                <div class="input-group" style="float: left;">
                    <input class="form-control datepicker" data-date-format="dd/mm/yyyy" aria-describedby="basic-addon2" id="dayNotification" name="dayNotification" style="width: 120px; height: 35px; float: left; font-size: 24px;" />
                    <span class="input-group-addon" id="basic-addon3" style="float: left; min-height: 36px; min-width: 36px;"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202042") %></label>
            </div>
            <div class="col-xs-8">
                <input id="my-file-selector" type="file"
                    <%--onchange="$('#upload-file-info').html(this.files[0].name)"--%> />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131076") %></label>
            </div>
            <div class="col-xs-8">
                <textarea rows="3" cols="2" id="homeworkdetail" name="homeworkdetail" class="form-control text-info"></textarea>
            </div>
        </div>
        <div class="listuser type1"></div>
        <div class="row center">
            <input type="submit" id="btnSave" style="width: 100px;" class="btn btn-success custom-button" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" />
            &nbsp;<a href="/homeworklist.aspx" id="btnCancel" style="width: 100px;" class="btn btn-danger custom-button"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
