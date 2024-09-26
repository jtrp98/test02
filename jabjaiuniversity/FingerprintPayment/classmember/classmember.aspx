<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="classmember.aspx.cs" Inherits="FingerprintPayment.classmember" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <%--    <link href="/Styles/jquery-ui.css" rel="stylesheet" />--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <style type="text/css">
        /* .select2-selection__rendered {
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

        .select2-container--open {
            font: 26px THSarabun;
        }

        .select2-container--default .select2-results > .select2-results__options {
            max-height: 350px;
        }

        [class^='select2'] {
            border-radius: 1px !important;
            border-top-color: #abadb3 !important;
            border-left-color: #dbdfe6 !important;
            border-right-color: #dbdfe6 !important;
            border-bottom-color: #dbdfe6 !important;
        }*/

        .hid {
            visibility: hidden;
        }

        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }

        label {
            font-weight: normal;
            font-size: 26px;
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

        .swal2-modal {
            font-size: 20px;
            width: 350px;
        }

        select, select.form-control {
            -moz-appearance: auto !important;
            -webkit-appearance: auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <%-- <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="/Content/Material/assets/js/plugins/sweetalert2.js"></script>
    <%--    <script src="../Scripts/jscommon.js"></script>--%>
    <script type="text/javascript" language="javascript">

        var availableValueplane = [];
        $.fn.modal.Constructor.prototype.enforceFocus = function () { };
        //$(document).ready(function () {
        //    $('.js-example-basic-multiple2').select2({
        //        allowClear: true,
        //        placeholder: ''
        //    });
        //});

        //function comboclick() {
        //    var data = $('.js-example-basic-multiple2').select2('data');
        //    data.prop('selectedIndex', 1).change();
        //    $('#fieldId').select2("val", $('#fieldId option:eq(1)').val());
        //}

        //$(document).ready(function () {
        //    $('.js-example-basic-multiple3').select2({
        //        allowClear: true,
        //        placeholder: ''
        //    });
        //});

        //function comboclick() {
        //    var data = $('.js-example-basic-multiple3').select2('data');
        //    data.prop('selectedIndex', 1).change();
        //    $('#fieldId').select2("val", $('#fieldId option:eq(1)').val());
        //}

        //$(document).ready(function () {
        //    $('.js-example-basic-multiple4').select2({
        //        allowClear: true,
        //        placeholder: ''
        //    });
        //});

        //function comboclick() {
        //    var data = $('.js-example-basic-multiple4').select2('data');
        //    data.prop('selectedIndex', 1).change();
        //    $('#fieldId').select2("val", $('#fieldId option:eq(1)').val());
        //}

        function getUrlParameter(sParam) {
            var sPageURL = decodeURIComponent(window.location.search.substring(1)),
                sURLVariables = sPageURL.split('&'),
                sParameterName,
                i;

            for (i = 0; i < sURLVariables.length; i++) {
                sParameterName = sURLVariables[i].split('=');

                if (sParameterName[0] === sParam) {
                    return sParameterName[1] === undefined ? true : sParameterName[1];
                }
            }
        }

        $(document).ready(function () {
            $('input[id*=btnDel]').click(function () {
                showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('name')); return false;
            });
            $('#ctl00_MainContent_ddlsublevel').val(getUrlParameter("idlv"));

            funtionListSubLV3("ctl00_MainContent_ddlsublevel", "ddlSubLV2", getUrlParameter("idlv2"));
            //availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");

            $('input[id*=txtSearch]').val(getUrlParameter("sname"));

            $('#ctl00_MainContent_ddlsublevel').change(function () {
                funtionListSubLV3("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
                //availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
            });

            $('select[id*=ddlSubLV2]').change(function () {
                //availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
            });


            //$('input[id*=btnSearch]').click(function () {
            //    LoadData();
            //});

            $('input[id*=btnSearch2]').click(function () {
                var param1var = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                var param2var = $('select[id*=ddlSubLV2] option:selected').val();
                var param3var = $('select[id*=DropDownList1] option:selected').val();
                var param4var = $('select[id*=DropDownList2] option:selected').val();

                window.location.href = "classmember-teacherlist.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&year=" + param3var + "&term=" + param4var;
            });

            $("#ctl00_MainContent_DropDownList1").change(function () {
                getTremList($("#ctl00_MainContent_DropDownList1").val(), null);
            });


            let queryString = window.location.search;
            let urlParams = new URLSearchParams(queryString);
            let term = urlParams.get('term')

            getTremList($("#ctl00_MainContent_DropDownList1").val(), term);

            //$('input[id*=txtSearch]').autocomplete({
            //    width: 300,
            //    max: 10,
            //    delay: 100,
            //    minLength: 1,
            //    maxLength: 10,
            //    autoFocus: true,
            //    cacheLength: 1,
            //    scroll: true,
            //    highlight: false,
            //    source: function (request, response) {
            //        var results = $.ui.autocomplete.filter(availableValueplane, request.term);
            //        response(results.slice(0, 10));
            //    },
            //    select: function (event, ui) {
            //        event.preventDefault();
            //        $("input[id*=txtSearch]").val(ui.item.label);
            //    },
            //    focus: function (event, ui) {
            //        event.preventDefault();
            //    }
            //});

            ///* copy plan *////
            var cloneYear1 = $('#<%= DropDownList1.ClientID %>').clone();
            cloneYear1.attr('id', "selectCopyFromYear");
            cloneYear1.attr('name', "");
            cloneYear1.prepend($('<option>', { value: '', text: '-<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01102") %>-' }));
            cloneYear1.prop('selectedIndex', 0);
            $('#divCopyFromYear').html(cloneYear1);

            var cloneYear2 = $('#<%= DropDownList1.ClientID %>').clone();
            cloneYear2.attr('id', "selectCopyToYear");
            cloneYear2.attr('name', "");
            cloneYear2.prepend($('<option>', { value: '', text: '-<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01102") %>-' }));
            cloneYear2.prop('selectedIndex', 0);
            $('#divCopyToYear').html(cloneYear2);

            $('#modalCopy').on('change', '#selectCopyFromYear', function () {
                var $obj = $("#selectCopyFromTerm");
                $("body").mLoading();
                $.get("/App_Logic/dataGeneric.ashx?mode=listterm&id=" + $(this).val(), function (response) {
                    $obj.empty();
                    $.each(response, function (index, value) {
                        $obj.append("<option value=" + value.nTerm + ">" + value.sTerm);
                    });
                    $("body").mLoading('hide');

                });
            });

            $('#modalCopy').on('change', '#selectCopyToYear', function () {
                var $obj = $("#selectCopyToTerm");
                $("body").mLoading();
                $.get("/App_Logic/dataGeneric.ashx?mode=listterm&id=" + $(this).val(), function (response) {
                    $obj.empty();
                    $.each(response, function (index, value) {
                        $obj.append("<option value=" + value.nTerm + ">" + value.sTerm);
                    });
                    $obj.prepend($('<option>', { value: '', text: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>' }));
                    $("body").mLoading('hide');
                });
            });
            $("#modalCopy").on('shown.bs.modal', function () {
               <%-- $('#spanCopyTo').text($('#<%= DropDownList1.ClientID %> :selected').text() + '/' + $('#<%= DropDownList2.ClientID%> :selected').text());--%>
            });
            ///*end copy plan *////
        });
        function changeFinger() {
            $.ajax("/Api/change/?userid=" + $("#ctl00_MainContent_hdfsid").val() + "&type=0", function (Result) {
            }).done(function (Result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111048") %> " + Result);
                $("#modalpopupdatamac .modal-footer").addClass("hidden");
            });
        }


        function getTremList(yearId, term) {
            $.get("/App_Logic/dataGeneric.ashx?mode=listterm&id=" + yearId, function (response) {
                $("[id*=DropDownList2] option").remove();
                $.each(response, function (index, value) {
                    $("[id*=DropDownList2]").append("<option value=" + value.nTerm + ">" + value.sTerm);
                });
                $("[id*=DropDownList2]").prepend($('<option>', { value: '', text: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>' }));
                if (term !== null) $("select[id*=DropDownList2]").val(term);

                console.log(term);
            });
        };

        function functionListstudent(controlsublevel, sontrolsublevel2) {
            var availableValueplane = [];
            $.ajax({
                url: "/App_Logic/dataGenericListData.ashx?mode=liststudent&nelevel=" +
                    $('#' + controlsublevel + ' option:selected').val() + "&nsublevel=" + $('#' + sontrolsublevel2 + ' option:selected').val(),
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].sName,
                            value: objjson[index].sID,
                            code: objjson[index].studentid,
                        };
                        availableValueplane[index] = newObject;
                    });
                }
            });
            return availableValueplane;
        }

        function getroomdata(idlv3, nterm) {
            var input = idlv3;
            nTermSubLevel2 = idlv3;
            var fields = input.split('~');

            var idlv2 = fields[0];
            $('#currentTerm').val(nterm);
           //// var nterm = $("#<%=DropDownList2.ClientID%>").val();

            document.getElementById('<%=room.ClientID %>').value = idlv2;

            $("#<%=txtSearch.ClientID%>").val('-1');
            $("#<%=txtSearch2.ClientID%>").val('-1');
            $("#<%=txtSearch3.ClientID%>").val('-1');

            $("#headerpopup").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>");
            var num = -1;
            $("#<%=txtSearch.ClientID%>").val(num).trigger('change');
            $("#<%=txtSearch2.ClientID%>").val(num).trigger('change');
            $("#<%=txtSearch3.ClientID%>").val(num).trigger('change');

            $.get("/App_Logic/classmemJSON.ashx?mode=classmember&id=" + idlv2 + "&term=" + nterm, "", function (Result) {
                $.each(Result, function (index) {
                    var head1 = Result[index].head1;
                    var one = Result[index].one;
                    var two = Result[index].two;
                    var headid = Result[index].headid;
                    var oneid = Result[index].oneid;
                    var twoid = Result[index].twoid;

                    if (headid > 0) {
                        $("#<%=txtSearch.ClientID%>").val(headid).trigger('change');
                    }
                    if (oneid > 0) {
                        $("#<%=txtSearch2.ClientID%>").val(oneid).trigger('change');

                    }
                    if (twoid > 0) {
                        $("#<%=txtSearch3.ClientID%>").val(twoid).trigger('change');

                    }
                });
            });
        }

        function ShowPopUP(userid, name) {
            $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>" +
                "เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
            $("#ctl00_MainContent_hdfsid").val(userid);
            $("#modalpopupdatamac .modal-footer").removeClass("hidden")
        }
        function funtionListSubLV3(controlsublevel, sontrolsublevel2) {
            var SubLVID = $('#' + controlsublevel + ' option:selected').val();
            var sSubLV = $('#' + controlsublevel + ' option:selected').text();
            $("#" + sontrolsublevel2 + " option").remove();
            $('select[id*=' + sontrolsublevel2 + ']')
                .append($("<option></option>")
                    .attr("value", "")
                    .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106126") %>"));
            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
                success: function (msg) {

                    $.each(msg, function (index) {
                        $('select[id*=' + sontrolsublevel2 + ']')
                            .append($("<option></option>")
                                .attr("value", msg[index].nTermSubLevel2)
                                .text(msg[index].nTSubLevel2));
                        //.text(sSubLV + " / " + msg[index].nTSubLevel2));
                    });

                }
            });
        }

        function funtionListSubLV3(controlsublevel, controlsublevel2, setvalues) {
            var SubLVID = $('#' + controlsublevel + ' option:selected').val();
            var sSubLV = $('#' + controlsublevel + ' option:selected').text();
            $("#" + controlsublevel2 + " option").remove();
            $('select[id*=' + controlsublevel2 + ']')
                .append($("<option></option>")
                    .attr("value", "")
                    .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"));
            var request = $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
                success: function (msg) {

                    $.each(msg, function (index) {
                        $('select[id*=' + controlsublevel2 + ']')
                            .append($("<option></option>")
                                .attr("value", msg[index].nTermSubLevel2)
                                .text(msg[index].nTSubLevel2));
                        //.text(sSubLV + " / " + msg[index].nTSubLevel2));
                    });

                }
            });
            request.done(function () {
                $('#' + controlsublevel2).val(setvalues);
            })
        }

        var nTermSubLevel2 = null;
        function reset1(term) {
            let _saveData = {
                "nTeacherHeadid": $("#<%=txtSearch.ClientID%>").val(),
                "nTeacherAssistOne": $("#<%=txtSearch2.ClientID%>").val(),
                "nTeacherAssistTwo": $("#<%=txtSearch3.ClientID%>").val(),
                "nTerm": $('#currentTerm').val(),
                "nTermSubLevel2": parseInt(nTermSubLevel2)
            };

            PageMethods.SaveData(_saveData, function (response) {
                LoadData();
                $("#modalpopproduct").modal('hide')
            });
        }

        function LoadData() {
            let parameter1 = $("[id*=ddlsublevel]").val();
            let parameter2 = $("[id*=ddlSubLV2]").val();
            let parameter3 = $("[id*=DropDownList2]").val();
            let parameter4 = $("[id*=DropDownList1]").val();
            $("body").mLoading();
            PageMethods.LoadData(parameter1, parameter2, parameter3, parameter4, function (response) {
                console.log(response)
                let _html = "";
                _html = '<table class=" table-hover dataTable" cellspacing="0" cellpadding="2" border="0" id="ctl00_MainContent_dgd" style="font-weight:normal;font-style:normal;text-decoration:none;width:100%;border-collapse:collapse;"> ' +
                    '<thead><tr class="headerCell" style="font-weight:bold;font-style:normal;text-decoration:none;">' +
                    '<th class="centertext" scope="col" style="width:10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th><th scope="col"  class="centertext" style="width:10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></th><th scope="col" style="width:15%;"  class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></th><th scope="col" style="width:20%;"  class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210005") %></th><th scope="col" style="width:20%;"  class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303021") %></th><th scope="col" style="width:20%;"  class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303022") %></th><th class="centertext" scope="col"><button type="button" class="btn btn-success" data-toggle="modal" data-target="#modalCopy"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801035") %></button ></th>' +
                    '</tr></thead>';

                if (response.Result) {
                    $.each(response.Result, function (e, s) {
                        _html += '<tr class="itemCell" style="font-weight:normal;font-style:normal;text-decoration:none;">' +
                            '<td class="centertext" >' + s.number + '</td >' +
                            '<td class="centertext" >' + s.term + '</td >' +
                            '<td  class="centertext">' + s.room + '</td>' +
                            '<td  class="centertext">' + (s.teacherHead ?? "") + '</td>' +
                            '<td  class="centertext">' + (s.teacherAssistOne ?? "") + '</td>' +
                            '<td  class="centertext">' + (s.teacherAssistTwo ?? "") + '</td>' +
                            '<td class="centertext" ><a href="#" onclick="getroomdata(\'' + s.idlv2 + '\'  ,\'' + s.nTerm + '\');" class="btn btn-info" data-toggle="modal" data-target="#modalpopproduct"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803006") %></a></td>' +
                            '</tr>'
                    });
                }

                _html += "</table>"
                $(".wrapper-table").html(_html)
                $("body").mLoading('hide');
            });
        }

        function CopyClass() {
            var yearFrom = $('#selectCopyFromYear').val();
            var yearTo = $('#selectCopyToYear').val();

            var level = $('#<%= ddlsublevel.ClientID%>').val();
            var room = $('#ddlSubLV2>').val();

            var termFrom = $('#selectCopyFromTerm').val();
            var termTo = $('#selectCopyToTerm').val();

            $("body").mLoading();
            PageMethods.CopyData(yearFrom, yearTo, termFrom, termTo, level, room, function (response) {

                console.log(response);
                if (response.Message == 'SUCCESS') {
                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                    });
                }
                else {
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                    });
                }
                $("body").mLoading('hide');
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803004") %>         
            </p>
        </div>
    </div>

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>
        <asp:HiddenField ID="hdfsid" runat="server" />
        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="DropDownList2" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="ddlsublevel" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                            <div class="col-md-3 ">
                                <select id="ddlSubLV2" class="form-control">
                                </select>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-center">
                                <br />
                                <button type="button" id="btnSearch" onclick="LoadData()" class="btn btn-fill btn-info">
                                    <span class="material-icons">search</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-warning  card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803002") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12 wrapper-table">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalpopproduct" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
            aria-hidden="true">
            <div class="modal-dialog global-modal">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4>
                            <strong id="headerpopup"></strong>
                        </h4>
                    </div>
                    <div class="modal-body product-add-container" id="modalpopupdata-content">
                        <div id="productpopup">
                            <div class="form-group row" id="row-name">
                                <div class="col-md-4 col-form-label">
                                    <span style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210005") %></span>
                                </div>

                                <div class="col-md-6">
                                    <input type="hidden" id="currentTerm" />
                                    <input type="hidden" runat="server" id="head" />
                                    <input type="hidden" runat="server" id="help1" />
                                    <input type="hidden" runat="server" id="help2" />
                                    <asp:DropDownList ID="txtSearch" runat="server" Style="width: 100%;" CssClass="js-example-basic-multiple2 selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" data-size="7" name="classchoice2[]">
                                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132060") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row" id="row-name">

                                <div class="col-md-4 col-form-label">
                                    <span style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303021") %></span>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="txtSearch2" runat="server" Style="width: 100%;" CssClass="js-example-basic-multiple3 selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" data-size="7" name="classchoice3[]">
                                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132061") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="form-group row" id="row-name">

                                <div class="col-md-4 col-form-label">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303022") %></span>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="txtSearch3" runat="server" Style="width: 100%;" CssClass="js-example-basic-multiple4 selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" data-size="7" name="classchoice4[]">
                                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132062") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>

                            <div class="row d-none">
                                <div class="col-md-4">
                                    <label class="pull-right">
                                        hidden</label>
                                </div>
                                <div class="col-md-8 ">
                                    <asp:TextBox ID="room" runat="server" class="form-control">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row text-center planadd-row">
                        <div class="col-md-12 button-segment">
                            <asp:Button CssClass="btn btn-success global-btn" ID="btnSave" runat="server" Visible="false"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" />
                            <div class="btn btn-success global-btn" id="resetbtn" onclick="reset1()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %></div>
                            <asp:Button CssClass="btn btn-danger global-btn d-none" ID="btnCancle" runat="server"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
                        </div>
                    </div>
                    <div class="form-group row" id="row-name">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <label></label>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalCopy" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
            aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4>
                            <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00331") %></strong>
                        </h4>
                    </div>
                    <div class="modal-body product-add-container">

                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-3 col-form-label text-center">
                                        <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201074") %></strong>
                                    </div>
                                    <div class="col-md-7">
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-3 col-form-label text-center">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></span>
                                    </div>
                                    <div class="col-md-3">
                                        <div id="divCopyFromYear">
                                        </div>
                                    </div>
                                    <div class="col-md-3  text-center col-form-label text-center">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></span>
                                    </div>
                                    <div class="col-md-3">
                                        <div id="divCopyFromTerm">
                                            <select id="selectCopyFromTerm" class="form-control">
                                                <option value="">-<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01102") %>-</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <br />
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-3 col-form-label text-center">
                                        <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01107") %></strong>
                                    </div>
                                    <div class="col-md-7">
                                    </div>
                                </div>
                            </div>
                            <%-- <div class="col-md-12">
                                <br />
                            </div>--%>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-3 col-form-label text-center">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></span>
                                    </div>
                                    <div class="col-md-3">
                                        <div id="divCopyToYear">
                                        </div>
                                    </div>
                                    <div class="col-md-3 text-center col-form-label">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></span>
                                    </div>
                                    <div class="col-md-3">
                                        <div id="divCopyToTerm">
                                            <select id="selectCopyToTerm" class="form-control">
                                                <option value="">-<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01102") %>-</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer ">
                        <div class="col-md-12 text-center">
                            <button type="button" class="btn btn-success" onclick="CopyClass()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>


</asp:Content>
