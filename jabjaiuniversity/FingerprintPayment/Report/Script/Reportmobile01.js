//rgb(13, 199, 66) dc742
//rgb(3, 147, 199) 393c7
//rgb(228, 146, 47) e4922f
//rgb(255, 0, 254) ff0fe
//rgb(152, 80, 224) 9850e0
//rgb(255, 57, 66) ff3942
var reports_01 = (function () {
    this.reports_data = [];
    var HeaderCSS = "font-weight: bold;text-align: center;text-align: center !important;";
    var RowsCSS = "text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_1 = "text-align: right !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_2 = "text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_3 = "text-align: left !important; vertical-align: middle !important;padding:0px;";
    var color_0 = "background-color:#5db75d;", color_1 = "background-color:#347ab8;", color_2 = "background-color:#f0ad4e;", color_3 = "background-color:#c082ff;";
    var color_4 = "background-color:#9850e0;", color_5 = "background-color:#fb5955;", color_6 = "background-color:#fb5955;", color_sum = "background-color:#eee;";

    this.RenderHtml01 = function (table_name, export_file) {
        var dt = new Date();
        $("#" + table_name).html("");
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
        var thead_01 = [];
        var thead_02 = [];
        var thead_03 = [];
        var Months = this.reports_data;
        var Colspan = 23;

        if (export_file) {
            //Add Header Reports
            $("#" + table_name + " thead").append(
                RenderRows(
                    {
                        rowtype: "header",
                        data: [{ text: $("input[id*=hdfschoolname]").val(), style: "text-align: center;font-size:20px;border-width:0px;", colspan: Colspan }]
                    }, {
                    rowtype: "header",
                    data: [{ text: "รายงานสรุปสถิติการมาโรงเรียน ประจำวันที่ " + (ReportDate), style: "text-align: center;font-size:18px;border-width:0px;", colspan: Colspan }]
                }, {
                    rowtype: "header",
                    data: [{ text: "พิมพ์วันที่ :&nbsp;" + dt.toLocaleDateString(), style: "text-align: right;font-size:13px;border-width:0px;", colspan: Colspan }]
                }, {
                    rowtype: "header",
                    data: [{ text: "เวลา :&nbsp;" + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds(), style: "text-align: right;font-size:13px;border-width:0px;", colspan: Colspan }]
                }, {
                    rowtype: "header",
                    data: [
                        { text: "ปีการศึกษา :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: schoolData.Year, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                        { text: "เทอม :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: schoolData.Term, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                    ]
                }, {
                    rowtype: "header",
                    data: [
                        { text: "ระดับชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: $("select[id*=ddlsublevel] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                        { text: "ชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: $("select[id*=ddlSubLV2] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                    ]
                }, {
                    rowtype: "header",
                    data: [{ text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: Colspan }]
                },
                ));
            //Add Footer Reports
        }

        //Add Rows Table
        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header",
                    data: [
                        { text: "ลำดับ", style: HeaderCSS, rowspan: 2 }, { text: "วันที่", style: HeaderCSS, rowspan: 2 },
                        { text: "จำนวนนักเรียนทั้งหมด", style: HeaderCSS, colspan: 3 }, { text: "มาเรียน", style: HeaderCSS, colspan: 3 },
                        { text: "กิจกรรม", style: HeaderCSS, colspan: 3 }, { text: "สาย", style: HeaderCSS, colspan: 3 },
                        { text: "ลาป่วย", style: HeaderCSS, colspan: 3 }, { text: "ลากิจ", style: HeaderCSS, colspan: 3 },
                        { text: "ขาด", style: HeaderCSS, colspan: 3 }]
                },
                {
                    rowtype: "header",
                    data: [
                        { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS },
                        { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS },
                        { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS },
                        { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS },
                        { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS },
                        { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS },
                        { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS }
                    ]
                }
            ));

        var rows = 1;
        var _f_0 = 0, _f_1 = 0, _f_2 = 0, _f_3 = 0, _f_4 = 0, _f_5 = 0, _f_6 = 0;
        var _m_0 = 0, _m_1 = 0, _m_2 = 0, _m_3 = 0, _m_4 = 0, _m_5 = 0, _m_6 = 0;
        var sum_f = 0, sum_m = 0, _m = 0, _f = 0;
        var sum_f_0 = 0, sum_f_1 = 0, sum_f_2 = 0, sum_f_3 = 0, sum_f_4 = 0, sum_f_5 = 0, sum_f_6 = 0;
        var sum_m_0 = 0, sum_m_1 = 0, sum_m_2 = 0, sum_m_3 = 0, sum_m_4 = 0, sum_m_5 = 0, sum_m_6 = 0;

        $.each(this.reports_data, function (index, values) {
            //Man
            sum_m_0 += _m_0 = values.male_status_0;
            sum_m_1 += _m_1 = values.male_status_1;
            sum_m_2 += _m_2 = values.male_status_2;
            sum_m_3 += _m_3 = values.male_status_3;
            sum_m_4 += _m_4 = values.male_status_4;
            sum_m_5 += _m_5 = values.male_status_5;
            sum_m_6 += _m_6 = values.male_status_6;
            //Woman
            sum_f_0 += _f_0 = values.female_status_0;
            sum_f_1 += _f_1 = values.female_status_1;
            sum_f_2 += _f_2 = values.female_status_2;
            sum_f_3 += _f_3 = values.female_status_3;
            sum_f_4 += _f_4 = values.female_status_4;
            sum_f_5 += _f_5 = values.female_status_5;
            sum_f_6 += _f_6 = values.female_status_6;

            sum_f += _f = _f_0 + _f_1 + _f_2 + _f_3 + _f_4 + _f_5 + _f_6;
            sum_m += _m = _m_0 + _m_1 + _m_2 + _m_3 + _m_4 + _m_5 + _m_6;

            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: (rows++), style: RowsCSS },
                    { text: "<span style='color:royalblue;cursor: pointer;' onclick='Reports02(" + '"' + values.day + '"' + ")'>" + values.dayTH, style: RowsCSS },
                    { text: _m, style: RowsCSS }, { text: _f, style: RowsCSS }, { text: (_m + _f), style: RowsCSS + color_sum },
                    { text: _m_0, style: RowsCSS }, { text: _f_0, style: RowsCSS }, { text: _m_0 + _f_0, style: RowsCSS + color_0 },
                    { text: _m_1, style: RowsCSS }, { text: _f_1, style: RowsCSS }, { text: _m_1 + _f_1, style: RowsCSS + color_1 },
                    { text: _m_2, style: RowsCSS }, { text: _f_2, style: RowsCSS }, { text: _m_2 + _f_2, style: RowsCSS + color_2 },
                    { text: _m_3, style: RowsCSS }, { text: _f_3, style: RowsCSS }, { text: _m_3 + _f_3, style: RowsCSS + color_3 },
                    { text: _m_4, style: RowsCSS }, { text: _f_4, style: RowsCSS }, { text: _m_4 + _f_4, style: RowsCSS + color_4 },
                    { text: _m_5, style: RowsCSS }, { text: _f_5, style: RowsCSS }, { text: _m_5 + _f_5, style: RowsCSS + color_5 }
                ]
            }
            ));
        });
        //Rows Sum/Percent
        $("#" + table_name + " tbody").append(RenderRows(
            //{
            //    rowtype: "row",
            //    data: [
            //        { text: "รวม", colspan: 2, style: RowsCSS_1 },
            //        { text: sum_m, style: RowsCSS }, { text: sum_f, style: RowsCSS }, { text: (sum_m + sum_f), style: RowsCSS },
            //        { text: sum_m_0, style: RowsCSS }, { text: sum_f_0, style: RowsCSS }, { text: (sum_m_0 + sum_f_0), style: RowsCSS },
            //        { text: sum_m_1, style: RowsCSS }, { text: sum_f_1, style: RowsCSS }, { text: (sum_m_1 + sum_f_1), style: RowsCSS },
            //        { text: sum_m_2, style: RowsCSS }, { text: sum_f_2, style: RowsCSS }, { text: (sum_m_2 + sum_f_2), style: RowsCSS },
            //        { text: sum_m_3, style: RowsCSS }, { text: sum_f_3, style: RowsCSS }, { text: (sum_m_3 + sum_f_3), style: RowsCSS },
            //        { text: sum_m_4, style: RowsCSS }, { text: sum_f_4, style: RowsCSS }, { text: (sum_m_4 + sum_f_4), style: RowsCSS },
            //        { text: sum_m_5, style: RowsCSS }, { text: sum_f_5, style: RowsCSS }, { text: (sum_m_5 + sum_f_5), style: RowsCSS }
            //    ]
            //},
            {
                rowtype: "row",
                data: [
                    { text: "คิดเป็นร้อยละ", colspan: 2, style: RowsCSS_1 + color_sum },
                    { text: percent((sum_f + sum_m), sum_m), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m + sum_f)), style: RowsCSS + color_sum },
                    { text: percent((sum_f + sum_m), sum_m_0), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_0), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_0 + sum_f_0)), style: RowsCSS + color_sum },
                    { text: percent((sum_f + sum_m), sum_m_1), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_1), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_1 + sum_f_1)), style: RowsCSS + color_sum },
                    { text: percent((sum_f + sum_m), sum_m_2), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_2), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_2 + sum_f_2)), style: RowsCSS + color_sum },
                    { text: percent((sum_f + sum_m), sum_m_3), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_3), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_3 + sum_f_3)), style: RowsCSS + color_sum },
                    { text: percent((sum_f + sum_m), sum_m_4), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_4), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_4 + sum_f_4)), style: RowsCSS + color_sum },
                    { text: percent((sum_f + sum_m), sum_m_5), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_5), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_5 + sum_f_5)), style: RowsCSS + color_sum }
                ]
            }
        ));
        //$("body").mLoading('hide');
    };

    this.RenderHtml02_01 = function (table_name, export_file) {
        var dt = new Date();
        $("#" + table_name).html("");
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
        var thead_01 = [];
        var thead_02 = [];
        var thead_03 = [];
        var Months = this.reports_data;

        var Colspan = 23;
        if (export_file) {
            //Add Header Reports
            $("#" + table_name + " thead").append(
                RenderRows(
                    {
                        rowtype: "header",
                        data: [{ text: $("input[id*=hdfschoolname]").val(), style: "text-align: center;font-size:20px;border-width:0px;", colspan: Colspan }]
                    }, {
                    rowtype: "header",
                    data: [{ text: "รายงานสรุปสถิติการมาโรงเรียน ประจำวันที่ " + (ReportDate), style: "text-align: center;font-size:18px;border-width:0px;", colspan: Colspan }]
                }, {
                    rowtype: "header",
                    data: [{ text: $.format.date(new Date(), "พิมพ์วันที่ : dd MMM yyyy เวลา : HH:mm:ss น."), style: "text-align: right;font-size:13px;border-width:0px;", colspan: Colspan }]
                }, {
                    rowtype: "header",
                    data: [
                        { text: "ปีการศึกษา :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: schoolData.Year, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                        { text: "เทอม :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: schoolData.Term, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                    ]
                }, {
                    rowtype: "header",
                    data: [
                        { text: "ระดับชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: $("select[id*=ddlsublevel] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                        { text: "ชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: $("select[id*=ddlSubLV2] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                    ]
                }
                ));
            //Add Footer Reports
            $("#" + table_name + " tfoot").append(
                RenderRows(
                    {
                        rowtype: "foot",
                        data: [{ text: "", style: "", colspan: Colspan }]
                    }, {
                    rowtype: "foot",
                    data: [
                        { text: "ครูที่รับผิดชอบประจำวัน", style: "", colspan: 4 },
                        { text: "ลงชื่อ.......................................................", style: "text-align: right;", colspan: 7 },
                        { text: "", style: "", colspan: 2 },
                        { text: "ลงชื่อ.......................................................", style: "text-align: right;", colspan: 9 },
                        { text: "", style: "", colspan: 1 }
                    ]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 4 },
                        { text: "(.....................................................)", style: "text-align: right;", colspan: 7 },
                        { text: "", style: "", colspan: 2 },
                        { text: "(.....................................................)", style: "text-align: right;", colspan: 9 },
                        { text: "", style: "", colspan: 1 }
                    ]
                }, {
                    rowtype: "foot",
                    data: [{ text: "", style: "", colspan: Colspan }]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 4 },
                        { text: "รับทราบ  ลงชื่อ.......................................................", style: "text-align: center;", colspan: 18 },
                        { text: "", style: "", colspan: 1 }
                    ]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 4 },
                        { text: "(" + schoolData.SchoolHeadName + ")", style: "text-align: center;", colspan: 18 },
                        { text: "", style: "", colspan: 1 }
                    ]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 4 },
                        { text: "ผู้อำนวยการ" + $("input[id*=hdfschoolname]").val(), style: "text-align: center;", colspan: 18 },
                        { text: "", style: "", colspan: 1 }
                    ]
                }
                ));
        }

        //Add Rows Table
        var export_css = "width: 38px;";
        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header",
                    data: [
                        { text: "ลำดับ", style: HeaderCSS + (export_file ? export_css : ""), rowspan: 2 }, { text: "ชั้นเรียน", style: HeaderCSS + (export_file ? "width: 80px;" : ""), rowspan: 2 },
                        { text: "จำนวนนักเรียนทั้งหมด", style: HeaderCSS, colspan: 3 }, { text: "มาเรียน", style: HeaderCSS, colspan: 3 },
                        { text: "กิจกรรม", style: HeaderCSS, colspan: 3 }, { text: "สาย", style: HeaderCSS, colspan: 3 },
                        { text: "ลาป่วย", style: HeaderCSS, colspan: 3 }, { text: "ลากิจ", style: HeaderCSS, colspan: 3 },
                        { text: "ขาด", style: HeaderCSS, colspan: 3 },
                        { text: "ไม่เช็คชื่อ", style: HeaderCSS, colspan: 3 },
                        { text: "หมายเหตุ", style: HeaderCSS, rowspan: 2 }
                    ]
                },
                {
                    rowtype: "header",
                    data: [
                        { text: "ชาย", style: HeaderCSS + (export_file ? export_css : "") }, { text: "หญิง", style: HeaderCSS + (export_file ? export_css : "") }, { text: "รวม", style: HeaderCSS + (export_file ? export_css : "") },
                        { text: "ชาย", style: HeaderCSS + (export_file ? export_css : "") }, { text: "หญิง", style: HeaderCSS + (export_file ? export_css : "") }, { text: "รวม", style: HeaderCSS + (export_file ? export_css : "") },
                        { text: "ชาย", style: HeaderCSS + (export_file ? export_css : "") }, { text: "หญิง", style: HeaderCSS + (export_file ? export_css : "") }, { text: "รวม", style: HeaderCSS + (export_file ? export_css : "") },
                        { text: "ชาย", style: HeaderCSS + (export_file ? export_css : "") }, { text: "หญิง", style: HeaderCSS + (export_file ? export_css : "") }, { text: "รวม", style: HeaderCSS + (export_file ? export_css : "") },
                        { text: "ชาย", style: HeaderCSS + (export_file ? export_css : "") }, { text: "หญิง", style: HeaderCSS + (export_file ? export_css : "") }, { text: "รวม", style: HeaderCSS + (export_file ? export_css : "") },
                        { text: "ชาย", style: HeaderCSS + (export_file ? export_css : "") }, { text: "หญิง", style: HeaderCSS + (export_file ? export_css : "") }, { text: "รวม", style: HeaderCSS + (export_file ? export_css : "") },
                        { text: "ชาย", style: HeaderCSS + (export_file ? export_css : "") }, { text: "หญิง", style: HeaderCSS + (export_file ? export_css : "") }, { text: "รวม", style: HeaderCSS + (export_file ? export_css : "") },
                        { text: "ชาย", style: HeaderCSS + (export_file ? export_css : "") }, { text: "หญิง", style: HeaderCSS + (export_file ? export_css : "") }, { text: "รวม", style: HeaderCSS + (export_file ? export_css : "") }]
                }
            ));

        var rows = 1;
        $.each(this.reports_data, function (index, values) {
            var day = values.dayscan;
            var Objlevel = values.level;

            var total_sum_f = 0, total_sum_m = 0, total_m = 0, total_f = 0;
            var total_sum_f_0 = 0, total_sum_f_1 = 0, total_sum_f_2 = 0, total_sum_f_3 = 0, total_sum_f_4 = 0, total_sum_f_5 = 0, total_sum_f_6 = 0;
            var total_sum_m_0 = 0, total_sum_m_1 = 0, total_sum_m_2 = 0, total_sum_m_3 = 0, total_sum_m_4 = 0, total_sum_m_5 = 0, total_sum_m_6 = 0;

            $.each(Objlevel, function (indexlevel, valueslevel) {
                var _f_s = new Array(7).fill(0), _m_s = new Array(7).fill(0);
                var sum_f = 0, sum_m = 0;
                var sum_f_s = new Array(7).fill(0), sum_m_s = new Array(7).fill(0);

                var levelname = valueslevel.levelname;
                var Objlevel2 = valueslevel.level2;
                var sumstudent4level = 0;
                var sumstatus_04level = 0, sumstatus_14level = 0, sumstatus_24level = 0;

                $.each(Objlevel2, function (indexlevel2, valueslevel2) {
                    var _m = 0, _f = 0;
                    for (var index_status = 0; index_status <= 6; index_status++) {
                        //Woman
                        //Sum Level
                        _f_s[index_status] = valueslevel2["female_status_" + index_status];
                        sum_f_s[index_status] += _f_s[index_status];

                        //Man
                        //Sum Level
                        _m_s[index_status] = valueslevel2["male_status_" + index_status];
                        sum_m_s[index_status] += _m_s[index_status];

                        _m += valueslevel2["male_status_" + index_status];
                        _f += valueslevel2["female_status_" + index_status];
                    }

                    //Sum Total
                    total_sum_m_0 += _m_s[0];
                    total_sum_m_1 += _m_s[1];
                    total_sum_m_2 += _m_s[2];
                    total_sum_m_3 += _m_s[3];
                    total_sum_m_4 += _m_s[4];
                    total_sum_m_5 += _m_s[5];
                    total_sum_m_6 += _m_s[6];

                    //Sum Total
                    total_sum_f_0 += _f_s[0];
                    total_sum_f_1 += _f_s[1];
                    total_sum_f_2 += _f_s[2];
                    total_sum_f_3 += _f_s[3];
                    total_sum_f_4 += _f_s[4];
                    total_sum_f_5 += _f_s[5];
                    total_sum_f_6 += _f_s[6];

                    sum_m += _m;
                    sum_f += _f;
                    total_sum_m += _m;
                    total_sum_f += _f;

                    var HtmlRow = "";

                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row",
                        data: [
                            { text: (rows++), style: RowsCSS }, { text: valueslevel2.level2name, style: RowsCSS },
                            { text: _m, style: RowsCSS }, { text: _f, style: RowsCSS }, { text: (_m + _f), style: RowsCSS + color_sum },
                            { text: _m_s[0], style: RowsCSS }, { text: _f_s[0], style: RowsCSS }, { text: (_m_s[0] + _f_s[0]), style: RowsCSS + (export_file ? color_sum : color_0) },
                            { text: _m_s[1], style: RowsCSS }, { text: _f_s[1], style: RowsCSS }, { text: (_m_s[1] + _f_s[1]), style: RowsCSS + (export_file ? color_sum : color_1) },
                            { text: _m_s[2], style: RowsCSS }, { text: _f_s[2], style: RowsCSS }, { text: (_m_s[2] + _f_s[2]), style: RowsCSS + (export_file ? color_sum : color_2) },
                            { text: _m_s[3], style: RowsCSS }, { text: _f_s[3], style: RowsCSS }, { text: (_m_s[3] + _f_s[3]), style: RowsCSS + (export_file ? color_sum : color_3) },
                            { text: _m_s[4], style: RowsCSS }, { text: _f_s[4], style: RowsCSS }, { text: (_m_s[4] + _f_s[4]), style: RowsCSS + (export_file ? color_sum : color_4) },
                            { text: _m_s[5], style: RowsCSS }, { text: _f_s[5], style: RowsCSS }, { text: (_m_s[5] + _f_s[5]), style: RowsCSS + (export_file ? color_sum : color_5) },
                            { text: _m_s[6], style: RowsCSS }, { text: _f_s[6], style: RowsCSS }, { text: (_m_s[6] + _f_s[6]), style: RowsCSS + (export_file ? color_sum : color_6) },
                            { text: (_m + _f === _m_s[6] + _f_s[6]) && (_m + _f !== 0) ? "ไม่ได้ทำการเช็คชื่อ" : "", style: RowsCSS }
                        ]
                    }));
                });

                //Rows Sum/Percent
                $("#" + table_name + " tbody").append(RenderRows(
                    {
                        rowtype: "row",
                        data: [
                            { text: "รวม", colspan: 2, style: RowsCSS_1 + color_sum },
                            { text: sum_m, style: RowsCSS + color_sum }, { text: sum_f, style: RowsCSS + color_sum }, { text: (sum_m + sum_f), style: RowsCSS + color_sum },
                            { text: sum_m_s[0], style: RowsCSS + color_sum }, { text: sum_f_s[0], style: RowsCSS + color_sum }, { text: (sum_m_s[0] + sum_f_s[0]), style: RowsCSS + color_sum },
                            { text: sum_m_s[1], style: RowsCSS + color_sum }, { text: sum_f_s[1], style: RowsCSS + color_sum }, { text: (sum_m_s[1] + sum_f_s[1]), style: RowsCSS + color_sum },
                            { text: sum_m_s[2], style: RowsCSS + color_sum }, { text: sum_f_s[2], style: RowsCSS + color_sum }, { text: (sum_m_s[2] + sum_f_s[2]), style: RowsCSS + color_sum },
                            { text: sum_m_s[3], style: RowsCSS + color_sum }, { text: sum_f_s[3], style: RowsCSS + color_sum }, { text: (sum_m_s[3] + sum_f_s[3]), style: RowsCSS + color_sum },
                            { text: sum_m_s[4], style: RowsCSS + color_sum }, { text: sum_f_s[4], style: RowsCSS + color_sum }, { text: (sum_m_s[4] + sum_f_s[4]), style: RowsCSS + color_sum },
                            { text: sum_m_s[5], style: RowsCSS + color_sum }, { text: sum_f_s[5], style: RowsCSS + color_sum }, { text: (sum_m_s[5] + sum_f_s[5]), style: RowsCSS + color_sum },
                            { text: sum_m_s[6], style: RowsCSS + color_sum }, { text: sum_f_s[6], style: RowsCSS + color_sum }, { text: (sum_m_s[6] + sum_f_s[6]), style: RowsCSS + color_sum },
                            { text: "", style: RowsCSS + color_sum }
                        ]
                    }, {
                    rowtype: "row",
                    data: [
                        { text: "คิดเป็นร้อยละ", colspan: 2, style: RowsCSS_1 + color_sum },
                        { text: percent((sum_f + sum_m), sum_m), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m + sum_f)), style: RowsCSS + color_sum },
                        { text: percent((sum_f + sum_m), sum_m_s[0]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_s[0]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_s[0] + sum_f_s[0])), style: RowsCSS + color_sum },
                        { text: percent((sum_f + sum_m), sum_m_s[1]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_s[1]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_s[1] + sum_f_s[1])), style: RowsCSS + color_sum },
                        { text: percent((sum_f + sum_m), sum_m_s[2]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_s[2]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_s[2] + sum_f_s[2])), style: RowsCSS + color_sum },
                        { text: percent((sum_f + sum_m), sum_m_s[3]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_s[3]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_s[3] + sum_f_s[3])), style: RowsCSS + color_sum },
                        { text: percent((sum_f + sum_m), sum_m_s[4]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_s[4]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_s[4] + sum_f_s[4])), style: RowsCSS + color_sum },
                        { text: percent((sum_f + sum_m), sum_m_s[5]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_s[5]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_s[5] + sum_f_s[5])), style: RowsCSS + color_sum },
                        { text: percent((sum_f + sum_m), sum_m_s[6]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_s[6]), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_s[6] + sum_f_s[6])), style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum }
                    ]
                }
                ));
            });

            $("#" + table_name + " tbody").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        { text: "รวมทั้งหมด", colspan: 2, style: RowsCSS_1 + color_sum },
                        { text: total_sum_m, style: RowsCSS + color_sum }, { text: total_sum_f, style: RowsCSS + color_sum }, { text: (total_sum_m + total_sum_f), style: RowsCSS + color_sum },
                        { text: total_sum_m_0, style: RowsCSS + color_sum }, { text: total_sum_f_0, style: RowsCSS + color_sum }, { text: total_sum_m_0 + total_sum_f_0, style: RowsCSS + color_sum },
                        { text: total_sum_m_1, style: RowsCSS + color_sum }, { text: total_sum_f_1, style: RowsCSS + color_sum }, { text: total_sum_m_1 + total_sum_f_1, style: RowsCSS + color_sum },
                        { text: total_sum_m_2, style: RowsCSS + color_sum }, { text: total_sum_f_2, style: RowsCSS + color_sum }, { text: total_sum_m_2 + total_sum_f_2, style: RowsCSS + color_sum },
                        { text: total_sum_m_3, style: RowsCSS + color_sum }, { text: total_sum_f_3, style: RowsCSS + color_sum }, { text: total_sum_m_3 + total_sum_f_3, style: RowsCSS + color_sum },
                        { text: total_sum_m_4, style: RowsCSS + color_sum }, { text: total_sum_f_4, style: RowsCSS + color_sum }, { text: total_sum_m_4 + total_sum_f_4, style: RowsCSS + color_sum },
                        { text: total_sum_m_5, style: RowsCSS + color_sum }, { text: total_sum_f_5, style: RowsCSS + color_sum }, { text: total_sum_m_5 + total_sum_f_5, style: RowsCSS + color_sum },
                        { text: total_sum_m_6, style: RowsCSS + color_sum }, { text: total_sum_f_6, style: RowsCSS + color_sum }, { text: total_sum_m_6 + total_sum_f_6, style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum }
                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        { text: "คิดเป็นร้อยละทั้งหมด", colspan: 2, style: RowsCSS_1 + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m + total_sum_f)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m_0), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f_0), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m_0 + total_sum_f_0)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m_1), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f_1), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m_1 + total_sum_f_1)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m_2), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f_2), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m_2 + total_sum_f_2)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m_3), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f_3), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m_3 + total_sum_f_3)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m_4), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f_4), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m_4 + total_sum_f_4)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m_5), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f_5), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m_5 + total_sum_f_5)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m_6), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f_6), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m_6 + total_sum_f_6)), style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum }
                    ]
                }
            ));
        });
        //$("body").mLoading('hide');
    };

    this.RenderHtml02_02 = function (table_name, export_file) {
        var dt = new Date();
        $("#" + table_name).html("");
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

        var thead_01 = [];
        var thead_02 = [];
        var thead_03 = [];
        var Colspan = 11;

        if (export_file) {
            //Add Header Reports
            $("#" + table_name + " thead").append(
                RenderRows(
                    {
                        rowtype: "header",
                        data: [{ text: $("input[id*=hdfschoolname]").val(), style: "text-align: center;font-size:20px;border-width:0px;", colspan: Colspan }]
                    }, {
                    rowtype: "header",
                    data: [{ text: "รายงานสรุปสถิติการมาโรงเรียนประจำวันที่ " + (ReportDate), style: "text-align: center;font-size:18px;border-width:0px;", colspan: Colspan }]
                }, {
                    rowtype: "header",
                    data: [
                        { text: "ปีการศึกษา :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: schoolData.Year, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                        { text: "เทอม :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: schoolData.Term, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                    ]
                }, {
                    rowtype: "header",
                    data: [
                        { text: "ระดับชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: $("select[id*=ddlsublevel] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                        { text: "ชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: $("select[id*=ddlSubLV2] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                    ]
                }, {
                    rowtype: "header",
                    data: [{ text: $.format.date(new Date(), "พิมพ์วันที่ : dd MMM yyyy เวลา : HH:mm:ss น."), style: "text-align: right;font-size:13px;border-width:0px;", colspan: Colspan }]
                }
                ));

            //Add Footer Reports
            $("#" + table_name + " tfoot").append(
                RenderRows(
                    {
                        rowtype: "foot",
                        data: [{ text: "", style: "", colspan: Colspan }]
                    }, {
                    rowtype: "foot",
                    data: [
                        { text: "ครูที่รับผิดชอบประจำวัน", colspan: 2, style: "text-align: center;" },
                        { text: "ลงชื่อ.......................................................", style: "text-align: right;", colspan: 4 },
                        { text: "ลงชื่อ.......................................................", style: "text-align: center;", colspan: 4 },
                        { text: "", style: "text-align: right;", colspan: 1 }
                    ]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 2 },
                        { text: "(.....................................................)", style: "text-align: right;", colspan: 4 },
                        { text: "(.....................................................)", style: "text-align: center;", colspan: 4 },
                        { text: "", style: "text-align: right;", colspan: 1 }
                    ]
                }, {
                    rowtype: "foot",
                    data: [{ text: "", style: "", colspan: Colspan }]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "รับทราบ  ลงชื่อ.......................................................", style: "text-align: center;", colspan: Colspan }
                    ]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "(" + schoolData.SchoolHeadName + ")", style: "text-align: center;", colspan: Colspan }
                    ]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "ผู้อำนวยการ" + $("input[id*=hdfschoolname]").val(), style: "text-align: center;", colspan: Colspan }
                    ]
                }
                ));
        }

        //Add Header Table
        var Reports_Data02 = this.reports_data;
        thead_01.push(
            { text: "ลำดับ", style: HeaderCSS + (export_file ? "width:60px;" : ""), rowspan: 2 },
            { text: "ชั้นเรียน", style: HeaderCSS + (export_file ? "width:200px;" : ""), rowspan: 2 },
            { text: "จำนวนนักเรียนทั้งหมด", style: HeaderCSS, colspan: 3 },
            { text: "มาเรียน", style: HeaderCSS, colspan: 3 },
            { text: "ไม่มาเรียน", style: HeaderCSS, colspan: 3 },
            { text: "หมายเหตุ", style: HeaderCSS, rowspan: 2 });

        thead_02.push(
            { text: "ชาย", style: HeaderCSS + (export_file ? "width:90px;" : "") }, { text: "หญิง", style: HeaderCSS + (export_file ? "width:90px;" : "") }, { text: "รวม", style: HeaderCSS + (export_file ? "width:90px;" : "") },
            { text: "ชาย", style: HeaderCSS + (export_file ? "width:90px;" : "") }, { text: "หญิง", style: HeaderCSS + (export_file ? "width:90px;" : "") }, { text: "รวม", style: HeaderCSS + (export_file ? "width:90px;" : "") },
            { text: "ชาย", style: HeaderCSS + (export_file ? "width:90px;" : "") }, { text: "หญิง", style: HeaderCSS + (export_file ? "width:90px;" : "") }, { text: "รวม", style: HeaderCSS + (export_file ? "width:90px;" : "") }
        );

        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header",
                    data: thead_01
                },
                {
                    rowtype: "header",
                    data: thead_02
                }));

        //Add Rows Table
        var rows = 1;
        $.each(Reports_Data02, function (index) {
            var day = Reports_Data02[index].dayscan;
            var Objlevel = Reports_Data02[index].level;
            var total_sum_f = 0, total_sum_m = 0, total_m = 0, total_f = 0;
            var total_sum_f_0 = 0, total_sum_f_1 = 0;
            var total_sum_m_0 = 0, total_sum_m_1 = 0;

            $.each(Objlevel, function (indexlevel) {
                var levelname = Objlevel[indexlevel].levelname;
                var Objlevel2 = Objlevel[indexlevel].level2;
                var sumstudent4level = 0;
                var sumstatus_04level = 0, sumstatus_14level = 0, sumstatus_24level = 0;

                var sum_m = 0, sum_f = 0, sum_m_0 = 0, sum_m_1 = 0, sum_f_0 = 0, sum_f_1 = 0;

                $.each(Objlevel2, function (indexlevel2, valuesLevel2) {
                    var status_0 = 0, status_1 = 0, status_2 = 0;
                    var _m = 0, _m_0 = 0, _m_1 = 0;
                    var _f = 0, _f_0 = 0, _f_1 = 0;

                    sum_m_0 += _m_0 = valuesLevel2.male_status_0 + valuesLevel2.male_status_1 + valuesLevel2.male_status_2;
                    sum_m_1 += _m_1 = valuesLevel2.male_status_3 + valuesLevel2.male_status_4 + valuesLevel2.male_status_5 + valuesLevel2.male_status_6;
                    total_sum_m_0 += _m_0;
                    total_sum_m_1 += _m_1;

                    sum_f_0 += _f_0 = valuesLevel2.female_status_0 + valuesLevel2.female_status_1 + valuesLevel2.female_status_2;
                    sum_f_1 += _f_1 = valuesLevel2.female_status_3 + valuesLevel2.female_status_4 + valuesLevel2.female_status_5 + valuesLevel2.female_status_6;
                    total_sum_f_0 += _f_0;
                    total_sum_f_1 += _f_1;

                    sum_m += _m = valuesLevel2.male_status_0 + valuesLevel2.male_status_1 + valuesLevel2.male_status_2 + valuesLevel2.male_status_3 + valuesLevel2.male_status_4 + valuesLevel2.male_status_5 + valuesLevel2.male_status_6;
                    sum_f += _f = valuesLevel2.female_status_0 + valuesLevel2.female_status_1 + valuesLevel2.female_status_2 + valuesLevel2.female_status_3 + valuesLevel2.female_status_4 + valuesLevel2.female_status_5 + valuesLevel2.female_status_6;

                    total_sum_m += _m;
                    total_sum_f += _f;

                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row",
                        data: [
                            { text: (rows++), style: RowsCSS }, { text: valuesLevel2.level2name, style: RowsCSS },
                            { text: _m, style: RowsCSS }, { text: _f, style: RowsCSS }, { text: (_m + _f), style: RowsCSS },
                            { text: _m_0, style: RowsCSS }, { text: _f_0, style: RowsCSS }, { text: (_m_0 + _f_0), style: RowsCSS },
                            { text: _m_1, style: RowsCSS }, { text: _f_1, style: RowsCSS }, { text: (_m_1 + _f_1), style: RowsCSS },
                            { text: (_m + _f) === (valuesLevel2.female_status_6 + valuesLevel2.male_status_6) && (_m + _f !== 0) ? "ไม่ได้ทำการเช็คชื่อ" : "", style: RowsCSS }
                        ]
                    }));
                });

                $("#" + table_name + " tbody").append(RenderRows(
                    {
                        rowtype: "row",
                        data: [
                            { text: "รวม", colspan: 2, style: RowsCSS_1 + color_sum },
                            { text: sum_m, style: RowsCSS + color_sum }, { text: sum_f, style: RowsCSS + color_sum }, { text: (sum_m + sum_f), style: RowsCSS + color_sum },
                            { text: sum_m_0, style: RowsCSS + color_sum }, { text: sum_f_0, style: RowsCSS + color_sum }, { text: (sum_m_0 + sum_f_0), style: RowsCSS + color_sum },
                            { text: sum_m_1, style: RowsCSS + color_sum }, { text: sum_f_1, style: RowsCSS + color_sum }, { text: (sum_m_1 + sum_f_1), style: RowsCSS + color_sum },
                            { text: "", style: RowsCSS_1 + color_sum }
                        ]
                    },
                    {
                        rowtype: "row",
                        data: [
                            { text: "คิดเป็นร้อยละ", colspan: 2, style: RowsCSS_1 + color_sum },
                            { text: percent((sum_f + sum_m), sum_m), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m + sum_f)), style: RowsCSS + color_sum },
                            { text: percent((sum_f + sum_m), sum_m_0), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_0), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_0 + sum_f_0)), style: RowsCSS + color_sum },
                            { text: percent((sum_f + sum_m), sum_m_1), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_1), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_1 + sum_f_1)), style: RowsCSS + color_sum },
                            { text: "", style: RowsCSS + color_sum }
                        ]
                    }
                ));
            });

            $("#" + table_name + " tbody").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        { text: "รวมทั้งหมด", colspan: 2, style: RowsCSS_1 + color_sum },
                        { text: total_sum_m, style: RowsCSS + color_sum }, { text: total_sum_f, style: RowsCSS + color_sum }, { text: (total_sum_m + total_sum_f), style: RowsCSS + color_sum },
                        { text: total_sum_m_0, style: RowsCSS + color_sum }, { text: total_sum_f_0, style: RowsCSS + color_sum }, { text: (total_sum_m_0 + total_sum_f_0), style: RowsCSS + color_sum },
                        { text: total_sum_m_1, style: RowsCSS + color_sum }, { text: total_sum_f_1, style: RowsCSS + color_sum }, { text: (total_sum_m_1 + total_sum_f_1), style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum }
                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        { text: "คิดเป็นร้อยละทั้งหมด", colspan: 2, style: RowsCSS_1 + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m + total_sum_f)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m_0), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f_0), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m_0 + total_sum_f_0)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m_1), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f_1), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m_1 + total_sum_f_1)), style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum }
                    ]
                }
            ));
        });

        //$("body").mLoading('hide');

        //Add Footer Table
    };

    this.RenderHtml02_03 = function (table_name, export_file) {
        var dt = new Date();
        $("#" + table_name).html("");
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS + "width:50px;";
            RowsCSS_1 += ExportCSS;
            RowsCSS_2 += ExportCSS + "width:100px;";
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS + "width:50px;", "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            RowsCSS_2 = RowsCSS.replace(ExportCSS + "width:100px;", "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        var thead_01 = [];
        var thead_02 = [];
        var thead_03 = [];
        var Colspan = 12;

        if (export_file) {
            //Add Header Reports
            $("#" + table_name + " thead").append(
                RenderRows(
                    {
                        rowtype: "header",
                        data: [{ text: $("input[id*=hdfschoolname]").val(), style: "text-align: center;font-size:20px;border-width:0px;", colspan: Colspan }]
                    }, {
                    rowtype: "header",
                    data: [{ text: "รายงานสรุปสถิติการมาโรงเรียนประจำวันที่ " + (ReportDate), style: "text-align: center;font-size:18px;border-width:0px;", colspan: Colspan }]
                }, {
                    rowtype: "header",
                    data: [
                        { text: "ปีการศึกษา :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: schoolData.Year, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                        { text: "เทอม :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: schoolData.Term, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                    ]
                }, {
                    rowtype: "header",
                    data: [
                        { text: "ระดับชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: $("select[id*=ddlsublevel] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                        { text: "ชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: $("select[id*=ddlSubLV2] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                    ]
                }, {
                    rowtype: "header",
                    data: [{ text: $.format.date(new Date(), "พิมพ์วันที่ : dd MMM yyyy เวลา : HH:mm:ss น."), style: "text-align: right;font-size:13px;border-width:0px;", colspan: Colspan }]
                }
                ));

            //Add Footer Reports
            $("#" + table_name + " tfoot").append(
                RenderRows(
                    {
                        rowtype: "foot",
                        data: [{ text: "", style: "", colspan: Colspan }]
                    }, {
                    rowtype: "foot",
                    data: [
                        { text: "ครูที่รับผิดชอบประจำวัน", style: "text-align: center;", colspan: 3 },
                        { text: "ลงชื่อ..................................................", style: "text-align: center;", colspan: 5 },
                        { text: "ลงชื่อ.......................................................", style: "text-align: center;", colspan: 4 }]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 3 },
                        { text: "(.....................................................)", style: "text-align: center;", colspan: 5 },
                        { text: "(.....................................................)", style: "text-align: center;", colspan: 4 }
                    ]
                }, {
                    rowtype: "foot",
                    data: [{ text: "", style: "", colspan: Colspan }]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "รับทราบ  ลงชื่อ.......................................................", style: "text-align: center;", colspan: Colspan }
                    ]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "(" + schoolData.SchoolHeadName + ")", style: "text-align: center;", colspan: Colspan }
                    ]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "ผู้อำนวยการ" + $("input[id*=hdfschoolname]").val(), style: "text-align: center;", colspan: Colspan }
                    ]
                }
                ));
        }

        //Add Header Table
        var Reports_Data02 = this.reports_data;
        thead_01.push(
            { text: "ลำดับ", style: HeaderCSS, rowspan: 2 },
            { text: "ชั้นเรียน", style: HeaderCSS, rowspan: 2 },
            { text: "จำนวนนักเรียนทั้งหมด", style: HeaderCSS, colspan: 3 },
            { text: "มาเรียน", style: HeaderCSS, colspan: 3 },
            { text: "ไม่มาเรียน", style: HeaderCSS, colspan: 3 },
            { text: "รายชื่อนักเรียนที่ไม่มาเรียน", style: HeaderCSS + "width:200px", rowspan: 2 },
            { text: "หมายเหตุ", style: HeaderCSS, rowspan: 2 });

        thead_02.push(
            { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS },
            { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS },
            { text: "ป่วย", style: HeaderCSS }, { text: "ลา", style: HeaderCSS }, { text: "ขาด", style: HeaderCSS }
        );

        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header",
                    data: thead_01
                },
                {
                    rowtype: "header",
                    data: thead_02,
                }));

        //Add Rows Table
        var rows = 1;
        $.each(Reports_Data02, function (index) {
            var day = Reports_Data02[index].dayscan;
            var Objlevel = Reports_Data02[index].level;
            var total_sum_f = 0, total_sum_m = 0, total_m = 0, total_f = 0;
            var total_sum_1 = 0, total_sum_2 = 0, total_sum_3 = 0;
            var total_sum_m_0 = 0, total_sum_f_0 = 0;

            $.each(Objlevel, function (indexlevel) {
                var levelname = Objlevel[indexlevel].levelname;
                var Objlevel2 = Objlevel[indexlevel].level2;
                var sumstudent4level = 0;
                var sumstatus_04level = 0, sumstatus_14level = 0, sumstatus_24level = 0;

                var sum_m = 0, sum_f = 0, sum_m_0 = 0, sum_f_0 = 0, sum_1 = 0, sum_2 = 0, sum_3 = 0;

                $.each(Objlevel2, function (indexlevel2, valuesLevel2) {
                    var status_0 = 0, status_1 = 0, status_2 = 0;
                    var _m = 0, _m_0 = 0, _m_1 = 0;
                    var _f = 0, _f_0 = 0, s_1 = 0, s_2 = 0, s_3 = 0;

                    //Sum Level
                    sum_m_0 += _m_0 = valuesLevel2.male_status_0 + valuesLevel2.male_status_1 + valuesLevel2.male_status_2;
                    sum_f_0 += _f_0 = valuesLevel2.female_status_0 + valuesLevel2.female_status_1 + valuesLevel2.female_status_2;
                    //Sum Total
                    total_sum_m_0 += _m_0;
                    total_sum_f_0 += _f_0;

                    sum_1 += s_1 = valuesLevel2.female_status_3 + valuesLevel2.male_status_3;
                    sum_2 += s_2 = valuesLevel2.female_status_4 + valuesLevel2.male_status_4;
                    sum_3 += s_3 = valuesLevel2.female_status_5 + valuesLevel2.male_status_5;
                    total_sum_1 += s_1;
                    total_sum_2 += s_2;
                    total_sum_3 += s_3;

                    sum_m += _m = valuesLevel2.male_status_0 + valuesLevel2.male_status_1 + valuesLevel2.male_status_2 + valuesLevel2.male_status_3 + valuesLevel2.male_status_4 + valuesLevel2.male_status_5 + valuesLevel2.male_status_6;
                    sum_f += _f = valuesLevel2.female_status_0 + valuesLevel2.female_status_1 + valuesLevel2.female_status_2 + valuesLevel2.female_status_3 + valuesLevel2.female_status_4 + valuesLevel2.female_status_5 + valuesLevel2.female_status_6;
                    total_sum_m += _m;
                    total_sum_f += _f;

                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row",
                        data: [
                            { text: (rows++), style: RowsCSS }, { text: valuesLevel2.level2name, style: RowsCSS_2 },
                            { text: _m, style: RowsCSS }, { text: _f, style: RowsCSS }, { text: (_m + _f), style: RowsCSS },
                            { text: _m_0, style: RowsCSS }, { text: _f_0, style: RowsCSS }, { text: (_m_0 + _f_0), style: RowsCSS },
                            { text: s_1, style: RowsCSS }, { text: s_2, style: RowsCSS }, { text: s_3, style: RowsCSS },
                            { text: valuesLevel2.note3, style: RowsCSS },
                            { text: (_m + _f) === (valuesLevel2.female_status_6 + valuesLevel2.male_status_6) && (_m + _f !== 0) ? "ไม่ได้ทำการเช็คชื่อ" : "", style: "font-size:20px;" + RowsCSS },
                        ]
                    }));
                });

                $("#" + table_name + " tbody").append(RenderRows(
                    {
                        rowtype: "row",
                        data: [
                            { text: "รวม", colspan: 2, style: RowsCSS_1 + color_sum },
                            { text: sum_m, style: RowsCSS + color_sum }, { text: sum_f, style: RowsCSS + color_sum }, { text: (sum_m + sum_f), style: RowsCSS + color_sum },
                            { text: sum_m_0, style: RowsCSS + color_sum }, { text: sum_f_0, style: RowsCSS + color_sum }, { text: (sum_m_0 + sum_f_0), style: RowsCSS + color_sum },
                            { text: sum_1, style: RowsCSS + color_sum }, { text: sum_2, style: RowsCSS + color_sum }, { text: sum_3, style: RowsCSS + color_sum },
                            { text: "", style: RowsCSS + color_sum, colspan: 2 }
                        ]
                    },
                    {
                        rowtype: "row",
                        data: [
                            { text: "คิดเป็นร้อยละ", colspan: 2, style: RowsCSS_1 + color_sum },
                            { text: percent((sum_f + sum_m), sum_m), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m + sum_f)), style: RowsCSS + color_sum },
                            { text: percent((sum_f + sum_m), sum_m_0), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_0), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_0 + sum_f_0)), style: RowsCSS + color_sum },
                            { text: percent((sum_f + sum_m), sum_1), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_2), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_3), style: RowsCSS + color_sum },
                            { text: "", style: RowsCSS + color_sum },
                            { text: "", style: RowsCSS + color_sum }
                        ]
                    }
                ));
            });

            $("#" + table_name + " tbody").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        { text: "รวมทั้งหมด", colspan: 2, style: RowsCSS_1 + color_sum },
                        { text: total_sum_m, style: RowsCSS + color_sum }, { text: total_sum_f, style: RowsCSS + color_sum }, { text: (total_sum_m + total_sum_f), style: RowsCSS + color_sum },
                        { text: total_sum_m_0, style: RowsCSS + color_sum }, { text: total_sum_f_0, style: RowsCSS + color_sum }, { text: (total_sum_m_0 + total_sum_f_0), style: RowsCSS + color_sum },
                        { text: total_sum_1, style: RowsCSS + color_sum }, { text: total_sum_2, style: RowsCSS + color_sum }, { text: total_sum_3, style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum }
                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        { text: "คิดเป็นร้อยละทั้งหมด", colspan: 2, style: RowsCSS_1 + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m + total_sum_f)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m_0), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f_0), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m_0 + total_sum_f_0)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_1), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_2), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_3), style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum }
                    ]
                }
            ));
        });

        //$("body").mLoading('hide');

        //Add Footer Table
    };

    this.RenderHtml02_04 = function (table_name, export_file) {
        var dt = new Date();
        $("#" + table_name).html("");
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS + "width:50px;";
            RowsCSS_1 += ExportCSS;
            RowsCSS_2 += ExportCSS + "width:100px;";
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS + "width:50px;", "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            RowsCSS_2 = RowsCSS.replace(ExportCSS + "width:100px;", "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        var thead_01 = [];
        var thead_02 = [];
        var thead_03 = [];
        var Colspan = 12;

        if (export_file) {
            //Add Header Reports
            $("#" + table_name + " thead").append(
                RenderRows(
                    {
                        rowtype: "header",
                        data: [{ text: $("input[id*=hdfschoolname]").val(), style: "text-align: center;font-size:20px;border-width:0px;", colspan: Colspan }]
                    }, {
                    rowtype: "header",
                        data: [{ text: "รายงานสรุปสถิติการมาโรงเรียนประจำวันที่ " + (ReportDate), style: "text-align: center;font-size:18px;border-width:0px;", colspan: Colspan }]
                }
                    //, {
                    //    rowtype: "header",
                    //    data: [{ text: "ประจำวันที่ " + DateReport(ReportDate), style: "text-align: right;font-size:13px;border-width:0px;", colspan: Colspan }]
                    //}, {
                    //    rowtype: "header",
                    //    data: [{ text: $.format.date(new Date(), "พิมพ์วันที่ : dd MMM yyyy"), style: "text-align: right;font-size:13px;border-width:0px;", colspan: Colspan }]
                    //}, {
                    //    rowtype: "header",
                    //    data: [{ text: $.format.date(new Date(), "เวลา : HH:mm:ss น."), style: "text-align: right;font-size:13px;border-width:0px;", colspan: Colspan }]
                    //}, {
                    //    rowtype: "header",
                    //    data: [
                    //        { text: "ปีการศึกษา :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                    //        { text: schoolData.Year, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                    //        { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                    //        { text: "เทอม :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                    //        { text: schoolData.Term, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 }
                    //    ]
                    //}, {
                    //    rowtype: "header",
                    //    data: [
                    //        { text: "ระดับชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                    //        { text: $("select[id*=ddlsublevel] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                    //        { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                    //        { text: "ชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                    //        { text: $("select[id*=ddlSubLV2] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 }
                    //    ]
                    //}, {
                    //    rowtype: "header",
                    //    data: [{ text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: Colspan }]
                    //},
                    , {
                        rowtype: "header",
                        data: [
                            { text: "ปีการศึกษา :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                            { text: schoolData.Year, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                            { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                            { text: "เทอม :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                            { text: schoolData.Term, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                        ]
                    }, {
                    rowtype: "header",
                    data: [
                        { text: "ระดับชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: $("select[id*=ddlsublevel] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                        { text: "ชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: $("select[id*=ddlSubLV2] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                    ]
                }, {
                    rowtype: "header",
                    data: [{ text: $.format.date(new Date(), "พิมพ์วันที่ : dd MMM yyyy เวลา : HH:mm:ss น."), style: "text-align: right;font-size:13px;border-width:0px;", colspan: Colspan }]
                }
                ));

            //Add Footer Reports      
            $("#" + table_name + " tfoot").append(
                RenderRows(
                    {
                        rowtype: "foot",
                        data: [{ text: "", style: "", colspan: Colspan }]
                    }, {
                    rowtype: "foot",
                    data: [
                        { text: "ครูที่รับผิดชอบประจำวัน", style: "", colspan: 4 },
                        { text: "ลงชื่อ.......................................................", style: "text-align: right;", colspan: 7 }]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 4 },
                        { text: "(.....................................................)", style: "text-align: right;", colspan: 7 }]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 4 },
                        { text: "", style: "text-align: right;", colspan: 7 }
                    ]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 4 },
                        { text: "ลงชื่อ.......................................................", style: "text-align: right;", colspan: 7 }]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 4 },
                        { text: "(.....................................................)", style: "text-align: right;", colspan: 7 }]
                }, {
                    rowtype: "foot",
                    data: [{ text: "", style: "", colspan: Colspan }]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 4 },
                        { text: "รับทราบ  ลงชื่อ.......................................................", style: "text-align: right;", colspan: 7 }
                    ]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 4 },
                        { text: "(" + schoolData.SchoolHeadName + ")", style: "text-align: right;", colspan: 7 }
                    ]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 4 },
                        { text: "ผู้อำนวยการ" + $("input[id*=hdfschoolname]").val(), style: "text-align: right;", colspan: 7 }
                    ]
                }
                ));
        }

        //Add Header Table
        var Reports_Data02 = this.reports_data;
        thead_01.push(
            { text: "ลำดับ", style: HeaderCSS, rowspan: 2 },
            { text: "ชั้นเรียน", style: HeaderCSS, rowspan: 2 },
            { text: "จำนวนนักเรียนทั้งหมด", style: HeaderCSS, colspan: 3 },
            { text: "มาเรียน", style: HeaderCSS, colspan: 3 },
            { text: "มาสาย", style: HeaderCSS, colspan: 3 },
            { text: "รายชื่อนักเรียนที่มาเรียนสาย", style: HeaderCSS + "width:200px", rowspan: 2 },
            { text: "ไม่มาเรียน", style: HeaderCSS, colspan: 3 },
            { text: "รายชื่อนักเรียนที่ไม่มาเรียน", style: HeaderCSS + "width:200px", rowspan: 2 },
            { text: "หมายเหตุ", style: HeaderCSS, rowspan: 2 }
        );

        thead_02.push(
            { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS },
            { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS },
            { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS },
            { text: "ป่วย", style: HeaderCSS }, { text: "ลา", style: HeaderCSS }, { text: "ขาด", style: HeaderCSS }

        );

        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header",
                    data: thead_01
                },
                {
                    rowtype: "header",
                    data: thead_02
                }));

        //Add Rows Table
        var rows = 1;
        $.each(Reports_Data02, function (index) {
            var day = Reports_Data02[index].dayscan;
            var Objlevel = Reports_Data02[index].level;
            var total_sum_f = 0, total_sum_m = 0, total_m = 0, total_f = 0;
            var total_sum_1 = 0, total_sum_2 = 0, total_sum_3 = 0;
            var total_sum_m_0 = 0, total_sum_f_0 = 0;
            var total_sum_m_1 = 0, total_sum_f_1 = 0;

            $.each(Objlevel, function (indexlevel) {
                var levelname = Objlevel[indexlevel].levelname;
                var Objlevel2 = Objlevel[indexlevel].level2;
                var sumstudent4level = 0;
                var sumstatus_04level = 0, sumstatus_14level = 0, sumstatus_24level = 0;

                var sum_m = 0, sum_f = 0, sum_m_0 = 0, sum_f_0 = 0, sum_1 = 0, sum_2 = 0, sum_3 = 0;
                var sum_m_1 = 0, sum_f_1 = 0;

                $.each(Objlevel2, function (indexlevel2, valuesLevel2) {
                    var status_0 = 0, status_1 = 0, status_2 = 0;
                    var _m = 0, _m_0 = 0, _m_1 = 0;
                    var _f = 0, _f_0 = 0, _f_1 = 0, s_1 = 0, s_2 = 0, s_3 = 0;

                    //Sum Level
                    sum_m_0 += _m_0 = valuesLevel2.male_status_0 + valuesLevel2.male_status_1;
                    sum_f_0 += _f_0 = valuesLevel2.female_status_0 + valuesLevel2.female_status_1;
                    //Sum Total
                    total_sum_m_0 += _m_0;
                    total_sum_f_0 += _f_0;

                    //Sum Level 
                    sum_m_1 += _m_1 = valuesLevel2.male_status_2;
                    sum_f_1 += _f_1 = valuesLevel2.female_status_2;


                    sum_1 += s_1 = valuesLevel2.female_status_3 + valuesLevel2.male_status_3;
                    sum_2 += s_2 = valuesLevel2.female_status_4 + valuesLevel2.male_status_4;
                    sum_3 += s_3 = valuesLevel2.female_status_5 + valuesLevel2.male_status_5;
                    total_sum_1 += s_1;
                    total_sum_2 += s_2;
                    total_sum_3 += s_3;

                    sum_m += _m = valuesLevel2.male_status_0 + valuesLevel2.male_status_1 + valuesLevel2.male_status_2 + valuesLevel2.male_status_3 + valuesLevel2.male_status_4 + valuesLevel2.male_status_5 + valuesLevel2.male_status_6;
                    sum_f += _f = valuesLevel2.female_status_0 + valuesLevel2.female_status_1 + valuesLevel2.female_status_2 + valuesLevel2.female_status_3 + valuesLevel2.female_status_4 + valuesLevel2.female_status_5 + valuesLevel2.female_status_6;
                    total_sum_m += _m;
                    total_sum_f += _f;

                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row",
                        data: [
                            { text: (rows++), style: RowsCSS }, { text: valuesLevel2.level2name, style: RowsCSS_2 },
                            { text: _m, style: RowsCSS }, { text: _f, style: RowsCSS }, { text: (_m + _f), style: RowsCSS },
                            { text: _m_0, style: RowsCSS }, { text: _f_0, style: RowsCSS }, { text: (_m_0 + _f_0), style: RowsCSS },
                            { text: _m_1, style: RowsCSS }, { text: _f_1, style: RowsCSS }, { text: (_m_1 + _f_1), style: RowsCSS },
                            { text: RenderStudentList(valuesLevel2.note2), style: "" + RowsCSS_3 },
                            { text: s_1, style: RowsCSS }, { text: s_2, style: RowsCSS }, { text: s_3, style: RowsCSS },
                            { text: RenderStudentList(valuesLevel2.note), style: "" + RowsCSS_3 },
                            { text: (_m + _f) === (valuesLevel2.female_status_6 + valuesLevel2.male_status_6) && (_m + _f !== 0) ? "ไม่ได้ทำการเช็คชื่อ" : "", style: RowsCSS }
                        ]
                    }));
                });

                //Sum Total
                total_sum_m_1 += sum_m_1;
                total_sum_f_1 += sum_f_1;

                $("#" + table_name + " tbody").append(RenderRows(
                    {
                        rowtype: "row",
                        data: [
                            { text: "รวม", colspan: 2, style: RowsCSS_1 + color_sum },
                            { text: sum_m, style: RowsCSS + color_sum }, { text: sum_f, style: RowsCSS + color_sum }, { text: (sum_m + sum_f), style: RowsCSS + color_sum },
                            { text: sum_m_0, style: RowsCSS + color_sum }, { text: sum_f_0, style: RowsCSS + color_sum }, { text: (sum_m_0 + sum_f_0), style: RowsCSS + color_sum },
                            { text: sum_m_1, style: RowsCSS + color_sum }, { text: sum_f_1, style: RowsCSS + color_sum }, { text: (sum_m_1 + sum_f_1), style: RowsCSS + color_sum },
                            { text: "", style: RowsCSS + color_sum, colspan: 1 },
                            { text: sum_1, style: RowsCSS + color_sum }, { text: sum_2, style: RowsCSS + color_sum }, { text: sum_3, style: RowsCSS + color_sum },
                            { text: "", style: RowsCSS + color_sum },
                            { text: "", style: RowsCSS + color_sum }
                        ]
                    },
                    {
                        rowtype: "row",
                        data: [
                            { text: "คิดเป็นร้อยละ", colspan: 2, style: RowsCSS_1 + color_sum },
                            { text: percent((sum_f + sum_m), sum_m), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m + sum_f)), style: RowsCSS + color_sum },
                            { text: percent((sum_f + sum_m), sum_m_0), style: RowsCSS + color_sum }, { text: percent(sum_f + sum_m, sum_f_0), style: RowsCSS + color_sum }, { text: percent(sum_f + sum_m, sum_m_0 + sum_f_0), style: RowsCSS + color_sum },
                            { text: percent((sum_f + sum_m), sum_m_1), style: RowsCSS + color_sum }, { text: percent(sum_f + sum_m, sum_f_1), style: RowsCSS + color_sum }, { text: percent(sum_f + sum_m, sum_m_1 + sum_f_1), style: RowsCSS + color_sum },
                            { text: "", style: RowsCSS + color_sum, colspan: 1 },
                            { text: percent((sum_f + sum_m), sum_1), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_2), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_3), style: RowsCSS + color_sum },
                            { text: "", style: RowsCSS + color_sum, colspan: 1 },
                            { text: "", style: RowsCSS + color_sum }
                        ]
                    }
                ));
            });

            $("#" + table_name + " tbody").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        { text: "รวมทั้งหมด", colspan: 2, style: RowsCSS_1 + color_sum },
                        { text: total_sum_m, style: RowsCSS + color_sum }, { text: total_sum_f, style: RowsCSS + color_sum }, { text: (total_sum_m + total_sum_f), style: RowsCSS + color_sum },
                        { text: total_sum_m_0, style: RowsCSS + color_sum }, { text: total_sum_f_0, style: RowsCSS + color_sum }, { text: total_sum_m_0 + total_sum_f_0, style: RowsCSS + color_sum },
                        { text: total_sum_m_1, style: RowsCSS + color_sum }, { text: total_sum_f_1, style: RowsCSS + color_sum }, { text: total_sum_m_1 + total_sum_f_1, style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum, colspan: 1 },
                        { text: total_sum_1, style: RowsCSS + color_sum }, { text: total_sum_2, style: RowsCSS + color_sum }, { text: total_sum_3, style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum, colspan: 1 },
                        { text: "", style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum }
                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        { text: "คิดเป็นร้อยละทั้งหมด", colspan: 2, style: RowsCSS_1 + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m + total_sum_f)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m_0), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f_0), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m_0 + total_sum_f_0)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m_1), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f_1), style: RowsCSS + color_sum }, { text: percent(total_sum_f + total_sum_m, (total_sum_m_1 + total_sum_f_1)), style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum, colspan: 1 },
                        { text: percent((total_sum_f + total_sum_m), total_sum_1), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_2), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_3), style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum, colspan: 1 },
                        { text: "", style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum }
                    ]
                }
            ));
        });

        //$("body").mLoading('hide');

        //Add Footer Table
    };

    this.RenderHtml02_05 = function (table_name, export_file) {
        var dt = new Date();
        $("#" + table_name).html("");
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS + "width:50px;";
            RowsCSS_1 += ExportCSS;
            RowsCSS_2 += ExportCSS + "width:100px;";
            RowsCSS_3 += ExportCSS + "width:50px;";
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS + "width:50px;", "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            RowsCSS_2 = RowsCSS_2.replace(ExportCSS + "width:100px;", "");
            RowsCSS_3 = RowsCSS_3.replace(ExportCSS + "width:50px;", "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        var thead_01 = [];
        var thead_02 = [];
        var thead_03 = [];
        var Colspan = 12;

        if (export_file) {
            //Add Header Reports
            $("#" + table_name + " thead").append(
                RenderRows(
                    {
                        rowtype: "header",
                        data: [{ text: $("input[id*=hdfschoolname]").val(), style: "text-align: center;font-size:20px;border-width:0px;", colspan: Colspan }]
                    }, {
                    rowtype: "header",
                    data: [{ text: "รายงานสรุปสถิติการมาโรงเรียนประจำวันที่ " + (ReportDate), style: "text-align: center;font-size:18px;border-width:0px;", colspan: Colspan }]
                }, {
                    rowtype: "header",
                    data: [
                        { text: "ปีการศึกษา :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: schoolData.Year, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                        { text: "เทอม :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: schoolData.Term, style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                    ]
                }, {
                    rowtype: "header",
                    data: [
                        { text: "ระดับชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: $("select[id*=ddlsublevel] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: "", style: "text-align: center;font-size:13px;border-width:0px;", colspan: (Colspan - 8) },
                        { text: "ชั้นเรียน :", style: "text-align: right;font-size:13px;border-width:0px;", colspan: 2 },
                        { text: $("select[id*=ddlSubLV2] option:selected").text(), style: "text-align: left;font-size:13px;border-width:0px;", colspan: 2 },
                    ]
                }, {
                    rowtype: "header",
                    data: [{ text: $.format.date(new Date(), "พิมพ์วันที่ : dd MMM yyyy เวลา : HH:mm:ss น."), style: "text-align: right;font-size:13px;border-width:0px;", colspan: Colspan }]
                }
                ));

            //Add Footer Reports
            $("#" + table_name + " tfoot").append(
                RenderRows(
                    {
                        rowtype: "foot",
                        data: [{ text: "", style: "", colspan: Colspan }]
                    }, {
                    rowtype: "foot",
                    data: [
                        { text: "ครูที่รับผิดชอบประจำวัน", style: "text-align: center;", colspan: 3 },
                        { text: "ลงชื่อ..................................................", style: "text-align: center;", colspan: 5 },
                        { text: "ลงชื่อ.......................................................", style: "text-align: center;", colspan: 4 }]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "", style: "", colspan: 3 },
                        { text: "(.....................................................)", style: "text-align: center;", colspan: 5 },
                        { text: "(.....................................................)", style: "text-align: center;", colspan: 4 }
                    ]
                }, {
                    rowtype: "foot",
                    data: [{ text: "", style: "", colspan: Colspan }]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "รับทราบ  ลงชื่อ.......................................................", style: "text-align: center;", colspan: Colspan }
                    ]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "(" + schoolData.SchoolHeadName + ")", style: "text-align: center;", colspan: Colspan }
                    ]
                }, {
                    rowtype: "foot",
                    data: [
                        { text: "ผู้อำนวยการ" + $("input[id*=hdfschoolname]").val(), style: "text-align: center;", colspan: Colspan }
                    ]
                }
                ));
        }

        //Add Header Table
        var Reports_Data02 = this.reports_data;
        thead_01.push(
            { text: "ลำดับ", style: HeaderCSS, rowspan: 2 },
            { text: "ชั้นเรียน", style: HeaderCSS, rowspan: 2 },
            { text: "จำนวนนักเรียนทั้งหมด", style: HeaderCSS, colspan: 3 },
            { text: "มาเรียน", style: HeaderCSS, colspan: 3 },
            { text: "ไม่มาเรียน", style: HeaderCSS, colspan: 3 },
            { text: "รายชื่อนักเรียนที่ไม่มาเรียน", style: HeaderCSS + "width:200px", rowspan: 2 },
            { text: "หมายเหตุ", style: HeaderCSS, rowspan: 2 });

        thead_02.push(
            { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS },
            { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS },
            { text: "ชาย", style: HeaderCSS }, { text: "หญิง", style: HeaderCSS }, { text: "รวม", style: HeaderCSS }
        );

        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header",
                    data: thead_01
                },
                {
                    rowtype: "header",
                    data: thead_02,
                }));

        //Add Rows Table
        var rows = 1;
        $.each(Reports_Data02, function (index) {
            var day = Reports_Data02[index].dayscan;
            var Objlevel = Reports_Data02[index].level;
            var total_sum_f = 0, total_sum_m = 0, total_m = 0, total_f = 0;
            var total_sum_1 = 0, total_sum_2 = 0, total_sum_3 = 0;
            var total_sum_m_0 = 0, total_sum_f_0 = 0;

            $.each(Objlevel, function (indexlevel) {
                var levelname = Objlevel[indexlevel].levelname;
                var Objlevel2 = Objlevel[indexlevel].level2;
                var sumstudent4level = 0;
                var sumstatus_04level = 0, sumstatus_14level = 0, sumstatus_24level = 0;

                var sum_m = 0, sum_f = 0, sum_m_0 = 0, sum_f_0 = 0, sum_1 = 0, sum_2 = 0, sum_3 = 0;

                $.each(Objlevel2, function (indexlevel2, valuesLevel2) {
                    var status_0 = 0, status_1 = 0, status_2 = 0;
                    var _m = 0, _m_0 = 0, _m_1 = 0;
                    var _f = 0, _f_0 = 0, s_1 = 0, s_2 = 0, s_3 = 0;

                    //Sum Level
                    sum_m_0 += _m_0 = valuesLevel2.male_status_0 + valuesLevel2.male_status_1 + valuesLevel2.male_status_2;
                    sum_f_0 += _f_0 = valuesLevel2.female_status_0 + valuesLevel2.female_status_1 + valuesLevel2.female_status_2;
                    //Sum Total
                    total_sum_m_0 += _m_0;
                    total_sum_f_0 += _f_0;

                    sum_1 += s_1 = valuesLevel2.male_status_3 + valuesLevel2.male_status_4 + valuesLevel2.male_status_5 + valuesLevel2.male_status_6;
                    sum_2 += s_2 = valuesLevel2.female_status_3 + valuesLevel2.female_status_4 + valuesLevel2.female_status_5 + valuesLevel2.female_status_6;
                    sum_3 += s_3 = valuesLevel2.male_status_3 + valuesLevel2.male_status_4 + valuesLevel2.male_status_5 + valuesLevel2.male_status_6 + valuesLevel2.female_status_3 + valuesLevel2.female_status_4 + valuesLevel2.female_status_5 + valuesLevel2.female_status_6;
                    total_sum_1 += s_1;
                    total_sum_2 += s_2;
                    total_sum_3 += s_3;

                    sum_m += _m = valuesLevel2.male_status_0 + valuesLevel2.male_status_1 + valuesLevel2.male_status_2 + valuesLevel2.male_status_3 + valuesLevel2.male_status_4 + valuesLevel2.male_status_5 + valuesLevel2.male_status_6;
                    sum_f += _f = valuesLevel2.female_status_0 + valuesLevel2.female_status_1 + valuesLevel2.female_status_2 + valuesLevel2.female_status_3 + valuesLevel2.female_status_4 + valuesLevel2.female_status_5 + valuesLevel2.female_status_6;
                    total_sum_m += _m;
                    total_sum_f += _f;

                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row",
                        data: [
                            { text: (rows++), style: RowsCSS }, { text: valuesLevel2.level2name, style: RowsCSS_2 },
                            { text: _m, style: RowsCSS }, { text: _f, style: RowsCSS }, { text: (_m + _f), style: RowsCSS },
                            { text: _m_0, style: RowsCSS }, { text: _f_0, style: RowsCSS }, { text: (_m_0 + _f_0), style: RowsCSS },
                            { text: s_1, style: RowsCSS }, { text: s_2, style: RowsCSS }, { text: s_3, style: RowsCSS },
                            { text: RenderStudentList(valuesLevel2.note), style: RowsCSS_3 },
                            { text: (_m + _f) === (valuesLevel2.female_status_6 + valuesLevel2.male_status_6) && (_m + _f !== 0) ? "ไม่ได้ทำการเช็คชื่อ" : "", style: "" + RowsCSS },
                        ]
                    }));
                });

                $("#" + table_name + " tbody").append(RenderRows(
                    {
                        rowtype: "row",
                        data: [
                            { text: "รวม", colspan: 2, style: RowsCSS_1 + color_sum },
                            { text: sum_m, style: RowsCSS + color_sum }, { text: sum_f, style: RowsCSS + color_sum }, { text: (sum_m + sum_f), style: RowsCSS + color_sum },
                            { text: sum_m_0, style: RowsCSS + color_sum }, { text: sum_f_0, style: RowsCSS + color_sum }, { text: (sum_m_0 + sum_f_0), style: RowsCSS + color_sum },
                            { text: sum_1, style: RowsCSS + color_sum }, { text: sum_2, style: RowsCSS + color_sum }, { text: sum_3, style: RowsCSS + color_sum },
                            { text: "", style: RowsCSS + color_sum, colspan: 2 }
                        ]
                    },
                    {
                        rowtype: "row",
                        data: [
                            { text: "คิดเป็นร้อยละ", colspan: 2, style: RowsCSS_1 + color_sum },
                            { text: percent((sum_f + sum_m), sum_m), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m + sum_f)), style: RowsCSS + color_sum },
                            { text: percent((sum_f + sum_m), sum_m_0), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_f_0), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), (sum_m_0 + sum_f_0)), style: RowsCSS + color_sum },
                            { text: percent((sum_f + sum_m), sum_1), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_2), style: RowsCSS + color_sum }, { text: percent((sum_f + sum_m), sum_3), style: RowsCSS + color_sum },
                            { text: "", style: RowsCSS + color_sum, colspan: 2 }
                        ]
                    }
                ));
            });

            $("#" + table_name + " tbody").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        { text: "รวมทั้งหมด 3", colspan: 2, style: RowsCSS_1 + color_sum },
                        { text: total_sum_m, style: RowsCSS + color_sum }, { text: total_sum_f, style: RowsCSS + color_sum }, { text: (total_sum_m + total_sum_f), style: RowsCSS + color_sum },
                        { text: total_sum_m_0, style: RowsCSS + color_sum }, { text: total_sum_f_0, style: RowsCSS + color_sum }, { text: (total_sum_m_0 + total_sum_f_0), style: RowsCSS + color_sum },
                        { text: total_sum_1, style: RowsCSS + color_sum }, { text: total_sum_2, style: RowsCSS + color_sum }, { text: total_sum_3, style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum, colspan: 2 }
                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        { text: "คิดเป็นร้อยละทั้งหมด 3", colspan: 2, style: RowsCSS_1 + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m + total_sum_f)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_m_0), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_f_0), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), (total_sum_m_0 + total_sum_f_0)), style: RowsCSS + color_sum },
                        { text: percent((total_sum_f + total_sum_m), total_sum_1), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_2), style: RowsCSS + color_sum }, { text: percent((total_sum_f + total_sum_m), total_sum_3), style: RowsCSS + color_sum },
                        { text: "", style: RowsCSS + color_sum, colspan: 2 },
                    ]
                }
            ));
        });

        // $("body").mLoading('hide');

        //Add Footer Table
    };

    var report_txt = '';

    this.export_excel = function () {
        var dt = new Date();
        var file_name = 'รายงานสรุปสถิติการมาโรงเรียน_ ' + $("#txtname").val() + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getMinutes() + dt.getSeconds() + '.xls';

        if ($(".w3-button").is(":visible")) {
            switch ($("#select_Report").val()) {
                case "0": Reports_01.RenderHtml02_01("table_exports", true); break;
                case "1": Reports_01.RenderHtml02_02("table_exports", true); break;
                case "2": Reports_01.RenderHtml02_03("table_exports", true); break;
                case "3": Reports_01.RenderHtml02_04("table_exports", true); break;
                case "4": Reports_01.RenderHtml02_05("table_exports", true); break;
            }
        } else {
            Reports_01.RenderHtml01("table_exports", true);
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

function DateReport(stringDate) {
    var str = stringDate.split('/');
    var date = new Date(str[2], str[1] - 1, parseInt(str[0]));
    return $.format.date(date, "dd MMM yyyy");
}

function RenderStudentList(_list) {
    let _text = "";
    if (_list !== "") {
        $.each(_list.split(','), function (e, s) {
            _text += (e + 1) + "." + s + "<br/>";
        });
    }
    return _text;
}

function ExportPDF_03() {
    let Search = {
        "term_id": YTF.GetTermID(),
        "level_id": $('select[id*=ddlsublevel]').val(),
        "sort_type": $("#sort_type").val(),
        "dStart": '',// (moment($("#txtstart").val(), 'DD/MM/YYYY').add(-543, 'years').format("MM/DD/YYYY"))//getDate($("#txtstart").val()),
        "dEnd": '',//(moment($("#txtstart").val(), 'DD/MM/YYYY').add(-543, 'years').format("MM/DD/YYYY"))//getDate($("#txtend").val())
    };

    if (Search.sort_type == "1") {
        Search.dStart = ($("#select_month").val() + "/1/" + new Date().getFullYear());
        Search.dEnd = Search.dStart;
    }
    else if (Search.sort_type == "3") {
        if ($("#txtstart3").val() != '') {
            Search.dStart = $("#txtstart3").data("DateTimePicker").date().format('MM/DD/YYYY');           
        }
        else {
            Search.dStart = moment().format('MM/DD/YYYY');
        }
        Search.dEnd = $("#txtend3").data("DateTimePicker").date().format('MM/DD/YYYY');          
    }
    else if (Search.sort_type == "0") {
        if ($("#txtstart0").val() != '') {
            Search.dStart = $("#txtstart0").data("DateTimePicker").date().format('MM/DD/YYYY');
        } else {
            Search.dStart = moment().format('MM/DD/YYYY');
        }
        Search.dEnd = Search.dStart;
    }
    else {
        Search.dStart = moment().format('DD/MM/YYYY');
        Search.dEnd = Search.dStart;
    }
    //.add(-543, 'years')
    //Search.dStart = moment(Search.dStart, 'DD/MM/YYYY').format('MM/DD/YYYY');
    //Search.dEnd = moment(Search.dEnd, 'DD/MM/YYYY').format('MM/DD/YYYY');

    //$("body").mLoading('show');
    var json = JSON.stringify(Search);
    var req = new XMLHttpRequest();
    req.open("POST", "/Report/Handles/Reports0103_pdfHandler.ashx?ReportType=" + $("#select_Report").val(), true);
    req.responseType = "blob";
    req.onreadystatechange = function () {
        if (req.readyState === 4 && req.status === 200) {
            // test for IE
            if (typeof window.navigator.msSaveBlob === 'function') {
                window.navigator.msSaveBlob(req.response, "PdfName-" + new Date().getTime() + ".pdf");
            } else {
                var blob = req.response;
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = 'รายงานสรุปสถิติการมาโรงเรียน ' + $("input[id*=hdfschoolname]").val() + ".pdf";

                // append the link to the document body

                document.body.appendChild(link);

                link.click();
                //$("body").mLoading('hide');
            }
        }
    };
    req.send(json);
}
