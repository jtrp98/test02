var studentList = {
    PageIndex: 0,
    PageSize: 100,
    PageCount: 0,
    dt: null,
    LoadListData: function () {
        studentList.dt = $("#tableData").DataTable({
            "processing": true,
            "serverSide": true,
            "info": false,
            "searching": false,
            "paging": false,
            "stateSave": true,
            "ajax": {
                "url": "leaveList.aspx/LoadLeaveLetter",
                "type": "POST",
                "contentType": "application/json; charset=utf-8",
                "data": function (d) {
                    let dayend = null, daystart = null;
                    if ($("#startDay").val() != '') {
                        daystart = $("#startDay").data("DateTimePicker").date().format('DD/MM/YYYY');
                        //moment($("#startDay").val(), 'DD/MM/YYYY').format('DD/MM/YYYY');
                    }
                    if ($("#endDay").val() != '') {
                        dayend = $("#endDay").data("DateTimePicker").date().format('DD/MM/YYYY');
                        //moment($("#endDay").val(), 'DD/MM/YYYY').format('DD/MM/YYYY');
                    }

                    d.jobtype = $("select[id*=ddlJob]").val();
                    d.username = TSAC.GetUserName(); // $("input[id*=ddlnamedrop]").val();
                    d.daystart = daystart;
                    d.dayend = dayend;
                    d.year = $("select[id*=DropDownList1]").val();
                    d.status = $("select[id*=ddlstatus]").val();
                    d.page = studentList.PageIndex;
                    d.length = studentList.PageSize;

                    return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                },
                "dataSrc": function (json) {
                    var jsond = $.parseJSON(json.d);
                    //console.log(jsond);
                    return jsond.data;
                },
                "beforeSend": function () {
                    // Handle the beforeSend event
                    //$("#modalWaitDialog").modal('show');
                },
                "complete": function () {
                    // Handle the complete event
                    //$("#modalWaitDialog").modal('hide');
                }
            },
            "columns": [
                { "data": "", "orderable": false },
                { "data": "letterType", "orderable": true },
                { "data": "date", "orderable": true },
                { "data": "dateStartLeave", "orderable": true },
                { "data": "writerName", "orderable": true },
                { "data": "homeRoomTeacher", "orderable": true },
                { "data": "letterStatus", "orderable": true },
                {
                    "data": "",
                    "render": function (type, row, data) {
                        let _html = "";
                        if (data.letterStatus !== "ยกเลิกการลา") {
                            _html += '<a href="/Leaveform/leaveDetails.aspx?id=' + data.letterId + '" class=" fas fa-envelope-open-text"></a>';
                            _html += ' <a href="/Leaveform/leavePrint.aspx?id=' + data.letterId + '" target="_blank" class="fa fa-print"></a>';
                            _html += ' <a href="/Leaveform/leaveRemove.aspx?id=' + data.letterId + '" class="fa fa-remove" style="color: red;"></a>';
                        }
                        else {
                            _html += ' <a href="/Leaveform/leavePrint.aspx?id=' + data.letterId + '" target="_blank" class="fa fa-print"></a>';
                        }
                        return _html;
                    },
                    "orderable": false
                },
            ],
            //"order": [[1, "asc"]],
            "columnDefs": [
                { className: "vertical-align-middle text-center", "targets": [0, 7] },
                { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6] },
                //{ className: "text-center hide", "targets": [8] },
                { "targets": [8, 9, 10, 11], "visible": false },
            ],
            "drawCallback": function (settings) {
                //var json = settings.json;
                var json = $.parseJSON(settings.json.d);

                studentList.PageCount = json.pageCount;

                var options = '';
                for (var pi = 0; pi < json.pageCount; pi++) {
                    options += '<option ' + (studentList.PageIndex == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                }
                $('#tableData #sltPageIndex').html(options);
                $('#tableData #sltPageIndex').selectpicker('refresh');
                $('#tableData #spnPageInfo').html('หน้าที่ ' + (studentList.PageIndex + 1) + ' จากทั้งหมด ' + studentList.PageCount + ' หน้า');
            },
        });

        // order.dt search.dt
        studentList.dt.on('draw.dt', function () {
            studentList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                cell.innerHTML = (studentList.PageIndex * studentList.PageSize) + (i + 1) + '.';
            });
            studentList.dt.column(6, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                var letterId = studentList.dt.cells({ row: i, column: 8 }).data()[0];
                var tid = studentList.dt.cells({ row: i, column: 8 }).data()[0];
                //$(cell).find(".fa-edit").parent().attr("href", "StudentDetail.aspx?v=form&sid=" + sid + "&tid=" + tid).attr("target", "_blank");
                //$(cell).find(".fa-key").parent().attr("onClick", 'ShowPassword(' + sid + ')');
                //$(cell).find(".fa-qrcode").parent().attr("onClick", 'GenerateQrCode(' + sid + ')');
                //$(cell).find(".fa-remove").parent().attr("sid", sid);
            });
        });
    },
    ReloadListData: function () {
        studentList.dt.draw();
    }
}

$(document).ready(function () {

    $.ajaxSetup({
        statusCode: {
            500: function () {
                //window.location.replace("/Default.aspx");
            }
        }
    });

    if (jQuery().dataTable) {
        $.fn.dataTable.ext.errMode = 'none';
    }

    // Searching, Pagination event 
    $('#btnSearch').click(function () {

        studentList.PageIndex = 0;

        studentList.ReloadListData();

        return false;
    });

    $('#tableData #sltPageSize').change(function () {

        studentList.PageSize = parseInt($("#tableData #sltPageSize").children("option:selected").val());
        studentList.PageIndex = 0;

        studentList.ReloadListData();

        return false;
    });

    $('#tableData #sltPageIndex').change(function () {

        studentList.PageIndex = parseInt($("#tableData #sltPageIndex").children("option:selected").val());

        studentList.ReloadListData();

        return false;
    });

    $('#tableData #aPrevious').click(function () {

        if (studentList.PageIndex > 0) {
            studentList.PageIndex--;
            studentList.ReloadListData();
        }

        return false;
    });

    $('#tableData #aNext').click(function () {

        if (studentList.PageIndex < (studentList.PageCount - 1)) {
            studentList.PageIndex++;
            studentList.ReloadListData();
        }

        return false;
    });

    // Initial control

    // Datatable Section
    studentList.LoadListData();
});

