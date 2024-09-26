<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="FingerprintPayment.WebForm1" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <asp:Button ID="btn" runat="server" OnClick="btn_Click" Text="HttpWebRequest" />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="HttpWebRequest UserToken" />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="HttpWebRequest Products GET" />
            <br />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="HttpWebRequest Products POST" />
            <br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="HttpWebRequest Services GET" />
            <br />
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="HttpWebRequest Services POST" />
            <br />
            <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" Visible="true" Height="200px" Width="200px"></asp:TextBox>
            <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="SQL Execute" Visible="true" />
            <br />
            <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="HttpWebRequest Contacts GET" />
            <br />
            <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="HttpWebRequest Contacts POST" />
            <br />
            <asp:Button ID="Button9" runat="server" OnClick="Button9_Click" Text="HttpWebRequest Invoice GET" />
            <br />
            <asp:Button ID="Button10" runat="server" OnClick="Button10_Click" Text="HttpWebRequest Invoice POST" />
            <asp:Image ID="plBarCode" runat="server" />
            <asp:Button ID="Button11" runat="server" OnClick="Button11_Click" Text="Update permission" Visible="false" />

        </div>
    </form>
</body>
</html>
