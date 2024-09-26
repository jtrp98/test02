<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" EnableEventValidation="false" Async="true" Inherits="FingerprintPayment.StudentCall.Report" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- DatetimePicker -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.css" />
    <link href="Css/css.css" rel="stylesheet" />
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <%-- <link href="icofont/icofont.min.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402008") %>         
            </p>
        </div>
    </div>

    <form id="aspnetForm" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release">
        </asp:ScriptManager>
        <asp:HiddenField ID="hdfsid" runat="server" />
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
                        <div class="row ml-md-5">

                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                            <div class="col-md-2 ">
                                <asp:TextBox runat="server" ID="txtstart" CssClass="form-control  datepicker --date-validate" MaxLength="10" Style="" required autocomplete="off" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlsublevel" onchange="ddlclass()" runat="server" CssClass="selectpicker col-md-12" data-style="select-with-transition">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlsublevel2" ClientIDMode="Static" AutoPostBack="false" runat="server" CssClass="ddl2 selectpicker col-md-12" data-style="select-with-transition" Width="100%">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row ml-md-5">
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></label>
                            <div class="col-md-2 ">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl2 selectpicker col-md-12" data-style="select-with-transition" Width="100%">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402004") %>" Value="1" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402005") %>" Value="2" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402010") %>" Value="3" />
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                            <div class="col-md-2 ">
                                <uc1:StudentAutocomplete runat="server" ID="StudentAutocomplete" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 button-section text-center">
                                <button type="button" id="btnSearch" class="btn btn-info search-btn" onclick="onSearch()">
                                    <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
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

                        <table id="datatable1" class="table-hover dataTable" width="100%" style="width: 100%">
                            <thead>
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
                            </thead>

                        </table>


                    </div>

                </div>
            </div>
        </div>
    </form>



    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js" type="text/javascript"></script>--%>


    <%--  <div class="full-card box-content userlist-container">
    

        <div class="row">
        </div>

        <div class="row ">
        </div>


        <br />
        <div class=" row ">
            <div class="col-md-12">
               
            </div>
        </div>
    </div>--%>

    <%--    <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <%--  <script type="text/javascript" language="javascript" src="//cdn.datatables.net/1.10.22/js/jquery.dataTables.min.js"></script>--%>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/1.6.4/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/1.6.4/js/buttons.flash.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <%--	<script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/1.6.4/js/buttons.html5.min.js"></script>--%>
    <script src="Js/buttons.html5.js"></script>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/1.6.4/js/buttons.print.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/bootstrap-datetimepicker/moment-with-locales.js"></script>
    <script type='text/javascript' src="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.th.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" type="text/javascript"></script>

    <script>
        var $table;
        function ddlclass() {
            //$("body").mLoading();
            var ddl2 = document.getElementsByClassName("ddl2");

            for (i = -1; i <= 90; i++) {
                ddl2[1].remove(0);
            }

            setTimeout(function () {
                $.get("/App_Logic/ddlclassroom.ashx?idlv=" + document.getElementById('<%=ddlsublevel.ClientID%>').value, function (Result) {
                    var opt = document.createElement("option");
                    // Assign text and value to Option object
                    opt.text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>";
                    opt.value = "";
                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);
                    $.each(Result, function (index) {

                        // Create an Option object       
                        var opt = document.createElement("option");

                        // Assign text and value to Option object
                        opt.text = Result[index].name;
                        opt.value = Result[index].value;
                        //if (getUrlParameter("idlv2") == Result[index].value) {
                        //    opt.selected = "selected";
                        //}
                        // Add an Option object to Drop Down List Box
                        document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);
                    });
                    $('#<%=ddlsublevel2.ClientID%>').selectpicker('refresh');
                });

            }, 300);
            /*  $("body").mLoading('hide');*/
        }

        function onSearch() {

            if (!$("#aspnetForm").valid()) {
                return;
            }

            if ($.fn.dataTable.isDataTable('#datatable1')) {

                $table.destroy();
              <%-- $table= $('#datatable1').DataTable({
                    paging: false,
                    searching: false,
                    ajax: "<%= ResolveUrl("~/StudentCall/ReportData.ashx") %>?date=" + $("#ctl00_MainContent_txtstart").val() + "&status=" + ($("#ctl00_MainContent_ddlStatus").val() || "0")
                        + "&lvl2=" + ($("#ddlsublevel2").val() || "0") + "&name=" + $("#ctl00_MainContent_tags").val(),
                });--%>
            }
            else {
              <%--  $table = $('#example').DataTable({
                    paging: false,
                    searching: false,
                    ajax: "<%= ResolveUrl("~/StudentCall/ReportData.ashx") %>?date=" + $("#ctl00_MainContent_txtstart").val() + "&status=" + ($("#ctl00_MainContent_ddlStatus").val() || "0")
                        + "&lvl2=" + ($("#ddlsublevel2").val() || "0") + "&name=" + $("#ctl00_MainContent_tags").val(),
                });--%>
            }
            $table = $('#datatable1').DataTable({
                paging: true,
                "pageLength": 20,
                "bLengthChange": false,
                searching: false,
                ajax: "<%= ResolveUrl("~/StudentCall/ReportData.ashx") %>?date=" + $("#ctl00_MainContent_txtstart").val()
                    + "&status=" + ($("#ctl00_MainContent_ddlStatus").val() || "0")
                    + "&lvl1=" + ($("#ctl00_MainContent_ddlsublevel").val() || "0")
                    + "&lvl2=" + ($("#ddlsublevel2").val() || "0")
                    + "&name=" + SAC.GetStudentName(),
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excel',
                        title: 'Student Calling List <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133167") %> ' + $("#ctl00_MainContent_txtstart").val() + '',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
                            order: [[7, "asc"]]
                        },
                        customize: function (xlsx) {
                            var sheet = xlsx.xl.worksheets['sheet1.xml'];
                            //$('row c[r=A1]', sheet).attr('s', '2').attr('s', '51');
                            //$('c[r=A1] t', sheet).text('Custom text');
                        }
                    }
                ],

                columns: [
                    { "data": "no" },
                    { "data": "level" },
                    { "data": "code" },
                    { "data": "title" },
                    { "data": "fullName" },
                    {
                        "data": "status", "mRender": function (data, type, row) {
                            switch (data) {
                                case "1":
                                    return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402004") %>";
                                case "2":
                                    return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402005") %>";
                                case "3":
                                    return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402010") %>";
                                default:
                            }

                        }
                    },
                    { "data": "date0" },
                    { "data": "date1" },
                    { "data": "date2" },
                    { "data": "date3" },
                    {
                        "data": "isAnnouce", "mRender": function (data, type, row) {
                            switch (data + "") {
                                case "0":
                                    return "";

                                case "1":
                                    switch (row.status) {
                                        case "1":
                                        case "2":
                                            return "<a href='#' onclick='sendAnnouce(" + row.sid + ")' class='enable-annouce roundbtn' ><span class='material-icons'>campaign</span></a>";

                                        case "3":
                                            return "<a href='#' class='disable-annouce roundbtn'><span class='material-icons'>campaign</span></a>";
                                    }
                                    return "";
                                default:
                            }

                            //return "<a href='Admin/Categories/Edit/" + data + "'>EDIT</a>";
                        }
                    },
                ]
            });
           
        }

        function sendAnnouce(sid) {
            PageMethods.ResendAnnouncement(sid,
                function (response) {
                    //console.log("sent");
                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101266") %>',
                        //text: 'Something went wrong!',                      
                    })
                },
                function (response) {
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133290") %>',
                        //text: 'Something went wrong!',                       
                    })
                });
        }

        var availableValueUsers = [];

        function lightwellBuyer(request, response) {
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
            availableValue = availableValueUsers;
            for (i = 0, l = availableValue.length; i < l; i++) {
                obj = availableValue[i];
                //if (obj.code != null && obj.code != undefined && obj.code != "") {
                if (hasMatch(obj.label) || hasMatch(obj.code)) {
                    matches.push(obj);
                }
                //}
            }
            response(matches.slice(0, 10));
        }

        function setupTagName() {
            //$("body").mLoading();

            //$.get("/app_logic/studentlist.ashx", function (Result) {
            //    $.each(Result, function (index) {
            //        availableTags.push(Result[index].fullName);
            //    });
            //});

            //$("#ctl00_MainContent_tags").autocomplete({
            //    source: availableTags
            //});

            $.ajax({
                url: "/App_Logic/dataGenericListData.ashx?mode=liststudent&nelevel=&nsublevel=",
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].sName,
                            value: objjson[index].sID,
                            code: objjson[index].studentid,
                        };

                        availableValueUsers.push(newObject);
                    });
                }
            });

            //$('#ctl00_MainContent_tags').autocomplete({
            //    width: 300,
            //    max: 10,
            //    delay: 100,
            //    minLength: 1,
            //    maxLength: 10,
            //    autoFocus: true,
            //    cacheLength: 1,
            //    scroll: true,
            //    highlight: false,
            //    //source: function (request, response) {
            //    //    var results = $.ui.autocomplete.filter(availableValuestudent, request.term);
            //    //    response(results.slice(0, 10));
            //    //},
            //    source: lightwellBuyer,
            //    select: function (event, ui) {
            //        event.preventDefault();
            //        $("#ctl00_MainContent_tags").val(ui.item.label);
            //        $("#txtid").val(ui.item.value);
            //        console.log(ui.item);
            //    },
            //    focus: function (event, ui) {
            //        event.preventDefault();
            //    }
            //});

        }

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

            setupTagName();
        });
    </script>
</asp:Content>
