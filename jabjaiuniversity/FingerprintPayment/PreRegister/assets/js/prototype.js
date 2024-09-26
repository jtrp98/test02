
// String.Format in JavaScript 
// This is the function.
String.prototype.format = function () {
    var str = this;
    for (var i = 0; i < arguments.length; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        str = str.replace(reg, arguments[i]);
    }
    return str;
};
String.prototype.format.regex = new RegExp("{-?[0-9]+}", "g");

String.prototype.bool = function () {
    return (/^true$/i).test(this.toLowerCase());
};

String.prototype.yearThai = function () {
    if (this.indexOf(' ') == -1) {
        var dmyDateString = this.replace(/(\d{4})-(\d{2})-(\d{2})/, "$3/$2/$1");
        var dmy = dmyDateString.trim().split('/');
        return dmy[0] + '/' + dmy[1] + '/' + (parseInt(dmy[2]) + 543);
    }
    else {
        var my = this.trim().split(' ');
        return my[0] + ' ' + (parseInt(my[1]) + 543);
    }
};

Number.prototype.padLeft = function (width, char) {
    if (!char) {
        char = " ";
    }

    if (("" + this).length >= width) {
        return "" + this;
    }
    else {
        return arguments.callee.call(char + this, width, char);
    }
};

Number.prototype.monthName = function () {
    var monthName = ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"]

    return monthName[this - 1];
};

(function ($) {
    $.isBlank = function (obj) {
        return (!obj || $.trim(obj) === "");
    };

    $.isEmpty = function (obj) {
        if (typeof obj == 'number') return false;
        else if (typeof obj == 'string') return obj.length == 0;
        else if (Array.isArray(obj)) return obj.length == 0;
        else if (typeof obj == 'object') return obj == null || Object.keys(obj).length == 0;
        else if (typeof obj == 'boolean') return false;
        else return !obj;
    };
})(jQuery);