<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master"
    CodeBehind="Useradd.aspx.cs" Inherits="FingerprintPayment.Useradd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <%--<script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        div .TProduct:hover {
            color: blue;
        }

        .width-20 {
            width: 20px;
        }
    </style>
    <script language="javascript" type="text/javascript">
<!--
    $(document).unload(function () {
        $("#ctl00_MainContent_rowTel").removeClass("hide");
    })

    $(document).ready(function () {
        $("#ctl00_MainContent_rowTel").removeClass("hide");
        $('.datepicker').datepicker({});
        $("#stdCheck").addClass("width-20 form-control");
        $("select[id*=ddlTypeProduct]").change(function () {
            var _type = $("select[id*=ddlTypeProduct]").val();
            $('div[id*=Type_]').hide();
            $("div[id*=Type_" + _type + "]").show();
        });
        $("#stdCheck").click(function () {
            var $this = $(this);
            if ($this.is(':checked')) {
                $("#ctl00_MainContent_rowTel").removeClass("hide");
            }
            else {
                $("#ctl00_MainContent_rowTel").addClass("hide");
            }
        });
        $("select[id*=ctl00_MainContent_ddlcType]").change(function () {
            if ($(this).val() == "0") {
                $("#ctl00_MainContent_rowSMS").removeClass("hide");
                $("#ctl00_MainContent_rowSalary").addClass("hide");
                $("#ctl00_MainContent_rowLV").addClass("show");
            } else {
                $("#ctl00_MainContent_rowSMS").addClass("hide");
                $("#ctl00_MainContent_rowSalary").removeClass("hide");
                $("#ctl00_MainContent_rowLV").addClass("hide");
            }
        });
        $("div[id*=TProduct_]").click(function (e, s) {
            var _text = $("input[id*=txtval]").val();
            var _html = $("div[id*=tdBlackList]").html();
            if (_text.indexOf(this.id + ",") == -1 || _text == "") {
                _html += "<span id=" + this.id + " >" + $(this).html() + "</span>&nbsp;<img class='img-delete-icon' src='images/action_delete.png' style='vertical-align: middle;cursor:pointer;' name='" + this.id + "' onclick='DelSelectProduct(" + '"' + this.id + '"' + ")' /><br>";
                _text += this.id + ",";
                $("input[id*=txtval]").val(_text);
                $("div[id*=tdBlackList]").html(_html);
            }
        });
    });
    // -->
    function DelSelectProduct(ID) {
        var _text = $("input[id*=txtval]").val();
        var _html = "";

        _text = _text.replace(ID + ",", "");
        $("input[id*=txtval]").val(_text);
        $("div[id*=tdBlackList]").html(_html);
        var _str = $("input[id*=txtval]").val();
        jQuery.each(_str.split(','), function (index, item) {
            if (item != "") {
                var _id = item;
                var _val = $("div[id*=" + _id + "]").html();
                _html += "<span id=" + _id + ">" + _val + "&nbsp;<img class='img-delete-icon' src='images/action_delete.png' style='vertical-align: middle;cursor:pointer;' name='" + _id + "' onclick='DelSelectProduct(" + '"' + _id + '"' + ")' /></span><br>";
                $("div[id*=tdBlackList]").html(_html);
            }
        });
    }
    var popUpObj;
    function showModalPopUp() {
        if (document.getElementById("aspnetForm").ctl00$MainContent$txtsName.value + document.getElementById("aspnetForm").ctl00$MainContent$txtsLastName.value == "") {
            j_alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121048") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121049") %>');
            return false;
        }
        else if (document.getElementById("aspnetForm").ctl00$MainContent$txtsIdentification.value == "") {
            j_alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121048") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121050") %>');
            return false;
        }
        else {
            popUpObj = window.showModalDialog('frmRegister.aspx?page=users&Firstname=' + document.getElementById("aspnetForm").ctl00$MainContent$txtsName.value
            + '&Lastname=' + document.getElementById("aspnetForm").ctl00$MainContent$txtsLastName.value
            + '&IDCARD=' + document.getElementById("aspnetForm").ctl00$MainContent$txtsIdentification.value, this, 'status:1; resizable:1; dialogWidth:420px; dialogHeight:520px; dialogTop=180px; dialogLeft:510px; scroll:no;status=no');
        } window.onunload = OnUnload;
    }
    function listsublevel() {
        var SubLVID = $('#ctl00_MainContent_ddlSubLV option:selected').val();
        var sSubLV = $('#ctl00_MainContent_ddlSubLV option:selected').text();
        $('input[id*=txtSubLV2ID]').val("");
        $("#ddlSubLV2 option").remove();
        $.ajax({
            url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
            success: function (msg) {
                $.each(msg, function (index) {
                    if (index == 0) {
                        $('input[id*=txtSubLV2ID]').val(msg[index].nTermSubLevel2);
                    }
                    $('select[id*=ddlSubLV2]')
                        .append($("<option></option>")
                        .attr("value", msg[index].nTermSubLevel2)
                        .text(sSubLV + " / " + msg[index].nTSubLevel2));
                });
            }
        });
    }

    $(document).ready(function () {
        listsublevel();
        $('#ddlSubLV2').change(function () {
            $('input[id*=txtSubLV2ID]').val($('#ddlSubLV2 option:selected').val());
        });
        $('#ctl00_MainContent_ddlSubLV').change(function () {
            listsublevel();
        });
        var availableValueplane = [];
        $.ajax({
            //type: "GET",
            url: "/App_Logic/dataGeneric.ashx?mode=getlistproduct&id=",
            ////cache: false,
            //contentType: 'application/json;',
            success: function (objjson) {
                $.each(objjson, function (index) {
                    var newObject = {
                        label: objjson[index].sProduct,
                        //show: objjson[index].sProduct + " " + objjson[index].sBarCode,
                        value: objjson[index].nProductID
                    };
                    availableValueplane[index] = newObject;
                });
            }
        });
        $("input[id*=txtSearch]").autocomplete({
            width: 300,
            max: 10,
            delay: 100,
            minLength: 1,
            autoFocus: true,
            cacheLength: 1,
            scroll: true,
            highlight: false,
            source: function (request, response) {
                var results = $.ui.autocomplete.filter(availableValueplane, request.term);
                $("div[id*=TProduct_]").hide();
                $.each(results, function (i) {
                    $("#TProduct_" + results[i].value).show();
                });
                response(results.slice(0, 10));
            },
            select: function (event, ui) {
                event.preventDefault();
                $("input[id*=txtSearc").val(ui.item.label);
                $("div[id*=TProduct_]").hide();
                $("#TProduct_" + ui.item.value).show();
            },
            focus: function (event, ui) {
                event.preventDefault();
            }
        });
    });
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="detail-card box-content useradd-container">
        <div class="row hidden" style="display: none;">
            <div class="col-lg-3 col-md-4 col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %></label>
            </div>
            <div class="col-lg-9 col-md-8 col-xs-8">
                <asp:Button ID="btnRegister1" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121051") %>" OnClientClick="showModalPopUp()"
                    class="btn btn-info" />
                <span id="div1" style="clear: both; width: 100px;"></span>&nbsp;
                <asp:TextBox ID="txtCheckFinger1" runat="server" Text="1" Style="display: none;" />
                <asp:TextBox ID="txtUserFinger1" runat="server" Style="display: none;" />
                <asp:RequiredFieldValidator ID="revtxtCheckFinger" runat="server" Display="None"
                    SetFocusOnError="true" ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %><br>"
                    ControlToValidate="txtCheckFinger1" ValidationGroup="add"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender runat="server" ID="vcetxtCheckFinger" TargetControlID="revtxtCheckFinger"
                    HighlightCssClass="validatorcallouthighlight" Width="200px" />
            </div>
            <asp:TextBox ID="txtUserFinger2" runat="server" Style="display: none;" />
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-4 col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
            </div>
            <div class="col-lg-4 col-md-5 col-xs-5">
                <asp:TextBox ID="txtsName" runat="server" CssClass='form-control' />
                <asp:RequiredFieldValidator ID="reqtxtsName" runat="server" Display="None" SetFocusOnError="true"
                    ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %><br>" ControlToValidate="txtsName"
                    ValidationGroup="add"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender runat="server" ID="vcetxtsName" TargetControlID="reqtxtsName"
                    HighlightCssClass="validatorcallouthighlight" Width="200px" />
            </div>
            <div class="col-lg-5 col-md-3 col-xs-3">
                <asp:Button ID="btnImport" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121063") %>" class="btn btn-info hidden" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-4 col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
            </div>
            <div class="col-lg-4 col-md-5 col-xs-5">
                <asp:TextBox ID="txtsLastName" runat="server" CssClass='form-control' />
                <asp:RequiredFieldValidator ID="reqtxtsLastName" runat="server" Display="None" SetFocusOnError="true"
                    ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %><br>"
                    ControlToValidate="txtsLastName" ValidationGroup="add"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender runat="server" ID="vcetxtsLastName" TargetControlID="reqtxtsLastName"
                    HighlightCssClass="validatorcallouthighlight" Width="200px" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-4 col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %></label>
            </div>
            <div class="col-lg-4 col-md-5 col-xs-5">
                <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>&nbsp;</asp:ListItem>
                    <asp:ListItem Value="1">&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-4 col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104052") %></label>
            </div>
            <div class="col-lg-4 col-md-5 col-xs-5">
                <asp:TextBox ID="txtsIdentification" runat="server" MaxLength="13" CssClass='form-control' />
                <asp:RequiredFieldValidator ID="reqtxtsIdentification" runat="server" Display="None"
                    SetFocusOnError="true" ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104052") %><br>"
                    ControlToValidate="txtsIdentification" ValidationGroup="add"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender runat="server" ID="vcetxtsIdentification" TargetControlID="reqtxtsIdentification"
                    HighlightCssClass="validatorcallouthighlight" Width="200px" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-4 col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105034") %></label>
            </div>
            <div class="col-lg-4 col-md-5 col-xs-5">
                <div class="input-group date-container">
                    <asp:TextBox ID="txtdBirth" runat="server" class="form-control"
                        aria-describedby="basic-addon2" />
                    <div class="input-group-addon">
                        <i class="glyphicon glyphicon-calendar"></i>
                    </div>
                </div>
            <%--    <asp:RequiredFieldValidator ID="reqtxtdBirth" runat="server" Display="None" SetFocusOnError="true"
                    ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105034") %><br>"
                    ControlToValidate="txtdBirth" ValidationGroup="add"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender runat="server" ID="vcetxtdBirth" TargetControlID="reqtxtdBirth"
                    HighlightCssClass="validatorcallouthighlight" Width="200px" />
                <asp:Label Style="cursor: hand;" ID="lbldstart" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121052") %>" runat="server"
                    Visible="False"><img src="images/icbcalenp.gif" /></asp:Label>
                <cc1:MaskedEditExtender ID="makdend" runat="server" TargetControlID="txtdBirth" OnInvalidCssClass=""
                    OnFocusCssClass="" MessageValidatorTip="true" MaskType="Date" Mask="99/99/9999"
                    ErrorTooltipEnabled="True" ClearMaskOnLostFocus="true" AcceptAMPM="false">
                </cc1:MaskedEditExtender>--%>
            </div>
            <div class="col-lg-5 col-md-3 col-xs-3">
                <span style="color: Red">dd/mm/yyyy</span>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-4 col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121068") %></label>
            </div>
            <div class="col-lg-4 col-md-5 col-xs-5">
                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass='form-control'></asp:TextBox>
            </div>
        </div>
        <div class="row" id="rowLV" runat="server">
            <div class="col-lg-3 col-md-4 col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
            </div>
            <div class="col-lg-9 col-md-8 col-xs-8">
                <asp:DropDownList ID="ddlSubLV" runat="server" class="input--short" />
            </div>
        </div>
        <div class="row" id="Div2" runat="server">
            <div class="col-lg-3 col-md-4 col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
            </div>
            <div class="col-lg-4 col-md-5 col-xs-5">
                <asp:TextBox ID="txtSubLV2ID" runat="server" Style="display: none;" />
                <select id="ddlSubLV2" class="input--short">
                </select>
            </div>
        </div>
        <div class="row hidden" id="rowSMS" runat="server">
        </div>
        <div class="row hidden">
            <div class="col-lg-3 col-md-4 col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131176") %></label>
            </div>
            <div class="col-lg-9 col-md-8 col-xs-8">
                <asp:CheckBox ID="stdCheck" ClientIDMode="Static" runat="server" Checked="true" />
            </div>
        </div>
        <div class="row" id="rowTel" runat="server">
            <div class="col-lg-3 col-md-4 col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131165") %></label>
            </div>
            <div class="col-lg-4 col-md-5 col-xs-5">
                <asp:TextBox ID="stdTel" ClientIDMode="Static" runat="server" CssClass='form-control' />
            </div>

        </div>
        <div class="row" id="rowSalary" runat="server">
            <div class="col-lg-3 col-md-4 col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105050") %></label>
            </div>
            <div class="col-lg-4 col-md-5 col-xs-5">
                <asp:TextBox ID="salary" ClientIDMode="Static" CssClass="form-control" runat="server" />
            </div>

        </div>
        <div class="row">
            <div class="col-lg-3 col-md-4 col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131166") %></label>
            </div>
            <div class="col-lg-4 col-md-5 col-xs-5">
                <asp:TextBox ID="txtnMax" runat="server" CssClass='form-control'></asp:TextBox>
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131167") %>
            </div>
        </div>
    </div>
    <div class="detail-card box-content mini--space__top useradd-container">
        <div class="row">
            <div class="col-xs-12">
                <label class="label-header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %> BlackList</label>
            </div>
        </div>
        <div class="row row-search-container">
            <div class="col-xs-3">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603091") %></label>
            </div>
            <div class="col-xs-3">
                <asp:DropDownList ID="ddlTypeProduct" runat="server" class="form-control" />
            </div>
            <div class="col-xs-2">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131169") %></label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtval" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="txtSearch" runat="server" CssClass='form-control'></asp:TextBox></span>
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-6 list">
                <div class="row list--header">
                    <div class="col-xs-12">
                        <label class="label-header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %></label>
                    </div>
                </div>
                <div class="row list-content">
                    <div class="col-xs-12">
                        <asp:Literal ID="ltrListProduct" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="col-xs-6 list">
                <div class="row list--header">
                    <div class="col-xs-12">
                        <label class="label-header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %> BlackList</label>
                    </div>
                </div>
                <div class="row list-content">
                    <div class="col-xs-12">
                        <div id="tdBlackList">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row mini--space__top center">
            <asp:Button ID="btnSave" class="btn btn-success global-btn"
                runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" ValidationGroup="add" />
            &nbsp;<asp:Button ID="btnCancel" class="btn btn-danger global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
        </div>
    </div>
</asp:Content>
