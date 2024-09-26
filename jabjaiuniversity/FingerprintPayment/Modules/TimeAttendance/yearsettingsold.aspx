<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="yearsettingsold.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.yearsettingsold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        ul.ui-autocomplete
        {
            z-index: 10000;
        }
        .centerText
        {
            text-align: center;
        }
        .setmin-width0
        {
            min-width: 0px !important;
        }
        .setfont-size14
        {
            font-size: 14px !important;
        }
    </style>
    <div id="btnnew" style="text-align: right" runat="server">
        <button type="button" id="btn-model" class="btn btn-success glyphicon glyphicon-plus font20"
            data-toggle="modal" data-target="#myModal">
            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132687") %></button>
    </div>
    <div style="margin-top: 30px; background: white;">
        <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
            <Columns>
                <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="numberYear" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>">
                </asp:BoundColumn>
                <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="YearStatus" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>">
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderStyle-CssClass="header-tb-color widthmax300-px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101022") %>">
                    <ItemTemplate>
                        <p style="margin-left: 0px;">
                            <asp:LinkButton CssClass="btn btn-primary width200-px glyphicon glyphicon-list font20 btnYear"
                                ID="btnManage" runat="server" CommandName="Manage">&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131228") %></asp:LinkButton>
                            <asp:LinkButton CssClass="btn btn-success width200-px glyphicon glyphicon-user font20 btnStd"
                                ID="btnStdManage" runat="server" CommandName="Student">&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132704") %></asp:LinkButton>
                            <asp:LinkButton CssClass="btn btn-warning width200-px glyphicon glyphicon-education font20 btnCongrate"
                                ID="btnCongrate" runat="server" CommandName="Year">&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132688") %></asp:LinkButton>
                            <asp:Panel runat="server" ID="pnlNull" CssClass="centerText">
                                -
                            </asp:Panel>
                        </p>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content width900-px" style="margin-left: -200px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132689") %>
                        <label>
                            <%=DateTime.Now.Year.ToString() %></label></h4>
                </div>
                <div class="modal-body" style="max-height: 470px; overflow: scroll;">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default font20 setmin-width0 setfont-size14"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    <button type="button" id="yearnew" class="btn btn-primary font20 setmin-width0 setfont-size14">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalYear" tabindex="-1" role="dialog" data-focus-on="input:first"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content width1000-px" style="margin-left: -200px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H1">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132690") %>
                        <label id="lblYear">
                            2558</label>
                        <label id="lblTerm">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label><label id="lblNumberTerm"></label></h4>
                </div>
                <div class="modal-body" style="max-height: 470px;">
                    <div class="modal-body2" style="max-height: 470px; overflow: scroll;">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default font20 btnEndTerm setmin-width0 setfont-size14"
                        onclick='if(window.confirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132691") %> \n <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132692") %>")){newTerm();}'>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132693") %></button>
                    <button type="button" class="btn btn-danger font20 setmin-width0 setfont-size14"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="Studentmodal" tabindex="-1" role="dialog" data-focus-on="input:first"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-backdrop fade in">
        </div>
        <div class="modal-dialog" style="z-index: 1060;">
            <div class="modal-content width500-px" style="margin-left: -30px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H6">
                        Title</h4>
                </div>
                <div class="modal-bodystackstd" style="max-height: 430px; overflow-y: scroll;">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-warning font20 glyphicon glyphicon-cog btnstdlist setmin-width0 setfont-size14"
                        termsublv2="-1" data-toggle="modal" data-target="#stack2" onclick="return false;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132705") %></button>
                    <button type="button" class="btn btn-danger font20 setmin-width0 setfont-size14"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalCongrate" tabindex="-1" role="dialog" data-focus-on="input:first"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-backdrop fade in">
        </div>
        <div class="modal-dialog" style="z-index: 1060;">
            <div class="modal-content width500-px" style="margin-left: -30px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H7">
                        Title</h4>
                </div>
                <div class="modal-bodystackclose" style="max-height: 430px; overflow-y: scroll;">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success font20 setmin-width0 setfont-size14"
                        onclick="if(window.confirm('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132706") %>')) submitYear();">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>
                    <button type="button" class="btn btn-default font20 setmin-width0 setfont-size14"
                        onclick="if(window.confirm('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132707") %>')) submitAllStd();">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132694") %></button>
                    <button type="button" class="btn btn-danger font20 setmin-width0 setfont-size14"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="stack2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-backdrop fade in">
        </div>
        <div class="modal-dialog" style="z-index: 1060;">
            <div class="modal-content" style="margin-left: -20%; width: 850px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H2">
                        Title</h4>
                </div>
                <div class="modal-bodystack" style="max-height: 470px; overflow-y: scroll;">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger font20 setmin-width0 setfont-size14"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="stack3" tabindex="-1" role="dialog" data-focus-on="input:first"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-backdrop fade in">
        </div>
        <div class="modal-dialog" style="z-index: 1070;">
            <div class="modal-content width500-px" style="margin-left: -30px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H3">
                        Title</h4>
                </div>
                <div class="modal-bodystack3" style="max-height: 470px;">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger font20 setmin-width0 setfont-size14"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="stack4" tabindex="-1" role="dialog" data-focus-on="input:first"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-backdrop fade in">
        </div>
        <div class="modal-dialog" style="z-index: 1080;">
            <div class="modal-content width900-px" style="margin-left: -200px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H4">
                        Title</h4>
                </div>
                <div class="modal-bodystack4" style="max-height: 470px;">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger font20 setmin-width0 setfont-size14"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="stack5" tabindex="-1" role="dialog" data-focus-on="input:first"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-backdrop fade in">
        </div>
        <div class="modal-dialog" style="z-index: 1090;">
            <div class="modal-content width900-px" style="margin-left: -200px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H5">
                        Title</h4>
                </div>
                <div class="modal-bodystack5" style="max-height: 470px; overflow: scroll;">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger font20 setmin-width0 setfont-size14"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var dataInsert;
        var str;
        var availableTags = [""];
        var availableValue = [""];
        var availableTagsT = [""];
        var availableValueT = [""];
        var Nos;
        var staticPlane;
        var staticTermlv;
        var ddl_lv1Item;
        var ddl_sublv1Item;
        var ddl_sublv2Item;
        var ddl_class;
        $(function () {
            $.ajax({ cache: false });
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=itemlv",
                cache: false,
                success: function (msg) {
                    var objjson = $.parseJSON(msg);

                    $.each(objjson, function (index) {
                        ddl_lv1Item += '<option value="' + objjson[index].LevelValue + '">' + objjson[index].LevelName + '</option>';
                    });
                }
            });

            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=itemclass",
                cache: false,
                success: function (msg) {
                    var objjson = $.parseJSON(msg);
                    console.log(objjson);
                    $.each(objjson, function (index) {
                        ddl_class += '<option value="' + objjson[index].sClassID + '">' + objjson[index].sClass + '</option>';
                    });
                }
            });

            $("#yearnew").click(function () {

                if (ajaxdoPostback()) {
                    $.ajax({
                        url: "/App_Logic/insertJSON.aspx?mode=termyear",
                        type: "post",
                        data: dataInsert,
                        success: function (resp) {

                            if (resp == "success") {
                                $("#myModal").hide();
                                alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132708") %>");
                                window.location.reload(true);
                            } else {
                                alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132564") %>");
                            }
                            //  window.location.reload(true);
                        }
                    });
                }

            });


            $("#btn-model").click(function () {
                $(".modal-body").html('<div style="text-align:center"><img src="/images/loading.gif" /></div>');
                $.ajax({
                    url: "/App_Logic/modalJSON.aspx?mode=termyear",
                    success: function (msg) {
                        var objjson = $.parseJSON(msg);
                        var strHtml = '<ul id="myTab" class="nav nav-tabs">';
                        var tabHTML = '';
                        var i = 1;

                        //   strHtml += '';
                        $.each(objjson, function (index) {

                            if (index % 3 == 0) {
                                strHtml += ' <li ';

                                if (index == 0) {
                                    strHtml += ' class="active"';
                                    tabHTML += '<div id="myTabContent" class="tab-content">';
                                    tabHTML += '<div class="tab-pane fade in active" id="tab' + i + '" style="background:white;">';
                                }
                                else {
                                    tabHTML += '<div class="tab-pane fade" id="tab' + i + '" style="background:white;">';
                                }
                                strHtml += '><a href="#tab' + i + '" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132194") %> ' + i + '</a></li>';

                                tabHTML += '<table class="table table-striped"><tr><td style="width:70%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></td><td style="width:30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132695") %></td></tr>';
                                tabHTML += '<tr><td colspan="2" style="background:#FF8A65;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132194") %> ' + i + '</td></tr>';
                                i += 1;
                            }

                            tabHTML += '<tr><td>' + objjson[index].SubLevel + '</td><td><input type="number" min="1" max="20" sublv="' + objjson[index].nTSubLevel + '" termsub="yes" class="form-control termsub" id="txt' + index + '" style="width:70px;height:39px;" required /></td></tr>';
                            if (index % 3 == 2) {
                                tabHTML += "</table></div>";
                            }
                        });
                        strHtml += '</ul>';
                        tabHTML += "</div>";
                        //   console.log(tabHTML);
                        $(".modal-body").html(strHtml + tabHTML);
                    }
                });

            });
        });

        function submitYear() {
            $.ajax({
                url: "/App_Logic/insertJSON.aspx?mode=congrateYear",
                cache: false,
                success: function (msg) {
                    if (msg == "success") {
                        window.location.reload();
                    } else { alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132709") %>"); }
                }
            });
        }

        function submitAllStd() {
            $.ajax({
                url: "/App_Logic/insertJSON.aspx?mode=setallnewlv",
                cache: false,
                success: function (msg) {
                    if (msg == "success") {
                        window.location.reload();
                    } else { alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132709") %>"); }
                }
            });
        }

        function chooseLv1(lv1Data, stddata) {
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=itemsublv1&lv1=" + lv1Data,
                cache: false,
                success: function (msg) {
                    var objjson = $.parseJSON(msg);
                    $(".ddl_sublv1[stddata='" + stddata + "']").find('option').remove();
                    $(".ddl_sublv1[stddata='" + stddata + "']").removeAttr("disabled");
                    $(".ddl_sublv1[stddata='" + stddata + "']").append('<option value="not"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %></option>');
                    $.each(objjson, function (index) {
                        $(".ddl_sublv1[stddata='" + stddata + "']").append('<option value="' + objjson[index].SubLevelValue + '">' + objjson[index].SubLevelName + '</option>');
                    });
                    $(".ddl_sublv1").change(function () {
                        if ($(this).val() != "not") {
                            chooseLv2($(this).val(), $(this).attr("stddata"));
                            $(".buttonSetStd[stddata='" + $(this).attr("stddata") + "']").attr("disabled", "disabled");
                        }
                        else {
                            $(".ddl_sublv2[stddata='" + $(this).attr("stddata") + "']").find('option').remove();
                            $(".ddl_sublv2[stddata='" + $(this).attr("stddata") + "']").append('<option value="not"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %></option>');
                            $(".ddl_sublv2[stddata='" + $(this).attr("stddata") + "']").attr("disabled", "disabled");
                        }
                    });
                }
            });
        }

        function removeTR(stdid) {
            $("tr[trstd='" + stdid + "']").remove();
        }

        function removeTRC(stdid) {
            $("tr[trstdc='" + stdid + "']").remove();
        }

        function chooseLv2(lv2Data, stddata) {
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=itemsublv2&sublv1=" + lv2Data,
                cache: false,
                success: function (msg) {
                    var objjson = $.parseJSON(msg);
                    $(".ddl_sublv2[stddata='" + stddata + "']").find('option').remove();
                    $(".ddl_sublv2[stddata='" + stddata + "']").removeAttr("disabled");
                    $.each(objjson, function (index) {
                        $(".ddl_sublv2[stddata='" + stddata + "']").append('<option value="' + objjson[index].SubLevel2Value + '">' + objjson[index].SubLevel2Name + '</option>');
                    });
                    $(".buttonSetStd[stddata='" + stddata + "']").removeAttr("disabled");
                    $(".buttonSetStd[stddata='" + stddata + "']").click(function () {
                        var trSTD = $(this).attr("stddata");
                        $.ajax({
                            url: "/App_Logic/insertJSON.aspx?mode=setstdlv&stdid=" + trSTD + "&sublv2=" + $(".ddl_sublv2[stddata='" + $(this).attr("stddata") + "']").val(),
                            success: function (resp) {
                                if (resp == "success") {
                                    removeTR(trSTD);
                                    $(".AlertStd").removeClass("alert-danger");
                                    $(".AlertStd").addClass("alert-success");
                                    $(".AlertStd").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132710") %>");
                                    $(".AlertStd").show();
                                    $(".AlertStd").fadeIn();
                                } else {
                                    $(".AlertStd").removeClass("alert-success");
                                    $(".AlertStd").addClass("alert-danger");
                                    $(".AlertStd").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132711") %>");
                                    $(".AlertStd").show();
                                    $(".AlertStd").fadeIn();
                                }
                                //setTimeout(function () { $(".AlertStd").fadeOut(); }, 1500);
                            }
                        });
                    });
                }
            });
        }

        function styleColor(daysn) {
            switch (daysn) {
                case 0: return "background:#F5D76E";
                case 1: return "background:#E26A6A";
                case 2: return "background:#2ECC71";
                case 3: return "background:#EB9532";
                case 4: return "background:#22A7F0";
                case 5: return "background:#BE90D4";
                case 6: return "background:#E74C3C";
            }
        }

        function generateSchedultTB(days, sNOtb) {
            var txtHTML = "";
            var objjson;
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=schegen&days=" + days + "&term=" + sNOtb,
                cache: false,
                success: function (resp) {

                    objjson = $.parseJSON(resp);

                    var date;
                    var formatted1, formatted2;
                    $.each(objjson, function (index) {
                        /* date = new Date(parseInt(objjson[index].time1.substr(6)));
                        formatted1 = date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2) + "-" + ("0" + date.getDate()).slice(-2) + " " + date.getHours() + ":" + date.getMinutes();
                        date = new Date(parseInt(objjson[index].time2.substr(6)));
                        formatted2 = date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2) + "-" + ("0" + date.getDate()).slice(-2) + " " + date.getHours() + ":" + date.getMinutes();
                        */
                        if (parseInt(objjson[index].time1H) < 10) {
                            formatted1 = "0" + objjson[index].time1H + ":";
                        } else {
                            formatted1 = objjson[index].time1H + ":";
                        }

                        if (parseInt(objjson[index].time1M) < 10) {
                            formatted1 += "0" + objjson[index].time1M + "";
                        }
                        else {
                            formatted1 += objjson[index].time1M + "";
                        }

                        //
                        if (parseInt(objjson[index].time2H) < 10) {
                            formatted2 = "0" + objjson[index].time2H + ":";
                        } else {
                            formatted2 = objjson[index].time2H + ":";
                        }

                        if (parseInt(objjson[index].time2M) < 10) {
                            formatted2 += "0" + objjson[index].time2M + "";
                        }
                        else {
                            formatted2 += objjson[index].time2M + "";
                        }




                        txtHTML += '<div class="col-xs-3">' + objjson[index].plane + '</hr></br>';
                        txtHTML += objjson[index].teacher + '</br>' + formatted1 + ' - ' + formatted2;
                        // txtHTML += objjson[index].teacher + '</br>' + formatted1.substr(11, 5) + ' - ' + formatted1.substr(11, 5);
                        txtHTML += '</div>';
                    });
                    $(".d" + days).append(txtHTML);
                }
            });

        }

        function newTerm() {

            $.ajax({
                url: "/App_Logic/insertJSON.aspx?mode=newterm",
                success: function (resp) {
                    if (resp == "success") {
                        window.location.reload();
                    }
                    else {
                        alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132564") %>");
                    }
                }
            });
        }

        function modalYear(currentYear, currentTerm) {
            $("#lblYear").text(currentYear);
            $("#lblNumberTerm").text("  " + currentTerm);
            $(".modal-body2").html('<div style="text-align:center"><img src="/images/loading.gif" /></div>');
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=yeartermset",
                cache: false,
                success: function (msg) {
                    var objjson = $.parseJSON(msg);
                    var strHtml = '';
                    var tabHTML = '';
                    var i = 1;
                    $.each(objjson, function (index) {
                        if (index == 0) {
                            strHtml += '<div class="panel-group"><div class="panel panel-primary"><div class="panel-heading"><a class="wt panel-collapse collapse in"';
                            strHtml += '  data-toggle="collapse" data-parent="#accordion' + index + '" href="#collapseOne' + index + '" style="color:white">' + objjson[index].Level + ' </a></div>';
                            strHtml += ' <div id="collapseOne' + index + '" class="panel-body collapse in"> <div class="accordion-inner max200px">  ';
                            strHtml += '<table class="table table-striped"><tr><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></th><th></th></tr><tr><td>' + objjson[index].SubLevel + "/" + objjson[index].SubLevel2 + '</td><td>' + buttonYear(objjson[index].Term) + '</td></tr>';
                        } else if ((index < objjson.length - 1) && (objjson[index].Level != objjson[index - 1].Level)) {
                            strHtml += "</table></div></div></div></div>";
                            strHtml += '<div class="panel-group"><div class="panel panel-primary"><div class="panel-heading"><a class="wt panel-collapse collapse in"';
                            strHtml += '  data-toggle="collapse" data-parent="#accordion' + index + '" href="#collapseOne' + index + '" style="color:white">' + objjson[index].Level + ' </a></div>';
                            strHtml += ' <div id="collapseOne' + index + '" class="panel-body collapse"> <div class="accordion-inner max200px"> ';
                            strHtml += '<table class="table table-striped"><tr><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></th><th></th></tr><tr><td>' + objjson[index].SubLevel + "/" + objjson[index].SubLevel2 + '</td><td>' + buttonYear(objjson[index].Term) + '</td></tr>';
                        }
                        else if (index == objjson.length - 1) {
                            strHtml += '<tr><td>' + objjson[index].SubLevel + "/" + objjson[index].SubLevel2 + '</td><td>' + buttonYear(objjson[index].Term) + '</td></tr>';
                            strHtml += "</table></div></div></div></div>";
                        }
                        else {
                            strHtml += '<tr><td>' + objjson[index].SubLevel + "/" + objjson[index].SubLevel2 + '</td><td>' + buttonYear(objjson[index].Term) + '</td></tr>';
                        }
                        i += 1;


                    });
                    $(".modal-body2").html(strHtml);

                    $(".btnwin").click(function () {
                        $(".modal-bodystack").html('<div style="text-align:center"><img src="/images/loading.gif" /></div>');
                        $("#H2").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132712") %>");
                        termPlaneSetting($(this).attr('termsublv2'));

                    });
                    $(".btnlist").click(function () {
                        $(".modal-bodystack").html('<div style="text-align:center"><img src="/images/loading.gif" /></div>');
                        $("#H2").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132195") %>");
                        termPlaneList($(this).attr('termsublv2'));

                    });
                    $(".btnsche").click(function () {
                        $(".modal-bodystack5").html('<div style="margin-top:30px;text-align:center;" class="loadingDiv"><img src="/images/loading.gif" /></div>');
                        $("#H5").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701014") %>");
                        var scheHTML = '<div class="col-xs-12" id="schedTB" style="display:none;">';
                        var countD = 0;
                        for (; countD <= 6; countD++) {
                            scheHTML += '<div class="row" style="border:solid 1px;min-height:105px"><div class="col-xs-12 d' + countD + '"><div class="col-xs-2 " style="' + styleColor(countD) + '">' + getPlaneDay(countD) + '';
                            scheHTML += '</div></div></div>';
                        }
                        scheHTML += '</div>';
                        $(".modal-bodystack5").append(scheHTML);
                        $("#schedTB").hide();

                        for (countD = 0; countD <= 6; countD++) {
                            generateSchedultTB(countD, $(this).attr("termsublv2"));
                            if (countD == 6) {
                                $("#schedTB").show();
                                $(".loadingDiv").remove();
                            }
                        }

                    });
                }
            });
        }

        function modalStudent() {
            $(".modal-bodystack").html('<div style="text-align:center"><img src="/images/loading.gif" /></div>');
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=yeartermset",
                cache: false,
                success: function (msgterm) {
                    var objjson = $.parseJSON(msgterm);
                    var strHtml = '';
                    var tabHTML = '';
                    var i = 1;
                    if (objjson != []) {
                        $.each(objjson, function (index) {
                            if (index == 0) {
                                strHtml += '<div class="panel-group"><div class="panel panel-primary"><div class="panel-heading"><a class="wt panel-collapse collapse in"';
                                strHtml += '  data-toggle="collapse" data-parent="#accordion' + index + '" href="#collapseTermOne' + index + '" style="color:white">' + objjson[index].Level + ' </a></div>';
                                strHtml += ' <div id="collapseTermOne' + index + '" class="panel-body collapse in"> <div class="accordion-inner max200px">  ';
                                strHtml += '<table class="table table-striped"><tr><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></th><th></th></tr><tr><td>' + objjson[index].SubLevel + "/" + objjson[index].SubLevel2 + '</td><td>' + buttonStdTerm(objjson[index].Term) + '</td></tr>';
                            } else if ((index < objjson.length - 1) && (objjson[index].Level != objjson[index - 1].Level)) {
                                //  console.log(objjson[index].Level);
                                strHtml += "</table></div></div></div></div>";
                                strHtml += '<div class="panel-group"><div class="panel panel-primary"><div class="panel-heading"><a class="wt panel-collapse collapse in"';
                                strHtml += '  data-toggle="collapse" data-parent="#accordion' + index + '" href="#collapseTermOne' + index + '" style="color:white">' + objjson[index].Level + ' </a></div>';
                                strHtml += ' <div id="collapseTermOne' + index + '" class="panel-body collapse"> <div class="accordion-inner max200px"> ';
                                strHtml += '<table class="table table-striped"><tr><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></th><th></th></tr><tr><td>' + objjson[index].SubLevel + "/" + objjson[index].SubLevel2 + '</td><td>' + buttonStdTerm(objjson[index].Term) + '</td></tr>';
                            }
                            else if (index == objjson.length - 1) {
                                strHtml += '<tr><td>' + objjson[index].SubLevel + "/" + objjson[index].SubLevel2 + '</td><td>' + buttonStdTerm(objjson[index].Term) + '</td></tr>';
                                strHtml += "</table></div></div></div></div>";
                            }
                            else {
                                strHtml += '<tr><td>' + objjson[index].SubLevel + "/" + objjson[index].SubLevel2 + '</td><td>' + buttonStdTerm(objjson[index].Term) + '</td></tr>';
                            }
                            i += 1;

                        });
                        $("#H6").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132696") %>");
                        $(".modal-bodystackstd").html(strHtml);
                        $(".btnstdlist").click(function () {
                            stdList($(this).attr("termsublv2"));
                        });

                    }

                }
            });
        }

        function modalCongrate() {
            $(".modal-bodystackclose").html('<div style="text-align:center"><img src="/images/loading.gif" /></div>');
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=yeartermset",
                cache: false,
                success: function (msgterm) {
                    var objjson = $.parseJSON(msgterm);
                    var strHtml = '';
                    var tabHTML = '';
                    var i = 1;
                    if (objjson != []) {
                        $.each(objjson, function (index) {
                            if (index == 0) {
                                strHtml += '<div class="panel-group"><div class="panel panel-primary"><div class="panel-heading"><a class="wt panel-collapse collapse in"';
                                strHtml += '  data-toggle="collapse" data-parent="#accordion' + index + '" href="#collapseCongrate' + index + '" style="color:white">' + objjson[index].Level + ' </a></div>';
                                strHtml += ' <div id="collapseCongrate' + index + '" class="panel-body collapse in"> <div class="accordion-inner max200px">  ';
                                strHtml += '<table class="table table-striped"><tr><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></th><th></th></tr><tr><td>' + objjson[index].SubLevel + "/" + objjson[index].SubLevel2 + '</td><td>' + buttonCloseYear(objjson[index].Term) + '</td></tr>';
                            } else if ((index < objjson.length - 1) && (objjson[index].Level != objjson[index - 1].Level)) {
                                //  console.log(objjson[index].Level);
                                strHtml += "</table></div></div></div></div>";
                                strHtml += '<div class="panel-group"><div class="panel panel-primary"><div class="panel-heading"><a class="wt panel-collapse collapse in"';
                                strHtml += '  data-toggle="collapse" data-parent="#accordion' + index + '" href="#collapseCongrate' + index + '" style="color:white">' + objjson[index].Level + ' </a></div>';
                                strHtml += ' <div id="collapseCongrate' + index + '" class="panel-body collapse"> <div class="accordion-inner max200px"> ';
                                strHtml += '<table class="table table-striped"><tr><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></th><th></th></tr><tr><td>' + objjson[index].SubLevel + "/" + objjson[index].SubLevel2 + '</td><td>' + buttonCloseYear(objjson[index].Term) + '</td></tr>';
                            }
                            else if (index == objjson.length - 1) {
                                strHtml += '<tr><td>' + objjson[index].SubLevel + "/" + objjson[index].SubLevel2 + '</td><td>' + buttonCloseYear(objjson[index].Term) + '</td></tr>';
                                strHtml += "</table></div></div></div></div>";
                            }
                            else {
                                strHtml += '<tr><td>' + objjson[index].SubLevel + "/" + objjson[index].SubLevel2 + '</td><td>' + buttonCloseYear(objjson[index].Term) + '</td></tr>';
                            }
                            i += 1;

                        });
                        $("#H7").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132713") %>");
                        $(".modal-bodystackclose").html(strHtml);
                        $(".btnstdlistclose").click(function () {
                            stdListCongrate($(this).attr("termsublv2"));
                        });

                    }

                }
            });
        }

        function stdListCongrate(termdata) {
            $("#H2").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104001") %>");
            $(".modal-bodystack").html('<div style="text-align:center"><img src="/images/loading.gif" /></div>');
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=btnStdManage&termsub=" + termdata,
                cache: false,
                success: function (msg) {
                    var objjson = $.parseJSON(msg);
                    var strHtml = '';
                    var tabHTML = '';
                    var i = 1;
                    var ddl_result;


                    if (objjson.length != 0) {
                        $.each(objjson, function (index) {
                            ddl_result = '<select name="ddl_result" class="form-control ddl_result" stddata="' + objjson[index].sID + '"><option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132714") %></option><option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132715") %></option><option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132716") %></option></select>';

                            if (index == 0) {
                                if (objjson[index].LevelData == -1) {
                                }
                                strHtml += '<div class="alert AlertStdC" role="alert" style="display:none;"></div><table class="table table-striped"><tr><th style="width:30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></th><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132717") %></th><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132718") %></th></tr>';
                                strHtml += '<tr trstdc="' + objjson[index].sID + '"><td>' + objjson[index].Name + ' ' + objjson[index].LastName + '</td><td>' + ddl_result + '</td><td><a class="btn btn-warning glyphicon glyphicon-cog buttonSetCon" stddata="' + objjson[index].sID + '" style="font-size: 16px !important;">&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132718") %></a></td></tr>';
                            }
                            else if (index == objjson.length - 1) {
                                strHtml += "</table>";
                            }
                        });
                    }
                    else {
                        strHtml += "<div style='text-align:center;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102035") %></div>";
                    }

                    $(".modal-bodystack").html(strHtml);
                    $(".buttonSetCon").click(function () {
                        var trSTD = $(this).attr("stddata");
                        var learndata = $(".ddl_result[stddata='" + trSTD + "']").val();
                        $.ajax({
                            url: "/App_Logic/insertJSON.aspx?mode=setstdnewlv&stdid=" + trSTD + "&sublv2=" + termdata + "&status=" + learndata,
                            cache: false,
                            success: function (resp) {
                                if (resp == "success") {
                                    removeTRC(trSTD);
                                    $(".AlertStdC").removeClass("alert-danger");
                                    $(".AlertStdC").addClass("alert-success");
                                    $(".AlertStdC").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132697") %>");
                                    $(".AlertStdC").show();
                                    $(".AlertStdC").fadeIn();
                                } else {
                                    $(".AlertStdC").removeClass("alert-success");
                                    $(".AlertStdC").addClass("alert-danger");
                                    $(".AlertStdC").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132711") %>");
                                    $(".AlertStdC").show();
                                    $(".AlertStdC").fadeIn();
                                }
                                //setTimeout(function () { $(".AlertStd").fadeOut(); }, 1500);
                            }
                        });
                    });
                }
            });
        }


        function stdList(termdata) {
            $("#H2").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104001") %>");
            $(".modal-bodystack").html('<div style="text-align:center"><img src="/images/loading.gif" /></div>');
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=btnStdManage&termsub=" + termdata,
                cache: false,
                success: function (msg) {
                    var objjson = $.parseJSON(msg);
                    var strHtml = '';
                    var tabHTML = '';
                    var i = 1;
                    var ddl_lv;
                    var ddl_sublv1;
                    var ddl_sublv2;
                    var arr_selected = [];
                    var arr_selected_where = [];
                    var count_selected = 0;
                    if (objjson.length != 0) {
                        $.each(objjson, function (index) {
                            ddl_lv = '<select name="ddl_lv" class="form-control ddl_lv" stddata="' + objjson[index].sID + '"><option value="not"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132719") %></option>' + ddl_lv1Item + '</select>';
                            ddl_sublv1 = '<select name="ddl_sublv1" class="form-control ddl_sublv1" stddata="' + objjson[index].sID + '" disabled><option><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132719") %></option></select>';
                            ddl_sublv2 = '<select name="ddl_sublv2" class="form-control ddl_sublv2" stddata="' + objjson[index].sID + '" disabled><option><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %></option></select>';
                            if (objjson[index].LevelData == 0 && objjson[index].TSubLevel != null && objjson[index].TSubLevel > 0) {
                                arr_selected[count_selected] = objjson[index].sID;
                                arr_selected_where[count_selected] = objjson[index].TSubLevel;
                                count_selected++;
                            }
                            if (index == 0) {
                                if (objjson[index].LevelData == -1) {
                                }
                                strHtml += '<div class="alert AlertStd" role="alert" style="display:none;"></div><table class="table table-striped"><tr><th style="width:25%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></th><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132720") %></th><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></th><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132721") %></th><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132722") %></th></tr>';
                                strHtml += '<tr trstd="' + objjson[index].sID + '"><td>' + objjson[index].Name + ' ' + objjson[index].LastName + '</td><td>' + ddl_lv + '</td><td>' + ddl_sublv1 + '</td><td>' + ddl_sublv2 + '</td><td><a class="btn btn-success glyphicon glyphicon-cog buttonSetStd" stddata="' + objjson[index].sID + '" style="font-size: 16px !important;" disabled>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132722") %></a></td></tr>';
                            }
                            else if (index == objjson.length - 1) {
                                strHtml += "</table>";
                            }
                        });
                    }
                    else {
                        strHtml += "<div style='text-align:center;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102035") %></div>";
                    }

                    $(".modal-bodystack").html(strHtml);

                    for (var x = 0; x < arr_selected.length; x++) {
                        var stddata_x = arr_selected[x];
                        $(".ddl_lv[stddata='" + stddata_x + "']").attr("disabled", "disabled");
                        $.ajax({
                            url: "/App_Logic/modalJSON.aspx?mode=thisTSub&sublv=" + arr_selected_where[x],
                            cache: false,
                            success: function (respCurrentT) {
                                var objjsonTSUB = $.parseJSON(respCurrentT);

                                $(".ddl_sublv1[stddata='" + stddata_x + "']").find('option').remove();
                                $.each(objjsonTSUB, function (index) {
                                    $(".ddl_sublv1[stddata='" + stddata_x + "']").append('<option value="' + objjsonTSUB[index].nTSubLevel + '">' + objjsonTSUB[index].SubLevel + '</option>');
                                    $(".ddl_sublv1[stddata='" + stddata_x + "']").attr("disabled", "disabled");
                                });
                                chooseLv2($(".ddl_sublv1[stddata='" + stddata_x + "']").val(), stddata_x);
                            }
                        });
                    }
                    $(".ddl_lv").change(function () {
                        if ($(this).val() != "not") {
                            chooseLv1($(this).val(), $(this).attr("stddata"));
                            $(".buttonSetStd[stddata='" + $(this).attr("stddata") + "']").attr("disabled", "disabled");
                        }
                        else {
                            $(".ddl_sublv1[stddata='" + $(this).attr("stddata") + "']").find('option').remove();
                            $(".ddl_sublv1[stddata='" + $(this).attr("stddata") + "']").append('<option value="not"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132719") %></option>');
                            $(".ddl_sublv1[stddata='" + $(this).attr("stddata") + "']").attr("disabled", "disabled");
                            $(".ddl_sublv2[stddata='" + $(this).attr("stddata") + "']").find('option').remove();
                            $(".ddl_sublv2[stddata='" + $(this).attr("stddata") + "']").append('<option value="not"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %></option>');
                            $(".ddl_sublv2[stddata='" + $(this).attr("stddata") + "']").attr("disabled", "disabled");
                        }
                    });
                }
            });
        }

        function termPlaneList(termsublv2) {
            str = termsublv2;
            $(".modal-bodystack").html('<div style="text-align:center"><img src="/images/loading.gif" /></div>');
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=planelist&sublv=" + str,
                cache: false,
                success: function (msg) {
                    var objjson = $.parseJSON(msg);
                    var strHtml = '<div style="text-align:center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102035") %></div>';
                    var i = 0;
                    if (objjson != []) {
                        $.each(objjson, function (index) {
                            if (index == 0) {
                                strHtml = "";
                                strHtml += '<table class="table table-striped"> <thead><tr><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %></><th></th></tr></thead><tbody>';
                                i++;
                            }
                            strHtml += '<tr trplanedata="' + objjson[index].sPlaneID + '"><td>' + objjson[index].sPlaneName + '</td><td>' + buttonPlane(objjson[index].sPlaneID, objjson[index].sNo) + '</td></tr>';
                        });
                        if (i > 0) {
                            strHtml += "</tbody></table>";
                        }
                    }
                    $(".modal-bodystack").html(strHtml);
                    $(".btnset").click(function () {
                        $("#H4").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132698") %>");
                        $(".modal-bodystack4").html('<div style="text-align:center"><img src="/images/loading.gif" /></div>');
                        planeSet($(this).attr("planeid"), $(this).attr("termsublv2"), $(this).attr("sNo"));

                    });
                    $(".btndltplane").click(function () {
                        if (window.confirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102158") %>")) {
                            var currentPlanelist = $(this).attr("planeid");
                            var currentTermSubLv2 = $(this).attr("termsublv2");
                            $.ajax({
                                url: "/App_Logic/deleteJSON.aspx?mode=delPlaneList&planeid=" + currentPlanelist + "&termsublv2=" + currentTermSubLv2,
                                success: function (resp) {
                                    if (resp == "success") {
                                        $("tr[trplanedata='" + currentPlanelist + "']").remove();
                                    }
                                    else {
                                        alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132564") %>");
                                    }
                                }
                            });
                        }
                        return false;
                    });

                }
            });
        }

        function daySelectSet() {
            var dayHTML = "";
            dayHTML += '<option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202007") %></option>';
            dayHTML += '<option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202008") %></option>';
            dayHTML += '<option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202009") %></option>';
            dayHTML += '<option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603009") %></option>';
            dayHTML += '<option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202011") %></option>';
            dayHTML += '<option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202012") %></option>';
            dayHTML += '<option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202013") %></option>';

            return dayHTML;
        }

        function planeSet(planid, termsublv2, sNodata) {
            $(".learnset").html('');
            Nos = sNodata;
            staticPlane = planid;
            staticTermlv = termsublv2;
            availableTags = [""];
            availableValue = [""];
            var setHTML = "";
            setHTML += '<div class="col-xs-12" >';
            setHTML += '<div class="col-xs-2"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %> :</label></div><div class="col-xs-3"><select id="selDay" class="form-control">' + daySelectSet() + '</select></div>';
            setHTML += '<div class="col-xs-2"> <label>ผู้สอน :</label></div><div class="col-xs-5"> <input class="autocom" id="teacher" name="teacher" /></div>';
            setHTML += '</div>';
            setHTML += '<div class="col-xs-12" style="margin-top:15px;"><div class="col-xs-2"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %> :</label></div><div class="col-xs-3"><select id="setClass" class="form-control"></select></div><div class="col-xs-7"> </div></div>';
            setHTML += '<div class="col-xs-12" style="margin-top:15px;"><div class="col-xs-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132686") %></div><div class="col-xs-3"><div class="input-group clockpicker" style="float:left;" data-placement="left" data-align="top" data-autoclose="true"><input type="text" class="form-control mon" id="monstart1" runat="server" style="height:38px;" /> <span class="input-group-addon"> <span class="glyphicon glyphicon-time"></span></span> </div></div>';
            setHTML += '<div class="col-xs-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701073") %></div><div class="col-xs-3"> <div class="input-group clockpicker" style="float:left;" data-placement="left" data-align="top" data-autoclose="true">';
            setHTML += '<input type="text" class="form-control mon" id="monstart2" runat="server"  style="height:38px;" /> <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span> </span></div></div>';
            setHTML += '<div class="col-xs-2" style="text-align:right"> <button type="button" class="btn btn-success font20 addLearn setmin-width0 setfont-size14" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132699") %></button></div></div>';
            setHTML += '<div class="col-xs-12 learnset" style="margin-top:20px;max-height:300px;"></div>';
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=teacher",
                success: function (msg) {

                    var i = 0;
                    var objjson = $.parseJSON(msg);
                    $.each(objjson, function (index) {
                        availableTagsT[i] = objjson[index].sName + " " + objjson[index].sLastname;
                        availableValueT[i] = objjson[index].sName + " " + objjson[index].sLastname;
                        i += 1;
                    });

                    learnList();
                    console.log(availableTagsT);
                    $("#teacher").autocomplete({
                        source: availableTagsT
                    });
                    $("#setClass").html('');
                    $("#setClass").html(ddl_class);
                    $(".addLearn").click(function () {
                        var time1 = $("#ctl00_MainContent_monstart1").val();
                        var time2 = $("#ctl00_MainContent_monstart2").val();

                        if ($("#teacher").val() == "") { alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132700") %>"); return false; }
                        if ($("#ctl00_MainContent_monstart1").val() == "" || $("#ctl00_MainContent_monstart2").val() == "") { alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132723") %>"); return false; }
                        time1 = time1.substr(0, 2);
                        time2 = time2.substr(0, 2);
                        if (time2 < time1) { alert("กรุณากรอกชั่วงเวลาให้ถูกต้อง"); return false; }
                        time1 = $("#ctl00_MainContent_monstart1").val() + ":00";
                        time2 = $("#ctl00_MainContent_monstart2").val() + ":00";
                        $.ajax({
                            url: "/App_Logic/insertJSON.aspx?mode=addlearn&sNo=" + Nos + "&days=" + $("#selDay").val() + "&plane=" + planid + "&sublv=" + termsublv2 + "&time1=" + time1 + "&time2=" + time2 + "&classroom=" + $("#setClass").val() + "&teacher=" + $("#teacher").val(),
                            type: "post",
                            cache: false,
                            success: function (msg) {
                                if (msg == "found") alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132701") %>");
                                else if (msg == "success") {
                                    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132505") %>");
                                    learnList();
                                }
                                else if (msg == "duplicate") alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132725") %>");
                                else alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132564") %>");
                            }
                        });
                        return false;
                    });
                }
            });

            $(".modal-bodystack4").html(setHTML);
            $('.clockpicker').clockpicker();
            return false;
        }

        function learnList() {
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=learnlist&sNo=" + Nos + "&term=" + staticTermlv + "&plane=" + staticPlane,
                cache: false,
                success: function (msg) {
                    var tbLearn = "";
                    var i = 0;
                    var objjson = $.parseJSON(msg);
                    var date;
                    var formatted1, formatted2, formatted3, formatted4;
                    console.log(objjson);

                    tbLearn += '<table class="table table-striped"><thead><tr><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202022") %></th><th>ผู้สอน</th><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></th><th>เวลาเข้าเรียน</th><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132702") %></th><th></th></tr></thead><tbody>';
                    $.each(objjson, function (index) {
                        /*  date = new Date(parseInt(objjson[index].dTimeStart_IN.substr(6)));
                        formatted1 = date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2) + "-" + ("0" + date.getDate()).slice(-2) + " " + date.getHours() + ":" + date.getMinutes();
                        date = new Date(parseInt(objjson[index].dTimeStart_OUT.substr(6)));
                        formatted2 = date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2) + "-" + ("0" + date.getDate()).slice(-2) + " " + date.getHours() + ":" + date.getMinutes();
                        date = new Date(parseInt(objjson[index].dTimeEnd_IN.substr(6)));
                        formatted3 = date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2) + "-" + ("0" + date.getDate()).slice(-2) + " " + date.getHours() + ":" + date.getMinutes();
                        date = new Date(parseInt(objjson[index].dTimeEnd_OUT.substr(6)));
                        formatted4 = date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2) + "-" + ("0" + date.getDate()).slice(-2) + " " + date.getHours() + ":" + date.getMinutes();
                        */
                        if (parseInt(objjson[index].dTimeStart_INH) < 10) {
                            formatted1 = "0" + objjson[index].dTimeStart_INH + ":";
                        } else {
                            formatted1 = objjson[index].dTimeStart_INH + ":";
                        }

                        if (parseInt(objjson[index].dTimeStart_INM) < 10) {
                            formatted1 += "0" + objjson[index].dTimeStart_INM + "";
                        }
                        else {
                            formatted1 += objjson[index].dTimeStart_INM + "";
                        }

                        //
                        if (parseInt(objjson[index].dTimeStart_OUTH) < 10) {
                            formatted2 = "0" + objjson[index].dTimeStart_OUTH + ":";
                        } else {
                            formatted2 = objjson[index].dTimeStart_OUTH + ":";
                        }

                        if (parseInt(objjson[index].dTimeStart_OUTM) < 10) {
                            formatted2 += "0" + objjson[index].dTimeStart_OUTM + "";
                        }
                        else {
                            formatted2 += objjson[index].dTimeStart_OUTM + "";
                        }

                        ///

                        if (parseInt(objjson[index].dTimeEnd_INH) < 10) {
                            formatted3 = "0" + objjson[index].dTimeEnd_INH + ":";
                        } else {
                            formatted3 = objjson[index].dTimeEnd_INH + ":";
                        }

                        if (parseInt(objjson[index].dTimeEnd_INM) < 10) {
                            formatted3 += "0" + objjson[index].dTimeEnd_INM + "";
                        }
                        else {
                            formatted3 += objjson[index].dTimeEnd_INM + "";
                        }

                        //
                        if (parseInt(objjson[index].dTimeEnd_OUTH) < 10) {
                            formatted4 = "0" + objjson[index].dTimeEnd_OUTH + ":";
                        } else {
                            formatted4 = objjson[index].dTimeEnd_OUTH + ":";
                        }

                        if (parseInt(objjson[index].dTimeEnd_OUTM) < 10) {
                            formatted4 += "0" + objjson[index].dTimeEnd_OUTM + "";
                        }
                        else {
                            formatted4 += objjson[index].dTimeEnd_OUTM + "";
                        }

                        tbLearn += '<tr trplanelist="' + objjson[index].learnID + '"><td>' + getPlaneDay(objjson[index].nPlaneDay) + '</td>';
                        tbLearn += '<td>' + objjson[index].nName + '</td>';
                        tbLearn += '<td>' + objjson[index].Class + '</td>';
                        tbLearn += ' <td>' + formatted1 + ' - ' + formatted2 + ' </td>';
                        tbLearn += '<td>' + formatted3 + ' - ' + formatted4 + '</td>';
                        //  tbLearn += ' <td>' + formatted1.substr(11, 5) + ' - ' + formatted2.substr(11, 5) + ' </td>';
                        //  tbLearn += '<td>' + formatted3.substr(11, 5) + ' - ' + formatted4.substr(11, 5) + '</td>';
                        tbLearn += '<td><button class="btn btn-danger btnScheListDel setmin-width0 setfont-size14" tempdata="' + objjson[index].learnID + '"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></button></td>';
                        tbLearn += '</tr>';
                    });
                    tbLearn += '</tbody></table>';
                    $(".learnset").html(tbLearn);
                    $(".btnScheListDel").click(function () {
                        if (window.confirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102158") %>")) {
                            var currentPlanelist = $(this).attr("tempdata");
                            $.ajax({
                                url: "/App_Logic/deleteJSON.aspx?mode=delScheduleList&temp=" + currentPlanelist,
                                success: function (resp) {
                                    if (resp == "success") {
                                        $("tr[trplanelist='" + currentPlanelist + "']").remove();
                                    }
                                    else {
                                        alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132564") %>");
                                    }
                                }
                            });
                        }
                        return false;
                    });
                }
            });
        }

        function getPlaneDay(nDays) {
            switch (nDays) {
                case 0: return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202007") %>";
                case 1: return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202008") %>";
                case 2: return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202009") %>";
                case 3: return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603009") %>";
                case 4: return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202011") %>";
                case 5: return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202012") %>";
                case 6: return "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202013") %>";
            }


        }

        function buttonPlane(rowsdata, rowNo) {
            var btnHTML = '';
            btnHTML += '<button type="button" sNo="' + rowNo + '" planeid="' + rowsdata + '" termsublv2="' + str + '"  class="btn btn-info font20 glyphicon glyphicon-info-sign btnset  setmin-width0 setfont-size14" style="float:left;margin-left:7px;" data-toggle="modal" data-target="#stack4" onclick="return false;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132726") %></button>';
            btnHTML += '<button type="button" sNo="' + rowNo + '" planeid="' + rowsdata + '" termsublv2="' + str + '" class="btn btn-danger font20 glyphicon glyphicon-remove btndltplane  setmin-width0 setfont-size14" style="float:left;margin-left:7px">ลบรายวิชา</button>';
            return btnHTML;
        }

        function termPlaneSetting(termsublv2) {
            str = termsublv2;
            availableTags = [""];
            availableValue = [""];
            var mainHtml = '  ';
            mainHtml += ' <label for="tags"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132727") %>: </label>';
            mainHtml += ' <input class="autocom" id="plane" name="plane" />';
            mainHtml += '   <button class="btn btn-success addplane"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132712") %></button>';


            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=plane",
                success: function (msg) {
                    $(".modal-bodystack").html(mainHtml);
                    var i = 0;
                    var objjson = $.parseJSON(msg);
                    $.each(objjson, function (index) {
                        availableTags[i] = objjson[index].sPlaneName;
                        availableValue[i] = objjson[index].sPlaneID;
                        i += 1;
                    });

                    $("#plane").autocomplete({
                        source: availableTags
                    });
                    $(".addplane").click(function () {
                        if ($("#plane").val() == "") { alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132703") %>"); return false; }
                        $.ajax({
                            url: "/App_Logic/insertJSON.aspx?mode=addplane&sublv=" + str,
                            type: "POST",
                            cache: false,
                            data: "plane=" + $("#plane").val(),
                            success: function (msg) {
                                if (msg == "found") alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132728") %>");
                                else if (msg == "success") alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132505") %>");
                                else if (msg == "duplicate") alert("เคยลงทะเบียนรายวิชานี้แล้ว");
                                else alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132564") %>");

                            }
                        });
                        return false;
                    });
                }
            });

        }

        function buttonStdTerm(rowsdata) {
            var btnHTML = '';
            btnHTML += '<button type="button" termsublv2="' + rowsdata + '"  class="btn btn-info  glyphicon glyphicon-info-sign btnstdlist  setmin-width0 setfont-size14" style="float:left;margin-left:7px;" data-toggle="modal" data-target="#stack2" onclick="return false;" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104001") %></button>';
            return btnHTML;
        }

        function buttonCloseYear(rowsdata) {
            var btnHTML = '';
            btnHTML += '<button type="button" termsublv2="' + rowsdata + '"  class="btn btn-info  glyphicon glyphicon-info-sign btnstdlistclose  setmin-width0 setfont-size14" style="float:left;margin-left:7px;" data-toggle="modal" data-target="#stack2" onclick="return false;" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104001") %></button>';
            return btnHTML;
        }

        function buttonYear(rowsdata) {
            var btnHTML = '';
            btnHTML += '<button type="button" termsublv2="' + rowsdata + '"  class="btn btn-info  glyphicon glyphicon-info-sign btnlist  setmin-width0 setfont-size14" style="float:left;margin-left:7px;" data-toggle="modal" data-target="#stack2" onclick="return false;" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132729") %></button>';
            btnHTML += '<button type="button" termsublv2="' + rowsdata + '" class="btn btn-success  glyphicon glyphicon-plus btnwin  setmin-width0 setfont-size14" style="float:left;margin-left:7px" data-toggle="modal" data-target="#stack2" onclick="return false;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132712") %></button>';
            btnHTML += '<button type="button" termsublv2="' + rowsdata + '" class="btn btn-warning  glyphicon glyphicon-calendar btnsche  setmin-width0 setfont-size14" style="float:left;margin-left:7px" data-toggle="modal" data-target="#stack5" onclick="return false;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701014") %></button>';

            return btnHTML;
        }

        function ajaxdoPostback() {
            dataInsert = "";
            var j = 0;
            $('input[termsub=yes]').each(function () {
                j++;
                if (j > 1) dataInsert += "&";
                dataInsert += "txt" + j + "=" + $(this).val() + "&sublv" + j + "=" + $(this).attr("sublv");
                if ($(this).val() == "" || $(this).val() == "0") {
                    $(this).focus();
                    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132730") %>");
                    return false;
                }
            });
            if (dataInsert == "") return false;
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
