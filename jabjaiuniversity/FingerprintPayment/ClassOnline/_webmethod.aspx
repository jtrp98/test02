<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="_webmethod.aspx.cs"
    Inherits="FingerprintPayment.ClassOnline._webmethod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

  


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content smssetting-container">
     
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
 <%--   <script>
        $.ajax({
            type: "GET",
            url: '<%= ResolveUrl("~/ClassOnline/_webmethod.aspx/GetNotification") %>',
            //data: "{ id : 'teste' }",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                console.log(data);
            }
        });
    </script>--%>
</asp:Content>
