var _currentPage = function () {
    var _countingRow = 1;

    var initSettinsBox = function () {
        $(".btnPrint").click(function () {
            $("#pageTemp").html('');
            $("#page1").clone().appendTo("#pageTemp");
            $("#pageTemp").find("input.chkShowProduct:not(:checked)").closest('tr').remove();
            $("#pageTemp").find("input:checkbox").remove();
            var contents = $("#pageTemp").html();
            var frame1 = document.createElement('iframe');
            frame1.name = "frame1";
            frame1.style.position = "absolute";
            frame1.style.top = "-1000000px";
            document.body.appendChild(frame1);
            var frameDoc = (frame1.contentWindow) ? frame1.contentWindow : (frame1.contentDocument.document) ? frame1.contentDocument.document : frame1.contentDocument;
            frameDoc.document.open();
            frameDoc.document.write("<html><meta http-equiv='cache-control' content='no-cache'><head><title></title>");
            frameDoc.document.write("<link rel=\"stylesheet\" href=\"/bootstrap%20SB2/bower_components/bootstrap/dist/css/bootstrap.css\" type=\"text/css\"/>");
            frameDoc.document.write("<script type=\"text/javascript\" src=\"/Scripts/jquery-1.10-2.min.js\" /></script>");
            frameDoc.document.write('</head><body>');
            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                document.body.removeChild(frame1);
            }, 500);
            return false;
        });

        $(".jabjai-toolbar-link").click(function () {
            $(".jabjai-right-panel").addClass("jabjai-panel--on");
            return false;
        });

        $(".jabjai-panel__close").click(function () {
            $(".jabjai-right-panel").removeClass("jabjai-panel--on");
            return false;
        });
    };


    var modelInit = function () {
        if (_invoice !== null && _invoice.InvoiceId !== 0) {
            //generateMainDetail();
            if (_invoice.InvoiceItems !== null) {
                generateItem();
            }

            $(".chk-all").change(function () {
                $(".chkShowProduct").prop('checked', $(this).is(":checked"));
                $(".chk-all").prop('checked', $(this).is(":checked"));
                calculateProduct();
            });

            $(".chkShowProduct").change(function () {
                $("input[data-row='" + $(this).data('row') + "']").prop('checked', $(this).is(":checked"));
                calculateProduct();
            });
        }

        function calculateProduct() {
            var grandTotal = 0;
            var outstandingamount = 0;
            $("#page1 #tb-products-1 input.chkShowProduct:checked").each(function (index, item) {
                grandTotal += parseFloat($(item).data('amount'));
                outstandingamount += parseFloat($(item).data('outstanding'));
            });

            $(".product_grand_total").text(grandTotal.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(".product_outstandingamount").text(outstandingamount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(".product_outstandingamountText").text("( " + ArabicNumberToText(outstandingamount.toLocaleString(undefined, { minimumFractionDigits: 2 })) + " )");
        }

        function appendItem() {
            var itemTemplate = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:21px;padding-left:5px;\">    \
                        <input type=\"checkbox\"  class=\"chkShowProduct\" data-row=\"chk-"+ _countingRow + "\" data-amount=\"\" data-outstanding=\"\" checked /> \
                       <span class=\"lb_product_name\" style=\"font-size:21px;\"> </span> \
                   </td >  \
                   <td style=\"border: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span class=\"lb_product_grand_total\"style=\"font-size:21px;\"> </span> \
                   </td>   \
                <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span  class=\"lb_product_outstandingamount\" style=\"font-size:21px;\">  </span> \
                   </td>   \
                </tr>   ";
            $("#tb-products-1 tbody,#tb-products-2 tbody").append(itemTemplate);
        }

        function generateItem() {
            var TotalAmount = 0;
            var TotalOutstandingAmount = 0;
            var maxRows = 8;

            $("#tb-products-1 thead,#tb-products-2 thead ").append("\<tr style=\"border:1px solid black;border-collapse: collapse;height:20px;\">\
                    <th style=\"border:1px solid black;border-collapse: collapse;width:50%;font-size:18px;padding-left:5px;\">\
                         <input type=\"checkbox\"  class=\"chk-all\" checked >รายการ</th>\
                    <th  style=\"text-align:center;border:1px solid black;border-collapse: collapse;width:25%;font-size:18px;\">ยอดชำระ</th>\
                    <th  style =\"text-align:center;border:1px solid black;border-collapse: collapse;width:25%;font-size:18px;\">\
                       <span class=\"span-paid\">รับชำระ</span>\
                    </th>\
                </tr>");

            $.each(_invoice.InvoiceItems, function (index, item) {
                appendItem();
                var lastRow = $("#tb-products-1 tbody tr:last,#tb-products-2 tbody tr:last");
                $(lastRow).find("input").data('amount', item.GrandTotal.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find("input").data('outstanding', item.OutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find(".lb_product_name").text(item.ProductName);
                $(lastRow).find(".lb_product_grand_total").text(item.GrandTotal.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find(".lb_product_outstandingamount").text(item.OutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                TotalAmount += item.GrandTotal;
                TotalOutstandingAmount += item.OutstandingAmount;
                _countingRow++;
                maxRows--;
            });


            while (maxRows > 0) {
                var emptyTeample = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse;height:23px\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:21px;padding:5px;\"> &nbsp   \
                   </td >  \
                   <td style=\"border: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">  &nbsp \
                   </td>   \
                <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">&nbsp   \
                   </td>   \
                </tr>   ";
                $("#tb-products-1 tbody,#tb-products-2 tbody").append(emptyTeample);
                _countingRow++;
                maxRows--;
            }

            var itemTemplate = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:21px;padding:5px;text-align:right;text-align:right;\">    \
                       <span  style=\"font-size:21px;\"> รวม</span> \
                   </td >  \
                   <td style=\"border-left: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span class=\"product_grand_total\"style=\"font-size:21px;\"> </span> \
                   </td>   \
                <td style=\"border-left: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span  class=\"product_outstandingamount\" style=\"font-size:21px;\">  </span> \
                   </td>   \
                </tr>\  ";

            var periodAmount = "\
                <tr style =\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" colspan=\"2\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
                       <span class=\"paid_period_number\" style=\"font-size:18px;\"></span> \
                   </td>  \
                <td style=\"border-left: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span  class=\"paid_period_amount\" style=\"font-size:18px;\">  </span> \
                   </td>   \
                </tr> \
                 <tr style =\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" colspan=\"2\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
                       <span  style=\"font-size:18px;\"> รวมค่าบริการ </span> \
                   </td>  \
                <td style=\"border-left: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span  class=\"paid_all_period_amount\" style=\"font-size:18px;\">  </span> \
                   </td>   \
                </tr>\
                <tr style =\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" colspan=\"2\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
                       <span  style=\"font-size:18px;\"> ส่วนลดรวม </span> \
                   </td>  \
                <td style=\"border-left: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span  class=\"paid_all_period_amount\" style=\"font-size:18px;\">  </span> \
                   </td>   \
                </tr>\
                <tr style =\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" colspan=\"2\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
                       <span  style=\"font-size:18px;\"> ยอดชำระแล้วทั้งสิ้น</span> \
                   </td>  \
                <td style=\"border-left: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span  class=\"paid_all_period_amount\" style=\"font-size:18px;\">  </span> \
                   </td>   \
                </tr>";

            $("#tb-products-1 tbody,#tb-products-2 tbody ").append(itemTemplate);
            var lastRow = $("#tb-products-1 tbody tr:last,#tb-products-2 tbody tr:last");
            $(lastRow).find(".product_grand_total").text(TotalAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(lastRow).find(".product_outstandingamount").text(TotalOutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(lastRow).find(".product_outstandingamountText").text("( " + ArabicNumberToText(TotalOutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 })) + " )");
            if (_hideCheckbox) {
                $("input:checkbox").remove();
                $(".span-paid").text("จำนวนเงินชำระ");

                $("#tb-products-1 tbody,#tb-products-2 tbody ").append(periodAmount);
                $(".paid_period_number").text("ชำระแล้วครั้งที่/Partial payment #" + _paidPeriod);
                $(".paid_period_amount").text(TotalOutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(".paid_all_period_amount").text(_totalAllPaid.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            }
        }
    };

    return {
        init: function () {
            modelInit();
            initSettinsBox();
        }
    };
}();

var _currentPage_type2 = function () {
    var _countingRow = 1;

    var initSettinsBox = function () {
        $(".jabjai-toolbar-link").click(function () {
            $(".jabjai-right-panel").addClass("jabjai-panel--on");
            return false;
        });

        $(".jabjai-panel__close").click(function () {
            $(".jabjai-right-panel").removeClass("jabjai-panel--on");
            return false;
        });
    };


    var modelInit = function () {
        if (_invoice !== null && _invoice.InvoiceId !== 0) {
            //generateMainDetail();
            if (_invoice.InvoiceItems !== null) {
                generateItem();
            }


            $(".chk-all").change(function () {
                $(".chkShowProduct").prop('checked', $(this).is(":checked"));
                $(".chk-all").prop('checked', $(this).is(":checked"));
                calculateProduct();
            });

            $(".chkShowProduct").change(function () {
                $("input[data-row='" + $(this).data('row') + "']").prop('checked', $(this).is(":checked"));
                calculateProduct();
            });
        }

        function calculateProduct() {
            var grandTotal = 0;
            var outstandingamount = 0;
            $("#page1 input.chkShowProduct:checked").each(function (index, item) {
                grandTotal += parseFloat($(item).data('amount').replace(',', ''));
                outstandingamount += parseFloat($(item).data('outstanding').replace(',', ''));
            });

            $(".product_grand_total").text(grandTotal.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(".product_outstandingamount").text(outstandingamount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
        }

        function appendItem() {
            var itemTemplate = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse\">   \
                   <td class=\"drag-icon ui-sortable-handle center\" style=\"font-size:21px;padding-left:5px;text-align:center;\"> "+ _countingRow + "  </td>\
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:21px;padding-left:5px;border: 1px solid black;border-right: 1px solid black;\">    \
                        <input type=\"checkbox\"  class=\"chkShowProduct\" data-row=\"chk-"+ _countingRow + "\" data-amount=\"\" data-outstanding=\"\" checked /> \
                       <span class=\"lb_product_name\" style=\"font-size:21px;\"> </span> \
                   </td >  \
                <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span  class=\"lb_product_outstandingamount\" style=\"font-size:21px;\">  </span> \
                   </td>   \
                </tr>   ";
            $("#tb-products-1 tbody,#tb-products-2 tbody").append(itemTemplate);
        }

        function generateItem() {
            var TotalAmount = 0;
            var TotalOutstandingAmount = 0;
            var maxRows = 9;

            //if (_invoice.ManualDiscount > 0) {
            //    maxRows = 7;
            //}

            $("#tb-products-1 ,#tb-products-2  ").append("\<tr style=\"border:1px solid black;border-collapse: collapse;height:20px;\">\
                    <th style=\"border:1px solid black;border-collapse: collapse;width:5%;font-size:18px;padding-left:5px;text-align:center;\" > ลำดับ </th>\
                    <th style=\"border:1px solid black;border-collapse: collapse;width:70%;font-size:18px;padding-left:5px;\">\
                         <input type=\"checkbox\"  class=\"chk-all\" checked >รายการ</th>\
                    <th  style=\"text-align:center;border:1px solid black;border-collapse: collapse;width:25%;font-size:18px;\">\
                       <span class=\"span-paid\">รับชำระ</span>\
                    </th>\
                </tr>");
            $.each(_invoice.InvoiceItems, function (index, item) {
                appendItem();
                let lastRow = $("#tb-products-1 tbody tr:last,#tb-products-2 tbody tr:last");
                $(lastRow).find("input").data('amount', item.GrandTotal.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find("input").data('outstanding', item.OutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find(".lb_product_name").text(item.ProductName);
                $(lastRow).find(".lb_product_grand_total").text(item.GrandTotal.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find(".lb_product_outstandingamount").text(item.OutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                TotalAmount += item.GrandTotal;
                TotalOutstandingAmount += item.OutstandingAmount;
                _countingRow++;
                maxRows--;
            });

            if (_invoice.paid_Discount ?? 0 > 0) {
                appendItem();
                let paid_Discount = _invoice.paid_Discount * -1;
                let lastRow = $("#tb-products-1 tbody tr:last,#tb-products-2 tbody tr:last");
                $(lastRow).find("input").data('amount', paid_Discount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find("input").data('outstanding', paid_Discount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find(".lb_product_name").text('ส่วนลด');
                $(lastRow).find(".lb_product_grand_total").text(paid_Discount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find(".lb_product_outstandingamount").text(paid_Discount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                TotalAmount += paid_Discount;
                TotalOutstandingAmount += paid_Discount;
                _countingRow++;
                maxRows--;
            }

            while (maxRows > 0) {
                var emptyTeample = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse;height:23px\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:21px;padding:5px;\"></td>\
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:21px;padding:5px;border: 1px solid black;border-right: 1px solid black;\"></td >\
                    <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\"></td>\
                </tr>   ";
                $("#tb-products-1 tbody,#tb-products-2 tbody").append(emptyTeample);
                _countingRow++;
                maxRows--;
            }

            var itemTemplate = " \
                <tr style=\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" colspan=2 style=\"font-size:21px;padding:5px;text-align:right;text-align:right;\">    \
                       <dev class=\"row\" > \
                            <b style=\"font-size:18px;font-weight: bolder;text-align: left;\" class=\"col-md-6 product_outstandingamountText\" > </b>\
                            <b style=\"font-size:18px;font-weight: bolder;\" class=\"col-md-6\"> รวมเงิน </b> \
                        </div> \
                   </td >  \
                <td style=\"border-left: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span  class=\"product_outstandingamount\" style=\"font-size:18px;font-weight: bolder;\">  </span> \
                   </td>   \
                </tr>\  ";

            //if (_invoice.ManualDiscount > 0) {
            //    itemTemplate = " \
            //     <tr style =\"border: 1px solid black;border-collapse:collapse\">   \
            //        <td class=\"drag-icon ui-sortable-handle\" colspan=\"2\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
            //           <b  style=\"font-size:18px;\"> รวมค่าบริการ </b> \
            //       </td>  \
            //    <td style=\"border-left: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
            //              <b  class=\"paid_all_period_amount\" style=\"font-size:18px;\">  </b> \
            //       </td>   \
            //    </tr>\
            //    <tr style =\"border: 1px solid black;border-collapse:collapse\">   \
            //        <td class=\"drag-icon ui-sortable-handle\" colspan=\"2\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
            //           <b  style=\"font-size:18px;\"> ส่วนลดรวม </b> \
            //       </td>  \
            //    <td style=\"border-left: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
            //              <b  class=\"all_totalDiscount\" style=\"font-size:18px;\">  </b> \
            //       </td>   \
            //    </tr>\ " + itemTemplate;
            //}

            $("#tb-products-1 tbody,#tb-products-2 tbody ").append(itemTemplate);
            var lastRow = $("#tb-products-1 tbody tr:last,#tb-products-2 tbody tr:last");
            $(".paid_all_period_amount").text(_invoice.TotalAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(".all_totalDiscount").text(_invoice.ManualDiscount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(".product_grand_total").text(TotalAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(lastRow).find(".product_outstandingamount").text(TotalOutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(lastRow).find(".product_outstandingamountText").text("( " + ArabicNumberToText(TotalOutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 })) + " )");
            if (_hideCheckbox) {
                $("input:checkbox").remove();
                $(".span-paid").text("จำนวนเงินชำระ");

                $(".paid_period_amount").text(TotalOutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(".paid_all_period_amount").text(_totalAllPaid.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            }
        }
    };

    return {
        init: function () {
            modelInit();
            initSettinsBox();
        }
    };
}();

var _currentPage_type3 = function () {
    var _countingRow = 1;

    var initSettinsBox = function () {
        $(".jabjai-toolbar-link").click(function () {
            $(".jabjai-right-panel").addClass("jabjai-panel--on");
            return false;
        });

        $(".jabjai-panel__close").click(function () {
            $(".jabjai-right-panel").removeClass("jabjai-panel--on");
            return false;
        });
    };

    var modelInit = function () {


        if (_invoice !== null && _invoice.InvoiceId !== 0) {
            //generateMainDetail();
            if (_invoice.InvoiceItems !== null) {
                generateItem();
            }


            $(".chk-all").change(function () {
                $(".chkShowProduct").prop('checked', $(this).is(":checked"));
                $(".chk-all").prop('checked', $(this).is(":checked"));
                calculateProduct();
            });

            $(".chkShowProduct").change(function () {
                $("input[data-row='" + $(this).data('row') + "']").prop('checked', $(this).is(":checked"));
                calculateProduct();
            });
        }

        function calculateProduct() {
            var grandTotal = 0;
            var outstandingamount = 0;
            $("#page1 #tb-products-1 input.chkShowProduct:checked").each(function (index, item) {
                grandTotal += parseFloat($(item).data('amount').replace(',', ''));
                outstandingamount += parseFloat($(item).data('outstanding').replace(',', ''));
            });

            $(".product_grand_total").text(grandTotal.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(".product_outstandingamount").text(outstandingamount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(".product_outstandingamountText").text("( " + ArabicNumberToText(outstandingamount.toLocaleString(undefined, { minimumFractionDigits: 2 })) + " )");

        }

        function appendItem() {
            var itemTemplate = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse;height:30px;\">   \
                   <td class=\"drag-icon ui-sortable-handle center\" style=\"padding-left:5px;text-align:center;\"> "+ _countingRow + "  </td>\
                    <td class=\"drag-icon ui-sortable-handle\" style=\"padding-left:5px;border: 1px solid black;border-right: 1px solid black;\">    \
                        <input type=\"checkbox\"  class=\"chkShowProduct\" data-row=\"chk-"+ _countingRow + "\" data-amount=\"\" data-outstanding=\"\" checked /> \
                       <span class=\"lb_product_name\" > </span> \
                   </td >  \
                <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\" >   \
                          <span  class=\"lb_product_outstandingamount\" >  </span> \
                   </td>   \
                </tr>   ";
            $("#tb-products-3 tbody").append(itemTemplate);
        }

        function generateItem() {
            var TotalAmount = 0;
            var TotalOutstandingAmount = 0;
            var maxRows = 22;

            $("table[id*=tb-products-3]").append("\<tr style=\"border:1px solid black;border-collapse: collapse;height:30px;\">\
                    <th style=\"border:1px solid black;border-collapse: collapse;width:10%;padding-left:5px;text-align:center;\" > ลำดับ </th>\
                    <th style=\"border:1px solid black;border-collapse: collapse;width:65%;padding-left:5px;\">\
                         <input type=\"checkbox\"  class=\"chk-all\" checked >รายการ</th>\
                    <th  style=\"text-align:center;border:1px solid black;border-collapse: collapse;width:25%;\">\
                       <span class=\"span-paid\">รับชำระ</span>\
                    </th>\
                </tr>");
            $.each(_invoice.InvoiceItems, function (index, item) {
                appendItem();
                var lastRow = $("#tb-products-3 tbody tr:last");
                $(lastRow).find("input").data('amount', item.GrandTotal.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find("input").data('outstanding', item.OutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find(".lb_product_name").text(item.ProductName);
                $(lastRow).find(".lb_product_grand_total").text(item.GrandTotal.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find(".lb_product_outstandingamount").text(item.OutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                TotalAmount += item.GrandTotal;
                TotalOutstandingAmount += item.OutstandingAmount;
                _countingRow++;
                maxRows--;
            });

            if (_invoice.paid_Discount ?? 0 > 0) {
                appendItem();
                let paid_Discount = _invoice.paid_Discount * -1;
                var lastRow = $("#tb-products-3 tbody tr:last");
                $(lastRow).find("input").data('amount', paid_Discount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find("input").data('outstanding', paid_Discount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find(".lb_product_name").text("ส่วนลด");
                $(lastRow).find(".lb_product_grand_total").text(paid_Discount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find(".lb_product_outstandingamount").text(paid_Discount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                TotalAmount += paid_Discount;
                TotalOutstandingAmount += paid_Discount;
                _countingRow++;
                maxRows--;
            }

            while (maxRows > 0) {
                var emptyTeample = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse;height:30px;\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:21px;padding:5px;\"></td>\
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:21px;padding:5px;border: 1px solid black;border-right: 1px solid black;\"></td >\
                    <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\"></td>\
                </tr>   ";
                $("#tb-products-3 tbody").append(emptyTeample);
                _countingRow++;
                maxRows--;
            }



            var itemTemplate = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse;height:30px;\">   \
                    <td class=\"drag-icon ui-sortable-handle\" colspan=2 style=\"padding:5px;text-align:right;text-align:right;\">    \
                            <b style=\"font-size:18px;font-weight: bolder;text-align: left;\" class=\"col-md-6 product_outstandingamountText\" > </b>\
                            <b style=\"font-size:18px;font-weight: bolder;\" class=\"col-md-6\"> ยอดรับชำระรวม </b> \
                   </td >  \
                <td style=\"border-left: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span  class=\"product_outstandingamount\" style=\"font-weight: bolder;\">  </span> \
                   </td>   \
                </tr>\  ";

            //if (_invoice.ManualDiscount > 0) {
            //    itemTemplate = itemTemplate + " \
            //    <tr style =\"border: 1px solid black;border-collapse:collapse\">   \
            //        <td class=\"drag-icon ui-sortable-handle\" colspan=\"2\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
            //           <b  style=\"font-size:18px;\"> รวมค่าบริการ </b> \
            //       </td>  \
            //    <td style=\"border-left: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
            //              <b  class=\"paid_all_period_amount\" style=\"font-size:18px;\">  </span> \
            //       </td>   \
            //    </tr>\
            //    <tr style =\"border: 1px solid black;border-collapse:collapse\">   \
            //        <td class=\"drag-icon ui-sortable-handle\" colspan=\"2\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
            //           <b  style=\"font-size:18px;\"> ส่วนลดรวม </b> \
            //       </td>  \
            //    <td style=\"border-left: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
            //              <b  class=\"all_totalDiscount\" style=\"font-size:18px;\">  </b> \
            //       </td>   \
            //    </tr>\ ";
            //}

            $("#tb-products-3 tbody ").append(itemTemplate);
            var lastRow = $("#tb-products-3 tbody tr:last");
            $(".paid_all_period_amount").text(_invoice.TotalAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(".all_totalDiscount").text(_invoice.ManualDiscount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(lastRow).find(".product_grand_total").text(TotalAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(lastRow).find(".product_outstandingamount").text(TotalOutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(lastRow).find(".product_outstandingamountText").text("( " + ArabicNumberToText(TotalOutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 })) + " )");
            if (_hideCheckbox) {
                $("input:checkbox").remove();
                $(".span-paid").text("จำนวนเงินชำระ");

                $(".paid_period_amount").text(TotalOutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(".paid_all_period_amount").text(_totalAllPaid.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            }
        }
    };

    return {
        init: function () {
            modelInit();
            initSettinsBox();
        }
    };
}();

$(function () {
    _currentPage_type2.init();
    _currentPage_type3.init();

    $(".btnPrint").click(function () {
        $("#pageTemp").html('');
        if ($("#ddl-option").val() === "0") {
            $("#page1").clone().appendTo("#pageTemp");
        } else {
            $("#page3").clone().appendTo("#pageTemp");
        }
        $("#pageTemp").find("input.chkShowProduct:not(:checked)").closest('tr').remove();
        $("#pageTemp").find("input:checkbox").remove();
        var contents = $("#pageTemp").html();
        var frame1 = document.createElement('iframe');
        frame1.name = "frame1";
        frame1.style.position = "absolute";
        frame1.style.top = "-1000000px";
        document.body.appendChild(frame1);
        var frameDoc = (frame1.contentWindow) ? frame1.contentWindow : (frame1.contentDocument.document) ? frame1.contentDocument.document : frame1.contentDocument;
        frameDoc.document.open();
        frameDoc.document.write("<html><meta http-equiv='cache-control' content='no-cache'><head><title></title>");
        frameDoc.document.write("<link rel=\"stylesheet\" href=\"/bootstrap%20SB2/bower_components/bootstrap/dist/css/bootstrap.css\" type=\"text/css\"/>");
        frameDoc.document.write("<link rel=\"stylesheet\" href=\"/Content/print.css\" type=\"text/css\"/>");
        frameDoc.document.write("<script type=\"text/javascript\" src=\"/Scripts/jquery-1.10-2.min.js\" /></script>");
        frameDoc.document.write('</head><body>');
        frameDoc.document.write(contents);
        frameDoc.document.write('</body></html>');
        frameDoc.document.close();
        setTimeout(function () {
            window.frames["frame1"].focus();
            window.frames["frame1"].print();
            document.body.removeChild(frame1);
        }, 500);
        return false;
    });

    $("#ddl-option").change(function () {
        if ($(this).val() === "0") {
            $("#page1").show();
            $("#page3").hide();
        }
        else {
            $("#page1").hide();
            $("#page3").show();
        }
    });

    $(".employees-name-1").html(employees_name);
    $(".employees-name-2").html("(" + employees_name + ")");
    ShowType();

    $("#ddl-show-name").change(function () {
        ShowType();
    });
});

function ShowType() {
    $("[name=show_type_2]").hide();
    $("[name=show_type_1]").hide();
    if ($("#ddl-show-name").val() === "0") {
        $(".employees-name-1").html(employees_name);
        $("[name=show_type_1]").show();
    }
    else if ($("#ddl-show-name").val() === "1") {
        $(".employees-name-1").html("<br/>");
        $("[name=show_type_1]").show();
    }
    else if ($("#ddl-show-name").val() === "2") {
        $("[name=show_type_2]").show();
    }
}