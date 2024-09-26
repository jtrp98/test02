<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="ParentCardOld.aspx.cs" EnableEventValidation="false" Inherits="FingerprintPayment.StudentCall.ParentCardOld" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>--%>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <script src="../Scripts/bootstrap-toggle.js"></script>
    <link href="/Content/bootstrap-toggle.css" rel="stylesheet" />

    <!-- load the CSS files in the right order -->
    <link href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/css/fileinput.min.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/themes/explorer/theme.min.css" rel="stylesheet" />

    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />
    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <style>
        label {
            font-weight: normal;
            font-size: 26px;
        }

        .select2-selection__rendered {
            line-height: 41px !important;
        }

        .select2-container .select2-selection--single {
            height: 41px !important;
        }

        .select2-selection__arrow {
            height: 41px !important;
        }

            .select2-selection__arrow b {
                border-color: black transparent transparent transparent !important;
            }

        [class^='select2'] {
            border-radius: 1px !important;
            border-top-color: #abadb3 !important;
            border-left-color: #dbdfe6 !important;
            border-right-color: #dbdfe6 !important;
            border-bottom-color: #dbdfe6 !important;
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .centertext {
            text-align: center;
        }

        .centerText {
            text-align: center;
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }
    </style>
    <style>
        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }

        .cover {
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .f70 {
            font-size: 70%;
        }

        .listItem {
            color: blue;
            background-color: White;
        }

        .hid {
            visibility: hidden;
        }

        .hid2 {
            visibility: hidden;
            display: none;
        }

        #loading {
            display: block;
            position: fixed;
            top: 0;
            left: 0;
            z-index: 100;
            width: 100vw;
            height: 100vh;
            background-color: rgba(192, 192, 192, 0.5);
            background-image: url("https://i.imgur.com/CgViPo0.gif");
            background-repeat: no-repeat;
            background-position: center;
        }

        .width10 {
            margin: 0 auto;
            width: 10%;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }

        .gvbutton {
            font-size: 25px;
        }

        .nounder a:hover {
            text-decoration: none;
        }

        .shadowblack {
            text-decoration: none;
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .boxhead a {
            color: #FFFFFF;
            text-decoration: none;
        }

        a.imjusttext {
            color: #ffffff;
            text-decoration: none;
        }

            a.imjusttext:hover {
                color: aquamarine;
            }

        .btn-red {
            background: red; /* use your color here */
        }


        .nowrap {
            max-width: 100%;
            white-space: nowrap;
        }

        .namemangin {
            margin-left: 5px;
            padding-left: 35px;
            border-left: 10px
        }

        .tab {
            border-collapse: collapse;
            margin-left: 6px;
            margin-right: 6px;
            border-bottom: 3px solid #337AB7;
            border-left: 3px solid #337AB7;
            border-right: 3px solid #337AB7;
            border-top: 3px solid #337AB7;
            box-shadow: inset 0 1px 0 #337AB7;
        }

        /*.kv-file-content {
            display: none;
        }*/

        /* .file-details-cell samp {
            font-size: 14px;
        }

        .file-actions-cell {
            width: 60px;
        }*/
        .file-thumbnail-footer {
            display: none;
        }

        .fileinput-remove {
            display: none;
        }

        .krajee-default.file-preview-frame .kv-file-content {
            width: auto;
            height: auto;
        }

        .btn-file {
            padding: 2px 20px;
        }

        .kv-file-content img {
            max-height: 250px !important;
        }

        .file-input .glyphicon {
            font-size: 14px;
        }

        .file-caption-main {
            /* display:none!important;*/
        }

        .progress-bar {
            font-size: 14px;
        }

        label.error {
            color: red;
            font-size: 24px;
            /*position: absolute;
    bottom: -24px;*/
        }
        /* Testing Specific file merge */
    </style>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js" type="text/javascript"></script>
    <!-- load the JS files in the right order -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/js/plugins/piexif.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/js/plugins/purify.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/js/fileinput.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/themes/explorer/theme.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        function pageLoad() {
            $('.card-status-toggle').bootstrapToggle();

            $('#ctl00_MainContent_dgd').DataTable({
                paging: false,
                searching: false,
                info: false,
                //"bSort": true,
                //"autoWidth": false,
                //"aoColumnDefs": [
                //    {
                //        'bSortable': false, 'aTargets': [3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                //    }
                //]
            });
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(onEachRequest);
        prm.add_endRequest(endRequest);

        function onEachRequest(sender, args) {
            $("body").mLoading('show');
        }
        function endRequest(sender, args) {
            //Do all what you want to do in jQuery ready function
            $("body").mLoading('hide');
        }

        var availableValueplane = [];

        $(document).ready(function () {



            $.fn.inputFilter = function (inputFilter) {
                return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
                    if (inputFilter(this.value)) {
                        this.oldValue = this.value;
                        this.oldSelectionStart = this.selectionStart;
                        this.oldSelectionEnd = this.selectionEnd;
                    } else if (this.hasOwnProperty("oldValue")) {
                        this.value = this.oldValue;
                        this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                    } else {
                        this.value = "";
                    }
                });
            };

            //$('#ddlType').on('change', function (e) {
            //    if ($(this).val() == "2") {
            //        $('.row.student').hide();
            //    }
            //    else {
            //        $('.row.student').show();
            //    }
            //});

            $('input[id*=btnSearch]').click(function () {

                if ($("#aspnetForm").valid()) {
                    $("body").mLoading();
                    var load = document.getElementsByClassName("load");
                    var inputname = document.getElementsByClassName("inputname");
                    load[0].classList.remove('hidden');

                    //var type = $('#ddlType option:selected').val();
                    var param1var = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                    var param2var = $('select[id*=ddlsublevel2] option:selected').val();
                    if (param2var == undefined)
                        param2var = "";
                    window.location.href = "ParentCard.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&name=" + inputname[0].value + "&c=1";
                    $("body").mLoading('hide');
                }

            });

            $("#myModal1").on('shown.bs.modal', function () {
                //$(this).find('#txtStudentCardNumber1').focus();
                //$(this).find('#txtStudentCardNumber2').focus();
                //$(this).find('#txtStudentCardNumber3').focus();
                if ($("input[id$=txtStudentCardNumber1]").val() == "") {
                    $("input[id$=txtStudentCardNumber1]").focus();
                }
                //else if ($("input[id$=txtStudentCardNumber2]").val() == "") {
                //    $("input[id$=txtStudentCardNumber2]").focus();
                //}
                //else if ($("input[id$=txtStudentCardNumber3]").val() == "") {
                //    $("input[id$=txtStudentCardNumber3]").focus();
                //}

                //$('#ctl00_MainContent_btnAddStudentCard').prop('disabled', true);
            });

            $('.wrapper-table').on('change', '.card-status-toggle', function () {
                var $this = $(this);

                PageMethods.SwitchCardStatus($this.data('sid'), $this.data('index'), $this.prop('checked'),
                    function (response) {
                        $.confirm({
                            title: false,
                            content: '<img src="../images/checked.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h1 class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106017") %></h1>',
                            theme: 'material',
                            type: 'blue',
                            buttons: {
                                "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                    btnClass: 'btn-primary',
                                    keys: ['enter', 'shift'],
                                    action: function () {

                                    }
                                }
                            }
                        });
                    },
                    function (response) {
                        $.confirm({
                            title: false,
                            content: '<img src="../images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h1 class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132153") %></h1>',
                            theme: 'material',
                            type: 'red',
                            buttons: {
                                "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                    btnClass: 'btn-primary',
                                    keys: ['enter', 'shift'],
                                    action: function () {
                                    }
                                }
                            }
                        });

                    });
            });

            //$('#ddlType').trigger('change');

            //$("input[id*=txtStudentCardNumber]").on('keyup', function () {

            //});

            var wto;
            $("input[id*=txtStudentCardNumber]").on('keyup', function () {

                var $this = $(this);
                var _val = $this.val();
                var _old = $this.data('old') + '';

                clearTimeout(wto);
                wto = setTimeout(function () {


                    if (_val == "" || _val == _old) {
                        $('#ctl00_MainContent_btnAddStudentCard').prop('disabled', false);
                        $("#lblErrorMessage").text('');
                        $("#lblErrorMessage").hide();
                    }
                    else if (_val != _old) {

                        PageMethods.IsCardValid(_val,
                            function (response) {
                                if (response) {
                                    $('#ctl00_MainContent_btnAddStudentCard').prop('disabled', false);
                                    $("#lblErrorMessage").text('');
                                    $("#lblErrorMessage").hide();
                                }
                                else {
                                    $('#ctl00_MainContent_btnAddStudentCard').prop('disabled', true);
                                    $("#lblErrorMessage").text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133296") %>');
                                    $("#lblErrorMessage").show();
                                    $this.focus();
                                }
                            },
                            function (response) {

                            });
                    }
                    else {

                        //$("#lblErrorMessage").text('');
                        //$("#lblErrorMessage").hide();

                    }

                }, 800);


            });

            $("#txtStudentCardNumber1").inputFilter(function (value) {
                return /^\d*$/.test(value); // Allow digits only, using a RegExp
            });

            var $el1 = $("#filebg");
            $el1.fileinput({
                allowedFileExtensions: ['jpg', 'png', 'gif'],
                uploadUrl: "<%= ResolveUrl("~/StudentCall/UploadFileBG.ashx") %>",
                uploadAsync: true,
                deleteUrl: "<%= ResolveUrl("~/StudentCall/RemoveFileBG.ashx") %>",
                showUpload: false, // hide upload button                                 
                maxFileCount: 1,
                browseOnZoneClick: true,
                initialPreviewAsData: true,
                autoReplace: true,
                overwriteInitial: true,
                showRemove: false,
                showUpload: false, // <------ just set this from true to false
                initialPreviewFileType: 'image',
                overwriteInitial: true,
                //maxImageWidth: 204,
                //maxImageHeight: 325,
                //resizePreference: 'height',
                //resizeImage: true,
                <% if (!string.IsNullOrEmpty(BgFile))
        {%>

                initialPreview: ["<%=BgFile %>"],
                initialPreviewConfig: [{
                    key: "<%= UserData.CompanyID %>",
                }]
                <%} %>
            }).on("filebatchselected", function (event, files) {
                $el1.fileinput("upload");
            }).on('filebeforeload', function (event, file, index, reader) {
                $el1.fileinput('clear')
                //$('.file-preview .file-preview-thumbnails').html();
            });;

            if (jQuery.validator) {//.messages

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",
                });

                $.validator.addMethod("requiredSelect", function (element) {
                    let i = $("select.chosen-select :selected").val();

                    return (i != '');
                }, "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>");

                //$.validator.addClassRules('chosen-select', {
                //    requiredSelect: true ,
                //});


            }

        });

        $("#ctl00_MainContent_tags").ready(function () {
            if (getParameterByName('name') != "") {
                $("#ctl00_MainContent_tags").val(getParameterByName('name'));
            }
        });

        function ddlclass() {
            $("body").mLoading();
            var ddl2 = document.getElementsByClassName("ddl2");

            for (i = -1; i <= 90; i++) {
                ddl2[1].remove(0);
            }

            setTimeout(function () {
                $.get("/App_Logic/ddlclassroom.ashx?idlv=" + document.getElementById('<%=ddlsublevel.ClientID%>').value, function (Result) {
                    var opt = document.createElement("option");
                    // Assign text and value to Option object
                    opt.text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206188") %>";
                    opt.value = "";
                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);
                    $.each(Result, function (index) {

                        // Create an Option object       
                        var opt = document.createElement("option");

                        // Assign text and value to Option object
                        opt.text = Result[index].name;
                        opt.value = Result[index].value;
                        if (getUrlParameter("idlv2") == Result[index].value) {
                            opt.selected = "selected";
                        }
                        // Add an Option object to Drop Down List Box
                        document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);
                    });

                });

            }, 300);
            $("body").mLoading('hide');
        }

        function OpenStudentCardDialog(studentId, studentSurName, stdId, nFC, card1, family) {
            try {
                $("body").mLoading();

                $('#lblStudentCode').text(studentId);
                $('#lblStudentSurName').text(studentSurName);
                $('#hdnStdId').val(stdId);
                $("input[id$=txtStudentCardNumber1]").val(card1);
                $("input[id$=txtStudentCardNumber1]").attr('data-old', card1);
                $("input[id$=txtStudentCardNumber1]").prop('disabled', !!card1);

                PageMethods.GetFamilyList(stdId,
                    function (response) {
                        //response.father + " (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %>)",
                        //    response.mother + " (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %>)",
                        //    response.family + " (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %>)",

                        if (response) {
                            var _tags = [

                            ];

                            if (!!(response.father + '').trim()) {
                                _tags.push(response.father + " (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %>)");
                            }
                            if (!!(response.mother + '').trim()) {
                                _tags.push(response.mother + " (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %>)");
                            }
                            if (!!(response.family + '').trim()) {
                                _tags.push(response.family + " (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %>)");
                            }

                            $("#txtFamily").autocomplete({
                                source: _tags,
                                minLength: 0,
                            })
                                .focus(function () {
                                    $(this).autocomplete("search", $(this).val());
                                });
                        }

                        $("#txtFamily").val(family);
                        //$("input[id$=txtStudentCardNumber2]").val(card2);
                        //$("input[id$=txtStudentCardNumber2]").attr('data-old', card2);
                        //$("input[id$=txtStudentCardNumber2]").prop('disabled', !!card2);
                        //$("input[id$=txtStudentCardNumber3]").val(card3);
                        //$("input[id$=txtStudentCardNumber3]").attr('data-old', card3);
                        //$("input[id$=txtStudentCardNumber3]").prop('disabled', !!card3);

                        $("#lblErrorMessage").text('');
                        $("#lblErrorMessage").css('display', 'none');

                        $("body").mLoading('hide');
                    },
                    function (response) {

                    });

                return false;
            }
            catch (err) {
                console.log(err);
                return false;
            }
            return false;

        }

        function printAll() {
            var param = new URLSearchParams(window.location.search);
            window.open("PrintCard.aspx?type=all&lvl2=" + param.get('idlv2'), "_blank"); //$("#ddlsublevel2").val()
            return false;
        }

        function ValidateStudentCard() {
            $("body").mLoading();
            $("#lblErrorMessage").text('');
            if ($("input[id$=txtStudentCardNumber1]").val() != "" && (!$.isNumeric($("input[id$=txtStudentCardNumber1]").val()))) {
                $("input[id$=txtStudentCardNumber1]").focus();
                $("#lblErrorMessage").text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133297") %>');
                $("#lblErrorMessage").css('display', '');
                $("body").mLoading('hide');
                return false;
            }
            //else if ($("input[id$=txtStudentCardNumber2]").val() != "" && (!$.isNumeric($("input[id$=txtStudentCardNumber2]").val()))) {
            //    $("input[id$=txtStudentCardNumber2]").focus();
            //    $("#lblErrorMessage").text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133297") %>');
            //    $("#lblErrorMessage").css('display', '');
            //    $("body").mLoading('hide');
            //    return false;
            //}
            //else if ($("input[id$=txtStudentCardNumber3]").val() != "" && (!$.isNumeric($("input[id$=txtStudentCardNumber3]").val()))) {
            //    $("input[id$=txtStudentCardNumber3]").focus();
            //    $("#lblErrorMessage").text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133297") %>');
            //    $("#lblErrorMessage").css('display', '');
            //    $("body").mLoading('hide');
            //    return false;
            //}
            else {
                $("#lblErrorMessage").text('');
                $("#lblErrorMessage").css('display', 'none');
                $('#myModal1').modal('toggle');
                $("body").mLoading();
                PageMethods.UpdateStudentCard($('#hdnStdId').val()
                    , $("input[id$=txtStudentCardNumber1]").val()
                    , $("input[id=txtFamily]").val()
                    //, $("input[id$=txtStudentCardNumber2]").val()
                    //, $("input[id$=txtStudentCardNumber3]").val()
                    //, $("input[id$=txtStudentCardNumber]").val(),
                    , function (response) {
                        if (response) {

                            $.confirm({
                                title: false,
                                content: '<img src="../images/checked.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h1 class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106017") %></h1>',
                                theme: 'material',
                                type: 'blue',
                                buttons: {
                                    "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                        btnClass: 'btn-primary',
                                        keys: ['enter', 'shift'],
                                        action: function () {

                                            //var _sid = response.sid;
                                            __doPostBack('ctl00$MainContent$btnUpdate', '');
                                            //if (response.status1 == "1")
                                            //    if ($('.card-status-toggle[data-sid=' + _sid + '][data-index=1]').length == 0) {
                                            //        $('.slot-card1[data-sid=' + _sid + ']').html("<input class='card-status-toggle' type='checkbox'  data-sid='" + _sid + "'  data-index='1'  data-toggle='toggle' data-on='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132589") %>' data-off='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132588") %>' data-onstyle='success' data-offstyle='danger' checked='true'>");
                                            //    }

                                            //if (response.status2 == "1")
                                            //    if ($('.card-status-toggle[data-sid=' + _sid + '][data-index=2]').length == 0) {
                                            //        $('.slot-card2[data-sid=' + _sid + ']').html("<input class='card-status-toggle' type='checkbox'  data-sid='" + _sid + "'  data-index='2' data-toggle='toggle' data-on='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132589") %>' data-off='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132588") %>' data-onstyle='success' data-offstyle='danger' checked='true'>");
                                            //    }

                                            //if (response.status3 == "1")
                                            //    if ($('.card-status-toggle[data-sid=' + _sid + '][data-index=3]').length == 0) {
                                            //        $('.slot-card3[data-sid=' + _sid + ']').html("<input class='card-status-toggle' type='checkbox'  data-sid='" + _sid + "'  data-index='3' data-toggle='toggle' data-on='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132589") %>' data-off='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132588") %>' data-onstyle='success' data-offstyle='danger' checked='true'>");
                                            //    }

                                            //$('.card-status-toggle[data-sid=' + _sid + ']').bootstrapToggle();

                                        }
                                    }
                                }
                            });
                        }
                        else {
                            $.confirm({
                                title: false,
                                content: '<img src="../images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h1 class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132153") %></h1>',
                                theme: 'material',
                                type: 'red',
                                buttons: {
                                    "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                        btnClass: 'btn-primary',
                                        keys: ['enter', 'shift'],
                                        action: function () {
                                        }
                                    }
                                }
                            });

                        }

                        $("body").mLoading('hide');
                    },
                    function (response) {
                        console.log(response);
                        $("body").mLoading('hide');

                    });
            }
            return false;

        }

        function start() {
            $("body").mLoading();
            var availableTags = [];
            $.get("/app_logic/studentlist.ashx", function (Result) {
                $.each(Result, function (index) {
                    availableTags.push(Result[index].fullName);
                });
            });

            $("#ctl00_MainContent_tags").autocomplete({
                source: availableTags
            });
            ddlclass();
            $("body").mLoading('hide');
        }
        window.onload = start;

        function getParameterByName(name) {
            if (name !== "" && name !== null && name != undefined) {
                name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                    results = regex.exec(location.search);
                return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
            } else {
                var arr = location.href.split("/");
                return arr[arr.length - 1];
            }
        }
    </script>

    <div class="full-card box-content userlist-container">
        <div id="loading" class="hidden load"></div>
        <asp:HiddenField ID="hdfsid" runat="server" />

        <!-- Search Content -->


        <div class="form-group row studentx">
            <div class="col-xs-12 hidden">
                <div class="col-xs-2 righttext">
                    <label>ddl2</label>
                </div>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtddl2" runat="server" CssClass="d2" Width="50%"> </asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group row student">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlsublevel" onchange="ddlclass()" runat="server" CssClass="ddl2 form-control" Width="100%" required>
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206188") %>" Value="" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlsublevel2" ClientIDMode="Static" AutoPostBack="false" runat="server" CssClass="ddl2 form-control" Width="100%" required>
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206188") %>" Value="" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group row ">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                    <asp:TextBox ID="tags" runat="server" class="form-control inputname" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>" Width="100%" />
                </div>
            </div>
        </div>




        <div class="row">
            <div class="col-xs-7 button-section">
                <input type="button" id="btnSearch" class="btn btn-info search-btn pull-right" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
            </div>
        </div>

        <!-- Search Result - Student Details -->
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:LinkButton runat="Server" ID="btnUpdate" Text="Save" OnClick="btnUpdate_Click" CssClass="hidden" />
                            <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                                GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                OnDataBound="CustomersGridView_DataBound"
                                OnRowDataBound="dgd_RowDataBound"
                                Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-tablex table table-bordered table-hover dataTable">
                                <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                                <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                                    BackColor="#337AB7" />
                                <PagerTemplate>
                                    <table width="100%" class="tab">
                                        <tr>
                                            <td style="width: 25%">
                                                <asp:Label ID="Label1" BorderColor="#337AB7"
                                                    ForeColor="white"
                                                    Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>:"
                                                    runat="server" />
                                                <asp:DropDownList ID="PageDropDownList2"
                                                    AutoPostBack="true"
                                                    OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged2"
                                                    runat="server" />
                                            </td>
                                            <td style="width: 45%">
                                                <asp:LinkButton ID="backbutton" runat="server"
                                                    CssClass="imjusttext" OnClick="backbutton_Click">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>
                                                </asp:LinkButton>
                                                <asp:DropDownList ID="PageDropDownList"
                                                    AutoPostBack="true"
                                                    OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged"
                                                    runat="server" />
                                                <asp:LinkButton ID="nextbutton" runat="server"
                                                    CssClass="imjusttext" OnClick="nextbutton_Click">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
                                                </asp:LinkButton>
                                            </td>
                                            <td style="width: 70%; text-align: right">
                                                <asp:Label ID="CurrentPageLabel"
                                                    ForeColor="white"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </PagerTemplate>
                                <Columns>
                                    <asp:BoundField DataField="number" HeaderStyle-CssClass="centertext" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                        <HeaderStyle Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="sIdentification" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>" HeaderStyle-CssClass="centertext">
                                        <HeaderStyle Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="sName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %>" HeaderStyle-CssClass="centertext">
                                        <HeaderStyle Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Receiver" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133291") %>" HeaderStyle-CssClass="centertext">
                                        <HeaderStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133298") %>" HeaderStyle-CssClass="centertext">
                                        <ItemTemplate>
                                            <div class="slot-card1" data-sid="<%#Eval("sId") %>">
                                                <%# GetToggleButton(Eval("Card")+"",Eval("StatusCard")+"",Eval("sId")+"","1") %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106007") %>">
                                        <ItemTemplate>
                                            <div class="slot-card2" data-sid="<%#Eval("sId") %>">
                                                <%# GetToggleButton(Eval("Card")+"",Eval("StatusCard")+"",Eval("sId")+"","2") %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="12%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106008") %>">
                                        <ItemTemplate>
                                            <div class="slot-card3" data-sid="<%#Eval("sId") %>">
                                                <%# GetToggleButton(Eval("Card")+"",Eval("StatusCard")+"",Eval("sId")+"","3") %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="12%" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="centertext">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                            <button type="button" class="btn btn-success" data-toggle="modal" data-target="#myModal2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106127") %></button>
                                            <a href="#" class="btn btn-success" onclick="printAll();"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106138") %></a>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <button type="button" class="btn btn-success" style="" data-toggle="modal" onclick="OpenStudentCardDialog('<%# Eval("sIdentification") %>'
                                                ,'<%# Eval("sName") %>'
                                                ,'<%# Eval("sId") %>'
                                                ,''
                                                ,'<%# (Eval("Card") + "").Split(',').ElementAtOrDefault(0) %>'
                                                ,'<%# Eval("Receiver") %>'
                                            <%--  ,'<%# (Eval("Card") + "").Split(',').ElementAtOrDefault(1) %>' 
                                              ,'<%# (Eval("Card") + "").Split(',').ElementAtOrDefault(2) %>'--%>
                                          )"
                                                data-target="#myModal1">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>
                                            </button>
                                            <a class="btn btn-success " target="_blank" href="PrintCard.aspx?sid=<%# Eval("sId") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133288") %></a>
                                            <%-- <div class="btn btn-warning" style="" data-toggle="modal" onclick="UpdateStudentCard('<%# Eval("sstudentid") %>','<%# Eval("sName") %>','<%# Eval("stdId") %>','<%# Eval("NFC") %>')" data-target="#myModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></div>--%>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%" />
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" CssClass="headerCell" />
                                <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" CssClass="itemCell" />
                                <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <!-- Add Student Card - Modal -->
        <div class="modal fade" id="myModal1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133292") %></h2>
                    </div>
                    <div class="modal-body">
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-12 text-center alert-warning">
                                <asp:Label ID="lblErrorMessage" ClientIDMode="Static" CssClass="has-error" Text="" runat="server" Style="display: none" />
                            </div>
                        </div>
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:HiddenField runat="server" ID="hdnStdId" Value="10" ClientIDMode="Static" />
                                <asp:Label ID="lblStudentCode" ClientIDMode="Static" BorderColor="#337AB7" Text="" runat="server" />
                            </div>
                        </div>
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                            </div>
                            <div class="col-xs-7">
                                <asp:Label ID="lblStudentSurName" ClientIDMode="Static" BorderColor="#337AB7" Text="" runat="server" />
                            </div>
                        </div>
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label>หมายเลขบัตใบที่ 1</label>
                            </div>
                            <div class="col-xs-7" style="display: table;">
                                <asp:TextBox ID="txtStudentCardNumber1" runat="server" placeholder="Student Card Number" ClientIDMode="static" CssClass="form-control " Width="100%" MaxLength="20" autocomplete="off" />
                                <a style="cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;" onclick="$('#txtStudentCardNumber1').val('').prop('disabled', false);"><i class="fa fa-remove text-danger"></i></a>
                            </div>
                        </div>
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133291") %></label>
                            </div>
                            <div class="col-xs-7" style="display: table;">
                                <asp:TextBox ID="txtFamily" runat="server" Style="width: 100%" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                <a style="display: table-cell; vertical-align: middle; padding-left: 5px;"><i class="fa fa-remove text-danger" style="visibility: hidden"></i></a>
                            </div>
                        </div>
                        <%-- <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133294") %></label>
                            </div>
                            <div class="col-xs-7" style="display: table;">
                                <asp:TextBox ID="txtStudentCardNumber2" runat="server" placeholder="Student Card Number" ClientIDMode="static" CssClass="form-control " Width="100%" MaxLength="20" />
                                <a style="cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;" onclick="$('#txtStudentCardNumber2').val('').prop('disabled', false);"><i class="fa fa-remove text-danger"></i></a>
                            </div>
                        </div>
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133295") %></label>
                            </div>
                            <div class="col-xs-7" style="display: table;">
                                <asp:TextBox ID="txtStudentCardNumber3" runat="server" placeholder="Student Card Number" ClientIDMode="static" CssClass="form-control " Width="100%" MaxLength="20" />
                                <a style="cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;" onclick="$('#txtStudentCardNumber3').val('').prop('disabled', false);"><i class="fa fa-remove text-danger"></i></a>
                            </div>
                        </div>--%>
                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnAddStudentCard" class="btn btn-success" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>" OnClientClick="return ValidateStudentCard()" UseSubmitBehavior="false" Style="width: 74px;" />
                        <button type="button" class="btn btn-danger" id="btnUpdateStudentCard" runat="server" data-dismiss="modal" style="width: 74px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal2" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303007") %></h2>
                    </div>
                    <div class="modal-body" style="height: 500px;">
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-12 text-center alert-warning">
                                <asp:Label ID="Label2" ClientIDMode="Static" CssClass="has-error" Text="" runat="server" Style="display: none" />
                            </div>
                        </div>
                        <div class="col-xs-12" style="padding: 5px;">

                            <div class="col-xs-12">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106128") %> </label>
                                <input id="filebg" name="filebg" type="file" accept="image/*">
                            </div>
                        </div>

                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <%-- <div class="modal-footer"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133299") %>
                        <asp:Button ID="Button1" class="btn btn-success" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>" OnClientClick="return ValidateStudentCard()" UseSubmitBehavior="false" Style="width: 74px;" />
                        <button type="button" class="btn btn-danger" id="Button2" runat="server" data-dismiss="modal" style="width: 74px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>

    <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>
</asp:Content>

