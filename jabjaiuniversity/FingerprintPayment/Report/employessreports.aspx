<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="employessreports.aspx.cs" Inherits="FingerprintPayment.Report.employessreports" %>

<%@ Register Src="~/UserControls/TeacherAutocomplete.ascx" TagPrefix="uc1" TagName="TeacherAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style type="text/css">
        .dropdown.bootstrap-select {
            padding-left: 0px;
        }


        .report-wrapper .scoreBar {
            overflow: auto;
        }

        .color1 {
            background-color: #ecf9ff !important;
        }

        .color2 {
            background-color: #fcfff5 !important;
        }

        #datatable1.dataTable tbody tr:last-child td,
        #datatable1.dataTable thead tr th,
        #datatable1.dataTable thead tr td{
            border-bottom: 1px solid #000;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105002") %>           
            </p>
        </div>
    </div>

    <form id="aspnetForm" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Release" />
        <asp:HiddenField ID="hdfschoolname" runat="server" />

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

                        <div class="row student">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102162") %></label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlsublevel" runat="server" ClientIDMode="Static" class="selectpicker col-md-12" DataTextField="Text" DataValueField="Value" data-style="select-with-transition">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                            <div class="col-md-3">
                                <uc1:TeacherAutocomplete runat="server" ID="TeacherAutocomplete" />
                            </div>
                            <div class="col-md-2"></div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %> :</label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="DropDownListType" runat="server" class="selectpicker col-md-12" ClientIDMode="Static" data-style="select-with-transition">
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105043") %>" Value="1" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105044") %>" Value="2" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-center">
                                <button type="button" class="btn btn-info " onclick="Reports();">
                                    <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                                <button type="button" class="btn btn-success " onclick="Export();">
                                    <span class="material-icons">receipt_long
                                    </span>
                                    Export
                                </button>
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
                        <asp:Literal ID="ltrHeaderReport" runat="server" />

                        <div id="reporttype1" class="report-wrapper" style="display: none;">
                            <fieldset class="scoreBar">
                                <div class="htmlcontent">
                                </div>
                            </fieldset>
                        </div>

                        <div id="reporttype2" class="report-wrapper" style="display: none;">
                            <fieldset class="scoreBar">
                                <div class="htmlcontent">
                                </div>
                            </fieldset>
                        </div>

                    </div>

                </div>
            </div>
        </div>

    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.rowsGroup.v2.0.0.js"></script>

    <script type='text/javascript' src="//cdnjs.cloudflare.com/ajax/libs/underscore.js/1.11.0/underscore-min.js"></script>

    <%--<script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>--%>

    <script type="text/javascript">
        //var availableValuestudent = [];
        //var availableValueEmployees = [];
        var searchData = null;
        var $table = null;
        //function Reports() {
        //    $("body").mLoading();
        //    $("#example").html();
        //    var thead = $("#example thead");
        //    var tbody = $("#example tbody");
        //    thead.html("");
        //    tbody.html("");
        //    var search = "";
        //    $("#divprint").html("");
        //    var dt = new Date();

        //    var sort = "";
        //    sort += "type=" + $('select[id*=ddlsublevel]').val();
        //    //sort += "&level2=" + $('select[id*=ddlSubLV2]').val();
        //    sort += "&employessname=" + $('#txtname').val();
        //    $.post("/app_Logic/Report/employessreports.ashx?" + sort, "", function (_res) {
        //        var result = JSON.parse(_res);
        //        if (result.length === 0) {
        //            $("#divprint").html("<center><h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105021") %></h1></center>");
        //        }
        //        else {
        //            var headerreport = '<tr><th colspan="6" style="text-align: center; font-size: 26px; font-weight: bolder;border:0px;" id="school">' + $("input[id*=hdfschoolname]").val();
        //            headerreport += '<tr><th colspan="6" style="text-align: center; font-size: 22px; font-weight: bolder;border:0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105004") %>';
        //            headerreport += '<tr><th colspan="6" style="text-align: right; font-size: 22px; font-weight: bolder;border:0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603092") %>&nbsp;' + dt.toLocaleDateString();
        //            headerreport += '<tr><th colspan="6" style="text-align: right; font-size: 22px; font-weight: bolder;border:0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> :&nbsp;' + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds();
        //            headerreport += '<tr><th colspan="6" style="text-align: right; font-size: 22px; font-weight: bolder;border:0px;"><br>';
        //            thead.append(headerreport);

        //            var headertable = "";
        //            headertable = "<tr class='warning'>";
        //            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>';
        //            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102162") %>	';
        //            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %>	';
        //            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %>';
        //            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105033") %>';
        //            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105045") %>';
        //            tbody.append(headertable);

        //            var indexemployess = 1;
        //$.each(result, function (index, data) {
        //    var type = result[index].type
        //    var employessdata = result[index].employess
        //    $.each(employessdata, function (indexlemp, dataemp) {
        //        var rowstable = "<tr class='active'>";
        //        rowstable += "<td style='text-align:center;font-size: 20px;'>" + (indexemployess++);
        //        if (indexlemp === 0) rowstable += '<td rowspan="' + employessdata.length + '" id="headder" style="text-align: center; font-size: 20px; font-weight: bolder">' + type;
        //        rowstable += "<td style='text-align:left; font-size: 20px;'>" + employessdata[indexlemp].phone;
        //        rowstable += "<td style='text-align:left;font-size: 20px;'>" + employessdata[indexlemp].employessname;
        //        rowstable += "<td style='text-align:left;font-size: 20px;'>" + employessdata[indexlemp].timetable;
        //        rowstable += "<td style='text-align:left;font-size: 20px;'>" + employessdata[indexlemp].password;
        //        tbody.append(rowstable);
        //    })
        //});
        //        }
        //        $("body").mLoading('hide');
        //    });
        //}

        function Reports() {
            var etype = $('#ddlsublevel').val();
            var name = TAC.GetUserName();
            var rtype = $('#DropDownListType').val();

            var data = {
                "etype": etype,
                "name": name,
                "rtype": rtype,
            };

            //$("body").mLoading();
            $(".report-wrapper").hide();

            window["renderReport" + rtype](data);
            $("#reporttype" + rtype).show();
        }

        function Export() {
            var etype = $('#ddlsublevel').val();
            var name = TAC.GetUserName();
            var rtype = $('#DropDownListType').val();

            var data = {
                "etype": etype,
                "name": name,
                "rtype": rtype,
            };

            //$("body").mLoading();
            var url = "Handles/ReportEmployee_Handler.ashx?etype=" + data.etype + "&name=" + data.name + "&rtype=" + data.rtype;
            window.open(url);
            //$("body").mLoading('hide');
            //$(".report-wrapper").hide();

            //window["renderReport" + rtype](data);
            //$("#reporttype" + rtype).show();
        }

        function renderReport1(data) {

            PageMethods.SearchReportsType1(data, function (result) {

                var $content = $('#reporttype1 .htmlcontent');
                var _content = "";
                _content += '<table id="datatable1" class="table-hover dataTable" cellspacing="0" width="100%">';
                _content += '<thead class="myHead">\
                                    <tr >\
                                        <td align="center" class="" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td>\
                                        <td align="center" class="" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102162") %></td>\
                                        <td align="center" class="" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></td>\
                                        <td align="center" class="" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></td>\
                                        <td align="center" class="" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105033") %></td>\
                                        <td align="center" class="" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105045") %></td>\
                                    </tr>\
                                </thead>';
                _content += '<tbody class="myTable">';

                $.each(result, function (j, r) {
                    _content += '<tr><td align="center">' + (j + 1) + '</td>\
                                    <td align="center" >' + r.type + '</td>\
                                    <td align="center" >' + r.phone + '</td>\
                                    <td align="left">' + r.employessname + '</td>\
                                    <td align="center">' + r.timetable + '</td>\
                                    <td align="center">' + r.password + '</td>\
                                </tr>';
                });

                _content += '</tbody>';
                _content += '</table>';

                $content.html('');
                $content.append(_content);

                $('#datatable1').DataTable({
                    paging: true,
                    "pageLength": 20,
                    "bLengthChange": false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    //'rowsGroup': [1],
                });

                //$("body").mLoading('hide');

            });

        }

        function renderReport2(data) {

            PageMethods.SearchReportsType2(data, function (result) {

                var $content = $('#reporttype2 .htmlcontent');
                $content.html('');

                var _content = "";
                _content += '<table id="datatable2" class="table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                _content += '<thead class="myHead">\
                                    <tr >\
                            <td rowspan=2 align="center" class="" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td>\
                            <td colspan=27 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101029") %></td>\
                            <td colspan=10 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %></td>\
                            <td colspan=10 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102029") %></td>\
                            <td colspan=11 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102093") %></td>\
                            <td colspan=7 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102033") %></td>\
                            <td colspan=6 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102049") %></td>\
                            <td colspan=4 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102065") %></td>\
                            <td colspan=5 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102071") %></td>\
                            <td colspan=6 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102114") %></td>\
                            <td colspan=5 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102113") %></td>\
                                    </tr>\
                 <tr>\
                <td 1 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %></td>\
                <td 2 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401083") %></td>\
                <td 3 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></td>\
                <td 4 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102013") %></td>\
                <td 5 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %></td>\
                <td 6 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102015") %></td>\
                <td 7 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></td>\
                <td 8 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></td>\
                <td 9 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101068") %></td>\
                <td 10 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101070") %></td>\
                <td 11 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105046") %></td>\
                <td 12 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %></td>\
                <td 13 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101083") %></td>\
                <td 14 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101084") %></td>\
                <td 15  align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></td>\
                <td 16 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101217") %></td>\
                <td 17 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></td>\
                <td 18 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></td>\
                <td 19 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></td>\
                <td 20 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102022") %></td>\
                <td 21 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102023") %></td>\
                <td 22 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102024") %></td>\
                <td 23 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102025") %></td>\
                <td 24 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %></td>\
                <td 25 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105033") %></td>\
                <td 26 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101116") %></td>\
                <td 27 data-col=28 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105045") %></td>\
                    \
                <td 28 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></td>\
                <td 29 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102031") %></td>\
                <td 30 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102030") %></td>\
                <td 31 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></td>\
                <td 32 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102032") %></td>\
                <td 33 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></td>\
                <td 34 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></td>\
                <td 35 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></td>\
                <td 36 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></td>\
                <td 37 data-col=10 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></td>\
                    \
                <td 38 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></td>\
                <td 39 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102031") %></td>\
                <td 40 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102030") %></td>\
                <td 41 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></td>\
                <td 42 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102032") %></td>\
                <td 43 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></td>\
                <td 44 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></td>\
                <td 45 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></td>\
                <td 46 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></td>\
                <td 47 data-col=10 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></td>\
                \
                <td 48 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105047") %></td>\
                <td 49 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %></td>\
                <td 50 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105048") %></td>\
                <td 51 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105049") %></td>\
                <td 52 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105050") %></td>\
                <td 53 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105051") %></td>\
                <td 54 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105052") %></td>\
                <td 55 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105053") %></td>\
                <td 56 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105054") %></td>\
                <td 57 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105055") %></td>\
                <td 58 data-col=11 align = "center" class="color2" style = "height: 30px;" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105056") %></td >\
                    \
                <td 59 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td>\
                <td 60 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101192") %></td>\
                <td 61 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></td>\
                <td 62 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></td>\
                <td 63 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></td>\
                <td 64 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></td>\
                <td 65 data-col=7 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102034") %></td>\
                    \
                <td 66 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td>\
                <td 67 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01072") %></td>\
                <td 68 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01072") %></td>\
                <td 69 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102045") %></td>\
                <td 70 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102063") %></td>\
                <td 71 data-col=6 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102050") %></td>\
                    \
                <td 72 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td>\
                <td 73 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102066") %></td>\
                <td 74 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102067") %></td>\
                <td 75 data-col=4 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102130") %></td>\
                    \
                <td 76 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td>\
                <td 77 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102072") %></td>\
                <td 78 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102073") %></td>\
                <td 79 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102074") %></td>\
                <td 80 data-col=5 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102075") %></td>\
                    \
                <td 81 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td>\
                <td 82 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102115") %></td>\
                <td 83 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102116") %></td>\
                <td 84 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102117") %></td>\
                <td 85 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102118") %></td>\
                <td 86 data-col=6 align="center" class="color1" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102119") %></td>\
                \
                <td 87 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td>\
                <td 88 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102130") %></td>\
                <td 89 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102132") %></td>\
                <td 90 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></td>\
                <td 91 data-col=5 align="center" class="color2" style="height: 30px; "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102140") %></td>\
                \
                 </tr>\
                                </thead>';
                _content += '<tbody class="myTable">';

                var personalStatus = {
                    '1': '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102038") %>',
                    '2': '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121008") %>',
                    '3': '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102039") %>',
                    '4': '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102040") %>',
                    '5': '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102041") %>'
                };

                var workStatus = {
                    '1': '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102096") %>',
                    '2': '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101032") %>',
                    '3': '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121003") %>',
                };

                var licenseType = {
                    '1': '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210024") %>',
                    '2': '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121089") %>',
                    '3': '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121090") %>',
                    '4': '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121091") %>',
                };


                $.each(result, function (j, r) {

                    var maxrow = _.max([r.address.length, r.education.length, r.family.length, r.honor.length, r.insignia.length, r.info.length, r.license.length, r.teaching.length, r.training.length, r.salary.length, 1]);


                    //if (maxrow > 1) {
                    //    console.log(maxrow);
                    //}

                    var _info = r.info[0] || {};
                    var _address = r.address[0] || {};
                    var _salary = r.salary[0] || {};





                    for (var i = 0; i < maxrow; i++) {

                        _content += '<tr  data-semp=' + r.emp.sEmp + '>';
                        _content += '<td   align="center">' + (j + 1) + '</td>';
                        _content += '<td   align="center">' + (r.type || '') + '</td>';
                        _content += '<td   align="center">' + (_info.Code || '') + '</td>';
                        _content += '<td   align="center">' + (r.job || '') + '</td>';
                        _content += '<td   align="center">' + (r.dept || '') + '</td>';
                        _content += '<td   align="center">' + (r.emp.cSex == "0" ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>" : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %>") + '</td>';
                        _content += '<td   align="center">' + (r.title || '') + '</td>';
                        _content += '<td   align="center">' + (r.emp.sName || '') + '</td>';
                        _content += '<td   align="center">' + (r.emp.sLastname || '') + '</td>';
                        _content += '<td   align="center">' + (_info.FirstNameEn || '') + '</td>';
                        _content += '<td   align="center">' + (_info.LastNameEn || '') + '</td>';
                        _content += '<td   align="center">' + (r.emp.nMoney || '0') + '</td>';
                        _content += '<td   align="center">' + (r.emp.sIdentification || '') + '</td>';
                        _content += '<td   align="center">' + (_info.PassportNumber || '') + '</td>';
                        _content += '<td   align="center">' + (_info.PassportCountry || '') + '</td>';
                        _content += '<td   align="center">' + (r.dob || '') + '</td>';
                        _content += '<td   align="center">' + (_info.BloodType || '') + '</td>';
                        _content += '<td   align="center">' + (_info.Nationality || '') + '</td>';
                        _content += '<td   align="center">' + (_info.Ethnicity || '') + '</td>';
                        _content += '<td   align="center">' + (_info.Religion || '') + '</td>';
                        _content += '<td   align="center">' + (personalStatus[_info.PersonalStatus] || '') + '</td>';
                        _content += '<td   align="center">' + (_info.SpouseFirstName || '') + '</td>';
                        _content += '<td   align="center">' + (_info.SpouseLastName || '') + '</td>';
                        _content += '<td   align="center">' + r.emp.sPhone + '</td>';
                        _content += '<td   align="center">' + r.emp.sEmail + '</td>';
                        _content += '<td   align="center">' + r.timetype + '</td>';
                        _content += '<td   align="center">' + r.bio + '</td>';
                        _content += '<td   data-col=28 align="center">' + r.password + '</td>';

                        _content += '<td   align="center">' + (r.emp.sHomeNumber || '') + '</td>';
                        _content += '<td   align="center">' + (r.emp.sMuu || '') + '</td>';
                        _content += '<td   align="center">' + (r.emp.Village || '') + '</td>';
                        _content += '<td   align="center">' + (r.emp.sSoy || '') + '</td>';
                        _content += '<td   align="center">' + (r.emp.Building || '') + '</td>';
                        _content += '<td   align="center">' + (r.emp.sRoad || '') + '</td>';
                        _content += '<td   align="center">' + (r.emp.Province || '') + '</td>';
                        _content += '<td   align="center">' + (r.emp.Amphur || '') + '</td>';
                        _content += '<td   align="center">' + (r.emp.Tumbon || '') + '</td>';
                        _content += '<td data-col=10   align="center">' + (r.emp.sPost || '') + '</td>';

                        _content += '<td   align="center">' + (_address.No || '') + '</td>';
                        _content += '<td   align="center">' + (_address.VillageNo || '') + '</td>';
                        _content += '<td   align="center">' + (_address.Village || '') + '</td>';
                        _content += '<td   align="center">' + (_address.Alley || '') + '</td>';
                        _content += '<td   align="center">' + (_address.Building || '') + '</td>';
                        _content += '<td   align="center">' + (_address.Road || '') + '</td>';
                        _content += '<td   align="center">' + (_address.Province || '') + '</td>';
                        _content += '<td   align="center">' + (_address.Amphur || '') + '</td>';
                        _content += '<td   align="center">' + (_address.Tubmon || '') + '</td>';
                        _content += '<td data-col=10   align="center">' + (_address.Postcode || '') + '</td>';

                        _content += '<td   align="center">' + (workStatus[_salary.WorkStatus] || '') + '</td>';
                        _content += '<td   align="center">' + (_salary.Degree || '') + '</td>';
                        _content += '<td   align="center">' + (_salary.WorkInEducationDate || '') + '</td>';
                        _content += '<td   align="center">' + (_salary.GovernmentOrderDate || '') + '</td>';
                        _content += '<td   align="center">' + (_salary.Salary || '') + '</td>';
                        _content += '<td   align="center">' + (_salary.WorkStartDate || '') + '</td>';
                        _content += '<td   align="center">' + (_salary.PositionMoney || '') + '</td>';
                        _content += '<td   align="center">' + (_salary.AcademicStandingMoney || '') + '</td>';
                        _content += '<td   align="center">' + (_salary.RetirementDate || '') + '</td>';
                        _content += '<td   align="center">' + (_salary.NetSalary || '') + '</td>';
                        _content += '<td  data-col=11+  align="center">' + (_salary.RemainGovernmentDay || '') + '</td>';

                        //_content += '<tr  >';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   data-col=27 align="center"></td>';

                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td data-col=10   align="center"></td>';

                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td data-col=10   align="center"></td>';

                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';
                        //_content += '<td   align="center"></td>';

                        //for (var i = 0; i < maxrow; i++) {

                        //    if (i > 0) {
                        //        _content += '<tr>';
                        //    }

                        var _family = r.family[i] || {};

                        _content += '<td  align="center">' + (_family.ID || '') + '</td>';
                        _content += '<td  align="center">' + (_family.FamilyRelation || '') + '</td>';
                        _content += '<td  align="center">' + (_family.Title || '') + '</td>';
                        _content += '<td  align="center">' + (_family.FirstName || '') + '</td>';
                        _content += '<td  align="center">' + (_family.LastName || '') + '</td>';
                        _content += '<td  align="center">' + (_family.dob || '') + '</td>';
                        _content += '<td  align="center">' + (_family.LiveStatus || _family.DeathStatus || '') + '</td>';

                        var _edu = r.education[i] || {};
                        //var _eduDesc = r.educationDesc[i] || {};

                        _content += '<td  align="center">' + (_edu.ID || '') + '</td>';
                        _content += '<td  align="center">' + (_edu.StudyYear || '') + '</td>';
                        _content += '<td  align="center">' + (_edu.GraduationYear || '') + '</td>';
                        _content += '<td  align="center">' + (_edu.Level || '') + '</td>';
                        _content += '<td  align="center">' + (_edu.Major || '') + '</td>';
                        _content += '<td  align="center">' + (_edu.Institution || '') + '</td>';

                        var _honor = r.honor[i] || {};
                        _content += '<td  align="center">' + (_honor.ID || '') + '</td>';
                        _content += '<td  align="center">' + (_honor.Type || '') + '</td>';
                        _content += '<td  align="center">' + (_honor.Department || '') + '</td>';
                        _content += '<td  align="center">' + (_honor.Year || '') + '</td>';

                        var _training = r.training[i] || {};
                        _content += '<td  align="center">' + (_training.ID || '') + '</td>';
                        _content += '<td  align="center">' + (_training.ProjectName || '') + '</td>';
                        _content += '<td  align="center">' + (_training.TrainingName || '') + '</td>';
                        _content += '<td  align="center">' + (_training.StartDate || '') + '</td>';
                        _content += '<td  align="center">' + (_training.EndDate || '') + '</td>';

                        var _license = r.license[i] || {};
                        _content += '<td  align="center">' + (_license.ID || '') + '</td>';
                        _content += '<td  align="center">' + (licenseType[_license.LicenseType] || '') + '</td>';
                        _content += '<td  align="center">' + (_license.LicenseNo || '') + '</td>';
                        _content += '<td  align="center">' + (_license.LicenseName || '') + '</td>';
                        _content += '<td  align="center">' + (_license.IssuedDate || '') + '</td>';
                        _content += '<td  align="center">' + (_license.ExpireDate || '') + '</td>';

                        var _insignia = r.insignia[i] || {};
                        _content += '<td  align="center">' + (_insignia.ID || '') + '</td>';
                        _content += '<td  align="center">' + (_insignia.Year || '') + '</td>';
                        _content += '<td  align="center">' + (_insignia.Grade || '') + '</td>';
                        _content += '<td  align="center">' + (_insignia.Position || '') + '</td>';
                        _content += '<td  align="center">' + (_insignia.Date || '') + '</td>';

                        _content += '</tr>';
                    }

                });

                _content += '</tbody>';
                _content += '</table>';

                $content.html('');
                $content.append(_content);

                if ($.fn.DataTable.isDataTable('#datatable2')) {
                    //$('#datatable2').dataTable();
                    //$table.clear();
                    $table.destroy();
                }
                //else {
                //}

                $table = $('#datatable2').DataTable({
                    paging: false,
                    "pageLength": 20,
                    "bLengthChange": false,
                    searching: false,
                    info: false,
                    //rowGroup : true,
                    //rowGroup: {
                    //    enable: false
                    //},
                    //destroy: true,
                    //"bSort": true,
                    //order: [[0, 'asc']],                    
                    'rowsGroup': [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58],
                });

                //$table.rowGroup().enable().draw();

                //$("body").mLoading('hide');

            });

        }

        $(document).ready(function () {

            //$.fn.dataTableExt.sErrMode = 'none'

            //$("#exportfile").click(function () {
            //    var dt = new Date();
            //    $('#example').tableExport({ type: 'excel', escape: 'false', sheets: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105004") %>', fileName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105004") %>_" + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
            //});

            //getlistEmployee();

        });

        //function getlistEmployee() {
        //    availableValueEmployees = [];
        //    $.ajax({
        //        url: "/App_Logic/modalJSON.aspx?mode=teacher",
        //        dataType: "json",
        //        success: function (objjson) {
        //            $.each(objjson, function (index) {
        //                var newObject = {
        //                    label: objjson[index].sName + ' ' + objjson[index].sLastname,
        //                    value: objjson[index].sEmp
        //                };
        //                availableValueEmployees[index] = newObject;
        //            });
        //        }
        //    });

        //    $('#txtname').autocomplete({
        //        width: 300,
        //        max: 10,
        //        delay: 100,
        //        minLength: 1,
        //        autoFocus: true,
        //        cacheLength: 1,
        //        scroll: true,
        //        highlight: false,

        //        source: function (request, response) {
        //            var results = $.ui.autocomplete.filter(availableValueEmployees, request.term);
        //            response(results.slice(0, 10));
        //        },
        //        select: function (event, ui) {
        //            event.preventDefault();
        //            $("#txtname").val(ui.item.label);
        //        },
        //        focus: function (event, ui) {
        //            event.preventDefault();
        //        }

        //    });

        //}


    </script>
</asp:Content>
