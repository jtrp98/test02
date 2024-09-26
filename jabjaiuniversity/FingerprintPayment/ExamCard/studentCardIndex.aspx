<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Material2.Master" CodeBehind="studentCardIndex.aspx.cs" Inherits="FingerprintPayment.ExamCard.studentCardIndex" %>

<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>
<%@ Register Src="~/UserControls/YTLCFilter.ascx" TagPrefix="uc1" TagName="YTLCFilter" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Styles/SettingDialog.css" rel="stylesheet" />
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <style>
        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106092") %>
            </p>
        </div>
    </div>

    <form id="aspnetForm" runat="server">

        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Release" />

        <asp:HiddenField ID="hdfschoolname" runat="server" />

        <div class="row ">
            <div class="col-md-12">
                <div class="card ">

                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                    </div>
                    <div class="card-body ">
                        <uc1:YTLCFilter runat="server" ID="YTLCFilter" IsRequired="true" />


                        <div class="row">
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                            <div class="col-md-3">
                                <div class="form-group ">
                                    <uc1:StudentAutocomplete runat="server" ID="StudentAutocomplete" />
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label"></label>
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-2"></div>
                        </div>



                        <div class="row">
                            <div class="col-md-12 text-center">
                                <button type="button" class="btn btn-info " onclick="searchData();">
                                    <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row ">
            <div class="col-md-12">
                <div class="card ">

                    <div class="card-header card-header-warning  card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %></h4>
                    </div>
                    <div class="card-body ">
                        <table id="example" class=" table-hover dataTable table-show-result" style="width: 100%"></table>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h3 class="modal-title" id="exampleModalLabel"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106096") %></strong></h3>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class=" col-sm-12">
                                <div class="row">
                                    <label class="col-md-5 col-sm-6 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106097") %> :</label>
                                    <div class="col-md-7 col-sm-6">
                                        <select id="select_exam" class="selectpicker col-sm-12" data-style="select-with-transition">
                                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106098") %></option>
                                            <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106099") %></option>
                                            <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106100") %></option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class=" col-sm-12">
                                <div class="row">
                                    <label class="col-md-5 col-sm-6 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106101") %> :</label>
                                    <div class="col-md-7 col-sm-6">
                                        <div class="form-group">
                                            <input type="text" id="txtstart" class="form-control datepicker" />
                                            <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                                <i class="material-icons">event</i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=" col-sm-12">
                                <div class="row">
                                    <label class="col-md-5 col-sm-6 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106102") %> :</label>
                                    <div class="col-md-7 col-sm-6">
                                        <div class="form-group">
                                            <input type="text" id="txtend" class="form-control datepicker" />
                                            <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                                <i class="material-icons">event</i>
                                            </span>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="col-sm-12 text-center">
                            <button type="button" class="btn btn-success" onclick="Printsetting()" data-dismiss="modal"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></strong></button>

                            <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="Canclesetting()"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></strong></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="../Report/ScriptReport.js" type="text/javascript"></script>
    <script src="studentCard.js?v=1" type="text/javascript"></script>
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>

    <script type="text/javascript">

        var date = '<%= DateTime.Today.ToString("dd/MM/yyyy") %>';
        var sTudent_Card = new sTudent_Card();
        var availableValuestudent = [];
        var yEar = "";
        var tErm = "";
        var sUbLV = "";
        var sUbLV2 = "";

        var dStart = "";
        var dEnd = "";

        var exam = "";

        var final_year = "";
        var final_term = "";

        $(function () {

            $('.datepicker').datetimepicker({
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

            $(".datepicker").keydown(function (e) {
                e.preventDefault();
            });

            if (jQuery.validator) {//.messages

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",
                });

                $("#aspnetForm").validate({  // initialize the plugin

                    errorPlacement: function (error, element) {
                        let _class = element.attr('class');

                        if (_class.includes('--date-validate') || _class.includes('--req-append-last')) {
                            error.insertAfter(element.parent());
                        }
                        else {
                            error.insertAfter(element);
                        }

                    }

                });
            }

            //$(".datepicker").datepicker({
            //});

            //$('select[id*=ddlyear]').change(function () {
            //    GetListTrem();
            //});

            //$('select[id*=ddlsublevel]').change(function () {
            //    GetlistSubLV2();
            //});

            //$('select[id*=ddlSubLV2]').change(function () {
            //    Getliststudent();
            //});

            //$('#txtname').autocomplete({
            //    width: 300,
            //    max: 10,
            //    delay: 100,
            //    minLength: 1,
            //    autoFocus: true,
            //    cacheLength: 1,
            //    scroll: true,
            //    highlight: false,
            //    source: function (request, response) {
            //        var results;
            //        results = $.ui.autocomplete.filter(availableValuestudent, request.term);
            //        response(results.slice(0, 10));
            //    },
            //    select: function (event, ui) {
            //        event.preventDefault();
            //        $("#txtname").val(ui.item.label);
            //        $("#txtid").val(ui.item.value);
            //    },
            //    focus: function (event, ui) {
            //        event.preventDefault();
            //        $("#txtid").val("");
            //    }
            //});
        });

        function Modelsetting() {
            $("#exampleModal").modal()
        }


        function searchData() {

            if (!$("#aspnetForm").valid()) {
                return;
            }

            yEar = YTLCF.GetYearID();
            tErm = YTLCF.GetTermID();
            sUbLV = YTLCF.GetLevelID();
            sUbLV2 = YTLCF.GetClassID();

            var data = {
                "yEar_Id": yEar,
                "tErm_Id": tErm,
                "sUbLV_Id": sUbLV,
                "sUbLV2_Id": sUbLV2,
                "student_id": SAC.GetStudentID()
            };
        
            $("body").mLoading();      
            PageMethods.return_data(data, function (e) {
                console.log(e.data);
                sTudent_Card.return_data = e;
                sTudent_Card.RenderHtml("example", false);
                $("body").mLoading('hide');
            });
        }


        function Printsetting() {

        }

        function Canclesetting() {
            $("#txtstart").val('');
            $("#txtend").val('');
        }

        function SentDataCardAll() {

            tErm = YTLCF.GetTermID(); /*<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %>*/
            sUbLV = YTLCF.GetLevelID(); /*<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %>*/
            sUbLV2 = YTLCF.GetClassID(); /*<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %>*/

            dStart = document.getElementById('txtstart').value;
            dEnd = document.getElementById('txtend').value;

            exam = select_exam.options[select_exam.selectedIndex].value;

            //var queryString = "?tErm=" + tErm + "&sUbLV2=" + sUbLV2 + "&dStart=" + dStart + "&dEnd=" + dEnd + "&exam=" + exam + "&sUbLV" + sUbLV;
            var queryString = "?tErm=" + tErm + "&sUbLV2=" + sUbLV2 + "&dStart=" + dStart + "&dEnd=" + dEnd + "&exam=" + exam;

            if (dStart == "" || dEnd == "") {
                alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106103") %>");
            } else {
                window.open('studentCardPrintSuperAll.aspx' + queryString);
            }


        }

        function SentDataCardId(sID) {

            yEar = YTLCF.GetYearID();;
            tErm = YTLCF.GetTermID();

            dStart = document.getElementById('txtstart').value;
            dEnd = document.getElementById('txtend').value;

            exam = select_exam.options[select_exam.selectedIndex].value;

            var queryString = "?sID=" + sID + "&dStart=" + dStart + "&dEnd=" + dEnd + "&exam=" + exam + "&yEar=" + yEar + "&tErm=" + tErm;

            if (dStart == "" || dEnd == "") {
                alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106103") %>");
            } else {
                window.open('studentPrintAll.aspx' + queryString);
            }


        }

        //function GetListTrem() {
        //    var YearID = $('#ctl00_MainContent_ddlyear option:selected').val();
        //    $('select[id*=semister] option').remove();
        //    $.ajax({
        //        url: "/App_Logic/dataGeneric.ashx?mode=listterm&id=" + YearID,
        //        success: function (msg) {
        //            $.each(msg, function (index) {
        //                $('select[id*=semister]')
        //                    .append($("<option></option>")
        //                        .attr("value", msg[index].nTerm)
        //                        .text(msg[index].sTerm)
        //                    );
        //            });
        //        }
        //    });
        //}

        //function GetlistSubLV2() {
        //    var SubLVID = $('#ctl00_MainContent_ddlsublevel option:selected').val();
        //    $('select[id*=ddlSubLV2] option').remove();
        //    $.ajax({
        //        url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
        //        success: function (msg) {
        //            $.each(msg, function (index) {
        //                $('select[id*=ddlSubLV2]')
        //                    .append($("<option></option>")
        //                        .attr("value", msg[index].nTermSubLevel2)
        //                        .text(msg[index].nTSubLevel2)
        //                    );
        //            });
        //        }
        //    });
        //}

        //function Getliststudent() {
        //    $.ajax({
        //        url: "/App_Logic/dataGenericListData.ashx?mode=liststudent&nelevel=" +
        //            $('#ctl00_MainContent_ddlsublevel option:selected').val() + "&nsublevel=" + $('select[id*=ddlSubLV2] option:selected').val(),
        //        dataType: "json",
        //        success: function (objjson) {
        //            $.each(objjson, function (index) {
        //                var newObject = {
        //                    label: objjson[index].sName,
        //                    value: objjson[index].sID,
        //                    studentid: objjson[index].studentid
        //                };
        //                availableValuestudent[index] = newObject;
        //            });
        //        }
        //    });
        //}



    </script>
</asp:Content>
