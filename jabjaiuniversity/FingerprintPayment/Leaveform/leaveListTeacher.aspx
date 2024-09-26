<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="leaveListTeacher.aspx.cs" Inherits="FingerprintPayment.leaveListTeacher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/TeacherStudentAutocomplete.ascx" TagPrefix="uc1" TagName="TeacherStudentAutocomplete" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <%--    <link href="/Styles/jquery-ui.css" rel="stylesheet" />--%>
       <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <%--<script src="Script/updateEmpStatus.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmss") %>" type="text/javascript"></script>--%>

    <%--     <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.2/css/all.css" integrity="sha384-oS3vJWv+0UjzBfQzYUhtDYW+Pj2yciDJxpsK1OYPAYjqT085Qq/1cq5FLXAZQ7Ay" crossorigin="anonymous" />

    <style>
        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }

        .pad0 {
            padding: 0px !important;
        }

        .cover {
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .listItem {
            color: blue;
            background-color: White;
        }

        .hid {
            visibility: hidden;
        }

        .hid2 {
            visibility: hidden;
            display: none;
        }

        .width10 {
            margin: 0 auto;
            width: 10%;
        }

        .centertext {
            text-align: center;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }

        label {
            font-weight: normal;
            font-size: 26px;
        }

        .gvbutton {
            font-size: 25px;
        }

        .nounder a:hover {
            text-decoration: none;
        }

        .shadowblack {
            text-decoration: none;
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .boxhead a {
            color: #FFFFFF;
            text-decoration: none;
        }

        a.imjusttext {
            color: #ffffff;
            text-decoration: none;
        }

            a.imjusttext:hover {
                color: aquamarine;
            }

        .centerText {
            text-align: center;
        }

        .btn-red {
            background: red; /* use your color here */
        }


        .nowrap {
            max-width: 100%;
            white-space: nowrap;
        }

        .namemangin {
            margin-left: 5px;
            padding-left: 35px;
            border-left: 10px
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }

        .tab {
            border-collapse: collapse;
            margin-left: 6px;
            margin-right: 6px;
            border-bottom: 3px solid #337AB7;
            border-left: 3px solid #337AB7;
            border-right: 3px solid #337AB7;
            border-top: 3px solid #337AB7;
            box-shadow: inset 0 1px 0 #337AB7;
        }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="JSLeaveform.js?v=<%=DateTime.Now.Ticks%>" type="text/javascript"></script>
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/excanvas.min.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.time.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>

    <script type='text/javascript' src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>

    <!-- DataTables -->
    <%-- <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>--%>

    <script type="text/javascript" src="/scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js"></script>
    <script src="../Scripts/moment.js" type="text/javascript"></script>
    <script src="LeaveDatalistTeacher.js?v=<%=DateTime.Now.Ticks%>" type="text/javascript"></script>

    <script type="text/javascript">
        //$(document).ready(function () {
        //    $(function () {
        //        $("#startDay").datepicker({ dateFormat: 'dd/mm/yy' });
        //        $("#endDay").datepicker({ dateFormat: 'dd/mm/yy' });
        //    });
        //});

        function relink() {

            var name = document.getElementsByClassName("linkname");
            var job = document.getElementsByClassName("linkjob");
            var from = document.getElementsByClassName("linkfrom");
            var to = document.getElementsByClassName("linkto");
            var year = document.getElementsByClassName("linkyear");
            var status = document.getElementsByClassName("linkstatus");

            window.location.href = "leaveList.aspx?job=" + job[0].value + "&year=" + year[0].value + "&end=" + to[0].value + "&start=" + from[0].value + "&name=" + name[0].value + "&status=" + status[0].value;
        }

        $(function () {

            //md.initFormExtendedDatetimepickers();

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

            $('#endDay').on('dp.change', function (e) {
                var d1 = moment($("#startDay").val(), 'DD/MM/YYYY');
                var d2 = moment($("#endDay").val(), 'DD/MM/YYYY');

                if (d2 < d1) {
                    //alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131144") %>');
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131144") %>',
                        //text: 'Something went wrong!',                      
                    })
                    $("#endDay").val('');
                }
            })

            $(".datepicker").keydown(function (e) {
                e.preventDefault();
            });

        });


    //function start() {


    //    var availableTags = [];


    //    $.get("/PreRegister/preRegisterStudentList.ashx", function (Result) {
    //        $.each(Result, function (index) {
    //            availableTags.push(Result[index].fullName);
    //        });
    //    });


    //    $("#ctl00_MainContent_ddlnamedrop").autocomplete({
    //        source: availableTags
    //    });


    //}

    //$(window).on('load', function () {
    //    start();


    //});

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102228") %>          
            </p>
        </div>
    </div>

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>

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

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102229") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="DropDownList1" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102230") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="ddlstatus" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value=""></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102232") %>" Value="reject"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102233") %>" Value="accept"></asp:ListItem>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102231") %>" Value="wait"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102234") %></label>
                            <div class="col-md-3 ">
                                <div class="form-group ">
                                    <asp:TextBox ID="startDay" runat="server" ClientIDMode="static" CssClass="form-control datepicker" Width="100%" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105040") %></label>
                            <div class="col-md-3 ">
                                <div class="form-group ">
                                    <asp:TextBox ID="endDay" runat="server" ClientIDMode="static" CssClass="form-control datepicker" Width="100%" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row ">
                            <div class="col-md-1 d-none"></div>
                            <label class="col-md-1  col-form-label text-left d-none"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105059") %></label>
                            <div class="col-md-3 d-none">
                                <asp:DropDownList ID="ddlJob" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%">
                           <%--         <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="all"></asp:ListItem>--%>
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02309") %>"  Selected="True" Value="teacher"></asp:ListItem>
                         <%--           <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %>" Value="student"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                            <div class="col-md-3 ">
                                <uc1:TeacherStudentAutocomplete runat="server" ID="TeacherStudentAutocomplete" />
                                <%-- <asp:TextBox ID="ddlnamedrop" onchange="" runat="server" Width="100%" CssClass="linkname js-example-basic-single form-control" name="classchoice" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>">        
                                </asp:TextBox>--%>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-center">
                                <br />
                                <button type="button" id="btnSearch" class="btn btn-fill btn-info">
                                    <span class="material-icons">search</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-warning  card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102145") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12">

                                <table id="tableData" class="table-hover dataTable" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th style="width: 6%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th style="width: 6%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></th>
                                            <th style="width: 17%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102236") %></th>
                                            <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102237") %></th>
                                            <th style="width: 16%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102238") %></th>
                                            <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102239") %></th>
                                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                            <th style="width: 12%" class="text-center"></th>
                                        </tr>
                                    </thead>

                                    <tfoot>
                                        <tr>
                                            <th colspan="10">
                                                <div class="row">
                                                    <div class="col-md-4 mb-4 text-left" style="padding-left: 4%;">
                                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>: </span>
                                                        <select id="sltPageSize" class="selectpicker" data-style="select-with-transition" data-width="40%">
                                                            <option value="20">20</option>
                                                            <option value="50">50</option>
                                                            <option selected="selected" value="100">100</option>
                                                        </select>
                                                    </div>
                                                    <div class="col-md-4 mb-4 text-center">
                                                        <a id="aPrevious" href="#" style="text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></a>
                                                        <select id="sltPageIndex" class="selectpicker" data-style="select-with-transition" data-width="40%">
                                                        </select>
                                                        <a id="aNext" href="#" style="text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></a>
                                                    </div>
                                                    <div class="col-md-4 mb-4 text-right" style="padding-right: 2%;">
                                                        <span id="spnPageInfo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132005") %></span>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>


