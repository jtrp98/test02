<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="UploadONETData.aspx.cs" Inherits="FingerprintPayment.StudentInfo.UploadONETData" EnableSessionState="ReadOnly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .modal-body {
            font-size: 26px;
        }

        .modal-dialog {
            text-align: center;
            vertical-align: middle;
        }

        .modal-content {
            /* Bootstrap sets the size of the modal in the modal-dialog class, we need to inherit it */
            width: inherit;
            height: inherit;
            /* To center horizontally */
            margin: 0 auto;
        }

        .btn.btn-success, .btn.btn-cancel, .btn.btn-danger, .btn.btn-primary {
            width: 110px;
            height: 44px;
        }

        .drag-zone {
            width: 100%;
        }

            .drag-zone .file-drag-area {
                /*width: 600px;*/
                height: 400px;
                border: 1px dashed #707070;
                text-align: center;
                font-size: 24px;
                position: relative;
            }

            .drag-zone .file-drag-over {
                color: #eee;
                border-color: #eee;
            }

            .drag-zone .file-drag-area:before {
                content: "\f16b";
                font: normal normal normal 14px/1 FontAwesome;
                font-size: 3em;
                line-height: 250px;
                color: #707070;
            }

            .drag-zone .file-choose {
                position: absolute;
                bottom: 10px;
                width: 100%;
                height: 50%;
            }

                .drag-zone .file-choose p {
                    color: #707070;
                    margin: 10px 0 15px 0;
                }

        .circle-progress-value {
            stroke-width: 6px;
            stroke: hsl(280, 90%, 50%);
            stroke-linecap: round;
            stroke: aquamarine;
        }

        .circle-progress-circle {
            stroke: #ddd;
            stroke-width: 2px;
        }

        .circle-progress-text {
            font-family: "Georgia";
            font-size: 22px;
            fill: white;
        }

        .upload-progress {
            text-align: center;
            position: absolute;
            width: 95%;
            height: 93%;
            /* padding-top: 10px; */
            margin-left: 15px;
            background-color: rgba(50,50,50,0.5);
            z-index: 2;
        }

        [class^=upload-progress] > svg, [class*=" upload-progress"] > svg {
            width: 160px;
            height: 160px;
            position: absolute;
            top: 50%;
            margin-top: -80px;
            margin-left: -80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="uploadONET">
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-12">
                    <h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206102") %></h2>
                </div>
            </div>
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-12">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206103") %></h3>
                </div>
            </div>
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-3">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206105") %></h3>
                </div>
                <div class="col-md-3">
                    <select id="sltYear" name="sltYear[]"
                        class="form-control">
                        <asp:Literal ID="ltrYear" runat="server" />
                    </select>
                </div>
            </div>
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-3">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101279") %></h3>
                </div>
                <div class="col-md-3">
                    <select id="sltLevel" name="sltLevel[]"
                        class="form-control">
                        <option selected="selected" value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111064") %></option>
                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111067") %></option>
                        <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111070") %></option>
                    </select>
                </div>
            </div>
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-3">
                    <h3>4. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404058") %></h3>
                </div>
                <div class="col-md-3">
                    <button type="button" class="btn btn-warning global-btn" data-toggle="modal" data-target="#modalUploadOnetData">
                        <i class="fa fa-file-excel-o" aria-hidden="true" style="vertical-align: middle; padding-right: 10px;"></i>Browse File</button>
                </div>
            </div>
        </div>
    </div>

    <div id="modalUploadOnetData" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 15%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="max-width: 900px;">

                <div class="modal-header center" style="padding: 0px; top: 25%;">
                    <h1>Upload File Excel</h1>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row">
                            <div class="upload-progress" style="display: none;"></div>
                            <div class="col-xs-12 text-center">
                                <div style="display: none;">
                                    <input type="file" name="fuUpload" id="fuUpload" />
                                </div>
                                <div class="drag-zone" align="center">
                                    <div class="file-drag-area">
                                        <div class="file-choose">
                                            <div class="row">
                                                <div class="col-md-12 mb-12">
                                                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101285") %></p>
                                                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %></p>
                                                    <br />
                                                    <input id="btnUpload" name="btnUpload" type="button" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101287") %>" class="btn btn-success" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button id="btnCloseModalUpload" type="button" class="btn btn-danger global-btn"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>

            </div>
        </div>
    </div>
    <!-- /.modal -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
    <script type="text/javascript" src="/StudentInfo/Scripts/circle-progress.js"></script>
    <script type="text/javascript">

        var circleProgress;
        var intervalId;

        var byteData;
        var reader = new FileReader();
        reader.onload = function () {
            byteData = this.result;

            byteData = byteData.split(';')[1].replace("base64,", "");

            var year = $(".uploadONET #sltYear").children("option:selected").text();
            var level = $(".uploadONET #sltLevel").children("option:selected").val();

            $.ajax({
                type: "POST",
                url: "UploadONETData.aspx/UploadExcelData",
                data: "{'byteData': '" + byteData + "', 'year': '" + year + "', 'level': " + level + " }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var jsonObj = $.parseJSON(result.d);

                    alert(jsonObj.message);

                    resetMonitor();

                    //Clear input[file]
                    $('input[type=file]').val('');
                },
                beforeSend: function () {
                    // Display upload-progress
                    $('.upload-progress').show();

                    $('#btnCloseModalUpload').prop('disabled', true);

                    disabledClickOutsideModal(true);
                },
                error: function (response) {
                    alert(response.responseText);

                    resetMonitor();
                }
            });

            // Periodically update monitors
            startMonitor();

        }

        function startRecursiveMonitor() {
            $.ajax({
                type: "POST",
                url: "UploadONETData.aspx/UpdateMonitor",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var jsonObj = $.parseJSON(result.d);

                    if (!jsonObj.isComplete) {
                        setTimeout(function () {
                            startRecursiveMonitor();
                        }, 1000);
                    }

                    updateMonitor(jsonObj.percentage);
                },
                error: function (response) {
                    resetMonitor();
                }
            });
        }

        function startMonitor() {
            intervalId = setInterval(function () {
                $.ajax({
                    type: "POST",
                    url: "UploadONETData.aspx/UpdateMonitor",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        var jsonObj = $.parseJSON(result.d);

                        if (jsonObj.isComplete) {
                            clearInterval(intervalId);
                            intervalId = null;
                        }

                        updateMonitor(jsonObj.percentage);
                    },
                    error: function (response) {
                        clearInterval(intervalId);
                        intervalId = null;

                        resetMonitor();
                    }
                });
            }, 1000);
        }

        function updateMonitor(value) {
            circleProgress.value = value;
        }

        function resetMonitor() {
            $('.upload-progress').hide();

            circleProgress.value = 0;

            $('#btnCloseModalUpload').prop('disabled', false);

            disabledClickOutsideModal(false);
        }

        function disabledClickOutsideModal(flag) {
            var $modal = $('#modalUploadOnetData');
            // Prevent to close by ESC
            var keyboard = false; // 
            // Prevent to close on click outside the modal
            // If you specify the value "static", it is not possible to close the modal when clicking outside of it
            var backdrop = flag ? 'static' : true; // set true: click outside to escape modal, set 'static': not click outside to escape modal

            // Modal did not open yet
            if (typeof $modal.data('bs.modal') === 'undefined') {
                $modal.modal({
                    keyboard: keyboard,
                    backdrop: backdrop
                });
            } else {
                // Modal has already been opened
                $modal.data('bs.modal').options.keyboard = keyboard;
                $modal.data('bs.modal').options.backdrop = backdrop;

                if (keyboard === false) {
                    // Disable ESC
                    $modal.off('keydown.dismiss.bs.modal');
                } else {
                    // Resets ESC
                    $modal.data('bs.modal').escape();
                }
            }
        }

        $(function () {

            // Upload File
            $('input[type=file]').change(function () {
                if (typeof (FileReader) != "undefined") {
                    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$/;
                    $($(this)[0].files).each(function () {
                        var file = $(this);
                        if (regex.test(file[0].name.toLowerCase())) {
                            console.log('0');
                            reader.readAsDataURL(file[0]);
                            console.log('1');
                        } else {
                            alert(file[0].name + " is not a valid image file.");
                            return false;
                        }
                    });
                } else {
                    alert("This browser does not support HTML5 FileReader.");
                }
            });

            // Upload File - with button
            $("#btnUpload").bind({
                click: function () {

                    $('#fuUpload').trigger('click');

                    return false;
                }
            });

            // Upload File - drag file
            $('.file-drag-area').on('dragover', function () {
                $(this).addClass('file-drag-over');
                return false;
            });
            $('.file-drag-area').on('dragleave', function () {
                $(this).removeClass('file-drag-over');
                return false;
            });
            $('.file-drag-area').on('drop', function (e) {

                e.preventDefault();

                $(this).removeClass('file-drag-over');

                var files = e.originalEvent.dataTransfer.files;

                reader.readAsDataURL(files[0]);

            });

            circleProgress = new CircleProgress('.upload-progress', {
                max: 100,
                value: 0,
                textFormat: 'percent',
                fill: { color: '#ffffff' }
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
