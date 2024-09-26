<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpAdvancement.aspx.cs" Inherits="FingerprintPayment.Employees.EmpAdvancement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        #iptSalary-error, #iptPositionMoney-error, #iptAcademicStandingMoney-error {
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="empAdvancementForm" style="padding: 0px;">
                <form id="empAdvancementForm" class="form-padding" style="padding: 0px;">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102093") %></p>
                    <div class="row div-row-padding" style="margin-top: 14px;">
                    </div>
                    <div class="row div-row-padding">
                        <label class="col-md-2 mb-2 col-form-label text-right"></label>
                        <div class="col-md-3 mb-3"></div>
                        <label class="col-md-3 mb-3 col-form-label text-right"></label>
                        <label class="col-md-3 mb-3 col-form-label text-left"><span style="color: red;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102094") %></span></label>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="sltWorkStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105047") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltWorkStatus" name="sltWorkStatus" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121001") %>" data-can-resign="<%=CanResign%>">
                                <option selected="selected" value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102096") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101032") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121003") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121004") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptDegree"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptDegree" name="iptDegree"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %>" maxlength="100" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div id="divEmployeeDayQuit" class="row div-row-padding" style="display: none;">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptDayQuit" id="spnDayQuit"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptDayQuit" name="iptDayQuit" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <label class="col-md-3 mb-3 col-form-label text-right"><span></span></label>
                        <div class="col-md-3 mb-3"></div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptOfficialInEducationDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105048") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptOfficialInEducationDate" name="iptOfficialInEducationDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptPackingDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105049") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptPackingDate" name="iptPackingDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptSalary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105050") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <div class="input-group">
                                <input type="text" class="form-control text-left sum-to-netsalary"
                                    id="iptSalary" name="iptSalary" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105050") %>" maxlength="6" />
                                <div class="input-group-addon">
                                    <span class="input-group-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptOfficialStartDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105051") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptOfficialStartDate" name="iptOfficialStartDate" type="text" class="form-control datepicker" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105051") %>" maxlength="10" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptPositionMoney"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105052") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <div class="input-group">
                                <input type="text" class="form-control text-left sum-to-netsalary"
                                    id="iptPositionMoney" name="iptPositionMoney" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105052") %>" maxlength="6" />
                                <div class="input-group-addon">
                                    <span class="input-group-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptAcademicStandingMoney"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105053") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <div class="input-group">
                                <input type="text" class="form-control text-left sum-to-netsalary"
                                    id="iptAcademicStandingMoney" name="iptAcademicStandingMoney"
                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105053") %>" maxlength="6" />
                                <div class="input-group-addon">
                                    <span class="input-group-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptRetirementDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105054") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptRetirementDate" name="iptRetirementDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptNetSalary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105055") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <span id="spnCalculate" class="input-group-text btn" style="background-color: #999; border-color: #aaa;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102109") %></span>
                                </span>
                                <input type="text" class="form-control text-left"
                                    id="iptNetSalary" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105055") %>" disabled style="padding-left: 7px;" />
                                <div class="input-group-addon">
                                    <span class="input-group-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptRemainDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105056") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <div class='input-group date' id='divRemainDate'>
                                <input type='text' class="form-control"
                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105056") %>" disabled />
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 text-right">
                            <%--000--%>
                        </div>
                        <div class="col-md-3 mb-3">
                            <%--000--%>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>

                    <div class="row text-center" style="margin-bottom: 7px;">
                        <button id="save" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="cancel" type="button"
                            class="btn btn-danger" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var empAdvancementForm = {
                    GetItem: function (empID, id) {
                        $.ajax({
                            type: "POST",
                            url: "EmpAdvancement.aspx/GetItem",
                            data: '{empID: ' + empID + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empAdvancementForm.OnSuccessGet,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessGet: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {

                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                $(".empAdvancementForm #sltWorkStatus").selectpicker('val', $(this).find("F1").text());
                                $(".empAdvancementForm #iptDegree").val($(this).find("F2").text());
                                $(".empAdvancementForm #iptOfficialInEducationDate").val($(this).find("F3").text());
                                $(".empAdvancementForm #iptPackingDate").val($(this).find("F4").text());
                                $(".empAdvancementForm #iptSalary").val($(this).find("F5").text());
                                $(".empAdvancementForm #iptOfficialStartDate").val($(this).find("F6").text());
                                $(".empAdvancementForm #iptPositionMoney").val($(this).find("F7").text());
                                $(".empAdvancementForm #iptAcademicStandingMoney").val($(this).find("F8").text());
                                $(".empAdvancementForm #iptRetirementDate").val($(this).find("F9").text());
                                $(".empAdvancementForm #iptNetSalary").val($(this).find("F10").text());
                                //$(".empAdvancementForm #divRemainDate > :input").val($(this).find("F11").text());

                                empAdvancementForm.RemainRetirement.years = Number($(this).find("F12").text());
                                empAdvancementForm.RemainRetirement.months = Number($(this).find("F13").text());
                                empAdvancementForm.RemainRetirement.days = Number($(this).find("F14").text());
                                $(".empAdvancementForm #divRemainDate > :input").val(empAdvancementForm.RemainRetirement.years + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>   ' + empAdvancementForm.RemainRetirement.months + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>   ' + empAdvancementForm.RemainRetirement.days + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>');

                                $(".empAdvancementForm #iptDayQuit").val($(this).find("F15").text());

                                $("#sltWorkStatus").attr("data-dayquit", $(this).find("F15").text());

                                $("#sltWorkStatus").change();
                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "EmpAdvancement.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empAdvancementForm.OnSuccessSave,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSave: function (response) {
                        var title = "";
                        var body = "";

                        switch (response.d.split('-').length) {
                            case 1:

                                switch (response.d) {
                                    case "complete":
                                        title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                        $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').on('click', function () {

                                        });

                                        break;
                                    case "error":
                                        title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %>';

                                        break;
                                    default: break;
                                }

                                break;
                            case 2:

                                var flag = response.d.split('-');
                                switch (flag[0]) {
                                    case "complete":
                                        title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                        $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').on('click', function () {

                                            // Redirect to employee list : if page name = EmployeeNew.aspx
                                            //if (comFunction.getFilenamePathFromUrl().toLowerCase() == "employeenew.aspx") {

                                            //    window.location.replace("EmployeeDetail.aspx?v=view&eid=" + flag[1]);
                                            //}
                                        });

                                        break;
                                    case "warning":
                                        title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111026") %> [{0}]'.format(flag[1]);

                                        break;
                                    default: break;

                                }

                                break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    getRemainGovernment: function (date, month, year) {
                        var now = new Date();
                        var today = new Date(now.getFullYear(), now.getMonth(), now.getDate());
                        var target = new Date(year, month, date);

                        var age = {};

                        var diff = Math.floor(target.getTime() - today.getTime());
                        var secs = Math.floor(diff / 1000);
                        var mins = Math.floor(secs / 60);
                        var hours = Math.floor(mins / 60);
                        var days = Math.floor(hours / 24);
                        var months = Math.floor(days / 31);
                        var years = Math.floor(months / 12);
                        months = Math.floor(months % 12);
                        days = Math.floor(days % 31);
                        hours = Math.floor(hours % 24);
                        mins = Math.floor(mins % 60);
                        secs = Math.floor(secs % 60);
                        var message = "";
                        if (days <= 0) {
                            message += secs + " sec ";
                            message += mins + " min ";
                            message += hours + " hours ";
                        } else {
                            message += days + " days ";
                            if (months > 0 || years > 0) {
                                message += months + " months ";
                            }
                            if (years > 0) {
                                message += years + " years ago";
                            }
                        }

                        age = {
                            years: years,
                            months: months,
                            days: days
                        };

                        return age;
                    },
                    RemainRetirement: {}
                }

                $(document).ready(function () {

                    $("#empAdvancementForm").validate({
                        rules: {
                            iptOfficialInEducationDate: {
                                required: false,
                                thaiDate: true
                            },
                            iptPackingDate: {
                                required: false,
                                thaiDate: true
                            },
                            iptSalary: {
                                required: false,
                                number: true
                            },
                            iptOfficialStartDate: {
                                required: false,
                                thaiDate: true
                            },
                            iptPositionMoney: {
                                number: true
                            },
                            iptAcademicStandingMoney: {
                                number: true
                            },
                            iptRetirementDate: {
                                required: false,
                                thaiDate: true
                            },
                            iptDayQuit: {
                                required: function (element) {
                                    return $("#sltWorkStatus").val() != '1' && !$(element).val();
                                },
                                thaiDate: true
                            }
                        },
                        messages: {
                            iptOfficialInEducationDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptPackingDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptSalary: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            },
                            iptOfficialStartDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptPositionMoney: {
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            },
                            iptAcademicStandingMoney: {
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            },
                            iptRetirementDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptDayQuit: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            }
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptPackingDate":
                                case "iptOfficialStartDate":
                                case "iptDayQuit": error.insertAfter(element); break;
                                case "iptSalary":
                                case "iptPositionMoney":
                                case "iptAcademicStandingMoney": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    // Datatable Section

                    // Form Section
                    $(".empAdvancementForm #save").bind({
                        click: function () {

                            if ($('#empAdvancementForm').valid()) {

                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off('click');
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = new Array();
                                    data[0] = xEmpKey.eid + "-" + xEmpKey.pid;
                                    data[1] = $(".empAdvancementForm #sltWorkStatus").val(); //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105047") %>
                                    data[2] = $(".empAdvancementForm #iptDegree").val(); //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %>
                                    data[3] = $(".empAdvancementForm #iptOfficialInEducationDate").val(); //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>ฏิบัติงานในสถานศึกษา
                                    data[4] = $(".empAdvancementForm #iptPackingDate").val(); //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105049") %>
                                    data[5] = $(".empAdvancementForm #iptSalary").val(); //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105050") %>
                                    data[6] = $(".empAdvancementForm #iptOfficialStartDate").val(); //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105051") %>
                                    data[7] = $(".empAdvancementForm #iptPositionMoney").val(); //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105052") %>
                                    data[8] = $(".empAdvancementForm #iptAcademicStandingMoney").val(); //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105053") %>
                                    data[9] = $(".empAdvancementForm #iptRetirementDate").val(); //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105054") %>
                                    data[10] = $(".empAdvancementForm #iptNetSalary").val(); //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105055") %>
                                    data[11] = $(".empAdvancementForm #divRemainDate > :input").val(); //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105056") %>
                                    //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105056") %>
                                    data[12] = empAdvancementForm.RemainRetirement.years ? empAdvancementForm.RemainRetirement.years : "";
                                    data[13] = empAdvancementForm.RemainRetirement.months ? empAdvancementForm.RemainRetirement.months : "";
                                    data[14] = empAdvancementForm.RemainRetirement.days ? empAdvancementForm.RemainRetirement.days : "";

                                    data[15] = $(".empAdvancementForm #iptDayQuit").val(); //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102097") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102098") %>

                                    empAdvancementForm.SaveItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".empAdvancementForm #cancel").bind({
                        click: function () {

                            // Redirect to employee list
                            //empInfoForm.ClearSession(function () {
                            //    window.location.replace("EmployeeList.aspx");
                            //});

                            return false;
                        }
                    });

                    // Modal Section
                    $('#modalNotifyConfirmSave').on('show.bs.modal', function (e) {
                        $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                        $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
                    });

                    var previousWorkStatus;
                    $("#sltWorkStatus").on('shown.bs.select', function (e) {
                        previousWorkStatus = $(this).val();
                    }).change(function () {

                        // [P-1] Check value 2 - <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101032") %>
                        if ($(this).val() == "2" && (/false/).test($(this).data('can-resign'))) {

                            $(this).selectpicker('val', previousWorkStatus);

                            Swal.fire({
                                title: 'Error!',
                                text: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121002") %>',
                                type: 'error',
                                confirmButtonClass: "btn btn-error",
                                buttonsStyling: false
                            });
                        }

                        if ($(this).val() != "1") {

                            $("#spnDayQuit").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" + $("#sltWorkStatus option:selected").text() + " :");
                            $("#iptDayQuit").attr("placeholder", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" + $("#sltWorkStatus option:selected").text());

                            $(".empAdvancementForm #iptDayQuit").val($("#sltWorkStatus").attr("select-dayquit") ?? ($("#sltWorkStatus").attr("data-dayquit") ?? ""));

                            $("#iptDayQuit").prop('disabled', false);
                            $("#divEmployeeDayQuit").show();
                        } else {

                            $("#spnDayQuit").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :");
                            $("#iptDayQuit").attr("placeholder", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>");

                            $('#iptDayQuit').data("DateTimePicker").clear();

                            $("#iptDayQuit").prop('disabled', true);
                            $("#divEmployeeDayQuit").hide();
                        }

                    });

                    // Initial data

                    //$('.empAdvancementForm #divOfficialInEducationDate, .empAdvancementForm #divPackingDate, .empAdvancementForm #divOfficialStartDate').datetimepicker({
                    //    format: 'DD/MM/YYYY-BE',
                    //    locale: 'th'
                    //});
                    $('.empAdvancementForm #iptOfficialInEducationDate, .empAdvancementForm #iptPackingDate, .empAdvancementForm #iptOfficialStartDate').datetimepicker({
                        keepOpen: false,
                        debug: false,
                        format: 'DD/MM/YYYY-BE',
                        locale: 'th',
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

                    $('.empAdvancementForm #iptDayQuit').datetimepicker({
                        keepOpen: false,
                        debug: false,
                        format: 'DD/MM/YYYY-BE',
                        locale: 'th',
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
                    }).on('dp.change', function (e) {
                        if ($(".empAdvancementForm #iptDayQuit").val()) {
                            $("#sltWorkStatus").attr("select-dayquit", $(".empAdvancementForm #iptDayQuit").val());
                            //console.log("--" + $(".empAdvancementForm #iptDayQuit").val());
                        }
                    });

                    $('.empAdvancementForm #iptRetirementDate').datetimepicker({
                        keepOpen: false,
                        debug: false,
                        format: 'DD/MM/YYYY-BE',
                        locale: 'th',
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
                    }).on('dp.change', function (e) {
                        empAdvancementForm.RemainRetirement = empAdvancementForm.getRemainGovernment(e.date._d.getDate(), e.date._d.getMonth(), e.date._d.getFullYear());
                        //$('.empAdvancementForm #iptRemainGovernmentYear').val(remain.years);
                        //$('.empAdvancementForm #iptRemainGovernmentMonth').val(remain.months);
                        //$('.empAdvancementForm #iptRemainGovernmentDay').val(remain.days);
                        $(".empAdvancementForm #divRemainDate > :input").val(empAdvancementForm.RemainRetirement.years + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>   ' + empAdvancementForm.RemainRetirement.months + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>   ' + empAdvancementForm.RemainRetirement.days + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>');
                    });

                    $(".empAdvancementForm .datepicker").attr('maxlength', '10');

                    $('.empAdvancementForm #spnCalculate').click(function () {
                        var salary = parseInt($('.empAdvancementForm #iptSalary').val() == '' ? 0 : $('.empAdvancementForm #iptSalary').val());
                        var asMoney = parseInt($('.empAdvancementForm #iptAcademicStandingMoney').val() == '' ? 0 : $('.empAdvancementForm #iptAcademicStandingMoney').val());
                        var pMoney = parseInt($('.empAdvancementForm #iptPositionMoney').val() == '' ? 0 : $('.empAdvancementForm #iptPositionMoney').val());
                        $('.empAdvancementForm #iptNetSalary').val(salary + asMoney + pMoney);
                    });

                    $('.sum-to-netsalary').keyup(function () {
                        var sum = 0;
                        $('.sum-to-netsalary').each(function () {
                            sum += Number($(this).val());
                        });

                        // here, you have your sum
                        $('.empAdvancementForm #iptNetSalary').val(sum);
                    });

                    activateBootstrapSelect('.empAdvancementForm .selectpicker');

                    // Load info command
                    xEmpKey.pid = "1";
                    empAdvancementForm.GetItem(<%=Request.QueryString["eid"]%>, 1);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            View Content
        </asp:View>
    </asp:MultiView>
</body>
</html>
