var sTudent_Card = (function () {
    var $table = null;
    this.return_data = [];
    var HeaderCSS = "font-weight: bold;text-align: center;text-align: center !important;";
    var RowsCSS = "text-align: center !important; vertical-align: middle !important;";
    var RowsCSS_1 = "text-align: center !important; vertical-align: top !important;padding:0px;";
    var RowsCSS_2 = "text-align: right !important; vertical-align: middle !important;padding:0px;";
    var HeaderCSS_2 = "font-weight: bold; text-align: center;text-align: right !important;";

    this.RenderHtml = function (table_name, export_file) {
        $("#" + table_name).html("");
        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");
        if (export_file == true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS;
            RowsCSS_1 += ExportCSS;
            HeaderCSS_2 += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
            RowsCSS_1 = RowsCSS_1.replace(ExportCSS, "");
            HeaderCSS_2 = HeaderCSS_2.replace(ExportCSS, "");
        }

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");

        //$("#" + table_name + " thead").append(RenderRows({
        //    rowtype: "row", data: [
        //        {
        //            text: "<button type='button' class='btn btn-warning' onclick='SentDataCardAll()' >พิมพ์บัตรทั้งห้อง</button>", style: HeaderCSS_2, colspan: 4
        //        }
        //    ]
        //}));

        $("#" + table_name + " thead").append(RenderRows({
            rowtype: "header", data: [
                {
                    text: "ลำดับ", style: HeaderCSS + (export_file === true ? "width:80px;" : ""), rowspan: 1
                }
                ,
                {
                    text: "รหัสนักเรียน", style: HeaderCSS + (export_file === true ? "width:250px;" : ""), rowspan: 1
                }
                ,
                {
                    text: "ชื่อ - นามสกุล", style: HeaderCSS + (export_file === true ? "width:300px;" : ""), rowspan: 1
                }
                ,
                {
                    text: "<button type='button' class='btn btn-success' onclick='Modelsetting()'>ตั้งค่าการพิมพ์</button><button type='button' class='btn btn-warning' onclick='SentDataCardAll()' >พิมพ์บัตรทั้งห้อง</button>", style: HeaderCSS, rowspan: 1
                }
            ]
        }));

        $.each(this.return_data.data, function (data_Index, data_Values) {
            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row", data: [
                    { text: data_Index + 1, style: RowsCSS },
                    { text: data_Values.StudentID, style: RowsCSS },
                    { text: data_Values.StudentFullName, style: RowsCSS }
                    ,
                    {
                        text: "<button type='button' class='btn btn-warning' sID=" + data_Values.s_ID + " onclick='SentDataCardId(" + data_Values.s_ID + ")' >" + 'พิมพ์บัตรรายบุคคล' + "</button>", style: RowsCSS
                    }
                ]
            }));
        });


        if ($.fn.DataTable.isDataTable('#example')) {
            //$('#datatable2').dataTable();
            //$table.clear();
            $table.destroy();
        }

        //$("body").mLoading('hide');
        $table = $("#example").DataTable({
            paging: true,
            "pageLength": 20,
            "bLengthChange": false,
            searching: false,
            info: false,
        });
    };



});