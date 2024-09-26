<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="FingerprintPayment.AssetManagement.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />

    <link rel="stylesheet" href="/styles/style-asset.css" />

    <style type="text/css">
        .reportList #tableData .btn-group a {
            padding: 5px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="panel with-nav-tabs panel-default">
            <div class="panel-body">
                <div class="fade in" id="divContent"></div>
            </div>
        </div>
    </div>

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">

    <script type='text/javascript' src="/AssetManagement/Scripts/init-function.js"></script>

    <!-- DataTables -->
    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>

    <script type='text/javascript'>

        $(function () {

            // initial first tab content
            $('#divContent').html('<div class="loader"></div>');

            var report = '<%=Request.QueryString["r"]%>';
            var reportPage = '';
            switch (report) {
                case 'r01': reportPage = 'Report01.aspx'; break;
                case 'r02': reportPage = 'Report02.aspx'; break;
                default: reportPage = 'Report01.aspx'; break;
            }

            $.get(reportPage, { "id": 0 },
                function (data) {
                    $('#divContent').html(data);
                }
            );

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
