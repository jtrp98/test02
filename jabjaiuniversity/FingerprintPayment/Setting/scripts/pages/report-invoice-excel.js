

var _currentPageExcel = function () {
    var initPage = function () {

        $("#date").daterangepicker({
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
            $("#ddl-month-excel,#date,#ddl-year").addClass('hide');
            if ($(this).val() === "0")
                $("#date").removeClass('hide');
            else if ($(this).val() === "2")
                $("#ddl-month,#ddl-year").removeClass('hide');
            else if ($(this).val() === "3")
                $("#ddl-year").removeClass('hide');
        });

        $("#btnExport").click(function () {
            var formdata = {};
            formdata['ReportType'] = $("#ddl-search-by").val();

            if ($("#ddl-search-by").val() === "1")
                formdata['ReportDate'] = $("#dateRange").val();
            else if ($("#ddl-search-by").val() === "2") {
                formdata['ReportYear'] = $("#ddl-year").val();
                formdata['ReportMonth'] = $("#ddl-month").val();
            }
            else if ($("#ddl-search-by").val() === "3")
                formdata['ReportYear'] = $("#ddl-year").val();

            formdata['PaymentMethodId'] = $("#ddl-paymentmethod").val();
            formdata['TermId'] = $("#ddl-term").val();

            window.open(exportExcelUrl + "?filter=" + JSON.stringify(formdata));
        });
    };



    return {
        init: function () {
            initPage();
        }
    };
}();


var fnDataTable = function () {
    var initTableSearch = function (tableId) {
        var oTable = $(tableId);
        // begin first table
        oTable.dataTable({
            "processing": true,
            "serverSide": true,
            "deferRender": true,
            "searching": false,
            "paging": true,
            "pageLength": 100,
            "ajax": {
                "type": "POST",
                "url": detailsUrl,
                "contentType": 'application/json;',
                'data': function (data) {

                    if ($("#ddl-search-by").val() === "1")
                        data.columns[10].search.value = $("#dateRange").val();
                    else if ($("#ddl-search-by").val() === "2") {
                        data.columns[11].search.value = $("#ddl-month").val();
                        data.columns[12].search.value = $("#ddl-year").val();
                    }
                    else if ($("#ddl-search-by").val() === "3")
                        data.columns[12].search.value = $("#ddl-year").val();

                    data.columns[13].search.value = $("#ddl-paymentmethod").val();
                    data.columns[14].search.value = $("#ddl-term").val();

                    /*
                    if ($("#ddl-search-by-excel").val() === "term" && $('#ddl-term').val() !== "")
                        data.columns[9].search.value = $('#ddl-term').val();
                    else if ($("#ddl-search-by").val() === "date")
                        data.columns[3].search.value = $('#dateRange').val();
                    else if ($("#ddl-search-by").val() === "duedate")
                        data.columns[4].search.value = $('#dateRange').val();
                    */

                    return JSON.stringify(data);
                }
            },
            sDom: '<"search-box"r>lftip',
            "language": {
                "lengthMenu": "แสดงข้อมูลหน้าละ _MENU_",
                "search": "ค้นหา",
                "paginate": {
                    "previous": "ก่อนหน้านี้",
                    "next": "หน้าต่อไป"
                },
                "info": "แสดงข้อมูล หน้า _PAGE_ ถึง _PAGES_",
                "infoFiltered": "(จำนวนข้อมูลทั้งหมด _MAX_)"
            },
            "aoColumns": [
                { "mData": "No", "sName": "No", "aTargets": [0] },
                { "mData": "PaymentDateString", "sName": "PaymentDateString", "aTargets": [1] },
                { "mData": "InvoiceNumber", "sName": "InvoiceNumber", "aTargets": [2] },
                { "mData": "StudentCode", "sName": "StudentCode", "aTargets": [3] },
                { "mData": "StudentName", "sName": "StudentName", "aTargets": [4] },
                {
                    "mData": "StudentSubLevel", "sName": "StudentSubLevel", "aTargets": [5],
                    render: function (data, type, row, meta) {
                        return row.StudentSubLevel + "/" + row.StudentSubLevel2;
                    }
                },
                {
                    "mData": "StudentTerm", "sName": "StudentTerm", "aTargets": [6],
                    render: function (data, type, row, meta) {
                        return row.StudentTerm + "/" + row.StudentTermYear;
                    }
                },
                { "mData": "GrandTotal", "sName": "GrandTotal", "aTargets": [7], visible: false  },
                { "mData": "Amount", "sName": "Amount", "aTargets": [8] },
                { "mData": "Payee", "sName": "Payee", "aTargets": [9] },
                { "mData": "Payee", "sName": "ReportDate", "aTargets": [10], visible: false },
                { "mData": "Payee", "sName": "ReportMonth", "aTargets": [11], visible: false },
                { "mData": "Payee", "sName": "ReportYear", "aTargets": [12], visible: false },
                { "mData": "Payee", "sName": "PaymentMethodId", "aTargets": [13], visible: false },
                { "mData": "Payee", "sName": "TermId", "aTargets": [14], visible: false },
                { "mData": "PaymentMethod", "sName": "PaymentMethod", "aTargets": [15] }
            ],
            "order": [1, "asc"],
            "fnDrawCallback": function () {

                $("li a[type=delete]").bind('click', function (e) {
                    console.log($(this).attr("data"));
                });
            }
        });

        oTable.find('.group-checkable').change(function () {
            var set = jQuery(this).attr("data-set");
            var checked = jQuery(this).is(":checked");
            jQuery(set).each(function () {
                if (checked) {
                    $(this).prop("checked", true);
                    $(this).parents('tr').addClass("active");
                } else {
                    $(this).prop("checked", false);
                    $(this).parents('tr').removeClass("active");
                }
            });
        });
        $('#data-list input[type=search]').addClass("form-control input-sm");
        $('#data-list select').addClass("form-control input-sm");
        oTable.on('change', 'tbody tr .checkboxes', function () { //checkbox all
            $(this).parents('tr').toggleClass("active");
        });

        $(tableId + '_filter input').unbind();  //disable keytext search
        $(tableId + '_filter input').bind('keyup', function (e) { //enter for keytext search
            if (e.keyCode === 13) {
                oTable.fnFilter(this.value);
            }
        });

        $("#btnAdvSearch").bind('click', function (e) {
            oAllTable.fnFilter();
        });
        $("#btRemove").click(function () {

            var set = oTable.find('.group-checkable');
            var data = [];
            oTable.find('.checkboxes:checked').each(function () {
                data.push({ id: $(this).val() });
            });
        });

        //$("#searchBtn").click(function () {
        //    var value = $("#tableSearch_filter input").val();
        //    oTable.fnFilter(value);
        //});

        $("#resetBtn").click(function () {
            oTable.fnFilter("");
        });

        return oTable;
    };

    var conditionSearch = function () {
        $("div.search-panel ul li").bind('click', function (e) {
            var div = $(this).closest("div");
            var text = $(this).text();
            var val = $(this).attr("val");
            div.find("button span:eq(0)").html(text);
            div.next().attr("condition", val);
        });

        $($.fn.dataTable.tables(true)).DataTable()
            .columns.adjust();
    };

    return {
        //main function to initiate the module
        init: function () {
            if (!jQuery().dataTable) {
                return;
            }

            oAllTable = initTableSearch("#allSearch"); console.log(oAllTable.fnGetData());
            conditionSearch();
        }
    };
}();


$(function () {
    _currentPageExcel.init();
    fnDataTable.init();
});