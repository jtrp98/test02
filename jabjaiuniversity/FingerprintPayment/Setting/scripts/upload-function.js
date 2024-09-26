
function UploadFile(fieldClass, prefixFile, cl) {
    var formData = new FormData();

    var objFiles = $(fieldClass + ' input[type="file"]').get(0);
    var files = objFiles.files;
    for (var i = 0; i < files.length; i++) {
        formData.append(prefixFile + i, files[i]);
    }

    UploadFileProcess(fieldClass, formData, cl);
}

function UploadFileProcess(fieldClass, formData, cl) {
    $.ajax({
        url: '/Handles/UploadImageHandler.ashx?cl=' + cl,
        data: formData,
        dataType: 'json',
        type: 'POST',
        contentType: false,
        processData: false,
        xhr: function () {
            // this is the important part
            var xhr = new window.XMLHttpRequest();

            xhr.upload.addEventListener("progress", function (evt) {
                $(fieldClass + ' .progress').fadeIn();

                //check if the browser can determine the complete size of the data.
                if (evt.lengthComputable) {
                    var percentComplete = Math.round((evt.loaded / evt.total) * 100);

                    //do something with the percentage...
                    $(fieldClass + ' .progress-bar').css('width', percentComplete + '%').attr('aria-valuenow', percentComplete);
                }
            }, false);

            return xhr;
        },
        success: function (result) {
            //do some tasks after upload

            $(fieldClass + ' .img-photo').attr("src", "/Handles/GetImageHandler.ashx?cl=" + cl + "&im=" + result.file);

            $(fieldClass + ' .progress-bar').css('width', 100 + '%').attr('aria-valuenow', 100);

            $(fieldClass + ' .progress').fadeOut();

            $(fieldClass).attr("data-filename", result.file);
        },
        error: function (response) {
            $(fieldClass + ' .progress').fadeOut();
        }
    });
}