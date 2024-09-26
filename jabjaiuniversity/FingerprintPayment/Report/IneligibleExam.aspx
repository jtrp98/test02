<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="IneligibleExam.aspx.cs" Inherits="FingerprintPayment.Report.IneligibleExam" %>

<%@ Register Src="~/UserControls/YTFilter.ascx" TagPrefix="uc1" TagName="YTFilter" %>
<%@ Register Src="~/UserControls/LCFilter.ascx" TagPrefix="uc1" TagName="LCFilter" %>



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

            if (t == 'data') {
                var date1 = $("#datestart1").val();
                $table = $('#template1').DataTable({
                    dom: '<"top">rt<"bottom"lp><"clear">',
                    "processing": true,
                    "destroy": true,
                    "info": false,
                    paging: true,
                    "pageLength": 20,
                    "lengthChange": true,
                    lengthMenu: [
                        [20, 50, 100],
                        [20, 50, 100],
                    ],
                    "language": {
                        "lengthMenu": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %> : _MENU_<br/>/Showing"
                    },
                    searching: false,
                    ajax: {
                        url: "/Report/IneligibleExam.aspx/LoadData",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        "dataSrc": function (r) {
                            return r.d.data;
                        },
                        'data': function (d) {

                            var classId = $('#sltClass').val();

                            if (!classId) {
                                classId = $("#sltClass option:gt(1)").map(function () {
                                    return $(this).val();
                                })
                                    .get()
                                    .join(',');
                            }

                            d.search = {
                                'term': YTF.GetTermID(),
                                'teacherId': $('#teacherName').val(),
                                'subjectId': $('#subject').val(),
                                'levelId': $('#sltLevel').val(),
                                'classId': classId,
                                'status': $('#status').val(),
                            };

                            return JSON.stringify(d);
                        },
                    },

                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><br/>Order', data: 'Index', "class": "text-center", "width": "5%" },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104063") %><br/>ID number', data: 'Code', "class": "text-center", "width": "10%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %><br/>Name", data: 'FullName', "class": "text-center", "width": "15%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %><br/>Class", data: 'Class', "class": "text-center", "width": "12%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202029") %><br/>Class time", data: 'ClassTime', "width": "8%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303005") %><br/>Come study", data: 'Come', "width": "8%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206204") %><br/>Absent", data: 'Absent', "width": "8%", "class": "text-center" },

                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206205") %><br/>Percent", data: 'Percent', "width": "8%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br/>Status", data: 'Status', "width": "10%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201048") %><br/>Teacher Name", data: 'Teacher', "width": "10%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br/>Note", data: 'Note', "width": "10%", "class": "text-center" },
                        //{
                        //    "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201048") %><br/>Teacher Name", data: 'Note', "width": "10%", "class": "text-center",
                        //    "mRender": function (data, type, row) {
                        //        return CalcScore(row.ScoreAll);
                        //    }
                        //},
                    ],
                    "order": [[0, 'asc']],
                    "fnInitComplete": function (oSettings, json) {
                        //if ($('#methodName').val() == 'CheckBalance') {
                        //    $("#note1").show();
                        //}
                        //else {
                        //    $("#note1").hide();
                        //}
                    }
                });

            }
            else if (t == 'excel' || t == 'pdf') {
                var classId = $('#sltClass').val();

                if (!classId) {
                    classId = $("#sltClass option:gt(1)").map(function () {
                        return $(this).val();
                    })
                        .get()
                        .join(',');
                }

                var search = {
                    'term': YTF.GetTermID(),
                    'yearNo': YTF.GetYearNo(),
                    'termNo': YTF.GetTermText(),
                    'teacherId': $('#teacherName').val(),
                    'teacher': $('#teacherName :selected').text(),
                    'subjectId': $('#subject').val(),
                    'subject': $('#subject :selected').text(),
                    'levelId': $('#sltLevel').val(),
                    'levelNo': $('#sltLevel :selected').text(),
                    'classId': classId,
                    'status': $('#status').val(),
                };
                $("body").mLoading('show');
                if (t == 'excel') {
                    xhr = new XMLHttpRequest();
                    xhr.open("POST", "/Report/IneligibleExam.aspx/ExportExcel", true);
                    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                    xhr.responseType = 'blob';
                    xhr.onload = function () {
                        var dt = new Date();
                        var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206193") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';
                        saveAs(xhr.response, file_name);
                        $("body").mLoading('hide');
                    };
                    //xhr.send(search);
                    xhr.send(JSON.stringify({ 'search': search }));
                }
                else if (t == 'pdf') {
                    xhr = new XMLHttpRequest();
                    xhr.open("POST", "/Report/IneligibleExam.aspx/ExportPdf", true);
                    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                    xhr.responseType = 'blob';
                    xhr.onload = function () {
                        var dt = new Date();
                        var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206193") %>_' + dt.format("ddMMyyyyHHmmssss") + '.pdf';
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

            $("#sltTerm").on('change', function () {
                $("body").mLoading('show');
                PageMethods.LoadTeacherSubject($(this).val(),
                    function (response) {
                        $("body").mLoading('hide');

                        _data = response;

                        $("#teacherName").empty(); $("#teacherName").selectpicker('refresh');
                        $("#subject").empty(); $("#subject").selectpicker('refresh');
                        $("#sltLevel").empty(); $("#sltLevel").selectpicker('refresh');
                        $("#sltClass").empty(); $("#sltClass").selectpicker('refresh');

                        if (_data.length > 0) {
                            var arr = [];
                            $(_data).each(function () {
                                arr.push({ 'id': this.TeacherID, 'name': this.Teacher });
                            });
                            arr = _.uniqBy(arr, 'id');
                            arr = _.orderBy(arr, ['name'], ['asc']);
                            var options = '';
                            $(arr).each(function () {
                                options += '<option value="' + this.id + '">' + this.name + '</option>';
                            });
                            $("#teacherName").html(options);
                            $("#teacherName").selectpicker('refresh');
                        }
                    },
                    function (response) {
                        $("body").mLoading('hide');
                    }
                );
            });

            $("#teacherName").on('change', function () {

                $("#subject").empty(); $("#subject").selectpicker('refresh');

                if (_data.length > 0) {

                    var arr = [];
                    $(_.filter(_data, { 'TeacherID': +$(this).val() })).each(function () {
                        arr.push({ 'id': this.SubjectID, 'name': this.Subject });
                    });
                    arr = _.uniqBy(arr, 'id');
                    arr = _.orderBy(arr, ['name'], ['asc']);
                    var options = '';
                    $(arr).each(function () {
                        options += '<option value="' + this.id + '">' + this.name + '</option>';
                    });
                    $("#subject").html(options);
                    $("#subject").selectpicker('refresh');
                }

            });

            $("#subject").on('change', function () {

                $("#sltLevel").empty(); $("#sltLevel").selectpicker('refresh');

                if (_data.length > 0) {

                    var arr = [];
                    $(_.filter(_data, { 'SubjectID': +$(this).val(), 'TeacherID': +$('#teacherName').val() })).each(function () {
                        arr.push({ 'id': this.LevelID, 'name': this.LevelName });
                    });
                    arr = _.uniqBy(arr, 'id');
                    //arr = _.orderBy(arr, ['name'], ['asc']);
                    var options = '';
                    $(arr).each(function () {
                        options += '<option value="' + this.id + '">' + this.name + '</option>';
                    });
                    $("#sltLevel").html(options);
                    $("#sltLevel").selectpicker('refresh');
                }
            });

            $("#sltLevel").on('change', function () {

                $("#sltClass").empty(); $("#sltClass").selectpicker('refresh');

                if (_data.length > 0) {

                    var arr = [];
                    $(_.filter(_data, { 'LevelID': +$(this).val(), 'SubjectID': +$("#subject").val(), 'TeacherID': +$('#teacherName').val() })).each(function () {
                        arr.push({ 'id': this.ClassID, 'name': this.ClassRoom });
                    });
                    arr = _.uniqBy(arr, 'id');
                    //arr = _.orderBy(arr, ['name'], ['asc']);
                    var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104003") %></option>';
                    $(arr).each(function () {
                        options += '<option value="' + this.id + '">' + this.name + '</option>';
                    });
                    $("#sltClass").html(options);
                    $("#sltClass").selectpicker('refresh');
                }
            });

            $("#sltTerm").trigger('change');
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01705") %> > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206193") %>
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

                        <%-- <uc1:YTLCFilter runat="server" ID="YTLCFilter" IsLevelRequired="true" />--%>
                        <uc1:YTFilter runat="server" ID="YTFilter" IsRequired="true" />

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206195") %><br />
                                Teacher Name
                            </label>
                            <div class="col-sm-3">
                                <select id="teacherName" name="teacherName" class="selectpicker --req-append-last" data-live-search="true" data-style="select-with-transition" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206200") %>" required>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206196") %><br />
                                Subject Name</label>
                            <div class="col-sm-3">
                                <select id="subject" name="subject" class="selectpicker --req-append-last" data-live-search="true" data-style="select-with-transition" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206201") %>" required>
                                </select>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row">
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
                        </div>

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206197") %><br />
                                Status
                            </label>
                            <div class="col-sm-3">
                                <select id="status" class="selectpicker " data-style="select-with-transition" data-width="100%">
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206202") %></option>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3">
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
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206193") %></h4>

                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-6 text-left" style="">
                                <strong style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206199") %>
                                </strong>
                            </div>
                            <div class="col-md-6 " style="">
                                <%-- <div class="btn btn-success pull-right" id="exportfile" onclick="SearchData('report')">
                                    <span class="btn-label">
                                        <span class="material-icons">download
                                        </span>
                                    </span>
                                    Export
                                </div>--%>

                                <div class="dropdown">
                                    <button class="btn btn-success dropdown-toggle success pull-right" type="button" data-toggle="dropdown">
                                        Export <span class="caret" />
                                    </button>
                                    <ul class="dropdown-menu" style="font-size: 24px">
                                        <li>
                                            <a href="#"  onclick="SearchData('excel')">Excel</a>
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
