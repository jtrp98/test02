var report_sales = (function () {
    this.reports_data = [];
    var HeaderCSS = "font-weight: bold;text-align: center;text-align: center !important;";
    var RowsCSS = "text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_1 = "text-align: center !important; vertical-align: top !important;padding:0px;";
    var RowsCSS_2 = "text-align: right !important; vertical-align: middle !important;padding:0px;";
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
        var sum_money1 = 0, sum_money2 = 0, sum_cost = 0, sum_lucre = 0;

        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header", data: [{
                        text: "ลำดับ", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:100px;" , rowspan:2
                    }, {
                        text: "ร้าน", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:150px;" , rowspan:2
                    }, {
                        text: "ยอดขาย", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:150px;" , colspan:2
                    }]
                }));

        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header", data: [{
                        text: "ระบบภายในโรงเรียน", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:100px;"
                    }, {
                        text: "QR Code พร้อมเพย์", style: HeaderCSS + (export_file === true ? "" : "min-") + "width:150px;"
                    }]
                }));

        $.each(this.reports_data, function (data_Index, data_Values) {
            sum_money1 += data_Values.SUM_MONEY1;
            sum_money2 += data_Values.SUM_MONEY2;
            //sum_cost += data_Values.sum_cost;
            //sum_lucre += data_Values.sum_lucre;
            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row", data: [
                    {
                        text: data_Index + 1, style: RowsCSS
                    },
                    {
                        text: data_Values.shop_name, style: RowsCSS
                    },
                    {
                        text: $.number(data_Values.SUM_MONEY1, 2) + " บาท", style: RowsCSS
                    },
                    {
                        text: $.number(data_Values.SUM_MONEY2, 2) + " บาท", style: RowsCSS
                    }
                ]
            }));
        });

        $("#" + table_name + " tbody").append(RenderRows({
            rowtype: "row", data: [               
                {
                    text: 'รวม', style: RowsCSS , colspan:2
                },
                {
                    text: $.number(sum_money1,2) + " บาท", style: RowsCSS
                },
                {
                    text: $.number(sum_money2, 2) + " บาท", style: RowsCSS
                }
            ]
        }));

        //$("body").mLoading('hide');
    };

    this.export_excel = function () {
        //$("body").mLoading();
        var dt = new Date();
        var json;
        var xhr;
        var report_type = this.reports_data.report_type;
        var file_name = 'รายงานการขายแบบรวมร้านค้า_' + dt.format("ddMMyyyyHHmmssss") + '.xls';
        //this.RenderHtml_Detail('table_exports', true);
        json = JSON.stringify({ search: searchData });
        xhr = new XMLHttpRequest();
        xhr.open("POST", "/Report/Sales-group.aspx/exportExcel", true);
        xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
        xhr.responseType = 'blob';
        xhr.onload = function () {
            saveAs(xhr.response, file_name);
            //$("body").mLoading('hide');
        };
        xhr.send(json);
    };

});
