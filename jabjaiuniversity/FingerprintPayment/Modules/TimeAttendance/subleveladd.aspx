<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="subleveladd.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.subleveladd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function j_infosell(msg) {

            showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg, function () {
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="detail-card box-content subleveladd-container">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132665") %> <span style="color: red;">*</span></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtSLevel" runat="server" MaxLength="512" class="form-control" Width="300px" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104058") %> <span style="color: red;">*</span></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtnRooms" runat="server" MaxLength="512" class="form-control" Width="300px" />
            </div>
        </div>
        <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <div class="row mini--space__top">
            <div class="col-xs-12 center">
                <asp:Button ID="btnSave" Width="100" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" ValidationGroup="add"
                    OnClick="btnSave_Click" class="btn btn-success custom-button" />
                &nbsp;<asp:Button ID="btnCancel" Width="100" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" class="btn btn-danger custom-button" />
            </div>
        </div>
        <%--     </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
