<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ReadMail.aspx.cs" Inherits="FingerprintPayment.Modules.EduZone.ReadMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:MultiView ID="MvMainContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
            <style></style>
            <div class="row">
                <div class="col-md-12">
                    <p class="text-muted" style="font-size: small;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132472") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132471") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M403004") %>
                    </p>
                </div>
            </div>

            <div class="mailList row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header card-header-info card-header-icon">
                            <div class="card-icon">
                                <i class="material-icons">search</i>
                            </div>
                            <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M403003") %></label>
                                <div class="col-sm-3">
                                    <div class="form-group bmd-form-group">
                                        <input id="iptSearch" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M403003") %>">
                                    </div>
                                </div>
                                <div class="col-sm-1"></div>
                                <label class="col-sm-1 col-form-label"></label>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-2"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-11 mr-auto text-center">
                                    <button id="btnSearch" class="btn btn-info">
                                        <span class="btn-label">
                                            <i class="material-icons">search</i>
                                        </span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                    </button>
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

            <div class="mailList row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header card-header-warning card-header-icon">
                            <div class="card-icon">
                                <i class="material-icons">mail_outline</i>
                            </div>
                            <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M403004") %></h4>
                        </div>
                        <div class="card-body">
                            <div class="toolbar">
                                <!-- Here you can write extra buttons/actions for the toolbar -->
                            </div>
                            <div class="material-datatables">
                                <div id="datatables_wrapper" class="dataTables_wrapper dt-bootstrap4">
                                    <div class="row">
                                        <div class="col-sm-12 col-md-6">
                                            <div class="dataTables_length">
                                                <label>
                                                    Show
                                            <select id="datatables_length" aria-controls="datatables" class="custom-select custom-select-sm form-control form-control-sm" style="text-align-last: center;">
                                                <option value="10">10</option>
                                                <option value="20" selected>20</option>
                                                <option value="50">50</option>
                                                <option value="100">100</option>
                                            </select>
                                                    rows</label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-6">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="tableData" class="table table-no-bordered table-hover" cellspacing="0" width="100%" style="width: 100%">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                        <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106143") %></th>
                                                        <th style="width: 20%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M403005") %></th>
                                                        <th style="width: 35%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></th>
                                                        <th style="width: 5%" class="text-center disabled-sorting"></th>
                                                        <th style="width: 7%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %></th>
                                                        <th></th>
                                                        <th style="width: 8%" class="text-center"></th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 col-md-5">
                                            <div class="dataTables_info" role="status" aria-live="polite">Showing 1 to 10 of 40 rows</div>
                                        </div>
                                        <div class="col-sm-12 col-md-7">
                                            <div class="dataTables_paginate paging_full_numbers">
                                                <ul class="pagination">
                                                </ul>
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

        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <link rel="stylesheet" href="/styles/material-form.css?v=<%=DateTime.Now.Ticks%>" />
            <link href="/assets/plugins/summernote/0.8.16/summernote-lite.css?v=<%=DateTime.Now.Ticks%>" rel="stylesheet" />
            <style>
                #divAttachedFiles, #divSchools {
                    display: flex;
                    flex-wrap: wrap;
                    margin-top: 5px;
                    margin-left: 15px;
                }

                .attached-file, .choose-school {
                    min-width: 160px;
                    height: 40px;
                    border-radius: 1.2rem !important;
                    border: 1px solid #ccc;
                    padding: 1px;
                    display: flex;
                    width: fit-content;
                    margin-right: 5px;
                }

                .attached-file {
                    margin-bottom: 5px;
                }

                    .attached-file p, .choose-school p {
                        margin: 6px;
                        font-size: 14px;
                    }

                .icon-attached-file, .icon-choose-school {
                    width: 35px;
                    height: 35px;
                    color: white;
                    background-color: cornflowerblue;
                    padding: 7px 7px 7px 10px;
                }

                .school-reply.title, .office-reply.title {
                    margin-top: 10px !important;
                    margin-bottom: 0px !important;
                }

                .school-reply {
                    margin-bottom: 22px;
                }

                .office-reply {
                    margin-left: 42px;
                    margin-bottom: 22px;
                }

                .reply-action {
                    float: right;
                    margin-top: 32px;
                    margin-right: 5px;
                    font-weight: bold;
                    cursor: pointer;
                }

                .reply-input.show {
                }

                .reply-input.hide {
                    display: none;
                }

                /*summernote*/
                .dropdown-toggle::after {
                    display: none;
                }
                /*.note-btn-group.open .note-dropdown-menu {
                    display: block;
                    font-size: 18px;
                }

                .note-popover .note-popover-content .note-dropdown-menu, .note-toolbar .note-dropdown-menu {
                    min-width: 214px;
                }

                .note-fontname .note-current-fontname, .note-fontsize .note-current-fontsize {
                    font-size: 18px;
                }*/

                .note-modal-content .form-group input[type=file] {
                    opacity: 1;
                    position: initial;
                }

                .note-modal-content .bmd-form-group label {
                    color: #42515f;
                }

                .note-modal-content .bmd-form-group [class*=' bmd-label'] {
                    position: inherit;
                }

                .note-modal-content .note-modal-footer {
                    height: 55px;
                    padding: 7px 30px;
                }

                .div-text-span img {
                    cursor: pointer;
                }

            </style>
            <div class="row">
                <div class="col-md-12">
                    <p class="text-muted" style="font-size: small;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132472") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132471") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M403004") %>
                    </p>
                </div>
            </div>

            <div class="material-form row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header card-header-warning card-header-icon">
                            <div class="card-icon">
                                <i class="material-icons">mail_outline</i>
                            </div>
                            <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M403004") %></h4>
                        </div>
                        <div class="card-body" style="padding-left: 7%; padding-right: 3%; padding-bottom: 3%;">
                            <div class="toolbar">
                                <!-- Here you can write extra buttons/actions for the toolbar -->
                            </div>
                            <form class="form-padding">
                                <div class="row div-row-padding" style="">
                                    <div class="col-md-12 text-right">
                                        <asp:Literal ID="ltrMailDate" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="row div-row-padding" style="margin-bottom: 5px;">
                                    <div class="col-md-12">
                                        <div style="border: 1px solid #ccc; border-radius: 3px; display: flex;">
                                            <div class="col-form-label text-center" style="border: 2px solid #87ddeb; border-radius: 3px; margin: -1px; padding-bottom: 5px; width: 120px;">
                                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M403005") %></label>
                                            </div>
                                            <div class="div-text-span" style="padding-left: 15px;">
                                                <span class="span-data">
                                                    <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding" style="margin-bottom: 5px; <%=AttachedFilesDisplay%>">
                                    <div class="col-md-12">
                                        <div style="border: 1px solid #ccc; border-radius: 3px; display: flex;">
                                            <div class="col-form-label text-center" style="border: 2px solid #87ddeb; border-radius: 3px; margin: -1px; padding-bottom: 5px; width: 150px;">
                                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206375") %></label>
                                            </div>
                                            <div id="divAttachedFiles">
                                                <asp:Literal ID="ltrAttachedFiles" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding" style="">
                                    <div class="col-md-12 div-text-span">
                                        <div style="border: 1px solid #ccc; border-radius: 3px; padding-top: 25px;">
                                            <asp:Literal ID="ltrMailContent" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </div>

                            </form>
                        </div>
                        <!-- end content-->
                    </div>
                    <!--  end card  -->
                </div>
                <!-- end col-md-12 -->
            </div>
            <!-- end row -->

            <div class="row">
                <div class="col-md-12">
                    <div class="card" style="margin-top: 0px;">
                        <div class="card-body" style="padding-left: 7%; padding-right: 3%; padding-bottom: 30px;">
                            <div class="toolbar">
                            </div>
                            <div class="row" style="margin: 10px 0px 10px 0px;">
                                <div class="col-md-12">
                                </div>
                            </div>
                            <asp:Literal ID="ltrReply" runat="server"></asp:Literal>
                            <div class="row reply-input <%=ShowInputReply%>">
                                <div class="col-md-12">
                                    <hr style="border: 1px solid black;">
                                </div>
                            </div>
                            <div class="row reply-input <%=ShowInputReply%>" style="margin: 10px 0px 7px 0px;">
                                <div class="col-md-12">
                                    <h4 class="card-title" style="font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132469") %></h4>
                                </div>
                            </div>
                            <div class="row reply-input <%=ShowInputReply%>">
                                <div class="col-md-12">
                                    <textarea id="txtReplyContent"></textarea>
                                </div>
                            </div>
                            <div class="row reply-input <%=ShowInputReply%>">
                                <div class="col-md-12 text-right">
                                    <button id="btnReply" type="submit" class="btn btn-success" style="font-size: .95rem;" data-mail-id="<%=MailID%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132470") %></button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <asp:MultiView ID="MvScript" runat="server">
        <asp:View ID="ListScript" runat="server">
            <script type="text/javascript">
                var mailList = {
                    PageIndex: 0,
                    PageSize: 20,
                    PageCount: 0,
                    TotalRows: 0,
                    dt: null,
                    LoadListData: function () {
                        mailList.dt = $('.mailList #tableData').DataTable({
                            "processing": true,
                            "serverSide": true,
                            "info": false,
                            "searching": false,
                            "paging": false,
                            "stateSave": true,
                            "ajax": {
                                "url": "ReadMail.aspx/LoadMail",
                                "type": "POST",
                                "contentType": "application/json; charset=utf-8",
                                "data": function (d) {
                                    d.search = $(".mailList #iptSearch").val();
                                    d.page = mailList.PageIndex;
                                    d.length = mailList.PageSize;

                                    return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                                },
                                "dataSrc": function (json) {
                                    var jsond = $.parseJSON(json.d);
                                    return jsond.data;
                                },
                                "beforeSend": function () {
                                    // Handle the beforeSend event
                                    //$("#modalWaitDialog").modal('show');
                                },
                                "complete": function () {
                                    // Handle the complete event
                                    //$("#modalWaitDialog").modal('hide');
                                }
                            },
                            "columns": [
                                { "data": "no", "orderable": false },
                                {
                                    "data": "schools", "orderable": false,
                                    "render": function (data) {
                                        var resultRender = '';
                                        var jsonObj = $.parseJSON(data);
                                        $.each(jsonObj, function (i, row) {
                                            resultRender += '<br/>' + row.schoolName;
                                        });
                                        return resultRender.slice(5);
                                    }
                                },
                                { "data": "Title", "orderable": true },
                                { "data": "MailContent", "orderable": true },
                                {
                                    "data": "files", "orderable": false,
                                    "render": function (data) {
                                        var renderResult = '';
                                        if (data != null) {
                                            //var jsonObj = $.parseJSON(data);
                                            renderResult = '<i class="material-icons">attach_file</i>';
                                        }
                                        return renderResult;
                                    }
                                },
                                { "data": "MailDate", "orderable": true },
                                { "data": "mid", "orderable": true },
                            ],
                            "order": [[6, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 4, 5, 7] },
                                { "targets": [6], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<a href="#"><i class="fa fa-search" style="padding-right: 5px; font-size: 22px;"></i></a>';
                                    },
                                    "targets": 7
                                }
                            ],
                            "drawCallback": function (settings) {
                                //var json = settings.json;
                                var json = $.parseJSON(settings.json.d);
                                console.log(json);

                                mailList.PageCount = json.pageCount;
                                mailList.TotalRows = json.recordsTotal;

                                var pageLRSize = 3;
                                var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                                var previousDot = '';
                                var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                                var nextDot = '';
                                var elements = '';

                                if (mailList.PageIndex - pageLRSize > 1) {
                                    previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                                }

                                if (mailList.PageIndex + pageLRSize < json.pageCount - 1) {
                                    nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                                }

                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    if (pi == 0) {
                                        elements += '<li class="paginate_button page-item ' + (mailList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                        elements += previousDot;
                                    }
                                    else if (pi == json.pageCount - 1) {
                                        elements += nextDot;
                                        elements += '<li class="paginate_button page-item ' + (mailList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                    else if (mailList.PageIndex - pageLRSize <= pi && mailList.PageIndex + pageLRSize >= pi) {
                                        elements += '<li class="paginate_button page-item ' + (mailList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                    }
                                }

                                $('.pagination').html(previous + elements + next);

                                $('.dataTables_info').html('Showing ' + ((mailList.PageIndex * mailList.PageSize) + 1) + ' to ' + ((mailList.PageIndex * mailList.PageSize) + mailList.PageSize) + ' of ' + mailList.TotalRows + ' rows');
                            },
                            "createdRow": function (row, data, dataIndex) {
                            }
                        });

                        // order.dt search.dt
                        mailList.dt.on('draw.dt', function () {
                            mailList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (mailList.PageIndex * mailList.PageSize) + (i + 1) + '.';
                            });
                            mailList.dt.column(7, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var mid = mailList.dt.cells({ row: i, column: 6 }).data()[0];
                                $(cell).find(".fa-search").parent().attr("href", "ReadMail.aspx?v=form&mid=" + mid).attr("target", "_blank");
                            });
                        });
                    },
                    ReloadListData: function () {
                        mailList.dt.draw();
                    }
                }

                $(document).ready(function () {

                    // Searching, Pagination event
                    $('.mailList #btnSearch').click(function () {

                        mailList.ReloadListData();

                        return false;
                    });


                    $('.mailList #datatables_length').change(function () {

                        mailList.PageSize = parseInt($("#datatables_length").children("option:selected").val());
                        mailList.PageIndex = 0;

                        mailList.ReloadListData();

                        return false;
                    });

                    $('.mailList ul.pagination').on('click', 'li.paginate_button a', function () {

                        var pi = parseInt($(this).attr("data-dt-idx"));

                        if (pi == 100) {
                            if (mailList.PageIndex > 0) {
                                mailList.PageIndex--;
                                mailList.ReloadListData();

                                $('.pagination .paginate_button.page-item.active').removeClass('active');
                                $('.pagination .paginate_button.page-item a[data-dt-idx=' + mailList.PageIndex + ']').addClass('active');
                            }
                        }
                        else if (pi == 101) {
                            if (mailList.PageIndex < (mailList.PageCount - 1)) {
                                mailList.PageIndex++;
                                mailList.ReloadListData();

                                $('.pagination .paginate_button.page-item.active').removeClass('active');
                                $('.pagination .paginate_button.page-item a[data-dt-idx=' + mailList.PageIndex + ']').addClass('active');
                            }
                        }
                        else {
                            mailList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                            mailList.ReloadListData();

                            $('.pagination .paginate_button.page-item.active').removeClass('active');
                            $(this).addClass('active');
                        }

                        return false;
                    });

                    $('.mailList #tableData #datatables_previous').click(function () {

                        if (mailList.PageIndex > 0) {
                            mailList.PageIndex--;
                            mailList.ReloadListData();
                        }

                        return false;
                    });

                    $('.mailList #tableData #datatables_next').click(function () {

                        if (mailList.PageIndex < (mailList.PageCount - 1)) {
                            mailList.PageIndex++;
                            mailList.ReloadListData();
                        }

                        return false;
                    });


                    // Datatable Section
                    mailList.LoadListData();
                });

            </script>
        </asp:View>
        <asp:View ID="FormScript" runat="server">
            <script type='text/javascript' src="/assets/plugins/summernote/0.8.16/summernote-lite.js"></script>

            <script type='text/javascript'>

                function OnError(xhr, errorType, exception) {
                    var responseText;
                    try {
                        responseText = jQuery.parseJSON(xhr.responseText);
                        var errorMessage = "[" + errorType + ", " + exception + "] Exception:" + responseText.ExceptionType + ", StackTrace:" + responseText.StackTrace + ", Message:" + responseText.Message;

                        Swal.fire({
                            title: 'Error!',
                            text: 'Error Message - ' + errorMessage,
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            buttonsStyling: false
                        });
                    } catch (e) {
                        responseText = xhr.responseText;
                        Swal.fire({
                            title: 'Error!',
                            text: 'Error Message - c' + responseText,
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            buttonsStyling: false
                        });
                    }
                }

                $(document).ready(function () {

                    $("#txtReplyContent").summernote({
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
                            ['link', ['link', 'picture']],
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
                                if ($('#txtReplyContent').summernote('isEmpty')) {
                                    $("#txtReplyContent").summernote('code', '');
                                }
                            }
                        }
                    });

                    $(".reply-action").click(function () {
                        if ($('.reply-input').hasClass('hide')) {
                            $('.reply-input').removeClass('hide').addClass('show');
                        }
                        if ($(this).data('mode') == 'new') {
                            $('#txtReplyContent').summernote('code', '');
                        }
                        else if ($(this).data('mode') == 'edit') {
                            $('#txtReplyContent').summernote('code', $(this).parent().find('.div-text-span').html());
                        }
                    });

                    $("#btnReply").bind({
                        click: function () {

                            var replyId = 0;
                            var mode = 'new';
                            if ($('.reply-action').length > 0) {
                                replyId = $('.reply-action').data('reply-id');
                                mode = $('.reply-action').data('mode');
                            }

                            $.ajax({
                                async: false,
                                type: "POST",
                                url: 'ReadMail.aspx/SaveReplyMessage',
                                data: JSON.stringify({ mailId: $(this).data('mail-id'), replyRefId: replyId, replyMessage: $("#txtReplyContent").summernote('code'), mode: mode }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (result) {
                                    var r = JSON.parse(result.d);
                                    if (r.success) {
                                        Swal.fire({
                                            title: 'Done!',
                                            text: r.message,
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
                                            location.reload();
                                        });
                                    }
                                    else {
                                        Swal.fire({
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

                            return false;
                        }
                    });

                    $(".div-text-span img").click(function () {
                        $('<div>').css({
                            background: 'RGBA(0,0,0,.5) url(' + $(this).attr('src') + ') no-repeat center',
                            backgroundSize: 'auto 95%', // contain
                            width: '100%', height: '100%',
                            position: 'fixed',
                            zIndex: '10000',
                            top: '0', left: '0',
                            cursor: 'zoom-out'
                        }).click(function () {
                            $(this).remove();
                        }).appendTo('body');
                    });

                });

            </script>

        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
