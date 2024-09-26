$(function () {
    var validator = $("#aspnetForm").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side
            txtsProduct: { required: true },
            txtsBarCode: { required: true },
            txtnCost: { required: true, number: true },
            txtPrice: { required: true, number: true }
        },
        // Specify validation error messages
        messages: {
            txtsProduct: { required: "กรุณากรอกชื่อสินค้า" },
            txtsBarCode: { required: "กรุณากรอกรหัสบาร์โค้ด" },
            txtnCost: { required: "กรุณากรอกราคาต้นทุน", number: "กรุณากรอกตัวเลข" },
            txtPrice: { required: "กรุณากรอกราคาขาย", number: "กรุณากรอกตัวเลข" }
        },
        tooltip_options: {
            txtsProduct: { placement: 'right', html: true },
            txtsBarCode: { placement: 'right', html: true },
            txtnCost: { placement: 'right', html: true },
            txtPrice: { placement: 'right', html: true }
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            var stock = $("#chkStock").prop("checked") === true ? 1 : 0;
            var newObject = {
                mode: "product",
                productid: $("input[id*=productid]").val(),
                productname: $("input[id*=txtsProduct]").val(),
                barCode: $("input[id*=txtsBarCode]").val(),
                cost: $("input[id*=txtnCost]").val(),
                price: $("input[id*=txtPrice]").val(),
                number: 0,
                type: $("select[id*=ddlnType] option:selected").val(),
                warn: $("input[id*=txtnWarn]").val() === "" ? "0" : $("input[id*=txtnWarn]").val(),
                stock: stock,
                unit : $("select[id*=ddlUnit] option:selected").val(),
            };
            //$.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
            $("body").mLoading('show');
            //$('#modalpopproduct').modal('toggle');
            $.post("/App_Logic/insertDataJSON.ashx", { newObject }, function (mgs, status) {
                /* $.unblockUI();*/
                $("body").mLoading('hide');
            }).done(function (data, statusText, xhr) {
                /*$.unblockUI();*/
                $("body").mLoading('hide');
                //console.log(xhr.status);
                if (data.status === 403) {
                    Swal.fire({
                        type: 'error',
                        title: 'บันทึกไม่สำเร็จ',
                        text: "ท่านได้ทำการเพิ่มรหัสบาร์โค้ดนี้ไปแล้ว",
                    });
                    //$.alert({
                    //    title: '<h3>แจ้งเตือน</h3>',
                    //    content: '<h3>ท่านได้ทำการเพิ่มรหัสบาร์โค้ดนี้ไปแล้ว</h3>',
                    //    theme: 'bootstrap',
                    //    buttons: {
                    //        "<span style=\"font-size: 20px;\">Close</span>": function () {
                    //            //$.unblockUI();
                    //            $("body").mLoading('hide');
                    //        }
                    //    }
                    //});
                }
                else {
                    $('#modalpopproduct').modal('toggle');
                    Swal.fire({
                        type: 'success',
                        title: 'บันทึกสำเร็จ',
                    });
                    SearchData('data');
                }
                //loaddata();
            }).fail(function (xhr) {
                // $.unblockUI();
                $("body").mLoading('hide');
                //console.log(xhr.status);
                if (xhr.status === 403) {
                    Swal.fire({
                        type: 'error',
                        title: 'บันทึกไม่สำเร็จ',
                        text: "ท่านได้ทำการเพิ่มรหัสบาร์โค้ดนี้ไปแล้ว",
                    });
                    //$.alert({
                    //    title: '<h3>แจ้งเตือน</h3>',
                    //    content: '<h3>ท่านได้ทำการเพิ่มรหัสบาร์โค้ดนี้ไปแล้ว</h3>',
                    //    theme: 'bootstrap',
                    //    buttons: {
                    //        "<span style=\"font-size: 20px;\">Close</span>": function () {
                    //            $('#modalpopproduct').modal('toggle');
                    //        }
                    //    }
                    //});
                }
            });
        }
    });

    $("#btnCancel").click(function () {
        $('#modalpopproduct').modal('toggle');
        validator.resetForm();
    });
});

function getproductdata(productid) {
    //$.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
    $("body").mLoading('show');
    $.get("/App_Logic/dataJSon.ashx?mode=product&id=" + productid, "", function (Result) {
        $.each(Result, function (index) {
            //$.unblockUI();
            $("body").mLoading('hide');
            $("#headerpopup").html("แก้ไขข้อมูล");
            $("input[id*=productid]").val(Result[index].productid);
            $("input[id*=txtsProduct]").val(Result[index].productname);
            $("input[id*=txtsProduct]").val(Result[index].productname);
            $("input[id*=txtsBarCode]").val(Result[index].barCode);
            $("input[id*=txtnCost]").val(Result[index].cost);
            $("input[id*=txtPrice]").val(Result[index].price);
            $("select[id*=ddlnType]").val(Result[index].type);
            $("input[id*=txtnWarn]").val(Result[index].warn);
            $("select[id*=ddlnType]").val(Result[index].type);
            $("select[id*=ddlUnit]").val(Result[index].unit),
            $("#chkStock").prop("checked", Result[index].stock === "1");
            $('.selectpicker').selectpicker('refresh');
            $("#txtnWarn").attr("disabled", Result[index].stock === "1");
            $("#modalpopproduct").modal("show");
        });
    });
}

function cleardata() {
    $("#headerpopup").html("เพิ่มข้อมูล");
    $("input[id*=productid]").val("");
    $("input[id*=txtsProduct]").val("");
    $("input[id*=txtsProduct]").val("");
    $("input[id*=txtsBarCode]").val("");
    $("input[id*=txtnCost]").val("");
    $("input[id*=txtPrice]").val("");
    $("input[id*=txtnNumber]").val("");
    $("input[id*=txtnWarn]").val("");
    $("select[id*=ddlUnit]").val('').selectpicker('refresh');
    $("select[id*=ddlnType]").val('').selectpicker('refresh');
   // $("select[id*=ddlnType]").prop('selectedIndex', 0);
    $("#chkStock").attr("checked", false);
}