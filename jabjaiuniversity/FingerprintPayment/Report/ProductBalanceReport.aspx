<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="ProductBalanceReport.aspx.cs" Inherits="FingerprintPayment.ProductBalanceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <script type="text/javascript" src="/Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="/Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="/Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="/Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="/Scripts/tableExport/jspdf/libs/base64.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
            z-index: 9999;
        }

        .listItem {
            color: blue;
            background-color: White;
            z-index: 9999;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
            z-index: 9999;
        }
    </style>
    <script type="text/javascript">
        function AutocompleteProduct() {
            $('input[name*=txtSearch]').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                source: function (request, response) {
                    var results;
                    results = $.ui.autocomplete.filter(availableValueProduct, request.term);
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("input[name*=txtSearch]").val(ui.item.label);
                    $("input[name=productid]").val(ui.item.value);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                    $("input[name=productid]").val("");
                }
            });
        }

        function Reports() {
            $("body").mLoading();
            $("#example").html();
            var thead = $("#example thead");
            var tbody = $("#example tbody");
            thead.html("");
            tbody.html("");
            var search = "";
            $("#divprint").html("");
            var dt = new Date();

            $.post("/app_Logic/Report/ProductBalanceReport.ashx?producttype=" + $("select[id*=ddlcType]").val()
                + "&productname=" + encodeURIComponent($("input[id*=txtSearch]").val()), "", function (result) {
                    if (result.length === 0) {
                        $("#divprint").html("<center><h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105021") %></h1></center>");
                    }
                    else {
                        var headerreport = '<tr><th colspan="4" style="text-align: center; font-size: 26px; font-weight: bolder;border:0px;" id="school">' + $("input[id*=hdfschoolname]").val();
                        headerreport += '<tr><th colspan="4" style="text-align: center; font-size: 22px; font-weight: bolder;border:0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603090") %>';
                        headerreport += '<tr><th colspan="4" style="text-align: right; font-size: 22px; font-weight: bolder;border:0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603092") %>&nbsp;' + dt.toLocaleDateString();
                        headerreport += '<tr><th colspan="4" style="text-align: right; font-size: 22px; font-weight: bolder;border:0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> :&nbsp;' + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds();
                        headerreport += '<tr><th colspan="4" style="text-align: right; font-size: 22px; font-weight: bolder;border:0px;"><br>';
                        thead.append(headerreport);

                        var headertable = "<tr  style='background-color:#337ab7;color:#fff;'>";
                        headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>';
                        headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder">BarCode';
                        headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %>';
                        headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405006") %>';

                        tbody.append(headertable);
                        let result_1 = [];
                        $.each(result, function (index) {
                            var rowstable = "<tr class='active'>";
                            rowstable += "<td style='text-align:center;font-size: 20px;'>" + (index + 1);
                            rowstable += "<td style='text-align:right; font-size: 20px;' format='string'>" + result[index].barcode;
                            rowstable += "<td style='text-align:right;font-size: 20px;'>" + result[index].productname;
                            rowstable += "<td style='text-align:right;font-size: 20px;'>" + result[index].aumont;
                            tbody.append(rowstable);
                            if (result[index].aumont !== 0) result_1.push(result[index]);
                        });

                        console.table(result_1);
                        result_c = result_1;
                    }
                    $("body").mLoading('hide');
                });
        }

        var result_c = [];

        //function ClearData() {
        //    let SQL = "";
        //    $.each(result_c, function (e, s) {
        //        if (s.aumont != 0)
        //            SQL += "update TProduct set nNumber = ISNULL(nNumber,0) -  " + s.aumont + " ,UpdatedDate = DATEADD(hh,7,GETUTCDATE())  where nProductID = " + s.product_id + " \n ";
        //    });

        //    console.log(SQL);
        //}

        var availableValueProduct = [];
        function getlistproduct() {
            var typeproduct = $('select[id*=ddlcType]').val();
            availableValueProduct = [];
            $.ajax({
                url: "/App_Logic/dataJSon.ashx?mode=productlist&id=" + typeproduct,
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].label,
                            value: objjson[index].value
                        };
                        availableValueProduct[index] = newObject;
                    });
                }
            });
        }

        function runScript(e) {
            if (e.keyCode == 13) {
                Reports();
                return false;
            }
        }

        $(function () {
            $("select[name*=ddlcType]").change(function () {
                $("input[name=txtSearch]").val("");
                $("input[name=productid]").val("");
                getlistproduct();
            });

            $("#exportfile").click(function () {
                var dt = new Date();
                $('#example').tableExport({ type: 'excel', escape: 'false', sheets: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603090") %>', fileName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603001") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206458") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603006") %>_" + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
            });

            getlistproduct();
            AutocompleteProduct();
        });
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdfschoolname" runat="server" />
    <div class="report-card box-content">
        <div class="row">
            <div class="col-xs-2">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603091") %></label>
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlcType" runat="server" OnSelectedIndexChanged="ddlcType_SelectedIndexChanged"
                    class="input--short">
                </asp:DropDownList>
            </div>
            <div class="col-xs-2">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %></label>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtSearch" runat="server" onkeypress="return runScript(event)"
                    CssClass='form-control col-lg-12' />
            </div>
            <div class="col-lg-2">
                <input type="button" class="btn btn-primary button-custom col-xs-12" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="Reports();" />
            </div>
        </div>
        <div class="row mini--space__top hidden">
            <div class="col-xs-12 center">
                <asp:Button ID="btnSearch2" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132151") %>" class="btn btn-danger" />
                &nbsp;<input type="button" value="Print" class="btn btn-success" onclick="PrintElem('#divprint');" />
                <%--  &nbsp;<input type="button" value="Export To Excel" id="btnExport" class="btn btn-success"
                    onclick="ExcelElem();" />--%>
            </div>

        </div>
        <div class="row">
            <div class="col-xs-12 center">
                <div class="btn btn-success button-custom col-xs-12 col-md-12 col-sm-12" id="exportfile">Export File</div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-2 col-md-2 col-sm-2">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %>
            </div>
            <div class="col-xs-8 col-md-8 col-sm-8">
            </div>
            <div class="col-xs-2 col-md-2 col-sm-2">
            </div>
        </div>
        <div class="row border-bottom">
            <br />
            <div class="col-sm-12">
                <fieldset>
                    <asp:ListView ID="lvReport" runat="server">
                    </asp:ListView>
                    <table id="example" class="table table-condensed table-bordered table-show-result"
                        cellspacing="0" width="100%">
                        <thead></thead>
                        <tbody></tbody>
                        <%--<tfoot>
                            <tr>
                                <th colspan="6" style="text-align: center;">
                                    <ul class="pagination pagination-lg pager" id="myPager" />
                                </th>
                            </tr>
                        </tfoot>--%>
                    </table>
                </fieldset>
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <asp:ListView ID="lvCustomers" runat="server" GroupPlaceholderID="groupPlaceHolder1"
                    ItemPlaceholderID="itemPlaceHolder1" OnPagePropertiesChanging="OnPagePropertiesChanging"
                    OnSelectedIndexChanged="lvCustomers_SelectedIndexChanged">
                    <LayoutTemplate>
                        <table class="table table-condensed table-bordered" style="width: 100%; font-size: 20px;">
                            <tr class="warning">
                                <td style="width: 50%;">BarCode
                                </td>
                                <td style="width: 25%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %>
                                </td>
                                <td style="width: 25%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405006") %>
                                </td>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                            <tr>
                                <td colspan="3">
                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvCustomers" PageSize="20">
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
                        <tr class='active'>
                            <td style='text-align: left;'>
                                <%# Eval("sBarCode")%>
                            </td>
                            <td style='text-align: left;'>
                                <%#  Eval("sProduct")%>
                            </td>
                            <td style='text-align: center;'>
                                <%# CountNumberProduct_Reprot04(Eval("sBarCode").ToString())%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                <asp:Literal runat="server" ID="ltrHtml"></asp:Literal>
                <div id="divprint2" style="display: none;">
                    <asp:ListView ID="lvPrint" runat="server" GroupPlaceholderID="groupPlaceHolder1"
                        ItemPlaceholderID="itemPlaceHolder1" OnPagePropertiesChanging="OnPagePropertiesChanging">
                        <LayoutTemplate>
                            <table class="table table-condensed table-bordered" style="width: 100%; font-size: 20px;">
                                <tr class="warning">
                                    <td style="width: 50%;">BarCode
                                    </td>
                                    <td style="width: 25%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %>
                                    </td>
                                    <td style="width: 25%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405006") %>
                                    </td>
                                </tr>
                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <GroupTemplate>
                            <tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                            </tr>
                        </GroupTemplate>
                        <ItemTemplate>
                            <tr class='active'>
                                <td style='text-align: left;'>
                                    <%# Eval("sBarCode")%>
                                </td>
                                <td style='text-align: left;'>
                                    <%#  Eval("nProductID")%>
                                </td>
                                <td style='text-align: center;'>
                                    <%#  Eval("balances")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
