var RPAvgClassRoomStudent = (function () {
    this.Report_AVGStudent_ClassRoom = [];
    var HeaderCSS = "font-weight: bold;text-align: center;color: rgb(255, 255, 255);text-align: center !important; background-color: rgb(51, 122, 183);";
    var RowsCSS = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_1 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: top !important;padding:0px;";
    var RowsCSS_2 = "background-color: rgb(255, 255, 255);text-align: right !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_4 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: top !important;padding:0px;background-color:#eee;";
    var RowsCSS_Total = "background-color: rgb(255, 255, 255);font-weight: bold; text-align: center !important; vertical-align: top !important;padding:0px;background-color:#eee;";

    this.RenderHtml_AVGStudent_ClassRoom = function (table_name, export_file) {
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS;
            RowsCSS_1 += ExportCSS;
            RowsCSS_4 += ExportCSS;
            RowsCSS_Total += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            RowsCSS_4 = RowsCSS_4.replace(ExportCSS, "");
            RowsCSS_Total = RowsCSS_Total.replace(ExportCSS, "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");


        $("#" + table_name + " tbody").append(
            RenderRows({
                rowtype: "row",
                data: [
                    { text: "ชั้นเรียน", style: HeaderCSS, rowspan: 1 },
                    { text: "ชาย", style: HeaderCSS, rowspan: 1 },
                    { text: "หญิง", style: HeaderCSS, rowspan: 1 },
                    { text: "จำนวนนักเรียนรวม", style: HeaderCSS, rowspan: 1 },
                    { text: "จำนวนห้อง", style: HeaderCSS, rowspan: 1 },
                    { text: "เฉลี่ยต่อห้อง", style: HeaderCSS, rowspan: 1 },
                ]
            }));

        var TotalMale = 0, TotalFemale = 0, TotalMaleFemale = 0;

        var Layer0_data = this.Report_AVGStudent_ClassRoom.LaYer0s;
        $.each(Layer0_data, function (Layer0_Index, Layer0_Value) {

            var SumMale = 0, SumFemale = 0, SumMaleFemale = 0, SumRoom = 0;

            var Layer1_data = Layer0_Value.LaYer1s;
            $.each(Layer1_data, function (Layer1_Index, Layer1_Value) {

                var AVG_Room = (Layer1_Value.CountMaleFemale / Layer1_Value.CountRoom);

                SumMale += Layer1_Value.CountMale;
                SumFemale += Layer1_Value.CountFemale;
                SumMaleFemale += Layer1_Value.CountMaleFemale;
                SumRoom += Layer1_Value.CountRoom;

                TotalMale += Layer1_Value.CountMale;
                TotalFemale += Layer1_Value.CountFemale;
                TotalMaleFemale += Layer1_Value.CountMaleFemale;

                $("#" + table_name + " tbody").append(
                    RenderRows({
                        rowtype: "row",
                        data: [
                            { text: Layer1_Value.ClassRoomName, style: RowsCSS_1, rowspan: 1 },
                            { text: Layer1_Value.CountMale, style: RowsCSS_1, rowspan: 1 },
                            { text: Layer1_Value.CountFemale, style: RowsCSS_1, rowspan: 1 },
                            { text: Layer1_Value.CountMaleFemale, style: RowsCSS_1, rowspan: 1 },
                            { text: Layer1_Value.CountRoom, style: RowsCSS_1, rowspan: 1 },
                            { text: (AVG_Room).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'), style: RowsCSS_1, rowspan: 1 },
                        ]
                    }));
            });

            $("#" + table_name + " tbody").append(
                RenderRows({
                    rowtype: "row",
                    data: [
                        { text: "รวม" + Layer0_Value.FacultyclassName, style: RowsCSS_4, rowspan: 1 },
                        { text: SumMale, style: RowsCSS_4, rowspan: 1 },
                        { text: SumFemale, style: RowsCSS_4, rowspan: 1 },
                        { text: SumMaleFemale, style: RowsCSS_4, rowspan: 1 },
                        { text: SumRoom, style: RowsCSS_4, rowspan: 1 },
                        { text: "-", style: RowsCSS_4, rowspan: 1 },
                    ]
                }));
        });

        $("#" + table_name + " tbody").append(
            RenderRows({
                rowtype: "row",
                data: [
                    { text: "รวมจำนวนนักเรียนทั้งหมด", style: RowsCSS_Total, rowspan: 1 },
                    { text: TotalMale, style: RowsCSS_Total, rowspan: 1 },
                    { text: TotalFemale, style: RowsCSS_Total, rowspan: 1 },
                    { text: TotalMaleFemale, style: RowsCSS_Total, rowspan: 1 },
                    { text: "", style: RowsCSS_Total, rowspan: 1 },
                    { text: "", style: RowsCSS_Total, rowspan: 1 },
                ]
            }));

        $("body").mLoading('hide');

    }


    this.export_excel = function () {
        $("body").mLoading('hide');
        var dt = new Date();
        var json = JSON.stringify({ search: searchAvgClassRoom });
        var xhr = new XMLHttpRequest();
        var file_name = this.Report_AVGStudent_ClassRoom.HeaderText + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getDate() + dt.getMinutes() + dt.getSeconds() + '.xls';

        xhr.open("POST", "/Report/Report_AVG_ClassRoom.aspx/export_data", true);
        xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
        xhr.responseType = "blob";
        xhr.onload = function () {
            saveAs(xhr.response, file_name);
            $("body").mLoading('hide');
        };
        xhr.send(json);
    }








});