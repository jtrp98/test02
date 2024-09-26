<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="LineConnectQr.aspx.cs" Inherits="FingerprintPayment.Modules.Invoices.LineConnectQr" %>

<!DOCTYPE html>
<html lang="id" dir="ltr">

<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
     <link rel="icon" href="/images/School Bright logo only.png" sizes="16x16 32x32" type="image/png">
    <!-- Title -->
    <title>Line Callback</title>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" />
    <link rel="stylesheet" href="//stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" integrity="sha384-WskhaSGFgHYWDcbwN70/dfYBj47jz9qbsMId/iRN3ewGhXQFZCSftd1LZCfmhktB" crossorigin="anonymous" />
</head>

<body class="bg-white text-black py-5">
    <div class="container py-5">
        <div class="row">         
            <div class="col-md-12 text-center">                       
                <asp:Label ID="lblMessage" runat="server"/>                      
            </div>
        </div>      
         <div class="row">         
            <div class="col-md-12 text-center">                 
                <a class="btn btn-info" href="#" onclick="Close()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></a>
            </div>
        </div>
    </div>

    <div id="footer" class="text-center">
    </div>
</body>

</html>

<script type="text/javascript">
   

    function Close() {
        window.close();
    }
</script>

