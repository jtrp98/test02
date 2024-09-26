<%@ Page Title="" Language="C#" MasterPageFile="~/mp2.Master" AutoEventWireup="true" CodeBehind="addemployeesstartsystem.aspx.cs" Inherits="FingerprintPayment.addemployeesstartsystem1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp() {

            if ($("input[id*=txtsName]").val() + "" + $("input[id*=txtLastName]").val() == "") {
                j_alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121048") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121049") %>');
                return false;
            }
            else if ($("input[id*=txtsName]").val() + "" + $("input[id*=txtLastName]").val() == undefined) {
                j_alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121048") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121049") %>');
                return false;
            }
            else if (document.getElementById("aspnetForm").ctl00$MainContent$txtsIdentification.value == "") {
                j_alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121048") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121050") %>');
                return false;
            }
            else {
                popUpObj = window.showModalDialog('frmRegister.aspx?page=emp&Firstname=' + $("input[id*=txtsName]").val()
                + '&Lastname=' + $("input[id*=txtLastName]").val()
                + '&IDCARD=' + document.getElementById("aspnetForm").ctl00$MainContent$txtsIdentification.value, this, 'status:1; resizable:1; dialogWidth:420px; dialogHeight:520px; dialogTop=180px; dialogLeft:510px; scroll:no;status=no');
                //                      popUpObj.focus();
                //                      LoadModalDiv();
            }
        }
    </script>
    <script type="text/javascript">
        function autoTab2(obj, typeCheck) {
            /* กำหนด<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401010") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133150") %>ให้ _ แทนค่าอะไ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ก็<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132249") %> แล้ว<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00734") %>มด้วยเค<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ื่องหมาย 
            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %>สัญลักษณ์ที่ใช้แบ่ง เช่นกำหนด<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01080") %>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603033") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131288") %> 
            4-2215-54125-6-12 ก็สามา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ถกำหนด<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01080") %>  _-____-_____-_-__ 
            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603033") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %> 08-4521-6521 กำหนด<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01080") %> __-____-____ 
            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %>กำหนด<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %>เช่น 12:45:30 กำหนด<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01080") %> __:__:__ 
            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504038") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>้างล่าง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01080") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>กำหนด<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603033") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %> 
            */
            //            if (typeCheck == 1) {
            var pattern = new String("_-____-_____-_-__"); // กำหนด<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603033") %>ในนี้  
            var pattern_ex = new String("-"); // กำหนดสัญลักษณ์<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %>เค<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ื่องหมายที่ใช้แบ่งในนี้       
            //            } else {
            //                var pattern = new String("__-____-____"); // กำหนด<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603033") %>ในนี้  
            //                var pattern_ex = new String("-"); // กำหนดสัญลักษณ์<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %>เค<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ื่องหมายที่ใช้แบ่งในนี้                   
            //            }
            var returnText = new String("");
            var obj_l = obj.value.length;
            var obj_l2 = obj_l - 1;
            for (i = 0; i < pattern.length; i++) {
                if (obj_l2 == i && pattern.charAt(i + 1) == pattern_ex) {
                    returnText += obj.value + pattern_ex;
                    obj.value = returnText;
                }
            }
            if (obj_l >= pattern.length) {
                obj.value = obj.value.substr(0, pattern.length);
            }
        }
        function Mgsalert(str) {
            j_info('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>', str);
            return false;
        }
        function IsNumeric_citizen(sText, obj) {
            var ValidChars = "0123456789.";
            var IsNumber = true;
            var Char;
            for (i = 0; i < sText.length && IsNumber == true; i++) {
                Char = sText.charAt(i);
                if (ValidChars.indexOf(Char) == -1) {
                    if (Char != "-") {
                        IsNumber = false;
                    }
                }
            }
            if (IsNumber == false) {
                alert("Only numberic value");
                obj.value = sText.substr(0, sText.length - 1);
            } else {
                autoTab2(obj, 1);
            }
        }
        $(document).ready(function () {
            $('.datepicker').datepicker({});
            var d = new Date();
            var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);
            $("input[id$='txtdDate']").datepicker({
                showOn: 'button', buttonImageOnly: true, buttonImage: 'images/calendar_blue.png', changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy', isBuddhist: true, defaultDate: toDay, dayNames: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202007") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202008") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202009") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603009") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202011") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202012") %>'],
                dayNamesMin: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131005") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131006") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131007") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131008") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131009") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131010") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131011") %>'],
                monthNames: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>'],
                monthNamesShort: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210010") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210012") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210014") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210015") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210016") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210017") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210018") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210019") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210020") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210021") %>']
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="detail-card box-content">
        <asp:Label ID="lblUserID" runat="server" Style="display: none;" />
        <div class="row">
            <div class="col-lg-12 center">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131004") %></label>
            </div>
        </div>
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
                <asp:TextBox ID="txtUserFinger2" runat="server" Style="display: none;" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    ประเถทสมาชิก</label>
            </div>
            <div class="col-xs-8">
                <asp:RadioButtonList ID="rblcUserType" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %>" Selected="True" Value="1" />
                    <asp:ListItem Text="&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102166") %>" Value="2" />
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtsName" runat="server" CssClass='form-control' />
                <%--<asp:RequiredFieldValidator ID="reqtxtsName" runat="server" ControlToValidate="txtsName"
                    CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="กรุณากรอกชืื่อ"
                    ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>--%>
            </div>
            <div class="col-xs-4">
                <asp:Button ID="btnImport" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121063") %>" class="btn btn-info vertical-mid" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtLastName" runat="server" CssClass='form-control' />
                <%--     <asp:RequiredFieldValidator ID="reqtxtLastName" runat="server" ControlToValidate="txtsName"
                    CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121064") %>"
                    ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>--%>
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
            <div class="col-xs-8">
                <asp:DropDownList ID="ddlcUserType" runat="server">
                </asp:DropDownList>
                <%--onKeyUp="IsNumeric_citizen(this.value,this)" --%>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206327") %></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtsIdentification" runat="server" MaxLength="13" CssClass='form-control'
                    Style="width: 300px;" />
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
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtsPhone" runat="server" MaxLength="10" CssClass='form-control'
                    Style="width: 300px;" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    E-Mail</label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtsEmail" runat="server" CssClass='form-control' Style="width: 300px;" />
                <%--         <asp:RequiredFieldValidator ID="reqtxtsEmail" runat="server" ControlToValidate="txtsName"
                    CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121067") %>"
                    ValidationGroup="RegisterUser">*</asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121068") %></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtsAddress" runat="server" TextMode="MultiLine" CssClass='form-control'
                    Style="width: 300px; height: 100px;" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 center">
                <asp:Button ID="btnSave" OnClientClick="showModalPopUp();" class="btn btn-success"
                    runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121081") %>" ValidationGroup="RegisterUser" />
                &nbsp;<asp:Button ID="btnCancel" class="btn btn-danger" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
            </div>
        </div>
        <div class="row--space">
        </div>
    </div>
</asp:Content>
