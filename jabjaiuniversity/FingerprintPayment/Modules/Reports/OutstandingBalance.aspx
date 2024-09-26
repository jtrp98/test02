<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Material2.Master" CodeBehind="OutstandingBalance.aspx.cs" Inherits="FingerprintPayment.Modules.Reports.OutstandingBalance" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02720") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132546") %> 
            </p>
        </div>
    </div>

    <div class="employeeList row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">search</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                </div>

                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltYear" name="sltYear[]"
                                class="selectpicker col-sm-12" data-style="select-with-transition">
                                <asp:Literal ID="ltrYear" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltTerm" name="sltTerm[]"
                                class="selectpicker col-sm-12" data-style="select-with-transition" required="required">
                                <asp:Literal ID="ltrTerm" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>

                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltLevel" name="sltLevel[]"
                                class="selectpicker col-sm-12" data-style="select-with-transition">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                <asp:Literal ID="ltrLevel" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltClass" name="sltClass"
                                class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>" required="required">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>

                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltStudentCode" class="col-sm-12 selectpicker" data-style="select-with-transition" data-live-search="true">
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"></label>
                        <div class="col-sm-3 div-select-input">
                        </div>
                        <div class="col-sm-2"></div>
                    </div>

                    <div class="row">
                        <div class="col-sm-1"></div>
                        <div class="col-sm-2 div-select-input"></div>
                        <div class="col-sm-4 div-select-input">
                            <button type="button" class="btn btn-primary btn-sm" style="font-size: 20px" id="btnAdvSearch" onclick="GetReportsData()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
                            <button type="button" class="btn btn-success btn-sm" style="font-size: 20px" id="btnExport" onclick="export_excel();">Export Excel</button>
                        </div>
                        <div class="col-sm-3 left">
                            <button type="button" class="btn btn-success btn-sm" style="font-size: 20px" id="btnExportPDF" onclick="export_PDF();">Export PDF</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="mypanel"></div>

    <script type="text/template" id="table-template">
        {{#.}}
        <div class="employeeList row">
            <div class="col-md-12">
                <div class="card">
                    {{{HeaderReport}}}                

                    <div class="card-body">

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></label>
                            <div class="col-sm-3 col-form-label text-left">
                                : {{sStudentID}}
                            </div>
                            <div class="col-sm-1 col-form-label"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3 div-select-input">
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                            <div class="col-sm-3 col-form-label text-left">
                                : {{StudentName}}
                            </div>
                            <div class="col-sm-1 col-form-label"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3 div-select-input">
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                            <div class="col-sm-3 col-form-label text-left">
                                : {{SubLevel}} / {{nTSubLevel2}}
                            </div>
                            <div class="col-sm-1 col-form-label"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3 div-select-input">
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02878") %></th>
                                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501020") %></th>
                                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01615") %></th>
                                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501008") %></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {{Renew}}
                                        {{#term_Datas}}
                                        <tr>
                                            <td rowspan='{{TermRowsNumber}}'>{{TermYear}}/{{Term}}</td>
                                            {{#invoices_Data}}
                                            <td rowspan="{{InvoicesRowsNumber}}" class="text-center">{{Invoices_Code}}</td>
                                            {{#invoices_Items}}
                                            {{#sPayment}}
                                            <td class="text-center">{{idx}}</td>
                                            {{/sPayment}}
                                            <td>{{AccountingId}} {{sPayment}}</td>
                                            <td class="text-right">{{#format}}{{GrandTotal}}{{/format}}</td>
                                        </tr>
                                        <tr>
                                            {{/invoices_Items}}
                                            {{/invoices_Data}}
                                        </tr>
                                        {{/term_Datas}}
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td colspan="4" class="text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></td>
                                            <td class="text-right">{{#format}}{{Fb_GrandTotal}}{{/format}}</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" class="text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503017") %></td>
                                            <td class="text-right">{{#format}}{{Discount}}{{/format}}</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" class="text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132522") %></td>
                                            <td class="text-right">{{#format}}{{Fb_PaymentAmount}}{{/format}}</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" class="text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132523") %></td>
                                            <td class="text-right">{{#format}}{{Fb_OutstandingAmount}}{{/format}}</td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        {{/.}}
    </script>

    <page size="A4" id="page1" style="display: none;">

        <div class="form-group">
            <div class="text-right" style="padding: 15px 15px 0 0" id="full-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132053") %></div>
            <div class="col-md-8">
                <div class="col-md-12">
                    <ul style="padding: 0">
                        <li style="list-style: none; height: 22px">
                            <h3 style="margin: 0">
                                <img src="" style="width: 50px; height: 50px" />
                        </li>
                        <li style="list-style: none; height: 22px; padding: 0px 0 0 50px">
                            <p style="font-size: 14px">
                                <br />
                            </p>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="col-md-4 text-right" style="font-size: 18px">
                <ul>
                    <li style="list-style: none; height: 27px">
                    <li style="list-style: none; height: 27px">
                </ul>
            </div>
        </div>
        <div class="form-group" style="padding-top: 10px; margin: 0">
            <p class="text-center" style="font-size: 18px; margin: 0"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603028") %></b></p>
        </div>
        <div class="form-group" style="padding: 0px 0 0 15px">
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Name-Surname</b></p>
            </div>
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Identification Number</b></p>
            </div>
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Student Number</b></p>
            </div>
        </div>
        <div class="form-group" style="padding: 20px 0 0 15px">
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Academic Year</b></p>
            </div>
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Semester</b></p>
            </div>
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Class</b></p>
            </div>
        </div>

        <div class="form-group" style="padding: 35px 20px 0 20px">
            <table style="width: 100%" id="tb-products-1">
                <thead></thead>
                <tbody></tbody>
            </table>
        </div>
        <div class="form-group col-md-12">
            <div class="col-md-7" style="font-size: 18px">
                <ul>
                    <li class="text-left" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px;">____________________________________</p>
                    </li>
                    <li class="text-left" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px;" class="employees-name-3">(<span style="color: white;">___________________________________</span>)</p>
                    </li>
                </ul>
            </div>
            <div class="col-md-5 text-right" style="font-size: 22px; margin: 0 0 0 0;">
                <ul>
                    <li class="text-center" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %>____________________________________</p>
                    </li>
                    <li class="text-center" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px;" class="employees-name-2"></p>
                    </li>
                    <li class="text-center" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603031") %></b></p>
                    </li>
                    <li class="text-center" style="list-style: none;" name="show_type_1">
                        <p style="margin: 0; font-size: 16px; border-bottom: 1px solid black;" class="employees-name-1"></p>
                    </li>
                    <li class="text-center" style="list-style: none;" name="show_type_1">
                        <p style="margin: 0; font-size: 16px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603031") %></b></p>
                    </li>
                </ul>
            </div>
        </div>

        <div class="form-group col-md-12" style="padding-top: 10px; padding-left: 0px;">
            <hr style="width: 21cm; margin: 0; border-top: 2px dotted #a9a9a9;" size="10" />
        </div>
        <div id="page2">
            <div class="text-right" style="padding: 10px 15px 0 0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132515") %></div>
            <div class="form-group">
                <div class="col-md-8">
                    <div class="col-md-12">
                        <ul style="padding: 0">
                            <li style="list-style: none; height: 22px">
                                <h3 style="margin: 0"></h3>
                            </li>
                            <li style="list-style: none; height: 22px; padding: 0px 0 0 50px">
                                <p style="font-size: 14px">
                                    &nbsp;
                                </p>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-md-4 text-right" style="font-size: 18px">
                    <ul>
                        <li style="list-style: none; height: 27px">
                        <li style="list-style: none; height: 27px">
                    </ul>
                </div>
            </div>

            <div class="form-group" style="padding-top: 10px; margin: 0">
                <p class="text-center" style="font-size: 18px; margin: 0"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603028") %></b></p>
            </div>
            <div class="form-group" style="padding: 0px 0 0 15px">
                <div class="col-md-4">
                    <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Name-Surname</b></p>
                </div>
                <div class="col-md-4">
                    <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Identification Number</b></p>
                </div>
                <div class="col-md-4">
                    <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Student Number</b></p>
                </div>
            </div>
            <div class="form-group" style="padding: 20px 0 0 15px">
                <div class="col-md-4">
                    <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Academic Year</b></p>
                </div>
                <div class="col-md-4">
                    <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Semester</b></p>
                </div>
                <div class="col-md-4">
                    <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Class</b></p>
                </div>
            </div>
            <div class="form-group" style="padding: 25px 20px 0 20px">
                <table style="width: 100%" id="tb-products-2">
                    <thead></thead>
                    <tbody></tbody>
                </table>
            </div>

            <div class="form-group col-md-12">
                <div class="col-md-7" style="font-size: 18px">
                    <ul>
                        <li class="text-left" style="list-style: none;" name="show_type_2">
                            <p style="margin: 0; font-size: 16px;">____________________________________</p>
                        </li>
                        <li class="text-left" style="list-style: none;" name="show_type_2">
                            <p style="margin: 0; font-size: 16px;" class="employees-name-3">(<span style="color: white;">___________________________________</span>)</p>
                        </li>
                    </ul>
                </div>
                <div class="col-md-5 text-right" style="font-size: 22px; margin: 0 0 0 0;">
                    <ul>
                        <li class="text-center" style="list-style: none;" name="show_type_2">
                            <p style="margin: 0; font-size: 16px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %>____________________________________</p>
                        </li>
                        <li class="text-center" style="list-style: none;" name="show_type_2">
                            <p style="margin: 0; font-size: 16px;" class="employees-name-2"></p>
                        </li>
                        <li class="text-center" style="list-style: none;" name="show_type_2">
                            <p style="margin: 0; font-size: 16px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603031") %></b></p>
                        </li>
                        <li class="text-center" style="list-style: none;" name="show_type_1">
                            <p style="margin: 0; font-size: 16px; border-bottom: 1px solid black;" class="employees-name-1"></p>
                        </li>
                        <li class="text-center" style="list-style: none;" name="show_type_1">
                            <p style="margin: 0; font-size: 16px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603031") %></b></p>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </page>


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />

    <link href="../../Content/jquery-confirm.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-confirm.js"></script>

    <link rel="stylesheet" href="../../Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="../../Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <script src="/bootstrap%20SB2/bower_components/datatables/media/js/jquery.dataTables.min.js"></script>
    <script src="/Scripts/jquery.blockUI.js"></script>
    <script src="/Scripts/jquery-confirm.js"></script>
    <script src="/Scripts/jquery.serializeObject.js"></script>
    <script src="/Scripts/jquery.serializejson.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/numeral.js/2.0.6/numeral.min.js"></script>

    <style>
        page {
            background: white;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
            box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
        }

            page[size="A4"] {
                width: 21cm;
                height: 29.7cm;
            }

                page[size="A4"][layout="landscape"] {
                    width: 29.7cm;
                    height: 21cm;
                }

            page[size="A3"] {
                width: 29.7cm;
                height: 42cm;
            }

                page[size="A3"][layout="landscape"] {
                    width: 42cm;
                    height: 29.7cm;
                }

            page[size="A5"] {
                width: 14.8cm;
                height: 21cm;
            }

                page[size="A5"][layout="landscape"] {
                    width: 21cm;
                    height: 14.8cm;
                }

        .blue-font {
            color: #3498db
        }

        table tbody tr {
            line-height: 12px;
        }

        @media print {
            body, page {
                margin: 0;
                box-shadow: 0;
            }
        }

        .jabjai-sticky-toolbar {
            width: 120px !important;
        }
    </style>
    <link href="/Content/select2/select2.min.css" rel="stylesheet" />
    <script src="/Scripts/select2/select2.full.min.js"></script>
    <script src="/Scripts/moment.js"></script>
    <link href="/Content/custom/custom-datatable.css" rel="stylesheet" />
    <link href="/bootstrap%20SB2/bower_components/datatables/media/css/jquery.dataTables.css" rel="stylesheet" />
    <link href="/Content/custom/custom-datatable.css" rel="stylesheet" />
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />
    <script src="../../Scripts/mustache.js"></script>

    <link href="../../Scripts/ajax-bootstrap-select.css" rel="stylesheet" />
    <script src="../../Scripts/ajax-bootstrap-select.js"></script>
    <script src="../../Scripts/FileSaver.js" type="text/javascript"></script>
    <script src="../../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>

    <script>
        var idx = 1;

        $(function () {

            // Search
            $("#sltYear").change(function () {
                LoadTerm($(this).val(), '#sltTerm');
            });
            $("#sltLevel").change(function () {
                LoadTermSubLevel2($(this).val(), '#sltClass');
            });

            //$('#sltStudentCode').select2({
            //    ajax: {
            //        url: "/App_Logic/autoCompleteName.ashx",
            //        data: function (params) {
            //            var query = {
            //                wording: params.term,
            //                mode: 'ListStudent'
            //            }

            //            // Query parameters will be ?search=[term]&type=public
            //            return query;
            //        },
            //        processResults: function (data) {
            //            return {
            //                results: data
            //            };
            //        },
            //    }
            //});

            $('#sltStudentCode').selectpicker().ajaxSelectPicker({
                ajax: {
                    // data source
                    url: "/App_Logic/autoCompleteName.ashx",
                    // ajax type
                    type: 'GET',
                    // data type
                    dataType: 'json',
                    // Use "{{{q}}}" as a placeholder and Ajax Bootstrap Select will
                    // automatically replace it with the value of the search query.
                    data: {
                        wording: '{{{q}}}',
                        termID: $("[id*=sltTerm]").val(),
                        mode: 'ListStudentTrem'
                    }
                },
                locale: {
                    emptyTitle: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00258") %>'
                },
                // function to preprocess JSON data
                preprocessData: function (data) {
                    var contacts = [];
                    $.each(data, function (index, values) {
                        contacts.push(
                            {
                                'value': values.User_Id,
                                'text': values.User_Name,
                                'data': {
                                    'icon': 'icon-person',
                                    //'subtext': 'Internal'
                                },
                                'disabled': false
                            }
                        );
                    });

                    return contacts;
                },
                preserveSelected: false
            });
        })

        function GetReportsData() {
            let data = {
                "TermId": $("[id*=sltTerm]").val(), "SubLevel2Id": $("[id*=sltClass]").val(),
                "StudentCode": $("#sltStudentCode").val(), "YearId": $("[id*=sltYear]").val()
            };

            $.ajax({
                async: false,
                type: "POST",
                url: "/Modules/Reports/OutstandingBalance.aspx/ReturnList",
                data: JSON.stringify({ "search": data }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    // Show image container                
                },
                success: function (response) {
                    let template = $("#table-template").html();
                    let text = "";
                    let HeaderReport = true;
                    response.d.Data.idx = function () {
                        return idx++;
                    }

                    response.d.Data.Renew = function () {
                        idx = 1;
                    }

                    $.each(response.d.Data, function (e, s) {
                        let Discount = 0;
                        $.each(s.term_Datas, function (e1, s1) {
                            $.each(s1.invoices_Data, function (e2, s2) {
                                Discount += s2.Discount
                            });
                        });
                        s.Discount = Discount;
                    });

                    response.d.Data.format = function () {
                        return function (text, render) {
                            var result = render(text);
                            return Number(result).toLocaleString(0, { minimumFractionDigits: 2 });
                        }
                    }

                    response.d.Data.HeaderReport = function () {
                        if (HeaderReport == true) {
                            HeaderReport = false;
                            return `<div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503023") %></h4>
                    </div>`;
                        }
                        else return "";
                    }

                    text += Mustache.render(template, response.d.Data)

                    //$.each(response.d, function (e, s) {
                    //    text += Mustache.render(template, s)
                    //});

                    console.log(response.d);
                    $("#mypanel").html(text);
                },
                failure: function (response) {
                    console.log(response.d);
                },
                error: function (response) {
                    console.log(response.d);
                }
            });
        }

        function LoadTermSubLevel2(subLevelID, objResult) {
            if (subLevelID) {
                $("body").mLoading({
                    icon: "/scripts/blockUI/ProgressGreen.gif"
                });
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/StudentInfo/StudentList.aspx/LoadTermSubLevel2",
                    data: '{subLevelID: ' + subLevelID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        // Show image container
                    },
                    success: function (response) {
                        $("body").mLoading("hide");
                        var subLevel2 = response.d;

                        $(objResult).empty();

                        if (subLevel2.length > 0) {

                            var options = '';
                            $(subLevel2).each(function () {

                                options += '<option value="' + this.id + '">' + this.name + '</option>';

                            });

                            $(objResult).html(options);
                            $(objResult).selectpicker('refresh');
                        }
                        Swal.close();
                    },
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }

        function LoadTerm(yearID, objResult) {
            if (yearID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/StudentInfo/StudentList.aspx/LoadTerm",
                    data: '{yearID: ' + yearID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var yearData = response.d;

                        $(objResult).empty();

                        if (yearData.length > 0) {

                            var options = '';
                            //options += '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>';
                            $(yearData).each(function () {
                                options += '<option value="' + this.id + '">' + this.name + '</option>';
                            });

                            $(objResult).html(options);
                            $(objResult).selectpicker('refresh');
                        }
                        Swal.close();
                    },
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }

        function export_excel() {
            $("body").mLoading();
            var d = new Date();

            let datestring = ("0" + d.getDate()).slice(-2) + ("0" + (d.getMonth() + 1)).slice(-2) +
                d.getFullYear() + ("0" + d.getHours()).slice(-2) + ("0" + d.getMinutes()).slice(-2) + ("0" + d.getMilliseconds()).slice(-2);

            var json;
            var xhr;
            var searchData = {
                "TermId": $("[id*=sltTerm]").val(), "SubLevel2Id": $("[id*=sltClass]").val(),
                "StudentCode": $("#sltStudentCode").val(), "YearId": $("[id*=sltYear]").val()
            };
            var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132545") %> ' + datestring + '.xls';
            //this.RenderHtml_Detail('table_exports', true);
            json = JSON.stringify({ search: searchData });
            xhr = new XMLHttpRequest();
            xhr.open("POST", "/Modules/Reports/OutstandingBalance.aspx/ExportExcel", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                $("body").mLoading('hide');
            };
            xhr.send(json);

            var param = {
                "filename": file_name,
                "tabledata": $("#export_excel").html()
            };

        };

        function export_PDF() {
            let searchData = {
                "TermId": $("[id*=sltTerm]").val(), "SubLevel2Id": $("[id*=sltClass]").val(),
                "StudentCode": $("#sltStudentCode").val(), "YearId": $("[id*=sltYear]").val()
            };

            var d = new Date();

            let datestring = ("0" + d.getDate()).slice(-2) + ("0" + (d.getMonth() + 1)).slice(-2) +
                d.getFullYear() + ("0" + d.getHours()).slice(-2) + ("0" + d.getMinutes()).slice(-2) + ("0" + d.getMilliseconds()).slice(-2);

            if ($("#txtstart").val() === "") Search.dStart = (new Date().getMonth() + 1) + "/" + new Date().getDate() + "/" + new Date().getUTCFullYear()
            var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132545") %> ' + datestring + '.pdf';

            $("body").mLoading('show');
            var json = JSON.stringify({ search: searchData });
            var req = new XMLHttpRequest();
            xhr = new XMLHttpRequest();
            xhr.open("POST", "/Modules/Reports/OutstandingBalance.aspx/ExportPDF", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = "blob";
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                $("body").mLoading('hide');
            };
            xhr.send(json);
        }

    </script>

</asp:Content>
