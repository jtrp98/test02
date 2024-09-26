// Not Success Function
function HideValidators() {
    //Hide all validation errors  
    if (window.Page_Validators)
        for (var vI = 0; vI < Page_Validators.length; vI++) {
            var vValidator = Page_Validators[vI];
            vValidator.isvalid = true;
            ValidatorUpdateDisplay(vValidator);
        }
    //Hide all validaiton summaries  
    if (typeof (Page_ValidationSummaries) != "undefined") { //hide the validation summaries  
        for (sums = 0; sums < Page_ValidationSummaries.length; sums++) {
            summary = Page_ValidationSummaries[sums];
            summary.style.display = "none";
        }
    }
}
// Common Function
function CheckFileOnly(source, args) {
    //var sFile = args.value.toLowerCase();
    var sFile = document.getElementById(source.controltovalidate).value.toLowerCase();
    args.IsValid = ((sFile.endsWith('.pdf')) ||
        (sFile.endsWith('.txt')) ||
        (sFile.endsWith('.doc')) ||
        (sFile.endsWith('.docx')) ||
        (sFile.endsWith('.xls')) ||
        (sFile.endsWith('.xlsx')) ||
        (sFile.endsWith('.wav')) ||
        (sFile.endsWith('.dat')));
}
function CheckZipFileOnly(source, args) {
    //var sFile = args.value.toLowerCase();
    var sFile = document.getElementById(source.controltovalidate).value.toLowerCase();
    args.IsValid = ((sFile.endsWith('.zip')));
}
function doCheckUncheckAllUnlessDisableCheckBox(tblID, isChecked) {
    var table = document.getElementById(tblID);
    var rows = table.getElementsByTagName('tr');
    for (i = 0; i < rows.length; i++) {
        var cols = rows[i].getElementsByTagName('td');
        for (j = 0; j < cols.length; j++) {
            var inputs = cols[j].getElementsByTagName('input');
            for (k = 0; k < inputs.length; k++) {
                switch (inputs[k].type) {
                    case 'checkbox':
                        if (!inputs[k].disabled) inputs[k].checked = isChecked;
                        break;
                    case 'text':
                        //sumscore += parseInt(inputs[k].value == '' ? '0' : inputs[k].value);
                        break;
                }
            }
        }
    }
}
function doCheckUncheckAll(tblID, isChecked) {
    var table = document.getElementById(tblID);
    var rows = table.getElementsByTagName('tr');
    for (i = 0; i < rows.length; i++) {
        var cols = rows[i].getElementsByTagName('td');
        for (j = 0; j < cols.length; j++) {
            var inputs = cols[j].getElementsByTagName('input');
            for (k = 0; k < inputs.length; k++) {
                switch (inputs[k].type) {
                    case 'checkbox':
                        inputs[k].checked = isChecked;
                        break;
                    case 'text':
                        //sumscore += parseInt(inputs[k].value == '' ? '0' : inputs[k].value);
                        break;
                }
            }
        }
    }
}
function doFindCheckAll(tblID, chkAllID) {
    var isCheckAll = true;
    var table = document.getElementById(tblID);
    var rows = table.getElementsByTagName('tr');
    for (i = 1; i < rows.length; i++) {
        var cols = rows[i].getElementsByTagName('td');
        for (j = 0; j < cols.length; j++) {
            var inputs = cols[j].getElementsByTagName('input');
            for (k = 0; k < inputs.length; k++) {
                switch (inputs[k].type) {
                    case 'checkbox':
                        if (!inputs[k].checked) { isCheckAll = false; break; }
                        break;
                    case 'text':
                        //sumscore += parseInt(inputs[k].value == '' ? '0' : inputs[k].value);
                        break;
                }
            }
        }
    }
    // check all
    document.getElementById(chkAllID).checked = isCheckAll;
}
// เช็คว่ามีการเลือกเมื่อต้องการลบมะ และ ถ้าเลือกให้ยืนยันการลบ
function ConfirmDelete(cf, sl, tblID) {
    var isOneChecked = false;
    var table = document.getElementById(tblID);
    var rows = table.getElementsByTagName('tr');
    for (i = 1; i < rows.length; i++) {
        var cols = rows[i].getElementsByTagName('td');
        for (j = 0; j < cols.length; j++) {
            var inputs = cols[j].getElementsByTagName('input');
            for (k = 0; k < inputs.length; k++) {
                switch (inputs[k].type) {
                    case 'checkbox':
                        if (inputs[k].checked) { isOneChecked = true; break; }
                        break;
                    case 'text':
                        //sumscore += parseInt(inputs[k].value == '' ? '0' : inputs[k].value);
                        break;
                }
            }
        }
    }
    if (isOneChecked) {
        return confirm(cf);
    }
    else {
        alert(sl);
        return false;
    }
}
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
function isCorrectSlashPosition(keyChar, e) {
    if (keyChar == '/') {
        // check version browser
        if (window.event) e = window.event; //IE (e.srcelement), FireFox (e.target)
        var objTextBox = e.srcElement ? e.srcElement : e.target;
        // check count slash
        if (objTextBox.value.split('/').length > 2) return false;
        var _ifirstSlash = objTextBox.value.indexOf('/');
        var _iPosition = getCaretPosition(objTextBox);
        // check position
        if ((_iPosition == 2 || _iPosition == 3) && _ifirstSlash == -1) return true;
        else if ((_ifirstSlash + 1) == 2 && (_iPosition == 4 || _iPosition == 5)) return true;
        else if ((_ifirstSlash + 1) == 3 && (_iPosition == 5 || _iPosition == 6)) return true;
        else return false;
    }
    else return true;
}
function getCaretPosition(objTextBox) {
    /* Purpose : Returns the caret position of the cursor
     * in the text box
     * Effects : None
     * Inputs : objTextBox - a text box
     * Returns : Integer indicating the caret position
     * in the text box
     * http://bytes.com/topic/javascript/answers/149058-date-validation-using-onkeydown-javascript-asp
     */

    var i = objTextBox.value.length + 1;
    if (objTextBox.createTextRange) {
        objCaret = document.selection.createRange().duplicate();
        while (objCaret.parentElement() == objTextBox && objCaret.move("character", 1) == 1)--i;
    }
    return i;
}
// เช็คตัวเลข
function isNum(e) {
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
    datecheck = /\d|[.]/;

    return datecheck.test(keychar);
}
function isNumber(e, ctrID) {
    var charCode = (e.which) ? e.which : window.event.keyCode;
    var keyChar = String.fromCharCode(charCode);
    var re = /^\d+$/
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
function get(id, element) {
    //  validation code that was removed
    if (!element) return document.getElementById(id);
    if (element.getElementById) return element.getElementById(id);
    //  manual DOM walk that was removed ...
}
function CheckFloat(svalue, _txt)// ตรวจสอบ float
{
    if (svalue != '') {
        fajax.CheckFloat(svalue, _txt, CallBack_CheckID);
    }
}
function CheckDate(spono, ctrID, ctr_sdID, _txt)// ตรวจสอบ วันที่
{
    if (spono != '') {
        fajax.CheckDate(spono, ctrID, ctr_sdID, _txt, CallBack_CheckID);
    }
    else {
        document.getElementById(ctrID).innerHTML = '';
        document.getElementById(ctr_sdID).innerHTML = '';
    }
}
function CheckInt(svalue, _txt)// ตรวจสอบ float
{
    if (svalue != '') {
        fajax.CheckInt(svalue, _txt, CallBack_CheckID);
    }
}
function CheckInt1(svalue, _txt)// ตรวจสอบ float
{
    if (svalue != '') {
        fajax.CheckInt1(svalue, _txt, CallBack_CheckID);
    }
}
function CheckPlan(svalue, _txt)// ตรวจสอบ float
{
    if (svalue != '') {
        fajax.CheckPlan(svalue, _txt, CallBack_CheckID);
    }
}
function CheckActionno(svalue, _txt)// ตรวจสอบ float
{
    if (svalue != '') {
        fajax.CheckActionno(svalue, _txt, CallBack_CheckID);
    }
}
function CheckCompareDate(_start, _end, txtstart, txtend)// ตรวจสอบ กรอกเลขที่ซ้ำ
{
    if (_start != '' && _end != '') {
        fajax.CheckCompareDate(_start, _end, txtstart, txtend, CallBack_CheckID);
    }
}
function Checktrans(svalue, _txt, __ntotalpay, _ntotaltrans)// ตรวจสอบ float
{
    if (svalue != '') {
        fajax.Checktrans(svalue, _txt, __ntotalpay, _ntotaltrans, CallBack_CheckID);
    }
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
function DateSplit(sdate, returnID) {
    var arrdate = sdate.split('/');
    if (arrdate.length == 3)
        document.getElementById(returnID).value = arrdate[2];
}
function CustomValidateDate(source, args) {
    var arrStart = document.getElementById("cphMain_txtBegin").value.split('/');
    if (arrStart.length == 3) {
        var arrEnd = document.getElementById(source.controltovalidate).value.split('/');
        var start = new Date(arrStart[1] + "/" + arrStart[0] + "/" + arrStart[2]);
        var end = new Date(arrEnd[1] + "/" + arrEnd[0] + "/" + arrEnd[2]);
        args.IsValid = start.getTime() < end.getTime();
    }
    else {
        args.IsValid = false;
        //document.getElementById(source.controltovalidate).value='';
        //document.getElementById(source.ErrorMessage).value="<span style='font-size:12'><b>พบข้อผิดพลาด</b><br />กรุณากรอกวันที่เริ่มต้น ก่อนวันที่สิ้นสุด!";
    }
}
function setValueAutoComplete(ctrl_set2, val) {
    var _nodeName = $('#' + ctrl_set2)[0].nodeName;
    switch (_nodeName.toLowerCase()) {
        case 'input':
            var _type = $('#' + ctrl_set2)[0].type;
            if (_type == 'text') $('#' + ctrl_set2).val(val);
            break;
        case 'select':
            $('#' + ctrl_set2).val(val);
            break;
        case 'span':
            $('#' + ctrl_set2).html(val);
            break;
        default:
            //$('#'+ctrl_set2).html(val);
            break;
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

function funtionListSubLV2(controlsublevel, sontrolsublevel2) {
    var SubLVID = $('#' + controlsublevel + ' option:selected').val();
    var sSubLV = $('#' + controlsublevel + ' option:selected').text();
    $("#" + sontrolsublevel2 + " option").remove();
    $('select[id*=' + sontrolsublevel2 + ']')
        .append($("<option></option>")
            .attr("value", "")
            .text("ทั้งหมด"));
    $.ajax({
        url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
        success: function (msg) {
            $.each(msg, function (index) {
                $('select[id*=' + sontrolsublevel2 + ']')
                    .append($("<option></option>")
                        .attr("value", msg[index].nTermSubLevel2)
                        .text(sSubLV + " / " + msg[index].nTSubLevel2));
            });
        }
    });
}

function funtionListSubLV2(controlsublevel, controlsublevel2, setvalues) {
    var SubLVID = $('#' + controlsublevel + ' option:selected').val();
    var sSubLV = $('#' + controlsublevel + ' option:selected').text();
    $("#" + controlsublevel2 + " option").remove();
    $('select[id*=' + controlsublevel2 + ']')
        .append($("<option></option>")
            .attr("value", "")
            .text("ทั้งหมด"));
    var request = $.ajax({
        url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
        success: function (msg) {

            $.each(msg, function (index) {
                $('select[id*=' + controlsublevel2 + ']')
                    .append($("<option></option>")
                        .attr("value", msg[index].nTermSubLevel2)
                        .text(msg[index].nTSubLevel2));
            });

        }
    });
    request.done(function () {
        $('#' + controlsublevel2).val(setvalues);
    })
}

function functionListstudent(controlsublevel, sontrolsublevel2) {
    var availableValueplane = [];
    $.ajax({
        url: "/App_Logic/dataGenericListData.ashx?mode=liststudent&nelevel=" +
            $('#' + controlsublevel + ' option:selected').val() + "&nsublevel=" + $('#' + sontrolsublevel2 + ' option:selected').val(),
        dataType: "json",
        success: function (objjson) {
            $.each(objjson, function (index) {
                var newObject = {
                    label: objjson[index].sName,
                    value: objjson[index].sID,
                    code: objjson[index].studentid,
                };
                availableValueplane[index] = newObject;
            });
        }
    });
    return availableValueplane;
}

function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
}

function onOverlayCloseClick() {
    $("#content-overlay").toggleClass("closed");
}

function onNavbarToggleClick() {
    $("#content-nav").toggleClass("open");
}

function Dateformat(obj) {
    if (obj == undefined) return "";
    var parsedDate = new Date('1/1/1999 ' + obj.Hours + ':' + obj.Minutes + ':' + obj.Seconds);
    return parsedDate.toLocaleTimeString();
}

function createCookie(name, value, days) {
    var expires;

    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    } else {
        expires = "";
    }
    document.cookie = encodeURIComponent(name) + "=" + encodeURIComponent(value) + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = encodeURIComponent(name) + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) === 0) return decodeURIComponent(c.substring(nameEQ.length, c.length));
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}

String.format = function () {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
};


//return an array of objects according to key, value, or key and value matching
function getObjects(obj, key, val) {
    var objects = [];
    for (var i in obj) {
        if (!obj.hasOwnProperty(i)) continue;
        if (typeof obj[i] == 'object') {
            objects = objects.concat(getObjects(obj[i], key, val));
        } else
            //if key matches and value matches or if key matches and value is not passed (eliminating the case where key matches but passed value does not)
            if (i == key && obj[i] == val || i == key && val == '') { //
                objects.push(obj);
            } else if (obj[i] == val && key == '') {
                //only add if the object is not already in the array
                if (objects.lastIndexOf(obj) == -1) {
                    objects.push(obj);
                }
            }
    }
    return objects;
}

//return an array of values that match on a certain key
function getValues(obj, key) {
    var objects = [];
    for (var i in obj) {
        if (!obj.hasOwnProperty(i)) continue;
        if (typeof obj[i] == 'object') {
            objects = objects.concat(getValues(obj[i], key));
        } else if (i == key) {
            objects.push(obj[i]);
        }
    }
    return objects;
}

//return an array of keys that match on a certain value
function getKeys(obj, val) {
    var objects = [];
    for (var i in obj) {
        if (!obj.hasOwnProperty(i)) continue;
        if (typeof obj[i] == 'object') {
            objects = objects.concat(getKeys(obj[i], val));
        } else if (obj[i] == val) {
            objects.push(i);
        }
    }
    return objects;
}

Array.prototype.removeValue = function (name, value) {
    var array = $.map(this, function (v, i) {
        return v[name] === value ? null : v;
    });
    this.length = 0; //clear original array
    this.push.apply(this, array); //push all elements except the one we want to delete
}

function getDate(values) {
    if (values !== null && values !== "") {
        var array = values.split("/");
        return array[1] + "/" + array[0] + "/" + array[2];
    }
    else {
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (month < 10 ? '0' : '') + month + '/' +
            (day < 10 ? '0' : '') + day + '/' + d.getFullYear();
        return output;
    }
}