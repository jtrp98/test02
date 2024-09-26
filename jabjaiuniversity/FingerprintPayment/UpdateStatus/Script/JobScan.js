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
                text: "ลำดับ", style: HeaderCSS + "min-width: 50px;", rowspan: 4
            }, {
            text: "รหัสนักเรียน", style: HeaderCSS + "min-width: 100px;", rowspan: 4
        }, {
            text: "ชื่อนักเรียน", style: HeaderCSS + "min-width: 100px;", rowspan: 4
        }, {
            text: "เดือน", style: HeaderCSS + "min-width: 150px;"
        });
        thead_02.push({
            text: "สัปดาห์", style: HeaderCSS + "min-width: 150px;"
        });
        thead_03.push({
            text: "วันที่", style: HeaderCSS + "min-width: 150px;"
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
                    Column_Index++;
                    DaysLength += 1;
                    var title = "แก้ไขสถานะการมาโรงเรียน";
                    var content = "วันประจำวันที่ " + Days_values.Days_Name + " " + Months_values.Month_Name;
                    thead_03.push(
                        {
                            text: Days_values.Days_Status === "0" || Days_values.HoliDay_Status === true ? Days_values.Days_Name : createPopoverHeder(title, content, Days_values.Days_string, Days_values.Days_Name, "h_" + Days_index), style: HeaderCSS_1,
                            column: Column_Index,
                            attribute: " HoliDay_Status = '" + Days_values.HoliDay_Status + "' HoliDay_Name = '" + Days_values.HoliDay_Name + "' "
                            //text: Days_values.Days_Name, style: HeaderCSS_1
                        });
                });
            });
            thead_01.push({
                text: Months_values.Month_Name, style: HeaderCSS_2, colspan: DaysLength
            });
        });

        //Add Rows Table
        var Student = this.reports_data.reportsDatas;

        $("#" + table_name).append("<tbody>");
        $.each(Student, function (Student_Index, Student_Values) {
            var RowsData = [];
            RowsData.push({
                text: Student_Values.RowsIndex,
                style: RowsCSS
            }, {
                text: Student_Values.Student_Id,
                style: RowsCSS
            }, {
                text: "<lable data-userid='" + Student_Values.User_Id + "'>" + Student_Values.Student_Name,
                style: RowsCSS,
                colspan: 2
            });

            var Scan_data = Student_Values.scanDatas;
            $.each(Scan_data, function (Scan_data_Index, Scan_data_Values) {
                var text_status = "";
                var title = "แก้ไขสถานะการมาโรงเรียน";
                var tooltip = "ชื่อ-นามสกุล : " + Student_Values.Student_Name + " <br>รหัสนักเรียน : " + Student_Values.Student_Id + " <br>เลขที่ : " + (Student_Values.Student_Number === null ? "" : Student_Values.Student_Number);
                var content = tooltip + "<br/>วันประจำวันที่ : " + moment(Scan_data_Values.Scan_Date, 'MM/DD/YYYY').add(543, 'years').format('MM/DD/YYYY'); //+ getDate(Scan_data_Values.Scan_Date);
                RowsData.push({
                    style: RowsCSS + "color:#fff;font-weight: bolder;",
                    "text": text_status,
                    row: Student_Index,
                    column: Scan_data_Index,
                    fun: function () {
                        var message_status = "";
                        if (Scan_data_Values.Scan_Status != "Q") {                        
                            if (Scan_data_Values.Days_Status === "0") {
                                switch (Scan_data_Values.Scan_Status.trim()) {
                                    case "1": message_status = "<div data-placement=\"top\" data-toggle=\"tooltip\" data-html=\"true\" data-original-title=\"" + tooltip + "\" status-update=\"false\">ส</div>"; break;
                                    case "3": message_status = "<div data-placement=\"top\" data-toggle=\"tooltip\" data-html=\"true\" data-original-title=\"" + tooltip + "\" status-update=\"false\">ข</div>"; break;
                                    case "0": message_status = "<div data-placement=\"top\" data-toggle=\"tooltip\" data-html=\"true\" data-original-title=\"" + tooltip + "\" status-update=\"false\">/</div>"; break;
                                    case "10": message_status = "<div data-placement=\"top\" data-toggle=\"tooltip\" data-html=\"true\" data-original-title=\"" + tooltip + "\" status-update=\"false\">ล</div>"; break;
                                    case "11": message_status = "<div data-placement=\"top\" data-toggle=\"tooltip\" data-html=\"true\" data-original-title=\"" + tooltip + "\" status-update=\"false\">ป</div>"; break;
                                    case "12": message_status = "<div data-placement=\"top\" data-toggle=\"tooltip\" data-html=\"true\" data-original-title=\"" + tooltip + "\" status-update=\"false\">ก</div>"; break;
                                    case "6": message_status = "<div data-placement=\"top\" data-toggle=\"tooltip\" data-html=\"true\" data-original-title=\"" + tooltip + "\" status-update=\"false\">ห</div>"; break;
                                    case "99": message_status = "<div data-placement=\"top\" data-toggle=\"tooltip\" data-html=\"true\" data-original-title=\"" + tooltip + "\" status-update=\"false\">-</div>"; break;
                                    default: message_status = "<div data-placement=\"top\" data-toggle=\"tooltip\" data-html=\"true\" data-original-title=\"" + tooltip + "\" status-update=\"false\">" + Scan_data_Values.Scan_Time + "</div>"; break;
                                }

                                this.text = createPopover(title, content, Scan_data_Values.Scan_Date, Scan_data_Values.Schedule_Id, message_status, Scan_data_Values.Scan_Status.trim(), Student_Values.RowsIndex + "_" + Scan_data_Index);
                            } else {
                                this.text = createPopover(title, content, Scan_data_Values.Scan_Date, Scan_data_Values.Schedule_Id, " ", Student_Values.RowsIndex + "_" + Scan_data_Index);
                            }
                        }
                    }
                });
            });

            $("#" + table_name + " tbody").append(RenderRows({ rowtype: "row", data: RowsData }));
        });

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

        var options = {
            placement: function (context, source) {
                var position = $(source).position();

                if (position.left > 515) {
                    return "left";
                }

                if (position.left < 515) {
                    return "right";
                }

                if (position.top < 110) {
                    return "bottom";
                }

                return "top";
            },
            trigger: "click",
            container: 'body',
            html: true
        };

        // Freeze Header Table & Column
        $("#" + table_name).parent().freezeTable(
            {
                'columnNum': 4,
                //'scrollable': true,
                //'freezeHead': true,
                //'columnKeep': false,

            });

        $('[data-toggle="popover"]').popover(options);
        $('[data-toggle="tooltip"]').tooltip({ html: true });

        var HoliDay_Status = $("#" + table_name + " thead tr th[HoliDay_Status='true']");
        $.each(HoliDay_Status, function (index, s) {
            var column = $(this).attr("column");
            var HoliDay_Name = $(this).attr("HoliDay_Name");
            var rowspan = $("#" + table_name + " tbody tr").length;
            var rows_Index = 0;
            var rows = $("#" + table_name + " tbody tr td[row=" + rows_Index + "][column=" + (column - 1) + "]");
            while (rows.length === 0) {
                rows_Index++;
                rows = $("#" + table_name + " tbody tr td[row=" + rows_Index + "][column=" + (column - 1) + "]");
            };
            //if (rows === undefined) rows = $("#" + table_name + " tbody tr td[row=1][column=" + (column - 1) + "]");

            $("#" + table_name + " tbody tr td[row!=" + rows_Index + "][column=" + (column - 1) + "]").remove();
            $(rows).html("<div style=\"color:black;white-space:nowrap;mso-rotate:90;-webkit-transform: rotate(-90deg);-moz-transform: rotate(-90deg);-ms-transform: rotate(-90deg);-o-transform: rotate(-90deg);filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);\" >" + HoliDay_Name);
            $(rows).attr("rowspan", rowspan);
            $(rows).css("max-width", "50px");
            $(rows).css("background-color", "#fff");

        });
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
