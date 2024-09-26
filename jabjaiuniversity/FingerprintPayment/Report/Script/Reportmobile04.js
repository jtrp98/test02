var reports_04 = function () {
    this.reports_data = [];
    this.RenderHtml = function (table_name, export_file) {
        $("#" + table_name).html("");
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS.replace(";min-width", ";width");
            HeaderCSS_1 += ExportCSS.replace(";min-width", ";width");
            HeaderCSS_1 += ExportCSS.replace(";min-width", ";width");
            HeaderCSS_2 += ExportCSS.replace(";min-width", ";width");
            RowsCSS += ExportCSS.replace(";min-width", ";width");
            RowsCSS_1 += ExportCSS.replace(";min-width", ";width");
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS).replace(";width", ";min-width");
            HeaderCSS_1 = HeaderCSS_1.replace(ExportCSS, "").replace(";width", ";min-width");
            HeaderCSS_2 = HeaderCSS_2.replace(ExportCSS, "").replace(";width", ";min-width");
            RowsCSS = RowsCSS.replace(ExportCSS, "").replace(";width", ";min-width");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "").replace(";width", ";min-width");
        }

        $("#" + table_name).append("<thead>");
        var thead_01 = [];
        var thead_02 = [];
        var thead_03 = [];
        var thead_04 = [];
        var Months = this.reports_data.headerReports;
        //Gen Header Table
        thead_01.push(
            {
                text: "ลำดับ", style: HeaderCSS, rowspan: 4
            }, {
            text: "รหัสนักเรียน", style: HeaderCSS, rowspan: 4
        }, {
            text: "ชื่อนักเรียน", style: HeaderCSS, rowspan: 4
        }, {
            text: "เดือน", style: HeaderCSS
        });
        thead_02.push({
            text: "สัปดาห์", style: HeaderCSS
        });
        thead_03.push({
            text: "วันที่", style: HeaderCSS
        });

        thead_04.push({
            text: "คาบที่", style: HeaderCSS
        });

        var Column_Index = 0;
        $.each(Months, function (Months_index, Months_values) {
            var Weeks = Months_values.weeks;
            var DaysLength = 0;
            $.each(Weeks, function (Weeks_index, Weeks_values) {
                var Days = Weeks_values.days;
                thead_02.push({
                    text: Weeks_values.Weeks_Id, style: HeaderCSS_2, colspan: Days.length
                });

                $.each(Days, function (Days_index, Days_values) {
                    thead_03.push(
                        {
                            text: Days_values.Days_Name, style: HeaderCSS_1,
                            column: Column_Index,
                            attribute: " HoliDay_Status = '" + Days_values.HoliDay_Status + "' HoliDay_Name = '" + Days_values.HoliDay_Name + "' "
                        });
                    thead_04.push(
                        {
                            text: Days_values.Session, style: HeaderCSS_1
                        });

                    Column_Index += 1;
                    DaysLength += 1;
                });
            });
            thead_01.push({
                text: Months_values.Month_Name, style: HeaderCSS_2, colspan: DaysLength
            });
        });

        thead_01.push({
            text: "รวม(คาบ)", style: HeaderCSS, rowspan: 4
        }, {
            text: "มาเรียน(คาบ)", style: HeaderCSS, rowspan: 4
        }, {
            text: "ขาด(คาบ)", style: HeaderCSS, rowspan: 4
        }, {
            text: "ลากิจ(คาบ)", style: HeaderCSS, rowspan: 4
        }, {
            text: "ลาป่วย(คาบ)", style: HeaderCSS, rowspan: 4
        }, {
            text: "กิจกรรม(คาบ)", style: HeaderCSS, rowspan: 4
        });

        //Add Rows Table
        var Student = this.reports_data.reportsDatas;
        if (Student.length === 0) return;

        $("#" + table_name).append("<tbody>");
        $.each(Student, function (Student_Index, Student_Values) {

            //fix first row problem SB-9460
            if (Student_Index == 0 && Student.length > 1) {
                var style = "style='padding:0 !important;height:0px !important;line-height:0px !important;'"
                RowsHtml = "<tr class=dummyrow ><td " + style + ">";
                RowsHtml += "<td " + style + " >";
                RowsHtml += "<td colspan=2 " + style + " row='" + Student_Index + "'>";
                var Scan_data = Student_Values.scanDatas;
                $.each(Scan_data, function (Scan_data_Index, Scan_data_Values) {
                    postion = " row='" + Student_Index + "' column='" + Scan_data_Index + "' ";
                    RowsHtml += "<td Days_Status=1  background-color: rgb(207, 207, 205);' " + postion + "  " + style + ">";
                });

                RowsHtml += `<td  ${style}>
                             <td ${style}>
                             <td ${style}>
                             <td ${style}>
                             <td ${style}>`;

                $("#" + table_name + " tbody").append(RowsHtml);
                RowsHtml = '';
            }

            //end SB-9460

            var RowsData = [];
            RowsData.push({
                text: Student_Values.RowsIndex,
                style: RowsCSS
            }, {
                text: Student_Values.Student_Id,
                style: RowsCSS
            }, {
                text: Student_Values.Student_Name,
                style: RowsCSS,
                colspan: 2,
                attribute: " student-status=\"" + Student_Values.Student_Status + "\"" +
                    " day-start=\"" + Student_Values.DayStart + "\"" +
                    " day-end=\"" + Student_Values.DayEnd + "\"" +
                    " NoteIn=\"" + Student_Values.NoteIn + "\"" +
                    " NoteOut=\"" + Student_Values.NoteOut + "\"",
                row: Student_Index +1
            });

            var Scan_data = Student_Values.scanDatas;
            $.each(Scan_data, function (Scan_data_Index, Scan_data_Values) {
                var text_status = "";

                RowsData.push({
                    style: RowsCSS,
                    row: Student_Index+1,
                    column: Scan_data_Index,
                    "text": text_status,
                    fun: function () {
                        if (Scan_data_Values.Days_Status === "0") {
                            switch (Scan_data_Values.Scan_Status.trim()) {
                                case "0": this.text = "/"; break;
                                case "1": this.text = "ส"; break;
                                case "3": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ข</span>"; break;
                                case "5": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ป</span>"; break;
                                case "4": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ล</span>"; break;
                                case "6": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ก</span>"; break;
                                case "9": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ห</span>"; break;
                                case "99": this.text = ""; break;
                                default: this.text = Scan_data_Values.Scan_Time; break;
                            }
                        } else {
                            this.text = "";
                        }
                    }
                });
            });

            RowsData.push(
                {
                    text: Student_Values.TotalHour, style: RowsCSS
                }, {
                text: Student_Values.Sum_Status_0, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_1, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_3, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_4, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_5, style: RowsCSS
            });

            $("#" + table_name + " tbody").append(RenderRows({ rowtype: "row", data: RowsData, class: "normalrow" }));
        });

        if (export_file === true) {
            $("#" + table_name).append("<tfoot>");
            var Colspan = $("#example tbody tr:last td").length + 1;
            if (Colspan < 29) Colspan = 29;
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
                    text: this.report_txt,
                    colspan: Colspan,
                    style: "font-size:18px;"
                }]
            },
                {
                    rowtype: "header",
                    data: [{
                        text: "ระดับการศึกษา : ", colspan: 2, style: "font-size:13px;text-align: right !important;"
                    }, {
                        text: LCF.GetLevelText() /*$("select[id*=ddlsublevel] option:selected").text()*/, colspan: 2, style: "font-size:13px;"
                    }, {
                        text: "",
                        colspan: Colspan - 8
                    }, {
                        text: "พิมพ์วันที่ :", style: "font-size:13px;text-align: right !important;"
                    }, {
                        text: "{day_print}", colspan: 3, style: "font-size:13px;text-align: right !important;"
                    }]
                }, {
                rowtype: "header",
                data: [{
                    text: "ห้องเรียน : ", colspan: 2, style: "font-size:13px;text-align: right !important;"
                }, {
                    text: LCF.GetClassText()/*$('select[id*=ddlSubLV2] option:selected').text()*/, colspan: 2, style: "font-size:13px;"
                },
                {
                    text: "", colspan: Colspan - 8
                }, {
                    text: "เวลา :", style: "font-size:13px;text-align: right !important;"
                },
                {
                    text: "{time_print}", colspan: 3, style: "font-size:13px;text-align: right !important;"
                }
                ]
            }, {
                rowtype: "header",
                data: [{
                    text: "", colspan: Colspan
                }]
            }
            ));

            //Add Footer Table
            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "header",
                    data: [{
                        text: "",
                        colspan: Colspan,
                        style: "font-size:20px;"
                    }]
                },
                {
                    rowtype: "header",
                    data: [{
                        text: "หมายเหตุ",
                        colspan: 2,
                        rowspan: 11,
                        style: "font-size:13px;vertical-align:top;"
                    }, {
                        text: "",
                        style: "font-size:13px;background-color: rgb(236, 70, 72);"
                    }, {
                        text: "ขาดเรียน",
                        style: "font-size:13px;"
                    }, {
                        text: "",
                        style: "font-size:13px;background-color: rgb(207, 207, 205);"
                    }, {
                        text: "วันหยุดประจำสัปดาห์",
                        colspan: 6,
                        style: "font-size:13px;"
                    }, {
                        text: "",
                        colspan: Colspan - (12 + 16),
                        style: "font-size:13px;"
                    }, {
                        text: "ลงชื่อ..................................................ครูที่ปรึกษา",
                        colspan: 10,
                        style: "font-size:13px;text-align: center !important;"
                    }, {
                        text: "",
                        colspan: 2,
                        style: "font-size:13px;text-align: center !important;"
                    }, {
                        text: "ลงชื่อ..................................................รองผู้อำนวยการ",
                        colspan: 5,
                        style: "font-size:13px;text-align: center !important;"
                    }]
                }, {
                rowtype: "header",
                data: [{
                    text: "",
                    style: "font-size:13px;background-color: rgb(255, 242, 0);"
                }, {
                    text: "มาสาย",
                    style: "font-size:13px;"
                }, {
                    text: "",
                    style: "font-size:13px;"
                }, {
                    text: "วันกิจกรรม",
                    colspan: 6,
                    style: "font-size:13px;"
                }, {
                    text: "",
                    colspan: Colspan - 11,
                    style: "font-size:13px;"
                }]
            }, {
                rowtype: "header",
                data: [{
                    text: "",
                    style: "font-size:13px;background-color: rgb(255, 128, 255);"
                }, {
                    text: "ลา",
                    style: "font-size:13px;"
                }, {
                    text: "",
                    colspan: Colspan - 21,
                    style: "font-size:13px;"
                }, {
                    text: "(.................................................)",
                    colspan: 10,
                    style: "font-size:13px;text-align: center !important;"
                }, {
                    text: "",
                    colspan: 2,
                    style: "font-size:13px;"
                }, {
                    text: "(.................................................)",
                    colspan: 5,
                    style: "font-size:13px;text-align: center !important;"
                }]
            }, {
                rowtype: "header",
                data: [{
                    text: "",
                    colspan: Colspan - 1,
                    style: "font-size:13px;"
                }]
            }, {
                rowtype: "header",
                data: [{
                    text: "",
                    colspan: 7,
                    style: "font-size:13px;"
                }, {
                    text: "",
                    colspan: Colspan - (8 + 18),
                    style: "font-size:13px;"
                }, {
                    text: "ลงชื่อ..................................................ครูที่ปรึกษา",
                    colspan: 10,
                    style: "font-size:13px;"
                }, {
                    text: "",
                    colspan: 2,
                    style: "font-size:13px;"
                }, {
                    text: "ลงชื่อ..................................................ผู้อำนวยการโรงเรียน",
                    colspan: 5,
                    style: "font-size:13px;text-align: center !important;"
                }]
            }, {
                rowtype: "header",
                data: [{
                    text: "",
                    colspan: Colspan - 1,
                    style: "font-size:13px;"
                }]
            }, {
                rowtype: "header",
                data: [{
                    text: "",
                    colspan: 7,
                    style: "font-size:13px;"
                }, {
                    text: "",
                    colspan: Colspan - (8 + 18),
                    style: "font-size:13px;"
                }, {
                    text: "(.................................................)",
                    colspan: 10,
                    style: "font-size:13px;"
                }, {
                    text: "",
                    colspan: 2,
                    style: "font-size:13px;"
                }, {
                    text: "(.................................................)",
                    colspan: 5,
                    style: "font-size:13px;text-align: center !important;"
                }]
            }, {
                rowtype: "header",
                data: [{
                    text: "",
                    colspan: Colspan - 1,
                    style: "font-size:13px;"
                }]
            }, {
                rowtype: "header",
                data: [{
                    text: "",
                    colspan: 7,
                    style: "font-size:13px;"
                }, {
                    text: "",
                    colspan: Colspan - (8 + 18),
                    style: "font-size:13px;"
                }, {
                    text: "ลงชื่อ..................................................หัวหน้าระดับ",
                    colspan: 10,
                    style: "font-size:13px;"
                }, {
                    text: "",
                    colspan: 2,
                    style: "font-size:13px;"
                }, {
                    text: "",
                    colspan: 5,
                    style: "font-size:13px;text-align: center !important;"
                }]
            }, {
                rowtype: "header",
                data: [{
                    text: "",
                    colspan: Colspan - 1,
                    style: "font-size:13px;"
                }]
            }, {
                rowtype: "header",
                data: [{
                    text: "",
                    colspan: 7,
                    style: "font-size:13px;"
                }, {
                    text: "",
                    colspan: Colspan - (8 + 18),
                    style: "font-size:13px;"
                }, {
                    text: "(.................................................)",
                    colspan: 10,
                    style: "font-size:13px;"
                }, {
                    text: "",
                    colspan: 2,
                    style: "font-size:13px;"
                }, {
                    text: "",
                    colspan: 5,
                    style: "font-size:13px;text-align: center !important;"
                }]
            }
            ));

        }

        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header", data: thead_01
                }, {
                rowtype: "header", data: thead_02
            }, {
                rowtype: "header", data: thead_03
            }, {
                rowtype: "header", data: thead_04
            }));

        var StudentStatus = $("#" + table_name + " tbody tr.normalrow td[student-status='Q']");
        $.each(StudentStatus, function (index, s) {
            var columns = $("#" + table_name + " thead tr th[column]").length;
            var colstatr = parseInt($(s).attr("day-end"));
            var rows = parseInt($(s).attr("row"));
            var NoteOut = $(s).attr("noteout");

            $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + colstatr + "]").attr("colspan", columns - colstatr);
            $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + colstatr + "]").css("background-color", "#fff");
            $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + colstatr + "]").html("ลาออก สาเหตุ : " + NoteOut);
            for (var i = colstatr + 1; i < columns; i++) {
                $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + i + "]").remove();
            }
        });

        StudentStatus = $("#" + table_name + " tbody tr.normalrow td[student-status='C']");
        $.each(StudentStatus, function (index, s) {
            var columns = $("#" + table_name + " thead tr th[column]").length;
            var Day_In = parseInt($(s).attr("day-start"));
            var Day_Out = parseInt($(s).attr("day-end"));
            var rows = parseInt($(s).attr("row"));
            var NoteIn = $(s).attr("notein");
            var NoteOut = $(s).attr("noteout");

            if (Day_In > 0) {
                $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=0]").attr("colspan", Day_In);
                $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=0]").css("background-color", "#fff");
                $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=0]").html("ย้ายห้องมากจาก " + NoteIn);
                for (var i = 1; i < Day_In; i++) {
                    $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + i + "]").remove();
                }
            }

            if (Day_Out > 0) {
                $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + Day_Out + "]").attr("colspan", columns - Day_Out);
                $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + Day_Out + "]").css("background-color", "#fff");
                $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + Day_Out + "]").html("ย้ายห้องไปยัง " + NoteOut);
                for (var j = Day_Out + 1; j < columns; j++) {
                    $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + j + "]").remove();
                }
            }
        });

        if (!export_file) {
            // Freeze Header Table & Column
            $("#" + table_name).parent().freezeTable({ 'columnNum': 4 });
        }

        var HoliDay_Status = $("#" + table_name + " thead tr th[HoliDay_Status='true']");
        var rowspan = $("#" + table_name + " tbody tr").length;
        var arrRows = $("#" + table_name + " tbody tr.normalrow td:nth-child(3)[student-status!='']")
            .map(function () { return +$(this).attr('row'); }).get(); // ['1', '7']
        arrRows.push(rowspan);

        $.each(HoliDay_Status, function (index, s) {
            var column = $(this).attr("column");
            var HoliDay_Name = $(this).attr("HoliDay_Name");

            var rows_Index = 0;

            var _start = -1;
            var _end = -1;
            var _isWrite = false;
            for (var rows_Index = 0; rows_Index <= rowspan; rows_Index++) {
                //$("#" + table_name + " tbody tr td[row=" + (rows_Index +1 ) + "][column=" + (column - 1) + "]").attr('colspan');
                if (_start == -1) _start = rows_Index;
                if (_end == -1) _end = rows_Index;

                if (arrRows.includes(rows_Index)) {
                    _end = rows_Index;
                    var _rowspan = (_end - _start);
                    var rows = $("#" + table_name + " tbody tr td[row=" + _start + "][column=" + (column ) + "]");
                    $(rows).attr("rowspan", _rowspan);
                    $(rows).attr("style", "line-height:0px !important;max-width:50px;background-color:#fff;");

                    if (_rowspan > 5 && _isWrite == false) {

                        $(rows).html("<div style=\"white-space:nowrap;mso-rotate:90;-webkit-transform: rotate(-90deg);-moz-transform: rotate(-90deg);-ms-transform: rotate(-90deg);-o-transform: rotate(-90deg);filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);\" >" + HoliDay_Name);
                        _isWrite = true;

                    }
                    else {
                        $(rows).html("<div style=\"white-space:nowrap;mso-rotate:90;-webkit-transform: rotate(-90deg);-moz-transform: rotate(-90deg);-ms-transform: rotate(-90deg);-o-transform: rotate(-90deg);filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);\" >&nbsp;");
                    }

                    if (_rowspan > 1)
                        for (var i = _start+1 ; i < _end; i++) {
                            $("#" + table_name + " tbody tr td[row=" + i + "][column=" + (column ) + "][colspan='1']").remove();
                        }

                    _start = -1;
                    _end = -1;

                }
            }
        });
        //$.each(HoliDay_Status, function (index, s) {
        //    var column = $(this).attr("column");
        //    var HoliDay_Name = $(this).attr("HoliDay_Name");
        //    var rowspan = $("#" + table_name + " tbody tr").length;
        //    var rows_Index = 0;
        //    var rows = $("#" + table_name + " tbody tr td[row=" + rows_Index + "][column=" + column + "]");
        //    var allCol = $("#" + table_name + " tbody tr td").length;
        //    while (rows.length === 0) {
        //        if (rows_Index > allCol)
        //            break;

        //        rows_Index++;
        //        rows = $("#" + table_name + " tbody tr td[row=" + rows_Index + "][column=" + column + "]");
        //    };
        //    //if (rows === undefined) rows = $("#" + table_name + " tbody tr td[row=1][column=" + (column - 1) + "]");

        //    $("#" + table_name + " tbody tr td[row!=" + rows_Index + "][column=" + column + "]").remove();
        //    $(rows).html("<div style=\"white-space:nowrap;mso-rotate:90;-webkit-transform: rotate(-90deg);-moz-transform: rotate(-90deg);-ms-transform: rotate(-90deg);-o-transform: rotate(-90deg);filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);\" >" + HoliDay_Name);
        //    $(rows).attr("rowspan", rowspan);
        //    $(rows).css("max-width", "50px");
        //    $(rows).css("background-color", "#fff");
        //});
    };

    var report_txt = '';

    this.export_excel = function () {
        var dt = new Date();
        var file_name = 'รายงานการมาเรียนรายวิชา ' + this.report_txt + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getMinutes() + dt.getSeconds() + ' .xls';
        if (Search.sort_type === 0) {
            renderHTML_02('table_exports', true);
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

};
