var RePort_DesirableFeatues = (function () {
    this.Student_DesirableFeatues = [];
    var HeaderCSS = "font-weight: bold;text-align: center;color: rgb(255, 255, 255);text-align: center !important; background-color: rgb(51, 122, 183);";
    var RowsCSS = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_1 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: top !important;padding:0px;";
    var RowsCSS_3 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px;background-color:#eee;";

    this.RenderHtml_Student_DesirableFeatues = function (table_name, export_file) {

        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS;
            RowsCSS_1 += ExportCSS;
            RowsCSS_3 += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            RowsCSS_3 = RowsCSS_3.replace(ExportCSS, "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        var TypeTxT = "";
        var sOrtType = $("#sOrt_Type").val();

        if (sOrtType == "0") {
            TypeTxT = "ผลการประเมินคุณลักษณะอันพึงประสงค์";
        } else if (sOrtType == "1") {
            TypeTxT = "ผลการประเมินอ่านคิดวิเคราะห์";
        } else if (sOrtType == "2") {
            TypeTxT = "ผลการประเมินสมรรถนะ";
        }

        $("#" + table_name + " tbody").append(
            RenderRows(
                {
                    rowtype: "row",
                    data: [
                        { text: "ชั้นเรียน", style: HeaderCSS + (export_file === true ? "width:250px;" : ""), rowspan: 2, colspan: 1 },
                        { text: TypeTxT, style: HeaderCSS, rowspan: 1, colspan: 4 },
                        { text: "คนที่ผ่านเกณฑ์", style: HeaderCSS + (export_file === true ? "width:115px;" : ""), rowspan: 2, colspan: 1 }
                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        { text: "ปรับปรุง", style: HeaderCSS + (export_file === true ? "width:115x;" : ""), rowspan: 1 },
                        { text: "พอใช้", style: HeaderCSS + (export_file === true ? "width:115px;" : ""), rowspan: 1 },
                        { text: "ดี", style: HeaderCSS + (export_file === true ? "width:115px;" : ""), rowspan: 1 },
                        { text: "ดีมาก", style: HeaderCSS + (export_file === true ? "width:115px;" : ""), rowspan: 1 },
                    ]
                }));

        var Layer0_data = this.Student_DesirableFeatues.layer0s;
        var TotalpErcen = 0;
        var ScoreTotal = 0;
        $.each(Layer0_data, function (Layer0_Index, Layer0_Values) {
            $("#" + table_name + " tbody").append(
                RenderRows({
                    rowtype: "row",
                    data: [
                        { text: Layer0_Values.cLassFullName, style: RowsCSS + (export_file === true ? "width:70px;" : ""), rowspan: 1 },
                        { text: Layer0_Values.sCore0, style: RowsCSS + (export_file === true ? "width:70px;" : ""), rowspan: 1 },
                        { text: Layer0_Values.sCore1, style: RowsCSS + (export_file === true ? "width:70px;" : ""), rowspan: 1 },
                        { text: Layer0_Values.sCore2, style: RowsCSS + (export_file === true ? "width:70px;" : ""), rowspan: 1 },
                        { text: Layer0_Values.sCore3, style: RowsCSS + (export_file === true ? "width:70px;" : ""), rowspan: 1 },
                        { text: (Layer0_Values.pErcen23).toFixed(2), style: RowsCSS + (export_file === true ? "width:70px;" : ""), rowspan: 1 }
                    ]
                }));
        });



        if (export_file === true) {
            $("#" + table_name).append("<tbody>");
            var Colspan = 6;

            $("#" + table_name + " thead").append(RenderRows(
                {
                    rowtype: "header",
                    data: [
                        { text: $("input[id*=hdfschoolname]").val(), colspan: Colspan, style: "font-size:20px;" }
                    ]
                },
                {
                    rowtype: "header",
                    data: [
                        { text: this.Student_DesirableFeatues.hEadder, colspan: Colspan, style: "font-size:15px;" }
                    ]
                },
                {
                    rowtype: "header",
                    data: [
                        { text: "พิมพ์วันที่ :", style: "font-size:13px;text-align: right !important; width:70px;", colspan: Colspan - 1 },
                        { text: "{day_print}", style: "font-size:13px;text-align: right !important; width:115px;", colspan: 1 }
                    ]
                },
                {
                    rowtype: "header",
                    data: [
                        { text: "เวลา :", style: "font-size:13px;text-align: right !important; width:70px;", colspan: Colspan - 1 },
                        { text: "{time_print}", style: "font-size:13px;text-align: right !important; width:115px;", colspan: 1 }
                    ]
                },
                {
                    rowtype: "header",
                    data: [
                        { text: "", colspan: Colspan }
                    ]
                }))
        }




        $("body").mLoading('hide');

    }



    this.export_excel = function () {
        $("body").mLoading();
        var dt = new Date();

        var file_name = this.Student_DesirableFeatues.hEadder + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getDate() + dt.getMinutes() + dt.getSeconds() + ' .xls';

        this.RenderHtml_Student_DesirableFeatues('table_exports', true);

        var param = {
            "filename": "filename02",
            "tabledata": $("#export_excel").html()
        };

        $.post("/export_excel.aspx", param, function (data) {
            downloadFile(file_name, 'data:application/xml;charset=utf-8;base64,', data)
        });

    };






});