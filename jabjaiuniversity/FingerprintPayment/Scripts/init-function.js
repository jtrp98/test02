
var initFunction = {
    setDropdown: function (obj, table, firstOption) {

        $.ajax({
            type: "GET",
            url: "/Handles/InitDropdownData.ashx",
            data: { table: table },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (result) {

                $(obj).empty();

                var options = firstOption;
                for (var i = 0; i < result.length; i++) {

                    options += '<option value="' + result[i].id + '">' + result[i].value + '</option>';

                }

                $(obj).html(options);
            }
        });

    }
}

var systemMessage = {
    LimitInContact: function (r) {
        swal.fire({
            title: '',
            html: `
ไม่สามารถเพิ่มข้อมูลนักเรียน เนื่องจากเกินจำนวนที่ระบบกำหนด
<br/>
หมายเหตุ: จำนวนที่กำหนดทั้งหมด `+ r.data.limitInContact + ` คน` + (r.data.excessNumber > 0 ? `, ตรวจพบจำนวนนักเรียนเกินกำหนด ` + r.data.excessNumber + ` ราย` : ``),
            type: 'warning',
            confirmButtonClass: 'btn btn-danger',
            confirmButtonText: '<i class="fa fa-times"></i> ปิด',
            buttonsStyling: false
        });
    }
}