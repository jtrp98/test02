<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="Reportmobile05.aspx.cs" Inherits="FingerprintPayment.Report.Reportmobile05" %>

<%@ Register Src="~/UserControls/TeacherAutocomplete.ascx" TagPrefix="uc1" TagName="TeacherAutocomplete" %>


<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/magnific-popup.js/1.0.0/magnific-popup.min.css" integrity="sha512-nIm/JGUwrzblLex/meoxJSPdAKQOe2bLhnrZ81g5Jbh519z8GFJIWu87WAhBH+RAyGbM4+U3S2h+kL5JoV6/wA==" crossorigin="anonymous" />

    <style type="text/css">
        .dropdown.bootstrap-select {
            /*  width: 99% !important;*/
        }

        /*    table.dataTable tbody tr:last-child td, table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }*/

        table.dataTable thead tr td,
        table.dataTable thead tr th {
            text-align: center;
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

        /*#example {
            background- 
        }*/
        .sort_type {
            display: none;
        }
    </style>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105001") %>        
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

                        <%--
                            <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"></label>
                            <div class="col-md-3 ">
                              
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"></label>
                            <div class="col-md-3 ">
                               
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                        --%>

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %><br />
                                /Report Type</label>
                            <div class="col-md-3 ">
                                <select id="sort_type" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105010") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105011") %></option>
                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105012") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105013") %></option>
                                </select>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left formtype">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603033") %><br />
                                /Type</label>
                            <div class="col-md-3 ">
                                <select id="form_type" class="selectpicker formtype" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133180") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133181") %></option>
                                </select>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row sort_type0" style="display: none">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %><br />
                                /Date
                            </label>
                            <div class="col-md-3 ">
                                <div class="form-group has-successx">
                                    <input type="text" id="txtstart0" class="form-control datepicker" required />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105007") %><br />
                                /Status</label>
                            <div class="col-md-3 ">
                                <select id="status" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="9">
                                    <option value="-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105014") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></option>
                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105016") %></option>
                                    <option value="99"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105017") %></option>
                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105018") %></option>
                                    <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105019") %></option>
                                </select>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row sort_type1" style="display: none">
                            <div class="col-md-1 "></div>
                            <label class="col-md-1  col-form-label text-left ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %><br />
                                /Month</label>
                            <div class="col-md-3 ">
                                <select id="select_month1" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
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
                            <div class="col-md-1 "></div>
                            <label class="col-md-1 col-form-label text-left ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %><br />
                                /Year</label>
                            <div class="col-md-3 ">
                                <select id="select_year1" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <% for (int i = DateTime.Today.Year; i >= 2015; i--)
                                        {%>
                                    <option value="<%= i %>"><%= i + 543 %></option>
                                    <%}%>
                                </select>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row sort_type4" style="display: none">
                            <div class="col-md-1 "></div>
                            <label class="col-md-1  col-form-label text-left ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %><br />
                                /Year</label>
                            <div class="col-md-3 ">
                                <select id="select_year4" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <% for (int i = DateTime.Today.Year; i >= 2015; i--)
                                        {%>
                                    <option value="<%= i %>"><%= i + 543 %></option>
                                    <%}%>
                                </select>
                            </div>
                            <div class="col-md-1 "></div>
                            <label class="col-md-1 col-form-label text-left "></label>
                            <div class="col-md-3 ">
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row sort_type3" style="display: none">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %><br />
                                /Start Date</label>
                            <div class="col-md-3 ">

                                <div class="form-group has-successx">
                                    <input type="text" name="txtstart3" id="txtstart3" class="form-control datepicker" required="required" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %><br />
                                /End Date</label>
                            <div class="col-md-3 ">

                                <div class="form-group has-successx">
                                    <input type="text" name="txtend3" id="txtend3" class="form-control datepicker" required="required" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row formtype1" style="display: none">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105036") %><br />
                                /Start Time</label>
                            <div class="col-md-3 ">

                                <div class="form-group has-successx">
                                    <input type="time" name="time1" id="time1" class="form-control" />
                                    <%-- <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>--%>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105037") %><br />
                                /End Time</label>
                            <div class="col-md-3 ">

                                <div class="form-group has-successx">
                                    <input type="time" name="time2" id="time2" class="form-control" />
                                    <%--  <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>--%>
                                </div>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %><br />
                                /User Type</label>
                            <div class="col-md-3 ">

                                <asp:DropDownList ID="select_user_type" runat="server" ClientIDMode="Static"
                                    DataTextField="Text"
                                    DataValueField="Value" CssClass="selectpicker w-auto" data-style="select-with-transition" data-width="100%" data-size="7">
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102013") %><br />
                                /Department</label>
                            <div class="col-md-3 ">
                                <select id="select_department" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                    <% var q = getDepartment();
                                        foreach (var data in q)
                                        {%>
                                    <option value="<%= data.DepID %>"><%= data.departmentName %></option>
                                    <%}
                                    %>
                                </select>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105008") %><br />
                                /Full name</label>
                            <div class="col-md-3 ">
                                <uc1:TeacherAutocomplete runat="server" ID="TeacherAutocomplete" />
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
                                <button type="button" onclick="Getreports('example', false);" class="btn btn-fill btn-info">
                                    <span class="material-icons">search</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-right">
                                <a class="btn btn-fill btn-warning " href="/UpdateEmpStatus/EditsEmpWorkStatus.aspx" style="" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105009") %></a>
                                <button type="button" class="btn btn-fill btn-success " onclick="Reports_04.export_excel()">Export File</button>
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
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102145") %> / Result</h4>
                    </div>
                    <div class="card-body ">
                        <div class="report-container">

                            <asp:Literal ID="ltrHeaderReport" runat="server" />
                            <div class="row " style="overflow-y: auto;">
                                <br />
                                <div class="col-sm-12">
                                    <label id="explanation" style="display: block;">

                                        <%= string.Join(",", LeaveTypeList.Select(i => i.AbbrTH + "=" + i.TypeName).ToArray()  )%>
                                          <br />
                                        <%= string.Join(",", LeaveTypeList.Select(i => i.Abbr + "=" + i.TypeNameEN).ToArray()  )%>
                                       

                                        <%-- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133182") %>--%>
                                    </label>
                                </div>
                                <div class="col-sm-12">
                                    <fieldset>
                                        <asp:ListView ID="lvReport" runat="server">
                                        </asp:ListView>
                                        <table id="example" class="table-hover dataTable" style="" cellspacing="0" width="100%">
                                        </table>
                                    </fieldset>

                                </div>
                                <div class="col-sm-12">
                                    <span id="remark1" class="float-right" style="display: none"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105027") %>
                                    </span>
                                </div>
                                <div style="display: none">
                                    <asp:TextBox ID="txtHeadOfHuman" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox ID="txtHead" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <fieldset class="d-none" id="export_excel">
                                    <table id="table_exports" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
                                    </table>

                                </fieldset>
                                <div id="report4" class="d-none">
                                    <table id="table_exports4" class="" style="" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <table border="0" width="100%">
                                                    <tr>
                                                        <td colspan="15" style="font-size: 20px; text-align: center" id="report4-school"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="15" style="font-size: 20px; text-align: center;" id="report4-date"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="font-size: 16px; text-align: right;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %> : </td>
                                                        <td colspan="1" style="font-size: 16px; text-align: left;" id="report4-type"></td>
                                                        <td colspan="9" style="font-size: 16px; text-align: left;"></td>
                                                        <td colspan="1" style="font-size: 16px; text-align: right;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601088") %> </td>
                                                        <td colspan="2" style="font-size: 16px; text-align: left;" id="report4-print-date"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="font-size: 16px; text-align: right;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102013") %> : </td>
                                                        <td colspan="1" style="font-size: 16px; text-align: left;" id="report4-dept"></td>
                                                        <td colspan="9" style="font-size: 16px; text-align: left;"></td>
                                                        <td colspan="1" style="font-size: 16px; text-align: right;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> : </td>
                                                        <td colspan="2" style="font-size: 16px; text-align: left;" id="report4-print-time"></td>
                                                    </tr>
                                                    <%-- <tr>
                                         <td colspan="15" style="font-size:20px"></td>
                                     </tr>--%>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table id="report4-content">
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table border="0">
                                                    <tr>
                                                        <td colspan="15" style="text-align: center;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="15" style="text-align: center;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" style="font-size: 20px; text-align: center;">............................................</td>
                                                        <td colspan="7"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" style="text-align: center;"></td>
                                                        <td colspan="7"></td>
                                                    </tr>
                                                    <%--  <tr>
                                         <td></td>
                                         <td></td>
                                     </tr>--%>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">

    <%--  <script src="/app/Reports/Come2school/ReportCome2SchoolStudentJS.js?d=<%# DateTime.Now.ToString("ddMMyyyyHHmmssss") %>" type="text/javascript"></script>--%>
    <script src="ScriptReport.js?v=1" type="text/javascript"></script>
    <script src="Script/Reportmobile05.js?d=<%= DateTime.Now.ToString("ddMMyyyyHH") %>" type="text/javascript"></script>
    <script src="../Scripts/FileSaver.js" type="text/javascript"></script>
    <script src="../Scripts/freeze-table.js" type="text/javascript"></script>
    <%--    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" type="text/javascript"></script>--%>

    <script src="//cdnjs.cloudflare.com/ajax/libs/magnific-popup.js/1.0.0/jquery.magnific-popup.min.js" integrity="sha512-+m6t3R87+6LdtYiCzRhC5+E0l4VQ9qIT1H9+t1wmHkMJvvUQNI5MKKb7b08WL4Kgp9K0IBgHDSLCRJk05cFUYg==" crossorigin="anonymous"></script>

    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>

    <script type="text/javascript">
        moment.locale('th');
        var reports_data = [];
        var HeaderCSS = " text-align: center !important;vertical-align: middle !important; min-width: 100px;";
        var HeaderCSS_1 = " text-align: center !important;vertical-align: middle !important;padding:0px;min-width: 50px;";
        var HeaderCSS_2 = " text-align: center !important;vertical-align: middle !important;padding:0px;";
        var HeaderCSS_3 = " text-align: center !important;vertical-align: middle !important; min-width: 40px;";
        var HeaderCSS_4 = " text-align: center !important;vertical-align: middle !important; min-width: 150px;";
        var RowsCSS = "text-align: center !important; vertical-align: middle !important;padding:0px;";
        var RowsCSS_1 = "text-align: center !important; vertical-align: middle !important;";
        var RowsCSS_2 = "text-align: left !important; vertical-align: middle !important;";
        var RowsCSSHide = "display:none;";
        var Search = null;
        var CycleCSS = "font-size: 70%; border-radius: 100%;border: solid black 1px;padding-right: 0px;padding-left: 0px;padding-top: 0px;padding-bottom: 0px;height: 13px;text-align: center;color:red;";
        var availableValueEmployees = [];
        $(function () {
            $('select[id*=sort_type]').change(function () {
                switch_sort($(this).val());
            });
            switch_sort($('select[id*=sort_type]').val());

            //$('#plane').chosen();
            //$('.chosen-select-deselect').chosen({ allow_single_deselect: true });
            Reports_04 = new reports_04();

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

            $('#form_type').on('change', function () {
                var v = $(this).val();

                switch (v) {
                    case "0"://
                        $('.formtype1').hide();
                        break;
                    case "1"://
                        $('.formtype1').show();
                        break;
                }

            });

            if (jQuery.validator) {

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",
                });

                //$("#aspnetForm").validate({  // initialize the plugin
                //    errorPlacement: function (error, element) {
                //        var _class = element.attr('class');

                //        //if (_class.includes('--date-validate')) {
                //        //    error.insertAfter(element.parent());
                //        //}
                //        //else {

                //        //}
                //        error.insertAfter(element);
                //    }

                //});


                //$('#mysearchform').on('submit', function (e) {
                //    if ($('#aspnetForm').valid() == false) {

                //        e.preventDefault();
                //        e.stopPropagation();
                //        return false;
                //    }
                //});
            }

        });


        function switch_sort(sort_type) {

            $("div[class*=sort_type").hide();
            $(".sort_type" + sort_type).show();

            switch (sort_type) {
                case "0"://<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105010") %>
                    //$(".sort_type").hide();
                    //$($(".sort_type")[2]).show();
                    //$(".filterStatus").show();
                    $("#explanation").hide();
                    $('[class$=-table-wrap]').hide();
                    $('.formtype').hide();
                    break;
                case "1"://<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105011") %>
                    //$(".sort_type").hide();
                    //$(".sort_type_month").show();
                    //$($(".sort_type")[0]).show();
                    //$(".filterStatus").hide();
                    $("#explanation").show();
                    $('[class$=-table-wrap]').show();
                    $('.formtype').hide();
                    $('.formtype1').hide();
                    break;
                case "2":
                    //$(".sort_type").hide();
                    //$($(".sort_type")[1]).show();
                    $("#explanation").hide();
                    $('.formtype').hide();
                    $('.formtype1').hide();
                    break;
                //case "3": $(".sort_type").hide(); $($(".sort_type")[2]).show(); $($(".sort_type")[3]).show(); $("#explanation").hide(); break;
                case "3"://<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105013") %>
                    //$(".sort_type").hide();
                    //$($(".sort_type")[1]).show();
                    //$($(".sort_type")[2]).show();
                    //$(".filterStatus").hide();
                    $("#explanation").hide();
                    $('[class$=-table-wrap]').hide();
                    $('.formtype').show();
                    $('.formtype1').hide();
                    $('#form_type').trigger('change');
                    break;
                case "4"://<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105012") %>
                    //$(".sort_type").hide();
                    //$(".sort_type_month").hide();
                    //$($(".sort_type")[0]).show();
                    //$(".filterStatus").hide();
                    $("#explanation").hide();
                    $('[class$=-table-wrap]').hide();
                    $('.formtype').hide();
                    $('.formtype1').hide();
                    break;
            }
        }

        function Getreports(table_name, export_file) {

            if ($('#aspnetForm').valid() == false) {
                //e.preventDefault();
                //e.stopPropagation();
                return false;
            }

            var dStart = "", dEnd = "";

            Reports_04.report_txt = $('select[id*=plane] option:selected').text();

            if ($('select[id*=sort_type]').val() === "1") {
                //dStart = "1/" + $("#select_month1").val() + "/" + $("#select_year1 option:selected").val();
                dStart = moment("1/" + $("#select_month1").val() + "/" + $("#select_year1 option:selected").val(), 'DD/MM/YYYY');
                dEnd = dStart;
                Reports_04.report_txt += " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133174") %> " + $("#select_month1 option:selected").text() + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %> " + $("#select_month1 option:selected").text();
            }
            else if ($('select[id*=sort_type]').val() === "0") {

                dStart = $("#txtstart0").data("DateTimePicker").date();
                dEnd = dStart;
            }
            else if ($('select[id*=sort_type]').val() === "3") {

                dStart = $("#txtstart3").data("DateTimePicker").date();
                dEnd = $("#txtend3").data("DateTimePicker").date();

            }
            else if ($('select[id*=sort_type]').val() === "4") {
                dStart = moment('01/01/' + $("select[id*=select_year4] option:selected").val(), 'DD/MM/YYYY');
                dEnd = dStart;
                //dStart = '01/01/' + $("select[id*=select_year4] option:selected").text();//($("select[id*=select_year4] option:selected").text() - 543);
                //dEnd = '01/01/' + $("select[id*=select_year4] option:selected").text();//($("select[id*=select_year4] option:selected").text() - 543);
            }

            Search = {
                "term_id": $('select[id*=semister]').val(),
                "level2_id": $('select[id*=ddlSubLV2]').val(),
                "sort_type": $('select[id*=sort_type]').val(),
                "dStart": moment(dStart, 'DD/MM/YYYY').format('MM/DD/YYYY'),
                "dEnd": moment(dEnd, 'DD/MM/YYYY').format('MM/DD/YYYY'),
                "plane_Id": $('select[id*=plane]').val(),
                "department_id": $("#select_department option:selected").val(),
                "user_type": $("#select_user_type option:selected").val(),
                "emp_id": TAC.GetUserID(),
                "status": $("#status").val(),
                "formtype": $("#form_type").val(),
                "time1": $('#time1').val(),
                "time2": $('#time2').val(),
            };

            //File Name Reports
            switch ($('select[id*=sort_type]').val()) {
                case "0":
                    var _d = $("#txtstart0").data("DateTimePicker").date();//moment($("#txtstart0").val(), "DD/MM/YYYY");
                    Reports_04.report_txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133183") %> " + _d.format("DD MMMM") + ' ' + _d.add(543, 'years').format("YYYY");// + ' ' + (+_d.format("YYYY") + 543);
                    break;
                case "1":
                    Reports_04.report_txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133184") %> " + $("#select_month1 option:selected").text() + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %> " + $("#select_year1 option:selected").text();
                    break;
                case "3":
                    var _d1 = $("#txtstart3").data("DateTimePicker").date();
                    var _d2 = $("#txtend3").data("DateTimePicker").date();
                    $('#report4-date').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133185") %> ' + (_d1.format("dddd ที่ D MMMM") + ' ' + (+_d1.add(543, 'years').format("YYYY"))) + ' - ' + (_d2.format("dddd ที่ D MMMM") + ' ' + (+_d2.add(543, 'years').format("YYYY"))));
                    //Reports_04.report_txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133188") %> " + $("#txtstart").val() + ' - ' + $("#txtend").val() ;
                    break;
                case "4":
                    $('#report4-date').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133186") %> ' + $("#select_year4 option:selected").text());
                    //Reports_04.report_txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133187") %> " + $("select[id*=select_year] option:selected").text();
                    break;
                default:
                    return "";
            }

            $("body").mLoading('show');
            PageMethods.returnlist(Search, function (e) {
                Reports_04.reports_data = e;
                console.log(e);
                $('#remark1').hide();
                if (Search.sort_type === "0") {//day
                    $('#remark1').show();
                    Reports_04.RenderHTML_02(table_name, export_file);
                }
                else if (Search.sort_type === "3" || Search.sort_type === "4") {//year , select range

                    if (Search.formtype == "1") {
                        Reports_04.Render3Form2(table_name, export_file);
                    } else {
                        Reports_04.RenderHTML_04(table_name, export_file);
                    }
                }
                else {//monthly
                    Reports_04.RenderHtml(table_name, export_file);
                }
                $("body").mLoading('hide');
            }, function (e) {
                $("body").mLoading('hide');
            });
        }


    </script>
</asp:Content>

