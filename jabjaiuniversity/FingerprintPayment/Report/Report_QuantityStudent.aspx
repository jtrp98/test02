﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_QuantityStudent.aspx.cs" MasterPageFile="~/mp.Master" Inherits="FingerprintPayment.Report.Report_QuantityStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

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

    <link href="../Styles/SettingDialog.css" rel="stylesheet" />
    <script src="ScriptReport.js" type="text/javascript"></script>
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script src="../Scripts/FileSaver.js" type="text/javascript"></script>

    <script src="Script/RP_QuantityStudent.js" type="text/javascript"></script>

    <style type="text/css">
        @media (max-width: 999px) {
            .report-container {
                font-size: 18px;
            }

            label {
                font-weight: normal;
                font-size: 18px;
            }

            legend {
                padding-left: 30px;
                font-size: 18px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 18px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }

            .table-show-result {
                font-size: 20px;
            }
        }

        @media (min-width: 1000px) and (max-width: 1199px) {
            .report-container {
                font-size: 22px;
            }

            label {
                font-weight: normal;
                font-size: 22px;
            }

            legend {
                padding-left: 30px;
                font-size: 22px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 22px;
                width: 100%;
                padding-left: 30px;
                padding-right: 30px;
            }

            .table-show-result {
                font-size: 22px;
            }
        }

        @media (min-width: 1200px) {
            .report-container {
                font-size: 26px;
            }

            label {
                font-weight: normal;
                font-size: 26px;
            }

            legend {
                padding-left: 30px;
                font-size: 26px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 26px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }

            .table-show-result {
                font-size: 26px;
            }
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .centerText {
            text-align: center;
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }

        @media (min-width: 1300px) {
            #page-wrapper {
                position: inherit;
                margin: 0 0 0 250px;
                padding: 0 30px;
                border-left: 1px solid #e7e7e7;
                background-color: #eee;
                padding-top: 30px;
                padding-bottom: 30px;
                min-height: 900px;
            }
        }

        .header_01 {
            min-width: 100px;
        }

        .header_02 {
            min-width: 50px;
        }

        .btn_red.active {
            background-color: #337AB7;
        }

        .btn_red {
            min-width: 70px;
        }

        table.centerText {
            text-align: center;
        }
    </style>

    <script type="text/javascript">

        var RP_QuantityStudent = new RPQuantityStudent();

        var yEar = "";
        var tErm = "";
        var sUbLV = "";

        $(function () {
            $('select[id*=ddlyear]').change(function () {
                GetListTrem();
            });
        });

        function SearchData() {
            yEar = $('select[id*=ddlyear').val();
            tErm = $('select[id*=semister').val();
            sUbLV = $('select[id*=ddlsublevel').val();

            var data = {
                "yEar_Id": yEar,
                "tErm_Id": tErm,
                "sUbLV_Id": sUbLV,
                "sOrt_tYpe": $("#sort_type").val()
            };

            $("body").mLoading();

            PageMethods.Report_Quantity_ClassRoom(data, function (e) {
                console.log(e.data);
                RP_QuantityStudent.Report_Quantity_ClassRoom = e;
                RP_QuantityStudent.RenderHtml_Quantity_ClassRoom("example", false);
            });

        }

        function GetListTrem() {
            var YearID = $('#ctl00_MainContent_ddlyear option:selected').val();
            $('select[id*=semister] option').remove();
            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listterm&id=" + YearID,
                success: function (msg) {
                    $.each(msg, function (index) {
                        $('select[id*=semister]')
                            .append($("<option></option>")
                                .attr("value", msg[index].nTerm)
                                .text(msg[index].sTerm)
                            );
                    });
                }
            });
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />
    <asp:HiddenField ID="hdfschoolname" runat="server" />

    <div class="full-card box-content">
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlyear" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-3 col-sm-6 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="semister" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="row hidden">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlsublevel" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106065") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <select id="sort_type" class="form-control">
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105043") %></option>
                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105044") %></option>
                    </select>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-sm-12">
                <input type="button" class="btn btn-primary btn-block" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="SearchData();" />
            </div>
        </div>

        <div class="row--space">
        </div>

        <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %> </div>
            <div class="col-lg-8 col-md-8 col-sm-8"></div>
            <div class="col-lg-2 col-md-2 col-sm-2">
                <div class="dropdown">
                    <div class="btn btn-success button-custom" data-toggle="dropdown">Export File</div>
                    <ul class="dropdown-menu">
                        <li>
                            <a class="btn" id="exportfile" onclick="RP_QuantityStudent.export_excel()">
                                <h3><i class="fa  fa-file-excel-o"></i>&nbsp Export Excel</h3>
                            </a>
                        </li>
                        <%--<li>
                            <a class="btn" href="#">
                                <h3><i class="fa  fa-file-pdf-o"></i>&nbsp Export PDF</h3>
                            </a>
                        </li>--%>
                    </ul>
                </div>
            </div>
        </div>


        <asp:Literal ID="ltrHeaderReport" runat="server" />

        <div class="row border-bottom">
            <br />
            <div class="col-sm-12">
                <fieldset>
                    <asp:ListView ID="lvReport" runat="server"></asp:ListView>
                    <table id="example" class="table table-condensed table-bordered table-show-result" cellspacing="0" width="100%">
                    </table>
                </fieldset>
            </div>
        </div>

    </div>

    <fieldset class="hidden" id="export_excel">
        <table id="table_exports" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
        </table>
    </fieldset>
    <iframe id="txtArea1" style="display: none"></iframe>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
