<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScanBarcode.aspx.cs" Inherits="FingerprintPayment.StudentCall.ScanBarcode" MasterPageFile="~/mpVoid.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>School Bright - Scan Barcode</title>

        <!-- Bootstrap Core CSS -->
        <link href="/bootstrap SB2/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
        <link href="icofont/icofont.min.css" rel="stylesheet" />
        <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />

        <script src="/bootstrap SB2/bower_components/jquery/dist/jquery.min.js" type="text/javascript"></script>
        <script src="../Scripts/jquery.signalR-2.4.1.js"></script>
        <%-- <script src="//code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>

        <!-- Bootstrap Core JavaScript -->
        <script src="/bootstrap SB2/bower_components/bootstrap/dist/js/bootstrap.js" type="text/javascript"></script>
        <script src='<%=ResolveClientUrl("//signalr-studentcall.schoolbright.co/signalr/hubs") %>' type="text/javascript"></script>
        <script src="Js/jquery.loadTemplate.min.js"></script>
        <script src="Js/_func.js?v=5"></script>
        <%-- <script src="Js/chat.js"></script>--%>
        <%--  <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>--%>
        <script src="//cdn.jsdelivr.net/npm/underscore@1.11.0/underscore-min.js"></script>
        <script src="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.js"></script>
        <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.17.1/moment.min.js"></script>

        <style>
            body {
                background-color: #0e0e0e;
            }

            #header {
                font-size: 60px;
                color: white;
            }

            #time1 {
                font-size: 34px;
                color: white;
                display: block;
                margin-bottom: 0px;
                height: 24px;
            }

            #time2 {
                font-size: 56px;
                color: white;
                display: block;
                margin-bottom: 0px;
            }


            .header-wrapper .header {
                font-size: 50px;
                color: white;
                text-align: center;
                margin: 0px;
            }

            .header-wrapper .col1 {
                background: #00c907;
            }

            .header-wrapper .col2 {
                background: #009dc8;
            }

            .header-wrapper .col3 {
                background: #3bc53b;
            }

            .content-wrapper ul {
                padding: 0px;
                margin: 0px;
            }

            .content-wrapper .col1 {
                background: #0e0e0e;
                color: #fff !important;
                font-size: 28px;
                padding: 40px;
            }

                .content-wrapper .col1 .row {
                    padding: 10px 20px;
                }

            .content-wrapper .col2 {
                background: #0e0e0e;
                color: #fff !important;
                font-size: 28px;
            }

            .content-wrapper .col3 {
                background: #b4feb4;
            }

            ul > li {
                list-style-type: none;
                height: 150px;
                display: inline;
            }

            /*   ul > li span {
                color: black;
                font-size: 30px;
            }*/

            .tblstyle .imgProfile {
                width: 100%;
                max-width: 150px;
            }
            /*
        li.tblstyle .row {
            display: table-row;
        }

            li.tblstyle .row > div {
                display: table-cell;
                vertical-align: middle;
                border: 1px solid #000;
                float: none;
            }*/
            .swal2-popup {
                width: 16em !important;
            }

            .swal2-loader {
                height: 5em !important;
                width: 5em !important;
                border-width: .5em !important;
            }

            .swal2-actions {
                margin: 0px !important;
                font-size: 20px;
                padding-top: 10px;
            }

            .swal2-modal {
                font-size: 20px;
                padding: 18px 0px;
            }

            #connectionid {
                position: fixed;
                bottom: 0px;
                right: 0px;
                color: black;
                font-size: 15px;
            }

            .annouced-wrapper {
                padding: 10px 12px;
                border-left: 1px solid;
                border-right: 1px solid;
                border-bottom: 1px solid;
            }
        </style>
    </head>

    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
            <div class="container-fluid">
                <div class="row" style="padding: 16px;">
                    <div class="col-xs-12">
                        <div class="row">
                            <div class="col-xs-4 text-left">
                                <a href="#">
                                    <img src="/images/School Bright logo 1 storke222.png" style="width: 280px;" alt="School Bright">
                                </a>
                                <%-- <% if (Company != null && !string.IsNullOrEmpty(Company.sImage))
                                { %>
                            <img src="<%= Company.sImage %>" style="width: 60px; margin-left: 10px;" alt="<%=Company.sCompany %>">
                            <% } %>--%>
                            </div>
                            <div class="col-xs-4 text-center">
                                <label id="header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402016") %></label>
                            </div>
                            <div class="col-xs-4 text-right">
                                <label id="time1"><%= DateTime.Now.ToString("ddd dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %> </label>
                                <label id="time2">HH:MM:SS</label>
                                <label id="connectionid"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <input type="text" id="dummytext" style="width: 0; height: 0; display: none;"
                    value="" />
                <%-- <iframe src="dummy.mp3" allow="autoplay" id="audio"></iframe>--%>
                <div class="hidden">
                    <%--  <audio id="audioplayer1" autoplay="autoplay" muted="muted">
                    <source type="audio/mp3" src="dummy.mp3">
                </audio>
                <audio controls id="audioplayer2" onended="audioEnded2()"></audio>
                <audio controls id="audioplayer3" onended="audioEnded3()"></audio>
                <button id="unmuteButton"></button>--%>
                </div>
                <%--<iframe src="dummy.mp3" allow="autoplay"></iframe>--%>
                <div class="row">
                    <div id="list-wrapper" class="col-xs-12">

                        <div class="row header-wrapper">
                            <div class="col-xs-6 col1">
                                <p class="header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402016") %> <%--<i class="icofont-sand-clock"></i>--%></p>
                            </div>
                            <div class="col-xs-6 col2">
                                <p class="header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402017") %> <%--<i class="icofont-bullhorn"></i>--%></p>
                            </div>
                        </div>

                        <div class="row content-wrapper">
                            <div class="col-xs-6 col1">
                                <div class="row">
                                    <div class="col-xs-12 text-center">
                                        <img id="scan-img" src="../images/School%20Bright%20logo%20only.png" width="20%" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-xs-4">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></span>
                                    </div>
                                    <div class="col-xs-8">
                                        <span id="scan-fullName"></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-4">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></span>
                                    </div>
                                    <div class="col-xs-8">
                                        <span id="scan-level"></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-4">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></span>
                                    </div>
                                    <div class="col-xs-8">
                                        <span id="scan-code"></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-4">
                                        <span>input</span>
                                    </div>
                                    <div class="col-xs-8">
                                        <input id="scan-input" type="text" value="" class="form-control" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-6 col2">
                                <ul id="annouceList">
                                </ul>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </form>

        <script type="text/html" id="template1">
            <li data-id="sID" class="tblstyle">
                <div class="row annouced-wrapper" style="">
                    <div class="col-lg-4 col-xs-5 text-center" style="padding: 0px;">
                        <img data-src="imgProfile" style="" class="imgProfile" />
                    </div>
                    <div class="col-lg-8 col-xs-7 " style="padding-left: 0;">
                        <div class="row">
                            <div class="col-xs-4">
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></span>
                            </div>
                            <div class="col-xs-8">
                                <span data-content="FullName"></span>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-4">
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></span>
                            </div>
                            <div class="col-xs-8">
                                <span data-content="Level"></span>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-4">
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></span>
                            </div>
                            <div class="col-xs-8">
                                <span data-content="Code"></span>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-4">
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></span>
                            </div>
                            <div class="col-xs-8">
                                <span class="thistime" data-content="Time"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
        </script>

        <script type="text/javascript">
            window.onerror = function (msg, url, line, col, error) {
                // Note that col & error are new to the HTML 5 spec and may not be 
                // supported in every browser.  It worked for me in Chrome.
                var extra = !col ? '' : '\ncolumn: ' + col;
                extra += !error ? '' : '\nerror: ' + error;

                // You can view the information in an alert to see things working like this:
                alert("Error: " + msg + "\nurl: " + url + "\nline: " + line + extra);

                try {
                    //_hub.server.exceptionLog("OnError", "Error: " + msg + "\nurl: " + url + "\nline: " + line + extra);
                } catch (e) {

                }
                // TODO: Report this error via ajax so you can keep track
                //       of what pages have JS issues

                var suppressErrorAlert = true;
                // If you return true, then error alerts (like in older versions of 
                // Internet Explorer) will be suppressed.
                return suppressErrorAlert;
            };

            window.onbeforeunload = function (e) {

            };

            window.onunload = function (e) {

            }

        </script>

        <script>
            $.connection.hub.url = "https://signalr-studentcall.schoolbright.co/signalr"

            var _hub = $.connection.stdCallingHub;

            var _schoolId = <%= UserData.CompanyID %>;
            var _userID = <%= UserData.UserID %>;
            var _token = '<%= TOKEN %>';
            var _ipAddress = '';

            try {
                $.ajax({
                    url: 'https://api.ipify.org/?format=json',
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        _ipAddress = data.ip;
                    }
                });
            } catch (e) {

            }

            $.connection.hub.qs = { 'SHOOLID': _schoolId, 'UID': _userID, 'IPAddress': _ipAddress, 'TOKEN': _token };

            function countTimer() {
                //var today = new Date();
                //var time = today.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', second: 'numeric', hour12: false });
                //var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
                var time = moment().format('HH:mm:ss');
                $('#time2').html(time);
            }


            function initialPage() {
                PageMethods.InitialPage(_schoolId,
                    function (response) {

                        console.log(response);

                        if (response.lstdata) {
                            $.each(response.lstdata, function (index, data) {

                                $("#annouceList").loadTemplate($("#template1"),
                                    {
                                        sID: data.sID,
                                        FullName: data.FullName,
                                        //NickName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00811") %> " + (data.NickName || "-"),
                                        Time: data.Time,
                                        Code: data.Code,
                                        Level: data.Level,
                                        imgProfile: data.imgProfile || "../images/School%20Bright%20logo%20only.png",
                                        //Reciever: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106143") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00363") %> " + (data.Reciever || "-"),
                                    },
                                    {
                                        append: true,
                                        //elemPerPage: 10,
                                        //paged: true,
                                        //pageNo: 1
                                    });
                            });
                        }

                    },
                    function (response) {
                        console.log("connect fail");
                    });
            }


            $(function () {

                setInterval(countTimer, 1200);

                _hub.client.SendAnnouncement = function (data) {

                    if (data.SchoolID == 0) {
                        console.log("invalid schoolid = " + data.SchoolID);
                        return;
                    }

                    if (data.ScanType != "BCOD") {
                        console.log("Not Barcode Type");
                        return;
                    }

                    if (data.SchoolID == _schoolId) {

                        var _img = data.imgProfile || "../images/School%20Bright%20logo%20only.png";

                        $('#scan-img').attr("src", _img + "?v=" + data.Time);
                        $('#scan-fullName').text(data.FullName);
                        $('#scan-level').text(data.Level);
                        $('#scan-code').text(data.Code);

                        //$(this).val('');
                        //$(this).focus();

                        if ($("li#" + data.sID).length == 0) {

                            //push to display screen
                            $("#annouceList").loadTemplate($("#template1"),
                                {
                                    sID: data.sID,
                                    FullName: data.FullName,
                                    //NickName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00811") %> " + (data.NickName || "-"),
                                    Time: data.Time,
                                    Code: data.Code,
                                    Level: data.Level,
                                    imgProfile: data.Img || "../images/School%20Bright%20logo%20only.png",
                                    //Reciever: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106143") %> " + (data.Reciever || "-"),
                                },
                                {
                                    append: true,
                                    //elemPerPage: 10,
                                    //paged: true,
                                    //pageNo: 1
                                });
                        }
                    }
                };

                $.connection.hub.start({
                    //transport: ['webSockets'],
                    //jsonp: true, 
                })//'webSockets',
                    .done(function () {
                        $('#connectionid').html($.connection.hub.id);
                        console.log($.connection.hub.transport.name);
                        //console.log('Now connected, connection ID=' + $.connection.hub.id + " " + $.connection.hub.transport.name);                    
                    })
                    .fail(function (e) {
                        //alert(e);
                        _hub.server.exceptionLog("OnStartFail", e);
                        //console.log('Could not Connect: ' + e);
                        setTimeout(function () {
                            location.reload();
                        }, 2000);
                    });

                $.connection.hub.error(function (e) {
                    _hub.server.exceptionLog("OnError", e);

                    setTimeout(function () {
                        location.reload();
                    }, 2000); // Restart connection after 1 seconds.
                    //console.log('SignalR error: ' + error);
                });

                $("#scan-input").on('keypress', function (e) {
                    if (e.which == 13) {
                        var _barcode = $(this).val();

                        PageMethods.ScanInput(_barcode,
                            function (d) {
                                //var d = response.d;
                                $("#scan-input").val('');
                                $("#scan-input").focus();
                                if (d.status == '2') {

                                    //var data = d.data;

                                    //var _img = data.imgProfile || "../images/School%20Bright%20logo%20only.png";

                                    //$('#scan-img').attr("src", _img +"?v="+data.Time);
                                    //$('#scan-fullName').text(data.FullName);
                                    //$('#scan-level').text(data.Level);
                                    //$('#scan-code').text(data.Code);

                                    //$(this).val('');
                                    //$(this).focus();

                                    //if ($("li#" + data.sID).length == 0) {

                                    //    //push to display screen
                                    //    $("#annouceList").loadTemplate($("#template1"),
                                    //        {
                                    //            sID: data.sID,
                                    //            FullName: data.FullName,
                                    //            Time: data.Time,
                                    //            Code: data.Code,
                                    //            Level: data.Level,
                                    //            imgProfile: data.imgProfile || "../images/School%20Bright%20logo%20only.png", 
                                    //        },
                                    //        {
                                    //            append: true,
                                    //        });

                                    //}
                                }
                                else if (d.status == '1') {
                                    Swal.fire({
                                        icon: 'info',
                                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133300") %>',
                                    })
                                }
                                else if (d.status == '0') {
                                    Swal.fire({
                                        icon: 'error',
                                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402018") %>',
                                    })
                                }

                            },
                            function (response) {

                            }
                        );
                    }
                });

                initialPage();

                $("#scan-input").focus();
            });

        </script>
    </body>
    </html>

</asp:Content>
