var availableValuestudent = [];

//$(document)
//    .ajaxStart(function () {
//        $('#loading').show();
//    })
//    .ajaxStop(function () {
//        $('#loading').hide();
//    });

$(function () {

    //$('.datepicker').datepicker({ dateFormat: "dd/mm/yy", navigationAsDateFormat: true, showOtherMonths: true });

    //getlistemp();

    //$('#txtname').autocomplete({
    //    width: 300,
    //    max: 10,
    //    delay: 100,
    //    minLength: 1,
    //    autoFocus: true,
    //    cacheLength: 1,
    //    scroll: true,
    //    highlight: false,
    //    //source: function (request, response) {
    //    //    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
    //    //    response(results.slice(0, 10));
    //    //},
    //    source: lightwell,
    //    select: function (event, ui) {
    //        event.preventDefault();
    //        $("#txtname").val(ui.item.label);
    //        $("#empid").val(ui.item.value);
    //        $("#txtPhone").val(ui.item.phone);
    //        $phone = ui.item.phone;
    //    },
    //    focus: function (event, ui) {
    //        event.preventDefault();
    //        $("#empid").val("");
    //        $("#txtPhone").val("");
    //    }
    //});

    $('.datepicker').datetimepicker({
        format: 'DD/MM/YYYY-BE',
        locale: 'th',
        debug: false,
        //defaultDate: "<%=DateTime.Now.ToString("dd/MM/yyyy") %>",
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

    $(".datepicker").keydown(function (e) {
        e.preventDefault();
    });
   
});


function getlistemp() {
    availableValuestudent = [];
    $.ajax({
        url: "/UpdateEmpStatus/LogicUpdateEmpStatus.ashx?mode=GetEmpList",
        dataType: "json",
        success: function (objjson) {
            //console.log(objjson);
            $.each(objjson, function (index) {
                var newObject = {
                    label: objjson[index].Name,
                    value: objjson[index].EmpId,
                    phone: objjson[index].Phone
                };
                availableValuestudent[index] = newObject;
            });
        }
    });
}

function lightwell(request, response) {
    function hasMatch(s) {
        if (s === null) s = "";
        return s.toLowerCase().indexOf(request.term.toLowerCase()) !== -1;
    }
    var i, l, obj, matches = [];

    if (request.term === "") {
        response([]);
        return;
    }

    for (i = 0, l = availableValuestudent.length; i < l; i++) {
        obj = availableValuestudent[i];
        if (hasMatch(obj.label) || hasMatch(obj.phone)) {
            matches.push(obj);
        }
    }
    response(matches.slice(0, 10));
}

function CheckVal() {

}



function GetEmpData() {

    if (TAC.GetUserID() == "") {
        Swal.fire({
            type: 'error',
            title: 'ขออภัยค่ะ',
            text: 'กรุณากรอกข้อมูลบุคลากรที่ต้องการค้นหา'
        });
        return;       
    }

    //var date = new Date();
    //var day = date.getDate();
    //if (day <= 9) day = '0' + day;
    //var month = date.getMonth() + 1;
    //if (month <= 9) month = '0' + month;
    //var toDay = day + "/" + month + "/" + date.getFullYear();

    //$("#txtdaystart").val(toDay);
    //$("#txtdayend").val(toDay);

    //if ($('#txtNameDetail').html() === "") {
    $(".btn2").prop("disabled", false);
    //    document.getElementById("btnStatus").disabled = true;

    //}

    EmpDetail();
    //EmpScanDeatail();
   
}

function EmpDetail() {
    var empId = TAC.GetUserID();//$('#empid').val();

    $.ajax({
        url: "/UpdateEmpStatus/LogicUpdateEmpStatus.ashx?mode=GetData&empId=" + empId,
        success: function (msg) {
            //console.log(msg);
            $('#txtNameDetail').html(msg[0].Name);
            $('#txtType').html(msg[0].cType);
            $('#txtJob').html(msg[0].Job);
            $('#txtDepart').html(msg[0].Depa);
            $('#txtTimeType').html(msg[0].TimeType);
            $('#txtBirthday').html(msg[0].Birthday);
            document.getElementById("empImg").style.display = "block";
            document.getElementById("mokPic").style.display = "none";
            document.getElementById("empImg").src = msg[0].Pic;

        }
    });
}

function EmpScanDeatail() {
    var empId = TAC.GetUserID();//$('#empid').val();

    $.ajax({
        url: "/UpdateEmpStatus/LogicUpdateEmpStatus.ashx?mode=EmpWorkScan&empId=" + empId,
        success: function (msg) {
            //console.log(msg);
        }
    });
}

function EmpUpdateStatus() {

    if ($('#aspnetForm').valid() == false) {
        //e.preventDefault();
        //e.stopPropagation();
        return false;
    }

    //if ($("#txtname").val() === "" || $("#empid").val() === "" ) {
    if (TAC.GetUserID() == '') {
        Swal.fire({
            type: 'error',
            title: 'ขออภัยค่ะ',
            text: 'กรุณากรอกข้อมูลบุคลากรที่ต้องการค้นหา'
        });
        return;
    }

    
    var empId = TAC.GetUserID();
    var typeScanTime = $('input[name=typeScanTime]:checked').val();
    var dateStart = $("#txtdaystart").data("DateTimePicker").date().format('DD/MM/YYYY');//$("#txtdaystart").val();
    var dateEnd = $("#txtdayend").data("DateTimePicker").date().format('DD/MM/YYYY');//$("#txtdayend").val();
    var status = $('#ddlStatus').val();

    $.ajax({
        url: "/UpdateEmpStatus/LogicUpdateEmpStatus.ashx?mode=EmpUpdateStatus&empId=" + empId
            + "&typeScanTime=" + typeScanTime
            + "&status=" + status
            + "&dateStart=" + dateStart
            + "&dateEnd=" + dateEnd,
        success: function (msg) {
            //$.confirm({
            //    title: false,
            //    content: '<img src="/images/checked.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h1 class="text-center">ทำการบันทึกข้อมูลเรียบร้อยแล้ว</h1>',
            //    theme: 'material',
            //    type: 'blue',
            //    buttons: {
            //        "<span style=\"font-size: 20px;\">ปิด</span>": {
            //            btnClass: 'btn-primary',
            //            keys: ['enter', 'shift'],
            //            action: function () {
            //            }
            //        }
            //    }
            //});

            Swal.fire({
                type: 'success',
                title: 'สำเร็จ',
                text: 'ทำการบันทึกข้อมูลเรียบร้อยแล้ว'
            });

            $("#txtname").val("");
            $("#empid").val("");
            $("#txtPhone").val("");
            $('#txtNameDetail').html("");
            $('#txtType').html("");
            $('#txtJob').html("");
            $('#txtDepart').html("");
            $('#txtTimeType').html("");
            $('#txtBirthday').html("");
            $("#txtdaystart").val("");
            $("#txtdayend").val("");
            $("input[name=typeScanTime][value='0']").prop('checked', true);
            $("#ddlStatus").val("0");
            document.getElementById("empImg").style.display = "none";
            document.getElementById("mokPic").style.display = "block";
        }
    });
    
}