window.onload = function (e) {
    // init 
    // https://developers.line.me/ja/reference/liff/#initialize-liff-app

    liff.init(function (data) {

        var registerResult = false;

        try {
            registerResult = LINERegister($('input#ssid').val(), data.context.userId);

            if (registerResult) {
                AddOrOpenBot();
            }
        }
        catch (err) {
            ShowMessage('error: code[' + err.code + '], message[' + err.message + ']');
            $.ajax({
                type: "POST",
                url: "register.aspx/ErrorMessage",
                data: '{userId: \'' + data.context.userId + '\', os: \'' + '' + '\', ssid: \'' + $('input#ssid').val() + '\', message: \'' + err.message + '\'}',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        }
    });
};

function LINERegister(ssid, userId) {
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

    var result = false;

    $.ajax({
        type: "POST",
        url: "register.aspx/LINERegister",
        data: '{userId: \'' + userId + '\', os: \'' + '' + '\', ssid: \'' + ssid + '\'}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            try {
                var r = $.parseJSON(response.d);
                if (r.result == "success") {
                    result = true;
                }
                else {
                    if (r.code == '202') {
                        result = true;
                    } else {
                        ShowMessage('error: ' + r.message);
                    }
                }
            }
            catch (err) {
                $.ajax({
                    type: "POST",
                    url: "register.aspx/ErrorMessage",
                    data: '{userId: \'' + userId + '\', os: \'' + '' + '\', ssid: \'' + ssid + '\', message: \'' + err.message + '\'}',
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

function AddOrOpenBot() {
    const botBasicId = '@724pdlid';

    liff.openWindow({
        url: 'line://ti/p/' + botBasicId,
        external: true
    });
    setTimeout(function () {
        liff.closeWindow();
    }, 3000);
    //liff.closeWindow();
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