<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="FingerprintPayment.TuitionFee.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />


    <div class="full-card box-content employeeslist-container group-list">
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <div class="row">
                    <label class="col-md-4">preRegister ID</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="Button1" runat="server" Text="UPDATE" CssClass="btn btn-info" />
                    </div>
                </div>

                <div class="row--space"></div>
                <div class="row">
                    <label class="col-md-4"></label>
                    <div class="col-md-4">
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="Button2" runat="server" Text="Button" CssClass="btn btn-info" />
                    </div>
                </div>

                <div class="row--space"></div>
                <div class="row">
                    <label class="col-md-4">Invoice ID</label>
                    <div class="col-md-4">
                        <input type="text" class="form-control" id="txtInvoiceID" />
                    </div>
                    <div class="col-md-4">
                        <div class="btn btn-info" id="btnInvoiceUpdate">UPDATE</div>
                    </div>
                </div>
            </div>
            <div class="col-md-4"></div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#btnInvoiceUpdate").click(function () {
                let txtInvoiceID = $("#txtInvoiceID").val();
                if (isNaN(txtInvoiceID) == false) {
                    PageMethods.UpdateInvoice(txtInvoiceID, function (response) {
                        response
                    });
                }
            });
        })
    </script>
</asp:Content>
