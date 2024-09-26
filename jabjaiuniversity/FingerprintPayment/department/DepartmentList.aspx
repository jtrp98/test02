<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="DepartmentList.aspx.cs" Inherits="FingerprintPayment.department.DepartmentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" rel="stylesheet">
    <script src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('input[id*=btnDel]').click(function () {
                showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('name')); return false;
            });

            var availableValueEmployees = [];
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=teacher",
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].sName + ' ' + objjson[index].sLastname,
                            value: objjson[index].sEmp
                        };
                        availableValueEmployees[index] = newObject;
                    });
                }
            });

            $('#department_head_name').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                source: function (request, response) {
                    results = $.ui.autocomplete.filter(availableValueEmployees, request.term);
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("#department_head_name").val(ui.item.label);
                    $("#department_head_id").val(ui.item.value);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });

            $('#department_head_name').val(getUrlParameter("name"));
            //$("#type").val(getUrlParameter("type"));
            //$("#btnsearch").click(function () {
            //    window.location.href = "employeeslist.aspx?name=" + $('#ctl00_MainContent_txtSearch2').val() + "&type=" + $("#type").val();
            //});
        });

        var td_rows;
        $(function () {

            $("#modalpopup-data-submit").click(function () {
                var data = {
                    "department_name": $("#department_name").val(), "department_id": $("#department_id").val(),
                    "department_head_id": $("#department_head_id").val()
                };
                PageMethods.updatedata(data, function (result) {
                    if ($("#modalpopup-data h1").html() === "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102174") %>") {
                        td_rows.children("td.department_name").html(data.department_name);
                        td_rows.children("td.department_head_name").html($("#department_head_name").val());
                        $("#modalpopup-data").modal("hide");
                    } else {
                        window.location.href = "/department/DepartmentList.aspx";
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

        function getdata(department_id, control) {
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102174") %>");
            PageMethods.getdata(department_id,
                function (result) {
                    console.log(result);
                    $("#modalpopup-data").modal("show");
                    $("#department_id").val(result.department_id);
                    $("#department_name").val(result.department_name);
                    $("#department_head_name").val(result.department_head_name);
                    $("#department_head_id").val(result.department_head_id);
                    td_rows = ($(control).parent().parents("tr"));
                },
                function (result) {
                    console.log(result);
                })
        }

        function popupadd() {
            clearinput();
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102173") %>");
            $("#modalpopup-data").modal("show");
        }


        function clearinput() {
            $("#department_id").val("0");
            $("#department_name").val("");
            $("#department_head_name").val("");
            $("#department_head_id").val("");
        }
    </script>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="full-card box-content employeeslist-container">
        <div class="row form-group">
            <div class="col-md-6 col-sm-12">

                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00254") %></label>
                </div>
                <div class="col-lg-8 col-md-7 col-sm-8">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass='form-control' Style="width: 100%;"></asp:TextBox>
                </div>
            </div>


        </div>
        <div class="row form-group">
            <div class="col-sm-12 text-center">
                <asp:Button ID="Button1" class="btn btn-primary global-btn"
                    runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        OnDataBound="CustomersGridView_DataBound" ShowHeaderWhenEmpty="true"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />

                        <PagerTemplate>

                            <table width="100%" class="tab">
                                <tr>
                                    <td style="width: 25%">

                                        <asp:Label ID="Label1" BorderColor="#337AB7"
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
                            <asp:BoundField DataField="number" HeaderStyle-Width="100px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="departmentName" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102170") %>" ItemStyle-CssClass="department_name">
                                <HeaderStyle Width="40%"></HeaderStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="departmentHeadName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102171") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext department_head_name">
                                <HeaderStyle Width="30%"></HeaderStyle>
                            </asp:BoundField>

                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12">
                                        <div class="col-lg-4 col-md-4 col-sm-4 adjust-col-padding col-space nounder">
                                            <a class="glyphicon glyphicon-edit" style="font-size: 70%; cursor: pointer;" id="btnEdit<%# Eval("departmentId") %>" onclick="getdata(<%# Eval("departmentId") %>,this)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>"></a>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>">
                                                <a href="/department/DepartmentList-del.aspx?id=<%# Eval("departmentId") %>" id="Del<%# Eval("departmentId") %>" class="glyphicon glyphicon-remove" style="font-size: 70%; color: red;"></a>
                                            </div>
                                        </div>
                                </ItemTemplate>

                                <HeaderTemplate>
                                    <a class="btn btn-success" style="cursor: pointer;" onclick="popupadd()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102172") %></a>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalpopup" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <div class="col-xs-3">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102170") %></label>
            </div>
            <div class="col-xs-7">
                <input type="hidden" id="department_id" value="0" />
                <input type="text" id="department_name" value="0" class="form-control" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <div class="col-xs-3">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102171") %></label>
            </div>
            <div class="col-xs-7">
                <input type="text" id="department_head_name" value="0" class="form-control" />
                <input type="hidden" id="department_head_id" value="0" class="form-control" />
            </div>
        </div>
    </div>
</asp:Content>
