<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterOnline03.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnline03" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStyle" runat="server">
    <style>
        .card .card-body .col-form-label, .card .card-body .label-on-right {
            padding: 12px 5px 0 0;
        }

        .card .card-body .form-group {
            margin: 0px 0 0;
        }

        .col-form-label span {
            vertical-align: -webkit-baseline-middle;
            font-size: 1em;
            color: #000;
        }

        .filter-option-inner-inner {
            font-size: 16px;
        }

        .bootstrap-select .dropdown-toggle .filter-option-inner-inner {
            overflow: initial;
        }

        .bootstrap-select > .dropdown-toggle.bs-placeholder.btn-primary {
            color: #fff;
            font-size: 1em;
        }

        .form-control:invalid {
            background-image: linear-gradient(to top, #f44336 2px, rgba(244, 67, 54, 0) 2px), linear-gradient(to top, #d2d2d2 0px, rgba(210, 210, 210, 0) 0px);
        }

        /* Container Bag */
        .div-bag {
            /*width: fit-content;
            float: right;
            text-align: left;*/
        }

            .div-bag.div-bag-select {
                margin-bottom: -25px;
                color: #707070;
            }

            .div-bag.div-bag-select2 {
                margin-bottom: -13px;
                color: #707070;
            }

            .div-bag.div-bag-input {
                /*margin-bottom: -10px;*/
                color: #707070;
                /*float: right !important;*/
                text-align: right !important;
            }

        .table.table-borderless > tbody > tr > td {
            padding: 0px;
            vertical-align: middle;
            border: none;
        }

        .backup-plans-table .bootstrap-select > .dropdown-toggle {
            padding-right: 10px;
        }

        .backup-plans-table .bootstrap-select .dropdown-toggle .filter-option {
            padding: 7px 0px 0px 7px;
            /*font-weight: bold;*/
        }

        .backup-plans-table .bootstrap-select .dropdown-menu li.disabled a {
            box-shadow: none;
        }

        .backup-plans-table li.disabled {
            pointer-events: unset;
        }

        .backup-plans-table .bootstrap-select .btn.dropdown-toggle.select-with-transition {
            color: #3c4858;
        }

        .backup-plans-table .bootstrap-select > .dropdown-toggle.bs-placeholder, .backup-plans-table .bootstrap-select > .dropdown-toggle.bs-placeholder:hover {
            color: #bbb !important;
        }

        .backup-plans.hide {
            display: none;
        }

        @media (min-width: 320px) and (max-width: 767px) {

            .div-bag.div-bag-input {
                /*margin-bottom: -10px;*/
                color: #707070;
                /*float: left !important;*/
                text-align: left !important;
            }

            .col-form-label {
                text-align: left !important;
                margin-left: 10px;
            }

            .label-on-right {
                text-align: right !important;
            }
        }

        li.disabled {
            pointer-events: all;
        }

            li.disabled a {
                pointer-events: none;
                cursor: not-allowed;
            }

        .dropdown-menu span.text {
            white-space: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <form id="form">
                <div class="card">
                    <div class="card-header card-header-rose card-header-icon">
                        <div class="card-icon text-center" style="border-radius: 12px; margin-left: 30px; margin-top: -30px;">
                            <h4 style="font-family: THSarabunNew; margin-bottom: 0px; padding: 3px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01915") %></h4>
                            <p class="h6 text-center">(Choose Program)</p>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <br>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> : </span></label>
                                    <p class="h6">(Academic Year)</p>
                                </div>
                            </div>
                            <div class="col-md-5 mr-auto">
                                <div class="form-group">
                                    <select id="sltYear" name="sltYear" class="selectpicker" data-style="btn btn-primary btn-round" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203072") %>">
                                        <asp:Literal ID="ltrYear" runat="server" />
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103201") %> : </span></label>
                                    <p class="h6">(Type)</p>
                                </div>
                            </div>
                            <div class="col-md-5 mr-auto">
                                <div class="form-group">
                                    <select id="sltStudentType" name="sltStudentType" class="selectpicker" data-style="btn btn-primary btn-round" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132828") %>">
                                        <asp:Literal ID="ltrStudentType" runat="server" />
                                    </select>
                                </div>
                            </div>
                            <label class="col-md-3 label-on-right">
                                <i class="material-icons">help_outline
                                </i>
                            </label>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> : </span></label>
                                    <p class="h6">(Level)</p>
                                </div>
                            </div>
                            <div class="col-md-5 mr-auto">
                                <div class="form-group">
                                    <select id="sltClass" name="sltClass" class="selectpicker" data-style="btn btn-primary btn-round" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111085") %>">
                                        <asp:Literal ID="ltrClass" runat="server" />
                                    </select>
                                </div>
                            </div>
                        </div>
                        <%--/* <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132829") %>--%>
                        <div class="row option-time" style="display: none;">
                            <label class="col-md-4 col-form-label text-right"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132766") %> : </span></label>
                            <div class="col-md-5 mr-auto">
                                <div class="form-group">
                                    <select id="sltOptionTime" name="sltOptionTime" class="selectpicker" data-style="btn btn-primary btn-round" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132830") %>">
                                        <option selected="selected" value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132767") %></option>
                                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132768") %></option>
                                        <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132769") %></option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>--%>
                        <div class="row option-branch1" style="display: none;">
                            <label class="col-md-4 col-form-label text-right"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02951") %> : </span></label>
                            <div class="col-md-5 mr-auto">
                                <div class="form-group">
                                    <select id="sltOptionBranch1" name="sltOptionBranch1" class="selectpicker" data-style="btn btn-primary btn-round" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132838") %>">
                                        <asp:Literal ID="ltrOptionBranch1" runat="server" />
                                    </select>
                                </div>
                            </div>
                        </div>
                        <%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>--%>
                        <div class="row option-branch2" style="display: none;">
                            <label class="col-md-4 col-form-label text-right"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02951") %> : </span></label>
                            <div class="col-md-5 mr-auto">
                                <div class="form-group">
                                    <select id="sltOptionBranch2" name="sltOptionBranch2" class="selectpicker" data-style="btn btn-primary btn-round" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132838") %>">
                                        <asp:Literal ID="ltrOptionBranch2" runat="server" />
                                    </select>
                                </div>
                            </div>
                        </div>
                        <%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132829") %> */--%>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="div-bag div-bag-input">
                                    <label class="col-form-label" style="padding: 0px; margin: 0px;"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103190") %> : </span></label>
                                    <p class="h6">(Program)</p>
                                </div>
                            </div>
                            <div class="col-md-5 mr-auto">
                                <div class="form-group">
                                    <select id="sltEduProgram" name="sltEduProgram" class="selectpicker" data-style="btn btn-primary btn-round" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132831") %>">
                                        <asp:Literal ID="ltrEduProgram" runat="server" />
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="row backup-plans hide">
                            <div class="col-md-4"></div>
                            <div class="col-md-5 mr-auto">
                                <div class="card m-0">
                                    <div class="card-body">
                                        <table class="table table-borderless mb-0 backup-plans-table">
                                            <tbody>
                                                <%--Load Backup Plans--%>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2 ml-auto mr-auto mx-auto text-center" style="padding-top: 25px;">
                                <button id="btnNext" class="btn btn-success btn-round btn-block" style="font-size: 1.2em; height: 47px; padding-top: 7px;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %><p class="h6" style="margin-bottom: 0px;">(Next)</p>
                                </button>
                            </div>
                        </div>
                        <div class="row">
                            <br>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">

    <!-- Plugin for Select, full documentation here: http://silviomoreto.github.io/bootstrap-select -->
    <script src="assets/js/plugins/bootstrap-selectpicker.js"></script>

    <script>

        function LoadDataFromLocalStorage() {
            // Get data from localStorage
            if (ls.isBrowserSupport()) {
                // Code for localStorage
                preRegister = ls.getItem('preRegister');

                if (preRegister.Page03Saved) {

                    $('#sltYear').selectpicker('val', preRegister.Year);
                    LoadStudentType($("#sltYear").val(), $("#sltYear option:selected").data('year-be'));

                    $('#sltStudentType').selectpicker('val', preRegister.StudentType);
                    LoadClass($("#sltYear option:selected").data('year-be'), $("#sltStudentType").val());

                    $('#sltClass').selectpicker('val', preRegister.Class);
                    LoadPlan($("#sltYear option:selected").data('year-be'), $("#sltStudentType").val(), $("#sltClass").val());

                    $('#sltEduProgram').selectpicker('val', preRegister.EduProgram);

                    // Set backup plans in local storage
                    var backupPlansObj = JSON.parse(preRegister.BackupPlans);
                    $.each(backupPlansObj, function () {
                        $('#sltBackupPlans' + this.no).selectpicker('val', this.planId);
                    });

                    if ($("#sltClass option:selected").text().includes("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>")) {
                        $(".option-time").show();
                        $(".option-branch1").show();
                        $(".option-branch2").hide();

                        $('#sltOptionTime').selectpicker('val', preRegister.OptionTime);
                        $('#sltOptionBranch1').selectpicker('val', preRegister.OptionBranch);
                    }
                    else if ($("#sltClass option:selected").text().includes("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>")) {
                        $(".option-time").show();
                        $(".option-branch1").hide();
                        $(".option-branch2").show();

                        $('#sltOptionTime').selectpicker('val', preRegister.OptionTime);
                        $('#sltOptionBranch2').selectpicker('val', preRegister.OptionBranch);
                    }
                    else {
                        $(".option-time").hide();
                        $(".option-branch1").hide();
                        $(".option-branch2").hide();
                    }
                }

            } else {
                // No web storage Support.
            }
        }

        function SaveDataToLocalStorage() {
            if ($("#form").valid()) {

                preRegister.Year = $("#sltYear").val();
                preRegister.YearBE = $("#sltYear option:selected").data('year-be');
                preRegister.StudentType = $('#sltStudentType').val();
                preRegister.Class = $('#sltClass').val();
                preRegister.EduProgram = $('#sltEduProgram').val();
                if ($('#sltEduProgram').val() == '0') {
                    // Save backup plans
                    preRegister.MainPlan = $('select[name="sltBackupPlans[]"]:first-child option:not(".bs-title-option"):selected').val();

                    var backupPlans = [];
                    $('select[name="sltBackupPlans[]"]').each(function (i) {
                        backupPlans.push({ no: (i + 1), planId: $(this).find(':selected').val(), planName: $(this).find(':selected').text() });
                    });
                    preRegister.BackupPlans = JSON.stringify(backupPlans);
                }
                else {
                    preRegister.MainPlan = null;
                    preRegister.BackupPlans = null;
                }

                if ($("#sltClass option:selected").text().includes("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>")) {
                    preRegister.OptionTime = $('#sltOptionTime').val();
                    preRegister.OptionBranch = $('#sltOptionBranch1').val();
                }
                else if ($("#sltClass option:selected").text().includes("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>")) {
                    preRegister.OptionTime = $('#sltOptionTime').val();
                    preRegister.OptionBranch = $('#sltOptionBranch2').val();
                }

                preRegister.Page03Saved = true;

                // Save data to localStorage
                if (ls.isBrowserSupport()) {
                    // Code for localStorage
                    ls.setItem('preRegister', preRegister);
                } else {
                    // No web storage Support.
                }

                ez.activePageComplete(3);

                window.location.href = "RegisterOnline04.aspx";
            }
        }

        function LoadStudentType(year, yearBE) {
            if (yearBE) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline03.aspx/LoadStudentType",
                    data: '{year: ' + year + ', yearBE: ' + yearBE + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessLoadStudentType,
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }

        function OnSuccessLoadStudentType(response) {
            var studentTypes = response.d;

            $('#sltStudentType').empty();

            var options = '';
            $(studentTypes).each(function () {

                options += '<option value="' + this.ID + '">' + this.StudentType + '</option>';

            });

            $('#sltStudentType').html(options);

            //$('#sltClass').prop('disabled', false);
            $('#sltStudentType').selectpicker('refresh');
        }

        function LoadClass(yearBE, studentType) {
            if (yearBE && studentType) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline03.aspx/LoadClass",
                    data: '{yearBE: ' + yearBE + ', studentType: ' + studentType + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessLoadClass,
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }

        function OnSuccessLoadClass(response) {
            var classs = response.d;

            $('#sltClass').empty();

            var options = '';
            $(classs).each(function () {

                options += '<option value="' + this.nTSubLevel + '">' + this.SubLevel + '</option>';

            });

            $('#sltClass').html(options);

            //$('#sltClass').prop('disabled', false);
            $('#sltClass').selectpicker('refresh');
        }

        function LoadPlan(yearBE, studentType, classLevel) {
            if (yearBE && studentType && classLevel) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline03.aspx/LoadPlan",
                    data: '{yearBE: ' + yearBE + ', studentType: ' + studentType + ', classLevel: ' + classLevel + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessLoadPlan,
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }

        function OnSuccessLoadPlan(response) {
            var plans = response.d;

            $('#sltEduProgram').empty();

            var options = '';
            $(plans).each(function () {

                if (this.RegisterMax > this.RegisterAmount) {
                    options += '<option value="' + this.ID + '">' + this.Planname + '</option>';
                }
                else {
                    options += '<option value="' + this.ID + '" disabled>' + this.Planname + ' (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132832") %> ' + this.RegisterMax + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>)</option>';
                }

            });

            $('#sltEduProgram').html(options);

            //$('#sltEduProgram').prop('disabled', false);
            $('#sltEduProgram').selectpicker('refresh');
        }

        function LoadBackupPlans(yearBE, studentType, classLevel) {
            if (yearBE && studentType && classLevel) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "RegisterOnline03.aspx/LoadBackupPlans",
                    data: '{yearBE: ' + yearBE + ', studentType: ' + studentType + ', classLevel: ' + classLevel + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessLoadBackupPlans,
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }

        function OnSuccessLoadBackupPlans(response) {

            var r = JSON.parse(response.d);

            var options = '';
            $(r.data).each(function () {
                options += '<option value="' + this.planId + '">' + this.planName + '</option>';
            });


            var backupPlansRows = '';
            for (let i = 0; i < r.data.length; i++) {
                if (i == 0) {
                    backupPlansRows = `<tr>
                                            <td class="text-right" style="width: 30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132833") %> : </td>
                                            <td class="pl-3" style="width: 70%">
                                                <select id="sltBackupPlans`+ (i + 1) + `" name="sltBackupPlans[]" class="selectpicker main-plan" data-style="select-with-transition" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132834") %>">
                                                    <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132834") %></option>
                                                    `+ options + `
                                                </select>
                                            </td>
                                        </tr>`;
                }
                else {
                    backupPlansRows += `<tr>
                                            <td class="text-right" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132839") %> `+ i + ` : </td>
                                            <td class="pl-3">
                                                <select id="sltBackupPlans`+ (i + 1) + `" name="sltBackupPlans[]" class="selectpicker" data-style="select-with-transition" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132840") %> ` + i + `">
                                                    <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132840") %> ` + i + `</option>
                                                    `+ options + `
                                                </select>
                                            </td>
                                        </tr>`;
                }
            }

            $('.backup-plans-table tbody').html(backupPlansRows);

            $('select[name="sltBackupPlans[]"]').selectpicker();
        }

        var preRegister = null;
        $(document).ready(function () {

            //LoadDataFromLocalStorage();

            //ez.activateBootstrapSelect();

            $("#form").validate({
                rules: {
                    sltYear: "required",
                    sltStudentType: "required",
                    sltClass: "required",
                    sltOptionTime: "required",
                    sltOptionBranch1: {
                        required: function (element) {
                            return $("#sltClass option:selected").text().includes("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>") && !$(element).val();
                        }
                    },
                    sltOptionBranch2: {
                        required: function (element) {
                            return $("#sltClass option:selected").text().includes("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>") && !$(element).val();
                        }
                    },
                    sltEduProgram: "required"
                },
                messages: {
                    sltYear: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltStudentType: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltClass: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltOptionTime: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltOptionBranch1: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltOptionBranch2: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltEduProgram: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "sltYear":
                        case "sltStudentType":
                        case "sltClass":
                        case "sltOptionTime":
                        case "sltOptionBranch1":
                        case "sltOptionBranch2":
                        case "sltEduProgram": error.insertAfter(element.parent()); break;
                    }
                },
                onfocusout: false,
                invalidHandler: function (form, validator) {
                    var errors = validator.numberOfInvalids();
                    if (errors) {
                        validator.errorList[0].element.focus();
                    }
                }
            });

            $("#btnNext").bind({
                click: function () {

                    // Check main plan when choose all
                    if ($("#sltEduProgram").val() == '0' && !$('select[name="sltBackupPlans[]"]:first-child option:selected:not(".bs-title-option")').val()) {
                        Swal.fire({
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %>!',
                            html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132835") %>',
                            type: 'warning',
                            confirmButtonClass: "btn btn-warning",
                            buttonsStyling: false
                        });

                        return false;
                    }

                    if ($("#form").valid()) {

                        // Check already registered full amount in plan
                        var yearBE = $("#sltYear option:selected").data('year-be');
                        var studentType = $("#sltStudentType").val();
                        var classLevel = $("#sltClass").val();
                        var plan = $("#sltEduProgram").val();
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: "RegisterOnline03.aspx/CheckRegisteredFullAmount",
                            data: '{yearBE: ' + yearBE + ', studentType: ' + studentType + ', classLevel: ' + classLevel + ', plan: ' + plan + ' }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                var resp = JSON.parse(response.d);

                                if (resp.success && resp.registeredFullAmount) {
                                    Swal.fire({
                                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %>!',
                                        html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132836") %> (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132837") %> ' + resp.fullAmount + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>)',
                                        type: 'warning',
                                        confirmButtonClass: "btn btn-warning",
                                        buttonsStyling: false
                                    });

                                    return false;
                                }
                            },
                            failure: function (response) {
                                console.log(response.d);
                            },
                            error: function (response) {
                                console.log(response.d);
                            }
                        });
                    }

                    SaveDataToLocalStorage();

                    return false;
                }
            });

            $("#sltYear").change(function () {

                LoadStudentType($("#sltYear").val(), $("#sltYear option:selected").data('year-be'));

            });

            $("#sltStudentType").change(function () {

                LoadClass($("#sltYear option:selected").data('year-be'), $("#sltStudentType").val());

            });

            $("#sltClass").change(function () {

                // show/hide .option-time, option-branch1, option-branch2
                if ($("#sltClass option:selected").text().includes("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>")) {
                    $(".option-time").show();
                    $(".option-branch1").show();
                    $(".option-branch2").hide();
                }
                else if ($("#sltClass option:selected").text().includes("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>")) {
                    $(".option-time").show();
                    $(".option-branch1").hide();
                    $(".option-branch2").show();
                }
                else {
                    $(".option-time").hide();
                    $(".option-branch1").hide();
                    $(".option-branch2").hide();
                }

                LoadPlan($("#sltYear option:selected").data('year-be'), $("#sltStudentType").val(), $("#sltClass").val());

            });

            $("#sltEduProgram").change(function () {
                if ($(this).val() == '0') {
                    $('.backup-plans').removeClass('hide');

                    // Load backup plans
                    LoadBackupPlans($("#sltYear option:selected").data('year-be'), $("#sltStudentType").val(), $("#sltClass").val());
                }
                else {
                    $('.backup-plans').addClass('hide');
                }
            });

            $(document).on('change', 'select[name="sltBackupPlans[]"]', function () {
                if ($(this).val()) {
                    if ($(this).val() != $(this).data('prev-plan')) {
                        $('select[name="sltBackupPlans[]"] option[value="' + $(this).data('prev-plan') + '"]').attr('disabled', false);
                    }
                    $(this).data('prev-plan', $(this).val());
                    $('select[name="sltBackupPlans[]"] option[value="' + $(this).val() + '"]').attr('disabled', true);
                }
                else if ($(this).data('prev-plan')) {
                    $('select[name="sltBackupPlans[]"] option[value="' + $(this).data('prev-plan') + '"]').attr('disabled', false);
                    $(this).data('prev-plan', '');
                }
            });

            // Init

            LoadDataFromLocalStorage();

            ez.activateBootstrapSelect();

        });
    </script>
</asp:Content>
