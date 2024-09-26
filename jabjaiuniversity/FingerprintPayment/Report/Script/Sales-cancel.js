var report_sales = (function () {
    this.reports_data = [];
    var HeaderCSS = "font-weight: bold;text-align: center;color: rgb(255, 255, 255);text-align: center !important; background-color: rgb(51, 122, 183);";
    var RowsCSS = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_1 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: top !important;padding:0px;";
    var RowsCSS_2 = "background-color: rgb(255, 255, 255);text-align: right !important; vertical-align: middle !important;padding:0px;";
    var color_0 = "background-color:#5db75d;", color_1 = "background-color:#347ab8;", color_2 = "background-color:#f0ad4e;", color_3 = "background-color:#c082ff;";
    var color_4 = "background-color:#9850e0;", color_5 = "background-color:#fb5955;", color_sum = "background-color:#eee;";

    this.RenderHtml = function (table_name, export_file) {
        $("#" + table_name).html("");
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS;
            RowsCSS_1 += ExportCSS;
            RowsCSS_2 += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            RowsCSS_2 = RowsCSS_2.replace(ExportCSS, "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        //Add Rows Table

        var report_type = this.reports_data.report_type;
        var sum_money = 0, sum_cost = 0, sum_lucre = 0;

        //BAR CHART
        var barchart_data = this.reports_data.data;
        //$("#bar-chart-reports").html("");
        //var bar = new Morris.Line({
        //    element: 'bar-chart-reports',
        //    resize: true,
        //    data: barchart_data,
        //    barColors: ['#337AB7', '#FFAA18'],
        //    xkey: 'lable',
        //    ykeys: ['price'],
        //    labels: ['ยอดขาย'],
        //    hideHover: 'auto',
        //    parseTime: false,
        //    axes: true
        //});

        $("#" + table_name + " tbody").append(
            RenderRows(
                {
                    rowtype: "header", data: [{
                        text: "ลำดับ", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:100px;"
                    }, {
                        text: "วันที่", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:150px;"
                    }, {
                        text: "ราคาต้นทุน", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:150px;"
                    }, {
                        text: "ราคาขาย", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:150px;"
                    }, {
                        text: "กำไร", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:150px;"
                    }]
                }));

        $.each(this.reports_data.data, function (data_Index, data_Values) {
            sum_money += data_Values.price;
            sum_cost += data_Values.sum_cost;
            sum_lucre += data_Values.sum_lucre;
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
                            text: $.number(data_Values.cost) + " บาท", style: RowsCSS
                        },
                        {
                            text: $.number(data_Values.price) + " บาท", style: RowsCSS
                        },
                        {
                            text: $.number(data_Values.lucre) + " บาท", style: RowsCSS
                        },]
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
                            text: $.number(data_Values.cost) + " บาท", style: RowsCSS
                        },
                        {
                            text: $.number(data_Values.price) + " บาท", style: RowsCSS
                        },
                        {
                            text: $.number(data_Values.lucre) + " บาท", style: RowsCSS
                        }]
                }));
            }

        });


        $("#" + table_name + " tfoot").append(
            RenderRows(
                {
                    rowtype: "header", data: [{
                        text: "ยอดรวม", style: RowsCSS_2, colspan: 4
                    }, {
                        text: $.number(sum_money) + " บาท", style: RowsCSS, rowspan: 1
                    }]
                },
                {
                    rowtype: "header", data: [{
                        text: "ต้นทุน", style: RowsCSS_2, colspan: 4
                    }, {
                        text: $.number(sum_cost) + " บาท", style: RowsCSS, rowspan: 1
                    }]
                },
                {
                    rowtype: "header", data: [{
                        text: "กำไร", style: RowsCSS_2, colspan: 4
                    }, {
                        text: $.number(sum_lucre) + " บาท", style: RowsCSS, rowspan: 1
                    }]
                }));

        if (export_file === true) {
            $("#" + table_name).append("<tfoot>");
            var Colspan = $("#example tbody tr:last td").length;

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
                        text: "",
                        colspan: Colspan - 2
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
                        colspan: Colspan - 2
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
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS;
            RowsCSS_1 += ExportCSS;
            RowsCSS_2 += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            RowsCSS_2 = RowsCSS_2.replace(ExportCSS, "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        //Add Rows Table
        var sum_money = 0, sum_cost = 0, sum_lucre = 0;
        $("#" + table_name + " tbody").append(
            RenderRows(
                {
                    rowtype: "header", data: [{
                        text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:50px;" : ""), rowspan: 1
                    }, {
                        text: "วันที่/เวลา", style: HeaderCSS + (export_file === true ? "width:70px;" : "") + "", rowspan: 1
                    }, {
                        text: "ผู้ทำรายการ", style: HeaderCSS + (export_file === true ? "width:70px;" : "") + "", rowspan: 1
                    }, {
                        text: "ชั้น", style: HeaderCSS + (export_file === true ? "width:140px;" : ""), rowspan: 1
                    }, {
                        text: "รหัสนักเรียน", style: HeaderCSS + (export_file === true ? "width:140px;" : ""), rowspan: 1
                    }, {
                        text: "ชื่อ-สกุล", style: HeaderCSS + (export_file === true ? "width:90px;" : ""), rowspan: 1
                    }, {
                        text: "ชื่อร้าน", style: HeaderCSS + (export_file === true ? "width:90px;" : ""), rowspan: 1
                    }, {
                        text: "ชื่อสินค้า", style: HeaderCSS + (export_file === true ? "width:90px;" : ""), rowspan: 1
                    }, {
                        text: "จำนวน", style: HeaderCSS + (export_file === true ? "width:90px;" : ""), rowspan: 1
                    }, {
                        text: "ราคา", style: HeaderCSS + (export_file === true ? "width:90px;" : ""), rowspan: 1
                    }, {
                        text: "วันที่/เวลา ธุรกรรม", style: HeaderCSS + (export_file === true ? "width:90px;" : ""), rowspan: 1
                    }]
                }));

        var detail_data = this.reports_data.report_Details;
        var chart_data = this.reports_data.charts;

        $.each(detail_data, function (data_Index, data_Values) {

            var product_data = data_Values.products;
            $.each(product_data, function (product_data_index, product_data_values) {
                sum_money += product_data_values.total;
                sum_cost += product_data_values.sum_cost;
                sum_lucre += product_data_values.sum_lucre;
                if (product_data_index === 0) {
                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row", data: [
                            {
                                text: data_Index + 1, style: RowsCSS_1, rowspan: product_data.length
                            },
                            {
                                text: data_Values.time, style: RowsCSS_1, rowspan: product_data.length
                            },
                            {
                                text: data_Values.emp_name , style: RowsCSS_1, rowspan: product_data.length
                            },
                            {
                                text: data_Values.SubLevel === "" ? "" : data_Values.SubLevel + " / " + data_Values.nTSubLevel2, style: RowsCSS_1, rowspan: product_data.length
                            },
                            {
                                text: data_Values.sStudentID, style: RowsCSS_1, rowspan: product_data.length
                            },
                            {
                                text: data_Values.user_name, style: RowsCSS_1, rowspan: product_data.length
                            },
                            {
                                text: data_Values.shop_name, style: RowsCSS_1, rowspan: product_data.length
                            },
                            {
                                text: product_data_values.product_name, style: RowsCSS
                            },
                            {
                                text: product_data_values.amount, style: RowsCSS
                            },
                            {
                                text: $.number(product_data_values.price) + " บาท", style: RowsCSS
                            },
                            {
                                text: product_data_values.timeCancal, style: RowsCSS
                            }]
                    }));
                }
                else {
                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row", data: [
                            {
                                text: product_data_values.product_name, style: RowsCSS
                            },
                            {
                                text: product_data_values.amount, style: RowsCSS
                            },
                            {
                                text: $.number(product_data_values.price) + " บาท", style: RowsCSS
                            },
                            {
                                text: product_data_values.timeCancal, style: RowsCSS
                            }]
                    }));
                }
            });

        });

        //$("#" + table_name + " tfoot").append(
        //    RenderRows(
        //        {
        //            rowtype: "header", data: [{
        //                text: "ยอดรวม", style: RowsCSS_2, colspan: 9,
        //            }, {
        //                text: $.number(sum_money) + " บาท", style: RowsCSS, colspan: 2
        //            }]
        //        },
        //        {
        //            rowtype: "header", data: [{
        //                text: "ต้นทุน", style: RowsCSS_2, colspan: 9,
        //            }, {
        //                text: $.number(sum_cost) + " บาท", style: RowsCSS, colspan: 2
        //            }]
        //        },
        //        {
        //            rowtype: "header", data: [{
        //                text: "กำไร", style: RowsCSS_2, colspan: 9,
        //            }, {
        //                text: $.number(sum_lucre) + " บาท", style: RowsCSS, colspan: 2
        //            }]
        //        }));

        if (export_file === true) {
            $("#" + table_name).append("<tfoot>");
            var Colspan = $("#example tbody tr:last td").length;

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
                            text: "พิมพ์วันที่ :", style: "font-size:13px;text-align: right !important;",
                            colspan: Colspan - 1
                        }, {
                            text: "{day_print}", style: "font-size:13px;text-align: right !important;",
                            colspan: 1
                        }]
                }, {
                rowtype: "header",
                data: [{
                    text: "เวลา :", style: "font-size:13px;text-align: right !important;",
                    colspan: Colspan - 1
                },
                {
                    text: "{time_print}", style: "font-size:13px;text-align: right !important;",
                    colspan: 1
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
        $("body").mLoading();
        var dt = new Date();
        var json;
        var xhr;
        var report_type = this.reports_data.report_type;
        var file_name = 'รายงาน' + this.reports_data.header_text + "_" + dt.format("ddMMyyyyHHmmssss") + '.xls';
        if (report_type === undefined) {
            //this.RenderHtml_Detail('table_exports', true);
            json = JSON.stringify({ search: searchData });
            xhr = new XMLHttpRequest();
            xhr.open("POST", "/Report/Sales-cancel.aspx/export_data", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                $("body").mLoading('hide');
            };
            xhr.send(json);
        } else {
            json = JSON.stringify({ search: searchData });
            xhr = new XMLHttpRequest();

            xhr.open("POST", "/Report/Sales-cancel.aspx/export02_data", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                //aa = xhr.getResponseHeader("filename");
                saveAs(xhr.response, file_name);
                $("body").mLoading('hide');
            };
            xhr.send(json);
            //this.RenderHtml('table_exports', true);
        }

        var param = {
            "filename": file_name,
            "tabledata": $("#export_excel").html()
        };
    };

});

function deleteData(SellId, userName, timeBuyProduct) {
    $.confirm({
        title: '<h2>แจ้งเตือน</h2>',
        content: '<h3>ท่านต้องการลบรายการซื้อสินค้าของ<br/> ' + userName + ' <br/> เวลา ' + timeBuyProduct + ' <br/> นี้หรือไม่</h3>',
        theme: 'bootstrap',
        buttons: {
            danger: {
                text: "<span style=\"font-size: 20px;\">ลบข้อมูล</span>",
                btnClass: 'btn-red any-other-class',
                action: function () {
                    console.log(SellId);
                    PageMethods.CancelBuyProduct(SellId, function (e) {
                        if (e == "success") {
                            SearchData();
                        } else if (e == "fail") {
                            alert('ท่านไม่สามารถยกเลิกรายการนี้ได้');
                        } else {
                            alert('การตอบสนองล้มเหลว!');
                            SearchData();
                        }
                    });
                }
            },
            cancel: {
                text: "<span style=\"font-size: 20px;\" >ยกเลิก</span>",
                btnClass: 'btn-blue any-other-class',
                action: function () {
                }
            }
        }
    });
}