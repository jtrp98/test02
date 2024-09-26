var _currentTimestamp = $.now();
var _chartTitle = "";
var _chartLabels = [];
var _chartData = [];
var _accounting;

function masterData(masterObject) {
    $.ajax({
        url: masterDataUrl,
        type: "GET",
        data: "entity=" + masterObject.entity,
        success: function (result) {
            $.each(result.Result, function (index, item) {
                var newOption = new Option(item[masterObject.label], item[masterObject.value], false, false);
                $(masterObject.target).append(newOption);
            });
        }
    });
}

function termData() {
    $.ajax({
        url: masterDataUrl,
        type: "GET",
        data: "entity=term",
        success: function (result) {
            $.each(result.Result, function (index, item) {
                var newOption = new Option(item.Year.Number + "/" + item.sTerm, item.TermId, false, false);
                $("#ddl-term").append(newOption);
            });
        }
    });
}

var _currentPage = function () {
    var initPage = function () {


        $.each(YearJson, function (index, item) {
            var newOption = new Option(item.Number, item.Number - 543, false, false);
            $(".ddl-year").append(newOption).trigger('change');
        });

        $("#dateRange").daterangepicker({
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

        $("#ddl-search-by").change(function () {
            $("#ddl-month,#dateRange,#ddl-year").addClass('hide');
            if ($(this).val() === "1")
                $("#dateRange").removeClass('hide');
            else if ($(this).val() === "2")
                $("#ddl-month,#ddl-year").removeClass('hide');
            else if ($(this).val() === "3")
                $("#ddl-year").removeClass('hide');
        });

        $("#btnAdvSearch").click(function () {
            var formdata = $('#form-search').serializeJSON();
            $("#pre-submit").addClass('hide');
            $("#chart-accounting-movement").removeClass('hide');
            if ($("#ddl-search-by").val() === "1") {//week
                moment.locale('th');
                formdata['ReportDateRange'] = moment($("#dateRange").val(), 'DD/MM/YYYY').startOf('week').format('DD/MM/YYYY') + "-"
                    + moment($("#dateRange").val(), 'DD/MM/YYYY').endOf('week').format('DD/MM/YYYY');
            }

            setLabel(formdata);
            _accounting.forEach(function (item, index) {
                formdata["AccountingId"] = item.AccountinId;
                formdata["AccountingName"] = item.AccountingName;
                formdata["BorderColor"] = item.GraphBorderColor;
                ajaxReportInvoice(formdata);
            });
            renderChart();
        });

        $.ajax({
            url: masterDataUrl,
            type: "GET",
            data: "entity=accounting",
            success: function (result) {
                _accounting = result.Result;
            }
        });

        termData();

        masterData({ target: "#ddl-paymentmethod", entity: "paymentmethod", label: "PaymentMethodName", value: "PaymentMethodId" });
        /*
         */
    };

    function setLabel(formdata) {
        _chartLabels = [];
        _chartData = [];
        _chartTitle = "";

        var filterYear = 0;
        switch (formdata.ReportType) {
            case "1"://week
                _chartLabels = ["อาทิตย์", "จันทร์", "อังคาร", "พุธ", "พฤหัสบดี", "ศุกร์", "เสาร์"];
                _chartTitle = "รายงานประจำสัปดาห์ ตั้งแต่วันที่ " +
                    moment($("#dateRange").val(), 'DD/MM/YYYY').startOf('week').format('DD/MM/YYYY') + " - "
                    + moment($("#dateRange").val(), 'DD/MM/YYYY').endOf('week').format('DD/MM/YYYY');
                break;
            case "2"://month
                var startDate = parseInt(moment($("#dateRange").val(), 'DD/MM/YYYY').startOf('month').format('D'));
                var endDate = parseInt(moment($("#dateRange").val(), 'DD/MM/YYYY').endOf('month').format('D'));
                var filterMonth = parseInt(moment($("#dateRange").val(), 'DD/MM/YYYY').endOf('month').format('MM'));
                filterYear = parseInt(moment($("#dateRange").val(), 'DD/MM/YYYY').endOf('month').format('YYYY')) + 543;
                var textMonth = "" + filterMonth;
                _chartTitle = "รายงานประจำเดือน " + filterMonth + " ปี " + filterYear;

                if (filterMonth < 10) {
                    textMonth = "0" + textMonth;
                }

                while (startDate <= endDate) {
                    var textDay = "" + startDate;
                    if (startDate < 10) {
                        textDay = "0" + textDay;
                    }
                    _chartLabels.push(textDay + "/" + textMonth + "/" + filterYear);
                    startDate++;
                }
                break;
            case "3"://year
                filterYear = parseInt($("#ddl-year").val()) + 543;
                _chartLabels = ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'];
                _chartTitle = "รายงานประจำปี " + filterYear;
                break;
        }

    }

    function ajaxReportInvoice(formdata) {
        $.ajax({
            url: getReportInvoiceUrl,
            type: 'POST',
            async: false,
            data: { filter: JSON.stringify(formdata) },
            success: function (resp) {
                var dataChart = {
                    label: formdata["AccountingName"],
                    data: [],
                    fill: false,
                    lineTension: 0.1,
                    borderWidth: 1,
                    backgroundColor: [
                        formdata["BorderColor"]
                    ],
                    borderColor: [
                        formdata["BorderColor"]
                    ]
                };

                resp.Result.forEach(function (item, index) {
                    dataChart.data.push(item.TotalPaid);
                });
                _chartData.push(dataChart);
            }
        });
    }

    var myChart;
    function renderChart() {
        // clear canvas
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        if (myChart) myChart.destroy();

        myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: _chartLabels,
                datasets: _chartData
            },
            options: {

                title: {
                    display: true,
                    text: _chartTitle
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });

    }


    return {
        init: function () {
            initPage();
        }
    };
}();

$(function () {
    _currentPage.init();
});
$(document)
    .ajaxStart(function () {
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
    })
    .ajaxStop($.unblockUI)
    .ajaxError(function () {
        $.confirm({
            title: '<h3>แจ้งเตือน</h3>',
            content: '<h3>พบข้อผิดพลาดกรุณาแจ้งผู้ดูแลระบบ</h3>',
            theme: 'bootstrap',
            buttons: {
                "<span style=\"font-size: 20px;\">ปิด</span>": {
                    btnClass: 'btn-default',
                    action: function () {
                    }
                }
            }
        });
    });