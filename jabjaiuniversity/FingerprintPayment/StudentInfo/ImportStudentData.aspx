<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ImportStudentData.aspx.cs" Inherits="FingerprintPayment.StudentInfo.ImportStudentData" EnableSessionState="ReadOnly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <style type="text/css">
        .uploadStudentData .dropdown-toggle .filter-option-inner-inner {
            font-size: 14px;
        }

        .div-row-padding {
            padding-top: 10px;
        }

        .mb-3, .my-3 {
            margin-bottom: 0rem !important;
        }

        .label {
            display: inline;
            padding: .2em .6em .3em;
            font-size: 75%;
            font-weight: bold;
            line-height: 1;
            color: #fff;
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            border-radius: .25em;
        }

        .label-primary {
            background-color: #337ab7;
        }

        .label-success {
            background-color: #5cb85c;
        }

        .label-warning {
            background-color: #f0ad4e;
        }

        .label-danger {
            background-color: #d9534f;
        }

        .drag-zone {
            width: 100%;
        }

            .drag-zone .file-drag-area {
                /*width: 600px;*/
                height: 400px;
                border: 1px dashed #707070;
                text-align: center;
                font-size: 24px;
                position: relative;
            }

            .drag-zone .file-drag-over {
                color: #eee;
                border-color: #eee;
            }

            .drag-zone .file-drag-area:before {
                content: "\f16b";
                font: normal normal normal 14px/1 FontAwesome;
                font-size: 3em;
                line-height: 250px;
                color: #707070;
            }

            .drag-zone .file-choose {
                position: absolute;
                bottom: 10px;
                width: 100%;
                height: 50%;
            }

                .drag-zone .file-choose p {
                    color: #707070;
                    margin: 10px 0 15px 0;
                }

        .circle-progress-value {
            stroke-width: 6px;
            stroke: hsl(280, 90%, 50%);
            stroke-linecap: round;
            stroke: aquamarine;
        }

        .circle-progress-circle {
            stroke: #ddd;
            stroke-width: 2px;
        }

        .circle-progress-text {
            font-family: "Georgia";
            font-size: 22px;
            fill: white;
        }

        .upload-progress {
            text-align: center;
            position: absolute;
            width: 452px;
            height: 400px;
            /* padding-top: 10px;
            margin-left: 15px; */
            background-color: rgba(50,50,50,0.5);
            z-index: 2;
        }

        [class^=upload-progress] > svg, [class*=" upload-progress"] > svg {
            width: 160px;
            height: 160px;
            position: absolute;
            top: 50%;
            margin-top: -80px;
            margin-left: -80px;
        }

        .warning-tooltip, .error-tooltip {
            cursor: pointer;
        }

        .warning-tooltip-msg, .error-tooltip-msg {
            padding: 10px 15px;
        }

        .tooltip-inner {
            padding: 0px;
            overflow: auto;
            max-height: 300px;
            min-width: 385px;
        }

            .tooltip-inner::-webkit-scrollbar {
                width: 14px;
            }

            .tooltip-inner::-webkit-scrollbar-track {
                background-color: #cccccc;
            }

            .tooltip-inner::-webkit-scrollbar-thumb {
                background-color: #999999;
                border-radius: 10px;
                border-style: solid;
                border-width: 3px;
                border-color: #cccccc;
            }

        .error-tooltip-msg {
            background-color: #d9534f;
            color: white;
        }

        .warning-tooltip-msg {
            background-color: #efa743;
            color: white;
        }

        .warning-tooltip + .tooltip .tooltip-inner::-webkit-scrollbar {
            width: 14px;
        }

        .warning-tooltip + .tooltip .tooltip-inner::-webkit-scrollbar-track {
            background-color: #eb9114;
        }

        .warning-tooltip + .tooltip .tooltip-inner::-webkit-scrollbar-thumb {
            background-color: #bc7410;
            border-radius: 10px;
            border-style: solid;
            border-width: 3px;
            border-color: #eb9114;
        }

        #tblStudentData tbody tr th.no, #tblStudentData tbody tr td.code, #tblStudentData tbody tr td.idcardnumber {
            text-align: center;
        }

        #tblStudentData tbody tr td.status {
            vertical-align: middle;
        }

        /*.swal2-icon {
            font-size: 12px !important;
        }

        .swal2-title {
            font-size: 26px !important;
        }

        .swal2-content {
            font-size: 24px !important;
        }*/

        .choose-sheet-data {
            display: flex;
        }

            .choose-sheet-data input[type=checkbox] {
                /* Double-sized Checkboxes */
                -ms-transform: scale(1.3); /* IE */
                -moz-transform: scale(1.3); /* FF */
                -webkit-transform: scale(1.3); /* Safari and Chrome */
                -o-transform: scale(1.3); /* Opera */
                transform: scale(1.3);
                padding: 10px;
                cursor: pointer;
            }

        .form-check.form-check-inline {
            margin-right: 15px;
        }

        .new-row-import {
            color: var(--green);
            font-size: 20px;
            margin: -11px 0px 0px -10px;
            position: absolute;
            background-color: white;
            border-radius: 50%;
            border: 0px solid white;
            width: 18px;
            height: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101027") %>
            </p>
        </div>
    </div>

    <div class="uploadStudentData row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">file_upload</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101274") %></h4>
                </div>
                <div class="card-body">
                    <div class="row div-row-padding">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-11 col-form-label text-left" style="font-size: 16px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101275") %></label>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-11 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101276") %> <a href="/import/downloadList.aspx" style="text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101277") %></a></label>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-3 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101278") %></label>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltYear" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %>">
                                <asp:Literal ID="ltrYear" runat="server" />
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-3 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101279") %></label>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltLevel" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %>">
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-3 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101280") %></label>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltClassroom" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101281") %>">
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-3 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101282") %></label>
                        <div class="col-md-6 checkbox-radios choose-sheet-data">
                            <div class="form-check form-check-inline">
                                <label class="form-check-label" style="font-weight: bold;">
                                    <input id="chkSheet1" class="form-check-input choose-term" type="checkbox" value="1" disabled checked />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %>
                                    <span class="form-check-sign">
                                        <span class="check"></span>
                                    </span>
                                </label>
                            </div>
                            <div class="form-check form-check-inline">
                                <label class="form-check-label" style="font-weight: bold;">
                                    <input id="chkSheet2" class="form-check-input choose-term" type="checkbox" value="2" />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111021") %>
                                    <span class="form-check-sign">
                                        <span class="check"></span>
                                    </span>
                                </label>
                            </div>
                            <div class="form-check form-check-inline">
                                <label class="form-check-label" style="font-weight: bold;">
                                    <input id="chkSheet3" class="form-check-input choose-term" type="checkbox" value="3" />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %>
                                    <span class="form-check-sign">
                                        <span class="check"></span>
                                    </span>
                                </label>
                            </div>
                            <div class="form-check form-check-inline">
                                <label class="form-check-label" style="font-weight: bold;">
                                    <input id="chkSheet4" class="form-check-input choose-term" type="checkbox" value="4" />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %>
                                    <span class="form-check-sign">
                                        <span class="check"></span>
                                    </span>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-3 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101283") %></label>
                        <div class="col-md-3">
                            <button id="btnBrowse" type="button" class="btn btn-warning global-btn disabled" data-toggle="modal" data-target="#modalUploadStudentData">
                                <i class="fa fa-file-excel-o" aria-hidden="true" style="vertical-align: middle; padding-right: 10px;"></i>Browse File</button>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                        <div class="col-md-12">
                            <div class="material-datatables">
                                <div id="datatables_wrapper" class="dataTables_wrapper dt-bootstrap4">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="tblStudentData" class="table table-no-bordered table-hover" cellspacing="0" width="100%" style="width: 100%">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 5%" class="text-center" scope="col"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>
                                                        <th style="width: 10%" class="text-center" scope="col"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                                        <th style="width: 25%" class="text-center" scope="col"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                                                        <th style="width: 25%" class="text-center" scope="col"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                                        <th style="width: 20%" class="text-center" scope="col"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %></th>
                                                        <th style="width: 15%" class="text-center" scope="col"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%--<tr>
                                                            <th class="no" scope="row"></th>
                                                            <td class="code"></td>
                                                            <td class="name"></td>
                                                            <td class="lastname"></td>
                                                            <td class="idcardnumber"></td>
                                                            <td class="status">
                                                                <span class="label label-primary" style="font-size: 15px; padding: .1em .6em 0.2em;">ready</span>
                                                                <span class="label label-success" style="font-size: 15px; padding: .1em .6em 0.2em;">success</span>
                                                                <span class="label label-warning warning-tooltip" style="font-size: 15px; padding: .1em .6em 0.2em;" data-toggle="tooltip" data-placement="top" data-html="true" title="<div class='text-left'>1.Hooray!<br>2.Hooray! Hooray! Hooray! Hooray! Hooray! Hooray! </div>">warning:[3]</span>
                                                                <span class="label label-danger error-tooltip" style="font-size: 15px; padding: .1em .6em 0.2em;" data-toggle="tooltip" data-placement="top" data-html="true" title="<div class='text-left'>1.Hooray!<br>2.Hooray! Hooray! Hooray! Hooray! Hooray! Hooray! </div>">error:[3]</span>
                                                            </td>
                                                        </tr>--%>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding-bottom: 10px;">
                        <div class="col-md-12 text-center">
                            <div style="width: fit-content; margin-left: auto; margin-right: auto;">
                                <button id="btnImportData" type="button" class="btn btn-warning global-btn disabled">
                                    <i class="fa fa-floppy-o" aria-hidden="true" style="vertical-align: middle; padding-right: 10px;"></i>Import Data</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->

    <div id="modalUploadStudentData" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 5%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="max-width: 900px;">

                <div class="modal-header" style="padding: 0px; top: 15%; display: block; text-align: center;">
                    <h3>Upload File Excel</h3>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row" style="display: block;">
                            <div id="prgUpload" class="upload-progress" style="display: none;"></div>
                            <div class="col-xs-12 text-center">
                                <div style="display: none;">
                                    <input type="file" name="fuUpload" id="fuUpload" />
                                </div>
                                <div class="drag-zone" align="center">
                                    <div class="file-drag-area">
                                        <div class="file-choose">
                                            <div class="row">
                                                <div class="col-md-12 mb-12">
                                                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101285") %></p>
                                                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %></p>
                                                    <br />
                                                    <input id="btnUpload" name="btnUpload" type="button" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101287") %>" class="btn btn-success" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button id="btnCloseModalUpload" type="button" class="btn btn-danger global-btn"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>

            </div>
        </div>
    </div>
    <!-- /.modal -->

    <div id="modalImportStudentData" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 5%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="max-width: 900px;">

                <div class="modal-header" style="padding: 0px; top: 15%; display: block; text-align: center;">
                    <h3>Import Student Data</h3>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row" style="display: block;">
                            <div id="prgImport" class="upload-progress" style="display: none;"></div>
                            <div class="col-xs-12 text-center">
                                <div class="" align="center" style="height: 400px;">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button id="btnCloseModalImport" type="button" class="btn btn-danger global-btn"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>

            </div>
        </div>
    </div>
    <!-- /.modal -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type='text/javascript' src="/Scripts/init-function.js?v=<%=DateTime.Now.Ticks%>"></script>

    <script type="text/javascript" src="/StudentInfo/Scripts/circle-progress.js"></script>
    <script type="text/javascript">

        var LevelData = [<%=LevelData%>];
        var ClassRoomData = [<%=ClassRoomData%>];
        var UploadedFileName = '';

        var tooltipVisible = false;

        var circleProgress;
        var intervalId;

        var byteData;
        var reader = new FileReader();
        reader.onload = function () {

            circleProgress = new CircleProgress('#prgUpload', {
                max: 100,
                value: 0,
                textFormat: 'percent',
                fill: { color: '#ffffff' }
            });

            byteData = this.result;

            byteData = byteData.split(';')[1].replace("base64,", "");

            var year = $(".uploadStudentData #sltYear").children("option:selected").text();
            var level = $(".uploadStudentData #sltLevel").children("option:selected").val();
            var classroom = $(".uploadStudentData #sltClassroom").children("option:selected").val();
            var sheetData = '';
            $('.choose-sheet-data input[type=checkbox]').each(function () {
                sheetData += $(this).is(':checked') ? '1' : '0';
            });

            $.ajax({
                type: "POST",
                url: "ImportStudentData.aspx/UploadAndPrepareData",
                data: "{'byteData': '" + byteData + "', 'year': " + year + ", 'level': " + level + ", 'classroom': " + classroom + ", 'sheetData': '" + sheetData + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    var jsonObj = $.parseJSON(result.d);
                    console.log(jsonObj);

                    if (jsonObj.success) {

                        UploadedFileName = jsonObj.uploadedFileName;

                        // Render row status
                        $("#tblStudentData tbody").empty();

                        $.each(jsonObj.processDatas, function (i, row) {

                            var status = '';
                            var msgs = '';
                            if (row.status == 'ready') {
                                if (!row.inDB) {
                                    status = `<i class="material-icons new-row-import">add_circle</i>`;
                                }
                                status += `<span class="label label-primary ready" data-code="` + row.code + `" style="font-size: 15px; padding: .1em .6em 0.2em; margin-right: 3px;">ready</span>`;

                                var warningMessages = $.grep(row.errors, function (n, i) { return n.status == 'warning'; });

                                if (warningMessages.length > 0) {
                                    msgs = '';
                                    for (var i = 0; i < warningMessages.length; i++) {
                                        msgs += (i + 1) + '. ' + warningMessages[i].message + '<br/>';
                                    }
                                    status += `<span class="label label-warning warning-tooltip" style="font-size: 15px; padding: .1em .6em 0.2em; margin-right: 3px;" data-toggle="tooltip" data-placement="top" data-html="true" title="<div class='text-left warning-tooltip-msg'>` + msgs + `</div>">warning:[` + warningMessages.length + `]</span>`;
                                }
                            }
                            else {

                                var warningMessages = $.grep(row.errors, function (n, i) { return n.status == 'warning'; });
                                var errorMessages = $.grep(row.errors, function (n, i) { return n.status == 'error'; });

                                if (warningMessages.length > 0) {
                                    msgs = '';
                                    for (var i = 0; i < warningMessages.length; i++) {
                                        msgs += (i + 1) + '. ' + warningMessages[i].message + '<br/>';
                                    }
                                    status = `<span class="label label-warning warning-tooltip" style="font-size: 15px; padding: .1em .6em 0.2em; margin-right: 3px;" data-toggle="tooltip" data-placement="top" data-html="true" title="<div class='text-left warning-tooltip-msg'>` + msgs + `</div>">warning:[` + warningMessages.length + `]</span>`;
                                }
                                if (errorMessages.length > 0) {
                                    msgs = '';
                                    for (var i = 0; i < errorMessages.length; i++) {
                                        msgs += (i + 1) + '. ' + errorMessages[i].message + '<br/>';
                                    }
                                    status += `<span class="label label-danger error-tooltip" style="font-size: 15px; padding: .1em .6em 0.2em; margin-right: 3px;" data-toggle="tooltip" data-placement="top" data-html="true" title="<div class='text-left error-tooltip-msg'>` + msgs + `</div>">error:[` + errorMessages.length + `]</span>`;
                                }
                            }

                            $('#tblStudentData tbody').append(`
<tr>
    <th class="no" scope="row">`+ row.no + `</th>
    <td class="code">`+ row.code + `</td>
    <td class="name">`+ row.name + `</td>
    <td class="lastname">`+ row.lastname + `</td>
    <td class="idcardnumber">`+ row.idCardNumber + `</td>
    <td class="status" data-indb="`+ row.inDB + `">
        `+ status + `
    </td>
</tr>`);
                        });


                        //$('#tblStudentData .error-tooltip[data-toggle="tooltip"]')
                        //$('#tblStudentData .warning-tooltip[data-toggle="tooltip"]')
                        var hasTooltip = $('#tblStudentData [data-toggle="tooltip"]');
                        hasTooltip.on('click', function (e) {
                            e.preventDefault();
                            var isShowing = $(this).data('isShowing');
                            hasTooltip.removeData('isShowing');
                            if (isShowing !== 'true') {
                                hasTooltip.not(this).tooltip('hide');
                                $(this).data('isShowing', "true");
                                $(this).tooltip('show');
                            }
                            else {
                                $(this).tooltip('hide');
                            }
                        }).tooltip({
                            animation: true,
                            trigger: 'manual'
                        });

                        if ((jsonObj.code == '200' || jsonObj.code == '201' || $("span.ready:contains('ready')").length > 0) && ((jsonObj.contactData.limitInContact > 0 && (jsonObj.contactData.remainingNumber - jsonObj.newRowImport) >= 0) || jsonObj.newRowImport == 0)) {
                            $("#btnImportData").removeClass('disabled');
                            $("#btnImportData").parent().removeAttr("onclick");
                        }
                        else {
                            // 202, 203
                            $("#btnImportData").addClass('disabled');

                            if (!((jsonObj.contactData.limitInContact > 0 && (jsonObj.contactData.remainingNumber - jsonObj.newRowImport) >= 0) || jsonObj.newRowImport == 0)) {
                                $("#btnImportData").parent().attr('onclick', 'systemMessage.LimitInContact({"data":{"limitInContact":' + jsonObj.contactData.limitInContact + ', "currentNumber":' + jsonObj.contactData.currentNumber + ', "remainingNumber":' + jsonObj.contactData.remainingNumber + ', "excessNumber":' + jsonObj.contactData.excessNumber + '}}); return false;');
                            }
                        }

                        $('#modalUploadStudentData').modal('hide');
                    }
                    else {
                        Swal.fire({
                            title: 'Error!',
                            text: 'Error Message - ' + jsonObj.message,
                            type: 'error',
                            confirmButtonClass: "btn btn-error",
                            buttonsStyling: false
                        });

                        $("#btnImportData").addClass('disabled');
                    }

                    resetMonitor('#btnCloseModalUpload', '#modalUploadStudentData');

                    //Clear input[file]
                    $('input[type=file]').val('');
                },
                beforeSend: function () {
                    // Display upload-progress
                    $('.upload-progress').show();

                    $('#btnCloseModalUpload').prop('disabled', true);

                    disabledClickOutsideModal('#modalUploadStudentData', true);
                },
                error: function (response) {
                    alert(response.responseText);

                    resetMonitor('#btnCloseModalUpload', '#modalUploadStudentData');
                }
            });

            // Periodically update monitors
            startMonitor('#btnCloseModalUpload', '#modalUploadStudentData');

        }

        function startMonitor(closeModalButtonID, modalElementID) {
            intervalId = setInterval(function () {
                $.ajax({
                    type: "POST",
                    url: "ImportStudentData.aspx/UpdateMonitor",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        var jsonObj = $.parseJSON(result.d);

                        if (jsonObj.isComplete) {
                            clearInterval(intervalId);
                            intervalId = null;
                        }

                        updateMonitor(jsonObj.percentage);
                    },
                    error: function (response) {
                        clearInterval(intervalId);
                        intervalId = null;

                        resetMonitor(closeModalButtonID, modalElementID);
                    }
                });
            }, 1000);
        }

        function updateMonitor(value) {
            circleProgress.value = value;
        }

        function resetMonitor(closeModalButtonID, modalElementID) {
            $('.upload-progress').hide();

            circleProgress.value = 0;

            $(closeModalButtonID).prop('disabled', false);

            disabledClickOutsideModal(modalElementID, false);
        }

        function disabledClickOutsideModal(elementID, flag) {
            var $modal = $(elementID);
            // Prevent to close by ESC
            var keyboard = false; // 
            // Prevent to close on click outside the modal
            // If you specify the value "static", it is not possible to close the modal when clicking outside of it
            var backdrop = flag ? 'static' : true; // set true: click outside to escape modal, set 'static': not click outside to escape modal

            // Modal did not open yet
            if (typeof $modal.data('bs.modal') === 'undefined') {
                $modal.modal({
                    keyboard: keyboard,
                    backdrop: backdrop
                });
            } else {
                // Modal has already been opened
                //$modal.data('bs.modal').options.keyboard = keyboard;
                //$modal.data('bs.modal').options.backdrop = backdrop;
                $modal.data('bs.modal')._config.keyboard = keyboard;
                $modal.data('bs.modal')._config.backdrop = backdrop;

                if (keyboard === false) {
                    // Disable ESC
                    $modal.off('keydown.dismiss.bs.modal');
                } else {
                    // Resets ESC
                    $modal.data('bs.modal').escape();
                }
            }
        }

        // Check term count & alert & disabled button
        function CheckTermCount(showAlert) {
            if ($('#sltYear option:selected').data('term-count') < 2) {
                // Alert
                if (showAlert) {
                    Swal.fire({
                        title: 'Info!',
                        html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111018") %> <br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111019") %>',
                        type: 'info',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                }
                return false;
            }
            else {
                return true;
            }
        }

        $(function () {

            // Upload File
            $('input[type=file]').change(function () {
                if (typeof (FileReader) != "undefined") {
                    var regex = /^([\u0E00-\u0E7Fa-zA-Z0-9\s_\\.\-:()])+(.xls|.xlsx)$/;
                    $($(this)[0].files).each(function () {
                        var file = $(this);
                        if (regex.test(file[0].name.toLowerCase())) {
                            reader.readAsDataURL(file[0]);
                        } else {
                            alert(file[0].name + " is not a valid excel file.");
                            return false;
                        }
                    });
                } else {
                    alert("This browser does not support HTML5 FileReader.");
                }
            });

            // Upload File - with button
            $("#btnUpload").bind({
                click: function () {

                    $('#fuUpload').trigger('click');

                    return false;
                }
            });

            // Upload File - drag file
            $('.file-drag-area').on('dragover', function () {
                $(this).addClass('file-drag-over');
                return false;
            });
            $('.file-drag-area').on('dragleave', function () {
                $(this).removeClass('file-drag-over');
                return false;
            });
            $('.file-drag-area').on('drop', function (e) {

                e.preventDefault();

                $(this).removeClass('file-drag-over');

                var files = e.originalEvent.dataTransfer.files;

                reader.readAsDataURL(files[0]);

            });

            $('#sltYear').change(function () {
                if (!!$(this).val()) {
                    if (CheckTermCount(true) && $('#sltLevel').val() != '' && $('#sltClassroom').val() != '') {
                        $("#btnBrowse").removeClass('disabled');
                    }
                    else {
                        $("#btnBrowse").addClass('disabled');
                    }
                }
                else {
                    $("#btnBrowse").addClass('disabled');
                }
            });

            $('#sltLevel').change(function () {
                if (!!$(this).val()) {
                    var lid = $(this).val();
                    var classRooms = $.grep(ClassRoomData, function (classroom, i) { return classroom.lid == lid; });
                    if (classRooms && classRooms.length > 0) {
                        var sltClassroom = $('#sltClassroom');
                        sltClassroom.empty();
                        $.each(classRooms, function (i, data) {
                            sltClassroom.append($('<option/>', { text: data.name, value: data.id }));
                        });
                        $('#sltClassroom').selectpicker('refresh');
                    }

                    $("#btnBrowse").addClass('disabled');

                    $('#sltClassroom').change();
                }
                else {
                    $("#btnBrowse").addClass('disabled');
                }
            });

            $('#sltClassroom').change(function () {
                if (CheckTermCount(true) && $('#sltLevel').val() != '' && $('#sltClassroom').val() != '') {
                    $("#btnBrowse").removeClass('disabled');
                }
                else {
                    $("#btnBrowse").addClass('disabled');
                }
            });

            $("#btnImportData").bind({
                click: function () {

                    circleProgress = new CircleProgress('#prgImport', {
                        max: 100,
                        value: 0,
                        textFormat: 'percent',
                        fill: { color: '#ffffff' }
                    });

                    var year = $(".uploadStudentData #sltYear").children("option:selected").text();
                    var level = $(".uploadStudentData #sltLevel").children("option:selected").val();
                    var classroom = $(".uploadStudentData #sltClassroom").children("option:selected").val();
                    var sheetData = '';
                    $('.choose-sheet-data input[type=checkbox]').each(function () {
                        sheetData += $(this).is(':checked') ? '1' : '0';
                    });

                    var readyCodes = [];
                    $("span.ready:contains('ready')").each(function () {
                        readyCodes.push($(this).attr('data-code'));
                    });

                    $.ajax({
                        type: "POST",
                        url: "ImportStudentData.aspx/SaveData",
                        data: JSON.stringify({ fileName: UploadedFileName, year: year, level: level, classroom: classroom, sheetData: sheetData, readyCodes: readyCodes }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            var jsonObj = $.parseJSON(result.d);

                            //alert(jsonObj.message);
                            console.log(jsonObj);

                            switch (jsonObj.code) {
                                case "501":
                                    // Error contact limited
                                    systemMessage.LimitInContact({ "data": jsonObj.contactData });
                                    $("#btnImportData").parent().attr('onclick', 'systemMessage.LimitInContact({"data":{"limitInContact":' + jsonObj.contactData.limitInContact + ', "currentNumber":' + jsonObj.contactData.currentNumber + ', "remainingNumber":' + jsonObj.contactData.remainingNumber + ', "excessNumber":' + jsonObj.contactData.excessNumber + '}}); return false;');
                                    break;
                                default:

                                    // Render row status
                                    $("#tblStudentData tbody").empty();

                                    $.each(jsonObj.processDatas, function (i, row) {

                                        var status = '';
                                        var msgs = '';
                                        if (row.status.indexOf("success") != -1) {
                                            status = `<span class="label label-success" style="font-size: 15px; padding: .1em .6em 0.2em; margin-right: 3px;">` + row.status + `</span>`;
                                        }
                                        else {
                                            var errorMessages = $.grep(row.errors, function (n, i) { return n.status == 'error'; });

                                            if (errorMessages.length > 0) {
                                                msgs = '';
                                                for (var i = 0; i < errorMessages.length; i++) {
                                                    msgs += (i + 1) + '. ' + errorMessages[i].message + '<br/>';
                                                }
                                                status += `<span class="label label-danger error-tooltip" style="font-size: 15px; padding: .1em .6em 0.2em; margin-right: 3px;" data-toggle="tooltip" data-placement="top" data-html="true" title="<div class='text-left error-tooltip-msg'>` + msgs + `</div>">error:[` + errorMessages.length + `]</span>`;
                                            }
                                        }

                                        $('#tblStudentData tbody').append(`
<tr>
    <th class="no" scope="row">`+ row.no + `</th>
    <td class="code">`+ row.code + `</td>
    <td class="name">`+ row.name + `</td>
    <td class="lastname">`+ row.lastname + `</td>
    <td class="idcardnumber">`+ row.idCardNumber + `</td>
    <td class="status">
        `+ status + `
    </td>
</tr>`);
                                    });

                                    var hasTooltip = $('#tblStudentData [data-toggle="tooltip"]');
                                    hasTooltip.on('click', function (e) {
                                        e.preventDefault();
                                        var isShowing = $(this).data('isShowing');
                                        hasTooltip.removeData('isShowing');
                                        if (isShowing !== 'true') {
                                            hasTooltip.not(this).tooltip('hide');
                                            $(this).data('isShowing', "true");
                                            $(this).tooltip('show');
                                        }
                                        else {
                                            $(this).tooltip('hide');
                                        }
                                    }).tooltip({
                                        animation: true,
                                        trigger: 'manual'
                                    });

                                    if (jsonObj.success) {
                                        Swal.fire({
                                            title: "Success.",
                                            text: jsonObj.message,
                                            type: 'success',
                                            buttonsStyling: false,
                                            confirmButtonClass: "btn btn-success"
                                        });
                                    }
                                    else {
                                        Swal.fire({
                                            title: 'Error!',
                                            text: 'Error Message - ' + jsonObj.message,
                                            type: 'error',
                                            confirmButtonClass: "btn btn-error",
                                            buttonsStyling: false
                                        });
                                    }

                                    $("#btnImportData").parent().removeAttr("onclick");
                                    break;
                            }

                            $('#modalImportStudentData').modal('hide');

                            $("#btnImportData").addClass('disabled');

                            resetMonitor('#btnCloseModalImport', '#modalImportStudentData');

                        },
                        beforeSend: function () {
                            // Display upload-progress
                            $('.upload-progress').show();

                            $('#btnCloseModalImport').prop('disabled', true);

                            disabledClickOutsideModal('#modalImportStudentData', true);
                        },
                        error: function (response) {
                            alert(response.responseText);

                            resetMonitor('#btnCloseModalImport', '#modalImportStudentData');
                        }
                    });

                    // Periodically update monitors
                    startMonitor('#btnCloseModalImport', '#modalImportStudentData');

                    return false;
                }
            });

            // Initial 

            $('#sltLevel').empty();
            var sltLevel = $('#sltLevel');
            $.each(LevelData, function (i, data) {
                sltLevel.append($('<option/>', { text: data.levelName, value: data.levelID }));
            });
            // Insert first option to select
            $("#sltLevel").prepend("<option value='' selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111020") %></option>");
            $("#sltLevel").selectpicker('refresh');

        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
