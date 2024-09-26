<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="userlist2.aspx.cs" Inherits="FingerprintPayment.userlist2" EnableViewState="true" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>

    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="javascript/bootstrap-show-password/bootstrap-show-password.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>

    <style type="text/css">
        label {
            font-weight: normal;
            font-size: 26px;
        }

        .gvbutton {
            font-size: 25px;
        }

        .nounder a:hover {
            text-decoration: none;
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .boxhead a {
            color: #FFFFFF;
            text-decoration: none;
        }

        a.imjusttext {
            color: #ffffff;
            text-decoration: none;
        }

            a.imjusttext:hover {
                color: aquamarine;
            }

        .centerText {
            text-align: center;
        }

        .nowrap {
            max-width: 100%;
            white-space: nowrap;
        }

        .namemangin {
            margin-left: 5px;
            padding-left: 35px;
            border-left: 10px;
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }

        .tab {
            border-collapse: collapse;
            margin-left: 6px;
            margin-right: 6px;
            border-bottom: 3px solid #337AB7;
            border-left: 3px solid #337AB7;
            border-right: 3px solid #337AB7;
            border-top: 3px solid #337AB7;
            box-shadow: inset 0 1px 0 #337AB7;
        }

        /*loader spinner*/
        .spinner {
            background-color: #ffffff;
            opacity: .8;
            text-align: center;
            font-size: 10px;
            position: fixed;
            height: 100%;
            width: 100%;
            z-index: 5000;
            color: black;
            top: 0;
            left: 0;
            float: left;
            text-align: center;
            padding-top: 20%;
            display: none;
        }

            .spinner > div {
                background-color: #808080;
                opacity: 1.5;
                height: 50%;
                width: 6px;
                display: inline-block;
                -webkit-animation: sk-stretchdelay 1.2s infinite ease-in-out;
                animation: sk-stretchdelay 1.2s infinite ease-in-out;
            }

            .spinner .rect2 {
                -webkit-animation-delay: -1.1s;
                animation-delay: -1.1s;
            }

            .spinner .rect3 {
                -webkit-animation-delay: -1.0s;
                animation-delay: -1.0s;
            }

            .spinner .rect4 {
                -webkit-animation-delay: -0.9s;
                animation-delay: -0.9s;
            }

            .spinner .rect5 {
                -webkit-animation-delay: -0.8s;
                animation-delay: -0.8s;
            }

        @-webkit-keyframes sk-stretchdelay {
            0%, 40%, 100% {
                -webkit-transform: scaleY(0.4);
            }

            20% {
                -webkit-transform: scaleY(1.0);
            }
        }

        @keyframes sk-stretchdelay {
            0%, 40%, 100% {
                transform: scaleY(0.4);
                -webkit-transform: scaleY(0.4);
            }

            20% {
                transform: scaleY(1.0);
                -webkit-transform: scaleY(1.0);
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <script type="text/javascript" language="javascript">

        var availableValueplane = [];
        $(document).ready(function () {
            $('#password').password().password('focus').on('show.bs.password', function (e) {
                $('#eventLog').text('On show event');
            }).on('hide.bs.password', function (e) {
                $('#eventLog').text('On hide event');
            });

            $('#methods').click(function () {
                $('#password').password('toggle');
                $('#password').css('toggle');
            });

            $('a[id*=btnDel]').click(function () {
                var user_id = $(this).attr("user-data");
                var control_id = $(this).attr('id').replace(/\_/g, "$");
                PageMethods.getinvoice_status(user_id, function (result) {
                    if (result === false) {
                        alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131229") %>"); return false;
                    } else {
                        //alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131230") %>");
                        showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", control_id); return false;
                    }
                });
                ////showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('id').replace(/\_/g, "$")); return false;
            });

            $('select[id$=ddlSubLV]').val(getUrlParameter("idlv"));

            funtionListSubLV2M("ddlSubLV", "ddlSubLV2", getUrlParameter("idlv2"));
            availableValueplane = functionListstudentM("ddlSubLV", "ddlSubLV2");

            $('input[id*=txtSearch]').val(getUrlParameter("sname"));

            $("select[id*=ddlYear]").change(function () {
                getListTrem();
            });

            $('select[id$=ddlSubLV]').change(function () {
                funtionListSubLV2M("ddlSubLV", "ddlSubLV2");
                availableValueplane = functionListstudentM("ddlSubLV", "ddlSubLV2");
            });

            $('select[id*=ddlSubLV2]').change(function () {
                availableValueplane = functionListstudentM("ddlSubLV", "ddlSubLV2");
            });

            $('input[id*=txtSearch]').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                maxLength: 10,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                //source: function (request, response) {
                //    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
                //    response(results.slice(0, 10));
                //},
                source: lightwell,
                select: function (event, ui) {
                    event.preventDefault();
                    $("input[id*=txtSearch]").val(ui.item.label);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });


            $('input[id*=btnSearch]').click(function (e) {
                e.preventDefault();
                var param1var = $('select[id$=ddlSubLV] option:selected').val();
                var param2var = $('select[id*=ddlSubLV2] option:selected').val();
                var param3var = $('input[id*=txtSearch]').val();
                var param4var = $('select[id*=ddlTerm] option:selected').val();
                window.location.href = "Userlist2.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&sname=" + param3var + "&termId=" + param4var;
            });

            $('input[id*=txtSearch]').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                maxLength: 10,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                source: function (request, response) {
                    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("input[id*=txtSearch]").val(ui.item.label);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });

            $("#formpopup").validate({
                rules: {
                    newpassword: "required",
                    oldpassword: "required",
                    password_again: {
                        equalTo: "#newpassword"
                    }
                },
                messages: {
                    password_again: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121044") %>",
                    newpassword: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    oldpassword: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                },
                tooltip_options: {
                    password_again: { placement: 'right', trigger: 'focus' },
                    newpass: { placement: 'right', trigger: 'focus' },
                    oldpass: { placement: 'right', trigger: 'focus' }
                },
                submitHandler: function (e) {
                }
            });

            $("#modalpopup-data-cancel").click(function () {
                $("#modalpopup-data").modal("hide");
            });

            $("#modalpopup-data-submit").click(function (e) {
                e.preventDefault();
                if ($(this).html() === "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101229") %>") {
                    $("#changepassword").show();
                    $("#showpassword").hide();
                    $("#modalpopup-data-submit").prop("type", "submit");
                    $("#modalpopup-data-submit").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>");
                    $(this).addClass("btn-success");
                    $(this).removeClass("btn-primary");
                } else {
                    if ($("#formpopup").valid() === true) {
                        var data = {
                            "oldpass": $("#oldpassword").val(), "newpass": $("#newpassword").val(),
                            "user_id": $("#user_id").val()
                        };

                        PageMethods.changepassword(data,
                            function (response) {
                                if (response === "Success") {
                                    $("#modalpopup-data").modal("hide");
                                }
                                else {
                                    $(".message_response").show().fadeOut(1500);
                                }
                            },
                            function (response) {

                            });
                    }
                }
            });
        });

        function lightwell(request, response) {
            function hasMatch(s) {
                return s.toLowerCase().indexOf(request.term.toLowerCase()) !== -1;
            }
            var i, l, obj, matches = [];

            if (request.term === "") {
                response([]);
                return;
            }

            for (i = 0, l = availableValueplane.length; i < l; i++) {
                obj = availableValueplane[i];
                if (hasMatch(obj.label) || hasMatch(obj.code)) {
                    matches.push(obj);
                }
            }
            response(matches.slice(0, 10));
        }

        function getListTrem() {
            var YearID = $('select[id*=ddlYear] option:selected').val();
            $("select[id*=ddlTerm] option").remove();
            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listterm&id=" + YearID,
                success: function (msg) {
                    trem = msg;
                    $.each(msg, function (index) {
                        $('select[id*=ddlTerm]')
                            .append($("<option></option>")
                                .attr("value", msg[index].nTerm)
                                .text(msg[index].sTerm));
                    });
                }
            });

        }
        function changeFinger() {
            $.ajax("/App_Logic/deleteDataJSON.ashx?mode=delfinger&userid=" + $("#ctl00_MainContent_hdfsid").val() + "&type=0", function (Result) {
            }).done(function (Result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111048") %> " + Result);
                $("#modalpopupdatamac .modal-footer").addClass("hidden");
            });
        }

        function resetpin() {
            let user_id = $("#user_id").val();
            PageMethods.resettpin(user_id, function (response) {
                console.log(response);
                alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132160") %>");
            });
        }

        function ShowPopUP(userid, name) {
            $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>" +
                "เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
            $("#ctl00_MainContent_hdfsid").val(userid);
            $("#modalpopupdatamac .modal-footer").removeClass("hidden");
        }

        function ShowPopUPRegister(userid, name) {
            $.get("/App_Logic/dataJSon.ashx?mode=getpassword&userid=" + userid + "&type=0", "", function (result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121045") %> " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121046") %> <br/>" +
                    " <h1> " + result[0].password);
                $("#modalpopupdatamac .modal-footer").addClass("hidden");
            });
        }

        function Showpass(user_id) {
            $('.spinner').show();
            PageMethods.getpassword(user_id,
                function (response) {
                    $('.spinner').fadeOut();
                    $("#changepassword").hide();
                    $("#showpassword").show();
                    $("#changepassword input").val("");
                    $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101226") %>");
                    $("#modalpopup-data").modal("show");
                    $("#username").val(response.username);
                    $("#password").val(response.password);
                    $("#modalpopup-data-submit").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101229") %>");
                    $("#modalpopup-data-submit").prop("type", "buuton");
                    $("#user_id").val(user_id);
                    $("#modalpopup-data-submit").removeClass("btn-success");
                    $("#modalpopup-data-submit").addClass("btn-primary");
                    if ($('#password').css("display") === "none") {
                        $('#password').password('toggle');
                    }
                },
                function (response) {
                    console.log(response);
                });
        }


        function getLinkEdit(studentId) {
            window.open("/userlist2-edit.aspx?id=" + studentId + "&termId=" + $('select[id*=ddlTerm] option:selected').val(), "_blank");
        }
        //function comfirmDel(user_id, control_id) {
        //    PageMethods.getinvoice_status(user_id, function (result) {
        //        if (result === false) {
        //            alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131229") %>"); return false;
        //        } else {
        //            alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131230") %>");
        //            //showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(control_id).attr('id').replace(/\_/g, "$")); return false;
        //        }
        //    });
        //}

        function funtionListSubLV2M(controlsublevel, sontrolsublevel2) {
            var SubLVID = $('select[id$=' + controlsublevel + '] option:selected').val();
            var sSubLV = $('select[id$=' + controlsublevel + '] option:selected').text();
            $("#" + sontrolsublevel2 + " option").remove();
            $('select[id*=' + sontrolsublevel2 + ']')
                .append($("<option></option>")
                    .attr("value", "")
                    .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"));
            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
                success: function (msg) {

                    $.each(msg, function (index) {
                        $('select[id*=' + sontrolsublevel2 + ']')
                            .append($("<option></option>")
                                .attr("value", msg[index].nTermSubLevel2)
                                .text(sSubLV + " / " + msg[index].nTSubLevel2));
                    });

                }
            });
        }

        function funtionListSubLV2M(controlsublevel, controlsublevel2, setvalues) {
            var SubLVID = $('select[id$=' + controlsublevel + '] option:selected').val();
            var sSubLV = $('select[id$=' + controlsublevel + '] option:selected').text();
            $("#" + controlsublevel2 + " option").remove();
            $('select[id*=' + controlsublevel2 + ']')
                .append($("<option></option>")
                    .attr("value", "")
                    .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"));
            var request = $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
                success: function (msg) {

                    $.each(msg, function (index) {
                        $('select[id*=' + controlsublevel2 + ']')
                            .append($("<option></option>")
                                .attr("value", msg[index].nTermSubLevel2)
                                .text(msg[index].nTSubLevel2));
                    });

                }
            });
            request.done(function () {
                $('#' + controlsublevel2).val(setvalues);
            })
        }

        function functionListstudentM(controlsublevel, sontrolsublevel2) {
            var availableValueplane = [];
            $.ajax({
                url: "/App_Logic/dataGenericListData.ashx?mode=liststudent&nelevel=" +
                    $('select[id$=' + controlsublevel + '] option:selected').val() + "&nsublevel=" + $('#' + sontrolsublevel2 + ' option:selected').val(),
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].sName,
                            value: objjson[index].sID,
                            code: objjson[index].studentid,
                        };
                        availableValueplane[index] = newObject;
                    });
                }
            });
            return availableValueplane;
        }

    </script>
    <%--  <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTUser" ServicePath="AutoCompleteService.asmx" EnableCaching="true"
        FirstRowSelected="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>--%>
    <div class="full-card box-content userlist-container">
        <asp:HiddenField ID="hdfsid" runat="server" />
        <div class="form-group row student">
            <div class="col-md-6 col-xs-12">
                <label class="col-xs-12 col-md-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                <div class="col-xs-12 col-md-8 control-input">
                    <asp:DropDownList ID="ddlYear" runat="server" class="input--short" CssClass="form-control">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %>" Value="-1" class="grey hidden"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-xs-12 col-md-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                <div class="col-xs-12 col-md-8 control-input">
                    <asp:DropDownList ID="ddlTerm" runat="server" class="input--short" CssClass="form-control">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %>" Value="-1" class="grey hidden"></asp:ListItem>
                    </asp:DropDownList>
                    <%--   <asp:DropDownList ID="ddlSubLV2" runat="server" class="input--short" CssClass="form-control">
                        <asp:ListItem Value="" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"></asp:ListItem>
                    </asp:DropDownList>--%>
                </div>
            </div>
        </div>
        <div class="form-group row student">
            <div class="col-md-6 col-xs-12">
                <label class="col-xs-12 col-md-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                <div class="col-xs-12 col-md-8 control-input">
                    <asp:DropDownList ID="ddlSubLV" runat="server" class="input--short" CssClass="form-control">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %>" Value="-1" class="grey hidden"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-xs-12 col-md-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                <div class="col-xs-12 col-md-8 control-input">
                    <select id="ddlSubLV2" class="form-control input--short">
                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                    </select>
                    <%--   <asp:DropDownList ID="ddlSubLV2" runat="server" class="input--short" CssClass="form-control">
                        <asp:ListItem Value="" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"></asp:ListItem>
                    </asp:DropDownList>--%>
                </div>
            </div>
        </div>
        <div class="form-group row" id="row-name">
            <div class="col-md-6 col-xs-12 col-name">
                <label class="col-xs-12 col-md-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                <div class="col-xs-12 col-md-8 control-input">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass='form-control' class="input--mid" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101340") %>"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-12 col-md-6">
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 button-section">
                <asp:Button ID="btnSearch" class="btn btn-primary global-btn"
                    runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" ValidationGroup="add" />
            </div>
        </div>
        <div class="row mini--space__top">
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" ShowHeaderWhenEmpty="true"
                        OnDataBound="CustomersGridView_DataBound"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />
                        <PagerTemplate>
                            <table width="100%" class="tab">
                                <tr>
                                    <td style="width: 25%">
                                        <asp:Label ID="Label1" BorderColor="#337AB7"
                                            ForeColor="white"
                                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>:"
                                            runat="server" />
                                        <asp:DropDownList ID="PageDropDownList2"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged2"
                                            runat="server" />
                                    </td>
                                    <td style="width: 45%">
                                        <asp:LinkButton ID="backbutton" runat="server"
                                            CssClass="imjusttext" OnClick="backbutton_Click">
                                        </asp:LinkButton>
                                        <asp:DropDownList ID="PageDropDownList"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged"
                                            runat="server" />
                                        <asp:LinkButton ID="nextbutton" runat="server"
                                            CssClass="imjusttext" OnClick="nextbutton_Click">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 70%; text-align: right">
                                        <asp:Label ID="CurrentPageLabel"
                                            ForeColor="white"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </PagerTemplate>
                        <Columns>
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                <HeaderStyle Width="10%" CssClass="centerText" HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="sName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>">
                                <HeaderStyle Width="15%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="sLastName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>">
                                <HeaderStyle Width="15%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" DataField="sIdentification" ItemStyle-CssClass="centerText">
                                <HeaderStyle Width="14%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="status" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>">
                                <HeaderStyle Width="11%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="fingerStatus" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %>">
                                <HeaderStyle></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>">
                                <ItemTemplate>
                                    <center>
                                        <span title="<%# Eval("classroom") %>" data-toggle="tooltip">
                                            <%# Eval("classroom") %>
                                            <%--<%# ((string)Eval("classroom")).Length >10?((string)Eval("classroom")).Substring(0,10):((string)Eval("classroom")) %>--%>
                                        </span>
                                        <%--<button type="button" onclick="ShowPopUP(<%# Eval("sID") %>,'<%# Eval("sName").ToString() +" "+ Eval("sLastName").ToString() %>'); return false;"
                                            class="btn btn-permission <%# (bool)Eval("fingerStatus") ?"":"hidden" %>" data-toggle="modal" data-target="#modalpopupdatamac">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111044") %></button>
                                        <button type="button" onclick="ShowPopUPRegister(<%# Eval("sID") %>,'<%# Eval("sName").ToString() +" "+ Eval("sLastName").ToString() %>'); return false;"
                                            class="btn btn-permission <%# (bool)Eval("fingerStatus") ?"hidden":"" %>" data-toggle="modal" data-target="#modalpopupdatamac">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121047") %></button>--%>
                                    </center>
                                </ItemTemplate>
                                <HeaderStyle CssClass="centerText" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href="/userlist2-details.aspx?id=<%# Eval("sId") %>" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302012") %>" target="_blank" data-toggle="tooltip"><i class="fa fa-user"></i></a>&nbsp;
                                    <a onclick="getLinkEdit( <%# Eval("sId") %>);" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131231") %>" href="#" data-toggle="tooltip"><i class="fa fa-edit btnpermission"></i></a>&nbsp;
                                    <%--<asp:LinkButton ID="btnEdit" runat="server" class="fa fa-edit" data-toggle="tooltip" ToolTip="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131231") %>" CommandArgument='<%# Eval("sId") %>' CommandName="Edit" />--%>
                                    <a title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101229") %>" style="cursor: pointer;" data-toggle="tooltip" onclick="Showpass(<%# Eval("sId") %>)"><i class="fa fa-key"></i></a>&nbsp;
                                    <asp:LinkButton ID="btnDel" runat="server" class="fa fa-remove" OnClientClick="return false;" user-data='<%# Eval("sId") %>'
                                        ToolTip="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" data-toggle="tooltip" CommandArgument='<%# Eval("sId") %>' CommandName="Delete" />
                                </ItemTemplate>
                                <HeaderStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <asp:LinkButton ID="btnAdd" runat="server" class="btn-sm btn-success gvbutton" Text="เพื่มข้อมูล" data-toggle="tooltip" CommandName="Add" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataFormatString="sFinger" HeaderText="sFinger" Visible="False"></asp:BoundField>
                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="headerCell" />
                        <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="itemCell" />
                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div id="modalpopupdatamac" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true">
        <div class="modal-dialog maclist-modal" style="top: 50px;">
            <div class="modal-content">
                <div class="modal-header">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111044") %>                    <%--    <button type="button" id="modalpopupdata-close" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 style="font-size: 20px; text-align: center; font-weight: bold" class="modal-title"
                            id="modalpopupdata-header">Modal title</h4>--%>
                </div>
                <div class="modal-body" id="modalpopupdata-content">
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <label class="btn btn-primary" onclick="changeFinger();" style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111046") %></label>
                    <label class="btn btn-danger" ondblclick='$("#modalpopupdatamac").modal("hide");' onclick="" style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></label>
                </div>
            </div>
        </div>
    </div>
    <div class="spinner">
        <div class="rect1"></div>
        <div class="rect2"></div>
        <div class="rect3"></div>
        <div class="rect4"></div>
        <div class="rect5"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalpopup" runat="server">
    <div id="showpassword">
        <div class="row">
            <div class="col-xs-12">
                <div class="col-xs-3">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101227") %></label>
                </div>
                <div class="col-xs-7">
                    <input type="hidden" id="user_id" value="0" />
                    <input type="text" id="username" class="form-control" readonly="readonly" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="col-xs-3">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01511") %></label>
                </div>
                <div class="col-xs-7">
                    <input id="password" class="form-control" type="password" value="123" placeholder="password" readonly="readonly" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="col-xs-3">
                    <label>PIN</label>
                </div>
                <div class="col-xs-7">
                    <button class="btn btn-default" type="button" onclick="resetpin();">Reset PIN</button>
                </div>
            </div>
        </div>
    </div>
    <div id="changepassword" style="display: none;">
        <div class="row">
            <div class="col-xs-12">
                <div class="col-xs-4">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111081") %></label>
                </div>
                <div class="col-xs-8">
                    <input type="password" name="oldpassword" id="oldpassword" class="form-control" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="col-xs-4">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101232") %></label>
                </div>
                <div class="col-xs-8">
                    <input id="newpassword" name="newpassword" class="form-control" type="password" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="col-xs-4">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111082") %></label>
                </div>
                <div class="col-xs-8">
                    <input id="password_again" name="password_again" class="form-control" type="password" />
                </div>
            </div>
        </div>
    </div>
    <div class="row message_response" style="display: none;">
        <div class="col-xs-12 center">
            <label class="text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121044") %></label>
        </div>
    </div>
</asp:Content>


