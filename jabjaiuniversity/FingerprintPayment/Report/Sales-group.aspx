<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="Sales-group.aspx.cs" Inherits="FingerprintPayment.Report.Sales_group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link href="../Styles/SettingDialog.css" rel="stylesheet" />

    <style type="text/css">
        .fa.fa-print.btn-print {
            cursor: pointer;
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
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603038") %>       
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

                        <div class="row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %></label>
                            <div class="col-md-3">
                                <select id="sort_type" class='selectpicker' data-width="100%" data-style="select-with-transition">
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105013") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105011") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206508") %></option>
                                </select>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"></label>
                            <div class=" col-md-3">
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row sort_type t1">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></label>
                            <div class="col-md-3">
                                <select id="select_month" class="selectpicker" data-width="100%" data-style="select-with-transition">
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %></option>
                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %></option>
                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %></option>
                                    <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %></option>
                                    <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %></option>
                                    <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %></option>
                                    <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %></option>
                                    <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %></option>
                                    <option value="11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %></option>
                                    <option value="12"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %></option>
                                </select>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %></label>
                            <div class=" col-md-3">
                                <select id="select_year1" class="selectpicker" data-width="100%" data-style="select-with-transition">
                                    <% for (int i = 2018; i <= DateTime.Now.Year; i++)
                                        {%>
                                    <option value="<%= i %>"><%= i+543 %></option>
                                    <%}%>
                                </select>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row sort_type t2">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %></label>
                            <div class="col-md-3">
                                <select id="select_year2" class="selectpicker" data-width="100%" data-style="select-with-transition">
                                    <% for (int i = 2018; i <= DateTime.Now.Year; i++)
                                        {%>
                                    <option value="<%= i %>"><%= i+543 %></option>
                                    <%}%>
                                </select>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"></label>
                            <div class=" col-md-3">
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row sort_type t3">
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
                                <div class="btn btn-info pull-rightx" id="exportfile" onclick="Reports.export_excel()">
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

                        <div class="row ">
                            <br />

                            <div class="col-md-12">
                                <span id="lblSummary" style="float: right;"></span>
                            </div>

                            <div class="col-md-12">
                                <fieldset>
                                    <table id="example" class="table-hover dataTable"
                                        cellspacing="0" width="100%">
                                    </table>
                                </fieldset>
                            </div>
                        </div>

                        <fieldset class="d-none" id="export_excel">
                            <table id="table_exports" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                            </table>
                        </fieldset>
                        <%--   <iframe id="txtArea1" style="display: none"></iframe>--%>
                    </div>
                </div>
            </div>
        </div>
    </form>


</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <script src="../Scripts/FileSaver.js" type="text/javascript"></script>
    <script src="ScriptReport.js" type="text/javascript"></script>
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script src="Script/Sales-group.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmssss") %>" type="text/javascript"></script>

    <script type="text/javascript">
        var Reports = new report_sales();
        $(function () {
            //$("#btncontent_1").click(function () {
            //    //$("body").mLoading();
            //    $("#content_1").show();
            //    $("#content_0").hide();
            //    //qurey_detail(date)
            //});

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

            showsort_type();

            $("#sort_type").change(function () {
                showsort_type();
            })

        });

        function SearchData() {
            var dStart = "", dEnd = "";
            switch ($("#sort_type").val()) {
                case "0": dStart = date; break;
                case "1": dStart = $("#select_month").val() + '/01/' + $("#select_year1").val(); break;
                case "2": dStart = '01/01/' + $("#select_year2").val(); break;
                case "3": dStart = getDate($("#txtstart").val()); dEnd = ($("#txtend").val() == "" ? getDate($("#txtstart").val()) : getDate($("#txtend").val())); break;
            }

            shop_id = $("#shop_id").val();
            var data = {
                "sort_type": $("#sort_type").val(), "dStart": dStart,
                "dEnd": dEnd, "shop_id": shop_id, "user_id": $("#txtid").val(),
                "emp_id": $("#txtemployees_id").val(),
            };

            searchData = data;
            $("body").mLoading();
            PageMethods.reports_detail(data, function (e) {
                console.log(e.data);
                Reports.reports_data = e;
                Reports.RenderHtml("example", false);
                $("body").mLoading('hide');
            });
        }

        var searchData = [];
        function showsort_type() {
            var sort_type = $("#sort_type").val();
            $(".sort_type").hide();
            $(".sort_type.t" + sort_type).show();
            //switch ($("#sort_type").val()) {
            //    case "1": $(sort_type[0]).show(); $(sort_type[1]).show(); break;
            //    case "2": $(sort_type[1]).show(); break;
            //    case "3": $(sort_type[2]).show(); break;
            //}
        }

        function qurey_month(dStart) {
            var data = { "sort_type": 1, "dStart": getDate(dStart), "shop_id": shop_id, "user_id": $("#txtid").val(), "emp_id": $("#txtemployees_id").val() };

            $("body").mLoading();
            PageMethods.reports_detail(data, function (e) {
                console.log(e.data);
                Reports.reports_data = e;
                Reports.RenderHtml("example", false);
                $("body").mLoading('hide');
            });
        }

        function qurey_data(sort_type, dStart, dEnd) {
            var data = { "sort_type": sort_type, "dStart": dStart, "dEnd": dEnd, "shop_id": shop_id, "user_id": $("#txtid").val(), "emp_id": $("#txtemployees_id").val() };
            $("body").mLoading();
            PageMethods.reports_detail(data, function (e) {
                console.log(e.data);
                Chart_Reports(e.header_text, e.data);
                $("body").mLoading('hide');
            });
        }

        function getDate(values) {
            if (values != null && values != "") {
                var array = values.split("/");
                return array[1] + "/" + array[0] + "/" + (array[2] - 543);
            }
            else return '<%= DateTime.Today.ToString("MM/dd/yyyy") %>';
        }

    </script>
</asp:Content>
