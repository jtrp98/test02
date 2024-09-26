<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="Reportmobile03.aspx.cs" Inherits="FingerprintPayment.Reportmobile03" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
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

        var availableValueplane = [];

        $(document).ready(function () {

            document.getElementById('txtname').addEventListener('input', function () {
                if (this.value === '') {
                    $("#txtid").val("");
                }
            }, false);

            getListSubLV2();
            getliststudent();
            getListTrem();
            $('#ctl00_MainContent_ddlsublevel').change(function () {
                getListSubLV2();
                getliststudent();
            });

            $('#ctl00_MainContent_ddlyear').change(function () {
                $('input[id*=txtSubLV2ID]').val("");
                getListTrem();
            });

            $('select[id*=ddlSubLV2]').change(function () {
                getliststudent();
            });

            function getliststudent() {
                availableValueplane = [];
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
                            availableValueplane[index] = newObject;
                        });

                    }
                });
            }

            $('input[id*=txtname]').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                maxLength: 10,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                source: function (request, response) {
                    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("input[id*=txtname]").val(ui.item.label);
                    $("#txtid").val(ui.item.value);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                    $("#txtid").val("");
                }
            });
        });

        function reportEmployees() {
            var data = [];
            var userid = $('input[id*=txtid]').val();
            var tremid = $('#ctl00_MainContent_semister option:selected').val();
            if (userid == "") {
                alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131135") %>");
                return;
            }

            var score = 0;
            var StatusIN_0 = 0;
            var StatusIN_1 = 0;
            var StatusIN_2 = 0;
            var sHeaderReports = "";
            var request = $.ajax({
                type: "POST",
                url: "/App_Logic/dataReportGeneric.ashx?mode=GetReportmobile02&userid=" + userid + "&tremid=" + tremid,
                cache: false,
                contentType: 'application/json;',
                success: function (obj, status) {
                    $.each(obj, function (index) {

                        $("#labelsname").html(obj[index].sName + " " + obj[index].sLastname);
                        $("#txtnMoney").val(obj[index].nMoney);
                        $("#txtnMax").val(obj[index].nMax);
                        sHeaderReports = obj[index].sHeaderReports;

                        switch (obj[index].StatusIN) {
                            case "0  ":
                                score += 3;
                                StatusIN_0 += 1;
                                break;
                            case "1  ":
                                score += 1;
                                StatusIN_1 += 1;
                                break;
                            case "3  ":
                                score += 0;
                                StatusIN_2 += 1;
                                break;
                            default:
                                score += 0;
                                StatusIN_2 += 1;
                                break;
                        }
                    });
                }
            });

            request.done(function (msg) {
                var per = score / (msg.length * 3);
                per = per * 100;
                StatusIN_0 = (StatusIN_0 / msg.length) * 100;
                StatusIN_1 = (StatusIN_1 / msg.length) * 100;
                StatusIN_2 = (StatusIN_2 / msg.length) * 100;

                if (per < 51) {
                    $("#valuescore").html("F")
                }
                else if (per < 50) {
                    $("#valuescore").html("D");
                } else if (per < 70) {
                    $("#valuescore").html("D+");
                } else if (per <= 75) {
                    $("#valuescore").html("C");
                } else if (per <= 80) {
                    $("#valuescore").html("C+");
                } else if (per <= 85) {
                    $("#valuescore").html("B");
                } else if (per <= 90) {
                    $("#valuescore").html("B+");
                } else if (per <= 95) {
                    $("#valuescore").html("A");
                }
                else {
                    $("#valuescore").html("A+");
                }
                $(function () {
                    $('#container').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'pie'
                        },
                        title: {
                            text: sHeaderReports
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                                    }
                                }
                            }
                        },
                        series: [{
                            name: 'Percent',
                            colorByPoint: true,
                            data: [{
                                name: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>',
                                y: StatusIN_0,
                                sliced: true,
                                selected: true
                            }, {
                                name: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131136") %>',
                                y: StatusIN_1
                            }, {
                                name: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>',
                                y: StatusIN_2
                            }]
                        }]
                    });
                });
            }
            );
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="full-card box-content">
            <div class="row student">
                <div class="form-group col-sm-2">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :
                </div>
                <div class="form-group col-sm-4">
                    <asp:DropDownList ID="ddlyear" runat="server" class="form-control">
                        <asp:ListItem Text="2558" Value="2557" Selected="True" />
                        <asp:ListItem Text="2559" Value="2557" Selected="False" />
                        <asp:ListItem Text="2560" Value="2557" Selected="False" />
                    </asp:DropDownList>
                </div>
                <div class="form-group col-sm-2">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :
                </div>
                <div class="form-group col-sm-4">
                    <asp:DropDownList ID="semister" runat="server" class="form-control">
                        <asp:ListItem Text="1" Value="1" Selected="True" />
                        <asp:ListItem Text="2" Value="2" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row student">
                <div class="form-group col-sm-2">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> :
                </div>
                <div class="form-group col-sm-4">
                    <asp:DropDownList ID="ddlsublevel" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
                <div class="form-group col-sm-2">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> :
                </div>
                <div class="form-group col-sm-4">
                    <asp:DropDownList ID="ddlSubLV2" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-sm-2">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %> :
                </div>
                <div class="form-group col-sm-4">
                    <input type="text" class='form-control' id="txtid" style="display: none;" />
                    <input type="text" class='form-control' id="txtname" />
                </div>
                <div class="form-group col-sm-6">
                </div>
            </div>
            <div class="row">
                <div class="form-group col-sm-2">
                </div>
                <div class="form-group col-sm-4">
                    <input type="button" class="btn btn-primary" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="reportEmployees();" />
                </div>
                <div class="form-group col-sm-6">
                </div>
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-sm-12 col-md-4">
                <fieldset>
                    <legend><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131137") %></legend>
                    <div class="form-group">
                        <label class="control-label col-sm-2 col-md-6">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> :</label>
                        <p class="form-control-static text-big col-sm-4 col-md-6 text-md-left" id="labelsname">
                            lorem ipsum
                        </p>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2 col-md-6">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131138") %> :</label>
                        <p class="form-control-static text-big col-sm-4 col-md-6 text-md-left" id="valuescore">
                            A
                        </p>
                    </div>
                </fieldset>
                <fieldset>
                    <legend><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131134") %></legend>
                    <div class="form-group">
                        <label class="control-label col-sm-2 col-md-6">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602009") %></label>
                        <div class="col-sm-4 col-md-6">
                            <input type="text" id="txtnMoney" readonly="true" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2 col-md-6">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131139") %> :</label>
                        <div class=" col-sm-4 col-md-6">
                            <div class="input-group">
                                <input type="text" id="txtnMax" readonly="true" class="form-control" /><div class="input-group-addon">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131140") %>
                                </div>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-sm-12 col-md-8">
                <fieldset>
                    <legend><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131141") %></legend>
                    <div id="container" style="background: #c0c0c0; height: auto; margin: 0 auto">
                        <div style="padding: 25% 30%">
                            <b style="color: white; font-size: 40px;">Graph</b>
                        </div>
                    </div>
                    <div id="container2" style="background: #c0c0c0; height: auto; margin: 0 auto">
                        <div style="padding: 25% 30%">
                            <b style="color: white; font-size: 40px;">Graph</b>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>
