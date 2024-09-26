<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AmountStudent_Report.aspx.cs" MasterPageFile="~/Material2.Master" Inherits="FingerprintPayment.Report.AmountStudent_Report" %>

<%@ Register Src="~/UserControls/YTLCFilter.ascx" TagPrefix="uc1" TagName="YTLCFilter" %>
<%@ Register Src="~/UserControls/YTFilter.ascx" TagPrefix="uc1" TagName="YTFilter" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <%--    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>--%>

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link href="../Styles/SettingDialog.css" rel="stylesheet" />

    <style>
        .--level {
            /*visibility:hidden;*/
            display: none;
        }

        a.disabled {
            color: #c5c5c5 !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">

    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>

    <script src="../bootstrap SB2/bower_components/flot/excanvas.min.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot/jquery.flot.time.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js" type="text/javascript"></script>

    <script src="ScriptReport.js" type="text/javascript"></script>
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script src="../Scripts/FileSaver.js" type="text/javascript"></script>

    <script src="Script/RP_AmountStudent.js?v=<%=DateTime.Now.ToString("ddMMyyyyHH") %>" type="text/javascript"></script>



    <script type="text/javascript">

        var RP_AmountStudent = new RPAmountStudent();
        var searchData = [];
        var yEar = "";
        var tErm = "";
        var sUbLV = "";
        var levelID = "";
        var sOrt_tYpe = "";

        function SearchData() {

            if ($('#aspnetForm').valid() == false) {
                //e.preventDefault();
                //e.stopPropagation();
                return false;
            }

            yEar = YTLCF.GetYearID();
            tErm = YTLCF.GetTermID();
            //sUbLV = YTLCF.GetLevelID();
            levelID = YTLCF.GetLevelID().toString();
            sOrt_tYpe = $("#sort_type").val();

            var data = {
                "sOrt_tYpe": sOrt_tYpe,
                "yEar_Id": yEar,
                // "sUbLV_Id": sUbLV,
                "tErm_Id": tErm,
                "LevelID": levelID,
            };

            searchData = data;
            $("body").mLoading();
            PageMethods.report_AmountStudent(data, function (e) {
                //console.log(e.data);
                RP_AmountStudent.report_AmountStudent = e;
                RP_AmountStudent.RenderHtml_AmountStudent("example", false);
                $("body").mLoading('hide');
            });
        }

        $(function () {
            //$('select[id*=ddlyear]').change(function () {
            //    GetListTrem();
            //});

            $('#sort_type').change(function () {

                var _val = $(this).val();

                switch (_val) {
                    case '11':
                    case '12':
                    case '13':
                        $('#menu1-item1').removeClass('disabled');
                        break;

                    default:
                        $('#menu1-item1').addClass('disabled');
                        break;
                }

            });

            if (jQuery.validator) {

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",

                });

                $("#aspnetForm").validate({  // initialize the plugin
                    errorPlacement: function (error, element) {
                        var _class = element.attr('class');

                        if (_class.includes('--req-append-last')) {
                            error.insertAfter(element.parent());
                        }
                        else
                            error.insertAfter(element);
                    }

                });
            }
        });

        //function GetListTrem() {
        //    var YearID = $('#ctl00_MainContent_ddlyear option:selected').val();
        //    $('select[id*=semister] option').remove();
        //    $.ajax({
        //        url: "/App_Logic/dataGeneric.ashx?mode=listterm&id=" + YearID,
        //        success: function (msg) {
        //            $.each(msg, function (index) {
        //                $('select[id*=semister]')
        //                    .append($("<option></option>")
        //                        .attr("value", msg[index].nTerm)
        //                        .text(msg[index].sTerm)
        //                    );
        //            });
        //        }
        //    });
        //}

        function Export(rtype) {

            if ($('#aspnetForm').valid() == false) {
                //e.preventDefault();
                //e.stopPropagation();
                return false;
            }

            var type = $("#sort_type").val();

            if (rtype == 'pdf') {
                switch (type) {

                    case '11':
                    case '12':
                    case '13':
                        break;

                    default:
                        return;
                }
            }

            //$("body").mLoading();


            //if ($('select[id*=ddlyear]').val() == "") {
            //    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133144") %>");
            //    location.reload();
            //}
            //else if ($('select[id*=semister]').val() == "") {
            //    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133143") %>");
            //    location.reload();
            //}
            //else {
            //    //var sort = {
            //    //    "level": level,
            //    //    "level2": level2,
            //    //    "year_Id": year_Id,
            //    //    "year": year_no,
            //    //    "term_id": term_id,
            //    //    "studentname": studentname,
            //    //};
            //}
            //searchData = sort;
            //console.log(searchData);

            var lvl1 = YTLCF.GetLevelID().toString();
            var year_Id = YTLCF.GetYearID();
            var year_no = YTLCF.GetYearNo();
            var term_id = YTLCF.GetTermID();

            var url = "Handles/AmountStudent_Report.ashx?rtype=" + rtype + "&year=" + year_Id + "&term=" + term_id + "&lvl1=" + lvl1 + "&type=" + type;

            window.open(url);

        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104054") %>
            </p>
        </div>
    </div>

    <form runat="server" id="form1">
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

                        <%--     <uc1:YTFilter runat="server" ID="YTFilter"  />--%>
                        <uc1:YTLCFilter runat="server" ID="YTLCFilter" IsRequired="false" IsLevelMultiSelect="true" />

                        <%--   <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="ddlsublevel" runat="server" class="selectpicker --req-append-last" data-style="select-with-transition" data-width="100%" data-size="7" >
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"></label>
                            <div class="col-md-3 ">
                            </div>
                            <div class="col-md-2"></div>
                        </div>--%>


                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104055") %></label>
                            <div class="col-md-3 ">
                                <select id="sort_type" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <option value="11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01660") %></option>
                                    <option value="12"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01661") %></option>
                                    <option value="13"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01662") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01677") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01676") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01697") %></option>
                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01696") %></option>
                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01698") %></option>
                                    <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01675") %></option>
                                    <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01678") %></option>
                                    <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01667") %></option>
                                    <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01669") %></option>
                                    <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01670") %></option>
                                </select>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"></label>
                            <div class="col-md-3 ">
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 text-right">

                                <button type="button" onclick="SearchData();" class="btn btn-fill btn-info">
                                    <span class="material-icons">search</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                            </div>
                            <div class="col-md-6 text-left">
                                <div class="dropdown">
                                    <button class="btn btn-fill btn-success dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                        Export
    <span class="caret"></span>
                                    </button>
                                    <ul id="ddlMenu1" class="dropdown-menu" aria-labelledby="dropdownMenu1">
                                        <li><a id="menu1-item1" href="#" onclick="Export('pdf');" class="">PDF</a></li>
                                        <li><a href="#" onclick="RP_AmountStudent.export_excel()">EXCEL</a></li>
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
                        <div class="row">
                            <div class="col-md-12 text-right">
                                * <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01569") %>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 ">
                                <table id="example" class="table-hover dataTable " cellspacing="0" width="100%">
                                </table>
                            </div>

                            <fieldset class="d-none" id="export_excel">
                                <table id="table_exports" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                                </table>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

</asp:Content>


