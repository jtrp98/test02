var _currentTimestamp = $.now();
var oAllTable;
var oWaitingTable;
var oOverTable;
var oPaidTable;
var oVoidTable;
var _itemMasterData = [];
var _listMasterData = [
    { target: "#ddl-level", entity: "tlevel", label: "SubLevel", value: "TSubLevelId" },
    { target: "#ddl-paymentmethod", entity: "paymentmethod", label: "PaymentMethodName", value: "PaymentMethodId" },
    { target: "#ddl-bank", entity: "bank", label: "BankName", value: "BankId" }
];

function PopupInvoicePayment(element) {
    $("#modal-invoice-number").text($(element).data('number'));
    $("#invoice-id").val($(element).data('id'));
    $("#invoice-code").val($(element).data('code'));
    $("#modal-invoice-payment").modal();
}

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

        $("#btnPrint").click(function () {
            var TermSubLevel2Id = $("#ddl-sublevel").val();
            var TermId = $("#ddl-term").val();
            if (TermSubLevel2Id === null) {
                $.confirm({
                    title: '<h3>แจ้งเตือน</h3>',
                    content: "<h3>กรุณาเลือกชั้นเรียนที่ต้องการพิมพ์ใบแจ้งหนี้</h3>",
                    theme: 'bootstrap',
                    buttons: {
                        "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                        }
                    }
                });
            } else {
                window.open("/Modules/Invoices/PrintInvoiceGroupPreview.aspx?TermSubLevel2Id=" + TermSubLevel2Id + "&TermId=" + TermId, "_blank");
            }
        });

        //$('#studentcode').autocomplete({
        //    width: 300,
        //    max: 10,
        //    delay: 100,
        //    minLength: 1,
        //    autoFocus: true,
        //    cacheLength: 1,
        //    scroll: true,
        //    highlight: false,
        //    source: function (request, response) {
        //        $.get("/App_Logic/autoCompleteName.ashx?mode=ListStudent&wording=" + request.term, function (data) {
        //            //PageMethods.GetUser(request.term, function (data) {
        //            let _d = [];
        //            $.each(data, function (e, s) {
        //                _d.push({
        //                    label: s.User_Name,
        //                    value: s.User_Id,
        //                    User_Type: s.User_Type,
        //                })
        //            });
        //            response(_d);
        //        })
        //    },
        //    select: function (event, ui) {
        //        event.preventDefault();
        //        $("#studentcode").val(ui.item.label);
        //    },
        //    focus: function (event, ui) {
        //        event.preventDefault();
        //        //$("#txtid").val("");
        //    }
        //});

        function termData() {
            $.ajax({
                url: masterDataURL,
                type: "GET",
                data: "entity=year",
                success: function (result) {
                    $.each(result.Result, function (index, item) {
                        var newOption = new Option(item.Number, item.Number, false, false);
                        $("#ddl-term").append(newOption).trigger('change');
                    });
                }
            });
        }

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

        $("#dateRange").daterangepicker({
            singleDatePicker: false,
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

        termData();

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

var _rmInvoiceId = [];
var _rmAll_InvoiceId = [];
var fnDataTable = function () {
    var initTableSearch = function (tableId) {
        var oTable = $(tableId);
        // begin first table
        oTable.dataTable({
            "processing": true,
            "serverSide": true,
            "pageLength": 50,
            "deferRender": true,
            "searching": false,
            "paging": true,
            "ajax": {
                "type": "POST",
                "url": ajaxSearchHandlerUrl,
                "contentType": 'application/json;',
                "dataSrc": function (response) {
                    roleList = response;
                    console.log(roleList);
                    _rmAll_InvoiceId = response.aIds;
                    return response.data;
                },
                'data': function (data) {
                    if ($("#invoiceid").val() !== "")
                        data.columns[2].search.value = $('#invoiceid').val();
                    //if ($("#studentcode").val() !== "")
                    //    data.columns[5].search.value = $('#studentcode').val();
                    if ($("#ddl-level").val() !== "")
                        data.columns[16].search.value = $('#ddl-level').val();
                    if ($("#ddl-sublevel").val() !== "")
                        data.columns[18].search.value = $('#ddl-sublevel').val();

                    if ($("#ddl-search-by").val() === "term" && $('#ddl-term').val() !== "")
                        data.columns[14].search.value = $('#ddl-term').val();
                    else if ($("#ddl-search-by").val() === "date")
                        data.columns[8].search.value = $('#dateRange').val();
                    else if ($("#ddl-search-by").val() === "duedate")
                        data.columns[9].search.value = $('#dateRange').val();

                    if (tableId === "#waitingSearch") {
                        data.columns[13].search.value = 0;
                    } else if (tableId === "#overSearch") {
                        data.columns[9].search.value = overDueDate;
                    } else if (tableId === "#paidSearch") {
                        data.columns[12].search.value = invoicePaidStatus;
                    } else if (tableId === "#cancelSearch") {
                        data.columns[12].search.value = invoiceVoidStatus;
                    }

                    data.columns[20].search.value = $("#ddl-optionCourse").val();//optionCourse
                    data.columns[21].search.value = null;//sStudentID
                    data.columns[22].search.value = $("#studentcode").val();//registerCode
                    data.columns[3].search.value = $("#ddl-term").val();//TermYear
                    data.columns[23].search.value = "register";

                    data.columns[19].search.value = "0";

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
                {
                    "mData": "Term", "sName": "Term", "orderable": false, className: "text-center",
                    render: function (data, type, row, meta) {
                        if (row.PaidStatus || row.InvoiceStatus != "Approve") {
                            return "";
                        } else {
                            let _checked = "checked";
                            if (_rmInvoiceId.length == 0 || _rmInvoiceId.find(f => f == row.InvoiceId) === undefined) {
                                _checked = "";
                            }
                            return "<input type=\"checkbox\" data-invoice-id=\"" + row.InvoiceId + "\" " + _checked + " name=\"InvoiceId\" />";
                        }
                    }, "bVisible": false
                },
                {
                    "mData": "Term", "sName": "Term", "orderable": false, className: "text-center",
                    render: function (data, type, row, meta) {
                        return meta.settings._iDisplayStart + meta.row + 1;
                    }
                },
                { "mData": "Code", "sName": "Code", "aTargets": [0] },
                { "mData": "TermYear", "sName": "TermYear", "orderable": false },
                { "mData": "Term", "sName": "Term", "orderable": false, "bVisible": false },
                { "mData": "sStudentID", "sName": "StudentCode", "aTargets": [1], "bVisible": true },
                {
                    "mData": "Date", "sName": "Date", "orderable": false,
                    render: function (data, type, row, meta) {
                        return row.SubLevel + ' / ' + row.SubLevel2;
                    }, "bVisible": false
                },
                { "mData": "StudentName", "sName": "StudentName", "condition": "1", "aTargets": [2] },
                {
                    "mData": "Date", "sName": "Date", "aTargets": [3],
                    render: function (data, type, row, meta) {
                        return moment(row.Date).format("DD/MM/YYYY");
                    }
                },
                {
                    "mData": "DueDate", "sName": "DueDate", "aTargets": [4],
                    render: function (data, type, row, meta) {
                        return moment(row.DueDate).format("DD/MM/YYYY");
                    }
                },
                {
                    "mData": "GrandTotal", "sName": "GrandTotal", "aTargets": [5],
                    render: function (data, type, row, meta) {
                        if (row.GrandTotal !== null)
                            return row.GrandTotal.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        else
                            return "0.00";
                    }
                },
                {
                    "mData": "OutstandingAmount", "sName": "OutstandingAmount", "aTargets": [6],
                    render: function (data, type, row, meta) {
                        if (row.GrandTotal !== null)
                            return row.OutstandingAmount.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        else
                            return "0.00";
                    }
                },
                //{ "mData": "OutstandingAmount", "sName": "O.00utstandingAmount", "aTargets": [5] },
                { "mData": "DisplayStatus", "sName": "InvoiceStatus", "aTargets": [8] },
                { "mData": "PaidStatus", "sName": "PaidStatus", "aTargets": [9], "bVisible": false },
                { "mData": "InvoiceId", "sName": "TermId", "aTargets": [10], "bVisible": false },
                { "mData": "InvoiceId", "sName": "LevelId", "aTargets": [11], "bVisible": false },
                { "mData": "InvoiceId", "sName": "SubLevelId", "aTargets": [12], "bVisible": false },
                {
                    "mData": "InvoiceId", "sName": "action", "aTargets": [13], "className": "fix-right",
                    render: function (data, type, row, meta) {
                        var content = '';
                        let btnPayment = '';
                        if (row.InvoiceStatus === invoiceApproveStatus) {
                            content += '<li><a target="_blank" style=\"font-size:20px;\" data-id="' + row.InvoiceId + '" data-code="' + row.InvoiceCode + '"  href="/Modules/Invoices/perRegisterPrintBillPreview.aspx?InvoiceId=' + row.InvoiceId + '">ใบแจ้งหนี้</a></li>';
                            //content += '<li><a href=\"#\" style=\"font-size:20px;\" data-id="' + row.InvoiceId + '" data-code="' + row.InvoiceCode + '" data-number="' + row.Code + '" onclick="PopupInvoicePayment(this)">รับชำระ</a></li>';
                            btnPayment += '&nbsp;<a href=\"#\" style=\"font-size:20px;\" class="btn btn-primary btn-lg" data-id="' + row.InvoiceId + '" data-code="' + row.InvoiceCode + '" data-number="' + row.Code + '" onclick="PopupInvoicePayment(this)">รับชำระ</a>';
                        }
                        //else if (row.InvoiceStatus === invoicePaidStatus)
                        //    content += '<a target="_blank" class="btn btn-warning btn-lg"  data-id="' + row.InvoiceId + '" data-number="' + row.Code + '" href="/Modules/Invoices/PrintBillPreview.aspx?InvoiceId=' + row.InvoiceId + '">ใบเสร็จ</button>&nbsp;';

                        //if ((row.InvoiceStatus === invoiceDraftStatus || row.InvoiceStatus === invoiceApproveStatus)
                        //    && (row.PaidStatus !== null && row.PaidStatus === false))
                        if (row.OutstandingAmount != 0)
                            content += '<li><a data-id="' + row.InvoiceId + '" style=\"font-size:20px;\"  role="button" data-number="' + row.Code + '" target="_blank" href="/Modules/Invoices/perRegisterCreate.aspx?Mode=Edit&InvoiceId=' + row.InvoiceId + '">แก้ไข</a></li>';

                        content += '<li><a data-id="' + row.InvoiceId + '" style=\"font-size:20px;\" data-number="' + row.Code + '" target="_blank" href="/Modules/Invoices/perRegisterCreate.aspx?InvoiceId=' + row.InvoiceId + '">ข้อมูล</a></li>';

                        if (content !== '') {
                            content = "<div class=\"dropdown\" style=\"display: inline;\"><button class=\"btn btn-info dropdown-toggle\" type=\"button\" data-toggle=\"dropdown\" ><i class=\"fa fa-th-list\"></i> <span class=\"caret\"></span></button > <ul class=\"dropdown-menu\">" + content + "</ul></div>";
                        }
                        return content + btnPayment;
                    }
                },
                { "mData": "InvoiceId", "sName": "SubLevel2Id", "aTargets": [14], "bVisible": false },
                { "mData": "preRegisterId", "sName": "preRegisterId", "aTargets": [15], "bVisible": false },
                { "mData": "optionCourse", "sName": "optionCourse", "aTargets": [15], "bVisible": false },
                { "mData": "sStudentID", "sName": "sStudentID", "aTargets": [15], "bVisible": false },
                { "mData": "registerCode", "sName": "registerCode", "aTargets": [15], "bVisible": false },
                { "mData": "InvoiceId", "sName": "InvoiceType", "aTargets": [14], "bVisible": false },
            ],
            "aoColumnDefs": [{  // set default column settings
                'bSortable': false,
                'targets': [15]
            }, {
                "searchable": false,
                "targets": [15]
            }],
            "order": [2, "desc"],
            "fnDrawCallback": function (nRow, aData, iDisplayIndex, Page) {
                $("li a[type=delete]").bind('click', function (e) { //delete by item
                    console.log($(this).attr("data"));
                });

                $("input[name=InvoiceId]").bind("click", function () {
                    if ($(this).prop("checked")) {
                        _rmInvoiceId.push(parseInt($(this).attr("data-invoice-id")));
                        console.log(_rmInvoiceId);
                    } else {
                        delete _rmInvoiceId[_rmInvoiceId.findIndex(f => f == $(this).attr("data-invoice-id"))];
                        let data = _rmInvoiceId;
                        _rmInvoiceId = [];
                        data.filter(key => {
                            if (key !== $(this).attr("data-invoice-id")) {
                                _rmInvoiceId.push(key);
                            }
                        });
                        console.log(_rmInvoiceId);
                    }

                    if ($(this).prop("checked") && _rmInvoiceId.filter(f => f).length == _rmAll_InvoiceId.length) {
                        $("[name=InvoiceId-all]").prop("checked", true);
                    } else {
                        $("[name=InvoiceId-all]").prop("checked", false);
                    }
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

        oTable.find('[name=InvoiceId-all]').change(function () {
            oTable.find("[name=InvoiceId]").prop("checked", $(this).prop("checked"));
            if ($(this).prop("checked")) {
                _rmInvoiceId = _rmAll_InvoiceId;
            } else {
                _rmInvoiceId = [];
            }
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
            e.preventDefault();
            $("[name=InvoiceId-all]").prop("checked", false);
            _rmInvoiceId = [];

            $("#btnAdvSearch").data('timestamp', $.now());
            switch ($("#ul-table li.active").data('table')) {
                case "allSearch":
                    $("#allSearch").data('timestamp', $("#btnAdvSearch").data('timestamp'));
                    oAllTable.fnFilter();
                    break;
                case "waitingSearch":
                    $("#waitingSearch").data('timestamp', $("#btnAdvSearch").data('timestamp'));
                    oWaitingTable.fnFilter();
                    break;
                case "overSearch":
                    $("#overSearch").data('timestamp', $("#btnAdvSearch").data('timestamp'));
                    oOverTable.fnFilter();
                    break;
                case "paidSearch":
                    $("#paidSearch").data('timestamp', $("#btnAdvSearch").data('timestamp'));
                    oPaidTable.fnFilter();
                    break;
                case "cancelSearch":
                    $("#cancelSearch").data('timestamp', $("#btnAdvSearch").data('timestamp'));
                    oVoidTable.fnFilter();
                    break;
            }
            //console.log($(oTable).attr('id'));
            // $("#" + ).fnFilter();
            // $('.nav-tabs a:first').tab('show');
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
            $("#ul-table li.active").data('timestamp', _currentTimestamp);
            $("#btnAdvSearch").data('timestamp', _currentTimestamp);

            oAllTable = initTableSearch("#allSearch");
            $("#ul-table li").click(function () {
                console.log($(this).data('table'));
                if ($(this).data('timestamp') === "") {
                    $(this).data('timestamp', $("#btnAdvSearch").data('timestamp'));
                    switch ($(this).data('table')) {
                        case "allSearch":
                            oAllTable = initTableSearch("#" + $(this).data('table'));
                            break;
                        case "waitingSearch":
                            oWaitingTable = initTableSearch("#" + $(this).data('table'));
                            break;
                        case "overSearch":
                            oOverTable = initTableSearch("#" + $(this).data('table'));
                            break;
                        case "paidSearch":
                            oPaidTable = initTableSearch("#" + $(this).data('table'));
                            break;
                        case "cancelSearch":
                            oVoidTable = initTableSearch("#" + $(this).data('table'));
                            break;
                    }
                }
                else if ($(this).data('timestamp') !== $("#btnAdvSearch").data('timestamp')) {
                    $(this).data('timestamp', $("#btnAdvSearch").data('timestamp'));
                    switch ($(this).data('table')) {
                        case "allSearch":
                            oAllTable.fnFilter();
                            break;
                        case "waitingSearch":
                            oWaitingTable.fnFilter();
                            break;
                        case "overSearch":
                            oOverTable.fnFilter();
                            break;
                        case "paidSearch":
                            oPaidTable.fnFilter();
                            break;
                        case "cancelSearch":
                            oVoidTable.fnFilter();
                            break;
                    }
                }
            });
            conditionSearch();

        }
    };
}();

var fnInvoicePaymentModal = function () {

    var initHandles = function () {
        $("#chk-all").change(function () {
            if ($(this).is(":checked"))
                $(".chk-paid").prop("checked", true);
            else
                $(".chk-paid").prop("checked", false);
        });
        $('#modal-invoice-payment').on('shown.bs.modal', function (e) {
            $("#lbremainpaymentamount").text("0.00");
            $("#lbpaymentnetamount").text("0.00");
            $("#chk-all").prop("checked", true);
            $(".chk-paid").prop("checked", true);
            $("#PaidAmount").val('');
            masterData({ target: "#ddl-paymentmethod", entity: "paymentmethod", label: "PaymentMethodName", value: "PaymentMethodId" });
            $.ajax({
                url: getInvoiceUrl,
                type: "GET",
                data: "invoice_id=" + $("#modal-invoice-payment #invoice-id").val(),
                success: function (result) {
                    var rowTemplate = "<tr>  \
                             <td><input type = \"hidden\" class=\"chk-paid\" value=\"{product-id}\" name=\"ItemsPaid[][InvoiceItemId]\" />\
                            <input type = \"checkbox\" class=\"chk-paid\" data-id=\"{product-id}\" name=\"ItemsPaid[][IsChecked]\" value=\"true\" checked/></td >\
                            <td>{product-name}</td>\
                            <td>{accounting-name}</td>\
                            <td>{total-amount}</td>\
                            <td>{outstadingamount}</td>\
                        </tr>  ";
                    $("#tb-invoice-item tbody").html('');
                    let lbremainpaymentamount = parseFloat(result.Result.OutstandingAmount !== null ? result.Result.OutstandingAmount.toFixed(2) : 0.00);
                    let lbremainpaymentamount_1 = parseFloat(result.Result.ManualDiscount !== null ? result.Result.ManualDiscount.toFixed(2) : 0.00) +
                        parseFloat(result.Result.TotalDiscount !== null ? result.Result.TotalDiscount.toFixed(2) : 0.00);
                    let lbremainpaymentamount_0 = parseFloat(result.Result.GrandTotal !== null ? result.Result.GrandTotal.toFixed(2) : 0.00) + lbremainpaymentamount_1;
                    let lbremainpaymentamount_2 = 0;

                    $.each(result.Result.PaidPayments, function (e, s) {
                        if (s.Status !== "Void") {
                            lbremainpaymentamount_2 += parseFloat(s.Amount !== null ? s.Amount.toFixed(2) : 0.00);
                        }
                    });

                    $("#lbremainpaymentamount").text(lbremainpaymentamount.toLocaleString("en-US", { maximumFractionDigits: 2, minimumFractionDigits: 2 }));
                    $("#lbremainpaymentamount_0").text(lbremainpaymentamount_0.toLocaleString("en-US", { maximumFractionDigits: 2, minimumFractionDigits: 2 }));
                    $("#lbremainpaymentamount_1").text(lbremainpaymentamount_1.toLocaleString("en-US", { maximumFractionDigits: 2, minimumFractionDigits: 2 }));
                    $("#lbremainpaymentamount_2").text(lbremainpaymentamount_2.toLocaleString("en-US", { maximumFractionDigits: 2, minimumFractionDigits: 2 }));

                    $("#termName").val(result.Result.TermYear + ' / ' + result.Result.Term);
                    $("#classtName").val(result.Result.SubLevel);
                    $("#studentName").val(result.Result.StudentName);

                    $.each(result.Result.InvoiceItems, function (index, item) {
                        var currentRow = rowTemplate;
                        currentRow = currentRow.replace("{product-id}", item.InvoiceItemId);
                        currentRow = currentRow.replace("{product-name}", item.ProductName);
                        currentRow = currentRow.replace("{accounting-name}", "-");
                        currentRow = currentRow.replace("{total-amount}", item.Amount !== null ? item.Amount.toLocaleString("en-US", { maximumFractionDigits: 2, minimumFractionDigits: 2 }) : 0.00);

                        if (item.OutstandingAmount !== null && item.OutstandingAmount > 0)
                            currentRow = currentRow.replace("{outstadingamount}", item.OutstandingAmount.toLocaleString("en-US", { maximumFractionDigits: 2, minimumFractionDigits: 2 }));
                        else
                            currentRow = currentRow.replace("{outstadingamount}", "-");

                        $("#tb-invoice-item tbody").append(currentRow);
                        if (item.OutstandingAmount === 0)
                            $("#tb-invoice-item tbody tr:last .chk-paid[type='checkbox']").prop('disabled', true);
                    });

                }
            });
            // do something...
        });
    };

    var formValidation = function () {
        var mainForm = $('#invoice-payment-form');
        mainForm.validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: true, // do not focus the last invalid input
            ignore: "",  // validate all fields including form hidden input          
            errorPlacement: function (error, element) {
                console.log(error);
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

                $('#invoice-form :disabled').each(function (index, formDisabledElement) {
                    if (!(formDisabledElement.name in data))
                        data[formDisabledElement.name] = $(formDisabledElement).val();
                });

                $.ajax({
                    url: savePaymentUrl,
                    type: 'POST',
                    data: { invoice: JSON.stringify(data) },
                    dataType: "json",
                    success: function (resultData, textStatus, xhr) {
                        if (resultData.StatusCode === "200" && resultData.Result !== null) {
                            $.confirm({
                                title: '<h3>แจ้งเตือน</h3>',
                                content: '<h3>ท่านต้องการพิมพ์ใบเสร็จหรือไม่</h3>',
                                theme: 'bootstrap',
                                buttons: {
                                    "<span style=\"font-size: 20px;\">ตกลง</span>": function () {
                                        window.open("/Modules/Invoices/perRegisterPrintBillPreview.aspx?InvoiceId=" + data.InvoiceId + "&paidPaymentId=" + resultData.Result, "_blank");
                                        $("#btnAdvSearch").click();
                                        $('#modal-invoice-payment').modal('hide');
                                    }, "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                                        $("#btnAdvSearch").click();
                                        $('#modal-invoice-payment').modal('hide');
                                    }
                                }
                            });
                        } else {
                            $.confirm({
                                title: '<h3>แจ้งเตือน</h3>',
                                content: "<h3>" + resultData.Message + "</h3>",
                                theme: 'bootstrap',
                                buttons: {
                                    "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                                        $("#btnAdvSearch").click();
                                        $('#modal-invoice-payment').modal('hide');
                                    }
                                }
                            });
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        $.confirm({
                            title: '<h3>แจ้งเตือน</h3>',
                            content: "<h3>พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ</h3>",
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

        $("#PaidAmount").rules("add", {
            required: true,
            number: true,
            min: 0.01
        });

        $("#PaidAmount").focusout(function () {
            if ($.isNumeric($(this).val())) {
                $(this).val(parseFloat($(this).val()).toFixed(2));
            } else {
                $(this).val("0.00");
            }
        });

        $("#PaidAmount").keyup(function () {
            $("#lbpaymentnetamount").text(parseFloat($(this).val()).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        });
    };

    return {
        //main function to initiate the module
        init: function () {
            initHandles();
            formValidation();
        }
    };
}();

var fnInvoicePaymentMethodModel = function () {
    // btnSavePaymentMethod
    var initHandles = function () {
        $('#modal-add-paymentmethod').on('shown.bs.modal', function (e) {
            $('#payment-method-1')[0].reset();
            $('#payment-method-2')[0].reset();
            $('#payment-method-3')[0].reset();
            $.fn.modal.Constructor.prototype.enforceFocus = function () { };
            $('select').on('select2:open', function (e) {
                $('.custom-dropdown').parent().css('z-index', 99999);
            });
        });

        $('#btnSavePaymentMethod').on('click', function (e) {
            var numberPaymentmethodForm = $("li.li-paymentmethod:not(.hide)").data('paymentmethod');
            //console.log(numberPaymentmethodForm);
            $("#payment-method-" + numberPaymentmethodForm).submit();
        });
    };

    var formValidation = function (formId) {
        var mainForm = $(formId);
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
                console.log(data);
                $.ajax({
                    url: savePaymentMethodUrl,
                    type: 'POST',
                    data: { invoice: JSON.stringify(data) },
                    dataType: "json",
                    success: function (data, textStatus, xhr) {

                        if (data.StatusCode === "200" && data.Result) {
                            $.confirm({
                                title: '<h3>แจ้งเตือน</h3>',
                                content: '<h3>บันทึกช่องทางการชำระเงินสำเร็จ</h3>',
                                theme: 'bootstrap',
                                buttons: {
                                    "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                                        masterData({ target: "#ddl-paymentmethod", entity: "paymentmethod", label: "PaymentMethodName", value: "PaymentMethodId" });
                                        $('#modal-add-paymentmethod').modal('hide');
                                    }
                                }
                            });
                        } else {
                            $.confirm({
                                title: '<h3>แจ้งเตือน</h3>',
                                content: "<h3>" + data.Message + "</h3>",
                                theme: 'bootstrap',
                                buttons: {
                                    "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                                    }
                                }
                            });
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        $.confirm({
                            title: '<h3>แจ้งเตือน</h3>',
                            content: "<h3>พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ</h3>",
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
    };

    return {
        //main function to initiate the module
        init: function () {
            initHandles();
            formValidation("#payment-method-1");
            formValidation("#payment-method-2");
            formValidation("#payment-method-3");
        }
    };
}();

function funcInvoicePrint(invoiceId) {
    $.ajax({
        url: invoicePrintPreviewUrl,
        type: "GET",
        success: function (result) {

        }
    });
}

$(function () {
    _currentPage.init();
    fnDataTable.init();
    fnInvoicePaymentModal.init();
    fnInvoicePaymentMethodModel.init();
});
$(document)
    .ajaxStart(function () {
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
    })
    .ajaxStop($.unblockUI);