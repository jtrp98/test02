<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="SummaryDetailPrint.aspx.cs" Inherits="FingerprintPayment.VisitHousePage.Web.Report.SummaryDetailPrint" %>



<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <title><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133544") %></title>
    <link rel="stylesheet" href="/styles/style-visithouse-print.css" />
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" >
    <link href="/Content/Material/layout.css?v=<%=DateTime.Now.Ticks%>" rel="stylesheet" />
    <script src="/Content/Material/assets/js/core/jquery.min.js"></script>
    <style >
        .print-btn {
            position: fixed;
            top: 50%;
            right: 10px;
            z-index: 4;
            border: 1px solid black;
            padding: 10px 15px;
            color: #fff !important;
            background-color: #2196F3 !important;
            text-align: center;
            cursor: pointer;
        }

        .photo {
            padding-top: 2px;
        }

        table {
            margin: 0 0 4px 0px;
        }

        p {
            margin: 0 0 6px;
        }

        table th, table td {
            padding: 5px 2px 0 5px;
        }

        .table-bordered,
        .table-bordered td,
        .table-bordered th {
            border: 1px solid #000 !important;
        }

        table.table-bordered {
            margin-bottom: 1rem;
        }

            table.table-bordered th {
                font-weight: bold;
            }

        p.font-weight-bold {
            font-weight: 600 !important;
        }

        @media print {
            .print-btn {
                display: none !important;
            }

        }
    </style>
    <script>
        $(function () {
            $("#result-wrapper").html('');
            $.ajax(
                {
                    type: "POST",
                    url: "/VisitHousePage/Web/Report/SummaryDetail.aspx/LoadData",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        'term': '<%=Request.QueryString["term"] %>',
                        'level1': '<%=Request.QueryString["level1"] %>',
                        'level2': '<%=Request.QueryString["level2"] %>',
                    }),
                    success: function (response) {
                        $("#result-wrapper").html(response.d.html);
                        $('body > div:not(.ext1)').hide();

                        if ('<%=Request.QueryString["level1"] %>' == '') {
                            $('.--all').show();
                        }
                        else {
                            $('.--all').hide();
                        }
                    }
                });

        });
    </script>
</head>
<body>
    <div class="print-btn ext1" onclick="window.print();">
        <span class="glyphicon glyphicon-print"></span>
        <br>
        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>      
    </div>
    <div id="result-wrapper" class="book ext1">
     
    </div>
</body>
</html>
