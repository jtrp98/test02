<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ReportTransLog.aspx.cs" Inherits="FingerprintPayment.Report.ReportTransLog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/UserControls/TeacherStudentAutocomplete.ascx" TagPrefix="uc1" TagName="TeacherStudentAutocomplete" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <link href="../Styles/SettingDialog.css" rel="stylesheet" />
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />


    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" type="text/javascript"></script>
    <style type="text/css">
        .fa.fa-print.btn-print {
            cursor: pointer;
        }

        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }

        .more-one-tx-count {
            color: var(--yellow);
            font-size: .875rem;
        }
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603087") %></p>
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
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603083") %></label>
                            <div class="col-md-3">
                                <select id="type" class='selectpicker' data-width="100%" data-style="select-with-transition">
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504002") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603084") %></option>
                                </select>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"></label>
                            <div class=" col-md-3">
                            </div>
                            <div class="col-md-2"></div>
                        </div>


                        <div class="row typewrap" id="typewrap1">

                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603011") %></label>
                            <div class="col-md-3">
                                <uc1:TeacherStudentAutocomplete runat="server" ID="TeacherStudentAutocomplete" IsRequired="true" />
                                <%--<input type="text" class='form-control' id="txtid1" style="display: none;" />
                                <input type="text" class='form-control' id="txtname1" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>" />--%>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"></label>
                            <div class=" col-md-3">
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row typewrap" id="typewrap2" style="display: none">

                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603086") %></label>
                            <div class="col-md-3">
                                <%--<input type="text" class='form-control' id="txtid2" style="display: none;" />--%>
                                <input type="text" class='form-control' id="txtname2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106148") %>" autocomplete="off" required />
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"></label>
                            <div class=" col-md-3">
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106101") %></label>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtstart" CssClass="form-control  datepicker --date-validate" MaxLength="10" Style="" required autocomplete="off" ClientIDMode="Static" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106102") %></label>
                            <div class=" col-md-3">
                                <asp:TextBox runat="server" ID="txtend" CssClass="form-control  datepicker --date-validate" MaxLength="10" Style="" required autocomplete="off" ClientIDMode="Static" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12 text-center">
                                <br />
                                <button type="button" id="btnSearch" class="btn btn-info search-btn" onclick="SearchData()">
                                    <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                                <div class="btn btn-success " id="exportfile" onclick="Reports.export_excel()">
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
                        <%-- <div class="row">
                            <div class=" col-md-12 text-right ">
                               
                            </div>
                        </div>--%>

                        <div class="row ">
                            <br />

                            <div class="col-md-12">
                                <span id="lblSummary" style="float: right;"></span>
                            </div>

                            <div class="col-md-12">
                                <fieldset>
                                    <table id="example1" class="table-hover dataTable"
                                        cellspacing="0" width="100%" style="display:none">
                                    </table>
                                      <table id="example2" class="table-hover dataTable"
                                        cellspacing="0" width="100%" style="display:none">
                                    </table>
                                </fieldset>
                            </div>
                        </div>

                        <fieldset class="d-none" id="export_excel">
                            <table id="table_exports" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                            </table>
                        </fieldset>
                        <iframe id="txtArea1" style="display: none"></iframe>
                    </div>
                </div>
            </div>
        </div>

    </form>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">

    <script src="ScriptReport.js" type="text/javascript"></script>
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script src="Script/reportTransLog.js?d=<%=DateTime.Now.Ticks%>" type="text/javascript"></script>
    <script src="../Scripts/FileSaver.js" type="text/javascript"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        var dateNow = '<%=DateTime.Now.ToString("dd/MM/yyyy" , new System.Globalization.CultureInfo("th-TH")) %>';
        var availableValueUsers = [];
        var availableValueUsers2 = [];
        var Reports = new report_sales();
        var selectedCard;
        $(function () {

            var _startDate = moment("01/01/2022", "DD/MM/YYYY");

            $('.datepicker').datetimepicker({
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
                debug: false,
                minDate: _startDate,
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

            $("#btncontent_1").click(function () {
                //$("body").mLoading();
                $("#content_1").show();
                $("#content_0").hide();
                //qurey_detail(date)
            });


            $("#type").change(function () {
                var t = $(this).val();

                $('.typewrap').hide();

                switch (t) {
                    case "1": $('#typewrap1').show(); break;
                    case "2": $('#typewrap2').show(); break;
                  
                }
            });

            $('#txtname2').autoComplete({
                resolver: 'custom',
                minLength: 1,
                // noResultsText: '',
                events: {
                    search: function (qry, callback) {
                        var res = availableValueUsers2.filter((v) => {
                            if (v.text.toLowerCase().indexOf(qry.toLowerCase()) > -1 ) {
                                return v;
                            }
                        })
                        callback(res);
                    }
                }
            });

            $('#txtname2').on('autocomplete.select', function (evt, item) {
                selectedCard = item;
            });

            if (jQuery.validator) {

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

            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=backupcard",
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            text: objjson[index].name,
                            id: objjson[index].id,
                            money: objjson[index].money,
                            insurance: objjson[index].insurance,
                        };

                        availableValueUsers2.push(newObject);
                    });
                }
            });

        });
               
        function lightwellBuyer2(request, response) {

            function hasMatch(s) {
                s += '';
                return s.toLowerCase().indexOf((request.term + '').toLowerCase()) !== -1;
            }
            var i, l, obj, matches = [];

            if (request.term === "") {
                response([]);
                return;
            }

            var availableValue = [];
            availableValue = availableValueUsers2;
            for (i = 0, l = availableValue.length; i < l; i++) {
                obj = availableValue[i];
                //if (obj.code != null && obj.code != undefined && obj.code != "") {
                if (hasMatch(obj.label)) {
                    matches.push(obj);
                }
                //}
            }
            response(matches.slice(0, 10));
        }

        var searchData = [];
        function SearchData() {

            if ($('#aspnetForm').valid() == false) {
                return false;
            }

            var t = $('#type').val();
            if (t == '1') {
                //if ($("#txtid1").val() == '')
                //    return;

                if ($("#txtstart").val() == '')
                    return;

                if ($("#txtend").val() == '')
                    return;

                var dStart , dEnd ;

                if ($("#txtstart").val() != '')
                    dStart = $("#txtstart").data("DateTimePicker").date().format('MM/DD/YYYY');//getDate($("#txtstart").val());

                if ($("#txtend").val() != '')
                    dEnd = $("#txtend").data("DateTimePicker").date().format('MM/DD/YYYY');//getDate($("#txtend").val());
                else
                    dEnd = dStart;

                //dStart = getDate($("#txtstart").val());
                //dEnd = ($("#txtend").val() == "" ? getDate($("#txtstart").val()) : getDate($("#txtend").val()));
                //shop_id = $("#shop_id").val();

                var data = {
                    "dStart": dStart,
                    "dEnd": dEnd,
                    "user_id": TSAC.GetUserID(),
                    //"sort_type": $("#sort_type").val(),
                    //"shop_id": shop_id,
                    //"emp_id": $("#txtemployees_id").val(),
                    //"user_id": $("#txtuser_id").val(),
                };

                searchData = data;
                $("body").mLoading();
                
                PageMethods.reports_detail(data, function (e) {
                    //console.log(e.data);
                    Reports.reports_data = e;
                    Reports.RenderHtml("example1", false);
                    $('[rel="tooltip"]').tooltip();
                    $("body").mLoading("hide");
                });
            }
            else if (t == '2') {
                //if ($("#txtid2").val() == '')
                //    return;

                //if ($("#txtstart").val() == '')
                //    return;

                //var dStart = "", dEnd = "";

                //dStart = getDate($("#txtstart").val());
                //dEnd = ($("#txtend").val() == "" ? getDate($("#txtstart").val()) : getDate($("#txtend").val()));

                if ($("#txtstart").val() == '')
                    return;

                if ($("#txtend").val() == '')
                    return;

                var dStart = "", dEnd = "";

                if ($("#txtstart").val() != '')
                    dStart = moment($("#txtstart").val(), 'DD/MM/YYYY').format("YYYYMMDD");//getDate($("#txtstart").val());

                if ($("#txtend").val() != '')
                    dEnd = moment($("#txtend").val(), 'DD/MM/YYYY').format("YYYYMMDD");//getDate($("#txtend").val());
                else
                    dEnd = dStart;

                //shop_id = $("#shop_id").val();
                var data = {
                    "sStart": dStart,
                    "sEnd": dEnd,
                    "card_id": selectedCard.id,
                };

                searchData = data;
                $("body").mLoading();
                PageMethods.ReportBackupCard(data, function (e) {
                    //console.log(e.data);
                    Reports.reports_data = e;
                    Reports.RenderHtml2("example2", false);
                    $("body").mLoading("hide");
                });
            }
         
        }

        function getDate(values) {
            if (values != null && values != "") {
                var array = values.split("/");
                return array[1] + "/" + array[0] + "/" + array[2];
            }
            else return '<%= DateTime.Today.ToString("MM/dd/yyyy") %>';
        }

    </script>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />
    <asp:HiddenField ID="hdfschoolname" runat="server" />
    <div class="full-card box-content report-container" id="content_1">
              <div class="row">
            <div class="form-group col-sm-6 ">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603083") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <select id="type" class='form-control'>
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504002") %></option>
                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603084") %></option>
                    </select>
                </div>
            </div>
            <div class="form-group col-sm-6 ">
            </div>
        </div>
        <div class="row typewrap" id="typewrap1">
            <div class="form-group col-sm-6 ">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603011") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" class='form-control' id="txtid1" style="display: none;" />
                    <input type="text" class='form-control' id="txtname1" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>" />
                </div>
            </div>
            <div class="form-group col-sm-6 ">
            </div>
        </div>
        <div class="row typewrap" id="typewrap2" style="display:none">
            <div class="form-group col-sm-6 ">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603086") %> :</label>
                <div class="col-md-7 col-sm-6">
                     <input type="text" class='form-control' id="txtid2" style="display: none;" />
                    <input type="text" class='form-control' id="txtname2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>" />
                </div>
            </div>
            <div class="form-group col-sm-6 ">
            </div>
        </div>
        <div class="row ">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" id="txtstart" class="form-control datepicker col-md-6" readonly="readonly" />

                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" id="txtend" class="form-control datepicker" readonly="readonly" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <input type="button" class="btn btn-primary button-custom" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="SearchData();" />
            </div>
        </div>
        <div class="row--space">
        </div>
        <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2">
             
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8">
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2">
                <div class="btn btn-success button-custom" id="exportfile" onclick="Reports.export_excel()">Export File</div>
            </div>
        </div>
        <asp:Literal ID="ltrHeaderReport" runat="server" />
        <div class="row border-bottom">
            <br />
            <div class="col-sm-12">
                <fieldset>
                    <asp:ListView ID="lvReport" runat="server">
                    </asp:ListView>
                    <span id="lblSummary" style="float: right;"></span>
                    <table id="example" class="table table-bordered table-hover dataTable no-footer"
                        cellspacing="0" width="100%">
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
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>--%>
