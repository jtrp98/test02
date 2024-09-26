<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="OnlineReportEdit.aspx.cs"
    Inherits="FingerprintPayment.ClassOnline.OnlineReportEdit" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
  <%--  <link href="/bootstrap/bootstrap-chosen/bootstrap-chosen.css" rel="stylesheet" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <%-- <link href="//cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.css" rel="stylesheet">--%>
    <!-- load the CSS files in the right order -->

    <link href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/css/fileinput.min.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/themes/explorer-fa/theme.min.css" rel="stylesheet" />
    <link href="css/StyleSheet1.css?v=<%= DateTime.Now.ToString("ddMMyyyy")%>" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.min.css" />


    <style>
        .textboxstyle0 {
            /* font-size: 26px;*/
            background-color: #fff;
            border: 0;
            border-bottom: 1px solid #ccc;
        }

        .textboxstyle1 {
            /*  font-size: 28px;*/
            background-color: #fff;
            border: 0;
            border-bottom: 0px solid #ccc;
            box-shadow: none;
            padding: 0;

            background-image: linear-gradient(to top, #9c27b0 2px, rgba(156, 39, 176, 0) 2px), linear-gradient(to top, #FFF 1px, rgba(210, 210, 210, 0) 1px);
        }
        .textboxstyle2 {
            /*  font-size: 28px;*/
            background-color: #fff;
            border: 0;
            border-bottom: 0px solid #ccc;
            box-shadow: none;
            padding: 0;
        }
        .bg-chat {
            margin: 1px;
            padding: 20px;
            background-color: #f1f1f1;
        }

        .bg-chat1 {
            padding: 6px 20px;
            background-color: #fff;
        }

        .chat1-head {
            /*font-size: 24px;*/
        }

        .chat1-content {
            /*          font-size: 24px;*/
        }

        .chat1-date {
            font-size: 16px;
            /*font-style: italic;*/
        }

        .chat-box {
            margin-bottom: 12px;
        }


        .row-desc {
            border-bottom: 1px solid #ccc;
            padding: 0;
            margin-left: 15px;
            margin-right: 15px;
            margin-bottom: 20px;
        }

            .row-desc .pull-left {
                /* font-size: 26px;*/
                font-weight: bold;
            }

            .row-desc .pull-right {
                /* font-size: 26px;*/
            }

        .btn-smx {
            padding: 0px 8px !important;
        }

        .row-desc .pull-right {
            /*font-size: 22px;*/
        }

        .vert-linex {
            /*padding:0 20px;*/
            padding: 0 25px;
        }

        #upload-preview .file-caption-main {
            display: none !important;
        }

        .kv-file-content {
            display: none;
        }

        .fileinput-remove {
            display: none;
        }

        #upload-preview .kv-file-remove {
            display: none !important;
        }

       /* .swal2-popup {
            font-size: 1.8rem !important;
        }*/

       /* .swal2-confirm, .swal2-cancel {
            font-size: 2rem !important;
        }
*/
        #chatbox-wrapper .file-input {
            display: inline-block;
            right: 35px;
            float: right;
            top: -35px;
        }

            #chatbox-wrapper .file-input i.material-icons {
                font-size: 30px;
            }

        #chatbox-wrapper .fileinput-cancel {
            display: none !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card ">
        <div class="col-md-12">
            <form id="from1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>
                <div class="full-card box-content smssetting-container">
                    <div class="row">
                        <div class="col-md-12">
                            <h3><span class="material-icons" style="color: #f19f3a;">home_work</span>&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206345") %></h3>
                        </div>
                    </div>
                    <br />
                    <div class="row" style="margin: 0 10px;">
                        <div class="col-md-6 vert-linex">
                            <div class="row">
                                <p class="col-md-12 row-desc">
                                    <span class="pull-left --bold"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></span>
                                    <span class="pull-right "><%= _model.FullName %></span>
                                </p>
                            </div>

                            <div class="row">
                                <p class="col-md-12 row-desc">
                                    <span class="pull-left --bold"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206374") %></span>

                                    <span class="pull-right ">
                                        <% if (!string.IsNullOrEmpty(_model.Link))
                                            {
                                                Uri uri;
                                                var res = Uri.IsWellFormedUriString(_model.Link, UriKind.Absolute);//base.ValidHttpURL(_model.Link, out uri); 
                                        %>

                                        <% if (res)
                                            { %>
                                        <a href="<%= _model.Link %>" target="_blank" class="btn btn-info btn-smx"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132072") %></a>
                                        <% }
                                            else
                                            { %>
                                        <a href="#<%= _model.Link %>" class="btn btn-info btn-smx"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132072") %></a>
                                        <% }%>
                                        <% }
                                            else
                                            { %>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01392") %>
                                <% } %>
                                    </span>
                                </p>
                                <% if (!string.IsNullOrEmpty(_model.Link))
                                    { %>

                                <div class="col-md-12">
                                    <div id="thumb" style="margin: 5px 5px 15px 5px;"></div>
                                </div>

                                <% } %>
                            </div>

                            <%  var _arr = new List<string>() { ".png", ".jpg", ".jpeg" }; %>
                            <div class="row">
                                <p class="col-md-12 row-desc">
                                    <span class="pull-left --bold"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206375") %></span>
                                    <span class="pull-right ">
                                        <% if (_model.Files != null && _model.Files.Count > 0)
                                            { %>

                                        <% }
                                            else
                                            {%>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01388") %>
                                <% } %>
                                    </span>
                                </p>
                            </div>
                            <% if (_model.Files != null && _model.Files.Count > 0)
                                {%>

                            <div class="row">
                                <div id="upload-preview" class="col-md-12 row-desc" style="border: 0px;">
                                    <input id="fileUploadPreview" name="" type="file" class="" data-msg-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106018") %>">
                                </div>
                            </div>
                            <% } %>
                        </div>
                        <div class="col-md-6 vert-linex">

                            <%-- <div class="row">
                                <label class="col-sm-2 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203056") %></label>
                                <div class="col-sm-10">
                                    <div class="form-group has-success">

                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ID="txtScore" CssClass="form-control input-group-addon textboxstyle1 text-right" TextMode="Number" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %>" Style="text-align: right; border: 0px;" Width="70px" />
                                                <span class="form-control-feedback">/<%= _hw.MaxScore.HasValue ? _hw.MaxScore+"" : "-" %>
                                                    <asp:Button Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" runat="server" ID="btnSave" CssClass="btn btn-success search-btn" OnClick="btnSave_Click" Style="margin-left: 10px; margin-bottom: 2px; padding: 4px 14px 2px 14px;" />
                                                </span>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                            </div>--%>

                            <div class="row">

                                <div class="col-md-12 col-xs-12 row-desc">
                                    <span class="pull-left --bold"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203056") %></span>
                                    <span class="pull-right ">

                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" style="display:inline-flex;">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ID="txtScore" CssClass="form-control input-group-addon textboxstyle1 text-right" TextMode="Number" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %>" Style="text-align: right; border: 0px;font-size: 16px;" Width="70px" />
                                                <span style="font-size: 16px;    padding: 7px 0px 0 0;"> / <%= _hw.MaxScore.HasValue ? _hw.MaxScore+"" : "-" %>
                                                </span>

                                                <% if (true)/*(Permission == JabjaiMainClass.PermissionType.Modify)*/ { %>
                                                   <asp:Button Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" runat="server" ID="btnSave" CssClass="btn btn-success search-btn" OnClick="btnSave_Click" Style="margin-left: 10px; margin-bottom: 2px; padding: 4px 14px 2px 14px;" />
                                                 <% }else { %>
                                                    <button class="btn btn-success " type="button" onclick="NoPermissionPopup()" style="margin-left: 10px; margin-bottom: 2px; padding: 4px 14px 2px 14px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                                                 <% } %>
                                             
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </span>
                                </div>
                            </div>
                            <div class="col-md-12 col-xs-12 row-desc">
                                <div class="row bg-chat ">
                                    <div id="chatbox-wrapper" class="col-md-12 " style="overflow: hidden;">
                                    </div>
                                </div>
                            </div>
                            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSend1" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSend1">

                            <div class="row">
                                <div class="col-md-12 col-xs-12 row-desc">
                                    <div class="row bg-chat ">
                                        <div id="chatboxhist" class="col-md-12  bg-chat1 " style="overflow-y: scroll; height: 300px;">
                                            <% if (_model.Comment != null)
                                                    foreach (var r in _model.Comment)
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
                                                        <a target="_blank" class="" style="font-size: 14px;" href="<%=f.FileUrl %>"><%=f.FileTitle %></a>
                                                        <br />
                                                        <% break; %>

                                                        <% default: break; %>
                                                        <%} %>
                                                        <% } %>
                                                        <% } %>
                                                    </div>
                                                </div>
                                            </div>

                                            <% } %>
                                        </div>

                                        <div class="col-md-12  bg-chat1" style="margin-top: 15px;">

                                            <div class="form-groupx" style="height: 40px; position: relative;">
                                                <asp:TextBox runat="server" ID="txtComment" CssClass="form-control resizeable textboxstyle1 pull-left" TextMode="MultiLine" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00166") %>"
                                                    Style="font-size: 26px; position: absolute; left: 0; padding-right: 70px;" Rows='1' data-min-rows='1' />
                                                <asp:LinkButton runat="server" ID="btnSend1" CssClass="btn btn-default pull-left" OnClick="btnSend1_Click"
                                                    Style="padding: 0px 8px; position: absolute; right: 0px;">
                                            <i class="material-icons " 
                                                style="padding:5px" >send</i>                            
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>--%>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group text-center">
                                <a class="btn btn-default search-btn" href="OnlineReport.aspx?id=<%=Request.QueryString["wid"] %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></a>

                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <%--    <script src="//cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.js"></script>--%>

   <%-- <script src="js/chosen.jquery.js?v=<%= DateTime.Now.ToString("ddMMyyyy")%>"></script>--%>
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/js/fileinput.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/themes/explorer-fa/theme.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/js/plugins/piexif.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/js/plugins/purify.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" type="text/javascript"></script>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@9"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.min.js"></script>

    <script src="js/func.js?v=<%= DateTime.Now.ToString("ddMMyyyyHHmm")%>"></script>
    <script src="js/jquery.fitvids.js"></script>
    <script src="js/prettyyt.js?v=<%= DateTime.Now.ToString("ddMMyyyyHHmm")%>"></script>
    <script src="js/jQueryRotate.js"></script>
    <%-- <script type="text/javascript" src="//cdn.rawgit.com/igorlino/elevatezoom-plus/1.1.6/src/jquery.ez-plus.js"></script>--%>
    <%--<script type="text/javascript" src="https://cdn.rawgit.com/igorlino/elevatezoom-plus/1.1.6/src/jquery.ez-plus.js"></script>--%>

    <%-- <script src="https://cdn.jsdelivr.net/gh/igorlino/elevatezoom-plus@1.2.3/src/jquery.ez-plus.js"></script>--%>

    <%--    <script src="js/jquery.izoomify.js"></script>--%>
    <script>
        var _angle = 0;
        var _zoom;
       <%-- Sys.Application.add_init(function () {

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_initializeRequest(function (sender, args) {

            });

            prm.add_pageLoaded(function (sender, args) {

                if (sender._postBackSettings != null && sender._postBackSettings.panelsToUpdate == '<%= UpdatePanel1.UniqueID %>') {
                    Swal.fire({
                        title: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132071") %>",
                        type: "success",
                    });
                }

            });

        });--%>
        function LoadChat() {
            $.get('_ChatBox.aspx?wid=<%=wid %>&uid=<%=uid %>', function (data) {
                $('#chatbox-wrapper').html(data);

                //$("#chatboxhist").scrollTop($("#chatboxhist")[0].scrollHeight);
            });
        }

        function SendText(txt) {
            if (txt != '') {
                PageMethods.SendText(txt,<%=wid%>,<%=uid%>,
                    function (response) {
                        LoadChat();
                    },
                    function (response) {
                        //faild
                    });
            }
        }

        function alertsuccess() {
            Swal.fire({
                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00900") %>',
                icon: 'success',
            });
        }

        function loadThumbnail(url) {
            //var id = '';
            //var url = $(this).val();
            if (url != '') {
                id = youtube_parser(url);

                if (id != '') {
                    $('#thumb').html('<div id="ttt"></div>');

                    $('#ttt').prettyEmbed({
                        videoID: id,
                        //previewSize: 'hd',	
                        useFitVids: true,

                        playerControls: true,
                        playerInfo: false
                    });
                }
            }
        }

        function LoadChatComplete() {
            //scrollToBottom('chat-hist');
            //$("#chatboxhist").scrollTop($("#chatboxhist")[0].scrollHeight);

            //$("video").on("loadstart", function () {
            //    //console.log(3);
            //    $("#chatboxhist").scrollTop($("#chatboxhist")[0].scrollHeight);
            //});

            setTimeout(function () {
                $("#chatboxhist").scrollTop($("#chatboxhist")[0].scrollHeight);
            }, 1000);
        }

        $(function () {

            loadThumbnail('<%=_model.Link%>');
            LoadChat();
            //LoadChatComplete();
            var $prev1 = $("#fileUploadPreview");
            $prev1.fileinput({
                //uploadUrl: "#",
                append: true,
                //deleteUrl: "@(Url.Action("DummeyFile"))",
                theme: "explorer-fa",
                uploadAsync: true,
                allowedFileExtensions: ['mkv', 'avi', 'mov', 'wmv', 'mp4', 'gif', 'pdf', 'jpg', 'jpeg', 'png', 'txt', 'xls', 'xlsx', 'doc', 'docx', 'ppt', 'pptx'],
                overwriteInitial: false,
                initialPreviewAsData: true,
                maxFileSize: 25000,
                removeFromPreviewOnError: true,
                dropZoneEnabled: false,
                showUpload: false,
                showCaption: true,
                showRemove: false,
                purifyHtml: true, // this by default purifies HTML data for preview

                initialPreview: [
                    <%if (_model.Files != null)
            foreach (var f in _model.Files)
            { %> "<%= f.File %>", <%} %>
                ],
                initialPreviewConfig: [
                    <% if (_model.Files != null)
            foreach (var f in _model.Files)
            { %>
                    {
                        type: "<%= f.Type %>",
                        caption: "<%= f.TitleFile %>",
                        downloadUrl: "<%= f.File %>",
                        <% if (f.Type == "video")
        {%>
                        filetype: "<%=f.ContentType %>",
                        <% } %>     
                        // width: "200px",
                        //height: "200px",
                    },
                    <%} %>
                ],
            })
                .on('filezoomprev filezoomnext filezoomshown', function (event, params) {
                    _angle = 0;

                    var $img = $('#kvFileinputModal img');

                    if ($img.length > 0) {
                        var _html = '<div id="rotateTool">\
                        <button id="rotate1" class"btn btn-sm btn-default "><span class="material-icons">rotate_left</span></button>\
                        <button id="rotate2" class"btn btn-sm btn-default "><span class="material-icons">rotate_right</span></button>\
                        <button id="zoom1" class"btn btn-sm btn-default "><span class="material-icons">zoom_in</span></button>\
                        <button id="zoom2" class"btn btn-sm btn-default "><span class="material-icons">zoom_out</span></button>\
</div>';
                        // $("#kvFileinputModal .kv-zoom-body").prepend(_html);
                        //var $img = $('#kvFileinputModal img');
                        //$img.attr('data-zoom-image', $img.attr('src'));
                        var _url = $img.attr('src');
                        $("#kvFileinputModal .kv-zoom-body").html(_html + "<div id='zoomWrapper'>" + $img.prop('outerHTML') + "</div>")

                    }
                   
                    // $("#zoomWrapper img").ezPlus();
                    ////$img.attr('data-zoom-image', $img.attr('src'));

                    //$('#zoomWrapper').izoomify({
                    //    url: _url,
                    //    magnify: 2.5,
                    //});



                })
                //.on('filezoomnext', function (event, params) {
                //    $("#kvFileinputModal .kv-zoom-body").append("<div>1</div>")
                //})
                //.on('filezoomshown', function (event, params) {
                //   //alert('File zoom shown '+ params.previewId + '+ params.modal);
                //    $("#kvFileinputModal .kv-zoom-body").append("<div>1</div>")
                //})
                ;


            $('#kvFileinputModal').addClass('file-zoom-fullscreen');
            $('#kvFileinputModal .btn-borderless').addClass('active');

            $("#kvFileinputModal").on('click', "#rotate1", function () {
                _angle = _angle - 90;
                $("#kvFileinputModal .kv-zoom-body img.file-preview-image").rotate({ animateTo: _angle });
            });

            $("#kvFileinputModal").on('click', "#rotate2", function () {
                _angle = _angle + 90;
                $("#kvFileinputModal .kv-zoom-body img.file-preview-image").rotate({ animateTo: _angle });

            });

            $("#kvFileinputModal").on('click', "#zoom1", function () {
                var $img = $("#kvFileinputModal .kv-zoom-body img");
                var w = $img.width() + 300;
                //$img.css('width', w + 'px !important');
                $img[0].style.setProperty('width', w + 'px', 'important');
                //$img.animate({ width: w + "px !important;" , }, "fast");
            });

            $("#kvFileinputModal").on('click', "#zoom2", function () {
                var $img = $("#kvFileinputModal .kv-zoom-body img");
                var w = $img.width() - 300;
                $img[0].style.setProperty('width', w+'px', 'important');
                //$img.css('width', '');
                //var _style = $img.attr('style');

                //$img.css('width',  w + 'px !important');
                //$img.animate({ width: w + "px !important;", }, "fast");
            });
            //$(document)
            //    .one('focus.resizeable', 'textarea.resizeable', function () {
            //        var savedValue = this.value;
            //        this.value = '';
            //        this.baseScrollHeight = this.scrollHeight;
            //        this.value = savedValue;
            //    })
            //    .on('input.resizeable', 'textarea.resizeable', function () {
            //        var minRows = this.getAttribute('data-min-rows') | 0, rows;
            //        this.rows = minRows;
            //        rows = Math.ceil((this.scrollHeight - this.baseScrollHeight) / 20);
            //        this.rows = minRows + rows;
            //    });
        });
    </script>
</asp:Content>
