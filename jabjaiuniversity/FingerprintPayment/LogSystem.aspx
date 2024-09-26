<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogSystem.aspx.cs" Inherits="FingerprintPayment.LogSystem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true" ScriptMode="Release" />
        <div>
        </div>

        <%

            string path = physicalPath + "\\Log\\";
            var directory = System.IO.Directory.GetDirectories(path);

            foreach (var s in directory)
            {
                string[] _s = s.Split('\\');
                string _d = _s[_s.Length - 1];
        %>
        <div id="<%= _d %>">
            <div class="directory">
                <a onclick="ListFiles('<%= _d %>')" href="#"><%= _d %></a><br />
            </div>
            <div class="file">
            </div>
        </div>
        <%
            }
        %>
    </form>
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css">
    <!-- CSS Files -->
    <link href="/Content/Material/assets/css/material-dashboard.css?v=2.1.0" rel="stylesheet" />
    <link href="/Content/Material/layout.css?v=<%=DateTime.Now.Ticks%>" rel="stylesheet" />
    <%--<link href="/Content/Material/ldbtn.css" rel="stylesheet" />
  <link href="/Content/Material/loading.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/6.6.6/css/flag-icons.min.css" integrity="sha512-uvXdJud8WaOlQFjlz9B15Yy2Au/bMAvz79F7Xa6OakCl2jvQPdHD0hb3dEqZRdSwG4/sknePXlE7GiarwA/9Wg==" crossorigin="anonymous" referrerpolicy="no-referrer" />

    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />

    <script src="/Content/Material/assets/js/core/jquery.min.js"></script>
    <script src="/Scripts/FileSaver.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        let directory = "";

        function ListFiles(values) {
            directory = values;
            $(".file").html("");
            PageMethods.GetFiles(directory, function (respones) {

                let html = "";
                $.each(respones, function (e, s) {

                    html += "<a onclick=\"ReadFiles('" + s + "')\" href=\"#\" id='" + s + "'>" + s + "</a><br /><div id=\"files_output_" + s + "\"></div>";
                })

                $("#" + values + " .file").html(html);
            })
        }

        function ReadFiles(_f) {
            PageMethods.ReadFiles(_f, directory, function (respones) {

                $("div[id*=files_output]").html("");
                let _id = _f.replace(".json", "");
                $("#" + directory + " div[id*=files_output_" + _id + "]").append('<pre style=\"max-height: 400px;\">' + JSON.stringify(respones, null, 2) + '<\pre>');
                //let html = "";
                //$.each(respones, function (e, s) {

                //    html += "<a onclick=\"ListFiles('" + s + "')\" href=\"#\">" + s + "</a><br />";
                //})

                //$("#" + values + " .file .files_output").html(html);
            })
        }

    </script>
</body>
</html>
