<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="subleveledit.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.subleveledit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function j_infosell(msg) {
            showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg, function () {
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="detail-card box-content">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132669") %> <span style="color: red;">*</span></label>
            </div>
            <div class="col-xs-8">
                <asp:Literal ID="ltrSLevel" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803010") %> <span style="color: red;">*</span></label>
            </div>
            <div class="col-xs-8">
                <asp:DropDownList ID="ddlnTimeType" runat="server">
                </asp:DropDownList>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row mini--space__top">
                    <div class="col-xs-12 center subleveledit-btn-footer">
                        <asp:Button ID="btnSave" Width="100" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" ValidationGroup="add"
                            OnClick="btnSave_Click" class="btn btn-primary" />
                        &nbsp;<asp:Button ID="btnCancel" Width="100" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" class="btn btn-default" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
