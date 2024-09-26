<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="EmpType.aspx.cs" Inherits="FingerprintPayment.Employees.EmpType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" rel="stylesheet" />
    <script src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js" type="text/javascript"></script>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@9"></script>
      <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />
    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>

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

        .swal2-popup {
            font-size: 1.8rem !important;
        }

        .swal2-confirm, .swal2-cancel {
            font-size: 2rem !important;
        }
    </style>
    <script type="text/javascript">
        var td_rows;
        $(function () {

            $("#modalpopup-data-submit").click(function () {
                var data = {
                    "id": $("#typeid").val(), "title": $("#titleDescription").val()
                };
                PageMethods.updatedata(data, function (result) {
                    if ($("#modalpopup-data h1").html() === "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121098") %>") {
                        td_rows.html(data.titleDescription);
                        $("#modalpopup-data").modal("hide");
                    } else {
                        window.location.href = "/Employees/EmpType.aspx";
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

            $('#ctl00_MainContent_dgd').DataTable({         
                paging: false,
                searching: false,
                info: false,
            });
        })

        function getdata(id, control) {
            console.log(id);
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102157") %>");
            PageMethods.getdata(id,
                function (result) {
                    console.log(result);
                    $("#modalpopup-data").modal("show");
                    $("#typeid").val(result.id);
                    $("#titleDescription").val(result.title);
                    td_rows = ($(control).parent().parents("tr")).children("td.description");
                },
                function (result) {
                    console.log(result);
                })
        }

        function popupadd() {
            clearinput();
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102156") %>");
            $("#modalpopup-data").modal("show");
        }

        function statuschange(control) {
            var titleid = $(control).attr("typeid");
            console.log(titleid);
            PageMethods.StatusChange(titleid,
                function (reuslt) {
                    td_rows = ($(control).parent().parents("tr")).children("td.status");
                    if (reuslt === false) {
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
                }
            );

        }

        function clearinput() {
            $("#typeid").val("0");
            $("#titleDescription").val("");
        }

        function ConfirmDel(id) {

            Swal.fire({
                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102158") %>',
                //text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                //cancelButtonColor: '#d33',
                confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>',
                cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>'
            }).then((result) => {
                if (result.value) {

                    PageMethods.deletedata(id,
                        function (result) {
                            if (result == "success") {
                                window.location.href = "/Employees/EmpType.aspx";
                            }
                        },
                        function (result) {
                            console.log(result);
                        })

                    return true;
                }
            });
           <%-- var r = confirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102158") %> ?");
            if (r == true) {
                $("#<%= hfDelWork.ClientID %>").val($t.data('id'));
                $("#<%= ButtonDelWork.ClientID %>").trigger('click');
                //return true;
            }--%>

            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="full-card box-content employeeslist-container">
        <div class="row form-group">
            <div class="col-md-6 col-sm-12">

                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102155") %></label>
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
                        GridLines="None" AllowPaging="false" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        OnDataBound="CustomersGridView_DataBound" ShowHeaderWhenEmpty="true"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table-x table table-bordered table-hover dataTable">
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
                            <asp:BoundField DataField="index" HeaderStyle-Width="100px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="title" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %>" ItemStyle-CssClass="description">
                                <HeaderStyle Width="50%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="hid" ItemStyle-CssClass="hid">
                                <HeaderStyle Width="1%"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102148") %>" HeaderStyle-CssClass="status centertext" ItemStyle-CssClass="status centertext">
                                <HeaderStyle Width="20%"></HeaderStyle>
                                <ItemTemplate>
                                    <%# (bool)Eval("IsActive") ?"<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102150") %>":"<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102151") %>" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="col-xs-12 col-md-3 adjust-col-padding col-space nounder center"></div>
                                        <div class="col-xs-12 col-md-2 adjust-col-padding col-space nounder center">
                                            <a onclick="statuschange($(this))" typeid="<%# Eval("id") %>" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103164") %>" id="btnStatus<%# Eval("id") %>" class='<%# (bool)Eval("IsActive") ?"fa fa-toggle-on":"fa fa-toggle-off" %>' style="font-size: 70%; cursor: pointer;"></a>
                                        </div>
                                        <div class="col-xs-12 col-md-2 adjust-col-padding col-space nounder center">
                                            <a onclick="getdata('<%# Eval("id") %>',$(this));" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>" id="btnEdit<%# Eval("id") %>" class="glyphicon glyphicon-edit" style="font-size: 70%; cursor: pointer;" data-toggle="modal" data-target="#modalpopupdata"></a>
                                        </div>
                                        <div class="col-xs-12 col-md-2 adjust-col-padding col-space nounder">
                                            <a onclick="return ConfirmDel('<%# Eval("id") %>');" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>" id="btnDel<%# Eval("id") %>" class="glyphicon glyphicon-remove  <%# (bool)Eval("IsDel") ?"":"hidden" %> "  style="font-size: 70%; color: red;" ></a>
                                        </div>
                                        <div class="col-xs-12 col-md-3 adjust-col-padding col-space nounder center"></div>
                                    </div>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <a class="btn btn-success" onclick="popupadd()" data-toggle="modal" style="min-width: auto; cursor: pointer;" onclick="cleardata()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102156") %></a>
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


        <div id="modalpopproduct" class="modal fade alertBoxInfo" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog global-modal">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 id="headerpopup"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102156") %></h1>
                    </div>
                    <div class="modal-body product-add-container" id="modalpopupdata-content">
                        <div id="productpopup">

                            <div class="row" id="row-name">
                                <div class="col-xs-12">
                                    <div class="col-xs-3">
                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %></label>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalpopup" runat="server">
    <div id="productpopup">
        <div class="row planadd-row">
            <div class="col-xs-12 col-md-4">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %></label>
            </div>
            <div class="col-xs-12 col-md-8">
                <input type="hidden" id="typeid" value="0" />
                <input class="form-control" type="text" id="titleDescription" />
            </div>
        </div>
    </div>
</asp:Content>
