<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="employees-list.aspx.cs" Inherits="FingerprintPayment.Employees.employees_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="/javascript/bootstrap-show-password/bootstrap-show-password.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('#password').password().password('focus').on('show.bs.password', function (e) {
                $('#eventLog').text('On show event');
            }).on('hide.bs.password', function (e) {
                $('#eventLog').text('On hide event');
            });

            $('#methods').click(function () {
                $('#password').password('toggle');
                $('#password').css('toggle');
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

            $('#ctl00_MainContent_txtSearch').autocomplete({
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
                    $("#ctl00_MainContent_txtSearch").val(ui.item.label);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });

            $('#ctl00_MainContent_txtSearch').val(getUrlParameter("name"));
            $("#type").val(getUrlParameter("type"));

            $("input[id*=btnsearch]").click(function () {
                window.location.href = "employees-list.aspx?name=" + $('#ctl00_MainContent_txtSearch').val() + "&type=" + $("select[id*=ddlType] option:selected").val();
            });

            $("#formpopup").validate({
                rules: {
                    newpassword: "required",
                    oldpassword: "required",
                    password_again: {
                        equalTo: "#newpassword"
                    }
                },
                messages: {
                    password_again: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121044") %>",
                    newpassword: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    oldpassword: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                },
                tooltip_options: {
                    password_again: { placement: 'right', trigger: 'focus' },
                    newpassword: { placement: 'right', trigger: 'focus' },
                    oldpassword: { placement: 'right', trigger: 'focus' }
                },
                submitHandler: function (e) {
                }
            });

            $("#modalpopup-data-cancel").click(function () {
                $("#modalpopup-data").modal("hide");
            });

            $("#modalpopup-data-submit").click(function (e) {
                e.preventDefault();
                if ($(this).html() == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101229") %>") {
                    $("#changepassword").show();
                    $("#showpassword").hide();
                    $("#modalpopup-data-submit").prop("type", "submit")
                    $("#modalpopup-data-submit").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>");
                    $(this).addClass("btn-success");
                    $(this).removeClass("btn-primary");
                } else {
                    if ($("#formpopup").valid() === true) {
                        var data = {
                            "oldpass": $("#oldpassword").val(), "newpass": $("#newpassword").val(),
                            "user_id": $("#user_id").val(),
                        }
                        PageMethods.changepassword(data,
                            function (response) {
                                if (response === "Success") {
                                    $("#modalpopup-data").modal("hide");
                                }
                                else {
                                    $(".message_response").show().fadeOut(1500);
                                }
                            },
                            function (response) {

                            });
                    }
                }
            })

        });

        function showModalConfirm_Delete(id) {
            $('#modalconfirm-delete #modal-delete-header').text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>");
            $('#modalconfirm-delete #modalconfirm-delete-content').html("<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>");
            document.addEventListener("keydown", closeModalOnKey);
            $('#modalconfirm-delete').modal('show');

            $("#modalconfirm-delete-confirm").click(function () {
                PageMethods.delete(id,
                    function () {
                        window.location.href = "/employees/employees-list.aspx";
                    },
                    function () {
                    })
            });
        }

        function changeFinger() {
            $.ajax("/Api/change/?userid=" + $("#ctl00_MainContent_hdfsid").val() + "&type=1", function (Result) {
            }).done(function (Result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111048") %> " + Result);
                $("#modalpopupdatamac .modal-footer").addClass("hidden");
            });
        }
        function ShowPopUP(userid, name) {
            $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>" +
                "เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
            $("#ctl00_MainContent_hdfsid").val(userid);
            $("#modalpopupdatamac .modal-footer").removeClass("hidden")
        }


        function ShowPopUPRegister(userid, name) {
            $.get("/App_Logic/dataJSon.ashx?mode=getpassword&userid=" + userid + "&type=1", "", function (result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121045") %> " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121046") %> <br/>" +
                    " <h1> " + result[0].password);
                $("#modalpopupdatamac .modal-footer").addClass("hidden")
            })
        }

        function Showpass(user_id) {
            $('.spinner').show();
            PageMethods.getpassword(user_id,
                function (response) {
                    $('.spinner').fadeOut();
                    $("#changepassword").hide();
                    $("#showpassword").show();
                    $("#changepassword input").val("");
                    $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101226") %>");
                    $("#modalpopup-data").modal("show");
                    $("#username").val(response.username);
                    $("#password").val(response.password);
                    $("#modalpopup-data-submit").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101229") %>");
                    $("#modalpopup-data-submit").prop("type", "buuton");
                    $("#user_id").val(user_id);
                    $("#modalpopup-data-submit").removeClass("btn-success");
                    $("#modalpopup-data-submit").addClass("btn-primary");
                    if ($('#password').css("display") === "none") {
                        $('#password').password('toggle');
                    }
                },
                function (response) {
                    console.log(response);
                })
        }

    </script>
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
            color: yellow;
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .listItem {
            color: blue;
            background-color: White;
        }

        .hid {
            visibility: hidden;
        }

        .width10 {
            margin: 0 auto;
            width: 10%;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div class="full-card box-content employeeslist-container">
        <div class="row form-group">
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102162") %></label>
                </div>
                <div class="col-lg-8 col-md-7 col-sm-8">
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="width100 form-control">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value=""></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %>" Value="1"></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102166") %>" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                </div>
                <div class="col-lg-8 col-md-7 col-sm-8">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass='form-control' Style="width: 100%;" />
                </div>
            </div>
        </div>
        <div class="row form-group">
            <div class="col-sm-12 text-center">
                <asp:Button runat="server" type="button" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" class="btn btn-primary global-btn" ID="btnsearch" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
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
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                <HeaderStyle Width="10%" CssClass="center" HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %>">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <center>
                                        <span title="<%# Eval("timetype") %>" data-toggle="tooltip">
                                            <%# ((string)Eval("memberType")).Length >10?((string)Eval("memberType")).Substring(0,10):((string)Eval("memberType")) %>
                                        </span>
                                        <%-- <button onclick="ShowPopUP(<%# Eval("sEmp") %>,'<%# Eval("sName").ToString() +" "+ Eval("sLastName").ToString() %>'); return false;"
                                            class="btn btn-permission <%# (bool)Eval("fingerstatus") ?"":"hidden" %>" data-toggle="modal" data-target="#modalpopupdatamac">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111044") %></button>
                                        <button type="button" onclick="ShowPopUPRegister(<%# Eval("sEmp") %>,'<%# Eval("sName").ToString() +" "+ Eval("sLastName").ToString() %>'); return false;"
                                            class="btn btn-permission <%# (bool)Eval("fingerstatus") ?"hidden":"" %>" data-toggle="modal" data-target="#modalpopupdatamac">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121047") %></button>--%>
                                    </center>
                                </ItemTemplate>
                                <HeaderStyle CssClass="centerText" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="sName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>">
                                <HeaderStyle Width="15%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="sLastName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>">
                                <HeaderStyle Width="15%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderStyle-Width="20%" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %>" DataField="phone">
                                <HeaderStyle Width="14%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="dBirth" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %>"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="11%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="fingerStatus" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" HeaderText="fingerStatus">
                                <HeaderStyle></HeaderStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="fingerstatus" ItemStyle-CssClass="hidden">
                                <HeaderStyle Width="0%" CssClass="hidden"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href="/employees/employees-details.aspx?id=<%# Eval("sEmp") %>" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302012") %>" target="_blank"><i class="fa fa-user"></i></a>&nbsp;
                                    <%--<asp:LinkButton ID="btnEdit" runat="server" class="fa fa-edit" data-toggle="tooltip" CommandArgument='<%# Eval("sEmp") %>' CommandName="Edit" Visible='<%# int.Parse(Session["sEmpID"].ToString()) != ((int)Eval("sEmp")) %>' />--%>
                                    <%--    <%# int.Parse(Session["sEmpID"].ToString()) != ((int)Eval("sEmp")) ? "<a href=\"/employees/employees-edit.aspx?id=" + ((int)Eval("sEmp"))  + "\" class=\"fa fa-edit\" title=\"<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>\" data-toggle=\"tooltip\"></a>" : ""%>
                                    <%# int.Parse(Session["sEmpID"].ToString()) != ((int)Eval("sEmp")) ?  "<a title=\"<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101229") %>\" style=\"cursor: pointer;\" data-toggle=\"tooltip\" onclick=\"Showpass(" + ((int)Eval("sEmp")) +")\"><i class=\"fa fa-key\"></i></a>&nbsp;":""%>
                                    <%# int.Parse(Session["sEmpID"].ToString()) != ((int)Eval("sEmp")) ? "<a href=\"#\" onclick=\"showModalConfirm_Delete('" + ((int)Eval("sEmp"))  + "')\" class=\"fa fa-remove\" title=\"<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>\" data-toggle=\"tooltip\"></a>" : ""%>--%>
                                    <%--'<%# Eval("sEmp") %>'--%>
                                    <a href="/employees/employees-edit.aspx?id=<%# ((int)Eval("sEmp")) %>" class="fa fa-edit btnpermission" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>" target="_blank" data-toggle="tooltip"></a>
                                    <a title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101229") %>" style="cursor: pointer;" data-toggle="tooltip" onclick="Showpass(<%# ((int)Eval("sEmp")) %>)"><i class="fa fa-key"></i></a>&nbsp;
                                    <a href="#" onclick="showModalConfirm_Delete('<%# ((int)Eval("sEmp")) %>')" class="fa fa-remove" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" data-toggle="tooltip"></a>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <center>
                                        <asp:LinkButton ID="btnAdd" runat="server" class="btn-sm btn-success gvbutton" Text="เพื่มข้อมูล" data-toggle="tooltip" CommandName="Add" />
                                    </center>
                                </HeaderTemplate>
                                <ItemStyle CssClass="center" />
                                <HeaderStyle></HeaderStyle>
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
    <div id="modalpopupdatamac" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true">
        <div class="modal-dialog maclist-modal" style="top: 50px;">
            <div class="modal-content">
                <div class="modal-header">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111044") %>                    <%--    <button type="button" id="modalpopupdata-close" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 style="font-size: 20px; text-align: center; font-weight: bold" class="modal-title"
                            id="modalpopupdata-header">Modal title</h4>--%>
                </div>
                <div class="modal-body" id="modalpopupdata-content">
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <label class="btn btn-primary" onclick="changeFinger();" style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111046") %></label>
                    <label class="btn btn-danger" ondblclick='$("#modalpopupdatamac").modal("hide");' onclick="" style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></label>
                </div>
            </div>
        </div>
    </div>
    <div class="spinner">
        <div class="rect1"></div>
        <div class="rect2"></div>
        <div class="rect3"></div>
        <div class="rect4"></div>
        <div class="rect5"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalpopup" runat="server">
    <div id="showpassword">
        <div class="row">
            <div class="col-xs-12">
                <div class="col-xs-3">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101227") %></label>
                </div>
                <div class="col-xs-7">
                    <input type="hidden" id="user_id" value="0" />
                    <input type="text" id="username" class="form-control" readonly="readonly" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="col-xs-3">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01511") %></label>
                </div>
                <div class="col-xs-7">
                    <input id="password" class="form-control" type="password" value="123" placeholder="password" readonly="readonly" />
                </div>
            </div>
        </div>
    </div>
    <div id="changepassword" style="display: none;">
        <div class="row">
            <div class="col-xs-12">
                <div class="col-xs-4">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111081") %></label>
                </div>
                <div class="col-xs-8">
                    <input type="password" name="oldpassword" id="oldpassword" class="form-control" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="col-xs-4">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101232") %></label>
                </div>
                <div class="col-xs-8">
                    <input id="newpassword" name="newpassword" class="form-control" type="password" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="col-xs-4">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111082") %></label>
                </div>
                <div class="col-xs-8">
                    <input id="password_again" name="password_again" class="form-control" type="password" />
                </div>
            </div>
        </div>
    </div>
    <div class="row message_response">
        <div class="col-xs-12 center">
            <label class="text-danger"></label>
        </div>
    </div>
</asp:Content>

