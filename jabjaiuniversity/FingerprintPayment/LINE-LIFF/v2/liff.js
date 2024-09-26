window.onload = function (e) {
    // init 
    // https://developers.line.biz/en/reference/liff/#initialize-liff-app

    var registerResult = false;

    liff
        .init({
            liffId: "1653630518-y03N0qao" // 1653527349-LRexneZx //1653630518-y03N0qao
        })
        .then(() => {
            const context = liff.getContext();

            registerResult = LINERegister(context, $('input#ssid').val());

            if (registerResult.isSuccess) {
                if (registerResult.code == '200') {
                    AddAndOpenBot();
                }
                else if (registerResult.code == '202') {
                    OpenBot();
                }
            }
        })
        .catch((err) => {
            ShowMessage('error: code[' + err.code + '], message[' + err.message + ']');
            $.ajax({
                type: "POST",
                url: "register.aspx/ErrorMessage",
                data: '{userId: \'' + context.userId + '\', os: \'' + liff.getOS() + '\', ssid: \'' + $('input#ssid').val() + '\', message: \'' + err.message + '\'}',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        });
};

function LINERegister(context, ssid) {
    // {
    //    "type": "utou",
    //    "utouId": "UU29e6eb36812f484fd275d41b5af4e760926c516d8c9faa35…b1e8de8fbb6ecb263ee8724e48118565e3368d39778fe648d",
    //    "userId": "U70e153189a29f1188b045366285346bc",
    //    "viewType": "full",
    //    "accessTokenHash": "ArIXhlwQMAZyW7SDHm7L2g"
    //}
    //liff.openWindow({
    //    url: 'scheme://host'
    //});

    var result = {};

    result.isSuccess = false;

    $.ajax({
        type: "POST",
        url: "register.aspx/LINERegister",
        data: '{userId: \'' + context.userId + '\', os: \'' + liff.getOS() + '\', ssid: \'' + ssid + '\'}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            try {
                var r = $.parseJSON(response.d);
                if (r.result == "success") {
                    result.isSuccess = true;
                    result.code = r.code;
                }
                else {
                    if (r.code == '202') {
                        result.isSuccess = true;
                        result.code = r.code;
                    } else {
                        ShowMessage('error: ' + r.message);
                    }
                }
            }
            catch (err) {
                $.ajax({
                    type: "POST",
                    url: "register.aspx/ErrorMessage",
                    data: '{userId: \'' + context.userId + '\', os: \'' + liff.getOS() + '\', ssid: \'' + ssid + '\', message: \'' + err.message + '\'}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                });
            }
        },
        failure: function (response) {
            ShowMessage('failure');
        },
        error: function (response) {
            ShowMessage('error');
        }
    });

    return result;
}

function AddAndOpenBot() {
    const botBasicId = '@243rsixm'; //@724pdlid //@243rsixm

    switch (liff.getOS()) {
        case 'android':
            liff.openWindow({
                url: 'line://ti/p/' + botBasicId,
                external: false
            });
            liff.closeWindow();
            break;
        case 'ios':
            liff.openWindow({
                url: 'line://ti/p/' + botBasicId,
                external: false
            });
            setTimeout(function () {
                liff.closeWindow();
            }, 8000);
            break;
        case 'web':
            liff.closeWindow();
            break;
    }
}

function OpenBot() {
    const botBasicId = '@243rsixm'; //@724pdlid //@243rsixm

    switch (liff.getOS()) {
        case 'android':
            liff.openWindow({
                url: 'line://ti/p/' + botBasicId,
                external: false
            });
            liff.closeWindow();
            break;
        case 'ios':
            liff.openWindow({
                url: 'line://ti/p/' + botBasicId,
                external: false
            });
            setTimeout(function () {
                liff.closeWindow();
            }, 8000);
            break;
        case 'web':
            liff.closeWindow();
            break;
    }
}

function ShowMessage(message) {
    document.getElementById('dtMessage').textContent = message;
    $("#divMessage").fadeIn(2000, function () {
        setTimeout(function () {
            $("#divMessage").fadeOut(1000);
            //liff.closeWindow();
        }, 5000);
    });
}