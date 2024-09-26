<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="leveladd.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.leveladd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script>
    function j_infosell(msg) {

        showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg, function () {
        });
    }

  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <div class="detail-card box-content leveladd-container">
     
 <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>


        <div class="row">
            <div class="col-lg-offset-1 col-lg-3 col-md-4 col-xs-4" style="text-align: center;">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132608") %> <span style="color:red;">*</span></label>
            </div>
            <div class="col-lg-6 col-md-8 col-xs-8">
                    <asp:TextBox ID="txtLevel" runat="server" MaxLength="512" class="form-control" />
            </div>
        </div>


         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

        <div class="row mini--space__top">
            <div class="col-xs-12 center">
                <asp:Button ID="btnSave" Width="100" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" ValidationGroup="add" OnClick="btnSave_Click" class="btn btn-success global-btn"/>
                &nbsp;<asp:Button ID="btnCancel" Width="100" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" class="btn btn-danger global-btn"/>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">

</asp:Content>
