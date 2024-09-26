<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="FingerprintPayment.studentclass.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Fonts and icons -->
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
    <%-- <script src="/Content/Material/assets/js/plugins/moment.min.js"></script>--%>
    <script src="/Content/Material/assets/js/plugins/moment-with-locales.js"></script>
    <!-- Plugin for the DateTimePicker, full documentation here: https://eonasdan.github.io/bootstrap-datetimepicker/ -->
    <script src="/Content/Material/assets/js/plugins/bootstrap-datetimepicker.th.min.js"></script>
    <script src="Scripts/studentclass.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmss") %>" type="text/javascript"></script>
    <%--<link href="Styles/jquery-multi-selection.css" rel="stylesheet" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="Scripts/jquery.multi-selection.v1.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />
    <script src="/Scripts/jquery-confirm.js"></script>
    <style type="text/css">
        .box-detail {
            border: solid 1px #E8E8E8;
        }

            .box-detail select[multiple] {
                border-radius: 0px;
            }

            .box-detail span {
                font-size: 18px;
            }

        .nav.nav-tabs li.active a {
            border-radius: unset;
            border-top: solid 2px red !important;
        }

        .div-datepicker {
            margin-bottom: 0px;
        }
            .div-datepicker input {
                height: 40px;
            }

        .bootstrap-datetimepicker-widget {
            min-width: 228px;
            font-size: 18px;
        }

        .picker-switch {
            font-size: 20px;
        }

        .datepicker:before, .datepicker:after {
            display: none;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />

    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist" style="background-color: #fff;">
        <li role="presentation" class="active"><a href="#tab1" aria-controls="profile" role="tab" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101308") %></a></li>
        <li role="presentation"><a href="#tab2" aria-controls="messages" role="tab" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101309") %></a></li>
        <li role="presentation"><a href="#tab3" aria-controls="settings" role="tab" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101310") %></a></li>
        <li role="presentation" class="hidden"><a href="#tab4" aria-controls="settings" role="tab" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133340") %></a></li>
    </ul>
    <!-- Tab panes -->
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane box-detail full-card box-content active" id="tab1">
            <div class="row">
                <div class="col-lg-5  box-detail">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></h1>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-year" group-data="1">
                                <%= strYearOption %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101316") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-term" group-data="1">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101313") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-class" group-data="1">
                                <%
                                    string strOption = "<option value=\"\" selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %></option>";
                                    string strOption2 = "<option value=\"\" selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %></option>";
                                    List<int> ClassupId = new List<int> { 41, 47, 50, 53, 56, 58 };
                                    foreach (var data in classesData)
                                    {
                                        strOption += "<option value=\"" + data.class_id + "\">" + data.class_name + "</option>";
                                        if (data.isGraduate)
                                        {
                                            strOption2 += "<option value=\"" + data.class_id + "\" dayProfessionalStandard=\"" + (data.class_name.StartsWith("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>") || data.class_name.StartsWith("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>") ? "true" : "false") + "\" >" + data.class_name + "</option>";
                                        }
                                    }%>
                                <%= strOption %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-room" group-data="1">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101281") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row" style="height: 42px;">
                        <%--    <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></span>
                        </div>
                        <div class="col-lg-6">
                            <input class="form-control" group-data="1" />
                        </div>
                        <div class="col-lg-2">
                            <div class="btn btn-primary search-btn" group-data="1" style="font-size: 20px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></div>
                        </div>--%>
                    </div>
                    <div class="row--space">
                    </div>
                </div>
                <div class="col-lg-2 move-panel">
                    <%--      <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</p>
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101316") %> :</p>--%>
                </div>
                <div class="col-lg-5  box-detail">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101311") %></h1>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-year" group-data="2" disabled>
                                <%= strYearOption %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101316") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-term" group-data="2" disabled>
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101313") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-class" group-data="2" disabled>
                                <%= strOption %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-room" group-data="2">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101281") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-12 text-center" style="height: 42px;">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %></span>
                            <span class="student-number">0</span>
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                </div>
            </div>
            <div class="row jp-multiselect-1">
                <div class="col-lg-5">
                    <div class="row from-panel">
                        <select class="form-control" id="select-student-name-1" group-data="1" multiple="multiple" style="min-height: 450px;">
                        </select>
                    </div>
                </div>
                <div class="col-lg-2 center">
                    <button type="button" class="btn btn-move-all-right btn-primary" style="width: 100px;">>></button>
                    <br />
                    <br />
                    <button type="button" class="btn btn-move-selected-right btn-default" style="width: 100px;"><i class="fa  fa-angle-double-right"></i></button>
                    <br />
                    <br />
                    <button type="button" class="btn btn-move-selected-left btn-default" style="width: 100px;"><i class="fa  fa-angle-double-left"></i></button>
                    <br />
                    <br />
                </div>
                <div class="col-lg-5">
                    <div class="row to-panel">
                        <select class="form-control" id="select-student-name-2" group-data="2" multiple="multiple" style="min-height: 450px;">
                        </select>
                    </div>
                </div>
            </div>
            <div class="row--space"></div>
            <div class="row">
                <div class="col-lg-4">
                </div>
                <div class="col-lg-4 btn btn-success">
                    <i class="fa fa-save"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>
               
                </div>
                <div class="col-lg-4">
                </div>
            </div>
        </div>
        <div role="tabpanel" class="tab-pane box-detail full-card box-content " id="tab2">
            <div class="row">
                <div class="col-lg-5  box-detail">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></h1>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-year" group-data="3">
                                <%= strYearOption %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101316") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-term" group-data="3">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101313") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-class" group-data="3">
                                <%= strOption %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-room" group-data="3">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101281") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row" style="height: 23px;">
                        <%--  <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></span>
                        </div>
                        <div class="col-lg-6">
                            <input class="form-control" />
                        </div>
                        <div class="col-lg-2">
                            <div class="btn btn-primary" style="font-size: 20px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></div>
                        </div>--%>
                    </div>
                    <div class="row--space">
                    </div>
                </div>
                <div class="col-lg-2">
                    <%-- <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</p>
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101316") %> :</p>--%>
                </div>
                <div class="col-lg-5  box-detail">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101311") %></h1>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></span>
                        </div>
                        <div class="col-lg-8">
                            <span id="text-year"></span>
                            <%--        <select class="select2 form-control select-year" group-data="4">
                                <%= strYearOption %>
                            </select>--%>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101316") %></span>
                        </div>
                        <div class="col-lg-8">
                            <span id="text-term"></span>
                            <input id="termId" type="hidden" />
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-class" group-data="4">
                                <%= strOption %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-room" group-data="4">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101281") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row" style="height: 48px;">
                        <div class="col-lg-12 text-center" style="height: 42px;">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %></span>
                            <span class="student-number">0</span>
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                </div>
            </div>
            <div class="row jp-multiselect-2">
                <div class="col-lg-5">
                    <div class="row from-panel">
                        <select class="form-control" id="select-student-name-3" group-data="3" multiple="multiple" style="min-height: 450px;">
                        </select>
                    </div>
                </div>
                <div class="col-lg-2 center">
                    <button type="button" class="btn btn-move-all-right btn-primary" style="width: 100px;">>></button>
                    <br />
                    <br />
                    <button type="button" class="btn btn-move-selected-right btn-default" style="width: 100px;"><i class="fa  fa-angle-double-right"></i></button>
                    <br />
                    <br />
            <%--<button type="button" class="btn btn-move-selected-left btn-default" style="width: 100px;"><i class="fa  fa-angle-double-left"></i></button>
                    <br />
                    <br />--%>
                </div>
                <div class="col-lg-5">
                    <div class="row to-panel">
                        <select class="form-control" id="select-student-name-4" group-data="4" multiple="multiple" style="min-height: 450px;">
                        </select>
                    </div>
                </div>
            </div>
            <div class="row--space">
                <div class="row">
                    <div class="col-lg-4">
                    </div>
                    <div class="col-lg-4 btn btn-success">
                        <i class="fa fa-save"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>
                   
                    </div>
                    <div class="col-lg-4">
                    </div>
                </div>
            </div>
        </div>
        <div role="tabpanel" class="tab-pane box-detail box-content full-card box-content " id="tab3">
            <div class="row">
                <div class="col-lg-5  box-detail">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></h1>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-year" group-data="5">
                                <%= strYearOption %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101316") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-term" group-data="5">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101313") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-class" group-data="5">
                                <%= strOption2 %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-room" group-data="5">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101281") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row" style="height: 48px;">
                        <%-- <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></span>
                        </div>
                        <div class="col-lg-6">
                            <input class="form-control" />
                        </div>
                        <div class="col-lg-2">
                            <div class="btn btn-primary" style="font-size: 20px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></div>
                        </div>--%>
                    </div>
                    <div class="row--space">
                    </div>
                </div>
                <div class="col-lg-2">
                    <%--                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</p>
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101316") %> :</p>--%>
                </div>
                <div class="col-lg-5  box-detail">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101311") %></h1>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-year" disabled="disabled" group-data="6">
                                <%= strYearOption %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101316") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-term" disabled="disabled" group-data="6">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101313") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-class" disabled="disabled" group-data="6">
                                <%= strOption2 %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101317") %></span>
                        </div>
                        <div class="col-lg-8">
                            <%--<input class="form-control datepicker" id="dayGraduate" />--%>
                            <div class="form-group div-datepicker">
                                <input id="dayGraduate" name="dayGraduate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1; margin: 9px 14px 9px 9px;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row" target="dayProfessionalStandard" style="display: none;">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206521") %></span>
                        </div>
                        <div class="col-lg-8">
                            <input class="form-control datepicker" id="dayProfessionalStandard" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 text-center" style="height: 48px;">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %></span>
                            <span class="student-number">0</span>
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                </div>
            </div>
            <div class="row jp-multiselect-3">
                <div class="col-lg-5">
                    <div class="row from-panel">
                        <select class="form-control" id="select-student-name-5" group-data="5" multiple="multiple" style="min-height: 450px;">
                        </select>
                    </div>
                </div>
                <div class="col-lg-2 center">
                    <button type="button" class="btn btn-move-all-right btn-primary" style="width: 100px;">>></button>
                    <br />
                    <br />
                    <button type="button" class="btn btn-move-selected-right btn-default" style="width: 100px;"><i class="fa  fa-angle-double-right"></i></button>
                    <br />
                    <br />
                    <button type="button" class="btn btn-move-selected-left btn-default" style="width: 100px;"><i class="fa  fa-angle-double-left"></i></button>
                    <br />
                    <br />
                </div>
                <div class="col-lg-5">
                    <div class="row to-panel">
                        <select class="form-control" id="select-student-name-6" group-data="6" multiple="multiple" style="min-height: 450px;">
                        </select>
                    </div>
                </div>
            </div>
            <div class="row--space">
                <div class="row">
                    <div class="col-lg-4">
                    </div>
                    <div class="col-lg-4 btn btn-success">
                        <i class="fa fa-save"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>
                   
                    </div>
                    <div class="col-lg-4">
                    </div>
                </div>
            </div>
        </div>
        <div role="tabpanel" class="tab-pane box-detail box-content full-card box-content " id="tab4">
            <div class="row">
                <div class="col-lg-5  box-detail">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></h1>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-year" group-data="5">
                                <%= strYearOption %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101316") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-term" group-data="5">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101313") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-class" group-data="5">
                                <%= strOption2 %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-room" group-data="5">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101281") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row" style="height: 48px;">
                        <%--<div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></span>
                        </div>
                        <div class="col-lg-6">
                            <input class="form-control" />
                        </div>
                        <div class="col-lg-2">
                            <div class="btn btn-primary" style="font-size: 20px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></div>
                        </div>--%>
                    </div>
                    <div class="row--space">
                    </div>
                </div>
                <div class="col-lg-2">
                    <%--<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</p>
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101316") %> :</p>--%>
                </div>
                <div class="col-lg-5  box-detail">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101311") %></h1>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-year" disabled="disabled" group-data="6">
                                <%= strYearOption %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101316") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-term" disabled="disabled" group-data="6">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101313") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %></span>
                        </div>
                        <div class="col-lg-8">
                            <select class="select2 form-control select-class" disabled="disabled" group-data="6">
                                <%= strOption2 %>
                            </select>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101317") %></span>
                        </div>
                        <div class="col-lg-8">
                            <%--<input class="form-control datepicker" id="dayGraduate" />--%>
                            <div class="form-group div-datepicker">
                                <input id="dayGraduate" name="dayGraduate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1; margin: 9px 14px 9px 9px;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 text-center" style="height: 48px;">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %></span>
                            <span class="student-number">0</span>
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                    </div>
                    <div class="row--space">
                    </div>
                </div>
            </div>
            <div class="row jp-multiselect-3">
                <div class="col-lg-5">
                    <div class="row from-panel">
                        <select class="form-control" id="select-student-name-5" group-data="5" multiple="multiple" style="min-height: 450px;">
                        </select>
                    </div>
                </div>
                <div class="col-lg-2 center">
                    <button type="button" class="btn btn-move-all-right btn-primary" style="width: 100px;">>></button>
                    <br />
                    <br />
                    <button type="button" class="btn btn-move-selected-right btn-default" style="width: 100px;"><i class="fa  fa-angle-double-right"></i></button>
                    <br />
                    <br />
                    <%--             <button type="button" class="btn btn-move-selected-left btn-default" style="width: 100px;"><i class="fa  fa-angle-double-left"></i></button>
                    <br />
                    <br />--%>
                </div>
                <div class="col-lg-5">
                    <div class="row to-panel">
                        <select class="form-control" id="select-student-name-6" group-data="6" multiple="multiple" style="min-height: 450px;">
                        </select>
                    </div>
                </div>
            </div>
            <div class="row--space">
                <div class="row">
                    <div class="col-lg-4">
                    </div>
                    <div class="col-lg-4 btn btn-success">
                        <i class="fa fa-save"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>
                   
                    </div>
                    <div class="col-lg-4">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
