<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="Report1.aspx.cs" Inherits="FingerprintPayment.Card.PermissionCard.Report1" %>

<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link rel="stylesheet" href="//use.fontawesome.com/releases/v5.15.3/css/all.css" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/simplelightbox/2.14.1/simple-lightbox.min.css" integrity="sha512-bhYgu+LnK6ZXsq18JjhqqzZjnw4GzgCe6s34JBw09tQZ9oFC5tdSlLJW/7ZCf9EXuepe6tnQW57X8z3DKnsAGg==" crossorigin="anonymous" referrerpolicy="no-referrer" />
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
         .gallery-item {
            width: 70px;
        }
    </style>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/simplelightbox/2.14.1/simple-lightbox.jquery.js" integrity="sha512-rAjNTLUtuyD/v5DzqmT8t2b8KUjK5b+OFxL3D7vtZjHam/u8BETTKaU2fWCBUuTttxfufYYSPsKv+JTJpBwdgA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script>

        function LoadLevel2(subLevelID, objResult) {
            if (subLevelID) {

                $("body").mLoading();
                $.ajax({
                    async: true,
                    type: "POST",
                    url: "<%=Page.ResolveUrl("~/StudentInfo/StudentList.aspx/LoadTermSubLevel2")%>",
                    data: '{subLevelID: ' + subLevelID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var subLevel2 = response.d;
                        $(objResult).empty();
                        if (subLevel2.length > 0) {
                            var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %></option>';
                            $(subLevel2).each(function () {
                                options += '<option value="' + this.id + '">' + this.name + '</option>';
                            });

                            $(objResult).html(options);
                            $(objResult).selectpicker('refresh');
                        }
                        $("body").mLoading("hide");
                    },
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }

        function LoadTerm(yearID, objResult) {
            if (yearID) {
                $("body").mLoading();
                $.ajax({
                    async: true,
                    type: "POST",
                    url: "<%=Page.ResolveUrl("~/StudentInfo/StudentList.aspx/LoadTerm")%>",
                    data: '{yearID: ' + yearID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var yearData = response.d;

                        $(objResult).empty();

                        if (yearData.length > 0) {

                            var options = '';
                            $(yearData).each(function () {

                                options += '<option value="' + this.id + '">' + this.name + '</option>';

                            });

                            $(objResult).html(options);
                            $(objResult).selectpicker('refresh');
                        }
                        $("body").mLoading("hide");
                    },
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }

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
                    scrollX: true,
                    "pageLength": 20,
                    searching: false,
                    "lengthChange": false,
                    ajax: {
                        url: "/Card/PermissionCard/Report1.aspx/LoadData",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        "dataSrc": function (r) {
                            return r.d.data;
                        },
                        'data': function (d) {
                            var date = '', month = '', term = '', level1 = '', level2 = '';
                            var type1 = $('#ReportType1').val();
                            var type2 = $('#ReportType2').val();

                            switch (type1) {
                                case '1':

                                    break;

                                case '2':
                                    level1 = $('#selectlevel').val();
                                    level2 = $('#selectclass').val();
                                    break;

                                default:
                            }

                            switch (type2) {
                                case '1':
                                    date = $("#date1").data("DateTimePicker").date().format("MM/DD/YYYY");
                                    //moment($('#date1').val(), 'DD/MM/YYYY').add(-543, 'years').format('MM/DD/YYYY');
                                    break;

                                case '2':
                                    month = moment(`01/${$('#month2').val()}/${$('#year2 :selected').text()}`, 'DD/MM/YYYY')
                                        .add(-543, 'years').format('MM/DD/YYYY');
                                    break;
                                case '3':
                                    term = $('#term3').val();
                                    break;
                                default:
                            }


                            d.search = {
                                'type1': type1,
                                'type2': type2,
                                'date': date,
                                'month': month,
                                'term': term,
                                'level1': level1,
                                'level2': level2,
                                'name': SAC.GetStudentName(),
                            };

                            return JSON.stringify(d);
                        },
                    },


                    "columns": [
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><br/>No.', data: 'Index', "class": "text-center", "width": "5%" },
                        { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %><br/>Number', data: 'Number', "class": "text-center", "width": "5%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %><br/>Student ID", data: 'Code', "class": "text-center", "width": "10%" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %><br/>Full Name", data: 'FullName', "width": "12%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107053") %><br/>Transaction date", data: 'Created', "width": "12%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107054") %><br/>License no.", data: 'RefNo', "width": "12%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107039") %><br/>List group", data: 'GroupName', "width": "12%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %><br/>Details", data: 'Cause', "width": "14%", "class": "text-center" },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107056") %><br/>Request date", data: 'StartDate', "width": "12%", "class": "text-center" },
                        {
                            "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107057") %><br/>Attach Image", data: 'Index', "class": "text-center  ", "width": "12%", "mRender": function (data, type, row) {
                                var img = "";
                                if (row.Files) {
                                    row.Files.forEach(function (x) {
                                        img += `<a title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107057") %> ${row.Code}/${row.FullName}"  class="gallery-link" href="${x}"><img class="gallery-item" src="${x}" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107057") %> ${row.Code}/${row.FullName}"></a>`;
                                    });
                                    return `<div id="gallery${row.sID}" class="gallery">${img}</div>`;
                                }
                                return "-"
                            }
                        },
                        { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107058") %><br/>Prepare by", data: 'Employee', "width": "12%", "class": "text-center" },
                        {
                            "title": "", data: 'Index', "width": "10%", "class": "text-center",
                            "mRender": function (data, type, row) {                       
                                return `<a target='_blank' href='Preview.aspx?sID=${row.sID}&term=${row.Term}&pID=${row.PID}' class='btn btn-warning btn-sm' ><span class='material-icons'>search</span></a>`;

                            }
                        },

                    ],
                    "drawCallback": function (settings) {
                        //alert('DataTables has redrawn the table');
                        $('.gallery .gallery-link').simpleLightbox();

                    },
                    "fnInitComplete": function (settings) {
                        //alert('DataTables has redrawn the table');
                    }
                    //"order": [[0, 'asc']]
                });

            }
            else if (t == 'report') {
                var date = '', month = '', term = '', level1 = '', level2 = '';
                var type1 = $('#ReportType1').val();
                var type2 = $('#ReportType2').val();

                switch (type1) {
                    case '1':

                        break;

                    case '2':
                        level1 = $('#selectlevel').val();
                        level2 = $('#selectclass').val();
                        break;

                    default:
                }

                switch (type2) {
                    case '1':
                        date = moment($('#date1').val(), 'DD/MM/YYYY').add(-543, 'years').format('MM/DD/YYYY');
                        break;

                    case '2':
                        month = moment(`01/${$('#month2').val()}/${$('#year2 :selected').text()}`, 'DD/MM/YYYY')
                            .add(-543, 'years').format('MM/DD/YYYY');
                        break;
                    case '3':
                        term = $('#term3').val();
                        break;
                    default:
                }

                var json = {
                    'type1': type1,
                    'type2': type2,
                    'date': date,
                    'month': month,
                    'term': term,
                    'level1': level1,
                    'level2': level2,
                    'name': SAC.GetStudentName(),
                };
                var dt = new Date();
                var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107030") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                xhr = new XMLHttpRequest();
                $("body").mLoading();
                xhr.open("POST", "/Card/PermissionCard/Report1.aspx/ExportExcel", true);
                xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                xhr.responseType = 'blob';
                xhr.onload = function () {
                    //aa = xhr.getResponseHeader("filename");
                    saveAs(xhr.response, file_name);
                    $("body").mLoading('hide');
                };
                xhr.send(JSON.stringify({ 'search': json }));
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

            $('#ReportType1').on('change', function () {
                //$('div[id^=filter]').hide();
                //$(`div#filter${$(this).val()}`).show();
                if ($(this).val() == '2') {
                    $(`div#filter2`).show();
                }
                else {
                    $(`div#filter2`).hide();
                }
            });

            $('#ReportType2').on('change', function () {
                $('div[id^=report]').hide();
                $(`div#report${$(this).val()}`).show();
            });

            $("#selectlevel").change(function () {
                LoadLevel2($(this).val(), '#selectclass');
            });

            $("#year3").change(function () {
                LoadTerm($(this).val(), '#term3');
            });

            $('#ReportType1').trigger('change');
            $('#ReportType2').trigger('change');
        });

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107031") %>
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

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %></label>
                            <div class="col-sm-3">
                                <select name="ReportType1" id="ReportType1" class="selectpicker --req-append-last" data-width="100%" data-size="7" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106160") %>" required>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107033") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107034") %></option>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></label>
                            <div class="col-sm-3">
                                <select name="ReportType2" id="ReportType2" class="selectpicker --req-append-last" data-width="100%" data-size="7" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106160") %>" required>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107035") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107036") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107037") %></option>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row" id="report1">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104069") %><br />
                                Date</label>
                            <div class="col-sm-3">
                                <div class="form-group ">
                                    <input type="text" name="date1" id="date1" class="datepicker form-control" value="" required />
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

                        <div class="row" id="report2">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206191") %><br />
                                Academic Year</label>
                            <div class="col-sm-3">
                                <select name="year2" id="year2" class="selectpicker --req-append-last" data-width="100%" data-size="7" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %>" required>
                                    <% foreach (var item in ListYear)
                                        { %>
                                    <option <%= item.Selected ? "selected" : "" %> value="<%=item.Value %>"><%=item.Text %></option>
                                    <% } %>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>/<br />
                                Month</label>
                            <div class="col-sm-3">
                                <select name="month2" id="month2" class="selectpicker --req-append-last" data-width="100%" data-size="7" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107040") %>" required>
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
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row" id="report3">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206191") %><br />
                                Academic Year</label>
                            <div class="col-sm-3">
                                <select name="year3" id="year3" class="selectpicker --req-append-last" data-width="100%" data-size="7" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %>" required>
                                    <% foreach (var item in ListYear)
                                        { %>
                                    <option <%= item.Selected ? "selected" : "" %> value="<%=item.Value %>"><%=item.Text %></option>
                                    <% } %>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206194") %><br />
                                Semester</label>
                            <div class="col-sm-3">
                                <select name="term3" id="term3" class="selectpicker --req-append-last" data-width="100%" data-size="7" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %>" required>
                                    <% foreach (var item in ListTerm)
                                        { %>
                                    <option <%= item.Selected ? "selected" : "" %> value="<%=item.Value %>"><%=item.Text %></option>
                                    <% } %>
                                </select>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row" id="filter2">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>/<br />
                                Level Class</label>
                            <div class="col-sm-3">
                                <select name="selectlevel" id="selectlevel" class="selectpicker  --req-append-last" data-size="7" data-width="100%" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %>" required>
                                    <% foreach (var item in ListLevel1)
                                        { %>
                                    <option <%= item.Selected ? "selected" : "" %> value="<%=item.Value %>"><%=item.Text %></option>
                                    <% } %>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>/<br />
                                Class</label>
                            <div class="col-sm-3">
                                <select name="selectclass" id="selectclass" class="selectpicker  --req-append-last" data-size="7" data-width="100%" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>">
                                </select>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row" id="filter1">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %>/<br />
                                Name surename</label>
                            <div class="col-sm-3">
                                <uc1:StudentAutocomplete runat="server" ID="StudentAutocomplete" />
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107039") %>/<br />
                                List Group</label>
                            <div class="col-sm-3">
                                <select name="selectGroup" id="selectGroup" class="selectpicker --req-append-last" data-width="100%" data-size="7" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>">
                                    <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                    <% foreach (var item in ListGroup)
                                        { %>
                                    <option <%= item.Selected ? "selected" : "" %> value="<%=item.Value %>"><%=item.Text %></option>
                                    <% } %>
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


                <div class="card ">
                    <div class="card-header card-header-primary card-header-icon">
                        <div class="card-icon">
                            <span class="material-icons">list_alt</span>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107030") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12" style="">
                                <table id="template1" class="table-hover dataTables display nowrap" width="100%" style="margin: 0 5px;">
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

    <!-- Modal -->
    <div class="modal fade" id="Modal1" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div id="wrapper1"></div>

            </div>
        </div>
    </div>
</asp:Content>
