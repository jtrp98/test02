$(function () {
    var _type = 0;
    var _html
    if ($("div.lisTUsers.type0") !== []) $("div.listuser").html(genhtml(0));
    if ($("div.lisTUsers.type1") !== []) $("div.listuser").html(genhtml(1));
    if ($("div.lisTUsers.type2") !== []) _type === 2;
})

function genhtml(type) {
    var ArrayHtml = [];
    var html = "";
    if (type === 0) {
        ArrayHtml = [{
            rowcss: "row",
            div1css: 'col-xs-4',
            div1controll: '<label class="pull-right">รูปแบบการส่ง<span style="color: red;">*</span></label>',
            div2css: 'col-xs-8',
            div2controll: '<label class="radio-inline"><input type="radio" name="optradio" id="optradio_0" runat="server" style="position: unset; margin-right: 5px;" value="0" checked>รายชั้น</label>' +
                     '<label class="radio-inline"><input type="radio" name="optradio" id="optradio_1" runat="server" style="position: unset; margin-right: 5px;" value="1">รายบุคคล</label>'
        },
        //{
        //    rowcss: 'row send_type_0',
        //    div1css: 'col-xs-4',
        //    div1controll: '<label class="pull-right">ส่งทุกช่วงชั้น<span style="color: red;"></span></label>',
        //    div2css: 'col-xs-8',
        //    div2controll: ''
        //},
        {
            rowcss: 'row send_type_0',
            div1css: 'container col-xs-12',
            div1controll: '<div id="lvSet"></div><div id="divmain"></div><div id="myTabContent" class="tab-content"></div>'
        }, {
            rowcss: "row hidden send_type_1",
            div1css: 'col-xs-4',
            div1controll: '<label class="pull-right">ประเภท<span style="color: red;"></span></label>',
            div2css: "col-xs-8",
            div2controll: '<select name="selecttype" class="form-control">' +
                    '<option value="0">นักเรียน</option>' +
                    '<option value="1">พนักงาน</option>' +
                '</select>'
        }, {
            rowcss: "row hidden send_type_1 user_type_1",
            div1css: 'col-xs-4',
            div1controll: '<label class="pull-right">ระดับชั้นเรียน :<span style="color: red;"></span></label>',
            div2css: 'col-xs-8',
            div2controll: '<select name="selectlevel" class="form-control dropdown"></select>'
        }, {
            rowcss: 'row hidden send_type_1 user_type_1',
            div1css: 'col-xs-4',
            div1controll: '<label class="pull-right">ชั้นเรียน :<span style="color: red;"></span></label>',
            div2css: 'col-xs-8',
            div2controll: ' <select name="selectsublevel" class="form-control"></select>',
        }, {
            rowcss: "row hidden send_type_1",
            div1css: 'col-xs-4',
            div1controll: '<label class="pull-right">ชื่อ - นามสกุล :<span style="color: red;"></span></label>',
            div2css: 'col-xs-5',
            div2controll: '<input type="text" name="sname" style="width: 300px;" class="form-control" /><input type="hidden" name="iduser" style="width: 300px;"  class="form-control" /> ',
            div3css: 'col-xs-3',
            div3controll: '<input type="button" id="btnaddlistuser" class="btn btn-success" style="width: 100px;" value="เพิ่ม" />'
        }, {
            rowcss: 'row hidden send_type_1',
            div1css: 'col-xs-12',
            div1controll: '<table id="tablelistuser" class="table table-striped col-lg-12 ">' +
                    '<thead class="table-tab">' +
                        '<td style="width: 60%;">ชื่อ - นามสกุล</td>' +
                        '<td style="width: 20%;">ประเภท</td>' +
                        '<td style="width: 20%;">ระดับ</td>' +
                    '</thead>' +
                    '<tbody style="height: 400px; overflow-y: scroll;">' +
                    '</tbody>' +
                '</table>',
        }];


    }
    else if (type === 1) {
        ArrayHtml = [{
            rowcss: "row",
            div1css: 'col-xs-4',
            div1controll: '<label class="pull-right">รูปแบบการส่ง<span style="color: red;">*</span></label>',
            div2css: 'col-xs-8',
            div2controll: '<label class="radio-inline"><input type="radio" name="optradio" id="optradio_0" runat="server" style="position: unset; margin-right: 5px;" value="0" checked>รายชั้น</label>' +
                     '<label class="radio-inline"><input type="radio" name="optradio" id="optradio_1" runat="server" style="position: unset; margin-right: 5px;" value="1">รายบุคคล</label>'
        },
        {
            rowcss: 'row send_type_0',
            div1css: 'container col-xs-12',
            div1controll: '<div id="lvSet"></div><div id="divmain"></div><div id="myTabContent" class="tab-content">Loaging . . .</div>'
        }, {
            rowcss: "row hidden send_type_1 user_type_1",
            div1css: 'col-xs-4',
            div1controll: '<label class="pull-right">ระดับชั้นเรียน :<span style="color: red;"></span></label>',
            div2css: 'col-xs-8',
            div2controll: '<select name="selectlevel" class="form-control dropdown"></select>'
        }, {
            rowcss: 'row hidden send_type_1 user_type_1',
            div1css: 'col-xs-4',
            div1controll: '<label class="pull-right">ชั้นเรียน :<span style="color: red;"></span></label>',
            div2css: 'col-xs-8',
            div2controll: ' <select name="selectsublevel" class="form-control"></select>',
        }, {
            rowcss: "row hidden send_type_1",
            div1css: 'col-xs-4',
            div1controll: '<label class="pull-right">ชื่อ - นามสกุล :<span style="color: red;"></span></label>',
            div2css: 'col-xs-5',
            div2controll: '<input type="text" name="sname" style="width: 300px;" class="form-control" /><input type="hidden" name="iduser" style="width: 300px;"  class="form-control" /> ',
            div3css: 'col-xs-3',
            div3controll: '<input type="button" id="btnaddlistuser" class="btn btn-success" style="width: 100px;" value="เพิ่ม" />'
        }, {
            rowcss: 'row hidden send_type_1',
            div1css: 'col-xs-12',
            div1controll: '<table id="tablelistuser" class="table table-striped col-lg-12 ">' +
                    '<thead class="table-tab">' +
                        '<td style="width: 60%;">ชื่อ - นามสกุล</td>' +
                        '<td style="width: 20%;">ประเภท</td>' +
                        '<td style="width: 20%;">ระดับ</td>' +
                    '</thead>' +
                    '<tbody style="height: 400px; overflow-y: scroll;">' +
                    '</tbody>' +
                '</table>',
        }];
    }

    $.each(ArrayHtml, function (index) {
        html += '<div class="' + ArrayHtml[index].rowcss + '">';
        if (ArrayHtml[index].div1css !== undefined) {
            html += '<div class="' + ArrayHtml[index].div1css + '">' + ArrayHtml[index].div1controll + '</div>';
        }
        if (ArrayHtml[index].div2css !== undefined) {
            html += '<div class="' + ArrayHtml[index].div2css + '">' + ArrayHtml[index].div2controll + '</div>';
        }
        if (ArrayHtml[index].div3css !== undefined) {
            html += '<div class="' + ArrayHtml[index].div3css + '">' + ArrayHtml[index].div3controll + '</div>';
        }
        html += "</div>"
    });
    return html;
}