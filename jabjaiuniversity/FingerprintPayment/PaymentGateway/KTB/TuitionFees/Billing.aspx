<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="FingerprintPayment.PaymentGateway.KTB.TuitionFees.Billing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        /* https://www.cleancss.com/css-minify/ */
        @font-face{font-family:THSarabun;src:url(../../../fonts/THSarabun.ttf)}html,body{height:100%;display:flex;flex-direction:column;font-family:THSarabun}.section-top{height:576px}.section-bottom{flex-grow:1;margin-top:20px}.section-bottom.improve-system{text-align:center;margin-top:5px}.section-bottom.improve-system .confirm-btn{text-decoration:none;width:90%!important;border-radius:25px;background-image:linear-gradient(to right,#29c47b,#63d39f)}.card{box-shadow:0 4px 8px 0 rgba(0,0,0,0.2);transition:.3s;height:100%}.card:hover{box-shadow:0 8px 16px 0 rgba(0,0,0,0.2)}.container{height:100%;display:flex;flex-direction:column}.confirm-btn{text-align:center;padding:15px 0 0}.confirm-btn button{width:100%;height:50px}.font-1{font-size:18px!important}.font-2{font-size:25px!important}.font-3{font-size:35px!important}.btn-success{color:#fff;background-color:#5cb85c;border-color:#4cae4c}.btn{display:inline-block;margin-bottom:0;font-weight:400;text-align:center;white-space:nowrap;vertical-align:middle;-ms-touch-action:manipulation;touch-action:manipulation;cursor:pointer;background-image:none;border:1px solid transparent;padding:6px 0;font-size:14px;line-height:1.42857143;border-radius:4px;-webkit-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none;margin-top:10px;font-size:23px;width:100%}.pay-button{border-radius:25px;background-color:#219b78!important;background-image:linear-gradient(to right,#20c276,#67d6a3)}.row-topic{display:flex;height:93px;margin-bottom:17px}.logo{width:96px;height:86.34px;margin-top:10px}.topic{text-indent:1em;border-left:6px solid #ffa291;margin:25px 10px 10px}.topic p{margin:0;font-weight:700}.topic-detail{text-align:left;margin:10px 0 20px;font-weight:700;color:#ffb8a9}.payer{display:block;overflow:hidden}.payer p{width:100%;text-align:left;margin:0;font-weight:400}.payer p.row-input{text-align:left}.row-highlight{background-color:#fff0e1;margin:0 -20px}.row{border-bottom:1px solid #facda4;display:block;overflow:hidden;margin:0 20px;padding-top:25px}.row.last{border-bottom:0 solid #facda4;height:15px}.row-label{float:left;width:5%;text-align:left;margin:-3px -3px 10px 3px;font-weight:700;color:#444;white-space:nowrap}.row-input{float:right;width:95%;text-align:right;margin:18px 3px -5px -3px;font-weight:700;color:#444}.row-summary{bottom:15px}.row-summary .row-label{float:left;width:50%;text-align:left;margin:15px 0 10px;font-size:20px;font-weight:700;color:#000}.row-summary .row-input{float:right;width:50%;text-align:right;margin:15px 0 10px;font-size:20px;font-weight:700;color:#000}.payment-container[_kpayment]{visibility:hidden;opacity:0;z-index:999999;position:fixed;top:0;bottom:0;left:0;right:0;overflow:hidden;transition-timing-function:ease-in;transition:.2s;background-color:rgba(0,0,0,0.8);text-align:center;justify-content:center;align-items:center;display:flex}.payment-container[_kpayment].show{transition:.25s;transition-timing-function:ease-out;opacity:1;visibility:visible}div.payment-container > iframe[_kpayment]{border:none;transition:.2s;transition-timing-function:ease-in-out}.pay-button[_kpayment]{width:105px;transition:all .5s;line-height:normal;font-family:"Helvetica Neue",Helvetica,Arial,sans-serif;font-size:14px;display:inline-block;padding:10px 15px;min-height:15px;border:0;font-weight:700;text-align:center;text-decoration:none;outline:none;border-radius:2px;user-select:none;background-color:#219b78;color:#fff;cursor:pointer;width:110px;transition:all .5s}.pay-button[_kpayment] span::before{position:absolute;width:28px;height:36px;content:"";margin-top:-10px;opacity:0;margin-left:-30px;-webkit-transition:all .5s;transition:all .5s;background:transparent url(data:image/png;base64,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) no-repeat center right;background-size:contain}.pay-button[_kpayment] span::after{content:"Pay Now"}.pay-button[_kpayment].processing span::after{content:"Processing"}.pay-button[_kpayment].processing{pointer-events:none;cursor:default}.pay-button[_kpayment]:hover span{padding-left:20px}.pay-button[_kpayment]:hover span:before{opacity:1}.pay-button[_kpayment][disabled],.pay-button[_kpayment].disabled{background-color:#9e9e9e;pointer-events:none;cursor:not-allowed}.pay-button[_kpayment] span::after{content:'<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %>';font-family:THSarabun;font-size:25px!important}.pay-button[_kpayment] span::before{margin-top:-5px}
    </style>
    <style>
        /* modal */
        #modal-background{display:none;position:fixed;top:0;left:0;width:100%;height:100%;z-index:1000;background-color:rgba(0,0,0,0.8)}#modal-content{display:none;padding-top:15px;box-sizing:border-box;overflow-x:hidden;text-align:left}#card-view{border-radius:20px;width:20em;box-shadow:0 2px 2px 0 rgba(0,0,0,.14),0 3px 1px -2px rgba(0,0,0,.12),0 1px 5px 0 rgba(0,0,0,.2);overflow:hidden;min-height:705px;left:50%;margin:0 0 0 -160px;padding:10px;position:absolute;top:25px;bottom:25px;z-index:1000;background:#fff}#card-detail{padding:10px 10px 3px;margin:0 auto}#card-detail p{margin:0;font-size:19px;color:#707070}#card-detail p span.left{font-weight:700}#card-detail p span.right{float:right}#card-detail p.amount{font-size:25px;font-weight:700;margin-top:10px}#modal-background.active,#modal-content.active{display:block}.modal-close{display:inline-block;vertical-align:middle;background-size:contain;background-position:center center;background-repeat:no-repeat;background-image:url(data:image/png;base64,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)}.row-topic.modal{margin-bottom:-5px}.logo.modal{margin-top:3px;width:80px;height:72.53px}.topic.modal{border-left:0 solid #ffa291;margin:20px -8px 10px;color:#707070;line-height:1}.modal-address,.modal-tax,.modal-message{margin:0;text-align:center;font-size:19px;font-weight:700;color:#707070}.modal-message{font-size:21px}hr.modal{margin:7px 0 0;border:1px solid #ff9800aa}hr.modal.dash{margin:0;border:1px dashed #ff9800aa}.save-bill{display:flex;justify-content:center;align-items:center;padding-top:3px}.save-bill button{width:92%;height:32px;border-radius:24px;font-family:THSarabun;font-size:20px;border:0;color:#fff;background-color:#219b78!important;background-image:linear-gradient(to right,#20c276,#67d6a3)}
        .checkmark__circle{stroke-dasharray:166;stroke-dashoffset:166;stroke-width:2;stroke-miterlimit:10;stroke:#7ac142;fill:none;animation:stroke 0.6s cubic-bezier(0.65,0,0.45,1) forwards}.checkmark{width:114px;height:114px;border-radius:50%;display:block;stroke-width:4;stroke:#fff;stroke-miterlimit:10;margin:14% auto;box-shadow:inset 0px 0px 0px #7ac142;animation:fill .4s ease-in-out .4s forwards, scale .3s ease-in-out .9s both;position:absolute;opacity:0.9;}.checkmark__check{transform-origin:50% 50%;stroke-dasharray:48;stroke-dashoffset:48;animation:stroke 0.3s cubic-bezier(0.65,0,0.45,1) 0.8s forwards}@keyframes stroke{100%{stroke-dashoffset:0}}@keyframes scale{0%,100%{transform:none}50%{transform:scale3d(1.1,1.1,1)}}@keyframes fill{100%{box-shadow: inset 0px 0px 0px 63px #7ac142}}
    </style>
    <!--Script references. -->
    <!--Reference the jQuery library. -->
    <script src="https://code.jquery.com/jquery-3.4.1.min.js" type="text/javascript"></script>
    <!--Reference the SignalR library. -->
    <script src="https://ajax.aspnetcdn.com/ajax/signalr/jquery.signalr-2.2.2.min.js"></script>
    <!--Reference the autogenerated SignalR hub script. -->
    <script src='https://signalrpos.schoolbright.co/signalr/hubs'></script>

    <script src="https://superal.github.io/canvas2image/canvas2image.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>
</head>
<body>
    <asp:Literal ID="ltrPay" runat="server"></asp:Literal>
    <%--<div class="section-top">
        <div class="card" style="border-radius: 25px; margin: 11px; padding: 5px;">
            <div class="container">
                <div class="section-top" style="padding: 0 15px 0 15px;">
                    <div class="row-topic">
                        <img src="../../../images/SchoolBrightLogo.png" alt="" class="logo" />
                        <blockquote class="topic">
                            <p class="font-2">ชำระค่าเทอม</p>
                            <p class="font-1">26 มี.ค. 67 12:59 น.</p>
                        </blockquote>
                    </div>
                    <p class="topic-detail font-2">รายละเอียดการชำระค่าเทอม</p>
                    <div class="payer">
                        <p class="row-label font-2">testgrade testgrade</p>
                        <p class="row-input font-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111059") %> / 1</p>
                    </div>
                    <div class="row-highlight">
                        <div class="row">
                            <p class="row-label font-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803041") %></p>
                            <p class="row-input font-2">1025502</p>
                        </div>
                        <div class="row">
                            <p class="row-label font-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132738") %></p>
                            <p class="row-input font-2">Q084967032606098S9005IV</p>
                        </div>
                        <div class="row">
                            <p class="row-label font-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></p>
                            <p class="row-input font-2">
                                15,550.00
                                <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>-->
                            </p>
                        </div>
                        <div class="row" style="display: none;">
                            <p class="row-label font-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131067") %></p>
                            <p class="row-input font-2">
                                0.00
                                <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>-->
                            </p>
                        </div>
                        <div class="row last">
                        </div>
                    </div>
                </div>
                <div class="section-bottom" style="padding: 0 15px 0 15px;">
                    <div class="row-summary">
                        <p class="row-label font-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501013") %></p>
                        <p class="row-input font-3">
                            15,550.00
                            <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>-->
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="section-bottom">
        <div class="confirm-btn">
            <button _kpayment="" type="button" class="pay-button" style="width: 92%; height: 50px; border-radius: 25px;"><span style="transition: all 0.5s ease 0s;"></span></button>
        </div>
    </div>--%>
    <%--<div id="modal-background"></div>
    <div id="modal-content">
        <div id="card-view">
            <span class="modal-close" style="width: 2rem; height: 2rem; float: right;"></span>
            <div class="row-topic modal">
                <img src="../../../images/SchoolBrightLogo.png" alt="" class="logo modal" />
                <blockquote class="topic modal">
                    <p class="font-2">โรงเรียนจับจ่ายวิทยา</p>
                    <p class="font-1">Jabjai Wittaya School</p>
                </blockquote>
            </div>
            <p class="modal-address">708/13 ถนนจรัญสนิทวงศ์ ซอยจรัญสนิทวงศ์ 3</p>
            <p class="modal-address">แขวงวัดท่าพระ เขตบางกอกใหญ่ กรุงเทพมหานคร 10600</p>
            <p class="modal-tax"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132518") %>: 0-1055-59099-99-9</p>
            <hr class="modal" />
            <div id="card-detail" style="">
                <p class="date"><span class="left">&nbsp;</span><span class="right">28/03/2567</span></p>
                <p class="fullname"><span class="left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>:</span><span class="right">เด็กชายสมพงษ์ สุมนัส</span></p>
                <p class="level"><span class="left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>:</span><span class="right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111061") %>/1</span></p>
                <p class="student-code"><span class="left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>:</span><span class="right">12345</span></p>
                <p class="ref-1"><span class="left">REF.1</span><span class="right">334455-1234568-011</span></p>
                <p class="ref-2"><span class="left">REF.2</span><span class="right">334455-1234568-011</span></p>
                <p class="amount"><span class="left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106164") %></span><span class="right"><span style="color: green;">10,000</span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span></p>
            </div>
            <hr class="modal dash" />
            <div id="card-scan" style="margin-top: 10px; text-align: center;">
                <div style="display: flex; justify-content: center;">
                    <img src="/images/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %>QR.png" style="width: 40px; position: absolute; padding: 48px;" />
                    <img src="/images/qrcode1.png" style="width: 135px;" />
                </div>
                <img src="/images/barcode1.png" style="width: 215px; height: 60px; margin-top: 5px;" />
            </div>
            <p class="modal-message">กรุณาบันทึกรูป หรือแคปหน้าจอ</p>
            <p class="modal-message">เพื่อชำระเงินที่ Application ของธนาคาร</p>
            <div class="save-bill">
                <button type="button">บันทึกรูปภาพ</button>
            </div>
        </div>
    </div>--%>
    <script>
        $(document).ready(function () {

            $.connection.hub.url = "https://signalrpos.schoolbright.co/signalr/hubs";

            // Declare a proxy to reference the hub. 
            var kbank = $.connection.kBankHub;

            kbank.client.callbackResponse = function (res) {
                console.log('callbackResponse');
                //console.log(res);
                var jsonObj = JSON.parse(res); console.log(jsonObj);
                if (jsonObj.statusCode == '00') {
                    $('#card-scan div').append('<svg class="checkmark" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 52 52"><circle class="checkmark__circle" cx="26" cy="26" r="25" fill="none"/><path class="checkmark__check" fill="none" d="M14.1 27.2l7.1 7.2 16.7-16.8"/></svg>');
                }
            };

            // Start the connection.
            $.connection.hub.start().done(function () {
                kbank.server.iMEIMapping('<%= REF1 %>');
            });

            $.connection.hub.error(function (e) {
                console.log(e);
            });

            $(".pay-button, .modal-close").click(function () {
                $("#modal-content, #modal-background").toggleClass("active");
                return false;
            });
            $(".save-bill button").click(function () {
                console.log(".save-bill button");
                html2canvas(document.querySelector('#card-view'), {
                    onrendered: function (canvas) {
                        return Canvas2Image.saveAsPNG(canvas);
                    }
                });
                return false;
            });
        });
    </script>
</body>
</html>
