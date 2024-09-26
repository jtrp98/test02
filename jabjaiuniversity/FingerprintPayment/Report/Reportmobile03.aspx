<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="Reportmobile03.aspx.cs" Inherits="FingerprintPayment.Report.Reportmobile03" %>

<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>
<%@ Register Src="~/UserControls/LCFilter.ascx" TagPrefix="uc1" TagName="LCFilter" %>
<%@ Register Src="~/UserControls/YTFilter.ascx" TagPrefix="uc1" TagName="YTFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/magnific-popup.js/1.0.0/magnific-popup.min.css" integrity="sha512-nIm/JGUwrzblLex/meoxJSPdAKQOe2bLhnrZ81g5Jbh519z8GFJIWu87WAhBH+RAyGbM4+U3S2h+kL5JoV6/wA==" crossorigin="anonymous" />
    <style type="text/css">
       
    </style>
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
    <%-- <script src="/app/Reports/Come2school/ReportCome2SchoolStudentJS.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>
    <script src="ScriptReport.js" type="text/javascript"></script>
    <script src="Script/Reportmobile03.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmm") %>" type="text/javascript"></script>
    <script src="../Scripts/FileSaver.js" type="text/javascript"></script>
    <script src="../Scripts/freeze-table.js" type="text/javascript"></script>

    <script src="//cdnjs.cloudflare.com/ajax/libs/magnific-popup.js/1.0.0/jquery.magnific-popup.min.js" integrity="sha512-+m6t3R87+6LdtYiCzRhC5+E0l4VQ9qIT1H9+t1wmHkMJvvUQNI5MKKb7b08WL4Kgp9K0IBgHDSLCRJk05cFUYg==" crossorigin="anonymous"></script>
    <script>
        $(function () {

            $('.datepicker').datetimepicker({
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
                debug: false,
                //defaultDate: "<%=DateTime.Now.ToString("dd/MM/yyyy") %>",
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
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303011") %>   
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
                        <%--   <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="ddlsublevel" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="ddlSubLV2" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2"></div>
                        </div>--%>
                        <uc1:LCFilter runat="server" ID="LCFilter" IsRequired="true" />
                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %></label>
                            <div class="col-md-3 ">
                                <select id="sort_type" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105010") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105011") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206190") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105013") %></option>
                                </select>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left reports_type"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133165") %></label>
                            <div class="col-md-3 reports_type">
                                <select id="reports_type" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105043") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105044") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132513") %></option>
                                </select>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row sort_type0">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                            <div class="col-md-3 ">
                                <div class="form-group has-successx">
                                    <input type="text" id="txtstart1" class="form-control datepicker" required />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"></label>
                            <div class="col-md-3 ">
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row sort_type1" style="display: none">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %></label>
                            <div class="col-md-3 ">
                                <select id="select_year" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <% for (int i = DateTime.Today.Year; i >= 2015; i--)
                                        {%>
                                    <option value="<%= i %>"><%= i + 543 %></option>
                                    <%}%>
                                </select>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></label>
                            <div class="col-md-3 ">
                                <select id="select_month" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
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
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" sort_type2" style="display: none">
                            <uc1:YTFilter runat="server" ID="YTFilter" />
                            <%--<div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="ddlyear" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <asp:ListItem Text="2558" Value="2557" Selected="True" />
                                    <asp:ListItem Text="2559" Value="2557" Selected="False" />
                                    <asp:ListItem Text="2560" Value="2557" Selected="False" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="semister" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <asp:ListItem Text="1" Value="1" Selected="True" />
                                    <asp:ListItem Text="2" Value="2" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2"></div>--%>
                        </div>

                        <div class=" row sort_type3" style="display: none">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                            <div class="col-md-3 ">
                                <div class="form-group has-successx">
                                    <input type="text" id="txtstart3" name="txtstart3" class="form-control datepicker" required />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                            <div class="col-md-3 ">
                                <div class="form-group has-successx">
                                    <input type="text" id="txtend3" name="txtend3" class="form-control datepicker" required />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301008") %></label>
                            <div class="col-md-3 ">
                                <uc1:StudentAutocomplete runat="server" ID="StudentAutocomplete" />
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"></label>
                            <div class="col-md-3 ">
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-center">
                                <br />
                                <button type="button" onclick="Getreports('example', false);" class="btn btn-fill btn-info  ">
                                    <span class="material-icons">search</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>                              
                                </button>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-right">
                                <a class="btn btn-fill btn-warning " href="/UpdateStatus/JobScan.aspx" style="" target="_blank">
                                    <span class="material-icons">edit</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>
                                </a>
                                <%-- <button type="button" class="btn btn-fill btn-success " onclick="ToExcel()">
                                    <span class="material-icons">receipt_long</span>&nbsp;Export File
                                </button>--%>
                                <div style="display: inline-block;">
                                    <div class="dropdown" style="display: none;" id="btnExport_2">
                                        <div id="dLabel" class="btn btn-success button-custom" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            Export File
                        <span class="caret"></span>
                                        </div>
                                        <ul class="dropdown-menu" aria-labelledby="dLabel">
                                            <li>
                                                <a class="" style="cursor: pointer" onclick="export_excel2()">
                                                    <span><i class="fa  fa-file-excel-o"></i>Export Excel</span>
                                                </a>
                                            </li>
                                            <li>
                                                <a class="" style="cursor: pointer" onclick="export_pdf2()">
                                                    <span><i class="fa  fa-file-pdf-o"></i>Export PDF</span>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="btn btn-success button-custom" onclick="export_excel()" id="btnExport_1">Export File</div>
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
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102145") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="report-container">
                            <%-- <asp:Literal ID="Literal1" runat="server" />--%>
                            <div class="row " style="overflow-y: auto;">
                                <br />
                                <div class="col-sm-12">
                                    <fieldset>
                                        <asp:ListView ID="ListView1" runat="server">
                                        </asp:ListView>
                                        <table id="example" class="table-hover dataTable  table-show-result" cellspacing="0" width="100%">
                                        </table>

                                    </fieldset>
                                </div>
                                <div class="col-sm-12">
                                    <span id="remark1" class="float-right" style="display: none"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105027") %>
                                    </span>
                                </div>
                                <fieldset class="d-none" id="export_excel">
                                    <table id="table_exports" class="table table-condensed table-bordered table-show-result" cellspacing="0" width="100%">
                                    </table>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <%-- <div class="report-container">

        <div class="row">
            <div class="col-sm-12">
            </div>
        </div>
        <div class='row hidden' style="font-weight: bolder; font-size: 40px;">
            <br />
            <fieldset>
                <legend><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M406001") %></legend>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-success'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>
                            <br />
                        <span class='text-large' id="status01">0</span>
                    </p>
                </div>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-warning'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>
                            <br />
                        <span class='text-large' id="status02">0</span>
                    </p>
                </div>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-danger'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>
                            <br />
                        <span class='text-large' id="status03">0</span>
                    </p>
                </div>
            </fieldset>
        </div>
        <div class="row--space">
        </div>
        <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6">
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2">
                <a class="btn btn-info button-custom btnpermission" href="/UpdateStatus/JobScan.aspx" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %></a>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2">
            
                <div class="dropdown" style="display: none;" id="btnExport_2">
                    <div id="dLabel" class="btn btn-success button-custom" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        Export File
                        <span class="caret"></span>
                    </div>
                    <ul class="dropdown-menu" aria-labelledby="dLabel">
                        <li>
                            <a class="btn" onclick="export_excel2()">
                                <h3><i class="fa  fa-file-excel-o"></i>Export Excel</h3>
                            </a>
                        </li>
                        <li>
                            <a class="btn" onclick="export_pdf2()">
                                <h3><i class="fa  fa-file-pdf-o"></i>Export PDF</h3>
                            </a>
                        </li>
                    </ul>
                </div>
                <div class="btn btn-success button-custom" onclick="export_excel()" id="btnExport_1">Export File</div>
            </div>
        </div>
        <asp:Literal ID="ltrHeaderReport" runat="server" />
        <div class="row border-bottom" style="overflow-y: auto;">
            <br />
            <div class="col-sm-12">
                <fieldset>
                    <asp:ListView ID="lvReport" runat="server">
                    </asp:ListView>
                    <table id="example" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                    </table>
                </fieldset>
            </div>
            <fieldset class="hidden" id="export_excel">
                <table id="table_exports" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                </table>
            </fieldset>
        </div>
    </div>--%>
</asp:Content>
