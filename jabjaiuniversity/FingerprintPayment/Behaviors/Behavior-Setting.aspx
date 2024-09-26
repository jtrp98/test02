<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="Behavior-Setting.aspx.cs" Inherits="FingerprintPayment.Behavior_Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap-toggle.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-toggle.js" type="text/javascript"></script>

    <!-- Forms Validations Plugin -->
    <script type='text/javascript' src="../PreRegister/assets/js/plugins/jquery.validate.min.js"></script>
    <script type='text/javascript' src="../PreRegister/assets/js/plugins/additional-methods.min.js"></script>
    <script type='text/javascript' src="../Content/Material/assets/js/plugins/bootstrap-selectpicker.js"></script>

    <script type="text/javascript">
        $(function () {

            //$('.timepicker').datetimepicker({
            //    // format: 'H:mm A',    // use this format if you want the 24hours timepicker
            //    format: 'HH:mm', //use this format if you want the 12hours timpiecker with AM/PM toggle
            //    icons: {
            //        time: "fa fa-clock-o",
            //        date: "fa fa-calendar",
            //        up: "fa fa-chevron-up",
            //        down: "fa fa-chevron-down",
            //        previous: 'fa fa-chevron-left',
            //        next: 'fa fa-chevron-right',
            //        today: 'fa fa-screenshot',
            //        clear: 'fa fa-trash',
            //        close: 'fa fa-remove'

            //    }
            //});

            // Validate rule for formpopup

            $("#behavior_Late_SettingTime").click(function () {
                if ($("#behavior_Late_SettingTime").prop("checked")) {
                    $("[name*=setting_point]").show();
                } else {
                    $("[name*=setting_point]").hide();
                }
            });

            $("#formpopup").validate({
                rules: {
                    behavior_score: {
                        required: true,
                        number: true
                    },
                    behavior_late: {
                        required: true,
                        number: true
                    },
                    behavior_absence: {
                        required: true,
                        number: true
                    },
                    behavior_errand: {
                        required: true,
                        number: true
                    },
                    behavior_sick: {
                        required: true,
                        number: true
                    },
                    behavior_uncheckname: {
                        required: true,
                        number: true
                    },
                    behavior_alert: {
                        required: true,
                        number: true
                    }
                },
                messages: {
                    behavior_score: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    behavior_late: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    behavior_absence: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    behavior_errand: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    behavior_sick: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    behavior_uncheckname: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                    behavior_alert: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    },
                },
                focusInvalid: false,
                invalidHandler: function () {
                    $(this).find(":input.error:first").focus();
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "0": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            $("#modalpopup-data").on('show.bs.modal', function () {
                $('#modalpopup-data').css("top", "0%");
            });

            $('#behavior_show_minus_sign').bootstrapToggle();

            $("#modalpopup-data-submit").click(function () {

                if ($('#formpopup').valid()) {
                    let lateSettingTimes = [];
                    if ($("#behavior_Late_SettingTime").prop("checked")) {
                        $.each($("[name=setting_point_0] div.row"), function (e, s) {
                            lateSettingTimes.push({
                                "TimeStatr": $(s).find("[name=timein]").val(),
                                "TimeEnd": $(s).find("[name=timeout]").val(),
                                "cut_point": $(s).find("[name=cut_point]").val(),
                                "BehaviorTimeSettingID": $(s).find("[name=BehaviorTimeSettingID]").val(),
                            });
                        })
                    }

                    var data = {
                        "behavior_score": $("#behavior_score").val(),
                        "behavior_type": $("#behavior_type").val(),
                        "behavior_late": $("#behavior_late").val(),
                        "behavior_absence": $("#behavior_absence").val(),
                        "behavior_errand": $("#behavior_errand").val(), // ###
                        "behavior_sick": $("#behavior_sick").val(), // ###
                        "behavior_uncheckname": $("#behavior_uncheckname").val(), // ###
                        "behavior_begin_late": $("#behavior_begin_late").val(),
                        "behavior_begin_absence": $("#behavior_begin_absence").val(),
                        "behavior_begin_errand": $("#behavior_begin_errand").val(), // ###
                        "behavior_begin_sick": $("#behavior_begin_sick").val(), // ###
                        "behavior_begin_uncheckname": $("#behavior_begin_uncheckname").val(), // ###
                        "behavior_time_start_cut_point": $("#behavior_time_start_cut_point").val(), // ###
                        "behavior_absence_half_point": $("#behavior_absence_half_point").prop('checked') ? "1" : "0", // ###
                        "behavior_show_alert": $("#behavior_alert").val(),
                        "behavior_show_minus_sign": ($("#behavior_show_minus_sign").prop("checked") ? 1 : 0),
                        "behavior_Late_SettingTime": ($("#behavior_Late_SettingTime").prop("checked") ? true : false),
                        "lateSettingTimes": lateSettingTimes
                    };

                    console.log(data);

                    PageMethods.updatedata(data,
                        function (result) {
                            $("span[id$=score]").html(result.behavior_score);
                            $("span[id$=type]").html(result.behavior_type_name);
                            $("span[id$=late]").html(result.behavior_late);
                            $("span[id$=absence]").html(result.behavior_absence);
                            $("span[id$=errand]").html(result.behavior_errand); // ###
                            $("span[id$=sick]").html(result.behavior_sick); // ###
                            $("span[id$=uncheckname]").html(result.behavior_uncheckname); // ###
                            $("span[id$=begin_late]").html(result.behavior_begin_late);
                            $("span[id$=begin_absence]").html(result.behavior_begin_absence);
                            $("span[id$=begin_errand]").html(result.behavior_begin_errand); // ###
                            $("span[id$=begin_sick]").html(result.behavior_begin_sick); // ###
                            $("span[id$=begin_uncheckname]").html(result.behavior_begin_uncheckname); // ###
                            $("span[id$=time_start_cut_point]").html(result.behavior_time_start_cut_point); // ###
                            $("input[id$=absence_half_point]").prop('checked', (result.behavior_absence_half_point == "1")); // ###
                            $("span[id$=show_minus_sign]").html(result.behavior_show_minus_sign_name);
                            $("span[id$=show_behavior_alert]").html(result.behavior_show_alert);

                            $("input[id$=late_setting_point]").prop('checked', result.behavior_Late_SettingTime); // ###

                            $("[id*=LateSettingTime]").html("");
                            if ($("input[id$=late_setting_point]").prop("checked")) {
                                $("[id*=LateSettingTime]").html("");
                                $.each(result.lateSettingTimes, function (e, s) {
                                    $("[id*=LateSettingTime]").append(`<div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <div class="col-md-5">
                                <label class="col-md-1 col-form-label" name="timein">{0}</label>
                            </div>
                            <label class="col-md-1 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                            <div class="col-md-5">
                                <label class="col-md-1 col-form-label" name="timeout">{1}</label>
                            </div>
                            <label class="col-md-1 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %></label>
                        </div>
                        <div class="col-md-2">
                            <label class="col-md-1 col-form-label" name="cut_point">{2}</label>
                        </div>
                        <div class="col-md-1" style="padding-left: 0px; padding-right: 0px;" >
                            <div class="form-group">
                                <label class="col-form-label" style="font-size: 20px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></label>
                            </div>
                        </div>
                </div>`);
                                    let rowUpdate = $("[id*=LateSettingTime] div.row:last");
                                    rowUpdate.find("[name=timein]").html(s.TimeStatr);
                                    rowUpdate.find("[name=timeout]").html(s.TimeEnd);
                                    rowUpdate.find("[name=cut_point]").html(s.cut_point);
                                });
                            } 

                            $("#modalpopup-data").modal("hide");
                        },
                        function (result) {
                            alert(result._meassage);
                        }
                    );
                }

            });

            $("#behavior_score").attr('maxlength', '4');
            $("#behavior_late").attr('maxlength', '4');
            $("#behavior_absence").attr('maxlength', '4');
            $("#behavior_errand").attr('maxlength', '4'); // ###
            $("#behavior_sick").attr('maxlength', '4'); // ###
            $("#behavior_uncheckname").attr('maxlength', '4'); // ###
            $("#behavior_alert").attr('maxlength', '4'); // ###

        });

        var _rowIndex = 0;
        function addTimelate(BehaviorTimeSettingID) {
            let _td = $("[name=setting_point_0] td");
            $(_td[1]).append(`<div class="row" data-row="` + _rowIndex + `" data-BehaviorTimeSettingID="` + BehaviorTimeSettingID + `">
                        <div class="col-md-8">
                            <div class="col-md-5">
                                <input type="text" name="BehaviorTimeSettingID" value="` + BehaviorTimeSettingID + `" class="hide" />
                                <div class="input-group clockpicker addvalues" data-placement="left" data-align="top" data-autoclose="true">
                                    <input type="text" class="form-control mon clock-box" id="timein" name="timein" placeholder="00:00" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <label class="col-md-1 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                            <div class="col-md-5">
                                <div class="input-group clockpicker addvalues" data-placement="left" data-align="top" data-autoclose="true">
                                    <input type="text" class="form-control mon clock-box" id="timeout" name="timeout" placeholder="00:00" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <label class="col-md-1 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %></label>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <input type="text" name="cut_point" class="form-control" />
                            </div>
                        </div>
                        <div class="col-md-1" style="padding-left: 0px; padding-right: 0px;" onclick="removeTimelate(` + _rowIndex + `)">
                            <div class="form-group">
                                <label class="col-form-label" style="font-size: 20px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></label>
                                <span class="glyphicon glyphicon-trash" style="vertical-align: middle; font-size: 15px;"></span>
                            </div>
                        </div>
                </div>`);
            $('.clockpicker').clockpicker();
            _rowIndex += 1;
        }

        function removeTimelate(_Index) {
            $("[data-row=" + _Index + "]").remove()
        }

        function getdata() {

            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302014") %>");

            PageMethods.getdata(
                function (result) {
                    $("#modalpopup-data").css("top", "10%");
                    $("#modalpopup-data").find(".modal-dialog").css("width", "60%");
                    $("#modalpopup-data").find(".modal-content").css("width", "100%");
                    $("#modalpopup-data").find(".modal-content").css("padding", "15px");
                    $("#modalpopup-data").modal("show");
                    $("#behavior_score").val(result.behavior_score);
                    $("#behavior_type").val(result.behavior_type);
                    $("#behavior_late").val(result.behavior_late);
                    $("#behavior_absence").val(result.behavior_absence);
                    $("#behavior_errand").val(result.behavior_errand); // ###
                    $("#behavior_sick").val(result.behavior_sick); // ###
                    $("#behavior_uncheckname").val(result.behavior_uncheckname); // ###
                    $("#behavior_begin_late").val(result.behavior_begin_late);
                    $("#behavior_begin_absence").val(result.behavior_begin_absence);
                    $("#behavior_begin_errand").val(result.behavior_begin_errand); // ###
                    $("#behavior_begin_sick").val(result.behavior_begin_sick); // ###
                    $("#behavior_begin_uncheckname").val(result.behavior_begin_uncheckname); // ###
                    $("#behavior_time_start_cut_point").val(result.behavior_time_start_cut_point); // ###
                    $("#behavior_absence_half_point").prop('checked', (result.behavior_absence_half_point == "1")); // ###
                    $("#behavior_alert").val(result.behavior_show_alert);
                    $('#behavior_show_minus_sign').bootstrapToggle(result.behavior_show_minus_sign == "1" ? "on" : "off");
                    $("#behavior_Late_SettingTime").prop('checked', result.behavior_Late_SettingTime); // ###

                    if ($("#behavior_Late_SettingTime").prop("checked")) {
                        $("[name*=setting_point]").show();
                        $("[name=setting_point_0] td").html("");
                        $.each(result.lateSettingTimes, function (e, s) {
                            addTimelate(s.BehaviorTimeSettingID);
                            let rowUpdate = $("[name=setting_point_0] div.row:last");
                            rowUpdate.find("[name=timein]").val(s.TimeStatr);
                            rowUpdate.find("[name=timeout]").val(s.TimeEnd);
                            rowUpdate.find("[name=cut_point]").val(s.cut_point);
                            rowUpdate.find("[name=BehaviorTimeSettingID]").val(s.BehaviorTimeSettingID);
                        });
                    } else {
                        $("[name*=setting_point]").hide();
                    }
                },
                function (result) {
                    console.log(result);
                })
        }
    </script>
    <style type="text/css">
        .table > tbody > tr > td {
            border-top: none;
        }

        .table > tbody > tr.sub > td {
            padding: 0px 8px 4px 8px;
        }

        .table > tbody > tr.border {
            border-bottom: 1px solid #ddd;
        }

        .modal-content {
            /* Bootstrap sets the size of the modal in the modal-dialog class, we need to inherit it */
            width: inherit;
            height: inherit;
            /* To center horizontally */
            margin: 0 auto;
        }

        .modal-footer {
            border-top: none;
        }

        /* Toggle Switch Style */
        .switch {
            position: relative;
            display: inline-block;
            width: 60px;
            height: 34px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 26px;
                width: 26px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        .toggle.btn {
            width: 100px !important;
        }

        .toggle-on {
            padding: 0px;
        }

        .toggle-off {
            padding: 0px 10px 0px 0px;
        }

        #modalpopup-data-submit, #modalpopup-data-cancel {
            width: 90px;
        }

        .table tbody tr td input {
            text-align: center;
        }

        .table tbody tr td select {
            text-align-last: center;
        }

        .error {
            color: red;
            font-size: 20px;
        }

        #behavior_absence_half_point, #behavior_Late_SettingTime, #late_setting_point {
            /* Double-sized Checkboxes */
            -ms-transform: scale(1.3); /* IE */
            -moz-transform: scale(1.3); /* FF */
            -webkit-transform: scale(1.3); /* Safari and Chrome */
            -o-transform: scale(1.3); /* Opera */
            transform: scale(1.3);
            padding: 10px;
            cursor: pointer;
        }

        .behavior_absence_half_point input {
            /* Double-sized Checkboxes */
            -ms-transform: scale(1.3); /* IE */
            -moz-transform: scale(1.3); /* FF */
            -webkit-transform: scale(1.3); /* Safari and Chrome */
            -o-transform: scale(1.3); /* Opera */
            transform: scale(1.3);
            padding: 10px;
            cursor: pointer;
        }

        .checkbox-inline input[type="checkbox"] {
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="detail-card box-content companyedit-container" style="width: 100%;">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-md-12 center">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302014") %></label>
            </div>
        </div>
        <%--<div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2">
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4">
                <label>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302016") %></label>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4">
                <asp:Label ID="score" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2"></div>
            <div class="col-lg-4 col-md-4 col-sm-4">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302017") %></label>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4">
                <asp:Label ID="type" runat="server"></asp:Label>
            </div>
        </div>--%>

        <table class="table" style="border: 1px solid #ccc;">
            <thead class="bg-primary">
                <tr>
                    <th scope="col" style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                    <th scope="col" colspan="3" style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302015") %></th>
                </tr>
            </thead>
            <tbody>
                <tr class="border">
                    <td style="text-align: center; width: 10%;">1.</td>
                    <td style="width: 60%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302016") %></td>
                    <td style="width: 20%; text-align: center;">
                        <asp:Label ID="score" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: center; width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></td>
                </tr>
                <tr class="border">
                    <td style="text-align: center;">2.</td>
                    <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302017") %></td>
                    <td style="text-align: center;">
                        <asp:Label ID="type" runat="server"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: center;">3.</td>
                    <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302019") %></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr class="sub">
                    <td></td>
                    <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302020") %></td>
                    <td style="text-align: center;">
                        <asp:Label ID="late" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></td>
                </tr>
                <tr class="sub">
                    <td></td>
                    <td style="padding-left: 60px;">
                        <label class="checkbox-inline">
                            <asp:CheckBox ID="late_setting_point" runat="server" Enabled="false" CssClass="behavior_absence_half_point" />
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105013") %>
                            <span style="padding-left: 8px;"></span>
                        </label>
                    </td>
                    <td></td>
                    <td style="text-align: center;"></td>
                </tr>
                <tr class="sub">
                    <td></td>
                    <td colspan="3">
                        <asp:Label ID="LateSettingTime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="sub">
                    <td></td>
                    <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302023") %></td>
                    <td style="text-align: center;">
                        <asp:Label ID="absence" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></td>
                </tr>
                <tr class="sub">
                    <td></td>
                    <td style="padding-left: 60px;">
                        <label class="checkbox-inline">
                            <asp:CheckBox ID="absence_half_point" runat="server" Enabled="false" CssClass="behavior_absence_half_point" /><span style="padding-left: 8px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302024") %></span>
                        </label>
                    </td>
                    <td></td>
                    <td style="text-align: center;"></td>
                </tr>
                <%--###--%>
                <tr class="sub">
                    <td></td>
                    <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302025") %></td>
                    <td style="text-align: center;">
                        <asp:Label ID="errand" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></td>
                </tr>
                <tr class="sub">
                    <td></td>
                    <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302026") %></td>
                    <td style="text-align: center;">
                        <asp:Label ID="sick" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></td>
                </tr>
                <tr class="border sub">
                    <td></td>
                    <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302027") %></td>
                    <td style="text-align: center;">
                        <asp:Label ID="uncheckname" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></td>
                </tr>
                <tr>
                    <td style="text-align: center;">4.</td>
                    <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302028") %></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr class="sub">
                    <td></td>
                    <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302029") %></td>
                    <td style="text-align: center;">
                        <asp:Label ID="begin_late" runat="server"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr class="sub">
                    <td></td>
                    <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302030") %></td>
                    <td style="text-align: center;">
                        <asp:Label ID="begin_absence" runat="server"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <%--###--%>
                <tr class="sub">
                    <td></td>
                    <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302031") %></td>
                    <td style="text-align: center;">
                        <asp:Label ID="begin_errand" runat="server"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr class="sub">
                    <td></td>
                    <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302032") %></td>
                    <td style="text-align: center;">
                        <asp:Label ID="begin_sick" runat="server"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr class="sub">
                    <td></td>
                    <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302033") %></td>
                    <td style="text-align: center;">
                        <asp:Label ID="begin_uncheckname" runat="server"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr class="border">
                    <td></td>
                    <td colspan="3">หมายเหตุ : หากตั้งค่าเป็น  0 ระบบตัดคะแนนขาดอัติโนมัติจะไม่ทำงาน</td>
                </tr>
                <tr class="border">
                    <td style="text-align: center;">5.</td>
                    <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302035") %></td>
                    <td style="text-align: center;">
                        <asp:Label ID="time_start_cut_point" runat="server"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr class="border">
                    <td style="text-align: center;">6.</td>
                    <td>แสดงเครื่องหมายลบ (เช่น -30)</td>
                    <td style="text-align: center;">
                        <asp:Label ID="show_minus_sign" runat="server"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr class="border">
                    <td style="text-align: center;">7.</td>
                    <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302037") %></td>
                    <td style="text-align: center;">
                        <asp:Label ID="show_behavior_alert" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></td>
                </tr>
            </tbody>
        </table>

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 center">
                <a class="btn btn-info btnpermission" onclick="getdata()" style="cursor: pointer;">
                    <span class="glyphicon glyphicon-pencil" style="vertical-align: middle; padding: 5px;"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %></a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalpopup" runat="server">
    <table class="table">
        <thead class="bg-primary">
            <tr>
                <th scope="col" style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                <th scope="col" colspan="3" style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302015") %></th>
            </tr>
        </thead>
        <tbody>
            <tr class="border">
                <td style="text-align: center; width: 10%;">1.</td>
                <td style="width: 60%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302016") %></td>
                <td style="width: 20%;">
                    <input type="text" id="behavior_score" name="behavior_score" class="form-control" />
                </td>
                <td style="text-align: center; width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></td>
            </tr>
            <tr class="border">
                <td style="text-align: center;">2.</td>
                <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302017") %></td>
                <td>
                    <select id="behavior_type" class="form-control">
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405023") %></option>
                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105012") %></option>
                        <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302018") %></option>
                    </select>
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: center;">3.</td>
                <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302019") %></td>
                <td></td>
                <td></td>
            </tr>
            <tr class="sub">
                <td></td>
                <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302020") %></td>
                <td>
                    <input type="text" id="behavior_late" name="behavior_late" class="form-control" />
                </td>
                <td style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></td>
            </tr>
            <tr class="sub">
                <td></td>
                <td style="padding-left: 60px;">
                    <label class="checkbox-inline">
                        <input id="behavior_Late_SettingTime" class="behavior_absence_half_point" type="checkbox" />
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105013") %>
                            <span style="padding-left: 8px;"></span>
                    </label>
                </td>
                <td></td>
                <td style="text-align: center;"></td>
            </tr>
            <tr class="sub" name="setting_point_0">
                <td></td>
                <td colspan="3">
                    <%--<div class="row" style="padding-bottom: 12px;">
                        <div class="col-md-8">
                            <div class="col-md-5">
                                <div class="input-group clockpicker addvalues" data-placement="left" data-align="top" data-autoclose="true">
                                    <input type="text" class="form-control mon clock-box" id="timein" name="timein" placeholder="00:00" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <label class="col-md-1 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                            <div class="col-md-5">
                                <div class="input-group clockpicker addvalues" data-placement="left" data-align="top" data-autoclose="true">
                                    <input type="text" class="form-control mon clock-box" id="timeout" name="timeout" placeholder="00:00" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <label class="col-md-1 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %></label>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <input type="text" name="behavior_absence" class="form-control" />
                            </div>
                        </div>
                        <div class="col-md-1" style="padding-left: 0px; padding-right: 0px;">
                            <div class="form-group">
                                <label class="col-form-label" style="font-size: 20px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></label>
                                <span class="glyphicon glyphicon-trash" style="vertical-align: middle; font-size: 15px;"></span>
                            </div>
                        </div>
                    </div>                  
                </td>--%>
            </tr>
            <tr class="sub" name="setting_point_1">
                <td></td>
                <td style="padding-left: 60px;"></td>
                <td colspan="2">
                    <div class="btn btn-success" onclick="addTimelate(0)">
                        <span class="glyphicon glyphicon-plus" style="vertical-align: middle; padding: 5px;"></span>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302021") %>
                    </div>
                </td>
            </tr>
            <tr class="sub">
                <td></td>
                <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302023") %></td>
                <td>
                    <input type="text" id="behavior_absence" name="behavior_absence" class="form-control" />
                </td>
                <td style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></td>
            </tr>
            <%--###--%>
            <tr class="sub">
                <td></td>
                <td style="padding-left: 60px;">
                    <label class="checkbox-inline">
                        <input type="checkbox" id="behavior_absence_half_point" name="behavior_absence_half_point" style="position: relative;" /><span style="padding-left: 8px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302024") %></span>
                    </label>
                </td>
                <td></td>
                <td style="text-align: center;"></td>
            </tr>
            <tr class="sub">
                <td></td>
                <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302025") %></td>
                <td>
                    <input type="text" id="behavior_errand" name="behavior_errand" class="form-control" />
                </td>
                <td style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></td>
            </tr>
            <tr class="sub">
                <td></td>
                <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302026") %></td>
                <td>
                    <input type="text" id="behavior_sick" name="behavior_sick" class="form-control" />
                </td>
                <td style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></td>
            </tr>
            <tr class="border sub">
                <td></td>
                <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302027") %></td>
                <td>
                    <input type="text" id="behavior_uncheckname" name="behavior_uncheckname" class="form-control" />
                </td>
                <td style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></td>
            </tr>
            <tr>
                <td style="text-align: center;">4.</td>
                <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302028") %></td>
                <td></td>
                <td></td>
            </tr>
            <tr class="sub">
                <td></td>
                <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302029") %></td>
                <td>
                    <select id="behavior_begin_late" class="form-control">
                        <option value="0">0</option>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                    </select>
                </td>
                <td></td>
            </tr>
            <tr class="sub">
                <td></td>
                <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302030") %></td>
                <td>
                    <select id="behavior_begin_absence" class="form-control">
                        <option value="0">0</option>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                    </select>
                </td>
                <td></td>
            </tr>
            <%--###--%>
            <tr class="sub">
                <td></td>
                <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302031") %></td>
                <td>
                    <select id="behavior_begin_errand" class="form-control">
                        <option value="0">0</option>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                    </select>
                </td>
                <td></td>
            </tr>
            <tr class="sub">
                <td></td>
                <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302032") %></td>
                <td>
                    <select id="behavior_begin_sick" class="form-control">
                        <option value="0">0</option>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                    </select>
                </td>
                <td></td>
            </tr>
            <tr class="sub">
                <td></td>
                <td style="padding-left: 40px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302033") %></td>
                <td>
                    <select id="behavior_begin_uncheckname" class="form-control">
                        <option value="0">0</option>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                    </select>
                </td>
                <td></td>
            </tr>
            <tr class="border">
                <td></td>
                <td colspan="3">หมายเหตุ : หากตั้งค่าเป็น  0 ระบบตัดคะแนนขาดอัติโนมัติจะไม่ทำงาน</td>
            </tr>
            <tr class="border">
                <td style="text-align: center;">5.</td>
                <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302035") %></td>
                <td>
                    <select id="behavior_time_start_cut_point" class="form-control">
                        <%--<option value="11:30">11:30</option>--%>
                        <option value="12:30">12:30</option>
                        <option value="13:30">13:30</option>
                        <option value="14:30">14:30</option>
                        <option value="15:30">15:30</option>
                        <option value="16:30">16:30</option>
                        <option value="17:30">17:30</option>
                        <option value="18:30">18:30</option>
                        <option value="19:30">19:30</option>
                        <option value="20:30">20:30</option>
                        <option value="21:30">21:30</option>
                        <option value="23:30">23:30</option>
                        <%--<option value="00:30">00:30</option>--%>
                    </select>
                </td>
                <td></td>
            </tr>
            <tr class="border">
                <td style="text-align: center;">6.</td>
                <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302036") %></td>
                <td style="text-align: center;">
                    <%--<label class="switch">
                        <input id="behavior_show_minus_sign" type="checkbox" checked  />
                        <span class="slider round"></span>
                    </label>--%>
                    <div class="checkbox">
                        <label>
                            <input id="behavior_show_minus_sign" type="checkbox" checked data-on="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %>" data-off="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %>" />
                            <%--<span class="slider round"></span>--%>
                        </label>
                    </div>
                </td>
                <td></td>
            </tr>
            <tr class="border">
                <td style="text-align: center;">7.</td>
                <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302037") %></td>
                <td style="text-align: center;">
                    <input type="text" id="behavior_alert" name="behavior_alert" class="form-control" />
                </td>
                <td></td>
            </tr>
        </tbody>
    </table>

    <%--<div class="row planadd-row">
        <div class="col-xs-12">
            <div class="col-xs-6">
                <label>1. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132040") %></label>
            </div>
            <div class="col-xs-4">
                <input type="text" id="behavior_score" class="form-control" />
            </div>

        </div>
    </div>
    <div class="row" id="row-name">
        <div class="col-xs-12">
            <div class="col-xs-6">
                <label>2. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132039") %></label>
            </div>
            <div class="col-xs-4">
                <select id="behavior_type" class="form-control">
                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405023") %></option>
                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105012") %></option>
                </select>
            </div>
        </div>
    </div>--%>
</asp:Content>
