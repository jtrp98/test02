var _countingRow = 1;
var _currentPage = function () {

    var initSettinsBox = function () {
        $("#btnPrint").click(function () {
            $("#pageTemp").html('');
            $("#page1").clone().appendTo("#pageTemp");
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
            frameDoc.document.write("<link rel=\"stylesheet\" href=\"//Content/print.css\" type=\"text/css\"/>");
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

        if (_invoice !== null && _invoice.InvoiceId != 0) {
            //generateMainDetail();
            if (_invoice.InvoiceItems !== null) {
                generateItem();
            }
        }

        function appendItem() {
            var itemTemplate = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:18px;padding:5px;text-align:center\">    \
                       "+ _countingRow + " \
                   </td >  \
                   <td style=\"border: 1px solid black; border-collapse: collapse;padding:5px;\">   \
                       <span class=\"lb_product_name\" style=\"font-size:18px;\"> </span> \
                   </td>   \
                <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:center;\">   \
                          <span class=\"lb_product_grand_total\"style=\"font-size:18px;\"> </span> \
                   </td>   \
                </tr>   ";
            $("#tb-products-1 tbody,#tb-products-2 tbody").append(itemTemplate);
        }

        function generateItem() {
            var TotalAmount = 0;
            var TotalOutstandingAmount = 0;
            var maxRows = 13;

            $.each(_invoice.InvoiceItems, function (index, item) {
                appendItem();
                var lastRow = $("#tb-products-1 tbody tr:last");
                $(lastRow).find("input").data('amount', item.GrandTotal.toFixed(2));
                $(lastRow).find("input").data('outstanding', item.OutstandingAmount.toFixed(2));
                $(lastRow).find(".lb_product_name").text(item.ProductName);
                $(lastRow).find(".lb_product_grand_total").text(item.GrandTotal.toFixed(2));
                $(lastRow).find(".lb_product_outstandingamount").text(item.OutstandingAmount.toFixed(2));
                TotalAmount += item.GrandTotal;
                TotalOutstandingAmount += item.OutstandingAmount;
                _countingRow++;
                maxRows--;
            });

            while (maxRows > 0) {
                var emptyTeample = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse;height:23px\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:18px;padding:5px;\">    \
                   </td >  \
                   <td style=\"border: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                   </td>   \
                <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                   </td>   \
                </tr>   ";
                $("#tb-products-1 tbody").append(emptyTeample);
                _countingRow++;
                maxRows--;
            }

            var itemTemplate = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
                   </td >  \
                   <td style=\"border: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                       <span  style=\"font-size:18px;\"> รวม</span> \
                   </td>   \
                <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:center;\">   \
                      <span class=\"product_grand_total\"style=\"font-size:18px;\"> </span> \
                   </td>   \
                </tr>   ";
            $("#tb-products-1 tbody").append(itemTemplate);
            var lastRow = $("#tb-products-1 tbody tr:last");
            $(lastRow).find(".product_grand_total").text(TotalAmount.toFixed(2));
            $(lastRow).find(".product_outstandingamount").text(TotalOutstandingAmount.toFixed(2));
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

    var initSettinsBox = function () {
        $("#btnPrint").click(function () {
            $("#pageTemp").html('');
            $("#page1").clone().appendTo("#pageTemp");
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

        $("#ddl-option").change(function () {
            if ($(this).val() === "0") {
                $(".labelDueDate").show().css("height", "26.4px");
            } else {
                $(".labelDueDate").hide().css("height", "26.4px");
            }
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

        if (_invoice != null && _invoice.InvoiceId !== 0) {
            //generateMainDetail();
            if (_invoice.InvoiceItems !== null) {
                generateItem();
            }
        }

        function appendItem() {
            var itemTemplate = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:18px;padding:5px;text-align:center\">    \
                       "+ _countingRow + " \
                   </td >  \
                   <td style=\"border: 1px solid black; border-collapse: collapse;padding:5px;\">   \
                       <span class=\"lb_product_name\" style=\"font-size:18px;\"> </span> \
                   </td>   \
                <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:center;\">   \
                          <span class=\"lb_product_grand_total\"style=\"font-size:18px;\"> </span> \
                   </td>   \
                </tr>   ";
            $("#tb-products-1 tbody,#tb-products-2 tbody").append(itemTemplate);
        }

        function generateItem() {
            var TotalAmount = 0;
            var TotalOutstandingAmount = 0;
            var maxRows = 13;

            $.each(_invoice.InvoiceItems, function (index, item) {
                appendItem();
                var lastRow = $("#tb-products-1 tbody tr:last");
                $(lastRow).find("input").data('amount', item.GrandTotal.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find("input").data('outstanding', item.OutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find(".lb_product_name").text(item.ProductName);
                $(lastRow).find(".lb_product_grand_total").text(item.GrandTotal.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find(".lb_product_outstandingamount").text(item.OutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                TotalAmount += item.GrandTotal;
                //TotalOutstandingAmount += item.OutstandingAmount;
                _countingRow++;
                maxRows--;
            });

            if (_invoice.ManualDiscount > 0) {
                appendItem();
                var lastRow = $("#tb-products-1 tbody tr:last");
                //$(lastRow).find("input").data('amount', item.GrandTotal.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                //$(lastRow).find("input").data('outstanding', item.OutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                $(lastRow).find(".lb_product_name").text("ส่วนลด");
                $(lastRow).find(".lb_product_grand_total").text((_invoice.ManualDiscount * -1.0).toLocaleString(undefined, { minimumFractionDigits: 2 }));
                //$(lastRow).find(".lb_product_outstandingamount").text(item.OutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
                TotalAmount -= _invoice.ManualDiscount;
                //TotalOutstandingAmount -= _invoice.ManualDiscount;
                maxRows--;
            }

            $.each(_invoice.PaidPayments, function (index, item) {
                TotalOutstandingAmount += item.Amount;
            });



            while (maxRows > 0) {
                var emptyTeample = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse;height:23px\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:18px;padding:5px;\">    \
                   </td >  \
                   <td style=\"border: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                   </td>   \
                <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                   </td>   \
                </tr>   ";
                $("#tb-products-1 tbody").append(emptyTeample);
                _countingRow++;
                maxRows--;
            }


            var itemTemplate = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
                   </td >  \
                   <td style=\"border: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                       <span  style=\"font-size:18px;\"> รวม</span> \
                   </td>   \
                <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:center;\">   \
                      <span class=\"product_grand_total\"style=\"font-size:18px;\"> </span> \
                   </td>   \
                </tr>\
               <tr style=\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
                   </td >  \
                   <td style=\"border: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                       <span  style=\"font-size:18px;\"> รับชำระแล้ว</span> \
                   </td>   \
                <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:center;\">   \
                      <span class=\"paid_done_total\"style=\"font-size:18px;\"> </span> \
                   </td>   \
                </tr>\
               <tr style=\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
                   </td >  \
                   <td style=\"border: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                       <span  style=\"font-size:18px;\"> ยอดหนี้คงเหลือ</span> \
                   </td>   \
                <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:center;\">   \
                      <span class=\"product_outstandingamount\"style=\"font-size:18px;\"> </span> \
                   </td>   \
                </tr>";

            //if (_invoice.ManualDiscount > 0) {
            //    itemTemplate = " \
            //       <tr style=\"border: 1px solid black;border-collapse:collapse\">   \
            //            <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
            //           </td >  \
            //           <td style=\"border: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
            //               <span  style=\"font-size:18px;\"> ยอดหนี้คงเหลือ</span> \
            //           </td>   \
            //        <td style=\"border: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:center;\">   \
            //              <span class=\"product_outstandingamount\"style=\"font-size:18px;\"> </span> \
            //           </td>   \
            //        </tr>";
            //}

            $("#tb-products-1 tbody").append(itemTemplate);
            var lastRow = $("#tb-products-1 tbody tr:last");
            $(".product_grand_total").text(TotalAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            //  $(lastRow).find(".product_outstandingamount").text(TotalOutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            //$(".product_outstandingamount").text(_invoice.OutstandingAmount.toLocaleString(undefined, { minimumFractionDigits: 2 }));
            //$(".product_outstandingamount").text((TotalAmount - TotalOutstandingAmount - _invoice.ManualDiscount).toLocaleString(undefined, { minimumFractionDigits: 2 }));
            //$(".paid_done_total").text((TotalOutstandingAmount).toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(".product_outstandingamount").text((TotalAmount - TotalOutstandingAmount).toLocaleString(undefined, { minimumFractionDigits: 2 }));
            $(".paid_done_total").text((TotalOutstandingAmount).toLocaleString(undefined, { minimumFractionDigits: 2 }));
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
    //_currentPage.init();
    _currentPage_type2.init();
});