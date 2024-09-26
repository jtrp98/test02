<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StdHealth.aspx.cs" Inherits="FingerprintPayment.StudentInfo.StdHealth" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        /* Health Table */
        .table-cell-center {
            text-align: center;
        }

            .table-cell-center thead th:first-child, .table-cell-center tbody th:first-child {
                text-align: center;
                width: 120px;
            }

            .table-cell-center thead th {
                text-align: center;
            }

            .table-cell-center tbody tr td {
                padding-top: 10px;
                padding-bottom: 6px;
                padding-left: 4px;
                padding-right: 4px;
            }

                .table-cell-center tbody tr td input {
                    text-align: center;
                }

            .table-cell-center.level-n-6 {
                text-align: center;
                font-size: 14px;
                color: white;
            }

                .table-cell-center.level-n-6 thead th:first-child, .table-cell-center.level-n-6 tbody th:first-child {
                    text-align: center;
                    width: 120px;
                }

                .table-cell-center.level-n-6 tbody tr:not(:first-child) th:first-child, .table-cell-center.level-n-6 tbody tr.age td {
                    color: black;
                }

                .table-cell-center.level-n-6 thead th {
                    text-align: center;
                }

                .table-cell-center.level-n-6 tbody tr td {
                    padding-top: 12px;
                    padding-bottom: 4px;
                }

                    .table-cell-center.level-n-6 tbody tr td input {
                        text-align: center;
                    }

            .table-cell-center thead th.highlight, .table-cell-center tbody tr.bg-primary td.highlight {
                background-color: #d9534f55;
            }

            .table-cell-center tbody td.highlight, .table-cell-center tbody tr.age td.highlight {
                background-color: #d9534f11;
            }

        .input-health {
            background: transparent;
            border: none;
            border-bottom: 1px solid #ccc;
            -webkit-box-shadow: none;
            box-shadow: none;
            border-radius: 0;
        }

            .input-health:focus {
                box-shadow: -1px 8px 8px -6px rgba(0,0,0,0.075);
                outline: 0 none;
            }

            .input-health:placeholder {
                color: gray;
                opacity: 0.2; /* Firefox */
            }

            .input-health:-ms-input-placeholder { /* Internet Explorer 10-11 */
                color: gray;
            }

            .input-health:-ms-input-placeholder { /* Microsoft Edge */
                color: gray;
            }

        .input-health-view {
            background: transparent;
            border: none;
            border-bottom: 1px solid #ccc;
            -webkit-box-shadow: none;
            box-shadow: none;
            border-radius: 0;
            margin: 0px 0px 0px 0px;
            display: inline-block;
            color: black;
        }

        /* Tooltip */
        .tooltip {
            position: relative;
            display: inline-block;
            border-bottom: 1px dotted black;
            opacity: 1;
        }

            .tooltip .tooltiptext {
                visibility: hidden;
                width: 220px;
                background-color: #555;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 5px 0;
                position: absolute;
                z-index: 1;
                bottom: 125%;
                left: 50%;
                margin-left: -60px;
                opacity: 0;
                transition: opacity 0.3s;
                font-family: THSarabunNew;
                font-size: 12px;
                line-height: 1.8;
            }

                .tooltip .tooltiptext::after {
                    content: "";
                    position: absolute;
                    top: 100%;
                    left: 25%;
                    margin-left: 0px;
                    margin-top: -1px;
                    border-width: 5px;
                    border-style: solid;
                    border-color: #555 transparent transparent transparent;
                }

            .tooltip:hover .tooltiptext {
                visibility: visible;
                opacity: 1;
            }

        @media (min-width: 992px) {
            .col-md-offset-3 {
                margin-left: 25%;
            }

            .col-md-offset-9 {
                margin-left: 75%;
            }
        }
    </style>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            List Content
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="stdHealthForm">
                <form id="stdGraphHealthForm" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %></p>
                    <div class="row div-row-padding">
                        <div class="col-md-3 col-md-offset-9">
                            <div class="tooltip">
                                <i class="fa fa-question-circle" />
                                <span class="tooltiptext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101210") %></span>
                            </div>
                        </div>
                        <div class="col-md-6 col-md-offset-3" style="width: 800px; height: 450px;">
                            <canvas id="cvsHealthGraph" width="800" height="450"></canvas>
                        </div>
                    </div>
                </form>
                <form id="stdHealthForm" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101211") %></p>
                    <div class="row div-row-padding" style="margin-top: 8px;">
                        <div class="col-md-12 mb-12">
                            <asp:Literal ID="ltrTable" runat="server">ltrTable</asp:Literal>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right">
                            <label for="sltHealthBlood"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101217") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltHealthBlood" name="sltHealthBlood" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101218") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101218") %></option>
                                <option value="A">A</option>
                                <option value="A+">A+</option>
                                <option value="A-">A-</option>
                                <option value="B">B</option>
                                <option value="B+">B+</option>
                                <option value="B-">B-</option>
                                <option value="AB">AB</option>
                                <option value="AB+">AB+</option>
                                <option value="AB-">AB-</option>
                                <option value="O">O</option>
                                <option value="O+">O+</option>
                                <option value="O-">O-</option>
                                <option value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01370") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01370") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101219") %></label>
                        </div>
                        <div class="col-md-3 mb-3">
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right">
                            <label for="iptHealthSickFood"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101220") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHealthSickFood" name="iptHealthSickFood"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101220") %>" maxlength="255" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right">
                            <label for="iptHealthSickDrug"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131225") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHealthSickDrug" name="iptHealthSickDrug"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131225") %>" maxlength="255" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right">
                            <label for="iptHealthSickOther"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101222") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHealthSickOther" name="iptHealthSickOther"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101222") %>" maxlength="255" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right">
                            <label for="iptHealthSickCongenital"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101223") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHealthSickCongenital" name="iptHealthSickCongenital"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101223") %>" maxlength="255" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right">
                            <label for="iptHealthSickDanger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101224") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHealthSickDanger" name="iptHealthSickDanger"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101224") %>" maxlength="255" />
                        </div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center">
                        <button id="saveHealth" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="saveHealthAndNext" type="submit" class="btn btn-info save-next"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101126") %></button>
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var stdHealthForm = {
                    GetItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdHealth.aspx/GetItem",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdHealthForm.OnSuccessGet,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessGet: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {
                            xStdKey.hltid = '0';
                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                xStdKey.hltid = $(this).find("F0").text();
                                $("#sltHealthBlood").selectpicker('val', $(this).find("F1").text());
                                $("#iptHealthSickFood").val($(this).find("F2").text());
                                $("#iptHealthSickDrug").val($(this).find("F3").text());
                                $("#iptHealthSickOther").val($(this).find("F4").text());
                                $("#iptHealthSickCongenital").val($(this).find("F5").text());
                                $("#iptHealthSickDanger").val($(this).find("F6").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdHealth.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdHealthForm.OnSuccessSave,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSave: function (response) {
                        var title = "";
                        var body = "";

                        var flag = response.d.split('-');
                        switch (flag[0]) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                xStdKey.hltid = flag[1];

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {
                                    refreshHealthGraph(<%=Request.QueryString["sid"]%>, '<%=Request.QueryString["tid"]%>');
                                });

                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111026") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %>';

                                break;
                            default: break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    SaveAndNextItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdHealth.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdHealthForm.OnSuccessSaveAndNext,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSaveAndNext: function (response) {
                        var title = "";
                        var body = "";

                        var flag = response.d.split('-');
                        switch (flag[0]) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

                                    // Redirect to student list
                                    window.location.replace("StudentDetail.aspx?v=form&sid=" + StudentDetail_NextSID + "&tid=" + xStdKey.tid);

                                });

                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111026") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %>';

                                break;
                            default: break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ClearSession: function (callbackRedirect) {
                        $.ajax({
                            type: "POST",
                            url: "StdHealth.aspx/ClearSessionID",
                            data: '{}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                callbackRedirect();
                            },
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    }
                }

                var healthGraph;
                function refreshHealthGraph(stdID, termID) {
                    $.ajax({
                        type: "POST",
                        url: "StdHealth.aspx/ReloadHealthGraph",
                        data: '{stdID: ' + stdID + ', termID: \'' + termID + '\'}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            var jsonObj = $.parseJSON(result.d);

                            healthGraph.data.datasets[0].data = jsonObj.graphData;

                            healthGraph.update();
                        },
                        failure: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                            $("#modalWaitDialog").modal('hide');
                        },
                        error: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                            $("#modalWaitDialog").modal('hide');
                        }
                    });
                }

                function drawHealthGraph(label, data) {
                    var chartInfo = {
                        type: 'line',
                        data: {
                            labels: label,
                            datasets: [{
                                label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111031") %>',
                                data: data,
                                borderWidth: 2,
                                pointStyle: 'circle',
                                backgroundColor: 'rgba(0,0,0,0.0)',
                                borderColor: '#c45850'
                            },
                            {
                                label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111032") %>',
                                data: [18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5,],
                                fill: 'origin',
                                pointStyle: 'dash',
                                borderWidth: 1,
                                backgroundColor: 'rgba(192,192,192,0.3)'
                            },
                            {
                                label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>',
                                data: [25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25],
                                borderWidth: 1,
                                pointStyle: 'dash',
                                backgroundColor: 'rgba(0,255,0,0.3)'
                            },
                            {
                                label: 'อ้วนระดับ1',
                                data: [30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30,],
                                borderWidth: 1,
                                pointStyle: 'dash',
                                backgroundColor: 'rgba(255,255,0,0.3)'
                            },
                            {
                                label: 'อ้วนระดับ2',
                                data: [35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35],
                                borderWidth: 1,
                                pointStyle: 'dash',
                                backgroundColor: 'rgba(255,122,0,0.3)'
                            },
                            {
                                label: 'อ้วนระดับ3',
                                data: [40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40],
                                borderWidth: 1,
                                pointStyle: 'dash',
                                backgroundColor: 'rgba(255,0,0,0.3)'
                            }]
                        },
                        options: {
                            responsive: true,
                            maintainAspectRatio: false,
                            elements: { line: { fill: '-1' } },
                            scales: {
                                yAxes: [{
                                    scaleLabel: {
                                        display: true,
                                        labelString: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111036") %>'
                                    }
                                }],
                                xAxes: [{
                                    scaleLabel: {
                                        display: true,
                                        labelString: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111037") %>'
                                    }
                                }]
                            },
                            title: {
                                display: true,
                                text: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111038") %>'
                            },
                            legend: {
                                onClick: null
                            },
                            animation: {
                                duration: 0 // general animation time
                            },
                            hover: {
                                animationDuration: 0 // duration of animations when hovering an item
                            },
                            responsiveAnimationDuration: 0 // animation duration after a resize
                        }
                    }

                    var canvas = document.getElementById('cvsHealthGraph');
                    var ctx = canvas.getContext('2d');
                    // clear canvas
                    ctx.clearRect(0, 0, canvas.width, canvas.height);

                    healthGraph = new Chart(ctx, chartInfo);
                }

                $(document).ready(function () {

                    $("#stdHealthForm").validate({
                        rules: {
                            sltHealthBlood: "required"
                        },
                        messages: {
                            sltHealthBlood: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "sltHealthBlood": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    function GetHealthDataFromInput() {

                        var healthInfo = {};

                        healthInfo.sID = xStdKey.sid;
                        healthInfo.hID = xStdKey.hltid;
                        healthInfo.blood = $("#sltHealthBlood").val();
                        healthInfo.sickFood = $("#iptHealthSickFood").val();
                        healthInfo.sickDrug = $("#iptHealthSickDrug").val();
                        healthInfo.sickOther = $("#iptHealthSickOther").val();
                        healthInfo.sickCongenital = $("#iptHealthSickCongenital").val();
                        healthInfo.sickDanger = $("#iptHealthSickDanger").val();

                        var healthData = [];
                        $('.input-health').filter(function () { return !!this.value; }).each(function (index) {
                            healthData.push(jQuery.parseJSON('{"type":"' + $(this).attr('data-type') + '", "level":' + $(this).attr('data-level') + ', "month":' + $(this).attr('data-month') + ', "value":' + ($(this).val() ? $(this).val() : null) + '}'));
                        });
                        healthInfo.healthData = healthData;

                        return healthInfo;
                    }

                    $(".stdHealthForm #saveHealth").bind({
                        click: function () {

                            if ($('#stdHealthForm').valid()) {

                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetHealthDataFromInput();

                                    stdHealthForm.SaveItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".stdHealthForm #saveHealthAndNext").bind({
                        click: function () {

                            if ($('#stdHealthForm').valid()) {

                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetHealthDataFromInput();

                                    stdHealthForm.SaveAndNextItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".stdHealthForm .btn-cancel").bind({
                        click: function () {

                            // Redirect to employee list
                            stdHealthForm.ClearSession(function () {
                                window.location.replace("StudentList.aspx");
                            });

                            return false;
                        }
                    });

                    // Modal Section
                    $('#modalNotifyConfirmSave').off().on('show.bs.modal', function (e) {
                        $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                        $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
                    });

                    // Initial data

                    activateBootstrapSelect('.stdHealthForm .selectpicker');

                    $('.input-health').number(true, 2);

                    // Load info command
                    stdHealthForm.GetItem(<%=Request.QueryString["sid"]%>);

                    <%=GenerateGraph%>
                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            <div class="stdHealthView view-form">
                <form id="stdGraphHealthView" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %></p>
                    <div class="row div-row-padding">
                        <div class="col-md-3 col-md-offset-9">
                            <div class="tooltip">
                                <i class="fa fa-question-circle" />
                                <span class="tooltiptext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101210") %></span>
                            </div>
                        </div>
                        <div class="col-md-6 col-md-offset-3" style="width: 800px; height: 450px;">
                            <canvas id="cvsHealthGraphView" width="800" height="450"></canvas>
                        </div>
                    </div>
                </form>
                <form id="stdHealthView" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101211") %></p>
                    <div class="row div-row-padding" style="margin-top: 8px;">
                        <div class="col-md-12 mb-12">
                            <asp:Literal ID="ltrTableView" runat="server">ltrTable</asp:Literal>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101217") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHealthBlood"></span>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101219") %></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101220") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHealthSickFood"></span>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131225") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHealthSickDrug"></span>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101222") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHealthSickOther"></span>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101223") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHealthSickCongenital"></span>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-md-offset-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101224") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHealthSickDanger"></span>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center">
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var stdHealthView = {
                    GetItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdHealth.aspx/GetItemView",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdHealthView.OnSuccessGet,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessGet: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {
                            xStdKey.hltid = '0';
                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                xStdKey.hltid = $(this).find("F0").text();
                                $("#spHealthBlood").text($(this).find("F1").text());
                                $("#spHealthSickFood").text($(this).find("F2").text());
                                $("#spHealthSickDrug").text($(this).find("F3").text());
                                $("#spHealthSickOther").text($(this).find("F4").text());
                                $("#spHealthSickCongenital").text($(this).find("F5").text());
                                $("#spHealthSickDanger").text($(this).find("F6").text());

                            });

                        }
                    }
                }

                var healthGraph;
                function refreshHealthGraph(stdID, termID) {
                    $.ajax({
                        type: "POST",
                        url: "StdHealth.aspx/ReloadHealthGraph",
                        data: '{stdID: ' + stdID + ', termID: \'' + termID + '\'}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            var jsonObj = $.parseJSON(result.d);

                            healthGraph.data.datasets[0].data = jsonObj.graphData;

                            healthGraph.update();
                        },
                        failure: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                            $("#modalWaitDialog").modal('hide');
                        },
                        error: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                            $("#modalWaitDialog").modal('hide');
                        }
                    });
                }

                function drawHealthGraph(label, data) {
                    var chartInfo = {
                        type: 'line',
                        data: {
                            labels: label,
                            datasets: [{
                                label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111031") %>',
                                data: data,
                                borderWidth: 2,
                                pointStyle: 'circle',
                                backgroundColor: 'rgba(0,0,0,0.0)',
                                borderColor: '#c45850'
                            },
                            {
                                label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111032") %>',
                                data: [18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5,],
                                fill: 'origin',
                                pointStyle: 'dash',
                                borderWidth: 1,
                                backgroundColor: 'rgba(192,192,192,0.3)'
                            },
                            {
                                label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>',
                                data: [25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25],
                                borderWidth: 1,
                                pointStyle: 'dash',
                                backgroundColor: 'rgba(0,255,0,0.3)'
                            },
                            {
                                label: 'อ้วนระดับ1',
                                data: [30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30,],
                                borderWidth: 1,
                                pointStyle: 'dash',
                                backgroundColor: 'rgba(255,255,0,0.3)'
                            },
                            {
                                label: 'อ้วนระดับ2',
                                data: [35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35],
                                borderWidth: 1,
                                pointStyle: 'dash',
                                backgroundColor: 'rgba(255,122,0,0.3)'
                            },
                            {
                                label: 'อ้วนระดับ3',
                                data: [40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40],
                                borderWidth: 1,
                                pointStyle: 'dash',
                                backgroundColor: 'rgba(255,0,0,0.3)'
                            }]
                        },
                        options: {
                            responsive: true,
                            maintainAspectRatio: false,
                            elements: { line: { fill: '-1' } },
                            scales: {
                                yAxes: [{
                                    scaleLabel: {
                                        display: true,
                                        labelString: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111036") %>'
                                    }
                                }],
                                xAxes: [{
                                    scaleLabel: {
                                        display: true,
                                        labelString: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111037") %>'
                                    }
                                }]
                            },
                            title: {
                                display: true,
                                text: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111038") %>'
                            },
                            legend: {
                                onClick: null
                            },
                            animation: {
                                duration: 0 // general animation time
                            },
                            hover: {
                                animationDuration: 0 // duration of animations when hovering an item
                            },
                            responsiveAnimationDuration: 0 // animation duration after a resize
                        }
                    }

                    var canvas = document.getElementById('cvsHealthGraphView');
                    var ctx = canvas.getContext('2d');
                    // clear canvas
                    ctx.clearRect(0, 0, canvas.width, canvas.height);

                    healthGraph = new Chart(ctx, chartInfo);
                }

                $(document).ready(function () {

                    $(".stdHealthView .btn-cancel").bind({
                        click: function () {

                            window.close();

                            return false;
                        }
                    });

                    // Load info command
                    stdHealthView.GetItem(<%=Request.QueryString["sid"]%>);

                    <%=GenerateGraph%>
                });

            </script>
        </asp:View>
    </asp:MultiView>
</body>
</html>
