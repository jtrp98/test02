// "use strict";

function ThaiNumberToText(Number) {
    Number = Number.replace(/๐/gi, '0');
    Number = Number.replace(/๑/gi, '1');
    Number = Number.replace(/๒/gi, '2');
    Number = Number.replace(/๓/gi, '3');
    Number = Number.replace(/๔/gi, '4');
    Number = Number.replace(/๕/gi, '5');
    Number = Number.replace(/๖/gi, '6');
    Number = Number.replace(/๗/gi, '7');
    Number = Number.replace(/๘/gi, '8');
    Number = Number.replace(/๙/gi, '9');
    return ArabicNumberToText(Number);
}

function ArabicNumberToThaiNumber(Number) {
    Number = Number.replace('0', /๐/gi);
    Number = Number.replace('1', /๑/gi);
    Number = Number.replace('2', /๒/gi);
    Number = Number.replace('3', /๓/gi);
    Number = Number.replace('4', /๔/gi);
    Number = Number.replace('5', /๕/gi);
    Number = Number.replace('6', /๖/gi);
    Number = Number.replace('7', /๗/gi);
    Number = Number.replace('8', /๘/gi);
    Number = Number.replace('9', /๙/gi);
    return ArabicNumberToText(Number);
}

function ArabicNumberToText(_Number) {
    var Number = CheckNumber(_Number);
    var NumberArray = new Array("ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ");
    var DigitArray = new Array("", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน");
    var BahtText = "";
    if (isNaN(Number)) {
        return "ข้อมูลนำเข้าไม่ถูกต้อง";
    } else {
        if ((Number - 0) > 9999999.9999) {
            return "ข้อมูลนำเข้าเกินขอบเขตที่ตั้งไว้";
        } else {
            Number = Number.split(".");
            if (Number[1].length > 0) {
                Number[1] = Number[1].substring(0, 2);
            }
            var NumberLen = Number[0].length - 0;
            for (var i = 0; i < NumberLen; i++) {
                let tmp = Number[0].substring(i, i + 1) - 0;
                if (tmp !== 0) {
                    if ((i === (NumberLen - 1)) && (tmp === 1)) {
                        BahtText += "เอ็ด";
                    } else
                        if ((i === (NumberLen - 2)) && (tmp === 2)) {
                            BahtText += "ยี่";
                        } else
                            if ((i === (NumberLen - 2)) && (tmp === 1)) {
                                BahtText += "";
                            } else {
                                BahtText += NumberArray[tmp];
                            }
                    BahtText += DigitArray[NumberLen - i - 1];
                }
            }
            BahtText += "บาท";
            if ((Number[1] === "0") || (Number[1] === "00")) {
                BahtText += "ถ้วน";
            } else {
                DecimalLen = Number[1].length - 0;
                for (var i = 0; i < DecimalLen; i++) {
                    let tmp = Number[1].substring(i, i + 1) - 0;
                    if (tmp !== 0) {
                        if ((i === (DecimalLen - 1)) && (tmp === 1)) {
                            BahtText += "เอ็ด";
                        } else
                            if ((i === (DecimalLen - 2)) && (tmp === 2)) {
                                BahtText += "ยี่";
                            } else
                                if ((i === (DecimalLen - 2)) && (tmp === 1)) {
                                    BahtText += "";
                                } else {
                                    BahtText += NumberArray[tmp];
                                }
                        BahtText += DigitArray[DecimalLen - i - 1];
                    }
                }
                BahtText += "สตางค์";
            }
            return BahtText;
        }
    }
}

function ArabicNumberToTextENG(_Number) {
    var th = ['', 'thousand', 'million', 'billion', 'trillion'];
    var dg = ['', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'];
    var tn = ['ten', 'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen'];
    var tw = ['twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];
    var thStr = '';
    var num = _Number.toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    var parts = num.split('.');
    var wholeNum = parts[0].toLocaleString().split(',');
    var decNum = parseInt(parts[1]);

    if (parseFloat(wholeNum) === 0) {
        return 'zero dollars and ' + decNum + '/100 cents';
    }

    if (wholeNum.length > 15) {
        return 'Number too large';
    }

    for (i = wholeNum.length; i > 0; i--) {
        let _d = parseInt(wholeNum[i - 1]);
        let _w = "";

        if (_d >= 100) {
            _w = dg[parseInt(_d / 100)] + " hundred ";
            _d = _d % 100;
        }

        if (_d < 10) {
            _w += dg[_d];
        } else if (_d < 20) {
            _w += tn[_d % 10];
        } else {
            _w += tw[parseInt(_d / 10) - 2] + dg[(_d % 10)];
        }

        switch (wholeNum.length - i) {
            case 0: thStr = _w; break;
            case 1: thStr = _w + " thousand " + thStr; break;
            case 2: thStr = _w + " million " + thStr; break;
        }
    }

    var dollars = thStr + ' bath ';
    var cents = '';
    if (decNum == 0) {
        cents = "only";
    } else {
        if (decNum < 10) {
            cents = dg[decNum];
        } else if (decNum < 20) {
            cents = tn[decNum % 10];
        } else {
            cents = tw[parseInt(decNum / 10) - 2] + ' ' + dg[(decNum % 10)];
        }

        cents += " sateng ";
    }

    return (dollars + cents).toUpperCase();

}

function CheckNumber(Number) {
    var decimal = false;
    Number = Number.toString();
    Number = Number.replace(/ |,|บาท|฿/gi, '');
    for (var i = 0; i < Number.length; i++) {
        if (Number[i] === '.') {
            decimal = true;
        }
    }
    if (decimal === false) {
        Number = Number + '.00';
    }
    return Number
}