<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ByComplete.aspx.cs" Inherits="FingerprintPayment.EQ.Report.ByComplete" %>

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
                        url: "/EQ/Report/ByComplete.aspx/LoadData",
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
                                //'name': SAC.GetStudentName(),
                                //'type': $('#SDQType').val(),
                            };

                            return JSON.stringify(d);
                        },
                    },


                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>', data: 'Room', "class": "text-center", "width": "7%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %>", data: 'Count', "class": "text-center", "width": "10%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304019") %>", data: 'Done', "class": "text-center", "width": "10%" },     
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304020") %>", data: 'NotDone', "class": "text-center", "width": "10%" },     
                        //{
                        //    "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304020") %>", data: 'ScoreAll', "width": "10%", "class": "text-center",
                        //    "mRender": function (data, type, row) {
                        //        return row.Count - row.Done;
                        //    }
                        //},
                       
                    ],
                    "order": [[0, 'asc']],

                    "fnInitComplete": function (oSettings, json) {
                        var p = json.d.percent;
                        $('#s1').text(p.s1.toFixed(2) + '%');
                        $('#s2').text(p.s2.toFixed(2) + '%');

                        $(".result-wrapper").show();
                    }
                });

            }
            else if (t == 'report') {
                var json = JSON.stringify({
                    'yearNo': YTLCF.GetYearNo(),
                    'term': YTLCF.GetTermID(),
                    'termNo': YTLCF.GetTermNo(),
                    'level1': YTLCF.GetLevelID(),
                    'level2': YTLCF.GetClassID(),
                    //'name': SAC.GetStudentName(),
                    //'type': $('#SDQType').val(),
                });
                var dt = new Date();
                var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306008") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                xhr = new XMLHttpRequest();

                xhr.open("POST", "/EQ/Report/ByComplete.aspx/ExportExcel", true);
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
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306009") %>
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
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306008") %></h4>
                    </div>
                    <div class="card-body ">
                         
                        <div class="row result-wrapper" style="display:none">
                            <div class="col-md-12" style="">
                                <table id="template1" class=" table-hover dataTable" width="100%" style="margin: 0 5px;">
                                    <thead>
                                        <tr>
                                            <th style="width: 20%; text-align: center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></th>
                                            <th style="width: 20%; text-align: center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %></th>
                                            <th style="width: 60%; text-align: center" colspan="2" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307004") %></th>
                                        </tr>
                                          <tr>                                         
                                            <th style="width: 30%; text-align: center" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304019") %></th>
                                            <th style="width: 30%; text-align: center" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304020") %></th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>

                        <div class="row result-wrapper" style="display:none">
                            <div class="col-md-12" id="result-wrapper">
                                  <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306011") %>  <span id="s1"></span> / <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306012") %>  <span id="s2"></span>
                                </strong>
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
