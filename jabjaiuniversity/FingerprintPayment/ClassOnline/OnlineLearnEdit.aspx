﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="OnlineLearnEdit.aspx.cs"
    Inherits="FingerprintPayment.ClassOnline.OnlineLearnEdit" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
  <%--  <link href="/bootstrap/bootstrap-chosen/bootstrap-chosen.css" rel="stylesheet" />--%>
   <%-- <link href="/Styles/jquery-ui.css" rel="stylesheet" />--%>
    <!-- load the CSS files in the right order -->

    <link href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/css/fileinput.min.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/themes/explorer-fa/theme.min.css" rel="stylesheet" />

    <link href="css/StyleSheet1.css?v=<%= DateTime.Now.ToString("ddMMyyyy")%>" rel="stylesheet" />

    <!-- DatetimePicker -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.css" />


    <!-- Timepicker -->
    <link href="css/timepicker.min.css" rel="stylesheet" />


    <%--  <style>
        .vert-line {
            border-right: 2px solid black;
            padding-right: 40px;
            margin-right: 40px;
        }

        .ui-datepicker-div {
            z-index: 9;
        }

        .fileUpload1 {
            width: 100%;
        }

        .file-caption-main .btn {
            padding: 0 6px;
        }

        .kv-file-content {
            display: none;
        }

        .file-details-cell samp {
            
        }

        .file-actions-cell {
            width: 60px;
        }

        .file-input .glyphicon {
            
        }

        .chosen-container-multi .chosen-choices .search-choice .search-choice-close {
            background-size: 440%; /*WTF this*/
        }
        /*.kv-file-remove,.file-caption-icon{
            display:none !important; 
        }*/
        .-custom-fixx {
            display: block !important;
            width: 0 !important;
            height: 0 !important;
            border: 1px solid #fff;
            position: relative;
            top: 35px;
            /*left: 80px;*/
            margin: 0 auto;
        }

        .topic__spinner {
            position: absolute;
            top: 45px;
            right: -20px;
        }

        #link-wrapper .linkrow {
            margin-bottom: 5px;
        }

        #thumb, .pretty-embed {
            padding: 0;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card ">
        <div class="col-md-12">

            <form id="from1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>
                <div class="full-card box-content smssetting-container">
                    <div class="row">
                        <div class="col-md-12">
                            <h3><span class="material-icons" style="color: #f19f3a; font-size: 30px;">home_work</span>&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206345") %></h3>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-7 vert-line">
                            <div class="form-group">
                                <label for=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206370") %></label>
                                <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206371") %>" Style="" required />
                            </div>
                            <div class="form-group">
                                <label for=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></label>
                                <asp:TextBox runat="server" ID="txtDetail" CssClass="form-control" TextMode="MultiLine" Rows="6" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206356") %>" Style="" required />
                            </div>
                            <div id="attachlink" class="form-groupx">
                                <label for="" style="color:black"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206357") %>  </label>
                                  <button type="button" onclick="addnewlink()" class="btn btn-success search-btn" style="height: 26px; width: 26px; padding: 3px; outline: none;">
                                    <span class="material-icons">add</span>
                                </button>
                                <div id="link-wrapper" class="row">
                                    <div class="linkhrow-wrapper col-md-12 p-0">
                                        <div class="row">
                                            <div class="col-md-2 linkrow p-0">
                                                <button type="button" onclick="removelink(this)" class="btn btn-danger search-btn" style="height: 26px; width: 26px; padding: 3px; outline: none;">
                                                    <span class="material-icons">remove</span>
                                                </button>
                                                <span class='rownum'>1</span>
                                            </div>
                                            <div class="col-md-10 linkrow p-0">
                                                <input type="text" class="attach-link form-control" style="" />
                                                <div id="thumb" class="col-md-12"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:TextBox runat="server" ID="txtLink" CssClass="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132064") %>" Style="display: none;" />
                            </div>
                            <br />
                            <div class="form-group">
                                <label for="" style="position: relative;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206358") %></label>
                                <%-- <asp:FileUpload ID="FileUpload2" runat="server" multiple class="fileUpload1" />--%>
                                <input id="fileUpload1" name="fileUpload1[]" runat="server" type="file" multiple class="fileUpload1" data-msg-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106018") %>">

                                <span style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206360") %></span><br />
                                <%--<span style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132063") %></span>--%>

                                <asp:TextBox runat="server" ID="tbFiles" CssClass="form-control d-none" TextMode="MultiLine" />
                                <asp:TextBox runat="server" ID="tbFilesDel" CssClass="form-control d-none" />
                            </div>
                        </div>
                        <div class="col-md-4 vert-linex">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label for=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02259") %></label>
                                        <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="true" required
                                             title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206363") %>"
                                            DataTextField="Text"
                                            DataValueField="Value"
                                             data-width="100%"
                                            CssClass="chosen-selectx selectpicker1  -custom-fixx"
                                            data-style="select-with-transition" data-live-search="true"
                                            OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <i class="fa fa-spinner fa-spin topic__spinner" style="display: none" aria-hidden="true"></i>
                                    </div>
                                    <div class="form-group">
                                        <label for=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401048") %></label>
                                        <asp:DropDownList ID="ddlType" runat="server"
                                             title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206361") %>"
                                            DataTextField="Text"
                                            DataValueField="Value"
                                             data-width="100%"
                                            CssClass="chosen-selectx selectpicker1  choose-type1"
                                            data-style="select-with-transition" data-live-search="true">
                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206366") %>" Value="1" />
                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206367") %>" Value="2" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group chosen-wrap-here" id="selectType1">
                                        <label for=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %></label>
                                        <asp:ListBox ID="ddlLevel" runat="server" SelectionMode="Multiple"
                                              title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206362") %>"
                                            DataTextField="Text"
                                            DataValueField="Value"
                                             data-width="100%"
                                            CssClass="chosen-selectx selectpicker1  -custom-fixx"
                                            data-style="select-with-transition" data-live-search="true"></asp:ListBox>
                                    </div>
                                    <div class="form-group chosen-wrap-here" id="selectType2" style="display: none">
                                        <label for=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206369") %></label>
                                        <asp:ListBox ID="ddlStudent" runat="server" SelectionMode="Multiple"
                                               title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206368") %>"
                                            DataTextField="Text"
                                            DataValueField="Value"
                                             data-width="100%"
                                            CssClass="chosen-selectx selectpicker1  -custom-fixx"
                                            data-style="select-with-transition" data-live-search="true"></asp:ListBox>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="form-group">
                                <label for=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206350") %></label>
                                <asp:DropDownList ID="ddlDisplay" runat="server"
                                        title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206372") %>"
                                    ClientIDMode="Static"
                                    DataTextField="Text"
                                    DataValueField="Value"
                                     data-width="100%"
                                    CssClass="chosen-selectx selectpicker1 "
                                    data-style="select-with-transition" data-live-search="true">
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206364") %>" Value="1" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206365") %>" Value="2" />
                                </asp:DropDownList>
                            </div>

                            <div id="displayDate-wrapper" class="form-group" style="display: none;">
                                <div class="row" style="margin: 0">
                                    <div class="col-md-8" style="padding-left: 0; padding-right: 5px;">
                                        <div class="form-group has-successx" style="margin:8px 0px;">
                                            <asp:TextBox runat="server" ID="txtDisplayDate" ClientIDMode="Static" CssClass="form-control datepicker bs-datepicker --date-validate" Style="" autocomplete="off" />
                                            <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                                <i class="material-icons">event</i>
                                            </span>
                                        </div>

                                    </div>
                                    <div class="col-md-4" style="padding-left: 5px; padding-right: 0;">
                                        <asp:TextBox runat="server" ID="txtDisplayTime" ClientIDMode="Static" CssClass="form-control bs-timepicker" Style="height: 44px;" autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <%-- <div id="displayDate-wrapper" class="form-group" style="display: none">
                        <div class="col-md-8" style="padding-left: 0; padding-right: 5px;">
                            <div class='input-group date' id='divDisplayDate'>
                                <asp:TextBox runat="server" ID="txtDisplayDate" ClientIDMode="Static" CssClass="form-control bs-datepicker --date-validate" Style="background: white;" autocomplete="off" />
                                <span class="input-group-addon">
                                    <i class="glyphicon glyphicon-calendar"></i>
                                </span>
                            </div>

                        </div>
                        <div class="col-md-4" style="padding-left: 5px; padding-right: 0;">
                            <asp:TextBox runat="server" ID="txtDisplayTime" ClientIDMode="Static" CssClass="form-control bs-timepicker" Style="height: 41px; background: white;" autocomplete="off" />
                        </div>

                    </div>--%>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group text-center">

                                <% if (_class != null)
                                    { %>
                                <a class="btn btn-default search-btn" href="OnlineManage.aspx?term=<%=_class.TermId.Trim() %>&plan=<%=_class.PlanId%>&level=<%=_class.LevelId %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></a>
                                <% }%>

                                <asp:Button Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" runat="server" ID="btnSave" CssClass="btn btn-success search-btn" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

  <%--  <script src="js/chosen.jquery.js?v=<%= DateTime.Now.ToString("ddMMyyyy")%>"></script>--%>
   <%-- <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>
    <!-- load the JS files in the right order -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/js/fileinput.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/themes/explorer-fa/theme.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/js/plugins/piexif.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/js/plugins/purify.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" type="text/javascript"></script>
    <script type='text/javascript' src="/assets/plugins/bootstrap-datetimepicker/moment-with-locales.js"></script>
    <script type='text/javascript' src="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.th.min.js"></script>
    <script src="js/timepicker.min.js"></script>

    <script src="js/func.js?v=<%= DateTime.Now.ToString("ddMMyyyyHHmm")%>"></script>
    <%--   <script src="js/previewtube.js"></script>--%>
    <script src="js/jquery.fitvids.js"></script>
    <script src="js/prettyyt.js?v=<%= DateTime.Now.ToString("ddMMyyyyHHmm")%>"></script>
    <script>


        Sys.Application.add_init(function () {

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_initializeRequest(function (sender, args) {
                if (sender._postBackSettings != null && sender._postBackSettings.panelsToUpdate == '<%= UpdatePanel1.UniqueID %>') {
                    $('.topic__spinner').show();
                }
            });

            prm.add_pageLoaded(function (sender, args) {

                if (!isMobileAndTabletCheck())
                    //$('.chosen-select').chosen();
                    $('.selectpicker1').selectpicker();
                else {
                    //$(".chosen-select").removeClass('-custom-fixx');
                }

                $('.choose-type1').on('change', function (e) {
                    let _value = $(this).children("option:selected").val();

                    if (_value == "1") {
                        $('#selectType1').show();
                        $('#selectType2').hide();
                        $('#<%= ddlLevel.ClientID%>').attr('required', 'true');
                        $('#<%= ddlStudent.ClientID%>').removeAttr('required');
                    }
                    else if (_value == "2") {
                        $('#selectType1').hide();
                        $('#selectType2').show();
                        $('#<%= ddlStudent.ClientID%>').attr('required', 'true');
                        $('#<%= ddlLevel.ClientID%>').removeAttr('required');
                    }
                });

                $('.choose-type1').trigger('change');

                __validator();

            });

        });

        //function pageLoad(sender, args) {
        //}

        function preventMultipleSubmissions() {
            //$('#<%=btnSave.ClientID %>').text('')
            $('#<%=btnSave.ClientID %>').prop('disabled', true);
        }

        window.onbeforeunload = preventMultipleSubmissions;

        var $el1;

        $(function () {

            $('#datepicker').datetimepicker({
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
                debug: false,
                //autoclose: true,
                //autoclose: true,
                //showOn: "button",
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-chevron-up",
                    down: "fa fa-chevron-down",
                    previous: 'fa fa-chevron-left',
                    next: 'fa fa-chevron-right',
                    today: 'fa fa-screenshot',
                    clear: 'fa fa-trash',
                    close: 'fa fa-remove'
                }
            });

            $('#txtDisplayTime').timepicker();

            $(".bs-datepicker").keydown(function (e) {
                e.preventDefault();
            });

            <%-- $('.chosen-select').chosen();

            $('.choose-type1').on('change', function (e) {
                let _value = $(this).children("option:selected").val();

                if (_value == "1") {
                    $('#selectType1').show();
                    $('#selectType2').hide();
                    $('#<%= ddlLevel.ClientID%>').attr('required', 'true');
                    $('#<%= ddlStudent.ClientID%>').removeAttr('required');
                }
                else if (_value == "2") {
                    $('#selectType1').hide();
                    $('#selectType2').show();
                    $('#<%= ddlStudent.ClientID%>').attr('required', 'true');
                    $('#<%= ddlLevel.ClientID%>').removeAttr('required');
                }
            });
            $('.choose-type1').trigger('change');--%>

            $el1 = $(".fileUpload1");
            $el1.fileinput({
                uploadUrl: "<%= ResolveUrl("~/ClassOnline/WorkUploadFile.ashx") %>",
                append: true,
                deleteUrl: "<%= ResolveUrl("~/ClassOnline/WorkUploadFile.ashx") %>",
                theme: "explorer-fa",
                uploadAsync: true,
                allowedFileExtensions: ['mkv', 'avi', 'mov', 'wmv', 'mp4', 'pdf', 'gif', 'jpg', 'jpeg', 'png', 'txt', 'xls', 'xlsx', 'doc', 'docx', 'ppt', 'pptx'],
                overwriteInitial: false,
                initialPreviewAsData: true,
                maxFileSize: 25000,
                removeFromPreviewOnError: true,
                dropZoneEnabled: false,
                showUpload: false,
                showCaption: true,
                purifyHtml: true,
                showRemove: false,

                initialPreview: [
                <% foreach (var f in files)
        { %>
                    "<%=f.File %>",
                <% } %>
                ],
                initialPreviewConfig: [
                     <% foreach (var f in files)
        { %>
                    {
                        type: "<%= f.Type %>",
                        caption: "<%=f.TitleFile %>",
                        downloadUrl: "<%=f.File%>",
                         <% if (f.Type == "video")
        {%>
                        filetype: "<%=f.ContentType %>",
                        <% } %>     
                        //width: "120px",
                        key: "<%=f.Id %>",
                    }
                    ,
                <% } %>],


            }).on("filebatchselected", async function (event, files) {
                //$el1.fileinput("upload");
                //console.log($el1.fileinput('getFileList'));            
                await processFile();

            }).on('filebatchuploadsuccess', function (event, data, previewId, index) {

            }).on('fileremoved', async function (event, id, index) {
                //console.log($el1.fileinput('getFileList'));
                await processFile();

            }).on('filedeleted', function (event, key, jqXHR, data) {
                //console.log('Key = ' + key);
                $('#<%= tbFilesDel.ClientID %>').val($('#<%= tbFilesDel.ClientID %>').val() + ',' + key);

            });;

            //$('.choose-type1').on('change', function (e) {
            //    let _value = $(this).children("option:selected").val();

            //    if (_value == "1") {
            //        $('#selectType1').show();
            //        $('#selectType2').hide();
            //    }
            //    else if (_value == "2") {
            //        $('#selectType1').hide();
            //        $('#selectType2').show();
            //    }
            //});

            //$('#<%= txtLink.ClientID %>')
            $('#attachlink').on('change', '.attach-link', function () {
                var id = '';
                var url = $(this).val();
                if (url != '') {
                    id = youtube_parser(url);
                    var $thu = $(this).next(); //.parent().find('#thumb');
                    if ($thu.length == 0) {
                         $thu  = $(this).parent().next();
                    }
                    if (id != '') {
                        var $div = $('<div id="ttt" class="col-md-12"></div>')
                        $thu.html($div);

                        $div.prettyEmbed({
                            videoID: id,
                            //previewSize: 'hd',	
                            useFitVids: true,

                            playerControls: true,
                            playerInfo: false
                        });
                    }
                }

                var _str = [];
                $('.attach-link').each(function (i, item) {
                    var _s = $(item).val();
                    if (_s != '') {
                        _str.push(_s);
                    }
                });

                $('#<%= txtLink.ClientID %>').val(_str.join('|'));
            });

            $('#ddlDisplay').on('change', function () {
                var v = $('#ddlDisplay').val();

                if (v == "1") {
                    $('#displayDate-wrapper').hide();
                    $("#txtDisplayDate").prop('required', false);
                    $("#txtDisplayTime").prop('required', false);
                }
                else {
                    $('#displayDate-wrapper').show();
                    $("#txtDisplayDate").prop('required', true);
                    $("#txtDisplayTime").prop('required', true);
                }
            });

            //$('#<%= txtLink.ClientID %>').trigger('change');
            $('#kvFileinputModal').addClass('file-zoom-fullscreen');
            $('#kvFileinputModal .btn-borderless').addClass('active');
            $('#<%= ddlDisplay.ClientID %>').trigger('change');

            initLink();
        });

        function removelink(t) {
            var $this = $(t);

            $this.parents('.linkhrow-wrapper').remove();

            //reorder num
            $('#link-wrapper .linkrow .rownum').each(function (i, r) {
                $(this).html(i + 1);
            });

            $('.attach-link:last').trigger('change');
        }

        function addnewlink() {
            var index = $('.attach-link').length + 1;
            var _html = `<div  class="linkhrow-wrapper col-md-12 p-0">
                            <div class="row">
                                <div class="col-md-2 linkrow p-0" >
                                    <button type="button" onclick="removelink(this)" class="btn btn-danger search-btn" style="height: 26px;width: 26px;padding: 3px;">
                                           <span class="material-icons">remove</span>
                                    </button>
                                    <span class='rownum'>${index}</span>
                                </div>
                                <div class="col-md-10 linkrow p-0">
                                    <input type="url" name="url${index}" placeholder="https://www.google.com" class="attach-link form-control"  style="" />
                                    <div id="thumb" class="col-md-12"></div>  
                                </div>
                            </div>
                        </div>`;

            $("#link-wrapper").append(_html);
            //$("#link-wrapper").append('<div class="col-md-1 linkrow">' + index +'</div><div class="col-md-11 linkrow"><input type="text" class="attach-link form-control" style="" /><div id="thumb" class="col-md-12"></div></div> ');
            return false;
        }

        function initLink() {
            var _arr = $('#<%= txtLink.ClientID %>').val().split('|');

            _arr.forEach(function (item, index) {
                if (index == 0) {

                }
                else {
                    addnewlink();
                }

                $('.attach-link:last').val(item);
                $('.attach-link:last').trigger('change');
            });
        }

        async function processFile() {
            var files = $el1.fileinput('getFileList');

            var base64 = "";
            for (var i = 0; i < files.length; i++) {
                //var s = getBase64(files[i]);
                await toBase64(files[i]).then(data => {
                    base64 += files[i].name + "|" + files[i].type + "|" + data.split("base64,")[1] + "?"
                    //console.log(data);
                }).catch((message) => {
                    alert(message);
                });
                //base64 += s + ",";
            }
            $('#<%= tbFiles.ClientID %>').val(base64);

            // console.log(base64);
            return base64;
        }
    </script>
</asp:Content>
