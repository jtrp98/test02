

var _countingRow = 1;
var _currentPage = function () {

    var initSettinsBox = function () {
        $("#btnPrint").click(function () {
            var oDate = new Date();
            var MainWindow = window.open();
            MainWindow.document.write("<html><meta http-equiv='cache-control' content='no-cache'><head><title></title>");
            MainWindow.document.write("<link rel=\"stylesheet\" href=\"https://" + _domain + "/bootstrap%20SB2/bower_components/bootstrap/dist/css/bootstrap.css\" type=\"text/css\"/>");
            MainWindow.document.write("<script type=\"text/javascript\" src=\"https://" + _domain + "/Scripts/jquery-1.10-2.min.js\" /></script>");
            MainWindow.document.write("<script type=\"text/javascript\" src=\"https://" + _domain + "/Scripts/pages/invoice-print.js?" + oDate.getTime() + "\" /></script>");
            MainWindow.document.write('</head><body>');
            $("#pageTemp").html('');
            $("#page1").clone().appendTo("#pageTemp");
            $("#page2").clone().appendTo("#pageTemp");
            $("#pageTemp").find("input:not(:checked)").closest('tr').remove();
            $("#pageTemp").find("input:checkbox").remove();
            MainWindow.document.write($("#pageTemp").html());
            MainWindow.document.write('</body></html>');
            MainWindow.document.close();
        });
    };


    var modelInit = function () {
        $("#ddl-option").change(function () {
            if ($(this).val() === "0")
                $("#page2").show();
            else
                $("#page2").hide();
        });

        if (_invoice !== null && _invoice.InvoiceId !== 0) {
            //generateMainDetail();
            if (_invoice.InvoiceItems !== null) {
                generateItem();
            }


            $(".chk-all").change(function () {
                $(".chkShowProduct").prop('checked', $(this).is(":checked"));
                $(".chk-all").prop('checked', $(this).is(":checked"));
            });

            $(".chkShowProduct").change(function () {
                $("input[data-row='" + $(this).data('row') + "']").prop('checked', $(this).is(":checked"));
                $(function () {
                    var grandTotal = 0;
                    var outstandingamount = 0;
                    $("#page1 input:checked").each(function (index, item) {
                        grandTotal += parseFloat($(item).data('amount'));
                        outstandingamount += parseFloat($(item).data('outstanding'));
                    });

                    $(".product_grand_total").text(grandTotal.toFixed(2));
                    $(".product_outstandingamount").text(outstandingamount.toFixed(2));
                });
            });
        }

        function appendItem() {
            var itemTemplate = " \
               <tr style=\"border-left: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:18px;padding:5px;\">    \
                        <input type=\"checkbox\"  class=\"chkShowProduct\" data-row=\"chk-"+ _countingRow + "\" data-amount=\"\" data-outstanding=\"\" checked /> \
                       <span class=\"lb_product_name\" style=\"font-size:18px;\"> </span> \
                   </td >  \
                   <td style=\"border-left: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span class=\"lb_product_grand_total\"style=\"font-size:18px;\"> </span> \
                   </td>   \
                <td style=\"border-left: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span  class=\"lb_product_outstandingamount\" style=\"font-size:18px;\">  </span> \
                   </td>   \
                </tr>   ";
            $("#tb-products-1 tbody,#tb-products-2 tbody").append(itemTemplate);
        }

        function generateItem() {
            var TotalAmount = 0;
            var TotalOutstandingAmount = 0;

            $.each(_invoice.InvoiceItems, function (index, item) {
                appendItem();
                var lastRow = $("#tb-products-1 tbody tr:last,#tb-products-2 tbody tr:last");
                $(lastRow).find("input").data('amount', item.GrandTotal.toFixed(2));
                $(lastRow).find("input").data('outstanding', item.OutstandingAmount.toFixed(2));
                $(lastRow).find(".lb_product_name").text(item.ProductName);
                $(lastRow).find(".lb_product_grand_total").text(item.GrandTotal.toFixed(2));
                $(lastRow).find(".lb_product_outstandingamount").text(item.OutstandingAmount.toFixed(2));
                TotalAmount += item.GrandTotal;
                TotalOutstandingAmount += item.OutstandingAmount;
                _countingRow++;
            });

            var itemTemplate = " \
               <tr style=\"border: 1px solid black;border-collapse:collapse\">   \
                    <td class=\"drag-icon ui-sortable-handle\" style=\"font-size:18px;padding:5px;text-align:right;text-align:right;\">    \
                       <span  style=\"font-size:18px;\"> รวม</span> \
                   </td >  \
                   <td style=\"border-left: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span class=\"product_grand_total\"style=\"font-size:18px;\"> </span> \
                   </td>   \
                <td style=\"border-left: 1px solid black;border-right: 1px solid black; border-collapse: collapse;padding:5px;text-align:right;\">   \
                          <span  class=\"product_outstandingamount\" style=\"font-size:18px;\">  </span> \
                   </td>   \
                </tr>   ";
            $("#tb-products-1 tbody,#tb-products-2 tbody ").append(itemTemplate);
            var lastRow = $("#tb-products-1 tbody tr:last,#tb-products-2 tbody tr:last");
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

$(function () {
    _currentPage.init();
});