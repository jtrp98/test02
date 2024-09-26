<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LINESummary.aspx.cs" Inherits="FingerprintPayment.StudentInfo.LINESummary" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>LINE Summary - School Bright</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <style>
        .bs-example {
            margin: 20px;
        }

        .truncate {
            width: 335px;
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
    </style>
</head>
<body>
    <div class="bs-example">
        <asp:Literal ID="ltrSummary" runat="server"></asp:Literal>
    </div>
</body>
</html>
