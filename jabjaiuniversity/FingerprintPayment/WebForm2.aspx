<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="FingerprintPayment.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="javascript/jquery-2.1.3.min.js"></script>
</head>
<body>
    <!-- Test --> 


    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button2" runat="server" Text="Button2" OnClick="Button2_Click" /><br />
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /><br />
            <asp:DropDownList ID="ddlSchool" runat="server"></asp:DropDownList><br />
            <asp:TextBox ID="textbox" runat="server"></asp:TextBox>
            <asp:Button ID="btnMessageLine" runat="server" Text="Send Message" OnClick="btnMessageLine_Click" /><br />
            <asp:DataGrid ID="dgd" runat="server"></asp:DataGrid>
            <select id="select_1" multiple="multiple">
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
                <option value="5">4</option>
                <option value="4">5</option>
                <option value="6">6</option>
            </select>
            <select id="select_2" multiple="multiple">
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
                <option value="5">4</option>
                <option value="4">5</option>
                <option value="6">6</option>
            </select>
        </div>
    </form>
    <script type="text/javascript">
        //var values = [];
        $(function () {
            $("#select_1").change(function (e) {
                CheckedSelcetValues($("#select_1").val(), "select_2");
            });

            $("#select_2").change(function (e) {
                CheckedSelcetValues($("#select_2").val(), "select_1");
            });
        });

        function CheckedSelcetValues(v1, selectId) {
            var tmp = [];
            $.each(v1, function (e1, s1) {
                var checked = false;
                $.each($("#" + selectId + " option"), function (e2, s2) {
                    console.log(s1 === $(s2).val());
                    if (s1 == $(s2).val()) checked = true;
                });
                $("#" + selectId + " option[value=" + s1 + "]").attr("disabled", checked);
            });
        }
    </script>
</body>
</html>
