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
            HeaderCSS += ExportCSS.replace(";min-width", ";width");
            RowsCSS += ExportCSS.replace(";min-width", ";width");
            RowsCSS_1 += ExportCSS.replace(";min-width", ";width");
            RowsCSS_2 += ExportCSS.replace(";min-width", ";width");
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "").replace(";min-width", ";width");
            RowsCSS = RowsCSS.replace(ExportCSS, "").replace(";min-width", ";width");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "").replace(";min-width", ";width");
            RowsCSS_2 = RowsCSS_2.replace(ExportCSS, "").replace(";min-width", ";width");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        //Add Rows Table
        var report_type = this.reports_data.report_type;

        if (report_type != 3) {
            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "header",
                data: [
                    { text: "ลำดับ", style: HeaderCSS + "width:200px;", rowspan: 2 },
                    { text: "วันที่", style: HeaderCSS, rowspan: 2 },
                    { text: "จำนวน/ชิ้น", style: HeaderCSS + "width:200px;", rowspan: 2 },
                    { text: "ราคาทุน", style: HeaderCSS + "width:150px;", rowspan: 2 },
                    { text: "ยอดขาย/บาท", style: HeaderCSS, rowspan: 1, colspan: 2 },
                    { text: "กำไร", style: HeaderCSS + "width:150px;", rowspan: 2 },
                ]
            }));

            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "header",
                data: [
                    { text: "ระบบภายในโรงเรียน", style: HeaderCSS + "width:200px;", rowspan: 1, colspan: 1 },
                    { text: "QR Code พร้อมเพย์", style: HeaderCSS + "width:200px;", rowspan: 1, colspan: 1 }
                ]
            }));
        } else if (report_type == 3) {
            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "header", data:
                    [
                        { text: "ลำดับ", style: HeaderCSS, rowspan: 2 },
                        { text: "ชื่อร้านค้า", style: HeaderCSS, rowspan: 2 },
                        { text: "ประเภท", style: HeaderCSS, rowspan: 2 },
                        { text: "Barcode", style: HeaderCSS, rowspan: 2 },
                        { text: "ชื่อสินค้า", style: HeaderCSS, rowspan: 2 },
                        { text: "จำนวน/ชิ้น", style: HeaderCSS, rowspan: 2 },
                        { text: "ราคาทุน", style: HeaderCSS + "width:150px;" , rowspan: 2 },
                        { text: "ยอดขาย/บาท", style: HeaderCSS, rowspan: 1, colspan: 2 },
                        { text: "กำไร", style: HeaderCSS + "width:150px;" , rowspan: 2 },
                    ]
            }));

            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "header",
                data: [
                    { text: "ระบบภายในโรงเรียน", style: HeaderCSS + "width:200px;", rowspan: 1, colspan: 1 },
                    { text: "QR Code พร้อมเพย์", style: HeaderCSS + "width:200px;", rowspan: 1, colspan: 1 }
                ]
            }));

        }

        var sum_amount = 0, sum_price1 = 0, sum_price2 = 0, sum_cost = 0, sum_profit = 0;
        var Total_amount = 0, Total_price1 = 0, Total_price2 = 0, Total_Cost = 0, Total_Profit = 0;

        var reports_data;
        if (report_type != 3) {
            reports_data = this.reports_data.data;

            $.each(reports_data, function (data_Index, data_Values) {

                sum_price1 += data_Values.pPrice1;
                sum_price2 += data_Values.pPrice2;
                sum_amount += data_Values.pAmount;
                sum_cost += data_Values.nCost;
                sum_profit += data_Values.nProfit;

                if (report_type === 1) {
                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row", data: [
                            { text: data_Index + 1, style: RowsCSS },
                            { text: `<a onclick='qurey_detail("` + data_Values.values + `")'>` + data_Values.lable + `</a>`, style: RowsCSS },
                            { text: $.number(data_Values.pAmount), style: RowsCSS },
                            { text: $.number(data_Values.nCost, 2), style: RowsCSS },/*ทุน*/
                            { text: $.number(data_Values.pPrice1, 2), style: RowsCSS },
                            { text: $.number(data_Values.pPrice2, 2), style: RowsCSS },
                            { text: $.number(data_Values.nProfit, 2), style: RowsCSS },/*กำไร*/
                        ]
                    }));
                }
                else if (report_type === 2) {
                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row", data: [
                            { text: data_Index + 1, style: RowsCSS },/*ลำดับ*/
                            { text: `<a onclick='qurey_month("` + data_Values.values + `")'>` + data_Values.lable + `</a>`, style: RowsCSS },/*ปี*/
                            { text: $.number(data_Values.pAmount), style: RowsCSS },/*จำนวน*/
                            { text: $.number(data_Values.nCost, 2), style: RowsCSS },/*ทุน*/
                            { text: $.number(data_Values.pPrice1, 2), style: RowsCSS },/*ยอดขาย*/
                            { text: $.number(data_Values.pPrice2, 2), style: RowsCSS },/*ยอดขาย*/
                            { text: $.number(data_Values.nProfit, 2), style: RowsCSS },/*กำไร*/
                        ]
                    }));
                }
            });

            $("#" + table_name + " tfoot").append(RenderRows({
                rowtype: "header",
                data: [
                    { text: "ยอดขายทั้งหมด", style: RowsCSS_2, colspan: 2 },
                    { text: $.number(sum_amount), style: RowsCSS, rowspan: 1, colspan: 1 },
                    { text: $.number(sum_cost, 2), style: RowsCSS, rowspan: 1, colspan: 1 },
                    { text: $.number(sum_price1, 2), style: RowsCSS, rowspan: 1, colspan: 1 },
                    { text: $.number(sum_price2, 2), style: RowsCSS, rowspan: 1, colspan: 1 },
                    { text: $.number(sum_profit, 2), style: RowsCSS, rowspan: 1, colspan: 1 },
                ]
            }
            //, {
            //    rowtype: "header",
            //    data: [
            //        { text: "ยอดขายทั้งหมด", style: RowsCSS_2, colspan: 3 },
            //        { text: $.number(sum_price1), style: RowsCSS, rowspan: 1 },
            //        { text: $.number(sum_price2), style: RowsCSS, rowspan: 1 }
            //    ]
            //}
            ));

        }
        else if (report_type == 3) {
            reports_data = this.reports_data.report_Details;

            $.each(reports_data, function (data_Index, data_Values) {
                var product_data = data_Values.products;
                var amount = 0, price1 = 0, price2 = 0, cost = 0, profit = 0 ;
                $.each(product_data, function (product_data_index, product_data_values) {

                    Total_amount += product_data_values.pSumAmount;
                    Total_price1 += product_data_values.pSumPrice1;
                    Total_price2 += product_data_values.pSumPrice2;
                    Total_Cost += product_data_values.pSumCost;
                    Total_Profit += product_data_values.pSumProfit;

                    amount += product_data_values.pSumAmount;
                    price1 += product_data_values.pSumPrice1;
                    price2 += product_data_values.pSumPrice2;
                    cost += product_data_values.pSumCost;
                    profit += product_data_values.pSumProfit;

                    if (product_data_index === 0) {
                        $("#" + table_name + " tbody").append(RenderRows({
                            rowtype: "row", data: [
                                { text: data_Index + 1, style: RowsCSS_1, rowspan: product_data.length },
                                { text: data_Values.sHopName, style: RowsCSS_1, rowspan: product_data.length },
                                { text: data_Values.pTypeName, style: RowsCSS_1, rowspan: product_data.length },
                                { text: product_data_values.pRoductBarCode, style: RowsCSS },
                                { text: product_data_values.pRoductName, style: RowsCSS },
                                { text: product_data_values.pSumAmount, style: RowsCSS },
                                { text: $.number(product_data_values.pSumCost, 2), style: RowsCSS },
                                { text: $.number(product_data_values.pSumPrice1, 2), style: RowsCSS },
                                { text: $.number(product_data_values.pSumPrice2, 2), style: RowsCSS },
                                { text: $.number(product_data_values.pSumProfit, 2), style: RowsCSS }
                            ]
                        }));
                    } else {
                        $("#" + table_name + " tbody").append(RenderRows({
                            rowtype: "row", data: [
                                { text: product_data_values.pRoductBarCode, style: RowsCSS },
                                { text: product_data_values.pRoductName, style: RowsCSS },
                                { text: product_data_values.pSumAmount, style: RowsCSS },
                                { text: $.number(product_data_values.pSumCost, 2), style: RowsCSS },
                                { text: $.number(product_data_values.pSumPrice1, 2), style: RowsCSS },
                                { text: $.number(product_data_values.pSumPrice2, 2), style: RowsCSS },
                                { text: $.number(product_data_values.pSumProfit, 2), style: RowsCSS }
                            ]
                        }));
                    }

                    //if (product_data_index == (product_data.length - 1)) {
                      
                    //}
                });

                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row", data: [
                        { text: 'รวม ' + data_Values.pTypeName, style: RowsCSS_1, colspan: 3 },
                        { text: '' },
                        { text: '' },
                        { text: $.number(amount), style: RowsCSS_1 },
                        { text: $.number(cost, 2), style: RowsCSS_1 },
                        { text: $.number(price1, 2), style: RowsCSS_1 },
                        { text: $.number(price2, 2), style: RowsCSS_1 },
                        { text: $.number(profit, 2), style: RowsCSS_1 }
                    ]
                }));
            });

            //$("#" + table_name + " tfoot").append(RenderRows({
            //    rowtype: "header", data: [
            //        { text: "จำนวนทั้งหมด", style: RowsCSS_2, colspan: 6 },
            //        { text: $.number(Total_amount) + " ชิ้น", style: RowsCSS, colspan: 2 }
            //    ]
            //}, {
            //    rowtype: "header", data: [
            //        { text: "ยอดขายทั้งหมด", style: RowsCSS_2, colspan: 6 },
            //        { text: $.number(Total_price1) + " บาท", style: RowsCSS, colspan: 1 },
            //        { text: $.number(Total_price2) + " บาท", style: RowsCSS, colspan: 1 }
            //    ]
            //}));

            $("#" + table_name + " tfoot").append(RenderRows( {
                rowtype: "header",
              
                data: [
                    { text: "ยอดขายทั้งหมด", style: RowsCSS_2, colspan: 5 },
                    { text: $.number(Total_amount), style: RowsCSS, rowspan: 1, colspan: 1 },
                    { text: $.number(Total_Cost, 2), style: RowsCSS, rowspan: 1, colspan: 1 },
                    { text: $.number(Total_price1, 2), style: RowsCSS, rowspan: 1, colspan: 1 },
                    { text: $.number(Total_price2, 2), style: RowsCSS, rowspan: 1, colspan: 1 },
                    { text: $.number(Total_Profit, 2), style: RowsCSS, rowspan: 1, colspan: 1 },
                ]
            }));

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

        var Total_amount = 0, Total_price1 = 0, Total_price2 = 0, Total_Cost = 0, Total_Profit = 0;

        $("#" + table_name + " tbody").append(RenderRows({
            rowtype: "header", data:
                [
                    { text: "ลำดับ", style: HeaderCSS, rowspan: 2 },
                    { text: "ชื่อร้านค้า", style: HeaderCSS, rowspan: 2 },
                    { text: "ประเภท", style: HeaderCSS, rowspan: 2 },
                    { text: "Barcode", style: HeaderCSS, rowspan: 2 },
                    { text: "ชื่อสินค้า", style: HeaderCSS, rowspan: 2 },
                    { text: "จำนวน", style: HeaderCSS, rowspan: 2 },
                    { text: "ราคาทุน", style: HeaderCSS + "width:150px;", rowspan: 2 },
                    { text: "ยอดขาย/บาท", style: HeaderCSS, rowspan: 1, colspan: 2 },
                    { text: "กำไร", style: HeaderCSS + "width:150px;", rowspan: 2 },
                ]
        }));

        $("#" + table_name + " tbody").append(RenderRows({
            rowtype: "header", data:
                [
                    { text: "ระบบภายในโรงเรียน", style: HeaderCSS + "width:200px;", rowspan: 1, colspan: 1 },
                    { text: "QR Code พร้อมเพย์", style: HeaderCSS + "width:200px;", rowspan: 1, colspan: 1 }
                ]
        }));

        var detail_data = this.reports_data.report_Details;

        $.each(detail_data, function (data_Index, data_Values) {
            var product_data = data_Values.products;
            var amount = 0, price1 = 0, price2 = 0, cost = 0, profit = 0;
            $.each(product_data, function (product_data_index, product_data_values) {

                Total_amount += product_data_values.pSumAmount;
                Total_price1 += product_data_values.pSumPrice1;
                Total_price2 += product_data_values.pSumPrice2;
                Total_Cost += product_data_values.pSumCost;
                Total_Profit += product_data_values.pSumProfit;

                amount += product_data_values.pSumAmount;
                price1 += product_data_values.pSumPrice1;
                price2 += product_data_values.pSumPrice2;
                cost += product_data_values.pSumCost;
                profit += product_data_values.pSumProfit;

                if (product_data_index === 0) {
                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row", data: [
                            { text: data_Index + 1, style: RowsCSS_1, rowspan: product_data.length },
                            { text: data_Values.sHopName, style: RowsCSS_1, rowspan: product_data.length },
                            { text: data_Values.pTypeName, style: RowsCSS_1, rowspan: product_data.length },
                            { text: product_data_values.pRoductBarCode, style: RowsCSS },
                            { text: product_data_values.pRoductName, style: RowsCSS },
                            { text: product_data_values.pSumAmount, style: RowsCSS },
                            { text: $.number(product_data_values.pSumCost, 2), style: RowsCSS },
                            { text: $.number(product_data_values.pSumPrice1, 2), style: RowsCSS },
                            { text: $.number(product_data_values.pSumPrice2, 2), style: RowsCSS },
                            { text: $.number(product_data_values.pSumProfit, 2), style: RowsCSS }
                        ]
                    }));
                }
                else {
                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row", data: [
                            { text: product_data_values.pRoductBarCode, style: RowsCSS },
                            { text: product_data_values.pRoductName, style: RowsCSS },
                            { text: product_data_values.pSumAmount, style: RowsCSS },
                            { text: $.number(product_data_values.pSumCost, 2), style: RowsCSS },
                            { text: $.number(product_data_values.pSumPrice1, 2), style: RowsCSS },
                            { text: $.number(product_data_values.pSumPrice2, 2), style: RowsCSS },
                            { text: $.number(product_data_values.pSumProfit, 2), style: RowsCSS }
                        ]
                    }));
                }
            });


            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row", data: [
                    { text: 'รวม ' + data_Values.pTypeName, style: RowsCSS_1, colspan: 3 },
                    { text: '' },
                    { text: '' },
                    { text: $.number(amount), style: RowsCSS_1 },
                    { text: $.number(cost, 2), style: RowsCSS_1 },
                    { text: $.number(price1, 2), style: RowsCSS_1 },
                    { text: $.number(price2, 2), style: RowsCSS_1 },
                    { text: $.number(profit, 2), style: RowsCSS_1 }
                ]
            }));
        });

        $("#" + table_name + " tfoot").append(RenderRows({
            rowtype: "header",

            data: [
                { text: "ยอดขายทั้งหมด", style: RowsCSS_2, colspan: 5 },
                { text: $.number(Total_amount), style: RowsCSS, rowspan: 1, colspan: 1 },
                { text: $.number(Total_Cost, 2), style: RowsCSS, rowspan: 1, colspan: 1 },
                { text: $.number(Total_price1, 2), style: RowsCSS, rowspan: 1, colspan: 1 },
                { text: $.number(Total_price2, 2), style: RowsCSS, rowspan: 1, colspan: 1 },
                { text: $.number(Total_Profit, 2), style: RowsCSS, rowspan: 1, colspan: 1 },
            ]
        }));

        //$("#" + table_name + " tfoot").append(RenderRows({
        //    rowtype: "header", data: [
        //        { text: "จำนวนทั้งหมด", style: RowsCSS_2, colspan: 6 },
        //        { text: $.number(Total_amount) + " ชิ้น", style: RowsCSS, colspan: 2 }
        //    ]
        //}, {
        //    rowtype: "header", data: [
        //        { text: "ยอดขายทั้งหมด", style: RowsCSS_2, colspan: 6 },
        //        { text: $.number(Total_price1) + " บาท", style: RowsCSS, colspan: 1 },
        //        { text: $.number(Total_price2) + " บาท", style: RowsCSS, colspan: 1 }
        //    ]
        //}));


        $("body").mLoading('hide');
    };


    this.export_excel = function () {
        $("body").mLoading();
        var dt = new Date();
        var json;
        var xhr;
        var report_type = this.reports_data.report_type;
        var file_name = 'รายงาน' + this.reports_data.header_text + "_" + dt.format("ddMMyyyyHHmmssss") + '.xls';

        if (report_type === undefined) {

            json = JSON.stringify({ search: searchData });
            xhr = new XMLHttpRequest();

            xhr.open("POST", "/Report/reportTypeProduct.aspx/export_data", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                $("body").mLoading('hide');
            };
            xhr.send(json);
        } else if (report_type == 3) {
            json = JSON.stringify({ search: searchData });
            xhr = new XMLHttpRequest();

            xhr.open("POST", "/Report/reportTypeProduct.aspx/export03_data", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                $("body").mLoading('hide');
            };
            xhr.send(json);
        }
        else {
            json = JSON.stringify({ search: searchData });
            xhr = new XMLHttpRequest();

            xhr.open("POST", "/Report/reportTypeProduct.aspx/export02_data", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                $("body").mLoading('hide');
            };
            xhr.send(json);
        }




    };


});