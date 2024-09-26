<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadiframe-1.aspx.cs" Inherits="FingerprintPayment.studentCard.uploadiframe_1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link src="http://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <link rel="stylesheet" href="css/uploadiframe.css" />
    <link rel="stylesheet" href="css/loading.css" />

    <script type="text/javascript">

        function readURL(input) {
            if (input.files && input.files[0]) {

                var reader = new FileReader();

                reader.onload = function (e) {
                    $('.image-upload-wrap').hide();

                    $('.file-upload-image').attr('src', e.target.result);
                    $('.file-upload-content').show();

                    $('.image-title').html(input.files[0].name);
                };

                reader.readAsDataURL(input.files[0]);

            } else {
                removeUpload();
            }
        }

        function clickbtn() {
            var clickButton = document.getElementById("<%= btnUpload.ClientID %>");
            clickButton.click();
        }

    </script>

</head>

<body onload="myFunction()" style="margin: 0;">

    <div id="loader"></div>

    <form style="display:none;" id="form1" runat="server" class="animate-bottom">
        <div class="col-xs-12" style="padding: 5px;">
            <div class="col-xs-7">
                <img id="blah" src="" runat="server" width="218" height="136">
            </div>
        </div>
        <div id="upload" runat="server">
            <asp:FileUpload ID="FileUpload" runat="server" onchange="clickbtn()" />
            <asp:Button ID="btnUpload" runat="server" CssClass="hidden" Text="Upload" />
        </div>
        <div id="reset" class="hidden" runat="server">
            <asp:Button ID="Button1" runat="server" CssClass="" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106052") %>" />
        </div>
    </form>


    <script>
        var myVar;

        function myFunction() {
            myVar = setTimeout(showPage, 3000);
        }

        function showPage() {
            document.getElementById("loader").style.display = "none";
            document.getElementById("form1").style.display = "block";
        }
    </script>


</body>
</html>
