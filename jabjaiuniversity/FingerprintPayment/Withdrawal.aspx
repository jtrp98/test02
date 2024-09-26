<%@ Page Title="" Language="C#" MasterPageFile="~/Material.Master" AutoEventWireup="true"
    CodeBehind="Withdrawal.aspx.cs" Inherits="FingerprintPayment.Withdrawal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
<%--    <link href="Content/select2/select2.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }

        @media (min-width: 1300px) {
            #page-wrapper {
                position: inherit;
                margin: 0 0 0 250px;
                padding: 0 30px;
                border-left: 1px solid #e7e7e7;
                background-color: #eee;
                padding-top: 30px;
                padding-bottom: 30px;
                min-height: 900px;
            }
        }

        table.tableSection {
            display: table;
            width: 100%;
            font-size: 24px;
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

         .select2-search__field {
            padding: 0 6px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content10" ContentPlaceHolderID="Script" runat="server">
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>
<%--    <script src="Scripts/select2/select2.js"></script>--%>
    <script type="text/javascript">

        var keyEnter = jQuery.Event("keydown");
        keyEnter.which = 13;
        keyEnter.keyCode = 13;

        var selectedUser1 = null;
        var availableValueEmployees = [];
        var arrUser1;
        var isWait = false;
        $.ajaxSetup({ cache: true });

        $(function () {

            //$("#btncancel").click(function () {
            //    $("#modal-invoice-payment").modal("show");
            //});

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
                    $("#btnsubmit").click();
                }
            });

            $("#btnsearch").click(function () {
                SearchHistroy();
            });

            $('#SearchUser1').autoComplete({
                //resolver: 'custom',
                minLength: 2,
                resolverSettings: {
                    //fail: function () {
                    //    console.log('fail');
                    //},
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
                       
                        if (isWait) {
                            $('#SearchUser1').trigger(keyEnter);
                            isWait = false;
                        }
                        return resultFromServer;
                    }
                }   
            }).on('autocomplete.select', function (evt, item) {
                //console.log(item);
                //console.log("select");
                selectedUser1 = item;
                getbalance(selectedUser1.User_Id, selectedUser1.User_Type);
                arrUser1 = [];
                isWait = false;

            }).on('keydown', function (e) {
                //console.log("keydown");
                if (e.keyCode == 13) {
                    if (arrUser1 != null &&  arrUser1.length > 0) {
                        $('#SearchUser1').autoComplete('set', arrUser1[0]);
                        selectedUser1 = arrUser1[0];
                        getbalance(selectedUser1.User_Id, selectedUser1.User_Type);
                        arrUser1 = [];
                        isWait = false;
                    }
                    else {
                        isWait = true;
                    }
                }
            }); 

            $("#btnsubmit").click(function () {
                $("body").mLoading();
                //var studentid = $("#txtid").val();
                var money = $("#txtmoney").val();
                //var name = $("#txtname").val();

                if (money === "") {                   
                    $.confirm({
                        title: false,
                        content: '<div class="text-center"><img src="images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><h3 class="text-danger text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602023") %></h3></div>',
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
                    $("body").mLoading('hide');
                }
                else if (money > parseFloat($("#txtbalance").val())) {
                  
                    $.confirm({
                        title: false,
                        content: '<div class="text-center"><img src="images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><h3 class="text-danger text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602024") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602025") %></h3></div>',
                        theme: 'material',
                        columnClass: 'col-md-5 col-md-offset-4',
                        type: 'red',
                        buttons: {
                            "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                keys: ['enter', 'shift'],
                                btnClass: 'btn-primary',
                                action: function () {
                                    $("#txtmoney").focus();
                                }
                            }
                        }
                    });
                    $("body").mLoading('hide');
                } else {
                    if (selectedUser1) {
                        PageMethods.WithdrawalMoney({
                            "Money": money,
                            "User_id": selectedUser1.User_Id,
                            "User_type": selectedUser1.User_Type,
                            "Name": selectedUser1.User_Name
                        },
                            function (response) {

                                if (response.success) {
                                    $.confirm({
                                        title: false,
                                        content: '<div class="text-center"><img src="images/checked.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h3 class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602032") %></h3></div>',
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
                                        }
                                    });
                                } else {
                                    var messageAlert = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602028") %>";

                                    $.confirm({
                                        title: false,
                                        content: '<div class="text-center"><img src="images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><h3 class="text-danger text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602027") %><br />' + messageAlert + '</h3></div>',
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


                            }
                        )

                        $("#txtbalance").val("");
                        $("#txtmoney").val("");
                    }
                }

            });

            $('#SearchUser1').focus();
        });

        function focusOnInputID2() {
            selectedUser1 = null;
            arrUser1 = [];
            isWait = false;
            $('#SearchUser1').autoComplete('clear');
            $('#imgProfile').attr('src', '');
            setTimeout(function () {
                $('#SearchUser1').focus();
            }, 400);
        }

        function getbalance(userid, usertype ) {
            $("body").mLoading();

            if (usertype == '2') {
                $.confirm({
                    title: false,
                    content: '<div class="text-center"><img src="images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><h3 class="text-danger text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132145") %></h3></div>',
                    theme: 'material',
                    columnClass: 'col-md-5 col-md-offset-4',
                    type: 'red',
                    buttons: {
                        "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                            keys: ['enter', 'shift'],
                            btnClass: 'btn-primary',
                            action: function () {
                                focusOnInputID2();
                                $("body").mLoading('hide')
                            }
                        }
                    }
                });
            }
            else {
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
        }

   
        $results = "";
        var rowsIndex = 1;
        function getHistroy() {
            $("body").mLoading();
            PageMethods.SearchHistoryTopups(selectedUser1.User_Id, selectedUser1.User_Type, "", function (result) {
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

        function getTopuphistory() {
            PageMethods.Historytopups(function (response) {
                $("#topuptable tbody tr").remove();
                $.each(response, function (Key, Values) {
                    $("#topuptable tbody").append("<tr><td>" + Values.time + "<td>" + Values.name + "<td>" + Values.money + "<td>" + Values.balannce);
                }); 
                focusOnInputID2();
            }, function () { });
        }

        function toupupcancel(topup_id, name, loadingdata) {
            console.log(topup_id);
            $("body").mLoading();
            PageMethods.TopupMoney_Cancel(topup_id, name, function (response) {
                $("body").mLoading('hide');
                if (response === "success") {
                    $.confirm({
                        title: false,
                        content: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132146") %></h3>',
                        theme: 'material',
                        columnClass: 'col-md-5 col-md-offset-4',
                        type: 'blue',
                        buttons: {
                            "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": {
                                keys: ['enter', 'shift'],
                                btnClass: 'btn-primary',
                                action: function () {
                                    if (loadingdata === 1) getTopuphistory();
                                    else SearchHistroy();
                                }
                            }
                        }
                    });
                } else {
                    var messageAlert = "";
                    if (response == "fail") messageAlert = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132147") %>";
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
                                }
                            }
                        }
                    });
                }
                console.log(response);

            }, function () { });
        }


        function SearchHistroy() {
            /*user_id = $("#txtstudent_id").val();*/
           /* topup_date = "";*/
            rowsIndex = 1;
           /* user_type = $("#select-type").val();*/
            $(".tableSection tbody tr").remove();
            $(".tableSection tbody").scrollTop(0);
            getHistroy();
        }

       
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"  ScriptMode="Release" />


    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M600001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701068") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602021") %>
            </p>
        </div>
    </div>
    <div class="row">
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
                                <div class="col-sm-8">
                                
                                     <div class="input-group">                                     
                                        <input id="SearchUser1" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602004") %>" style=""
                                            data-url="/App_Logic/autoCompleteName.ashx?mode=TopUp"
                                            autocomplete="off"
                                            data-noresults-text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102035") %>">                                   
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
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602026") %> :</label>
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
                        <div class="form-group col-sm-12">
                            <input type="button" class="btn btn-info col-lg-12" id="btnsubmit" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602021") %>" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">account_circle</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602029") %></h4>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="form-group col-sm-12">
                            <table id="topuptable" class="table table-hover">
                                <thead>
                                    <tr>
                                        <th style="width: 25%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602030") %></th>
                                        <th style="width: 25%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602031") %></th>
                                        <th style="width: 16%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602026") %></th>
                                        <th style="width: 16%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106151") %></th>
                                      
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
    </div>
    <%--  <div class="report-container">
    </div>--%>

  <%--  <div class="modal fade" role="dialog" id="modal-invoice-payment" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog  modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title">
                        <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132148") %>                               
                        </b>
                    </h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <label class="col-md-2 col-sm-6 control-label text-left">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405034") %> :</label>
                        <div class="col-md-4">
                            <select class="form-control" style="font-size: 26px;" id="select-type">
                                <option value="0" selected="selected"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %> - <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102166") %></option>
                            </select>
                        </div>
                    </div>
                    <div class="row ">
                        <label class="col-md-2 col-sm-6 control-label text-left" id="lablesearch">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603047") %></label>
                        <div class="col-md-4 col-sm-7">
                            <input type="text" class='form-control' id="txtstudent_id" style="display: none;" />
                            <input type="text" class='form-control search' style="font-size: 26px;" id="txtstudent_name" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101340") %>"   />
                            <input type="text" class='form-control search' style="font-size: 26px; display: none;" id="txtemployees_name" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105008") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %>/Username"   />
                        </div>
                        <div class="col-md-1">
                            <div class="btn btn-primary" id="btnsearch"><i class="glyphicon glyphicon-search"></i>&nbsp; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602014") %>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <table id="HistroyTopup" class="table table-hover tableSection">
                                <thead>
                                    <tr>
                                        <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602031") %></th>
                                        <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132167") %></th>
                                        <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602030") %></th>
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
                    <div class="col-lg-10"></div>
                    <div class="btn btn-primary col-lg-2" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></div>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>
