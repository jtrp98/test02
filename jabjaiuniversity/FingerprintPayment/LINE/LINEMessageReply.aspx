<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LINEMessageReply.aspx.cs" Inherits="FingerprintPayment.LINE.LINEMessageReply" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>LINE Message Reply - School Bright</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <style>
        .truncate {
            width: 460px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .detail p {
            margin-bottom: 0.3rem;
        }

            .detail p .fa {
                margin-right: 5px;
            }

        .table tr td p.result {
            position: absolute;
            font-size: 12px;
            color: #aaa;
            margin-top: -14px;
            margin-left: -10px;
        }
    </style>
</head>
<body>
    <div class="container">
        <table class="table table-hover">
            <thead>
                <tr>
                    <th class="text-center" style="width: 10%">SMS ID</th>
                    <th class="text-center" style="width: 10%">Message ID</th>
                    <th class="text-center" style="width: 15%">Title</th>
                    <th class="text-center" style="width: 44%">Message</th>
                    <th style="width: 7%"></th>
                    <th style="width: 7%"></th>
                    <th style="width: 7%"></th>
                </tr>
            </thead>
            <tbody>
                <asp:Literal ID="ltrMessageReply" runat="server"></asp:Literal>
            </tbody>
        </table>
    </div>
</body>
</html>
