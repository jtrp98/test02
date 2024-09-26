<%@ Page Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="Behaviorsreportsdetail-2.aspx.cs"
    Inherits="FingerprintPayment.Report.Behaviorsreportsdetail_2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <script src="ScriptReport.js" type="text/javascript"></script>
    <script src="../Scripts/FileSaver.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <link href="../Content/jquery-confirm.css" rel="stylesheet" />
    <script src="../Scripts/jquery-confirm.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-dateformat.js" type="text/javascript"></script>
    <script type="text/javascript">
        function export_excel() {
            $("body").mLoading('show');
            var json = JSON.stringify(
                {
                    "student_id": "<%= (String)Request.QueryString["userid"].ToString() %>",
                    "term_id":  "<%= (String)Request.QueryString["termid"].ToString() %>",
                    "year_Id":  "<%= (String)Request.QueryString["yearid"].ToString() %>",
                });
            var xhr = new XMLHttpRequest();
            xhr.open("POST", "/Report/Handles/Behaviorsreportsdetail2_exportHandler.ashx", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                $("body").mLoading('hide');
                saveAs(xhr.response, '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303023") %> ' + $("input[id*=hdfschoolname]").val() + '.xls');
            };
            xhr.onerror = function () {
                window.location.reload();
            };
            xhr.send(json);
        }

        function CancelScore(BehaviorsrId) {
            $.confirm({
                title: '<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802056") %></h1>',
                content: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133145") %></h3>',
                buttons: {
                    '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>': {
                        text: '<h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %></h4>',
                        //btnClass: 'btn-danger',
                        keys: ['enter'],
                        action: function () {
                            $("body").mLoading();
                            PageMethods.CancelScore(BehaviorsrId, function (response) {
                                let student_id =  "<%= (String)Request.QueryString["userid"].ToString() %>";
                                let term_id = "<%= (String)Request.QueryString["termid"].ToString() %>";
                                let year_Id ="<%= (String)Request.QueryString["yearid"].ToString() %>";
                                PageMethods.GetData(student_id, term_id, year_Id, function (data) {
                                    $("#myTable tr").remove();
                                    $.each(data.Details, function (e, s) {
                                        var date = new Date(s.dateTime);
                                        var cancleDate = new Date(s.cancleDate);
                                        let Html = `<tr>
                                            <td class="center">`+ (e + 1) + `</td>
                                            <td>`+ $.format.date(date, "dd/MM/yyyy") + `</td>
                                            <td>` + $.format.date(date, "HH:mm:ss") + `</td>
                                            <td>`+ s.Name + `</td>
                                            <td class="center">`+ s.Type + `</td>
                                            <td class="center">`+ s.Score + `</td>
                                            <td class="center">`+ s.residualScore + `</td>
                                            <td>`+ s.teacherName + `</td>
                                            <td class="center">`+ s.Note + (s.Status === "" ? "" : "\"<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>\"") + `</td>`;
                                        if (s.Status === "") {
                                            Html += `<td ><div class='btn btn-danger' onclick="CancelScore('` + s.ID + `');"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602011") %></div></td >`;
                                        } else {
                                            if (s.Status == 'holiday') {
                                                Html += `<td ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133146") %> ${$.format.date(cancleDate, "dd/MM/yyyy HH:mm")} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133150") %> ${s.cancleBy}</td>`;
                                            }
                                            else {
                                                Html += `<td ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> ${$.format.date(cancleDate, "dd/MM/yyyy HH:mm")} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133150") %> ${s.cancleBy}</td >`;
                                            }
                                        }
                                        Html += `</tr >`
                                        $("#myTable").append(Html)
                                    });
                                    $("body").mLoading('hide');
                                });
                            });
                        }
                    },
                    '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>': {
                        text: '<h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></h4>',
                        btnClass: 'btn-danger',
                        keys: ['ESC'],
                        action: function () {
                        }
                    },
                }
            });
        }

        //function formatDate(date) {
        //    var hours = date.getHours();
        //    var minutes = date.getMinutes();
        //    var ampm = hours >= 12 ? 'pm' : 'am';
        //    hours = hours % 12;
        //    hours = hours ? hours : 12; // the hour '0' should be '12'
        //    minutes = minutes < 10 ? '0' + minutes : minutes;
        //    var strTime = hours + ':' + minutes + ' ' + ampm;
        //    return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear() + "  " + strTime;
        //}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />
    <asp:HiddenField ID="hdfschoolname" runat="server" />
    <% var models = GetData((String)Request.QueryString["userid"].ToString(), (String)Request.QueryString["termid"].ToString(), (String)Request.QueryString["yearid"].ToString()); %>
    <div class="full-card box-content">
        <div class="row">
            <div class="col-lg-2"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></b> </div>
            <div class="col-lg-8"><%= models.student_Code %></div>
        </div>
        <div class="row">
            <div class="col-lg-2"><b>ชื่อ-นามสุกล</b></div>
            <div class="col-lg-8"><%= models.student_name %></div>
            <div class="col-lg-2 col-md-2 col-sm-2 btn btn-success button-custom" id="exportfile" onclick="export_excel()">
                Export File
            </div>
        </div>
        <div class="row">
            <div class="col-lg-2"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></b> </div>
            <div class="col-lg-8"><%= models.roomName %></div>

        </div>
        <div class="row border-bottom">
            <div class="col-sm-12">
                <fieldset>
                    <asp:ListView ID="lvReport" runat="server">
                    </asp:ListView>
                    <table id="example" class="table table-condensed table-bordered table-show-result"
                        cellspacing="0" width="100%">
                        <thead>
                            <tr style="background-color: rgb(51, 122, 183); color: #fff;">
                                <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></th>
                                <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %></th>
                                <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133147") %></th>
                                <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></th>
                                <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133148") %></th>
                                <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303024") %></th>
                                <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133149") %></th>
                                <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></th>
                                <th class="center"></th>
                            </tr>
                        </thead>
                        <tbody id="myTable" style="font-size: 18px;">
                            <% 

                                int Index = 1;
                                if (models.Details != null)
                                {
                                    foreach (var data in models.Details.OrderByDescending(o => o.ID))
                                    {
                                        
                                        %>
                            <tr>
                                <td class="center"><%= Index++ %></td>
                                <td><%= data.dateTime.Value.ToString("dd/MM/yy" ,new System.Globalization.CultureInfo("th-th")) %></td>
                                <td><%= data.dateTime.Value.ToString("HH:mm:ss" ,new System.Globalization.CultureInfo("th-th")) %></td>
                                <td><%= data.Name %></td>
                                <td class="center"><%= data.Type %></td>
                                <td class="center"><%= data.Score %></td>
                                <%--<td class="center"><%= !string.IsNullOrEmpty(data.Status) ?  (oldresidual ?? data.residualScore) : data.residualScore %></td>--%>
                                <td class="center"><%= data.residualScore %></td>
                                <td><%= data.teacherName %></td>
                                <td class="center"><%= data.Note + (string.IsNullOrEmpty(data.Status) ? "":"\"<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>\"" ) %></td>
                                <td><% if (string.IsNullOrEmpty(data.Status))
                                        {%>
                                    <div class='btn btn-danger' onclick="CancelScore(<%= data.ID %>);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602011") %></div>
                                    <%} else if (data.Status == "holiday"){ %>
                                        <%= (" <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133146") %> " +data.cancleDate.Value.ToString("dd/MM/yyyy <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> HH:mm") +" <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133150") %> "+data.cancleBy) %>                                    
                                    <%} else if (data.Status == "delete")
                                    { %>
                                        <%= (" <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> " +data.cancleDate.Value.ToString("dd/MM/yyyy <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> HH:mm") +" <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133150") %> "+data.cancleBy) %>
                                    <% }
%>
                                    </td>
                            </tr>
                            <%                                        
                                                                          
                                    }

                                }
                            %>
                        </tbody>
                    </table>
                </fieldset>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
