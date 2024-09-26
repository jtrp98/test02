<%@ Page Title="" Language="C#" MasterPageFile="~/mppopup.Master" AutoEventWireup="true" CodeBehind="macadd.aspx.cs" Inherits="FingerprintPayment.macadd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function getAverment() {
            $.ajax({
                type: "POST",
                url: "/api/Addmobile",
                cache: false,
                dataType: "html",
                success: function (response) {

                }
            });
        }
        $(document).ready(function () {
            setInterval(function () { getAverment(); }, 1000);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card  box-content col3 macadd-container">
        <div class="row">
            <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release"></asp:ScriptManager>
            <div class="col-lg-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <span class="detail-box"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131088") %>  
                        <asp:Label ID="lbltop" runat="server" />
                            <br>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131089") %> 
                     <asp:Label ID="lblTime" runat="server" />
                        </span>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="timecode" EventName="Tick" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:Timer ID="timecode" runat="server" Interval="100"></asp:Timer>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
