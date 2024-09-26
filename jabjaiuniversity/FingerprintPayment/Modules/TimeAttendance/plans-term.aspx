<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="plans-term.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.plans_term" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function showrow(mode, index) {
            if (mode == "edit") {

            }
            else if (mode == "listterm") {

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager runat="server" EnablePageMethods="true" />
    <script src="../../app/Control/filterControl.js" type="text/javascript"></script>
    <script src="../../app/plans/PlansJS.js" type="text/javascript"></script>
     <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
      <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
     <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>
    <div class="report-card box-content">
        <div id="filter">
        </div>
        <div class="row">
            <div class="col-lg-1 col-md-1 col-sm-1"></div>
            <div class="col-lg-10 col-md-10 col-sm-10">
                <input type="button" id="btnSearch" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" class="btn btn-success col-xs-12" />
            </div>
            <div class="col-lg-1 col-md-1 col-sm-1"></div>
        </div>
        <div class="row--space">
            <div class="col-xs-12">
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12" id="listroom">
            </div>
        </div>
    </div>
    <script id="template" type="x-tmpl-mustache">
        <div class="row table table-striped plans-room-table">
            <div class="col-lg-1 col-md-1 col-sm-1"></div>
            <div class="col-lg-4 col-md-4 col-sm-4" style="background:#286090; color: white;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></div>
            <div class="col-lg-7 col-md-7 col-sm-7" style="background:#286090; color: white;">&nbsp;</div>
        </div>
        {{#listroom}}
        <div class="row">
            <div class="col-lg-1 col-md-1 col-sm-1"></div>
            <div class="col-lg-4 col-md-4 col-sm-4">{{lvname}}</div>
            <div class="col-lg-7 col-md-7 col-sm-7 center">
            <a href="#" onclick="CheckPlanCourseExist(this, {{lv2id}},'{{termid}}','plans-schedule.aspx?id={{lv2id}}&idterm={{termid}}');return false;" target="_blank" class="btn btn-primary btn-link btn-plans-room btnpermission"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202003") %></a>&nbsp;
            <a href="plans-scheduledetail.aspx?id={{lv2id}}&idterm={{termid}}" target="_blank" class="btn btn-info btn-link btn-plans-room"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202005") %></a>
            </div>
        </div>
        {{/listroom}}
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
