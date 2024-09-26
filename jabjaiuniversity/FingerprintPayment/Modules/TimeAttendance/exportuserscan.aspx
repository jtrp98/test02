<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master"   AutoEventWireup="true" CodeBehind="exportuserscan.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.exportuserscan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .font-20px
    {
        font-size:20px;
        }
</style>
<div class="col-md-12">
    <div class="col-md-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></div>
    <div class="col-md-2">
     <asp:DropDownList ID="ddlcType" runat="server" class="form-control font-20px">
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %>" Value="0" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102166") %>" Value="1" />
                </asp:DropDownList>
    </div>

    <div class="col-md-9">&nbsp;</div>
</div>


<div class="col-md-12">
    <ul class="nav nav-tabs">
     <li class="active"><a data-toggle="tab" href="#tab1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132571") %></a></li>
     <li><a data-toggle="tab" href="#tab2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132572") %></a></li>
    </ul>
    <br/>
    <div class="tab-content">
     <div id="tab1" class="tab-pane fade in active">
    <br/>

     <div class="col-md-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206248") %></div>
     <div class="col-md-2">        
             <input class="form-control datepicker font-20px" data-date-format="dd/mm/yyyy" aria-describedby="basic-addon2" id="txtHolidayStart" style="width: 100px;height: 35px;float:left"  runat="server" />
             <span class="input-group-addon" id="basic-addon2"  style="float:left;min-height:36px;min-width:36px;" ><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>
     </div>
     <div class="col-md-1">
            <a  id="btnExportDay" href="/App_Logic/dataJSON.aspx?mode=exportuserbyday" class="btn btn-primary font-20px" target="_blank"  >Export to excel</a>
     </div>        
     </div>
     <div id="tab2" class="tab-pane fade">
    <br/>

     <div class="col-md-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></div>
             <div class="col-md-2" style="font-size:26px !important;">
               <asp:DropDownList ID="ddlMonth" runat="server" class="form-control font-20px">
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>" Value="1" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>" Value="2" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>" Value="3" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>" Value="4" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>" Value="5" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>" Value="6" />
                    <asp:ListItem Text="ก<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>กฏาคม" Value="7" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>" Value="8" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>" Value="9" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>" Value="10" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>" Value="11" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>" Value="12" />
                </asp:DropDownList>
             </div>

     <div class="col-md-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></div>
             <div class="col-md-2" style="font-size:26px !important;">
                 <asp:DropDownList ID="ddlYear" runat="server" ClientIDMode="Static" CssClass="form-control font-20px" />
             </div>

    <div class="col-md-1">
            <asp:Button ID="btnExpotyMonth"  CssClass="btn btn-primary font-20px" Text="Export to excel" runat="server" />
     </div>  
     </div>
     </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">

<script>
    $(function () {
        $('.datepicker').datepicker({});
        $.ajax({
            url: "/App_Logic/modalJSON.aspx?mode=allyear",
            success: function (resp) {
                var obj = $.parseJSON(resp);


                if (obj != []) {
                    $.each(obj, function (index) {
                        $("#ddlYear").append("<option value='" + obj[index].nYear + "'>" + obj[index].numberYear + "</option>");
                    });
                } else {
                    nullYear = 0;
                }
            }
        });
    });
</script>
</asp:Content>
