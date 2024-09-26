window.onload = function (e) {

    // init 
    // https://developers.line.biz/en/reference/liff/#initialize-liff-app
    liff
        .init({
            liffId: "1653527349-LRexneZx" // 1653527349-LRexneZx //1653630518-y03N0qao
        })
        .then(() => {
            LINERegister($('input#ssid').val());
        })
        .catch((err) => {
            ShowMessage('error: code[' + err.code + '], message[' + err.message + ']');
            $.ajax({
                type: "POST",
                url: "liff-register.aspx/ErrorMessage",
                data: '{userId: \'' + '' + '\', os: \'' + liff.getOS() + '\', ssid: \'' + $('input#ssid').val() + '\', message: \'' + err.message + '\'}',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        });
};

function LINERegister(ssid) {
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

    const botBasicId = '@724pdlid';

    const context = liff.getContext();
    $.ajax({
        type: "POST",
        url: "liff-register.aspx/LINERegister",
        data: '{userId: \'' + context.userId + '\', os: \'' + liff.getOS() + '\', ssid: \'' + ssid + '\'}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            try {
                var r = $.parseJSON(response.d);
                if (r.result == "success") {
                    AddOrOpenBot(botBasicId);
                }
                else {
                    if (r.code == '202') {
                        AddOrOpenBot(botBasicId);
                    } else {
                        ShowMessage('error: ' + r.message);
                    }
                }
            }
            catch (err) {
                $.ajax({
                    type: "POST",
                    url: "liff-register.aspx/ErrorMessage",
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
}

function AddOrOpenBot(botBasicId) {
    switch (liff.getOS()) {
        case 'android':
            liff.openWindow({
                url: 'line://ti/p/' + botBasicId,
                external: true
            });
            liff.closeWindow();
            //setTimeout(function () {
            //    liff.openWindow({
            //        url: 'line://ti/p/' + botBasicId,
            //        external: true
            //    });
            //    liff.closeWindow();
            //}, 3000);
            break;
        case 'ios':
            liff.openWindow({
                url: 'https://line.me/R/ti/p/' + botBasicId,
                external: true
            });
            //setTimeout(function () {
            //    liff.openWindow({
            //        url: 'https://line.me/R/ti/p/' + botBasicId,
            //        external: true
            //    });
            //    //liff.closeWindow();
            //}, 3000);
            //setTimeout(function () {
            //    liff.closeWindow();
            //}, 5000);
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