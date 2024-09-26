<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="reportsmonth.aspx.cs" Inherits="FingerprintPayment.Report.reportsmonth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>

    <script type="text/javascript">
        $(function () {
            $("#exportfile").click(function () {
                var dt = new Date();
                $('#example').tableExport({ type: 'excel', escape: 'false', sheets: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104001") %>', fileName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104001") %>_" + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
            });

            $('select[id*=ddlsublevel]').change(function () {
                $('input[id*=txtSubLV2ID]').val("");
                getListSubLV2();
            });

            $('select[id*=ddlSubLV2]').change(function () {
            });
        })


        function getListSubLV2() {
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
        function Reports() {
            var term = $('select[id*=ddlSubLV2] option:selected').val();
            var dayreports = $('select[id*=ddlmonth] option:selected').val() + "/01/" + $('select[id*=ddlyear] option:selected').val();
            $.get("/App_Logic/Report/reportsmonth.ashx?term=" + term + "&dayreport=" + dayreports, "", function (result) {
                var columnname = result.daylist;
                var studentdata = result.userdata;
                var htmltable = $(".reportmonth");
                htmltable.html("");
                htmltable.append("<div style=\"overflow-x:auto;\"><div style=\"width:4500px\"><table class=\"table\" id=\"example\">" +
                    genHeaderTable(result.daylist) + genRowTable(studentdata) + "</div></div>");
            });
        }
        function genHeaderTable(ArrayHeader) {
            var htmlHeader = "<tr><td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td><td style=\"width:150px\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" +
                "</td><td style=\"width:300px\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301008") %></td>";
            $.each(ArrayHeader, function (colindex) {
                htmlHeader += "<td style=\"width:100px\">" + ArrayHeader[colindex].LogDate
                    + "<br/> " + ArrayHeader[colindex].LogDayName + "</td>"
            });
            htmlHeader += "<td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>)</td><td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>)</td><td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>)</td>" +
                "<td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>)</td></tr>"
            return htmlHeader;
        }
        function genRowTable(ArrayRow) {
            var htmlRow = "<tbody>";
            $.each(ArrayRow, function (colindex) {
                htmlRow += "<tr><td>" + (colindex + 1) + "</td><td>" + ArrayRow[colindex].sIdentification + "</td>"
                htmlRow += "<td>" + ArrayRow[colindex].name + "</td>";
                var time = ArrayRow[colindex].time;
                $.each(time, function (col2index) {
                    htmlRow += "<td>" + time[col2index].LogTime + "</td>";
                });
                htmlRow += "<td style=\"width:150px\">" + ArrayRow[colindex].LogScanStatus0 + "(" + ArrayRow[colindex].LogScanPercent0 + "%)</td>";
                htmlRow += "<td  style=\"width:150px\">" + ArrayRow[colindex].LogScanStatus3 + "(" + ArrayRow[colindex].LogScanPercent3 + "%)</td>";
                htmlRow += "<td style=\"width:150px\">" + ArrayRow[colindex].LogScanStatus1 + "(" + ArrayRow[colindex].LogScanPercent1 + "%)</td>";
                htmlRow += "<td  style=\"width:150px\">" + ArrayRow[colindex].LogScanStatus9 + "(" + ArrayRow[colindex].LogScanPercent0 + "%)</td></tr>";
            });

            htmlRow += "</tbody>";
            return htmlRow;
        }
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
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %> :</label>
                <div class="col-md-3 col-sm-3">
                    <asp:DropDownList ID="ddlmonth" runat="server" class="form-control col-lg-6"></asp:DropDownList>
                </div>
                <div class="col-md-3 col-sm-3">
                    <asp:DropDownList ID="ddlyear" runat="server" class="form-control col-lg-6"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <input type="button" class="btn btn-primary button-custom" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="Reports();" />
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
            </div>
        </div>
        <div class='row hidden' style="font-weight: bolder; font-size: 40px;">
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
        <div class="row--space">
        </div>
        <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8">
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2">
                <div class="btn btn-success button-custom" id="exportfile">Export File</div>
            </div>
        </div>
        <asp:Literal ID="ltrHeaderReport" runat="server" />
        <div class="row border-bottom">
            <br />
            <div class="col-sm-12 reportmonth">
                <fieldset>
                    <asp:ListView ID="lvReport" runat="server">
                    </asp:ListView>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
