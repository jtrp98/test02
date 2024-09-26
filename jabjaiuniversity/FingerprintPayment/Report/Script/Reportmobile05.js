var reports_04 = (function () {
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
            HeaderCSS_3 += ExportCSS.replace(";min-width", ";width");
            HeaderCSS_4 += ExportCSS.replace(";min-width", ";width");

            RowsCSS += ExportCSS.replace(";min-width", ";width");
            RowsCSS_1 += ExportCSS.replace(";min-width", ";width");
            RowsCSS_2 += ExportCSS.replace(";min-width", ";width");
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS).replace(";width", ";min-width");
            HeaderCSS_1 = HeaderCSS_1.replace(ExportCSS, "").replace(";width", ";min-width");
            HeaderCSS_2 = HeaderCSS_2.replace(ExportCSS, "").replace(";width", ";min-width");
            HeaderCSS_3 = HeaderCSS_3.replace(ExportCSS, "").replace(";width", ";min-width");
            HeaderCSS_4 = HeaderCSS_4.replace(ExportCSS, "").replace(";width", ";min-width");
            RowsCSS = RowsCSS.replace(ExportCSS, "").replace(";width", ";min-width");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "").replace(";width", ";min-width");
            RowsCSS_2 = RowsCSS_2.replace(ExportCSS, "").replace(";width", ";min-width");
            //$("#" + table_name).css("font-size", "16px");
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
                text: "ลำดับ", style: HeaderCSS_3, rowspan: 3
            }
            , {
                text: "รหัส", style: HeaderCSS, rowspan: 3
            }, {
            text: "ชื่อพนักงาน", style: HeaderCSS, rowspan: 3
        }, {
            text: "เดือน", style: HeaderCSS
        });
        thead_02.push({
            text: "สัปดาห์", style: HeaderCSS
        });
        thead_03.push({
            text: "วันที่", style: HeaderCSS
        });
        $.each(Months, function (Months_index, Months_values) {
            var Weeks = Months_values.weeks;
            var DaysLength = 0;
            $.each(Weeks, function (Weeks_index, Weeks_values) {
                var Days = Weeks_values.days;
                thead_02.push({
                    text: Weeks_values.Weeks_Id, style: HeaderCSS_2, colspan: Days.length
                });
                $.each(Days, function (Days_index, Days_values) {
                    DaysLength += 1;
                    thead_03.push(
                        {
                            text: Days_values.Days_Name, style: HeaderCSS_1
                        });
                });
            });
            thead_01.push({
                text: Months_values.Month_Name, style: HeaderCSS_2, colspan: DaysLength
            });
        });

        thead_01.push({
            text: "สาย", style: HeaderCSS_3 + (export_file === true ? "width:40px;" : ""), rowspan: 3
        }, {
            text: "เวลาสาย", style: HeaderCSS_3 + (export_file === true ? "width:80px;" : ""), rowspan: 3
        }, {
            text: "ขาด", style: HeaderCSS_3 + (export_file === true ? "width:40px;" : ""), rowspan: 3
        }, {
            text: "ลาป่วย", style: HeaderCSS_3 + (export_file === true ? "width:40px;" : ""), rowspan: 3
        }, {
            text: "ลากิจ", style: HeaderCSS_3 + (export_file === true ? "width:40px;" : ""), rowspan: 3
        }, {
            text: "ราชการ", style: HeaderCSS_3 + (export_file === true ? "width:40px;" : ""), rowspan: 3
        }, {
            text: "อบรม ดูงาน", style: HeaderCSS_3 + (export_file === true ? "width:40px;" : ""), rowspan: 3
        }, {
            text: "คลอดบุตร", style: HeaderCSS_3 + (export_file === true ? "width:40px;" : ""), rowspan: 3
        }, {
            text: "พักผ่อน", style: HeaderCSS_3 + (export_file === true ? "width:40px;" : ""), rowspan: 3
        }, {
            text: "อุปสมบท", style: HeaderCSS_3 + (export_file === true ? "width:40px;" : ""), rowspan: 3
        }, {
            text: "เตรียมพล", style: HeaderCSS_3 + (export_file === true ? "width:40px;" : ""), rowspan: 3
        }, {
            text: "รวม", style: HeaderCSS_3 + (export_file === true ? "width:40px;" : ""), rowspan: 3
        }, {
            text: "หมายเหตุ", style: HeaderCSS_3 + (export_file === true ? "width:40px;" : ""), rowspan: 3, class: ''
        });

        //Add Rows Table
        var Student = this.reports_data.reportsDatas;

        $("#" + table_name).append("<tbody>");
        $.each(Student, function (Student_Index, Student_Values) {
            var RowsData = [];
            RowsData.push(
                {
                    text: Student_Values.RowsIndex,
                    style: RowsCSS + (export_file === true ? "width:40px;" : "")
                }
                , {
                    text: Student_Values.Code,
                    style: RowsCSS + (export_file === true ? "width:80px;" : ""),
                }
                , {
                    text: Student_Values.Name,
                    style: RowsCSS_2 + (export_file === true ? "width:150px;" : ""),
                    colspan: 2
                }
            );

            var Scan_data = Student_Values.scanDatas;
            $.each(Scan_data, function (Scan_data_Index, Scan_data_Values) {
                var text_status = "";

                RowsData.push({
                    style: RowsCSS + (export_file === true ? "width:40px;" : ""),
                    "text": text_status,
                    fun: function () {
                        //if (Scan_data_Values.Days_Status.trim() === "0") {
                        switch (Scan_data_Values.Scan_StatusIn.trim()) {
                          
                            case "0": this.text = "<span style='color:black;'>" + Scan_data_Values.Scan_TimeIn + "</span>"; break;
                            case "1": this.text = "<span style='color:#e4922f;'>" + Scan_data_Values.Scan_TimeIn + "</span>"; break;
                            case "3": this.text = "<span style='color:red;'>ข</span>"; break;
                            case "11": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ป</span>"; break;
                            case "10": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ล</span>"; break;
                            //case "12": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ก</span>"; break;
                            case "8": this.text = "<span style='" + /*CycleCSS*/ "" + "'>" + (Scan_data_Values.Scan_TimeIn === null || Scan_data_Values.Scan_TimeIn.trim() === "-" ? "หยุด" : Scan_data_Values.Scan_TimeIn) + "</span>"; break;
                            case "9": this.text = "<span style='" + /*CycleCSS*/ "" + "'>" + (Scan_data_Values.Scan_TimeIn === null || Scan_data_Values.Scan_TimeIn.trim() === "-" ? "หยุด" : Scan_data_Values.Scan_TimeIn) + "</span>"; break;
                            case "21": this.text = "ค"; break;
                            case "22": this.text = "พ"; break;
                            case "23": this.text = "ศ"; break;
                            case "24": this.text = "ท"; break;
                            case "25": this.text = "อ"; break;
                            case "26": this.text = "ร"; break;
                            case "98": this.text = "-"; break;//ลาออก
                            default: this.text = Scan_data_Values.Scan_StatusIn.trim() === "-" ? "หยุด" : Scan_data_Values.Scan_TimeIn; break;
                        }

                        switch (Scan_data_Values.Scan_StatusOut.trim()) {
                          
                            case "0": this.text += "<br/><span style='color:black;'>" + Scan_data_Values.Scan_TimeOut + "</span>"; break;
                            case "1": this.text += "<br/><span style='color:#e4922f;'>" + Scan_data_Values.Scan_TimeIn + "</span>"; break;
                            case "2": this.text += "<br/><span style='color:#e4922f;'>" + Scan_data_Values.Scan_TimeOut + "</span>"; break;
                            case "3":
                                if (Scan_data_Values.Scan_TimeOut !== "-") {
                                    this.text += "<br/>" + Scan_data_Values.Scan_TimeOut;
                                }
                                else {
                                    switch (Scan_data_Values.Scan_StatusIn.trim()) {
                                        case "11": this.text += "<br/><span style='" + /*CycleCSS*/ "" + "'>ป</span>"; break;
                                        case "10": this.text += "<br/><span style='" + /*CycleCSS*/ "" + "'>ล</span>"; break;
                                        //case "12": this.text += "<br/><span style='" + /*CycleCSS*/ "" + "'>ก</span>"; break;
                                        case "8": this.text += "<br/><span style='" + /*CycleCSS*/ "" + "'>" + (Scan_data_Values.Scan_TimeOut === null || Scan_data_Values.Scan_TimeOut.trim() === "-" ? "หยุด" : Scan_data_Values.Scan_TimeOut) + "</span>"; break;
                                        case "9": this.text += "<br/><span style='" + /*CycleCSS*/ "" + "'>" + (Scan_data_Values.Scan_TimeOut === null || Scan_data_Values.Scan_TimeOut.trim() === "-" ? "หยุด" : Scan_data_Values.Scan_TimeOut) + "</span>"; break;
                                        case "21": this.text += "<br/>ค"; break;
                                        case "22": this.text += "<br/>พ"; break;
                                        case "23": this.text += "<br/>ศ"; break;
                                        case "24": this.text += "<br/>ท"; break;
                                        case "25": this.text += "<br/>อ"; break;
                                        case "26": this.text += "<br/>ร"; break;
                                        default: this.text += "<br/><span style='color:red;'>ข</span>"; break;
                                    }
                                }
                                break;
                            case "11": this.text += "<br/><span style='" + /*CycleCSS*/ "" + "'>ป</span>"; break;
                            case "10": this.text += "<br/><span style='" + /*CycleCSS*/ "" + "'>ล</span>"; break;
                            case "12": this.text += "<br/><span style='" + /*CycleCSS*/ "" + "'>ก</span>"; break;
                            case "21": this.text += "<br/>ค"; break;
                            case "22": this.text += "<br/>พ"; break;
                            case "23": this.text += "<br/>ศ"; break;
                            case "24": this.text += "<br/>ท"; break;
                            case "25": this.text += "<br/>อ"; break;
                            case "26": this.text += "<br/>ร"; break;
                            case "98": this.text += "<br/>-"; break;//ลาออก
                            default: this.text += "<br/>" + (Scan_data_Values.Scan_StatusIn.trim() === "-" ? "หยุด" : Scan_data_Values.Scan_TimeOut); break;
                        }

                        //} else {
                        //    if (Scan_data_Values.Scan_TimeIn.trim() === "-") {
                        //        this.text = "<span style='color:black;'>หยุด</span>";
                        //    } else {
                        //        this.text = "<span style='color:black;'>" + Scan_data_Values.Scan_TimeIn + "</span>";
                        //    }

                        //    if (Scan_data_Values.Scan_TimeOut.trim() === "-") {
                        //        this.text += "<br/><span style='color:black;'>หยุด</span>";
                        //    } else {
                        //        this.text += "<br/><span style='color:black;'>" + Scan_data_Values.Scan_TimeOut + "</span>";
                        //    }
                        //}
                    }
                });
            });

            RowsData.push({
                text: Student_Values.Sum_Status_1, style: RowsCSS
            }, {
                text: Student_Values.Time_Late, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_2, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_3, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_4, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_26, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_25, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_21, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_22, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_23, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_24, style: RowsCSS
            }, {
                text: Student_Values.Sum_Status_1
                    + Student_Values.Sum_Status_2
                    + Student_Values.Sum_Status_3
                    + Student_Values.Sum_Status_4
                    + Student_Values.Sum_Status_26
                    + Student_Values.Sum_Status_25
                    + Student_Values.Sum_Status_21
                    + Student_Values.Sum_Status_22
                    + Student_Values.Sum_Status_23
                    + Student_Values.Sum_Status_24
                , style: RowsCSS
            }, {
                text: '', class: ''
            }
            );

            $("#" + table_name + " tbody").append(RenderRows({ rowtype: "row", data: RowsData }));
        });

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
                    text: this.report_txt,
                    colspan: Colspan,
                    style: "font-size:18px;"
                }]
            },
                {
                    rowtype: "header",
                    data: [{
                        text: "ประเภทบุคลากร :", colspan: 2, style: "font-size:13px;text-align: right !important;"
                    }, {
                        text: $("select[id*=select_user_type] option:selected").text(), colspan: 2, style: "font-size:13px;"
                    }, {
                        text: "",
                        colspan: Colspan - 7
                    }, {
                        text: "พิมพ์วันที่ :", style: "font-size:13px;text-align: right !important;"
                    }, {
                        text: "{day_print}", colspan: 3, style: "font-size:13px;text-align: right !important;"
                    }]
                }, {
                rowtype: "header",
                data: [{
                    text: "แผนก : ", colspan: 2, style: "font-size:13px;text-align: right !important;"
                }, {
                    text: $("select[id*=select_department] option:selected").text(), colspan: 2, style: "font-size:13px;"
                }, {
                    text: "",
                    colspan: Colspan - 7
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

        }

        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header", data: thead_01
                }, {
                rowtype: "header", data: thead_02
            }, {
                rowtype: "header", data: thead_03
            }
            ));

        if (!export_file) {
            // Freeze Header Table & Column
            $("#" + table_name).parent().freezeTable({ 'columnNum': 3 });
        }
       
    };

    this.RenderHTML_02 = function (table_name, export_file) {
        var dt = new Date();
        $("#" + table_name).html("");
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            //HeaderCSS_3 += ExportCSS;
            RowsCSS += ExportCSS;
            RowsCSS_1 += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            //HeaderCSS_3 = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            //$("#" + table_name).css("font-size", "24px");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        var thead_01 = [];
        var rows_data = [];

        if (export_file === true) {
           
            var Colspan = $("#example tbody tr:last td").length -1;

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
                        text: "ประเภทบุคลากร :" + $("select[id*=select_user_type] option:selected").text(), colspan: 2, style: "font-size:13px;text-align: left !important;"
                    }, {
                        text: "", colspan: 4, style: "font-size:13px;text-align: left !important;"
                    }, {
                        text: "พิมพ์วันที่ :", style: "font-size:13px;text-align: right !important;"
                    }, {
                        text: "{day_print}", colspan: 1, style: "font-size:13px;text-align: left !important;"
                    }]
                }, {
                rowtype: "header",
                data: [{
                    text: "แผนก : " + $("select[id*=select_department] option:selected").text(), colspan: 2, style: "font-size:13px;text-align: left !important;"
                }, {
                    text: "", colspan: 4, style: "font-size:13px;text-align: left !important;"
                }, {
                    text: "เวลา :", style: "font-size:13px;text-align: right !important;"
                },
                {
                    text: "{time_print}", colspan: 1, style: "font-size:13px;text-align: left !important;"
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

        //Add Header Table
        var Reports_Data02 = this.reports_data.data;
        thead_01.push(
            { text: "ลำดับ", style: "" + (export_file === true ? "width:80px;" : "") },
            { text: "ประเภทบุคลากร", style: "" + (export_file === true ? "width:150px;" : "") },
            { text: "แผนก", style: "" + (export_file === true ? "width:150px;" : "") },
            { text: "รหัส", style: ""  + (export_file === true ? "width:100px;" : "") },
            { text: "ชื่อ-นามสกุล", style: ""  + (export_file === true ? "width:200px;" : "") },
            { text: "สถานะการเข้างาน", style: ""  + (export_file === true ? "width:100px;" : "") },
            { text: "เวลา", style: ""  + (export_file === true ? "width:100px;" : "") },
            { text: "สถานะการออกงาน", style: ""  + (export_file === true ? "width:100px;" : "") },
            { text: "เวลา", style: "" + (export_file === true ? "width:100px;" : "") },
            { text: "รูป", style: (export_file === true ? "width:150px;" : ""), class: "no-export" },
            { text: "หมายเหตุ", style: ""  + (export_file === true ? "width:150px;" : ""), class: '' }
        );

        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header",
                    data: thead_01
                }
            ));

        //Add Rows Table
        var rows = 1;
        $.each(Reports_Data02, function (index, data) {
            $("#" + table_name + " tbody").append(
                RenderRows(
                    {
                        rowtype: "row",
                        data: [
                            { text: index + 1, style: "text-align:center;width:80px;" },
                            { text: data.Department, style: "text-align:center;width:150px;" },
                            { text: data.EmpType, style: "text-align:center;width:150px;" },
                            { text: data.Code, style: "text-align:center;width:100px;" },
                            { text: data.Student_Name, style: "text-align:left;width:200px" },
                            {
                                text: data.ScanIn_Status, style: "text-align:center;width:100px",
                                fun: function () {
                                    switch (data.ScanIn_Status.trim()) {
                                        case "0":
                                            if (data.IsScanEarly) 
                                                this.text = "<span style='color:#e4922f;'>เข้าก่อนเวลา</span>";                                            
                                            else
                                                this.text = "<span style='color:black;'>เข้าตรงเวลา</span>";
                                            break;
                                        case "1": this.text = "<span style='color:#e4922f;'>สาย</span>"; break;
                                        case "3": this.text = "<span style='color:red;'>ขาด</span>"; break;
                                        case "11": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ป่วย</span>"; break;
                                        case "10": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ลากิจ</span>"; break;
                                        //case "12": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ก</span>"; break;
                                        case "9": case "8": this.text = "<span style='" + /*CycleCSS*/ "" + "'>หยุด</span>"; break;
                                        //case "9": this.text = "<span style='" + /*CycleCSS*/ "" + "'>" + (data.ScanIn_Time === null || data.ScanIn_Time.trim() === "-" ? "หยุด" : data.ScanIn_Time) + "</span>"; break;
                                        case "21": this.text = "ลาคลอดบุตร"; break;
                                        case "22": this.text = "ลาพักร้อน"; break;
                                        case "23": this.text = "ลาอุปสมบทหรือการไปประกอบพิธีฮัจย์"; break;
                                        case "24": this.text = "ลาเข้ารับการตรวจเลือกหรือเข้ารับการเตรียมพล"; break;
                                        case "25": this.text = "ลาไปศึกษา ฝึกอบรม ปฏิบัติการวิจัย หรือดูงาน"; break;
                                        case "26": this.text = "ลาไปราชการ"; break;
                                        case "30": this.text = data.ScanInText; break;
                                        case "98": this.text = "-"; break;//ลาออก
                                        default: this.text = data.ScanIn_Time.trim() === "-" ? "หยุด" : data.Scan_TimeIn; break;

                                    }
                                }
                            },
                            { text: data.ScanIn_Time, style: "text-align:center;width:100px;" },
                            {
                                text: data.ScanOut_Status, style: "text-align:center;width:100px;",
                                fun: function () {
                                    switch (data.ScanOut_Status.trim()) {
                                        case "0":
                                            if (data.IsOutLate)
                                                this.text = "<span style='color:#e4922f;'>ออกเกินเวลา</span>";
                                            else
                                                this.text = "<span style='color:black;'>ออกตรงเวลา</span>";
                                            break;
                                            
                                        case "2": this.text = "<span style='color:#e4922f;'>ออกก่อนเวลา</span>"; break;
                                        case "3":
                                            if (data.IsOutLate) {
                                                this.text = "<span style='color:#e4922f;'>ออกเกินเวลา</span>";
                                            }
                                            else {
                                                switch (data.ScanIn_Status.trim()) {
                                                    case "11": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ป่วย</span>"; break;
                                                    case "10": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ลากิจ</span>"; break;
                                                    //case "12": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ก</span>"; break;
                                                    case "8": this.text = "<span style='" + /*CycleCSS*/ "" + "'>" + (data.Scan_StatusIn === null || data.ScanOut_Time.trim() === "-" ? "หยุด" : data.ScanOut_Time) + "</span>"; break;
                                                    case "9": this.text = "<span style='" + /*CycleCSS*/ "" + "'>" + (data.Scan_StatusIn === null || data.ScanOut_Time.trim() === "-" ? "หยุด" : data.ScanOut_Time) + "</span>"; break;
                                                    case "21": this.text = "ลาคลอดบุตร"; break;
                                                    case "22": this.text = "ลาพักร้อน"; break;
                                                    case "23": this.text = "ลาอุปสมบทหรือการไปประกอบพิธีฮัจย์"; break;
                                                    case "24": this.text = "ลาเข้ารับการตรวจเลือกหรือเข้ารับการเตรียมพล"; break;
                                                    case "25": this.text = "ลาไปศึกษา ฝึกอบรม ปฏิบัติการวิจัย หรือดูงาน"; break;
                                                    case "26": this.text = "ลาไปราชการ"; break;
                                                    case "30": this.text = data.ScanInText; break;
                                                    default: this.text = "<span style='color:red;'>ขาด</span>"; break;
                                                }
                                            }
                                            break;
                                        //this.text = (data.ScanIn_Status.trim() === "3" ? "<span style='color:red;'>ขาด</span>" : "หยุด"); break;
                                        case "11": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ป่วย</span>"; break;
                                        case "10": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ลากิจ</span>"; break;
                                        //case "12": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ก</span>"; break;
                                        case "8": this.text = "หยุด"; break;
                                        case "9": this.text = "หยุด"; break;
                                        case "21": this.text = "ลาคลอดบุตร"; break;
                                        case "22": this.text = "ลาพักร้อน"; break;
                                        case "23": this.text = "ลาอุปสมบทหรือการไปประกอบพิธีฮัจย์"; break;
                                        case "24": this.text = "ลาเข้ารับการตรวจเลือกหรือเข้ารับการเตรียมพล"; break;
                                        case "25": this.text = "ลาไปศึกษา ฝึกอบรม ปฏิบัติการวิจัย หรือดูงาน"; break;
                                        case "26": this.text = "ลาไปราชการ"; break;
                                        case "30": this.text = data.ScanInText; break;
                                        case "98": this.text = "-"; break;//ลาออก
                                        default: this.text = data.ScanOut_Time.trim() === "-" ? "หยุด" : data.ScanOut_Time; break;
                                    }
                                }
                            },
                            { text: data.ScanOut_Time, style: "text-align:center;width:100px" },
                            {
                                text: data.FaceUrl, style: "text-align:center;width:100px;" ,class: "no-export",
                                fun: function () {
                                    var html = "";
                                    if (data.FaceUrl) {
                                        html = `<a style='width: 30px;height:30px;' target='_blank' class='popup-link' href='${data.FaceUrl}'>
                                                    <img width='30px' height='30px'  style='width: 30px;height:30px;' src='${data.FaceUrl}'></img>
                                                </a>
                                                <img width='7%' height='7%' style='padding:5% !important;' class='hidden' src='` + data.FaceUrl + `'></img>`;

                                    }
                                    this.text = html;
                                }
                            },

                            { text: data.DeviceType, style: "width:120px;text-align: center;" , class: '' },
                        ]
                    }
                ));
        });

        if (export_file === true) {
            console.log($("#" + table_name + " tfoot").html());

            $("#table_exports").find('.no-export').remove();

            var summary = this.reports_data.summary;
            $("#" + table_name + " tfoot").html('');
            
            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '<br/>',
                            style: "text-align:right;",
                            colspan: 8,
                        },
                        
                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: 'จำนวนบุคลากร-อาจารย์',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.CountAll + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                        {
                            text: 'ลาบวช',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count23 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                    ]
                })
            );    

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: 'ไม่มาปฏิบัติงาน (ขาด)',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count3 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                        {
                            text: 'ลารับราชการทหาร',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count24 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                    ]
                },      
            ));

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: 'ลาป่วย',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count11 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                        {
                            text: 'ลาเรียน',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count25 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                    ]
                },
            ));

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: 'ลากิจส่วนตัว',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count10 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                        {
                            text: 'ลาไปราชการ',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count26 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                    ]
                },
            ));

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: 'ลาคลอดบุตร',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count21 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                        {
                            text: 'มาปฏิบัติงาน',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count0 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                    ]
                },
            ));

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: 'ลาพักร้อน',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count22 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                        {
                            text: '',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: ' ',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                    ]
                },
            ));

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '<br/>',
                            style: "text-align:right;",
                            colspan: 8,
                        },
                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: '...........................................',
                            style: "text-align:center;",
                            colspan: 4,
                        },
                       
                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: '( ' + $('#txtHeadOfHuman').val()  + ' )',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: 'รองผู้อำนวยการฝ่ายบุคคล / หัวหน้าฝ่ายบุคคล',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: 'ผู้รายงาน',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
            ));

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '<br/>',
                            style: "text-align:right;",
                            colspan: 8,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: '...........................................',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: '( ' + $('#txtHead').val() + ' )',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: 'ผู้อำนวยการโรงเรียน',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: 'ผู้ตรวจรายงาน',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
            ));
        }
        else {
            $('.popup-link').magnificPopup({ type: 'image' });
        }
        //$("body").mLoading('hide');

        //Add Footer Table
    };

    this.RenderHTML_04 = function (table_name, export_file) {
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
            //$("#" + table_name).css("font-size", "24px");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        var thead_01 = [];
        var rows_data = [];


        $('#report4-school').text($("input[id*=hdfschoolname]").val());
        $('#report4-type').text($("select[id*=select_user_type] option:selected").text());
        $('#report4-dept').text($("select[id*=select_department] option:selected").text());
        $('#report4-print-date').text(moment().format('D MMMM') + ' ' + (+moment().format('YYYY') + 543));
        $('#report4-print-time').text(moment().format('HH:mm') + 'น.');

        var Reports_Data02 = this.reports_data;
        thead_01.push(
            { text: "ลำดับ", style: " text-align: center !important;vertical-align: middle !important; width: 50px;" + (export_file === true ? "width:50px;" : "") },
            { text: "รหัส", style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:120px;" : "") },
            { text: "ชื่อ-นามสกุล", style: " text-align: center !important;vertical-align: middle !important; width: 200px;" + (export_file === true ? "width:150px;" : "") },

            { text: "สาย", style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:100px;" : "") },//1
            { text: "ลากิจ", style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:100px;" : "") },//10
            { text: "ลาป่วย", style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:100px;" : "") },//11
            { text: "ขาด", style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:100px;" : "") },//3
            { text: "เข้าก่อน ออกหลัง", style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:100px;" : "") },//early
            { text: "ราชการ", style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:100px;" : "") },//26
            { text: "อบรม ดูงาน", style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:100px;" : "") },//25
            { text: "คลอดบุตร", style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:100px;" : "") },//21
            { text: "พักผ่อน", style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:100px;" : "") },//22
            { text: "อุปสมบท", style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:100px;" : "") },//23
            { text: "เตรียมพล", style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:100px;" : "") },//24
            { text: "รวม", style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:100px;" : "") },
            { text: "หมายเหตุ", class: 'd-none', style: " text-align: center !important;vertical-align: middle !important; width: 80px;" + (export_file === true ? "width:150px;" : "") }
        );

        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header",
                    data: thead_01
                }
            ));

        //Add Rows Table
        var rows = 1;
        $.each(Reports_Data02, function (index, data) {
            $("#" + table_name + " tbody").append(
                RenderRows(
                    {
                        rowtype: "row",
                        data: [
                            { text: index + 1, style: "text-align:center;width: 50px;" },
                            { text: data.Code, style: "text-align:center;width: 80px;" },
                            { text: data.Name, style: "text-align:left;width: 200px;" },
                            { text: data.Sum_Status_1, style: "text-align:center;width: 80px;" },
                            { text: data.Sum_Status_10, style: "text-align:center;width: 80px;" },
                            { text: data.Sum_Status_11, style: "text-align:center;width: 80px;" },
                            { text: data.Sum_Status_3, style: "text-align:center;width: 80px;" },
                            { text: data.Sum_Status_Early + "/" + data.Sum_Day, style: "text-align:center;width: 80px;" },
                            { text: data.Sum_Status_26, style: "text-align:center;width: 80px;" },
                            { text: data.Sum_Status_25, style: "text-align:center;width: 80px;" },
                            { text: data.Sum_Status_21, style: "text-align:center;width: 80px;" },
                            { text: data.Sum_Status_22, style: "text-align:center;width: 80px;" },
                            { text: data.Sum_Status_23, style: "text-align:center;width: 80px;" },
                            { text: data.Sum_Status_24, style: "text-align:center;width: 80px;" },
                            {
                                text: data.Sum_Status_1
                                    + data.Sum_Status_10
                                    + data.Sum_Status_11
                                    + data.Sum_Status_3
                                    + data.Sum_Status_26
                                    + data.Sum_Status_25
                                    + data.Sum_Status_21
                                    + data.Sum_Status_22
                                    + data.Sum_Status_23
                                    + data.Sum_Status_24
                                , style: "text-align:center;width: 80px;"
                            },
                            { text: '', style: "width: 80px;", class: 'd-none' },
                        ]
                    }
                ));
        });

        //$("body").mLoading('hide');

        //Add Footer Table
    };
    var report_txt = '';

    this.Render3Form2 = function (table_name, export_file) {
        var dt = new Date();
        $("#" + table_name).html("");
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            //HeaderCSS_3 += ExportCSS;
            RowsCSS += ExportCSS;
            RowsCSS_1 += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            //HeaderCSS_3 = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            //$("#" + table_name).css("font-size", "24px");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        var thead_01 = [];
        var rows_data = [];

        if (export_file === true) {

            var Colspan = $("#example tbody tr:last td").length - 1;

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
                        text: "ประเภทบุคลากร :" + $("select[id*=select_user_type] option:selected").text(), colspan: 2, style: "font-size:13px;text-align: left !important;"
                    }, {
                        text: "", colspan: 4, style: "font-size:13px;text-align: left !important;"
                    }, {
                        text: "พิมพ์วันที่ :", style: "font-size:13px;text-align: right !important;"
                    }, {
                        text: "{day_print}", colspan: 1, style: "font-size:13px;text-align: left !important;"
                    }]
                }, {
                rowtype: "header",
                data: [{
                    text: "แผนก : " + $("select[id*=select_department] option:selected").text(), colspan: 2, style: "font-size:13px;text-align: left !important;"
                }, {
                    text: "", colspan: 4, style: "font-size:13px;text-align: left !important;"
                }, {
                    text: "เวลา :", style: "font-size:13px;text-align: right !important;"
                },
                {
                    text: "{time_print}", colspan: 1, style: "font-size:13px;text-align: left !important;"
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

        //Add Header Table
        
        thead_01.push(
            { text: "ลำดับ", style: "" + (export_file === true ? "width:80px;" : "") },
            { text: "วันที่", style: "" + (export_file === true ? "width:80px;" : "") },
            { text: "ประเภทบุคลากร", style: "" + (export_file === true ? "width:150px;" : "") },
            { text: "แผนก", style: "" + (export_file === true ? "width:150px;" : "") },
            { text: "รหัส", style: "" + (export_file === true ? "width:100px;" : "") },
            { text: "ชื่อ-นามสกุล", style: "" + (export_file === true ? "width:200px;" : "") },
            { text: "สถานะการเข้างาน", style: "" + (export_file === true ? "width:100px;" : "") },
            { text: "เวลา", style: "" + (export_file === true ? "width:100px;" : "") },
            { text: "สถานะการออกงาน", style: "" + (export_file === true ? "width:100px;" : "") },
            { text: "เวลา", style: "" + (export_file === true ? "width:100px;" : "") },
            { text: "รูป", style: (export_file === true ? "width:150px;" : ""), class: "no-export" },
            { text: "หมายเหตุ", style: "" + (export_file === true ? "width:150px;" : ""), class: '' }
        );

        $("#" + table_name + " thead").append(
            RenderRows(
                {
                    rowtype: "header",
                    data: thead_01
                }
            ));

        //Add Rows Table
        var rows = 1;

        $.each(this.reports_data, function (index, data1) {
            var c = data1.count;
            var date = data1.date;

            $.each(data1.lst, function (index, data) {
                $("#" + table_name + " tbody").append(
                    RenderRows(
                        {
                            rowtype: "row",
                            data: [
                                { text: index + 1, style: "text-align:center;width:80px;" },
                                index == 0 ? { text: date, style: "text-align:center;width:80px;vertical-align: text-top;", rowspan: c } : {},
                                { text: data.EmpType, style: "text-align:center;width:150px;" },
                                { text: data.Department, style: "text-align:center;width:150px;" },
                                { text: data.Code, style: "text-align:center;width:100px;" },
                                { text: data.Name, style: "text-align:left;width:200px" },
                                {
                                    text: data.Status1, style: "text-align:center;width:100px",
                                    fun: function () {
                                        switch (data.Status1) {
                                            case "0":
                                                if (data.IsEarly)
                                                    this.text = "<span style='color:#e4922f;'>เข้าก่อนเวลา</span>";
                                                else
                                                    this.text = "<span style='color:black;'>เข้าตรงเวลา</span>";
                                                break;
                                            case "1": this.text = "<span style='color:#e4922f;'>สาย</span>"; break;
                                            case "3": this.text = "<span style='color:red;'>ขาด</span>"; break;
                                            case "11": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ป่วย</span>"; break;
                                            case "10": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ลากิจ</span>"; break;
                                            //case "12": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ก</span>"; break;
                                            case "9": case "8": this.text = "<span style='" + /*CycleCSS*/ "" + "'>หยุด</span>"; break;
                                            //case "9": this.text = "<span style='" + /*CycleCSS*/ "" + "'>" + (data.ScanIn_Time === null || data.ScanIn_Time.trim() === "-" ? "หยุด" : data.ScanIn_Time) + "</span>"; break;
                                            case "21": this.text = "ลาคลอดบุตร"; break;
                                            case "22": this.text = "ลาพักร้อน"; break;
                                            case "23": this.text = "ลาอุปสมบทหรือการไปประกอบพิธีฮัจย์"; break;
                                            case "24": this.text = "ลาเข้ารับการตรวจเลือกหรือเข้ารับการเตรียมพล"; break;
                                            case "25": this.text = "ลาไปศึกษา ฝึกอบรม ปฏิบัติการวิจัย หรือดูงาน"; break;
                                            case "26": this.text = "ลาไปราชการ"; break;
                                            case "98": this.text = "-"; break;//ลาออก
                                            default: this.text = data.Time1 === "-" ? "หยุด" : data.Scan_TimeIn; break;

                                        }
                                    }
                                },
                                { text: data.Time1, style: "text-align:center;width:100px;" },
                                {
                                    text: data.Status2, style: "text-align:center;width:100px;",
                                    fun: function () {
                                        switch (data.Status2) {
                                            case "0":
                                                if (data.IsLate) 
                                                    this.text = "<span style='color:#e4922f;'>ออกเกินเวลา</span>";  
                                                else 
                                                    this.text = "<span style='color:black;'>ออกตรงเวลา</span>";   
                                                break;
                                            case "2": this.text = "<span style='color:#e4922f;'>ออกก่อนเวลา</span>"; break;
                                            case "3":
                                                if (data.IsLate) {
                                                    this.text = "<span style='color:#e4922f;'>ออกเกินเวลา</span>";
                                                }
                                                else {
                                                    switch (data.Status1.trim()) {
                                                        case "11": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ป่วย</span>"; break;
                                                        case "10": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ลากิจ</span>"; break;
                                                        //case "12": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ก</span>"; break;
                                                        case "8": this.text = "<span style='" + /*CycleCSS*/ "" + "'>" + (data.Scan_StatusIn === null || data.ScanOut_Time.trim() === "-" ? "หยุด" : data.ScanOut_Time) + "</span>"; break;
                                                        case "9": this.text = "<span style='" + /*CycleCSS*/ "" + "'>" + (data.Scan_StatusIn === null || data.ScanOut_Time.trim() === "-" ? "หยุด" : data.ScanOut_Time) + "</span>"; break;
                                                        case "21": this.text = "ลาคลอดบุตร"; break;
                                                        case "22": this.text = "ลาพักร้อน"; break;
                                                        case "23": this.text = "ลาอุปสมบทหรือการไปประกอบพิธีฮัจย์"; break;
                                                        case "24": this.text = "ลาเข้ารับการตรวจเลือกหรือเข้ารับการเตรียมพล"; break;
                                                        case "25": this.text = "ลาไปศึกษา ฝึกอบรม ปฏิบัติการวิจัย หรือดูงาน"; break;
                                                        case "26": this.text = "ลาไปราชการ"; break;
                                                        default: this.text = "<span style='color:red;'>ขาด</span>"; break;
                                                    }
                                                }
                                                break;
                                            //this.text = (data.ScanIn_Status.trim() === "3" ? "<span style='color:red;'>ขาด</span>" : "หยุด"); break;
                                            case "11": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ป่วย</span>"; break;
                                            case "10": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ลากิจ</span>"; break;
                                            //case "12": this.text = "<span style='" + /*CycleCSS*/ "" + "'>ก</span>"; break;
                                            case "8": this.text = "หยุด"; break;
                                            case "9": this.text = "หยุด"; break;
                                            case "21": this.text = "ลาคลอดบุตร"; break;
                                            case "22": this.text = "ลาพักร้อน"; break;
                                            case "23": this.text = "ลาอุปสมบทหรือการไปประกอบพิธีฮัจย์"; break;
                                            case "24": this.text = "ลาเข้ารับการตรวจเลือกหรือเข้ารับการเตรียมพล"; break;
                                            case "25": this.text = "ลาไปศึกษา ฝึกอบรม ปฏิบัติการวิจัย หรือดูงาน"; break;
                                            case "26": this.text = "ลาไปราชการ"; break;
                                            case "98": this.text = "-"; break;//ลาออก
                                            default: this.text = data.Time2 === "-" ? "หยุด" : data.ScanOut_Time; break;
                                        }
                                    }
                                },
                                { text: data.Time2, style: "text-align:center;width:100px" },
                                {
                                    text: data.Face1, style: "text-align:center;width:100px;", class: "no-export",
                                    fun: function () {
                                        var html = "";
                                        if (data.Face1) {
                                            html = `<a style='width: 30px;height:30px;' target='_blank' class='popup-link' href='${data.Face1}'>
                                                    <img width='30px' height='30px'  style='width: 30px;height:30px;' src='${data.Face1}'></img>
                                                </a>
                                                <img width='7%' height='7%' style='padding:5% !important;' class='hidden' src='` + data.Face1 + `'></img>`;

                                        }
                                        this.text = html;
                                    }
                                },

                                { text: data.Device1, style: "width:120px;text-align: center;", class: '' },
                            ]
                        }
                    )
                );
            });

            $("#" + table_name + " tbody").append(
                RenderRows(
                    {
                        rowtype: "row",
                        data: [
                            { text: "รวมทั้งหมด " + c + " คน", style: "text-align:right;"  ,colspan :12 },
                        ]
                    }
                )
            );
        });
       

        if (export_file === true) {
            console.log($("#" + table_name + " tfoot").html());

            $("#table_exports").find('.no-export').remove();

            var summary = this.reports_data.summary;
            $("#" + table_name + " tfoot").html('');

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '<br/>',
                            style: "text-align:right;",
                            colspan: 8,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: 'จำนวนบุคลากร-อาจารย์',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.CountAll + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                        {
                            text: 'ลาบวช',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count23 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                    ]
                })
            );

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: 'ไม่มาปฏิบัติงาน (ขาด)',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count3 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                        {
                            text: 'ลารับราชการทหาร',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count24 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                    ]
                },
            ));

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: 'ลาป่วย',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count11 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                        {
                            text: 'ลาเรียน',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count25 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                    ]
                },
            ));

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: 'ลากิจส่วนตัว',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count10 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                        {
                            text: 'ลาไปราชการ',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count26 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                    ]
                },
            ));

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: 'ลาคลอดบุตร',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count21 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                        {
                            text: 'มาปฏิบัติงาน',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count0 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                    ]
                },
            ));

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: 'ลาพักร้อน',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: summary.Count22 + ' คน',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                        {
                            text: '',
                            style: "text-align:right;",
                            colspan: 3,
                        },
                        {
                            text: ' ',
                            style: "text-align:center;",
                            colspan: 1,
                        },
                    ]
                },
            ));

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '<br/>',
                            style: "text-align:right;",
                            colspan: 8,
                        },
                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: '...........................................',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: '( ' + $('#txtHeadOfHuman').val() + ' )',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: 'รองผู้อำนวยการฝ่ายบุคคล / หัวหน้าฝ่ายบุคคล',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: 'ผู้รายงาน',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
            ));

            $("#" + table_name + " tfoot").append(RenderRows(
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '<br/>',
                            style: "text-align:right;",
                            colspan: 8,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: '...........................................',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: '( ' + $('#txtHead').val() + ' )',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: 'ผู้อำนวยการโรงเรียน',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        {
                            text: '',
                            colspan: 4,
                        },
                        {
                            text: 'ผู้ตรวจรายงาน',
                            style: "text-align:center;",
                            colspan: 4,
                        },

                    ]
                },
            ));
        }
        else {
            $('.popup-link').magnificPopup({ type: 'image' });
        }
        $("body").mLoading('hide');

        //Add Footer Table
    };
        
    this.export_excel = function () {

        if (Search == null) {
            return;
        }

        var dt = new Date();
        var file_name = this.report_txt + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getMinutes() + dt.getSeconds() + '.xls';
        if (Search.sort_type === "0") {
            this.RenderHTML_02('table_exports', true);
            var param = {
                "filename": "filename02",
                "tabledata": $("#export_excel").html()
            };
            $.post("/export_excel.aspx", param, function (data) {
                downloadFile(file_name, 'data:application/xml;charset=utf-8;base64,', data);
            });
        }
        else if (Search.sort_type === "3" || Search.sort_type === "4") {
            $('#report4 #report4-content').html($('#example').html());
            $('#report4 #report4-type').html($('#select_user_type :selected').text());
            $('#report4 #report4-dept').html($('#select_department :selected').text());

            $('#report4 #report4-print-date').html(moment().format("DD/MM/YYYY"));
            $('#report4 #report4-print-time').html(moment().format("HH:mm"));
            tableToExcel("report4", 'DataSheet', 'รายงานการมาทำงานใหม่ ' + $("input[id*=hdfschoolname]").val());
        }
        else {
            //var dStart = "", dEnd = "";
            //if ($('select[id*=sort_type]').val() === "1") {
            //    dStart = $("#select_month").val() + "/1/" + ($("select[id*=select_year] option:selected").text() - 543);             
            //}
            //else if ($('select[id*=sort_type]').val() === "0") {              
            //    if ($("#txtend").val() !== "") {
            //        dEnd = $("#txtend").val().split('/')[1] + "/" + $("#txtend").val().split('/')[0] + "/" + $("#txtend").val().split('/')[2];
            //        dStart = dEnd;
            //        $('#txtend-error').hide();
            //    }
            //    else {
            //        $('#txtend-error').show();
            //        return false;
            //    }
            //}
            //else if ($('select[id*=sort_type]').val() === "3") {

            //    if ($("#txtstart").val() !== "") {
            //        dStart = $("#txtstart").val().split('/')[1] + "/" + $("#txtstart").val().split('/')[0] + "/" + $("#txtstart").val().split('/')[2];
            //        $('#txtstart-error').hide();
            //    }
            //    else {
            //        $('#txtstart-error').show();
            //        return false;
            //    }
            //    if ($("#txtend").val() !== "") {
            //        dEnd = $("#txtend").val().split('/')[1] + "/" + $("#txtend").val().split('/')[0] + "/" + $("#txtend").val().split('/')[2];
            //        $('#txtend-error').hide();
            //    }
            //    else {
            //        $('#txtend-error').show();
            //        return false;
            //    }
            //}
            //else if ($('select[id*=sort_type]').val() === "4") {
            //    dStart = '01/01/' + ($("select[id*=select_year] option:selected").text() - 543);
            //    dEnd = '01/01/' + ($("select[id*=select_year] option:selected").text() - 543);
            //}

            //Search = {
            //    "term_id": $('select[id*=semister]').val(),
            //    "level2_id": $('select[id*=ddlSubLV2]').val(),
            //    "sort_type": $('select[id*=sort_type]').val(),
            //    "dStart": dStart,
            //    "dEnd": dEnd,
            //    "plane_Id": $('select[id*=plane]').val(),
            //    "department_id": $("#select_department option:selected").val(),
            //    "user_type": $("#select_user_type option:selected").val(),
            //    "emp_id": $("#txtid").val(),
            //    "status": $("#status").val()
            //};

            //$("body").mLoading('show');
            var json = JSON.stringify(Search);
            var xhr = new XMLHttpRequest();
            xhr.open("POST", "/Report/Handles/Reports05_exportHandler.ashx", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                saveAs(xhr.response, 'รายงานการมาทำงาน ใหม่ ' + $("input[id*=hdfschoolname]").val() + "_" + dt.format("ddMMyyyyHHmmssss") + '.xls');
                //$("body").mLoading('hide');
            };
            xhr.onerror = function () {
                window.location.reload();
            };
            xhr.send(json);
        }
    };

    var tableToExcel = (function () {
        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body>{table}</body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

        return function (table, name, filename) {
            if (!table.nodeType) table = document.getElementById(table)
            var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
            // window.location.href = uri + base64(format(template, ctx))

            var today = new Date();
            var date = ('0' + today.getDate()).slice(-2) + "-" + ('0' + (today.getMonth() + 1)).slice(-2) + "-" + today.getFullYear();

            var link = document.createElement("a");
            link.download = filename + '_' + date + ".xls";
            link.href = uri + base64(format(template, ctx));
            link.click();
        }
    })()

});
