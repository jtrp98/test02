<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="FingerprintPayment.LINE_LIFF.v1.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>LINE Register - School Bright</title>
    <script src="https://res.wx.qq.com/mmbizwap/zh_CN/htmledition/js/vconsole/3.0.0/vconsole.min.js"></script>
    <script> var vConsole = new VConsole();</script>
    <link rel="stylesheet" href="liff.css?v=<%=DateTime.Now.Ticks%>" />
</head>
<body>
    <div id="root">
        <div class="wrap main">
            <div data-js-top="true"></div>
            <div class="container empty">
                <header style="height: 100px"></header>
                <div id="profile" class="profile">
                    <div class="thumb_profile">
                        <a>
                            <img src="../school-bright-line.png" alt="" /></a>
                    </div>
                    <div class="profile_info">
                        <h1 class="profile_title" style="margin: 21px 50px 3px 50px;">กำลังลงทะเบียน</h1>
                        <h1 class="profile_title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132344") %><span id="dots" style="position: absolute;"></span></h1>
                        <div data-js-floating-bar-threshold="true" style="position: absolute; left: 0; right: 0; margin-top: 0; pointer-events: none"></div>
                        <div id="divMessage" class="floating_button" style="background-color: #35435e; transform: translateY(0px); display: none;">
                            <div class="group">
                                <div class="img">
                                    <img src="../school-bright-line.jpg" alt="" />
                                </div>
                                <dl class="txt_area">
                                    <dt id="dtMessage"></dt>
                                </dl>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="content"></div>
            </div>
        </div>
    </div>
    <input type="hidden" id="ssid" name="ssid" value="<%=SSID %>" />
</body>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
<!-- LIFF SDK -->
<script src="https://d.line-scdn.net/liff/1.0/sdk.js"></script>
<script src="liff.js?v=<%=DateTime.Now.Ticks%>"></script>
<script type="text/javascript">

    var dots = 0;
    $(document).ready(function () {
        setInterval(type, 600);
    });

    function type() {
        if (dots < 3) {
            $('#dots').append('.');
            dots++;
        }
        else {
            $('#dots').html('');
            dots = 0;
        }
    }
</script>
</html>
