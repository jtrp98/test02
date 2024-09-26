<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="studentreports.aspx.cs" Inherits="FingerprintPayment.Report.studentreports" %>

<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>
<%@ Register Src="~/UserControls/YTLCFilter.ascx" TagPrefix="uc1" TagName="YTLCFilter" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <style type="text/css">
        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }

        .dropdown.bootstrap-select {
            padding-left: 0 !important;
            padding-right: 0 !important;
        }

        #reporttype1 #headder {
            background-color: #fff !important;
            color: #000000 !important;
            white-space: nowrap;
            padding: 15px 25px !important;
        }

        .report-wrapper .scoreBar {
            overflow: auto;
        }

        .report-wrapper .nav-item {
            border: 1px solid #ccc;
            border-radius: 24px;
        }

        .report-wrapper .table thead th {
            min-width: 55px !important;
            padding-top: 40px !important;
            text-align: center !important;
            /* font-size: 20px !important;*/
            /*font-weight: bolder !important;*/
        }

        .dataTable a.btn-a {
            width: auto !important;
        }

        #reporttype1.report-wrapper .table thead td {
            min-width: 70px !important;
            padding-top: 50px !important;
            text-align: center !important;
            /* font-size: 20px !important;*/
            font-weight: bolder !important;
        }

        #reporttype9 .img-container {
            border: 1px solid #eeeeee;
            margin: 2px;
            width: 100%;
            overflow: hidden;
            max-height: 240px;
            min-height: 240px;
            min-width: 200px;
            max-width: 200px;
            /* background-size: auto; */
        }

        a.disabled {
            color: #c5c5c5 !important;
        }
        /*  .report-wrapper .nav {
            position: absolute;
            left: 20px;
            top: -50px;
        }*/
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104002") %>          
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

                        <uc1:YTLCFilter runat="server" ID="YTLCFilter" />

                        <div class="row">
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                            <div class="col-md-3">
                                <uc1:StudentAutocomplete runat="server" ID="StudentAutocomplete" />
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %></label>
                            <div class="col-md-3">

                                <asp:DropDownList ID="DropDownListType" runat="server" class=" selectpicker col-md-12" data-style="select-with-transition"   data-width="100%" ClientIDMode="Static">
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %>" Value="1" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104004") %>" Value="2" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104005") %>" Value="16" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104006") %>" Value="14" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104007") %>" Value="12" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104008") %>" Value="9" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104009") %>" Value="15" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104010") %>" Value="3" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104011") %>" Value="4" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104012") %>" Value="13" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104013") %>" Value="5" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104014") %>" Value="6" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104015") %>" Value="7" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104016") %>" Value="8" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104017") %>" Value="10" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104018") %>" Value="11" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104019") %></label>
                            <div class="col-md-3">
                                <select id="sltStudentStatus" name="sltStudentStatus" class="selectpicker" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103002") %>"  data-width="100%" multiple>
                                    <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101031") %></option>
                                    <option selected="selected" value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101033") %></option>
                                    <option selected="selected"  value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101032") %></option>
                                    <option selected="selected" value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101034") %></option>
                                    <option selected="selected" value="4" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101030") %></option>
                                    <option selected="selected" value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101035") %></option>
                                    <option selected="selected" value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101036") %></option>
                                    <option selected="selected" value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101037") %></option>
                                </select>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"></label>
                            <div class="col-md-3">
                                
                            </div>
                            <div class="col-md-2"></div>

                        </div>
                        <div class="row">

                            <div class="col-md-6 text-right">
                                <button type="button" class="btn btn-info " onclick="Reports();">
                                    <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                            </div>

                            <div class="col-md-6 text-left">

                                <div class="dropdown ">

                                    <button class="btn btn-success dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                        <span class="material-icons">receipt_long
                                        </span>
                                        Export
                                        <span class="caret"></span>
                                    </button>
                                    <ul id="ddlMenu1" class="dropdown-menu" aria-labelledby="dropdownMenu1">
                                        <li><a id="menu1-item1" href="#" onclick="Export('pdf');" class="disabled" style="">PDF</a></li>
                                        <li><a id="menu1-item2" href="#" onclick="Export('excel');" style="">EXCEL</a></li>
                                        <li><a id="menu1-item3" href="#" onclick="Export('excelAll');" class="disabled" style="">EXCEL (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305014") %>)</a></li>
                                    </ul>
                                </div>

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
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %></h4>
                    </div>
                    <div class="card-body ">

                        <div class='row d-none' style="font-weight: bolder;">
                            <br />
                            <fieldset>
                                <legend><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M406001") %></legend>
                                <div class='form-group col-sm-4'>
                                    <p class='text-center text-success'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %><br />
                                        <span class='text-large' id="status01">0</span>
                                    </p>
                                </div>
                                <div class='form-group col-sm-4'>
                                    <p class='text-center text-warning'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %><br />
                                        <span class='text-large' id="status02">0</span>
                                    </p>
                                </div>
                                <div class='form-group col-sm-4'>
                                    <p class='text-center text-danger'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %><br />
                                        <span class='text-large' id="status03">0</span>
                                    </p>
                                </div>
                            </fieldset>
                        </div>

                        <div class="row">

                            <div class=" col-md-12 ">
                                <div id="dynamicCol" class="dropdown pull-right">
                                    <button class="btn btn-info dropdown-toggle" type="button" id="dropdownMenu2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104021") %>
    <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu" aria-labelledby="dropdownMenu2" style="max-height: 250px; overflow: auto">
                                        <li><a onclick="addCol(0);" href="#" style="">-</a></li>
                                        <li><a onclick="addCol(1);" href="#" style="">1</a></li>
                                        <li><a onclick="addCol(2);" href="#" style="">2</a></li>
                                        <li><a onclick="addCol(3);" href="#" style="">3</a></li>
                                        <li><a onclick="addCol(4);" href="#" style="">4</a></li>
                                        <li><a onclick="addCol(5);" href="#" style="">5</a></li>
                                        <li><a onclick="addCol(6);" href="#" style="">6</a></li>
                                        <li><a onclick="addCol(7);" href="#" style="">7</a></li>
                                        <li><a onclick="addCol(8);" href="#" style="">8</a></li>
                                        <li><a onclick="addCol(9);" href="#" style="">9</a></li>
                                        <li><a onclick="addCol(10);" href="#" style="">10</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <asp:Literal ID="ltrHeaderReport" runat="server" />

                        <div class="row border-bottom">
                            <br />
                            <div class="col-sm-12">

                                <%--       <asp:ListView ID="lvReport" runat="server">
                    </asp:ListView>--%>
                                <div id="reporttype1" class="report-wrapper">

                                    <ul class="nav nav-pills">
                                    </ul>

                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>

                                <div id="reporttype2" class="report-wrapper" style="display: none;">
                                    <ul class="nav nav-pills">
                                    </ul>

                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>


                                <div id="reporttype3" class="report-wrapper" style="display: none;">
                                    <ul class="nav nav-pills">
                                    </ul>

                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>

                                <div id="reporttype4" class="report-wrapper" style="display: none;">
                                    <ul class="nav nav-pills">
                                    </ul>
                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>

                                <div id="reporttype5" class="report-wrapper" style="display: none;">

                                    <ul class="nav nav-pills">
                                    </ul>

                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>

                                <div id="reporttype6" class="report-wrapper" style="display: none;">

                                    <ul class="nav nav-pills">
                                    </ul>

                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>

                                <div id="reporttype7" class="report-wrapper" style="display: none;">

                                    <ul class="nav nav-pills">
                                    </ul>

                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>

                                <div id="reporttype8" class="report-wrapper" style="display: none;">

                                    <ul class="nav nav-pills">
                                    </ul>
                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>

                                <div id="reporttype9" class="report-wrapper" style="display: none;">

                                    <ul class="nav nav-pills">
                                    </ul>
                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>

                                <div id="reporttype10" class="report-wrapper" style="display: none;">

                                    <ul class="nav nav-pills">
                                    </ul>
                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>

                                <div id="reporttype11" class="report-wrapper" style="display: none;">

                                    <ul class="nav nav-pills">
                                    </ul>
                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>


                                <div id="reporttype12" class="report-wrapper" style="display: none;">
                                    <ul class="nav nav-pills">
                                    </ul>

                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>

                                <div id="reporttype13" class="report-wrapper" style="display: none;">
                                    <ul class="nav nav-pills">
                                    </ul>

                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>


                                <div id="reporttype14" class="report-wrapper" style="display: none;">
                                    <ul class="nav nav-pills">
                                    </ul>

                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>

                                <div id="reporttype15" class="report-wrapper" style="display: none;">
                                    <ul class="nav nav-pills">
                                    </ul>

                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>

                                <div id="reporttype16" class="report-wrapper" style="display: none;">
                                    <ul class="nav nav-pills">
                                    </ul>

                                    <fieldset class="scoreBar">
                                        <div class="tab-content">
                                        </div>
                                    </fieldset>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>


    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">


    <script type="text/javascript" src="//unpkg.com/xlsx/dist/shim.min.js"></script>
    <script type="text/javascript" src="//unpkg.com/xlsx/dist/xlsx.full.min.js"></script>

    <script type="text/javascript" src="//unpkg.com/blob.js@1.0.1/Blob.js"></script>
    <script type="text/javascript" src="//unpkg.com/file-saver@1.3.3/FileSaver.js"></script>

    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>

    <script type="text/javascript">

        $.fn.dataTable.ext.errMode = 'none';

        var availableValuestudent = [];
        var searchData = null;
        var level = "";
        var level2 = "";
        var studentname = "";
        var year_Id = "";
        var term_id = "";
        var year_no = 0;

        $(document).ready(function () {
            $("#exportfile").click(function () {
                var dt = new Date();
                $('#reporttype1').tableExport({ type: 'excel', escape: 'false', sheets: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104001") %>', fileName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104001") %>_" + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
            });

            $('select[id*=ddlyear]').change(function () {
                getListTrem();
            });

            $('select[id*=ddlsublevel]').change(function () {
                getListSubLV2();
                getliststudent();
            });

            getliststudent();

            $('select[id*=ddlSubLV2]').change(function () {
                getliststudent();
            });

            $('#DropDownListType').change(function () {

                var _val = $(this).val();

                switch (_val) {
                    case '1':
                        $('#menu1-item3').removeClass('disabled');
                        break;

                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '12':
                    case '13':
                    case '14':
                    case '15':
                    case '16':
                        $('#menu1-item1').removeClass('disabled');
                        $('#menu1-item2').removeClass('disabled');
                        $('#menu1-item3').addClass('disabled');
                        break;

                    case '10':
                    case '11':
                        $('#dynamicCol').hide();
                        //$('#btnExport').hide();
                        $('#menu1-item1').addClass('disabled');
                        $('#menu1-item2').addClass('disabled');
                        $('#menu1-item3').addClass('disabled');
                        break;

                    default:
                        $('#dynamicCol').show();
                        //$('#btnExport').show();
                        $('#menu1-item1').addClass('disabled');
                        $('#menu1-item3').addClass('disabled');
                        break;
                }


            });

            //console.log(availableValuestudent);

            //$(".datepicker").datepicker();

            //$("#exportfile2").click(function () {

            //    //BuildReport2();

            //});


            $('.report-wrapper').on('change', '.input-dynamic', function (e) {
                $(this).parent().find('.span-dynamic').text($(this).val());
            });

            $('#DropDownListType').trigger('change');
        });

        function Reports() {
            level = YTLCF.GetLevelID();//$('select[id*=ddlsublevel]').val();
            level2 = YTLCF.GetClassID();//$('select[id*=ddlSubLV2]').val();
            year_Id = YTLCF.GetYearID();// $('select[id*=ddlyear]').val();
            year_no = YTLCF.GetYearNo();//$('select[id*=ddlyear] :selected').text()
            term_id = YTLCF.GetTermID();//$('select[id*=semister]').val();
            studentname = SAC.GetStudentName();//$("#txtname").val();

            //$("body").mLoading();


            //if ($('select[id*=ddlyear]').val() == "") {
            //    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133144") %>");
            //    //location.reload();
            //    return;
            //}
            //else if ($('select[id*=semister]').val() == "") {
            //    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133143") %>");
            //    //location.reload();
            //    return;
            //}
            //else if ($('select[id*=ddlsublevel]').val() == "") {
            //    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133208") %>");
            //    return;
            //    //location.reload();
            //}
            //else {

            //}

            var sort = {
                "level": level,
                "level2": level2,
                "year_Id": year_Id,
                "year": year_no,
                "term_id": term_id,
                "studentname": studentname,
                "status": $('#sltStudentStatus').val()
            };
            searchData = sort;
            // $("body").mLoading();

            $(".report-wrapper").hide();

            var type = $("#DropDownListType").val();
            $('#reporttype' + type + ' .myTable').html('');
            //console.log(searchData);
            window["renderReport" + type](searchData);

            $("#reporttype" + type).show();
        }

        function renderReport1(searchData) {

            //$("#reporttype1").html();
            //var thead = $("#reporttype1 thead");
            //var tbody = $("#reporttype1 tbody");
            //thead.html("");
            //tbody.html("");
            $("#divprint").html("");
            var dt = new Date();


            $("body").mLoading();
            PageMethods.SearchReports_Detail(searchData, function (result) {
                console.log(result);
                var $nav = $('#reporttype1 .nav');
                var $content = $('#reporttype1 .tab-content');

                $nav.html('');
                $content.html('');

                //var result = $.parseJSON(result);
                if (result.length === 0) {
                    $("#divprint").html("<center><h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105021") %></h1></center>");
                }
                else {


                    //var headerreport = '<tr class="hidden"><th colspan="18" style="text-align: center; border:0px;" id="school">' + $("input[id*=hdfschoolname]").val();
                    //headerreport += '<tr class="hidden"><th colspan="18" style="text-align: center; font-size: 22px;border:0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104001") %>';
                    //headerreport += '<tr class="hidden"><th colspan="18" style="text-align: right; font-size: 22px;border:0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603092") %>&nbsp;' + dt.toLocaleDateString();
                    //headerreport += '<tr class="hidden"><th colspan="18" style="text-align: right; font-size: 22px;border:0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> :&nbsp;' + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds();
                    //headerreport += '<tr class="hidden"><th colspan="18" style="text-align: right; font-size: 22px;border:0px;"><br>';
                    //thead.append(headerreport);

                    var headertable = "";
                    headertable = "<tr class='warning'>";
                    headertable += '<th id="headder" class="col-80px" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>';
                    //headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);" class="no-sort"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>';
                    //headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"  class="no-sort"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>';

                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %>
                    headertable += '<th id="headder" class="col-80px" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104022") %>';

                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105045") %>';

                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101050") %>';

                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %>';

                    headertable += '<th id="headder" class="" style="min-width: 100px !important; text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>';
                    headertable += '<th id="headder" class="" style="min-width: 100px !important; text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>';

                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %>';
                    headertable += '<th id="headder" class="" style="min-width: 100px !important; text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %>';
                    headertable += '<th id="headder" class="" style="min-width: 100px !important; text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %>';

                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101074") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104025") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101111") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101113") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104026") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104027") %>';

                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %>
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101130") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101143") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101146") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101145") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101144") %>';
                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101147") %>
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104028") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %>';

                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104031") %>
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104031") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104032") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104033") %>';

                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104034") %>
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104034") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104035") %>';

                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104071") %>
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101172") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);">GPA';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101178") %>';

                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133209") %>
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104037") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %>';

                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %>
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101192") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104038") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104039") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104025") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104040") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104041") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104042") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101201") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104043") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104045") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101197") %>';


                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %>
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211011") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00550") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104025") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104040") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104041") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104042") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104046") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104045") %>';

                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %>
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104047") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104048") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104025") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104040") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104041") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104042") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104049") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104045") %>';
                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %>
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104050") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104051") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101217") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101220") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131225") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101222") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101223") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101224") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101121") %>';
                    headertable += '<th id="headder" style="text-align: center;  background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104052") %>';
                    //thead.append(headertable);


                    $.each(result, function (i, data) {
                        //var level = result[index].level2
                        //$.each(level, function (indexlevel, datalevel)

                        var _nav = "";
                        var _content = "";

                        _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t1-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + ' ">' + (data.level1 + '/' + data.level2) + '</a></li>';
                        _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t1-room' + (i + 1) + '">';
                        _content += '<table class="table table-bordered table-hover dataTable " cellspacing="0" width="100%">';
                        _content += '<thead class="myHead">';
                        _content += headertable;
                        _content += '</thead>';

                        _content += '<tbody class="myTable">';

                        {
                            var student = data.students;// level[indexlevel].student
                            var rowspan = 0;

                            //$.each(level, function (indexlstudent, datastudent) {
                            //    rowspan += level[indexlstudent].student.length;
                            //})
                            //console.log("Rows = " + rowspan);
                            //var a = " ";
                            //if (rowspan > 0)
                            {
                                $.each(student, function (indexlstudent, datastudent) {
                                    var rowstable = "<tr class='activexs '>";
                                    rowstable += "<td style='text-align:center;'>" + (indexlstudent + 1);
                                    //if (indexlevel === 0 && indexlstudent === 0) {
                                    //    rowstable += '<td rowspan="' + rowspan + '" id="headder" style="text-align: center;  font-weight: bolder">' + result[index].levelname;
                                    //}
                                    //if (indexlstudent === 0) {
                                    //    rowstable += '<td rowspan="' + student.length + '" id="headder" style="text-align: center;  font-weight: bolder">' + level[indexlevel].level2name;
                                    //}

                                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %>
                                    rowstable += "<td style='text-align:center; '>" + (student[indexlstudent].student_number || '');

                                    var studentstatus = student[indexlstudent].studentStatus;
                                    var studentstatustxt;

                                    switch (studentstatus) {
                                        case 0:
                                            studentstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101031") %>";
                                            break;
                                        case 1:
                                            studentstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101033") %>";
                                            break;
                                        case 2:
                                            studentstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101032") %>";
                                            break;
                                        case 3:
                                            studentstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101034") %>";
                                            break;
                                        case 4:
                                            studentstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101030") %>";
                                            break;
                                        case 5:
                                            studentstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101035") %>";
                                            break;
                                        case 6:
                                            studentstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101036") %>";
                                            break;
                                        case 7:
                                            studentstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101037") %>";
                                            break;
                                        default:
                                            studentstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101031") %>";
                                            break;
                                    }

                                    rowstable += "<td style='text-align:left; '>" + studentstatustxt;

                                    rowstable += "<td style='text-align:left;' Int-Length='0000000000'>" + student[indexlstudent].sidentification;
                                    rowstable += "<td style='text-align:left; ' Int-Length='0000000000'>" + (student[indexlstudent].studentId || '');

                                    rowstable += "<td style='text-align:left; ' Int-Length='0000000000'>" + student[indexlstudent].studentPassWord;

                                    rowstable += "<td style='text-align:left;'>" + student[indexlstudent].stMoveIn;

                                    var studentsex = student[indexlstudent].studentsex;
                                    var studentsextxt;
                                    if (studentsex == 0) {
                                        studentsextxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>";
                                    }
                                    else if (studentsex == 1) {
                                        studentsextxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %>";
                                    } else {
                                        studentsextxt = "";
                                    }
                                    rowstable += "<td style='text-align:left;'>" + studentsextxt;


                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].titleDes === null ? "" : student[indexlstudent].titleDes);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].studentname === null ? "" : student[indexlstudent].studentname);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].studentlastname === null ? "" : student[indexlstudent].studentlastname);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stNickName === null ? "" : student[indexlstudent].stNickName);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].studentnameEN === null ? "" : student[indexlstudent].studentnameEN);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].studentlastnameEN === null ? "" : student[indexlstudent].studentlastnameEN);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stNickNameEN === null ? "" : student[indexlstudent].stNickNameEN);

                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].birth || "");

                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stReligion === null ? "" : student[indexlstudent].stReligion);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stNation === null ? "" : student[indexlstudent].stNation);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stRace === null ? "" : student[indexlstudent].stRace);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stSonTotal === null ? "" : student[indexlstudent].stSonTotal);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stSonNumber === null ? "" : student[indexlstudent].stSonNumber);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stRelativeHere === null ? "" : student[indexlstudent].stRelativeHere);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].phone === null ? "" : student[indexlstudent].phone);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stEmail === null ? "" : student[indexlstudent].stEmail);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].money || "");

                                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %>
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stHomeRegistCode === null ? "" : student[indexlstudent].stHomeRegistCode);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].homeRegistNumber === null ? "" : student[indexlstudent].homeRegistNumber);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].homeRegistSoy === null ? "" : student[indexlstudent].homeRegistSoy);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].homeRegistMuu === null ? "" : student[indexlstudent].homeRegistMuu);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].homeRegistRoad === null ? "" : student[indexlstudent].homeRegistRoad);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].homeRegistTumbon === null ? "" : student[indexlstudent].homeRegistTumbon);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].homeRegistAumpher === null ? "" : student[indexlstudent].homeRegistAumpher);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].homeRegistProvince === null ? "" : student[indexlstudent].homeRegistProvince);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].homeRegistPost === null ? "" : student[indexlstudent].homeRegistPost);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].homeRegistPhone === null ? "" : student[indexlstudent].homeRegistPhone);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].bornFrom === null ? "" : student[indexlstudent].bornFrom);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].bornFromTumbon === null ? "" : student[indexlstudent].bornFromTumbon);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].bornFromAumpher === null ? "" : student[indexlstudent].bornFromAumpher);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].bornFromProvince === null ? "" : student[indexlstudent].bornFromProvince);
                                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101147") %>
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].homeNumber === null ? "" : "&nbsp" + student[indexlstudent].homeNumber);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].muu === null ? "" : student[indexlstudent].muu);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].soy === null ? "" : student[indexlstudent].soy);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].road === null ? "" : student[indexlstudent].road);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].tumbon === null ? "" : student[indexlstudent].tumbon);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].aumpher === null ? "" : student[indexlstudent].aumpher);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].provin === null ? "" : student[indexlstudent].provin);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].post === null ? "" : student[indexlstudent].post);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stHousePhone === null ? "" : student[indexlstudent].stHousePhone);

                                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104031") %>
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].ststayWithName || "");
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].ststayWithLast || "");

                                    var sststayHomeType = student[indexlstudent].ststayHomeType;
                                    var sststayHomeTypetxt;
                                    if (sststayHomeType == 1) {
                                        sststayHomeTypetxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101159") %>";
                                    }
                                    else if (sststayHomeType == 2) {
                                        sststayHomeTypetxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101160") %>";
                                    }
                                    else if (sststayHomeType == 3) {
                                        sststayHomeTypetxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101161") %>";
                                    }
                                    else if (sststayHomeType == 4) {
                                        sststayHomeTypetxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101162") %>";
                                    }
                                    else if (sststayHomeType == 5) {
                                        sststayHomeTypetxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101163") %>";
                                    }
                                    else {
                                        sststayHomeTypetxt = "";
                                    }
                                    rowstable += "<td style='text-align:left;'>" + sststayHomeTypetxt;

                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].ststayWithEmail || "");
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].ststayWithEmergency || "");

                                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104034") %>
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].friNearHomename === null ? "" : student[indexlstudent].friNearHomename);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].friNearHomelast === null ? "" : student[indexlstudent].friNearHomelast);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].friNearHomephone === null ? "" : student[indexlstudent].friNearHomephone);

                                    //rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famTitle === null ? "" : student[indexlstudent].famTitle);

                                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104071") %>
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stOldSchoolName || "");
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stOldSchoolTumbon || "");
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stOldSchoolAumpher || "");
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stOldSchoolProvince || "");

                                    var OldSchoolGrad = student[indexlstudent].stOldSchoolGraduated;
                                    var OldSchoolGradtxt;

                                    switch (OldSchoolGrad + "") {

                                        case "1": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131192") %>"; break;
                                        case "2": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131193") %>"; break;
                                        case "3": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131194") %>"; break;
                                        case "4": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131195") %>"; break;
                                        case "5": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131196") %>"; break;
                                        case "6": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206406") %>"; break;
                                        case "7": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131198") %>"; break;
                                        case "8": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131199") %>"; break;
                                        case "9": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131200") %>"; break;
                                        case "10": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131203") %>"; break;
                                        case "11": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131188") %>"; break;
                                        case "12": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131189") %>"; break;
                                        case "13": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131190") %>"; break;
                                        case "14": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131191") %>"; break;
                                        case "15": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131201") %>"; break;
                                        case "16": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131202") %>"; break;
                                        case "17": OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131204") %>"; break;
                                        default: OldSchoolGradtxt = OldSchoolGrad; break;
                                    }

                                    //if (OldSchoolGrad == 1) {
                                    //    OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131192") %>";
                                    //}
                                    //else if (OldSchoolGrad == 2) {
                                    //    OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131193") %>";
                                    //}
                                    //else if (OldSchoolGrad == 3) {
                                    //    OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131194") %>";
                                    //}
                                    //else if (OldSchoolGrad == 4) {
                                    //    OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131195") %>";
                                    //}
                                    //else if (OldSchoolGrad == 5) {
                                    //    OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131196") %>";
                                    //}
                                    //else if (OldSchoolGrad == 6) {
                                    //    OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206406") %>";
                                    //}
                                    //else if (OldSchoolGrad == 7) {
                                    //    OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131198") %>";
                                    //}
                                    //else if (OldSchoolGrad == 8) {
                                    //    OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131199") %>";
                                    //}
                                    //else if (OldSchoolGrad == 9) {
                                    //    OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103101") %>";
                                    //}
                                    //else if (OldSchoolGrad == 10) {
                                    //    OldSchoolGradtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133210") %>";
                                    //}
                                    //else {
                                    //    OldSchoolGradtxt = OldSchoolGrad;
                                    //}
                                    rowstable += "<td style='text-align:left; '>" + OldSchoolGradtxt;

                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stOldSchoolGPA === null ? "" : student[indexlstudent].stOldSchoolGPA);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stmoveOutReason === null ? "" : student[indexlstudent].stmoveOutReason);

                                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133209") %>
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stOldhome === null ? "" : "&nbsp" + student[indexlstudent].stOldhome);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stOldmuu === null ? "" : student[indexlstudent].stOldmuu);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stOldsoy === null ? "" : student[indexlstudent].stOldsoy);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stOldroad === null ? "" : student[indexlstudent].stOldroad);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stOldtumbon === null ? "" : student[indexlstudent].stOldtumbon);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stOldaumper === null ? "" : student[indexlstudent].stOldaumper);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stOldprovince === null ? "" : student[indexlstudent].stOldprovince);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stOldpostcode === null ? "" : student[indexlstudent].stOldpostcode);
                                    rowstable += "<td style='text-align:left;'>" + (student[indexlstudent].stOldphone === null ? "" : student[indexlstudent].stOldphone);

                                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %>
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famRelate === null ? "" : student[indexlstudent].famRelate);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famTitle === null ? "" : student[indexlstudent].famTitle);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famName === null ? "" : student[indexlstudent].famName);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famlastname === null ? "" : student[indexlstudent].famlastname);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famNameEN === null ? "" : student[indexlstudent].famNameEN);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famlastnameEN === null ? "" : student[indexlstudent].famlastnameEN);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famBirday === null ? "" : student[indexlstudent].famBirday);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famReligion === null ? "" : student[indexlstudent].famReligion);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famRace === null ? "" : student[indexlstudent].famRace);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famNation === null ? "" : student[indexlstudent].famNation);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famhome === null ? "" : "&nbsp" + student[indexlstudent].famhome);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].fammuu === null ? "" : student[indexlstudent].fammuu);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famsoy === null ? "" : student[indexlstudent].famsoy);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famroad === null ? "" : student[indexlstudent].famroad);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famtumbon === null ? "" : student[indexlstudent].famtumbon);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famaumper === null ? "" : student[indexlstudent].famaumper);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famprovince === null ? "" : student[indexlstudent].famprovince);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].fampostcode === null ? "" : student[indexlstudent].fampostcode);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famphone1 === null ? "" : student[indexlstudent].famphone1);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famphone2 === null ? "" : student[indexlstudent].famphone2);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famphone3 === null ? "" : student[indexlstudent].famphone3);

                                    var Ffamstatus = student[indexlstudent].famstatus;
                                    var famstatustxt;
                                    if (Ffamstatus == 1) {
                                        famstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101203") %>";
                                    }
                                    else if (Ffamstatus == 2) {
                                        famstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101204") %>";
                                    }
                                    else if (Ffamstatus == 3) {
                                        famstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101205") %>";
                                    }
                                    else if (Ffamstatus == 4) {
                                        famstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101206") %>";
                                    }
                                    else if (Ffamstatus == 5) {
                                        famstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103105") %>";
                                    }
                                    else if (Ffamstatus == 6) {
                                        famstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101207") %>";
                                    }
                                    else if (Ffamstatus == 7) {
                                        famstatustxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101208") %>";
                                    }
                                    else {
                                        famstatustxt = "";
                                    }
                                    rowstable += "<td style='text-align:left; '>" + famstatustxt;

                                    var sfameducation = student[indexlstudent].fameducation;
                                    var sfameducationtxt;
                                    if (sfameducation == 1) {
                                        sfameducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102054") %>";
                                    }
                                    else if (sfameducation == 2) {
                                        sfameducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %>";
                                    }
                                    else if (sfameducation == 3) {
                                        sfameducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %>";
                                    }
                                    else if (sfameducation == 4) {
                                        sfameducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102059") %> ";
                                    }
                                    else if (sfameducation == 5) {
                                        sfameducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %>";
                                    }
                                    else if (sfameducation == 6) {
                                        sfameducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02176") %>";
                                    }
                                    else {
                                        sfameducationtxt = "";
                                    }
                                    rowstable += "<td style='text-align:left; '>" + sfameducationtxt;

                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famJob === null ? "" : student[indexlstudent].famJob);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famJobTower === null ? "" : student[indexlstudent].famJobTower);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famJobSalaryMonth);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].famJobSalary);

                                    var fWithMoney = student[indexlstudent].famWithdrawMoney;
                                    var fWithMoneytxt;
                                    if (fWithMoney == 0) {
                                        fWithMoneytxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101200") %>";
                                    }
                                    else if (fWithMoney == 1) {
                                        fWithMoneytxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101199") %>";
                                    }
                                    else {
                                        fWithMoneytxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103022") %>";
                                    }
                                    rowstable += "<td style='text-align:left; '>" + fWithMoneytxt;

                                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %>
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterTitle === null ? "" : student[indexlstudent].faterTitle);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterName === null ? "" : student[indexlstudent].faterName);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterLastname === null ? "" : student[indexlstudent].faterLastname);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterNameEN === null ? "" : student[indexlstudent].faterNameEN);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterLastnameEN === null ? "" : student[indexlstudent].faterLastnameEN);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterBirday === null ? "" : student[indexlstudent].faterBirday);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterReligion === null ? "" : student[indexlstudent].faterReligion);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterRace === null ? "" : student[indexlstudent].faterRace);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterNation === null ? "" : student[indexlstudent].faterNation);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterhome === null ? "" : "&nbsp" + student[indexlstudent].faterhome);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].fatermuu === null ? "" : student[indexlstudent].fatermuu);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].fatersoy === null ? "" : student[indexlstudent].fatersoy);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterroad === null ? "" : student[indexlstudent].faterroad);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].fatertumbon === null ? "" : student[indexlstudent].fatertumbon);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].fateraumper === null ? "" : student[indexlstudent].fateraumper);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterprovince === null ? "" : student[indexlstudent].faterprovince);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterpostcode === null ? "" : student[indexlstudent].faterpostcode);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterphone1 === null ? "" : student[indexlstudent].faterphone1);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterphone2 === null ? "" : student[indexlstudent].faterphone2);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterphone3 === null ? "" : student[indexlstudent].faterphone3);

                                    var sfatereducation = student[indexlstudent].fatereducation;
                                    var sfatereducationtxt;
                                    if (sfatereducation == 1) {
                                        sfatereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102054") %>";
                                    }
                                    else if (sfatereducation == 2) {
                                        sfatereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %>";
                                    }
                                    else if (sfatereducation == 3) {
                                        sfatereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %>";
                                    }
                                    else if (sfatereducation == 4) {
                                        sfatereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102059") %> ";
                                    }
                                    else if (sfatereducation == 5) {
                                        sfatereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %>";
                                    }
                                    else if (sfatereducation == 6) {
                                        sfatereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02176") %>";
                                    }
                                    else {
                                        sfatereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103022") %>";
                                    }
                                    rowstable += "<td style='text-align:left; '>" + sfatereducationtxt;

                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterJob === null ? "" : student[indexlstudent].faterJob);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterJobTower === null ? "" : student[indexlstudent].faterJobTower);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterJobSalaryMonth);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].faterJobSalary);

                                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %>
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterTitle === null ? "" : student[indexlstudent].moterTitle);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterName === null ? "" : student[indexlstudent].moterName);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterLastname === null ? "" : student[indexlstudent].moterLastname);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterNameEN === null ? "" : student[indexlstudent].moterNameEN);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterLastnameEN === null ? "" : student[indexlstudent].moterLastnameEN);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterBirday === null ? "" : student[indexlstudent].moterBirday);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterReligion === null ? "" : student[indexlstudent].moterReligion);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterRace === null ? "" : student[indexlstudent].moterRace);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterNation === null ? "" : student[indexlstudent].moterNation);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterhome === null ? "" : "&nbsp" + student[indexlstudent].moterhome);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].motermuu === null ? "" : student[indexlstudent].motermuu);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].motersoy === null ? "" : student[indexlstudent].motersoy);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterroad === null ? "" : student[indexlstudent].moterroad);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].motertumbon === null ? "" : student[indexlstudent].motertumbon);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moteraumper === null ? "" : student[indexlstudent].moteraumper);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterprovince === null ? "" : student[indexlstudent].moterprovince);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterpostcode === null ? "" : student[indexlstudent].moterpostcode);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterphone1 === null ? "" : student[indexlstudent].moterphone1);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterphone2 === null ? "" : student[indexlstudent].moterphone2);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterphone3 === null ? "" : student[indexlstudent].moterphone3);

                                    var smotereducation = student[indexlstudent].motereducation;
                                    var smotereducationtxt;
                                    if (smotereducation == 1) {
                                        smotereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102054") %>";
                                    }
                                    else if (smotereducation == 2) {
                                        smotereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %>";
                                    }
                                    else if (smotereducation == 3) {
                                        smotereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %>";
                                    }
                                    else if (smotereducation == 4) {
                                        smotereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102059") %> ";
                                    }
                                    else if (smotereducation == 5) {
                                        smotereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %>";
                                    }
                                    else if (smotereducation == 6) {
                                        smotereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02176") %>";
                                    }
                                    else {
                                        smotereducationtxt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103022") %>";
                                    }
                                    rowstable += "<td style='text-align:left; '>" + smotereducationtxt;

                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterJob === null ? "" : student[indexlstudent].moterJob);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterJobTower === null ? "" : student[indexlstudent].moterJobTower);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterJobSalaryMonth);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].moterJobSalary);

                                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %>
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stdWeight || '');
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stdHeight || '');
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stdBlood == null ? "" : student[indexlstudent].stdBlood);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stdSickFood == null ? "" : student[indexlstudent].stdSickFood);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stdSickDruq == null ? "" : student[indexlstudent].stdSickDruq);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stdSickOther == null ? "" : student[indexlstudent].stdSickOther);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stdSickNormal == null ? "" : student[indexlstudent].stdSickNormal);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].stdSickDanger == null ? "" : student[indexlstudent].stdSickDanger);

                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].JourneyType == null ? "" : student[indexlstudent].JourneyType);
                                    rowstable += "<td style='text-align:left; '>" + (student[indexlstudent].CardNFC);
                                    //tbody.append(rowstable);
                                    _content += rowstable;
                                })
                            }
                        }


                        _content += '</tbody>';
                        _content += '</table>';
                        _content += "</div>";

                        $nav.append(_nav);
                        $content.append(_content);
                    });


                    $('#reporttype1 .table').DataTable({
                        paging: false,
                        searching: false,
                        //orderable: false,
                        info: false,
                        bSort: true,
                        "autoWidth": false,
                        //responsive: true,
                        //columnDefs: [{
                        //    "targets": 'no-sort',
                        //    "orderable": false,
                        //}]
                    });
                }
                $("body").mLoading('hide');
            });
        }

        function renderReport2(searchData) {

            $("#divprint").html("");
            var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType2(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype2 .nav');
                var $content = $('#reporttype2 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t2-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + '">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t2-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                                    <tr class="thisrow">\
                                        <th rowspan="2" align="center" class="xsortable col-80px" style="height: 30px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                                        <th rowspan="2" align="center" class="xsortable col-80px" style="height: 30px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>\
                                        <th rowspan="2" align="center" class="xsortable" style="height: 30px; min-width: 150px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></th>\
                                        <th rowspan="2" align="center" class="xsortable" style="height: 30px; min-width: 180px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>\
                                        <th rowspan="2" align="center" class="xsortable" style="height: 30px; min-width: 100px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101350") %></th>\
                                        <th colspan="9" class="no-sort" style="height: 30px;"></th>\
                                        <th rowspan="2" align="center" style="width: 200px;" class="no-sort"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></th>\
                                    </tr>\
                                    <tr style="height: 35px;"><th class="no-sort"></th><th class="no-sort"></th><th class="no-sort"></th><th class="no-sort"></th><th class="no-sort"></th><th></th><th></th><th></th><th></th></tr>\
                                </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                                    <td align="center" >' + (s.no || '-') + '</td>\
                                    <td align="center" >' + s.code + '</td>\
                                    <td align="left">' + s.title + ' ' + s.fname + '</td>\
                                    <td align="left">' + s.lname + '</td>\
                                    <td align="left" style=""></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });

                $('#reporttype2 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                    "aoColumnDefs": [
                        {
                            'bSortable': false, 'aTargets': [3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                        }
                    ]
                });

                $("body").mLoading('hide');

            });

        }

        function renderReport16(searchData) {

            $("#divprint").html("");
            var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType16(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype16 .nav');
                var $content = $('#reporttype16 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t16-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + '">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t16-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                           <tr class="thisrow">\
                               <th  align="center" class="xsortable col-80px" style="height: 30px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                               <th  align="center" class="xsortable col-80px" style="height: 30px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>\
                               <th  align="center" class="xsortable" style="height: 30px; min-width: 150px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>\
                               <th  align="center" class="xsortable" style="height: 30px; min-width: 150px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104025") %></th>\
                               <th  align="center" class="xsortable" style="height: 30px; min-width: 150px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133211") %></th>\
                               <th  align="center" class="xsortable" style="height: 30px; min-width: 200px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133212") %></th>\
                                <th  align="center" class="xsortable" style="height: 30px; min-width: 200px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133214") %></th>\
                                <th  align="center" class="xsortable" style="height: 30px; min-width: 200px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104071") %></th>\
                                <th  align="center" class="xsortable" style="height: 30px; min-width: 200px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101178") %></th>\
                                <th  align="center" class="xsortable" style="height: 30px; min-width: 250px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101050") %></th>\
                                <th  align="center" class="xsortable" style="height: 30px; min-width: 200px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133213") %></th>\
                                <th  align="center" class="xsortable" style="height: 30px; min-width: 200px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %></th>\
                           </tr>\
                       </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                            <td align="center" >' + (s.code || '-') + '</td>\
                            <td align="center" >' + s.name + '</td>\
                            <td align="left">' + s.birthDay + '</td>\
                            <td align="left">' + `${s.tumbonHome || ''}<br/>${s.aumpherHome || ''}<br/>${s.provinceHome || ''}` + '</td>\
                            <td align="left">' + (s.father || '') + '<br/>' + (s.mother || '') + '</td>\
                            <td align="left">' + (s.fatherJob || '') + '<br/>' + (s.motherJob || '') + '</td>\
                            <td align="left">' + (s.oldSchool || '') + '</td>\
                            <td align="left">' + (s.reason || '') + '</td>\
                            <td align="left">' + s.firstDate + '</td>\
                            <td align="left">' + `${s.number || ''} ${s.soy || ''} ${s.muu || ''} ${s.road || ''} ${s.tumbon || ''}<br/>${s.aumpher || ''} ${s.province || ''}<br/>${s.post || ''} ${s.phone || ''}` + '</td>\
                            <td align="left">' + (s.oldDegree || '') + '</td>\
                       </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });

                $('#reporttype15 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                    //"aoColumnDefs": [
                    //    {
                    //        'bSortable': false, 'aTargets': [3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                    //    }
                    //]
                });

                $("body").mLoading('hide');

            });

        }


        function renderReport15(searchData) {

            $("#divprint").html("");
            var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType15(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype15 .nav');
                var $content = $('#reporttype15 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t15-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + '">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t15-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                               <tr class="thisrow">\
                                   <th  align="center" class="xsortable col-80px" style="height: 30px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                                   <th  align="center" class="xsortable col-80px" style="height: 30px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>\
                                   <th  align="center" class="xsortable" style="height: 30px; min-width: 150px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></th>\
                                   <th  align="center" class="xsortable" style="height: 30px; min-width: 150px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>\
                                   <th  align="center" class="xsortable" style="height: 30px; min-width: 150px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>\
                                   <th  align="center" class="xsortable" style="height: 30px; min-width: 300px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121068") %></th>\
                               </tr>\
                           </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                               <td align="center" >' + (s.no || '-') + '</td>\
                               <td align="center" >' + s.code + '</td>\
                               <td align="left">' + s.title + ' ' + s.fname + '</td>\
                               <td align="left">' + s.lname + '</td>\
                               <td align="left">' + `${s.number || ''} ${s.soy || ''} ${s.muu || ''} ${s.road || ''} ${s.tumbon || ''} ${s.aumpher || ''} ${s.province || ''} ${s.post || ''} ${s.phone || ''}` + '</td>\
                           </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });

                $('#reporttype15 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                    //"aoColumnDefs": [
                    //    {
                    //        'bSortable': false, 'aTargets': [3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                    //    }
                    //]
                });

                $("body").mLoading('hide');

            });

        }

        function renderReport14(searchData) {

            $("#divprint").html("");
            var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType2(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype14 .nav');
                var $content = $('#reporttype14 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t2-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + '">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t2-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                                    <tr class="thisrow">\
                                        <th rowspan="2" align="center" class="xsortable col-80px" style="height: 30px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                                        <th rowspan="2" align="center" class="xsortable col-80px" style="height: 30px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>\
                                        <th rowspan="2" align="center" class="xsortable" style="height: 30px; min-width: 150px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></th>\
                                        <th rowspan="2" align="center" class="xsortable" style="height: 30px; min-width: 220px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106106") %></th>\
                                        <th colspan="9" class="no-sort" style="height: 30px;"></th>\
                                        <th rowspan="2" align="center" style="width: 200px;" class="no-sort"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></th>\
                                    </tr>\
                                    <tr style="height: 35px;"><th class="no-sort"></th><th class="no-sort"></th><th class="no-sort"></th><th class="no-sort"></th><th class="no-sort"></th><th></th><th></th><th></th><th></th></tr>\
                                </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                                    <td align="center" >' + (s.no || '-') + '</td>\
                                    <td align="center" >' + s.code + '</td>\
                                    <td align="left">' + s.nameEn + '</td>\
                                    <td align="left" style=""></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });

                $('#reporttype14 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                    "aoColumnDefs": [
                        {
                            'bSortable': false, 'aTargets': [3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                        }
                    ]
                });
                $("body").mLoading('hide');
            });
        }

        function renderReport12(searchData) {

            $("#divprint").html("");
            var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType12(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype12 .nav');
                var $content = $('#reporttype12 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t2-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + '">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t2-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                                    <tr class="thisrow">\
                                        <th rowspan="2" align="center" class="xsortable col-80px" style="height: 30px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                                        <th rowspan="2" align="center" class="xsortable col-80px" style="height: 30px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>\
                                        <th rowspan="2" align="center" class="xsortable" style="height: 30px; min-width: 150px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></th>\
                                        <th rowspan="2" align="center" class="xsortable" style="height: 30px; min-width: 300px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></th>\
                                        <th colspan="9" class="no-sort" style="height: 30px;"></th>\
                                        <th rowspan="2" align="center" style="width: 200px;" class="no-sort"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></th>\
                                    </tr>\
                                    <tr style="height: 35px;"><th class="no-sort"></th><th class="no-sort"></th><th class="no-sort"></th><th class="no-sort"></th><th class="no-sort"></th><th></th><th></th><th></th><th></th></tr>\
                                </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                                    <td align="center" >' + (s.no || '-') + '</td>\
                                    <td align="center" >' + s.code + '</td>\
                                    <td align="left">' + s.name + '</td>\
                                    <td align="left" style=""></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                    <td align="left"></td>\
                                </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });

                $('#reporttype12 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                    "aoColumnDefs": [
                        {
                            'bSortable': false, 'aTargets': [3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                        }
                    ]
                });

                $("body").mLoading('hide');

            });

        }

        function renderReport3(searchData) {

            $("#divprint").html("");
            //var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType3(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                //$('#reporttype3 .-logo').attr('src', sc.logo);
                //$('#reporttype3 .-name').html(sc.nameTH);
                //$('#reporttype3 .-address').html(sc.address);
                //$('#reporttype3 .-tel').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105084") %> : ' + sc.phone1 + (sc.phone2 + '' == '' ? '' : ' ,' + sc.phone2) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133215") %> : ' + sc.fax);
                //$('#reporttype3 .-website').html(sc.website + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %>: ' + sc.email);

                var $nav = $('#reporttype3 .nav');
                var $content = $('#reporttype3 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    //_nav = "";
                    //_content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t3-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + ' ">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t3-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                                    <tr class="thisrow">\
                                        <th style="height: 30px;" class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                                        <th style="height: 30px;" class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>\
                                        <th style="height: 30px; width: 10%" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></th>\
                                        <th style="height: 30px; min-width: 200px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>\
                                        <th style="height: 30px; min-width: 150px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>\
                                        <th style="height: 30px; min-width: 150px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %></th>\
                                        <th style="height: 30px; min-width: 250px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></th>\
                                        <th style="height: 30px; min-width: 200px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %></th>\
                                    </tr>\
                                </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                                    <td align="center" >' + (s.no || '-') + '</td>\
                                    <td align="center" >' + s.code + '</td>\
                                    <td align="left">' + s.title + ' ' + s.fname + '</td>\
                                    <td align="left">' + s.lname + '</td>\
                                    <td align="center">' + s.nick + '</td>\
                                    <td align="center">' + s.date + '</td>\
                                    <td align="center" >' + s.id + '</td>\
                                </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);



                });

                $('#reporttype3 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false
                    //"aoColumnDefs": [
                    //    {
                    //        'bSortable': false, 'aTargets': [3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                    //    }
                    //]

                });

                $("body").mLoading('hide');

            });

        }

        function renderReport4(searchData) {

            $("#divprint").html("");
            //var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType3(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype4 .nav');
                var $content = $('#reporttype4 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t4-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + ' ">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t4-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                                    <tr class="thisrow">\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>\
                                        <th style="height: 30px;  min-width: 150px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></th>\
                                        <th style="height: 30px;  min-width: 300px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></th>\
                                        <th style="height: 30px;  min-width: 150px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %></th>\
                                        <th style="height: 30px;  min-width: 200px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></th>\
                                    </tr>\
                                </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                                    <td align="center" >' + (s.no || '-') + '</td>\
                                    <td align="center" >' + s.code + '</td>\
                                    <td align="left">' + s.name + '</td>\
                                    <td align="center">' + (s.nick || '') + '</td>\
                                    <td align="center">' + s.date + '</td>\
                                </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });

                $('#reporttype4 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                });

                $("body").mLoading('hide');

            });
        }

        function renderReport13(searchData) {

            $("#divprint").html("");
            //var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType3(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype13 .nav');
                var $content = $('#reporttype13 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t13-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + ' ">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t13-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                                    <tr class="thisrow">\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>\
                                        <th style="height: 30px;  min-width: 150px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></th>\
                                        <th style="height: 30px;  min-width: 300px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></th>\
                                        <th style="height: 30px;  min-width: 150px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %></th>\
                                        <th style="height: 30px;  min-width: 150px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></th>\
                                        <th style="height: 30px;  min-width: 100px !important;" class="" align="center">อายุ(ปี)</th>\
                                        <th style="height: 30px;  min-width: 100px !important;" class="" align="center">อายุ(เดือน)</th>\
                                    </tr>\
                                </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                                    <td align="center" >' + (s.no ?? '-') + '</td>\
                                    <td align="center" >' + s.code + '</td>\
                                    <td align="left">' + s.name + '</td>\
                                    <td align="center">' + (s.nick ?? '') + '</td>\
                                    <td align="center">' + (s.date ?? '') + '</td>\
                                    <td align="center">' + (s.ageYear ?? '') + '</td>\
                                    <td align="center">' + (s.ageMonth ?? '') + '</td>\
                                </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });

                $('#reporttype13 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                });

                $("body").mLoading('hide');

            });
        }

        function renderReport5(searchData) {

            $("#divprint").html("");
            //var dt = new Date();
            $("body").mLoading();
            PageMethods.SearchReportsType3(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype5 .nav');
                var $content = $('#reporttype5 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t5-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + ' ">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t5-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                                    <tr class="thisrow">\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>\
                                        <th style="height: 30px; min-width: 150px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></th>\
                                        <th style="height: 30px; min-width: 300px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>\
                                        <th style="height: 30px; min-width: 300px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>\
                                        <th style="height: 30px; min-width: 100px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %></th>\
                                    </tr>\
                                </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                                    <td align="center" >' + (s.no || '-') + '</td>\
                                    <td align="center" >' + s.code + '</td>\
                                    <td align="left">' + s.title + ' ' + s.fname + '</td>\
                                    <td align="left">' + s.lname + '</td>\
                                    <td align="center">' + s.nick + '</td>\
                                </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });

                $('#reporttype5 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                });

                $("body").mLoading('hide');

            });


        }

        function renderReport6(searchData) {

            $("#divprint").html("");
            var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType6(searchData, function (result) {

                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype6 .nav');
                var $content = $('#reporttype6 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t6-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + ' ">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t6-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                                    <tr class="thisrow">\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                                        <th style="height: 30px; min-width: 150px !important;" class="" align="center" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></th >\
                                        <th style="height: 30px; min-width: 300px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></th>\
                                        <th style="height: 30px; min-width: 200px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %></th>\
                                        <th style="height: 30px; min-width: 300px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %>)</th>\
                                        <th style="height: 30px; min-width: 200px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %>)</th>\
                                        <th style="height: 30px; min-width: 300px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %>)</th>\
                                        <th style="height: 30px; min-width: 200px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %>)</th>\
                                        <th style="height: 30px; min-width: 300px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %>)</th>\
                                        <th style="height: 30px; min-width: 200px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %>)</th>\
                                    </tr>\
                                </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                                    <td align="center" >' + (s.no || '-') + '</td>\
                                    <td align="center" >' + s.code + '</td>\
                                    <td align="left">' + s.name + '</td>\
                                    <td align="center">' + s.id + '</td>\
                                    <td align="center">' + s.mother + '</td>\
                                    <td align="center">' + s.motherid + '</td>\
                                    <td align="center">' + s.father + '</td>\
                                    <td align="center">' + s.fatherid + '</td>\
                                    <td align="center">' + s.family + '</td>\
                                    <td align="center">' + s.familyid + '</td>\
                                </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });


                $('#reporttype6 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                });

                $("body").mLoading('hide');

            });

        }

        function renderReport7(searchData) {

            $("#divprint").html("");
            //var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType3(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype7 .nav');
                var $content = $('#reporttype7 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t7-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + ' ">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t7-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                                    <tr class="thisrow">\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>\
                                        <th style="height: 30px; min-width: 150px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></th>\
                                        <th style="height: 30px; min-width: 300px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></th>\
                                        <th style="height: 30px; min-width: 200px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %></th>\
                                    </tr>\
                                </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                                    <td align="center" >' + (s.no || '-') + '</td>\
                                    <td align="center" >' + s.code + '</td>\
                                    <td align="left">' + s.name + '</td>\
                                    <td align="center">' + s.id + '</td>\
                                </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });

                $('#reporttype7 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                });

                $("body").mLoading('hide');
            });



        }

        function renderReport8(searchData) {

            $("#divprint").html("");
            var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType3(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype8 .nav');
                var $content = $('#reporttype8 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t8-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + ' ">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t8-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                                    <tr class="thisrow">\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>\
                                        <th style="height: 30px; min-width: 150px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></th>\
                                        <th style="height: 30px; min-width: 300px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></th>\
                                    </tr>\
                                </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                                    <td align="center" >' + (s.no || '-') + '</td>\
                                    <td align="center" >' + s.code + '</td>\
                                    <td align="left">' + s.name + '</td>\
                                </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });

                $('#reporttype8 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                });

                $("body").mLoading('hide');

            });



        }

        function renderReport9(searchData) {

            $("#divprint").html("");
            var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType9(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype9 .nav');
                var $content = $('#reporttype9 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t9-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + ' ">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t9-room' + (i + 1) + '">';
                    //_content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    //_content += '<thead class="myHead">\
                    //                <tr class="thisrow">\
                    //                    <td style="height: 30px; width: 5%" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></td>\
                    //                    <td style="height: 30px; width: 15%" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></td>\
                    //                    <td style="height: 30px; max-width: 20%" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></td>\
                    //                </tr>\
                    //            </thead>';
                    //_content += '<tbody class="myTable">';
                    _content += '<div class="row">';

                    $.each(r.students, function (j, s) {
                        let pictureUrl = s.picture||"";
                        if (pictureUrl.indexOf("?x-image-process=image/resize,m_fill,h_300,w_270") != -1) {
                            pictureUrl = pictureUrl + '&v=<%= DateTime.Now.Ticks %>';
                        } else {
                            pictureUrl = pictureUrl + '?v=<%= DateTime.Now.Ticks %>';
                        }

                        _content += '<div class="col-md-3">\
                                        <div class="panel-body  text-center img-container" ><img  src="'+ pictureUrl + '"></div>\
                                        <div class="panel-footer">\
                                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>: '+ s.code + '</p>\
                                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>: '+ s.name + '</p>\
                                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %>: '+ (s.id || "-") + '</p>\
                                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %>: '+ s.date + '</p>\
                                        </div >\
                                    </div>';
                    });

                    _content += '</div>';
                    //$.each(r.students, function (j, s) {
                    //    _content += '<tr><td align="center">' + (j + 1) + '</td>\
                    //                <td align="center" >' + s.code + '</td>\
                    //                <td align="left">' + s.name + '</td>\
                    //            </tr>';
                    //});
                    //_content += '</tbody>';
                    //_content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });

                $('#reporttype9 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                });

                $("body").mLoading('hide');

            });


        }

        function renderReport10(searchData) {

            $("#divprint").html("");
            //var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType10(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype10 .nav');
                var $content = $('#reporttype10 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t10-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + ' ">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t10-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                                    <tr class="thisrow">\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>\
                                        <th style="height: 30px; min-width: 150px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></th>\
                                        <th style="height: 30px; min-width: 300px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></th>\
                                        <th style="height: 30px; min-width: 200px !important;" class="" align="center"><a  target="_blank" href="<%= Page.ResolveUrl("~/Report/Print/StudentPaper.aspx") %>?mode=1&type=1&lvl1='+ r.lvl1id + '&lvl2=' + r.lvl2id + '&term=' + YTLCF.GetTermID() + '" class="btn btn-sm btn-success btn-a" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133216") %></button></th>\
                                    </tr>\
                                </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                                    <td align="center" >' + (s.no || '-') + '</td>\
                                    <td align="center" >' + s.code + '</td>\
                                    <td align="left">' + s.name + '</td>\
                                    <td align="center"><a target="_blank" href="<%= Page.ResolveUrl("~/Report/Print/StudentPaper.aspx") %>?mode=1&type=2&sid='+ s.sid + '&term=' + YTLCF.GetTermID() + '" class="btn btn-sm btn-success btn-a" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405041") %></button></td>\
                                </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });

                $('#reporttype10 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                });

                $("body").mLoading('hide');

            });
        }

        function renderReport11(searchData) {

            $("#divprint").html("");
            //var dt = new Date();

            $("body").mLoading();
            PageMethods.SearchReportsType10(searchData, function (result) {
                //console.log(result);
                var sc = result.school;
                var info = result.info;

                var $nav = $('#reporttype11 .nav');
                var $content = $('#reporttype11 .tab-content');

                $nav.html('');
                $content.html('');

                var tab = 1;
                $.each(info, function (i, a) {
                    //var i = 1;
                    var r = a.room;

                    var _nav = "";
                    var _content = "";

                    _nav += '<li class="nav-item ' + (i == 0 ? " active " : "") + '"><a data-toggle="pill" href= "#t11-room' + (i + 1) + '" class="nav-link ' + (i == 0 ? " active " : "") + ' ">' + (r.level1 + '/' + r.level2) + '</a></li>';
                    _content += '<div class="tab-pane ' + (i == 0 ? " active " : "") + '" id="t11-room' + (i + 1) + '">';
                    _content += '<table class="table table-bordered table-hover dataTable" cellspacing="0" width="100%">';
                    _content += '<thead class="myHead">\
                                    <tr class="thisrow">\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>\
                                        <th style="height: 30px; " class="col-80px" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>\
                                        <th style="height: 30px; min-width: 150px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></th>\
                                        <th style="height: 30px; min-width: 300px !important;" class="" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></th>\
                                        <th style="height: 30px; min-width: 200px !important;" class="" align="center"><a  target="_blank" href="<%= Page.ResolveUrl("~/Report/Print/StudentPaper.aspx") %>?mode=2&type=1&lvl1='+ r.lvl1id + '&lvl2=' + r.lvl2id + '&term=' + YTLCF.GetTermID() + '" class="btn btn-sm btn-success btn-a" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133216") %></button></th>\
                                    </tr>\
                                </thead>';
                    _content += '<tbody class="myTable">';

                    $.each(r.students, function (j, s) {
                        _content += '<tr><td align="center">' + (j + 1) + '</td>\
                                    <td align="center" >' + (s.no || '-') + '</td>\
                                    <td align="center" >' + s.code + '</td>\
                                    <td align="left">' + s.name + '</td>\
                                    <td align="center"><a target="_blank" href="<%= Page.ResolveUrl("~/Report/Print/StudentPaper.aspx") %>?mode=2&type=2&sid='+ s.sid + '&term=' + YTLCF.GetTermID() + '" class="btn btn-sm btn-success btn-a" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405041") %></button></td>\
                                </tr>';
                    });
                    _content += '</tbody>';
                    _content += '</table>';
                    _content += "</div>";

                    $nav.append(_nav);
                    $content.append(_content);

                });

                $('#reporttype11 .table').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                });

                $("body").mLoading('hide');
            });



        }

        function Export(rtype) {

            var type = $("#DropDownListType").val();

            if (rtype == 'pdf') {
                switch (type) {

                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '12':
                    case '13':
                    case '14':
                    case '15':
                    case '16':
                        break;

                    default:
                        return;
                }
            }

            level = YTLCF.GetLevelID();//$('select[id*=ddlsublevel]').val();
            level2 = YTLCF.GetClassID();//$('select[id*=ddlSubLV2]').val();
            year_Id = YTLCF.GetYearID();// $('select[id*=ddlyear]').val();
            year_no = YTLCF.GetYearNo();//$('select[id*=ddlyear] :selected').text()
            term_id = YTLCF.GetTermID();//$('select[id*=semister]').val();
            studentname = SAC.GetStudentName();//$("#txtname").val();



            //if ($('select[id*=ddlyear]').val() == "") {
            //    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133144") %>");
            //    location.reload();
            //}
            //else if ($('select[id*=semister]').val() == "") {
            //    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133143") %>");
            //    location.reload();
            //}
            //else {

            //}

            var sort = {
                "level": level,
                "level2": level2,
                "year_Id": year_Id,
                "year": year_no,
                "term_id": term_id,
                "studentname": studentname,
            };

            searchData = sort;
            //console.log(searchData);
            var url = "Handles/ReportStudent_Handler.ashx?rtype=" + rtype + "&year=" + searchData.year + "&term=" + searchData.term_id + "&lvl1=" + searchData.level + "&lvl2=" + (searchData.level2 || "") + "&name=" + searchData.studentname + "&type=" + type + '&status=' + $('#sltStudentStatus').val().join(',');

            if ($('.input-dynamic:visible').length > 0) {

                var arrCol = $('.input-dynamic:visible').map(function () { return $(this).val(); }).get();
                url += "&cols=" + encodeURIComponent(arrCol.join());
            }

            window.open(url);


        }

        function s2ab(s) {
            const buf = new ArrayBuffer(s.length);
            const view = new Uint8Array(buf);
            for (var i = 0; i != s.length; ++i) view[i] = s.charCodeAt(i) & 0xFF;
            return buf;
        }

        function addCol(no) {
            var $th = $('.report-wrapper:visible .table thead tr.thisrow');
            var $tr = $('.report-wrapper:visible .table tbody tr');

            if ($th.length > 0) {
                $th.each(function () {
                    $(this).children("td.-manual").remove();
                });

                $th.each(function () {
                    for (var i = 0; i < no; i++) {
                        $(this).append('<td rowspan=' + $th.length + ' class="-manual" align="center" style="width: 150px"><span class="span-dynamic hidden"></span><input type="text" class="input-dynamic" style="width: 150px;" autocomplete="on"/></td>');
                    }
                });
            }

            if ($tr.length > 0) {
                $tr.each(function () {
                    $(this).children("td.-manual").remove();
                });

                $tr.each(function () {
                    for (var i = 0; i < no; i++) {
                        $(this).append('<td class="-manual"></td>');
                    }
                });

                $('.table:visible').DataTable({
                    paging: false,
                    searching: false,
                    info: false,
                    "bSort": true,
                    "autoWidth": false,
                });
            }

        }

        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><style>input{display:none !important;}.myTable td{ mso-number-format:\\@;}</style>{table}</body></html>'
                , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

            return function (table, name, filename) {
                if (!table.nodeType) table = document.getElementById(table)
                //var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                var ctx1 = { worksheet: 'Worksheet1', table: table.innerHTML }
                var ctx2 = { worksheet: 'Worksheet2', table: table.innerHTML }
                // window.location.href = uri + base64(format(template, ctx))

                var today = new Date();
                var date = ('0' + today.getDate()).slice(-2) + "-" + ('0' + (today.getMonth() + 1)).slice(-2) + "-" + today.getFullYear();

                var link = document.createElement("a");
                link.download = filename + '_' + date + ".xls";
                link.href = uri + base64(format(template, ctx1) + format(template, ctx2));
                link.click();
            }
        })()



        function getliststudent() {

        }


    </script>



</asp:Content>

