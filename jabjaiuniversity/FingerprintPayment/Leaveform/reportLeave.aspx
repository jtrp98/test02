<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="reportLeave.aspx.cs" Inherits="FingerprintPayment.reportLeave" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>

    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript" src="/scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js"></script>
    <script src="/Content/Material/assets/js/plugins/moment-with-locales.js"></script>
    <script src="/Content/Material/assets/js/plugins/bootstrap-datetimepicker.th.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {

                $('.datepicker').datetimepicker({
                    keepOpen: false,
                    debug: false,
                    format: 'DD/MM/YYYY-BE',
                    locale: 'th',
                    icons: {
                        time: "fa fa-clock-o",
                        date: "fa fa-calendar",
                        up: "fa fa-chevron-up",
                        down: "fa fa-chevron-down",
                        previous: 'fa fa-chevron-left',
                        next: 'fa fa-chevron-right',
                        today: 'fa fa-screenshot',
                        clear: 'fa fa-trash',
                        close: 'fa fa-remove'
                    }
                });
            });
        });

        function relink() {

            var name = document.getElementsByClassName("linkname");
            var job = document.getElementsByClassName("linkjob");
            var from = document.getElementsByClassName("linkfrom");
            var to = document.getElementsByClassName("linkto");
            var year = document.getElementsByClassName("linkyear");
            var status = document.getElementsByClassName("linkstatus");

            window.location.href = "reportLeave.aspx?job=" + job[0].value + "&year=" + year[0].value + "&end=" + to[0].value + "&start=" + from[0].value + "&name=" + name[0].value + "&status=" + status[0].value;
        }

        var availableValuestudent = [];
        function start() {
            var availableTags = [];

            $.get("/PreRegister/preRegisterStudentList.ashx", function (Result) {
                $.each(Result, function (index) {
                    availableTags.push(Result[index].fullName);
                });
            });

            //$('#tb tbody').on('click', 'tr', function () {
            //    var data = table.row(this).data();
            //    // alert('You clicked on ' + data[2] + '\'s row');
            //    console.log(data);
            //    $('#txtGradeDetailId').html(data.nGradeDetailId);
            //    $('#txtSubject').html(data.sPlaneName);
            //    $('#txtGradeCal').html(data.gradeCal);
            //    $('#inputGradeEdits').val(data.gradeSet);
            //    if (data.gradeSet !== "") document.getElementById("gradeNew").value = data.gradeSet;
            //    else document.getElementById("gradeNew").value = -1;
            //});

        }


        function getlistStd() {
            $.ajax({
                url: "/App_Logic/autoCompleteName.ashx?mode=GetStdList",
                dataType: "json",
                success: function (objjson) {
                    //console.log(objjson);
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].Name,
                            value: objjson[index].StdId,
                            code: objjson[index].Code
                        };
                        availableValuestudent.push(newObject);
                    });
                }
            });
        }


        function getlistemp() {
            availableValuestudent = [];
            $.ajax({
                url: "/App_Logic/autoCompleteName.ashx?mode=GetEmpList",
                dataType: "json",
                success: function (objjson) {
                    //console.log(objjson);
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].Name,
                            value: objjson[index].EmpId,
                            code: objjson[index].Phone
                        };
                        availableValuestudent.push(newObject);
                    });
                }
            });
        }

        function lightwell(request, response) {
            function hasMatch(s) {
                if (s === null) s = "";
                return s.toLowerCase().indexOf(request.term.toLowerCase()) !== -1;
            }
            var i, l, obj, matches = [];

            if (request.term === "") {
                response([]);
                return;
            }

            for (i = 0, l = availableValuestudent.length; i < l; i++) {
                obj = availableValuestudent[i];
                if (hasMatch(obj.label) || hasMatch(obj.code)) {
                    matches.push(obj);
                }
            }
            response(matches.slice(0, 10));
        }

        $(function () {
            getlistStd();
            getlistemp();

            $('#ctl00_MainContent_ddlnamedrop').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                //source: function (request, response) {
                //    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
                //    response(results.slice(0, 10));
                //},
                source: lightwell,
                select: function (event, ui) {
                    event.preventDefault();
                    $("#ctl00_MainContent_ddlnamedrop").val(ui.item.label);
                    //$("#txtId").val(ui.item.value);
                    //$("#txtCode").val(ui.item.code);
                    $code = ui.item.code;
                },
                focus: function (event, ui) {
                    event.preventDefault();
                    //$("#txtId").val("");
                    //$("#txtCode").val("");
                }
            });
        })

        $(window).on('load', function () {
            start();
        });

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


        .select2-selection__rendered {
            line-height: 40px !important;
        }

        .select2-container .select2-selection--single {
            height: 40px !important;
        }

        .select2-selection__arrow {
            height: 40px !important;
        }


        .cover {
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .bigfont {
            font-size: 200%;
        }

        .smol {
            font-size: 85%;
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

        .width10 {
            margin: 0 auto;
            width: 10%;
        }

        .centertext {
            text-align: center;
        }

        .righttext {
            text-align: right;
        }

        .lefttext {
            text-align: left;
        }

        .bord {
            border-left: 1px solid #ffffff;
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

        .button-custom {
            font-size: 26px;
            padding-left: 30px;
            padding-right: 30px;
            width: 100%;
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

        .width100 {
            margin: 0 auto;
            width: 100%;
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
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="full-card box-content employeeslist-container">

        <div class="form-group row student">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-5 col-md-5 col-sm-5 col-xs-5  righttext">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102229") %></label>
                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-control linkyear">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-4 col-sm-4  righttext">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102230") %>
                    </label>
                </div>
                <div class="col-lg-7 col-md-7 col-sm-7">
                    <asp:DropDownList ID="ddlstatus" runat="server" CssClass="width100 form-control linkstatus">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="all"></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102232") %>" Value="reject"></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102233") %>" Value="accept"></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102231") %>" Value="wait"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

        </div>

        <div class="row form-group">
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-5 col-md-5 col-sm-5  righttext">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102234") %>
                    </label>
                </div>
                <div class="col-lg-7 col-md-7 col-sm-7">
                    <asp:TextBox ID="startDay" runat="server" ClientIDMode="static" CssClass="form-control linkfrom datepicker" Width="100%" />
                </div>
            </div>
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-4 col-sm-4  righttext">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105040") %>
                    </label>
                </div>
                <div class="col-lg-7 col-md-7 col-sm-7">
                    <asp:TextBox ID="endDay" runat="server" ClientIDMode="static" CssClass="form-control linkto datepicker" Width="100%" />
                </div>
            </div>

        </div>

        <div class="row form-group">
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-5 col-md-5 col-sm-5  righttext">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105059") %></label>
                </div>
                <div class="col-lg-7 col-md-7 col-sm-7">
                    <asp:DropDownList ID="ddlJob" runat="server" CssClass="width100 form-control linkjob">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="all"></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02309") %>" Value="teacher"></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %>" Value="student"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-4 col-sm-4  righttext">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                </div>
                <div class="col-lg-7 col-md-7 col-sm-7">
                    <asp:TextBox ID="ddlnamedrop" onchange="" runat="server" Width="100%" CssClass="linkname js-example-basic-single form-control" name="classchoice">        
                    </asp:TextBox>


                </div>
            </div>

        </div>




        <div class="row form-group">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                <div class="btn btn-primary" style="width: 100%; height: 48px; padding-bottom: 2px;" onclick="relink()">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></label>
                </div>
            </div>

        </div>
        <div class="row form-group">
            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 text-center pull-right">
                <asp:Button ID="btnExport" class="btn btn-success" Width="100%" runat="server" Text="Export To Excel" />
            </div>
        </div>


        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />



                        <Columns>
                            <asp:BoundField DataField="number" HeaderStyle-Width="100px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext smol">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="letterDate" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" HeaderStyle-CssClass="centertext smol">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="letterType" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105057") %>" HeaderStyle-CssClass="centertext smol" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="writerName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105058") %>" HeaderStyle-CssClass="centertext smol" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="writerJob" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105059") %>" HeaderStyle-CssClass="centertext smol" ItemStyle-CssClass="smol centertext">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="leaveDay" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105060") %>" HeaderStyle-CssClass="centertext smol" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="letterStatus" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102230") %>" HeaderStyle-CssClass="centertext smol" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="leaveSick" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105061") %>" HeaderStyle-CssClass="centertext smol" ItemStyle-CssClass="centertext ">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="leaveBusiness" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105062") %>" HeaderStyle-CssClass="centertext smol " ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="leaveSon" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105063") %>" HeaderStyle-CssClass="centertext smol " ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="6%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="leaveOther" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105064") %>" HeaderStyle-CssClass="centertext smol " ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="7%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="leaveTotal" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105065") %>" HeaderStyle-CssClass="centertext smol " ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="6%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderStyle-Width="10px" HeaderText="" ItemStyle-CssClass="nounder">
                                <ItemTemplate>
                                    <div title="print">
                                        <a href="/Leaveform/leavePrint.aspx?id=<%# Eval("letterId") %>" target="_blank" class="fa fa-print pull-right " style="font-size: 70%;"></a>
                                    </div>

                                </ItemTemplate>

                                <HeaderStyle Width="1%"></HeaderStyle>

                            </asp:TemplateField>



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


        <div class="row mini--space__top hidden">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />



                        <Columns>
                            <asp:BoundField DataField="number" HeaderStyle-Width="100px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="letterDate" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="letterType" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105057") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="writerName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105058") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="writerJob" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105059") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="leaveDay" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105060") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="letterStatus" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102230") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="leaveSick" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105061") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext ">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="leaveBusiness" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105062") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="leaveSon" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105063") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="6%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="leaveOther" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105064") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="7%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="leaveTotal" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105065") %>" HeaderStyle-CssClass="centertext" ItemStyle-CssClass="centertext">
                                <HeaderStyle Width="6%"></HeaderStyle>
                            </asp:BoundField>


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


