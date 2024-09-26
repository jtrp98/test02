<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="leaveSetting.aspx.cs" Inherits="FingerprintPayment.leaveSetting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/select2/select2.min.css" rel="stylesheet" />
    <script src="/Scripts/select2/select2.full.min.js"></script>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <style type="text/css">
        .righttext {
            position: relative;
            text-align: right;
            white-space: nowrap;
        }

        .lefttext {
            position: relative;
            text-align: left;
            white-space: nowrap;
        }

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

        .noborder {
            border-style: none;
            text-decoration: none;
            text-shadow: none !important;
            box-shadow: inset 0px 0px 0px 0px red;
        }

        .hid {
            visibility: hidden;
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
            border-left: 10px
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

        .select2-selection {
            text-align: left;
        }

        .table-custom-sortable tbody tr td span.select2-selection
        span.select2-selection__rendered {
            padding: 5px 5px;
            color: black;
        }

        input.select2-search__field {
            font-size: 18px;
            z-index: 99999;
        }

        .select2-selection__clear {
            padding-right: 15px;
        }

        .has-error .select2-selection {
            border-color: rgb(185, 74, 72) !important;
        }

        .select2-results {
            font-size: 20px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(function () {
            $("#btnSetting_4").click(function () {
                $("body").mLoading();
                PageMethods.getLeaveData(function (e) {
                    $("#drpLeaveType").val(e.nType);
                    $("#txtLeaveMax").val(e.nLimit);
                    $("#modal_Setting").modal();
                    $("body").mLoading('hide');
                });
            });

            $(".js-example-basic-single").select2();

            $("#btnSaveSetting").click(function () {
                let data = {
                    nLimit: $("#txtLeaveMax").val(),
                    nType: $("#drpLeaveType").val(),
                };
                $("#modal_Setting").modal("hide");
                $("body").mLoading();
                PageMethods.Update(data, function (e) {
                    $("#nLimit").html(e.nLimit);
                    $("#nType").html(e.nType == "1" ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %>" : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>");
                    $("body").mLoading('hide');
                });
            });
        });

        function getroomdata(idlv3) {
            $("body").mLoading();
            var input = idlv3;

            var fields = input.split('~');
            var departid = fields[1];

            $("#<%=assistOne.ClientID%>").val('-1').trigger('change');
            $("#<%=assistTwo.ClientID%>").val('-2').trigger('change');
            $("#<%=departid.ClientID%>").val(departid);

            $.get("/App_Logic/classmemJSON.ashx?mode=department&id=" + departid + "", function (Result) {
                $("body").mLoading('hide');
                $("#modalpopproduct").modal();
                $.each(Result, function (index) {
                    var head1 = Result[index].head1;
                    var one = Result[index].one;
                    var two = Result[index].two;
                    var headid = Result[index].headid;
                    var oneid = Result[index].oneid;
                    var twoid = Result[index].twoid;

                    if (oneid > 0)
                        $("#<%=assistOne.ClientID%>").val(oneid).trigger('change');
                    if (twoid > 0)
                        $("#<%=assistTwo.ClientID%>").val(twoid).trigger('change');

                    $("#head1").html(Result[index].head1);
                });
            });
        }
        function reset1() {
            $("#<%=assistOne.ClientID%>").val('-1');
            $("#<%=assistTwo.ClientID%>").val('-2');
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <%--  <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTEmployees" ServicePath="AutoCompleteService.asmx"
        EnableCaching="true" FirstRowSelected="true" CompletionListCssClass="completionList"
        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>--%>
    <div class="full-card box-content employeeslist-container">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-md-12 center">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301045") %></label>
            </div>
        </div>
        <div class="row form-group">
            <div class="col-sm-12 lefttext">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102190") %></label>
            </div>
        </div>

        <div class="row form-group">
            <div class="col-md-12 col-sm-12">
                <div class="col-lg-6 col-md-6 col-sm-6 lefttext">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301048") %></label>
                </div>
                <div class="col-lg-1 col-md-1 col-sm-1">
                    <asp:Label ID="requestPeople" runat="server" CssClass='form-control noborder righttext' Style="width: 100%;"></asp:Label>
                </div>
                <div class="col-lg-1 col-md-1 col-sm-1">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></label>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <asp:Button ID="Button1" class="btn btn-warning global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00149") %>" />
                </div>
            </div>
        </div>

        <div class="row form-group">
            <div class="col-md-12 col-sm-12">
                <div class="col-lg-8 col-md-8 col-sm-8 lefttext">
                    <label>2. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00357") %></label>
                </div>
            </div>
        </div>

        <div class="row form-group">
            <div class="col-md-12 col-sm-12">
                <div class="col-lg-8 col-md-8 col-sm-8 lefttext">
                    <label>3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01150") %></label>
                </div>

                <div class="col-lg-4 col-md-4 col-sm-4">
                    <asp:Button ID="Button2" class="btn btn-warning global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301052") %>" />
                </div>
            </div>
        </div>
        <div class="row form-group">
            <div class="col-md-12 col-sm-12">
                <div class="col-lg-8 col-md-8 col-sm-8 lefttext">
                    <% 
                        int? nLimit = f_data.nLimit ?? 0;
                        string nType = (f_data.nType ?? 1) == 1 ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %>" : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>";
                    %>
                    <label>4. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132342") %> <span id="nLimit"><%= nLimit %></span>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301054") %> 1 <span id="nType"><%= nType %></span></label>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <div id="btnSetting_4" class="btn btn-warning global-btn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00148") %></div>
                </div>
            </div>
        </div>

        <div class="row form-group">
            <div class="col-md-12 col-sm-12">
                <div class="col-lg-8 col-md-8 col-sm-8 lefttext">
                    <label>5. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01151") %></label>
                </div>
            </div>
        </div>

        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7" BackColor="#337AB7" />
                        <Columns>
                            <asp:BoundField DataField="number" HeaderStyle-Width="100px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="7%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="departmentName" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102170") %>">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="departmentHead" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102195") %>">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="approveOne" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102196") %> 2">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="approveTwo" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102196") %> 3">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderStyle-Width="50px" HeaderText="">
                                <ItemTemplate>
                                    <a href="#" onclick='<%# "getroomdata(\"" + Eval("toWebservice") + "\");" %>' class="btn btn-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132339") %></a>
                                </ItemTemplate>
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="headerCell" />
                        <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="itemCell" />
                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="row form-group">
            <div class="col-sm-12 text-center hid">
                <label>hidden</label>
            </div>
        </div>
    </div>

    <div id="modalpopproduct" class="modal fade alertBoxInfo" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 id="headerpopup"></h1>
                </div>
                <div class="modal-body product-add-container" id="modalpopupdata-content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="col-xs-3">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102196") %> 1</label>
                            </div>
                            <div class="col-xs-7">
                                <span id="head1"></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="col-xs-3">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102196") %> 2</label>
                            </div>
                            <div class="col-xs-7">
                                <input type="hidden" runat="server" id="head" />
                                <input type="hidden" runat="server" id="help1" />

                                <asp:DropDownList ID="assistOne" runat="server" CssClass='js-example-basic-single' Style="width: 100%;">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132340") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-12">
                            <div class="col-xs-3">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102196") %> 3</label>
                            </div>
                            <div class="col-xs-7">
                                <asp:DropDownList ID="assistTwo" runat="server" CssClass='js-example-basic-single' Style="width: 100%;">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132341") %>" Value="-2" class="grey hidden"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row hidden">
                        <div class="col-xs-4">
                            <label class="pull-right">hidden</label>
                        </div>
                        <div class="col-xs-8 ">
                            <asp:TextBox ID="departid" runat="server" class="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer text-center planadd-row">
                    <asp:Button CssClass="btn btn-primary global-btn" ID="btnSave" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" />
                    <button type="button" class="btn btn-danger global-btn" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    <div class="btn btn-warning" id="resetbtn" onclick="reset1()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106049") %></div>
                </div>
            </div>
        </div>
    </div>

    <div id="modal_Setting" class="modal fade alertBoxInfo" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog global-modal">
            <div class="modal-content">
                <div class="modal-header">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132343") %></h1>
                </div>
                <div class="modal-body product-add-container" id="modal_Setting-content">
                    <div>
                        <div class="form-group row">
                            <div class="col-xs-12">
                                <div class="col-xs-5">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132342") %></label>
                                </div>
                                <div class="col-xs-7">
                                    <input type="text" value="0" id="txtLeaveMax" class="form-control" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-xs-12">
                                <div class="col-xs-5">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132313") %></label>
                                </div>
                                <div class="col-xs-7">
                                    <select class="form-control" id="drpLeaveType">
                                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405023") %></option>
                                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105012") %></option>
                                    </select>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row text-center planadd-row">
                        <div class="col-xs-12 button-segment">
                            <div class="btn btn-primary global-btn" id="btnSaveSetting"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %></div>
                            <div class="btn btn-danger global-btn" id="btnCancleSetting" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


