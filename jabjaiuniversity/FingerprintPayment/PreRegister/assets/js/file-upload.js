
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
        url: '/Ashx/UploadImageHandler.ashx?cl=' + cl,
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

            $(fieldClass + ' .img-photo').attr("src", "/Ashx/GetImageHandler.ashx?cl=" + cl + "&im=" + result.file);

            $(fieldClass + ' .progress-bar').css('width', 100 + '%').attr('aria-valuenow', 100);

            $(fieldClass + ' .progress').fadeOut();

            $(fieldClass).attr("data-filename", result.file);
        },
        error: function (response) {
            $(fieldClass + ' .progress').fadeOut();
        }
    });
}

/**
 * Bootstrap File Upload (https://github.com/davidstutz/bootstrap-file-upload)
 *
 * Apache License, Version 2.0:
 * Copyright (c) 2016 - 2018 David Stutz
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a
 * copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 *
 * BSD 3-Clause License:
 * Copyright (c) 2016 David Stutz
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *    - Redistributions of source code must retain the above copyright notice,
 *      this list of conditions and the following disclaimer.
 *    - Redistributions in binary form must reproduce the above copyright notice,
 *      this list of conditions and the following disclaimer in the documentation
 *      and/or other materials provided with the distribution.
 *    - Neither the name of David Stutz nor the names of its contributors may be
 *      used to endorse or promote products derived from this software without
 *      specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 * PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS;
 * OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
 * OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
 * ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
+function ($) {
    'use strict';

    var FileUpload = function (element) {
        this.element = $(element);
        var defaultText = this.element.text();

        var label = this.element.text();
        var input = $('input', this.element);

        this.element.text('');
        this.element.append('<span class="file-upload-text"></span>');
        $('.file-upload-text', this.element).text(label);

        this.element.append(input);

        this.element.on('change', ':file', function () {
            var input = $(this);

            if (input.val()) {
                var label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
                $('.file-upload-text', $(this).parent('label')).text(label);
            }
            else {

                $('.file-upload-text', $(this).parent('label')).text(defaultText);
            }
        });
    };

    function Plugin() {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data('bs.file-upload');

            if (!data) {
                $this.data('bs.file_upload', (data = new FileUpload(this)));
            }
        });
    }

    var old = $.fn.file_upload;
    $.fn.file_upload = Plugin;
    $.fn.file_upload.Constructor = FileUpload;

    $.fn.file_upload.noConflict = function () {
        $.fn.file_upload = old;
        return this;
    };
}(jQuery);