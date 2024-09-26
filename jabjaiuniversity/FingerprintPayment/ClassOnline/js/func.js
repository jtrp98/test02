

//window.onerror = function (msg, url, line, col, error) {
//    // Note that col & error are new to the HTML 5 spec and may not be
//    // supported in every browser.  It worked for me in Chrome.
//    var extra = !col ? '' : '\ncolumn: ' + col;
//    extra += !error ? '' : '\nerror: ' + error;

//    // You can view the information in an alert to see things working like this:
//    alert("Error: " + msg + "\nurl: " + url + "\nline: " + line + extra);

//    // TODO: Report this error via ajax so you can keep track
//    //       of what pages have JS issues

//    var suppressErrorAlert = true;
//    // If you return true, then error alerts (like in older versions of
//    // Internet Explorer) will be suppressed.
//    return suppressErrorAlert;
//};

$.extend({
    jYoutube: function (url, size) {
        if (url === null) { return ""; }

        size = (size === null) ? "big" : size;
        var vid;
        var results;

        results = url.match("[\?&]v=([^&#]*)");

        vid = (results === null) ? url : results[1];

        if (size == "small") {
            return "http://img.youtube.com/vi/" + vid + "/2.jpg";
        } else {
            return "http://img.youtube.com/vi/" + vid + "/0.jpg";
        }
    }
});

window.isMobileAndTabletCheckV1 = function () {
    let check = false;
    (function (a) { if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino|android|ipad|playbook|silk/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) check = true; })(navigator.userAgent || navigator.vendor || window.opera);
    return check;
};

window.isMobileAndTabletCheck = function () {
    if (navigator.userAgent.match(/Android/i)
        || navigator.userAgent.match(/webOS/i)
        || navigator.userAgent.match(/iPhone/i)
        || navigator.userAgent.match(/iPad/i)
        || navigator.userAgent.match(/iPod/i)
        || navigator.userAgent.match(/Mac/i)
        || navigator.userAgent.match(/BlackBerry/i)
        || navigator.userAgent.match(/Windows Phone/i)) {
        return true;
    }
    

    return false;
};

function youtube_parser(url) {
    var regExp = /^.*((youtu.be\/)|(v\/)|(\/u\/\w\/)|(embed\/)|(watch\?))\??v?=?([^#&?]*).*/;
    var match = url.match(regExp);
    return (match && match[7].length == 11) ? match[7] : false;
}

const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
    reader.readAsDataURL(file);
});

//function toBase64(file) {
//    var reader = new FileReader();
//    reader.readAsDataURL(file);
//    reader.onload = function () {
//        console.log(reader.result);
//    };
//    reader.onerror = function (error) {
//        console.log('Error: ', error);
//    };
//}

function NoPermissionPopup() {
    Swal.fire({
        icon: 'warning',
        title: '<span style="color:#ed8229;">ปฏิเสธการเข้าใช้</span>',
        html: 'ขออภัยค่ะ ท่านไม่มีสิทธิ์ในการเข้าใช้งานเมนูนี้<br/>โปรดติดต่อครูผู้ดูแลระบบโรงเรียนของท่าน เพื่อทำการแก้ไขสิทธิ์ ขอบคุณค่ะ',
    })
}

function __validator() {

    $(function () {
        if (jQuery.validator) {//.messages

            jQuery.extend(jQuery.validator.messages, {
                required: "กรุณาระบุให้ครบถ้วน",
                url: "กรุณาระบุลิงก์ให้ถูกต้อง",
                //remote: "Please fix this field.",
                //email: "Please enter a valid email address.",
                //date: "Please enter a valid date.",
                //dateISO: "Please enter a valid date (ISO).",
                //number: "Please enter a valid number.",
                //digits: "Please enter only digits.",
                //creditcard: "Please enter a valid credit card number.",
                //equalTo: "Please enter the same value again.",
                //accept: "Please enter a value with a valid extension.",
                //maxlength: jQuery.validator.format("Please enter no more than {0} characters."),
                //minlength: jQuery.validator.format("Please enter at least {0} characters."),
                //rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
                //range: jQuery.validator.format("Please enter a value between {0} and {1}."),
                //max: jQuery.validator.format("Please enter a value less than or equal to {0}."),
                //min: jQuery.validator.format("Please enter a value greater than or equal to {0}.")
            });

            $.validator.addMethod("requiredSelect", function (element) {
                let i = $("select.chosen-select :selected").val();

                return (i != '');
            }, "กรุณาระบุให้ครบถ้วน");

            //$.validator.addClassRules('chosen-select', {
            //    requiredSelect: true ,
            //});

            $("#aspnetForm").validate({  // initialize the plugin
                //rules: {
                //    select: { requiredSelect: true }
                //},
                //submitHandler: function (form) {  // fires on valid form
                //    alert('yes valid');
                //    //$.post(...post my data using ajax..)
                //    alert('data posted successfully.');
                //    //clear form to submit another data
                //    clearForm();  // <- I don't see this function in your OP
                //    return false;
                //},
                //invalidHandler: function (e, validator) {
                //    // 'this' refers to the form
                //    //alert('invalid');

                //    e.preventDefault();
                //    e.stopPropagation();
                //    return false;
                //}
                //rules: {
                //    field: {
                //        required: true,
                //        url: true
                //    }
                //},
                errorPlacement: function (error, element) {
                    let _class = element.attr('class');

                    if (_class.includes('--date-validate')) {
                        error.insertAfter(element.parent());
                    }
                    else {
                        error.insertAfter(element);
                    }

                }

            });


            $('#aspnetForm').on('submit', function (e) {
                // alert("Valid: " + $('#aspnetForm').valid());

                if ($('#aspnetForm').valid() == false) {

                    e.preventDefault();
                    e.stopPropagation();
                    return false;
                }
            });
        }
    });
}

__validator();