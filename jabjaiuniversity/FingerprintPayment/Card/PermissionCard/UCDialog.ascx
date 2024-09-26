<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCDialog.ascx.cs" Inherits="FingerprintPayment.Card.PermissionCard.UCDialog" %>

<form id="formDialog1">
    <div class="modal-header ">
        <h4 class="modal-title text-center" id="exampleModalLabel">บันทึกขออนุญาต</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
    </div>
    <div class="modal-body">
        <div id="modal-wrapper">
            <div class="row">
                <input type="hidden" name="RefNo" id="RefNo" value="<%= ModelData.No %>" />
                <input type="hidden" name="StudentID" id="StudentID" value="<%= ModelData.StudentID %>" />
                <input type="hidden" name="Term" id="Term" value="<%= ModelData.Term %>" />
                <div class="col-md-4 col-form-label text-right"><strong>เลขที่ใบอนุญาต :</strong></div>
                <div class="col-md-8 col-form-label">
                    <%= ModelData.No %>
                </div>
                <div class="col-md-4 col-form-label text-right"><strong>วันที่บันทึกรายการ : </strong></div>
                <div class="col-md-8 col-form-label"><%= ModelData.Date.ToString("dd/MM/yyyy",new System.Globalization.CultureInfo("th-TH")) %></div>

                <div class="col-md-4 col-form-label text-right"><strong>รหัสนักเรียน :</strong></div>
                <div class="col-md-8 col-form-label"><%= ModelData.StudentCode %></div>

                <div class="col-md-4 col-form-label text-right"><strong>ชื่อ-สกุล :</strong></div>
                <div class="col-md-8 col-form-label"><%= ModelData.StudentName %></div>

                <div class="col-md-4 col-form-label text-right"><strong>ระดับชั้น :</strong></div>
                <div class="col-md-8 col-form-label"><%= ModelData.Class %></div>
            </div>

            <div class="row">
                <div class="col-md-4 col-form-label text-right"><strong>ประเภทของการขออนุญาต :</strong></div>
                <div class="col-md-8 ">
                    <select name="SelectedType" id="SelectedType" class="selectpicker --req-append-last" data-width="100%" data-size="7" data-style="select-with-transition" title="เลือกประเภท" required>
                        <% foreach (var item in ModelData.TypeList)
                            { %>
                        <option value="<%=item.Value %>"><%=item.Text %></option>
                        <% } %>
                    </select>
                </div>

                <div class="col-md-4 col-form-label text-right"><strong>ประวัติการขออนุญาตประเภทนี้ :</strong></div>
                <div class="col-md-2">
                    <input type="number" id="Time1" class="form-control" value="" />
                    <input type="number" id="Time2" name="Time" class="form-control" value="" style="display: none;" />
                </div>
                <div class="col-md-6 col-form-label">
                    ครั้ง
                </div>
                <div class="col-md-12 col-form-label ">
                    <input type="checkbox" class="checkbox-round mx-2" name="IsAttach" id="IsAttach">
                    <strong>มีเอกสารรับรองการขออนุญาต :</strong>
                </div>
                <div class="col-md-12 attach-wrapper" style="display: none">
                    <input type="file" id="attachfile" name="attachfile[]" accept="image/png, image/gif, image/jpeg, image/jpg" data-browse-on-zone-click="true" multiple data-msg-placeholder="เลือกไฟล์..." />
                </div>

                <div class="col-md-2 col-form-label text-right"><strong>ตั้งแต่วันที่ :</strong></div>
                <div class="col-md-4">
                    <div class="form-group ">
                        <input type="text" name="StartDate"  id="StartDate" class="datepicker form-control" value="" required />
                        <span class="form-control-feedback" style="color: #000; opacity: 1;">
                            <i class="material-icons">event</i>
                        </span>
                    </div>

                </div>
                <div class="col-md-1 col-form-label text-right"><strong>เวลา </strong></div>
                <div class="col-md-4">
                    <div class="form-group ">
                        <input type="text" name="StartTime" id="StartTime" class="timepicker1 form-control" value="" required />
                        <span class="form-control-feedback" style="color: #000; opacity: 1;">
                            <i class="material-icons">event</i>
                        </span>
                    </div>
                </div>

                <div class="col-md-2 col-form-label text-right"><strong>ถึงวันที่ :</strong></div>
                <div class="col-md-4">

                    <div class="form-group ">
                        <input type="text" name="EndDate" id="EndDate" class="datepicker form-control" value="" required />
                        <span class="form-control-feedback" style="color: #000; opacity: 1;">
                            <i class="material-icons">event</i>
                        </span>
                    </div>
                </div>
                <div class="col-md-1 col-form-label text-right"><strong>เวลา </strong></div>
                <div class="col-md-4">
                    <div class="form-group ">
                        <input type="text" name="EndTime" id="EndTime" class="timepicker1 form-control" value="" required />
                        <span class="form-control-feedback" style="color: #000; opacity: 1;">
                            <i class="material-icons">event</i>
                        </span>
                    </div>
                </div>

                <div class="col-md-2 col-form-label text-right"><strong>เนื่องจาก :</strong></div>
                <div class="col-md-10">
                    <textarea class="form-control" name="Cause" id="Cause" required></textarea>
                </div>

                <div class="col-md-2 col-form-label text-right"><strong>หมายเหตุ :</strong></div>
                <div class="col-md-10">
                    <textarea class="form-control" name="Note" id="Note"></textarea>
                </div>
            </div>
        </div>
    </div>
    <div class="modal-footer text-center" style="display: block;">
        <button type="button" class="btn btn-info" onclick="OnPreview()">ดูตัวอย่าง</button>
        <button type="submit" class="btn btn-success">บันทึก</button>
    </div>
</form>
<script>
    var $attach = $("#attachfile");
    function SaveFormData() {
        $('#formDialog1').on('submit', function (e) {

            var d = new FormData();
            var obj = {};
            obj.RefNo = $('#RefNo').val();
            obj.StudentID = $('#StudentID').val();
            obj.TypeID = $('#SelectedType').val();
            obj.Time = $('#Time').val();
            obj.IsAttach = $('#IsAttach').is(':checked');
                       
            //obj.AttachUrl = $('#AttachUrl').val();
            $.each($attach.fileinput('getFileList'), function (i, file) {
                d.append('attFile1', file);
            });

            obj.StartDate = moment($('#StartDate').val() + ' ' + $('#StartTime').val(), 'DD/MM/YYYY hh:mm')
                .add(-543, 'years')
                .format('MM/DD/YYYY hh:mm');
            obj.EndDate = moment($('#EndDate').val() + ' ' + $('#EndTime').val(), 'DD/MM/YYYY hh:mm')
                .add(-543, 'years')
                .format('MM/DD/YYYY hh:mm');               
            obj.Cause = $('#Cause').val();
            obj.Note = $('#Note').val();

            d.append('data', JSON.stringify(obj));

            //alert('stop');
            $.ajax({
                type: "post",
                url: "SaveData.ashx?sid=" + $('#StudentID').val(),
                data: d,
                contentType: false, // Important
                processData: false, // Important
                success: function (response) {
                    Swal.fire({
                        type: 'success',
                        title: 'บันทึกสำเร็จ',
                        showConfirmButton: false,
                        timer: 1500,
                        //willClose: () => {
                        //    window.location.reload();
                        //}
                    }).then((result) => {
                        //window.location.reload();
                        $('#Modal1').modal('hide');
                        var url = `Preview.aspx?sID=${$("#StudentID").val()}&term=${$("#Term").val()}&pID=${response.id}`;

                        window.open(url, '_blank');
                    })
                },
                error: function (result) {
                    Swal.fire({
                        type: 'error',
                        title: 'บันทึกไม่สำเร็จ',
                        //ext: response.msg,
                    })
                },
            });
            return false;

        });
      
    }

    function OnPreview() {
        var date1 = $('#StartDate').val();
        var date2 = $('#EndDate').val();
        var time1, time2;
        if (date1 == date2) {
            time1 = $('#StartTime').val();
            time2 = $('#EndTime').val();
        }
        else {
            time1 = date1 + " " + $('#StartTime').val();
            time2 = date2 + " " + $('#EndTime').val();
        }
        var url = `Preview.aspx?sID=${$("#StudentID").val()}&term=${$("#Term").val()}&refNo=${$("#RefNo").val()}&cause=${$("#Cause").val()}&type=${$('#SelectedType :selected').text()}&date1=${time1}&date2=${time2}`;

        window.open(url, '_blank');
    }

    $(function () {
             
        $(".selectpicker").selectpicker();

        $('.datepicker').datetimepicker({
            format: 'DD/MM/YYYY-BE',
            locale: 'th',
            debug: false,
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

        $('.timepicker1').timepicker();

        $(".datepicker").keydown(function (e) {
            e.preventDefault();
        });

        $attach.fileinput({
            /* browseClass: "btn btn-info",*/
            /*mainClass: "d-grid",*/
            deleteUrl: "<%= ResolveUrl("~/ClassOnline/WorkUploadFile.ashx") %>", //just add dummy 
            uploadUrl: "#",
            append: true,
            purifyHtml: true,
            uploadAsync: true,
            maxFileCount: 1,
            maxFileSize: 5120,
            msgSizeTooLarge: 'ไฟล์ "{name}"(<b>{size}</b>) ใหญ่เกินไป ขนาดอัพโหลดสูงสุดที่อนุญาตคือ <b>{maxSize}</b>. กรุณาลองใหม่อีกครั้ง!',
            theme: 'fa5',
            showCaption: false,
            showRemove: false,
            showUpload: false,
            showBrowse: false,
            dropZoneEnabled: true,
            overwriteInitial: false,
            initialPreviewAsData: true,
            allowedFileExtensions: ['jpg', 'jpeg', 'png'],

        });

        $('#IsAttach').on('change', function (e) {
            var $input = $(this);
            if ($input.is(':checked')) {
                $('.attach-wrapper').show();
            }
            else {
                $('.attach-wrapper').hide();
            }
        });

        $("#SelectedType").on('change', function (e) {
            $("body").mLoading();
            $("#Time1").val('');
            $("#Time2").val('');
            $.ajax({
                type: "POST",
                url: "/Card/PermissionCard/Main.aspx/CheckHistoryCountTime",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    search: {
                        'typeId': $("#SelectedType").val(),
                        'sID': $('#StudentID').val(),
                    }
                }),
                success: function (response) {
                    $("#Time1").val(response.d.count);
                    $("#Time2").val(response.d.count);
                    $("body").mLoading("hide");
                }
            });
        });

        if (jQuery.validator) {//.messages

            jQuery.extend(jQuery.validator.messages, {
                required: "กรุณาระบุให้ครบถ้วน",
            });

            $("#formDialog1").validate({  // initialize the plugin

                errorPlacement: function (error, element) {
                    let _class = element.attr('class');

                    if (_class.includes('--req-append-last')) {
                        error.insertAfter(element.parent());
                    }
                    else {
                        error.insertAfter(element);
                    }

                }

            });
        }

        SaveFormData();

    });
</script>
