var sublevel_data = [];
var class_data = [];
var group_data = [];
var invoices = [];
var i = 0;
var data_completed = 0, data_length = 0;
let savedata = false;
var invoice_data = [];
var appActive = true;

$(function () {
    addTableRow();
    $("#modalpopup-data .btn-danger").hide();
    PageMethods.getyear(
        function (response) {
            response = $.parseJSON(response);
            select_settingdata($("#year_id"), response.data, "เลือกปีการศึกษา", "year_id",
                "year_name", "");
        },
        function () { });

    $("#year_id").change(function () {
        PageMethods.gettrem($(this).val(),
            function (response) {
                getTuitionfeesetting($("#year_id").val(), $("#term_id").val());
                response = $.parseJSON(response);
                //if (response.data == []) addTableRow();
                select_settingdata($("#term_id"), response.data, "เลือกเทอม", "term_id",
                    "term_name", "");
            },
            function () {

            });
    });

    $("#ckbSetting").change(function () {
        if ($("#ckbSetting").prop("checked")) {
            $(".setting").css("display", "");
        } else {
            $(".setting").css("display", "none");
        }
    });

    $("#modalpopup-data-submit").click(function () {
        $("#modalpopup-data").modal("hide");
    });

    $("#term_id").change(function () {
        //if ($("#term_id").val() === "") addTableRow();
        //else
        $("#tableAddRow thead input[type=checkbox]").prop("checked", true);
        getTuitionfeesetting($("#year_id").val(), $("#term_id").val());
    });

    $("#submit").click(function (e) {
        SaveDataToDataBase();
    });

    $("#submitpeak").click(function () {
        //alert("อยู่ระหว่างการปรับปรุงขออภัยในความไม่สะดวก");
        if (savedata === false) {
            SaveDataToDataBase();
            SaveComplete();
        } else {
            appActive = true;
            invoice_data = [];
            $.confirm({
                title: '<h3>แจ้งเตือน</h3>',
                content: `<h3><input type="radio" name="appActive" value="0"/> สร้างใบหนี้โดยปิดการแสดงผลในแอปพลิเคชันผู้ปกครอง<br/>
                                <input type="radio" name="appActive" value="1"/> สร้างใบแจ้งหนี้และแสดงผลในแอปพลิเคชันผู้ปกครองทันที<br/><br/>
                          <span style='color:red;'> หมายเหตุ : หากเป็นสร้างหนี้ซ้ำของนักเรียนคนเดิม ระบบจะลบใบแจ้งหนี้เก่าและสร้างใบใหม่ขึ้นมาแทนที่</span></h3>`,
                theme: 'bootstrap',
                buttons: {
                    "<span style=\"font-size: 20px;\">ตกลง</span>": function () {
                        if ($("[name=appActive]:checked").val() !== undefined) {
                            appActive = $("[name=appActive]:checked").val() == "1" ? true : false;
                            getInvoices();
                        } else {
                            $.confirm({
                                title: '<h3>แจ้งเตือน</h3>',
                                content: "<h3>กรุณาเลือกประเภททำรายการ</h3>",
                                theme: 'bootstrap',
                                buttons: {
                                    "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                                    }
                                }
                            });
                            return false;
                        }
                    }, "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                    }
                }
            });
            //var invoices_List = getInvoicesList();
            //loading_popup(1, invoices_List.length);
            //setpeakengine(invoices_List);
        }
    });
});

$(document).ready(function () {
    $(".datepicker").datepicker({ dateFormat: 'dd/mm/yy' });
    $('#aspnetForm').on('submit', function (event) {

        // adding rules for inputs with class 'comment'
        $($("#tableAddRow tbody").children("tr")).each(function () {
            addRules($(this).find("[id*=sublevel]").attr("id"),
                "กรุณาเลือกชั้นปี");
            addRules($(this).find("[id*=sublevel]").attr("id"),
                "กรุณาเลือกห้องเรียน");
            addRules($(this).find("[id*=group]").attr("id"),
                "กรุณาเลือกกลุ่มรายการ");
            addRules($(this).find("[id*=issuedDate]").attr("id"),
                "กรุณาเลือกวันที่ออกเอกสาร");
            addRules($(this).find("[id*=dueDate]").attr("id"),
                "กรุณาเลือกวันที่ครบกำหนดชำระเงิน");
            console.log($(this).find("[id*=sublevel]").attr("id"));
        });

        // prevent default submit action         
        event.preventDefault();

        // test if form is valid 
        if ($('#aspnetForm').valid()) {
            console.log("validates");
        } else {
            console.log("does not validate");
        }
    });

    $("#tableAddRow").hide();
    $('.addBtn').on('click', function () {
        addTableRow();
        $("#submitpeak").addClass("disabled");
        //$("#submitpeak").hide();
    });

    $('.addBtnRemove').click(function () {
        $("#submitpeak").addClass("disabled");
        $(this).closest('tr').remove();
    });

    //Get Group Data
    PageMethods.getgroup(
        function (response) {
            group_data = $.parseJSON(response).data;
            if (group_data !== [] && sublevel_data !== []) $("#tableAddRow").show();
        },
        function () { });

    //Get SubLevel Data
    PageMethods.getsublevel(
        function (response) {
            sublevel_data = $.parseJSON(response).data;
            if (group_data !== [] && sublevel_data !== []) $("#tableAddRow").show();
        },
        function () {

        });

    //Get Class Data
    PageMethods.getclass(
        function (response) {
            response = $.parseJSON(response);
            class_data = response;
        },
        function () {

        });

    $("#aspnetForm").validate({
        rules: {
            "year_id": "required",
            "term_id": "required"
        },
        messages: {
            "year_id": "กรุณาเลือกชั้นปี",
            "term_id": "กรุณาเลือกชั้นปี"
        },
        tooltip_options: {
            "year_id": {
                placement: 'top',
                trigger: 'focus'
            },
            "term_id": {
                placement: 'top',
                trigger: 'focus'
            }
        },
        submitHandler: function (e) { }
    });
});

//Send Data
function SaveComplete() {
    setTimeout(function () {
        if (savedata === false) {
            SaveComplete();
        } else {
            var invoices_List = getInvoicesList(false);
            loading_popup();
            setpeakengine(invoices_List);
        }
    }, 500);
}

var index_list = 0;

function getInvoicesList(rows_all) {
    var tr = $("#tableAddRow tbody").children("tr");
    var invoices_List = [];
    $.each(tr, function (s, e) {
        let row_last = $(e);
        let group_select = row_last.find("[id*=group]");
        let checkbox = row_last.find("input[type=checkbox]");
        let sublevel_select = row_last.find("[id^=sublevel]");
        let class_select = row_last.find("[id^=class]");
        let sublevelNew_select = row_last.find("[id^=New_sublevel]");
        let classNew_select = row_last.find("[id^=New_class]");
        let price_label = row_last.find("[id*=label]");
        let id = row_last.find("[id*=item_id]");
        sublevel_select.prop('disabled', 'disabled');
        class_select.prop('disabled', 'disabled');

        let Fd_NewTermClass_id = null, Fd_NewTermLevel_id = null;
        if (classNew_select.val() !== "" && isNaN(parseInt(classNew_select.val())) == false) {
            Fd_NewTermClass_id = parseInt(classNew_select.val());
        }
        if (sublevelNew_select.val() !== "" && isNaN(parseInt(sublevelNew_select.val())) == false) {
            Fd_NewTermLevel_id = parseInt(sublevelNew_select.val());
        }

        if (checkbox.prop('checked') === true || rows_all) {
            invoices_List.push({
                group_id: parseInt(group_select.val()),
                sublevel_id: parseInt(sublevel_select.val()),
                class_id: parseInt(class_select.val()),
                dueDate: $.datepicker.formatDate('mm/dd/yy', $(row_last.find(
                    "[id*=dueDate]")).datepicker(
                        'getDate')),
                issuedDate: $.datepicker.formatDate('mm/dd/yy', $(row_last.find(
                    "[id*=issuedDate]")).datepicker('getDate')),
                id: parseInt(id.val()),
                Fd_NewTermClass_id: Fd_NewTermClass_id,
                Fd_NewTermLevel_id: Fd_NewTermLevel_id
            });
        }
    });

    console.log(invoices_List);
    return invoices_List;
}

function loading_popup(index, length) {
    $("body").mLoading({
        "text": '<p class="h4" style="color:black;"> กำลังทำการส่งข้อมูลไปยัง Peak <br/><br/> ขั้นตอนนี้อาจใช้เวลา 5 - 10 นาที <br/><br/> เชิญพักผ่อนตามอัธยาศัย <br/><br/> กำลังดึงข้อมูลนักเรียน ( ' +
            index + ' / ' + length + ' )</p>',
        html: true,
        icon: "/scripts/blockUI/ProgressGreen.gif"
    });
}

function loading_popupMessage(message) {
    $("body").mLoading({
        "text": message,
        html: true,
        icon: "/scripts/blockUI/ProgressGreen.gif"
    });
}

function loading_sendinvoices_popup(index, length, time) {
    $("body").mLoading({
        "text": '<p class="h4" style="color:black;"> กำลังทำการส่งข้อมูลไปยัง Peak <br/><br/> ขั้นตอนนี้อาจใช้เวลา  ' +
            time +
            ' นาที <br/><br/> เชิญพักผ่อนตามอัธยาศัย <br/><br/> กำลังส่งข้อมูลนักเรียน <span id="loading_counter"> ( ' +
            index + ' / ' + length + ' ) </span></p>' +
            '<br/><p style="color:red;font-size:15px;">** โปรดอ่าน **</p><p style="color:red;font-size:15px;">ข้อมูลนักเรียน/นักศึกษาที่ได้ทำการแก้ไขในระบบ Peak อาทิเช่น แก้ไขวันที่ครบกำหนดชำระเงิน <br/>รับชำระเงิน แก้ไขรายการสินค้า หรืออื่นๆ ระบบจะไม่ทำการส่งข้อมูลไปใหม่</p>',
        html: true,
        icon: "/scripts/blockUI/ProgressGreen.gif"
    });
}

function getInvoices() {
    data_completed = 1;
    InvoicesGroup_Id = [];
    $.each($("#tableAddRow tbody tr"), function (index, values) {
        if ($(this).find("input[type=checkbox]:checked").prop("checked")) {
            InvoicesGroup_Id.push(parseInt($(this).find("input[id*=item_id]").val()));
        }
    });

    $("body").mLoading();
    rowIndex = 0;
    SaveInvoice(InvoicesGroup_Id[rowIndex]);
}

var InvoicesGroup_Id = [];
var rowIndex = 0;
function SaveInvoice(InvoicesGroup_Data) {
    let ckbSetting = false
    if ($("#ckbSetting").prop("checked") != undefined) {
        ckbSetting = $("#ckbSetting").prop("checked");
    }
    //console.log(InvoicesGroup_Data);
    //return;
    PageMethods.SaveInvoice(parseInt($("#year_id").val()), $("#term_id").val(), [InvoicesGroup_Data], ckbSetting, appActive,
        function (e) {
            rowIndex += 1;
            if (rowIndex === InvoicesGroup_Id.length) {
                $("body").mLoading("hide");
                $.confirm({
                    title: '<h3>แจ้งเตือน</h3>',
                    content: "<h3>ทำรายการเสร็จเรียบร้อยแล้ว</h3>",
                    theme: 'bootstrap',
                    buttons: {
                        "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                        }
                    }
                });
            } else {
                SaveInvoice(InvoicesGroup_Id[rowIndex]);
            }

            //e = $.parseJSON(e);
            //if (e.length === 0) {
            //    getvoid();
            //} else {
            //    data_length = 0;
            //    $.each(e, function (key, value) {
            //        data_length += value.invoicesStudents.length;
            //    });
            //    //loading_popup(1, data_length);    
            //    //var tolaltime = 2 * data_length;
            //    //var millisecond = tolaltime % 60;
            //    //var minute = (tolaltime - (tolaltime % 60)) / 60;
            //    //loading_sendinvoices_popup(data_completed, data_length, minute + "." + millisecond +
            //    //    " นาที - " + (minute + 5) + "." + millisecond);
            //    invoice_data = e;
            //    studentlength = 0;
            //    invoicelength = invoice_data.length;
            //    studentindex = 0;
            //    invoiceindex = 0;
            //    GetInvoicesData(invoiceindex);
            //}
        });
}

var JSon_Void = [];
var index;

function getVoid() {
    //data_completed = 0;
    $("body").mLoading("hide");
    $("#modalpopup-data").modal("show").find("#message").html("ส่งข้อมูลเรียบร้อยแล้ว");
    console.log("Send Void Completed !!");

    //$("#modalpopup-data").modal("show").find("#message").html("ส่งข้อมูลเรียบร้อยแล้ว");

    //loading_popupMessage("กำลังดึงข้อมูล Void");
    //PageMethods.getInvoicesVoid(parseInt($("#year_id").val()), $("#term_id").val(),
    //    function (response) {
    //        JSon_Void = $.parseJSON(response);
    //        data_length = JSon_Void.length;
    //        data_completed = 0;
    //        console.log(JSon_Void);
    //        $("body").mLoading("hide");
    //        if (JSon_Void.length === 0) {
    //            $("#modalpopup-data").modal("show").find("#message").html("ส่งข้อมูลเรียบร้อยแล้ว");
    //            console.log("Send Void Completed !!")
    //        } else {
    //            BootstrapDialog.show({
    //                title: 'ยืนยันการทำรายการ',
    //                closable: false,
    //                message: 'ส่งข้อมูล Void ไปยัง Peak',
    //                buttons: [{
    //                    label: 'ตกลง',
    //                    cssClass: 'btn-primary',
    //                    action: function (dialog) {
    //                        dialog.close();
    //                        SendVoid(data_completed);
    //                        //dialog.setTitle('Title 1');
    //                    }
    //                },
    //                {
    //                    label: 'ยกเลิก',
    //                    action: function (dialog) {
    //                        dialog.close();
    //                    }
    //                }
    //                ]
    //            });
    //        }
    //    },
    //    function (e) {

    //    });
}

var StudentLength = 0;
var InvoiceLength = 0;
var StudentIndex = 0;
var InvoiceIndex = 0;
var DataTmp = [];

function GetInvoicesData(rowsIndex) {
    DataTmp = invoice_data[rowsIndex];
    StudentLength = DataTmp.invoicesStudents.length;
    GetStudentData(StudentIndex);
    console.log(InvoiceIndex + " / " + InvoiceLength);
}

function GetStudentData(rowsIndex) {
    //console.log(rowsIndex + " " + StudentLength)
    if (rowsIndex < StudentLength) {
        var Value1 = invoice_data[InvoiceIndex].invoicesStudents[rowsIndex];
        var data = {
            "GroupId": invoice_data[InvoiceIndex].GroupId,
            "Id": invoice_data[InvoiceIndex].Id,
            "Name": invoice_data[InvoiceIndex].Name,
            "Update": new Date(parseInt(invoice_data[InvoiceIndex].Update.substring(6))),
            "School_id": invoice_data[InvoiceIndex].School_id,
            "invoicesStudents": [{
                "school_id": Value1.School_id,
                "issuedDate": new Date(parseInt(Value1.IssuedDate.substring(6))),
                "dueDate": new Date(parseInt(Value1.DueDate.substring(6))),
                "tuitionfeeDetail_id": Value1.Id,
                "contactId": Value1.ContactId,
                "student_id": Value1.Student_Id,
                "Id": Value1.Id
            }],
            "items": invoice_data[InvoiceIndex].items
        };
        //console.log(data)
        SendPeakEngine(data);
        //console.log((data_completed++) + " / " + data_length);
        //loading_popup(data_completed++, data_length);

    } else {
        InvoiceIndex += 1;
        if (InvoiceIndex < InvoiceLength) {
            StudentIndex = 0;
            GetInvoicesData(InvoiceIndex);
        } else {
            console.log("Send Invoices completed");
            console.log("completed : " + data_completed + " / " + data_length);
            //$("#loading_counter").html("( " + data_completed + " / " + data_length + " )");
            getVoid();
        }
    }
}

function SendPeakEngine(Invoices) {
    PageMethods.SetupInvoicess(Invoices,
        function (e) {
            StudentIndex += 1;
            if (data_completed > 1) $("#loading_counter").html("( " + data_completed + " / " + data_length + " )");
            data_completed += 1;
            GetStudentData(StudentIndex);
            console.log(e);
        },
        function () {

        });

}

function setpeakengine(invoices_List) {
    var data = invoices_List[index_list];
    PageMethods.getinvoice_student({
        year_id: parseInt($("#year_id").val()),
        term_id: $("#term_id").val(),
        "Invoices_List": [invoices_List[index_list]]
    },
        function (response) {
            if (response !== null) {
                $.each(response, function (index, data) {
                    invoice_data.push(data);
                });
            }
            index_list += 1;
            if (index_list < invoices_List.length) {
                loading_popup(index_list, invoices_List.length);
                setpeakengine(invoices_List);
            } else {
                index_list = 0;
                if (invoice_data.length === 0) {
                    $("body").mLoading("hide");
                    $("#modalpopup-data").modal("show").find("#message").html("ส่งข้อมูลเรียบร้อยแล้ว");
                } else {
                    var tolaltime = 2 * invoice_data.length;
                    var millisecond = tolaltime % 60;
                    var minute = (tolaltime - (tolaltime % 60)) / 60;
                    loading_sendinvoices_popup(index_list + 1, invoice_data.length, (minute + "." +
                        millisecond) + " นาที - " + (minute + 5) + "." + millisecond);
                    sendinvoices_peak(index_list);
                }
            }
        },
        function (e) {
            console.log(e.StackTrace);
        }
    );
}

function sendinvoices_peak(invoices_data_index) {
    console.log(invoice_data[invoices_data_index].name);
    PageMethods.sendinvoice_student(invoice_data[invoices_data_index],
        function (response) {
            index_list += 1;
            if (index_list < invoice_data.length) {
                console.log(response);
                //$("body").mLoading({ "text": 'กำลังทำการส่งข้อมูลไปยัง Peak กรุณารอชักครู่ ( ' + (index_list + 1) + ' / ' + invoices_List.length + ' )' });
                //loading_sendinvoices_popup(index_list, );
                $("#loading_counter").html("( " + index_list + " / " + invoice_data.length + " )");
                sendinvoices_peak(index_list);
            } else {
                console.log(response);
                $("body").mLoading("hide");
                $("#modalpopup-data").modal("show").find("#message").html("ส่งข้อมูลเรียบร้อยแล้ว");
                getTuitionfeesetting($("#year_id").val(), $("#term_id").val());
                index_list = 0;
            }
        },
        function (e) {
            console.log(e.StackTrace);
        }
    );
}

function SaveDataToDataBase() {
    //e.preventDefault();
    var invoices_List = getInvoicesList(true);
    if ($('form').valid() === false) return;
    if (invoices_List.length === 0) {
        $("#modalpopup-data").modal("show").find("#message").html("กรุณาเพิ่มข้อมูลรายการ");
        return;
    }
    //$('.spinner').show();
    $("body").mLoading({
        "text": 'กำลังทำการบันทึกข้อมูลกรุณารอชักครู่'
    });

    let ckbSetting = false
    if ($("#ckbSetting").prop("checked") != undefined) {
        ckbSetting = $("#ckbSetting").prop("checked");
    }

    PageMethods.saveData({
        year_id: parseInt($("#year_id").val()),
        term_id: $("#term_id").val(),
        Fd_NewTremStatus: ckbSetting,
        invoices_List: invoices_List
    }, $("#term_id").val(), ckbSetting,
        function (response) {
            //$("#submitpeak").show();
            setTimeout($("body").mLoading("hide"), 1000);
            getTuitionfeesetting($("#year_id").val(), $("#term_id").val());
            $("#modalpopup-data").modal("show").find("#message").html("บันทึกข้อมูลเรียบร้อยแล้ว");
            savedata = true;
            $("#submitpeak").removeClass("disabled");
        },
        function (e) {
            setTimeout($("body").mLoading("hide"), 1000);
            $("#modalpopup-data").modal("show").find("#message").html(
                "เกิดข้อผิดพลาดกรุณาลองใหม่ อีกครั้ง");
            console.log(e.StackTrace);
        }
    );
}

function addRules(control, messages) {
    $("#" + control).rules("add", {
        required: true,
        messages: {
            required: messages
        }
    });
}
//Setting Select In Table Rows 
function setting_select() {
    var row_last = $("#tableAddRow tbody tr:last");
    var group_select = row_last.find("[id*=group]");
    var sublevel_select = row_last.find("[id^=sublevel]");
    var class_select = row_last.find("[id^=class]");
    var Newsublevel_select = row_last.find("[id^=New_sublevel]");
    var Newclass_select = row_last.find("[id^=New_class]");
    var price_label = row_last.find("[id*=label]");

    select_settingdata(group_select, group_data, "เลือกลุ่มรายการ", "group_id", "group_name", "");
    select_settingdata(sublevel_select, sublevel_data, "เลือกขั้นปี", "sublevel_id", "sublevel_name", "");
    select_settingdata(Newsublevel_select, sublevel_data, "เลือกขั้นปี", "sublevel_id", "sublevel_name", "");

    sublevel_select.on("change", function () {
        getclass(sublevel_select, class_select, "0");
    });

    Newsublevel_select.on("change", function () {
        getclass(Newsublevel_select, Newclass_select, "0");
    });

    if ($("#tableAddRow tbody")[0].scrollHeight > 420) $("#tableAddRow tbody").scrollTop($(
        "#tableAddRow tbody")[0].scrollHeight);

    group_select.on("change", function () {
        var data = getObjects(group_data, "group_id", $(this).val());
        if ($(this).val() !== "" && data.length > 0) {
            price_label.number(data[0]["price"], 2);
        } else {
            price_label.number(0, 2);
        }
    });

    getclass(sublevel_select, class_select);
    row_last.find(".addBtnRemove").on("click", function () {
        $(this).closest('tr').remove();
    });
}

//Setting Class Select Control
function getclass(sublevel_select, class_select, class_vlaues) {
    if ($(sublevel_select).val() === "") {
        $("#" + $(class_select).attr("id") + " option").remove();

        $(class_select).append($("<option></option>")
            .attr("value", "0")
            .text("เลือกห้องทั้งหมด"));
        $(class_select).siblings().hide();
        $(class_select).show();
    } else {
        $(class_select).siblings('').show();
        $(class_select).hide();

        select_settingdata(class_select, getObjects(class_data, "level_id", $(sublevel_select).val()),
            "เลือกห้องทั้งหมด", "class_id", "class_name", "0");
        $(class_select).val(class_vlaues);
        data_completed += 1;
    }
}

//Setting Select 
function select_settingdata(control, data, text_default, display_value, display_text, value_default) {
    $("#" + $(control).attr("id") + " option").remove();
    if (text_default !== "") {
        $(control).append($("<option></option>")
            .attr("value", value_default)
            .text(text_default));
    }
    $.each(data, function (s, e) {
        $(control).append($("<option></option>")
            .attr("value", data[s][display_value])
            .text(data[s][display_text]));
    });

    $(control).siblings().css("display", "none");
    $(control).show();
}

function loadingdata() {
    if (data_length > data_completed) {
        setTimeout(loadingdata, 500);
    } else {
        console.log("completed");
    }
}

//Add Table Rows
function addTableRow() {
    let ckbSetting = false
    if ($("#ckbSetting").prop("checked") != undefined) {
        ckbSetting = $("#ckbSetting").prop("checked");
    }

    var tempTr = $(
        '<tr><td style="width: 5%" class="center"><input type="checkbox" value="1" class="form-check-input" checked /></td>' +
        '<td style="width: 10%"><select name="sublevel_' + i + '" id="sublevel_' + i +
        '" class="form-control" ><option value="">Loading . . .</option></select></td>' +
        '<td style="width: 10%" class="center"><i class="fa fa-refresh fa-spin"></i><select name="class_' +
        i + '" id="class_' + i +
        '" class="form-control" style="display:none;" ><option>Loading . . .</option></select></td>' +
        '<td style="width: 10%"><select name="group_' + i + '" id="group_' + i +
        '" class="form-control" ><option>Loading . . .</option></select></td>' +
        '<td class="right" style="width: 10%"><label id="label_' + i + '">0.00</label> บาท</td>' +
        '<td class="right" style="width: 10%"><input type="text" autocomplete="off" name="issuedDate_' +
        i +
        '" id="issuedDate_' + i + '" class="form-control datepicker" required /></td>' +
        '<td class="right" style="width: 10%"><input type="text" autocomplete="off" name="dueDate_' + i +
        '" id="dueDate_' +
        i + '" class="form-control datepicker" required /></td>' +

        '<td style="width: 10%;' + (ckbSetting ? "" : "display: none;") + '" class="setting"><select name="New_sublevel_' + i + '" id="New_sublevel_' + i +
        '" class="form-control" ><option value="">Loading . . .</option></select></td>' +
        '<td style="width: 10%;' + (ckbSetting ? "" : "display: none;") + '" class="center setting"><i class="fa fa-refresh fa-spin"></i><select name="New_class_' +
        i + '" id="New_class_' + i +
        '" class="form-control" style="display:none;" ><option>Loading . . .</option></select></td>' +

        '<td class="center" style="width: 10%; vertical-align: middle;"><span class="fa fa-times addBtnRemove" id="addBtn_' +
        i + '"></span>' +
        '<input name="item_id_' + i + '" id="item_id_' + i + '" value="0" type="hidden"/></td></tr>');
    $("#tableAddRow").append(tempTr);
    setting_select();
    i++;
    $(".datepicker").datepicker({
        minDate: new Date(),
        dateFormat: 'dd/mm/yy'
    });
    $(tempTr).find("input[type=checkbox]").change(function () {
        $("#tableAddRow thead input[type=checkbox]").prop("checked", $("#tableAddRow tbody input[type=checkbox]:checked").length === $("#tableAddRow tbody tr").length);
    });
    return tempTr;
}

//Get Data Tuitionfeesetting
function getTuitionfeesetting(year_id, term_id) {
    $("body").mLoading();
    PageMethods.getdata(year_id, term_id, function (response) {
        if (response.indexOf("{\"Message\":") != -1) {
            $("body").mLoading("hide");
            return;
        }
        console.log(response);
        let responseData = $.parseJSON(response).data;
        let _d1 = $.parseJSON(response);
        let ckbSetting = false;
        if (_d1.tremStatus != null) {
            ckbSetting = _d1.tremStatus;
        }

        $("#ckbSetting").prop("checked", ckbSetting);

        if ($("#ckbSetting").prop("checked")) {
            $(".setting").css("display", "");
        } else {
            $(".setting").css("display", "none");
        }

        setTimeout($("body").mLoading("hide"), 1000);
        $("#tableAddRow tbody tr").remove();
        i = 0;
        if (responseData.length > 0) {
            data_length = responseData.length;
            data_completed = 0;
            loadingdata();
            $.each(responseData, function (index, data) {
                savedata = true;
                $("#submitpeak").removeClass("disabled");
                var tr = addTableRow();
                tr.find("[id^=sublevel]").val(data.level_id);
                tr.find("[id^=New_sublevel]").val(data.levelNew_id);
                tr.find("[id*=group]").val(data.paygroup_id);
                tr.find("[id*=dueDate]").val(data.dueDate);
                tr.find("[id*=issuedDate]").val(data.issuedDate);
                tr.find("[id*=label]").number(data.price, 2);
                tr.find("[id*=sublevel]").prop('disabled', 'disabled');
                tr.find("[id*=class]").prop('disabled', 'disabled');
                tr.find("[id*=item_id]").val(data.id);
                getclass(tr.find("[id^=sublevel]"), tr.find("[id^=class]"), data.class_id);
                getclass(tr.find("[id^=New_sublevel]"), tr.find("[id^=New_class]"), data.classNew_id);
            });
        } else {
            $("#submitpeak").addClass("disabled");
            savedata = false;
            //$("#submitpeak").hide();
        }
    });
}