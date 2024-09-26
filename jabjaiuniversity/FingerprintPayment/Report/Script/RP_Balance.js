var RPBalanceStudent = (function () {
    this.Report_Student_Balance = [];
    var HeaderCSS = "font-weight: bold;text-align: center;color: rgb(255, 255, 255);text-align: center !important; background-color: rgb(51, 122, 183);";
    var RowsCSS = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_1 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: top !important;padding:0px;";
    var RowsCSS_2 = "background-color: rgb(255, 255, 255);text-align: right !important; vertical-align: middle !important;padding:0px;padding-right:3px";

    var RowsCSS_1_left = "background-color: rgb(255, 255, 255);text-align: left !important; vertical-align: top !important;padding:0px;padding-left:3px";
    var RowsCSS_SUM1 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important; padding-left:0px; padding-top:1px; padding-bottom:0px; padding-right:7px; background-color:#eee;";
    var RowsCSS_SUM2 = "background-color: rgb(255, 255, 255);text-align: right !important; vertical-align: middle !important; padding-left:0px; padding-top:1px; padding-bottom:0px; padding-right:7px; background-color:#eee;";

    this.RenderHtml_Student_Balance = function (table_name, export_file) {
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS;
            RowsCSS_1 += ExportCSS;
            RowsCSS_1_left += ExportCSS;
            RowsCSS_SUM1 += ExportCSS;
            RowsCSS_SUM2 += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            RowsCSS_1_left = RowsCSS_1_left.replace(ExportCSS, "")
            RowsCSS_SUM1 = RowsCSS_SUM1.replace(ExportCSS, "");
            RowsCSS_SUM2 = RowsCSS_SUM2.replace(ExportCSS, "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        var rEport_tYpe = $('select[id*=report_type').val();

        //RenderTable
        if (rEport_tYpe == 0) {

            $("#" + table_name + " tbody").append(
                RenderRows({
                    rowtype: "row",
                    data: [
                        { text: "ชั้นเรียน", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 1 },
                        { text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 1 },
                        { text: "วันที่", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 1 },
                        { text: "รหัสนักเรียน", style: HeaderCSS + (export_file === true ? "width:100px;" : ""), rowspan: 1 },
                        { text: "ชื่อ - นามสกุล", style: HeaderCSS + (export_file === true ? "width:340px;" : ""), rowspan: 1 },
                        { text: "ยอดเงินคงเหลือ", style: HeaderCSS + (export_file === true ? "width:120px;" : ""), rowspan: 1 }
                    ]
                }));

            var TotalBalance = 0;

            var Layer0_data = this.Report_Student_Balance.LaYer0s;
            $.each(Layer0_data, function (Layer0_Index, Layer0_Values) {
                var Layer1_data = Layer0_Values.LaYer1s;

                $.each(Layer1_data, function (Layer1_Index, Layer1_Values) {
                    var Layer2_data = Layer1_Values.LaYer2s;
                    var SumBalance = 0;

                    $.each(Layer2_data, function (Layer2_Index, Layer2_Values) {
                        var Layer3_data = Layer2_Values.LaYer3s;

                        $.each(Layer3_data, function (Layer3_Index, Layer3_Values) {
                            SumBalance += Layer3_Values.StudentBalance;

                            TotalBalance += Layer3_Values.StudentBalance;

                            if (Layer2_Index == 0) {
                                $("#" + table_name + " tbody").append(
                                    RenderRows({
                                        rowtype: "row",
                                        data: [
                                            { text: Layer1_Values.RoomFullName, style: RowsCSS_1, rowspan: Layer2_data.length },
                                            { text: Layer2_Index + 1, style: RowsCSS_1, rowspan: 1 },
                                            { text: Layer3_Values.StudentdSend, style: RowsCSS_1, rowspan: 1 },
                                            { text: Layer2_Values.StudentID, style: RowsCSS_1, rowspan: 1 },
                                            { text: Layer3_Values.StudentTitle + Layer3_Values.StudentFullName, style: RowsCSS_1_left, rowspan: 1 },
                                            { text: (Layer3_Values.StudentBalance).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_1, rowspan: 1 }
                                        ]
                                    }));

                            } else {

                                $("#" + table_name + " tbody").append(
                                    RenderRows({
                                        rowtype: "row",
                                        data: [
                                            { text: Layer2_Index + 1, style: RowsCSS_1, rowspan: 1 },
                                            { text: Layer3_Values.StudentdSend, style: RowsCSS_1, rowspan: 1 },
                                            { text: Layer2_Values.StudentID, style: RowsCSS_1, rowspan: 1 },
                                            { text: Layer3_Values.StudentTitle + Layer3_Values.StudentFullName, style: RowsCSS_1_left, rowspan: 1 },
                                            { text: (Layer3_Values.StudentBalance).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_1, rowspan: 1 }
                                        ]
                                    }));
                            }

                        });

                    });

                    $("#" + table_name + " tbody").append(
                        RenderRows({
                            rowtype: "row",
                            data: [
                                { text: "จำนวนรวม", style: RowsCSS_SUM2, colspan: 5 },
                                { text: (SumBalance).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_SUM1, rowspan: 1 }
                            ]
                        }));

                });

            });


            //เพิ่มเติม

            $("#" + table_name + " tbody").append(
                RenderRows({
                    rowtype: "row",
                    data: [
                        { text: "รวมทั้งหมด", style: RowsCSS_SUM2, colspan: 5 },
                        { text: (TotalBalance).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_SUM1, rowspan: 1 }
                    ]
                }));



            $("body").mLoading('hide');

        } else if (rEport_tYpe == 1) {


            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 1 },
                    { text: "วันที่", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 1 },
                    { text: "รหัสบุคลากร", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 1 },
                    { text: "ชื่อ - นามสกุล", style: HeaderCSS + (export_file === true ? "width:360px;" : ""), rowspan: 1 },
                    { text: "ยอดเงินคงเหลือ", style: HeaderCSS + (export_file === true ? "width:120px;" : ""), rowspan: 1 }
                ]
            }));


            var EmpLevel0_data = this.Report_Student_Balance.empLevel0s
            var EmSumBalance = 0;
            $.each(EmpLevel0_data, function (EmpLevel0_Index, EmpLevel0_Values) {
                var EmpLevel1_data = EmpLevel0_Values.empLevel1s;

                $.each(EmpLevel1_data, function (EmpLevel1_Index, EmpLevel1_Values) {
                    EmSumBalance += EmpLevel1_Values.EmpBalance;

                    $("#" + table_name + " tbody").append(RenderRows({
                        rowtype: "row",
                        data: [
                            { text: EmpLevel0_Index + 1, style: RowsCSS_1, rowspan: 1 },
                            { text: EmpLevel1_Values.EmpdSend, style: RowsCSS_1, rowspan: 1 },
                            { text: EmpLevel1_Values.EmpID, style: RowsCSS_1, rowspan: 1 },
                            { text: EmpLevel1_Values.EmpTitle + EmpLevel1_Values.EmpFullName, style: RowsCSS_1_left, rowspan: 1 },
                            { text: (EmpLevel1_Values.EmpBalance).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_1, rowspan: 1 }
                        ]
                    }));
                });
            });

            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: "จำนวนรวม", style: RowsCSS_SUM2, colspan: 4 },
                    { text: (EmSumBalance).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_SUM1, rowspan: 1 }
                ]
            }));

            $("body").mLoading('hide');

        } else if (rEport_tYpe == 3) {

            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: "วันที่", style: HeaderCSS, rowspan: 1 },
                    { text: "ยอดเติม", style: HeaderCSS, rowspan: 1 }, //Topup
                    { text: "ยอดใช้จ่าย", style: HeaderCSS, rowspan: 1 }, //Sales
                    { text: "ยอดถอน", style: HeaderCSS, rowspan: 1 }, //Withdrawal
                    { text: "ยอดคงเหลือ", style: HeaderCSS, rowspan: 1 }, //Final Balance
                ]
            }));

            var userlevel0_data = this.Report_Student_Balance.balanceAmounts;
            $.each(userlevel0_data, function (userlevel0_Index, userlevel0_Values) {
                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row",
                    data: [
                        { text: userlevel0_Values.DateToday, style: RowsCSS },
                        { text: (userlevel0_Values.TotalBalanceTopUp).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_2 },
                        { text: (userlevel0_Values.TotalSales).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_2 },
                        { text: (userlevel0_Values.TotalWithdrawal).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_2 },
                        { text: (userlevel0_Values.FinalBalance).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_2 }
                    ]
                }));

            });

            $("body").mLoading('hide');
        } else if (rEport_tYpe == 4) {

            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: "ลำดับ", style: HeaderCSS, rowspan: 1 },  // SL NO
                    { text: "วันที่", style: HeaderCSS, rowspan: 1 },  // Date
                    { text: "รหัสนักเรียน", style: HeaderCSS, rowspan: 1 }, //Student Code
                    { text: "ชื่อ-สกุล", style: HeaderCSS, rowspan: 1 },  //Full Name
                    { text: "นักเรียน / ครู", style: HeaderCSS, rowspan: 1 }, //Student or Teacher - Type
                    { text: "ยอดเติม", style: HeaderCSS, rowspan: 1 }, //Topup
                    { text: "ยอดใช้จ่าย", style: HeaderCSS, rowspan: 1 }, //Sales
                    { text: "ยอดถอน", style: HeaderCSS, rowspan: 1 }, //Withdrawal
                    { text: "ยอดคงเหลือ", style: HeaderCSS, rowspan: 1 }, //Final Balance
                ]
            }));

            var balanceAmounts_data = this.Report_Student_Balance.balanceAmounts;
            var rowNumber = 1;
            $.each(balanceAmounts_data, function (index,values) {
                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row",
                    data: [
                        { text: rowNumber, style: RowsCSS },
                        { text: values.DateToday, style: RowsCSS },
                        { text: values.StudentID, style: RowsCSS },
                        { text: values.FullName, style: RowsCSS_1_left },
                        { text: values.Type, style: RowsCSS },
                        { text: (values.TotalBalanceTopUp).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_2 },
                        { text: (values.TotalSales).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_2 },
                        { text: (values.TotalWithdrawal).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_2 },
                        { text: (values.FinalBalance).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_2 }
                    ]
                }));
                rowNumber++;
            });

            $("body").mLoading('hide');
        }

    }

    //this.export_excel = function () {
    //    $("body").mLoading();
    //    var dt = new Date();

    //    var file_name = this.Report_Student_Balance.HeaderText + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getDate() + dt.getMinutes() + dt.getSeconds() + ' .xls';
    //    this.RenderHtml_Student_Balance('table_exports', true);

    //    var param = {
    //        "filename": "filename02",
    //        "tabledata": $("#export_excel").html()
    //    };

    //    $.post("/export_excel.aspx", param, function (data) {
    //        downloadFile(file_name, 'data:application/xml;charset=utf-8;base64,', data)
    //    });
    //};

    this.export_excel = function () {
        $("body").mLoading('show');

        var rEport_tYpe = parseInt($('select[id*=report_type').val());

        if (rEport_tYpe == 0) {
            var dt = new Date();
            var json = JSON.stringify({ search: searchBalance });
            var xhr = new XMLHttpRequest();
            var file_name = this.Report_Student_Balance.HeaderText + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getDate() + dt.getMinutes() + dt.getSeconds() + '.xls';

            xhr.open("POST", "/Report/Report_Balance.aspx/export_data", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = "blob";
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                $("body").mLoading('hide');
            };
            xhr.send(json);
        } else if (rEport_tYpe == 1) {
            var dt = new Date();
            var json = JSON.stringify({ search: searchBalance });
            var xhr = new XMLHttpRequest();
            var file_name = this.Report_Student_Balance.EmpHeaderText + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getDate() + dt.getMinutes() + dt.getSeconds() + '.xls';

            xhr.open("POST", "/Report/Report_Balance.aspx/export_data", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = "blob";
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                $("body").mLoading('hide');
            };
            xhr.send(json);
        } else {
            var dt = new Date();
            var json = JSON.stringify({ search: searchBalance });
            var xhr = new XMLHttpRequest();
            var file_name = this.Report_Student_Balance.listHeaderText + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getDate() + dt.getMinutes() + dt.getSeconds() + '.xls';

            xhr.open("POST", "/Report/Report_Balance.aspx/export_data", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = "blob";
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                $("body").mLoading('hide');
            };
            xhr.send(json);
        }


    }


});