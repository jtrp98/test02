<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterStart.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterStart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="assets/js/localStorage.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="ltrMessage" runat="server" />
        </div>
    </form>
    <script>
        // Get data from localStorage
        if (ls.isBrowserSupport()) {
            // Code for localStorage
            preRegister = ls.getItem('preRegister');

            if (preRegister.FullComplete) {

                window.location.href = "RegisterStart.aspx?rid=" + preRegister.registerID;

            }
            else {
                ls.setItem('RegisterOnline_enSourceId', '<%=enSourceId%>');

                if (ls.getItem('RegisterOnline_enSourceId') != '') {

                    window.location.href = "RegisterOnline01.aspx";

                }
            }

        } else {
            // No web storage Support.
        }
    </script>
</body>
</html>
