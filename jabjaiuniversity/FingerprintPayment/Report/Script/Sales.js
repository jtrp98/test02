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
        var sum_money = 0, sum_cost = 0, sum_lucre = 0, sum_deduct = 0;

        //BAR CHART
        var barchart_data = this.reports_data.data;
        $("#bar-chart-reports").html("");
        var bar = new Morris.Line({
            element: 'bar-chart-reports',
            resize: true,
            data: barchart_data,
            barColors: ['#337AB7', '#FFAA18'],
            xkey: 'lable',
            ykeys: ['price'],
            labels: ['ยอดขาย'],
            hideHover: 'auto',
            parseTime: false,
            axes: true
        });


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
            //if (data_Values.IsSum == 1)
            //else
            //    console.log(data_Values);

            sum_money += data_Values.price;
            sum_cost += data_Values.cost;
            sum_lucre += data_Values.lucre;
            sum_deduct += data_Values.deduct;
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
                            text: $.number(data_Values.cost, 2) + " บาท", style: RowsCSS
                        },
                        {
                            text: $.number(data_Values.price, 2) + " บาท", style: RowsCSS
                        },
                        {
                            text: $.number(data_Values.lucre - data_Values.deduct, 2) + " บาท", style: RowsCSS
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
                            text: $.number(data_Values.cost, 2) + " บาท", style: RowsCSS
                        },
                        {
                            text: $.number(data_Values.price, 2) + " บาท", style: RowsCSS
                        },
                        {
                            text: $.number(data_Values.lucre - data_Values.deduct, 2) + " บาท", style: RowsCSS
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
                        text: $.number(sum_money, 2) + " บาท", style: RowsCSS, rowspan: 1
                    }]
                },
                {
                    rowtype: "header", data: [{
                        text: "ยอดหัก%", style: RowsCSS_2, colspan: 4,
                    }, {
                        text: $.number(sum_deduct, 2), style: RowsCSS, colspan: 3
                    }]
                },
                {
                    rowtype: "header", data: [{
                        text: "ต้นทุน", style: RowsCSS_2, colspan: 4
                    }, {
                        text: $.number(sum_cost, 2) + " บาท", style: RowsCSS, rowspan: 1
                    }]
                },
                {
                    rowtype: "header", data: [{
                        text: "กำไร", style: RowsCSS_2, colspan: 4
                    }, {
                        text: $.number(sum_lucre - sum_deduct, 2) + " บาท", style: RowsCSS, rowspan: 1
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
        var sum_money = 0, sum_cost = 0, sum_lucre = 0, sum_cancle = 0, sum_sell = 0, sum_deduct = 0, sum_offline = 0,
            sum_cancle2 = 0, minus_cancle = 0;
        $("#" + table_name + " tbody").append(
            RenderRows(
                {
                    rowtype: "header", data: [{
                        text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:50px;" : ""), rowspan: 1
                    }, {
                        text: "เวลา", style: HeaderCSS + (export_file === true ? "width:70px;" : "") + "", rowspan: 1
                    }, {
                        text: ($("#shop_id").val() === "" ? "ร้านค้า" : "ชื่อพนักงาน"), style: HeaderCSS + (export_file === true ? "width:140px;" : ""), rowspan: 1
                    }, {
                        text: "ชั้นเรียน", style: HeaderCSS + (export_file === true ? "width:140px;" : ""), rowspan: 1
                    }, {
                        text: "ชื่อผู้ซื้อ", style: HeaderCSS + (export_file === true ? "width:140px;" : ""), rowspan: 1
                    }, {
                        text: "ประเภท", style: HeaderCSS + (export_file === true ? "width:140px;" : ""), rowspan: 1
                    }, {
                        text: "รหัสบาร์โค้ด", style: HeaderCSS + (export_file === true ? "width:90px;" : ""), rowspan: 1
                    }, {
                        text: "ชื่อสินค้า", style: HeaderCSS + (export_file === true ? "width:90px;" : ""), rowspan: 1
                    }, {
                        text: "ราคาต้นทุน", style: HeaderCSS + (export_file === true ? "width:90px;" : ""), rowspan: 1
                    }, {
                        text: "ราคาขาย", style: HeaderCSS + (export_file === true ? "width:90px;" : ""), rowspan: 1
                    }, {
                        text: "จำนวน<br/>(หน่วย)", style: HeaderCSS + (export_file === true ? "width:90px;" : ""), rowspan: 1
                    }, {
                        text: "ยอดรวม(บาท)", style: HeaderCSS + "width:70px;", rowspan: 1
                    }, {
                        text: "หมายเหตุ", style: HeaderCSS + "width:70px;", rowspan: 1
                    }, {
                        text: "", style: HeaderCSS + "width:70px;", rowspan: 1
                    }]
                }));

        var detail_data = this.reports_data.report_Details;
        var chart_data = this.reports_data.charts;

        $("#bar-chart-reports").html("");
        var bar = new Morris.Line({
            element: 'bar-chart-reports',
            resize: true,
            data: chart_data,
            barColors: ['#337AB7', '#FFAA18'],
            xkey: 'lable',
            ykeys: ['values'],
            labels: ['ยอดขาย'],
            hideHover: 'auto',
            parseTime: false,
            axes: true
        });

        $.each(detail_data, function (data_Index, data_Values) {
           
            var product_data = data_Values.products;
            //console.log(data_Values.sell_Id + ' ' + data_Values.dSell);
            //console.log(data_Values);
            $.each(product_data, function (product_data_index, product_data_values) {
             
                if (data_Values.cancle_status == 'Y') { //&& data_Values.IsEqualCancleDay

                    if (data_Values.IsEqualSearchDay || data_Values.IsEqualSynDay) {
                        sum_money += product_data_values.total;                        
                        sum_cost += product_data_values.sum_cost;
                        sum_lucre += product_data_values.sum_lucre;
                        sum_deduct += product_data_values.deduct;
                    }

                    if (data_Values.IsEqualCancleDay) {
                        sum_cancle += product_data_values.total;
                        sum_deduct -= product_data_values.deduct;
                    }
                    else {
                        sum_cancle2 += product_data_values.total;
                    }

                }
                else if (data_Values.IsOffline && !data_Values.IsEqualSynDay) {
                    sum_offline += product_data_values.total;
                }
                else if (data_Values.IsEqualSearchDay || data_Values.IsEqualSynDay) {

                    sum_money += product_data_values.total;                    
                    sum_cost += product_data_values.sum_cost;
                    sum_lucre += product_data_values.sum_lucre;
                    sum_deduct += product_data_values.deduct;

                }
              
                if (product_data_index === 0) {

                    //console.log(product_data_values.payment_type);
                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row", data: [
                            {
                                text: data_Index + 1, style: RowsCSS_1, rowspan: product_data.length
                            },
                            {
                                text: data_Values.time.replaceAll(' ', '<br/>'), style: RowsCSS_1, rowspan: product_data.length
                            },
                            {
                                text: (shop_id === "" ? data_Values.shop_name : (data_Values.emp_name === "" ? "ซื้อผ่านแอป" : data_Values.emp_name)), style: RowsCSS_1, rowspan: product_data.length
                            },
                            {
                                text: data_Values.CLASSNAME, style: RowsCSS_1, rowspan: product_data.length
                            },
                            {
                                text: data_Values.user_name, style: RowsCSS_1, rowspan: product_data.length
                            },
                            {
                                text: product_data_values.product_type, style: RowsCSS
                            },
                            {
                                text: product_data_values.barcode, style: RowsCSS
                            },
                            {
                                text: product_data_values.product_name, style: RowsCSS
                            },
                            {
                                text: $.number(product_data_values.cost, 2), style: RowsCSS
                            },
                            {
                                text: $.number(product_data_values.price, 2), style: RowsCSS
                            },
                            {
                                text: product_data_values.amount, style: RowsCSS
                            },
                            {
                                text: $.number(product_data_values.total, 2), style: RowsCSS
                            },
                            {
                                text: (data_Values.DeviceID ? data_Values.DeviceID + ' <br/>' : '')
                                    + data_Values.remark.replaceAll('$', '<br/>')
                                    //+ (data_Values.IsOffline == true ? "รายการออฟไลน์จะนับรวมยอดในวันที่ระบบออนไลน์" : "")
                                    + (data_Values.PaymentType == 3 ? `ซื้อสินค้าผ่านพร้อมเพย์<br/>${data_Values.KTType}${data_Values.RefPromptPay} ${data_Values.RemarkPromptPay} ${(data_Values.RemarkPromptPay.trim() ? `<br>ยอดตกน้ำ<br>วันที่ ${data_Values.BuyPromptPay}` : '' )}`  : "")
                                    + (product_data_values.total == 0 ? "เบิก/ฟรี" : ""),
                                style: RowsCSS
                            },
                            {
                                text: "<i class=\"fa fa-print btn-print\" style='color:blue;' onclick=\"window.open('invoices.aspx?id=" + data_Values.id + "')\"/> " +
                                    ((data_Values.PaymentType != '3' && data_Values.cancle_status == "N") ?
                                        "<i class=\"fa fa-trash btn-delete\"  style='color:red;cursor: pointer;' onclick=\"deleteData('" + data_Values.sell_Id + "','" + data_Values.user_name + "','" + data_Values.time + "' )\" />" : ""), style: RowsCSS, rowspan: product_data.length
                            }]
                    }));
                }
                else {
                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row", data: [
                            {
                                text: product_data_values.product_type, style: RowsCSS
                            },
                            {
                                text: product_data_values.barcode, style: RowsCSS
                            },
                            {
                                text: product_data_values.product_name, style: RowsCSS
                            },
                            {
                                text: $.number(product_data_values.cost, 2), style: RowsCSS
                            },
                            {
                                text: $.number(product_data_values.price, 2), style: RowsCSS
                            },
                            {
                                text: product_data_values.amount, style: RowsCSS
                            },
                            {
                                text: $.number(product_data_values.total, 2), style: RowsCSS
                            }]
                    }));
                }
            });

        });
               
        $("#" + table_name + " tfoot").append(
            RenderRows(
                {
                    rowtype: "header", data: [{
                        text: "ยอดขายรวม", style: RowsCSS_2, colspan: 10,
                    }, {
                        text: $.number(sum_money , 2), style: RowsCSS, colspan: 3
                    }]
                },
                {
                    rowtype: "header", data: [{
                        text: "ยอดยกเลิกรวม", style: RowsCSS_2, colspan: 10,
                    }, {
                        text: $.number(sum_cancle, 2), style: RowsCSS, colspan: 3
                    }]
                },
                {
                    rowtype: "header", data: [{
                        text: "ยอดสุทธิ", style: RowsCSS_2, colspan: 10,
                    }, {
                        text: $.number(sum_money - sum_cancle , 2), style: RowsCSS, colspan: 3
                    }]
                },
                {
                    rowtype: "header", data: [{
                        text: "ยอดหัก%", style: RowsCSS_2, colspan: 10,
                    }, {
                        text: $.number(sum_deduct, 2), style: RowsCSS, colspan: 3
                    }]
                },
                {
                    rowtype: "header", data: [{
                        text: "ต้นทุน", style: RowsCSS_2, colspan: 10,
                    }, {
                        text: $.number(sum_cost, 2), style: RowsCSS, colspan: 3
                    }]
                },
                {
                    rowtype: "header", data: [{
                        text: "กำไร", style: RowsCSS_2, colspan: 10,
                    }, {
                        text: $.number(sum_lucre - sum_deduct - sum_cancle, 2), style: RowsCSS, colspan: 3
                    }]
                }));

        if (sum_offline > 0) {
            $("#" + table_name + " tfoot").append(
                RenderRows(
                    {
                        rowtype: "header", data: [{
                            text: "รายการออฟไลน์", style: RowsCSS_2, colspan: 10,
                        }, {
                            text: $.number(sum_offline, 2), style: RowsCSS, colspan: 3
                        }]
                    },
                    {
                        rowtype: "header", data: [{
                            text: "หมายเหตุ", style: RowsCSS_2, colspan: 10,
                        }, {
                            text: "รายการออฟไลน์ นับยอดในวันที่เงินเข้าระบบออนไลน์", style: RowsCSS, colspan: 3
                        }]
                    })
            );
        }
        if (sum_cancle2 > 0) {
            $("#" + table_name + " tfoot").append(
                RenderRows(
                    {
                        rowtype: "header", data: [{
                            text: "รายการยกเลิก", style: RowsCSS_2, colspan: 10,
                        }, {
                            text: $.number(sum_cancle2, 2), style: RowsCSS, colspan: 3
                        }]
                    }
                    , {
                        rowtype: "header", data: [{
                            text: "หมายเหตุ", style: RowsCSS_2, colspan: 10,
                        }, {
                            text: "รายการยกเลิก จะนับยอดในวันที่กระทำการยกเลิก", style: RowsCSS, colspan: 3
                        }]
                    })
            );
        }
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
            xhr.open("POST", "/Report/Sales.aspx/export_data", true);
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

            xhr.open("POST", "/Report/Sales.aspx/export02_data", true);
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

        //var json = JSON.stringify(param);
        //var xhr = new XMLHttpRequest();
        //xhr.open("POST", "/export_excel.aspx",true);
        //xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
        //xhr.responseType = 'blob';
        //xhr.onload = function () {
        //    saveAs(xhr.response, file_name);
        //};
        //xhr.send(json);
        //$.post("/export_excel.aspx", param, function (data) {
        //    saveAs(data, file_name);
        //    //downloadFile(file_name, 'data:application/xml;charset=utf-8;base64,', data);
        //});
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
                    PageMethods.CancelBuyProduct_v2(SellId, function (e) {
                        let JSONData = $.parseJSON(e.resultDes)
                        if (JSONData.status.indexOf("Sales Canceld Successfully") != -1) {
                            SearchData();
                        } else if (e == "fail") {
                            alert('ท่านไม่สามารถยกเลิกรายการนี้ได้');
                        } else {
                            alert('ท่านไม่สามารถยกเลิกรายการนี้ได้');
                            //alert('การตอบสนองล้มเหลว!');
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