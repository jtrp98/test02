<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="Reportmobile01.aspx.cs" Inherits="FingerprintPayment.Reportmobile01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="app/Reports/Come2school/ReportLearnScanning01JS.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var availableValueEmployees = [];
            document.getElementById('txtname').addEventListener('input', function () {
                if (this.value === '') {
                    $("#txtid").val("");
                }
            }, false);

            $('.datepicker').datepicker({ dateFormat: "dd/mm/yy", navigationAsDateFormat: true, showOtherMonths: true });
            $('#ctl00_MainContent_ddlsublevel').change(function () {
                $('input[id*=txtSubLV2ID]').val("");
                getListSubLV2();
            });

            $('#ctl00_MainContent_dllTypeReport').change(function () {
                var type = $("#ctl00_MainContent_dllTypeReport option:selected").val();
                if (type == 1) {
                    $(".student").css("display", "");
                } else if (type == 2) {
                    $(".student").css("display", "none");
                    $.ajax({
                        url: "/App_Logic/modalJSON.aspx?mode=teacher",
                        dataType: "json",
                        success: function (objjson) {
                            availableValueEmployees = [];
                            $.each(objjson, function (index) {
                                var newObject = {
                                    label: objjson[index].sName + ' ' + objjson[index].sLastname,
                                    value: objjson[index].sEmp
                                };
                                availableValueEmployees[index] = newObject;
                            });
                        }
                    });
                }
            });

            $('#txtname').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                source: function (request, response) {
                    var type = $("#ctl00_MainContent_dllTypeReport option:selected").val();
                    var results;
                    if (type == 1) {
                        results = $.ui.autocomplete.filter(availableValuestudent, request.term);
                    } else {
                        results = $.ui.autocomplete.filter(availableValueEmployees, request.term);
                    }
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("#txtname").val(ui.item.label);
                    $("#txtid").val(ui.item.value);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                    $("#txtid").val("");
                }
            });
        });
    </script>
    <script type="text/javascript" language="javascript">
        var status01 = 0;
        var status02 = 0;
        var status03 = 0;
        var _numberpage = 0;
        var data = [];

        function reportHeader() {
            $("body").mLoading();
            $("#myPager").html("");
            $("#myTable").html("");
            var userid = $('input[id*=txtid]').val();
            var dstart = $('input[id*=txtstart]').val();
            var dend = $('input[id*=txtend]').val();
            var status = $('select[id*=status] option:selected').val();
            var tremid = $('#ctl00_MainContent_semister option:selected').val();
            var sublv = $('#ctl00_MainContent_ddlsublevel option:selected').val();
            var sublv2 = $('#ctl00_MainContent_ddlSubLV2 option:selected').val();

            var _url = "";
            if ($("#ctl00_MainContent_dllTypeReport option:selected").val() == 1) {
                _url = "/App_Logic/dataReportGeneric.ashx?mode=GetHeaderReportmobile03&userid=" + userid + "&tremid=" + tremid + "&sublv=" + sublv + "&sublv2="
                + sublv2 + "&status=" + status + "&dstart=" + dstart + "&dend=" + dend;
            }
            else {
                _url = "/App_Logic/dataReportGeneric.ashx?mode=GetHeaderReportmobile01&userid=" + userid + "&dstart=" + dstart + "&dend=" + dend + "&status=" + status;
            }

            status01 = 0;
            status02 = 0;
            status03 = 0;

            var requestheader1 = $.ajax({
                type: "POST",
                url: _url,
                cache: false,
                contentType: 'application/json;',
                success: function (obj) {
                    $.each(obj, function (index) {
                        var _status = obj[index].StatusIN;
                        if (_status == null) _status = "3";
                        switch (_status.trim()) {
                            case "0":
                                status01 = obj[index].Status_0;
                                break;
                            case "1":
                                status02 = obj[index].Status_1;
                                break;
                                //default:
                            case "3":
                                status03 += obj[index].Status_2;
                                break;
                        }
                    });
                    if (status01 == null) status01 = 0;
                    if (status02 == null) status02 = 0;
                    if (status03 == null) status03 = 0;
                    $("#status01").html(status01);
                    $("#status02").html(status02);
                    $("#status03").html(status03);
                    _numberpage = status01 + status02 + status03;
                }
            });
            requestheader1.error(function (xhr) {
                if (xhr.status === 500) {
                    window.location.href = "Default.aspx";
                    return;
                }
            });
            requestheader1.done(function (msg) {
                reportEmployees(0);
            });
        }

        function reportEmployees(_indexpager) {

            var userid = $('input[id*=txtid]').val();
            var dstart = $('input[id*=txtstart]').val();
            var dend = $('input[id*=txtend]').val();
            var status = $('select[id*=status] option:selected').val();
            var tremid = $('#ctl00_MainContent_semister option:selected').val();
            var sublv = $('#ctl00_MainContent_ddlsublevel option:selected').val();
            var sublv2 = $('#ctl00_MainContent_ddlSubLV2 option:selected').val();

            var _url = "";
            if ($("#ctl00_MainContent_dllTypeReport option:selected").val() == 1) {
                _url = "/App_Logic/dataReportGeneric.ashx?mode=GetReportmobile03&userid=" + userid + "&tremid=" + tremid + "&page=" + _indexpager +
                "&sublv=" + sublv + "&sublv2=" + sublv2 + "&status=" + status + "&dstart=" + dstart + "&dend=" + dend;
            }
            else {
                _url = "/App_Logic/dataReportGeneric.ashx?mode=GetReportmobile01&userid=" + userid + "&dstart=" + dstart + "&dend=" + dend + "&page=" + _indexpager + "&status=" + status;
                //_url = "/api/Reportmobile01?id=" + userid + "&dstart=" + dstart + "&dend=" + dend + "&page=" + _indexpager + "&status=" + status;
            }

            var request = $.ajax({
                type: "GET",
                url: _url,
                cache: false,
                contentType: 'application/json;',
                success: function (obj, status) { }
            });

            request.done(function (msg) {

                GenTable(msg);
                if (_indexpager == 0) {
                    $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 20 });
                    $(".index_0").css("display", "");
                }
                $(".grouppager").click(function () {
                    var grouppager = $(this).html();
                    $("[class*=linkpager_]").css("display", "none");
                    var indexpager = Math.floor(((grouppager) / 10));
                    $("[class*=linkpager_" + indexpager + "]").css("display", "");
                });
                $("body").mLoading('hide');
            });
        }
        function GenTable(obj) {
            var _dScan = "";
            var sublv = "";
            status01 = 0;
            status02 = 0;
            status03 = 0;
            $("#myTable").html("");
            $.each(obj, function (_i) {
                data[data.length] = obj[_i];
                var row$ = $('<tr/>');
                var pager = 0;
                pager = Math.floor((_i / 20));
                var rowheader$ = $('<tr/>');
                if (_dScan != obj[_i].dScan) {
                    sublv = "";
                    var _dd = new Date(parseInt(obj[_i].dScan.substr(6)));
                    rowheader$.append($('<td/>').attr('colspan', '6').html("<br/>" + _dd.toDateString("dd/MM/yyyy")));
                    _dScan = obj[_i].dScan;
                    $("#myTable").append(rowheader$.addClass("header index_" + pager + "")); //.css("display", "none"));.css("display", "none"));
                    rowheader$ = $('<tr/>');
                    rowheader$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %>"));
                    rowheader$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %>"));
                    rowheader$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>"));
                    rowheader$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %>"));
                    rowheader$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>"));
                    rowheader$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %>"));
                    $("#myTable").append(rowheader$.addClass("header index_" + pager).css("text-align", "center").css("background-color", "#337ab7").css("color", "#fff"));
                }
                else if ((_i + 1) % 20 == 1) {
                    var rowheader$ = $('<tr/>');
                    var _dd = new Date(parseInt(obj[_i].dScan.substr(6)));
                    rowheader$.append($('<td/>').attr('colspan', '6').html("<br/>" + _dd.toDateString("dd/MM/yyyy")));
                    $("#myTable").append(rowheader$.addClass("header index_" + pager + "")); //.css("display", "none"));
                    rowheader$ = $('<tr/>');
                    rowheader$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %>"));
                    rowheader$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %>"));
                    rowheader$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>"));
                    rowheader$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %>"));
                    rowheader$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>"));
                    rowheader$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %>"));
                    $("#myTable").append(rowheader$.addClass("header index_" + pager).css("text-align", "center").css("background-color", "#337ab7").css("color", "#fff"));

                }
                if ($("#ctl00_MainContent_dllTypeReport option:selected").val() == 1) {
                    if (sublv != obj[_i].SubLevel + "/" + obj[_i].nTSubLevel2 || (_i + 1) % 20 == 1) {
                        sublv = obj[_i].SubLevel + "/" + obj[_i].nTSubLevel2;
                        var rowheadersub$ = $('<tr/>');
                        rowheadersub$.append($('<td/>').attr('colspan', '6').html(sublv));
                        $("#myTable").append(rowheadersub$.addClass("header index_" + pager + "").css("text-align", "left").css("background-color", "#B4C6E7"));
                    }
                }
                row$.append($('<td/>').html(obj[_i].sName));

                var _status = obj[_i].SIN;
                if (_status == null) _status = "3";
                switch (_status.trim()) {
                    case "0":
                        row$.append($('<td/>').html(Dateformat(obj[_i].dIn)).css("text-align", "center"));
                        row$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>").css("text-align", "center"));
                        break;
                    case "1":
                        row$.append($('<td/>').html(Dateformat(obj[_i].dIn)).css("text-align", "center"));
                        row$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>").css("text-align", "center"));
                        break;
                    case "3": row$.append($('<td/>').html("-").css("text-align", "center"));
                        row$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>").css("text-align", "center"));
                        break;
                    case "":
                        row$.append($('<td/>').html("-").css("text-align", "center"));
                        row$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>").css("text-align", "center"));
                        break;
                    default:
                        row$.append($('<td/>').html("-").css("text-align", "center"));
                        row$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02242") %>").css("text-align", "center"));
                        break;
                }

                _status = obj[_i].SOUT;
                if (_status == null) _status = "3";
                switch (_status.trim()) {
                    case "0":
                        row$.append($('<td/>').html(Dateformat(obj[_i].dOut)).css("text-align", "center"));
                        row$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>").css("text-align", "center"));
                        break;
                    case "1":
                        row$.append($('<td/>').html(Dateformat(obj[_i].dOut)).css("text-align", "center"));
                        row$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>").css("text-align", "center"));
                        break;
                    case "2":
                        row$.append($('<td/>').html(Dateformat(obj[_i].dOut)).css("text-align", "center"));
                        row$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105017") %>").css("text-align", "center"));
                        break;
                    case "3":
                        if (Dateformat(obj[_i].dOut) != "") {
                            row$.append($('<td/>').html(Dateformat(obj[_i].dOut)).css("text-align", "center"));
                            row$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131127") %>").css("text-align", "center"));
                        } else {
                            row$.append($('<td/>').html("-").css("text-align", "center"));
                            row$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>").css("text-align", "center"));
                        }
                        break;
                    default:
                        if (_status = obj[_i].SIN.trim() == "9") {
                            row$.append($('<td/>').html("-").css("text-align", "center"));
                            row$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02242") %>").css("text-align", "center"));
                        }
                        else {
                            row$.append($('<td/>').html("-").css("text-align", "center"));
                            row$.append($('<td/>').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>").css("text-align", "center"));
                        }
                        break;
                }
                row$.append($('<td/>').html(""));
                $("#myTable").append(row$.addClass("rowdetail"));
            });
        }
    </script>
    <script type="text/javascript" language="javascript">
        $.fn.pageMe = function (opts) {
            var $this = this,
        defaults = {
            perPage: 7,
            showPrevNext: false,
            hidePageNumbers: false
        },
        settings = $.extend(defaults, opts);

            var listElement = $this;
            var perPage = settings.perPage;
            var children = listElement.children(".rowdetail");
            var pager = $('.pager');

            if (typeof settings.childSelector != "undefined") {
                children = listElement.find(settings.childSelector);
            }

            if (typeof settings.pagerSelector != "undefined") {
                pager = $(settings.pagerSelector);
            }

            var numItems = _numberpage; //children.size();
            var numPages = Math.ceil(numItems / perPage);

            pager.data("curr", 0);

            if (settings.showPrevNext) {
                $('<li><a href="#" class="prev_link">«</a></li>').appendTo(pager);
            }

            var curr = 0;
            while (numPages > curr && (settings.hidePageNumbers == false)) {
                if (curr >= 9) {
                    if ((curr + 1) % 10 == 0) {
                        $('<li><a href="#" class="page_link grouppager" >' + (curr + 1) + '</a></li>').appendTo(pager);
                    } else {
                        var indexpager = Math.floor((curr + 1) / 10);
                        $('<li><a href="#" class="page_link linkpager_' + indexpager + '" style="display:none;">' + (curr + 1) + '</a></li>').appendTo(pager);
                    }
                }
                else if (curr == 0) {
                    $('<li><a href="#" class="page_link grouppager">' + (curr + 1) + '</a></li>').appendTo(pager);
                }
                else {
                    $('<li><a href="#" class="page_link linkpager_0">' + (curr + 1) + '</a></li>').appendTo(pager);
                }
                curr++;
            }

            if (settings.showPrevNext) {
                $('<li><a href="#" class="next_link">»</a></li>').appendTo(pager);
            }

            pager.find('.page_link:first').addClass('active');
            pager.find('.prev_link').hide();
            if (numPages <= 1) {
                pager.find('.next_link').hide();
            }
            pager.children().eq(1).addClass("active");

            pager.find('li .page_link').click(function () {
                $("body").mLoading();
                var clickedPage = $(this).html().valueOf() - 1;
                reportEmployees(clickedPage + 1);
                goTo(clickedPage, perPage);
                return false;
            });
            pager.find('li .prev_link').click(function () {
                previous();
                return false;
            });
            pager.find('li .next_link').click(function () {
                next();
                return false;
            });

            function previous() {
                $("body").mLoading();
                var goToPage = parseInt(pager.data("curr")) - 1;
                reportEmployees(goToPage + 1);
                goTo(goToPage);
                if (Math.floor((goToPage + 2) % 10) == 0) {
                    $("[class*=linkpager_]").css("display", "none");
                    var indexpager = Math.floor(((goToPage + 1) / 10));
                    $("[class*=linkpager_" + indexpager + "]").css("display", "");
                }
            }

            function next() {
                $("body").mLoading();
                goToPage = parseInt(pager.data("curr")) + 1;
                reportEmployees(goToPage + 1);
                goTo(goToPage);
                if (Math.floor((goToPage + 1) % 10) == 0) {
                    $("[class*=linkpager_]").css("display", "none");
                    var indexpager = Math.floor(((goToPage + 1) / 10));
                    $("[class*=linkpager_" + indexpager + "]").css("display", "");
                }
            }

            function goTo(page) {
                var startAt = page * perPage,
            endOn = startAt + perPage;

                if (page >= 1) {
                    pager.find('.prev_link').show();
                }
                else {
                    pager.find('.prev_link').hide();
                }

                if (page < (numPages - 1)) {
                    pager.find('.next_link').show();
                }
                else {
                    pager.find('.next_link').hide();
                }

                pager.data("curr", page);
                pager.children().removeClass("active");
                pager.children().eq(page + 1).addClass("active");
            }
        };
    </script>
    <script language="javascript" type="text/javascript">

        var availableValuestudent = [];
        function getListSubLV2() {
            //                alert($('#ctl00_MainContent_ddlSubLV option:selected').val())
            var SubLVID = $('#ctl00_MainContent_ddlsublevel option:selected').val();
            var sSubLV = $('#ctl00_MainContent_ddlsublevel option:selected').text();
            $('select[id*=ddlSubLV2] option').remove();
            $('select[id*=ddlSubLV2]')
                            .append($("<option></option>")
                            .attr("value", "")
                            .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"));
            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
                success: function (msg) {

                    $.each(msg, function (index) {
                        $('select[id*=ddlSubLV2]')
                            .append($("<option></option>")
                            .attr("value", msg[index].nTermSubLevel2)
                            .text(sSubLV + " / " + msg[index].nTSubLevel2));
                    });
                }
            });
        }

        function getListTrem() {
            var YearID = $('#ctl00_MainContent_ddlyear option:selected').val();
            var YearNumber = $('#ctl00_MainContent_ddlyear option:selected').text();
            $("#ctl00_MainContent_semister option").remove();
            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listterm&id=" + YearID,
                success: function (msg) {

                    $.each(msg, function (index) {
                        $('select[id*=semister]')
                            .append($("<option></option>")
                            .attr("value", msg[index].nTerm)
                            .text(msg[index].sTerm));
                    });
                }
            });
        }

        $(document).ready(function () {
            $("#exportfile").click(function () {
                var userid = $('input[id*=txtid]').val();
                var dstart = $('input[id*=txtstart]').val();
                var dend = $('input[id*=txtend]').val();
                var status = $('select[id*=status] option:selected').val();
                var tremid = $('#ctl00_MainContent_semister option:selected').val();
                var sublv = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                var sublv2 = $('#ctl00_MainContent_ddlSubLV2 option:selected').val();

                //window.showModalDialog("frmExport.aspx?userid=" + userid + "&dstart=" + dstart + "&dend=" + dend + "&status=" + status, this, 'status:1; resizable:1; dialogWidth:600px; dialogHeight:700px; dialogTop=180px; dialogLeft:510px; scroll:auto;status=no');

                var win;
                if ($("#ctl00_MainContent_dllTypeReport option:selected").val() != 1) {

                    win = window.open("frmExport.aspx?userid=" + userid + "&dstart=" + dstart + "&dend=" + dend + "&status=" + status, '_blank');
                }
                else {
                    win = window.open("frmExport.aspx?userid=" + userid + "&tremid=" + tremid + "&sublv=" + sublv + "&sublv2=" + sublv2 + "&status=" + status + "&dstart=" + dstart + "&dend=" + dend, '_blank');
                }
                if (win) {
                    //Browser has allowed it to be opened
                    win.focus();
                } else {
                    //Broswer has blocked it
                    alert('Please allow popups for this site');
                }
                //window.showModalDialog("frmExport.aspx?userid=" + userid + "&dstart=" + dstart + "&dend=" + dend + "&status=" + status, this, 'status:1; resizable:1; dialogWidth:600px; dialogHeight:700px; dialogTop=180px; dialogLeft:510px; scroll:auto;status=no');
            });
            getListSubLV2();
            getliststudent();
            getListTrem();

            $('#ctl00_MainContent_ddlyear').change(function () {
                $('input[id*=txtSubLV2ID]').val("");
                getListTrem();
            });

            $('select[id*=ddlSubLV2]').change(function () {
                getliststudent();
            });

            function getliststudent() {
                availableValuestudent = [];
                $.ajax({
                    url: "/App_Logic/dataGenericListData.ashx?mode=liststudent&nelevel=" +
                        $('#ctl00_MainContent_ddlsublevel option:selected').val() + "&nsublevel=" + $('select[id*=ddlSubLV2] option:selected').val(),
                    dataType: "json",
                    success: function (objjson) {
                        $.each(objjson, function (index) {
                            var newObject = {
                                label: objjson[index].sName,
                                value: objjson[index].sID
                            };
                            availableValuestudent[index] = newObject;
                        });

                    }
                });
            }
        });
    </script>
    <style type="text/css">
        @media (max-width: 999px) {
            .report-container {
                font-size: 18px;
            }

            label {
                font-weight: normal;
                font-size: 18px;
            }

            legend {
                padding-left: 30px;
                font-size: 18px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 18px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }

            .table-show-result {
                font-size: 20px;
            }
        }

        @media (min-width: 1000px) and (max-width: 1199px) {
            .report-container {
                font-size: 22px;
            }

            label {
                font-weight: normal;
                font-size: 22px;
            }

            legend {
                padding-left: 30px;
                font-size: 22px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 22px;
                width: 100%;
                padding-left: 30px;
                padding-right: 30px;
            }

            .table-show-result {
                font-size: 22px;
            }
        }

        @media (min-width: 1200px) {
            .report-container {
                font-size: 26px;
            }

            label {
                font-weight: normal;
                font-size: 26px;
            }

            legend {
                padding-left: 30px;
                font-size: 26px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 26px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }

            .table-show-result {
                font-size: 26px;
            }
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="report-container">
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107032") %> :
                </label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="dllTypeReport" runat="server" class="form-control">
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %>" Value="1" Selected="True" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803012") %>" Value="2" Selected="False" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-sm-6">
            </div>
        </div>
        <div class="row student">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlyear" runat="server" class="form-control">
                        <asp:ListItem Text="2558" Value="2557" Selected="True" />
                        <asp:ListItem Text="2559" Value="2557" Selected="False" />
                        <asp:ListItem Text="2560" Value="2557" Selected="False" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="semister" runat="server" class="form-control">
                        <asp:ListItem Text="1" Value="1" Selected="True" />
                        <asp:ListItem Text="2" Value="2" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row student">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlsublevel" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlSubLV2" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <input type="text" class='form-control' id="txtid" style="display: none;" />
                    <input type="text" class='form-control' id="txtname" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <div class="input-group">
                        <input type="text" id="txtstart" readonly="true" class="form-control datepicker" /><div
                            class="input-group-addon">
                            <i class="glyphicon glyphicon-calendar"></i>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <div class="input-group">
                        <input type="text" id="txtend" class="form-control datepicker" readonly="true" /><div
                            class="input-group-addon">
                            <i class="glyphicon glyphicon-calendar"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="status" runat="server" class="form-control">
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" Selected="True" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>" Value="0" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>" Value="1" />
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>" Value="3" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <input type="button" class="btn btn-primary button-custom" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="reportHeader();" />
            </div>
        </div>
        <div class='row' style="font-weight: bolder; font-size: 40px;">
            <br />
            <fieldset>
                <legend><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M406001") %></legend>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-success'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %><br />
                        <span class='text-large' id="status01">0</span>
                    </p>
                </div>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-warning'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %><br />
                        <span class='text-large' id="status02">0</span>
                    </p>
                </div>
                <div class='form-group col-sm-4'>
                    <p class='text-center text-danger'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %><br />
                        <span class='text-large' id="status03">0</span>
                    </p>
                </div>
            </fieldset>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="btn btn-success button-custom" id="exportfile">Export File</div>
            </div>
        </div>
        <asp:Literal ID="ltrHeaderReport" runat="server" />
        <div class="row border-bottom">
            <br />
            <div class="col-sm-12">
                <fieldset>
                    <legend><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></legend>
                    <asp:ListView ID="lvReport" runat="server">
                    </asp:ListView>
                    <table id="example" class="table table-condensed table-bordered table-show-result"
                        cellspacing="0" width="100%">
                        <thead id="myHeader"></thead>
                        <tbody id="myTable"></tbody>
                        <tfoot>
                            <tr>
                                <th colspan="6" style="text-align: center;">
                                    <ul class="pagination pagination-lg pager" id="myPager" />
                                </th>
                            </tr>
                        </tfoot>
                    </table>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>
