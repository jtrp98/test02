var _currentTimestamp = $.now();
var oAllTable;
var oWaitingTable;
var oOverTable;
var oPaidTable;
var oVoidTable;
var _itemMasterData = [];
var _listMasterData = [
    //{ target: "#ddl-level", entity: "tlevel", label: "FullName", value: "TSubLevelId" },
    { target: "#ddl-paymentmethod", entity: "paymentmethod", label: "PaymentMethodName", value: "PaymentMethodId" },
    //{ target: "#ddl-bank", entity: "bank", label: "BankName", value: "BankId" }
];

function masterData(masterObject) {
    $.ajax({
        url: masterDataURL,
        type: "GET",
        data: "entity=" + masterObject.entity,
        success: function (result) {
            _itemMasterData[masterObject.entity] = result.Result;
            $(masterObject.target + " option").remove();
            $.each(result.Result, function (index, item) {
                var newOption = new Option(item[masterObject.label], item[masterObject.value], false, false);
                $(masterObject.target).append(newOption).trigger('change');
            });
            $(masterObject.target).val(null).trigger('change');
        }
    });
}

var _currentPage = function () {
    var initPage = function () {
        $.each(_listMasterData, function (index, item) { masterData(item); });

        $('#form-invocie-search').keydown(function (e) {
            var key = e.which;
            if (key === 13) {
                // As ASCII code for ENTER key is "13"
                $('#btnAdvSearch').click(); // Submit form code
                return false;
            }
        });

        $(".ddl-search-select2").select2({
            allowClear: true,
            placeholder: "เลือกเพื่อค้นหา"
        });


        $("#ddl-bank-type,#ddl-paymentmethod,#ddl-invoiceitem").select2({
            placeholder: "เลือกข้อมูล"
        });

        $('#btn-add-paymentmethod').on('click', function (e) {
            $("#modal-add-paymentmethod").modal();
        });

        $("input[type='radio'][name='rdPaymentType']").change(function () {
            $("#ul-paymentmethod li[data-paymentmethod][data-paymentmethod!='" + $(this).val() + "']").addClass("hide");
            $("#ul-paymentmethod li[data-paymentmethod][data-paymentmethod='" + $(this).val() + "']").removeClass("hide");
        });

        $(document).on('show.bs.modal', '.modal', function () {
            var zIndex = 900 + (10 * $('.modal:visible').length);
            $(this).css('z-index', zIndex);
            setTimeout(function () {
                $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
            }, 0);
        });

        $("#datePayment").daterangepicker({
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
            $("#ddl-term,#dateRange").addClass('hide');
            if ($(this).val() === "term")
                $("#ddl-term").removeClass('hide');
            else if ($(this).val() === "date" || $(this).val() === "duedate")
                $("#dateRange").removeClass('hide');
        });

        $('#ddl-level')
            .on('select2:unselect', function (e) {
                $("#ddl-sublevel").not('.select2-container').empty();
            })
            .on('select2:select', function (e) {
                $("#ddl-sublevel").not('.select2-container').empty();
                var data = e.params.data;
                $.ajax({
                    url: masterDataURL,
                    type: "GET",
                    data: "entity=tsublevel&id=" + data.id,
                    success: function (result) {
                        $.each(result.Result, function (index, item) {
                            var newOption = new Option(item.TSubLevel2, item.TermSubLevel2Id, false, false);
                            $("#ddl-sublevel").append(newOption).trigger('change');
                        });
                    }
                });
            });

        $.fn.modal.Constructor.prototype.enforceFocus = function () { };
        $('select').on('select2:open', function (e) {
            $('.custom-dropdown').parent().css('z-index', 99999);
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
            "ajax": {
                "type": "POST",
                "url": ajaxSearchHandlerUrl,
                "contentType": 'application/json;',
                'data': function (data) {
                    if ($("#PaymentMethodId").val() !== "")
                        data.columns[0].search.value = $('#PaymentMethodId').val();
                    if ($("#PaymentMethodName").val() !== "")
                        data.columns[1].search.value = $('#PaymentMethodName').val();
                    //if ($("#ddl-level").val() !== "")
                    //    data.columns[10].search.value = $('#ddl-level').val();
                    //if ($("#ddl-sublevel").val() !== "")
                    //    data.columns[11].search.value = $('#ddl-sublevel').val();


                    //if ($("#ddl-search-by").val() === "term" && $('#ddl-term').val() !== "")
                    //    data.columns[9].search.value = $('#ddl-term').val();
                    //else if ($("#ddl-search-by").val() === "date")
                    //    data.columns[3].search.value = $('#dateRange').val();
                    //else if ($("#ddl-search-by").val() === "duedate")
                    //    data.columns[4].search.value = $('#dateRange').val();

                    //if (tableId === "#waitingSearch") {
                    //    data.columns[8].search.value = 0;
                    //} else if (tableId === "#overSearch") {
                    //    data.columns[4].search.value = overDueDate;
                    //} else if (tableId === "#paidSearch") {
                    //    data.columns[7].search.value = invoicePaidStatus;
                    //} else if (tableId === "#cancelSearch") {
                    //    data.columns[7].search.value = invoiceVoidStatus;
                    //}

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
                "zeroRecords": "ไม่พบข้อมูล",
                "info": "แสดงข้อมูล หน้า _PAGE_ ถึง _PAGES_",
                "infoEmpty": "ไม่พบข้อมูล",
                "infoFiltered": "(จำนวนข้อมูลทั้งหมด _MAX_)"
            },
            //"oLanguage": {
            //    "sSearch": '<a class="btn btn-default " id="searchBtn" style="float:right;height: 30px;" > <i class="fa fa-search"></i></a>'
            //},
            "aoColumns": [
                //{ "mData": "PaymentMethodId", "sName": "PaymentMethodId", "aTargets": [0] },
                { "mData": "PaymentMethodTypeName", "sName": "PaymentMethodTypeName", "aTargets": [0] },
                { "mData": "BankName", "sName": "BankName", "aTargets": [1] },
                { "mData": "PaymentMethodName", "sName": "PaymentMethodName", "aTargets": [2] },
                { "mData": "BankNumber", "sName": "BankNumber", "condition": "1", "aTargets": [3] },
                { "mData": "Note", "sName": "Note", "aTargets": [4] },
                //{
                //    "mData": "CreateDate", "sName": "CreateDate", "aTargets": [3],
                //    render: function (data, type, row, meta) {
                //        return moment(row.CreateDate).format("DD/MM/YYYY HH:mm:ss");
                //    }
                //},
                //{
                //    "mData": "DueDate", "sName": "DueDate", "aTargets": [4],
                //    render: function (data, type, row, meta) {
                //        return moment(row.DueDate).format("DD/MM/YYYY");
                //    }
                //},
                //{
                //    "mData": "GrandTotal", "sName": "GrandTotal", "aTargets": [5],
                //    render: function (data, type, row, meta) {
                //        if (row.GrandTotal !== null)
                //            return row.GrandTotal.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                //        else
                //            return "0.00";
                //    }
                //},
                //{
                //    "mData": "OutstandingAmount", "sName": "OutstandingAmount", "aTargets": [6],
                //    render: function (data, type, row, meta) {
                //        if (row.GrandTotal !== null)
                //            return row.OutstandingAmount.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                //        else
                //            return "0.00";
                //    }
                //},
                ////{ "mData": "OutstandingAmount", "sName": "O.00utstandingAmount", "aTargets": [5] },
                //{ "mData": "DisplayStatus", "sName": "InvoiceStatus", "aTargets": [7] },
                //{ "mData": "PaidStatus", "sName": "PaidStatus", "aTargets": [8], "bVisible": false },
                //{ "mData": "InvoiceId", "sName": "TermId", "aTargets": [9], "bVisible": false },
                //{ "mData": "InvoiceId", "sName": "LevelId", "aTargets": [10], "bVisible": false },
                //{ "mData": "InvoiceId", "sName": "SubLevelId", "aTargets": [11], "bVisible": false },
                {
                    "mData": "PaymentMethodId", "sName": "action", "aTargets": [5], "className": "fix-right",
                    render: function (data, type, row, meta) {
                        var content = '';
                        content += '<a class="btn btn-primary btn-lg"  data-id="' + row.PaymentMethodId + '"  role="button" target="_blank" onclick="getPaymentMethod(\'' + row.PaymentMethodId + '\')">แก้ไขข้อมูล</a>&nbsp;';
                        content += '<a class="btn btn-danger btn-lg"  data-id="' + row.PaymentMethodId + '" target="_blank" href="deletePaymentMethod(\'' + row.PaymentMethodId + '\')">ลบข้อมูล</a>&nbsp;';
                        return content;
                    }
                }
            ],
            "aocolumndefs": [{  // set default column settings
                'bsortable': false,
                'targets': [5]
            }, {
                "searchable": false,
                "targets": [5]
            }],
            "order": [0, "desc"],
            "fndrawcallback": function () {

                $("li a[type=delete]").bind('click', function (e) { //delete by item
                    console.log($(this).attr("data"));
                });
            }
        });

        //oTable.find('.group-checkable').change(function () {
        //    var set = jQuery(this).attr("data-set");
        //    var checked = jQuery(this).is(":checked");
        //    jQuery(set).each(function () {
        //        if (checked) {
        //            $(this).prop("checked", true);
        //            $(this).parents('tr').addClass("active");
        //        } else {
        //            $(this).prop("checked", false);
        //            $(this).parents('tr').removeClass("active");
        //        }
        //    });
        //});
        //$('#data-list input[type=search]').addClass("form-control input-sm");
        //$('#data-list select').addClass("form-control input-sm");
        //oTable.on('change', 'tbody tr .checkboxes', function () { //checkbox all
        //    $(this).parents('tr').toggleClass("active");
        //});

        //$(tableId + '_filter input').unbind();  //disable keytext search
        //$(tableId + '_filter input').bind('keyup', function (e) { //enter for keytext search
        //    if (e.keyCode === 13) {
        //        oTable.fnFilter(this.value);
        //    }
        //});

        //$("#btnAdvSearch").bind('click', function (e) {
        //    e.preventDefault();
        //    $("#btnAdvSearch").data('timestamp', $.now());
        //    switch ($("#ul-table li.active").data('table')) {
        //        case "allSearch":
        //            $("#allSearch").data('timestamp', $("#btnAdvSearch").data('timestamp'));
        //            oAllTable.fnFilter();
        //            break;
        //        case "waitingSearch":
        //            $("#waitingSearch").data('timestamp', $("#btnAdvSearch").data('timestamp'));
        //            oWaitingTable.fnFilter();
        //            break;
        //        case "overSearch":
        //            $("#overSearch").data('timestamp', $("#btnAdvSearch").data('timestamp'));
        //            oOverTable.fnFilter();
        //            break;
        //        case "paidSearch":
        //            $("#paidSearch").data('timestamp', $("#btnAdvSearch").data('timestamp'));
        //            oPaidTable.fnFilter();
        //            break;
        //        case "cancelSearch":
        //            $("#cancelSearch").data('timestamp', $("#btnAdvSearch").data('timestamp'));
        //            oVoidTable.fnFilter();
        //            break;
        //    }
        //    //console.log($(oTable).attr('id'));
        //    // $("#" + ).fnFilter();
        //    // $('.nav-tabs a:first').tab('show');
        //});
        //$("#btRemove").click(function () {

        //    var set = oTable.find('.group-checkable');
        //    var data = [];
        //    oTable.find('.checkboxes:checked').each(function () {
        //        data.push({ id: $(this).val() });
        //    });
        //});

        ////$("#searchBtn").click(function () {
        ////    var value = $("#tableSearch_filter input").val();
        ////    oTable.fnFilter(value);
        ////});

        //$("#resetBtn").click(function () {
        //    oTable.fnFilter("");
        //});

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
            $("#ul-table li.active").data('timestamp', _currentTimestamp);
            $("#btnAdvSearch").data('timestamp', _currentTimestamp);

            oAllTable = initTableSearch("#allSearch");
            //    $("#ul-table li").click(function () {
            //        console.log($(this).data('table'));
            //        if ($(this).data('timestamp') === "") {
            //            $(this).data('timestamp', $("#btnAdvSearch").data('timestamp'));
            //            switch ($(this).data('table')) {
            //                case "allSearch":
            //                    oAllTable = initTableSearch("#" + $(this).data('table'));
            //                    break;
            //                case "waitingSearch":
            //                    oWaitingTable = initTableSearch("#" + $(this).data('table'));
            //                    break;
            //                case "overSearch":
            //                    oOverTable = initTableSearch("#" + $(this).data('table'));
            //                    break;
            //                case "paidSearch":
            //                    oPaidTable = initTableSearch("#" + $(this).data('table'));
            //                    break;
            //                case "cancelSearch":
            //                    oVoidTable = initTableSearch("#" + $(this).data('table'));
            //                    break;
            //            }
            //        }
            //        else if ($(this).data('timestamp') !== $("#btnAdvSearch").data('timestamp')) {
            //            $(this).data('timestamp', $("#btnAdvSearch").data('timestamp'));
            //            switch ($(this).data('table')) {
            //                case "allSearch":
            //                    oAllTable.fnFilter();
            //                    break;
            //                case "waitingSearch":
            //                    oWaitingTable.fnFilter();
            //                    break;
            //                case "overSearch":
            //                    oOverTable.fnFilter();
            //                    break;
            //                case "paidSearch":
            //                    oPaidTable.fnFilter();
            //                    break;
            //                case "cancelSearch":
            //                    oVoidTable.fnFilter();
            //                    break;
            //            }
            //        }
            //    });
            //    conditionSearch();

        }
    };
}();

$(function () {
    _currentPage.init();
    fnDataTable.init();
    //fnInvoicePaymentModal.init();
    //fnInvoicePaymentMethodModel.init();
});
$(document)
    .ajaxStart(function () {
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
    })
    .ajaxStop($.unblockUI);