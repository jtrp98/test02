var RP_AcademicResult = (function () {
    this.Student_AcademicResult = [];
    var HeaderCSS = "font-weight: bold;text-align: center;color: rgb(255, 255, 255);text-align: center !important; background-color: rgb(51, 122, 183);";
    var RowsCSS = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_1 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: top !important;padding:0px;";
    var RowsCSS_2 = "background-color: rgb(255, 255, 255);text-align: right !important; vertical-align: middle !important;padding:0px;";

    this.RenderHtml_Student_AcademicResult = function (table_name, export_file) {

        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS;
            RowsCSS_1 += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");


        var Layer0_data = this.Student_AcademicResult.LaYer0s;
        let HeaderColumns = [];
        HeaderColumns.push({ text: "กลุ่มสาระ/ชั้น", style: HeaderCSS + (export_file === true ? "width:250px;" : "") });
        $.each(Layer0_data, function (Layer0_Index, Layer0_Values) {
            HeaderColumns.push({ text: Layer0_Values.ClassName, style: HeaderCSS + (export_file === true ? "width:100px;" : "") });
            //var Layer1_data = Layer0_Values.LaYer1s;
        });

        HeaderColumns.push({ text: "รวม", style: HeaderCSS + (export_file === true ? "width:70px;" : "") });

        $("#" + table_name + " tbody").append(
            RenderRows({
                rowtype: "row",
                data: HeaderColumns
            }));

        $.each(this.Student_AcademicResult.viewtts1s, function (Layer1_Index, Layer1_Values) {

            let dataRow = [];
            dataRow.push({ text: Layer1_Values.CousTypename, style: HeaderCSS });

            var sUmGradEsCore = 0;
            var sUmGradEsCore3uP = 0;
            var TotalGradEsCore3uP = 0;

            $.each(Layer0_data, function (Layer2_Index, Layer2_Values) {
                let LevelFindData = Layer1_Values.cLassRoom.filter(function (x) {
                    return x.ClassID == Layer2_Values.ClassID;
                });

                if (LevelFindData.length > 0) {
                    //dataRow.push({ text: LevelFindData[0].GradEsCore3uP, style: RowsCSS_1, rowspan: 1 });
                    dataRow.push({ text: LevelFindData[0].Grade30UpPercent.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_1 });
                    sUmGradEsCore += LevelFindData[0].StudentCount;
                    sUmGradEsCore3uP += LevelFindData[0].Grade30Up;
                } else {
                    dataRow.push({ text: "0", style: RowsCSS_1 });
                }

            });
            console.log("sUmGradEsCore3uP" + sUmGradEsCore3uP);
            console.log("sUmGradEsCore" + sUmGradEsCore);
            TotalGradEsCore3uP = ((sUmGradEsCore3uP / sUmGradEsCore) * 100);
            console.log(TotalGradEsCore3uP)
            dataRow.push({ text: ((isNaN(TotalGradEsCore3uP))? "0"  : TotalGradEsCore3uP.toFixed(2)), style: RowsCSS_1 });

            $("#" + table_name + " tbody").append(
                RenderRows({
                    rowtype: "row",
                    data: dataRow
                }));

        });


        if (export_file === true) {
            $("#" + table_name).append("<tbody>");
            var Colspan = 14;

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
                        { text: this.Student_AcademicResult.HeaderText, colspan: Colspan, style: "font-size:15px;" }
                    ]
                },
                {
                    rowtype: "header",
                    data: [
                        { text: "", colspan: Colspan - 3 },
                        { text: "พิมพ์วันที่ :", style: "font-size:13px;text-align: right !important; width:70px;" },
                        { text: "{day_print}", colspan: 2, style: "font-size:13px;text-align: right !important; width:70px;" }
                    ]
                },
                {
                    rowtype: "header",
                    data: [
                        { text: "", colspan: Colspan - 3 },
                        { text: "เวลา :", style: "font-size:13px;text-align: right !important; width:70px;" },
                        { text: "{time_print}", colspan: 2, style: "font-size:13px;text-align: right !important; width:70px;" }
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

    };


    this.export_excel = function () {
        var dt = new Date();
        var file_name = this.Student_AcademicResult.HeaderText + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getDate() + dt.getMinutes() + dt.getSeconds() + ' .xls';

        this.RenderHtml_Student_AcademicResult('table_exports', true);

        var param = {
            "filename": "filename02",
            "tabledata": $("#export_excel").html()
        };
        $.post("/export_excel.aspx", param, function (data) {
            downloadFile(file_name, 'data:application/xml;charset=utf-8;base64,', data)
        });


    }





    //this.export_excel = function () {
    //    $("body").mLoading();
    //    var dt = new Date();
    //    var json = JSON.stringify({ search: sEarchData });
    //    var xhr = new XMLHttpRequest();
    //    var file_name = this.Student_AcademicResult.HeaderText + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getDate() + dt.getMinutes() + dt.getSeconds() + ' .xls';

    //    xhr.open("POST", "/Report/Report_AcademicResult.aspx/export_data", true);
    //    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
    //    xhr.responseType = "blob";
    //    xhr.onload = function () {
    //        saveAs(xhr.response, file_name);
    //        $("body").mLoading('hide');
    //    };
    //    xhr.send(json);
    //};




});