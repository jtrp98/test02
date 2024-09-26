<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ReportTraining.aspx.cs" Inherits="FingerprintPayment.Report.ReportTraining" %>

<%@ Register Src="~/UserControls/TeacherAutocomplete.ascx" TagPrefix="uc1" TagName="TeacherAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Styles/SettingDialog.css" rel="stylesheet" />
      <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style type="text/css">
        .dropdown.bootstrap-select {
            padding-left: 0 !important;
            padding-right: 0 !important;
        }

        .bootstrap-datetimepicker-widget table td.active, .bootstrap-datetimepicker-widget table td.active:hover {
            background-color: transparent !important;
        }

        .bootstrap-datetimepicker-widget table td.day:hover, .bootstrap-datetimepicker-widget table td.hour:hover, .bootstrap-datetimepicker-widget table td.minute:hover, .bootstrap-datetimepicker-widget table td.second:hover {
            background: transparent;
        }

        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133190") %>        
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

                        <div class="row">
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                            <div class=" col-md-3 ">
                                <uc1:TeacherAutocomplete runat="server" ID="TeacherAutocomplete" IsRequired="true" />

                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></label>
                            <div class=" col-md-3">
                                <select id="type" class="selectpicker col-md-12" data-style="select-with-transition">
                                    <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106160") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102077") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102078") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102079") %></option>
                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102080") %></option>
                                </select>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105085") %></label>
                            <div class="col-md-3">
                                <input type="text" id="txtstart" class="form-control datepicker --date-validate " required />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                            <div class="col-md-3">
                                <input type="text" id="txtend" class="form-control datepicker --date-validate" required />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                             <div class="col-md-2"></div>
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
                            <div class="col-md-12">
                                <asp:Literal ID="ltrHeaderReport" runat="server" />
                                <table id="example" class="table-hover dataTable" width="100%" style="width: 100%">
                                    <%-- <thead>
                                <tr>
                                    <th align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                    <th align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></th>
                                    <th align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                    <th align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></th>
                                    <th align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                    <th align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                    <th align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></th>
                                    <th align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402011") %></th>
                                    <th align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402012") %></th>
                                    <th align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402013") %></th>
                                    <th align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402014") %></th>
                                </tr>
                            </thead>--%>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script src="ScriptReport.js" type="text/javascript"></script>
    <script src="../Scripts/FileSaver.js" type="text/javascript"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        var availableValueUsers = [];
        //var Reports = new report_sales();
        $(function () {


            $('.datepicker').datetimepicker({
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
                debug: false,
                //autoclose: true,
                //autoclose: true,
                //showOn: "button",
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

            $(".datepicker").keydown(function (e) {
                e.preventDefault();
            });

            if (jQuery.validator) {//.messages

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",
                });

                $("#aspnetForm").validate({  // initialize the plugin

                    errorPlacement: function (error, element) {
                        let _class = element.attr('class');

                        if (_class.includes('--date-validate')) {
                            error.insertAfter(element.parent());
                        }
                        else {
                            error.insertAfter(element);
                        }

                    }

                });
            }


        });



        function SearchData(t) {

            if (!$("#aspnetForm").valid()) {
                return;
            }

            //if ($("#txtstart").val() == '')
            //    return;

            var dStart = "", dEnd = "";



            if ($("#txtstart").val() != '')
                dStart = moment($("#txtstart").val(), 'DD/MM/YYYY').format("DDMMYYYY");//getDate($("#txtstart").val());

            if ($("#txtend").val() != '')
                dEnd = moment($("#txtend").val(), 'DD/MM/YYYY').format("DDMMYYYY");//getDate($("#txtend").val());
            else
                dEnd = dStart

            if (t == 'data') {
                var dt = $('#example').DataTable({
                    "processing": true,
                    "serverSide": false,
                    "destroy": true,
                    "info": false,
                    paging: false,
                    searching: false,
                    "ajax": "./Handles/DataEmpTraining_Handler.ashx?c=data&emp=" + TAC.GetUserID() + "&type=" + $("#type").val() + "&dstart=" + dStart + "&dend=" + dEnd,
                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', data: 'index', "class": "text-center", "width": "5%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>", data: 'date', "class": "text-center", "width": "15%" },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %>", data: 'type', "class": "text-center", "width": "15%",

                        },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102072") %>", data: 'project', "width": "15%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102073") %>", data: 'training', "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105087") %>", data: 'place', "class": "text-center", "width": "15%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102083") %>", data: 'hour', "class": "text-center", "width": "10%" },
                    ],

                    "order": [[0, 'asc']]
                });

            }
            else if (t == 'report') {
                var url = "Handles/DataEmpTraining_Handler.ashx?c=report&emp=" + TAC.GetUserID() + "&type=" + $("#type").val() + "&dstart=" + dStart + "&dend=" + dEnd;
                window.open(url);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
