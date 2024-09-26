var RPQuantityStudent = (function () {
    this.Report_Quantity_Student = [];
    var HeaderCSS = "font-weight: bold;text-align: center;color: rgb(255, 255, 255);text-align: center !important; background-color: rgb(51, 122, 183);";
    var RowsCSS = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_1 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: top !important;padding:0px;";
    var RowsCSS_2 = "background-color: rgb(255, 255, 255);text-align: right !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_3 = "background-color: rgb(255, 255, 255);text-align: right !important; vertical-align: middle !important; padding-left:0px; padding-top:1px; padding-bottom:0px; padding-right:7px; background-color:#eee;";
    var RowsCSS_4 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: top !important;padding:0px;background-color:#eee;";
    var RowsCSS_fontbold = "font-weight: bold; background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px;";

    this.RenderHtml_Quantity_ClassRoom = function (table_name, export_file) {

        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        var sort_type = $("#sort_type").val();

        if (sort_type == "1") {
            $("#" + table_name + " tbody").append(
                RenderRows(
                    {
                        rowtype: "row",
                        data: [
                            { text: "ชั้นเรียน", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },
                            { text: "จำนวนห้องเรียน", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },
                            { text: "รายงานสถิติแสดงจำนวนนักเรียน ชาย-หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1, colspan: 2 },
                            { text: "รวมนักเรียน", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 }
                        ]
                    },
                    {
                        rowtype: "row",
                        data: [
                            { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                            { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 }
                        ]
                    }
                ));
        } else {
            $("#" + table_name + " tbody").append(
                RenderRows(
                    {
                        rowtype: "row",
                        data: [
                            { text: "ระดับชั้น/ห้อง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },
                            { text: "รายงานสถิติแสดงจำนวนนักเรียน ชาย-หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1, colspan: 2 },
                            { text: "รวมนักเรียน", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },
                            { text: "แผนการเรียน", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },
                        ]
                    },
                    {
                        rowtype: "row",
                        data: [
                            { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                            { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 }
                        ]
                    }
                ));
        }

        if (sort_type == "1") {
            var resultRoom = 0; var resultMale = 0; var resultFemalemale = 0; var resultStudent = 0;
            var Level0_data = this.Report_Quantity_ClassRoom.level0s;

            $.each(Level0_data, function (Level0_Index, Level0_Values) {
                var Level01_data = Level0_Values.Level1s;

                var SumRoom = 0; var SumMale = 0; var SumFemalemale = 0; var SumStudent = 0;
                $.each(Level01_data, function (Level1_Index, Level1_Values) {
                    var TotalRoom = Level1_Values.TotalRoom;
                    var TotalMale = Level1_Values.TotalMale;
                    var TotalFemale = Level1_Values.TotalFemale;
                    var TotalStudent = TotalMale + TotalFemale;

                    SumRoom += TotalRoom;
                    SumMale += TotalMale;
                    SumFemalemale += TotalFemale;
                    SumStudent = SumMale + SumFemalemale;

                    resultRoom += Level1_Values.TotalRoom;
                    resultMale += Level1_Values.TotalMale;
                    resultFemalemale += Level1_Values.TotalFemale;
                    resultStudent = resultMale + resultFemalemale;

                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row",
                        data: [
                            { text: Level1_Values.ClassName, style: RowsCSS },
                            { text: TotalRoom, style: RowsCSS },
                            { text: TotalMale, style: RowsCSS },
                            { text: TotalFemale, style: RowsCSS },
                            { text: TotalStudent, style: RowsCSS }
                        ]
                    }));
                });

                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row",
                    data: [
                        { text: "รวม", style: RowsCSS_fontbold },
                        { text: SumRoom, style: RowsCSS_fontbold },
                        { text: SumMale, style: RowsCSS_fontbold },
                        { text: SumFemalemale, style: RowsCSS_fontbold },
                        { text: SumStudent, style: RowsCSS_fontbold }
                    ]
                }));

            });

            $("#" + table_name + " tfoot").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: "รวมทั้งหมด", style: RowsCSS_fontbold },
                    { text: resultRoom, style: RowsCSS_fontbold },
                    { text: resultMale, style: RowsCSS_fontbold },
                    { text: resultFemalemale, style: RowsCSS_fontbold },
                    { text: resultStudent, style: RowsCSS_fontbold }
                ]
            }));

            if (export_file === true) {
                $("#" + table_name).append("<tfoot>");
                var Colspan = 5;

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
                        data: [
                            { text: "", colspan: Colspan - 2 },
                            { text: "พิมพ์วันที่ :", style: "font-size:13px;text-align: right !important;" },
                            { text: "{day_print}", style: "font-size:13px;text-align: right !important;" }
                        ]
                    },
                    {
                        rowtype: "header",
                        data: [
                            { text: "", colspan: Colspan - 2 },
                            { text: "เวลา :", style: "font-size:13px;text-align: right !important;" },
                            { text: "{time_print}", style: "font-size:13px;text-align: right !important;" }
                        ]
                    },
                    {
                        rowtype: "header",
                        data: [
                            { text: "", colspan: Colspan }
                        ]
                    }
                ))
            }
            $("body").mLoading('hide');

        } else {

            var Grandtotal_male = 0; var Grandtotal_female = 0; var Grandtotal_student = 0;
            var Level0_data = this.Report_Quantity_ClassRoom.level0s;

            $.each(Level0_data, function (Level0_Index, Level0_Values) {
                var Level01_data = Level0_Values.Level1s;

                $.each(Level01_data, function (Level1_Index, Level1_Values) {
                    var Level02_data = Level1_Values.Level2s;

                    var total_male = 0; var total_female = 0; var total_student = 0;
                    $.each(Level02_data, function (Level2_Index, Level2_Values) {
                        var sum_student = (Level2_Values.male + Level2_Values.female);
                        total_male += Level2_Values.male;
                        total_female += Level2_Values.female;
                        total_student = (total_male + total_female);

                        Grandtotal_male += Level2_Values.male;
                        Grandtotal_female += Level2_Values.female;
                        Grandtotal_student = (Grandtotal_male + Grandtotal_female);

                        $("#" + table_name + "").append(RenderRows({
                            rowtype: "row",
                            data: [
                                { text: Level2_Values.ClassFullName, style: RowsCSS },
                                { text: Level2_Values.male, style: RowsCSS },
                                { text: Level2_Values.female, style: RowsCSS },
                                { text: sum_student, style: RowsCSS },
                                { text: Level0_Values.GroupClassName, style: RowsCSS }
                            ]
                        }));
                    });

                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row",
                        data: [
                            { text: "รวม", style: RowsCSS_fontbold },
                            { text: total_male, style: RowsCSS_fontbold },
                            { text: total_female, style: RowsCSS_fontbold },
                            { text: total_student, style: RowsCSS_fontbold },
                            { text: "", style: RowsCSS_fontbold }
                        ]
                    }));

                });

            });

            $("#" + table_name + " tfoot").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: "รวมทั้งหมด", style: RowsCSS_fontbold },
                    { text: Grandtotal_male, style: RowsCSS_fontbold },
                    { text: Grandtotal_female, style: RowsCSS_fontbold },
                    { text: Grandtotal_student, style: RowsCSS_fontbold },
                    { text: "", style: RowsCSS_fontbold }
                ]
            }));

            if (export_file === true) {
                $("#" + table_name).append("<tfoot>")
                var Colspan = 5;

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
                        data: [
                            { text: "", colspan: Colspan - 2 },
                            { text: "พิมพ์วันที่ :", style: "font-size:13px;text-align: right !important;" },
                            { text: "{day_print}", style: "font-size:13px;text-align: right !important;" }
                        ]
                    },
                    {
                        rowtype: "header",
                        data: [
                            { text: "", colspan: Colspan - 2 },
                            { text: "เวลา :", style: "font-size:13px;text-align: right !important;" },
                            { text: "{time_print}", style: "font-size:13px;text-align: right !important;" }
                        ]
                    },
                    {
                        rowtype: "header",
                        data: [
                            { text: "", colspan: Colspan }
                        ]
                    }
                ))
            }
            $("body").mLoading('hide');
        };

    }


    this.export_excel = function () {
        $("body").mLoading();
        var dt = new Date();

        var file_name = this.Report_Quantity_ClassRoom.headerText + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getDate() + dt.getMinutes() + dt.getSeconds() + ' .xls';
        this.RenderHtml_Quantity_ClassRoom('table_exports', true);

        var param = {
            "filename": "filename02",
            "tabledata": $("#export_excel").html()
        };

        $.post("/export_excel.aspx", param, function (data) {
            downloadFile(file_name, 'data:application/xml;charset=utf-8;base64,', data)
        });

    };



});