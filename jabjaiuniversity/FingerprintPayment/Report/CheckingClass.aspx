<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="CheckingClass.aspx.cs" Inherits="FingerprintPayment.Report.CheckingClass" %>

<%@ Register Src="~/UserControls/YTFilter.ascx" TagPrefix="uc1" TagName="YTFilter" %>
<%@ Register Src="~/UserControls/LCFilter.ascx" TagPrefix="uc1" TagName="LCFilter" %>
<%@ Register Src="~/UserControls/YTLCFilter.ascx" TagPrefix="uc1" TagName="YTLCFilter" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <style>
        label.error {
            color: red;
        }

        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }

        /* .dataTables_wrapper .btn-group {
            display: none;
        }*/

        .dataTables_length {
            display: block !important;
            float: left;
        }

        .ui-timepicker-standard {
            z-index: 99 !important;
        }

        .dataTables_length .custom-select {
            appearance: auto !important;
            text-align: center !important;
            width: 80px !important;
        }

        .custom-border-header {
            border-left: 0.5px solid #000;
            border-right: .5px solid #000;
        }

        #template1 {
            border-collapse: collapse;
        }

            #template1 tr {
                border-bottom: 1px solid #000;
            }

        .custom-border-td {
            border-left: 1px solid #000;
            border-right: 1px solid #000;
            /*   border-top:0.5px solid #000;
            border-bottom:0.5px solid #000;*/
        }

        #template1 tr td:last-child {
            border-right: 1px solid #000;
        }
    </style>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="//cdn.jsdelivr.net/npm/lodash@4.17.21/lodash.min.js"></script>
    <script>
        var $table;
        var _data;
        //function LoadTeacherSubject() {
        //    var term = YTF.GetTermID();
        //    PageMethods.LoadTeacherSubject(term,
        //        function (response) {
        //            DATA = response;
        //        },
        //        function (response) {

        //        }
        //    );
        //}

        function SearchData(t) {
            if (!$("#aspnetForm").valid()) {
                return;
            }


            var classId = $('#sltClass').val();

            if (!classId) {
                classId = $("#sltClass option:gt(1)").map(function () {
                    return $(this).val();
                })
                    .get()
                    .join(',');
            }

            var subjectId = $('#subject').val();

            if (!subjectId) {
                subjectId = $("#subject option:gt(1)").map(function () {
                    return $(this).val();
                })
                    .get()
                    .join(',');
            }
            var rt = $('#reportType').val();
            var date1, date2;
            switch (rt) {
                case "1":
                    date1 = moment($("#date1").val(), 'DD/MM/YYYY').add(-543, 'years');
                    date2 = moment($("#date1").val(), 'DD/MM/YYYY').add(-543, 'years');
                    break;
                case "2":
                    date1 = moment($("#date21").val(), 'DD/MM/YYYY').add(-543, 'years');
                    date2 = moment($("#date22").val(), 'DD/MM/YYYY').add(-543, 'years');
                    break;
                case "3":
                    var m = $('#month').val();
                    var y = $('#year').val();
                    date1 = moment("01/" + m + "/" + y, 'DD/MM/YYYY');
                    date2 = moment("01/" + m + "/" + y, 'DD/MM/YYYY').endOf('month');
                    break;
                default:
                    break;
            }

            var search = {
                'term': YTLCF.GetTermID(),
                'yearNo': YTLCF.GetYearNo(),
                'termNo': YTLCF.GetTermNo(),
                'subjectId': subjectId,
                'levelId': $('#sltLevel').val(),
                'levelNo': $('#sltLevel :selected').text(),
                'classId': classId,
                'reportType': rt,
                'date1': date1.format('MM/DD/YYYY'),
                'date2': date2.format('MM/DD/YYYY'),
            };

            if (t == 'data') {

                $("body").mLoading('show');
                $.ajax({
                    async: true,
                    type: "POST",
                    url: "<%=Page.ResolveUrl("~/Report/CheckingClass.aspx/LoadData")%>",
                    data: JSON.stringify({ 'search': search }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                 
                        var $table = $('#template1');
                        var $thead = $table.find('thead');
                        var $tbody = $table.find('tbody');

                        $thead.empty();
                        $tbody.empty();

                        if (response.d.data.length > 0) {
                            $("#exportDD").show();

                            var times = response.d.times;

                            var _thead = `<tr >
                                        <th width=8%></th><th width=6%></th>`;
                            for (var i = 0; i < times.length - 1; i++) {
                                _thead += `<th width=${(85 / times.length)}%></th>`;
                            }
                            _thead += `</tr>`;

                            _thead += `<tr>
                                        <th class="text-center custom-border-header" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %><br/>Date</th>
                                        <th class="text-center custom-border-header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %><br/>Class Level</th>`;
                            var _c = 1;
                            for (var i = 0; i < times.length - 12; i = i + 12) {
                                _thead += `<th colspan=12 class="text-center custom-border-header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206444") %> ${(_c++)}<br/>${times[i].Time} - ${times[i + 12].Time}</th>`;
                            }
                            _thead += "</tr>";
                            $thead.append(_thead);

                            $.each(response.d.data, function (i, v) {
                                var _tr = "<tr class='custom-border-tr'>";

                                _tr += "<td class='text-center custom-border-td' >" + v.Date + "</td>";
                                _tr += "<td class='text-center custom-border-td'>" + v.Class + "</td>";

                                if (v.IsHoliday == true) {
                                    _tr += "<td  class='text-center' colspan=" + times.length + "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %></td>";
                                }
                                else {

                                    var _counter = 0;
                                    $.each(v.Schdule, function (j, s) {

                                        while (_counter < times.length - 1) {
                                            if (_counter == s.Col) {
                                                _tr += `<td  colspan=${s.ColSpan} class="text-center custom-border-td">
                                                    ${s.Code}<br/>
                                                    ${s.Teacher}<br/>
                                                    ${(s.IsChecked ? "<span style=color:green><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206341") %></span>" : '<span style=color:red><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206340") %></span>')}
                                                </td>`;
                                                _counter = (_counter + s.ColSpan);

                                                //_counter++;
                                                break;
                                            } 
                                            else {
                                                _tr += `<td class=""></td>`;
                                                _counter++;
                                            }
                                        }
                                    });

                                    while (_counter < times.length - 1) {
                                        _tr += `<td class=""></td>`;
                                        _counter++;
                                    }

                                    //for (var j = 0; j < times.length; j++) {
                                    //    var s = v.Schdule[j];

                                    //    if (s.Col == j) {
                                    //        _tr += `<td  colspan=${s.ColSpan} class="text-center border">${s.Code}<br/>${s.Teacher}<br/>${(s.IsChecked ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206341") %>" : '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206340") %>')}</td>`
                                    //    }
                                    //    else {
                                    //        _tr += `<td class=""></td>`;
                                    //    }

                                    //}                              
                                }
                                _tr += "</tr>";

                                $tbody.append(_tr);
                            });
                        }
                        else {                           
                            $("#exportDD").hide();
                            var tr = "<tr><th class='text-center'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102035") %></th></tr>";
                            $thead.append(tr);
                        }
                        $("body").mLoading('hide');
                    },
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });

            }
            else if (t == 'excel' || t == 'pdf') {

                //$("body").mLoading('show');
                if (t == 'excel') {
                    xhr = new XMLHttpRequest();
                    xhr.open("POST", "/Report/CheckingClass.aspx/ExportExcel", true);
                    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                    xhr.responseType = 'blob';
                    xhr.onload = function () {
                        var dt = new Date();
                        var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206329") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';
                        saveAs(xhr.response, file_name);
                        $("body").mLoading('hide');
                    };
                    //xhr.send(search);
                    xhr.send(JSON.stringify({ 'search': search }));
                }
                else if (t == 'pdf') {
                    xhr = new XMLHttpRequest();
                    xhr.open("POST", "/Report/CheckingClass.aspx/ExportPdf", true);
                    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                    xhr.responseType = 'blob';
                    xhr.onload = function () {
                        var dt = new Date();
                        var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206329") %>_' + dt.format("ddMMyyyyHHmmssss") + '.pdf';
                        saveAs(xhr.response, file_name);
                        $("body").mLoading('hide');
                    };
                    //xhr.send(search);
                    xhr.send(JSON.stringify({ 'search': search }));
                }


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

            $('#reportType').on('change', function () {
                $('.reptype').hide();
                $('.reptype.-t' + $(this).val()).show();
            });

            //$('#date2').on('dp.change', function (e) {
            //    var d1 = moment($("#date1").val(), 'DD/MM/YYYY');
            //    var d2 = moment($("#date2").val(), 'DD/MM/YYYY');

            //    if (d2 < d1) {
            //        //alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131144") %>');
            //        Swal.fire({
            //            type: 'error',
            //            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131144") %>',
            //            //text: 'Something went wrong!',                      
            //        })
            //        $("#date2").val('');
            //    }
            //})



            //$("#sltTerm").on('change', function () {

            //});

            //$("#subject").on('change', function () {

            //    //$("#sltLevel").empty();

            //    //if (_data.length > 0) {

            //    //    var arr = [];
            //    //    $(_.filter(_data, { 'SubjectID': +$(this).val() })).each(function () {
            //    //        arr.push({ 'id': this.LevelID, 'name': this.LevelName });
            //    //    });
            //    //    arr = _.uniqBy(arr, 'id');
            //    //    var options = '';
            //    //    $(arr).each(function () {
            //    //        options += '<option value="' + this.id + '">' + this.name + '</option>';
            //    //    });
            //    //    $("#sltLevel").html(options);
            //    //    $("#sltLevel").selectpicker('refresh');
            //    //}
            //});

            $("#sltLevel").on('change', function () {
                $("body").mLoading('show');
                PageMethods.LoadSubject($('#sltTerm').val(), $(this).val(),
                    function (response) {
                        $("body").mLoading('hide');

                        _data = response;

                        $("#subject").empty(); $("#subject").selectpicker('refresh');
                        //$("#sltLevel").empty(); $("#sltLevel").selectpicker('refresh');
                        //$("#sltClass").empty(); $("#sltClass").selectpicker('refresh');

                        if (_data.length > 0) {
                            var arr = [];
                            $(_data).each(function () {
                                arr.push({ 'id': this.SubjectID, 'name': this.Subject });
                            });
                            arr = _.uniqBy(arr, 'id');
                            arr = _.orderBy(arr, ['name'], ['asc']);
                            var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00803") %></option>';
                            $(arr).each(function () {
                                options += '<option value="' + this.id + '">' + this.name + '</option>';
                            });
                            $("#subject").html(options);
                            $("#subject").selectpicker('refresh');
                        }
                    },
                    function (response) {
                        $("body").mLoading('hide');
                    }
                );


            });

            $('#searchForm').on('change', '#sltClass', function () {

                if ($(this).val()) {
                    $("#subject").empty(); $("#subject").selectpicker('refresh');
                    if (_data.length > 0) {

                        var arr = [];
                        $(_.filter(_data, { 'ClassID': +$(this).val() })).each(function () {
                            arr.push({ 'id': this.SubjectID, 'name': this.Subject });
                        });
                        arr = _.uniqBy(arr, 'id');
                        arr = _.orderBy(arr, ['name'], ['asc']);
                        var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00803") %></option>';
                        $(arr).each(function () {
                            options += '<option value="' + this.id + '">' + this.name + '</option>';
                        });
                        $("#subject").html(options);
                        $("#subject").selectpicker('refresh');
                    }
                }
            });

            //$("#sltTerm").trigger('change');
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206328") %>
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
                    <div class="card-body " id="searchForm">

                        <%--<uc1:YTFilter runat="server" ID="YTFilter" />--%>
                        <%--  <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %>/<br />
                                Level Class</label>
                            <div class="col-sm-3">
                                <select id="sltLevel" name="sltLevel" class="selectpicker  --req-append-last" data-size="7" data-width="100%" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %>" required>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>/<br />
                                Class</label>
                            <div class="col-sm-3">
                                <select id="sltClass" name="sltClass" class="selectpicker  --req-append-last" data-size="7" data-width="100%" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104003") %>">
                                </select>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>--%>

                        <uc1:YTLCFilter runat="server" ID="YTLCFilter" IsLevelRequired="true" />

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %>/<br />
                                Report Type
                            </label>
                            <div class="col-sm-3">
                                <select id="reportType" name="reportType" class="selectpicker --req-append-last" data-live-search="true" data-style="select-with-transition" data-width="100%">
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105010") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206330") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105011") %></option>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206196") %><br />
                                Subject Name</label>
                            <div class="col-sm-3">
                                <select id="subject" name="subject" class="selectpicker --req-append-last" data-style="select-with-transition" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206201") %>" data-live-search="true">
                                </select>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>


                        <div class="row reptype -t1">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104069") %><br />
                                Date
                            </label>
                            <div class="col-sm-3">
                                <div class="form-group ">
                                    <input type="text" id="date1" name="date1" class="form-control datepicker" value="" required>
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row reptype -t2" style="display: none">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104069") %><br />
                                Date
                            </label>
                            <div class="col-sm-3">
                                <div class="form-group ">
                                    <input type="text" id="date21" name="date21" class="form-control datepicker" value="" required>
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104070") %><br />
                                To Date
                            </label>
                            <div class="col-sm-3">
                                <div class="form-group ">
                                    <input type="text" id="date22" name="date22" class="form-control datepicker" value="" required>
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row reptype -t3" style="display: none">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>/<br />
                                Month
                            </label>
                            <div class="col-sm-3">
                                <select id="month" name="month" class="selectpicker --req-append-last" data-live-search="true" data-style="select-with-transition" data-width="100%" required>
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
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %></label>
                            <div class="col-sm-3">
                                <select id="year" name="year" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <% for (int i = DateTime.Today.Year; i >= 2015; i--)
                                        {%>
                                    <option value="<%= i %>"><%= i + 543 %></option>
                                    <%}%>
                                </select>
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

                            </div>
                        </div>
                    </div>
                </div>


                <div class="card ">
                    <div class="card-header card-header-primary card-header-icon">
                        <div class="card-icon">
                            <span class="material-icons">list_alt</span>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206329") %></h4>

                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <%-- <div class="col-md-6 text-left" style="">
                                <strong style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206199") %>
                                </strong>
                            </div>--%>
                            <div class="col-md-12 " style="">
                                <%-- <div class="btn btn-success pull-right" id="exportfile" onclick="SearchData('report')">
                                    <span class="btn-label">
                                        <span class="material-icons">download
                                        </span>
                                    </span>
                                    Export
                                </div>--%>

                                <div class="dropdown" id="exportDD">
                                    <button class="btn btn-success dropdown-toggle success pull-right" type="button" data-toggle="dropdown">
                                        Export <span class="caret" />
                                    </button>
                                    <ul class="dropdown-menu" style="font-size: 24px">
                                        <li>
                                            <a href="#" onclick="SearchData('excel')">Excel</a>
                                        </li>
                                        <li>
                                            <a href="#" onclick="SearchData('pdf')">PDF</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="">
                                <table id="template1" class="table-hover dataTableX" borderx="1" width="100%" style="margin: 0 5px;">
                                    <thead></thead>
                                    <tbody></tbody>
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
                        <%--  <div class="row">
                            <div class="col-md-12 text-center" id="note1" style="display: none">
                                <strong style="color: red"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133153") %>
                                </strong>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" id="result-wrapper">
                                <strong style="float: right;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701074") %>
                                </strong>
                            </div>
                        </div>--%>
                    </div>
                </div>

            </div>
        </div>
    </form>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
