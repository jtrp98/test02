<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="Reportmobile02.aspx.cs" Inherits="FingerprintPayment.Report.Reportmobile02" %>

<%@ Register Src="~/UserControls/TeacherAutocomplete.ascx" TagPrefix="uc1" TagName="TeacherAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <%--  <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>--%>
    <%--    <script src="/app/Reports/Come2school/ReportCome2SchoolteacherJS.js" type="text/javascript"></script>--%>
    <style>
        td#headder {
            border-bottom: 1px solid #000;
            background-color: #FFF !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">

    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>

    <script>
        $(function () {

            $('.datepicker').datetimepicker({
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
                debug: false,
                //defaultDate: "<%=DateTime.Now.ToString("dd/MM/yyyy") %>",
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

            if (jQuery.validator) {

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",

                });

                $("#aspnetForm").validate({  // initialize the plugin
                    errorPlacement: function (error, element) {
                        var _class = element.attr('class');

                        if (_class.includes('--req-append-last')) {
                            error.insertAfter(element.parent());
                        }
                        else
                            error.insertAfter(element);
                    }

                });
            }

            $("#exportfile").click(function () {

                var id = TAC.GetUserID();
                if (id != "") {
                    var dt = new Date();
                    $('#example').tableExport({ type: 'excel', escape: 'false', sheets: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133166") %>', fileName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133166") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>อง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405014") %>_" + TAC.GetUserName() + "_" + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
                } else {
                    var dt = new Date();
                    $('#example').tableExport({ type: 'excel', escape: 'false', sheets: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133166") %>', fileName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133166") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>อง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405014") %>_" + $("input[id*=hdfschoolname]").val() + "_" + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
                }
            });

        });

        function sortday() {
            var start = $("#txtstart").val();
            var end = $("#txtend").val();
            var sort = "&day=" + moment(start, 'DD/MM/YYYY').add(-543, 'years').format('DD/MM/YYYY')
                + "&end=" + moment(end, 'DD/MM/YYYY').add(-543, 'years').format('DD/MM/YYYY');
            return sort;
        }
        function sortuser() {
            //var userid = $("#txtid").val();
            var sort = "&userid=" + TAC.GetUserID();
            return sort;
        }

        function sortstatus() {
            var status = $("select[id*=status] option:selected").val();
            var sort = "&status=" + status;
            return sort;
        }

        function Reports1() {
            //$("#backpage").addClass("hidden");
            //$("body").mLoading();

            if ($('#aspnetForm').valid() == false) {
                //e.preventDefault();
                //e.stopPropagation();
                return false;
            }

            var dt = new Date();
            var Header = $("#myHeader");
            var HtmlTable = $("#myTable");
            Header.html("");
            HtmlTable.html("");
            var day = "";
            if ($("#txtstart").val() !== "" && $("#txtend").val() !== "") {
                day = $("#txtstart").val() + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105040") %> " + $("#txtend").val();
            }
            else if ($("#txtstart").val()) {
                day = $("#txtstart").val();
            }
            else {
                day = dt.toLocaleDateString();
            }

            var id = TAC.GetUserID();
            if (id !== "") {

                Header.append("<tr><th style='text-align: center;font-size:26px;border-width:0px;'id='school' colspan=12>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
                    + `<tr><th colspan=12 style='text-align: center;font-size:24px;border-width:0px;'id='headdatail'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133166") %></th></tr> `
                    + `<tr><th colspan=12 style='text-align: center;font-size:24px;border-width:0px;'id='dayfall'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133167") %> ` + day + `</th></tr>"`
                    + `<tr><th colspan=12 style='text-align: right;font-size:20px;border-width:0px;'id='dayshort'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601088") %>&nbsp;` + dt.toLocaleDateString()
                    + `<tr><th colspan=12 style='text-align: right;font-size:20px;border-width:0px;'id='timetoday'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
                    + `<tr><th  style="text-align: right; border-width:0px;"colspan=12><br></th>`);

                HtmlTable.append("<tr id='headder' style='font-weight: bold;text-align: center; '>"
                    + "<td id='headder' rowspan='2'style='width:5%;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><td  id='headder' style='width:10%'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %><td  id='headder' style='width:10%'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %>"
                    + "<td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %><td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105036") %><td id='headder'colspan='5'style='width:15%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td><td id='headder'rowspan='2'>เวลาเลิกงาน<td id='headder'colspan='5'style='width:15%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td>");

                HtmlTable.append("<tr id='headder' style='font-weight: bold; text-align: center; >"
                    + "<td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %><td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %><td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %><td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133171") %><td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206449") %>"
                    + "<td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133172") %><td id='headder'style='width:5%'>ก่อนเวลา<td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %><td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133171") %><td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206449") %>");

                $.get("/App_Logic/Report/ReportCome2Schoolteacher.ashx?mode=reportscome2school02teacher&" + sortday() + sortuser() + sortstatus(), {}, function (Obj) {
                    var sumallstudent4level = 0;
                    var sumallstatus_0 = 0, sumallstatus_1 = 0, sumallstatus_2 = 0, sumallstatus_3 = 0, sumallstatus_4 = 0;
                    var sumallstatusout_0 = 0, sumallstatusout_1 = 0, sumallstatusout_2 = 0, sumallstatusout_3 = 0, sumallstatusout_4 = 0;
                    $.each(Obj, function (indexlevel) {
                        var HtmlRow = "";
                        HtmlRow += "<tr><td class='text-center' >" + (indexlevel + 1)
                            + "<td class='text-center'>" + Obj[indexlevel].day
                            + "<td class='text-center'>" + Obj[indexlevel].employesstype
                            + "<td class='text-center'>" + Obj[indexlevel].employessname
                            + "<td class='text-center'>" + (Obj[indexlevel].statusIn_9 === 0 ? Obj[indexlevel].timeinscan : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %>")
                            + "<td class='text-center'>" + (Obj[indexlevel].statusIn_0 == "1" ? "X" : "")
                            + "<td class='text-center'>" + (Obj[indexlevel].statusIn_1 == "1" ? "X" : "")
                            + "<td class='text-center'>" + (Obj[indexlevel].statusIn_2 == "1" ? "X" : "")
                            + "<td class='text-center'>" + (Obj[indexlevel].statusIn_3 == "1" ? "X" : "")
                            + "<td class='text-center'>" + (Obj[indexlevel].statusIn_4 == "1" ? "X" : "")
                            + "<td class='text-center'>" + (Obj[indexlevel].statusIn_9 === 0 ? Obj[indexlevel].timeoutscan : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %>")
                            + "<td class='text-center'>" + (Obj[indexlevel].statusOut_0 == "1" ? "X" : "")
                            + "<td class='text-center'>" + (Obj[indexlevel].statusOut_1 == "1" ? "X" : "")
                            + "<td class='text-center'>" + (Obj[indexlevel].statusOut_2 == "1" && Obj[indexlevel].statusIn_9 === 0 ? "X" : ""
                            + "<td class='text-center'>" + (Obj[indexlevel].statusOut_3 == "1" ? "X" : "")
                            + "<td class='text-center'>" + (Obj[indexlevel].statusOut_4 == "1" ? "X" : "")

                            );

                        sumallstudent4level += 1;
                        sumallstatus_0 += Obj[indexlevel].statusIn_0;
                        sumallstatus_1 += Obj[indexlevel].statusIn_1;
                        sumallstatus_2 += Obj[indexlevel].statusIn_2;
                        sumallstatus_3 += Obj[indexlevel].statusIn_3;
                        sumallstatus_4 += Obj[indexlevel].statusIn_4;
                        sumallstatusout_0 += Obj[indexlevel].statusOut_0;
                        sumallstatusout_1 += Obj[indexlevel].statusOut_1;
                        sumallstatusout_2 += (Obj[indexlevel].statusIn_9 === 0 ? Obj[indexlevel].statusOut_2 : 0);
                        sumallstatusout_3 += Obj[indexlevel].statusOut_3;
                        sumallstatusout_4 += Obj[indexlevel].statusOut_4;
                        HtmlTable.append(HtmlRow);
                    })
                    HtmlTable.append("<tr><td colspan=4 class='right'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304021") %><td class='text-center'>" + sumallstudent4level
                        + "<td class='text-center'>" + sumallstatus_0 + "<td class='text-center'>" + sumallstatus_1
                        + "<td class='text-center'>" + sumallstatus_2
                        + "<td class='text-center'>" + sumallstatus_3
                        + "<td class='text-center'>" + sumallstatus_4
                        + "<td class='text-center'>" + sumallstudent4level
                        + "<td class='text-center'>" + sumallstatusout_0 + "<td class='text-center'>" + sumallstatusout_1
                        + "<td class='text-center'>" + sumallstatusout_2
                        + "<td class='text-center'>" + sumallstatusout_3
                        + "<td class='text-center'>" + sumallstatusout_4
                    );
                    /*$("body").mLoading('hide');*/
                });
            } else {


                Header.append("<tr><th style='text-align: center;font-size:26px;border-width:0px;'id='school' colspan=6>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
                    + `<tr><th colspan=6 style='text-align: center;font-size:24px;border-width:0px;'id='headdatail'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133166") %></th></tr> `
                    + `<tr><th colspan=6 style='text-align: center;font-size:24px;border-width:0px;'id='dayfall'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133167") %> ` + day + `</th></tr>"`
                    + `<tr><th colspan=6 style='text-align: right;font-size:20px;border-width:0px;'id='dayshort'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601088") %>&nbsp;` + dt.toLocaleDateString()
                    + `<tr><th colspan=6 style='text-align: right;font-size:20px;border-width:0px;'id='timetoday'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
                    + `<tr><th  style="text-align: right; border-width:0px;"colspan=7><br></th>`);

                HtmlTable.append("<tr id='headder' style='font-weight: bold; text-align: center;'><td id='headder' rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>"
                    + "<td id='headder' rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133169") %><td id='headder'colspan='3'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>");
                HtmlTable.append("<tr id='headder' style='font-weight: bold; text-align: center; '>"
                    + "<td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %><td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %><td id='headder'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>");

                $.get("/App_Logic/Report/ReportCome2Schoolteacher.ashx?mode=reportscome2school01teacher" + sortday() + sortuser() + sortstatus(), {}, function (Obj) {
                    var sumallstudent4level = 0;
                    var sumallstatus_0 = 0, sumallstatus_1 = 0, sumallstatus_2 = 0;
                    $.each(Obj, function (index) {
                        HtmlTable.append("<tr><td class='text-center'>" + (index + 1) + "<td style='color:royalblue;cursor: pointer;' onclick='Reports2(" + '"' + Obj[index].day + '"' + ")' class='text-center'>" + Obj[index].day + "<td class='text-center'>" + Obj[index].employessnumber + "<td class='text-center'>" + Obj[index].status_0 + "<td class='text-center'>" + Obj[index].status_1 + "<td class='text-center'>" + Obj[index].status_2);
                        sumallstudent4level += Obj[index].employessnumber;
                        sumallstatus_0 += Obj[index].status_0;
                        sumallstatus_1 += Obj[index].status_1;
                        sumallstatus_2 += Obj[index].status_2;
                    });
                    HtmlTable.append("<tr><td><td class='right'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304021") %><td class='text-center'>" + sumallstudent4level
                        + "<td class='text-center'>" + sumallstatus_0 + "<td class='text-center'>" + sumallstatus_1
                        + "<td class='text-center'>" + sumallstatus_2);
                    //$("body").mLoading('hide');
                });
            }

        }

        function Reports2(day) {
            $("#backpage").removeClass("hidden");
            //$("body").mLoading();
            var dt = new Date();
            var Header = $("#myHeader");
            var HtmlTable = $("#myTable");
            Header.html("");
            HtmlTable.html("");
            Header.append("<tr><th style='text-align: center;font-size:26px;border-width:0px;'id='school' colspan=11>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
                + `<tr><th colspan=11 style='text-align: center;font-size:24px;border-width:0px;'id='headdatail'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133166") %></th></tr> `
                + `<tr><th colspan=11 style='text-align: center;font-size:24px;border-width:0px;'id='dayfall'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133167") %> ` + day + `</th></tr>"`
                + `<tr><th colspan=11 style='text-align: right;font-size:20px;border-width:0px;'id='dayshort'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601088") %>&nbsp;` + dt.toLocaleDateString()
                + `<tr><th colspan=11 style='text-align: right;font-size:20px;border-width:0px;'id='timetoday'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
                + `<tr><th  style="text-align: right; border-width:0px;"colspan=7><br></th>`);

            HtmlTable.append("<tr id='headder' style='font-weight: bold; text-align: center;'>"
                + "<td id= 'headder' rowspan= '2' style= 'width:5%;' > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %> <td  id= 'headder' style= 'width:10%'rowspan= '2' > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %>"
                + "<td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %><td id='headder'rowspan='2'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105036") %><td id='headder'colspan='5'style='width:15%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td><td id='headder'rowspan='2'>เวลาเลิกงาน<td id='headder'colspan='5'style='width:15%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td>");
            HtmlTable.append("<tr id='headder' style='font-weight: bold; text-align: center;'>"
                + "<td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %><td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %><td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %><td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133171") %><td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206449") %>"
                + "<td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133172") %><td id='headder'style='width:5%'>ก่อนเวลา<td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %><td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133171") %><td id='headder'style='width:5%'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206449") %>");

            $.get("/App_Logic/Report/ReportCome2Schoolteacher.ashx?mode=reportscome2school02teacher&day=" + day + sortuser() + sortstatus(), {}, function (Obj) {
                reports_data = Obj;

                var sumallstudent4level = 0;
                var sumallstatus_0 = 0, sumallstatus_1 = 0, sumallstatus_2 = 0, sumallstatus_3 = 0, sumallstatus_4 = 0;
                var sumallstatusout_0 = 0, sumallstatusout_1 = 0, sumallstatusout_2 = 0, sumallstatusout_3 = 0, sumallstatusout_4 = 0;
                $.each(Obj, function (indexlevel) {
                    var HtmlRow = "";
                    HtmlRow += "<tr><td class='text-center' >" + (indexlevel + 1)
                        + "<td class='text-center'>" + Obj[indexlevel].employesstype
                        + "<td class='text-center'>" + Obj[indexlevel].employessname
                        + "<td class='text-center'>" + (Obj[indexlevel].statusIn_9 === 0 ? Obj[indexlevel].timeinscan : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %>")
                        + "<td class='text-center'>" + (Obj[indexlevel].statusIn_0 == "1" ? "X" : "")
                        + "<td class='text-center'>" + (Obj[indexlevel].statusIn_1 == "1" ? "X" : "")
                        + "<td class='text-center'>" + (Obj[indexlevel].statusIn_2 == "1" ? "X" : "")
                        + "<td class='text-center'>" + (Obj[indexlevel].statusIn_3 == "1" ? "X" : "")
                        + "<td class='text-center'>" + (Obj[indexlevel].statusIn_4 == "1" ? "X" : "")
                        + "<td class='text-center'>" + (Obj[indexlevel].statusIn_9 === 0 ? Obj[indexlevel].timeoutscan : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %>")
                        + "<td class='text-center'>" + (Obj[indexlevel].statusOut_0 == "1" ? "X" : "")
                        + "<td class='text-center'>" + (Obj[indexlevel].statusOut_1 == "1" ? "X" : "")
                        + "<td class='text-center'>" + (Obj[indexlevel].statusOut_2 == "1" && Obj[indexlevel].statusIn_9 === 0 ? "X" : ""
                            + "<td class='text-center'>" + (Obj[indexlevel].statusOut_3 == "1" ? "X" : "")
                            + "<td class='text-center'>" + (Obj[indexlevel].statusOut_4 == "1" ? "X" : ""));

                    sumallstudent4level += 1;
                    sumallstatus_0 += Obj[indexlevel].statusIn_0;
                    sumallstatus_1 += Obj[indexlevel].statusIn_1;
                    sumallstatus_2 += Obj[indexlevel].statusIn_2;
                    sumallstatus_3 += Obj[indexlevel].statusIn_3;
                    sumallstatus_4 += Obj[indexlevel].statusIn_4;
                    sumallstatusout_0 += Obj[indexlevel].statusOut_0;
                    sumallstatusout_1 += Obj[indexlevel].statusOut_1;
                    sumallstatusout_2 += Obj[indexlevel].statusOut_2;
                    sumallstatusout_3 += Obj[indexlevel].statusOut_3;
                    sumallstatusout_4 += Obj[indexlevel].statusOut_4;
                    HtmlTable.append(HtmlRow);
                })
                HtmlTable.append("<tr><td><td><td class='right'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304021") %><td class='text-center'>" + sumallstudent4level
                    + "<td class='text-center'>" + sumallstatus_0 + "<td class='text-center'>" + sumallstatus_1
                    + "<td class='text-center'>" + sumallstatus_2
                    + "<td class='text-center'>" + sumallstatus_3
                    + "<td class='text-center'>" + sumallstatus_4
                    + "<td class='text-center'>" + sumallstudent4level
                    + "<td class='text-center'>" + sumallstatusout_0 + "<td class='text-center'>" + sumallstatusout_1
                    + "<td class='text-center'>" + sumallstatusout_2
                    + "<td class='text-center'>" + sumallstatusout_3
                    + "<td class='text-center'>" + sumallstatusout_4
                );
                //$("body").mLoading('hide');
            });
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133173") %>  
            </p>
        </div>
    </div>

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Release" />
        <asp:HiddenField ID="hdfschoolname" runat="server" />

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
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133170") %></label>
                            <div class="col-md-3 ">
                                <uc1:TeacherAutocomplete runat="server" ID="TeacherAutocomplete" />
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left "></label>
                            <div class="col-md-3 ">
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                            <div class="col-md-3 ">
                                <div class="form-group has-successx">
                                    <input type="text" name="txtstart" id="txtstart" class="form-control datepicker" required />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                            <div class="col-md-3 ">
                                <div class="form-group has-successx">
                                    <input type="text" name="txtend" id="txtend" class="form-control datepicker" required />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="status" runat="server" class="selectpicker" data-style="select-with-transition" data-width="100%" data-size="7">
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" Selected="True" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>" Value="0" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>" Value="1" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>" Value="3" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left "></label>
                            <div class="col-md-3 ">
                            </div>
                            <div class="col-md-2"></div>
                        </div>


                        <div class="row">
                            <div class="col-md-12 text-center">
                                <br />
                                <button type="button" onclick="Reports1();" class="btn btn-fill btn-info">
                                    <span class="material-icons">search</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-right">

                                <button type="button" class="btn btn-fill btn-success " id="exportfile">Export File</button>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-warning card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102145") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row ">
                            <br />
                            <div class="col-sm-12">
                                <fieldset>
                                    <%-- <asp:ListView ID="lvReport" runat="server">
                                    </asp:ListView>--%>
                                    <table id="example" class="table-hover dataTable  table-show-result"
                                        cellspacing="0" width="100%">
                                        <thead id="myHeader" hidden></thead>
                                        <tbody id="myTable"></tbody>
                                    </table>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>

    <div class="report-container">

        <%-- <div class="row--space">
        </div>
        <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2">
                <div class="btn btn-default button-custom hidden" id="backpage"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></div>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8">
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2">
                <div class="btn btn-success button-custom" id="exportfile">Export File</div>
            </div>
        </div>--%>
        <%--     <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %>
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8">
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2">
            </div>
        </div>--%>
        <%-- <asp:Literal ID="ltrHeaderReport" runat="server" />--%>
    </div>
</asp:Content>
