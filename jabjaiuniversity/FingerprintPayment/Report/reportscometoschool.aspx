<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="reportscometoschool.aspx.cs" Inherits="FingerprintPayment.Report.reportscometoschool" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="/app/Reports/Come2school/ReportCome2SchoolStudentJS.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>

    <script type="text/javascript">
        function search() {
            var dt = new Date();
            var day = dt.getDate() + "/" + ("0" + (dt.getMonth() + 1)).slice(-2) + "/" + dt.getFullYear();
            search_data = {
                daystart: $("#txtstart").val(),
                dayend: $("#txtend").val(),
                user_id: $("#txtid").val(), level_id: $("select[id*=ddlsublevel] option:selected").val(),
                level2_id: $("select[id*=ddlSubLV2] option:selected").val(), status: "",
                level2: $("select[id*=ddlSubLV2] option:selected").text(),
                level: $("select[id*=ddlsublevel] option:selected").text()
            };

            if (search_data.user_id !== "") {
                reports03(search_data.daystart, search_data.dayend, search_data.level2_id, "", search_data.user_id, search_data.level, search_data.level2);
            }
            else if (search_data.level2_id !== "") {
                if (search_data.daystart !== "" && search_data.dayend !== "") {
                    if (search_data.daystart < search_data.dayend) {
                        reports01(search_data.daystart, search_data.dayend);
                    } else if (search_data.daystart === search_data.dayend || search_data.daystart === search_data.dayend) {
                        reports03(search_data.daystart, null, search_data.level2_id, "", "", search_data.level, search_data.level2);
                    }
                }
                else if (search_data.daystart !== "") {
                    reports03(search_data.daystart, null, search_data.level2_id, "", "", search_data.level, search_data.level2);
                }
                else {
                    reports03(day, search_data.level2_id, null, search_data.level_id, "", "", search_data.level, search_data.level2);
                }
            }
            else if (search_data.level_id !== "") {
                if (search_data.daystart !== "" && search_data.dayend !== "") {
                    if (daystart < dayend) {
                        reports01(search_data.daystart, search_data.dayend);
                    } else if (daystart === dayend || search_data.daystart === search_data.dayend) {
                        reports02(search_data.daystart);
                    }
                }
                else if (search_data.daystart !== "") {
                    reports02(search_data.daystart);
                }
                else {
                    reports02(day);
                }
            }
            else {
                if (search_data.dayend !== "" && search_data.daystart !== "") {
                    reports01(search_data.daystart, search_data.dayend);
                } else if (search_data.dayend === "") {
                    reports01(search_data.daystart, search_data.daystart);
                }
                else {
                    reports01(day, day);
                }
            }
        }

        function reports01(daystart, dayend) {
            var dt = new Date();
            var day = dt.getDate() + "/" + ("0" + (dt.getMonth() + 1)).slice(-2) + "/" + dt.getFullYear();
            var thead = {
                schoolname: $("input[id*=hdfschoolname]").val(),
                day: daystart === "" ? day : daystart + (dayend === "" ? "" : " - " + dayend),
                dayprint: dt.toLocaleDateString(),
                timeprint: dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds(),
                year: $("select[id*=ddlyear] option:selected").text(),
                term: $("select[id*=semister] option:selected").text(),
                level: $('#ctl00_MainContent_ddlsublevel option:selected').text(),
                level2: $('#ctl00_MainContent_ddlSubLV2 option:selected').text()
            };
            var search_data = {
                daystart: daystart, dayend: dayend,
                user_id: null, level_id: null,
                level2_id: null, status: null
            };
            PageMethods._staticInstance.reports01(search_data,
                function (result) {
                    var data = {
                        tbody: jQuery.parseJSON(result),
                        thead: thead
                    };
                    var template = $('#templatesortbystudent').html();
                    Mustache.parse(template);   // optional, speeds up future uses
                    var rendered = Mustache.render(template, data);
                    $("#target").show();
                    $('#target').html(rendered);
                },
                function (result) {
                    //window.location.href = "/Default.aspx";
                    //alert(result);
                });
        }

        function reports02(daystart) {
            var dt = new Date();
            var day = dt.getDate() + "/" + ("0" + (dt.getMonth() + 1)).slice(-2) + "/" + dt.getFullYear();
            var thead = {
                schoolname: $("input[id*=hdfschoolname]").val(),
                day: daystart,
                dayprint: dt.toLocaleDateString(),
                timeprint: dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds(),
                year: $("select[id*=ddlyear] option:selected").text(),
                term: $("select[id*=semister] option:selected").text(),
                level: $('#ctl00_MainContent_ddlsublevel option:selected').text(),
                level2: $('#ctl00_MainContent_ddlSubLV2 option:selected').text()
            };
            $day_reports = daystart;
            var search_data = {
                daystart: daystart, dayend: null,
                user_id: null, level_id: null,
                level2_id: null, status: null
            };
            PageMethods._staticInstance.reports02(search_data,
                function (result) {
                    var data = {
                        tbody: jQuery.parseJSON(result),
                        thead: thead,
                        "check_zero": function () {
                            return function (text, render) {
                                var result = '';
                                var _tmp = 0;
                                if (text.indexOf("status_0") > -1) {
                                    _tmp = this.status_0;
                                }
                                else if (text.indexOf("status_1") > -1) {
                                    _tmp = this.status_1;
                                }
                                else if (text.indexOf("status_2") > -1) {
                                    _tmp = this.status_2;
                                }
                                else if (text.indexOf("status_3") > -1) {
                                    _tmp = this.status_3;
                                }
                                else if (text.indexOf("status_4") > -1) {
                                    _tmp = this.status_4;
                                }
                                else if (text.indexOf("status_5") > -1) {
                                    _tmp = this.status_5;
                                }

                                if (_tmp === 0) {
                                    result = "<td class=\"center\">0</td>";
                                }
                                else {
                                    result = render(text);
                                }
                                return result;
                            }
                        }
                    };
                    var template = $('#template_reports_01').html();
                    Mustache.parse(template);   // optional, speeds up future uses
                    var rendered = Mustache.render(template, data);
                    $("#target").show();
                    $('#target').html(rendered);
                }, function (result) {
                    //window.location.href = "/Default.aspx";
                    //alert(result);
                });
        }

        function reports03(daystart, dayend, level2_id, status, user_id, level, level2) {
            var dt = new Date();
            var day = dt.getDate() + "/" + ("0" + (dt.getMonth() + 1)).slice(-2) + "/" + dt.getFullYear();
            var thead = {
                schoolname: $("input[id*=hdfschoolname]").val(),
                day: daystart,
                dayprint: dt.toLocaleDateString(),
                timeprint: dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds(),
                year: $("select[id*=ddlyear] option:selected").text(),
                term: $("select[id*=semister] option:selected").text(),
                header_level: level,
                header_level2: level2,
                student_id: $studentid,
                student_name: $("#txtname").val(),
            };
            search_data = {
                daystart: daystart, dayend: dayend,
                user_id: user_id === "" ? null : user_id, level_id: null,
                level2_id: level2_id, status: status === "" ? null : status
            };
            PageMethods._staticInstance.reports03(search_data,
                function (result) {
                    var data = {
                        tbody: jQuery.parseJSON(result),
                        thead: thead
                    };
                    var template = $('#template_reports_02').html();
                    Mustache.parse(template);   // optional, speeds up future uses
                    var rendered = Mustache.render(template, data);
                    $("#target").show();
                    $('#target').html(rendered);
                }, function (result) {
                    //window.location.href = "/Default.aspx";
                    //alert(result);
                });
        }

    </script>
    <style type="text/css">
        @media (max-width: 999px) {
            .report-container {
                font-size: 18px;
            }

            label {
                font-weight: normal;
                font-size: 18px;
            }

            legend {
                padding-left: 30px;
                font-size: 18px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 18px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }

            .table-show-result {
                font-size: 20px;
            }
        }

        @media (min-width: 1000px) and (max-width: 1199px) {
            .report-container {
                font-size: 22px;
            }

            label {
                font-weight: normal;
                font-size: 22px;
            }

            legend {
                padding-left: 30px;
                font-size: 22px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 22px;
                width: 100%;
                padding-left: 30px;
                padding-right: 30px;
            }

            .table-show-result {
                font-size: 22px;
            }
        }

        @media (min-width: 1200px) {
            .report-container {
                font-size: 26px;
            }

            label {
                font-weight: normal;
                font-size: 26px;
            }

            legend {
                padding-left: 30px;
                font-size: 26px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 26px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }

            .table-show-result {
                font-size: 26px;
            }
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdfschoolname" runat="server" />
    <div class="report-container">
        <div class="row student">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlyear" runat="server" class="form-control">
                        <asp:ListItem Text="2558" Value="2557" Selected="True" />
                        <asp:ListItem Text="2559" Value="2557" Selected="False" />
                        <asp:ListItem Text="2560" Value="2557" Selected="False" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="semister" runat="server" class="form-control">
                        <asp:ListItem Text="1" Value="1" Selected="True" />
                        <asp:ListItem Text="2" Value="2" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row student">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlsublevel" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlSubLV2" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603047") %></label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" class='form-control' id="txtid" style="display: none;" />
                    <input type="text" class='form-control' id="txtname" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" id="txtstart" readonly="true" class="form-control datepicker col-md-6" />
                    <%-- <div class="input-group">
                        <input type="text" id="txtstart" readonly="true" class="form-control datepicker" /><div
                            class="input-group-addon">
                            <i class="glyphicon glyphicon-calendar"></i>
                        </div>
                    </div>--%>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" id="txtend" class="form-control datepicker" readonly="true" />
                </div>
            </div>
        </div>
        <div class="row hidden">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="status" runat="server" class="form-control col-sm-6">
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" Selected="True" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>" Value="0" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>" Value="1" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>" Value="3" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %>" Value="4" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %>" Value="5" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %>" Value="10" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <input type="button" class="btn btn-primary button-custom" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="search();" />
            </div>
        </div>
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
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %><br />
                        <span class='text-large' id="status01">0</span>
                    </p>
                </div>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-warning'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %><br />
                        <span class='text-large' id="status02">0</span>
                    </p>
                </div>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-danger'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %><br />
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
            <div class="col-lg-8 col-md-8 col-sm-8">
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2">
                <div class="btn btn-success button-custom" id="exportfile">Export File</div>
            </div>
        </div>
        <asp:Literal ID="ltrHeaderReport" runat="server" />
        <div class="row border-bottom">
            <br />
            <div class="col-sm-12">
                <fieldset>
                    <asp:ListView ID="lvReport" runat="server">
                    </asp:ListView>
                    <div id="target"></div>
                    <script id="templatesortbystudent" type="x-tmpl-mustache">
                    <table class="table table-condensed table-bordered table-show-result" id="example" cellspacing="0" width="100%">
                        {{#thead}}
                        <thead id="myHeader" class="hidden">
                            <tr>
                                <th style="text-align: center; font-size: 26px; border-width: 0px 1px;" id="school" colspan="9">{{schoolname}}
                                </th>
                            </tr>
                            <tr>
                                <th colspan="9" style="text-align: center; font-size: 24px; border-width: 0px 1px;" id="headdatail"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133189") %>
                                </th>
                            </tr>
                            <tr>
                                <th colspan="9" style="text-align: center; font-size: 24px; border-width: 0px 1px;" id="dayfall"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133167") %> {{day}}
                                </th>
                            </tr>
                            <tr>
                                <th colspan="9" style="text-align: right; font-size: 20px; border-width: 0px 1px;" id="dayshort"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601088") %>&nbsp; {{dayprint}}
                                </th>
                            </tr>
                            <tr>
                                <th colspan="9" style="text-align: right; font-size: 20px; border-width: 0px 1px;" id="timetoday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> :&nbsp; {{timeprint}}
                                </th>
                            </tr>
                            <tr style="font-size: 20px;">
                                <th colspan="2" style="text-align: right; width: 20%; border-width: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> : 
                                </th>
                                <th style="width: 30%; border-width: 0px;">&nbsp; {{year}}
                                </th>
                                <th style="text-align: right; width: 20%; border-width: 0px;" colspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> : 
                                </th>
                                <th colspan="4" style="width: 30%; border-width: 0px;">&nbsp; {{term}}
                                </th>
                            </tr>
                            <tr style="font-size: 20px;">
                                <th colspan="2" style="text-align: right; width: 20%; border-width: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> : 
                                </th>
                                <th style="width: 30%; border-width: 0px;" >&nbsp; {{level}}
                                </th>
                                <th style="text-align: right; width: 20%; border-width: 0px;" colspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> : 
                                </th>
                                <th colspan="4" style="width: 30%; border-width: 0px;">&nbsp; {{level2}}
                                </th>
                            </tr>
                            <tr>
                                <th colspan="9" style="width: 30%; border-width: 0px;">
                                    <br />
                                </th>
                            </tr>
                        </thead>
                        {{/thead}}
                        {{#tbody}}
                        <tbody>
                            <tr id='headder' style='font-weight: bold; font-size:20px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>
                                <td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td>
                                <td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></td>
                                <td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305019") %></td>
                                <td id='headder'colspan='6'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td>
                            </tr>
                            <tr id='headder' style='font-weight: bold; font-size:18px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %></td>
                            </tr>
                            {{#data}}
                                <tr>
                                    <td class="center">{{index}}</td>
                                    <td class="center" style='color:royalblue; cursor:pointer;' onclick="reports02('{{day}}')">{{day}}</td>
                                    <td class="center">{{studentnumber}}</td>
                                    <td class="center">{{status_0}}</td>
                                    <td class="center">{{status_1}}</td>
                                    <td class="center">{{status_2}}</td>
                                    <td class="center">{{status_3}}</td>
                                    <td class="center">{{status_4}}</td>
                                    <td class="center">{{status_5}}</td>
                                </tr>
                            {{/data}}
                            {{#footer}}
                                 <tr id='total' style='font-weight: bold;' rowspan=4>
                                    <td class='right' colspan=2 style='border:0px;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206458") %></td>
                                    <td colspan=7 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303005") %> {{status_0}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_0}} %
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_0}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_0}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_0}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_0}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=7 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %> {{status_1}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_1}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_1}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_1}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_1}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_1}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=7 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206204") %> {{status_2}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_2}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_2}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_2}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_2}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_2}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=7 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %> {{status_3}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_3}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_3}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_3}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_3}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_3}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=7 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %> {{status_4}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_4}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_4}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_4}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_4}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_4}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=7 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %> {{status_5}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_5}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_5}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_5}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_5}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_5}} %
                                    </td>
                                </tr>
                            {{/footer}}
                        </tbody>
                        {{/tbody}}
                    </table>
                    </script>
                    <script id="template_reports_01" type="x-tmpl-mustache">
                    <table class="table table-condensed table-bordered table-show-result" id="example" cellspacing="0" width="100%">
                        {{#thead}}
                        <thead id="myHeader" class="hidden">
                            <tr>
                                <th style="text-align: center; font-size: 26px; border-width: 0px 1px;" id="school" colspan="10">{{schoolname}}
                                </th>
                            </tr>
                            <tr>
                                <th colspan="10" style="text-align: center; font-size: 24px; border-width: 0px 1px;" id="headdatail"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133189") %>
                                </th>
                            </tr>
                            <tr>
                                <th colspan="10" style="text-align: center; font-size: 24px; border-width: 0px 1px;" id="dayfall"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133167") %> {{day}}
                                </th>
                            </tr>
                            <tr>
                                <th colspan="10" style="text-align: right; font-size: 20px; border-width: 0px 1px;" id="dayshort"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601088") %>&nbsp; {{dayprint}}
                                </th>
                            </tr>
                            <tr>
                                <th colspan="10" style="text-align: right; font-size: 20px; border-width: 0px 1px;" id="timetoday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> :&nbsp; {{timeprint}}
                                </th>
                            </tr>
                            <tr style="font-size: 20px;">
                                <th colspan="2" style="text-align: right; width: 20%; border-width: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> : 
                                </th>
                                <th style="width: 30%; border-width: 0px;" >&nbsp; {{year}}
                                </th>
                                <th style="text-align: right; width: 20%; border-width: 0px;" colspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> : 
                                </th>
                                <th colspan="5" style="width: 30%; border-width: 0px;">&nbsp; {{term}}
                                </th>
                            </tr>
                            <tr style="font-size: 20px;">
                                <th colspan="2" style="text-align: right; width: 20%; border-width: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> : 
                                </th>
                                <th style="width: 30%; border-width: 0px;" >&nbsp; {{level}}
                                </th>
                                <th style="text-align: right; width: 20%; border-width: 0px;" colspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> : 
                                </th>
                                <th colspan="5" style="width: 30%; border-width: 0px;">&nbsp; {{level2}}
                                </th>
                            </tr>
                            <tr>
                                <th colspan="10" style="width: 30%; border-width: 0px;">
                                    <br />
                                </th>
                            </tr>
                        </thead>
                        {{/thead}}
                        {{#tbody}}
                        <tbody>
                            <tr id='headder' style='font-weight: bold; font-size:20px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>
                                <td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td>
                                <td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></td>
                                <td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></td>
                                <td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305019") %></td>
                                <td id='headder'colspan='6'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td>
                            </tr>
                            <tr id='headder' style='font-weight: bold; font-size:18px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %></td>
                            </tr>
                            {{#data}}
                                {{#level2}}
                                <tr>
                                    <td class="center">{{index}}</td>
                                    {{#show_levelname}}
                                    <td style='color:royalblue;' >{{levelname}}</td>
                                    {{/show_levelname}}
                                    {{^show_levelname}}
                                    <td style='color:royalblue;' ></td>
                                    {{/show_levelname}}
                                    <td style='color:royalblue;' class="center" >{{level2name}}</td>
                                    <td style='color:royalblue; cursor:pointer;' class="center" onclick="reports03($day_reports,null,'{{level2id}}','0','','{{levelname}}','{{level2name}}')">{{studentnember}}</td>
                                    {{#check_zero}}
                                    <td style='color:royalblue; cursor:pointer;' class="center" onclick="reports03($day_reports,null,'{{level2id}}','1','','{{levelname}}','{{level2name}}')">{{status_0}}</td>
                                    {{/check_zero}}
                                    {{#check_zero}}
                                    <td style='color:royalblue; cursor:pointer;' class="center" onclick="reports03($day_reports,null,'{{level2id}}','3','','{{levelname}}','{{level2name}}')">{{status_1}}</td>
                                    {{/check_zero}}
                                    {{#check_zero}}
                                    <td style='color:royalblue; cursor:pointer;' class="center" onclick="reports03($day_reports,null,'{{level2id}}','3','','{{levelname}}','{{level2name}}')">{{status_2}}</td>
                                    {{/check_zero}}
                                    {{#check_zero}}
                                    <td style='color:royalblue; cursor:pointer;' class="center" onclick="reports03($day_reports,null,'{{level2id}}','4','','{{levelname}}','{{level2name}}')">{{status_3}}</td>
                                    {{/check_zero}}
                                    {{#check_zero}}
                                    <td style='color:royalblue; cursor:pointer;' class="center" onclick="reports03($day_reports,null,'{{level2id}}','5','','{{levelname}}','{{level2name}}')">{{status_4}}</td>
                                    {{/check_zero}}
                                    {{#check_zero}}
                                    <td style='color:royalblue; cursor:pointer;' class="center" onclick="reports03($day_reports,null,'{{level2id}}','10','','{{levelname}}','{{level2name}}')">{{status_5}}</td>
                                    {{/check_zero}}
                                </tr>
                                {{/level2}}
                                 <tr>
                                    <td class="right"></td>
                                    <td class="right"></td>
                                    <td class="right" style='color:royalblue;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></td>
                                    <td class="center" >{{studentnember}}</td>
                                    <td class="center">{{footer_status_0}}</td>
                                    <td class="center">{{footer_status_1}}</td>
                                    <td class="center">{{footer_status_2}}</td>
                                    <td class="center">{{footer_status_3}}</td>
                                    <td class="center">{{footer_status_4}}</td>
                                    <td class="center">{{footer_status_5}}</td>
                                </tr>
                            {{/data}}
                            {{#footer}}
                                 <tr id='total' style='font-weight: bold;' rowspan=4>
                                    <td class='right' colspan=2 style='border:0px;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206458") %></td>
                                    <td colspan=8 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303005") %> {{status_0}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_0}} %
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_0}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_0}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_0}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_0}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=8 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %> {{status_1}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_1}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_1}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_1}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_1}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_1}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=8 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206204") %> {{status_2}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_2}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_2}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_2}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_2}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_2}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=8 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %> {{status_3}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_3}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_3}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_3}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_3}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_3}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=8 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %> {{status_4}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_4}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_4}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_4}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_4}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_4}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=8 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %> {{status_5}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_5}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_5}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_5}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_5}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_5}} %
                                    </td>
                                </tr>
                            {{/footer}}
                        </tbody>
                        {{/tbody}}
                    </table>
                    </script>
                    <script id="template_reports_02" type="x-tmpl-mustache">
                    <table class="table table-condensed table-bordered table-show-result" id="example" cellspacing="0" width="100%">
                         {{#thead}}
                         <thead id="myHeader" class="hidden">
                            <tr>
                                <th style="text-align: center; font-size: 26px; border-width: 0px 1px;" id="school" colspan="10">{{schoolname}}
                                </th>
                            </tr>
                            <tr>
                                <th colspan="10" style="text-align: center; font-size: 24px; border-width: 0px 1px;" id="headdatail"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133189") %>
                                </th>
                            </tr>
                            <tr>
                                <th colspan="10" style="text-align: center; font-size: 24px; border-width: 0px 1px;" id="dayfall"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133167") %> {{day}}
                                </th>
                            </tr>
                            <tr>
                                <th colspan="10" style="text-align: right; font-size: 20px; border-width: 0px 1px;" id="dayshort"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601088") %>&nbsp; {{dayprint}}
                                </th>
                            </tr>
                            <tr>
                                <th colspan="10" style="text-align: right; font-size: 20px; border-width: 0px 1px;" id="timetoday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> :&nbsp; {{timeprint}}
                                </th>
                            </tr>
                            <tr style="font-size: 20px;">
                                <th colspan="2" style="text-align: right; width: 20%; border-width: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> : 
                                </th>
                                <th style="width: 30%; border-width: 0px;">&nbsp; {{year}}
                                </th>
                                <th style="text-align: right; width: 20%; border-width: 0px;" colspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> : 
                                </th>
                                <th colspan="5" style="width: 30%; border-width: 0px;">&nbsp; {{term}}
                                </th>
                            </tr>
                            <tr style="font-size: 20px;">
                                <th colspan="2" style="text-align: right; width: 20%; border-width: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> : 
                                </th>
                                <th style="width: 30%; border-width: 0px;">&nbsp; {{header_level}}
                                </th>
                                <th style="text-align: right; width: 20%; border-width: 0px;" colspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> : 
                                </th>
                                <th colspan="5" style="width: 30%; border-width: 0px;">&nbsp; {{header_level2}}
                                </th>
                            </tr>
                            {{#student_id}}
                            <tr style="font-size: 20px;">
                                <th colspan="2" style="text-align: right; width: 20%; border-width: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> : 
                                </th>
                                <th style="width: 30%; border-width: 0px;" colspan="3">&nbsp; {{student_id}}
                                </th>
                                <th style="text-align: right; width: 20%; border-width: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %> : 
                                </th>
                                <th colspan="9" style="width: 30%; border-width: 0px;">&nbsp; {{student_name}}
                                </th>
                            </tr>
                            {{/student_id}}
                            <tr>
                                <th colspan="10" style="border-width: 0px;">
                                    <br />
                                </th>
                            </tr>
                        </thead>
                        {{/thead}}
                        {{#tbody}}
                        <tbody>
                            <tr id='headder' style='font-weight: bold; font-size:20px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>
                                <td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td>
                                <td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></td>
                                <td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></td>
                                <td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %></td>
                                <td id='headder'colspan='6'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td>
                            </tr>
                            <tr id='headder' style='font-weight: bold; font-size:18px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></td>
                                <td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %></td>
                            </tr>
                            {{#data}}
                                {{#student_data}}
                                <tr>
                                    <td class="center">{{index}}</td>
                                    <td class="center">{{studentname}}</td>
                                    <td class="center">{{studentlastname}}</td>
                                    <td class="center">{{timein}}</td>
                                    <td class="center">{{#statusin_0}}<span style="color:red;">X</span>{{/statusin_0}}{{^statusin_0}}{{/statusin_0}}</td>
                                    <td class="center">{{#statusin_1}}<span style="color:red;">X</span>{{/statusin_1}}{{^statusin_1}}{{/statusin_1}}</td>
                                    <td class="center">{{#statusin_2}}<span style="color:red;">X</span>{{/statusin_2}}{{^statusin_2}}{{/statusin_2}}</td>
                                    <td class="center">{{#statusin_3}}<span style="color:red;">X</span>{{/statusin_3}}{{^statusin_3}}{{/statusin_3}}</td>
                                    <td class="center">{{#statusin_4}}<span style="color:red;">X</span>{{/statusin_4}}{{^statusin_4}}{{/statusin_4}}</td>
                                    <td class="center">{{#statusin_5}}<span style="color:red;">X</span>{{/statusin_5}}{{^statusin_5}}{{/statusin_5}}</td>
                                </tr>
                                {{/student_data}}
                            {{/data}}
                            {{#footer}}
                                <tr>
                                    <td class="right"></td>
                                    <td class="right"></td>
                                    <td class="right" style='color:royalblue;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></td>
                                    <td class="center" >{{studentnember}}</td>
                                    <td class="center">{{sum_status_0}}</td>
                                    <td class="center">{{sum_status_1}}</td>
                                    <td class="center">{{sum_status_2}}</td>
                                    <td class="center">{{sum_status_3}}</td>
                                    <td class="center">{{sum_status_4}}</td>
                                    <td class="center">{{sum_status_5}}</td>
                                </tr>
                                 <tr id='total' style='font-weight: bold;' rowspan=4>
                                    <td class='right' colspan=2 style='border:0px;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206458") %></td>
                                    <td colspan=8 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303005") %> {{status_0}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_0}} %
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_0}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_0}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_0}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_0}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=8 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %> {{status_1}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_1}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_1}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_1}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_1}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_1}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=8 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206204") %> {{status_2}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_2}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_2}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_2}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_2}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_2}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=8 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %> {{status_3}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_3}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_3}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_3}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_3}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_3}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=8 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %> {{status_4}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_4}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_4}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_4}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_4}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_4}} %
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td colspan=2 style='border:0px;'></td>
                                    <td colspan=8 style='border:0px;'>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %> {{status_5}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_status_5}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %> {{male_status_5}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_male_status_5}} % 
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %> {{female_status_5}} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> {{percent_female_status_5}} %
                                    </td>
                                </tr>
                            {{/footer}}
                        </tbody>
                        {{/tbody}}
                    </table>
                    </script>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>
