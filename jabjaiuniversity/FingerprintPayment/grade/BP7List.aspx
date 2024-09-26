<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="BP7List.aspx.cs" Inherits="FingerprintPayment.grade.BP7List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <script type="text/javascript" language="javascript">

        var availableValueplane = [];

        $(document).ready(function () {


            $('input[id*=btnSearch]').click(function () {
                var load = document.getElementsByClassName("load");
                load[0].classList.remove('hidden');
                var mode = document.getElementsByClassName("mode");

                var param1var = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                var param2var = $('select[id*=ddlsublevel2] option:selected').val();
                var param3var = $('select[id*=DropDownList1] option:selected').val();
                if (param2var == undefined)
                    param2var = "";
                var param4var = $('select[id*=DropDownList2] option:selected').val();
                if (param4var == undefined)
                    param4var = "";
                window.location.href = "BP7List.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&year=" + param3var + "&term=" + param4var + "&mode=" + mode[0].value;


            });


        });

        function bootbox2() {
            bootbox.alert({
                message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206412") %></h3>',
                backdrop: true
            });
        }

        function changeFinger() {
            $.ajax("/Api/change/?userid=" + $("#ctl00_MainContent_hdfsid").val() + "&type=0", function (Result) {
            }).done(function (Result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111048") %> " + Result);
                $("#modalpopupdatamac .modal-footer").addClass("hidden");
            });
        }

        function ddlyear() {
            var ddl1 = document.getElementsByClassName("ddl1");
            var select = document.getElementById('DD1');

            for (i = -1; i <= 5; i++) {
                ddl1[1].remove(0);
            }

            $.get("/App_Logic/ddlterm.ashx?year=" + ddl1[0].options[ddl1[0].selectedIndex].value, function (Result) {
                $.each(Result, function (index) {

                    // Create an Option object       
                    var opt = document.createElement("option");

                    // Assign text and value to Option object
                    opt.text = Result[index].value;
                    opt.value = Result[index].value;

                    if (getUrlParameter("term") != "" && getUrlParameter("term") == Result[index].value) {
                        opt.selected = "selected";
                    }

                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=DropDownList2.ClientID%>').options.add(opt);



                });
            });


        }

        function ddlclass() {
            var ddl2 = document.getElementsByClassName("ddl2");
            var load = document.getElementsByClassName("load");
            load[0].classList.remove('hidden');
            for (i = -1; i <= 20; i++) {
                ddl2[1].remove(0);
            }

            $("#<%=ddlsublevel2.ClientID%> option").remove();

            $.get("/App_Logic/ddlclassroom.ashx?idlv=" + ddl2[0].options[ddl2[0].selectedIndex].value, function (Result) {
                $.each(Result, function (index) {

                    // Create an Option object       
                    var opt = document.createElement("option");

                    // Assign text and value to Option object
                    opt.text = Result[index].name;
                    opt.value = Result[index].value;

                    if (getUrlParameter("idlv2") != "" && getUrlParameter("idlv2") == Result[index].value) {
                        opt.selected = "selected";
                    }

                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);


                });

                load[0].classList.add('hidden');
            });


        }

        function ShowPopUP(userid, name) {
            $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>" +
                "เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
            $("#ctl00_MainContent_hdfsid").val(userid);
            $("#modalpopupdatamac .modal-footer").removeClass("hidden")
        }

        function start() {


            var d2 = document.getElementsByClassName("d2");
            ddlyear();
            ddlclass();




            <%--setTimeout(function () {
                document.getElementById('<%=DropDownList2.ClientID%>').value = d1[0].value;
                document.getElementById('<%=ddlsublevel2.ClientID%>').value = d2[0].value;
            }, 900);--%>
        }
        window.onload = start;
    </script>

    <div class="full-card box-content userlist-container">
        <div id="loading" class="hidden load"></div>
        <asp:HiddenField ID="hdfsid" runat="server" />

        <div class="form-group row student">
            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">
                    <label>ddl1</label>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtddl1" runat="server" CssClass="d1" Width="50%"> </asp:TextBox>
                </div>
                <div class="col-xs-2 righttext">
                    <label>ddl2</label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtddl2" runat="server" CssClass="d2" Width="50%"> </asp:TextBox>
                </div>
            </div>

            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="DropDownList1" onchange="ddlyear()" runat="server" CssClass="ddl1 form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddl1 form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group row student hidden">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    mode</label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:TextBox ID="modetxt" runat="server" CssClass="mode" Width="50%"> </asp:TextBox>
                </div>
            </div>

        </div>
        <div class="form-group row student">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlsublevel" onchange="ddlclass()" runat="server" CssClass="ddl2 form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlsublevel2" runat="server" CssClass="ddl2 form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-xs-12 button-section">
                <input type="button" id="btnSearch" class='btn btn-info search-btn' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
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
                                            CssClass="imjusttext" OnClick="backbutton_Click"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>
                                        </asp:LinkButton>
                                        <asp:DropDownList ID="PageDropDownList"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged"
                                            runat="server" />
                                        <asp:LinkButton ID="nextbutton" runat="server"
                                            CssClass="imjusttext" OnClick="nextbutton_Click"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
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
                                <HeaderStyle Width="15%" CssClass="centertext"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="sstudentid" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="sName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %>">
                                <HeaderStyle Width="25%"></HeaderStyle>
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>

                                    <div class="btn-group">
                                        <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103029") %><span class="caret"></span>
                                        </button>
                                        <ul class="dropdown-menu pull-right">

                                            <li><a target="_blank" style="font-size: 20px" href="/grade/BP7Print.aspx?year=<%# Eval("year")%>&idlv=<%# Eval("idlv")%>&idlv2=<%# Eval("idlv2")%>&term=<%# Eval("term")%>&id=<%# Eval("sID")%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206484") %></a></li>
                                            <li><a target="_blank" style="font-size: 20px" href="/grade/BP7PrintEN.aspx?year=<%# Eval("year")%>&idlv=<%# Eval("idlv")%>&idlv2=<%# Eval("idlv2")%>&term=<%# Eval("term")%>&id=<%# Eval("sID")%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206485") %></a></li>
                                            <li><a target="_blank" style="font-size: 20px" href="/grade/BP7Print2.aspx?year=<%# Eval("year")%>&idlv=<%# Eval("idlv")%>&idlv2=<%# Eval("idlv2")%>&term=<%# Eval("term")%>&id=<%# Eval("sID")%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206486") %></a></li>

                                            <li><a target="_blank" style="font-size: 20px" href="/grade/BP7Print3.aspx?year=<%# Eval("year")%>&idlv=<%# Eval("idlv")%>&idlv2=<%# Eval("idlv2")%>&term=<%# Eval("term")%>&id=<%# Eval("sID")%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206487") %></a></li>
                                            <% foreach (var i in ReportTypeList) { %>
                                             <li><a target="_blank" style="font-size: 20px" href="/grade/BP7/Print1.aspx?term=<%# Eval("nterm")%>&studentId=<%# Eval("sID")%>&formId=<%=i.Value %>"><%=i.Text %></a></li>
                                            <%} %>
                                        </ul>
                                    </div>

                                </ItemTemplate>
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="headerCell" />
                        <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="itemCell" />
                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

