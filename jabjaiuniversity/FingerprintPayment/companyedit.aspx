<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="companyedit.aspx.cs" Inherits="FingerprintPayment.companyedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            $("#ctl00_MainContent_fulLogo").change(function () {
                displayPreview(this);
            });
        });
        var _URL = window.URL || window.webkitURL;
        var _src = "";
        function displayPreview(files) {
            var file = files.files[0]
            var img = new Image();
            var sizeKB = file.size / 1024;
            var chk = true;
            img.onload = function () {
                $('#preview').append(img);
                if (img.width > 187 || img.height > 58) {
                    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131058") %>");
                    clearInputFile(files)
                    $('img[id*=imgLogo]').attr('src', '');
                    return false;
                }
                else {
                    readURL(files);
                    _src = $("#ctl00_MainContent_fulLogo").val();
                    return true;
                }
            }
            img.src = _URL.createObjectURL(file);
        }
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    if (input.files[0].size > (1024 * 200)) {
                        alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131062") %> ");
                        clearInputFile(input);
                        return false;
                    } else {
                        $('img[id*=imgLogo]').attr('src', e.target.result);
                    }
                }
            }
            reader.readAsDataURL(input.files[0]);
        }
        function clearInputFile(f) {
            if (f.value) {
                try {
                    f.value = _src; //for IE11, latest Chrome/Firefox/Opera...
                } catch (err) {
                }
                if (f.value) { //for IE5 ~ IE10
                    var form = document.createElement('form'), ref = f.nextSibling;
                    form.appendChild(f);
                    form.reset();
                    ref.parentNode.insertBefore(f, ref);
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="detail-card box-content companyedit-container">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-md-12 center">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131059") %></label>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-2 col-md-1 col-sm-1">
            </div>
            <div class="col-lg-2 col-md-3 col-sm-3">
                <label>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131060") %></label>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6">
                <asp:TextBox ID="txtsCompany" CssClass="form-control" Width="300px" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-2 col-md-1 col-sm-1"></div>
            <div class="col-lg-2 col-md-3 col-sm-3">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121068") %></label>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6">
                <asp:TextBox ID="txtsAddress" CssClass="form-control" Width="300px" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-2 col-md-1 col-sm-1"></div>
            <div class="col-lg-2 col-md-3 col-sm-3">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02940") %></label>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6">
                <asp:TextBox ID="txtsTel" CssClass="form-control" Width="300px" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-2 col-md-1 col-sm-1"></div>
            <div class="col-lg-2 col-md-3 col-sm-3">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131063") %></label>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6">
                <img class="hidden" id="preview" />
                <asp:Image ID="imgLogo" runat="server" />
                <asp:FileUpload ID="fulLogo" runat="server" accept="image/*" />
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131061") %>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 center">
                <asp:Button ID="btnSave" CssClass="btn-success btn global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                <asp:Button ID="btnCancel" CssClass="btn-danger btn global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
