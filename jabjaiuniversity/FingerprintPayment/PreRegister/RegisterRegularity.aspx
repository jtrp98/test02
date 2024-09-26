<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="RegisterRegularity.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterRegularity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <link rel="stylesheet" href="/styles/style-std.css?v=<%=DateTime.Now.Ticks%>" />

    <link href="/assets/plugins/summernote/0.8.12/summernote-lite.css?v=<%=DateTime.Now.Ticks%>" rel="stylesheet" />

    <link href="assets/css/layout.css?v=<%=DateTime.Now.Ticks%>" rel="stylesheet" />

    <style>
        /* Modal */
        .modal {
            height: 100%;
            width: 100%;
            position: fixed;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            margin: auto;
            background: rgba(142,68,173,.2);
            z-index: 9999;
            color: #fff;
        }

        .spinner3 {
            float: left;
            margin: 0px 0 0 -25px;
            height: 28px;
            width: 28px;
            animation: rotate 0.8s infinite linear;
            border: 4px solid #26c6da;
            border-right-color: transparent;
            border-radius: 50%;
        }

        .div-file {
            display: inline;
        }

            .div-file input[type="file"] {
                height: 0px;
                opacity: 0 !important;
                position: absolute;
                width: 0px;
            }


        /*summernote*/
        .note-btn-group.open .note-dropdown-menu {
            display: block;
            font-size: 18px;
        }

        .note-popover .note-popover-content .note-dropdown-menu, .note-toolbar .note-dropdown-menu {
            min-width: 214px;
        }

        .note-fontname .note-current-fontname, .note-fontsize .note-current-fontsize {
            font-size: 18px;
        }

        #regularityTabs .nav-item a.nav-link {
            font-size: 1rem;
        }

        #tableData tbody tr td {
            text-align: center;
        }

        .note-editor .note-icon-caret {
            display: none;
        }

        .note-editor .dropdown-toggle:after {
            margin-right: 0px;
        }

        .note-editor textarea.note-codable {
            height: 300px !important;
        }

        .note-editable b, .note-editable strong {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00692") %>
            </p>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="card stdProfile">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">public</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00692") %></h4>
                </div>
                <div class="card-body">
                    <div class="toolbar">
                        <!-- Here you can write extra buttons/actions for the toolbar -->
                    </div>
                    <div class="panel with-nav-tabs panel-default">
                        <div class="panel-heading">
                            <ul class="nav nav-pills" role="tablist" id="regularityTabs">
                                <li class="nav-item active"><a class="nav-link active show" href="#tab1" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01587") %></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab2" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00349") %></a></li>
                            </ul>
                        </div>
                        <div class="panel-body" style="padding: 0px 0px 15px 0px;">
                            <div class="tab-content">
                                <div class="tab-pane fade in active show" id="tab1">
                                    <table id="tableData" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                <th style="width: 70%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></th>
                                                <th style="width: 25%" colspan="2" class="text-right"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <%=RegularityData%>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="tab-pane fade" id="tab2">
                                    <div>
                                        <form class="form-padding">
                                            <div class="row div-row-padding">
                                                <div class="col-md-12 mb-12">
                                                    <textarea id="txtEditor"></textarea>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row text-center">
                                                <button id="save" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                                                <button id="cancel" type="button"
                                                    class="btn btn-danger" data-dismiss="modal">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                                            </div>
                                            <div id="modalRegularity" class="modal">
                                                <div class="card" style="width: 300px; left: 50%; top: 50%; margin-left: -150px; margin-top: -100px;">
                                                    <div class="card-body text-center" style="padding-left: 15px;">
                                                        <div class="spinner3" style="margin: -3px 0 0 0px;"></div>
                                                        <span style="margin-left: 25px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132862") %></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type='text/javascript' src="/assets/plugins/summernote/0.8.16/summernote-lite.js"></script>

    <script type='text/javascript'>

        function SaveDataToDatabase(messageData, callbackRedirect) {

            InsertSaveSpin();

            $.ajax({
                async: false,
                type: "POST",
                url: 'RegisterRegularity.aspx/SaveExplanationData',
                data: JSON.stringify({ messageData: messageData }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    ClearSaveSpin();

                    var r = JSON.parse(result.d);
                    if (r.success) {

                        swal({
                            title: 'Done!',
                            text: 'Complete your process.',
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            showConfirmButton: true,
                            buttonsStyling: false
                        }).then(result => {
                            if (result.value) {
                                // handle Confirm button click
                                // result.value will contain `true` or the input value
                            } else {
                                // handle dismissals
                                // result.dismiss can be 'cancel', 'overlay', 'esc' or 'timer'
                            }
                            callbackRedirect();
                        });
                    }
                    else {
                        swal({
                            title: 'Warning!',
                            text: 'Warning Message - ' + r.message,
                            type: 'warning',
                            confirmButtonClass: "btn btn-warning",
                            buttonsStyling: false
                        });
                    }
                },
                error: OnError
            });
        }

        function OnError(xhr, errorType, exception) {

            ClearSaveSpin();

            var responseText;
            try {
                responseText = jQuery.parseJSON(xhr.responseText);
                var errorMessage = "[" + errorType + ", " + exception + "] Exception:" + responseText.ExceptionType + ", StackTrace:" + responseText.StackTrace + ", Message:" + responseText.Message;

                swal({
                    title: 'Error!',
                    text: 'Error Message - ' + errorMessage,
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
            } catch (e) {
                responseText = xhr.responseText;
                swal({
                    title: 'Error!',
                    text: 'Error Message - c' + responseText,
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
            }
        }

        function InsertSaveSpin() {
            $("#save").prepend('<div class="spinner" style="margin: -3px 0 0 -20px;"></div>');

            $("#modalRegularity").fadeToggle();
        }

        function ClearSaveSpin() {
            $("#save").children().remove(".spinner");

            $("#modalRegularity").fadeOut();
        }

        function OnErrorRegularity(xhr, errorType, exception) {

            var responseText;
            try {
                responseText = jQuery.parseJSON(xhr.responseText);
                var errorMessage = "[" + errorType + ", " + exception + "] Exception:" + responseText.ExceptionType + ", StackTrace:" + responseText.StackTrace + ", Message:" + responseText.Message;

                swal({
                    title: 'Error!',
                    text: 'Error Message - ' + errorMessage,
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
            } catch (e) {
                responseText = xhr.responseText;
                swal({
                    title: 'Error!',
                    text: 'Error Message - c' + responseText,
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
            }
        }

        $(document).ready(function () {

            $(document).unbind('keydown').bind('keydown', function (e) {
                if (e.which === 8) {
                    //e.preventDefault();
                }
            });

            $("#txtEditor").summernote({
                height: 300,                 // set editor height
                minHeight: null,             // set minimum height of editor
                maxHeight: null,             // set maximum height of editor
                focus: true,                  // set focus to editable area after initializing summernote
                toolbar: [
                    ['fontname', ['fontname']],
                    ['fontsize', ['fontsize']],
                    ['color', ['color']],
                    ['undo', ['undo', 'redo']],
                    ['style', ['bold', 'italic', 'underline', 'clear']],
                    ['link', ['link']],
                    ['font', ['strikethrough', 'superscript', 'subscript']],
                    ['para', ['paragraph', 'ul', 'ol']],
                    ['fullscreen', ['fullscreen', 'codeview']]
                ],
                tabsize: 2,
                hint: {
                    mentions: ['jayden', 'sam', 'alvin', 'david',
                        'kyrra', 'stacie'],
                    match: /\B@(\w*)$/,
                    search: function (keyword, callback) {
                        callback(jQuery.grep(this.mentions, function (item) {
                            return item.indexOf(keyword) === 0;
                        }));
                    },
                    content: function (item) {
                        return $('<span>').html('<strong class="mentionned">@'
                            + item + '</strong><span>&nbsp;</span>')[0];
                    }
                },
                callbacks: {
                    onFocus: function (contents) {
                        if ($('#txtEditor').summernote('isEmpty')) {
                            $("#txtEditor").summernote('code', '');
                        }
                    }
                }
            });

            $('#txtEditor').summernote('code', `<%=ExplanationData%>`);

            $("#save").bind({
                click: function () {

                    SaveDataToDatabase($("#txtEditor").summernote('code'), function () { });

                    return false;
                }
            });

            $('.div-file input[type="file"]').change(function () {

                var files = $(this).get(0).files;
                if (files.length > 0) {
                    if (files[0].size > 4096000) {
                        alert("Try to upload file less than 4MB!");
                        return;
                    }
                }
                else {
                    return;
                }

                var formData = new FormData();

                for (var i = 0; i < files.length; i++) {
                    formData.append("fileRegularity" + i, files[i]);
                }

                var lid = $(this).attr('data-lid');

                $.ajax({
                    type: 'POST',
                    url: 'Ashx/UploadFileRegularityHandler.ashx?lid=' + lid,
                    data: formData,
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    beforeSend: function () {
                        // Handle the beforeSend event
                        $('#divProgress' + lid).html('<div class="spinner3"></div>');
                    },
                    success: function (result) {
                        //do some tasks after upload
                        //console.log(result);

                        if (result.success) {
                            $('#divProgress' + lid).html('<i class="fa fa-check text-success" aria-hidden="true" style="font-size: 22px; vertical-align: middle; margin-right: 5px; margin-top: 3px;"></i>');
                        }
                        else {
                            $('#divProgress' + lid).html('');
                        }
                    },
                    error: function (response) {
                        //console.log(response);

                        $('#divProgress' + lid).html('');

                    }
                });

            });

            $('.regularity-remove').click(function () {

                var lid = $(this).attr('data-lid');

                $.ajax({
                    async: false,
                    type: "POST",
                    url: 'RegisterRegularity.aspx/RemoveRegularityData',
                    data: JSON.stringify({ lid: lid }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {

                        var r = JSON.parse(result.d);
                        if (r.success) {
                            //$('#divProgress' + lid).html('');

                            swal({
                                title: 'Done!',
                                text: 'Complete your process.',
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                showConfirmButton: true,
                                buttonsStyling: false
                            }).then(result => {
                                $('#divProgress' + lid).html('');
                                if (result.value) {
                                    // handle Confirm button click
                                    // result.value will contain `true` or the input value
                                } else {
                                    // handle dismissals
                                    // result.dismiss can be 'cancel', 'overlay', 'esc' or 'timer'
                                }
                            });
                        }
                        else {
                            swal({
                                title: 'Warning!',
                                text: 'Warning Message - ' + r.message,
                                type: 'warning',
                                confirmButtonClass: "btn btn-warning",
                                buttonsStyling: false
                            });
                        }
                    },
                    error: OnErrorRegularity
                });

            });

            $('.div-file .fa-cloud-upload').click(function () {

                $('input[data-lid=' + $(this).attr('data-lid') + ']').trigger('click');

                return false;
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
