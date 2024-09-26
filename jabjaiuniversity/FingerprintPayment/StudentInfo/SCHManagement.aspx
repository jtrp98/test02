<%@ Page Title="" Language="C#" MasterPageFile="~/Material.Master" AutoEventWireup="true" CodeBehind="SCHManagement.aspx.cs" Inherits="FingerprintPayment.StudentInfo.SCHManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
        .sch-management .card .card-body .col-form-label.label-checkbox, .sch-management .card .card-body .label-on-right.label-checkbox {
            padding-top: 5px;
            padding-right: 10px;
        }

        .sch-management .table .form-check .form-check-sign {
            top: -2px;
            left: 0;
            padding-right: 0;
        }

        .sch-management .table-responsive {
            display: block;
            width: 100%;
            overflow-x: inherit;
            -webkit-overflow-scrolling: touch;
            -ms-overflow-style: -ms-autohiding-scrollbar;
        }

        .sch-management .table-borderless tbody + tbody, .table-borderless td {
            border: 0;
        }

        .sch-management .table > tbody > tr > td {
            padding: 0px 8px;
            vertical-align: middle;
            border-color: #ddd;
        }

        .sch-management .bootstrap-select > .dropdown-toggle {
            width: 100%;
            padding-right: 10px;
            z-index: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row sch-management">
        <div class="col-md-12">
            <div class="card ">
                <div class="card-header card-header-rose card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">search</i>
                    </div>
                    <h4 class="card-title">Search Form</h4>
                </div>
                <div class="card-body ">
                    <div class="row">
                        <label class="col-md-2 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></label>
                        <div class="col-md-8">
                            <div class="form-group">
                                <input type="text" class="form-control" id="iptStudentID">
                            </div>
                        </div>
                        <label class="col-md-2 label-on-right"></label>
                    </div>
                </div>
                <div class="card-footer ml-auto mr-auto">
                    <button id="btnSearch" type="button" class="btn btn-fill btn-rose"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <form id="frmStudyInfo" class="form-horizontal">
                <div class="card">
                    <div class="card-header card-header-icon card-header-rose">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102049") %></h4>
                    </div>
                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table table-borderless">
                                <thead class="text-primary">
                                    <th width="10%"></th>
                                    <th width="10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></th>
                                    <th width="10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></th>
                                    <th width="17%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></th>
                                    <th width="18%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></th>
                                    <th width="17%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>
                                    <th width="18%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="card-footer ml-auto mr-auto">
                        <button id="btnSaveStudyInfo" type="button" class="btn btn-rose"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script>

        var LevelData = [<%=LevelData%>];
        var RoomData = [<%=RoomData%>];

        function initLevelRoom() {
            $.each(LevelData, function (i, data) {
                $('select[id^="sltLevelx"]').append($('<option/>', { text: data.levelName, value: data.levelID }));
            });
        }

        function setStudentLevel() {
            $('select[id^="sltLevelx"]').each(function (index, element) {
                // element == this
                var uniqueID = $(element).attr('id').replace('sltLevelx', '');
                var levelID = $('#chkx' + uniqueID).attr('data-level');
                var roomID = $('#chkx' + uniqueID).attr('data-room');
                $(element).selectpicker('val', levelID);

                if (!!$(element).val()) {
                    var classRooms = $(RoomData).filter(function (i, n) { return n.lid == $('#sltLevelx' + uniqueID).val(); });
                    if (classRooms && classRooms.length > 0) {
                        var sltRoom = $('#sltRoomx' + uniqueID);
                        $.each(classRooms, function (i, data) {
                            sltRoom.append($('<option/>', { text: data.name, value: data.id }));
                        });

                        sltRoom.selectpicker('val', roomID);
                    }
                }
            });
        }

        function setStudentStatus() {
            $('select[id^="sltStatusx"]').each(function (index, element) {
                // element == this
                var uniqueID = $(element).attr('id').replace('sltStatusx', '');
                var statusID = $('#chkx' + uniqueID).attr('data-status');
                $(element).selectpicker('val', statusID);
            });
        }

        function setFormValidation(id) {
            $(id).validate({
                highlight: function (element) {
                    $(element).closest('.form-group').removeClass('has-success').addClass('has-danger');
                    $(element).closest('.form-check').removeClass('has-success').addClass('has-danger');
                },
                success: function (element) {
                    $(element).closest('.form-group').removeClass('has-danger').addClass('has-success');
                    $(element).closest('.form-check').removeClass('has-danger').addClass('has-success');
                },
                errorPlacement: function (error, element) {
                    $(element).closest('.form-group').append(error);
                },
            });
        }

        function activateBootstrapSelect() {
            if ($(".selectpicker").length != 0) {
                $(".selectpicker").selectpicker();
            }
        }

        function blankForNull(s) {
            return s === null ? "" : s;
        }

        function nullForBlank(s) {
            return s === "" ? null : s;
        }

        $(document).ready(function () {

            //setFormValidation('#frmStudyInfo');

            $('.sch-management #btnSearch').click(function () {

                var param = { studentID: $('.sch-management #iptStudentID').val() };
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "SCHManagement.aspx/SearchStudent",
                    data: JSON.stringify(param),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        var r = JSON.parse(result.d);

                        console.log(r);

                        if (r.data.length > 0) {
                            var rows = '';
                            $.each(r.data, function (index, row) {
                                rows += `
<tr>
    <td>
        <div class="row">
            <div class="col-md-12 checkbox-radios">
                <div class="form-check form-check-inline">
                    <label class="form-check-label">
                        <input id="chkx`+ row.uniqueID + `" ` + (row.flag == 'old' ? 'checked' : '') + ` class="form-check-input" type="checkbox" data-sid="` + r.sid + `" data-flag="` + row.flag + `" data-hid="` + blankForNull(row.historyID) + `" data-year="` + row.yearID + `" data-term="` + row.termID + `" data-level="` + blankForNull(row.levelID) + `" data-room="` + blankForNull(row.roomID) + `" data-number="` + blankForNull(row.number) + `" data-status="` + blankForNull(row.status) + `">&nbsp;
                        <span class="form-check-sign">
                            <span class="check"></span>
                        </span>
                    </label>
                </div>
            </div>
        </div>
    </td>
    <td>
        <div class="row">
            <label class="col-md-12" style="margin-bottom: 0px;">`+ row.year + `</label>
        </div>
    </td>
    <td>
        <div class="row">
            <label class="col-md-12" style="margin-bottom: 0px;">`+ row.term + `</label>
        </div>
    </td>
    <td>
        <select id="sltLevelx`+ row.uniqueID + `" class="selectpicker" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111022") %>">
        </select>
    </td>
    <td>
        <select id="sltRoomx`+ row.uniqueID + `" class="selectpicker" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106126") %>">
        </select>
    </td>
    <td>
        <div class="form-group bmd-form-group">
            <label for="iptNumberx`+ row.uniqueID + `" class="bmd-label-floating"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></label>
            <input type="text" class="form-control" id="iptNumberx`+ row.uniqueID + `" number="true" value="` + blankForNull(row.number) + `">
        </div>
    </td>
    <td>
        <select id="sltStatusx`+ row.uniqueID + `" class="selectpicker" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103002") %>">
            <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101031") %></option>
            <option value="1" disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101033") %></option>
            <option value="2" disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101032") %></option>
            <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101034") %></option>
            <option value="4" disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101030") %></option>
            <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101035") %></option>
            <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101036") %></option>
            <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101037") %></option>
        </select>
    </td>
</tr>`;
                            });

                            $('.table.table-borderless tbody').html(rows);

                            initLevelRoom();

                            activateBootstrapSelect();

                            setStudentLevel();

                            setStudentStatus();


                        }
                        else {
                            $('.table.table-borderless tbody').html('No data found.');
                        }
                    },
                    error: onError
                });

                return false;
            });

            function onError(xhr, errorType, exception) {
                var responseText;
                try {
                    responseText = jQuery.parseJSON(xhr.responseText);
                    var errorMessage = "[" + errorType + ", " + exception + "] Exception:" + responseText.ExceptionType + ", StackTrace:" + responseText.StackTrace + ", Message:" + responseText.Message;

                    Swal.fire({
                        title: 'Error!',
                        text: 'Error Message - ' + errorMessage,
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        buttonsStyling: false
                    });
                } catch (e) {
                    responseText = xhr.responseText;
                    Swal.fire({
                        title: 'Error!',
                        text: 'Error Message - e' + responseText,
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        buttonsStyling: false
                    });
                }
            }

            $(document).on("change", 'select[id^="sltLevelx"]', function () {
                if (!!$(this).val()) {
                    var uniqueID = $(this).attr('id').replace('sltLevelx', '');
                    var levelID = $(this).val();
                    var rooms = $.grep(RoomData, function (room, i) { return room.lid == levelID; });
                    if (rooms && rooms.length > 0) {
                        var sltRoom = $('#sltRoomx' + uniqueID);
                        sltRoom.empty();
                        $.each(rooms, function (i, data) {
                            sltRoom.append($('<option/>', { text: data.name, value: data.id }));
                        });

                        $('#sltRoomx' + uniqueID).selectpicker('refresh');
                    }
                }
            });

            $('#btnSaveStudyInfo').click(function () {

                var studyInfos = [];
                //studyInfos.push({ check: true, sID: 0, termID: 0, historyID: 0, flag: 'delelte/insert', roomID: '', number: '', status: '' });

                $('.table.table-borderless tbody tr').each(function (index, element) {
                    // element == this
                    var chk = $(element).find('input[id^="chkx"]');
                    var uniqueID = chk.attr('id').replace('chkx', '');

                    var si = {};

                    si.check = chk.prop("checked");
                    si.flag = chk.attr('data-flag');

                    si.sID = chk.attr('data-sid');
                    si.termID = chk.attr('data-term');
                    si.historyID = chk.attr('data-hid');
                    si.roomID = nullForBlank($(element).find('#sltRoomx' + uniqueID).val());
                    si.number = nullForBlank($(element).find('#iptNumberx' + uniqueID).val());
                    si.status = nullForBlank($(element).find('#sltStatusx' + uniqueID + ' option:selected').val());

                    si.oRoomID = nullForBlank(chk.attr('data-room'));
                    si.oNumber = nullForBlank(chk.attr('data-number'));
                    si.oStatus = nullForBlank(chk.attr('data-status'));

                    if (si.check && si.flag == 'new') {
                        // insert case
                        si.flag = 'insert';
                    }
                    else if (!si.check && si.flag == 'old') {
                        // delete case
                        si.flag = 'delete';
                    }
                    else if (si.check && si.flag == 'old') {
                        // edit case
                        if (si.oRoomID != si.roomID || si.oNumber != si.number || si.oStatus != si.status) {
                            si.flag = 'edit';
                        }
                        else {
                            si.flag = 'nope';
                        }
                    }
                    else {
                        si.flag = 'nope';
                    }

                    studyInfos.push(si);
                });

                console.log(studyInfos);

                $.ajax({
                    async: true,
                    type: "POST",
                    url: "SCHManagement.aspx/SaveStudyInfo",
                    data: JSON.stringify({ studyInfos }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var r = JSON.parse(response.d);
                        if (r.success) {
                            swal({
                                title: "Save success!",
                                buttonsStyling: false,
                                confirmButtonClass: "btn btn-success"
                            });

                            $('.sch-management #btnSearch').click();
                        }
                        else {
                            swal({
                                title: "Save error! - " + r.message,
                                buttonsStyling: false,
                                confirmButtonClass: "btn btn-danger"
                            });
                        }
                    },
                    error: onError
                });

                return false;
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
