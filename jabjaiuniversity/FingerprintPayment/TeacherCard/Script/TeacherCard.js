var Techer_Card = (function () {

    var $table = null;

    this.Student_DesirableFeatues = [];

    var HeaderCSS = "font-weight: bold;text-align: center;text-align: center !important; ";
    var RowsCSS = "text-align: center !important; vertical-align: middle !important;";

    this.RenderHtml_ListDataEmp = function (table_name, export_file) {

        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");

        if (export_file == true) {
            HeaderCSS += ExportCSS;
            RowsCSS += ExportCSS;
        } else {
            HeaderCSS = HeaderCSS.replace(ExportCSS, "");
            RowsCSS = RowsCSS.replace(ExportCSS, "");
        }



        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");




        $("#" + table_name + " thead").append(
            RenderRows({
                rowtype: "row",
                data: [
                    { text: "ลำดับ", style: HeaderCSS, rowspan: 1 },
                    { text: "ประเภทบุคลากร", style: HeaderCSS, rowspan: 1 },
                    { text: "รหัสพนักงาน", style: HeaderCSS, rowspan: 1 },
                    { text: "ชื่อ - นามสกุล", style: HeaderCSS, rowspan: 1 },
                    {
                        text: "<button type='button' class='btn btn-success' onclick='Modelsetting()'><i class='material-icons'>settings</i>" + 'ตั้งค่ารูปแบบบัตร SB' + "</button>"  + "<br>" + "<button type='button' class='btn btn-warning' onclick='PrintALL()'><i class='material-icons'>print</i>" + '&nbsp;พิมพ์บัตรบุคลากรทั้งหมด' + "</button>", style: HeaderCSS, rowspan: 1
                    },
                    {
                        text: '<button type="button" class="btn btn-warning reportbuilder" data-toggle="modal" data-target="#myModal2"><i class="material-icons">settings</i>&nbsp;' + "ตั้งค่ารูปแบบบัตรโรงเรียน" + '</button>' +
                            '<br>' +
                            '<button type="button" class="btn btn-success reportbuilder" onclick="PrintAll2()" ><i class="material-icons">print</i>&nbsp;พิมพ์บัตรบุคลากรทั้งหมด</button>', style: "width: 20%; font-weight: bold; ", class: "text-center td-reportbuilder", rowspan: 1
                    }
                ]
            }));

             
        var dataEmp = this.List_DataEmp.empLists;

        $.each(dataEmp, function (data_Index, data_Values) {
            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row", data: [
                    { text: data_Index + 1, style: RowsCSS, rowspan: 1 },
                    { text: data_Values.EmpType, style: RowsCSS, rowspan: 1 },
                    { text: data_Values.Code, style: RowsCSS, rowspan: 1 },
                    { text: data_Values.EmpFullName, style: RowsCSS, rowspan: 1 },
                    { text: "<button type='button' class='btn btn-warning' onclick='PrintID(" + data_Values.EmpID + ")'><i class='material-icons'>print</i>" + 'พิมพ์บัตรบุคลากร', style: RowsCSS },
                    { text: '<button type="button" class="btn btn-success reportbuilder" onclick="PrintID2(' + data_Values.EmpID + ')"><i class="material-icons">print</i>&nbsp;' + "พิมพ์บัตรบุคลากร" + '</button>', class: "text-center td-reportbuilder", style: RowsCSS, rowspan: 1 },
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

        //$("body").mLoading('hide');

    };




});