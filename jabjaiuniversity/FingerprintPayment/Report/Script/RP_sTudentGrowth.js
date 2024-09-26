var sTudentGrowth = (function () {
    this.reports_Growth = [];
    var HeaderCSS = "font-weight: bold;text-align: center;color: rgb(255, 255, 255);text-align: center !important; background-color: rgb(51, 122, 183);";
    var RowsCSS1111 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px; width:120px;";
    var RowsCSS = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_1 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: top !important;padding:0px;";
    var RowsCSS_2 = "background-color: rgb(255, 255, 255);text-align: right !important; vertical-align: middle !important;padding:0px;";
    var RowsCSS_3 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: middle !important; padding-left:0px; padding-top:1px; padding-bottom:0px; padding-right:7px; background-color:#eee;";
    var RowsCSS_4 = "background-color: rgb(255, 255, 255);text-align: center !important; vertical-align: top !important;padding:0px;background-color:#eee;";

    this.RenderHtml_Growth = function (table_name, export_file) {
        $("#" + table_name).html("");
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file === true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS;
            RowsCSS_1 += ExportCSS;
            RowsCSS_2 += ExportCSS;
            RowsCSS_3 += ExportCSS;
            RowsCSS_4 += ExportCSS;
            RowsCSS1111 += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            RowsCSS_2 = RowsCSS_2.replace(ExportCSS, "");
            RowsCSS_3 = RowsCSS_3.replace(ExportCSS, "");
            RowsCSS_4 = RowsCSS_4.replace(ExportCSS, "");
            RowsCSS1111 = RowsCSS1111.replace(ExportCSS, "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        $("#" + table_name + " tbody").append(
            RenderRows(
                {
                    rowtype: "row",
                    data: [
                        { text: "ระดับชั้น", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 4, colspan: 1 },
                        { text: "นักเรียนทั้งหมด(คน)", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 4, colspan: 1 },
                        { text: "จำนวนนักเรียนที่ได้รับการชั่งน้ำหนักและวัดส่วนสูง", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 3, colspan: 3 },
                        {
                            text: "เกณฑ์น้ำหนักตามเกณฑ์ส่วนสูง" + "<button type='button' class='btn btn-primary glyphicon glyphicon-question-sign' onclick='showModalBMI()' >",
                            style: HeaderCSS + (export_file === true ? "width:70px;" : ""), colspan: 12
                        },
                        {
                            text: "เกณฑ์ส่วนสูงตามเกณฑ์อายุ" + "<button type='button' class='btn btn-primary glyphicon glyphicon-question-sign' onclick='showModalHight()' >",
                            style: HeaderCSS + (export_file === true ? "width:70px;" : ""), colspan: 10
                        },
                        { text: "ส่วนสูงระดับดีและรูปร่างสมส่วนในเด็กวัยเดียวกัน", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 3, colspan: 3 }

                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        { text: "ผอม", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 2, colspan: 2 },
                        { text: "ค่อนข้างผอม", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 2, colspan: 2 },
                        { text: "สมส่วน", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 2, colspan: 2 },
                        { text: "ท้วม", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 2, colspan: 2 },
                        { text: "ภาวะอ้วน", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), colspan: 4 },
                        { text: "เตี้ย", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 2, colspan: 2 },
                        { text: "ค่อนข้างเตี้ย", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), rowspan: 2, colspan: 2 },
                        { text: "ส่วนสูงระดับดี", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), colspan: 6 }
                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        { text: "เริ่มอ้วน", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), colspan: 2 },
                        { text: "อ้วน", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), colspan: 2 },
                        { text: "สูงตามเกณฑ์", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), colspan: 2 },
                        { text: "ค่อนข้างสูง", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), colspan: 2 },
                        { text: "สูงกว่าเกณฑ์", style: HeaderCSS + (export_file === true ? "width:70px;" : ""), colspan: 2 }
                    ]
                },
                {
                    rowtype: "row",
                    data: [
                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "รวม", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },

                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },

                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },

                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },

                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },

                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },

                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },

                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },

                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },

                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },

                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },

                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },

                        { text: "ชาย", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "หญิง", style: HeaderCSS + (export_file === true ? "width:50px;" : "") },
                        { text: "รวม", style: HeaderCSS + (export_file === true ? "width:50px;" : "") }
                    ]
                }
            ));

        var Layer0_data = this.reports_Growth.Layer0s;
        $.each(Layer0_data, function (Layer0_Index, Layer0_Values) {

            //จำนวนรวมนักเรียนที่ได้รับการชั่งน้ำหนัก
            var Total_Male_Acep = 0, Total_Female_Acep = 0, Total_MF_Acep = 0;

            //จำนวนรวมนักเรียนทั้งหมด
            var total_mf = 0;

            //เกณฑ์น้ำหนัก
            var TotalMaleThin = 0, TotalFemaleThin = 0;
            var TotalMaleSkinny = 0, TotalFemaleSkinny = 0;
            var TotalMaleShapely = 0, TotalFemaleShapely = 0;
            var TotalMalePlump = 0, TotalFemalePlump = 0;
            var TotalMaleChubby = 0, TotalFemaleChubby = 0;
            var TotalMaleFat = 0, TotalFemaleFat = 0;

            //เกณฑ์ส่วนสูง
            var TotalMaleShort = 0, TotalFemaleShort = 0;
            var TotalMaleMediumShort = 0, TotalFemaleMediumShort = 0;
            var TotalMaleNormal = 0, TotalFemaleNormal = 0;
            var TotalMaleMediumTall = 0, TotalFemaleMediumTall = 0;
            var TotalMaleTall = 0, TotalFemaleTall = 0;

            //เปอเซนเตี้ยและค่อนข้างเตี้ย
            var total_mf_low = 0, percen_mf_low = 0;
            var total_mf_skinny = 0, percen_mf_skinny = 0;
            var total_mf_chubby_fat = 0, percen_mf_chubby_fat = 0;

            var percen_fm_fm = 0;

            var total_sum_mm = 0, total_sum_ff = 0, total_sum_ffmm = 0;
            var sum_mm = 0; var sum_ff = 0; var sum_ffmm = 0;

            var Layer1_data = Layer0_Values.Layer1s;
            $.each(Layer1_data, function (Layer1_Index, Layer1_Values) {

                //จำนวนรวมนักเรียนทั้งหมด
                total_mf += Layer1_Values.SumStudent;

                //นักเรียนที่ได้รับการชั่งน้ำหนัก
                var Male_Acep = Layer1_Values.Male_Acep;
                var Female_Acep = Layer1_Values.Female_Acep;
                var Sum_MF_Acep = (Layer1_Values.Male_Acep + Layer1_Values.Female_Acep);
                //จำนวนรวมนักเรียนที่ได้รับการชั่งน้ำหนัก
                Total_Male_Acep += Male_Acep;
                Total_Female_Acep += Female_Acep;
                Total_MF_Acep += Sum_MF_Acep;

                //รวมนักเรียนที่สูงและสมส่วนในเวลาเดียวกัน
                sum_mm = (Layer1_Values.MaleShapAnNormal + Layer1_Values.MaleShapAnMediumTall + Layer1_Values.MaleShapAnTall);
                sum_ff = (Layer1_Values.FemaleShapAnNormal + Layer1_Values.FemaleShapAnMediumTall + Layer1_Values.FemaleShapAnTall);
                sum_ffmm = (sum_mm + sum_ff);

                //เกณฑ์น้ำหนัก
                TotalMaleThin += Layer1_Values.CountMaleThin;/*ผอม*/
                TotalFemaleThin += Layer1_Values.CountFemaleThin;
                TotalMaleSkinny += Layer1_Values.CountMaleSkinny;/*ค่อนข้างผอม*/
                TotalFemaleSkinny += Layer1_Values.CountFemaleSkinny;
                TotalMaleShapely += Layer1_Values.CountMaleShapely;/*สมส่วน*/
                TotalFemaleShapely += Layer1_Values.CountFemaleShapely;
                TotalMalePlump += Layer1_Values.CountMalePlump;/*ท้วม*/
                TotalFemalePlump += Layer1_Values.CountFemalePlump;
                TotalMaleChubby += Layer1_Values.CountMaleChubby;/*เริ่มอ้วน*/
                TotalFemaleChubby += Layer1_Values.CountFemaleChubby;
                TotalMaleFat += Layer1_Values.CountMaleFat;/*อ้วน*/
                TotalFemaleFat += Layer1_Values.CountFemaleFat;

                //เกณฑ์ส่วนสูง
                TotalMaleShort += Layer1_Values.CountMaleShort;
                TotalFemaleShort += Layer1_Values.CountFemaleShort;
                TotalMaleMediumShort += Layer1_Values.CountMaleMediumShort;
                TotalFemaleMediumShort += Layer1_Values.CountFemaleMediumShort;
                TotalMaleNormal += Layer1_Values.CountMaleNormal;
                TotalFemaleNormal += Layer1_Values.CountFemaleNormal;
                TotalMaleMediumTall += Layer1_Values.CountMaleMediumTall;
                TotalFemaleMediumTall += Layer1_Values.CountFemaleMediumTall;
                TotalMaleTall += Layer1_Values.CountMaleTall;
                TotalFemaleTall += Layer1_Values.CountFemaleTall;

                total_sum_mm += sum_mm;
                total_sum_ff += sum_ff;
                total_sum_ffmm += sum_ffmm;

                //เปอเซนเตี้ยและค่อนข้างเตี้ย
                total_mf_low = TotalMaleShort + TotalFemaleShort + TotalMaleMediumShort + TotalFemaleMediumShort;
                percen_mf_low = (total_mf_low / Total_MF_Acep * 100).toFixed(2);
                //ผอมและค่อนข้างผอม
                total_mf_skinny = TotalMaleThin + TotalFemaleThin + TotalMaleSkinny + TotalFemaleSkinny;
                percen_mf_skinny = (total_mf_skinny / Total_MF_Acep * 100).toFixed(2);
                //เริ่มอ้วนและอ้วน
                total_mf_chubby_fat = TotalMaleChubby + TotalFemaleChubby + TotalMaleFat + TotalFemaleFat;
                percen_mf_chubby_fat = (total_mf_chubby_fat / Total_MF_Acep * 100).toFixed(2);
                //รวมนักเรียนที่สูงและสมส่วนในเวลาเดียวกัน
                percen_fm_fm = (total_sum_ffmm / Total_MF_Acep * 100).toFixed(2);

                $("#" + table_name + " tbody").append(RenderRows({
                    rowtype: "row",
                    data: [
                        { text: Layer1_Values.ClassFullName, style: RowsCSS1111 },
                        { text: Layer1_Values.SumStudent, style: RowsCSS },
                        { text: Male_Acep, style: RowsCSS },
                        { text: Female_Acep, style: RowsCSS },
                        { text: Sum_MF_Acep, style: RowsCSS },

                        { text: Layer1_Values.CountMaleThin, style: RowsCSS }, /*ผอม*/
                        { text: Layer1_Values.CountFemaleThin, style: RowsCSS },
                        { text: Layer1_Values.CountMaleSkinny, style: RowsCSS }, /*ค่อข้างผอม*/
                        { text: Layer1_Values.CountFemaleSkinny, style: RowsCSS },
                        { text: Layer1_Values.CountMaleShapely, style: RowsCSS }, /*หุ่นดี*/
                        { text: Layer1_Values.CountFemaleShapely, style: RowsCSS },
                        { text: Layer1_Values.CountMalePlump, style: RowsCSS }, /*ท้วม*/
                        { text: Layer1_Values.CountFemalePlump, style: RowsCSS },
                        { text: Layer1_Values.CountMaleChubby, style: RowsCSS }, /*เริ่มอ้วน*/
                        { text: Layer1_Values.CountFemaleChubby, style: RowsCSS },
                        { text: Layer1_Values.CountMaleFat, style: RowsCSS }, /*อ้วน*/
                        { text: Layer1_Values.CountFemaleFat, style: RowsCSS },

                        { text: Layer1_Values.CountMaleShort, style: RowsCSS }, /*ชเตี้ย*/
                        { text: Layer1_Values.CountFemaleShort, style: RowsCSS }, /*ญเตี้ย*/
                        { text: Layer1_Values.CountMaleMediumShort, style: RowsCSS }, /*ชค่อนข้างเตี้ย*/
                        { text: Layer1_Values.CountFemaleMediumShort, style: RowsCSS }, /*ญค่อนข้างเตี้ย*/
                        { text: Layer1_Values.CountMaleNormal, style: RowsCSS }, /*ชสูงตามเกณฑ์*/
                        { text: Layer1_Values.CountFemaleNormal, style: RowsCSS }, /*ญสูงตามเกณฑ์*/
                        { text: Layer1_Values.CountMaleMediumTall, style: RowsCSS }, /*ชค่อนข้างสูง*/
                        { text: Layer1_Values.CountFemaleMediumTall, style: RowsCSS }, /*ญค่อนข้างสูง*/
                        { text: Layer1_Values.CountMaleTall, style: RowsCSS }, /*ชสูง*/
                        { text: Layer1_Values.CountFemaleTall, style: RowsCSS },

                        { text: sum_mm, style: RowsCSS },
                        { text: sum_ff, style: RowsCSS },
                        { text: sum_ffmm, style: RowsCSS }
                    ]
                }));
            });

            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: "รวม", style: RowsCSS },
                    { text: total_mf, style: RowsCSS },
                    { text: Total_Male_Acep, style: RowsCSS },
                    { text: Total_Female_Acep, style: RowsCSS },
                    { text: Total_MF_Acep, style: RowsCSS },

                    { text: TotalMaleThin, style: RowsCSS },
                    { text: TotalFemaleThin, style: RowsCSS },
                    { text: TotalMaleSkinny, style: RowsCSS },
                    { text: TotalFemaleSkinny, style: RowsCSS },
                    { text: TotalMaleShapely, style: RowsCSS },
                    { text: TotalFemaleShapely, style: RowsCSS },
                    { text: TotalMalePlump, style: RowsCSS },
                    { text: TotalFemalePlump, style: RowsCSS },
                    { text: TotalMaleChubby, style: RowsCSS },
                    { text: TotalFemaleChubby, style: RowsCSS },
                    { text: TotalMaleFat, style: RowsCSS },
                    { text: TotalFemaleFat, style: RowsCSS },

                    { text: TotalMaleShort, style: RowsCSS },
                    { text: TotalFemaleShort, style: RowsCSS },
                    { text: TotalMaleMediumShort, style: RowsCSS },
                    { text: TotalFemaleMediumShort, style: RowsCSS },
                    { text: TotalMaleNormal, style: RowsCSS },
                    { text: TotalFemaleNormal, style: RowsCSS },
                    { text: TotalMaleMediumTall, style: RowsCSS },
                    { text: TotalFemaleMediumTall, style: RowsCSS },
                    { text: TotalMaleTall, style: RowsCSS },
                    { text: TotalFemaleTall, style: RowsCSS },

                    { text: total_sum_mm, style: RowsCSS },
                    { text: total_sum_ff, style: RowsCSS },
                    { text: total_sum_ffmm, style: RowsCSS }
                ]
            }));
            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: "เตี้ยและค่อนข้างเตี้ย", colspan: 3, style: RowsCSS },
                    { text: "จำนวน", colspan: 2, style: RowsCSS },
                    { text: total_mf_low, colspan: 2, style: RowsCSS },
                    { text: "คน", colspan: 2, style: RowsCSS },
                    { text: "", colspan: 2, style: RowsCSS_4 },
                    { text: "คิดเป็นร้อยละ", colspan: 3, style: RowsCSS_4 },
                    { text: percen_mf_low, colspan: 2, style: RowsCSS_4 },
                    { text: "(ไม่เกินร้อยละ 10)", colspan: 4, style: RowsCSS_3 },
                    { text: "", colspan: 10, style: RowsCSS_3 }
                ]
            }));
            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: "ผอมและค่อนข้างผอม", colspan: 3, style: RowsCSS },
                    { text: "จำนวน", colspan: 2, style: RowsCSS },
                    { text: total_mf_skinny, colspan: 2, style: RowsCSS },
                    { text: "คน", colspan: 2, style: RowsCSS },
                    { text: "", colspan: 2, style: RowsCSS_4 },
                    { text: "คิดเป็นร้อยละ", colspan: 3, style: RowsCSS_4 },
                    { text: percen_mf_skinny, colspan: 2, style: RowsCSS_4 },
                    { text: "(ไม่เกินร้อยละ 10)", colspan: 4, style: RowsCSS_3 },
                    { text: "", colspan: 10, style: RowsCSS_3 }
                ]
            }));
            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: "เริ่มอ้วนและอ้วน", colspan: 3, style: RowsCSS },
                    { text: "จำนวน", colspan: 2, style: RowsCSS },
                    { text: total_mf_chubby_fat, colspan: 2, style: RowsCSS },
                    { text: "คน", colspan: 2, style: RowsCSS },
                    { text: "", colspan: 2, style: RowsCSS_4 },
                    { text: "คิดเป็นร้อยละ", colspan: 3, style: RowsCSS_4 },
                    { text: percen_mf_chubby_fat, colspan: 2, style: RowsCSS_4 },
                    { text: "(ไม่เกินร้อยละ 10)", colspan: 4, style: RowsCSS_3 },
                    { text: "", colspan: 10, style: RowsCSS_3 }
                ]
            }));
            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: "หุ่นดีและสมส่วนในคนเดียวกัน", colspan: 3, style: RowsCSS },
                    { text: "จำนวน", colspan: 2, style: RowsCSS },
                    { text: total_sum_ffmm, colspan: 2, style: RowsCSS },
                    { text: "คน", colspan: 2, style: RowsCSS },
                    { text: "", colspan: 2, style: RowsCSS_4 },
                    { text: "คิดเป็นร้อยละ", colspan: 3, style: RowsCSS_4 },
                    { text: percen_fm_fm, colspan: 2, style: RowsCSS_4 },
                    { text: "(ร้อยละ 66 ขึ้นไป)", colspan: 4, style: RowsCSS_3 },
                    { text: "", colspan: 10, style: RowsCSS_3 }
                ]
            }));
        });

        if (export_file === true) {
            $("#" + table_name).append("<tfoot>");
            var Colspan = 30;
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
                        { text: "รายงานภาวะการเจริญเติบโต", colspan: Colspan, style: "font-size:15px;" }
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
        $("body").mLoading();
        var dt = new Date();
        var file_name = 'รายงานภาวะการเจริญเติบโต' + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getDate() + dt.getMinutes() + dt.getSeconds() + ' .xls';

        this.RenderHtml_Growth('table_exports', true);

        var param = {
            "filename": "filename02",
            "tabledata": $("#export_excel").html()
        };
        $.post("/export_excel.aspx", param, function (data) {
            downloadFile(file_name, 'data:application/xml;charset=utf-8;base64,', data)
        });

    };


});



