<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="TitleList.aspx.cs" Inherits="FingerprintPayment.TitleList.TitleList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <link href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" rel="stylesheet" />

    <style type="text/css">
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
            display: none;
        }

        .hid2 {
            visibility: hidden;
            display: none;
        }

        .width10 {
            margin: 0 auto;
            width: 10%;
        }

        .centertext {
            text-align: center;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }

        label {
            font-weight: normal;
            font-size: 26px;
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

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
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

        .centerText {
            text-align: center;
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
            border-left: 10px;
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
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

        .description {
            color: black;
        }
    </style>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">

    <%--<script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>
    <script src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var td_rows;
        var td_rows_en;
        $(function () {

            $("#modalpopup-data-submit").click(function () {
                var data = {
                    "nTitleid": $("#nTitleid").val(), "titleDescription": $("#titleDescription").val(), "titleDescriptionEn": $("#titleDescriptionEn").val()
                };
                PageMethods.updatedata(data, function (result) {
                    if ($("#modalpopup-data h1").html() === "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121098") %>") {
                        td_rows.html(data.titleDescription);
                        td_rows_en.html(data.titleDescriptionEn);
                        $("#modalpopup-data").modal("hide");
                    } else {
                        window.location.href = "/TitleList/TitleList.aspx";
                    }
                },
                    function (result) {
                        alert(result._meassage);
                    })
            });

            $("#modalpopup-data-cancel").click(function () {
                $("#modalpopup-data").modal("hide");
                clearinput();
            });
        })

        function getdata(title_id, control) {
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121098") %>");
            PageMethods.getdata(title_id,
                function (result) {
                    console.log(result);
                    $("#modalpopup-data").modal("show");
                    $("#nTitleid").val(result.nTitleid);
                    $("#titleDescription").val(result.titleDescription);
                    $("#titleDescriptionEn").val(result.titleDescriptionEn);
                    td_rows = ($(control).parent().parents("tr")).children("td.description");
                    td_rows_en = ($(control).parent().parents("tr")).children("td.descriptionEn");
                },
                function (result) {
                    console.log(result);
                })
        }

        function popupadd() {
            clearinput();
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133362") %>");
            $("#modalpopup-data").modal("show");
        }

        function statuschange(control) {
            var titleid = $(control).attr("titleid");
            console.log(titleid);
            PageMethods.StatusChange(titleid,
                function (reuslt) {
                    td_rows = ($(control).parent().parents("tr")).children("td.status");
                    if (reuslt === "notworking") {
                        $(td_rows).css({ "background-color": "HotPink" })
                        $(td_rows).html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102151") %>");
                        $(control).addClass("fa-toggle-off");
                        $(control).removeClass("fa-toggle-on");
                    } else {
                        $(td_rows).css({ "background-color": "PaleGreen" })
                        $(td_rows).html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>");
                        $(control).addClass("fa-toggle-on");
                        $(control).removeClass("fa-toggle-off");
                    }
                },
                function () {
                });

        }

        function clearinput() {
            $("#nTitleid").val("0");
            $("#titleDescription").val("");
            $("#titleDescriptionEn").val("");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102143") %>
            </p>
        </div>
    </div>

    <form runat="server" id="form1">

        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>


        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                    </div>
                    <div class="card-body ">

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102144") %></label>
                            <div class="col-md-3 ">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass='form-control' Style="width: 100%;"></asp:TextBox>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"></label>
                            <div class="col-md-3 ">
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-center">
                                <br />
                                <asp:Button ID="Button1" class="btn btn-primary global-btn"
                                    runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
                            </div>
                        </div>


                    </div>

                </div>
            </div>
        </div>



        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-warning card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102145") %></h4>
                    </div>
                    <div class="card-body ">

                        <div class="full-card box-content employeeslist-container">

                            <div class="row mini--space__top">
                                <div class="col-md-12">
                                    <div class="wrapper-table">
                                        <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                                            GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            OnDataBound="CustomersGridView_DataBound" ShowHeaderWhenEmpty="true" OnRowDataBound="dgd_RowDataBound1"
                                            Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="table-hover dataTable">
                                            <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                                            <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                                                BackColor="#337AB7" />
                                            <PagerTemplate>
                                                <table width="100%" class="tab">
                                                    <tr>
                                                        <td style="width: 25%">
                                                            <asp:Label ID="Label1" BorderColor="#337AB7" ForeColor="white"
                                                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>:" runat="server" />
                                                            <asp:DropDownList ID="PageDropDownList2" AutoPostBack="true"
                                                                OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged2"
                                                                runat="server" />
                                                        </td>
                                                        <td style="width: 45%">
                                                            <asp:LinkButton ID="backbutton" runat="server" CssClass="imjusttext" OnClick="backbutton_Click">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>
                                                            </asp:LinkButton>
                                                            <asp:DropDownList ID="PageDropDownList" AutoPostBack="true"
                                                                OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged"
                                                                runat="server" />
                                                            <asp:LinkButton ID="nextbutton" runat="server" CssClass="imjusttext" OnClick="nextbutton_Click">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
                                                            </asp:LinkButton>

                                                        </td>
                                                        <td style="width: 70%; text-align: right">
                                                            <asp:Label ID="CurrentPageLabel" ForeColor="white" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </PagerTemplate>

                                            <Columns>
                                                <asp:BoundField DataField="number" HeaderStyle-Width="100px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                                    <HeaderStyle Width="10%"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="description" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102146") %>" ItemStyle-CssClass="description">
                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="descriptionEn" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102147") %>" ItemStyle-CssClass="descriptionEn">
                                                    <HeaderStyle Width="25%"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="workingStatus" HeaderText="" HeaderStyle-CssClass="hid" ItemStyle-CssClass="hid">
                                                    <HeaderStyle Width="1%"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="status" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102148") %>" HeaderStyle-CssClass="status centertext" ItemStyle-CssClass="status centertext">
                                                    <HeaderStyle Width="20%"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                                    <ItemTemplate>
                                                        <div class="row">
                                                            <div class="col-xs-12 col-md-3 adjust-col-padding col-space nounder center"></div>
                                                            <div class="col-xs-12 col-md-2 adjust-col-padding col-space nounder center">
                                                                <a onclick="statuschange($(this))" titleid="<%# Eval("titleid") %>" id="btnStatus<%# Eval("titleid")%>" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103164") %>" class='<%# (bool)Eval("toggleOn") ?"fa fa-toggle-on":"fa fa-toggle-off" %>' style="font-size: 16px; cursor: pointer;"></a>
                                                            </div>
                                                            <div class="col-xs-12 col-md-2 adjust-col-padding col-space nounder center">
                                                                <a onclick="getdata('<%# Eval("titleid") %>',$(this));" data-toggle="tooltip" id="btnEdit<%# Eval("titleid")%>" class="btnedit fa fa-edit" style="font-size: 16px; cursor: pointer;" data-toggle="modal" data-target="#modalpopupdata"></a>
                                                            </div>
                                                            <div class="col-xs-12 col-md-2 adjust-col-padding col-space nounder">
                                                                <a href="/TitleList/TitleList-del.aspx?id=<%# Eval("titleid") %>" data-toggle="tooltip" id="btnDel<%# Eval("titleid")%>" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>" class="fa fa-remove" style="font-size: 16px; color: red;"></a>
                                                            </div>
                                                            <div class="col-xs-12 col-md-3 adjust-col-padding col-space nounder center"></div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <a class="btn btn-success text-white" onclick="popupadd()" data-toggle="modal" style="min-width: auto; cursor: pointer;" onclick="cleardata()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102149") %></a>
                                                    </HeaderTemplate>
                                                    <HeaderStyle Width="20%"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataFormatString="sFinger" HeaderText="sFinger" Visible="False"></asp:BoundField>
                                                <asp:BoundField DataField="sEmp" HeaderText="sEmp" Visible="False" />
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


                            <div id="modalpopproduct" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
                                aria-hidden="true">
                                <div class="modal-dialog global-modal">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h1 id="headerpopup"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102149") %></h1>
                                        </div>
                                        <div class="modal-body product-add-container" id="modalpopupdata-content">
                                            <div id="productpopup">

                                                <div class="row" id="row-name">
                                                    <div class="col-xs-12">
                                                        <div class="col-xs-3">
                                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                        </div>
                                                        <div class="col-xs-5 leftText">
                                                            <asp:TextBox ID="txt" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer" style="display: block; text-align: center;">
                                            <div class="row text-center planadd-row">
                                                <div class="col-xs-12 button-segment">
                                                    <asp:Button CssClass="btn btn-success global-btn " ID="btnSave" runat="server" Width="100px"
                                                        Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                                                    <asp:Button CssClass="btn btn-danger global-btn" ID="btnCancle" runat="server" Width="100px"
                                                        Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div id="modalpopup-data" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
            aria-hidden="true" style="margin: 0 auto; top: 25%;">
            <div class="modal-dialog global-modal">
                <div class="modal-content" style="max-width: 900px;padding:15px 25px;">
                    <div class="modal-header center" style="padding: 0px; top: 25%;">
                        <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102152") %></h4>
                    </div>
                    <div class="modal-body product-add-container" id="modalpopup-data-content">
                        <div id="productpopup">
                            <div class="row planadd-row">
                                <div class="col-xs-12 col-md-6">
                                    <p style="margin: 7px 0px 0px 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102146") %></p>
                                </div>
                                <div class="col-xs-12 col-md-6">
                                    <input type="hidden" id="nTitleid" value="0" />
                                    <input class="form-control" type="text" id="titleDescription" maxlength="100" />
                                </div>
                            </div>
                            <div class="row planadd-row">
                                <div class="col-xs-12 col-md-6">
                                    <p style="margin: 7px 0px 0px 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102147") %></p>
                                </div>
                                <div class="col-xs-12 col-md-6">
                                    <input class="form-control" type="text" id="titleDescriptionEn" maxlength="100" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer" style="display: block; text-align: center;">
                        <button type="button" id="modalpopup-data-submit" class="btn btn-primary global-btn">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %></button>
                        <button type="button" id="modalpopup-data-cancel" class="btn btn-danger global-btn"
                            data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </div>
            </div>
        </div>


    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
