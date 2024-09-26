
var UploadedFileName = '';

var tooltipVisible = false;

var circleProgress;
var intervalId;

var byteData;
var reader = new FileReader();
var _files;
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

    var formData = new FormData();
    //var objFiles =$('input[type=file]').val('');
    //var files = objFiles.files;
    //for (var i = 0; i < files.length; i++) {
    //$('#fuUpload').html(files[0]);
    formData.append('_pr_', _files);
    //}


    $.ajax({
        type: 'POST',
        url: '/import/product_Import.ashx?ShopID=' + shop_id,
        //data: "{'byteData': '" + byteData + "', 'year': " + year + ", 'level': " + level + ", 'classroom': " + classroom + ", 'sheetData': '" + sheetData + "' }",
        //contentType: "application/json; charset=utf-8",
        data: formData,
        //dataType: 'json',
        //contentType: false,
        //processData: false,
        //async: true,
        //contentType: "multipart/form-data; boundary=" + mul.bound,
        processData: false,
        contentType: false,
        cache: false,
        success: function (result) {

            var jsonObj = result;
            console.log(jsonObj);

            let _error = $.parseJSON(result.Error);

            if (result.isSuccess) {
                $('.row .table').show();
                $('.row.upload').hide();
                let html = "";
                $("#tblProductData tbody").html("");
                $.each(_error, function (r, s) {
                    html += "\
                    <tr style='display: table;width: 100 %;table - layout: fixed;/* even columns width , fix width of table too*/'>\
                        <td style=\"width: 25%;\">"+ s.TypeName + "</td>\
                        <td style=\"width: 25%;\">"+ s.BarCode + "</td>\
                        <td style=\"width: 25%;\">"+ s.ProductName + "</td>\
                        <td style=\"width: 25%;\">"+ s.Note + "</td>\
                    </tr>";
                });

                $("#tblProductData tbody").html(html);
            } else {
                //alert(result.message);
            }

            $('#modalUploadStudentData').modal('hide');

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
        error: function (xhr, status, error) {
            alert(error.responseText);

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
            url: "Product.aspx/UpdateMonitor",
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
function updateFiles() {

    var objFiles = $('input[type="file"]').get(0);
    var files = objFiles.files;

    _files = files[0];

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

        _files = files[0];

        reader.readAsDataURL(files[0]);

    });

    function updateFiles() {

        var objFiles = $('input[type="file"]').get(0);
        var files = objFiles.files;

        _files = files[0];

    }

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

                                $('#tblProductData tbody').append(`
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

                            var hasTooltip = $('#tblProductData [data-toggle="tooltip"]');
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

});
