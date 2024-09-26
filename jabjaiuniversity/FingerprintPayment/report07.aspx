<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="report07.aspx.cs" Inherits="FingerprintPayment.report07" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .completionList
        {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
            z-index: 9999;
        }
        .listItem
        {
            color: blue;
            background-color: White;
            z-index: 9999;
        }
        .itemHighlighted
        {
            background-color: #ffc0c0;
            z-index: 9999;
        }
    </style>
    <script language="javascript">
        function PrintElem(elem) {
            var sID = "report07";
            var sfilter = $("select[id*=ddlcType]").val() + "|" + $("input[id*=txtSearch]").val() + "|" + $("select[id*=ddlSearch]").val();
            $.ajax({
                type: "POST",
                url: "PagePrint.ashx",
                cache: false,
                dataType: "html",
                data: { ID: encodeURIComponent(sID), filter: encodeURIComponent(sfilter) },
                success: function (response) {
                    var _str = response;
                    if (_str == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131117") %>")
                        j_alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131119") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131117") %>");
                    else
                        Popup(response);
                }
            });

        }
        function Popup(data) {

            var mywindow = window.open('', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131118") %>', 'height=400,width=900');
            mywindow.document.write('<html><head><title><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131118") %></title>');
            /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
            mywindow.document.write('<link href="/bootstrap/css/bootstrap.css" rel="Stylesheet" type="text/css" />');
            mywindow.document.write('<link href="/bootstrap/css/bootstrap-clockpicker.min.css" rel="stylesheet" type="text/css" />');
            mywindow.document.write('</head><body >');
            mywindow.document.write(data);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10

            mywindow.print();
            mywindow.close();

            return true;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTUser" ServicePath="AutoCompleteService.asmx" EnableCaching="true"
        FirstRowSelected="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>
    <div class="report-card box-content">
        <div class="row">
            <div class="col-xs-2">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlcType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcType_SelectedIndexChanged"
                    class="input--short">
                </asp:DropDownList>
            </div>
            <div class="col-xs-2">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301008") %></label>
            </div>
            <div class="col-xs-6">
                <asp:TextBox ID="txtSearch" runat="server" AutoCompleteType="Disabled" OnTextChanged="txtSearch_TextChanged"
                    AutoPostBack="True" class="input--mid" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-2">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106151") %></label>
            </div>
            <div class="col-xs-10">
                <asp:DropDownList ID="ddlSearch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcType_SelectedIndexChanged"
                    class="input--short">
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="0" Selected="True" />
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131120") %>" Value="2" />
                    <asp:ListItem Text="ไม่มียอดนระบบ" Value="1" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12 center">
                <asp:Button ID="btnSearch2" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132151") %>" class="btn btn-danger"
                    PostBackUrl="~/reportmain.aspx" />
                &nbsp;<input type="button" value="Print" class="btn btn-success" onclick="PrintElem('#divprint');" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <center>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104001") %></center>
                <asp:ListView ID="lvCustomers" runat="server" GroupPlaceholderID="groupPlaceHolder1"
                    ItemPlaceholderID="itemPlaceHolder1">
                    <LayoutTemplate>
                        <table class="table table-condensed table-bordered" style="width: 100%; font-size: 20px;">
                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                            <tr>
                                <td colspan="6">
                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvCustomers" PageSize="20"
                                        OnPagePropertiesChanging="OnPagePropertiesChanging">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link" />
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="true"
                                                ShowPreviousPageButton="false" />
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                        </tr>
                    </GroupTemplate>
                    <ItemTemplate>
                        <%# AddHeader()%>
                        <%# trHeader == 0 || string.Empty == header ? "" : @" <tr class=""warning"">
                                <td style=""width: 15%;text-align: center;"">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                </td>
                                <td style=""width: 15%;text-align: center;"">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                </td>
                                <td style=""width: 25%;text-align: center;"">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206327") %>
                                </td>
                                <td style=""width: 15%;text-align: center;"">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105034") %>
                                </td>
                                <td style=""text-align: center;"">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132152") %>
                                </td>
                                <td style=""width: 15%;text-align: center;"">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105046") %>
                                </td></tr>"%>
                        <tr class='active'>
                            <td style='text-align: left;'>
                                <%# Eval("sName")%>
                            </td>
                            <td style='text-align: left;'>
                                <%#  Eval("sLastname")%>
                            </td>
                            <td style='text-align: center;'>
                                <%# Eval("sIdentification").ToString()%>
                            </td>
                            <td style='text-align: right;'>
                                <%# DateTime.Parse(Eval("dBirth").ToString()).ToShortDateString() %>
                            </td>
                            <td style='text-align: right;'>
                                <%#  Eval("nMax")%>
                            </td>
                            <td style='text-align: right;'>
                                <%# Eval("nMoney").ToString()%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
</asp:Content>
