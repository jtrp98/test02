<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="permissionssetting.aspx.cs" Inherits="FingerprintPayment.permissionssetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showpermission(user_id) {
            PageMethods.get_permission(user_id, function (result) {
                result = $.parseJSON(result);
                $(this).removeClass("disabled");
                $(".employeeslist-container").hide();
                var template = $('#template').html();
                var data = [];
                Mustache.parse(template);   // optional, speeds up future uses
                var rendered = Mustache.render(template, result);
                $("#target").show();
                $('#target').html(rendered);

                $("#btncancel").click(function () {
                    $(".employeeslist-container").show();
                    $("#target").hide();
                });

                $("#btnsave").click(function () {
                    $(this).addClass("disabled");
                    $selectmenu = $(".menu-control").prop("menuid", "*");
                    var menu = [];
                    $.each($selectmenu, function (index) {
                        if ($($selectmenu[index]).attr("menuid") != undefined)
                            menu.push({ menuid: $($selectmenu[index]).attr("menuid"), value: $($selectmenu[index]).val() })
                    });

                    $.post("/App_Logic/insertDataJSON.ashx?mode=menu", JSON.stringify({ user_id: user_id, menu }), function (result) {
                        $(".employeeslist-container").show();
                        $("#target").hide();
                    });
                    //console.log(menu);
                });
            }, function () {

            });
        }

        var availableValueEmployees = [];
        $(function () {
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
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true"></asp:ScriptManager>

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
            <div class="col-xs-12 col-md-12">
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
                                    <a onclick="showpermission('<%# Eval("sEmp") %>')" class="btn fa fa-check-square-o"></a>
                                    <%--'<%# Eval("sEmp") %>'--%>
                                </ItemTemplate>
                                <HeaderTemplate>
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
    <div id="target"><%--<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>--%></div>
    <script id="template" type="x-tmpl-mustache">
        <div class="full-card box-content">
            <div class="row">
                <div class="col-lg-12">
                    <span class='h2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131110") %> {{employessname}}</span>
                    <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                    {{#groupmenu}}
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingOne">
                                <h4 class="panel-title">
                                   <a role="button" data-toggle="collapse" data-parent="#accordion" 
                                        href="#groupmenu_{{groupmenuid}}" aria-expanded="true" aria-controls="{{groupmenuid}}">
                                        <span class='h2'><span class="fa arrow"></span>{{groupmenu}}</span>
                                    </a>
                                </h4>
                            </div>
                            <div id="groupmenu_{{groupmenuid}}" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                                <div class="panel-body">
                                    {{#menu}}
                                        <div class="row bottom">
                                            <div class="col-lg-4">
                                                <span class='h2'>{{menuname}}</span><br/>
                                            </div>
                                            <div class="col-lg-4">
                                                <select menuid="{{menuid}}" class="form-control h3 menu-control" style="margin-top:0px;">
                                                    {{#option}}
                                                    <option value="{{value}}" {{selected}}>{{text}}</option>
                                                    {{/option}}
                                                </select>
                                            </div>
                                        </div>
                                    {{/menu}}                        
                                </div>
                            </div>
                        </div>
                    {{/groupmenu}}
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1 col-md-12">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131109") %>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1 col-md-2">
                    1. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121072") %>      
                </div>
                <div class="col-xs-1 col-md-10">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132135") %>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1 col-md-2">
                    2. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404030") %>           
                </div>
                <div class="col-xs-1 col-md-10">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132136") %>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1 col-md-2">
                    3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>
                </div>
                <div class="col-xs-1 col-md-10">
                     <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132137") %>
                </div>
            </div>
            <br/>
            <div class="row">
                <div class="col-xs-1 col-md-4"></div>
                <div class="col-xs-10 col-md-4">
                    <input type="button" class="btn btn-success col-xs-5 col-md-5" id="btnsave" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />&nbsp;&nbsp;
                    <input type="button" class="btn btn-danger col-xs-5 col-md-5" id="btncancel" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
                </div>
                <div class="col-xs-1 col-md-4"></div>
            </div>
        </div>
    </script>
    &nbsp;
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
