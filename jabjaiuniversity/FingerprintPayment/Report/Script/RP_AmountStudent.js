var RPAmountStudent = (function () {
    this.report_AmountStudent = [];
    var HeaderCSS = "font-weight: bold;text-align: center;text-align: center !important;";
    var RowsCSS = "text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_1 = "text-align: center !important; vertical-align: top !important;padding:0px;";
    var RowsCSS_2 = "font-weight: bold; background-color: rgb(255, 255, 255);text-align: right !important; vertical-align: middle !important;padding:0px; padding-right:15px; ";
    var RowsCSS_3 = "font-weight: bold; background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: top !important;padding:0px;";

    this.RenderHtml_AmountStudent = function (table_name, export_file) {
        var ExportCSS = "background-color:#337ab7;width:200px !important;";
        //var ExportCSS = "background-color:#337ab7;width:150px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            //RowsCSS += ExportCSS;
            //RowsCSS_1 += ExportCSS.replace(";min-width", ";width");
            //RowsCSS_2 += ExportCSS;
            //RowsCSS_3 += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replaceAll(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "").replace(";min-width", ";width");
            RowsCSS_2 = RowsCSS_2.replace(ExportCSS, "");
            RowsCSS_3 = RowsCSS_3.replace(ExportCSS, "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        var sort_type = $("#sort_type").val();

        //HeadderTEXT
        if (sort_type == "1") {
            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ชั้นเรียน", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "อายุ (ปี)", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "รวม", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 }
                    ]
                }));
        }
        else if (sort_type == "2") {
            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ศาสนา", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "รวม", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 }
                    ]
                }));
        }
        else if (sort_type == "3") {
            $("#" + table_name + " thead").append(
                RenderRows(
                    {
                        rowtype: "header",
                        data: [
                            { text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },
                            { text: "ชั้นเรียน", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },
                            { text: "รายงานนักเรียนจำแนกตามรายได้บิดา/ปี", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1, colspan: 4 },
                            { text: "รวมนักเรียน", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 2 }
                        ]
                    },
                    {
                        rowtype: "header",
                        data: [
                            { text: "ต่ำกว่า 150,000/ปี", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                            { text: "150,000 - 300,000/ปี", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                            { text: "มากกว่า 300,000/ปี", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 },
                            { text: "ไม่ระบุ", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 }
                        ]
                    }));
        }
        else if (sort_type == "4") {
            $("#" + table_name + " thead").append(
                RenderRows(
                    {
                        rowtype: "header",
                        data: [
                            { text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },
                            { text: "ชั้นเรียน", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },
                            { text: "รายงานนักเรียนจำแนกตามการศึกษาบิดา", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1, colspan: 7 },
                            { text: "รวมนักเรียน", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 2 }
                        ]
                    },
                    {
                        rowtype: "header",
                        data: [
                            { text: "ต่ำกว่าประถมศึกษา", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                            { text: "ประถมศึกษา", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                            { text: "มัธยมศึกษา หรือเทียบเท่า", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 },
                            { text: "ปริญญาตรี หรือเทียบเท่า", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 },
                            { text: "ปริญญาโท", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 },
                            { text: "สูงกว่าปริญญาโท", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 },
                            { text: "ไม่ระบุ", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 }
                        ]
                    }));
        }
        else if (sort_type == "5") {
            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ศาสนา", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "รวม", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 }
                    ]
                }));
        }
        else if (sort_type == "6") {
            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ชื่อจังหวัด", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "รวม", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 }
                    ]
                }));
        }
        else if (sort_type == "7") {
            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ชื่อจังหวัด", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ชื่ออำเภอ", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "รวม", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 }
                    ]
                }));
        }
        else if (sort_type == "8") {
            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ชื่อจังหวัด", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ชื่ออำเภอ", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ชื่อตำบล", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "รวม", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 }
                    ]
                }));
        }
        else if (sort_type == "9") {
            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "เชื้อชาติ", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "รวม", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 1 }
                    ]
                }));
        }
        else if (sort_type == "10") {
            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ระดับชั้น", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "รหัส", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ชื่อนามสกุล", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 }
                    ]
                }));
        }
        else if (sort_type == "11") {
            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ระดับชั้น", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },
                        { text: "จำนวนห้องเรียน", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },
                        { text: "รายงานสถิติแสดงจำนวนนักเรียนชาย-หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1, colspan:2 },
                        { text: "รวมนักเรียน", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },                       
                    ]
                }));

            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },                     
                    ]
                }));
        }
        else if (sort_type == "12") {
            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ระดับชั้น", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },                        
                        { text: "รายงานสถิติแสดงจำนวนนักเรียนชาย-หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1, colspan: 2 },
                        { text: "รวมนักเรียน", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 2 },
                        { text: "ครูประจำชั้น", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 2 },
                    ]
                }));

            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                    ]
                }));
        }
        else if (sort_type == "13") {
            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ระดับชั้น", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 2 },
                        { text: "แผนการเรียน", style: HeaderCSS + (export_file === true ? "width:200px;" : ""), rowspan: 2 },
                        { text: "จำนวนนักเรียนชาย-หญิง", style: HeaderCSS + (export_file === true ? "width:200px;" : ""), rowspan: 1, colspan: 2 },
                        { text: "รวมนักเรียน", style: HeaderCSS + (export_file === true ? "width:150px;" : ""), rowspan: 2 },
                     
                    ]
                }));

            $("#" + table_name + " thead").append(
                RenderRows({
                    rowtype: "header",
                    data: [
                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                    ]
                }));
        }
        else {

        }


        //BODY
        if (sort_type == "1") {
            var Level0_data = this.report_AmountStudent.Level0s;

            $.each(Level0_data, function (Level0_Index, Level0_Values) {
                var Level1_data = Level0_Values.Level1s;

                var SumMale = 0, SumFemale = 0, SumStudent = 0;

                $.each(Level1_data, function (Level1_Index, Level1_Values) {

                    SumMale += Level1_Values.male;
                    SumFemale += Level1_Values.female;
                    SumStudent = SumMale + SumFemale

                    if (Level1_Index == 0) {
                        $("#" + table_name + " tbody").append(RenderRows({
                            rowtype: "row",
                            data: [
                                { text: Level0_Values.ClassName, style: RowsCSS_1, rowspan: Level1_data.length },
                                { text: Level1_Values.StudentAge + " " + 'ปี', style: RowsCSS_1 },
                                { text: Level1_Values.male, style: RowsCSS_1 },
                                { text: Level1_Values.female, style: RowsCSS_1 },
                                { text: (Level1_Values.male + Level1_Values.female), style: RowsCSS_1 }
                            ]
                        }));
                    } else {
                        $("#" + table_name + " tbody").append(RenderRows({
                            rowtype: "row",
                            data: [
                                { text: Level1_Values.StudentAge + " " + 'ปี', style: RowsCSS_1 },
                                { text: Level1_Values.male, style: RowsCSS_1 },
                                { text: Level1_Values.female, style: RowsCSS_1 },
                                { text: (Level1_Values.male + Level1_Values.female), style: RowsCSS_1 }
                            ]
                        }));
                    }
                });

                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row",
                    data: [
                        { text: "จำนวนรวม", style: RowsCSS_2, colspan: 2 },
                        { text: SumMale, style: RowsCSS_3 },
                        { text: SumFemale, style: RowsCSS_3 },
                        { text: SumStudent, style: RowsCSS_3 }
                    ]
                }));

            });


            if (export_file === true) {
                $("#" + table_name).append("<tfoot>");
                var Colspan = 5;
                $("#" + table_name + " thead").prepend(RenderRows(
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
                            text: this.report_AmountStudent.HeaderText,
                            colspan: Colspan,
                            style: "font-size:16px;"
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
           // //$("body").mLoading('hide');
        }
        else if (sort_type == "2") {

            var Level0_data = this.report_AmountStudent.religions;

            //$.each(Level0_data, function (Level0_Index, Level0_Values) {
            //    var Level1_data = Level0_Values.Level1s;
            //    var SumMale = 0, SumFemale = 0, SumStudent = 0;
            //    $.each(Level1_data, function (Level1_Index, Level1_Values) {
            //        SumMale += Level1_Values.male;
            //        SumFemale += Level1_Values.female;
            //        SumStudent = SumMale + SumFemale;

            //        $("#" + table_name + " tbody").append(RenderRows({
            //            rowtype: "row",
            //            data: [
            //                { text: Level1_Index + 1, style: RowsCSS_1 },
            //                { text: Level1_Values.Religion, style: RowsCSS_1 },
            //                { text: Level1_Values.male, style: RowsCSS_1 },
            //                { text: Level1_Values.female, style: RowsCSS_1 },
            //                { text: (Level1_Values.male + Level1_Values.female), style: RowsCSS_1 }
            //            ]
            //        }));
            //    });

            //    $("#" + table_name + " tbody").append(RenderRows({
            //        rowtype: "row",
            //        data: [
            //            { text: "รวม", style: RowsCSS_2, colspan: 2 },
            //            { text: SumMale, style: RowsCSS_3 },
            //            { text: SumFemale, style: RowsCSS_3 },
            //            { text: SumStudent, style: RowsCSS_3 }
            //        ]
            //    }));
            //});

            var SumMale = 0, SumFemale = 0, SumStudent = 0;

            $.each(Level0_data, function (Level0_Index, Level0_Values) {
                var Level1_data = Level0_Values.Level1s;

                SumMale += Level0_Values.male;
                SumFemale += Level0_Values.female;
                SumStudent = SumMale + SumFemale;

                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row",
                    data: [
                        { text: Level0_Index + 1, style: RowsCSS_1 },
                        { text: Level0_Values.Religion, style: RowsCSS_1 },
                        { text: Level0_Values.male, style: RowsCSS_1 },
                        { text: Level0_Values.female, style: RowsCSS_1 },
                        { text: (Level0_Values.male + Level0_Values.female), style: RowsCSS_1 }
                    ]
                }));
            });

            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: "รวม", style: RowsCSS_2, colspan: 2 },
                    { text: SumMale, style: RowsCSS_3 },
                    { text: SumFemale, style: RowsCSS_3 },
                    { text: SumStudent, style: RowsCSS_3 }
                ]
            }));


            if (export_file === true) {
                $("#" + table_name).append("<tfoot>");
                var Colspan = 5;
                $("#" + table_name + " thead").prepend(RenderRows(
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
                            text: this.report_AmountStudent.HeaderText,
                            colspan: Colspan,
                            style: "font-size:16px;"
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
            ////$("body").mLoading('hide');
        }
        else if (sort_type == "3") {
            var Level0_data = this.report_AmountStudent.Level0s;
            $.each(Level0_data, function (Level0_Index, Level0_Values) {
                var Level1_data = Level0_Values.Level1s;

                $.each(Level1_data, function (Level1_Index, Level1_Values) {
                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row",
                        data: [
                            { text: Level1_Index + 1, style: RowsCSS_1 },
                            { text: Level1_Values.RoomName, style: RowsCSS_1 },
                            { text: Level1_Values.cDw, style: RowsCSS_1 },
                            { text: Level1_Values.cBtw, style: RowsCSS_1 },
                            { text: Level1_Values.cUp, style: RowsCSS_1 },
                            { text: Level1_Values.None, style: RowsCSS_1 },
                            { text: Level1_Values.sStuAmount, style: RowsCSS_1 },
                        ]
                    }));
                });

            });

            if (export_file === true) {
                $("#" + table_name).append("<tfoot>");
                var Colspan = 7;
                $("#" + table_name + " thead").prepend(RenderRows(
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
                            text: this.report_AmountStudent.HeaderText,
                            colspan: Colspan,
                            style: "font-size:16px;"
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
                    //{
                    //    rowtype: "header",
                    //    data: [
                    //        { text: "", colspan: Colspan }
                    //    ]
                    //}
                ))
            }

            ////$("body").mLoading('hide');
        }
        else if (sort_type == "4") {
            var Level0_data = this.report_AmountStudent.Level0s;
            $.each(Level0_data, function (Level0_Index, Level0_Values) {
                var Level1_data = Level0_Values.Level1s;

                $.each(Level1_data, function (Level1_Index, Level1_Values) {
                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row",
                        data: [
                            { text: Level1_Index + 1, style: RowsCSS_1 },
                            { text: Level1_Values.RoomName, style: RowsCSS_1 },
                            { text: Level1_Values.Kindergarten, style: RowsCSS_1 },
                            { text: Level1_Values.Primary, style: RowsCSS_1 },
                            { text: Level1_Values.HighSchool, style: RowsCSS_1 },
                            { text: Level1_Values.Bachelor, style: RowsCSS_1 },
                            { text: Level1_Values.Master, style: RowsCSS_1 },
                            { text: Level1_Values.Doctor, style: RowsCSS_1 },
                            { text: Level1_Values.None, style: RowsCSS_1 },
                            { text: Level1_Values.sStuAmount, style: RowsCSS_1 }
                        ]
                    }));
                });
            });

            if (export_file === true) {
                $("#" + table_name).append("<tfoot>");
                var Colspan = 10;
                $("#" + table_name + " thead").prepend(RenderRows(
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
                            text: this.report_AmountStudent.HeaderText,
                            colspan: Colspan,
                            style: "font-size:16px;"
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

           // //$("body").mLoading('hide');
        }
        else if (sort_type == "5") {
            var Level0_data = this.report_AmountStudent.Level0s;
            $.each(Level0_data, function (Level0_Index, Level0_Values) {
                var Level1_data = Level0_Values.Level1s;
                var SumMale = 0, SumFemale = 0, SumStudent = 0;
                $.each(Level1_data, function (Level1_Index, Level1_Values) {
                    SumMale += Level1_Values.male;
                    SumFemale += Level1_Values.female;
                    SumStudent = SumMale + SumFemale;
                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row",
                        data: [
                            { text: Level1_Index + 1, style: RowsCSS_1 },
                            { text: Level1_Values.Religion, style: RowsCSS_1 },
                            { text: Level1_Values.male, style: RowsCSS_1 },
                            { text: Level1_Values.female, style: RowsCSS_1 },
                            { text: (Level1_Values.male + Level1_Values.female), style: RowsCSS_1 }
                        ]
                    }));
                });
                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row",
                    data: [
                        { text: "รวม", style: RowsCSS_2, colspan: 2 },
                        { text: SumMale, style: RowsCSS_3 },
                        { text: SumFemale, style: RowsCSS_3 },
                        { text: SumStudent, style: RowsCSS_3 }
                    ]
                }));
            });
            if (export_file === true) {
                $("#" + table_name).append("<tfoot>");
                var Colspan = 5;
                $("#" + table_name + " thead").prepend(RenderRows(
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
                            text: this.report_AmountStudent.HeaderText,
                            colspan: Colspan,
                            style: "font-size:16px;"
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
            ////$("body").mLoading('hide');
        }
        else if (sort_type == "6") {
            var Level0_data = this.report_AmountStudent.province;
            $.each(Level0_data, function (Level0_Index, Level0_Values) {

                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row",
                    data: [
                        { text: Level0_Index + 1, style: RowsCSS_1 },
                        { text: Level0_Values.ProvinceName, style: RowsCSS_1 },
                        { text: Level0_Values.male, style: RowsCSS_1 },
                        { text: Level0_Values.female, style: RowsCSS_1 },
                        { text: Level0_Values.sStuAmount, style: RowsCSS_1 }
                    ]
                }));

            });

            if (export_file === true) {
                $("#" + table_name).append("<tfoot>");
                var Colspan = 5;
                $("#" + table_name + " thead").prepend(RenderRows(
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
                            text: this.report_AmountStudent.HeaderText,
                            colspan: Colspan,
                            style: "font-size:16px;"
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
            ////$("body").mLoading('hide');
        }
        else if (sort_type == "7") {
            var Level0_data = this.report_AmountStudent.province;
            $.each(Level0_data, function (Level0_Index, Level0_Values) {
                var Level1_data = Level0_Values.amphur;

                $.each(Level1_data, function (Level1_Index, Level1_Values) {

                    if (Level1_Index == 0) {
                        $("#" + table_name + " tbody").append(RenderRows({
                            rowtype: "row",
                            data: [
                                { text: Level0_Values.ProvinceName, style: RowsCSS_1, rowspan: Level1_data.length },
                                { text: Level1_Values.AmphurName, style: RowsCSS_1 },
                                { text: Level1_Values.male, style: RowsCSS_1 },
                                { text: Level1_Values.female, style: RowsCSS_1 },
                                { text: Level1_Values.sStuAmount, style: RowsCSS_1 }
                            ]
                        }));
                    } else {
                        $("#" + table_name + " tbody").append(RenderRows({
                            rowtype: "row",
                            data: [
                                { text: Level1_Values.AmphurName, style: RowsCSS_1 },
                                { text: Level1_Values.male, style: RowsCSS_1 },
                                { text: Level1_Values.female, style: RowsCSS_1 },
                                { text: Level1_Values.sStuAmount, style: RowsCSS_1 }
                            ]
                        }));
                    }
                });
            });

            if (export_file === true) {
                $("#" + table_name).append("<tfoot>");
                var Colspan = 5;
                $("#" + table_name + " thead").prepend(RenderRows(
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
                            text: this.report_AmountStudent.HeaderText,
                            colspan: Colspan,
                            style: "font-size:16px;"
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
            //$("body").mLoading('hide');
        }
        else if (sort_type == "8") {

            var Level0_data = this.report_AmountStudent.province;

            $.each(Level0_data, function (Level0_Index, Level0_Values) {
                var Level1_data = Level0_Values.amphur;

                $.each(Level1_data, function (Level1_Index, Level1_Values) {
                    var Level2_data = Level1_Values.district;

                    $.each(Level2_data, function (Level2_Index, Level2_Values) {

                        var TotalStudent = (Level2_Values.male + Level2_Values.female);

                        if (Level2_Index == 0) {
                            $("#" + table_name + " tbody").append(RenderRows({
                                rowtype: "row",
                                data: [
                                    { text: Level0_Values.ProvinceName, style: RowsCSS_1, rowspan: Level2_data.length },
                                    { text: Level1_Values.AmphurName, style: RowsCSS_1, rowspan: Level2_data.length },

                                    { text: Level2_Values.DistrictName, style: RowsCSS_1 },
                                    { text: Level2_Values.male, style: RowsCSS_1 },
                                    { text: Level2_Values.female, style: RowsCSS_1 },
                                    { text: TotalStudent, style: RowsCSS_1 }
                                ]
                            }));
                        }
                        else {
                            $("#" + table_name + " tbody").append(RenderRows({
                                rowtype: "row",
                                data: [
                                    { text: Level2_Values.DistrictName, style: RowsCSS_1 },
                                    { text: Level2_Values.male, style: RowsCSS_1 },
                                    { text: Level2_Values.female, style: RowsCSS_1 },
                                    { text: TotalStudent, style: RowsCSS_1 }
                                ]
                            }));
                        }
                    });
                });
            });

            if (export_file === true) {
                $("#" + table_name).append("<tfoot>");
                var Colspan = 6;
                $("#" + table_name + " thead").prepend(RenderRows(
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
                            text: this.report_AmountStudent.HeaderText,
                            colspan: Colspan,
                            style: "font-size:16px;"
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

            //$("body").mLoading('hide');
        }
        else if (sort_type == "9") {

            var Level0_data = this.report_AmountStudent.Level0s;

            var SumMale = 0, SumFemale = 0;
            var SumStudent = 0;
            $.each(Level0_data, function (Level0_Index, Level0_Values) {

                SumMale += Level0_Values.male;
                SumFemale += Level0_Values.female;
                SumStudent = (SumMale + SumFemale);

                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row",
                    data: [
                        { text: Level0_Index + 1, style: RowsCSS_1 },
                        { text: Level0_Values.Race, style: RowsCSS_1 },
                        { text: Level0_Values.male, style: RowsCSS_1 },
                        { text: Level0_Values.female, style: RowsCSS_1 },
                        { text: Level0_Values.sStuAmount, style: RowsCSS_1 }
                    ]
                }));
            });

            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: "รวม", style: RowsCSS_2, colspan: 2 },
                    { text: SumMale, style: RowsCSS_3 },
                    { text: SumFemale, style: RowsCSS_3 },
                    { text: SumStudent, style: RowsCSS_3 }
                ]
            }));

            if (export_file === true) {
                $("#" + table_name).append("<tfoot>");
                var Colspan = 5;
                $("#" + table_name + " thead").prepend(RenderRows(
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
                            text: this.report_AmountStudent.HeaderText,
                            colspan: Colspan,
                            style: "font-size:16px;"
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

            //$("body").mLoading('hide');
        }
        else if (sort_type == "10") {
            var Level0_data = this.report_AmountStudent.Level0s;
            $.each(Level0_data, function (Level0_Index, Level0_Values) {
                var Level1_data = Level0_Values.Level1s;
                $.each(Level1_data, function (Level1_Index, Level1_Values) {

                    if (Level1_Index == 0) {
                        $("#" + table_name + " tbody").append(RenderRows({
                            rowtype: "row",
                            data: [
                                { text: Level0_Values.ClassName, style: RowsCSS_1, rowspan: Level1_data.length },
                                { text: Level1_Values.Index, style: RowsCSS_1 },
                                { text: Level1_Values.StudentId, style: RowsCSS_1 },
                                { text: Level1_Values.FullName, style: RowsCSS_1 },
                            ]
                        }));
                    } else {
                        $("#" + table_name + " tbody").append(RenderRows({
                            rowtype: "row",
                            data: [
                                { text: Level1_Values.Index, style: RowsCSS_1 },
                                { text: Level1_Values.StudentId, style: RowsCSS_1 },
                                { text: Level1_Values.FullName, style: RowsCSS_1 }
                            ]
                        }));
                    }
                });
            });

          

            //$("body").mLoading('hide');
        }
        else if (sort_type == "11") {
            var data = this.report_AmountStudent.data;
            $.each(data, function (i, r) {

                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row",
                    data: [
                        { text: r.ClassName, style: RowsCSS_1, },
                        { text: r.RoomCount, style: RowsCSS_1, },
                        { text: r.Male, style: RowsCSS_1 },
                        { text: r.Female, style: RowsCSS_1 },
                        { text: r.All, style: RowsCSS_1 },
                    ]
                }));                   
               
            });

            if (export_file === true) {
                $("#" + table_name).append("<tfoot>");
                var Colspan = 5;
                $("#" + table_name + " thead").prepend(RenderRows(
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
                            text: this.report_AmountStudent.HeaderText,
                            colspan: Colspan,
                            style: "font-size:16px;"
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

            //$("body").mLoading('hide');
        }
        else if (sort_type == "12") {
            var data = this.report_AmountStudent.data;
            $.each(data, function (i, r) {

                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row",
                    data: [
                        { text: r.RoomName, style: RowsCSS_1, },
                        { text: r.Male, style: RowsCSS_1 },
                        { text: r.Female, style: RowsCSS_1 },
                        { text: r.All, style: RowsCSS_1 },
                        { text: r.Teacher, style: RowsCSS_1 },
                    ]
                }));

            });

            if (export_file === true) {
                $("#" + table_name).append("<tfoot>");
                var Colspan = 5;
                $("#" + table_name + " thead").prepend(RenderRows(
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
                            text: this.report_AmountStudent.HeaderText,
                            colspan: Colspan,
                            style: "font-size:16px;"
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
            //$("body").mLoading('hide');
        }
        else if (sort_type == "13") {
            var data = this.report_AmountStudent.data;
            $.each(data, function (i, r) {

                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row",
                    data: [
                        { text: r.Room, style: RowsCSS_1, },
                        { text: r.Plan, style: RowsCSS_1, },
                        { text: r.Male, style: RowsCSS_1 },
                        { text: r.Female, style: RowsCSS_1 },
                        { text: r.All, style: RowsCSS_1 },
                       
                    ]
                }));

            });

            if (export_file === true) {
                $("#" + table_name).append("<tfoot>");
                var Colspan = 5;
                $("#" + table_name + " thead").prepend(RenderRows(
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
                            text: this.report_AmountStudent.HeaderText,
                            colspan: Colspan,
                            style: "font-size:16px;"
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
            //$("body").mLoading('hide');
        }
        else {
            //Addcondition
        }

    }


    this.export_excel = function () {
     /*   $("body").mLoading();*/
        var dt = new Date();

        var file_name = this.report_AmountStudent.HeaderText + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getDate() + dt.getMinutes() + dt.getSeconds() + ' .xls';
        this.RenderHtml_AmountStudent('table_exports', true);

        var sort_type = $("#sort_type").val();

        if (sort_type == "10") {
            var json = JSON.stringify({ search: searchData });
            var xhr = new XMLHttpRequest();

            xhr.open("POST", "/Report/AmountStudent_Report.aspx/export10_data", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                //$("body").mLoading('hide');
            };
            xhr.send(json);
        } else {
            var param = {
                "filename": "filename02",
                "tabledata": $("#export_excel").html()
            };

            $.post("/export_excel.aspx", param, function (data) {
                downloadFile(file_name, 'data:application/xml;charset=utf-8;base64,', data)
            });
        }
    };


    this.export_pdf = function () {
       /* $("body").mLoading();*/
        var filemaxnaja = "รายงาน"
        var json = JSON.stringify({ search: searchData });
        var xhr = new XMLHttpRequest();
        xhr.open("POST", "/Report/AmountStudent_Report.aspx/pdf10_data", true);
        xhr.responseType = 'blob';
        xhr.onreadystatechange = function () {
            var blob = xhr.response;
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = filemaxnaja + ".pdf";

            document.body.appendChild(link);

            link.click();
            //$("body").mLoading('hide');
        };

        xhr.send(json);
    }




});