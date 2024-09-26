<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="FingerprintPayment.StudentCall.View" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>School Bright - Student Calling</title>

    <!-- Bootstrap Core CSS -->
    <link href="/bootstrap SB2/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <link href="icofont/icofont.min.css?v=1" rel="stylesheet" />
    <link href="Js/sweetalert2.min.css?v=1" rel="stylesheet" />

    <script src="/bootstrap SB2/bower_components/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.signalR-2.4.1.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="/bootstrap SB2/bower_components/bootstrap/dist/js/bootstrap.js" type="text/javascript"></script>
    <script src='<%=ResolveClientUrl("//signalr-studentcall.schoolbright.co/signalr/hubs") %>' type="text/javascript"></script>
    <%-- <script src='<%=ResolveClientUrl("https://localhost:44355/signalr/hubs") %>' type="text/javascript"></script>--%>

    <script src="Js/underscore-min.js?v=1"></script>
    <script src="Js/sweetalert2.min.js?v=1"></script>
    <script src="Js/moment.min.js?v=1"></script>

    <script src="Js/jquery.loadTemplate.min.js"></script>
    <script src="Js/_func.js?v=5"></script>

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

        #gate {
            font-size: 45px;
            color: white;
            display: block;
            /* margin-bottom: 15px; */
            height: 35px;
            margin-top: -22px;
        }

        .header-wrapper .header {
            font-size: 50px;
            color: white;
            text-align: center;
            margin: 0px;
        }

        .header-wrapper .col1 {
            background: #ffc400;
        }

        .header-wrapper .col2 {
            background: #f93d77;
        }

        .header-wrapper .col3 {
            background: #3bc53b;
        }

        .content-wrapper ul {
            padding: 0px;
            margin: 0px;
        }

        .content-wrapper .col1 {
            background: #f2e7c5;
        }

        .content-wrapper .col2 {
            background: #ffc8d9;
        }

        .content-wrapper .col3 {
            background: #b4feb4;
        }

        ul > li {
            list-style-type: none;
            height: 150px;
            display: inline;
        }

            ul > li span {
                color: black;
                font-size: 30px;
            }

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
                            <a href="#" onclick="sendReqConectedUser()">
                                <img src="/images/School Bright logo 1 storke222.png" style="width: 280px;" alt="School Bright">
                            </a>
                            <% if (Company != null && !string.IsNullOrEmpty(Company.sImage))
                                { %>
                            <img src="<%= Company.sImage %>" style="width: 60px; margin-left: 10px;" alt="<%=Company.sCompany %>">
                            <% } %>
                        </div>
                        <div class="col-xs-4 text-center">
                            <label id="header">Student Calling List</label>
                        </div>
                        <div class="col-xs-4 text-right">
                            <label id="time1"><%= DateTime.Now.ToString("ddd dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %> </label>
                            <label id="time2">HH:MM:SS</label>
                            <label id="gate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402026") %> <%= Gate.GateName %></label>
                            <label id="connectionid"></label>
                        </div>
                    </div>
                </div>
            </div>
            <input type="text" id="dummytext" style="width: 0; height: 0; display: none;" value="" />
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
                        <div class="col-xs-4 col1">
                            <p class="header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402004") %> <i class="icofont-sand-clock"></i></p>
                        </div>
                        <div class="col-xs-4 col2">
                            <p class="header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402005") %> <i class="icofont-bullhorn"></i></p>
                        </div>
                        <div class="col-xs-4 col3">
                            <p class="header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402006") %> <i class="icofont-car-alt-3"></i></p>
                        </div>
                    </div>
                    <div class="row content-wrapper">
                        <div class="col-xs-4 col1">
                            <ul id="waitingList">
                            </ul>
                        </div>
                        <div class="col-xs-4 col2">
                            <ul id="annouceList">
                            </ul>
                        </div>
                        <div class="col-xs-4 col3">
                            <ul id="completeList">
                            </ul>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </form>

    <script type="text/html" id="template1">
        <li data-id="sID" class="tblstyle">
            <div class="row " style="border: 1px solid white;">
                <div class="col-lg-4 col-xs-5" style="padding: 0px;">
                    <img data-src="imgProfile" style="" class="imgProfile" />
                </div>
                <div class="col-lg-8 col-xs-7" style="padding-left: 0;">
                    <div class="row">
                        <div class="col-xs-12">
                            <span data-content="FullName"></span>
                        </div>
                        <div class="col-xs-6">
                            <span data-content="NickName"></span>
                        </div>
                        <div class="col-xs-6 text-right">
                            <span class="thistime" data-content="Time"></span>
                        </div>
                        <div class="col-xs-6">
                            <span data-content="Code"></span>
                        </div>
                        <div class="col-xs-6  text-right">
                            <span data-content="Level"></span>
                        </div>
                        <div class="col-xs-12">
                            <span data-content="Reciever"></span>
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
                _hub.server.exceptionLog("OnError", "Error: " + msg + "\nurl: " + url + "\nline: " + line + extra);
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
            $.connection.hub.stop();
        };

        window.onunload = function (e) {
            $.connection.hub.stop();
        }

    </script>

    <script>

        var announceQueue = [];
        var announcedQueue = [];
        var completedQueue = [];

        var isPlay = false;
        var isLoading = false;
        var _sid = 0;

        $.connection.hub.url = "https://signalr-studentcall.schoolbright.co/signalr"
        //$.connection.hub.url = "https://localhost:44355/signalr"
        var _hub = $.connection.stdCallingHub;

        var _schoolId = <%= Company.nCompany%>;
        var _userID = <%= 0 %>;
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

        $(function () {

            _hub.client.broadcastConnectedUser = function (mode, d1) {
                console.log(mode);
                console.log("ALL USR");
                console.log(d1);

            };

            _hub.client.sendNothing = function (data) {
                console.log("sendNothing");
                console.log(data);
            };

            _hub.client.SendAnnouncement = function (data) {
                if (isLoading) {
                    return;
                }

                if (data.SchoolID == 0) {
                    console.log("invalid schoolid = " + data.SchoolID);
                    return;
                }

                if (data.SchoolID == _schoolId) {

                    if ($("li#" + data.sID).length == 0) {
                        //push to queue   
                        announceQueue.push(data);

                        //push to display screen
                        $("#waitingList").loadTemplate($("#template1"),
                            {
                                sID: data.sID,
                                FullName: data.FullName,
                                NickName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00811") %> " + (data.NickName || "-"),
                                Time: data.Time,
                                Code: data.Code,
                                Level: data.Level,
                                imgProfile: data.Img,
                                Reciever: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106143") %> " + (data.Reciever || "-"),
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

            _hub.client.ResendAnnouncement = function (data) {

                if (isLoading) {
                    return;
                }

                if (data.SchoolID == 0) {
                    console.log("invalid schoolid = " + data.SchoolID);
                    return;
                }

                if (data.SchoolID == _schoolId) {

                    var d = _.filter(announcedQueue, function (obj) { return obj.sID == data.sID; })[0];

                    //console.log(d);
                    //if ($("#annouceList li#" + data.sID).length > 0) {
                    if (d) {

                        announcedQueue = _.without(announcedQueue, _.findWhere(announcedQueue, {
                            sID: data.sID
                        }));

                        var $li = $("#annouceList li#" + data.sID);
                        $li.find(".thistime").text(data.Time);
                        //push to queue   
                        //announceQueue.unshift(d);// push in first position
                        //console.log(isPlay);

                        announceQueue.unshift(d);

                        if (isPlay) {
                            $("#waitingList li:eq(0)").after($li);
                        }
                        else {
                            //announceQueue.unshift(d);
                            $li.prependTo("#waitingList");
                        }

                    }
                }
            };

            _hub.client.SendBackHome = function (data) {

                if (isLoading) {
                    return;
                }

                if (data.SchoolID == 0) {
                    console.log("invalid schoolid = " + data.SchoolID);
                    return;
                }

                if (data.SchoolID == _schoolId) {

                    if (data.type == "1") { //TStudentCall not null

                        //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133284") %>
                        //if ($("#waitingList li#" + data.sID).length > 0) {
                        var d = _.filter(announceQueue, function (obj) { return obj.sID == data.sID; })[0];

                        if (d) {

                            announceQueue = _.without(announceQueue, _.findWhere(announceQueue, {
                                sID: data.sID
                            }));

                            //push to queue   
                            completedQueue.push(d);

                            var $li = $("#waitingList li#" + data.sID);

                            $li.find(".thistime").text(data.Time);

                            $li.appendTo("#completeList");

                        }
                        else { //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402005") %>
                            d = _.filter(announcedQueue, function (obj) { return obj.sID == data.sID; })[0];

                            if (d) {
                                var $li = $("#annouceList li#" + data.sID);

                                $li.find(".thistime").text(data.Time);

                                $li.prependTo("#completeList");

                                completedQueue.push(d);
                            }

                        }



                    }
                    else if (data.type == "2") {//in case not found li 

                        if ($("li#" + data.sID).length == 0) {

                            //push to display screen
                            $("#completeList").loadTemplate($("#template1"),
                                {
                                    sID: data.sID,
                                    FullName: data.FullName,
                                    NickName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00811") %> " + (data.NickName || "-"),
                                    Time: data.Time,
                                    Code: data.Code,
                                    Level: data.Level,
                                    imgProfile: data.Img,
                                    Reciever: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106143") %> " + (data.Reciever || "-"),
                                },
                                {
                                    prepend: true,
                                    elemPerPage: 10,
                                    paged: true,
                                    pageNo: 1
                                });
                        }
                        else {

                        }

                    }
                }

            };

            //$.connection.hub.start({ jsonp: true });
            //$.connection.hub.logging = true;
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

            //$.connection.hub.disconnected(function () {
            //    if ($.connection.hub.lastError) {
            //        alert("Disconnected. Reason: " + $.connection.hub.lastError.message);
            //    }

            //    setTimeout(function () {
            //        $.connection.hub.start();
            //    }, 1000); // Restart connection after 1 seconds.
            //});

            //$.connection.hub.connectionSlow(function() {
            //    Swal.fire({
            //        icon: 'info',
            //        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133285") %>',
            //        //text: 'Something went wrong!',                      
            //    })
            //});

            initialPage();

            //var timerInterval
            setInterval(countTimer, 1000);

            setInterval(refreshConn, 1000 * 60);
        });

        function sendReqConectedUser() {
            _hub.server.requestConnectedUser("CLICKED");
        }

        function clearStat() {
            $("#waitingList").html('');
            $("#annouceList").html('');
            $("#completeList").html('');

            announceQueue = [];
            announcedQueue = [];
        }

        function initialPage() {

            isLoading = true;

            clearStat();

            swal.showLoading();

            PageMethods.InitialPage(_schoolId, '<%= TOKEN %>',
                function (response) {

                    console.log(response);

                    if (swal.isLoading())
                        swal.close();

                    isLoading = false;

                    if (response.status1) {
                        $.each(response.status1, function (index, data) {
                            // response.status1.forEach(data => {
                            //push to queue   
                            announceQueue.push(data);

                            $("#waitingList").loadTemplate($("#template1"),
                                {
                                    sID: data.sID,
                                    FullName: data.FullName,
                                    NickName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00811") %> " + (data.NickName || "-"),
                                    Time: data.Time,
                                    Code: data.Code,
                                    Level: data.Level,
                                    imgProfile: data.Img,
                                    Reciever: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106143") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00363") %> " + (data.Reciever || "-"),
                                },
                                {
                                    append: true,
                                    //elemPerPage: 10,
                                    //paged: true,
                                    //pageNo: 1
                                });
                        });
                    }

                    if (response.status2) {
                        $.each(response.status2, function (index, data) {
                            //response.status2.forEach(data => {
                            //push to queue 
                            announcedQueue.push(data);

                            $("#annouceList").loadTemplate($("#template1"),
                                {
                                    sID: data.sID,
                                    FullName: data.FullName,
                                    NickName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00811") %> " + (data.NickName || "-"),
                                    Time: data.Time,
                                    Code: data.Code,
                                    Level: data.Level,
                                    imgProfile: data.Img,
                                    Reciever: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106143") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00363") %> " + (data.Reciever || "-"),
                                },
                                {
                                    prepend: true,
                                    //elemPerPage: 10,
                                    //paged: true,
                                    //pageNo: 1
                                });
                        });
                    }

                    if (response.status3) {
                        $.each(response.status3, function (index, data) {
                            //response.status3.forEach(data => {
                            announcedQueue.push(data);

                            $("#completeList").loadTemplate($("#template1"),
                                {
                                    sID: data.sID,
                                    FullName: data.FullName,
                                    NickName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00811") %> " + (data.NickName || "-"),
                                    Time: data.Time,
                                    Code: data.Code,
                                    Level: data.Level,
                                    imgProfile: data.Img,
                                    Reciever: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106143") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00363") %> " + (data.Reciever || "-"),
                                },
                                {
                                    prepend: true,
                                    //elemPerPage: 10,
                                    //paged: true,
                                    //pageNo: 1
                                });
                        });
                    }

                    Swal.fire({
                        title: "Student Calling List",

                        showConfirmButton: true,
                        confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402003") %>',
                        willOpen: function () {

                        },

                    }).then(function (result) {

                        init();

                        setInterval(annouceTimer, 1200);


                    });
                },
                function (response) {
                    console.log("connect fail");
                });
        }

        function annouceTimer() {

            try {

                if (announceQueue.length > 0 && isPlay == false) {
                    var ann = announceQueue.shift();

                    announcedQueue.push(ann);

                    //const regex = /==/gi;
                    //console.log(ann.Sound);
                    if (ann.Sound) {

                        //comment v2
                        //var sound = (ann.Sound + '').replace(/==/gi, "");
                        //var $player2 = $("#audioplayer2");                        
                        //$player2.attr('src', 'data:audio/mp3;base64,' + sound);

                        //var $player3 = $("#audioplayer3");
                        //$player3.attr('src', 'data:audio/mp3;base64,' + sound);

                        //_sid = ann.sID;
                        //isPlay = true;
                        ////$player2.trigger("play");
                        //audioplayer2.play();
                        //end comment v2

                        //comment v1
                        isPlay = true;
                        _sid = ann.sID;
                        var sound = (ann.Sound + '').replace(/==/gi, "");
                        var audioFromString = base64ToBuffer(sound);

                        context.decodeAudioData(audioFromString, function (buffer) {
                            // audioBuffer is global to reuse the decoded audio later.
                            //audioBuffer = buffer;

                            var s1 = context.createBufferSource();
                            s1.buffer = buffer;
                            s1.loop = false;
                            s1.connect(context.destination);
                            s1.start(context.currentTime); // Play immediately.                                    

                            var s2 = context.createBufferSource();
                            s2.buffer = buffer;
                            s2.loop = false;
                            s2.connect(context.destination);
                            s2.start(context.currentTime + buffer.duration); // Play immediately.  
                            //alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133286") %> " + _sid + " success.");
                            //console.log("annouceTimer");
                            //console.log(announceQueue);
                            setTimeout(function () {
                                //$("#waitingList li#" + _sid).prependTo("#annouceList");
                                //isPlay = false;
                                //_sid = 0;

                                PageMethods.SetAnnouced(_sid, _schoolId,
                                    function (response) {

                                        var d = _.filter(completedQueue, function (obj) { return obj.sID == _sid; })[0];

                                        if (d) {
                                            $("#waitingList li#" + _sid).prependTo("#completeList");
                                        }
                                        else {
                                            $("#waitingList li#" + _sid).prependTo("#annouceList");
                                        }

                                        //console.log("Annouced " + _sid + " success.");
                                        isPlay = false;
                                        _sid = 0;
                                    },
                                    function (response) {
                                        //alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131078") %> " + _sid + " fail.")
                                        console.log("Annouced " + _sid + " fail.");
                                    }
                                );
                            }, (buffer.duration * 2) * 1000);


                        }, function (e) {

                            alert('Error decoding file' + e);
                        });
                        //end comment v1

                    }
                    else {
                        alert("no sound");

                    }

                }

            } catch (e) {
                alert(e);
            }
        }


        function refreshConn() {

            $.ajax({
                type: "GET",
                url: "View.aspx/RefreshConnection",
                /*data: "{}",*/
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    // Do something interesting here.
                }
            });

            //PageMethods.RefreshConnection(1,
            //    function (response) {
            //        console.log(response);
            //    },
            //    function (response) {
            //        console.log("connect fail");
            //    });
        }


    </script>
</body>
</html>
