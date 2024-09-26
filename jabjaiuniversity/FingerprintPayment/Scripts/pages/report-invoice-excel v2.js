

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
            formdata['ItemType'] = $("#ddl-itemtype").val();

            window.open(exportExcelUrl + "?filter=" + JSON.stringify(formdata));
        });
    };

    return {
        init: function () {
            initPage();
        }
    };
}();

var footer = "";

var fnDataTable = function () {
    var initTableSearch = function (tableId) {
        var oTable = $(tableId);

        $("allSearch").DataTable().destroy();

        // begin first table
        oTable.dataTable({
            "processing": true,
            "serverSide": true,
            "deferRender": true,
            "searching": false,
            "paging": true,
            "pageLength": 100,
            "scrollY": "800px",
            "scrollX": true,
            "scrollCollapse": true,
            "fixedColumns": {
                left: 8
                //rightColumns: 1
            },
            "ajax": {
                "type": "POST",
                "url": detailsUrl,
                "contentType": 'application/json;',
                'data': function (data) {

                    if ($("#ddl-search-by").val() === "1")
                        data.columns[aoColumnDefs.length - 5].search.value = $("#dateRange").val();
                    else if ($("#ddl-search-by").val() === "2") {
                        data.columns[aoColumnDefs.length - 4].search.value = $("#ddl-month").val();
                        data.columns[aoColumnDefs.length - 3].search.value = $("#ddl-year").val();
                    }
                    else if ($("#ddl-search-by").val() === "3")
                        data.columns[aoColumnDefs.length - 3].search.value = $("#ddl-year").val();

                    data.columns[aoColumnDefs.length - 2].search.value = $("#ddl-paymentmethod").val();
                    data.columns[aoColumnDefs.length - 1].search.value = $("#ddl-term").val();

                    /*
                    if ($("#ddl-search-by-excel").val() === "term" && $('#ddl-term').val() !== "")
                        data.columns[9].search.value = $('#ddl-term').val();
                    else if ($("#ddl-search-by").val() === "date")
                        data.columns[3].search.value = $('#dateRange').val();
                    else if ($("#ddl-search-by").val() === "duedate")
                        data.columns[4].search.value = $('#dateRange').val();
                    */

                    return JSON.stringify(data);
                },
                "dataSrc": function (response) {
                    footer = response.footer;
                    $.each(response.data, function (e, s) {
                        let PRODUCT_ITEM = $.parseJSON(s.PRODUCT_ITEM)
                        $.each(PRODUCT_ITEM, function (e1, s1) {
                            response.data[e]["PR_" + s1.D] = s1.A;
                        });

                        let PRODUCT_GROUP = $.parseJSON(s.PRODUCT_GROUP)

                        response.data[e]["ProductType_0"] = PRODUCT_GROUP[0].ProductType_0;
                        response.data[e]["ProductType_1"] = PRODUCT_GROUP[0].ProductType_1;
                    });

                    return response.data;
                }
            },
            //footerCallback: function (row, data, start, end, display) {
            //    var api = this.api();
            //    //this.api().columns().every(function () {
            //    //    var column = this;
            //    //    $(footer).append('<th colspan="15"><lable>รวมเงินภาคเรียนที่ 1</lable></th>');
            //    //});
            //},
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
            "aoColumnDefs": aoColumnDefs,
            "aoColumns": aoColumns,
            "order": [1, "asc"],
            "drawCallback": function () {

                //$("li a[type=delete]").bind('click', function (e) {
                //    console.log($(this).attr("data"));
                //});         
                //let _countCol = $("#allSearch").find("thead tr th").length;
                //let _html = '<td colspan="7" class="text-right dtfc-fixed-left" style="left: 0px; position: sticky;">รวมเงินภาคเรียนที่ 1</td>';
                //_html += '<td data="class-tfoot-2" class="text-right dtfc-fixed-left" style="left: 653px; position: sticky;">10,000,000.00</td>';

                //for (i = 8; i < _countCol - 2; i++) {
                //    _html += '<td data="class-tfoot" class="text-right">10,000.00</td>';
                //}

                //$("#allSearch").find('tbody').append('<tr>' + _html + '<td colspan="2"></td></tr>');

                //$("#allSearch tbody").find('[data=class-tfoot]').css("position", "unset");
                //$("#allSearch tbody").find('[data=class-tfoot-2]').css("left", "653px");
                //settingFooter();

                settingFooter();
            },
            "initComplete": function (settings, json) {

                //let _countCol = $(this).find("thead tr th").length;
                //let _html = '<td colspan="7" class="text-right dtfc-fixed-left" style="left: 0px; position: sticky;">รวมเงินภาคเรียนที่ 1</td>';
                //_html += '<td data="class-tfoot-2" class="text-right dtfc-fixed-left" style="left: 653px; position: sticky;">10,000,000.00</td>';

                //for (i = 8; i < _countCol - 2; i++) {
                //    _html += '<td data="class-tfoot" class="text-right">10,000.00</td>';
                //}

                //$(this).find('tbody').append('<tr>' + _html + '<td colspan="2"></td></tr>');

                //$("#allSearch tbody").find('[data=class-tfoot]').css("position", "unset");
                //$("#allSearch tbody").find('[data=class-tfoot-2]').css("left", "653px");
                //settingFooter();
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
        var startTime;
        $("div.search-panel ul li").bind('click', function (e) {
            var div = $(this).closest("div");
            var text = $(this).text();
            var val = $(this).attr("val");
            div.find("button span:eq(0)").html(text);
            div.next().attr("condition", val);
        });

        $($.fn.dataTable.tables(true)).DataTable()
            .columns.adjust().on('preDraw', function () {
                startTime = new Date().getTime();
            }).on('draw.dt', function () {
                console.log('Redraw took at: ' + (new Date().getTime() - startTime) + 'mS');

                $("#allSearch tbody").find('[data=class-tfoot]').css("position", "unset");
                $("#allSearch tbody").find('[data=class-tfoot-2]').css("left", "653px");
            });
    };

    return {
        //main function to initiate the module
        init: function () {
            if (!jQuery().dataTable) {
                return;
            }

            oAllTable = initTableSearch("#allSearch");
            //console.log(oAllTable.fnGetData());
            conditionSearch();

            $("#btnAdvSearch").bind('click', function (e) {
                oAllTable.fnFilter();
                SettingHeader();
                oAllTable.fnGetData();
                conditionSearch();

                $.each(_Col, function (e, s) {
                    $("#allSearch").DataTable().column(s.Index).visible(s.visible);
                });

                settingFooter();
            });
        }
    };
}();


$(function () {
    SettingHeader();
    _currentPageExcel.init();
    fnDataTable.init();
    settingFooter();
});

function settingFooter() {
    let _countCol = $("#allSearch").find("thead tr th").length;
    let _html = '<td colspan="7" class="text-right dtfc-fixed-left" style="left: 0px; position: sticky;">รวมเงินภาคเรียนที่ 2</td>';
    _html += '<td data="class-tfoot-2" class="text-right dtfc-fixed-left" style="left: 653px; position: sticky;">10,000,000.00</td>';

    //for (i = 8; i < _countCol - 2; i++) {
    //    _html += '<td data="class-tfoot" class="text-right">10,000.00</td>';
    //}
    if (footer == "") return;
    let _footer = $.parseJSON(footer);

    $("#allSearch tbody").find('[data=row-tfoot]').remove();
    _html = '<td colspan="7" class="text-right dtfc-fixed-left" style="left: 0px; position: sticky;">' + _footer.wording_2 + '</td>';
    _html += '<td data="class-tfoot-2" class="text-right dtfc-fixed-left" style="left: 653px; position: sticky;">' + _footer.footer_SUM2.toLocaleString(undefined, { minimumFractionDigits: 2 }) + '</td>';
    if ($("#ddl-itemtype").val() == "2") {
        $.each(_footer.footer_2, function (e, s) {
            if (s.nPaymentID > 0) {
                _html += '<td data="class-tfoot" class="text-right">' + s.amount.toLocaleString(undefined, { minimumFractionDigits: 2 }) + '</td>';
            }
        });
    } else {
        $.each(_footer.footer_2, function (e, s) {
            if (s.nPaymentID == 0) {
                _html += '<td data="class-tfoot" class="text-right">' + s.amount.toLocaleString(undefined, { minimumFractionDigits: 2 }) + '</td>';
            }
        });
    }
    $("#allSearch").find('tbody').append('<tr data="row-tfoot">' + _html + '<td colspan="2"></td></tr>');

    //_html = '<td colspan="7" class="text-right dtfc-fixed-left" style="left: 0px; position: sticky;">' + _footer.wording_0 + '</td>';
    //_html += '<td data="class-tfoot-2" class="text-right dtfc-fixed-left" style="left: 653px; position: sticky;">' + _footer.footer_SUM0.toLocaleString(undefined, { minimumFractionDigits: 2 }) + '</td>';
    //if ($("#ddl-itemtype").val() == "2") {
    //    $.each(_footer.footer_0, function (e, s) {
    //        if (s.nPaymentID > 0) {
    //            _html += '<td data="class-tfoot" class="text-right">' + s.amount.toLocaleString(undefined, { minimumFractionDigits: 2 }) + '</td>';
    //        }
    //    });
    //}
    //else {
    //    $.each(_footer.footer_0, function (e, s) {
    //        if (s.nPaymentID == 0) {
    //            _html += '<td data="class-tfoot" class="text-right">' + s.amount.toLocaleString(undefined, { minimumFractionDigits: 2 }) + '</td>';
    //        }
    //    });
    //}

    //$("#allSearch").find('tbody').append('<tr data="row-tfoot">' + _html + '<td colspan="2"></td></tr>');

    //_html = '<td colspan="7" class="text-right dtfc-fixed-left" style="left: 0px; position: sticky;">' + _footer.wording_1 + '</td>';
    //_html += '<td data="class-tfoot-2" class="text-right dtfc-fixed-left" style="left: 653px; position: sticky;">' + _footer.footer_SUM1.toLocaleString(undefined, { minimumFractionDigits: 2 }) + '</td>';
    //if ($("#ddl-itemtype").val() == "2") {
    //    $.each(_footer.footer_1, function (e, s) {
    //        if (s.nPaymentID > 0) {
    //            _html += '<td data="class-tfoot" class="text-right">' + s.amount.toLocaleString(undefined, { minimumFractionDigits: 2 }) + '</td>';
    //        }
    //    });
    //} else {
    //    $.each(_footer.footer_1, function (e, s) {
    //        if (s.nPaymentID == 0) {
    //            _html += '<td data="class-tfoot" class="text-right">' + s.amount.toLocaleString(undefined, { minimumFractionDigits: 2 }) + '</td>';
    //        }
    //    });
    //}
    //$("#allSearch").find('tbody').append('<tr data="row-tfoot">' + _html + '<td colspan="2"></td></tr>');

    $("#allSearch tbody").find('[data=class-tfoot]').css("position", "unset");
    $("#allSearch tbody").find('[data=class-tfoot-2]').css("left", "653px");
}

var aoColumns = [];
var aoColumnDefs = [];
var _Col = [];

function SettingHeader() {
    aoColumns = [];
    aoColumnDefs = [];
    _Col = [];
    $("#allSearch thead tr th").remove();

    $("#allSearch thead tr").append("<th>ลำดับ</th>");
    $("#allSearch thead tr").append("<th>วันที่</th>");
    $("#allSearch thead tr").append("<th>เลขที่ใบเสร็จ</th>");
    $("#allSearch thead tr").append("<th>รหัสนักเรียน</th>");
    $("#allSearch thead tr").append("<th>ชื่อ-สกุล</th>");
    $("#allSearch thead tr").append("<th>ชั้นเรียน</th>");
    $("#allSearch thead tr").append("<th>ภาคเรียน</th>");
    //$("#allSearch thead tr").append("<th>ยอดเต็ม</th>");
    $("#allSearch thead tr").append("<th>ยอดรับชำระ</th>");

    aoColumns = [
        { "mData": "No", "sName": "No", "width": "50px" },
        { "mData": "PaymentDateString", "sName": "PaymentDateString", 'bSortable': true },
        { "mData": "InvoiceNumber", "sName": "InvoiceNumber", 'bSortable': true },
        { "mData": "StudentCode", "sName": "StudentCode", 'bSortable': true },
        { "mData": "StudentName", "sName": "StudentName", 'bSortable': true },
        {
            "mData": "StudentSubLevel", "sName": "StudentSubLevel", 'bSortable': true,
            render: function (data, type, row, meta) {
                if (row.StudentSubLevel2 != null) {
                    return row.StudentSubLevel + "/" + row.StudentSubLevel2;
                } else {
                    return row.StudentSubLevel;
                }
            }
        },
        {
            "mData": "StudentTerm", "sName": "StudentTerm", 'bSortable': true,
            render: function (data, type, row, meta) {
                return row.StudentTerm + "/" + row.StudentTermYear;
            }
        },
        //{ "mData": "GrandTotal", "sName": "GrandTotal", visible: false },
        {
            "mData": "Amount", "sName": "Amount", 'bSortable': true, "className": "text-right",
            render: function (data, type, row, meta) {
                let outstandingamount = row["Amount"];
                return outstandingamount.toLocaleString(undefined, { minimumFractionDigits: 2 })
            }
        }
        //{ "mData": "Payee", "sName": "Payee", "aTargets": [9] },
        //{ "mData": "Payee", "sName": "ReportDate", "aTargets": [10], visible: false },
        //{ "mData": "Payee", "sName": "ReportMonth", "aTargets": [11], visible: false },
        //{ "mData": "Payee", "sName": "ReportYear", "aTargets": [12], visible: false },
        //{ "mData": "Payee", "sName": "PaymentMethodId", "aTargets": [13], visible: false },
    ];

    aoColumnDefs.push({ "aTargets": [0], "bSortable": false });
    aoColumnDefs.push({ "aTargets": [1], "bSortable": true });
    aoColumnDefs.push({ "aTargets": [2], "bSortable": true });
    aoColumnDefs.push({ "aTargets": [3], "bSortable": true });
    aoColumnDefs.push({ "aTargets": [4], "bSortable": true });
    aoColumnDefs.push({ "aTargets": [5], "bSortable": true });
    aoColumnDefs.push({ "aTargets": [6], "bSortable": true });
    aoColumnDefs.push({ "aTargets": [7], "bSortable": true });

    let itemtype_0 = $("#ddl-itemtype").val() == "1";
    let itemtype_1 = $("#ddl-itemtype").val() == "2";

    $("#allSearch thead tr").append("<th>ค่าธรรมเนียมเรียน</th>");
    $("#allSearch thead tr").append("<th>ค่าธรรมเนียมอื่นๆ</th>");

    _Col.push({ Index: aoColumnDefs.length, visible: itemtype_0 });
    aoColumnDefs.push({ "aTargets": [aoColumnDefs.length], "bSortable": false });
    aoColumns.push({
        "mData": "ProductType_0", "sName": "ProductType_0", "className": "text-right", "visible": itemtype_0,
        render: function (data, type, row, meta) {
            let outstandingamount = row["ProductType_0"];
            return outstandingamount.toLocaleString(undefined, { minimumFractionDigits: 2 })
        }
    });

    _Col.push({ Index: aoColumnDefs.length, visible: itemtype_0 });
    aoColumnDefs.push({ "aTargets": [aoColumnDefs.length], "bSortable": false });
    aoColumns.push({
        "mData": "ProductType_1", "sName": "ProductType_1", "className": "text-right", "visible": itemtype_0,
        render: function (data, type, row, meta) {
            let outstandingamount = row["ProductType_1"];
            return outstandingamount.toLocaleString(undefined, { minimumFractionDigits: 2 })
        }
    });

    $.each(ProductJson, function (e, s) {
        $("#allSearch thead tr").append("<th>" + s.sPayment + "</th>");
        _Col.push({ Index: aoColumnDefs.length, visible: itemtype_1 });
        aoColumnDefs.push({ "aTargets": [aoColumnDefs.length], "bSortable": false, });
        aoColumns.push({
            "mData": "PR_" + s.nPaymentID, "sName": "PR_" + s.nPaymentID, "className": "text-right", visible: itemtype_1,
            render: function (data, type, row, meta) {
                let outstandingamount = row["PR_" + s.nPaymentID];
                return (outstandingamount ?? 0).toLocaleString(undefined, { minimumFractionDigits: 2 }) //+ "|PR_" + s.nPaymentID;
            }
        });
        //$("#allSearch").DataTable().column(aoColumnDefs.length - 1).visible(itemtype_1);
    });

    aoColumnDefs.push({ "aTargets": [aoColumnDefs.length], "bSortable": true });
    aoColumnDefs.push({ "aTargets": [aoColumnDefs.length], "bSortable": true });
    aoColumnDefs.push({ "aTargets": [aoColumnDefs.length], "bSortable": true });

    //$("#allSearch thead tr").append("<th></th>");
    //$("#allSearch thead tr").append("<th></th>");
    //$("#allSearch thead tr").append("<th></th>");
    //$("#allSearch thead tr").append("<th></th>");
    //$("#allSearch thead tr").append("<th></th>");
    //$("#allSearch thead tr").append("<th></th>");
    //$("#allSearch thead tr").append("<th></th>");
    $("#allSearch thead tr").append("<th>รับชำระโดย</th>");
    $("#allSearch thead tr").append("<th>ช่องทางการชำระ</th>");
    $("#allSearch thead tr").append("<th>วันที่ทำรายการ</th>");

    $("#allSearch thead tr").append("<th class=\"fix-right\"></th>");
    $("#allSearch thead tr").append("<th class=\"fix-right\"></th>");
    $("#allSearch thead tr").append("<th class=\"fix-right\"></th>");
    $("#allSearch thead tr").append("<th class=\"fix-right\"></th>");
    $("#allSearch thead tr").append("<th class=\"fix-right\"></th>");


    //aoColumns.push({ "mData": "Payee", "sName": "TermId", "aTargets": [14 + ProductJson.length], visible: false });
    //aoColumns.push({ "mData": "Payee", "sName": "TermId", "aTargets": [16 + ProductJson.length], visible: false });
    //aoColumns.push({ "mData": "Payee", "sName": "TermId", "aTargets": [17 + ProductJson.length], visible: false });
    aoColumns.push({ "mData": "Payee", "sName": "Payee" });
    aoColumns.push({ "mData": "PaymentMethod", "sName": "PaymentMethod" });
    aoColumns.push({
        "mData": "CreateDate", "sName": "CreateDate", render: function (data, type, row, meta) {
            if (row["CreatedDate"] == null) return "";
            const date = new Date(parseInt(row["CreatedDate"].substr(6)));
            return date.toLocaleString("th-TH", {
                day: "numeric",
                month: "short",
                year: "numeric",
                hour: "numeric",
                minute: "2-digit",
                second: "2-digit"
            }) + " น.";
        }
    });

    aoColumnDefs.push({ "mData": "Payee", "sName": "ReportDate", "aTargets": [aoColumnDefs.length], "bSortable": true, visible: false });
    aoColumnDefs.push({ "mData": "Payee", "sName": "ReportMonth", "aTargets": [aoColumnDefs.length], "bSortable": true, visible: false });
    aoColumnDefs.push({ "mData": "Payee", "sName": "ReportYear", "aTargets": [aoColumnDefs.length], "bSortable": true, visible: false });
    aoColumnDefs.push({ "mData": "Payee", "sName": "PaymentMethodId", "aTargets": [aoColumnDefs.length], "bSortable": true, visible: false });
    aoColumnDefs.push({ "mData": "Payee", "sName": "TermId", "aTargets": [aoColumnDefs.length], "bSortable": true, visible: false });

}
