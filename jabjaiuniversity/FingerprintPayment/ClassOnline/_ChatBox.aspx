<%@ Page Title="" Language="C#" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="_ChatBox.aspx.cs"
    Inherits="FingerprintPayment.ClassOnline._ChatBox" %>




<%--    <div class="col-md-12 col-xs-12 row-desc">
        <div class="row bg-chat "> </div>
    </div>--%>
<div id="chatboxhist" class="col-md-12  bg-chat1 " style="overflow-y: scroll; height: 300px;">
    <% if (_model != null)
            foreach (var r in _model)
            { %>
    <div class="row chat-box <%= r.Type == 2 ? "text-right" : "text-left" %>">
        <div class="col-md-12">
            <strong class="chat1-head"><%= r.Type == 2 ? r.Teacher : r.Student %></strong>
        </div>
        <div class="col-md-12">
            <span class="chat1-date"><%= r.Date.Value.ToString(@"HH.mm dd\/MM\/yy" , new System.Globalization.CultureInfo("th-TH"))%> </span>
        </div>
        <div class="col-md-12">
            <div class="attach-container">
                <% if (!string.IsNullOrEmpty(r.Comment))
                    { %>
                <span class="chat1-content"><%= r.Comment %></span>
                <% }
                    else
                    { %>
                <% foreach (var f in r.Files)
                    { %>
                <% switch (f.Type)
                    { %>
                <%case "video": %>

                <div class="attach-item">
                    <a data-fancybox="gallery" href="<%=f.FileUrl %>#t=0.5">
                        <video width="130" controls>
                            <source type="<%=f.ContentType %>" src="<%=f.FileUrl %>">
                            Your browser does not support the video tag.
                        </video>
                    </a>
                </div>

                <% break; %>

                <% case "image": %>

                <div class="attach-item">
                    <a data-fancybox="gallery" href="<%=f.FileUrl %>">
                        <img src="<%=f.FileUrl %>" width="130" />
                    </a>
                </div>

                <% break; %>

                <% case "office":
                    case "pdf":%>
                <a target="_blank" class="" style="" href="<%=f.FileUrl %>"><%=f.FileTitle %></a>
                <br />
                <% break; %>

                <% default: break; %>
                <% } %>
                <% } %>
                <% } %>
            </div>
        </div>
    </div>

    <% } %>
</div>

<div class="col-md-12  bg-chat1" style="margin-top: 15px;">

    <% if (true) /*(Permission == JabjaiMainClass.PermissionType.Modify)*/
        { %>

    <div class="form-groupx" style="height: 40px; position: relative;">
        <textarea id="txtComment" class="form-control resizeable textboxstyle2 pull-left" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00166") %>"
            style="position: relative; left: 0; padding-right: 90px; height: 38px !important;" rows='1' data-min-rows='1' />
        <div id="uploadspinner" style="position: absolute; float: right; right: 70px; font-size: 25px; top: 5px; display: none">
            <i class="fa fa-spinner fa-spin"></i>
        </div>
        <input id="browseChat" type="file" multiple>
        <button id="btnSend1" type="button" class="pull-left"
            style="padding: 0px 8px; position: absolute; right: -15px; border: 0; background: white; cursor: pointer;">
            <i class="material-icons " style="padding: 5px">send</i>
        </button>

    </div>

    <% }
    else
    { %>

    <% } %>
</div>

<script>

    $(document)
        .one('focus.resizeable', 'textarea.resizeable', function () {
            var savedValue = this.value;
            this.value = '';
            this.baseScrollHeight = this.scrollHeight;
            this.value = savedValue;
        })
        .on('input.resizeable', 'textarea.resizeable', function () {
            var minRows = this.getAttribute('data-min-rows') | 0, rows;
            this.rows = minRows;
            rows = Math.ceil((this.scrollHeight - this.baseScrollHeight) / 16);
            this.rows = minRows + rows;
        });

    $(function () {
        $('#btnSend1').on('click', function () {
            var txt = $('#txtComment').val() + '';
            $('#btnSend1').prop('disabled', true);
            SendText(txt);

        });

        var $chatfi = $("#browseChat").fileinput({
            browseIcon: '<i class="material-icons">attachment</i>',
            browseLabel: '',
            browseClass: "",
            fileType: ['mkv', 'avi', 'mov', 'wmv', 'mp4', 'pdf', 'gif', 'jpg', 'jpeg', 'png', 'txt', 'xls', 'xlsx', 'doc', 'docx', 'ppt', 'pptx'],
            uploadAsync: false,
            uploadUrl: "<%= ResolveClientUrl("ChatUploadFile.ashx") %>?wid=<%=wid %>&uid=<%=uid %>",
            dropZoneEnabled: false,
            showClose: false,
            showCaption: false,
            showRemove: false,
            showProgress: false,
            showUpload: false,
            showUploadedThumbs: false,
            showPreview: false,
            layoutTemplates: { progress: '' }
        }).on("filebatchselected", function (event, files) {
            //setTimeout(function () {
            $('#uploadspinner').show();
            $chatfi.fileinput("upload");
            //}, 1500);
        }).on('filebatchuploadcomplete', function (event, data) {
            LoadChat();
        });

        LoadChatComplete();
    });
</script>
