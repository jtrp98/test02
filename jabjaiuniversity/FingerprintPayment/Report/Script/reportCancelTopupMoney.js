var Report_CancelTopupMoney = (function () {
    this.reports_data = [];
    var HeaderCSS = "font-weight: bold;text-align: center;color: rgb(255, 255, 255);text-align: center !important; background-color: rgb(51, 122, 183);";
    var RowsCSS = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_1 = "background-color: rgb(255, 255, 255);text-align: right !important; vertical-align: middle !important;padding:0px;";

    this.RenderHtml = function (table_name, export_file) {
        $("#" + table_name).html("");
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS.replace(";min-width", ";width");
            RowsCSS += ExportCSS.replace(";min-width", ";width");
            RowsCSS_1 += ExportCSS.replace(";min-width", ";width");
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "").replace(";width", ";min-width");
            RowsCSS = RowsCSS.replace(ExportCSS, "").replace(";width", ";min-width");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "").replace(";width", ";min-width");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        //Add Rows Table
        var report_type = this.reports_data.report_type;
        var sum_money = 0;

        $("#" + table_name + " tbody").append(RenderRows({
            rowtype: "header", data: [
                {
                    text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:200px;" : "")
                },
                {
                    text: "วันที่", style: HeaderCSS + (export_file === true ? "width:200px;" : "")
                },
                {
                    text: "ยอดยกเลิกการเติม", style: HeaderCSS + (export_file === true ? "width:200px;" : "")
                }]
        }));

        $.each(this.reports_data.data, function (data_Index, data_Values) {

            sum_money += data_Values.CnMoney;

            if (report_type === 2) {
                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row", data: [
                        {
                            text: data_Index + 1, style: RowsCSS
                        },
                        {
                            text: `<a onclick='qurey_month("` + data_Values.values + `")'>` + data_Values.lable + "</a>", style: RowsCSS
                        }, /*ปี*/
                        {
                            text: $.number(data_Values.CnMoney) + " บาท", style: RowsCSS
                        }
                    ]
                }));
            }
            else {
                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row", data: [
                        {
                            text: data_Index + 1, style: RowsCSS
                        },
                        {
                            text: "<a onclick='qurey_detail(\"" + data_Values.values + "\")'>" + data_Values.values + "</a>", style: RowsCSS
                        }, /*เดือน*/
                        {
                            text: $.number(data_Values.CnMoney) + " บาท", style: RowsCSS
                        }
                    ]
                }));
            }
        });

        $("#" + table_name + " tfoot").append(
            RenderRows(
                {
                    rowtype: "header", data: [
                        {
                            text: "ยอดยกเลิกการเติม", style: /*RowsCSS_1*/ RowsCSS, colspan: 2
                        },
                        {
                            text: $.number(sum_money) + " บาท", style: RowsCSS, rowspan: 1
                        }
                    ]
                }));

        if (export_file === true) {
            $("#" + table_name).append("<tfoot>");
            var Colspan = 3;

            $("#" + table_name + " thead").append(RenderRows(
                {
                    rowtype: "header",
                    data: [{
                        text: $("input[id*=hdfschoolname]").val(),
                        colspan: Colspan,
                        style: "font-size:20px;"
                    }]
                },
                {
                    rowtype: "header",
                    data: [{
                        text: this.reports_data.header_text,
                        colspan: Colspan,
                        style: "font-size:18px;"
                    }]
                },
                {
                    rowtype: "header",
                    data: [
                        {
                            text: ""
                        },
                        {
                            text: "พิมพ์วันที่ :", style: "font-size:13px;text-align: right !important;"
                        },
                        {
                            text: "{day_print}", style: "font-size:13px;text-align: right !important;"
                        }
                    ]
                },
                {
                    rowtype: "header",
                    data: [
                        {
                            text: "",
                        },
                        {
                            text: "เวลา :", style: "font-size:13px;text-align: right !important;"
                        },
                        {
                            text: "{time_print}", style: "font-size:13px;text-align: right !important;"
                        }
                    ]
                },
                {
                    rowtype: "header",
                    data: [{
                        text: "", Colspan: 5
                    }]
                }
            ));
        }

        $("body").mLoading('hide');

    };



    this.RenderHtml_Detail = function (table_name, export_file) {
        $("#" + table_name).html("");
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS.replace(";min-width", ";width");
            RowsCSS += ExportCSS.replace(";min-width", ";width");
            RowsCSS_1 += ExportCSS.replace(";min-width", ";width");
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "").replace(";width", ";min-width");
            RowsCSS = RowsCSS.replace(ExportCSS, "").replace(";width", ";min-width");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "").replace(";width", ";min-width");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        //Add Rows Table
        $("#bar-chart-reports").hide();

        var sum_money = 0;

        $("#" + table_name + " tbody").append(RenderRows(
            {
                rowtype: "header", data:
                    [
                        {
                            text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:50px;" : ""), rowspan: 1
                        },
                        {
                            text: "เวลา", style: HeaderCSS + (export_file === true ? "width:200px;" : ""), rowspan: 1
                        },
                        {
                            text: "ชื่อผู้ทำรายการ", style: HeaderCSS + (export_file === true ? "width:200px;" : ""), rowspan: 1
                        },
                        {
                            text: "รหัสนักเรียน", style: HeaderCSS + (export_file === true ? "width:200px;" : ""), rowspan: 1
                        },
                        {
                            text: "ชื่อ - สกุล", style: HeaderCSS + (export_file === true ? "width:200px;" : ""), rowspan: 1
                        },
                        {
                            text: "ชั้นเรียน", style: HeaderCSS + (export_file === true ? "width:200px;" : ""), rowspan: 1
                        },
                        {
                            text: "ยอดเงิน", style: HeaderCSS + (export_file === true ? "width:200px;" : ""), rowspan: 1
                        }
                    ]
            }));


        $.each(this.reports_data.data, function (data_Index, data_Values) {
            sum_money += data_Values.money;
            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row", data: [
                    {
                        text: data_Index + 1, style: RowsCSS
                    },
                    {
                        text: data_Values.time + " น.", style: RowsCSS
                    },
                    {
                        text: data_Values.emp_name, style: RowsCSS
                    },
                    {
                        text: data_Values.sStudentID, style: RowsCSS
                    },
                    {
                        text: data_Values.user_name, style: RowsCSS
                    },
                    {
                        text: data_Values.class_name, style: RowsCSS
                    },
                    {
                        text: $.number(data_Values.money) + " บาท", style: RowsCSS
                    }
                ]
            }));
        });


        $("#" + table_name + " tfoot").append(RenderRows(
            {
                rowtype: "header", data: [
                    {
                        text: "ยอดยกเลิกการเติม", style: /*RowsCSS_1*/ RowsCSS, colspan: 6,
                    },
                    {
                        text: $.number(sum_money) + " บาท", style: RowsCSS, rowspan: 1
                    }
                ]
            }));


        if (export_file === true) {
            $("#" + table_name).append("<tfoot>");
            var Colspan = 7;

            $("#" + table_name + " thead").append(RenderRows(
                {
                    rowtype: "header",
                    data: [{
                        text: $("input[id*=hdfschoolname]").val(),
                        colspan: Colspan,
                        style: "font-size:20px;"
                    }]
                },
                {
                    rowtype: "header",
                    data: [{
                        text: this.reports_data.header_text,
                        colspan: Colspan,
                        style: "font-size:18px;"
                    }]
                },
                {
                    rowtype: "header",
                    data: [
                        {
                            text: "", colspan: Colspan - 2
                        },
                        {
                            text: "พิมพ์วันที่ :", style: "font-size:13px;text-align: right !important;"
                        }, {
                            text: "{day_print}", style: "font-size:13px;text-align: right !important;"
                        }]
                },
                {
                    rowtype: "header",
                    data: [
                        {
                            text: "", colspan: Colspan - 2
                        }, {
                            text: "เวลา :", style: "font-size:13px;text-align: right !important;"
                        },
                        {
                            text: "{time_print}", style: "font-size:13px;text-align: right !important;"
                        }]
                },
                {
                    rowtype: "header",
                    data: [{
                        text: "", colspan: Colspan
                    }]
                }
            ))

        }
        $("body").mLoading('hide');
    };



    this.export_excel = function () {
        var dt = new Date();
        var report_type = this.reports_data.report_type;
        var file_name = 'รายงาน' + this.reports_data.header_text + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getDate() + dt.getMinutes() + dt.getSeconds() + ' .xls';

        if (report_type === 3) {
            this.RenderHtml_Detail('table_exports', true);
        } else {
            this.RenderHtml('table_exports', true);
        }
        var param = {
            "filename": "filename02",
            "tabledata": $("#export_excel").html()
        };
        $.post("/export_excel.aspx", param, function (data) {
            downloadFile(file_name, 'data:application/xml;charset=utf-8;base64,', data)
        });

    };





});