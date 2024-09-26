//rgb(13, 199, 66) dc742
//rgb(3, 147, 199) 393c7
//rgb(228, 146, 47) e4922f
//rgb(255, 0, 254) ff0fe
//rgb(152, 80, 224) 9850e0
//rgb(255, 57, 66) ff3942
var reports_invoices = (function () {
    this.reports_data = [];
    var HeaderCSS = "font-weight: bold;text-align: center;color: rgb(255, 255, 255);text-align: center !important; background-color: rgb(51, 122, 183);";
    var RowsCSS = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_1 = "background-color: rgb(255, 255, 255);text-align: right !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_2 = "background-color: rgb(255, 255, 255);text-align: left !important; vertical-align: middle !important;padding:0px;";
    var color_0 = "background-color:#0dc742", color_1 = "background-color:#0393c7", color_2 = "background-color:#e4922f", color_3 = "background-color:#ff0fe";
    var color_4 = "background-color:#9850e0", color_5 = "background-color:#ff3942", color_sum = "background-color:#eee;;";

    this.RenderHtml = function (table_name, export_file) {
        var dt = new Date();
        $("#" + table_name).html("");
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file == true) {
            HeaderCSS += ExportCSS.replace(";min-width", ";width");
            RowsCSS += ExportCSS.replace(";min-width", ";width");
            RowsCSS_1 += ExportCSS.replace(";min-width", ";width");
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS).replace(";width", ";min-width");
            RowsCSS = RowsCSS.replace(ExportCSS).replace(";width", ";min-width");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS).replace(";width", ";min-width");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");
        var thead_01 = [];
        var thead_02 = [];
        var thead_03 = [];
        var Months = this.reports_data;
        var Colspan = 7;

        if (export_file) {
            //Add Header Reports
            $("#" + table_name + " thead").append(
                RenderRows(
                    {
                        rowtype: "header",
                        data: [{ text: $("input[id*=hdfschoolname]").val(), style: "text-align: center;font-size:20px;border-width:0px;", colspan: Colspan }]
                    }, {
                        rowtype: "header",
                        data: [{ text: "รายงานรายชื่อลูกหนี้รายห้อง", style: "text-align: center;font-size:18px;border-width:0px;", colspan: Colspan }]
                    }, {
                        rowtype: "header",
                        data: [
                            { text: "ปีการศึกษา :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 1 },
                            { text: $("select[id*=ddlyear] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 1 },
                            { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 3) },
                            { text: "เทอม :" + $("select[id*=semister] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 1 },
                        ]
                    }, {
                        rowtype: "header",
                        data: [
                            { text: "ระดับชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 1 },
                            { text: $("select[id*=ddlsublevel] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 1 },
                            { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 3) },
                            { text: "ชั้นเรียน :" + $("select[id*=ddlSubLV2] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 1 },
                        ]
                    }, {
                        rowtype: "header",
                        data: [
                            { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 3) },
                            { text: "อาจารย์ประจำชั้น : " + teacher_name, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 3 },
                        ]
                    }, {
                        //    rowtype: "header",
                        //    data: [{ text: "ประจำวันที่ " + ReportDate, style: "text-align: right;font-size:13px;border-width:0px;", colspan: Colspan }]
                        //}, {
                        //    rowtype: "header",
                        //    data: [{ text: "พิมพ์วันที่ :&nbsp;" + dt.toLocaleDateString(), style: "text-align: right;font-size:13px;border-width:0px;", colspan: Colspan }]
                        //}, {
                        rowtype: "header",
                        data: [{ text: "พิมพ์วันที่ :&nbsp;" + dt.toLocaleDateString() + " เวลา :&nbsp;" + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds(), style: "text-align: right;font-size:13px;border-width:0px;", colspan: Colspan }]
                    }, {
                        rowtype: "header",
                        data: [{ text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: Colspan }]
                    }
                ));
            //Add Footer Reports
        }

        //Add Rows Table
        $("#" + table_name + " tbody").append(
            RenderRows(
                {
                    rowtype: "row",
                    data: [
                        { text: "ลำดับ", style: HeaderCSS }, { text: "รหัสนักเรียน", style: HeaderCSS },
                        { text: "ชื่อ - นามสกุล", style: HeaderCSS, }, { text: "ยอดเต็ม", style: HeaderCSS },
                        { text: "ชำระแล้ว", style: HeaderCSS }, { text: "ค้างชำระ", style: HeaderCSS },
                        { text: "หมายเหตุ", style: HeaderCSS }
                    ]
                }
            ));


        var sum_0 = 0, sum_1 = 0, sum_2 = 0;

        $.each(this.reports_data, function (index, values) {

            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: (index + 1), style: RowsCSS },
                    { text: values.student_id, style: RowsCSS },
                    { text: values.student_name, style: RowsCSS_2 },
                    { text: values.paymentAmount, style: RowsCSS },
                    { text: values.payment, style: RowsCSS },
                    { text: values.debt, style: RowsCSS },
                    { text: values.TotalDiscount, style: RowsCSS },
                ]
            }
            ));

            sum_0 += parseFloat(values.paymentAmount.replace(",", ""));
            sum_1 += parseFloat(values.payment.replace(",", ""));
            sum_2 += parseFloat(values.debt.replace(",", ""));
        });
        //Rows Sum/Percent
        $("#" + table_name + " tfoot").append(RenderRows(
            {
                rowtype: "row",
                data: [
                    { text: "รวม", colspan: 3, style: RowsCSS_1 },
                    { text: $.number(sum_0, 2), style: RowsCSS },
                    { text: $.number(sum_1, 2), style: RowsCSS },
                    { text: $.number(sum_2, 2), style: RowsCSS },
                    { text: "", style: RowsCSS },
                ]
            },
        ));
        $("body").mLoading('hide');
    };

    var report_txt = '';

    this.export_excel = function () {
        var dt = new Date();
        var file_name = 'รายงานรายชื่อลูกหนี้รายห้อง โรงเรียน ' + $("input[id*=hdfschoolname]").val() + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getMinutes() + dt.getSeconds() + ' .xls';

        this.RenderHtml("table_exports", true);
        var param = {
            "filename": "filename02",
            "tabledata": $("#export_excel").html()
        };
        $.post("/export_excel.aspx", param, function (data) {
            downloadFile(file_name, 'data:application/xml;charset=utf-8;base64,', data);
        });
    }

    this.export_PDF = function () {

        if ($('select[id*=ddlSubLV2]').val() === "") {
            $.alert({
                title: '<h3>แจ้งเตือน</h3>',
                content: '<h3>กรุณาเลือกห้องเรียน !</h3>',
                theme: 'bootstrap',
                buttons: {
                    "<span style=\"font-size: 20px;\">Close</span>": function () {
                    }
                }
            });
            return;
        } else {
            $("body").mLoading('show');
            var json = JSON.stringify({
                "term_id": $('select[id*=semister]').val(),
                "level2_id": $('select[id*=ddlSubLV2]').val(),
                "year_id": $('select[id*=ddlyear]').val(),
                "report_type": $('select[id*=report_type]').val()
            });
            var req = new XMLHttpRequest();
            var dt = new Date();
            var file_name = 'รายงานรายชื่อลูกหนี้รายห้อง โรงเรียน ' + $("input[id*=hdfschoolname]").val() + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getMinutes() + dt.getSeconds() + ' .pdf';

            req.open("POST", "/Report/Handles/report_invoices_exportPDFHandler.ashx", true);
            req.responseType = "blob";
            req.onreadystatechange = function () {
                if (req.readyState === 4 && req.status === 200) {

                    // test for IE

                    if (typeof window.navigator.msSaveBlob === 'function') {
                        window.navigator.msSaveBlob(req.response, "PdfName-" + new Date().getTime() + ".pdf");
                    } else {
                        var blob = req.response;
                        var link = document.createElement('a');
                        link.href = window.URL.createObjectURL(blob);
                        link.download = file_name;

                        // append the link to the document body

                        document.body.appendChild(link);

                        link.click();
                        $("body").mLoading('hide');
                    }
                }
            };
            req.send(json);
        }
    }
});
