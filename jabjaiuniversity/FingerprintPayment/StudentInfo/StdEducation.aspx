<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StdEducation.aspx.cs" Inherits="FingerprintPayment.StudentInfo.StdEducation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            List Content
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="stdEducationForm">
                <form id="stdEducationForm" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101171") %></p>
                    <div class="row div-row-padding" style="margin-top: 30px;">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptOldSchoolName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101172") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptOldSchoolName" name="iptOldSchoolName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101172") %>" maxlength="250" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltOldSchoolProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltOldSchoolProvince" name="sltOldSchoolProvince" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>
                                <asp:Literal ID="ltrOldSchoolProvince" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltOldSchoolAmphoe"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltOldSchoolAmphoe" name="sltOldSchoolAmphoe" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltOldSchoolTombon"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltOldSchoolTombon" name="sltOldSchoolTombon" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptOldSchoolGPA"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101173") %>:</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptOldSchoolGPA" name="iptOldSchoolGPA"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101174") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptCredit"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101175") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptCredit" name="iptCredit"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101175") %>" maxlength="7" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptOldSchoolDateGraduated"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101176") %> :</label></div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptOldSchoolDateGraduated" name="iptOldSchoolDateGraduated" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptOldSchoolGraduated"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptOldSchoolGraduated" name="iptOldSchoolGraduated"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %>" maxlength="250" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptMoveOutReason"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101178") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMoveOutReason" name="iptMoveOutReason"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101178") %>" maxlength="200" />
                        </div>
                        <label class="col-md-3 mb-3 col-form-label text-right" for=""><span></span></label>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center">
                        <button id="saveEducation" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="saveEducationAndNext" type="submit" class="btn btn-info save-next"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101126") %></button>
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var stdEducationForm = {
                    GetItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdEducation.aspx/GetItem",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdEducationForm.OnSuccessGet,
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

                                $("#iptOldSchoolName").val($(this).find("F1").text());
                                $("#sltOldSchoolProvince").selectpicker('val', $(this).find("F2").text());
                                LoadDistrict('#sltOldSchoolAmphoe', $("#sltOldSchoolProvince").val());

                                $("#sltOldSchoolAmphoe").selectpicker('val', $(this).find("F3").text());
                                LoadSubDistrict('#sltOldSchoolTombon', $("#sltOldSchoolAmphoe").val());

                                $("#sltOldSchoolTombon").selectpicker('val', $(this).find("F4").text());
                                $("#iptOldSchoolGPA").val($(this).find("F5").text());
                                $("#iptOldSchoolGraduated").val($(this).find("F6").text());
                                $("#iptMoveOutReason").val($(this).find("F7").text());
                                $("#iptCredit").val($(this).find("F8").text());
                                $("#iptOldSchoolDateGraduated").val($(this).find("F9").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdEducation.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdEducationForm.OnSuccessSave,
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

                        switch (response.d) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

                                });

                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111026") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %>';

                                break;
                            default: break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    SaveAndNextItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdEducation.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdEducationForm.OnSuccessSaveAndNext,
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
                    OnSuccessSaveAndNext: function (response) {
                        var title = "";
                        var body = "";

                        switch (response.d) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

                                    // Redirect to student list
                                    window.location.replace("StudentDetail.aspx?v=form&sid=" + StudentDetail_NextSID + "&tid=" + xStdKey.tid);

                                });

                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111026") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %>';

                                break;
                            default: break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ClearSession: function (callbackRedirect) {
                        $.ajax({
                            type: "POST",
                            url: "StdEducation.aspx/ClearSessionID",
                            data: '{}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                callbackRedirect();
                            },
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    }
                }

                $(document).ready(function () {

                    $("#stdEducationForm").validate({
                        rules: {
                            iptOldSchoolName: "required",
                            //sltOldSchoolProvince: "required",
                            //sltOldSchoolAmphoe: "required",
                            //sltOldSchoolTombon: "required",
                            iptOldSchoolGPA: {
                                required: true,
                                number: false
                            },
                            iptCredit: {
                                required: false,
                                number: true
                            },
                            iptOldSchoolGraduated: "required",
                            iptMoveOutReason: "required"
                        },
                        messages: {
                            iptOldSchoolName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            //sltOldSchoolProvince: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            //sltOldSchoolAmphoe: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            //sltOldSchoolTombon: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptOldSchoolGPA: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            },
                            iptCredit: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            },
                            iptOldSchoolGraduated: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptMoveOutReason: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptOldSchoolName":
                                case "iptOldSchoolGPA": 
                                case "iptOldSchoolGraduated":
                                case "iptMoveOutReason": error.insertAfter(element); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    function GetEducationDataFromInput() {

                        var educationData = new Array();

                        educationData[0] = xStdKey.sid;
                        educationData[1] = $("#iptOldSchoolName").val();
                        educationData[2] = $("#sltOldSchoolProvince").val();
                        educationData[3] = $("#sltOldSchoolAmphoe").val();
                        educationData[4] = $("#sltOldSchoolTombon").val();
                        educationData[5] = $("#iptOldSchoolGPA").val();
                        educationData[6] = $("#iptOldSchoolGraduated").val();
                        educationData[7] = $("#iptMoveOutReason").val();
                        educationData[8] = $("#iptCredit").val();
                        educationData[9] = $("#iptOldSchoolDateGraduated").val();

                        return educationData;
                    }

                    $(".stdEducationForm #saveEducation").bind({
                        click: function () {

                            if ($('#stdEducationForm').valid()) {

                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetEducationDataFromInput();

                                    stdEducationForm.SaveItem(data);
                                });
                            }

                            return false;
                        }
                    });

                    $(".stdEducationForm #saveEducationAndNext").bind({
                        click: function () {

                            if ($('#stdEducationForm').valid()) {

                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetEducationDataFromInput();

                                    stdEducationForm.SaveAndNextItem(data);
                                });
                            }

                            return false;
                        }
                    });

                    $(".stdEducationForm .btn-cancel").bind({
                        click: function () {

                            // Redirect to employee list
                            stdEducationForm.ClearSession(function () {
                                window.location.replace("StudentList.aspx");
                            });

                            return false;
                        }
                    });

                    $("#sltOldSchoolProvince").change(function () {

                        LoadDistrict('#sltOldSchoolAmphoe', $("#sltOldSchoolProvince").val());

                    });
                    $("#sltOldSchoolAmphoe").change(function () {

                        LoadSubDistrict('#sltOldSchoolTombon', $("#sltOldSchoolAmphoe").val());

                    });

                    // Modal Section
                    $('#modalNotifyConfirmSave').off().on('show.bs.modal', function (e) {
                        $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                        $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
                    });

                    // Initial data

                    activateBootstrapSelect('.stdEducationForm .selectpicker');

                    //$('#iptOldSchoolGPA, #iptCredit').number(true, 2);
                    $('#iptCredit').number(true, 2);

                    //$('#divOldSchoolDateGraduated').datetimepicker({
                    //    format: 'DD/MM/YYYY-BE',
                    //    locale: 'th'
                    //});
                    $('.stdEducationForm .datepicker').datetimepicker({
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


                    // Load info command
                    stdEducationForm.GetItem(<%=Request.QueryString["sid"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            <div class="stdEducationView view-form">
                <form id="stdEducationView" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101171") %></p>
                    <div class="row div-row-padding" style="margin-top: 30px;">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101172") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spOldSchoolName"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spOldSchoolProvince"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spOldSchoolAmphoe"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spOldSchoolTombon"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101173") %>:</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spOldSchoolGPA"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101175") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spCredit"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <label class="col-md-2 mb-2 col-form-label text-right"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101176") %> :</span></label>
                        <div class="col-md-3 mb-3">
                            <span class="span-data" id="spOldSchoolDateGraduated"></span>
                        </div>
                        <label class="col-md-3 mb-3 col-form-label text-right"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> :</span></label>
                        <div class="col-md-3 mb-3">
                            <span class="span-data" id="spOldSchoolGraduated"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <label class="col-md-2 mb-2 col-form-label text-right"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101178") %> :</span></label>
                        <div class="col-md-3 mb-3">
                            <span class="span-data" id="spMoveOutReason"></span>
                        </div>
                        <label class="col-md-3 mb-3 col-form-label text-right"><span></span></label>
                        <div class="col-md-3 mb-3"></div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center">
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var stdEducationView = {
                    GetItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdEducation.aspx/GetItemView",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdEducationView.OnSuccessGet,
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

                                $("#spOldSchoolName").text($(this).find("F1").text());
                                $("#spOldSchoolProvince").text($(this).find("F2").text());
                                $("#spOldSchoolAmphoe").text($(this).find("F3").text());
                                $("#spOldSchoolTombon").text($(this).find("F4").text());
                                $("#spOldSchoolGPA").text($(this).find("F5").text());
                                $("#spOldSchoolGraduated").text($(this).find("F6").text());
                                $("#spMoveOutReason").text($(this).find("F7").text());
                                $("#spCredit").text($(this).find("F8").text());
                                $("#spOldSchoolDateGraduated").text($(this).find("F9").text());

                            });

                        }
                    }
                }

                $(document).ready(function () {

                    $(".stdEducationView .btn-cancel").bind({
                        click: function () {

                            window.close();

                            return false;
                        }
                    });

                    // Load info command
                    stdEducationView.GetItem(<%=Request.QueryString["sid"]%>);

                });

            </script>
        </asp:View>
    </asp:MultiView></body>
</html>
