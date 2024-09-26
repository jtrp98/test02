<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="FingerprintPayment.UpdateStatus.index" %>

<%@ Register Src="~/UserControls/YTFilter.ascx" TagPrefix="uc1" TagName="YTFilter" %>
<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>
    <%--  <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>--%>

    <%--<link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>--%>

    <%--  <script src="/bootstrap/bootstrap-chosen/chosen.jquery.js" type="text/javascript"></script>
    <link href="/bootstrap/bootstrap-chosen/bootstrap-chosen.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style type="text/css">
        /*.btn-status0.active, .btn-status1.active, .btn-status2.active,
        .btn-status3.active, .btn-status4.active, .btn-status5.active {
            color: #000 !important;
        }*/

        .btn-status0 {
            background-color: #33cc66;
            color: #fff;
        }

        .btn-status1 {
            background-color: #ff9966;
            color: #fff;
        }

        .btn-status2 {
            background-color: #ff6666;
            color: #fff;
        }

        .btn-status3 {
            background-color: #cc66cc;
            color: #fff;
        }

        .btn-status4 {
            background-color: #9933cc;
            color: #fff;
        }

        .btn-status5 {
            background-color: #3399cc;
            color: #fff;
        }

        .btn-status6 {
            background-color: #fdb954;
            color: #fff;
        }

        .chosen-container-multi .chosen-choices .search-choice .search-choice-close {
            background-size: 440%;
        }

        .swal2-popup {
            width: 30em !important;
            font-size: 1.2em !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script src="Script/updateStatus.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmss") %>" type="text/javascript"></script>

    <script src="/bootstrap SB2/bower_components/flot/excanvas.min.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.time.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js" type="text/javascript"></script>

    <script>
        var __allClass;
        var _username = '<%= UserData.Name %>';
        function getAllClass() {

            $.ajax({
                async: false,
                type: "POST",
                url: "index.aspx/GetAllLevelClass",
                //data: '{subLevelID: ' + subLevelID + ' }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    __allClass = response.d;
                },
                failure: function (response) {
                    console.log(response.d);
                },
                error: function (response) {
                    console.log(response.d);
                }
            });
        }

        function printSlip() {
            var studentId = SAC.GetStudentID();
            if (studentId === "") {
                $.confirm({
                    title: false,
                    content: '<img src="/images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><h1 class="text-danger text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602024") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133395") %></h1>',
                    theme: 'material',
                    columnClass: 'col-md-5 col-md-offset-4',
                    type: 'red',
                    buttons: {
                        "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                            keys: ['enter', 'shift'],
                            btnClass: 'btn-primary',
                            action: function () {
                                //console.log(result);
                            }
                        }
                    }
                });
                return;
            }

            var name = $('#student_Name').html();
            var code = $('#student_Code').html();
            var late = $('#Status_1').html();
            var absent = $('#Status_2').html();
            var businessLeave = $('#Status_4').html();
            var sickLeave = $('#Status_5').html();
            //console.log(name);
            var currentUrl = window.location.href.toLowerCase();
            var split = currentUrl.split('/updatestatus');
            var printSlipURL = split[0] + "/Slip/AllowEnterClass.aspx?"
                + "name=" + name + "&code=" + code + "&late=" + late + "&absent=" + absent + "&businessLeave=" + businessLeave + "&sickLeave=" + sickLeave + `&sid=${studentId}&term=${YTF.GetTermID()}`;

            window.open(
                printSlipURL,
                '_blank' // <- This is what makes it open in a new window.
            );


            //alert(url);
        }

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


            getAllClass();

            $("#selectType").on('change', function (e) {
                var _v = $(this).val();
                $('.typewrapper').hide();
                $('.-type' + _v).show();

                if (_v == "1") {
                    $("#userProfile").show();
                    $("#btnSearchWrapper").show();
                    $(".historywrapper").show();
                    $(".status-unknow").show();
                }
                else if (_v == "2") {
                    $("#userProfile").hide();
                    $("#btnSearchWrapper").hide();
                    $(".historywrapper").hide();
                    $(".status-unknow").show();
                } else {
                    $("#userProfile").hide();
                    $("#btnSearchWrapper").hide();
                    $(".historywrapper").hide();
                    $(".status-unknow").hide();
                }
            });

            $("#ddlsublevel").on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
                $("body").mLoading();

                var $ddl = $('#ddlclass');
                if (isSelected) {
                    if (e.target.options[clickedIndex].selected) {
                        var d = __allClass.filter(o => o.levelId == e.target.options[clickedIndex].value).shift();

                        d.classList.forEach(e => {
                            $ddl.append($('<option>', {
                                value: e.classId,
                                text: e.className
                            }));
                        });
                    }
                } else {
                    if (!e.target.options[clickedIndex].selected) {
                        var d = __allClass.filter(o => o.levelId == e.target.options[clickedIndex].value).shift();

                        d.classList.forEach(e => {
                            $ddl.find("option[value='" + e.classId + "']").remove();
                        });
                    }
                }

                $('#ddlclass').selectpicker("refresh");
                $("body").mLoading('hide');


            });

            $('#termNo').html(YTF.GetTermText());
            $('#yearNo').html(YTF.GetYearNo());
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301002") %>
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
                        <uc1:YTFilter runat="server" ID="YTFilter" />
                        <div class="row">

                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %><br />
                                Type :</label>
                            <div class="col-sm-3">
                                <select class="selectpicker " data-width="100%" data-size="7" data-style="select-with-transition" id="selectType">
                                    <option value="1" selected="selected"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206367") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206366") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                </select>
                            </div>
                            <div class="col-sm-1"></div>

                            <label class="col-sm-1 col-form-label text-left typewrapper -type1">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301008") %><br />
                                Student Name :</label>
                            <div class="col-sm-3 typewrapper -type1">
                                <uc1:StudentAutocomplete runat="server" ID="StudentAutocomplete" />
                                <%--<input type="text" class='form-control' id="txtid" style="display: none;" />
                                <input type="text" class='form-control' id="txtname" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101340") %>" tabindex="0" />--%>
                            </div>
                            <div class="col-sm-2"></div>

                            <div class="col-sm-12 typewrapper -type2 p-0" style="display: none">
                                <div class="row ">
                                    <div class="col-sm-1"></div>
                                    <label class="col-sm-1 col-form-label text-left">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %><br />
                                        Level Class :</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlsublevel" runat="server" multiple ClientIDMode="Static" DataTextField="Text" DataValueField="Value" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>..." class="selectpicker " data-width="100%" data-size="7" data-style="select-with-transition">
                                            <%--<asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" />--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1"></div>

                                    <label class="col-sm-1 col-form-label text-left">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %><br />
                                        Class :</label>
                                    <div class="col-sm-3">
                                        <select id="ddlclass" class="selectpicker " data-width="100%" data-size="7" data-style="select-with-transition" multiple title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106126") %>..."></select>
                                    </div>
                                    <div class="col-sm-2"></div>
                                </div>
                            </div>

                            <div class="col-sm-12 typewrapper -type3 p-0" style="display: none">
                            </div>

                        </div>

                        <div class="row" id="btnSearchWrapper">
                            <div class="form-group col-sm-5">
                            </div>
                            <div class="form-group col-sm-2 btn btn-success" id="btnSearch">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                            </div>
                            <div class="form-group col-sm-5">
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
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></h4>
                    </div>
                    <div class="card-body ">
                        <div id="userProfile" class="row">
                            <div class="col-sm-12 col-lg-3" id="Picture">
                                <div class="col-lg-12 center" style="padding: 10px;">
                                    <i class="fa fa-4x fa-user" style="background-color: #39c; width: 180px; height: 180px; padding: 30px; border-radius: 50%;"></i>
                                </div>
                            </div>
                            <div class="col-sm-12 col-lg-9">
                                <div class="row">
                                    <label class="col-lg-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                                    <span class="col-lg-3 output" id="student_Name"></span>
                                    <label class="col-lg-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></label>
                                    <span class="col-lg-3 output" id="student_Code"></span>
                                </div>
                                <div class="row">
                                    <label class="col-lg-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104064") %></label>
                                    <span class="col-lg-3 output" id="Identification"></span>
                                    <label class="col-lg-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105034") %></label>
                                    <span class="col-lg-3 output" id="birthDay"></span>
                                </div>
                                <div class="row">
                                    <label class="col-lg-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301010") %></label>
                                    <span class="col-lg-3 output" id="father_Name"></span>
                                    <label class="col-lg-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                    <span class="col-lg-3 output" id="Phone"></span>
                                </div>
                                <div class="row">
                                    <label class="col-lg-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301011") %></label>
                                    <span class="col-lg-3 output" id="mother_Name"></span>
                                    <label class="col-lg-3 hidden"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104041") %></label>
                                    <span class="col-lg-3 output hidden" id="Mobile"></span>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301013") %> :</label>
                            <div class="col-sm-3">
                                <div class="form-group has-successx">
                                    <input type="text" class='form-control datepicker' id="txtdaystart" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301014") %>" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>

                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105040") %> :</label>
                            <div class="col-sm-3">
                                <div class="form-group has-successx">
                                    <input type="text" class='form-control datepicker' id="txtdayend" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301014") %>" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>

                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row">

                            <div class="col-sm-2">
                                <button type="button" class="btn btn-status0 col-lg-10" onclick="UpdateStudntStatus(0)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></button>
                            </div>
                            <div class="col-sm-2">
                                <button type="button" class="btn btn-status1 col-lg-10" onclick="UpdateStudntStatus(1)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></button>
                            </div>
                            <div class="col-sm-2">
                                <%--<button type="button" class="btn btn-status2  col-lg-10" onclick="UpdateStudntStatus(3)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></button>--%>
                                <div class="dropdown">
                                    <button class="btn btn-status2 dropdown-toggle col-lg-10" type="button" data-toggle="dropdown">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %><span class="caret" style="float: right; margin-top: 15px;"></span>
                                    </button>
                                    <ul class="dropdown-menu" style="font-size: 24px">
                                        <li><a href="#" onclick="UpdateStudntStatus(3)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301018") %></a></li>
                                        <li><a href="#" onclick="UpdateStudntStatus(3,0)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301019") %></a></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <button type="button" class="btn btn-status5  col-lg-10" onclick="UpdateStudntStatus(12)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %></button>
                            </div>
                            <div class="col-sm-2">
                                <%--<button type="button" class="btn btn-status4  col-lg-10" onclick="UpdateStudntStatus(10)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></button>--%>
                                <div class="dropdown">
                                    <button class="btn btn-status4 dropdown-toggle col-lg-10" type="button" data-toggle="dropdown">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %><span class="caret" style="float: right; margin-top: 15px;"></span>
                                    </button>
                                    <ul class="dropdown-menu" style="font-size: 24px">
                                        <li><a href="#" onclick="UpdateStudntStatus(10)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301020") %></a></li>
                                        <li><a href="#" onclick="UpdateStudntStatus(10,0)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301021") %></a></li>
                                        <li><a href="#" onclick="UpdateStudntStatus(10,1)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301022") %></a></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <%-- <button type="button" class="btn btn-status3  col-lg-10" onclick="UpdateStudntStatus(11)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></button>--%>
                                <div class="dropdown">
                                    <button class="btn btn-status3 dropdown-toggle col-lg-10" type="button" data-toggle="dropdown">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %><span class="caret" style="float: right; margin-top: 15px;"></span>
                                    </button>
                                    <ul class="dropdown-menu" style="font-size: 24px">
                                        <li><a href="#" onclick="UpdateStudntStatus(11)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301023") %></a></li>
                                        <li><a href="#" onclick="UpdateStudntStatus(11,0)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301024") %></a></li>
                                        <li><a href="#" onclick="UpdateStudntStatus(11,1)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301025") %></a></li>
                                    </ul>
                                </div>
                            </div>

                            <div class="col-sm-2 ">
                                <button type="button" class="btn btn-success  col-lg-10" onclick="UpdateStudntStatus(8)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %></button>
                            </div>

                            <div class="col-sm-2 status-unknow">
                                <button type="button" class="btn btn-status6  col-lg-10" onclick="UpdateStudntStatus(99)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103022") %></button>
                            </div>

                        </div>

                        <div class="row--space">
                            <hr />
                        </div>

                        <div class="row historywrapper">                             
                            <div class="form-group col-sm-10" style="padding: 0px">
                                <h4><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301027") %> <span id="termNo"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> <span id="yearNo"></span></b></h4>
                            </div>

                            <div class="col-sm-2" style="margin-top: 10px">
                                <button type="button" class="btn btn-primary  col-lg-10" onclick="printSlip()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301028") %></button>
                                <i class="fa fa-question-circle" style="font-size: 30px; color: #337ab7; margin-left: 1px; margin-top: 8px"
                                    data-original-title="*<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301029") %>* " data-placement="bottom" data-toggle="tooltip"></i>
                            </div>

                        </div>
                        <div class="row historywrapper">
                            <div class=" col-sm-6">
                                <div class="row m-2">
                                    <div class="col-lg-4">
                                        <i class="fa fa-users" style="background-color: #33cc66; padding: 8px; border-radius: 25px; color: #fff;"></i>
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="Status_0">0</span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                                    </div>
                                    <div class="col-lg-4">
                                        ( <span id="percent_0">0</span> % )
                                    </div>
                                </div>
                                <div class="row m-2">
                                    <div class="col-lg-4">
                                        <i class="fa fa-users" style="background-color: #ff9966; padding: 8px; border-radius: 25px; color: #fff;"></i>
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="Status_1">0</span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                                    </div>
                                    <div class="col-lg-4">
                                        ( <span id="percent_1">0</span> % )
                                    </div>
                                </div>
                                <div class="row m-2">
                                    <div class="col-lg-4">
                                        <i class="fa fa-users" style="background-color: #ff6666; padding: 8px; border-radius: 25px; color: #fff;"></i>
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></span>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="Status_2">0</span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                                    </div>
                                    <div class="col-lg-4">
                                        ( <span id="percent_2">0</span> % )
                                    </div>
                                </div>
                                <div class="row m-2">
                                    <div class="col-lg-4">
                                        <i class="fa fa-users" style="background-color: #3399cc; padding: 8px; border-radius: 25px; color: #fff;"></i>
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %></span>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="Status_3">0</span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                                    </div>
                                    <div class="col-lg-4">
                                        ( <span id="percent_3">0</span> % )
                                    </div>
                                </div>
                                <div class="row m-2">
                                    <div class="col-lg-4">
                                        <i class="fa fa-users" style="background-color: #9933cc; padding: 8px; border-radius: 25px; color: #fff;"></i>
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></span>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="Status_4">0</span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                                    </div>
                                    <div class="col-lg-4">
                                        ( <span id="percent_4">0</span> % )
                                    </div>
                                </div>
                                <div class="row m-2">
                                    <div class="col-lg-4">
                                        <i class="fa fa-users" style="background-color: #cc66cc; padding: 8px; border-radius: 25px; color: #fff;"></i>
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></span>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="Status_5">0</span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                                    </div>
                                    <div class="col-lg-4">
                                        ( <span id="percent_5">0</span> % )
                                    </div>
                                </div>
                                <div class="row m-2">
                                    <div class="col-lg-4">
                                        <i class="fa fa-users" style="background-color: #e8e8e8; padding: 8px; border-radius: 25px; color: #fff;"></i>
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301030") %></span>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="Status_6">0</span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                                    </div>
                                    <div class="col-lg-4">
                                        ( <span id="percent_6">0</span> % )
                                    </div>
                                </div>
                            </div>
                            <div class=" col-sm-6 ">
                                <div class="flot-chart">
                                    <div class="flot-chart-content" id="flot-pie-chart2" style="height: 350px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

</asp:Content>


