<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FingerprintPayment.VisitHousePage.Web.WebForm1" %>

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
        /*  .dropdown.bootstrap-select {
            width: 99% !important;
        }*/

        /*   table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }

        .dataTables_wrapper .btn-group {
            display: none;
        }
*/
        /*  .dataTable tfoot td {
            border-bottom: 2px solid #000;
        }*/
        /*table.dataTable thead .sorting_asc,
        table.dataTable thead .sorting_desc,
        table.dataTable thead .sorting {
            background-image: url('') !important;
        }
        .dataTables_wrapper .dataTables_paginate .paginate_button:hover {
            background: #fff !important;
            border: 0px !important;
        }*/
    </style>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/2.0.1/js/dataTables.buttons.min.js"></script>
    <%--    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/1.6.4/js/buttons.flash.min.js"></script>--%>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/2.0.1/js/buttons.html5.min.js"></script>
    <%-- <script src="Js/buttons.html5.js"></script>--%>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/2.0.1/js/buttons.print.min.js"></script>

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
                        url: "/VisitHousePage/Web/Index.aspx/LoadData",
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
                                'name': SAC.GetStudentName()
                            };

                            return JSON.stringify(d);
                        },
                    },

                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', data: 'Index', "class": "text-center", "width": "10%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>", data: 'Code', "class": "text-center", "width": "15%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %>", data: 'FullName', "width": "30%", "class": "text-center" },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>", data: 'Status1', "width": "15%", "class": "text-center",
                            "mRender": function (data, type, row) {
                                switch (row.Status1) {
                                    case 0:
                                        return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101031") %>";
                                    case 1:
                                        return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101033") %>";
                                    case 2:
                                        return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101032") %>";
                                    case 3:
                                        return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101034") %>";
                                    case 4:
                                        return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101030") %>";
                                    case 5:
                                        return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101035") %>";
                                    case 6:
                                        return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101036") %>";
                                    case 7:
                                        return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101037") %>";
                                    default:
                                        return "";
                                }

                            }

                        },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305003") %>", data: 'Status2', "class": "text-center", "width": "20%", "mRender": function (data, type, row) {
                                var txt = "";
                                var color = "";
                                switch (row.Status2) {
                                    case 1:
                                        txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305004") %>";
                                        color = "btn-default";
                                        break;
                                    case 2:
                                        txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305006") %>";
                                        color = "btn-success";
                                        break;
                                    case 3:
                                        txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305005") %>";
                                        color = "btn-danger";
                                        break;
                                    default:
                                        txt = "";
                                        break;
                                }
                                return `<a  href='Edit.aspx?sId=${row.sID}&term=${row.TermID}' class='btn btn-fill ${color}' ><span class='material-icons'>home</span>&nbsp; ${txt}</a>`;
                            }
                        },
                    ],
                    "order": [[0, 'asc']]
                });

            }
            else if (t == 'report1') {

                var url = `/VisithousePage/Web/PrintPreview.aspx?classid=${YTLCF.GetLevelID()}&roomid=${YTLCF.GetClassID()}&term=${YTLCF.GetTermID()}`;
                window.open(url, "_blank");
            }
            else if (t == 'report2') {

                var url = `/VisithousePage/Web/PrintPreview2.aspx?classid=${YTLCF.GetLevelID()}&roomid=${YTLCF.GetClassID()}&term=${YTLCF.GetTermID()}`;
                window.open(url, "_blank");
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
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305002") %>
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
                        <%--     <uc1:YTFilter runat="server" ID="YTFilter" IsRequired="true" />--%>
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

                            <div class="col-8 mx-auto">
                                <div class="row">
                                    <div class="col-md-6 text-right">
                                        <button type="button" class="btn btn-success" onclick="SearchData('data');">
                                            <span class="btn-label">
                                                <i class="material-icons">search</i>
                                            </span>
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                        </button>
                                    </div>
                                    <div class="col-md-6 text-left">
                                        <div class="dropdown">
                                            <a class="btn btn-info dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <span class="btn-label">
                                                    <span class="material-icons">receipt_long
                                                    </span>
                                                </span>
                                                Export
                                            </a>
                                            <div class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                                <a class="dropdown-item" style="cursor: pointer;" onclick="SearchData('report1')"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105043") %></a>
                                                <a class="dropdown-item" style="cursor: pointer;" onclick="SearchData('report2')"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105044") %></a>
                                            </div>
                                        </div>
                                    </div>
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
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306026") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12" style="">
                                <table id="template1" class=" table-hover dataTable" width="100%" style="margin: 0 5px;">
                                    <%-- <thead>
                                        <tr>
                                            <th style="width: 10%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th style="width: 15%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                            <th style="width: 30%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                            <th style="width: 10%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                            <th style="width: 20%; text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305003") %></th>
                                        </tr>
                                    </thead>--%>
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
