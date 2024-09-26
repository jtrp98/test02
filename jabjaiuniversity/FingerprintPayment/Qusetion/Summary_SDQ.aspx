<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mp.Master" CodeBehind="Summary_SDQ.aspx.cs" Inherits="FingerprintPayment.Qusetion.Summary_SDQ1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />

    <style type="text/css">
        .mTD {
            text-align: left;
        }

        h1 {
            font-size: 39px;
            margin-top: 20px;
            margin-bottom: 35px;
        }


        .header-photo {
            position: relative;
            width: 180px;
            margin: 0 auto 30px;
            z-index: 1;
        }

            .header-photo img {
                max-width: 100%;
                background-color: #fff;
                border: 3px solid #fff;
                border-radius: 300px;
            }


        .header-body {
        }

            .header-body h2 {
                font-size: 36px;
                font-weight: bolder;
                color: #686857;
                margin: 0px 0 0px;
                line-height: 1.2em;
            }

            .header-body h4 {
                font-size: 23px;
                font-weight: bolder;
                color: #686857;
                margin: 5px 0;
                line-height: 1.2em;
            }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="full-card box-content">
        <div class="row" style="text-align: center;">
            <div class="col-md-12">
                <h1 style="font-weight: bolder;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132990") %></h1>
            </div>

            <div class="col-md-12">
                <div class="col-md-7">
                    <table class="table table-bordered">
                        <tbody id="myTable1">
                        </tbody>
                        <tbody id="myTable2">
                        </tbody>
                        <tbody id="myTable3">
                        </tbody>
                    </table>
                </div>

                <div class="col-md-5" style="text-align: center; border: solid 1px #CCCCAA">
                    <div class="header-photo">
                        <img id="imgProfile" />
                    </div>
                    <div class="header-body">
                        <h2 id="studentid"></h2>
                        <h4 id="studentname"></h4>
                        <h4 id="classroom"></h4>
                        <h4 id="studentnumber"></h4>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <button type="button" class="btn btn-danger" id="backHome" style="width: 9%;" onclick="backClick()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00070") %></button>
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div id="modalWaitDialog" class="modal fade" tabindex="-1" role="dialog" data-keyboard="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00106") %></h3>
                </div>
                <div class="modal-body">
                    <div style="text-align: center;">
                        <img src="../Assets/images/wait.gif" style="width: 75px; height: 75px;" />
                    </div>
                </div>
                <div class="modal-footer" style="text-align: center;">
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">

    <script type="text/javascript">

        $(function () {

            var full = window.location.href;
            var half = full.split('?');
            var split = half[1].split('&');
            var sID = split[0];
            var term = split[1];

            $("#modalWaitDialog").modal('show');

            $.get("/Qusetion/Ashx/Summary_SDQ.ashx?" + sID + "&" + term, function (Result) {

                console.log(Result);
                console.log(Result[0].Results);

                $("#imgProfile").attr("src", Result[0].StudentPicture);
                $("#studentid").text(Result[0].StudentID);
                $("#studentname").text(Result[0].Name);
                $("#classroom").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>" + " " + Result[0].ClassRoom);
                $("#studentnumber").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %>" + " " + Result[0].StudentNumber);


                buildTable(Result[0].Results);

                $("#modalWaitDialog").modal('hide');

            });

        });

        function buildTable(data) {
            var myTable1 = document.getElementById(`myTable1`);
            var myTable2 = document.getElementById(`myTable2`);
            var myTable3 = document.getElementById(`myTable3`);

            var ResultNum = 0;
            var ResultString = 0;

            for (var i = 0; i < 4; i++) {

                ResultNum += data[i].Score;
                ResultString += data[i].Score;

                var row = `<tr>
<td class="mTD">${data[i].GroupName}</td>
<td style="width: 10%;">${data[i].Score}</td>
<td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304049") %></td>
<td style="width: 13%;">${data[i].Result}</td>
</tr>`
                myTable1.innerHTML += row;
            }


            var row2 = `<tr>
<td class="mTD"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133121") %></td>
<td style="width: 10%;">${ResultNum}</td>
<td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304049") %></td>
<td style="width: 13%;">${sCal(ResultString)}</td>
</tr>`
            myTable2.innerHTML = row2;


            for (var i = 4; i < 6; i++) {
                var row3 = `<tr>
<td class="mTD">${data[i].GroupName}</td>
<td style="width: 10%;">${data[i].Score}</td>
<td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304049") %></td>
<td style="width: 13%;">${data[i].Result}</td>
</tr>`
                myTable3.innerHTML += row3;
            }
        }

        function sCal(v) {
            var txt = "";

            if (v <= 16) txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
            else if (v == 17 && v == 18) txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %>"
            else txt = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %>";

            return txt;
        }

        function cal(group, val) {
            var ans = "";

            switch (group) {
                case 1:
                    if (val <= 5) ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (val == 6) ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %>";
                    else ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %>";
                    break;
                case 2:
                    if (val <= 4) ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (val == 5) ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %>";
                    else ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %>";
                    break;
                case 3:
                    if (val <= 5) ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (val == 6) ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %>";
                    else ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %>";
                    break;
                case 4:
                    if (val <= 3) ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (val == 4) ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %>";
                    else ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %>";
                    break;
                case 5:
                    if (val <= 2) ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %>";
                    else if (val == 3) ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %>";
                    else ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    break;
                case 6:
                    if (val == 0) ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (val <= 2) ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %>";
                    else ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %>";
                    break;
            }
            return ans;
        }

        function backClick() {
            var link = "Index_SDQ.aspx";
            window.location = link;
        }

    </script>

</asp:Content>
