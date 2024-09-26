﻿var report_Withdrawalmoney = (function () {
    this.reports_data = [];
    this.search_Data = [];
    var HeaderCSS = "font-weight: bold;text-align: center;color: rgb(255, 255, 255);text-align: center !important; background-color: rgb(51, 122, 183);";
    var RowsCSS = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_1 = "background-color: rgb(255, 255, 255);text-align: right !important; vertical-align: middle !important;padding:0px;";
    var color_0 = "background-color:#5db75d;", color_1 = "background-color:#347ab8;", color_2 = "background-color:#f0ad4e;", color_3 = "background-color:#c082ff;";
    var color_4 = "background-color:#9850e0;", color_5 = "background-color:#fb5955;", color_sum = "background-color:#eee;";

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

        //BAR CHART
        var barchart_data = this.reports_data.data;
        $("#bar-chart-reports").show().html("");
        var bar = new Morris.Line({
            element: 'bar-chart-reports',
            resize: true,
            data: barchart_data,
            barColors: ['#337AB7', '#FFAA18'],
            xkey: 'lable',
            ykeys: ['mobile', 'website'],
            labels: ['ถอนผ่านแอปพลิเคชัน', 'ถอนผ่านโรงเรียน'],
            hideHover: 'auto',
            parseTime: false,
            axes: true
        });


        $("#" + table_name + " tbody").append(
            RenderRows(
                {
                    rowtype: "header", data: [{
                        text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:50px;" : "")
                    }, {
                        text: "วันที่", style: HeaderCSS + (export_file === true ? "width:200px;" : "")
                    }, {
                        text: "ยอดถอน", style: HeaderCSS + (export_file === true ? "width:200px;" : "")
                    }]
                }));

        $.each(this.reports_data.data, function (data_Index, data_Values) {
            sum_money += (data_Values.website + data_Values.mobile);
            if (report_type === 2) {
                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row", data: [
                        {
                            text: data_Index + 1, style: RowsCSS
                        },
                        {
                            text: `<a onclick='qurey_month("` + data_Values.values + `")'>` + data_Values.lable + "</a>", style: RowsCSS
                        },
                        {
                            text: $.number(data_Values.website + data_Values.mobile) + " บาท", style: RowsCSS
                        }]
                }));
            } else {
                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row", data: [
                        {
                            text: data_Index + 1, style: RowsCSS
                        },
                        {
                            text: "<a onclick='qurey_detail(\"" + data_Values.values + "\")'>" + data_Values.values + "</a>", style: RowsCSS
                        },
                        {
                            text: $.number(data_Values.website + data_Values.mobile) + " บาท", style: RowsCSS
                        }]
                }));
            }
        });

        $("#" + table_name + " tfoot").append(
            RenderRows(
                {
                    rowtype: "header", data: [{
                        text: "ยอดถอน", style: RowsCSS, colspan: 2
                    }, {
                        text: $.number(sum_money) + " บาท", style: RowsCSS, rowspan: 1
                    }]
                }));

        if (export_file === true) {
            $("#" + table_name).append("<tfoot>");
            var Colspan = 3;

            //Add Header Table
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
                    data: [{
                        text: ""
                    }, {
                        text: "พิมพ์วันที่ :", style: "font-size:13px;text-align: right !important;"
                    }, {
                        text: "{day_print}", style: "font-size:13px;text-align: right !important;"
                    }]
                }, {
                    rowtype: "header",
                    data: [
                        {
                            text: "",
                        }, {
                            text: "เวลา :", style: "font-size:13px;text-align: right !important;"
                        },
                        {
                            text: "{time_print}", style: "font-size:13px;text-align: right !important;"
                        }
                    ]
                }, {
                    rowtype: "header",
                    data: [{
                        text: "", colspan: 3
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
        $("#" + table_name + " tbody").append(
            RenderRows(
                {
                    rowtype: "header", data: [{
                        text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:50px;" : ""), rowspan: 1
                    }, {
                        text: "ชื่อผู้ทำรายการ", style: HeaderCSS + (export_file === true ? "width:200px;" : ""), rowspan: 1
                    }, {
                        text: "ชื่อผู้ถอน", style: HeaderCSS + (export_file === true ? "width:200px;" : ""), rowspan: 1
                    }, {
                        text: "ชั้นเรียน", style: HeaderCSS + (export_file === true ? "width:200px;" : ""), rowspan: 1
                    }, {
                        text: "เวลา", style: HeaderCSS + (export_file === true ? "width:200px;" : ""), rowspan: 1
                    }, {
                        text: "ยอดถอน", style: HeaderCSS + (export_file === true ? "width:200px;" : ""), rowspan: 1
                    }]
                }));

        $.each(this.reports_data.data, function (data_Index, data_Values) {
            sum_money += data_Values.money;
            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row", data: [
                    {
                        text: data_Index + 1, style: RowsCSS
                    },
                    {
                        text: data_Values.emp_name, style: RowsCSS
                    },
                    {
                        text: data_Values.user_name, style: RowsCSS
                    },
                    {
                        text: data_Values.class_name, style: RowsCSS
                    },
                    {
                        text: data_Values.time + " น.", style: RowsCSS
                    },
                    {
                        text: $.number(data_Values.money) + " บาท", style: RowsCSS
                    }]
            }));
        });

        $("#" + table_name + " tfoot").append(
            RenderRows(
                {
                    rowtype: "header", data: [{
                        text: "ยอดถอน", style: RowsCSS, colspan: 4,
                    }, {
                        text: $.number(sum_money) + " บาท", style: RowsCSS, rowspan: 1
                    }]
                }));

        if (export_file === true) {
            $("#" + table_name).append("<tfoot>");
            var Colspan = 5;

            //Add Header Table
            $("#" + table_name + " thead").append(RenderRows(
                {
                    rowtype: "header",
                    data: [{
                        text: $("input[id*=hdfschoolname]").val(),
                        colspan: Colspan,
                        style: "font-size:20px;"
                    }]
                }, {
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
                }, {
                    rowtype: "header",
                    data: [
                        {
                            text: "", colspan: Colspan - 2
                        }, {
                            text: "เวลา :", style: "font-size:13px;text-align: right !important;"
                        },
                        {
                            text: "{time_print}", style: "font-size:13px;text-align: right !important;"
                        }
                    ]
                }, {
                    rowtype: "header",
                    data: [{
                        text: "", colspan: Colspan
                    }]
                }
            ));
        }

        $("body").mLoading('hide');
    };

    var report_txt = '';

    this.export_excel = function () {
        var dt = new Date();
        var report_type = this.reports_data.report_type;
        var file_name = 'รายงาน' + this.reports_data.header_text + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getMinutes() + dt.getSeconds() + ' .xls';

        if (report_type === 3 && (this.search_Data.dEnd === this.search_Data.dStart || (this.search_Data.dEnd === undefined))) {
            this.RenderHtml_Detail('table_exports', true);
        } else {
            this.RenderHtml('table_exports', true);
        }

        var param = {
            "filename": "filename02",
            "tabledata": $("#export_excel").html()
        };
        $.post("/export_excel.aspx", param, function (data) {
            downloadFile(file_name, 'data:application/xml;charset=utf-8;base64,', data);
        });
    };
});
