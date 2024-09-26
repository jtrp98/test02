var Student_Card = (function () {

    var HeadClass = "centertext";

    var HeadNumberCSS = "width: 10%; font-weight: bold; ";
    var HeadStudenIdCSS = "width: 15%; font-weight: bold;";
    var HeadFullnameCSS = "width: 20%; font-weight: bold; ";
    var HeadSettingCSS = "width: 30%; font-weight: bold; ";

    var RowsCSS = "vertical-align: middle !important;";

    this.RenderHtml_ListDataStudent = function (table_name) {

        //var load = document.getElementsByClassName("load");

        var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
        $("#" + table_name).html("");

        $("#" + table_name).append("<thead>");
        $("#" + table_name).append("<tbody>");
        $("#" + table_name).append("<tfoot>");


        $("#" + table_name + " thead").append(RenderRows({
            rowtype: "row",
            data: [
                { text: 'ลำดับ', style: HeadNumberCSS, class: HeadClass, rowspan: 1 , },
                { text: 'รหัสนักเรียน', style: HeadStudenIdCSS, class: HeadClass, rowspan: 1 },
                { text: 'ชื่อ - นามสกุล', style: HeadFullnameCSS, class: HeadClass, rowspan: 1 },
                {
                    text: '<button type="button" class="btn btn-success" style="width: 200px"  data-toggle="modal" data-target="#myModal"><i class="material-icons">settings</i>&nbsp;ตั้งค่ารูปแบบบัตร SB&nbsp;</button>' +
                        '<br><button type="button" class="btn btn-warning" onclick="nextpage()" ><i class="material-icons">print</i>&nbsp;พิมพ์บัตรนักเรียนทั้งหมด</button>'
                      , style: HeadSettingCSS, class: "text-center", rowspan: 1
                },
                {
                    text: '<button type="button" class="btn btn-warning reportbuilder" data-toggle="modal" data-target="#myModal2"><i class="material-icons">settings</i>&nbsp;' + "ตั้งค่ารูปแบบบัตรโรงเรียน" + '</button>' +
                        '<br>' +
                        '<button type="button" class="btn btn-success reportbuilder" onclick="PrintAll2()" ><i class="material-icons">print</i>&nbsp;พิมพ์บัตรนักเรียนทั้งหมด</button>', style: "width: 20%; font-weight: bold; ", class: "text-center td-reportbuilder", rowspan: 1
                }
            ]
        }));

        var dataStudent = this.List_DataStudent.Studentlists;
        $.each(dataStudent, function (index, values) {
            $("#" + table_name + " tbody").append(RenderRows({
                rowtype: "row",
                data: [
                    { text: index + 1, class: HeadClass, style: RowsCSS, rowspan: 1 },
                    { text: values.studentID, class: HeadClass, style: RowsCSS, rowspan: 1 },
                    { text: values.fullname, class: HeadClass, style: RowsCSS, rowspan: 1 },
                    {
                        text: '<button type="button" class="btn btn-warning" onclick="SentStudentData(' + values.sid + ')"><i class="material-icons">print</i>&nbsp;' + "พิมพ์บัตรนักเรียน" + '</button>', class: "text-center", style: RowsCSS, rowspan: 1
                    },
                    {
                        text: '<button type="button" class="btn btn-success reportbuilder" onclick="SentStudentData2(' + values.sid + ')"><i class="material-icons">print</i>&nbsp;' + "พิมพ์บัตรนักเรียน" + '</button>', class: "text-center td-reportbuilder", style: RowsCSS, rowspan: 1
                    }
                ]
            }));
        });

        //load[0].classList.add('hidden');


    };

});



function RenderRows() {
    var RowsHtml = "";
    $.each(arguments, function (index, array_data) {
        RowsHtml += "<tr style=\"" + array_data.style + "\" >";
        $.each(array_data.data, function (index, data) {
            if (data.fun !== undefined) data.fun();
            RowsHtml += "<" + (array_data.rowtype === "header" ? "th" : "td") +
                " colspan='" + (data.colspan === undefined ? 1 : data.colspan) +
                "' rowspan='" + (data.rowspan === undefined ? 1 : data.rowspan) +
                "' class='" + data.class + "' " +
                "' style='" + data.style + "' " +
                (data.row === undefined ? "" : " row='" + data.row + "'") +
                (data.column === undefined ? "" : " column='" + data.column + "'") +
                (data.attribute === undefined ? "" : data.attribute) +
                "> " + data.text;
        });
    });
    return RowsHtml;
}


