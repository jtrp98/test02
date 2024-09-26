<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="employeesedit.aspx.cs" Inherits="FingerprintPayment.employessedit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <object id="objSecuBSP" style="left: 0px; top: 0px" height="0" width="0" classid="CLSID:6283f7ea-608c-11dc-8314-0800200c9a66"
        name="objSecuBSP" viewastext>
    </object>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('tr[id*="trReport0"]').hide();
            $('tr[id*="trSubReport"]').hide();
            $('tr[id="tab03_trReport"]').hide();
            $(".datepicker").datepicker({ format: "dd/mm/yyyy" });
            Tab01();
            Tab02();
            Tab03();
        });
        function Tab01() {
            if ($('input[id*=ctl08_rebcStatus02]').is(':checked')) {
                $('tr[id*="trReport0"]').show();
            }
            if ($('input[id*=ctl09_rebsubcStatus02]').is(':checked')) {
                $('tr[id*="trSubReport"]').show();
            }
            if ($('input[id*=ctl09_rebsubcStatus01]').is(':checked')) {
                $('tr[id*="trSubReport"]').hide();
            }

            $('input[id*=ctl08_rebcStatus02]').click(function () {
                $('tr[id*="trReport0"]').show();
                $('input[id*=rebsubcStatus01]').attr("checked", "checked");
            });
            $('input[id*=ctl08_rebcStatus01]').click(function () {
                $('tr[id*="trReport0"]').hide();
                $('tr[id*="trSubReport"]').hide();
            });
            $('input[id*=ctl09_rebsubcStatus02]').click(function () {
                $('tr[id*="trSubReport"]').show();
            });
            $('input[id*=ctl09_rebsubcStatus01]').click(function () {
                $('tr[id*="trSubReport"]').hide();
            });

            $('input[id*=ctl03_rebcMember02]').click(function () {
                $('tr[id*="tab03_trReport"]').show();
            });

            $('input[id*=ctl03_rebcMember01]').click(function () {
                $('tr[id*="tab03_trReport"]').hide();
            });

            $('input[id=ctl11_rebcStatus03]').click(function () {
                $('input[id*=rebcStatus03]').attr("checked", "checked");
                $('input[id*=rebsubcStatus02]').attr("checked", "checked");
                $('input[id*=ctl08_rebcStatus02]').attr("checked", "checked");
                $('input[id*=ctl09_rebsubcStatus021]').attr("checked", "checked");
                $('tr[id*="trReport0"]').show();
                $('tr[id*="trSubReport"]').show();
            });
            $('input[id=ctl11_rebcStatus02]').click(function () {
                $('input[id*=rebcStatus02]').attr("checked", "checked");
                $('input[id*=rebsubcStatus02]').attr("checked", "checked");
                $('input[id*=ctl09_rebsubcStatus021]').attr("checked", "checked");
                $('tr[id*="trReport0"]').show();
                $('tr[id*="trSubReport"]').show();

            });
            $('input[id=ctl11_rebcStatus01]').click(function () {
                $('input[id*=rebcStatus01]').attr("checked", "checked");
                $('input[id*=ctl09_rebsubcStatus022]').attr("checked", "checked");
                $('tr[id*="trReport0"]').hide();
                $('tr[id*="trSubReport"]').hide();
            });
        }
        function Tab02() {
            //Tab02
            $('input[id=ctl11_rebcTime03]').click(function () {
                $('input[id*=rebcTime03]').attr("checked", "checked");
            });
            $('input[id=ctl11_rebcTime02]').click(function () {
                $('input[id*=rebcTime02]').attr("checked", "checked");
            });
            $('input[id=ctl11_rebcTime01]').click(function () {
                $('input[id*=rebcTime01]').attr("checked", "checked");
            });
        }
        function Tab03() {
            //Tab03
            $('input[id=ctl11_rebcTime04]').click(function () {
                $('input[id*=rebcMember01]').attr("checked", "checked");
                $('tr[id="tab03_trReport"]').hide();
            });
            $('input[id=ctl11_rebcTime05]').click(function () {
                $('input[id*=rebcMember02]').attr("checked", "checked");
                $('tr[id="tab03_trReport"]').show();
            });
            $('input[id=ctl11_rebcTime06]').click(function () {
                $('input[id*=rebcMember03]').attr("checked", "checked");
                $('input[id*=ctl04_rebcMember02]').attr("checked", "checked");
                $('tr[id="tab03_trReport"]').show();
            });

            if ($('input[id*=ctl03_rebcMember01]').is(':checked')) {
                $('input[id*=ctl04_rebcMember01]').attr("checked", "checked");
                $('tr[id*="tab03_trReport"]').hide();
            }
            if ($('input[id*=ctl03_rebcMember02]').is(':checked')) {
                $('input[id*=ctl04_rebcMember02]').attr("checked", "checked");
                $('tr[id*="tab03_trReport"]').show();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="detail-card box-content employeesadd-container">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Label ID="lblUserID" runat="server" Style="display: none;" />
        <div class="row" style="display: none;">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %></label>
            </div>
            <div class="col-xs-8">
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
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105032") %></label>
            </div>
            <div class="col-xs-4">
                <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control">
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %>" Value="1" Selected="True" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102166") %>" Value="2" />
                </asp:DropDownList>
            </div>
            <div class="col-xs-4">
            </div>
        </div>
        <asp:TextBox ID="txtUserFinger2" runat="server" Style="display: none;" />
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtsName" runat="server" CssClass='form-control' />
            </div>
            <div class="col-xs-4">
                <asp:RequiredFieldValidator ID="reqtxtsName" runat="server" ControlToValidate="txtsName"
                    CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="กรุณากรอกชืื่อ"
                    ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>
                <asp:Button ID="btnImport" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121063") %>" class="btn btn-info hidden" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtLastName" runat="server" CssClass='form-control' />

            </div>
            <div class="col-xs-4">
                <asp:RequiredFieldValidator ID="reqtxtLastName" runat="server" ControlToValidate="txtsName"
                    CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121064") %>"
                    ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121065") %></label>
            </div>
            <div class="col-xs-8">
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %></label>
            </div>
            <div class="col-xs-8">
                <asp:RadioButton ID="rbnSex0" runat="server" class="space-radio" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>" Checked="True"
                    GroupName="sex" />
                <asp:RadioButton ID="rbnSex1" runat="server" class="space-radio" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %>" GroupName="sex" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105033") %></label>
            </div>
            <div class="col-xs-4 type-container">
                <asp:DropDownList ID="ddlcUserType" runat="server">
                </asp:DropDownList>
                <%--onKeyUp="IsNumeric_citizen(this.value,this)" --%>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtsIdentification" runat="server" MaxLength="13" CssClass='form-control' />
                <%--onKeyUp="IsNumeric_citizen(this.value,this)" --%>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
            </div>
            <div class="col-xs-4">
                <div class="input-group">
                    <asp:TextBox ID="txtdBirth" runat="server" class="form-control datepicker" />
                    <div class="input-group-addon">
                        <i class="glyphicon glyphicon-calendar"></i>
                    </div>
                </div>
                <asp:RequiredFieldValidator ID="reqtxtdBirth" runat="server" Display="None" SetFocusOnError="true"
                    ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105034") %><br>"
                    ControlToValidate="txtdBirth" ValidationGroup="add"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender runat="server" ID="vcetxtdBirth" TargetControlID="reqtxtdBirth"
                    HighlightCssClass="validatorcallouthighlight" Width="200px" />
                <asp:Label Style="cursor: hand;" ID="lbldstart" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121052") %>" runat="server"
                    Visible="False"><img src="images/icbcalenp.gif" /></asp:Label>
                <%--<cc1:ValidatorCalloutExtender ID="vcereExdend" runat="server" Width="180px" TargetControlID="reqtxtdBirth"
    HighlightCssClass="validatorCalloutHighlight">
</cc1:ValidatorCalloutExtender>
<cc1:CalendarExtender ID="caldend" runat="server" TargetControlID="txtdBirth" PopupButtonID="lbldstart"
    FirstDayOfWeek="Sunday" Format="dd/MM/yyyy">
</cc1:CalendarExtender>--%>
                <cc1:MaskedEditExtender ID="makdend" runat="server" TargetControlID="txtdBirth" OnInvalidCssClass=""
                    OnFocusCssClass="" MessageValidatorTip="true" MaskType="Date" Mask="99/99/9999"
                    ErrorTooltipEnabled="True" ClearMaskOnLostFocus="true" AcceptAMPM="false">
                </cc1:MaskedEditExtender>
            </div>
            <div class="col-xs-4">
                <span style="color: Red">dd/mm/yyyy</span>
            </div>
        </div>
        <div class="row hidden">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtsPhone" runat="server" MaxLength="10" CssClass='form-control' />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    E-Mail</label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtsEmail" runat="server" CssClass='form-control' />
            </div>
            <div class="col-xs-4">
                <asp:RequiredFieldValidator ID="reqtxtsEmail" runat="server" ControlToValidate="txtsName"
                    CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121067") %>"
                    ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row--top" style="margin-bottom: 140px; margin-top: 10px;">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121068") %></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtsAddress" runat="server" TextMode="MultiLine" CssClass='form-control'
                    Style="width: 100%; height: 100px; margin-left: -4px;" />
            </div>
        </div>
        <ul class="nav nav-tabs hidden">
            <li class="active"><a data-toggle="tab" href="#sellingsys"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121069") %></a></li>
            <li><a data-toggle="tab" href="#timeattendance"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121070") %></a></li>
            <li><a data-toggle="tab" href="#member"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121071") %></a></li>
        </ul>
        <div class="tab-content hidden">
            <div id="sellingsys" class="tab-pane fade in active">
                <div class="row mini--space__top">
                    <div class="col-xs-3">
                        <label style="padding-left: 20px;">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01897") %></label>
                    </div>
                    <div class="col-xs-9">
                        <input type="button" class="btn btn-info btn-permission"
                            id="ctl11_rebcStatus01" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121072") %>" />
                        <input type="button" class="btn btn-info btn-permission"
                            id="ctl11_rebcStatus02" value="มีสิทธ์การเข้าถึง" />
                        <input type="button" class="btn btn-info btn-permission"
                            id="ctl11_rebcStatus03" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121053") %>" />
                    </div>
                </div>
                <div class="row  ">
                    <div class="col-xs-12">
                        <div class="wrapper-table">
                            <table class="cool-table cool-table-7" style="width: 100%; font-style: normal; font-weight: normal; text-decoration: none; font-size: 22px; border-collapse: collapse;"
                                border="0"
                                cellspacing="0" cellpadding="2">
                                <tbody>
                                    <tr class="headerCell" style="font-style: normal; font-weight: bold; text-decoration: none;">
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121054") %>
                                        </td>
                                        <td>
                                            <div class="center">
                                                ไม่มีสิทธ์
                                            </div>
                                        </td>
                                        <td>
                                            <div class="center">
                                                มีสิทธ์การเข้าถึง
                                            </div>
                                        </td>
                                        <td>
                                            <div class="center">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121053") %>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="itemCell" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="font-style: normal; font-weight: normal; text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603091") %>
                                        </td>
                                        <td align="center" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl01_rebcStatus01" runat="server" GroupName="ctl01" />
                                        </td>
                                        <td align="center" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl01_rebcStatus02" runat="server" GroupName="ctl01" />
                                        </td>
                                        <td align="center" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl01_rebcStatus03" runat="server" GroupName="ctl01" />
                                        </td>
                                    </tr>
                                    <tr class="alternateCell" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="font-style: normal; font-weight: normal; text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601039") %>
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl02_rebcStatus01" runat="server" GroupName="ctl02" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl02_rebcStatus02" runat="server" GroupName="ctl02" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl02_rebcStatus03" runat="server" GroupName="ctl02" />
                                        </td>
                                    </tr>
                                    <tr class="itemCell" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="font-style: normal; font-weight: normal; text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601068") %>
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl03_rebcStatus01" runat="server" GroupName="ctl03" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl03_rebcStatus02" runat="server" GroupName="ctl03" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl03_rebcStatus03" runat="server" GroupName="ctl03" />
                                        </td>
                                    </tr>
                                    <tr class="alternateCell" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="font-style: normal; font-weight: normal; text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121075") %>
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl04_rebcStatus01" runat="server" GroupName="ctl04" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl04_rebcStatus02" runat="server" GroupName="ctl04" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl04_rebcStatus03" runat="server" GroupName="ctl04" />
                                        </td>
                                    </tr>
                                    <tr class="itemCell" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="font-style: normal; font-weight: normal; text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132123") %>
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl05_rebcStatus01" runat="server" GroupName="ctl05" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl05_rebcStatus02" runat="server" GroupName="ctl05" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl05_rebcStatus03" runat="server" GroupName="ctl05" />
                                        </td>
                                    </tr>
                                    <tr class="alternateCell" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="font-style: normal; font-weight: normal; text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121056") %>
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl06_rebcStatus01" runat="server" GroupName="ctl06" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl06_rebcStatus02" runat="server" GroupName="ctl06" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl06_rebcStatus03" runat="server" GroupName="ctl06" />
                                        </td>
                                    </tr>
                                    <tr class="itemCell" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="font-style: normal; font-weight: normal; text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121076") %>
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl07_rebcStatus01" runat="server" GroupName="ctl07" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl07_rebcStatus02" runat="server" GroupName="ctl07" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl07_rebcStatus03" runat="server" GroupName="ctl07" />
                                        </td>
                                    </tr>
                                    <tr class="alternateCell" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="font-style: normal; font-weight: normal; text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603001") %>
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl08_rebcStatus01" runat="server" GroupName="ctl08" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl08_rebcStatus02" runat="server" GroupName="ctl08" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;"></td>
                                    </tr>
                                    <tr class="alternateCell" id="trReport01" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="font-style: normal; font-weight: normal; text-decoration: none;">&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121077") %>
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl09_rebsubcStatus01" runat="server" GroupName="ctl09" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl09_rebsubcStatus02" runat="server" GroupName="ctl09" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;"></td>
                                    </tr>
                                    <tr class="alternateCell" id="trSubReport" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="font-style: normal; font-weight: normal; text-decoration: none;"></td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none; text-align: left;">
                                            <asp:RadioButton ID="ctl09_rebsubcStatus021" runat="server" Text="&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121057") %>"
                                                GroupName="sub1" /><br />
                                            <asp:RadioButton ID="ctl09_rebsubcStatus022" runat="server" Text="&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121058") %>"
                                                GroupName="sub1" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;"></td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;"></td>
                                    </tr>
                                    <tr class="alternateCell" id="trReport02" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="font-style: normal; font-weight: normal; text-decoration: none;">&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121078") %>
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl10_rebsubcStatus01" runat="server" GroupName="ctl10" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl10_rebsubcStatus02" runat="server" GroupName="ctl10" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;"></td>
                                    </tr>
                                    <tr class="alternateCell" id="trReport03" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="font-style: normal; font-weight: normal; text-decoration: none;">&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121079") %>
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl11_rebsubcStatus01" runat="server" GroupName="ctl11" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl11_rebsubcStatus02" runat="server" GroupName="ctl11" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;"></td>
                                    </tr>
                                    <tr class="alternateCell" id="trReport04" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="font-style: normal; font-weight: normal; text-decoration: none;">&nbsp;&nbsp;รายงานการการเต็มเงิน
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl12_rebsubcStatus01" runat="server" GroupName="ctl12" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl12_rebsubcStatus02" runat="server" GroupName="ctl12" />
                                        </td>
                                        <td align="center" style="font-style: normal; font-weight: normal; text-decoration: none;"></td>
                                    </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div id="timeattendance" class="tab-pane fade">
                <div class="row mini--space__top">
                    <div class="col-xs-3">
                        <label style="padding-left: 20px;">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01897") %></label>
                    </div>
                    <div class="col-xs-9">
                        <input type="button" class="btn btn-info btn-fixed-width"
                            id="ctl11_rebcTime01" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121072") %>" />
                        <input type="button" class="btn btn-info btn-fixed-width"
                            id="ctl11_rebcTime02" value="มีสิทธ์การเข้าถึง" />
                        <input type="button" class="btn btn-info btn-fixed-width"
                            id="ctl11_rebcTime03" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121053") %>" />
                    </div>
                </div>
                <div class="row ">
                    <div class="col-xs-12">
                        <div class="wrapper-table">
                            <asp:DataGrid ID="dgdTime" runat="server" AutoGenerateColumns="False" Width="100%"
                                CellPadding="2" GridLines="None" Font-Size="22px" AllowPaging="True" Font-Bold="False"
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                PageSize="20" CssClass="cool-table cool-table-7" PagerStyle-Visible="false">
                                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                                <Columns>
                                    <asp:BoundColumn HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121054") %>" DataField="sMenu" ItemStyle-Width="25%" ItemStyle-CssClass="dgddt">
                                        <ItemStyle Width="25%" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="ไม่มีสิทธ์" ItemStyle-Width="20%">
                                        <HeaderTemplate>
                                            <div class="center">
                                                ไม่มีสิทธ์
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rebcTime01" Checked="true" runat="server" GroupName="Status" />
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="มีสิทธ์การเข้าถึง" ItemStyle-Width="25%">
                                        <HeaderTemplate>
                                            <div class="center">
                                                มีสิทธ์การเข้าถึง
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rebcTime02" runat="server" GroupName="Status" />
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121053") %>" ItemStyle-Width="25%">
                                        <HeaderTemplate>
                                            <div class="center">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121053") %>
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rebcTime03" runat="server" GroupName="Status" />
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" CssClass="headerCell" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" CssClass="itemCell" />
                                <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="pagerCell" />
                                <SelectedItemStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" />
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
            </div>
            <div id="member" class="tab-pane fade">
                <div class="row mini--space__top">
                    <div class="col-xs-3">
                        <label style="padding-left: 20px;">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01897") %></label>
                    </div>
                    <div class="col-xs-9">
                        <input type="button" class="btn btn-info btn-fixed-width"
                            id="ctl11_rebcTime04" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121072") %>" />
                        <input type="button" class="btn btn-info btn-fixed-width"
                            id="ctl11_rebcTime05" value="มีสิทธ์การเข้าถึง" />
                        <input type="button" class="btn btn-info btn-fixed-width"
                            id="ctl11_rebcTime06" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121053") %>" />
                    </div>
                </div>
                <div class="row ">
                    <div class="col-xs-12">
                        <div class="wrapper-table">
                            <table class="cool-table cool-table-7" id="ctl00_MainContent_dgdMenu" style="width: 100%; font-size: 22px; font-style: normal; font-weight: normal; text-decoration: none; border-collapse: collapse;"
                                border="0" cellspacing="0" cellpadding="2">
                                <tbody>
                                    <tr class="headerCell" style="font-style: normal; font-weight: bold; text-decoration: none;">
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121054") %>
                                        </td>
                                        <td>
                                            <div class="center">
                                                ไม่มีสิทธ์
                                            </div>
                                        </td>
                                        <td>
                                            <div class="center">
                                                มีสิทธ์การเข้าถึง
                                            </div>
                                        </td>
                                        <td>
                                            <div class="center">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121053") %>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="itemCell" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701003") %>
                                        </td>
                                        <td align="center" style="width: 20%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl01_rebcMember01" runat="server" GroupName="rebcMember01" />
                                        </td>
                                        <td align="center" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl01_rebcMember02" runat="server" GroupName="rebcMember01" />
                                        </td>
                                        <td align="center" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl01_rebcMember03" runat="server" GroupName="rebcMember01" />
                                        </td>
                                    </tr>
                                    <tr class="alternateCell" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701026") %>
                                        </td>
                                        <td align="center" style="width: 20%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl02_rebcMember01" runat="server" GroupName="rebcMember02" />
                                        </td>
                                        <td align="center" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl02_rebcMember02" runat="server" GroupName="rebcMember02" />
                                        </td>
                                        <td align="center" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl02_rebcMember03" runat="server" GroupName="rebcMember02" />
                                        </td>
                                    </tr>
                                    <tr class="itemCell" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %>
                                        </td>
                                        <td align="center" style="width: 20%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl03_rebcMember01" runat="server" GroupName="rebcMember03" />
                                        </td>
                                        <td align="center" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl03_rebcMember02" runat="server" GroupName="rebcMember03" />
                                        </td>
                                        <td align="center" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl03_rebcMember03" runat="server" GroupName="rebcMember03" />
                                        </td>
                                    </tr>
                                    <tr class="itemCell" id="tab03_trReport" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121059") %>
                                        </td>
                                        <td align="center" style="width: 20%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl04_rebcMember01" runat="server" GroupName="rebcMember04" />
                                        </td>
                                        <td align="center" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl04_rebcMember02" runat="server" GroupName="rebcMember04" />
                                        </td>
                                        <td align="center" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;"></td>
                                    </tr>
                                    <tr class="itemCell" style="font-style: normal; font-weight: normal; text-decoration: none;">
                                        <td align="left" class="dgddt" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102227") %>
                                        </td>
                                        <td align="center" style="width: 20%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl05_rebcMember01" runat="server" GroupName="rebcMember05" />
                                        </td>
                                        <td align="center" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl05_rebcMember02" runat="server" GroupName="rebcMember05" />
                                        </td>
                                        <td align="center" style="width: 25%; font-style: normal; font-weight: normal; text-decoration: none;">
                                            <asp:RadioButton ID="ctl05_rebcMember03" runat="server" GroupName="rebcMember05" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr style="margin-top: 15px; bottom: 50px" />
        <div class="row space">
            <div class="col-xs-12 center">
                <asp:Button ID="btnSave" class="btn btn-success global-btn"
                    runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" ValidationGroup="RegisterUser" />
                &nbsp;<asp:Button ID="btnCancel" class="btn btn-danger global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
            </div>
        </div>
    </div>
</asp:Content>
