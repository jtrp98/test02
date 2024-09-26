<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Summary_EQ.aspx.cs" MasterPageFile="~/mp.Master" Inherits="FingerprintPayment.Qusetion.Summary_EQ" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />

    <style type="text/css">
        th {
            text-align: center;
        }

        .TopicLarge {
            text-align: left;
            font-weight: bold;
        }

        .Content {
            font-weight: bold;
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
                        <thead>
                            <tr>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132991") %></th>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132992") %></th>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306015") %></th>
                            </tr>
                        </thead>
                        <tbody id="myTable">
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

            $.get("/Qusetion/Ashx/Summary_EQ.ashx?" + sID + "&" + term, function (Result) {

                console.log(Result);
                console.log(Result[0].level1);

                $("#imgProfile").attr("src", Result[0].StudentPicture);
                $("#studentid").text(Result[0].StudentID);
                $("#studentname").text(Result[0].Name);
                $("#classroom").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>" + " " + Result[0].ClassRoom);
                $("#studentnumber").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %>" + " " + Result[0].StudentNumber);

                buildTable(Result[0].level1);

                $("#modalWaitDialog").modal('hide');

            });
        });

        function buildTable(data) {

            var myTable = document.getElementById('myTable');

            var Result = 0;

            for (var i = 0; i < data.length; i++) {

                Result += data[i].lScore;

                var row = `<tr>
<td class="TopicLarge">${NameLarge(data[i].lGroup)}</td>
<td class="Content">${data[i].lScore}</td>
<td class="Content">${ScoreLarge(data[i].lGroup, data[i].lScore)}</td>
</tr>`
                myTable.innerHTML += row;

                var level2 = data[i].level2;
                for (var j = 0; j < level2.length; j++) {

                    var row = `<tr>
<td>${NameSmall(level2[j].sGroup)}</td>
<td>${level2[j].sScore}</td>
<td>${ScoreSmall(level2[j].sGroup, level2[j].sScore)}</td>
</tr>`
                    myTable.innerHTML += row;
                }

            }

            var row = `<tr>
<td class="Content"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %> EQ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></td>
<td class="Content">${Result}</td>
<td class="Content">${ScoreResult(Result)}</td>
</tr>`
            myTable.innerHTML += row;

        }

        function NameLarge(group) {
            var text = "";
            switch (group) {
                case 1:
                    text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132993") %> :";
                    break;
                case 2:
                    text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132997") %> :";
                    break;
                case 3:
                    text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132982") %> :";
                    break;
            }
            return text;
        }

        function ScoreLarge(group, score) {
            var text = "";
            switch (group) {
                case 1:
                    if (score < 48)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                    else if (score >= 48 || score <= 58)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (score > 58)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                    break;
                case 2:
                    if (score < 45)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                    else if (score >= 45 || score <= 57)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (score > 57)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                    break;
                case 3:
                    if (score < 40)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                    else if (score >= 40 || score <= 55)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (score > 55)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                    break;
            }
            return text;
        }

        function NameSmall(group) {
            var text = "";
            switch (group) {
                case 1:
                    text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133141") %>";
                    break;
                case 2:
                    text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306032") %>";
                    break;
                case 3:
                    text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306033") %>";
                    break;

                case 4:
                    text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306035") %>";
                    break;
                case 5:
                    text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306036") %>";
                    break;
                case 6:
                    text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133142") %>";
                    break;

                case 7:
                    text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132979") %>";
                    break;
                case 8:
                    text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132981") %>";
                    break;
                case 9:
                    text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306041") %>";
                    break;
            }
            return text;
        }

        function ScoreSmall(group, score) {
            var text = "";
            switch (group) {
                case 1:
                    if (score < 13)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                    else if (score >= 13 || score <= 17)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (score > 17)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                    break;
                case 2:
                    if (score < 16)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                    else if (score >= 16 || score <= 20)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (score > 20)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                    break;
                case 3:
                    if (score < 16)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                    else if (score >= 16 || score <= 22)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (score > 22)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                    break;
                case 4:
                    if (score < 14)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                    else if (score >= 14 || score <= 20)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (score > 20)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                    break;
                case 5:
                    if (score < 13)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                    else if (score >= 13 || score <= 19)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (score > 19)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                    break;
                case 6:
                    if (score < 14)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                    else if (score >= 14 || score <= 20)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (score > 20)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                    break;
                case 7:
                    if (score < 9)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                    else if (score >= 9 || score <= 13)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (score > 13)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                    break;
                case 8:
                    if (score < 16)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                    else if (score >= 16 || score <= 22)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (score > 22)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                    break;
                case 9:
                    if (score < 15)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                    else if (score >= 15 || score <= 21)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                    else if (score > 21)
                        text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                    break;
            }
            return text;
        }

        function ScoreResult(score) {
            var text = "";

            if (score < 140)
                text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
            else if (score >= 140 || score <= 170)
                text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
            else if (score > 170)
                text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";

            return text;
        }

        function backClick() {
            var link = "Index_EQ.aspx";
            window.location = link;
        }

    </script>


</asp:Content>
