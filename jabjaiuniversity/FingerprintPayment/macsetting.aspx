<%@ Page Title="" Language="C#" MasterPageFile="~/mppopup.Master" AutoEventWireup="true" CodeBehind="macsetting.aspx.cs" Inherits="FingerprintPayment.macsetting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $('input[id*=btnDel]').click(function () {
                showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('name')); return false;
            });
            $('input[id*=type]').click(function () {
                if ($('input[id*=type]:checked').val() == "0") {
                    $(".classname").addClass("hidden");
                    $(".comname").removeClass("hidden");
                } else {
                    $(".comname").addClass("hidden");
                    $(".classname").removeClass("hidden");
                    $('input[id*=txtsClass]').val($('select[id*=ddlClass] option:selected').html());
                }
            });
            if ($('input[id*=type]:checked').val() == "0") {
                $(".classname").addClass("hidden");
                $(".comname").removeClass("hidden");
            } else {
                $(".comname").addClass("hidden");
                $(".classname").removeClass("hidden");
            }
            $('select[id*=ddlClass]').change(function () {
                $('input[id*=txtsClass]').val($('select[id*=ddlClass] option:selected').html());
            })
            $("input[id*=txtsMac1]").keyup(function (e) {
                if (e.keyCode == 8) return;
                if ($("input[id*=txtsMac1]").val().length == 2) {
                    $("input[id*=txtsMac2]").focus();
                    $("input[id*=txtsMac2]").focus();
                }
            });
            $("input[id*=txtsMac2]").keyup(function (e) {
                if (e.keyCode == 8) return;
                if ($("input[id*=txtsMac2]").val().length == 2) {
                    $("input[id*=txtsMac3]").focus();
                    $("input[id*=txtsMac3]").focus();
                }
            });
            $("input[id*=txtsMac3]").keyup(function (e) {
                if (e.keyCode == 8) return;
                if ($("input[id*=txtsMac3]").val().length == 2) {
                    $("input[id*=txtsMac4]").focus();
                    $("input[id*=txtsMac4]").focus();
                }
            });
            $("input[id*=txtsMac4]").keyup(function (e) {
                if (e.keyCode == 8) return;
                if ($("input[id*=txtsMac4]").val().length == 2) {
                    $("input[id*=txtsMac5]").focus();
                    $("input[id*=txtsMac5]").focus();
                }
            });
            $("input[id*=txtsMac5]").keyup(function (e) {
                if (e.keyCode == 8) return;
                if ($("input[id*=txtsMac5]").val().length == 2) {
                    $("input[id*=txtsMac6]").focus();
                    $("input[id*=txtsMac6]").focus();
                }
            });
        })
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
    <div class="full-card  box-content col3 macsetting-container">
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-4">
                <label>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131095") %></label>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8">
                <input id="type1" type="radio" runat="server" name="type" checked="true" value="0" />
                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131096") %></span>
                <input id="type2" type="radio" runat="server" name="type" value="1" />
                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131097") %></span>
            </div>
        </div>
        <div class="row comname">
            <div class="col-lg-4 col-md-4 col-sm-4">
                <label>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131093") %></label>
            </div>
            <div class="col-lg-7 col-md-7 col-sm-7">
                <asp:TextBox ID="txtsClass" CssClass="form-control" runat="server" />
                <asp:HiddenField ID="hfdsClassID" runat="server" />
                <asp:RequiredFieldValidator ID="revtxtsClass" runat="server" Display="None" SetFocusOnError="true"
                    ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801045") %><br>"
                    ControlToValidate="txtsClass" ValidationGroup="add"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender runat="server" ID="vcerevtxtsClass" TargetControlID="revtxtsClass"
                    HighlightCssClass="validatorcallouthighlight" Width="200px" />
            </div>
        </div>
        <div class="row classname">
            <div class="col-lg-4 col-md-4 col-sm-4">
                <label>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801045") %></label>
            </div>
            <div class="col-lg-7 col-md-7 col-sm-7">
                <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="row--space">
        </div>
        <br />
        <div class="row">
            <div class="col-md-12 col-sm-12 text-center vertical-mid">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" ValidationGroup="add"
                    Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
