<%@ Page Title="" Language="C#" MasterPageFile="~/Material.Master" AutoEventWireup="true" CodeBehind="topup.aspx.cs" Inherits="FingerprintPayment.topup" %>


<asp:Content ID="Content10" ContentPlaceHolderID="Script" runat="server">

<%--    <link href="Content/select2/select2.css" rel="stylesheet" />
    <script src="Scripts/select2/select2.js"></script>--%>

    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>

    <script type="text/javascript">

        var keyEnter = jQuery.Event("keydown");
        keyEnter.which = 13; 
        keyEnter.keyCode = 13;

        var selectedUser1 = null;
        var selectedUser2 = null;
        var availableValueEmployees = [];
        var arrUser1, arrUser2;
        var isWait1 = false;
        var isWait2 = false;

        $.ajaxSetup({ cache: true });

        var CONFIG;

        $(function () {

            PageMethods.GetConfig(
                function (response) {
                    CONFIG = response;
                },
                function (response) {

                }
            );

            //$('#ctl00_MainContent_ddlsublevel').change(function () {
            //    $('input[id*=txtSubLV2ID]').val("");
            //});

            $("#btncancel").click(function () {
                $("#modal-invoice-payment").modal("show");
            });

            $(".tableSection tbody").scrollTop(0)
            $(".tableSection tbody").scroll(function (e) {
                var $this = $(this);
                e.preventDefault();
                if ($results == "") {

                    if ($this.scrollTop() + $this.height() == document.getElementById("table-body").scrollHeight && document.getElementById("table-body").scrollHeight > 460) {
                        if (topup_date !== "enddata") {
                            $results == "Loading"
                            getHistroy();
                        }
                        //console.log("Loadinng");
                    }
                    else {
                    }
                }
            });
                   
            getTopuphistory();

            $("#txtmoney").keydown(function (event) {
                if (event.keyCode == 13) {
                    if (!$('#btnsubmit').prop('disabled')) {
                        $("#btnsubmit").click();
                    }                    
                }
            });

            $("#btnsearch").click(function () {
                SearchHistroy();
            });

            $('#SearchUser1').autoComplete({
                //resolver: 'custom',
                minLength: 2,
                //autoSelect: false,
                resolverSettings: {
                    fail: function (e) {
                        console.log('fail : ' + JSON.stringify(e));
                    },
                    requestThrottling: 200,
                    queryKey: "wording",
                },
                formatResult: function (item) {
                    return {
                        text: item.User_Name,
                        type: item.User_Type,
                        id: item.User_Id,
                    };
                },
                events: {
                    
                    searchPost: function (resultFromServer) {
                        //console.log("searchPost");
                        arrUser1 = resultFromServer;                    

                        if (isWait1) {
                            $('#SearchUser1').trigger(keyEnter);
                            isWait1 = false;
                        }
                        return resultFromServer;
                    }
                }                
            }).on('autocomplete.select', function (evt, item) {
                //console.log("select");
                selectedUser1 = item;
                getbalance(selectedUser1.User_Id, selectedUser1.User_Type);
                arrUser1 = [];
                isWait1 = false;
            }).on('keydown', function (e) {
                
                if (e.keyCode == 13) {
                    //console.log("keydown1");
                    if (arrUser1 != null && arrUser1.length > 0) {
                        //console.log("keydown2");
                        $('#SearchUser1').autoComplete('set', arrUser1[0]);
                        selectedUser1 = arrUser1[0];
                        getbalance(selectedUser1.User_Id, selectedUser1.User_Type);
                        arrUser1 = [];
                        isWait1 = false;
                    }
                    else {
                        isWait1 = true;
                    }
                    
                }
            });
            //.on('autocomplete.dd.hidden', function (evt, item) {
            //    console.log("hidden");
            //    arrUser1 = [];
            //    isWait = false;
            //})

            $('#SearchUser2').autoComplete({
                //resolver: 'custom',
                minLength: 2,
                
                resolverSettings: {
                    fail: function (e) {
                        console.log('fail : ' + JSON.stringify(e));
                    },
                    requestThrottling: 200,
                    queryKey: "wording",
                },
                formatResult: function (item) {
                    return {
                        text: item.User_Name,
                        type: item.User_Type,
                        id: item.User_Id,
                    };
                },
                events: {
                    searchPost: function (resultFromServer) {
                        arrUser2 = resultFromServer;
                        if (isWait2) {
                            $('#SearchUser2').trigger(keyEnter);
                            isWait2 = false;
                        }
                        return resultFromServer;
                    }
                }     
            }).on('autocomplete.select', function (evt, item) {
                //console.log(item);
                selectedUser2 = item;
                rowsIndex = 1;
                $(".tableSection tbody tr").remove();
                $(".tableSection tbody").scrollTop(0);
                getHistroy();
                arrUser2 = [];
                isWait2 = false;
            }).on('keydown', function (e) {
                if (e.keyCode == 13) {
                    if (arrUser2 != null &&  arrUser2.length > 0) {
                        $('#SearchUser2').autoComplete('set', arrUser2[0]);
                        selectedUser2 = arrUser2[0];
                        rowsIndex = 1;
                        $(".tableSection tbody tr").remove();
                        $(".tableSection tbody").scrollTop(0);
                        getHistroy();
                        arrUser2 = [];
                        isWait2 = false;
                    }
                    else {
                        isWait2 = true;
                    }
                }
            });


            $("#btnsubmit").click(function () {
                $("body").mLoading();               
                var money = $("#txtmoney").val();
              
                if (money === "") {

                    $.confirm({
                        title: false,
                        content: '<div class="text-center"><img src="images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><h3 class="text-danger text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602019") %></h1></div>',
                        theme: 'material',
                        columnClass: 'col-md-5 col-md-offset-4',
                        type: 'red',
                        buttons: {
                            "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                keys: ['enter', 'shift'],
                                btnClass: 'btn-primary',
                                action: function () {
                                    //$('#mySelect2').select2('open');
                                    $('#txtmoney').focus();
                                }
                            }
                        }
                    });
                    $("body").mLoading('hide');
                }
                else if (money > CONFIG.maxTopup) {
                    $.confirm({
                        title: false,
                        content: '<div class="text-center"><img src="images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h3 class="text-danger text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602024") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131162") %> ' + CONFIG.maxTopup +' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131163") %></h3></div>',
                        theme: 'material',
                        columnClass: 'col-md-5 col-md-offset-4',
                        type: 'red',
                        buttons: {
                            "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                keys: ['enter', 'shift'],
                                btnClass: 'btn-primary',
                                action: function () {
                                    //$('#mySelect2').select2('open');
                                    //$('.select2-search__field').focus();
                                    $('#txtmoney').focus();
                                }
                            }
                        }
                    });
                    $("body").mLoading('hide');
                } else {

                    if (selectedUser1) {

                        $('#btnsubmit').prop('disabled', true);

                        PageMethods.TopupMoney({
                            "Money": money,
                            "User_id": selectedUser1.User_Id,
                            "User_type": selectedUser1.User_Type,
                            "Name": selectedUser1.User_Name
                        },
                            function (response) {
                                
                                if (response.status == "topup updated sucessfully") {
                                    $.confirm({
                                        title: false,
                                        content: '<div class="text-center"><img src="images/checked.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h3 class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602013") %></h3></div>',
                                        theme: 'material',
                                        type: 'blue',
                                        buttons: {
                                            "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                                btnClass: 'btn-primary',
                                                keys: ['enter', 'shift'],
                                                action: function () {
                                                    getTopuphistory();
                                                }
                                            }
                                        },
                                        //onClose: function () {
                                        //    $("#SearchUser1").focus();
                                        //},
                                    });

                                }
                                else if (response.status == "topup updated sucessfully[ Offline ]") {
                                    $.confirm({
                                        width: 'auto',
                                        useBootstrap: false,// Key line
                                        title: false,
                                        content: '<div class="text-center"><img src="images/checked.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h3><p class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131150") %></p>' +
                                            '<p class="text-center"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131149") %></p> ' +
                                            '<p class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131151") %></p> ' +
                                            '<p class="text-center">ระบบจะทำการบันทึกลงฐานข้อมูลอัติโนมัติเมื่อระบบกลับมาออนไลน์</p> ' +
                                            '<p class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131152") %> </p>' +
                                            '<br/><p class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131153") %></p></h3></div>',
                                        theme: 'material',
                                        type: 'blue',
                                        buttons: {
                                            "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                                btnClass: 'btn-primary',
                                                keys: ['enter', 'shift'],
                                                action: function () {
                                                    focusOnInputID2();
                                                }
                                            }
                                        }
                                    });
                                }
                                else if (response.statusCode != 200) {
                                    $.confirm({
                                        width: 'auto',
                                        useBootstrap: false,// Key line
                                        title: false,
                                        content: '<div class="text-center"><img src="images/checked.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h3><p class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131154") %></p>' +
                                            '<br/><p class="text-center"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %> : ' + response.ReponesTime + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %></p> ' +
                                            '<br/><p class="text-center">' + response.status + '</p> ' +
                                            '</h3></div>',
                                        theme: 'material',
                                        type: 'blue',
                                        buttons: {
                                            "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                                btnClass: 'btn-primary',
                                                keys: ['enter', 'shift'],
                                                action: function () {
                                                    focusOnInputID2();
                                                }
                                            }
                                        }
                                    });
                                }
                                else if (response.status == "Function Cannot be access at this time. System is down temporarily") {
                                    $.confirm({
                                        width: 'auto',
                                        useBootstrap: false,// Key line
                                        title: false,
                                        content: '<div class="text-center"><img src="images/checked.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h3><p class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131155") %></p>' +
                                            '<br/><p class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131156") %></p> ' +
                                            '<br/><p class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131157") %></p> ' +
                                            '</h3></div>',
                                        theme: 'material',
                                        type: 'blue',
                                        buttons: {
                                            "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                                btnClass: 'btn-primary',
                                                keys: ['enter', 'shift'],
                                                action: function () {
                                                    focusOnInputID2();
                                                }
                                            }
                                        }
                                    });
                                }
                                else {
                                    var messageAlert = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602028") %>";

                                    $.confirm({
                                        title: false,
                                        content: '<div class="text-center"><img src="images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><h3 class="text-danger text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131159") %> <br />' + messageAlert + '</h3></div>',
                                        theme: 'material',
                                        columnClass: 'col-md-5 col-md-offset-4',
                                        type: 'red',
                                        buttons: {
                                            "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                                keys: ['enter', 'shift'],
                                                btnClass: 'btn-primary',
                                                action: function () {
                                                    $('#txtmoney').focus();
                                                }
                                            }
                                        }
                                    });
                                }
                                $("body").mLoading('hide');
                                $('#btnsubmit').prop('disabled', false);
                            },
                            function (response) {
                               
                                $.confirm({
                                    title: false,
                                    content: '<div class="text-center"><img src="images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><h3 class="text-danger text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131159") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602028") %></h3></div>',
                                    theme: 'material',
                                    columnClass: 'col-md-5 col-md-offset-4',
                                    type: 'red',
                                    buttons: {
                                        "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                            keys: ['enter', 'shift'],
                                            btnClass: 'btn-primary',
                                            action: function () {
                                                console.log(response);
                                                $("body").mLoading('hide');
                                                $('#txtmoney').focus();
                                            }
                                        }
                                    }
                                });
                                $('#btnsubmit').prop('disabled', false);
                                $("body").mLoading('hide');
                            }
                        )
                                             
                        $("#txtbalance").val("");
                        $("#txtmoney").val("");
                    }
                    else {
                        $.confirm({
                            title: false,
                            content: '<div class="text-center"><img src="images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h3 class="text-danger text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602018") %></h3></div>',
                            theme: 'material',
                            columnClass: 'col-md-5 col-md-offset-4',
                            type: 'red',
                            buttons: {
                                "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                    keys: ['enter', 'shift'],
                                    btnClass: 'btn-primary',
                                    action: function () {
                                        $("body").mLoading('hide');
                                        focusOnInputID2();
                                    }
                                }
                            }
                        });
                        $('#btnsubmit').prop('disabled', false);
                    }
                }

            });

            $('#SearchUser1').focus();

        });

        function getbalance(userid, usertype) {
            $("body").mLoading();
            PageMethods.Getbalance(userid, usertype,
                function (response) {
                    $("#txtbalance").val(response.Money);
                    $("body").mLoading('hide')
                    $("#txtmoney").focus();
                    $('#imgProfile').attr('src', response.Picture);
                },
                function (response) {
                }
            );
        }
        
        $results = "";
        var rowsIndex = 1;
        function getHistroy() {
            $("body").mLoading();
            PageMethods.SearchHistoryTopups(selectedUser2.User_Id, selectedUser2.User_Type, "", function (result) {
                console.log(result);
                topup_date = "enddata";
                $.each(result, function (e, s) {
                    topup_date = s.date;
                    $(".tableSection tbody").append("<tr><td>" + rowsIndex++ + ". " + s.name + "<td>" + s.money + "<td>" + s.time
                        + "<td class=\"center\"><div class=\"btn btn-danger\" onclick=\"toupupcancel('" + s.id + "','" + s.name + "',2)\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602011") %></div>");
                });
                $("body").mLoading('hide');
                $results = "";

            });
        }

        function focusOnInputID2() {
            selectedUser1 = null;
            arrUser1 = [];
            isWait1 = false;
            $('#SearchUser1').autoComplete('clear');
            $('#imgProfile').attr('src', '');
        
            setTimeout(function () {
                $('#SearchUser1').focus();
            }, 400);
        }
        function getTopuphistory() {
            PageMethods.Historytopups(function (response) {
                $("#topuptable tbody tr").remove();
                $.each(response, function (Key, Values) {
                    $("#topuptable tbody").append("<tr><td>" + Values.time + "<td>" + Values.name + "<td>" + Values.money + "<td>" + Values.balannce
                        + "<td class=\"center\"><div class=\"btn btn-danger\" onclick=\"toupupcancel('" + Values.id + "','" + Values.name + "',1)\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602011") %></div>");

                });
                focusOnInputID2();

            }, function () { });
        }

        function toupupcancel(topup_id, name, loadingdata) {
            console.log(topup_id);
            $("body").mLoading();
            PageMethods.TopupMoney_Cancel(topup_id, name, function (response) {
                $("body").mLoading('hide');
                var JSONdata = $.parseJSON(response)
                if (JSONdata.nMoney > 0) {
                    console.table(JSONdata);
                    $.confirm({
                        title: false,
                        content: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602016") %></h3>',
                        theme: 'material',
                        columnClass: 'col-md-5 col-md-offset-4',
                        type: 'blue',
                        buttons: {
                            "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                keys: ['enter', 'shift'],
                                btnClass: 'btn-primary',
                                action: function () {
                                    if (loadingdata === 1)
                                        getTopuphistory();
                                    else
                                        SearchHistroy();

                                }
                            }
                        }
                    });
                } else {
                    console.log(response);
                    var messageAlert = "";
                    if (response == "fail") messageAlert = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131160") %>";
                    else messageAlert = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602017") %>";

                    $.confirm({
                        title: false,
                        content: '<div class="text-center"><img src="images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><h3 class="text-danger text-center">' + messageAlert + '</h3></div>',
                        theme: 'material',
                        columnClass: 'col-md-5 col-md-offset-4',
                        type: 'red',
                        buttons: {
                            "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                keys: ['enter', 'shift'],
                                btnClass: 'btn-primary',
                                action: function () {
                                    $('#SearchUser1').focus();
                                }
                            }
                        }
                    });
                }

            }, function () { });
        }

        function ClearHistroyList() {
            //$(".search").val("");          
            topup_date = "";
            rowsIndex = 1;
            $(".tableSection tbody tr").remove();
            $(".tableSection tbody").scrollTop(0);
        }

        function SearchHistroy() {           
            rowsIndex = 1;
            $(".tableSection tbody tr").remove();
            $(".tableSection tbody").scrollTop(0);
            getHistroy();
        }

      
        function ClearData() {
          
            $("#txtbalance").val('');
            $("#txtmoney").val('');           
            arrUser1 = [];
            isWait1 = false;
            focusOnInputID2();
        }

    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <style type="text/css">
       
        table.tableSection {
            display: table;
            width: 100%;
            /*  font-size: 24px;*/
        }

            table.tableSection thead,
            table.tableSection tbody {
                float: left;
                width: 100%;
            }

                table.tableSection thead th {
                    vertical-align: top;
                }

            table.tableSection tbody {
                overflow-y: scroll;
                /* Giving height to make the tbody scroll */
                /* Giving height dynamically is recommended */
                height: 460px;
            }

            table.tableSection tr {
                width: 100%;
                display: table;
                /* Keeping the texts of both thead and tbody in same alignment */
                text-align: left;
                height: 65px !important;
            }

            table.tableSection th,
            table.tableSection td {
                width: 25%;
            }

            table.tableSection thead {
                padding-right: 18px;
                /* 18px is approx. value of width of scroll bar */
                /*width: calc(100% - 20px);*/
            }

        .select2-results__message {
            display: none;
        }

        .modal-body .select2-container {
            width: 450px !important;
        }

        .modal-body input.select2-search__field {
            width: 450px !important;
        }

            .modal-body input.select2-search__field[placeholder=''] {
                width: 0.75em !important;
            }

        .select2-search__field {
            padding: 0 6px !important;
        }
        /*.select2-container,.select2-selection,.select2-selection__rendered{
               max-height:40px !important;
           }*/
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release" />

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M600001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701068") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701068") %>
            </p>
        </div>
    </div>

    <div class="employeeList row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">search</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                </div>
                <div class="card-body">

                    <div class="row">
                        <div class="col-sm-8">
                            <div class="row student">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-2 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602003") %></label>
                                <div class="col-sm-8 ">
                                    <div class="input-group">
                                        <%-- <select id="mySelect2" class="form-control" multiple="multiple" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602004") %>"></select>--%>
                                        <input id="SearchUser1" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602004") %>" style=""
                                            data-url="/App_Logic/autoCompleteName.ashx?mode=TopUp"
                                            autocomplete="off"
                                            data-noresults-text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102035") %>">
                                        <%--<div class="input-group-append">
                                            <span class="input-group-text"><i class="material-icons" style="font-size: 1.55rem;">search</i></span>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="padding: 14px 0;">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-2 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602009") %></label>
                                <div class="col-sm-8">
                                    <input type="text" class='form-control' readonly="readonly" id="txtbalance" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-2 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602005") %></label>
                                <div class="col-sm-8">
                                    <input type="text" class='form-control' id="txtmoney" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="col-sm-12 text-right">
                                <img id="imgProfile" src="" style="width: 130px;" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-8">
                            <input type="button" class="btn btn-info col-lg-12" id="btnsubmit" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701068") %>" />
                        </div>
                        <div class="col-sm-4">
                            <input type="button" class="btn btn-danger col-lg-12" value="Clear" onclick="ClearData()" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- end row -->

    <div class="employeeList row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">account_circle</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602006") %></h4>
                </div>
                <div class="card-body">
                    <div class="toolbar">
                        <!-- Here you can write extra buttons/actions for the toolbar -->
                    </div>
                    <div class="material-datatables">
                        <div id="datatables_wrapper" class="dataTables_wrapper dt-bootstrap4">
                            <div class="row">
                                <div class="col-sm-12">
                                    <table id="topuptable" class="table table-hover">
                                        <thead>
                                            <tr>
                                                <th style="width: 25%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602007") %></th>
                                                <th style="width: 25%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602008") %></th>
                                                <th style="width: 16%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602015") %></th>
                                                <th style="width: 16%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106151") %></th>
                                                <th style="width: 27%" class="center">
                                                    <div id="btncancel" class="btn btn-warning" onclick="ClearHistroyList()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602010") %></div>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->

    <div class="modal fade" role="dialog" id="modal-invoice-payment" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog  modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="card-header card-header-info card-header-icon modal-header">
                    <%-- <div class="card-icon">
                        <i class="material-icons">search</i>                          
                    </div>--%>
                    <h4 class="card-title "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602012") %></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <label class="col-sm-2 col-form-label text-left" id="lablesearch">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602003") %></label>
                        <div class="col-sm-9">

                            <div class="input-group">

                                <input id="SearchUser2" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602004") %>" style=""
                                    data-url="/App_Logic/autoCompleteName.ashx?mode=TopUp"
                                    autocomplete="off"
                                    data-noresults-text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102035") %>">
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <label class="col-sm-12 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602014") %></label>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <table id="HistroyTopup" class="table table-hover tableSection">
                                <thead>
                                    <tr>
                                        <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602008") %></th>
                                        <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602015") %></th>
                                        <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602007") %></th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody id="table-body">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-sm-10"></div>
                    <div class="btn btn-primary col-sm-2" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
