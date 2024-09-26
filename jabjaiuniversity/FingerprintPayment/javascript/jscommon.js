function isDate(e) {
    /* http://www.w3schools.com/jsref/jsref_onkeypress.asp */
    var keynum;
    var keychar;
    var datecheck;

    if (window.event) // IE
    {
        keynum = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        keynum = e.which;
    }
    keychar = String.fromCharCode(keynum);
    //alert(keychar+' '+keynum);
    datecheck = /\d|[/]/;

    return datecheck.test(keychar) && isCorrectSlashPosition(keychar, e);
}
// เช็คตัวเลข
function isNumber(e, ctrID) {
    var charCode = (e.which) ? e.which : window.event.keyCode;
    var keyChar = String.fromCharCode(charCode);
    var re = /^\d+$/
    return re.test(ctrID.value + keyChar);
}
function isDecimal(e, ctrID) {
    var charCode = (e.which) ? e.which : window.event.keyCode;
    //alert(charCode)
    if (charCode == 8) return true;
    var keyChar = String.fromCharCode(charCode);
    var re = /^\d+$|[.]/
    return re.test(ctrID.value + keyChar);
}
// Validate with CustomValidator & Javascript
// เช็คว่าวันที่ถูกต้องป่าว 28 เดือนที่ 2 ในปีนั้นมีป่าว
function ValidateDate(y, mo, d, h, mi, s) {
    var date = new Date(y, mo - 1, d, h, mi, s, 0);
    var ny = date.getFullYear();
    var nmo = date.getMonth() + 1;
    var nd = date.getDate();
    var nh = date.getHours();
    var nmi = date.getMinutes();
    var ns = date.getSeconds();
    return ny == y && nmo == mo && nd == d && nh == h && nmi == mi && ns == s;
}
function CustomValidatorValidateDate(source, args) {
    var _date = document.getElementById(source.controltovalidate).value;
    var _dmy = _date.split('/');
    args.IsValid = ValidateDate(_dmy[2] - 543, _dmy[1], _dmy[0], 0, 0, 0);
}
// Clear FileUpload
function RemoveFileUpload(control) {
    var who = document.getElementsByName(control)[0];
    who.value = "";
    var who2 = who.cloneNode(false);
    who2.onchange = who.onchange;
    who.parentNode.replaceChild(who2, who);
}
function openwin(sfile, swinid, nwidth, nheight) {
    // sfile = ชื่อไฟล์ที่ต้องการเปิด
    // swinid =	รหัสหน้าต่าง
    //	กรณี รหัสเดิมจะเปิดข้อมูลเดิมในหน้าต่างเดิม
    //	กรณี รหัสใหม่จะเปิดหน้าต่างใหม่ขึ้นมา
    //	กรณี ต้องการให้เปิดหน้าต่างใหม่ทุกครั้งให้ส่ง '_blank' เข้ามาจะเปิดใหม่ทุกครั้ง
    //	กรณี ต้องการเปิดหน้าต่างเดิมทุกครั้ง ให้ส่งค่าที่เป็น อักษรอะไรมาก็ได้ที่ไม่ได้เปลี่ยนแปลงเมื่อส่งมาครั้งใหม่
    var newwin = window.open(sfile, swinid, 'WIDTH=' + nwidth + ',HEIGHT=' + nheight + ',RESIZABLE=yes,SCROLLBARS=yes,STATUS=1,LEFT=0,TOP=0');
    newwin.moveTo(0, 0);
    if (newwin != null) {
        if (newwin.opener == null)
            newwin.opener = self;
    }
}

function opencalenwin(sfile, swinid) {
    // sfile = ชื่อไฟล์ที่ต้องการเปิด
    // swinid =	รหัสหน้าต่าง
    //	กรณี รหัสเดิมจะเปิดข้อมูลเดิมในหน้าต่างเดิม
    //	กรณี รหัสใหม่จะเปิดหน้าต่างใหม่ขึ้นมา
    //	กรณี ต้องการให้เปิดหน้าต่างใหม่ทุกครั้งให้ส่ง '_blank' เข้ามาจะเปิดใหม่ทุกครั้ง
    //	กรณี ต้องการเปิดหน้าต่างเดิมทุกครั้ง ให้ส่งค่าที่เป็น อักษรอะไรมาก็ได้ที่ไม่ได้เปลี่ยนแปลงเมื่อส่งมาครั้งใหม่
    var newwin = window.open(sfile, swinid, 'WIDTH=220,HEIGHT=220,RESIZABLE=yes,SCROLLBARS=yes,STATUS=1,LEFT=0,TOP=0');
    newwin.moveTo(0, 0);
    if (newwin != null) {
        if (newwin.opener == null)
            newwin.opener = self;
    }
}
function sselect_deselectAll(chkVal, idVal, idstr, idstr2) {
    var frm = document.forms[0];
    // Loop through all elements
    for (i = 0; i < frm.length; i++) {
        // Look for our Header Template's Checkbox
        if (idVal.indexOf(idstr) != -1) {
            //if elements is them RadioButtonList not access scope if
            if (frm.elements[i].name.indexOf(idstr2) != -1 && frm.elements[i].disabled == false) {
                // Check if main checkbox is checked, then select or deselect datagrid checkboxes 
                if (chkVal == true) {
                    frm.elements[i].checked = true;
                }
                else {
                    frm.elements[i].checked = false;
                }
                // Work here with the Item Template's multiple checkboxes
            }
        }
        else if (idVal.indexOf('DeleteThis') != -1) {
            // Check if any of the checkboxes are not checked, and then uncheck top select all checkbox
            if (frm.elements[i].checked == false) {
                frm.elements[1].checked = false; //Uncheck main select all checkbox
            }
        }
    }
}
function numbersonly(e, decimal) {
    var key;
    var keychar;

    if (window.event) {
        key = window.event.keyCode;
    }
    else if (e) {
        key = e.which;
    }
    else {
        return true;
    }
    keychar = String.fromCharCode(key);

    if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {
        return true;
    }
    else if ((("0123456789").indexOf(keychar) > -1)) {
        return true;
    }
    else if (decimal && (keychar == ".")) {
        return true;
    }
    else
        return false;
}

function SetMenu() {

    //1 = 1;
    //if ($('a[id=side-menu_11]').attr('class') != undefined) $('ul[id=side-menu_10]').addClass('in');
    //else if ($('a[id=side-menu_12]').attr('class') != undefined) $('ul[id=side-menu_10]').addClass('in');
    //else if ($('a[id=side-menu_13]').attr('class') != undefined) $('ul[id=side-menu_10]').addClass('in');
    //else if ($('a[id=side-menu_14]').attr('class') != undefined) $('ul[id=side-menu_10]').addClass('in');
    //else if ($('a[id=side-menu_15]').attr('class') != undefined) $('ul[id=side-menu_10]').addClass('in');

    //if ($('a[id=side-menu_21]').attr('class') != undefined) $('ul[id=side-menu_20]').addClass('in');
    //else if ($('a[id=side-menu_22]').attr('class') != undefined) $('ul[id=side-menu_20]').addClass('in');
    //else if ($('a[id=side-menu_23]').attr('class') != undefined) $('ul[id=side-menu_20]').addClass('in');
    //else if ($('a[id=side-menu_24]').attr('class') != undefined) $('ul[id=side-menu_20]').addClass('in');
    //else if ($('a[id=side-menu_25]').attr('class') != undefined) $('ul[id=side-menu_20]').addClass('in');

    //if ($('a[id=side-menu_31]').attr('class') != undefined) $('ul[id=side-menu_30]').addClass('in');
    //else if ($('a[id=side-menu_32]').attr('class') != undefined) $('ul[id=side-menu_30]').addClass('in');

    //$('ul[id=side-menu_10]').click(function () {
    //    if ($('ul[id=side-menu_10]').attr('class').indexOf("in") > -1) {
    //        $('a[id=side-menu_11]').removeClass("in");
    //        $('a[id=side-menu_12]').removeClass("in");
    //        $('a[id=side-menu_13]').removeClass("in");
    //        $('a[id=side-menu_14]').removeClass("in");
    //        $('a[id=side-menu_15]').removeClass("in");
    //    }
    //});
}

String.format = function () {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }

    return s;
}