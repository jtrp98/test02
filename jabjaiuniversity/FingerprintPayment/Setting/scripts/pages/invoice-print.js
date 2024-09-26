function printInvoice() {
    $("input:not(:checked)").closest('tr').remove();
    setTimeout(function () {
        window.print();
        window.close();
    },700);
}

$(function () {
    printInvoice();
});