var _countingRow = $("#tb-products tbody tr").length + 1;
var defaultOptions;

var _currentPage = function () {

    function initSelect2Handle(target) {

        $(target).find(".ddl-product").select2({
            width: "100%",
            placeholder: "พิมพ์เพื่อค้นหา",
            allowClear: true,
            ajax: {
                url: searchProductUrl,
                type: 'POST',
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return {
                        term: params.term ? params.term : ""
                    };
                },
                processResults: function (response, page) {
                    var results = [];

                    response.Result.forEach(function (d, v) {

                        var o = {};
                        o.id = d.PaymentId;
                        o.full_name = d.Name;
                        o.value = d.PaymentId;
                        o.collection = d;
                        results.push(o);
                    });

                    return {
                        results: results
                    };
                },
                cache: true
            },
            escapeMarkup: function (markup) {
                return markup;
            }, // let our custom formatter work
            minimumInputLength: 0,
            templateResult: formatRepo,
            templateSelection: formatRepoSelection,
            "language": {
                "noResults": function () {
                    return "ไม่พบข้อมูล";
                },
                "inputTooShort": function () {
                    return "กรอกข้อมูลเพื่อค้นหา";
                }
            }
        });
        //.select2('val',null);
        // $(target).val([]).trigger('change');

        $(target).find(".ddl-product").on("select2:unselect", function (e) {
            $(e.target).closest("tr").find(".txt-product-price,.txt-product-discount").val("0.00");
            $(e.target).closest("tr").find(".txt-product-qty").val("1");
            $(e.target).closest("tr").find("input[name='InvoiceItems[][AccountingId]']").val("");
            $(e.target).closest("tr").find("input[name='InvoiceItems[][AccountingName]']").val("-");
        });

        $(target).find(".ddl-product").on('select2:select', function (e) {
            e.preventDefault();
            var data = e.params.data;
            $(e.target).closest("tr").find("input[name='InvoiceItems[][ProductAmount]']").val(data.collection.Price.toFixed(2));
            if (data.collection.Accounting !== null) {
                $(e.target).closest("tr").find("input[name='InvoiceItems[][AccountingName]']").val(data.collection.Accounting.AccountingName);
                $(e.target).closest("tr").find("input[name='InvoiceItems[][AccountingId]']").val(data.collection.Accounting.AccountingId);
            } else {
                $(e.target).closest("tr").find("input[name='InvoiceItems[][AccountingName]']").val("-");
                $(e.target).closest("tr").find("input[name='InvoiceItems[][AccountingId]']").val("");
            }
            calculateAmount();
        });

        $(target).find(".remove-product").click(function () {
            $(this).closest("tr").remove();
            calculateAmount();
        });
    }

    function initValidateProduct(target) {
        $(target).find(".ddl-product").rules("add", {
            required: true
        });
        $(target).find(".txt-product-qty").rules("add", {
            required: true,
            number: true,
            min: 1,
            digits: false
        });

        $(target).find(".txt-product-price").rules("add", {
            required: true,
            validatorNumber: true,
            //min: 0
        });

        $(target).find(".txt-product-price").focusout(function () {
            if ($.isNumeric($(this).val().replace(",", ""))) {
                $(this).val(parseFloat($(this).val().replace(",", "")).toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
            } else {
                $(this).val("0.00");
            }
        });

        $(target).find(".txt-product-qty").focusout(function () {
            if ($.isNumeric($(this).val())) {
                $(this).val(parseInt($(this).val()));
            } else {
                $(this).val(1);
            }
        });

        $(target).find(".txt-product-discount").focusout(function () {
            if ($.isNumeric($(this).val())) {
                $(this).val(parseFloat($(this).val()).toFixed(2));
            } else {
                $(this).val("0.00");
            }
        });

        $(target).find(".txt-product-price").keyup(function () {
            calculateAmount();
        });

        $(target).find(".txt-product-qty").keyup(function () {
            calculateAmount();
        });

        $(target).find(".txt-product-discount").keyup(function () {
            calculateAmount();
        });
    }

    function calculateAmount() {
        var sumDiscount = 0;
        var sumAmount = 0;
        var totalDiscount = 0;
        var totalAmount = 0;
        $.each($("#tb-products tbody tr"), function (index, row) {
            var currentPrice = $(row).find("input[name='InvoiceItems[][ProductAmount]']").val().replace(',', '');
            var currentQty = $(row).find("input[name='InvoiceItems[][Qty]']").val();
            var currentDiscount = $(row).find("input[name='InvoiceItems[][Discount]']").val().replace(',', '');

            if ($.isNumeric(currentPrice) && $.isNumeric(currentQty)) {
                sumAmount += (parseFloat(currentPrice) * parseInt(currentQty));

                if ($.isNumeric(currentDiscount) && parseFloat(currentDiscount) >= 0)
                    sumDiscount += parseFloat(currentDiscount);
            }
        });

        if ($.isNumeric($("#ManualDiscount").val()))
            sumDiscount += parseFloat($("#ManualDiscount").val());

        $("#TotalDiscount").text(sumDiscount.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $("#AllAmount").text(sumAmount.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        $("#TotalAmount").text((sumAmount - sumDiscount).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ","));
    }

    function formatRepo(repo) {
        if (repo.loading) return repo.text;

        var markup = "<div class='select2-result-repository__title'>" + repo.full_name + "</div>";

        return markup;
    }

    function formatRepoSelection(repo) {
        return repo.full_name || repo.text;
    }

    function appendItem() {
        var itemTemplate = " \
                <tr style =\"display: table-row;\">   \
                    <td class=\"drag-icon ui-sortable-handle\" align=\"center\">    \
                        <input type=\"hidden\" name=\"InvoiceItems[][InvoiceItemId]\" value=\"0\" /> \
                        <input type=\"hidden\" name=\"InvoiceItems[][InvoiceId]\" value=\"1\" /> \
                        <img src=\"/Assets/images/img-drag-icon.png\" />   \
                    </td>   \
                    <td>   \
                        <div class=\"form-group\" style=\"margin: 0\"> \
                            <select class=\"form-control select2 ddl-product\" style=\"text-align: left\"  name=\"InvoiceItems[][PaymentId]\" required=\"required\">   \
                            </select>   \
                        </div> \
                    </td>   \
                    <td>   \
                     <input type=\"hidden\" class=\"form-control txt-product-accounting-id\" name=\"InvoiceItems[][AccountingId]\" /> \
                    <input type=\"text\" class=\"form-control txt-product-accounting-name\" name=\"InvoiceItems[][AccountingName]\" disabled=\"disabled\" /> \
                    </td>   \
                    <td>   \
                        <div class=\"form-group\" style=\"margin: 0\"> \
                          <input type=\"text\" placeholder=\"0\" class=\"form-control price-box-input txt-product-qty\"  name=\"InvoiceItems[][Qty]\" value=\"1\" required=\"required\"  />   \
                        </div> \
                    </td>   \
                    <td>   \
                        <div class=\"form-group\" style=\"margin: 0\"> \
                            <input type=\"text\" placeholder=\"0.00\" class=\"form-control price-box-input txt-product-price\"   name=\"InvoiceItems[][ProductAmount]\" value=\"0.00\" required=\"required\" />   \
                        </div> \
                    </td>   \
                    <td>   \
                        <div class=\"form-group\" style=\"margin: 0\"> \
                            <input type=\"text\" placeholder=\"0.00\" class=\"form-control price-box-input txt-product-discount\" name=\"InvoiceItems[][Discount]\" />   \
                        </div> \
                    </td>   \
                    <td style=\"width: 2%\" align=\"center\">   \
                        <span class=\"glyphicon glyphicon-remove remove-product\"></span>   \
                    </td>   \
                </tr>   ";
        $("#tb-products tbody").append(itemTemplate);
        initSelect2Handle($("#tb-products tbody tr:last"));
        initValidateProduct($("#tb-products tbody tr:last"));
        _countingRow++;
    }

    var initPage = function () {

        function masterData() {
            $.ajax({
                url: termDataUrl,
                type: "GET",
                async: false,
                data: "entity=term",
                success: function (result) {
                    $.each(result.Result, function (index, item) {
                        var newOption = new Option(item.Year.Number + "/" + item.sTerm, item.TermId, false, false);
                        $("#ddl-term").append(newOption).trigger('change');
                    });
                }
            });
        }

        $("#ddl-user").select2({
            width: "100%",
            placeholder: "พิมพ์เพื่อค้นหา",
            ajax: {
                url: searchUserUrl,
                dataType: 'json',
                type: "GET",
                delay: 250,
                data: function (params) {
                    return {
                        term: params.term
                    };
                },
                processResults: function (response, page) {
                    var results = [];

                    response.Result.forEach(function (d, v) {

                        var o = {};
                        o.id = d.UserId;
                        o.full_name = d.FullName;
                        o.value = d.UserId;
                        o.collection = d;
                        results.push(o);
                    });

                    return {
                        results: results
                    };
                },
                cache: true
            },
            escapeMarkup: function (markup) {
                return markup;
            }, // let our custom formatter work
            minimumInputLength: 1,
            templateResult: formatRepo,
            templateSelection: formatRepoSelection,
            "language": {
                "noResults": function () {
                    return "ไม่พบข้อมูล";
                },
                "inputTooShort": function () {
                    return "กรอกข้อมูลเพื่อค้นหา";
                }
            }
        });

        $("#ddl-user").on('select2:select', function (e) {
            var data = e.params.data;
            //   $("#addr-detail").text(e.params.data.collection.Address);
        });

        $("#btn-add-item").click(function () {
            appendItem();
        });


        $(document).on('show.bs.modal', '.modal', function () {
            var zIndex = 900 + (10 * $('.modal:visible').length);
            $(this).css('z-index', zIndex);
            setTimeout(function () {
                $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
            }, 0);
        });

        $("#ManualDiscount").focusout(function () {
            if ($.isNumeric($(this).val())) {
                $(this).val(parseFloat($(this).val()));
            } else {
                $(this).val("");
            }
        });

        $("#btnDraft,#btn-save-approve,#btn-cancel-invoice").click(function () {
            var status = $(this).data('status');
            $.confirm({
                title: '<h3>แจ้งเตือน</h3>',
                content: '<h3>กรุณายืนยันเพื่อบันทึกข้อมูล</h3>',
                theme: 'bootstrap',
                buttons: {
                    "<span style=\"font-size: 20px;\">ปิด</span>": {
                        btnClass: 'btn-default',
                        action: function () {
                        }
                    },
                    "<span style=\"font-size: 20px;\">ยืนยัน</span>": {
                        btnClass: 'btn-primary',
                        action: function () {
                            $("#InvoiceStatus").val(status);
                            $('#invoice-form').submit();
                        }
                    }
                }
            });
        });

        $("#btn-delete-invoice").click(function () {
            var status = $(this).data('status');
            $.confirm({
                title: '<h3>แจ้งเตือน</h3>',
                content: '<h3>กรุณายืนยันเพื่อลบข้อมูล</h3>',
                theme: 'bootstrap',
                buttons: {
                    "<span style=\"font-size: 20px;\">ปิด</span>": {
                        btnClass: 'btn-default',
                        action: function () {
                        }
                    },
                    "<span style=\"font-size: 20px;\">ยืนยัน</span>": {
                        btnClass: 'btn-primary',
                        action: function () {
                            $("#InvoiceStatus").val(status);
                            $('#invoice-form').submit();
                        }
                    }
                }
            });
        });

        $("#spanBtnCancel").click(function () {
            $.confirm({
                title: '<h3>แจ้งเตือน</h3>',
                content: '<h3>คุณต้องการยกเลิกการทำรายการ ?</h3>',
                theme: 'bootstrap',
                buttons: {
                    "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                    },
                    "<span style=\"font-size: 20px;\">ยืนยัน</span>": function () {
                        window.location = "/Modules/Invoices/Index.aspx";
                    }
                }
            });
        });

        $("#btn-back").click(function () {
            window.location = "/Modules/Invoices/Index.aspx";
        });

        $("#ManualDiscount").keyup(function () {
            calculateAmount();
        });

        $(".date-single").daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            locale: {
                applyLabel: 'เลือก',
                cancelLabel: 'ล้าง',
                fromLabel: 'จาก',
                toLabel: 'ถึง',
                customRangeLabel: 'Custom',
                daysOfWeek: ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'],
                monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
                firstDay: 1,
                format: 'DD/MM/YYYY'
            }
        }, function (start, end, label) {
        });

        masterData();
        initSelect2Handle($("#tb-products"));

        if (_isLocked) {
            $.ajax({
                url: paidHistory,
                type: "POST",
                data: "InvoiceId=" + $("#InvoiceId").val(),
                dataType: "json",
                success: function (response) {
                    var coutingPaid = 1;
                    var buttonInvoice = "<div class=\"btn-group\"> \
                        <a  href=\"/Modules/Invoices/perRegisterPrintBillPreview.aspx?InvoiceId={invoiceId}&paidPaymentId={paidPaymentId}\" target=\"_blank\" class=\"btn btn-primary  btn-lg\">ออกใบเสร็จ</a>\
                                    <button style=\"height:46px\" type =\"button\" class=\"btn btn-primary btn-lg dropdown-toggle\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\"> \
                                        <span class=\"caret\"></span>\
                                            <span class=\"sr-only\">Toggle Dropdown</span>\
                                             </button >\
                                                <ul class=\"dropdown-menu\">\
                                                    <li> <a href=\"/Modules/Invoices/perRegisterPrintBillPreview.aspx?InvoiceId={invoiceId}&paidPaymentId={paidPaymentId}\" target=\"_blank\" style=\"font-size:18px\">ออกใบเสร็จ</a></li>\
                                                    <li> <a href=\"#\" onclick=\"voidPaidPayment({invoiceId},{paidPaymentId});return false;\" style=\"font-size:18px\">Void</a></li>\
                                                </ul>\
                                </div>";

                    //var tableHistory = "\
                    //    <div class=\"tb-paid-history white-card col-md-12\"> \
                    //    <hr/> \
                    //        <div class=\"col-md-10\" style=\"border-right: 1px solid #DDDDDD;\"> \
                    //            <p><b>รับชำระเงินครั้งที่ {countingPaid}</b> เมื่อวันที่ <input type=\"text\" value=\"{PaidDate}\" class=\"form-control\" style=\"width:150px;display:inline\" disabled/> \
                    //            <b>ผู้รับชำระ</b> <input type=\"text\" value=\"{Payee}\" class=\"form-control\" style=\"width:200px;display:inline\" disabled/> </p >\
                    //            <p> <b>ช่องทางการชำระเงิน</b> <input type=\"text\" value=\"{PaymentMethod}\" class=\"form-control\" style=\"width:250px;display:inline\" disabled/>\
                    //               <b>หมายเหตุ</b> <input type=\"text\" value=\"{Note}\" rows=\"3\"  columns=\"5\" class=\"form-control\" style=\"width:250px;display:inline\" disabled/>\ </p >\
                    //            <table id=\"tb-payment-detail-{TBID}\" class=\"col-md-8\"> \
                    //               <tr><th class=\"col-md-1\"></th><th class=\"col-md-4\">สินค้า/บริการ</th><th class=\"col-md-4\">ยอดชำระ</th></tr>\
                    //            </table> \
                    //         </div> \
                    //        <div class=\"col-md-2\"> \
                    //             <ul class=\"invoice-result-box\"> \
                    //                <li class=\"total-lst\">รับชำระเงินรวม <div class=\"line-payment-summary\"></div></li> \
                    //                <li> \
                    //                    <strong>{PaidTotal}</strong><span><strong> บาท</strong></span>\
                    //                    <div class=\"line-payment-summary\"></div>\
                    //                    <div class=\"clear\"></div>\
                    //                    <div class=\"line-payment-summary\"></div>\
                    //                </li>\
                    //            </ul >\
                    //             <div class=\"text-right\"><br/>{buttonInvoice}</div>\
                    //         </div> \
                    //    </div> \
                    //";

                    var tableHistory = "\
                        <div class=\"tb-paid-history white-card col-md-12\"> \
                        <hr/> \
                            <div class=\"col-md-10\" style=\"border-right: 1px solid #DDDDDD;\"> \
                                <p><b>รับชำระเงินครั้งที่ {countingPaid}</b> เมื่อวันที่ <input type=\"text\" value=\"{PaidDate}\" class=\"form-control\" style=\"width:150px;display:inline\" disabled/> \
                                <b>ผู้รับชำระ</b> <input type=\"text\" value=\"{Payee}\" class=\"form-control\" style=\"width:200px;display:inline\" disabled/> </p >\
                                <p> <b>ช่องทางการชำระเงิน</b> <input type=\"text\" value=\"{PaymentMethod}\" class=\"form-control\" style=\"width:250px;display:inline\" disabled/>\
                                <table id=\"tb-payment-detail-{TBID}\" class=\"col-md-8\"> \
                                   <tr><th class=\"col-md-1\"></th><th class=\"col-md-4\">สินค้า/บริการ</th><th class=\"col-md-4\">ยอดชำระ</th></tr>\
                                </table> \
                             </div> \
                            <div class=\"col-md-2\"> \
                                 <ul class=\"invoice-result-box\"> \
                                    <li class=\"total-lst\">รับชำระเงินรวม <div class=\"line-payment-summary\"></div></li> \
                                    <li> \
                                        <strong>{PaidTotal}</strong><span><strong> บาท</strong></span>\
                                        <div class=\"line-payment-summary\"></div>\
                                        <div class=\"clear\"></div>\
                                        <div class=\"line-payment-summary\"></div>\
                                    </li>\
                                </ul >\
                                 <div class=\"text-right\"><br/>{buttonInvoice}</div>\
                             </div> \
                        </div> \
                    ";

                    var rowPayment = " \
                        <tr>\
                        <td style=\"padding:5px\"><div class=\"circle-payment circle-payment-orange\">{rowCounting}</div><div class=\"line-circle line-payment-orange\"></div></td>\
                        <td style=\"padding:5px\"><input type=\"text\" value=\"{ProductName}\" class=\"form-control\"  disabled/></td>\
                        <td style=\"padding:5px\"><input type=\"text\" value=\"{PaidAmount}\" class=\"form-control\" disabled/></td>\
                        </tr> ";
                    if (response.Result !== null) {
                        $.each(response.Result, function (index, item) {
                            var tempHtml = tableHistory;
                            var rowCounting = 1;
                            tempHtml = tempHtml.replace("{countingPaid}", coutingPaid);
                            tempHtml = tempHtml.replace("{TBID}", coutingPaid);
                            tempHtml = tempHtml.replace("{PaidDate}", item.PaymentDateString);
                            tempHtml = tempHtml.replace("{Payee}", item.Payee);
                            tempHtml = tempHtml.replace("{Note}", item.Note === undefined ? "" : item.Note);
                            tempHtml = tempHtml.replace("{PaymentMethod}", item.Payments.length === 0 ? "" : item.Payments[0].PaymentMethod);
                            tempHtml = tempHtml.replace("{PaidTotal}", item.Amount.toLocaleString("en-US", { maximumFractionDigits: 2, minimumFractionDigits: 2 }));

                            if (item.Status !== "Void") {
                                tempHtml = tempHtml.replace("{buttonInvoice}", buttonInvoice);
                                tempHtml = tempHtml.replace(/\{paidPaymentId}/g, item.PaidPaymentId);
                                tempHtml = tempHtml.replace(/\{invoiceId}/g, item.InvoiceId);
                            }

                            tempHtml = tempHtml.replace("{buttonInvoice}", '<h1 style="color:red">การชำระเงินถูกยกเลิก</h1>');

                            $("#paid-payment-history").append(tempHtml);
                            $.each(item.Payments, function (index2, payment) {
                                var tempRow = rowPayment;
                                tempRow = tempRow.replace("{rowCounting}", rowCounting);
                                tempRow = tempRow.replace("{ProductName}", payment.ProductName);
                                tempRow = tempRow.replace("{PaidAmount}", payment.Amount.toLocaleString("en-US", { maximumFractionDigits: 2, minimumFractionDigits: 2 }));
                                $("#tb-payment-detail-" + coutingPaid + " tbody").append(tempRow);
                                rowCounting++;
                            });
                            coutingPaid++;
                        });
                    }
                }
            });
        }

        $.fn.modal.Constructor.prototype.enforceFocus = function () { };
        $('select').on('select2:open', function (e) {
            $('.custom-dropdown').parent().css('z-index', 1050);
        });
    };

    var formValidation = function () {

        var mainForm = $('#invoice-form');
        mainForm.validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: true, // do not focus the last invalid input
            ignore: "",  // validate all fields including form hidden input          
            errorPlacement: function (error, element) {
                if ($(element).data('show-error') && $(element).data('show-error') === true)
                    error.appendTo(element.closest('.form-group'));
            },
            invalidHandler: function (event, validator) { //display error alert on form submit         
                //App.scrollTo(error1, -200);
            },

            highlight: function (element) { // hightlight error inputs
                $(element).closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.form-group').removeClass('has-error'); // set error class to the control group
            },
            success: function (label) {
                label.closest('.form-group').removeClass('has-error'); // set success class to the control group
            },

            submitHandler: function (form) {
                var data = mainForm.serializeJSON();

                if (data.InvoiceId == 0 && (data.InvoiceItems === undefined || data.InvoiceItems === null || data.InvoiceItems.length === 0)) {
                    $.confirm({
                        title: '<h3>แจ้งเตือน</h3>',
                        content: '<h3>กรุณาเลือกข้อมูลสินค้า</h3>',
                        theme: 'bootstrap',
                        buttons: {
                            "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                            }
                        }
                    });
                    return false;
                }

                $('#invoice-form :disabled').each(function (index, formDisabledElement) {
                    if (!(formDisabledElement.name in data))
                        data[formDisabledElement.name] = $(formDisabledElement).val();
                });

                $.ajax({
                    url: saveUrl,
                    type: 'POST',
                    data: { invoice: JSON.stringify(data) },
                    dataType: "json",
                    success: function (data, textStatus, xhr) {
                        $.confirm({
                            title: '<h3>แจ้งเตือน</h3>',
                            content: '<h3>บันทึกข้อมูลสำเร็จ</h3>',
                            theme: 'bootstrap',
                            buttons: {
                                "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                                    window.location = "/Modules/Invoices/Index.aspx";
                                }
                            }
                        });
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.log(textStatus);
                        $.alert({
                            title: '<h3>แจ้งเตือน</h3>',
                            content: '<h3>พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ </h3>',
                            theme: 'bootstrap',
                            buttons: {
                                "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                                }
                            }
                        });
                    }
                });
                return false;
            }
        });
        $("#ddl-user").rules("add", {
            messages: {
                required: "กรุณาเลือกผู้จ่ายเงิน"
            }
        });
        initValidateProduct($("#tb-products"));
    };

    var modelInit = function () {
        if (_invoice !== null && _invoice.InvoiceId !== 0) {
            generateMainDetail();
            if (_invoice.InvoiceItems !== null) {
                generateItem();
            }

            if (_isLocked) {
                $("#invoice-form button,.remove-product").hide();
                $("#btn-back").show();
                $("#invoice-form input,select,textarea").prop('disabled', true);
            } else {
                $("#btn-back").hide();
            }
        }

        function generateMainDetail() {
            if (typeof _invoice.StudentName !== "undefined" && _invoice.StudentName !== null) {
                var newStudentOption = new Option(_invoice.StudentName, _invoice.StudentId, false, false);
                $("#ddl-user").append(newStudentOption).trigger('change');
            }
            if (typeof _invoice.TermId !== "undefined" && _invoice.TermId !== null) {
                $("#ddl-term").val(_invoice.TermId);
            }
            $("#ManualDiscount").val(_invoice.ManualDiscount.toFixed(2));
            $("#Description").val(_invoice.Description);
        }

        function generateItem() {
            $.each(_invoice.InvoiceItems, function (index, item) {
                appendItem();
                var lastRow = $("#tb-products tbody tr:last");
                var newProductOption = new Option(item.ProductName, item.PaymentId, false, false);
                $(lastRow).find(".ddl-product").append(newProductOption).trigger('change');
                $(lastRow).find("[name='InvoiceItems[][InvoiceItemId]']").val(item.InvoiceItemId);
                $(lastRow).find("[name='InvoiceItems[][InvoiceId]']").val(item.InvoiceId);
                $(lastRow).find(".txt-product-accounting-id").val(item.AccountingId);
                $(lastRow).find(".txt-product-accounting-name").val(item.AccountingName);
                $(lastRow).find(".txt-product-qty").val(item.Qty.toFixed(2));
                $(lastRow).find(".txt-product-price").val(item.ProductAmount.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
                $(lastRow).find(".txt-product-discount").val(item.Discount.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 }));

                if (item.OutstandingAmount != item.GrandTotal) {
                    $(lastRow).find(".remove-product").hide();
                    $(lastRow).find("input,select,textarea").prop('disabled', true);
                }
            });
            calculateAmount();
        }
    };

    return {
        init: function () {
            initPage();
            formValidation();
            modelInit();
        }
    };
}();

$(function () {
    jQuery.validator.addMethod("validatorNumber", function (value, element) {
        let _val = $(element).val().replace(',', '');
        if ($.isNumeric(_val)) {
            $(element).val(parseFloat(_val).toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
            return true;
        } else {
            $(element).val("0.00");
            return false;
        }
    }, "* Amount must be greater than zero");
    _currentPage.init();
});

function voidPaidPayment(invoiceId, paidPaymentId) {

    $.confirm({
        title: '<h3>แจ้งเตือน</h3>',
        content: '<h3>กรุณายืนยันการยกเลิกการชำระเงิน</h3>',
        theme: 'bootstrap',
        buttons: {
            "<span style=\"font-size: 20px;\">ปิด</span>": function () {
            },
            confirm: {
                text: 'ยืนยัน',
                btnClass: 'btn-primary font-size-20',
                action: function () {
                    $.ajax({
                        url: paidUpdateToVoidUrl,
                        type: "POST",
                        data: {
                            invoiceId: invoiceId,
                            paidPaymentId: paidPaymentId
                        }, success: function (resp) {
                            var message = "บันทึกข้อมูลสำเร็จ";
                            var endAction = function () {
                                location.reload();
                            };

                            if (!resp.Result) {
                                message = resp.Message;
                                endAction = function () {
                                    location.reload();
                                };
                            }

                            $.confirm({
                                title: '<h3>แจ้งเตือน</h3>',
                                content: '<h3>' + message + '</h3>',
                                theme: 'bootstrap',
                                buttons: {
                                    "<span style=\"font-size: 20px;\">ปิด</span>": endAction
                                }
                            });
                        }
                    });
                }
            }
        }
    });
    return false;
}


$(document)
    .ajaxStart(function () {
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
    })
    .ajaxStop($.unblockUI);