<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="report05.aspx.cs" Inherits="FingerprintPayment.report05" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .completionList
        {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }
        
        .listItem
        {
            color: blue;
            background-color: White;
        }
        
        .itemHighlighted
        {
            background-color: #ffc0c0;
        }
    </style>
    <script language="javascript">
        function SetContextKey() {
            $find('<%=AutoCompleteExtender2.ClientID%>').set_contextKey($get("<%=ddlcType.ClientID %>").value);
        }
        function PrintElem(elem) {
            Popup($(elem).html());
        }
        function Popup(data) {
            var mywindow = window.open('', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131116") %>', 'height=400,width=900');
            mywindow.document.write('<html><head><title><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131116") %></title>');
            /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
            mywindow.document.write('</head><body >');
            mywindow.document.write(data);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10

            mywindow.print();
            mywindow.close();

            return true;
        }
        $(document).ready(function () {
            $('.datepicker').datepicker({});
            //            var d = new Date();
            //            var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear());
            //            $("input[id$='txtdStart']").datepicker({ showOn: 'button', buttonImageOnly: true, buttonImage: 'images/calendar_blue.png', changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy', isBuddhist: true, defaultDate: toDay, dayNames: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202007") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202008") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202009") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603009") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202011") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202012") %>'],
            //                dayNamesMin: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131005") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131006") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131007") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131008") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131009") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131010") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131011") %>'],
            //                monthNames: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>'],
            //                monthNamesShort: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210010") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210012") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210014") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210015") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210016") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210017") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210018") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210019") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210020") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210021") %>']
            //            });
            //            $("input[id$='txtdEnd']").datepicker({ showOn: 'button', buttonImageOnly: true, buttonImage: 'images/calendar_blue.png', changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy', isBuddhist: true, defaultDate: toDay, dayNames: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202007") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202008") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202009") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603009") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202011") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202012") %>'],
            //                dayNamesMin: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131005") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131006") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131007") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131008") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131009") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131010") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131011") %>'],
            //                monthNames: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>'],
            //                monthNamesShort: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210010") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210012") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210014") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210015") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210016") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210017") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210018") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210019") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210020") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210021") %>']
            //            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtsunit"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTUser" ServicePath="AutoCompleteService.asmx" EnableCaching="true"
        FirstRowSelected="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>
    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTProduct" ServicePath="AutoCompleteService.asmx" EnableCaching="true"
        FirstRowSelected="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="report-card box-content">
        <div class="row">
            <div class="col-xs-2">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtsunit" runat="server" CssClass='form-control' Style="width: 300px;" />
            </div>
        </div>
        <div class="row row--space">
            <div class="col-xs-2">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
            </div>
            <div class="col-xs-4">
                <div class="input-group">
                    <asp:TextBox ID="txtdStart" runat="server" class="form-control datepicker" />
                    <div class="input-group-addon">
                        <i class="glyphicon glyphicon-calendar"></i>
                    </div>
                </div>
                <cc1:MaskedEditExtender ID="makdstart" runat="server" TargetControlID="txtdStart"
                    OnInvalidCssClass="" OnFocusCssClass="" MessageValidatorTip="true" MaskType="Date"
                    Mask="99/99/9999" ErrorTooltipEnabled="True" ClearMaskOnLostFocus="true" AcceptAMPM="false">
                </cc1:MaskedEditExtender>
            </div>
            <div class="col-xs-1">
                <label class="center">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
            </div>
            <div class="col-xs-4">
                <div class="input-group">
                    <asp:TextBox ID="txtdEnd" runat="server" class="form-control datepicker" />
                    <div class="input-group-addon">
                        <i class="glyphicon glyphicon-calendar"></i>
                    </div>
                </div>
                <cc1:MaskedEditExtender ID="makdend" runat="server" TargetControlID="txtdEnd" OnInvalidCssClass=""
                    OnFocusCssClass="" MessageValidatorTip="true" MaskType="Date" Mask="99/99/9999"
                    ErrorTooltipEnabled="True" ClearMaskOnLostFocus="true" AcceptAMPM="false">
                </cc1:MaskedEditExtender>
            </div>
        </div>
        <div class="row row--space">
            <div class="col-xs-2">
                <label class="pull-right label--filter">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603091") %></label>
            </div>
            <div class="col-xs-3">
                <asp:DropDownList class="input--filter input--short" ID="ddlcType" runat="server"
                    AutoPostBack="True">
                </asp:DropDownList>
            </div>
            <div class="col-xs-2">
                <label class="pull-right label--filter">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %></label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtSearch" CssClass='form-control' Style="width: 300px;" runat="server"
                    onkeyup="SetContextKey()" AutoCompleteType="Disabled" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12 center">
                <asp:Button ID="btnSearch0" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" class="btn btn-primary" />
                &nbsp;<asp:Button ID="btnSearch2" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132151") %>" class="btn btn-danger"
                    PostBackUrl="~/reportmain.aspx" />
                &nbsp;<input type="button" value="Print" class="btn btn-success" onclick="PrintElem('#divprint');" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div id="divprint">
                    <asp:Literal ID="ltrHtml" runat="server" />
                </div>
            </div>
        </div>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>--%>
