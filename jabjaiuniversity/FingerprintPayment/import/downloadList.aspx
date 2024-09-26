<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="downloadList.aspx.cs" Inherits="FingerprintPayment.import.downloadList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/FileSaver.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
        label {
            font-weight: normal;
            font-size: 26px;
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .centertext {
            text-align: center;
        }

        .centerText {
            text-align: center;
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }
    </style>
    <style>
        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }

        .cover {
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .listItem {
            color: blue;
            background-color: White;
        }

        .hid {
            visibility: hidden;
        }

        .hid2 {
            visibility: hidden;
            display: none;
        }

        #loading {
            display: block;
            position: fixed;
            top: 0;
            left: 0;
            z-index: 100;
            width: 100vw;
            height: 100vh;
            background-color: rgba(192, 192, 192, 0.5);
            background-image: url("https://i.imgur.com/CgViPo0.gif");
            background-repeat: no-repeat;
            background-position: center;
        }

        .width10 {
            margin: 0 auto;
            width: 10%;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }

        .gvbutton {
            font-size: 25px;
        }

        .nounder a:hover {
            text-decoration: none;
        }

        .shadowblack {
            text-decoration: none;
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .boxhead a {
            color: #FFFFFF;
            text-decoration: none;
        }

        a.imjusttext {
            color: #ffffff;
            text-decoration: none;
        }

            a.imjusttext:hover {
                color: aquamarine;
            }

        .btn-red {
            background: red; /* use your color here */
        }


        .nowrap {
            max-width: 100%;
            white-space: nowrap;
        }

        .namemangin {
            margin-left: 5px;
            padding-left: 35px;
            border-left: 10px
        }

        .tab {
            border-collapse: collapse;
            margin-left: 6px;
            margin-right: 6px;
            border-bottom: 3px solid #337AB7;
            border-left: 3px solid #337AB7;
            border-right: 3px solid #337AB7;
            border-top: 3px solid #337AB7;
            box-shadow: inset 0 1px 0 #337AB7;
        }
    </style>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>
    <div class="full-card box-content userlist-container mainpage">
        <div class="col-xs-12 centerText">
            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01618") %></label>
        </div>
        <div class="col-xs-12 hid" style="font-size: 30%">
            <p>hidden</p>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <table class="cool-table" id="ctl00_MainContent_dgd" style="font-weight: normal; font-style: normal; text-decoration: none; width: 100%; border-collapse: collapse;" cellspacing="0" cellpadding="2" border="0">
                        <tbody>
                            <tr class="headerCell" style="font-weight: bold; font-style: normal; text-decoration: none;">
                                <th class="centertext" scope="col" style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                <th class="centertext" scope="col" style="width: 50%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00551") %></th>
                                <th scope="col" style="width: 20%;">&nbsp;</th>
                                <th scope="col" style="width: 20%;">&nbsp;</th>
                            </tr>
                            <tr class="itemCell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">1</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M901001") %></td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10">
                                            <a href="Import score version 1.4.6.xlsx" class="btn btn-success button1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M900001") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10">
                                            <a target="_blank" href="https://www.youtube.com/watch?v=iiZW5h5bL3k" class="btn btn-info button3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02011") %></a>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">2</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132293") %></td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10">
                                            <a href="Import old grade version 1.1.xlsx" class="btn btn-success button1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00636") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10">
                                            <a target="_blank" href="https://www.youtube.com/watch?v=jvHbC8kJ65Y&feature=youtu.be" class="btn btn-info button3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132292") %></a>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">3</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M901001") %></td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10">
                                            <a href="Import Student 2.3.9.xlsx" class="btn btn-success button1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00636") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">4</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132294") %></td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10">
                                            <a href="import employees 1.5.1.xlsx" class="btn btn-success button1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00636") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">5</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M901001") %></td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10">
                                            <a href="import Schedule 1.0.xlsx" class="btn btn-success button1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00636") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">6</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M901001") %></td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10" style="display: flex;">
                                            <a id="aTempInvoices" href="#" class="btn btn-success button1" onclick="export_excel()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00636") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">7</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M901001") %> </td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10" style="display: flex;">
                                            <a href="moneybalance%201.0.xls" class="btn btn-success button1" style="margin-left: 5px;" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00636") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">8</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01295") %></td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10" style="display: flex;">
                                            <a href="Import%20Library%20Management%201.0.xlsx" class="btn btn-success button1" style="margin-left: 5px;" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00636") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">9</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M901001") %></td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10" style="display: flex;">
                                            <a href="ฟอ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>์ม<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206392") %>%20SB%20exam%201.0.xlsx" class="btn btn-success button1" style="margin-left: 5px;" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00636") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">10</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132295") %> </td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10" style="display: flex;">
                                            <a href="pair_answe_sheet/pair answer sheet 30 TH 4key [22032023].pdf" class="btn btn-success button1" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00637") %></a>
                                            <a href="pair_answe_sheet/pair answer sheet 30 EN 4key [22032023].pdf" class="btn btn-success button1" style="margin-left: 5px;" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00638") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">11</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132296") %> </td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10" style="display: flex;">
                                            <a href="pair_answe_sheet/pair answer sheet 50 TH 4key [22032023].pdf" class="btn btn-success button1" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00637") %></a>
                                            <a href="pair_answe_sheet/pair answer sheet 50 EN 4key [22032023].pdf" class="btn btn-success button1" style="margin-left: 5px;" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00638") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">12</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132297") %> </td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10" style="display: flex;">
                                            <a href="pair_answe_sheet/pair answer sheet 60 TH 4key [22032023].pdf" class="btn btn-success button1" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00637") %></a>
                                            <a href="pair_answe_sheet/pair answer sheet 60 EN 4key [22032023].pdf" class="btn btn-success button1" style="margin-left: 5px;" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00638") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">13</td>
                                <td class="lefttext">ดาวน์โหลดกระดาษคำตอบ แบบ 100 ข้อ</td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10" style="display: flex;">
                                            <a href="pair_answe_sheet/pair answer sheet 100 TH 4key [22032023].pdf" class="btn btn-success button1" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00637") %></a>
                                            <a href="pair_answe_sheet/pair answer sheet 100 EN 4key [22032023].pdf" class="btn btn-success button1" style="margin-left: 5px;" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00638") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">14</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M902001") %> SchoolBrightTempScan <span id="spnTempScanVersion"></span></td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10" style="display: flex;">
                                            <a id="aTempScanUrl32" href="#" class="btn btn-success button1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M900001") %><sup style="position: absolute; margin: 19px 0px 0px -23px; font-size: 60%;">(32 bit)</sup></a>
                                            <a id="aTempScanUrl64" href="#" class="btn btn-success button1" style="margin-left: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M900001") %><sup style="position: absolute; margin: 19px 0px 0px -23px; font-size: 60%;">(64 bit)</sup></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">15</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M902001") %> School Bright Shop Window 1.0</td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10" style="display: flex;">
                                            <a href="https://schoolbrightapp.s3.ap-southeast-1.amazonaws.com/SchoolBrightShop/x32/SB_Shop_32_10016.exe" class="btn btn-success button1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M900001") %><sup style="position: absolute; margin: 19px 0px 0px -23px; font-size: 60%;">(32 bit)</sup></a>
                                            <a href="https://schoolbrightapp.s3.ap-southeast-1.amazonaws.com/SchoolBrightShop/x64/SB_Shop_64_10016.exe" class="btn btn-success button1" style="margin-left: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M900001") %><sup style="position: absolute; margin: 19px 0px 0px -23px; font-size: 60%;">(64 bit)</sup></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="itemcell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                                <td class="centertext">16</td>
                                <td class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132298") %></td>
                                <td>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10" style="display: flex;">
                                            <a href="PDPA%20v.1%20Jabjai%20Corporation%20Co.,%20Ltd.pdf" class="btn btn-success button1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00636") %></a>
                                        </div>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:GridView ID="dgd" runat="server" Visible="false" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        OnDataBound="CustomersGridView_DataBound"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />

                        <PagerTemplate>

                            <table width="100%" class="tab">
                                <tr>
                                    <td style="width: 25%">

                                        <asp:Label ID="Label11" BorderColor="#337AB7"
                                            ForeColor="white"
                                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>:"
                                            runat="server" />
                                        <asp:DropDownList ID="PageDropDownList2"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged2"
                                            runat="server" />

                                    </td>
                                    <td style="width: 45%">
                                        <asp:LinkButton ID="backbutton" runat="server"
                                            CssClass="imjusttext" OnClick="backbutton_Click">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>
                                        </asp:LinkButton>
                                        <asp:DropDownList ID="PageDropDownList"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged"
                                            runat="server" />
                                        <asp:LinkButton ID="nextbutton" runat="server"
                                            CssClass="imjusttext" OnClick="nextbutton_Click">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
                                        </asp:LinkButton>

                                    </td>

                                    <td style="width: 70%; text-align: right">

                                        <asp:Label ID="CurrentPageLabel"
                                            ForeColor="white"
                                            runat="server" />

                                    </td>

                                </tr>
                            </table>

                        </PagerTemplate>

                        <Columns>
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="name" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00551") %>" ItemStyle-CssClass="lefttext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="50%"></HeaderStyle>
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10">
                                            <a href="Import score version 1.4.6" class="btn btn-success button1 hidden"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M900001") %></a>
                                            <a href="Import old grade version 1.0.xlsx" class="btn btn-success button2 hidden"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M900001") %> </a>

                                        </div>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <div class="col-xs-12">
                                        <div class="col-xs-10">
                                            <a target="_blank" href="//www.youtube.com" class="btn btn-info button3 hidden"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132292") %>  </a>
                                            <a target="_blank" href="//www.youtube.com" class="btn btn-info button4 hidden"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132292") %>  </a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:TemplateField>


                            <asp:BoundField DataFormatString="sFinger" HeaderText="sFinger" Visible="False"></asp:BoundField>

                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="headerCell" />
                        <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="itemCell" />

                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
    <script type="text/javascript">

        function InitTempScanInstaller() {
            $.ajax({
                async: false,
                type: "POST",
                url: "TempScanDownloadInstallFileHandler.ashx",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    //console.log(result);
                    if (result.success) {
                        $('#spnTempScanVersion').html(result.versionData.version);
                        $('#aTempScanUrl32').attr('href', result.versionData.setupx86.url);
                        $('#aTempScanUrl64').attr('href', result.versionData.setupx64.url);
                    }
                    else {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00645") %>[' + result.versionData.Message + ']');
                    }
                },
                failure: function (response) {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');
                },
                error: function (response) {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');
                }
            });
        }

        $(document).ready(function () {

            $.ajaxSetup({
                statusCode: {
                    500: function () {
                        window.location.replace("/Default.aspx");
                    }
                }
            });

            if (jQuery().dataTable) {
                $.fn.dataTable.ext.errMode = 'none';
            }

            InitTempScanInstaller();

        });

        function export_excel() {
            //$("body").mLoading();
            var xhr;

            //this.RenderHtml_Detail('table_exports', true);
            xhr = new XMLHttpRequest();
            xhr.open("POST", "/import/invoices.aspx/ExportExcl", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                saveAs(xhr.response, "ImportInvoice.xlsx");
                //$("body").mLoading('hide');
            };
            xhr.send();
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
