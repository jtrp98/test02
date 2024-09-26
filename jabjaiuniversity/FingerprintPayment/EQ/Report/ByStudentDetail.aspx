<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ByStudentDetail.aspx.cs" Inherits="FingerprintPayment.EQ.Report.ByStudentDetail" %>

<%@ Register Src="~/UserControls/YTLCFilter.ascx" TagPrefix="uc1" TagName="YTLCFilter" %>
<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <style>
        label.error {
            color: red;
        }

        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }

        .dataTables_wrapper .btn-group {
            display: none;
        }
    </style>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

  
    <script src="eq.js?v=1"></script>
    <script>

        function SearchData(t) {
            if (!$("#aspnetForm").valid()) {
                return;
            }

            if (t == 'data') {

                var dt = $('#template1').DataTable({
                    "processing": true,
                    //"serverSide": true,
                    "destroy": true,
                    "info": false,
                    paging: true,
                    "pageLength": 20,
                    searching: false,
                    "lengthChange": false,
                    ajax: {
                        url: "/EQ/Report/ByStudentDetail.aspx/LoadData",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        "dataSrc": function (r) {
                            return r.d.data;
                        },
                        'data': function (d) {

                            d.search = {
                                'term': YTLCF.GetTermID(),
                                'level1': YTLCF.GetLevelID(),
                                'level2': YTLCF.GetClassID(),
                                'name': SAC.GetStudentName(),
                                //'type': $('#SDQType').val(),
                            };

                            return JSON.stringify(d);
                        },
                    },


                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', data: 'Index', "class": "text-center", "width": "7%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>", data: 'Code', "class": "text-center", "width": "10%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>", data: 'Room', "class": "text-center", "width": "10%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %>", data: 'FullName', "width": "15%", "class": "text-center" },
                        {
                            "title": "1.1", data: 'Score11', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcScoreByRange(row.Score11, 13, 18);
                            }
                        },
                        {
                            "title": "1.2", data: 'Score12', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcScoreByRange(row.Score12, 16, 21);
                            }
                        },
                        {
                            "title": "1.3", data: 'Score13', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcScoreByRange(row.Score13, 17, 23);
                            }
                        },

                        //2
                        {
                            "title": "2.1", data: 'Score21', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcScoreByRange(row.Score21, 15, 21);
                            }
                        },
                        {
                            "title": "2.2", data: 'Score22', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcScoreByRange(row.Score22, 14, 20);
                            }
                        },
                        {
                            "title": "2.3", data: 'Score23', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcScoreByRange(row.Score23, 15, 20);
                            }
                        },

                        //3
                        {
                            "title": "3.1", data: 'Score31', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcScoreByRange(row.Score31, 9, 14);
                            }
                        },
                        {
                            "title": "3.2", data: 'Score32', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcScoreByRange(row.Score32, 16, 22);
                            }
                        },
                        {
                            "title": "3.3", data: 'Score33', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcScoreByRange(row.Score33, 15, 22);
                            }
                        },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306015") %>", data: 'Score33', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                return CalcScoreSum(row.ScoreAll);
                            }
                        },
                    ],
                    "order": [[0, 'asc']]
                });

            }
            else if (t == 'report') {
                var json = JSON.stringify({
                    'yearNo': YTLCF.GetYearNo(),
                    'term': YTLCF.GetTermID(),
                    'termNo': YTLCF.GetTermNo(),
                    'level1': YTLCF.GetLevelID(),
                    'level2': YTLCF.GetClassID(),
                    'name': SAC.GetStudentName(),
                    //'type': $('#SDQType').val(),
                });
                var dt = new Date();
                var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306021") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                xhr = new XMLHttpRequest();

                xhr.open("POST", "/EQ/Report/ByStudentDetail.aspx/ExportExcel", true);
                xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                xhr.responseType = 'blob';
                xhr.onload = function () {
                    //aa = xhr.getResponseHeader("filename");
                    saveAs(xhr.response, file_name);
                    //$("body").mLoading('hide');
                };
                xhr.send(json);
            }
        }

        $(function () {

            if (jQuery.validator) {//.messages

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",
                });

                $("#aspnetForm").validate({  // initialize the plugin

                    errorPlacement: function (error, element) {
                        let _class = element.attr('class');

                        if (_class.includes('--req-append-last')) {
                            error.insertAfter(element.parent());
                        }
                        else {
                            error.insertAfter(element);
                        }

                    }

                });
            }
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306022") %>
            </p>
        </div>
    </div>

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>

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

                        <uc1:YTLCFilter runat="server" ID="YTLCFilter" IsLevelRequired="true" />

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                            <div class="col-sm-3">
                                <uc1:StudentAutocomplete runat="server" ID="StudentAutocomplete" />
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row mt-2">
                            <div class="col-md-12 text-center">
                                <button type="button" class="btn btn-success" onclick="SearchData('data');">
                                    <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                                <div class="btn btn-info pull-rightx" id="exportfile" onclick="SearchData('report')">
                                    <span class="btn-label">
                                        <span class="material-icons">receipt_long
                                        </span>
                                    </span>
                                    Export
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="card ">
                    <div class="card-header card-header-primary card-header-icon">
                        <div class="card-icon">
                            <span class="material-icons">list_alt</span>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306021") %></h4>
                    </div>
                    <div class="card-body ">
                     
                        <div class="row">
                            <div class="col-md-12" style="">
                                <table id="template1" class=" table-hover dataTable" width="100%" style="margin: 0 5px;">
                                    <thead>
                                        <tr>
                                            <th style="width: 5%; text-align: center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th style="width: 10%; text-align: center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                            <th style="width: 10%; text-align: center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></th>
                                            <th style="width: 15%; text-align: center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></th>
                                            <th style="width: 15%; text-align: center" colspan="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306023") %></th>
                                            <th style="width: 15%; text-align: center" colspan="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306024") %></th>
                                            <th style="width: 15%; text-align: center" colspan="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306025") %></th>
                                            <th style="width: 15%; text-align: center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306015") %></th>
                                        </tr>
                                        <tr>
                                            <th style="text-align: center">1.1</th>
                                            <th style="text-align: center">1.2</th>
                                            <th style="text-align: center">1.3</th>

                                            <th style="text-align: center">2.1</th>
                                            <th style="text-align: center">2.2</th>
                                            <th style="text-align: center">2.3</th>

                                            <th style="text-align: center">3.1</th>
                                            <th style="text-align: center">3.2</th>
                                            <th style="text-align: center">3.3</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12" id="result-wrapper">
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </form>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
